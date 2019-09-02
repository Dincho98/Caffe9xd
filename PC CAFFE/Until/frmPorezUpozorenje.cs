using System;
using System.Windows.Forms;

namespace PCPOS.Until
{
    public partial class frmPorezUpozorenje : Form
    {
        public frmPorezUpozorenje()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPromijena_Click(object sender, EventArgs e)
        {
            frmPromjenaPoreza frm = new frmPromjenaPoreza();
            this.Close();
            frm.ShowDialog();
        }

        private void btnNeSmetaj_Click(object sender, EventArgs e)
        {
            classSQL.Setings_Update("UPDATE postavke SET promjena_poreza=0");
            this.Close();
        }
    }
}