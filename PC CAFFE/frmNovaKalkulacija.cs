using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNovaKalkulacija : Form
    {
        private DataSet DSskladiste = new DataSet();
        private DataSet DSroba = new DataSet();
        private DataSet DSPartneri = new DataSet();
        private DataSet DSzaposlenik = new DataSet();
        private DataSet DSporezi = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataTable DTstavke = new DataTable();
        private DataTable TBroba = new DataTable();
        private bool update = false;
        private double nabavna_ukupno = 0;
        public string broj_kalkulacije_edit { get; set; }
        public string edit_skladiste { get; set; }
        public frmMenu MainForm { get; set; }
        private bool load = false;

        public frmNovaKalkulacija()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNovaKalkulacija_Load(object sender, EventArgs e)
        {
            PaintRows(dataGridView1);
            numeric();
            tabControl1.SelectedTab = tabPage2;
            loadCB();
            nuGodinaKalk.Text = DateTime.Now.Year.ToString();
            txtBrojKalkulacije.Text = brojKalkulacije();
            SetRekapitulacija();
            EnableDisable(false);
            txtBrojKalkulacije.Select();
            ReadOnly(true);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_kalkulacije_edit != null) { edit_kalkulacija(broj_kalkulacije_edit, edit_skladiste); }
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            load = true;
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
            nuGodinaKalk.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaKalk.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaKalk.Value = 2012;
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void Form1_Paint1(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojKalkulacije()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM kalkulacija WHERE id_skladiste='" + CBskladiste.SelectedValue + "'", "kalkulacija").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void SetRekapitulacija()
        {
            dataGridView2.Rows.Add("NABAVNI IZNOS U VALUTI", "");
            dataGridView2.Rows.Add("NABAVNI IZNOS U KUNAMA", "");
            dataGridView2.Rows.Add("IZNOS PRIJEVOZA", "");
            dataGridView2.Rows.Add("IZNOS CARINE", "");
            dataGridView2.Rows.Add("IZNOS TROŠKOVA UKUPNO", "");
            dataGridView2.Rows.Add("IZNOS POSEBNOG POREZA", "");
            dataGridView2.Rows.Add("VELEPRODAJNI IZNOS", "");
            dataGridView2.Rows.Add("MALOPRODAJNI IZNOS", "");
        }

        private void loadCB()
        {
            //CB zaposlenik
            //Properties.Settings.Default.id_zaposlenik = "1";
            DSzaposlenik = classSQL.select("SELECT ime + ' ' + prezime AS ime_prezime,id_zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici");
            txtZaposlenik.Text = DSzaposlenik.Tables[0].Rows[0][0].ToString();

            //DS skladiste
            DSskladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1') ORDER BY skladiste ", "skladiste");
            CBskladiste.DataSource = DSskladiste.Tables[0];
            CBskladiste.DisplayMember = "skladiste";
            CBskladiste.ValueMember = "id_skladiste";

            ////DS partneri
            //DSPartneri = classSQL.select("SELECT * FROM partners ORDER BY ime_tvrtke", "partners");
            //cbDobavljac.DataSource = DSPartneri.Tables[0];
            //cbDobavljac.DisplayMember = "ime_tvrtke";
            //cbDobavljac.ValueMember = "id_partner";

            //DS porez
            DSporezi = classSQL.select("SELECT * FROM porezi ORDER BY id_porez ASC", "porezi");
            txtPDV.DataSource = DSporezi.Tables[0];
            txtPDV.DisplayMember = "naziv";
            txtPDV.ValueMember = "iznos";

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtValutaValuta.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtValutaValuta.Text = "1";

            //DS porez_na_potrosnju
            DataTable DSpp = classSQL.select("SELECT * FROM porez_na_potrosnju ORDER BY id_porez_potrosnja ASC", "porezi").Tables[0];
            cbPorez_potrosnja.DataSource = DSpp;
            cbPorez_potrosnja.DisplayMember = "naziv";
            cbPorez_potrosnja.ValueMember = "iznos";
            cbPorez_potrosnja.SelectedValue = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            if (Properties.Settings.Default.id_roba != "")
            {
                ReadOnly(false);
                EnableDisable(true);
                Fill_Roba(Properties.Settings.Default.id_roba);
            }
        }

        private void Fill_Roba(string id_roba)
        {
            TBroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + id_roba + "' AND oduzmi ='DA'", "roba").Tables[0];
            if (TBroba.Rows.Count == 0)
            {
                MessageBox.Show("Odabrani artikl ili usluga se ne oduzima iz skladište te nije potrebno raditi kalkulaciju.", "Greška");
                return;
            }
            txtSifra_robe.Text = TBroba.Rows[0]["sifra"].ToString();
            txtNazivRobe.Text = TBroba.Rows[0]["naziv"].ToString();
            setRoba();
            txtKolicina.Select();
            PaintRows(dataGridView1);
        }

        private void setRoba()
        {
            txtFakCijena.Text = TBroba.Rows[0]["nc"].ToString();
            txtVPC.Text = TBroba.Rows[0]["vpc"].ToString();
            txtMPC.Text = TBroba.Rows[0]["mpc"].ToString();
            txtKolicina.Text = "1";
            cbPorez_potrosnja.SelectedValue = TBroba.Rows[0]["porez_potrosnja"].ToString();
            int br = dataGridView1.Rows.Count + 1;
            dataGridView1.Rows.Add(br, txtSifra_robe.Text, txtNazivRobe.Text, txtKolicina.Text, txtFakCijena.Text, "", txtMarza.Text, txtVPC.Text, txtMPC.Text, txtRabat.Text, txtPrijevoz.Text, txtCarina.Text, txtPosebniPorez.Text, txtPDV.SelectedValue, "", cbPorez_potrosnja.SelectedValue);
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            PaintRows(dataGridView1);
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            btnSve.Enabled = false;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                for (int y = 0; y < dataGridView1.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text.Trim() == dataGridView1.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                TBroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "' AND oduzmi='DA'", "roba").Tables[0];
                if (TBroba.Rows.Count > 0)
                {
                    ReadOnly(false);
                    EnableDisable(true);
                    txtSifra_robe.Text = TBroba.Rows[0]["sifra"].ToString().Trim();
                    txtNazivRobe.Text = TBroba.Rows[0]["naziv"].ToString();
                    setRoba();
                    izracun();
                    txtKolicina.Select();
                }
                else
                {
                    if (MessageBox.Show("Za ovu šifru ne postoj artikl ili na artiklu nije aktivirano oduzimanje sa skladišta.\r\nŽelite li dodati novu šifru?", "Greška", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        frmRobaUsluge robaUsluge = new frmRobaUsluge();
                        robaUsluge.Show();
                    }
                }
            }
        }

        private void txtKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtFakCijena.Select();
            }
        }

        private void txtFakCijena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtRabat.Select();
            }
        }

        private void txtRabat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtPrijevoz.Select();
            }
        }

        private void txtPrijevoz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtCarina.Select();
            }
        }

        private void txtCarina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtPosebniPorez.Select();
            }
        }

        private void txtPosebniPorez_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtMarza.Select();
            }
        }

        private void txtMarza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtVPC.Text = ((nabavna_ukupno * Convert.ToDouble(txtMarza.Text) / 100) + nabavna_ukupno).ToString("#0.00");
                txtMPC.Text = ((Convert.ToDouble(txtVPC.Text) * Convert.ToDouble(txtPDV.SelectedValue) / 100) + Convert.ToDouble(txtVPC.Text)).ToString("#0.00");
                txtVPC.Select();
            }
        }

        private void txtVPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }
                izracunVPC();
                txtPDV.Select();
            }
        }

        private void txtPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                izracun();
                cbPorez_potrosnja.Select();
            }
        }

        private void cbPorez_potrosnja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                izracun();
                txtMPC.Select();
            }
        }

        private void txtMPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }
                izracunMPC();
                txtSifra_robe.Select();
                dataGridView1.ClearSelection();
                novi();
            }
        }

        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                tabControl1.SelectedTab = tabPage2;
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
                btnSve.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSve.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAll.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAll.Enabled = true;
            }
        }

        private void novi()
        {
            //txtSifra_robe.Select();
            txtSifra_robe.Text = "";
            txtNazivRobe.Text = "";
            txtKolicina.Text = "0.00";
            txtFakCijena.Text = "0.00";
            txtMarza.Text = "0.00";
            txtVPC.Text = "0.0000";
            txtMPC.Text = "0.00";
            txtRabat.Text = "0.00";
            txtPrijevoz.Text = "0.00";
            txtCarina.Text = "0.00";
            txtPosebniPorez.Text = "0.00";

            txtIznosRabat.Text = "0.00";
            txtIznosFakCijena.Text = "0.00";
            txtUkupno.Text = "0.00";
            txtIznosMPC.Text = "0.00";
        }

        private void izracun()
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            txtIznosFakCijena.Text = Convert.ToString(Convert.ToDouble(txtKolicina.Text) * Convert.ToDouble(txtFakCijena.Text));

            txtIznosRabat.Text = Convert.ToString(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100);

            nabavna_ukupno = ((Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtValutaValuta.Text)) - (Convert.ToDouble(txtIznosRabat.Text) * Convert.ToDouble(txtValutaValuta.Text))) + (Convert.ToDouble(txtPrijevoz.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtCarina.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtPosebniPorez.Text) / Convert.ToDouble(txtKolicina.Text));

            double VPC = nabavna_ukupno;
            txtNabavnaCijenaKune.Text = (nabavna_ukupno * Convert.ToDouble(txtKolicina.Text)).ToString();
            VPC = (VPC * Convert.ToDouble(txtMarza.Text) / 100) + VPC;
            //txtVPC.Text = VPC.ToString();

            double MPC = nabavna_ukupno;
            MPC = (VPC * (Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorez_potrosnja.SelectedValue)) / 100) + VPC;
            //txtMPC.Text = MPC.ToString();
            txtIznosVPC.Text = Convert.ToString(Convert.ToDouble(txtVPC.Text) * Convert.ToDouble(txtKolicina.Text));

            txtUkupno.Text = String.Format("{0:0.00}", Convert.ToDouble(txtVPC.Text) - nabavna_ukupno);
            txtIznosPDV.Text = String.Format("{0:0.00}", VPC * (Convert.ToDouble(txtPDV.SelectedValue) / 100) * Convert.ToDouble(txtKolicina.Text));
            txtIznosMPC.Text = String.Format("{0:0.00}", Convert.ToDouble(txtMPC.Text) * Convert.ToDouble(txtKolicina.Text));

            //
            txtMarza.Text = Convert.ToString((Convert.ToDouble(txtVPC.Text) / nabavna_ukupno - 1) * 100);
            txtMPC.Text = Convert.ToString((Convert.ToDouble(txtVPC.Text) * (Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorez_potrosnja.SelectedValue)) / 100) + Convert.ToDouble(txtVPC.Text));

            change_dataGrid();
        }

        private void izracunVPC()
        {
            nabavna_ukupno = ((Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtValutaValuta.Text)) - (Convert.ToDouble(txtIznosRabat.Text) * Convert.ToDouble(txtValutaValuta.Text))) + (Convert.ToDouble(txtPrijevoz.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtCarina.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtPosebniPorez.Text) / Convert.ToDouble(txtKolicina.Text));

            txtMPC.Text = Convert.ToString((Convert.ToDouble(txtVPC.Text) * (Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorez_potrosnja.SelectedValue)) / 100) + Convert.ToDouble(txtVPC.Text));
            txtMarza.Text = Convert.ToString((Convert.ToDouble(txtVPC.Text) / nabavna_ukupno - 1) * 100);
            txtUkupno.Text = String.Format("{0:0.00}", Convert.ToDouble(txtVPC.Text) - nabavna_ukupno);

            if (Convert.ToDouble(txtMarza.Text) < 0)
            {
                txtMarza.BackColor = Color.Red;
                MessageBox.Show("Upozorenje.\r\nMarža je manja od nule.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtMarza.BackColor = Color.White;
            }

            change_dataGrid();
        }

        private void izracunMPC()
        {
            nabavna_ukupno = ((Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtValutaValuta.Text)) - (Convert.ToDouble(txtIznosRabat.Text) * Convert.ToDouble(txtValutaValuta.Text))) + (Convert.ToDouble(txtPrijevoz.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtCarina.Text) / Convert.ToDouble(txtKolicina.Text)) + (Convert.ToDouble(txtPosebniPorez.Text) / Convert.ToDouble(txtKolicina.Text));

            txtVPC.Text = Convert.ToString(Convert.ToDouble(txtMPC.Text) / Convert.ToDouble("1," + (Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorez_potrosnja.SelectedValue))));
            txtMarza.Text = Convert.ToString((Convert.ToDouble(txtVPC.Text) / nabavna_ukupno - 1) * 100);
            txtUkupno.Text = String.Format("{0:0.00}", Convert.ToDouble(txtVPC.Text) - nabavna_ukupno);

            if (Convert.ToDouble(txtMarza.Text) < 0)
            {
                txtMarza.BackColor = Color.Red;
                MessageBox.Show("Upozorenje.\r\nMarža je manja od nule.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtMarza.BackColor = Color.White;
            }

            change_dataGrid();
            EnableDisable(false);
        }

        private void change_dataGrid()
        {
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value = txtSifra_robe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value = txtNazivRobe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = txtKolicina.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = String.Format("{0:0.00}", Convert.ToDouble(txtFakCijena.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = String.Format("{0:0.00}", nabavna_ukupno);
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value = txtMarza.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value = String.Format("{0:0.0000}", Convert.ToDouble(txtVPC.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value = String.Format("{0:0.00}", Convert.ToDouble(txtMPC.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value = String.Format("{0:0.00}", Convert.ToDouble(txtRabat.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value = String.Format("{0:0.00}", Convert.ToDouble(txtPrijevoz.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[11].Value = String.Format("{0:0.00}", Convert.ToDouble(txtCarina.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[12].Value = String.Format("{0:0.00}", Convert.ToDouble(txtPosebniPorez.Text));
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[13].Value = Convert.ToDouble(txtPDV.SelectedValue);
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["porez_potrosnja"].Value = cbPorez_potrosnja.SelectedValue;
        }

        private void decimal_set_Leave(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            izracun();

            txtKolicina.Text = String.Format("{0:0.00}", Convert.ToDouble(txtKolicina.Text));
            txtFakCijena.Text = String.Format("{0:0.00}", Convert.ToDouble(txtFakCijena.Text));
            txtVPC.Text = String.Format("{0:0.0000}", Convert.ToDouble(txtVPC.Text));
            txtMPC.Text = String.Format("{0:0.00}", Convert.ToDouble(txtMPC.Text));
            txtPrijevoz.Text = String.Format("{0:0.00}", Convert.ToDouble(txtPrijevoz.Text));
            txtCarina.Text = String.Format("{0:0.00}", Convert.ToDouble(txtCarina.Text));
            txtPosebniPorez.Text = String.Format("{0:0.00}", Convert.ToDouble(txtPosebniPorez.Text));
            txtIznosRabat.Text = String.Format("{0:0.00}", Convert.ToDouble(txtIznosRabat.Text));
            txtIznosFakCijena.Text = String.Format("{0:0.00}", Convert.ToDouble(txtIznosFakCijena.Text));
            txtUkupno.Text = String.Format("{0:0.00}", Convert.ToDouble(txtUkupno.Text));
            txtIznosMPC.Text = String.Format("{0:0.00}", Convert.ToDouble(txtIznosMPC.Text));
            txtIznosPDV.Text = String.Format("{0:0.00}", Convert.ToDouble(txtIznosPDV.Text));
            txtIznosPDV.Text = String.Format("{0:0.00}", Convert.ToDouble(txtIznosPDV.Text));
            txtNabavnaCijenaKune.Text = String.Format("{0:0.00}", Convert.ToDouble(txtNabavnaCijenaKune.Text));
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

            if ((char)(',') == (e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double nabavni_valuta = 0;
            double nabavni_kune = 0;
            double prijevoz = 0;
            double carina = 0;
            double troskovi = 0;
            double posebni_porez = 0;
            double pdv = 0;
            double veleprodaja = 0;
            double maloprodaja = 0;
            double rabat = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;

            if (tabControl1.SelectedIndex == 2)
            {
                if (dataGridView1.Rows.Count < 1)
                {
                    tabControl1.SelectedTab = tabPage2;
                    MessageBox.Show("Za pogled na rekapitulaciju morate imati najmanje jednu stavku upisanu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double PDV_postotak = Convert.ToDouble(dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString());
                    rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                    nabavni_valuta = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + nabavni_valuta;
                    nabavni_kune = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + nabavni_kune;
                    fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                    prijevoz = Convert.ToDouble(dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString()) + prijevoz;
                    carina = Convert.ToDouble(dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString()) + carina;
                    troskovi = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + troskovi;
                    posebni_porez = Convert.ToDouble(dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString()) + posebni_porez;
                    pdv = (troskovi * PDV_postotak / 100) + pdv;
                    veleprodaja = (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + veleprodaja;
                    maloprodaja = (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + maloprodaja;
                    fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
                }

                dataGridView2.Rows[0].Cells[1].Value = String.Format("{0:0.00}", fak_cijena - fak_cijena_Rabat);
                dataGridView2.Rows[1].Cells[1].Value = String.Format("{0:0.00}", (fak_cijena - fak_cijena_Rabat) * Convert.ToDouble(txtValutaValuta.Text));
                dataGridView2.Rows[2].Cells[1].Value = String.Format("{0:0.00}", prijevoz);
                dataGridView2.Rows[3].Cells[1].Value = String.Format("{0:0.00}", carina);
                dataGridView2.Rows[4].Cells[1].Value = String.Format("{0:0.00}", troskovi);
                dataGridView2.Rows[5].Cells[1].Value = String.Format("{0:0.00}", posebni_porez);
                dataGridView2.Rows[6].Cells[1].Value = String.Format("{0:0.00}", veleprodaja);
                dataGridView2.Rows[7].Cells[1].Value = String.Format("{0:0.00}", maloprodaja);

                PaintRows(dataGridView2);
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                if (dataGridView1.Rows.Count > 0) { cbValuta.Enabled = false; }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double PDV_postotak = Convert.ToDouble(dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString());
                    double kolicina = Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                    rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());

                    nabavni_valuta = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * kolicina) + nabavni_valuta;
                    nabavni_kune = Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) + nabavni_kune;
                    fak_cijena = (Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) * kolicina) + fak_cijena;
                    prijevoz = Convert.ToDouble(dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString()) + prijevoz;
                    carina = Convert.ToDouble(dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString()) + carina;
                    troskovi = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + troskovi;
                    posebni_porez = Convert.ToDouble(dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString()) + posebni_porez;
                    pdv = (troskovi * PDV_postotak / 100) + pdv;
                    veleprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) + veleprodaja;
                    maloprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) + maloprodaja;
                    fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
                }

                if (cbValuta.Text != "HR Kuna")
                {
                    txtValutaFakIznos.Text = String.Format("{0:0.00}", fak_cijena);
                    txtValutaIznosRabata.Text = String.Format("{0:0.00}", fak_cijena_Rabat);
                    txtValutaUkupniIznos.Text = String.Format("{0:0.00}", fak_cijena - fak_cijena_Rabat);
                    txtValutaUkupniPorez.Text = String.Format("{0:0.00}", pdv / Convert.ToDouble(txtValutaValuta.Text));
                }

                txtKuneUkupno.Text = String.Format("{0:0.00}", (fak_cijena - fak_cijena_Rabat) * Convert.ToDouble(txtValutaValuta.Text));
                txtKuneCarina.Text = String.Format("{0:0.00}", carina);
                txtKunePrijevoz.Text = String.Format("{0:0.00}", prijevoz);
                txtKunePosebniPorez.Text = String.Format("{0:0.00}", posebni_porez);
                txtKuneNabavniIznos.Text = String.Format("{0:0.00}", ((fak_cijena - fak_cijena_Rabat) * Convert.ToDouble(txtValutaValuta.Text)) + carina + prijevoz + posebni_porez);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                txtSifra_robe.Select();
            }
        }

        private void btnOtvoriKalkulacije_Click(object sender, EventArgs e)
        {
            frmPopisKalkulacija popisKalkulacija = new frmPopisKalkulacija();
            popisKalkulacija.MainForm = this;
            popisKalkulacija.ShowDialog();

            if (broj_kalkulacije_edit != null)
            {
                edit_kalkulacija(broj_kalkulacije_edit, edit_skladiste);
                ReadOnly(false);
                EnableDisable(true);
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        //---------------------------------------------------------------SPREMANJE-----------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("fak_cijena");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("prijevoz");
            DTsend.Columns.Add("carina");
            DTsend.Columns.Add("marza_postotak");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("posebni_porez");
            DTsend.Columns.Add("broj");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("id_skladiste");
            DataRow row;

            btnSve.Enabled = true;

            string Str = txtSifraDobavljac.Text.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);
            if (!isNum)
            {
                txtSifraDobavljac.Text = "0";
            }

            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Nemate niti jedan artikl za spremiti", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (update == true)
            {
                update_kalkulacija();
                ReadOnly(true);
                MessageBox.Show("Spremljeno.");
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string broj = brojKalkulacije();
            if (broj.Trim() != txtBrojKalkulacije.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                txtBrojKalkulacije.Text = broj;
            }

            double veleprodaja = 0;
            double maloprodaja = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            double rabat = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                veleprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) + veleprodaja;
                maloprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) + maloprodaja;
                fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
            }

            //DateTime dRac = Convert.ToDateTime(dtpRacun.Value);
            //string dtRac = dRac.Month + "." + dRac.Day + "." + dRac.Year;

            //DateTime dOtp = Convert.ToDateTime(dtpOtpremnica.Value);
            //string dtOtp = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            //DateTime dNow = Convert.ToDateTime(dtpDatumNow.Value);
            //string dtNow = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string fak_ukupno = (fak_cijena - fak_cijena_Rabat).ToString().Replace(",", ".");
            string vpc_ukupno = veleprodaja.ToString();
            string mpc_ukupno = maloprodaja.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                fak_ukupno = fak_ukupno.Replace(",", ".");
                vpc_ukupno = vpc_ukupno.Replace(",", ".");
                mpc_ukupno = mpc_ukupno.Replace(",", ".");
            }
            else
            {
                fak_ukupno = fak_ukupno.Replace(".", ",");
                vpc_ukupno = vpc_ukupno.Replace(".", ",");
                mpc_ukupno = mpc_ukupno.Replace(".", ",");
            }

            string sql = "INSERT INTO kalkulacija (godina,broj,id_partner,racun,otpremnica,racun_datum,otpremnica_datum," +
            "mjesto_troska,datum,ukupno_vpc,ukupno_mpc,fakturni_iznos,tecaj,id_valuta,id_skladiste,id_zaposlenik)" +
            " VALUES (" +
            "'" + nuGodinaKalk.Text + "'," +
            "'" + txtBrojKalkulacije.Text + "'," +
            "'" + txtSifraDobavljac.Text + "'," +
            "'" + txtBrojRac.Text + "'," +
            "'" + txtOtpremnica.Text + "'," +
            "'" + dtpRacun.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + dtpOtpremnica.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + txtMjestoTroska.Text + "'," +
            "'" + dtpDatumNow.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + vpc_ukupno + "'," +
            "'" + mpc_ukupno + "'," +
            "'" + fak_ukupno + "'," +
            "'" + txtValutaValuta.Text + "'," +
            "'" + cbValuta.SelectedValue + "'," +
            "'" + CBskladiste.SelectedValue + "'," +
            "'" + Properties.Settings.Default.id_zaposlenik + "'" +
            ")";
            provjera_sql(classSQL.insert(sql));

            string kol;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), CBskladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "+");
                SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));

                row = DTsend.NewRow();
                row["kolicina"] = dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString();
                row["fak_cijena"] = dataGridView1.Rows[i].Cells["fak_cijena"].FormattedValue.ToString();
                row["rabat"] = dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString();
                row["prijevoz"] = dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString();
                row["carina"] = dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString();
                row["marza_postotak"] = dataGridView1.Rows[i].Cells["marza"].FormattedValue.ToString();
                row["porez"] = dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString();
                row["posebni_porez"] = dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString();
                row["broj"] = txtBrojKalkulacije.Text;
                row["sifra"] = dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString();
                row["vpc"] = dataGridView1.Rows[i].Cells["vpc"].FormattedValue.ToString();
                row["porez_potrosnja"] = dataGridView1.Rows[i].Cells["porez_potrosnja"].FormattedValue.ToString();
                row["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                DTsend.Rows.Add(row);

                string nbc = dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString();
                string vpc = dataGridView1.Rows[i].Cells["vpc"].FormattedValue.ToString();
                string mpc = dataGridView1.Rows[i].Cells["mpc"].FormattedValue.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    nbc = nbc.Replace(",", ".");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(",", ".");
                }
                else
                {
                    nbc = nbc.Replace(".", ",");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(".", ",");
                }

                classSQL.update("UPDATE roba SET nc='" + nbc + "',porez='" + dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString() + "',mpc='" + mpc + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'");
                classSQL.update("UPDATE roba_prodaja SET nc='" + nbc + "',porez='" + dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString() + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'");
            }

            SQL.SQLkalkulacija.InsertStavke(DTsend);
            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova kalkulacija br." + txtBrojKalkulacije.Text + "')");
            ReadOnly(true);
            EnableDisable(false);
            delete_fields();
            txtBrojKalkulacije.Text = brojKalkulacije();
            btnSve.Enabled = true;
            MessageBox.Show("Spremljeno.");
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string dg(int row, string cell)
        {
            return dataGridView1.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void edit_kalkulacija(string id, string skl)
        {
            DataTable DTKalkulacije = classSQL.select("SELECT * FROM kalkulacija WHERE broj='" + id + "' AND id_skladiste='" + skl + "'", "kalkulacija").Tables[0];
            DTstavke = classSQL.select("SELECT kalkulacija.id_skladiste,kalkulacija_stavke.kolicina,kalkulacija_stavke.fak_cijena " +
                " ,kalkulacija_stavke.rabat,kalkulacija_stavke.prijevoz,kalkulacija_stavke.vpc,kalkulacija_stavke.porez_potrosnja,kalkulacija_stavke.carina,kalkulacija_stavke.marza_postotak,kalkulacija_stavke.porez,kalkulacija_stavke.posebni_porez,kalkulacija_stavke.broj,kalkulacija_stavke.sifra,kalkulacija_stavke.id_stavka " +
                " FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija_stavke.broj=kalkulacija.broj" +
                " WHERE kalkulacija_stavke.broj='" + DTKalkulacije.Rows[0]["broj"] + "'AND kalkulacija.id_skladiste='" + skl + "'", "kalkulacija_stavke").Tables[0];

            DateTime _DT;
            DateTime.TryParse(DTKalkulacije.Rows[0]["datum"].ToString(), out _DT);

            string ovl = Util.Korisno.UzmiOvlastTrenutnogZaposlenika();
            int br_days = 366;
            if (ovl == "user")
                br_days = 100;

            if (Util.Korisno.ZabranaUređivanjaDokumenta(br_days, _DT, ovl))
            {
                MessageBox.Show("Nemate ovlaštenje uređivati ovaj dokumenat nakon " + br_days + " dana od izrade istog.");
                return;
            }

            ReadOnly(false);
            update = true;

            //kalkulacija
            cbPorez_potrosnja.SelectedValue = DTstavke.Rows[0]["porez_potrosnja"].ToString();
            txtBrojKalkulacije.Text = DTKalkulacije.Rows[0]["broj"].ToString();
            txtSifraDobavljac.Text = DTKalkulacije.Rows[0]["id_partner"].ToString();
            txtBrojRac.Text = DTKalkulacije.Rows[0]["racun"].ToString();
            txtOtpremnica.Text = DTKalkulacije.Rows[0]["otpremnica"].ToString();
            dtpRacun.Text = DTKalkulacije.Rows[0]["racun_datum"].ToString();
            dtpOtpremnica.Text = DTKalkulacije.Rows[0]["otpremnica_datum"].ToString();
            txtMjestoTroska.Text = DTKalkulacije.Rows[0]["mjesto_troska"].ToString();
            dtpDatumNow.Text = DTKalkulacije.Rows[0]["datum"].ToString();
            nuGodinaKalk.Text = DTKalkulacije.Rows[0]["godina"].ToString();
            cbValuta.SelectedValue = DTKalkulacije.Rows[0]["id_valuta"].ToString();
            txtValutaValuta.Text = DTKalkulacije.Rows[0]["tecaj"].ToString();
            CBskladiste.SelectedValue = DTKalkulacije.Rows[0]["id_skladiste"].ToString();
            txtZaposlenik.Text = classSQL.select("SELECT ime + ' ' + prezime AS ime_prezime FROM zaposlenici WHERE id_zaposlenik='" + DTKalkulacije.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            Properties.Settings.Default.idSkladiste = DTKalkulacije.Rows[0]["id_skladiste"].ToString();
            Properties.Settings.Default.Save();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < DTstavke.Rows.Count; i++)
            {
                txtSifra_robe.Text = DTstavke.Rows[i]["sifra"].ToString();
                string s = DTstavke.Rows[i]["sifra"].ToString();
                txtNazivRobe.Text = classSQL.select("select naziv from roba WHERE sifra='" + DTstavke.Rows[i]["sifra"].ToString() + "'", "roba").Tables[0].Rows[0][0].ToString();
                txtKolicina.Text = DTstavke.Rows[i]["kolicina"].ToString();
                txtRabat.Text = DTstavke.Rows[i]["rabat"].ToString();
                txtFakCijena.Text = DTstavke.Rows[i]["fak_cijena"].ToString();
                txtPrijevoz.Text = DTstavke.Rows[i]["prijevoz"].ToString();
                txtCarina.Text = DTstavke.Rows[i]["carina"].ToString();
                txtPosebniPorez.Text = DTstavke.Rows[i]["posebni_porez"].ToString();
                txtMarza.Text = DTstavke.Rows[i]["marza_postotak"].ToString();
                txtPDV.SelectedValue = DTstavke.Rows[i]["porez"].ToString();
                txtVPC.Text = DTstavke.Rows[i]["vpc"].ToString();

                dataGridView1.Rows.Add(i + 1, txtSifra_robe.Text, txtNazivRobe.Text, txtKolicina.Text, txtFakCijena.Text, "", txtMarza.Text, txtVPC.Text, txtMPC.Text, txtRabat.Text, txtPrijevoz.Text, txtCarina.Text, txtPosebniPorez.Text, txtPDV.SelectedValue, DTstavke.Rows[i]["id_stavka"].ToString(), cbPorez_potrosnja.SelectedValue);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }

            ControlDisableEnable(0, 1, 1, 0, 1);
            PaintRows(dataGridView1);
        }

        //-----------------------------------------------------------UPDATE----------------------------------------------------------

        private void update_kalkulacija()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("fak_cijena");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("prijevoz");
            DTsend.Columns.Add("carina");
            DTsend.Columns.Add("marza_postotak");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("posebni_porez");
            DTsend.Columns.Add("broj");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("id_skladiste");
            DataRow row;

            DataTable DTsend2 = new DataTable();
            DTsend2.Columns.Add("kolicina");
            DTsend2.Columns.Add("fak_cijena");
            DTsend2.Columns.Add("rabat");
            DTsend2.Columns.Add("prijevoz");
            DTsend2.Columns.Add("carina");
            DTsend2.Columns.Add("marza_postotak");
            DTsend2.Columns.Add("porez");
            DTsend2.Columns.Add("posebni_porez");
            DTsend2.Columns.Add("broj");
            DTsend2.Columns.Add("where_broj");
            DTsend2.Columns.Add("sifra");
            DTsend2.Columns.Add("vpc");
            DTsend2.Columns.Add("id_skladiste");
            DTsend2.Columns.Add("porez_potrosnja");
            DTsend2.Columns.Add("id_skladiste_staro");
            DataRow row2;

            double veleprodaja = 0;
            double maloprodaja = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            double rabat = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                veleprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) + veleprodaja;
                maloprodaja = Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) + maloprodaja;
                fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
            }

            string fak_ukupno = (fak_cijena - fak_cijena_Rabat).ToString().Replace(",", ".");
            string vpc_ukupno = veleprodaja.ToString();
            string mpc_ukupno = maloprodaja.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                fak_ukupno = fak_ukupno.Replace(",", ".");
                vpc_ukupno = vpc_ukupno.Replace(",", ".");
                mpc_ukupno = mpc_ukupno.Replace(",", ".");
            }
            else
            {
                fak_ukupno = fak_ukupno.Replace(".", ",");
                vpc_ukupno = vpc_ukupno.Replace(".", ",");
                mpc_ukupno = mpc_ukupno.Replace(".", ",");
            }

            string sql_update_kalkulacija = "UPDATE kalkulacija SET " +
                " broj ='" + txtBrojKalkulacije.Text + "'," +
                " id_partner='" + txtSifraDobavljac.Text + "'," +
                " racun='" + txtBrojRac.Text + "'," +
                " racun_datum='" + dtpRacun.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " otpremnica_datum='" + dtpOtpremnica.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " mjesto_troska='" + txtMjestoTroska.Text + "'," +
                " datum='" + dtpDatumNow.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " godina='" + nuGodinaKalk.Text + "'," +
                " ukupno_vpc='" + vpc_ukupno + "'," +
                " ukupno_mpc='" + mpc_ukupno + "'," +
                " tecaj='" + txtValutaValuta.Text + "'," +
                " id_valuta='" + cbValuta.SelectedValue + "'," +
                " fakturni_iznos='" + fak_ukupno + "'," +
                " id_skladiste='" + CBskladiste.SelectedValue + "'" +
                " WHERE broj ='" + broj_kalkulacije_edit + "'";

            provjera_sql(classSQL.update(sql_update_kalkulacija));

            //Update kalkulacija_stavke
            string kol;
            for (int br = 0; br < dataGridView1.Rows.Count; br++)
            {
                string id_stavka = dataGridView1.Rows[br].Cells["id_stavka"].FormattedValue.ToString();
                if (id_stavka == "")
                {
                    //INSERT
                    kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), CBskladiste.SelectedValue.ToString(), dg(br, "kolicina"), "1", "+");
                    SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(br, "sifra"));

                    row = DTsend.NewRow();
                    row["kolicina"] = dataGridView1.Rows[br].Cells["kolicina"].FormattedValue.ToString();
                    row["fak_cijena"] = dataGridView1.Rows[br].Cells["fak_cijena"].FormattedValue.ToString();
                    row["rabat"] = dataGridView1.Rows[br].Cells["rabat"].FormattedValue.ToString();
                    row["prijevoz"] = dataGridView1.Rows[br].Cells["prijevoz"].FormattedValue.ToString();
                    row["carina"] = dataGridView1.Rows[br].Cells["carina"].FormattedValue.ToString();
                    row["marza_postotak"] = dataGridView1.Rows[br].Cells["marza"].FormattedValue.ToString();
                    row["porez"] = dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString();
                    row["posebni_porez"] = dataGridView1.Rows[br].Cells["posebni_porez"].FormattedValue.ToString();
                    row["broj"] = txtBrojKalkulacije.Text;
                    row["sifra"] = dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString();
                    row["porez_potrosnja"] = dataGridView1.Rows[br].Cells["porez_potrosnja"].FormattedValue.ToString();
                    row["vpc"] = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                    row["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                    DTsend.Rows.Add(row);
                }
                else
                {
                    //UPDATE

                    //ako je promijenjeno skladište
                    DataRow[] dataROW = DTstavke.Select("id_stavka = " + id_stavka);
                    if (CBskladiste.SelectedValue.ToString() != dataROW[0]["id_skladiste"].ToString())
                    {
                        //vrača na staro skladiste
                        kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(br, "sifra"));

                        //oduzima sa novog skladiste
                        kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), CBskladiste.SelectedValue.ToString(), dg(br, "kolicina"), "1", "+");
                        SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(br, "sifra"));
                    }
                    else
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), CBskladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(br, "kolicina"))).ToString(), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(br, "sifra"));
                    }

                    row2 = DTsend2.NewRow();
                    row2["kolicina"] = dataGridView1.Rows[br].Cells["kolicina"].FormattedValue.ToString();
                    row2["fak_cijena"] = dataGridView1.Rows[br].Cells["fak_cijena"].FormattedValue.ToString();
                    row2["rabat"] = dataGridView1.Rows[br].Cells["rabat"].FormattedValue.ToString();
                    row2["prijevoz"] = dataGridView1.Rows[br].Cells["prijevoz"].FormattedValue.ToString();
                    row2["carina"] = dataGridView1.Rows[br].Cells["carina"].FormattedValue.ToString();
                    row2["marza_postotak"] = dataGridView1.Rows[br].Cells["marza"].FormattedValue.ToString();
                    row2["porez"] = dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString();
                    row2["posebni_porez"] = dataGridView1.Rows[br].Cells["posebni_porez"].FormattedValue.ToString();
                    row2["broj"] = txtBrojKalkulacije.Text;
                    row2["where_broj"] = broj_kalkulacije_edit;
                    row2["sifra"] = dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString();
                    row2["vpc"] = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                    row2["porez_potrosnja"] = dataGridView1.Rows[br].Cells["porez_potrosnja"].FormattedValue.ToString();
                    row2["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                    row2["id_skladiste_staro"] = edit_skladiste;
                    DTsend2.Rows.Add(row2);
                }//ako je stavka ==""

                string nbc = dataGridView1.Rows[br].Cells["nab_cijena"].FormattedValue.ToString();
                string vpc = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                string mpc = dataGridView1.Rows[br].Cells["mpc"].FormattedValue.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    nbc = nbc.Replace(",", ".");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(",", ".");
                }
                else
                {
                    nbc = nbc.Replace(".", ",");
                    vpc = vpc.Replace(".", ",");
                    mpc = mpc.Replace(".", ",");
                }

                classSQL.update("UPDATE roba SET nc='" + nbc + "',porez='" + dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString() + "',mpc='" + mpc + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString() + "'");
                classSQL.update("UPDATE roba_prodaja SET nc='" + nbc + "',porez='" + dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString() + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'");
            }//for
            provjera_sql(SQL.SQLkalkulacija.InsertStavke(DTsend));
            provjera_sql(SQL.SQLkalkulacija.UpdateStavke(DTsend2));

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + 2 + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Uređivanje kalkulacije br." + txtBrojKalkulacije.Text + "')");
            EnableDisable(false);
            delete_fields();
            update = false;
            txtBrojKalkulacije.Text = brojKalkulacije();
            btnSve.Enabled = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // izracun();

                int br = dataGridView1.SelectedCells[0].RowIndex;

                txtSifra_robe.Text = dataGridView1.Rows[br].Cells[1].FormattedValue.ToString();
                txtNazivRobe.Text = dataGridView1.Rows[br].Cells[2].FormattedValue.ToString();
                txtKolicina.Text = dataGridView1.Rows[br].Cells[3].FormattedValue.ToString();
                txtFakCijena.Text = dataGridView1.Rows[br].Cells[4].FormattedValue.ToString();
                txtMarza.Text = dataGridView1.Rows[br].Cells[6].FormattedValue.ToString();
                txtVPC.Text = dataGridView1.Rows[br].Cells[7].FormattedValue.ToString();
                txtMPC.Text = dataGridView1.Rows[br].Cells[8].FormattedValue.ToString();
                txtRabat.Text = dataGridView1.Rows[br].Cells[9].FormattedValue.ToString();
                txtPrijevoz.Text = dataGridView1.Rows[br].Cells[10].FormattedValue.ToString();
                txtCarina.Text = dataGridView1.Rows[br].Cells[11].FormattedValue.ToString();
                txtPosebniPorez.Text = dataGridView1.Rows[br].Cells[12].FormattedValue.ToString();
                txtPDV.SelectedValue = dataGridView1.Rows[br].Cells[13].FormattedValue.ToString();
                cbPorez_potrosnja.SelectedValue = dataGridView1.Rows[br].Cells["porez_potrosnja"].FormattedValue.ToString();
                izracun();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) { MessageBox.Show("Nemate stavke za brisanje.", "Greška"); return; }
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].Value.ToString() != "")
            {
                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DataRow[] dataROW = DTstavke.Select("id_stavka = " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'");

                    classSQL.delete("DELETE FROM kalkulacija_stavke WHERE id_stavka='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            MessageBox.Show("Obrisano.");
        }

        private void btnObrisiSve_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) { MessageBox.Show("Nemate stavke za brisanje.", "Greška"); return; }
            if (MessageBox.Show("Brisanjem ove kalkulacije brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu kalkulaciju?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                int y = dataGridView1.Rows.Count;
                for (int i = 0; i < y; i++)
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        DataRow[] dataROW = DTstavke.Select("id_stavka = " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());

                        string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                        kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'");

                        classSQL.delete("DELETE FROM kalkulacija_stavke WHERE id_stavka='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                    else
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                }
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + 3 + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje kalkulacije br." + txtBrojKalkulacije.Text + "')");
                classSQL.delete("DELETE FROM kalkulacija WHERE broj='" + txtBrojKalkulacije.Text + "'");
                ReadOnly(true);
                MessageBox.Show("Obrisano.");
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReadOnly(true);
            btnSve.Enabled = true;
            delete_fields();
            EnableDisable(false);
            txtBrojKalkulacije.Text = brojKalkulacije();
            txtBrojKalkulacije.Enabled = true;
            txtBrojKalkulacije.ReadOnly = false;
            txtBrojKalkulacije.Select();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void delete_fields()
        {
            //txtSifra_robe.Select();
            txtSifra_robe.Text = "";
            txtNazivRobe.Text = "";
            txtKolicina.Text = "0.00";
            txtFakCijena.Text = "0.00";
            txtMarza.Text = "0.00";
            txtVPC.Text = "0.0000";
            txtMPC.Text = "0.00";
            txtRabat.Text = "0.00";
            txtPrijevoz.Text = "0.00";
            txtCarina.Text = "0.00";
            txtPosebniPorez.Text = "0.00";
            txtIznosRabat.Text = "0.00";
            txtIznosFakCijena.Text = "0.00";
            txtUkupno.Text = "0.00";
            txtIznosMPC.Text = "0.00";
            txtNabavnaCijenaKune.Text = "0.00";
            txtMjestoTroska.Text = "";
            txtBrojRac.Text = "";
            txtOtpremnica.Text = "";

            dataGridView1.Rows.Clear();
        }

        private void EnableDisable(bool b)
        {
            txtKolicina.Enabled = b;
            txtPrijevoz.Enabled = b;
            txtCarina.Enabled = b;
            txtMarza.Enabled = b;
            txtRabat.Enabled = b;
            txtPDV.Enabled = b;
            cbPorez_potrosnja.Enabled = b;
            txtVPC.Enabled = b;
            txtMPC.Enabled = b;
            txtFakCijena.Enabled = b;
            txtPosebniPorez.Enabled = b;

            dtpDatumNow.Enabled = b;
            CBskladiste.Enabled = b;
            txtSifraDobavljac.Enabled = b;
            txtMjestoTroska.Enabled = b;
            txtBrojRac.Enabled = b;
            txtOtpremnica.Enabled = b;
            dtpRacun.Enabled = b;
            dtpOtpremnica.Enabled = b;

            if (b == false)
            {
                txtBrojKalkulacije.Enabled = true;
                nuGodinaKalk.Enabled = true;
                CBskladiste.Enabled = true;
            }
            else
            {
                txtBrojKalkulacije.Enabled = false;
                nuGodinaKalk.Enabled = false;
                //CBskladiste.Enabled = false;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            ReadOnly(false);
            dtpDatumNow.Select();
            txtMjestoTroska.Enabled = true;
            txtBrojRac.Enabled = true;
            txtOtpremnica.Enabled = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            dtpRacun.Enabled = true;
            dtpOtpremnica.Enabled = true;
        }

        private void ReadOnly(bool b)
        {
            bool ab;
            if (b == false) { ab = true; } else { ab = false; }
            dtpDatumNow.Enabled = ab;
            //CBskladiste.Enabled = b;
            txtBrojKalkulacije.Enabled = b;
            nuGodinaKalk.Enabled = b;
            txtSifraDobavljac.Enabled = ab;

            //txtMjestoTroska.ReadOnly = b;
            //txtBrojRac.ReadOnly = b;
            //txtOtpremnica.ReadOnly = b;
            txtKolicina.ReadOnly = b;
            txtPrijevoz.ReadOnly = b;
            txtCarina.ReadOnly = b;
            txtMarza.ReadOnly = b;
            txtRabat.ReadOnly = b;
            txtVPC.ReadOnly = b;
            txtMPC.ReadOnly = b;
            txtFakCijena.ReadOnly = b;
            txtPosebniPorez.ReadOnly = b;
        }

        private void txtBrojKalkulacije_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM kalkulacija WHERE godina='" + nuGodinaKalk.Value.ToString() + "' AND broj='" + txtBrojKalkulacije.Text + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'", "kalkulacija").Tables[0];
                delete_fields();
                if (DT.Rows.Count == 0)
                {
                    if (brojKalkulacije() == txtBrojKalkulacije.Text.Trim())
                    {
                        update = false;
                        EnableDisable(true);
                        btnSve.Enabled = false;
                        txtBrojKalkulacije.Text = brojKalkulacije();
                        btnDeleteAll.Enabled = false;
                        txtBrojKalkulacije.ReadOnly = true;
                        nuGodinaKalk.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    if (Until.classZakljucavanjeDokumenta.isLockKalkulacija(Convert.ToInt32(txtBrojKalkulacije.Text), Convert.ToInt32(CBskladiste.SelectedValue)))
                    {
                        MessageBox.Show("Kalkulacija je zaključana. Uređivanje nije dopušteno.");
                        return;
                    }

                    broj_kalkulacije_edit = txtBrojKalkulacije.Text;
                    edit_skladiste = CBskladiste.SelectedValue.ToString();
                    edit_kalkulacija(txtBrojKalkulacije.Text, CBskladiste.SelectedValue.ToString());
                    EnableDisable(true);
                    update = true;
                    btnDeleteAll.Enabled = true;
                    txtBrojKalkulacije.ReadOnly = true;
                    nuGodinaKalk.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
                dtpDatumNow.Select();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            dtpDatumNow.Select();
        }

        private void dtpDatumNow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraDobavljac.Select();
            }
        }

        private void CBskladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraDobavljac.Select();
            }
        }

        private void cbDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMjestoTroska.Select();
            }
        }

        private void txtMjestoTroska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBrojRac.Select();
            }
        }

        private void txtBrojRac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpRacun.Select();
                txtOtpremnica.Text = txtBrojRac.Text;
            }
        }

        private void dtpRacun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtOtpremnica.Select();
                dtpOtpremnica.Value = dtpRacun.Value;
            }
        }

        private void txtOtpremnica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpOtpremnica.Select();
            }
        }

        private void dtpOtpremnica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableDisable(true);
        }

        private void txtNazivRobe_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            txtKolicina.Select();
        }

        private void txtSifraDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraDobavljac.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraDobavljac.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraDobavljac.Select();
                        }
                    }
                    else
                    {
                        txtSifraDobavljac.Select();
                        return;
                    }
                }

                string Str = txtSifraDobavljac.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraDobavljac.Text = "0";
                }

                DataTable DT = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraDobavljac.Text + "'", "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtImeDobavljaca.Text = DT.Rows[0][0].ToString();
                    txtMjestoTroska.Select();
                }
                else
                {
                    MessageBox.Show("Traženi dobavljač ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifraDobavljac.Select();
                }
            }
        }

        private void txtImeDobavljaca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMjestoTroska.Select();
            }
        }

        private void frmNovaKalkulacija_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraDobavljac.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImeDobavljaca.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void CBskladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                txtBrojKalkulacije.Text = brojKalkulacije();
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