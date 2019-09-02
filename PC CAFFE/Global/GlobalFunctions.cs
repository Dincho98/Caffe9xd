using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Global
{
    static class GlobalFunctions
    {
        /// <summary>
        /// Method used to create In cindition for queries
        /// </summary>
        public static string CreateInCondition(List<int> list, bool isString = false)
        {
            string inStatement = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!isString)
                {
                    inStatement += list[i].ToString();
                }
                else
                    inStatement += ("'" + list[i].ToString() + "'");

                if (i + 1 < list.Count)
                    inStatement += ",";
            }
            return inStatement;
        }

        public static void BackupDatabase()
        {
            string remoteServer = "";
            string DBname = "";

            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

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

        public static void RestartApplication()
        {
            Application.Restart();
            Environment.Exit(0);
        }

        public static void SpremiPdf(string ime,ReportViewer reportViewer)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, extension;
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Dokumenti\"+ ime+"."+ extension);
            System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath, System.IO.FileMode.Create);
            pdfFile.Write(bytes, 0, bytes.Length);
            pdfFile.Close();
        }

        //Ova funkcija spaja datum i vrijeme u pravilnom formatu
        public static DateTime GenerirajDatumSVremenom(DateTime datum, int hour, int minute, int sec)
        {
            string[] dateValues = datum.ToString().Split('.');
            return new DateTime(Int32.Parse(dateValues[2]), Int32.Parse(dateValues[1]), Int32.Parse(dateValues[0]), hour, minute, sec);
        }

        public static bool AutomatskoPrintanjeSankaKuhinjePicerijeNijeUkljuceno()
        {
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            return DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "0" ? true : false;
        }
    }
}
