using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.SQL
{
    internal class ClassSkladiste
    {
        public static string GetAmount(string sifra, string skladiste, string oduzeti, string mnozeno_kolicinom, string funk)
        {
            double kol_skladiste = 0;
            double kol = 0;
            DataSet DSkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                kol_skladiste = 0;
                DataTable STrob = classSQL.select("SELECT nbc, mpc / (1 zbroj replace(porez, ',','.')::numeric /100) as vpc, porez, porez_potrosnja FROM roba WHERE sifra ='" + sifra + "'", "roba").Tables[0];
                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste, kolicina, nc, vpc, ulazni_porez, sifra, porez_potrosnja) VALUES ('" + skladiste + "', '0', '" + STrob.Rows[0]["nbc"].ToString().Replace(",", ".") + "', '" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "', '" + STrob.Rows[0]["porez"].ToString() + "', '" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + "')");
                }
                else
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste, kolicina, nc, vpc, ulazni_porez, sifra, porez_potrosnja) VALUES ('" + skladiste + "', '0', '" + STrob.Rows[0]["nbc"].ToString().Replace(",", ".") + "', '" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "', '" + STrob.Rows[0]["porez"].ToString() + "', '" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + "')");
                }
            }
            else
            {
                kol_skladiste = Convert.ToDouble(DSkol.Tables[0].Rows[0][0].ToString());
            }

            if (funk == "-")
            {
                kol = Convert.ToDouble(kol_skladiste) - (Convert.ToDouble(oduzeti) * Convert.ToDouble(mnozeno_kolicinom));
            }
            else
            {
                kol = Convert.ToDouble(kol_skladiste) + (Convert.ToDouble(oduzeti) * Convert.ToDouble(mnozeno_kolicinom));
            }
            return kol.ToString();
        }

        public static string SetBrojcanik(string sifra, string skladiste, string kolicina, string funk)
        {
            double kol = 0;

            try
            {
                DataTable DTnormativi = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra='" + sifra + "'", "caffe_normativ").Tables[0];
                if (DTnormativi.Rows.Count != 0)
                {
                    for (int i = 0; i < DTnormativi.Rows.Count; i++)
                    {
                        DataTable DTroba_prodaja = classSQL.select("SELECT brojcanik FROM roba_prodaja WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "'", "roba_prodaja").Tables[0];
                        if (DTroba_prodaja.Rows.Count != 0)
                        {
                            try
                            {
                                string brojcanik_k = DTroba_prodaja.Rows[0]["brojcanik"].ToString() == "" ? "0" : DTroba_prodaja.Rows[0]["brojcanik"].ToString();
                                kol = Convert.ToDouble(brojcanik_k) + Convert.ToDouble(kolicina);
                                classSQL.update("UPDATE roba_prodaja SET brojcanik='" + kol.ToString().Replace(",", ".") + "' WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "' AND id_skladiste='" + skladiste + "'");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
            catch { }

            return "";
        }

        public static string ZaVaranje(string sifra, string skladiste, string kolicina, string stol, string funk)
        {
            DataTable DTnormativi = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra='" + sifra + "'", "caffe_normativ").Tables[0];
            if (DTnormativi.Rows.Count != 0)
            {
                for (int i = 0; i < DTnormativi.Rows.Count; i++)
                {
                    try
                    {
                        decimal Kol = Convert.ToDecimal(kolicina) * Convert.ToDecimal(DTnormativi.Rows[i]["kolicina"].ToString());
                        classSQL.insert("INSERT INTO kucani_predracuni (sifra,datum,kolicina,id_zaposlenik,id_stol,id_skladiste) VALUES " +
                            "('" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','" + Kol.ToString().Replace(",", ".") + "'," +
                            "'" + Properties.Settings.Default.id_zaposlenik + "','" + stol + "','" + skladiste + "')");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return "";
        }

        public static string GetAmountCaffe(string sifra, string skladiste, string kolicina, string funk)
        {
            double kol = 0;

            try
            {
                DataTable DTnormativi = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra='" + sifra + "'", "caffe_normativ").Tables[0];
                if (DTnormativi.Rows.Count != 0)
                {
                    for (int i = 0; i < DTnormativi.Rows.Count; i++)
                    {
                        DataTable DTroba_prodaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "'", "roba_prodaja").Tables[0];
                        if (DTroba_prodaja.Rows.Count != 0)
                        {
                            if (funk == "-")
                            {
                                kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) - (Convert.ToDouble(DTnormativi.Rows[i]["kolicina"].ToString()) * Convert.ToDouble(kolicina));
                            }
                            else if (funk == "+")
                            {
                                kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) + (Convert.ToDouble(DTnormativi.Rows[i]["kolicina"].ToString()) * Convert.ToDouble(kolicina));
                            }
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "'");
                        }
                    }
                }
            }
            catch { }
            return kol.ToString();
        }

        public static string GetAmountPredracun(string sifra, string skladiste, string kolicina, string funk)
        {
            double kol = 0;
            // AND id_skladiste='" + skladiste + "'
            // AND id_skladiste='" + skladiste + "'
            DataTable DTnormativi = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra='" + sifra + "'", "caffe_normativ").Tables[0];
            if (DTnormativi.Rows.Count != 0)
            {
                for (int i = 0; i < DTnormativi.Rows.Count; i++)
                {
                    DataTable DTroba_prodaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "'", "roba_prodaja").Tables[0];
                    if (DTroba_prodaja.Rows.Count != 0)
                    {
                        double kol_pred = 0;
                        double.TryParse(DTroba_prodaja.Rows[0]["kolicina_predracun"].ToString(), out kol_pred);
                        if (funk == "-") { kol = kol_pred - (Convert.ToDouble(DTnormativi.Rows[i]["kolicina"].ToString()) * Convert.ToDouble(kolicina)); } else if (funk == "+") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) + (Convert.ToDouble(DTnormativi.Rows[i]["kolicina"].ToString()) * Convert.ToDouble(kolicina)); }
                        classSQL.update("UPDATE roba_prodaja SET kolicina_predracun='" + kol.ToString().Replace(",", ".") + "' WHERE sifra='" + DTnormativi.Rows[i]["sifra_normativ"].ToString() + "'");
                    }
                }
            }

            return kol.ToString();
        }

        public static string GetAmountCaffeDirect(string sifra, string skladiste, string kolicina, string funk)
        {
            double kol = 0;
            DataTable DTroba_prodaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja").Tables[0];
            if (DTroba_prodaja.Rows.Count > 0)
            {
                if (funk == "-") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) - Convert.ToDouble(kolicina); } else if (funk == "+") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) + Convert.ToDouble(kolicina); }
                classSQL.update("UPDATE roba_prodaja SET kolicina='" + Math.Round(kol, 4) + "' WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'");
            }

            //OVAJ KOD TREBA REALIZIRATI

            //else
            //{
            //    string sql = "INSERT INTO " + skladiste + "(id_skladiste,kolicina,nc,sifra,porez_potrosnja,id_grupa,id_podgrupa,"+
            //        "mjera,aktivnost,povratna_naknada,poticajna_naknada,ulazni_porez,izlazni_porez,naziv,mpc,id_partner) VALUES ("+
            //        "'" + skladiste + "'," +
            //        "'" + kolicina + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        "'" + skladiste + "'," +
            //        ")";
            //    classSQL.insert(sql);

            //    if (funk == "-") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) - Convert.ToDouble(kolicina); }
            //    else if (funk == "+") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) + Convert.ToDouble(kolicina); }
            //    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'");
            //}
            return kol.ToString();
        }
    }
}