using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Primka
{
    public partial class frmPrimkaIspis : Form
    {
        public frmPrimkaIspis()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string is_kalkulacija { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public string idPrimka { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmPrimkaIspis_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            //dokumenat = "primka";
            //broj_dokumenta = "1";
            //skladiste = "2";

            if (dokumenat == "primka")
            {
                Primka();
            }

            this.reportViewer1.RefreshReport();
        }

        private void Primka()
        {
            string sql1 = "SELECT " +
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
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_stavke = "SELECT " +
                " primka_stavke.sifra," +
                " roba_prodaja.naziv," +
                " roba_prodaja.mjera AS jmj," +
                " primka_stavke.u_pakiranju cijena2," +
                " primka_stavke.kolicina cijena3," +
                " primka_stavke.cijena_po_komadu cijena4," +
                " primka_stavke.rabat cijena5," +
                " primka_stavke.nabavna_cijena*primka_stavke.kolicina cijena6," +
                " primka_stavke.ulazni_porez cijena7," +
                " primka_stavke.broj_paketa cijena1," +
                " primka_stavke.iznos cijena8," +
                " primka_stavke.povratna_naknada*primka_stavke.kolicina AS cijena10," +
                " primka_stavke.nabavni_iznos" +
                //" primka_stavke.id_skladiste " +
                " FROM primka_stavke" +
                " LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke  AND primka.is_kalkulacija='0'" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=primka_stavke.sifra WHERE primka_stavke.broj_primke='" + broj_dokumenta + "'" +
                " AND primka_stavke.is_kalkulacija='0' AND primka_stavke.id_skladiste='" + skladiste + "' AND primka.id ='" + idPrimka +"'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_stavke).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_stavke).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select($@"SELECT datum FROM primka WHERE id='{idPrimka}' AND is_kalkulacija='0'", "primka").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " partners.ime_tvrtke AS string1," +
                " partners.adresa AS string2," +
                " partners.oib AS string4," +
                " grad.grad +' '+ grad.posta AS string3," +
                " partners.id_partner AS string5," +
                " primka.br_ulaznog_doc AS string9," +
                " primka.datum AS datum1," +
                " primka.napomena AS string8," +
                " skladiste.skladiste AS string10," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string11," +
                " CAST ('PRIMKA  ' AS nvarchar) + CAST (primka.broj_primke AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM primka " +
                " LEFT JOIN partners ON partners.id_partner=primka.id_partner " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=primka.id_skladiste " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=primka.id_zaposlenik " +
                " WHERE primka.id ='" + idPrimka + "' AND primka.is_kalkulacija='0' AND primka.id_skladiste='" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}