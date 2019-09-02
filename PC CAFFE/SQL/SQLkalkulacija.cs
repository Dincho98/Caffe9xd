using Npgsql;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLkalkulacija
    {
        //Insert
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();

                try
                {
                    nonqueryCommand.CommandText = "INSERT  INTO kalkulacija_stavke (kolicina, fak_cijena,rabat,prijevoz,carina,marza_postotak,porez,posebni_porez,broj,sifra,vpc,id_skladiste,porez_potrosnja) VALUES (@kolicina, @fak_cijena,@rabat,@prijevoz,@carina,@marza_postotak,@porez,@posebni_porez,@broj,@sifra,@vpc,@id_skladiste,@porez_potrosnja)";

                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@fak_cijena", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@prijevoz", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@carina", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@marza_postotak", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@posebni_porez", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@broj", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int);
                    nonqueryCommand.Parameters.Add("@porez_potrosnja", SqlDbType.Decimal);

                    nonqueryCommand.Prepare();

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"];
                        nonqueryCommand.Parameters["@fak_cijena"].Value = DT.Rows[i]["fak_cijena"];
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"];
                        nonqueryCommand.Parameters["@prijevoz"].Value = DT.Rows[i]["prijevoz"];
                        nonqueryCommand.Parameters["@carina"].Value = DT.Rows[i]["carina"];
                        nonqueryCommand.Parameters["@marza_postotak"].Value = DT.Rows[i]["marza_postotak"];
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"];
                        nonqueryCommand.Parameters["@posebni_porez"].Value = DT.Rows[i]["posebni_porez"];
                        nonqueryCommand.Parameters["@broj"].Value = DT.Rows[i]["broj"];
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"];
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"];
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"];
                        nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"];
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
                        string sql = "INSERT INTO kalkulacija_stavke (kolicina, fak_cijena,rabat,prijevoz,carina,marza_postotak,porez,posebni_porez,broj,sifra,vpc,id_skladiste,porez_potrosnja) VALUES " +
                                     " (" +
                                     "'" + DT.Rows[i]["kolicina"].ToString() + "'," +
                                     "'" + DT.Rows[i]["fak_cijena"].ToString().Replace(".", ",") + "'," +
                                     "'" + DT.Rows[i]["rabat"].ToString() + "'," +
                                     "'" + DT.Rows[i]["prijevoz"].ToString().Replace(".", ",") + "'," +
                                     "'" + DT.Rows[i]["carina"].ToString().Replace(".", ",") + "'," +
                                     "'" + DT.Rows[i]["marza_postotak"].ToString() + "'," +
                                     "'" + DT.Rows[i]["porez"].ToString() + "'," +
                                     "'" + DT.Rows[i]["posebni_porez"].ToString().Replace(".", ",") + "'," +
                                     "'" + DT.Rows[i]["broj"].ToString() + "'," +
                                     "'" + DT.Rows[i]["sifra"].ToString() + "'," +
                                     "'" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                                     "'" + DT.Rows[i]["id_skladiste"].ToString().Replace(".", ",") + "'," +
                                     "'" + DT.Rows[i]["porez_potrosnja"].ToString().Replace(",", ".") + "'" +
                                     ")";

                        NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
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

        //Update
        public static string UpdateStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();

                try
                {
                    nonqueryCommand.CommandText = "UPDATE kalkulacija_stavke SET kolicina=@kolicina, fak_cijena=@fak_cijena,rabat=@rabat,prijevoz=@prijevoz,carina=@carina,marza_postotak=@marza_postotak,porez=@porez,posebni_porez=@posebni_porez,broj=@broj,sifra=@sifra,vpc=@vpc,id_skladiste=@id_skladiste,porez_potrosnja=@porez_potrosnja WHERE broj=@where_broj AND id_skladiste=@id_skladiste_staro AND sifra=@sifra";

                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@fak_cijena", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@prijevoz", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@carina", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@marza_postotak", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@posebni_porez", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@broj", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@where_broj", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int);
                    nonqueryCommand.Parameters.Add("@id_skladiste_staro", SqlDbType.Int);
                    nonqueryCommand.Parameters.Add("@porez_potrosnja", SqlDbType.Int);

                    nonqueryCommand.Prepare();

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"];
                        nonqueryCommand.Parameters["@fak_cijena"].Value = DT.Rows[i]["fak_cijena"];
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"];
                        nonqueryCommand.Parameters["@prijevoz"].Value = DT.Rows[i]["prijevoz"];
                        nonqueryCommand.Parameters["@carina"].Value = DT.Rows[i]["carina"];
                        nonqueryCommand.Parameters["@marza_postotak"].Value = DT.Rows[i]["marza_postotak"];
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"];
                        nonqueryCommand.Parameters["@posebni_porez"].Value = DT.Rows[i]["posebni_porez"];
                        nonqueryCommand.Parameters["@broj"].Value = DT.Rows[i]["broj"];
                        nonqueryCommand.Parameters["@where_broj"].Value = DT.Rows[i]["where_broj"];
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"];
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"];
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"];
                        nonqueryCommand.Parameters["@id_skladiste_staro"].Value = DT.Rows[i]["id_skladiste_staro"];
                        nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"];
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
                        string sql = "UPDATE kalkulacija_stavke SET " +
                            " kolicina='" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            " fak_cijena='" + DT.Rows[i]["fak_cijena"].ToString().Replace(".", ",") + "'," +
                            " rabat='" + DT.Rows[i]["rabat"].ToString() + "'," +
                            " prijevoz='" + DT.Rows[i]["prijevoz"].ToString().Replace(".", ",") + "'," +
                            " carina='" + DT.Rows[i]["carina"].ToString().Replace(".", ",") + "'," +
                            " vpc='" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            " marza_postotak='" + DT.Rows[i]["marza_postotak"].ToString() + "'," +
                            " porez='" + DT.Rows[i]["porez"].ToString() + "'," +
                            " posebni_porez='" + DT.Rows[i]["posebni_porez"].ToString().Replace(".", ",") + "'," +
                            " broj='" + DT.Rows[i]["broj"].ToString() + "'," +
                            " sifra='" + DT.Rows[i]["sifra"].ToString() + "'," +
                            " id_skladiste='" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            " porez_potrosnja='" + DT.Rows[i]["porez_potrosnja"].ToString() + "'" +
                            " WHERE broj='" + DT.Rows[i]["where_broj"].ToString() + "' AND id_skladiste='" + DT.Rows[i]["id_skladiste_staro"].ToString() + "' AND sifra='" + DT.Rows[i]["sifra"].ToString() + "'";

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