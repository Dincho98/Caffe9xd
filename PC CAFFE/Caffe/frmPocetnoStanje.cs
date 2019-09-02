using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPocetnoStanje : Form
    {
        public frmPocetnoStanje()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPocetnoStanje_Load(object sender, EventArgs e)
        {
            SetData();
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void SetData()
        {
            DataTable DT_zaposlenik = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
            if (DT_zaposlenik.Rows.Count > 0) { lblZaposlenik.Text = "Prijavljen: " + DT_zaposlenik.Rows[0][0].ToString(); } else { lblZaposlenik.Text = ""; }

            try
            {
                DataTable DTmin = classSQL.select("SELECT pocetno_stanje FROM smjene WHERE id='" + ZadnjiBroj() + "' AND smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ", "smjene").Tables[0];
                if (DTmin.Rows.Count > 0)
                    txtPocetno.Text = DTmin.Rows[0][0].ToString();
                else
                    txtPocetno.Text = "0";
            }
            catch (Exception)
            {
            }
        }

        private string ZadnjiBroj()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene WHERE smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnZapocniSmjenu_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtPocetno.Text, out dec_parse))
            {
                MessageBox.Show("Krivo upisani polog.", "Upozorenje");
                return;
            }

            string sql = "INSERT INTO smjene " +
                " (pocetno_stanje,konobar,pocetak,id_ducan,id_kasa) VALUES " +
                " (" +
                "'" + txtPocetno.Text.Replace(",", ".") + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + Util.Korisno.idDucan + "'," +
                "'" + Util.Korisno.idKasa + "'" +
                ")";
            classSQL.insert(sql);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPocetno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnZapocniSmjenu.PerformClick();
            }
            else if (e.KeyData == Keys.Escape)
            {
                button1.PerformClick();
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