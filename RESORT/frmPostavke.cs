using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace RESORT
{
    public partial class frmPostavke : Form
    {
        public frmPostavke()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;
        private DataTable DTpostavke;
        private bool load = false;

        private void frmPostavke_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];
            Fill();
            load = true;
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private INIFile ini = new INIFile();

        private void Fill()
        {
            nuPDV.Maximum = 40;
            nuPDV.Minimum = 0;

            try
            {
                nuPDV.Value = int.Parse(DTpostavke.Rows[0]["pdv_nocenje"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
            {
                rbPoSobi.Checked = true;
            }
            else
            {
                rbPoGostu.Checked = true;
            }

            txtRemoteImeServera.Text = ini.Read("Postgre", "hostname");
            txtRemoteLozinka.Text = "q1w2e3r4";
            txtRemoteUsername.Text = ini.Read("Postgre", "username");
            txtRemotePort.Text = ini.Read("Postgre", "port");
            cbRemoteNameDatabase.Text = ini.Read("Postgre", "ime_baze");
            txtBackupLokacije.Text = ini.Read("Postgre", "backup_path");

            if (ini.Read("Postgre", "aktivnost_backup_baze") == "1")
            {
                chbBackupAktivnost.Checked = true;
            }
            else
            {
                chbBackupAktivnost.Checked = false;
            }

            if (DTpostavke.Rows[0]["bool_path_image"].ToString() == "1")
            {
                try
                {
                    cblogo.Checked = true;
                    pblogo.Image = Image.FromFile(DTpostavke.Rows[0]["path_image"].ToString());
                    txtputanja.Text = DTpostavke.Rows[0]["path_image"].ToString();
                }
                catch { }
            }
            else
            {
                cblogo.Checked = false;
            }

            //boje fill__________________________
            txtBoja1.Text = DTBojeForme.Rows[0]["x1"].ToString();
            txtBoja2.Text = DTBojeForme.Rows[0]["x2"].ToString();
            txtBoja3.Text = DTBojeForme.Rows[0]["x3"].ToString();
            txtBoja4.Text = DTBojeForme.Rows[0]["y1"].ToString();
            txtBoja5.Text = DTBojeForme.Rows[0]["y2"].ToString();
            txtBoja6.Text = DTBojeForme.Rows[0]["y3"].ToString();
        }

        private void txtBoja1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE FormColors SET " +
                    "x1='" + txtBoja1.Text + "'," +
                    "x2='" + txtBoja2.Text + "'," +
                    "x3='" + txtBoja3.Text + "'," +
                    "y1='" + txtBoja4.Text + "'," +
                    "y2='" + txtBoja5.Text + "'," +
                    "y3='" + txtBoja6.Text + "'";

                classDBlite.LiteSqlCommand(sql);
            }
            catch (Exception)
            {
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Forme.frmTestColor pos = new Forme.frmTestColor();
            pos.ShowDialog();
        }

        private void btnNadogradi_Click(object sender, EventArgs e)
        {
            string path = GetApplicationPath();
            File.WriteAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/RESORT update.txt", path, Encoding.UTF8);

            Process.Start(path + "\\RESORT Update.exe");
        }

        private void btnSpremiBackup_Click(object sender, EventArgs e)
        {
            string active = "0";
            if (chbBackupAktivnost.Checked)
            {
                active = "1";
            }

            ini.Write("Postgre", "aktivnost_backup_baze", active);
            ini.Write("Postgre", "backup_path", txtBackupLokacije.Text);
        }

        private static string GetApplicationPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private void btnProvjeriNadogradnju_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://www.pc1.hr/update/resort/verzija.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");
            string VerzijaNaNetu = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");

            if (Properties.Settings.Default.verzija_programa < Convert.ToDecimal(VerzijaNaNetu))
            {
                if (MessageBox.Show("Na internetu postoji novija inačica programa.\r\n\r\nVaša verziju programa je: " + Properties.Settings.Default.verzija_programa + ".\r\nŽelite li skinuti noviju verziju programa.", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnNadogradi.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Trenutno koristite najnoviju inačicu programa.\r\nVaša verziju programa je: " + Properties.Settings.Default.verzija_programa + ".", "Update");
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            File.WriteAllText("DBbackup/DBbackup.bat", "pg_dump.exe --host " + txtRemoteImeServera.Text + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + txtBackupLokacije.Text + "\\db" + DateTime.Now.ToString("yyyy-MM-dd") + ".backup\" \"" + cbRemoteNameDatabase.Text + "\"");
            string path = System.Environment.CurrentDirectory;
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = path + "\\DBbackup";
            proc.StartInfo.FileName = path + "\\DBbackup\\DBbackup.bat";
            proc.Start();
        }

        private void btnLoadBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBackupLokacije.Text = folderDlg.SelectedPath;
            }
        }

        private void txtRemoteSpremi_Click(object sender, EventArgs e)
        {
            ini.Write("Postgre", "username", txtRemoteUsername.Text);
            ini.Write("Postgre", "port", txtRemotePort.Text);
            ini.Write("Postgre", "password", txtRemoteLozinka.Text);
            ini.Write("Postgre", "ime_baze", cbRemoteNameDatabase.Text);
            ini.Write("Postgre", "hostname", txtRemoteImeServera.Text);
        }

        private void nuPDV_ValueChanged(object sender, EventArgs e)
        {
            if (load)
                classDBlite.LiteSqlCommand("UPDATE postavke SET pdv_nocenje='" + nuPDV.Value + "'");
        }

        private void rbPoGostu_CheckedChanged(object sender, EventArgs e)
        {
            if (load)
                classDBlite.LiteSqlCommand("UPDATE postavke SET naplata_po_sobi='0'");
        }

        private void rbPoSobi_CheckedChanged(object sender, EventArgs e)
        {
            if (load)
                classDBlite.LiteSqlCommand("UPDATE postavke SET naplata_po_sobi='1'");
        }

        private void btnpath_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            txtputanja.Text = openFileDialog1.FileName;
                            pblogo.Image = new Bitmap(openFileDialog1.FileName);
                            pblogo.SizeMode = PictureBoxSizeMode.StretchImage;
                            pblogo.BorderStyle = BorderStyle.Fixed3D;
                            pblogo.BringToFront();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            if (cblogo.Checked)
            {
                txtputanja.Text = txtputanja.Text.Trim();

                if (txtputanja.Text == "")
                {
                    MessageBox.Show("Odaberite putanju slike za logo ili ostavite opciju " +
                        "'Logo na fakturi' neoznačenu!", "Upozorenje!");

                    txtputanja.Text = txtputanja.Text.Trim();
                }

                if (txtputanja.Text.Trim() == "")
                    return;

                try
                {
                    Image img = Image.FromFile(txtputanja.Text);
                    if (ImageFormat.Jpeg.Equals(img.RawFormat))
                    {
                        // JPEG
                    }
                    else if (ImageFormat.Png.Equals(img.RawFormat))
                    {
                        // PNG
                    }
                    else if (ImageFormat.Gif.Equals(img.RawFormat))
                    {
                        // GIF
                    }
                    else if (ImageFormat.Bmp.Equals(img.RawFormat))
                    {
                        // Bmp
                    }
                    else
                        return;
                }
                catch
                {
                    MessageBox.Show("Odabrana datoteka nije slika!", "Greška!");
                    return;
                }
            }
            else
            {
                txtputanja.Text = "";
            }

            if (load)
            {
                if (cblogo.Checked)
                {
                    classDBlite.LiteSqlCommand("UPDATE postavke SET bool_path_image='1'");
                    classDBlite.LiteSqlCommand("UPDATE postavke SET path_image='" + txtputanja.Text + "'");
                }
                else
                {
                    classDBlite.LiteSqlCommand("UPDATE postavke SET bool_path_image='0'");
                    classDBlite.LiteSqlCommand("UPDATE postavke SET path_image='" + txtputanja.Text + "'");
                }
            }
        }

        private void cblogo_CheckedChanged(object sender, EventArgs e)
        {
            if (load)
            {
                if (cblogo.Checked)
                {
                    classDBlite.LiteSqlCommand("UPDATE postavke SET bool_path_image='1'");
                    classDBlite.LiteSqlCommand("UPDATE postavke SET path_image='" + txtputanja.Text + "'");
                }
                else
                {
                    classDBlite.LiteSqlCommand("UPDATE postavke SET bool_path_image='0'");
                    classDBlite.LiteSqlCommand("UPDATE postavke SET path_image=''");
                }
            }
        }
    }
}