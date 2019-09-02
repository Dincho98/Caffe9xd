using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synPartner
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPartner(bool _posalji_sve)
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
                if (MessageBox.Show("Želite poslati partnere?", "Slanje partnera", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = "  WHERE (partners.editirano='1' OR partners.novo='1') ";
            }
            //************************************************************************************

            query = "SELECT * FROM partners" +
                " LEFT JOIN grad ON grad.id_grad = partners.id_grad " +
                " LEFT JOIN zemlja ON zemlja.id_zemlja = grad.id_drzava " +
                filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (r["editirano"].ToString() == "1" || r["novo"].ToString() == "1")
                {
                    tempDel = "DELETE FROM partneri WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "' AND id_partner='" + r["id_partner"].ToString() + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (posalji_sve)
                {
                    tempDel = "DELETE FROM partneri WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "';~";
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
                    //oib je oib tvrtke koja koristi nas program
                    //oib_tvrtke je oib partnera

                    sql += "INSERT INTO partneri (id_partner, ime_tvrtke, grad, adresa, oib_tvrtke," +
                        "drazava, ime, prezime, email, tel, editirano, novo, oib, poslovnica) VALUES (" +
                        "'" + r["id_partner"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ime_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                        "'" + r["grad"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["adresa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["zemlja"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ime"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["prezime"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["email"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["tel"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["editirano"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["novo"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica + "'" +
                        ");~";
                }

                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE partners SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
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
                string query = "SELECT * FROM partneri WHERE (novo='1' OR editirano='1') " +
                    "AND oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        //DateTime d;
                        //DateTime.TryParse(r["datum_rodenja"].ToString(), out d);

                        query = @"BEGIN;
INSERT INTO partners ( ime_tvrtke, id_grad, adresa, oib, id_zemlja, id_partner, ime, prezime, email, tel, novo, editirano )
VALUES (
'" + r["ime_tvrtke"].ToString() + @"',
1,
'" + r["adresa"].ToString() + @"',
'" + r["oib_tvrtke"].ToString() + @"',
1,
'" + r["id_partner"].ToString() + @"',
'" + r["ime"].ToString() + @"',
'" + r["prezime"].ToString() + @"',
'" + r["email"].ToString() + @"',
'" + r["tel"].ToString() + @"', 0, 0);
COMMIT;";
                        classSQL.insert(query);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE partneri SET novo = '0', editirano = '0' WHERE id = '" + r["id"].ToString() + "';~";
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