using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmAddPartners : Form
    {
        public frmAddPartners()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private long karticaKupca = 0;
        private bool edit = false;
        public frmMenu MainFormMenu { get; set; }

        private void frmAddPartners_Load(object sender, EventArgs e)
        {
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
            label20.Text = "Hormonalni" + Environment.NewLine + "nadomjestak:";

            txtSifra.Text = brojPartner();
            txtBrojKartice.Text = brojKartice();
            txtKarticaKupca.Text = getUserCard();
            rbPoslovni.Checked = true;
            SetCb();
            preload = true;
            cmbSpol.SelectedIndex = 0;
            txtIme.Select();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

        private void SetCb()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2806";

            //CB drzava
            DataSet DSdrzava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja ", "zemlja");
            cbDrzava.DataSource = DSdrzava.Tables[0];
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "id_zemlja";
            cbDrzava.SelectedValue = "60";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //xIme.Visible = true;
            //xPrezime.Visible = true;
            //xGrad.Visible = true;
            //xAdresa.Visible = true;
            //xTel.Visible = false;
            //xmail.Visible = true;
            //xOIB.Visible = false;
            //xTvrtka.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //xIme.Visible = false;
            //xPrezime.Visible = false;
            //xGrad.Visible = true;
            //xAdresa.Visible = true;
            //xTel.Visible = true;
            //xmail.Visible = true;
            //xOIB.Visible = true;
            //xTvrtka.Visible = true;
        }

        private string brojPartner()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private string brojKartice()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_kartice) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString("000000");
            }
            else
            {
                return Convert.ToDouble("1").ToString("000000");
            }
        }

        private string getUserCard()
        {
            DateTime dTime = DateTime.Now;
            DataTable dt = classSQL.select("SELECT MAX(right(kartica_kupca, 8)::numeric) as kk FROM partners WHERE left(kartica_kupca, 2) = '" + dTime.Year.ToString().Substring(2) + "'", "partners").Tables[0];

            string rez = dTime.Year.ToString().Substring(2) + 1.ToString("00000000");

            if (dt != null)
            {
                if (dt.Rows[0]["kk"].ToString() != "")
                {
                    rez = dTime.Year.ToString().Substring(2) + Convert.ToInt64(dt.Rows[0]["kk"]).ToString("00000000");
                }
            }

            return rez;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (rbPoslovni.Checked)
            {
                decimal dec_parse;
                if (!Decimal.TryParse(txtOib.Text, out dec_parse))
                {
                    //MessageBox.Show("Krivi unos za OIB.", "Greška"); return;
                }

                if (txtTvrtka.Text == "")
                {
                    MessageBox.Show("Krivi unos za tvrtku.", "Greška");
                    return;
                }
                if (txtOib.Text == "")
                {
                    //MessageBox.Show("Krivi unos za OIB.", "Greška"); return;
                }
                if (txtAdresa.Text == "")
                {
                    MessageBox.Show("Krivi unos za adresu.", "Greška");
                    return;
                }
                //if (txtEmail.Text == "")
                //{
                //    MessageBox.Show("Krivi unos za email.", "Greška"); return;
                //}
            }
            else if (rbPrivatni.Checked)
            {
                //if (txtIme.Text == "")
                //{
                //    MessageBox.Show("Krivi unos za ime.", "Greška"); return;
                //}
                //if (txtPrezime.Text == "")
                //{
                //    MessageBox.Show("Krivi unos za prezime.", "Greška"); return;
                //}
                if (txtAdresa.Text == "")
                {
                    MessageBox.Show("Krivi unos za adresu.", "Greška");
                    return;
                }
                //if (txtEmail.Text == "")
                //{
                //    MessageBox.Show("Krivi unos za email.", "Greška"); return;
                //}

                if (txtTvrtka.Text == "")
                {
                    txtTvrtka.Text = txtIme.Text + " " + txtPrezime.Text;
                }
            }

            if (txtKarticaKupca.Text.Length > 0 && !Int64.TryParse(txtKarticaKupca.Text, out karticaKupca))
            {
                MessageBox.Show("Krivi unos za karticu kupca.");
                return;
            }

            DataTable dtKarticaKupca = classSQL.select("select * from partners where kartica_kupca = '-pero-';", "Kartica_kupca").Tables[0];

            try
            {
                dtKarticaKupca = classSQL.select("select * from partners where kartica_kupca = '" + karticaKupca + "';", "Kartica_kupca").Tables[0];
            }
            catch
            {
            }

            if (edit)
            {
                if (dtKarticaKupca != null && dtKarticaKupca.Rows.Count > 0 && dtKarticaKupca.Rows[0]["id_partner"].ToString() != txtSifra.Text)
                {
                    MessageBox.Show("Broj kartice kupca je već iskorišten.");
                }
                Update();
                edit = true;
            }
            else
            {
                if (dtKarticaKupca != null && dtKarticaKupca.Rows.Count > 0)
                {
                    MessageBox.Show("Broj kartice kupca je već iskorišten.");
                }

                Spremi();
                edit = true;
            }

            EnableDisable(false);
        }

        private void Update()
        {
            string brojKarticeSTR = "0";
            string aktivan = "0";
            string letak = "0";
            string vrsta_korisnika = "0";
            string broj_bodova = "0";
            string popust = "0";
            //int karticaKupca = 0;

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            if (txtBrojKartice.Text == "")
            {
                brojKarticeSTR = brojKartice();
            }

            string sql = "UPDATE partners SET " +
            " broj_kartice='" + brojKarticeSTR + "'," +
            " ime='" + txtIme.Text + "'," +
            " prezime='" + txtPrezime.Text + "'," +
            " ime_tvrtke='" + txtTvrtka.Text + "'," +
            " adresa='" + txtAdresa.Text + "'," +
            " id_zemlja = '" + cbDrzava.SelectedValue + "'," +
            " id_grad='" + cbGrad.SelectedValue + "'," +
            " oib='" + txtOib.Text + "'," +
            " tel='" + txtTel.Text + "'," +
            " mob='" + txtMob.Text + "'," +
            " email='" + txtEmail.Text + "'," +
            " napomena='" + txtNapomena.Text + "'," +
            " bodovi='" + broj_bodova + "'," +
            " popust='" + popust + "'," +
            " editirano='1'," +
            " aktivan='" + aktivan + "'," +
            " vrsta_korisnika='" + vrsta_korisnika + "',";
            if (karticaKupca != 0)
            {
                sql += " primanje_letaka='" + letak + "'," +
                " kartica_kupca = '" + karticaKupca + "',";
            }
            else
            {
                sql += " primanje_letaka='" + letak + "',";
            }

            sql += @"spol = '" + (cmbSpol.SelectedIndex == 0 ? "O" : (cmbSpol.SelectedIndex == 1 ? "M" : "Z")) + @"',
datum_rodenja = '" + dtpDatumRodenja.Value.ToString("yyyy-MM-dd HH:mm:ss") + @"',
trudnoca = '" + txtTrudnoca.Text + @"',
kontracepcija = '" + txtKontracepcija.Text + @"',
hormonalni_nadomjestak = '" + txtHormonalniNadomjestak.Text + @"'
WHERE id_partner='" + txtSifra.Text + "'" +
            "";

            //rtbNapomena.Text = sql;
            provjera_sql(classSQL.update(sql));
            karticaKupca = 0;
            MessageBox.Show("Spremljeno");
        }

        private void Spremi()
        {
            string aktivan = "0";
            string letak = "0";
            string vrsta_korisnika = "0";
            string broj_bodova = "0";
            string popust = "0";

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            string sql = "";
            if (karticaKupca == 0)
            {
                sql = "INSERT INTO partners (id_partner,ime_tvrtke,id_grad,adresa,oib,napomena," +
                "ime,prezime,email,tel,bodovi," +
                "popust,broj_kartice,aktivan,vrsta_korisnika,novo, primanje_letaka, spol, zanimanje, datum_rodenja, datum_upisa, trudnoca, kontracepcija, hormonalni_nadomjestak, id_zemlja, mob) VALUES (" +
                "'" + brojPartner() + "'," +
                "'" + txtTvrtka.Text + "'," +
                "'" + cbGrad.SelectedValue + "'," +
                "'" + txtAdresa.Text + "'," +
                "'" + txtOib.Text + "'," +
                "'" + txtNapomena.Text + "'," +
                "'" + txtIme.Text + "'," +
                "'" + txtPrezime.Text + "'," +
                "'" + txtEmail.Text + "'," +
                "'" + txtTel.Text + "'," +
                "'" + broj_bodova + "'," +
                "'" + popust + "'," +
                "'" + brojKartice() + "'," +
                "'" + aktivan + "'," +
                "'" + vrsta_korisnika + "'," +
                "'1'," +
                "'" + letak + "'," +
                "'" + (cmbSpol.SelectedIndex == 0 ? "O" : (cmbSpol.SelectedIndex == 1 ? "M" : "Z")) + "'," +
                "'" + "" + "'," +
                "'" + dtpDatumRodenja.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + txtTrudnoca.Text + "'," +
                "'" + txtKontracepcija.Text + "'," +
                "'" + txtHormonalniNadomjestak.Text + "'," +
                "'" + cbDrzava.SelectedValue + "'," +
                "'" + txtMob.Text + "'" +
                ")";
            }
            else
            {
                sql = "INSERT INTO partners (id_partner,ime_tvrtke,id_grad,adresa,oib,napomena," +
                    "ime,prezime,email,tel,bodovi," +
                    "popust,broj_kartice,aktivan,vrsta_korisnika,novo,primanje_letaka, kartica_kupca, spol, zanimanje, datum_rodenja, datum_upisa, trudnoca, kontracepcija, hormonalni_nadomjestak, id_zemlja, mob) VALUES (" +
                    "'" + brojPartner() + "'," +
                    "'" + txtTvrtka.Text + "'," +
                    "'" + cbGrad.SelectedValue + "'," +
                    "'" + txtAdresa.Text + "'," +
                    "'" + txtOib.Text + "'," +
                    "'" + txtNapomena.Text + "'," +
                    "'" + txtIme.Text + "'," +
                    "'" + txtPrezime.Text + "'," +
                    "'" + txtEmail.Text + "'," +
                    "'" + txtTel.Text + "'," +
                    "'" + broj_bodova + "'," +
                    "'" + popust + "'," +
                    "'" + brojKartice() + "'," +
                    "'" + aktivan + "'," +
                    "'" + vrsta_korisnika + "'," +
                    "'1'," +
                    "'" + letak + "'," +
                    "'" + karticaKupca + "'," +
                    "'" + (cmbSpol.SelectedIndex == 0 ? "O" : (cmbSpol.SelectedIndex == 1 ? "M" : "Z")) + "'," +
                "'" + "" + "'," +
                "'" + dtpDatumRodenja.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + txtTrudnoca.Text + "'," +
                "'" + txtKontracepcija.Text + "'," +
                "'" + txtHormonalniNadomjestak.Text + "'," +
                "'" + cbDrzava.SelectedValue + "'," +
                "'" + txtMob.Text + "'" +
                    ")";
            }
            //rtbNapomena.Text = sql;
            provjera_sql(classSQL.insert(sql));
            karticaKupca = 0;
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
            txtAdresa.Text = "";
            txtBrojKartice.Text = brojKartice();
            txtEmail.Text = "";
            txtIme.Text = "";
            txtOib.Text = "";
            txtPrezime.Text = "";
            txtSifra.Text = brojPartner();
            txtTel.Text = "";
            txtTvrtka.Text = "";
            txtKarticaKupca.Text = "";
            karticaKupca = 0;
            txtMob.Text = "";
            txtNapomena.Text = "";
            cmbSpol.SelectedIndex = 0;
            txtTrudnoca.Text = "";
            txtKontracepcija.Text = "";
            txtHormonalniNadomjestak.Text = "";
            dtpDatumRodenja.Value = DateTime.Now;
            edit = false;
        }

        private void EnableDisable(bool x)
        {
            txtAdresa.Enabled = x;
            txtEmail.Enabled = x;
            txtIme.Enabled = x;
            txtOib.Enabled = x;
            txtPrezime.Enabled = x;
            txtMob.Enabled = x;
            txtTel.Enabled = x;
            txtTvrtka.Enabled = x;
            cbGrad.Enabled = x;
            cbDrzava.Enabled = x;
            cmbSpol.Enabled = x;
            txtNapomena.Enabled = x;
            txtTrudnoca.Enabled = x;
            txtKontracepcija.Enabled = x;
            txtHormonalniNadomjestak.Enabled = x;
            dtpDatumRodenja.Enabled = x;

            if (x == true) { button1.Visible = false; } else { button1.Visible = true; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partner = new frmPartnerTrazi();
            partner.ShowDialog();

            if (Properties.Settings.Default.id_partner != "")
            {
                FillPartner(Properties.Settings.Default.id_partner);
                edit = true;
                EnableDisable(true);
            }
        }

        private void FillPartner(string id)
        {
            DataTable DT = classSQL.select("SELECT * FROM partners WHERE id_partner='" + id + "'", "partners").Tables[0];

            txtSifra.Text = DT.Rows[0]["id_partner"].ToString();
            txtBrojKartice.Text = DT.Rows[0]["broj_kartice"].ToString();
            txtIme.Text = DT.Rows[0]["ime"].ToString();
            txtPrezime.Text = DT.Rows[0]["prezime"].ToString();
            txtTvrtka.Text = DT.Rows[0]["ime_tvrtke"].ToString();
            txtAdresa.Text = DT.Rows[0]["adresa"].ToString();
            cbDrzava.SelectedValue = (DT.Rows[0]["id_zemlja"].ToString().Length == 0 ? 60.ToString() : DT.Rows[0]["id_zemlja"].ToString());
            cbGrad.SelectedValue = DT.Rows[0]["id_grad"].ToString();
            txtOib.Text = DT.Rows[0]["oib"].ToString();
            txtTel.Text = DT.Rows[0]["tel"].ToString();
            txtMob.Text = DT.Rows[0]["mob"].ToString();
            txtEmail.Text = DT.Rows[0]["email"].ToString();
            txtKarticaKupca.Text = DT.Rows[0]["kartica_kupca"].ToString().Length > 0 ? DT.Rows[0]["kartica_kupca"].ToString() : "";

            if (DT.Rows[0]["vrsta_korisnika"].ToString() == "1")
            {
                rbPoslovni.Checked = true;
                rbPrivatni.Checked = false;
            }
            else
            {
                rbPoslovni.Checked = false;
                rbPrivatni.Checked = true;
            }

            txtNapomena.Text = DT.Rows[0]["napomena"].ToString();
            dtpDatumRodenja.Value = Convert.ToDateTime(DT.Rows[0]["datum_rodenja"].ToString().Length == 0 ? DateTime.Now.ToString() : DT.Rows[0]["datum_rodenja"].ToString());
            cmbSpol.SelectedIndex = (DT.Rows[0]["spol"].ToString() == "O" ? 0 : (DT.Rows[0]["spol"].ToString() == "M" ? 1 : 2));
            txtTrudnoca.Text = DT.Rows[0]["trudnoca"].ToString();
            txtKontracepcija.Text = DT.Rows[0]["kontracepcija"].ToString();
            txtHormonalniNadomjestak.Text = DT.Rows[0]["hormonalni_nadomjestak"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnableDisable(true);
            btnOdustani.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sifarnik.frmDodajZemlju zemlje = new Sifarnik.frmDodajZemlju();
            zemlje.ShowDialog();

            //CB drzava
            cbDrzava.DataSource = null;
            DataSet DSdrzava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja ", "zemlja");
            cbDrzava.DataSource = DSdrzava.Tables[0];
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "id_zemlja";
            cbDrzava.SelectedValue = "60";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNoviGrad novigr = new frmNoviGrad();
            novigr.ShowDialog();

            //CB grad
            cbGrad.DataSource = null;
            DataSet DSgrad = classSQL.select("SELECT * FROM grad WHERE id_drzava = '" + cbDrzava.SelectedValue.ToString() + "' ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
        }

        private bool preload = false;

        private void cbDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preload == true)
            {
                try
                {
                    if (cbDrzava.SelectedValue != null)
                    {
                        //CB grad
                        DataSet DSgrad1 = classSQL.select("SELECT * FROM grad WHERE id_drzava = '" + cbDrzava.SelectedValue.ToString() + "' ORDER BY grad ", "grad");
                        cbGrad.DataSource = DSgrad1.Tables[0];
                        cbGrad.DisplayMember = "grad";
                        cbGrad.ValueMember = "id_grad";
                        //cbGrad.SelectedValue = "2806";
                    }
                }
                catch { }
            }
        }

        private void frmAddPartners_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
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