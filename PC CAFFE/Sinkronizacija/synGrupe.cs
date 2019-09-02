using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synGrupe
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synGrupe(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

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

            if (posalji_sve)
                if (MessageBox.Show("Želite poslati grupe?", "Slanje grupa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            string query = "";
            query = "SELECT * FROM grupa";

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            //ako mijenjam neku pojedinacnu grupu svim stavkama dodijelim editirano=1
            foreach (DataRow r in DT.Rows)
            {
                if (r["editirano"].ToString() == "True")
                {
                    tempDel = "DELETE FROM grupe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (posalji_sve)
                {
                    tempDel = "DELETE FROM grupe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            if (sql.Length > 4)
            {
                foreach (DataRow r in DT.Rows)
                {
                    sql += "INSERT INTO grupe (id_grupa, grupa, id_podgrupa, aktivnost, novo, editirano,oib,poslovnica) VALUES (" +
                            "'" + r["id_grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + r["grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + r["id_podgrupa"].ToString() + "'," +
                            "'" + r["aktivnost"].ToString().Replace(",", ".") + "'," +
                            "'0'," +
                            "'0','" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + poslovnica.Replace(";", "").Replace("~", "") + "');~";
                }

                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE grupa SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }

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
                string query = "SELECT * FROM grupe WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        //DateTime d;
                        //DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

                        query = "BEGIN; " +
                            "DELETE FROM grupa WHERE id_grupa='" + r["id_grupa"].ToString() + "';" +
                                "INSERT INTO grupa (" +
                                " id_grupa ,grupa, opis, id_podgrupa, aktivnost, editirano, novo" +
                                ") VALUES (" +
                                " '" + r["id_grupa"].ToString() + "'," +
                                " '" + r["grupa"].ToString() + "'," +
                                " ''," +
                                " '" + r["id_podgrupa"].ToString() + "'," +
                                " '" + r["aktivnost"].ToString() + "'," +
                                " '0'," +
                                " '0'" +
                                " );" +
                                " COMMIT;";
                        classSQL.insert(query);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE grupa SET novo='0', editirano='0' " +
                            " WHERE id_grupa='" + r["id_grupa"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                    }
                }
            }
            catch { }
        }

        #endregion PRIMI PODATKE
    }
}