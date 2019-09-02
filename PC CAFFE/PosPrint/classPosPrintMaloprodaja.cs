using GenCode128;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintMaloprodaja
    {
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTrac;
        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
        private static DataTable dt_blaganik = classSQL.select("SELECT ime,prezime,oib FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
        private static string rHeader;
        private static string rStavke;
        private static string rUkupno;
        private static string rZahvala;
        private static string rBottom;
        private static Image img_barcode = null;

        private static string tekst;

        private static DataTable DTpdv = new DataTable();
        private static DataRow RowPdv;

        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string broj_racuna, string kupac, string barcode, string brRac, string placanje)
        {
            img_barcode = Code128Rendering.MakeBarcodeImage("000" + brRac, int.Parse("3"), true);

            tekst = "";

            try
            {
                DTrac = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + brRac + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];

                PrintReceiptHeader(DT.Rows[0]["skraceno_ime"].ToString(), DT.Rows[0]["adresa"].ToString(), "My State, My Country", DT.Rows[0]["tel"].ToString(), DateTime.Now, blagajnik, broj_racuna, kupac);

                if (DTpdv.Columns["stopa"] == null)
                {
                    DTpdv.Columns.Add("stopa");
                    DTpdv.Columns.Add("osnovica");
                    DTpdv.Columns.Add("iznos");
                }

                /////priprema za fiskalizaciju
                DataTable DTOstaliPor = new DataTable();
                DTOstaliPor.Columns.Add("naziv");
                DTOstaliPor.Columns.Add("stopa");
                DTOstaliPor.Columns.Add("osnovica");
                DTOstaliPor.Columns.Add("iznos");
                DataRow RowOstaliPor;

                DataTable DTnaknade = new DataTable();
                DTnaknade.Columns.Add("naziv");
                DTnaknade.Columns.Add("iznos");
                DataRow ROWnaknade;

                string[] porez_na_potrosnju = new string[3];
                porez_na_potrosnju[0] = "";
                porez_na_potrosnju[1] = "";
                porez_na_potrosnju[2] = "";

                string iznososlobpdv = "";
                string iznos_marza = "";
                /////kraj priprema za fiskalizaciju

                double osnovica = 0;
                double pdv_stavka = 0;
                double Porez_potrosnja_stavka = 0;
                double ukupno = 0;

                double osnovica_sve = 0;
                double Porez_potrosnja_sve = 0;
                double pdv_sve = 0;
                double rabat_sve = 0;

                int maxPNP = 0;
                for (int i = 0; i < DTstavke.Rows.Count; i++)
                {
                    double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
                    double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                    double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
                    double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
                    double rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
                    double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                    double mpc = (cijena * kolicina) - ((cijena * kolicina) * Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString()) / 100);
                    mpc = Convert.ToDouble(mpc.ToString("#0.00"));

                    if (maxPNP < PP)
                        maxPNP = (int)PP;

                    rabat_sve = ((cijena * kolicina) - mpc) + rabat_sve;

                    //Ovaj kod dobiva PDV
                    double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                    pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                    //Ovaj kod dobiva porez na potrošnju
                    double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                    PrintLineItem(DTstavke.Rows[i]["ime"].ToString(), kolicina, cijena, DTstavke.Rows[i]["rabat"].ToString() + "%", mpc);

                    //izračun porez potrosnja
                    Porez_potrosnja_sve = (Porez_potrosnja_stavka * kolicina) + Porez_potrosnja_sve;

                    //izračun osnovica
                    osnovica = mpc / Convert.ToDouble("1," + (Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString()) + Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString())));
                    osnovica_sve = (osnovica) + osnovica_sve;

                    //Izracun pdv
                    pdv_sve = pdv_sve + (pdv_stavka);

                    //ukupno sve
                    ukupno = Convert.ToDouble((osnovica_sve + pdv_sve).ToString("#0.00")) + Porez_potrosnja_sve;
                }

                porez_na_potrosnju[0] = maxPNP.ToString();
                porez_na_potrosnju[1] = Convert.ToString(osnovica_sve);
                porez_na_potrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

                bool pdv = false;
                if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
                {
                    pdv = true;
                }

                string[] fiskalizacija;

                if (DTfis.Rows[0]["aktivna"].ToString() == "1")
                {
                    fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                    DT.Rows[0]["oib"].ToString(),
                    dt_blaganik.Rows[0]["oib"].ToString(),
                    DTstavke,
                    DateTime.Now,
                    pdv,
                    Convert.ToInt32(brRac),
                    DTpostavke.Rows[0]["default_ducan"].ToString(),
                    Convert.ToInt32(DTpostavke.Rows[0]["default_blagajna"].ToString()),
                    DTpdv,
                    porez_na_potrosnju,
                    DTOstaliPor,
                    iznososlobpdv,
                    iznos_marza,
                    DTnaknade,
                    Convert.ToDecimal(ukupno),
                    placanje,
                    false,
                    osnovica_sve
                    );
                }
                else
                {
                    fiskalizacija = new string[0];
                }

                PrintReceiptFooter(osnovica_sve, pdv_sve, rabat_sve, ukupno, barcode, fiskalizacija, placanje);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                printaj();
            }
        }

        private static void PrintReceiptFooter(double subTotal, double tax, double discount, double ukupno, string barcode, string[] fiskalizacija, string placanje)
        {
            //string _placanje = "";

            //if (placanje == "G")
            //{
            //    _placanje = "gotovina";
            //}
            //else if (placanje == "K")
            //{
            //    _placanje = "kartica";
            //}
            //else
            //{
            //    _placanje = "ostalo";
            //}

            string offSetString = new string(' ', 0);

            PrintTextLine(new string('-', (RecLineChars / 3) * 2));

            string za_povrat = "Plačeno: ";
            decimal ukupno_gotovina = Convert.ToDecimal(DTrac.Rows[0]["ukupno_gotovina"].ToString());
            decimal ukupno_kartice = Convert.ToDecimal(DTrac.Rows[0]["ukupno_kartice"].ToString());
            decimal dobiveno_gotovina = Convert.ToDecimal(DTrac.Rows[0]["dobiveno_gotovina"].ToString());

            if (ukupno_gotovina > 0 && ukupno_kartice == 0)
            {
                za_povrat += "Gotovina: " + ukupno_gotovina.ToString("#0.00") + " ";
            }
            else if (ukupno_kartice > 0 && ukupno_gotovina == 0)
            {
                za_povrat += "Kartice: " + ukupno_kartice.ToString("#0.00");
            }
            else
            {
                za_povrat += "Ostalo";
            }

            PrintTextLine(za_povrat);

            if (dobiveno_gotovina > 0)
            {
                za_povrat = "Za vratiti :" + Convert.ToDecimal(dobiveno_gotovina - ukupno_gotovina - ukupno_kartice).ToString("#0.00") + "\r\n";
                PrintTextLine(za_povrat);
            }

            PrintTextLine(offSetString + String.Format("OSNOVICA:  {0}", subTotal.ToString("#0.00")));
            PrintTextLine(offSetString + String.Format("PDV:       {0}", tax.ToString("#0.00")));
            PrintTextLine(offSetString + String.Format("POPUST:    {0}", discount.ToString("#0.00")));

            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
            {
                tekst += "JIR:" + fiskalizacija[0] + "\r\n" + "ZKI:" + fiskalizacija[1] + "\r\n";
            }

            PrintTextLine(offSetString + new string('-', RecLineChars));

            rStavke = tekst;
            tekst = "";

            PrintTextLine(offSetString + String.Format("UKUPNO:    {0}", ukupno.ToString("#0.00")));

            rUkupno = tekst;
            tekst = "";

            DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];

            if (dt.Rows[0]["aktivnost"].ToString() == "DA" && barcode != "")
            {
                tekst += offSetString + new string('-', (RecLineChars)) + Environment.NewLine;
                tekst += "Naša zahvala za Vašu kupovinu." + Environment.NewLine;

                double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

                tekst += UKpopust.ToString("#0.00") + " kn popusta." + Environment.NewLine + Environment.NewLine;
                rZahvala = tekst;
                tekst = "";

                DateTime RunsUntil;
                DateTime dvo = DateTime.Now;
                RunsUntil = dvo.AddDays(Convert.ToInt16(dt.Rows[0]["traje_do"].ToString()));
                ;

                tekst = Environment.NewLine + "Popust odgovara " + dt.Rows[0]["popust"].ToString() + "% vrijednosti kupovine \r\nkoju dobivate kod iduće kupovine. \r\nTrajanje kupona do " + RunsUntil.ToString() + Environment.NewLine;
                tekst += "Gotovinska isplata nije moguća." + Environment.NewLine + "Iznos sljedeće kupovine mora biti jednak \r\nili veći od vrijednosti bona." + Environment.NewLine;
            }
            else
            {
                tekst = "";
            }

            tekst += Environment.NewLine + Environment.NewLine + DTsetting.Rows[0]["bottom_text"].ToString() + Environment.NewLine;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }
            rBottom = tekst;
            tekst = "";
        }

        private static void PrintLineItem(string artikl, double kolicina, double cijena, string popust, double cijena_sve)
        {
            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            PrintText(TruncateAt(artikl.PadRight(a), a));
            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
            PrintText(TruncateAt(popust.ToString().PadLeft(p), p));
            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
        }

        private static void PrintReceiptHeader(string companyName, string addressLine1, string addressLine2, string taxNumber, DateTime dateTime, string cashierName, string broj, string kupac)
        {
            PrintTextLine(companyName);
            PrintTextLine("Adresa: " + addressLine1);

            PrintTextLine("Telefon: " + taxNumber);
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT.Rows[0]["oib"].ToString());
            PrintTextLine(new string('-', RecLineChars / 2));

            PrintTextLine(String.Format("Blagajnik : {0}", cashierName));
            PrintTextLine(String.Format("Racun broj : {0}", broj));

            if (kupac != "" && kupac != "0")
            {
                DataTable DTkupac = classSQL.select("SELECT partners.ime_tvrtke,partners.adresa,partners.oib,grad.grad,grad.posta FROM partners LEFT JOIN grad ON partners.id_grad=grad.id_grad WHERE id_partner='" + kupac + "'", "partners").Tables[0];
                PrintTextLine(String.Empty);
                PrintTextLine("Račun " + DT.Rows[0]["r1"].ToString());
                PrintTextLine("KUPAC:");
                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
            }
            rHeader = tekst;
            tekst = "";

            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            PrintTextLine(String.Empty);
            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
            PrintText(TruncateAt("POPUST".PadLeft(p), p));
            PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
            PrintText("\r\n");
            PrintTextLine(new string('=', RecLineChars));
            //PrintTextLine(String.Empty);
        }

        private static void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private static void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private static string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private static void PrintPage(object o, PrintPageEventArgs e)
        {
            float height = 0;

            //header
            String drawString = rHeader;
            Font drawFont = new Font("Lucida Console", 11, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(rHeader, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //stavke
            drawString = rStavke;
            drawFont = new Font("Lucida Console", 10, FontStyle.Regular);
            float y = height;
            float x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(rStavke, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Ukupno
            drawString = rUkupno;
            drawFont = new Font("Lucida Console", 10, FontStyle.Bold);
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(rUkupno, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Naša zahvala
            drawString = rZahvala;
            drawFont = new Font("Lucida Console", 12, FontStyle.Regular);
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(rZahvala, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Barcode
            if (img_barcode != null)
            {
                System.Drawing.Image img = img_barcode;
                e.Graphics.DrawImage(img_barcode, 0, height, 250, 50);
            }

            //Bottom
            drawString = rBottom;
            drawFont = new Font("Lucida Console", 10, FontStyle.Regular);
            y = height + 60;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

            stringSize = e.Graphics.MeasureString(rBottom, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            if (e.HasMorePages)
            {
                e.HasMorePages = false;
            }

            if (height > e.PageSettings.PaperSize.Height)
            {
                PaperSize psNew = new System.Drawing.Printing.PaperSize("Racun", e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize.Width, (int)height + 1);
                Size sSize = new Size(psNew.Width, psNew.Height);

                e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = psNew;
                e.PageSettings.PrinterSettings.DefaultPageSettings.Bounds.Inflate(sSize);
                e.PageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);
                e.PageSettings.PrinterSettings.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);

                e.PageSettings.PaperSize = psNew;

                e.PageSettings.Bounds.Inflate(sSize);

                e.PageBounds.Inflate(sSize);

                e.PageSettings.PrintableArea.Inflate(sSize);

                e.HasMorePages = true;
                e.Graphics.Clear(Color.White);
                e.Graphics.ResetClip();
                e.Graphics.Clip.MakeEmpty();
                //e.Graphics.Dispose();
                //e.Graphics.
            }
        }

        private static void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            //byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            //IntPtr pUnmanagedBytes = new IntPtr(0);
            //pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            //Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            //RawPrinterHelper.SendBytesToPrinter(printDoc.PrinterSettings.PrinterName, pUnmanagedBytes, 5);
            //Marshal.FreeCoTaskMem(pUnmanagedBytes);

            string drawString = rHeader + rStavke;

            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                   "Can't find printer \"{0}\".", printerName);
                MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();

            //string GS = Convert.ToString((char)29);
            //string ESC = Convert.ToString((char)27);

            //string COMMAND = "";
            //COMMAND = ESC + "@";
            //COMMAND += GS + "V" + (char)1;

            //RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, COMMAND);
        }
    }
}