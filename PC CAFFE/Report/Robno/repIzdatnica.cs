using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Robno
{
    public partial class repIzdatnica : Form
    {
        public repIzdatnica()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string broj_skladista { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void repIzdatnica_Load(object sender, EventArgs e)
        {
            if (broj_dokumenta != null)
            {
                LoadDocument();
            }

            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
        {
            string grad_string = "";
            DataTable grad = classSQL.select("SELECT grad,posta FROM grad, izdatnica" +
                " LEFT JOIN partners ON partners.id_partner=izdatnica.id_partner" +
                " WHERE izdatnica.broj='" + broj_dokumenta + "' AND izdatnica.id_skladiste = '" + broj_skladista + "'" +
                " And partners.id_grad=grad.id_grad", "grad").Tables[0];
            if (grad.Rows.Count != 0)
            {
                grad_string = grad.Rows[0]["posta"].ToString().Trim() + " " + grad.Rows[0]["grad"].ToString();
            }

            string sql1 = "SELECT " +
                " p.broj AS broj," +
                " p.originalni_dokument," +
                " p.datum," +
                " p.napomena AS napomena," +
                " p.godina," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " '" + grad_string + "' AS kupac_grad," +
                " partners.id_grad AS kupac_grad_id," +
                " partners.id_partner AS sifra_kupac," +
                " partners.oib AS kupac_oib," +
                " '' AS Naslov," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " grad.grad as mjesto," +
                " skladiste.id_skladiste," +
                " skladiste.skladiste" +
                " FROM izdatnica p" +
                " LEFT JOIN partners ON partners.id_partner=p.id_partner" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=p.id_izradio" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=p.id_skladiste" +
                " LEFT JOIN grad ON grad.id_grad=p.id_mjesto" +
                " WHERE p.broj='" + broj_dokumenta + "' AND p.id_skladiste = '" + broj_skladista + "'";

            //izdatnica broj + skladište ili id_izdatnica???

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSIzdatnica, "DTRIzdatnica");
            }
            else
            {
                classSQL.NpgAdatpter(sql1.Replace("nvarchar", "varchar")).Fill(dSIzdatnica, "DTRIzdatnica");
            }

            string sql_liste = string.Format(@"SELECT
p.sifra,
p.vpc,
p.mpc,
p.rabat,
p.broj,
ROUND(replace(p.kolicina, ',','.')::numeric, 3) AS kolicina,
p.nbc,
p.pdv,
p.ukupno,
roba_prodaja.naziv AS naziv_robe,
roba_prodaja.mjera as jm
FROM izdatnica_stavke p
LEFT JOIN izdatnica ON izdatnica.id_izdatnica=p.id_izdatnica
left join roba_prodaja on p.sifra = roba_prodaja.sifra and izdatnica.id_skladiste = roba_prodaja.id_skladiste
WHERE izdatnica.broj = '{0}' AND izdatnica.id_skladiste = '{1}';",
    broj_dokumenta,
    broj_skladista);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSIzdatnicaStavke, "DTIzdatnicaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSIzdatnicaStavke, "DTIzdatnicaStavke");
            }

            DataTable dt = dSIzdatnicaStavke.Tables[0];

            decimal iznosBezPoreza, sumIznosBezPoreza, uk, sumUk, pdv;
            string rabat;
            sumIznosBezPoreza = 0;
            sumUk = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uk = Math.Round(Convert.ToDecimal(dt.Rows[i]["ukupno"].ToString()), 2);
                pdv = Math.Round(Convert.ToDecimal(dt.Rows[i]["pdv"].ToString().Replace(".", ",")), 2);

                iznosBezPoreza = uk / (1 + pdv / 100);

                rabat = Convert.ToDouble(dt.Rows[i]["rabat"].ToString()) == 0 ? "" : dt.Rows[i]["rabat"].ToString();

                dt.Rows[i].SetField("iznosBezPoreza", Math.Round(iznosBezPoreza, 2));
                dt.Rows[i].SetField("rabat", rabat);

                sumIznosBezPoreza += iznosBezPoreza;
                sumUk += uk;
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSIzdatnica.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSIzdatnica.Tables[0].Rows[0]["kupac_grad_id"].ToString();
                id_kupac = dSIzdatnica.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //string id_kupac = "";
            //if (dSFaktura.Tables[0].Rows.Count > 0)
            //{
            //    id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Odabrani zahtjv ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string grad_kupac = "";
            if (id_kupac == "")
            {
                id_kupac = "0";
            }
            DataTable DTgrad_kupac = classSQL.select("SELECT grad,posta,id_drzava FROM grad WHERE id_grad='" + id_kupac + "'", "grad").Tables[0];

            if (DTgrad_kupac.Rows.Count != 0)
            {
                DataTable DTzemlja_kupac = classSQL.select("SELECT zemlja FROM zemlja WHERE id_zemlja='" + DTgrad_kupac.Rows[0]["id_drzava"].ToString() + "'", "zemlja").Tables[0];
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString() + "\r\n" + DTzemlja_kupac.Rows[0]["zemlja"].ToString();
            }

            sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.zr," +
                " '" + grad_kupac + "' AS grad_kupac," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naslov_racuna AS naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
        }
    }
}