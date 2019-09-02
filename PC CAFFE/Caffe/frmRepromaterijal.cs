using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmRepromaterijal : Form
    {
        public frmRepromaterijal()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private int status_spremanja = 0;
        private bool SveOcitano = false;
        public frmMenu MainFormMenu { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnableDisable(false);
            SetCB();
            RepromaterijalSet(cbGrupe.SelectedValue.ToString());
            SveOcitano = true;
            PaintRows(dgwR);
            PaintRows(dgwA);
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void RepromaterijalSet(string grupa)
        {
            string query = "";
            if (grupa != "0")
            {
                query = "AND roba_prodaja.id_podgrupa='" + grupa + "'";
            }

            string sql = "SELECT " +
            "roba_prodaja.sifra," +
            "roba_prodaja.naziv," +
            "grupa.grupa," +
            "roba_prodaja.id_podgrupa," +
            "roba_prodaja.id_grupa," +
            "roba_prodaja.ulazni_porez," +
            "roba_prodaja.izlazni_porez," +
            "roba_prodaja.id_skladiste," +
            "roba_prodaja.nc," +
            "roba_prodaja.porez_potrosnja," +
            "roba_prodaja.povratna_naknada," +
            "roba_prodaja.poticajna_naknada," +
            "roba_prodaja.aktivnost," +
            "roba_prodaja.mjera" +
            " FROM roba_prodaja " +
            " LEFT JOIN grupa ON grupa.id_grupa=roba_prodaja.id_grupa" +
            " WHERE roba_prodaja.aktivnost is not null " + query + "ORDER BY roba_prodaja.naziv";
            DataTable DT = classSQL.select(sql, "repromaterijal").Tables[0];
            dgwR.DataSource = DT;

            dgwR.Columns["id_podgrupa"].Visible = false;
            dgwR.Columns["id_grupa"].Visible = false;
            dgwR.Columns["ulazni_porez"].Visible = false;
            dgwR.Columns["izlazni_porez"].Visible = false;
            dgwR.Columns["porez_potrosnja"].Visible = false;
            dgwR.Columns["povratna_naknada"].Visible = false;
            dgwR.Columns["poticajna_naknada"].Visible = false;
            dgwR.Columns["aktivnost"].Visible = false;
            dgwR.Columns["mjera"].Visible = false;
            dgwR.Columns["nc"].Visible = false;
            dgwR.Columns["id_skladiste"].Visible = false;
            //dgwR.Columns["porezna_grupa"].Visible = false;

            dgwR.Columns["sifra"].HeaderText = "Šifra";
            dgwR.Columns["naziv"].HeaderText = "Naziv";
            dgwR.Columns["grupa"].HeaderText = "Grupa";

            dgwR.Columns["sifra"].Width = 200;

            if (dgwR.Rows.Count > 0) { FillDGVr(0); }
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
            if (RowIndex < 0)
            {
                return;
            }

            decimal.TryParse(dgwR.Rows[RowIndex].Cells["izlazni_porez"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal izlazniPorez);
            decimal.TryParse(dgwR.Rows[RowIndex].Cells["ulazni_porez"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ulazniPorez);
            decimal.TryParse(dgwR.Rows[RowIndex].Cells["poticajna_naknada"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal poticajnaNaknada);
            decimal.TryParse(dgwR.Rows[RowIndex].Cells["porez_potrosnja"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal porezPotrosnja);
            decimal.TryParse(dgwR.Rows[RowIndex].Cells["povratna_naknada"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal povratnaNaknada);
            decimal.TryParse(dgwR.Rows[RowIndex].Cells["nc"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal nabavnaCijena);

            txtSifra.Text = dgwR.Rows[RowIndex].Cells["sifra"].FormattedValue.ToString();
            txtNaziv.Text = dgwR.Rows[RowIndex].Cells["naziv"].FormattedValue.ToString();
            cbGrupa.SelectedValue = dgwR.Rows[RowIndex].Cells["id_grupa"].FormattedValue.ToString();
            cbPodgrupa.SelectedValue = dgwR.Rows[RowIndex].Cells["id_podgrupa"].FormattedValue.ToString();
            txtIzlazniPorez.Text = izlazniPorez.ToString("#0.00");
            txtUlazniPorez.Text = ulazniPorez.ToString("#0.00");
            txtPoticajnaNaknada.Text = poticajnaNaknada.ToString("#0.00");
            txtMjera.Text = dgwR.Rows[RowIndex].Cells["mjera"].FormattedValue.ToString();
            txtPNP.Text = porezPotrosnja.ToString("#0.00");
            txtPovratnaNaknada.Text = povratnaNaknada.ToString("#0.00");
            cbSkladiste.SelectedValue = dgwR.Rows[RowIndex].Cells["id_skladiste"].FormattedValue.ToString();
            txtNabavnaCijena.Text = nabavnaCijena.ToString("#0.00");

            if (dgwR.Rows[RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "1")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }

            FillArtikle(dgwR.Rows[RowIndex].Cells["sifra"].FormattedValue.ToString());
        }

        private void dgwR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDGVr(e.RowIndex);
        }

        private void FillArtikle(string sifra_repro)
        {
            DataTable DTroba = classSQL.select(@"SELECT * FROM roba
                JOIN caffe_normativ ON caffe_normativ.sifra = roba.sifra
                WHERE caffe_normativ.sifra_normativ='" + sifra_repro + "'", "roba").Tables[0];

            dgwA.Rows.Clear();
            foreach (DataRow r in DTroba.Rows)
            {
                dgwA.Rows.Add(r["sifra"].ToString(), r["naziv"].ToString(), Convert.ToDecimal(r["mpc"].ToString()).ToString("#0.00"));
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
            DataTable DTskl = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DTskl;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //fill podgrupa

            //fill
            DataTable DT = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            DataRow DTrow = DT.NewRow();
            DTrow["id_podgrupa"] = "0";
            DTrow["naziv"] = "Prikaži sve";
            DT.Rows.Add(DTrow);

            cbGrupe.DataSource = DT;
            cbGrupe.DisplayMember = "naziv";
            cbGrupe.ValueMember = "id_podgrupa";
            cbGrupe.SelectedValue = "0";
        }

        private void cbGrupe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SveOcitano)
            {
                if (dgwA.Rows.Count > 0) { dgwA.Rows.Clear(); }
                //if (dgwR.Rows.Count > 0) { dgwR.Rows.Clear(); }
                RepromaterijalSet(cbGrupe.SelectedValue.ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            status_spremanja = 1;
            FieldClear();
            EnableDisable(true);
            txtSifra.Text = Broj_unosa();
            btnUredi.Enabled = false;
        }

        private string Broj_unosa()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(sifra AS INT)) FROM roba_prodaja", "roba_prodaja").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            RepromaterijalSet(cbGrupe.SelectedValue.ToString());
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
            txtNabavnaCijena.Enabled = x;
            txtNaziv.Enabled = x;
            txtPNP.Enabled = x;
            txtPoticajnaNaknada.Enabled = x;
            txtPovratnaNaknada.Enabled = x;
            txtUlazniPorez.Enabled = x;
            cbGrupa.Enabled = x;
            cbPodgrupa.Enabled = x;
            cbSkladiste.Enabled = x;
            txtSifra.Enabled = x;

            if (x == false)
            {
                dgwA.Enabled = true;
                dgwR.Enabled = true;
            }
            else if (x == true)
            {
                dgwA.Enabled = false;
                dgwR.Enabled = false;
            }
        }

        private void FieldClear()
        {
            txtSifra.Text = "";
            txtIzlazniPorez.Text = "0,00";
            txtMjera.Text = "";
            txtNabavnaCijena.Text = "0,00";
            txtNaziv.Text = "";
            txtPNP.Text = "0,00";
            txtPoticajnaNaknada.Text = "0,00";
            txtPovratnaNaknada.Text = "0,00";
            txtUlazniPorez.Text = "0,00";
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
            if (!Decimal.TryParse(txtIzlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisan izlazni porez.", "Greška"); return; }
            if (txtMjera.Text == "") { MessageBox.Show("Krivo upisana mjera.", "Greška"); return; }
            if (!Decimal.TryParse(txtNabavnaCijena.Text, out dec_parse)) { MessageBox.Show("Krivo upisana nabavna cijena.", "Greška"); return; }
            if (!Decimal.TryParse(txtPNP.Text, out dec_parse)) { MessageBox.Show("Krivo upisani porez pnp.", "Greška"); return; }
            if (!Decimal.TryParse(txtPoticajnaNaknada.Text, out dec_parse)) { MessageBox.Show("Krivo upisana poticajna naknada.", "Greška"); return; }
            if (!Decimal.TryParse(txtPovratnaNaknada.Text, out dec_parse)) { MessageBox.Show("Krivo upisana povratna naknada.", "Greška"); return; }
            if (!Decimal.TryParse(txtUlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisani ulazni porez.", "Greška"); return; }

            string sql = "INSERT INTO roba_prodaja (" +
                "id_skladiste,kolicina,nc,vpc,sifra,porez_potrosnja,id_grupa,id_podgrupa,mjera,aktivnost,povratna_naknada,poticajna_naknada,ulazni_porez,izlazni_porez,naziv,novo" +
                ") VALUES (" +
                    "'" + cbSkladiste.SelectedValue + "'," +
                    "'0'," +
                    "'" + txtNabavnaCijena.Text.Replace(",", ".") + "'," +
                    "'0'," +
                    "'" + txtSifra.Text + "'," +
                    "'" + txtPNP.Text + "'," +
                    "'" + cbGrupa.SelectedValue + "'," +
                    "'" + cbPodgrupa.SelectedValue + "'," +
                    "'" + txtMjera.Text + "'," +
                    "'" + aktivnost + "'," +
                    "'" + txtPovratnaNaknada.Text + "'," +
                    "'" + txtPoticajnaNaknada.Text + "'," +
                    "'" + txtUlazniPorez.Text + "'," +
                    "'" + txtIzlazniPorez.Text + "'," +
                    "'" + txtNaziv.Text + "','1'" +
                    ")";

            provjera_sql(classSQL.insert(sql));
            RepromaterijalSet(cbGrupe.SelectedValue.ToString());

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Novi repromaterijal, sifra." + txtSifra.Text + "')");

            status_spremanja = 0;
            EnableDisable(false);
            btnNoviUnos.Enabled = true;
            btnUredi.Enabled = true;
        }

        private void Update()
        {
            string aktivnost = "0";
            if (chbAktivnost.Checked)
            {
                aktivnost = "1";
            }

            decimal dec_parse;
            //if (Provjeri_Sifru()) { MessageBox.Show("Krivo upisama šifra.", "Greška"); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Krivo upisan naziv.", "Greška"); return; }
            if (!Decimal.TryParse(txtIzlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisan izlazni porez.", "Greška"); return; }
            if (txtMjera.Text == "") { MessageBox.Show("Krivo upisana mjera.", "Greška"); return; }
            if (!Decimal.TryParse(txtNabavnaCijena.Text, out dec_parse)) { MessageBox.Show("Krivo upisana nabavna cijena.", "Greška"); return; }
            if (!Decimal.TryParse(txtPNP.Text, out dec_parse)) { MessageBox.Show("Krivo upisani porez pnp.", "Greška"); return; }
            if (!Decimal.TryParse(txtPoticajnaNaknada.Text, out dec_parse)) { MessageBox.Show("Krivo upisana poticajna naknada.", "Greška"); return; }
            if (!Decimal.TryParse(txtPovratnaNaknada.Text, out dec_parse)) { MessageBox.Show("Krivo upisana povratna naknada.", "Greška"); return; }
            if (!Decimal.TryParse(txtUlazniPorez.Text, out dec_parse)) { MessageBox.Show("Krivo upisani ulazni porez.", "Greška"); return; }

            string sql = "UPDATE roba_prodaja SET " +
                " id_skladiste = '" + cbSkladiste.SelectedValue + "'," +
                " nc='" + txtNabavnaCijena.Text.Replace(",", ".") + "'," +
                " porez_potrosnja='" + txtPNP.Text + "'," +
                " id_grupa='" + cbGrupa.SelectedValue + "'," +
                " id_podgrupa='" + cbPodgrupa.SelectedValue + "'," +
                " mjera='" + txtMjera.Text + "'," +
                " aktivnost='" + aktivnost + "'," +
                " povratna_naknada='" + txtPovratnaNaknada.Text + "'," +
                " poticajna_naknada='" + txtPoticajnaNaknada.Text + "'," +
                " ulazni_porez='" + txtUlazniPorez.Text + "'," +
                " izlazni_porez='" + txtIzlazniPorez.Text + "'," +
                " editirano='1'," +
                " naziv='" + txtNaziv.Text + "' WHERE sifra='" + txtSifra.Text + "'" +
                "";

            provjera_sql(classSQL.update(sql));

            RepromaterijalSet(cbGrupe.SelectedValue.ToString());
            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje repromaterijala, sifra." + txtSifra.Text + "')");
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
                string count = classSQL.select("SELECT count(*) FROM roba_prodaja WHERE sifra='" + txtSifra.Text + "'", "roba").Tables[0].Rows[0][0].ToString();

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

        private void frmRepromaterijal_Resize(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void btnObrisiArtikl_Click(object sender, EventArgs e)
        {
            if (dgwR.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovaj normativ?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //DataTable DT = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra_normativ='" + txtSifra.Text + "'", "racun_stavke").Tables[0];

                    //if (DT.Rows.Count > 0)
                    //{
                    //    MessageBox.Show("Ovaj artikl ne možete obrisati jer je spojen sa prodajnim artiklom.");
                    //}
                    //else
                    //{
                    classSQL.delete("DELETE FROM roba_prodaja WHERE sifra='" + txtSifra.Text + "'");
                    classSQL.delete("DELETE FROM caffe_normativ WHERE sifra_normativ='" + txtSifra.Text + "'");
                    MessageBox.Show("Obrisano!");

                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                        " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje repromaterijala, šifre: " + txtSifra.Text + "')"));

                    //}
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