using PCPOS.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PCPOS.PosPrint;
using System.Resources;

namespace PCPOS.Caffe
{
    public partial class frmCaffe : Form
    {
        //private bool useUDS = false;
        private UDS UDSAPI;

        private PartnerCompanyInfoType udsCompany;
        private CustomerInfoType udsCustomer;
        private bool UDSAPI_APPLY_DISCOUNT;
        private decimal UDSAPI_DISCOUNT_BASE = 0;
        private decimal UDSAPI_MAX_SCORES_DISCOUNT = 0;
        private decimal UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
        private int CODE = 0;
        private string[] IT = new string[] { "Bibite", "Cibo", "Ordihi", "R1 Conto", "Opzioni", "Tavoli", "Uscita", "Annulla", "Sconto", "Cancella ultino", "Finisci conto", "Aggiungi al tavolo" };
        public frmCaffe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public int idTrenutneNarudzbe;
        private int idPodgrupa { get; set; }
        private int idGrupa { get; set; }

        public string IznosKartica { get; set; }
        public string IznosGotovina { get; set; }
        public string _OdabraniStol { get; set; }
        public decimal popust_na_cijeli_racun { get; set; }
        public bool _zavrsi { get; set; }
        public bool mozeOtvoritiNapomenu { get; set; }

        //kartica kupca
        public string kartica_kupca { get; set; }

        public decimal karticaIznosZaOduzeti = 0;

        public string DobivenoGotovina { get; set; }
        private bool Bline_display = false;
        public static string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifra_skladiste { get; set; }
        public DataSet DSpostavke;
        private DataTable DTpostavkePrinter;
        private DataTable DTpromocije;
        private DataTable DTpromocije1;
        private DataTable dtArtikli;
        private DataTable DTsend = new DataTable();
        private DataTable DTean = new DataTable();
        private double ukupno = 0;
        private double ukupnoBezRabata = 0;
        private string brRac;
        private string sifraPartnera = "0";
        private int selectedRow = -1;
        private int beauty_osoba = 0;
        public List<OsobeDugNaplata> beauty_dug_naplata = null;

        public static bool posaljiNarudzbeNaStol = false;
        //pola pola za pizze
        private int polpol = 0;

        private bool dodajDjelatnikaNaStavkuRacuna = false;
        private bool dodajOsobuNaRacunAutomatski = false;

        public static bool neuspjelaFiskalizacijaPostoji;
        DateTime datumPrveNeuspjeleFiskalizacije;
        DateTime datumZadnjeMoguceFiskalizacije;
        DateTime datumPrvogRacunaSGreskom;
        DateTime datumZadnjeMoguceFiskalizacijeRacunaSGreskom;
        DateTime danasnjiDatum;
        private bool zmirkaj = false;

        private int polindex = -1;

        private int napomenaindex = -1;
        //this.Bring
        //public frmMenu MainForm { get; set; }
        public string blagajnik_ime = "";
        private DataTable DTremotePostavke;
        private Until.FunkcijeRobno RobnoFunkcije = new Until.FunkcijeRobno();
        private ToolTip ToolTip1 = new ToolTip();
        private DataSet DSstol = new DataSet();

        private string Sukupno = "";
        private string datumOD = "";
        private int caffe_icon_width;
        private int caffe_icon_height;
        private int margin_all;
        private int pages_count;
        private int count_roba;
        private int product_wcount_hcount;
        private int current_page = 1;
        public frmScren mainForm{ get; set; }

        private void frmCaffe_Load(object sender, EventArgs e)
        {
            mozeOtvoritiNapomenu = true;
            neuspjelaFiskalizacijaPostoji = false;
            posaljiNarudzbeNaStol = false;
            zmirkaj = false;
            ProvjeriNeuspjeleFiskalizacije();


            if (neuspjelaFiskalizacijaPostoji)
            {
                danasnjiDatum = DateTime.Now;
                var preostaloVrijemeFiskalizacije = (datumZadnjeMoguceFiskalizacije - danasnjiDatum).TotalHours;
                labelPostojeNeuspjeleFiskalizacije.Text = "POSTOJE NEUSPJELE FISKALIZACIJE! (PREOSTALO: " + preostaloVrijemeFiskalizacije.ToString("#0.00") + " SATI)";
            }

            if (Class.Postavke.is_beauty)
            {
                btnOtpremnica.Text = "Osobe";
                btnPolaPola.Visible = false;
                dgw.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                btnDesert.Enabled = false;
                btnPredjela.Enabled = false;
                btnGlavnoJelo.Enabled = false;
                btnDesert.Visible = false;
                btnPredjela.Visible = false;
                btnGlavnoJelo.Visible = false;
            }

            if (Class.Postavke.is_caffe)
            {
                //98816793336
                // EN = 0, HR = 1, IT = 2 (prilagoditi prema frmPostavke)
                CultureInfo culture = null;
                switch (Postavke.default_jezik)
                {
                    case 0:
                        culture = new CultureInfo("en-GB");
                        break;
                    case 1:
                        culture = new CultureInfo("hr-HR");
                        break;
                    case 2:
                        culture = new CultureInfo("it-IT");
                        break;
                }
                SetCurrentCulture(culture);
                SetButtonNames();
            }

            if (Class.Postavke.UDSGame)
            {
                //string ApiKey = "cEtJOnB9Z0JkenBNfmkyMlgoaylyZHkreHIqblB9akpPaD1dPU9yfVpuNkk9d3AycVk=";

                UDSAPI = new UDS(Class.Postavke.UDSGameApiKey);
                udsCompany = UDSAPI.getCompany();

                if (udsCompany != null)
                {
                    UDSAPI_APPLY_DISCOUNT = (udsCompany.baseDiscountPolicy == "APPLY_DISCOUNT" ? true : false);
                    UDSAPI_DISCOUNT_BASE = udsCompany.marketingSettings.discountBase;
                    UDSAPI_MAX_SCORES_DISCOUNT = udsCompany.marketingSettings.maxScoresDiscount;
                    if (UDSAPI_MAX_SCORES_DISCOUNT > 99)
                    {
                        MessageBox.Show("Nije dozvoljeno fiskaliziranje računa s 0 kn, stoga je maximalni dozvoljeni postotak popusta promijenjen na 99%.");
                        UDSAPI_MAX_SCORES_DISCOUNT = 99;
                    }

                    if (UDSAPI_APPLY_DISCOUNT)
                    {
                        if (UDSAPI_DISCOUNT_BASE > UDSAPI_MAX_SCORES_DISCOUNT)
                        {
                            MessageBox.Show("Osnovni postotak popusta je veči od maksimalnog dozvoljenog postotka.\r\nOsnovni postotak je promjenjen u maksimalni dozvoljeni postotak.");
                            UDSAPI_DISCOUNT_BASE = UDSAPI_MAX_SCORES_DISCOUNT;
                        }
                    }
                }
                else
                {
                    btnOdustaniKartica.Enabled = false;
                    btnOdustaniKartica.Visible = false;
                }

                btnKoristiKarticu.Enabled = false;
                btnKoristiKarticu.Visible = false;
                btnOdustaniKartica.Text = "UDS Game";
            }


            if (Class.Postavke.rad_sa_tabletima)
            {
                btnDodajNaStol.Location = btnGotovina.Location;
                btnGotovina.Enabled = false;
                btnGotovina.Visible = false;
                btnDodajNaStol.Width = 278;
            }

            removeBeautyOsoba();

            idPodgrupa = -1;

            showHideNapomena();

            kartica_kupca = "";
            popust_na_cijeli_racun = 0;
            timer1.Start();
            timer2.Start();
            timer3.Start();

            DataGridViewRow row = dgw.RowTemplate;
            row.Height = 35;

            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
            id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();

            if (!Class.Postavke.is_caffe)
            {
                btnPice.Visible = false;
                btnHrana.Visible = false;
                btnDodajNaStol.Visible = false;
                btnDodajNaStol.Enabled = false;
                btnStolovi.Visible = false;
                btnPolaPola.Visible = false;
                btnNapomena.Visible = false;
                btnDodatak.Visible = false;
                //if (!Class.Postavke.is_beauty)
                //{
                btnGotovina.Size = new Size(277, 59);
                //}
                btnNajProdavanije.Location = new Point(12, 13);
                btnPredjela.Visible = false;
                btnGlavnoJelo.Visible = false;
                btnDesert.Visible = false;
                btnNarudzbe.Visible = false;
            }

            if (Class.Postavke.is_beauty)
            {
                btnDodatak.Text = "BON";
                btnDodatak.Enabled = true;
                btnDodatak.Visible = true;

                btnDodajNaStol.Text = "BON";
                //btnDodajNaStol.Visible = true;
                //btnDodajNaStol.Enabled = true;

                btnNapomena.Visible = true;
                btnNapomena.Font = new Font("Arial", 9.5F, FontStyle.Bold);
                btnNapomena.Text = "Dodaj osobu";
            }

            DTremotePostavke = classSQL.select("SELECT * FROM remote_postavke", "remote_postavke").Tables[0];
            DTpromocije = classSQL.select("SELECT * FROM promocije WHERE do_datuma >='" +
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AND od_datuma <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'", "promocije").Tables[0];

            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            lblPrijavljen.Text = "Prijavljen: " + classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            brRac = brojRacuna();

            sifra_skladiste = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            SetGrupe(1);

            lblKupac.Text = "";
            sifraPartnera = "0";

            try
            {
                if (DTpostavkePrinter.Rows[0]["port_display_enable"].ToString() == "1")
                {
                    Bline_display = true;
                    spLineDisplay.PortName = DTpostavkePrinter.Rows[0]["port_display"].ToString();
                    spLineDisplay.Open();
                }
            }
            catch { }

            try
            {
                btnPredjela.Visible = false;
                btnDesert.Visible = false;
                btnGlavnoJelo.Visible = false;

                if (Class.Postavke.is_caffe)
                {
                    DataTable DDt = classSQL.select("SELECT id_podgrupa FROM roba WHERE id_podgrupa='2'", "roba").Tables[0];
                    if (DDt.Rows.Count > 0)
                    {
                        btnPredjela.Visible = true;
                        btnDesert.Visible = true;
                        btnGlavnoJelo.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (Class.Postavke.is_caffe)
            {
                if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() != "1")
                {
                    btnNarudzbe.Visible = false;
                }
            }

            string ZBS = ZadnjiBrojSmjene();

            if (ZBS == "null")
            {
                frmPocetnoStanje ps = new frmPocetnoStanje();
                ps.ShowDialog(this);
            }
            else
            {
                string sql = "SELECT * FROM smjene WHERE id='" + ZBS + "' AND smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ";
                DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

                if (DT_smjena.Rows.Count > 0)
                {
                    if (DT_smjena.Rows[0]["zavrsetak"].ToString() != "")
                    {
                        frmPocetnoStanje ps = new frmPocetnoStanje();
                        ps.ShowDialog(this);
                    }
                }
            }

            if (DSpostavke.Tables[0].Rows[0]["preferirajSifre"].ToString() == "1")
            {
                btnPice.Select();
                flpArtikli.Controls.Clear();
                frmRadSaSiframa rs = new frmRadSaSiframa();
                rs.MainForm = this;
                rs.ShowDialog(this);
            }

            PrikazZadnjegRacuna();

            _zavrsi = false;

            if (File.Exists("hamer"))
            {
                flpArtikli.Controls.Clear();
                frmRadSaSiframa rs = new frmRadSaSiframa();
                rs.MainForm = this;
                rs.ShowDialog(this);
            }

            lblKarticaKorisnik.Visible = Util.Korisno.kartica_kupca;
            lblKarticaUkupno.Visible = Util.Korisno.kartica_kupca;

            if (Util.Korisno.kartica_kupca)
            {
                timeKarticaKupca.Start();
            }

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            btnNajProdavanije.PerformClick();
        }

        private void showHideNapomena()
        {
            try
            {
                string sql = "SELECT * FROM napomene;";
                DataSet dsNapomena = classSQL.select(sql, "napomena");

                if (dsNapomena != null && dsNapomena.Tables.Count > 0 && dsNapomena.Tables[0] != null && dsNapomena.Tables[0].Rows.Count > 0)
                {
                    btnNapomena.Visible = true;
                }
                else
                {
                    btnNapomena.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProvjeriNeuspjeleFiskalizacije()
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

                //   MessageBox.Show(datumZadnjeMoguceFiskalizacije.ToString());
            }
            else
            {
                neuspjelaFiskalizacijaPostoji = false;
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene WHERE smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "null";
            }
        }

        private void SetGrupe(int idPodgrupa = 1)
        {
            DataTable DT = new DataTable();
            string sql = string.Format("SELECT * FROM grupa WHERE (id_podgrupa ='{0}' or id_podgrupa = '3') AND aktivnost='1' ORDER BY grupa ASC", idPodgrupa);
            if (idPodgrupa != 1)
            {
                sql = string.Format("SELECT * FROM grupa WHERE id_podgrupa ='{0}' AND aktivnost = '1' ORDER BY grupa ASC", idPodgrupa);
            }
            DT = classSQL.select(sql, "grupa").Tables[0];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ime_gumba = DT.Rows[i]["grupa"].ToString();
                string name_gumba = DT.Rows[i]["id_grupa"].ToString();

                Button btnGrupa = new Button();
                btnGrupa.Text = ime_gumba;
                btnGrupa.Name = name_gumba;
                btnGrupa.BackColor = Color.White;
                btnGrupa.Cursor = Cursors.Hand;
                btnGrupa.FlatAppearance.BorderColor = Color.FromArgb(31, 53, 79);
                btnGrupa.FlatAppearance.BorderSize = 1;
                btnGrupa.FlatAppearance.CheckedBackColor = Color.Transparent;
                btnGrupa.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                btnGrupa.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
                btnGrupa.FlatStyle = FlatStyle.Flat;
                btnGrupa.Font = new Font("Arial", 10F);
                btnGrupa.Size = new Size(170, 60);
                btnGrupa.TabIndex = 2;
                btnGrupa.UseVisualStyleBackColor = false;
                btnGrupa.Click += new EventHandler(this.btnGrupa_Click);
                btnGrupa.KeyDown += new KeyEventHandler(this.btnPice_KeyDown);
                btnGrupa.Enter += new EventHandler(this.btnPice_Enter);
                flpGrupe.Controls.Add(btnGrupa);

                if (i == 0)
                {
                    btnGrupa.PerformClick();
                }
            }
        }

        private void btnGrupa_Click(object sender, EventArgs e)
        {
            //koristi se za dodatak
            selectedRow = -1;
            current_page = 1;
            Control conGrupa = (Control)sender;
            lblnativGrupe.Text = conGrupa.Text;

            idGrupa = Convert.ToInt32(conGrupa.Name);
            label1.Tag = idGrupa;

            dtArtikli = new DataTable();

            dtArtikli = classSQL.select(string.Format("SELECT * FROM roba WHERE id_grupa='{0}' AND aktivnost = '1' ORDER BY naziv ASC LIMIT 200;", conGrupa.Name.ToString()), "roba").Tables[0];
            lblnativGrupe.Tag = classSQL.select(string.Format("select case when is_polpol = '1' then id_grupa else '0' end as polpol from grupa where id_grupa = '{0}';", conGrupa.Name.ToString()), "grupa").Tables[0].Rows[0]["polpol"];

            if (Convert.ToInt32(lblnativGrupe.Tag) != 0)
            {
                btnPolaPola.Visible = true;
            }
            else
            {
                btnPolaPola.Visible = false;
            }

            getArtikli();

            startTimerKartica(true, true, true);
        }

        private void btnPages_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean((sender as Button).Tag.ToString()))
            {
                current_page++;
            }
            else
            {
                current_page--;
            }
            getArtikli();
        }

        public void getArtikli(bool relodePostavke = true)
        {
            if (relodePostavke)
            {
                DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            }

            flpArtikli.Controls.Clear();

            ArrayList artikli_po_stranici = setValuesForArtiklButtons();
            int start = 0;
            int stop = dtArtikli.Rows.Count;
            int brojStranica = artikli_po_stranici.Count;
            if (brojStranica > 1)
            {
                stop = 0;
                for (int i = 0; i < current_page - 1; i++)
                {
                    start += (Convert.ToInt32(artikli_po_stranici[i]));
                }
                for (int i = 0; i < current_page; i++)
                {
                    stop += (Convert.ToInt32(artikli_po_stranici[i]));
                }
            }

            try
            {
                Color color = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["button_color_icona"].ToString());
                int step = 25;
                int newA = (color.A - step < 1 ? (color.A + step > 255 ? color.A - step : color.A + step) : color.A - step);
                int newR = (color.R - step < 1 ? (color.R + step > 255 ? color.R - step : color.R + step) : color.R - step);
                int newG = (color.G - step < 1 ? (color.G + step > 255 ? color.G - step : color.G + step) : color.G - step);
                int newB = (color.B - step < 1 ? (color.B + step > 255 ? color.B - step : color.B + step) : color.B - step);

                if (current_page > 1 && current_page <= brojStranica)
                {
                    //◄←

                    color = Color.FromArgb(newA, newR, newG, newB);
                    Button btnGrupa = new Button();
                    btnGrupa.Text = "◄─";
                    btnGrupa.Name = "btnPrew";
                    btnGrupa.Tag = false;
                    btnGrupa.BackgroundImageLayout = ImageLayout.Stretch;
                    btnGrupa.BackColor = color;
                    btnGrupa.ForeColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["font_color_icona"].ToString());
                    btnGrupa.Cursor = Cursors.Hand;
                    btnGrupa.FlatAppearance.BorderColor = Color.LightSlateGray;
                    btnGrupa.FlatAppearance.BorderSize = 0;
                    btnGrupa.FlatAppearance.CheckedBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btnGrupa.FlatStyle = FlatStyle.Flat;
                    btnGrupa.Font = new Font("Arial", 26, Util.Korisno.biu(DSpostavke.Tables[0].Rows[0]["bui_icona"].ToString()));
                    btnGrupa.Size = new Size(caffe_icon_width, caffe_icon_height);
                    //btnGrupa.TabIndex = 2;
                    btnGrupa.TabStop = false;

                    btnGrupa.Margin = new Padding(margin_all);

                    btnGrupa.UseVisualStyleBackColor = false;
                    btnGrupa.Click += new EventHandler(this.btnPages_Click);
                    btnGrupa.MouseEnter += new EventHandler(this.pic_MouseEnter1);
                    btnGrupa.MouseLeave += new EventHandler(this.pic_MouseLeave1);
                    btnGrupa.KeyDown += new KeyEventHandler(this.btnPice_KeyDown);
                    btnGrupa.Enter += new EventHandler(this.btnPice_Enter);

                    flpArtikli.Controls.Add(btnGrupa);
                }

                for (int i = start; i < stop; i++)
                {
                    string ime_gumba = dtArtikli.Rows[i]["naziv"].ToString();
                    string name_gumba = dtArtikli.Rows[i]["sifra"].ToString();

                    Button btnGrupa = new Button();
                    btnGrupa.Text = ime_gumba;
                    btnGrupa.Name = name_gumba;
                    btnGrupa.BackgroundImageLayout = ImageLayout.Stretch;
                    btnGrupa.BackColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["button_color_icona"].ToString());
                    btnGrupa.ForeColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["font_color_icona"].ToString());
                    btnGrupa.Cursor = Cursors.Hand;
                    btnGrupa.FlatAppearance.BorderColor = Color.LightSlateGray;
                    btnGrupa.FlatAppearance.BorderSize = 0;
                    btnGrupa.FlatAppearance.CheckedBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btnGrupa.FlatStyle = FlatStyle.Flat;
                    btnGrupa.Font = new Font("Arial", Convert.ToInt16(DSpostavke.Tables[0].Rows[0]["font_size_icona"]), Util.Korisno.biu(DSpostavke.Tables[0].Rows[0]["bui_icona"].ToString()));
                    btnGrupa.Size = new Size(caffe_icon_width, caffe_icon_height);
                    btnGrupa.TabIndex = 2;

