using System;
using System.Windows.Forms;

namespace RESORT.Fiskalizacija
{
    public partial class frmPregledNefiskaliziranog : Form
    {
        public frmPregledNefiskaliziranog()
        {
            InitializeComponent();
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
            RemoteDB.update(string.Format(@"UPDATE neuspjela_fiskalizacija
SET
    xml = '{0}',
    greska = '{1}'
WHERE broj_racuna = '{2}' AND id_ducan = '{3}' AND id_kasa = '{4}';",
                poslano,
                greska,
                brojRacuna,
                ducan,
                blagajna));

            MessageBox.Show("Spremljeno");
            this.Close();
        }
    }
}