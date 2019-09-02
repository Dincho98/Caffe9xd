using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class SveUskladaRobeNaSkladistuForm : Form
    {
        UskladaSkladistaForm uskladaSkladistaForm;
        public SveUskladaRobeNaSkladistuForm(UskladaSkladistaForm u)
        {
            uskladaSkladistaForm = u;
            InitializeComponent();
            dohvatiPodatkeOUskladamaRobe();
        }
        private void dohvatiPodatkeOUskladamaRobe()
        {
            string sql = "select ur.id_usklade,ur.datum,ur.napomena,zaposlenici.ime,zaposlenici.prezime from usklada_robe ur join zaposlenici on zaposlenici.id_zaposlenik=ur.izradio WHERE obrisano=0";
            DataSet DSUskladeRobe = classSQL.select(sql, "usklada_robe");
            sveUskladeView.DataSource = DSUskladeRobe.Tables[0];
            sveUskladeView.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Uskladaskladista.UskladaSkladistaReportForm uskladaSkladistaReport = new Report.Uskladaskladista.UskladaSkladistaReportForm();
            uskladaSkladistaReport.broj_dokumenta = int.Parse(sveUskladeView.CurrentRow.Cells[0].FormattedValue.ToString());
            //aa.skladiste = int.Parse(sveUskladeView.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString());
            uskladaSkladistaReport.ShowDialog();
            //MessageBox.Show("BLAAAAAAA");

        }

        private void sveUskladeView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UrediUskladuButton_Click(object sender, EventArgs e)
        {
            if (sveUskladeView.SelectedRows.Count < 1)
            {
                MessageBox.Show("Niste odabrali dokument!");
            }
            else if (sveUskladeView.SelectedRows.Count > 1)
            {
                MessageBox.Show("Odaberite jedan dokument!");
            }
            else
            {
                UskladaSkladistaForm urediUskladu = new UskladaSkladistaForm();
                urediUskladu.MdiParent = this.MdiParent;
                urediUskladu.Dock = DockStyle.Fill;
                urediUskladu.broj_usklade_edit = sveUskladeView.CurrentRow.Cells[0].Value.ToString();
                uskladaSkladistaForm.Close();
                this.Close();
                urediUskladu.Show();
            }
            Report.Uskladaskladista.UskladaSkladistaReportForm aa = new Report.Uskladaskladista.UskladaSkladistaReportForm();
            aa.broj_dokumenta = int.Parse(sveUskladeView.CurrentRow.Cells[0].FormattedValue.ToString());
            aa.ShowDialog();
        }
    }
}
