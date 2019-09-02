using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    public class synPoslovnice
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPoslovnice(bool _posalji_sve)
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

            try
            {
                string sql_za_web = "sql=";

                string query = "SELECT * FROM poslovnice WHERE (novo='1' OR editirano='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'";

                if (posalji_sve)
                {
                    if (MessageBox.Show("Želite poslati poslovnice?", "Slanje poslovnica", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    query = "SELECT * FROM poslovnice WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'";
                }

                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");
                //DT = classSQL.select(query, "poslovnice").Tables[0];
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        //DateTime d;
                        // DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

                        query = "BEGIN; " +
                            "DELETE FROM poslovnice WHERE fiskalna_oznaka_poslovnice='" + r["fiskalna_oznaka_poslovnice"].ToString() + "';" +
                                "INSERT INTO poslovnice (" +
                                " adresa,aktivno,fiskalna_oznaka_poslovnice,grad_opcina,iban,ispostava,knjigovodstvo,najam,naziv_poslovnice,oib,osoblje,podrucni_ured" +
                                ") VALUES (" +
                                " '" + r["adresa"].ToString() + "'," +
                                " '" + r["aktivno"].ToString() + "'," +
                                " '" + r["fiskalna_oznaka_poslovnice"].ToString() + "'," +
                                " '" + r["grad_opcina"].ToString() + "'," +
                                " '" + r["iban"].ToString() + "'," +
                                " '" + r["ispostava"].ToString() + "'," +
                                " '" + r["knjigovodstvo"].ToString().Replace(',', '.') + "'," +
                                " '" + r["najam"].ToString().Replace(',', '.') + "'," +
                                " '" + r["naziv_poslovnice"].ToString() + "'," +
                                " '" + r["oib"].ToString() + "'," +
                                " '" + r["osoblje"].ToString().Replace(',', '.') + "'," +
                                " '" + r["podrucni_ured"].ToString() + "'" +
                                " );" +
                                " COMMIT;";
                        //classSQL.insert(query);

                        //OVAJ DIO NE KORISTIM JER NEMAM novo i editirano
                        /*
                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE poslovnice SET novo='0', editirano='0' " +
                            " WHERE fiskalna_oznaka_poslovnice='" + r["fiskalna_oznaka_poslovnice"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                        //**********************SQL WEB REQUEST***************************************
                         * */
                    }

                    /*
                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4", Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                    }
                     * */
                }
            }
            catch { }
        }
    }
}