using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Report.Kalkposkl
{
    public partial class FaktureForm : Form
    {
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        private bool spremiPdf;

        public FaktureForm(bool spremiPdf=false)
        {
            InitializeComponent();
            this.spremiPdf = spremiPdf;
        }

        private void FaktureForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSFaktureReport.fakture' table. You can move, or remove it, as needed.
            LoadData();
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource faktureDataSource = new ReportDataSource();
            ReportDataSource fakturaStavkeDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);


            faktureDataSource.Name = "DSFaktureReport";
            faktureDataSource.Value = dsRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(faktureDataSource);

            fakturaStavkeDataSource.Name = "DSFakturaStavke";
            fakturaStavkeDataSource.Value = dSRfakturaStavke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(fakturaStavkeDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Fakture.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("Fakture", reportViewer);
                this.Close();
            }
        }

        /// <summary>
        /// Method used to load report data
        /// </summary>
        private void LoadData()
        {
            LoadCompanyData();
            LoadFaktureData();
        }

        /// <summary>
        /// Method used to load company data
        /// </summary>
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
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        /// <summary>
        /// Method used to load fakture data
        /// </summary>
        private void LoadFaktureData()
        {
            string queryReport = $@"SELECT fakture.broj_fakture As id,
                                fakture.date As datum, 
                                fakture.datum_valute As datum_valute,  
                                fakture.ukupno, 
                                fakture.id_vd As vd, 
                                fakture.id_fakturirati,
                                partners.id_partner,
                                partners.ime_tvrtke
                                FROM fakture
                                LEFT JOIN partners ON fakture.id_fakturirati = partners.id_partner 
                                WHERE fakture.date >= '{datumOD.ToString("dd-MM-yyyy 00:00:00")}' AND fakture.date <= '{datumDO.ToString("dd-MM-yyyy 23:59:59")}'
                                ORDER BY fakture.broj_fakture ASC;";

            DataTable dTfakture = new DataTable();
            dTfakture = classSQL.select(queryReport, "DTlisteTekst").Tables[0];

            List<int> idList = new List<int>();
            for (int x = 0; x < dTfakture.Rows.Count; x++)
            {
                idList.Add(Convert.ToInt32(dTfakture.Rows[x]["id"].ToString()));
                DataRow row = dsRlisteTekst.Tables[0].NewRow();
                row["string1"] = dTfakture.Rows[x]["id"].ToString();
                row["string2"] = dTfakture.Rows[x]["vd"].ToString();
                row["datum1"] = dTfakture.Rows[x]["datum"].ToString();
                row["datum2"] = dTfakture.Rows[x]["datum_valute"].ToString();
                row["string3"] = dTfakture.Rows[x]["id_partner"].ToString();
                row["string4"] = dTfakture.Rows[x]["ime_tvrtke"].ToString();
                row["ukupno1"] = dTfakture.Rows[x]["ukupno"].ToString();
                row["datum_od"] = datumOD.ToString();
                row["datum_do"] = datumDO.ToString();
                dsRlisteTekst.Tables[0].Rows.Add(row);
            }

            if (idList.Count > 0)
                LoadFakturaStavkeData(idList);
        }

        /// <summary>
        /// Loads faktura_stavke data
        /// </summary>
        /// <param name="list"></param>
        private void LoadFakturaStavkeData(List<int> list)
        {
            string inStatement = Global.GlobalFunctions.CreateInCondition(list);
            string query = $@"SELECT faktura_stavke.vpc,
                                CAST(REPLACE(faktura_stavke.porez, ',', '.') AS numeric) AS porez,
                                CAST(REPLACE(faktura_stavke.rabat, ',', '.') AS numeric) AS rabat,
                                CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
                                CAST(REPLACE(faktura_stavke.porez_potrosnja, ',', '.') AS numeric) AS porez_potrosnja,
                                faktura_stavke.mpc
                                FROM faktura_stavke
                                LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture
                                WHERE faktura_stavke.broj_fakture IN({inStatement});";

            DataTable data = classSQL.select(query, "faktura_stavke").Tables[0];

            decimal ukupnoOsnovica = 0;
            decimal ukupnoPdv = 0;
            decimal ukupnoPnp = 0;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                decimal mpc = Convert.ToDecimal(data.Rows[i]["mpc"].ToString());
                decimal porez = Convert.ToDecimal(data.Rows[i]["porez"].ToString());
                decimal rabat = Convert.ToDecimal(data.Rows[i]["rabat"].ToString());
                decimal kolicina = Convert.ToDecimal(data.Rows[i]["kolicina"].ToString());
                decimal porez_potrosnja = Convert.ToDecimal(data.Rows[i]["porez_potrosnja"].ToString());

                ukupnoOsnovica += Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) / (1 + (porez + porez_potrosnja) / 100), 4);
                ukupnoPdv += Math.Round((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * porez) / (100 + porez + porez_potrosnja)) / 100), 4);
                ukupnoPnp += Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * porez_potrosnja) / (100 + porez + porez_potrosnja)) / 100, 4);
            }

            decimal ukupnoFakturirano = ukupnoOsnovica + ukupnoPdv + ukupnoPnp;

            DataRow row = dSRfakturaStavke.Tables[0].NewRow();
            row["string1"] = ukupnoOsnovica.ToString("#0.00");
            row["string2"] = ukupnoPdv.ToString("#0.00");
            row["string3"] = ukupnoPnp.ToString("#0.00");
            row["string4"] = ukupnoFakturirano.ToString("#0.00");
            dSRfakturaStavke.Tables[0].Rows.Add(row);
        }
    }
}
