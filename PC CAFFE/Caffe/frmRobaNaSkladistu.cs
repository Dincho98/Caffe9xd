using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmRobaNaSkladistu : Form
    {
        private string poslovnica = "1";

        public frmRobaNaSkladistu()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmRobaNaSkladistu_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            GetPoslovnica();
            SetRobaSkladiste(null, null);
        }

        private void SetRobaSkladiste(string sifra, string naziv)
        {
            DataTable DTitems_sort = new DataTable();
            DTitems_sort.Columns.Add("Šifra", typeof(string));
            DTitems_sort.Columns.Add("Naziv", typeof(string));
            DTitems_sort.Columns.Add("Količina", typeof(double));

            DataTable DT = GetRoba(sifra, naziv);
            DataRow dr;
            decimal pocetno = 0, kalk = 0, primka = 0, inventura = 0, otpis = 0, racuni = 0, meduskladisnice_ulaz = 0, meduskladisnice_izlaz = 0, otpremnica = 0, fakture = 0, izdatnica = 0;
            foreach (DataRow r in DT.Rows)
            {
                try
                {
                    decimal.TryParse(r["pocetno"].ToString(), out pocetno);
                    decimal.TryParse(r["primka"].ToString(), out primka);
                    decimal.TryParse(r["kalkulacija"].ToString(), out kalk);
                    decimal.TryParse(r["inventura"].ToString(), out inventura);
                    decimal.TryParse(r["otpis"].ToString(), out otpis);
                    decimal.TryParse(r["racuni"].ToString(), out racuni);
                    decimal.TryParse(r["fakture"].ToString(), out fakture);
                    decimal.TryParse(r["otpremnica"].ToString(), out otpremnica);
                    decimal.TryParse(r["izdatnica"].ToString(), out izdatnica);

                    decimal.TryParse(r["izlaz_ms"].ToString(), out meduskladisnice_izlaz);
                    decimal.TryParse(r["ulaz_ms"].ToString(), out meduskladisnice_ulaz);

                    dr = DTitems_sort.NewRow();
                    dr["Šifra"] = r["sifra"].ToString();
                    dr["Naziv"] = r["naziv"].ToString();
                    dr["Količina"] = Math.Round(pocetno + primka + kalk + inventura - otpis - racuni - fakture - otpremnica + (meduskladisnice_ulaz - meduskladisnice_izlaz) - izdatnica, 5).ToString("#0.000");
                    DTitems_sort.Rows.Add(dr);
                }
                catch { }
            }

            if (DTitems_sort.Rows.Count > 0)
            {
                DataView dv = DTitems_sort.DefaultView;
                dv.Sort = "Količina DESC";
                DTitems_sort = dv.ToTable();
                //dgw.AutoGenerateColumns = false;

                dgw.DataSource = DTitems_sort;
                dgw.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void txtSifra_robe_TextChanged(object sender, EventArgs e)
        {
            SetRobaSkladiste(txtSifra_robe.Text, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SetRobaSkladiste(null, textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int RecLineChars;
        private string tekst = "";
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        private void btnIspis_Click(object sender, EventArgs e)
        {
            tekst = "";
            RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());

            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            PrintTextLine("Adresa: " + DT_tvr.Rows[0]["adresa"].ToString());
            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
            PrintTextLine(new string('-', RecLineChars));

            for (int i = 0; i < dgw.RowCount; i++)
            {
                PrintText(TruncateAt(dgw.Rows[i].Cells["Šifra"].FormattedValue.ToString().PadRight(6), 6));
                PrintText(TruncateAt(dgw.Rows[i].Cells["Naziv"].FormattedValue.ToString().PadRight(24), 24));
                PrintTextLine(TruncateAt(Convert.ToDouble(dgw.Rows[i].Cells["Količina"].FormattedValue.ToString()).ToString("#0.00").PadLeft(8), 8));
            }
            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            PosPrint.classPrint.printaj(tekst);
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <param name="naziv"></param>
        private DataTable GetRoba(string sifra, string naziv)
        {
            string filter = "";
            if (classSQL.remoteConnectionString != "")
            {
                if (sifra != null)
                {
                    filter = "WHERE roba_prodaja.sifra LIKE '%" + sifra + "%'";
                }
                else if (naziv != null)
                {
                    filter = "WHERE roba_prodaja.naziv ~* '" + naziv + "'";
                }
            }
            else
            {
                if (sifra != null)
                {
                    filter = "WHERE roba_prodaja.sifra LIKE '%" + sifra + "%'";
                }
                else if (naziv != null)
                {
                    filter = "WHERE roba_prodaja.naziv LIKE '%" + naziv + "%'";
                }
            }

            string sql = string.Format(@"SELECT skladiste.skladiste, roba_prodaja.sifra, roba_prodaja.naziv,
            COALESCE(
	            (SELECT SUM(kolicina) AS kol
	            FROM pocetno
	            WHERE roba_prodaja.sifra = pocetno.sifra AND roba_prodaja.id_skladiste = pocetno.id_skladiste),
            0) AS pocetno,

            COALESCE(
	            (SELECT SUM(ch.kolicina) AS kol
	            FROM primka_stavke ch
	            LEFT JOIN primka h ON ch.broj_primke::INTEGER = h.broj_primke::INTEGER AND h.id_skladiste = ch.id_skladiste AND h.is_kalkulacija = 'FALSE'
	            WHERE roba_prodaja.sifra = ch.sifra AND ch.is_kalkulacija = 'FALSE' AND roba_prodaja.id_skladiste = ch.id_skladiste),
            0) AS primka,

            COALESCE(
	            (SELECT SUM(ch.kolicina) AS kol
	            FROM primka_stavke ch
	            LEFT JOIN primka h ON ch.broj_primke::INTEGER = h.broj_primke::INTEGER AND h.id_skladiste = ch.id_skladiste AND h.is_kalkulacija = 'TRUE'
	            WHERE roba_prodaja.sifra = ch.sifra AND ch.is_kalkulacija = 'TRUE' AND roba_prodaja.id_skladiste = ch.id_skladiste),
            0) AS kalkulacija,

            COALESCE(
	            (SELECT SUM(REPLACE(ch.kolicina, ',','.')::NUMERIC) AS izd
	            FROM izdatnica_stavke ch
	            LEFT JOIN izdatnica h ON ch.id_izdatnica = h.id_izdatnica
	            WHERE roba_prodaja.sifra = ch.sifra AND roba_prodaja.id_skladiste = h.id_skladiste),
            0) AS izdatnica,

            COALESCE(
	            (SELECT SUM(REPLACE(ch.kolicina,',','.')::NUMERIC - ch.kolicina_koja_je_bila_na_skl) AS kol
	            FROM inventura_stavke ch
	            LEFT JOIN inventura h ON h.broj_inventure = ch.broj_inventure
	            WHERE roba_prodaja.sifra = ch.sifra_robe AND h.is_pocetno_stanje = '0' AND roba_prodaja.id_skladiste = h.id_skladiste),
            0) AS inventura,

            COALESCE(
	            (SELECT SUM(REPLACE(ch.kolicina,',','.')::NUMERIC) AS kol
	            FROM povrat_robe_stavke ch
	            LEFT JOIN povrat_robe h ON ch.broj = h.broj
	            WHERE roba_prodaja.sifra = ch.sifra AND roba_prodaja.id_skladiste = h.id_skladiste),
            0) AS otpis,

            COALESCE(
	            (SELECT SUM(REPLACE(caffe_normativ.kolicina, ',', '.')::NUMERIC *
		            COALESCE(
			            (SELECT SUM(REPLACE(rs.kolicina, ',', '.')::numeric)
			            FROM racun_stavke rs
			            LEFT JOIN racuni r ON rs.broj_racuna::integer = r.broj_racuna::INTEGER AND rs.id_ducan = r.id_ducan AND rs.id_blagajna = r.id_kasa
			            WHERE rs.sifra_robe = caffe_normativ.sifra),
		            0))
	            FROM caffe_normativ
	            WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),
            0) AS racuni,

            COALESCE(
	            (SELECT SUM(REPLACE(caffe_normativ.kolicina,',','.')::NUMERIC *
		            COALESCE(
			            (SELECT SUM(REPLACE(fs.kolicina, ',', '.')::NUMERIC)
			            FROM faktura_stavke fs
			            LEFT JOIN fakture f ON fs.broj_fakture = f.broj_fakture
			            WHERE fs.sifra = caffe_normativ.sifra),
		            0))
	            FROM caffe_normativ
	            WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra AND roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),
            0) AS fakture,

            COALESCE(
	            (SELECT SUM(REPLACE(caffe_normativ.kolicina,',','.')::NUMERIC *
		            COALESCE(
			            (SELECT SUM(os.kolicina)
			            FROM otpremnica_stavke os
			            LEFT JOIN otpremnice o on os.broj_otpremnice = o.broj_otpremnice and os.id_skladiste = o.id_skladiste
			            WHERE os.sifra_robe = caffe_normativ.sifra AND os.naplaceno_fakturom = FALSE),
		            0))
	            FROM caffe_normativ
	            WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra AND roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),
            0) AS otpremnica,

            (SELECT COALESCE(SUM(ch.kolicina), 0) AS kolicina
            FROM medu_poslovnice ch
            WHERE ch.iz_poslovnice = '{0}' AND ch.sifra = roba_prodaja.sifra AND roba_prodaja.id_skladiste = ch.id_skladiste) AS izlaz_ms,

            (SELECT COALESCE(SUM(ch.kolicina), 0) AS kolicina
            FROM medu_poslovnice ch
            WHERE ch.u_poslovnicu = '{0}' AND ch.sifra = roba_prodaja.sifra AND roba_prodaja.id_skladiste = ch.id_skladiste) AS ulaz_ms,
            roba_prodaja.nc as nbc

            FROM roba_prodaja
            LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste {1}", poslovnica, filter);

            DataTable DT = classSQL.select(sql, "ks").Tables[0];

            return DT;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetPoslovnica()
        {
            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }
        }
    }
}