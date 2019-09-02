using Npgsql;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLfaktura
    {
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }
                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT INTO faktura_stavke (kolicina,vpc,nbc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi,porez_potrosnja)" +
                        " VALUES (@kolicina,@vpc,@nbc,@porez,@broj_fakture,@rabat,@id_skladiste,@sifra,@oduzmi,@porez_potrosnja)";
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_fakture", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
                    nonqueryCommand.Parameters.Add("@mpc", SqlDbType.Decimal);
                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                        nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_fakture"].Value = DT.Rows[i]["broj_fakture"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                        nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                        nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"].ToString();
                        nonqueryCommand.Parameters["@mpc"].Value = DT.Rows[i]["mpc"].ToString().Replace('.', ',');
                        nonqueryCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
                finally
                {
                    classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                //////////////////////////////////////////////////////REMOTE

                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "INSERT INTO faktura_stavke (kolicina,vpc,nbc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi,porez_potrosnja, mpc) VALUES " +
                            "(" +
                            "'" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            "'" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "',";
                        if (DT.Columns["nbc"] != null)
                        {
                            sql += " '" + DT.Rows[i]["nbc"].ToString() + "',";
                        }
                        else
                        {
                            sql += " '" + DT.Rows[i]["nc"].ToString() + "',";
                        }
                        sql += " '" + DT.Rows[i]["porez"].ToString() + "'," +
                        "'" + DT.Rows[i]["broj_fakture"].ToString() + "'," +
                        "'" + DT.Rows[i]["rabat"].ToString() + "'," +
                        "'" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                        "'" + DT.Rows[i]["sifra"].ToString() + "'," +
                        "'" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                        "'" + DT.Rows[i]["porez_potrosnja"].ToString() + "'," +
                        "'" + DT.Rows[i]["mpc"].ToString().Replace(',', '.') + "'" +
                        ")";

                        NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                    }

                    //nonqueryCommand.CommandText = "INSERT INTO faktura_stavke (kolicina,vpc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi)" +
                    //    " VALUES (@kolicina,@vpc,@porez,@broj_fakture,@rabat,@id_skladiste,@sifra,@oduzmi)";
                    //nonqueryCommand.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 8);
                    //nonqueryCommand.Parameters.Add("@vpc", NpgsqlDbType.Money, 8);
                    //nonqueryCommand.Parameters.Add("@porez", NpgsqlDbType.Varchar, 10);
                    //nonqueryCommand.Parameters.Add("@broj_fakture", NpgsqlDbType.Bigint, 8);
                    //nonqueryCommand.Parameters.Add("@rabat", NpgsqlDbType.Varchar, 10);
                    //nonqueryCommand.Parameters.Add("@id_skladiste", NpgsqlDbType.Bigint, 8);
                    //nonqueryCommand.Parameters.Add("@sifra", NpgsqlDbType.Varchar, 20);
                    //nonqueryCommand.Parameters.Add("@oduzmi", NpgsqlDbType.Varchar, 2);
                    //nonqueryCommand.Prepare();
                    //for (int i = 0; i < DT.Rows.Count; i++)
                    //{
                    //    nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                    //    nonqueryCommand.Parameters["@vpc"].Value = Convert.ToDecimal( DT.Rows[i]["vpc"].ToString());
                    //    nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                    //    nonqueryCommand.Parameters["@broj_fakture"].Value = Convert.ToDecimal(DT.Rows[i]["broj_fakture"].ToString());
                    //    nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                    //    nonqueryCommand.Parameters["@id_skladiste"].Value = Convert.ToDecimal(DT.Rows[i]["id_skladiste"].ToString());
                    //    nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                    //    nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                    //    nonqueryCommand.ExecuteNonQuery();
                    //}
                }
                catch (NpgsqlException ex)
                {
                    classSQL.remoteConnection.Close();
                    return ex.ToString();
                }
                finally
                {
                    //classSQL.remoteConnection.Close();
                }
                return "";
            }
        }

        public static string UpdateStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();

                try
                {
                    nonqueryCommand.CommandText = "UPDATE faktura_stavke SET kolicina=@kolicina,vpc=@vpc,nbc=@nbc,porez=@porez,broj_fakture=@broj_fakture,rabat=@rabat,id_skladiste=@id_skladiste,id_stavka=@id_stavka,oduzmi=@oduzmi,porez_potrosnja=@porez_potrosnja WHERE sifra=@sifra AND broj_fakture=@broj_fakture";

                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_fakture", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
                    nonqueryCommand.Parameters.Add("@porez_potrosnja", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@mpc", SqlDbType.Decimal);

                    nonqueryCommand.Prepare();

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                        nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_fakture"].Value = DT.Rows[i]["broj_fakture"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                        nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                        nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"].ToString();
                        nonqueryCommand.Parameters["@mpc"].Value = DT.Rows[i]["mpc"].ToString().Replace(',', '.');
                        nonqueryCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
                finally
                {
                    classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                //REMOTE UPDATE//////////////////////////////////////////////////////////////

                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }

                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();

                try
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "UPDATE faktura_stavke SET " +
                            " kolicina='" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            "vpc='" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "nbc='" + DT.Rows[i]["nbc"].ToString() + "'," +
                            "porez='" + DT.Rows[i]["porez"].ToString() + "'," +
                            "broj_fakture='" + DT.Rows[i]["broj_fakture"].ToString() + "'," +
                            "rabat='" + DT.Rows[i]["rabat"].ToString() + "'," +
                            "id_skladiste='" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            "sifra='" + DT.Rows[i]["sifra"].ToString() + "'," +
                            "oduzmi='" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                            "porez_potrosnja='" + DT.Rows[i]["porez_potrosnja"].ToString() + "'," +
                            "mpc='" + DT.Rows[i]["mpc"].ToString().Replace(',', '.') + "'" +
                            " WHERE id_stavka='" + DT.Rows[i]["id_stavka"].ToString() + "' AND broj_fakture='" + DT.Rows[i]["broj_fakture"].ToString() + "'";

                        NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
                finally
                {
                    classSQL.connection.Close();
                }
                return "";
            }
        }
    }
}