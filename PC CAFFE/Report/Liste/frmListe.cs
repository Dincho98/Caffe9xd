using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Liste
{
    public partial class frmListe : Form
    {
        private bool pdfSpremi;
        private string nacin;

        public frmListe(bool pdfSpremi=false, string nacin=null)
        {
            InitializeComponent();
            this.pdfSpremi = pdfSpremi;
            this.nacin = nacin;
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string id_podgrupa { get; set; }
        public string _iz_poslovnice { get; set; }

        public string grupa { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);


            //dokumenat = "PrometPoRobi";
            //broj_dokumenta = "2";
            //skladiste = "2";
            //datumOD = DateTime.Now.AddDays(-2);
            //datumDO = DateTime.Now.AddDays(+10);

            if (dokumenat == "promjena_cijene")
            {
                promjenaCijene();
                this.Text = ImeForme;
            }
            else if (dokumenat == "meduskladisnica")
            {
                meduskladisnica();
                this.Text = ImeForme;
            }
            else if (dokumenat == "NORMATIV")
            {
                Normativ();
                this.Text = ImeForme;
            }
            else if (dokumenat == "PrometPoRobi")
            {
                PrometPoRobi();
                this.Text = ImeForme;
            }
            else if (dokumenat == "ODJAVA")
            {
                OdjavaRobe();
                this.Text = ImeForme;
            }
            else if (dokumenat == "PrometRobe")
            {
                PrometRobe();
                this.Text = ImeForme;
            }
            else if (dokumenat == "MEDU_POS")
            {
                meduskladisnica();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();


            if (pdfSpremi)
            {
                Global.GlobalFunctions.SpremiPdf("ProdajnaRoba"+nacin, reportViewer1);
                this.Close();
            }
        }

        private void promjenaCijene()
        {
            string sql1 = "SELECT " +
        " podaci_tvrtka.ime_tvrtke," +
        " podaci_tvrtka.skraceno_ime," +
        " podaci_tvrtka.oib," +
        " podaci_tvrtka.tel," +
        " podaci_tvrtka.fax," +
        " podaci_tvrtka.mob," +
        " podaci_tvrtka.iban," +
        " podaci_tvrtka.adresa," +
        " podaci_tvrtka.vl," +
        " podaci_tvrtka.poslovnica_adresa," +
        " podaci_tvrtka.poslovnica_grad," +
        " podaci_tvrtka.email," +
        " podaci_tvrtka.naziv_fakture," +
        " podaci_tvrtka.text_bottom," +
        " grad.grad + '' + grad.posta AS grad" +
        " FROM podaci_tvrtka" +
        " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
        "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = string.Format(@"SELECT
promjena_cijene_stavke.sifra,
roba.naziv,
promjena_cijene_stavke.kolicina AS jmj,
promjena_cijene_stavke.stara_cijena AS cijena1,
promjena_cijene_stavke.nova_cijena AS cijena3,
promjena_cijene_stavke.pdv AS cijena4,
CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CAST(promjena_cijene_stavke.stara_cijena AS numeric) AS cijena6,
CAST(CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CAST(promjena_cijene_stavke.stara_cijena AS numeric) AS numeric) - CAST((CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CASTpromjena_cijene_stavke.stara_cijena AS numeric)) / CAST('1.'+promjena_cijene_stavke.pdv AS numeric) AS numeric)  AS cijena5
FROM promjena_cijene_stavke
LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra
WHERE broj='{0}';", broj_dokumenta);

            sql_liste = string.Format(@"SELECT
promjena_cijene_stavke.sifra,
roba.naziv,
promjena_cijene_stavke.kolicina AS jmj,
promjena_cijene_stavke.stara_cijena AS cijena1,
promjena_cijene_stavke.nova_cijena AS cijena3,
promjena_cijene_stavke.pdv AS cijena4,
CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CAST(promjena_cijene_stavke.stara_cijena AS numeric) AS cijena5,
promjena_cijene_stavke.kolicina * (CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CAST(promjena_cijene_stavke.stara_cijena AS numeric)) AS cijena6
FROM promjena_cijene_stavke
LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra
WHERE broj='{0}';", broj_dokumenta);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT date FROM promjena_cijene WHERE broj='" + broj_dokumenta + "'", "promjena_cijene").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["date"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl3," +
                " 'Stara cijena' AS tbl4," +
                " 'Nova cijena' AS tbl5," +
                " 'PDV' AS tbl6," +
                " 'Iznos' AS tbl7," +
                " 'Ukupno' AS tbl8," +
                " promjena_cijene.date AS datum1," +
                " promjena_cijene.napomena AS komentar," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('ZAPISNIK O PROMJENI CIJENE  ' AS nvarchar) + CAST (promjena_cijene.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM promjena_cijene " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=promjena_cijene.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=promjena_cijene.id_izradio " +
                " WHERE broj ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void meduskladisnica()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            /*
            string sql_liste = "SELECT " +
                " meduskladisnica_stavke.sifra," +
                " roba_prodaja.naziv," +
                " meduskladisnica_stavke.kolicina AS cijena1," +
                " roba_prodaja.mjera AS jmj," +
                //" meduskladisnica_stavke.vpc AS cijena3 ," +
                " meduskladisnica_stavke.mpc AS cijena5," +
                " meduskladisnica_stavke.pdv AS cijena4," +
                " CAST(meduskladisnica_stavke.mpc AS money) * CAST(meduskladisnica_stavke.kolicina AS numeric) AS cijena6 " +
                " FROM meduskladisnica_stavke" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=meduskladisnica_stavke.sifra WHERE broj='" + broj_dokumenta + "' AND iz_skladista='" + skladiste + "'";
            */

            string query = @"SELECT medu_poslovnice.sifra,
                roba_prodaja.naziv,
                medu_poslovnice.kolicina AS cijena1,
                roba_prodaja.mjera AS jmj,
                medu_poslovnice.nbc AS cijena5,
                medu_poslovnice.pdv AS cijena4,
                medu_poslovnice.nbc*medu_poslovnice.kolicina AS cijena6
                FROM medu_poslovnice" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=medu_poslovnice.sifra " +
                " WHERE broj='" + broj_dokumenta + "' AND godina='" + godina + "' AND iz_poslovnice='" + _iz_poslovnice + "';";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(query).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(query).Fill(dSRliste, "DTliste");
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                " 'MPC' AS tbl7," +
                " 'PDV %' AS tbl6," +
                " 'Iznos' AS tbl8," +
                " medu_poslovnice.datum AS datum1," +
                " medu_poslovnice.napomena AS komentar," +
                " 'Iz poslovnice: '+iz.naziv_poslovnice + ' u skladište: ' + u.naziv_poslovnice AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('MEĐUSKLADIŠNICA IZMEĐU POSLOVNICA ' AS nvarchar) + CAST (medu_poslovnice.broj AS nvarchar) +'/'+ CAST (" + godina + " AS nvarchar) AS naslov" +
                " FROM medu_poslovnice " +
                @"LEFT JOIN poslovnice u ON u.fiskalna_oznaka_poslovnice=medu_poslovnice.u_poslovnicu
                LEFT JOIN poslovnice iz ON iz.fiskalna_oznaka_poslovnice=medu_poslovnice.iz_poslovnice
                LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=medu_poslovnice.id_izradio
                WHERE medu_poslovnice.broj ='" + broj_dokumenta + "' AND medu_poslovnice.godina='" + godina + "' AND iz_poslovnice='" + _iz_poslovnice + "';";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        /*
        private void meduskladisnica()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " meduskladisnica_stavke.sifra," +
                " roba_prodaja.naziv," +
                " meduskladisnica_stavke.kolicina AS cijena1," +
                " roba_prodaja.mjera AS jmj," +
                //" meduskladisnica_stavke.vpc AS cijena3 ," +
                " meduskladisnica_stavke.mpc AS cijena5," +
                " meduskladisnica_stavke.pdv AS cijena4," +
                " CAST(meduskladisnica_stavke.mpc AS money) * CAST(meduskladisnica_stavke.kolicina AS numeric) AS cijena6 " +
                " FROM meduskladisnica_stavke" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=meduskladisnica_stavke.sifra WHERE broj='" + broj_dokumenta + "' AND iz_skladista='" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM meduskladisnica WHERE broj='" + broj_dokumenta + "'", "meduskladisnica").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                //" 'VPC' AS tbl5," +
                " 'MPC' AS tbl7," +
                " 'PDV iznos' AS tbl6," +
                " 'Iznos' AS tbl8," +
                " meduskladisnica.datum AS datum1," +
                " meduskladisnica.napomena AS komentar," +
                " 'Iz skladišta: '+skladiste.skladiste + ' u skladište: ' + T2.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('MEĐUSKLADIŠNICA  ' AS nvarchar) + CAST (meduskladisnica.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM meduskladisnica " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=meduskladisnica.id_skladiste_od " +
                " LEFT JOIN skladiste T2 ON T2.id_skladiste = meduskladisnica.id_skladiste_do " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=meduskladisnica.id_izradio " +
                " WHERE meduskladisnica.broj ='" + broj_dokumenta + "' AND meduskladisnica.id_skladiste_od='"+skladiste+"'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
        */

        private void Normativ()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " normativi_stavke.sifra_robe," +
                " roba.naziv," +
                " normativi_stavke.kolicina AS cijena1," +
                " roba.jm AS jmj," +
                " roba_prodaja.vpc AS cijena3 ," +
                " (CAST(roba_prodaja.vpc AS NUMERIC)*(CAST(roba_prodaja.porez AS NUMERIC)zbrojCAST(roba_prodaja.porez_potrosnja AS NUMERIC))/100)zbrojCAST(roba_prodaja.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4, " +
                " (CAST(roba_prodaja.vpc AS NUMERIC)*(CAST(roba_prodaja.porez AS NUMERIC)zbrojCAST(roba_prodaja.porez_potrosnja AS NUMERIC))/100)zbrojCAST(roba_prodaja.vpc AS NUMERIC)*CAST(normativi_stavke.kolicina AS NUMERIC) AS cijena6 " +
                " FROM normativi_stavke" +
                " LEFT JOIN roba ON roba.sifra=normativi_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=normativi_stavke.id_skladiste" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste=normativi_stavke.id_skladiste " +
                " WHERE broj_normativa='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = DT.Rows[0]["godina_normativa"].ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                " 'VPC' AS tbl5," +
                " 'MPC' AS tbl7," +
                " 'Skladište' AS tbl6," +
                " 'Iznos' AS tbl8," +
                //"  normativi.godina_normativa AS datum1," +
                "  normativi.komentar AS komentar," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('Normativ  ' AS nvarchar) + CAST (normativi.broj_normativa AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM normativi " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=normativi.id_zaposlenik " +
                " WHERE normativi.broj_normativa ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void PrometPoRobi()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string skl = "";
            if (skladiste != null)
            {
                skl = " AND racun_stavke.id_skladiste='" + skladiste + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string sql = "SELECT " +
                " SUM(CAST(REPLACE(racun_stavke.kolicina,',','.')  AS numeric)) AS kolicina," +
                " roba.naziv," +
                " roba.jm," +
                " racun_stavke.sifra_robe" +
                " FROM racun_stavke" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE  racuni.datum_racuna>'" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " " + skl + blag + duc + art + " GROUP BY naziv,jm,sifra_robe ORDER BY kolicina DESC";

            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                string sql_normativi = "SELECT " +
                    " caffe_normativ.sifra_normativ," +
                    " caffe_normativ.kolicina," +
                    " roba_prodaja.naziv," +
                    " roba_prodaja.kolicina AS kk," +
                    " skladiste.skladiste," +
                    " roba_prodaja.nc," +
                    " roba_prodaja.ulazni_porez," +
                    " roba_prodaja.mjera" +
                    " FROM caffe_normativ " +
                    " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=caffe_normativ.sifra_normativ " +
                    " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste" +
                    " WHERE caffe_normativ.sifra='" + row["sifra_robe"].ToString() + "'";

                DataTable DTn = classSQL.select(sql_normativi, "normativi").Tables[0];

                if (DTn.Rows.Count == 0)
                {
                    DataRow DTrow = dSRliste.Tables[0].NewRow();
                    DTrow["sifra"] = row["sifra_robe"].ToString();
                    DTrow["naziv"] = row["naziv"].ToString();
                    DTrow["jmj"] = row["jm"].ToString();
                    DTrow["cijena1"] = Convert.ToDecimal(row["kolicina"].ToString()).ToString("#0.000");
                    DTrow["cijena3"] = "0.00";
                    DTrow["cijena4"] = "-------";
                    DTrow["cijena5"] = "0.00";
                    DTrow["cijena6"] = "0.00";
                    dSRliste.Tables[0].Rows.Add(DTrow);
                }
                else
                {
                    foreach (DataRow r in DTn.Rows)
                    {
                        DataRow DTrow = dSRliste.Tables[0].NewRow();
                        DTrow["sifra"] = r["sifra_normativ"].ToString();
                        DTrow["naziv"] = r["naziv"].ToString();
                        DTrow["jmj"] = r["mjera"].ToString();
                        DTrow["cijena1"] = Convert.ToDecimal(Convert.ToDecimal(r["kolicina"].ToString()) * Convert.ToDecimal(row["kolicina"].ToString())).ToString("#0.000");
                        DTrow["cijena3"] = r["kk"].ToString();
                        DTrow["cijena4"] = r["skladiste"].ToString();
                        DTrow["cijena5"] = r["ulazni_porez"].ToString();
                        DTrow["cijena6"] = (Convert.ToDecimal(r["nc"].ToString()) * (Convert.ToDecimal(Convert.ToDecimal(r["kolicina"].ToString()) * Convert.ToDecimal(row["kolicina"].ToString())))).ToString("#0.00");
                        dSRliste.Tables[0].Rows.Add(DTrow);
                    }
                }
            }

            string year = "";

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Stanje SK' AS tbl5," +
                " 'Skladište' AS tbl6," +
                " 'Ul.porez' AS tbl7," +
                " 'MPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi sa skladišta ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD.ToString("dd.MM.yyyy H:mm:ss") + " - " + datumDO.ToString("dd.MM.yyyy H:mm:ss") + s + b + a + "' AS komentar" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void OdjavaRobe()
        {
            string sql1 = "SELECT " +
               " podaci_tvrtka.ime_tvrtke," +
               " podaci_tvrtka.skraceno_ime," +
               " podaci_tvrtka.oib," +
               " podaci_tvrtka.tel," +
               " podaci_tvrtka.fax," +
               " podaci_tvrtka.mob," +
               " podaci_tvrtka.iban," +
               " podaci_tvrtka.adresa," +
               " podaci_tvrtka.vl," +
               " podaci_tvrtka.poslovnica_adresa," +
               " podaci_tvrtka.poslovnica_grad," +
               " podaci_tvrtka.email," +
               " podaci_tvrtka.zr," +
               " podaci_tvrtka.naziv_fakture," +
               " podaci_tvrtka.text_bottom," +
               " grad.grad + '' + grad.posta AS grad" +
               " FROM podaci_tvrtka" +
               " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
               "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " povrat_robe_stavke.sifra," +
                " roba_prodaja.naziv," +
                " roba_prodaja.mjera AS jmj," +
                " povrat_robe_stavke.kolicina AS cijena1," +
                " povrat_robe_stavke.nbc AS cijena3," +
                " povrat_robe_stavke.pdv AS cijena4," +
                " CAST(povrat_robe_stavke.nbc AS money) * CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS numeric) AS cijena6" +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=povrat_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM povrat_robe WHERE broj='" + broj_dokumenta + "'", "povrat_robe").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Cijena' AS tbl5," +
                " 'Porez' AS tbl6," +
                " 'Iznos ' AS tbl8," +
                " povrat_robe.datum AS datum1," +
                " povrat_robe.napomena AS komentar," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('ODJAVA ROBE: ' AS nvarchar) + CAST (povrat_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM povrat_robe " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=povrat_robe.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=povrat_robe.id_izradio " +
                " WHERE broj ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private decimal ukupno_rabat = 0;

        private void PrometRobe()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            ukupno_rabat = 0;

            string skl = "";
            if (id_podgrupa != null)
            {
                skl = " AND roba.id_podgrupa='" + id_podgrupa + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string gr = "";
            if (grupa != null)
            {
                gr = " AND grupa.id_grupa='" + grupa + "'";
            }

            string sql = "SELECT " +
            " racun_stavke.kolicina," +
            " grupa.grupa," +
            " racun_stavke.sifra_robe," +
            " racun_stavke.mpc," +
            " racun_stavke.porez_potrosnja," +
            " racun_stavke.porez," +
            " racun_stavke.rabat," +
            " racuni.nacin_placanja," +
            " roba.naziv," +
            " roba.jm" +
            " FROM racun_stavke" +
            " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
            " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
            " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
            " WHERE  racuni.datum_racuna>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            " " + skl + blag + duc + art + gr + " ORDER BY grupa";
            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

            decimal kol = 0;
            decimal pnp = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal pnpUKUPNO = 0;
            decimal pdvUKUPNO = 0;
            decimal SVE_UKUPNO = 0;
            decimal OSNOVICA = 0;
            string g = "";

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
            }

            if (DTpdvN.Columns["stopa"] == null)
            {
                DTpdvN.Columns.Add("stopa");
                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
            }
            else
            {
                DTpdvN.Clear();
            }

            //if (DTosnovica.Columns["osnovica"] == null)
            //{
            //    DTosnovica.Columns.Add("osnovica");
            //    DTosnovica.Columns.Add("nacin");
            //}
            //else
            //{
            //    DTosnovica.Clear();
            //}

            decimal UG = 0;
            decimal UK = 0;
            decimal UV = 0;
            decimal rabat = 0;

            foreach (DataRow row in DT.Rows)
            {
                kol = Convert.ToDecimal(row["kolicina"].ToString());
                mpc = Convert.ToDecimal(row["mpc"].ToString());
                pnp = Convert.ToDecimal(row["porez_potrosnja"].ToString());
                pdv = Convert.ToDecimal(row["porez"].ToString());

                rabat = Convert.ToDecimal(row["rabat"].ToString());
                ukupno_rabat = (mpc * rabat / 100) + ukupno_rabat;
                mpc = mpc - (mpc * rabat / 100);

                //Ovaj kod dobiva PDV
                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
                decimal ppdv = (((mpc * kol) * PreracunataStopaPDV) / 100);
                pdvUKUPNO = ppdv + pdvUKUPNO;

                //Ovaj kod dobiva porez na potrošnju
                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
                decimal ppnp = (((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100);
                pnpUKUPNO = ppnp + pnpUKUPNO;

                SVE_UKUPNO = (mpc * kol) + SVE_UKUPNO;

                if (row["nacin_placanja"].ToString() == "G")
                {
                    StopePDVaN(pdv, ppdv, "G", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UG = (mpc * kol) + UG;
                }
                else if (row["nacin_placanja"].ToString() == "K")
                {
                    StopePDVaN(pdv, ppdv, "K", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UK = (mpc * kol) + UK;
                }
                else if (row["nacin_placanja"].ToString() == "T")
                {
                    StopePDVaN(pdv, ppdv, "T", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UV = (mpc * kol) + UV;
                }

                string ajjj = row["nacin_placanja"].ToString();
                Artikli(row["naziv"].ToString(), kol, row["sifra_robe"].ToString(), mpc, row["jm"].ToString(), ppnp, ppdv);
                StopePDVa(pdv, ((mpc * kol) * PreracunataStopaPDV) / 100);

                OSNOVICA = ((mpc * kol) - ((ppdv) + (ppnp))) + OSNOVICA;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string year = "";

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }
            string pdv_ispis = "";

            for (int p = 0; p < DTpdv.Rows.Count; p++)
            {
                pdv_ispis += "PDV " + DTpdv.Rows[p]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[p]["iznos"].ToString()).ToString("#0.000") + "\r\n";
            }

            string stope = "";
            if (ukupno_rabat > 0)
            {
                stope += "\r\n\r\nUKUPNO RABAT:     " + ukupno_rabat.ToString("#0.00") + "\r\n";
            }
            stope += "PNP ukupno:       " + pnpUKUPNO.ToString("#0.000") + "\r\n";

            //for(int i=0; i<DTpdv.Rows.Count; i++)
            //{
            //    PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00"));
            //}

            //GOTOVINA
            if (UG > 0)
            {
                stope += "UKUPNO GOTOVINA:  " + UG.ToString("#0.000") + "\r\n";
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "G")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "G")
                    {
                        stope += "OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000") + "\r\n";
                        stope += "PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000") + "\r\n";
                    }
                }
            }

            //KARTICA
            if (UK > 0)
            {
                stope += "UKUPNO KARTICE:   " + UK.ToString("#0.000") + "\r\n";
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "K")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "K")
                    {
                        stope += "OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000") + "\r\n";
                        stope += "PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000") + "\r\n";
                    }
                }
            }

            //VIRMAN
            if (UV > 0)
            {
                stope += "UKUPNO VIRMAN:    " + UV.ToString("#0.000") + "\r\n";
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "T")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "T")
                    {
                        stope += "OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000") + "\r\n";
                        stope += "PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000") + "\r\n";
                    }
                }
            }

            stope += "OSNOVICA UKUPNO:  " + OSNOVICA.ToString("#0.00") + "\r\n";
            for (int i = 0; i < DTpdv.Rows.Count; i++)
            {
                stope += "PDV " + DTpdv.Rows[i]["stopa"].ToString() + "% UKUPNO:   " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00") + "\r\n";
            }
            stope += "SVE UKUPNO:       " + SVE_UKUPNO.ToString("#0.00") + "\r\n";

            //string odabrano "OD DATUMA: "+datumOD.ToString()+"DO DATUM:"+datumDO.ToString();

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'PNP iznos' AS tbl4," +
                " 'PDV iznos' AS tbl5," +
                " 'Količina' AS tbl6," +
                " 'Od datuma:" + datumOD.ToString("dd.MM.yyyy") + " - " + datumDO.ToString("dd.MM.yyyy") + "' AS skladiste," +
                " 'Ukupno porez%' AS tbl7," +
                " 'MPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + stope + "' AS komentar," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD.ToString("dd.MM.yyyy") + " - " + datumDO.ToString("dd.MM.yyyy") + s + b + a + "' AS komentar" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        //DSRliste.DTlisteDataTable DTartikli = new DSRliste.DTlisteDataTable();

        private void Artikli(string artikl, decimal kolicina, string sifra, decimal mpc, string jmj, decimal pnp, decimal pdv_iznos)
        {
            DataRow[] dataROW = dSRliste.Tables["DTliste"].Select("sifra = '" + sifra + "' AND cijena7='" + mpc + "'");

            if (dataROW.Count() == 0)
            {
                DataRow RowArtikl = dSRliste.Tables[0].NewRow();
                RowArtikl["sifra"] = sifra;
                RowArtikl["naziv"] = artikl;
                RowArtikl["jmj"] = jmj;
                RowArtikl["cijena1"] = pnp;
                RowArtikl["cijena3"] = pdv_iznos;
                RowArtikl["cijena4"] = kolicina.ToString();
                RowArtikl["cijena5"] = (pnp + pdv_iznos).ToString("#0.000");
                RowArtikl["cijena6"] = mpc * kolicina;
                RowArtikl["cijena7"] = mpc;
                dSRliste.Tables["DTliste"].Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["cijena4"] = Convert.ToDecimal(dataROW[0]["cijena4"].ToString()) + kolicina;
                dataROW[0]["cijena1"] = Convert.ToDecimal(dataROW[0]["cijena1"].ToString()) + pnp;
                dataROW[0]["cijena3"] = Convert.ToDecimal(dataROW[0]["cijena3"].ToString()) + pdv_iznos;
                dataROW[0]["cijena5"] = (Convert.ToDecimal(dataROW[0]["cijena5"].ToString()) + pnp + pdv_iznos).ToString("#0.000");
                dataROW[0]["cijena6"] = (Convert.ToDecimal(dataROW[0]["cijena6"].ToString()) + mpc * kolicina).ToString("#0.000");
            }
        }

        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal pdv_stavka)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = pdv.ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
            }
        }

        private DataTable DTpdvN = new DataTable();

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            if (osnovica < 0 && Convert.ToInt16(pdv) == 0)
            {
            }

            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = osnovica;
                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }
    }
}