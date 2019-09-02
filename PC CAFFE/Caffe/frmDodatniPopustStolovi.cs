using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmDodatniPopustStolovi : Form
    {
        public frmDodatniPopustStolovi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public Caffe.frmStoloviZaNaplatu frm { get; set; }
        public Caffe.frmStoloviZaNaplatuCustom frmCustom { get; set; }

        private void frmDodatniPopust_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private DataSet DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

        private void frmPostavi_Click(object sender, EventArgs e)
        {
            decimal mpc_ukupno = 0;
            decimal popust = Convert.ToDecimal(txtPopust.Text);
            decimal ukupno = 0;
            decimal ukupno_stavka = 0;
            decimal PP = 0;
            decimal PDV = 0;
            int i = frm.dgw.CurrentRow.Index;

            ukupno_stavka = Convert.ToDecimal(frm.dgw.Rows[i].Cells["cijena"].FormattedValue.ToString());

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

            frm.dgw.Rows[i].Cells["cijena"].Value = Math.Round(_Pmpc, 3).ToString("#0.00");
            frm.dgw.Rows[i].Cells["vpc"].Value = _vpc;
            mpc_ukupno += ukupno_stavka;

            ukupno = ukupno - (mpc_ukupno * popust / 100);

            try
            {
                classSQL.update("UPDATE na_stol SET vpc='" + _vpc.ToString().Replace(",", ".") + "', mpc='" + _Pmpc.ToString().Replace(",", ".") + "' WHERE id_stol='" + frm._odabraniStol + "' AND sifra='" + frm.dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Close();
        }

        private void txtPopust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnDodaj.PerformClick();
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