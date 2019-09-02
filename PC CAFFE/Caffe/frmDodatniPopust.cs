using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmDodatniPopust : Form
    {
        public bool hasDiscount = false;

        public frmDodatniPopust()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmCaffe frm { get; set; }

        private void frmDodatniPopust_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private DataSet DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

        private void frmPostavi_Click(object sender, EventArgs e)
        {
            decimal popust = 0;
            decimal.TryParse(txtPopust.Text, out popust);

            for (int i = 0; i < frm.dgw.Rows.Count; i++)
            {
                frm.dgw.Rows[i].Cells["rabat"].Value = popust.ToString().Replace('.', ',');
            }

            hasDiscount = true;
            frm.IzracunUkupno();
            this.Close();
            /*decimal mpc_ukupno = 0;
            decimal popust = Convert.ToDecimal(txtPopust.Text);
            decimal ukupno = 0;
            decimal ukupno_stavka = 0;
            decimal PP = 0;
            decimal PDV = 0;

            //decimal PoustMpcUkupnoHamer = 0;
            //decimal PoustVpcUkupnoHamer = 0;

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

            ukupno = ukupno - (mpc_ukupno * popust / 100);*/
        }

        private void txtPopust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnDodaj.PerformClick();
            }
            else if (e.KeyData == Keys.Up)
            {
            }
            else if (e.KeyData == Keys.Down)
            {
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