using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synInventura
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synInventura(bool _posalji_sve)
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

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati inventure?", "Slanje inventura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = ""; //  WHERE (inventura.broj_inventure='8' OR inventura.broj_inventure='9')
            }
            else
            {
                filter = " WHERE (inventura.editirano='1' OR inventura.novo='1')";
            }
            //****************************************************************************

            string query = "SELECT " +
                " inventura.broj_inventure," +
                " inventura.id_skladiste," +
                " inventura.datum," +
                " inventura.id_zaposlenik," +
                " inventura.godina," +
                " inventura_stavke.sifra_robe," +
                " inventura_stavke.jmj," +
                " inventura_stavke.kolicina," +
                " inventura_stavke.kolicina_koja_je_bila_na_skl," +
                " inventura_stavke.cijena," +
                " inventura_stavke.naziv," +
                " inventura_stavke.id_stavke," +
                " zaposlenici.oib AS oib_zaposlenika," +
                " inventura_stavke.porez," +
                " inventura.novo," +
                " inventura.editirano," +
                " inventura.is_pocetno_stanje," +
                " inventura_stavke.povratna_naknada," +
                " inventura_stavke.mpc," +
                " inventura.zakljucano," +
                " inventura.obrisano" +
                " FROM inventura" +
                " LEFT JOIN inventura_stavke ON inventura_stavke.broj_inventure = inventura.broj_inventure" +
                " LEFT JOIN roba_prodaja ON inventura_stavke.sifra_robe = roba_prodaja.sifra" +
                " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik " +
                " " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM inventura WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "';~";
                    //break;
                }
                else
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM inventura WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' AND broj_inventure='" + r["broj_inventure"].ToString() + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                    if (posalji_sve)
                        break;
                }
            }

            string is_pocetno_stanje = "0";
            int count_sql = 0;

            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (r["is_pocetno_stanje"].ToString().ToUpper() == "TRUE") { is_pocetno_stanje = "1"; } else { is_pocetno_stanje = "0"; }

                if (r["sifra_robe"].ToString() != "")
                {
                    //OVO KORISTIM ZBOG CELKE I TREALEGRI JER ONI NISU IMALI ZASEBNO POVRATNU NAKNADU NEGO URAČUNATU U CIJENI
                    string povratna_naknada = "0";
                    decimal mpc;
                    if (datum.Year == 2014 && (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "05593216962" || DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "77566209058"))
                    {
                        povratna_naknada = r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "");
                    }

                    decimal.TryParse(r["mpc"].ToString(), out mpc);

                    sql += "\nINSERT INTO inventura (broj_inventure,id_skladiste,datum,id_zaposlenik,godina,sifra_robe,jmj,kolicina,cijena," +
                        "naziv, porez, kolicina_koja_je_bila_na_skl, oib, poslovnica, novo, editirano, oib_zaposlenika, povratna_naknada, is_pocetno_stanje, mpc, zakljucano, obrisano) VALUES (" +
                        "'" + r["broj_inventure"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["id_zaposlenik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra_robe"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["jmj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["cijena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina_koja_je_bila_na_skl"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + povratna_naknada.Replace(",", ".") + "'," +
                        "'" + is_pocetno_stanje + "'," +
                        "'" + mpc.ToString().Replace(",", ".") + "'," +
                        "'" + r["zakljucano"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "', " +
                        "'" + r["obrisano"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'" +
                        ");";
                    sql += "~";

                    count_sql++;
                    if (count_sql >= 500)
                    {
                        sql += "Ł";
                        count_sql = 0;
                    }
                }
            }

            bool nemaGreske = true;
            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);
                //sql = sql.Remove(sql.Length - 1);

                string[] posaljiNaWeb = sql.Replace("sql=", "").Split('Ł');
                foreach (string posaljiNa in posaljiNaWeb)
                {
                    //posaljiNa = posaljiNa.Replace("sql=", "");
                    //ŠALJE NA WEB i DOBIVAM ODG
                    string stringZaWeb = posaljiNa.Replace("sql=", "");
                    stringZaWeb = stringZaWeb.Remove(stringZaWeb.Length - 1);
                    string[] odg = Pomagala.MyWebRequest("sql=" + stringZaWeb + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');

                    if (odg[0] != "OK" || odg[1] != "1")
                    {
                        nemaGreske = false;
                    }
                }

                if (nemaGreske)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        sql = "UPDATE inventura SET editirano='0', novo='0' " +
                            "WHERE broj_inventure='" + r["broj_inventure"].ToString() + "';";

                        SqlPostgres.update(sql);
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

                //PCPOS.Until.classFukcijeZaUpravljanjeBazom baza = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                string GodinaKojaSeKoristi = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();

                string sql_za_web = "sql=";
                string query = "SELECT * FROM inventura WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "' AND YEAR(datum)='" + GodinaKojaSeKoristi + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + GodinaKojaSeKoristi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal kolicina;
                        decimal cijena;
                        decimal povratna_naknada;
                        decimal kolicina_koja_je_bila_na_skl;

                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
                        decimal.TryParse(r["cijena"].ToString().Replace(".", ","), out cijena);
                        decimal.TryParse(r["kolicina_koja_je_bila_na_skl"].ToString().Replace(".", ","), out kolicina_koja_je_bila_na_skl);
                        decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

                        DateTime datum;
                        DateTime.TryParse(r["datum"].ToString(), out datum);

                        string sqlPG = "BEGIN; " +
                            " UPDATE inventura SET zakljucano='" + r["zakljucano"].ToString() + "', obrisano = '" + r["obrisano"].ToString() + "', datum='" + datum.ToString("yyyy-MM-dd H:mm:ss") + "' WHERE broj_inventure='" + r["broj_inventure"].ToString() + "';" +
                            " UPDATE inventura_stavke SET " +
                            " kolicina='" + Math.Round(kolicina, 4).ToString().Replace(".", ",") + "'," +
                            " kolicina_koja_je_bila_na_skl='" + Math.Round(kolicina_koja_je_bila_na_skl, 4).ToString().Replace(",", ".") + "'," +
                            " cijena='" + Math.Round(cijena, 4).ToString().Replace(",", ".") + "'," +
                            " povratna_naknada='" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'" +
                            " WHERE sifra_robe='" + r["sifra_robe"].ToString() + "' " +
                            " AND broj_inventure='" + r["broj_inventure"].ToString() + "'; COMMIT;";
                        classSQL.update(sqlPG);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE inventura SET novo='0', editirano='0' " +
                            " WHERE id='" + r["id"].ToString() + "'" +
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