using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace PCPOS.Report.NaruzdbeNaStolu
{
    public partial class FrmNarudzbeNaStolu : Form
    {
        public object SmtpMail { get; private set; }

        public FrmNarudzbeNaStolu()
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer1);
        }

        private void FrmNarudzbeNaStolu_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource narudzbeNaStolDataSource = new ReportDataSource();
            narudzbeNaStolDataSource.Name = "DataSetNarudzbeNaStolu"; // rdlc
            narudzbeNaStolDataSource.Value = narudzbeNaStoluDataSet.Tables[0]; // designer
            reportViewer1.LocalReport.DataSources.Add(narudzbeNaStolDataSource);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            podaciTvrtkeDataSource.Name = "DataSetPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            LoadDataNarudzbe();
            LoadDataCompany();

            this.reportViewer1.LocalReport.ReportEmbeddedResource = @"PCPOS.Report.NaruzdbeNaStolu.ReportNarudzbeNaStolu.rdlc";
            this.reportViewer1.RefreshReport();

        }

        private void LoadDataNarudzbe()
        {
            string sqlPodaci=$@"SELECT ns.broj_narudzbe, ns.id_stol, r.naziv, ns.kom, ns.mpc, ns.vpc, ns.porez, ns.porez_potrosnja
                                                      FROM na_stol_naplaceno ns, roba r WHERE ns.sifra = r.sifra ORDER BY CAST(ns.broj_narudzbe AS INT) ASC;";
            DataTable DTNarudzbe = classSQL.select(sqlPodaci,"na_stol").Tables[0];
            for(int i = 0; i < DTNarudzbe.Rows.Count; i++)
            { 
                DataRow row = narudzbeNaStoluDataSet.Tables[0].NewRow();
                row["broj_narudzbe"] = Int32.Parse(DTNarudzbe.Rows[i]["broj_narudzbe"].ToString());
                row["id_stol"] = DTNarudzbe.Rows[i]["id_stol"].ToString();
                row["ime_artikla"] = DTNarudzbe.Rows[i]["naziv"].ToString();
                row["kom"] = DTNarudzbe.Rows[i]["kom"].ToString();
                row["mpc"] =DTNarudzbe.Rows[i]["mpc"].ToString();
                row["vpc"] =DTNarudzbe.Rows[i]["vpc"].ToString();
                row["porez"] =DTNarudzbe.Rows[i]["porez"].ToString();
                row["pnp"] =DTNarudzbe.Rows[i]["porez_potrosnja"].ToString();
                narudzbeNaStoluDataSet.Tables[0].Rows.Add(row);
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


    }
}
