using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmFakturaStorno : Form
    {
        public frmFakturaStorno()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private static DataTable DToib = classSQL.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private static DataTable DTpdv = new DataTable();

        public string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifraSkladiste { get; set; }
        public string sifraPartnera { get; set; }
        public string blagajnik_ime { get; set; }
        public bool FakturaFiskalizirana = false;

        private void button1_Click(object sender, EventArgs e)
        {
            string broj;

            DataTable DTzki = classSQL.select("Select zki, storno From fakture where broj_fakture = '" + textBox1.Text + "' and godina_fakture='" + nmGodinaFakture.Value.ToString() + "'", "zki").Tables[0];
            if (DTzki.Rows[0]["zki"].ToString() != "")
            {
                FakturaFiskalizirana = true;
            }

            if (DTzki.Rows[0]["storno"].ToString() == "DA")
            {
                MessageBox.Show("Faktura je već stornirana !");
                return;
            }

            try
            {
                broj = Convert.ToInt64(textBox1.Text).ToString();
            }
            catch
            {
                MessageBox.Show("Krivi broj fakture.", "Greška");
                return;
            }

            DataTable DT = classSQL.select("SELECT broj_fakture FROM fakture WHERE broj_fakture='" + broj + "' and godina_fakture='" + nmGodinaFakture.Value.ToString() + "'", "fakture").Tables[0];

            if (DT.Rows.Count != 0)
            {
                stornirajFakturuHelper(broj);
            }
            else
            {
                MessageBox.Show("U bazi ne postoji ova faktura.", "Greška");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable DTzki = new DataTable();

            try
            {
                DTzki = classSQL.select("Select zki, storno From fakture " +
                    " where broj_fakture = '" + BrojFakture() + "' and godina_fakture='" + nmGodinaFakture.Value.ToString() + "'" +
                    " AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "zki").Tables[0];
            }
            catch
            {
                return;
            }

            if (DTzki.Rows[0]["zki"].ToString() != "")
            {
                FakturaFiskalizirana = true;
            }
            if (DTzki.Rows[0]["storno"].ToString() == "DA")
            {
                MessageBox.Show("Faktura je već stornirana !");
                return;
            }

            string br = BrojFakture();
            if (br != "")
            {
                stornirajFakturuHelper(br);
            }
            else
            {
                MessageBox.Show("U bazi ne postoji niti jedna faktura!");
            }
        }

        private string BrojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_fakture AS integer)) FROM fakture" +
                " WHERE id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "fakture").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                return DSbr.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private void stornirajFakturuHelper(string broj)
        {
            DataTable DSracun = classSQL.select("SELECT storno FROM fakture where broj_fakture='" + broj + "' " +
                "  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "' " +
                " and godina_fakture='" + nmGodinaFakture.Value.ToString() + "'", "fakture").Tables[0];

            if (DSracun.Rows[0]["storno"].ToString() == "DA")
            {
                MessageBox.Show("Faktura je već stornirana!");
                this.ActiveControl = textBox1;
                textBox1.SelectAll();
            }
            else
            {
                if (FakturaFiskalizirana)
                {
                    StornirajFakturu(broj);
                }
                MessageBox.Show("Faktura " + broj + " je stornirana.");
                this.Close();
            }
        }

        private string[] InsertFaktura(string staraFaktura, string kasa, string ducan, string partner)
        {
            DataTable DTracun = classSQL.select("SELECT * FROM fakture where broj_fakture='" + staraFaktura + "' and godina_fakture='" + nmGodinaFakture.Value.ToString() + "'", "fakture").Tables[0];

            string ukupno_rabat = (Convert.ToDouble(DTracun.Rows[0]["ukupno_rabat"].ToString()) * -1).ToString();
            string ukupno_porez = (Convert.ToDouble(DTracun.Rows[0]["ukupno_porez"].ToString()) * -1).ToString();
            string ukupno_mpc = (Convert.ToDouble(DTracun.Rows[0]["ukupno_mpc"].ToString()) * -1).ToString();
            string ukupno_vpc = (Convert.ToDouble(DTracun.Rows[0]["ukupno_vpc"].ToString()) * -1).ToString();
            string ukupno_mpc_rabat = (Convert.ToDouble(DTracun.Rows[0]["ukupno_mpc_rabat"].ToString()) * -1).ToString();
            string ukupno_povratna_naknada = (Convert.ToDouble(DTracun.Rows[0]["ukupno_povratna_naknada"].ToString()) * -1).ToString();
            string ukupno_osnovica = (Convert.ToDouble(DTracun.Rows[0]["ukupno_osnovica"].ToString()) * -1).ToString();
            string ukupno = (Convert.ToDouble(DTracun.Rows[0]["ukupno"].ToString()) * -1).ToString();

            string novaFaktura = (Convert.ToInt32(BrojFakture()) + 1).ToString();
            string sql = "INSERT INTO fakture (broj_fakture,id_odrediste,id_fakturirati,date,dateDVO,datum_valute," +
                "id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma,godina_ponude," +
                "godina_fakture,oduzmi_iz_skladista,tecaj,ukupno,storno," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez,id_ducan,id_kasa)" +
                "VALUES " +
                " (" +
                 " '" + novaFaktura + "'," +
                " '" + DTracun.Rows[0]["id_odrediste"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_fakturirati"].ToString() + "'," +
                " '" + DTracun.Rows[0]["date"].ToString() + "'," +
                " '" + DTracun.Rows[0]["dateDVO"].ToString() + "'," +
                " '" + DTracun.Rows[0]["datum_valute"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_izjava"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_zaposlenik"].ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + DTracun.Rows[0]["model"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'," +
                " '" + DTracun.Rows[0]["zr"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_valuta"].ToString() + "'," +
                " '" + DTracun.Rows[0]["otprema"].ToString() + "'," +
                " '" + "0" + "'," +
                " '" + DTracun.Rows[0]["napomena"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_vd"].ToString() + "'," +
                " '" + DTracun.Rows[0]["godina_predujma"].ToString() + "'," +
                " '" + DTracun.Rows[0]["godina_ponude"].ToString() + "'," +
                " '" + DTracun.Rows[0]["godina_fakture"].ToString() + "'," +
                " '" + DTracun.Rows[0]["oduzmi_iz_skladista"].ToString() + "'," +
                " '" + DTracun.Rows[0]["tecaj"].ToString().Replace(",", ".") + "'," +
                " '" + ukupno + "'," +
                " 'NE'," +
                "'" + ukupno_povratna_naknada.Replace(",", ".") + "'," +
                "'" + ukupno_mpc.Replace(",", ".") + "'," +
                "'" + ukupno_vpc.Replace(",", ".") + "'," +
                "'" + ukupno_mpc_rabat.Replace(",", ".") + "'," +
                "'" + ukupno_rabat.Replace(",", ".") + "'," +
                "'" + ukupno_osnovica.Replace(",", ".") + "'," +
                "'" + ukupno_porez.Replace(",", ".") + "','" + id_ducan + "','" + id_kasa + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sqlStorno = "UPDATE fakture SET storno='DA' WHERE broj_fakture='" + staraFaktura + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'";
            classSQL.update(sqlStorno);

            string sqlNP = "SELECT naziv_placanja FROM nacin_placanja WHERE id_placanje='" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'";
            DataTable dt = classSQL.select(sqlNP, "nacin_placanja").Tables[0];

            string nazivPlacanja = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "O";
            nazivPlacanja = nazivPlacanja.ToLower();

            string np = Util.Korisno.VratiNacinPlacanja(nazivPlacanja);

            string[] a = new string[4];
            a[0] = novaFaktura;
            a[1] = DTracun.Rows[0]["oduzmi_iz_skladista"].ToString();
            a[2] = np;
            a[3] = DTracun.Rows[0]["zki"].ToString();

            return a;
        }

        private void SrediStavkeRobuSkladiste(ref DataTable DSfakturaStavke, string staraFaktura, string novaFaktura, string oduzmi)
        {
            string kol;
            string povrNaknad;
            string sifra;

            //za svaku stavku vrati robu na skladište
            for (int i = 0; i < DSfakturaStavke.Rows.Count; i++)
            {
                sifra = DSfakturaStavke.Rows[i]["sifra"].ToString();

                //povrNaknad = DSracunStavke.Rows[i]["povratna_naknada"].ToString();
                //povrNaknad = (Convert.ToDouble(povrNaknad) * -1).ToString();
                //DSracunStavke.Rows[i].SetField("povratna_naknada", povrNaknad);

                try
                {
                    povrNaknad = DSfakturaStavke.Rows[i]["povratna_naknada"].ToString();
                    povrNaknad = (Convert.ToDouble(povrNaknad)).ToString();
                }
                catch
                {
                    povrNaknad = "0";
                }

                DSfakturaStavke.Rows[i].SetField("povratna_naknada", povrNaknad);

                kol = DSfakturaStavke.Rows[i]["kolicina"].ToString();
                kol = (Convert.ToDouble(kol) * -1).ToString();
                DSfakturaStavke.Rows[i].SetField("kolicina", kol);

                sifraSkladiste = DSfakturaStavke.Rows[i]["id_skladiste"].ToString();

                //postavi broj_racuna na novi račun (kad ne bi stavili, u bazu bi sve stavke išle na stari račun)
                DSfakturaStavke.Rows[i].SetField("broj_fakture", novaFaktura);
                //

                //ako je u fakturi bilo postavljeno da se ne oduzima sa skladišta, onda nema niti vraćanja na skladište
                if (oduzmi != "0")
                {
                    DataTable robaOduzmi = classSQL.select(
                        "SELECT oduzmi FROM roba where sifra='" +
                        sifra + "'", "roba")
                        .Tables[0];

                    if (robaOduzmi.Rows[0]["oduzmi"].ToString() == "DA")
                    {
                        //kolicina je množena prije s minusom i sad se opet množi s minusom, znači +, odnosno
                        //vraća se na skladište
                        kol = SQL.ClassSkladiste.GetAmount(
                            sifra,
                            sifraSkladiste,
                            kol,
                            "1",
                            "-");
                        SQL.SQLroba_prodaja.UpdateRows(
                            sifraSkladiste,
                            kol,
                            sifra);
                    }
                }
            }

            provjera_sql(SQL.SQLfaktura.InsertStavke(DSfakturaStavke));
        }

        private void StornirajFakturu(string staraFaktura)
        {
            //ubaci u racuni i vrati broj nove fakture, oduzmi, način plaćanja i zki
            //zki treba: ako ga nema onda NE fiskalizira novu fakturu, ako ga ima onda fiskalizira
            string[] iznosi = InsertFaktura(staraFaktura, id_kasa, id_ducan, sifraPartnera);

            string novaFaktura = iznosi[0].ToString();
            string oduzmi = iznosi[1].ToString();
            string nacinPlacanja = iznosi[2].ToString();
            string zki = iznosi[3].ToString();

            DataTable DSfakturaStavke = classSQL.select("SELECT * FROM faktura_stavke where broj_fakture='" + staraFaktura + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "faktura_stavke").Tables[0];

            SrediStavkeRobuSkladiste(ref DSfakturaStavke, staraFaktura, novaFaktura, oduzmi);

            //Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), null, "Faktura", staraFaktura, true);

            DataTable DT = classSQL.select("SELECT date, ukupno FROM fakture where broj_fakture='" + novaFaktura + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "fakture").Tables[0];

            zki = "";
            if (DT.Rows.Count > 0)
            {
                if (zki != "")
                {
                    FiskalizirajFakturu(DSfakturaStavke, Convert.ToDateTime(DT.Rows[0][0].ToString()), Convert.ToInt32(novaFaktura), DT.Rows[0][1].ToString(), nacinPlacanja);
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES" +
                        "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'Storno fakture broj " + staraFaktura + " (fiskalizirana), nova faktura broj: " + novaFaktura + " (fiskalizirana)')"));
                }
                else
                {
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES" +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'Storno fakture broj " + staraFaktura + " (nije fiskalizirana), nova faktura broj: " + novaFaktura + " (nije fiskalizirana)')"));
                }
            }
            else
            {
                MessageBox.Show("Greška kod fiskalizacije storna fakture!", "Greška!");
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES" +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'Storno fakture broj " + staraFaktura + " (fiskalizirana), nova faktura broj: " + novaFaktura + " (nije fiskalizirana)')"));
            }
        }

        private void FiskalizirajFakturu(DataTable DTsend, DateTime datum, int brojFakture, string ukupno, string nacinPlacanja)
        {
            DataSet DSkolicina = new DataSet();
            DataRow RowPdv;

            dodajKoloneDTpdv();
            DTpdv.Clear();

            double mnozeno = 1;
            double osnovicaStavka, pdvStavka;
            double osnovicaSve = 0;
            double pdvSve = 0;
            double povratnaNaknadaSve = 0;
            double rabatSve = 0;
            double kolNaknada;
            double povratnaNaknada;
            DataTable DTtemp;
            string sifra = "";
            string kol = "";

            for (int i = 0; i < DTsend.Rows.Count; i++)
            {
                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
                double kolicina = Convert.ToDouble(DTsend.Rows[i]["kolicina"].ToString());
                mnozeno = kolicina >= 0 ? 1 : -1;
                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                double PDV = Convert.ToDouble(DTsend.Rows[i]["porez"].ToString());
                double VPC = Convert.ToDouble(DTsend.Rows[i]["vpc"].ToString());
                double rabat = Convert.ToDouble(DTsend.Rows[i]["rabat"].ToString());
                povratnaNaknada = Convert.ToDouble(DTsend.Rows[i]["povratna_naknada"].ToString());

                //double povratnaNaknada;
                ////mora biti tak jer prije nije postojala povratna naknada!
                //try
                //{
                //    povratnaNaknada = Convert.ToDouble(row["povratna_naknada"].ToString());
                //}
                //catch
                //{
                //    povratnaNaknada = 0;
                //}

                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                double cijena = Math.Round(VPC * PDV / 100 + VPC - 0.0000001, 2);
                //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
                double mpc = cijena * kolicina * (1 - rabat / 100);
                mpc = Convert.ToDouble(mpc.ToString("#0.00"));

                rabatSve += cijena * kolicina - mpc;

                //Ovaj kod dobiva PDV
                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

                //Ovaj kod dobiva porez na potrošnju
                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

                //treba smanjiti za iznos povratne naknade

                povratnaNaknada *= mnozeno;
                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100);
                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

                dodajPDV(PDV, osnovicaStavka);

                osnovicaSve += osnovicaStavka;

                pdvSve += pdvStavka;

                povratnaNaknadaSve += povratnaNaknada;
            }

            //--------------------------------------------------------
            //fiskalizacija

            DataTable DTOstaliPor = new DataTable();
            DTOstaliPor.Columns.Add("naziv");
            DTOstaliPor.Columns.Add("stopa");
            DTOstaliPor.Columns.Add("osnovica");
            DTOstaliPor.Columns.Add("iznos");

            DataTable DTnaknade = new DataTable();
            DTnaknade.Columns.Add("naziv");
            DTnaknade.Columns.Add("iznos");

            if (povratnaNaknadaSve != 0)
            {
                RowPdv = DTnaknade.NewRow();
                RowPdv["iznos"] = povratnaNaknadaSve.ToString("0.00");
                RowPdv["naziv"] = "Povratna naknada";
                DTnaknade.Rows.Add(RowPdv);
            }

            double porezPotrosnjaSve = 0;

            string[] porezNaPotrosnju = setPorezNaPotrosnju();
            porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
            porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
            porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

            string iznososlobpdv = "";
            string iznos_marza = "";

            bool pdv = false;
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
            {
                pdv = true;
            }

            string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";

            string[] fiskalizacija = new string[3];

            //try
            //{
            //    fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(DTtvrtka.Rows[0]["oib"].ToString(),
            //        oib,
            //        null,
            //        datum,
            //        pdv,
            //        brojFakture,
            //        "",//poslovnica
            //        2, //naplatni_uredaj
            //        DTpdv,
            //        porezNaPotrosnju,
            //        DTOstaliPor,
            //        iznososlobpdv,
            //        iznos_marza,
            //        DTnaknade,
            //        Convert.ToDecimal(ukupno),
            //        nacinPlacanja,
            //        false,
            //        osnovicaSve
            //        );
            //}
            //catch
            //{
            //    fiskalizacija = new string[3];
            //    fiskalizacija[0] = "";
            //    fiskalizacija[1] = "";
            //    fiskalizacija[2] = "";
            //}

            //ažuriraj fakturu sa zki i jir
            string sql = "UPDATE fakture SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
                " WHERE broj_fakture='" + brojFakture + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'";
            //provjera_sql(classSQL.update(sql));
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        #region fiskalizacija helper

        /// <summary>
        /// Dodaje kolone tablici DTpdv ako još nisu dodane
        /// </summary>
        private static void dodajKoloneDTpdv()
        {
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("iznos");
            }
        }

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(double stopa, double iznos)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString() + "'");
            DataRow RowPdv;

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Math.Round(stopa, 2).ToString("#0.00");
                RowPdv["iznos"] = Math.Round(iznos * stopa / 100, 2).ToString("#0.00");
                RowPdv["osnovica"] = iznos.ToString("#0.00");
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = (Math.Round(Convert.ToDouble(dataROW[0]["iznos"].ToString()), 2) + Math.Round(iznos * stopa / 100, 2)).ToString("#0.00");
                dataROW[0]["osnovica"] = (Math.Round(Convert.ToDouble(dataROW[0]["osnovica"].ToString()), 2) + Math.Round(iznos, 2)).ToString("#0.00");
            }
        }

        /// <summary>
        /// postavlja porez_na_potrosnju na empty string
        /// </summary>
        /// <returns></returns>
        private static string[] setPorezNaPotrosnju()
        {
            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "";
            porez_na_potrosnju[1] = "";
            porez_na_potrosnju[2] = "";

            return porez_na_potrosnju;
        }

        #endregion fiskalizacija helper

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmFakturaStorno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmFakturaStorno_Load(object sender, EventArgs e)
        {
            numeric();
            string sql = "SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik = '" +
                Properties.Settings.Default.id_zaposlenik + "'";
            blagajnik_ime = classSQL.select(sql, "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }
    }
}