using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Ponude.Forme
{
    public partial class frmSvePonude : Form
    {
        public frmPonude MainForm { get; set; }
        public string sifra_fakture;

        public frmSvePonude()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
        //public frmMenu MainFormMenu { get; set; }

        private void frmSveFakture_Load(object sender, EventArgs e)
        {
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
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
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

            string sql = "SELECT " + top + " Rponude.broj AS [Broj ponude], Rponude.datum AS [Datum],Rponude.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS [Partner],Rponude.ukupno as Ukupno " +
                " FROM Rponude INNER JOIN valute ON Rponude.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON Rponude.id_partner = partners.id_partner ORDER BY CAST(Rponude.broj AS integer) DESC" +
                "" + remote + "";

            DSfakture = RemoteDB.select(sql, "rfakture");
            dgv.DataSource = DSfakture.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj ponude"].Value.ToString();

                if (this.MainForm == null)
                {
                    frmPonude childForm = new frmPonude();
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
                Broj = "Rponude.broj='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "Rponude.id_partner='" + txtPartner.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "Rponude.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "Rponude.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "Rponude.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "Rponude.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT DISTINCT " + top + " Rponude.broj AS [Broj fakture], Rponude.datum AS Datum,Rponude.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                    ",partners.ime_tvrtke AS [Partner],Rponude.ukupno as Ukupno " +
                    " FROM Rponude INNER JOIN valute ON Rponude.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON Rponude.id_partner = partners.id_partner " + filter + " ORDER BY Rponude.datum DESC" + remote;

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
            izvjestaji.Faktura.repFaktura rfak = new izvjestaji.Faktura.repFaktura();
            rfak.dokumenat = "PON";
            rfak.ImeForme = "Ponuda";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            izvjestaji.Faktura.repFaktura rfak = new izvjestaji.Faktura.repFaktura();
            rfak.dokumenat = "PON";
            rfak.ImeForme = "Ponuda";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSveFakture_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}