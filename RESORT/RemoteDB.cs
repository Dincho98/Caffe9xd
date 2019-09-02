using Npgsql;
using System;
using System.Data;

namespace RESORT
{
    internal class RemoteDB
    {
        public static DataSet dataSet = new DataSet();
        public static NpgsqlDataAdapter Npgadapter = new NpgsqlDataAdapter();

        public static DataTable DTremoteDB = classDBlite.LiteSelect("SELECT * FROM remote_database WHERE id=1", "remote_database").Tables[0];
        public static string remoteConnectionString = GetRemoteConnString();
        public static NpgsqlConnection remoteConnection = new NpgsqlConnection(remoteConnectionString);

        private static string GetRemoteConnString()
        {
            INIFile ini = new INIFile();
            return "Server=" + ini.Read("Postgre", "hostname") + ";Port=" + ini.Read("Postgre", "port") + ";" + "User Id=" + ini.Read("Postgre", "username") + ";Password=" + "q1w2e3r4" + ";Database=" + ini.Read("Postgre", "ime_baze") + ";";
        }

        //SELECT
        public static DataSet select(string sql, string table)
        {
            dataSet = new DataSet();
            if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
            dataSet.Reset();
            da.Fill(dataSet);
            return dataSet;
        }

        //password = classSQL.select_settings("SELECT pass FROM postavke", "postavke").Tables[0].Rows[0][0].ToString(); ;
        //return "Server=" + server + ";Port=" + port + ";" + "User Id=" + username + ";Password=" + password + ";Database=" + database + ";";

        //INSERT
        public static string insert(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                remoteConnection.Close();
                Funkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return ex.ToString();
            }
        }

        //UPDATE
        public static string update(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                remoteConnection.Close();
                Funkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return ex.ToString();
            }
        }

        public static NpgsqlDataAdapter NpgAdatpter(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                dataSet = new DataSet();
                Npgadapter.SelectCommand = new NpgsqlCommand(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(Npgadapter);
                return Npgadapter;
            }
            catch (Exception ex)
            {
                Funkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return Npgadapter;
            }
        }

        //DELETE
        public static string delete(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                remoteConnection.Close();
                Funkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return ex.ToString();
            }
        }
    }
}