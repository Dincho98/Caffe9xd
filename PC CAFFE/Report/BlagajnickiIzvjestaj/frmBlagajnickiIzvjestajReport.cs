using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.BlagajnickiIzvjestaj
{
    public partial class frmBlagajnickiIzvjestajReport : Form
    {
        public frmBlagajnickiIzvjestajReport()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string sort { get; set; }
        public string ime_partnera { get; set; }
        public string sifra_partnera { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            string sql1 = VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        public void btnTrazi_Click(object sender, EventArgs e)
        {
            dSsaldaKonti.Clear();

            string filter = " WHERE cast(bi.datum as date) >= '" + tdOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                " AND cast(bi.datum as date) <= '" + tdDoDatuma.Value.ToString("yyyy-MM-dd") + "'";

            DateTime datumPrethodniDan = new DateTime(tdOdDatuma.Value.AddDays(-1).Year, tdOdDatuma.Value.AddDays(-1).Month, tdOdDatuma.Value.AddDays(-1).Day);

            string sqlPomocni = string.Format(@"SELECT '{0:yyyy-MM-dd}' as datum, 'STANJE' as string1, 'Stanje blagajne na dan {0:dd.MM.yyyy.}' as string2, coalesce(sum(ROUND(uplaceno, 2)), 0) as numeric1, coalesce(sum(ROUND(izdatak, 2)), 0) as numeric2 FROM
                        (
	                        SELECT id, datum, dokumenat,
                            CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
                                THEN partners.ime_tvrtke
                                ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
                                END AS oznaka_dokumenta,
                            CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE', 'PROMET BLAGAJNE - R')
                                THEN uplaceno
                                ELSE '0' END as uplaceno, izdatak
                            FROM blagajnicki_izvjestaj
                            LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
                            WHERE dokumenat <> 'PROMET BLAGAJNE'

	                        UNION ALL

                            SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
	                        concat(MIN(CAST(racuni.broj_racuna AS INT)),'-',MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

	                        SUM( ( CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC)*CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno,'0' as izdatak

	                        FROM racuni
	                        LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna
                            AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
	                        WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null) GROUP BY date_trunc('day', datum_racuna)
                        )
                        bm
                        where cast(datum as date) <= '{0:yyyy-MM-dd}' and cast(datum as date) >= '{1}-01-01'

UNION ALL", datumPrethodniDan, Util.Korisno.GodinaKojaSeKoristiUbazi);

            sqlPomocni = string.Format(@"
SELECT '{0:yyyy-MM-dd}' as datum, 'STANJE' as string1, 'Stanje blagajne na dan {0:dd.MM.yyyy.}' as string2, coalesce(sum(ROUND(bi.uplaceno, 2)), 0) as numeric1, coalesce(sum(ROUND(bi.izdatak, 2)), 0) as numeric2
from blagajnicki_izvjestaj bi
left join partners p on bi.id_partner = p.id_partner
where cast(datum as date) <= '{0:yyyy-MM-dd}' and cast(datum as date) >= '{1}-01-01'

UNION ALL", datumPrethodniDan, Util.Korisno.GodinaKojaSeKoristiUbazi);

            if (datumPrethodniDan.Month == 12 && datumPrethodniDan.Day == 31)
                sqlPomocni = "";

            string sql = string.Format(@"{0}
SELECT datum, dokumenat as string1, oznaka_dokumenta as string2,ROUND(uplaceno,2) as numeric1, ROUND(izdatak,2) as numeric2 FROM
                        (

	                        SELECT id, datum, dokumenat,
                            CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
                                THEN partners.ime_tvrtke
                                ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
                                END AS oznaka_dokumenta,
                            CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE', 'PROMET BLAGAJNE - R')
                                THEN uplaceno
                                ELSE '0' END as uplaceno, izdatak
                            FROM blagajnicki_izvjestaj
                            LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
                            WHERE dokumenat <> 'PROMET BLAGAJNE'

	                        UNION ALL

                            SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
	                        concat(MIN(CAST(racuni.broj_racuna AS INT)),'-',MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

	                        SUM( ( CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC)*CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno,'0' as izdatak

	                        FROM racuni
	                        LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna
                            AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
	                        WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null) GROUP BY date_trunc('day', datum_racuna)

                        )
                        bm
                        {1}
                        ORDER BY datum ASC", sqlPomocni, filter);

            sql = string.Format(@"{0}
select bi.datum, bi.dokumenat  as string1, case when bi.id_partner is null then bi.oznaka_dokumenta else
	concat(bi.oznaka_dokumenta, ' - ', case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end)
	end  as string2, ROUND(bi.uplaceno, 2) as numeric1, ROUND(bi.izdatak, 2) as numeric2
from blagajnicki_izvjestaj bi
left join partners p on bi.id_partner = p.id_partner
{1}
order by datum asc;", sqlPomocni, filter);

            classSQL.NpgAdatpter(sql.Replace("+", "zbroj")).Fill(dSsaldaKonti, "DTsaldaKonti");

            string sql1 = VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = string.Format("FILTER-> OD DATUMA: {0} DO DATUMA: {1}", tdOdDatuma.Value.ToString("dd.MM.yyyy."), tdDoDatuma.Value.ToString("dd.MM.yyyy."));

            var selectedRow = (from row in (dSsaldaKonti.Tables[0]).AsEnumerable()
                               where row.Field<DateTime>("datum") == datumPrethodniDan
                               select row).FirstOrDefault();

            decimal uplaceno = 0, izdatak = 0;
            if (selectedRow != null)
            {
                decimal.TryParse(selectedRow["numeric1"].ToString(), out uplaceno);
                decimal.TryParse(selectedRow["numeric2"].ToString(), out izdatak);
            }

            ReportParameter p1 = new ReportParameter("do_datum", tdDoDatuma.Value.ToString("dd.MM.yyyy."));
            ReportParameter p2 = new ReportParameter("uplaceno_do", uplaceno.ToString());
            ReportParameter p3 = new ReportParameter("izdatak_do", izdatak.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

            this.reportViewer1.RefreshReport();
        }

        private string VratiSql(string idKupacGrad, string nazivFakture, string idKupac)
        {
            string r1 = " '0' AS R1,";

            if (nazivFakture.Trim() == "") nazivFakture = " podaci_tvrtka.naziv_fakture,";

            //e sad, vuče naziv fakture iz tablice podaci_tvrtke ALI ako je privatni partner onda mijenja 'R1' u ''
            if (nazivFakture == " podaci_tvrtka.naziv_fakture," && idKupac != "")
            {
                DataTable DTkupac = new DataTable();
                try
                {
                    string kupac = "";
                    if (idKupac != "")
                    {
                        DTkupac = classSQL.select("SELECT vrsta_korisnika FROM partners WHERE id_partner='" + idKupac + "'", "partners").Tables[0];
                    }
                    if (DTkupac.Rows.Count != 0)
                    {
                        kupac = DTkupac.Rows[0]["vrsta_korisnika"].ToString();
                        if (kupac != "1")
                        {
                            DTkupac = classSQL.select_settings("SELECT naziv_fakture FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
                            if (DTkupac.Rows.Count != 0)
                            {
                                string nazivFaktureIzTablice = DTkupac.Rows[0]["naziv_fakture"].ToString();
                                nazivFaktureIzTablice = nazivFaktureIzTablice.Replace(" R1", "");
                                nazivFaktureIzTablice = nazivFaktureIzTablice.Replace("R1", "");
                                nazivFakture = "'" + nazivFaktureIzTablice.Replace("R1", "") + "' as naziv_fakture,";
                            }
                            r1 = " '0' AS R1,";
                        }
                        else
                        {
                            r1 = " '1' AS R1,";
                        }
                    }
                }
                catch
                {
                }
            }

            string grad_kupac = "";
            DataTable DTgrad_kupac = new DataTable();
            try
            {
                if (idKupacGrad != "")
                {
                    DTgrad_kupac = classSQL.select("SELECT grad, posta FROM grad WHERE id_grad='" + idKupacGrad + "'", "grad").Tables[0];
                }
                if (DTgrad_kupac.Rows.Count != 0)
                {
                    grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                }
            }
            catch
            {
            }

            string grad_id = "";
            DataTable DTgrad_tvrtke = classSQL.select("SELECT podaci_tvrtka.id_grad FROM podaci_tvrtka", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_id = DTgrad_tvrtke.Rows[0]["id_grad"].ToString().Trim();
            }

            string grad_tvrtke = "";
            DTgrad_tvrtke = classSQL.select("SELECT grad, posta FROM grad WHERE id_grad='" + grad_id + "'", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_tvrtke = DTgrad_tvrtke.Rows[0]["grad"].ToString().Trim() + ' ' + DTgrad_tvrtke.Rows[0]["posta"].ToString().Trim();
            }

            string grad_poslovnica = "";
            DataTable dtGradPoslovnica = classSQL.select("select poslovnica_grad from podaci_tvrtka", "podaci_tvrtka").Tables[0];

            DataTable DTgrad_poslovnica = classSQL.select("SELECT grad, posta FROM grad WHERE grad='" + dtGradPoslovnica.Rows[0][0].ToString() + "' and id_drzava = '60';", "grad").Tables[0];
            if (DTgrad_poslovnica.Rows.Count != 0)
            {
                grad_poslovnica = DTgrad_poslovnica.Rows[0]["posta"].ToString().Trim() + ' ' + DTgrad_poslovnica.Rows[0]["grad"].ToString().Trim();
            }

            //string SQL_post = "Select racun_bool from podaci_tvrtka";
            //string provjera_texta = classSQL.select_settings(SQL_post, "provjera_texta").Tables[0].Rows[0][0].ToString();
            string filter = "";

            filter = " podaci_tvrtka.text_bottom";

            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.swift," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.vl," +
                " '" + grad_kupac + "' AS grad_kupac," +
                " '" + grad_tvrtke + "' AS grad," +
                " podaci_tvrtka.poslovnica_adresa," +
                " '" + grad_poslovnica + "' as poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.pdvBr," +
                " podaci_tvrtka.nazivPoslovnice," +
                nazivFakture +
                r1 + filter +
                " FROM podaci_tvrtka";

            return sql1;
        }
    }
}