using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCPOS.Caffe
{
    public partial class frmBackOffice : Form
    {
        public Caffe.frmCaffe MainForm { get; set; }

        public frmBackOffice()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void btnUnosNormativa_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajnaRoba prp = new Caffe.frmProdajnaRoba();
            prp.ShowDialog();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTzaposlenik;

        private void frmBackOffice_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            if (MainForm != null)
            {
                this.BackColor = MainForm.BackColor;
            }

            if (DateTime.Now.Hour > 4)
            {
                dtpOD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 01);
                dtpDO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }
            else
            {
                dtpOD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 5, 0, 01);
                dtpDO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 4, 59, 59);
            }

            DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];
            if (DTzaposlenik.Rows.Count > 0)
            {
                if (DTpostavke.Rows[0]["zabraniUvidSkladiste"].ToString() == "1")
                {
                    btnRobaNaSkladistu.Visible = false;
                    btnRobaNaSkladistu.Visible = false;
                    btnRobaNaSkladistu.Visible = false;
                    btnRobaNaSkladistu.Visible = false;
                }
            }

            if (DTpostavke.Rows[0]["zaposlenici_vide_samo_danasnju_prodaju"].ToString() == "1" && Util.Korisno.UzmiOvlastTrenutnogZaposlenika() == "user")
            {
                flowLayoutPanel1.Visible = false;
                dtpOD.Visible = false;
                dtpDO.Visible = false;
                btnTrazi.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }

            /* if (File.Exists("hamer"))
             {
                 btnSinkronizacija.Visible = true;
                 button3.Text = "X-ČITANJE";
             }
             else
             {
                 btnSinkronizacija.Visible = false;
             }*/

            if (Util.Korisno.oibTvrtke == "23315587468" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() == "4")
            {
                btnKarticaKupca.Visible = false;
            }
            SetDnevniIzvještaj(dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss"));
            SetPotrosenaRoba(dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss"));
        }

        private void SetDnevniIzvještaj(string datumOD, string datumDO)
        {
            DataTable DT = new DataTable();
            DataTable DT3 = new DataTable();

            string dodatan_uvijet_dopustenja = "";
            string dodatan_uvijet_dopustenja_zaposlenici = "";
            int dop;
            int.TryParse(DTzaposlenik.Rows[0]["id_dopustenje"].ToString(), out dop);
            if (DTpostavke.Rows[0]["zabrana_zaposleniku_da_vidi_druge_promete"].ToString() == "1" && dop > 2)
            {
                dodatan_uvijet_dopustenja = " AND racuni.id_blagajnik='" + Properties.Settings.Default.id_zaposlenik + "'";
                dodatan_uvijet_dopustenja_zaposlenici = " AND z.id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'";
                flowLayoutPanel1.Visible = false;
            }

            lblUkupno.Text = "Ukupno: 0.00 kn";
            lblGotovina.Text = "Gotovina: 0.00 kn";
            lblKartice.Text = "Kartice: 0.00 kn";
            lblOstalo.Text = "Ostalo" + (Class.PodaciTvrtka.oibTvrtke == "98816793336" ? " - FOODEX" : "") + ": 0.00 kn";
            lblFakture.Text = "Fakture: 0.00 kn";
            lblOtpremnice.Text = "Otpremnice: 0.00 kn";

            decimal ukupno = 0;
            decimal faktureUkupno = 0;
            decimal otpremniceUkupno = 0;

            // Racuni
            string sql = "select sum(gotovina) as gotovina, sum(kartice) as kartice, sum(virman) as virman, sum(ostalo) as ostalo from ( " +
                    "select coalesce(case when y.nacin_placanja = 'G' then ukupno end, 0) as gotovina, coalesce(case when y.nacin_placanja = 'K' then ukupno end, 0) as kartice, " +
                    "coalesce(case when y.nacin_placanja = 'T' then ukupno end, 0) as virman, coalesce(case when y.nacin_placanja = 'O' then ukupno end, 0) as ostalo from ( " +
                    "select sum(x.ukupno) as ukupno, x.nacin_placanja from ( " +
                    "SELECT (racun_stavke.mpc::numeric - (racun_stavke.mpc::numeric * (replace(racun_stavke.rabat, ',','.')::numeric /100))) * replace(racun_stavke.kolicina, ',','.')::numeric as ukupno, racuni.nacin_placanja " +
                    "FROM racun_stavke " +
                    "LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna AND racuni.godina=racun_stavke.godina " +
                    "WHERE racuni.datum_racuna > '" + datumOD + "' AND racuni.datum_racuna < '" + datumDO + "'" + dodatan_uvijet_dopustenja + "  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "') x " +
                    "group by x.nacin_placanja) y " +
                    ") a;";

            DT = classSQL.select(sql, "racuni").Tables[0];
            if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
            {
                string b = "0";

                if (DT.Rows[0]["virman"].ToString() != "")
                    b = DT.Rows[0]["virman"].ToString();

                ukupno += Convert.ToDecimal(DT.Rows[0]["gotovina"].ToString()) + Convert.ToDecimal(DT.Rows[0]["kartice"].ToString()) + Convert.ToDecimal(DT.Rows[0]["ostalo"].ToString());
                lblGotovina.Text = "Gotovina: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString())) + " kn";
                lblKartice.Text = "Kartice: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["kartice"].ToString())) + " kn";

                if (DT.Rows[0]["ostalo"].ToString() != "")
                    lblOstalo.Text = "Ostalo" + (Class.PodaciTvrtka.oibTvrtke == "98816793336" ? " - FOODEX" : "") + ": " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["ostalo"].ToString())) + " kn";
                else
                    lblOstalo.Text = "Ostalo" + (Class.PodaciTvrtka.oibTvrtke == "98816793336" ? " - FOODEX" : "") + ": 0.00 kn";
            }

            // Fakture
            string queryFakture = $@"SELECT SUM(fakture.ukupno) AS ukupno
                                    FROM fakture
                                    WHERE fakture.date > '{datumOD}' AND fakture.date < '{datumDO}' AND fakture.id_ducan = '{Util.Korisno.idDucan}'";
            DataTable DTfakture = classSQL.select(queryFakture, "fakture").Tables[0];
            if (DTfakture.Rows.Count > 0)
            {
                decimal.TryParse(DTfakture.Rows[0]["ukupno"].ToString(), out faktureUkupno);
                if (faktureUkupno > 0)
                    ukupno += faktureUkupno;
                lblFakture.Text = "Fakture: " + faktureUkupno.ToString("#0.00") + " kn";
            }

            // Otpremnice
            string queryOtpremnice = $@"SELECT SUM(otpremnice.ukupno) AS ukupno
                                        FROM otpremnice
                                        WHERE otpremnice.datum > '{datumOD}' AND otpremnice.datum < '{datumDO}' AND naplaceno_fakturom = 1";
            DataTable DTotpremnice = classSQL.select(queryOtpremnice, "otpremnice").Tables[0];
            if(DTotpremnice.Rows.Count > 0)
            {
                decimal.TryParse(DTotpremnice.Rows[0]["ukupno"].ToString(), out otpremniceUkupno);
                if (otpremniceUkupno > 0)
                    ukupno += otpremniceUkupno;
                lblOtpremnice.Text = "Otpremnice: " + otpremniceUkupno.ToString("#0.00") + " kn";
            }

            if (ukupno > 0)
                lblUkupno.Text = "Ukupno: " + ukupno.ToString("#0.00") + " kn";
            else
                lblUkupno.Text = "Ukupno: 0.00 kn";

            string sql_konobar = "DROP TABLE IF EXISTS tempT; " +
                "CREATE TEMP TABLE tempT AS " +
                "SELECT sum(a.gotovina) AS gotovina, sum(a.kartice) AS kartice, sum(a.virman) AS virman, sum(a.ostalo) AS ostalo, a.id_blagajnik FROM ( " +
                    "SELECT COALESCE(CASE WHEN y.nacin_placanja = 'G' THEN ukupno END, 0) AS gotovina, COALESCE(CASE WHEN y.nacin_placanja = 'K' THEN ukupno END, 0) AS kartice, " +
                    "COALESCE(CASE WHEN y.nacin_placanja = 'T' THEN ukupno END, 0) AS virman, COALESCE(CASE WHEN y.nacin_placanja = 'O' THEN ukupno END, 0) AS ostalo, y.id_blagajnik FROM ( " +
                    "SELECT sum(x.ukupno) AS ukupno, x.nacin_placanja, x.id_blagajnik FROM ( " +
                        "SELECT (racun_stavke.mpc::NUMERIC - (racun_stavke.mpc::NUMERIC * (REPLACE(racun_stavke.rabat, ',','.')::NUMERIC /100))) * REPLACE(racun_stavke.kolicina, ',','.')::NUMERIC AS ukupno, racuni.nacin_placanja, racuni.id_blagajnik " +
                        "FROM racun_stavke " +
                        "LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna AND racuni.godina=racun_stavke.godina " +
                        "WHERE racuni.datum_racuna > '" + datumOD + "' AND racuni.datum_racuna < '" + datumDO + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "' ) x " +
                    "GROUP BY x.id_blagajnik, x.nacin_placanja) y " +
                ") a " +
                "GROUP BY a.id_blagajnik; " +

                "select z.ime, z.prezime, t.gotovina, t.kartice, t.virman, t.ostalo, t.id_blagajnik " +
                "from zaposlenici z " +
                "left join tempT t on t.id_blagajnik = z.id_zaposlenik " +
                "where z.aktivan = 'DA' and t.id_blagajnik = z.id_zaposlenik;";

            DataTable DTkonobari = classSQL.select(sql_konobar, "zaposlenici").Tables[0];
            flpArtikli.Controls.Clear();
            int i = 1;

            Chart1.Series.Clear();

            foreach (DataRow row in DTkonobari.Rows)
            {
                GroupBox groupBox1 = new GroupBox();
                groupBox1.Location = new System.Drawing.Point(3, 3);
                groupBox1.Name = "groupBox1";
                groupBox1.Size = new System.Drawing.Size(425, 75);
                groupBox1.TabIndex = 0;
                groupBox1.TabStop = false;

                Label lbl = new Label();
                lbl.BackColor = System.Drawing.Color.Transparent;
                lbl.Font = new System.Drawing.Font("Verdana", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Location = new System.Drawing.Point(10, 15);
                lbl.AutoSize = true;
                lbl.TabIndex = i;

                string Ime_konobara = row["ime"].ToString() + " " + row["prezime"].ToString();

                string a = "0";
                string b = "0";

                if (row["ostalo"].ToString() != "") { a = row["ostalo"].ToString(); }
                if (row["virman"].ToString() != "") { b = row["virman"].ToString(); }

                lbl.Name = row["id_blagajnik"].ToString();
                lbl.Text = row["ime"].ToString() + " " + row["prezime"].ToString() + "  " + Convert.ToDecimal(Convert.ToDecimal(row["gotovina"].ToString()) + Convert.ToDecimal(row["kartice"].ToString()) + Convert.ToDecimal(a) + Convert.ToDecimal(b) + faktureUkupno).ToString("#0.00") +
                    " kn\r\nGotovina: " + Convert.ToDouble(row["gotovina"].ToString()).ToString("#0.00") +
                    " kn,  Kartice: " + Convert.ToDouble(row["kartice"].ToString()).ToString("#0.00") + " kn" +
                    " \nFakture: " + faktureUkupno.ToString("#0.00") + " kn," +
                    "  Ostalo: " + Convert.ToDouble(a).ToString("#0.00") + " kn";

                Chart1.Series.Add(Ime_konobara);
                Chart1.Series[Ime_konobara].Points.AddY(Convert.ToDouble(row["gotovina"].ToString()) + Convert.ToDouble(row["kartice"].ToString()));
                Chart1.Series[Ime_konobara].ChartType = SeriesChartType.Column;
                Chart1.Series[Ime_konobara]["PointWidth"] = "0.5";
                Chart1.Series[Ime_konobara].IsValueShownAsLabel = true;
                Chart1.Series[Ime_konobara]["BarLabelStyle"] = "Center";
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Series[Ime_konobara]["DrawingStyle"] = "Cylinder";

                groupBox1.Controls.Add(lbl);
                flpArtikli.Controls.Add(groupBox1);
                i++;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            RefreshGrids();
            SetDnevniIzvještaj(dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss"));
            SetPotrosenaRoba(dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss"));
        }

        private DataRow Row;

        private void SetPotrosenaRoba(string OD, string DO)
        {
            if (DTs.Columns["sifra"] == null)
            {
                DTs.Columns.Add("sifra");
                DTs.Columns.Add("naziv");
                DTs.Columns.Add("kolicina");
            }
            else
            {
                DTs.Clear();
            }

            string dodatan_uvjet_dopustenja = "";
            int dop;
            int.TryParse(DTzaposlenik.Rows[0]["id_dopustenje"].ToString(), out dop);
            if (DTpostavke.Rows[0]["zabrana_zaposleniku_da_vidi_druge_promete"].ToString() == "1" && dop > 2)
            {
                dodatan_uvjet_dopustenja = " AND id_blagajnik='" + Properties.Settings.Default.id_zaposlenik + "'";
            }

            DataTable DTitems_sort = new DataTable();
            DTitems_sort.Columns.Add("Šifra", typeof(string));
            DTitems_sort.Columns.Add("Naziv", typeof(string));
            DTitems_sort.Columns.Add("Količina", typeof(double));

            string queryRoba = $@"SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) AS kolicina,
	                        roba.naziv,
	                        racun_stavke.sifra_robe
                        FROM racun_stavke
                        LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                        LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
                        WHERE racuni.datum_racuna>'{OD}' {dodatan_uvjet_dopustenja} AND racuni.datum_racuna<'{DO}' AND racun_stavke.id_ducan='{Util.Korisno.idDucan}'
                        GROUP BY roba.naziv, racun_stavke.sifra_robe
                        UNION ALL
                        SELECT SUM(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)) AS kolicina,
	                        roba.naziv,
	                        faktura_stavke.sifra AS sifra_robe
                        FROM faktura_stavke
                        LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture
                        LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra
                        WHERE fakture.date>'{OD}' {dodatan_uvjet_dopustenja} AND fakture.date<'{DO}' AND fakture.id_ducan='{Util.Korisno.idDucan}'
                        GROUP BY roba.naziv, sifra_robe
                        UNION ALL
                        SELECT SUM(otpremnica_stavke.kolicina) AS kolicina,
	                        roba.naziv,
	                        otpremnica_stavke.sifra_robe
                        FROM otpremnica_stavke
                        LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice
                        LEFT JOIN roba ON roba.sifra = otpremnica_stavke.sifra_robe
                        WHERE otpremnice.datum>'{OD}' {dodatan_uvjet_dopustenja} AND otpremnice.datum<'{DO}'
                        GROUP BY roba.naziv, otpremnica_stavke.sifra_robe";

            DataTable DTroba = classSQL.select(queryRoba, "racun_stavke").Tables[0];

            List<Roba> robaList = new List<Roba>();
            foreach (DataRow row in DTroba.Rows)
            {
                Roba roba = new Roba
                {
                    Sifra = row["sifra_robe"].ToString(),
                    Naziv = row["naziv"].ToString(),
                    Kolicina = Convert.ToDecimal(row["kolicina"].ToString())
                };

                if (robaList.Any(it => it.Sifra == roba.Sifra))
                {
                    int index = robaList.FindIndex(item => item.Sifra == roba.Sifra);
                    robaList[index].Kolicina += roba.Kolicina;
                }
                else
                    robaList.Add(roba);
            }

            // Normativi
            if (robaList.Count > 0)
            {
                List<Roba> normativiList = new List<Roba>();
                foreach (Roba roba in robaList)
                {
                    dgw.Rows.Add(roba.Sifra, roba.Naziv, roba.Kolicina.ToString("#0.000"));

                    string queryNormativi = "SELECT " +
                        " caffe_normativ.sifra_normativ," +
                        " caffe_normativ.kolicina," +
                        " roba_prodaja.naziv" +
                        " FROM caffe_normativ " +
                        " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=caffe_normativ.sifra_normativ WHERE caffe_normativ.sifra='" + roba.Sifra + "'";

                    DataTable DTnormativi = classSQL.select(queryNormativi, "normativi").Tables[0];
                    if (DTnormativi.Rows.Count > 0)
                    {
                        foreach (DataRow rowNormativ in DTnormativi.Rows)
                        {
                            Roba normativ = new Roba
                            {
                                Sifra = rowNormativ["sifra_normativ"].ToString(),
                                Naziv = rowNormativ["naziv"].ToString(),
                                Kolicina = Convert.ToDecimal(rowNormativ["kolicina"].ToString()) * roba.Kolicina
                            };

                            if (normativiList.Any(it => it.Sifra == normativ.Sifra))
                            {
                                int index = normativiList.FindIndex(item => item.Sifra == normativ.Sifra);
                                normativiList[index].Kolicina += normativ.Kolicina;
                            }
                            else
                                normativiList.Add(normativ);
                        }
                    }
                }

                if (normativiList.Count > 0)
                {
                    foreach (Roba normativ in normativiList)
                    {
                        dgwUtroseniMaterijal.Rows.Add(normativ.Sifra, normativ.Naziv, normativ.Kolicina.ToString("#0.000"));
                    }
                }
            }
        }

        private DataTable DTs = new DataTable();

        private void Stavke(string sifra, decimal kol, string naziv)
        {
            DataRow[] dataROW = DTs.Select("sifra = '" + sifra + "'");

            if (dataROW.Count() == 0)
            {
                Row = DTs.NewRow();
                Row["sifra"] = sifra;
                Row["naziv"] = naziv;
                Row["kolicina"] = kol;
                DTs.Rows.Add(Row);
            }
            else
            {
                dataROW[0]["kolicina"] = Convert.ToDecimal(dataROW[0]["kolicina"].ToString()) + kol;
            }
        }

        private void btnRobaNaSkladistu_Click(object sender, EventArgs e)
        {
            Caffe.frmRobaNaSkladistu rs = new Caffe.frmRobaNaSkladistu();
            rs.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kasa.frmSviRacuni sr = new Kasa.frmSviRacuni();
            sr.FormBorderStyle = FormBorderStyle.None;
            sr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometPoRobi prp = new Kasa.frmPrometPoRobi();
            prp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmIspisProdajnihArtiklaNaMaliPrinter snm = new frmIspisProdajnihArtiklaNaMaliPrinter();
            snm.datumOd = dtpOD.Value;
            snm.datumDo = dtpDO.Value;
            snm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmNeupjeleTransakcije nf = new Fiskalizacija.frmNeupjeleTransakcije();
            nf.ShowDialog(this);
        }

        private void btnKontaktPodrska_Click(object sender, EventArgs e)
        {
            string _path = "help1.exe";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.WorkingDirectory = _path;
            proc.StartInfo.FileName = _path;
            proc.Start();
        }

        private void btnSinkronizacija_Click(object sender, EventArgs e)
        {
            frmSinkronizacija s = new frmSinkronizacija();
            s.ShowDialog();
        }

        private void btnUnosNormativa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmKalkulacija_Click(object sender, EventArgs e)
        {
            this.Close();

            if (Until.staticVarijableFunkcije.staticKalkulacija.IsDisposed)
                Until.staticVarijableFunkcije.staticKalkulacija = new Robno.frmKalkulacija();

            Until.staticVarijableFunkcije.staticKalkulacija.WindowState = FormWindowState.Maximized;

            if (Until.staticVarijableFunkcije.staticKalkulacija.Visible)
            {
                Until.staticVarijableFunkcije.staticKalkulacija.Focus();
                Until.staticVarijableFunkcije.staticKalkulacija.BringToFront();
                Until.staticVarijableFunkcije.staticKalkulacija.TopMost = true;
            }
            else
            {
                Until.staticVarijableFunkcije.staticKalkulacija.Show();
                Until.staticVarijableFunkcije.staticKalkulacija.Focus();
                Until.staticVarijableFunkcije.staticKalkulacija.BringToFront();
                Until.staticVarijableFunkcije.staticKalkulacija.TopMost = true;
            }
        }

        private void frmPrimka_Click(object sender, EventArgs e)
        {
            this.Close();

            if (Until.staticVarijableFunkcije.staticPrimka.IsDisposed)
                Until.staticVarijableFunkcije.staticPrimka = new Robno.frmPrimka();

            Until.staticVarijableFunkcije.staticPrimka.WindowState = FormWindowState.Maximized;

            if (Until.staticVarijableFunkcije.staticPrimka.Visible)
            {
                Until.staticVarijableFunkcije.staticPrimka.Focus();
                Until.staticVarijableFunkcije.staticPrimka.BringToFront();
                Until.staticVarijableFunkcije.staticPrimka.TopMost = true;
            }
            else
            {
                Until.staticVarijableFunkcije.staticPrimka.Show();
                Until.staticVarijableFunkcije.staticPrimka.Focus();
                Until.staticVarijableFunkcije.staticPrimka.BringToFront();
                Until.staticVarijableFunkcije.staticPrimka.TopMost = true;
            }
        }

        private void frmOtpisRobe_Click(object sender, EventArgs e)
        {
            this.Close();

            if (Until.staticVarijableFunkcije.staticPovratRobe.IsDisposed)
                Until.staticVarijableFunkcije.staticPovratRobe = new Robno.frmPovratRobe();

            Until.staticVarijableFunkcije.staticPovratRobe.WindowState = FormWindowState.Maximized;

            if (Until.staticVarijableFunkcije.staticPovratRobe.Visible)
            {
                Until.staticVarijableFunkcije.staticPovratRobe.Focus();
                Until.staticVarijableFunkcije.staticPovratRobe.BringToFront();
                Until.staticVarijableFunkcije.staticPovratRobe.TopMost = true;
            }
            else
            {
                Until.staticVarijableFunkcije.staticPovratRobe.Show();
            }
        }

        private void btnInventura_Click(object sender, EventArgs e)
        {
            this.Close();

            if (Until.staticVarijableFunkcije.staticInventura.IsDisposed)
                Until.staticVarijableFunkcije.staticInventura = new frmUnosInventura();

            Until.staticVarijableFunkcije.staticInventura.WindowState = FormWindowState.Maximized;

            if (Until.staticVarijableFunkcije.staticInventura.Visible)
            {
                Until.staticVarijableFunkcije.staticInventura.Focus();
                Until.staticVarijableFunkcije.staticInventura.BringToFront();
                Until.staticVarijableFunkcije.staticInventura.TopMost = true;
            }
            else
            {
                Until.staticVarijableFunkcije.staticInventura.Show();
            }
        }

        private void btnBlagajnickiIzvjestaj_Click(object sender, EventArgs e)
        {
            frmBlagajnickiIzvjestaj bi = new frmBlagajnickiIzvjestaj();
            bi.ShowDialog();
        }

        private void btnKarticaSkl_Click(object sender, EventArgs e)
        {
            Caffe.frmKarticaSkladista ks = new frmKarticaSkladista();
            ks.ShowDialog();
        }

        private void txtMeduskl_Click(object sender, EventArgs e)
        {
            Robno.frmMeduskladisnica ms = new Robno.frmMeduskladisnica();
            ms.ShowDialog();
        }

        private void btnPotrosnaRoba_Click(object sender, EventArgs e)
        {
            frmPotrosniMaterijal pm = new frmPotrosniMaterijal();
            pm.WindowState = FormWindowState.Maximized;
            pm.ShowDialog();
        }

        private void btnKarticaKupca_Click(object sender, EventArgs e)
        {
            frmKarticaKupca frmKK = new frmKarticaKupca();
            frmKK.WindowState = FormWindowState.Maximized;
            frmKK.ShowDialog();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void btnArtikl_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajniArtikli robaUsluge = new Caffe.frmProdajniArtikli();
            robaUsluge.StartPosition = FormStartPosition.CenterParent;
            robaUsluge.ControlBox = false;
            robaUsluge.FormBorderStyle = FormBorderStyle.FixedDialog;
            robaUsluge.ShowDialog(this);
        }

        /// <summary>
        /// Clears both data grid views
        /// </summary>
        private void RefreshGrids()
        {
            dgw.Rows.Clear();
            dgw.Refresh();
            dgwUtroseniMaterijal.Rows.Clear();
            dgwUtroseniMaterijal.Refresh();
        }
    }

    public class Roba
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public decimal Kolicina { get; set; }
    }
}