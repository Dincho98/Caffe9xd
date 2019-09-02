using System;
using System.Data;
using System.Linq;

namespace PCPOS.Sinkronizacija
{
    internal class synnaplatniUredaji
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synnaplatniUredaji(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("select * from ducan where id_ducan = '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "';", "ducan").Tables[0];
            DataTable DTblagajna = SqlPostgres.select("SELECT id_blagajna, id_ducan, ime_blagajne, CASE WHEN aktivnost = '1' THEN TRUE ELSE FALSE END AS aktivnost, editirano, novo FROM blagajna WHERE id_ducan = '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "' and (editirano = '1' or novo = '1');", "postavke").Tables[0];

            if (DTblagajna == null || DTblagajna.Rows.Count == 0)
                return;

            string poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            try
            {
                string sql = "";
                string naplatni = (from r in DTblagajna.AsEnumerable() where r.Field<int>("id_blagajna") == Convert.ToInt32(DTpostavke.Rows[0]["default_blagajna"].ToString()) select r.Field<string>("ime_blagajne")).FirstOrDefault().ToString();

                foreach (DataRow dRow in DTblagajna.Rows)
                {
                    if (sql.Length > 0)
                    {
                        sql += @"
";
                    }
                    sql += @"delete from naplatni_uredaji where oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"' and poslovnica = '" + poslovnica + @"' and naplatni_uredaj = '" + dRow["ime_blagajne"].ToString() + @"';~
insert into naplatni_uredaji (oib, poslovnica, naplatni_uredaj, aktivnost, editirano, novo) values (
'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"',
'" + poslovnica + @"',
'" + dRow["ime_blagajne"].ToString() + @"',
" + dRow["aktivnost"] + @",
false, false);~";

                    if (DTpostavke.Rows[0]["default_kasa_fakture"].ToString() == dRow["id_blagajna"].ToString())
                    {
                        sql += @"
update postavke set naplatni_fakture = '" + dRow["ime_blagajne"].ToString() + @"' where oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"' and poslovnica = '" + poslovnica + @"';~
INSERT INTO postavke (oib, poslovnica, naplatni, naplatni_fakture, rad_sa_siframa, broj_redova_printera, printer_bluetooth, android)
SELECT * from (select '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"' as oib, '" + poslovnica + @"' as poslovnica, '" + naplatni + @"' as naplatni, '" + dRow["ime_blagajne"].ToString() + @"' as naplatni_fakture, '0' as rad_sa_siframa, '32' as broj_redova_printera, '0' as printer_bluetooth, '0' as android) AS tmp
WHERE not EXISTS (select id from postavke where oib = " + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @" and poslovnica = '" + poslovnica + @"') LIMIT 1;~";
                    }
                }

                sql = sql.Remove(sql.Length - 1);
                string[] odg = Pomagala.MyWebRequest("sql=" + sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    foreach (DataRow r in DTblagajna.Rows)
                    {
                        sql = "UPDATE blagajna SET editirano='0', novo='0' " +
                           "WHERE ime_blagajne ='" + r["ime_blagajne"].ToString() + "' AND id_ducan = '" + r["id_ducan"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
            }
            catch
            {
                //ako nije zakomentirano onda ne radi, hahha. a u qurac
                //MessageBox.Show(ex.Message);
            }

            //try {
            //    string sql_za_web = "sql=";

            //    string query = "SELECT * FROM poslovnice WHERE (novo='1' OR editirano='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'";

            //    if (posalji_sve) {
            //        if (MessageBox.Show("Želite poslati poslovnice?", "Slanje poslovnica", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
            //            return;
            //        }
            //        query = "SELECT * FROM poslovnice WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'";
            //    }

            //    DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");
            //    DT = classSQL.select(query, "poslovnice").Tables[0];
            //    if (DT.Rows.Count > 0) {
            //        foreach (DataRow r in DT.Rows) {
            //            //DateTime d;
            //            // DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

            //            query = "BEGIN; " +
            //                "DELETE FROM poslovnice WHERE fiskalna_oznaka_poslovnice='" + r["fiskalna_oznaka_poslovnice"].ToString() + "';" +
            //                    "INSERT INTO poslovnice (" +
            //                    " adresa,aktivno,fiskalna_oznaka_poslovnice,grad_opcina,iban,ispostava,knjigovodstvo,najam,naziv_poslovnice,oib,osoblje,podrucni_ured" +
            //                    ") VALUES (" +
            //                    " '" + r["adresa"].ToString() + "'," +
            //                    " '" + r["aktivno"].ToString() + "'," +
            //                    " '" + r["fiskalna_oznaka_poslovnice"].ToString() + "'," +
            //                    " '" + r["grad_opcina"].ToString() + "'," +
            //                    " '" + r["iban"].ToString() + "'," +
            //                    " '" + r["ispostava"].ToString() + "'," +
            //                    " '" + r["knjigovodstvo"].ToString().Replace(',', '.') + "'," +
            //                    " '" + r["najam"].ToString().Replace(',', '.') + "'," +
            //                    " '" + r["naziv_poslovnice"].ToString() + "'," +
            //                    " '" + r["oib"].ToString() + "'," +
            //                    " '" + r["osoblje"].ToString().Replace(',', '.') + "'," +
            //                    " '" + r["podrucni_ured"].ToString() + "'" +
            //                    " );" +
            //                    " COMMIT;";
            //            classSQL.insert(query);

            //            //OVAJ DIO NE KORISTIM JER NEMAM novo i editirano
            //            /*
            //            //**********************SQL WEB REQUEST***************************************
            //            sql_za_web += "UPDATE poslovnice SET novo='0', editirano='0' " +
            //                " WHERE fiskalna_oznaka_poslovnice='" + r["fiskalna_oznaka_poslovnice"].ToString() + "'" +
            //                " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
            //            //**********************SQL WEB REQUEST***************************************
            //             * */
            //        }

            //        /*
            //        if (sql_za_web.Length > 4)
            //        {
            //            sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
            //            string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4", Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
            //        }
            //         * */
            //    }

            //} catch { }
        }
    }
}