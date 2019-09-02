using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPonude : Form
    {
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTpromocije1;
        private DataTable DTOtprema = new DataTable();
        private DataTable DSponude = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTpostavke = new DataTable();
        public frmMenu MainForm { get; set; }
        public string broj_ponude_edit { get; set; }
        private double u = 0;
        private bool edit = false;

        public frmPonude()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPonude_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            PaintRows(dgw);
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            numeric();
            fillComboBox();
            ttxBrojPonude.Text = brojPonude();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_ponude_edit != null) { fillPonude(); }
            ttxBrojPonude.Select();
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
            {
                this.Dock = DockStyle.None;
            }

            base.WndProc(ref m);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmPonude MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[3];
                d.BeginEdit(true);
            }
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

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private string brojPonude()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_ponude AS bigint)) FROM ponude", "ponude").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void fillComboBox()
        {
            //fill ziroracun
            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            cbZiroRacun.DisplayMember = "ziroracun";
            cbZiroRacun.ValueMember = "id_ziroracun";
            cbZiroRacun.SelectedValue = "1";

            //fill otprema
            DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            cbOtprema.DataSource = DTOtprema;
            cbOtprema.DisplayMember = "naziv";
            cbOtprema.ValueMember = "id_otprema";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            cbKomercijalist.DisplayMember = "IME";
            cbKomercijalist.ValueMember = "id_zaposlenik";

            //fill izjava
            DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            cbIzjava.DataSource = DSIzjava.Tables[0];
            cbIzjava.DisplayMember = "izjava";
            cbIzjava.ValueMember = "id_izjava";

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'pon' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill nacin_placanja
            DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            cbNacinPlacanja.DisplayMember = "naziv_placanja";
            cbNacinPlacanja.ValueMember = "id_placanje";
            cbNacinPlacanja.SelectedValue = 3;
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
        }

        private void numeric()
        {
            nmGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaPonude.Value = Util.Korisno.GodinaKojaSeKoristiUbazi;
        }

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            txtSifraFakturirati.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtPartnerNaziv1.Enabled = x;
            txtSifraNacinPlacanja.Enabled = x;
            txtModel.Enabled = x;
            txtSifraNarKupca.Enabled = x;
            txtNarKupca1.Enabled = x;
            cbOtprema.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            btnPartner1.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbIzjava.Enabled = x;
            cbKomercijalist.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbZiroRacun.Enabled = x;
            cbValuta.Enabled = x;
            cbNarKupca.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnNarKupca.Enabled = x;

            if (x == true)
            {
                ttxBrojPonude.ReadOnly = true;
                nmGodinaPonude.ReadOnly = true;
            }
            else
            {
                ttxBrojPonude.ReadOnly = false;
                nmGodinaPonude.ReadOnly = false;
            }
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtSifraFakturirati.Text = "";
            txtPartnerNaziv.Text = "";
            txtPartnerNaziv1.Text = "";
            txtSifraNacinPlacanja.Text = "";
            txtModel.Text = "";
            txtSifraNarKupca.Text = "";
            txtNarKupca1.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";

            dgw.Rows.Clear();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ((char)('+') == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ((char)('-') == (e.KeyChar))
                    return;
            }

            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region ON_KEY_DOWN_REGION

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            cbVD.Select();
                            txtSifraFakturirati.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
                        }
                    }
                    else
                    {
                        txtSifraOdrediste.Select();
                        return;
                    }
                }

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtSifraFakturirati.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraFakturirati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraFakturirati.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraFakturirati.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as ime_tvrtke FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv1.Text = DSpar.Rows[0][0].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
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
                txtDana.Select();
            }
        }

        private void txtDana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0";
                    dtpDanaValuta.Select();
                }

                try
                {
                    DateTime dvo = dtpDanaValuta.Value;
                    dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text));
                    ;
                    dtpDanaValuta.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void dtpDanaValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DateTime dt1 = dtpDanaValuta.Value;
                    DateTime dt2 = dtpDanaValuta.Value;
                    TimeSpan ts = dt1 - dt2;
                    txtDana.Text = (Convert.ToInt16(ts.Days.ToString()) + 1).ToString();
                    cbIzjava.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Text += cbIzjava.Text;
                cbKomercijalist.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtSifraNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    DataRow[] dataROW = DSnazivPlacanja.Tables[0].Select("id_placanje = " + txtSifraNacinPlacanja.Text);
                    cbNacinPlacanja.SelectedValue = dataROW[0]["id_placanje"].ToString();
                    cbNacinPlacanja.Select();
                }
                catch (Exception)
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                }
            }
        }

        private void cbNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbZiroRacun.Select();
            }
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private void cbZiroRacun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbValuta.Select();
            }
        }

        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNarKupca.Select();
            }
        }

        private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNarKupca.Select();
            }
        }

        private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbNarKupca.Select();
            }
        }

        private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNarKupca1.Select();
            }
        }

        private void txtNarKupca1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbOtprema.Select();
            }
        }

        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
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
                txtSifra_robe.Select();
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                //if (txtSifra_robe.Text.Length > 2)
                //{
                //    for (int y = 0; y < dgw.Rows.Count; y++)
                //    {
                //        if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                //        {
                //            MessageBox.Show("Artikl ili usluga već postoje u ovoj ponudi.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }
                //    }

                //    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
                //    {
                //        double uk;
                //        double popust;
                //        DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

                //        if (DTrp.Rows.Count == 0)
                //        {
                //            MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }

                //        if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
                //        {
                //            MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }

                //        DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                //        if (dateFromPopust < DateTime.Now)
                //        {
                //            MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }

                //        uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                //        popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                //        uk = uk * 3 / 100;

                //        if ((Convert.ToDouble(u.ToString()) - uk) < 0)
                //        {
                //            MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }

                //        dgw.Rows.Add(
                //                    dgw.RowCount - 1,
                //                    txtSifra_robe.Text,
                //                    "Popust sa prethodnog računa",
                //                    "1",
                //                    "kn",
                //                    1,
                //                    DTpostavke.Rows[0]["pdv"].ToString(),
                //                    String.Format("{0:0.00}", (uk * (-1))),
                //                    "0",
                //                    "0",
                //                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                //                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                //                    String.Format("{0:0.00}", (uk * (-1))),
                //                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                //                    String.Format("{0:0.00}", (uk * (-1))),
                //                    "",
                //                    "",
                //                    ""
                //                );

                //        int br = dgw.Rows.Count - 1;
                //        dgw.ClearSelection();
                //        izracun();
                //        PaintRows(dgw);
                //        dgw.ClearSelection();
                //        txtSifra_robe.Text = "";
                //        txtSifra_robe.Select();
                //        return;

                //    }

                //}

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    //ttxBrojPonude.Enabled = false;
                    //nmGodinaPonude.Enabled = false;
                    dgw.Rows[dgw.Rows.Count - 1].Cells["skladiste"].Selected = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion ON_KEY_DOWN_REGION

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            deleteFields();
            btnSveFakture.Enabled = false;
            ttxBrojPonude.Text = brojPonude();
            btnDeleteAllFaktura.Enabled = false;
            ttxBrojPonude.ReadOnly = true;
            nmGodinaPonude.ReadOnly = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtSifraOdrediste.Select();
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            decimal mpc = 0, vpc = 0, porez = 0, porez_potrosnja = 0;
            Decimal.TryParse(DTRoba.Rows[0]["mpc"].ToString(), out mpc);
            Decimal.TryParse(DTRoba.Rows[0]["porez_potrosnja"].ToString(), out porez_potrosnja);
            Decimal.TryParse(DTRoba.Rows[0]["porez"].ToString(), out porez);
            Decimal.TryParse(DTRoba.Rows[0]["mpc"].ToString(), out mpc);

            vpc = mpc / (1 + ((porez + porez_potrosnja) / 100));

            dgw.Rows[br].Cells[0].Value = "1";
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", vpc);
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", vpc);
            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["nbc"]);
            //dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();
            dgw.Rows[br].Cells["porez_potrosnja"].Value = DTRoba.Rows[0]["porez_potrosnja"].ToString();

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];
            dgw.BeginEdit(true);

            izracun();
            PaintRows(dgw);
        }

        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        //{
        //    int row = dgw.CurrentCell.RowIndex;
        //    if (dgw.CurrentCell.ColumnIndex == 3)
        //    {
        //        SetCijenaSkladiste();
        //    }

        //    else if (dgw.CurrentCell.ColumnIndex == 9)
        //    {
        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null)
        //        {
        //            MessageBox.Show("Niste odabrali skladište", "Greška");
        //            return;
        //        }

        //        dgw.CurrentCell.Selected = false;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.BackColor = Color.Silver;
        //        txtSifra_robe.Select();
        //    }
        //    izracun();
        //}

        private void SetCijenaSkladiste()
        {
            if (dgw.CurrentRow.Cells["skladiste"].Value != null)
            {
                DataSet dsRobaProdaja = new DataSet();
                dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
                if (dsRobaProdaja.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
                    {
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        dgw.CurrentRow.Cells["vpc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        lblNaDan.ForeColor = Color.Green;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        dgw.CurrentRow.Cells["vpc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        lblNaDan.ForeColor = Color.Red;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                }
                else
                {
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private double SveUkupno = 0;

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow.Index;

                decimal dec_parse;
                if (!Decimal.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    return;
                }

                if (!Decimal.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat"].Value = "0";
                    MessageBox.Show("Greška kod upisa rabata.", "Greška");
                    return;
                }

                if (!Decimal.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška");
                    return;
                }

                //if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }

                //    if (isNumeric(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat"].Value = "0"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }
                //    if (isNumeric(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }

                double mpc = 0, vpc = 0;

                double kol = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
                //double vpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());
                double porez = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString());
                double rbt = Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString());
                double porez_na_potrosnju = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez_potrosnja"].FormattedValue.ToString());

                try
                {
                    mpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString());
                    vpc = mpc / (1 + ((porez + porez_na_potrosnju) / 100));
                }
                catch
                {
                    vpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());
                    mpc = (vpc * (porez + porez_na_potrosnju) / 100) + vpc;
                }

                double porez_ukupno = vpc * (porez + porez_na_potrosnju) / 100;
                //double mpc = (vpc * (porez + porez_na_potrosnju) / 100) + vpc;
                double mpc_sa_kolicinom = mpc * kol;
                double rabat = mpc * rbt / 100;

                dgw.Rows[rowBR].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
                //dgw.Rows[rowBR].Cells["rabat_iznos"].Value = String.Format("{0:0.00}",(rabat * kol));
                dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = String.Format("{0:0.00}", (((mpc - rabat) * kol) / Convert.ToDouble("1," + porez)));
                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = String.Format("{0:0.00}", ((mpc - rabat) * kol));
                dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", ((mpc - rabat) / Convert.ToDouble("1," + porez)));

                double pdv = 0;
                double B_pdv = 0;
                u = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    u = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString()) + u;
                }

                //B_pdv = u / Convert.ToDouble("1," + dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString());
                B_pdv = u / (1 + (Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()) / 100));

                SveUkupno = u;
                textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", u);
                textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", B_pdv);
                textBox3.Text = "PDV: " + String.Format("{0:0.00}", u - B_pdv);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojPonude.Text = brojPonude();
            edit = false;
            btnDeleteAllFaktura.Enabled = false;
            ttxBrojPonude.ReadOnly = false;
            nmGodinaPonude.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private string ReturnSifra(string sifra)
        {
            if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
            {
                return "00000";
            }

            return sifra;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
                return;
            }

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Upozorenje.\r\nOva ponuda je prazna.", "Greška");
                return;
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Ponuda nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (edit == true)
            {
                UpdatePonude();
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string broj = brojPonude();
            if (broj.Trim() != ttxBrojPonude.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                ttxBrojPonude.Text = broj;
            }

            if (txtSifraOdrediste.Text == "" || txtSifraFakturirati.Text == "")
            {
                MessageBox.Show("Niste upisali šifru odredišta ili sifru za koga fakturirati.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtSifraNarKupca.Text == "")
            {
                txtSifraNarKupca.Text = "0";
            }

            string sql = "INSERT INTO ponude (broj_ponude,id_odrediste,id_fakturirati,date,vrijedi_do,id_izjava,id_zaposlenik_komercijala," +
                "id_zaposlenik_izradio,model,id_nacin_placanja,zr,id_valuta,otprema,godina_ponude,id_nar_kupca,id_vd,napomena,ukupno) VALUES " +
                " (" +
                 " '" + ttxBrojPonude.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + cbIzjava.SelectedValue + "'," +
                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + nmGodinaPonude.Value.ToString() + "'," +
                " '" + txtSifraNarKupca.Text + "'," +
                " '" + cbVD.SelectedValue + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + Convert.ToDouble(u.ToString()) + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sql_stavke = "";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string oduzmi = dg(i, "oduzmi");
                if (oduzmi == "DA")
                {
                    ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value, i);
                }
                string vpc = dg(i, "vpc");
                if (classSQL.remoteConnectionString == "")
                {
                    vpc = vpc.Replace(",", ".");
                }
                else
                {
                    vpc = vpc.Replace(".", ",");
                }

                sql_stavke = "INSERT INTO ponude_stavke " +
                "(sifra,kolicina,vpc,porez,rabat,id_skladiste,naziv,oduzmi,porez_potrosnja,broj_ponude, mpc)" +
                "VALUES" +
                "(" +
                "'" + ReturnSifra(dg(i, "sifra")) + "'," +
                "'" + dg(i, "kolicina") + "'," +
                "'" + vpc.Replace(",", ".") + "'," +
                "'" + dg(i, "porez") + "'," +
                "'" + dg(i, "rabat") + "'," +
                "'" + dgw.Rows[i].Cells[3].Value + "'," +
                 "'" + dg(i, "naziv") + "'," +
                 "'" + dg(i, "oduzmi") + "'," +
                 "'" + dg(i, "porez_potrosnja") + "'," +
                "'" + ttxBrojPonude.Text + "'," +
                 "'" + dg(i, "mpc").Replace(',', '.') + "'" +
                ")";
                provjera_sql(classSQL.insert(sql_stavke));
            }

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada nove ponude br." + ttxBrojPonude.Text + "')");

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(ttxBrojPonude.Text);
            }

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void printaj(string broj)
        {
            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.dokumenat = "PON";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Ponuda";
            pr.ShowDialog();
        }

        private DataSet DSprovjeraRobaProdaja = new DataSet();

        private void ProvjeriDaliPostojiRobaProdaja(string sif, object skl, int r)
        {
            DSprovjeraRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sif + "' AND id_skladiste='" + skl.ToString() + "'", "roba_prodaja");

            if (DSprovjeraRobaProdaja.Tables[0].Rows.Count == 0)
            {
                string sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES" +
                    "('" + skl.ToString() + "','0','" + dg(r, "nc").Replace(",", ".") + "','" + dg(r, "vpc").Replace(",", ".") + "','" + dg(r, "porez") + "','" + sif + "')";
                classSQL.insert(sql);
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void UpdatePonude()
        {
            if (txtSifraNarKupca.Text == "")
            {
                txtSifraNarKupca.Text = "0";
            }

            string sql = "UPDATE ponude SET " +
                " broj_ponude='" + ttxBrojPonude.Text + "'," +
                " id_odrediste='" + txtSifraOdrediste.Text + "'," +
                " id_fakturirati='" + txtSifraFakturirati.Text + "'," +
                " date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " vrijedi_do='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izjava='" + cbIzjava.SelectedValue.ToString() + "'," +
                " id_zaposlenik_komercijala='" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " zr='" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " id_valuta='" + cbValuta.SelectedValue.ToString() + "'," +
                " otprema='" + cbOtprema.SelectedValue.ToString() + "'," +
                " model='" + txtModel.Text + "'," +
                " id_nar_kupca='" + txtSifraNarKupca.Text + "'," +
                " id_vd='" + cbVD.SelectedValue + "'," +
                " godina_ponude='" + nmGodinaPonude.Value.ToString() + "'," +
                " ukupno='" + Convert.ToDouble(u.ToString()) + "'," +
                " napomena='" + rtbNapomena.Text + "'" +
                " WHERE broj_ponude='" + ttxBrojPonude.Text + "'";
            provjera_sql(classSQL.update(sql));

            for (int b = 0; b < dgw.Rows.Count; b++)
            {
                string vpc = dg(b, "vpc");
                if (classSQL.remoteConnectionString == "")
                {
                    vpc = vpc.Replace(",", ".");
                }
                else
                {
                    vpc = vpc.Replace(".", ",");
                }

                if (dgw.Rows[b].Cells["id_stavka"].Value != null)
                {
                    sql = "UPDATE ponude_stavke SET " +
                    " id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value + "'," +
                    " kolicina='" + dg(b, "kolicina") + "'," +
                    " vpc='" + vpc.Replace(",", ".") + "'," +
                    " porez='" + dg(b, "porez") + "'," +
                    " naziv='" + dg(b, "naziv") + "'," +
                    " sifra='" + ReturnSifra(dg(b, "sifra")) + "'," +
                    " rabat='" + dg(b, "rabat") + "'," +
                    " porez_potrosnja='" + dg(b, "porez_potrosnja") + "'," +
                    " mpc = '" + dg(b, "mpc").Replace(',', '.') + "'" +
                    " WHERE id_stavka='" + dg(b, "id_stavka") + "'";
                    provjera_sql(classSQL.update(sql));
                }
                else
                {
                    string sql_stavke = "INSERT INTO ponude_stavke (" +
                    "id_skladiste,sifra,rabat,broj_ponude,vpc,naziv,kolicina,oduzmi,porez,porez_potrosnja, mpc)" +
                    " VALUES (" +
                    "'" + dgw.Rows[b].Cells["skladiste"].Value + "'," +
                    "'" + ReturnSifra(dg(b, "sifra")) + "'," +
                    "'" + dg(b, "rabat") + "'," +
                    "'" + ttxBrojPonude.Text + "'," +
                    "'" + vpc.Replace(",", ".") + "'," +
                    "'" + dg(b, "naziv") + "'," +
                    "'" + dg(b, "kolicina") + "'," +
                    "'" + dg(b, "oduzmi") + "'," +
                    "'" + dg(b, "porez") + "'," +
                    "'" + dg(b, "porez_potrosnja") + "'," +
                    "'" + dg(b, "mpc").Replace(',', '.') + "'" +
                    ")";
                    provjera_sql(classSQL.insert(sql_stavke));
                }
            }

            MessageBox.Show("Spremljeno");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSveFakture.Enabled = true;

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "', '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje ponude br." + ttxBrojPonude.Text + "')");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.roba = true;
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    ttxBrojPonude.Enabled = false;
                    nmGodinaPonude.Enabled = false;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnPartner1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSavPotrosniMaterijal objForm3 = new frmSavPotrosniMaterijal();
            objForm3.sifra_ponude = "";
            objForm3.MainForm = this;
            broj_ponude_edit = null;
            objForm3.ShowDialog();
            if (broj_ponude_edit != null)
            {
                fillPonude();
                EnableDisable(true);
                edit = true;
                ttxBrojPonude.ReadOnly = true;
                nmGodinaPonude.ReadOnly = true;
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

        private void fillPonude()
        {
            //fill header

            EnableDisable(true);
            edit = true;

            DSponude = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj_ponude_edit + "'", "fakture").Tables[0];

            cbVD.SelectedValue = DSponude.Rows[0]["id_vd"].ToString();
            txtSifraOdrediste.Text = DSponude.Rows[0]["id_odrediste"].ToString();
            txtSifraFakturirati.Text = DSponude.Rows[0]["id_fakturirati"].ToString();
            txtPartnerNaziv.Text = classSQL.select("SELECT case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtPartnerNaziv1.Text = classSQL.select("SELECT case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtSifraNacinPlacanja.Text = DSponude.Rows[0]["id_nacin_placanja"].ToString();
            txtModel.Text = DSponude.Rows[0]["model"].ToString();
            txtSifraNarKupca.Text = DSponude.Rows[0]["id_nar_kupca"].ToString();
            txtNarKupca1.Text = DSponude.Rows[0]["id_nar_kupca"].ToString();
            cbOtprema.SelectedValue = DSponude.Rows[0]["otprema"].ToString();
            rtbNapomena.Text = DSponude.Rows[0]["napomena"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DSponude.Rows[0]["date"].ToString());
            dtpDanaValuta.Value = Convert.ToDateTime(DSponude.Rows[0]["vrijedi_do"].ToString());
            cbKomercijalist.SelectedValue = DSponude.Rows[0]["id_zaposlenik_komercijala"].ToString();
            cbNacinPlacanja.SelectedValue = DSponude.Rows[0]["id_nacin_placanja"].ToString();
            cbZiroRacun.SelectedValue = DSponude.Rows[0]["zr"].ToString();
            cbValuta.SelectedValue = DSponude.Rows[0]["id_valuta"].ToString();
            cbNarKupca.SelectedValue = DSponude.Rows[0]["id_nar_kupca"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSponude.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojPonude.Text = DSponude.Rows[0]["broj_ponude"].ToString();

            //--------fill faktura stavke------------------------------

            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DSponude.Rows[0]["broj_ponude"].ToString() + "'", "broj_ponude").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s;
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc as nbc,roba.naziv,roba.jm,roba_prodaja.sifra,roba_prodaja.id_skladiste,roba.oduzmi FROM roba_prodaja LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT r.* FROM roba r WHERE r.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "';";
                }

                //s = "SELECT ps.*, r.naziv, r.jm,  FROM ponude_stavke ps LEFT JOIN roba r ON ps.sifra = r.sifra WHERE ps.broj_ponude = '" + broj_ponude_edit + "' AND ps.sifra = '" + DSFS.Rows[i]["sifra"].ToString() + "'";
                dtR = classSQL.select(s, "ponude_stavke").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                try { dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString(); } catch (Exception) { }
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"].ToString());
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nbc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();
                //dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                izracun();
                ControlDisableEnable(0, 1, 1, 0, 1);
                PaintRows(dgw);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }

            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    classSQL.delete("DELETE FROM ponude_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + 2 + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa ponude br." + ttxBrojPonude.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM ponude_stavke WHERE broj_ponude='" + ttxBrojPonude.Text + "'");
                classSQL.delete("DELETE FROM ponude WHERE broj_ponude='" + ttxBrojPonude.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele ponude br." + ttxBrojPonude.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAllFaktura.Enabled = false;
                btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nmGodinaPonude.Value.ToString() + "' AND broj_ponude='" + ttxBrojPonude.Text + "'", "fakture").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPonude() == ttxBrojPonude.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        ttxBrojPonude.Text = brojPonude();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojPonude.ReadOnly = true;
                        nmGodinaPonude.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_ponude_edit = ttxBrojPonude.Text;
                    fillPonude();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojPonude.ReadOnly = true;
                    nmGodinaPonude.ReadOnly = true;
                }
                txtSifraOdrediste.Select();
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
            {
                SetCijenaSkladiste();
            }
            else if (dgw.CurrentCell.ColumnIndex == 9)
            {
                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Niste odabrali skladište", "Greška");
                    return;
                }

                dgw.CurrentCell.Selected = false;
                txtSifra_robe.Text = "";
                txtSifra_robe.BackColor = Color.Silver;
                txtSifra_robe.Select();
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    dgw.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString()) / Convert.ToDouble("1," + (Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) + Convert.ToDouble(dgw.CurrentRow.Cells["porez_potrosnja"].FormattedValue.ToString())));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu." + ex.ToString());
                }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void frmPonude_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainForm != null)
            {
            }
        }

        private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    txtSifra_robe.Select();
                }
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