using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmOdjavaKomisione : Form
    {
        public frmMenu MainForm { get; set; }
        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        private bool edit = false;
        private string skladiste_pocetno = "";
        public string broj_komisione_edit { get; set; }

        public frmOdjavaKomisione()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmOdjavaKomisione_Load(object sender, EventArgs e)
        {
            SetSkladiste();
            txtBroj.Text = brojOdjave();
            ControlDisableEnable(1, 0, 0, 1, 0);
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            if (broj_komisione_edit != null) { FillKomisione(); }
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void SetSkladiste()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1')", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOdjavaKomisione_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM odjava_komisione WHERE  broj='" + txtBroj.Text + "'", "odjava_komisione").Tables[0];
                DeleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojOdjave() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_komisione_edit = txtBroj.Text;
                    FillKomisione();
                    EnableDisable(true);
                    edit = true;
                    cbSkladiste.Select();
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }
            }
        }

        private void FillKomisione()
        {
            EnableDisable(true);
            DeleteFields();
            ControlDisableEnable(0, 1, 0, 0, 1);
            chbFakture.Checked = false;
            chbKasa.Checked = false;
            chbOtpremnice.Checked = false;
            chbRadniNalozi.Checked = false;
            chbSkladisnica.Checked = false;

            string sssql = "SELECT * FROM odjava_komisione WHERE broj='" + broj_komisione_edit + "'";
            DataTable DTheader = classSQL.select(sssql, "odjava_komisione").Tables[0];

            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            cbSkladiste.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_skladiste"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            dtpOdDatuma.Value = Convert.ToDateTime(DTheader.Rows[0]["od_datuma"].ToString());
            dtpDoDatuma.Value = Convert.ToDateTime(DTheader.Rows[0]["do_datuma"].ToString());
            txtSifraPartnera.Text = DTheader.Rows[0]["id_partner"].ToString();
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            if (DTheader.Rows[0]["izlazne_fakture"].ToString() == "DA")
            {
                chbFakture.Checked = true;
            }

            if (DTheader.Rows[0]["kasa"].ToString() == "DA")
            {
                chbKasa.Checked = true;
            }

            if (DTheader.Rows[0]["otpremnice"].ToString() == "DA")
            {
                chbOtpremnice.Checked = true;
            }

            if (DTheader.Rows[0]["radni_nalozi"].ToString() == "DA")
            {
                chbRadniNalozi.Checked = true;
            }

            if (DTheader.Rows[0]["skladisnica"].ToString() == "DA")
            {
                chbSkladisnica.Checked = true;
            }

            string sql = "SELECT " +
                " odjava_komisione_stavke.id_stavka," +
                " odjava_komisione_stavke.sifra," +
                " odjava_komisione_stavke.kolicina_prodano," +
                " odjava_komisione_stavke.nbc," +
                " odjava_komisione_stavke.vpc," +
                " odjava_komisione_stavke.id_stavka_dokumenat," +
                " odjava_komisione_stavke.dokumenat," +
                " odjava_komisione_stavke.table_name," +
                " roba.naziv" +
                " FROM odjava_komisione_stavke " +
                " LEFT JOIN roba ON roba.sifra=odjava_komisione_stavke.sifra" +
                " WHERE odjava_komisione_stavke.broj='" + broj_komisione_edit + "'";

            DT_stavke = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT_stavke.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT_stavke.Rows[i]["kolicina_prodano"].ToString();
                dgw.Rows[br].Cells["nbc"].Value = DT_stavke.Rows[i]["nbc"].ToString();
                dgw.Rows[br].Cells["vpc"].Value = DT_stavke.Rows[i]["vpc"].ToString();
                dgw.Rows[br].Cells["id"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka_dokumenat"].ToString();
                dgw.Rows[br].Cells["dokumenat"].Value = DT_stavke.Rows[i]["dokumenat"].ToString();
                dgw.Rows[br].Cells["table"].Value = DT_stavke.Rows[i]["table_name"].ToString();
            }

            edit = true;
            PaintRows(dgw);
            btnPripremi.Enabled = false;
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpOdDatuma.Select();
            }
        }

        private void dtpOdDatuma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDoDatuma.Select();
            }
        }

        private void dtpDoDatuma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraPartnera.Select();
            }
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                rtbNapomena.Select();
            }
        }

        private void txtPartnerIme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPripremi.Select();
            }
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }
        }

        private void EnableDisable(bool x)
        {
            dtpDoDatuma.Enabled = x;
            dtpOdDatuma.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifraPartnera.Enabled = x;
            txtPartnerIme.Enabled = x;

            if (x == true)
            {
                txtBroj.Enabled = false;
                nmGodina.Enabled = false;
            }
            else if (x == false)
            {
                txtBroj.Enabled = true;
                nmGodina.Enabled = true;
            }
        }

        private string brojOdjave()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM odjava_komisione", "odjava_komisione").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBroj.Text = brojOdjave();
            EnableDisable(true);
            edit = false;
            btnPripremi.Enabled = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnPripremi_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();

            ////////////////////////RACUNI//////////////////////////////////////////////////////////////////////
            if (chbKasa.Checked)
            {
                string sql = "SELECT racun_stavke.kolicina,racun_stavke.sifra_robe,racun_stavke.nbc,racun_stavke.vpc,roba.naziv,racun_stavke.id_stavka" +
                            " FROM racun_stavke" +
                            " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                            " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                            " WHERE racuni.datum_racuna >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna <= '" +
                            dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND racun_stavke.odjava IS NULL AND racun_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'  AND roba.oduzmi='DA'";
                DataTable DTracun = classSQL.select(sql, "racun_stavke").Tables[0];
                //dataGridView1.DataSource = DTracun;

                for (int x = 0; x < DTracun.Rows.Count; x++)
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, DTracun.Rows[x]["sifra_robe"].ToString(), DTracun.Rows[x]["naziv"].ToString(), "Kasa", DTracun.Rows[x]["kolicina"].ToString(), "0,00", DTracun.Rows[x]["nbc"].ToString(), DTracun.Rows[x]["vpc"].ToString(), DTracun.Rows[x]["id_stavka"].ToString(), "racun_stavke");
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////FAKTURE//////////////////////////////////////////////////////////////////////

            if (chbFakture.Checked)
            {
                string sql2 = "SELECT faktura_stavke.kolicina,faktura_stavke.sifra,faktura_stavke.nbc,faktura_stavke.vpc,faktura_stavke.id_stavka,roba.naziv" +
                            " FROM faktura_stavke " +
                            " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                            " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                            " WHERE fakture.date >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND fakture.date <= '" +
                            dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND faktura_stavke.odjava IS NULL AND faktura_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "' AND roba.oduzmi='DA'" +
                            "";
                DataTable DTfakture = classSQL.select(sql2, "faktura_stavke").Tables[0];
                //dataGridView1.DataSource = DTracun;

                for (int x = 0; x < DTfakture.Rows.Count; x++)
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, DTfakture.Rows[x]["sifra"].ToString(), DTfakture.Rows[x]["naziv"].ToString(), "Fakture", DTfakture.Rows[x]["kolicina"].ToString(), "0,00", DTfakture.Rows[x]["nbc"].ToString(), DTfakture.Rows[x]["vpc"].ToString(), DTfakture.Rows[x]["id_stavka"].ToString(), "faktura_stavke");
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////OTPREMNICE//////////////////////////////////////////////////////////////////////

            if (chbOtpremnice.Checked)
            {
                string sql3 = "SELECT otpremnica_stavke.kolicina,otpremnica_stavke.sifra_robe AS sifra,otpremnica_stavke.nbc,otpremnica_stavke.vpc,otpremnica_stavke.id_stavka,roba.naziv" +
                            " FROM otpremnica_stavke" +
                            " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe" +
                            " LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice" +
                            " WHERE otpremnice.na_sobu = false and otpremnice.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND otpremnice.datum <= '" +
                            dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND otpremnica_stavke.odjava IS NULL AND otpremnice.id_skladiste='" + cbSkladiste.SelectedValue + "' AND roba.oduzmi='DA'" +
                            "";
                DataTable DTotp = classSQL.select(sql3, "otpremnica_stavke").Tables[0];
                //dataGridView1.DataSource = DTracun;

                for (int x = 0; x < DTotp.Rows.Count; x++)
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, DTotp.Rows[x]["sifra"].ToString(), DTotp.Rows[x]["naziv"].ToString(), "Otpremnica", DTotp.Rows[x]["kolicina"].ToString(), "0,00", DTotp.Rows[x]["nbc"].ToString(), DTotp.Rows[x]["vpc"].ToString(), DTotp.Rows[x]["id_stavka"].ToString(), "otpremnica_stavke");
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////RADNINALOG//////////////////////////////////////////////////////////////////////

            if (chbRadniNalozi.Checked)
            {
                string sql4 = "SELECT  radni_nalog_normativ.kolicina,radni_nalog_normativ.sifra,radni_nalog_normativ.nbc,radni_nalog_normativ.vpc,radni_nalog_normativ.id,roba.naziv" +
                                          " FROM radni_nalog_normativ " +
                                          " LEFT JOIN roba ON roba.sifra=radni_nalog_normativ.sifra" +
                                          " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_normativ.broj" +
                                          " WHERE radni_nalog.datum_naloga >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND radni_nalog.datum_naloga <= '" +
                                          dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND radni_nalog_normativ.odjava IS NULL AND id_skladiste='" + cbSkladiste.SelectedValue + "'  AND roba.oduzmi='DA'";

                DataTable DTrn = classSQL.select(sql4, "radni_nalog_stavke").Tables[0];

                for (int x = 0; x < DTrn.Rows.Count; x++)
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, DTrn.Rows[x]["sifra"].ToString(), DTrn.Rows[x]["naziv"].ToString(), "Radni nalog", DTrn.Rows[x]["kolicina"].ToString(), "0,00", DTrn.Rows[x]["nbc"].ToString(), DTrn.Rows[x]["vpc"].ToString(), DTrn.Rows[x]["id"].ToString(), "radni_nalog_normativ");
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////Međuskladišnice//////////////////////////////////////////////////////////////////////

            if (chbSkladisnica.Checked)
            {
                string sql3 = "SELECT meduskladisnica_stavke.kolicina, meduskladisnica_stavke.sifra, meduskladisnica_stavke.nbc, meduskladisnica_stavke.vpc, meduskladisnica_stavke.id_stavka,roba.naziv " +
                " FROM meduskladisnica_stavke" +
                " LEFT JOIN roba ON roba.sifra=meduskladisnica_stavke.sifra" +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj" +
                " WHERE meduskladisnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND meduskladisnica.datum <= '" +
                dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND meduskladisnica.id_skladiste_od='" + cbSkladiste.SelectedValue + "' AND meduskladisnica_stavke.odjava IS NULL AND roba.oduzmi='DA'";

                DataTable DTmds = classSQL.select(sql3, "meduskladisnica_stavke").Tables[0];

                for (int x = 0; x < DTmds.Rows.Count; x++)
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, DTmds.Rows[x]["sifra"].ToString(), DTmds.Rows[x]["naziv"].ToString(), "Međuskladišnica", DTmds.Rows[x]["kolicina"].ToString(), "0,00", DTmds.Rows[x]["nbc"].ToString(), DTmds.Rows[x]["vpc"].ToString(), DTmds.Rows[x]["id_stavka"].ToString(), "meduskladisnica_stavke");
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            DataTable DTbool = classSQL.select("SELECT broj FROM odjava_komisione WHERE broj = '" + txtBroj.Text + "'", "odjava_komisione").Tables[0];

            string sql = "";

            string chFaktura = "NE";
            string chKasa = "NE";
            string chOtpremnice = "NE";
            string chRadnialozi = "NE";
            string chSkladisnica = "NE";

            if (chbFakture.Checked)
            {
                chFaktura = "DA";
            }

            if (chbKasa.Checked)
            {
                chKasa = "DA";
            }

            if (chbOtpremnice.Checked)
            {
                chOtpremnice = "DA";
            }

            if (chbRadniNalozi.Checked)
            {
                chRadnialozi = "DA";
            }

            if (chbSkladisnica.Checked)
            {
                chSkladisnica = "DA";
            }

            decimal dec_parse;
            if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                MessageBox.Show("Greška kod upisa šifre partnera.", "Greška");
                return;
            }

            if (DTbool.Rows.Count == 0)
            {
                sql = "INSERT INTO odjava_komisione (broj,godina,datum,od_datuma,do_datuma,id_partner,napomena,izlazne_fakture,kasa,otpremnice,radni_nalozi,skladisnica,id_skladiste,id_zaposlenik) VALUES (" +
                     " '" + txtBroj.Text + "'," +
                     " '" + nmGodina.Value.ToString() + "'," +
                      " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                       " '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        " '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                         " '" + txtSifraPartnera.Text + "'," +
                          " '" + rtbNapomena.Text + "'," +
                           " '" + chFaktura + "'," +
                            " '" + chKasa + "'," +
                             " '" + chOtpremnice + "'," +
                              " '" + chRadnialozi + "'," +
                              " '" + chSkladisnica + "'," +
                              " '" + cbSkladiste.SelectedValue.ToString() + "'," +
                              " '" + Properties.Settings.Default.id_zaposlenik + "'" +
                                ")";
                provjera_sql(classSQL.insert(sql));
            }
            else
            {
                sql = "UPDATE odjava_komisione SET" +
                    " broj='" + txtBroj.Text + "'," +
                    "godina='" + nmGodina.Value.ToString() + "'," +
                    "datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "od_datuma='" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "do_datuma='" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "id_partner='" + txtSifraPartnera.Text + "'," +
                    "napomena='" + rtbNapomena.Text + "'," +
                    "id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'," +
                    "izlazne_fakture='" + chFaktura + "'," +
                    "kasa='" + chKasa + "'," +
                    "otpremnice='" + chOtpremnice + "'," +
                    "radni_nalozi='" + chRadnialozi + "'," +
                    "skladisnica='" + chSkladisnica + "' WHERE broj='" + txtBroj.Text + "'";
                provjera_sql(classSQL.update(sql));
            }

            string sql_odjava = "";
            string ssql = "";
            for (int i = 0; i < dgw.RowCount; i++)
            {
                sql_odjava = "UPDATE " + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + " SET " +
                 " odjava='1' WHERE id_stavka='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";
                provjera_sql(classSQL.update(sql_odjava));

                if (dgw.Rows[i].Cells["id"].FormattedValue.ToString() == "")
                {
                    ssql = "INSERT INTO odjava_komisione_stavke (broj,sifra,kolicina_prodano,vpc,nbc,id_stavka_dokumenat,dokumenat,table_name,rabat) VALUES (" +
                        "'" + txtBroj.Text + "'," +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["dokumenat"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'" +
                        ")";
                    provjera_sql(classSQL.insert(ssql));
                }
                else
                {
                    ssql = "UPDATE odjava_komisione_stavke SET" +
                        " broj='" + txtBroj.Text + "'," +
                        " sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " kolicina_prodano='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " vpc='" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString() + "'," +
                        " nbc='" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        " id_stavka_dokumenat='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'," +
                        " dokumenat='" + dgw.Rows[i].Cells["dokumenat"].FormattedValue.ToString() + "'," +
                        " rabat='" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'," +
                        " table_name='" + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + "'" +
                        " WHERE id_stavka='" + dg(i, "id") + "'";

                    provjera_sql(classSQL.update(ssql));
                }
            }

            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojOdjave();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnPripremi.Enabled = true;
            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojOdjave();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void DeleteFields()
        {
            txtPartnerIme.Text = "";
            txtSifraPartnera.Text = "";
            dgw.Rows.Clear();
            rtbNapomena.Text = "";
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Robno.frmSveOdjave_komisione srn = new frmSveOdjave_komisione();
            srn.MainForm = this;
            srn.ShowDialog();
            if (broj_komisione_edit != null)
            {
                DeleteFields();
                FillKomisione();
                EnableDisable(true);

                edit = true;
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 5)
            {
                decimal nabavna = Convert.ToDecimal(dgw.CurrentRow.Cells["nbc"].FormattedValue.ToString());
                decimal rabat = Convert.ToDecimal(dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString());
                dgw.CurrentRow.Cells["nbc"].Value = (nabavna - (nabavna * rabat / 100)).ToString("#0.00");
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
    }
}