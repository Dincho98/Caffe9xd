using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKarticaSkladiste : Form
    {
        private DataSet DSvd = new DataSet();
        private DataSet DSpartner = new DataSet();
        private DataSet DSMT = new DataSet();
        private DataSet DSroba = new DataSet();
        public frmMenu MainFormMenu { get; set; }
        private string poslovnica = "1";
        private string sort;
        private string datumDo;

        public frmKarticaSkladiste()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmKarticaSkladiste_Load(object sender, EventArgs e)
        {
            DateTime danasnjiDatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            dtpDoDatuma.Value = danasnjiDatum;
            datumDo = danasnjiDatum.ToString();

            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            PaintRows(dgw);
            cbSkl.Select();
            fillComboBox();
            //fillDataGrid();

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void fillComboBox()
        {
            //fill vrsta grupa
            DSvd = classSQL.select("SELECT * FROM grupa ORDER BY grupa ASC", "grupa");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "grupa";
            cbVD.ValueMember = "id_grupa";

            //fill skladiste
            DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste");
            cbSkl.DataSource = DSMT.Tables[0];
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";

            //fill partner
            DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste");
            cbSkl.DataSource = DSMT.Tables[0];
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 22;
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void cbSkl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbVD.Select();
            }
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraDob.Select();
            }
        }

        private void txtImeArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraDob.Select();
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraArtikla.Select();
            }
        }

        private void txtSifraArtikla_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraArtikla.Text != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT sifra,naziv FROM roba_prodaja WHERE sifra ='" + txtSifraArtikla.Text + "'", "roba");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraArtikla.Text = partner.Tables[0].Rows[0]["sifra"].ToString();
                        txtImeArtikla.Text = partner.Tables[0].Rows[0]["naziv"].ToString();
                        txtSifraDob.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void txtImeArtikla_KeyDown_1(object sender, KeyEventArgs e)
        {
            txtSifraDob.Select();
        }

        private void dtpDatumDO_KeyDown(object sender, KeyEventArgs e)
        {
            txtSifraDob.Select();
        }

        private void fillDataGrid()
        {
            RefreshGrid();

            string filter = "";

            if (chbSkladiste.Checked)
                filter += "roba_prodaja.id_skladiste='" + cbSkl.SelectedValue + "' AND ";

            if (chbVD.Checked)
                filter += "roba_prodaja.id_grupa='" + cbVD.SelectedValue + "' AND ";

            if (chbSifraArtikla.Checked)
                filter += "roba_prodaja.sifra='" + txtSifraArtikla.Text + "' AND ";

            if (chbDobavljac.Checked)
                filter += "roba_prodaja.id_partner='" + txtSifraDob.Text + "' AND ";

            if (cbPice.Checked)
                filter += "roba_prodaja.id_podgrupa = 1" + " OR ";

            if (cbHrana.Checked)
                filter += "roba_prodaja.id_podgrupa = 2" + " OR ";

            if (cbTrgovackaRoba.Checked)
                filter += "roba_prodaja.id_podgrupa = 3" + " OR ";

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = "LIMIT 200";
            }
            else
            {
                top = " TOP(200) ";
            }

            string mpcTwo = "";
            if (!cbPice.Checked && !cbHrana.Checked && cbTrgovackaRoba.Checked)
            {
                dgw.Columns["mpc"].Visible = true;
                mpcTwo = "roba_prodaja.mpc,";
            }
            else
                dgw.Columns["mpc"].Visible = false;

            string sql = "SELECT " + top + " roba_prodaja.sifra AS [Šifra], roba_prodaja.naziv AS [Naziv], roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište],roba_prodaja.nc AS [Nabavna cijena] FROM roba_prodaja " +
                " INNER JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste Order by kolicina ASC" +
                "" + remote + "";

            sql = string.Format(@"SELECT skladiste.skladiste, roba_prodaja.sifra, roba_prodaja.naziv, {3}
COALESCE(
	(SELECT SUM(kolicina) AS kol
	FROM pocetno
	WHERE roba_prodaja.sifra = pocetno.sifra AND roba_prodaja.id_skladiste = pocetno.id_skladiste and pocetno.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
0) AS pocetno,

COALESCE(
	(SELECT SUM(ch.kolicina) AS kol
	FROM primka_stavke ch
	LEFT JOIN primka h ON ch.broj_primke::INTEGER = h.broj_primke::INTEGER AND h.id_skladiste = ch.id_skladiste AND h.is_kalkulacija = 'FALSE'
	WHERE roba_prodaja.sifra = ch.sifra AND ch.is_kalkulacija = 'FALSE' AND roba_prodaja.id_skladiste = ch.id_skladiste AND h.datum <= '{1:yyyy-MM-dd HH:mm:ss}'),
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
WHERE ch.u_poslovnicu = '{0}' AND ch.sifra = roba_prodaja.sifra AND roba_prodaja.id_skladiste = ch.id_skladiste AND ch.datum <= '{1:yyyy-MM-dd HH:mm:ss}') AS ulaz_ms,
roba_prodaja.nc as nbc

FROM roba_prodaja
LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste {2}", poslovnica, datumDo, filter, mpcTwo);

            DSroba = classSQL.select(sql, "ks");

            decimal pocetno = 0, kalk = 0, primka = 0, inventura = 0, otpis = 0, racuni = 0, meduskladisnice_ulaz = 0, meduskladisnice_izlaz = 0, otpremnica = 0, fakture = 0, izdatnica = 0;

            foreach (DataRow r in DSroba.Tables[0].Rows)
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

                dgw.Rows.Add(r["sifra"].ToString(),
                    r["naziv"].ToString(),
                    Math.Round(pocetno + primka + kalk + inventura - otpis - racuni - fakture - otpremnica + (meduskladisnice_ulaz - meduskladisnice_izlaz) - izdatnica, 5).ToString("#0.0000"),
                    r["nbc"].ToString(),
                    (!cbPice.Checked && !cbHrana.Checked && cbTrgovackaRoba.Checked) ? r["mpc"].ToString() : "",
                    r["skladiste"].ToString()
                );
            }
        }

        private void DataGrigBackGround(DataGridView Ddgv)
        {
            int b = 0;
            for (int i = 0; i < Ddgv.RowCount; i++)
            {
                if (b == 0)
                {
                    Ddgv.Rows[i].Cells[1].Style.BackColor = Color.White;
                    b = 1;
                }
                else
                {
                    Ddgv.Rows[i].Cells[1].Style.BackColor = Color.AliceBlue;
                    b = 0;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRobaTrazi robaTrazi = new frmRobaTrazi();
            robaTrazi.ShowDialog();
            if (Properties.Settings.Default.id_roba != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT sifra,naziv FROM roba_prodaja WHERE sifra ='" + Properties.Settings.Default.id_roba + "'", "roba");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraArtikla.Text = partner.Tables[0].Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = partner.Tables[0].Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnArtikli_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivDob.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraDob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraDob.Text != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + txtSifraDob.Text + "'", "partners");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                        txtNazivDob.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKarticaSkladiste_Activated(object sender, EventArgs e)
        {
            PaintRows(dgw);
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

        private void btnIspis_Click(object sender, EventArgs e)
        {
            string filter = string.Format("Do datuma: {0:dd.MM.yyyy HH:mm:ss}", dtpDoDatuma.Value);
            string sortedId = "0";
            if (dgw.SortedColumn != null)
                sortedId = dgw.SortedColumn.Index.ToString();

            if (chbSkladiste.Checked)
            {
                if (filter.Length > 0)
                {
                    filter += Environment.NewLine;
                }
                filter += "Skladište: " + cbSkl.Text;
            }

            if (chbVD.Checked)
            {
                if (filter.Length > 0)
                {
                    filter += Environment.NewLine;
                }
                filter += "Grupa: " + cbVD.Text;
            }

            if (chbSifraArtikla.Checked)
            {
                if (filter.Length > 0)
                {
                    //filter += ", ";
                    filter += Environment.NewLine;
                }
                //filter += "Artikl: " + "[" + txtSifraArtikla.Text + "] " + txtImeArtikla.Text;
                filter += "Artikl: " + txtSifraArtikla.Text + " - " + txtImeArtikla.Text;
            }

            if (chbDobavljac.Checked)
            {
                if (filter.Length > 0)
                {
                    //filter += ", ";
                    filter += Environment.NewLine;
                }
                //filter += "Dobavljač: " + "[" + txtSifraDob.Text + "] " + txtNazivDob.Text;
                filter += "Dobavljač: " + txtSifraDob.Text + " - " + txtNazivDob.Text;
            }

            //if (filter.Length > 0) {
            //    filter = "FILTER" + Environment.NewLine + filter;
            //}

            Report.KarticaSkladista.frmKarticaSkladista f = new Report.KarticaSkladista.frmKarticaSkladista();
            f.DSroba = DSroba;
            f.filter = filter;
            f.sortId = sortedId;
            f.ShowDialog();
        }

        private void dtpDoDatuma_KeyUp(object sender, KeyEventArgs e)
        {
            datumDo = dtpDoDatuma.Value.ToString("yyyy-MM-dd 23:59:59");
            dtpDoDatuma.Value = DateTime.Parse(datumDo);
        }

        /// <summary>
        /// Method used to refresh items in DataGridView
        /// </summary>
        private void RefreshGrid()
        {
            dgw.Rows.Clear();
            dgw.Refresh();
        }
    }
}