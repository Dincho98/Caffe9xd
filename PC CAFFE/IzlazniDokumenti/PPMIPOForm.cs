using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.IzlazniDokumenti
{
    public partial class PPMIPOForm : Form
    {
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        private bool SlanjeDokumenta;
        private DateTime PocetniDatum;
        private DateTime ZavrsniDatum;

        DataTable DTpnp = new DataTable();

        public PPMIPOForm(bool slanjeDokumenata=false, DateTime? pocetniDatum=null, DateTime? zavrsniDatum=null)
        {
            InitializeComponent();
            LoadDucan();

            if (slanjeDokumenata)
            {
                SlanjeDokumenta = slanjeDokumenata;
                PocetniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(pocetniDatum),0,0,1);
                ZavrsniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(zavrsniDatum), 23, 59, 59);

            }
        }

        private void PPMIPOForm_Load(object sender, EventArgs e)
        {
            if (SlanjeDokumenta)
            {
                Report.PPMIPO.PnpReportForm form = new Report.PPMIPO.PnpReportForm(true);
                form.datumOd = PocetniDatum;
                form.datumDo = ZavrsniDatum;
                form.idDucan = cbDucan.SelectedValue.ToString();
                form.ShowDialog();
                this.Close();
            }
        }

        private void LoadDucan()
        {
            DataTable DTducan = classSQL.select("SELECT * FROM ducan where aktivnost = 'DA';", "ducan").Tables[0];
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";
            cbDucan.DataSource = DTducan;


        }

        private void LoadCompanyData()
        {
            string query = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " podaci_tvrtka.sifra_ppmipo," +
                " grad.grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";

            classSQL.CeAdatpter(query).Fill(dsRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private void LoadPorezNaPotrosnjuData()
        {
            string query = $@"SELECT ROUND(CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric), 0) AS porez_potrosnja,
	                            SUM(racun_stavke.vpc * CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) AS ukupno,
                                SUM(racun_stavke.vpc * CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS numeric)) * (MAX(CAST(REPLACE(racun_stavke.porez_potrosnja, ',', '.') AS numeric)) / 100) AS pnp
                              FROM racun_stavke
                              LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan
                              WHERE CAST(REPLACE(porez_potrosnja, ',', '.') AS numeric) > 0 AND racuni.id_ducan = {cbDucan.SelectedValue}
                                AND racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'
                              GROUP BY racun_stavke.porez_potrosnja";

            DTpnp = classSQL.select(query, "DTlisteTekst").Tables[0];
        }

        private void SetDates()
        {
            datumOd = dtpDatumOd.Value;
            datumDo = dtpDatumDo.Value;
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            Report.PPMIPO.PnpReportForm form = new Report.PPMIPO.PnpReportForm();
            form.datumOd = dtpDatumOd.Value.Date;
            form.datumDo = dtpDatumDo.Value.Date;
            form.idDucan = cbDucan.SelectedValue.ToString();
            form.ShowDialog();
            if (SlanjeDokumenta)
                this.Close();
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            SetDates();
            LoadCompanyData();
            LoadPorezNaPotrosnjuData();
            GenerateXML();
        }

        private void GenerateXML()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                decimal osnovica = 0;
                decimal stopa = 0;
                decimal pnp = 0;
                for (int x = 0; x < DTpnp.Rows.Count; x++)
                {
                    osnovica += Convert.ToDecimal(DTpnp.Rows[x]["ukupno"].ToString());
                    stopa = Convert.ToDecimal(DTpnp.Rows[x]["porez_potrosnja"].ToString());
                    pnp += Convert.ToDecimal(DTpnp.Rows[x]["pnp"].ToString());
                }

                string path = Path.GetFullPath(saveFileDialog.FileName) + ".txt";
                string text = $@"<ObrazacPPMIPO xmlns='http://e-porezna.porezna-uprava.hr/sheme/zahtjevi/ObrazacPPMIPO/v1-0' verzijaSheme='1.0'>
  <Metapodaci xmlns='http://e-porezna.porezna-uprava.hr/sheme/Metapodaci/v2-0'>
    <Naslov dc='http://purl.org/dc/elements/1.1/title'>Izvješče o obračunu poreza na potrošnju</Naslov>
    <Autor dc='http://purl.org/dc/elements/1.1/creator'></Autor>
    <Datum dc='http://purl.org/dc/elements/1.1/date'>{DateTime.Now}</Datum>
    <Format dc='http://purl.org/dc/elements/1.1/format'>text/xml</Format>
    <Jezik dc='http://purl.org/dc/elements/1.1/language'>hr-HR</Jezik>
    <Identifikator dc='http://purl.org/dc/elements/1.1/identifier'>98c26b02-eb67-dfad-bbae-58da06a48773</Identifikator>
    <Uskladjenost dc='http://purl.org/dc/terms/conformsTo'>ObrazacPPMIPO-v1-0</Uskladjenost>
    <Tip dc='http://purl.org/dc/elements/1.1/type'>Elektronički obrazac</Tip>
    <Adresant>Ministarstvo Financija, Porezna uprava, {dsRpodaciTvrtke.Tables[0].Rows[0]["grad"]}</Adresant>
  </Metapodaci>
  <Zaglavlje>
    <Razdoblje>
      <DatumOd>{datumOd.Date}</DatumOd>
      <DatumDo>{datumDo.Date}</DatumDo>
    </Razdoblje>
    <Obveznik>
      <Naziv>{dsRpodaciTvrtke.Tables[0].Rows[0]["ime_tvrtke"]}</Naziv>
      <OIB>{dsRpodaciTvrtke.Tables[0].Rows[0]["oib"]}</OIB>
      <Adresa>
        <Mjesto>{dsRpodaciTvrtke.Tables[0].Rows[0]["grad"]}</Mjesto>
        <Ulica>{dsRpodaciTvrtke.Tables[0].Rows[0]["adresa"]}</Ulica>
        <Broj></Broj>
      </Adresa>
    </Obveznik>
    <Ispostava>0</Ispostava>
  </Zaglavlje>
    <Tijelo>
      <Obracuni>
        <Obracun>
          <RedniBroj>1</RedniBroj>
            <SifraOpcineGrada>{dsRpodaciTvrtke.Tables[0].Rows[0]["sifra_ppmipo"]}</SifraOpcineGrada>
            <NazivOpcineGrada>{dsRpodaciTvrtke.Tables[0].Rows[0]["grad"]}</NazivOpcineGrada>
            <BrojObjekata>3</BrojObjekata>
            <Osnovica>{Math.Round(osnovica, 2, MidpointRounding.AwayFromZero)}</Osnovica>
            <Stopa>{stopa}</Stopa>
            <Porez>{Math.Round(pnp, 2, MidpointRounding.AwayFromZero)}</Porez>
        </Obracun>
      </Obracuni>
        <ObracuniUkupno>
          <UkBrojObjekata>3</UkBrojObjekata>
          <UkOsnovica>{Math.Round(osnovica, 2, MidpointRounding.AwayFromZero)}</UkOsnovica>
          <UkPorez>{Math.Round(pnp, 2, MidpointRounding.AwayFromZero)}</UkPorez>
        </ObracuniUkupno>
    </Tijelo>
  </ObrazacPPMIPO>";
                File.WriteAllText(path, text);
            }
        }

    }
}
