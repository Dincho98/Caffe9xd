using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOpcije2 : Form
    {
        public frmOpcije2()
        {
            InitializeComponent();
        }

        public Caffe.frmCaffe FormCaffe { get; set; }
        private DataTable DTsetting;
        private DataTable DTsend;
        private DataTable DTsettings = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTremotePostavke;
        private DataTable DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private Until.FunkcijeRobno RobnoFunkcije = new Until.FunkcijeRobno();

        private void frmOpcije2_Load(object sender, EventArgs e)
        {

        }

        private void LoadFunkcija()
        {
            int si = 7;
            while (si < 72)
            {
                if (si < 12)
                {
                    si++;
                }
                else if (si >= 12 && si < 28)
                {
                    si = si + 2;
                }
                else if (si >= 28 && si < 36)
                {
                    si = si + 8;
                }
                else if (si >= 36 && si < 48)
                {
                    si = si + 12;
                }
                else
                {
                    si = si + 24;
                }

                cmbSize.Items.Add(si);
            }

            DTremotePostavke = classSQL.select("SELECT * FROM remote_postavke", "remote_postavke").Tables[0];

            if (DTsettings.Rows[0]["is_caffe"].ToString() == "0")
            {
                groupBox2.Visible = false;
            }

            if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() == "1")
            {
                chbRadSaNarudzbama.Checked = true;
            }
            else
            {
                chbRadSaNarudzbama.Checked = false;
            }

            if (DTsettings.Rows[0]["preferirajSifre"].ToString() == "1")
            {
                chbRadSSiframa.Checked = true;
            }
            else
            {
                chbRadSSiframa.Checked = false;
            }

            string[] bui = DTsettings.Rows[0]["bui_icona"].ToString().Split(';');
            chkBold.Checked = Convert.ToBoolean(Convert.ToInt32(bui[0]));
            chkUnderline.Checked = Convert.ToBoolean(Convert.ToInt32(bui[1]));
            chkItalic.Checked = Convert.ToBoolean(Convert.ToInt32(bui[2]));

            cmbSize.SelectedItem = Convert.ToInt32(DTsettings.Rows[0]["font_size_icona"]);
            btnButtonColor.BackColor = Util.Korisno.hexToColor(DTsettings.Rows[0]["button_color_icona"].ToString());
            btnFontColor.BackColor = Util.Korisno.hexToColor(DTsettings.Rows[0]["font_color_icona"].ToString());

            nuVisina.Value = Convert.ToInt32(DTsettings.Rows[0]["cafe_icon_height"].ToString());
            nuSirina.Value = Convert.ToInt32(DTsettings.Rows[0]["caffe_icon_width"].ToString());
        }


        private void nuVisina_ValueChanged(object sender, EventArgs e)
        {
            classSQL.Setings_Update("UPDATE postavke SET cafe_icon_height='" + nuVisina.Value + "'");
            foreach (Control c in FormCaffe.flpArtikli.Controls)
            {
                c.Size = new Size(c.Width, Convert.ToInt32(nuVisina.Value));
            }
            FormCaffe.getArtikli(true);
        }

        private void nuSirina_ValueChanged(object sender, EventArgs e)
        {
            classSQL.Setings_Update("UPDATE postavke SET caffe_icon_width='" + nuSirina.Value + "'");
            foreach (Control c in FormCaffe.flpArtikli.Controls)
            {
                c.Size = new Size(Convert.ToInt32(nuSirina.Value), c.Height);
            }
            FormCaffe.getArtikli(true);
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        public bool _zavrsi { get; set; }

        private void btnStornoZadnjegR_Click(object sender, EventArgs e)
        {
            bool storno = false;
            if (!File.Exists("belveder"))
            {
                if (MessageBox.Show("Dali ste sigurni da želite stornirati račun?", "Storno računa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    storno = true;
                }
            }
            else
            {
                Dodaci.frmVracaNekuVrijednost v = new Dodaci.frmVracaNekuVrijednost();
                Properties.Settings.Default.privremena_vrijednost = "";
                v.txtBroj.PasswordChar = '*';
                v._title = "Unesite ključ:";
                v.ShowDialog();

                string key = File.ReadAllText("belveder");
                if (Properties.Settings.Default.privremena_vrijednost == key)
                {
                    if (MessageBox.Show("Dali ste sigurni da želite stornirati račun?", "Storno računa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        storno = true;
                    }
                }
                else
                {
                    MessageBox.Show("Krivi unos!");
                }
            }

            if (storno)
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
                DTsend.Columns.Add("godina");
                DTsend.Columns.Add("rabat");
                DTsend.Columns.Add("cijena");
                DTsend.Columns.Add("ime");
                DTsend.Columns.Add("stol");
                DTsend.Columns.Add("jelo");
                DTsend.Columns.Add("porez_potrosnja");
                DTsend.Columns.Add("storno");
                DTsend.Columns.Add("id_ducan");
                DTsend.Columns.Add("id_blagajna");
                DTsend.Columns.Add("dod");
                DTsend.Columns.Add("pol");

                DataRow row;

                string brRac = brojRacuna();
                DateTime dRac = Convert.ToDateTime(DateTime.Now);
                string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

                DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni " +
                    "WHERE  id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "' " +
                    "AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    "AND godina='" + DateTime.Now.Year.ToString() + "'"
                    , "racuni").Tables[0];

                string sql = "SELECT * FROM racuni WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "' AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "' AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'";
                DataTable DTr = classSQL.select(sql, "racuni").Tables[0];

                if (DTr.Rows.Count > 0)
                {
                    if (DTr.Rows[0]["storno"].ToString() == "DA")
                    {
                        MessageBox.Show("Ovaj je račun je već stonirani.");
                        return;
                    }
                    decimal dec;
                    decimal Dukupno_virman = 0;
                    if (!decimal.TryParse(DTr.Rows[0]["ukupno_virman"].ToString(), out dec))
                    {
                        Dukupno_virman = 0;
                    }
                    else
                    {
                        Dukupno_virman = Convert.ToDecimal(DTr.Rows[0]["ukupno_virman"].ToString());
                    }

                    string sqlR = "INSERT INTO racuni (" +
                        " broj_racuna," +
                        " datum_racuna," +
                        " id_ducan," +
                        " id_kasa," +
                        " id_kupac," +
                        " id_blagajnik," +
                        " ukupno_gotovina," +
                        " ukupno_kartice," +
                        " storno," +
                        " ukupno," +
                        " dobiveno_gotovina," +
                        " id_stol," +
                        " nacin_placanja," +
                        " ukupno_virman," +
                        " godina" +
                        ") VALUES (" +
                        "'" + brRac + "'," +
                         "'" + dt + "'," +
                        "'" + DTr.Rows[0]["id_ducan"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_kasa"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_kupac"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_blagajnik"].ToString() + "'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno_gotovina"].ToString()) * (-1)).ToString() + "'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno_kartice"].ToString()) * (-1)).ToString() + "'," +
                        "'NE'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno"].ToString()) * (-1)).ToString() + "'," +
                        "'" + DTr.Rows[0]["dobiveno_gotovina"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_stol"].ToString() + "'," +
                        "'" + DTr.Rows[0]["nacin_placanja"].ToString() + "'," +
                        "'" + (Dukupno_virman * (-1)).ToString() + "'," +
                        "'" + DTr.Rows[0]["godina"].ToString() + "'" +
                        ")";

                    string aa = DTr.Rows[0]["ukupno_gotovina"].ToString();

                    provjera_sql(classSQL.insert(sqlR));

                    string sqlA = "SELECT * FROM racun_stavke WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "'" +
                        " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                        " AND godina='" + DateTime.Now.Year.ToString() + "'" +
                        " AND id_blagajna='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'";

                    DataTable DTA = classSQL.select(sqlA, "racun_stavke").Tables[0];

                    for (int i = 0; i < DTA.Rows.Count; i++)
                    {
                        sifra = DTA.Rows[i]["sifra_robe"].ToString();
                        row = DTsend.NewRow();
                        row["broj_racuna"] = brRac;
                        row["sifra_robe"] = sifra;
                        row["mpc"] = DTA.Rows[i]["mpc"].ToString();
                        row["godina"] = DTA.Rows[i]["godina"].ToString();
                        row["id_skladiste"] = DTA.Rows[i]["id_skladiste"].ToString();
                        row["porez"] = DTA.Rows[i]["porez"].ToString();
                        row["kolicina"] = (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString();
                        row["rabat"] = DTA.Rows[i]["rabat"].ToString();
                        row["vpc"] = DTA.Rows[i]["vpc"].ToString();
                        row["cijena"] = DTA.Rows[i]["mpc"].ToString();
                        row["porez_potrosnja"] = DTA.Rows[i]["porez_potrosnja"].ToString();
                        row["ime"] = classSQL.select("SELECT naziv FROM roba WHERE sifra='" + sifra + "'", "roba").Tables[0].Rows[0][0].ToString();
                        row["stol"] = DTr.Rows[0]["id_stol"].ToString();
                        row["nbc"] = DTA.Rows[0]["nbc"].ToString().Replace(",", ".");
                        row["dod"] = 0;
                        row["pol"] = 0;

                        DTsend.Rows.Add(row);

                        sqlA = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,odjava,nbc,porez_potrosnja,id_ducan,godina,id_blagajna) VALUES (" +
                            "'" + brRac + "'," +
                            "'" + DTA.Rows[i]["sifra_robe"].ToString() + "'," +
                            "'" + DTA.Rows[i]["id_skladiste"].ToString() + "'," +
                            "'" + DTA.Rows[i]["mpc"].ToString() + "'," +
                            "'" + DTA.Rows[i]["porez"].ToString() + "'," +
                            "'" + (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString() + "'," +
                            "'" + DTA.Rows[i]["rabat"].ToString() + "'," +
                            "'" + DTA.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DTA.Rows[i]["odjava"].ToString() + "'," +
                            "'" + DTA.Rows[i]["nbc"].ToString().Replace(",", ".") + "'," +
                            "'" + DTA.Rows[i]["porez_potrosnja"].ToString() + "'," +
                            "'" + DTA.Rows[i]["id_ducan"].ToString() + "'," +
                            "'" + DTA.Rows[i]["godina"].ToString() + "'," +
                            "'" + DTA.Rows[i]["id_blagajna"].ToString() + "'" +
                            ")";
                        provjera_sql(classSQL.insert(sqlA));

                        SQL.ClassSkladiste.GetAmountCaffe(DTA.Rows[i]["sifra_robe"].ToString(), DTA.Rows[i]["id_skladiste"].ToString(), (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString(), "-");
                    }

                    if (DTr.Rows[0]["nacin_placanja"].ToString() == "G")
                        Util.Korisno.dodajIznosUBlagajnickiIzvjestaj(Convert.ToInt32(brRac), dRac, (Convert.ToDecimal(DTr.Rows[0]["ukupno"].ToString()) * (-1)));

                    //OVA FUNKCIJA POSTAVLJA SKLADIŠTE TAKO DA UZME U OBZIR SVE DOKUMENTE I ZAKLJUČI STANJE NA SKLADIŠTU
                    if (DTsettings.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "1")
                    {
                        RobnoFunkcije.PostaviStanjeSkladista();
                    }

                    if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
                    {
                        try
                        {
                            try
                            {
                                PosPrint.classPosPrintCaffe.PrintReceipt(DTsend, DTr.Rows[0]["id_blagajnik"].ToString(), brRac + "/" + DTr.Rows[0]["id_ducan"].ToString() + "/" + DTr.Rows[0]["id_kasa"].ToString(), DTr.Rows[0]["id_kupac"].ToString(), barcode, brRac, DTr.Rows[0]["nacin_placanja"].ToString(), "DA", 0);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\nŽelite li ispisati ovaj dokumenat na A4 format?\r\nOvo je orginalna greška:\r\n" + ex, "Printer", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                                rfak.dokumenat = "RAC";
                                rfak.ImeForme = "Račun";
                                rfak.broj_dokumenta = brRac;
                                rfak.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                        rfak.dokumenat = "RAC";
                        rfak.ImeForme = "Račun";
                        rfak.broj_dokumenta = brRac;
                        rfak.ShowDialog();
                    }

                    if (DSbr.Rows.Count != 0)
                    {
                        classSQL.update("UPDATE racuni SET storno='DA' WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "'" +
                            " AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "';");
                    }

                    MessageBox.Show("Izvršeno");
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool storno = false;
            if (!File.Exists("belveder"))
            {
                if (MessageBox.Show("Dali ste sigurni da želite stornirati račun?", "Storno računa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    storno = true;
                }
            }
            else
            {
                Dodaci.frmVracaNekuVrijednost v = new Dodaci.frmVracaNekuVrijednost();
                Properties.Settings.Default.privremena_vrijednost = "";
                v.txtBroj.PasswordChar = '*';
                v._title = "Unesite ključ:";
                v.ShowDialog();

                string key = File.ReadAllText("belveder");
                if (Properties.Settings.Default.privremena_vrijednost == key)
                {
                    if (MessageBox.Show("Dali ste sigurni da želite stornirati račun?", "Storno računa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        storno = true;
                    }
                }
                else
                {
                    MessageBox.Show("Krivi unos!");
                }
            }

            if (!storno)
                return;

            //if (MessageBox.Show("Dali ste sigurni da želite napraviti storno računa?", "Storno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
            Kasa.frmStornoRacuna2 sr = new Kasa.frmStornoRacuna2();
            sr.ShowDialog();
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kasa.frmDodajPartnera dp = new Kasa.frmDodajPartnera();
            dp.ShowDialog();
        }

        private void btnPromjenaPlacanja_Click(object sender, EventArgs e)
        {
            Kasa.frmPromjenaNacinaPlacanja pnp = new Kasa.frmPromjenaNacinaPlacanja();
            pnp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kasa.frmIspisOdredenogRacuna odd = new Kasa.frmIspisOdredenogRacuna();
            odd.ShowDialog();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string barcode = "";
        private string sifra = "";

        private void button1_Click(object sender, EventArgs e)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");

            DataRow row;

            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            DataTable DSkupac = classSQL.select("SELECT id_kupac FROM racuni WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];

            string sql = "SELECT racun_stavke.vpc,racun_stavke.sifra_robe,racun_stavke.porez_potrosnja,roba.naziv,racun_stavke.id_skladiste,racun_stavke.mpc,racun_stavke.porez,racun_stavke.kolicina,racun_stavke.rabat FROM racun_stavke " +
                " INNER JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE racun_stavke.broj_racuna='" + DSbr.Rows[0][0].ToString() + "'" +
                " AND racun_stavke.id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                " AND racun_stavke.id_blagajna='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                " AND racun_stavke.godina='" + DateTime.Now.Year.ToString() + "'";
            DataTable DTrac = classSQL.select(sql, "racun_stavke").Tables[0];

            for (int i = 0; i < DTrac.Rows.Count; i++)
            {
                sifra = DTrac.Rows[i]["sifra_robe"].ToString();
                row = DTsend.NewRow();
                row["ime"] = DTrac.Rows[i]["naziv"].ToString();
                row["porez"] = DTrac.Rows[i]["porez"].ToString();
                row["mpc"] = DTrac.Rows[i]["mpc"].ToString();
                row["kolicina"] = DTrac.Rows[i]["kolicina"].ToString();
                row["cijena"] = DTrac.Rows[i]["mpc"].ToString();
                row["vpc"] = DTrac.Rows[i]["vpc"].ToString();
                row["rabat"] = DTrac.Rows[i]["rabat"].ToString();
                row["porez_potrosnja"] = DTrac.Rows[i]["porez_potrosnja"].ToString();
                DTsend.Rows.Add(row);
            }

            string blagajnik = classSQL.select("SELECT ime + ' ' + prezime AS name FROM zaposlenici", "zaposlenici").Tables[0].Rows[0]["name"].ToString();

            barcode = "000" + DSbr.Rows[0][0].ToString();
            if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() != "0")
            {
                PosPrint.classPosPrintCaffeNaknadno.PrintReceipt(DTsend, blagajnik, DSbr.Rows[0][0].ToString() + "/" + DateTime.Now.Year.ToString(), DSkupac.Rows[0][0].ToString(), barcode, DSbr.Rows[0][0].ToString(), "G", "");
            }
            else
            {
                if (DTsend.Rows.Count > 0)
                {
                    Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                    rfak.dokumenat = "RAC";
                    rfak.ImeForme = "Račun";
                    rfak.broj_dokumenta = DSbr.Rows[0][0].ToString();

                    rfak.godina = DateTime.Now.Year.ToString();
                    rfak.id_kasa = DTsettings.Rows[0]["default_ducan"].ToString();
                    rfak.id_poslovnica = DTsettings.Rows[0]["default_blagajna"].ToString();

                    rfak.ShowDialog();
                }
                //MessageBox.Show("Nema printera!");
            }

            classSQL.update("UPDATE racuni SET broj_ispisa=broj_ispisa+1 WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "'" +
                    " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                    " AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "'");
        }

        private void chbRadSaNarudzbama_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRadSaNarudzbama.Checked)
            {
                classSQL.update("UPDATE remote_postavke SET rad_sa_narudzbama='1'");
            }
            else
            {
                classSQL.update("UPDATE remote_postavke SET rad_sa_narudzbama='0'");
            }
        }

        private void chbRadSSiframa_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRadSSiframa.Checked)
            {
                classSQL.Setings_Update("UPDATE postavke SET preferirajSifre='1'");
            }
            else
            {
                classSQL.Setings_Update("UPDATE postavke SET preferirajSifre='0'");
            }
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void openCashDrawer1()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();

            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            IntPtr pUnmanagedBytes = new IntPtr(0);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            PCPOS.PosPrint.RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                if (DTpostavke.Rows[0]["ladicaOn"].ToString() == "1")
                {
                    openCashDrawer1();
                }

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                PCPOS.PosPrint.RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, "" + COMMAND);
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

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat = new StringFormat();
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);
            String drawString = "";
            Font drawFont = font;

            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
        }

        private void frmOpcije_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnButtonColor_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = btnButtonColor.BackColor;
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string s = Util.Korisno.colorToHex(colorDialog1.Color);

                    classSQL.Setings_Update("UPDATE postavke SET button_color_icona = '" + s + "';");

                    //MessageBox.Show(s + Environment.NewLine + "UPDATE postavke SET button_color_icona = '" + s + "';");
                    foreach (Control c in FormCaffe.flpArtikli.Controls)
                    {
                        c.BackgroundImage = null;

                        c.BackColor = colorDialog1.Color;
                    }

                    this.btnButtonColor.BackColor = colorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = btnFontColor.BackColor;
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    classSQL.Setings_Update("UPDATE postavke SET font_color_icona='" + Util.Korisno.colorToHex(colorDialog1.Color) + "'");
                    foreach (Control c in FormCaffe.flpArtikli.Controls)
                    {
                        if (c.GetType() == typeof(Button))
                        {
                            c.BackgroundImage = null;
                            string s = Util.Korisno.colorToHex(colorDialog1.Color);
                            c.ForeColor = colorDialog1.Color;
                        }
                    }

                    this.btnFontColor.BackColor = colorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSize.SelectedItem != null)
                {
                    classSQL.Setings_Update("UPDATE postavke SET font_size_icona='" + cmbSize.SelectedItem + "'");
                    foreach (Control c in FormCaffe.flpArtikli.Controls)
                    {
                        float f = c.Font.Size;
                        float.TryParse(cmbSize.SelectedItem.ToString(), out f);
                        c.Font = new System.Drawing.Font(c.Font.FontFamily, f, onSizeChange());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in FormCaffe.flpArtikli.Controls)
                {
                    c.Font = bui(c.Font);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in FormCaffe.flpArtikli.Controls)
                {
                    c.Font = bui(c.Font);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkUnderline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in FormCaffe.flpArtikli.Controls)
                {
                    c.Font = bui(c.Font);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private FontStyle onSizeChange()
        {
            FontStyle fs = new FontStyle();
            if (chkBold.Checked) { fs = fs | FontStyle.Bold; }
            if (chkUnderline.Checked) { fs = fs | FontStyle.Underline; }
            if (chkItalic.Checked) { fs = fs | FontStyle.Italic; }

            if (!chkBold.Checked && !chkItalic.Checked && !chkUnderline.Checked) { fs = FontStyle.Regular; }

            return fs;
        }

        private Font bui(Font f)
        {
            try
            {
                string[] bui = new string[3];
                Font nf = f;
                FontStyle fs = new FontStyle();
                if (chkBold.Checked) { fs = fs | FontStyle.Bold; bui[0] = "1"; } else { bui[0] = "0"; }
                if (chkUnderline.Checked) { fs = fs | FontStyle.Underline; bui[1] = "1"; } else { bui[1] = "0"; }
                if (chkItalic.Checked) { fs = fs | FontStyle.Italic; bui[2] = "1"; } else { bui[2] = "0"; }

                if (!chkBold.Checked && !chkItalic.Checked && !chkUnderline.Checked) { fs = FontStyle.Regular; bui[0] = "0"; bui[1] = "0"; bui[2] = "0"; }

                nf = new Font(nf, fs);
                classSQL.Setings_Update("UPDATE postavke SET bui_icona='" + bui[0] + ";" + bui[1] + ";" + bui[2] + "'");

                return nf;
            }
            catch
            {
                return f;
            }
        }

        private void btnNaknadniPartnerNaRacun_Click(object sender, EventArgs e)
        {
            try
            {
                Caffe.frmNaknadnoDodavanjePartnera f = new frmNaknadnoDodavanjePartnera();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIspisOtpremnice_Click(object sender, EventArgs e)
        {
            try
            {
                string broj_otpremnice = Interaction.InputBox("Ispis otpremnice", "Otpremnica", classSQL.select("select coalesce(max(broj_otpremnice), 0) as br from otpremnice", "otpremnice").Tables[0].Rows[0]["br"].ToString());
                if (broj_otpremnice != null && broj_otpremnice != "0")
                {
                    Class.Otpremnica _otpremnica = new Class.Otpremnica();
                    _otpremnica.otpremnicaPripremaZaPrint(broj_otpremnice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

