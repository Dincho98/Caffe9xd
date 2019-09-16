using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmProdajniArtikli : Form
    {
        public frmMenu MainForm { get; set; }
        private int status_spremanja = 0;
        private bool SveOcitano = false;
        public string btnPromjenaIzgledaNaziv = null;

        public frmProdajniArtikli()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmProdajniArtikli_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            EnableDisable(false);
            SetCB();
            ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());
            SveOcitano = true;
            PaintRows(dgwR);
            PaintRows(dgwA);
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            //OnemoguciOdredjenePDVove();
        }


        //-----------Zadatak: Prisiliti korisnika ako je u sustavu PDVa da ne stavi 0% PDV.---------------//
        //Funkcionalnost ostaje ista kao i do sad, znači PDV se upisuje u txtIzlazniPorez.
        //Upisuje se tako da mi kliknemo na 0,5,13 ili 25% te se taj broj upisuje u txtIzlazniPorez.
        //------------------------------------------------------------------------------------------------//
        //Ova metoda služi kako bi se provjerilo nalazi li se tvrtka u sustavu PDVa.
        private bool TvrtkaJeUSustavuPDVa()
        {
            bool tvrtkaJeUSustavuPDVa = false;
            DataTable DataTablePostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            tvrtkaJeUSustavuPDVa=DTpostavke.Rows[0]["sustav_pdv"].ToString()=="1"?true:false;
            return tvrtkaJeUSustavuPDVa;
        }

        //Ova metoda onemogućuje klik na 5,13,25% ako firma nije u sustavu PDVa.
        //Ova metoda onemogućuje klik na 0% ako je firma u sustavu PDVa.
        private void OnemoguciOdredjenePDVove()
        {
            if (!TvrtkaJeUSustavuPDVa())
            {
                checkBoxZeroPercent.Enabled = true;
                checkBoxFivePercent.Enabled = false;
                checkBoxThirteenPercent.Enabled = false;
                checkBoxTrentyFivePercent.Enabled = false;
            }
            else
            {
                checkBoxZeroPercent.Enabled = false;
                checkBoxFivePercent.Enabled = true;
                checkBoxThirteenPercent.Enabled = true;
                checkBoxTrentyFivePercent.Enabled = true;
            }
        }

        /// <summary>
        /// Event handlers
        /// </summary>
        //Treba imati na umu da UVIJEK smije biti uključen samo 1 checkBox!
        private void checkBoxZeroPercent_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBoxZeroPercent.Checked)
            {
                KvaciceNaCheckBoxovima(true, false, false, false);
                txtIzlazniPorez.Text = "0";
            }
            else
            {
                txtIzlazniPorez.Text = "";
            }
        }

        private void checkBoxFivePercent_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBoxFivePercent.Checked)
            {
                KvaciceNaCheckBoxovima(false, true, false, false);
                txtIzlazniPorez.Text = "5";
            }
            else
            {
                txtIzlazniPorez.Text = "";
            }
        }

        private void checkBoxThirteenPercent_MouseClick(object sender, MouseEventArgs e)
        {

            if (checkBoxThirteenPercent.Checked)
            {
                KvaciceNaCheckBoxovima(false, false, true, false);
                txtIzlazniPorez.Text = "13";
            }
            else
            {
                txtIzlazniPorez.Text = "";
            }
        }

        private void checkBoxTrentyFivePercent_Click(object sender, EventArgs e)
        {

            if (checkBoxTrentyFivePercent.Checked)
            {
                KvaciceNaCheckBoxovima(false, false, false, true);
                txtIzlazniPorez.Text = "25";
            }
            else
            {
                txtIzlazniPorez.Text = "";
            }
        }

        private void KvaciceNaCheckBoxovima(bool cb0, bool cb5, bool cb13, bool cb25)
        {
            checkBoxZeroPercent.Checked = cb0;
            checkBoxFivePercent.Checked = cb5;
            checkBoxThirteenPercent.Checked = cb13;
            checkBoxTrentyFivePercent.Checked = cb25;
        }
        //-----------------------------------------------------------------------------//

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void CreateButton(string ime, string border_color)
        {
            btnGrupa.Text = ime;
            btnGrupa.BackColor = System.Drawing.Color.Transparent;
            btnGrupa.ForeColor = Color.White;
            btnGrupa.BackgroundImage = Image.FromFile("btnArtikli.png");
            btnGrupa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnGrupa.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            btnGrupa.FlatAppearance.BorderSize = 0;
            btnGrupa.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            btnGrupa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnGrupa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnGrupa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGrupa.Font = new System.Drawing.Font("Arial", 9.5F);
            btnGrupa.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            btnGrupa.TabIndex = 2;
            btnGrupa.UseVisualStyleBackColor = false;

            if (border_color != "" && border_color != "0")
            {
                if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Bez boje")
                {
                    btnGrupa.FlatAppearance.BorderSize = 0;
                    bezBoje.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Crno")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Black.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Bijelo")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.White;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    White.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Plavo")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Blue.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Zeleno")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Green;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Green.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Žuto")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Yellow.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Forest Green")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    ForestGreen.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "SkyBlue")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    SkyBlue.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Sivo")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Gray.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Violet")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Violet;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Violet.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Mangeta")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Magenta;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Mangenta.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Medium purple")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.MediumPurple;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    MediumPurple.Checked = true;
                }
                else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].FormattedValue.ToString() == "Aqua")
                {
                    btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
                    btnGrupa.FlatAppearance.BorderSize = 5;
                    Aqua.Checked = true;
                }
            }

            if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "standard")
            {
                btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnBordo")
            {
                btnGrupa.BackgroundImage = Properties.Resources.bordo;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnTamno_crvena")
            {
                btnGrupa.BackgroundImage = Properties.Resources.crveno_crna;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnLjubicasta")
            {
                btnGrupa.BackgroundImage = Properties.Resources.ljubicasto;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnNarandasta")
            {
                btnGrupa.BackgroundImage = Properties.Resources.naranda;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnPlavo")
            {
                btnGrupa.BackgroundImage = Properties.Resources.plavo;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnSivo")
            {
                btnGrupa.BackgroundImage = Properties.Resources.sivo;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnTamno_sivo")
            {
                btnGrupa.BackgroundImage = Properties.Resources.tamno_siva;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnNebo")
            {
                btnGrupa.BackgroundImage = Properties.Resources.vodena;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnZeleno")
            {
                btnGrupa.BackgroundImage = Properties.Resources.zelena;
            }
            else if (dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].FormattedValue.ToString() == "btnZuto")
            {
                btnGrupa.BackgroundImage = Properties.Resources.zuto;
            }
            else
            {
                btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
            }
        }

        private void ProdajniArtikliSet(string grupa)
        {
            string query = "";
            if (grupa != "0")
            {
                query = "AND roba.id_grupa='" + grupa + "'";
            }

            string sql = "SELECT " +
            "roba.sifra," +
            "roba.naziv," +
            "grupa.grupa," +
            "roba.id_grupa," +
            "roba.mpc," +
            "roba.ean," +
            "roba.border_color," +
            "roba.button_style," +
            "roba.porez," +
            "roba.porez_potrosnja," +
            "roba.aktivnost," +
            "roba.id_podgrupa," +
            "roba.porezna_grupa," +
            "roba.jm" +
            " FROM roba " +
            " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
            " WHERE roba.aktivnost is not null " + query + "ORDER BY roba.naziv";
            DataTable DT = classSQL.select(sql, "roba").Tables[0];
            dgwR.DataSource = DT;

            dgwR.Columns["button_style"].Visible = false;
            dgwR.Columns["border_color"].Visible = false;
            dgwR.Columns["id_grupa"].Visible = false;
            dgwR.Columns["porez_potrosnja"].Visible = false;
            dgwR.Columns["porez"].Visible = false;
            dgwR.Columns["aktivnost"].Visible = false;
            dgwR.Columns["jm"].Visible = false;
            dgwR.Columns["ean"].Visible = false;
            dgwR.Columns["mpc"].Visible = false;
            dgwR.Columns["porezna_grupa"].Visible = false;
            dgwR.Columns["id_podgrupa"].Visible = false;
            dgwR.Columns["sifra"].HeaderText = "Šifra";
            dgwR.Columns["naziv"].HeaderText = "Naziv";
            dgwR.Columns["grupa"].HeaderText = "Grupa";

            dgwR.Columns["sifra"].Width = 100;

            if (dgwR.Rows.Count > 0) { FillDGVr(0); }

            PaintRows(dgwR);
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

        private void FillDGVr(int RowIndex)
        {
            if (RowIndex != -1)
            {
                txtSifra.Text = dgwR.Rows[RowIndex].Cells["sifra"].FormattedValue.ToString();
                txtNaziv.Text = dgwR.Rows[RowIndex].Cells["naziv"].FormattedValue.ToString();
                cbGrupa.SelectedValue = dgwR.Rows[RowIndex].Cells["id_grupa"].FormattedValue.ToString();
                txtMjera.Text = dgwR.Rows[RowIndex].Cells["jm"].FormattedValue.ToString();
                txtPNP.Text = Convert.ToDecimal(dgwR.Rows[RowIndex].Cells["porez_potrosnja"].FormattedValue.ToString()).ToString("#0.00");
                txtCijena.Text = dgwR.Rows[RowIndex].Cells["mpc"].FormattedValue.ToString();
                txtEan.Text = dgwR.Rows[RowIndex].Cells["ean"].FormattedValue.ToString();
                txtIzlazniPorez.Text = dgwR.Rows[RowIndex].Cells["porez"].FormattedValue.ToString();
                switch (txtIzlazniPorez.Text)
                {
                    case "0":
                        KvaciceNaCheckBoxovima(true, false, false, false);
                        break;
                    case "5":
                        KvaciceNaCheckBoxovima(false, true, false, false);
                        break;
                    case "13":
                        KvaciceNaCheckBoxovima(false, false, true, false);
                        break;
                    case "25":
                        KvaciceNaCheckBoxovima(false, false, false, true);
                        break;
                }

                if (dgwR.Rows[RowIndex].Cells["id_podgrupa"].FormattedValue.ToString() != "")
                {
                    cbPodgrupa.SelectedValue = dgwR.Rows[RowIndex].Cells["id_podgrupa"].FormattedValue.ToString();
                }
                else
                {
                    cbPodgrupa.SelectedValue = 1;
                }

                if (dgwR.Rows[RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "1")
                {
                    chbAktivnost.Checked = true;
                }
                else
                {
                    chbAktivnost.Checked = false;
                }

                try
                {
                    cbPoreznaGrupa.SelectedValue = dgwR.Rows[RowIndex].Cells["porezna_grupa"].FormattedValue.ToString();
                }
                catch
                {
                }

                CreateButton(txtNaziv.Text, dgwR.Rows[RowIndex].Cells["border_color"].FormattedValue.ToString());
                FillRepromaterijal(txtSifra.Text);
            }
        }

        private void dgwR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDGVr(e.RowIndex);
        }

        private void FillRepromaterijal(string sifra)
        {
            string sql = "SELECT skladiste.skladiste,caffe_normativ.kolicina,caffe_normativ.sifra,caffe_normativ.sifra_normativ FROM caffe_normativ " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=caffe_normativ.id_skladiste" +
                " WHERE caffe_normativ.sifra='" + sifra + "' ORDER BY id_stavka ASC";

            DataTable DT = classSQL.select(sql, "caffe_normativ").Tables[0];
            dgwA.Rows.Clear();
            foreach (DataRow rowNor in DT.Rows)
            {
                DataTable DTroba = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + rowNor["sifra_normativ"].ToString() + "'", "roba").Tables[0];

                foreach (DataRow r in DTroba.Rows)
                {
                    decimal.TryParse(rowNor["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rowKolicina);
                    dgwA.Rows.Add(r["sifra"].ToString(),
                        r["naziv"].ToString(),
                        rowKolicina.ToString().Replace('.', ','),
                        rowNor["skladiste"].ToString());
                }
            }
        }

        private void SetCB()
        {
            //fill grupa
            DataTable DTgrupa = classSQL.select("SELECT * FROM grupa", "otprema").Tables[0];
            cbGrupa.DataSource = DTgrupa;
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //fill podgrupa
            DataTable DTpodgrupa = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            cbPodgrupa.DataSource = DTpodgrupa;
            cbPodgrupa.DisplayMember = "naziv";
            cbPodgrupa.ValueMember = "id_podgrupa";

            //fill podgrupa
            //DataTable DTskl = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            //cbSkladiste.DataSource = DTskl;
            //cbSkladiste.DisplayMember = "skladiste";
            //cbSkladiste.ValueMember = "id_skladiste";

            DataTable DTpp = new DataTable();
            DTpp.Columns.Add("id");
            DTpp.Columns.Add("naziv");

            DTpp.Rows.Add("V", "Vino");
            DTpp.Rows.Add("Z", "Žestoka i alkoholna pica");
            DTpp.Rows.Add("P", "Pivo");
            DTpp.Rows.Add("B", "Bezalkoholna pića");
            DTpp.Rows.Add("O", "Ostalo");

            cbPoreznaGrupa.DataSource = DTpp;
            cbPoreznaGrupa.DisplayMember = "naziv";
            cbPoreznaGrupa.ValueMember = "id";

            //fill
            DataTable DT = classSQL.select("SELECT * FROM grupa", "grupa").Tables[0];
            DataRow DTrow = DT.NewRow();
            DTrow["id_grupa"] = "0";
            DTrow["grupa"] = "Prikaži sve";
            DT.Rows.Add(DTrow);

            cbGrupe.DataSource = DT;
            cbGrupe.DisplayMember = "grupa";
            cbGrupe.ValueMember = "id_grupa";
            cbGrupe.SelectedValue = "0";
        }

        private void cbGrupe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SveOcitano)
            {
                if (dgwA.Rows.Count > 0) { dgwA.Rows.Clear(); }
                //if (dgwR.Rows.Count > 0) { dgwR.Rows.Clear(); }
                ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            status_spremanja = 1;
            FieldClear();
            EnableDisable(true);
            KvaciceNaCheckBoxovima(false, false, false, false);
           // OnemoguciOdredjenePDVove();
            btnUredi.Enabled = false;
            txtSifra.Text = Broj_unosa();
            txtCijena.Text = "0,00";
            txtPNP.Text = "0,00";
            txtIzlazniPorez.Text = "";
            txtEan.Text = "0,00";
            chbAktivnost.Checked = true;
            FillRepromaterijal(txtSifra.Text);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());
            EnableDisable(false);
            PaintRows(dgwR);
            PaintRows(dgwA);
            btnUredi.Enabled = true;
            btnNoviUnos.Enabled = true;
            status_spremanja = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void EnableDisable(bool x)
        {
            txtIzlazniPorez.Enabled = x;
            txtMjera.Enabled = x;
            txtNaziv.Enabled = x;
            txtPNP.Enabled = x;
            cbGrupa.Enabled = x;
            cbPodgrupa.Enabled = x;
            //cbSkladiste.Enabled = x;
            txtSifra.Enabled = x;
            txtEan.Enabled = x;
            txtCijena.Enabled = x;
            dgwA.Enabled = x;
            btnObrisiNormativ.Enabled = x;
            btnDodajNoviArtikl.Enabled = x;
            cbPoreznaGrupa.Enabled = x;

            checkBoxZeroPercent.Enabled = x;
            checkBoxFivePercent.Enabled = x;
            checkBoxThirteenPercent.Enabled = x;
            checkBoxTrentyFivePercent.Enabled = x;

            if (x == false)
            {
                dgwR.Enabled = true;
            }
            else if (x == true)
            {
                dgwR.Enabled = false;
            }
        }

        private void FieldClear()
        {
            txtSifra.Text = "";
            txtIzlazniPorez.Text = "";
            txtMjera.Text = "";
            txtNaziv.Text = "";
            txtPNP.Text = "";
            txtEan.Text = "";
            txtCijena.Text = "";
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {

            if (status_spremanja == 1)
            {
                Spremi();
            }
            else if (status_spremanja == 2)
            {
                Update();
            }

            classSQL.update("UPDATE caffe_normativ SET editirano='1'");
        }

        private void ProvjeraPoreza()
        {

        }

        private void Spremi()
        {
            string aktivnost = "0";
            if (chbAktivnost.Checked)
            {
                aktivnost = "1";
            }

            decimal dec_parse;
            if (Provjeri_Sifru()) { MessageBox.Show("Krivo upisama šifra.", "Greška"); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Krivo upisan naziv.", "Greška"); return; }

            if (txtIzlazniPorez.Text == "") { MessageBox.Show("Morate odabrati porez!","Greška"); return;}
            if (TvrtkaJeUSustavuPDVa() && checkBoxZeroPercent.Checked)
            {
                MessageBox.Show("Vaša tvrtka nalazi se u sustavu PDVa. Porez može biti 5%, 13% ili 25%. Ukoliko smatrate da je došlo do pogreške, nazovite Code-IT.", "Greška");
                return;
            }

            if (!TvrtkaJeUSustavuPDVa() && !checkBoxZeroPercent.Checked)
            {
                MessageBox.Show("Vaša tvrtka ne nalazi se u sustavu PDVa. Porez može biti samo 0%. Ukoliko smatrate da je došlo do pogreške, nazovite Code-IT.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtIzlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisan izlazni porez.", "Greška"); return; }
            if (txtMjera.Text == "") { MessageBox.Show("Krivo upisana mjera.", "Greška"); return; }
            if (!Decimal.TryParse(txtPNP.Text, out dec_parse)) { MessageBox.Show("Krivo upisani porez pnp.", "Greška"); return; }
            if (!Decimal.TryParse(txtCijena.Text, out dec_parse)) { MessageBox.Show("Krivo upisana cijena.", "Greška"); return; }

            string sql = "INSERT INTO roba (" +
                "naziv,id_grupa,jm,mpc,sifra,ean,porez,porez_potrosnja,aktivnost,porezna_grupa,id_podgrupa,novo" +
                ") VALUES (" +
                    "'" + txtNaziv.Text + "'," +
                    "'" + cbGrupa.SelectedValue + "'," +
                    "'" + txtMjera.Text.Replace(",", ".") + "'," +
                    "'" + txtCijena.Text.Replace(".", ",") + "'," +
                    "'" + txtSifra.Text + "'," +
                    "'" + txtEan.Text + "'," +
                    "'" + txtIzlazniPorez.Text + "'," +
                    "'" + txtPNP.Text + "'," +
                    "'" + aktivnost + "'," +
                    "'" + cbPoreznaGrupa.SelectedValue.ToString() + "'," +
                    "'" + cbPodgrupa.SelectedValue.ToString() + "','1'" +
                    ");";

            provjera_sql(classSQL.insert(sql));

            for (int i = 0; i < dgwA.Rows.Count; i++)
            {
                if (!Decimal.TryParse(dgwA.Rows[i].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    MessageBox.Show("Krivo upisana količina za normativ. U ovo polje upisuju se samo brojevi.", "Greška");
                    dgwA.Rows[i].Cells["kolicina"].Value = "0";
                }

                sql = "UPDATE caffe_normativ SET kolicina='" + dgwA.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "',editirano='1' " +
                    " WHERE sifra='" + dgwR.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' " +
                    " AND sifra_normativ='" + dgwA.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                //provjera_sql(classSQL.update(sql));
            }

            ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Novi prodajni artikl po šifri: " + txtSifra.Text + "')"));

            status_spremanja = 0;
            EnableDisable(false);
            btnNoviUnos.Enabled = true;
            btnUredi.Enabled = true;
        }

        private void Update()
        {
            string aktivnost = "0";
            string selectedSifra = dgwR.Rows[dgwR.SelectedRows[0].Index].Cells["sifra"].FormattedValue.ToString();
            if (chbAktivnost.Checked)
            {
                aktivnost = "1";
            }

            decimal dec_parse;
            //if (Provjeri_Sifru()) { MessageBox.Show("Krivo upisana šifra.", "Greška"); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Krivo upisan naziv.", "Greška"); return; }

            if (txtIzlazniPorez.Text == "") { MessageBox.Show("Morate odabrati porez!", "Greška"); return; }
            if (TvrtkaJeUSustavuPDVa() && checkBoxZeroPercent.Checked)
            {
                MessageBox.Show("Vaša tvrtka nalazi se u sustavu PDVa. Porez može biti 5%, 13% ili 25%. Ukoliko smatrate da je došlo do pogreške, nazovite Code-IT.", "Greška");
                return;
            }

            if (!TvrtkaJeUSustavuPDVa() && !checkBoxZeroPercent.Checked)
            {
                MessageBox.Show("Vaša tvrtka ne nalazi se u sustavu PDVa. Porez može biti samo 0%. Ukoliko smatrate da je došlo do pogreške, nazovite Code-IT.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtIzlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisan izlazni porez.", "Greška"); return; }
            if (txtMjera.Text == "") { MessageBox.Show("Krivo upisana mjera.", "Greška"); return; }
            if (!Decimal.TryParse(txtPNP.Text, out dec_parse)) { MessageBox.Show("Krivo upisani porez pnp.", "Greška"); return; }
            if (!Decimal.TryParse(txtCijena.Text, out dec_parse)) { MessageBox.Show("Krivo upisana cijena.", "Greška"); return; }

            string sql = "UPDATE roba SET " +
                    " naziv='" + txtNaziv.Text + "'," +
                    " id_grupa='" + cbGrupa.SelectedValue + "'," +
                    " jm='" + txtMjera.Text + "'," +
                    " mpc='" + txtCijena.Text.Replace(".", ",") + "'," +
                    " ean='" + txtEan.Text + "'," +
                    " porez='" + txtIzlazniPorez.Text + "'," +
                    " porez_potrosnja='" + txtPNP.Text + "'," +
                    " id_podgrupa='" + cbPodgrupa.SelectedValue + "'," +
                    " editirano='1'," +
                    " porezna_grupa='" + cbPoreznaGrupa.SelectedValue + "'," +
                    " aktivnost='" + aktivnost + "' WHERE  sifra='" + txtSifra.Text + "'" +
                    "";

            provjera_sql(classSQL.update(sql));

            for (int i = 0; i < dgwA.Rows.Count; i++)
            {
                if (!Decimal.TryParse(dgwA.Rows[i].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    MessageBox.Show("Krivo upisana količina za normativ. U ovo polje upisuju se samo brojevi.", "Greška");
                    dgwA.Rows[i].Cells["kolicina"].Value = "0";
                }

                // dgwR.Rows[i].Cells["sifra"].FormattedValue.ToString()
                sql = "UPDATE caffe_normativ SET kolicina='" + dgwA.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'" +
                    " WHERE sifra='" + selectedSifra + "' " +
                    " AND sifra_normativ='" + dgwA.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                provjera_sql(classSQL.update(sql));
            }

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Promjena prodajnog artikla po šifri: " + txtSifra.Text + "')"));

            ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());
            status_spremanja = 0;
            EnableDisable(false);
            btnNoviUnos.Enabled = true;
            btnUredi.Enabled = true;
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

        private void btnUredi_Click(object sender, EventArgs e)
        {
            status_spremanja = 2;
            EnableDisable(true);
            txtSifra.Enabled = false;
            btnNoviUnos.Enabled = false;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            Provjeri_Sifru();
        }

        private string Broj_unosa()
        {
            DataTable DSbr = classSQL.select("SELECT COUNT(sifra) FROM roba", "roba").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                bool ne_postoji = true;
                int brojac = 1;
                string vrati = "";
                while (ne_postoji)
                {
                    DataTable DT = classSQL.select("SELECT sifra FROM roba WHERE sifra='" + (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + brojac).ToString() + "'", "roba").Tables[0];
                    if (DT.Rows.Count == 0)
                    {
                        vrati = (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + brojac).ToString();
                        ne_postoji = false;
                    }
                    else
                    {
                        brojac++;
                    }
                }
                return vrati;
            }
            else
            {
                return "1";
            }
        }

        private bool Provjeri_Sifru()
        {
            if (txtSifra.Text != "")
            {
                if (txtSifra.Text.Length > 2)
                {
                    if (txtSifra.Text.Substring(0, 3) == "000")
                    {
                        MessageBox.Show("Početak šifra ne smije sadržavati više od dvije nule.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSifra.Text = "";
                        return true;
                    }
                }
                string count = classSQL.select("SELECT count(*) FROM roba WHERE sifra='" + txtSifra.Text + "'", "roba").Tables[0].Rows[0][0].ToString();

                if (count == "0")
                {
                    txtSifra.BackColor = Color.Azure;
                    return false;
                }
                else
                {
                    txtSifra.BackColor = Color.MistyRose;
                    return true;
                }
            }
            else
            {
                txtSifra.BackColor = Color.MistyRose;
                return true;
            }
        }

        private void btnDodajNoviArtikl_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Greška.\r\nNiste upisali šifru!", "Greška");
                return;
            }
            Caffe.frmDodajNormativ dn = new frmDodajNormativ();
            dn.sifra_prodajnog_artikla = txtSifra.Text;
            dn.ShowDialog();
            FillRepromaterijal(txtSifra.Text);
        }

        private void btnObrisiNormativ_Click(object sender, EventArgs e)
        {
            if (dgwA.Rows.Count > 0)
            {
                try
                {
                    string SQL = "DELETE FROM caffe_normativ " +
                         " WHERE sifra='" + txtSifra.Text + "' " +
                         " AND sifra_normativ='" + dgwA.Rows[dgwA.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.delete(SQL);

                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                     " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje normativa prema po prodajnoj šifri: " + txtSifra.Text + " i po normativu: " + dgwA.Rows[dgwA.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "')"));

                    FillRepromaterijal(txtSifra.Text);

                    MessageBox.Show("Obrisali ste normativ s prodajnog artikla.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void frmProdajniArtikli_Resize(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void dgwA_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwA.CurrentCell.ColumnIndex == 2)
            {
                decimal kkol = 0;
                if (!decimal.TryParse(dgwA.Rows[dgwA.CurrentRow.Index].Cells["kolicina"].FormattedValue.ToString(), out kkol))
                {
                    MessageBox.Show("Krivi unos za količinu", "UPOZORENJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                provjera_sql(classSQL.update("UPDATE caffe_normativ SET kolicina='" + kkol.ToString() + "' WHERE " +
                 "sifra_normativ='" + dgwA.Rows[dgwA.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND " +
                 "sifra='" + txtSifra.Text + "'"));

                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Promjena normativa za prodajnu šifru: " + txtSifra.Text + " i za normativ " + dgwA.Rows[dgwA.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "')"));
            }
        }

        private int RecLineChars;
        private string tekst = "";
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        private void btnIspis_Click(object sender, EventArgs e)
        {
            tekst = "";
            RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());

            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            PrintTextLine("Adresa: " + DT_tvr.Rows[0]["adresa"].ToString());
            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
            PrintTextLine(new string('-', RecLineChars));

            DataTable DTroba = classSQL.select("SELECT * FROM roba ORDER BY naziv", "roba").Tables[0];

            for (int i = 0; i < DTroba.Rows.Count; i++)
            {
                PrintText(TruncateAt(DTroba.Rows[i]["sifra"].ToString().PadRight(6), 6));
                PrintText(TruncateAt(DTroba.Rows[i]["naziv"].ToString().PadRight(24), 24));
                PrintTextLine(TruncateAt(Convert.ToDouble(DTroba.Rows[i]["mpc"].ToString()).ToString("#0.00").PadLeft(8), 8));
            }
            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            PosPrint.classPrint.printaj(tekst);
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private void Green_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton Rbutton = (RadioButton)sender;

            if (Rbutton.Text == "Bez boje")
            {
                btnGrupa.FlatAppearance.BorderSize = 0;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Crno")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Bijelo")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.White;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Plavo")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Zeleno")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Green;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Žuto")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Forest Green")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "SkyBlue")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Sivo")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Violet")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Violet;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Mangeta")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Magenta;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Medium purple")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.MediumPurple;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
            else if (Rbutton.Text == "Aqua")
            {
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
                btnGrupa.FlatAppearance.BorderSize = 5;
                classSQL.update("UPDATE roba SET border_color='" + Rbutton.Text + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["border_color"].Value = Rbutton.Text;
            }
        }

        private INIFile ini = new INIFile();

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("iniPostavke.ini"))
                {
                    File.WriteAllText("iniPostavke.ini", "[OPCE]");
                }

                DataTable DT = classSQL.select("SELECT COUNT(id_roba) FROM roba WHERE button_style NOT IN ('standard') ", "roba").Tables[0];
                if (DT.Rows.Count > 0)
                {
                    string aa = ini.Read("OPCE", "btn_style");
                    bool bool_buttonStyle = ini.Read("OPCE", "btn_style") == "1" ? true : false;
                    if (DT.Rows[0][0].ToString() == "5" && !bool_buttonStyle)
                    {
                        // return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Caffe.frmOdabirGumba og = new frmOdabirGumba();
            og._PrArtikli = this;
            btnPromjenaIzgledaNaziv = null;
            og.ShowDialog();

            if (btnPromjenaIzgledaNaziv != null)
            {
                dgwR.Rows[dgwR.CurrentRow.Index].Cells["button_style"].Value = btnPromjenaIzgledaNaziv;
                classSQL.update("UPDATE roba SET button_style='" + btnPromjenaIzgledaNaziv + "' WHERE sifra='" + dgwR.Rows[dgwR.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");

                if (btnPromjenaIzgledaNaziv == "standard")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
                }
                else if (btnPromjenaIzgledaNaziv == "btnBordo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.bordo;
                }
                else if (btnPromjenaIzgledaNaziv == "btnTamno_crvena")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.crveno_crna;
                }
                else if (btnPromjenaIzgledaNaziv == "btnLjubicasta")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.ljubicasto;
                }
                else if (btnPromjenaIzgledaNaziv == "btnNarandasta")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.naranda;
                }
                else if (btnPromjenaIzgledaNaziv == "btnPlavo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.plavo;
                }
                else if (btnPromjenaIzgledaNaziv == "btnSivo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.sivo;
                }
                else if (btnPromjenaIzgledaNaziv == "btnTamno_sivo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.tamno_siva;
                }
                else if (btnPromjenaIzgledaNaziv == "btnNebo")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.vodena;
                }
                else if (btnPromjenaIzgledaNaziv == "btnZeleno")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.zelena;
                }
                else if (btnPromjenaIzgledaNaziv == "btnZuto")
                {
                    btnGrupa.BackgroundImage = Properties.Resources.zuto;
                }
                else
                {
                    btnGrupa.BackgroundImage = Properties.Resources.btnArtikli;
                }
            }
        }

        private void btnObrisiArtikl_Click(object sender, EventArgs e)
        {
            if (dgwR.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati artikl?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable DT = classSQL.select("SELECT * FROM racun_stavke WHERE sifra_robe='" + txtSifra.Text + "'", "racun_stavke").Tables[0];

                    if (DT.Rows.Count > 0)
                    {
                        MessageBox.Show("Ovaj artikl ne možete obrisati jer je korišten u prodaji.");
                    }
                    else
                    {
                        classSQL.delete("DELETE FROM roba WHERE sifra='" + txtSifra.Text + "'");
                        MessageBox.Show("Obrisano!");
                        ProdajniArtikliSet(cbGrupe.SelectedValue.ToString());

                        provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                            " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje prodajne šifre: " + txtSifra.Text + "')"));
                    }
                }
            }
        }

        private void frmProdajniArtikli_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, false, true, true, true, true, true, false, false, false);
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

    }
}