using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.KarticaSkladista
{
    public partial class frmKarticaSkladista2 : Form
    {
        public DataTable dtPodaci { get; set; }
        public DateTime dtDatum { get; set; }
        public string broj_kalkulacije { get; set; }

        public frmKarticaSkladista2()
        {
            InitializeComponent();
        }

        private void frmKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            SetDS();
            this.reportViewer1.RefreshReport();
        }

        private void SetDS()
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
                " podaci_tvrtka.zr," +
                //" '" + grad_kupac + "' AS grad_kupac," +
                " '' AS grad_kupac," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture AS naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            ReportParameter p1 = new ReportParameter("filter", string.Format("Do datuma: {0:dd.MM.yyyy HH:mm:ss}", dtDatum));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

            foreach (DataRow row in dtPodaci.Rows)
            {
                DataRow DTrow = dSRliste.Tables["DTliste"].NewRow();

                //ovaj dio je iz tablice roba
                DTrow["sifra"] = row["sifra"].ToString();
                DTrow["naziv"] = row["naziv"].ToString();
                DTrow["string1"] = row["skladiste"].ToString();

                DTrow["cijena1"] = row["pocetno"].ToString();
                DTrow["cijena2"] = row["primka"].ToString();
                DTrow["cijena3"] = row["kalkulacija"].ToString();
                DTrow["cijena5"] = row["izdatnica"].ToString();
                DTrow["cijena6"] = row["inventura"].ToString();
                DTrow["cijena7"] = row["otpis"].ToString();
                DTrow["cijena8"] = row["racuni"].ToString();
                DTrow["cijena9"] = row["fakture"].ToString();
                DTrow["cijena10"] = row["otpremnica"].ToString();
                DTrow["cijena11"] = row["izlaz_ms"].ToString();
                DTrow["cijena12"] = row["ulaz_ms"].ToString();
                //DTrow["cijena13"] = row["mpc"].ToString();

                dSRliste.Tables["DTliste"].Rows.Add(DTrow);
            }
        }
    }
}