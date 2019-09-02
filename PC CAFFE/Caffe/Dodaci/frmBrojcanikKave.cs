using System;
using System.Windows.Forms;

namespace PCPOS.Caffe.Dodaci
{
    public partial class frmBrojcanikKave : Form
    {
        public frmBrojcanikKave()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmUskladenjePrometa MyForm { get; set; }
        public string sifra { get; set; }
        public string naziv { get; set; }
        public int row { get; set; }
        public int cell { get; set; }

        private void frmBrojcanikKave_Load(object sender, EventArgs e)
        {
            try
            {
                txtDonos1.Text = MyForm.dgv.Rows[row].Cells["donos_brojcanik1"].FormattedValue.ToString();
                txtDonos2.Text = MyForm.dgv.Rows[row].Cells["donos_brojcanik2"].FormattedValue.ToString();

                txtKrajDana.Text = MyForm.dgv.Rows[row].Cells["brojcanik_kraj_dana1"].FormattedValue.ToString();
                txtKrajDana2.Text = MyForm.dgv.Rows[row].Cells["brojcanik_kraj_dana2"].FormattedValue.ToString();

                decimal a;
                decimal b;
                if (!decimal.TryParse(txtKrajDana.Text, out a)) { a = 0; }
                if (!decimal.TryParse(txtDonos1.Text, out b)) { b = 0; }
                txtRazlika1.Text = (a - b).ToString();

                if (!decimal.TryParse(txtKrajDana2.Text, out a)) { a = 0; }
                if (!decimal.TryParse(txtDonos2.Text, out b)) { b = 0; }
                txtRazlika2.Text = ((a - b) * 2).ToString();
                UKUPNO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void txtKrajDana_Leave(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (!decimal.TryParse(txtKrajDana.Text, out a)) { a = 0; }
            if (!decimal.TryParse(txtDonos1.Text, out b)) { b = 0; }
            txtRazlika1.Text = (a - b).ToString();
            UKUPNO();
        }

        private void txtKrajDana2_Leave(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (!decimal.TryParse(txtKrajDana2.Text, out a)) { a = 0; }
            if (!decimal.TryParse(txtDonos2.Text, out b)) { b = 0; }
            txtRazlika2.Text = ((a - b) * 2).ToString();
            UKUPNO();
        }

        private void txtDonos1_Leave(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (!decimal.TryParse(txtKrajDana.Text, out a)) { a = 0; }
            if (!decimal.TryParse(txtDonos1.Text, out b)) { b = 0; }
            txtRazlika1.Text = (a - b).ToString();
            UKUPNO();
        }

        private void txtDonos2_Leave(object sender, EventArgs e)
        {
            decimal a;
            decimal b;
            if (!decimal.TryParse(txtKrajDana2.Text, out a)) { a = 0; }
            if (!decimal.TryParse(txtDonos2.Text, out b)) { b = 0; }
            txtRazlika2.Text = ((a - b) * 2).ToString();
            UKUPNO();
        }

        private void UKUPNO()
        {
            decimal a;
            decimal b;
            if (!decimal.TryParse(txtRazlika1.Text, out a)) { a = 0; }
            if (!decimal.TryParse(txtRazlika2.Text, out b)) { b = 0; }
            txtUkupno.Text = (a + b).ToString();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOK_Click(object sender, EventArgs e)
        {
            MyForm.dgv.Rows[row].Cells["donos_brojcanik1"].Value = txtDonos1.Text;
            MyForm.dgv.Rows[row].Cells["donos_brojcanik2"].Value = txtDonos2.Text;

            MyForm.dgv.Rows[row].Cells["brojcanik_kraj_dana1"].Value = txtKrajDana.Text;
            MyForm.dgv.Rows[row].Cells["brojcanik_kraj_dana2"].Value = txtKrajDana2.Text;

            MyForm.dgv.Rows[row].Cells["brojcanik_prijenos"].Value = txtUkupno.Text;
            //MyForm.dgv.Rows[row].Cells["prodaja_prodano"].Value = Convert.ToDecimal(txtUkupno.Text).ToString("#0.000");
            MyForm.dgv.Rows[row].Cells["iznoskuna"].Value = Math.Round((Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["brojcanik_prijenos"].FormattedValue.ToString()) * Convert.ToDecimal(MyForm.dgv.Rows[row].Cells["cijena"].FormattedValue.ToString())), 3).ToString("#0.00");

            this.Close();
        }

        private void txtDonos1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtUkupno.Select();
                txtOK.PerformClick();
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