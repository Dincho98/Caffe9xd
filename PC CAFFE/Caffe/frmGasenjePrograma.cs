using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Caffe
{
    public partial class frmGasenjePrograma : Form
    {
        public frmMenu MainForm { get; set; }
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        public frmGasenjePrograma()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmGasenjePrograma_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void BackupBaze()
        {
            string remoteServer = "";
            string DBname = "";

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                remoteServer = book.Attribute("server").Value;
                DBname = book.Attribute("database").Value;
            }

            string lokacija_za_spremanje = "";
            if (Directory.Exists(DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString()))
            {
                lokacija_za_spremanje = DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString();
            }
            else
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString()))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString());
                }

                lokacija_za_spremanje = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString();
            }

            File.WriteAllText("DBbackup/DBbackup.bat", "pg_dump.exe --host " + remoteServer + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + lokacija_za_spremanje + "\\db" + DateTime.Now.ToString("yyyy-MM-dd") + ".backup\" \"" + DBname + "\"");
            string _path = System.Environment.CurrentDirectory;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.WorkingDirectory = _path + "\\DBbackup";
            proc.StartInfo.FileName = _path + "\\DBbackup\\DBbackup.bat";
            proc.Start();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            BackupBaze();
            Application.Exit();
        }

        private void btnIzlazIzProg_Click(object sender, EventArgs e)
        {
            BackupBaze();
            try
            {
                Application.Exit();
            }
            catch { }
        }

        private void btnizlazIgasenjeR_Click(object sender, EventArgs e)
        {
            BackupBaze();
            System.Threading.Thread.Sleep(15000);
            Process.Start("shutdown", "/s /t 0");
            this.Close();
        }

        private void btnIzlazIzProg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                btnIzlazIzProg.PerformClick();
            }
            else if (e.KeyData == Keys.F2)
            {
                btnizlazIgasenjeR.PerformClick();
            }
            else if (e.KeyData == Keys.Escape)
            {
                btnIzlaz.PerformClick();
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