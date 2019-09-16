using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOdabirPodgrupe : Form
    {
        public int IdPodgrupa = 0;

        public frmOdabirPodgrupe()
        {
            InitializeComponent();
        }

        private void BtnPice_Click(object sender, EventArgs e)
        {
            IdPodgrupa = 1;
            Close();
        }

        private void BtnHrana_Click(object sender, EventArgs e)
        {
            IdPodgrupa = 2;
            Close();
        }

        private void BtnTrgovackaRoba_Click(object sender, EventArgs e)
        {
            IdPodgrupa = 3;
            Close();
        }

        private void btnProizvoljno_Click(object sender, EventArgs e)
        {
            if (gumbicProizvoljnoOmogucen())
            {
                IdPodgrupa = 4;
            }
            Close();
        }

        private bool gumbicProizvoljnoOmogucen()
        {
            bool gumbicOmogucen= true;
            string sql = "SELECT * FROM podgrupa";
            int numRows=classSQL.select(sql, "podgrupa").Tables[0].Rows.Count;
            if (numRows != 4)
            {
                MessageBox.Show("Potrebno je nadograditi bazu kako bi mogli dodavati Proizvoljnu napomenu.","Upozorenje",MessageBoxButtons.OK,MessageBoxIcon.Error);
                gumbicOmogucen = false;
            }
            return gumbicOmogucen;
        }
    }
}
