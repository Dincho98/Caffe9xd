using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveFakture : Form
    {
        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
        public frmMenu MainFormMenu { get; set; }
        public frmFaktura MainForm { get; set; }
        public string sifra_fakture;

        public frmSveFakture()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSveFakture_Load(object sender, EventArgs e)
        {
            if (MainFormMenu == null)
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 60;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }
            else
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 140;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }

            if (MainForm != null)
            {
                MainForm.broj_fakture_edit = null;
            }

            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            if (DTpostavke.Rows[0]["on_off_postotak"].ToString() == "NE")
            {
                txtIspisBona.Visible = false;
            }

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            string sql = "SELECT " + top + " fakture.broj_fakture AS [Broj fakture],fakture.id_vd AS VD, fakture.date AS [Datum],fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS Partner,fakture.ukupno as Ukupno " +
                " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON fakture.id_fakturirati = partners.id_partner ORDER BY CAST(fakture.broj_fakture AS integer) DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj fakture"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    frmFaktura childForm = new frmFaktura();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_fakture_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else if (this.MainFormMenu != null)
                {
                    frmFaktura childForm = new frmFaktura();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_fakture_edit = broj;
                    childForm.MdiParent = MainFormMenu;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                    this.Close();
                }
                else
                {
                    MainForm.broj_fakture_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";
            int i = 0;

            if (chbBroj.Checked)
            {
                if (txtBroj.Text.Length > 0 && Int32.TryParse(txtBroj.Text, out i))
                {
                    Broj = "fakture.broj_fakture='" + i + "' AND ";
                }
                else
                {
                    MessageBox.Show("Krivi unos za broj fakture.");
                    return;
                }
            }
            if (chbSifra.Checked)
            {
                if (txtPartner.Text.Length > 0 && Int32.TryParse(txtPartner.Text, out i))
                {
                    Partner = "fakture.id_fakturirati='" + i + "' AND ";
                }
                else
                {
                    MessageBox.Show("Krivi unos za šifru partnera.");
                    return;
                }
            }
            if (chbVD.Checked)
            {
                VD = "fakture.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "fakture.date >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "fakture.date <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "fakture.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                if (cbArtikl.Text.Length > 0)
                {
                    SifraArtikla = "faktura_stavke.sifra ='" + cbArtikl.Text + "' AND ";
                }
                else
                {
                    MessageBox.Show("Krivi unos za šifru artikla.");
                    return;
                }
            }
            if (chbIzradio.Checked)
            {
                Izradio = "fakture.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            string sql = "SELECT DISTINCT " + top + " faktura_stavke.broj_fakture AS [Broj fakture],fakture.id_vd AS VD, fakture.date AS Datum,fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                    ",partners.ime_tvrtke AS Partner,fakture.ukupno as Ukupno " +
                    " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON fakture.id_fakturirati = partners.id_partner INNER JOIN faktura_stavke ON faktura_stavke.broj_fakture = fakture.broj_fakture" + filter + " ORDER BY fakture.date DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            PaintRows(dgv);
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "FAK";
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "FAK";
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIspisBona_Click(object sender, EventArgs e)
        {
            frmBarCode bc = new frmBarCode();
            bc.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            bc.ukupno = Convert.ToDouble(dgv.CurrentRow.Cells["Ukupno"].FormattedValue.ToString());
            bc.ShowDialog();
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
    }
}