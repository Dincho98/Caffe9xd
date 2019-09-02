using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmDodajPartnera : Form
    {
        public frmDodajPartnera()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmDodajPartnera_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke,oib,adresa FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartner.Enabled = false;
                    txtSifraPartner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtOIB.Text = partner.Tables[0].Rows[0]["OIB"].ToString();
                    txtAdresa.Text = partner.Tables[0].Rows[0]["adresa"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSviRacuni sr = new frmSviRacuni();
            sr.ShowDialog();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (txtSifraPartner.Text != "")
            {
                decimal dec_parse;
                if (Decimal.TryParse(txtSifraPartner.Text, out dec_parse))
                {
                    txtSifraPartner.Text = dec_parse.ToString();
                }
                else
                {
                    MessageBox.Show("Greška kod upisa šifre partnera.", "Greška");
                    return;
                }
            }
            else
            {
                txtSifraPartner.Text = "0";
            }
            string sql = "UPDATE racuni SET id_kupac='" + txtSifraPartner.Text + "' WHERE broj_racuna='" + txtBroj.Text + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'";
            classSQL.update(sql);

            MessageBox.Show("Spremljeno");
            this.Close();
            txtSifraPartner.Enabled = true;
        }

        private void txtSifraPartner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal dec_parse;
                if (Decimal.TryParse(txtSifraPartner.Text, out dec_parse))
                {
                    txtSifraPartner.Text = dec_parse.ToString();
                }
                else
                {
                    MessageBox.Show("Greška kod upisa šifre partnera.", "Greška");
                    return;
                }

                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke,oib,adresa FROM partners WHERE id_partner ='" + txtSifraPartner.Text + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartner.Enabled = false;
                    txtSifraPartner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtOIB.Text = partner.Tables[0].Rows[0]["oib"].ToString();
                    txtAdresa.Text = partner.Tables[0].Rows[0]["adresa"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSifraPartner.Text = "";
            txtImePartner.Text = "";
            txtOIB.Text = "";
            txtAdresa.Text = "";
            txtBroj.Text = "";
            txtSifraPartner.Enabled = true;
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraPartner.Select();
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