                    btnGrupa.Margin = new Padding(margin_all);

                    btnGrupa.UseVisualStyleBackColor = false;
                    btnGrupa.Click += new EventHandler(this.SetRoba);
                    btnGrupa.MouseEnter += new EventHandler(this.pic_MouseEnter1);
                    btnGrupa.MouseLeave += new EventHandler(this.pic_MouseLeave1);
                    btnGrupa.KeyDown += new KeyEventHandler(this.btnPice_KeyDown);
                    btnGrupa.Enter += new EventHandler(this.btnPice_Enter);
                    ToolTip1.SetToolTip(btnGrupa, "Cijena: " + Convert.ToDouble(dtArtikli.Rows[i]["mpc"].ToString()).ToString("#0.00") + " kn\r\nŠifra: " + dtArtikli.Rows[i]["sifra"].ToString() + "");
                    SetBorders(btnGrupa, dtArtikli.Rows[i]["border_color"].ToString());
                    SetStyle(btnGrupa, dtArtikli.Rows[i]["button_style"].ToString());

                    flpArtikli.Controls.Add(btnGrupa);
                }

                if (brojStranica > 1 && current_page < brojStranica)
                {
                    //►→─
                    color = Color.FromArgb(newA, newR, newG, newB);
                    Button btnGrupa = new Button();
                    btnGrupa.Text = "─►";
                    btnGrupa.Name = "btnNext";
                    btnGrupa.Tag = true;
                    btnGrupa.BackgroundImageLayout = ImageLayout.Stretch;
                    btnGrupa.BackColor = color;
                    btnGrupa.ForeColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["font_color_icona"].ToString());
                    btnGrupa.Cursor = Cursors.Hand;
                    btnGrupa.FlatAppearance.BorderColor = Color.LightSlateGray;
                    btnGrupa.FlatAppearance.BorderSize = 0;
                    btnGrupa.FlatAppearance.CheckedBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btnGrupa.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btnGrupa.FlatStyle = FlatStyle.Flat;
                    btnGrupa.Font = new Font("Arial", 26, Util.Korisno.biu(DSpostavke.Tables[0].Rows[0]["bui_icona"].ToString()));
                    btnGrupa.Size = new Size(caffe_icon_width, caffe_icon_height);
                    btnGrupa.TabIndex = 2;

                    btnGrupa.Margin = new Padding(margin_all);

                    btnGrupa.UseVisualStyleBackColor = false;
                    btnGrupa.Click += new EventHandler(this.btnPages_Click);
                    btnGrupa.MouseEnter += new EventHandler(this.pic_MouseEnter1);
                    btnGrupa.MouseLeave += new EventHandler(this.pic_MouseLeave1);
                    btnGrupa.KeyDown += new KeyEventHandler(this.btnPice_KeyDown);
                    btnGrupa.Enter += new EventHandler(this.btnPice_Enter);

