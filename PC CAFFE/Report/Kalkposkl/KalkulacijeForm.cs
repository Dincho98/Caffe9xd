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
    public partial class KalkulacijeForm : Form
    {
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private bool spremiPdf;
        public KalkulacijeForm(bool spremiPdf)
        {
            InitializeComponent();
            this.spremiPdf = spremiPdf;
        }

        private void KalkulacijeForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSKalkulacijeReport.primka' table. You can move, or remove it, as needed.
            LoadData();
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource kalkulacijeReportDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            kalkulacijeReportDataSource.Name = "DSKalkulacijeReport";
            kalkulacijeReportDataSource.Value = dsRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(kalkulacijeReportDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Kalkulacije.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();


            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("Kalkulacije", reportViewer);
                this.Close();
            }
        }

        private void LoadData()
        {
            LoadCompanyData();
            LoadKalkulacijeData();
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
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private void LoadKalkulacijeData()
        {
            /*string selectQuery = @"SELECT 'broj_primke', 'id_skladiste', 'br_ulaznog_doc', 'id_partner', 'datum', 
                                       'iznos_bez_poreza', 'iznos_sa_porezom', 'carina', 'valuta', 'napomena', 
                                       'iznos', 'id_zaposlenik', 'is_kalkulacija', 'editirano', 'id_poslovnica', 
                                       'novo', 'id', 'zakljucano'
                                  FROM primka;";

            classSQL.CeAdatpter(selectQuery).Fill(dSKalkulacijeReport, "primka");*/

            string queryReport = $@"SELECT primka.br_ulaznog_doc, 
	                                primka.datum,
	                                skladiste.skladiste,
	                                SUM(primka_stavke.nabavna_cijena * primka_stavke.kolicina) As nabavna,
	                                SUM((primka_stavke.iznos_marze * primka_stavke.kolicina)) As marza_uk,
	                                SUM(primka_stavke.prodajna_cijena * primka_stavke.kolicina) As bez_pdv,
	                                SUM(((primka_stavke.prodajna_cijena_sa_porezom-primka_stavke.povratna_naknada)-primka_stavke.prodajna_cijena)*primka_stavke.kolicina) As pdv,
	                                SUM(primka_stavke.prodajna_cijena_sa_porezom *primka_stavke.kolicina) As sa_pdv,
                                    partners.id_partner,
	                                partners.ime_tvrtke
                                  FROM primka
                                  LEFT JOIN primka_stavke ON primka_stavke.broj_primke = primka.broj_primke
                                  LEFT JOIN skladiste ON skladiste.id_skladiste = primka.id_skladiste
                                  LEFT JOIN partners ON partners.id_partner = primka.id_partner
                                  WHERE primka.is_kalkulacija = TRUE AND primka_stavke.is_kalkulacija = TRUE AND primka.datum >= '{datumOD.ToString("dd-MM-yyyy 00:00:00")}' AND primka.datum <= '{datumDO.ToString("dd-MM-yyyy 23:59:59")}'
                                  GROUP BY primka.broj_primke, primka.br_ulaznog_doc,primka.datum,partners.id_partner,partners.ime_tvrtke,skladiste.skladiste;";

            DataTable dTkalkulacije = new DataTable();
            dTkalkulacije = classSQL.select(queryReport, "DTlisteTekst").Tables[0];

            for (int x = 0; x < dTkalkulacije.Rows.Count; x++)
            {
                DataRow row = dsRlisteTekst.Tables[0].NewRow();
                row["string1"] = dTkalkulacije.Rows[x]["br_ulaznog_doc"].ToString();
                row["datum1"] = dTkalkulacije.Rows[x]["datum"].ToString();
                row["string2"] = dTkalkulacije.Rows[x]["skladiste"].ToString();
                row["ukupno1"] = dTkalkulacije.Rows[x]["nabavna"].ToString();
                row["ukupno2"] = dTkalkulacije.Rows[x]["marza_uk"].ToString();
                row["ukupno3"] = dTkalkulacije.Rows[x]["bez_pdv"].ToString();
                row["ukupno4"] = dTkalkulacije.Rows[x]["pdv"].ToString();
                row["ukupno5"] = dTkalkulacije.Rows[x]["sa_pdv"].ToString();
                row["datum_od"] = datumOD.ToString();
                row["datum_do"] = datumDO.ToString();
                row["string3"] = dTkalkulacije.Rows[x]["id_partner"].ToString();
                row["string4"] = dTkalkulacije.Rows[x]["ime_tvrtke"].ToString();
                dsRlisteTekst.Tables[0].Rows.Add(row);
            }
        }
    }
}
