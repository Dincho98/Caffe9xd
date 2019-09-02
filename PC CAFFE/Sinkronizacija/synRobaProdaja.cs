using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synRobaProdaja
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private Until.FunkcijeRobno FunkcijeRobno = new Until.FunkcijeRobno();

        //private string Util.Korisno.domena_za_sinkronizaciju = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synRobaProdaja(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region SALJEM PODATKE

        public void Send()
        {
            try
            {
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                string query = "";
                if (posalji_sve)
                {
                    if (MessageBox.Show("Želite poslati repromaterijale?", "Slanje repromaterija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    query = "SELECT * FROM roba_prodaja";
                }
                else
                {
                    query = "SELECT * FROM roba_prodaja WHERE editirano='1' OR novo='1'";
                }

                DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

                poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string sql = "sql=";
                foreach (DataRow r in DT.Rows)
                {
                    int id_partner;
                    decimal mpc, kolicina_predracun, cijena2, upakiranju, brojcanik;

                    int.TryParse(r["id_partner"].ToString(), out id_partner);
                    decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                    decimal.TryParse(r["kolicina_predracun"].ToString().Replace(".", ","), out kolicina_predracun);
                    decimal.TryParse(r["cijena2"].ToString().Replace(".", ","), out cijena2);

                    decimal.TryParse(r["u_pakiranju"].ToString().Replace(".", ","), out upakiranju);
                    decimal.TryParse(r["brojcanik"].ToString().Replace(".", ","), out brojcanik);

                    sql += "DELETE FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND poslovnica='" + poslovnica.Replace(";", "").Replace("~", "") + "'" +
                        " AND id_skladiste = '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "';~" +
                        "" +
                        "INSERT INTO roba_prodaja (id_skladiste, kolicina, nc, vpc, sifra, porez_potrosnja, " +
                        "id_grupa, id_podgrupa, mjera, aktivnost, povratna_naknada, poticajna_naknada, ulazni_porez, " +
                        "izlazni_porez, naziv, mpc, id_partner, kolicina_predracun, cijena2, u_pakiranju, brojcanik, oib, poslovnica, novo, editirano) VALUES " +
                        "(" +
                        "'" + r["id_skladiste"].ToString() + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                        "'" + r["nc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["vpc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez_potrosnja"].ToString().Replace(",", ".") + "'," +
                        "'" + r["id_grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_podgrupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mjera"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["aktivnost"].ToString() + "'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                        "'" + r["poticajna_naknada"].ToString().Replace(",", ".") + "'," +
                        "'" + r["ulazni_porez"].ToString().Replace(",", ".") + "'," +
                        "'" + r["izlazni_porez"].ToString().Replace(",", ".") + "'," +
                        "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + mpc.ToString().Replace(",", ".") + "'," +
                        "'" + id_partner + "'," +
                        "'" + kolicina_predracun.ToString().Replace(",", ".") + "'," +
                        "'" + cijena2.ToString().Replace(",", ".") + "'," +
                        "'" + upakiranju.ToString().Replace(",", ".") + "'," +
                        "'" + brojcanik.ToString().Replace(",", ".") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'1'" +
                        ");~";
                }

                if (sql.Length > 4)
                {
                    sql = sql.Remove(sql.Length - 1);

                    //ŠALJE NA WEB i DOBIVAM ODG
                    string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                    if (odg[0] == "OK" && odg[1] == "1")
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            sql = "UPDATE roba_prodaja SET editirano='0', novo='0' " +
                               "WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "';";

                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion SALJEM PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            try
            {
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string sql_za_web = "sql=";
                string query = "SELECT * FROM roba_prodaja WHERE (novo='1' OR editirano='1') " +
                    " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal porez;
                        decimal.TryParse(r["ulazni_porez"].ToString().Replace(".", ","), out porez);

                        query = "BEGIN; " +
                            "DELETE FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "';" +
                                "INSERT INTO roba_prodaja (" +
                                        "id_skladiste,kolicina,nc,vpc,sifra,porez_potrosnja,id_grupa,id_podgrupa,mjera,aktivnost," +
                                        "povratna_naknada,poticajna_naknada,ulazni_porez,izlazni_porez,naziv,novo,editirano" +
                                        ") VALUES (" +
                                            "'" + r["id_skladiste"].ToString() + "'," +
                                            "'0'," +
                                            "'" + r["nc"].ToString().Replace(",", ".") + "'," +
                                            "'0'," +
                                            "'" + r["sifra"].ToString() + "'," +
                                            "'" + r["porez_potrosnja"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["id_grupa"].ToString() + "'," +
                                            "'" + r["id_podgrupa"].ToString() + "'," +
                                            "'" + r["mjera"].ToString() + "'," +
                                            "'1'," +
                                            "'" + r["povratna_naknada"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["poticajna_naknada"].ToString().Replace(".", ",") + "'," +
                                            "'" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + r["izlazni_porez"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["naziv"].ToString() + "'," +
                                            "'0','0'" +
                                            ");" +
                                " COMMIT;";
                        classSQL.insert(query);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE roba_prodaja SET novo='0', editirano='0' " +
                            " WHERE sifra='" + r["sifra"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                            " AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                    }

                    //*********PORAVNAJ SKLADIŠTE**********
                    FunkcijeRobno.PostaviStanjeSkladista();
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE
    }
}