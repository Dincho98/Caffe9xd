using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.PrometiPoDanima
{
    public partial class frmListePoDanima : Form
    {
        public bool spremiPdf;

        public frmListePoDanima(bool spremiPdf=false)
        {
            InitializeComponent();
            this.spremiPdf = spremiPdf;
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string id_podgrupa { get; set; }

        public string grupa { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private ReportParameter p1;
        private ReportParameter p2;
        private ReportParameter p3;

        private void frmListe_Load(object sender, EventArgs e)
        {
            int height = SystemInformation.VirtualScreen.Height;
            this.Height = height - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            //DTartikli = dSRliste.Tables[0];

            this.reportViewer1.RefreshReport();

            promjenaCijene();
            PrometProdajneRobe();

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("ObracunPrometaPoDanima", reportViewer1);
                this.Close();
            }
        }

        private void promjenaCijene()
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

        private DataTable DTpdv = new DataTable();
        private DataTable DTartikli = new DataTable();
        private DataRow RowPdv;
        private DataRow RowOsnovica;
        private DataRow RowArtikl;

        private void StopePDVa(decimal pdv, decimal pdv_stavka)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
            }
        }

        private void Artikli(string datum, decimal osnovica, decimal pdv, decimal pnp, decimal mpc, decimal gotovina, decimal kartice, decimal transakcijski, decimal ostalo)
        {
            if (kartice > 0)
            {
            }

            DataRow[] dataROW = dSRliste.Tables[0].Select("sifra = '" + datum + "'");

            if (dataROW.Count() == 0)
            {
                RowArtikl = dSRliste.Tables[0].NewRow();
                RowArtikl["sifra"] = datum;
                RowArtikl["cijena1"] = Math.Round(osnovica, 3).ToString("#0.000");
                RowArtikl["cijena2"] = Math.Round(pdv, 3).ToString("#0.000");
                RowArtikl["cijena3"] = Math.Round(pnp, 3).ToString("#0.000");
                ;
                RowArtikl["cijena5"] = Math.Round(mpc, 3).ToString("#0.000");
                ;
                RowArtikl["cijena6"] = Math.Round(gotovina, 3).ToString("#0.000");
                ;
                RowArtikl["cijena7"] = Math.Round(kartice, 3).ToString("#0.000");
                ;
                RowArtikl["cijena8"] = Math.Round(transakcijski, 3).ToString("#0.000");
                ;
                RowArtikl["cijena9"] = Math.Round(ostalo, 3).ToString("#0.000");
                ;
                dSRliste.Tables[0].Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["cijena1"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena1"].ToString()) + osnovica), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena2"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena2"].ToString()) + pdv), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena3"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena3"].ToString()) + pnp), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena5"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena5"].ToString()) + mpc), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena6"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena6"].ToString()) + gotovina), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena7"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena7"].ToString()) + kartice), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena8"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena8"].ToString()) + transakcijski), 3).ToString("#0.000");
                ;
                dataROW[0]["cijena9"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena9"].ToString()) + ostalo), 3).ToString("#0.000");
                ;
            }
        }

        private void PrometProdajneRobe()
        {
            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
                p3 = new ReportParameter("blagajnik", GetZaposlenikName());
            }
            else
            {
                p3 = new ReportParameter("blagajnik", "");
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string gr = "";
            if (grupa != null)
            {
                gr = " AND grupa.id_grupa='" + grupa + "'";
            }

            string sql = string.Format(@"SELECT
                racun_stavke.kolicina,
                grupa.grupa,
                racun_stavke.sifra_robe,
                racun_stavke.mpc,
                racun_stavke.porez_potrosnja,
                racun_stavke.porez,
                racuni.nacin_placanja,
                racuni.datum_racuna,
                roba.naziv
                FROM racun_stavke
                LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
                LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa
                WHERE  racuni.datum_racuna >= '{0}' AND racuni.datum_racuna <= '{1}'
                {2}{3}{4}{5}
                ORDER BY racuni.datum_racuna ASC;",
                datumOD.ToString("yyyy-MM-dd H:mm:ss"),
                datumDO.ToString("yyyy-MM-dd H:mm:ss"),
                blag, duc, art, gr);
            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

            sql = @"SELECT '' AS string1,CAST(racuni.datum_racuna AS DATE) as datum,
                CAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL) as stopa,CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL) as stopa_pnp,
                ROUND(SUM(
	                ((CAST(racun_stavke.mpc AS DECIMAL)-
	                ((CAST(racun_stavke.mpc AS DECIMAL)*
	                CAST(REPLACE(racun_stavke.rabat,',','.') AS DECIMAL)/100)))/
	                (1zbroj((CAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL))/100)))*CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL)
                ),4) AS osnovica,
                ROUND(SUM(
	                ((CAST(racun_stavke.mpc AS DECIMAL)-
	                ((CAST(racun_stavke.mpc AS DECIMAL)*
	                CAST(REPLACE(racun_stavke.rabat,',','.') AS DECIMAL)/100)))*CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL))*
	                ((100*CAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL))/(100zbrojCAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL)))/100
                ),4) AS decimal1,
                ROUND(SUM(
	                ((CAST(racun_stavke.mpc AS DECIMAL)-
	                ((CAST(racun_stavke.mpc AS DECIMAL)*
	                CAST(REPLACE(racun_stavke.rabat,',','.') AS DECIMAL)/100)))*CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL))*
	                ((100*CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL))/(100zbrojCAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL)))/100
                ),4) AS decimal2,
                ROUND(SUM(
	                (CAST(racun_stavke.mpc AS DECIMAL)-
	                ((CAST(racun_stavke.mpc AS DECIMAL)*
	                CAST(REPLACE(racun_stavke.rabat,',','.') AS DECIMAL)/100)))*CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL)
                ),4) AS iznos
                FROM racun_stavke
                LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
                LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa
                WHERE  racuni.datum_racuna >= '" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "' " + blag + duc + art + gr +
                " GROUP BY CAST(REPLACE(racun_stavke.porez,',','.') AS DECIMAL),CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS DECIMAL),CAST(racuni.datum_racuna AS DATE) ORDER BY datum";

            classSQL.NpgAdatpter(sql).Fill(dSstope, "DTstope");

            int __pnp, __pdv;
            decimal __Dpnp, __Dpdv;
            foreach (DataRow r in dSstope.Tables[0].Rows)
            {
                decimal.TryParse(r["stopa_pnp"].ToString(), out __Dpnp);
                decimal.TryParse(r["stopa"].ToString(), out __Dpdv);

                __pnp = Convert.ToInt16(__Dpnp);
                __pdv = Convert.ToInt16(__Dpdv);

                if (__pnp > 0)
                    r["string1"] = "Porez " + __pdv.ToString() + "+" + __pnp.ToString() + "%";
                else
                    r["string1"] = "Porez " + __pdv.ToString() + "%";
            }

            decimal kol = 0;
            decimal pnp = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal pnpUKUPNO = 0;
            decimal pdvUKUPNO = 0;
            decimal SVE_UKUPNO = 0;
            decimal OSNOVICA = 0;
            string g = "";

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
            }

            if (DTartikli.Columns["sifra"] == null)
            {
                DTartikli.Columns.Add("sifra");
                DTartikli.Columns.Add("kolicina");
                DTartikli.Columns.Add("mpc");
                DTartikli.Columns.Add("naziv");
            }

            if (DTpdvN.Columns["stopa"] == null)
            {
                DTpdvN.Columns.Add("stopa");
                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
            }
            else
            {
                DTpdvN.Clear();
            }

            int oo = DT.Rows.Count;

            foreach (DataRow row in DT.Rows)
            {
                kol = Convert.ToDecimal(row["kolicina"].ToString());
                mpc = Convert.ToDecimal(row["mpc"].ToString());
                pnp = Convert.ToDecimal(row["porez_potrosnja"].ToString());
                pdv = Convert.ToDecimal(row["porez"].ToString());

                //Ovaj kod dobiva PDV
                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
                decimal ppdv = Math.Round((((mpc * kol) * PreracunataStopaPDV) / 100), 4);
                pdvUKUPNO = Math.Round((ppdv + pdvUKUPNO), 4);

                //Ovaj kod dobiva porez na potrošnju
                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
                decimal ppnp = Math.Round((((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100), 4);
                pnpUKUPNO = Math.Round((ppnp + pnpUKUPNO), 4);

                SVE_UKUPNO = Math.Round(((mpc * kol) + SVE_UKUPNO), 4);

                decimal UG = 0;
                decimal UK = 0;
                decimal UV = 0;
                decimal UO = 0;

                if (row["nacin_placanja"].ToString() == "G" || row["nacin_placanja"].ToString() == "")
                {
                    StopePDVaN(pdv, ppdv, "G", Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4));
                    UG = Math.Round((mpc * kol), 4);
                }
                else if (row["nacin_placanja"].ToString() == "K")
                {
                    StopePDVaN(pdv, ppdv, "K", Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4));
                    UK = Math.Round((mpc * kol), 4);
                }
                else if (row["nacin_placanja"].ToString() == "T")
                {
                    StopePDVaN(pdv, ppdv, "T", Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4));
                    UV = Math.Round((mpc * kol), 4);
                }
                else if (row["nacin_placanja"].ToString() == "O")
                {
                    StopePDVaN(pdv, ppdv, "O", Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4));
                    UO = Math.Round((mpc * kol), 4);
                }

                string ajjj = row["nacin_placanja"].ToString();

                DateTime d = Convert.ToDateTime(row["datum_racuna"].ToString());
                decimal o = Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4);
                decimal p = Math.Round((((mpc * kol) * PreracunataStopaPDV) / 100), 4);

                Artikli(d.ToString("dd.MM.yyyy"), o, p, ppnp, mpc * kol, UG, UK, UV, UO);

                StopePDVa(pdv, Math.Round((((mpc * kol) * PreracunataStopaPDV) / 100), 4));

                OSNOVICA = Math.Round(((mpc * kol) - ((ppdv) + (ppnp))), 4) + OSNOVICA;
            }

            string porezi = "";
            for (int i = 0; i < DTpdvN.Rows.Count; i++)
            {
                //if (Convert.ToDecimal(DTpdvN.Rows[i]["stopa"].ToString()) > 0)
                {
                    string nacin_pplacanja = "";

                    if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "G")
                    {
                        nacin_pplacanja = "GOTOVINA: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "T")
                    {
                        nacin_pplacanja = "TRANSAKCIJSKI RAČUN: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "O")
                    {
                        nacin_pplacanja = "OSTALO: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "K")
                    {
                        nacin_pplacanja = "KARTICE: ";
                    }

                    porezi += "Način plačanja: " + nacin_pplacanja +
                        "\r\nOsnovica za stopu od " + DTpdvN.Rows[i]["stopa"].ToString() + " %: " + Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()), 3).ToString("#0.00") + "" +
                        "\r\nIznos poreza za stopu od " + DTpdvN.Rows[i]["stopa"].ToString() + " %: " + Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()), 3).ToString("#0.00") +
                        "\r\n\r\n";
                }
            }

            p1 = new ReportParameter("datum", "Od datuma: " + datumOD.ToString("dd.MM.yyyy") + " do datuma " + datumDO.ToString("dd.MM.yyyy"));
            p2 = new ReportParameter("stope_poreza", porezi);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });
        }

        private DataTable DTpdvN = new DataTable();

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            if (osnovica < 0 && Convert.ToInt16(pdv) == 0)
            {
            }

            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = Math.Round(osnovica, 3);
                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + Math.Round(osnovica, 3);
            }
        }

        /// <summary>
        /// Method used to get selected "blagajnik" full name
        /// </summary>
        /// <returns></returns>
        private string GetZaposlenikName()
        {
            string result = "";
            DataTable dataTable = classSQL.select($"SELECT ime, prezime FROM zaposlenici WHERE id_zaposlenik = {blagajnik}", "zaposlenici").Tables[0];
            if (dataTable.Rows.Count > 0)
                result = dataTable.Rows[0]["ime"].ToString() + " " + dataTable.Rows[0]["prezime"].ToString();
            return result;
        }
    }
}