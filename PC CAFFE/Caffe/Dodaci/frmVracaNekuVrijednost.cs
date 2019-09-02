using System;
using System.Windows.Forms;

namespace PCPOS.Caffe.Dodaci
{
    public partial class frmVracaNekuVrijednost : Form
    {
        public frmVracaNekuVrijednost()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string _title { get; set; }

        private void frmVracaNekuVrijednost_Load(object sender, EventArgs e)
        {
            label1.Text = _title;
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.privremena_vrijednost = null;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.privremena_vrijednost = txtBroj.Text;
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