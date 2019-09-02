using System;
using System.Data;
using System.Data.SQLite;

namespace RESORT
{
    internal class classDBlite
    {
        private static SQLiteConnection sql_con;
        private static SQLiteCommand sql_cmd;
        private static SQLiteDataAdapter DB;
        private static DataSet DS = new DataSet();

        public static DataSet LiteSelect(string sql, string table)
        {
            LiteOpen();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = sql;
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS = new DataSet();
            DB.Fill(DS);
            LiteClose();
            return DS;
        }

        public static string LiteSqlCommand(string sql)
        {
            try
            {
                LiteOpen();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = sql;
                sql_cmd.ExecuteNonQuery();
                LiteClose();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string LiteOpen()
        {
            try
            {
                sql_con = new SQLiteConnection("Data Source=DBresort.db;Version=3;New=False;Compress=True;");
                sql_con.Open();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string LiteClose()
        {
            try
            {
                sql_con.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static DataSet dataSet = new DataSet();
        public static SQLiteDataAdapter _LiteAdapter = new SQLiteDataAdapter();

        public static SQLiteDataAdapter LiteAdatpter(string sql)
        {
            try
            {
                dataSet = new DataSet();
                _LiteAdapter.SelectCommand = new SQLiteCommand(sql, sql_con);
                SQLiteCommandBuilder builder = new SQLiteCommandBuilder(_LiteAdapter);
                return _LiteAdapter;
            }
            catch (Exception)
            {
                return _LiteAdapter;
            }
        }
    }
}