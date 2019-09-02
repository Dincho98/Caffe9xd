using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmRegistracija : Form
    {
        public frmRegistracija()
        {
            InitializeComponent();
        }

        private bool registrirano = false;
        public frmMenu MainForm { get; set; }

        private void frmRegistracija_Load(object sender, EventArgs e)
        {
            try
            {
                //string s = classFiskalizacija.ComputeHash(Encoding.ASCII.GetBytes((getUniqueID("C") + "5AR").ToCharArray())).ToUpper();
                //string ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);
                txtCode.Text = getUniqueID("C");
            }
            catch (Exception)
            {
            }
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            string tvrtka = txtTvrtka.Text;
            string oib = txtOib.Text;
            string mail = txtMail.Text;

            if (tvrtka == "") { MessageBox.Show("Krivo upisana tvrtka."); return; }
            if (oib == "") { MessageBox.Show("Krivo upisani oib."); return; }
            if (mail == "") { MessageBox.Show("Krivo upisana mail adresa."); return; }
            if (oib.Length != 11) { MessageBox.Show("Krivo upisana mail adresa."); return; }

            string code = getUniqueID("C");
            string generirano = GetMD5(oib + mail + code);

            if (generirano == txtGenerirano.Text)
            {
                registrirano = true;
                classSQL.Setings_Update("UPDATE postavke SET aktivnost='1'");
                File.WriteAllText("code", generirano);
                MessageBox.Show("Program je uspješno registriran.");
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Krivo upisani podaci.");
            }
        }

        public string GetMD5(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        private void frmRegistracija_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (registrirano == false)
            {
                Application.Exit();
            }
        }
    }
}