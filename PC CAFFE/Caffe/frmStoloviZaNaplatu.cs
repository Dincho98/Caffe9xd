using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmStoloviZaNaplatu : Form
    {
        public frmStoloviZaNaplatu()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string kartica_kupca { get; set; }
        public string _odabraniStol;
        public string broj_narudzbe;
        private DataTable DTpostavkePrinter;
        private DataTable DTpostavke = new DataTable();
        public string blagajnik_ime = "";

        public frmCaffe FRMcaffe { get; set; }

        private void frmStoloviZaNaplatu_Load(object sender, EventArgs e)
        {
            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            SetStolovi();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void SetStolovi()
        {
            string sql = @"SELECT
stolovi.naziv AS stol_naziv,
SUM(na_stol.mpc * na_stol.kom) AS ukupno,
na_stol.id_stol
FROM na_stol
LEFT JOIN stolovi ON stolovi.id_stol = na_stol.id_stol
GROUP BY na_stol.id_stol, stolovi.naziv, stolovi.id_stol
ORDER BY stolovi.id_stol;";

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];

            flpStolovi.Controls.Clear();
            dgw.Rows.Clear();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Button btn = CreateButtonTable(DT.Rows[i]["stol_naziv"].ToString(), DT.Rows[i]["ukupno"].ToString(), DT.Rows[i]["id_stol"].ToString(), i);
                flpStolovi.Controls.Add(btn);

                int id = 0;
                int.TryParse(DT.Rows[i]["id_stol"].ToString(), out id);

                if ((_odabraniStol != null) ? id == (Convert.ToInt32(_odabraniStol)) : (i == 0))
                {
                    btn.PerformClick();
                }
            }
        }

        private Button CreateButtonTable(string naziv_stola, string ukupno, string id_stol, int i)
        {
            Button button1 = new Button();
            button1.Anchor = (((AnchorStyles.Top | AnchorStyles.Right)));
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Image.FromFile("Slike/stol.png");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.LightSlateGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.CheckedBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ImageAlign = ContentAlignment.TopLeft;
            button1.Location = new Point(904, 84);
            button1.Name = id_stol;
            button1.Text = naziv_stola + "\r\n" + Convert.ToDouble(ukupno).ToString("#0.00") + " kn";
            button1.Size = new Size(200, 110);
            button1.TabIndex = i;
            button1.UseVisualStyleBackColor = false;
            button1.Click += new EventHandler(btnTable_Click);
            button1.Font = new Font("Algerian", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            button1.KeyDown += new KeyEventHandler(AllKeyDown);
            return button1;
        }

        private void SetArtDgw(string id_stol)
        {
            string sql = string.Format(@"SELECT racun_stavke.sifra_robe, racun_stavke.kolicina, roba.naziv, racuni.broj_racuna, racun_stavke.mpc
FROM racuni
LEFT JOIN racun_stavke ON racun_stavke.broj_racuna = racuni.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_blagajna
LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe
WHERE racuni.ukupno_gotovina = '0' AND racuni.ukupno_kartice = '0' AND id_stol = '{0}';", id_stol);

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];
            dgw.Rows.Clear();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgw.Rows.Add(DT.Rows[i]["sifra_robe"].ToString(), DT.Rows[i]["naziv"].ToString(), DT.Rows[i]["kolicina"].ToString(), DT.Rows[i]["mpc"].ToString());
            }
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();
            PaintRows(dgw);

            Control conGrupa = (Control)sender;

            string sql = string.Format(@"SELECT na_stol.sifra,
CASE WHEN na_stol.dod = 2 then napomene.napomena else roba.naziv end as naziv,
na_stol.broj_narudzbe,
CASE WHEN na_stol.dod = 2 then 0 else na_stol.kom end as kom,
na_stol.id_skladiste,
CASE WHEN na_stol.dod = 2 then 0 else na_stol.mpc end as mpc,
CASE WHEN na_stol.dod = 2 then 0 else na_stol.vpc end as vpc,
na_stol.br,
na_stol.jelo,
CASE WHEN na_stol.dod = 2 then 0 else na_stol.porez end as porez,
na_stol.skinuto,
na_stol.id_zaposlenik,
CASE WHEN na_stol.dod = 2 then 0 else na_stol.porez_potrosnja end as porez_potrosnja,
na_stol.dod,
na_stol.pol
FROM na_stol
LEFT JOIN roba ON roba.sifra = na_stol.sifra
LEFT JOIN napomene ON napomene.id::varchar = na_stol.sifra
WHERE id_stol = '{0}'
ORDER BY CAST(broj_narudzbe as BIGINT), CAST(br as BIGINT);",
conGrupa.Name.ToString());

            _odabraniStol = conGrupa.Name.ToString();

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];
            broj_narudzbe = DT.Rows[DT.Rows.Count - 1]["broj_narudzbe"].ToString();

            sql = string.Format(@"select mjesto, ulica, kbr, telefon
from adresa_dostave
where id= (select id_adresa_dostave from na_stol where id_stol = '{0}'
limit 1);", _odabraniStol);

            DataSet dsNarudzba = classSQL.select(sql, "adresa_dostave");

            if (dsNarudzba != null && dsNarudzba.Tables.Count > 0 && dsNarudzba.Tables[0] != null && dsNarudzba.Tables[0].Rows.Count > 0 && dsNarudzba.Tables[0].Rows[0] != null)
            {
                DataRow drDostava = dsNarudzba.Tables[0].Rows[0];
                lblDostava.Text = "Dostava: " + drDostava["ulica"].ToString() + " " + drDostava["kbr"].ToString() + ", " + drDostava["mjesto"].ToString();
            }
            else
            {
                lblDostava.Text = "";
            }

            decimal u = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgw.Rows.Add(DT.Rows[i]["broj_narudzbe"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DT.Rows[i]["kom"].ToString(),
                    Convert.ToDouble(DT.Rows[i]["mpc"].ToString()),
                    "",
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["id_skladiste"].ToString(),
                    DT.Rows[i]["porez"].ToString(),
                    DT.Rows[i]["porez_potrosnja"].ToString(),
                    DT.Rows[i]["vpc"].ToString(),
                    DT.Rows[i]["br"].ToString(),
                    DT.Rows[i]["jelo"].ToString(),
                    DT.Rows[i]["skinuto"].ToString(),
                    DT.Rows[i]["id_zaposlenik"].ToString(),
                    DT.Rows[i]["dod"].ToString(),
                    DT.Rows[i]["pol"].ToString()
                    );

                this.dgw.Rows[i].Cells["chb_naplati"].Value = true;
            }

            PaintRows(dgw);
        }

        private void PaintRows(DataGridView dg)
        {
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 30;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "False")
                    {
                        dgw.Rows[i].Cells["chb_naplati"].Value = true;
                    }
                    else
                    {
                        dgw.Rows[i].Cells["chb_naplati"].Value = false;
                    }
                }
            }
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni  WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string dg(int row, string cell)
        {
            try
            {
                return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void btnNaplati_Click(object sender, EventArgs e)
        {
            #region PROVJERAVA DALI JE DOBRA GODINA

            try
            {
                PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        //if (MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                        //    "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte POWER COMPUTERS.\r\n" +
                        //    "Želite li nastaviti izradu ovog računa?", "Upozorenje.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                        //    startTimerKartica(true, true, true);
                        //    return;
                        //}

                        if (MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\nŽelite prebaciti program u tekuću godinu?", "Upozorenje.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            //PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new PCPOS.Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                            string bazaZaTekucuGodinu = Util.Korisno.prefixBazeKojaSeKoristi() + DateTime.Now.Year.ToString();
                            B.PostaviGodinu_U_XML(bazaZaTekucuGodinu);
                        }
                        //startTimerKartica(true, true, true);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            if (dgw.Rows.Count == 0)
            {
                return;
            }

            if (DTpostavke.Rows[0]["stol_konobar"].ToString() == "1")
            {
                if (dgw.Rows[0].Cells["id_zaposlenik"].FormattedValue.ToString() != Properties.Settings.Default.id_zaposlenik)
                {
                    DataTable DT = classSQL.select("SELECT ime +' '+prezime as IME FROM zaposlenici WHERE id_zaposlenik='" + dgw.Rows[0].Cells["id_zaposlenik"].FormattedValue.ToString() + "'", "zaposlenici").Tables[0];
                    string zap = "";
                    if (DT.Rows.Count > 0)
                    {
                        zap = DT.Rows[0][0].ToString();
                    }

                    MessageBox.Show("Nemate ovlaštenja za naplatu ovog stola.\r\nOvaj stol je započeo " + zap + ".", "Upozorenje.");
                    return;
                }
            }

            string napomena = "";
            if (Class.Postavke.napomena_na_kraju_racuna)
            {
                Kasa.frmNapomenaRacun f = new Kasa.frmNapomenaRacun();
                f.ShowDialog();
                napomena = f.napomena;
            }

            DataTable DTsend = new DataTable();
            string brRac = "";
            string sifraPartnera = "";
            decimal ukupno = 0;

            string kol;
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");

            DTsend.Columns.Add("stol");
            DTsend.Columns.Add("jelo");

            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DTsend.Columns.Add("dod");
            DTsend.Columns.Add("pol");

            DataRow row;

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (dgw.Rows[y].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                {
                    ukupno = (Convert.ToDecimal(dgw.Rows[y].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDecimal(dgw.Rows[y].Cells["kolicina"].FormattedValue.ToString())) + ukupno;
                }
            }

            string uk1 = ukupno.ToString();

            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
            }
            else
            {
                uk1 = uk1.Replace(".", ",");
            }

            if (sifraPartnera == "" || sifraPartnera == null)
            {
                sifraPartnera = "0";
            }

            brRac = brojRacuna();

            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "ukupno_gotovina,ukupno_kartice,ukupno,storno,dobiveno_gotovina,id_stol,godina, napomena) " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'," +
                "'" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'0'," +
                "'0'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'0'," +
                "'" + _odabraniStol + "','" + DateTime.Now.Year.ToString() + "'" +
                "'" + napomena.Trim() + "'" +
                ")";

            if (dg(0, "skinuto") == "1")
            {
                DataTable DT = classSQL.select("SELECT datum FROM kucani_predracuni WHERE id_stol='" + _odabraniStol + "' ORDER BY datum DESC LIMIT 1", "kucani_predracuni").Tables[0];
                DataTable DTp = classSQL.select("SELECT datum_ispisa FROM svi_predracuni WHERE id_stol='" + _odabraniStol + "' ORDER BY datum_ispisa DESC LIMIT 1", "kucani_predracuni").Tables[0];
                if (DT.Rows.Count > 0)
                {
                    classSQL.delete("DELETE FROM kucani_predracuni WHERE id_stol='" + _odabraniStol + "' AND datum='" + Convert.ToDateTime(DT.Rows[0][0].ToString()).ToString("yyyy-MM-dd H:mm:ss") + "'");
                    classSQL.delete("DELETE FROM svi_predracuni WHERE id_stol='" + _odabraniStol + "' AND datum_ispisa='" + Convert.ToDateTime(DTp.Rows[0][0].ToString()).ToString("yyyy-MM-dd H:mm:ss") + "'");
                }
            }

            //zapisivanje bonusa na karticu kupca
            if (Util.Korisno.kartica_kupca && kartica_kupca != null && kartica_kupca.Length > 0)
            {
                string poslovnica = classSQL.select("select ime_ducana from ducan where id_ducan = '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'", "ducan").Tables[0].Rows[0]["ime_ducana"].ToString();
                string naplatni_uredaj = classSQL.select("select ime_blagajne from blagajna where id_ducan = '" + DTpostavke.Rows[0]["default_ducan"].ToString() + "' and id_blagajna = '" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'", "blagajna").Tables[0].Rows[0]["ime_blagajne"].ToString();
                frmDodajRacunKartica.zapisiKarticaKupciRacuni(poslovnica, naplatni_uredaj, Convert.ToInt32(brRac), Convert.ToDateTime(dt), ukupno, kartica_kupca);
            }

            provjera_sql(classSQL.insert(sql));

            int id_adresa_dostave = 0;

            int.TryParse(classSQL.select("select coalesce(id_adresa_dostave, 0) as id_adresa_dostave from na_stol where id_stol = '" + _odabraniStol + "' limit 1", "na_stol").Tables[0].Rows[0]["id_adresa_dostave"].ToString(), out id_adresa_dostave);

            string sifra = "";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                {
                    if (dg(i, "skinuto") != "1")
                    {
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                    }

                    sifra = dg(i, "sifra");
                    DataTable DTroba = Global.Database.GetRoba(sifra);
                    string cijena = DTroba?.Rows.Count > 0 ? DTroba.Rows[0]["mpc"].ToString().Replace('.', ',') : dg(i, "cijena");

                    row = DTsend.NewRow();
                    row["broj_racuna"] = brRac;
                    row["sifra_robe"] = sifra;
                    row["id_skladiste"] = dg(i, "id_skladiste");
                    row["mpc"] = cijena;
                    row["porez"] = dg(i, "porez");
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = dg(i, "vpc");
                    row["cijena"] = dg(i, "cijena");
                    row["rabat"] = "0";
                    row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                    row["ime"] = dg(i, "naziv");
                    row["id_ducan"] = DTpostavke.Rows[0]["default_ducan"].ToString();
                    row["id_blagajna"] = DTpostavke.Rows[0]["default_blagajna"].ToString();
                    row["dod"] = dg(i, "dod");
                    row["pol"] = dg(i, "polapola");
                    DTsend.Rows.Add(row);

                    provjera_sql(classSQL.delete("DELETE FROM na_stol WHERE id_stol='" + _odabraniStol + "' AND broj_narudzbe='" + dg(i, "runda") + "' AND sifra='" + sifra + "' and dod = '" + dg(i, "dod") + "'"));

                    if (sifra.Length > 4)
                    {
                        if (sifra.Substring(0, 3) == "000")
                        {
                            string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "' AND dokumenat='RN'";
                            classSQL.update(sqlnext);
                        }
                    }
                }
            }

            Caffe.frmNaplata naplata = new Caffe.frmNaplata();
            naplata.racun_ili_stol = brRac;
            naplata.ShowDialog();
            string nacin_placanja = naplata.nacin_placanja;

            if (nacin_placanja == "G")
                Util.Korisno.dodajIznosUBlagajnickiIzvjestaj(Convert.ToInt32(brRac), dRac, Convert.ToDecimal(ukupno));

            try
            {
                PosPrint.classPosPrintCaffe.id_adresa_dostave = id_adresa_dostave;
                PosPrint.classPosPrintCaffe.PrintReceipt(DTsend, "", brRac + "/" + DateTime.Now.Year.ToString(), sifraPartnera, "", brRac, nacin_placanja, "", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
            }
            finally
            {
                if (DTpostavkePrinter.Rows[0]["windows_printer_name2"].ToString() != "Nije instaliran")
                {
                    //PosPrint.classPosPrintKuhinja.PrintReceipt(DTsend);
                }
            }

            provjera_sql(SQL.SQLracun.InsertStavke(DTsend));
            provjera_sql(SQL.SQLracun.InsertNapomene(DTsend));

            FRMcaffe.PrikazZadnjegRacuna();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("belveder"))
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati cijeli stol?", "Brisanje sa cijelog stola!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "False")
                        {
                            string sql = "DELETE FROM na_stol WHERE broj_narudzbe='" + dg(i, "runda") + "' AND id_poslovnica='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "' AND sifra='" + dg(i, "sifra") + "' AND id_stol='" + _odabraniStol + "' AND br='" + dg(i, "br") + "'";
                            classSQL.delete(sql);
                        }
                    }

                    dgw.Rows.Clear();
                    PaintRows(dgw);
                }
            }
            else
            {
                Caffe.Dodaci.frmVracaNekuVrijednost v = new Dodaci.frmVracaNekuVrijednost();
                Properties.Settings.Default.privremena_vrijednost = "";
                v.txtBroj.PasswordChar = '*';
                v._title = "Unesite ključ:";
                v.ShowDialog();

                string key = File.ReadAllText("belveder");
                if (Properties.Settings.Default.privremena_vrijednost == key)
                {
                    if (MessageBox.Show("Dali ste sigurni da želite obrisati cijeli stol?", "Brisanje sa cijelog stola!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dgw.Rows.Count; i++)
                        {
                            if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "False")
                            {
                                string sql = "DELETE FROM na_stol WHERE broj_narudzbe='" + dg(i, "runda") + "' AND id_poslovnica='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "' AND sifra='" + dg(i, "sifra") + "' AND id_stol='" + _odabraniStol + "' AND br='" + dg(i, "br") + "'";
                                classSQL.delete(sql);
                            }
                        }

                        dgw.Rows.Clear();
                        PaintRows(dgw);
                    }
                }
                else
                {
                    MessageBox.Show("Krivi unos!");
                }
            }

            Control conGrupa = (Control)sender;
            dgw.Rows.Clear();
            PaintRows(dgw);

            string sql1 = "SELECT " +
               " na_stol.sifra," +
               " roba.naziv," +
               " na_stol.broj_narudzbe," +
               " na_stol.kom," +
               " na_stol.id_skladiste," +
               " na_stol.mpc," +
               " na_stol.vpc," +
               " na_stol.br," +
               " na_stol.jelo," +
               " na_stol.porez," +
               " na_stol.porez_potrosnja, " +
               " na_stol.dod, " +
               " na_stol.pol " +
               " FROM na_stol" +
               " LEFT JOIN roba ON roba.sifra=na_stol.sifra " +
               " WHERE id_stol='" + _odabraniStol + "'";
            //_odabraniStol = conGrupa.Name.ToString();

            DataTable DT = classSQL.select(sql1, "racuni").Tables[0];

            decimal u = 0;
            for (int ii = 0; ii < DT.Rows.Count; ii++)
            {
                dgw.Rows.Add(DT.Rows[ii]["broj_narudzbe"].ToString(),
                    DT.Rows[ii]["naziv"].ToString(),
                    DT.Rows[ii]["kom"].ToString(),
                    Convert.ToDouble(DT.Rows[ii]["mpc"].ToString()),
                    "",
                    DT.Rows[ii]["sifra"].ToString(),
                    DT.Rows[ii]["id_skladiste"].ToString(),
                    DT.Rows[ii]["porez"].ToString(),
                    DT.Rows[ii]["porez_potrosnja"].ToString(),
                    DT.Rows[ii]["vpc"].ToString(),
                    DT.Rows[ii]["br"].ToString(),
                    DT.Rows[ii]["dod"].ToString(),
                    DT.Rows[ii]["pol"].ToString());

                this.dgw.Rows[ii].Cells["chb_naplati"].Value = true;
            }

            PaintRows(dgw);
        }

        private void AllKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                btnNaplati.PerformClick();
            }

            if (e.KeyData == Keys.F2)
            {
                btnPredRacun.PerformClick();
            }

            if (e.KeyData == Keys.F3)
            {
                btnObrisi.PerformClick();
            }
            if (e.KeyData == Keys.F4)
            {
                btnPosaljiUKuhinju.PerformClick();
            }

            if (e.KeyData == Keys.Escape)
            {
                btnESC.PerformClick();
            }

            if (e.KeyData == Keys.Delete)
            {
                btnDellAll.PerformClick();
            }

            if (e.KeyData == Keys.F5)
            {
                btnIspisNarudzbe.PerformClick();
            }
        }

        private void btnPredRacun_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
            {
                return;
            }

            string napomena = "";
            if (Class.Postavke.napomena_na_kraju_predracuna)
            {
                Kasa.frmNapomenaRacun f = new Kasa.frmNapomenaRacun();
                f.ShowDialog(this);
                napomena = f.napomena;
            }

            DataTable DTsend = new DataTable();
            string brRac = "";
            string sifraPartnera = "";
            decimal ukupno = 0;

            string kol;
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DTsend.Columns.Add("napomena");
            DataRow row;

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                {
                    if (dgw.Rows[y].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                    {
                        ukupno = Convert.ToDecimal(dgw.Rows[y].Cells["cijena"].FormattedValue.ToString()) + ukupno;
                    }
                }
            }

            string uk1 = ukupno.ToString();

            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
            }
            else
            {
                uk1 = uk1.Replace(".", ",");
            }

            if (sifraPartnera == "" || sifraPartnera == null)
            {
                sifraPartnera = "0";
            }

            string sifra = "";
            int broj = 1;
            try
            {
                DataTable DT = classSQL.select("SELECT MAX(broj) FROM svi_predracuni", "svi_predracuni").Tables[0];
                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "") { broj = Convert.ToInt16(DT.Rows[0][0].ToString()) + 1; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgw.Rows[i].Cells["dod"].Value) != 2)
                {
                    if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                    {
                        if (DTpostavke.Rows[0]["skidaj_sa_predracuna"].ToString() == "1" && dg(i, "skinuto") != "1")
                        {
                            try
                            {
                                kol = SQL.ClassSkladiste.GetAmountPredracun(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                            dgw.Rows[i].Cells["skinuto"].Value = "1";

                            classSQL.update("UPDATE na_stol SET skinuto='1' WHERE id_stol='" + _odabraniStol + "' AND br='" + dg(i, "br") + "'");
                            kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                            SQL.ClassSkladiste.ZaVaranje(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), _odabraniStol, "+");
                        }

                        sifra = dg(i, "sifra");
                        row = DTsend.NewRow();
                        row["broj_racuna"] = brRac;
                        row["sifra_robe"] = sifra;
                        row["id_skladiste"] = dg(i, "id_skladiste");
                        row["mpc"] = dg(i, "cijena");
                        row["porez"] = dg(i, "porez");
                        row["kolicina"] = dg(i, "kolicina");
                        row["vpc"] = dg(i, "vpc");
                        row["cijena"] = dg(i, "cijena");
                        row["rabat"] = "0";
                        row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                        row["ime"] = dg(i, "naziv");
                        row["id_ducan"] = DTpostavke.Rows[0]["default_ducan"].ToString();
                        row["id_blagajna"] = DTpostavke.Rows[0]["default_blagajna"].ToString();
                        row["napomena"] = napomena;
                        DTsend.Rows.Add(row);

                        try
                        {
                            string lqs = "INSERT INTO svi_predracuni (broj,sifra,mpc,porez,id_stol,id_zaposlenik,datum_ispisa,kolicina,vpc,porez_potrosnja,naziv,id_ducan,id_blagajna, napomena) VALUES (" +
                                "'" + broj + "'," +
                                "'" + sifra + "'," +
                                "'" + dg(i, "cijena").Replace(",", ".") + "'," +
                                "'" + dg(i, "porez").Replace(",", ".") + "'," +
                                "'" + _odabraniStol + "'," +
                                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                "'" + dg(i, "kolicina").Replace(",", ".") + "'," +
                                "'" + dg(i, "vpc").Replace(",", ".") + "'," +
                                "'" + dg(i, "porez_potrosnja").Replace(",", ".") + "'," +
                                "'" + dg(i, "naziv") + "'," +
                                "'" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'," +
                                "'" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "', " +
                                "'" + napomena + "'" +
                                ")";

                            classSQL.insert(lqs);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            try
            {
                PosPrint.classPosPrintCaffePredracun.predracun = true;
                PosPrint.classPosPrintCaffePredracun.PrintReceipt(DTsend, "", "Predračun", sifraPartnera, "", "Predračun", "G", _odabraniStol);
                if (DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "1")
                {
                    btnPosaljiUKuhinju.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }

        private void btnPosaljiUKuhinju_Click(object sender, EventArgs e)
        {
            posaljiUKuhinju(false);
        }

        private void posaljiUKuhinju(bool posaljiSve)
        {
            //if (DTpostavkePrinter.Rows[0]["windows_printer_name2"].ToString() != "Nije instaliran" || DTpostavkePrinter.Rows[0]["windows_printer_name3"].ToString() != "Nije instaliran")
            {
                int max = 0;
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (max < int.Parse(dgw.Rows[y].Cells[0].FormattedValue.ToString()))
                    {
                        max = int.Parse(dgw.Rows[y].Cells[0].FormattedValue.ToString());
                    }
                }

                //int temp;
                //var MaxID2 = dgw.Rows.Cast<DataGridViewRow>()
                //            .Max(r => int.TryParse(r.Cells["Id"].Value.ToString(), out temp) ?
                //                       temp : 0);

                DataTable DTsend = new DataTable();
                DTsend = new DataTable();
                DTsend.Columns.Add("broj_racuna");
                DTsend.Columns.Add("sifra_robe");
                DTsend.Columns.Add("id_skladiste");
                DTsend.Columns.Add("mpc");
                DTsend.Columns.Add("vpc");
                DTsend.Columns.Add("nbc");
                DTsend.Columns.Add("porez");
                DTsend.Columns.Add("kolicina");
                DTsend.Columns.Add("rabat");
                DTsend.Columns.Add("cijena");
                DTsend.Columns.Add("stol");
                DTsend.Columns.Add("ime");
                DTsend.Columns.Add("porez_potrosnja");
                DTsend.Columns.Add("storno");
                DTsend.Columns.Add("id_ducan");
                DTsend.Columns.Add("id_blagajna");
                DTsend.Columns.Add("jelo");
                DTsend.Columns.Add("dod");
                DTsend.Columns.Add("pol");
                DataRow row;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "True" || posaljiSve)
                    {
                        if (max == int.Parse(dgw.Rows[i].Cells[0].FormattedValue.ToString()) || DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "0" || posaljiSve)
                        {
                            row = DTsend.NewRow();
                            row["broj_racuna"] = _odabraniStol;
                            row["sifra_robe"] = dg(i, "sifra");
                            row["id_skladiste"] = dg(i, "id_skladiste");
                            row["mpc"] = dg(i, "cijena");
                            row["porez"] = dg(i, "porez");
                            row["kolicina"] = dg(i, "kolicina");
                            row["vpc"] = dg(i, "vpc");
                            row["stol"] = _odabraniStol;
                            row["cijena"] = dg(i, "cijena");
                            row["rabat"] = "0";
                            row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                            row["ime"] = dg(i, "naziv");
                            row["id_ducan"] = DTpostavke.Rows[0]["default_ducan"].ToString();
                            row["id_blagajna"] = DTpostavke.Rows[0]["default_blagajna"].ToString();
                            row["jelo"] = dg(i, "jelo");
                            row["dod"] = dg(i, "dod");
                            row["pol"] = dg(i, "polapola");
                            DTsend.Rows.Add(row);
                        }
                    }
                }

                string printer1Naziv = DTpostavkePrinter.Rows[0]["windows_printer_name"].ToString();
                string printer2Naziv = DTpostavkePrinter.Rows[0]["windows_printer_name2"].ToString();
                string printer3Naziv = DTpostavkePrinter.Rows[0]["windows_printer_name3"].ToString();

                if (printer1Naziv != "Nije instaliran" && printer1Naziv != "")
                {
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe;
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter1(DTsend);
                }

                if (printer2Naziv != "Nije instaliran" && printer2Naziv != "")
                {
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe;
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter2(DTsend);
                }

                if (printer3Naziv != "Nije instaliran" && printer3Naziv != "")
                {
                    PosPrint.classPosPrintKuhinja.broj_narudzbe = broj_narudzbe;
                    PosPrint.classPosPrintKuhinja.PrintOnPrinter3(DTsend);
                }
            }
        }

        private void btnDellAll_Click(object sender, EventArgs e)
        {
            if (!File.Exists("belveder"))
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati cijeli stol?", "Brisanje sa cijelog stola!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    classSQL.delete("DELETE FROM na_stol WHERE id_stol='" + _odabraniStol + "'");
                    SetStolovi();
                }
            }
            else
            {
                Caffe.Dodaci.frmVracaNekuVrijednost v = new Dodaci.frmVracaNekuVrijednost();
                Properties.Settings.Default.privremena_vrijednost = "";
                v.txtBroj.PasswordChar = '*';
                v._title = "Unesite ključ:";
                v.ShowDialog();

                string key = File.ReadAllText("belveder");
                if (Properties.Settings.Default.privremena_vrijednost == key)
                {
                    if (MessageBox.Show("Dali ste sigurni da želite obrisati cijeli stol?", "Brisanje sa cijelog stola!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        classSQL.delete("DELETE FROM na_stol WHERE id_stol='" + _odabraniStol + "'");
                        SetStolovi();
                    }
                }
                else
                {
                    MessageBox.Show("Krivi unos!");
                }
            }
        }

        private void btnIspisNarudzbe_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
            {
                return;
            }

            int max = 0;
            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (max < int.Parse(dgw.Rows[y].Cells[0].FormattedValue.ToString()))
                {
                    max = int.Parse(dgw.Rows[y].Cells[0].FormattedValue.ToString());
                }
            }

            DataTable DTsend = new DataTable();
            string brRac = "";
            string sifraPartnera = "";
            decimal ukupno = 0;

            string kol;
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DataRow row;

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (max == int.Parse(dgw.Rows[y].Cells[0].FormattedValue.ToString()))
                {
                    if (dgw.Rows[y].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                    {
                        ukupno = Convert.ToDecimal(dgw.Rows[y].Cells["cijena"].FormattedValue.ToString()) + ukupno;
                    }
                }
            }

            string uk1 = ukupno.ToString();

            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
            }
            else
            {
                uk1 = uk1.Replace(".", ",");
            }

            if (sifraPartnera == "" || sifraPartnera == null)
            {
                sifraPartnera = "0";
            }

            string sifra = "";
            int broj = 1;
            try
            {
                DataTable DT = classSQL.select("SELECT MAX(broj) FROM svi_predracuni", "svi_predracuni").Tables[0];
                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "") { broj = Convert.ToInt16(DT.Rows[0][0].ToString()) + 1; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                // || DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "0"
                if (max == int.Parse(dgw.Rows[i].Cells[0].FormattedValue.ToString()))
                {
                    if (dgw.Rows[i].Cells["chb_naplati"].FormattedValue.ToString() == "True")
                    {
                        if (DTpostavke.Rows[0]["skidaj_sa_predracuna"].ToString() == "1")
                        {
                            try
                            {
                                SQL.ClassSkladiste.SetBrojcanik(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "+");

                                {
                                    kol = SQL.ClassSkladiste.GetAmountPredracun(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                                }
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                            kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTpostavke.Rows[0]["default_skladiste"].ToString(), dg(i, "kolicina"), "-");
                        }

                        sifra = dg(i, "sifra");
                        row = DTsend.NewRow();
                        row["broj_racuna"] = brRac;
                        row["sifra_robe"] = sifra;
                        row["id_skladiste"] = dg(i, "id_skladiste");
                        row["mpc"] = dg(i, "cijena");
                        row["porez"] = dg(i, "porez");
                        row["kolicina"] = dg(i, "kolicina");
                        row["vpc"] = dg(i, "vpc");
                        row["cijena"] = dg(i, "cijena");
                        row["rabat"] = "0";
                        row["porez_potrosnja"] = dg(i, "porez_potrosnja");
                        row["ime"] = dg(i, "naziv");
                        row["id_ducan"] = DTpostavke.Rows[0]["default_ducan"].ToString();
                        row["id_blagajna"] = DTpostavke.Rows[0]["default_blagajna"].ToString();
                        DTsend.Rows.Add(row);

                        try
                        {
                            string lqs = "INSERT INTO svi_predracuni (broj,sifra,mpc,porez,id_stol,id_zaposlenik,datum_ispisa,kolicina,vpc,porez_potrosnja,naziv,id_ducan,id_blagajna) VALUES (" +
                                "'" + broj + "'," +
                                "'" + sifra + "'," +
                                "'" + dg(i, "cijena").Replace(",", ".") + "'," +
                                "'" + dg(i, "porez").Replace(",", ".") + "'," +
                                "'" + _odabraniStol + "'," +
                                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                "'" + dg(i, "kolicina").Replace(",", ".") + "'," +
                                "'" + dg(i, "vpc").Replace(",", ".") + "'," +
                                "'" + dg(i, "porez_potrosnja").Replace(",", ".") + "'," +
                                "'" + dg(i, "naziv") + "'," +
                                "'" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'," +
                                "'" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'" +
                                ")";

                            classSQL.insert(lqs);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            try
            {
                PosPrint.classPosPrintCaffePredracun.predracun = false;
                if (DTsend.Rows.Count > 0)
                {
                    PosPrint.classPosPrintCaffePredracun.PrintReceipt(DTsend, "", "Predračun", sifraPartnera, "", "Predračun", "G", _odabraniStol);
                }

                //if (DTpostavke.Rows[0]["bool_direct_print_kuhinja"].ToString() == "1")
                {
                    btnPosaljiUKuhinju.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška sa printerom.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }

        private void btnDodajPopust_Click(object sender, EventArgs e)
        {
            Caffe.frmDodatniPopustStolovi d = new frmDodatniPopustStolovi();
            d.frm = this;
            d.ShowDialog();
        }

        private void btnAdresa_Click(object sender, EventArgs e)
        {
            try
            {
                frmNarudzbaAdresa f = new frmNarudzbaAdresa();
                f.broj_narudzbe = Convert.ToInt32(broj_narudzbe);
                f.stol = Convert.ToInt32(_odabraniStol);
                f.id_ducan = Convert.ToInt32(DTpostavke.Rows[0]["default_ducan"]);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSveUKuhinju_Click(object sender, EventArgs e)
        {
            try
            {
                posaljiUKuhinju(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOtpremnica_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgw.Rows.Count > 0)
                {
                    frmMiniOtpremnica f = new frmMiniOtpremnica();
                    f.dgv = dgw;
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        classSQL.delete("DELETE FROM na_stol WHERE id_stol='" + _odabraniStol + "'");
                        SetStolovi();
                        //IscrtajZidove();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}