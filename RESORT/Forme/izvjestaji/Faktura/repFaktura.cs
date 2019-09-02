using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RESORT.izvjestaji.Faktura
{
    public partial class repFaktura : Form
    {
        public repFaktura()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }

        private void repFaktura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;

            if (dokumenat == "FAK")
            {
                if (broj_dokumenta == null) { return; }
                FillFaktura(broj_dokumenta);
            }

            if (dokumenat == "PON")
            {
                if (broj_dokumenta == null) { return; }
                FillPonude(broj_dokumenta);
            }
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }

        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal pdv_stavka, decimal osnovica)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = pdv.ToString();
                RowPdv["stavka"] = pdv_stavka.ToString();
                RowPdv["osnovica"] = osnovica.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["stavka"] = Convert.ToDecimal(dataROW[0]["stavka"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private void FillPonude(string broj)
        {
            string sql2 = "SELECT " +
                " Rponude_stavke.id," +
                " Rponude_stavke.dana," +
                " Rponude_stavke.ukupno," +
                " Rponude_stavke.porez," +
                " Rponude_stavke.broj," +
                " Rponude_stavke.opis_usluge AS ime_gosta," +
                " Rponude_stavke.datumdolaska," +
                " Rponude_stavke.datumodlaska," +
                " Rponude_stavke.boravisna_pristojba," +
                " Rponude_stavke.iznos_usluge," +
                " Rponude_stavke.rabat," +
                " Rponude_stavke.cijena_sobe " +
                " FROM Rponude_stavke" +
                " WHERE Rponude_stavke.broj='" + broj + "'";

            RemoteDB.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");

            DataTable DTs = dSRfakturaStavke.Tables["DTfakturaStavke"];

            decimal ukupno = 0;
            decimal bor_p = 0;
            decimal porez = 0;
            decimal broj_nocenja = 0;
            decimal porez_iznos = 0;
            decimal iznos_usluge = 0;
            decimal cijena_sobe = 0;
            decimal ukupno_za_preracun_osnovice = 0;
            decimal boravisna_ukupno = 0;
            decimal preracunata_stopa = 0;

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("stavka");
                DTpdv.Columns.Add("osnovica");
            }
            else
            {
                DTpdv.Clear();
            }

            decimal ukupno_u_valuti = 0;
            foreach (DataRow row in DTs.Rows)
            {
                try
                {
                    ukupno = Convert.ToDecimal(row["ukupno"].ToString());
                    bor_p = Convert.ToDecimal(row["boravisna_pristojba"].ToString());
                    porez = Convert.ToDecimal(row["porez"].ToString());
                    iznos_usluge = Convert.ToDecimal(row["iznos_usluge"].ToString());
                    cijena_sobe = Convert.ToDecimal(row["cijena_sobe"].ToString());
                    broj_nocenja = Convert.ToDecimal(row["dana"].ToString());

                    ukupno_za_preracun_osnovice = ukupno - (bor_p * broj_nocenja);

                    preracunata_stopa = (100 * porez) / (100 + porez);
                    porez_iznos = ((ukupno_za_preracun_osnovice * preracunata_stopa) / 100);

                    StopePDVa(porez, porez_iznos, ukupno_za_preracun_osnovice - porez_iznos);
                    boravisna_ukupno = (bor_p * broj_nocenja) + boravisna_ukupno;

                    ukupno_u_valuti += ukupno;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            string porezi = "Boravišna pristojba:" + BrojiRedove((boravisna_ukupno).ToString("#0.00"), 9) + " kn\r\n";
            for (int ii = 0; ii < DTpdv.Rows.Count; ii++)
            {
                porezi += "Osnovica za porez od " + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["osnovica"].ToString()).ToString("#0.00"), 9) + " kn\r\n" +
                          "Iznos poreza od " + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["stavka"].ToString()).ToString("#0.00"), 9) + " kn\r\n";
            }

            string sql = "SELECT " +
                " Rponude.broj," +
                " Rponude.datum," +
                " 'Ponuda: ' + Rponude.broj + '/' + Rponude.godina AS naslov," +
                " Rponude.godina," +
                " Rponude.datumDVO AS datum_dvo," +
                " Rponude.datum_valute," +
                " '" + porezi + "' AS porezne_stope," +
                " nacin_placanja.naziv_placanja," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " partners.ime_tvrtke as kupac_tvrtka," +
                " partners.oib as kupac_oib," +
                " partners.id_partner," +
                " partners.adresa as kupac_adresa," +
                " grad.grad as kupac_grad," +
                " '' as string6," +
                " Rponude.id_valuta," +
                " valute.tecaj," +
                " valute.naziv," +
                " valute.ime_valute" +
                " FROM Rponude " +
                " LEFT JOIN valute ON valute.id_valuta=Rponude.id_valuta" +
                " LEFT JOIN nacin_placanja ON Rponude.nacin_placanja=nacin_placanja.id_placanje" +
                " LEFT JOIN zaposlenici ON Rponude.id_izradio=zaposlenici.id_zaposlenik" +
                " LEFT JOIN partners ON Rponude.id_partner=partners.id_partner" +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad" +
                " WHERE Rponude.broj='" + broj + "'" +
                "";

            RemoteDB.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");

            if (dSFaktura.Tables["DTRfaktura"].Rows[0]["id_valuta"].ToString() != "5")
            {
                string ime_valute_skraceno = dSFaktura.Tables["DTRfaktura"].Rows[0]["naziv"].ToString();
                decimal _tecaj;
                decimal.TryParse(dSFaktura.Tables["DTRfaktura"].Rows[0]["tecaj"].ToString(), out _tecaj);
                if (_tecaj == 0) { _tecaj = 1; }
                dSFaktura.Tables["DTRfaktura"].Rows[0]["string6"] = "UKUPNO U VALUTI: " + (ukupno_u_valuti / _tecaj).ToString("N2") + " " + ime_valute_skraceno;
            }

            string sql1 = "SELECT " +
                " podaci_tvrtke.ime_tvrtke," +
                " podaci_tvrtke.oib," +
                " podaci_tvrtke.tel," +
                " podaci_tvrtke.fax," +
                " podaci_tvrtke.mob," +
                " podaci_tvrtke.iban," +
                " podaci_tvrtke.iban1," +
                " podaci_tvrtke.adresa," +
                " podaci_tvrtke.grad," +
                " podaci_tvrtke.vl," +
                " podaci_tvrtke.poslovnica_adresa," +
                " podaci_tvrtke.poslovnica_grad," +
                " podaci_tvrtke.email," +
                " podaci_tvrtke.opis_na_kraju_fakture " +
                " FROM podaci_tvrtke ";

            classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            this.reportViewer1.RefreshReport();
        }

        private void FillFaktura(string broj)
        {
            DataTable DT_postavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

            string sql2 = string.Format(@"SELECT
rfaktura_stavke.id,
round(rfaktura_stavke.dana, 2) as dana,
rfaktura_stavke.ukupno,
rfaktura_stavke.porez,
rfaktura_stavke.broj,
rfaktura_stavke.ime_gosta,
rfaktura_stavke.datumdolaska,
rfaktura_stavke.datumodlaska,
rfaktura_stavke.boravisna_pristojba,
rfaktura_stavke.iznos_usluge,
round(rfaktura_stavke.rabat, 2) as rabat,
rfaktura_stavke.cijena_sobe,
rfaktura_stavke.broj_sobe,
rfaktura_stavke.otpremnica_pnp
FROM rfaktura_stavke
WHERE rfaktura_stavke.broj = '{0}'
ORDER BY rfaktura_stavke.broj ASC", broj);

            //RemoteDB.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");

            DataTable DT = RemoteDB.select(sql2, "rfaktura_stavke").Tables[0];

            DateTime d1, d2;
            decimal boravisna_prostojba_ukupno = 0;

            bool prikazi_datum = false;
            foreach (DataRow r in DT.Rows)
            {
                DateTime.TryParse(r["datumdolaska"].ToString(), out d1);
                DateTime.TryParse(r["datumodlaska"].ToString(), out d2);

                if (d1.ToString("yyyy-MM-dd H:mm:ss") != "1970-01-10 0:00:01" && d2.ToString("yyyy-MM-dd H:mm:ss") != "1970-01-10 0:00:01")
                {
                    prikazi_datum = true;
                }
                int broj_sobe = -1;
                if (int.TryParse(r["broj_sobe"].ToString(), out broj_sobe) && broj_sobe == -1)
                {
                    decimal boravisna_pristojba = 0;
                    decimal.TryParse(r["ukupno"].ToString(), out boravisna_pristojba);
                    boravisna_prostojba_ukupno += boravisna_pristojba;
                }

                //if (d1.Year == 1970) {
                //    r["datumdolaska"] = "";
                //}
                //if (d2.Year == 1970)
                //{
                //    r["datumodlaska"] = "";
                //}
            }

            ////////////////////////////////////////////////////////////////RUČNO DODAJE FAKTURE////////////////////////////////

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow DTrow = dSRfakturaStavke.Tables["DTfakturaStavke"].NewRow();
                DTrow["dana"] = DT.Rows[i]["dana"].ToString();
                DTrow["ukupno"] = DT.Rows[i]["ukupno"].ToString();
                DTrow["porez"] = DT.Rows[i]["porez"].ToString();
                DTrow["broj"] = DT.Rows[i]["broj"].ToString();
                DTrow["ime_gosta"] = DT.Rows[i]["ime_gosta"].ToString();
                if (DT.Rows[i]["broj"].ToString() == "0")
                {
                    DTrow["datumdolaska"] = "";
                    DTrow["datumodlaska"] = "";
                }
                else
                {
                    DateTime.TryParse(DT.Rows[i]["datumdolaska"].ToString(), out d1);
                    DateTime.TryParse(DT.Rows[i]["datumodlaska"].ToString(), out d2);

                    if (d1.Year == 1970)
                    {
                        DTrow["datumdolaska"] = "";
                    }
                    else
                    {
                        DTrow["datumdolaska"] = Convert.ToDateTime(DT.Rows[i]["datumdolaska"].ToString()).ToString("dd.MM.yyyy");
                    }
                    if (d2.Year == 1970)
                    {
                        DTrow["datumodlaska"] = "";
                    }
                    else
                    {
                        DTrow["datumodlaska"] = Convert.ToDateTime(DT.Rows[i]["datumodlaska"].ToString()).ToString("dd.MM.yyyy");
                    }
                }

                if (!prikazi_datum)
                {
                    DTrow["datumdolaska"] = "";
                    DTrow["datumodlaska"] = "";
                }

                DTrow["rabat"] = DT.Rows[i]["rabat"].ToString();
                DTrow["otpremnica_pnp"] = DT.Rows[i]["otpremnica_pnp"].ToString();
                DTrow["cijena_sobe"] = DT.Rows[i]["cijena_sobe"].ToString();
                dSRfakturaStavke.Tables["DTfakturaStavke"].Rows.Add(DTrow);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////

            DataTable DTs = dSRfakturaStavke.Tables["DTfakturaStavke"];

            decimal ukupno = 0;
            decimal bor_p = 0;
            decimal porez = 0;
            decimal broj_nocenja = 0;
            decimal porez_iznos = 0;
            decimal iznos_usluge = 0;
            decimal cijena_sobe = 0;
            decimal ukupno_za_preracun_osnovice = 0;
            decimal boravisna_ukupno = 0;
            decimal preracunata_stopa = 0;
            decimal otpremnica_pnp = 0;

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("stavka");
                DTpdv.Columns.Add("osnovica");
            }
            else
            {
                DTpdv.Clear();
            }

            decimal ukupno_u_valuti = 0;
            foreach (DataRow row in DTs.Rows)
            {
                try
                {
                    ukupno = Convert.ToDecimal(row["ukupno"].ToString());
                    //bor_p = Convert.ToDecimal(row["boravisna_pristojba"].ToString());
                    porez = Convert.ToDecimal(row["porez"].ToString());
                    //iznos_usluge = Convert.ToDecimal(row["iznos_usluge"].ToString());
                    cijena_sobe = Convert.ToDecimal(row["cijena_sobe"].ToString());
                    broj_nocenja = Convert.ToDecimal(row["dana"].ToString());
                    if (DTs.Columns.Contains("otpremnica_pnp"))
                    {
                        //otpremnica_pnp = Convert.ToDecimal(row["otpremnica_pnp"].ToString());
                        decimal.TryParse(row["otpremnica_pnp"].ToString(), out otpremnica_pnp);
                    }
                    else
                    {
                        otpremnica_pnp = 0;
                    }

                    otpremnica_pnp = 0;

                    ukupno_za_preracun_osnovice = ukupno - (bor_p * broj_nocenja);

                    preracunata_stopa = (100 * porez) / (100 + porez);
                    porez_iznos = ((ukupno_za_preracun_osnovice * preracunata_stopa) / 100);

                    StopePDVa(porez, porez_iznos, ukupno_za_preracun_osnovice - porez_iznos);
                    boravisna_ukupno = (bor_p * broj_nocenja) + boravisna_ukupno;

                    ukupno_u_valuti += ukupno;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //-------------------------UZIMA PODATKE ZA FISKALNI RAČUN--------------------------------------------------------------------------------

            string sqlll = "SELECT oznaka_prodajnog_mj, oznakaPP FROM podaci_fiskalizacija";
            DataTable DTfis = classDBlite.LiteSelect(sqlll, "All").Tables[0];

            int mjesta = 11;
            string porezi = "";//"Boravišna pristojba:" + BrojiRedove((boravisna_ukupno).ToString("#0.00"), 9) + " kn\r\n";
            for (int ii = 0; ii < DTpdv.Rows.Count; ii++)
            {
                if (Convert.ToDecimal(DTpdv.Rows[ii]["stopa"].ToString()) > 0)
                {
                    porezi += "Osnovica za porez od " + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["osnovica"].ToString()).ToString("##,#0.00"), mjesta) + " kn\r\n" +
                          "Iznos poreza od " + DTpdv.Rows[ii]["stopa"].ToString() + " %:" + BrojiRedove(Convert.ToDecimal(DTpdv.Rows[ii]["stavka"].ToString()).ToString("##,#0.00"), mjesta) + " kn\r\n";
                }
            }
            porezi += "Boravišna pristojba: " + BrojiRedove(boravisna_prostojba_ukupno.ToString("##,#0.00"), mjesta) + " kn\r\n";

            DataTable DTpt = classDBlite.LiteSelect("SELECT podaci_tvrtke.r1 FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];

            string dod = "'' AS string1, '' as string2,";
            if (prikazi_datum)
            {
                dod = "'Dolazak' AS string1, 'Odlazak' as string2,";
            }
            string prodajnoMJ = "", broj_kase = "";
            DataTable DTducan = RemoteDB.select(string.Format("SELECT ime_ducana FROM ducan WHERE id_ducan='{0}';", DTfis.Rows[0]["oznakaPP"].ToString()), "ducan").Tables[0];
            if (DTducan.Rows.Count > 0)
                prodajnoMJ = DTducan.Rows[0]["ime_ducana"].ToString();
            else
                prodajnoMJ = "1";

            DataTable DTnaplatni = RemoteDB.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DTfis.Rows[0]["oznaka_prodajnog_mj"].ToString() + "'", "blagajna").Tables[0];
            if (DTnaplatni.Rows.Count > 0)
            {
                broj_kase = DTnaplatni.Rows[0]["ime_blagajne"].ToString();
            }

            string sql = "SELECT " + dod +
                " rfakture.broj," +
                " rfakture.jir," +
                " rfakture.ukupno," +
                " rfakture.napomena," +
                " rfakture.zik," +
                " '' AS string6," +
                " rfakture.datum," +
                " 'Račun " + DTpt.Rows[0][0].ToString() + ": ' + rfakture.broj + '/' + '" + prodajnoMJ + "' + '/' + '" + broj_kase + "' AS naslov," +
                " rfakture.godina," +
                " rfakture.datumDVO AS datum_dvo," +
                " rfakture.datum_valute," +
                " '" + porezi + "' AS porezne_stope," +
                " nacin_placanja.naziv_placanja," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " partners.ime_tvrtke as kupac_tvrtka," +
                " partners.oib as kupac_oib," +
                " partners.id_partner," +
                " partners.adresa as kupac_adresa," +
                " grad.grad as kupac_grad," +
                " zemlja.zemlja as zemlja," +
                " valute.id_valuta," +
                " valute.tecaj," +
                " valute.naziv," +
                " valute.ime_valute" +
                " FROM rfakture " +
                " LEFT JOIN valute ON valute.id_valuta=rfakture.id_valuta" +
                " LEFT JOIN nacin_placanja ON rfakture.nacin_placanja=nacin_placanja.id_placanje" +
                " LEFT JOIN zaposlenici ON rfakture.id_izradio=zaposlenici.id_zaposlenik" +
                " LEFT JOIN partners ON rfakture.id_partner=partners.id_partner" +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad" +
                " LEFT JOIN zemlja ON grad.id_drzava=zemlja.id_zemlja" +
                " WHERE rfakture.broj='" + broj + "'" +
                "";

            RemoteDB.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");

            if (dSFaktura.Tables["DTRfaktura"].Rows[0]["kupac_grad"].ToString() != "")
            {
                dSFaktura.Tables["DTRfaktura"].Rows[0]["kupac_grad"] = dSFaktura.Tables["DTRfaktura"].Rows[0]["kupac_grad"] + "\r\nZemlja: " +
                dSFaktura.Tables["DTRfaktura"].Rows[0]["zemlja"];
            }

            if (dSFaktura.Tables["DTRfaktura"].Rows[0]["id_valuta"].ToString() != "5")
            {
                string ime_valute_skraceno = dSFaktura.Tables["DTRfaktura"].Rows[0]["naziv"].ToString();
                decimal _tecaj;
                decimal.TryParse(dSFaktura.Tables["DTRfaktura"].Rows[0]["tecaj"].ToString(), out _tecaj);
                if (_tecaj == 0) { _tecaj = 1; }
                dSFaktura.Tables["DTRfaktura"].Rows[0]["string6"] = "UKUPNO U VALUTI: " + (ukupno_u_valuti / _tecaj).ToString("N2") + " " + ime_valute_skraceno;
            }

            classNumberToLetter NT = new classNumberToLetter();
            string brojSlovima = NT.PretvoriBrojUTekst(dSFaktura.Tables[0].Rows[0]["ukupno"].ToString(), ',', "kn", "lp");

            string sql1 = "SELECT " +
                " podaci_tvrtke.ime_tvrtke," +
                " podaci_tvrtke.oib," +
                " podaci_tvrtke.tel," +
                " podaci_tvrtke.fax," +
                " podaci_tvrtke.mob," +
                " podaci_tvrtke.iban," +
                " podaci_tvrtke.iban1," +
                " podaci_tvrtke.adresa," +
                " podaci_tvrtke.grad," +
                " '" + DT_postavke.Rows[0]["path_image"].ToString() + "'as path_image," +
                " '" + DT_postavke.Rows[0]["bool_path_image"].ToString() + "'as bool_path_image," +
                " podaci_tvrtke.vl," +
                " podaci_tvrtke.poslovnica_adresa," +
                " podaci_tvrtke.poslovnica_grad," +
                " podaci_tvrtke.email," +
                " '" + brojSlovima + "' AS broj_slovima," +
                " 'PLAĆANJE: " + dSFaktura.Tables[0].Rows[0]["naziv_placanja"].ToString() + "' AS placanje," +
                " podaci_tvrtke.opis_na_kraju_fakture " +
                " FROM podaci_tvrtke ";

            classDBlite.LiteAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private string BrojiRedove(string str, int br)
        {
            string vrati = "";
            for (int i = 0; i < br; i++)
            {
                if (vrati.Length == (br - str.Length))
                {
                    vrati += str;
                    return vrati;
                }
                else if ((br - str.Length) < br)
                {
                    vrati += " ";
                }
            }

            return vrati;
        }

        private void repFaktura_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}