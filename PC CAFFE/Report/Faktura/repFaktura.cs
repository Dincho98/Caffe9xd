using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace PCPOS.Report.Faktura
{
    public partial class repFaktura : Form
    {
        public repFaktura()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string id_kasa { get; set; }
        public string id_poslovnica { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }

        private double osnovica_ukupno = 0;
        private double SveUkupno = 0;
        private double pdv_ukupno = 0;
        private double ukupno_rabat = 0;

        private void repFaktura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;
            //dokumenat = "OTP";
            //broj_dokumenta = "1";
            //from_skladiste = "3";

            if (dokumenat == "FAK")
            {
                if (broj_dokumenta == null) { return; }
                FillFaktura(broj_dokumenta);
            }
            else if (dokumenat == "RAC")
            {
                if (broj_dokumenta == null) { return; }
                FillRacun(broj_dokumenta);
            }
            else if (dokumenat == "PON")
            {
                if (broj_dokumenta == null) { return; }
                FillPonude(broj_dokumenta);
            }
            else if (dokumenat == "OTP")
            {
                if (broj_dokumenta == null) { return; }
                FillOtpremnicu(broj_dokumenta, from_skladiste);
            }
            this.reportViewer1.RefreshReport();
        }

        private void FillRacun(string broj)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " racun_stavke.kolicina," +
                " racun_stavke.vpc," +
                " racun_stavke.mpc," +
                " CAST(racun_stavke.mpc AS NUMERIC) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)   -      (CAST(racun_stavke.mpc AS NUMERIC) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)*CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) AS iznos," +
                " racun_stavke.porez," +
                " racun_stavke.porez_potrosnja," +
                " racun_stavke.broj_racuna," +
                " racun_stavke.rabat," +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv as naziv," +
                " racun_stavke.id_skladiste AS skladiste" +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe " +
                " WHERE racun_stavke.broj_racuna='" + broj + "' AND racun_stavke.id_ducan='" + (id_poslovnica == null ? Util.Korisno.idDucan : id_poslovnica) + "' AND racun_stavke.id_blagajna='" + (id_kasa == null ? Util.Korisno.idKasa : id_kasa) + "'";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            string iznososlobpdv = "";
            string iznos_marza = "";
            /////kraj priprema za fiskalizaciju

            double osnovica = 0;
            double pdv_stavka = 0;
            double Porez_potrosnja_stavka = 0;
            double ukupno = 0;

            double osnovica_sve = 0;
            double Porez_potrosnja_sve = 0;
            double pdv_sve = 0;
            double rabat_sve = 0;
            double rabat = 0;
            ukupno_rabat = 0;

            for (int i = 0; i < dSRfakturaStavke.Tables[0].Rows.Count; i++)
            {
                if (DTpdv.Columns["stopa"] == null)
                {
                    DTpdv.Columns.Add("stopa");
                    DTpdv.Columns.Add("iznos");
                }

                double kolicina = Convert.ToDouble(dSRfakturaStavke.Tables[0].Rows[i]["kolicina"].ToString());
                double PP = Convert.ToDouble(dSRfakturaStavke.Tables[0].Rows[i]["porez_potrosnja"].ToString());
                double PDV = Convert.ToDouble(dSRfakturaStavke.Tables[0].Rows[i]["porez"].ToString());
                double VPC = Convert.ToDouble(dSRfakturaStavke.Tables[0].Rows[i]["vpc"].ToString());

                double MPC = 0;
                double.TryParse(dSRfakturaStavke.Tables[0].Rows[i]["mpc"].ToString(), out MPC);

                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                double mpc = MPC * kolicina;

                rabat = Convert.ToDouble(dSRfakturaStavke.Tables[0].Rows[i]["rabat"].ToString());
                ukupno_rabat = (mpc * rabat / 100) + ukupno_rabat;
                mpc = mpc - (mpc * rabat / 100);

                //Ovaj kod dobiva PDV
                double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                //Ovaj kod dobiva porez na potrošnju
                double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                //izračun porez potrosnja
                Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                //izračun osnovica
                if (rabat > 0)
                {
                    double pps = (100 * (PDV + PP)) / (100 + PDV + PP);
                    osnovica = mpc - (mpc * pps / 100);
                }
                else
                {
                    osnovica = VPC * kolicina;
                }
                osnovica_sve = (osnovica) + osnovica_sve;

                //Izracun pdv
                pdv_sve = pdv_sve + (pdv_stavka);

                StopePDVa(Convert.ToDecimal(PDV), (Convert.ToDecimal(mpc) * Convert.ToDecimal(PreracunataStopaPDV)) / 100);
                StopePDVa(PDV, Math.Round(pdv_stavka, 4), Math.Round(((VPC * kolicina) - (mpc * rabat / 100)), 4));

                //ukupno sve
                ukupno += mpc;
            }

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(SveUkupno.ToString(), ',', "kn", "lp").ToString();

            string year = classSQL.select("SELECT datum_racuna FROM racuni WHERE broj_racuna='" + broj_dokumenta + "' AND racuni.id_ducan='" + (id_poslovnica == null ? Util.Korisno.idDucan : id_poslovnica) + "' AND racuni.id_kasa='" + (id_kasa == null ? Util.Korisno.idKasa : id_kasa) + "'", "racuni").Tables[0].Rows[0][0].ToString();
            DateTime date = Convert.ToDateTime(year);
            year = date.Year.ToString();

            string pdv_ispis = "";
            for (int p = 0; p < DTpdv.Rows.Count; p++)
            {
                if (pdv_ispis.Length > 0)
                {
                    pdv_ispis += " kn\r\n";
                }
                pdv_ispis += "" + DTpdv.Rows[p]["stopa"].ToString() + "%:   " + Convert.ToDecimal(DTpdv.Rows[p]["iznos"].ToString()).ToString("#0.00");
            }

            DataTable DTrac = classSQL.select("SELECT * FROM racuni WHERE racuni.broj_racuna='" + broj_dokumenta + "' AND racuni.id_ducan='" + (id_poslovnica == null ? Util.Korisno.idDucan : id_poslovnica) + "' AND racuni.id_kasa='" + (id_kasa == null ? Util.Korisno.idKasa : id_kasa) + "'", "racuni").Tables[0];
            string popust_sve_napomena = "";
            decimal popust_cijelog_racuna = 0;
            decimal.TryParse(DTrac.Rows[0]["popust_cijeli_racun"].ToString(), out popust_cijelog_racuna);
            if (popust_cijelog_racuna > 0)
            {
                decimal popust_iznos = 0;
                popust_iznos = ((decimal)ukupno * (1 + (((100 * popust_cijelog_racuna) / (100 - popust_cijelog_racuna)) / 100))) - (decimal)ukupno;
                popust_sve_napomena = "\r\nUKUPNO POPUSTA NA RAČUN: " + popust_cijelog_racuna + "%\r\n";
                popust_sve_napomena += "POPUST U KN: " + Math.Round(popust_iznos, 3).ToString("#0.00") + " kn\r\n";
            }

            string sql = "SELECT " +
                " racuni.broj_racuna," +
                " 'JIR: ' + racuni.jir AS string1," +
                " 'ZIK: ' + racuni.zik AS string2," +
                " racuni.datum_racuna AS datum, '1970-01-01' as datum_dvo, '1970-01-01' as datum_valute, " +
                " '" + ukupno + "' AS ukupno," +
                " '" + pdv_ispis + "' AS iznos_pdv," +
                " '" + osnovica_sve + "' AS osnovica," +
                " '" + ukupno_rabat + "' AS rabat," +
                " '" + Porez_potrosnja_sve.ToString("#0.00") + "' AS string3," +
                " CAST (racuni.broj_racuna AS nvarchar) + '/' + CAST (ducan.ime_ducana AS nvarchar) + '/' + CAST (blagajna.ime_blagajne AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " ' Novčanice: ' + CAST(racuni.ukupno_gotovina AS money) + '   Kartice: ' + CAST(racuni.ukupno_kartice AS money) + '   Virman: ' + CAST(racuni.ukupno_virman AS money)  AS placanje," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " '" + popust_sve_napomena + "' AS napomena," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM racuni" +
                " LEFT JOIN podaci_tvrtka ON podaci_tvrtka.id='1'" +
                " LEFT JOIN ducan ON ducan.id_ducan = racuni.id_ducan" +
                " LEFT JOIN blagajna ON blagajna.id_ducan = racuni.id_ducan and blagajna.id_blagajna = racuni.id_kasa" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN partners ON partners.id_partner=racuni.id_kupac" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=racuni.id_blagajnik WHERE racuni.broj_racuna='" + broj_dokumenta + "'  AND racuni.id_ducan='" + (id_poslovnica == null ? Util.Korisno.idDucan : id_poslovnica) + "' AND racuni.id_kasa='" + (id_kasa == null ? Util.Korisno.idKasa : id_kasa) + "';" +
                "";


            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }

            if (id_kupac.Length == 0)
                id_kupac = "0";

            string grad_kupac = "";
            DataTable DTgrad_kupac = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + id_kupac + "'", "grad").Tables[0];
            if (DTgrad_kupac.Rows.Count != 0)
            {
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                dSFaktura.Tables[0].Rows[0]["kupac_grad"] = grad_kupac;
            }

            string poslovnica_grad = "";
            DataTable DTposlovnica_grad = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + Class.PodaciTvrtka.gradPoslovnicaId + "'", "grad").Tables[0];
            if (DTposlovnica_grad.Rows.Count != 0)
            {
                poslovnica_grad = DTposlovnica_grad.Rows[0]["posta"].ToString().Trim() + " " + DTposlovnica_grad.Rows[0]["grad"].ToString();
            }

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
                " '" + grad_kupac + "' AS grad_kupac," +
                " podaci_tvrtka.poslovnica_adresa," +
                "  '" + poslovnica_grad + "' as poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naslov_racuna AS naziv_fakture," +
                " podaci_tvrtka.text_bottom, " +
                "podaci_tvrtka.nazivPoslovnice as ime_poslovnice,  podaci_tvrtka.swift, podaci_tvrtka.pdvBr as pdv_br," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";

            //DSRpodaciTvrtke DSpt = new DSRpodaciTvrtke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            this.reportViewer1.RefreshReport();
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

        private void FillFaktura(string broj)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " faktura_stavke.kolicina as kolicina," +
                " faktura_stavke.vpc," +
                " faktura_stavke.porez," +
                " faktura_stavke.broj_fakture," +
                " faktura_stavke.rabat," +
                " faktura_stavke.sifra," +
                " roba.naziv as naziv," +
                " faktura_stavke.id_skladiste AS skladiste," +
                " faktura_stavke.mpc," +
                " faktura_stavke.vpc * replace(faktura_stavke.kolicina, ',','.')::numeric as iznos," +
                " faktura_stavke.porez_potrosnja" +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra WHERE faktura_stavke.broj_fakture='" + broj + "' order by roba.naziv asc;";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            sql2 = @"SELECT sum(replace(faktura_stavke.kolicina, ',','.')::numeric) as kolicina, faktura_stavke.vpc, faktura_stavke.porez, faktura_stavke.broj_fakture, faktura_stavke.rabat, faktura_stavke.sifra, roba.naziv as naziv, faktura_stavke.id_skladiste AS skladiste,
                    faktura_stavke.mpc, ((faktura_stavke.vpc - (faktura_stavke.vpc * replace(rabat,',','.')::numeric / 100)) * sum(replace(faktura_stavke.kolicina, ',','.')::numeric)) as iznos, faktura_stavke.porez_potrosnja, '' as string1
                    FROM faktura_stavke
                    LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra
                    WHERE faktura_stavke.broj_fakture='" + broj + @"'
                    group by faktura_stavke.vpc, faktura_stavke.porez, faktura_stavke.broj_fakture, faktura_stavke.rabat, faktura_stavke.sifra, roba.naziv, faktura_stavke.id_skladiste, faktura_stavke.mpc, faktura_stavke.porez_potrosnja
                    order by roba.naziv asc;";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol_stavka = 0;
            double porez_potrosnja = 0, porez_potrosnja_stavka = 0;
            double pdv = 0;
            double rabat = 0;
            double mpc_stavka = 0;
            double rabat_stavka = 0;
            double pdv_stavka = 0;
            double osnovica_stavka = 0;
            double RabatSve = 0;
            double potrosnjaUkupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                mpc_stavka = Convert.ToDouble(DT.Rows[i]["mpc"].ToString());
                vpc = Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                kol_stavka = Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
                pdv = Convert.ToDouble(DT.Rows[i]["porez"].ToString());
                rabat = Convert.ToDouble(DT.Rows[i]["rabat"].ToString());
                porez_potrosnja = Convert.ToDouble(DT.Rows[i]["porez_potrosnja"].ToString());
                vpc = Math.Round((mpc_stavka / (1 + (pdv + porez_potrosnja) / 100)), 4, MidpointRounding.AwayFromZero);

                rabat_stavka = Math.Round((vpc * rabat / 100 * kol_stavka), 6, MidpointRounding.AwayFromZero);
                osnovica_stavka = Math.Round((vpc - (vpc * rabat / 100)) * kol_stavka, 6, MidpointRounding.AwayFromZero);
                pdv_stavka = Math.Round((vpc - (vpc * rabat / 100)) * pdv / 100 * kol_stavka, 6, MidpointRounding.AwayFromZero);
                porez_potrosnja_stavka = Math.Round((((vpc - (vpc * rabat / 100)) * (porez_potrosnja / 100)) * kol_stavka), 6, MidpointRounding.AwayFromZero);
                //mpc_stavka = Math.Round((vpc - (vpc * rabat / 100)) * (1 + (pdv + porez_potrosnja) / 100) * kol_stavka, 3, MidpointRounding.AwayFromZero);

                RabatSve += rabat_stavka;
                osnovica_ukupno += osnovica_stavka;
                pdv_ukupno += pdv_stavka;
                potrosnjaUkupno += porez_potrosnja_stavka;
                SveUkupno += ((mpc_stavka - (mpc_stavka * rabat / 100)) * kol_stavka);

                StopePDVa(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), pdv_stavka, osnovica_stavka);
            }

            //porez_potrosnja = SveUkupno - pdv_ukupno - osnovica_ukupno;
            //SveUkupno = Math.Round(osnovica_ukupno, 2, MidpointRounding.AwayFromZero) + Math.Round(pdv_ukupno, 2, MidpointRounding.AwayFromZero) + Math.Round(potrosnjaUkupno, 2, MidpointRounding.AwayFromZero);

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(SveUkupno.ToString(), ',', "kn", "lp").ToString();

            string sql = "SELECT " +
                " fakture.broj_fakture," +
                " fakture.date AS datum," +
                " fakture.dateDVO AS datum_dvo," +
                " fakture.datum_valute," +
                " fakture.mj_troska AS mjesto_troska," +
                " CASE WHEN nacin_placanja.naziv_placanja = 'Virman' THEN 'Transakcijski račun' ELSE nacin_placanja.naziv_placanja END AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (fakture.model AS nvarchar) + '  '+ CAST (fakture.broj_fakture AS nvarchar)+CAST (fakture.godina_fakture AS nvarchar)+'-'+CAST (fakture.id_fakturirati AS nvarchar) AS model," +
                " fakture.napomena," +
                " '" + RabatSve + "' AS rabat," +
                " '" + SveUkupno + "' AS ukupno," +
                " '" + Math.Round(pdv_ukupno, 2, MidpointRounding.AwayFromZero) + "' AS iznos_pdv," +
                " '" + osnovica_ukupno + "' AS osnovica," +
                " fakture.godina_fakture," +
                " CAST (fakture.broj_fakture AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(2) + "' AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Datum isporuke:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " partners.oib AS kupac_oib," +
                " '" + potrosnjaUkupno.ToString("#0.00") + "' as string3" +
                " FROM fakture" +
                " LEFT JOIN partners ON partners.id_partner=fakture.id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=fakture.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=fakture.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=fakture.zr" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=fakture.id_zaposlenik_izradio WHERE fakture.broj_fakture='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }

            if (id_kupac.Length == 0)
                id_kupac = "0";

            string grad_kupac = "";
            DataTable DTgrad_kupac = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad='" + id_kupac + "'", "grad").Tables[0];
            if (DTgrad_kupac.Rows.Count != 0)
            {
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                dSFaktura.Tables[0].Rows[0]["kupac_grad"] = grad_kupac;
            }

            string poslovnica_grad = "";
            DataTable DTposlovnica_grad = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + Class.PodaciTvrtka.gradPoslovnicaId + "'", "grad").Tables[0];
            if (DTposlovnica_grad.Rows.Count != 0)
            {
                poslovnica_grad = DTposlovnica_grad.Rows[0]["posta"].ToString().Trim() + " " + DTposlovnica_grad.Rows[0]["grad"].ToString();
            }


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
                 " '" + grad_kupac + "' AS grad_kupac," +
                 " podaci_tvrtka.poslovnica_adresa," +
                 "  '" + poslovnica_grad + "' as poslovnica_grad," +
                 " podaci_tvrtka.email," +
                 " podaci_tvrtka.naslov_racuna AS naziv_fakture," +
                 " podaci_tvrtka.text_bottom, " +
                 "podaci_tvrtka.nazivPoslovnice as ime_poslovnice,  podaci_tvrtka.swift, podaci_tvrtka.pdvBr as pdv_br," +
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
            }
            this.reportViewer1.RefreshReport();

            //----------------------------------------------------BarCode------------------------------------------
            string barCodeString = DodajRedak("HRVHUB30", 8) + "\n";
            barCodeString += DodajRedak("HRK", 3) + "\n";
            barCodeString += DodajRedak(BarCodeIznos(Double.Parse(dSFaktura.DTRfaktura.Rows[0]["ukupno"].ToString()).ToString("#0.00")), 15) + "\n";
            barCodeString += DodajRedak(dSFaktura.DTRfaktura.Rows[0]["kupac_tvrtka"].ToString(), 30) + "\n";
            barCodeString += DodajRedak(dSFaktura.DTRfaktura.Rows[0]["kupac_adresa"].ToString(), 27) + "\n";
            barCodeString += DodajRedak(dSFaktura.DTRfaktura.Rows[0]["kupac_grad"].ToString(), 27) + "\n";
            barCodeString += DodajRedak(dSRpodaciTvrtke.DTRpodaciTvrtke.Rows[0]["ime_tvrtke"].ToString(), 25) + "\n";
            barCodeString += DodajRedak(dSRpodaciTvrtke.DTRpodaciTvrtke.Rows[0]["adresa"].ToString(), 25) + "\n";
            barCodeString += DodajRedak(dSRpodaciTvrtke.DTRpodaciTvrtke.Rows[0]["grad"].ToString(), 27) + "\n";
            barCodeString += DodajRedak(dSRpodaciTvrtke.DTRpodaciTvrtke.Rows[0]["iban"].ToString(), 21) + "\n";
            barCodeString += "HR00" + "\n"; 
            barCodeString += DodajRedak(dSFaktura.DTRfaktura.Rows[0]["model"].ToString().Trim(), 22) + "\n"; 
            barCodeString += "PADD" + "\n"; 
            barCodeString += DodajRedak("Uplata", 35);

            //Napravi data table
            DataTable dt = new DataTable();
            dt.Columns.Add("image", typeof(byte[]));
            if (SviPodaciPostoje(barCodeString))
            {
                //Napravi bitmapu
                Bitmap bitmap = (Bitmap)(new ImageConverter()).ConvertFrom(GenerateBarCodeZXing(barCodeString));
                ImageConverter converter = new ImageConverter();
                byte[] slikaUBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                dt.Rows.Add(slikaUBytes);
            }

            //Napuni data set dataSetPDF417Code s data tableom dt
            dataSetPDF417Code.Tables.Add(dt); // Tables[1]
            dataSetPDF417Code.Tables[1].TableName = "tablica1";
            bindingSourceSlika.DataMember = "tablica1";


            //Opcenita shema za reportDataSource
            ReportDataSource dsPDF417Kod = new ReportDataSource();
            dsPDF417Kod.Name = "DataSetPdfCode";
            dsPDF417Kod.Value = dataSetPDF417Code.Tables[1];
            reportViewer1.LocalReport.DataSources.Add(dsPDF417Kod);

            reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Faktura.Report.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.RefreshReport();

        }

        //Metoda za generiranje PDF417 codea.
        private byte[] GenerateBarCodeZXing(string data)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.PDF_417,
                Options = new EncodingOptions { Width = 200, Height = 50 } //optional
            };
            var imgBitmap = writer.Write(data);
            using (var stream = new MemoryStream())
            {
                imgBitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        //Iznos mora biti u skladu s propisima 
        //Puni se s desna na lijevo, ima 15 znakova, lijevo se nadopunjuju 0
        private string BarCodeIznos(string iznos)
        {
            string iznosPremaPropisu = ""; // Ovo je varijabla koja će u konačnici imati oblik kakav treba biti kod skeniranja
            string praviIznos = iznos;
            int kolikoZnakovaNisuNule = PrebrojiOnoStoNijeNulaIliZarez(praviIznos); //Maknemo , i . iz pravog iznosa
            int kolikoNula = 15 - kolikoZnakovaNisuNule;
            //U string upisujemo 15-kolikoZnakovaNisuNule nula.
            for (int i = 0; i < kolikoNula; i++)
                iznosPremaPropisu += "0";
            //U string upisujemo iznos bez točke i zareza -> TAKO JE PROPISANO ZAKONOM da mora biti kod skeniranja
            for (int i = 0; i < praviIznos.Length; i++)
            {
                if (praviIznos[i] != ',' && praviIznos[i] != '.')
                    iznosPremaPropisu += praviIznos[i];
            }
            return iznosPremaPropisu;
        }

        private int PrebrojiOnoStoNijeNulaIliZarez(string iznos)
        {
            int brojZnakova = 0;
            for (int i = 0; i < iznos.Length; i++)
            {
                if (iznos[i] != '.' && iznos[i] != ',')
                    brojZnakova++;
            }
            return brojZnakova;
        }

        //Svaki zapis ima određenu duljinu polja.
        //Prema zapisniku, STRIKTNO se treba poštivati duljina polja, inače se PDF417 uopće ne želi skenirati u bankovnim aplikacijama.
        //Prvi argument je zapis koji se treba dodati, a drugi argument je maximalna duljina određena zapisnikom.
        //Ako je duljina zapisa veća od maximalne dopuštene duljine, smanjujemo duljinu zapisa na maximalnu duljinu.
        string DodajRedak(string zapis, int maximalnaDuljina)
        {
            string zapisZaDodati = zapis;
            if (zapisZaDodati.Length > maximalnaDuljina)
                zapisZaDodati = zapisZaDodati.Substring(0, maximalnaDuljina);
            return zapisZaDodati;
        }

        //Ako neki od podataka za bar kod u postavkama ne postoji,
        //tada se bar kod ne ispisuje.
        private bool SviPodaciPostoje(string barCodeString)
        {
            bool podaciPostoje = true;
            string[] redovi = barCodeString.Split('\n');
            string[] imenaRedova = {"-","-","Iznos","Ime i prezime platitelja","Ulica i broj platitelja","Poštanski broj i mjesto platitelja","Naziv primatelja",
                            "Ulica i broj primatelja","Poštanski broj i mjesto primatelja","IBAN primatelja","Model računa primatelja",
                            "Poziv na broj primatelja","Šifra namjene","Opis plaćanja" };
            for (int i = 0; i < redovi.Length && podaciPostoje; i++)
            {
                if (redovi[i].Trim() == "-")
                {
                    MessageBox.Show("Barkod neće biti generiran jer nedostaje podatak o: " + imenaRedova[i] + ".","Informacija",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    podaciPostoje = false;
                }
            }
            return podaciPostoje;
        }
        //-----------------------------------------------------------------------------------------------------------//


        //DataRow RowPdv;
        private void StopePDVa(double pdv, double iznos, double osnovica)
        {
            DataRow[] dataROW = dSstope.Tables["DTstope"].Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSstope.Tables["DTstope"].NewRow();
                RowPdv["stopa"] = pdv;
                RowPdv["iznos"] = iznos;
                RowPdv["osnovica"] = osnovica;
                dSstope.Tables["DTstope"].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznos;
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private void FillPonude(string broj)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            string sql2 = "SELECT " +
               " replace(ponude_stavke.kolicina, ',','.')::numeric as kolicina," +
               " ponude_stavke.vpc," +
               " ponude_stavke.porez," +
               " ponude_stavke.broj_ponude," +
               " ponude_stavke.rabat," +
               " ponude_stavke.sifra," +
               " roba.naziv as naziv," +
               " ponude_stavke.id_skladiste AS skladiste," +
               " ponude_stavke.mpc," +
               " ponude_stavke.vpc * replace(ponude_stavke.kolicina, ',','.')::numeric as iznos," +
               " ponude_stavke.porez_potrosnja," +
               " 'OVO NIJE FISKALIZIRAN RAČUN' as string1" +
               " FROM ponude_stavke" +
               " LEFT JOIN roba ON roba.sifra=ponude_stavke.sifra WHERE ponude_stavke.broj_ponude='" + broj + "'";
            DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc_stavka = 0;
            double kol_stavka = 0;
            double porez_potrosnja = 0;
            double pdv = 0;
            double rabat = 0;
            double mpc_stavka = 0;
            double rabat_stavka = 0;
            double pdv_stavka = 0;
            double osnovica_stavka = 0;
            double RabatSve = 0;
            double potrosnjaUkupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc_stavka = Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                kol_stavka = Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
                pdv = Convert.ToDouble(DT.Rows[i]["porez"].ToString());
                rabat = Convert.ToDouble(DT.Rows[i]["rabat"].ToString());
                //porez_potrosnja = Convert.ToDouble(DT.Rows[i]["porez_potrosnja"].ToString());
                porez_potrosnja = 0;
                double.TryParse(DT.Rows[i]["porez_potrosnja"].ToString(), out porez_potrosnja);
                //mpc_stavka = Convert.ToDouble(DT.Rows[i]["mpc"].ToString());
                mpc_stavka = vpc_stavka * (1 + (pdv / 100));
                //mpc_stavka = mpc_stavka - (mpc_stavka * rabat / 100);

                pdv = vpc_stavka * (pdv / 100);
                porez_potrosnja = (vpc_stavka * (porez_potrosnja / 100));
                rabat = (mpc_stavka * (rabat / 100));

                rabat_stavka = (rabat * kol_stavka);
                mpc_stavka = (((mpc_stavka - rabat) * kol_stavka));
                pdv_stavka = (pdv * kol_stavka);

                osnovica_stavka = (vpc_stavka * kol_stavka);
                porez_potrosnja = (porez_potrosnja * kol_stavka);

                RabatSve += rabat_stavka;
                osnovica_ukupno += osnovica_stavka;
                pdv_ukupno += pdv_stavka;
                SveUkupno += mpc_stavka;
                potrosnjaUkupno += porez_potrosnja;

                StopePDVa(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), pdv_stavka, osnovica_stavka);
            }

            porez_potrosnja = SveUkupno - pdv_ukupno - osnovica_ukupno;
            SveUkupno = osnovica_ukupno + pdv_ukupno + potrosnjaUkupno - RabatSve;

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(SveUkupno.ToString(), ',', "kn", "lp").ToString();

            string sql = "SELECT " +
                " ponude.broj_ponude," +
                " ponude.date AS datum," +
                " ponude.vrijedi_do AS datum_dvo, '1970-01-01' as datum_valute, " +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (ponude.model AS nvarchar) + '  '+ CAST (ponude.broj_ponude AS nvarchar)+CAST (ponude.godina_ponude AS nvarchar)+'-'+CAST (ponude.id_fakturirati AS nvarchar) AS model," +
                " ponude.napomena," +
                " '" + RabatSve + "' AS rabat," +
                " '" + SveUkupno + "' AS ukupno," +
                " '" + Math.Round(pdv_ukupno, 2, MidpointRounding.AwayFromZero) + "' AS iznos_pdv," +
                " '" + osnovica_ukupno + "' AS osnovica," +
                " ponude.godina_ponude," +
                " CAST (ponude.broj_ponude AS nvarchar) +'/'+ CAST (ponude.godina_ponude AS nvarchar) AS Naslov," +
                " case when vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end AS kupac_tvrtka," +
                " 'Vrijedi do:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " partners.oib AS kupac_oib," +
                " '" + potrosnjaUkupno.ToString("#0.00") + "' as string3" +
                " FROM ponude" +
                " LEFT JOIN partners ON partners.id_partner=ponude.id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=ponude.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=ponude.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=ponude.zr" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=ponude.id_zaposlenik_izradio WHERE ponude.broj_ponude='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";

            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }

            if (id_kupac.Length == 0)
                id_kupac = "0";

            string grad_kupac = "";
            DataTable DTgrad_kupac = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + id_kupac + "'", "grad").Tables[0];
            if (DTgrad_kupac.Rows.Count != 0)
            {
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                dSFaktura.Tables[0].Rows[0]["kupac_grad"] = grad_kupac;
            }

            string poslovnica_grad = "";
            DataTable DTposlovnica_grad = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + Class.PodaciTvrtka.gradPoslovnicaId + "'", "grad").Tables[0];
            if (DTposlovnica_grad.Rows.Count != 0)
            {
                poslovnica_grad = DTposlovnica_grad.Rows[0]["posta"].ToString().Trim() + " " + DTposlovnica_grad.Rows[0]["grad"].ToString();
            }

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
                " '" + grad_kupac + "' AS grad_kupac," +
                " podaci_tvrtka.poslovnica_adresa," +
                "  '" + poslovnica_grad + "' as poslovnica_grad," +
                " podaci_tvrtka.email," +
                " 'Ponuda ' AS naziv_fakture," +
                " podaci_tvrtka.text_bottom, " +
                "podaci_tvrtka.nazivPoslovnice as ime_poslovnice,  podaci_tvrtka.swift, podaci_tvrtka.pdvBr as pdv_br," +
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
            this.reportViewer1.RefreshReport();
        }

        private void FillOtpremnicu(string broj, string skladiste)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " otpremnica_stavke.kolicina," +
                " otpremnica_stavke.vpc," +
                " otpremnica_stavke.porez," +
                " otpremnica_stavke.broj_otpremnice," +
                " otpremnica_stavke.rabat," +
                " otpremnica_stavke.sifra_robe," +
                " roba.naziv as naziv," +
                " otpremnica_stavke.vpc * otpremnica_stavke.kolicina as iznos, " +
                " otpremnica_stavke.id_skladiste AS skladiste," +
                " otpremnica_stavke.porez_potrosnja," +
                " 'OVO NIJE FISKALIZIRAN RAČUN' as string1" +
                " FROM otpremnica_stavke" +
                " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe WHERE otpremnica_stavke.broj_otpremnice='" + broj + "' AND otpremnica_stavke.id_skladiste='" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double osnovica = 0;
            double pdv_stavka = 0;
            double Porez_potrosnja_stavka = 0;
            double ukupno = 0;

            double osnovica_sve = 0;
            double osnovica_pnp = 0;
            double Porez_potrosnja_sve = 0;
            double pdv_sve = 0;
            ukupno_rabat = 0;
            double rabat = 0;

            double maxPNP = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                double kolicina = Convert.ToDouble(DT.Rows[i]["kolicina"].ToString().Replace('.', ','));
                double PP = 0;
                if (DT.Columns.Contains("porez_potrosnja"))
                    PP = Convert.ToDouble(DT.Rows[i]["porez_potrosnja"].ToString());

                double PDV = Convert.ToDouble(DT.Rows[i]["porez"].ToString().Replace('.', ','));
                double VPC = Convert.ToDouble(DT.Rows[i]["vpc"].ToString().Replace('.', ','));
                double cijena = Math.Round(((VPC * (PP + PDV) / 100) + VPC), 3);
                double mpc = cijena * kolicina;

                rabat = Convert.ToDouble(DT.Rows[i]["rabat"].ToString().Replace('.', ','));
                ukupno_rabat = (mpc * rabat / 100) + ukupno_rabat;
                mpc = Math.Round((mpc - (mpc * rabat / 100)), 3);

                //Ovaj kod dobiva PDV
                double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                //Ovaj kod dobiva porez na potrošnju
                double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                if (PP > 0)
                {
                    osnovica_pnp += mpc / (1 + (PP + PDV) / 100);
                }

                if (maxPNP < PP)
                    maxPNP = PP;

                //izračun porez potrosnja
                Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                //izračun osnovica
                if (rabat > 0)
                {
                    double pps = (100 * (PDV + PP)) / (100 + PDV + PP);
                    osnovica = mpc - (mpc * pps / 100);
                }
                else
                {
                    osnovica = VPC * kolicina;
                }

                osnovica_sve = (osnovica) + osnovica_sve;

                StopePDVa(PDV, pdv_stavka, (osnovica));

                //Izracun pdv
                pdv_sve = pdv_sve + (pdv_stavka);
                ukupno = mpc + ukupno;
            }

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(ukupno.ToString(), ',', "kn", "lp").ToString();

            string sql = "SELECT " +
                " otpremnice.broj_otpremnice," +
                " otpremnice.datum AS datum, '1970-01-01' as datum_dvo, '1970-01-01' as datum_valute, " +
                " 'Mjesto otpreme:  '+otpremnice.mj_otpreme  + '\nAdresa otpreme:  ' + otpremnice.adr_otpreme + '\nIstovarno mjesto:  ' +  otpremnice.istovarno_mj + '\nRegistracija:  ' +  otpremnice.registracija    AS string1," +
                " 'Isprave:  '+otpremnice.isprave  + '\nTroškovi prijevoza:  ' + otpremnice.troskovi_prijevoza + '\nIstovarni rok:  ' +  otpremnice.istovarni_rok    AS string2," +
                " otprema.naziv AS otprema," +
                " otpremnice.napomena," +
                " '" + ukupno_rabat.ToString("#0.00") + "' AS rabat," +
                " '" + ukupno.ToString("#0.00") + "' AS ukupno," +
                " '" + pdv_sve.ToString("#0.00") + "' AS iznos_pdv," +
                " '" + osnovica_sve.ToString("#0.00") + "' AS osnovica," +
                " '" + Porez_potrosnja_sve.ToString("#0.00") + "' as string3, " +
                " otpremnice.godina_otpremnice," +
                " CAST (otpremnice.broj_otpremnice AS nvarchar) +'/'+ CAST (otpremnice.godina_otpremnice AS nvarchar) AS Naslov," +
                " case when otpremnice.na_sobu then sobe.naziv_sobe else partners.ime_tvrtke end AS kupac_tvrtka," +
                " case when otpremnice.na_sobu then '' else partners.adresa end AS kupac_adresa," +
                " case when otpremnice.na_sobu then '0' else partners.id_grad end AS id_kupac_grad," +
                " case when otpremnice.na_sobu then '0' else partners.id_partner end AS sifra_kupac," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " case when otpremnice.na_sobu then 'SOBA' else partners.oib end AS kupac_oib" +
                " FROM otpremnice" +
                " LEFT JOIN partners ON partners.id_partner=otpremnice.osoba_partner" +
                " LEFT JOIN sobe ON sobe.id=otpremnice.osoba_partner" +
                " LEFT JOIN otprema ON otprema.id_otprema=otpremnice.id_otprema" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=otpremnice.id_izradio WHERE otpremnice.broj_otpremnice='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            if (id_kupac.Length == 0)
                id_kupac = "0";

            string grad_kupac = "";
            DataTable DTgrad_kupac = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + id_kupac + "'", "grad").Tables[0];
            if (DTgrad_kupac.Rows.Count != 0)
            {
                grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                dSFaktura.Tables[0].Rows[0]["kupac_grad"] = grad_kupac;
            }

            string poslovnica_grad = "";
            DataTable DTposlovnica_grad = classSQL.select("SELECT grad,posta FROM grad WHERE id_grad = '" + Class.PodaciTvrtka.gradPoslovnicaId + "'", "grad").Tables[0];
            if (DTposlovnica_grad.Rows.Count != 0)
            {
                poslovnica_grad = DTposlovnica_grad.Rows[0]["posta"].ToString().Trim() + " " + DTposlovnica_grad.Rows[0]["grad"].ToString();
            }

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
                " '" + grad_kupac + "' AS grad_kupac," +
                " podaci_tvrtka.poslovnica_adresa," +
                "  '" + poslovnica_grad + "' as poslovnica_grad," +
                " podaci_tvrtka.email," +
                " 'OTPREMNICA ' AS naziv_fakture," +
                " podaci_tvrtka.text_bottom, " +
                "podaci_tvrtka.nazivPoslovnice as ime_poslovnice,  podaci_tvrtka.swift, podaci_tvrtka.pdvBr as pdv_br," +
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
            }
            this.reportViewer1.RefreshReport();
        }
    }
}