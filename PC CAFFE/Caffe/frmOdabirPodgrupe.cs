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
    }
}
