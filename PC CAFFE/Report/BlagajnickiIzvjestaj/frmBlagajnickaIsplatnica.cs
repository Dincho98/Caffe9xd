using Microsoft.Reporting.WinForms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.BlagajnickiIzvjestaj
{
    public partial class frmBlagajnickaIsplatnica : Form
    {
        public string dokument { get; set; }
        public string broj { get; set; }
        public decimal iznos { get; set; }
        public string partner { get; set; }
        public string broj_dokumenta { get; set; }
        public string mjesto_i_datum { get; set; }
        private string[] _dokuments = new string[] { "POČETNO STANJE", "POZAJMNICA", "POCETNI POLOG BLAGAJNE", "PROMET BLAGAJNE", "PROMET BLAGAJNE - R" };
        public string[] dokuments { get { return _dokuments; } }

        public frmBlagajnickaIsplatnica()
        {
            InitializeComponent();
        }

        private void frmBlagajnickaIsplatnica_Load(object sender, EventArgs e)
        {
            try
            {
                PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();
                string iznos_slovima = broj_u_text.PretvoriBrojUTekst(iznos.ToString(), ',', "kn", "lp").ToString();

                string naslov = (dokuments.Contains(dokument) ? "UPLATNICA BROJ:" : "ISPLATNICA BROJ:");
                string adresaNazivTvrtke = Class.PodaciTvrtka.nazivTvrtke;
                string adresaAdresaTvrtke = Class.PodaciTvrtka.adresaTvrtke;
                string adresaGradTvrtke = classSQL.select(string.Format(@"select concat(posta, ' ', grad) as grad from grad where id_grad = {0};", Class.PodaciTvrtka.gradTvrtke), "grad").Tables[0].Rows[0]["grad"].ToString();

                string adresa = adresaNazivTvrtke;
                if (adresa.Length > 0 && adresaAdresaTvrtke.Length > 0) adresa += Environment.NewLine;
                adresa += adresaAdresaTvrtke;
                if (adresa.Length > 0 && adresaGradTvrtke.Length > 0) adresa += Environment.NewLine;
                adresa += adresaGradTvrtke;

                ReportParameter p1 = new ReportParameter("adresa", adresa);
                ReportParameter p2 = new ReportParameter("naslov", naslov);
                ReportParameter p3 = new ReportParameter("broj", broj);
                ReportParameter p4 = new ReportParameter("iznos", (iznos.ToString("#,##0.00") + " kn"));
                ReportParameter p5 = new ReportParameter("slovima_iznos", iznos_slovima);
                ReportParameter p6 = new ReportParameter("kome", partner);
                ReportParameter p7 = new ReportParameter("za", dokument + (dokument.Length > 0 && broj_dokumenta.Length > 0 ? " - " : "") + broj_dokumenta);
                ReportParameter p8 = new ReportParameter("datum", mjesto_i_datum);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8 });
                this.reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}