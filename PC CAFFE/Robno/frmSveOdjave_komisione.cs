using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveOdjave_komisione : Form
    {
        public frmSveOdjave_komisione()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmMenu MainFormMenu { get; set; }
        public Robno.frmOdjavaKomisione MainForm { get; set; }

        private DataTable DTodjave;

        private void frmSveOdjave_komisione_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

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
            row.Height = 22;
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

            string sql = "SELECT " + top + " odjava_komisione.broj AS [Broj], odjava_komisione.datum AS Datum,odjava_komisione.od_datuma AS [Od datuma],odjava_komisione.do_datuma AS [Do datuma]" +
                ",partners.ime_tvrtke AS Partner,skladiste.skladiste AS [Skladište]" +
                " FROM odjava_komisione LEFT JOIN skladiste ON skladiste.id_skladiste = odjava_komisione.id_skladiste " +
                " LEFT JOIN partners ON odjava_komisione.id_partner = partners.id_partner ORDER BY CAST(odjava_komisione.broj AS integer) DESC" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;

            PaintRows(dgv);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                Broj = "odjava_komisione.broj='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "odjava_komisione.id_partner='" + txtPartner.Text + "' AND ";
            }

            if (chbOD.Checked)
            {
                DateStart = "odjava_komisione.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "odjava_komisione.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "odjava_komisione_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "odjava_komisione.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT  DISTINCT " + top + " odjava_komisione.broj AS [Broj], odjava_komisione.datum AS Datum,odjava_komisione.od_datuma AS [Od datuma],odjava_komisione.do_datuma AS [Do datuma]" +
                ",partners.ime_tvrtke AS Partner,skladiste.skladiste AS [Skladište]" +
                " FROM odjava_komisione LEFT JOIN skladiste ON skladiste.id_skladiste = odjava_komisione.id_skladiste " +
                " LEFT JOIN odjava_komisione_stavke ON odjava_komisione_stavke.broj = odjava_komisione.broj" +
                " LEFT JOIN partners ON odjava_komisione.id_partner = partners.id_partner " + filter + " ORDER BY odjava_komisione.datum DESC" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;

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

            if (this.MdiParent != null)
            {
                var mdiForm = this.MdiParent;
                mdiForm.IsMdiContainer = true;
                Robno.frmOdjavaKomisione childForm = new Robno.frmOdjavaKomisione();
                childForm.MainForm = MainFormMenu;
                childForm.broj_komisione_edit = broj;
                childForm.MdiParent = mdiForm;
                childForm.Dock = DockStyle.Fill;
                childForm.Show();
            }
            else
            {
                MainForm.broj_komisione_edit = broj;
                MainForm.Show();
                this.Close();
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Odjava.frmOdjava rfak = new Report.Odjava.frmOdjava();
            rfak.dokumenat = "odjava";
            rfak.ImeForme = "Odjava robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.Odjava.frmOdjava rfak = new Report.Odjava.frmOdjava();
            rfak.dokumenat = "odjava";
            rfak.ImeForme = "Odjava robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
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