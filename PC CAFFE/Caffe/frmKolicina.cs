using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmKolicina : Form
    {
        public frmKolicina()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string _type { get; set; }

        public Caffe.frmCaffe MainForm { get; set; }

        private void frmKolicina_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            txtkol.Select();
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
            if (e.KeyChar == '-')
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

        private void btn_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            txtkol.Text += conGrupa.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "DEL" && txtkol.Text.Length > 0)
            {
                txtkol.Text = txtkol.Text.Remove(txtkol.Text.Length - 1);
            }
        }

        private void button14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnEnter.PerformClick();
            }
            else if (e.KeyCode == Keys.F1)
            {
                e.SuppressKeyPress = true;
                btnEnter.PerformClick();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (Decimal.TryParse(txtkol.Text, out dec_parse))
            {
                txtkol.Text = dec_parse.ToString();
                if (dec_parse == 0)
                {
                    MessageBox.Show("Količina ne smije biti nula!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                this.Close();
                return;
            }

            if (_type == "K")
            {
                MainForm.dgw.CurrentRow.Cells[1].Value = txtkol.Text;
            }
            else if (_type == "C")
            {
                double PDV = Convert.ToDouble(MainForm.dgw.CurrentRow.Cells["porez"].FormattedValue.ToString());
                double PP = Convert.ToDouble(MainForm.dgw.CurrentRow.Cells["porez_potrosnja"].FormattedValue.ToString());
                double mpc = Convert.ToDouble(txtkol.Text);
                double pdv_stavka = 0;
                double pnp_stavka = 0;

                //Ovaj kod dobiva PDV
                double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                //Ovaj kod dobiva porez na potrošnju
                double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                pnp_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                MainForm.dgw.CurrentRow.Cells["vpc"].Value = (mpc - pdv_stavka - pnp_stavka);
                MainForm.dgw.CurrentRow.Cells["mpc"].Value = mpc.ToString("#0.00");
            }

            MainForm.IzracunUkupno();
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