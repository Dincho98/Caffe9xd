using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmProizvodaci : Form
    {
        public frmProizvodaci()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSp = new DataSet();

        private void frmProizvodaci_Load(object sender, EventArgs e)
        {
            FillP();
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void FillP()
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_manufacturers AS Šifra,manufacturers AS [Proizvođač] FROM manufacturers").Fill(DSp, "manufacturers");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_manufacturers AS [Šifra],manufacturers AS [Proizvođač] FROM manufacturers").Fill(DSp, "manufacturers");
            }

            dgv.DataSource = DSp.Tables["manufacturers"];
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_manufacturers AS Šifra,manufacturers AS [Proizvođač] FROM manufacturers").Update(DSp, "manufacturers");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_manufacturers AS [Šifra],manufacturers AS [Proizvođač] FROM manufacturers").Update(DSp, "manufacturers");
            }

            dgv.DataSource = DSp.Tables["manufacturers"];
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