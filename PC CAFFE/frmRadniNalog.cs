using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRadniNalog : Form
    {
        public string broj_RN_edit { get; set; }
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DSRoba = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSMT = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTOtprema = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private DataTable DSponude = new DataTable();
        private DataTable DSFS = new DataTable();
        private BindingSource skladisteBindingSource = new BindingSource();
        private BindingSource RnBindingSource = new BindingSource();
        private bool edit = false;
        public frmMenu MainFormMenu { get; set; }

        public frmRadniNalog()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmRadniNalog_Load(object sender, EventArgs e)
        {
            ControlDisableEnable(0, 1, 1, 0, 1);
            numeric();
            fillComboBox();
            ttxBrojPonude.Text = brojRadnogNaloga();
            EnableDisable(false);
            ttxBrojPonude.Select();
            DGVCREATE();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            btnSpremi.Enabled = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            PaintRows(dgw);
            if (broj_RN_edit != null) { fillRN(); }
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

        private void DGVCREATE()
        {
            DataGridViewTextBoxColumn sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewComboBoxColumn skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nab_cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn iznos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // kalkulacijaDataGridView
            //
            this.dgw.AutoGenerateColumns = false;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            sifra,
            naziv,
            skladiste,
            kolicina,
            nab_cijena,
            iznos,
            mpc,
            iznos1,
            vpc,
            nbc,
            porez,id_stavka,oduzmi});

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            sifra.DataPropertyName = "sifra";
            sifra.HeaderText = "Šifra";
            sifra.Name = "sifra";
            sifra.ReadOnly = true;

            naziv.DataPropertyName = "naziv";
            naziv.HeaderText = "Naziv";
            naziv.Name = "naziv";
            naziv.ReadOnly = true;

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";
            //skladiste.FlatStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            //skladiste.DisplayStyle = FlatStyle.System;

            kolicina.DataPropertyName = "kolicina";
            kolicina.HeaderText = "Količina";
            kolicina.Name = "kolicina";

            nab_cijena.DataPropertyName = "nbc";
            nab_cijena.HeaderText = "Nabavna cijena";
            nab_cijena.Name = "nbc";
            nab_cijena.ReadOnly = true;

            iznos.DataPropertyName = "iznos";
            iznos.HeaderText = "Iznos";
            iznos.Name = "iznos";
            iznos.ReadOnly = true;

            mpc.DataPropertyName = "mpc";
            mpc.HeaderText = "MPC";
            mpc.Name = "mpc";
            mpc.ReadOnly = true;

            iznos1.DataPropertyName = "iznos1";
            iznos1.HeaderText = "Iznos";
            iznos1.Name = "iznos1";
            iznos1.ReadOnly = true;

            nbc.Visible = false;
            nbc.Name = "nbc";

            vpc.Visible = false;
            vpc.Name = "vpc";

            porez.Visible = false;
            porez.Name = "porez";

            id_stavka.Visible = false;
            id_stavka.Name = "id_stavka";

            oduzmi.Visible = false;
            oduzmi.Name = "oduzmi";
        }

        private void fillComboBox()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzvrsio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzvrsio.DisplayMember = "IME";
            cbIzvrsio.ValueMember = "id_zaposlenik";

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'rna' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill mjTroška
            DSMT = classSQL.select("SELECT * FROM mjesto_troska", "mjesto_troska");
            cbMjestoTroska.DataSource = DSMT.Tables[0];
            cbMjestoTroska.DisplayMember = "mjesto";
            cbMjestoTroska.ValueMember = "id_mjesto";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
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

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            cbMjestoTroska.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            dtpDatumNaloga.Enabled = x;
            dtpDatumPrimitka.Enabled = x;
            dtpDatumZavrsetka.Enabled = x;
            cbIzvrsio.Enabled = x;
            nmGodinaPonude.Enabled = x;
        }

        private void numeric()
        {
            nmGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaPonude.Value = 2012;
        }

        private string brojRadnogNaloga()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_naloga) FROM radni_nalog", "radni_nalog").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            btnSve.Enabled = false;
            ttxBrojPonude.Text = brojRadnogNaloga();
            btnDeleteAll.Enabled = false;
            btnSpremi.Enabled = true;
            ttxBrojPonude.Select();
            ttxBrojPonude.Enabled = false;
            nmGodinaPonude.Enabled = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSve.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojPonude.Text = brojRadnogNaloga();
            edit = false;
            btnDeleteAll.Enabled = false;
            btnSpremi.Enabled = false;
            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;
            ttxBrojPonude.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            dgw.Rows.Clear();
        }

        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            nmGodinaPonude.Select();
        }

        private void nmGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        {
            cbVD.Select();
        }

        private void btnSviRN_Click(object sender, EventArgs e)
        {
            frmSviRadniNalozi srn = new frmSviRadniNalozi();
            srn.sifra_rn = "";
            srn.MainForm = this;
            srn.ShowDialog();
            if (broj_RN_edit != null)
            {
                deleteFields();
                fillRN();
                EnableDisable(true);

                edit = true;
                btnDeleteAll.Enabled = true;
                btnSpremi.Enabled = true;
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumNaloga.Select();
            }
        }

        private void dtpDatumNaloga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumPrimitka.Select();
            }
        }

        private void dtpDatumPrimitka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumZavrsetka.Select();
            }
        }

        private void dtpDatumZavrsetka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbMjestoTroska.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraOdrediste.Select();
            }
        }

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                        txtIzradio.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void txtPartnerNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtIzradio.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbIzvrsio.Select();
            }
        }

        private void cbIzvrsio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
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
                    //dgw.Select();
                    //dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
                    ttxBrojPonude.Enabled = false;
                    nmGodinaPonude.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[2];
            dgw.BeginEdit(true);

            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["nbc"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()));
            dgw.Rows[br].Cells["iznos"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()));
            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString()));
            dgw.Rows[br].Cells["iznos1"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString()));
            dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()));
            dgw.Rows[br].Cells["porez"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString()));
            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

            PaintRows(dgw);
            izracun();
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

                if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }

                double kol = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
                double nbc = Convert.ToDouble(dgw.Rows[rowBR].Cells["nbc"].FormattedValue.ToString());
                double mpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString());

                double nbc_ukupno = nbc * kol;
                double mpc_sa_kolicinom = mpc * kol;

                dgw.Rows[rowBR].Cells["iznos"].Value = String.Format("{0:0.00}", nbc_ukupno);
                dgw.Rows[rowBR].Cells["iznos1"].Value = String.Format("{0:0.00}", mpc_sa_kolicinom);

                double u = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    u = Convert.ToDouble(dgw.Rows[i].Cells["iznos1"].FormattedValue.ToString()) + u;
                }

                textBox1.Text = "Ukupno sa PDV-om: " + String.Format("{0:0.00}", u);
            }
        }

        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        //{
        //    int row = dgw.CurrentCell.RowIndex;
        //    if (dgw.CurrentCell.ColumnIndex == 2)
        //    {
        //        SetCijenaSkladiste();
        //    }

        //    else if (dgw.CurrentCell.ColumnIndex == 6)
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
                        dgw.CurrentRow.Cells["nbc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["nc"]);
                        dgw.CurrentRow.Cells["mpc"].Value = String.Format("{0:0.00}", ((Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]) * Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["porez"]) / 100) + Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"])));
                        //lblNaDan.ForeColor = Color.Green;
                        //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        dgw.CurrentRow.Cells["nbc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["nc"]);
                        dgw.CurrentRow.Cells["mpc"].Value = String.Format("{0:0.00}", ((Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]) * Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["porez"]) / 100) + Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"])));
                        //lblNaDan.ForeColor = Color.Red;
                        //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                }
                else
                {
                    //lblNaDan.ForeColor = Color.Red;
                    //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
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

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null)
                {
                    MessageBox.Show("Faktura nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (txtSifraOdrediste.Text == "")
            {
                MessageBox.Show("Niste upisali šifru odredišta.", "Greška");
                return;
            }

            if (isNumeric(txtSifraOdrediste.Text, System.Globalization.NumberStyles.Integer) == false)
            {
                MessageBox.Show("Niste pravilno upisali šifru odredišta.", "Greška");
                return;
            }

            if (dgw.Rows.Count == 0)
            {
                MessageBox.Show("Za spremiti nemate niti jednu stavku.", "Greška");
                return;
            }

            if (edit == true)
            {
                UpdateRN();
                EnableDisable(false);
                deleteFields();

                ttxBrojPonude.Enabled = true;
                nmGodinaPonude.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                ttxBrojPonude.ReadOnly = false;
                return;
            }

            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;

            string sql = "INSERT INTO radni_nalog (broj_naloga,godina_naloga,datum_naloga,datum_primitka," +
                "zavrsna_kartica,mj_troska,id_narucioc,id_izradio,id_izvrsio,vrasta_dokumenta,napomena) " +
                " VALUES " +
                "(" +
                "'" + ttxBrojPonude.Text + "'," +
                    "'" + nmGodinaPonude.Value.ToString() + "'," +
                        "'" + dtpDatumNaloga.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'" + dtpDatumPrimitka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                "'" + dtpDatumZavrsetka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                "'" + cbMjestoTroska.SelectedValue + "'," +
                                "'" + txtSifraOdrediste.Text + "'," +
                                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                                "'" + cbIzvrsio.SelectedValue.ToString() + "'," +
                                "'" + cbVD.SelectedValue.ToString() + "'," +
                                "'" + rtbNapomena.Text + "'" +
                                ")";
            provjera_sql(classSQL.insert(sql));

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string vpc = dg(i, "vpc");
                string nbc = dg(i, "nbc");
                if (classSQL.remoteConnectionString == "")
                {
                    vpc = vpc.Replace(",", ".");
                    nbc = nbc.Replace(",", ".");
                }
                else
                {
                    vpc = vpc.Replace(".", ",");
                    nbc = nbc.Replace(".", ",");
                }

                string sql_stavke = "INSERT INTO radni_nalog_stavke (" +
                    "id_skladiste,sifra_robe,broj_naloga,vpc,nbc,naziv,kolicina,porez,oduzmi)" +
                    " VALUES (" +
                    "'" + dgw.Rows[i].Cells["skladiste"].Value + "'," +
                    "'" + dg(i, "sifra") + "'," +
                    "'" + ttxBrojPonude.Text + "'," +
                    "'" + vpc + "'," +
                    "'" + nbc + "'," +
                    "'" + dg(i, "naziv") + "'," +
                    "'" + dg(i, "kolicina") + "'," +
                    "'" + dg(i, "porez") + "'," +
                    "'" + dg(i, "oduzmi") + "'" +
                    ")";
                provjera_sql(classSQL.insert(sql_stavke));

                /////////////////////////////////////////////////////////////////////ovo uzima stavke sa skladista/////////////////////////////////

                DataTable DSn = new DataTable();
                string upit = "";
                string sql_update = "";

                upit = "SELECT roba_prodaja.vpc,roba_prodaja.porez,normativi.sifra_artikla,normativi_stavke.sifra_robe,roba_prodaja.nc,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                    "  FROM normativi_stavke " +
                    " LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                    " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe" +
                    " WHERE normativi.sifra_artikla='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                for (int x = 0; x < DSn.Rows.Count; x++)
                {
                    string sql_stavke_normativi = "INSERT INTO radni_nalog_normativ (" +
                    "id_skladiste,sifra,broj,vpc,nbc,kolicina,pdv)" +
                    " VALUES (" +
                    "'" + DSn.Rows[x]["id_skladiste"].ToString() + "'," +
                    "'" + DSn.Rows[x]["sifra_robe"].ToString() + "'," +
                    "'" + ttxBrojPonude.Text + "'," +
                    "'" + DSn.Rows[x]["vpc"].ToString() + "'," +
                    "'" + DSn.Rows[x]["nc"].ToString() + "'," +
                    "'" + DSn.Rows[x]["kolicina"].ToString() + "'," +
                    "'" + DSn.Rows[x]["porez"].ToString() + "'" +
                    ")";
                    provjera_sql(classSQL.insert(sql_stavke_normativi));

                    string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), DSn.Rows[x]["kolicina"].ToString(), dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), "-");
                    sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                    provjera_sql(classSQL.update(sql_update));
                }

                string k1 = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells["skladiste"].Value.ToString(), dg(i, "kolicina"), "1", "+");
                sql_update = "UPDATE roba_prodaja SET kolicina='" + k1 + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dgw.Rows[i].Cells["skladiste"].Value + "'";
                provjera_sql(classSQL.update(sql_update));
            }

            MessageBox.Show("Spremljeno");
            ttxBrojPonude.ReadOnly = false;
            edit = false;
            EnableDisable(false);
            deleteFields();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
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
                    SetRoba();
                    //dgw.Select();
                    //dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
                    ttxBrojPonude.Enabled = false;
                    nmGodinaPonude.Enabled = false;
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
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void fillRN()
        {
            //fill header

            deleteFields();

            DSponude = classSQL.select("SELECT * FROM radni_nalog WHERE broj_naloga = '" + broj_RN_edit + "'", "radni_nalog").Tables[0];

            cbVD.SelectedValue = DSponude.Rows[0]["vrasta_dokumenta"].ToString();
            txtSifraOdrediste.Text = DSponude.Rows[0]["id_narucioc"].ToString();
            txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_narucioc"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtSifraOdrediste.Text = DSponude.Rows[0]["id_narucioc"].ToString();
            rtbNapomena.Text = DSponude.Rows[0]["napomena"].ToString();
            dtpDatumNaloga.Value = Convert.ToDateTime(DSponude.Rows[0]["datum_naloga"].ToString());
            dtpDatumPrimitka.Value = Convert.ToDateTime(DSponude.Rows[0]["datum_primitka"].ToString());
            dtpDatumZavrsetka.Value = Convert.ToDateTime(DSponude.Rows[0]["zavrsna_kartica"].ToString());
            cbIzvrsio.SelectedValue = DSponude.Rows[0]["id_izvrsio"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSponude.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojPonude.Text = DSponude.Rows[0]["broj_naloga"].ToString();
            cbMjestoTroska.SelectedValue = DSponude.Rows[0]["mj_troska"].ToString();
            cbIzvrsio.SelectedValue = DSponude.Rows[0]["id_izvrsio"].ToString();

            //--------fill faktura stavke------------------------------

            DSFS = classSQL.select("SELECT * FROM radni_nalog_stavke WHERE broj_naloga = '" + broj_RN_edit + "'", "radni_nalog_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells["sifra"].Value = DSFS.Rows[i]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
                try { dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString(); } catch (Exception) { }
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["nbc"].Value = String.Format(DSFS.Rows[i]["nbc"].ToString());
                dgw.Rows[br].Cells["iznos"].Value = "0,00";
                dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", (Convert.ToDouble(DSFS.Rows[i]["vpc"].ToString()) * Convert.ToDouble(DSFS.Rows[i]["porez"].ToString()) / 100));
                dgw.Rows[br].Cells["iznos1"].Value = "0,00";
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["vpc"].Value = DSFS.Rows[i]["vpc"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = DSFS.Rows[i]["oduzmi"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                izracun();
                PaintRows(dgw);
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void UpdateRN()
        {
            string sql = "UPDATE radni_nalog SET " +
                " broj_naloga='" + ttxBrojPonude.Text + "'," +
                " godina_naloga='" + nmGodinaPonude.Value.ToString() + "'," +
                "'" + dtpDatumNaloga.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + dtpDatumPrimitka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + dtpDatumZavrsetka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " mj_troska='" + cbMjestoTroska.SelectedValue + "'," +
                " id_narucioc='" + txtSifraOdrediste.Text + "'," +
                " id_izvrsio='" + cbIzvrsio.SelectedValue + "'," +
                " napomena='" + rtbNapomena.Text + "'," +
                " vrasta_dokumenta='" + cbVD.SelectedValue + "'" +
                " WHERE broj_naloga='" + ttxBrojPonude.Text + "'";
            classSQL.update(sql);

            for (int b = 0; b < dgw.Rows.Count; b++)
            {
                string vpc = dg(b, "vpc");
                string nbc = dg(b, "nbc");
                if (classSQL.remoteConnectionString == "")
                {
                    vpc = vpc.Replace(",", ".");
                    nbc = nbc.Replace(",", ".");
                }
                else
                {
                    vpc = vpc.Replace(".", ",");
                    nbc = nbc.Replace(".", ",");
                }

                if (dgw.Rows[b].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dg(b, "id_stavka"));
                    sql = "UPDATE radni_nalog_stavke SET " +
                    " id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value + "'," +
                    " sifra_robe='" + dg(b, "sifra") + "'," +
                    " broj_naloga='" + ttxBrojPonude.Text + "'," +
                    " vpc='" + vpc + "'," +
                    " nbc='" + nbc + "'," +
                    " porez='" + dg(b, "porez") + "'," +
                    " naziv='" + dg(b, "naziv") + "'," +
                    " kolicina='" + dg(b, "kolicina") + "'," +
                    " oduzmi='" + dg(b, "oduzmi") + "'" +
                    " WHERE id_stavka='" + dg(b, "id_stavka") + "'";
                    provjera_sql(classSQL.update(sql));

                    /////////////////////////////////////////update roba kolicina///////////////////////////////////////
                    DataTable DSn = new DataTable();
                    string upit = "";
                    string sql_update = "";

                    upit = "SELECT roba_prodaja.vpc,roba_prodaja.porez,normativi.sifra_artikla,normativi_stavke.sifra_robe,roba_prodaja.nc,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                        "  FROM normativi_stavke " +
                        " LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                        " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe" +
                            " WHERE normativi.sifra_artikla='" + dgw.Rows[b].Cells["sifra"].FormattedValue.ToString() + "'";
                    DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                    if (dgw.Rows[b].Cells["skladiste"].Value.ToString() == dataROW[0]["id_skladiste"].ToString())
                    {
                        for (int x = 0; x < DSn.Rows.Count; x++)
                        {
                            string sql_stavke_normativi = "UPDATE radni_nalog_normativ SET kolicina='" + (Convert.ToDouble(DSn.Rows[x]["kolicina"].ToString()) * Convert.ToDouble(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString())) + "'";
                            provjera_sql(classSQL.insert(sql_stavke_normativi));

                            double kol_stara = Convert.ToDouble(dataROW[0]["kolicina"].ToString()) * Convert.ToDouble(DSn.Rows[x]["kolicina"].ToString());
                            double kol_nova = Convert.ToDouble(dg(b, "kolicina")) * Convert.ToDouble(DSn.Rows[x]["kolicina"].ToString());

                            string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), (kol_stara - kol_nova).ToString(), "1", "+");
                            sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                            provjera_sql(classSQL.update(sql_update));
                        }

                        string k_o_l = SQL.ClassSkladiste.GetAmount(dg(b, "sifra"), dgw.Rows[b].Cells["skladiste"].Value.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(b, "kolicina"))).ToString(), "1", "+");
                        provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina = '" + k_o_l + "' WHERE id_skladiste='" + dataROW[b]["id_skladiste"].ToString() + "'AND sifra='" + dataROW[b]["sifra_robe"].ToString() + "'"));
                    }
                    else
                    {
                        //////////////////////////////////////////////////////////////ako je promjenjeno skladiste//////////////

                        for (int x = 0; x < DSn.Rows.Count; x++)
                        {
                            string sql_stavke_normativi = "UPDATE radni_nalog_normativ SET kolicina='" + DSn.Rows[x]["kolicina"].ToString() + "'";
                            provjera_sql(classSQL.insert(sql_stavke_normativi));

                            double kol_stara = Convert.ToDouble(dataROW[0]["kolicina"].ToString()) * Convert.ToDouble(DSn.Rows[x]["kolicina"].ToString());
                            double kol_nova = Convert.ToDouble(dg(b, "kolicina")) * Convert.ToDouble(DSn.Rows[x]["kolicina"].ToString());

                            string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), (kol_stara - kol_nova).ToString(), "1", "+");
                            sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                            provjera_sql(classSQL.update(sql_update));
                        }

                        //dodaje u novo skladiste
                        string k_o_l = SQL.ClassSkladiste.GetAmount(dg(b, "sifra"), dgw.Rows[b].Cells["skladiste"].Value.ToString(), dg(b, "kolicina"), "1", "+");
                        provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina = '" + k_o_l + "' WHERE id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value.ToString() + "' AND sifra='" + dataROW[0]["sifra_robe"].ToString() + "'"));

                        //oduzima staro skladiste
                        k_o_l = SQL.ClassSkladiste.GetAmount(dg(b, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "-");
                        provjera_sql(classSQL.update("UPDATE roba_prodaja SET kolicina ='" + k_o_l + "' WHERE id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "' AND sifra='" + dataROW[0]["sifra_robe"].ToString() + "'"));
                    }
                }
                else
                {
                    string sql_stavke = "INSERT INTO radni_nalog_stavke (" +
                    "id_skladiste,sifra_robe,broj_naloga,vpc,nbc,naziv,kolicina,porez)" +
                    " VALUES (" +
                    "'" + dgw.Rows[b].Cells["skladiste"].Value + "'," +
                    "'" + dg(b, "sifra") + "'," +
                    "'" + ttxBrojPonude.Text + "'," +
                    "'" + vpc + "'," +
                    "'" + nbc + "'," +
                    "'" + dg(b, "naziv") + "'," +
                    "'" + dg(b, "kolicina") + "'," +
                    "'" + dg(b, "porez") + "'" +
                    ")";
                    provjera_sql(classSQL.insert(sql_stavke));

                    /////////////////////////////////////////////////////////////////////ovo uzima stavke sa skladista

                    DataTable DSn = new DataTable();
                    string upit = "";
                    string sql_update = "";

                    upit = "SELECT roba_prodaja.vpc,roba_prodaja.porez,normativi.sifra_artikla,normativi_stavke.sifra_robe,roba_prodaja.nc,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                        "  FROM normativi_stavke " +
                        " LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                        " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe" +
                    " WHERE normativi.sifra_artikla='" + dgw.Rows[b].Cells["sifra"].FormattedValue.ToString() + "'";
                    DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                    for (int x = 0; x < DSn.Rows.Count; x++)
                    {
                        string sql_stavke_normativi = "INSERT INTO radni_nalog_normativ (" +
                       "id_skladiste,sifra,broj,vpc,nbc,kolicina,pdv)" +
                       " VALUES (" +
                       "'" + DSn.Rows[x]["id_skladiste"].ToString() + "'," +
                       "'" + DSn.Rows[x]["sifra_robe"].ToString() + "'," +
                       "'" + ttxBrojPonude.Text + "'," +
                       "'" + DSn.Rows[x]["vpc"].ToString() + "'," +
                       "'" + DSn.Rows[x]["nc"].ToString() + "'," +
                       "'" + DSn.Rows[x]["kolicina"].ToString() + "'," +
                       "'" + DSn.Rows[x]["porez"].ToString() + "'" +
                       ")";
                        provjera_sql(classSQL.insert(sql_stavke_normativi));

                        string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), DSn.Rows[x]["kolicina"].ToString(), dgw.Rows[b].Cells["kolicina"].FormattedValue.ToString(), "-");
                        sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                        provjera_sql(classSQL.update(sql_update));
                    }

                    string k1 = SQL.ClassSkladiste.GetAmount(dg(b, "sifra"), dgw.Rows[b].Cells["skladiste"].Value.ToString(), dg(b, "kolicina"), "1", "+");
                    sql_update = "UPDATE roba_prodaja SET kolicina='" + k1 + "' WHERE sifra='" + dgw.Rows[b].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value + "'";
                    provjera_sql(classSQL.update(sql_update));
                }
            }

            MessageBox.Show("Spremljeno");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSve.Enabled = true;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");

                    DataTable DSn = new DataTable();
                    string upit = "";
                    string sql_update = "";

                    upit = "SELECT normativi.sifra_artikla,normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                    "  FROM normativi_stavke INNER JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                    " WHERE normativi.sifra_artikla='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'";
                    DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                    for (int x = 0; x < DSn.Rows.Count; x++)
                    {
                        classSQL.delete("DELETE FROM radni_nalog_normativ WHERE sifra='" + dgw.CurrentRow.Cells["sifra_artikla"].FormattedValue.ToString() + "' AND broj='" + ttxBrojPonude.Text + "'");

                        string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), DSn.Rows[x]["kolicina"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                        sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                        provjera_sql(classSQL.update(sql_update));
                    }

                    classSQL.delete("DELETE FROM radni_nalog_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "',','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje stavke sa radnog naloba br." + ttxBrojPonude.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllRN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                        skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        DataTable DSn = new DataTable();
                        string upit = "";
                        string sql_update = "";

                        upit = "SELECT normativi.sifra_artikla,normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                        "  FROM normativi_stavke INNER JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                        " WHERE normativi.sifra_artikla='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                        DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                        for (int x = 0; x < DSn.Rows.Count; x++)
                        {
                            string k = SQL.ClassSkladiste.GetAmount(DSn.Rows[x]["sifra_robe"].ToString(), DSn.Rows[x]["id_skladiste"].ToString(), DSn.Rows[x]["kolicina"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                            sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                            provjera_sql(classSQL.update(sql_update));
                        }

                        skl = skl - fa_kolicina;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                    }
                }

                classSQL.delete("DELETE FROM radni_nalog_normativ WHERE broj='" + ttxBrojPonude.Text + "'");
                classSQL.delete("DELETE FROM radni_nalog_stavke WHERE broj_naloga='" + ttxBrojPonude.Text + "'");
                classSQL.delete("DELETE FROM radni_nalog WHERE broj_naloga='" + ttxBrojPonude.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje cijelog radnog naloga br." + ttxBrojPonude.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAll.Enabled = false;
                btnObrisi.Enabled = false;
            }
        }

        private void btnSveStavke_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                frmStavkeRadnogNaloga frmst = new frmStavkeRadnogNaloga();
                frmst.sf_artikla = dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString();
                frmst.MainForm = this;
                frmst.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nemate niti jednu stavku za pogled.", "Greška");
            }
        }

        private void ttxBrojPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_naloga FROM radni_nalog WHERE broj_naloga='" + ttxBrojPonude.Text + "'", "radni_nalog").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojRadnogNaloga() == ttxBrojPonude.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveStavke.Enabled = false;
                        ttxBrojPonude.Text = brojRadnogNaloga();
                        btnDeleteAll.Enabled = false;
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
                    broj_RN_edit = ttxBrojPonude.Text;
                    fillRN();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAll.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojPonude.ReadOnly = true;
                    nmGodinaPonude.ReadOnly = true;
                    btnSpremi.Enabled = true;
                }
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 2 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "da")
            {
                SetCijenaSkladiste();
            }
            else if (dgw.CurrentCell.ColumnIndex == 3)
            {
                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "da")
                {
                    MessageBox.Show("Niste odabrali skladište", "Greška");
                    return;
                }

                dgw.CurrentCell.Selected = false;
                dgw.ClearSelection();
                txtSifra_robe.Text = "";
                txtSifra_robe.BackColor = Color.Silver;
                txtSifra_robe.Select();
            }

            if (MouseButtons != 0)
                return;

            if (dgw.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[3];
                    dgw.BeginEdit(true);
                }
                catch (Exception) { }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
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