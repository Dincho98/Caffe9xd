using Npgsql;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmPostavkeUdaljeneBaze : Form
    {
        public frmPostavkeUdaljeneBaze()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTpostavke;

        private void frmPostavkeUdaljeneBaze_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            SetRemoteFields();
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                txtRemoteImeServera.Text = book.Attribute("server").Value;
                txtRemoteUsername.Text = book.Attribute("username").Value;
                txtRemotePort.Text = book.Attribute("port").Value;
                cbRemoteNameDatabase.Text = book.Attribute("database").Value;
                txtRemoteLozinka.Text = DTpostavke.Rows[0]["pass"].ToString();

                if (book.Attribute("active").Value == "1")
                {
                    chbActive.Checked = true;
                }
                else
                {
                    chbActive.Checked = false;
                }
            }
            txtRemoteLozinka.PasswordChar = '*';

            try
            {
                DataSet DSss = new DataSet();
                NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(cbRemoteNameDatabase.Text, "postgres"));
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT datname FROM pg_database WHERE datistemplate = false;", remoteConnection);
                DSss.Reset();
                da.Fill(DSss);
                remoteConnection.Close();

                for (int i = 0; i < DSss.Tables[0].Rows.Count; i++)
                {
                    cbRemoteNameDatabase.Items.Add(DSss.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtRemoteSpremi_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                book.Attribute("server").Value = txtRemoteImeServera.Text;
                book.Attribute("username").Value = txtRemoteUsername.Text;
                book.Attribute("port").Value = txtRemotePort.Text;
                book.Attribute("database").Value = cbRemoteNameDatabase.Text;
                classSQL.Setings_Update("UPDATE postavke SET pass='" + txtRemoteLozinka.Text + "'");

                if (chbActive.Checked)
                {
                    book.Attribute("active").Value = "1";
                }
                else
                {
                    book.Attribute("active").Value = "0";
                }
            }
            xmlFile.Save(path);
            MessageBox.Show("Spremljeno");
            Application.Restart();
        }

        private void txtRemoteTest_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                book.Attribute("server").Value = txtRemoteImeServera.Text;
                book.Attribute("username").Value = txtRemoteUsername.Text;
                book.Attribute("port").Value = txtRemotePort.Text;
                book.Attribute("database").Value = cbRemoteNameDatabase.Text;
                classSQL.Setings_Update("UPDATE postavke SET pass='" + txtRemoteLozinka.Text + "'");

                if (chbActive.Checked)
                {
                    book.Attribute("active").Value = "1";
                }
                else
                {
                    book.Attribute("active").Value = "0";
                }
            }
            xmlFile.Save(path);

            if (SQL.claasConnectDatabase.TestRemoteConnection() == true)
            {
                MessageBox.Show("Konekcija je uspjela.");
            }
            else
            {
                MessageBox.Show("Konekcija nije uspjela.");
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