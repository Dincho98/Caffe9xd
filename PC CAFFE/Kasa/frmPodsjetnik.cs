using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPodsjetnik : Form
    {
        public frmPodsjetnik()
        {
            InitializeComponent();
        }

        private void frmPodsjetnik_Load(object sender, EventArgs e)
        {
            btnOdjava.Select();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnOdjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}