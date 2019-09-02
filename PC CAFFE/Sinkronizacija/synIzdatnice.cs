using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synIzdatnice
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;

        //private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synIzdatnice(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji podatke

        public void Send()
        {
            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati sve izdatnice?", "Slanje otpisa robe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = " WHERE (iz.editirano='1' OR iz.novo='1')";
            }
            //****************************************************************************

            string query = string.Format(@"select '{1}' as oib, '{2}' as poslovnica, '{3}' as naplatni_uredaj, iz.id_izdatnica, iz.broj, iz.id_partner, iz.originalni_dokument, iz.id_izradio, iz.datum, iz.napomena, iz.id_skladiste, iz.godina::integer as godina, iz.id_mjesto, izs.sifra, izs.kolicina::numeric as kolicina, izs.nbc as nbc
from izdatnica iz
left join izdatnica_stavke izs on iz.id_izdatnica = izs.id_izdatnica
{0}
order by iz.broj asc;", filter, Class.PodaciTvrtka.oibTvrtke, Class.Postavke.default_poslovnica, Class.Postavke.maloprodaja_naplatni_uredaj);

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = string.Format(@"DELETE FROM izdatnice WHERE oib = '{0}' AND poslovnica = '{1}' and naplatni_uredaj = {2} AND id_skladiste = '{3}';~",
                            Class.PodaciTvrtka.oibTvrtke,
                            Class.Postavke.default_poslovnica,
                            Class.Postavke.maloprodaja_naplatni_uredaj,
                            r["id_skladiste"].ToString());
                }
                else
                {
                    if (Convert.ToBoolean(r["editirano"].ToString()))
                    {
                        tempDel = string.Format(@"DELETE FROM otpis_robe WHERE oib = '{0}' AND poslovnica = '{1}' AND naplatni_uredaj = {2} AND broj = '{3}' AND id_skladiste = '{4}';~",
                            Class.PodaciTvrtka.oibTvrtke,
                            Class.Postavke.default_poslovnica,
                            Class.Postavke.maloprodaja_naplatni_uredaj,
                            r["broj"].ToString(),
                            r["id_skladiste"].ToString());
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
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (r["sifra"].ToString() != "")
                {
                    string Partner = (r["id_partner"].ToString().Replace(",", ".") == "" ? "0" : r["id_partner"].ToString().Replace(",", "."));

                    int _godina = datum.Year;
                    int.TryParse(r["godina"].ToString(), out _godina);
                    if (_godina == 0)
                        _godina = datum.Year;

                    sql += string.Format(@"INSERT INTO izdatnice (oib, poslovnica, naplatni_uredaj, id_izdatnica, broj, id_partner, originalni_dokument, id_izradio, datum, napomena, id_skladiste, godina, id_mjesto, sifra, kolicina, nbc)
VALUES ('{0}', '{1}', '{2}', {3}, {4}, {5}, '{6}', {7}, '{8}', '{9}', {10}, {11}, {12}, '{13}', {14}, {15});~",
r["oib"],
r["poslovnica"],
r["naplatni_uredaj"],
r["id_izdatnica"],
r["broj"],
r["id_partner"],
r["originalni_dokument"],
r["id_izradio"],
datum.ToString("yyyy-MM-dd HH:mm:ss"),
r["napomena"],
r["id_skladiste"],
r["godina"],
r["id_mjesto"],
r["sifra"],
r["kolicina"].ToString().Replace(',', '.'),
r["nbc"].ToString().Replace(',', '.'));
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
                        sql = string.Format("UPDATE izdatnica SET editirano = '0', novo = '0' WHERE broj = '{0}';", r["broj"].ToString());
                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #endregion Pošalji podatke

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            try
            {
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' and id_ducan = " + DTpostavke.Rows[0]["default_ducan"] + " LIMIT 1", "postavke").Tables[0];

                //poslovnica = "1";
                //if (DTposlovnica.Rows.Count > 0)
                //{
                //    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                //}

                string sql_za_web = "sql=";
                string query = "SELECT * FROM otpis_robe WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + Class.Postavke.default_poslovnica + "' ORDER BY broj, poslovnica, id_skladiste, datum ASC;";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                string tempDel = "", sql = "BEGIN;\n";

                foreach (DataRow r in DT.Rows)
                {
                    if (r["novo"].ToString() == "1" || r["editirano"].ToString() == "1")
                    {
                        tempDel = "DELETE FROM povrat_robe_stavke WHERE broj = '" + r["broj"] + "';\n" +
                        "DELETE FROM povrat_robe WHERE broj='" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "' AND id_skladiste = '" + r["id_skladiste"] + "';\n";
                    }

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }

                if (DT.Rows.Count > 0)
                {
                    int id_partner, broj = 0, old_broj = -1, id_izradio, godina = 0, old_godina = -1, id_skladiste = 0, old_id_skladiste = -1;
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal kolicina, porez, rabat = 0, nbc = 0, vpc = 0, mpc = 0;

                        int.TryParse(r["id_skladiste"].ToString(), out id_skladiste);
                        int.TryParse(r["broj"].ToString(), out broj);
                        int.TryParse(r["godina"].ToString(), out godina);

                        int.TryParse(r["id_partner"].ToString(), out id_partner);
                        int.TryParse(r["id_izradio"].ToString(), out id_izradio);

                        decimal.TryParse(r["vpc"].ToString().Replace('.', ','), out vpc);
                        decimal.TryParse(r["pdv"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
                        decimal.TryParse(r["nbc"].ToString().Replace('.', ','), out nbc);
                        decimal.TryParse(r["mpc"].ToString().Replace('.', ','), out mpc);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        if (old_broj != broj || old_godina != godina || old_id_skladiste != id_skladiste)
                        {
                            sql += addHead(broj, DateT, id_partner, id_izradio, godina, id_skladiste, Convert.ToInt16(r["zakljucano"]));
                            sql += addChild(r["sifra"].ToString(), vpc, porez, rabat, broj, kolicina, nbc, mpc);
                        }
                        else
                        {
                            sql += addChild(r["sifra"].ToString(), vpc, porez, rabat, broj, kolicina, nbc, mpc);
                        }

                        old_broj = broj;
                        old_godina = godina;
                        old_id_skladiste = id_skladiste;

                        //**********************SQL WEB REQUEST***************************************************************************
                        sql_za_web += "UPDATE otpis_robe SET novo='0', editirano='0'" +
                            " WHERE id ='" + r["id"].ToString() + "'" +
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

        private string addHead(int broj, DateTime dt, int id_partner, int id_izradio, int godina, int id_skladiste, int zakljucano)
        {
            try
            {
                string sql = "INSERT INTO povrat_robe (broj, datum, id_odrediste, id_partner, id_izradio, godina, id_skladiste, editirano, novo, zakljucano) " +
                "VALUES ( " +
                "'" + broj + "', " +
                "'" + dt + "', " +
                "'" + 0 + "', " +
                "'" + id_partner + "', " +
                "'" + id_izradio + "', " +
                "'" + godina + "', " +
                "'" + id_skladiste + "', " +
                "'0', " +
                "'0', " +
                "'" + zakljucano + "');\n";

                return sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private string addChild(string sifra, decimal vpc, decimal pdv, decimal rabat, int broj, decimal kolicina, decimal nbc, decimal mpc)
        {
            try
            {
                string sql = "INSERT INTO povrat_robe_stavke (sifra, vpc, pdv, rabat, broj, kolicina, nbc, mpc, prodajna_cijena) " +
                    "VALUES ( " +
                    "'" + sifra + "', " +
                    "'" + vpc + "', " +
                    "'" + pdv + "', " +
                    "'" + rabat + "', " +
                    "'" + broj + "', " +
                    "'" + kolicina + "', " +
                    "'" + nbc + "', " +
                    "'" + mpc + "', " +
                    "'" + mpc.ToString().Replace(',', '.') + "');\n";

                return sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
    }
}