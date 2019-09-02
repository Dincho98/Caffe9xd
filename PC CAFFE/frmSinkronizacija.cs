using System;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSinkronizacija : Form
    {
        public frmSinkronizacija()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string ducan = "";
        public DateTime DatumOD { get; set; }
        public DateTime DatumDO { get; set; }
        private DataTable DTp;
        private string fileName = "";
        private string pathZip = "";
        private string pathTxt = "";

        private void frmSinkronizacija_Load(object sender, EventArgs e)
        {
            dtpOD.Select();

            dtpDO.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
            dtpOD.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:01");

            DTp = classSQL.select("SELECT ime_ducana FROM postavke LEFT JOIN ducan ON ducan.id_ducan=CAST(postavke.default_ducan AS INT)", "postavke").Tables[0];
            ducan = DTp.Rows[0][0].ToString();
            txtPutanja.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + DTp.Rows[0][0].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".zip";
            fileName = DTp.Rows[0][0].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".zip";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileInfo fileInf = new FileInfo(txtPutanja.Text);
            //string uri = "ftp://pc1.hr/pc1.hr/hamer/" + fileName;

            //string uri = "ftp://hamercom@hamer.com.hr/www/" + fileName;
            string uri = "ftp://ftp.fino-ck.hr/www/" + fileName;
            progressBar1.Maximum = Convert.ToInt32(fileInf.Length);
            progressBar1.Minimum = 0;
            double progress_counter = 0;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            //reqFTP.Credentials = new NetworkCredential("hamercom", "4kyaY96u4Y");
            reqFTP.Credentials = new NetworkCredential("finockhr", "dbD04V7tr9");
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    if ((progressBar1.Value + contentLen) <= progressBar1.Maximum)
                    {
                        progressBar1.Value += contentLen;
                        progressBar1.Increment(contentLen);
                    }
                    else
                    {
                        progressBar1.Value = progressBar1.Maximum;
                    }
                    double progress_now = (double)((double)(progressBar1.Value / 100) * 100) / (progressBar1.Maximum / 100);

                    if (progress_now > progress_counter)
                    {
                        lblSlanje.Text = String.Format("{0}% of {1}kb", Math.Round(progress_now, 2), (progressBar1.Maximum / 1000).ToString("#,#"));
                        Application.DoEvents();
                        progress_counter++;
                    }
                }

                // Close the file stream and the Request Stream
                strm.Close();
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                lblSlanje.Text = "";
                progressBar1.Value = Convert.ToInt32(fileInf.Length);
                MessageBox.Show("Datoteka je uspješno poslana.");
                fs.Close();
            }
            catch (WebException l)
            {
                String status = ((FtpWebResponse)l.Response).StatusDescription;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja.Text = openFileDialog1.FileName;
                fileName = System.IO.Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void btnKreirajFajlove_Click(object sender, EventArgs e)
        {
            DatumOD = dtpOD.Value;
            DatumDO = dtpDO.Value;

            try
            {
                KreirajPutanju(DatumOD.Year.ToString(), DatumOD.Month.ToString(), DatumOD.Day.ToString(), System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                pathTxt = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DatumOD.Year.ToString() + "\\" + DatumOD.Month.ToString() + "\\" + DatumOD.Day.ToString();
                pathZip = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DatumOD.Year.ToString() + "\\" + DatumOD.Month.ToString() + "\\" + DatumOD.Day.ToString() + "";

                synkracuni();
                synkstavke();
                synkplacanje();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Zip.Go(pathTxt, pathZip + "\\" + DTp.Rows[0][0].ToString() + "_" + DatumOD.ToString("ddMMyyyy") + ".zip");

            MessageBox.Show("Fajlovi su uspješno kreirani", "Kreirano");
        }

        private void synkracuni()
        {
            string zapisi = "\r\n";

            DataTable DTsync1 = new DataTable();
            string sync1 = "SELECT broj_racuna, datum_racuna, storno, id_kasa, id_kupac, id_blagajnik, id_ducan FROM racuni WHERE  racuni.datum_racuna> '" + DatumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + DatumDO.ToString("yyyy-MM-dd H:mm:ss") + "' ORDER BY CAST(broj_racuna AS int) ASC ";

            DTsync1 = classSQL.select(sync1, "racuni").Tables[0];

            DataTable DTsync = new DataTable();

            for (int i = 0; DTsync1.Rows.Count > i; i++)
            {
                string broj = DTsync1.Rows[i]["broj_racuna"].ToString();
                string storno = "N";
                string id_kasa = DTsync1.Rows[i]["id_kasa"].ToString();
                string id_kupac = DTsync1.Rows[i]["id_kupac"].ToString();
                string id_blagajnik = DTsync1.Rows[i]["id_blagajnik"].ToString();
                string id_ducan = ducan;
                DateTime datum_racuna = Convert.ToDateTime(DTsync1.Rows[i]["datum_racuna"].ToString());

                string sync = "SELECT " +
                " mpc, vpc, kolicina, porez_potrosnja, rabat, porez " +
                "FROM racun_stavke WHERE broj_racuna = '" + broj + "'  AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'" +
                "";

                DTsync = classSQL.select(sync, "racuni_stavke").Tables[0];

                string godina = dtpOD.Value.Year.ToString();
                DateTime Sistemski_datum = DateTime.Now;
                DateTime Administrativni_datum = dtpOD.Value;
                DateTime Sistemsko_vrijeme = DateTime.Now;
                decimal iznos = 0;
                decimal porez = 0;
                decimal popust = 0;
                decimal porez_stavka = 0;
                decimal povrat = 0;

                //godina = DTsync1.Rows[i]["godina"].ToString();
                //Sistemski_datum = Convert.ToDateTime(DTsync1.Rows[i]["sistemski_datum"].ToString());
                //Administrativni_datum = Convert.ToDateTime(DTsync1.Rows[i]["administrativni_datum"].ToString());
                //Sistemsko_vrijeme = Convert.ToDateTime(DTsync1.Rows[i]["sistemsko_vrijeme"].ToString());

                for (int y = 0; DTsync.Rows.Count > y; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTsync.Rows[y]["vpc"].ToString());
                    decimal mpc = Convert.ToDecimal(DTsync.Rows[y]["mpc"].ToString());
                    decimal porez_potr = Convert.ToDecimal(DTsync.Rows[y]["porez_potrosnja"].ToString());
                    decimal kol = Convert.ToDecimal(DTsync.Rows[y]["kolicina"].ToString());
                    decimal rab = Convert.ToDecimal(DTsync.Rows[y]["rabat"].ToString());
                    decimal pdv = Convert.ToDecimal(DTsync.Rows[y]["porez"].ToString());

                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * (pdv + porez_potr)) / (100 + pdv + porez_potr));

                    popust += (mpc * rab / 100) * kol;
                    mpc = mpc - (mpc * rab / 100);

                    porez_stavka = Math.Round(((mpc * kol) * (PreracunataStopaPDV / 100)), 3) + porez_stavka;
                    iznos = Math.Round((mpc * kol), 3) + iznos;
                    porez = (((mpc * kol) * PreracunataStopaPDV) / 100) + porez;
                }

                string status = "1";
                string obracun_smjene = "N";
                string izdanih_r_racuna = id_kupac;
                string izdanih_kopija_potvrda = "0";
                string broj_izvrsenih_korekcija = "0";
                string sifra_posl_partnera = "";
                string popust_np = "0";
                string broj_smjene = "0";
                string ind_knj = "N";

                //privremeno - blagajnik uvijek mora biti 001
                string blagajnik = "001";

                try
                {
                    if (i == 0)
                    {
                        if (File.Exists(pathTxt + "\\" + id_ducan + "_KRACUNI_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt"))
                        {
                            File.Delete(pathTxt + "\\" + id_ducan + "_KRACUNI_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                zapisi += "#" + nule(8, broj) + "#;" + "#" + godina + "#;" +
                     "#" + Sistemsko_vrijeme.ToString("dd.MM.yyyy") + "#;" +
                     "#" + Sistemsko_vrijeme.ToString("dd.MM.yyyy HH:mm") + "#;" + "#" + Administrativni_datum.ToString("dd.MM.yyyy") + "#;" +
                     "#" + status + "#;" + "#" + nule(10, Math.Round(iznos, 2).ToString("#0.00")).Replace(",", ".") +
                     "#;" + "#" + nule(10, Math.Round(porez, 2).ToString("#0.00")).Replace(",", ".") + "#;" +
                     "#" + nule(10, Math.Round(popust, 2).ToString("#0.00")).Replace(",", ".") +
                     "#;" + "#" + nule(10, Math.Round(povrat, 2).ToString("#0.00")).Replace(",", ".") + "#;" +
                     "#" + obracun_smjene + "#;" + "#" + r_racun(izdanih_r_racuna) + "#;" + "#" + izdanih_kopija_potvrda +
                     "#;" + "#" + broj_izvrsenih_korekcija + "#;" + "#" + nule(2, id_kasa) + "#;" +
                     "#" + blagajnik + "#;" + "#" + sifra_posl_partnera + "#;" + "#" + popust_np +
                     "#;" + "#" + id_ducan + "#;" + "#" + storno +
                     "#;" + "#" + broj_smjene + "#;" + "#" + id_ducan + "#;" + "#" + ind_knj + "#";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathTxt + "\\" + id_ducan + "_KRACUNI_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt", true))
                {
                    file.WriteLine(zapisi);
                    zapisi = "";
                }
            }
        }

        private void synkstavke()
        {
            string zapisi = "\r\n";
            DataTable DTsync1 = new DataTable();
            string sync1 = "SELECT broj_racuna,datum_racuna, storno, id_kasa, id_blagajnik, id_ducan, EXTRACT(YEAR FROM datum_racuna) as godina_racuna FROM racuni WHERE  racuni.datum_racuna> '" + DatumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + DatumDO.ToString("yyyy-MM-dd H:mm:ss") + "' ORDER BY datum_racuna ASC ";

            DTsync1 = classSQL.select(sync1, "racuni").Tables[0];
            DataTable DTsync = new DataTable();
            int provjera_za_prvi = 0;
            for (int i = 0; DTsync1.Rows.Count > i; i++)
            {
                Int32 brojac = 0;
                string broj = DTsync1.Rows[i]["broj_racuna"].ToString();
                string id_kasa = DTsync1.Rows[i]["id_kasa"].ToString();
                string id_blagajnik = DTsync1.Rows[i]["id_blagajnik"].ToString();
                string id_ducan = ducan;
                string godina_racuna = DTsync1.Rows[i]["godina_racuna"].ToString();

                string sync = "SELECT " +
                "CURRENT_DATE as Sistemski_datum, " +
                " racun_stavke.id_stavka, racun_stavke.sifra_robe, " +
                " racun_stavke.mpc, racun_stavke.vpc, racun_stavke.kolicina, racun_stavke.porez_potrosnja, racun_stavke.rabat, racun_stavke.porez, " +
                " roba.naziv, roba.jm, roba.sifra " +
                "FROM racun_stavke LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE broj_racuna = '" + broj + "'  AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'" +
                "";

                DTsync = classSQL.select(sync, "racuni_stavke").Tables[0];

                DateTime Sistemski_datum = new DateTime();

                for (int y = 0; DTsync.Rows.Count > y; y++)
                {
                    brojac++;
                    decimal popust = 0;
                    decimal neto_bez_pdv = 0;

                    decimal mpc = Convert.ToDecimal(DTsync.Rows[y]["mpc"].ToString());
                    decimal porez_potr = Convert.ToDecimal(DTsync.Rows[y]["porez_potrosnja"].ToString());
                    decimal vpc = Convert.ToDecimal(DTsync.Rows[y]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTsync.Rows[y]["kolicina"].ToString());
                    decimal rab = Convert.ToDecimal(DTsync.Rows[y]["rabat"].ToString());
                    decimal pdv = Convert.ToDecimal(DTsync.Rows[y]["porez"].ToString());

                    decimal pdv_grupa = pdv + porez_potr;

                    Sistemski_datum = Convert.ToDateTime(DTsync.Rows[y]["Sistemski_datum"].ToString());
                    string naziv_artikla = DTsync.Rows[y]["naziv"].ToString();

                    string jedinicna_mjera = DTsync.Rows[y]["jm"].ToString();

                    if (jedinicna_mjera.Length > 4)
                    {
                        jedinicna_mjera = jedinicna_mjera.Remove(4);
                    }

                    string sifra_artikla = DTsync.Rows[y]["sifra"].ToString();
                    Int32 id_stavka = brojac;
                    DateTime datum_racuna = Convert.ToDateTime(DTsync1.Rows[i]["datum_racuna"].ToString());

                    //popust = (mpc * rab / 100) * kol;
                    //mpc = mpc - (mpc * rab / 100);

                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * (pdv + porez_potr)) / (100 + pdv + porez_potr));

                    decimal porez_stavka = ((mpc - (mpc * rab / 100)) * kol) * (PreracunataStopaPDV / 100);
                    neto_bez_pdv = vpc;

                    string vrsta_popusta = "";
                    string Pop_broj = "";
                    string rekpr_broj = "";
                    string iznos_popusta_reklamno = "0";
                    string sifra_grupe_artikala = "               ";
                    string naziv_grupe_artikala = "";
                    string faktor_poreza = "0";
                    string nevazno = "";

                    try
                    {
                        if (provjera_za_prvi == 0)
                        {
                            if (File.Exists(pathTxt + "\\" + id_ducan + "_KSTRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt"))
                            {
                                File.Delete(pathTxt + "\\" + id_ducan + "_KSTRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt");
                            }
                            provjera_za_prvi++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    zapisi += "#" + nule(3, Convert.ToString(id_stavka)) + "#;" + "#" + nule(10, Math.Round(kol, 3).ToString("#0.000")).Replace(",", ".") + "#;" +
                       "#" + nule(10, Math.Round(mpc, 2).ToString("#0.00")).Replace(",", ".") + "#;" +
                       "#" + nule(10, Convert.ToString(Math.Round(neto_bez_pdv, 3).ToString("#0.000"))).Replace(",", ".") + "#;" + "#" + nule(2, Convert.ToString(porezna_grupa(pdv_grupa))) +
                       "#;" + "#" + nule(10, Math.Round(((mpc * rab / 100) * kol), 2).ToString("#0.00")).Replace(",", ".") + "#;" +
                       "#" + vrsta_popusta + "#;" +
                       "#" + nule(2, id_kasa) + "#;" + "#" + nule(8, broj) + "#;" + "#" + godina_racuna +
                       "#;" + "#" + sifra_artikla + "#;" + "#" + Pop_broj + "#;" +
                       "#" + rekpr_broj + "#;" + "#" + iznos_popusta_reklamno + "#;" + "#" + id_ducan +
                       "#;" + "#" + sifra_grupe_artikala + "#;" +
                       "#" + naziv_grupe_artikala + "#;" + "#" + razmaci(naziv_artikla) + "#;" + "#" + jedinicna_mjera + "#;" + "#" + faktor_poreza + "#;" + "#" + nule(10, Math.Round(porez_stavka, 2).ToString("#0.00")).Replace(",", ".") + "#;" + "#" + "00000000.00" + "#";
                    //File.WriteAllText("D:\\" + id_ducan + "_KRACUNI" + Sistemski_datum.ToString("dd-MM-yyyy") + "", zapisi);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathTxt + "\\" + id_ducan + "_KSTRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt", true))
                    {
                        file.WriteLine(zapisi);
                        zapisi = "";
                    }
                }
            }
        }

        private void synkplacanje()
        {
            string zapisi = "\r\n";

            DataTable DTsync1 = new DataTable();
            string sync1 = "SELECT broj_racuna, datum_racuna, nacin_placanja, storno, id_kasa, id_blagajnik, id_ducan, ukupno, EXTRACT(YEAR FROM datum_racuna) as godina_racuna FROM racuni WHERE  racuni.datum_racuna> '" + DatumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + DatumDO.ToString("yyyy-MM-dd H:mm:ss") + "'  ORDER BY CAST(broj_racuna AS int) ASC  ";

            DTsync1 = classSQL.select(sync1, "racuni").Tables[0];
            DataTable DTsync = new DataTable();
            for (int i = 0; DTsync1.Rows.Count > i; i++)
            {
                string broj = DTsync1.Rows[i]["broj_racuna"].ToString();
                string sifra_value = "191";
                string broj_kase = "01";
                string redni_broj_naplate_po_rac = "1";
                string broj_kartice = "0";
                string nacin_placanja = DTsync1.Rows[i]["nacin_placanja"].ToString();
                string datum_naplate = "";
                string id_broj = "";
                string id_broj2 = "";
                decimal ukupno_racun = Convert.ToDecimal(DTsync1.Rows[i]["ukupno"].ToString());
                string id_kasa = DTsync1.Rows[i]["id_kasa"].ToString();
                string id_blagajnik = DTsync1.Rows[i]["id_blagajnik"].ToString();
                string id_ducan = ducan;
                string godina_racuna = DTsync1.Rows[i]["godina_racuna"].ToString();
                DateTime datum_racuna = Convert.ToDateTime(DTsync1.Rows[i]["datum_racuna"].ToString());

                try
                {
                    if (i == 0)
                    {
                        if (File.Exists(pathTxt + "\\" + id_ducan + "_KPLRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt"))
                        {
                            File.Delete(pathTxt + "\\" + id_ducan + "_KPLRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                zapisi += "#" + sifra_value + "#;" + "#" + broj_kase + "#;" +
                       "#" + nule(8, broj) + "#;" +
                       "#" + godina_racuna + "#;" + "#" + nule(10, Convert.ToString(ukupno_racun.ToString("#0.00"))).Replace(",", ".") +
                       "#;" + "#" + redni_broj_naplate_po_rac + "#;" +
                       "#" + broj_kartice + "#;" +
                       "#" + nacin_pl(nacin_placanja) + "#;" + "#" + datum_naplate + "#;" + "#" + id_broj +
                       "#;" + "#" + id_broj2 + "#;" + "#" + id_ducan + "#";
                //File.WriteAllText("D:\\" + id_ducan + "_KRACUNI" + Sistemski_datum.ToString("dd-MM-yyyy") + "", zapisi);

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathTxt + "\\" + id_ducan + "_KPLRACUNa_" + datum_racuna.ToString("dd-MM-yyyy").Replace("-", "") + ".txt", true))
                {
                    file.WriteLine(zapisi);
                    zapisi = "";
                }
            }
        }

        public string r_racun(string input)
        {
            string vrati_string = "";

            if (input == "0")
            {
                vrati_string = "0";
            }
            else if (input != "0")
            {
                vrati_string = "1";
            }

            return vrati_string;
        }

        public decimal porezna_grupa(decimal input)
        {
            decimal vrati_poreznu_grupu = 0;

            if (input == 28)
            {
                vrati_poreznu_grupu = 28;
            }
            else if (input == 25) { vrati_poreznu_grupu = 25; }
            else if (input == 13) { vrati_poreznu_grupu = 33; }
            else if (input == 10) { vrati_poreznu_grupu = 06; }
            else if (input == 5) { vrati_poreznu_grupu = 05; }
            else if (input == 16) { vrati_poreznu_grupu = 16; }
            else
            {
                vrati_poreznu_grupu = input;
            }

            return vrati_poreznu_grupu;
        }

        public string storn(string input)
        {
            string vrati_string = "";
            if (input == "DA")
            {
                vrati_string = "S";
            }
            else if (input == "NE")
            {
                vrati_string = "N";
            }

            return vrati_string;
        }

        public string nacin_pl(string input)
        {
            string strmessage = "";

            if (input == "G")
            {
                strmessage = "1";
            }
            else if (input == "O")
            {
                strmessage = "3";
            }
            else if (input == "K")
            {
                strmessage = "4";
            }
            else
            {
                strmessage = "1";
            }

            return strmessage;
        }

        public string razmaci(string input)
        {
            string strmessage = "";
            int rez = 0;

            if (input.Length > 35)
            {
                input = input.Remove(35);
            }

            rez = 35 - input.Length;

            strmessage = input + new String(' ', rez);

            return strmessage;
        }

        public string nule(decimal maxchar, string input)
        {
            if (input == "10")
            {
            }

            string strmessage = "";
            string minus = "";
            decimal rez = 0;
            if (input.Contains("-"))
            {
                input.Replace("-", "");
                minus = "-";
            }
            else
            {
                minus = "";
            }
            input = input.Replace("-", "");
            if (input.Contains(",") || input.Contains("."))
            {
                if (input.Length < 12)
                {
                    decimal zarez = maxchar + 1;
                    rez = zarez - input.Length;
                }
            }
            else
            {
                if (input.Length < 12)
                {
                    rez = maxchar - input.Length;
                }
            }

            if (rez == 1)
            {
                strmessage = minus + "0" + input;
            }
            else if (rez == 2)
            {
                strmessage = minus + "00" + input;
            }
            else if (rez == 3)
            {
                strmessage = minus + "000" + input;
            }
            else if (rez == 4)
            {
                strmessage = minus + "0000" + input;
            }
            else if (rez == 5)
            {
                strmessage = minus + "00000" + input;
            }
            else if (rez == 6)
            {
                strmessage = minus + "000000" + input;
            }
            else if (rez == 7)
            {
                strmessage = minus + "0000000" + input;
            }
            else if (rez == 8)
            {
                strmessage = minus + "00000000" + input;
            }
            else if (rez == 9)
            {
                strmessage = minus + "000000000" + input;
            }
            else if (rez == 10)
            {
                strmessage = minus + "0000000000" + input;
            }
            else if (rez == 11)
            {
                strmessage = minus + "00000000000" + input;
            }
            else
            {
                strmessage = input;
            }

            return strmessage;
        }

        private void racunaj(string broj)
        {
            int duzina = broj.Length;
        }

        private void KreirajPutanju(string godina, string mjesec, string dan, string path)
        {
            if (!Directory.Exists(path + "\\" + godina))
                Directory.CreateDirectory(path + "\\" + godina);

            if (!Directory.Exists(path + "\\" + godina + "\\" + mjesec))
                Directory.CreateDirectory(path + "\\" + godina + "\\" + mjesec);

            if (!Directory.Exists(path + "\\" + godina + "\\" + mjesec + "\\" + dan))
                Directory.CreateDirectory(path + "\\" + godina + "\\" + mjesec + "\\" + dan);
        }
    }
}