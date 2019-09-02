using PCPOS.PosPrint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmMaliPrinter : Form
    {
        public frmMaliPrinter()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string blagajna { get; set; }
        public string podgrupa { get; set; }
        public string grupa { get; set; }
        public string ducan { get; set; }
        public bool ispis_stavka { get; set; }
        public bool ispis_sifra { get; set; }

        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        private int RecLineChars;
        private decimal ukupno_popust = 0;

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private string tekst = "";

        private void frmMaliPrinter_Load(object sender, EventArgs e)
        {
            ukupno_popust = 0;
            switch (dokumenat)
            {
                case "PrometRobe":
                    RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
                    PrometProdajneRobe();
                    break;

                case "PorezNaPotrosnju":
                    RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
                    PorezNaPotrosnju();
                    break;
            }
        }

        private void PorezNaPotrosnju()
        {
            decimal pnpUKUPNO = 0;

            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            PrintTextLine("Adresa: " + DT_tvr.Rows[0]["adresa"].ToString());
            if (DT_tvr.Rows[0]["poslovnica_adresa"].ToString() != "")
            {
                PrintTextLine(DT_tvr.Rows[0]["poslovnica_adresa"].ToString());
            }
            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
            PrintTextLine("OD: " + datumOD.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine("DO: " + datumDO.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine(new string('-', RecLineChars));

            string sql = "SELECT (SELECT SUM( " +
                "((CAST(racun_stavke.mpc AS numeric)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric))*(1-CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric)/100)) " +
                "*((100*CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS numeric))/(100 zbroj CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS numeric) zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)) / 100)) " +
                "FROM racun_stavke LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe " +
                "LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna WHERE roba.id_grupa=grupa.id_grupa AND " +
                " racuni.datum_racuna BETWEEN '" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "') as pnp, grupa.grupa " +
                "FROM grupa";

            sql = @"drop table if exists tempRacuni;
create temp table tempRacuni as
select broj_racuna::bigint as broj, id_ducan as id_poslovnica, id_kasa as id_naplatni, datum_racuna as datum, 'rac'::varchar as doc
from racuni
where datum_racuna BETWEEN '" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + @"' AND '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + @"';

drop table if exists tempFakture;
create temporary table tempFakture as
select broj_fakture as broj, id_ducan as id_poslovnica, id_kasa as id_naplatni, date as datum, 'fak'::varchar as doc
from fakture
where date BETWEEN '" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + @"' AND '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + @"';

drop table if exists tempHead;
create temp table tempHead as
select * from tempRacuni
union
select * from tempFakture;

drop table if exists tempRacuniStavke;
create temp table tempRacuniStavke as
select r.broj, sifra_robe as sifra, mpc::numeric as mpc, replace(kolicina, ',','.')::numeric as kolicina, replace(porez, ',','.')::numeric as pdv, replace(rabat, ',','.')::numeric as rabat, replace(porez_potrosnja, ',','.')::numeric as pp, 'rac'::varchar as doc
from racun_stavke rs
right join tempRacuni r on r.broj = rs.broj_racuna::bigint and r.id_poslovnica = rs.id_ducan and r.id_naplatni = rs.id_blagajna;

drop table if exists tempFaktureStavke;
create temp table tempFaktureStavke as
select f.broj, sifra as sifra, mpc as mpc, replace(kolicina, ',','.')::numeric as kolicina, replace(porez, ',','.')::numeric as pdv, replace(rabat, ',','.')::numeric as rabat, replace(porez_potrosnja, ',','.')::numeric as pp, 'fak'::varchar as doc
from faktura_stavke fs
right join tempFakture f on f.broj = fs.broj_fakture::bigint;

drop table if exists tempChild;
create temp table tempChild as
select * from tempRacuniStavke
union all
select * from tempFaktureStavke;

select coalesce((SELECT SUM(((rs.mpc *
	rs.kolicina) *
	(1 - rs.rabat / 100)) *
	((100 * rs.pp) /
	(100 zbroj rs.pp zbroj rs.pdv) / 100))), 0) as pnp, g.grupa
from tempHead r
left join tempChild rs on r.broj = rs.broj and r.doc = rs.doc
left join roba ro on rs.sifra = ro.sifra
left join grupa g on ro.id_grupa = g.id_grupa
group by g.grupa
order by g.grupa;";

            DataTable DT = classSQL.select(sql, "pnppg").Tables[0];

            foreach (DataRow r in DT.Rows)
            {
                decimal pnp;
                decimal.TryParse(r["pnp"].ToString(), out pnp);

                if (pnp != 0)
                {
                    int praznina = RecLineChars - pnp.ToString("#0.000").Length - ("PNP " + r["grupa"].ToString() + ":").Length;

                    if (praznina < 0)
                    {
                        int i = 0;
                        while (praznina < 0)
                        {
                            i++;
                            praznina = RecLineChars - pnp.ToString("#0.000").Length - ("PNP " + r["grupa"].ToString().Remove(r["grupa"].ToString().Length - i) + ":").Length;
                        }
                        PrintTextLine("PNP " + r["grupa"].ToString().Remove(r["grupa"].ToString().Length - i) + ":" + new string(' ', praznina) + pnp.ToString("#0.000"));
                    }
                    else
                    {
                        PrintTextLine("PNP " + r["grupa"].ToString() + ":" + new string(' ', praznina) + pnp.ToString("#0.000"));
                    }
                    pnpUKUPNO += pnp;
                }
            }

            PrintTextLine(new string('-', RecLineChars));
            PrintTextLine("PNP ukupno:" + new string(' ', RecLineChars - pnpUKUPNO.ToString("#0.000").Length - "PNP ukupno:".Length) + pnpUKUPNO.ToString("#0.000"));

            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);

            rtb.Font = font;
            rtb.Text = tekst;
        }

        private DataTable DTpdv = new DataTable();
        private DataTable DTartikli = new DataTable();
        private DataRow RowPdv;
        private DataRow RowOsnovica;
        private DataRow RowArtikl;

        private void StopePDVa(decimal pdv, decimal pdv_stavka)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
            }
        }

        private void Artikli(string artikl, decimal kolicina, string sifra, string mpc)
        {
            DataRow[] dataROW = DTartikli.Select("sifra = '" + sifra + "' AND mpc='" + mpc + "'");

            if (dataROW.Count() == 0)
            {
                RowArtikl = DTartikli.NewRow();
                RowArtikl["sifra"] = sifra;
                RowArtikl["mpc"] = mpc;
                RowArtikl["kolicina"] = kolicina.ToString();
                RowArtikl["naziv"] = artikl;
                DTartikli.Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["kolicina"] = Convert.ToDecimal(dataROW[0]["kolicina"].ToString()) + kolicina;
            }
        }

        private decimal ukupno_rabat = 0;

        /// <summary>
        /// 
        /// </summary>
        private void PrometProdajneRobe()
        {
            decimal popustArtikl5000 = 0;
            string skl = "";
            if (podgrupa != null)
            {
                skl = " AND roba.id_podgrupa='" + podgrupa + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string blagajnaUvijet = "";
            if (blagajna != null)
            {
                blagajnaUvijet = " AND racuni.id_kasa='" + blagajna + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string gr = "";
            if (grupa != null)
            {
                gr = " AND grupa.id_grupa='" + grupa + "'";
            }

            string query = $@"SELECT CAST(racun_stavke.broj_racuna AS int)
	                        ,roba.naziv 
	                        ,racun_stavke.kolicina
	                        ,racun_stavke.sifra_robe
	                        ,CAST(racun_stavke.mpc AS numeric)
	                        ,racun_stavke.porez_potrosnja
	                        ,racun_stavke.porez
	                        ,racun_stavke.rabat
	                        ,racuni.nacin_placanja
                        FROM racun_stavke 
                        LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna AND racuni.godina=racun_stavke.godina 
                        LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe 
                        LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa 
                        WHERE racuni.datum_racuna>'{datumOD.ToString("yyyy-MM-dd H:mm:ss")}' AND racuni.datum_racuna<'{datumDO.ToString("yyyy-MM-dd H:mm:ss")}' {skl + blag + duc + art + gr + blagajnaUvijet}
                        UNION ALL
                        SELECT faktura_stavke.broj_fakture
	                        ,roba.naziv 
	                        ,faktura_stavke.kolicina
	                        ,faktura_stavke.sifra AS sifra_robe
	                        ,CAST(faktura_stavke.mpc AS numeric)
	                        ,faktura_stavke.porez_potrosnja
	                        ,faktura_stavke.porez
	                        ,faktura_stavke.rabat
	                        ,'F' AS nacin_placanja
                        FROM faktura_stavke
                        LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture
                        LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra
                        LEFT JOIN grupa ON grupa.id_grupa = roba.id_grupa
                        WHERE fakture.date>'{datumOD.ToString("yyyy-MM-dd H:mm:ss")}' AND fakture.date<'{datumDO.ToString("yyyy-MM-dd H:mm:ss")}'
                        UNION ALL
                        SELECT otpremnica_stavke.broj_otpremnice
	                        ,roba.naziv 
	                        ,CAST(otpremnica_stavke.kolicina AS varchar)
	                        ,otpremnica_stavke.sifra_robe
	                        ,otpremnica_stavke.vpc AS mpc
	                        ,CAST(otpremnica_stavke.porez_potrosnja AS varchar)
	                        ,CAST(otpremnica_stavke.porez AS varchar)
	                        ,CAST(otpremnica_stavke.rabat AS varchar)
	                        ,'OT' AS nacin_placanja
                        FROM otpremnica_stavke
                        LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice
                        LEFT JOIN roba ON roba.sifra = otpremnica_stavke.sifra_robe
                        LEFT JOIN grupa ON grupa.id_grupa = roba.id_grupa
                        WHERE otpremnice.datum>'{datumOD.ToString("yyyy-MM-dd H:mm:ss")}' AND otpremnice.datum<'{datumDO.ToString("yyyy-MM-dd H:mm:ss")}'";

            DataTable DTracuni = classSQL.select(query, "racun_stavke").Tables[0];

            string popustiQuery = "SELECT " +
                " count(*) as count," +
                " racun_stavke.broj_racuna" +
                " FROM racun_stavke" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna AND racuni.godina=racun_stavke.godina" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
                " WHERE  racuni.datum_racuna>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " " + skl + blag + duc + art + gr + blagajnaUvijet + " GROUP BY racun_stavke.broj_racuna HAVING count(*) > 1";
            DataTable DTpopusti = classSQL.select(popustiQuery, "racun_stavke").Tables[0];

            decimal rabat = 0;
            decimal kol = 0;
            decimal pnp = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal pnpUKUPNO = 0;
            decimal pdvUKUPNO = 0;
            decimal SVE_UKUPNO = 0;
            decimal OSNOVICA = 0;
            string g = "";

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
            }

            if (DTartikli.Columns["sifra"] == null)
            {
                DTartikli.Columns.Add("sifra");
                DTartikli.Columns.Add("kolicina");
                DTartikli.Columns.Add("mpc");
                DTartikli.Columns.Add("naziv");
            }

            if (DT_tvr.Rows[0]["skraceno_ime"].ToString().Contains(";"))
            {
                string[] adresas = DT_tvr.Rows[0]["skraceno_ime"].ToString().Split(';');

                foreach (string item in adresas)
                {
                    PrintTextLine(item);
                }
            }
            else
            {
                PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            }

            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
            PrintTextLine("OD: " + datumOD.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine("DO: " + datumDO.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine(new string('-', RecLineChars));

            if (DTpdvN.Columns["stopa"] == null)
            {
                DTpdvN.Columns.Add("stopa");
                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
            }
            else
            {
                DTpdvN.Clear();
            }

            // Ukupne vrijednosti
            decimal UG = 0;
            decimal UGSP = 0;
            decimal UK = 0;
            decimal UKBP = 0;
            decimal UP = 0;
            decimal UKSP = 0;
            decimal UF = 0;
            decimal UOT = 0;
            decimal UV = 0;
            decimal UO = 0;
            List<string> listaId = new List<string>();
            string[] oibZaPopust = new string[] { "67660751355", "99731647274", "38799206384" };

            foreach (DataRow row in DTpopusti.Rows)
            {
                listaId.Add(row["broj_racuna"].ToString());
            }

            foreach (DataRow row in DTracuni.Rows)
            {
                if (!oibZaPopust.Contains(Util.Korisno.oibTvrtke) || (oibZaPopust.Contains(Util.Korisno.oibTvrtke)))
                {
                    decimal.TryParse(row["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out kol);
                    decimal.TryParse(row["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mpc);
                    decimal.TryParse(row["porez_potrosnja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out pnp);
                    decimal.TryParse(row["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out pdv);
                    decimal.TryParse(row["rabat"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out rabat);

                    if (row["nacin_placanja"].ToString() == "OT")
                    {
                        DataTable DTroba = Global.Database.GetRoba(row["sifra_robe"].ToString());
                        if (DTroba?.Rows.Count > 0)
                            decimal.TryParse(DTroba.Rows[0]["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mpc);
                    }

                    mpc = mpc - (mpc * rabat / 100);
                    ukupno_rabat = ((mpc * rabat / 100) * kol) + ukupno_rabat;

                    //Ovaj kod dobiva PDV
                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
                    decimal ppdv = (((mpc * kol) * PreracunataStopaPDV) / 100);
                    pdvUKUPNO = ppdv + pdvUKUPNO;

                    //Ovaj kod dobiva porez na potrošnju
                    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
                    decimal ppnp = (((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100);
                    pnpUKUPNO = ppnp + pnpUKUPNO;

                    SVE_UKUPNO = (mpc * kol) + SVE_UKUPNO;

                    if (row["nacin_placanja"].ToString() == "G")
                    {
                        if (row["sifra_robe"].ToString() != "5000")
                        {
                            StopePDVaN(pdv, ppdv, "G", ((mpc * kol) - ((ppdv) + (ppnp))));
                            UG = (mpc * kol) + UG;
                            UKBP = (mpc * kol) + UKBP;
                        }
                        else
                        {
                            UGSP = (mpc * kol) + UGSP;
                            UP = (mpc * kol) + UP;
                        }

                    }
                    else if (row["nacin_placanja"].ToString() == "K")
                    {
                        if (row["sifra_robe"].ToString() != "5000")
                        {
                            StopePDVaN(pdv, ppdv, "K", ((mpc * kol) - ((ppdv) + (ppnp))));
                            UK = (mpc * kol) + UK;
                            UKBP = (mpc * kol) + UKBP;
                        }
                        else
                        {
                            UKSP = (mpc * kol) + UKSP;
                            UP = (mpc * kol) + UP;
                        }
                    }
                    else if (row["nacin_placanja"].ToString() == "F")
                    {
                        StopePDVaN(pdv, ppdv, "F", ((mpc * kol) - ((ppdv) + (ppnp))));
                        UF += (mpc * kol);
                    }
                    else if (row["nacin_placanja"].ToString() == "OT")
                    {
                        StopePDVaN(pdv, ppdv, "OT", ((mpc * kol) - ((ppdv) + (ppnp))));
                        UOT += (mpc * kol);
                    }
                    else if (row["nacin_placanja"].ToString() == "T")
                    {
                        StopePDVaN(pdv, ppdv, "T", ((mpc * kol) - ((ppdv) + (ppnp))));
                        UV = (mpc * kol) + UV;
                    }
                    else if (row["nacin_placanja"].ToString() == "O")
                    {
                        StopePDVaN(pdv, ppdv, "O", ((mpc * kol) - ((ppdv) + (ppnp))));
                        UO = (mpc * kol) + UO;
                    }

                    string ajjj = row["nacin_placanja"].ToString();

                    if (rabat > 0)
                        Artikli(row["naziv"].ToString(), kol, "R_" + row["sifra_robe"].ToString(), mpc.ToString());
                    else
                        Artikli(row["naziv"].ToString(), kol, row["sifra_robe"].ToString(), mpc.ToString());

                    StopePDVa(pdv, ((mpc * kol) * PreracunataStopaPDV) / 100);

                    OSNOVICA = ((mpc * kol) - ((ppdv) + (ppnp))) + OSNOVICA;
                }
                else
                {
                    decimal pa5000 = 0;
                    decimal ko5000 = 0;
                    if (decimal.TryParse(row["mpc"].ToString(), out pa5000) && decimal.TryParse(row["kolicina"].ToString(), out ko5000))
                    {
                        popustArtikl5000 += (pa5000 * ko5000);
                    }
                }
            }

            ukupno_rabat += (popustArtikl5000 < 0 ? (popustArtikl5000 * -1) : popustArtikl5000 * -1);

            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString()) - 2;
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString()) + 1;
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString()) + 1;

            if (ispis_stavka)
            {
                PrintTextLine(String.Empty);
                PrintText(TruncateAt("STAVKA".PadRight(a), a));
                PrintText(TruncateAt("KOL".PadLeft(k), k));
                PrintText(TruncateAt("CIJENA".PadLeft(c), c));
                PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
                PrintText("\r\n");
                PrintTextLine(new string('=', RecLineChars));

                if (DTartikli.Rows.Count > 0)
                {
                    DataView dv = DTartikli.DefaultView;
                    dv.Sort = "naziv";
                    DTartikli = dv.ToTable();
                }

                for (int i = 0; i < DTartikli.Rows.Count; i++)
                {
                    if (!oibZaPopust.Contains(Util.Korisno.oibTvrtke) || (oibZaPopust.Contains(Util.Korisno.oibTvrtke) && DTartikli.Rows[i]["sifra"].ToString() != "5000"))
                    {
                        try
                        {
                            if (ispis_sifra)
                            {
                                string stavka = DTartikli.Rows[i]["sifra"].ToString() + "/" + DTartikli.Rows[i]["naziv"].ToString();
                                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                                {
                                    PrintText(TruncateAt(stavka.PadRight(a), a));
                                }
                                else
                                {
                                    PrintTextLine(stavka.Length > 25 ? stavka.Substring(0, 25).TrimEnd() + "." : stavka);
                                    //tekst += classOstalo.SvavkaZaPrinter(stavka, a);
                                }
                            }
                            else
                            {
                                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                                {
                                    PrintText(TruncateAt(DTartikli.Rows[i]["naziv"].ToString().PadRight(a), a));
                                }
                                else
                                {
                                    tekst += classOstalo.SvavkaZaPrinter(DTartikli.Rows[i]["naziv"].ToString(), a);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            PrintText(TruncateAt(artikl.PadRight(a), a));
                        }

                        PrintText(TruncateAt(String.Empty.PadRight(a), a));
                        PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString()).ToString("#0.00").PadLeft(k), k));
                        PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()).ToString("#0.00").PadLeft(c), c));
                        PrintTextLine(TruncateAt((Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()) * Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString())).ToString("#0.00").PadLeft(s), s));
                    }
                }

                PrintTextLine("");
                PrintTextLine(new string('-', RecLineChars));
            }

            PrintTextLine("PNP ukupno:       " + pnpUKUPNO.ToString("#0.00"));

            // GOTOVINA
            if (UG > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO GOTOVINA:  " + UG.ToString("#0.00"));
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "G")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));
                    }
                }
            }

            // KARTICA
            if (UK > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO KARTICE:   " + UK.ToString("#0.00"));
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "K")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));

                        // TODO ispisati po karticama ako ih ima.

                        string sqlllol = string.Format(@"drop table if exists tempPoKarticama;
create temporary table tempPoKarticama as
select round(avg(karticaID)) as kartica, pdv as stopa, naziv, sum((mpc - (mpc*rabat/100)) * kolicina) as ukupno,
(sum((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * pdv) / (100 zbroj pdv zbroj pnp))) / 100)) as pdv,
(sum((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * pnp) / (100 zbroj pdv zbroj pnp))) / 100)) as pnp
from (
	select rs.sifra_robe as sifra, replace(rs.kolicina, ',','.')::numeric as kolicina, rs.mpc::numeric as mpc, replace(rs.porez_potrosnja, ',','.')::numeric as pnp, replace(rs.porez, ',','.')::numeric as pdv, replace(rs.rabat, ',','.')::numeric as rabat, r.nacin_placanja, r.karticaID, case when coalesce(k.naziv, '') = '' then 'Ostale kartice' else k.naziv end as naziv
	from racun_stavke rs
	left join racuni r on rs.broj_racuna = r.broj_racuna and rs.id_ducan = r.id_ducan and rs.id_blagajna = r.id_kasa
	left join kartice k on r.karticaID = k.id
    LEFT JOIN roba ro ON ro.sifra=rs.sifra_robe
	LEFT JOIN grupa g ON g.id_grupa=ro.id_grupa
	where r.datum_racuna between '" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + @".000000' and '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + @".999999'
" + skl.Replace("roba.", "ro.") + blag.Replace("racuni.", "r.") + duc.Replace("racuni.", "r.") + art.Replace("racun_stavke.", "rs.") + gr.Replace("grupa.", "g.") + blagajnaUvijet.Replace("racuni.", "r.") + @"
) prodano
where nacin_placanja = 'K'
group by naziv, pdv;

