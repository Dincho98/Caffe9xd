using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synFaktura
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();
        public bool gasenje = false;
        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;
        private string od_datuma = null;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synFaktura(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public synFaktura(bool _posalji_sve, string _od_datuma)
        {
            posalji_sve = _posalji_sve;
            od_datuma = _od_datuma;
        }

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            DataTable DTnaplatni_uredaj = SqlPostgres.select("SELECT ime_blagajne FROM blagajna" +
                " WHERE id_blagajna='" + DTpostavke.Rows[0]["default_kasa_fakture"].ToString() + "'", "zaposlenici").Tables[0];

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
                if (MessageBox.Show("Želite poslati fakture?", "Slanje faktura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = ";";
            }
            else
            {
                filter = " WHERE (f.novo = '1' or f.editirano = '1');";
            }
            //************************************************************************************

            string query = @"SELECT f.broj_fakture,
f.id_odrediste,
f.id_fakturirati,
f.date AS datum,
f.datedvo,
f.datum_valute,
f.id_izjava,
f.id_zaposlenik,
f.id_zaposlenik_izradio,
model,
id_nacin_placanja,
f.zr,
f.id_valuta,
f.otprema,
f.id_predujam,
f.napomena,
ROUND(f.ukupno::numeric, 6) AS ukupno,
f.id_vd,
f.godina_predujma::NUMERIC AS godina_predujma,
f.godina_ponude::NUMERIC AS godina_ponude,
f.godina_fakture::NUMERIC AS godina_fakture,
f.mj_troska,
CASE WHEN f.oduzmi_iz_skladista::NUMERIC = 1 THEN TRUE ELSE FALSE END AS oduzmi_iz_skladista,
f.id_kasa AS id_naplatni_uredaj,
f.id_ducan AS id_poslovnica,
fs.sifra,
REPLACE(fs.kolicina, ',','.')::NUMERIC AS kolicina,
ROUND(vpc, 6) AS vpc,
ROUND(REPLACE(porez, ',','.')::NUMERIC, 2) AS pdv,
ROUND(REPLACE(rabat, ',','.')::NUMERIC, 2) AS rabat,
id_skladiste,
CASE WHEN UPPER(oduzmi) = 'DA' THEN 1 ELSE 0 END AS oduzmi,
CASE WHEN UPPER(odjava) = 'DA' THEN 1 ELSE 0 END AS odjava,
ROUND(nbc::numeric, 6) AS nbc,
ROUND(REPLACE(porez_potrosnja, ',','.')::NUMERIC, 2) AS pnp,
mpc
FROM fakture f
LEFT JOIN faktura_stavke fs ON f.broj_fakture = fs.broj_fakture" + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (posalji_sve)
                {
                    tempDel = "DELETE FROM fakture WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                            " AND poslovnica = '" + poslovnica + "' AND naplatni_uredaj = '" + naplatni_uredaj + "' AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                }
                else
                {
                    DateTime dt_temp;
                    DateTime.TryParse(od_datuma, out dt_temp);
                    tempDel = "DELETE FROM fakture WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                                " AND poslovnica = '" + poslovnica + "' AND naplatni_uredaj = '" + naplatni_uredaj + "' and broj_fakture = '" + r["broj_fakture"] + "' AND YEAR(datum) = '" + datum.Year.ToString() + "';~";
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            int count_sql = 0;
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);
                DateTime datedvo;
                DateTime.TryParse(r["datedvo"].ToString(), out datedvo);
                DateTime datum_valute;
                DateTime.TryParse(r["datum_valute"].ToString(), out datum_valute);

                sql += @"INSERT INTO `fakture`
(`oib`,
`poslovnica`,
`naplatni_uredaj`,
`broj_fakture`,
`id_odrediste`,
`id_fakturirati`,
`datum`,
`datum_dvo`,
`datum_valute`,
`id_izjava`,
`id_zaposlenik`,
`id_zaposlenik_izradio`,
`model`,
`id_nacin_placanja`,
`iban`,
`id_valuta`,
`otprema`,
`napomena`,
`ukupno`,
`id_vd`,
`godina_ponude`,
`godina_fakture`,
`mj_troska`,
`sifra`,
`id_skladiste`,
`kolicina`,
`nbc`,
`vpc`,
`mpc`,
`porez`,
`rabat`,
`porez_potrosnja`,
`oduzmi`,
`odjava`,
`editirano`,
`novo`)
VALUES (
'" + Util.Korisno.oibTvrtke + @"',
'" + Util.Korisno.nazivDucan + @"',
'" + Util.Korisno.nazivBlagajnaFakture + @"',
'" + r["broj_fakture"] + @"',
'" + r["id_odrediste"] + @"',
'" + r["id_fakturirati"] + @"',
'" + datum.ToString("yyyy-MM-dd H:mm:ss") + @"',
'" + datedvo.ToString("yyyy-MM-dd H:mm:ss") + @"',
'" + datum_valute.ToString("yyyy-MM-dd H:mm:ss") + @"',
'" + r["id_izjava"] + @"',
'" + r["id_zaposlenik"] + @"',
'" + r["id_zaposlenik_izradio"] + @"',
'" + r["model"] + @"',
'" + r["id_nacin_placanja"] + @"',
'" + r["zr"] + @"',
'" + r["id_valuta"] + @"',
'" + r["otprema"] + @"',
'" + r["napomena"] + @"',
'" + r["ukupno"].ToString().Replace(",", ".") + @"',
'" + r["id_vd"] + @"',
'" + r["godina_ponude"] + @"',
'" + r["godina_fakture"] + @"',
'" + r["mj_troska"] + @"',
'" + r["sifra"] + @"',
'" + r["id_skladiste"] + @"',
'" + r["kolicina"].ToString().Replace(",", ".") + @"',
'" + r["nbc"].ToString().Replace(",", ".") + @"',
'" + r["vpc"].ToString().Replace(",", ".") + @"',
'" + r["mpc"].ToString().Replace(",", ".") + @"',
'" + r["pdv"].ToString().Replace(",", ".") + @"',
'" + r["rabat"].ToString().Replace(",", ".") + @"',
'" + r["pnp"].ToString().Replace(",", ".") + @"',
'" + r["oduzmi"] + @"',
'" + r["odjava"] + @"',
'0',
'0');~";

                count_sql++;
                if (count_sql > 500)
                {
                    sql += "Ł";
                    count_sql = 0;
                }
            }

            if (sql.Length > 4)
            {
                //ŠALJE NA WEB i DOBIVAM ODG

                string[] arrSql = sql.Split('Ł');
                bool sql_je_ispravan = true;

                foreach (string ___sql in arrSql)
                {
                    string sql_finish = "sql=" + ___sql.Remove(___sql.Length - 1);
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
                        sql = @"UPDATE fakture
SET editirano='0', novo='0'
WHERE broj_fakture = '" + r["broj_fakture"].ToString() + @"'
AND id_ducan='" + r["id_poslovnica"].ToString() + @"'
AND id_kasa='" + r["id_naplatni_uredaj"].ToString() + @"';";

                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DateTime dtSyn = DateTime.Now;

            string sql_za_web = "sql=";

            string sql = "SELECT * FROM fakture WHERE (editirano = '1' OR novo = '1') AND OIB = '" + Util.Korisno.oibTvrtke + "' AND poslovnica = '" + Util.Korisno.nazivDucan + "' AND YEAR(datum) = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "';";

            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string _zadnji_broj = "", _zadnja_godina = "", _zadnja_poslovnica = "", _oib = "", _naplatni_uredaj = "", brFak = "", br = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);
                    br = r["broj_fakture"].ToString().Trim();
                    if (_oib == r["oib"].ToString().Trim() && _zadnji_broj == br && _zadnja_godina == d.Year.ToString().Trim() && _zadnja_poslovnica == r["poslovnica"].ToString().Trim() && _naplatni_uredaj == r["naplatni_uredaj"].ToString().Trim())
                    {
                        SpremiStavke(r);
                    }
                    else
                    {
                        _oib = r["oib"].ToString().Trim();
                        _zadnji_broj = br;
                        _zadnja_godina = d.Year.ToString().Trim();
                        _zadnja_poslovnica = r["poslovnica"].ToString().Trim();
                        _naplatni_uredaj = r["naplatni_uredaj"].ToString().Trim();

                        SpremiHeader(r, DT, dtSyn);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************

                    if (!brFak.Contains(br))
                    {
                        if (brFak.Length > 0)
                            brFak += ", ";

                        brFak += br;
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                }
                sql_za_web = "UPDATE fakture SET editirano = '0', novo = '0' WHERE oib='" + _oib + "' AND broj_fakture IN (" + brFak + ") AND poslovnica='" + _zadnja_poslovnica + "' AND naplatni_uredaj = " + _naplatni_uredaj + ";~";
                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest("sql=" + sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT, DateTime dtSyn)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            //DODANO
            string delete = "DELETE FROM faktura_stavke WHERE broj_fakture = '" + r["broj_fakture"].ToString() + "'; " +
                "DELETE FROM fakture WHERE broj_fakture = '" + r["broj_fakture"].ToString() + "' AND id_ducan = '" + Util.Korisno.idDucan + "' AND id_kasa = '" + Util.Korisno.idKasaFakture + "';";

            SqlPostgres.delete(delete);

            StringBuilder sql = new StringBuilder();
            string ukp = r["ukupno"].ToString().Replace(".", ",");
            decimal ikp = Convert.ToDecimal(ukp);

            sql.Append("INSERT INTO fakture (broj_fakture, id_odrediste, id_fakturirati, date, datedvo, datum_valute, id_izjava, id_zaposlenik, id_zaposlenik_izradio, model, id_nacin_placanja, zr, id_valuta, otprema, napomena, ukupno, id_vd, godina_predujma, godina_ponude, godina_fakture, mj_troska, oduzmi_iz_skladista, id_kasa, id_ducan, novo, editirano)");
            sql.Append("VALUES ");
            sql.Append("( ");
            sql.Append("'" + r["broj_fakture"].ToString() + "', "); //broj_fakture
            sql.Append("'" + r["id_odrediste"].ToString() + "', "); //id_odrediste
            sql.Append("'" + r["id_fakturirati"].ToString() + "', "); //id_fakturirati
            sql.Append("'" + Convert.ToDateTime(r["datum"]).ToString() + "', "); //date
            sql.Append("'" + Convert.ToDateTime(r["datum_dvo"]).ToString() + "', "); //datedvo
            sql.Append("'" + Convert.ToDateTime(r["datum_valute"]).ToString() + "', "); //datum_valute
            sql.Append("'" + 1.ToString() + "', "); //id_izjava
            sql.Append("'" + r["id_zaposlenik"].ToString() + "', "); //id_zaposlenik
            sql.Append("'" + r["id_zaposlenik"].ToString() + "', "); //id_zaposlenik_izradio
            sql.Append("'" + r["model"].ToString() + "', "); //model
            sql.Append("'" + r["id_nacin_placanja"].ToString() + "', ");     //id_nacin_placanja----------
            sql.Append("'" + r["iban"].ToString() + "', "); //zr
            sql.Append("'" + r["id_valuta"].ToString() + "', "); //id_valuta
            sql.Append("'" + r["otprema"].ToString() + "', "); //otprema
            sql.Append("'" + r["napomena"].ToString().Trim() + "', "); //napomena
            sql.Append("'" + Convert.ToDecimal(r["ukupno"].ToString().Replace(".", ",")).ToString() + "', "); //ukupno
            sql.Append("'" + r["id_vd"].ToString().Trim() + "', "); //id_vd
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', "); //godina_predujma
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', "); //godina_ponude
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', "); //godina_fakture
            sql.Append("'" + r["mj_troska"].ToString() + "', "); //mj_troska
            sql.Append("'" + r["oduzmi"].ToString() + "', "); //oduzmi_iz_skladista
            sql.Append("'" + Util.Korisno.idDucan + "', ");
            sql.Append("'" + Util.Korisno.idKasaFakture + "', ");
            sql.Append("'0', ");
            sql.Append("'0'");
            sql.Append(");");

            SqlPostgres.insert(sql.ToString());
        }

        private void SpremiStavke(DataRow r)
        {
            decimal kolicina = 0, rabat = 0, vpc = 0, povratna_naknada = 0, porez = 0, nbc = 0, mpc = 0, rabat_izn = 0, mpc_rabat = 0, ukupno_rabat = 0, ukupno_vpc = 0, ukupno_mpc = 0, ukupno_mpc_rabat = 0, ukupno_porez = 0, ukupno_osnovica = 0, plusPorez = 0, porez_potrosnja = 0;

            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            plusPorez = (1 + (porez / 100));
            mpc = vpc * plusPorez;
            decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
            decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);
            decimal.TryParse(r["porez_potrosnja"].ToString().Replace(".", ","), out porez_potrosnja);

            rabat_izn = mpc * (rabat / 100);
            mpc_rabat = mpc - rabat_izn;
            ukupno_rabat = kolicina * rabat_izn;
            ukupno_mpc = mpc * kolicina;
            ukupno_vpc = vpc * kolicina;
            ukupno_mpc_rabat = mpc_rabat * kolicina;
            ukupno_osnovica = ukupno_mpc_rabat / plusPorez;
            ukupno_porez = ukupno_mpc_rabat - ukupno_osnovica;

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO faktura_stavke");
            sb.Append(" (");
            sb.Append(" ");
            sb.Append("kolicina, vpc, porez, broj_fakture, rabat, id_skladiste, sifra, oduzmi, odjava, nbc,");
            sb.Append("\n\t");
            sb.Append("porez_potrosnja, mpc");
            sb.Append(" )\n");
            sb.Append("VALUES ");
            sb.Append("(");
            sb.Append(" ");
            sb.Append("'" + Math.Round(kolicina, 4).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(vpc, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + porez.ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["broj_fakture"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(rabat, 4).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["id_skladiste"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["sifra"].ToString() + "',");
            sb.Append(" ");
            sb.Append("CASE WHEN " + r["oduzmi"].ToString() + " = 1 THEN 'DA' ELSE 'NE' END,");
            sb.Append(" ");
            sb.Append("'" + r["odjava"] + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(nbc, 4).ToString() + "',");
            sb.Append("\n\t");
            sb.Append("'" + Math.Round(porez_potrosnja, 2).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(mpc, 4).ToString().Replace(",", ".") + "'");
            sb.Append(")");
            sb.Append(";");

            SqlPostgres.insert(sb.ToString());
        }

        #endregion PRIMI PODATKE
    }
}