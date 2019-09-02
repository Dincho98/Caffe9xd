using PCPOS.PosPrint;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.predracuni
{
    public partial class frmPOSMaliPrinter : Form
    {
        public frmPOSMaliPrinter()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string podgrupa { get; set; }
        public string grupa { get; set; }
        public string ducan { get; set; }
        public bool ispis_stavka { get; set; }
        public bool ispis_sifra { get; set; }
        public DataGridView dgv { get; set; }

        public bool zbirno { get; set; }

        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        private int RecLineChars;
        public string idstol { get; set; }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private string tekst = "";

        private void frmMaliPrinter_Load(object sender, EventArgs e)
        {
            RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString()) + 3;
            PrometProdajneRobe();
        }

        public string GetZaposlenici()
        {
            string zaposlenik = "";
            string z = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
                {
                    z += "'" + dgv.Rows[i].Cells["id"].FormattedValue.ToString() + "',";
                }

                if (i == dgv.Rows.Count - 1)
                {
                    if (z.Length > 1)
                    {
                        z = z.Remove(z.Length - 1);
                        zaposlenik = "(" + z + ")";
                    }
                }
            }
            return zaposlenik;
        }

        private void PrometProdajneRobe()
        {
            string uvjet = "";
            if (ducan != null)
            {
                uvjet += " AND svi_predracuni.id_ducan='" + ducan + "'";
            }

            string blag = GetZaposlenici();
            if (blag.Length > 0)
            {
                uvjet += " AND svi_predracuni.id_zaposlenik IN " + blag + "";
            }

            if (idstol != "")
            {
                uvjet += "AND svi_predracuni.id_stol='" + idstol + "'";
            }

            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
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
            DataTable DTtvrtka = classSQL.select(sql1, "podaci_tvrtka").Tables[0];

            string sql_liste = "";

            sql_liste = "SELECT " +
               " svi_predracuni.sifra," +
               " svi_predracuni.mpc," +
               " svi_predracuni.kolicina*svi_predracuni.mpc AS cijena6," +
               " svi_predracuni.porez AS cijena5," +
               " svi_predracuni.id_stol," +
               " svi_predracuni.id_zaposlenik," +
               " svi_predracuni.datum_ispisa," +
               " svi_predracuni.kolicina," +
               " svi_predracuni.vpc," +
               " svi_predracuni.porez_potrosnja," +
               " svi_predracuni.naziv," +
               " svi_predracuni.broj AS cijena1," +
               " svi_predracuni.id_blagajna," +
               " ducan.ime_ducana," +
               " stolovi.naziv AS naziv_stol," +
               " blagajna.ime_blagajne" +
               " FROM svi_predracuni " +
               " LEFT JOIN stolovi ON stolovi.id_stol=CAST(svi_predracuni.id_stol AS INT) " +
               " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=CAST(svi_predracuni.id_zaposlenik AS INT) " +
               " LEFT JOIN blagajna ON blagajna.id_blagajna=CAST(svi_predracuni.id_blagajna AS INT) " +
               " LEFT JOIN ducan ON ducan.id_ducan=CAST(svi_predracuni.id_ducan AS INT) " +
               " WHERE svi_predracuni.datum_ispisa>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "'" +
               " AND svi_predracuni.datum_ispisa<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "' " + uvjet + " ORDER BY svi_predracuni.naziv ASC"; //" ORDER BY CAST(id AS INT) ASC";

            DataTable DT = classSQL.select(sql_liste, "racun_stavke").Tables[0];

            if (zbirno)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    Artikli(DT.Rows[i]["naziv"].ToString(), Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), DT.Rows[i]["sifra"].ToString(), DT.Rows[i]["mpc"].ToString());
                }
            }

            sql_liste = "SELECT SUM(mpc*kolicina) as mpc, zaposlenici.ime + ' '+zaposlenici.prezime as zaposlenik " +
               " FROM svi_predracuni " +
               " LEFT JOIN stolovi ON stolovi.id_stol=CAST(svi_predracuni.id_stol AS INT) " +
               " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=CAST(svi_predracuni.id_zaposlenik AS INT) " +
               " LEFT JOIN blagajna ON blagajna.id_blagajna=CAST(svi_predracuni.id_blagajna AS INT) " +
               " LEFT JOIN ducan ON ducan.id_ducan=CAST(svi_predracuni.id_ducan AS INT) " +
               " WHERE svi_predracuni.datum_ispisa>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "'" +
               " AND svi_predracuni.datum_ispisa<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "' " + uvjet + " GROUP BY zaposlenik "; //" ORDER BY CAST(id AS INT) ASC";

            DataTable DT_group_zap = classSQL.select(sql_liste, "racun_stavke").Tables[0];

            string Zaposlenici = "";
            decimal mm = 0;
            foreach (DataRow r in DT_group_zap.Rows)
            {
                decimal.TryParse(r["mpc"].ToString(), out mm);
                Zaposlenici += r["zaposlenik"].ToString() + ":\t\t" + Math.Round(mm, 2).ToString("N2") + " kn\r\n";
            }

            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OD: " + datumOD.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine("DO: " + datumDO.ToString("dd.MM.yyyy H:mm:ss"));
            PrintTextLine(new string('-', RecLineChars));

            string artikl = "";
            string stol = "";
            string datum = "";
            string kol = "";

            if (artikl.Length > 18)
            {
                artikl = artikl.Remove(18);
            }
            decimal d = 0;
            if (!zbirno)
            {
                PrintTextLine(String.Empty);
                PrintText(TruncateAt("STAVKA".PadRight(15), 15));
                PrintText(TruncateAt("KOL".PadLeft(5), 5));
                PrintText(TruncateAt("STOL".PadLeft(10), 10));
                PrintText(TruncateAt("DATUM".PadLeft(8), 8));
                PrintText("\r\n");
                PrintTextLine(new string('=', RecLineChars));

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    artikl = DT.Rows[i]["naziv"].ToString();
                    stol = DT.Rows[i]["naziv_stol"].ToString();
                    kol = DT.Rows[i]["kolicina"].ToString();
                    try
                    {
                        datum = Convert.ToDateTime(DT.Rows[i]["datum_ispisa"].ToString()).ToString("MM.dd.yy");
                        d += Convert.ToDecimal(DT.Rows[i]["mpc"].ToString()) * Convert.ToDecimal(kol);
                    }
                    catch { }

                    if (artikl.Length > 15)
                    {
                        artikl = artikl.Remove(15);
                    }

                    if (datum.Length > 8)
                    {
                        datum = datum.Remove(8);
                    }

                    PrintText(TruncateAt(artikl.PadRight(15), 15));
                    PrintText(TruncateAt(kol.PadLeft(5), 5) + " ");
                    PrintText(TruncateAt(stol.PadLeft(10), 10));
                    PrintText(TruncateAt(datum.PadLeft(8), 8));
                    PrintText("\r\n");
                }
            }
            else
            {
                PrintTextLine(String.Empty);
                PrintText(TruncateAt("STAVKA".PadRight(16), 16));
                PrintText(TruncateAt("KOL".PadLeft(6), 6));
                PrintText(TruncateAt("CIJENA".PadLeft(7), 7));
                PrintText(TruncateAt("UKUPNO".PadLeft(7), 7));
                PrintText("\r\n");
                PrintTextLine(new string('=', RecLineChars));

                d = 0;
                for (int i = 0; i < DTartikli.Rows.Count; i++)
                {
                    artikl = DTartikli.Rows[i]["naziv"].ToString();

                    kol = DTartikli.Rows[i]["kolicina"].ToString();

                    if (artikl.Length > 16)
                    {
                        artikl = artikl.Remove(16);
                    }

                    decimal ukupno_zb = 0;
                    try
                    {
                        ukupno_zb = Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()) * Convert.ToDecimal(kol);
                    }
                    catch { }

                    PrintText(TruncateAt(artikl.PadRight(16), 16));
                    PrintText(TruncateAt(kol.PadLeft(6), 6) + " ");
                    PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()).ToString("#0.00").PadLeft(7), 7));
                    PrintText(TruncateAt(Math.Round(ukupno_zb, 3).ToString("#0.00").PadLeft(7), 7));
                    PrintText("\r\n");

                    d += ukupno_zb;
                }
            }

            PrintTextLine(new string('=', RecLineChars));
            PrintTextLine("\r\nUKUPNO: " + Math.Round(d).ToString("#0.00"));
            tekst += "\r\n\r\n" + Zaposlenici;

            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);
            rtb.Font = font;
            rtb.Text = tekst;
        }

        private DataTable DTartikli = new DataTable();
        private DataRow RowArtikl;

        private void Artikli(string artikl, decimal kolicina, string sifra, string mpc)
        {
            if (DTartikli.Columns["sifra"] == null)
            {
                DTartikli.Columns.Add("sifra");
                DTartikli.Columns.Add("kolicina");
                DTartikli.Columns.Add("mpc");
                DTartikli.Columns.Add("naziv");
            }

            DataRow[] dataROW = DTartikli.Select("sifra = '" + sifra + "' AND mpc='" + mpc + "'");

            if (dataROW.Count() == 0)
            {
                RowArtikl = DTartikli.NewRow();
                RowArtikl["sifra"] = sifra;
                RowArtikl["mpc"] = mpc;
                RowArtikl["kolicina"] = kolicina.ToString();
                RowArtikl["naziv"] = artikl;
                DTartikli.Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["kolicina"] = Convert.ToDecimal(dataROW[0]["kolicina"].ToString()) + kolicina;
            }
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

            //header
            String drawString = tekst;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(tekst, drawFont);

            drawFont = font;
            float y = 0.0F;
            float x = 0.0F;

            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        private void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            //if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                string ttx = "\r\n" + tekst;
                ttx = ttx.Replace("č", "c");
                ttx = ttx.Replace("Č", "C");
                ttx = ttx.Replace("ž", "z");
                ttx = ttx.Replace("Ž", "Z");
                ttx = ttx.Replace("ć", "c");
                ttx = ttx.Replace("Ć", "C");
                ttx = ttx.Replace("đ", "d");
                ttx = ttx.Replace("Đ", "D");
                ttx = ttx.Replace("š", "s");
                ttx = ttx.Replace("Š", "S");

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
            }
            //else
            //{
            //    if (!printDoc.PrinterSettings.IsValid)
            //    {
            //        string msg = String.Format(
            //           "Can't find printer \"{0}\".", printerName);
            //        MessageBox.Show(msg, "Print Error");
            //        return;
            //    }
            //    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //    printDoc.Print();

            //}
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = "Izvješće.txt";
            //saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.OpenFile().ToString(), rtb.Text);
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            printaj();
        }
    }
}