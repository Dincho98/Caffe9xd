using RESORT.Forme.Sifarnik;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmAddPartners : Form
    {
        public frmAddPartners()
        {
            InitializeComponent();
        }

        private bool edit = false;
        private bool preload = false;

        //public frmMenu MainFormMenu { get; set; }
        private void frmAddPartners_Load(object sender, EventArgs e)
        {
            txtSifra.Text = brojPartner();
            txtBrojKartice.Text = DateTime.Now.Year.ToString().Remove(0, 2) + brojKartice();
            SetCb();
            txtIme.Select();
            preload = true;
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
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
            DataTable DT = RemoteDB.select("SELECT * FROM zupanije ORDER BY naziv", "zupanije").Tables[0];
            txtZupanija.DataSource = DT;
            txtZupanija.DisplayMember = "naziv";
            txtZupanija.ValueMember = "id_zupanija";
            txtZupanija.SelectedValue = "8";

            DataTable DT1 = RemoteDB.select("SELECT * FROM djelatnosti ORDER BY ime_djelatnosti", "djelatnosti").Tables[0];
            txtDjelatnost.DataSource = DT1;
            txtDjelatnost.DisplayMember = "ime_djelatnosti";
            txtDjelatnost.ValueMember = "id_djelatnost";

            //CB grad
            DataSet DSgrad = RemoteDB.select("SELECT * FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2806";

            DataSet DSdrzava = RemoteDB.select("SELECT * FROM zemlja ORDER BY zemlja ", "zemlja");
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

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    xIme.Visible = true;
        //    xPrezime.Visible = true;
        //    xGrad.Visible = true;
        //    xAdresa.Visible = true;
        //    xTel.Visible = false;
        //    xmail.Visible = true;
        //    xOIB.Visible = false;
        //    xTvrtka.Visible = false;
        //}

        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    xIme.Visible = false;
        //    xPrezime.Visible = false;
        //    xGrad.Visible = true;
        //    xAdresa.Visible = true;
        //    xTel.Visible = true;
        //    xmail.Visible = true;
        //    xOIB.Visible = true;
        //    xTvrtka.Visible = true;
        //}

        private string brojPartner()
        {
            DataTable DSbr = RemoteDB.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];
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
            DataTable DSbr = RemoteDB.select("SELECT MAX(broj_kartice) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString("000000");
            }
            else
            {
                return Convert.ToDouble("1").ToString("000000");
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (rbPoslovni.Checked)
            {
                decimal dec_parse;
                if (!Decimal.TryParse(txtOib.Text, out dec_parse))
                {
                    // MessageBox.Show("Krivi unos za OIB.", "Greška"); return;
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
                if (txtEmail.Text == "")
                {
                    // MessageBox.Show("Krivi unos za email.", "Greška"); return;
                }
            }
            else if (rbPrivatni.Checked)
            {
                if (txtIme.Text == "")
                {
                    MessageBox.Show("Krivi unos za ime.", "Greška");
                    return;
                }
                if (txtPrezime.Text == "")
                {
                    MessageBox.Show("Krivi unos za prezime.", "Greška");
                    return;
                }
                if (txtAdresa.Text == "")
                {
                    MessageBox.Show("Krivi unos za adresu.", "Greška");
                    return;
                }
                if (txtEmail.Text == "")
                {
                    //MessageBox.Show("Krivi unos za email.", "Greška"); return;
                }

                if (txtTvrtka.Text == "")
                {
                    txtTvrtka.Text = txtIme.Text + " " + txtPrezime.Text;
                }
            }

            if (edit)
            {
                Update();
                edit = true;
            }
            else
            {
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

            if (chbAktivan.Checked)
            {
                aktivan = "1";
            }

            if (chbPrimanjeLetaka.Checked)
            {
                letak = "1";
            }

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            if (txtPopust.Text != "")
            {
                popust = "1";
            }

            if (txtBodovi.Text != "")
            {
                broj_bodova = "1";
            }

            if (txtBrojKartice.Text == "")
            {
                brojKarticeSTR = brojKartice();
            }

            string sql = "UPDATE partners SET " +
            "ime_tvrtke='" + txtTvrtka.Text + "'," +
            "id_grad='" + cbGrad.SelectedValue + "'," +
            "adresa='" + txtAdresa.Text + "'," +
            "oib='" + txtOib.Text + "'," +
            "napomena='" + rtbNapomena.Text + "'," +
            "id_djelatnost='" + txtDjelatnost.SelectedValue + "'," +
            "ime='" + txtIme.Text + "'," +
            "prezime='" + txtPrezime.Text + "'," +
            "email='" + txtEmail.Text + "'," +
            "tel='" + txtTel.Text + "'," +
            "mob='" + txtMob.Text + "'," +
            "datum_rodenja='" + dtpDatum.Value.ToString("yyyy-MM-dd") + "'," +
            "bodovi='" + broj_bodova + "'," +
            "popust='" + popust + "'," +
            "broj_kartice='" + brojKarticeSTR + "'," +
            "aktivan='" + aktivan + "'," +
            "vrsta_korisnika='" + vrsta_korisnika + "'," +
            "primanje_letaka='" + letak + "'," +
            "id_zupanija='" + txtZupanija.SelectedValue + "' WHERE id_partner='" + txtSifra.Text + "'" +
            "";

            //rtbNapomena.Text = sql;
            provjera_sql(RemoteDB.update(sql));
            MessageBox.Show("Spremljeno");
        }

        private void Spremi()
        {
            string aktivan = "0";
            string letak = "0";
            string vrsta_korisnika = "0";
            string broj_bodova = "0";
            string popust = "0";

            if (chbAktivan.Checked)
            {
                aktivan = "1";
            }

            if (chbPrimanjeLetaka.Checked)
            {
                letak = "1";
            }

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            if (txtPopust.Text != "")
            {
                popust = "1";
            }

            if (txtBodovi.Text != "")
            {
                broj_bodova = "1";
            }

            string sql = "INSERT INTO partners (id_partner,ime_tvrtke,id_grad,adresa,oib,napomena," +
            "id_djelatnost,ime,prezime,email,tel,mob,datum_rodenja,bodovi," +
            "popust,broj_kartice,aktivan,vrsta_korisnika,primanje_letaka,id_zupanija) VALUES (" +
            "'" + brojPartner() + "'," +
            "'" + txtTvrtka.Text + "'," +
            "'" + cbGrad.SelectedValue + "'," +
            "'" + txtAdresa.Text + "'," +
            "'" + txtOib.Text + "'," +
            "'" + rtbNapomena.Text + "'," +
            "'" + txtDjelatnost.SelectedValue + "'," +
            "'" + txtIme.Text + "'," +
            "'" + txtPrezime.Text + "'," +
            "'" + txtEmail.Text + "'," +
            "'" + txtTel.Text + "'," +
            "'" + txtMob.Text + "'," +
            "'" + dtpDatum.Value.ToString("yyyy-MM-dd") + "'," +
            "'" + broj_bodova + "'," +
            "'" + popust + "'," +
            "'" + brojKartice() + "'," +
            "'" + aktivan + "'," +
            "'" + vrsta_korisnika + "'," +
            "'" + letak + "'," +
            "'" + txtZupanija.SelectedValue + "'" +
            ")";

            //rtbNapomena.Text = sql;
            provjera_sql(RemoteDB.insert(sql));
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
            txtBodovi.Text = "";
            txtBrojKartice.Text = DateTime.Now.Year.ToString().Remove(0, 2) + brojKartice();
            txtEmail.Text = "";
            txtIme.Text = "";
            txtMob.Text = "";
            txtOib.Text = "";
            txtPopust.Text = "";
            txtPrezime.Text = "";
            txtSifra.Text = brojPartner();
            txtTel.Text = "";
            txtTvrtka.Text = "";
            edit = false;
        }

        private void EnableDisable(bool x)
        {
            txtAdresa.Enabled = x;
            txtBodovi.Enabled = x;
            txtEmail.Enabled = x;
            txtIme.Enabled = x;
            txtMob.Enabled = x;
            txtOib.Enabled = x;
            txtPopust.Enabled = x;
            txtPrezime.Enabled = x;
            txtTel.Enabled = x;
            txtTvrtka.Enabled = x;
            cbGrad.Enabled = x;

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
            DataTable DT = RemoteDB.select("SELECT * FROM partners WHERE id_partner='" + id + "'", "partners").Tables[0];
            txtAdresa.Text = DT.Rows[0]["adresa"].ToString();
            txtBodovi.Text = DT.Rows[0]["bodovi"].ToString();
            txtBrojKartice.Text = DT.Rows[0]["broj_kartice"].ToString();
            txtDjelatnost.SelectedValue = DT.Rows[0]["id_djelatnost"].ToString();
            txtEmail.Text = DT.Rows[0]["email"].ToString();
            txtIme.Text = DT.Rows[0]["ime"].ToString();
            txtMob.Text = DT.Rows[0]["mob"].ToString();
            txtOib.Text = DT.Rows[0]["oib"].ToString();
            txtPopust.Text = DT.Rows[0]["popust"].ToString();
            txtPrezime.Text = DT.Rows[0]["prezime"].ToString();
            txtSifra.Text = DT.Rows[0]["id_partner"].ToString();
            txtTel.Text = DT.Rows[0]["tel"].ToString();
            txtTvrtka.Text = DT.Rows[0]["ime_tvrtke"].ToString();
            txtZupanija.SelectedValue = DT.Rows[0]["id_zupanija"].ToString();
            cbGrad.SelectedValue = DT.Rows[0]["id_grad"].ToString();
            try
            {
                dtpDatum.Value = Convert.ToDateTime(DT.Rows[0]["datum_rodenja"].ToString());

                DataTable DTzemlja = RemoteDB.select("SELECT id_drzava FROM grad WHERE id_grad='" + DT.Rows[0]["id_grad"] + "'", "grad").Tables[0];

                cbDrzava.SelectedValue = (int)DTzemlja.Rows[0]["id_drzava"];
            }
            catch (Exception)
            {
            }
            if (DT.Rows[0]["aktivan"].ToString() == "1")
            {
                chbAktivan.Checked = true;
            }

            if (DT.Rows[0]["primanje_letaka"].ToString() == "1")
            {
                chbPrimanjeLetaka.Checked = true;
            }

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnableDisable(true);
            btnOdustani.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Forme.sifarnici.frmNovaDrzava nd = new Forme.sifarnici.frmNovaDrzava();
            nd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNoviGrad novigr = new frmNoviGrad();
            novigr.ShowDialog();

            //CB grad
            cbGrad.DataSource = null;
            DataSet DSgrad = RemoteDB.select("SELECT * FROM grad WHERE id_drzava = '" + cbDrzava.SelectedValue.ToString() + "' ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
        }

        private void cbDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preload == true)
            {
                try
                {
                    //CB grad
                    DataSet DSgrad1 = RemoteDB.select("SELECT * FROM grad WHERE id_drzava = '" + cbDrzava.SelectedValue + "' ORDER BY grad ", "grad");
                    cbGrad.DataSource = DSgrad1.Tables[0];
                    cbGrad.DisplayMember = "grad";
                    cbGrad.ValueMember = "id_grad";
                    //cbGrad.SelectedValue = "2806";
                }
                catch { }
            }
        }
    }
}