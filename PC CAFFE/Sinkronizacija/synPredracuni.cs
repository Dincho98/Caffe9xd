using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synPredracuni
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        //private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPredracuni(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            string id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
            string id_blagajna = DTpostavke.Rows[0]["default_blagajna"].ToString();
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' AND id_ducan = '" + DTpostavke.Rows[0]["default_ducan"] + "' LIMIT 1", "postavke").Tables[0];
            DataTable DTnaplatni_uredaj = SqlPostgres.select("SELECT * FROM blagajna WHERE (aktivnost = '1' or aktivnost = 'DA') AND id_ducan = '" + DTpostavke.Rows[0]["default_ducan"] + "' AND id_blagajna = '" + DTpostavke.Rows[0]["default_blagajna"] + "' LIMIT 1;", "blagajna").Tables[0];

            string poslovnica = "1", naplatni_uredaj = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            if (DTnaplatni_uredaj.Rows.Count > 0)
            {
                naplatni_uredaj = DTnaplatni_uredaj.Rows[0]["ime_blagajne"].ToString();
            }

            string query = "";

            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati predračune?", "Slanje predračuna", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = ";";
            }
            else
            {
                query = " WHERE sp.novo='1';";
            }

            string sql = "SELECT sp.id, sp.broj, sp.id_stol,sp.sifra, sp.naziv AS naziv_artikla, z.oib AS oib_zaposlenik, ROUND(sp.mpc, 4) AS mpc, ROUND(sp.vpc, 4) AS nbc, ROUND(sp.porez, 4) AS porez, ROUND(porez_potrosnja, 4) AS porez_potrosnja, ROUND(sp.kolicina, 4) AS kolicina, datum_ispisa FROM svi_predracuni sp LEFT JOIN zaposlenici z ON sp.id_zaposlenik = z.id_zaposlenik" + query;

            DataTable DT = SqlPostgres.select(sql, "TABLE").Tables[0];

            //sql = "sql=";
            sql = "";
            int i = 0;
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum_ispisa"].ToString(), out datum);

                string helsql = "DELETE FROM predracuni WHERE sifra='" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'" +
                    " AND oib ='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "' and naplatni_uredaj = '" + naplatni_uredaj + "';~";
                if (!sql.Contains(helsql)) { sql += helsql; }
                sql += "INSERT INTO predracuni (oib, poslovnica, naplatni_uredaj, broj, id_stol, sifra, naziv_artikla, oib_zaposlenik, mpc, nbc, porez, porez_potrosnja, " +
                    "kolicina, datum_ispisa, novo) VALUES (" +
                    "'" + DTpodaci_tvrtka.Rows[0]["oib"] + "'," +
                    "'" + poslovnica + "'," +
                    "'" + naplatni_uredaj + "'," +
                    "'" + r["broj"].ToString().Replace(",", ".") + "'," +
                    "'" + r["id_stol"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + r["naziv_artikla"].ToString().Replace(",", ".") + "'," +
                    "'" + r["oib_zaposlenik"].ToString().Replace(",", ".") + "'," +
                    "'" + r["mpc"].ToString().Replace(",", ".") + "'," +
                    "'" + r["nbc"].ToString().Replace(",", ".") + "'," +
                    "'" + r["porez"].ToString().Replace(",", ".") + "'," +
                    "'" + r["porez_potrosnja"].ToString().Replace(",", ".") + "'," +
                    "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'0');~";

                if (i >= 500)
                {
                    sql += "Ł";
                }
                i++;
            }

            if (sql.Length > 4)
            {
                //sql = sql.Remove(sql.Length - 1);

                string[] a = sql.Split('Ł');
                bool bezGreske = true;
                foreach (string s in a)
                {
                    sql = s.Remove(s.Length - 1);
                    string[] odg = Pomagala.MyWebRequest("sql=" + sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                    if (odg[0] != "OK" || odg[1] != "1")
                    {
                        bezGreske = false;
                    }
                }
                if (bezGreske)
                {
                    foreach (DataRow dRow in DT.Rows)
                    {
                        string s = "update svi_predracuni set novo = '0' where id= '" + dRow["id"] + "';";
                        classSQL.update(s);
                    }
                }

                //ŠALJE NA WEB i DOBIVAM ODG

                //if (odg[0] == "OK" && odg[1] == "1")
                //{
                //    foreach (DataRow r in DT.Rows)
                //    {
                //        sql = "UPDATE svi_predracuni SET novo='0' " +
                //            "WHERE broj = '" + r["broj"].ToString() + "' AND sifra= '" + r["sifra"].ToString() + "';";

                //        SqlPostgres.update(sql);
                //    }
                //}
            }
        }

        #endregion Pošalji PODATKE

        #region PRIMI PODATKE

        //public void UzmiPodatkeSaWeba () {
        //    try {
        //        DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        //        DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
        //        DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

        //        poslovnica = "1";
        //        if (DTposlovnica.Rows.Count > 0) {
        //            poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
        //        }

        //        string sql_za_web = "sql=";
        //        string query = "SELECT * FROM predracuni WHERE (novo='1') " +
        //            "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
        //        DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

        //        if (DT.Rows.Count > 0) {
        //            string zadnji_broj = "", zadnja_godina = "", zadnja_poslovnica = "", is_kalkulacija = "", id_skladiste = "";
        //            foreach (DataRow r in DT.Rows) {
        //                DateTime d;
        //                DateTime.TryParse(r["datum"].ToString(), out d);

        //                if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && zadnja_poslovnica == r["poslovnica"].ToString()) {
        //                    zadnji_broj = r["broj"].ToString();
        //                    zadnja_godina = d.Year.ToString();
        //                    zadnja_poslovnica = r["poslovnica"].ToString();
        //                    is_kalkulacija = r["is_kalkulacija"].ToString();
        //                    id_skladiste = r["id_skladiste"].ToString();
        //                    //SpremiStavke(r);
        //                } else {
        //                    zadnji_broj = r["broj"].ToString();
        //                    zadnja_godina = d.Year.ToString();
        //                    zadnja_poslovnica = r["poslovnica"].ToString();
        //                    is_kalkulacija = r["is_kalkulacija"].ToString();
        //                    id_skladiste = r["id_skladiste"].ToString();

        //                    //SpremiHeader(r, DT);
        //                    //SpremiStavke(r);
        //                }

        //                DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

        //                query = "BEGIN; " +
        //                    "DELETE FROM zaposlenici WHERE id_zaposlenik='" + r["id_zaposlenik"].ToString() + "';" +
        //                        "INSERT INTO zaposlenici (" +
        //                        " id_zaposlenik,ime,kartica,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka,novo,editirano" +
        //                        ") VALUES (" +
        //                        " '" + r["id_zaposlenik"].ToString() + "'," +
        //                        " '" + r["ime"].ToString() + "'," +
        //                        " '" + r["kartica"].ToString() + "'," +
        //                        " '" + r["prezime"].ToString() + "'," +
        //                        " '" + r["id_grad"].ToString() + "'," +
        //                        " '" + r["adresa"].ToString() + "'," +
        //                        " '" + r["id_dopustenje"].ToString() + "'," +
        //                        " '" + r["oib_zaposlenika"].ToString() + "'," +
        //                        " '" + r["tel"].ToString() + "'," +
        //                        " '" + d.ToString("yyyy-MM-dd H:mm:ss") + "'," +
        //                        " '" + r["email"].ToString() + "'," +
        //                        " '" + r["mob"].ToString() + "'," +
        //                        " '" + r["aktivan"].ToString() + "'," +
        //                        " '" + r["zaporka"].ToString() + "','0','0'" +
        //                        " );" +
        //                        " COMMIT;";
        //                classSQL.insert(query);

        //                //**********************SQL WEB REQUEST***************************************
        //                sql_za_web += "UPDATE djelatnik SET novo='0', editirano='0' " +
        //                    " WHERE id_zaposlenik='" + r["id_zaposlenik"].ToString() + "'" +
        //                    " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
        //                //**********************SQL WEB REQUEST***************************************
        //            }

        //            if (sql_za_web.Length > 4) {
        //                sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
        //                string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
        //            }
        //        }

        //    } catch { }
        //}

        #endregion PRIMI PODATKE
    }
}