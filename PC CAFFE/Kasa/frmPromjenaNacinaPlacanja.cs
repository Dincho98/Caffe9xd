using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPromjenaNacinaPlacanja : Form
    {
        public frmPromjenaNacinaPlacanja()
        {
            InitializeComponent();
        }

        private void frmPromjenaNacinaPlacanja_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtGotovina.Text = "";
            txtKartica.Text = "";
            txtBroj.Text = "";
            txtGotovina.Enabled = false;
            txtKartica.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSviRacuni sr = new frmSviRacuni();
            sr.ShowDialog();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (txtGotovina.Text == "")
            {
                txtGotovina.Text = "0";
            }

            if (txtKartica.Text == "")
            {
                txtKartica.Text = "0";
            }

            decimal dec_parse;
            if (!Decimal.TryParse(txtKartica.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa kartice.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtGotovina.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa kartice.", "Greška");
                return;
            }

            string sql = "UPDATE racuni SET ukupno_gotovina='" + txtGotovina.Text + "',ukupno_kartice='" + txtKartica.Text + "',dobiveno_gotovina='" + Convert.ToDecimal(txtGotovina.Text) + "' WHERE broj_racuna='" + txtBroj.Text + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'";
            provjera_sql(classSQL.update(sql));

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Promjena načina plaćanja po računu ." + txtBroj.Text + "')"));

            txtGotovina.Enabled = true;
            txtKartica.Enabled = true;

            MessageBox.Show("Spremljeno", "Spremljeno");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataTable dt = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + txtBroj.Text + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Upisali ste krivi broj računa.", "Greška");
                }
                else
                {
                    txtGotovina.Text = Convert.ToDecimal(dt.Rows[0]["ukupno_gotovina"].ToString()).ToString("#0.00");
                    txtKartica.Text = Convert.ToDecimal(dt.Rows[0]["ukupno_kartice"].ToString()).ToString("#0.00");
                    lblIznos.Text = Convert.ToDecimal(dt.Rows[0]["ukupno"].ToString()).ToString("#0.00");
                    txtGotovina.Enabled = true;
                    txtKartica.Enabled = true;
                }

                txtGotovina.Select();
            }
        }

        private void txtGotovina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtKartica.Select();
            }
        }

        private void txtKartica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTrazi.Select();
            }
        }
    }
}