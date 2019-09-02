using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmPostavaBaze : Form
    {
        public frmPostavaBaze()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPostavaBaze_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void btnKompaktnaTest_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection connection = new SqlCeConnection("Data Source=" + txtKompaktnaPut.Text + ";Password=drazen2814mia;Persist Security Info=True");
                if (connection.State.ToString() == "Closed") { connection.Open(); }
                MessageBox.Show("Konekcija je uspjela.");
            }
            catch (Exception)
            {
                MessageBox.Show("Konekcija nije uspjela.");
            }
        }

        private void btnKompaktnaSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection connection = new SqlCeConnection("Data Source=" + txtKompaktnaPut.Text + ";Password=drazen2814mia;Persist Security Info=True");
                if (connection.State.ToString() == "Closed") { connection.Open(); }
            }
            catch (Exception)
            {
                MessageBox.Show("Konekcija nije uspjela.");
                return;
            }

            if (!File.Exists(txtKompaktnaPut.Text))
            {
                MessageBox.Show("Odabrana baza ne postoji.", "Greška");
                return;
            }

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_compact").Elements("path_database") select c;
            foreach (XElement book in query)
            {
                book.Attribute("path").Value = txtKompaktnaPut.Text;
            }
            xmlFile.Save(path);
            MessageBox.Show("Spremljeno", "Spremljeno");
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtKompaktnaPut.Text = openFileDialog1.FileName;
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