using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmSveFakture : Form
    {
        public frmFaktura MainForm { get; set; }
        public string sifra_fakture;

        public frmSveFakture()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;
        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
        //public frmMenu MainFormMenu { get; set; }

        private void frmSveFakture_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            //if (MainFormMenu == null)
            //{
            //    int heigt = SystemInformation.VirtualScreen.Height;
            //    this.Height = heigt - 60;
            //    this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            //}
            //else
            //{
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 140;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            //}

            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

            //if (DTpostavke.Rows[0]["on_off_postotak"].ToString() == "NE")
            //{
            //    txtIspisBona.Visible = false;
            //}

            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
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
            try
            {
                //DataTable DT = RemoteDB.select("SELECT SUM(rfaktura_stavke.ukupno) FROM rfaktura_stavke", "rfakture").Tables[0];
                DataTable DT = RemoteDB.select("SELECT coalesce(SUM(rfaktura_stavke.ukupno), 0) FROM rfaktura_stavke", "rfakture").Tables[0];
                label6.Text = String.Format("{0:n}", Convert.ToDecimal(DT.Rows[0][0].ToString())) + " kn";
            }
            catch { }

            string top = "";
            string remote = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 1000";
            }
            else
            {
                top = " TOP(1000) ";
            }

            string sql = "SELECT " + top + " rfakture.broj AS [Broj fakture], rfakture.datum AS [Datum],rfakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS [Partner],rfakture.ukupno as [Ukupno],rfakture.storno" +
                " FROM rfakture INNER JOIN valute ON rfakture.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON rfakture.id_partner = partners.id_partner ORDER BY CAST(rfakture.broj AS integer) DESC" +
                "" + remote + "";

            DSfakture = RemoteDB.select(sql, "rfakture");
            dgv.DataSource = DSfakture.Tables[0];

            dgv.Columns["storno"].Visible = false;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj fakture"].Value.ToString();

                if (this.MainForm == null)
                {
                    frmFaktura childForm = new frmFaktura();
                    childForm.broj_fakture_edit = broj;
                    childForm.ShowDialog();
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

            if (chbBroj.Checked)
            {
                Broj = "rfakture.broj='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "rfakture.id_partner='" + txtPartner.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "rfakture.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "rfakture.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "rfakture.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "rfakture.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 1000";
            }
            else
            {
                top = " TOP(1000) ";
            }

            string sql = "SELECT DISTINCT " + top + " rfakture.broj AS [Broj fakture], rfakture.datum AS Datum,rfakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                    ",partners.ime_tvrtke AS [Partner],rfakture.ukupno as [Ukupno],rfakture.storno " +
                    " FROM rfakture INNER JOIN valute ON rfakture.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON rfakture.id_partner = partners.id_partner " + filter + " ORDER BY rfakture.datum DESC" + remote;

            dgv.Columns["storno"].Visible = false;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            DSfakture = RemoteDB.select(sql, "rfakture");
            dgv.DataSource = DSfakture.Tables[0];
            PaintRows(dgv);
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = RemoteDB.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill komercijalist
            DS_Zaposlenik = RemoteDB.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RESORT.izvjestaji.Faktura.repFaktura rfak = new RESORT.izvjestaji.Faktura.repFaktura();
            rfak.dokumenat = "FAK";
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            RESORT.izvjestaji.Faktura.repFaktura rfak = new RESORT.izvjestaji.Faktura.repFaktura();
            rfak.dokumenat = "FAK";
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSveFakture_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sifarnici.frmPartnerTrazi partnerTrazi = new sifarnici.frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (RESORT.Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = RemoteDB.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + RESORT.Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtPartner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}