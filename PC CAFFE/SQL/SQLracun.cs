using Npgsql;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Globalization;

namespace PCPOS.SQL
{
    internal class SQLracun
    {
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }
                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,nbc,porez_potrosnja)" +
                        " VALUES (@broj_racuna,@sifra_robe,@id_skladiste,@mpc,@porez,@kolicina,@rabat,@vpc,@nbc,@porez_potrosnja)";

                    nonqueryCommand.Parameters.Add("@broj_racuna", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@sifra_robe", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int, 4);
                    nonqueryCommand.Parameters.Add("@mpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 5);
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 6);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 4);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money);
                    nonqueryCommand.Parameters.Add("@porez_potrosnja", SqlDbType.Money);
                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["dod"]) != 2)
                        {
                            nonqueryCommand.Parameters["@broj_racuna"].Value = DT.Rows[i]["broj_racuna"].ToString();
                            nonqueryCommand.Parameters["@sifra_robe"].Value = DT.Rows[i]["sifra_robe"].ToString();
                            nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                            nonqueryCommand.Parameters["@mpc"].Value = DT.Rows[i]["mpc"].ToString();
                            nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                            nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                            nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                            nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                            nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();
                            nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"].ToString();
                            nonqueryCommand.ExecuteNonQuery();
                        }
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
                    if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }

                    decimal mpc, porez, kolicina, rabat, vpc, nbc, porez_potrosnja;
                    int id_izradio = 0;

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (DT.Rows[i]["dod"]== null || DT.Rows[i]["dod"].ToString().Trim().Length == 0 || Convert.ToInt32(DT.Rows[i]["dod"]) != 2)
                        {
                            decimal.TryParse(DT.Rows[i]["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mpc);
                            decimal.TryParse(DT.Rows[i]["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out porez);
                            decimal.TryParse(DT.Rows[i]["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out kolicina);
                            decimal.TryParse(DT.Rows[i]["rabat"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out rabat);
                            decimal.TryParse(DT.Rows[i]["vpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out vpc);
                            decimal.TryParse(DT.Rows[i]["nbc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out nbc);
                            decimal.TryParse(DT.Rows[i]["porez_potrosnja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out porez_potrosnja);
                            if (Class.Postavke.is_beauty)
                                int.TryParse(DT.Rows[i]["id_izradio"].ToString(), out id_izradio);

                            string sql = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,nbc,porez_potrosnja,id_ducan,id_blagajna,godina" + (Class.Postavke.is_beauty ? ", id_izradio" : "") + ") VALUES (" +
                            "'" + DT.Rows[i]["broj_racuna"].ToString() + "'," +
                            "'" + DT.Rows[i]["sifra_robe"].ToString() + "'," +
                            "'" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            "'" + Math.Round(mpc, 2).ToString().Replace(".", ",") + "'," +
                            "'" + Math.Round(porez, 2).ToString().Replace(".", ",") + "'," +
                            "'" + Math.Round(kolicina, 3).ToString().Replace(".", ",") + "'," +
                            "'" + Math.Round(rabat, 5).ToString().Replace(".", ",") + "'," +
                            "'" + Math.Round(vpc, 3).ToString().Replace(",", ".") + "'," +
                            "'" + Math.Round(nbc, 4).ToString().Replace(",", ".") + "'," +
                            "'" + Math.Round(porez_potrosnja, 2).ToString().Replace(".", ",") + "'," +
                            "'" + DT.Rows[i]["id_ducan"].ToString() + "'," +
                            "'" + DT.Rows[i]["id_blagajna"].ToString() + "','" + DateTime.Now.Year.ToString() + "'";
                            if (Class.Postavke.is_beauty)
                                sql += ",'" + id_izradio.ToString() + "'";

                            sql += ")";

                            NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                            comm.ExecuteNonQuery();
                            id_izradio = 0;
                        }
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.remoteConnection.Close();
                    return ex.ToString();
                }
                finally
                {
                    //classSQL.connection.Close();
                }
                return "";
            }
        }

        public static string InsertNapomene(DataTable DT)
        {
            int count = DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int index = i;
                DataRow dr = DT.Rows[index];
                int dodI = 0;
                int.TryParse(dr["dod"].ToString().Trim(), out dodI);
                if (dodI == 0)
                {
                    int nextIndex = i + 1;
                    if (nextIndex < count)
                    {
                        for (int j = nextIndex; j < count; j++)
                        {
                            int dodJ = 0;
                            int.TryParse(DT.Rows[j]["dod"].ToString().Trim(), out dodJ);

                            if (dodJ == 2)
                            {
                                string s = DT.Rows[j]["dod"].ToString();
                                string sql = "";
                                //classSQL.insert(sql);
                            }
                            else if (dodJ == 0)
                            {
                                i = j - 1;
                                break;
                            }
                            i = j - 1;
                            //break;
                        }
                    }
                }
            }
            return "";
        }
    }
}