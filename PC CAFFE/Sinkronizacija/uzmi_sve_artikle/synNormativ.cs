using System.Data;

namespace PCPOS.Sinkronizacija.uzmi_sve_artikle
{
    internal class synNormativ
    {
        public bool uzmi_sve_artikle = false;
        private PCPOS.Until.classFukcijeZaUpravljanjeBazom baza = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");

        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

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

                string query = "SELECT * FROM normativ " +
                     "WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";

                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    classSQL.delete("DELETE FROM caffe_normativ");

                    foreach (DataRow r in DT.Rows)
                    {
                        //id	sifra	sifra_normativ	kolicina	id_skladiste	editirano	novo	oib	poslovnica
                        query = "BEGIN; " +
                                "INSERT INTO caffe_normativ " +
                                    "(sifra,sifra_normativ,kolicina,id_skladiste,editirano,novo) " +
                                    " VALUES " +
                                    " (" +
                                    "'" + r["sifra"].ToString() + "'," +
                                    "'" + r["sifra_normativ"].ToString() + "'," +
                                    "'" + r["kolicina"].ToString().Replace(".", ",") + "'," +
                                    "'" + r["id_skladiste"].ToString() + "','0','0'" +
                                    "); " +
                                " COMMIT;";
                        classSQL.insert(query);

                        //**********************SQL WEB REQUEST*************************************************************************
                        sql_za_web += "UPDATE normativ SET novo='0', editirano='0' " +
                            " WHERE sifra='" + r["sifra"].ToString() + "' AND sifra_normativ='" + r["sifra_normativ"].ToString() + "' " +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                        //**********************SQL WEB REQUEST*************************************************************************
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