using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAktivnostiZaposlenika : Form
    {
        public frmAktivnostiZaposlenika()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();

        private void frmAktivnostiZaposlenika_Load(object sender, EventArgs e)
        {
            fillCB();
            fillDataGrid();
            PaintRows(dgv);
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

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    //Graphics c = e.Graphics;
        //    //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    //c.FillRectangle(bG, 0, 0, Width, Height);
        //}
        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 200";
            }
            else
            {
                top = " TOP(200) ";
            }

            string sql = "SELECT " + top + " zaposlenici.ime + '' + zaposlenici.prezime AS Zaposlenik,aktivnost_zaposlenici.datum AS Datum,aktivnost_zaposlenici.radnja AS Radnja" +
                " FROM aktivnost_zaposlenici" +
                " INNER JOIN zaposlenici ON aktivnost_zaposlenici.id_zaposlenik = zaposlenici.id_zaposlenik ORDER BY DATUM DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
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
            string Uvijet = "";

            if (chbOD.Checked)
            {
                DateStart = "aktivnost_zaposlenici.datum >='" + dtOd + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "aktivnost_zaposlenici.datum <='" + dtDo + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "zaposlenici.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
            }

            if (txtUvijet.Text.Length > 0)
            {
                Uvijet = " aktivnost_zaposlenici.radnja  ~* '" + txtUvijet.Text + "' AND";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio + Uvijet;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 200";
            }
            else
            {
                top = " TOP(200) ";
            }

            string sql = "SELECT " + top + " zaposlenici.ime + '' + zaposlenici.prezime AS Zaposlenik,aktivnost_zaposlenici.datum AS Datum,aktivnost_zaposlenici.radnja AS Radnja" +
                " FROM aktivnost_zaposlenici" +
                " INNER JOIN zaposlenici ON aktivnost_zaposlenici.id_zaposlenik = zaposlenici.id_zaposlenik " + filter +
                " ORDER BY DATUM DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            PaintRows(dgv);
        }

        private void fillCB()
        {
            //fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            //cbVD.DataSource = DSvd.Tables[0];
            //cbVD.DisplayMember = "vd";
            //cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
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