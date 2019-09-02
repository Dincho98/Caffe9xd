using System;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmNapomenaRacun : Form
    {
        public string napomena { get; set; }

        public frmNapomenaRacun()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNapomenaRacun_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            napomena = "";
        }

        private void zanemari_Click(object sender, EventArgs e)
        {
            napomena = "";
            this.Close();
        }

        private void ispis_Click(object sender, EventArgs e)
        {
            napomena = rtbNapomena.Text;
            this.Close();
        }
    }
}