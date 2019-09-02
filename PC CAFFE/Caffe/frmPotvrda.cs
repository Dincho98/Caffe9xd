using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPotvrda : Form
    {
        public frmPotvrda()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmOpcije MainForm_O { get; set; }
        public frmCaffe MainForm { get; set; }

        private void frmPotvrda_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            this.btnZavrsi.Select();
        }

        private void btnOdustani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                MainForm._zavrsi = true;
                this.Close();
            }
            else if (e.KeyData == Keys.Escape)
            {
                MainForm._zavrsi = false;
                this.Close();
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                MainForm._zavrsi = false;
                this.Close();
            }
            else if (MainForm_O != null)
            {
                MainForm_O._zavrsi = false;
                this.Close();
            }
        }

        private void btnZavrsi_Click(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                MainForm._zavrsi = true;
                this.Close();
            }
            else if (MainForm_O != null)
            {
                MainForm_O._zavrsi = true;
                this.Close();
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