using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT
{
    public partial class frmOdabir : Form
    {
        public frmOdabir()
        {
            InitializeComponent();
        }

        public string OD_datuma { get; set; }
        public string DO_datuma { get; set; }
        public string soba { get; set; }
        public string __godina { get; set; }

        private DataTable DTBojeForme;
        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmOdabir_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            lblOdDatuma.Text = "Od datuma: " + OD_datuma;
            lblDodatuma.Text = "Do datuma: " + DO_datuma;
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

        private void btnRezervacija_Click(object sender, EventArgs e)
        {
            Forme.frmUnosRezervacije f = new Forme.frmUnosRezervacije();
            f.__OD_datuma = OD_datuma;
            f.__DO_datuma = DO_datuma;
            f.__soba = soba;
            f.__godina = __godina;
            f.ShowDialog();
        }

        private void btnUnosGosta_Click(object sender, EventArgs e)
        {
            Forme.frmUpisGosta f = new Forme.frmUpisGosta();
            f.__OD_datuma = OD_datuma;
            f.__DO_datuma = DO_datuma;
            f.__soba = soba;
            f.__godina = __godina;
            f.ShowDialog();
        }
    }
}