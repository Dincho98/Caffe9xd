using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Odjava
{
    public partial class frmOdjava : Form
    {
        public frmOdjava()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void frmOdjava_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            if (dokumenat == "odjava")
            {
                odjava();
                this.Text = ImeForme;
            }
            else if (dokumenat == "POVRATNICA")
            {
                povrat_robe();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private void odjava()
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
                " odjava_komisione_stavke.sifra," +
                " roba.naziv," +
                " roba.jm AS naziv2," +
                " odjava_komisione_stavke.kolicina_prodano AS kolicina," +
                " odjava_komisione_stavke.rabat AS cijena1 ," +
                " odjava_komisione_stavke.nbc AS cijena2," +
                " CAST(odjava_komisione_stavke.nbc AS money) * CAST(odjava_komisione_stavke.kolicina_prodano AS numeric) AS cijena3" +
                " FROM odjava_komisione_stavke" +
                " LEFT JOIN roba ON roba.sifra=odjava_komisione_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM odjava_komisione WHERE broj='" + broj_dokumenta + "'", "odjava_komisione").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Rabat%' AS tbl5," +
                " 'Cijena' AS tbl6," +
                " 'Iznos' AS tbl7," +
                " odjava_komisione.datum AS datum1," +
                " odjava_komisione.napomena AS komentar," +
                " odjava_komisione.od_datuma AS datum2," +
                " odjava_komisione.do_datuma AS datum3," +
                " partners.ime_tvrtke AS string2," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('PREDMET: ODJAVA ROBE BROJ: ' AS nvarchar) + CAST (odjava_komisione.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM odjava_komisione " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=odjava_komisione.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=odjava_komisione.id_zaposlenik " +
                " LEFT JOIN partners ON partners.id_partner=odjava_komisione.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
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

        private void povrat_robe()
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
                " roba.naziv," +
                " roba.jm AS naziv2," +
                " povrat_robe_stavke.kolicina AS kolicina," +
                " povrat_robe_stavke.rabat AS cijena1 ," +
                " povrat_robe_stavke.nbc AS cijena2," +
                " CAST(povrat_robe_stavke.nbc AS money) * CAST(povrat_robe_stavke.kolicina AS numeric) AS cijena3" +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba ON roba.sifra=povrat_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

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
                " 'Rabat%' AS tbl5," +
                " 'Cijena' AS tbl6," +
                " 'Iznos' AS tbl7," +
                " povrat_robe.datum AS datum1," +
                " povrat_robe.napomena AS komentar," +
                " partners.ime_tvrtke AS string2," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('POVRATNICA DOBAVLJAČU: ' AS nvarchar) + CAST (povrat_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM povrat_robe " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=povrat_robe.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=povrat_robe.id_izradio " +
                " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
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
    }
}