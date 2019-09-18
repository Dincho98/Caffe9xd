using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PCPOS
{
    public partial class frmFaktura : Form
    {
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTRoba = new DataTable();
        private DataTable DSfakture = new DataTable();
        private DataTable DTpromocije1;
        private int brojStavkiUFakturi { get; set; }
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private DataTable DTnaplaceneotpremnice = new DataTable();

        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;
        public string broj_fakture_edit { get; set; }
        public frmMenu MainForm { get; set; }

        private List<int> otpremniceIdList = new List<int>();

        public frmFaktura()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
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

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;

            PaintRows(dgw);
            numeric();
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            fillComboBox();
            ttxBrojFakture.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_fakture_edit != null) { fillFakture(); }

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmFaktura MainForm { get; set; }

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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            //fill ziroracun
            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            cbZiroRacun.DisplayMember = "ziroracun";
            cbZiroRacun.ValueMember = "id_ziroracun";
            //cbZiroRacun.SelectedValue = "1";

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
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
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
            cbValuta.SelectedValue = 1;
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            //DTSK.Rows.Add(0,"");
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
            int godina = DateTime.Now.Year;
            nmGodinaFakture.Minimum = Convert.ToInt16(godina - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(godina + 30);
            nmGodinaFakture.Value = godina;

            nuGodinaPredujma.Minimum = Convert.ToInt16(godina - 30);
            nuGodinaPredujma.Maximum = Convert.ToInt16(godina + 30);
            nuGodinaPredujma.Value = godina;

            nuGodinaPonude.Minimum = Convert.ToInt16(godina - 30);
            nuGodinaPonude.Maximum = Convert.ToInt16(godina + 30);
            nuGodinaPonude.Value = godina;
        }

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
                        lblNaDan.ForeColor = Color.Lime;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        dgw.CurrentRow.Cells["vpc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        lblNaDan.ForeColor = Color.Red;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                        MessageBox.Show("Na odabranom skladištu nemate unešeni artikl.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
        }

        private string ReturnSifra(string sifra)
        {
            if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
            {
                return "00000";
            }

            return sifra;
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.roba = true;
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

                if (txtSifra_robe.Text.Length > 2)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
                    {
                        double uk;
                        double popust;
                        DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

                        if (DTrp.Rows.Count == 0)
                        {
                            MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
                        {
                            MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                        if (dateFromPopust < DateTime.Now)
                        {
                            MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                        popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                        uk = uk * 3 / 100;

                        if ((Convert.ToDouble(SveUkupno) - uk) < 0)
                        {
                            MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dgw.Rows.Add(
                                    dgw.RowCount - 1,
                                    txtSifra_robe.Text,
                                    "Popust sa prethodnog računa",
                                    "1",
                                    "kn",
                                    1,
                                    DTpostavke.Rows[0]["pdv"].ToString(),
                                    String.Format("{0:0.00}", (uk * (-1))),
                                    "0",
                                    "0",
                                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                                    String.Format("{0:0.00}", (uk * (-1))),
                                    (uk * (-1)) / Convert.ToDouble("1," + DTpostavke.Rows[0]["pdv"].ToString()),
                                    String.Format("{0:0.00}", (uk * (-1))),
                                    "",
                                    "",
                                    ""
                                );

                        int br = dgw.Rows.Count - 1;
                        dgw.ClearSelection();
                        izracun();
                        PaintRows(dgw);
                        dgw.ClearSelection();
                        txtSifra_robe.Text = "";
                        txtSifra_robe.Select();
                        return;
                    }
                }

                //for (int y = 0; y < dgw.Rows.Count; y++)
                //{
                //    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                //    {
                //        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    ttxBrojFakture.ReadOnly = true;

                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

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

                double vpc_stavka = 0;
                double kol_stavka = 0;
                double porez_potrosnja = 0;
                double pdv = 0;
                double rabat = 0;
                double mpc_stavka = 0;
                double rabat_stavka = 0;
                double pdv_stavka = 0;
                double RabatSve = 0;
                double osnovica_ukupno = 0;
                double pdv_ukupno = 0;

                kol_stavka = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());

                pdv = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString());
                rabat = Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString());
                porez_potrosnja = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez_potrosnja"].FormattedValue.ToString());
                mpc_stavka = Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString());

                vpc_stavka = (mpc_stavka - (mpc_stavka * (rabat / 100))) / (1 + ((pdv + porez_potrosnja) / 100));
                if (dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString() != "")
                    //vpc_stavka = Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());

                    pdv = vpc_stavka * (pdv / 100);
                porez_potrosnja = (vpc_stavka * (porez_potrosnja / 100));
                rabat = (mpc_stavka * (rabat / 100));

                rabat_stavka = (rabat * kol_stavka);
                pdv_stavka = (pdv * kol_stavka);

                porez_potrosnja = (porez_potrosnja * kol_stavka);

                osnovica_ukupno = vpc_stavka * kol_stavka;
                pdv_ukupno += pdv_stavka;
                //SveUkupno = (mpc_stavka - rabat) * kol_stavka;
                SveUkupno = mpc_stavka * kol_stavka - rabat_stavka;
                porez_potrosnja = SveUkupno - pdv_ukupno - osnovica_ukupno;

                dgw.Rows[rowBR].Cells["mpc"].Value = String.Format("{0:0.00}", mpc_stavka);
                dgw.Rows[rowBR].Cells["rabat_iznos"].Value = String.Format("{0:0.00}", rabat_stavka);
                dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = String.Format("{0:0.00}", osnovica_ukupno);
                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = String.Format("{0:0.00}", SveUkupno);
                dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", vpc_stavka);

                pdv = 0;
                u = 0;
                porez_potrosnja = 0;
                rabat_stavka = 0;

                vpc_stavka = 0;
                kol_stavka = 0;
                porez_potrosnja = 0;
                pdv = 0;
                rabat = 0;
                mpc_stavka = 0;
                rabat_stavka = 0;
                pdv_stavka = 0;
                osnovica_ukupno = 0;
                double porez_potrosnja_ukupno = 0;
                pdv_ukupno = 0;
                SveUkupno = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    mpc_stavka = Convert.ToDouble(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString());
                    rabat = Convert.ToDouble(dgw.Rows[i].Cells["rabat"].FormattedValue.ToString());
                    kol_stavka = Convert.ToDouble(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                    vpc_stavka = (mpc_stavka - (mpc_stavka * (rabat / 100))) / (1 + ((pdv + porez_potrosnja) / 100));
                    //if (dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString() != "")
                    //    vpc_stavka = Convert.ToDouble(dgw.Rows[i].Cells["vpc"].FormattedValue.ToString());
                    pdv = Convert.ToDouble(dgw.Rows[i].Cells["porez"].FormattedValue.ToString());
                    porez_potrosnja = Convert.ToDouble(dgw.Rows[i].Cells["porez_potrosnja"].FormattedValue.ToString());
                    vpc_stavka = mpc_stavka / (1 + (pdv + porez_potrosnja) / 100);

                    RabatSve += ((vpc_stavka * (rabat / 100)) * kol_stavka);
                    osnovica_ukupno += ((vpc_stavka - (vpc_stavka * (rabat / 100))) * kol_stavka);
                    pdv_ukupno += ((vpc_stavka - (vpc_stavka * rabat / 100)) * (pdv / 100) * kol_stavka);
                    porez_potrosnja_ukupno += (((vpc_stavka - (vpc_stavka * rabat / 100)) * (porez_potrosnja / 100)) * kol_stavka);
                    SveUkupno += ((mpc_stavka - (mpc_stavka * rabat / 100)) * kol_stavka);
                }
                //SveUkupno = Math.Round(osnovica_ukupno, 2, MidpointRounding.AwayFromZero) + Math.Round(pdv_ukupno, 2, MidpointRounding.AwayFromZero) + Math.Round(porez_potrosnja_ukupno, 2, MidpointRounding.AwayFromZero);
                u = SveUkupno;
                textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", SveUkupno);
                textBox2.Text = "Bez PDV-a: " + String.Format("{0:0.00}", osnovica_ukupno);
                textBox5.Text = "PNP: " + String.Format("{0:0.00}", porez_potrosnja_ukupno);
                textBox3.Text = "PDV: " + String.Format("{0:0.00}", pdv_ukupno);
                textBox4.Text = "Rabat: " + String.Format("{0:0.00}", RabatSve);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            //MessageBox.Show("Selected Index = " + selectedIndex);
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
                    deleteFields();
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
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
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
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
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
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

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    Properties.Settings.Default.id_partner = txtSifraOdrediste.Text;
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);
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

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners").Tables[0];
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

                dtpDatumDVO.Value = dtpDatum.Value;
                dtpDanaValuta.Value = dtpDatum.Value;
                dtpDatumDVO.Select();
            }
        }

        private void dtpDatumDVO_KeyDown(object sender, KeyEventArgs e)
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
                    DateTime dvo = dtpDatumDVO.Value;
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
                    DateTime dt2 = dtpDatumDVO.Value;
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
                e.SuppressKeyPress = true;
                try
                {
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
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtSifraNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbNacinPlacanja.Select();
                cbNacinPlacanja.SelectedValue = txtSifraNacinPlacanja.Text;
            }
        }

        private void cbNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraRadniNalog.Select();
                txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            }
        }

        private void txtSifraRadniNalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbRadniBalog.Select();
            }
        }

        private void cbRadniBalog_KeyDown(object sender, KeyEventArgs e)
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
                nuGodinaPredujma.Select();
            }
        }

        private void nuGodinaPredujma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbPredujam.Select();
            }
        }

        private void cbPredujam_KeyDown(object sender, KeyEventArgs e)
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
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    txtSifra_robe.Select();
                }
            }
        }

        #endregion ON_KEY_DOWN_REGION

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (chbOdizmiIzSkladista.Checked)
                    {
                        if (dgw.Rows[dgw.CurrentRow.Index].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                        {
                            string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                            kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                        }
                    }
                    classSQL.delete("DELETE FROM faktura_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje stavke sa fakture br." + ttxBrojFakture.Text + "')");
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
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        if (chbOdizmiIzSkladista.Checked)
                        {
                            if (dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                            {
                                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                                skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                                fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                                skl = skl + fa_kolicina;
                                classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                            }
                        }
                    }
                }

                SetNenaplaceno();
                classSQL.delete("DELETE FROM faktura_stavke WHERE broj_fakture='" + ttxBrojFakture.Text + "'");
                classSQL.update("UPDATE fakture SET ukupno = 0 WHERE broj_fakture='" + ttxBrojFakture.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje cijele fakture br." + ttxBrojFakture.Text + "')");
                MessageBox.Show("Obrisano.");

                ttxBrojFakture.ReadOnly = false;
                nmGodinaFakture.ReadOnly = false;
                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojFakture.Text = brojFakture();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            otpremniceIdList.Clear();

            ControlDisableEnable(1, 0, 0, 1, 0);
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
            txtSifraRadniNalog.Enabled = x;
            txtNarKupca1.Enabled = x;
            cbOtprema.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            btnPartner1.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbIzjava.Enabled = x;
            cbKomercijalist.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbZiroRacun.Enabled = x;
            cbValuta.Enabled = x;
            cbRadniBalog.Enabled = x;
            cbNarKupca.Enabled = x;
            nuGodinaPredujma.Enabled = x;
            cbPredujam.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnRadniNalog.Enabled = x;
            btnNarKupca.Enabled = x;
            btnPredujam.Enabled = x;
            btnOdaberiOtpremnice.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtSifraFakturirati.Text = "";
            txtPartnerNaziv.Text = "";
            txtPartnerNaziv1.Text = "";
            //txtSifraNacinPlacanja.Text = "";
            txtModel.Text = "";
            txtSifraNarKupca.Text = "";
            txtSifraRadniNalog.Text = "";
            txtNarKupca1.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            otpremniceIdList.Clear();
            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            ttxBrojFakture.ReadOnly = true;
            nmGodinaFakture.ReadOnly = true;
            txtSifraOdrediste.Select();
            chbOdizmiIzSkladista.Enabled = true;
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_fakture) FROM fakture", "fakture").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

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

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Faktura nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (edit == true)
            {
                UpdateFaktura();
                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printaj(ttxBrojFakture.Text);
                }
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ttxBrojFakture.Text = brojFakture();
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string broj = brojFakture();
            if (broj.Trim() != ttxBrojFakture.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                ttxBrojFakture.Text = broj;
            }

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string oduzmi_iz_skladista = "";
            if (chbOdizmiIzSkladista.Checked)
            {
                oduzmi_iz_skladista = "1";
            }
            else
            {
                oduzmi_iz_skladista = "0";
            }

            string sql = "INSERT INTO fakture (broj_fakture,id_odrediste,id_fakturirati,date,dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma,godina_ponude,godina_fakture,oduzmi_iz_skladista,ukupno, id_ducan, id_kasa, editirano, novo) VALUES " +
                " (" +
                 " '" + ttxBrojFakture.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + cbIzjava.SelectedValue + "'," +
                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + "0" + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + cbVD.SelectedValue.ToString() + "'," +
                " '" + nuGodinaPredujma.Value.ToString() + "'," +
                " '" + nuGodinaPonude.Value.ToString() + "'," +
                " '" + nmGodinaFakture.Value.ToString() + "'," +
                " '" + oduzmi_iz_skladista + "'," +
                " '" + uk + "'," +
                " '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'," +
                " '" + DTpostavke.Rows[0]["default_kasa_fakture"].ToString() + "', " +
                " '0'," +
                " '1'" +
                ")";

            provjera_sql(classSQL.insert(sql));

            string kol = "";
            DataSet DSkolicina = new DataSet();

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (chbOdizmiIzSkladista.Checked)
                {
                    //if (dg(i, "oduzmi") == "DA")
                    //{
                    kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                    SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                    //}
                }

                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nc");
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["porez"] = dg(i, "porez");
                row["sifra"] = ReturnSifra(dg(i, "sifra"));
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                row["id_skladiste"] = "1";//     dgw.Rows[i].Cells[3].Value;
                row["mpc"] = dg(i, "mpc");
                DTsend.Rows.Add(row);

                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }

            string barcode = "000" + ttxBrojFakture.Text;

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));

            //zapisivanje stavki
            provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend));

            if (dgw.Rows.Count > 0)
            {
                for (int i = 0; i < DTnaplaceneotpremnice.Rows.Count; i++)
                {
                    string pop_s_otpr = "Update otpremnica_stavke Set naplaceno_fakturom = '1' WHERE broj_otpremnice = '" + DTnaplaceneotpremnice.Rows[i]["broj_otpremnice"].ToString() + "';";
                    classSQL.update(pop_s_otpr);

                    pop_s_otpr = "Update otpremnice Set editirano = '1' WHERE broj_otpremnice = '" + DTnaplaceneotpremnice.Rows[i]["broj_otpremnice"].ToString() + "';";
                    classSQL.update(pop_s_otpr);
                }
                if (otpremniceIdList.Count > 0)
                    UpdateOtpremniceStatus();
            }

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(ttxBrojFakture.Text);
            }

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ttxBrojFakture.ReadOnly = false;
            ttxBrojFakture.Text = brojFakture();
            nmGodinaFakture.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void printaj(string broj)
        {
            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.dokumenat = "FAK";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
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

                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
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

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveFakture objForm2 = new frmSveFakture();
            objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFakture();
            }
        }

        private void fillFakture()
        {
            //fill header

            EnableDisable(true);
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);

            DSfakture = classSQL.select("SELECT * FROM fakture WHERE broj_fakture = '" + broj_fakture_edit + "'", "fakture").Tables[0];
            brojStavkiUFakturi = int.Parse(classSQL.select("SELECT count(*) FROM faktura_stavke WHERE broj_fakture = '" + broj_fakture_edit + "'", "fakture").Tables[0].Rows[0][0].ToString());
            

            cbVD.SelectedValue = DSfakture.Rows[0]["id_vd"].ToString();
            txtSifraOdrediste.Text = DSfakture.Rows[0]["id_odrediste"].ToString();
            Properties.Settings.Default.id_partner = txtSifraOdrediste.Text;
            txtSifraFakturirati.Text = DSfakture.Rows[0]["id_fakturirati"].ToString();
            txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtSifraNacinPlacanja.Text = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            txtModel.Text = DSfakture.Rows[0]["model"].ToString();
            cbOtprema.SelectedValue = DSfakture.Rows[0]["otprema"].ToString();
            rtbNapomena.Text = DSfakture.Rows[0]["napomena"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DSfakture.Rows[0]["date"].ToString());
            dtpDatumDVO.Value = Convert.ToDateTime(DSfakture.Rows[0]["dateDVO"].ToString());
            dtpDanaValuta.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum_valute"].ToString());
            cbKomercijalist.SelectedValue = DSfakture.Rows[0]["id_zaposlenik"].ToString();
            cbNacinPlacanja.SelectedValue = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            cbZiroRacun.SelectedValue = DSfakture.Rows[0]["zr"].ToString();
            cbValuta.SelectedValue = DSfakture.Rows[0]["id_valuta"].ToString();
            nuGodinaPredujma.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_predujma"].ToString());
            nuGodinaPonude.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_ponude"].ToString());
            nmGodinaFakture.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_fakture"].ToString());
            cbPredujam.SelectedValue = DSfakture.Rows[0]["id_predujam"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSfakture.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojFakture.Text = DSfakture.Rows[0]["broj_fakture"].ToString();
            //MessageBox.Show(DSfakture.Rows[0]["id_odrediste"].ToString());

            if (DSfakture.Rows[0]["oduzmi_iz_skladista"].ToString() == "1")
            {
                chbOdizmiIzSkladista.Checked = true;
            }
            else
            {
                chbOdizmiIzSkladista.Checked = false;
            }
            chbOdizmiIzSkladista.Enabled = false;

            //--------fill faktura stavke------------------------------

            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM faktura_stavke WHERE broj_fakture = '" + DSfakture.Rows[0]["broj_fakture"].ToString() + "'", "broj_fakture").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nbc,roba.naziv,roba.jm,roba.sifra FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                //dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nbc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[5];
                izracun();
                PaintRows(dgw);
            }
        }

        private void UpdateFaktura()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DataRow row;

            DataTable DTsend1 = new DataTable();
            DTsend1.Columns.Add("kolicina");
            DTsend1.Columns.Add("vpc");
            DTsend1.Columns.Add("nbc");
            DTsend1.Columns.Add("broj_fakture");
            DTsend1.Columns.Add("porez");
            DTsend1.Columns.Add("id_stavka");
            DTsend1.Columns.Add("sifra");
            DTsend1.Columns.Add("rabat");
            DTsend1.Columns.Add("oduzmi");
            DTsend1.Columns.Add("porez_potrosnja");
            DTsend1.Columns.Add("id_skladiste");
            DTsend1.Columns.Add("mpc");

            DataRow row1;

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string sql = "UPDATE fakture SET" +
                " id_odrediste= '" + txtSifraOdrediste.Text + "'," +
                " id_fakturirati='" + txtSifraFakturirati.Text + "'," +
                " date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " dateDVO='" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " datum_valute='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izjava='" + cbIzjava.SelectedValue + "'," +
                " id_zaposlenik='" + cbKomercijalist.SelectedValue + "'," +
                " id_zaposlenik_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                " model= '" + txtModel.Text + "'," +
                " id_nacin_placanja='" + cbNacinPlacanja.SelectedValue + "'," +
                " zr='" + cbZiroRacun.SelectedValue + "'," +
                " id_valuta='" + cbValuta.SelectedValue + "'," +
                " otprema='" + cbOtprema.SelectedValue + "'," +
                " id_predujam='0'," +
                " napomena='" + rtbNapomena.Text + "'," +
                " id_vd='" + cbVD.SelectedValue.ToString() + "'," +
                " godina_predujma='" + nuGodinaPredujma.Value.ToString() + "'," +
                " godina_ponude='" + nuGodinaPonude.Value.ToString() + "'," +
                " godina_fakture='" + nmGodinaFakture.Value.ToString() + "'," +
                " id_ducan = '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'," +
                " id_kasa = '" + DTpostavke.Rows[0]["default_kasa_fakture"].ToString() + "'," +
                " editirano = '1'," +
                " novo = '0'," +
                " ukupno='" + uk + "' WHERE  broj_fakture='" + ttxBrojFakture.Text + "'";

            provjera_sql(classSQL.update(sql));

            DataSet DSkolicina = new DataSet();
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (chbOdizmiIzSkladista.Checked)
                    {
                        //if (dg(i, "oduzmi") == "DA")
                        //{
                        if (dgw.Rows[i].Cells[3].Value.ToString() == dataROW[0]["id_skladiste"].ToString())
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                        }
                        else
                        {
                            //vrača na staro skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                            //oduzima sa novog skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                        }
                        //}
                    }
                    if (i > (brojStavkiUFakturi-1))
                    {
                        row1 = DTsend1.NewRow();
                        row1["kolicina"] = dg(i, "kolicina");
                        row1["vpc"] = dg(i, "vpc");
                        row1["nbc"] = dg(i, "nc");
                        row1["broj_fakture"] = ttxBrojFakture.Text;
                        row1["porez"] = dg(i, "porez");
                        row1["id_stavka"] = dg(i, "id_stavka");
                        row1["sifra"] = ReturnSifra(dg(i, "sifra"));
                        row1["rabat"] = dg(i, "rabat");
                        row1["oduzmi"] = dg(i, "oduzmi");
                        row1["porez_potrosnja"] = dg(i, "porez_potrosnja");
                        row1["id_skladiste"] = "1"; //dgw.Rows[i].Cells[3].Value;
                        row1["mpc"] = dg(i, "mpc");
                        DTsend1.Rows.Add(row1);
                    }
                }
                else
                {
                    if (chbOdizmiIzSkladista.Checked)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                        }
                    }
                    if (i > (brojStavkiUFakturi-1))
                    {
                        row = DTsend.NewRow();
                        row["kolicina"] = dg(i, "kolicina");
                        row["vpc"] = dg(i, "vpc");
                        row["nbc"] = dg(i, "nc");
                        row["broj_fakture"] = ttxBrojFakture.Text;
                        row["porez"] = dg(i, "porez");
                        row["id_stavka"] = dg(i, "id_stavka");
                        row["sifra"] = ReturnSifra(dg(i, "sifra"));
                        row["rabat"] = dg(i, "rabat");
                        row["oduzmi"] = dg(i, "oduzmi");
                        row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                        row["id_skladiste"] = "1"; //dgw.Rows[i].Cells[3].Value;
                        row["mpc"] = dg(i, "mpc");
                        DTsend.Rows.Add(row);
                    }
                }
            }
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));
            provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend1));
            /*if (GetStavkeCount() > 0)
                provjera_sql(SQL.SQLfaktura.UpdateStavke(DTsend1));
            else
                provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend1));
                */
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            edit = false;
        }

        /// <summary>
        /// Private method used to get "stavke" count for specific "faktura"
        /// </summary>
        /// <returns></returns>
        private int GetStavkeCount()
        {
            string query = $@"SELECT id_stavka FROM faktura_stavke WHERE broj_fakture = {ttxBrojFakture.Text}";
            DataTable DTstavke = classSQL.select(query, "faktura_stavke").Tables[0];
            return DTstavke.Rows.Count;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSavPotrosniMaterijal sp = new frmSavPotrosniMaterijal();
            sp.ShowDialog();
        }

        private void txtBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nuGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();

            dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            dgw.Rows[br].Cells["id_stavka"].Value = DTRoba.Rows[0]["id_roba"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            decimal _mpc = Convert.ToDecimal(DTRoba.Rows[0]["mpc"].ToString());
            dgw.Rows[br].Cells["vpc"].Value = (_mpc - (_mpc * (Convert.ToDecimal(DTRoba.Rows[0]["porez"].ToString()) / 100)));
            try
            {
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
            }
            catch
            {
                double vp = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());
                double rabat = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());

                double mpc = vp + (vp * rabat / 100);

                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
            }
            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["nbc"]);
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();
            dgw.Rows[br].Cells["porez_potrosnja"].Value = DTRoba.Rows[0]["porez_potrosnja"].ToString();

            dgw.CurrentCell = dgw.Rows[br].Cells[5];
            dgw.BeginEdit(true);
            izracun();
            PaintRows(dgw);
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_fakture FROM fakture WHERE godina_fakture='" + nmGodinaFakture.Value.ToString() + "' AND broj_fakture='" + ttxBrojFakture.Text + "'", "fakture").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojFakture() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojFakture.ReadOnly = true;
                        nmGodinaFakture.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    fillFakture();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void txtBrojPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nuGodinaPonude.Value.ToString() + "' AND broj_ponude='" + txtBrojPonude.Text + "'", "ponude").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillPonude(DT.Rows[0][0].ToString());
                }
            }
        }

        private void fillPonude(string broj)
        {
            //fill header
            DataTable DTotpremnice = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj + "'", "ponude").Tables[0];

            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            txtSifraFakturirati.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            cbZiroRacun.SelectedValue = DTotpremnice.Rows[0]["zr"].ToString();
            cbNacinPlacanja.SelectedValue = DTotpremnice.Rows[0]["id_nacin_placanja"].ToString();
            cbValuta.SelectedValue = DTotpremnice.Rows[0]["id_valuta"].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["otprema"].ToString();

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                //DataTable tblRoba = classSQL.select("SELECT * FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                //if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                //{
                //    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                //}
                //else
                {
                    s = "SELECT roba.nbc,roba.naziv,roba.jm,roba.sifra FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                //dgw.Rows[br].Cells[0].Value = i + 1;
                //dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                //dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                //dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                ////dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                //dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                //dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                //dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();
                //dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                //dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                //dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                //dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                //dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                //dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                ////dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                //dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                //dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                //dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nbc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                edit = false;
                PaintRows(dgw);
                izracun();
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
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void cbOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuGodinaPredujma.Select();
            }
        }

        private void frmFaktura_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        public bool popunjena_s_otpremnicom = false; //ako je fakturirano zapisuje u otpremnice (naplaceno da ne uzme jos 1 put)

        private void txtOtpremnicaKupac_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    /*DataTable DT = classSQL.select("SELECT id_partner FROM partners WHERE id_partner = '" + txtOtpremnicaKupac.Text + "' ", "otpremnice").Tables[0];
                    deleteFields();
                    if (DT.Rows.Count == 0)
                    {
                        MessageBox.Show("Odabrani partner ne postoji.", "Greška");
                    }
                    else if (DT.Rows.Count == 1)
                    {
                        EnableDisable(true);
                        edit = true;
                        btnDeleteAllFaktura.Enabled = true;
                        fillOtpremnice(DT.Rows[0][0].ToString());
                        popunjena_s_otpremnicom = true;
                        chbOdizmiIzSkladista.Enabled = false;
                        chbOdizmiIzSkladista.Checked = false;
                        btnObrisi.Enabled = false;
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtOtpremnicaKupac_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOtpremnicaKupca_Click(object sender, EventArgs e)
        {
            try
            {
                frmPartnerTrazi sp = new frmPartnerTrazi();
                sp.ShowDialog();
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
                frmOtpremnicaUFakturu frm = new frmOtpremnicaUFakturu();
                frm.ShowDialog();

                if ((frm.broj_otpremnice != null) && (frm.skl_otpremnice != null))
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillOtpremnice(frm.broj_otpremnice, frm.skl_otpremnice);
                    popunjena_s_otpremnicom = true;
                    chbOdizmiIzSkladista.Enabled = false;
                    chbOdizmiIzSkladista.Checked = false;
                    btnObrisi.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillOtpremnice(string broj)
        {
            //fill header

            //--------fill otpremnica stavke----------------------------
            /*DataTable DTotpremnice = new DataTable();
            DataTable DTotpremnica_stavke = new DataTable();
            DataTable DTskl = classSQL.select("SELECT * FROM skladiste ", "skl").Tables[0];
            if (DTnaplaceneotpremnice.Columns.Contains("id") != true)
            {
                DTnaplaceneotpremnice.Columns.Add("id");
                DTnaplaceneotpremnice.Columns.Add("broj_otpremnice");
            }

            DataRow row;
            DataTable dtR = new DataTable();
            //DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];
            bool header_1x = true;
            for (int i = 0; i < DTskl.Rows.Count; i++)
            {
                DTotpremnice = classSQL.select("SELECT * FROM otpremnice  WHERE na_sobu = false and osoba_partner = '" + broj + "' AND id_skladiste = '" + DTskl.Rows[i]["id_skladiste"].ToString() + "' and datum >= '" + dtpOtpremnicaDatumOd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and datum <= '" + dtpOtpremnicaDatumDo.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'", "otpremnice").Tables[0];

                if (DTotpremnice.Rows.Count != 0)
                {
                    for (int z = 0; z < DTotpremnice.Rows.Count; z++)
                    {
                        row = DTnaplaceneotpremnice.NewRow();
                        row["id"] = DTnaplaceneotpremnice.Rows.Count.ToString();
                        row["broj_otpremnice"] = DTotpremnice.Rows[z]["broj_otpremnice"];
                        DTnaplaceneotpremnice.Rows.Add(row);

                        try
                        {
                            DTotpremnica_stavke = classSQL.select("SELECT * FROM otpremnica_stavke  WHERE broj_otpremnice = '" + DTotpremnice.Rows[z]["broj_otpremnice"].ToString() + "' AND naplaceno_fakturom <> 'TRUE'", "otpremnice stavke").Tables[0];
                            for (int x = 0; x < DTotpremnica_stavke.Rows.Count; x++)
                            {
                                dgw.Rows.Add();
                                int br = dgw.Rows.Count - 1;
                                DataTable roba = classSQL.select("Select * from roba where sifra = '" + DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString() + "'", "roba").Tables[0];
                                dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                                dgw.Rows[br].Cells["sifra"].Value = DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString();
                                dgw.Rows[br].Cells["naziv"].Value = roba.Rows[0]["naziv"].ToString();
                                dgw.Rows[br].Cells["jmj"].Value = roba.Rows[0]["jm"].ToString();
                                dgw.Rows[br].Cells["oduzmi"].Value = (roba.Columns.Contains("oduzmi") ? roba.Rows[0]["oduzmi"].ToString() : "");
                                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                dgw.Rows[br].Cells["kolicina"].Value = DTotpremnica_stavke.Rows[x]["kolicina"].ToString();
                                dgw.Rows[br].Cells["porez"].Value = DTotpremnica_stavke.Rows[x]["porez"].ToString();
                                dgw.Rows[br].Cells["rabat"].Value = DTotpremnica_stavke.Rows[x]["rabat"].ToString();
                                dgw.Rows[br].Cells["skladiste"].Value = DTotpremnica_stavke.Rows[x]["id_skladiste"].ToString();
                                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["vpc"]), 3) * (1 + ((Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["porez"]) + Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["porez_potrosnja"])) / 100)));
                                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTotpremnica_stavke.Rows[x]["nbc"].ToString());
                                dgw.Rows[br].Cells["id_stavka"].Value = DTotpremnica_stavke.Rows[x]["id_stavka"].ToString();
                                dgw.Rows[br].Cells["porez_potrosnja"].Value = DTotpremnica_stavke.Rows[x]["porez_potrosnja"].ToString();
                                dgw.Select();
                                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                                ControlDisableEnable(0, 1, 1, 0, 1);
                                edit = false;

                                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                                //a ne povratna naknada upisana u fakturi
                                if (DTotpremnica_stavke.Columns.Contains("povratna_naknada"))
                                {
                                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTotpremnica_stavke.Rows[x]["povratna_naknada"].ToString();
                                }
                                else
                                {
                                    dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                                }

                                if (header_1x)
                                {
                                    txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
                                    try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                    txtSifraFakturirati.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
                                    try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                    header_1x = false;
                                }
                                izracun();
                            }
                        }
                        catch { }
                    }
                }
            }*/
        }

        private void fillOtpremnice(string broj, string skladiste)
        {
            //fill header

            //--------fill otpremnica stavke----------------------------
            DataTable DTotpremnice = new DataTable();
            DataTable DTotpremnica_stavke = new DataTable();

            if (DTnaplaceneotpremnice.Columns.Contains("id") != true)
            {
                DTnaplaceneotpremnice.Columns.Add("id");
                DTnaplaceneotpremnice.Columns.Add("broj_otpremnice");
            }

            DataRow row;
            DataTable dtR = new DataTable();
            //DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];
            bool header_1x = !popunjena_s_otpremnicom;
            DTotpremnice = classSQL.select("SELECT * FROM otpremnice WHERE na_sobu = false and broj_otpremnice = '" + broj + "' AND id_skladiste = '" + skladiste + "' ", "otpremnice").Tables[0];

            if (DTotpremnice.Rows.Count != 0)
            {
                for (int z = 0; z < DTotpremnice.Rows.Count; z++)
                {
                    row = DTnaplaceneotpremnice.NewRow();
                    row["id"] = DTnaplaceneotpremnice.Rows.Count.ToString();
                    row["broj_otpremnice"] = DTotpremnice.Rows[z]["broj_otpremnice"];
                    DTnaplaceneotpremnice.Rows.Add(row);

                    //dtR = classSQL.select(s, "roba_prodaja").Tables[0];
                    try
                    {
                        DTotpremnica_stavke = classSQL.select("SELECT * FROM otpremnica_stavke  WHERE broj_otpremnice = '" + DTotpremnice.Rows[z]["broj_otpremnice"].ToString() + "' AND naplaceno_fakturom <> 'TRUE'", "otpremnice stavke").Tables[0];
                        for (int x = 0; x < DTotpremnica_stavke.Rows.Count; x++)
                        {
                            dgw.Rows.Add();
                            int br = dgw.Rows.Count - 1;
                            DataTable roba = classSQL.select("Select * from roba where sifra = '" + DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString() + "'", "roba").Tables[0];
                            dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                            dgw.Rows[br].Cells["sifra"].Value = DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString();
                            dgw.Rows[br].Cells["naziv"].Value = roba.Rows[0]["naziv"].ToString();
                            dgw.Rows[br].Cells["jmj"].Value = roba.Rows[0]["jm"].ToString();
                            dgw.Rows[br].Cells["oduzmi"].Value = roba.Rows[0]["oduzmi"].ToString();
                            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                            dgw.Rows[br].Cells["kolicina"].Value = DTotpremnica_stavke.Rows[x]["kolicina"].ToString();
                            dgw.Rows[br].Cells["porez"].Value = DTotpremnica_stavke.Rows[x]["porez"].ToString();
                            dgw.Rows[br].Cells["rabat"].Value = DTotpremnica_stavke.Rows[x]["rabat"].ToString();
                            dgw.Rows[br].Cells["skladiste"].Value = DTotpremnica_stavke.Rows[x]["id_skladiste"].ToString();
                            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                            dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["vpc"]), 3) * (1 + ((Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["porez"]) + Convert.ToDecimal(DTotpremnica_stavke.Rows[x]["porez_potrosnja"])) / 100)));
                            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTotpremnica_stavke.Rows[x]["nbc"].ToString());
                            dgw.Rows[br].Cells["id_stavka"].Value = DTotpremnica_stavke.Rows[x]["id_stavka"].ToString();
                            dgw.Rows[br].Cells["porez_potrosnja"].Value = DTotpremnica_stavke.Rows[x]["porez_potrosnja"].ToString();
                            dgw.Select();
                            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                            ControlDisableEnable(0, 1, 1, 0, 1);
                            edit = false;

                            if (DTotpremnica_stavke.Columns.Contains("povratna_naknada"))
                            {
                                dgw.Rows[br].Cells["povratna_naknada"].Value = DTotpremnica_stavke.Rows[x]["povratna_naknada"].ToString();
                            }
                            else
                            {
                                dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                            }

                            if (header_1x)
                            {
                                txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
                                try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                txtSifraFakturirati.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
                                try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                header_1x = false;
                            }
                            izracun();
                        }
                    }
                    catch { }
                }
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
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

        private void btnOdaberiOtpremnice_Click(object sender, EventArgs e)
        {
            if (txtSifraOdrediste.Text != "")
            {
                Sifarnik.frmOtpremnice form = new Sifarnik.frmOtpremnice();
                form.ShowDialog();
                if (form.confirmed && form.idList.Count > 0)
                {
                    otpremniceIdList = form.idList;
                    try
                    {
                        string inStatement = CreateInCondition(form.idList);
                        string query = $@"SELECT
	                                otpremnica_stavke.sifra_robe,
	                                roba.naziv,
	                                roba.jm,
	                                otpremnica_stavke.oduzmi,
	                                otpremnica_stavke.vpc,
	                                otpremnica_stavke.kolicina,
	                                otpremnica_stavke.porez,
	                                otpremnica_stavke.rabat,
	                                otpremnica_stavke.id_skladiste,
	                                otpremnica_stavke.nbc,
	                                otpremnica_stavke.id_stavka,
	                                otpremnica_stavke.porez_potrosnja
                                FROM otpremnice
                                JOIN otpremnica_stavke ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice
                                JOIN roba ON CAST(roba.sifra AS numeric) = CAST(otpremnica_stavke.sifra_robe AS numeric)
                                WHERE otpremnice.osoba_partner = {Properties.Settings.Default.id_partner} AND otpremnica_stavke.broj_otpremnice IN ({inStatement})
                                ORDER BY otpremnica_stavke.id_stavka ASC;";
                        DataTable dt = classSQL.select(query, "otpremnice").Tables[0];
                        FillGrid(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Partner nije odabran!", "Greška");
        }

        private void FillGrid(DataTable dataTable)
        {
            dgw.DataSource = null;
            dgw.Rows.Clear();
            for (int x = 0; x < dataTable.Rows.Count; x++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                dgw.Rows[br].Cells["sifra"].Value = dataTable.Rows[x]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dataTable.Rows[x]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dataTable.Rows[x]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dataTable.Rows[x]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.000}", dataTable.Rows[x]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = dataTable.Rows[x]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = dataTable.Rows[x]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = dataTable.Rows[x]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = dataTable.Rows[x]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.000}", dataTable.Rows[x]["vpc"]);
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(dataTable.Rows[x]["vpc"]), 3) * (1 + ((Convert.ToDecimal(dataTable.Rows[x]["porez"]) + Convert.ToDecimal(dataTable.Rows[x]["porez_potrosnja"])) / 100)));
                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dataTable.Rows[x]["nbc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = dataTable.Rows[x]["id_stavka"].ToString();
                dgw.Rows[br].Cells["porez_potrosnja"].Value = dataTable.Rows[x]["porez_potrosnja"].ToString();
            }
            izracun();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateInCondition(List<int> list, bool isString = false)
        {
            string inStatement = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!isString)
                {
                    inStatement += list[i].ToString();
                }
                else
                    inStatement += ("'" + list[i].ToString() + "'");

                if (i + 1 < list.Count)
                    inStatement += ",";
            }
            return inStatement;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateOtpremniceStatus()
        {
            try
            {
                string inStatement = CreateInCondition(otpremniceIdList);
                string query = $@"UPDATE otpremnice SET naplaceno_fakturom = 2 WHERE otpremnice.broj_otpremnice IN ({inStatement})";
                SetStavkeStatus();
                classSQL.update(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetStavkeStatus()
        {
            foreach (DataGridViewRow row in dgw.Rows)
            {
                try
                {
                    string idStavka = row.Cells["id_stavka"].Value.ToString();
                    string query = $@"UPDATE otpremnica_stavke SET naplaceno_fakturom = TRUE WHERE id_stavka = {idStavka}";
                    classSQL.update(query);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void SetNenaplaceno()
        {
            try
            {
                List<int> idList = new List<int>();
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(dgw.Rows[i].Cells["sifra"].Value.ToString());
                    if (!idList.Contains(id))
                        idList.Add(id);
                }

                if (idList.Count > 0)
                {
                    string inStatement = CreateInCondition(idList, true);
                    DataTable stavke = classSQL.select($@"SELECT broj_otpremnice FROM otpremnica_stavke WHERE sifra_robe IN ({inStatement}) GROUP BY broj_otpremnice", "otpremnica_stavke").Tables[0];
                    for (int i = 0; i < stavke.Rows.Count; i++)
                    {
                        classSQL.update($@"UPDATE otpremnice SET naplaceno_fakturom = 1 WHERE otpremnice.broj_otpremnice = {stavke.Rows[i]["broj_otpremnice"].ToString()}");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}