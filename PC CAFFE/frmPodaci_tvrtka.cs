using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class podloga : Form
    {
        public podloga()
        {
            InitializeComponent();
        }

        private void frmPodaci_tvrtka_Load(object sender, EventArgs e)
        {
            DataTable DTSK = new DataTable("nacin");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("R1", "Račun R1");
            DTSK.Rows.Add("R2", "Račun R2");

            cbR1.DataSource = DTSK;
            cbR1.DisplayMember = "naziv";
            cbR1.ValueMember = "id";

            SetValue();

            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void SetValue()
        {
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            DataSet DSgrad1 = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbPoslovnicaGrad.DataSource = DSgrad1.Tables[0];
            cbPoslovnicaGrad.DisplayMember = "grad";
            cbPoslovnicaGrad.ValueMember = "id_grad";

            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            txtImeTvrtke.Text = (DTpodaci.Rows[0]["ime_tvrtke"] == null || DTpodaci.Rows[0]["ime_tvrtke"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["ime_tvrtke"].ToString());
            txtSkracenoImeTvrtke.Text = (DTpodaci.Rows[0]["skracenoImeTvrtke"] == null || DTpodaci.Rows[0]["skracenoImeTvrtke"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["skracenoImeTvrtke"].ToString());
            txtSkraceno.Text = (DTpodaci.Rows[0]["skraceno_ime"] == null || DTpodaci.Rows[0]["skraceno_ime"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["skraceno_ime"].ToString().Replace(";", "\n"));
            txtOib.Text = (DTpodaci.Rows[0]["oib"] == null || DTpodaci.Rows[0]["oib"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["oib"].ToString());
            txtTelefon.Text = (DTpodaci.Rows[0]["tel"] == null || DTpodaci.Rows[0]["tel"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["tel"].ToString());
            txtFax.Text = (DTpodaci.Rows[0]["fax"] == null || DTpodaci.Rows[0]["fax"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["fax"].ToString());
            txtMobitel.Text = (DTpodaci.Rows[0]["mob"] == null || DTpodaci.Rows[0]["mob"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["mob"].ToString());
            txtNapomenaTransakcijski.Text = (DTpodaci.Rows[0]["napomenaTransa"] == null || DTpodaci.Rows[0]["napomenaTransa"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["napomenaTransa"].ToString());
            txtSwift.Text = (DTpodaci.Rows[0]["swift"] == null || DTpodaci.Rows[0]["swift"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["swift"].ToString());
            txtPdvBr.Text = (DTpodaci.Rows[0]["pdvBr"] == null || DTpodaci.Rows[0]["pdvBr"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["pdvBr"].ToString());
            txtAdresa.Text = (DTpodaci.Rows[0]["adresa"] == null || DTpodaci.Rows[0]["adresa"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["adresa"].ToString());
            txtVlasnik.Text = (DTpodaci.Rows[0]["vl"] == null || DTpodaci.Rows[0]["vl"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["vl"].ToString());
            txtZR.Text = (DTpodaci.Rows[0]["zr"] == null || DTpodaci.Rows[0]["zr"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["zr"].ToString());
            cbGrad.SelectedValue = (DTpodaci.Rows[0]["id_grad"] == null || DTpodaci.Rows[0]["id_grad"].ToString() == "0" ? 2806 : Convert.ToInt16((DTpodaci.Rows[0]["id_grad"].ToString().Length == 0 ? "2806" : DTpodaci.Rows[0]["id_grad"].ToString())));
            txtEmail.Text = (DTpodaci.Rows[0]["email"] == null || DTpodaci.Rows[0]["email"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["email"].ToString());
            txtNazivPoslovnice.Text = (DTpodaci.Rows[0]["nazivPoslovnice"] == null || DTpodaci.Rows[0]["nazivPoslovnice"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["nazivPoslovnice"].ToString());
            txtPoslovnicaAdresa.Text = (DTpodaci.Rows[0]["poslovnica_adresa"] == null || DTpodaci.Rows[0]["poslovnica_adresa"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["poslovnica_adresa"].ToString());
            cbPoslovnicaGrad.SelectedValue = (DTpodaci.Rows[0]["poslovnica_grad"] == null ? 2806 : Convert.ToInt16((DTpodaci.Rows[0]["poslovnica_grad"].ToString().Length == 0 ? "2806" : DTpodaci.Rows[0]["poslovnica_grad"].ToString())));
            txtIBAN.Text = (DTpodaci.Rows[0]["iban"] == null || DTpodaci.Rows[0]["iban"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["iban"].ToString());
            txtNaslovFakture.Text = (DTpodaci.Rows[0]["naziv_fakture"] == null || DTpodaci.Rows[0]["naziv_fakture"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["naziv_fakture"].ToString());

            rtbKrajDokumenta.Text = (DTpodaci.Rows[0]["text_bottom"] == null || DTpodaci.Rows[0]["text_bottom"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["text_bottom"].ToString());
            cbR1.SelectedValue = DTpodaci.Rows[0]["r1"].ToString();
            tbPPMIPO.Text = (DTpodaci.Rows[0]["sifra_ppmipo"] == null || DTpodaci.Rows[0]["sifra_ppmipo"].ToString().Length == 0 ? "-" : DTpodaci.Rows[0]["sifra_ppmipo"].ToString());
            tbEmailZaSlanjeDokUKnjigovodstvo.Text= (DTpodaci.Rows[0]["email_knjigovodstvo"].ToString()); // Ovak treba sve ici, ovo gore je neko sranje znapisano
        } 


        private void brnSpremi_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE podaci_tvrtka SET " +
                " ime_tvrtke='" + txtImeTvrtke.Text + "'," +
                " skracenoImeTvrtke='" + txtSkracenoImeTvrtke.Text + "'," +
                " skraceno_ime='" + txtSkraceno.Text.Replace("\n", ";") + "'," +
                " oib='" + txtOib.Text + "'," +
                " tel='" + txtTelefon.Text + "'," +
                " fax='" + txtFax.Text + "'," +
                " mob='" + txtMobitel.Text + "'," +
                " napomenaTransa='" + txtNapomenaTransakcijski.Text + "'," +
                " swift='" + txtSwift.Text + "'," +
                " pdvBr='" + txtPdvBr.Text + "'," +
                " adresa='" + txtAdresa.Text + "'," +
                " vl='" + txtVlasnik.Text + "'," +
                " zr='" + txtZR.Text + "'," +
                " id_grad='" + cbGrad.SelectedValue + "'," +
                " email='" + txtEmail.Text + "'," +
                " text_bottom='" + rtbKrajDokumenta.Text + "'," +
                " nazivPoslovnice='" + txtNazivPoslovnice.Text + "'," +
                " poslovnica_adresa='" + txtPoslovnicaAdresa.Text + "'," +
                " poslovnica_grad='" + cbPoslovnicaGrad.SelectedValue + "'," +
                " iban='" + txtIBAN.Text + "'," +
                " naziv_fakture='" + txtNaslovFakture.Text + "'," +
                " naslov_racuna='" + cbR1.Text + "  " + "'," +
                " r1='" + cbR1.SelectedValue + "'," +
                " sifra_ppmipo='" + tbPPMIPO.Text + "'," +
                " email_knjigovodstvo='"+tbEmailZaSlanjeDokUKnjigovodstvo.Text+"'"+
                " WHERE id='1'" +
                "";

            classSQL.Setings_Update(sql);
            Class.PodaciTvrtka.getPodaci();
            MessageBox.Show("Spremljeno");
        }
    }
}