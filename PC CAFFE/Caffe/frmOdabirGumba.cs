using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOdabirGumba : Form
    {
        public frmOdabirGumba()
        {
            InitializeComponent();
        }

        public Caffe.frmProdajniArtikli _PrArtikli { get; set; }

        private void frmOdabirGumba_Load(object sender, EventArgs e)
        {
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            _PrArtikli.btnPromjenaIzgledaNaziv = btn.Name;
            this.Close();
        }
    }
}