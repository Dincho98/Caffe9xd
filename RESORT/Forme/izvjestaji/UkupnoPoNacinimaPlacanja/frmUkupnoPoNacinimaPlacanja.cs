using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.izvjestaji.UkupnoPoNacinimaPlacanja
{
    public partial class frmUkupnoPoNacinimaPlacanja : Form
    {
        private int godinaBaze = 0;
        private DataTable DTBojeForme;

        public frmUkupnoPoNacinimaPlacanja()
        {
            InitializeComponent();
        }

        private void frmUkupnoPoNacinimaPlacanja_Load(object sender, EventArgs e)
        {
            try
            {
                DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
                INIFile ini = new INIFile();
                string imeBaze = ini.Read("Postgre", "ime_baze");
                string godinaBaze = imeBaze.Substring(imeBaze.Length - 4, 4);
                int.TryParse(godinaBaze, out this.godinaBaze);
                if (this.godinaBaze == 0)
                    this.godinaBaze = DateTime.Now.Year;

                dtpOdDatuma.Value = new DateTime(this.godinaBaze, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtpDoDatuma.Value = new DateTime(this.godinaBaze, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format(@"SELECT SUM(COALESCE(rfaktura_stavke.ukupno, 0)) AS ukupno, nacin_placanja.naziv_placanja as ime_gosta
FROM rfaktura_stavke
LEFT JOIN rfakture ON rfaktura_stavke.broj = rfakture.broj
LEFT JOIN nacin_placanja ON rfakture.nacin_placanja = nacin_placanja.id_placanje
WHERE rfakture.datum >= '{0:yyyy-MM-dd HH:mm:ss}' AND  rfakture.datum <= '{1:yyyy-MM-dd HH:mm:ss}'
GROUP BY nacin_placanja.naziv_placanja
ORDER BY nacin_placanja.naziv_placanja;",
    dtpOdDatuma.Value, dtpDoDatuma.Value);

                RemoteDB.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSRfakturaStavke, "DTfakturaStavke");

                //DataSet dsUkupnoPoNacinima = RemoteDB.select(sql, "rfakture");
                //string rezultat = "";
                //if (dsUkupnoPoNacinima != null && dsUkupnoPoNacinima.Tables.Count > 0 && dsUkupnoPoNacinima.Tables[0] != null && dsUkupnoPoNacinima.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow item in dsUkupnoPoNacinima.Tables[0].Rows)
                //    {
                //        if (rezultat.Length > 0)
                //        {
                //            rezultat += Environment.NewLine;
                //        }
                //        rezultat += ((item["naziv_placanja"].ToString()).Trim()).PadRight(40) + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + ((((decimal)item["ukupno"]).ToString("#,##0.00")).Trim()).PadLeft(40);
                //    }
                //}

                //string opisNaKraju = nacini + "¤" + porezi;

                string sql1 = "SELECT " +
                " podaci_tvrtke.ime_tvrtke," +
                " podaci_tvrtke.adresa," +
                " podaci_tvrtke.oib," +
                " podaci_tvrtke.grad, " +
                //" '" + porezi + "' AS opis_na_kraju_fakture, " +
                //" '" + nacini + "' AS nacini, " +
                " 'Ukupno po načinima plaćanja od " + dtpOdDatuma.Value.ToString("dd.MM.yyyy HH:mm:ss") + " do " + dtpDoDatuma.Value.ToString("dd.MM.yyyy HH:mm:ss") + "' as naziv_fakture" +
                " FROM podaci_tvrtke " +
                "";

                classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

                //ReportParameter rp = new ReportParameter("rezultat", rezultat);
                //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                this.reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmUkupnoPoNacinimaPlacanja_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}