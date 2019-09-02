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
    public partial class PrimkeForm : Form
    {
        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string skladiste_odabir { get; set; }
        public string ducan { get; set; }
        public bool premadatumu { get; set; }
        public bool prema_rac { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }
        public bool spremiPdf;

        public PrimkeForm(bool spremiPdf=false)
        {
            InitializeComponent();
            this.spremiPdf = spremiPdf;
        }

        private void PrimkeForm_Load(object sender, EventArgs e)
        {
            LoadData();
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource primkeDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            primkeDataSource.Name = "DSPrimkeReport";
            primkeDataSource.Value = dsRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(primkeDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Primke.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();


            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("Primke", reportViewer);
                this.Close();
            }
        }

        private void LoadData()
        {
            LoadCompanyData();
            LoadPrimkeData();
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

        private void LoadPrimkeData()
        {
            string queryPrimka = $@"SELECT primka.broj_primke, 
                                    primka.id_skladiste, 
                                    primka.br_ulaznog_doc, 
                                    primka.id_partner, 
                                    primka.datum, 
                                    primka.iznos_bez_poreza, 
                                    primka.iznos_sa_porezom, 
                                    primka.carina, primka.valuta, 
                                    primka.napomena, 
                                    primka.iznos, 
                                    primka.id_zaposlenik, 
                                    primka.is_kalkulacija, 
                                    primka.editirano, 
                                    primka.id_poslovnica, 
                                    primka.novo, 
                                    primka.zakljucano,
                                    primka_stavke.rabat,
                                    primka.iznos as nabavni_iznos,
                                    SUM(primka_stavke.povratna_naknada) as povratna_naknada,
                                    partners.id_partner,
                                    partners.ime_tvrtke,
                                    skladiste.skladiste
                                    FROM primka
                                    LEFT JOIN primka_stavke ON primka_stavke.broj_primke = primka.broj_primke
                                    LEFT JOIN partners ON partners.id_partner = primka.id_partner 
                                    LEFT JOIN skladiste ON primka.id_skladiste = skladiste.id_skladiste
                                    WHERE primka.datum >= '{datumOD.ToString("dd-MM-yyyy 00:00:00")}' AND primka.datum <= '{datumDO.ToString("dd-MM-yyyy 23:59:59")}'
                                        AND primka.is_kalkulacija = FALSE {(bool1 ? $"AND primka.id_skladiste = {skladiste_odabir}" : "")} AND primka_stavke.is_kalkulacija = FALSE
                                    GROUP BY primka.broj_primke, primka.id_skladiste, primka.br_ulaznog_doc, primka.id_partner, primka.datum, primka.iznos_bez_poreza, primka.iznos_sa_porezom, primka.carina, primka.valuta, primka.napomena, primka.iznos, primka.id_zaposlenik, primka.is_kalkulacija, primka.editirano, primka.id_poslovnica, primka.novo, primka.zakljucano,primka_stavke.rabat,partners.id_partner,partners.ime_tvrtke,skladiste.skladiste;";

            DataTable dTprimka = new DataTable();
            dTprimka = classSQL.select(queryPrimka, "DTlisteTekst").Tables[0];

            for (int x = 0; x < dTprimka.Rows.Count; x++)
            {
                DataRow row = dsRlisteTekst.Tables[0].NewRow();
                row["string1"] = dTprimka.Rows[x]["broj_primke"].ToString();
                row["string2"] = dTprimka.Rows[x]["br_ulaznog_doc"].ToString();
                row["string3"] = dTprimka.Rows[x]["skladiste"].ToString();
                row["string4"] = dTprimka.Rows[x]["id_partner"].ToString() + " " + dTprimka.Rows[x]["ime_tvrtke"].ToString();
                row["datum1"] = dTprimka.Rows[x]["datum"].ToString();
                row["ukupno1"] = dTprimka.Rows[x]["rabat"].ToString();
                row["ukupno2"] = dTprimka.Rows[x]["nabavni_iznos"].ToString();
                row["ukupno3"] = dTprimka.Rows[x]["iznos_bez_poreza"].ToString();
                row["ukupno4"] = dTprimka.Rows[x]["iznos_sa_porezom"].ToString();
                row["ukupno5"] = dTprimka.Rows[x]["povratna_naknada"].ToString();
                row["datum_od"] = datumOD.ToString();
                row["datum_do"] = datumDO.ToString();
                dsRlisteTekst.Tables[0].Rows.Add(row);
            }
        }
    }
}
