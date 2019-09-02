using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLroba_prodaja
    {
        public static string UpdateRows(string id_skladiste, string kolicina, string sifra)
        {
            if (classSQL.remoteConnectionString == "")
            {
                try
                {
                    if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                    SqlCeCommand UpdateCmd = classSQL.connection.CreateCommand();

                    UpdateCmd.CommandText = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";

                    UpdateCmd.Parameters.Add("@id_skladiste", SqlDbType.Int, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", SqlDbType.NVarChar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@sifra", SqlDbType.NVarChar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();
                    classSQL.connection.Close();
                    return "";
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }

                    NpgsqlCommand UpdateCmd = classSQL.remoteConnection.CreateCommand();

                    UpdateCmd.CommandText = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";

                    UpdateCmd.Parameters.Add("@id_skladiste", NpgsqlDbType.Integer, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@sifra", NpgsqlDbType.Varchar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();
                    classSQL.connection.Close();
                    return "";
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
            }
        }

        public static string UpdateRowsPlus(string id_skladiste, string kolicina, string sifra)
        {
            try
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                SqlCeCommand UpdateCmd = classSQL.connection.CreateCommand();

                UpdateCmd.CommandText = "UPDATE mytable, (SELECT @roba_prodaja := MAX(col1) FROM roba_prodaja) o SET col1 = (@loop := @loop + 1)";

                UpdateCmd.Parameters.Add("@id_skladiste", SqlDbType.Int, 4, "id_skladiste");
                UpdateCmd.Parameters.Add("@kolicina", SqlDbType.NVarChar, 10, "kolicina");
                UpdateCmd.Parameters.Add("@sifra", SqlDbType.NVarChar, 20, "sifra");

                UpdateCmd.Parameters["@sifra"].Value = sifra;
                UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                UpdateCmd.ExecuteNonQuery();
                classSQL.connection.Close();
                return "";
            }
            catch (SqlCeException ex)
            {
                classSQL.connection.Close();
                return ex.ToString();
            }
        }
    }
}