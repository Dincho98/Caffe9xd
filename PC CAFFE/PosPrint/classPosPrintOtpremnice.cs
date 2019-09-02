using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintOtpremnice
    {
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTotpremnica;
        private static DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];
        private static DataTable dt_blaganik;
        private static DataTable dt_blagajna = classSQL.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'", "zaposlenici").Tables[0];
        private static DataTable dt_ducan = classSQL.select("SELECT ime_ducana FROM ducan WHERE id_ducan='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'", "zaposlenici").Tables[0];

        private static string tekst;

        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
        private static int broj_slova_na_racunu = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());

        private static String id_kasa = DTpostavke.Rows[0]["default_blagajna"].ToString();
        private static String id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();

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
        public static int id_adresa_dostave { get; set; }
        private static string brOtpremnica { get; set; }
        private static double ukupno_rabat = 0;

        private static int pageHeight = 0;
        private static Graphics grRacun = null;

        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string broj_racuna, string kupac, string barcode, string brOtpr, string placanje, decimal kIznosOduzeti, bool na_sobu = false, bool fiskaliziran = true)
        {
            brOtpremnica = brOtpr;

            try
            {
                int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
                int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
                int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
                int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
                int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

                RecLineChars = a + k + c + p + s;
            }
            catch
            {
            }
            DataView dv = DTstavke.DefaultView;
            dv.Sort = "naziv asc";
            DTstavke = dv.ToTable();

            DataTable dtPomoc = DTstavke.Clone();
            string _vpc = "", _nbc = "", _god = "", _br = "", _pdv = "", _sifra = "", _naziv = "", _pp = "", _rab = "", _oduzmi = "", _skl = "";
            string vpc = "", nbc = "", god = "", br = "", pdv = "", sifra = "", naziv = "", pp = "", rab = "", oduzmi = "", skl = "";

            foreach (DataRow drRow in DTstavke.Rows)
            {
                if (!DTstavke.Columns.Contains("kol"))
                {
                    DTstavke.Columns.Add("kol", typeof(decimal));
                }
                decimal kol = 0;
                decimal.TryParse(drRow["kolicina"].ToString(), out kol);
                drRow["kol"] = kol;
            }

            foreach (DataRow drRow in DTstavke.Rows)
            {
                vpc = drRow["vpc"].ToString();
                nbc = drRow["nbc"].ToString();
                god = drRow["godina_otpremnice"].ToString();
                br = drRow["broj_otpremnice"].ToString();
                pdv = drRow["porez"].ToString();
                sifra = drRow["sifra_robe"].ToString();
                naziv = drRow["naziv"].ToString();
                pp = drRow["porez_potrosnja"].ToString();
                rab = drRow["rabat"].ToString();
                oduzmi = drRow["oduzmi"].ToString();
                skl = drRow["id_skladiste"].ToString();

                string sPostoji = String.Format("vpc = '{0}' and nbc = '{1}' and godina_otpremnice = '{2}' and broj_otpremnice = '{3}' and porez = '{4}' and sifra_robe = '{5}' and " + (naziv.Length == 0 ? "(naziv is null or naziv = '{6}')" : "naziv = '{6}'") + " and porez_potrosnja = '{7}' and rabat = '{8}' and " + (oduzmi.Length == 0 ? "(oduzmi is null or oduzmi = '{9}')" : "oduzmi = '{9}'") + " and id_skladiste = '{10}'", vpc, nbc, god, br, pdv, sifra, naziv, pp, rab, oduzmi, skl);

                int postoji = dtPomoc.Select(sPostoji).Count();

                if ((vpc != _vpc || nbc != _nbc || god != _god || br != _br || pdv != _pdv || sifra != _sifra ||
                naziv != _naziv || pp != _pp || rab != _rab || oduzmi != _oduzmi || skl != _skl) && postoji == 0)
                {
                    DataRow dRow = dtPomoc.NewRow();
                    object sumObj = null;
                    sumObj = DTstavke.Compute("Sum(kol)", sPostoji);
                    dRow["kolicina"] = sumObj.ToString();
                    dRow["vpc"] = vpc;
                    dRow["nbc"] = nbc;
                    dRow["godina_otpremnice"] = god;
                    dRow["broj_otpremnice"] = br;
                    dRow["porez"] = pdv;
                    dRow["sifra_robe"] = sifra;
                    dRow["naziv"] = naziv;
                    dRow["porez_potrosnja"] = pp;
                    dRow["rabat"] = rab;
                    dRow["oduzmi"] = oduzmi;
                    dRow["id_skladiste"] = skl;

                    dtPomoc.Rows.Add(dRow);
                }

                _vpc = vpc;
                _nbc = nbc;
                _god = god;
                _br = br;
                _pdv = pdv;
                _sifra = sifra;
                _naziv = naziv;
                _pp = pp;
                _rab = rab;
                _oduzmi = oduzmi;
                _skl = skl;
            }

            dt_blaganik = classSQL.select("SELECT ime,prezime,oib FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

            BrojRacunaa = brOtpr;
            //img_barcode = Code128Rendering.MakeBarcodeImage("000" + brRac, int.Parse("3"), true);

            //_storno = storno;

            tekst = "";
            try
            {
                DTotpremnica = classSQL.select("SELECT * FROM otpremnice WHERE broj_otpremnice='" + brOtpr + "'" +
                    " AND godina_otpremnice = '" + DateTime.Now.Year.ToString() + "'", "otpremnice").Tables[0];

                if (DTotpremnica.Rows.Count == 0)
                {
                    MessageBox.Show("Greška sa otpremnicom: " + brOtpr);
                    return;
                }

                kupac = DTotpremnica.Rows[0]["osoba_partner"].ToString();

                PrintReceiptHeader(DT.Rows[0]["ime_tvrtke"].ToString(), DT.Rows[0]["skraceno_ime"].ToString(), DT.Rows[0]["adresa"].ToString(), "My State, My Country", DT.Rows[0]["tel"].ToString(), Convert.ToDateTime(DTotpremnica.Rows[0]["datum"].ToString()), blagajnik, brOtpr, kupac, Convert.ToBoolean(DTotpremnica.Rows[0]["na_sobu"].ToString()));

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
                double osnovica_pnp = 0;
                double Porez_potrosnja_sve = 0;
                double pdv_sve = 0;
                ukupno_rabat = 0;
                double rabat = 0;

                double maxPNP = 0;

                for (int i = 0; i < dtPomoc.Rows.Count; i++)
                {
                    //if (Convert.ToInt32(DTstavke.Rows[i]["dod"]) != 2) {
                    double kolicina = Convert.ToDouble(dtPomoc.Rows[i]["kolicina"].ToString().Replace('.', ','));
                    double PP = 0;
                    if (dtPomoc.Columns.Contains("porez_potrosnja"))
                        PP = Convert.ToDouble(dtPomoc.Rows[i]["porez_potrosnja"].ToString());

                    double PDV = Convert.ToDouble(dtPomoc.Rows[i]["porez"].ToString().Replace('.', ','));
                    double VPC = Convert.ToDouble(dtPomoc.Rows[i]["vpc"].ToString().Replace('.', ','));
                    double cijena = Math.Round(((VPC * (PP + PDV) / 100) + VPC), 3);
                    double mpc = cijena * kolicina;

                    rabat = Convert.ToDouble(dtPomoc.Rows[i]["rabat"].ToString().Replace('.', ','));
                    ukupno_rabat = (mpc * rabat / 100) + ukupno_rabat;
                    mpc = Math.Round((mpc - (mpc * rabat / 100)), 3);

                    //Ovaj kod dobiva PDV
                    double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                    pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                    //Ovaj kod dobiva porez na potrošnju
                    double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                    if (PP > 0)
                    {
                        osnovica_pnp += mpc / (1 + (PP + PDV) / 100);
                    }

                    if (maxPNP < PP)
                        maxPNP = PP;

                    PrintLineItem(dtPomoc.Rows[i]["naziv"].ToString(), kolicina, cijena, dtPomoc.Rows[i]["rabat"].ToString() + "%", mpc);

                    //izračun porez potrosnja
                    Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

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

                    osnovica_sve = (osnovica) + osnovica_sve;

                    StopePDVa(PDV, pdv_stavka, (osnovica));

                    //Izracun pdv
                    pdv_sve = pdv_sve + (pdv_stavka);
                    ukupno = mpc + ukupno;

                    //}
                }

                porez_na_potrosnju[0] = maxPNP.ToString().Replace(".", ",");
                porez_na_potrosnju[1] = Convert.ToString(osnovica_pnp);
                porez_na_potrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

                string[] fiskalizacija = new string[3];

                if (kIznosOduzeti > 0)
                {
                    ukupno = ukupno - (double)kIznosOduzeti;
                }

                PrintReceiptFooter(osnovica_sve, pdv_sve, Porez_potrosnja_sve, ukupno, barcode, fiskalizacija, Porez_potrosnja_sve, fiskaliziran);
            }
            finally
            {
                int maxPrint = 1;

                int.TryParse(DTsetting.Rows[0]["ispisOtpremnica"].ToString(), out maxPrint);
                for (int i = 0; i < maxPrint; i++)
                {
                    printaj();
                }
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

        private static void PrintReceiptFooter(double subTotal, double tax, double discount, double ukupno, string barcode, string[] fiskalizacija, double PNP, bool fiskaliziran = true)
        {
            string offSetString = new string(' ', 0);

            PrintTextLine(new string('=', RecLineChars));

            try
            {
                if (ukupno_rabat > 0)
                {
                    PrintTextLine("UKUPNO POPUST: " + Math.Round(ukupno_rabat, 3).ToString("#0.00") + " kn");
                }

                decimal popust_cijelog_racuna = 0;
                if (popust_cijelog_racuna > 0)
                {
                    decimal popust_iznos = 0;
                    popust_iznos = ((decimal)ukupno * (1 + (((100 * popust_cijelog_racuna) / (100 - popust_cijelog_racuna)) / 100))) - (decimal)ukupno;
                    PrintTextLine("UKUPNO POPUSTA NA RAČUN: " + popust_cijelog_racuna + "%");
                    PrintTextLine("POPUST U KN: " + Math.Round(popust_iznos, 3).ToString("#0.00") + " kn");
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            PrintTextLine(offSetString + String.Format("PNP:     {0}", PNP.ToString("#0.00")));
            //PrintTextLine(offSetString + String.Format("POPUST:    {0}", discount.ToString("#0.00")));

            PrintTextLine(offSetString + new string('-', RecLineChars));

            _2 = tekst;
            tekst = "";

            PrintTextLine(offSetString + String.Format("UKUPNO:    {0}", ukupno.ToString("#0.00") + " KN"));
            _3 = tekst;
            tekst = "";

            if (!fiskaliziran)
            {
                tekst += Environment.NewLine + Environment.NewLine;
                PrintTextLine("OVO NIJE FISKALIZIRAN RAČUN");
            }

            tekst += Environment.NewLine + Environment.NewLine;
            tekst += "POTPIS:" + Environment.NewLine;

            //tekst

            PrintTextLine(new string('˙', RecLineChars));
            //tekst += Environment.NewLine;
            string[] napomena = DTotpremnica.Rows[0]["napomena"].ToString().Trim().Split(' ');

            if (napomena.Count() > 0 && napomena[0] != "")
            {
                //tekst += Environment.NewLine;

                string n = "";
                string linja = "";
                for (int iii = 0; iii < napomena.Length; iii++)
                {
                    if (n.Length == 0 && linja.Length == 0)
                        linja = "NAPOMENA:";

                    string newLinja = linja + " " + napomena[iii];
                    if (newLinja.Length > broj_slova_na_racunu || iii + 1 == napomena.Count())
                    {
                        if (newLinja.Length > broj_slova_na_racunu)
                        {
                            n += linja + Environment.NewLine;
                            linja = "";
                            linja += napomena[iii];
                        }
                        else
                        {
                            n += newLinja + Environment.NewLine;
                            linja = "";
                        }

                        iii++;
                    }
                    if (linja.Length > 0)
                        linja += " ";
                    if (iii < napomena.Count())
                        linja += napomena[iii];
                }

                tekst += n;
            }
            string[] lines = DTsetting.Rows[0]["bottom_text"].ToString().Split('\n');
            tekst += Environment.NewLine + Environment.NewLine;

            foreach (string line in lines)
            {
                int brojText = line.Trim().Length;
                int brojOstatak = (RecLineChars - brojText) / 2;
                string praznaMjesta = "";
                for (int _br = 0; _br < brojOstatak; _br++) { praznaMjesta += " "; }
                tekst += praznaMjesta + line.Trim() + "\r\n";
            }

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }
            _5 = tekst;
            tekst = "";
        }

        private static string SetNewLine(string text, int br)
        {
            string vrati = "";
            int NL = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (NL == br)
                {
                    vrati += text[i].ToString() + "\r\n";
                    NL = 0;
                }
                else
                {
                    vrati += text[i].ToString();
                    NL++;
                }
            }

            return vrati;
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
                    PrintText(TruncateAt(artikl.PadRight(a), a).ToUpper());
                }
                else
                {
                    tekst += classOstalo.SvavkaZaPrinter(artikl, a).ToUpper();
                }
            }
            catch
            {
                PrintText(TruncateAt(artikl.PadRight(a), a));
            }

            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
            if (File.Exists("hamer")) { PrintText(TruncateAt(popust.PadLeft(p), p)); }

            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
        }

        private static void PrintReceiptHeader(string imeTvrtke, string companyName, string addressLine1, string addressLine2, string taxNumber, DateTime dateTime, string cashierName, string broj, string kupac, bool na_sobu = false)
        {
            if (companyName.Length > 0)
            {
                string[] lines = companyName.Split(';');

                foreach (string line in lines)
                {
                    int brojText = line.Trim().Length;
                    int brojOstatak = (RecLineChars - brojText) / 2;
                    string praznaMjesta = "";
                    for (int _br = 0; _br < brojOstatak; _br++) { praznaMjesta += " "; }
                    tekst += praznaMjesta + line.Trim().ToUpper() + "\r\n";
                }
            }
            else
            {
                tekst += imeTvrtke + "\r\n";
                if (addressLine1.Length > 0)
                    PrintTextLine(("Adresa: " + addressLine1).ToUpper());

                if (DT.Rows[0]["vl"].ToString() != "")
                {
                    PrintTextLine(DT.Rows[0]["vl"].ToString().ToUpper());
                }
                if (DT.Rows[0]["poslovnica_adresa"].ToString() != "")
                {
                    PrintTextLine(DT.Rows[0]["poslovnica_adresa"].ToString().ToUpper());
                }
                if (taxNumber != "")
                {
                    PrintTextLine(("Telefon: " + taxNumber).ToUpper());
                }
            }

            PrintTextLine(new string('-', RecLineChars));
            PrintTextLine(("Datum: " + dateTime).ToUpper());

            if (companyName.Length == 0)
                PrintTextLine(("OIB: " + DT.Rows[0]["oib"].ToString()).ToUpper());

            PrintTextLine((String.Format("Blagajnik : {0}", dt_blaganik.Rows[0]["ime"].ToString() + " " + dt_blaganik.Rows[0]["prezime"].ToString())).ToUpper());
            PrintTextLine((String.Format("Otpremnica broj : {0}", broj + "/" + dt_ducan.Rows[0][0].ToString() + "/" + dt_blagajna.Rows[0][0].ToString())).ToUpper());

            if (kupac != "" && kupac != "0")
            {
                if (na_sobu)
                {
                    DataTable dtSoba = classSQL.select(string.Format(@"select naziv_sobe from sobe where id = {0}", kupac), "sobe").Tables[0];
                    PrintTextLine(String.Empty);
                    PrintTextLine(new string('-', RecLineChars));
                    PrintTextLine("SOBA:");
                    PrintTextLine(dtSoba.Rows[0]["naziv_sobe"].ToString());
                    PrintTextLine(new string('-', RecLineChars));
                }
                else
                {
                    DataTable DTkupac = classSQL.select("SELECT partners.ime_tvrtke,partners.adresa,partners.oib,grad.grad,grad.posta FROM partners LEFT JOIN grad ON partners.id_grad=grad.id_grad WHERE id_partner='" + kupac + "'", "partners").Tables[0];
                    PrintTextLine(String.Empty);
                    PrintTextLine(new string('-', RecLineChars));
                    PrintTextLine("KUPAC:");
                    PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                    PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                    PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                    PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
                    PrintTextLine(new string('-', RecLineChars));
                }
            }

            _1 = tekst;
            tekst = "";

            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            //PrintTextLine(new string('-', RecLineChars));

            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
            if (File.Exists("hamer")) { PrintText(TruncateAt("RAB%".PadLeft(p), p)); }
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

        private static void PrintPage(object o, PrintPageEventArgs e)
        {
            float height = 0;
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], Convert.ToInt16(DTpostavke.Rows[0]["font_print_size"].ToString()));

            System.Drawing.Text.PrivateFontCollection privateFonts_ukupno = new PrivateFontCollection();
            privateFonts_ukupno.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font_ukupno = new Font(privateFonts.Families[0], 16);

            //header
            String drawString = _1;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, height, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(_1, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //stavke
            drawString = _2;
            drawFont = font;
            float y = height;
            float x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_2, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Ukupno
            drawString = _3;
            drawFont = font_ukupno;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_3, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Naša zahvala
            drawString = _4;
            drawFont = font_ukupno;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_4, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //Bottom
            drawString = _5;
            drawFont = font;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

            stringSize = e.Graphics.MeasureString(_5.TrimEnd(), drawFont);
            height = float.Parse(stringSize.Height.ToString()) + height;

            pageHeight++;

            if (e.HasMorePages)
            {
                e.HasMorePages = false;
            }

            if (pageHeight > e.PageSettings.PaperSize.Height)
            {
                PaperSize psNew = new System.Drawing.Printing.PaperSize("Racun", e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize.Width, pageHeight);
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
            }
        }

        private static void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;

            string drawString = _1 + _2;

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
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
            else
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = String.Format(
                       "Can't find printer \"{0}\".", printerName);
                    MessageBox.Show(msg, "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

                printDoc.Print();
            }
        }

        private static void openCashDrawer1()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();

            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            IntPtr pUnmanagedBytes = new IntPtr(0);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
        }
    }
}