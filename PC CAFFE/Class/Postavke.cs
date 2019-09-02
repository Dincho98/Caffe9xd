using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Class
{
    public static class Registracija
    {
        private static string _productKey = "";
        public static string productKey { get { return _productKey; } }

        private static string _activationCode = "";
        public static string activationCode { get { return _activationCode; } }

        private static int _broj = 0;
        public static int broj { get { return _broj; } }

        public static void getPodaci()
        {
            try
            {
                DataSet ds = classSQL.select_settings("select * from registracija;", "registracija");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    _productKey = ds.Tables[0].Rows[0]["productKey"].ToString();
                    _activationCode = ds.Tables[0].Rows[0]["activationCode"].ToString();
                    _broj = Convert.ToInt32(ds.Tables[0].Rows[0]["broj"].ToString());
                }
            }
            catch
            {
            }
        }

        public static string GetMD5(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        public static string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private static string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private static string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }
    }

    public static class PodaciTvrtka
    {
        public static string nazivTvrtke { get; internal set; }
        public static string kratkiNazivTvrtke { get; internal set; }
        public static string adresaTvrtke { get; internal set; }
        public static int gradTvrtke { get; internal set; }
        public static string oibTvrtke { get; internal set; }
        public static string telefonTvrtke { get; internal set; }
        public static string faxTvrtke { get; internal set; }
        public static string mobitelTvrtke { get; internal set; }
        public static string vlasnikTvrtke { get; internal set; }
        public static string zrTvrtke { get; internal set; }
        public static string emailTvrtke { get; internal set; }
        public static string nazivPoslovnice { get; internal set; }
        public static string adresaPoslovnice { get; internal set; }
        public static int gradPoslovnicaId { get; internal set; }
        public static string iban { get; internal set; }
        public static string swift { get; internal set; }
        public static string pdvBr { get; internal set; }
        public static string naslovFakture { get; internal set; }
        public static string tipRacuna { get; internal set; }
        public static string naslovRacuna { get; internal set; }
        public static string napomenaVirman { get; internal set; }
        public static string textPocetakPosPrintera { get; internal set; }
        public static string textNaKrajuDokumenta { get; internal set; }
        public static string sifraPPMIPO { get; internal set; }
        //public static object gradPoslovnicaId { get; internal set; }

        public static void getPodaci()
        {
            try
            {
                DataSet ds = classSQL.select_settings("select * from podaci_tvrtka;", "podaci_tvrtka");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0] != null)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    nazivTvrtke = dr["ime_tvrtke"].ToString();
                    kratkiNazivTvrtke = dr["skracenoImeTvrtke"].ToString();
                    adresaTvrtke = dr["adresa"].ToString();
                    gradTvrtke = Convert.ToInt32(dr["id_grad"].ToString());
                    oibTvrtke = dr["oib"].ToString();
                    telefonTvrtke = dr["tel"].ToString();
                    faxTvrtke = dr["fax"].ToString();
                    mobitelTvrtke = dr["mob"].ToString();
                    vlasnikTvrtke = dr["vl"].ToString();
                    zrTvrtke = dr["zr"].ToString();
                    emailTvrtke = dr["email"].ToString();
                    nazivPoslovnice = dr["nazivPoslovnice"].ToString();
                    adresaPoslovnice = dr["poslovnica_adresa"].ToString();
                    gradPoslovnicaId = Convert.ToInt32((dr["poslovnica_grad"] != null && dr["poslovnica_grad"].ToString().Length > 0 ? dr["poslovnica_grad"].ToString() : "0"));
                    iban = dr["iban"].ToString();
                    swift = dr["swift"].ToString();
                    pdvBr = dr["pdvBr"].ToString();
                    naslovFakture = dr["naziv_fakture"].ToString();
                    tipRacuna = dr["r1"].ToString();
                    naslovRacuna = dr["naslov_racuna"].ToString();
                    napomenaVirman = dr["napomenaTransa"].ToString();
                    textPocetakPosPrintera = dr["skraceno_ime"].ToString();
                    textNaKrajuDokumenta = dr["text_bottom"].ToString();
                    sifraPPMIPO = dr["sifra_ppmipo"].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public static class Postavke
    {
        public const string OIB_PC1 = "47165970760";

        public static bool is_caffe { get; internal set; }
        public static bool skidaj_kolicinu_po_dokumentima { get; internal set; }
        public static bool is_beauty { get; internal set; }
        public static int id_default_skladiste { get; internal set; }
        public static int id_maloprodaja_naplatni_uredaj { get; internal set; }
        public static int maloprodaja_naplatni_uredaj { get; internal set; }
        public static int id_default_ducan { get; internal set; }
        public static string default_poslovnica { get; internal set; }
        public static int default_jezik { get; internal set; }

        public static Color backGround { get; set; }
        public static string gradientFrom { get; set; }
        public static string gradientTo { get; set; }

        public static bool UDSGame { get; internal set; }
        public static bool UDSGameEmployees { get; internal set; }
        public static string UDSGameApiKey { get; internal set; }
        public static bool TEST_FISKALIZACIJA { get; internal set; }

        private static bool _napomena_na_kraju_racuna = false;
        public static bool napomena_na_kraju_racuna { get { return _napomena_na_kraju_racuna; } }

        private static bool _napomena_na_kraju_predracuna = false;
        public static bool napomena_na_kraju_predracuna { get { return _napomena_na_kraju_predracuna; } }

        private static bool _rad_sa_tabletima = false;
        public static bool rad_sa_tabletima { get { return _rad_sa_tabletima; } }

        public static void getPodaci()
        {
            try
            {
                DataSet ds = classSQL.select_settings("select * from postavke;", "postavke");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0] != null)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    skidaj_kolicinu_po_dokumentima = Convert.ToBoolean(dr["skidaj_kolicinu_po_dokumentima"]);
                    is_caffe = Convert.ToBoolean(dr["is_caffe"]);
                    is_beauty = Convert.ToBoolean(dr["is_beauty"]);
                    if (is_caffe && is_beauty)
                        is_beauty = false;

                    id_default_skladiste = Convert.ToInt32(dr["default_skladiste"]);
                    id_maloprodaja_naplatni_uredaj = Convert.ToInt32(dr["default_blagajna"]);
                    maloprodaja_naplatni_uredaj = Convert.ToInt32(classSQL.select(string.Format("select ime_blagajne from blagajna where id_blagajna = {0};", id_maloprodaja_naplatni_uredaj), "blagajne").Tables[0].Rows[0]["ime_blagajne"]);
                    id_default_ducan = Convert.ToInt32(dr["default_ducan"]);
                    default_poslovnica = classSQL.select(string.Format("select ime_ducana from ducan where id_ducan = {0}", id_default_ducan), "ducan").Tables[0].Rows[0]["ime_ducana"].ToString();
                    default_jezik = Convert.ToInt32(dr["default_jezik"]);

                    if (ds.Tables[0].Columns.Contains("napomena_na_kraju_racuna"))
                    {
                        _napomena_na_kraju_racuna = Convert.ToBoolean(dr["napomena_na_kraju_racuna"]);
                    }

                    if (ds.Tables[0].Columns.Contains("napomena_na_kraju_predracuna"))
                    {
                        _napomena_na_kraju_predracuna = Convert.ToBoolean(dr["napomena_na_kraju_predracuna"]);
                    }

                    if (ds.Tables[0].Columns.Contains("rad_sa_tabletima")) {
                        _rad_sa_tabletima = Convert.ToBoolean(dr["rad_sa_tabletima"]);
                    }

                    backGround = Color.Silver;

                    bool defaultValue = false, useGradient = true;

                    if (defaultValue)
                    {
                        gradientFrom = "#EFEFF1";
                        gradientTo = "#BEC8D2";
                    }
                    else
                    {
                        if (useGradient)
                        {
                            gradientFrom = "#BEC8D2";
                            gradientTo = "#517487";
                        }
                        else
                        {
                            gradientFrom = "#5bc0de";
                            gradientTo = gradientFrom;
                        }
                    }

                    UDSGame = Convert.ToBoolean(dr["useUdsGame"]);
                    UDSGameEmployees = Convert.ToBoolean(dr["useUdsGameEmployees"]);
                    UDSGameApiKey = dr["useUdsGameApiKey"].ToString().Trim();

                    TEST_FISKALIZACIJA = Convert.ToBoolean(dr["test_fiskalizacija"]);

                    Class.PodaciZaSpajanjeCompaktna.getPodaci();
                }
            }
            catch (Exception)
            {
            }
        }

        public static void changeBackground(object sender, PaintEventArgs e)
        {
            
            try
            {
                int widthForm = (sender as Form).ClientRectangle.Width;
                int heightForm = (sender as Form).ClientRectangle.Height;
                if (widthForm != 0 && heightForm != 0)
                {
                    bool useImg = true;
                    using (TextureBrush tBrush = new TextureBrush(SetImageOpacity(Properties.Resources.sace_pattern, 0.05F), WrapMode.Tile))
                    using (LinearGradientBrush lbrush = new LinearGradientBrush((sender as Form).ClientRectangle, ColorTranslator.FromHtml(Class.Postavke.gradientFrom), ColorTranslator.FromHtml(Class.Postavke.gradientTo), LinearGradientMode.Vertical))
                    {
                        //lbrush.SetBlendTriangularShape(0.8f, 0.3f);

                        e.Graphics.FillRectangle(lbrush, (sender as Form).ClientRectangle);

                        if (useImg)
                            e.Graphics.FillRectangle(tBrush, (sender as Form).ClientRectangle);
                    }
                    //(sender as Form).BackgroundImage = Properties.Resources.gradient_1761190_960_720;
                    //(sender as Form).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private static Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default,
                                                  ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                                   0, 0, image.Width, image.Height,
                                   GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }
    }

    public static class PosPrint
    {
        private static bool _sortIspisNaMaliPrinter = true;
        public static bool sortIspisNaMaliPrinter { get { return _sortIspisNaMaliPrinter; } }

        private static bool _ispisUkupnoIspodStavkiMaliPrinter = false;
        public static bool ispisUkupnoIspodStavkiMaliPrinter { get { return _ispisUkupnoIspodStavkiMaliPrinter; } }

        private static float _fontUkupnoSizeMaliPrinter = 14;
        public static float fontUkupnoSizeMailiPrinter { get { return _fontUkupnoSizeMaliPrinter; } }

        public static void getPodaci()
        {
            try
            {
                if (Class.PodaciTvrtka.oibTvrtke == "74340789103") { _sortIspisNaMaliPrinter = false; }
                if (Class.PodaciTvrtka.oibTvrtke == "89780910318") { _ispisUkupnoIspodStavkiMaliPrinter = true; _fontUkupnoSizeMaliPrinter = 16; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static class PodaciZaSpajanjeCompaktna
    {
        private static string _remoteServer = "localhost";
        public static string remoteServer { get { return _remoteServer; } }
        private static string _remoteUsername = "postgres";
        public static string remoteUsername { get { return _remoteUsername; } }
        private static string _remotePort = "5432";
        public static string remotePort { get { return _remotePort; } }
        private static string _remoteDb = "db2018";
        public static string remoteDb { get { return _remoteDb; } }
        private static bool _aktivan = true;
        public static bool aktivan { get { return _aktivan; } }

        public static void getPodaci()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                _remoteServer = book.Attribute("server").Value;
                _remoteUsername = book.Attribute("username").Value;
                _remotePort = book.Attribute("port").Value;
                _remoteDb = book.Attribute("database").Value;

                if (book.Attribute("active").Value == "1")
                {
                    _aktivan = true;
                }
                else
                {
                    _aktivan = false;
                }
            }
        }
    }
}