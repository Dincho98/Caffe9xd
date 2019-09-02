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

namespace PCPOS.Report.IzlazniDokumenti
{
    public partial class ObracunReport : Form
    {
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }

        // Ukupno racuni
        decimal racun_osnovica = 0;
        decimal racun_pdv = 0;
        decimal racun_pnp = 0;
        decimal racun_bruto = 0;
        decimal racun_gotovina = 0;
        decimal racun_gotovina_pdv = 0;
        decimal racun_kartice = 0;
        decimal racun_kartice_pdv = 0;
        decimal racun_ostalo = 0;

        // Ukupno fakture
        decimal faktura_osnovica = 0;
        decimal faktura_pdv = 0;
        decimal faktura_pnp = 0;
        decimal faktura_ukupno = 0;

        // Ukupno promet
        decimal ukupno_promet = 0;

        // Obracun ukupno
        private bool spremiPdf;

        public ObracunReport(bool spremiPdf=false)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer);
            this.spremiPdf = spremiPdf;
        }

        private void ObracunReport_Load(object sender, EventArgs e)
        {
            LoadPodaciTvrtke();
            LoadData();
            LoadPorezData();

            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource listeDataSource = new ReportDataSource();
            ReportDataSource porezDataSource = new ReportDataSource();
            ReportDataSource obracunFaktureDataSource = new ReportDataSource();
            ReportDataSource obracunUkupnoDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            listeDataSource.Name = "DSListe";
            listeDataSource.Value = dSRlisteTekst.Tables[0];
            reportViewer.LocalReport.DataSources.Add(listeDataSource);

            porezDataSource.Name = "DSPorez";
            porezDataSource.Value = dSRlisteTekst.Tables[1];
            reportViewer.LocalReport.DataSources.Add(porezDataSource);

            obracunFaktureDataSource.Name = "DSObracunFakture";
            obracunFaktureDataSource.Value = dSRlisteTekst.Tables[2];
            reportViewer.LocalReport.DataSources.Add(obracunFaktureDataSource);

            obracunUkupnoDataSource.Name = "DSObracunUkupno";
            obracunUkupnoDataSource.Value = dSRlisteTekst.Tables[3];
            reportViewer.LocalReport.DataSources.Add(obracunUkupnoDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.IzlazniDokumenti.ObracunPrometaIPoreza.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("ObracunPrometaIPoreza", reportViewer);
                this.Close();
            }
        }

        /// <summary>
        /// Loads podaci_tvrtke data
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
        /// Method used to load report data
        /// </summary>
        private void LoadData()
        {
            // Racuni
            string queryRacun = $@"SELECT DATE(racuni.datum_racuna) AS datum_racuna,
                                racuni.nacin_placanja,
                                CAST(racun_stavke.mpc AS numeric) AS mpc,
	                            CAST(REPLACE(racun_stavke.rabat, ',', '.') AS numeric) AS rabat,
	                            CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
	                            CAST(REPLACE(racun_stavke.porez, ',', '.') AS numeric) AS pdv,
	                            CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric) AS porez_potrosnja
                            FROM racun_stavke
                            LEFT JOIN racuni ON racun_stavke.broj_racuna = racuni.broj_racuna
                            WHERE racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTracun = classSQL.select(queryRacun, "racun_stavke").Tables[0];

            // Fakture
            string queryFakture = $@"SELECT DATE(fakture.date) AS datum_fakture,
                                CAST(faktura_stavke.mpc AS numeric) AS mpc,
	                            CAST(REPLACE(faktura_stavke.rabat, ',', '.') AS numeric) AS rabat,
	                            CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
	                            CAST(REPLACE(faktura_stavke.porez, ',', '.') AS numeric) AS pdv,
	                            CAST(REPLACE(faktura_stavke.porez_potrosnja, ',', '.') AS numeric) AS porez_potrosnja
                            FROM faktura_stavke
                            LEFT JOIN fakture ON faktura_stavke.broj_fakture = fakture.broj_fakture
                            WHERE fakture.date >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND fakture.date <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTfakture = classSQL.select(queryFakture, "faktura_stavke").Tables[0];

            // Get data by date
            DateTime dateGroup = datumOd;
            while (dateGroup.Date <= datumDo.Date)
            {
                DataRow[] racunRows = DTracun.Select($"datum_racuna = '{dateGroup.ToString("dd-MM-yyyy")}'");
                DataRow[] faktureRows = DTfakture.Select($"datum_fakture = '{dateGroup.ToString("dd-MM-yyyy")}'");

                if (racunRows.Length > 0)
                {
                    for (int i = 0; i < racunRows.Length; i++)
                    {
                        Calculate(racunRows, i, 0);
                    }
                }

                if (faktureRows.Length > 0)
                {
                    for (int i = 0; i < faktureRows.Length; i++)
                    {
                        Calculate(faktureRows, i, 1);
                    }
                }

                ukupno_promet += (racun_bruto + faktura_ukupno);

                // Add row
                DataRow row = dSRlisteTekst.Tables[0].NewRow();
                row["datum3"] = dateGroup.ToString("dd.MM.yyyy.");
                row["racun_osnovica"] = Math.Round(racun_osnovica, 2, MidpointRounding.AwayFromZero);
                row["racun_pdv"] = Math.Round(racun_pdv, 2, MidpointRounding.AwayFromZero);
                row["racun_pnp"] = Math.Round(racun_pnp, 2, MidpointRounding.AwayFromZero);
                row["racun_bruto"] = Math.Round(racun_bruto, 2, MidpointRounding.AwayFromZero);
                row["racun_gotovina"] = Math.Round(racun_gotovina, 2, MidpointRounding.AwayFromZero);
                row["racun_gotovina_pdv"] = Math.Round(racun_gotovina_pdv, 2, MidpointRounding.AwayFromZero);
                row["racun_kartice"] = Math.Round(racun_kartice, 2, MidpointRounding.AwayFromZero);
                row["racun_kartice_pdv"] = Math.Round(racun_kartice_pdv, 2, MidpointRounding.AwayFromZero);
                row["racun_ostalo"] = Math.Round(racun_ostalo, 2, MidpointRounding.AwayFromZero);
                row["faktura_osnovica"] = Math.Round(faktura_osnovica, 2, MidpointRounding.AwayFromZero);
                row["faktura_pdv"] = Math.Round(faktura_pdv, 2, MidpointRounding.AwayFromZero);
                row["faktura_pnp"] = Math.Round(faktura_pnp, 2, MidpointRounding.AwayFromZero);
                row["faktura_ukupno"] = Math.Round(faktura_ukupno, 2, MidpointRounding.AwayFromZero);
                row["ukupno_promet"] = Math.Round(ukupno_promet, 2, MidpointRounding.AwayFromZero);
                dSRlisteTekst.Tables[0].Rows.Add(row);

                dateGroup = dateGroup.AddDays(1);
                ResetAll();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadPorezData()
        {
            // Gotovina i kartice
            string gkQuery = $@"SELECT roba.id_podgrupa,
                                CAST(racun_stavke.mpc AS numeric) AS mpc,
                                CAST(REPLACE(racun_stavke.rabat, ',', '.') AS numeric) AS rabat,
                                CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
                                ROUND(CAST(REPLACE(racun_stavke.porez, ',', '.') AS numeric), 0) AS pdv,
                                ROUND(CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja
                                FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna
                            LEFT JOIN roba ON racun_stavke.sifra_robe = roba.sifra
                            WHERE racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTgk = classSQL.select(gkQuery, "racun_stavke").Tables[0];

            // Fakture
            string faktureQuery = $@"SELECT roba.id_podgrupa,
                                CAST(faktura_stavke.mpc AS numeric) AS mpc,
                                CAST(REPLACE(faktura_stavke.rabat, ',', '.') AS numeric) AS rabat,
                                CAST(REPLACE(faktura_stavke.kolicina, ',', '.') AS numeric) AS kolicina,
                                ROUND(CAST(REPLACE(faktura_stavke.porez, ',', '.') AS numeric), 0) AS pdv,
                                ROUND(CAST(REPLACE(faktura_stavke.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja
                                FROM faktura_stavke
                            LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture
                            LEFT JOIN roba ON faktura_stavke.sifra = roba.sifra
                            WHERE fakture.date >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND fakture.date <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTfakture = classSQL.select(faktureQuery, "faktura_stavke").Tables[0];

            // Calculate
            CalculateGK(DTgk);
            CalculateFakture(DTfakture);
        }

        /// <summary>
        /// Method used to calculate totals
        /// </summary>
        /// <param name="rows">DataRow array</param>
        /// <param name="index">Row index</param>
        /// <param name="data">0 - racuni, 1 - fakture</param>
        private void Calculate(DataRow[] rows, int index, int data)
        {
            decimal mpc = Convert.ToDecimal(rows[index]["mpc"].ToString());
            decimal pdv = Convert.ToDecimal(rows[index]["pdv"].ToString());
            decimal rabat = Convert.ToDecimal(rows[index]["rabat"].ToString());
            decimal kolicina = Convert.ToDecimal(rows[index]["kolicina"].ToString());
            decimal porez_potrosnja = Convert.ToDecimal(rows[index]["porez_potrosnja"].ToString());

            decimal osnovica = Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) / (1 + (pdv + porez_potrosnja) / 100), 4);
            decimal porez = Math.Round((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * pdv) / (100 + pdv + porez_potrosnja)) / 100), 4);
            decimal pnp = Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * porez_potrosnja) / (100 + pdv + porez_potrosnja)) / 100, 4);

            if (data == 0)
            {
                racun_osnovica += osnovica;
                racun_pdv += porez;
                racun_pnp += pnp;
                racun_bruto += (osnovica + porez + pnp);
                if (rows[index]["nacin_placanja"].ToString().Trim() == "G")
                {
                    racun_gotovina += (osnovica + porez + pnp);
                    racun_gotovina_pdv += porez;
                }
                else if (rows[index]["nacin_placanja"].ToString().Trim() == "K")
                {
                    racun_kartice += (osnovica + porez + pnp);
                    racun_kartice_pdv += porez;
                }
            }
            else if (data == 1)
            {
                faktura_osnovica += osnovica;
                faktura_pdv += porez;
                faktura_pnp += pnp;
                faktura_ukupno += (osnovica + porez + pnp);
            }
        }

        /// <summary>
        /// Method used to calculate "Gotovina i kartice" totals
        /// </summary>
        /// <param name="dataSet"></param>
        private void CalculateGK(DataTable dataTable)
        {
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 1"), 1, 1);
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 2"), 2, 1);
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 3"), 3, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        private void CalculateFakture(DataTable dataTable)
        {
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 1"), 1, 2);
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 2"), 2, 2);
            CalculatePodgrupa(dataTable.Select("id_podgrupa = 3"), 3, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="idPodgrupa"></param>
        /// <param name="data"></param>
        private void CalculatePodgrupa(DataRow[] rows, int idPodgrupa, int data)
        {
            if (rows.Length > 0)
            {
                string tableString = "";
                string rowString = "";
                string podgrupaString = "";
                int tableIndex = 0;

                if (data == 1)
                {
                    tableString = "racun_stavke";
                    rowString = "gk";
                    tableIndex = 1;
                }
                else if (data == 2)
                {
                    tableString = "faktura_stavke";
                    rowString = "f";
                    tableIndex = 2;
                }

                string query = $@"SELECT DISTINCT ROUND(CAST(REPLACE({tableString}.porez, ',', '.') AS numeric), 0) AS pdv,
	                            ROUND(CAST(REPLACE({tableString}.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja
                            FROM {tableString}";
                DataTable DTdistinct = classSQL.select(query, "racun_stavke").Tables[0];

                if (DTdistinct.Rows.Count > 0)
                {
                    for (int i = 0; i < DTdistinct.Rows.Count; i++)
                    {
                        decimal distinctPdv = Convert.ToDecimal(DTdistinct.Rows[i]["pdv"].ToString());
                        decimal distinctPp = Convert.ToDecimal(DTdistinct.Rows[i]["porez_potrosnja"].ToString());

                        for (int j = 0; j < rows.Length; j++)
                        {
                            decimal pdv = Convert.ToDecimal(rows[j]["pdv"].ToString());
                            decimal porez_potrosnja = Convert.ToDecimal(rows[j]["porez_potrosnja"].ToString());
                            decimal mpc = 0;
                            decimal kolicina = 0;
                            decimal rabat = 0;
                            decimal osnovica = 0;
                            decimal porez = 0;
                            decimal pnp = 0;

                            if (distinctPdv == pdv && distinctPp == porez_potrosnja)
                            {
                                mpc = Convert.ToDecimal(rows[j]["mpc"].ToString());
                                kolicina = Convert.ToDecimal(rows[j]["kolicina"].ToString());
                                rabat = Convert.ToDecimal(rows[j]["rabat"].ToString());

                                osnovica = Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) / (1 + (pdv + porez_potrosnja) / 100), 4);
                                porez = Math.Round((((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * pdv) / (100 + pdv + porez_potrosnja)) / 100), 4);
                                pnp = Math.Round(((mpc - (mpc * rabat / 100)) * kolicina) * ((100 * porez_potrosnja) / (100 + pdv + porez_potrosnja)) / 100, 4);

                                #region Gotovina i kartice, transakcijski računi

                                if (idPodgrupa == 1)
                                    podgrupaString = "pice";
                                else if (idPodgrupa == 2)
                                    podgrupaString = "hrana";
                                else if (idPodgrupa == 3)
                                    podgrupaString = "trg";

                                DataRow row = dSRlisteTekst.Tables[tableIndex].NewRow();
                                if (distinctPp == 0)
                                    row[$"{rowString}_{podgrupaString}_naziv"] = $"POREZ: {distinctPdv}%";
                                else
                                    row[$"{rowString}_{podgrupaString}_naziv"] = $"POREZ: {distinctPdv}% + {Math.Round(distinctPp, 4)}%";
                                row[$"{rowString}_{podgrupaString}_stopa_pdv"] = distinctPdv;
                                row[$"{rowString}_{podgrupaString}_osnovica"] = osnovica;
                                row[$"{rowString}_{podgrupaString}_pdv"] = porez;
                                row[$"{rowString}_{podgrupaString}_pnp"] = pnp;
                                row[$"{rowString}_{podgrupaString}_ukupno"] = osnovica + porez + pnp;
                                if (!CheckDataRow(row))
                                    dSRlisteTekst.Tables[tableIndex].Rows.Add(row);

                                // Ukupno podgrupe
                                DataRow rowUkupnoPodgrupa = dSRlisteTekst.Tables[tableIndex].NewRow();
                                if (distinctPp == 0)
                                    rowUkupnoPodgrupa[$"{rowString}_ukupno_naziv"] = $"POREZ: {distinctPdv}%";
                                else
                                    rowUkupnoPodgrupa[$"{rowString}_ukupno_naziv"] = $"POREZ: {distinctPdv}% + {Math.Round(distinctPp, 4)}%";
                                rowUkupnoPodgrupa[$"{rowString}_ukupno_stopa_pdv"] = distinctPdv;
                                rowUkupnoPodgrupa[$"{rowString}_ukupno_osnovica"] = osnovica;
                                rowUkupnoPodgrupa[$"{rowString}_ukupno_pdv"] = porez;
                                rowUkupnoPodgrupa[$"{rowString}_ukupno_pnp"] = pnp;
                                rowUkupnoPodgrupa[$"{rowString}_ukupno_iznos"] = osnovica + porez + pnp;
                                dSRlisteTekst.Tables[tableIndex].Rows.Add(rowUkupnoPodgrupa);

                                #endregion Gotovina i kartice, transakcijski računi

                                #region Sve ukupno

                                // Podgrupe
                                DataRow rowUkupno = dSRlisteTekst.Tables[3].NewRow();
                                if (distinctPp == 0)
                                    rowUkupno[$"uk_{podgrupaString}_naziv"] = $"POREZ: {distinctPdv}%";
                                else
                                    rowUkupno[$"uk_{podgrupaString}_naziv"] = $"POREZ: {distinctPdv}% + {Math.Round(distinctPp, 4)}%";
                                rowUkupno[$"uk_{podgrupaString}_stopa_pdv"] = distinctPdv;
                                rowUkupno[$"uk_{podgrupaString}_osnovica"] = osnovica;
                                rowUkupno[$"uk_{podgrupaString}_pdv"] = porez;
                                rowUkupno[$"uk_{podgrupaString}_pnp"] = pnp;
                                rowUkupno[$"uk_{podgrupaString}_ukupno"] = osnovica + porez + pnp;
                                if (!CheckDataRow(rowUkupno))
                                    dSRlisteTekst.Tables[3].Rows.Add(rowUkupno);

                                // Sve ukupno
                                DataRow rowSveUkupno = dSRlisteTekst.Tables[3].NewRow();
                                if (distinctPp == 0)
                                    rowSveUkupno["uk_naziv"] = $"POREZ: {distinctPdv}%";
                                else
                                    rowSveUkupno["uk_naziv"] = $"POREZ: {distinctPdv}% + {Math.Round(distinctPp, 4)}%";
                                rowSveUkupno["uk_stopa_pdv"] = distinctPdv;
                                rowSveUkupno["uk_osnovica"] = osnovica;
                                rowSveUkupno["uk_pdv"] = porez;
                                rowSveUkupno["uk_pnp"] = pnp;
                                rowSveUkupno["uk_iznos"] = osnovica + porez + pnp;
                                dSRlisteTekst.Tables[3].Rows.Add(rowSveUkupno);

                                #endregion Sve ukupno
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool CheckDataRow(DataRow row)
        {
            foreach (DataColumn column in row.Table.Columns)
                if (!row.IsNull(column))
                    return false;

            return true;
        }

        /// <summary>
        /// Resets all values
        /// </summary>
        private void ResetAll()
        {
            racun_osnovica = 0;
            racun_pdv = 0;
            racun_pnp = 0;
            racun_bruto = 0;
            racun_gotovina = 0;
            racun_gotovina_pdv = 0;
            racun_kartice = 0;
            racun_kartice_pdv = 0;
            racun_ostalo = 0;

            faktura_osnovica = 0;
            faktura_pdv = 0;
            faktura_pnp = 0;
            faktura_ukupno = 0;

            ukupno_promet = 0;
        }
    }
}
