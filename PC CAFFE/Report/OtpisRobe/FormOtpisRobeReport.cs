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

namespace PCPOS.Report.OtpisRobe
{
    public partial class FormOtpisRobeReport : Form
    {
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        bool spremiPdf;

        public FormOtpisRobeReport(bool spremiPdf=false)
        {
            InitializeComponent();
            this.Controls.Add(reportViewer1);
            this.spremiPdf = spremiPdf;
        }

        private void FormOtpisRobeReport_Load(object sender, EventArgs e)
        {
            ReportDataSource OtpisRobeDataSource = new ReportDataSource();
            OtpisRobeDataSource.Name = "OtpisRobeDataSet";
            OtpisRobeDataSource.Value = izdatnicaDataSet.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(OtpisRobeDataSource);

            ReportDataSource PodaciTvrtkeDataSource = new ReportDataSource();
            PodaciTvrtkeDataSource.Name = "DataSetPodaciTvrtke";
            PodaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(PodaciTvrtkeDataSource);

            LoadDataOtpisRobe();
            LoadDataCompany();

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.OtpisRobe.ReportSviOtpisiRobe.rdlc";
            this.reportViewer1.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("OtpisRobe", reportViewer1);
                this.Close();
            }
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

        private void LoadDataOtpisRobe()
        {
            string sqlQuery = $@"SELECT DISTINCT povrat_robe.broj, povrat_robe.datum, skladiste.skladiste FROM povrat_robe,skladiste
                                WHERE povrat_robe.id_skladiste = skladiste.id_skladiste 
                                AND povrat_robe.datum>'{datumOD.ToString("dd-MM-yyyy 00:00:00")}' 
                                AND povrat_robe.datum<'{datumDO.ToString("dd-MM-yyyy 23:59:59")}'  
                                ORDER BY broj;"; 
            DataTable DataTableOtpisiRobe = classSQL.select(sqlQuery, "povrat_roba").Tables[0];

            for(int i=0;i<DataTableOtpisiRobe.Rows.Count;i++)
            {
                DataRow row = izdatnicaDataSet.Tables[0].NewRow();
                row["ID_Izdatnica"] =DataTableOtpisiRobe.Rows[i]["broj"].ToString();
                row["Datum"] = DataTableOtpisiRobe.Rows[i]["datum"].ToString();
                row["Partner"]= DataTableOtpisiRobe.Rows[i]["skladiste"].ToString();// ime skladista
                row["Iznos"]=GetImeSkladista(DataTableOtpisiRobe.Rows[i]["broj"].ToString());
                row["DatumOd"] = datumOD.ToString("dd/MM/yyyy"); 
                row["DatumDo"] = datumDO.ToString("dd/MM/yyyy");
                izdatnicaDataSet.Tables[0].Rows.Add(row);
            }
        }

        private string GetImeSkladista(string broj)
        {
            string sqlQuery = $@"SELECT 
                                SUM(CAST(povrat_robe_stavke.nbc AS money) * CAST(REPLACE(povrat_robe_stavke.kolicina, ',', '.') AS numeric))
                                AS cijena FROM povrat_robe_stavke WHERE povrat_robe_stavke.broj = {broj};";
            DataTable DTTable = classSQL.select(sqlQuery, "povrat_robe_stavke").Tables[0];
            return DTTable.Rows[0][0].ToString();
        }
    }
}
