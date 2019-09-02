using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Uskladaskladista
{
    public partial class UskladaSkladistaReportForm : Form
    {
        public UskladaSkladistaReportForm()
        {
            InitializeComponent();
        }

        public int broj_dokumenta { get; set; }
        public int skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void UskladaSkladistaReportForm_Load(object sender, EventArgs e)
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

            string sql_liste = "select usklada_robe_stavke.roba_id as sifra,replace(usklada_robe_stavke.nova_kolicina, ',', '.') as cijena4,roba_prodaja.naziv,usklada_robe_stavke.stara_kolicina as cijena2," +
                                "roba_prodaja.nc as cijena1,roba_prodaja.ulazni_porez, (roba_prodaja.nc) * cast(replace(usklada_robe_stavke.stara_kolicina, ',', '.') as numeric) as cijena3," +
                                "(roba_prodaja.nc) * cast(replace(usklada_robe_stavke.nova_kolicina, ',', '.') as numeric) as cijena5,cast(replace(usklada_robe_stavke.nova_kolicina, ',', '.') as numeric) - cast(replace(usklada_robe_stavke.stara_kolicina, ',', '.') as numeric) as cijena6," +
                                "(cast(replace(usklada_robe_stavke.nova_kolicina, ',', '.') as numeric) - cast(replace(usklada_robe_stavke.stara_kolicina, ',', '.') as numeric)) * (roba_prodaja.nc + cast(replace(roba_prodaja.povratna_naknada, ',', '.') as numeric)) as cijena7 " +
                                "from usklada_robe_stavke left join usklada_robe on usklada_robe_stavke.usklada_id = usklada_robe.id_usklade left join roba_prodaja on cast(roba_prodaja.sifra as integer) = usklada_robe_stavke.roba_id where " +
                                "usklada_robe.id_usklade = " + broj_dokumenta;

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
            DataTable DT = classSQL.select("SELECT datum FROM usklada_robe WHERE id_usklade='" + broj_dokumenta + "'", "inventura").Tables[0];

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
                " 'Trenutno stanje' AS tbl6," +
                " 'Iznos usklade' AS tbl7," +
                " 'Razlika' AS tbl8," +
                " 'Iznos' AS tbl9," +
                " usklada_robe.datum AS datum1," +
                " usklada_robe.napomena AS komentar," +
                
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('USKLADA SKLADIŠTA  ' AS nvarchar) + CAST (usklada_robe.id_usklade AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM usklada_robe " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=usklada_robe.izradio WHERE usklada_robe.id_usklade='" + broj_dokumenta + "'";
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