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

namespace PCPOS.Report.GrupeProizvoda
{
    public partial class ReportObracunGrupe : Form
    {
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public string grupa { get; set; }

        int grupaId = 0;
        string nazivGrupe = "";
        decimal osnovica = 0;
        decimal porez = 0;
        decimal pnp = 0;

        bool spremiPdf = false;

        public ReportObracunGrupe(bool spremiPdf = false)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer);
            this.spremiPdf = spremiPdf;
        }

        private void ReportObracunGrupe_Load(object sender, EventArgs e)
        {
            LoadPodaciTvrtke();
            LoadGrupeData();

            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource listeSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            listeSource.Name = "DSliste";
            listeSource.Value = dSRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(listeSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.GrupeProizvoda.ObracunGrupeProizvoda.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("ObracunPoGrupamaProizvoda", reportViewer);
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadPodaciTvrtke()
        {
            string sql = "SELECT " +
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
            classSQL.CeAdatpter(sql).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGrupeData()
        {
            string query = $@"SELECT grupa.id_grupa AS grupa_id,
	                            grupa.grupa AS naziv_grupa,
	                            CAST(racun_stavke.mpc AS numeric) AS mpc,
	                            CAST(REPLACE(racun_stavke.rabat, ',', '.') AS numeric) AS rabat,
	                            CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
	                            CAST(REPLACE(racun_stavke.porez, ',', '.') AS numeric) AS pdv,
	                            CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric) AS porez_potrosnja
                            FROM racun_stavke
                            LEFT JOIN roba ON racun_stavke.sifra_robe = roba.sifra
                            LEFT JOIN racuni ON racun_stavke.broj_racuna = racuni.broj_racuna
                            LEFT JOIN grupa ON roba.id_grupa = grupa.id_grupa
                            WHERE racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}' {grupa}
                            ORDER BY grupa.grupa ASC";

            DataTable dataSet = classSQL.select(query, "racun_stavke").Tables[0];

            for (int i = 0; i < dataSet.Rows.Count; i++)
            {
                if ((grupaId != Convert.ToInt32(dataSet.Rows[i]["grupa_id"].ToString())) || dataSet.Rows.Count - 1 == i)
                {
                    if (dataSet.Rows.Count - 1 == i)
                        Calculate(dataSet, i);

                    if (osnovica > 0)
                    {
                        DataRow row = dSRlisteTekst.Tables[0].NewRow();
                        row["string1"] = nazivGrupe;
                        row["ukupno1"] = Math.Round(osnovica, 2, MidpointRounding.AwayFromZero);
                        row["ukupno2"] = Math.Round(porez, 2, MidpointRounding.AwayFromZero);
                        row["ukupno3"] = Math.Round(pnp, 2, MidpointRounding.AwayFromZero);
                        row["ukupno4"] = Math.Round(osnovica + porez + pnp, 2, MidpointRounding.AwayFromZero);
                        row["datum_od"] = datumOd.ToString("dd.MM.yyy.");
                        row["datum_do"] = datumDo.ToString("dd.MM.yyy.");
                        dSRlisteTekst.Tables[0].Rows.Add(row);
                    }

                    if (dataSet.Rows.Count - 1 != i)
                    {
                        grupaId = Convert.ToInt32(dataSet.Rows[i]["grupa_id"].ToString());
                        nazivGrupe = dataSet.Rows[i]["naziv_grupa"].ToString();
                        osnovica = 0;
                        porez = 0;
                        pnp = 0;

                        Calculate(dataSet, i);
                    }
                }
                else
                {
                    Calculate(dataSet, i);
                }
            }
        }

        private void Calculate(DataTable dataTable, int index)
        {
            decimal mpc = Convert.ToDecimal(dataTable.Rows[index]["mpc"].ToString());
            decimal pdv = Convert.ToDecimal(dataTable.Rows[index]["pdv"].ToString());
            decimal rabat = Convert.ToDecimal(dataTable.Rows[index]["rabat"].ToString());
            decimal kolicina = Convert.ToDecimal(dataTable.Rows[index]["kolicina"].ToString());
            decimal porez_potrosnja = Convert.ToDecimal(dataTable.Rows[index]["porez_potrosnja"].ToString());

            osnovica += Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) / (1 + (pdv + porez_potrosnja) / 100), 4);
            porez += Math.Round((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * pdv) / (100 + pdv + porez_potrosnja)) / 100), 4);
            pnp += Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * porez_potrosnja) / (100 + pdv + porez_potrosnja)) / 100, 4);
        }
    }
}
