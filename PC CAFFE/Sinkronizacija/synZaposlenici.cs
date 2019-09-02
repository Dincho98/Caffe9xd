using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synZaposlenici
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synZaposlenici(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string query = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati zaposlenike?", "Slanje zaposlenika", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                query = "SELECT * FROM zaposlenici";
            }
            else
            {
                query = "SELECT * FROM zaposlenici WHERE (editirano='1' OR novo='1')";
            }
            //****************************************************************************

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM djelatnik WHERE " +
                            " poslovnica='" + poslovnica.Replace(";", "").Replace("~", "") + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "';~";
                }
                else
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM djelatnik WHERE id_zaposlenik='" + r["id_zaposlenik"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica.Replace(";", "").Replace("~", "") + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum_rodenja"].ToString(), out datum);

                sql += "INSERT INTO djelatnik (id_zaposlenik, ime, prezime, id_grad, adresa," +
                    "id_dopustenje,oib_zaposlenika,tel,datum_rodenja,email,mob,aktivan,zaporka,kartica,oib,poslovnica,editirano,novo) VALUES (" +
                    "'" + r["id_zaposlenik"].ToString() + "'," +
                    "'" + r["ime"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["prezime"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["id_grad"].ToString() + "'," +
                    "'" + r["adresa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["id_dopustenje"].ToString() + "'," +
                    "'" + r["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["tel"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss").Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["email"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["mob"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["aktivan"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["zaporka"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["kartica"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                    "'0'," +
                    "'0');~";
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE zaposlenici SET editirano='0', novo='0';";
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
                string query = "SELECT * FROM djelatnik WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        DateTime d;
                        DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

                        query = "BEGIN; " +
                            "DELETE FROM zaposlenici WHERE id_zaposlenik='" + r["id_zaposlenik"].ToString() + "';" +
                                "INSERT INTO zaposlenici (" +
                                " id_zaposlenik,ime,kartica,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka,novo,editirano" +
                                ") VALUES (" +
                                " '" + r["id_zaposlenik"].ToString() + "'," +
                                " '" + r["ime"].ToString() + "'," +
                                " '" + r["kartica"].ToString() + "'," +
                                " '" + r["prezime"].ToString() + "'," +
                                " '" + r["id_grad"].ToString() + "'," +
                                " '" + r["adresa"].ToString() + "'," +
                                " '" + r["id_dopustenje"].ToString() + "'," +
                                " '" + r["oib_zaposlenika"].ToString() + "'," +
                                " '" + r["tel"].ToString() + "'," +
                                " '" + d.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                " '" + r["email"].ToString() + "'," +
                                " '" + r["mob"].ToString() + "'," +
                                " '" + r["aktivan"].ToString() + "'," +
                                " '" + r["zaporka"].ToString() + "','0','0'" +
                                " );" +
                                " COMMIT;";
                        classSQL.insert(query);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE djelatnik SET novo='0', editirano='0' " +
                            " WHERE id_zaposlenik='" + r["id_zaposlenik"].ToString() + "'" +
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