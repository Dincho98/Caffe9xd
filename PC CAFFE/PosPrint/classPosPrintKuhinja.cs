using PCPOS.Caffe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintKuhinja
    {
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTrac;
        private static DataTable dt = classSQL.select("SELECT traje_do, popust, aktivnost FROM promocija1", "promocija1").Tables[0];
        public static string broj_narudzbe { get; set; }

        private static bool ima_stavke_za_kuhinju = false;

        private static string tekst;

        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString()) + 2;

        private static string _1;
        private static string _2;
        private static string _3;
        private static bool artiklSeNalaziUOznacenojGrupi;

        private static string zadnji = "";
        //private static Image img_barcode = null;

        //Printer Pizzerija
        public static List<string> listaOznacenihGrupa;


        public static void NapuniListuOznacenimGrupama()
        {
            listaOznacenihGrupa = new List<string>();
            string sqlQuery = "SELECT id_grupa FROM grupa WHERE printer3=1";
            DataTable DTOznaceneGrupe = classSQL.select(sqlQuery, "grupe").Tables[0];
            foreach (DataRow row in DTOznaceneGrupe.Rows)
            {
                listaOznacenihGrupa.Add(row[0].ToString());
            }
            zadnji = "";
        }

        public static bool ArtiklIzOznaceneGrupePostojan = false;
        //Provjeravamo ako postoji artikl koji spada u označenu grupu u POS Postavkama
        public static bool PrintOnPrinter4(DataTable DTRac)
        {
            bool artiklPostojan = false;
            for (int i = 0; !artiklPostojan && i < DTrac.Rows.Count; i++)
            {
                //IDovi grupa artikala
                for (int j = 0; j < listaOznacenihGrupa.Count; j++)
                {
                    string sql = $@"SELECT naziv FROM roba WHERE sifra='{DTrac.Rows[i]["sifra_robe"].ToString()}' 
                                    AND id_grupa={listaOznacenihGrupa[j]}";
                    DataTable DTx = classSQL.select(sql, "roba").Tables[0];
                    if (DTx.Rows.Count > 0)
                        artiklPostojan = true;
                }
            }
            return artiklPostojan;
        }

        public static string PronadjiIDGrupePremaArtiklovomNazivu(string ime)
        {
            string id;
            string sqlQuery = $"SELECT id_grupa FROM roba WHERE naziv='{ime}'";
            DataTable DTIme = classSQL.select(sqlQuery, "roba").Tables[0];
            if (DTIme.Rows.Count > 0)
            {
                return DTIme.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        


        //Friendly advice: If it works, don't touch it.
        //And of course, take MUCH anti stress pills. =)
        public static void PrintOnPrinter2(DataTable DTartikli, bool koristiPrinter3 = false, bool pijaca_i_trgovacka = false, bool koristiPrinter4 = false, bool printNarudzbeNaF4 = false)
        {
            ima_stavke_za_kuhinju = false;
            DataTable DTPosPrint = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            tekst = "";
            string napomenaKolicina = "   ► ";
            try
            {
                if (DTsetting.Rows[0]["windows_printer_name2"].ToString() != "Nije instaliran" || DTsetting.Rows[0]["windows_printer_name3"].ToString() != "Nije instaliran")
                {
                    DTrac = DTartikli.Clone();
                    bool artiklZaPrint = false;
                    for (int i = 0; i < DTartikli.Rows.Count; i++)
                    {
                        string sql = string.Format(@"select
case when coalesce(grupa.printer3, 0) = 0 then false else true end as printer3, roba.id_podgrupa
from roba
left join grupa on roba.id_grupa = grupa.id_grupa where roba.sifra = '{0}';", DTartikli.Rows[i]["sifra_robe"].ToString());

                        DataSet dsPom = classSQL.select(sql, "pom");

                        bool printer3 = koristiPrinter3, dod = false;
                        int id_podgrupa = 2, pol = -1;

                        if (dsPom != null && dsPom.Tables.Count > 0 && dsPom.Tables[0] != null && dsPom.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                bool.TryParse(dsPom.Tables[0].Rows[0]["printer3"].ToString(), out printer3);
                                int.TryParse(dsPom.Tables[0].Rows[0]["id_podgrupa"].ToString(), out id_podgrupa);
                            }
                            catch
                            {
                            }
                        }

                        int dodatak = 0;
                        int.TryParse(DTartikli.Rows[i]["dod"].ToString(), out dodatak);
                        int.TryParse(DTartikli.Rows[i]["id_podgrupa"].ToString(), out id_podgrupa);

                        if (dodatak > 0)
                            dod = true;

                        int.TryParse(DTartikli.Rows[i]["pol"].ToString(), out pol);

                        ///
                        // Ovaj dio koda koristi se za to da se na ONO što je označeno na grupama u Postavke POS Opreme printa SAMO
                        // na 4. printeru. To znači da ti artikli ne smiju biti na printeru 2 ili 3!
                        ///
                        artiklSeNalaziUOznacenojGrupi = false;
                        if (!koristiPrinter4)
                        {
                            NapuniListuOznacenimGrupama();
                            string idGrupe = PronadjiIDGrupePremaArtiklovomNazivu(DTartikli.Rows[i][10].ToString());
                            if (frmStoloviZaNaplatuCustom.poslanoSaStola || frmCaffe.posaljiNarudzbeNaStol)
                                idGrupe = PronadjiIDGrupePremaArtiklovomNazivu(DTartikli.Rows[i][11].ToString());
                            foreach (var grupa in listaOznacenihGrupa)
                            {
                                if (idGrupe == grupa)
                                {
                                    artiklSeNalaziUOznacenojGrupi = true;
                                    ArtiklIzOznaceneGrupePostojan = true;
                                    break;
                                }
                            }
                        }


                        if ((pijaca_i_trgovacka ? id_podgrupa != 2 : id_podgrupa == 2) && !artiklSeNalaziUOznacenojGrupi)
                        {
                            if (dod && artiklZaPrint)
                            {
                                DTrac.ImportRow(DTartikli.Rows[i]);
                            }
                            else if (dod == false && (pijaca_i_trgovacka ? true : printer3 == koristiPrinter3))
                            {
                                artiklZaPrint = true;
                                DTrac.ImportRow(DTartikli.Rows[i]);
                            }
                            else
                            {
                                artiklZaPrint = false;
                            }
                        }
                        else
                        {
                            artiklZaPrint = false;
                        }
                    }
                }
                else
                {
                    DTrac = DTartikli;
                }

                if (koristiPrinter4 || printNarudzbeNaF4 || frmStoloviZaNaplatuCustom.testVariable)
                {
                    DTrac = DTartikli;
                }
                else
                {
                    if (DTrac == null || DTrac.Rows.Count == 0)
                    {
                        return;
                    }
                }


                DataTable DTt = classSQL.select("SELECT ime+' '+prezime AS zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

                string title = "Kuhinja";
                //Ako mi nije nadjena drugiPrint u bazi (nije nadogradjeno, tada nemoj ni gledat ovo
                if (DTPosPrint.Columns["drugiPrint"] != null)
                {
                    title = DTPosPrint.Rows[0]["drugiPrint"].ToString();
                }

                if (koristiPrinter4)
                {
                    title = "Pizzerija";
                    //Ako mi nije nadjena treciPrint u bazi (nije nadogradjeno, tada nemoj ni gledat ovo
                    if (DTPosPrint.Columns["treciPrint"] != null)
                    {
                        title = DTPosPrint.Rows[0]["treciPrint"].ToString();
                    }
                }
                else
                {
                    if (pijaca_i_trgovacka)
                    {
                        title = "Šank";


                        //Ako mi nije nadjena prvi u bazi (nije nadogradjeno, tada nemoj ni gledat ovo
                        if (DTPosPrint.Columns["prviPrint"] != null)
                        {
                            title = DTPosPrint.Rows[0]["prviPrint"].ToString();
                        }
                    }

                }

                PrintReceiptHeader(DateTime.Now, DTt.Rows[0]["zaposlenik"].ToString(), DTrac.Rows[0]["stol"].ToString(), title);
                if (!ArtiklIzOznaceneGrupePostojan)
                {
                    NapuniListuOznacenimGrupama();
                    ArtiklIzOznaceneGrupePostojan = PrintOnPrinter4(DTrac);
                }
                string jelo = "";
                int polindex = -1;
                for (int i = 0; i < DTrac.Rows.Count; i++)
                {
                    string sql = $@"SELECT naziv, mpc FROM roba WHERE sifra='{DTrac.Rows[i]["sifra_robe"].ToString()}' 
                                    AND {(pijaca_i_trgovacka ? "id_podgrupa!='2'" : "id_podgrupa='2'")}";
                    bool artiklFound = true;
                    if (koristiPrinter4)
                    {
                        //Potrebno je provjeriti ako artikl koji je na računu ima 
                        string idGrupe = PronadjiIDGrupePremaArtiklovomNazivu(DTrac.Rows[i][10].ToString());
                        if (frmStoloviZaNaplatuCustom.poslanoSaStola)
                            idGrupe = PronadjiIDGrupePremaArtiklovomNazivu(DTrac.Rows[i][11].ToString());
                        foreach (var idGrupeIzListe in listaOznacenihGrupa)
                        {
                            artiklFound = true;
                            if (idGrupe == idGrupeIzListe)
                            {
                                sql = $@"SELECT naziv, mpc FROM roba WHERE sifra='{DTrac.Rows[i]["sifra_robe"].ToString()}' AND id_grupa={idGrupe} ";
                                break;
                            }
                            artiklFound = false;
                        }
                    }


                    if ((koristiPrinter4 && artiklFound) || !koristiPrinter4)
                    {
                        DataTable DT = classSQL.select(sql, "roba").Tables[0];
                        if (DT.Rows.Count > 0 && Convert.ToInt32(DTrac.Rows[i]["dod"]) < 2)
                        {
                            if (DTrac.Rows[i]["jelo"].ToString() != jelo)
                            {
                                jelo = DTrac.Rows[i]["jelo"].ToString();
                                PrintTextLine("\r\n" + DTrac.Rows[i]["jelo"].ToString().ToUpper() + ":", false);
                            }
                            ima_stavke_za_kuhinju = true;
                            string artikl = DT.Rows[0]["naziv"].ToString();

                            int polaPolaIndex1 = -1, polaPolaIndex2 = -1;
                            int.TryParse(DTrac.Rows[i]["pol"].ToString(), out polaPolaIndex1);
                            if (i + 1 < DTrac.Rows.Count)
                            {
                                int.TryParse(DTrac.Rows[i + 1]["pol"].ToString(), out polaPolaIndex2);
                            }
                            if (((i + 1) <= (DTrac.Rows.Count - 1) && DTrac.Rows[i]["dod"].ToString() == "0" && DTrac.Rows[i + 1]["dod"].ToString() == "0") && (polaPolaIndex1 != polaPolaIndex2 || polaPolaIndex1 == -1 || DTrac.Rows[i]["pol"].ToString() == ""))
                            {
                                PrintLineItem(DTrac.Rows[i]["kolicina"].ToString(), artikl, true);
                            }
                            else
                            {
                                string kolicina = DTrac.Rows[i]["kolicina"].ToString();
                                if (DTrac.Rows[i]["dod"].ToString() == "1") //DODATAK
                                {
                                    if (kolicina.Length == 1)
                                    {
                                        kolicina += "  + ";
                                    }
                                    else if (kolicina.Length == 2)
                                    {
                                        kolicina += " + ";
                                    }
                                    else
                                    {
                                        kolicina += "+ ";
                                    }
                                }
                                else if (DTrac.Rows[i]["dod"].ToString() == "2") //NAPOMENA
                                {
                                    kolicina = napomenaKolicina;
                                }

                                if ((i + 1) <= (DTrac.Rows.Count - 1) && (Convert.ToInt32(DTrac.Rows[i + 1]["dod"].ToString()) > 0 || (Int32.TryParse(DTrac.Rows[i + 1]["pol"].ToString(), out polaPolaIndex1) && polaPolaIndex1 > -1)))
                                {
                                    //PrintLineItem(kolicina, DTrac.Rows[i]["sifra_robe"].ToString() + " " + artikl, false);
                                    PrintLineItem(kolicina, artikl, false);
                                }
                                else
                                {
                                    //PrintLineItem(kolicina, DTrac.Rows[i]["sifra_robe"].ToString() + " " + artikl, true);
                                    int a = -1, b = -1;
                                    if (polindex >= 0 && i + 1 < DTrac.Rows.Count && Int32.TryParse(DTrac.Rows[i + 1]["pol"].ToString(), out a) && Int32.TryParse(DTrac.Rows[polindex]["pol"].ToString(), out b) && a == b && a > -1)
                                    {
                                        PrintLineItem(kolicina, artikl, false);
                                    }
                                    else
                                    {
                                        PrintLineItem(kolicina, artikl, true);
                                    }
                                }
                            }
                            int polpol = -1;
                            if (int.TryParse(DTrac.Rows[i]["pol"].ToString(), out polpol))
                            {
                                polindex = i;
                            }
                        }
                        else if (Convert.ToInt32(DTrac.Rows[i]["dod"]) == 2)
                        {
                            //string kolicina = "   o ";
                            if (i + 1 < DTrac.Rows.Count && Convert.ToInt32(DTrac.Rows[i + 1]["dod"]) == 0)
                            {
                                PrintLineItem(napomenaKolicina, DTrac.Rows[i]["ime"].ToString(), true);
                            }
                            else
                            {
                                PrintLineItem(napomenaKolicina, DTrac.Rows[i]["ime"].ToString(), false);
                            }
                        }
                    }
                }

                PrintTextLine(new string('=', RecLineChars), false);

                /* for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
                 {
                     tekst += Environment.NewLine;
                 }*/

                _2 = tekst;


            }
            finally
            {

                //Bolje ne dirati ovaj kod u finally jer sad radi treba, i to na svim kasama i kompjutorima. Ako se proba napraviti funkcija
                //ili nest izmijeniti, moguce je da na drugim kasama ili kompjuterima ne bude radilo!!!!!!
                if (DTpostavke.Rows[0]["print_narudzbe_bez_pitanja"].ToString() == "0")
                {
                    if (ima_stavke_za_kuhinju == true)
                    {
                        if (DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "1")
                        {
                            if (pijaca_i_trgovacka)
                                printaj(4);
                            else
                            {
                                if (koristiPrinter3)
                                    printaj(3);
                                else
                                {
                                    if (koristiPrinter4)
                                        printaj(10);
                                    else
                                        printaj(2);
                                }
                            }
                        }
                        else
                        {

                            //Bolje ne dirati ovaj kod u finally jer sad radi treba, i to na svim kasama i kompjutorima. Ako se proba napraviti funkcija
                            //ili nest izmijeniti, moguce je da na drugim kasama ili kompjuterima ne bude radilo!!!!!!
                            string msg = $"Želite li poslati narudžbu u {DTPosPrint.Rows[0]["drugiPrint"].ToString()}?";
                            string ttl = DTPosPrint.Rows[0]["drugiPrint"].ToString();
                            if (pijaca_i_trgovacka)
                            {
                                msg = $"Želite li poslati narudžbu na {DTPosPrint.Rows[0]["prviPrint"].ToString()}?";
                                ttl = DTPosPrint.Rows[0]["prviPrint"].ToString();
                            }

                            if (koristiPrinter4 && listaOznacenihGrupa.Count > 0)
                            {
                                msg = $"Zelite li poslati narudzbu u  {DTPosPrint.Rows[0]["treciPrint"].ToString()}?";
                                ttl = DTPosPrint.Rows[0]["treciPrint"].ToString();
                            }

                            if (zadnji != msg) // !!
                            {
                                if (MessageBox.Show(msg, ttl, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    if (pijaca_i_trgovacka)
                                        printaj(4);
                                    else
                                    {
                                        if (koristiPrinter3)
                                            printaj(3);
                                        else
                                        {
                                            if (koristiPrinter4)
                                                printaj(10);
                                            else
                                                printaj(2);
                                        }
                                    }
                                }
                            }

                            zadnji = msg; // !
                        }
                    }
                }
                else
                {
                    //Bolje ne dirati ovaj kod u finally jer sad radi treba, i to na svim kasama i kompjutorima. Ako se proba napraviti funkcija
                    //ili nest izmijeniti, moguce je da na drugim kasama ili kompjuterima ne bude radilo!!!!!!
                    if (ima_stavke_za_kuhinju == true)
                    {
                        string msg = $"Želite li poslati narudžbu u {DTPosPrint.Rows[0]["drugiPrint"].ToString()}?";
                        string ttl = DTPosPrint.Rows[0]["drugiPrint"].ToString();
                        if (pijaca_i_trgovacka)
                        {
                            msg = $"Želite li poslati narudžbu na {DTPosPrint.Rows[0]["prviPrint"].ToString()}?";
                            ttl = DTPosPrint.Rows[0]["prviPrint"].ToString();
                        }

                        if (koristiPrinter4 && listaOznacenihGrupa.Count > 0)
                        {
                            msg = $"Zelite li poslati narudzbu u  {DTPosPrint.Rows[0]["treciPrint"].ToString()}?";
                            ttl = DTPosPrint.Rows[0]["treciPrint"].ToString();
                        }

                        if (zadnji != msg) // !!
                        {
                            //if (MessageBox.Show(msg, ttl, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            // {
                            if (pijaca_i_trgovacka)
                                printaj(4);
                            else
                            {
                                if (koristiPrinter3)
                                    printaj(3);
                                else
                                {
                                    if (koristiPrinter4)
                                        printaj(10);
                                    else
                                        printaj(2);
                                }
                                //  }
                            }
                        }
                        zadnji = msg; // !
                    }
                }
            }
        }

        public static void PrintOnPrinter3(DataTable DTartikli)
        {
            PrintOnPrinter2(DTartikli, true);
        }


        public static void PrintOnPrinter1(DataTable DTartikli)
        {
            PrintOnPrinter2(DTartikli, true, true);
        }

        public static void PrintOnPrinter10(DataTable DTartikli) // Pošto su sjebani svi brojevi printera do sad, uzimam broj 10 => to je printer
                                                                 // koji printa određene grupe.
        {
            PrintOnPrinter2(DTartikli, false, false, true);
        }

        private static void PrintLineItem(string kolicina, string artikl, bool b)
        {
            int a = 24;
            int k = 5;

            PrintText(TruncateAt(kolicina.PadRight(k), k, false), false);
            try
            {
                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                {
                    PrintText(TruncateAt(artikl.PadRight(a), a, true), true);
                }
                else
                {
                    tekst += classOstalo.SvavkaZaPrinter(artikl, a);
                }
            }
            catch
            {
                PrintText(TruncateAt(artikl.PadRight(a), a, true), true);
            }

            PrintText("\r\n", false);
            if (b)
                PrintTextLine(new string('=', RecLineChars), false);
        }

        private static void PrintLineItem(string artikl, string jmj, string kolicina)
        {
            int a = 24;
            int j = 8;
            int k = 5;

            try
            {
                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                {
                    PrintText(TruncateAt(artikl.PadRight(a), a, true), true);
                }
                else
                {
                    tekst += classOstalo.SvavkaZaPrinter(artikl, a);
                }
            }
            catch
            {
                PrintText(TruncateAt(artikl.PadRight(a), a, true), true);
            }

            PrintText(TruncateAt(jmj.PadLeft(j), j, false), false);
            PrintText(TruncateAt(kolicina.PadLeft(k), k, false), false);
            PrintText("\r\n", false);
        }

        private static void PrintReceiptHeader(DateTime dateTime, string cashierName, string stol, string title = null)
        {
            DataTable DT = classSQL.select("SELECT naziv FROM stolovi WHERE id_stol='" + stol + "'", "stolovi").Tables[0];

            if (title != null)
            {
                PrintTextLine(title, false);
            }

            DataSet adresa_narudzbe = classSQL.select("select * from adresa_dostave where id = (select id_adresa_dostave from na_stol where id_stol = '" + stol + "' limit 1)", "adresa_narudzbe");
            if (adresa_narudzbe != null && adresa_narudzbe.Tables.Count > 0 && adresa_narudzbe.Tables[0] != null && adresa_narudzbe.Tables[0].Rows.Count > 0)
            {
                PrintTextLine("Mjesto: " + adresa_narudzbe.Tables[0].Rows[0]["mjesto"], false);
                PrintTextLine("Ulica: " + adresa_narudzbe.Tables[0].Rows[0]["ulica"], false);
                PrintTextLine("K. broj: " + adresa_narudzbe.Tables[0].Rows[0]["kbr"], false);
                PrintTextLine("Telefon: " + adresa_narudzbe.Tables[0].Rows[0]["telefon"], false);
            }

            if (DT.Rows.Count > 0)
            {
                PrintTextLine("Stol: " + DT.Rows[0]["naziv"].ToString(), false);
            }
            PrintTextLine(String.Format("Blagajnik : {0}", cashierName), false);
            PrintTextLine("Datum: " + DateTime.Now, false);
            if (broj_narudzbe != null)
            {
                PrintTextLine("Broj narudžbe: " + broj_narudzbe, false);
            }
            _1 = tekst;
            tekst = "";

            int a = 24; //artikl
                        //          int j = 8;  //cijena
            int k = 5;  //kolicina
                        //a += 8;

            PrintTextLine(String.Empty, false);
            PrintText(TruncateAt("KOL".PadRight(k), k, false), false);
            PrintText(TruncateAt("STAVKA".PadRight(a), a, false), false);
            //PrintText(TruncateAt("KN".PadRight(j), j));
            PrintText("\r\n", false);
            PrintTextLine(new string('=', RecLineChars), false);

            //broj_narudzbe = null;
        }


        private static void PrintText(string text, bool artikl)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars, artikl);
        }

        private static void PrintTextLine(string text, bool artikl)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars, artikl);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private static string TruncateAt(string text, int maxWidth, bool artikl)
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
            String drawString = _1;
            Font drawFont = new Font("MS Gothic", 14, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(_1, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //stavke
            drawString = _2;
            drawFont = new Font("MS Gothic", 14, FontStyle.Bold);
            float y = height;
            float x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_2, drawFont);

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

        /// <summary>
        /// Broj printera 2 ili 3
        /// </summary>
        /// <param name="brojPrintera"></param>
        private static void printaj(int brojPrintera)
        {
            //SVI IDovi su sjebani do kraja. 
            // 4 - Sank
            // 3 - Kuhinja
            // 10 - Picerija
            string printerName = "";
            if (brojPrintera == 4)
                printerName = DTsetting.Rows[0]["windows_printer_sank"].ToString();
            else if (brojPrintera == 3 || brojPrintera == 2)
                printerName = DTsetting.Rows[0]["windows_printer_name2"].ToString();
            else if (brojPrintera == 10)
                printerName = DTsetting.Rows[0]["windows_printer_name3"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            string drawString = _1 + _2;

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                string ttx = _1 + _2 + _3;
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
    }
}