using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmKalkulacija : Form
    {
        public frmKalkulacija()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private bool edit = false;
        private DataTable DT_Skladiste;
        private DataTable DTartikli;
        private DataTable DT_V;
        private DataTable DT_P;
        private DataTable DTpostavke = new DataTable();

        private decimal IznosNetto = 0;
        private decimal IznosUkupno = 0;

        public frmMenu MainForm { get; set; }
        private bool start = false;
        private decimal iznos_ukupno;
        public string broj_primke_edit { get; set; }
        public string skladiste_edit { get; set; }

        //public string broj_primke_edit { get; set; }
        private decimal valuta;

        private DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
        private string poslovnica = "1";

        private void frmPrimka_Load(object sender, EventArgs e)
        {
            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            //robno.PostaviStanjeSkladista();
            try
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            SetCB();
            txtBroj.Text = brojPrimke();
            ControlDisableEnable(1, 0, 0, 1, 0);
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            if (broj_primke_edit != null) { FillPrimke(broj_primke_edit, skladiste_edit); ControlDisableEnable(0, 1, 1, 0, 1); }
            start = true;
            EnableDisable(true, false, false);
            label24.Visible = false;

            DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];

            if (DTzaposlenik.Rows.Count > 0)
            {
                if (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "05593216962" &&
                     DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "1" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "2")
                {
                    txtProdajnaCijena.ReadOnly = true;
                    txtProdajnaCijenaSaPorezom.ReadOnly = true;
                    txtMarza.ReadOnly = true;
                    label24.Visible = true;
                }
            }
        }

        private void SetCB()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            DT_V = classSQL.select("SELECT * FROM valute", "valute").Tables[0];
            cbValuta.DataSource = DT_V;
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = "1";
            txtValuta.Text = cbValuta.SelectedValue.ToString();

            DT_P = classSQL.select("SELECT * FROM porezi", "porezi").Tables[0];
            cbUlazniPorez.DataSource = DT_P;
            cbUlazniPorez.DisplayMember = "naziv";
            cbUlazniPorez.ValueMember = "iznos";
            cbUlazniPorez.SelectedValue = DTpostavke.Rows[0]["pdv"].ToString();
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
            row.Height = 20;
        }

        private string brojPrimke()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_primke AS INT)) FROM primka WHERE is_kalkulacija='True'", "primka").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
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

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            txtBroj.Text = brojPrimke();
            EnableDisable(false, true, false);
            ControlDisableEnable(0, 1, 1, 0, 1);
            skladiste_edit = cbSkladiste.SelectedValue.ToString();
            txtBrojUlaznogDok.Select();

            lblPorez.Text = "Porez: 0.00 kn";
            lblUkupno.Text = "Ukupno: 0.00 kn";
            ;
        }

        private void EnableDisable(bool a, bool x, bool y)
        {
            txtBroj.Enabled = a;
            cbSkladiste.Enabled = a;

            txtBrojUlaznogDok.Enabled = x;
            txtSifraPartnera.Enabled = x;
            pictureBartikli.Enabled = x;

            button2.Enabled = x;
            btnObrisiNormativ.Enabled = x;

            txtNazivDobavljaca.Enabled = x;
            dtpDatum.Enabled = x;
            txtIznosBezPoreza.Enabled = x;
            txtIznosSaPorezom.Enabled = x;
            txtCarina.Enabled = x;
            //txtValuta.Enabled = x;
            cbValuta.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnNovaStavka.Enabled = x;
            btnObrisiNormativ.Enabled = x;

            //txtNazivArtikla.Enabled = y;
            txtUpakiranju.Enabled = y;
            txtBrojPaketa.Enabled = y;
            txtKolicina.Enabled = y;
            txtCijenaPoKom.Enabled = y;
            //txtNabavnaCijena.Enabled = y;
            txtNabavniIznos.Enabled = y;
            //txtIznos.Enabled = y;
            //txtNabavniIznos.Enabled = y;
            txtRabat.Enabled = y;
            cbUlazniPorez.Enabled = y;
            txtMarza.Enabled = y;
            txtIznosMarze.Enabled = y;
            txtProdajnaCijena.Enabled = y;
            txtProdajnaCijenaSaPorezom.Enabled = y;
            txtPovratnaNaknada.Enabled = y;
            //txtPorez.Enabled = y;
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(true, false, false);
            DeleteFields(true, true, true, true);
            txtBroj.Text = brojPrimke();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            lblPorez.Text = "Porez: 0.00 kn";
            lblUkupno.Text = "Ukupno: 0.00 kn";
            ;
        }

        private void DeleteFields(bool broj, bool header, bool stavke, bool dg)
        {
            if (broj)
            {
                txtBroj.Text = brojPrimke();
            }

            if (header)
            {
                txtBrojUlaznogDok.Text = "";
                txtSifraPartnera.Text = "";
                txtNazivDobavljaca.Text = "";
                txtIznosBezPoreza.Text = "0,00";
                txtIznosSaPorezom.Text = "0,00";
                txtCarina.Text = "0,00";
                rtbNapomena.Text = "";
            }

            if (stavke)
            {
                txtSifra_robe.Text = "";
                txtNazivArtikla.Text = "";
                txtUpakiranju.Text = "0,00";
                txtBrojPaketa.Text = "0,00";
                txtKolicina.Text = "0,00";
                txtCijenaPoKom.Text = "0,00";
                txtNabavnaCijena.Text = "0,00";
                txtNabavniIznos.Text = "0,00";
                txtNabavniIznos.Text = "0,00";
                txtRabat.Text = "0,00";
                txtPorez.Text = "0,00";
                txtSifra_robe.Select();
                txtMarza.Text = "0,00";
                txtIznosMarze.Text = "0,00";
                txtProdajnaCijena.Text = "0,00";
                txtProdajnaCijenaSaPorezom.Text = "0,00";

                txtProvjeraSaPorezom.Text = "0,00";
                txtProvjeraNetoIzFakture.Text = "0,00";
                txtProvjeraNetoSaPorezom.Text = "0,00";
                txtPovratnaNaknada.Text = "0,00";
            }

            if (dg)
            {
                if (dgw.RowCount > 0)
                    dgw.Rows.Clear();
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            #region ZABRANA AKO IZNOSI NA PRIMKI I FAKTURI NISU JEDNAKI

            if (chbZabranaZbogIznosa.Checked)
            {
                decimal ukupan_iznos_bez_poreza, RAZLIKA_BEZ_POREZA;
                Izracun();

                decimal.TryParse(txtIznosBezPoreza.Text, out ukupan_iznos_bez_poreza);

                RAZLIKA_BEZ_POREZA = ukupan_iznos_bez_poreza - IznosNetto;

                if (RAZLIKA_BEZ_POREZA > 1 || RAZLIKA_BEZ_POREZA < -1)
                {
                    MessageBox.Show("Primka ne može biti spremljena je postoje razlike više od jedne kune u cijeni bez pdv-a.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            #endregion ZABRANA AKO IZNOSI NA PRIMKI I FAKTURI NISU JEDNAKI

            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();

            decimal dec_parse;

            if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                txtSifraPartnera.Text = "0";
            }

            if (!Decimal.TryParse(txtIznosBezPoreza.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                txtIznosBezPoreza.Text = "0,000";
                MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtIznosSaPorezom.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                txtIznosSaPorezom.Text = "0,000";
                MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtCarina.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                txtCarina.Text = "0,000";
                MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška");
                return;
            }

            if (!Decimal.TryParse(Properties.Settings.Default.id_zaposlenik, out dec_parse) || Properties.Settings.Default.id_zaposlenik == "")
            {
                Properties.Settings.Default.id_zaposlenik = "1";
            }

            Izracun();
            string sql = "";
            if (edit == false)
            {
                txtBroj.Text = brojPrimke();

                sql = "INSERT INTO primka (" +
                    "broj_primke,id_skladiste,br_ulaznog_doc,id_partner,datum,iznos_bez_poreza,iznos_sa_porezom,carina,valuta,id_zaposlenik,napomena,iznos,is_kalkulacija,id_poslovnica,novo,editirano)" +
                    " VALUES " +
                    "(" +
                    "'" + txtBroj.Text + "'," +
                    "'" + cbSkladiste.SelectedValue + "'," +
                    "'" + txtBrojUlaznogDok.Text + "'," +
                    "'" + txtSifraPartnera.Text + "'," +
                    "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + txtIznosBezPoreza.Text.Replace(",", ".") + "'," +
                    "'" + txtIznosSaPorezom.Text.Replace(",", ".") + "'," +
                    "'" + txtCarina.Text.Replace(",", ".") + "'," +
                    "'" + txtValuta.Text.Replace(",", ".") + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + rtbNapomena.Text + "'," +
                    "'" + iznos_ukupno.ToString().Replace(",", ".") + "','True','" + poslovnica + "','1','0'" +
                    ")";
                provjera_sql(classSQL.insert(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada kalkulacije br." + txtBroj.Text + "')"));
            }
            else
            {
                sql = "UPDATE primka SET " +
                    "id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'," +
                    "br_ulaznog_doc='" + txtBrojUlaznogDok.Text + "'," +
                    "id_partner='" + txtSifraPartnera.Text + "'," +
                    "datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "iznos_bez_poreza='" + txtIznosBezPoreza.Text.Replace(",", ".") + "'," +
                    "iznos_sa_porezom='" + txtIznosSaPorezom.Text.Replace(",", ".") + "'," +
                    "carina='" + txtCarina.Text.Replace(",", ".") + "'," +
                    "id_poslovnica='" + poslovnica + "'," +
                    "valuta='" + txtValuta.Text.Replace(",", ".") + "'," +
                    "editirano='1'," +
                    "iznos='" + iznos_ukupno.ToString().Replace(",", ".") + "'," +
                    "napomena='" + rtbNapomena.Text + "' WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1'" +
                    "";

                provjera_sql(classSQL.update(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje kalkulacije robe br." + txtBroj.Text + "')"));
            }

            ///////////FUNKCIJA PROVJERAVA DALI POSTOJI RAZLIKA U PRODAJNIM CIJENAMA///////////////
            ///////////I ZAPISUJE U TABLICU "promjena_cijene_auto"////////////////////////////////
            //ProvjeraCijenaIzradaZapisnika_O_Promijeni();

            string kol = "";
            provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1' AND id_skladiste='" + skladiste_edit + "'"));
            for (int i = 0; i < dgw.RowCount; i++)
            {
                //***************************************PROVJERA*************************************************

                try { decimal.TryParse(dg(i, "pdv"), out _pdv); } catch { _pdv = 0; } //Try catch je ako se desi greška sa cb

                decimal.TryParse(dg(i, "upakiranju").Replace(".", ","), out _u_pakiranju);
                decimal.TryParse(dg(i, "brojpaketa").Replace(".", ","), out _brojPaketa);

                //kolicina
                dgw.Rows[i].Cells["kolicina"].Value = Math.Round((_u_pakiranju * _brojPaketa), 4).ToString("#0.0000");
                decimal.TryParse(dg(i, "kolicina").Replace(".", ","), out _kolicina);

                //cijena
                decimal.TryParse(dg(i, "cijena").Replace(".", ","), out _cijena);
                decimal.TryParse(dg(i, "rabat").Replace(".", ","), out _rabat);
                dgw.Rows[i].Cells["nbc"].Value = Math.Round(_cijena * (1 - _rabat / 100), 4).ToString("#0.0000");
                dgw.Rows[i].Cells["nbcIznos"].Value = Math.Round(((_cijena * (1 - _rabat / 100)) * _kolicina), 4).ToString("#0.0000");
                decimal.TryParse(dg(i, "povratna_naknada").Replace(".", ","), out _povratna_naknada);

                //marza
                decimal.TryParse(dg(i, "nbc"), out _nabavna);
                if (!decimal.TryParse(dg(i, "marza").Replace(".", ","), out _marza)) { txtMarza.Text = "0,00"; };
                dgw.Rows[i].Cells["iznos_marze"].Value = (_nabavna * _marza / 100).ToString("#0.00000");
                dgw.Rows[i].Cells["prodajna_cijena"].Value = (_nabavna + (_nabavna * _marza / 100)).ToString("#0.0000");

                dgw.Rows[i].Cells["prodajna_cijena_sa_porezom"].Value = (((_nabavna + (_nabavna * _marza / 100)) * (1 + (_pdv / 100))) + _povratna_naknada).ToString("#0.00");

                sql = "INSERT INTO primka_stavke (" +
                    "broj_primke,id_skladiste,sifra,u_pakiranju,broj_paketa,kolicina,cijena_po_komadu,rabat," +
                    "nabavna_cijena,ulazni_porez,nabavni_iznos,iznos,marza,iznos_marze,prodajna_cijena,prodajna_cijena_sa_porezom,povratna_naknada,is_kalkulacija)" +
                    " VALUES " +
                    "(" +
                        "'" + txtBroj.Text + "'," +
                        "'" + cbSkladiste.SelectedValue.ToString() + "'," +
                        "'" + dg(i, "sifra") + "'," +
                        "'" + _u_pakiranju.ToString().Replace(",", ".") + "'," +
                        "'" + _brojPaketa.ToString().Replace(",", ".") + "'," +
                        "'" + _kolicina.ToString().Replace(",", ".") + "'," +
                        "'" + _cijena.ToString().Replace(",", ".") + "'," +
                        "'" + _rabat.ToString().Replace(",", ".") + "'," +
                        "'" + dg(i, "nbc").Replace(",", ".") + "'," +
                        "'" + _pdv.ToString().Replace(",", ".") + "'," +
                        "'" + dg(i, "nbcIznos").Replace(",", ".") + "'," +
                        "'" + dg(i, "iznos").Replace(",", ".") + "'," +
                        "'" + _marza.ToString().Replace(",", ".") + "'," +
                        "'" + dg(i, "iznos_marze").Replace(",", ".") + "'," +
                        "'" + dg(i, "prodajna_cijena").Replace(",", ".") + "'," +
                        "'" + dg(i, "prodajna_cijena_sa_porezom").Replace(",", ".") + "'," +
                        "'" + _povratna_naknada.ToString().Replace(",", ".") + "','1'" +
                    ")";

                provjera_sql(classSQL.insert(sql));

                robno.PostaviNabavneCijeneZaTablicuRobaPremaSifri(dg(i, "sifra"));

                try
                {
                    decimal _nova_cijena;
                    decimal.TryParse(dgw.Rows[i].Cells["prodajna_cijena_sa_porezom"].FormattedValue.ToString(), out _nova_cijena);
                    classSQL.update("UPDATE roba_prodaja SET povratna_naknada='" + dgw.Rows[i].Cells["povratna_naknada"].FormattedValue.ToString().Replace(",", ".") + "',u_pakiranju='" + dgw.Rows[i].Cells["upakiranju"].FormattedValue.ToString().Replace(",", ".") + "', nc='" + dg(i, "cijena").Replace(",", ".") + "' WHERE sifra='" + dg(i, "sifra") + "'");
                    //classSQL.update("UPDATE roba SET mpc='" + _nova_cijena.ToString().Replace(".", ",") + "' WHERE sifra='" + dg(i, "sifra") + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (dg(i, "id_stavka") == "")
                {
                    //dodaje na skladiste
                    kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "+");
                    SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                }
                else
                {
                    if (DTartikli.Rows.Count != 0)
                    {
                        DataRow[] dataROW = DTartikli.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                        if (cbSkladiste.SelectedValue.ToString() == skladiste_edit)
                        {
                            kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "-");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                        else
                        {
                            //oduzima sa starog skladista
                            kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                            SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                            //dodaje na novo skladiste
                            kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                    }
                }
            }

            EnableDisable(true, false, false);
            DeleteFields(true, true, true, true);
            txtBroj.Text = brojPrimke();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);

            robno.PostaviNabavneCijeneZaTablicuRoba();

            MessageBox.Show("Spremljeno");

            lblPorez.Text = "Porez: 0.00 kn";
            lblUkupno.Text = "Ukupno: 0.00 kn";
            ;
        }

        #region PROVJERA CIJENA I AKO SU RAZLIČITE IZRADI ZAPISNIK O PROMJENI CIJENE

        private void ProvjeraCijenaIzradaZapisnika_O_Promijeni()
        {/*
            try
            {
                DataTable DTprodajni_artikli = classSQL.select("SELECT * FROM roba", "roba").Tables[0];
                DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                string poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string broj_kalkulacije = txtBroj.Text;

                foreach(DataGridViewRow row in dgw.Rows)
                {
                    string _sifra = row.Cells["sifra"].FormattedValue.ToString();
                    decimal _nova_cijena, _stara_cijena, _kolicina,_porez;
                    string _datum = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");

                    DataRow[] r =               DTprodajni_artikli.Select("sifra='" + _sifra + "'");

                    if (r.Length > 0)
                    {
                        decimal.TryParse(row.Cells["prodajna_cijena_sa_porezom"].FormattedValue.ToString(), out _nova_cijena);
                        decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out _kolicina);

                        ///PROVJERAVAM STARU CIJENU
                       // DataTable DTpromjena_cijene_auto = classSQL.select("SELECT * FROM promjena_cijene_auto WHERE sifra='" + _sifra + "' ORDER BY id DESC LIMIT 1", "promjena_cijene_auto").Tables[0];
                       // if (DTpromjena_cijene_auto.Rows.Count == 0)
                       // {
                            decimal.TryParse(r[0]["mpc"].ToString(), out _stara_cijena);
                       // }
                       // else
                       // {
                       //     decimal.TryParse(DTpromjena_cijene_auto.Rows[0]["mpc"].ToString(), out _stara_cijena);
                       // }

                        decimal.TryParse(r[0]["porez"].ToString(), out _porez);

                        _nova_cijena = Math.Round(_nova_cijena, 2);
                        _stara_cijena = Math.Round(_stara_cijena, 2);

                        if (_nova_cijena != _stara_cijena || edit)
                        {
                            string sql = "BEGIN;" +
                                "DELETE FROM promjena_cijene_auto WHERE sifra='" + _sifra + "' AND kalkulacija='" + broj_kalkulacije + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "';" +
                                "INSERT INTO promjena_cijene_auto (sifra,stara_cijena,nova_cijena,kolicina," +
                                "datum,poslovnica,kalkulacija,id_skladiste,novo,editirano,porez) VALUES (" +
                                "'" + _sifra + "'," +
                                "'" + _stara_cijena.ToString().Replace(",", ".") + "'," +
                                "'" + _nova_cijena.ToString().Replace(",", ".") + "'," +
                                "'" + _kolicina.ToString().Replace(",", ".") + "'," +
                                "'" + _datum + "'," +
                                "'" + poslovnica + "'," +
                                "'" + broj_kalkulacije + "'," +
                                "'" + cbSkladiste.SelectedValue.ToString() + "'," +
                                "'1'," +
                                "'1'," +
                                "'" + _porez.ToString().Replace(",", ".") + "'" +
                                ");" +
                                "UPDATE roba SET mpc='" + _nova_cijena.ToString().Replace(".",",") + "' WHERE sifra='" + _sifra + "';" +
                                "COMMIT;";

                            classSQL.insert(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }

        #endregion PROVJERA CIJENA I AKO SU RAZLIČITE IZRADI ZAPISNIK O PROMJENI CIJENE

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraPartnera.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifraOdredista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBrojUlaznogDok.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtMjestoTroska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtIznosBezPoreza.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtOrginalniDok_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTRoba;

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba" +
                    " WHERE sifra='" + txtSifra_robe.Text + "' AND oduzmi='DA'";

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'  AND oduzmi='DA'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            #region OVO KORISTI CELKA

            //DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            //DataTable DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];
            //if (DTzaposlenik.Rows.Count > 0)
            //{
            //    if (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "05593216962" &&
            //         DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "1" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "2")
            //    {
            //        MessageBox.Show("Nemate ovlaštenja pristupati ovoj funkcionalnosti u programu!");
            //        return;
            //    }
            //}

            #endregion OVO KORISTI CELKA

            this.TopMost = false;

            Robno.frmSveKalkulacije sp = new Robno.frmSveKalkulacije();
            sp.MainForm = this;
            sp.ShowDialog();

            this.TopMost = true;

            if (broj_primke_edit != null)
            {
                DeleteFields(true, true, true, true);
                EnableDisable(false, true, true);
                FillPrimke(broj_primke_edit, skladiste_edit);
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove kalkulacije oduzimate i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu primku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        DataRow[] dataROW = DTartikli.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                        skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_edit + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        skl = skl - fa_kolicina;
                        provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_edit + "'"));
                    }
                }
                classSQL.update("UPDATE primka SET editirano='1',iznos='0' WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='1'");
                //provjera_sql(classSQL.delete("DELETE FROM primka WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'"));
                provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'"));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele kalkulacije br." + txtBroj.Text + "')"));
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(true, false, false);
                DeleteFields(false, true, true, true);
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }

            if (MessageBox.Show("Brisanjem ove stavke mijenjate i količinu robe na skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                int i = dgw.CurrentRow.Index;

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "")
                {
                    DataRow[] dataROW = DTartikli.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_edit + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                    fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                    skl = skl - fa_kolicina;
                    provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND is_kalkulacija='1' AND id_skladiste='" + skladiste_edit + "'"));
                }

                provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND sifra='" + dgw.Rows[i].Cells["sifra"].Value.ToString() + "'"));

                MessageBox.Show("Obrisano.");
                EnableDisable(false, true, false);
                DeleteFields(false, false, true, false);
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                Izracun();
                classSQL.update("UPDATE primka SET editirano='1', iznos='" + iznos_ukupno.ToString().Replace(",", ".") + "' WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='1'");
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    dgw.CurrentRow.Cells[4].Selected = true;
                    //dgw.CurrentCell = dgw.CurrentRow.Cells[5];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[5];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[7];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }
        }

        private void txtSifra_robe_Validated(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            if (txtSifra_robe.Text != "")
            {
                //return;
            }

            //for (int y = 0; y < dgw.Rows.Count; y++)
            //{
            //    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
            //    {
            //        MessageBox.Show("Artikl ili usluga već postoje u ovoj primki.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}

            string sql = "SELECT * FROM roba_prodaja" +
                " WHERE sifra='" + txtSifra_robe.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND aktivnost='1'";

            DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                txtNazivArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                txtSifra_robe.BackColor = Color.White;
                SetRoba();
                EnableDisable(false, true, true);
                txtUpakiranju.Select();
            }
            else
            {
                MessageBox.Show("Za ovu šifru ne postoj artikl ili ga nema na odabranom skladištu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifra_robe.Select();
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];

            //double mpc = Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString());
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["ulazni_porez"].ToString());

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1,000";
            dgw.Rows[br].Cells["cijena"].Value = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.000");
            dgw.Rows[br].Cells["rabat"].Value = "0,000";
            dgw.Rows[br].Cells["nbc"].Value = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.000");
            dgw.Rows[br].Cells["pdv"].Value = pdv;
            dgw.Rows[br].Cells["ukupno"].Value = ((Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()) * pdv / 100) + Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString())).ToString("#0.00");
            dgw.Rows[br].Cells["upakiranju"].Value = "1,000";
            dgw.Rows[br].Cells["brojpaketa"].Value = "1,000";
            dgw.Rows[br].Cells["nbcIznos"].Value = "1,000";
            dgw.Rows[br].Cells["iznos"].Value = "1,000";
            dgw.Rows[br].Cells["id_stavka"].Value = "";

            txtKolicina.Text = "1,000";
            txtNabavnaCijena.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.0000");
            txtNabavniIznos.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.0000");
            txtRabat.Text = "0,000";
            //txtIznos.Text = ((Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()) * Convert.ToDouble(cbUlazniPorez.SelectedValue.ToString()) / 100) + Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString())).ToString("#0.00");
            txtCijenaPoKom.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.0000");
            txtBrojPaketa.Text = "1,000";
            txtUpakiranju.Text = DTRoba.Rows[0]["u_pakiranju"].ToString();
            txtPorez.Text = pdv.ToString();
            txtPovratnaNaknada.Text = DTRoba.Rows[0]["povratna_naknada"].ToString();
            cbUlazniPorez.SelectedValue = pdv;

            PoravnajPremaStarojCijeni();

            PaintRows(dgw);
            Izracun();
            SetInDgw();
            txtProdajnaCijena_Validated(null, null);
            IzracunMarza___Ukupno();
        }

        private DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
        private DataTable DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];

        private void PoravnajPremaStarojCijeni()
        {
            decimal cijena_bez_pdva = 0, cijena_sa_pdvom = 0, porez_ = 0;
            /*DataTable DTcijena = classSQL.select("SELECT * FROM primka_stavke WHERE is_kalkulacija='1' AND sifra='" + DTRoba.Rows[0]["sifra"].ToString() + "' ORDER BY id_stavka DESC LIMIT 1", "primka_stavke").Tables[0];

            if (DTcijena.Rows.Count > 0)
            {
                decimal.TryParse(DTcijena.Rows[0]["prodajna_cijena"].ToString(), out cijena_bez_pdva);
                decimal.TryParse(DTcijena.Rows[0]["prodajna_cijena_sa_porezom"].ToString(), out cijena_sa_pdvom);
            }
            else if (DTcijena.Rows.Count == 0)*/
            {
                DataTable DTcijena = classSQL.select("SELECT * FROM roba WHERE sifra='" + DTRoba.Rows[0]["sifra"].ToString() + "' LIMIT 1", "primka_stavke").Tables[0];
                if (DTcijena.Rows.Count > 0)
                {
                    decimal.TryParse(DTcijena.Rows[0]["porez"].ToString(), out porez_);
                    decimal.TryParse(DTcijena.Rows[0]["mpc"].ToString(), out cijena_sa_pdvom);
                    cijena_bez_pdva = (cijena_sa_pdvom / (1 + (porez_ / 100)));
                }
            }

            txtProdajnaCijena.Text = Math.Round(cijena_bez_pdva, 3).ToString("#0.00");
            txtProdajnaCijenaSaPorezom.Text = Math.Round(cijena_sa_pdvom, 3).ToString("#0.00");

            decimal.TryParse(txtUpakiranju.Text.Replace(".", ","), out _u_pakiranju);
            if (_u_pakiranju == 0) { txtUpakiranju.Text = "1"; }

            decimal.TryParse(txtCijenaPoKom.Text.Replace(".", ","), out _cijena);
            if (_cijena == 0) { txtCijenaPoKom.Text = "1"; txtNabavnaCijena.Text = "1"; }
        }

        private decimal _kolicina = 0;
        private decimal _rabat = 0;
        private decimal _cijena = 0;
        private decimal _pdv = 0;
        private decimal _nabavna = 0;
        private decimal _u_pakiranju = 0;
        private decimal _brojPaketa = 0;
        private decimal _marza = 0;
        private decimal _prodajna_cijena = 0;
        private decimal _ProdajnaCijenaSaPorezom = 0;
        private decimal _povratna_naknada = 0;
        private decimal dec;

        private void txtUpakiranju_Validated(object sender, EventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void txtPovratnaNaknada_Validated(object sender, EventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void txtBrojPaketa_Validated(object sender, EventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void txtKolicina_Validated(object sender, EventArgs e)
        {
            _kolicina = Convert.ToDecimal(txtKolicina.Text);
            _brojPaketa = Convert.ToDecimal(txtBrojPaketa.Text);

            txtUpakiranju.Text = (_kolicina / _brojPaketa).ToString("#0.000");
            IzracunMarza___Ukupno();
        }

        private void txtCijenaPoKom_Validated(object sender, EventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void txtMarza_Validated(object sender, EventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void IzracunMarza___Ukupno()
        {
            try { decimal.TryParse(cbUlazniPorez.SelectedValue.ToString(), out _pdv); } catch { _pdv = 0; } //Try catch je ako se desi greška sa cb

            decimal.TryParse(txtUpakiranju.Text.Replace(".", ","), out _u_pakiranju);
            decimal.TryParse(txtBrojPaketa.Text.Replace(".", ","), out _brojPaketa);
            txtKolicina.Text = Math.Round((_u_pakiranju * _brojPaketa), 4).ToString("#0.0000");

            decimal.TryParse(txtKolicina.Text.Replace(".", ","), out _kolicina);

            //cijena
            decimal.TryParse(txtCijenaPoKom.Text.Replace(".", ","), out _cijena);
            decimal.TryParse(txtRabat.Text.Replace(".", ","), out _rabat);
            txtNabavnaCijena.Text = Math.Round(_cijena * (1 - _rabat / 100), 4).ToString("#0.0000");
            txtNabavniIznos.Text = Math.Round(((_cijena * (1 - _rabat / 100)) * _kolicina), 4).ToString("#0.0000");

            decimal.TryParse(txtPovratnaNaknada.Text.Replace(".", ","), out _povratna_naknada);

            //marza
            decimal.TryParse(txtNabavnaCijena.Text.Replace(".", ","), out _nabavna);
            if (!decimal.TryParse(txtMarza.Text.Replace(".", ","), out _marza)) { txtMarza.Text = "0,00"; };
            txtIznosMarze.Text = (_nabavna * _marza / 100).ToString("#0.000");

            txtProdajnaCijena.Text = (_nabavna + (_nabavna * _marza / 100)).ToString("#0.00");
            txtProdajnaCijenaSaPorezom.Text = (((_nabavna + (_nabavna * _marza / 100)) * (1 + (_pdv / 100))) + _povratna_naknada).ToString("#0.00");
            try
            {
                if (DTzaposlenik.Rows.Count > 0)
                {
                    if (/*DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "05593216962" &&*/
                         DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "1" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "2")
                    {
                        PoravnajPremaStarojCijeni();
                    }
                }
                decimal.TryParse(txtProdajnaCijena.Text.Replace(".", ","), out _prodajna_cijena);
                txtMarza.Text = ((_prodajna_cijena / _nabavna - 1) * 100).ToString("#0.0000");
                if (!decimal.TryParse(txtMarza.Text.Replace(".", ","), out _marza)) { txtMarza.Text = "0,00"; };
                txtIznosMarze.Text = (_nabavna * _marza / 100).ToString("#0.000");
            }
            catch { }

            Izracun();
            SetInDgw();
        }

        private void txtProdajnaCijena_Validated(object sender, EventArgs e)
        {
            decimal.TryParse(txtNabavnaCijena.Text, out _nabavna);
            decimal.TryParse(txtProdajnaCijena.Text, out _prodajna_cijena);
            if (_prodajna_cijena == 0) { MessageBox.Show("Greška kod unosa prodajne cijene. Molimo provijerite prodajnu cijenu."); return; }
            if (_nabavna == 0) { return; }
            txtMarza.Text = ((_prodajna_cijena / _nabavna - 1) * 100).ToString("#0.0000");
            IzracunMarza___Ukupno();
            Izracun();
            SetInDgw();
        }

        private void txtProdajnaCijenaSaPorezom_Validated(object sender, EventArgs e)
        {
            decimal.TryParse(txtNabavnaCijena.Text, out _nabavna);
            decimal.TryParse(txtProdajnaCijenaSaPorezom.Text, out _ProdajnaCijenaSaPorezom);
            decimal.TryParse(txtPovratnaNaknada.Text, out _povratna_naknada);
            decimal.TryParse(txtProdajnaCijena.Text, out _prodajna_cijena);
            if (_ProdajnaCijenaSaPorezom == 0) { MessageBox.Show("Greška kod unosa"); return; }

            txtMarza.Text = (((((_ProdajnaCijenaSaPorezom - _povratna_naknada) / (1 + (_pdv / 100)))) / _nabavna - 1) * 100).ToString("#0.0000");

            IzracunMarza___Ukupno();
            Izracun();
            SetInDgw();
        }

        private void txtRabat_Validating(object sender, CancelEventArgs e)
        {
            IzracunMarza___Ukupno();
        }

        private void cbUlazniPorez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                IzracunMarza___Ukupno();
                Izracun();
                SetInDgw();
            }
        }

        private void SetInDgw()
        {
            int br = dgw.CurrentRow.Index;

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["naziv"].Value = txtNazivArtikla.Text;
            dgw.Rows[br].Cells["kolicina"].Value = txtKolicina.Text;
            dgw.Rows[br].Cells["cijena"].Value = txtCijenaPoKom.Text;
            dgw.Rows[br].Cells["nbc"].Value = txtNabavnaCijena.Text;
            dgw.Rows[br].Cells["rabat"].Value = txtRabat.Text;
            dgw.Rows[br].Cells["pdv"].Value = txtPorez.Text;
            dgw.Rows[br].Cells["ukupno"].Value = (Convert.ToDouble(txtNabavnaCijena.Text) * Convert.ToDouble(txtKolicina.Text)).ToString("#0.000");
            dgw.Rows[br].Cells["upakiranju"].Value = txtUpakiranju.Text;
            dgw.Rows[br].Cells["brojpaketa"].Value = txtBrojPaketa.Text;
            dgw.Rows[br].Cells["nbcIznos"].Value = txtNabavniIznos.Text;
            dgw.Rows[br].Cells["iznos"].Value = "0";//txtIznos.Text;
            dgw.Rows[br].Cells["marza"].Value = txtMarza.Text;
            dgw.Rows[br].Cells["iznos_marze"].Value = txtIznosMarze.Text;
            dgw.Rows[br].Cells["prodajna_cijena"].Value = txtProdajnaCijena.Text;
            dgw.Rows[br].Cells["prodajna_cijena_sa_porezom"].Value = txtProdajnaCijenaSaPorezom.Text;
            dgw.Rows[br].Cells["povratna_naknada"].Value = txtPovratnaNaknada.Text;
        }

        private void Izracun()
        {
            decimal p = 0;
            decimal u = 0;
            decimal ss = 0;
            decimal pp = 0;

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                try
                {
                    ss = Convert.ToDecimal(dgw.Rows[i].Cells["ukupno"].FormattedValue.ToString());
                    pp = Convert.ToDecimal(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());

                    u = ss + u;
                    p = (ss * pp / 100) + p;

                    //provjera-------------
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Krivi unos. \r\n" + ex, "Greška");
                }
            }

            iznos_ukupno = u;

            IznosNetto = u;

            txtProvjeraNetoIzFakture.Text = u.ToString("#0.00");
            txtProvjeraSaPorezom.Text = p.ToString("#0.00");
            txtProvjeraNetoSaPorezom.Text = (u + p).ToString("#0.00");

            lblUkupno.Text = "Ukupno: " + u.ToString("#0.00") + " kn";
            lblPorez.Text = "Porez: " + p.ToString("#0.00") + " kn";
        }

        private void btnNovaStavka_Click(object sender, EventArgs e)
        {
            EnableDisable(false, true, false);
            DeleteFields(false, false, true, false);
        }

        private void cbValuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                txtValuta.Text = cbValuta.SelectedValue.ToString();
            }
        }

        private void txtSifra_robe_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtSifra_robe.Text == "")
                {
                    this.TopMost = false;
                    frmRobaTrazi roba_trazi = new frmRobaTrazi();
                    roba_trazi.ShowDialog();
                    this.TopMost = true;

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void pictureBoxPartner_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            this.TopMost = true;
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivDobavljaca.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void pictureBartikli_Click(object sender, EventArgs e)
        {
            this.TopMost = false;

            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();

            this.TopMost = true;

            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj primki.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba_prodaja WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    if (txtSifra_robe.Text == "")
                    {
                        txtNazivArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                        txtSifra_robe.BackColor = Color.White;
                        SetRoba();
                        EnableDisable(false, true, true);
                        txtUpakiranju.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbSkladiste_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void txtSifraPartnera_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtSifraPartnera.Text == "")
                {
                    this.TopMost = false;
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    this.TopMost = true;
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivDobavljaca.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            dtpDatum.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivDobavljaca.Text = DSpar.Rows[0][0].ToString();
                    dtpDatum.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    txtSifraPartnera.Select();
                }
            }
        }

        private void dgw_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                int br = dgw.CurrentRow.Index;
                txtSifra_robe.Text = dgw.Rows[br].Cells["sifra"].FormattedValue.ToString();
                txtNazivArtikla.Text = dgw.Rows[br].Cells["naziv"].FormattedValue.ToString();
                txtKolicina.Text = dgw.Rows[br].Cells["kolicina"].FormattedValue.ToString();
                txtCijenaPoKom.Text = dgw.Rows[br].Cells["cijena"].FormattedValue.ToString();
                txtRabat.Text = dgw.Rows[br].Cells["rabat"].FormattedValue.ToString();
                txtNabavnaCijena.Text = dgw.Rows[br].Cells["nbc"].FormattedValue.ToString();
                txtPorez.Text = dgw.Rows[br].Cells["pdv"].FormattedValue.ToString();
                //txtIznos.Text = dgw.Rows[br].Cells["ukupno"].FormattedValue.ToString();
                txtUpakiranju.Text = dgw.Rows[br].Cells["upakiranju"].FormattedValue.ToString();
                txtBrojPaketa.Text = dgw.Rows[br].Cells["brojpaketa"].FormattedValue.ToString();
                txtNabavniIznos.Text = dgw.Rows[br].Cells["nbcIznos"].FormattedValue.ToString();
                //txtIznos.Text = dgw.Rows[br].Cells["iznos"].FormattedValue.ToString();
                txtMarza.Text = dgw.Rows[br].Cells["marza"].FormattedValue.ToString();
                txtIznosMarze.Text = dgw.Rows[br].Cells["iznos_marze"].FormattedValue.ToString();
                txtProdajnaCijena.Text = dgw.Rows[br].Cells["prodajna_cijena"].FormattedValue.ToString();
                txtProdajnaCijenaSaPorezom.Text = dgw.Rows[br].Cells["prodajna_cijena_sa_porezom"].FormattedValue.ToString();
                txtPovratnaNaknada.Text = dgw.Rows[br].Cells["povratna_naknada"].FormattedValue.ToString();
            }
        }

        private void txtBroj_KeyDown_1(object sender, KeyEventArgs e)
        {
            #region OVO KORISTI CELKA

            //DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            //DataTable DTzaposlenik = classSQL.select("SELECT * FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "postavke").Tables[0];
            //if (DTzaposlenik.Rows.Count > 0)
            //{
            //    if (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == "05593216962" &&
            //         DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "1" && DTzaposlenik.Rows[0]["id_dopustenje"].ToString() != "2")
            //    {
            //        MessageBox.Show("Nemate ovlaštenja pristupati ovoj funkcionalnosti u programu!");
            //        return;
            //    }
            //}

            #endregion OVO KORISTI CELKA

            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_primke FROM primka WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='1' AND id_skladiste='" + cbSkladiste.SelectedValue + "'", "primka").Tables[0];
                DeleteFields(false, true, true, true);
                if (DT.Rows.Count == 0)
                {
                    if (brojPrimke() == txtBroj.Text)
                    {
                        DeleteFields(false, true, true, true);
                        edit = false;
                        EnableDisable(false, true, true);
                        txtBrojUlaznogDok.Select();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_primke_edit = txtBroj.Text;
                    skladiste_edit = cbSkladiste.SelectedValue.ToString();
                    edit = true;
                    EnableDisable(false, true, true);
                    FillPrimke(broj_primke_edit, cbSkladiste.SelectedValue.ToString());

                    txtBrojUlaznogDok.Select();
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPrimke(string _broj_primke, string _skladiste)
        {
            cbSkladiste.SelectedValue = _skladiste;

            string sql = "SELECT * FROM primka WHERE broj_primke='" + _broj_primke + "' AND id_skladiste='" + _skladiste + "' AND is_kalkulacija='1'";
            DataTable DTheader = classSQL.select(sql, "primka").Tables[0];

            DateTime _DT;
            DateTime.TryParse(DTheader.Rows[0]["datum"].ToString(), out _DT);

            string ovl = Util.Korisno.UzmiOvlastTrenutnogZaposlenika();
            int br_days = 366;
            if (ovl == "user")
                br_days = 100;

            if (Util.Korisno.ZabranaUređivanjaDokumenta(br_days, _DT, ovl))
            {
                EnableDisable(true, false, false);
                DeleteFields(true, true, true, true);
                edit = false;
                MessageBox.Show("Nemate ovlaštenje uređivati ovaj dokumenat nakon " + br_days + " dana od izrade istog.");
                return;
            }

            sql = "SELECT primka_stavke.id_stavka," +
                " primka_stavke.sifra," +
                " primka_stavke.u_pakiranju," +
                " primka_stavke.broj_paketa," +
                " primka_stavke.kolicina," +
                " primka_stavke.cijena_po_komadu, " +
                " primka_stavke.rabat, " +
                " primka_stavke.nabavna_cijena, " +
                " primka_stavke.ulazni_porez, " +
                " primka_stavke.nabavni_iznos, " +
                " primka_stavke.iznos, " +
                " primka_stavke.marza, " +
                " primka_stavke.iznos_marze, " +
                " primka_stavke.prodajna_cijena, " +
                " primka_stavke.prodajna_cijena_sa_porezom, " +
                " primka_stavke.prodajna_cijena_sa_porezom, " +
                " primka_stavke.povratna_naknada, " +
                " primka_stavke.id_skladiste, " +
                " roba_prodaja.naziv " +
                " FROM primka_stavke " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=primka_stavke.sifra AND roba_prodaja.id_skladiste=primka_stavke.id_skladiste " +
                " WHERE primka_stavke.broj_primke='" + _broj_primke + "' AND primka_stavke.id_skladiste='" + _skladiste + "' AND is_kalkulacija='1'";
            DTartikli = classSQL.select(sql, "primka_stavke").Tables[0];

            if (DTheader.Rows.Count > 0)
            {
                //FILL HEADER
                txtBrojUlaznogDok.Text = DTheader.Rows[0]["br_ulaznog_doc"].ToString();
                txtSifraPartnera.Text = DTheader.Rows[0]["id_partner"].ToString();
                try
                {
                    txtNazivDobavljaca.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTheader.Rows[0]["id_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
                }
                catch { };
                dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
                txtIznosBezPoreza.Text = DTheader.Rows[0]["iznos_bez_poreza"].ToString();
                txtIznosSaPorezom.Text = DTheader.Rows[0]["iznos_sa_porezom"].ToString();
                txtCarina.Text = DTheader.Rows[0]["carina"].ToString();
                txtValuta.Text = DTheader.Rows[0]["valuta"].ToString();
                rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
                txtBroj.Text = DTheader.Rows[0]["broj_primke"].ToString();

                //FILL STAVKE
                int rb = 1;
                foreach (DataRow row in DTartikli.Rows)
                {
                    decimal ukupno, kom, nbc;
                    decimal.TryParse(row["kolicina"].ToString(), out kom);
                    decimal.TryParse(row["nabavna_cijena"].ToString(), out nbc);

                    ukupno = kom * nbc;

                    dgw.Rows.Add(rb,
                        row["sifra"].ToString(),
                        row["naziv"].ToString(),
                        row["kolicina"].ToString(),
                        row["cijena_po_komadu"].ToString(),
                        row["rabat"].ToString(),
                        row["nabavna_cijena"].ToString(),
                        row["ulazni_porez"].ToString(),
                        Math.Round(ukupno, 2).ToString("#0.00"),
                        row["u_pakiranju"].ToString(),
                        row["broj_paketa"].ToString(),
                        row["id_stavka"].ToString(),
                        row["nabavni_iznos"].ToString(),
                        row["iznos"].ToString(),
                        row["marza"].ToString(),
                        row["iznos_marze"].ToString(),
                        row["prodajna_cijena"].ToString(),
                        row["prodajna_cijena_sa_porezom"].ToString(),
                        row["povratna_naknada"].ToString());
                    rb++;
                }
                Izracun();
                edit = true;
            }
        }

        private void cbSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                txtBroj.Text = brojPrimke();
            }
        }

        private void frmPrimka_Resize(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnableDisable(false, true, true);
        }

        private void frmPrimka_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                if (MessageBox.Show("Želite li spremiti primku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    btnSpremi.PerformClick();
                }
            }

            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            robno.PostaviStanjeSkladista();
            try
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
        }

        private void txtNabavnaCijena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void txtPorez_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void btnObrisiNormativ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void btnNovaStavka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNovaStavka.PerformClick();
                return;
            }
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
        }

        private void txtNabavniIznos_Validated(object sender, EventArgs e)
        {
            decimal iznos_nabavni, kol, rabat;
            decimal.TryParse(txtNabavniIznos.Text, out iznos_nabavni);
            decimal.TryParse(txtKolicina.Text, out kol);
            decimal.TryParse(txtRabat.Text, out rabat);

            //decimal ppstopa = (((rabat / (iznos_nabavni / kol)) * 100 * 1000) / 1000);
            //txtCijenaPoKom.Text = Math.Round((iznos_nabavni / kol) * (1 + (ppstopa / 100)), 4).ToString();
            txtCijenaPoKom.Text = Math.Round((((100 * (iznos_nabavni)) / (100 - rabat)) / _kolicina), 4).ToString("#0.0000");

            //txtCijenaPoKom.Text = Math.Round((iznos_nabavni / kol), 4).ToString("#0.0000");
            txtNabavnaCijena.Text = Math.Round((iznos_nabavni / kol) - (((iznos_nabavni / kol) * rabat) / 100), 4).ToString("#0.0000");

            IzracunMarza___Ukupno();
            Izracun();
            SetInDgw();
        }

        private void frmKalkulacija_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
        }

        private void txtIznosSaPorezom_Validated(object sender, EventArgs e)
        {
            decimal d;
            decimal.TryParse(txtIznosSaPorezom.Text, out d);
            txtIznosSaPorezom.Text = d.ToString("#0.00");
        }

        private void txtIznosBezPoreza_Validated(object sender, EventArgs e)
        {
            decimal d;
            decimal.TryParse(txtIznosBezPoreza.Text, out d);
            txtIznosBezPoreza.Text = d.ToString("#0.00");
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