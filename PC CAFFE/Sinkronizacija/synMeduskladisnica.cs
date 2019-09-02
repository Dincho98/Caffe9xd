using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synMeduskladisnica
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private string id_poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synMeduskladisnica(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            poslovnica = "1";
            id_poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati međuskladišnice?", "Slanje međuskladišnica", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = " WHERE medu_poslovnice.iz_poslovnice='" + poslovnica + "' AND medu_poslovnice.zakljucano::integer = 0;";
            }
            else
            {
                filter = " WHERE medu_poslovnice.novo_izskl='1' AND medu_poslovnice.iz_poslovnice='" + poslovnica + "' AND medu_poslovnice.zakljucano::integer = 0;";
            }
            //****************************************************************************

            string query = "SELECT medu_poslovnice.*,zaposlenici.oib AS oib_zaposlenika " +
                " FROM medu_poslovnice" +
                " LEFT JOIN zaposlenici ON medu_poslovnice.id_izradio = zaposlenici.id_zaposlenik " +
                "" + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (posalji_sve)
                {
                    tempDel = "DELETE FROM medu_poslovnice WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND iz_poslovnice='" + r["iz_poslovnice"] + "' AND u_poslovnicu = '" + r["u_poslovnicu"] + "' AND YEAR(datum)='" + datum.Year.ToString() + "' AND zakljucano = '0';~";
                }
                else
                {
                    if (r["novo_izskl"].ToString().ToLower() == "true")
                    {
                        tempDel = "DELETE FROM medu_poslovnice WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'  AND broj='" + r["broj"].ToString() + "'" +
                            " AND iz_poslovnice='" + r["iz_poslovnice"] + "' AND u_poslovnicu = '" + r["u_poslovnicu"] + "' and YEAR(datum)='" + datum.Year.ToString() + "' AND zakljucano = '0';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                if (r["sifra"].ToString() != "")
                {
                    decimal nbc, mpc, pdv, kol, pnp, povratna_naknada;

                    decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
                    decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                    decimal.TryParse(r["pdv"].ToString().Replace(".", ","), out pdv);
                    decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kol);
                    decimal.TryParse(r["pnp"].ToString().Replace(".", ","), out pnp);
                    decimal.TryParse(r["pp"].ToString().Replace(".", ","), out povratna_naknada);

                    DateTime DateT;
                    DateTime.TryParse(r["datum"].ToString(), out DateT);

                    sql += "INSERT INTO medu_poslovnice (sifra, nbc, mpc, pdv, kolicina, pnp, povratna_naknada, id_skladiste, broj, godina, " +
                    "datum, iz_poslovnice, u_poslovnicu, id_izradio, napomena, oib, oib_zaposlenika, novo_izskl, novo_uskl, zakljucano) VALUES (" +
                         " '" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + Math.Round(nbc, 3).ToString().Replace(",", ".") + "'," +
                         " '" + Math.Round(mpc, 3).ToString().Replace(",", ".") + "'," +
                         " '" + Math.Round(pdv, 2).ToString().Replace(",", ".") + "'," +
                         " '" + Math.Round(kol, 4).ToString().Replace(",", ".") + "'," +
                         " '" + Math.Round(pnp, 2).ToString().Replace(",", ".") + "'," +
                         " '" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'," +
                         " '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + r["broj"].ToString() + "'," +
                         " '" + r["godina"].ToString() + "'," +
                         " '" + DateT.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                         " '" + r["iz_poslovnice"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + r["u_poslovnicu"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + r["id_izradio"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                         " '0'," +
                         " '1'," +
                         " '" + r["zakljucano"].ToString().Replace(";", "").Replace("~", "") + "'" +
                         ");~";
                }
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
                        sql = "UPDATE medu_poslovnice SET novo_izskl='0', novo_uskl='0' " +
                            " WHERE broj='" + r["broj"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #endregion POŠALJI PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            string sql_za_web = "sql=";

            poslovnica = "1";
            id_poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            #region IZ POSLOVNICE

            string sql = "SELECT * FROM medu_poslovnice" +
                " WHERE  (novo_izskl='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND (iz_poslovnice='" + poslovnica + "');";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                sql = "BEGIN;";
                string tempDel = "";
                foreach (DataRow r in DT.Rows)
                {
                    tempDel = "DELETE FROM medu_poslovnice WHERE iz_poslovnice = '" + r["iz_poslovnice"] + "' and u_poslovnicu = '" + r["u_poslovnicu"] + "' AND godina='" + r["godina"].ToString() + "' AND broj='" + r["broj"].ToString() + "';";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }

                foreach (DataRow r in DT.Rows)
                {
                    if (r["sifra"].ToString() != "")
                    {
                        decimal nbc, mpc, pdv, kol, pnp, povratna_naknada;
                        decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
                        decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                        decimal.TryParse(r["pdv"].ToString().Replace(".", ","), out pdv);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kol);
                        decimal.TryParse(r["pnp"].ToString().Replace(".", ","), out pnp);
                        decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        sql += "INSERT INTO medu_poslovnice (sifra,nbc,mpc,pdv,kolicina,pnp,pp,id_skladiste,broj,godina," +
                        "datum,iz_poslovnice,u_poslovnicu,id_izradio,napomena,novo_izskl,novo_uskl, zakljucano) VALUES (" +
                             " '" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + Math.Round(nbc, 3).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(mpc, 3).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(pdv, 2).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(kol, 4).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(pnp, 2).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'," +
                             " '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["broj"].ToString() + "'," +
                             " '" + r["godina"].ToString() + "'," +
                             " '" + DateT.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                             " '" + r["iz_poslovnice"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["u_poslovnicu"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["id_izradio"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '0'," +
                             " '0'," +
                             " '" + r["zakljucano"].ToString().Replace(";", "").Replace("~", "") + "'" +
                             ");";
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE medu_poslovnice SET novo_izskl='0'" +
                        " WHERE id='" + r["id"].ToString() + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                sql += " COMMIT;";
                classSQL.insert(sql);
                Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
                robno.PostaviStanjeSkladista();
            }

            #endregion IZ POSLOVNICE

            #region U POSLOVNICU

            sql = "SELECT * FROM medu_poslovnice" +
                " WHERE  (novo_uskl='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND (u_poslovnicu='" + poslovnica + "');";
            DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string tempDel = "";
                sql = "BEGIN;";

                foreach (DataRow r in DT.Rows)
                {
                    tempDel = "DELETE FROM medu_poslovnice WHERE u_poslovnicu='" + poslovnica + "' AND godina='" + r["godina"].ToString() + "' AND broj='" + r["broj"].ToString() + "';";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }

                foreach (DataRow r in DT.Rows)
                {
                    if (r["sifra"].ToString() != "")
                    {
                        decimal nbc, pdv, kol, pnp, povratna_naknada;
                        decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
                        decimal.TryParse(r["pdv"].ToString().Replace(".", ","), out pdv);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kol);
                        decimal.TryParse(r["pnp"].ToString().Replace(".", ","), out pnp);
                        decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        sql += "INSERT INTO medu_poslovnice (sifra,mpc,pdv,kolicina,pnp,pp,id_skladiste,broj,godina," +
                        "datum,iz_poslovnice,u_poslovnicu,id_izradio,napomena,novo_izskl,novo_uskl, zakljucano) VALUES (" +
                             " '" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + Math.Round(nbc, 3).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(pdv, 2).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(kol, 4).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(pnp, 2).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'," +
                             " '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["broj"].ToString() + "'," +
                             " '" + r["godina"].ToString() + "'," +
                             " '" + DateT.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                             " '" + r["iz_poslovnice"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["u_poslovnicu"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["id_izradio"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '0'," +
                             " '0'," +
                             " '" + r["zakljucano"].ToString().Replace(";", "").Replace("~", "") + "'" +
                             ");";
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE medu_poslovnice SET novo_uskl='0'" +
                        " WHERE id='" + r["id"].ToString() + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                sql += " COMMIT;";
                classSQL.insert(sql);

                Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
                robno.PostaviStanjeSkladista();
            }

            #endregion U POSLOVNICU

            if (sql_za_web.Length > 4)
            {
                sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
            }
        }

        #endregion PRIMI PODATKE
    }
}