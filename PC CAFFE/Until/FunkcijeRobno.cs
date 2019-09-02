using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Until
{
    internal class FunkcijeRobno
    {
        private DataTable DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private string poslovnica = "1";

        public FunkcijeRobno()
        {
            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }
        }

        #region PostaviStanjeSkladista

        public void PostaviStanjeSkladista()
        {
            try
            {
                if (DSpostavke.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() != "1") { return; }

                string query = "UPDATE roba_prodaja SET kolicina=" +
                                "(SELECT" +
                                " REPLACE(CAST(ROUND((coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra=pocetno.sifra),0)" +
                                "+coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='False'),0)" +
                                "+coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='True'),0)" +

                                "+coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)-kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke" +
                                " LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure" +
                                " WHERE roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje='0'),0)" +

                                "-coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra),0)" +

                                "-coalesce((SELECT " +
                                    "SUM( CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra),0)) " +
                                " FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra),0)" +

                                "-coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice='" + poslovnica + "' AND medu_poslovnice.sifra=roba_prodaja.sifra),0)" +
                                "+coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu='" + poslovnica + "' AND medu_poslovnice.sifra=roba_prodaja.sifra),0)" +

                                "),5) as character varying),'.',',')" +

                                ");";

                query = string.Format(@"UPDATE roba_prodaja SET kolicina=
                                (select replace(cast(round(x.pocetno + x.primka + x.kalkulacija + x.inventura - x.otpis - x.racuni - x.fakture - x.otpremnica + (x.ulaz_ms - x.izlaz_ms) - x.izdatnica, 6) as character varying), '.',',') AS kolicina
from(
    SELECT roba_prodaja.sifra, roba_prodaja.naziv, coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra = pocetno.sifra and roba_prodaja.id_skladiste = pocetno.id_skladiste), 0) AS pocetno,
    coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra = primka_stavke.sifra AND is_kalkulacija = 'False' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS primka,
        coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra = primka_stavke.sifra AND is_kalkulacija = 'True' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS kalkulacija,
            coalesce((SELECT SUM(replace(kolicina, ',', '.')::numeric) AS izd FROM izdatnica_stavke left join izdatnica on izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica WHERE roba_prodaja.sifra = izdatnica_stavke.sifra and roba_prodaja.id_skladiste = izdatnica.id_skladiste),0) AS izdatnica,
               coalesce((SELECT SUM(CAST(REPLACE(kolicina, ',', '.') AS numeric) - kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure = inventura_stavke.broj_inventure WHERE roba_prodaja.sifra = inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje = '0' and roba_prodaja.id_skladiste = inventura.id_skladiste),0) as inventura,
	coalesce((SELECT SUM(CAST(REPLACE(kolicina, ',', '.') AS numeric)) AS kol FROM povrat_robe_stavke left join povrat_robe on povrat_robe_stavke.broj = povrat_robe.broj WHERE roba_prodaja.sifra = povrat_robe_stavke.sifra and roba_prodaja.id_skladiste = povrat_robe.id_skladiste),0) as otpis,
	coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe = caffe_normativ.sifra), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS racuni,
               coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric)) FROM faktura_stavke WHERE faktura_stavke.sifra = caffe_normativ.sifra), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS fakture,
                      coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(otpremnica_stavke.kolicina) FROM otpremnica_stavke WHERE otpremnica_stavke.sifra_robe = caffe_normativ.sifra and otpremnica_stavke.naplaceno_fakturom = false), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS otpremnica,

                             (SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice = '{0}' AND medu_poslovnice.sifra = roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS izlaz_ms,

                                 (SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu = '{0}' AND medu_poslovnice.sifra = roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS ulaz_ms

    FROM roba_prodaja
) x
where x.sifra = roba_prodaja.sifra

);", poslovnica);

                classSQL.update(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion PostaviStanjeSkladista

        #region PostaviStanjeSkladistaPremaSifri

        public void PostaviStanjeSkladistaPremaSifri(string sifra, string skladiste)
        {
            try
            {
                if (DSpostavke.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() != "1") { return; }

                string query = string.Format(@"UPDATE roba_prodaja SET kolicina=
(SELECT
REPLACE(CAST((coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra=pocetno.sifra),0)
+ coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='False'),0)
+ coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='True'),0)
- coalesce((SELECT SUM(replace(kolicina, ',','.')::numeric) AS kol FROM izdatnica_stavke WHERE roba_prodaja.sifra = izdatnica_stavke.sifra),0)
-coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra),0)
+coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)-kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke
LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure
WHERE roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje='0'),0)
-coalesce((SELECT
SUM( CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra),0))
FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra),0)
-coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice = '{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra),0)
+coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu = '{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra),0)
) as character varying),'.',',')
) WHERE sifra IN
(SELECT sifra_normativ FROM caffe_normativ WHERE caffe_normativ.sifra = '{1}')
AND id_skladiste = '{2}';", poslovnica, sifra, skladiste);

                query = string.Format(@"UPDATE roba_prodaja SET kolicina=
                (select replace(cast(round(x.pocetno + x.primka + x.kalkulacija + x.inventura - x.otpis - x.racuni - x.fakture - x.otpremnica + (x.ulaz_ms - x.izlaz_ms) - x.izdatnica, 6) as character varying), '.',',') AS kolicina
from(
    SELECT roba_prodaja.sifra, roba_prodaja.naziv, coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra = pocetno.sifra and roba_prodaja.id_skladiste = pocetno.id_skladiste), 0) AS pocetno,
    coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra = primka_stavke.sifra AND is_kalkulacija = 'False' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS primka,
        coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra = primka_stavke.sifra AND is_kalkulacija = 'True' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS kalkulacija,
            coalesce((SELECT SUM(replace(kolicina, ',', '.')::numeric) AS izd FROM izdatnica_stavke left join izdatnica on izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica WHERE roba_prodaja.sifra = izdatnica_stavke.sifra and roba_prodaja.id_skladiste = izdatnica.id_skladiste),0) AS izdatnica,
               coalesce((SELECT SUM(CAST(REPLACE(kolicina, ',', '.') AS numeric) - kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure = inventura_stavke.broj_inventure WHERE roba_prodaja.sifra = inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje = '0' and roba_prodaja.id_skladiste = inventura.id_skladiste),0) as inventura,
	coalesce((SELECT SUM(CAST(REPLACE(kolicina, ',', '.') AS numeric)) AS kol FROM povrat_robe_stavke left join povrat_robe on povrat_robe_stavke.broj = povrat_robe.broj WHERE roba_prodaja.sifra = povrat_robe_stavke.sifra and roba_prodaja.id_skladiste = povrat_robe.id_skladiste),0) as otpis,
	coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe = caffe_normativ.sifra), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS racuni,
               coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric)) FROM faktura_stavke WHERE faktura_stavke.sifra = caffe_normativ.sifra), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS fakture,
                      coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina, ',', '.') AS numeric) * coalesce((SELECT SUM(otpremnica_stavke.kolicina) FROM otpremnica_stavke WHERE otpremnica_stavke.sifra_robe = caffe_normativ.sifra and otpremnica_stavke.naplaceno_fakturom = false), 0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS otpremnica,

                             (SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice = '{0}' AND medu_poslovnice.sifra = roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS izlaz_ms,

                                 (SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu = '{0}' AND medu_poslovnice.sifra = roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS ulaz_ms

    FROM roba_prodaja

    where roba_prodaja.id_skladiste = '{2}'
) x
where x.sifra = roba_prodaja.sifra
) WHERE sifra IN
(SELECT sifra_normativ FROM caffe_normativ WHERE caffe_normativ.sifra = '{1}')
AND id_skladiste = '{2}';", poslovnica, sifra, skladiste);

                classSQL.update(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion PostaviStanjeSkladistaPremaSifri

        private BackgroundWorker bw = new BackgroundWorker();

        #region ObavjestiAkoNaSkladistuImaManjeOdNule

        private string mess_za_minus = "";

        public void ObavjestiAkoNaSkladistuImaManjeOdNule(string sifra, string skladiste)
        {
            try
            {
                if (DSpostavke.Rows[0]["obavjeti_ako_nema_repromaterijala"].ToString() != "1") { return; }
                string query = "SELECT kolicina,sifra,naziv FROM roba_prodaja WHERE sifra IN (" +
                    "SELECT sifra_normativ FROM caffe_normativ WHERE caffe_normativ.sifra='" + sifra + "')";
                DataTable DT = classSQL.select(query, "rp").Tables[0];

                mess_za_minus = "Na skladištu nemate:\r\n";
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal kolicina;
                        decimal.TryParse(r["kolicina"].ToString(), out kolicina);

                        if (kolicina < 0)
                        {
                            mess_za_minus += r["naziv"].ToString() + " " + kolicina.ToString("#0.000") + "\r\n";
                        }
                    }

                    if (mess_za_minus.Length > 25)
                    {
                        MessageBox.Show(mess_za_minus, "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                        bw.RunWorkerAsync();
                        //bw.ProgressChanged +=new ProgressChangedEventHandler(bw_ProgressChanged);
                        //bw.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion ObavjestiAkoNaSkladistuImaManjeOdNule

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {/*
            Util.classMail.send_email("drazen0001@gmail.com", "", "drazen0001@gmail.com", "Stanje u minus", "mess_za_minus");
          * */
        }

        #region Postavi Nabavne Cijene TABLICE: (roba, roba prodaja, racuni)

        public void PostaviNabavneCijeneZaTablicuRoba()
        {
            //Postavljam nabavne cijene u PRODAJNE artikle
            string sql = "UPDATE roba SET nbc=coalesce((" +
                    "SELECT SUM(" +
                        "CAST(REPLACE(caffe_normativ.kolicina,',','.') AS NUMERIC)*coalesce((SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra=caffe_normativ.sifra_normativ LIMIT 1),0)" +
                  ") FROM caffe_normativ WHERE caffe_normativ.sifra=roba.sifra" +
                "),0)";
            classSQL.update(sql);
        }

        public void PostaviNabavneCijeneZaTablicuRobaPremaSifri(string sifra)
        {
            //Postavljam nabavne cijene u PRODAJNE artikle prema šifri
            string sql = "UPDATE roba SET nbc=coalesce((" +
                    "SELECT SUM(" +
                        "CAST(REPLACE(caffe_normativ.kolicina,',','.') AS NUMERIC)*coalesce((SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra=caffe_normativ.sifra_normativ LIMIT 1),0)" +
                  ") FROM caffe_normativ WHERE caffe_normativ.sifra=roba.sifra" +
                "),0) " +
                "WHERE roba.sifra IN (SELECT sifra FROM caffe_normativ WHERE sifra_normativ='" + sifra + "')";
            classSQL.update(sql);
        }

        public void PostaviNabavneCijeneZaTablicuRacuni()
        {
            //Postavljam nabavne cijene u računu
            string sql = "UPDATE racun_stavke SET nbc=coalesce((" +
                "SELECT nbc FROM roba WHERE roba.sifra=racun_stavke.sifra_robe LIMIT 1" +
                "),0)";
            classSQL.update(sql);
        }

        public void PostaviNabavneCijeneZaTablicuRobaProdaja()
        {
            //Postavljam prosječne cijene u normativ
            string sql = @"UPDATE roba_prodaja SET nc=
                            ROUND((CASE WHEN
	                            COALESCE((
	                            SELECT cijena_po_komadu FROM primka_stavke
	                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
	                            WHERE primka.datum<'@datum' AND primka_stavke.sifra=roba_prodaja.sifra ORDER BY primka.datum DESC LIMIT 1
	                            ),0)<>0
                            THEN
	                            COALESCE((
		                            SELECT cijena_po_komadu FROM primka_stavke
		                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
		                            WHERE primka.datum<'@datum' AND primka_stavke.sifra=roba_prodaja.sifra ORDER BY primka.datum DESC LIMIT 1
	                            ),0)
                            WHEN
	                            COALESCE((
	                            SELECT nc FROM pocetno WHERE pocetno.sifra=roba_prodaja.sifra AND pocetno.id_skladiste=roba_prodaja.id_skladiste LIMIT 1
	                            ),0)<>0
                            THEN
	                            COALESCE((
	                            SELECT nc FROM pocetno WHERE pocetno.sifra=roba_prodaja.sifra AND pocetno.id_skladiste=roba_prodaja.id_skladiste LIMIT 1
	                            ),0)
                            ELSE
	                            COALESCE((
	                            SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra=roba_prodaja.sifra LIMIT 1
	                            ),0)
                            END),4), editirano = '1';";

            sql = sql.Replace("@datum", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));

            classSQL.update(sql);
            //MessageBox.Show("Roba prodaja riješena.");
        }

        public void PostaviNabavneCijeneRoba()
        {
            string query = @"UPDATE roba SET nbc=
                                ROUND((COALESCE((
                                          SELECT SUM(
		                                CAST(REPLACE(caffe_normativ.kolicina,',','.') AS NUMERIC)*

		                                (SELECT
                                                            CASE WHEN
	                                                            COALESCE((
	                                                            SELECT cijena_po_komadu FROM primka_stavke
	                                                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
	                                                            WHERE primka.datum<'@datum' AND primka_stavke.sifra=caffe_normativ.sifra_normativ ORDER BY primka.datum DESC LIMIT 1
	                                                            ),0)<>0
                                                            THEN
	                                                            COALESCE((
		                                                            SELECT cijena_po_komadu FROM primka_stavke
		                                                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
		                                                            WHERE primka.datum<'@datum' AND primka_stavke.sifra=caffe_normativ.sifra_normativ ORDER BY primka.datum DESC LIMIT 1
	                                                            ),0)
                                                            WHEN
	                                                            COALESCE((
	                                                            SELECT nc FROM pocetno WHERE pocetno.sifra=caffe_normativ.sifra_normativ LIMIT 1
	                                                            ),0)<>0
                                                            THEN
	                                                            COALESCE((
	                                                            SELECT nc FROM pocetno WHERE pocetno.sifra=caffe_normativ.sifra_normativ LIMIT 1
	                                                            ),0)
                                                            ELSE
	                                                            COALESCE((
	                                                            SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra=caffe_normativ.sifra_normativ
	                                                            ),0)
                                                            END)

                                          ) FROM caffe_normativ WHERE caffe_normativ.sifra=roba.sifra
                                         ),0)),4) ;";

            query = query.Replace("@datum", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
            classSQL.update(query);
            //MessageBox.Show("Roba riješena.");
        }

        public void UzmiNabavneCijeneRacuni()
        {
            string query = @"UPDATE racun_stavke SET nbc=
                    ROUND(COALESCE((
                      SELECT SUM(
		            CAST(REPLACE(caffe_normativ.kolicina,',','.') AS NUMERIC)*

		                    (SELECT
                                            CASE WHEN
	                                            COALESCE((
	                                            SELECT cijena_po_komadu FROM primka_stavke
	                                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
	                                            WHERE primka.datum<t.datum_racuna AND primka_stavke.sifra=caffe_normativ.sifra_normativ ORDER BY primka.datum DESC LIMIT 1
	                                            ),0)<>0
                                            THEN
	                                            COALESCE((
		                                            SELECT cijena_po_komadu FROM primka_stavke
		                                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
		                                            WHERE primka.datum<t.datum_racuna AND primka_stavke.sifra=caffe_normativ.sifra_normativ ORDER BY primka.datum DESC LIMIT 1
	                                            ),0)
                                            WHEN
	                                            COALESCE((
	                                            SELECT cijena FROM inventura_stavke
	                                            LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure AND godina='2014'
	                                            WHERE inventura.datum<t.datum_racuna AND inventura_stavke.sifra_robe=caffe_normativ.sifra_normativ ORDER BY inventura.datum DESC LIMIT 1
	                                            ),0)<>0
                                            THEN
	                                            COALESCE((
	                                            SELECT cijena FROM inventura_stavke
	                                            LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure AND godina='2014'
	                                            WHERE inventura.datum<t.datum_racuna AND inventura_stavke.sifra_robe=caffe_normativ.sifra_normativ ORDER BY inventura.datum DESC LIMIT 1
	                                            ),0)
                                            ELSE
	                                            COALESCE((
	                                            SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra=caffe_normativ.sifra_normativ
	                                            ),0)
                                            END)

                          ) FROM caffe_normativ WHERE caffe_normativ.sifra=racun_stavke.sifra_robe
                         ),0),4)

		    FROM (SELECT a.broj_racuna,racuni.datum_racuna FROM racun_stavke a JOIN racuni ON a.broj_racuna=racuni.broj_racuna) t
            WHERE racun_stavke.broj_racuna = t.broj_racuna;";

            classSQL.update(query);
            MessageBox.Show("Računi riješeni.");
        }

        public decimal UzmiNabavnuCijenuRetrogradno(string sifra, DateTime datum)
        {
            string query = @"SELECT
                            ROUND((CASE WHEN
	                            COALESCE((
	                            SELECT cijena_po_komadu FROM primka_stavke
	                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
	                            WHERE primka.datum<'@datum' AND primka_stavke.sifra='@sifra' ORDER BY primka.datum DESC LIMIT 1
	                            ),0)<>0
                            THEN
	                            COALESCE((
		                            SELECT cijena_po_komadu FROM primka_stavke
		                            LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.id_skladiste=primka_stavke.id_skladiste
		                            WHERE primka.datum<'@datum' AND primka_stavke.sifra='@sifra' ORDER BY primka.datum DESC LIMIT 1
	                            ),0)
                            WHEN
	                            COALESCE((
	                            SELECT cijena FROM inventura_stavke
	                            LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure AND godina='2014'
	                            WHERE inventura.datum<'@datum' AND inventura_stavke.sifra_robe='@sifra' ORDER BY inventura.datum DESC LIMIT 1
	                            ),0)<>0
                            THEN
	                            COALESCE((
	                            SELECT cijena FROM inventura_stavke
	                            LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure AND godina='2014'
	                            WHERE inventura.datum<'@datum' AND inventura_stavke.sifra_robe='@sifra' ORDER BY inventura.datum DESC LIMIT 1
	                            ),0)
                            ELSE
	                            COALESCE((
	                            SELECT nc FROM roba_prodaja WHERE roba_prodaja.sifra='@sifra'
	                            ),0)
                            END),4);";

            query = query.Replace("@sifra", sifra);
            query = query.Replace("@datum", datum.ToString("yyyy-MM-dd H:mm:ss"));
            DataTable DT = classSQL.select(query, "nbc").Tables[0];

            decimal nbc = 0;
            if (DT.Rows.Count > 0)
            {
                decimal.TryParse(DT.Rows[0][0].ToString(), out nbc);
                return nbc;
            }

            return 0;
        }

        #endregion Postavi Nabavne Cijene TABLICE: (roba, roba prodaja, racuni)

        //"-coalesce((SELECT " +
        //"    SUM((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra)) " +
        //" FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra),0)" +
    }
}