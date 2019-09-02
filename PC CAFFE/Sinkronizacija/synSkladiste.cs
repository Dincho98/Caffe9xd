using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synSkladiste
    {
        private pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;

        //private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synSkladiste(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        //OVO BUDU SKLADIŠTA

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
            query = "SELECT * FROM skladiste WHERE editirano='1';";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati skladišta?", "Slanje skladišta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT * FROM skladiste;";
            }

            //query = "SELECT * FROM skladiste;";

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            //ako mijenjam neku pojedinacnu grupu svim stavkama dodijelim editirano=1
            foreach (DataRow r in DT.Rows)
            {
                if (r["editirano"].ToString() == "True")
                {
                    tempDel = "DELETE FROM skladiste WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (posalji_sve)
                {
                    tempDel = "DELETE FROM skladiste WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
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
                    string aktivnost = "0";
                    if (r["aktivnost"].ToString() == "DA")
                    {
                        aktivnost = "1";
                    }

                    sql += "INSERT INTO skladiste (id_skladiste_postgres, skladiste, id_grad, id_zemlja, aktivnost, editirano,oib,poslovnica,datum_syn) VALUES (" +
                            "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + r["skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'1'," +
                            "'60'," +
                            "'" + aktivnost + "'," +
                            "'0'," +
                            "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                            "NOW());~";
                }

                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE skladiste SET editirano='0';";
                    SqlPostgres.update(sql);
                }
            }
        }
    }
}