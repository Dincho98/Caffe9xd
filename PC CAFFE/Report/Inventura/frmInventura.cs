using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Inventura
{
    public partial class frmInventura : Form
    {
        public frmInventura()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void frmInventura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 140;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 301, 5);

            LoadDocument();
            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
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

            string sql_liste = "SELECT " +
                " inventura_stavke.sifra_robe AS sifra," +
                " REPLACE(inventura_stavke.kolicina,',','.') AS cijena4," +
                " inventura_stavke.naziv," +
                " inventura_stavke.kolicina_koja_je_bila_na_skl AS cijena2," +
                " inventura_stavke.cijena AS cijena1," +
                " roba_prodaja.ulazni_porez," +
                " (inventura_stavke.cijena+inventura_stavke.povratna_naknada)*inventura_stavke.kolicina_koja_je_bila_na_skl as cijena3," +
                " inventura_stavke.kolicina as cijena4," +
                " (inventura_stavke.cijena+inventura_stavke.povratna_naknada)*CAST(REPLACE(inventura_stavke.kolicina,',','.') AS numeric) as cijena5," +
                " CAST(REPLACE(inventura_stavke.kolicina,',','.') AS numeric)-inventura_stavke.kolicina_koja_je_bila_na_skl as cijena6," +
                " (CAST(REPLACE(inventura_stavke.kolicina,',','.') AS numeric)-inventura_stavke.kolicina_koja_je_bila_na_skl) * (inventura_stavke.cijena+inventura_stavke.povratna_naknada) as cijena7," +
                " roba_prodaja.porez_potrosnja" +
                " FROM inventura_stavke" +
                " LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=inventura_stavke.sifra_robe AND roba_prodaja.id_skladiste='" + skladiste + "'" +
                " WHERE inventura.broj_inventure='" + broj_dokumenta + "'";

            sql_liste = sql_liste.Replace("+", "zbroj");
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM inventura WHERE broj_inventure='" + broj_dokumenta + "'", "inventura").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra ' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Cijena' AS tbl3," +
                " 'Kol.Kartice' AS tbl4," +
                " 'Iznos.Kar' AS tbl5," +
                " 'Kol.inv' AS tbl6," +
                " 'Iznos inv' AS tbl7," +
                " 'Razlika' AS tbl8," +
                " 'Iznos' AS tbl9," +
                " inventura.datum AS datum1," +
                " inventura.napomena AS komentar," +
                " 'Skladište: '+skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('INVENTURA  ' AS nvarchar) + CAST (inventura.broj_inventure AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM inventura " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=inventura.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=inventura.id_zaposlenik WHERE inventura.broj_inventure='" + broj_dokumenta + "'";
            //" WHERE meduskladisnica.broj ='" + broj_dokumenta + "'";

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