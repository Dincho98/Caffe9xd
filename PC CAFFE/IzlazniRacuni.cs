using Microsoft.Reporting.WinForms;
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
    public partial class IzlazniRacuni : Form
    {
        private bool isFaktura = true;
        private bool slanjeDokumentacije;
        private string cmbIme;
        private DateTime? pocetniDatum;
        private DateTime? zavrsniDatum;

        public IzlazniRacuni(bool slanjeDokumentacije = false,string cmbIme=null,DateTime? pocetniDatum = null, DateTime? zavrsniDatum = null)
        {
            InitializeComponent();

            if (slanjeDokumentacije)
            {
                this.slanjeDokumentacije = slanjeDokumentacije;
                this.cmbIme = cmbIme;
                this.pocetniDatum = pocetniDatum;
                this.zavrsniDatum = zavrsniDatum;
            }
        }

        private void IzlazniRacuni_Load(object sender, EventArgs e)
        {
            OnFormLoad();

            if (slanjeDokumentacije)
            {
                DateTime PocetniDatum = GenerirajDatumSVremenom(Convert.ToDateTime(pocetniDatum), 0, 0, 1);
                DateTime ZavrsniDatum = GenerirajDatumSVremenom(Convert.ToDateTime(zavrsniDatum), 23, 59, 59);

                cbDatum.Checked = true;
                cmbDokument.SelectedValue = cmbIme;
                dtpOdDatuma.Value = PocetniDatum;
                dtpDoDatuma.Value = ZavrsniDatum;
                btnIspis.PerformClick();
            }
        }

        //Ova funkcija spaja datum i vrijeme u pravilnom formatu
        private DateTime GenerirajDatumSVremenom(DateTime datum,int hour,int minute, int sec)
        {
            string [] dateValues = datum.ToString().Split('.');
            return new DateTime(Int32.Parse(dateValues[2]), Int32.Parse(dateValues[1]), Int32.Parse(dateValues[0]), hour, minute, sec);
        }

        /// <summary>
        /// Method used to execute actions on Form Load
        /// </summary>
        private void OnFormLoad()
        {
            cbDatum.Checked = true;
            ManageDatumGroupBoxContent();
            FillComboBoxDokument();
        }

        /// <summary>
        /// Method used to manage "Brojevi" GroupBox content
        /// </summary>
        private void ManageBrojeviGroupBoxContent()
        {
            if (cbBrojevi.Checked)
                cbDatum.Checked = false;

            // Brojevi
            lblOdRacuna.Enabled = cbBrojevi.Checked;
            lblDoRacuna.Enabled = cbBrojevi.Checked;
            lblKalkulacija.Enabled = cbBrojevi.Checked;

            // Datum
            lblOdDatuma.Enabled = !cbBrojevi.Checked;
            lblDoDatuma.Enabled = !cbBrojevi.Checked;
            dtpOdDatuma.Enabled = !cbBrojevi.Checked;
            dtpDoDatuma.Enabled = !cbBrojevi.Checked;
        }

        /// <summary>
        /// Method used to manage "Datum" GroupBox content
        /// </summary>
        private void ManageDatumGroupBoxContent()
        {
            if (cbDatum.Checked)
                cbBrojevi.Checked = false;

            // Brojevi
            lblOdRacuna.Enabled = cbBrojevi.Checked;
            lblDoRacuna.Enabled = cbBrojevi.Checked;
            lblKalkulacija.Enabled = cbBrojevi.Checked;
            tbOdRacuna.Enabled = cbBrojevi.Checked;
            tbDoRacuna.Enabled = cbBrojevi.Checked;

            // Datum
            lblOdDatuma.Enabled = !cbBrojevi.Checked;
            lblDoDatuma.Enabled = !cbBrojevi.Checked;
        }

        /// <summary>
        /// Method used to fill ComboBox "cmbDokument"
        /// </summary>
        private void FillComboBoxDokument()
        {
            DataTable DTSK = new DataTable("IzlazniRacuni");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("kalk", "Kalkulacije");
            DTSK.Rows.Add("prim", "Primke");
            DTSK.Rows.Add("fakt", "Fakture");
            DTSK.Rows.Add("izd", "Izdatnica");
            DTSK.Rows.Add("otp_rob", "Otpis robe");

            cmbDokument.DataSource = DTSK;
            cmbDokument.DisplayMember = "naziv";
            cmbDokument.ValueMember = "id";

            GetSkladiste();
        }

        /// <summary>
        /// Method used to fill ComboBox "cmbSkladiste"
        /// </summary>
        private void GetSkladiste()
        {
            try
            {
                if ((cmbDokument.SelectedValue.ToString() != "fakt" || Util.Korisno.oibTvrtke != Class.Postavke.OIB_PC1))
                {
                    if (isFaktura)
                    {
                        DataTable DTSK1 = new DataTable("IzlazniRacuniSkladiste");

                        string sql = "SELECT * FROM skladiste";
                        DTSK1 = classSQL.select(sql, "skl").Tables[0];

                        cmbSkladiste.DataSource = DTSK1;
                        cmbSkladiste.DisplayMember = "skladiste";
                        cmbSkladiste.ValueMember = "id_skladiste";
                        isFaktura = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbBrojevi_CheckedChanged(object sender, EventArgs e)
        {
            ManageBrojeviGroupBoxContent();
        }

        private void cbDatum_CheckedChanged(object sender, EventArgs e)
        {
            ManageDatumGroupBoxContent();
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            if (cbDatum.Checked)
            {
                if (cmbDokument.SelectedValue.ToString() == "fakt")
                { 
                    Report.Kalkposkl.FaktureForm faktureForm = new Report.Kalkposkl.FaktureForm(slanjeDokumentacije);
                    faktureForm.datumOD = dtpOdDatuma.Value.Date;
                    faktureForm.datumDO = dtpDoDatuma.Value.Date;
                    faktureForm.ShowDialog();
                    if(slanjeDokumentacije)
                        this.Close();
                }

                if (cmbDokument.SelectedValue.ToString() == "kalk")
                {
                    Report.Kalkposkl.KalkulacijeForm kalkulacijeForm = new Report.Kalkposkl.KalkulacijeForm(slanjeDokumentacije);
                    kalkulacijeForm.datumOD = dtpOdDatuma.Value.Date;
                    kalkulacijeForm.datumDO = dtpDoDatuma.Value.Date;
                    kalkulacijeForm.ShowDialog();
                    if (slanjeDokumentacije)
                        this.Close();
                }

                if (cmbDokument.SelectedValue.ToString() == "prim")
                {
                    Report.Kalkposkl.PrimkeForm primkeForm = new Report.Kalkposkl.PrimkeForm(slanjeDokumentacije);
                    primkeForm.BrojFakDO = tbDoRacuna.Text;
                    primkeForm.documenat = cmbDokument.SelectedValue.ToString();
                    primkeForm.prema_rac = cbBrojevi.Checked;
                    primkeForm.bool1 = cbSkladiste.Checked;
                    primkeForm.skladiste_odabir = cmbSkladiste.SelectedValue.ToString();
                    primkeForm.skladiste = cmbSkladiste.Text;
                    primkeForm.BrojFakOD = tbOdRacuna.Text;
                    primkeForm.datumOD = dtpOdDatuma.Value.Date;
                    primkeForm.datumDO = dtpDoDatuma.Value.Date;
                    primkeForm.ShowDialog();
                    if (slanjeDokumentacije)
                        this.Close();
                }

                //Napravljeno samo za datume
                if (cmbDokument.SelectedValue.ToString() == "izd")
                {
                    Report.Izdatnica.FormIzdatnicaReport formIzdatnicaReport = new Report.Izdatnica.FormIzdatnicaReport(slanjeDokumentacije);
                    formIzdatnicaReport.datumOD = dtpOdDatuma.Value.Date;
                    formIzdatnicaReport.datumDO = dtpDoDatuma.Value.Date;
                    formIzdatnicaReport.ShowDialog();
                    if (slanjeDokumentacije)
                        this.Close();
                }

                //Napravljeno samo za datume
                if (cmbDokument.SelectedValue.ToString() == "otp_rob")
                {
                    Report.OtpisRobe.FormOtpisRobeReport formOtpisRobeReport = new Report.OtpisRobe.FormOtpisRobeReport(slanjeDokumentacije);
                    formOtpisRobeReport.datumOD = dtpOdDatuma.Value.Date;
                    formOtpisRobeReport.datumDO = dtpDoDatuma.Value.Date;
                    formOtpisRobeReport.ShowDialog();
                    if (slanjeDokumentacije)
                        this.Close();
                }
            }
        }
    }
}
