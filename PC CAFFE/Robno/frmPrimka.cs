using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmPrimka : Form
    {
        public frmPrimka()
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
        public string PrimkaId { get; set; }

        //public string broj_primke_edit { get; set; }
        private decimal valuta;

        private DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
        private string poslovnica = "1";

        private void frmPrimka_Load(object sender, EventArgs e)
        {
            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
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
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_primke AS INT)) FROM primka WHERE is_kalkulacija='False' and id_skladiste = '" + cbSkladiste.SelectedValue + "'", "primka").Tables[0];
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
            lblMpcUkupno.Text = "Ukupno: 0.00 kn";
            ;
        }

        private void EnableDisable(bool a, bool x, bool y)
        {
            txtBroj.Enabled = a;
            cbSkladiste.Enabled = a;

            pictureBartikli.Enabled = x;
            button2.Enabled = x;
            btnObrisiNormativ.Enabled = x;
            txtBrojUlaznogDok.Enabled = x;
            txtSifraPartnera.Enabled = x;
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
            lblMpcUkupno.Text = "Ukupno: 0.00 kn";
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
                txtIznosBezPoreza.Text = "0,000";
                txtIznosSaPorezom.Text = "0,000";
                txtCarina.Text = "0,000";
                rtbNapomena.Text = "";
            }

            if (stavke)
            {
                txtSifra_robe.Text = "";
                txtNazivArtikla.Text = "";
                txtUpakiranju.Text = "0,000";
                txtBrojPaketa.Text = "0,000";
                txtKolicina.Text = "0,000";
                txtPovratnaNaknada.Text = "0,00";
                txtCijenaPoKom.Text = "0,000";
                txtNabavnaCijena.Text = "0,000";
                txtNabavniIznos.Text = "0,00";
                txtIznos.Text = "";
                txtNabavniIznos.Text = "0,00";
                txtRabat.Text = "0,000";
                txtPorez.Text = "0,000";
                txtSifra_robe.Select();
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
                decimal ukupan_iznos_bez_poreza, ukupan_iznos_sa_porezom, RAZLIKA_BEZ_POREZA, RAZLIKA_SA_POREZOM;
                Izracun();

                decimal.TryParse(txtIznosBezPoreza.Text, out ukupan_iznos_bez_poreza);
                decimal.TryParse(txtIznosSaPorezom.Text, out ukupan_iznos_sa_porezom);

                RAZLIKA_BEZ_POREZA = ukupan_iznos_bez_poreza - IznosNetto;
                RAZLIKA_SA_POREZOM = ukupan_iznos_sa_porezom - IznosUkupno;

                if (RAZLIKA_BEZ_POREZA > 1 || RAZLIKA_BEZ_POREZA < -1)
                {
                    MessageBox.Show("Primka ne može biti spremljena je postoje razlike više od jedne kune u cijeni bez pdv-a.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (RAZLIKA_SA_POREZOM > 1 || RAZLIKA_SA_POREZOM < -1)
                {
                    MessageBox.Show("Primka ne može biti spremljena je postoje razlike više od jedne kune u cijeni sa pdv-om.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            #endregion ZABRANA AKO IZNOSI NA PRIMKI I FAKTURI NISU JEDNAKI

            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            decimal dec_parse;

            if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                //MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška"); return;
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

            string sql = "";
            if (edit == false)
            {
                txtBroj.Text = brojPrimke();
                Izracun();

                sql = "INSERT INTO primka (" +
                    "broj_primke,id_skladiste,br_ulaznog_doc,id_partner,datum,iznos_bez_poreza,iznos_sa_porezom,carina,valuta,id_zaposlenik,napomena,is_kalkulacija,iznos,id_poslovnica,novo,editirano)" +
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
                    "'0'," +
                    "'" + iznos_ukupno.ToString().Replace(",", ".") + "','" + poslovnica + "','1','0'" +
                    ")";
                provjera_sql(classSQL.insert(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada primke br." + txtBroj.Text + "')"));
            }
            else
            {
                Izracun();
                PrimkaId = txtBroj.Text;

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
                    "is_kalkulacija='0'," +
                    "editirano='1'," +
                    "iznos='" + iznos_ukupno.ToString().Replace(",", ".") + "'," +
                    "napomena='" + rtbNapomena.Text + "' WHERE broj_primke='" + PrimkaId + "' AND is_kalkulacija='0'" +
                    "";

                provjera_sql(classSQL.update(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje primke robe br." + txtBroj.Text + "')"));
            }

            string kol = "";
            provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='0' AND id_skladiste='" + skladiste_edit + "'"));
            for (int i = 0; i < dgw.RowCount; i++)
            {
                sql = "INSERT INTO primka_stavke (" +
                    "broj_primke,id_skladiste,sifra,u_pakiranju,broj_paketa,kolicina,cijena_po_komadu,rabat,nabavna_cijena,ulazni_porez,nabavni_iznos,iznos,is_kalkulacija,povratna_naknada)" +
                    " VALUES " +
                    "(" +
                    "'" + txtBroj.Text + "'," +
                    "'" + cbSkladiste.SelectedValue.ToString() + "'," +
                    "'" + dg(i, "sifra") + "'," +
                    "'" + dg(i, "upakiranju").Replace(",", ".") + "'," +
                    "'" + dg(i, "brojpaketa").Replace(",", ".") + "'," +
                    "'" + dg(i, "kolicina").Replace(",", ".") + "'," +
                    "'" + dg(i, "cijena").Replace(",", ".") + "'," +
                    "'" + dg(i, "rabat").Replace(",", ".") + "'," +
                    "'" + dg(i, "nbc").Replace(",", ".") + "'," +
                    "'" + dg(i, "pdv").Replace(",", ".") + "'," +
                    "'" + dg(i, "nbcIznos").Replace(",", ".") + "'," +
                    "'" + dg(i, "mpcIznos").Replace(",", ".") + "','0','" + dg(i, "povratna_naknada").Replace(",", ".") + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql));

                decimal prosjecna_nabavna_cijena = 0;
                decimal prosjecna_nabavna_cijena_sum = 0;
                int broj_istih_sifra = 0;
                foreach (DataGridViewRow pr in dgw.Rows)
                {
                    if (pr.Cells["sifra"].FormattedValue.ToString() == dg(i, "sifra"))
                    {
                        broj_istih_sifra++;
                        decimal.TryParse(pr.Cells["cijena"].FormattedValue.ToString(), out prosjecna_nabavna_cijena);
                        prosjecna_nabavna_cijena_sum += prosjecna_nabavna_cijena;
                    }
                }

                prosjecna_nabavna_cijena = prosjecna_nabavna_cijena_sum / broj_istih_sifra;

                try
                {
                    classSQL.update("UPDATE roba_prodaja SET u_pakiranju='" + dgw.Rows[i].Cells["upakiranju"].FormattedValue.ToString().Replace(",", ".") + "', nc='" + Math.Round(prosjecna_nabavna_cijena, 4).ToString().Replace(",", ".") + "' WHERE sifra='" + dg(i, "sifra") + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                robno.PostaviNabavneCijeneZaTablicuRobaPremaSifri(dg(i, "sifra"));

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

            robno.PostaviNabavneCijeneZaTablicuRobaProdaja();
            robno.PostaviNabavneCijeneRoba();

            MessageBox.Show("Spremljeno");

            lblPorez.Text = "Porez: 0.00 kn";
            lblMpcUkupno.Text = "Ukupno: 0.00 kn";
            ;
        }

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

            Robno.frmSvePrimke sp = new Robno.frmSvePrimke();
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
            if (MessageBox.Show("Brisanjem ove primke oduzimate i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu primku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                classSQL.update("UPDATE primka SET editirano='1', iznos='0' WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='0'");
                //provjera_sql(classSQL.delete("DELETE FROM primka WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='0'"));
                provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='0'"));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele primke br." + txtBroj.Text + "')"));
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
                    provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_edit + "'"));
                }

                provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='0' AND sifra='" + dgw.Rows[i].Cells["sifra"].Value.ToString() + "'"));

                MessageBox.Show("Obrisano.");
                EnableDisable(false, true, false);
                DeleteFields(false, false, true, false);
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                Izracun();
                classSQL.update("UPDATE primka SET editirano='1', iznos='" + iznos_ukupno.ToString().Replace(",", ".") + "' WHERE broj_primke='" + txtBroj.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND is_kalkulacija='0'");
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

            if (txtSifra_robe.Text != "" && txtIznos.Text != "")
            {
                return;
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
                " WHERE sifra='" + txtSifra_robe.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "' AND aktivnost='1';";

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
            dgw.Rows[br].Cells["mpcIznos"].Value = ((Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()) * pdv / 100) + Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString())).ToString("#0.00");
            dgw.Rows[br].Cells["upakiranju"].Value = "1,000";
            dgw.Rows[br].Cells["brojpaketa"].Value = "1,000";
            dgw.Rows[br].Cells["nbcIznos"].Value = "1,00";
            dgw.Rows[br].Cells["iznos"].Value = "1,00";
            dgw.Rows[br].Cells["id_stavka"].Value = "";

            decimal povratna_naknada;
            decimal.TryParse(DTRoba.Rows[0]["povratna_naknada"].ToString(), out povratna_naknada);

            dgw.Rows[br].Cells["povratna_naknada"].Value = Math.Round(povratna_naknada, 3).ToString("#0.00");
            txtPovratnaNaknada.Text = Math.Round(povratna_naknada, 3).ToString("#0.00");

            txtKolicina.Text = "1,000";
            txtNabavnaCijena.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.000");
            txtNabavniIznos.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
            txtRabat.Text = "0,000";
            txtIznos.Text = ((Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()) * pdv / 100) + Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString())).ToString("#0.00");
            txtCijenaPoKom.Text = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.000");
            txtBrojPaketa.Text = "1,000";
            txtUpakiranju.Text = DTRoba.Rows[0]["u_pakiranju"].ToString();
            txtPorez.Text = pdv.ToString();
            cbUlazniPorez.SelectedValue = pdv;

            PaintRows(dgw);
            //SetInDgw();
            Izracun();
        }

        private decimal _kolicina = 0;
        private decimal _rabat = 0;
        private decimal _cijena = 0;
        private decimal _pdv = 0;
        private decimal _nabavna = 0;
        private decimal _u_pakiranju = 0;
        private decimal _brojPaketa = 0;

        private decimal dec;

        private void txtUpakiranju_Validated(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtUpakiranju.Text, out dec)) { txtUpakiranju.Text = dec.ToString("#0.000"); } else { MessageBox.Show("Greška kod upisa.", "Greška"); txtUpakiranju.Text = "0,00"; return; }

            txtUpakiranju.Text = Convert.ToDecimal(txtUpakiranju.Text).ToString("#0.000");
            _kolicina = (Convert.ToDecimal(txtUpakiranju.Text) * Convert.ToDecimal(txtBrojPaketa.Text));
            _rabat = Convert.ToDecimal(txtRabat.Text);
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);

            txtKolicina.Text = _kolicina.ToString("#0.000");
            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
            SetInDgw();
            Izracun();
        }

        private void txtBrojPaketa_Validated(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtBrojPaketa.Text, out dec)) { txtBrojPaketa.Text = dec.ToString("#0.000"); } else { MessageBox.Show("Greška kod upisa.", "Greška"); txtBrojPaketa.Text = "0,00"; return; }

            _kolicina = (Convert.ToDecimal(txtUpakiranju.Text) * Convert.ToDecimal(txtBrojPaketa.Text));
            _rabat = Convert.ToDecimal(txtRabat.Text);
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);

            txtKolicina.Text = _kolicina.ToString("#0.000");
            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
            SetInDgw();
            Izracun();
        }

        private void txtKolicina_Validated(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtKolicina.Text, out dec)) { txtKolicina.Text = dec.ToString("#0.000"); } else { MessageBox.Show("Greška kod upisa.", "Greška"); txtKolicina.Text = "0,00"; return; }

            _kolicina = Convert.ToDecimal(txtKolicina.Text);
            _u_pakiranju = Convert.ToDecimal(txtUpakiranju.Text);
            _brojPaketa = Convert.ToDecimal(txtBrojPaketa.Text);
            _rabat = Convert.ToDecimal(txtRabat.Text);
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);

            txtUpakiranju.Text = (_kolicina / _brojPaketa).ToString("#0.000");

            txtKolicina.Text = Convert.ToDecimal(txtKolicina.Text).ToString("#0.000");
            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
            SetInDgw();
            Izracun();
        }

        private void txtCijenaPoKom_Validated(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtCijenaPoKom.Text, out dec)) { txtCijenaPoKom.Text = dec.ToString("#0.0000"); } else { MessageBox.Show("Greška kod upisa.", "Greška"); txtCijenaPoKom.Text = "0,0000"; return; }

            _kolicina = Convert.ToDecimal(txtKolicina.Text);
            _u_pakiranju = Convert.ToDecimal(txtUpakiranju.Text);
            _brojPaketa = Convert.ToDecimal(txtBrojPaketa.Text);
            _rabat = Convert.ToDecimal(txtRabat.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);

            _cijena = Convert.ToDecimal(txtCijenaPoKom.Text);
            txtCijenaPoKom.Text = _cijena.ToString("#0.0000");

            txtNabavnaCijena.Text = (_cijena - (_cijena * _rabat / 100)).ToString("#0.0000");
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);

            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
            SetInDgw();
            Izracun();
        }

        private void txtRabat_Validating(object sender, CancelEventArgs e)
        {
            if (Decimal.TryParse(txtRabat.Text, out dec)) { txtRabat.Text = dec.ToString("#0.0000"); } else { MessageBox.Show("Greška kod upisa.", "Greška"); txtRabat.Text = "0,00"; return; }

            _kolicina = Convert.ToDecimal(txtKolicina.Text);
            _u_pakiranju = Convert.ToDecimal(txtUpakiranju.Text);
            _brojPaketa = Convert.ToDecimal(txtBrojPaketa.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);
            _cijena = Convert.ToDecimal(txtCijenaPoKom.Text);
            _nabavna = _cijena;
            _rabat = Convert.ToDecimal(txtRabat.Text);

            txtNabavnaCijena.Text = (_nabavna - (_nabavna * _rabat / 100)).ToString("#0.0000");
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
            SetInDgw();
            Izracun();
        }

        private void cbUlazniPorez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                txtPorez.Text = cbUlazniPorez.SelectedValue.ToString();

                _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
                _pdv = Convert.ToDecimal(txtPorez.Text);
                _kolicina = Convert.ToDecimal(txtKolicina.Text);

                txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");
                Izracun();
                SetInDgw();
            }
        }

        private void SetInDgw()
        {
            int br = dgw.CurrentRow.Index;

            decimal nabavna, kol, porez;
            decimal.TryParse(txtKolicina.Text, out kol);
            decimal.TryParse(txtNabavnaCijena.Text, out nabavna);
            decimal.TryParse(txtPorez.Text, out porez);

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["naziv"].Value = txtNazivArtikla.Text;
            dgw.Rows[br].Cells["kolicina"].Value = txtKolicina.Text;
            dgw.Rows[br].Cells["cijena"].Value = txtCijenaPoKom.Text;
            dgw.Rows[br].Cells["nbc"].Value = txtNabavnaCijena.Text;
            dgw.Rows[br].Cells["rabat"].Value = txtRabat.Text;
            dgw.Rows[br].Cells["pdv"].Value = txtPorez.Text;
            dgw.Rows[br].Cells["mpcIznos"].Value = Math.Round((kol * nabavna) * (1 + (porez / 100)), 3).ToString("#0.00");
            dgw.Rows[br].Cells["upakiranju"].Value = txtUpakiranju.Text;
            dgw.Rows[br].Cells["brojpaketa"].Value = txtBrojPaketa.Text;
            dgw.Rows[br].Cells["nbcIznos"].Value = txtNabavniIznos.Text;
            dgw.Rows[br].Cells["iznos"].Value = txtIznos.Text;

            decimal povratna_naknada;
            decimal.TryParse(txtPovratnaNaknada.Text, out povratna_naknada);
            dgw.Rows[br].Cells["povratna_naknada"].Value = Math.Round(povratna_naknada, 3).ToString("#0.00");
        }

        private void Izracun()
        {
            decimal p = 0;
            decimal u = 0;
            decimal ss = 0;
            decimal pp = 0;
            decimal mpc = 0;
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                try
                {
                    mpc = Convert.ToDecimal(dgw.Rows[i].Cells["mpcIznos"].FormattedValue.ToString()) + mpc;
                    ss = Convert.ToDecimal(dgw.Rows[i].Cells["nbcIznos"].FormattedValue.ToString());
                    pp = Convert.ToDecimal(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());

                    u = ss + u;

                    p = (ss * pp / 100) + p;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Krivi unos. \r\n" + ex, "Greška");
                }
            }

            iznos_ukupno = u;

            IznosNetto = u;
            IznosUkupno = mpc;

            lblNbcUkupno.Text = "Bez poreza: " + u.ToString("#0.00") + " kn";
            lblMpcUkupno.Text = "Sa porezom: " + mpc.ToString("#0.00") + " kn";
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
                /*
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj primki.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                */

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
                txtIznos.Text = dgw.Rows[br].Cells["mpcIznos"].FormattedValue.ToString();
                txtUpakiranju.Text = dgw.Rows[br].Cells["upakiranju"].FormattedValue.ToString();
                txtBrojPaketa.Text = dgw.Rows[br].Cells["brojpaketa"].FormattedValue.ToString();
                txtNabavniIznos.Text = dgw.Rows[br].Cells["nbcIznos"].FormattedValue.ToString();
                //txtIznos.Text = dgw.Rows[br].Cells["iznos"].FormattedValue.ToString();

                decimal povratna_naknada;
                decimal.TryParse(dgw.Rows[br].Cells["povratna_naknada"].FormattedValue.ToString(), out povratna_naknada);
                txtPovratnaNaknada.Text = Math.Round(povratna_naknada, 3).ToString("#0.00");
            }
        }

        private void txtBroj_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

                DataTable DT = classSQL.select("SELECT broj_primke FROM primka WHERE broj_primke='" + txtBroj.Text + "' AND is_kalkulacija='0' AND id_skladiste='" + cbSkladiste.SelectedValue + "'", "primka").Tables[0];
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
                    if (Until.classZakljucavanjeDokumenta.isLockPrimka(Convert.ToInt32(txtBroj.Text), Convert.ToInt32(cbSkladiste.SelectedValue)))
                    {
                        MessageBox.Show("Primka je zaključana. Uređivanje nije dopušteno.");
                        return;
                    }
                    broj_primke_edit = txtBroj.Text;
                    skladiste_edit = cbSkladiste.SelectedValue.ToString();
                    EnableDisable(false, true, true);
                    edit = true;
                    FillPrimke(broj_primke_edit, cbSkladiste.SelectedValue.ToString());

                    txtBrojUlaznogDok.Select();
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPrimke(string _broj_primke, string _skladiste)
        {
            cbSkladiste.SelectedValue = _skladiste;

            string sql = "SELECT * FROM primka WHERE broj_primke='" + _broj_primke + "' AND id_skladiste='" + _skladiste + "' AND is_kalkulacija='0'";
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
                " primka_stavke.povratna_naknada, " +
                " primka_stavke.iznos, " +
                " primka_stavke.broj_primke, " +
                " primka_stavke.id_skladiste, " +
                " roba_prodaja.naziv " +
                " FROM primka_stavke " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=primka_stavke.sifra AND roba_prodaja.id_skladiste=primka_stavke.id_skladiste " +
                " WHERE primka_stavke.broj_primke='" + _broj_primke + "' AND primka_stavke.id_skladiste='" + _skladiste + "' AND primka_stavke.is_kalkulacija='0'" +
                " ORDER BY primka_stavke.id_stavka ASC";
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
                    dgw.Rows.Add(rb,
                        row["sifra"].ToString(),
                        row["naziv"].ToString(),
                        row["kolicina"].ToString(),
                        row["cijena_po_komadu"].ToString(),
                        row["rabat"].ToString(),
                        row["nabavna_cijena"].ToString(),
                        row["ulazni_porez"].ToString(),
                        row["nabavni_iznos"].ToString(),
                        row["iznos"].ToString(),
                        row["u_pakiranju"].ToString(),
                        row["broj_paketa"].ToString(),
                        row["id_stavka"].ToString(),
                        row["iznos"].ToString(),
                        row["povratna_naknada"].ToString()
                        );
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

            txtCijenaPoKom.Text = Math.Round((((100 * (iznos_nabavni)) / (100 - rabat)) / _kolicina), 4).ToString("#0.0000");
            ;

            txtNabavnaCijena.Text = Math.Round((iznos_nabavni / kol), 4).ToString("#0.0000");

            _kolicina = Convert.ToDecimal(txtKolicina.Text);
            _u_pakiranju = Convert.ToDecimal(txtUpakiranju.Text);
            _brojPaketa = Convert.ToDecimal(txtBrojPaketa.Text);
            _rabat = Convert.ToDecimal(txtRabat.Text);
            _nabavna = Convert.ToDecimal(txtNabavnaCijena.Text);
            _pdv = Convert.ToDecimal(cbUlazniPorez.SelectedValue);

            txtUpakiranju.Text = (_kolicina / _brojPaketa).ToString("#0.000");

            txtKolicina.Text = Convert.ToDecimal(txtKolicina.Text).ToString("#0.000");
            txtNabavniIznos.Text = (_kolicina * _nabavna).ToString("#0.00");
            txtIznos.Text = (((_nabavna * _pdv / 100) + _nabavna) * _kolicina).ToString("#0.00");

            SetInDgw();

            Izracun();
        }

        private void frmPrimka_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
        }

        private void txtIznosBezPoreza_Validated(object sender, EventArgs e)
        {
            decimal d;
            decimal.TryParse(txtIznosBezPoreza.Text, out d);
            txtIznosBezPoreza.Text = d.ToString("#0.00");
        }

        private void txtIznosSaPorezom_Validated(object sender, EventArgs e)
        {
            decimal d;
            decimal.TryParse(txtIznosSaPorezom.Text, out d);
            txtIznosSaPorezom.Text = d.ToString("#0.00");
        }

        private void btnUvozRoto_Click(object sender, EventArgs e)
        {
            SinkronizacijaDobavljac.frmRoto r = new SinkronizacijaDobavljac.frmRoto();
            r.ShowDialog();
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