                    flpArtikli.Controls.Add(btnGrupa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void setWidthHeightMarginForArtiklButtons()
        {
            caffe_icon_width = Convert.ToInt32(DSpostavke.Tables[0].Rows[0]["caffe_icon_width"].ToString());
            caffe_icon_height = Convert.ToInt32(DSpostavke.Tables[0].Rows[0]["cafe_icon_height"].ToString());
            margin_all = 3;
        }

        public ArrayList setValuesForArtiklButtons()
        {
            setWidthHeightMarginForArtiklButtons();

            int container_width = flpArtikli.Width - (flpArtikli.Padding.Left + flpArtikli.Padding.Right) - 2;
            int container_height = flpArtikli.Height - (flpArtikli.Padding.Top + flpArtikli.Padding.Bottom) - 2;
            int wcount = (caffe_icon_width >= container_width ? container_width / (50 + (2 * margin_all)) : container_width / (caffe_icon_width + (2 * margin_all)));
            int hcount = container_height / (caffe_icon_height + (2 * margin_all));
            product_wcount_hcount = wcount * hcount;
            count_roba = dtArtikli.Rows.Count;
            pages_count = 0;
            ArrayList number_per_page = new ArrayList();
            while (count_roba > 0)
            {
                pages_count++;
                int oduzmi = 0;
                if ((pages_count == 1 && count_roba <= product_wcount_hcount) || count_roba < product_wcount_hcount)
                {
                    oduzmi = count_roba;
                }

                //if (count_roba <= product_wcount_hcount)
                //{ // ako je manje ili jednako artikala kao umnozak
                //    oduzmi = count_roba;
                //    if (pages_count > 1 && oduzmi == product_wcount_hcount)
                //    {
                //        oduzmi--;
                //        if (count_roba - oduzmi > 0)
                //            oduzmi--;
                //    }
                //}
                else
                { //if (count_roba > product_wcount_hcount){
                    if (pages_count == 1)
                    {
                        oduzmi = product_wcount_hcount - 1;
                    }
                    else
                    {
                        oduzmi = product_wcount_hcount - 2;
                    }

                    //if (pages_count == 1 || count_roba <= (product_wcount_hcount - 1))
                    //    oduzmi = product_wcount_hcount - 1;

                    //if (oduzmi > count_roba)
                    //    oduzmi = count_roba;

                    //if (count_roba == product_wcount_hcount)
                    //    oduzmi = product_wcount_hcount;
                }
                count_roba -= oduzmi;

                number_per_page.Add(oduzmi);
            }

            if (pages_count < current_page)
                current_page = pages_count;

            return number_per_page;
        }

        private void SetStyle(Button btnGrupa, string btnName)
        {
            try
            {
                if (btnName == "standard")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
                }
                else if (btnName == "btnBordo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.bordo;
                }
                else if (btnName == "btnTamno_crvena")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.crveno_crna;
                }
                else if (btnName == "btnLjubicasta")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.ljubicasto;
                }
                else if (btnName == "btnNarandasta")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.naranda;
                }
                else if (btnName == "btnPlavo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.plavo;
                }
                else if (btnName == "btnSivo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.sivo;
                }
                else if (btnName == "btnTamno_sivo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.tamno_siva;
                }
                else if (btnName == "btnNebo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.vodena;
                }
                else if (btnName == "btnZeleno")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.zelena;
                }
                else if (btnName == "btnZuto")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.zuto;
                }
                else
                {
                    //btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetBorders(Button btnGrupa, string border_color)
        {
            try
            {
                if (border_color != "" && border_color != "0")
                {
                    if (border_color == "Bez boje")
                    {
                        btnGrupa.FlatAppearance.BorderSize = 0;
                    }
                    else if (border_color == "Crno")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Black;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Bijelo")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.White;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Plavo")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Blue;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Zeleno")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Green;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Žuto")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Yellow;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Forest Green")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.ForestGreen;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "SkyBlue")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.SkyBlue;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Sivo")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Gray;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Violet")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Violet;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Mangeta")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Magenta;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Medium purple")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.MediumPurple;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                    else if (border_color == "Aqua")
                    {
                        btnGrupa.FlatAppearance.BorderColor = Color.Aqua;
                        btnGrupa.FlatAppearance.BorderSize = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void PaintRows(DataGridView dg)
        {
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (dg.Columns["dod"] != null && dg.Rows[0].Cells["dod"].Value != null && dg.Rows[i].Cells["dod"].Value.ToString() == "1" && DSpostavke.Tables[0].Rows[0]["dodatak_na_artikl"].ToString() == "1")
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = SystemColors.ControlLight;
                }
                else if (dg.Columns["dod"] != null && dg.Rows[0].Cells["dod"].Value != null && dg.Rows[i].Cells["dod"].Value.ToString() == "2")
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = SystemColors.ControlLight;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 35;
        }

        private void SetRoba(string sifra)
        {
            DataTable DT = new DataTable();
            string sql = @"
                        SELECT
                        roba.naziv, roba.mpc, roba.nbc, roba.porez, roba.sifra, roba.porez_potrosnja,
                        (select case when is_dodatak = true then 1 else 0 end as dod from grupa where id_grupa = roba.id_grupa) as dod,
                        id_grupa, id_podgrupa
                        FROM roba
                        WHERE roba.sifra = '" + sifra + @"';";

            DT = classSQL.select(sql, "roba").Tables[0];

            if (DT.Rows.Count > 0)
            {
                //provjera dali su artikli iz iste grupe za pola-pola
                int newpolindex = -1;
                if (polindex != -1)
                {
                    sql = "select id_grupa, 'btn' as tip from roba where sifra = '" + sifra + "' " +
                            "union " +
                            "select id_grupa, 'dgv' as tip from roba where sifra = '" + dgw.Rows[polindex].Cells["sifra"].Value + "'";
                    DataTable dtPolPol = classSQL.select(sql, "roba").Tables[0];
                    if (Convert.ToInt32(dtPolPol.Rows[0]["id_grupa"]) != Convert.ToInt32(dtPolPol.Rows[1]["id_grupa"]))
                    {
                        MessageBox.Show("Artikli nisu iz iste grupe.");
                        return;
                    }
                    else
                    {
                        int m = dgw.Rows.Count;
                        if (m == 1)
                        {
                            newpolindex = polindex;
                        }
                        else
                        {
                            for (int i = polindex; i < m; i++)
                            {
                                if ((i + 1 < m && dgw.Rows[i + 1].Cells["dod"].Value.ToString() == "0") || i + 1 == m)
                                {
                                    newpolindex = i;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (DT.Rows[0]["dod"] != null && DT.Rows[0]["dod"].ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt32(DT.Rows[0]["dod"]) == 0)
                    {
                        selectedRow = -1;
                    }
                }

                double kolicina = 1;
                double.TryParse(DT.Rows[0]["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double mpc);
                double.TryParse(DT.Rows[0]["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double porez);
                double.TryParse(DT.Rows[0]["porez_potrosnja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double pnp);
                double pdv_stavka;
                double Porez_potrosnja_stavka;

                try
                {
                    if (Bline_display)
                    {
                        string naziv_artikla = DT.Rows[0]["naziv"].ToString();
                        if (naziv_artikla.Length > 10)
                        {
                            naziv_artikla = naziv_artikla.Remove(10);
                        }

                        naziv_artikla = naziv_artikla.Replace("č", "c");
                        naziv_artikla = naziv_artikla.Replace("Č", "C");
                        naziv_artikla = naziv_artikla.Replace("ž", "z");
                        naziv_artikla = naziv_artikla.Replace("Ž", "Z");
                        naziv_artikla = naziv_artikla.Replace("ć", "c");
                        naziv_artikla = naziv_artikla.Replace("Ć", "C");
                        naziv_artikla = naziv_artikla.Replace("đ", "d");
                        naziv_artikla = naziv_artikla.Replace("Đ", "D");
                        naziv_artikla = naziv_artikla.Replace("š", "s");
                        naziv_artikla = naziv_artikla.Replace("Š", "S");

                        spLineDisplay.Write(Convert.ToString((char)12));
                        spLineDisplay.WriteLine(naziv_artikla + " " + mpc.ToString("#0.00") + " kn");
                    }
                }
                catch { }

                double PreracunataStopaPDV = Convert.ToDouble((100 * porez) / (100 + porez + pnp));
                pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * pnp) / (100 + porez + pnp));
                Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                decimal nbc;
                decimal.TryParse(DT.Rows[0]["nbc"].ToString(), out nbc);
                if (selectedRow == -1 && polindex == -1)
                {
                    dgw.Rows.Add(
                        DT.Rows[0]["naziv"].ToString(),
                        (polindex != -1 ? 0.5 : kolicina),
                        mpc.ToString("#0.00"),
                        DT.Rows[0]["sifra"].ToString(),
                        DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(),
                        DT.Rows[0]["porez"].ToString(),
                        (Class.Postavke.UDSGame && udsCustomer != null && UDSAPI_APPLY_DISCOUNT ? UDSAPI_DISCOUNT_BASE.ToString() : "0"),
                        mpc - (pdv_stavka + Porez_potrosnja_stavka),
                        DT.Rows[0]["porez_potrosnja"].ToString(),
                        "",
                        "",
                        nbc.ToString("#0.00000"),
                        (btnDodatak.BackColor == Color.White ? DT.Rows[0]["dod"].ToString() : "0"),
                        (polindex != -1 ? polpol.ToString() : ""),
                        DT.Rows[0]["id_podgrupa"].ToString()
                    );

                    dgw.Rows[dgw.RowCount - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                    dgw.ClearSelection();
                    dgw.Rows[dgw.Rows.Count - 1].Selected = true;
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
                }
                else
                {
                    DataGridViewRow drNew = (DataGridViewRow)dgw.Rows[0].Clone();

                    drNew.Cells[0].Value = DT.Rows[0]["naziv"].ToString();
                    drNew.Cells[1].Value = (polindex != -1 ? 0.5 : kolicina);
                    drNew.Cells[2].Value = mpc.ToString("#0.00");//(polindex != -1 ? (mpc / 2).ToString("#0.00") : mpc.ToString("#0.00"));
                    drNew.Cells[3].Value = DT.Rows[0]["sifra"].ToString();
                    drNew.Cells[4].Value = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                    drNew.Cells[5].Value = DT.Rows[0]["porez"].ToString();
                    drNew.Cells[6].Value = (Class.Postavke.UDSGame && udsCustomer != null && UDSAPI_APPLY_DISCOUNT ? UDSAPI_DISCOUNT_BASE.ToString() : "0");
                    drNew.Cells[7].Value = mpc - (pdv_stavka + Porez_potrosnja_stavka);
                    drNew.Cells[8].Value = DT.Rows[0]["porez_potrosnja"].ToString();
                    drNew.Cells[9].Value = "";
                    drNew.Cells[10].Value = "";
                    drNew.Cells[11].Value = nbc.ToString("#0.00000");
                    drNew.Cells[12].Value = DT.Rows[0]["dod"].ToString();
                    drNew.Cells[13].Value = (polindex != -1 ? polpol.ToString() : "");

                    dgw.Rows.Insert((polindex != -1 ? newpolindex + 1 : selectedRow + 1), drNew);
                }

                if (DSpostavke.Tables[0].Rows[0]["obavjeti_ako_nema_repromaterijala"].ToString() == "1")
                {
                    RobnoFunkcije.ObavjestiAkoNaSkladistuImaManjeOdNule(DT.Rows[0]["sifra"].ToString(), DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString());
                }

                IzracunUkupno();
                try
                {
                    if (Bline_display)
                    {
                        spLineDisplay.WriteLine((char)13 + "UKUPNO:" + ukupno.ToString("#0.00") + " kn");
                    }
                }
                catch { }
                PaintRows(dgw);
            }

            if (selectedRow == -1)
            {
                startTimerKartica(true, true, true);
            }
            else
            {
                startTimerKartica(false, true, true);
            }
        }

        private int brojac = 0;
        private int brojac_po3 = 1;
        private double brojac_iznos = 1000000;
        private int RoW = 0;

        private int brojac1 = 0;
        private int brojac_po2 = 1;
        private double brojac_iznos1 = 1000000;
        private int RoW1 = 0;

        public void ProvjeraPromocije(string sifra, int rowBR)
        {
            if (DTpromocije.Rows.Count > 0)
            {
                bool a_true = false;
                bool a1_true = false;
                bool a2_true = false;

                for (int i = 0; i < DTpromocije.Rows.Count; i++)
                {
                    int row1 = 0;
                    int row2 = 0;
                    int row3 = 0;
                    int row_za_popust = 0;

                    if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2=3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();

                        for (int a = 0; a < dgw.Rows.Count - 1; a++)
                        {
                            if (dgw.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art2.Trim())
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                                else if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art3.Trim())
                                {
                                    a2_true = true;
                                    row_za_popust = a;
                                }

                                if (a_true == true && a1_true == true && a2_true == true && dgw.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgw.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                                {
                                    dgw.Rows[row1].Cells["kolicina"].ReadOnly = true;
                                    dgw.Rows[row2].Cells["kolicina"].ReadOnly = true;
                                    dgw.Rows[row_za_popust].Cells["kolicina"].ReadOnly = true;
                                    dgw.Rows[row1].Cells["zakljucaj"].Value = 1;
                                    dgw.Rows[row2].Cells["zakljucaj"].Value = 1;
                                    dgw.Rows[row_za_popust].Cells["zakljucaj"].Value = 1;
                                    dgw.Rows[row_za_popust].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                                    dgw.Rows[row_za_popust].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row_za_popust].Cells["popust"].Value) / 100)));
                                    startTimerKartica(true, true, true);
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        for (int a = 0; a < dgw.Rows.Count; a++)
                        {
                            if (dgw.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    dgw.Rows[a].Cells["zakljucaj"].Value = 1;
                                    dgw.Rows[a].Cells["mpc"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString()) * Convert.ToDouble(DTpromocije.Rows[i]["popust1"].ToString()) / 100)));
                                    dgw.Rows[a].Cells["vpc"].Value = Convert.ToDouble(dgw.Rows[a].Cells["mpc"].Value) / (Convert.ToDouble(dgw.Rows[rowBR].Cells["porez_potrosnja"].FormattedValue.ToString()) + Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()));
                                    startTimerKartica(true, true, true);
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        for (int a = 0; a < dgw.Rows.Count; a++)
                        {
                            if (dgw.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                            }
                        }
                        if (a_true == true && a1_true == true && dgw.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgw.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgw.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[row2].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgw.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgw.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgw.Rows[row1].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row1].Cells["popust"].Value) / 100)));
                            dgw.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgw.Rows[row2].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row2].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2&3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();
                        for (int a = 0; a < dgw.Rows.Count; a++)
                        {
                            if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                            {
                                a_true = true;
                                row1 = a;
                            }
                            else if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                            {
                                a1_true = true;
                                row2 = a;
                            }
                            else if (dgw.Rows[a].Cells["sifra"].FormattedValue.ToString() == art3)
                            {
                                a2_true = true;
                                row3 = a;
                            }
                        }
                        if (a_true == true && a1_true == true && a2_true == true && dgw.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgw.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgw.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[row2].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[row3].Cells["kolicina"].ReadOnly = true;

                            dgw.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgw.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgw.Rows[row3].Cells["zakljucaj"].Value = 1;

                            dgw.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgw.Rows[row1].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row1].Cells["popust"].Value) / 100)));
                            dgw.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgw.Rows[row2].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row2].Cells["popust"].Value) / 100)));
                            dgw.Rows[row3].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgw.Rows[row3].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.Rows[row3].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[row3].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[row3].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a+a=a")
                    {
                        brojac++;
                        if (brojac == 3)
                        {
                            for (int a = (brojac_po3 - 3); a < brojac_po3; a++)
                            {
                                if (Convert.ToDouble(dgw.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos)
                                {
                                    brojac_iznos = Convert.ToDouble(dgw.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgw.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[RoW].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgw.Rows[RoW].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[RoW].Cells["popust"].Value) / 100);
                            IzracunUkupno();
                            brojac = 0;
                        }
                        brojac_po3++;
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a=a")
                    {
                        brojac1++;
                        if (brojac1 == 2)
                        {
                            for (int a = (brojac_po2 - 2); a < brojac_po2; a++)
                            {
                                if (Convert.ToDouble(dgw.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos1)
                                {
                                    brojac_iznos1 = Convert.ToDouble(dgw.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW1 = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgw.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgw.Rows[RoW1].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgw.Rows[RoW1].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgw.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgw.Rows[RoW].Cells["popust"].Value) / 100);
                            IzracunUkupno();
                            brojac1 = 0;
                        }
                        brojac_po2++;
                    }
                }
            }
        }

        private void SetRoba(object sender, EventArgs e)
        {
            Control conArtikl = (Control)sender;
            SetRoba(conArtikl.Name.ToString());
        }

        public void IzracunUkupno()
        {
            ukupno = 0;
            ukupnoBezRabata = 0;
            decimal _popust = 0;
            decimal _kolicina = 0;
            decimal _mpc = 0;

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                _popust = Convert.ToDecimal(dgw.Rows[i].Cells["rabat"].FormattedValue.ToString());
                _kolicina = Convert.ToDecimal(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                _mpc = Convert.ToDecimal(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString()) * _kolicina;

                ukupnoBezRabata += Convert.ToDouble(_mpc);

                _mpc = _mpc - (_mpc * _popust / 100);

                ukupno = Convert.ToDouble(_mpc) + ukupno;
            }

            if (dgw.Rows.Count == 0)
            {
                ukupno = 0;
            }

            lblUkupno.Text = ukupno.ToString("#0.00") + " Kn";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w + 2, h);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w - 2, h);
        }

        private Color colDef;
        private Color colNapomena;

        private void pic_MouseEnter1(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    Button btn = (Button)sender;
                    int i = 0;
                    bool b = false;
                    if (btn.Tag != null && !Int32.TryParse(btn.Tag.ToString(), out i) && bool.TryParse(btn.Tag.ToString(), out b))
                    {
                        sender = colorMouseOn(sender, 30);
                        return;
                    }
                    sender = colorMouseOn(sender, 25);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private object colorMouseOn(object sender, int c)
        {
            Button PB = ((Button)sender);

            colDef = PB.BackColor;

            int r = colDef.R;
            int g = colDef.G;
            int b = colDef.B;

            if (r - c > c) { r -= c; } else { r += c; }
            if (g - c > c) { g -= c; } else { g += c; }
            if (b - c > c) { b -= c; } else { b += c; }

            Color colTemp = new Color();

            colTemp = Color.FromArgb(colDef.A, r, g, b);
            if (c == 30)
                colNapomena = colTemp;

            PB.FlatAppearance.CheckedBackColor = Color.Transparent;
            PB.FlatAppearance.MouseDownBackColor = Color.Transparent;
            PB.FlatAppearance.MouseOverBackColor = colTemp;
            PB.FlatStyle = FlatStyle.Flat;
            PB.BackgroundImageLayout = ImageLayout.Stretch;

            sender = PB;
            return sender;
        }

        private void pic_MouseLeave1(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    Button btn = (Button)sender;
                    int i = 0;
                    bool b = false;
                    if (btn.Tag != null && !Int32.TryParse(btn.Tag.ToString(), out i) && bool.TryParse(btn.Tag.ToString(), out b))
                    {
                        sender = colorMouseOn(sender, 30);
                        return;
                    }

                    Button btnGrupa = ((Button)sender);
                    btnGrupa.BackColor = colDef;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void btnOdjava_Click(object sender, EventArgs e)
        {
             if (Properties.Settings.Default.id_dopustenje ==2)
                 this.Close();
             else
             {
                this.Hide();
                mainForm.btnOdjava.PerformClick();
                this.Close();
             }
             /*
            switch (Properties.Settings.Default.id_dopustenje)
            {
                case 1: // Izbacen
                    this.Close();
                    break;
                case 2:
                    this.Close();
                    break;
                case 3:
                    CloseForm();
                    break;
                case 4:
                    CloseForm();
                    break;
                    /* default:
                         this.Close();
            }
            
            break;*/
        }

        private void btnDodajNaStol_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.is_beauty)
            {
                return;
            }

            DataTable DTsend = new DataTable();

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
            DTsend.Columns.Add("stol");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DTsend.Columns.Add("jelo");
            DTsend.Columns.Add("dod");
            DTsend.Columns.Add("pol");
            DTsend.Columns.Add("id_podgrupa");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                startTimerKartica(true, true, true);
                setDefUkupnoAfterKartica(true, true);
                return;
            }

            if (DSpostavke.Tables[0].Rows[0]["stolovi_razvrstavanje"].ToString() == "1")
            {
                frmOdabirStolaCustom odbStola = new frmOdabirStolaCustom();
                _OdabraniStol = null;
                odbStola.CAFFE = this;
                odbStola.ShowDialog(this);
            }
            else
            {
                frmOdabirStola odbStola = new frmOdabirStola();

                _OdabraniStol = null;
                odbStola.CAFFE = this;
                odbStola.ShowDialog(this);
            }

            if (_OdabraniStol != null)
            {
                //int broj_narudzbe = getBrojNarudzbe(id_ducan);
                int broj_narudzbe = GetZadnjiBrojNarudzbe(id_ducan);
                int zadnji_broj = 0;
                DataTable DTzadnji = classSQL.select("SELECT MAX(br) FROM na_stol WHERE id_stol='" + _OdabraniStol + "' AND id_poslovnica='" + DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString() + "'", "na_stol").Tables[0];
                if (DTzadnji.Rows.Count > 0)
                {
                    Int32.TryParse(DTzadnji.Rows[0][0].ToString(), out zadnji_broj);
                }

                string dat = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");

                if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() == "1")
                {
                    DataTable DT = classSQL.select("SELECT datum FROM caffe_narudzbe GROUP BY datum ORDER BY datum", "caffe_narudzbe").Tables[0];

                    if (DT.Rows.Count >= 10)
                    {
                        //classSQL.update("DELETE FROM caffe_narudzbe WHERE datum='" + DT.Rows[0]["datum"].ToString() + "' and broj_narudzbe != '" + broj_narudzbe + "'");
                    }
                }

                bool sankBool = false;
                bool kuhinjaBool = false;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    int dodatak = Convert.ToInt32(dg(i, "dod"));
                    int idPodgrupa = 0;
                    try
                    {
                        idPodgrupa = Convert.ToInt32(dg(i, "id_podgrupa"));
                    }
                    //Ovo je potrebno zbog F1
                    catch(Exception ex)
                    {
                        //MessageBox.Show("A");
                        int sifraArtikla = Convert.ToInt32(dg(i, "sifra"));
                        string sqlCmd = "SELECT * FROM roba WHERE sifra='" + sifraArtikla+"'";
                        DataTable dt = classSQL.select(sqlCmd, "roba").Tables[0];
                        idPodgrupa=Int32.Parse(dt.Rows[0]["id_podgrupa"].ToString());
                    }

                    if (dodatak == 0)
                    {
                        if (idPodgrupa == 1 && !sankBool)
                            sankBool = true;
                        else if (idPodgrupa == 2 && !kuhinjaBool)
                            kuhinjaBool = true;
                    }

                    decimal.TryParse(dg(i, "mpc").ToString().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mpc);
                    decimal.TryParse(dg(i, "rabat").ToString().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rabat);
                    string sql = "INSERT INTO na_stol (br,jelo,id_stol,sifra,broj_narudzbe,kom,id_poslovnica,id_skladiste,mpc,vpc,porez,porez_potrosnja,id_zaposlenik, dod, pol, rabat) VALUES (" +
                        " '" + zadnji_broj + (i + 1) + "'," +
                        " '" + dg(i, "jelo") + "'," +
                        " '" + _OdabraniStol + "'," +
                        " '" + dg(i, "sifra") + "'," +
                        " '" + broj_narudzbe + "'," +
                        " '" + dg(i, "kolicina").Replace(",", ".") + "'," +
                        " '" + DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString() + "'," +
                        " '" + dg(i, "skladiste") + "'," +
                        " '" + (mpc * (1 - (rabat / 100))).ToString().Replace(',', '.') + "'," +
                        " '" + dg(i, "vpc").Replace(",", ".") + "'," +
                        " '" + dg(i, "porez").Replace(",", ".") + "'," +
                        " '" + dg(i, "porez_potrosnja").Replace(",", ".") + "'," +
                        " '" + Properties.Settings.Default.id_zaposlenik + "', " +
                        " '" + (dg(i, "dod") == "" ? 0.ToString() : dg(i, "dod")) + "', " +
                        " '" + (dg(i, "polapola") == "" ? "-1" : dg(i, "polapola")) + "'," +
                        " '" + rabat + "'" +
                        ")";

                    provjera_sql(classSQL.insert(sql));

                    row = DTsend.NewRow();
                    row["broj_racuna"] = brRac;
                    row["sifra_robe"] = dg(i, "sifra");
                    row["id_skladiste"] = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                    row["mpc"] = dg(i, "mpc");
                    row["porez"] = dg(i, "porez");
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = dg(i, "vpc");
                    row["cijena"] = dg(i, "mpc");
                    row["rabat"] = "0";
                    row["nbc"] = dg(i, "nbc");
                    row["stol"] = _OdabraniStol;
                    row["jelo"] = dg(i, "jelo");
                    row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                    row["ime"] = dg(i, "naziv");
                    row["id_ducan"] = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();
                    row["id_blagajna"] = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                    row["dod"] = (dg(i, "dod") == "" ? 0.ToString() : dg(i, "dod"));
                    row["pol"] = (dg(i, "polapola") == "" ? "-1" : dg(i, "polapola"));
                    row["id_podgrupa"] = dg(i, "id_podgrupa");
                    DTsend.Rows.Add(row);

                    if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() == "1")
                    {
                        ZaprimiNarudzbu(brRac, dg(i, "sifra"), dg(i, "kolicina"), _OdabraniStol, dat, broj_narudzbe, id_ducan);
                    }
                }

                //if (DSpostavke.Tables[0].Rows[0]["bool_direct_print_kuhinja"].ToString() == "1")
                //{

                posaljiNarudzbeNaStol = true;
                if (DTpostavkePrinter.Rows[0]["windows_printer_sank"].ToString() != "Nije instaliran" && sankBool)
                {
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe.ToString();
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter1(DTsend);
                }

                if (DTpostavkePrinter.Rows[0]["windows_printer_name2"].ToString() != "Nije instaliran" && kuhinjaBool)
                {
                    posaljiNarudzbeNaStol = true;
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe.ToString();
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter2(DTsend);
                    posaljiNarudzbeNaStol = true;
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe.ToString();
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter3(DTsend);
                }
                posaljiNarudzbeNaStol = false;

                frmStoloviZaNaplatuCustom.poslanoSaStola = true; // !
                PosPrint.classPosPrintKuhinja.NapuniListuOznacenimGrupama();
                //Ako je instaliran printer && ako ima bilo koja oznacena grupa u POS Postavke && Ako ima artikl na racunu koji se nalazi u oznacenoj grupi
                if (DTpostavkePrinter.Rows[0][29].ToString() != "Nije instaliran" && PosPrint.classPosPrintKuhinja.listaOznacenihGrupa.Count > 0 && PosPrint.classPosPrintKuhinja.ArtiklIzOznaceneGrupePostojan)
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter10(DTsend);
                frmStoloviZaNaplatuCustom.poslanoSaStola = false; // !

                if (DSpostavke.Tables[0].Rows[0]["prijava_nakon_racuna"].ToString() == "1")
                {
                    switch (Properties.Settings.Default.id_dopustenje)
                    {
                        case 1:
                            Global.GlobalFunctions.RestartApplication();
                            break;
                        case 2:
                            Global.GlobalFunctions.RestartApplication();
                            break;
                        case 3:
                            //CloseForm();
                            break;
                        case 4:
                            //CloseForm();
                            break;
                    }
                }

                NoviUnos();
                txtUnos.Select();

                try
                {
                    if (Bline_display)
                    {
                        spLineDisplay.Write(Convert.ToString((char)12));
                        spLineDisplay.WriteLine("UKUPNO:" + ukupno.ToString("#0.00") + " kn");
                    }
                }
                catch { }

                if (DSpostavke.Tables[0].Rows[0]["stolovi_razvrstavanje"].ToString() == "1")
                {
                    frmStoloviZaNaplatuCustom cf = new frmStoloviZaNaplatuCustom();
                    if (_OdabraniStol != null)
                        cf._odabraniStol = _OdabraniStol;

                    cf.kartica_kupca = kartica_kupca;
                    cf.FRMcaffe = this;
                    cf.ShowDialog(this);
                }
                else
                {
                    frmStoloviZaNaplatu cf = new frmStoloviZaNaplatu();
                    if (_OdabraniStol != null)
                        cf._odabraniStol = _OdabraniStol;

                    cf.kartica_kupca = kartica_kupca;
                    cf.FRMcaffe = this;
                    cf.ShowDialog(this);
                }

                kartica_kupca = "";
                lblKarticaKorisnik.Text = "";
                lblKarticaUkupno.Text = "";
            }

            startTimerKartica(true, true, true);

            setDefUkupnoAfterKartica(true, true);
            if (_OdabraniStol == null)
            {
                IzracunUkupno();
            }
            if (Convert.ToInt32(label1.Tag) == 0)
            {
                btnNajProdavanije.PerformClick();
            }
            else
            {
                foreach (Control c in flpGrupe.Controls)
                {
                    if (c is Button && Convert.ToInt32(((Button)c).Name) == Convert.ToInt32(label1.Tag))
                    {
                        ((Button)c).PerformClick();
                    }
                }
            }
        }


        private string GetBrojNarudzbe(string stol, string poslovnica)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_narudzbe AS bigint)) FROM na_stol WHERE id_poslovnica='" + poslovnica + "'", "na_stol").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private static int GetZadnjiBrojNarudzbe(string id_ducan)
        {
            string sql = "SELECT MAX(CAST (broj_narudzbe AS numeric)) as broj_narudzbe FROM na_stol WHERE id_poslovnica='" + id_ducan + "'";
            string sql2 = "SELECT MAX(CAST (broj_narudzbe AS numeric)) as broj_narudzbe FROM na_stol_naplaceno WHERE id_poslovnica='" + id_ducan + "'";
            DataTable DTZadnjiBr = classSQL.select(sql, "na_stol").Tables[0];
            DataTable DTZadnjiBr2 = classSQL.select(sql2, "na_stol_naplaceno").Tables[0];

            int broj1 = 0, broj2 = 0;
            if (!string.IsNullOrEmpty(DTZadnjiBr.Rows[0]["broj_narudzbe"].ToString()))
                broj1 = Int32.Parse(DTZadnjiBr.Rows[0]["broj_narudzbe"].ToString());
            if (!string.IsNullOrEmpty(DTZadnjiBr2.Rows[0]["broj_narudzbe"].ToString()))
                broj2 = Int32.Parse(DTZadnjiBr2.Rows[0]["broj_narudzbe"].ToString());

            if (broj1 != 0 && broj2 != 0)
                return broj1 > broj2 ? broj1 + 1 : broj2 + 1;

            if (broj1 != 0 && broj2 == 0)
                return broj1 + 1;

            if (broj1 == 0 && broj2 != 0)
                return broj2 + 1;

            return 1;
        }


        private void InsertIntoNa_Stol_Naplaceno(DataTable DTArtikli)
        {
            //Constant variables: id_stol, broj_narudzbe, id_poslovnica, id_zaposlenik, pol
            int id_stol = -1; // Po ovome znamo da narudžba nije išla na stol
            string broj_narudzbe = Convert.ToString(GetZadnjiBrojNarudzbe(id_ducan));
            int id_poslovnica = Int32.Parse(id_ducan);
            int id_zaposlenik = Int32.Parse(Properties.Settings.Default.id_zaposlenik);
            int br = 0;
            int pol = -1;
            //Empty variables: jelo, !!!!skinuto, id_adresa_dostave
            //################ SKINUTO !!!!!!!!! -> Pošto se ovaj column ne koristi, koristit ćemo ga za broj računa AKO IKAD BUDE POTREBAN
            //Default variables: id -> AutoIncrement
            //Varying variables: sifra,kom,mpc,vpc,porez,porez_potrosnja, dod, pol, rabat, id_skladiste

            foreach (DataRow row in DTArtikli.Rows)
            {
                string sifra = row["sifra_robe"].ToString();
                decimal kom = decimal.Parse(row["kolicina"].ToString().Replace(',','.'));
                string mpc = row["mpc"].ToString().Replace(',', '.');
                string vpc = row["vpc"].ToString().Replace(',', '.');
                string porez = row["porez"].ToString().Replace(',', '.');
                string id_skladiste = row["id_skladiste"].ToString();
                string porez_potrosnja = row["porez_potrosnja"].ToString().Replace(',', '.');
                int dod = Int32.Parse(row["dod"].ToString());
                string rabat = row["rabat"].ToString().Replace(',', '.');

                string sqlQuery = $@"INSERT INTO na_stol_naplaceno 
                            VALUES({id_stol},'{sifra}','{broj_narudzbe}',{kom},{id_poslovnica},'{id_skladiste}',
                            CAST ({mpc} AS NUMERIC),CAST ({vpc} AS NUMERIC),CAST ({porez} AS NUMERIC),CAST ({porez_potrosnja} AS NUMERIC),
                            {br},NULL,{brRac},{id_zaposlenik},{dod},{pol},NULL,CAST ({rabat} AS NUMERIC),DEFAULT)";
                classSQL.insert(sqlQuery);
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE " +
                " godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'", "racuni").Tables[0];

            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                /////////////////////////OVAJ KOD SLUŽI AKO JE POSTAVLJENA VRIJEDNOST U POSTAVKAMA ZA POČETAK RAČUNA//////////////////////////
                /////////////////////////AKO NIJE VRAČA 1/////////////////////////////////////////////////////////////////////////////////////
                string[] zadnji_racun_godina = DSpostavke.Tables[0].Rows[0]["zadnji_racun"].ToString().Split(';');
                if (zadnji_racun_godina[0] != "0" && zadnji_racun_godina[1] == DateTime.Now.Year.ToString())
                {
                    int brZ;
                    int.TryParse(zadnji_racun_godina[0], out brZ);
                    return (brZ + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string barcode = "";

        private void SpremiRacun()
        {
            string napomena = "";
            if (Class.Postavke.napomena_na_kraju_racuna)
            {
                Kasa.frmNapomenaRacun f = new Kasa.frmNapomenaRacun();
                f.ShowDialog(this);
                napomena = f.napomena;
            }

            string kol;
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
            DTsend.Columns.Add("stol");
            DTsend.Columns.Add("jelo");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DTsend.Columns.Add("dod");
            DTsend.Columns.Add("pol");
            DTsend.Columns.Add("id_izradio");
            DTsend.Columns.Add("id_podgrupa");

            DataRow row;

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            string uk1 = ukupno.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
            }
            else
            {
                uk1 = uk1.Replace(".", ",");
            }

            try
            {
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            }
            finally
            {
            }

            brRac = brojRacuna();
            classPosPrintKuhinja.broj_narudzbe = Convert.ToString(GetZadnjiBrojNarudzbe(id_ducan));
            //classPosPrintKuhinja.broj_narudzbe = brRac;

            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "ukupno_gotovina,ukupno_kartice,ukupno,storno,dobiveno_gotovina,id_stol,novo,godina,nacin_placanja,popust_cijeli_racun, popust_racun_kartica_kupca, napomena" + (Class.Postavke.is_beauty ? ", beauty_partner" : "") + ") " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + uk1.ToString().Replace(".", ",") + "'," +
                "'0'," +
                "'" + uk1.ToString().Replace(".", ",") + "'," +
                "'NE'," +
                "'0'," +
                "'" + _OdabraniStol + "','1','" + DateTime.Now.Year.ToString() + "'," +
                "'G'," +
                "'" + Math.Round(popust_na_cijeli_racun, 3).ToString() + "'," +
                "'" + karticaIznosZaOduzeti.ToString().Replace(',', '.') + "', " +
                "'" + napomena.Trim() + "'";

            if (Class.Postavke.is_beauty)
            {
                sql += ", '" + beauty_osoba.ToString() + "'";
            }
            sql += ");";

            try
            {
                if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() == "1")
                {
                    DataTable DT = classSQL.select("SELECT datum FROM caffe_narudzbe GROUP BY datum ORDER BY datum", "caffe_narudzbe").Tables[0];

                    if (DT.Rows.Count >= 10)
                    {
                        //classSQL.update("DELETE FROM caffe_narudzbe WHERE datum='" + DT.Rows[0]["datum"].ToString() + "'");
                    }
                }
            }
            catch { }

            provjera_sql(classSQL.insert(sql));


            if (Util.Korisno.kartica_kupca && kartica_kupca.Length > 0)
            {
                string sqlKK = "INSERT INTO karticakupci_racuni (oib, poslovnica, naplatni_uredaj, kartica_kupca, broj_racuna, datum_racun, iznos, bodovi) VALUES ('" + Class.PodaciTvrtka.oibTvrtke + "', (select ime_ducana from ducan where id_ducan = '" + id_ducan + "'), (select ime_blagajne from blagajna where id_ducan = '" + id_ducan + "' and id_blagajna = '" + id_kasa + "'), '" + kartica_kupca + "', '" + brRac + "', '" + dt + "', '" + ukupno.ToString().Replace(',', '.') + "', '" + (karticaIznosZaOduzeti > 0 ? ((-1) * karticaIznosZaOduzeti).ToString().Replace(',', '.') : (ukupno * (Convert.ToDouble(DSpostavke.Tables[0].Rows[0]["kartica_popust"]) / 100)).ToString().Replace(',', '.')) + "');";

                classSQL.insert(sqlKK);

                kartica_kupca = "";
                lblKarticaKorisnik.Text = "";
                lblKarticaUkupno.Text = "";
            }

            string sifra = "";
            int brNarudzbe = getBrojNarudzbe(id_ducan);

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dg(i, "dod").Length == 0 || Convert.ToInt32(dg(i, "dod")) != 2)
                {
                    if (DSpostavke.Tables[0].Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "0")
                    {
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                    }
                    SQL.ClassSkladiste.SetBrojcanik(dg(i, "sifra"), DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "+");
                }

                sifra = dg(i, "sifra");
                row = DTsend.NewRow();
                row["broj_racuna"] = brRac;
                row["sifra_robe"] = sifra;
                row["id_skladiste"] = dgw.Rows[i].Cells["skladiste"].Value;
                row["mpc"] = dg(i, "mpc");
                row["nbc"] = dg(i, "nbc");
                row["porez"] = dg(i, "porez");
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "rabat");
                row["vpc"] = dg(i, "vpc");
                row["cijena"] = dg(i, "mpc");
                row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                row["ime"] = dg(i, "naziv");
                row["stol"] = "0";
                row["id_ducan"] = id_ducan;
                row["id_blagajna"] = id_kasa;
                row["jelo"] = dg(i, "jelo");
                row["dod"] = dg(i, "dod");
                row["pol"] = dg(i, "polapola");
                row["id_podgrupa"] = dg(i, "id_podgrupa");
                if (Class.Postavke.is_beauty)
                    row["id_izradio"] = dgw.Rows[i].Tag;

                DTsend.Rows.Add(row);
                if (DTremotePostavke.Rows[0]["rad_sa_narudzbama"].ToString() == "1")
                {
                    ZaprimiNarudzbu(brRac, sifra, dg(i, "kolicina"), _OdabraniStol, dt, brNarudzbe, id_ducan);
                }
            }

            InsertIntoNa_Stol_Naplaceno(DTsend);

            sifra_skladiste = "";
            provjera_sql(SQL.SQLracun.InsertStavke(DTsend));
            provjera_sql(SQL.SQLracun.InsertNapomene(DTsend));

            if (beauty_dug_naplata != null && beauty_dug_naplata.Count > 0)
            {
                foreach (var item in beauty_dug_naplata)
                {
                    sql = String.Format(@"update beauty_dug set naplaceno_broj_racunom = {0}, naplaceno_id_ducan = {1}, naplaceno_id_blagajna = {2}
where id = {3}", brRac, id_ducan, id_kasa, item.id);
                    if (item.godina == Util.Korisno.GodinaKojaSeKoristiUbazi)
                    {
                        classSQL.update(sql);
                    }
                    else
                    {
                        string sqlDbLink = string.Format(@"select * from dblink
(
'host={0} user={1} password=q1w2e3r4 dbname={2}',
'{3}'
)
tt
(
    updated text
);",
Class.PodaciZaSpajanjeCompaktna.remoteServer.Trim(),
Class.PodaciZaSpajanjeCompaktna.remoteUsername.Trim(),
Class.PodaciZaSpajanjeCompaktna.remoteDb.Trim().Substring(0, Class.PodaciZaSpajanjeCompaktna.remoteDb.Trim().Length - 4) + item.godina.ToString(),
sql);
                        classSQL.update(sqlDbLink);
                    }
                }

                beauty_dug_naplata = null;
            }

            //OVA FUNKCIJA POSTAVLJA SKLADIŠTE TAKO DA UZME U OBZIR SVE DOKUMENTE I ZAKLJUČI STANJE NA SKLADIŠTU
            if (DSpostavke.Tables[0].Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "1")
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    int dod = 0;
                    int.TryParse(dg(i, "dod").ToString().Trim(), out dod);
                    if (dod != 2)
                        RobnoFunkcije.PostaviStanjeSkladistaPremaSifri(dg(i, "sifra"), DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString());
                }
            }

            barcode = "000" + brRac;

            try
            {
                string nacin_placanja = "";
                frmNaplata naplata = new frmNaplata();
                naplata.racun_ili_stol = brRac;
                naplata.R1 = "";
                naplata.R1 = sifraPartnera;
                naplata.ShowDialog(this);
                nacin_placanja = naplata.nacin_placanja;
                int idOdabraniZaposlenikNaplate = naplata.id_zaposlenik;
                decimal vraceniIznos = naplata.vraceniIznos;

                if (idOdabraniZaposlenikNaplate != 0)
                {
                    sql = string.Format(@"update racuni set id_blagajnik = '{0}' where broj_racuna = '{1}' and id_ducan = '{2}' and id_kasa = '{3}';",
                        idOdabraniZaposlenikNaplate, brRac, id_ducan, id_kasa);
                    classSQL.update(sql);
                }

                if (nacin_placanja == "G")
                    Util.Korisno.dodajIznosUBlagajnickiIzvjestaj(Convert.ToInt32(brRac), dRac, Convert.ToDecimal(ukupno));

                try
                {
                    DataTable DTfiltered = null;
                    if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() != "0")
                    {
                        PosPrint.classPosPrintCaffe.PrintReceipt(DTfiltered ?? DTsend, idOdabraniZaposlenikNaplate.ToString(), brRac + "/" + DateTime.Now.Year.ToString(), sifraPartnera, barcode, brRac, nacin_placanja, "", karticaIznosZaOduzeti, Convert.ToDecimal((((decimal)ukupnoBezRabata - karticaIznosZaOduzeti) / (decimal)ukupnoBezRabata)), vraceniIznos);
                    }
                    else
                    {
                        PrikazRacunaAkoJEiskljucenaAktivnostPOSPrintera();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
                }

                NoviUnos();
                txtUnos.Select();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() != "0")
                {
                    if (DTpostavkePrinter.Rows[0]["windows_printer_sank"].ToString() != "Nije instaliran")
                        PosPrint.classPosPrintKuhinja.PrintOnPrinter1(DTsend);
                    //PosPrint.classPosPrintKuhinja.

                    if (DTpostavkePrinter.Rows[0]["windows_printer_name2"].ToString() != "Nije instaliran")
                        PosPrint.classPosPrintKuhinja.PrintOnPrinter2(DTsend);

                    if (DTpostavkePrinter.Rows[0]["windows_printer_name3"].ToString() != "Nije instaliran")
                        PosPrint.classPosPrintKuhinja.PrintOnPrinter3(DTsend);

                    //Ako postoji uopce koja grupa da je ozancena za 4. printer u postavkama POS opreme
                    classPosPrintKuhinja.NapuniListuOznacenimGrupama();
                    //Ako je instaliran printer && ako ima bilo koja oznacena grupa u POS Postavke && Ako ima artikl na racunu koji se nalazi u oznacenoj grupi
                    if (DTpostavkePrinter.Rows[0][29].ToString() != "Nije instaliran" && classPosPrintKuhinja.listaOznacenihGrupa.Count > 0 && classPosPrintKuhinja.ArtiklIzOznaceneGrupePostojan)
                        PosPrint.classPosPrintKuhinja.PrintOnPrinter10(DTsend);
                }
            }

            lblKupac.Text = "";
            popust_na_cijeli_racun = 0;
        }


        private DialogResult ShowConfirmDialog(string message)
        {
            return MessageBox.Show(message, "Obavijest", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        //private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            //PokretacSinkronizacije.PokreniSinkronizaciju(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Util.Korisno.RadimSinkronizaciju = false;
        }

        private int getBrojNarudzbe(string id_ducan)
        {
            string sql = "select COALESCE(max(broj_narudzbe), 0::bigint)zbroj1  as br from caffe_narudzbe where id_ducan = '" + id_ducan + "';";

            DataTable dtBr = classSQL.select(sql, "caffe_narudzbe").Tables[0];
            int i = 0;
            if (dtBr != null)
            {
                Int32.TryParse(dtBr.Rows[0]["br"].ToString(), out i);
                return i;
            }

            return i;
        }

        private void ZaprimiNarudzbu(string broj_rac, string sifra, string kolicina, string stol, string dat, int brNarudzbe, string id_ducan)
        {
            try
            {
                string sql = string.Format("SELECT id_podgrupa FROM roba WHERE sifra = '{0}';", sifra);
                DataSet ds = classSQL.select(sql, "roba");
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable DT = ds.Tables[0];

                    if (DT.Rows[0][0].ToString() == "2")
                    {
                        sql = "INSERT INTO caffe_narudzbe (broj_racuna,sifra_stavke,djelatnik,kuhinja_spremna,sank_spreman,kolicina,id_stol,datum, broj_narudzbe, id_ducan) VALUES (" +
                            "'" + broj_rac + "', " +
                            "'" + sifra + "', " +
                            "'" + Properties.Settings.Default.id_zaposlenik + "', " +
                            "'0', " +
                            "'0', " +
                            "'" + kolicina + "', " +
                            "'" + stol + "', " +
                            "'" + dat + "', " +
                            "'" + brNarudzbe + "', " +
                            "'" + id_ducan + "'" +
                            ")";
                        provjera_sql(classSQL.insert(sql));
                    }
                }
            }
            catch { }
        }

        private void NoviUnos()
        {
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            SetOnNull();
            try
            {
                if (dgw.Columns.Contains("rabat"))
                    dgw.Columns["rabat"].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void SetOnNull()
        {
            dgw.Rows.Clear();
            dgw.Refresh();
            lblUkupno.Text = "0,00 Kn";
            sifraPartnera = "0";
        }

        private void btnOdustaniSve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da želite odustati?", "Odustani", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                brRac = brojRacuna();
                lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
                SetOnNull();
            }
            txtUnos.Select();

            startTimerKartica(true, true, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.FormBorderStyle = FormBorderStyle.None;
            partnerTrazi.MainForm = this;
            partnerTrazi.Dock = DockStyle.Fill;
            partnerTrazi.ShowDialog(this);

            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner, ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    sifraPartnera = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    lblKupac.Text = "Partner R1: " + partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    sifraPartnera = "0";
                    lblKupac.Text = "";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            else
            {
                sifraPartnera = "0";
                lblKupac.Text = "";
            }

            txtUnos.Select();
            startTimerKartica(true, true, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DSpostavke.Tables[0].Rows[0]["stolovi_razvrstavanje"].ToString() == "1")
            {
                frmStoloviZaNaplatuCustom cf = new frmStoloviZaNaplatuCustom();
                cf.FRMcaffe = this;
                cf.ShowDialog(this);
            }
            else
            {
                frmStoloviZaNaplatu cf = new frmStoloviZaNaplatu();
                cf.FRMcaffe = this;
                cf.ShowDialog(this);
            }
            startTimerKartica(true, true, true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataTable DT = classSQL.select("SELECT id_dopustenje FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "' AND id_dopustenje IN('1','2')", "zaposlenici").Tables[0];
            if (DSpostavke.Tables[0].Rows[0]["samo_prodaja"].ToString() == "0" || DT.Rows.Count > 0)
            {
                frmBackOffice bo = new frmBackOffice();
                bo.MainForm = this;
                bo.Dock = DockStyle.Fill;
                bo.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Nemate dopuštenja pristupati ovome dijelu programa.");
            }
            startTimerKartica(true, true, true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmOpcije opcije = new frmOpcije();
            opcije.FormCaffe = this;
            opcije.ShowDialog(this);

            PrikazZadnjegRacuna();
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

            startTimerKartica(true, true, true);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.UDSGame && udsCustomer != null && CODE != 0)
            {
                btnOdustaniKartica.PerformClick();
            }
            removeBeautyOsoba();
            popust_na_cijeli_racun = 0;
            NoviUnos();
            sifraPartnera = "0";
            lblKupac.Text = "";
            startTimerKartica(true, true, true);
        }

        private void btnPice_Click(object sender, EventArgs e)
        {
            dtArtikli = new DataTable();
            DataTable dtGrupe = new DataTable();
            Button btnCurrent = (Button)sender;
            int id_podgrupa = Convert.ToInt32(btnCurrent.Tag);
            idPodgrupa = id_podgrupa;
            idGrupa = 0;
            if (id_podgrupa > 0)
            {
                flpGrupe.Controls.Clear();
                SetGrupe(id_podgrupa);
            }
            else
            {
                lblnativGrupe.Text = btnCurrent.Text;
                current_page = 1;
                label1.Tag = 0;
                if (!Class.Postavke.is_beauty)
                    btnPolaPola.Visible = true;

                flpArtikli.Controls.Clear();
                string top = "";
                string remote = "";
                if (classSQL.remoteConnectionString != "")
                {
                    remote = "LIMIT 30";
                }
                else
                {
                    top = "TOP(30)";
                }

                string sql = string.Format(@"SELECT {0}
SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) AS kolicina,
roba.naziv,
roba.button_style,
roba.mpc,
roba.border_color,
racun_stavke.sifra_robe as sifra
FROM racun_stavke
LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna  AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
WHERE racuni.datum_racuna > '{1}'
AND racuni.datum_racuna < '{2}' AND roba.aktivnost='1'
GROUP BY racun_stavke.sifra_robe, roba.mpc, roba.button_style, roba.border_color, roba.naziv
ORDER BY kolicina DESC
{3};",
top,
DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd H:mm:ss"),
DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
remote);
                dtArtikli = classSQL.select(sql, "racuni").Tables[0];

                getArtikli();
            }

            startTimerKartica(true, true, true);
        }

        private void btnPice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnPice.Select();
                flpArtikli.Controls.Clear();
                frmRadSaSiframa rs = new frmRadSaSiframa();
                rs.MainForm = this;
                rs.ShowDialog(this);
            }
            if (e.KeyCode == Keys.F5)
            {
                btnGotovina.PerformClick();
            }
            if (e.KeyCode == Keys.F6)
            {
                btnDodajNaStol.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnOdjava.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnRacunR1.PerformClick();
            }
            if (e.KeyCode == Keys.Delete)
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                IzracunUkupno();
            }
        }

        private void btnGotovina_Click(object sender, EventArgs e)
        {
            #region PROVJERAVA DALI JE DOBRA GODINA
            mozeOtvoritiNapomenu = false;
            try
            {
                Until.classFukcijeZaUpravljanjeBazom B = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        //if (MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                        //    "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte POWER COMPUTERS.\r\n" +
                        //    "Želite li nastaviti izradu ovog računa?", "Upozorenje.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                        //    startTimerKartica(true, true, true);
                        //    return;
                        //}

                        if (MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\nŽelite prebaciti program u tekuću godinu?", "Upozorenje.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            //PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new PCPOS.Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                            string bazaZaTekucuGodinu = Util.Korisno.prefixBazeKojaSeKoristi() + DateTime.Now.Year.ToString();
                            B.PostaviGodinu_U_XML(bazaZaTekucuGodinu);
                        }

                        startTimerKartica(true, true, true);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                startTimerKartica(true, true, true);
                setDefUkupnoAfterKartica(true, false);
                return;
            }

            if (DSpostavke.Tables[0].Rows[0]["dodatno_upozorenje"].ToString() == "1")
            {
                frmPotvrda p = new frmPotvrda();
                p.MainForm = this;
                p.ShowDialog(this);
            }
            else
                _zavrsi = true;

            if (_zavrsi == true)
            {
                if (Class.Postavke.UDSGame && udsCompany != null && udsCustomer != null && CODE != 0 && !UDSAPI_APPLY_DISCOUNT)
                {
                    //bool udsDobarUnos = false;
                    //while (!udsDobarUnos) {
                    //   decimal[] rez = UDSAPI.getScoreToSubstractFromCustomerAccount((decimal)ukupnoBezRabata, UDSAPI_MAX_SCORES_DISCOUNT);

                    //    udsDobarUnos = Convert.ToBoolean(rez[0]);
                    //    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = rez[1];
                    //}

                    //if (udsDobarUnos) {
                    //}
                }
                else
                {
                    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
                }

                _OdabraniStol = "00";
                SpremiRacun();

                if (Class.Postavke.UDSGame && udsCompany != null && udsCustomer != null && CODE != 0)
                {
                    UDSAPI.postPurchase(udsCompany, CODE, (udsCustomer.participantId == null ? 0 : udsCustomer.participantId), (Class.Postavke.UDSGameEmployees ? Properties.Settings.Default.id_zaposlenik : ""), string.Format("{0}/{1}/{2}", brRac, Class.Postavke.default_poslovnica, Class.Postavke.maloprodaja_naplatni_uredaj), (decimal)ukupnoBezRabata, UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT);
                    btnOdustaniKartica.PerformClick();
                    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
                    ukupnoBezRabata = 0;
                }

                removeBeautyOsoba();
                _zavrsi = false;
            }

            if (Util.Korisno.kartica_kupca)
                kartica_kupca = "";

            PrikazZadnjegRacuna();

            try
            {
                Util.Korisno kor = new Util.Korisno();
                kor.ProvjeriVrijemeUpozoriKorisnika(5);
            }
            catch { }

            try
            {
                if (Bline_display)
                {
                    spLineDisplay.Write(Convert.ToString((char)12));
                    spLineDisplay.WriteLine("UKUPNO:" + ukupno.ToString("#0.00") + " kn");
                }
            }
            catch { }

            if (DSpostavke.Tables[0].Rows[0]["prijava_nakon_racuna"].ToString() == "1")
            {
                int dopustenje = Util.Korisno.GetDopustenjeZaposlenika();
                if (dopustenje == 1 || dopustenje == 2)
                    Close();
                else if (dopustenje == 3 || dopustenje == 4)
                {
                    frmPrijava p = new frmPrijava();
                    p.ShowDialog();
                }
            }

            startTimerKartica(true, true, true);
            setDefUkupnoAfterKartica(true, true);
            NoviUnos();

            foreach (Control c in flpGrupe.Controls)
            {
                if (c is Button && Convert.ToInt32(((Button)c).Name) == Convert.ToInt32(label1.Tag))
                {
                    ((Button)c).PerformClick();
                }
            }

            mozeOtvoritiNapomenu = true;
        }

        public void PrikazZadnjegRacuna()
        {
            try
            {
                DataTable DTzR = classSQL.select("SELECT broj_racuna,ukupno FROM racuni " +
                " WHERE godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'" +
                " ORDER BY CAST(broj_racuna AS INT) DESC LIMIT 1", "racuni").Tables[0];
                if (DTzR.Rows.Count > 0)
                {
                    lblZadnjiRac.Text = "Zadnji rač.: " + DTzR.Rows[0]["broj_racuna"].ToString() + ", Ukupno: " + Convert.ToDecimal(DTzR.Rows[0]["ukupno"].ToString()).ToString("#0.00") + " kn";
                }
                else
                {
                    lblZadnjiRac.Text = "";
                }
            }
            catch (Exception) { }
        }

        public void PrikazRacunaAkoJEiskljucenaAktivnostPOSPrintera()
        {
            try
            {
                DataTable DTzR = classSQL.select("SELECT broj_racuna,ukupno FROM racuni " +
                " WHERE godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'" +
                " ORDER BY CAST(broj_racuna AS INT) DESC LIMIT 1", "racuni").Tables[0];
                if (DTzR.Rows.Count > 0)
                {
                    Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                    rfak.dokumenat = "RAC";
                    rfak.ImeForme = "Račun";
                    rfak.broj_dokumenta = DTzR.Rows[0]["broj_racuna"].ToString();

                    rfak.godina = DateTime.Now.Year.ToString();
                    rfak.id_kasa = id_ducan;
                    rfak.id_poslovnica = id_ducan;

                    rfak.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnNarudzbe_Click(object sender, EventArgs e)
        {
            frmNarudzbe na = new frmNarudzbe();
            na.ShowDialog(this);
            startTimerKartica(true, true, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flpArtikli.Controls.Clear();
            frmRadSaSiframa rs = new frmRadSaSiframa();
            rs.MainForm = this;
            rs.ShowDialog(this);

            startTimerKartica(true, true, true);
        }

        private void frmCaffe_Activated(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            btnPice.Select();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                IzracunUkupno();
                setDefUkupnoAfterKartica(false, false);
            }

            startTimerKartica(true, true, true);
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //    return;

            if (dgw.RowCount == 0 || e.RowIndex < 0 || Convert.ToInt32(dgw.Rows[e.RowIndex].Cells["dod"].Value) == 2)
            {
                startTimerKartica(true, true, true);
                return;
            }

            if (!Class.Postavke.is_caffe && Class.Postavke.is_beauty && e.ColumnIndex == 0)
            {
                dodajDjelatnikaNaStavkuRacuna = true;
                lblPrijavljen_Click(sender, e);

                return;
            }

            if (e.ColumnIndex == 1)
            {
                if (!File.Exists("hamer"))
                {
                    frmKolicina k = new frmKolicina();
                    //k.Parent = this;
                    k.MainForm = this;
                    k.label1.Text = "Unos KOLIČINE";
                    k._type = "K";
                    k.ShowDialog(this);
                }
            }
            else if (e.ColumnIndex == 2)
            {
                if (!File.Exists("hamer"))
                {
                    frmKolicina k = new frmKolicina();
                    //k.Parent = this;
                    k.MainForm = this;
                    k.label1.Text = "Unos CIJENE";
                    k._type = "C";
                    k.ShowDialog(this);
                }
            }
            startTimerKartica(true, true, true);
        }

        private void lblKupac_Click(object sender, EventArgs e)
        {
            sifraPartnera = "0";
            startTimerKartica(true, true, true);
        }

        private void btnZavrsiSmjenu_Click(object sender, EventArgs e)
        {
            string ZBS = ZadnjiBrojSmjene();

            if (ZBS != "null")
            {
                string sql = "SELECT * FROM smjene WHERE id='" + ZBS + "'  AND smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ";
                DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

                if (DT_smjena.Rows.Count > 0)
                {
                    if (DT_smjena.Rows[0]["zavrsetak"].ToString() == "")
                    {
                        DataTable DT = classSQL.select("SELECT id_dopustenje FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "' AND id_dopustenje IN('1','2')", "zaposlenici").Tables[0];

                        if (DSpostavke.Tables[0].Rows[0]["samo_prodaja"].ToString() == "0" || DT.Rows.Count > 0)
                        {
                            frmZavrsiSmjenu ps = new frmZavrsiSmjenu();
                            ps.ShowDialog(this);
                        }
                        else
                        {
                            if (MessageBox.Show("Dali ste sigurni da želite završiti smjenu?", "Završetak smjene", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                datumOD = DT_smjena.Rows[0]["pocetak"].ToString();
                                DataTable DT3 = new DataTable();
                                string sql1 = "SELECT SUM(ukupno_gotovina) AS [gotovina],SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
                                " WHERE datum_racuna>'" + datumOD + "' " +
                                " AND godina='" + DateTime.Now.Year.ToString() + "' AND id_kasa='" + id_kasa + "' AND id_ducan='" + id_ducan + "'";

                                DT = classSQL.select(sql1, "racuni").Tables[0];
                                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
                                {
                                    string a = "0";
                                    string b = "0";

                                    if (DT.Rows[0]["virman"].ToString() != "") { b = DT.Rows[0]["virman"].ToString(); }

                                    Sukupno = String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString()) + Convert.ToDouble(DT.Rows[0]["kartice"].ToString()) + Convert.ToDouble(a) + Convert.ToDouble(b));
                                }

                                if (Sukupno == "")
                                {
                                    Sukupno = "0";
                                }

                                string sql11 = "UPDATE smjene SET zavrsetak='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "', zavrsno_stanje='" + Sukupno.Replace(",", ".") + "'," +
                                    "konobarZ='" + Properties.Settings.Default.id_zaposlenik + "' WHERE id='" + ZBS + "'  AND smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ";
                                provjera_sql(classSQL.update(sql11));
                                MessageBox.Show("Smjena je završena.", "Završeno!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Smjena nije započeta.", "Greška");
                    }
                }
                else
                {
                    MessageBox.Show("Smjena nije započeta.", "Greška");
                }
            }

            startTimerKartica(true, true, true);
        }
        //Vrijeme i zmirkanje ak postoje neuspjele fiskalizacije
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSat.Text = DateTime.Now.ToString();

            if (neuspjelaFiskalizacijaPostoji)
            {
                labelPostojeNeuspjeleFiskalizacije.Visible = zmirkaj;
                zmirkaj = !zmirkaj;
            }
        }

        //Ova funkcija prikazuje koliko sati je preostalo do fiskalizacije
        //Javlja se svakih 10 sec
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (neuspjelaFiskalizacijaPostoji)
            {
                DateTime DatumVrijemeSada = DateTime.Now;
                var hours = (datumZadnjeMoguceFiskalizacije - DatumVrijemeSada).TotalHours;
                labelPostojeNeuspjeleFiskalizacije.Visible = true;
                labelPostojeNeuspjeleFiskalizacije.Text = "POSTOJE NEUSPJELE FISKALIZACIJE! (PREOSTALO: " + hours.ToString("#0.00") + " SATI)";
            }
            else
            {
                labelPostojeNeuspjeleFiskalizacije.Visible = false;
            }
        }

        //Svakih 10 min provjeri se ako postoji neuspjela fiskalizacija ili racun s greskom
        private void timer3_Tick(object sender, EventArgs e)
        {
            ProvjeriNeuspjeleFiskalizacije();
        }

        private void btnPredjela_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["jelo"].FormattedValue.ToString() == "")
                {
                    dgw.Rows[i].Cells["jelo"].Value = "Predjelo";
                    dgw.Rows[i].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                }
            }
            startTimerKartica(true, true, true);
        }

        private void btnGlavnoJelo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["jelo"].FormattedValue.ToString() == "")
                {
                    dgw.Rows[i].Cells["jelo"].Value = "Glavno jelo";
                    dgw.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
            startTimerKartica(true, true, true);
        }

        private void btnDesert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["jelo"].FormattedValue.ToString() == "")
                {
                    dgw.Rows[i].Cells["jelo"].Value = "Desert";
                    dgw.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
                }
            }
            startTimerKartica(true, true, true);
        }

        private void frmCaffe_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Bline_display)
                {
                    spLineDisplay.Write(Convert.ToString((char)12));
                    spLineDisplay.Close();
                }
            }
            catch { }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //file hamer je file koji postoji samo za mljekaru i pekare hamer
            if (DSpostavke.Tables[0].Rows[0]["koristi_popust_prema_stavki"].ToString() == "1")
            {
                if (dgw.Rows.Count > 0)
                {
                    DodatniPopustPostoci dp = new DodatniPopustPostoci();
                    dp.frm = this;
                    dp.ShowDialog(this);
                    if (dp.hasDiscount)
                    {
                        dgw.Columns["rabat"].Visible = true;
                        dgw.Columns["rabat"].Width = 55;
                    }
                }
                else
                {
                    MessageBox.Show("Nemate odabrane stavke");
                }
            }
            else
            {
                frmDodatniPopust n = new frmDodatniPopust();
                n.frm = this;
                n.ShowDialog(this);
                if (n.hasDiscount)
                {
                    dgw.Columns["rabat"].Visible = true;
                    dgw.Columns["rabat"].Width = 55;
                }
            }
            startTimerKartica(true, true, true);
        }

        private void lblPrijavljen_Click(object sender, EventArgs e)
        {
            try
            {
                frmOdabirZaposlenikaNaknadno oz = new frmOdabirZaposlenikaNaknadno();
                oz.caffe = this;
                oz.dodajDjelatnikaNaStavkuRacuna = dodajDjelatnikaNaStavkuRacuna;
                oz.ShowDialog(this);

                dodajDjelatnikaNaStavkuRacuna = false;
                startTimerKartica(true, true, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void timeKarticaKupca_Tick(object sender, EventArgs e)
        {
            //txtKarticaKupca.Focus();
        }

        private void txtKarticaKupca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string prefix = DSpostavke.Tables[0].Rows[0]["reader_prefix"].ToString();
                string sufix = DSpostavke.Tables[0].Rows[0]["reader_sufix"].ToString();
                string kk = "";

                if (e.KeyChar == (char)Keys.Enter && Util.Korisno.regKartica(prefix, sufix, txtKarticaKupca.Text, ref kk))
                {
                    kartica_kupca = kk;

                    txtKarticaKupca.Text = "";
                    string sql = "select coalesce(sum(iznos), 0) as ukupnoKn, coalesce(sum(bodovi), 0) as ukupnoBodovi from karticakupci_racuni where oib = '" + Class.PodaciTvrtka.oibTvrtke + "' AND kartica_kupca = '" + kartica_kupca + "'";
                    lblKarticaUkupno.Text = classSQL.select(sql, "karticakupca_racuni").Tables[0].Rows[0]["ukupnoBodovi"].ToString();
                    lblKarticaKorisnik.Text = kartica_kupca;
                    txtKarticaKupca.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOdustaniKartica_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.UDSGame)
            {
                if (udsCompany == null)
                {
                    return;
                }

                if (CODE != 0)
                {
                    CODE = 0;

                    if (udsCustomer != null && dgw.Rows.Count > 0 && UDSAPI_APPLY_DISCOUNT)
                    {
                        dgw.Columns["rabat"].Visible = false;
                        dgw.Columns["rabat"].Width = 55;

                        foreach (DataGridViewRow row in dgw.Rows)
                        {
                            row.Cells["rabat"].Value = 0;
                        }

                        IzracunUkupno();
                    }

                    //if (UDSAPI_APPLY_DISCOUNT)
                    //{
                    btnOdustaniKartica.Width = 95;
                    btnOdustaniKartica.Text = "UDS Game";
                    //}
                    //else
                    //{
                    btnKoristiKarticu.Width = 95;
                    btnKoristiKarticu.Text = "Koristi karticu";
                    btnKoristiKarticu.Enabled = false;
                    btnKoristiKarticu.Visible = false;
                    //}

                    udsCustomer = null;

                    return;
                }

                var input = Microsoft.VisualBasic.Interaction.InputBox("Discount code");
                CODE = 0;

                if (int.TryParse(input, out CODE))
                {
                    udsCustomer = UDSAPI.getCustomer(CODE);
                    if (udsCustomer != null)
                    {
                        if (UDSAPI_APPLY_DISCOUNT)
                        {
                            btnOdustaniKartica.Width = 196;
                            btnOdustaniKartica.Text = string.Format("UDS Game odustani za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);

                            dgw.Columns["rabat"].Visible = true;
                            dgw.Columns["rabat"].Width = 55;

                            if (dgw.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow row in dgw.Rows)
                                {
                                    row.Cells["rabat"].Value = UDSAPI_DISCOUNT_BASE;
                                }

                                IzracunUkupno();
                            }
                        }
                        else
                        {
                            btnKoristiKarticu.Visible = true;
                            btnKoristiKarticu.Enabled = true;
                            btnOdustaniKartica.Text = "UDS Game odustani";
                            btnKoristiKarticu.Text = string.Format("UDS Game bodovi za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);
                            btnKoristiKarticu.Width = 196;
                        }
                    }
                    else
                    {
                        CODE = 0;
                        if (UDSAPI_APPLY_DISCOUNT)
                        {
                            btnOdustaniKartica.Width = 95;
                            btnOdustaniKartica.Text = "UDS Game";
                        }
                        else
                        {
                            btnKoristiKarticu.Width = 95;
                            btnKoristiKarticu.Text = "Koristi karticu";
                            btnKoristiKarticu.Enabled = false;
                            btnKoristiKarticu.Visible = false;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    kartica_kupca = "";
                    lblKarticaKorisnik.Text = "";
                    lblKarticaUkupno.Text = "";
                    txtKarticaKupca.Text = "";
                    setDefUkupnoAfterKartica(false, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                startTimerKartica(false, true, true);
            }
        }

        private void btnPice_Enter(object sender, EventArgs e)
        {
            if (timeKarticaKupca.Enabled && Util.Korisno.kartica_kupca)
            {
                timeKarticaKupca.Stop();
                timeKarticaKupca.Enabled = false;
            }
        }

        /// <summary>
        /// Pokrece timera za karticu kupca i ponistava selektirani red u gridu
        /// </summary>
        /// <param name="d">true/false</param>
        /// <param name="p">true/false</param>
        private void startTimerKartica(bool d, bool p, bool n)
        {
            if (Util.Korisno.kartica_kupca && !timeKarticaKupca.Enabled)
            {
                timeKarticaKupca.Enabled = true;
                timeKarticaKupca.Start();
            }

            if (d)
                selectedRow = -1;

            if (p)
                polindex = -1;

            if (n)
                napomenaindex = -1;

            if (polindex == -1)
            {
                btnPolaPola.BackColor = Color.White;
                btnPolaPola.BackgroundImage = null;
                btnPolaPola.FlatAppearance.BorderColor = Color.FromArgb(31, 53, 79);
                btnPolaPola.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                btnPolaPola.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                btnPolaPola.ForeColor = Color.Black;
            }
            else
            {
                btnPolaPola.BackColor = Color.FromArgb(31, 53, 79);
                btnPolaPola.BackgroundImage = null;
                btnPolaPola.FlatAppearance.BorderColor = Color.White;
                btnPolaPola.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnPolaPola.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                btnPolaPola.ForeColor = Color.White;
            }

            if (selectedRow == -1)
            {
                btnDodatak.BackColor = Color.White;
                btnDodatak.BackgroundImage = null;
                btnDodatak.FlatAppearance.BorderColor = Color.FromArgb(31, 53, 79);
                btnDodatak.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                btnDodatak.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                btnDodatak.ForeColor = Color.Black;
            }
            else
            {
                btnDodatak.BackColor = Color.FromArgb(31, 53, 79);
                btnDodatak.BackgroundImage = null;
                btnDodatak.FlatAppearance.BorderColor = Color.White;
                btnDodatak.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnDodatak.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                btnDodatak.ForeColor = Color.White;
            }

            if (napomenaindex == -1)
            {
                btnNapomena.BackColor = Color.White;
                btnNapomena.BackgroundImage = null;
                btnNapomena.FlatAppearance.BorderColor = Color.FromArgb(31, 53, 79);
                btnNapomena.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                btnNapomena.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                btnNapomena.ForeColor = Color.Black;

                btnNapomena.Tag = 0;
            }
        }

        private void dgw_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.Korisno.kartica_kupca && timeKarticaKupca.Enabled)
            {
                timeKarticaKupca.Stop();
                timeKarticaKupca.Enabled = false;
            }
        }

        private void btnDodatak_Click(object sender, EventArgs e)
        {
            try
            {
                if (Class.Postavke.is_beauty)
                {
                    frmBon f = new frmBon();
                    f.ShowDialog(this);

                    return;
                }

                if (dgw.Rows.Count > 0 && Convert.ToInt32(dgw.Rows[dgw.CurrentCell.RowIndex].Cells["dod"].Value) == 0)
                {
                    selectedRow = dgw.CurrentCell.RowIndex;
                }
                else
                {
                    selectedRow = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            startTimerKartica(false, true, true);
        }

        private void btnKoristiKarticu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Class.Postavke.UDSGame && !UDSAPI_APPLY_DISCOUNT && CODE != 0)
                {
                    frmKoristiKarticu f = new frmKoristiKarticu();
                    f.useUDS = Class.Postavke.UDSGame;
                    f.udsScores = udsCustomer.scores;
                    f.ukupno_iznos = Convert.ToDecimal(lblUkupno.Text.Split(' ')[0]);
                    f.ukupno_iznos = Convert.ToDecimal(ukupno);
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        karticaIznosZaOduzeti = Convert.ToDecimal(f.txtIznosZaOduzeti.Text);
                        UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = karticaIznosZaOduzeti;
                        ukupno = (double)f.ukupno_iznos;
                        lblUkupno.Text = f.ukupno_iznos.ToString("#0.00") + " Kn";
                    }
                }
                else
                {
                    if (kartica_kupca != null && kartica_kupca.Length == 10)
                    {
                        frmKoristiKarticu f = new frmKoristiKarticu();
                        f.kartica_kupca = kartica_kupca;
                        f.ukupno_iznos = Convert.ToDecimal(lblUkupno.Text.Split(' ')[0]);
                        f.ukupno_iznos = Convert.ToDecimal(ukupno);
                        if (f.ShowDialog(this) == DialogResult.OK)
                        {
                            karticaIznosZaOduzeti = Convert.ToDecimal(f.txtIznosZaOduzeti.Text);
                            ukupno = (double)f.ukupno_iznos;
                            lblUkupno.Text = f.ukupno_iznos.ToString("#0.00") + " Kn";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kartica nije odabrana.");
                    }

                    startTimerKartica(true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPolaPola_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgw.Rows.Count > 0)
                {
                    if (btnPolaPola.BackColor != Color.FromArgb(31, 53, 79))
                    {
                        polindex = dgw.CurrentCell.RowIndex;

                        string sql = "select is_polpol from grupa where id_grupa = (select id_grupa from roba where sifra = '" + dgw.Rows[polindex].Cells["sifra"].Value + "')";
                        DataTable dtIsPolpol = classSQL.select(sql, "grupa").Tables[0];
                        if (dtIsPolpol != null && dtIsPolpol.Rows.Count > 0)
                        {
                            if (!Convert.ToBoolean(dtIsPolpol.Rows[0]["is_polpol"]))
                            {
                                MessageBox.Show("Artikl nije iz grupe koja podržava opciju pola-pola.");
                                polindex = -1;
                                return;
                            }
                        }

                        foreach (DataGridViewRow dRow in dgw.Rows)
                        {
                            if (dRow.Index != polindex && dRow.Cells["polapola"].Value.ToString() != "" && dRow.Cells["polapola"].Value.ToString() == dgw.Rows[polindex].Cells["polapola"].Value.ToString())
                            {
                                MessageBox.Show("Artikl već ima uključenu opciju pola-pola.");
                                return;
                            }
                        }

                        int pp = 0;
                        if (!Int32.TryParse(dgw.Rows[polindex].Cells["polapola"].Value.ToString(), out pp))
                        {
                            polpol++;
                        }
                        dgw.Rows[polindex].Cells["polapola"].Value = polpol;
                        dgw.Rows[polindex].Cells["kolicina"].Value = 0.5;
                    }

                    startTimerKartica(true, false, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void setDefUkupnoAfterKartica(bool setDef, bool dodajOduzetiIznos)
        {
            if (setDef)
            {
                ukupno = 0;
                karticaIznosZaOduzeti = 0;
            }

            if (dodajOduzetiIznos)
            {
                ukupno = ukupno + Convert.ToDouble(karticaIznosZaOduzeti);
            }

            lblUkupno.Text = ukupno.ToString("#0.00") + " Kn";
            karticaIznosZaOduzeti = 0;
        }

        private void btnNapomena_Click(object sender, EventArgs e)
        {
            //if (_zavrsi || !string.IsNullOrEmpty(_OdabraniStol))
            //    return;
            if (!mozeOtvoritiNapomenu)
                return;

            try
            {
                // Frizerski salon?
                if (Class.Postavke.is_beauty)
                {
                    if (!dodajOsobuNaRacunAutomatski)
                    {
                        frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                        partnerTrazi.MainForm = this;
                        partnerTrazi.ShowDialog(this);
                    }

                    if (Properties.Settings.Default.id_partner != "")
                    {
                        beauty_osoba = Convert.ToInt32(Properties.Settings.Default.id_partner);
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner, case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as ime_tvrtke FROM partners WHERE id_partner ='" + beauty_osoba + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            lblBeautyOsoba.Enabled = true;
                            sifraPartnera = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            lblBeautyOsoba.Text = "Osoba: " + partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            removeBeautyOsoba();
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        }

                        Properties.Settings.Default.id_partner = "";
                    }
                    else
                    {
                        removeBeautyOsoba();
                    }
                    txtUnos.Select();

                    startTimerKartica(true, true, true);

                    return;
                }

                if (dgw.Rows.Count == 0)
                {
                    return;
                }
                if (Convert.ToInt32(dgw.Rows[dgw.CurrentCell.RowIndex].Cells["dod"].Value) == 2)
                {
                    return;
                }

                napomenaindex = dgw.CurrentCell.RowIndex;
                if (Convert.ToInt16(btnNapomena.Tag) == 0)
                {
                    btnNapomena.Tag = 1;
                    btnNapomena.BackColor = Color.FromArgb(31, 53, 79);
                    btnNapomena.BackgroundImage = null;
                    btnNapomena.FlatAppearance.BorderColor = Color.White;
                    btnNapomena.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                    btnNapomena.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                    btnNapomena.ForeColor = Color.White;
                    lblnativGrupe.Text = "Napomene";

                    btnGotovina.Focus();
                }

                // Odabir podgrupe za prikaz određenih napomena
                frmOdabirPodgrupe formPodgrupa = new frmOdabirPodgrupe();
                formPodgrupa.ShowDialog();
                if (formPodgrupa.IdPodgrupa == 0)
                    return;

                string sql = $"select * from napomene where aktivno = '1' AND id_podgrupa = {formPodgrupa.IdPodgrupa} order by napomena asc;";
                DataSet dsNapomena = classSQL.select(sql, "napomene");
                if (dsNapomena != null)
                {
                    flpArtikli.Controls.Clear();
                    setWidthHeightMarginForArtiklButtons();
                    foreach (DataRow dr in dsNapomena.Tables[0].Rows)
                    {
                        object ime_gumba = "", name_gumba = "";

                        ime_gumba = dr["napomena"];
                        name_gumba = dr["id"];

                        Button btnGrupa = new Button();
                        btnGrupa.Text = ime_gumba.ToString();
                        btnGrupa.Name = name_gumba.ToString();
                        btnGrupa.BackColor = Color.Transparent;

                        btnGrupa.BackColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["button_color_icona"].ToString());
                        btnGrupa.ForeColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["font_color_icona"].ToString());

                        btnGrupa.Font = new Font("Arial", Convert.ToInt16(DSpostavke.Tables[0].Rows[0]["font_size_icona"]), Util.Korisno.biu(DSpostavke.Tables[0].Rows[0]["bui_icona"].ToString()));
                        btnGrupa.Size = new Size(caffe_icon_width, caffe_icon_height);
                        btnGrupa.Margin = new Padding(margin_all);

                        btnGrupa.BackgroundImageLayout = ImageLayout.Stretch;

                        btnGrupa.Cursor = Cursors.Hand;
                        btnGrupa.FlatAppearance.BorderColor = Util.Korisno.hexToColor(DSpostavke.Tables[0].Rows[0]["font_color_icona"].ToString());
                        btnGrupa.FlatAppearance.BorderSize = 0;
                        btnGrupa.FlatAppearance.CheckedBackColor = Color.Transparent;
                        btnGrupa.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        btnGrupa.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        btnGrupa.FlatStyle = FlatStyle.Flat;

                        btnGrupa.TabIndex = 2;
                        for (int i = napomenaindex; i < dgw.Rows.Count; i++)
                        {
                            if (i != napomenaindex && Convert.ToInt32(dgw.Rows[i].Cells["dod"].Value) == 2 && Convert.ToInt32(dgw.Rows[i].Cells["sifra"].Value) == Convert.ToInt32(name_gumba))
                            {
                                btnGrupa.Tag = true;
                                btnGrupa.FlatAppearance.BorderSize = Util.Korisno.selectButtonBorderSize;
                                break;
                            }
                            else if (i > napomenaindex && Convert.ToInt32(dgw.Rows[i].Cells["dod"].Value) == 0)
                            {
                                btnGrupa.Tag = false;
                                break;
                            }

                            btnGrupa.Tag = false;
                        }

                        btnGrupa.UseVisualStyleBackColor = false;

                        btnGrupa.Click += new EventHandler(this.btnNap_Click);
                        btnGrupa.MouseEnter += new EventHandler(this.pic_MouseEnter1);
                        btnGrupa.MouseLeave += new EventHandler(this.pic_MouseLeave1);
                        btnGrupa.KeyDown += new KeyEventHandler(this.btnPice_KeyDown);
                        btnGrupa.Enter += new EventHandler(this.btnPice_Enter);

                        flpArtikli.Controls.Add(btnGrupa);

                        btnGotovina.Focus();
                    }
                }

                startTimerKartica(true, true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgw.Rows.Count == 0)
                    return;

                if (sender is Button)
                {
                    Button btn = (Button)sender;
                    if ((bool)btn.Tag == true)
                    {
                        btn.FlatAppearance.BorderSize = 0;
                        for (int i = napomenaindex; i < dgw.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dgw.Rows[i].Cells["dod"].Value) == 2 && Convert.ToInt32(dgw.Rows[i].Cells["sifra"].Value) == Convert.ToInt32(btn.Name))
                            {
                                dgw.Rows.RemoveAt(i);
                                i--;
                            }
                            else if (napomenaindex != i && Convert.ToInt32(dgw.Rows[i].Cells["dod"].Value) == 0)
                            {
                                btn.Tag = false;
                                return;
                            }
                        }

                        btn.Tag = false;
                    }
                    else
                    {
                        btn.Tag = true;

                        if (sender is Button)
                        {
                            btn.FlatAppearance.BorderSize = Util.Korisno.selectButtonBorderSize;
                            int id = Convert.ToInt32(((Button)sender).Name);
                            string sql = "select * from napomene where id = '" + id + "';";
                            DataSet dsNapomena = classSQL.select(sql, "napomene");
                            if (dsNapomena != null)
                            {
                                DataGridViewRow drNew = (DataGridViewRow)dgw.Rows[0].Clone();

                                drNew.Cells[0].Value = dsNapomena.Tables[0].Rows[0]["napomena"].ToString();//Naziv
                                drNew.Cells[1].Value = 0;//Kol
                                drNew.Cells[2].Value = 0;//Cijena
                                drNew.Cells[3].Value = dsNapomena.Tables[0].Rows[0]["id"].ToString(); //sifra
                                drNew.Cells[4].Value = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(); //skladiste
                                drNew.Cells[5].Value = 0;//porez
                                drNew.Cells[6].Value = "0";//rabat
                                drNew.Cells[7].Value = 0;//vpc
                                drNew.Cells[8].Value = 0;//porez_potrosnja
                                drNew.Cells[9].Value = "";//zakljucaj
                                drNew.Cells[10].Value = "";//jelo
                                drNew.Cells[11].Value = 0;//nbc
                                drNew.Cells[12].Value = 2;//dod
                                drNew.Cells[13].Value = "";//polapola
                                drNew.Cells[14].Value = dsNapomena.Tables[0].Rows[0]["id_podgrupa"].ToString(); // podgrupa

                                int currentRow = dgw.CurrentCell.RowIndex;
                                dgw.Rows.Insert((currentRow + 1), drNew);
                            }
                        }
                    }
                    PaintRows(dgw);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgw_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblnativGrupe.Text.ToUpper() == "NAPOMENE")
                {
                    if (Convert.ToInt32(dgw.Rows[dgw.CurrentCell.RowIndex].Cells["dod"].Value) == 2)
                    {
                        if (Convert.ToInt32(label1.Tag) == 0)
                        {
                            btnNajProdavanije.PerformClick();
                        }
                        else
                        {
                            foreach (Control c in flpGrupe.Controls)
                            {
                                if (c is Button && Convert.ToInt32(((Button)c).Name) == Convert.ToInt32(label1.Tag))
                                {
                                    ((Button)c).PerformClick();
                                }
                            }
                        }
                    }
                    else if (Convert.ToInt32(dgw.Rows[dgw.CurrentCell.RowIndex].Cells["dod"].Value) == 0)
                    {
                        btnNapomena.BackColor = Color.FromArgb(31, 53, 79);
                        btnNapomena.BackgroundImage = null;
                        btnNapomena.FlatAppearance.BorderColor = Color.White;
                        btnNapomena.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
                        btnNapomena.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                        btnNapomena.ForeColor = Color.White;

                        napomenaindex = dgw.CurrentCell.RowIndex;
                        btnNapomena.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOtpremnica_Click(object sender, EventArgs e)
        {
            try
            {
                if (Class.Postavke.is_beauty)
                {
                    frmOsobe f = new frmOsobe();
                    f.MainForm = this;
                    f.ShowDialog(this);
                    beauty_dug_naplata = f.osobe;
                    //beauty_dug_naplata = f.ids;
                    if ((f.ids.Length > 0 && f.idPartner > 0) || (f.dodajOsobu && f.idPartner > 0))
                    {
                        dodajOsobuNaRacunAutomatski = true;
                        Properties.Settings.Default.id_partner = f.idPartner.ToString();
                        btnNapomena_Click(new object(), new EventArgs());
                        dodajOsobuNaRacunAutomatski = false;
                    }
                    return;
                }

                if (dgw.Rows.Count > 0)
                {
                    frmMiniOtpremnica f = new frmMiniOtpremnica();
                    f.dgv = dgw;
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        NoviUnos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            flpGrupe.AutoScrollPosition = new Point(0, flpGrupe.VerticalScroll.Value - flpGrupe.VerticalScroll.SmallChange * 8);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            flpGrupe.AutoScrollPosition = new Point(0, flpGrupe.VerticalScroll.Value + flpGrupe.VerticalScroll.SmallChange * 8);
        }

        private void lblBeautyOsoba_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Želite maknuti odabranu osobu?", "Osoba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                removeBeautyOsoba();
            }
        }

        private void removeBeautyOsoba()
        {
            try
            {
                lblBeautyOsoba.Enabled = false;
                lblBeautyOsoba.Text = "";
                beauty_osoba = 0;
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

        /// <summary>
        /// Used to set button text's based on chosen language
        /// </summary>
        private void SetButtonNames()
        {
            btnPice.Text = Properties.Strings.Pice;
            btnHrana.Text = Properties.Strings.Hrana;
            btnNarudzbe.Text = Properties.Strings.Narudzbe;
            btnRacunR1.Text = Properties.Strings.R1;
            btnOpcije.Text = Properties.Strings.Opcije;
            btnStolovi.Text = Properties.Strings.Stolovi;
            btnOdjava.Text = Properties.Strings.Odjava + " ESC";
            btnOdjava.Font = new Font(new FontFamily("Arial"), 9.5F, FontStyle.Bold);
            btnOdustani.Text = Properties.Strings.Odustani;
            btnPopust.Text = Properties.Strings.Popust;
            btnDelete.Text = Properties.Strings.BrisiStavku;
            btnDelete.Font = new Font(new FontFamily("Arial"), 9.0F, FontStyle.Regular);
            btnGotovina.Text = Properties.Strings.ZavrsiRacun + " F5"; ;
            btnDodajNaStol.Text = Properties.Strings.DodajNaStol + " F6"; ;
            btnDodajNaStol.Font = new Font(new FontFamily("Arial"), 9.5F, FontStyle.Bold);
        }

        /// <summary>
        /// Sets current culture info
        /// </summary>
        /// <param name="culture">CultureInfo</param>
        private void SetCurrentCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

    }
}