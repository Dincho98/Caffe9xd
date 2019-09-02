using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKoristiKarticu : Form
    {
        public string kartica_kupca { get; set; }
        public decimal ukupno_iznos { get; set; }
        public bool useUDS { get; internal set; }
        public decimal udsScores { get; internal set; }

        private string sql = "";

        public frmKoristiKarticu()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmKoristiKarticu_Load(object sender, EventArgs e)
        {
            try
            {
                if (!useUDS)
                {
                    sql = "SELECT COALESCE(SUM(bodovi), 0) AS bodoviUkp FROM karticakupci_racuni WHERE kartica_kupca = '" + kartica_kupca + "'";

                    DataTable dtUkp = classSQL.select(sql, "karticakupci_racuni").Tables[0];
                    if (dtUkp != null)
                    {
                        lblUkupnoBodovi.Text = Convert.ToDecimal(dtUkp.Rows[0]["bodoviUkp"]).ToString("#0.00");
                    }
                    else
                    {
                        lblUkupnoBodovi.Text = "0,00";
                    }
                }
                else
                {
                    lblUkupnoBodovi.Text = udsScores.ToString("#0.00");
                }
                lblUkupniIznosRacuna.Text = ukupno_iznos.ToString("#0.00");
                txtIznosZaOduzeti.Focus();
                txtIznosZaOduzeti.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNastavi_Click(object sender, EventArgs e)
        {
            try
            {
                decimal d = 0;

                if (decimal.TryParse(txtIznosZaOduzeti.Text, out d) && d > 0)
                {
                    if (d < ukupno_iznos && d <= Convert.ToDecimal(lblUkupnoBodovi.Text))
                    {
                        ukupno_iznos -= d;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    if (useUDS)
                    {
                        MessageBox.Show("Krivo upisan iznos za UDS Game bodove.");
                    }
                    else
                    {
                        MessageBox.Show("Krivo upisan iznos za popust kartice.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIznosZaOduzeti_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if()
                decimal d = 0;
                if (decimal.TryParse(txtIznosZaOduzeti.Text, out d))
                {
                    if (d > Convert.ToDecimal(lblUkupniIznosRacuna.Text) || d > Convert.ToDecimal(lblUkupnoBodovi.Text))
                    {
                        txtIznosZaOduzeti.BackColor = Color.Coral;
                    }
                    else
                    {
                        txtIznosZaOduzeti.BackColor = SystemColors.Window;
                    }
                }
                else if (txtIznosZaOduzeti.Text == "")
                {
                    txtIznosZaOduzeti.BackColor = SystemColors.Window;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIznosZaOduzeti_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == ','
                    && (sender as TextBox).Text.IndexOf(',') > -1)
                {
                    e.Handled = true;
                }

                if (txtIznosZaOduzeti.Text.Length > 0 && e.KeyChar == (char)Keys.Enter)
                {
                    //btnNastavi.Focus();
                    btnNastavi.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}