using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmUskladenje : Form
    {
        public frmUskladenje()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string sifra { get; set; }
        public string naziv { get; set; }
        public string jmj { get; set; }

        //public string unos_robe { get; set; }
        public int row { get; set; }

        public int cell { get; set; }
        //public string brojcanik { get; set; }
        //public string skladiste_stanje { get; set; }

        public frmUskladenjePrometa MyForm { get; set; }

        private void frmUskladenje_Load(object sender, EventArgs e)
        {
            txtZalihaKolicina.Select();
            Fill();
            txtProdanoStanje.Select();
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void Fill()
        {
            lblSifra.Text = "Šifra: " + sifra;
            lblNaziv.Text = "Naziv: " + naziv;
            lblMjera.Text = "Jedinična mjera: " + jmj;

            //Zaliha , DONOS
            txtZalihaKolicina.Text = MyForm.dgv.Rows[row].Cells["kolicina_skladiste"].FormattedValue.ToString();
            txtZalihaBrojcanik.Text = MyForm.dgv.Rows[row].Cells["_brojcanik"].FormattedValue.ToString();
            txtZalihaUlaz.Text = MyForm.dgv.Rows[row].Cells["unos_robe"].FormattedValue.ToString();
            txtZalihaUkupno.Text = (Convert.ToDecimal(txtZalihaKolicina.Text) + Convert.ToDecimal(txtZalihaUlaz.Text)).ToString();

            //PRODAJA
            txtProdanoBrojcanik.Text = MyForm.dgv.Rows[row].Cells["brojcanik_prijenos"].FormattedValue.ToString();
            if (MyForm.dgv.Rows[row].Cells["sifra"].FormattedValue.ToString() != "29")
            {
                txtProdanoStanje.Text = ((Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["kolicina_skladiste"].FormattedValue.ToString()) + Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["unos_robe"].FormattedValue.ToString())) - Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["prodaja_prodano"].FormattedValue.ToString())).ToString("#0.000");
            }
            else
            {
                txtProdanoStanje.Text = (Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["kolicina_skladiste"].FormattedValue.ToString()) + Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["prodaja_prodano"].FormattedValue.ToString())).ToString("#0.000");
            }
            txtProdanoKolicina.Text = MyForm.dgv.Rows[row].Cells["prodaja_kucano"].FormattedValue.ToString();

            //PRIJENOS
            txtPrijenosKolicina.Text = MyForm.dgv.Rows[row].Cells["stanje_prijenos"].FormattedValue.ToString();
            txtPrijenosBrojcanik.Text = MyForm.dgv.Rows[row].Cells["brojcanik_prijenos"].FormattedValue.ToString();

            txtCijena.Text = MyForm.dgv.Rows[row].Cells["cijena"].FormattedValue.ToString();
        }

        private void txtUnos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtOK.PerformClick();
                //System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void txtUnos_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);

                decimal d;
                if (!decimal.TryParse(txt.Text, out d))
                {
                    txt.Text = "0";
                    MessageBox.Show("Krivo upisana cijena.");
                    txt.Select();
                }
            }
        }

        private void txtOK_Click(object sender, EventArgs e)
        {
            decimal d;

            if (!decimal.TryParse(txtZalihaKolicina.Text, out d)) { txtZalihaKolicina.Text = "0"; }
            if (!decimal.TryParse(txtZalihaBrojcanik.Text, out d)) { txtZalihaBrojcanik.Text = "0"; }
            if (!decimal.TryParse(txtZalihaUlaz.Text, out d)) { txtZalihaUlaz.Text = "0"; }
            if (!decimal.TryParse(txtZalihaUkupno.Text, out d)) { txtZalihaUkupno.Text = "0"; }
            if (!decimal.TryParse(txtProdanoBrojcanik.Text, out d)) { txtProdanoBrojcanik.Text = "0"; }
            if (!decimal.TryParse(txtProdanoStanje.Text, out d)) { txtProdanoStanje.Text = "0"; }
            if (!decimal.TryParse(txtProdanoKolicina.Text, out d)) { txtProdanoKolicina.Text = "0"; }
            if (!decimal.TryParse(txtPrijenosKolicina.Text, out d)) { txtPrijenosKolicina.Text = "0"; }
            if (!decimal.TryParse(txtPrijenosBrojcanik.Text, out d)) { txtPrijenosBrojcanik.Text = "0"; }
            if (!decimal.TryParse(txtCijena.Text, out d)) { txtCijena.Text = "0"; }

            try
            {
                classSQL.update("UPDATE roba_prodaja SET cijena2='" + txtCijena.Text.Replace(",", ".") + "' WHERE sifra='" + sifra + "'");
            }
            catch
            {
            }

            MyForm.dgv.Rows[row].Cells["kolicina_skladiste"].Value = txtZalihaKolicina.Text;
            MyForm.dgv.Rows[row].Cells["_brojcanik"].Value = txtZalihaBrojcanik.Text;
            MyForm.dgv.Rows[row].Cells["unos_robe"].Value = txtZalihaUlaz.Text;
            //MyForm.dgv.Rows[row].Cells["ukupno"].Value = txtZalihaUkupno.Text;

            MyForm.dgv.Rows[row].Cells["_brojcanik"].Value = txtProdanoBrojcanik.Text;
            if (MyForm.dgv.Rows[row].Cells["sifra"].FormattedValue.ToString() != "29")
            {
                MyForm.dgv.Rows[row].Cells["prodaja_prodano"].Value = ((Convert.ToDecimal(txtZalihaKolicina.Text) + Convert.ToDecimal(txtZalihaUlaz.Text)) - Convert.ToDecimal(txtProdanoStanje.Text)).ToString("#0.000");
            }
            else
            {
                MyForm.dgv.Rows[row].Cells["prodaja_prodano"].Value = (Convert.ToDecimal(txtProdanoStanje.Text) - Convert.ToDecimal(txtZalihaKolicina.Text)).ToString("#0.000");
            }
            MyForm.dgv.Rows[row].Cells["prodaja_kucano"].Value = txtProdanoKolicina.Text;

            MyForm.dgv.Rows[row].Cells["stanje_prijenos"].Value = txtPrijenosKolicina.Text;
            MyForm.dgv.Rows[row].Cells["brojcanik_prijenos"].Value = txtPrijenosBrojcanik.Text;

            MyForm.dgv.Rows[row].Cells["cijena"].Value = txtCijena.Text;

            MyForm.dgv.Rows[row].Cells["razlika"].Value = Math.Round((Convert.ToDecimal(txtProdanoKolicina.Text) - ((Convert.ToDecimal(txtZalihaKolicina.Text) + Convert.ToDecimal(txtZalihaUlaz.Text)) - Convert.ToDecimal(txtProdanoStanje.Text))), 3).ToString("#0.00");

            MyForm.dgv.Rows[row].Cells["iznoskuna"].Value = Math.Round((Convert.ToDecimal(txtCijena.Text) * (Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["prodaja_prodano"].FormattedValue.ToString()))), 3).ToString("#0.00");

            this.Close();
        }

        private void txtZalihaKolicina_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);

                if (txt.Name == "txtProdanoKolicina")
                {
                }
                else if (txt.Name == "txtProdanoStanje")
                {
                    //txtProdanoKolicina.Text = (Convert.ToDecimal(txtZalihaKolicina.Text) - Convert.ToDecimal(txtProdanoStanje.Text)).ToString("#0.000");
                }
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
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