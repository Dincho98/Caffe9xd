using PCPOS.Sinkronizacija;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Util
{
    internal class Korisno
    {
        public static bool kartica_kupca = false;
        public static decimal verzijaPrograma = 0;
        public static int selectButtonBorderSize = 5;
        public static string domena_za_sinkronizaciju;
        public static string oibTvrtke = "";

        public static string getDomenaZaSinkronizaciju()
        {
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            domena_za_sinkronizaciju = DTpostavke.Rows[0]["domena_za_sinkronizaciju"].ToString();
            if (domena_za_sinkronizaciju.Length > 0)
                return domena_za_sinkronizaciju;

            return "NemaDomeneZaSinkronizaciju";
        }

        public static void getKarticaKupca()
        {
            try
            {
                DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                kartica_kupca = Convert.ToBoolean(DTpostavke.Rows[0]["kartica_kupca"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static int GetDopustenjeZaposlenika()
        {
            int DOPUSTENJE = 999;
            DataSet DTzap = classSQL.select(string.Format("SELECT id_zaposlenik,id_dopustenje FROM zaposlenici WHERE id_zaposlenik='{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici");

            if (DTzap != null && DTzap.Tables.Count > 0 && DTzap.Tables[0] != null && DTzap.Tables[0].Rows.Count > 0)
            {
                if (int.TryParse(DTzap.Tables[0].Rows[0]["id_dopustenje"].ToString(), out DOPUSTENJE))
                {
                    return DOPUSTENJE;
                }
            }

            return DOPUSTENJE;
        }

        public static string colorToHex(System.Drawing.Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static Color hexToColor(string s)
        {
            try
            {
                int a = Int32.Parse(s.Substring(1, 2), NumberStyles.HexNumber);
                int r = Int32.Parse(s.Substring(3, 2), NumberStyles.HexNumber);
                int g = Int32.Parse(s.Substring(5, 2), NumberStyles.HexNumber);
                int b = Int32.Parse(s.Substring(7, 2), NumberStyles.HexNumber);
                //MessageBox.Show(s + Environment.NewLine + a + Environment.NewLine + r + Environment.NewLine + g + Environment.NewLine + b + Environment.NewLine);
                return Color.FromArgb(a, r, g, b);
            }
            catch
            {
                //MessageBox.Show(ex.Message);
                return Color.FromArgb(122, 220, 187, 64);
            }
        }

        public static FontStyle biu(String s)
        {
            try
            {
                string[] ass = s.Split(';');
                FontStyle fs = new FontStyle();
                if (Convert.ToBoolean(Convert.ToInt16(ass[0]))) { fs = fs | FontStyle.Bold; }
                if (Convert.ToBoolean(Convert.ToInt16(ass[1]))) { fs = fs | FontStyle.Italic; }
                if (Convert.ToBoolean(Convert.ToInt16(ass[2]))) { fs = fs | FontStyle.Underline; }

                if (!Convert.ToBoolean(Convert.ToInt16(ass[0])) && !Convert.ToBoolean(Convert.ToInt16(ass[1])) && !Convert.ToBoolean(Convert.ToInt16(ass[2]))) { fs = FontStyle.Regular; }

                return fs;
            }
            catch
            {
                FontStyle fs = new FontStyle();
                return fs;
            }
        }

        public static bool regKartica(string prefix, string sufix, string tekst, ref string kartica_kupca)
        {
            Match match = Regex.Match(tekst, @"" + prefix + "([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])" + sufix + "$");
            if (match.Success)
            {
                if (prefix.Length > 0)
                {
                    kartica_kupca = match.Value.Remove(0, prefix.Length);
                }
                else
                {
                    kartica_kupca = match.Value;
                }
                if (sufix.Length > 0)
                {
                    kartica_kupca = kartica_kupca.Remove(10, sufix.Length);
                }

                return true;
            }

            return false;
        }

        public static bool RadimSinkronizaciju = false;

        public static int GodinaKojaSeKoristiUbazi = 0;

        public Korisno()
        {
            PCPOS.Until.classFukcijeZaUpravljanjeBazom baza = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
            GodinaKojaSeKoristiUbazi = baza.UzmiGodinuKojaSeKoristi();
        }

        /// <summary>
        /// Vraća dućan i blagajnu u polju
        /// </summary>
        /// <returns></returns>
        ///
        public static string idDucan { get; set; }

        public static string idKasa { get; set; }
        public static string idKasaFakture { get; set; }
        public static string nazivDucan { get; set; }
        public static string nazivBlagajna { get; set; }
        public static string nazivBlagajnaFakture { get; set; }

        public static string[] VratiDucanIBlagajnu()
        {
            //DataTable dtpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
            DataTable dtpodaci = classSQL.select_settings("SELECT default_ducan, default_blagajna, default_kasa_fakture FROM postavke", "postavke").Tables[0];

            string ducanid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_ducan"].ToString() : "";
            string blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_blagajna"].ToString() : "";
            string blagajnaFaktureid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_kasa_fakture"].ToString() : "";
            idDucan = ducanid;
            idKasa = blagajnaid;
            idKasaFakture = blagajnaFaktureid;

            string ducan = "";
            string blagajna = "", blagajnaFakture = "";

            if (ducanid != "")
            {
                DataTable DTducan = classSQL.select("SELECT ime_ducana FROM ducan where id_ducan ='" + ducanid + "'", "ducan").Tables[0];
                ducan = DTducan.Rows.Count > 0 ? DTducan.Rows[0][0].ToString() : "";
            }
            if (blagajnaid != "")
            {
                DataTable DTblagajna = classSQL.select("SELECT ime_blagajne FROM blagajna where id_blagajna='" + blagajnaid + "'", "blagajna").Tables[0];
                blagajna = DTblagajna.Rows.Count > 0 ? DTblagajna.Rows[0][0].ToString() : "";
            }

            if (blagajnaFaktureid != "")
            {
                DataTable DTblagajna = classSQL.select("SELECT ime_blagajne FROM blagajna where id_blagajna='" + blagajnaFaktureid + "'", "blagajna").Tables[0];
                blagajnaFakture = DTblagajna.Rows.Count > 0 ? DTblagajna.Rows[0][0].ToString() : "";
            }

            nazivDucan = ducan;
            nazivBlagajna = blagajna;
            nazivBlagajnaFakture = blagajnaFakture;

            string[] a = new string[3];
            a[0] = ducan;
            a[1] = blagajna;
            a[2] = blagajnaFakture;

            return a;
        }

        /// <summary>
        /// vraća u obliku: '/PC/3'
        /// </summary>
        /// <param name="vrsta">4 za avans, 3 za ifb, 2 za fakture, 1 maloprodaja</param>
        /// <returns></returns>
        public static string VratiDucanIBlagajnuZaIspis(int vrsta)
        {
            //DataTable dtpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
            DataTable dtpodaci;
            string ducan = "", blagajna = "";
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            string sql = "";
            if (vrsta == 2)
            {
                sql = "SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTpostavke.Rows[0]["default_kasa_fakture"].ToString() + "';";
            }
            else
            {
                sql = "SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "';";
            }
            DataTable dt_blagajna = classSQL.select(sql, "zaposlenici").Tables[0];
            DataTable dt_ducan = classSQL.select("SELECT ime_ducana FROM ducan WHERE id_ducan='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'", "zaposlenici").Tables[0];

            if (dt_blagajna.Rows.Count > 0)
            {
                blagajna = dt_blagajna.Rows[0][0].ToString();
            }
            if (dt_ducan.Rows.Count > 0)
            {
                ducan = dt_ducan.Rows[0][0].ToString();
            }

            return "/" + ducan + "/" + blagajna;
        }

        private BackgroundWorker DWnadogradnja = new BackgroundWorker();
        private string oib_nadogradnja;

        public void ProvjeriNadogradnjuPremaOibu(string _oib_nadogradnja)
        {
            DWnadogradnja.WorkerReportsProgress = true;
            DWnadogradnja.WorkerSupportsCancellation = true;
            oib_nadogradnja = _oib_nadogradnja;
            DWnadogradnja.RunWorkerAsync();
        }


        private BackgroundWorker bw = new BackgroundWorker();

        private int razlika_sati;

        public void ProvjeriVrijemeUpozoriKorisnika(int _razlika_sati)
        {
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            razlika_sati = _razlika_sati;
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DateTime dat = ProvjeriVrijemeNaInternetu();
                //DateTime dat2 = GetFastestNISTDate();
                if (dat > DateTime.Now.AddHours(razlika_sati) || dat.AddHours(razlika_sati) < DateTime.Now)
                {
                    MessageBox.Show("Greška!\r\nNa računalu je postavljeno krivo vrijeme, izradom računa ako je krivi datum dolazi do problema sa usporedbom izvještaja i razlike datuma na poreznoj upravi.\r\n" +
                        "Ako želite sada promijeniti datum izđite iz programa i u donjem desnom kutu promjenite datum i vrijeme.", "Greška",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }
            catch
            {
            }
        }

        //public static DateTime GetFastestNISTDate () {
        //    var result = DateTime.MinValue;

        //    // Initialize the list of NIST time servers
        //    // http://tf.nist.gov/tf-cgi/servers.cgi
        //    string[] servers = new string[] {
        //        "hr.pool.ntp.org",
        //        "at.pool.ntp.org",
        //        "ba.pool.ntp.org ",
        //        "hu.pool.ntp.org",
        //    };

        //    // Try 5 servers in random order to spread the load
        //    Random rnd = new Random();
        //    //.OrderBy(s => rnd.NextDouble()).Take(5)
        //    foreach (string server in servers) {
        //        try {
        //            // Connect to the server (at port 13) and get the response
        //            string serverResponse = string.Empty;
        //            using (var reader = new StreamReader(new System.Net.Sockets.TcpClient(server, 13).GetStream())) {
        //                serverResponse = reader.ReadToEnd();
        //            }

        //            // If a response was received
        //            if (!string.IsNullOrEmpty(serverResponse)) {
        //                // Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
        //                string[] tokens = serverResponse.Split(' ');

        //                // Check the number of tokens
        //                if (tokens.Length >= 6) {
        //                    // Check the health status
        //                    string health = tokens[5];
        //                    if (health == "0") {
        //                        // Get date and time parts from the server response
        //                        string[] dateParts = tokens[1].Split('-');
        //                        string[] timeParts = tokens[2].Split(':');

        //                        // Create a DateTime instance
        //                        DateTime utcDateTime = new DateTime(
        //                            Convert.ToInt32(dateParts[0]) + 2000,
        //                            Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]),
        //                            Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
        //                            Convert.ToInt32(timeParts[2]));

        //                        // Convert received (UTC) DateTime value to the local timezone
        //                        result = utcDateTime.ToLocalTime();

        //                        return result;
        //                        // Response successfully received; exit the loop

        //                    }
        //                }

        //            }

        //        } catch {
        //            // Ignore exception and try the next server
        //        }
        //    }
        //    return result;
        //}

        public static void dodajIznosUBlagajnickiIzvjestaj(int broj_racuna, DateTime datum_racuna, decimal iznos)
        {
            try
            {
                string sql = string.Format(@"select * from blagajnicki_izvjestaj where cast(datum as date) = '{0}' and dokumenat = 'PROMET BLAGAJNE';",
                    datum_racuna.ToString("yyyy-MM-dd"));

                DataSet dsBlagajnicki = classSQL.select(sql, "blagajnicki_izvjestaj");
                if (dsBlagajnicki != null && dsBlagajnicki.Tables.Count > 0 && dsBlagajnicki.Tables[0] != null && dsBlagajnicki.Tables[0].Rows.Count > 0)
                {
                    int id = 0;
                    int.TryParse(dsBlagajnicki.Tables[0].Rows[0]["id"].ToString().Trim(), out id);
                    decimal blagajnicki_iznos = 0;
                    decimal.TryParse(dsBlagajnicki.Tables[0].Rows[0]["uplaceno"].ToString(), out blagajnicki_iznos);
                    string oznaka_dokumenta = broj_racuna.ToString();
                    if (dsBlagajnicki.Tables[0].Rows[0]["oznaka_dokumenta"].ToString().Trim().Length > 0)
                    {
                        if (dsBlagajnicki.Tables[0].Rows[0]["oznaka_dokumenta"].ToString().Trim().Contains("-"))
                        {
                            oznaka_dokumenta = dsBlagajnicki.Tables[0].Rows[0]["oznaka_dokumenta"].ToString().Trim().Split('-')[0] + "-" + broj_racuna.ToString();
                        }
                        else
                        {
                            oznaka_dokumenta = dsBlagajnicki.Tables[0].Rows[0]["oznaka_dokumenta"].ToString().Trim() + "-" + broj_racuna.ToString();
                        }
                    }

                    sql = string.Format(@"update blagajnicki_izvjestaj
set uplaceno = {0}, oznaka_dokumenta = '{1}', datum = '{2}', editirano = '1'
where id = {3};", (blagajnicki_iznos + iznos).ToString().Replace(',', '.'),
oznaka_dokumenta,
datum_racuna.ToString("yyyy-MM-dd HH:mm:ss"),
id);
                }
                else
                {
                    int rb = dohvatiZadnjiRedniBrojUBlagajnickomeIzvjestaju(true);

                    sql = string.Format(@"INSERT INTO blagajnicki_izvjestaj(
        rb, datum, dokumenat, oznaka_dokumenta, uplaceno, izdatak)
VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5});",
rb,
(datum_racuna).ToString("yyyy-MM-dd HH:mm:ss"),
"PROMET BLAGAJNE",
broj_racuna.ToString(),
iznos.ToString().Replace(',', '.'),
0.ToString().Replace(',', '.')
);
                }

                classSQL.update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int dohvatiZadnjiRedniBrojUBlagajnickomeIzvjestaju(bool uplata)
        {
            try
            {
                string sql = string.Format(@"select coalesce(max(rb), 0) zbroj 1 as rb
from blagajnicki_izvjestaj
where dokumenat not in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE', 'PROMET BLAGAJNE', 'PROMET BLAGAJNE - R');");
                if (uplata)
                {
                    sql = string.Format(@"select coalesce(max(rb), 0) zbroj 1 as rb
from blagajnicki_izvjestaj
where dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE', 'PROMET BLAGAJNE', 'PROMET BLAGAJNE - R');");
                }

                DataSet dsRb = classSQL.select(sql, "blagajnicki_izvjestaj");
                if (dsRb != null && dsRb.Tables.Count > 0 && dsRb.Tables[0] != null && dsRb.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(dsRb.Tables[0].Rows[0]["rb"]);
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public static string prefixBazeKojaSeKoristi()
        {
            string prefix = "db";
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = (from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c);
            foreach (XElement book in query)
            {
                string databaseName = book.Attribute("database").Value.ToString();
                string godinaKojaSeKoristi = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();
                if (databaseName.EndsWith(godinaKojaSeKoristi))
                {
                    prefix = databaseName.Substring(0, databaseName.Length - godinaKojaSeKoristi.Length);
                    break;
                }
            }
            return prefix;
        }

        public static DateTime ProvjeriVrijemeNaInternetu()
        {
            //default Windows time server
            //const string ntpServer = "time.windows.com";
            //const string ntpServer = "hr.pool.ntp.org";

            string[] servers = new string[] {
                "hr.pool.ntp.org",
                "0.europe.pool.ntp.org",
                "1.europe.pool.ntp.org",
                "2.europe.pool.ntp.org",
                "3.europe.pool.ntp.org",
            };

            foreach (string server in servers)
            {
                try
                {
                    // NTP message size - 16 bytes of the digest (RFC 2030)
                    var ntpData = new byte[48];

                    //Setting the Leap Indicator, Version Number and Mode values
                    ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                    var addresses = Dns.GetHostEntry(server).AddressList;

                    //The UDP port number assigned to NTP is 123
                    var ipEndPoint = new IPEndPoint(addresses[0], 123);
                    //NTP uses UDP
                    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 7000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();

                    //Offset to get to the "Transmit Timestamp" field (time at which the reply
                    //departed the server for the client, in 64-bit timestamp format."
                    const byte serverReplyTime = 40;

                    //Get the seconds part
                    ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                    //Get the seconds fraction
                    ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                    //Convert From big-endian to little-endian
                    intPart = SwapEndianness(intPart);
                    fractPart = SwapEndianness(fractPart);

                    var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                    //**UTC** time
                    var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

                    return networkDateTime.ToLocalTime();
                }
                catch
                {
                }
            }
            return DateTime.Now;
        }

        // stackoverflow.com/a/3294698/162671
        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        /// <summary>
        /// Nadograđuje program. Ako je varijabla sPorukom true, onda pita korisnika da li želi skinuti noviju verziju programa.
        /// </summary>
        /// <param name="sPorukom"></param>
        public static void NovijaInacica(bool sPorukom)
        {
            if (!sPorukom)
            {
                //Nadogradi();
                return;
            }

            if (MessageBox.Show("Na Internetu postoji novija inačica programa.\r\nŽelite li skinuti noviju verziju programa.", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               // Nadogradi();
            }
        }



        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        public static string VratiNacinPlacanja(string nazivPlacanja)
        {
            string np;

            switch (nazivPlacanja)
            {
                case "gotovina":
                    np = "G";
                    break;

                case "novčanice":
                    np = "G";
                    break;

                case "novčanica":
                    np = "G";
                    break;

                case "kartica":
                    np = "K";
                    break;

                case "kartice":
                    np = "K";
                    break;

                case "virman":
                    np = "T";
                    break;

                case "transakcijski račun":
                    np = "T";
                    break;

                case "transakcijski račun - kupon":
                    np = "O";
                    break;

                default:
                    np = "O";
                    break;
            }

            if (nazivPlacanja.Contains("transakcijski"))
            {
                if (nazivPlacanja.ToLower() != "transakcijski račun - kupon")
                    np = "T";
            }

            if (nazivPlacanja.Contains("kartic"))
            {
                np = "K";
            }

            if (nazivPlacanja.Contains("novčan"))
            {
                np = "G";
            }

            return np;
        }

        public static string UzmiOvlastTrenutnogZaposlenika()
        {
            DataTable DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];
            if (DTzaposlenik.Rows.Count > 0)
            {
                if (DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "1" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "2")
                {
                    return "user";
                }
                else
                {
                    return "admin";
                }
            }
            return "user";
        }

        public static void getOib()
        {
            string oib = "NemaOib-a";
            DataSet dsTvrtkaOib = classSQL.select_settings("select oib from podaci_tvrtka;", "podaci_tvrtka");
            if (dsTvrtkaOib != null && dsTvrtkaOib.Tables.Count > 0 && dsTvrtkaOib.Tables[0] != null && dsTvrtkaOib.Tables[0].Rows.Count > 0)
            {
                oib = dsTvrtkaOib.Tables[0].Rows[0]["oib"].ToString();
                //return dsTvrtkaOib.Tables[0].Rows[0]["oib"].ToString();
            }

            oibTvrtke = oib;
            //return oib;
        }

        public static string getPoslovnicaNaziv()
        {
            DataSet dsDefaultDucan = classSQL.select_settings("select default_ducan from postavke;", "postavke");
            if (dsDefaultDucan != null && dsDefaultDucan.Tables.Count > 0 && dsDefaultDucan.Tables[0] != null && dsDefaultDucan.Tables[0].Rows.Count > 0)
            {
                DataSet poslovnica = classSQL.select("select ime_ducana from ducan where id_ducan = '" + dsDefaultDucan.Tables[0].Rows[0]["default_ducan"].ToString() + "';", "ducan");
                if (poslovnica != null && poslovnica.Tables.Count > 0 && poslovnica.Tables[0] != null && poslovnica.Tables[0].Rows.Count > 0)
                {
                    return poslovnica.Tables[0].Rows[0]["ime_ducana"].ToString();
                }
            }

            return "NemaPoslovnice";
        }

        public static bool ZabranaUređivanjaDokumenta(int _broj_dana, DateTime _datum_dok, string _ovlast)
        {
            if (_ovlast == "sve" && _datum_dok.AddDays(_broj_dana) < DateTime.Now)
            {
                return true;
            }
            else if (_ovlast == "admin" && _datum_dok.AddDays(_broj_dana) < DateTime.Now)
            {
                return true;
            }
            else if (_ovlast == "user" && _datum_dok.AddDays(_broj_dana) < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ProvjeriRacuneOdZadnjeProvjere(DataTable DTpostavke)
        {
            if (File.Exists("ZadnjaProvjeraRacuna"))
            {
                string datum = File.ReadAllText("ZadnjaProvjeraRacuna");
                DateTime _dt_;
                DateTime.TryParse(datum, out _dt_);

                //OVO JE AKO SE IZBLESIRA DATUM
                if (_dt_.ToString() == "1.1.0001. 0:00:00")
                {
                    File.WriteAllText("ZadnjaProvjeraRacuna", DateTime.Now.AddDays(-2).ToString());
                    datum = File.ReadAllText("ZadnjaProvjeraRacuna");
                    DateTime.TryParse(datum, out _dt_);
                }

                synRacuni Racuni = new synRacuni(true, _dt_.AddHours(-1).ToString(""));
                Racuni.gasenje = true;
                if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    if (!Util.Korisno.RadimSinkronizaciju)
                    {
                        Util.Korisno.RadimSinkronizaciju = true;
                        Racuni.Send();
                        Util.Korisno.RadimSinkronizaciju = false;
                        File.WriteAllText("ZadnjaProvjeraRacuna", DateTime.Now.ToString());
                    }
                }
            }
            else
            {
                File.WriteAllText("ZadnjaProvjeraRacuna", DateTime.Now.AddDays(-2).ToString());
            }
        }

        public static bool CheckForInternetConnection(string urlForCheck = null)
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead((urlForCheck != null ? urlForCheck:"http://www.google.com")))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}