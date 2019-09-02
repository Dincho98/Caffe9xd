using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPrometStanje : Form
    {
        public frmPrometStanje()
        {
            InitializeComponent();
        }

        public string poslovnica { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string ImeForme { get; set; }

        private void frmPrometStanje_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            Fil();
            this.reportViewer1.RefreshReport();
        }

        private void Fil()
        {
            string poslo = "";
            string skladi = "";
            if (poslovnica != null) { poslo = "Poslovnica=" + poslovnica; }
            if (skladiste != null) { skladi = "  Skladište=" + skladiste; }

            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " 'Od datuma: " + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "  -  " + datumDO.ToString("dd.MM.yyyy H:mm:ss") + "\r\n" + poslo + skladi + "' AS string1," +
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

            DSRlisteTekst DS = new DSRlisteTekst();

            string skl = "";
            if (skladiste != null) { skl = " WHERE roba_prodaja.skladiste ='" + skladiste + "'"; }

            string sql_roba = "SELECT " +
                " roba_prodaja.sifra,roba_prodaja.mjera,roba_prodaja.naziv,roba_prodaja.nc,roba_prodaja.kolicina,roba_prodaja.ulazni_porez " +
                " FROM roba_prodaja " +
                " " + skl;
            DataTable DTsvaRoba = classSQL.select(sql_roba, "roba_prodaja").Tables[0];

            string sql_racuni = "";
            string sql_primka = "";
            DataTable DTrac = new DataTable();
            DataTable DTkalk = new DataTable();
            DataTable DTprimka = new DataTable();
            sql_racuni = "SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina],racun_stavke.sifra_robe FROM racun_stavke " +
                    " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                    " WHERE racuni.datum_racuna >'" + datumOD.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " AND racuni.datum_racuna < '" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'  AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'" +
                    " GROUP BY racun_stavke.sifra_robe";
            DTrac = classSQL.select(sql_racuni, "racun_stavke").Tables[0];

            //Primka
            sql_primka = "SELECT SUM(CAST(primka_stavke.kolicina AS NUMERIC)) AS [kolicina],primka_stavke.sifra  FROM primka_stavke" +
           " LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke" +
           " WHERE primka.datum>'" + datumOD.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "'" +
           " AND primka.datum<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
           " GROUP BY primka_stavke.sifra";
            DTprimka = classSQL.select(sql_primka, "primka_stavke").Tables[0];

            string sql_normativi = "SELECT " +
                " caffe_normativ.sifra_normativ," +
                " caffe_normativ.sifra," +
                " caffe_normativ.kolicina as [kolicina_normativ]," +
                " caffe_normativ.id_skladiste," +
                " grupa.grupa," +
                " roba_prodaja.mjera,roba_prodaja.naziv,roba_prodaja.nc,roba_prodaja.kolicina,roba_prodaja.porez_potrosnja,roba_prodaja.ulazni_porez " +
                " FROM caffe_normativ" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=caffe_normativ.sifra_normativ" +
                " LEFT JOIN grupa ON grupa.id_grupa=roba_prodaja.id_grupa" + skl;
            DataTable DTnorativi = classSQL.select(sql_normativi, "caffe_normativ").Tables[0];

            decimal pdv = 0;
            decimal pnp = 0;
            decimal vpc = 0;
            decimal kol_skl = 0;

            foreach (DataRow row in DTsvaRoba.Rows)
            {
                DataRow[] dataROW_Nor = DTnorativi.Select("sifra_normativ = '" + row["sifra"].ToString() + "'");

                if (dataROW_Nor.Count() > 0)
                {
                    decimal kol_racun = 0;
                    decimal kol_primaka = 0;
                    decimal kolicina_normativ = 0;
                    decimal KolUkupno = 0;

                    foreach (DataRow rowNormativ in dataROW_Nor)
                    {
                        kolicina_normativ = decimal.Parse(rowNormativ["kolicina_normativ"].ToString());

                        DataRow[] dataROW_R = DTrac.Select("sifra_robe = '" + rowNormativ["sifra"].ToString() + "'");
                        if (dataROW_R.Count() > 0)
                        {
                            kol_racun = Convert.ToDecimal(dataROW_R[0]["kolicina"].ToString());
                        }
                        else
                        {
                            kol_racun = 0;
                        }

                        KolUkupno = KolUkupno + (kol_racun * kolicina_normativ);
                    }

                    DataRow[] dataROW_P = DTprimka.Select("sifra = '" + dataROW_Nor[0]["sifra_normativ"].ToString() + "'");
                    if (dataROW_P.Count() > 0)
                    {
                        kol_primaka = Convert.ToDecimal(dataROW_P[0]["kolicina"].ToString());
                    }

                    kol_skl = decimal.Parse(dataROW_Nor[0]["kolicina"].ToString());
                    pdv = decimal.Parse(dataROW_Nor[0]["ulazni_porez"].ToString());
                    pnp = decimal.Parse(dataROW_Nor[0]["porez_potrosnja"].ToString());
                    vpc = decimal.Parse(dataROW_Nor[0]["nc"].ToString());

                    DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();
                    DTrow["string1"] = dataROW_Nor[0]["grupa"].ToString();
                    DTrow["string2"] = dataROW_Nor[0]["naziv"].ToString();
                    DTrow["string3"] = dataROW_Nor[0]["mjera"].ToString();
                    DTrow["ukupno1"] = ((kol_skl + (KolUkupno)) - (kol_primaka)).ToString("#0.000");
                    DTrow["ukupno2"] = kol_primaka.ToString("#0.000");
                    DTrow["ukupno3"] = kol_skl.ToString("#0.000");
                    DTrow["ukupno4"] = (KolUkupno).ToString("#0.000");
                    DTrow["ukupno5"] = (vpc + ((vpc * (pdv + pnp)) / 100)).ToString("#0.000");
                    DTrow["ukupno6"] = ((KolUkupno) * (vpc + ((vpc * (pdv + pnp)) / 100))).ToString("#0.000");
                    dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                }
            }
        }
    }
}