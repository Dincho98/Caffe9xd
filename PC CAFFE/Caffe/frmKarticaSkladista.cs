using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmKarticaSkladista : Form
    {
        private DataTable dtPodaci;
        private string poslovnica = "1";

        public frmKarticaSkladista()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;

            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }
        }

        private void frmKarticaSkladista_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            DateTime danasniDatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            dtpDoDatuma.Value = danasniDatum;

            getPodaci();
        }

        private void getPodaci()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            string query = "SELECT sifra ,naziv, coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra=pocetno.sifra),0) AS pocetno," +
                "coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='False'),0) AS primka," +
                "coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='True'),0) AS kalkulacija," +
                "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)-kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje='0'),0) as inventura," +
                "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra),0) as otpis," +
                "coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra),0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra),0) AS racuni, " +

                "(SELECT coalesce(SUM(medu_poslovnice.kolicina)) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice='" + poslovnica + "' AND medu_poslovnice.sifra=roba_prodaja.sifra) AS izlaz_ms," +
                "(SELECT coalesce(SUM(medu_poslovnice.kolicina)) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu='" + poslovnica + "' AND medu_poslovnice.sifra=roba_prodaja.sifra) AS ulaz_ms" +
                " FROM roba_prodaja ORDER BY naziv;";

            //TODO: otpremnice prikazati samo one koje nisu naplacene fakturom.
            query = string.Format(@"SELECT skladiste.skladiste, roba_prodaja.sifra, roba_prodaja.naziv,
COALESCE(
	(SELECT SUM(kolicina) AS kol
	FROM pocetno
	WHERE roba_prodaja.sifra = pocetno.sifra AND roba_prodaja.id_skladiste = pocetno.id_skladiste and pocetno.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
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
	WHERE roba_prodaja.sifra = ch.sifra AND ch.is_kalkulacija = 'TRUE' AND roba_prodaja.id_skladiste = ch.id_skladiste AND h.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
0) AS kalkulacija,

COALESCE(
	(SELECT SUM(REPLACE(ch.kolicina, ',','.')::NUMERIC) AS izd
	FROM izdatnica_stavke ch
	LEFT JOIN izdatnica h ON ch.id_izdatnica = h.id_izdatnica
	WHERE roba_prodaja.sifra = ch.sifra AND roba_prodaja.id_skladiste = h.id_skladiste AND h.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
0) AS izdatnica,

COALESCE(
	(SELECT SUM(REPLACE(ch.kolicina,',','.')::NUMERIC - ch.kolicina_koja_je_bila_na_skl) AS kol
	FROM inventura_stavke ch
	LEFT JOIN inventura h ON h.broj_inventure = ch.broj_inventure
	WHERE roba_prodaja.sifra = ch.sifra_robe AND h.is_pocetno_stanje = '0' AND roba_prodaja.id_skladiste = h.id_skladiste AND h.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
0) AS inventura,

COALESCE(
	(SELECT SUM(REPLACE(ch.kolicina,',','.')::NUMERIC) AS kol
	FROM povrat_robe_stavke ch
	LEFT JOIN povrat_robe h ON ch.broj = h.broj
	WHERE roba_prodaja.sifra = ch.sifra AND roba_prodaja.id_skladiste = h.id_skladiste and h.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
0) AS otpis,

COALESCE(
	(SELECT SUM(REPLACE(caffe_normativ.kolicina, ',', '.')::NUMERIC *
		COALESCE(
			(SELECT SUM(REPLACE(rs.kolicina, ',', '.')::numeric)
			FROM racun_stavke rs
			LEFT JOIN racuni r ON rs.broj_racuna::integer = r.broj_racuna::INTEGER AND rs.id_ducan = r.id_ducan AND rs.id_blagajna = r.id_kasa
			WHERE rs.sifra_robe = caffe_normativ.sifra AND r.datum_racuna <= '{1:yyyy-MM-dd HH:mm:ss}'),
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
			WHERE fs.sifra = caffe_normativ.sifra and f.date <= '{1:yyyy-MM-dd HH:mm:ss}'),
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
			WHERE os.sifra_robe = caffe_normativ.sifra AND os.naplaceno_fakturom = FALSE and o.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
		0))
	FROM caffe_normativ
	WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra AND roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),
0) AS otpremnica,

(SELECT COALESCE(SUM(ch.kolicina), 0) AS kolicina
FROM medu_poslovnice ch
WHERE ch.iz_poslovnice = '{0}' AND ch.sifra = roba_prodaja.sifra AND roba_prodaja.id_skladiste = ch.id_skladiste AND ch.datum <= '{1:yyyy-MM-dd HH:mm:ss}') AS izlaz_ms,

(SELECT COALESCE(SUM(ch.kolicina), 0) AS kolicina
FROM medu_poslovnice ch
WHERE ch.u_poslovnicu = '{0}' AND ch.sifra = roba_prodaja.sifra AND roba_prodaja.id_skladiste = ch.id_skladiste AND ch.datum <= '{1:yyyy-MM-dd HH:mm:ss}') AS ulaz_ms

FROM roba_prodaja
LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste", poslovnica, dtpDoDatuma.Value);

            DataTable DT = classSQL.select(query, "ks").Tables[0];

            dtPodaci = DT;

            decimal pocetno = 0, kalk = 0, primka = 0, inventura = 0, otpis = 0, racuni = 0, meduskladisnice_ulaz = 0, meduskladisnice_izlaz = 0, otpremnica = 0, fakture = 0, izdatnica = 0;

            foreach (DataRow r in DT.Rows)
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

                dgv.Rows.Add(r["sifra"].ToString(),
                    r["naziv"].ToString(),
                    Math.Round(pocetno, 5).ToString("#0.0000"),
                     Math.Round(primka, 5).ToString("#0.0000"),
                      Math.Round(kalk, 5).ToString("#0.0000"),
                      Math.Round(izdatnica, 5).ToString("#0.0000"),
                       Math.Round(inventura, 5).ToString("#0.0000"),
                        Math.Round(otpis, 5).ToString("#0.0000"),
                         Math.Round(racuni, 5).ToString("#0.0000"),
                         Math.Round(fakture, 5).ToString("#0.0000"),
                         Math.Round(otpremnica, 5).ToString("#0.0000"),
                         Math.Round(meduskladisnice_ulaz - meduskladisnice_izlaz, 5).ToString("#0.0000"),
                          Math.Round(pocetno + primka + kalk + inventura - otpis - racuni - fakture - otpremnica + (meduskladisnice_ulaz - meduskladisnice_izlaz) - izdatnica, 5).ToString("#0.0000")
                    );
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            getPodaci();
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.KarticaSkladista.frmKarticaSkladista2 f = new Report.KarticaSkladista.frmKarticaSkladista2();
            f.dtPodaci = dtPodaci;
            f.dtDatum = dtpDoDatuma.Value;
            f.ShowDialog();
        }
    }
}