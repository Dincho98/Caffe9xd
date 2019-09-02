using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.IzlazniDokumenti
{
    public partial class ObracunForm : Form
    {
        private bool SpremanjeDokumenta;
        DateTime PocetniDatum;
        DateTime ZavrsniDatum;

        public ObracunForm(bool spremanjeDokumenta=false, DateTime? pocetniDatum=null, DateTime? zavrsniDatum=null)
        {
            InitializeComponent();
            SpremanjeDokumenta = spremanjeDokumenta;
            PocetniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(pocetniDatum), 0, 0, 1);
            ZavrsniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(zavrsniDatum), 23, 59, 59);
        }

        private void ObracunForm_Load(object sender, EventArgs e)
        {
            if (SpremanjeDokumenta)
            {
                Report.IzlazniDokumenti.ObracunReport form = new Report.IzlazniDokumenti.ObracunReport(true);
                form.datumOd = PocetniDatum;
                form.datumDo = ZavrsniDatum;
                form.ShowDialog();
                this.Close();
            }
        }

        private void btnIspisi_Click(object sender, EventArgs e)
        {
            if (dtpOd.Value < dtpDo.Value)
            {
                Report.IzlazniDokumenti.ObracunReport form = new Report.IzlazniDokumenti.ObracunReport();
                form.datumOd = dtpOd.Value;
                form.datumDo = dtpDo.Value;
                form.ShowDialog();
            }
            else
                MessageBox.Show("\"Datum od\" ne može biti veći od \"Datum do\".");

        }

    }
}
