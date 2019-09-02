using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveKalkulacije : Form
    {
        public frmSveKalkulacije()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmMenu MainFormMenu { get; set; }
        public Robno.frmKalkulacija MainForm { get; set; }

        private DataTable DTodjave;

        private void frmSvePrimke_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 465, 5);

            fillCB();
            fillDataGrid();
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
            row.Height = 20;
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " primka.broj_primke AS [Broj], primka.datum AS [Datum],primka.iznos AS [Iznos]," +
                " partners.ime_tvrtke AS [Partner],skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
                " FROM primka LEFT JOIN skladiste ON skladiste.id_skladiste = primka.id_skladiste " +
                " LEFT JOIN partners ON primka.id_partner = partners.id_partner WHERE is_kalkulacija='1' ORDER BY CAST(primka.broj_primke AS integer) DESC" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;
            dgv.Columns["id_skladiste"].Visible = false;

            PaintRows(dgv);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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
                Broj = " AND primka.broj_primke='" + txtBroj.Text + "'";
            }
            if (chbSifra.Checked)
            {
                Partner = " AND primka.id_partner='" + txtPartner.Text + "'";
            }

            if (chbOD.Checked)
            {
                DateStart = " AND primka.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd") + "'";
            }
            if (chbDO.Checked)
            {
                DateEnd = " AND primka.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = " AND primka_stavke.sifra ='" + cbArtikl.Text + "'";
            }
            if (chbIzradio.Checked)
            {
                Izradio = " AND primka.id_zaposlenik='" + cbIzradio.SelectedValue + "'";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = "SELECT  DISTINCT " + top + " primka.broj_primke AS [Broj], primka.datum AS [Datum],primka.iznos AS [Iznos]," +
                " partners.ime_tvrtke AS [Partner],skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
                " FROM primka LEFT JOIN skladiste ON skladiste.id_skladiste = primka.id_skladiste " +
                " LEFT JOIN primka_stavke ON primka.broj_primke = primka_stavke.broj_primke " +
                " LEFT JOIN partners ON primka.id_partner = partners.id_partner WHERE primka_stavke.is_kalkulacija='1' " + filter + "" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;
            dgv.Columns["id_skladiste"].Visible = false;

            PaintRows(dgv);
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

        private DataSet DS_Zaposlenik;

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();
            string skladiste = dgv.CurrentRow.Cells["id_skladiste"].Value.ToString();
            if (Until.classZakljucavanjeDokumenta.isLockKalkulacija(Convert.ToInt32(broj), Convert.ToInt32(skladiste)))
            {
                MessageBox.Show("Kalkulacija je zaključana. Uređivanje nije dopušteno.");
                return;
            }
            if (this.MdiParent != null)
            {
                var mdiForm = this.MdiParent;
                mdiForm.IsMdiContainer = true;
                Robno.frmKalkulacija childForm = new Robno.frmKalkulacija();
                childForm.MainForm = MainFormMenu;
                childForm.broj_primke_edit = broj;
                childForm.skladiste_edit = skladiste;
                childForm.MdiParent = mdiForm;
                childForm.Dock = DockStyle.Fill;
                childForm.Show();
            }
            else
            {
                MainForm.broj_primke_edit = broj;
                MainForm.skladiste_edit = skladiste;
                MainForm.Show();
                this.Close();
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.kalkulacija_caffe.frmKalkulacijaIspis rfak = new Report.kalkulacija_caffe.frmKalkulacijaIspis();
            rfak.dokumenat = "primka";
            rfak.ImeForme = "Primka";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.kalkulacija_caffe.frmKalkulacijaIspis rfak = new Report.kalkulacija_caffe.frmKalkulacijaIspis();
            rfak.dokumenat = "primka";
            rfak.ImeForme = "Primka";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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