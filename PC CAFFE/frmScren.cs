using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCPOS
{
    public partial class frmScren : Form
    {
        public frmScren()
        {
            InitializeComponent();


            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmMenu MainForm { get; set; }
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private string id_kasa;
        private string id_ducan;
        public static string verzijaProgramaZaRacun = "6.870";

        public static bool neuspjelaFiskalizacijaPostoji;
        public static DateTime datumPrveNeuspjeleFiskalizacije;
        public static DateTime datumZadnjeMoguceFiskalizacije;
        public static DateTime datumPrvogRacunaSGreskom;
        public static DateTime datumZadnjeMoguceFiskalizacijeRacunaSGreskom;
        public static DateTime danasnjiDatum;

        private void frmScren_Load(object sender, EventArgs e)
        {

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            id_kasa = DTpostavke.Rows[0]["default_blagajna"].ToString();
            id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();

            try
            {
                //Preuzmi last version.txt
                GetTxtLastVersion(); // AKO SE PROGRAM SPORO OTVARA, TREBA ZAKOMENTIRATI OVU LINIJU, NO TADA NADOGRADNJA NECE BITI MOGUCA!
                                     // TO JE ZATO JER PLEXY NE RADI ILI U TOM TRENTKU IZGUBI KONEKCIJU!
                string lastVersion;
                string currentPathLastVersion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"lastVersion.txt");
                using (StreamReader reader = new StreamReader(currentPathLastVersion))
                {
                    lastVersion = reader.ReadLine();
                }


                string currentVersion = "Potrebna nova verzija.";
                string currentPathCurrentVersion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"currentVersion.txt");

                if (File.Exists(currentPathCurrentVersion))
                {
                    using (StreamReader reader = new StreamReader(currentPathCurrentVersion))
                    {
                        currentVersion = reader.ReadLine();
                        verzijaProgramaZaRacun = currentVersion;
                    }
                }

                if (!lastVersion.Equals(currentVersion))
                {
                    currentVersion = "Potrebna nova verzija.";
                }

                //label4.Text = "Verzija programa: " + Properties.Settings.Default.verzija_programa;
                label4.Text = "Verzija programa: " + currentVersion;
                PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                int trenutnaG = B.UzmiGodinuKojaSeKoristi();

                if (trenutnaG == DateTime.Now.Year)
                {
                    lblTrenutnaGodina.ForeColor = Color.White;
                    timerUpozoranaNaKrivuGodinu.Stop();
                }
                else
                {
                    lblTrenutnaGodina.ForeColor = Color.Red;
                    timerUpozoranaNaKrivuGodinu.Interval = 500;
                    timerUpozoranaNaKrivuGodinu.Start();
                }
                lblTrenutnaGodina.Text = "Trenutno koristite " + trenutnaG + " g:";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                timer2.Start();
                SetMoneyValue();
                timer1.Start();
                PieStart(10);
                btnMaloprodaja.Select();
                txtBrojDana.Text = "10";

                //Postoje neuspjele fiskalizacije
                timer3.Start();
                timer4.Start();
                neuspjelaFiskalizacijaPostoji = false;
                ProvjeriPostojiLiNeuspjelaFiskalizacija();

                if (neuspjelaFiskalizacijaPostoji)
                {
                    danasnjiDatum = DateTime.Now;
                    var preostaloVrijemeFiskalizacije = (datumZadnjeMoguceFiskalizacije - danasnjiDatum).TotalHours;
                    labelPreostaloVrijeme.Text = "Preostalo vrijeme: " + preostaloVrijemeFiskalizacije.ToString("#0.00h");
                }
                else
                {
                    labelPostojeNeuspjeleFiskalizacije.Visible = false;
                    labelPreostaloVrijeme.Visible = false;
                    labelHitnoNazoviteCodeIt.Visible = false;
                }
            }
            catch
            {
            }
        }

        public static void ProvjeriPostojiLiNeuspjelaFiskalizacija()
        {
            string sql = "SELECT * FROM neuspjela_fiskalizacija";
            DataTable DTNeuspjeleFiskalizacije = classSQL.select(sql, "neuspjela_fiskalizacija").Tables[0];
            string broj_rac = "";
            string query = @"SELECT
                            racuni.broj_racuna,
                            racuni.datum_racuna,
                            ducan.ime_ducana,
                            racuni.id_ducan,
                            blagajna.ime_blagajne,
                            racuni.nacin_placanja,
                            racuni.id_blagajnik,
                            racuni.id_kasa,
                            racuni.godina,
                            SUM(
                            CAST(mpc AS NUMERIC)*
                            CAST(REPLACE(kolicina,',','.') AS NUMERIC)
                            ) AS ukupno
                            FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                            LEFT JOIN ducan ON ducan.id_ducan=racuni.id_ducan
                            LEFT JOIN blagajna ON blagajna.id_blagajna=racuni.id_kasa
                            WHERE ((jir='' AND zik='') OR (jir IS NULL AND zik IS NULL)) " + broj_rac + @" AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + @"' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + @"'
                            GROUP BY racuni.broj_racuna,racuni.godina,racuni.datum_racuna,ducan.ime_ducana,blagajna.ime_blagajne,
                            racuni.id_blagajnik,racuni.nacin_placanja,racuni.id_ducan,racuni.id_kasa,racuni.godina
                            ORDER BY racuni.datum_racuna ASC;";
            DataTable DTRacuniSGreskom = classSQL.select(query, "racuni").Tables[0];

            //Ako se sfiskalizira i zapis nestane s tablice neuspjela_fiskalizacija
            //ne bude se updatealo, zato jer su varijable i dalje imaju one stare vrijednosti.
            //To treba samo tu napraviti na frmScren, jer na frmCaffe je nemoguce naknadno fiskalizirati racun.
            if (DTNeuspjeleFiskalizacije.Rows.Count > 0 || DTRacuniSGreskom.Rows.Count > 0)
            {
                neuspjelaFiskalizacijaPostoji = true;
                datumPrveNeuspjeleFiskalizacije = DateTime.MaxValue;
                datumPrvogRacunaSGreskom = DateTime.MaxValue;
                datumZadnjeMoguceFiskalizacije = DateTime.MaxValue;
                datumZadnjeMoguceFiskalizacijeRacunaSGreskom = DateTime.MaxValue;

                if (DTNeuspjeleFiskalizacije.Rows.Count > 0)
                {
                    datumPrveNeuspjeleFiskalizacije = Convert.ToDateTime(DTNeuspjeleFiskalizacije.Rows[0]["date"].ToString());
                    datumZadnjeMoguceFiskalizacije = datumPrveNeuspjeleFiskalizacije.AddDays(2);
                }

                if (DTRacuniSGreskom.Rows.Count > 0)
                {
                    datumPrvogRacunaSGreskom = Convert.ToDateTime(DTRacuniSGreskom.Rows[0]["datum_racuna"].ToString());
                    datumZadnjeMoguceFiskalizacijeRacunaSGreskom = datumPrvogRacunaSGreskom.AddDays(2);
                }

                if (datumZadnjeMoguceFiskalizacijeRacunaSGreskom.CompareTo(datumZadnjeMoguceFiskalizacije) == -1)
                    datumZadnjeMoguceFiskalizacije = datumZadnjeMoguceFiskalizacijeRacunaSGreskom;
            }
            else
            {
                neuspjelaFiskalizacijaPostoji = false;
            }
        }

        private void GetTxtLastVersion()
        {
            FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create($"ftp://5.189.154.50/CodeCaffe/lastVersion.txt");
            requestDir.Credentials = new NetworkCredential("codeadmin", "Eqws64%2");
            requestDir.Timeout = 5000;
            //Ako se nakon 5 sekundi ne skine file -> npr plesk steka, izgubi mu se konekcija, tada se program pokrece automatski, no 
            //tada nije moguce updateati program jer nije preuzeta lastVersion.txt
            try
            {
                WebResponse response = requestDir.GetResponse();
                string fileName = @"lastVersion.txt";
                string url = $"ftp://5.189.154.50/CodeCaffe/{fileName}";
                using (WebClient req = new WebClient())
                {
                    req.Credentials = new NetworkCredential("codeadmin", "Eqws64%2");
                    byte[] fileData = req.DownloadData(url);

                    using (FileStream file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{fileName}")))
                    {
                        file.Write(fileData, 0, fileData.Length);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ako trenutno postoji nova verzija, nemoguće je updateati program.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            /*
             string fileName = @"lastVersion.txt";
                string url = $"ftp://5.189.154.50/CodeCaffe/{fileName}";
                using (WebClient req = new WebClient())
                {
                    req.Credentials = new NetworkCredential("codeadmin", "Eqws64%2");
                    byte[] fileData = req.DownloadData(url);

                    using (FileStream file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"{fileName}")))
                    {
                        file.Write(fileData, 0, fileData.Length);
                    }
                }
                */
        }

        private void timerUpozoranaNaKrivuGodinu_Tick(object sender, EventArgs e)
        {
            if (lblTrenutnaGodina.Visible == false)
            {
                lblTrenutnaGodina.Visible = true;
            }
            else
            {
                lblTrenutnaGodina.Visible = false;
            }
        }

        private string OO(string s)
        {
            try
            {
                return Convert.ToDecimal(s).ToString("#0.00");
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private string SetValue()
        {
            string html = "";
            try
            {
                string sql_liste = "SELECT " +
                    " racun_stavke.sifra_robe AS sifra," +
                    " SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) AS kolicina," +
                    " roba.naziv AS naziv " +
                    " FROM racun_stavke" +
                    " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                    " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                    " WHERE  racuni.datum_racuna>'" + DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + "' AND racuni.datum_racuna<'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                    " AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'" +
                    " GROUP BY racun_stavke.sifra_robe,roba.naziv";

                DataTable DT = classSQL.select(sql_liste, "roba").Tables[0];

                foreach (DataRow row in DT.Rows)
                {
                    html += "<tr style=\" font-size:12px; font-family:Arial, Helvetica, sans-serif;\">\n<td>" + row["sifra"].ToString() + "</td>" +
                        "<td>" + row["naziv"].ToString() + "</td>\n" +
                        "<td>" + row["kolicina"].ToString() + "</td>\n" +
                        "</tr>";
                }
            }
            catch (Exception) { }

            return html;
        }

        private string UzmiPredracune()
        {
            string html = "";
            DataTable DT = classSQL.select("SELECT  SUM(mpc) AS ukupno FROM svi_predracuni WHERE CAST(datum_ispisa as DATE) = '" + DateTime.Now.ToString() + "'", "svi_predracuni").Tables[0];

            decimal __uk;

            if (DT.Rows.Count > 0)
            {
                decimal.TryParse(DT.Rows[0][0].ToString(), out __uk);
                html += "<div>" +
                    "UKUPNO PREDRAČUNI: " + Math.Round(__uk, 3).ToString("#0.00") + " kn" +
                "</div><br/><br/>";
            }

            return html;
        }

        private void Upload(string fakture, string maloprodaja, string kartice, string gotovina)
        {
            try
            {
                string html = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/>\n" +
                    "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\n" +
                    "</head>\n" +
                    "<body style=\"\">\n" +
                    "<script>function prikaz(){document.getElementById('tablica').style.visibility='visible';}</script>\n" +
                    "<div style=\"  text-align:left; width:300px; margin-top:20px; font-family:Tahoma, Geneva, sans-serif; font-size:16px;\">\n" +
                    "<div style=\" font-size:20px; font-weight:bold;\">Stanje prodaje:</div><br/>\n" +
                    "<div>" + maloprodaja + "</div>\n" +
                    "<div>" + gotovina + "</div>\n" +
                    "<div>" + kartice + "</div>\n" +
                    "<div>" + fakture + "</div>\n" +
                    UzmiPredracune() +
                    "<input type=\"button\" onClick=\"prikaz();\" style=\"width:200px; height:40px;\" value=\"Promet po robi\"/>\n" +
                    "<br/><br/><br/>\n<table id='tablica' border=\"1px\" style=\"width:700px;  visibility:hidden;\">\n<tr style=\" background-color:#666; font-weight:bold; color:#FFF;\">\n<td style=\"width:140px; padding:5px;\">Šifra</td><td>Naziv</td>\n<td style=\"width:50px;\">Kol</td></tr>\n" +
                    SetValue() +
                    "</table>" +
                    "</div>" +
                    "</body>" +
                    "</html>";
                File.WriteAllText("index.html", html);

                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(DTpostavke.Rows[0]["salji_na_web_ftp"].ToString());
                requestFTPUploader.Credentials = new NetworkCredential(DTpostavke.Rows[0]["salji_na_web_user"].ToString(), DTpostavke.Rows[0]["salji_na_web_pass"].ToString());
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

                FileInfo fileInfo = new FileInfo("index.html");
                FileStream fileStream = fileInfo.OpenRead();

                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];

                Stream uploadStream = requestFTPUploader.GetRequestStream();
                int contentLength = fileStream.Read(buffer, 0, bufferLength);

                while (contentLength != 0)
                {
                    uploadStream.Write(buffer, 0, contentLength);
                    contentLength = fileStream.Read(buffer, 0, bufferLength);
                }

                uploadStream.Close();
                fileStream.Close();

                requestFTPUploader.Abort();
                requestFTPUploader = null;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void PieStart(int br)
        {
            //Random random = new Random();
            Chart1.Series["Series1"].Points.Clear();

            for (int i = 1; i < br + 1; i++)
            {
                DataTable DT = new DataTable();
                DataTable DT_fak = new DataTable();

                string sql = "SELECT SUM(ukupno) AS [ukupno] FROM racuni " +
                    " WHERE datum_racuna>'" + DateTime.Today.AddDays((i - br)).ToString("yyyy-MM-dd H:mm:ss") + "' " +
                    " AND datum_racuna<'" + DateTime.Today.AddDays((i - br) + 1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'";
                DT = classSQL.select(sql, "racuni").Tables[0];

                string sql_fak = "SELECT SUM(ukupno) AS [ukupno] FROM fakture " +
                " WHERE date>'" + DateTime.Today.AddDays((i - br)).ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND date<'" + DateTime.Today.AddDays((i - br) + 1).ToString("yyyy-MM-dd H:mm:ss") + "' " +
                "";
                DT_fak = classSQL.select(sql_fak, "racuni").Tables[0];

                double sveM = 0;

                if (DT.Rows[0]["ukupno"].ToString() != "")
                {
                    if (cbMaloprodaja.Checked)
                        sveM = Convert.ToDouble(DT.Rows[0]["ukupno"].ToString());
                }

                if (DT_fak.Rows[0]["ukupno"].ToString() != "")
                {
                    if (chbFakture.Checked)
                        sveM = Convert.ToDouble(DT_fak.Rows[0]["ukupno"].ToString()) + sveM;
                }

                Chart1.Series["Series1"].Points.AddY(sveM.ToString("#0.00").Replace(",", "."));

                if (sveM != 0)
                {
                    Chart1.Series["Series1"].Points[i - 1].MarkerStyle = MarkerStyle.Circle;
                    Chart1.Series["Series1"].Points[i - 1].MarkerSize = 5;
                    if (sveM > 0)
                        Chart1.Series["Series1"].Points[i - 1].MarkerColor = Color.Green;
                    else
                        Chart1.Series["Series1"].Points[i - 1].MarkerColor = Color.Red;
                }
            }

            Chart1.Series["Series1"].ChartType = SeriesChartType.SplineArea;
            Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(106, 86, 5);
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(106, 86, 5);
            Chart1.Series["Series1"].BorderColor = Color.FromArgb(31, 53, 79);
            Chart1.Series["Series1"].Color = Color.FromArgb(128, 31, 53, 79);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        public void SetMoneyValue()
        {
            try
            {
                DataTable DT = new DataTable();
                DataTable DT3 = new DataTable();

                string sql = "SELECT SUM(ukupno) AS [ukupno],SUM(ukupno_gotovina) AS [gotovina]," +
                    " SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
                    " WHERE datum_racuna>'" + DateTime.Today.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " AND datum_racuna<'" + DateTime.Today.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'";

                DT = classSQL.select(sql, "racuni").Tables[0];

                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
                {
                    label8.Text = "Maloprodaja ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["ukupno"].ToString())) + " kn";
                    label10.Text = "Blagajna gotovina: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString())) + " kn";
                    label11.Text = "Kartice ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["kartice"].ToString())) + " kn";
                }
                else
                {
                    label8.Text = "Maloprodaja ukupno: 0.00 kn";
                    label10.Text = "Blagajna gotovina: 0.00 kn";
                    label11.Text = "Kartice ukupno: 0.00 kn";
                }

                /*string sql3 = "SELECT SUM(ukupno) AS [Ukupno] FROM fakture " +
                    " WHERE date>'" + DateTime.Today.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND date<'" + DateTime.Today.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    "";
                DT3 = classSQL.select(sql3, "fakture").Tables[0];
                if (DT3.Rows.Count > 0 && DT3.Rows[0][0].ToString() != "")
                {
                    label9.Text = "Fakture ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT3.Rows[0][0].ToString())) + " kn";
                }
                else
                {
                    label9.Text = "Fakture ukupno: 0.00 kn";
                }*/

                if (DTpostavke.Rows[0]["salji_na_web"].ToString() == "1")
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                DataTable DT = new DataTable();
                DataTable DT3 = new DataTable();
                string sql = "SELECT SUM(ukupno) AS [ukupno],SUM(ukupno_gotovina) AS [gotovina],SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
                " WHERE datum_racuna>'" + monthCalendar1.SelectionStart.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND datum_racuna<'" + monthCalendar1.SelectionStart.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'";

                DT = classSQL.select(sql, "racuni").Tables[0];
                //MessageBox.Show(monthCalendar1.SelectionStart.ToString());

                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
                {
                    label8.Text = "Maloprodaja ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["ukupno"].ToString())) + " kn";
                    label10.Text = "Blagajna gotovina: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString())) + " kn";
                    label11.Text = "Kartice ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["kartice"].ToString())) + " kn";
                }
                else
                {
                    label8.Text = "Maloprodaja ukupno: 0.00 kn";
                    label10.Text = "Blagajna gotovina: 0.00 kn";
                    label11.Text = "Kartice ukupno: 0.00 kn";
                }

                string sql3 = "SELECT SUM(ukupno) AS [Ukupno] FROM fakture " +
                " WHERE date>'" + monthCalendar1.SelectionStart.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND date<'" + monthCalendar1.SelectionStart.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                "";
                DT3 = classSQL.select(sql3, "fakture").Tables[0];
                if (DT3.Rows.Count > 0 && DT3.Rows[0][0].ToString() != "")
                {
                    label9.Text = "Fakture ukupno: " + String.Format("{0:0.00}", Convert.ToDouble(DT3.Rows[0][0].ToString())) + " kn";
                }
                else
                {
                    label9.Text = "Fakture ukupno: 0.00 kn";
                }
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void picMaloprodaj_Click(object sender, EventArgs e)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmKasa))
                {
                    OpenForm.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            Caffe.frmCaffe ks = new Caffe.frmCaffe();
            ks.MainForm = MainForm;
            //ks.BringToFront();
            ks.Show();
        }

        private void picKalk_Click(object sender, EventArgs e)
        {
            Robno.frmPrimka nova_kalkulacija = new Robno.frmPrimka();
            nova_kalkulacija.MdiParent = MainForm;
            nova_kalkulacija.Dock = DockStyle.Fill;
            nova_kalkulacija.MainForm = MainForm;
            nova_kalkulacija.Show();
        }

        private void picFak_Click(object sender, EventArgs e)
        {
            Kasa.frmSviRacuni f = new Kasa.frmSviRacuni();
            //f.MdiParent = MainForm;
            //f.Dock = DockStyle.Fill;
            //f.MainForm = MainForm;
            f.Show();
        }

        private void picPonude_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajniArtikli f = new Caffe.frmProdajniArtikli();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainForm = MainForm;
            f.Show();
        }

        private void picRoba_Click(object sender, EventArgs e)
        {
            Caffe.frmRepromaterijal f = new Caffe.frmRepromaterijal();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainFormMenu = MainForm;
            f.Show();
        }

        private void picPartner_Click(object sender, EventArgs e)
        {
            Sifarnik.frmAddPartners f = new Sifarnik.frmAddPartners();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainFormMenu = MainForm;
            f.Show();
        }

        private void picOtpremnica_Click(object sender, EventArgs e)
        {
            frmOtpremnica f = new frmOtpremnica();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainForm = MainForm;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMoneyValue();
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SetMoneyValue();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetMoneyValue();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Caffe.frmPrijava p = new Caffe.frmPrijava();
            //MainForm.Hide();
            //p.MainForm = MainForm;
            //p.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime _datum = DateTime.Now;
            lblSat.Text = _datum.ToString("H:mm:ss");
            lblDatum.Text = _datum.ToString("dd.MM.yyyy.");
        }

        //Update vrijednosti svakih 10 sec.
        private void timer3_Tick_1(object sender, EventArgs e)
        {
            if (neuspjelaFiskalizacijaPostoji)
            {
                //Koji je danasnji datum i vrijeme?
                danasnjiDatum = DateTime.Now;
                var preostaloVrijemeFiskalizacije = (datumZadnjeMoguceFiskalizacije - danasnjiDatum).TotalHours;
                labelPostojeNeuspjeleFiskalizacije.Visible = true;
                labelPreostaloVrijeme.Visible = true;
                labelHitnoNazoviteCodeIt.Visible = true;
                labelPreostaloVrijeme.Text = "Preostalo vrijeme: " + preostaloVrijemeFiskalizacije.ToString("#0.00h");
                //Postavi labele na visible
            }
            else
            {
                //Postavi labele na non-visible
                labelPostojeNeuspjeleFiskalizacije.Visible = false;
                labelPreostaloVrijeme.Visible = false;
                labelHitnoNazoviteCodeIt.Visible = false;
            }
        }

        //Provjera ako postoji neuspjela fiskalizacija gleda se svakih 10 min
        private void timer4_Tick(object sender, EventArgs e)
        {
            ProvjeriPostojiLiNeuspjelaFiskalizacija();
        }

        private void txtBrojDana_TextChanged(object sender, EventArgs e)
        {
            if (txtBrojDana.Text != "" || txtBrojDana.Text == "0")
            {
                int broj = 0;
                if (!int.TryParse(txtBrojDana.Text, out broj))
                {
                    MessageBox.Show("Greška kod upisa.", "Greška");
                    return;
                }
                PieStart(broj);
            }
        }

        private void cbMaloprodaja_CheckedChanged(object sender, EventArgs e)
        {
            PieStart(Convert.ToInt16(txtBrojDana.Text));
        }

        private void chbFakture_CheckedChanged(object sender, EventArgs e)
        {
            PieStart(Convert.ToInt16(txtBrojDana.Text));
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Upload(label9.Text, label8.Text, label11.Text, label10.Text);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //try {
            //    string _path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "help1.exe";
            //    if (!File.Exists(_path)) {
            //        SkidajPodrsku();
            //    }
            //    System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //    proc.StartInfo.WorkingDirectory = _path;
            //    proc.StartInfo.FileName = _path;
            //    proc.Start();
            //} catch {
            //    MessageBox.Show("Spajanje na Power Computers nije uspjelo!", "Upozorenje!");
            //}
        }

        static public bool Check()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private static void SkidajPodrsku()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\help1.exe";

            if (System.IO.File.Exists(path))
            {
                return;
            }

            if (!Check())
            {
                MessageBox.Show("Pokušaj skidanja datoteke s Interneta nije uspio jer niste spojeni na Internet. " +
                    "Provjerite svoju Internet konekciju.",
                    "Upozorenje!");
                return;
            }

            MessageBox.Show("Slijedi skidanje programa kojim ćete se moći spojiti na Code-iT...");

            string sUrlToDnldFile;
            sUrlToDnldFile = "http://www.pc1.hr/pcpos/update/help1.exe";

            bool status = false;

            try
            {
                Uri url = new Uri(sUrlToDnldFile);
                string sFileSavePath = "";
                string sFileName = System.IO.Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\help.exe";

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                response.Close();

                // gets the size of the file in bytes

                long iSize = response.ContentLength;

                // keeps track of the total bytes downloaded so we can update the progress bar

                long iRunningByteTotal = 0;

                System.Net.WebClient client = new System.Net.WebClient();

                System.IO.Stream strRemote = client.OpenRead(url);

                System.IO.FileStream strLocal = new System.IO.FileStream(sFileSavePath,
                    System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);

                int iByteSize = 0;

                byte[] byteBuffer = new byte[1024];

                while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    // write the bytes to the file system at the file path specified

                    strLocal.Write(byteBuffer, 0, iByteSize);

                    iRunningByteTotal += iByteSize;

                    // calculate the progress out of a base "100"

                    double dIndex = (double)(iRunningByteTotal);

                    double dTotal = (double)iSize;

                    double dProgressPercentage = (dIndex / dTotal);

                    int iProgressPercentage = (int)(dProgressPercentage * 100);

                    // update the progress bar

                    //bgWorker1.ReportProgress(iProgressPercentage);
                }

                strRemote.Close();
                strLocal.Flush();
                strLocal.Close();

                System.IO.File.Copy(sFileSavePath, path);

                MessageBox.Show("Datoteka uspješno skinuta!", "");

                status = true;
            }
            catch (Exception exM)
            {
                MessageBox.Show("Pokušaj skidanja datoteke s Interneta nije uspio.\n\n" +
                exM.Message, "Upozorenje!");
                status = false;
            }

            return;
        }

        private void btnMaloprodaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Caffe.frmPrijava p = new Caffe.frmPrijava();
                MainForm.Hide();
                p.MainForm = MainForm;
                p.ShowDialog();
            }
            if (e.KeyData == Keys.Enter)
            {
                foreach (Form OpenForm in Application.OpenForms)
                {
                    if (OpenForm.GetType() == typeof(frmKasa))
                    {
                        OpenForm.WindowState = FormWindowState.Maximized;
                        return;
                    }
                }

                Caffe.frmCaffe ks = new Caffe.frmCaffe();
                ks.MainForm = MainForm;
                ks.Show();
            }
        }

        private void btnPromjenaG_Click(object sender, EventArgs e)
        {
            PCPOS.Until.frmPromjenaGodine pg = new Until.frmPromjenaGodine();
            pg.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //Until.frmHtmlInfo iss = new Until.frmHtmlInfo();
            //iss.Show();
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            Global.GlobalFunctions.BackupDatabase();
            Caffe.frmPrijava p = new Caffe.frmPrijava();
            MainForm.Hide();
            p.MainForm = MainForm;
            p.ShowDialog();
        }

        private void btnPodrska_Click(object sender, EventArgs e)
        {
            /*try
            {
                string _path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "help1.exe";
                if (!File.Exists(_path))
                {
                    SkidajPodrsku();
                }
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = _path;
                proc.StartInfo.FileName = _path;
                proc.Start();
            }
            catch
            {
                MessageBox.Show("Spajanje na Code-iT nije uspjelo!", "Upozorenje!");
            }*/
            FormPodrska form = new FormPodrska();
            form.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            /*Until.frmHtmlInfo iss = new Until.frmHtmlInfo();
            iss.Show();*/
            frmNewInfo frmNewInfoX = new frmNewInfo();
            frmNewInfoX.ShowDialog();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void btnFakture_Click(object sender, EventArgs e)
        {
            try
            {
                frmFaktura f = new frmFaktura();
                f.MdiParent = this.MdiParent;
                f.Dock = DockStyle.Fill;
                f.MainForm = this.MainForm;
                f.Show();
            }
            catch
            {
                frmFaktura f = new frmFaktura();
                f.ShowDialog();
            }
        }

        private void kalkulacijeButton_Click(object sender, EventArgs e)
        {
            Robno.frmKalkulacija form = new Robno.frmKalkulacija();
            form.MdiParent = this.MdiParent;
            form.MainForm = this.MainForm;
            form.Dock = DockStyle.Fill;
            form.Show();
        }

    }
}