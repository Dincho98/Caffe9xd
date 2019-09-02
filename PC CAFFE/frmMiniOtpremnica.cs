using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmMiniOtpremnica : Form
    {
        public DataGridView dgv { get; set; }
        public bool koristi_sobu { get; private set; }

        private bool partnerExists = false;

        public frmMiniOtpremnica()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbNaPartnera.Checked)
                {
                    partnerExists = false;
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPatner.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            partnerExists = true;
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        }
                    }
                }
                else
                {
                    dgvSobeVisible(!dgvSobe.Visible);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbNaPartnera.Checked && partnerExists && dgv != null)
                {
                    Class.Otpremnica _otpremnica = new Class.Otpremnica(Convert.ToInt32(txtSifraPatner.Text), dtpDatum.Value, rtbNapomena.Text, false);
                    _otpremnica.izracun(dgv);
                    string broj_otpremnice = _otpremnica.BrojOtpremnice.ToString();
                    if (_otpremnica.otpremnicaSpremi(dgv, ref broj_otpremnice, _otpremnica.IdSkladiste.ToString(), dtpDatum.Value, _otpremnica.IdDjelatnik.ToString(), _otpremnica.IdOdrediste.ToString(), _otpremnica.IdPartner.ToString(), _otpremnica.Osoba, _otpremnica.Napomena, _otpremnica.Godina.ToString(), _otpremnica.IdKomercijalista.ToString()))
                    {
                        DataTable DTsend = _otpremnica.DtOtpremnicaStavke;

                        try
                        {
                            PosPrint.classPosPrintOtpremnice.PrintReceipt(DTsend, Properties.Settings.Default.id_zaposlenik, broj_otpremnice + "/" + DateTime.Now.Year.ToString(), txtSifraPatner.Text, "", broj_otpremnice, "", 0);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
                        }

                        DialogResult = DialogResult.OK;
                    }
                }
                else if (rbNaSobu.Checked && txtSifraPatner.Text.Length > 0)
                {
                    Class.Otpremnica _otpremnica = new Class.Otpremnica(Convert.ToInt32(txtSifraPatner.Text), dtpDatum.Value, rtbNapomena.Text, true);
                    _otpremnica.izracun(dgv);
                    string broj_otpremnice = _otpremnica.BrojOtpremnice.ToString();
                    if (_otpremnica.otpremnicaSpremi(dgv, ref broj_otpremnice, _otpremnica.IdSkladiste.ToString(), dtpDatum.Value, _otpremnica.IdDjelatnik.ToString(), _otpremnica.IdOdrediste.ToString(), _otpremnica.IdPartner.ToString(), _otpremnica.Osoba, _otpremnica.Napomena, _otpremnica.Godina.ToString(), _otpremnica.IdKomercijalista.ToString()))
                    {
                        DataTable DTsend = _otpremnica.DtOtpremnicaStavke;

                        try
                        {
                            PosPrint.classPosPrintOtpremnice.PrintReceipt(DTsend, Properties.Settings.Default.id_zaposlenik, broj_otpremnice + "/" + DateTime.Now.Year.ToString(), txtSifraPatner.Text, "", broj_otpremnice, "", 0, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
                        }

                        DialogResult = DialogResult.OK;
                    }
                }

                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
        }

        private void frmMiniOtpremnica_Load(object sender, EventArgs e)
        {
            try
            {
                rbNaPartnera.Checked = true;

                string sql = string.Format(@"select id, naziv_sobe as naziv, broj_lezaja as lezaji, cijena_nocenja as cijena
from sobe
where aktivnost = 1
order by naziv_sobe asc;");

                DataSet dsSobe = classSQL.select(sql, "sobe");

                if (dsSobe != null && dsSobe.Tables.Count > 0 && dsSobe.Tables[0] != null)
                {
                    dgvSobe.DataSource = dsSobe.Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbNaPartnera_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbNaPartnera.Checked)
                {
                    lblNazivNa.Text = "Poslovni partner";
                }
                else
                {
                    lblNazivNa.Text = "Hotelska soba";
                }
                dgvSobeVisible(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvSobeVisible(bool visible = false)
        {
            dgvSobe.Enabled = visible;
            dgvSobe.Visible = visible;
            btnDown.Enabled = visible;
            btnDown.Visible = visible;
            btnUp.Enabled = visible;
            btnUp.Visible = visible;

            if (visible)
            {
                dgvSobe.BringToFront();
            }
            else
            {
                dgvSobe.SendToBack();
            }
        }

        private void dgvSobe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSobe.Rows.Count == 0)
                {
                    MessageBox.Show("Nema kreiranih soba.");
                    rbNaPartnera.Checked = true;
                    return;
                }

                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                if (MessageBox.Show("Želite dodati artikle na odabranu sobu?", "Otpremnica na sobu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                txtSifraPatner.Text = dgvSobe.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtNazivPartner.Text = dgvSobe.Rows[e.RowIndex].Cells["naziv"].Value.ToString();
                dgvSobeVisible(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                string controlName = (sender as Button).Tag.ToString().Trim();
                DataGridView dgv = (this.Controls.Find(controlName, true)[0] as DataGridView);
                int step = 5;
                int.TryParse(dgv.Tag.ToString(), out step);
                for (int i = 0; i < step; i++)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex + 1 < dgv.Rows.Count)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex + 1;
                    }
                    else
                    {
                        return;
                    }
                }
                controlName = null;
                dgv = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                string controlName = (sender as Button).Tag.ToString().Trim();
                DataGridView dgv = (this.Controls.Find(controlName, true)[0] as DataGridView);
                int step = 5;
                int.TryParse(dgv.Tag.ToString(), out step);
                for (int i = 0; i < step; i++)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex - 1 >= 0)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex - 1;
                    }
                    else
                    {
                        return;
                    }
                }

                controlName = null;
                dgv = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}