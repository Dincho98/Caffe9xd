using Npgsql;
using System;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLotpremnica
    {
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }
                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT  INTO otpremnica_stavke (kolicina,vpc,nbc,porez,broj_otpremnice,rabat,id_skladiste,sifra_robe,godina_otpremnice)" +
                        " VALUES (@kolicina,@vpc,@nbc,@porez,@broj_otpremnice,@rabat,@id_skladiste,@sifra_robe,@godina_otpremnice)";
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 15);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_otpremnice", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int, 4);
                    nonqueryCommand.Parameters.Add("@sifra_robe", SqlDbType.NVarChar, 20);
                    //nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
                    nonqueryCommand.Parameters.Add("@godina_otpremnice", SqlDbType.NVarChar, 6);
                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                        nonqueryCommand.Parameters["@nbc"].Value = Convert.ToDouble(DT.Rows[i]["nbc"].ToString());
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_otpremnice"].Value = DT.Rows[i]["broj_otpremnice"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra_robe"].Value = DT.Rows[i]["sifra_robe"].ToString();
                        //nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                        nonqueryCommand.Parameters["@godina_otpremnice"].Value = DT.Rows[i]["godina_otpremnice"].ToString();

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
                    //classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT  INTO otpremnica_stavke (kolicina,vpc,porez,broj_otpremnice,rabat,id_skladiste,sifra_robe,godina_otpremnice)" +
                        " VALUES (@kolicina,@vpc,@porez,@broj_otpremnice,@rabat,@id_skladiste,@sifra_robe,@godina_otpremnice)";

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "INSERT  INTO otpremnica_stavke (kolicina,vpc,nbc,porez,broj_otpremnice,rabat,id_skladiste,sifra_robe,godina_otpremnice, porez_potrosnja)" +
                        " VALUES (" +
                        "'" + DT.Rows[i]["kolicina"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["nbc"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["porez"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["broj_otpremnice"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["rabat"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                        "'" + DT.Rows[i]["sifra_robe"].ToString() + "'," +
                        //"'" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                        "'" + DT.Rows[i]["godina_otpremnice"].ToString() + "'," +
                        "'" + DT.Rows[i]["porez_potrosnja"].ToString().Replace(",", ".") + "'" +
                        ")";

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
                    //classSQL.connection.Close();
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
                    nonqueryCommand.CommandText = "UPDATE otpremnica_stavke SET kolicina=@kolicina,vpc=@vpc,nbc=@nbc,porez=@porez,broj_otpremnice=@broj_otpremnice,rabat=@rabat,id_skladiste=@id_skladiste,sifra_robe=@sifra_robe,godina_otpremnice=@godina_otpremnice WHERE id_stavka=@id_stavka";
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 15);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_otpremnice", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int, 4);
                    nonqueryCommand.Parameters.Add("@sifra_robe", SqlDbType.NVarChar, 20);
                    //nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
                    nonqueryCommand.Parameters.Add("@godina_otpremnice", SqlDbType.NVarChar, 6);
                    nonqueryCommand.Parameters.Add("@id_stavka", SqlDbType.BigInt, 8);
                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                        nonqueryCommand.Parameters["@nbc"].Value = Convert.ToDouble(DT.Rows[i]["nbc"].ToString());
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_otpremnice"].Value = DT.Rows[i]["broj_otpremnice"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra_robe"].Value = DT.Rows[i]["sifra_robe"].ToString();
                        //nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                        nonqueryCommand.Parameters["@godina_otpremnice"].Value = DT.Rows[i]["godina_otpremnice"].ToString();
                        nonqueryCommand.Parameters["@id_stavka"].Value = DT.Rows[i]["id_stavka"].ToString();
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
                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "UPDATE otpremnica_stavke SET " +
                        " kolicina='" + DT.Rows[i]["kolicina"].ToString().Replace(",", ".") + "'," +
                        " vpc='" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                        " nbc='" + DT.Rows[i]["nbc"].ToString() + "'," +
                        " porez='" + DT.Rows[i]["porez"].ToString() + "'," +
                        " broj_otpremnice='" + DT.Rows[i]["broj_otpremnice"].ToString() + "'," +
                        " rabat='" + DT.Rows[i]["rabat"].ToString() + "'," +
                        " id_skladiste='" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                        " sifra_robe='" + DT.Rows[i]["sifra_robe"].ToString() + "'," +
                        //" oduzmi='" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                        " godina_otpremnice='" + DT.Rows[i]["godina_otpremnice"].ToString() + "' " +
                        " WHERE id_stavka='" + DT.Rows[i]["id_stavka"].ToString() + "'";

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