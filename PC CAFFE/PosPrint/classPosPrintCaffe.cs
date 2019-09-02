using GenCode128;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintCaffe
    {
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
        private static string id_kasa = DTpostavke.Rows[0]["default_blagajna"].ToString();
        private static string id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
        private static string Underline = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'u', (byte)'C' });
        private static string Italic = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'i', (byte)'C' });
        private static string CenterAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        private static string RightAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        private static string DoubleWideCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        private static string DoubleHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'3', (byte)'C' });
        private static string DoubleWideAndHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'4', (byte)'C' });
        private static string Bold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        private static string Normal = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'n', (byte)'C' });
        private static string id_stol;
        private static string _1;
        private static string _2;
        private static string _3;
        private static string _4;
        private static string _5;
        private static string fiskal_tekst;
        private static string kockice;
        private static string napomena;
        private static decimal karticaIznosZaOduzeti = 0;
        private static Image img_barcode = null;
        private static DataTable DTpdv = new DataTable();
        private static DataRow RowPdv;
        private static string BrojRacunaa;
        public static int id_adresa_dostave { get; set; }
        private static string brRacun { get; set; }
        private static double ukupno_rabat = 0;
        private static decimal vraceniIznos = 0;
        private static int pageHeight = 0;
        private static Graphics grRacun = null;
        private static double ukupno = 0;

        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string broj_racuna, string kupac, string barcode, string brRac, string placanje, string storno, decimal kIznosOduzeti, decimal koeficijent = 1, decimal vraceni = 0)
        {
            vraceniIznos = vraceni;
            ukupno = 0;
            brRacun = brRac;
            id_stol = DTstavke.Rows[0]["stol"].ToString();

            karticaIznosZaOduzeti = kIznosOduzeti;
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
            if (Class.PosPrint.sortIspisNaMaliPrinter)
                dv.Sort = "ime asc";

            DTstavke = dv.ToTable();

            DataTable dtPomoc = DTstavke.Clone();
            string br = "", sifra = "", skl = "", mpcs = "", vpc = "", nbc = "", porez = "", kol = "", rab = "", cij = "", ime = "", stol = "", jelo = "", pp = "", stor = "", id_duc = "", id_bla = "", dod = "", pol = "";
            string _br = "", _sifra = "", _skl = "", _mpcs = "", _vpc = "", _nbc = "", _porez = "", _kol = "", _rab = "", _cij = "", _ime = "", _stol = "", _jelo = "", _pp = "", _stor = "", _id_duc = "", _id_bla = "", _dod = "", _pol = "";

            foreach (DataRow drRow in DTstavke.Rows)
            {
                if (!DTstavke.Columns.Contains("kol"))
                {
                    DTstavke.Columns.Add("kol", typeof(decimal));
                }
                drRow["kol"] = drRow["kolicina"];
            }

            foreach (DataRow drRow in DTstavke.Rows)
            {
                br = drRow["broj_racuna"].ToString();
                sifra = drRow["sifra_robe"].ToString();
                skl = drRow["id_skladiste"].ToString();
                mpcs = drRow["mpc"].ToString();
                vpc = drRow["vpc"].ToString();
                nbc = drRow["nbc"].ToString();
                porez = drRow["porez"].ToString();
                rab = drRow["rabat"].ToString();
                cij = drRow["cijena"].ToString();
                ime = drRow["ime"].ToString();
                stol = drRow["stol"].ToString();
                jelo = drRow["jelo"].ToString();
                pp = drRow["porez_potrosnja"].ToString();
                stor = drRow["storno"].ToString();
                id_duc = drRow["id_ducan"].ToString();
                id_bla = drRow["id_blagajna"].ToString();
                dod = drRow["dod"].ToString();
                pol = drRow["pol"].ToString();

                string sPostoji = string.Format((br.Length > 0 ? "broj_racuna = '{1}'{0}" : "(broj_racuna is null or broj_racuna = '{1}'){0}") +
                    (sifra.Length > 0 ? "sifra_robe = '{2}'{0}" : "(sifra_robe is null or sifra_robe = '{2}'){0}") +
                    (skl.Length > 0 ? "id_skladiste = '{3}'{0}" : "(id_skladiste is null or id_skladiste = '{3}'){0}") +
                    (mpcs.Length > 0 ? "mpc = '{4}'{0}" : "(mpc is null or mpc = '{4}'){0}") +
                    (vpc.Length > 0 ? "vpc = '{5}'{0}" : "(vpc is null or vpc = '{5}'){0}") +
                    (nbc.Length > 0 ? "nbc = '{6}'{0}" : "(nbc is null or nbc = '{6}'){0}") +
                    (porez.Length > 0 ? "porez = '{7}'{0}" : "(porez is null or porez = '{7}'){0}") +
                    (rab.Length > 0 ? "rabat = '{8}'{0}" : "(rabat is null or rabat = '{8}'){0}") +
                    (cij.Length > 0 ? "cijena = '{9}'{0}" : "(cijena is null or cijena = '{9}'){0}") +
                    (ime.Length > 0 ? "ime = '{10}'{0}" : "(ime is null or ime = '{10}'){0}") +
                    (stol.Length > 0 ? "stol = '{11}'{0}" : "(stol is null or stol = '{11}'){0}") +
                    (jelo.Length > 0 ? "jelo = '{12}'{0}" : "(jelo is null or jelo = '{12}'){0}") +
                    (pp.Length > 0 ? "porez_potrosnja = '{13}'{0}" : "(porez_potrosnja is null or porez_potrosnja = '{13}'){0}") +
                    (stor.Length > 0 ? "storno = '{14}'{0}" : "(storno is null or storno = '{14}'){0}") +
                    (id_duc.Length > 0 ? "id_ducan = '{15}'{0}" : "(id_ducan is null or id_ducan = '{15}'){0}") +
                    (id_bla.Length > 0 ? "id_blagajna = '{16}'{0}" : "(id_blagajna is null or id_blagajna = '{16}'){0}") +
                    (dod.Length > 0 ? "dod = '{17}'{0}" : "(dod is null or dod = '{17}'){0}") +
                    (pol.Length > 0 ? "pol = '{18}'" : "(pol is null or pol = '{18}')"), " and ", br, sifra, skl, mpcs, vpc, nbc, porez, rab, cij, ime, stol, jelo, pp, stor, id_duc, id_bla, dod, pol);

                int postoji = dtPomoc.Select(sPostoji).Count();

                if ((br != _br || sifra != _sifra || skl != _skl || mpcs != _mpcs || vpc != _vpc || nbc != _nbc || porez != _porez || rab != _rab || cij != _cij || ime != _ime || stol != _stol || jelo != _jelo || pp != _pp || stor != _stor || id_duc != _id_duc || id_bla != _id_bla || dod != _dod || pol != _pol) && postoji == 0)
                {
                    DataRow dRow = dtPomoc.NewRow();
                    object sumObj = null;
                    sumObj = DTstavke.Compute("Sum(kol)", sPostoji);
                    dRow["kolicina"] = sumObj.ToString();
                    dRow["broj_racuna"] = br;
                    dRow["sifra_robe"] = sifra;
                    dRow["id_skladiste"] = skl;
                    dRow["mpc"] = mpcs;
                    dRow["vpc"] = vpc;
                    dRow["nbc"] = nbc;
                    dRow["porez"] = porez;
                    dRow["rabat"] = rab;
                    dRow["cijena"] = cij;
                    dRow["ime"] = ime;
                    dRow["stol"] = stol;
                    dRow["jelo"] = jelo;
                    dRow["porez_potrosnja"] = pp;
                    dRow["storno"] = stor;
                    dRow["id_ducan"] = id_duc;
                    dRow["id_blagajna"] = id_bla;
                    dRow["dod"] = dod;
                    dRow["pol"] = pol;

                    dtPomoc.Rows.Add(dRow);
                }

                _br = br;
                _sifra = sifra;
                _skl = skl;
                _mpcs = mpcs;
                _vpc = vpc;
                _nbc = nbc;
                _porez = porez;
                _rab = rab;
                _cij = cij;
                _ime = ime;
                _stol = stol;
                _jelo = jelo;
                _pp = pp;
                _stor = stor;
                _id_duc = id_duc;
                _id_bla = id_bla;
                _dod = dod;
                _pol = pol;
            }

            DTstavke = dtPomoc;

            dt_blaganik = classSQL.select("SELECT ime,prezime,oib FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

            BrojRacunaa = brRac;
            img_barcode = Code128Rendering.MakeBarcodeImage("000" + brRac, int.Parse("3"), true);

            _storno = storno;

            tekst = "";
            try
            {
                DTrac = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + brRac + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'", "racuni").Tables[0];

                if (DTrac.Rows.Count == 0)
                {
                    MessageBox.Show("Greška sa računom: " + brRac);
                    return;
                }

                napomena = DTrac.Rows[0]["napomena"].ToString();
                kupac = DTrac.Rows[0]["id_kupac"].ToString();

                PrintReceiptHeader(DT.Rows[0]["ime_tvrtke"].ToString(), DT.Rows[0]["skraceno_ime"].ToString(), DT.Rows[0]["adresa"].ToString(), "My State, My Country", DT.Rows[0]["tel"].ToString(), Convert.ToDateTime(DTrac.Rows[0]["datum_racuna"].ToString()), blagajnik, brRac, kupac);

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
                //double ukupno = 0;

                double osnovica_sve = 0;
                double osnovica_pnp = 0;
                double Porez_potrosnja_sve = 0;
                double pdv_sve = 0;
                ukupno_rabat = 0;

                double maxPNP = 0;

                for (int i = 0; i < DTstavke.Rows.Count; i++)
                {
                    int dodi = 0;
                    int.TryParse(DTstavke.Rows[i]["dod"].ToString().Trim(), out dodi);
                    if (dodi != 2)
                    {
                        double.TryParse(DTstavke.Rows[i]["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double kolicina);
                        double.TryParse(DTstavke.Rows[i]["porez_potrosnja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double PP);
                        double.TryParse(DTstavke.Rows[i]["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double PDV);
                        double.TryParse(DTstavke.Rows[i]["vpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double VPC);
                        double.TryParse(DTstavke.Rows[i]["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double mpc);
                        double.TryParse(DTstavke.Rows[i]["rabat"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double rabat);
                        //double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
                        //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString().Replace(',', '.'));
                        //double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
                        //double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
                        //double mpc = Math.Round(((VPC * (PP + PDV) / 100) + VPC), 3);
                        //double mpc = Convert.ToDouble(DTstavke.Rows[i]["mpc"].ToString());
                        if (Class.Postavke.UDSGame && karticaIznosZaOduzeti != 0)
                        {
                            VPC = VPC * (double)Math.Round(koeficijent, 10, MidpointRounding.AwayFromZero);
                        }

                        double MPC = Math.Round(((VPC * (PP + PDV) / 100) + VPC), 3);
                        double ukupnoStavka = mpc * kolicina;

                        rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
                        ukupno_rabat = (ukupnoStavka * rabat / 100) + ukupno_rabat;
                        ukupnoStavka = Math.Round((ukupnoStavka - (ukupnoStavka * rabat / 100)), 3);

                        //Ovaj kod dobiva PDV
                        double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                        pdv_stavka = (ukupnoStavka * PreracunataStopaPDV) / 100;

                        //Ovaj kod dobiva porez na potrošnju
                        double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                        Porez_potrosnja_stavka = (ukupnoStavka * PreracunataStopaPorezNaPotrosnju) / 100;

                        if (PP > 0)
                        {
                            osnovica_pnp += ukupnoStavka / (1 + (PP + PDV) / 100);
                        }

                        if (maxPNP < PP)
                            maxPNP = PP;

                        // Stavke tekst
                        string nazivStavka = DTstavke.Rows[i]["ime"].ToString();

                        PrintText((nazivStavka.Length > 25 ? nazivStavka.Substring(0, 25).TrimEnd() + "." : nazivStavka) + "\r\n");
                        PrintLineItem("", kolicina, mpc, rabat + "%", (mpc * kolicina) * (1 - (rabat / 100)));

                        //izračun porez potrosnja
                        Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                        //izračun osnovica
                        if (rabat > 0)
                        {
                            double pps = (100 * (PDV + PP)) / (100 + PDV + PP);
                            osnovica = ukupnoStavka - (ukupnoStavka * pps / 100);
                        }
                        else
                        {
                            osnovica = VPC * kolicina;
                        }

                        osnovica_sve = (osnovica) + osnovica_sve;

                        StopePDVa(PDV, pdv_stavka, (osnovica));

                        //Izracun pdv
                        pdv_sve = pdv_sve + (pdv_stavka);
                        ukupno = ukupnoStavka + ukupno;
                    }
                }

                porez_na_potrosnju[0] = maxPNP.ToString().Replace(".", ",");
                porez_na_potrosnju[1] = Convert.ToString(osnovica_pnp);
                porez_na_potrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

                string[] fiskalizacija = new string[3];

                //if (kIznosOduzeti > 0)
                //{
                //    ukupno = ukupno - (double)kIznosOduzeti;
                //}

                if (DTfis.Rows[0]["aktivna"].ToString() == "1")
                {
                    bool pdv = false;
                    if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
                    {
                        pdv = true;
                    }

                    string naplatni_uredaj_po_nazivu = dt_blagajna.Rows.Count > 0 ? dt_blagajna.Rows[0][0].ToString() : DTpostavke.Rows[0]["default_blagajna"].ToString();

                    fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                         DT.Rows[0]["oib"].ToString(),
                         dt_blaganik.Rows[0]["oib"].ToString(),
                         DTstavke, Convert.ToDateTime(DTrac.Rows[0]["datum_racuna"].ToString()),
                         pdv,
                         Convert.ToInt32(brRac),
                         DTpostavke.Rows[0]["default_ducan"].ToString(),
                         Convert.ToInt32(naplatni_uredaj_po_nazivu),
                         DTpdv,
                         porez_na_potrosnju,
                         DTOstaliPor,
                         iznososlobpdv,
                         iznos_marza,
                         DTnaknade,
                         Convert.ToDecimal(ukupno),
                         //(Convert.ToDecimal(ukupno) - kIznosOduzeti),
                         placanje,
                         false,
                         osnovica_sve
                         );
                }
                PrintReceiptFooter(osnovica_sve, pdv_sve, Porez_potrosnja_sve, ukupno, barcode, fiskalizacija, Porez_potrosnja_sve);
            }
            finally
            {
                int maxPrint = 1;
                if (placanje.ToString().ToUpper() == "G")
                {
                    maxPrint = Convert.ToInt32(DTsetting.Rows[0]["ispisGotovina"]);
                }
                else if (placanje.ToString().ToUpper() == "K")
                {
                    maxPrint = Convert.ToInt32(DTsetting.Rows[0]["ispisKartica"]);
                }
                else if (placanje.ToString().ToUpper() == "T")
                {
                    maxPrint = Convert.ToInt32(DTsetting.Rows[0]["ispisVirman"]);
                }
                else if (placanje.ToString().ToUpper() == "O")
                {
                    maxPrint = Convert.ToInt32(DTsetting.Rows[0]["ispisOstalo"]);
                }
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

        private static void PrintReceiptFooter(double subTotal, double tax, double discount, double ukupno, string barcode, string[] fiskalizacija, double PNP)
        {
            string offSetString = new string(' ', 0);
            PrintTextLine(new string('=', RecLineChars));
            if (Class.PosPrint.ispisUkupnoIspodStavkiMaliPrinter && DTpostavke.Rows[0]["direct_print"].ToString() == "0")
            {
                PrintTextLine("{[5AR]}");
                PrintTextLine(new string('=', RecLineChars));
            }
            try
            {
                if (ukupno_rabat > 0)
                {
                    PrintTextLine("UKUPNO POPUST: " + Math.Round(ukupno_rabat, 3).ToString("#0.00") + " kn");
                }

                decimal popust_cijeli_racun = 0;
                decimal.TryParse(DTrac.Rows[0]["popust_cijeli_racun"].ToString(), out popust_cijeli_racun);
                if (popust_cijeli_racun > 0)
                {
                    decimal popust_iznos = 0;
                    popust_iznos = ((decimal)ukupno * (1 + (((100 * popust_cijeli_racun) / (100 - popust_cijeli_racun)) / 100))) - (decimal)ukupno;
                    PrintTextLine("UKUPNO POPUSTA NA RAČUN: " + popust_cijeli_racun + "%");
                    PrintTextLine("POPUST U KN: " + Math.Round(popust_iznos, 3).ToString("#0.00") + " kn");
                }

                if (karticaIznosZaOduzeti > 0)
                {
                    PrintTextLine("UKUPNO POPUSTA NA RAČUN U KN: " + karticaIznosZaOduzeti + " kn");
                }

                if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
                {
                    for (int i = 0; i < DTpdv.Rows.Count; i++)
                    {
                        PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00") + " OSNOVICA: " + Convert.ToDecimal(DTpdv.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                    }
                }
                else
                {
                    PrintTextLine("PDV nije uračunat u cijenu prema");
                    if (Class.PodaciTvrtka.oibTvrtke == "36486943127")
                        tekst += Environment.NewLine;

                    PrintTextLine("čl.90. st.2. zakona o PDV-u.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            PrintTextLine(offSetString + string.Format("PNP:       {0}", PNP.ToString("#0.00")));
            try
            {
                if (Convert.ToDouble(DTrac.Rows[0]["dobiveno_gotovina"].ToString()) > 0)
                {
                    tekst += "DOBIVENO NOVČANICE: " + Convert.ToDouble(DTrac.Rows[0]["dobiveno_gotovina"].ToString()).ToString("#0.00") + " kn\n";
                }
                else if (DTrac.Rows[0]["nacin_placanja"].ToString() == "K")
                {
                    tekst += "PLAĆENO KARTICOM.\r\n";
                }
                else if (DTrac.Rows[0]["nacin_placanja"].ToString() == "O")
                {
                    tekst += "OSTALI NAČINI PLAČANJA" + (Class.PodaciTvrtka.oibTvrtke == "98816793336" ? " - FOODEX" : "") + ".\r\n";
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

                    tekst += classOstalo.SvavkaZaPrinter(DT.Rows[0]["napomenaTransa"].ToString(), RecLineChars) + "\r\n";
                }
                if (vraceniIznos > 0)
                    tekst += $"IZNOS ZA VRATITI: {vraceniIznos} kn\r\n";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            PrintTextLine(offSetString + new string('-', RecLineChars));

            _2 = tekst;
            tekst = "";

            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
            {
                fiskal_tekst = "";
                kockice = "";

                fiskal_tekst = "JIR:" + fiskalizacija[0] + "\r\n" + "ZKI:" + fiskalizacija[1] + "\r\n";
                classSQL.update("UPDATE racuni SET jir='" + fiskalizacija[0] + "',zik='" + fiskalizacija[1] + "' WHERE broj_racuna='" + BrojRacunaa + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'");

                for (int j = 0; j < RecLineChars; j++)
                {
                    kockice += "=";
                }
                kockice += "\r\n";
            }

            PrintTextLine(offSetString + string.Format("UKUPNO: {0} KN", ukupno.ToString("#0.00")));
            _3 = tekst;
            tekst = "";

            //Embed 'center' alignment tag on front of string below to have it printed in the center of the receipt.
            //PrintTextLine(printer, System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' }) + footerText);
            _4 = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["aktivnost"].ToString() == "DA" && barcode != "")
                {
                    tekst += offSetString + new string('-', (RecLineChars)) + Environment.NewLine;
                    tekst += "Naša zahvala za Vašu kupovinu." + Environment.NewLine;

                    double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

                    tekst += UKpopust.ToString("#0.00") + " kn popusta." + Environment.NewLine + Environment.NewLine;
                    _4 = tekst;
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

            if (napomena != "")
            {
                tekst += new string('-', (RecLineChars)) + Environment.NewLine;

                nap = "";
                tekst += PrintTextRecursiveString(napomena + Environment.NewLine);

                _4 += tekst;

                tekst = "";
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

            //tu ide adresa
            if (id_adresa_dostave != null && id_adresa_dostave > 0)
            {
                if (DTsetting.Rows[0]["adresa_narudzbe_racun_kraj"].ToString() == "1")
                {
                    DataSet adresa_narudzbe = classSQL.select("select * from adresa_dostave where id = '" + id_adresa_dostave + "' limit 1;", "adresa_narudzbe");
                    if (adresa_narudzbe != null && adresa_narudzbe.Tables.Count > 0 && adresa_narudzbe.Tables[0] != null && adresa_narudzbe.Tables[0].Rows.Count > 0)
                    {
                        tekst += Environment.NewLine;
                        tekst += "PODACI ZA DOSTAVU:" + Environment.NewLine;
                        tekst += "Mjesto: " + adresa_narudzbe.Tables[0].Rows[0]["mjesto"] + Environment.NewLine;
                        tekst += "Ulica: " + adresa_narudzbe.Tables[0].Rows[0]["ulica"] + Environment.NewLine;
                        tekst += "Kucni broj: " + adresa_narudzbe.Tables[0].Rows[0]["kbr"] + Environment.NewLine;
                        tekst += "Telefon: " + adresa_narudzbe.Tables[0].Rows[0]["telefon"] + Environment.NewLine;
                    }
                }
            }

            id_adresa_dostave = 0;
            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            // Code-iT verzija programa bottom text
            string codeIt = $"Code-iT verzija programa: {Properties.Settings.Default.verzija_programa.ToString()}";
            tekst += Environment.NewLine;
            PrintTextLine(new string('-', RecLineChars));
            string center = "";
            for (int i = 0; i < (RecLineChars - codeIt.Length) / 2; i++)
                center += " ";
            tekst += center + codeIt;

            _5 = tekst;
            tekst = "";
        }

        public static string nap { get; set; }

        private static string PrintTextRecursiveString(string text)
        {
            int duzina = RecLineChars - (DTpostavke.Rows[0]["direct_print"].ToString() == "1" ? 0 : 13);
            //duzina = RecLineChars;
            if (text.Length <= duzina)
            {
                nap += text;
                return nap;
            }
            else
            {
                string pomocni = TruncateAt(text, duzina);
                int lastIndexOfSpace = pomocni.LastIndexOf(' ');

                nap += TruncateAt(pomocni, lastIndexOfSpace) + "\r\n";
                text = text.Substring(lastIndexOfSpace + 1);
                return PrintTextRecursiveString(text);
            }
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
                PrintText(TruncateAt(artikl.PadRight(a), a).ToUpper());
            }

            PrintText(TruncateAt(artikl.PadLeft(a), a));
            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
            PrintText(TruncateAt(popust.PadLeft(p), p));

            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
        }

        private static void PrintReceiptHeader(string imeTvrtke, string companyName, string addressLine1, string addressLine2, string taxNumber, DateTime dateTime, string cashierName, string broj, string kupac)
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

            PrintTextLine((string.Format("Blagajnik : {0}", dt_blaganik.Rows[0]["ime"].ToString() + " " + dt_blaganik.Rows[0]["prezime"].ToString())).ToUpper());
            PrintTextLine((string.Format("Račun broj : {0}".ToUpper(), broj + "/" + dt_ducan.Rows[0][0].ToString() + "/" + dt_blagajna.Rows[0][0].ToString())));

            if (kupac != "" && kupac != "0")
            {
                DataTable DTkupac = classSQL.select("SELECT partners.ime_tvrtke,partners.adresa,partners.oib,grad.grad,grad.posta FROM partners LEFT JOIN grad ON partners.id_grad=grad.id_grad WHERE id_partner='" + kupac + "'", "partners").Tables[0];
                PrintTextLine(string.Empty);
                PrintTextLine(new string('=', RecLineChars));
                PrintTextLine("Račun " + DT.Rows[0]["r1"].ToString());
                PrintTextLine("KUPAC:");
                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
                PrintTextLine(new string('=', RecLineChars));
            }

            _1 = tekst;
            tekst = "";

            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            PrintTextLine(new string('-', RecLineChars));

            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
            PrintText(TruncateAt("POPUST".PadLeft(p), p));
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
            System.Drawing.Font font_ukupno = new Font(privateFonts.Families[0], Class.PosPrint.fontUkupnoSizeMailiPrinter);

            try
            {
                if (File.Exists("C://logo/logo.jpg"))
                {
                    Image ik = Image.FromFile("C://logo/logo.jpg");
                    bool bigerWidth = false;
                    float rezol = 0;
                    if (ik.Size.Width > ik.Size.Height)
                    {
                        bigerWidth = true;
                        rezol = ik.Size.Width / ik.Size.Height;
                    }
                    else
                    {
                        rezol = ik.Size.Height / ik.Size.Width;
                    }

                    int newWidth = ik.Size.Width, newHeight = ik.Size.Height;

                    if ((bigerWidth ? ik.Size.Width : ik.Size.Height) > e.PageSettings.PrintableArea.Size.Width)
                    {
                        if (bigerWidth)
                        {
                            newWidth = (int)e.PageSettings.PrintableArea.Size.Width;
                            newHeight = (ik.Size.Height * newWidth) / ik.Size.Width;
                        }
                        else
                        {
                            newHeight = (int)e.PageSettings.PrintableArea.Size.Width;
                            newWidth = (newHeight * ik.Size.Width) / ik.Size.Height;
                        }
                    }

                    Point pp = new Point(0, 0);
                    e.Graphics.DrawImage(ik, (e.PageSettings.PrintableArea.Size.Width - newWidth) / 2, height, newWidth, newHeight);
                    pageHeight = (int)(height + newHeight);

                    height = newHeight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //header
            string drawString = _1;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, height, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(_1, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //stavke

            float y = 0, x = 0;
            if (_2.Contains("{[5AR]}") && Class.PosPrint.ispisUkupnoIspodStavkiMaliPrinter)
            {
                string[] _2s = _2.Split(new string[] { "{[5AR]}" + Environment.NewLine }, StringSplitOptions.None);

                drawString = _2s[0];
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_2s[0], drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                drawString = _3.ToUpper();
                drawFont = font_ukupno;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_3, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                drawString = _2s[1];
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_2s[1], drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;
            }
            else
            {
                drawString = _2;
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_2, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;
            }

            //fiskal
            drawString = fiskal_tekst;
            drawFont = new System.Drawing.Font("Arial", 9F);
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(fiskal_tekst, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //kockice
            drawString = kockice;
            drawFont = font;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(kockice, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            if (!_2.Contains("{[5AR]}") && !Class.PosPrint.ispisUkupnoIspodStavkiMaliPrinter)
            {
                //Ukupno
                drawString = _3;
                drawFont = font_ukupno;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_3, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;
            }

            //Naša zahvala
            drawString = _4;
            drawFont = font_ukupno;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_4, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["aktivnost"].ToString() == "DA")
                {
                    //Barcode
                    if (img_barcode != null)
                    {
                        System.Drawing.Image img = img_barcode;
                        e.Graphics.DrawImage(img_barcode, 0, height, 250, 50);
                    }
                    height = height + 70;
                }
            }

            //Bottom
            drawString = _5;
            drawFont = font;
            y = height;
            x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

            stringSize = e.Graphics.MeasureString(_5.TrimEnd(), drawFont);
            height = float.Parse(stringSize.Height.ToString()) + height;

            try
            {
                if (File.Exists("C://logo/logo2.jpg"))
                {
                    Image ik = Image.FromFile("C://logo/logo2.jpg");
                    bool bigerWidth = false;
                    float rezol = 0;
                    if (ik.Size.Width > ik.Size.Height)
                    {
                        bigerWidth = true;
                        rezol = ik.Size.Width / ik.Size.Height;
                    }
                    else
                    {
                        rezol = ik.Size.Height / ik.Size.Width;
                    }

                    int newWidth = ik.Size.Width, newHeight = ik.Size.Height;

                    if ((bigerWidth ? ik.Size.Width : ik.Size.Height) > e.PageSettings.PrintableArea.Size.Width)
                    {
                        if (bigerWidth)
                        {
                            newWidth = (int)e.PageSettings.PrintableArea.Size.Width;
                            newHeight = (ik.Size.Height * newWidth) / ik.Size.Width;
                        }
                        else
                        {
                            newHeight = (int)e.PageSettings.PrintableArea.Size.Width;
                            newWidth = (newHeight * ik.Size.Width) / ik.Size.Height;
                        }
                    }

                    Point pp = new Point(0, 0);
                    e.Graphics.DrawImage(ik, (e.PageSettings.PrintableArea.Size.Width - newWidth) / 2, height, newWidth, newHeight);
                    pageHeight = (int)(height + newHeight);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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
                //e.Graphics.Dispose();
                //e.Graphics.
            }
        }

        private static void printaj()
        {
            napomena = "";
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;

            string drawString = _1 + _2;

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                if (DTpostavke.Rows[0]["ladicaOn"].ToString() == "1")
                {
                    openCashDrawer1();
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
            else
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = string.Format(
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