using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Kalkulacija
{
    public partial class frmKalkulacija : Form
    {
        public frmKalkulacija()
        {
            InitializeComponent();
        }

        public string broj_kalkulacije { get; set; }

        private void frmKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            SetDS();
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

            string sql_stavke = "SELECT " +
                " kalkulacija_stavke.sifra," +
                " roba.naziv," +
                " roba.jm AS jmj," +
                " kalkulacija_stavke.kolicina," +
                " kalkulacija_stavke.rabat," +
                " kalkulacija_stavke.fak_cijena," +
                " kalkulacija_stavke.prijevoz," +
                " kalkulacija_stavke.carina," +
                " kalkulacija_stavke.posebni_porez," +
                " kalkulacija_stavke.marza_postotak as marza," +
                " kalkulacija_stavke.vpc," +
                " kalkulacija_stavke.porez AS pdv" +
                " FROM kalkulacija_stavke" +
                " LEFT JOIN roba ON kalkulacija_stavke.sifra=roba.sifra WHERE kalkulacija_stavke.broj='" + broj_kalkulacije + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }

            DataTable DT = dSkalkulacija_stavke.Tables[0];
            double fak_iznos = 0;
            double fak_iznos_stavka = 0;
            double fak_netto = 0;
            double osnovica = 0;
            double fak_pdv = 0;
            double fak_ukupno_stavka = 0;
            double fak_ukupno = 0;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                fak_iznos_stavka = (Convert.ToDouble(DT.Rows[i]["fak_cijena"].ToString()) * Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()));
                fak_iznos = fak_iznos_stavka + fak_iznos;

                fak_netto = (fak_iznos_stavka - (fak_iznos_stavka * Convert.ToDouble(DT.Rows[i]["rabat"].ToString()) / 100)) + fak_netto;

                fak_ukupno_stavka = (Convert.ToDouble(DT.Rows[i]["vpc"].ToString()) * Convert.ToDouble(DT.Rows[i]["pdv"].ToString()) / 100) + Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                fak_ukupno_stavka = fak_ukupno_stavka * Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
                fak_ukupno = fak_ukupno_stavka + fak_ukupno;

                osnovica = fak_ukupno_stavka / Convert.ToDouble("1," + DT.Rows[i]["pdv"].ToString()) + osnovica;
                fak_pdv = fak_ukupno_stavka - (fak_ukupno_stavka / Convert.ToDouble("1," + DT.Rows[i]["pdv"].ToString())) + fak_pdv;
            }

            string sql_kalk = "SELECT " +
            " kalkulacija.broj," +
            " kalkulacija.id_partner," +
            " kalkulacija.godina," +
            " 'Kalkulacija' AS naslov," +
            " skladiste.skladiste," +
            " kalkulacija.datum," +
            " partners.ime_tvrtke AS dobavljac," +
            " kalkulacija.tecaj AS tecaj," +
            " kalkulacija.racun," +
            " kalkulacija.tecaj," +
            " zaposlenici.ime + ' ' + zaposlenici.prezime AS kalkulirao," +
            " kalkulacija.racun_datum," +
            " valute.ime_valute AS valuta," +
            " '" + fak_iznos + "' AS fak_iznos," +
            " '" + fak_netto + "' AS netto_fak_iznos," +
            " '" + fak_pdv + "' AS pdv," +
            " '" + osnovica + "' AS osnovica," +
            " '" + fak_ukupno + "' AS ukupno" +
            " FROM kalkulacija" +
            " LEFT JOIN skladiste ON kalkulacija.id_skladiste=skladiste.id_skladiste" +
            " LEFT JOIN zaposlenici ON kalkulacija.id_zaposlenik=zaposlenici.id_zaposlenik" +
            " LEFT JOIN valute ON kalkulacija.id_valuta=valute.id_valuta" +
            " LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner WHERE kalkulacija.broj='" + broj_kalkulacije + "'" +
            "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
            else
            {
                classSQL.NpgAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
        }
    }
}