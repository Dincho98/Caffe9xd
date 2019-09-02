using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPromjenaCijene : Form
    {
        public frmPromjenaCijene()
        {
            InitializeComponent();
        }

        private DataTable DS_Skladiste = new DataTable();

        public string idSkladiste { get; set; }
        public string sifra { get; set; }
        public string porez { get; set; }

        private void frmPromjenaCijene_Load(object sender, EventArgs e)
        {
            txtMpc.Select();
            SetSkladiste();
        }

        private void SetSkladiste()
        {
            //DS skladiste
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1') ORDER BY skladiste", "skladiste").Tables[0];
            CBskladiste.DataSource = DS_Skladiste;
            CBskladiste.DisplayMember = "skladiste";
            CBskladiste.ValueMember = "id_skladiste";

            CBskladiste.SelectedValue = idSkladiste;
        }

        private void txtMpc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVpc.Select();
        }

        private void txtVpc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CBskladiste.Select();
        }

        private void CBskladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPromjena.PerformClick();
        }

        private void txtMpc_Leave(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (Decimal.TryParse(txtMpc.Text, out dec_parse))
            {
                txtVpc.Text = Convert.ToDouble(Convert.ToDouble(txtMpc.Text) / Convert.ToDouble("1," + porez)).ToString("#0.0000");
            }
            else
            {
                MessageBox.Show("Greška kod upisa mpc-a.", "Greška");
                txtMpc.Text = "0";
                return;
            }
        }

        private void btnPromjena_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE roba SET vpc='" + txtVpc.Text.Replace(",", ".") + "',mpc='" + txtMpc.Text.Replace(".", ",") + "' WHERE sifra='" + sifra + "'";
            classSQL.update(sql);

            DataTable DT = classSQL.select("SELECT sifra FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'", "roba_prodaja").Tables[0];

            if (DT.Rows.Count != 0)
            {
                string sql1 = "UPDATE roba_prodaja SET vpc='" + txtVpc.Text.Replace(",", ".") + "' WHERE sifra='" + sifra + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'";
                classSQL.update(sql1);
            }

            this.Close();
        }
    }
}