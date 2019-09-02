using PCPOS.PosPrint;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmSviRacuni : Form
    {
        public frmSviRacuni()
        {
            InitializeComponent();
        }

        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTrac;
        private static DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];
        private static DataTable dt_blaganik;
        private static DataTable dt_blagajna = classSQL.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'", "zaposlenici").Tables[0];
        private static DataTable dt_ducan = classSQL.select("SELECT ime_ducana FROM ducan WHERE id_ducan='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'", "zaposlenici").Tables[0];

        private static string tekst;

        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
        private static string _storno = "";

        private static String Underline = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'u', (byte)'C' });
        private static String Italic = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'i', (byte)'C' });
        private static String CenterAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        private static String RightAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        private static String DoubleWideCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        private static String DoubleHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'3', (byte)'C' });
        private static String DoubleWideAndHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'4', (byte)'C' });
        private static String Bold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        private static String Normal = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'n', (byte)'C' });

        private static string _1;
        private static string _2;
        private static string _3;
        private static string _4;
        private static string _5;
        private static string fiskal_tekst;
        private static string kockice;
        private static Image img_barcode = null;

        private static DataTable DTpdv = new DataTable();
        private static DataRow RowPdv;

        private static string BrojRacunaa;
        private static double ukupno_rabat;

        private void frmSviRacuni_Load(object sender, EventArgs e)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            Font font = new Font(privateFonts.Families[0], 11);
            rtb.Font = font;
        }

        private DataTable DTsend = new DataTable();

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DataRow row;

            string sql = "SELECT " +
                " racuni.id_blagajnik, " +
                " racuni.broj_racuna, " +
                " racuni.id_kupac, " +
                " racuni.nacin_placanja, " +
                " racuni.storno, " +
                " racuni.jir, " +
                " racuni.zik " +
                " FROM racuni " +
                " WHERE racuni.datum_racuna>'" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND racuni.datum_racuna<'" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "' " +
                " ORDER BY racuni.datum_racuna ASC";
            DataTable DT = classSQL.select(sql, "racuni").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string kol;
                DTsend.Rows.Clear();
                DataTable DTa = classSQL.select("SELECT " +
                    " racun_stavke.sifra_robe," +
                    " racun_stavke.id_skladiste," +
                    " racun_stavke.mpc," +
                    " racun_stavke.porez," +
                    " racun_stavke.porez_potrosnja," +
                    " racun_stavke.kolicina," +
                    " racun_stavke.rabat," +
                    " racun_stavke.vpc," +
                    " racun_stavke.nbc," +
                    " roba.naziv AS ime" +
                    " FROM racun_stavke LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                    " WHERE broj_racuna='" + DT.Rows[i]["broj_racuna"].ToString() + "'  AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'" +
                    " ORDER BY racun_stavke.id_stavka", "racun_stavke").Tables[0];

                for (int j = 0; j < DTa.Rows.Count; j++)
                {
                    row = DTsend.NewRow();
                    row["broj_racuna"] = DT.Rows[i]["broj_racuna"].ToString();
                    row["sifra_robe"] = DTa.Rows[j]["sifra_robe"].ToString();
                    row["id_skladiste"] = DTa.Rows[j]["id_skladiste"].ToString();
                    row["mpc"] = DTa.Rows[j]["mpc"].ToString();
                    row["porez"] = DTa.Rows[j]["porez"].ToString();
                    row["kolicina"] = DTa.Rows[j]["kolicina"].ToString();
                    row["rabat"] = DTa.Rows[j]["rabat"].ToString();
                    row["vpc"] = DTa.Rows[j]["vpc"].ToString();
                    row["nbc"] = DTa.Rows[j]["nbc"].ToString();
                    row["cijena"] = DTa.Rows[j]["mpc"].ToString();
                    row["ime"] = DTa.Rows[j]["ime"].ToString();
                    row["porez_potrosnja"] = DTa.Rows[j]["porez_potrosnja"].ToString();
                    DTsend.Rows.Add(row);
                }

                PrintReceipt(DTsend,
                    DT.Rows[i]["id_blagajnik"].ToString(),
                    DT.Rows[i]["broj_racuna"].ToString(),
                    DT.Rows[i]["id_kupac"].ToString(),
                    "",
                    DT.Rows[i]["broj_racuna"].ToString(),
                    DT.Rows[i]["nacin_placanja"].ToString(),
                    DT.Rows[i]["storno"].ToString(),
                    DT.Rows[i]["jir"].ToString(),
                    DT.Rows[i]["zik"].ToString()
                    );
            }

            rtb.Text = "\r\n" + _1 + _2 + fiskal_tekst + kockice + _3 + _4 + _5;
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            printaj();
        }

        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string broj_racuna, string kupac, string barcode, string brRac, string placanje, string storno, string jir, string zik)
        {
            dt_blaganik = classSQL.select("SELECT ime,prezime,oib FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
            BrojRacunaa = brRac;

            _storno = storno;

            tekst = "";
            try
            {
                DTrac = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + brRac + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
                kupac = DTrac.Rows[0]["id_kupac"].ToString();
                PrintReceiptHeader(DT.Rows[0]["skraceno_ime"].ToString(), DT.Rows[0]["adresa"].ToString(), "My State, My Country", DT.Rows[0]["tel"].ToString(), Convert.ToDateTime(DTrac.Rows[0]["datum_racuna"].ToString()), blagajnik, brRac, kupac);

                if (DTpdv.Columns["stopa"] == null)
                {
                    DTpdv.Columns.Add("stopa");
                    DTpdv.Columns.Add("osnovica");
                    DTpdv.Columns.Add("iznos");
                }
                else
                {
                    DTpdv.Clear();
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
                ukupno_rabat = 0;
                double rabat = 0;

                for (int i = 0; i < DTstavke.Rows.Count; i++)
                {
                    double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
                    double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                    double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
                    double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
                    double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                    double mpc = cijena * kolicina;

                    rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
                    ukupno_rabat = (mpc * rabat / 100) + ukupno_rabat;
                    mpc = mpc - (mpc * rabat / 100);

                    //Ovaj kod dobiva PDV
                    double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                    pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                    //Ovaj kod dobiva porez na potrošnju
                    double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                    PrintLineItem(DTstavke.Rows[i]["ime"].ToString(), kolicina, cijena, DTstavke.Rows[i]["rabat"].ToString() + "%", mpc);

                    //izračun porez potrosnja
                    Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                    //izračun osnovica
                    //izračun osnovica
                    if (rabat > 0)
                    {
                        double pps = (100 * (PDV + PP)) / (100 + PDV + PP);
                        osnovica = mpc - (mpc * pps / 100);
                    }
                    else
                    {
                        osnovica = VPC * kolicina;
                    }
                    osnovica_sve = (osnovica * kolicina) + osnovica_sve;

                    StopePDVa(PDV, pdv_stavka, (osnovica * kolicina));

                    //Izracun pdv
                    pdv_sve = pdv_sve + (pdv_stavka);

                    //ukupno sve
                    //ukupno = Convert.ToDouble((osnovica_sve + pdv_sve).ToString("#0.00")) + Porez_potrosnja_sve;
                    ukupno = mpc + ukupno;
                }

                porez_na_potrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
                porez_na_potrosnju[1] = Convert.ToString(osnovica_sve);
                porez_na_potrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

                string[] fiskalizacija = new string[3];

                fiskalizacija[0] = jir;
                fiskalizacija[1] = zik;
                fiskalizacija[2] = "";

                PrintReceiptFooter(osnovica_sve, pdv_sve, Porez_potrosnja_sve, ukupno, barcode, fiskalizacija, Porez_potrosnja_sve);
            }
            finally
            {
                //printaj();
            }
        }

        private static void StopePDVa(double pdv, double pdv_stavka, double osnovica)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = pdv.ToString();
                RowPdv["osnovica"] = osnovica.ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = (Convert.ToDouble(dataROW[0]["iznos"].ToString()) + pdv_stavka).ToString("#0.0000");
                dataROW[0]["osnovica"] = (Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + osnovica).ToString("#0.0000");
            }
        }

        private static void PrintReceiptFooter(double subTotal, double tax, double discount, double ukupno, string barcode, string[] fiskalizacija, double PNP)
        {
            string offSetString = new string(' ', 0);

            PrintTextLine(new string('=', RecLineChars));

            try
            {
                if (ukupno_rabat > 0)
                {
                    PrintTextLine("UKUPNO POPUST: " + Math.Round(ukupno_rabat, 3).ToString("#0.00") + " kn");
                }

                if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
                {
                    for (int i = 0; i < DTpdv.Rows.Count; i++)
                    {
                        PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00") + "     OSNOVICA: " + Convert.ToDecimal(DTpdv.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                    }
                }
                else
                {
                    PrintTextLine("PDV NIJE URAČUNAT U CIJENU!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            PrintTextLine(offSetString + String.Format("PNP:       {0}", PNP.ToString("#0.00")));

            try
            {
                if (Convert.ToDouble(DTrac.Rows[0]["dobiveno_gotovina"].ToString()) > 0)
                {
                    tekst += "GOTOVINA NOVČANICE: " + Convert.ToDouble(DTrac.Rows[0]["dobiveno_gotovina"].ToString()).ToString("#0.00") + " kn\n";
                }
                else if (DTrac.Rows[0]["nacin_placanja"].ToString() == "K")
                {
                    tekst += "PLAĆENO KARTICOM.\r\n";
                }
                else if (DTrac.Rows[0]["nacin_placanja"].ToString() == "O")
                {
                    tekst += "OSTALI NAČINI PLAČANJA.\r\n";
                }
                else if (DTrac.Rows[0]["nacin_placanja"].ToString() == "T")
                {
                    tekst += "PLAĆANJE VIRMAN.\r\n\r\nUPLATA NA:\r\n";
                    if (DT.Rows[0]["zr"].ToString() != "")
                    {
                        tekst += "ŽR: " + DT.Rows[0]["zr"].ToString() + "\r\n";
                    }
                    if (DT.Rows[0]["zr"].ToString() != "")
                    {
                        tekst += "IBAN: " + DT.Rows[0]["iban"].ToString() + "\r\n";
                    }
                    tekst += "MODEL BR: " + BrojRacunaa + "-" + Convert.ToDateTime(DTrac.Rows[0]["datum_racuna"].ToString()).Year + "\r\n";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            PrintTextLine(offSetString + new string('-', RecLineChars));

            _2 += tekst;
            tekst = "";

            PrintTextLine(offSetString + String.Format("UKUPNO:    {0}", ukupno.ToString("#0.00")));
            _2 += tekst;
            tekst = "";

            _2 += "JIR: " + "\r\n" + fiskalizacija[0] + "\r\n";
            _2 += "ZKI: " + "\r\n" + fiskalizacija[1] + "\r\n";

            //Embed 'center' alignment tag on front of string below to have it printed in the center of the receipt.
            //PrintTextLine(printer, System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' }) + footerText);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["aktivnost"].ToString() == "DA" && barcode != "")
                {
                    tekst += offSetString + new string('-', (RecLineChars)) + Environment.NewLine;
                    tekst += "Naša zahvala za Vašu kupovinu." + Environment.NewLine;

                    double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

                    tekst += UKpopust.ToString("#0.00") + " kn popusta." + Environment.NewLine + Environment.NewLine;
                    _4 += tekst;
                    tekst = "";

                    DateTime RunsUntil;
                    DateTime dvo = DateTime.Now;
                    RunsUntil = dvo.AddDays(Convert.ToInt16(dt.Rows[0]["traje_do"].ToString()));

                    tekst = "Popust odgovara " + dt.Rows[0]["popust"].ToString() + "% vrijednosti kupovine \r\nkoju dobivate kod iduće kupovine. \r\nTrajanje kupona do " + RunsUntil.ToString() + Environment.NewLine;
                    tekst += "Gotovinska isplata nije moguća." + Environment.NewLine + "Iznos sljedeće kupovine mora biti jednak \r\nili veći od vrijednosti bona." + Environment.NewLine;
                }
                else
                {
                    tekst = "";
                }
            }
            else
            {
                tekst = "";
            }
        }

        private static void PrintLineItem(string artikl, double kolicina, double cijena, string popust, double cijena_sve)
        {
            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            try
            {
                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                {
                    PrintText(TruncateAt(artikl.PadRight(a), a));
                }
                else
                {
                    tekst += classOstalo.SvavkaZaPrinter(artikl, a);
                }
            }
            catch
            {
                PrintText(TruncateAt(artikl.PadRight(a), a));
            }

            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
        }

        private static void PrintReceiptHeader(string companyName, string addressLine1, string addressLine2, string taxNumber, DateTime dateTime, string cashierName, string broj, string kupac)
        {
            PrintTextLine("\r\n");
            PrintTextLine(new string('*', RecLineChars));
            PrintTextLine(new string('*', RecLineChars));
            PrintTextLine("Datum: " + dateTime);
            PrintTextLine("OIB: " + DT.Rows[0]["oib"].ToString());
            PrintTextLine(new string('-', RecLineChars));
            PrintTextLine(String.Format("Blagajnik : {0}", dt_blaganik.Rows[0]["ime"].ToString() + " " + dt_blaganik.Rows[0]["prezime"].ToString()));
            PrintTextLine(String.Format("Račun broj : {0}", broj + "/" + dt_ducan.Rows[0][0].ToString() + "/" + dt_blagajna.Rows[0][0].ToString()));

            if (kupac != "" && kupac != "0")
            {
                DataTable DTkupac = classSQL.select("SELECT partners.ime_tvrtke,partners.adresa,partners.oib,grad.grad,grad.posta FROM partners LEFT JOIN grad ON partners.id_grad=grad.id_grad WHERE id_partner='" + kupac + "'", "partners").Tables[0];
                PrintTextLine(String.Empty);
                PrintTextLine(new string('=', RecLineChars));
                PrintTextLine("Račun " + DT.Rows[0]["r1"].ToString());
                PrintTextLine("KUPAC:");
                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
                PrintTextLine(new string('=', RecLineChars));
            }

            _2 += tekst;
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
            PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
            PrintText("\r\n");
            PrintTextLine(new string('=', RecLineChars));
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

        private static void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            string drawString = _1 + _2 + _3;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                _5 += Environment.NewLine;
            }

            string ttx = "\r\n" + _1 + _2 + fiskal_tekst + kockice + _3 + _4 + _5;
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
    }
}