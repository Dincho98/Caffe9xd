using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    class SQLusklada_skladista
    {
        public static string unesiStavkeUskladeRobe(DataTable dataTable)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed")
                {
                    classSQL.connection.Open();
                }
                SqlCeCommand sqlCeCommand = classSQL.connection.CreateCommand();
                try
                {
                    sqlCeCommand.CommandText = @"INSERT INTO usklada_robe_stavke(usklada_id,roba_id,nova_kolicina,stara_kolicina) VALUES(@usklada_id,@roba_id,@nova_kolicina,@stara_kolicina";
                    sqlCeCommand.Parameters.Add("@usklada_id", SqlDbType.Int);
                    sqlCeCommand.Parameters.Add("@roba_id", SqlDbType.Int);
                    sqlCeCommand.Parameters.Add("@nova_kolicina", SqlDbType.VarChar, 30);
                    sqlCeCommand.Parameters.Add("@stara_kolicina", SqlDbType.VarChar, 30);
                    sqlCeCommand.Prepare();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        sqlCeCommand.Parameters["@usklada_id"].Value = dataTable.Rows[i]["@usklada_id"].ToString();
                        sqlCeCommand.Parameters["@roba_id"].Value = dataTable.Rows[i]["@roba_id"].ToString();
                        sqlCeCommand.Parameters["@nova_kolicina"].Value = int.Parse(dataTable.Rows[i]["@nova_kolicina"].ToString());
                        sqlCeCommand.Parameters["@stara_kolicina"].Value = int.Parse(dataTable.Rows[i]["@stara_kolicina"].ToString());
                        sqlCeCommand.ExecuteNonQuery();
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
                if (classSQL.remoteConnection.State.ToString() == "Closed")
                {
                    classSQL.remoteConnection.Open();
                }
                NpgsqlCommand npgsqlCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string CommandText = @"INSERT INTO usklada_robe_stavke(usklada_id,roba_id,nova_kolicina,stara_kolicina) 
                                            VALUES('" + int.Parse(dataTable.Rows[i]["usklada_id"].ToString()) + "','"
                                                    + int.Parse(dataTable.Rows[i]["roba_id"].ToString()) + "','"
                                                    + dataTable.Rows[i]["nova_kolicina"].ToString() + "','"
                                                    + dataTable.Rows[i]["stara_kolicina"].ToString() + "')";
                        NpgsqlCommand comm = new NpgsqlCommand(CommandText, classSQL.remoteConnection);
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
        public static string unesiStavkePrimkeGeneriraneNaTemeljuUskladeSkladista(DataTable dataTable)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed")
                {
                    classSQL.connection.Open();
                }
                SqlCeCommand sqlCeCommand = classSQL.connection.CreateCommand();
                SqlCeCommand sqlceCommand = classSQL.connection.CreateCommand();
                try
                {
                    sqlCeCommand.CommandText = @"INSERT INTO primka_stavke(sifra,kolicina,nabavna_cijena,nabavni_iznos,iznos,broj_primke,id_skladiste,is_kalkulacija) VALUES (" +
                                               "@sifra,@kolicina,@nabavna_cijena,@nabavni_iznos,@iznos,@broj_primke,@skladiste_id,'false'";
                    sqlceCommand.CommandText = @"UPDATE roba_prodaja SET kolicina=@nova_kolicina WHERE sifra='@sifra'";
                    sqlCeCommand.Parameters.Add("@sifra", SqlDbType.VarChar);
                    sqlCeCommand.Parameters.Add("@kolicina", SqlDbType.Float);
                    sqlCeCommand.Parameters.Add("@nabavna_cijena", SqlDbType.Decimal);
                    sqlCeCommand.Parameters.Add("@nabavni_iznos", SqlDbType.Decimal);
                    sqlCeCommand.Parameters.Add("@iznos", SqlDbType.Decimal);
                    sqlCeCommand.Parameters.Add("@broj_primke", SqlDbType.VarChar);
                    sqlCeCommand.Parameters.Add("@skladiste_id", SqlDbType.Int);
                    sqlceCommand.Parameters.Add("@nova_kolicina", SqlDbType.VarChar);
                    sqlceCommand.Parameters.Add("@sifra", SqlDbType.VarChar);
                    sqlceCommand.Prepare();
                    sqlCeCommand.Prepare();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        sqlCeCommand.Parameters["@sifra"].Value = dataTable.Rows[i]["@sifra"].ToString();
                        sqlCeCommand.Parameters["@kolicina"].Value = float.Parse(dataTable.Rows[i]["@kolicina"].ToString());
                        sqlCeCommand.Parameters["@nabavna_cijena"].Value = Convert.ToDecimal(dataTable.Rows[i]["@nabavna_cijena"].ToString().Replace(",", "."));
                        sqlCeCommand.Parameters["@nabavni_iznos"].Value = Convert.ToDecimal(dataTable.Rows[i]["@nabavni_iznos"].ToString().Replace(",", "."));
                        sqlCeCommand.Parameters["@iznos"].Value = Convert.ToDecimal(dataTable.Rows[i]["@iznos"].ToString().Replace(",", "."));
                        sqlCeCommand.Parameters["@broj_primke"].Value = dataTable.Rows[0]["@broj_primke"].ToString();
                        sqlCeCommand.Parameters["@skladiste_id"].Value = Convert.ToInt32(dataTable.Rows[i]["@skladiste_id"].ToString());
                        sqlceCommand.Parameters["@sifra"].Value = dataTable.Rows[i]["@sifra"].ToString();
                        sqlceCommand.Parameters["@nova_kolicina"].Value = dataTable.Rows[i]["@nova_kolicina"].ToString();
                        sqlceCommand.ExecuteNonQuery();
                        sqlCeCommand.ExecuteNonQuery();
                        
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
                if (classSQL.remoteConnection.State.ToString() == "Closed")
                {
                    classSQL.remoteConnection.Open();
                }
                NpgsqlCommand npgsqlCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string commandText = @"INSERT INTO primka_stavke(sifra,kolicina,nabavna_cijena,nabavni_iznos,iznos,broj_primke,id_skladiste,is_kalkulacija) VALUES ('" +
                                             dataTable.Rows[i]["sifra"].ToString() + "','" + (dataTable.Rows[i]["kolicina"].ToString().Replace(",", ".")) + "','" +
                                             (dataTable.Rows[i]["nabavna_cijena"].ToString().Replace(",", ".")) + "','" +
                                             (dataTable.Rows[i]["nabavni_iznos"].ToString().Replace(",", ".")) + "','" +
                                             (dataTable.Rows[i]["iznos"].ToString().Replace(",", ".")) + "','" +
                                             dataTable.Rows[0]["broj_primke"].ToString() + "','" +
                                             Convert.ToInt32(dataTable.Rows[i]["skladiste_id"].ToString()) + "','false')";
                        string command="UPDATE roba_prodaja SET kolicina='"+ dataTable.Rows[i]["nova_kolicina"].ToString() +"' where sifra='"+dataTable.Rows[i]["sifra"].ToString() + "'";
                        NpgsqlCommand comm = new NpgsqlCommand(commandText, classSQL.remoteConnection);
                        NpgsqlCommand updateCommand = new NpgsqlCommand(command, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch(NpgsqlException ex)
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
        public static string UnesiStavkeIzdatniceGeneriraneNaTemeljuUskladeSkladista(DataTable dataTable)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed")
                {
                    classSQL.connection.Open();
                }
                SqlCeCommand sqlCeCommand = classSQL.connection.CreateCommand();
                try
                {
                    sqlCeCommand.CommandText = @"INSERT INTO izdatnica_stavke(sifra,kolicina,nbc,id_izdatnica) VALUES(@sifra_artikla,@kolicina,@nbc,@id_izdatnica)";
                    sqlCeCommand.Parameters.Add("@sifra_artikla", SqlDbType.VarChar);
                    sqlCeCommand.Parameters.Add("@kolicina", SqlDbType.Char);
                    sqlCeCommand.Parameters.Add("@nbc", SqlDbType.Decimal);
                    sqlCeCommand.Parameters.Add("@id_izdatnica", SqlDbType.Int);
                    sqlCeCommand.Prepare();
                    for(int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        sqlCeCommand.Parameters["@sifra_artikla"].Value = dataTable.Rows[i]["@sifra_artikla"].ToString();
                        sqlCeCommand.Parameters["@kolicina"].Value = dataTable.Rows[i]["@kolicina"].ToString();
                        sqlCeCommand.Parameters["@nbc"].Value = double.Parse(dataTable.Rows[i]["@nbc"].ToString().Replace(",", "."));
                        sqlCeCommand.Parameters["@id_izdatnica"].Value = int.Parse(dataTable.Rows[0]["@id_izdatnica"].ToString());
                        sqlCeCommand.ExecuteNonQuery();
                    }
                }
                catch(SqlCeException ex)
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
                if (classSQL.remoteConnection.State.ToString() == "Closed")
                {
                    classSQL.remoteConnection.Open();
                }
                NpgsqlCommand npgsqlCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for(int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string commandText = @"INSERT INTO izdatnica_stavke(sifra,kolicina,nbc,id_izdatnica) VALUES('" +
                                            dataTable.Rows[i]["sifra_artikla"].ToString() + "','"
                                            + dataTable.Rows[i]["kolicina"].ToString() + "','"
                                            + (dataTable.Rows[i]["nbc"].ToString().Replace(",", ".")) + "','"
                                            + int.Parse(dataTable.Rows[0]["id_izdatnica"].ToString()) + "')";
                        string command = "UPDATE roba_prodaja SET kolicina='" + dataTable.Rows[i]["nova_kolicina"].ToString() + "' where sifra='" + dataTable.Rows[i]["sifra_artikla"].ToString() + "'";
                        NpgsqlCommand comm = new NpgsqlCommand(commandText, classSQL.remoteConnection);
                        NpgsqlCommand comm2 = new NpgsqlCommand(command, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                        comm2.ExecuteNonQuery();
                    }
                }
                catch(NpgsqlException ex)
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
