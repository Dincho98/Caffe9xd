using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmNaplata : Form
    {
        private int _id_zaposlenik;
        public int id_zaposlenik { get { return _id_zaposlenik; } }

        public frmNaplata(int zaposlenik = 0)
        {
            if (zaposlenik == 0)
                int.TryParse(Properties.Settings.Default.id_zaposlenik, out zaposlenik);

            _id_zaposlenik = zaposlenik;
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string stol { get; set; }
        public string racun_ili_stol { get; set; }
        public double ukupni_iznos { get; set; }
        public bool naplata_sve { get; set; }
        public string R1 { get; set; }
        public string nacin_placanja = "G";
        public decimal vraceniIznos = 0;

        private bool hasKartice = false;
        private DataTable DT_postavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private bool zavrseno = false;

        private void frmNaplata_Load(object sender, EventArgs e)
        {
            zavrseno = false;
            naplata_sve = racun_ili_stol.Contains("STOL");
            racun_ili_stol = racun_ili_stol.Replace("STOL", "");
            SetValue();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            cbNacin.Select();
        }

        private void SetValue()
        {
            try
            {
                string sql = @"select id, naziv
from kartice
where aktivnost = '1'
order by naziv;";

                DataSet dsKartice = classSQL.select(sql, "kartice");
                if (dsKartice != null && dsKartice.Tables.Count > 0 && dsKartice.Tables[0] != null && dsKartice.Tables[0].Rows.Count > 0)
                {
                    cmbKartice.DisplayMember = "naziv";
                    cmbKartice.ValueMember = "id";
                    cmbKartice.DataSource = dsKartice.Tables[0];
                    hasKartice = true;
                }
                else
                {
                    hasKartice = false;
                }

                if (R1 != null && R1 != "")
                {
                    DataTable DDTP = classSQL.select(string.Format(@"SELECT id_partner, ime_tvrtke FROM partners WHERE id_partner = '{0}';", R1), "partners").Tables[0];

                    if (DDTP.Rows.Count > 0)
                    {
                        lblKupac.Text = "Partner R1: " + DDTP.Rows[0]["ime_tvrtke"].ToString();
                    }
                }

                sql = string.Format(@"select id_zaposlenik as id, concat(ime, ' ', prezime) as zaposlenik
from zaposlenici
where aktivan = 'DA' and prezime != 'PC'
order by zaposlenik;");

                DataSet dsZaposlenik = classSQL.select(sql, "zaposlenici");

                if (dsZaposlenik != null && dsZaposlenik.Tables.Count > 0 && dsZaposlenik.Tables[0] != null && dsZaposlenik.Tables[0].Rows.Count > 0)
                {
                    cmbDjelatnik.ValueMember = "id";
                    cmbDjelatnik.DisplayMember = "zaposlenik";
                    cmbDjelatnik.DataSource = dsZaposlenik.Tables[0];
                    cmbDjelatnik.SelectedValue = Properties.Settings.Default.id_zaposlenik;
                }
                else
                {
                    cmbDjelatnik.Enabled = false;
                    cmbDjelatnik.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataTable DTSK1 = new DataTable("nacin");
            DTSK1.Columns.Add("id_nacin", typeof(string));
            DTSK1.Columns.Add("nacin", typeof(string));
            if (File.Exists("hamer"))
            {
                DTSK1.Rows.Add("1", "Gotovina");
            }
            else
            {
                string[] nacini_placanja = DT_postavke.Rows[0]["nacini_placanja"].ToString().Split(';');

                if (nacini_placanja[0] == "1")
                {
                    DTSK1.Rows.Add("1", "Gotovina");
                }
                if (nacini_placanja[1] == "1")
                {
                    DTSK1.Rows.Add("2", "Kartice");
                }
                if (nacini_placanja[3] == "1")
                {
                    DTSK1.Rows.Add("4", "Ostalo" + (Class.PodaciTvrtka.oibTvrtke == "98816793336"? " - Foodex" : ""));
                }

                //if (Class.Postavke.is_beauty)
                //{
                //    DTSK1.Rows.Add("5", "Bon");
                //}
            }

            cbNacin.DataSource = DTSK1;
            cbNacin.DisplayMember = "nacin";
            cbNacin.ValueMember = "id_nacin";

            if (naplata_sve == true)
            {
                string sql = string.Format(@"SELECT
racuni.id_stol,
SUM(racuni.ukupno) AS ukupno,
stolovi.naziv
FROM racuni
LEFT JOIN stolovi ON racuni.id_stol = stolovi.id_stol
WHERE racuni.id_stol = '{0}' AND ukupno_gotovina = '0' AND ukupno_kartice = '0'
AND racuni.id_kasa = '{1}' AND racuni.id_ducan = '{2}'
GROUP BY racuni.id_stol,stolovi.naziv;",
                    racun_ili_stol, Util.Korisno.idKasa, Util.Korisno.idDucan);

                DataTable DT = classSQL.select(sql, "racuni").Tables[0];
                lblBrojRacuna.Text = "";
                lblBrojStola.Text = "Broj stola: " + DT.Rows[0]["naziv"].ToString();
                ukupni_iznos = Convert.ToDouble(DT.Rows[0]["ukupno"].ToString());
                txtDobiveni.Text = ukupni_iznos.ToString("#0.00");
            }
            else
            {
                string sql = string.Format(@"SELECT * FROM
racuni
LEFT JOIN stolovi ON racuni.id_stol = stolovi.id_stol
WHERE broj_racuna = '{0}' AND racuni.id_kasa = '{1}' AND racuni.id_ducan = '{2}';",
                    racun_ili_stol, Util.Korisno.idKasa, Util.Korisno.idDucan);

                DataTable DT = classSQL.select(sql, "racuni").Tables[0];
                lblBrojRacuna.Text = "Broj racuna: " + racun_ili_stol;
                lblBrojStola.Text = "Broj stola: " + DT.Rows[0]["naziv"].ToString();
                ukupni_iznos = Convert.ToDouble(DT.Rows[0]["ukupno"].ToString());
                //if (Convert.ToDouble(DT.Rows[0]["popust_racun_kartica_kupca"].ToString()) > 0) {
                //ukupni_iznos = Convert.ToDouble(DT.Rows[0]["ukupno"].ToString()) - Convert.ToDouble(DT.Rows[0]["popust_racun_kartica_kupca"].ToString());
                //}
                txtDobiveni.Text = ukupni_iznos.ToString("#0.00");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNumeric_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "C")
            {
                txtDobiveni.Text = txtDobiveni.Text.Remove(txtDobiveni.Text.Length - 1);
            }
            else if (btn.Text == "Enter")
            {
                btnZavrsiRacun.PerformClick();
            }
            else
            {
                if (txtDobiveni.Text == ukupni_iznos.ToString("#0.00")) { txtDobiveni.Text = ""; }
                txtDobiveni.Text += btn.Text;
            }
        }

        private void cbNacin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDobiveni.Select();
            }
            if (e.KeyCode == Keys.F1)
            {
                btnKupac.PerformClick();
            }
        }

        private void txtDobiveni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnZavrsiRacun.PerformClick();
            }
        }

        private void txtZaVratiti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnZavrsiRacun.PerformClick();
            }
        }

        private void btnZavrsiRacun_Click(object sender, EventArgs e)
        {
            zavrseno = false;

            try
            {
                decimal dec_parse;
                if (!Decimal.TryParse(txtDobiveni.Text, out dec_parse))
                {
                    //txtDobiveni.Text = txtDobiveni.Text.Remove(txtDobiveni.Text.Length - 1);
                    txtDobiveni.Text = "0";
                    MessageBox.Show("Greška kod upisa dobivenog iznosa.", "Greška");
                    return;
                }

                if (Convert.ToDouble(txtDobiveni.Text) < ukupni_iznos)
                {
                    MessageBox.Show("Dobiveni iznos je manji od ukupnoga.", "Greška");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            string got = "0";
            string kar = "0";
            string vir = "0";
            string ostalo = "0";
            string ukupno = ukupni_iznos.ToString("#0.00");
            string gotovina_ukupno = "0";

            string dodajR1 = "";
            try
            {
                if (cbNacin.SelectedValue.ToString() == "1")
                {
                    got = txtDobiveni.Text;
                    gotovina_ukupno = ukupno;
                    nacin_placanja = "G";
                }
                else if (cbNacin.SelectedValue.ToString() == "2")
                {
                    kar = ukupno;
                    nacin_placanja = "K";
                }
                else if (cbNacin.SelectedValue.ToString() == "3")
                {
                    vir = ukupno;
                    nacin_placanja = "T";
                }
                else if (cbNacin.SelectedValue.ToString() == "4")
                {
                    ostalo = ukupno;
                    nacin_placanja = "O";
                }
                else if (cbNacin.SelectedValue.ToString() == "5")
                {
                    ostalo = ukupno;
                    nacin_placanja = "O";
                }

                if (R1 != "" && R1 != null)
                {
                    dodajR1 = ",id_kupac='" + R1 + "'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            string sql = "UPDATE racuni SET dobiveno_gotovina='" + got + "', ukupno_ostalo='" + ostalo.Replace(",", ".") + "', ukupno_gotovina='" + gotovina_ukupno + "',ukupno_kartice='" + kar + "',ukupno_virman='" + vir + "', nacin_placanja='" + nacin_placanja + "'" + dodajR1 + " WHERE broj_racuna='" + racun_ili_stol + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'";

            if (Convert.ToInt32(cbNacin.SelectedValue) == 2 && hasKartice)
            {
                sql = "UPDATE racuni SET karticaID = '" + cmbKartice.SelectedValue + "', dobiveno_gotovina='" + got + "', ukupno_ostalo='" + ostalo.Replace(",", ".") + "', ukupno_gotovina='" + gotovina_ukupno + "',ukupno_kartice='" + kar + "',ukupno_virman='" + vir + "', nacin_placanja='" + nacin_placanja + "'" + dodajR1 + " WHERE broj_racuna='" + racun_ili_stol + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'";
            }
            classSQL.update(sql);

            //provjera_sql(classSQL.insert(sql));

            //if (Util.Korisno.kartica_kupca && kartica_kupca.Length > 0)
            //{

            //    string sqlKK = "INSERT INTO karticakupci_racuni (oib, poslovnica, naplatni_uredaj, kartica_kupca, broj_racuna, datum_racun, iznos) VALUES ((select oib from podaci_tvrtka limit 1), (select ime_ducana from ducan where id_ducan = '" + id_ducan + "'), (select ime_blagajne from blagajna where id_ducan = '" + id_ducan + "' and id_blagajna = '" + id_kasa + "'), '" + kartica_kupca + "', '" + brRac + "', '" + dt + "', " + uk1.ToString() + ");";

            //    classSQL.insert(sqlKK);

            //}
            decimal.TryParse(txtZaVratiti.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out vraceniIznos);
            zavrseno = true;
            this.Close();
        }

        private void txtDobiveni_TextChanged(object sender, EventArgs e)
        {
            if (txtDobiveni.Text != "")
            {
                decimal dec_parse;
                if (!Decimal.TryParse(txtDobiveni.Text, out dec_parse))
                {
                    txtDobiveni.Text = txtDobiveni.Text.Remove(txtDobiveni.Text.Length - 1);
                    MessageBox.Show("Greška kod upisa.", "Greška");
                    return;
                }

                txtZaVratiti.Text = Convert.ToDouble(Convert.ToDouble(txtDobiveni.Text) - ukupni_iznos).ToString("#0.00");
            }
        }

        private void btnKupac_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    R1 = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    lblKupac.Text = "Partner R1: " + partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    R1 = "";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            else
            {
                R1 = "";
            }
        }

        private void frmNaplata_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (zavrseno == false)
            {
                e.Cancel = true;
            }
        }

        private void cbNacin_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbNacin.SelectedValue) == 2 && hasKartice)
                {
                    cmbKartice.Visible = hasKartice;
                    label3.Visible = hasKartice;
                }
                else
                {
                    cmbKartice.Visible = (hasKartice ? !hasKartice : hasKartice);
                    label3.Visible = (hasKartice ? !hasKartice : hasKartice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbDjelatnik_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDjelatnik.SelectedValue != null)
                    _id_zaposlenik = (int)cmbDjelatnik.SelectedValue;
            }
            catch (Exception)
            {
                throw;
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