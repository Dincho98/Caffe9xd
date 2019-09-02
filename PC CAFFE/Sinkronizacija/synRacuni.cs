using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synRacuni
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();
        public bool gasenje = false;
        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;
        private string od_datuma = null;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KREIRANJA
        public synRacuni(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public synRacuni(bool _posalji_sve, string _od_datuma)
        {
            posalji_sve = _posalji_sve;
            od_datuma = _od_datuma;
        }

        public void Send()
        {
            try
            {
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
                DataTable DTnaplatni_uredaj = SqlPostgres.select("SELECT ime_blagajne FROM blagajna" +
                    " WHERE id_blagajna='" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'", "zaposlenici").Tables[0];

                string poslovnica = "1";
                string naplatni_uredaj = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                if (DTnaplatni_uredaj.Rows.Count > 0)
                {
                    naplatni_uredaj = DTnaplatni_uredaj.Rows[0]["ime_blagajne"].ToString();
                }

                //************************************GLEDA NA VARIJABLU posalji_sve ******************
                string filter = "";
                if (posalji_sve)
                {
                    if (!gasenje)
                    {
                        if (MessageBox.Show("Želite poslati račune?", "Slanje računa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (od_datuma == null && MessageBox.Show("Šaljem račune. Ovo bi moglo potrajati par minuta ovisno o broju računa. Program ne dirajte sve dok ne dođe nova poruka.\r\nDali ste sigurni da želite nastaviti?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    if (od_datuma == null)
                        od_datuma = Interaction.InputBox("Odabir razdoblja", "Odabir razdoblja za koje želite poslati ponovno stavke na web server.", DateTime.Now.AddDays(-31).ToString("dd.MM.yyyy 00:00:01"));

                    DateTime dt_temp;
                    DateTime.TryParse(od_datuma, out dt_temp);

                    filter = "WHERE racuni.datum_racuna >= '" + dt_temp.ToString("yyyy-MM-dd H:mm:ss") + "' order by racuni.datum_racuna asc;";
                }
                else
                {
                    filter = "  WHERE (racuni.editirano='1' OR racuni.novo='1') order by racuni.datum_racuna asc;";
                }
                //************************************************************************************

                string query = @"SELECT
racun_stavke.sifra_robe,
racun_stavke.id_skladiste,
racun_stavke.mpc,
racun_stavke.porez,
racun_stavke.kolicina,
racun_stavke.rabat,
racun_stavke.vpc,
racun_stavke.nbc,
racun_stavke.porez_potrosnja,
racun_stavke.povratna_naknada,
racun_stavke.id_ducan,
racun_stavke.id_blagajna,
racuni.broj_racuna,
racuni.novo,
racuni.id_kasa,
racuni.editirano,
zaposlenici.oib AS oib_zaposlenika,
racuni.datum_racuna,
racuni.id_blagajnik,
racuni.id_kupac,
coalesce(grupa.id_grupa, 1) as id_grupa,
coalesce(podgrupa.id_podgrupa, 1) as id_podgrupa,
racuni.nacin_placanja,
racuni.jir,
racuni.zik, racuni.broj_ispisa,
ducan.ime_ducana, blagajna.ime_blagajne
FROM racun_stavke
LEFT JOIN roba ON racun_stavke.sifra_robe = roba.sifra
LEFT JOIN grupa ON roba.id_grupa = grupa.id_grupa
LEFT JOIN podgrupa ON podgrupa.id_podgrupa = grupa.id_podgrupa
LEFT JOIN racuni ON racun_stavke.broj_racuna = racuni.broj_racuna  AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
LEFT JOIN zaposlenici ON racuni.id_blagajnik = zaposlenici.id_zaposlenik
left join ducan on racun_stavke.id_ducan = ducan.id_ducan
left join blagajna on racun_stavke.id_blagajna = blagajna.id_blagajna
" + filter;

                DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

                string sql = "";
                string tempDel = "";
                //string broj = "";

                //            var consolidatedChildren =
                //from c in DT.AsEnumerable()
                //group c by new
                //{
                //    c.Field<string>("oib"),
                //    c.Friend,
                //    c.FavoriteColor,
                //} into gcs
                //select new ConsolidatedChild()
                //{
                //    School = gcs.Key.School,
                //    Friend = gcs.Key.Friend,
                //    FavoriteColor = gcs.Key.FavoriteColor,
                //    Children = gcs.ToList(),
                //};

                var newSort = from row in DT.AsEnumerable()
                              group row by new { ime_ducana = row.Field<string>("ime_ducana"), ime_blagajne = row.Field<string>("ime_blagajne"), godina = row.Field<DateTime>("datum_racuna").Year, skladiste = row.Field<int>("id_skladiste") } into grp
                              select new
                              {
                                  godina = grp.Key.godina,
                                  ime_ducana = grp.Key.ime_ducana,
                                  ime_blagajne = grp.Key.ime_blagajne,
                                  skladiste = grp.Key.skladiste
                              };

                foreach (var item in newSort)
                {
                    String result = DT.AsEnumerable()
                    .Where(r => r.Field<string>("ime_ducana") == item.ime_ducana && r.Field<string>("ime_blagajne") == item.ime_blagajne && r.Field<DateTime>("datum_racuna").Year == item.godina && r.Field<int>("id_skladiste") == item.skladiste)
                     .Select(row => row["broj_racuna"].ToString()).Distinct()
                     .Aggregate((s1, s2) => String.Concat(s1, ", ", s2));

                    //DateTime datum;
                    //DateTime.TryParse(r["datum_racuna"].ToString(), out datum);

                    if (posalji_sve && od_datuma == null)
                    {
                        tempDel = @"DELETE FROM racuni
WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
AND broj in (" + result + @")
AND poslovnica='" + item.ime_ducana + @"'
AND naplatni_uredaj='" + item.ime_blagajne + @"'
AND id_skladiste = '" + item.skladiste + @"'
AND YEAR(datum)='" + item.godina + "';";
                    }
                    else if (posalji_sve && od_datuma != null)
                    {
                        DateTime dt_temp;
                        DateTime.TryParse(od_datuma, out dt_temp);
                        tempDel = @"DELETE FROM racuni
WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
AND datum >= '" + dt_temp.ToString("yyyy-MM-dd H:mm:ss") + @"'
AND poslovnica = '" + item.ime_ducana + @"'
AND naplatni_uredaj = '" + item.ime_blagajne + @"'
AND id_skladiste = '" + item.skladiste + @"'
AND YEAR(datum) = '" + item.godina + "';";
                    }
                    else
                    {
                        tempDel = @"DELETE FROM racuni
WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
AND broj in (" + result + @")
AND poslovnica='" + item.ime_ducana + @"'
AND naplatni_uredaj='" + item.ime_blagajne + @"'
AND id_skladiste = '" + item.skladiste + @"'
AND YEAR(datum)='" + item.godina + "';";
                    }

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;

                        if (sql.Length > 0)
                            sql += "~";
                    }
                }

                //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
                //                foreach (DataRow r in DT.Rows)
                //                {
                //                    DateTime datum;
                //                    DateTime.TryParse(r["datum_racuna"].ToString(), out datum);

                //                    if (posalji_sve && od_datuma == null)
                //                    {
                //                        tempDel = @"DELETE FROM racuni
                //WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
                //AND broj='" + r["broj_racuna"].ToString() + @"'
                //AND poslovnica='" + poslovnica + @"'
                //AND naplatni_uredaj='" + naplatni_uredaj + @"'
                //AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                //                    }
                //                    else if (posalji_sve && od_datuma != null)
                //                    {
                //                        DateTime dt_temp;
                //                        DateTime.TryParse(od_datuma, out dt_temp);
                //                        tempDel = @"DELETE FROM racuni
                //WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
                //AND datum >= '" + dt_temp.ToString("yyyy-MM-dd H:mm:ss") + @"'
                //AND poslovnica = '" + poslovnica + @"'
                //AND naplatni_uredaj = '" + naplatni_uredaj + @"'
                //AND YEAR(datum) = '" + datum.Year.ToString() + "';~";
                //                    }
                //                    else
                //                    {
                //                        //if (broj.Length > 0) {
                //                        //    broj += ", ";
                //                        //}
                //                        //broj += r["broj_racuna"].ToString();

                //                        tempDel = @"DELETE FROM racuni
                //WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
                //AND broj='" + r["broj_racuna"].ToString() + @"'
                //AND poslovnica='" + poslovnica + @"'
                //AND naplatni_uredaj='" + naplatni_uredaj + @"'
                //AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                //                        //                        tempDel = @"DELETE FROM racuni
                //                        //WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + @"'
                //                        //AND broj in (" + broj + @")
                //                        //AND poslovnica='" + poslovnica + @"'
                //                        //AND naplatni_uredaj='" + naplatni_uredaj + @"'
                //                        //AND YEAR(datum)='" + datum.Year.ToString() + "';~";

                //                    }

                //                    if (!sql.Contains(tempDel))
                //                    {
                //                        sql += tempDel;
                //                    }

                //                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
                int dquery = 50;

                int count_sql = 0;
                string sqlInser = @"INSERT INTO racuni (broj, datum, poslovnica, naplatni_uredaj, oib, id_blagajnik, kupac, naplata, sifra,
id_skladiste, mpc, porez, kolicina, rabat, vpc, nbc, pp, povratna_naknada, id_grupa, oib_zaposlenika, id_podgrupa, jir, zki, broj_ispisa)
VALUES";
                foreach (DataRow r in DT.Rows)
                {
                    DateTime datum;
                    DateTime.TryParse(r["datum_racuna"].ToString(), out datum);

                    if (count_sql % dquery == 0)
                    {
                        if (sql.Contains(sqlInser))
                        {
                            if (count_sql > 0 && count_sql % dquery == 0)
                            {
                                sql = sql.Remove(sql.Length - 1);
                                sql += ";~";

                                if (count_sql % (dquery * 5) == 0)
                                {
                                    sql += "Ł";
                                }
                            }
                        }
                        sql += Environment.NewLine + sqlInser;
                    }

                    sql += @" ('" + r["broj_racuna"].ToString().Replace(";", "").Replace("~", "") + @"', '" + datum.ToString("yyyy-MM-dd H:mm:ss") + @"', '" + poslovnica + @"', '" + naplatni_uredaj.Replace(";", "").Replace("~", "") + @"', '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["id_blagajnik"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["id_kupac"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["nacin_placanja"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["sifra_robe"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["porez_potrosnja"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + @"', '" + r["id_grupa"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["id_podgrupa"].ToString().Replace(";", "").Replace("~", "") + @"', '" + r["jir"].ToString() + @"', '" + r["zik"].ToString() + @"', '" + r["broj_ispisa"].ToString().Replace(";", "").Replace("~", "") + @"' ),";

                    count_sql++;
                }
                //sql =

                if (sql.Length > 4)
                {
                    //ŠALJE NA WEB i DOBIVAM ODG
                    if (sql.EndsWith(","))
                        sql = sql.Remove(sql.Length - 1);

                    string[] arrSql = sql.Split('Ł');
                    bool sql_je_ispravan = true;

                    for (int i = 0; i < arrSql.Count(); i++)
                    {
                        if (arrSql[i].EndsWith("~"))
                            arrSql[i] = arrSql[i].Remove(arrSql[i].Length - 1);

                        if (arrSql[i].EndsWith(";"))
                            arrSql[i] = arrSql[i].Remove(arrSql[i].Length - 1);

                        if (arrSql[i].EndsWith(","))
                            arrSql[i] = arrSql[i].Remove(arrSql[i].Length - 1);

                        string sql_finish = "sql=" + arrSql[i];
                        string[] odg = Pomagala.MyWebRequest(sql_finish + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                        if (odg[0] != "OK" || odg[1] != "1")
                        {
                            sql_je_ispravan = false;
                        }
                    }

                    if (sql_je_ispravan)
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            sql = @"UPDATE racuni SET editirano='0', novo='0'
WHERE broj_racuna='" + r["broj_racuna"].ToString() + "' AND id_ducan='" + r["id_ducan"].ToString() + "' AND id_kasa='" + r["id_kasa"].ToString() + "';";

                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            string sql_za_web = "sql=";

            poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            string sql = @"SELECT *
FROM racuni
WHERE OIB = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica = '" + poslovnica + "';";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string zadnji_broj = "", zadnja_godina = "", zadnja_poslovnica = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);

                    if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && zadnja_poslovnica == r["poslovnica"].ToString())
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        SpremiStavke(r);
                    }
                    else
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();

                        SpremiHeader(r, DT);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    string sqlWeb = @"UPDATE racuni SET editirano='0', novo = '0'
WHERE id='" + r["id"].ToString() + @"' AND poslovnica='" + poslovnica + @"'
AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    if (!sql_za_web.Contains(sqlWeb))
                        sql_za_web += sqlWeb;
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);
            string sql = @"
DELETE FROM racuni
WHERE broj_racuna='" + r["broj"].ToString() + @"'
and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"')
AND id_kasa = (select id_blagajna from blagajna where ime_blagajne = '" + r["naplatni_uredaj"].ToString() + @"' and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"'));
DELETE FROM racun_stavke WHERE broj_racuna='" + r["broj"].ToString() + @"'
and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"')
AND id_blagajna = (select id_blagajna from blagajna where ime_blagajne = '" + r["naplatni_uredaj"].ToString() + @"' and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"'));";

            classSQL.delete(sql);

            decimal bez_poreza, porez, kolicina, UK_bez_poreza = 0, UK_sa_porezom = 0;

            //ZBRAJAM UKUPNO NA PRIMKI ILI KALKULACIJI
            DataRow[] RowsTrenutniRacun = DT.Select("broj='" + r["broj"].ToString() + "' and poslovnica = '" + r["poslovnica"] + "' and naplatni_uredaj = '" + r["naplatni_uredaj"] + "'");
            foreach (DataRow Rc in RowsTrenutniRacun)
            {
                decimal.TryParse(Rc["vpc"].ToString().Replace(".", ","), out bez_poreza);
                decimal.TryParse(Rc["porez"].ToString().Replace(".", ","), out porez);
                decimal.TryParse(Rc["kolicina"].ToString().Replace(".", ","), out kolicina);

                UK_bez_poreza += (bez_poreza * kolicina);
                UK_sa_porezom += ((bez_poreza * (1 + (porez / 100))) * kolicina);
            }

            sql = @"
INSERT INTO racuni ( broj_racuna, datum_racuna, id_ducan, id_kasa, id_kupac, id_blagajnik, ukupno_gotovina, ukupno_kartice, storno, ukupno,
dobiveno_gotovina, id_stol, ukupno_virman, jir, zik, nacin_placanja, editirano, novo, godina, ukupno_ostalo)
VALUES
(
'" + r["broj"].ToString() + @"',
'" + d.ToString("yyyy-MM-dd H:mm:ss") + @"',
(select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"'),
(select id_blagajna from blagajna where ime_blagajne = '" + r["naplatni_uredaj"].ToString() + @"' and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"')),
'" + r["kupac"].ToString() + @"',
'" + r["id_blagajnik"].ToString() + @"',
'" + Math.Round(UK_sa_porezom, 2).ToString() + @"',
'0',
'" + (r["storno"].ToString() == "1" ? "DA" : "NE") + @"',
'" + Math.Round(UK_sa_porezom, 2).ToString() + @"',
'" + Math.Round(UK_sa_porezom, 2).ToString() + @"',
'0',
'0',
'',
'',
'" + r["naplata"].ToString() + @"',
'0',
'0',
'" + d.ToString("yyyy") + @"',
'0'
)";

            classSQL.insert(sql);
        }

        private void SpremiStavke(DataRow r)
        {
            decimal mpc, porez, kolicina, rabat, vpc, nbc, porez_potrosnja, povratna_naknada;

            decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);
            decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
            decimal.TryParse(r["pp"].ToString().Replace(".", ","), out porez_potrosnja);
            decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            string sql = "INSERT INTO racun_stavke (" +
                "broj_racuna, sifra_robe, id_skladiste, mpc, porez, kolicina, rabat, vpc," +
                "nbc, porez_potrosnja, id_ducan, id_blagajna," +
                "godina, povratna_naknada)" +
                " VALUES " +
                "(" +
                "'" + r["broj"] + "'," +
                "'" + r["sifra"] + "'," +
                "'" + r["id_skladiste"] + "'," +
                "'" + mpc.ToString() + "'," +
                "'" + porez.ToString() + "'," +
                "'" + kolicina.ToString() + "'," +
                "'" + rabat.ToString() + "'," +
                "'" + vpc.ToString().Replace(",", ".") + "'," +
                "'" + nbc.ToString().Replace(",", ".") + "'," +
                "'" + porez_potrosnja.ToString() + "'," +
                "(select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"')," +
                "(select id_blagajna from blagajna where ime_blagajne = '" + r["naplatni_uredaj"].ToString() + @"' and id_ducan = (select id_ducan from ducan where ime_ducana = '" + r["poslovnica"].ToString() + @"'))," +
                "'" + d.ToString("yyyy") + "'," +
                "'" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'" +
                ");";

            classSQL.insert(sql);
        }

        #endregion PRIMI PODATKE
    }
}