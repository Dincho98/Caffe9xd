using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synPotrosniMaterijal
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPotrosniMaterijal(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji PODATKE

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
                if (MessageBox.Show("Želite poslati potrošni materijal?", "Slanje potrošnog materijala", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT * FROM potrosni_materijal";
            }
            else
            {
                query = "SELECT * FROM potrosni_materijal WHERE novo='1'";
            }

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";
            /*
             "DELETE FROM potrosni_materijal WHERE broj='" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~"
             */

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM potrosni_materijal WHERE godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (r["novo"].ToString() == "1")
                {
                    tempDel = "DELETE FROM potrosni_materijal WHERE broj='" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                decimal kol, rabat, cijena, porez;
                DateTime dat;

                decimal.TryParse(r["kolicina"].ToString(), out kol);
                decimal.TryParse(r["rabat"].ToString(), out rabat);
                decimal.TryParse(r["cijena"].ToString(), out cijena);
                decimal.TryParse(r["porez"].ToString(), out porez);
                DateTime.TryParse(r["datum"].ToString(), out dat);

                sql += "INSERT INTO potrosni_materijal (id_partner,id_zaposlenik,broj,godina,datum,placanje,napomena,sifra,naziv,jmj,kolicina,porez,cijena,rabat,oib,poslovnica)" +
               " VALUES " +
               " (" +
               " '" + r["id_partner"].ToString() + "'," +
               " '" + r["id_zaposlenik"].ToString() + "'," +
               " '" + r["broj"].ToString() + "'," +
               " '" + r["godina"].ToString() + "'," +
               " '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'," +
               " '" + r["placanje"].ToString() + "'," +
               " '" + r["napomena"].ToString() + "'," +
               " '" + r["sifra"].ToString() + "'," +
               " '" + r["naziv"].ToString() + "'," +
               " '" + r["jmj"].ToString() + "'," +
               " '" + Math.Round(kol, 5).ToString().Replace(",", ".") + "'," +
               " '" + Math.Round(porez, 5).ToString().Replace(",", ".") + "'," +
               " '" + Math.Round(cijena, 5).ToString().Replace(",", ".") + "'," +
               " '" + Math.Round(rabat, 5).ToString().Replace(",", ".") + "'," +
               " '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
               " '" + poslovnica + "'" +
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
                        sql = "UPDATE potrosni_materijal SET novo='0' " +
                            "WHERE broj='" + r["broj"].ToString() + "' AND godina='" + r["godina"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
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
                string query = "SELECT * FROM potrosni_materijal WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                string tempDel = "", sql = "BEGIN;";

                foreach (DataRow r in DT.Rows)
                {
                    if (r["novo"].ToString() == "1" || r["editirano"].ToString() == "1")
                    {
                        tempDel = "DELETE FROM potrosni_materijal WHERE broj='" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "';";
                    }

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        int id_partner, broj, id_zaposlenik, godina;
                        decimal kolicina, porez, cijena, rabat;

                        int.TryParse(r["id_partner"].ToString(), out id_partner);
                        int.TryParse(r["broj"].ToString(), out broj);
                        int.TryParse(r["id_zaposlenik"].ToString(), out id_zaposlenik);
                        int.TryParse(r["godina"].ToString(), out godina);

                        //decimal.TryParse(r["nc"].ToString().Replace(".", ","), out nbc);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
                        decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["cijena"].ToString().Replace(".", ","), out cijena);
                        decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        sql += "INSERT INTO potrosni_materijal (id_partner, id_zaposlenik, broj, godina, " +
                            " datum, placanje, napomena, sifra, naziv, jmj," +
                            " kolicina, porez, cijena, rabat,novo) VALUES (" +
                             " '" + id_partner + "'," +
                             " '" + id_zaposlenik + "'," +
                             " '" + broj + "'," +
                             " '" + godina + "'," +
                             " '" + DateT.ToString("yyyy-MM-dd HH:mm") + "'," +
                             " '" + r["placanje"].ToString() + "'," +
                             " '" + r["napomena"].ToString() + "'," +
                             " '" + r["sifra"].ToString() + "'," +
                             " '" + r["naziv"].ToString() + "'," +
                             " '" + r["jmj"].ToString() + "'," +
                             " '" + kolicina.ToString().Replace(",", ".") + "'," +
                             " '" + porez.ToString().Replace(",", ".") + "'," +
                             " '" + cijena.ToString().Replace(",", ".") + "'," +
                             " '" + rabat.ToString().Replace(",", ".") + "'," +
                             " '0'" +
                             "); ";

                        //**********************SQL WEB REQUEST***************************************************************************
                        sql_za_web += "UPDATE potrosni_materijal SET novo='0',editirano='0'" +
                            " WHERE id='" + r["id"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                        //**********************SQL WEB REQUEST***************************************************************************
                    }

                    sql += " COMMIT;";

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                        if (odg[0] == "OK" && odg[1] == "1")
                        {
                            SqlPostgres.insert(sql);
                        }
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