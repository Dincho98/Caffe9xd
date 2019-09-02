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

namespace PCPOS.Report.Izdatnica
{
    public partial class FormIzdatnicaReport : Form
    {
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public bool spremiPdf;

        public FormIzdatnicaReport(bool spremiPdf=false)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer1);
            this.spremiPdf = spremiPdf;
        }

        private void FormIzdatnicaReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource IzdatnicaDataSource = new ReportDataSource();
            IzdatnicaDataSource.Name = "DataSetIzdatnica"; // rdlc
            IzdatnicaDataSource.Value = IzdatnicaDataSet.Tables[0]; // designer tj. report
            reportViewer1.LocalReport.DataSources.Add(IzdatnicaDataSource);

            ReportDataSource PodaciTvrtkeDataSource = new ReportDataSource();
            PodaciTvrtkeDataSource.Name = "DataSetPodaciTvrtke"; // rdlc
            PodaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0]; // designer tj. report
            reportViewer1.LocalReport.DataSources.Add(PodaciTvrtkeDataSource);

            LoadDataIzdatnice();
            LoadDataCompany();

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Izdatnica.ReportIzdatnica.rdlc";
            this.reportViewer1.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("Izdatnice", reportViewer1);
                this.Close();
            }
        }

        private void LoadDataIzdatnice()
        {
            string sqlQuery = $@"SELECT DISTINCT izdatnica.id_izdatnica, izdatnica.datum, partners.ime_tvrtke AS partner 
                        FROM izdatnica, partners, izdatnica_stavke
                        WHERE izdatnica.id_izdatnica = izdatnica_stavke.id_izdatnica
                        AND izdatnica.id_partner = partners.id_partner
                        AND izdatnica.datum>'{datumOD.ToString("dd-MM-yyyy 00:00:00")}' 
                        AND izdatnica.datum<'{datumDO.ToString("dd-MM-yyyy 23:59:59")}'  
                        ORDER BY id_izdatnica;";
            DataTable DataTableIzdatnice = classSQL.select(sqlQuery, "izdatnica").Tables[0];

            for (int i = 0; i < DataTableIzdatnice.Rows.Count; i++)
            {
                DataRow row = IzdatnicaDataSet.Tables[0].NewRow();
                row["ID_Izdatnica"] = Int32.Parse(DataTableIzdatnice.Rows[i]["id_izdatnica"].ToString());
                row["Datum"] = DataTableIzdatnice.Rows[i]["datum"].ToString();
                row["Partner"] = DataTableIzdatnice.Rows[i]["partner"].ToString();
                row["Iznos"] = GetIznosIzdatnica(DataTableIzdatnice.Rows[i]["id_izdatnica"].ToString());
                row["DatumOd"] = datumOD.ToString("dd/MM/yyyy");
                row["DatumDo"] = datumDO.ToString("dd/MM/yyyy");
                IzdatnicaDataSet.Tables[0].Rows.Add(row);
            }
        }

        private string GetIznosIzdatnica(string id_izdatnice)
        {
            string sqlQuery = $@"SELECT SUM(nbc) FROM izdatnica_stavke WHERE id_izdatnica={id_izdatnice}";
            DataTable DataTableUkupniIznosIzdatnice = classSQL.select(sqlQuery, "izdatnica_stavke").Tables[0];
            return DataTableUkupniIznosIzdatnice.Rows[0][0].ToString();
        }


        private void LoadDataCompany()
        {
            string sqlpodaci = "SELECT " +
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
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }
    }
}
