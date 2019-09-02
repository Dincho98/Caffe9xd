using Npgsql;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.SQL
{
    internal class claasConnectDatabase
    {
        public static bool TestCompactConnection(string path)
        {
            string connectionString = "Data Source=database.sdf;Password=q1w2e3r4;Persist Security Info=True";
            SqlCeConnection conn = new SqlCeConnection(connectionString);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception) { return false; }
        }

        //public static string GetCompactDatabasePath()
        //{
        //    string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
        //    XDocument xmlFile = XDocument.Load(path);
        //    var query = from c in xmlFile.Element("settings").Elements("database_compact").Elements("path_database") select c;
        //    foreach (XElement book in query)
        //    {
        //        return book.Attribute("path").Value;
        //    }

        //    return "";
        //}

        public static string GetCompactConnectionString()
        {
            //string path = GetCompactDatabasePath();
            return "Data Source=database.sdf;Password=q1w2e3r4;Persist Security Info=True";
        }

        public static string GetRemoteConnectionString()
        {
            string server = "", port = "", username = "", password = "", database = "";
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            DataSet DS = new DataSet();
            DS.ReadXml(path);
            server = DS.Tables["postgree"].Rows[0]["server"].ToString();
            port = DS.Tables["postgree"].Rows[0]["port"].ToString();
            username = DS.Tables["postgree"].Rows[0]["username"].ToString();
            database = DS.Tables["postgree"].Rows[0]["database"].ToString();

            if (DS.Tables["postgree"].Rows[0]["active"].ToString() == "1")
            {
                password = classSQL.select_settings("SELECT pass FROM postavke", "postavke").Tables[0].Rows[0][0].ToString();
                ;
                return "Server=" + server + ";Port=" + port + ";" + "User Id=" + username + ";Password=" + password + ";Database=" + database + ";";
            }
            else
            {
                return "";
            }
        }

        public static bool TestRemoteConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(GetRemoteConnectionString());
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}