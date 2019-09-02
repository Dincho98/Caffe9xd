using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RESORT.Forme.izvjestaji.Avansi
{
    public partial class repAvans : Form
    {
        public repAvans()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string godina { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }

        private void repAvans_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            FillAvans(broj_dokumenta, godina);
            this.Text = ImeForme;
            this.reportViewer1.RefreshReport();
        }

        private void FillAvans(string broj, string godina)
        {
            RESORT.classNumberToLetter broj_u_text = new RESORT.classNumberToLetter();

            string sql = "SELECT " +
                " avansi.broj_avansa," +
                " avansi.dat_dok AS datum," +
                " avansi.dat_knj AS datum_knj," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " CAST (avansi.model AS nvarchar) + '  '+ CAST (avansi.broj_avansa AS nvarchar)+CAST (avansi.godina_avansa AS nvarchar)+'-'+CAST (avansi.broj_avansa AS nvarchar) AS model," +
                " avansi.opis," +
                " avansi.ukupno," +
                " avansi.osnovica10," +
                " avansi.porez_var as porez," +
                " avansi.osnovica_var as osnovica," +
                " avansi.godina_avansa," +
                " avansi.nult_stp," +
                " avansi.neoporezivo," +
                " avansi.jir," +
                " avansi.zki," +
                " '' as naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " ziro_racun.ziroracun AS ziro," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '' AS broj_slovima," +
                " partners.oib AS kupac_oib," +
                " porezi.iznos AS porez_postotak" +
                " FROM avansi" +
                " LEFT JOIN partners ON partners.id_partner=avansi.id_partner" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=avansi.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=avansi.ziro" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=avansi.id_zaposlenik_izradio" +
                " LEFT JOIN porezi ON porezi.id_porez=avansi.id_pdv" +
                " WHERE avansi.broj_avansa='" + broj + "'" +
                " AND avansi.godina_avansa='" + godina + "'";

            if (RemoteDB.remoteConnectionString == "")
            {
                classDBlite.LiteAdatpter(sql).Fill(dSAvans, "DTRAvans");
            }
            else
            {
                RemoteDB.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSAvans, "DTRAvans");
            }

            string ukupno = broj_u_text.PretvoriBrojUTekst(dSAvans.Tables[0].Rows[0]["ukupno"].ToString(), ',', "kn", "lp").ToString().ToLower();
            dSAvans.Tables[0].Rows[0]["broj_slovima"] = ukupno;

            string id_kupac = "";
            if (dSAvans.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSAvans.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjv ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string grad_kupac = "";
            DataTable DTgrad_kupac = RemoteDB.select("SELECT grad,posta FROM grad WHERE id_grad='" + id_kupac + "'", "grad").Tables[0];
            if (DTgrad_kupac.Rows.Count != 0)
            {
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
            }

            string sql1 = "SELECT " +
                " podaci_tvrtke.ime_tvrtke," +
                " podaci_tvrtke.skraceno_ime," +
                " podaci_tvrtke.oib," +
                " podaci_tvrtke.tel," +
                " podaci_tvrtke.fax," +
                " podaci_tvrtke.mob," +
                " podaci_tvrtke.iban," +
                " podaci_tvrtke.adresa," +
                " podaci_tvrtke.vl," +
                " podaci_tvrtke.r1," +
                " podaci_tvrtke.grad AS grad_kupac," +
                " podaci_tvrtke.poslovnica_adresa," +
                " podaci_tvrtke.poslovnica_grad," +
                " podaci_tvrtke.email," +
                " podaci_tvrtke.opis_na_kraju_fakture AS text_bottom " +
                " FROM podaci_tvrtke" +
                "";

            if (RemoteDB.remoteConnectionString == "")
            {
                classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            DataTable DTfis = classDBlite.LiteSelect("SELECT * FROM podaci_fiskalizacija", "podaci_fiskalizacija").Tables[0];
            string prodajnoMJ, broj_kase;
            DataTable DTducan = RemoteDB.select("SELECT ime_ducana FROM ducan WHERE id_ducan='" + DTfis.Rows[0]["oznakaPP"].ToString() + "'", "ducan").Tables[0];

            if (DTducan.Rows.Count > 0)
                prodajnoMJ = DTducan.Rows[0]["ime_ducana"].ToString();
            else
                prodajnoMJ = "1";

            DataTable DT = RemoteDB.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTfis.Rows[0]["oznaka_prodajnog_mj_avans"].ToString() + "'", "blagajna").Tables[0];
            if (DT.Rows.Count > 0)
                broj_kase = DT.Rows[0]["ime_blagajne"].ToString();
            else
                broj_kase = "1";

            string oznaka = dSRpodaciTvrtke.Tables[0].Rows[0]["r1"].ToString();
            string naslov = Convert.ToDecimal(dSAvans.Tables[0].Rows[0]["ukupno"].ToString()) <= 0 ? "Račun za predujam " + oznaka : "Storno račun za predujam " + oznaka;
            dSAvans.Tables[0].Rows[0]["naslov"] = naslov;
            dSAvans.Tables[0].Rows[0]["fiskalna_oznaka"] = "Fiskalna oznaka: " + broj + "/" + prodajnoMJ + "/" + broj_kase;
            this.reportViewer1.RefreshReport();
        }

        private void repAvans_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}