using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSavPotrosniMaterijal : Form
    {
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        public frmMenu MainFormMenu { get; set; }
        public frmPonude MainForm { get; set; }
        public string sifra_ponude;

        public frmSavPotrosniMaterijal()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSvePonude_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            fillCB();
            fillDataGrid();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

        private void fillCB()
        {
            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='pon' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private DataSet DSfakture = new DataSet();

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

            string sql = "SELECT " + top + " ponude.broj_ponude AS [Broj ponude],ponude.id_vd AS VD, ponude.date AS Datum,ponude.vrijedi_do as [Vrijedi do],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS Partner,ponude.ukupno as Ukupno " +
                " FROM ponude INNER JOIN valute ON ponude.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON ponude.id_fakturirati = partners.id_partner  ORDER BY CAST(ponude.broj_ponude AS integer)  DESC " +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            PaintRows(dgv);
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj ponude"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmPonude();
                    childForm.broj_ponude_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else if (this.MainFormMenu != null)
                {
                    frmPonude childForm = new frmPonude();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_ponude_edit = broj;
                    childForm.MdiParent = MainFormMenu;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                    this.Close();
                }
                else
                {
                    MainForm.broj_ponude_edit = broj;
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

            if (chbBroj.Checked)
            {
                Broj = "ponude.broj_ponude='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "ponude.id_fakturirati='" + txtPartner.Text + "' AND ";
            }
            if (chbVD.Checked)
            {
                VD = "ponude.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "ponude.date >='" + dtOd + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "ponude.date <='" + dtDo + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "ponude.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "ponude_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "ponude.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT DISTINCT " + top + " ponude_stavke.broj_ponude AS [Broj ponude],ponude.id_vd AS VD, ponude.date AS Datum,ponude.vrijedi_do as [Vrijedi do],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS Partner,ponude.ukupno as Ukupno " +
                " FROM ponude INNER JOIN valute ON ponude.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON ponude.id_fakturirati = partners.id_partner INNER JOIN ponude_stavke ON ponude.broj_ponude = ponude_stavke.broj_ponude" + filter + " ORDER BY ponude.date DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            PaintRows(dgv);
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void frmSvePonude_Activated(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "PON";
            rfak.ImeForme = "Ponude";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "PON";
            rfak.ImeForme = "Ponude";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
            rfak.ShowDialog();
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