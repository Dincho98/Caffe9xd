using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace PCPOS.Report.predracuni
{
    public partial class frmPredracuniSvi : Form
    {
        public frmPredracuniSvi()
        {
            InitializeComponent();
        }

        public int idDjelatnik { get; set; }
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }

        private void frmNarudzbe_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

                string sql = string.Format(@"SELECT
concat(svi_predracuni.broj, '/', ducan.ime_ducana, '/', blagajna.ime_blagajne) AS naziv2,
zaposlenici.ime || ' ' || zaposlenici.prezime as string1,
stolovi.naziv AS string2,
svi_predracuni.datum_ispisa AS string3,
svi_predracuni.sifra,
svi_predracuni.naziv,
round(svi_predracuni.kolicina, 2) as kolicina,
round(svi_predracuni.mpc, 2) AS cijena1,
round(svi_predracuni.kolicina * svi_predracuni.mpc, 2) AS cijena2
FROM svi_predracuni
LEFT JOIN stolovi ON stolovi.id_stol = CAST(svi_predracuni.id_stol AS INT)
LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = CAST(svi_predracuni.id_zaposlenik AS INT)
LEFT JOIN blagajna ON blagajna.id_blagajna = CAST(svi_predracuni.id_blagajna AS INT)
LEFT JOIN ducan ON ducan.id_ducan = CAST(svi_predracuni.id_ducan AS INT)
WHERE  CAST(svi_predracuni.datum_ispisa AS date) BETWEEN '{0:yyyy-MM-dd}' AND '{1:yyyy-MM-dd}'
and case when {2} = 0 then 1 = 1 else CAST(svi_predracuni.id_zaposlenik AS INT) = {2} end
group by svi_predracuni.id, svi_predracuni.broj, ducan.ime_ducana, blagajna.ime_blagajne, zaposlenici.ime, zaposlenici.prezime, stolovi.naziv, svi_predracuni.datum_ispisa, svi_predracuni.sifra, svi_predracuni.naziv, svi_predracuni.kolicina, svi_predracuni.mpc
ORDER BY CAST(svi_predracuni.id AS INT) ASC", datumOd, datumDo, idDjelatnik);

                classSQL.NpgAdatpter(sql).Fill(dSRliste, "DTliste");

                sql = string.Format(@"SELECT
podaci_tvrtka.ime_tvrtke,
podaci_tvrtka.skraceno_ime,
podaci_tvrtka.oib,
podaci_tvrtka.tel,
podaci_tvrtka.fax,
podaci_tvrtka.mob,
podaci_tvrtka.iban,
podaci_tvrtka.adresa,
podaci_tvrtka.vl,
podaci_tvrtka.poslovnica_adresa,
podaci_tvrtka.nazivPoslovnice as ime_poslovnice,
g2.posta + ' ' + g2.grad AS poslovnica_grad,
podaci_tvrtka.email,
podaci_tvrtka.naziv_fakture,
podaci_tvrtka.text_bottom,
g1.posta + ' ' + g1.grad AS grad
FROM podaci_tvrtka
LEFT JOIN grad g1 ON g1.id_grad = podaci_tvrtka.id_grad
left join grad g2 on g2.id_grad = podaci_tvrtka.poslovnica_grad");

                classSQL.CeAdatpter(sql).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

                this.reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}