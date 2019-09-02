using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synOtpisRobe
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synOtpisRobe(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji podatke

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

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati otpis robe?", "Slanje otpisa robe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = " where povrat_robe.zakljucano::integer = 0";
            }
            else
            {
                filter = " WHERE (povrat_robe.editirano='1' OR povrat_robe.novo='1') and povrat_robe.zakljucano = '0'";
            }
            //****************************************************************************

            DataTable DTpr = classSQL.select("SELECT * FROM povrat_robe_stavke WHERE mpc='0' OR mpc is null OR mpc=''", "povrat_robe_stavke").Tables[0];
            foreach (DataRow r in DTpr.Rows)
            {
                DataTable DTr = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString() + "'", "roba_prodaja").Tables[0];
                decimal _mpc, _vpc, _rabat, _porez;
                decimal.TryParse(DTr.Rows[0]["nc"].ToString(), out _mpc);
                decimal.TryParse(DTr.Rows[0]["ulazni_porez"].ToString(), out _porez);

                _vpc = (_mpc / (1 + (_porez / 100)));
                _rabat = 0;

                classSQL.update("UPDATE povrat_robe_stavke SET mpc='" + Math.Round(_mpc, 3).ToString().Replace(".", ",") + "'" +
                    ",vpc='" + Math.Round(_vpc, 4).ToString().Replace(".", ",") + "'" +
                    ",rabat='" + Math.Round(_rabat, 5).ToString().Replace(".", ",") + "'" +
                    " WHERE id_stavka='" + r["id_stavka"].ToString() + "'");
            }

            string query = "SELECT " +
                " povrat_robe.broj," +
                " povrat_robe.datum," +
                " povrat_robe.id_odrediste," +
                " povrat_robe.id_partner," +
                " povrat_robe.mjesto_troska," +
                " povrat_robe.orginalni_dokument," +
                " povrat_robe.id_izradio," +
                " povrat_robe.napomena," +
                " povrat_robe.godina," +
                " povrat_robe_stavke.prodajna_cijena," +
                " povrat_robe.novo," +
                " povrat_robe.editirano," +
                " povrat_robe.id_skladiste," +
                " povrat_robe_stavke.sifra," +
                " povrat_robe_stavke.vpc," +
                " roba_prodaja.povratna_naknada," +
                " povrat_robe_stavke.pdv," +
                " povrat_robe_stavke.rabat," +
                " povrat_robe_stavke.broj," +
                " povrat_robe_stavke.kolicina," +
                " zaposlenici.oib AS oib_zaposlenika," +
                " povrat_robe_stavke.id_stavka," +
                " povrat_robe_stavke.nbc," +
                " povrat_robe_stavke.mpc," +
                " povrat_robe.zakljucano::integer as zakljucano" +
                " FROM povrat_robe" +
                " LEFT JOIN povrat_robe_stavke ON povrat_robe_stavke.broj=povrat_robe.broj" +
                " LEFT JOIN roba_prodaja ON povrat_robe_stavke.sifra = roba_prodaja.sifra" +
                " LEFT JOIN zaposlenici ON povrat_robe.id_izradio = zaposlenici.id_zaposlenik " +
                " " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM otpis_robe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            "AND poslovnica='" + poslovnica + "' AND godina='" + r["godina"].ToString() + "' AND zakljucano = '0';~";
                }
                else
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM otpis_robe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            "AND poslovnica='" + poslovnica + "' AND broj='" + r["broj"].ToString() + "' AND godina='" + r["godina"].ToString() + "' AND zakljucano = '0';~";
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
                    string Partner = r["id_partner"].ToString().Replace(",", ".") == "" ? "0" : r["id_partner"].ToString().Replace(",", ".");

                    int _godina = datum.Year;
                    int.TryParse(r["godina"].ToString(), out _godina);
                    if (_godina == 0)
                        _godina = datum.Year;

                    sql += "INSERT INTO otpis_robe (broj, datum, id_partner, id_izradio, godina, id_skladiste, sifra, vpc, pdv, " +
                           "kolicina, prodajna_cijena, id_stavka, nbc, mpc, rabat, novo, editirano, oib, oib_zaposlenika, povratna_naknada, poslovnica, zakljucano) VALUES (" +
                           "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                           "'" + Partner + "'," +
                           "'" + r["id_izradio"].ToString() + "'," +
                           "'" + _godina.ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["pdv"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["kolicina"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'" + r["prodajna_cijena"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'" + r["id_stavka"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["rabat"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'0'," +
                           "'0','" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
                           "'" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + poslovnica + "'," +
                            "'" + r["zakljucano"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "');~";
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
                        sql = "UPDATE povrat_robe SET editirano='0', novo='0' " +
                            "WHERE broj='" + r["broj"].ToString() + "';";

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

                poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string sql_za_web = "sql=";
                string query = "SELECT * FROM otpis_robe WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "' ORDER BY broj, poslovnica, id_skladiste, datum ASC;";
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