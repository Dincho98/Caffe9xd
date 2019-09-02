using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Liste2
{
    public partial class frmListe : Form
    {
        private bool spremiPdf;

        public frmListe(bool spremiPdf=false)
        {
            InitializeComponent();
            this.spremiPdf = spremiPdf;
        }

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            this.Text = ImeForme;

            if (dokumenat == "PROMET")
            {
                PrometKase();
                this.Text = ImeForme;
            }
            this.reportViewer1.RefreshReport();

            if (spremiPdf)
            {
                Global.GlobalFunctions.SpremiPdf("PrometKase", reportViewer1);
                this.Close();
            }
        }

        private void PrometKase()
        {
            string sql1 = string.Format(@"SELECT
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
podaci_tvrtka.poslovnica_grad,
podaci_tvrtka.email,
podaci_tvrtka.naziv_fakture,
podaci_tvrtka.text_bottom,
grad.grad + ' ' + grad.posta AS grad
FROM podaci_tvrtka
LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad;");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string WHERE = "WHERE ", _ducan = "SVE", _zaposlenik = "SVE", _skladiste = "SVE";

            if (blagajnik != null)
            {
                WHERE += "racuni.id_blagajnik='" + blagajnik + "' AND ";
                try
                {
                    _zaposlenik = classSQL.select("SELECT ime + ' ' + prezime  FROM zaposlenici WHERE id_zaposlenik='" + blagajnik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            if (ducan != null)
            {
                WHERE += "racuni.id_ducan='" + ducan + "' AND ";
                try
                {
                    _ducan = classSQL.select("SELECT ime_ducana  FROM ducan WHERE id_ducan='" + ducan + "'", "ducan").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            if (skladiste != null)
            {
                WHERE += "racun_stavke.id_skladiste='" + skladiste + "' AND ";
                try
                {
                    _skladiste = classSQL.select("SELECT skladiste FROM skladiste WHERE id_skladiste='" + skladiste + "'", "skladiste").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            WHERE += "cast(racuni.datum_racuna as date) >='" + datumOD.ToString("yyyy-MM-dd") + "' AND cast(racuni.datum_racuna as date) <='" + datumDO.ToString("yyyy-MM-dd") + "'";

            string sql_liste = string.Format(@"SELECT SUM(ukupno) AS [cijena1],SUM(ukupno_gotovina) AS [cijena2],SUM(ukupno_kartice) AS [cijena3]
FROM racuni
{0}
and
EXISTS(
    select 1 from racun_stavke where racun_stavke.broj_racuna = racuni.broj_racuna AND racun_stavke.id_ducan = racuni.id_ducan AND racun_stavke.id_blagajna = racuni.id_kasa
);",
WHERE);
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            //------------------------------------OVAJ DIO RACUNA RABAT-------------------------------------------------------------------
            DataSet DS = new DataSet();
            string sql_liste1 = string.Format(@"SELECT
racun_stavke.mpc,
racun_stavke.rabat,
racun_stavke.kolicina
FROM racun_stavke
LEFT JOIN racuni ON racun_stavke.broj_racuna = racuni.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_blagajna;");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste1).Fill(DS, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste1).Fill(DS, "DTliste");
            }

            decimal mpc = 0;
            decimal rabat = 0;
            decimal kolicina = 0;
            decimal rabat_iznos = 0;

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                mpc = Convert.ToDecimal(DS.Tables[0].Rows[i]["mpc"].ToString());
                rabat = Convert.ToDecimal(DS.Tables[0].Rows[i]["rabat"].ToString());
                kolicina = Convert.ToDecimal(DS.Tables[0].Rows[i]["kolicina"].ToString());
                rabat_iznos = (mpc * rabat / 100) + rabat_iznos;
            }

            //------------------------------------OVAJ DIO RACUNA RABAT----------------kraj--------------------------------------
            string sql_liste_string = string.Format(@"SELECT
'Način plačanja ' AS tbl1,
'Popust ' AS tbl2,
'Iznos ' AS tbl3,
'GOTOVINA ' AS string1,
'KARTICA ' AS string2,
'Dućan: {0}' AS string4,
'Skladište: {1}' AS string5,
'Blagajnik: {2}' AS string6,
'PROMET KASE PO NAČINIMA PLAĆANJA ' AS naslov,
'Rabat ukupno: {3}' AS string3,
'Za razdoblje: {4} - {5}' AS godina;",
                _ducan, _skladiste, _zaposlenik, rabat_iznos.ToString("#0.00"),
                datumOD.ToString("dd.MM.yyyy"), datumDO.ToString("dd.MM.yyyy"));

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}