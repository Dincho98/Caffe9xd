using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmSveOtpremnice : Form
    {
        public frmSveOtpremnice()
        {
            InitializeComponent();
        }

        private void frmSveOtpremnice_Load(object sender, EventArgs e)
        {
            try
            {
                btnDodajOdabrano.Enabled = false;
                btnUp.Click += Funkcije.btnScroll_Click;
                btnDown.Click += Funkcije.btnScroll_Click;
                postaviVrijednostTextbox();
                prikaziSobe(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSrchSoba_Click(object sender, EventArgs e)
        {
            try
            {
                prikaziSobe(!pnlSobe.Visible);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void prikaziSobe(bool show = false)
        {
            pnlSobe.Enabled = show;
            pnlSobe.Visible = show;

            if (show)
            {
                pnlSobe.BringToFront();
                //dohvacaje soba u datagridview
                string sql = string.Format(@"select s.id, s.broj_sobe, ts.tip as tip_sobe, s.naziv_sobe, s.broj_lezaja, s.cijena_nocenja
from sobe s
left join tip_sobe ts on s.id_tip_sobe = ts.id
where s.aktivnost = 1
order by s.broj_sobe asc;");

                DataSet dsSobe = RemoteDB.select(sql, "sobe");
                dgvSobe.Rows.Clear();
                if (dsSobe != null && dsSobe.Tables.Count > 0 && dsSobe.Tables[0] != null && dsSobe.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsSobe.Tables[0].Rows)
                    {
                        dgvSobe.Rows.Add(dRow["id"], dRow["broj_sobe"], dRow["tip_sobe"], dRow["naziv_sobe"], dRow["broj_lezaja"], dRow["cijena_nocenja"]);
                    }
                }
            }
            else
            {
                pnlSobe.SendToBack();
            }
        }

        private void postaviVrijednostTextbox(int id = 0, int broj_sobe = 0, string naziv_sobe = null)
        {
            try
            {
                if (id == 0 && broj_sobe == 0 && naziv_sobe == null)
                {
                    txtBrojSobe.Tag = id;
                    txtBrojSobe.Text = "";
                    txtNazivSobe.Text = txtBrojSobe.Text;
                }
                else
                {
                    txtBrojSobe.Tag = id;
                    txtBrojSobe.Text = broj_sobe.ToString(); ;
                    txtNazivSobe.Text = naziv_sobe;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDodajSobu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSobe.Rows.Count == 0 || dgvSobe.SelectedRows.Count == 0)
                    return;

                if (MessageBox.Show("Želite dodati odabranu sobu?", "Otpremnice na sobu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                int id = 0, broj_sobe = 0;
                string naziv_sobe = null;
                int.TryParse(dgvSobe.SelectedRows[0].Cells["id"].Value.ToString(), out id);
                int.TryParse(dgvSobe.SelectedRows[0].Cells["broj_sobe"].Value.ToString(), out broj_sobe);
                naziv_sobe = dgvSobe.SelectedRows[0].Cells["naziv_sobe"].Value.ToString();

                postaviVrijednostTextbox(id, broj_sobe, naziv_sobe);

                prikaziSobe(false);
                uzmiOtpremniceIzSobe();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void uzmiOtpremniceIzSobe()
        {
            try
            {
                string sql = string.Format(@"select o.broj_otpremnice as broj, o.datum, concat(z.ime, ' ', z.prezime) as zaposlenik, s.naziv_sobe as soba, o.ukupno::numeric as ukupno, o.napomena as napomena
from otpremnice o
left join sobe s on o.osoba_partner = s.id
left join zaposlenici z on o.id_izradio = z.id_zaposlenik
where na_sobu = true and rfaktura_broj = 0 and rfaktura_poslovnica = 0 and rfaktura_naplatni_uredaj = 0
and s.id = {0}
order by broj asc;", Convert.ToInt32(txtBrojSobe.Tag));

                DataSet dsOtpremnice = RemoteDB.select(sql, "otpremnice");
                dgvOtpremnice.Rows.Clear();
                btnDodajOdabrano.Enabled = false;
                if (dsOtpremnice != null && dsOtpremnice.Tables.Count > 0 && dsOtpremnice.Tables[0] != null && dsOtpremnice.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsOtpremnice.Tables[0].Rows)
                    {
                        dgvOtpremnice.Rows.Add(dRow["broj"], dRow["datum"], dRow["zaposlenik"], dRow["soba"], dRow["ukupno"], dRow["napomena"]);
                    }
                    btnDodajOdabrano.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDodajOdabrano_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOtpremnice.Rows.Count == 0)
                {
                    return;
                }

                int totalCount = dgvOtpremnice.Rows.Cast<DataGridViewRow>().Count(r => (Convert.ToBoolean(r.Cells["dodaj"].Value)) == true);
                if (totalCount == 0)
                {
                    MessageBox.Show("Nemate odabrane otpremnice.");
                    return;
                }

                DialogResult = DialogResult.Yes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}