using Microsoft.VisualBasic;
using SkiniNovijeFajloveZaPostgres.csproj;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace SkiniNovijeFajloveZaPostgres
{
    public partial class frmDownloadPostgres : Form
    {
        int brProgres = 0;
        decimal nadogradiNaVerziju = 0;
        decimal[] podrzaneVerzije = new decimal[] { 9.1M, 9.2M, 9.3M, 9.5M };
        public frmDownloadPostgres()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ver = File.ReadAllLines("DBbackup\\VerzijaPostgres.txt");
            string verzija = Interaction.InputBox("Odabir verzije PostgreSQL", "Odabir razdoblja za koje želite poslati ponovno stavke na web server.", ver[0]);

            bool pretvoriUDecimal = decimal.TryParse(verzija.Replace('.', ','), out nadogradiNaVerziju);

            int indexUArrayu = Array.LastIndexOf(podrzaneVerzije, nadogradiNaVerziju);

            if (verzija != null && verzija != "" && pretvoriUDecimal && indexUArrayu >= 0)
            {
                bgDownload.RunWorkerAsync();
            }
            else {
                this.Close();
            }
        }

        private void SkiniPotrebneStavke()
        {
            Process[] pArry = Process.GetProcesses();

            foreach (Process p in pArry)
            {
                string s = p.ProcessName;
                if (s.CompareTo("PC POS") == 0)
                {
                    p.Kill();
                }
            }

            classDownloadFiles D = new classDownloadFiles();
            if (nadogradiNaVerziju == 9.2M)
            {
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/de/Npgsql.resources.dll", "de/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/es/Npgsql.resources.dll", "es/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fi/Npgsql.resources.dll", "fi/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fr/Npgsql.resources.dll", "fr/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/ja/Npgsql.resources.dll", "ja/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/zh-CN/Npgsql.resources.dll", "zh-CN/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Mono.Security.dll", "Mono.Security.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.dll", "Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.doc", "Npgsql.pdb");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.XML", "Npgsql.XML");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.doc", "policy.2.0.Npgsql.config");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.dll", "policy.2.0.Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libeay32.dll", "DBbackup\\libeay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libiconv.dll", "DBbackup\\libiconv.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libiconv-2.dll", "DBbackup\\libiconv-2.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libintl.dll", "DBbackup\\libintl.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libintl-8.dll", "DBbackup\\libintl-8.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/libpq.dll", "DBbackup\\libpq.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/pg_dump.exe", "DBbackup\\pg_dump.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/pg_dumpall.exe", "DBbackup\\pg_dumpall.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/psql.exe", "DBbackup\\psql.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/ssleay32.dll", "DBbackup\\ssleay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.3/zlib1.dll", "DBbackup\\zlib1.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
            }
            else if (nadogradiNaVerziju == 9.3M)
            {
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/de/Npgsql.resources.dll", "de/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/es/Npgsql.resources.dll", "es/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fi/Npgsql.resources.dll", "fi/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fr/Npgsql.resources.dll", "fr/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/ja/Npgsql.resources.dll", "ja/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/zh-CN/Npgsql.resources.dll", "zh-CN/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Mono.Security.dll", "Mono.Security.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.dll", "Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.doc", "Npgsql.pdb");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.XML", "Npgsql.XML");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.doc", "policy.2.0.Npgsql.config");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.dll", "policy.2.0.Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libeay32.dll", "DBbackup\\libeay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libiconv.dll", "DBbackup\\libiconv.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libiconv-2.dll", "DBbackup\\libiconv-2.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libintl.dll", "DBbackup\\libintl.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libintl-8.dll", "DBbackup\\libintl-8.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/libpq.dll", "DBbackup\\libpq.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/pg_dump.exe", "DBbackup\\pg_dump.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/pg_dumpall.exe", "DBbackup\\pg_dumpall.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/psql.exe", "DBbackup\\psql.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/ssleay32.dll", "DBbackup\\ssleay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.2/zlib1.dll", "DBbackup\\zlib1.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;

                File.WriteAllText("DBbackup\\VerzijaPostgres.txt", "9.3");
            }
            else if (nadogradiNaVerziju == 9.5M)
            {
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/de/Npgsql.resources.dll", "de/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/es/Npgsql.resources.dll", "es/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fi/Npgsql.resources.dll", "fi/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/fr/Npgsql.resources.dll", "fr/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/ja/Npgsql.resources.dll", "ja/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/zh-CN/Npgsql.resources.dll", "zh-CN/Npgsql.resources.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Mono.Security.dll", "Mono.Security.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.dll", "Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.doc", "Npgsql.pdb");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/Npgsql.XML", "Npgsql.XML");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.doc", "policy.2.0.Npgsql.config");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/ftpbin/policy.2.0.Npgsql.dll", "policy.2.0.Npgsql.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libeay32.dll", "DBbackup\\libeay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libiconv.dll", "DBbackup\\libiconv.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libiconv-2.dll", "DBbackup\\libiconv-2.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                //D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libintl.dll", "DBbackup\\libintl.dll");
                //bgDownload.ReportProgress(brProgres + 1);
                //brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libintl-8.dll", "DBbackup\\libintl-8.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/libpq.dll", "DBbackup\\libpq.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/pg_dump.exe", "DBbackup\\pg_dump.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/pg_dumpall.exe", "DBbackup\\pg_dumpall.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/psql.exe", "DBbackup\\psql.exe");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/ssleay32.dll", "DBbackup\\ssleay32.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;
                D.SkiniDatoteku("https://pc1.hr/caffe/update/ostalo/pg-9.5/zlib1.dll", "DBbackup\\zlib1.dll");
                bgDownload.ReportProgress(brProgres + 1);
                brProgres++;

                File.WriteAllText("DBbackup\\VerzijaPostgres.txt", "9.5");
            }

            
        }

        private void bgDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            SkiniPotrebneStavke();
        }

        private void bgDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            double d = (((double)e.ProgressPercentage / (double)23) * (double)100);
            lblProgres.Text = e.ProgressPercentage.ToString() + " od " + "23";
            progressBar1.Value = (int)d;

        }

        private void bgDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
