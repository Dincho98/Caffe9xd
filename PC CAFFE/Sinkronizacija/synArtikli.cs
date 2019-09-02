using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synArtikli
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synArtikli(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji PODATKE

        public void Send()
        {
            try
            {
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                string poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string query = "";
                if (posalji_sve)
                {
                    if (MessageBox.Show("Želite poslati robu?", "Slanje robe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    query = "SELECT * FROM roba order by id_roba asc;";
                }
                else
                {
                    query = "SELECT * FROM roba WHERE editirano='1' OR novo='1' order by id_roba asc;";
                }

                DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

                string sql = "";
                foreach (DataRow r in DT.Rows)
                {
                    decimal brojcanik;
                    decimal.TryParse(r["brojcanik"].ToString(), out brojcanik);

                    if (sql.Length > 0)
                    {
                        sql += @"
";
                    }
                    sql += @"DELETE FROM roba WHERE sifra='" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND oib='" + Class.PodaciTvrtka.oibTvrtke + "' AND poslovnica='" + poslovnica + @"';~
INSERT INTO roba (naziv, id_grupa, jm, mpc, sifra, ean, porez, pp, aktivnost, id_podgrupa,border_color,button_style, brojcanik, editirano, novo,oib,poslovnica,porezna_grupa)
VALUES (" +
                        "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                        "'" + r["id_grupa"].ToString() + "'," +
                        "'" + r["jm"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mpc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ean"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".") + "'," +
                        "'" + r["porez_potrosnja"].ToString().Replace(",", ".") + "'," +
                        "'" + r["aktivnost"].ToString() + "'," +
                        "'" + r["id_podgrupa"].ToString() + "'," +
                        "'" + r["border_color"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["button_style"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + brojcanik.ToString().Replace(",", ".") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + Class.PodaciTvrtka.oibTvrtke + "'," +
                        "'" + poslovnica + "','" + r["porezna_grupa"].ToString() + @"');~";
                }

                if (sql.Length > 4)
                {
                    sql = sql.Remove(sql.Length - 1);

                    //ŠALJE NA WEB i DOBIVAM ODG
                    string[] odg = Pomagala.MyWebRequest("sql=" + sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                    if (odg[0] == "OK" && odg[1] == "1")
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            sql = "UPDATE roba SET editirano='0', novo='0' " +
                                "WHERE sifra='" + r["sifra"].ToString() + "';";

                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion Pošalji PODATKE

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
                string query = "SELECT * FROM roba WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";

                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                //*************************LOCAL ARTIKLI******************************
                query = "SELECT * FROM roba;";
                DataTable DTlocal = SqlPostgres.select(query, "loc").Tables[0];
                decimal porez, pp;

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        DataRow[] row = DTlocal.Select("sifra='" + r["sifra"].ToString() + "'");
                        decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["pp"].ToString().Replace(".", ","), out pp);

                        if (row.Length == 0)
                        {
                            query = "INSERT INTO roba (" +
                                        "naziv,id_grupa,jm,mpc,sifra,ean,porez,porez_potrosnja,aktivnost,porezna_grupa,id_podgrupa,novo,editirano" +
                                        ") VALUES (" +
                                            "'" + r["naziv"].ToString() + "'," +
                                            "'" + r["id_grupa"].ToString() + "'," +
                                            "'" + r["jm"].ToString() + "'," +
                                            "'" + r["mpc"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["sifra"].ToString() + "'," +
                                            "'" + r["ean"].ToString() + "'," +
                                            "'" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + r["aktivnost"].ToString() + "'," +
                                            "'" + r["porezna_grupa"].ToString() + "'," +
                                            "'" + r["id_podgrupa"].ToString() + "'," +
                                            "'0'," +
                                            "'0'" +
                                            ");";
                            classSQL.insert(query);
                        }
                        else
                        {
                            query = "UPDATE roba SET " +
                                            " naziv='" + r["naziv"].ToString() + "'," +
                                            " id_grupa='" + r["id_grupa"].ToString() + "'," +
                                            " jm='" + r["jm"].ToString() + "'," +
                                            " mpc='" + r["mpc"].ToString().Replace(".", ",") + "'," +
                                            " porez='" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                            " porez_potrosnja='" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                            " aktivnost='" + r["aktivnost"].ToString() + "'," +
                                            " porezna_grupa='" + r["porezna_grupa"].ToString() + "'," +
                                            " id_podgrupa='" + r["id_podgrupa"].ToString() + "'" +
                                            " WHERE sifra='" + r["sifra"].ToString() + "';";
                            classSQL.insert(query);
                        }

                        //**********************SQL WEB REQUEST***************************************************************************
                        sql_za_web += "UPDATE roba SET novo='0', editirano='0' " +
                            " WHERE sifra='" + r["sifra"].ToString() + "' AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                    }
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE
    }
}