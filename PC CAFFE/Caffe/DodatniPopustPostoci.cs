using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class DodatniPopustPostoci : Form
    {
        public bool hasDiscount = false;

        public DodatniPopustPostoci()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmCaffe frm { get; set; }
        public string stavka_naziv { get; set; }

        private void frmDodatniPopust_Load(object sender, EventArgs e)
        {
            int br = frm.dgw.SelectedRows[0].Index;
            frm.dgw.CurrentCell = frm.dgw.Rows[br].Cells[0];

            label2.Text = "Stavka: " + frm.dgw.CurrentRow.Cells["naziv"].FormattedValue.ToString();
            /*if (frm.popust_na_cijeli_racun != 0)
            {
                txtPopust.Text = frm.popust_na_cijeli_racun.ToString();
                btnDodaj.Enabled = false;
            }
            else
            {
                foreach (var item in frm.dgw.Rows)
                {
                    if ((item as DataGridViewRow).Cells["rabat"].FormattedValue.ToString() != "0")
                    {
                        btnDodajNaSve.Enabled = false;
                    }
                }
            }*/
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private DataSet DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

        private void frmPostavi_Click(object sender, EventArgs e)
        {
            /*if (frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString() != "0")
            {
                MessageBox.Show("Popust na ovoj stavki več postoji popust od " + frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString() + " % .\r\nAko želite promjeniti popust sa stavke morate obrisati stavku i dodati novi popust.", "");
                this.Close();
            }
            decimal popust = 0;
            decimal.TryParse(txtPopust.Text, out popust);
            frm.dgw.CurrentRow.Cells["rabat"].Value = popust.ToString().Replace('.', ',');
            frm.IzracunUkupno();
            frm.dgw.CurrentRow.DefaultCellStyle.BackColor = Color.Pink;
            this.Close();*/
            PostaviPopust();
        }

        private void txtPopust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (decimal.TryParse(txtPopust.Text.Replace(',', '.'), out decimal popust) && popust > 0)
                {
                    e.SuppressKeyPress = true;
                    btnDodaj.PerformClick();
                }
                else
                    MessageBox.Show("Iznos popusta mora biti veći od 0!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.KeyData == Keys.Up)
            {
                btnGore.PerformClick();
            }
            else if (e.KeyData == Keys.Down)
            {
                btnDole.PerformClick();
            }
        }

        private void btnGore_Click(object sender, EventArgs e)
        {
            try
            {
                int curent = frm.dgw.CurrentRow.Index;
                if (curent > 0)
                    frm.dgw.CurrentCell = frm.dgw.Rows[curent - 1].Cells[0];
                label2.Text = "Stavka: " + frm.dgw.CurrentRow.Cells["naziv"].FormattedValue.ToString();
                txtPopust.Text = frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString();
            }
            catch { }
        }

        private void btnDole_Click(object sender, EventArgs e)
        {
            try
            {
                int curent = frm.dgw.CurrentRow.Index;
                if (curent < frm.dgw.RowCount - 1)
                    frm.dgw.CurrentCell = frm.dgw.Rows[curent + 1].Cells[0];
                label2.Text = "Stavka: " + frm.dgw.CurrentRow.Cells["naziv"].FormattedValue.ToString();
                txtPopust.Text = frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString();
            }
            catch { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            txtPopust.Text += conGrupa.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "DEL" && txtPopust.Text.Length > 0)
            {
                txtPopust.Text = txtPopust.Text.Remove(txtPopust.Text.Length - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostaviPopust(true);
            /*decimal mpc_ukupno = 0;
            decimal popust = Convert.ToDecimal(txtPopust.Text);
            decimal ukupno = 0;
            decimal ukupno_stavka = 0;
            decimal PP = 0;
            decimal PDV = 0;

            for (int i = 0; i < frm.dgw.RowCount; i++)
            {
                if (File.Exists("hamer"))
                {
                    ukupno_stavka = Convert.ToDecimal(frm.dgw.Rows[i].Cells["mpc"].FormattedValue.ToString());
                    PDV = Convert.ToDecimal(frm.dgw.Rows[i].Cells["porez"].FormattedValue.ToString());
                    PP = Convert.ToDecimal(frm.dgw.Rows[i].Cells["porez_potrosnja"].FormattedValue.ToString());

                    decimal _Pmpc = (ukupno_stavka - (ukupno_stavka * popust / 100));

                    //Ovaj kod dobiva PDV
                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * PDV) / (100 + PDV + PP));
                    decimal pdv_stavka = (_Pmpc * PreracunataStopaPDV) / 100;

                    //Ovaj kod dobiva porez na potrošnju
                    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * PP) / (100 + PDV + PP));
                    decimal pnp_stavka = (_Pmpc * PreracunataStopaPorezNaPotrosnju) / 100;

                    decimal _vpc = (_Pmpc - (pdv_stavka + pnp_stavka));

                    frm.dgw.Rows[i].Cells["mpc"].Value = Math.Round(_Pmpc, 3).ToString("#0.00");
                    frm.dgw.Rows[i].Cells["vpc"].Value = _vpc;
                    mpc_ukupno += ukupno_stavka;

                    frm.dgw.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                }
                else
                {
                    frm.popust_na_cijeli_racun = popust;
                    ukupno_stavka = Convert.ToDecimal(frm.dgw.Rows[i].Cells["mpc"].FormattedValue.ToString());
                    PDV = Convert.ToDecimal(frm.dgw.Rows[i].Cells["porez"].FormattedValue.ToString());
                    PP = Convert.ToDecimal(frm.dgw.Rows[i].Cells["porez_potrosnja"].FormattedValue.ToString());

                    decimal _Pmpc = (ukupno_stavka - (ukupno_stavka * popust / 100));

                    //Ovaj kod dobiva PDV
                    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * PDV) / (100 + PDV + PP));
                    decimal pdv_stavka = (_Pmpc * PreracunataStopaPDV) / 100;

                    //Ovaj kod dobiva porez na potrošnju
                    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * PP) / (100 + PDV + PP));
                    decimal pnp_stavka = (_Pmpc * PreracunataStopaPorezNaPotrosnju) / 100;

                    decimal _vpc = (_Pmpc - (pdv_stavka + pnp_stavka));

                    frm.dgw.Rows[i].Cells["mpc"].Value = Math.Round(_Pmpc, 3).ToString("#0.00");
                    frm.dgw.Rows[i].Cells["vpc"].Value = _vpc;
                    mpc_ukupno += ukupno_stavka;
                }
            }

            ukupno = ukupno - (mpc_ukupno * popust / 100);

            this.Close();*/
        }

        /// <summary>
        /// Sets discount for item/s depending on which button is clicked
        /// </summary>
        /// <param name="sviArtikli">Set true if button for all items is clicked</param>
        private void PostaviPopust(bool sviArtikli = false)
        {
            decimal.TryParse(txtPopust.Text, out decimal popust);

            if (popust > 0)
            {
                if (sviArtikli)
                {
                    for (int i = 0; i < frm.dgw.Rows.Count; i++)
                    {
                        frm.dgw.Rows[i].Cells["rabat"].Value = popust.ToString().Replace('.', ',');
                    }
                }
                else
                {
                    /*if (frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString() != "0")
                    {
                        MessageBox.Show("Popust na ovoj stavki več postoji popust od " + frm.dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString() + " % .\r\nAko želite promjeniti popust sa stavke morate obrisati stavku i dodati novi popust.", "");
                        this.Close();
                    }*/
                    frm.dgw.CurrentRow.Cells["rabat"].Value = popust.ToString().Replace('.', ',');
                    //frm.dgw.CurrentRow.DefaultCellStyle.BackColor = Color.Pink;
                }
                hasDiscount = true;
                frm.IzracunUkupno();
                this.Close();
            }
            else
                MessageBox.Show("Iznos popusta mora biti veći od 0!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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