using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmUzvratiti : Form
    {
        private double ostatak = 0;
        public frmKasa MainForm { get; set; }
        public Caffe.frmCaffe MainFormCaffe { get; set; }
        public string getUkupnoKasa;
        public string getNacin;

        public frmUzvratiti()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmUzvratiti_Load(object sender, EventArgs e)
        {
            txtDobiveni.Select();
            SetComboBox();
            txtDobiveni.Text = String.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa));
            lblUkupno.Text = String.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa)) + "Kn";
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.DarkOliveGreen, Color.AliceBlue, 1000);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void SetComboBox()
        {
            DataTable DTSK = new DataTable("nacin");
            DTSK.Columns.Add("id_nacin", typeof(string));
            DTSK.Columns.Add("nacin", typeof(string));
            if (getNacin == "GO")
            {
                DTSK.Rows.Add("GO", "Gotovina");
            }
            else if (getNacin == "KA")
            {
                DTSK.Rows.Add("KA", "Kartice");
            }

            cbNacin.DataSource = DTSK;
            cbNacin.DisplayMember = "nacin";
            cbNacin.ValueMember = "id_nacin";
            cbNacin.SelectedValue = getNacin;

            DataTable DTSK1 = new DataTable("nacin");
            DTSK1.Columns.Add("id_nacin", typeof(string));
            DTSK1.Columns.Add("nacin", typeof(string));
            //DTSK1.Rows.Add("GO", "Gotovina");
            DTSK1.Rows.Add("KA", "Kartice");

            cbNacin2.DataSource = DTSK1;
            cbNacin2.DisplayMember = "nacin";
            cbNacin2.ValueMember = "id_nacin";
        }

        private void cbNacin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDobiveni.Select();
            }
        }

        private void txtDobiveni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ostatak = Convert.ToDouble(txtDobiveni.Text) - Convert.ToDouble(getUkupnoKasa);

                if (ostatak < 0)
                {
                    if (getNacin == "GO")
                    {
                        lbl1.Visible = true;
                        lbl2.Visible = true;
                        txtDobiveni2.Visible = true;
                        cbNacin2.Visible = true;
                        cbNacin2.Select();
                        cbNacin2.SelectedValue = "KA";
                        txtDobiveni2.Text = IzracunOstatakKartica().ToString("#0.00");
                    }
                    else
                    {
                        btnSpremi.Select();
                    }
                }
                else
                {
                    btnSpremi.Select();
                }
                lblVratiti.Text = String.Format("{0:0.00}", IzracunOstatak());
            }
        }

        private void cbNacin2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDobiveni2.Select();
            }
        }

        private void txtDobiveni2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ostatak = Convert.ToDouble(txtDobiveni2.Text) - (Convert.ToDouble(getUkupnoKasa) - Convert.ToDouble(txtDobiveni.Text));

                if (e.KeyCode == Keys.Enter)
                {
                    lblVratiti.Text = String.Format("{0:0.00}", IzracunOstatak());

                    if (IzracunOstatak() < 0)
                    {
                        if (MessageBox.Show("Dobiveni iznos je manji od ukupnog iznosa.\r\nJeste li sigurni da želite završiti račun?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSpremi.Select();
                        }
                    }
                    else
                    {
                        btnSpremi.Select();
                    }
                }
            }
        }

        private double IzracunOstatakKartica()
        {
            double uk = Convert.ToDouble(getUkupnoKasa) - (Convert.ToDouble(txtDobiveni.Text));

            if (uk < 0)
            {
                lblVratiti.ForeColor = Color.Red;
            }
            else if (uk == 0)
            {
                lblVratiti.ForeColor = Color.Black;
            }
            else
            {
                lblVratiti.ForeColor = Color.Green;
            }
            return uk;
        }

        private double IzracunOstatak()
        {
            double uk = (Convert.ToDouble(txtDobiveni.Text) + Convert.ToDouble(txtDobiveni2.Text)) - Convert.ToDouble(getUkupnoKasa);

            if (uk < 0)
            {
                lblVratiti.ForeColor = Color.Red;
            }
            else if (uk == 0)
            {
                lblVratiti.ForeColor = Color.Black;
            }
            else
            {
                lblVratiti.ForeColor = Color.Green;
            }
            return uk;
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

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            Double k = 0;
            Double g = 0;

            try
            {
                if (cbNacin.SelectedValue.ToString() == "GO")
                {
                    g = Convert.ToDouble(getUkupnoKasa) - Convert.ToDouble(txtDobiveni2.Text);
                    if (MainForm != null)
                    {
                        MainForm.DobivenoGotovina = txtDobiveni.Text;
                    }
                    else if (MainFormCaffe != null)
                    {
                        MainFormCaffe.DobivenoGotovina = txtDobiveni.Text;
                    }
                }
                else
                {
                    if (MainForm != null)
                    {
                        MainForm.DobivenoGotovina = "0";
                    }
                    else if (MainFormCaffe != null)
                    {
                        MainFormCaffe.DobivenoGotovina = "0";
                    }
                }

                if (cbNacin2.SelectedValue.ToString() == "KA" && cbNacin2.Visible == true)
                {
                    k = Convert.ToDouble(txtDobiveni2.Text) + k;
                }

                if (getNacin == "KA")
                {
                    k = Convert.ToDouble(txtDobiveni.Text) + k;
                }

                if ((g + k) >= Convert.ToDouble(getUkupnoKasa))
                {
                    if (MainForm != null)
                    {
                        MainForm.IznosGotovina = g.ToString();
                        MainForm.IznosKartica = k.ToString();
                    }
                    else if (MainFormCaffe != null)
                    {
                        MainFormCaffe.IznosGotovina = g.ToString();
                        MainFormCaffe.IznosKartica = k.ToString();
                    }
                    this.Close();
                }
                else
                {
                    string msg = "Iznos računa je: " + String.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa)) + " kn" +
                        "\r\na prema Vašem upisu gotovina iznosi: " + g.ToString("#0.00") + " kn \r\na kartice: " + k.ToString("#0.00") + " kn." +
                        "\r\nFali vam još " + Convert.ToDouble(Convert.ToDouble(getUkupnoKasa) - (g + k)).ToString("#0.00") + " kn.";
                    MessageBox.Show(msg, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDobiveni.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod izračunavanja. Provjerite uspisane vrijednosti.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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