using Npgsql;
using PCPOS.Sinkronizacija;
using PCPOS.Until;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmPostavke : Form
    {
        public frmPostavke()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet dt = new DataSet();
        private DataTable DTpostavke;

        private bool loaded = false;

        private void frmPostavke_Load(object sender, EventArgs e)
        {
            DataTable dtDopustenje = classSQL.select("select id_dopustenje from zaposlenici where id_zaposlenik = '" + Properties.Settings.Default.id_zaposlenik + "';", "zaposlenici").Tables[0];
            if (dtDopustenje != null && dtDopustenje.Rows.Count > 0 && Convert.ToInt32(dtDopustenje.Rows[0][0]) == 2)
            {
                button4.Visible = true;
                //grbPreuzmiSveWeb.Visible = true;
            }
            else
            {
                button4.Visible = false;
                //grbPreuzmiSveWeb.Visible = true;
            }

            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            SetRemoteFields();
            SetComboBox();
            //SetNagradivanje();
            backgroundWorker1.RunWorkerAsync();
            SetDatabase();
            SetFiskal();
            BackupBaze();
            Magnetska_kartica();
            WebPogled();
            cbSustavPDV.SelectedValue = DTpostavke.Rows[0]["sustav_pdv"].ToString();
            SetJezikComboBox();

            LOADChbNaciniPlacanja();
            LoadZadnjiRacun();

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            if (DTpostavke.Rows[0]["zabraniUvidSkladiste"].ToString() == "0")
            {
                chbZabraniUvidStanjeSkl.Checked = false;
            }
            else if (DTpostavke.Rows[0]["zabraniUvidSkladiste"].ToString() == "1")
            {
                chbZabraniUvidStanjeSkl.Checked = true;
            }
            if (DTpostavke.Rows[0]["zaposlenici_vide_samo_danasnju_prodaju"].ToString() == "1")
            {
                chbSamoDanasnjaProdaja.Checked = true;
            }
            else
            {
                chbSamoDanasnjaProdaja.Checked = false;
            }

            if (DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "0")
            {
                chbPosaljiUkuhinju.Checked = false;
            }
            else if (DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "1")
            {
                chbPosaljiUkuhinju.Checked = true;
            }

            if (DTpostavke.Rows[0]["print_narudzbe_bez_pitanja"].ToString() == "0")
            {
                checkBoxSaljiNarudzbeAutomatski.Checked = false;
            }
            else if (DTpostavke.Rows[0]["print_narudzbe_bez_pitanja"].ToString() == "1")
            {
                checkBoxSaljiNarudzbeAutomatski.Checked = true;
            }

            try
            {
                chbIspisNaKopijiRacuna.Checked = Convert.ToBoolean(Convert.ToInt32(DTpostavke.Rows[0]["ispis_na_kopiji_racuna"]));
            }
            catch (Exception)
            {
            }

            if (DTpostavke.Rows[0]["samo_prodaja"].ToString() == "0")
            {
                chbDodatnaDopustenja.Checked = false;
            }
            else if (DTpostavke.Rows[0]["samo_prodaja"].ToString() == "1")
            {
                chbDodatnaDopustenja.Checked = true;
            }

            if (DTpostavke.Rows[0]["upozori_iskljucenu_fiskalizaciju"].ToString() == "0")
            {
                chbFiskalizacijaIskljucena.Checked = false;
            }
            else if (DTpostavke.Rows[0]["upozori_iskljucenu_fiskalizaciju"].ToString() == "1")
            {
                chbFiskalizacijaIskljucena.Checked = true;
            }

            if (DTpostavke.Rows[0]["koristi_popust_prema_stavki"].ToString() == "0")
            {
                chbPopust.Checked = false;
            }
            else if (DTpostavke.Rows[0]["koristi_popust_prema_stavki"].ToString() == "1")
            {
                chbPopust.Checked = true;
            }

            if (DTpostavke.Rows[0]["stolovi_razvrstavanje"].ToString() == "0")
            {
                chbStoloviRaspored.Checked = false;
                btnUrediStolove.Enabled = false;
            }
            else if (DTpostavke.Rows[0]["stolovi_razvrstavanje"].ToString() == "1")
            {
                chbStoloviRaspored.Checked = true;
                btnUrediStolove.Enabled = true;
            }

            if (DTpostavke.Rows[0]["skidaj_sa_predracuna"].ToString() == "0")
            {
                chbSkidajSaPredracuna.Checked = false;
            }
            else if (DTpostavke.Rows[0]["skidaj_sa_predracuna"].ToString() == "1")
            {
                chbSkidajSaPredracuna.Checked = true;
            }

            //if (DTpostavke.Rows[0]["is_caffe"].ToString() == "0")
            //{
            //    chbUgo.Checked = false;
            //}
            //else if (DTpostavke.Rows[0]["is_caffe"].ToString() == "1")
            //{
            //    chbUgo.Checked = true;
            //}

            chbUgo.Checked = Class.Postavke.is_caffe;
            chbBeauty.Enabled = !chbUgo.Checked;
            chbBeauty.Checked = Class.Postavke.is_beauty;

            if (DTpostavke.Rows[0]["zabrana_zaposleniku_da_vidi_druge_promete"].ToString() == "0")
            {
                chbZaposleniciVideSamoSvojuProdaju.Checked = false;
            }
            else if (DTpostavke.Rows[0]["zabrana_zaposleniku_da_vidi_druge_promete"].ToString() == "1")
            {
                chbZaposleniciVideSamoSvojuProdaju.Checked = true;
            }

            if (DTpostavke.Rows[0]["kartica_kupca"].ToString() == "0")
            {
                chkKarticaKupca.Checked = false;
            }
            else if (DTpostavke.Rows[0]["kartica_kupca"].ToString() == "1")
            {
                chkKarticaKupca.Checked = true;
            }

            if (DTpostavke.Rows[0]["dodatak_na_artikl"].ToString() == "0")
            {
                chkDodatakNaArtikl.Checked = false;
            }
            else if (DTpostavke.Rows[0]["dodatak_na_artikl"].ToString() == "1")
            {
                chkDodatakNaArtikl.Checked = true;
            }

            txtDomena.Text = Util.Korisno.domena_za_sinkronizaciju;

            if (!Directory.Exists(txtBackupLokacije.Text))
            {
                txtBackupLokacije.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString();
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString()))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString());
                }

                btnSpremiBackup_Click(null, null);
            }

            if (DTpostavke.Rows[0]["stol_konobar"].ToString() == "0")
            {
                chbStolKonobar.Checked = false;
            }
            else if (DTpostavke.Rows[0]["stol_konobar"].ToString() == "1")
            {
                chbStolKonobar.Checked = true;
            }

            if (DTpostavke.Rows[0]["prijava_nakon_racuna"].ToString() == "0")
            {
                chbNakonPrijave.Checked = false;
            }
            else if (DTpostavke.Rows[0]["prijava_nakon_racuna"].ToString() == "1")
            {
                chbNakonPrijave.Checked = true;
            }

            if (DTpostavke.Rows[0]["dodatno_upozorenje"].ToString() == "0")
            {
                chbDodatnoUpozorenje.Checked = false;
            }
            else if (DTpostavke.Rows[0]["dodatno_upozorenje"].ToString() == "1")
            {
                chbDodatnoUpozorenje.Checked = true;
            }

            UcitajOstalePostavke();

            txtLozinkaZaCert.Text = DTpostavke.Rows[0]["certifikat_zaporka"].ToString();
            txtPutanjaZaCert.Text = DTpostavke.Rows[0]["putanja_certifikat"].ToString();
            txtPopustKartica.Text = DTpostavke.Rows[0]["kartica_popust"].ToString();
            txtReaderPrefix.Text = DTpostavke.Rows[0]["reader_prefix"].ToString();
            txtReaderSufix.Text = DTpostavke.Rows[0]["reader_sufix"].ToString();

            txtApiKey.Text = Class.Postavke.UDSGameApiKey;
            chbUseUdsGame.Checked = Class.Postavke.UDSGame;
            chbUseEmployees.Checked = Class.Postavke.UDSGameEmployees;

            chbTestFisklaizacija.Checked = Class.Postavke.TEST_FISKALIZACIJA;
            chbIspisNapomeneNaMaloprodajnimRacunima.Checked = Class.Postavke.napomena_na_kraju_racuna;
            chbNapomenaNaKrajuPredracuna.Checked = Class.Postavke.napomena_na_kraju_predracuna;

            chbRadSaTabletima.Checked = Class.Postavke.rad_sa_tabletima;

            karticaDisabled(chkKarticaKupca.Checked);

            loaded = true;
        }

        #region UZIMA IP i USER-a

        private string _ip = "";
        private string _unm = "";

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _ip = GetIP().Trim();
            _unm = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtNetIP.Text = _ip;
            txtCompUsername.Text = _unm;
        }

        private string GetIP()
        {
            try
            {
                // check IP using DynDNS's service
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org");
                WebResponse response = request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream());

                // read complete response
                string ipAddress = stream.ReadToEnd();

                // replace everything and keep only IP
                return ipAddress.
                    Replace("<html><head><title>Current IP Check</title></head><body>Current IP Address: ", string.Empty).
                    Replace("</body></html>", string.Empty);
            }
            catch (Exception)
            {
                return "0";
            }
        }

        #endregion UZIMA IP i USER-a

        private void BackupBaze()
        {
            if (DTpostavke.Rows[0]["backup_aktivnost"].ToString() == "1")
            {
                chbBackupAktivnost.Checked = true;
            }
            else
            {
                chbBackupAktivnost.Checked = false;
            }

            txtBackupLokacije.Text = DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString();
        }

        private void WebPogled()
        {
            if (DTpostavke.Rows[0]["salji_na_web"].ToString() == "1")
            {
                chbWebActive.Checked = true;
            }
            else { chbWebActive.Checked = false; }

            txtUsernameWeb.Text = DTpostavke.Rows[0]["salji_na_web_user"].ToString();
            txtPasswordWeb.Text = DTpostavke.Rows[0]["salji_na_web_pass"].ToString();
            txtDomenaWeb.Text = DTpostavke.Rows[0]["salji_na_web_ftp"].ToString();
        }

        //void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void SetFiskal()
        {
            DataTable DTSK = new DataTable("OznakaFiskal");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("P", "P - na nivou poslovnog prostora");
            DTSK.Rows.Add("N", "N - na nivou naplatnog uređaja");

            cbOznakaFiskal.DataSource = DTSK;
            cbOznakaFiskal.DisplayMember = "naziv";
            cbOznakaFiskal.ValueMember = "id";

            DataTable DT = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
            if (DT.Rows[0]["aktivna"].ToString() == "1")
            {
                chbFiskal.Checked = true;
            }
            else
            {
                chbFiskal.Checked = false;
            }
            txtNazivCertifikata.Text = DT.Rows[0]["naziv_certifikata"].ToString();
            cbOznakaFiskal.SelectedValue = DT.Rows[0]["oznaka_slijednosti"].ToString();
        }

        private void Magnetska_kartica()
        {
            if (DTpostavke.Rows[0]["magnetska_kartica"].ToString() == "1")
            {
                chbPrijava.Checked = true;
            }
            else { chbPrijava.Checked = false; }
        }

        private void SetRemoteFields()
        {
            PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
            List<string> L = B.UzmiSveBazeIzPostgressa();
            DataTable DT = new DataTable();
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("id");
                DT.Columns.Add("name", typeof(int));
            }

            foreach (string db in L)
            {
                //if (db != "postgres" && (db.StartsWith(Util.Korisno.prefixBazeKojaSeKoristi())))
                string dbprefix = db;
                dbprefix = dbprefix.Remove(dbprefix.Length - Util.Korisno.GodinaKojaSeKoristiUbazi.ToString().Length);
                if (db != "postgres" && (dbprefix == Util.Korisno.prefixBazeKojaSeKoristi()))
                {
                    string baza = db;
                    baza = db.Remove(0, Util.Korisno.prefixBazeKojaSeKoristi().Length);
                    //baza = baza.Replace("DB", "");
                    //baza = baza.Replace("POS", "");
                    //baza = baza.Replace("db", "");
                    //baza = baza.Replace("pos", "");
                    DT.Rows.Add(db, baza);
                }
            }
            DataView dv = new DataView(DT);
            dv.Sort = "name asc";
            DT = dv.ToTable();
            cbRemoteNameDatabase.DataSource = DT;
            cbRemoteNameDatabase.ValueMember = "id";
            cbRemoteNameDatabase.DisplayMember = "name";

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                txtRemoteImeServera.Text = book.Attribute("server").Value;
                txtRemoteUsername.Text = book.Attribute("username").Value;
                txtRemotePort.Text = book.Attribute("port").Value;
                cbRemoteNameDatabase.SelectedValue = book.Attribute("database").Value;
                txtRemoteLozinka.Text = DTpostavke.Rows[0]["pass"].ToString();

                if (book.Attribute("active").Value == "1")
                {
                    chbActive.Checked = true;
                }
                else
                {
                    chbActive.Checked = false;
                }
            }
            txtRemoteLozinka.PasswordChar = '*';

            //try
            //{
            //    //DataTable DTDB = classSQL.select("SELECT datname FROM pg_database WHERE datistemplate IS FALSE AND datallowconn IS TRUE AND datname!='postgres';", "").Tables[0];

            //    //for (int i = 0; i < DTDB.Rows.Count; i++)
            //    //{
            //    //    cbRemoteNameDatabase.Items.Add(DTDB.Rows[i][0].ToString());
            //    //}
            //}
            //catch (Exception)
            //{
            //}
        }

        private void SetComboBox()
        {
            //fill Skladiste
            DataTable DTskladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DTskladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //fill Zaposlenik
            DataTable DS_Zaposlenik = classSQL.select("SELECT ime +' '+ prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbBlagajnik.DataSource = DS_Zaposlenik;
            cbBlagajnik.DisplayMember = "IME";
            cbBlagajnik.ValueMember = "id_zaposlenik";

            //fill Ducan
            DataTable DTducan = classSQL.select("SELECT * FROM ducan where aktivnost = 'DA';", "ducan").Tables[0];
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";
            cbDucan.DataSource = DTducan;
            cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();

            //fill blagajna
            DataTable DTblagajna = classSQL.select(string.Format("SELECT * FROM blagajna where aktivnost = '1' and id_ducan = '{0}';", DTpostavke.Rows[0]["default_ducan"].ToString()), "blagajna").Tables[0];
            cbKasa.DataSource = DTblagajna;
            cbKasa.DisplayMember = "ime_blagajne";
            cbKasa.ValueMember = "id_blagajna";

            //fill faktura blagajna
            DataTable DTblagajna_fakture = classSQL.select(string.Format("SELECT * FROM blagajna where aktivnost = '1' and id_ducan = '{0}';", DTpostavke.Rows[0]["default_ducan"].ToString()), "blagajna").Tables[0];
            cmbFaktureKasa.DataSource = DTblagajna_fakture;
            cmbFaktureKasa.DisplayMember = "ime_blagajne";
            cmbFaktureKasa.ValueMember = "id_blagajna";

            //MessageBox.Show(DTpostavke.Rows[0]["default_skladiste"].ToString());
            try
            {
                cbSkladiste.SelectedValue = DTpostavke.Rows[0]["default_skladiste"].ToString();
                cbBlagajnik.SelectedValue = DTpostavke.Rows[0]["default_blagajnik"].ToString();
                //cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();
                cbKasa.SelectedValue = DTpostavke.Rows[0]["default_blagajna"].ToString();
                cmbFaktureKasa.SelectedValue = DTpostavke.Rows[0]["default_kasa_fakture"].ToString();
            }
            catch (Exception)
            {
            }

            DataTable DTSK = new DataTable("pdv");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("0", "NE");
            DTSK.Rows.Add("1", "DA");

            cbSustavPDV.DataSource = DTSK;
            cbSustavPDV.DisplayMember = "naziv";
            cbSustavPDV.ValueMember = "id";
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string cashback = "0";
            string bodovi = "0";
            string postotak = "0";

            //if (rbCashBack.Checked)
            //{
            //    cashback = "1";
            //}
            //else if (rbBodovi.Checked)
            //{
            //    bodovi = "1";
            //}
            //else if (rbPopustSlljedecakupovina.Checked)
            //{
            //    postotak = "1";
            //}

            string sql = "UPDATE postavke SET " +
                " default_ducan='" + cbDucan.SelectedValue + "'," +
                " default_blagajna='" + cbKasa.SelectedValue + "'," +
                " default_skladiste='" + cbSkladiste.SelectedValue + "'," +
                " default_blagajnik='" + cbBlagajnik.SelectedValue + "'," +
                " default_jezik=" + cbJezik.SelectedValue + "," +
                " on_off_cashback='" + cashback + "'," +
                " on_off_bodovi='" + bodovi + "'," +
                " putanja_certifikat='" + txtPutanjaZaCert.Text + "'," +
                " certifikat_zaporka='" + txtLozinkaZaCert.Text + "'," +
                " sustav_pdv='" + cbSustavPDV.SelectedValue + "'," +
                " on_off_postotak='" + postotak + "'," +
                " print_narudzbe_bez_pitanja='"+(checkBoxSaljiNarudzbeAutomatski.Checked?'1':'0')+"',"+
                " default_kasa_fakture = '" + cmbFaktureKasa.SelectedValue + "'" +
                "";

            provjera_sql(classSQL.Setings_Update(sql));

            string _1 = "0";
            if (chbFiskal.Checked)
            {
                _1 = "1";
            }

            sql = "UPDATE fiskalizacija SET oznaka_slijednosti='" + cbOznakaFiskal.SelectedValue.ToString() + "',naziv_certifikata='" + txtNazivCertifikata.Text + "',aktivna='" + _1 + "'";
            provjera_sql(classSQL.Setings_Update(sql));

            MessageBox.Show("Spremljeno!");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnKompaktnaTest_Click(object sender, EventArgs e)
        {
            if (SQL.claasConnectDatabase.TestCompactConnection(txtKompaktnaPut.Text) == true)
            {
                MessageBox.Show("Konekcija je uspjela.");
            }
            else
            {
                MessageBox.Show("Konekcija nije uspjela.");
            }
        }

        private void btnKompaktnaSpremi_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtKompaktnaPut.Text))
            {
                MessageBox.Show("Odabrana baza ne postoji.", "Greška");
                return;
            }

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_compact").Elements("path_database") select c;
            foreach (XElement book in query)
            {
                book.Attribute("path").Value = txtKompaktnaPut.Text;
            }
            xmlFile.Save(path);
            MessageBox.Show("Spremljeno", "Spremljeno");
        }

        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtKompaktnaPut.Text = openFileDialog1.FileName;
            }
        }

        private void SetDatabase()
        {
            txtKompaktnaPut.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\database.sdf";
        }

        /// <summary>
        /// 
        /// </summary>
        private void ConnectToFtp()
        {
            try
            {
                string fileName = @"PC POS.exe";
                string url = $"ftp://5.189.154.50/CodeCaffe/{fileName}";
                using (WebClient req = new WebClient())
                {
                    req.Credentials = new NetworkCredential("codeadmin", "Eqws64%2");
                    byte[] fileData = req.DownloadData(url);

                    using (FileStream file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"dela\{fileName}")))
                    {
                        file.Write(fileData, 0, fileData.Length);
                    }
                }
                MessageBox.Show("Dela");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRemoteTest_Click(object sender, EventArgs e)
        {
            if (SQL.claasConnectDatabase.TestRemoteConnection() == true)
            {
                MessageBox.Show("Konekcija je uspjela.");
            }
            else
            {
                MessageBox.Show("Konekcija nije uspjela.");
            }
        }

        private void txtRemoteSpremi_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                book.Attribute("server").Value = txtRemoteImeServera.Text;
                book.Attribute("username").Value = txtRemoteUsername.Text;
                book.Attribute("port").Value = txtRemotePort.Text;
                book.Attribute("database").Value = cbRemoteNameDatabase.SelectedValue.ToString();
                provjera_sql(classSQL.Setings_Update("UPDATE postavke SET pass='" + txtRemoteLozinka.Text + "'"));

                if (chbActive.Checked)
                {
                    book.Attribute("active").Value = "1";
                }
                else
                {
                    book.Attribute("active").Value = "1";
                }
            }
            xmlFile.Save(path);
            Class.PodaciZaSpajanjeCompaktna.getPodaci();
            MessageBox.Show("Spremljeno");

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
            Application.Restart();
        }

      /*  private void btnNadogradi_Click(object sender, EventArgs e)
        {
            string nadogradnjaProgramaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"NadogradnjaPrograma.exe");
            string fileName = @"NadogradnjaPrograma.exe";
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

            MessageBox.Show($@"Program će se automatski ažurirati. Molimo pričekajte 10 - 20 sekundi.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Threading.Thread.Sleep(500); // Potrebno zbog toga što environment.Exit() prekida sve procese iako se izvršavaju. Pa dajemo
                                                // programu još PUNO vremena (gledajući iz perspektive procesora) da se sve završi.
            Process.Start("NadogradnjaPrograma.exe"); // Pokretanje programa za update
            Environment.Exit(0); // Izlaz iz trenutnog programa
        }
        */

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCreateRemoteDB crb = new frmCreateRemoteDB();
            crb.ShowDialog();
        }

        private void btnSpremiWeb_Click(object sender, EventArgs e)
        {
            string active = "0";
            if (chbWebActive.Checked)
            {
                active = "1";
            }

            string sql = "UPDATE postavke SET " +
                " salji_na_web='" + active + "'," +
                " salji_na_web_ftp='" + txtDomenaWeb.Text + "'," +
                " salji_na_web_user='" + txtUsernameWeb.Text + "'," +
                " salji_na_web_pass='" + txtPasswordWeb.Text + "'" +
                "";
            classSQL.Setings_Update(sql);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBackupLokacije.Text = folderDlg.SelectedPath;
                //Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btnSpremiBackup_Click(object sender, EventArgs e)
        {
            string active = "0";

            if (chbActive.Checked)
            {
                active = "1";
            }

            string sql = "UPDATE postavke SET " +
                " lokacija_sigurnosne_kopije='" + txtBackupLokacije.Text + "'," +
                " backup_aktivnost='" + active + "'" +
                "";
            classSQL.Setings_Update(sql);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            //File.WriteAllText("DBbackup/DBbackup.bat", "pg_dump.exe --host " + txtRemoteImeServera.Text + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + txtBackupLokacije.Text + "\\db" + DateTime.Now.ToString("yyyy-MM-dd") + ".backup\" \"" + cbRemoteNameDatabase.Text + "\"");
            string path = System.Environment.CurrentDirectory;
            //Process.Start(path+"/DBbackup/DBbackup.bat");

            /*System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WorkingDirectory = path + "\\DBbackup";
            proc.StartInfo.FileName = path + "\\DBbackup\\DBbackup.bat";
            //proc.StartInfo.RedirectStandardError = true;
            //proc.StartInfo.RedirectStandardOutput = true;
            //proc.StartInfo.UseShellExecute = false;
            proc.Start();*/
            System.Diagnostics.Process.Start(path + "\\DBbackup\\pg_dump.exe", "--host " + txtRemoteImeServera.Text + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + txtBackupLokacije.Text + "\\db" + DateTime.Now.ToString("yyyy-MM-dd") + ".backup\" \"" + cbRemoteNameDatabase.Text + "\"");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //return;
            //DataTable DT = classSQL.select("SELECT vpc,porez,porez_potrosnja,id_stavka FROM racun_stavke", "racun_stavke").Tables[0];

            //decimal vpc=0;
            //decimal pnp=0;
            //decimal pdv=0;
            //decimal mpc=0;

            //for(int i = 0; i<DT.Rows.Count; i++)
            //{
            //    try
            //    {
            //        vpc = Convert.ToDecimal(DT.Rows[i]["vpc"].ToString());
            //        pnp = Convert.ToDecimal(DT.Rows[i]["porez_potrosnja"].ToString());
            //        pdv = Convert.ToDecimal(DT.Rows[i]["porez"].ToString());

            //        mpc=(vpc*(pnp+pdv)/100)+vpc;

            //        string sql = "UPDATE racun_stavke SET mpc='"+mpc+"' WHERE id_stavka='"+DT.Rows[i]["id_stavka"].ToString()+"'";
            //        classSQL.update(sql);

            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        private void chbDodatnaDopustenja_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDodatnaDopustenja.Checked)
            {
                string sql = "UPDATE postavke SET samo_prodaja='1'";
                classSQL.Setings_Update(sql);
            }
            else if (!chbDodatnaDopustenja.Checked)
            {
                string sql = "UPDATE postavke SET samo_prodaja='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void chbPrijava_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPrijava.Checked)
            {
                string sql = "UPDATE postavke SET magnetska_kartica='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET magnetska_kartica='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ProvjeraBaze.IncomingWithUpdate();
        }

        private void chbPosaljiUkuhinju_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPosaljiUkuhinju.Checked)
            {
                string sql = "UPDATE postavke SET bool_direct_print_kuhinja='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET bool_direct_print_kuhinja='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void chbSkidajSaPredracuna_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSkidajSaPredracuna.Checked)
            {
                string sql = "UPDATE postavke SET skidaj_sa_predracuna='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET skidaj_sa_predracuna='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void chbStolKonobar_CheckedChanged(object sender, EventArgs e)
        {
            if (chbStolKonobar.Checked)
            {
                string sql = "UPDATE postavke SET stol_konobar='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET stol_konobar='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void btnNovaGodinaBaza_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("UPOZORENJE!!!\r\n" +
                "Prije kreiranja nove godine potrebnop je provjeriti dali je baza podataka stavljena na 'METHOD Trust'.\r\n" +
                "Ako mislite da nije tako postavljena prekinite ovu operaciju i provjerite postavke baze!\r\n" +
                "Postupak za ovaj korak je da otvorite pgAdmin->Tools->Server Configuration->pg_hba.config->Method->Trust!\r\n\r\nAko ste sigurni da je sve postavljeno kako treba nastavite sa YES!",
                "UPOZORENJE",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                u.KreirajNovuGodinu();
            }
        }

        private PCPOS.Until.classFukcijeZaUpravljanjeBazom u = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            u.BackupSvihBaza();
        }

        private void btnSpremiPromjeneZaFiskal_Click(object sender, EventArgs e)
        {
            //string sql = "UPDATE postavke SET " +
            //    " default_ducan='" + cbDucan.SelectedValue + "'," +
            //    " default_blagajna='" + cbKasa.SelectedValue + "'," +
            //    " default_skladiste='" + cbSkladiste.SelectedValue + "'," +
            //    " default_blagajnik='" + cbBlagajnik.SelectedValue + "'," +
            //    " putanja_certifikat='" + txtPutanjaZaCert.Text + "'," +
            //    " certifikat_zaporka='" + txtLozinkaZaCert.Text + "'," +
            //    " sustav_pdv='" + cbSustavPDV.SelectedValue + "'" +
            //    "";

            //provjera_sql(classSQL.Setings_Update(sql));

            //btnSpremi.PerformClick();

            btnSpremi_Click(sender, e);
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPutanjaZaCert.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Until.frmPromjenaPoreza frm = new Until.frmPromjenaPoreza();
            frm.dokument = "porez";
            frm.ShowDialog();
        }

        private void btnPrijenosStanja_Click(object sender, EventArgs e)
        {
            Until.classFukcijeZaUpravljanjeBazom DB = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");

            DataTable DTpgRoba = new DataTable();
            DataTable DTpgRobaProdaja = new DataTable();
            DataTable DTpgNormativ = new DataTable();

            if (DB.PostojiProslaGodina())
            {
                DB.BackupSvihBaza();

                NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(Convert.ToInt16(DateTime.Now.Year).ToString(), (Convert.ToInt16(DateTime.Now.Year) - 1).ToString()));

                string sql = "SELECT * FROM roba";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, remoteConnection);
                DataSet DS = new DataSet();
                da.Fill(DS);
                DataTable DTk = DS.Tables[0];

                DTpgRoba = classSQL.select("SELECT * FROM roba", "roba").Tables[0];
                DTpgRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja", "roba_prodaja").Tables[0];
                DTpgNormativ = classSQL.select("SELECT * FROM caffe_normativ", "caffe_normativ").Tables[0];
            }
        }

        private void UcitajOstalePostavke()
        {
            //if (DTpostavke.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "0")
            //{
            //    chbProgramskoStanjeSkladista.Checked = false;
            //}
            //else if (DTpostavke.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "1")
            //{
            //    chbProgramskoStanjeSkladista.Checked = true;
            //}
            chbProgramskoStanjeSkladista.Checked = Class.Postavke.skidaj_kolicinu_po_dokumentima;

            if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "0")
            {
                chbAktiviranaWebSyn.Checked = false;
            }
            else if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1")
            {
                chbAktiviranaWebSyn.Checked = true;
            }

            if (DTpostavke.Rows[0]["obavjeti_ako_nema_repromaterijala"].ToString() == "0")
            {
                chbUpozoriZaRepromaterijal.Checked = false;
            }
            else if (DTpostavke.Rows[0]["obavjeti_ako_nema_repromaterijala"].ToString() == "1")
            {
                chbUpozoriZaRepromaterijal.Checked = true;
            }
        }

        private void txtLozinkaZaWebAktivaciju_TextChanged(object sender, EventArgs e)
        {
            bool prikazi = false;
            if (txtLozinkaZaWebAktivaciju.Text == "WebSynq1w2e3r4")
            {
                prikazi = true;
            }

            chbAktiviranaWebSyn.Enabled = prikazi;
            btnPosaljiNaWeb.Enabled = prikazi;
            btnBrisiSve.Enabled = prikazi;
            txtDomena.Visible = prikazi;
            label41.Visible = prikazi;
            grbPreuzmiSveWeb.Visible = prikazi;
        }

        private void chbProgramskoStanjeSkladista_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("UPDATE postavke SET skidaj_kolicinu_po_dokumentima='{0}'", (chbProgramskoStanjeSkladista.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbAktiviranaWebSyn_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAktiviranaWebSyn.Checked)
            {
                string sql = "UPDATE postavke SET posalji_dokumente_na_web='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET posalji_dokumente_na_web='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void chbUpozoriZaRepromaterijal_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUpozoriZaRepromaterijal.Checked)
            {
                string sql = "UPDATE postavke SET obavjeti_ako_nema_repromaterijala='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET obavjeti_ako_nema_repromaterijala='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void btnPreuzmiWebSyn_Click(object sender, EventArgs e)
        {
            if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Sinkronizacija.uzmi_sve_artikle.synArtikli a = new Sinkronizacija.uzmi_sve_artikle.synArtikli();
                a.UzmiPodatkeSaWeba();

                Sinkronizacija.uzmi_sve_artikle.synNormativ n = new Sinkronizacija.uzmi_sve_artikle.synNormativ();
                n.UzmiPodatkeSaWeba();

                Sinkronizacija.uzmi_sve_artikle.synRobaProdaja rp = new Sinkronizacija.uzmi_sve_artikle.synRobaProdaja();
                rp.UzmiPodatkeSaWeba();

                MessageBox.Show("Izvršeno");
            }
            else
            {
                MessageBox.Show("Nemate prava pristupati ovoj funkcionalnosti.");
            }
        }

        private void btnPosaljiNaWeb_Click(object sender, EventArgs e)
        {
            synRacuni Racuni = new synRacuni(true);
            synPrimka Primka = new synPrimka(true);
            synGrupe Grupe = new synGrupe(true);
            synZaposlenici Zaposlenici = new synZaposlenici(true);
            synOtpisRobe OtpisRobe = new synOtpisRobe(true);
            synInventura Inventura = new synInventura(true);
            synPocetnoStanje PocetnoStanje = new synPocetnoStanje(true);
            synPromjena_cijene PromjenaCijene = new synPromjena_cijene(true);
            synBlagajnickiIzvjestaj BlagajnickiIzvj = new synBlagajnickiIzvjestaj(true);
            synMeduskladisnica Meduskladisnica = new synMeduskladisnica(true);
            synPoslovnice Poslovnice = new synPoslovnice(true);
            synPartner Partner = new synPartner(true);
            synNormativ Normativ = new synNormativ(true);
            synArtikli ProdajnaRoba = new synArtikli(true);
            synRobaProdaja Repromaterijal = new synRobaProdaja(true);
            synPredracuni Predracuni = new synPredracuni(true);
            synSkladiste Skladista = new synSkladiste(true);
            synIzdatnice Izdatnice = new synIzdatnice(true);

            synFaktura Faktura = new synFaktura(true);
            synOtpremnica Otpremnica = new synOtpremnica(true);

            if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    Racuni.Send();
                    Primka.Send();
                    Grupe.Send();
                    Zaposlenici.Send();
                    Zaposlenici.UzmiPodatkeSaWeba();
                    OtpisRobe.Send();
                    Inventura.Send();
                    PocetnoStanje.Send();
                    PromjenaCijene.Send();
                    BlagajnickiIzvj.Send();
                    Meduskladisnica.Send();
                    ProdajnaRoba.Send();
                    Repromaterijal.Send();
                    Normativ.Send();
                    Poslovnice.Send();
                    Partner.Send();
                    Predracuni.Send();
                    Skladista.Send();
                    Izdatnice.Send();

                    Faktura.Send();
                    Otpremnica.Send();

                    Util.Korisno.RadimSinkronizaciju = false;
                    MessageBox.Show("Izvršeno");
                }
            }
        }

        private void btnBrisiSve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Funkcija briše:\r\nSve račune,\r\nSve primke,\r\nSve inventure,\r\nPočetno stanje,\r\nOtpis robe\r\n\r\n\r\n\r\n" +
                "Dali ste sigurni da želite obrisati gore navedene stavke???", "Upozorenje",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Prije brisanja još jednom potvrdite da želite obrisati gore navedene stavke!!!", "Upozorenje",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "BEGIN;" +
                        "DELETE FROM racuni;" +
                        "DELETE FROM racun_stavke;" +
                        "DELETE FROM primka;" +
                        "DELETE FROM primka_stavke;" +
                        "DELETE FROM povrat_robe;" +
                        "DELETE FROM povrat_robe_stavke;" +
                        "DELETE FROM pocetno;" +
                        "DELETE FROM inventura;" +
                        "DELETE FROM inventura_stavke;" +
                        //"UPDATE roba_prodaja SET nc='0';" +
                        //"UPDATE roba SET mpc='21' WHERE sifra='153';" +
                        //"UPDATE roba SET mpc='21' WHERE sifra='146';" +
                        //"UPDATE roba SET mpc='12' WHERE sifra='163';" +
                        //"UPDATE roba SET mpc='12' WHERE sifra='164';" +
                        //"UPDATE roba SET mpc='18' WHERE sifra='791';" +
                        //"UPDATE roba SET mpc='18' WHERE sifra='741';" +
                        //"DELETE FROM roba WHERE sifra='1';" +
                        //"DELETE FROM roba WHERE sifra='2';" +
                        //"DELETE FROM roba WHERE sifra='3';" +
                        "COMMIT;";

                    classSQL.insert(query);
                    MessageBox.Show("Obrisano!");
                }
            }
        }

        #region SPREMAM POSTAVKE ZA NAČINE PLAĆANJA

        private void chbNPgotovina_CheckedChanged(object sender, EventArgs e)
        {
            SpremiChbNaciniPlacanja();
        }

        private void chbNPkartice_CheckedChanged(object sender, EventArgs e)
        {
            SpremiChbNaciniPlacanja();
        }

        private void chbNPvirman_CheckedChanged(object sender, EventArgs e)
        {
            SpremiChbNaciniPlacanja();
        }

        private void chbNPostalo_CheckedChanged(object sender, EventArgs e)
        {
            SpremiChbNaciniPlacanja();
        }

        private void LOADChbNaciniPlacanja()
        {
            try
            {
                string[] nacini_placanja = DTpostavke.Rows[0]["nacini_placanja"].ToString().Split(';');

                if (nacini_placanja[0] == "1")
                    chbNPgotovina.Checked = true;
                if (nacini_placanja[1] == "1")
                    chbNPkartice.Checked = true;
                if (nacini_placanja[2] == "1")
                    chbNPvirman.Checked = true;
                if (nacini_placanja[3] == "1")
                    chbNPostalo.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SpremiChbNaciniPlacanja()
        {
            try
            {
                string val = "";
                if (chbNPgotovina.Checked) { val = "1;"; } else { val = "0;"; }

                if (chbNPkartice.Checked) { val = val + "1;"; } else { val = val + "0;"; }

                if (chbNPvirman.Checked) { val = val + "1;"; } else { val = val + "0;"; }

                if (chbNPostalo.Checked) { val = val + "1"; } else { val = val + "0"; }

                classSQL.Setings_Update("UPDATE postavke SET nacini_placanja='" + val + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion SPREMAM POSTAVKE ZA NAČINE PLAĆANJA

        #region POSTAVA ZADNJEG RAČUNA

        private void txtZadnjiRacUGodini_TextChanged(object sender, EventArgs e)
        {
            string vall = "";
            int ii;
            int.TryParse(txtZadnjiRacUGodini.Text, out ii);

            vall = ii.ToString() + ";" + nuGodinaZaZadnjiRac.Value.ToString();
            classSQL.Setings_Update("UPDATE postavke SET zadnji_racun='" + vall + "'");
        }

        private void nuGodinaZaZadnjiRac_ValueChanged(object sender, EventArgs e)
        {
            string vall = "";
            vall = txtZadnjiRacUGodini.Text + ";" + nuGodinaZaZadnjiRac.Value.ToString();
            classSQL.Setings_Update("UPDATE postavke SET zadnji_racun='" + vall + "'");
        }

        private void LoadZadnjiRacun()
        {
            string[] zadnji_racun = DTpostavke.Rows[0]["zadnji_racun"].ToString().Split(';');
            txtZadnjiRacUGodini.Text = zadnji_racun[0];
            int zrac;
            int.TryParse(zadnji_racun[1], out zrac);
            nuGodinaZaZadnjiRac.Value = zrac;
        }

        #endregion POSTAVA ZADNJEG RAČUNA

        private void chbZaposleniciVideSamoSvojuProdaju_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (chbZaposleniciVideSamoSvojuProdaju.Checked)
                    {
                        string sql = "UPDATE postavke SET zabrana_zaposleniku_da_vidi_druge_promete='1'";
                        classSQL.Setings_Update(sql);
                    }
                    else
                    {
                        string sql = "UPDATE postavke SET zabrana_zaposleniku_da_vidi_druge_promete='0'";
                        classSQL.Setings_Update(sql);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnPostaviNbc_Click(object sender, EventArgs e)
        {
            FunkcijeRobno fr = new FunkcijeRobno();
            fr.PostaviNabavneCijeneZaTablicuRobaProdaja();
            fr.PostaviNabavneCijeneRoba();
            fr.UzmiNabavneCijeneRacuni();
        }

        private void txtDomena_TextChanged(object sender, EventArgs e)
        {
            Util.Korisno.domena_za_sinkronizaciju = txtDomena.Text;
            //Properties.Settings.Default.Save();
            classSQL.Setings_Update("update postavke set domena_za_sinkronizaciju = '" + txtDomena.Text + "';");
        }

        private void chbNakonPrijave_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (chbNakonPrijave.Checked)
                    {
                        string sql = "UPDATE postavke SET prijava_nakon_racuna='1'";
                        classSQL.Setings_Update(sql);
                    }
                    else
                    {
                        string sql = "UPDATE postavke SET prijava_nakon_racuna='0'";
                        classSQL.Setings_Update(sql);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void chbUgo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (chbUgo.Checked)
                    {
                        chbBeauty.Checked = false;
                    }

                    chbBeauty.Enabled = !chbUgo.Checked;

                    string sql = string.Format("UPDATE postavke SET is_caffe = '{0}';", (chbUgo.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chbFiskalizacijaIskljucena_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET upozori_iskljucenu_fiskalizaciju='{0}'", (chbFiskalizacijaIskljucena.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {
        }

        private void chbDodatnoUpozorenje_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET dodatno_upozorenje = '{0}';", (chbDodatnoUpozorenje.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chbPopust_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET koristi_popust_prema_stavki = '{0}';", (chbPopust.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chbZabraniUvidStanjeSkl_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET zabraniUvidSkladiste = '{0}';", (chbZabraniUvidStanjeSkl.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chbStoloviRaspored_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET stolovi_razvrstavanje = '{0}';", (chbStoloviRaspored.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                    btnUrediStolove.Enabled = chbStoloviRaspored.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUrediStolove_Click(object sender, EventArgs e)
        {
            Caffe.frmOdabirStolaCustom os = new Caffe.frmOdabirStolaCustom();
            os.ShowDialog();
        }

        private void chkKarticaKupca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKarticaKupca.Checked)
            {
                string sql = "UPDATE postavke SET kartica_kupca='1'";
                classSQL.Setings_Update(sql);
                Util.Korisno.kartica_kupca = true;
            }
            else
            {
                string sql = "UPDATE postavke SET kartica_kupca='0'";
                classSQL.Setings_Update(sql);
                Util.Korisno.kartica_kupca = false;
            }
            karticaDisabled(chkKarticaKupca.Checked);
        }

        private void txtPopustKartica_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && txtPopustKartica.Text.Length > 0)
                {
                    decimal postotak = 0;
                    if (decimal.TryParse(txtPopustKartica.Text, out postotak))
                    {
                        string sql = "UPDATE postavke SET kartica_popust = '" + postotak + "'";
                        classSQL.Setings_Update(sql);
                        MessageBox.Show("Spremljeni popust.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkDodatakNaArtikl_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    string sql = string.Format("UPDATE postavke SET dodatak_na_artikl = '{0}';", (chkDodatakNaArtikl.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                    Util.Korisno.kartica_kupca = chkDodatakNaArtikl.Checked;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtReaderPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string sql = "UPDATE postavke SET reader_prefix = '" + txtReaderPrefix.Text + "'";
                    classSQL.Setings_Update(sql);
                    MessageBox.Show("Prefik čitača je spremljen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtReaderSufix_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string sql = "UPDATE postavke SET reader_sufix = '" + txtReaderSufix.Text + "'";
                    classSQL.Setings_Update(sql);
                    MessageBox.Show("Sufiks čitača je spremljen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void karticaDisabled(bool kartica)
        {
            txtPopustKartica.Enabled = kartica;
            txtReaderPrefix.Enabled = kartica;
            txtReaderSufix.Enabled = kartica;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ProvjeraBaze.IncomingWithUpdate();
                File.WriteAllText("ProvjeraTablicaBaze" + Util.Korisno.GodinaKojaSeKoristiUbazi, Properties.Settings.Default.verzija_programa.ToString());
                MessageBox.Show("Izvršeno.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPoreznaPotrosnjuPromjena_Click(object sender, EventArgs e)
        {
            try
            {
                Until.frmPromjenaPoreza frm = new Until.frmPromjenaPoreza();
                frm.dokument = "porez_na_potrosnju";
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbSamoDanasnjaProdaja_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbSamoDanasnjaProdaja.Checked)
                {
                    string sql = "UPDATE postavke SET zaposlenici_vide_samo_danasnju_prodaju='1'";
                    classSQL.Setings_Update(sql);
                }
                else
                {
                    string sql = "UPDATE postavke SET zaposlenici_vide_samo_danasnju_prodaju='0'";
                    classSQL.Setings_Update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Želite preuzeti sve dokumente s weba?\nNakon preuzimanja nema povratka", "Preuzimanje dokumenta s web-a.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (MessageBox.Show("Sigurno želite preuzeti sve dokumente s weba?\nNakon preuzimanja nema povratka", "Preuzimanje dokumenta s web-a.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                synRacuni Racuni = new synRacuni(true);
                synPrimka Primka = new synPrimka(true);
                synGrupe Grupe = new synGrupe(true);
                synZaposlenici Zaposlenici = new synZaposlenici(true);
                synOtpisRobe OtpisRobe = new synOtpisRobe(true);
                synInventura Inventura = new synInventura(true);
                synPocetnoStanje PocetnoStanje = new synPocetnoStanje(true);
                synPromjena_cijene PromjenaCijene = new synPromjena_cijene(true);
                synBlagajnickiIzvjestaj BlagajnickiIzvj = new synBlagajnickiIzvjestaj(true);
                synMeduskladisnica Meduskladisnica = new synMeduskladisnica(true);
                synPoslovnice Poslovnice = new synPoslovnice(true);
                synPartner Partner = new synPartner(true);
                synNormativ Normativ = new synNormativ(true);
                synArtikli ProdajnaRoba = new synArtikli(true);
                synRobaProdaja Repromaterijal = new synRobaProdaja(true);
                synPredracuni Predracuni = new synPredracuni(true);
                synSkladiste Skladista = new synSkladiste(true);

                if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    if (!Util.Korisno.RadimSinkronizaciju)
                    {
                        Util.Korisno.RadimSinkronizaciju = true;

                        ProdajnaRoba.UzmiPodatkeSaWeba();
                        Repromaterijal.UzmiPodatkeSaWeba();

                        Racuni.UzmiPodatkeSaWeba();
                        Primka.UzmiPodatkeSaWeba();
                        Grupe.UzmiPodatkeSaWeba();
                        Zaposlenici.UzmiPodatkeSaWeba();
                        //Zaposlenici.UzmiPodatkeSaWeba();
                        OtpisRobe.UzmiPodatkeSaWeba();
                        Inventura.UzmiPodatkeSaWeba();
                        PocetnoStanje.UzmiPodatkeSaWeba();
                        PromjenaCijene.UzmiPodatkeSaWeba();
                        BlagajnickiIzvj.UzmiPodatkeSaWeba();
                        Meduskladisnica.UzmiPodatkeSaWeba();
                        Normativ.UzmiPodatkeSaWeba();
                        //Poslovnice.UzmiPodatkeSaWeba();
                        Partner.UzmiPodatkeSaWeba();
                        //Predracuni.UzmiPodatkeSaWeba();
                        //Skladista.UzmiPodatkeSaWeba();
                        Util.Korisno.RadimSinkronizaciju = false;
                        MessageBox.Show("Izvršeno");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbIspisNaKopijiRacuna_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE postavke SET ispis_na_kopiji_racuna = '" + (chbIspisNaKopijiRacuna.Checked ? 1 : 0) + "';";
                classSQL.Setings_Update(sql);
            }
            catch
            {
            }
        }

        private void chbBeauty_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE postavke SET is_beauty = '" + (chbBeauty.Checked ? 1 : 0) + "';";
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmPostavke_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Class.Postavke.getPodaci();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnPostaviKolicine_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select id_skladiste from skladiste where aktivnost = 'DA';";
                DataSet dsSkladsita = classSQL.select(sql, "skladiste");

                if (dsSkladsita != null && dsSkladsita.Tables.Count > 0 && dsSkladsita.Tables[0] != null && dsSkladsita.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsSkladsita.Tables[0].Rows)
                    {
                        sql = string.Format(@"SELECT sifra, naziv, coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra = pocetno.sifra and id_skladiste = roba_prodaja.id_skladiste), 0) zbroj
coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='False'  and id_skladiste = roba_prodaja.id_skladiste),0) zbroj
coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='True'  and id_skladiste = roba_prodaja.id_skladiste),0) zbroj
coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)-kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE roba_prodaja.sifra=inventura_stavke.sifra_robe and inventura.id_skladiste = roba_prodaja.id_skladiste AND inventura.is_pocetno_stanje='0'),0) -
coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke left join povrat_robe on povrat_robe_stavke.broj = povrat_robe.broj WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra and povrat_robe.id_skladiste = roba_prodaja.id_skladiste),0) -
coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra and racun_stavke.id_skladiste = roba_prodaja.id_skladiste),0))  FROM caffe_normativ WHERE caffe_normativ.id_skladiste = roba_prodaja.id_skladiste and caffe_normativ.sifra_normativ=roba_prodaja.sifra),0) -
coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)) FROM faktura_stavke WHERE faktura_stavke.sifra = caffe_normativ.sifra and faktura_stavke.id_skladiste = roba_prodaja.id_skladiste),0))  FROM caffe_normativ WHERE caffe_normativ.id_skladiste = roba_prodaja.id_skladiste and caffe_normativ.sifra_normativ = roba_prodaja.sifra),0) -
coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric) * coalesce((SELECT SUM(otpremnica_stavke.kolicina) FROM otpremnica_stavke WHERE otpremnica_stavke.sifra_robe=caffe_normativ.sifra and otpremnica_stavke.naplaceno_fakturom = false and otpremnica_stavke.id_skladiste = roba_prodaja.id_skladiste),0))  FROM caffe_normativ WHERE caffe_normativ.id_skladiste = roba_prodaja.id_skladiste and caffe_normativ.sifra_normativ=roba_prodaja.sifra),0) zbroj
((SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu='{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra and medu_poslovnice.id_skladiste = roba_prodaja.id_skladiste) -
(SELECT coalesce(SUM(medu_poslovnice.kolicina),0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice='{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra and medu_poslovnice.id_skladiste = roba_prodaja.id_skladiste)) as stanje
FROM roba_prodaja
where roba_prodaja.id_skladiste = '{1}'
ORDER BY naziv;",
cbDucan.SelectedValue, dRow[0].ToString());

                        sql = string.Format(@"select x.sifra as sifra, x.naziv  AS naziv, round(x.pocetno zbroj x.primka zbroj x.kalkulacija zbroj x.inventura - x.otpis - x.racuni - x.fakture - x.otpremnica zbroj (x.ulaz_ms - x.izlaz_ms) - x.izdatnica, 6) AS kolicina
from (
	SELECT roba_prodaja.sifra, roba_prodaja.naziv, coalesce((SELECT SUM(kolicina) AS kol FROM pocetno WHERE roba_prodaja.sifra = pocetno.sifra and roba_prodaja.id_skladiste = pocetno.id_skladiste), 0) AS pocetno,
	coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='False' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS primka,
	coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke WHERE roba_prodaja.sifra=primka_stavke.sifra AND is_kalkulacija='True' and roba_prodaja.id_skladiste = primka_stavke.id_skladiste),0) AS kalkulacija,
	coalesce((SELECT SUM(replace(kolicina, ',','.')::numeric) AS izd FROM izdatnica_stavke left join izdatnica on izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica WHERE roba_prodaja.sifra=izdatnica_stavke.sifra and roba_prodaja.id_skladiste = izdatnica.id_skladiste),0) AS izdatnica,
	coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)-kolicina_koja_je_bila_na_skl) AS kol FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura.is_pocetno_stanje='0' and roba_prodaja.id_skladiste = inventura.id_skladiste),0) as inventura,
	coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke left join povrat_robe on povrat_robe_stavke.broj = povrat_robe.broj WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra and roba_prodaja.id_skladiste = povrat_robe.id_skladiste),0) as otpis,
	coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke WHERE racun_stavke.sifra_robe=caffe_normativ.sifra),0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS racuni,
	coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((SELECT SUM(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)) FROM faktura_stavke WHERE faktura_stavke.sifra = caffe_normativ.sifra),0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ = roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS fakture,
	coalesce((SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric) * coalesce((SELECT SUM(otpremnica_stavke.kolicina) FROM otpremnica_stavke WHERE otpremnica_stavke.sifra_robe=caffe_normativ.sifra and otpremnica_stavke.naplaceno_fakturom = false),0))  FROM caffe_normativ WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra and roba_prodaja.id_skladiste = caffe_normativ.id_skladiste),0) AS otpremnica,
	(SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.iz_poslovnice='{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS izlaz_ms,
	(SELECT coalesce(SUM(medu_poslovnice.kolicina), 0) as kolicina FROM medu_poslovnice WHERE medu_poslovnice.u_poslovnicu='{0}' AND medu_poslovnice.sifra=roba_prodaja.sifra and roba_prodaja.id_skladiste = medu_poslovnice.id_skladiste) AS ulaz_ms
	FROM roba_prodaja
	where roba_prodaja.id_skladiste = '{1}'
) x
order by x.naziv;", cbDucan.SelectedValue, dRow[0].ToString());

                        DataTable DT = classSQL.select(sql, "ks").Tables[0];

                        decimal stanje = 0;

                        foreach (DataRow r in DT.Rows)
                        {
                            decimal.TryParse(r["kolicina"].ToString(), out stanje);

                            sql = string.Format("update roba_prodaja set kolicina = '{0}' where sifra = '{1}' and id_skladiste = '{2}';",
                                stanje.ToString().Replace('.', ','),
                                r["sifra"].ToString(),
                                dRow[0].ToString());
                            classSQL.update(sql);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cbDucan_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDucan.SelectedValue == null)
                    return;

                DataTable DTblagajna = classSQL.select(string.Format("SELECT * FROM blagajna where aktivnost = '1' and id_ducan = '{0}';", cbDucan.SelectedValue), "blagajna").Tables[0];
                cbKasa.DisplayMember = "ime_blagajne";
                cbKasa.ValueMember = "id_blagajna";
                cbKasa.DataSource = DTblagajna;

                DataTable DTblagajna_fakture = classSQL.select(string.Format("SELECT * FROM blagajna where aktivnost = '1' and id_ducan = '{0}';", cbDucan.SelectedValue), "blagajna").Tables[0];
                cmbFaktureKasa.DataSource = DTblagajna_fakture;
                cmbFaktureKasa.DisplayMember = "ime_blagajne";
                cmbFaktureKasa.ValueMember = "id_blagajna";
            }
            catch (Exception)
            {
                throw;
            }
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

        private void chbUseUdsGame_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGame = {0};", (chbUseUdsGame.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbUseEmployees_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGameEmployees = {0};", (chbUseEmployees.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtApiKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGameApiKey = '{0}';", txtApiKey.Text);
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbTestFisklaizacija_Click(object sender, EventArgs e)
        {
            try
            {
                string password = "test_q1w2e3r4";

                var input = Microsoft.VisualBasic.Interaction.InputBox("TEST FISKALIZACIJA");
                if (input == password)
                {
                    chbTestFisklaizacija.Checked = !chbTestFisklaizacija.Checked;
                    classSQL.select_settings(string.Format("update postavke set test_fiskalizacija = {0}", (chbTestFisklaizacija.Checked ? 1 : 0)), "postavke");
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbIspisNapomeneNaMaloprodajnimRacunima_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    string sql = string.Format("update postavke set napomena_na_kraju_racuna = {0};", (chbIspisNapomeneNaMaloprodajnimRacunima.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbNapomenaNaKrajuPredracuna_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set napomena_na_kraju_predracuna = {0};", (chbNapomenaNaKrajuPredracuna.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbRadSaTabletima_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set rad_sa_tabletima = {0};", (chbRadSaTabletima.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets cbJezik values
        /// </summary>
        private void SetJezikComboBox()
        {
            DataTable DTSK = new DataTable();

            DTSK.Columns.Add("id", typeof(int));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add(0, "EN");
            DTSK.Rows.Add(1, "HR");
            DTSK.Rows.Add(2, "IT");

            cbJezik.DataSource = DTSK;
            cbJezik.DisplayMember = "naziv";
            cbJezik.ValueMember = "id";

            cbJezik.SelectedIndex = 1;
        }

        private void cbJezik_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var index = cbJezik.SelectedValue;
                string query = $@"UPDATE postavke SET default_jezik = {index}";
                classSQL.Setings_Update(query);
            }
            catch
            {
                MessageBox.Show("Došlo je do greške kod promjene jezika, molimo nadogradite bazu.", "Greška");
            }
        }

        private void buttonNadograditiProgram_Click(object sender, EventArgs e)
        {
           // string nadogradnjaProgramaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"NadogradnjaPrograma.exe");
            string fileName = $@"NadogradnjaPrograma.exe";
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

            MessageBox.Show($@"Program će se automatski ažurirati.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Threading.Thread.Sleep(1000); // Potrebno zbog toga što environment.Exit() prekida sve procese iako se izvršavaju. Pa dajemo
                                                 // programu još PUNO vremena (gledajući iz perspektive procesora) da se sve završi.
            Process.Start("NadogradnjaPrograma.exe"); // Pokretanje programa za update
            Environment.Exit(0); // Izlaz iz trenutnog programa
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            MessageBox.Show(DTpostavke.Rows[0]["print_narudzbe_bez_pitanja"].ToString());
        }
    }
}