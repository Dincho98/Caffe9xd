using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synPromjena_cijene
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPromjena_cijene(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region PRIMI PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            string poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati promjene cijena?", "Slanje promjene cijene", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = " WHERE promjena_cijene.zakljucano::integer = 0;";
            }
            else
            {
                filter = " WHERE (promjena_cijene.editirano='1' OR promjena_cijene.novo='1') and promjena_cijene.zakljucano::integer = 0;";
            }
            //****************************************************************************

            string query = "SELECT *, promjena_cijene.zakljucano::integer as zakljucan " +
"FROM promjena_cijene " +
"LEFT JOIN promjena_cijene_stavke ON promjena_cijene.broj = promjena_cijene_stavke.broj " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            if (posalji_sve)
            {
                tempDel = "DELETE FROM promjena_cijene WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "' AND YEAR(datum)='" + DateTime.Now.Year.ToString() + "' AND zakljucano = '0';~";
                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }
            else
            {
                foreach (DataRow r in DT.Rows)
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        DateTime dd;
                        DateTime.TryParse(r["date"].ToString(), out dd);

                        tempDel = "DELETE FROM promjena_cijene WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' AND kalkulacija='" + r["kalkulacija"].ToString() + "' AND YEAR(datum)='" + dd.Year.ToString() + "'" +
                            ";~";

                        if (!sql.Contains(tempDel))
                        {
                            sql += tempDel;
                        }
                    }
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime dd;
                DateTime.TryParse(r["date"].ToString(), out dd);

                sql += "INSERT INTO promjena_cijene (broj, sifra, stara_cijena, nova_cijena, datum, oib, poslovnica, porez, kalkulacija, skladiste, zakljucano) VALUES (" +
                    "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["stara_cijena"].ToString().Replace(".", ",") + "'," +
                    "'" + r["nova_cijena"].ToString().Replace(".", ",") + "'," +
                    //"'" + r["kolicina"].ToString().Replace(".", ",") + "'," +
                    "'" + dd.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
                    "'" + poslovnica + "'," +
                    "'" + r["pdv"].ToString() + "'," +
                    "'" + r["kalkulacija"].ToString() + "'," +
                    "'" + r["id_skladiste"].ToString() + "', " +
                    "'" + r["zakljucan"] + "');~";
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE promjena_cijene SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }

        #endregion PRIMI PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            string sql_za_web = "sql=";

            poslovnica = "1";
            string id_poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            string sql = "SELECT * FROM promjena_cijene " +
                " WHERE  (novo='1' OR editirano='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "' ORDER BY broj;";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                sql = "BEGIN;";
                int broj = 0, old_broj = -1, godina = 0, old_godina = -1, id_skladiste = 0, old_id_skladiste = -1, zakljucano = 0, kalkulacija = 0;
                foreach (DataRow r in DT.Rows)
                {
                    if (r["sifra"].ToString() != "")
                    {
                        decimal kolicina = 0, porez = 0, stara_cijena = 0, nova_cijena = 0;

                        int.TryParse(r["skladiste"].ToString(), out id_skladiste);
                        int.TryParse(r["broj"].ToString(), out broj);
                        int.TryParse(r["godina"].ToString(), out godina);
                        int.TryParse(r["zakljucano"].ToString(), out zakljucano);
                        int.TryParse(r["kalkulacija"].ToString(), out kalkulacija);

                        decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
                        decimal.TryParse(r["stara_cijena"].ToString().Replace('.', ','), out stara_cijena);
                        decimal.TryParse(r["nova_cijena"].ToString().Replace('.', ','), out nova_cijena);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        if (old_broj != broj || old_godina != godina || old_id_skladiste != id_skladiste)
                        {
                            sql += addHead(broj, DateT, id_skladiste, zakljucano);
                            sql += addChild(r["sifra"].ToString(), porez, broj, kolicina, stara_cijena, nova_cijena);
                        }
                        else
                        {
                            sql += addChild(r["sifra"].ToString(), porez, broj, kolicina, stara_cijena, nova_cijena);
                        }

                        old_broj = broj;
                        old_godina = godina;
                        old_id_skladiste = id_skladiste;
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE pocetno SET novo='0',editirano='0'" +
                        " WHERE id='" + r["id"].ToString() + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                sql += " COMMIT;";
                classSQL.insert(sql);

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                }

                //Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
                //robno.PostaviStanjeSkladista();
            }
        }

        private string addHead(int broj, DateTime dt, int id_skladiste, int zakljucano)
        {
            try
            {
                string s = "DELETE FROM promjena_cijene_stavke WHERE broj = '" + broj + "';\n";
                s += "DELETE FROM promjena_cijene WHERE broj = '" + broj + "' AND id_skladiste = '" + id_skladiste + "';\n";
                string sql = s + "INSERT INTO promjena_cijene (broj, date, id_izradio, id_skladiste, editirano, novo, zakljucano) " +
                "VALUES ( " +
                "'" + broj + "', " +
                "'" + dt + "', " +
                "'0', " +
                "'" + id_skladiste + "', " +
                "'0', " +
                "'0', " +
                "'" + zakljucano + "');";

                return sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private string addChild(string sifra, decimal pdv, int broj, decimal kolicina, decimal stara_cijena, decimal nova_cijena)
        {
            try
            {
                string sql = "INSERT INTO promjena_cijene_stavke (sifra, pdv, broj, stara_cijena, nova_cijena, postotak) " +
                    "VALUES ( " +
                    "'" + sifra + "', " +
                    "'" + pdv + "', " +
                    "'" + broj + "', " +
                    //"'" + kolicina + "', " +
                    "'" + stara_cijena + "', " +
                    "'" + nova_cijena + "', " +
                    "'" + ((nova_cijena / stara_cijena - 1) * 100).ToString("#0.00000") + "');";

                return sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        #endregion PRIMI PODATKE
    }
}