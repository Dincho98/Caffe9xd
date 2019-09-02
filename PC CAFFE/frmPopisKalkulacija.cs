using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPopisKalkulacija : Form
    {
        public frmMenu MainFormMenu { get; set; }
        public frmNovaKalkulacija MainForm { get; set; }
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSskladiste = new DataSet();

        public frmPopisKalkulacija()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPopisKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            PaintRows(dataGridView1);
            fillCB();
            Fill();
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

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void fillCB()
        {
            //DS skladiste
            DSskladiste = classSQL.select("SELECT * FROM skladiste ORDER BY skladiste", "skladiste");
            cbSkladiste.DataSource = DSskladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='kal' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void Fill()
        {
            string compact = "", remote = "";
            if (classSQL.remoteConnectionString == "")
            {
                compact = " TOP (100) ";
            }
            else
            {
                remote = " LIMIT 100 ";
            }

            string sql = "SELECT " + compact + " kalkulacija.broj as [Broj],partners.ime_tvrtke AS [Dobavljač]," +
                "kalkulacija.datum AS [Datum],skladiste.skladiste AS [Skladište],kalkulacija.racun AS [Račun],kalkulacija.ukupno_vpc AS [Ukupno VPC],kalkulacija.ukupno_mpc AS [UKUPNO MPC],fakturni_iznos AS [Fakturni iznos],kalkulacija.id_skladiste AS [id_skladiste] FROM kalkulacija" +
                " LEFT JOIN partners ON kalkulacija.id_partner = partners.id_partner" +
                " LEFT JOIN skladiste ON kalkulacija.id_skladiste = skladiste.id_skladiste ORDER BY CAST(kalkulacija.broj AS integer) DESC " + remote + "";

            dataGridView1.DataSource = classSQL.select(sql, "kalkulacija").Tables[0];

            SetDecimalInDgv(dataGridView1, "Ukupno VPC", "UKUPNO MPC", "Fakturni iznos");
            PaintRows(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dataGridView1.CurrentRow.Cells[0].FormattedValue.ToString();
                string broj = dataGridView1.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                string skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                if (Until.classZakljucavanjeDokumenta.isLockKalkulacija(Convert.ToInt32(broj), Convert.ToInt32(skladiste)))
                {
                    MessageBox.Show("Kalkulacija je zaključana. Uređivanje nije dopušteno.");
                    return;
                }

                if (this.MdiParent == null)
                {
                    MainForm.broj_kalkulacije_edit = id;
                    MainForm.edit_skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                    this.Close();
                }
                else
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmNovaKalkulacija();
                    childForm.broj_kalkulacije_edit = id;
                    childForm.edit_skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            string Skladiste = "";

            if (chbBroj.Checked)
            {
                Broj = "kalkulacija.broj='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "kalkulacija.id_partner='" + txtPartner.Text + "' AND ";
            }
            //if (chbVD.Checked)
            //{
            //    VD = "kalkulacija.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            //}
            if (chbOD.Checked)
            {
                DateStart = "kalkulacija.date >='" + dtOd + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "kalkulacija.date <='" + dtDo + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "kalkulacija.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "kalkulacija_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "kalkulacija.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbSkladiste.Checked)
            {
                Skladiste = "kalkulacija.id_skladiste='" + cbSkladiste.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio + Skladiste;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string compact = "", remote = "";
            if (classSQL.remoteConnectionString == "")
            {
                compact = " TOP (100) ";
            }
            else
            {
                remote = " LIMIT 100 ";
            }

            string sql = "SELECT DISTINCT " + compact + " kalkulacija_stavke.broj as Broj, kalkulacija.id_kalkulacija as ID,partners.ime_tvrtke AS Dobavljač," +
                "kalkulacija.datum AS Datum,skladiste.skladiste AS Skladište,kalkulacija.racun AS [Račun],kalkulacija.ukupno_vpc AS [Ukupno VPC],kalkulacija.ukupno_mpc AS [UKUPNO MPC],fakturni_iznos AS [Fakturni iznos],kalkulacija.id_skladiste AS [id_skladiste] FROM kalkulacija" +
                " LEFT JOIN partners ON kalkulacija.id_partner = partners.id_partner" +
                " LEFT JOIN skladiste ON kalkulacija.id_skladiste = skladiste.id_skladiste INNER JOIN kalkulacija_stavke ON kalkulacija.broj = kalkulacija_stavke.broj" + filter + " ORDER BY kalkulacija.datum DESC " + remote;

            dataGridView1.DataSource = classSQL.select(sql, "kalkulacija").Tables[0];
            PaintRows(dataGridView1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                kalk.broj_kalkulacije = dataGridView1.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                kalk.ShowDialog();
            }
        }

        private void frmPopisKalkulacija_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainFormMenu != null)
            {
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                kalk.broj_kalkulacije = dataGridView1.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                kalk.ShowDialog();
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
    }
}