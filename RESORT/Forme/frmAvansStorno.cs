using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RESORT
{
    public partial class frmAvansStorno : Form
    {
        public frmAvansStorno()
        {
            InitializeComponent();
        }

        private INIFile ini = new INIFile();
        private static DataTable DTtvrtka = classDBlite.LiteSelect("SELECT * FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];

        private static DataTable DToib = RemoteDB.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private static DataTable DTpdv = new DataTable();

        private string brojAvans;

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable DT = RemoteDB.select("SELECT broj_avansa FROM avansi WHERE broj_avansa='" + textBox1.Text + "'", "avansi").Tables[0];

            if (DT.Rows.Count != 0)
            {
                try
                {
                    long broj = Convert.ToInt64(textBox1.Text);
                    brojAvans = broj.ToString();

                    stornirajAvansHelper(brojAvans);

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Krivi broj avansa.", "Greška");
                }
            }
            else
            {
                MessageBox.Show("Krivi broj avansa.", "Greška");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string br = brojAvansa();
            if (br != "")
            {
                stornirajAvansHelper(br);
            }
            else
            {
                MessageBox.Show("U bazi ne postoji niti jedan avans!");
            }
        }

        private string brojAvansa()
        {
            DataTable DSbr = RemoteDB.select("SELECT MAX(CAST(broj_avansa AS integer)) FROM avansi", "avansi").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                return DSbr.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private void stornirajAvansHelper(string broj)
        {
            DataTable DSracun = RemoteDB.select("SELECT storno FROM avansi where broj_avansa='" + broj + "'", "avansi").Tables[0];

            if (DSracun.Rows[0]["storno"].ToString() == "DA")
            {
                MessageBox.Show("Avans je već storniran!");
                this.ActiveControl = textBox1;
                textBox1.SelectAll();
            }
            else
            {
                stornirajAvans(broj);
                MessageBox.Show("Izvršeno.");
                this.Close();
            }
        }

        private void stornirajAvans(string stariBroj)
        {
            //ubaci u racuni i vrati IznosGotovina, IznosKartica, ukupno i novi racun
            DataTable DTracun = RemoteDB.select("SELECT * FROM avansi where broj_avansa='" + stariBroj + "'", "avansi").Tables[0];

            string noviAvans = brojAvansa();
            if (noviAvans != "")
            {
                noviAvans = (Convert.ToInt64(noviAvans) + 1).ToString();
            }
            else
            {
                MessageBox.Show("U bazi ne postoji niti jedan avans!");
                return;
            }

            //priprema za fiskalizaciju
            DataTable DTnaknade = new DataTable();
            DataTable DTOstaliPor = new DataTable();
            bool pdv = ini.Read("Postavke", "u_sustavu_pdva") == "1" ? true : false;
            string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";
            string iznososlobpdv = "";
            string iznos_marza = "";

            double Porez_potrosnja_sve = 0;

            string[] porezNaPotrosnju = setPorezNaPotrosnju();

            dodajKoloneDTpdv();
            DTpdv.Clear();

            //priprema za fiskalizaciju
            double osnovica_sve = 0;
            double osnovicaVar = Convert.ToDouble(DTracun.Rows[0]["osnovica_var"].ToString());
            double porezVar = Convert.ToDouble(DTracun.Rows[0]["porez_var"].ToString());
            double osnovica10 = Convert.ToDouble(DTracun.Rows[0]["osnovica10"].ToString());
            double porez10 = osnovica10 * .1;
            //dodajPDV(porez10, osnovica10);
            //dodajPDV(porezVar, osnovicaVar);

            if (osnovica10 != 0)
                dodajPDV(10.00, Math.Round(osnovica10 * -1, 2));
            if (osnovicaVar != 0)
                dodajPDV(Math.Round(porezVar / osnovicaVar, 4) * 100, Math.Round(osnovicaVar * -1, 2));

            osnovica_sve += osnovica10 + osnovicaVar;

            porezNaPotrosnju[0] = "0";
            porezNaPotrosnju[1] = Convert.ToString(osnovica_sve);
            porezNaPotrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

            DataTable DTnp = RemoteDB.select("SELECT naziv_placanja FROM nacin_placanja where id_placanje='" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'", "avansi").Tables[0];

            string npId = DTnp.Rows.Count > 0 ? DTnp.Rows[0][0].ToString() : "";
            string np;

            switch (npId.ToLower())
            {
                case "gotovina":
                    np = "G";
                    break;

                case "kartica":
                    np = "K";
                    break;

                case "virman":
                    np = "T";
                    break;

                default:
                    np = "O";
                    break;
            }

            DateTime datum = DateTime.Now;

            //string[] fiskalizacija = new string[3];
            string[] fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                DTtvrtka.Rows[0]["oib"].ToString(),
                oib,
                datum,
                Convert.ToInt32(noviAvans),
                DTpdv,
                porezNaPotrosnju,
                DTOstaliPor,
                iznososlobpdv,
                iznos_marza,
                DTnaknade,
                Convert.ToDecimal(DTracun.Rows[0]["ukupno"].ToString()) * -1,//.ToString().Replace(",", ".")
                np,
                false,
                "A"
                );

            string sql = "INSERT INTO avansi (broj_avansa,dat_dok,dat_knj,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,id_valuta,opis,id_vd,godina_avansa,ukupno,ziro,nult_stp,neoporezivo," +
                "osnovica10,osnovica_var,porez_var,id_pdv,id_partner,jir,zki,storno) VALUES " +
                " (" +
                 " '" + noviAvans + "'," +
                " '" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + DTracun.Rows[0]["dat_knj"].ToString() + "'," +
                " '" + "0" + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + DTracun.Rows[0]["model"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_valuta"].ToString() + "'," +
                " '" + DTracun.Rows[0]["opis"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_vd"].ToString() + "'," +
                " '" + DTracun.Rows[0]["godina_avansa"].ToString() + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["ukupno"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + DTracun.Rows[0]["ziro"].ToString() + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["nult_stp"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["neoporezivo"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["osnovica10"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["osnovica_var"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + (Convert.ToDecimal(DTracun.Rows[0]["porez_var"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                " '" + DTracun.Rows[0]["id_pdv"].ToString() + "', " +
                " '" + DTracun.Rows[0]["id_partner"].ToString() + "', " +
                " '" + fiskalizacija[0] + "', " +
                " '" + fiskalizacija[1] + "', " +
                " 'NE' " +
                ")";

            provjera_sql(RemoteDB.insert(sql));

            string sqlStorno = "UPDATE avansi SET storno='DA' WHERE broj_avansa='" + stariBroj + "'";
            RemoteDB.update(sqlStorno);

            sql = "INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'Storno avansa br." + stariBroj + "')";
            provjera_sql(RemoteDB.insert(sql));
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
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(stopa).ToString() + "'");
            DataRow RowPdv;

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(stopa).ToString();
                RowPdv["iznos"] = (iznos * stopa / 100).ToString();
                RowPdv["osnovica"] = iznos.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznos * stopa / 100;
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + iznos;
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

        private void frmAvansStorno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}