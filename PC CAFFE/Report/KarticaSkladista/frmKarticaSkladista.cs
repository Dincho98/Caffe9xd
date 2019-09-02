using System;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Report.KarticaSkladista
{
    public partial class frmKarticaSkladista : Form
    {
        public frmKarticaSkladista()
        {
            InitializeComponent();
        }

        public string filter { get; internal set; }
        public DataSet DSroba { get; internal set; }
        private DataSet DSsorted = new DataSet();
        public string sortId { get; set; }

        private void repIzdatnica_Load(object sender, EventArgs e)
        {
            if (DSroba != null && DSroba.Tables.Count > 0 && DSroba.Tables[0] != null)
            {
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                SortDataSet(DSroba, sortId);
                LoadDocument();
            }
            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
        {
            decimal pocetno = 0, kalk = 0, primka = 0, inventura = 0, otpis = 0, racuni = 0, meduskladisnice_ulaz = 0, meduskladisnice_izlaz = 0, otpremnica = 0, fakture = 0, izdatnica = 0, nbc = 0;
            foreach (DataRow r in DSsorted.Tables[0].Rows)
            {
                decimal.TryParse(r["pocetno"].ToString(), out pocetno);
                decimal.TryParse(r["primka"].ToString(), out primka);
                decimal.TryParse(r["kalkulacija"].ToString(), out kalk);
                decimal.TryParse(r["inventura"].ToString(), out inventura);
                decimal.TryParse(r["otpis"].ToString(), out otpis);
                decimal.TryParse(r["racuni"].ToString(), out racuni);
                decimal.TryParse(r["fakture"].ToString(), out fakture);
                decimal.TryParse(r["otpremnica"].ToString(), out otpremnica);
                decimal.TryParse(r["izdatnica"].ToString(), out izdatnica);
                decimal.TryParse(r["nbc"].ToString(), out nbc);

                decimal.TryParse(r["izlaz_ms"].ToString(), out meduskladisnice_izlaz);
                decimal.TryParse(r["ulaz_ms"].ToString(), out meduskladisnice_ulaz);

                decimal kolicina = Math.Round(pocetno + primka + kalk + inventura - otpis - racuni - fakture - otpremnica + (meduskladisnice_ulaz - meduskladisnice_izlaz) - izdatnica, 5);

                DataRow dRow = dSRliste.Tables[0].NewRow();
                dRow["sifra"] = r["sifra"].ToString();
                dRow["naziv"] = r["naziv"].ToString();
                dRow["cijena15"] = kolicina.ToString("#0.000");
                dRow["cijena1"] = r["nbc"].ToString();
                dRow["cijena2"] = (nbc * kolicina).ToString();
                dRow["string1"] = r["skladiste"].ToString();
                string isKalkulacija = "NE";
                if (CheckIfKalkulacija())
                {
                    DataTable DTkalkulacijaStavka = Global.Database.GetKalkulacijaByArticleId(r["sifra"].ToString());
                    if (DTkalkulacijaStavka?.Rows.Count > 0)
                    {
                        decimal.TryParse(DTkalkulacijaStavka.Rows[0]["iznos_marze"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal marza);
                        decimal.TryParse(DTkalkulacijaStavka.Rows[0]["prodajna_cijena"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mpc);
                        decimal.TryParse(DTkalkulacijaStavka.Rows[0]["prodajna_cijena_sa_porezom"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vpc);

                        dRow["cijena5"] = (marza * kolicina).ToString();
                        dRow["cijena6"] = (mpc * kolicina).ToString();
                        dRow["cijena7"] = (vpc * kolicina).ToString();
                    }
                    isKalkulacija = "DA";
                }
                dRow["string1"] = isKalkulacija;
                dSRliste.Tables[0].Rows.Add(dRow);
            }
            //for (int i = 0; i < DSroba.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dRow = dSRliste.Tables[0].NewRow();
            //    dRow["sifra"] = DSroba.Tables[0].Rows[i]["sifra"].ToString();
            //    dRow["naziv"] = DSroba.Tables[0].Rows[i]["naziv"].ToString();
            //    dRow["kolicina"] = Math.Round(Convert.ToDecimal(DSroba.Tables[0].Rows[i]["Količina"]), 5).ToString();
            //    dRow["cijena1"] = DSroba.Tables[0].Rows[i]["NBC"].ToString();
            //    dRow["cijena2"] = DSroba.Tables[0].Rows[i]["Ukupno"].ToString();
            //    dRow["string1"] = DSroba.Tables[0].Rows[i]["Skladište"].ToString();

            //    dSRliste.Tables[0].Rows.Add(dRow);
            //}

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
                " '" + filter + "' AS naziv_fakture," +
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="sortedId"></param>
        private void SortDataSet(DataSet dataSet, string sortedId)
        {
            if (DSsorted.Tables.Count > 0)
                DSsorted.Tables.RemoveAt(0);
            string column = "";
            switch (sortedId)
            {
                case "0":
                    column = "sifra";
                    break;
                case "1":
                    column = "naziv";
                    break;
            }
            dataSet.Tables[0].DefaultView.Sort = $"{column} ASC";
            DSsorted.Tables.Add(dataSet.Tables[0].DefaultView.ToTable());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckIfKalkulacija()
        {
            string skladisteNaziv = DSroba.Tables[0].Rows[0]["skladiste"].ToString();
            DataTable DTskladiste = Global.Database.GetSkladisteByName(skladisteNaziv);
            if (DTskladiste != null)
                return DTskladiste.Rows[0]["kalkulacija"].ToString() == "DA" ? true : false;
            else return false;
        }
    }
}