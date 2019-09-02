using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmNarudzbaAdresa : Form
    {
        public int broj_narudzbe { get; set; }
        public int stol { get; set; }
        public int id_ducan { get; set; }
        public int brrac { get; set; }

        public frmNarudzbaAdresa()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void btnESC_Click(object sender, EventArgs e)
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "update caffe_narudzbe set mjesto = '" + txtMjesto.Text + "', ulica = '" + txtUlica.Text + "', kbr = '" + txtKbr.Text + "', telefon = '" + txtTelefon.Text + "' where broj_narudzbe = '" + broj_narudzbe + "' and id_ducan = '" + id_ducan + "';";
                //classSQL.update(sql);

                sql =
                @"UPDATE adresa_dostave SET mjesto='" + txtMjesto.Text + @"', ulica='" + txtUlica.Text + @"', kbr='" + txtKbr.Text + @"', telefon = '" + txtTelefon.Text + @"' WHERE mjesto='" + txtMjesto.Text + @"' AND ulica='" + txtUlica.Text + @"' and kbr= '" + txtKbr.Text + @"' and telefon = '" + txtTelefon.Text + @"';
INSERT INTO adresa_dostave (mjesto, ulica, kbr, telefon)
SELECT '" + txtMjesto.Text + @"', '" + txtUlica.Text + @"', '" + txtKbr.Text + @"', '" + txtTelefon.Text + @"'
WHERE NOT EXISTS (SELECT 1 FROM adresa_dostave WHERE mjesto='" + txtMjesto.Text + @"' AND ulica='" + txtUlica.Text + @"' and kbr= '" + txtKbr.Text + @"' and telefon = '" + txtTelefon.Text + @"');
update na_stol set id_adresa_dostave =
(SELECT id from adresa_dostave where mjesto='" + txtMjesto.Text + @"' AND ulica='" + txtUlica.Text + @"' and kbr= '" + txtKbr.Text + @"' and telefon = '" + txtTelefon.Text + @"')
where id_stol = '" + stol + @"';";

                classSQL.update(sql);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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