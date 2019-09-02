using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Kasa
{
    public partial class frmPrometKase : Form
    {
        public frmPrometKase()
        {
            InitializeComponent();
        }

        public string od_datuma { get; set; }
        public string do_datuma { get; set; }

        private void test_Load(object sender, EventArgs e)
        {
            od_datuma = Convert.ToDateTime("10,10,2011").ToString("yyyy-MM-dd H:mm:ss");
            do_datuma = Convert.ToDateTime("10,10,2012").ToString("yyyy-MM-dd H:mm:ss");

            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            SetDS();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void SetDS()
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

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            string sql2 = "SELECT " +
                "racuni.broj_racuna AS br_racuna," +
                "racuni.id_kasa AS blagajna," +
                "racuni.datum_racuna AS datum," +
                "racuni.ukupno AS ukupno," +
                "racuni.ukupno_gotovina AS gotovina," +
                "racuni.ukupno_kartice AS kartice," +
                "zaposlenici.ime + ' ' + zaposlenici.prezime AS blagajnik," +
                "'PROMET KASE PO RAČUNIMA' AS naslov " +
                " FROM racuni LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=racuni.id_blagajnik WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSKasaPromet, "DTkasaPromet");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSKasaPromet, "DTkasaPromet");
            }

            //    string sql3 = "SELECT "+
            //        " racun_stavke.mpc," +
            //        " racun_stavke.vpc," +
            //        " racun_stavke.rabat," +
            //        " racun_stavke.kolicina," +
            //        " racun_stavke.porez," +
            //        " racuni.gotovina," +
            //        " racuni.kartice" +
            //        " FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna "+
            //        " WHERE  racuni.datum_racuna >= '" + od_datuma + "' AND racuni.datum_racuna <='" + do_datuma + "'";

            //   DataTable DTrc = classSQL.select(sql3, "racun_stavke").Tables[0];

            //   double kolicina_stavka = 0;
            //   double rabat_stavka = 0;
            //   double vpc_stavka = 0;
            //   double porez_stavka = 0;
            //   double mpc_stavka = 0;

            //   double sve_ukupno=0;
            //   double rabat = 0;
            //   double osnovica = 0;
            //   double pdv = 0;

            //   double sve_ukupno1 = 0;
            //   double rabat1 = 0;
            //   double osnovica1 = 0;
            //   double pdv1 = 0;

            //   for (int i = 0; i < DTrc.Rows.Count; i++)
            //   {
            //       kolicina_stavka = Convert.ToDouble(DTrc.Rows[i]["kolicina"].ToString());
            //       rabat_stavka = Convert.ToDouble(DTrc.Rows[i]["rabat"].ToString());
            //       vpc_stavka = Convert.ToDouble(DTrc.Rows[i]["vpc"].ToString());
            //       porez_stavka = Convert.ToDouble(DTrc.Rows[i]["porez"].ToString());
            //       mpc_stavka = ((vpc_stavka * porez_stavka / 100) + vpc_stavka)*kolicina_stavka;

            //           //izracun gotovina
            //           sve_ukupno = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) + sve_ukupno;
            //           rabat = (mpc_stavka * rabat_stavka / 100) + rabat;
            //           osnovica = ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / Convert.ToDouble("1," + porez_stavka)) + osnovica;
            //           pdv = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) - ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / Convert.ToDouble("1," + porez_stavka));

            //           //izracun kartica
            //           sve_ukupno1 = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) + sve_ukupno;
            //           rabat1 = (mpc_stavka * rabat_stavka / 100) + rabat;
            //           osnovica1 = ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / Convert.ToDouble("1," + porez_stavka)) + osnovica;
            //           pdv1 = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) - ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / Convert.ToDouble("1," + porez_stavka));

            //   }
        }
    }
}