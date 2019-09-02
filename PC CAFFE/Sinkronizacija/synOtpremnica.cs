using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    internal class synOtpremnica
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
        public synOtpremnica(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public synOtpremnica(bool _posalji_sve, string _od_datuma)
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
                if (MessageBox.Show("Želite poslati otpremnice?", "Slanje otpremnica", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = " order by o.datum asc;";
            }
            else
            {
                filter = " WHERE (o.novo = '1' or o.editirano = '1') order by o.datum asc;";
            }
            //************************************************************************************

            string query = @"select
o.broj_otpremnice,
o.godina_otpremnice::integer as godina_otpremnice, o.osoba_partner, o.id_odrediste, o.vrsta_dok, o.datum, o.id_izjava, o.napomena, o.id_kom, o.id_izradio, o.mj_otpreme, o.adr_otpreme, o.isprave, o.id_prijevoznik, o.registracija, o.istovarno_mj, o.istovarni_rok,
coalesce(replace(o.troskovi_prijevoza, ',','.')::numeric, 0)::numeric as troskovi_prijevoza, case when lower(o.partner_osoba) = 'p' then true else false end as partner_osoba, o.ukupno::numeric,
os.sifra_robe, os.id_skladiste, os.kolicina,
os.nbc,
os.vpc::numeric(15,6) as vpc,
(os.vpc::numeric(15,6) * (1 zbroj ((os.porez zbroj os.porez_potrosnja) / 100)))::numeric(15,6) as mpc,
os.porez, os.rabat, os.porez_potrosnja,
case when lower(os.oduzmi) = 'da' then true else false end as oduzmi,
os.odjava, os.naplaceno_fakturom
from otpremnice o
left join otpremnica_stavke os on o.broj_otpremnice = os.broj_otpremnice and o.godina_otpremnice::integer = os.godina_otpremnice" + filter;

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
                    tempDel = "DELETE FROM otpremnice WHERE oib = '" + Util.Korisno.oibTvrtke + "'" +
                            " AND poslovnica = '" + poslovnica + "' AND naplatni_uredaj = '" + naplatni_uredaj + "' AND godina_otpremnice = '" + datum.Year.ToString() + "';~";
                }
                else
                {
                    tempDel = "DELETE FROM otpremnice WHERE oib = '" + Util.Korisno.oibTvrtke + "' " +
                                " AND poslovnica = '" + poslovnica + "' AND naplatni_uredaj = '" + naplatni_uredaj + "' and broj_otpremnice = '" + r["broj_otpremnice"] + "' AND godina_otpremnice =  '" + datum.Year.ToString() + "';~";
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

                sql += @"INSERT INTO `otpremnice`
(`oib`,
`poslovnica`,
`naplatni_uredaj`,
`broj_otpremnice`,
`godina_otpremnice`,
`osoba_partner`,
`id_odrediste`,
`vrsta_dok`,
`datum`,
`id_izjava`,
`napomena`,
`id_komercijalista`,
`id_izradio`,
`mj_otpreme`,
`adr_otpreme`,
`isprave`,
`id_prijevoznik`,
`registracija`,
`istovarno_mj`,
`istovarni_rok`,
`troskovi_prijevoza`,
`partner_osoba`,
`ukupno`,
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
`naplaceno_fakturom`,
`novo`,
`editirano`)
VALUES (
'" + Util.Korisno.oibTvrtke + @"',
'" + Util.Korisno.nazivDucan + @"',
'" + naplatni_uredaj + @"',
'" + r["broj_otpremnice"] + @"',
'" + r["godina_otpremnice"] + @"',
'" + r["osoba_partner"] + @"',
'" + r["id_odrediste"] + @"',
'" + r["vrsta_dok"] + @"',
'" + datum.ToString("yyyy-MM-dd H:mm:ss") + @"',
" + (r["id_izjava"] == null || r["id_izjava"].ToString().Length == 0 ? "NULL" : "'" + r["id_izjava"] + "'") + @",
'" + r["napomena"] + @"',
'" + r["id_kom"] + @"',
'" + r["id_izradio"] + @"',
'" + r["mj_otpreme"] + @"',
'" + r["adr_otpreme"] + @"',
'" + r["isprave"] + @"',
" + (r["id_prijevoznik"] == null || r["id_prijevoznik"].ToString().Length == 0 ? "NULL" : "'" + r["id_prijevoznik"] + "'") + @",
'" + r["registracija"] + @"',
'" + r["istovarno_mj"] + @"',
" + (r["istovarni_rok"] == null || r["istovarni_rok"].ToString().Length == 0 ? "NULL" : "'" + r["istovarni_rok"] + "'") + @",
'" + r["troskovi_prijevoza"].ToString().Replace(",", ".") + @"',
" + r["partner_osoba"] + @",
" + r["ukupno"].ToString().Replace(",", ".") + @",
'" + r["sifra_robe"] + @"',
'" + r["id_skladiste"] + @"',
" + r["kolicina"].ToString().Replace(",", ".") + @",
" + r["nbc"].ToString().Replace(",", ".") + @",
" + r["vpc"].ToString().Replace(",", ".") + @",
" + r["mpc"].ToString().Replace(",", ".") + @",
" + r["porez"].ToString().Replace(",", ".") + @",
" + r["rabat"].ToString().Replace(",", ".") + @",
" + r["porez_potrosnja"].ToString().Replace(",", ".") + @",
" + r["oduzmi"] + @",
" + (r["odjava"] == null || r["odjava"].ToString().Length == 0 ? "NULL" : "'" + r["odjava"] + "'") + @",
" + r["naplaceno_fakturom"] + @",
0,
0);~";

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
                        sql = "UPDATE otpremnice SET editirano='0', novo='0' " +
                            "WHERE broj_otpremnice = '" + r["broj_otpremnice"].ToString() + "';";
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

            string sql = "SELECT * FROM otpremnice WHERE (editirano = '1' OR novo = '1') AND OIB = '" + Util.Korisno.oibTvrtke + "' AND poslovnica = '" + Util.Korisno.nazivDucan + "' AND godina_otpremnice = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "';";

            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string _zadnji_broj = "", _zadnja_godina = "", _zadnja_poslovnica = "", _oib = "", _naplatni_uredaj = "", brOtpr = "", br = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);
                    br = r["broj_otpremnice"].ToString().Trim();
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

                    if (!brOtpr.Contains(br))
                    {
                        if (brOtpr.Length > 0)
                            brOtpr += ", ";

                        brOtpr += br;
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                }
                sql_za_web = "UPDATE otpremnice SET editirano = '0', novo = '0' WHERE oib='" + _oib + "' AND broj_otpremnice IN (" + brOtpr + ") AND poslovnica='" + _zadnja_poslovnica + "' AND naplatni_uredaj = '" + _naplatni_uredaj + "';~";
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
            string delete = "DELETE FROM otpremnica_stavke WHERE broj_otpremnice = '" + r["broj_otpremnice"].ToString() + "'; " +
                "DELETE FROM otpremnice WHERE broj_otpremnice = '" + r["broj_otpremnice"].ToString() + "';";

            SqlPostgres.delete(delete);

            StringBuilder sql = new StringBuilder();
            string ukp = r["ukupno"].ToString().Replace(".", ",");
            decimal ikp = Convert.ToDecimal(ukp);

            sql.Append("INSERT INTO otpremnice (godina_otpremnice, id_skladiste, osoba_partner, id_odrediste, vrsta_dok, datum, id_izjava, napomena, id_kom, id_izradio, id_otprema, mj_otpreme, adr_otpreme, isprave, id_prijevoznik, registracija, istovarno_mj, istovarni_rok, troskovi_prijevoza, broj_otpremnice, partner_osoba, ukupno)");
            sql.Append("VALUES ");
            sql.Append("( ");
            sql.Append((r["godina_otpremnice"] == null || r["godina_otpremnice"].ToString().Length == 0 ? "NULL" : "'" + r["godina_otpremnice"].ToString() + "'") + ", ");
            sql.Append((r["id_skladiste"] == null || r["id_skladiste"].ToString().Length == 0 ? "NULL" : "'" + r["id_skladiste"].ToString() + "'") + ", ");
            sql.Append((r["osoba_partner"] == null || r["osoba_partner"].ToString().Length == 0 ? "NULL" : "'" + r["osoba_partner"].ToString() + "'") + ", ");
            sql.Append((r["id_odrediste"] == null || r["id_odrediste"].ToString().Length == 0 ? "NULL" : "'" + r["id_odrediste"].ToString() + "'") + ", ");
            sql.Append((r["vrsta_dok"] == null || r["vrsta_dok"].ToString().Length == 0 ? "NULL" : "'" + r["vrsta_dok"].ToString() + "'") + ", ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).ToString() + "', ");
            sql.Append((r["id_izjava"] == null || r["id_izjava"].ToString().Length == 0 ? "NULL" : "'" + r["id_izjava"].ToString() + "'") + ", ");
            sql.Append((r["napomena"] == null || r["napomena"].ToString().Length == 0 ? "NULL" : "'" + r["napomena"].ToString() + "'") + ", ");
            sql.Append((r["id_komercijalista"] == null || r["id_komercijalista"].ToString().Length == 0 ? "NULL" : "'" + r["id_komercijalista"].ToString() + "'") + ", ");
            sql.Append((r["id_izradio"] == null || r["id_izradio"].ToString().Length == 0 ? "NULL" : "'" + r["id_izradio"].ToString() + "'") + ", ");
            sql.Append((r["otprema"] == null || r["otprema"].ToString().Length == 0 ? "NULL" : "'" + r["otprema"].ToString() + "'") + ", ");
            sql.Append((r["mj_otpreme"] == null || r["mj_otpreme"].ToString().Length == 0 ? "NULL" : "'" + r["mj_otpreme"].ToString() + "'") + ", ");
            sql.Append((r["adr_otpreme"] == null || r["adr_otpreme"].ToString().Length == 0 ? "NULL" : "'" + r["adr_otpreme"].ToString() + "'") + ", ");
            sql.Append((r["isprave"] == null || r["isprave"].ToString().Length == 0 ? "NULL" : "'" + r["isprave"].ToString() + "'") + ", ");
            sql.Append((r["id_prijevoznik"] == null || r["id_prijevoznik"].ToString().Length == 0 ? "NULL" : "'" + r["id_prijevoznik"].ToString() + "'") + ", ");
            sql.Append((r["registracija"] == null || r["registracija"].ToString().Length == 0 ? "NULL" : "'" + r["registracija"].ToString() + "'") + ", ");
            sql.Append((r["istovarno_mj"] == null || r["istovarno_mj"].ToString().Length == 0 ? "NULL" : "'" + r["istovarno_mj"].ToString() + "'") + ", ");
            sql.Append((r["istovarni_rok"] == null || r["istovarni_rok"].ToString().Length == 0 ? "NULL" : "'" + r["istovarni_rok"].ToString() + "'") + ", ");
            sql.Append((r["troskovi_prijevoza"] == null || r["troskovi_prijevoza"].ToString().Length == 0 ? "NULL" : "'" + r["troskovi_prijevoza"].ToString() + "'") + ", ");
            sql.Append((r["broj_otpremnice"] == null || r["broj_otpremnice"].ToString().Length == 0 ? "NULL" : "'" + r["broj_otpremnice"].ToString() + "'") + ", ");
            sql.Append((r["partner_osoba"] == null || r["partner_osoba"].ToString().Length == 0 ? "NULL" : "'" + r["partner_osoba"].ToString() + "'") + ", ");
            sql.Append("'" + r["ukupno"].ToString().Replace(".", ",") + "'");
            sql.Append(");");

            SqlPostgres.insert(sql.ToString());
        }

        private void SpremiStavke(DataRow r)
        {
            decimal kolicina = 0, rabat = 0, vpc = 0, porez = 0, nbc = 0, mpc = 0, porez_potrosnja = 0;

            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
            decimal.TryParse(r["porez_potrosnja"].ToString().Replace(".", ","), out porez_potrosnja);

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO otpremnica_stavke");
            sb.Append(" (");
            sb.Append(" ");
            sb.Append("kolicina, vpc, porez, broj_otpremnice, rabat, id_skladiste, sifra_robe, oduzmi, godina_otpremnice, odjava, nbc,");
            sb.Append("\n\t");
            sb.Append("porez_potrosnja, naplaceno_fakturom");
            sb.Append(" )\n");
            sb.Append("VALUES ");
            sb.Append("(");
            sb.Append(" ");
            sb.Append("'" + Math.Round(kolicina, 6).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(vpc, 6).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + porez.ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + r["broj_otpremnice"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(rabat, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + r["id_skladiste"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["sifra"].ToString() + "',");
            sb.Append(" ");
            sb.Append("CASE WHEN " + r["oduzmi"].ToString() + " = 1 THEN 'DA' ELSE 'NE' END,");
            sb.Append(" ");
            sb.Append("'" + r["godina_otpremnice"] + "',");
            sb.Append(" ");
            sb.Append("'" + r["odjava"] + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(nbc, 4).ToString().Replace(",", ".") + "',");
            sb.Append("\n\t");
            sb.Append("'" + Math.Round(porez_potrosnja, 2).ToString().Replace(",", ".") + "',");
            //sb.Append(" ");
            //sb.Append("'" + Util.Korisno.idDucan + "',");
            sb.Append(" ");
            sb.Append("" + false + "\n");
            sb.Append(")");
            sb.Append(";");

            SqlPostgres.insert(sb.ToString());
        }

        #endregion PRIMI PODATKE
    }
}