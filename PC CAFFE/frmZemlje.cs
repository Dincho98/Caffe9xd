using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmZemlje : Form
    {
        public frmZemlje()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmZemlje_Load(object sender, EventArgs e)
        {
            DGVset();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataSet DS = new DataSet();

        private void DGVset()
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_zemlja as [Šifra],zemlja AS [Država],country_code AS Kod, aktivnost AS Aktivnost FROM zemlja ORDER BY zemlja ASC").Fill(DS, "zemlja");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_zemlja as [Šifra],zemlja AS [Država],country_code AS Kod, aktivnost AS Aktivnost FROM zemlja ORDER BY zemlja ASC").Fill(DS, "zemlja");
            }

            dgv.DataSource = DS.Tables["zemlja"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_zemlja as [Šifra],zemlja AS [Država],country_code AS Kod, aktivnost AS Aktivnost FROM zemlja ORDER BY zemlja ASC").Update(DS, "zemlja");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_zemlja as [Šifra],zemlja AS [Država],country_code AS Kod, aktivnost AS Aktivnost FROM zemlja ORDER BY zemlja ASC").Update(DS, "zemlja");
            }
            MessageBox.Show("Spremljeno");
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