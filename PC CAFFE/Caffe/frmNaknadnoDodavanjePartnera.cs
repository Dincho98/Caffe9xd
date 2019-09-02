using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmNaknadnoDodavanjePartnera : Form
    {
        private string patner;
        private int id_partner, id_ducan, id_kasa, broj_racuna;
        private DataTable postavke;

        public frmNaknadnoDodavanjePartnera()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNaknadnoDodavanjePartnera_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
                postavke = classSQL.select_settings("select * from postavke;", "postavke").Tables[0];
                id_ducan = Convert.ToInt32(postavke.Rows[0]["default_ducan"]);
                id_kasa = Convert.ToInt32(postavke.Rows[0]["default_blagajna"]);

                txtBrojRacuna.Text = (classSQL.select("select coalesce(max(broj_racuna::numeric), 0) as lastBrojRacuna from racuni where id_ducan = '" + id_ducan + "' and id_kasa = '" + id_kasa + "'", "racuni").Tables[0].Rows[0]["lastBrojRacuna"]).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNaknadnoDodavanjePartnera_Click(object sender, EventArgs e)
        {
            try
            {
                string partner;
                int broj_racuna = 0;

                if (!int.TryParse(txtBrojRacuna.Text, out broj_racuna))
                {
                    MessageBox.Show("Neispravni broj računa.");
                    return;
                }

                partner = classSQL.select("select coalesce(id_kupac, 0) as id_partner from racuni where broj_racuna = '" + broj_racuna + "' and id_ducan = '" + id_ducan + "' and id_kasa = '" + id_kasa + "'", "racuni").Tables[0].Rows[0]["id_partner"].ToString();

                if (partner.Length > 0 && partner != "0")
                {
                    MessageBox.Show("Dodavanje partnera na R1 račun nije dozvoljeno.");
                    return;
                }

                if (MessageBox.Show("Želite naknadno dodati partnera na odabrani račun?", "R1 račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    frmPartnerTrazi f = new frmPartnerTrazi();
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        partner = classSQL.select("select ime_tvrtke from partners where id_partner = '" + Properties.Settings.Default.id_partner + "';", "partners").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        if (MessageBox.Show("Sigurno želite dodati partnera " + partner + " na račun broj " + broj_racuna, "R1 račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string sql = "update racuni set id_kupac = '" + Properties.Settings.Default.id_partner + "' where broj_racuna = '" + broj_racuna + "' and id_ducan = '" + id_ducan + "' and id_kasa = '" + id_kasa + "';";
                            classSQL.update(sql);
                            MessageBox.Show("Izvršeno");

                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                }
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