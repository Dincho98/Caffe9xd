using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace PCPOS.Report.Narudzbe
{
    public partial class frmNarudzbe : Form
    {
        public frmNarudzbe()
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

                string sql = string.Format(@"select cn.broj_narudzbe as string1, to_char(cn.datum, 'DD.MM.YYYY. HH24:MI:SS') as string2, concat(z.ime, ' ', z.prezime) as string3,
cn.sifra_stavke as sifra, r.naziv as naziv, cn.kolicina as kolicina
from caffe_narudzbe cn
left join zaposlenici z on cn.djelatnik = z.id_zaposlenik
left join roba r on cn.sifra_stavke = r.sifra
where cast(cn.datum as date) between '{0:yyyy-MM-dd}' and '{1:yyyy-MM-dd}'
and case when {2} = 0 then 1 = 1 else cn.djelatnik = {2} end
order by cn.broj_narudzbe asc", datumOd, datumDo, idDjelatnik);

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