using System;
using System.Windows.Forms;

namespace PCPOS.Fiskalizacija
{
    public partial class frmPregledNefiskaliziranog : Form
    {
        public frmPregledNefiskaliziranog()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string brojRacuna { get; set; }
        public string poslano { get; set; }
        public string greska { get; set; }
        public string ducan { get; set; }
        public string blagajna { get; set; }
        public string datum { get; set; }

        private void frmPregledNefiskaliziranog_Load(object sender, EventArgs e)
        {
            txtBrojRacuna.Text = brojRacuna;
            txtDucan.Text = ducan;
            txtBlagajna.Text = blagajna;
            txtPoslano.Text = poslano;
            txtGreška.Text = greska;
            txtDatum.Text = datum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            classSQL.update("UPDATE neuspjela_fiskalizacija SET " +
                " xml='" + poslano + "'," +
                " greska='" + greska + "' WHERE broj_racuna='" + brojRacuna + "' AND id_ducan='" + ducan + "' AND id_kasa='" + blagajna + "'" +
                " ");

            MessageBox.Show("Spremljeno");
            this.Close();
        }
    }
}