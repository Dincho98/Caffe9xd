using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS
{
    internal class classSQL
    {
        public static string connectionString = SQL.claasConnectDatabase.GetCompactConnectionString();
        public static DataSet dataSet = new DataSet();
        public static SqlCeConnection connection = new SqlCeConnection(connectionString);
        public static SqlCeDataAdapter adapter = new SqlCeDataAdapter();
        public static NpgsqlDataAdapter Npgadapter = new NpgsqlDataAdapter();
        public static string remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
        public static NpgsqlConnection remoteConnection = new NpgsqlConnection(remoteConnectionString);

        //SELECT
        public static DataSet select(string sql, string table)
        {
            if (remoteConnectionString == "")
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
                dataSet = new DataSet();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql.Replace("zbroj", "+"), connectionString);
                adapter.Fill(dataSet, table);

                //connection.Close();
                return dataSet;
            }
            else
            {
                try
                {
                    dataSet = new DataSet();
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
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
        }

        //SELECT SETTINGS
        public static DataSet select_settings(string sql, string table)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
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

        public static string Setings_Update(string sql)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
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
        public static string insert(string sql)
        {
            if (remoteConnectionString == "")
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) { connection.Open(); }
                    SqlCeTransaction tx = connection.BeginTransaction();
                    SqlCeCommand cmd = connection.CreateCommand();
                    cmd.Transaction = tx;
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    tx.Commit();

                    return "";
                }
                catch (Exception msg)
                {
                    connection.Close();
                    return msg.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
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

        //UPDATE
        public static string update(string sql)
        {
            if (remoteConnectionString == "")
            {
                try
                {
                    {
                        if (connection.State == ConnectionState.Closed) { connection.Open(); }
                        dataSet = new DataSet();
                        adapter.SelectCommand = new SqlCeCommand(sql, connection);
                        SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
                        adapter.Fill(dataSet);
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
                    NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                    comm.CommandTimeout = 180;
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

        //ADAPTER
        public static SqlCeDataAdapter CeAdatpter(string sql)
        {
            if (connection.State == ConnectionState.Closed) { connection.Open(); }
            dataSet = new DataSet();
            adapter.SelectCommand = new SqlCeCommand(sql.Replace("zbroj", "+"), connection);
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);

            return adapter;
        }

        public static NpgsqlDataAdapter NpgAdatpter(string sql)
        {
            try
            {
                if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
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
        public static string delete(string sql)
        {
            if (remoteConnectionString == "")
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
                SqlCeTransaction tx = connection.BeginTransaction();
                SqlCeCommand cmd1 = connection.CreateCommand();
                cmd1.Transaction = tx;
                cmd1.Connection = connection;
                cmd1.CommandText = sql;
                cmd1.ExecuteNonQuery();
                tx.Commit();

                return "";
            }
            else
            {
                try
                {
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
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

        public static string UpdateRows(string id_skladiste, string kolicina, string nc, string vpc, string porez, string sifra)
        {
            if (remoteConnectionString == "")
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) { connection.Open(); }
                    string updateSql = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";
                    SqlCeCommand UpdateCmd = new SqlCeCommand(updateSql, connection);

                    UpdateCmd.Parameters.Add("@id_skladiste", SqlDbType.Int, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", SqlDbType.NVarChar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@npc", SqlDbType.Money, 8, "npc");
                    UpdateCmd.Parameters.Add("@vpc", SqlDbType.Money, 8, "vpc");
                    UpdateCmd.Parameters.Add("@porez", SqlDbType.NVarChar, 20, "porez");
                    UpdateCmd.Parameters.Add("@sifra", SqlDbType.NVarChar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@npc"].Value = kolicina;
                    UpdateCmd.Parameters["@vpc"].Value = vpc;
                    UpdateCmd.Parameters["@porez"].Value = porez;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();

                    return "";
                }
                catch (SqlCeException ex)
                {
                    connection.Close();
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
                    string updateSql = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";
                    NpgsqlCommand UpdateCmd = new NpgsqlCommand(updateSql, remoteConnection);

                    UpdateCmd.Parameters.Add("@id_skladiste", NpgsqlDbType.Integer, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@npc", NpgsqlDbType.Money, 8, "npc");
                    UpdateCmd.Parameters.Add("@vpc", NpgsqlDbType.Money, 8, "vpc");
                    UpdateCmd.Parameters.Add("@porez", NpgsqlDbType.Varchar, 20, "porez");
                    UpdateCmd.Parameters.Add("@sifra", NpgsqlDbType.Varchar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@npc"].Value = kolicina;
                    UpdateCmd.Parameters["@vpc"].Value = vpc;
                    UpdateCmd.Parameters["@porez"].Value = porez;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();
                    remoteConnection.Close();

                    return "";
                }
                catch (SqlCeException ex)
                {
                    remoteConnection.Close();
                    return ex.ToString();
                }
            }
        }
    }
}