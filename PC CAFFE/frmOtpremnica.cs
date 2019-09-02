using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmOtpremnica : Form
    {
        public frmOtpremnica()
        {
            InitializeComponent();
        }

        public string broj_otpremnice_edit { get; set; }
        public string skladiste_edit { get; set; }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTotpremnice = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private double u = 0;
        private bool edit = false;
        private string SveUkupno = "0";
        private bool load = false;
        public frmMenu MainForm { get; set; }
        private Class.Otpremnica _otpremnica;
        private Class.IzracunRrezultat _izracun_rezultat;

        private void frmOtpremnica_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            _otpremnica = new Class.Otpremnica();
            PaintRows(dgw);
            fillComboBox();
            txtBrojOtpremnice.Select();
            txtBrojOtpremnice.Text = brojOtpremnice();
            numeric();
            EnableDisable(false);
            txtBrojOtpremnice.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_otpremnice_edit != null) { fillOtpremnice(skladiste_edit); }
            this.Paint += new PaintEventHandler(Form1_Paint);
            load = true;
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmOtpremnica MainForm { get; set; }

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
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[4];
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

        private void numeric()
        {
            nmGodinaOtpremnice.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaOtpremnice.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaOtpremnice.Value = 2012;

            //nuGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            //nuGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            //nuGodinaPonude.Value = 2012;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojOtpremnice()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_otpremnice) FROM otpremnice  WHERE id_skladiste='" + cbSkladiste.SelectedValue + "'", "otpremnice").Tables[0];
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
            //fill otprema
            //DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            //cbOtprema.DataSource = DTOtprema;
            //cbOtprema.DisplayMember = "naziv";
            //cbOtprema.ValueMember = "id_otprema";

            //fill skladiste
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1') ORDER BY skladiste", "skladiste");
            cbSkladiste.DataSource = DS_Skladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            cbKomercijalist.DisplayMember = "IME";
            cbKomercijalist.ValueMember = "id_zaposlenik";

            //fill izjava
            //DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            //cbIzjava.DataSource = DSIzjava.Tables[0];
            //cbIzjava.DisplayMember = "izjava";
            //cbIzjava.ValueMember = "id_izjava";

            //fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            //cbVD.DataSource = DSvd.Tables[0];
            //cbVD.DisplayMember = "vd";
            //cbVD.ValueMember = "id_vd";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void txtBrojOtpremnice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                nmGodinaOtpremnice.Select();
            }
        }

        private void nmGodinaOtpremnice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbSkladiste.Select();
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraOdrediste.Select();
            }
        }

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                txtSifraPosPatner.Text = txtSifraOdrediste.Text;

                e.SuppressKeyPress = true;
                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    txtSifraPosPatner.Select();
                    txtSifraPosPatner.Text = txtSifraOdrediste.Text;
                    txtSifraPosPatner.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    txtSifraOdrediste.Select();
                }
            }
        }

        private void txtPartnerNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraPosPatner.Select();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Str = txtSifraPosPatner.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPosPatner.Text = "0";
                }

                e.SuppressKeyPress = true;
                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPosPatner.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPosPartner.Text = DSpar.Rows[0][0].ToString();
                    //cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //cbVD.Select();
            //}
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    dtpDatum.Select();
            //}
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbKomercijalist.Select();
            }
        }

        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    //rtbNapomena.Text = cbIzjava.Text;
            //    e.SuppressKeyPress = true;
            //    rtbNapomena.Select();
            //}
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtIzradio.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void cbOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtMjOtpreme.Select();
            //}
        }

        private void txtMjOtpreme_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtAdresaOtp.Select();
            //}
        }

        private void txtAdresaOtp_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtIsprave1.Select();
            //}
        }

        private void txtIsprave1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtSifraPrijevoznik.Select();
            //}
        }

        private void txtBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtSifraPrijevoznik.Select();
            //}
        }

        private void nuGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtSifraPrijevoznik.Select();
            //}
        }

        private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtSifraPrijevoznik.Select();
            //}
        }

        private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtSifraPrijevoznik.Select();
            //}
        }

        private void txtSifraPrijevoznik_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;

            //    //if (txtSifraPrijevoznik.Text == "") {
            //    //    txtSifraPrijevoznik.Text = "0";
            //    //    txtNazivPrijevoznik.Select();
            //    //    return;
            //    //}

            //    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPrijevoznik.Text + "'", "partners").Tables[0];
            //    if (DSpar.Rows.Count > 0) {
            //        txtNazivPrijevoznik.Text = DSpar.Rows[0][0].ToString();
            //        txtReg.Select();
            //    } else {
            //        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
            //    }

            //}
        }

        private void txtNazivPrijevoznik_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    txtReg.Select();
            //}
        }

        private void txtReg_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtIstovarnoMJ.Select();
            //}
        }

        private void txtIstovarnoMJ_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //dtpIstovarniRok.Select();
            //}
        }

        private void dtpIstovarniRok_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    //txtTroskovi.Select();
            //}
        }

        private void txtTroskovi_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    e.SuppressKeyPress = true;
            //    txtSifra_robe.Select();
            //}
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

            //if ((char)(',') == (e.KeyChar))
            //{
            //    e.Handled = false; return;
            //}

            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraPosPatner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPosPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraPosPatner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPosPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            //partnerTrazi.ShowDialog();
            //if (Properties.Settings.Default.id_partner != "") {
            //    DataSet partner = new DataSet();
            //    partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
            //    if (partner.Tables[0].Rows.Count > 0) {
            //        //txtSifraPrijevoznik.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
            //        //txtNazivPrijevoznik.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
            //    } else {
            //        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
            //    }
            //}
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    txtBrojOtpremnice.Enabled = false;
                    //nuGodinaPonude.Enabled = false;
                    //txtSifra_robe.Text = DTRoba.Rows[0]["sifra"].ToString();
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            string kol;
            string nc;
            string vpc;
            string porez;
            string mpc;
            DataTable DTRP = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + DTRoba.Rows[0]["sifra"].ToString() + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'", "roba_prodaja").Tables[0];

            //provjerava dali postoji artikl u roba_prodaja ili dali se radi o atriklu koji se ne skida sa skladišta
            if (DTRP.Rows.Count == 0 || DTRoba.Rows[0]["oduzmi"].ToString() == "DA")
            {
                kol = "0";
                nc = DTRoba.Rows[0]["nc"].ToString();
                vpc = DTRoba.Rows[0]["vpc"].ToString();
                porez = DTRoba.Rows[0]["porez"].ToString();
                mpc = DTRoba.Rows[0]["mpc"].ToString();
            }
            else
            {
                kol = DTRP.Rows[0]["kolicina"].ToString();
                nc = DTRP.Rows[0]["nc"].ToString();
                vpc = DTRP.Rows[0]["vpc"].ToString();
                porez = DTRP.Rows[0]["porez"].ToString();
                mpc = ((Convert.ToDouble(vpc) * Convert.ToDouble(porez) / 100) + Convert.ToDouble(vpc)).ToString();
            }

            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Rows[br].Cells[0].Value = br + 1;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", vpc);
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["porez"].Value = porez;
            dgw.Rows[br].Cells["porez_potrosnja"].Value = DTRoba.Rows[0]["porez_potrosnja"].ToString();
            ;
            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", vpc);
            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", nc);

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];
            dgw.BeginEdit(true);

            SetCijenaSkladiste();
            _izracun_rezultat = _otpremnica.izracun(dgw);
            textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoMpc);
            textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoVpc);
            textBox3.Text = "PDV: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnpPdv);
            PaintRows(dgw);
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        //private void izracun () {
        //    if (dgw.RowCount > 0) {
        //        int rowBR = dgw.CurrentRow.Index;

        //        if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }
        //        if (isNumeric(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat"].Value = "0"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }
        //        if (isNumeric(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }

        //        double kol = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
        //        double vpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());
        //        double porez = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString());
        //        double rbt = Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString());

        //        double porez_ukupno = vpc * porez / 100;
        //        double mpc = porez_ukupno + vpc;
        //        double mpc_sa_kolicinom = mpc * kol;
        //        double rabat = mpc * rbt / 100;

        //        //dgw.Rows[rowBR].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
        //        dgw.Rows[rowBR].Cells["rabat_iznos"].Value = String.Format("{0:0.00}", (rabat * kol));
        //        dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = String.Format("{0:0.00}", (((mpc - rabat) * kol) / Convert.ToDouble("1," + porez)));
        //        dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = String.Format("{0:0.00}", ((mpc - rabat) * kol));
        //        dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", ((mpc - rabat) / Convert.ToDouble("1," + porez)));

        //        double pdv = 0;
        //        double B_pdv = 0;
        //        u = 0;

        //        for (int i = 0; i < dgw.RowCount; i++) {
        //            u = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString()) + u;
        //            B_pdv = (Convert.ToDouble(dgw.Rows[i].Cells["cijena_bez_pdva"].FormattedValue.ToString()) * kol) + B_pdv;
        //            pdv = u - B_pdv;
        //        }

        //        SveUkupno = u.ToString();
        //        textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", u);
        //        textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", B_pdv);
        //        textBox3.Text = "PDV: " + String.Format("{0:0.00}", pdv);
        //    }
        //}

        private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //int row = dgw.CurrentCell.RowIndex;
            //if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "da")
            //{
            //    SetCijenaSkladiste();
            //}

            //else if (dgw.CurrentCell.ColumnIndex == 10)
            //{
            //    dgw.CurrentCell.Selected = false;
            //    txtSifra_robe.Text = "";
            //    //txtSifra_robe.BackColor = Color.Silver;
            //    txtSifra_robe.Select();
            //}
            //izracun();
        }

        private void SetCijenaSkladiste()
        {
            DataSet dsRobaProdaja = new DataSet();
            dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + cbSkladiste.SelectedValue + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
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

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    txtBrojOtpremnice.Enabled = false;
                    //nuGodinaPonude.Enabled = false;
                    dgw.Rows[0].Cells["kolicina"].Value = "1";
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printaj(string broj, string skl)
        {
            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.dokumenat = "OTP";
            pr.broj_dokumenta = broj;
            pr.from_skladiste = skl;
            pr.ImeForme = "Otpremnica";
            pr.ShowDialog();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

            if (txtSifraOdrediste.Text == "")
            {
                MessageBox.Show("Niste odabrali odredište.", "Greška");
                return;
            }

            if (txtSifraPosPatner.Text == "")
            {
                MessageBox.Show("Niste odabrali poslovnog partnera.", "Greška");
                return;
            }

            if (edit == true)
            {
                UpdateOtpremnica();
                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printaj(txtBrojOtpremnice.Text, cbSkladiste.SelectedValue.ToString());
                }
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string broj_otpremnice = txtBrojOtpremnice.Text;

            if (_otpremnica.otpremnicaSpremi(dgw, ref broj_otpremnice, cbSkladiste.SelectedValue.ToString(), dtpDatum.Value, Properties.Settings.Default.id_zaposlenik, txtSifraOdrediste.Text, txtSifraPosPatner.Text, rbOsoba.Checked, rtbNapomena.Text, nmGodinaOtpremnice.Value.ToString(), cbKomercijalist.SelectedValue.ToString()))
            {
                txtBrojOtpremnice.Text = broj_otpremnice;

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                txtBrojOtpremnice.ReadOnly = false;
                nmGodinaOtpremnice.ReadOnly = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void EnableDisable(bool x)
        {
            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            dtpDatum.Enabled = x;
            cbKomercijalist.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            txtSifraPosPatner.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            txtSifraPosPatner.Text = "";

            dgw.Rows.Clear();
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            btnSveFakture.Enabled = false;
            txtBrojOtpremnice.Text = brojOtpremnice();
            btnDeleteAllFaktura.Enabled = false;
            txtBrojOtpremnice.ReadOnly = true;
            nmGodinaOtpremnice.ReadOnly = true;
            txtSifraOdrediste.Select();
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            txtBrojOtpremnice.Text = brojOtpremnice();
            edit = false;
            txtBrojOtpremnice.ReadOnly = false;
            nmGodinaOtpremnice.ReadOnly = false;
            txtBrojOtpremnice.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveOtpremnice objForm2 = new frmSveOtpremnice();
            objForm2.sifra_otpremnice = "";
            objForm2.MainForm = this;
            broj_otpremnice_edit = null;
            objForm2.ShowDialog();
            if (broj_otpremnice_edit != null)
            {
                fillOtpremnice(skladiste_edit);
            }
        }

        private void fillOtpremnice(string skl)
        {
            EnableDisable(true);
            edit = true;

            //fill header
            DTotpremnice = classSQL.select("SELECT * FROM otpremnice WHERE broj_otpremnice = '" + broj_otpremnice_edit + "' AND id_skladiste='" + skl + "'", "otpremnice").Tables[0];

            txtSifraPosPatner.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
            try { txtNazivPosPartner.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            //cbVD.SelectedValue = DTotpremnice.Rows[0]["vrsta_dok"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DTotpremnice.Rows[0]["datum"].ToString());
            //cbIzjava.SelectedValue = DTotpremnice.Rows[0]["id_izjava"].ToString();
            rtbNapomena.Text = DTotpremnice.Rows[0]["napomena"].ToString();
            cbKomercijalist.SelectedValue = DTotpremnice.Rows[0]["id_kom"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTotpremnice.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            //cbOtprema.SelectedValue = DTotpremnice.Rows[0]["id_otprema"].ToString();
            //txtMjOtpreme.Text = DTotpremnice.Rows[0]["mj_otpreme"].ToString();
            //txtAdresaOtp.Text = DTotpremnice.Rows[0]["adr_otpreme"].ToString();
            //txtIsprave1.Text = DTotpremnice.Rows[0]["isprave"].ToString();
            if (DTotpremnice.Rows[0]["id_prijevoznik"].ToString() != "0")
            {
                //txtSifraPrijevoznik.Text = DTotpremnice.Rows[0]["id_prijevoznik"].ToString();
                try
                { //txtNazivPrijevoznik.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_prijevoznik"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception) { }
            }
            //txtReg.Text = DTotpremnice.Rows[0]["registracija"].ToString();
            //txtIstovarnoMJ.Text = DTotpremnice.Rows[0]["istovarno_mj"].ToString();
            //dtpIstovarniRok.Value = Convert.ToDateTime(DTotpremnice.Rows[0]["istovarni_rok"].ToString());
            //txtTroskovi.Text = DTotpremnice.Rows[0]["troskovi_prijevoza"].ToString();
            //nuGodinaPonude.Value = Convert.ToInt16(DTotpremnice.Rows[0]["godina_Otpremnice"].ToString());
            txtBrojOtpremnice.Text = broj_otpremnice_edit;
            cbSkladiste.SelectedValue = DTotpremnice.Rows[0]["id_skladiste"].ToString();

            if (DTotpremnice.Rows[0]["partner_osoba"].ToString() == "O")
            {
                rbOsoba.Checked = true;
            }
            else if (DTotpremnice.Rows[0]["partner_osoba"].ToString() == "P")
            {
                rbPoslovniPartner.Checked = true;
            }

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_otpremnice = '" + DTotpremnice.Rows[0]["broj_otpremnice"].ToString() + "'  AND id_skladiste='" + skl + "'", "otpremnica_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                //dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                _izracun_rezultat = _otpremnica.izracun(dgw);
                textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoMpc);
                textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoVpc);
                textBox3.Text = "PDV: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnpPdv);

                ControlDisableEnable(0, 1, 1, 0, 1);
                PaintRows(dgw);
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (_otpremnica.otpremnicaBrisi(dgw, DSFS, txtBrojOtpremnice.Text))
            {
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAllFaktura.Enabled = false;
                btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
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
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (dg(dgw.CurrentRow.Index, "oduzmi") == "DA")
                    {
                        if (Class.Postavke.is_caffe)
                        {
                            if (Class.Postavke.skidaj_kolicinu_po_dokumentima)
                            {
                                string kol = SQL.ClassSkladiste.GetAmountCaffe(dataROW[0]["sifra"].ToString(), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                            }
                            SQL.ClassSkladiste.SetBrojcanik(dataROW[0]["sifra"].ToString(), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "-");
                        }
                        else
                        {
                            string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                            kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                        }
                    }
                    classSQL.delete("DELETE FROM otpremnica_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa otpremnice br." + txtBrojOtpremnice.Text + "')"));
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void UpdateOtpremnica()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("godina_otpremnice");
            DTsend.Columns.Add("broj_otpremnice");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("id_stavka");
            DataRow row;

            DataTable DTsend1 = new DataTable();
            DTsend1.Columns.Add("kolicina");
            DTsend1.Columns.Add("vpc");
            DTsend1.Columns.Add("nbc");
            DTsend1.Columns.Add("godina_otpremnice");
            DTsend1.Columns.Add("broj_otpremnice");
            DTsend1.Columns.Add("porez");
            DTsend1.Columns.Add("sifra_robe");
            DTsend1.Columns.Add("rabat");
            DTsend1.Columns.Add("oduzmi");
            DTsend1.Columns.Add("id_skladiste");
            DataRow row1;

            string partner_osoba = "";
            if (rbOsoba.Checked)
            {
                partner_osoba = "O";
            }
            else if (rbPoslovniPartner.Checked)
            {
                partner_osoba = "P";
            }

            //string Str = txtSifraPrijevoznik.Text.Trim();
            //double Num;
            //bool isNum = double.TryParse(Str, out Num);
            //if (!isNum) {
            //    txtSifraPrijevoznik.Text = "0";
            //}

            if (classSQL.remoteConnectionString == "")
            {
                SveUkupno = SveUkupno.Replace(",", ".");
            }
            else
            {
                SveUkupno = SveUkupno.Replace(".", ",");
            }

            string sql = "UPDATE otpremnice SET " +
                " id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'," +
                " osoba_partner='" + txtSifraPosPatner.Text + "'," +
                " id_odrediste='" + txtSifraOdrediste.Text + "'," +
                " vrsta_dok=''," + //" + cbVD.SelectedValue + "
                " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izjava=''," + //" + cbIzjava.SelectedValue + "
                " napomena='" + rtbNapomena.Text + "'," +
                " id_kom='" + cbKomercijalist.SelectedValue + "'," +
                " id_otprema=''," + //" + cbOtprema.SelectedValue + "
                " mj_otpreme=''," + //" + txtMjOtpreme.Text + "
                " adr_otpreme=''," + //" + txtAdresaOtp.Text + "
                " isprave=''," + //" + txtIsprave1.Text + "
                " id_prijevoznik=''," + //" + txtSifraPrijevoznik.Text + "
                " registracija=''," + //" + txtReg.Text + "
                " istovarno_mj=''," + //" + txtIstovarnoMJ.Text + "
                " istovarni_rok=''," + //" + dtpIstovarniRok.Value.ToString("yyyy-MM-dd H:mm:ss") + "
                " troskovi_prijevoza=''," + //" + txtTroskovi.Text + "
                " partner_osoba='" + partner_osoba + "'," +
                " editirano = true," +
                " novo = false," +
                " ukupno='" + SveUkupno + "' WHERE broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                "";

            provjera_sql(classSQL.update(sql));

            string kol;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (dg(i, "oduzmi") == "DA")
                    {
                        if (cbSkladiste.SelectedValue.ToString() == dataROW[0]["id_skladiste"].ToString())
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                        else
                        {
                            //vrača na staro skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                            //oduzima sa novog skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                    }

                    row = DTsend.NewRow();
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = dg(i, "vpc");
                    row["nbc"] = dg(i, "nc");
                    row["godina_otpremnice"] = nmGodinaOtpremnice.Value.ToString();
                    row["broj_otpremnice"] = txtBrojOtpremnice.Text;
                    row["porez"] = dg(i, "porez");
                    row["sifra_robe"] = dg(i, "sifra");
                    row["rabat"] = dg(i, "rabat");
                    row["oduzmi"] = dg(i, "oduzmi");
                    row["id_stavka"] = dg(i, "id_stavka");
                    row["id_skladiste"] = cbSkladiste.SelectedValue.ToString();
                    DTsend.Rows.Add(row);
                }
                else
                {
                    if (dg(i, "oduzmi") == "DA")
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }

                    row1 = DTsend1.NewRow();
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["vpc"] = dg(i, "vpc");
                    row1["nbc"] = dg(i, "nc");
                    row1["godina_otpremnice"] = nmGodinaOtpremnice.Value.ToString();
                    row1["broj_otpremnice"] = txtBrojOtpremnice.Text;
                    row1["porez"] = dg(i, "porez");
                    row1["sifra_robe"] = dg(i, "sifra");
                    row1["rabat"] = dg(i, "rabat");
                    row1["oduzmi"] = dg(i, "oduzmi");
                    row1["id_skladiste"] = cbSkladiste.SelectedValue.ToString();
                    DTsend1.Rows.Add(row1);
                }
            }

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje otpremnice br." + txtBrojOtpremnice.Text + "')"));
            SQL.SQLotpremnica.UpdateStavke(DTsend);
            SQL.SQLotpremnica.InsertStavke(DTsend1);
            //MessageBox.Show("Spremljeno");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
        }

        private void txtBrojOtpremnice_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_otpremnice FROM otpremnice WHERE godina_otpremnice='" + nmGodinaOtpremnice.Value.ToString() + "' AND broj_otpremnice='" + txtBrojOtpremnice.Text + "'", "otpremnice").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojOtpremnice() == txtBrojOtpremnice.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        txtBrojOtpremnice.Text = brojOtpremnice();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        txtBrojOtpremnice.ReadOnly = true;
                        nmGodinaOtpremnice.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_otpremnice_edit = txtBrojOtpremnice.Text;
                    skladiste_edit = cbSkladiste.SelectedValue.ToString();
                    fillOtpremnice(cbSkladiste.SelectedValue.ToString());
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    txtBrojOtpremnice.ReadOnly = true;
                    nmGodinaOtpremnice.ReadOnly = true;
                }
                cbSkladiste.Select();
            }
        }

        private void nuGodinaPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) {
            //    DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nuGodinaPonude.Value.ToString() + "' AND broj_ponude='" + txtBrojPonude.Text + "'", "ponude").Tables[0];
            //    deleteFields();
            //    if (DT.Rows.Count == 0) {
            //        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
            //    } else if (DT.Rows.Count == 1) {
            //        EnableDisable(true);
            //        edit = true;
            //        btnDeleteAllFaktura.Enabled = true;
            //        fillPonude(DT.Rows[0][0].ToString());
            //    }
            //}
        }

        private void fillPonude(string broj)
        {
            //fill header
            DTotpremnice = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj + "'", "ponude").Tables[0];

            txtSifraPosPatner.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtNazivPosPartner.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                //dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                _izracun_rezultat = _otpremnica.izracun(dgw);
                textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoMpc);
                textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoVpc);
                textBox3.Text = "PDV: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnpPdv);
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    dgw.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString()) / Convert.ToDouble("1," + (Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) + Convert.ToDouble(dgw.CurrentRow.Cells["porez_potrosnja"].FormattedValue.ToString())));
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }

            _izracun_rezultat = _otpremnica.izracun(dgw);
            textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoMpc);
            textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnoVpc);
            textBox3.Text = "PDV: " + String.Format("{0:0.00}", _izracun_rezultat.ukupnpPdv);
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void cbSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                txtBrojOtpremnice.Text = brojOtpremnice();
            }
        }

        private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    cbKomercijalist.Select();
                }
            }
        }
    }
}