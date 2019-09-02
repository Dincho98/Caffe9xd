using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synPrimka
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private string id_poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPrimka(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            try
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
                    if (MessageBox.Show("Želite poslati primke i kalkulacije?", "Slanje primka i kalkulacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    //MessageBox.Show("Šaljem primke i kalkulacije");
                    filter = " WHERE zakljucano::integer = 0 ORDER BY primka.datum ASC";
                }
                else
                {
                    filter = "  WHERE (primka.editirano='1' OR primka.novo='1') AND zakljucano::integer = 0 ORDER BY primka.datum ASC";
                }
                //****************************************************************************

                string query = "SELECT " +
                    " primka.broj_primke," +
                    " primka.id_skladiste," +
                    " primka.br_ulaznog_doc," +
                    " primka.id_partner," +
                    " primka.datum," +
                    " primka.iznos_bez_poreza," +
                    " primka.iznos_sa_porezom," +
                    " primka.carina," +
                    " primka.valuta," +
                    " primka.napomena," +
                    " primka.iznos," +
                    " primka.id_zaposlenik," +
                    " primka_stavke.sifra," +
                    " primka_stavke.u_pakiranju," +
                    " primka_stavke.broj_paketa," +
                    " primka_stavke.kolicina," +
                    " primka_stavke.cijena_po_komadu," +
                    " primka_stavke.rabat," +
                    " primka_stavke.nabavni_iznos," +
                    " primka.is_kalkulacija," +
                    " primka_stavke.ulazni_porez," +
                    " primka_stavke.nabavna_cijena," +
                    " primka_stavke.iznos," +
                    " primka.editirano," +
                    " primka.novo," +
                    " primka.id_poslovnica," +
                    " primka_stavke.marza," +
                    " primka_stavke.iznos_marze," +
                    " zaposlenici.oib AS oib_zaposlenika," +
                    " primka_stavke.prodajna_cijena," +
                    " primka_stavke.prodajna_cijena_sa_porezom," +
                    " primka_stavke.povratna_naknada," +
                    " primka.zakljucano::integer as zakljucano" +
                    " FROM primka_stavke" +
                    " RIGHT JOIN primka ON primka_stavke.broj_primke = primka.broj_primke AND primka.is_kalkulacija=primka_stavke.is_kalkulacija" +
                    " LEFT JOIN zaposlenici ON primka.id_zaposlenik = zaposlenici.id_zaposlenik " +
                    " AND primka_stavke.id_skladiste = primka.id_skladiste " +
                    " AND primka.is_kalkulacija=primka_stavke.is_kalkulacija" +
                    "" + filter + ";";

                DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

                string is_kalk = "0";
                string sql = "sql=";
                string tempDel = "";

                //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
                foreach (DataRow r in DT.Rows)
                {
                    if (r["is_kalkulacija"].ToString().ToUpper() == "TRUE") { is_kalk = "1"; } else { is_kalk = "0"; }

                    DateTime datum;
                    DateTime.TryParse(r["datum"].ToString(), out datum);

                    if (posalji_sve)
                    {
                        tempDel = @"
DELETE FROM primke
WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
AND poslovnica='" + poslovnica + @"'
AND YEAR(datum)='" + datum.Year.ToString() + @"'
AND is_kalkulacija='" + is_kalk + @"' AND zakljucano = '0';~";
                    }
                    else
                    {
                        if (r["editirano"].ToString() == "True")
                        {
                            tempDel = @"
DELETE FROM primke
WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
AND poslovnica='" + poslovnica + @"'
AND broj='" + r["broj_primke"].ToString() + @"'
AND YEAR(datum)='" + datum.Year.ToString() + @"'
AND is_kalkulacija='" + is_kalk + @"' AND zakljucano = '0';~";
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

                    if (r["is_kalkulacija"].ToString().ToUpper() == "TRUE") { is_kalk = "1"; } else { is_kalk = "0"; }

                    if (r["sifra"].ToString() != "")
                    {
                        sql += @"
INSERT INTO primke (marza, iznos_marze, prodajna_cijena, prodajna_cijena_sa_porezom, broj, id_skladiste,ulazni_dok,id_partner,
datum,carina,valuta,napomena,id_zaposlenik,sifra,u_pakiranju,broj_paketa,kolicina,cijena_po_komadu,rabat,nabavna_cijena,ulazni_porez,
nabavni_iznos,povratna_naknada,editirano,oib,is_kalkulacija,oib_zaposlenika,poslovnica, zakljucano) VALUES (
'" + r["marza"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["iznos_marze"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["prodajna_cijena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["prodajna_cijena_sa_porezom"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["broj_primke"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["br_ulaznog_doc"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["id_partner"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + datum.ToString("yyyy-MM-dd H:mm:ss") + @"',
'" + r["carina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["valuta"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["id_zaposlenik"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + r["u_pakiranju"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["broj_paketa"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["cijena_po_komadu"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["nabavna_cijena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["ulazni_porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["nabavni_iznos"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'0',
'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + @"',
'" + is_kalk + @"',
'" + r["oib_zaposlenika"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"',
'" + poslovnica + @"',
'" + r["zakljucano"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"');~";
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
                            sql = "UPDATE primka SET editirano='0', novo='0' " +
                                " WHERE broj_primke='" + r["broj_primke"].ToString() + "' " +
                                " AND is_kalkulacija='" + r["is_kalkulacija"].ToString().Replace(";", "").Replace("~", "") + "';";

                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
            catch
            {
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

            //domena = "http://pos.pc1.hr/";
            string sql = "SELECT * FROM primke" +
                " WHERE (editirano='1') AND OIB = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "' order by broj asc;";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string zadnji_broj = "", zadnja_godina = "", zadnja_poslovnica = "", is_kalkulacija = "", id_skladiste = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);

                    if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && zadnja_poslovnica == r["poslovnica"].ToString() && is_kalkulacija == r["is_kalkulacija"].ToString() && id_skladiste == r["id_skladiste"].ToString())
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        is_kalkulacija = r["is_kalkulacija"].ToString();
                        id_skladiste = r["id_skladiste"].ToString();
                        SpremiStavke(r);
                    }
                    else
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        is_kalkulacija = r["is_kalkulacija"].ToString();
                        id_skladiste = r["id_skladiste"].ToString();

                        SpremiHeader(r, DT);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE primke SET editirano='0' " +
                        " WHERE id='" + r["id"].ToString() + "' AND poslovnica='" + poslovnica + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                }

                Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
                robno.PostaviNabavneCijeneZaTablicuRobaProdaja();
                robno.PostaviNabavneCijeneRoba();
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            classSQL.delete("DELETE FROM primka WHERE broj_primke='" + r["broj"].ToString() + "' and id_skladiste = '" + r["id_skladiste"].ToString() + "' AND is_kalkulacija='" + r["is_kalkulacija"].ToString() + "';" +
                "DELETE FROM primka_stavke WHERE broj_primke='" + r["broj"].ToString() + "' and id_skladiste = '" + r["id_skladiste"].ToString() + "' AND is_kalkulacija='" + r["is_kalkulacija"].ToString() + "';");

            decimal bez_poreza, porez, kolicina, carina, valuta, UK_bez_poreza = 0, UK_sa_porezom = 0;

            //ZBRAJAM UKUPNO NA PRIMKI ILI KALKULACIJI
            DataRow[] RowsTrenutnaPrimka = DT.Select("broj='" + r["broj"].ToString() + "' and id_skladiste = '" + r["id_skladiste"].ToString() + "'");
            foreach (DataRow Rc in RowsTrenutnaPrimka)
            {
                decimal.TryParse(Rc["nabavna_cijena"].ToString().Replace(".", ","), out bez_poreza);
                decimal.TryParse(Rc["ulazni_porez"].ToString().Replace(".", ","), out porez);
                decimal.TryParse(Rc["kolicina"].ToString().Replace(".", ","), out kolicina);

                UK_bez_poreza += (bez_poreza * kolicina);
                UK_sa_porezom += ((bez_poreza * (1 + (porez / 100))) * kolicina);
            }

            decimal.TryParse(r["carina"].ToString().Replace(".", ","), out carina);
            decimal.TryParse(r["valuta"].ToString().Replace(".", ","), out valuta);

            string sql = "INSERT INTO primka (" +
                "broj_primke,id_skladiste,br_ulaznog_doc,id_partner,datum,iznos_bez_poreza,iznos_sa_porezom,carina,valuta,id_zaposlenik," +
                "napomena,is_kalkulacija,iznos,id_poslovnica,novo,editirano, zakljucano)" +
                " VALUES " +
                "(" +
                "'" + r["broj"].ToString() + "'," +
                "'" + r["id_skladiste"].ToString() + "'," +
                "'" + r["ulazni_dok"].ToString() + "'," +
                "'" + r["id_partner"].ToString() + "'," +
                "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + Math.Round(UK_bez_poreza, 2).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(UK_sa_porezom, 2).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(carina, 2).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(valuta, 5).ToString().Replace(",", ".") + "'," +
                "'" + r["id_zaposlenik"].ToString().Replace(",", ".") + "'," +
                "'" + r["napomena"].ToString() + "'," +
                "'" + r["is_kalkulacija"].ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(UK_sa_porezom, 2).ToString().Replace(",", ".") + "'," +
                "'" + id_poslovnica + "','0','0'," +
                "'" + r["zakljucano"] + "'" +
                ")";

            classSQL.insert(sql);
        }

        private void SpremiStavke(DataRow r)
        {
            decimal u_pakiranju, broj_paketa, kolicina, rabat, cijena_po_komadu, nabavna_cijena, ulazni_porez,
                nabavni_iznos, marza, iznos_marze, prodajna_cijena, prodajna_cijena_sa_porezom, povratna_naknada, nabavni_iznos_sa_porezom;

            decimal.TryParse(r["u_pakiranju"].ToString().Replace(".", ","), out u_pakiranju);
            decimal.TryParse(r["broj_paketa"].ToString().Replace(".", ","), out broj_paketa);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["cijena_po_komadu"].ToString().Replace(".", ","), out cijena_po_komadu);
            decimal.TryParse(r["nabavna_cijena"].ToString().Replace(".", ","), out nabavna_cijena);
            decimal.TryParse(r["ulazni_porez"].ToString().Replace(".", ","), out ulazni_porez);
            decimal.TryParse(r["nabavni_iznos"].ToString().Replace(".", ","), out nabavni_iznos);
            decimal.TryParse(r["marza"].ToString().Replace(".", ","), out marza);
            decimal.TryParse(r["iznos_marze"].ToString().Replace(".", ","), out iznos_marze);
            decimal.TryParse(r["prodajna_cijena"].ToString().Replace(".", ","), out prodajna_cijena);
            decimal.TryParse(r["prodajna_cijena_sa_porezom"].ToString().Replace(".", ","), out prodajna_cijena_sa_porezom);
            decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

            nabavni_iznos_sa_porezom = ((nabavni_iznos * (1 + (ulazni_porez / 100))));

            string sql = "INSERT INTO primka_stavke (" +
                "sifra,u_pakiranju,broj_paketa,kolicina,cijena_po_komadu,rabat,nabavna_cijena,ulazni_porez," +
                "nabavni_iznos,iznos,broj_primke,id_skladiste,is_kalkulacija," +
                "marza,iznos_marze,prodajna_cijena,prodajna_cijena_sa_porezom,povratna_naknada)" +
                " VALUES " +
                "(" +
                "'" + r["sifra"].ToString() + "'," +
                "'" + Math.Round(u_pakiranju, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(broj_paketa, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(kolicina, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(cijena_po_komadu, 5).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(rabat, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(nabavna_cijena, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(ulazni_porez, 0).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(nabavni_iznos, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(nabavni_iznos_sa_porezom, 4).ToString().Replace(",", ".") + "'," +
                "'" + r["broj"].ToString() + "'," +
                "'" + r["id_skladiste"].ToString() + "'," +
                "'" + r["is_kalkulacija"].ToString() + "'," +
                "'" + Math.Round(marza, 6).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(iznos_marze, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(prodajna_cijena, 4).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(prodajna_cijena_sa_porezom, 2).ToString().Replace(",", ".") + "'," +
                "'" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'" +
                ");";

            classSQL.insert(sql);

            if (r["is_kalkulacija"].ToString() == "1")
            {
                try
                {
                    // classSQL.update("UPDATE roba SET mpc='" + Math.Round(prodajna_cijena_sa_porezom, 2).ToString().Replace(".", ",") + "'"+
                    //    " WHERE sifra='" + r["sifra"].ToString() + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //POSTAVLJAM CIJENU U SLUČAJU DA SE PROMIJENILA
            /*classSQL.update("UPDATE roba_prodaja SET "+
                " u_pakiranju='" + Math.Round(u_pakiranju, 4).ToString().Replace(",", ".") + "'," +
                " nc='" + Math.Round(nabavna_cijena, 4).ToString().Replace(",", ".") + "'"+
                " WHERE sifra='" + r["sifra"].ToString() + "'");*/

            classSQL.update("UPDATE roba_prodaja SET povratna_naknada='" + Math.Round(povratna_naknada, 2).ToString().Replace(".", ",") + "'," +
                        "u_pakiranju='" + Math.Round(u_pakiranju, 4).ToString().Replace(",", ".") + "'" +
                        ", nc='" + Math.Round(nabavna_cijena, 4).ToString().Replace(",", ".") + "' WHERE sifra='" + r["sifra"].ToString() + "'");
        }

        #endregion PRIMI PODATKE
    }
}