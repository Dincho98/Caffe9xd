using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKarticaRobe : Form
    {
        private DataSet DSMT = new DataSet();
        private DataTable DTitems_sort = new DataTable();
        private DataTable DTRoba;

        private decimal ukupnoStanje { get; set; }

        public frmMenu MainFormMenu { get; set; }

        public frmKarticaRobe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmKartica_Load(object sender, EventArgs e)
        {
            lblLoading.Visible = true;
            fillComboBox();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            lblLoading.Visible = false;
        }

        private void fillComboBox()
        {
            DTitems_sort.Columns.Add("Šifra");
            DTitems_sort.Columns.Add("Naziv");
            DTitems_sort.Columns.Add("Količina");
            DTitems_sort.Columns.Add("JMJ");
            DTitems_sort.Columns.Add("NBC");
            DTitems_sort.Columns.Add("VPC");
            DTitems_sort.Columns.Add("Datum");
            DTitems_sort.Columns.Add("Dokumenat");

            //fill mjTroška
            DSMT = classSQL.select("SELECT * FROM skladiste", "skladiste");
            cbSkladiste.DataSource = DSMT.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillGrid()
        {

            if (dgw.Rows.Count > 0)
                RefreshGrid();

            ukupnoStanje = 0;

            if (chbPocetno.Checked)
                Pocetno();

            if (chbMaloprodaja.Checked)
                Maloprodaja();

            if (chbFaktura.Checked)
                Fakture();

            if (chbPrimka.Checked)
                Primke();

            if (chbKalkulacija.Checked)
                Kalkulacije();

            if (chbMeduskladisnice.Checked)
                Medjuskladisnica();

            if (chbOtpremnica.Checked)
                Otpremnice();

            if (chbIzdatnice.Checked)
                Izdatnice();

            if (chbOtpis.Checked)
                Otpis();

            dgw.Sort(dgw.Columns[3], System.ComponentModel.ListSortDirection.Ascending);

            SetValues();

            decimal ulaz = decimal.Parse(lblPocetnoStanje.Text) + decimal.Parse(lblPrimke.Text) + decimal.Parse(lblKalkulacije.Text);
            lblUlaz.Text = ulaz.ToString("#0.0000");

            decimal izlaz = decimal.Parse(lblMaloprodaja.Text) + decimal.Parse(lblFakture.Text) + decimal.Parse(lblOtpremnice.Text) +
                            decimal.Parse(lblIzdatnice.Text) + decimal.Parse(lblOtpis.Text) + decimal.Parse(lblMedjuskladisnice.Text);
            lblIzlaz.Text = izlaz.ToString("#0.0000");

            decimal ukupno = ulaz - izlaz;
            lblUkupno.Text = ukupno.ToString("#0.0000");

            lblLoading.Visible = false;
        }

        private void SetValues()
        {
            PocetnoStanje();
            KolKalkulacija();
            KolPrimka();
            KolMedjuskladisnica();
            KolMaloprodaja();
            KolFaktura();
            KolIzdatnice();
            KolOtpremnica();
            KolOtpis();

        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshGrid()
        {
            dgw.Rows.Clear();
            dgw.Refresh();
        }

        #region Documents




        /// <param name="isKalkulacija"></param>
        /// <returns></returns>
        private DataTable GetPrimkaOrKalkulacija(bool isKalkulacija)
        {
            DateTime date = Global.Database.GetPocetnoDate();
            string query = $@"SELECT primka.broj_primke AS sifra_dokument
                                ,roba_prodaja.sifra
	                            ,roba_prodaja.naziv
	                            ,primka.datum AS datum
	                            ,primka_stavke.kolicina AS dokument_kolicina
                            FROM primka 
                            LEFT JOIN primka_stavke ON primka_stavke.broj_primke = primka.broj_primke
                            LEFT JOIN roba_prodaja ON roba_prodaja.sifra = primka_stavke.sifra
                            WHERE roba_prodaja.sifra = '{txtSifraArtikla.Text}'
                            AND primka.datum >= '{dtpOdDatuma.Text}' 
                            AND primka.datum <= '{dtpDoDatuma.Text}'                    
                            AND primka.is_kalkulacija = {(isKalkulacija ? "TRUE" : "FALSE")} 
                            AND primka_stavke.is_kalkulacija = {(isKalkulacija ? "TRUE" : "FALSE")}
                            AND primka.id_skladiste = '{cbSkladiste.SelectedValue.ToString()}'";

            return classSQL.select(query, "primka").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        private void PocetnoStanje()
        {
            string query = $@"SELECT kolicina FROM pocetno WHERE sifra='{txtSifraArtikla.Text}' AND novo = TRUE ";
            DataTable table = classSQL.select(query, "pocetno").Tables[0];
            double ukupno = 0;
            double broj = 0;

            if (table != null)
                foreach (DataRow item in table.Rows)
                {
                    if (!(item[0] is DBNull))
                        broj = Convert.ToDouble(item[0]);
                    ukupno += broj;
                }
            lblPocetnoStanje.Text = ukupno.ToString("#0.0000");
        }

        private void KolPrimka()
        {
            string query = $@"SELECT kolicina FROM primka_stavke WHERE sifra='{txtSifraArtikla.Text}' AND is_kalkulacija = FALSE AND id_skladiste='{cbSkladiste.SelectedValue.ToString()}' ";
            DataTable table = classSQL.select(query, "primka_stavke").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }

            lblPrimke.Text = ukupno.ToString("#0.0000");
        }

        private void KolKalkulacija()
        {
            string query = $@"SELECT kolicina FROM primka_stavke WHERE sifra='{txtSifraArtikla.Text}' AND is_kalkulacija = TRUE AND id_skladiste='{cbSkladiste.SelectedValue.ToString()}' ";

            DataTable table = classSQL.select(query, "primka_stavke").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }

            lblKalkulacije.Text = ukupno.ToString("#0.0000");
        }

        private void KolMaloprodaja()
        {
            string sql = "SELECT " +
            " caffe_normativ.kolicina," +
            " racun_stavke.kolicina" +
            " FROM racun_stavke " +
            " LEFT JOIN caffe_normativ ON caffe_normativ.sifra = racun_stavke.sifra_robe" +
            " WHERE caffe_normativ.sifra_normativ='" + txtSifraArtikla.Text + "' AND racun_stavke.sifra_robe = caffe_normativ.sifra";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;
            double kolicina = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;

                if (!(item[1] is DBNull))
                    kolicina = Convert.ToDouble(item[1]);
            }

            ukupno *= kolicina;

            lblMaloprodaja.Text = ukupno.ToString("#0.0000");
        }

        private void KolFaktura()
        {
            string sql = "SELECT MIN(" +
            "COALESCE((SELECT SUM(REPLACE(caffe_normativ.kolicina, ',', '.')::NUMERIC * COALESCE((SELECT SUM(REPLACE(faktura_stavke.kolicina, ',', '.')::NUMERIC) " +
            "FROM faktura_stavke LEFT JOIN fakture ON faktura_stavke.broj_fakture = fakture.broj_fakture " +
            "WHERE faktura_stavke.sifra = caffe_normativ.sifra ), 0)) " +
            "FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = '" + txtSifraArtikla.Text + "' ),0)) AS fakture " +
            "FROM roba_prodaja LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }

            lblFakture.Text = ukupno.ToString("#0.0000");
        }

        private void KolOtpremnica()
        {
            string sql = $@"SELECT MIN(" +
            " COALESCE((SELECT SUM(REPLACE(caffe_normativ.kolicina, ',', '.')::NUMERIC * COALESCE((SELECT SUM(otpremnica_stavke.kolicina) " +
            "FROM otpremnica_stavke LEFT JOIN otpremnice on otpremnica_stavke.broj_otpremnice = otpremnice.broj_otpremnice and otpremnica_stavke.id_skladiste = otpremnice.id_skladiste " +
            " WHERE otpremnica_stavke.sifra_robe = caffe_normativ.sifra AND otpremnica_stavke.naplaceno_fakturom = FALSE), 0)) " +
            " FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = '" + txtSifraArtikla.Text + "'),0)) AS otpremnica " +
            "FROM roba_prodaja LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }

            lblOtpremnice.Text = ukupno.ToString("#0.0000");
        }

        private void KolIzdatnice()
        {
            string sql = "SELECT SUM(CAST(REPLACE(izdatnica_stavke.kolicina, ',', '.') AS decimal)) " +
                         "FROM izdatnica_stavke " +
                         "LEFT JOIN roba_prodaja ON roba_prodaja.sifra=izdatnica_stavke.sifra " +
                         "WHERE izdatnica_stavke.sifra='" + txtSifraArtikla.Text + "' AND roba_prodaja.id_skladiste = '" + cbSkladiste.SelectedValue.ToString() + "'";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }

            lblIzdatnice.Text = ukupno.ToString("#0.0000");
        }

        private void KolMedjuskladisnica()
        {
            string sql = "SELECT " +
            " caffe_normativ.kolicina" +
            " FROM meduskladisnica_stavke " +
            " LEFT JOIN caffe_normativ ON caffe_normativ.sifra=meduskladisnica_stavke.sifra" +
            " WHERE caffe_normativ.sifra_normativ='" + txtSifraArtikla.Text + "'";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }
            lblMedjuskladisnice.Text = ukupno.ToString("#0.0000");
        }
        
        private void KolOtpis()
        {
            string sql = "SELECT SUM(CAST(REPLACE(povrat_robe_stavke.kolicina, ',', '.') AS decimal)) " +
                         "FROM povrat_robe_stavke " +
                         "LEFT JOIN roba_prodaja ON roba_prodaja.sifra = povrat_robe_stavke.sifra " +
                         "LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj " +
                         "WHERE povrat_robe_stavke.sifra='" + txtSifraArtikla.Text + "' AND roba_prodaja.id_skladiste = povrat_robe.id_skladiste";

            DataTable table = classSQL.select(sql, "caffe_normativ").Tables[0];
            double ukupno = 0;
            double broj = 0;

            foreach (DataRow item in table.Rows)
            {
                if (!(item[0] is DBNull))
                    broj = Convert.ToDouble(item[0]);
                ukupno += broj;
            }
            lblOtpis.Text = ukupno.ToString("#0.0000");
        }

        private void Pocetno()
        {
            DataTable dataTable = Global.Database.GetDocument(txtSifraArtikla.Text, "pocetno", "id", "datum", "sifra");
            AddRowsToGrid(dataTable, "Početno");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Maloprodaja()
        {
            DataTable dataTable = Global.Database.GetDocumentItems(txtSifraArtikla.Text, "racuni", "broj_racuna", "racun_stavke", "broj_racuna", "datum_racuna", "sifra_robe", cbSkladiste.SelectedValue.ToString(), dtpOdDatuma.Text, dtpDoDatuma.Text);
            AddRowsToGrid(dataTable, "Maloprodaja", false);

        }

        /// <summary>
        /// 
        /// </summary>
        private void Fakture()
        {
            DataTable dataTable = Global.Database.GetDocumentItems(txtSifraArtikla.Text, "fakture", "broj_fakture", "faktura_stavke", "broj_fakture", "date", "sifra", cbSkladiste.SelectedValue.ToString(), dtpOdDatuma.Text, dtpDoDatuma.Text);
            AddRowsToGrid(dataTable, "Faktura", false);
        }

        private void Otpis()
        {
            DataTable dataTable = GetOtpis();
            AddRowsToGrid(dataTable, "Otpis");
        }

        private DataTable GetOtpis()
        {
            string query = $@"SELECT povrat_robe.broj AS sifra_dokument
                               ,roba_prodaja.sifra
	                            ,roba_prodaja.naziv
	                            ,povrat_robe.datum AS datum
	                            ,povrat_robe_stavke.kolicina AS dokument_kolicina
                            FROM povrat_robe  
                            LEFT JOIN povrat_robe_stavke ON povrat_robe_stavke.broj = povrat_robe.broj
                            LEFT JOIN roba_prodaja ON roba_prodaja.sifra = povrat_robe_stavke.sifra
                            WHERE roba_prodaja.sifra = '{txtSifraArtikla.Text}'
                            AND povrat_robe.datum >= '{dtpOdDatuma.Text}' 
                            AND povrat_robe.datum <= '{dtpDoDatuma.Text}' 
                            AND povrat_robe.id_skladiste = '{cbSkladiste.SelectedValue.ToString()}'";

            return classSQL.select(query, "povrat_robe").Tables[0];
        }

        private void Izdatnice()
        {
            DataTable dataTable = GetIzdatnica();
            AddRowsToGrid(dataTable, "Izdatnica");
        }

        private DataTable GetIzdatnica()
        {
            string query = $@"SELECT izdatnica.broj AS sifra_dokument
                               ,roba_prodaja.sifra
	                            ,roba_prodaja.naziv
	                            ,izdatnica.datum AS datum
	                            ,izdatnica_stavke.kolicina AS dokument_kolicina
                            FROM izdatnica  
                            LEFT JOIN izdatnica_stavke ON izdatnica_stavke.broj = izdatnica.broj
                            LEFT JOIN roba_prodaja ON roba_prodaja.sifra = izdatnica_stavke.sifra
                            WHERE roba_prodaja.sifra = '{txtSifraArtikla.Text}'
                            AND izdatnica.datum >= '{dtpOdDatuma.Text}' 
                            AND izdatnica.datum <= '{dtpDoDatuma.Text}' 
                            AND izdatnica.id_skladiste = '{cbSkladiste.SelectedValue.ToString()}'";

            return classSQL.select(query, "izdatnica").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        private void Primke()
        {
            DataTable dataTable = GetPrimkaOrKalkulacija(false);
            AddRowsToGrid(dataTable, "Primka");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Kalkulacije()
        {
            DataTable dataTable = GetPrimkaOrKalkulacija(true);
            AddRowsToGrid(dataTable, "Kalkulacija");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isKalkulacija"></param>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        private void Medjuskladisnica()
        {
            DataTable dataTable = Global.Database.GetDocumentItems(txtSifraArtikla.Text, "meduskladisnica", "broj", "meduskladisnica_stavke", "broj", "datum", "sifra", cbSkladiste.SelectedValue.ToString(), dtpOdDatuma.Text, dtpDoDatuma.Text);
            AddRowsToGrid(dataTable, "Međuskladišnica", false);
        }

        private void Otpremnice()
        {
            DataTable dataTable = Global.Database.GetDocumentItemsObrazac(txtSifraArtikla.Text, "otpremnice", "broj_otpremnice", "otpremnica_stavke", "broj_otpremnice", "datum", "sifra_robe", cbSkladiste.SelectedValue.ToString(), dtpOdDatuma.Text, dtpDoDatuma.Text);
            AddRowsToGrid(dataTable, "Otpremnica", false);
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="documentName"></param>

        private void AddRowsToGrid(DataTable dataTable, string documentName, bool add = true)
        {
            if (dataTable?.Rows.Count > 0)
            {

                foreach (DataRow row in dataTable.Rows)
                {

                    if (dataTable.Columns.Contains("normativ_kolicina"))
                    {
                        decimal.TryParse(row["dokument_kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dokumentKolicina);
                        decimal.TryParse(row["normativ_kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal normativKolicina);

                        decimal ukupnoKolicina = dokumentKolicina * (normativKolicina != 0 ? normativKolicina : 1);

                        int index = dgw.Rows.Add();
                        dgw.Rows[index].Cells["sifra_robe"].Value = row["sifra"].ToString();
                        dgw.Rows[index].Cells["naziv"].Value = row["naziv"].ToString();
                        dgw.Rows[index].Cells["kolicina"].Value = ukupnoKolicina.ToString("#0.0000");
                        dgw.Rows[index].Cells["datum"].Value = DateTime.Parse(row["datum"].ToString());
                        dgw.Rows[index].Cells["dokument"].Value = row["sifra_dokument"].ToString() + ". " + documentName;
                    }
                    else
                    {
                        decimal.TryParse(row["dokument_kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dokumentKolicina);

                        int index = dgw.Rows.Add();
                        dgw.Rows[index].Cells["sifra_robe"].Value = row["sifra"].ToString();
                        dgw.Rows[index].Cells["naziv"].Value = row["naziv"].ToString();
                        dgw.Rows[index].Cells["kolicina"].Value = dokumentKolicina.ToString("#0.0000");
                        dgw.Rows[index].Cells["datum"].Value = DateTime.Parse(row["datum"].ToString());
                        dgw.Rows[index].Cells["dokument"].Value = row["sifra_dokument"].ToString() + ". " + documentName;
                    }

                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void SelectRoba()
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");
            if (propertis_sifra != "")
            {
                string sql = "";
                if (cbSkladiste.SelectedValue.ToString() == "1")
                {
                    sql = "SELECT " +
                        " roba_prodaja.sifra," +
                        " roba_prodaja.naziv," +
                        " roba_prodaja.nc," +
                        " roba_prodaja.kolicina," +
                        " roba_prodaja.vpc" +
                        " FROM roba " +
                        " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                        " WHERE roba.sifra='" + propertis_sifra + "'";
                }
                else
                {
                    sql = "SELECT " +
                    " roba_prodaja.sifra," +
                    " roba_prodaja.naziv," +
                    " roba_prodaja.nc," +
                    " roba_prodaja.kolicina," +
                    " roba_prodaja.vpc" +
                    " FROM roba " +
                    " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                    " WHERE roba.sifra='" + propertis_sifra + "' AND roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "'";
                }

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                    lblStanje.Text = Convert.ToDouble(DTRoba.Rows[0]["kolicina"].ToString()).ToString("#0.0000");
                    lblNbc.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl na odabranome skladistu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(txtSifraArtikla.Text))
                FillGrid();
        }

        private void btnArtikli_Click(object sender, EventArgs e)
        {
            SelectRoba();
        }

        private void tabKartica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabKartica.SelectedIndex == 1)
            {
                string sql = "SELECT " +
                    " roba_prodaja.sifra AS [Šifra]," +
                    " skladiste.skladiste AS [Skladište]," +
                    " roba_prodaja.kolicina AS [Količina]," +
                    " roba_prodaja.nc AS [NBC]," +
                    " roba_prodaja.vpc AS [VPC]" +
                    " FROM skladiste" +
                    " INNER JOIN roba_prodaja ON roba_prodaja.id_skladiste=skladiste.id_skladiste" +
                    " WHERE roba_prodaja.sifra='" + txtSifraArtikla.Text + "'";

                //dataGridView1.DataSource = classSQL.select(sql, "skladiste").Tables[0];
            }
        }

        private void PbTrazi_Click(object sender, EventArgs e)
        {
            lblLoading.Visible = true;
            Search();
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


        private void txtSifraArtikla_TextChanged_1(object sender, EventArgs e)
        {
            string sql = "SELECT " +
                            " roba.sifra," +
                            " roba_prodaja.naziv," +
                            " roba_prodaja.nc," +
                            " roba_prodaja.kolicina," +
                            " roba_prodaja.vpc" +
                            " FROM roba " +
                            " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                            " WHERE roba_prodaja.sifra='" + txtSifraArtikla.Text + "'";

            DTRoba = classSQL.select(sql, "roba").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                //txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                lblStanje.Text = Convert.ToDouble(DTRoba.Rows[0]["kolicina"].ToString()).ToString("#0.0000");
                lblNbc.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
            }
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraArtikla.Text == "")
                    SelectRoba();
                else
                {
                    lblLoading.Visible = true;
                    Search();
                }

            }
        }


    }
}