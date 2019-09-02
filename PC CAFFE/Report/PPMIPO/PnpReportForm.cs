using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Report.PPMIPO
{
    public partial class PnpReportForm : Form
    {
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public string idDucan { get; set; }
        private bool spremiPdf;

        public PnpReportForm(bool spremiPdf=false)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer);
            this.spremiPdf = spremiPdf;
        }

        private void PnpReportForm_Load(object sender, EventArgs e)
        {
            LoadData();
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource porezNaPotrosnjuDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dsRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            porezNaPotrosnjuDataSource.Name = "DSListe";
            porezNaPotrosnjuDataSource.Value = dsRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(porezNaPotrosnjuDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.PPMIPO.PorezNaPotrosnju.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("PPMIPO", reportViewer);
                this.Close();
            }
        }

        private void LoadData()
        {
            LoadCompanyData();
            LoadPorezNaPotrosnjuData();
        }

        private void LoadCompanyData()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " podaci_tvrtka.sifra_ppmipo," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dsRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private void LoadPorezNaPotrosnjuData()
        {
            decimal ukupnoRacuni = 0;
            decimal ukupnoPnpRacuni = 0;
            decimal ukupnoFakture = 0;
            decimal ukupnoPnpFakture = 0;
            decimal porezPotrosnja = 0;

            // Racuni
            string queryRacuni = $@"SELECT ROUND(CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja,
	                            SUM(racun_stavke.vpc * CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) AS ukupno,
                                SUM(racun_stavke.vpc * CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) * (MAX(CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric)) / 100) AS pnp
                              FROM racun_stavke
                              LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan
                              WHERE CAST(REPLACE(porez_potrosnja, ',', '.') AS numeric) > 0 AND racuni.id_ducan = {Convert.ToInt32(idDucan)}
                                AND racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'
                              GROUP BY racun_stavke.porez_potrosnja";

            DataTable DTracuni = new DataTable();
            DTracuni = classSQL.select(queryRacuni, "DTlisteTekst").Tables[0];

            for (int x = 0; x < DTracuni.Rows.Count; x++)
            {
                ukupnoRacuni += Convert.ToDecimal(DTracuni.Rows[x]["ukupno"].ToString());
                ukupnoPnpRacuni += Convert.ToDecimal(DTracuni.Rows[x]["pnp"].ToString());
                porezPotrosnja = Convert.ToDecimal(DTracuni.Rows[x]["porez_potrosnja"].ToString());
            }

            // Fakture
            string queryFakture = $@"SELECT ROUND(CAST(REPLACE(faktura_stavke.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja,
	                            SUM(faktura_stavke.vpc * CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric)) AS ukupno,
                                SUM(faktura_stavke.vpc * CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric)) * (MAX(CAST(REPLACE(faktura_stavke.porez_potrosnja, ',', '.') AS numeric)) / 100) AS pnp
                              FROM faktura_stavke
                              LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture
                              WHERE CAST(REPLACE(porez_potrosnja, ',', '.') AS numeric) > 0 AND fakture.id_ducan = {Convert.ToInt32(idDucan)}
                                AND fakture.date >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND fakture.date <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'
                              GROUP BY faktura_stavke.porez_potrosnja";

            DataTable DTfakture = new DataTable();
            DTfakture = classSQL.select(queryFakture, "DTlisteTekst").Tables[0];

            for (int x = 0; x < DTfakture.Rows.Count; x++)
            {
                ukupnoFakture += Convert.ToDecimal(DTfakture.Rows[x]["ukupno"].ToString());
                ukupnoPnpFakture += Convert.ToDecimal(DTfakture.Rows[x]["pnp"].ToString());
            }

            DataRow row = dsRlisteTekst.Tables[0].NewRow();
            row["ukupno2"] = (ukupnoRacuni + ukupnoFakture).ToString();
            row["ukupno3"] = (ukupnoPnpRacuni + ukupnoPnpFakture).ToString();
            row["string1"] = dsRpodaciTvrtke.Tables[0].Rows[0]["grad"].ToString();
            row["string2"] = dsRpodaciTvrtke.Tables[0].Rows[0]["sifra_ppmipo"].ToString();
            row["string3"] = porezPotrosnja.ToString();
            row["datum_od"] = datumOd.ToString();
            row["datum_do"] = datumDo.ToString();
            dsRlisteTekst.Tables[0].Rows.Add(row);
        }
    }
}
