using System;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPorezNaPotrosnju : Form
    {
        public frmPorezNaPotrosnju()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Caffe.frmMaliPrinter frm = new Caffe.frmMaliPrinter();
            frm.datumOD = dtpOD.Value;
            frm.datumDO = dtpDO.Value;
            frm.ImeForme = "Porez na potrošnju po grupama";
            frm.dokumenat = "PorezNaPotrosnju";
            frm.Show();
        }

        private void frmPorezNaPotrosnju_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

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