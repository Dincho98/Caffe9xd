using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synNormativ
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synNormativ(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

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

            string query = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati normative?", "Slanje normativa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT * FROM caffe_normativ;";
            }
            else
            {
                query = "SELECT * FROM caffe_normativ WHERE editirano='1' OR novo='1';";
            }

            DataTable DTprovjera = SqlPostgres.select(query, "TABLE").Tables[0];

            DataTable DT = new DataTable();

            string sql = "sql=";
            string tempDel = "";

            if (DTprovjera.Rows.Count > 0)
            {
                DT = SqlPostgres.select("SELECT * FROM caffe_normativ;", "TABLE").Tables[0];
                sql += "DELETE FROM normativ WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
            }

            decimal k;
            foreach (DataRow r in DT.Rows)
            {
                decimal.TryParse(r["kolicina"].ToString(), out k);

                sql += "INSERT INTO normativ (sifra, sifra_normativ, kolicina, id_skladiste, editirano, novo,oib,poslovnica) VALUES (" +
                    "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["sifra_normativ"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + k.ToString().Replace(",", ".") + "'," +
                    "'" + r["id_skladiste"].ToString() + "'," +
                    "'0'," +
                    "'0','" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "','" + poslovnica + "');~";
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE caffe_normativ SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }

        #endregion POŠALJI PODATKE

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

                string query;

                query = "SELECT * FROM normativ WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";

                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    query = "SELECT * FROM normativ WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                    DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                    if (DT.Rows.Count > 0)
                    {
                        SqlPostgres.delete("DELETE FROM caffe_normativ;");
                        foreach (DataRow r in DT.Rows)
                        {
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
                            SqlPostgres.insert(query);
                        }

                        //**********************SQL WEB REQUEST*************************************************************************
                        sql_za_web += "UPDATE normativ SET novo='0', editirano='0' " +
                            " WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST*************************************************************************

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