using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synBlagajnickiIzvjestaj
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synBlagajnickiIzvjestaj(bool _posalji_sve)
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

            string query = "";

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati blagajnički izvještaj?", "Slanje blagajničkog izvještaja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = " WHERE datum >= '" + DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd H:mm:ss") + "' order by datum asc;";
            }
            else
            {
                filter = "  WHERE (blagajnicki_izvjestaj.editirano='1' OR blagajnicki_izvjestaj.novo='1') order by datum asc;";
            }
            //************************************************************************************

            query = "SELECT * FROM blagajnicki_izvjestaj" + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM blagajnicki_izvjestaj WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "' AND id_blagajnicki_izvjestaj='" + r["id"].ToString() + "';~";
                }
                else if (r["editirano"].ToString().ToUpper() == "TRUE" || r["novo"].ToString().ToUpper() == "TRUE")
                {
                    tempDel = "DELETE FROM blagajnicki_izvjestaj WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "' AND id_blagajnicki_izvjestaj='" + r["id"].ToString() + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
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
                    DateTime dat;
                    DateTime.TryParse(r["datum"].ToString(), out dat);

                    sql += "INSERT INTO blagajnicki_izvjestaj (id_blagajnicki_izvjestaj, datum, dokumenat, oznaka_dokumenta, uplaceno, izdatak, poslovnica, oib, novo, editirano, id_partner) VALUES (" +
                        "'" + r["id"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["dokumenat"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["oznaka_dokumenta"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["uplaceno"].ToString().Replace(";", "").Replace("~", "").Replace(",", ".") + "'," +
                        "'" + r["izdatak"].ToString().Replace(";", "").Replace("~", "").Replace(",", ".") + "'," +
                        "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "").Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'0'," +
                        (r["id_partner"] == null || r["id_partner"].ToString().Length == 0 ? "NULL" : r["id_partner"].ToString()) +
                        ");~";
                }

                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    if (posalji_sve)
                    {
                        sql = "UPDATE blagajnicki_izvjestaj SET editirano='0', novo='0' WHERE datum<'" + DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd H:mm:ss") + "';";
                        SqlPostgres.update(sql);
                    }
                    else
                    {
                        foreach (DataRow dRow in DT.Rows)
                        {
                            sql = "UPDATE blagajnicki_izvjestaj SET editirano='0', novo='0' WHERE id = '" + dRow["id"] + "';";
                            SqlPostgres.update(sql);
                        }
                    }
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
                string query = "SELECT * FROM blagajnicki_izvjestaj WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                List<string> ListaInseranihVrijednosti = new List<string>();

                if (DT.Rows.Count > 0)
                {
                    decimal izdatak = 0;
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal.TryParse(r["izdatak"].ToString().Replace(".", ","), out izdatak);
                        string id_blagajnicki_izvjestaj = "";

                        if (r["novo"].ToString() == "1")
                        {
                            DateTime Datum;
                            DateTime.TryParse(r["datum"].ToString(), out Datum);
                            //yyyy-MM-dd H:mm:ss
                            decimal uplaceno = 0, isplaceno = 0;
                            decimal.TryParse(r["uplaceno"].ToString().Replace(".", ","), out uplaceno);
                            decimal.TryParse(r["izdatak"].ToString().Replace(".", ","), out isplaceno);

                            query = "BEGIN; " +
                                    "INSERT INTO blagajnicki_izvjestaj (datum,dokumenat, oznaka_dokumenta,uplaceno,izdatak,novo,editirano, id_partner) VALUES " +
                                    "(" +
                                        "'" + Datum.ToString("yyyy-MM-dd") + "'," +
                                        "'" + r["dokumenat"].ToString() + "'," +
                                        "'" + r["oznaka_dokumenta"].ToString() + "'," +
                                        "'" + uplaceno.ToString().Replace(",", ".") + "'," +
                                        "'" + isplaceno.ToString().Replace(",", ".") + "'," +
                                        "'0','0', " +
                                        (r["id_partner"] == null || r["id_partner"].ToString().Length == 0 ? "NULL" : r["id_partner"].ToString()) +
                                    "); " +
                                    "SELECT id FROM blagajnicki_izvjestaj ORDER BY id DESC LIMIT 1; " +
                                    "COMMIT;";
                            DataTable DTzadnjiID = classSQL.select(query, "id").Tables[0];
                            if (DTzadnjiID.Rows.Count > 0)
                            {
                                id_blagajnicki_izvjestaj = DTzadnjiID.Rows[0][0].ToString();
                                ListaInseranihVrijednosti.Add(id_blagajnicki_izvjestaj);
                            }
                        }
                        else
                        {
                            id_blagajnicki_izvjestaj = r["id_blagajnicki_izvjestaj"].ToString();
                            string[] docs = { "UPLATA UTRŠKA", "ISPLATA PO GOT RN", "POLOG ZAJMA" };
                            string dok = r["dokumenat"].ToString();
                            if (docs.Contains(dok))
                            {
                                query = @"BEGIN;
    UPDATE blagajnicki_izvjestaj
    SET izdatak='" + izdatak.ToString().Replace(",", ".") + @"',
    oznaka_dokumenta = '" + r["oznaka_dokumenta"].ToString() + @"',
    id_partner = " + (r["id_partner"] == null || r["id_partner"].ToString().Length == 0 ? "NULL" : r["id_partner"].ToString()) + @",
    editirano = '0', novo = '0'
    WHERE id='" + r["id_blagajnicki_izvjestaj"].ToString() + @"';
COMMIT;";
                                classSQL.update(query);
                            }
                        }

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE blagajnicki_izvjestaj SET novo='0', editirano='0',id_blagajnicki_izvjestaj='" + id_blagajnicki_izvjestaj + "' " +
                            " WHERE id='" + r["id"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                        if (odg[1] == "0")
                        {
                            foreach (string id in ListaInseranihVrijednosti)
                            {
                                classSQL.delete("DELETE FROM blagajnicki_izvjestaj WHERE id='" + id + "';");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        #endregion PRIMI PODATKE
    }
}