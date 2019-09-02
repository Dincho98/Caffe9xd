using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RESORT.izvjestaji.Prometi
{
    public partial class frmPrometi : Form
    {
        public frmPrometi()
        {
            InitializeComponent();
        }

        private static DataTable DTFakStavke = new DataTable();
        private static DataRow Drow;
        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;
        private DataTable DTpdvNacini = new DataTable();
        private DataRow RowPdvN;

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public DateTime OD { get; set; }
        public DateTime DO { get; set; }
        public string sifra_partnera { get; set; }
        public string imeGosta { get; set; }

        private void repFaktura_Load(object sender, EventArgs e)
        {
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("stavka");
                DTpdv.Columns.Add("osnovica");
            }
            else
            {
                DTpdv.Clear();
            }

            if (DTpdvNacini.Columns["stopa"] == null)
            {
                DTpdvNacini.Columns.Add("stopa");
                DTpdvNacini.Columns.Add("placanje");
                DTpdvNacini.Columns.Add("stavka");
                DTpdvNacini.Columns.Add("osnovica");
            }
            else
            {
                DTpdvNacini.Clear();
            }

            DTFakStavke = dSRfakturaStavke.DTfakturaStavke;
            string _sifraPARTNERA = "";
            string _broj_racuna = "";
            string _imeGosta = "";

            if (sifra_partnera != null && sifra_partnera != "")
            {
                _sifraPARTNERA = String.Format(" AND rfakture.id_partner = '{0}'", sifra_partnera);
            }
            if (dokumenat != null && dokumenat != "")
            {
                _broj_racuna = String.Format(" AND rfakture.broj = '{0}'", dokumenat);
            }
            if (imeGosta != null && imeGosta != "")
            {
                _imeGosta = String.Format(" AND rfaktura_stavke.ime_gosta  ~* '{0}'", imeGosta);
            }

            string sql = String.Format(@"SELECT rfaktura_stavke.broj, rfaktura_stavke.dana, rfaktura_stavke.ukupno, rfaktura_stavke.rabat,
rfaktura_stavke.porez, rfaktura_stavke.avans,
case when (rfakture.id_partner is not null and rfakture.id_partner != 0)
    then concat('(R1 - ', partners.ime_tvrtke, ') ', rfaktura_stavke.ime_gosta)
    else concat('(G) ', rfaktura_stavke.ime_gosta)
end as ime_gosta,
rfaktura_stavke.boravisna_pristojba, rfaktura_stavke.iznos_usluge, rfaktura_stavke.cijena_sobe, rfaktura_stavke.broj_sobe, nacin_placanja.naziv_placanja
FROM rfaktura_stavke
LEFT JOIN rfakture ON rfaktura_stavke.broj=rfakture.broj
left join partners on rfakture.id_partner = partners.id_partner
left join nacin_placanja on rfakture.nacin_placanja = nacin_placanja.id_placanje
WHERE rfakture.datum >= '{0}' AND  rfakture.datum <= '{1}'{2}{3}{4}
order by rfakture.broj;",
    OD.ToString("yyyy-MM-dd H:mm:ss"), DO.ToString("yyyy-MM-dd H:mm:ss"),
    _broj_racuna, _imeGosta, _sifraPARTNERA);

            string databaseName = new INIFile().Read("Postgre", "ime_baze");
            int godina = DateTime.Now.Year;
            godina = Convert.ToInt16(databaseName.Substring(databaseName.Length - 4));

            if (godina >= 2017)
            {
                sql = String.Format(@"SELECT rfaktura_stavke.broj, case when rfaktura_stavke.ukupno < 0 then abs(rfaktura_stavke.dana) * (-1) else rfaktura_stavke.dana end as dana, rfaktura_stavke.ukupno, rfaktura_stavke.rabat,
rfaktura_stavke.porez, rfaktura_stavke.avans,
case when (rfakture.id_partner is not null and rfakture.id_partner != 0)
    then concat('(R1 - ', partners.ime_tvrtke, ') ', rfaktura_stavke.ime_gosta)
    else concat('(G) ', rfaktura_stavke.ime_gosta)
end as ime_gosta,
rfaktura_stavke.boravisna_pristojba, rfaktura_stavke.iznos_usluge, rfaktura_stavke.cijena_sobe, rfaktura_stavke.broj_sobe, nacin_placanja.naziv_placanja, rfaktura_stavke.otpremnica_pnp
FROM rfaktura_stavke
LEFT JOIN rfakture ON rfaktura_stavke.broj=rfakture.broj
left join partners on rfakture.id_partner = partners.id_partner
left join nacin_placanja on rfakture.nacin_placanja = nacin_placanja.id_placanje
WHERE rfakture.datum >= '{0}' AND  rfakture.datum <= '{1}'{2}{3}{4}
order by rfakture.broj;",
    OD.ToString("yyyy-MM-dd H:mm:ss"), DO.ToString("yyyy-MM-dd H:mm:ss"),
    _broj_racuna, _imeGosta, _sifraPARTNERA);
            }

            DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

            //string nas = "Prikaz za razdoblje od: " + OD.ToString("dd.MM.yyyy HH:mm:ss") + " do " + DO.ToString("dd.MM.yyyy HH:mm:ss") + "";
            double ukupno10 = 0;
            foreach (DataRow row in DT.Rows)
            {
                decimal broj_dana = Convert.ToDecimal(row["dana"].ToString());
                try
                {
                    double bor_p2 = 0;
                    double iznos_usluge2 = 0;
                    string ime_gosta = null;

                    if (Convert.ToInt32(row["broj_sobe"]) == -1)
                    {
                        double.TryParse(row["cijena_sobe"].ToString(), out bor_p2);
                    }
                    else
                    {
                        ime_gosta = row["ime_gosta"].ToString();
                    }

                    if (double.TryParse(row["iznos_usluge"].ToString(), out iznos_usluge2)) { iznos_usluge2 = 0; }
                    //double.TryParse(row["iznos_usluge"].ToString(), out iznos_usluge2);

                    ukupno10 = Convert.ToDouble(row["ukupno"].ToString()) + ukupno10;

                    SetTable(Convert.ToInt16(row["broj"].ToString()),
                        Convert.ToDouble(row["dana"].ToString()),
                        bor_p2,
                        iznos_usluge2,
                        Convert.ToDouble(row["porez"].ToString()),
                        Convert.ToDouble(row["ukupno"].ToString()),
                        ime_gosta
                    );
                }
                catch
                {
                }
            }

            decimal ukupno = 0;
            decimal bor_p = 0;
            decimal porez = 0;
            decimal broj_nocenja = 0;
            decimal porez_iznos = 0;
            decimal iznos_usluge = 0;
            decimal cijena_sobe = 0;
            decimal ukupno_za_preracun_osnovice = 0;
            decimal boravisna_ukupno = 0;
            decimal pnp_ukupno = 0;
            decimal pnp_max = 0;
            decimal pnp_iznos = 0;
            decimal otpremnica_pnp = 0;

            foreach (DataRow row in DT.Rows)
            {
                try
                {
                    if (decimal.TryParse(row["boravisna_pristojba"].ToString(), out bor_p)) { bor_p = 0; }
                    if (decimal.TryParse(row["iznos_usluge"].ToString(), out iznos_usluge)) { iznos_usluge = 0; }

                    ukupno = Convert.ToDecimal(row["ukupno"].ToString());
                    porez = Convert.ToDecimal(row["porez"].ToString());
                    otpremnica_pnp = Convert.ToDecimal(row["otpremnica_pnp"].ToString());
                    cijena_sobe = Convert.ToDecimal(row["cijena_sobe"].ToString());
                    broj_nocenja = Convert.ToDecimal(row["dana"].ToString());

                    ukupno_za_preracun_osnovice = ukupno - (bor_p * broj_nocenja);

                    if (pnp_max < otpremnica_pnp)
                        pnp_max = otpremnica_pnp;

                    decimal PreracunataStopaPNP = Convert.ToDecimal((100 * otpremnica_pnp) / (100 + porez + otpremnica_pnp));
                    pnp_iznos = (ukupno_za_preracun_osnovice * PreracunataStopaPNP / 100);

                    //pnp_iznos = 0;
                    //otpremnica_pnp = 0;

                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * porez) / (100 + porez + otpremnica_pnp));
                    porez_iznos = (ukupno_za_preracun_osnovice * PreracunataStopaPDV / 100);

                    if ((int)row["broj_sobe"] != -1)
                    {
                        if (porez == 0)
                        {
                            porez = 0;
                        }

                        StopePDVa(porez, porez_iznos, ukupno_za_preracun_osnovice - porez_iznos - pnp_iznos);
                        StopePDVaNac(row["naziv_placanja"].ToString(), porez, porez_iznos, ukupno_za_preracun_osnovice - porez_iznos - pnp_iznos);
                    }
                    else
                    {
                        bor_p = 0;
                        decimal.TryParse(row["cijena_sobe"].ToString(), out bor_p);
                        bor_p = bor_p * (decimal)row["dana"];
                    }

                    pnp_ukupno += pnp_iznos;
                    boravisna_ukupno += bor_p;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            string porezi = "", nacini = "";//"Boravišna pristojba:" + BrojiRedove((boravisna_ukupno).ToString("#0.00"), 9) + " kn\r\n";
            int max = (DTpdv.Rows.Count >= DTpdvNacini.Rows.Count ? DTpdv.Rows.Count : DTpdvNacini.Rows.Count);

            DataView dv = DTpdvNacini.DefaultView;
            dv.Sort = "placanje, stopa asc";
            DataTable sortedDT = dv.ToTable();
            if (sortedDT.Rows.Count > 0)
            {
                nacini += @"                     Osnovica       Iznos
¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
";
            }
            for (int ii = 0; ii < sortedDT.Rows.Count; ii++)
            {
                string s = BrojiRedove(sortedDT.Rows[ii]["placanje"].ToString() + " " + sortedDT.Rows[ii]["stopa"].ToString() + " %", 14) + " " + BrojiRedove(Convert.ToDecimal(sortedDT.Rows[ii]["osnovica"].ToString()).ToString("##,#0.00"), 15) + BrojiRedove(Convert.ToDecimal(sortedDT.Rows[ii]["stavka"].ToString()).ToString("##,#0.00"), 12);
                nacini += s + Environment.NewLine;
            }

            if (nacini.Length > 0)
            {
                nacini += BrojiRedove("Porez na potrošnju" + " " + pnp_max + "%", 14) + BrojiRedove(pnp_ukupno.ToString("##,#0.00"), 28) + Environment.NewLine;
            }

            if (nacini.Length > 0)
            {
                nacini += BrojiRedove("Bor. pristojba", 14) + BrojiRedove(boravisna_ukupno.ToString("##,#0.00"), 28) + Environment.NewLine;
            }

            for (int ii = 0; ii < DTpdv.Rows.Count; ii++)
            {
                porezi += "Osnovica za PDV " + (DTpdv.Rows[ii]["stopa"].ToString() == "0" ? " " : "") + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["osnovica"].ToString()).ToString("##,#0.00"), 15) + " kn\r\n" +
                          "Iznos PDV " + (DTpdv.Rows[ii]["stopa"].ToString() == "0" ? " " : "") + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["stavka"].ToString()).ToString("##,#0.00"), 15) + " kn\r\n";
            }

            if (nacini.Length > 0)
            {
                porezi += "Porez na potrošnju:" + BrojiRedove(pnp_ukupno.ToString("##,#0.00"), 15) + " kn\r\n";
            }

            if (nacini.Length > 0)
            {
                porezi += "Boravišna pristojba:" + BrojiRedove(boravisna_ukupno.ToString("##,#0.00"), 15) + " kn\r\n";
            }

            string opisNaKraju = nacini + "¤" + porezi;

            string sql1 = "SELECT " +
            " podaci_tvrtke.ime_tvrtke," +
            " podaci_tvrtke.adresa," +
            " podaci_tvrtke.oib," +
            " podaci_tvrtke.grad, " +
            " '" + porezi + "' AS opis_na_kraju_fakture, " +
            " '" + nacini + "' AS nacini, " +
            " 'Računi za razdoblje od " + OD.ToString("dd.MM.yyyy HH:mm:ss") + " do " + DO.ToString("dd.MM.yyyy HH:mm:ss") + "' as naziv_fakture" +
            " FROM podaci_tvrtke " +
            "";

            classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            this.reportViewer1.RefreshReport();
        }

        private static void SetTable(int broj, double broj_dana, double boravisna_pristojba, double usluga, double porez, double ukupno, string imegosta = null, double pnp = 0)
        {
            DataRow[] dataROW = DTFakStavke.Select("broj = '" + broj + "'");
            double preracunataStopaPNP = Convert.ToDouble((100 * pnp) / (100 + porez + pnp));
            double ukupno_pnp = (ukupno - (broj_dana * boravisna_pristojba)) * preracunataStopaPNP / 100;

            double PreracunataStopaPDV = Convert.ToDouble((100 * porez) / (100 + porez + pnp));
            double ukupno_porez = (ukupno - (broj_dana * boravisna_pristojba)) * PreracunataStopaPDV / 100;

            if (dataROW.Count() == 0)
            {
                Drow = DTFakStavke.NewRow();
                Drow["broj"] = broj;
                Drow["ime_gosta"] = (imegosta == null ? "(G)" : imegosta);
                Drow["boravisna_pristojba"] = boravisna_pristojba * broj_dana;
                Drow["iznos_usluge"] = usluga * broj_dana;
                Drow["porez"] = ukupno_porez;
                Drow["ukupno"] = ukupno;
                Drow["otpremnica_pnp"] = ukupno_pnp;
                DTFakStavke.Rows.Add(Drow);
            }
            else
            {
                dataROW[0]["boravisna_pristojba"] = (Convert.ToDouble(dataROW[0]["boravisna_pristojba"].ToString()) + boravisna_pristojba * broj_dana).ToString();
                dataROW[0]["iznos_usluge"] = (Convert.ToDouble(dataROW[0]["iznos_usluge"].ToString()) + usluga * broj_dana).ToString();
                dataROW[0]["porez"] = (Convert.ToDouble(dataROW[0]["porez"].ToString()) + ukupno_porez).ToString();
                dataROW[0]["otpremnica_pnp"] = (Convert.ToDouble(dataROW[0]["otpremnica_pnp"].ToString()) + ukupno_pnp).ToString();
                dataROW[0]["ukupno"] = (Convert.ToDouble(dataROW[0]["ukupno"].ToString()) + ukupno).ToString();
                dataROW[0]["ime_gosta"] = ((dataROW[0]["ime_gosta"].ToString().Length == 0 || dataROW[0]["ime_gosta"].ToString() == "(G)") && imegosta != null ? imegosta : dataROW[0]["ime_gosta"]);
            }
        }

        private void StopePDVa(decimal pdv, decimal pdv_stavka, decimal osnovica)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + (pdv == 0 ? pdv.ToString() : pdv.ToString("#,##")) + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = (pdv == 0 ? pdv.ToString() : pdv.ToString("#,##"));
                RowPdv["stavka"] = pdv_stavka.ToString();
                RowPdv["osnovica"] = osnovica.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["stavka"] = Convert.ToDecimal(dataROW[0]["stavka"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private void StopePDVaNac(string placanje, decimal pdv, decimal pdv_stavka, decimal osnovica)
        {
            DataRow[] dataROW = DTpdvNacini.Select("stopa = '" + (pdv == 0 ? pdv.ToString() : pdv.ToString("#,##")) + "' and placanje = '" + placanje.Trim() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdvN = DTpdvNacini.NewRow();
                RowPdvN["placanje"] = placanje.Trim();
                RowPdvN["stopa"] = (pdv == 0 ? pdv.ToString() : pdv.ToString("#,##"));
                RowPdvN["stavka"] = pdv_stavka.ToString();
                RowPdvN["osnovica"] = osnovica.ToString();
                DTpdvNacini.Rows.Add(RowPdvN);
            }
            else
            {
                dataROW[0]["stavka"] = Convert.ToDecimal(dataROW[0]["stavka"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private string BrojiRedove(string str, int br)
        {
            string vrati = "";
            for (int i = 0; i < br; i++)
            {
                if (vrati.Length == (br - str.Length))
                {
                    vrati += str;
                    return vrati;
                }
                else if ((br - str.Length) < br)
                {
                    vrati += " ";
                }
            }

            return vrati;
        }

        private void frmPrometi_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}