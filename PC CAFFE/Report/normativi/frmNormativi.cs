using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.normativi
{
    public partial class frmNormativi : Form
    {
        public frmNormativi()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string id_podgrupa { get; set; }

        public string grupa { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            Normativ();
            this.reportViewer1.RefreshReport();
        }

        private void Normativ()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
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

            DataTable DT = classSQL.select("SELECT * FROM roba ORDER BY naziv ASC ", "roba").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                string sql = "SELECT roba_prodaja.sifra," +
                    "roba_prodaja.naziv," +
                    "caffe_normativ.kolicina FROM caffe_normativ " +
                    "LEFT JOIN roba_prodaja ON caffe_normativ.sifra_normativ=roba_prodaja.sifra " +
                    "WHERE caffe_normativ.sifra='" + row["sifra"].ToString() + "'" +
                    "ORDER BY caffe_normativ.id_stavka ASC";

                DataTable DTn = classSQL.select(sql, "roba").Tables[0];

                DataRow DTrow;
                int bb = 1;
                foreach (DataRow rowN in DTn.Rows)
                {
                    DTrow = dSRliste.Tables["DTliste"].NewRow();

                    //ovaj dio je iz tablice roba
                    DTrow["cijena3"] = bb;
                    DTrow["naziv"] = row["naziv"].ToString();
                    DTrow["sifra"] = row["sifra"].ToString();
                    DTrow["cijena1"] = row["mpc"].ToString();

                    //ovaj dio je iz tablice roba_prodaja i caffe_normativ
                    DTrow["naziv2"] = rowN["naziv"].ToString();
                    DTrow["jmj"] = rowN["sifra"].ToString();
                    DTrow["cijena2"] = rowN["kolicina"].ToString();
                    dSRliste.Tables["DTliste"].Rows.Add(DTrow);
                    bb++;
                }

                DTrow = dSRliste.Tables["DTliste"].NewRow();
                dSRliste.Tables["DTliste"].Rows.Add(DTrow);
            }
        }

        private void Artikli(string artikl, decimal kolicina, string sifra, decimal mpc, string jmj, decimal pnp, decimal pdv_iznos)
        {
            DataRow[] dataROW = dSRliste.Tables["DTliste"].Select("sifra = '" + sifra + "' AND cijena7='" + mpc + "'");

            if (dataROW.Count() == 0)
            {
                DataRow RowArtikl = dSRliste.Tables[0].NewRow();
                RowArtikl["sifra"] = sifra;
                RowArtikl["naziv"] = artikl;
                RowArtikl["jmj"] = jmj;
                RowArtikl["cijena1"] = pnp;
                RowArtikl["cijena3"] = pdv_iznos;
                RowArtikl["cijena4"] = kolicina.ToString();
                RowArtikl["cijena5"] = (pnp + pdv_iznos).ToString("#0.000");
                RowArtikl["cijena6"] = mpc * kolicina;
                RowArtikl["cijena7"] = mpc;
                dSRliste.Tables["DTliste"].Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["cijena4"] = Convert.ToDecimal(dataROW[0]["cijena4"].ToString()) + kolicina;
                dataROW[0]["cijena1"] = Convert.ToDecimal(dataROW[0]["cijena1"].ToString()) + pnp;
                dataROW[0]["cijena3"] = Convert.ToDecimal(dataROW[0]["cijena3"].ToString()) + pdv_iznos;
                dataROW[0]["cijena5"] = (Convert.ToDecimal(dataROW[0]["cijena5"].ToString()) + pnp + pdv_iznos).ToString("#0.000");
                dataROW[0]["cijena6"] = (Convert.ToDecimal(dataROW[0]["cijena6"].ToString()) + mpc * kolicina).ToString("#0.000");
            }
        }

        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal pdv_stavka)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = pdv.ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
            }
        }

        private DataTable DTpdvN = new DataTable();

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = osnovica;
                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }
    }
}