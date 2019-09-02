using Npgsql;
using System;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS
{
    internal class newSql
    {
        private DataSet dataSet = new DataSet();
        private string connectionString;
        private SqlCeConnection connection;
        private SqlCeDataAdapter adapter;
        private NpgsqlDataAdapter Npgadapter;
        private string remoteConnectionString;
        private NpgsqlConnection remoteConnection;

        public newSql()
        {
            connectionString = SQL.claasConnectDatabase.GetCompactConnectionString();
            connection = new SqlCeConnection(connectionString);
            adapter = new SqlCeDataAdapter();
            Npgadapter = new NpgsqlDataAdapter();
            remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
            remoteConnection = new NpgsqlConnection(remoteConnectionString);
        }

        public DataSet select(string sql, string table)
        {
            try
            {
                dataSet = new DataSet();
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                dataSet.Reset();

                da.Fill(dataSet);
                remoteConnection.Close();
                return dataSet;
            }
            catch
            {
                remoteConnection.Close();
                return dataSet;
            }
        }

        //SELECT SETTINGS
        public DataSet select_settings(string sql, string table)
        {
            try
            {
                if (connection.State.ToString() == "Closed") { connection.Open(); }
                dataSet = new DataSet();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, connectionString);
                adapter.Fill(dataSet, table);
                connection.Close();
                return dataSet;
            }
            catch (Exception)
            {
                return dataSet;
            }
        }

        public string Setings_Update(string sql)
        {
            try
            {
                if (connection.State.ToString() == "Closed") { connection.Open(); }
                SqlCeCommand comm = new SqlCeCommand(sql, connection);
                comm.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                connection.Close();
                return ex.ToString();
            }
        }

        //INSERT
        public string insert(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                remoteConnection.Close();
                return "";
            }
            catch (NpgsqlException ex)
            {
                remoteConnection.Close();
                return ex.ToString();
            }
        }

        //UPDATE
        public string update(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                remoteConnection.Close();
                return "";
            }
            catch (NpgsqlException ex)
            {
                remoteConnection.Close();
                return ex.ToString();
            }
        }

        //ADAPTER
        public SqlCeDataAdapter CeAdatpter(string sql)
        {
            if (connection.State.ToString() == "Closed") { connection.Open(); }
            dataSet = new DataSet();
            adapter.SelectCommand = new SqlCeCommand(sql.Replace("zbroj", "+"), connection);
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
            return adapter;
        }

        public NpgsqlDataAdapter NpgAdatpter(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                dataSet = new DataSet();
                Npgadapter.SelectCommand = new NpgsqlCommand(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(Npgadapter);
                remoteConnection.Close();
                return Npgadapter;
            }
            catch (NpgsqlException)
            {
                return Npgadapter;
            }
        }

        //DELETE
        public string delete(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                comm.ExecuteNonQuery();
                remoteConnection.Close();
                return "";
            }
            catch (NpgsqlException ex)
            {
                remoteConnection.Close();
                return ex.ToString();
            }
        }
    }
}