select *
from (
select *
from tempPoKarticama where kartica > 0
union
select *
from tempPoKarticama where kartica = 0
) x
where x.stopa = {0}", DTpdvN.Rows[i]["stopa"].ToString().Replace(',', '.'));

                        DataTable dtPoKarticama = classSQL.select(sqlllol, "racuni").Tables[0];
                        if (dtPoKarticama != null)
                        {
                            foreach (DataRow drRow in dtPoKarticama.Rows)
                            {
                                decimal osnovica = (Convert.ToDecimal(drRow["ukupno"].ToString()) - Convert.ToDecimal(drRow["pnp"].ToString()) - Convert.ToDecimal(drRow["pdv"].ToString()));
                                PrintTextLine("   " + drRow["naziv"]);
                                PrintTextLine("   OSNOVICA PDV " + Convert.ToDecimal(drRow["stopa"]).ToString("#0.00") + "%: " + osnovica.ToString("#0.00"));
                                PrintTextLine("   PDV " + Convert.ToDecimal(drRow["stopa"]).ToString("#0.00") + "%:          " + Convert.ToDecimal(drRow["pdv"].ToString()).ToString("#0.00"));
                            }
                        }
                    }
                }
            }

            // FAKTURE
            if (UF > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO FAKTURE:    " + UF.ToString("#0.00"));
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "F")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));
                    }
                }
            }

            // OTPREMNICE
            if (UOT > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO OTPREMNICE:    " + UOT.ToString("#0.00"));
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "OT")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:     " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:              " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));
                    }
                }
            }

            // VIRMAN
            if (UV > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO VIRMAN:    " + UV.ToString("#0.00"));
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "T")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));
                    }
                }
            }

            // OSTALO
            if (UO > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO OSTALO" + (Class.PodaciTvrtka.oibTvrtke == "98816793336" ? " - FOODEX" : "") + ":    " + UO.ToString("#0.00"));

                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "O")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.00"));
                    }
                }
            }

            PrintTextLine(new string('-', RecLineChars));
            if (!oibZaPopust.Contains(Util.Korisno.oibTvrtke))
            {
                PrintTextLine("OSNOVICA UKUPNO:  " + OSNOVICA.ToString("#0.00"));
            }
            else
            {
                PrintTextLine("UKUPNO BEZ POPUSTA:  " + UKBP.ToString("#0.00"));
            }
            for (int i = 0; i < DTpdv.Rows.Count; i++)
            {
                PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "% UKUPNO:     " + Math.Truncate(Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()) * 100) / 100);
            }

            string queryPopust = $@"";
            DataTable DTpopust = classSQL.select(query, "racun_stavke").Tables[0];

            if (!oibZaPopust.Contains(Util.Korisno.oibTvrtke))
            {
                PrintTextLine("UKUPNO RABAT:     " + ukupno_rabat.ToString("#0.00"));
                PrintTextLine("SVE UKUPNO:       " + SVE_UKUPNO.ToString("#0.00"));
            }
            else
            {
                PrintTextLine("UKUPNO POPUST:      " + (UP * -1).ToString("#0.00"));
                if (UGSP < 0)
                {
                    decimal ukupnoGotovinaSaPopustom = UG + UGSP;
                    if (ukupnoGotovinaSaPopustom > 0)
                        PrintTextLine("UKUPNO GOTOVINA SA POPUSTOM:  " + ukupnoGotovinaSaPopustom.ToString("#0.00"));
                }
                if (UGSP < 0)
                {
                    decimal ukupnoKarticaSaPopustom = UK + UKSP;
                    if (ukupnoKarticaSaPopustom > 0)
                        PrintTextLine("UKUPNO KARTICA SA POPUSTOM: " + ukupnoKarticaSaPopustom.ToString("#0.00"));
                }
                PrintTextLine("UKUPNO S POPUSTOM:  " + (SVE_UKUPNO - ukupno_rabat).ToString("#0.00"));
            }

            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);

            rtb.Font = font;
            rtb.Text = tekst;
        }

        private DataTable DTpdvN = new DataTable();

        private bool CheckIfContains(List<string> lista, string brojRacuna)
        {
            return lista.Contains(brojRacuna);
        }

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = osnovica;
                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

            //header
            String drawString = tekst;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(tekst, drawFont);

            drawFont = font;
            float y = 0.0F;
            float x = 0.0F;

            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

            float height = float.Parse(stringSize.Height.ToString());
            if (e.HasMorePages)
            {
                e.HasMorePages = false;
            }

            if (height > e.PageSettings.PaperSize.Height)
            {
                PaperSize psNew = new System.Drawing.Printing.PaperSize("Racun", e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize.Width, (int)height + 1);
                Size sSize = new Size(psNew.Width, psNew.Height);

                e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = psNew;
                e.PageSettings.PrinterSettings.DefaultPageSettings.Bounds.Inflate(sSize);
                e.PageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);
                e.PageSettings.PrinterSettings.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);

                e.PageSettings.PaperSize = psNew;

                e.PageSettings.Bounds.Inflate(sSize);

                e.PageBounds.Inflate(sSize);

                e.PageSettings.PrintableArea.Inflate(sSize);

                e.HasMorePages = true;
                e.Graphics.Clear(Color.White);
                e.Graphics.ResetClip();
                e.Graphics.Clip.MakeEmpty();
                //e.Graphics.Dispose();
                //e.Graphics.
            }
        }

        private void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;
            //printDoc.PrinterSettings.

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            //if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            //{
            string ttx = "\r\n" + tekst;
            ttx = ttx.Replace("č", "c");
            ttx = ttx.Replace("Č", "C");
            ttx = ttx.Replace("ž", "z");
            ttx = ttx.Replace("Ž", "Z");
            ttx = ttx.Replace("ć", "c");
            ttx = ttx.Replace("Ć", "C");
            ttx = ttx.Replace("đ", "d");
            ttx = ttx.Replace("Đ", "D");
            ttx = ttx.Replace("š", "s");
            ttx = ttx.Replace("Š", "S");

            string GS = Convert.ToString((char)29);
            string ESC = Convert.ToString((char)27);

            string COMMAND = "";
            COMMAND = ESC + "@";
            COMMAND += GS + "V" + (char)1;

            RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);

            //} else {
            //    if (!printDoc.PrinterSettings.IsValid) {
            //        string msg = String.Format(
            //           "Can't find printer \"{0}\".", printerName);
            //        MessageBox.Show(msg, "Print Error");
            //        return;
            //    }
            //    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //    printDoc.Print();

            //}

            //string GS = Convert.ToString((char)29);
            //string ESC = Convert.ToString((char)27);

            //string COMMAND = "";
            //COMMAND = ESC + "@";
            //COMMAND += GS + "V" + (char)1;

            //RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, COMMAND);
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = "Izvješće.rtf";
            //saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //File.WriteAllText(saveFileDialog1.FileName, rtb.Text);
                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
        }
    }
}