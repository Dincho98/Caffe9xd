using PCPOS.PosPrint;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPregledSmjene : Form
    {
        public frmPregledSmjene()
        {
            InitializeComponent();
        }

        public string datumOD { get; set; }
        public string datumDO { get; set; }
        public string id { get; set; }
        private string Sukupno = "";

        private void frmPregledSmjene_Load(object sender, EventArgs e)
        {
            if (datumDO == "")
            {
                datumDO = DateTime.Now.ToString("dd.MM.yyyy H:mm:ss");
            }

            LoadData();
            PrometProdajneRobe(false, false, false);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string blagMinimum = "0";

        private void LoadData()
        {
            string sql = "SELECT * FROM smjene WHERE id='" + id + "' AND smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ";
            DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

            string zap = Properties.Settings.Default.id_zaposlenik;
            if (DT_smjena.Rows[0]["konobarZ"].ToString() == "")
            {
                zap = Properties.Settings.Default.id_zaposlenik;
            }
            else
            {
                zap = DT_smjena.Rows[0]["konobarZ"].ToString();
            }

            DataTable DT_zaposlenik = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik='" + zap + "'", "zaposlenici").Tables[0];
            if (DT_zaposlenik.Rows.Count > 0) { lblSmjenaZavrsenaZaposlenik.Text = "Smjenu završava: " + DT_zaposlenik.Rows[0][0].ToString(); } else { lblSmjenaZavrsenaZaposlenik.Text = ""; }

            DataTable DT_zaposlenik1 = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik='" + DT_smjena.Rows[0]["konobar"].ToString() + "'", "zaposlenici").Tables[0];
            if (DT_zaposlenik.Rows.Count > 0) { lblZaposlenik.Text = "Smjenu započeo: " + DT_zaposlenik1.Rows[0][0].ToString(); } else { lblZaposlenik.Text = ""; }

            lblSmjenaPocela.Text = "Smjena počela: " + Convert.ToDateTime(datumOD).ToString("dd.MM.yyyy H:mm:ss");
            lblSmjenaZavrsena.Text = "Smjena završila: " + Convert.ToDateTime(datumDO).ToString("dd.MM.yyyy H:mm:ss");
            blagMinimum = Convert.ToDecimal(DT_smjena.Rows[0]["pocetno_stanje"].ToString()).ToString("#0.00");
            lblBLminimum.Text = "Polog: " + Convert.ToDecimal(DT_smjena.Rows[0]["pocetno_stanje"].ToString()).ToString("#0.00");

            DataTable DT = new DataTable();
            DataTable DT3 = new DataTable();

            string sql1 = "SELECT SUM(ukupno_gotovina) AS [gotovina],SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
            " WHERE datum_racuna>'" + Convert.ToDateTime(datumOD).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum_racuna<'" + Convert.ToDateTime(datumDO).ToString("yyyy-MM-dd H:mm:ss") + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "' " +
                "";

            DT = classSQL.select(sql1, "racuni").Tables[0];
            if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
            {
                string a = "0";
                string b = "0";

                if (DT.Rows[0]["virman"].ToString() != "") { b = DT.Rows[0]["virman"].ToString(); }

                Sukupno = String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString()) + Convert.ToDouble(DT.Rows[0]["kartice"].ToString()) + Convert.ToDouble(a) + Convert.ToDouble(b));

                lblUkupno.Text = "Sve ukupno: " + Sukupno + " kn";
                lblProdanoGotovina.Text = "Ukupno prodano u smjeni gotovina: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["gotovina"].ToString())) + " kn";
                lblProdanoKartice.Text = "Ukupno prodano u smjeni kartice: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["kartice"].ToString())) + " kn";

                if (DT.Rows[0]["virman"].ToString() != "") { lblProdanoVirman.Text = "Ukupno prodano u smjeni virman: " + String.Format("{0:0.00}", Convert.ToDouble(DT.Rows[0]["virman"].ToString())) + " kn"; } else { lblProdanoVirman.Text = "Virman: 0.00 kn"; }
            }
            else
            {
                lblUkupno.Text = "Ukupno: 0.00 kn";
                lblProdanoGotovina.Text = "Gotovina: 0.00 kn";
                lblProdanoKartice.Text = "Kartice: 0.00 kn";
                lblProdanoVirman.Text = "Virman: 0.00 kn";
            }

            string sql_konobar = "SELECT " +
                " SUM(ukupno_gotovina) AS gotovina," +
                " SUM(ukupno_kartice) AS kartice," +
                " SUM(ukupno_virman) AS [virman]," +
                " racuni.id_blagajnik," +
                " zaposlenici.ime," +
                " zaposlenici.prezime" +
                " FROM zaposlenici " +
                " LEFT JOIN racuni ON racuni.id_blagajnik=zaposlenici.id_zaposlenik" +
                " WHERE aktivan='DA' AND racuni.datum_racuna>'" + Convert.ToDateTime(datumOD).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND racuni.datum_racuna<'" + Convert.ToDateTime(datumDO).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'" +
                " GROUP BY racuni.id_blagajnik,ime,prezime";

            DataTable DTkonobari = classSQL.select(sql_konobar, "zaposlenici").Tables[0];
            flpArtikli.Controls.Clear();
            int i = 1;

            foreach (DataRow row in DTkonobari.Rows)
            {
                GroupBox groupBox1 = new GroupBox();
                groupBox1.Location = new System.Drawing.Point(3, 3);
                groupBox1.Name = "groupBox1";
                groupBox1.Size = new System.Drawing.Size(425, 75);
                groupBox1.TabIndex = 0;
                groupBox1.TabStop = false;

                Label lbl = new Label();
                lbl.BackColor = System.Drawing.Color.Transparent;
                lbl.Font = new System.Drawing.Font("Verdana", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Location = new System.Drawing.Point(10, 15);
                lbl.AutoSize = true;
                lbl.TabIndex = i;

                string Ime_konobara = row["ime"].ToString() + " " + row["prezime"].ToString();

                string a = "0";
                string b = "0";

                if (row["virman"].ToString() != "") { b = row["virman"].ToString(); }

                lbl.Name = row["id_blagajnik"].ToString();
                lbl.Text = row["ime"].ToString() + " " + row["prezime"].ToString() + "  " + Convert.ToDouble(Convert.ToDouble(row["gotovina"].ToString()) + Convert.ToDouble(row["kartice"].ToString()) + Convert.ToDouble(a) + Convert.ToDouble(b)).ToString("#0.00") +
                    " kn\r\nGotovina: " + Convert.ToDouble(row["gotovina"].ToString()).ToString("#0.00") +
                    " kn,  Kartice: " + Convert.ToDouble(row["kartice"].ToString()).ToString("#0.00") + " kn" +
                    " \n\rVirman: " + Convert.ToDouble(b).ToString("#0.00") + " kn";

                groupBox1.Controls.Add(lbl);
                flpArtikli.Controls.Add(groupBox1);
                i++;
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnZavrsiSmjenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                button1.PerformClick();
            }
        }

        private int RecLineChars;
        private string tekst = "";
        private DataTable DTpdv = new DataTable();
        private DataTable DTartikli = new DataTable();
        private DataRow RowPdv;
        private DataRow RowOsnovica;
        private DataRow RowArtikl;
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

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

        private void Artikli(string artikl, decimal kolicina, string sifra, string mpc)
        {
            DataRow[] dataROW = DTartikli.Select("sifra = '" + sifra + "' AND mpc='" + mpc + "'");

            if (dataROW.Count() == 0)
            {
                RowArtikl = DTartikli.NewRow();
                RowArtikl["sifra"] = sifra;
                RowArtikl["mpc"] = mpc;
                RowArtikl["kolicina"] = kolicina.ToString();
                RowArtikl["naziv"] = artikl;
                DTartikli.Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["kolicina"] = Convert.ToDecimal(dataROW[0]["kolicina"].ToString()) + kolicina;
            }
        }

        private void PrometProdajneRobe(bool pice, bool hrana, bool stavke)
        {
            RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());

            string podgrupa_pice = "";
            if (pice)
            {
                podgrupa_pice = " AND roba.id_podgrupa <> '1'";
            }
            string podgrupa_hrana = "";
            if (hrana)
            {
                podgrupa_hrana = " AND roba.id_podgrupa <> '2'";
            }

            string sql = "SELECT " +
                " racun_stavke.kolicina," +
                " grupa.grupa," +
                " racun_stavke.sifra_robe," +
                " racun_stavke.mpc," +
                " racun_stavke.porez_potrosnja," +
                " racun_stavke.porez," +
                " racuni.nacin_placanja," +
                //" racuni.ukupno_kartice," +
                //" racuni.ukupno_virman," +
                " roba.naziv" +
                " FROM racun_stavke" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna  AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
                " WHERE  racuni.datum_racuna>'" + Convert.ToDateTime(datumOD).ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + Convert.ToDateTime(datumDO).ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "' " +
                " " + podgrupa_hrana + podgrupa_pice +
                " " + " ORDER BY grupa.grupa";

            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

            decimal kol = 0;
            decimal pnp = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal pnpUKUPNO = 0;
            decimal pdvUKUPNO = 0;
            decimal SVE_UKUPNO = 0;
            decimal OSNOVICA = 0;
            string g = "";

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
            }

            if (DTartikli.Columns["sifra"] == null)
            {
                DTartikli.Columns.Add("sifra");
                DTartikli.Columns.Add("kolicina");
                DTartikli.Columns.Add("mpc");
                DTartikli.Columns.Add("naziv");
            }

            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
            PrintTextLine("Adresa: " + DT_tvr.Rows[0]["adresa"].ToString());
            if (DT_tvr.Rows[0]["poslovnica_adresa"].ToString() != "")
            {
                PrintTextLine(DT_tvr.Rows[0]["poslovnica_adresa"].ToString());
            }
            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
            PrintTextLine("Datum: " + DateTime.Now);
            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
            PrintTextLine("OD: " + datumOD);
            PrintTextLine("DO: " + datumDO);
            PrintTextLine("POLOG: " + blagMinimum);
            PrintTextLine(new string('-', RecLineChars));

            if (DTpdvN.Columns["stopa"] == null)
            {
                DTpdvN.Columns.Add("stopa");
                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
            }
            else
            {
                DTpdvN.Clear();
            }

            if (DTosnovica.Columns["osnovica"] == null)
            {
                DTosnovica.Columns.Add("osnovica");
                DTosnovica.Columns.Add("nacin");
            }
            else
            {
                DTosnovica.Clear();
            }

            decimal UG = 0;
            decimal UK = 0;
            decimal UV = 0;
            decimal UO = 0;

            foreach (DataRow row in DT.Rows)
            {
                kol = Convert.ToDecimal(row["kolicina"].ToString());
                mpc = Convert.ToDecimal(row["mpc"].ToString());
                pnp = Convert.ToDecimal(row["porez_potrosnja"].ToString());
                pdv = Convert.ToDecimal(row["porez"].ToString());

                //Ovaj kod dobiva PDV
                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
                decimal ppdv = (((mpc * kol) * PreracunataStopaPDV) / 100);
                pdvUKUPNO = ppdv + pdvUKUPNO;

                //Ovaj kod dobiva porez na potrošnju
                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
                decimal ppnp = (((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100);
                pnpUKUPNO = ppnp + pnpUKUPNO;

                SVE_UKUPNO = (mpc * kol) + SVE_UKUPNO;

                if (row["nacin_placanja"].ToString() == "G")
                {
                    StopePDVaN(pdv, ppdv, "G", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UG = (mpc * kol) + UG;
                }
                else if (row["nacin_placanja"].ToString() == "K")
                {
                    StopePDVaN(pdv, ppdv, "K", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UK = (mpc * kol) + UK;
                }
                else if (row["nacin_placanja"].ToString() == "T")
                {
                    StopePDVaN(pdv, ppdv, "T", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UV = (mpc * kol) + UV;
                }
                else if (row["nacin_placanja"].ToString() == "O")
                {
                    StopePDVaN(pdv, ppdv, "O", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UO = (mpc * kol) + UO;
                }

                string ajjj = row["nacin_placanja"].ToString();

                Artikli(row["naziv"].ToString(), kol, row["naziv"].ToString(), mpc.ToString());
                StopePDVa(pdv, ((mpc * kol) * PreracunataStopaPDV) / 100);

                OSNOVICA = ((mpc * kol) - ((ppdv) + (ppnp))) + OSNOVICA;
            }

            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString()) - 3;
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString()) + 3;
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString()) + 3;

            PrintTextLine(String.Empty);
            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
            PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
            PrintText("\r\n");
            PrintTextLine(new string('=', RecLineChars));

            for (int i = 0; i < DTartikli.Rows.Count; i++)
            {
                PrintText(TruncateAt(DTartikli.Rows[i]["naziv"].ToString().PadRight(a), a));
                PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString()).ToString("#0.00").PadLeft(k), k));
                PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()).ToString("#0.00").PadLeft(c), c));
                PrintTextLine(TruncateAt((Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()) * Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString())).ToString("#0.00").PadLeft(s), s));
            }

            PrintTextLine("");
            PrintTextLine(new string('-', RecLineChars));

            //PrintTextLine("OSNOVICA: " + OSNOVICA.ToString("#0.000"));
            PrintTextLine("PNP ukupno:       " + pnpUKUPNO.ToString("#0.000"));

            //for(int i=0; i<DTpdv.Rows.Count; i++)
            //{
            //    PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00"));
            //}

            //GOTOVINA
            if (UG > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO GOTOVINA:  " + UG.ToString("#0.000"));
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "G")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "G")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
                    }
                }
            }

            //KARTICA
            if (UK > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO KARTICE:  " + UK.ToString("#0.000"));
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "K")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "K")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
                    }
                }
            }

            //VIRMAN
            if (UV > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO VIRMAN:  " + UV.ToString("#0.000"));
                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
                //{
                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "T")
                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
                //}
                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "T")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
                    }
                }
            }

            //ostalo
            if (UO > 0)
            {
                PrintTextLine(new string('-', RecLineChars));
                PrintTextLine("UKUPNO OSTALO:    " + UO.ToString("#0.000"));

                for (int i = 0; i < DTpdvN.Rows.Count; i++)
                {
                    if (DTpdvN.Rows[i]["nacin"].ToString() == "O")
                    {
                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
                    }
                }
            }

            PrintTextLine(new string('-', RecLineChars));

            PrintTextLine("OSNOVICA UKUPNO:  " + OSNOVICA.ToString("#0.00"));
            for (int i = 0; i < DTpdv.Rows.Count; i++)
            {
                PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "% UKUPNO:   " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00"));
            }
            PrintTextLine("SVE UKUPNO:       " + SVE_UKUPNO.ToString("#0.00"));

            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);

            rtb.Font = font;
            rtb.Text = tekst;
        }

        private DataTable DTpdvN = new DataTable();

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            DataRow[] dataROW = DTpdvN.Select("stopa = '" + pdv.ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = pdv.ToString();
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

        private DataTable DTosnovica = new DataTable();

        private void StopeOsnovica(string nacin_P, decimal osnovica)
        {
            DataRow[] dataROW = DTosnovica.Select("nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowOsnovica = DTosnovica.NewRow();
                RowOsnovica["nacin"] = nacin_P;
                RowOsnovica["osnovica"] = osnovica;
                DTosnovica.Rows.Add(RowOsnovica);
            }
            else
            {
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

            //header
            String drawString = tekst;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(tekst, drawFont);

            drawFont = font;
            float y = 0.0F;
            float x = 0.0F;

            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        private void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                string ttx = "\r\n" + tekst;
                ttx = ttx.Replace("č", "c");
                ttx = ttx.Replace("Č", "C");
                ttx = ttx.Replace("ž", "z");
                ttx = ttx.Replace("Ž", "Z");
                ttx = ttx.Replace("ć", "c");
                ttx = ttx.Replace("Ć", "C");
                ttx = ttx.Replace("đ", "d");
                ttx = ttx.Replace("Đ", "D");
                ttx = ttx.Replace("š", "s");
                ttx = ttx.Replace("Š", "S");

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
            }
            else
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = String.Format(
                       "Can't find printer \"{0}\".", printerName);
                    MessageBox.Show(msg, "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();
            }
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private void chbStavke_CheckedChanged(object sender, EventArgs e)
        {
            bool p = false;
            if (!chbPice.Checked)
            {
                p = true;
            }

            bool h = false;
            if (!chbHrana.Checked)
            {
                h = true;
            }

            bool s = false;
            if (!chbStavke.Checked)
            {
                s = true;
            }

            DTpdv.Clear();
            DTartikli.Clear();
            DTpdvN.Clear();
            rtb.Text = "";
            tekst = "";
            PrometProdajneRobe(p, h, s);
        }

        private void chbHrana_CheckedChanged(object sender, EventArgs e)
        {
            bool p = false;
            if (!chbPice.Checked)
            {
                p = true;
            }

            bool h = false;
            if (!chbHrana.Checked)
            {
                h = true;
            }

            bool s = false;
            if (!chbStavke.Checked)
            {
                s = true;
            }

            DTpdv.Clear();
            DTartikli.Clear();
            DTpdvN.Clear();
            rtb.Text = "";
            tekst = "";
            PrometProdajneRobe(p, h, s);
        }

        private void chbPice_CheckedChanged(object sender, EventArgs e)
        {
            bool p = false;
            if (!chbPice.Checked)
            {
                p = true;
            }

            bool h = false;
            if (!chbHrana.Checked)
            {
                h = true;
            }

            bool s = false;
            if (!chbStavke.Checked)
            {
                s = true;
            }

            DTpdv.Clear();
            DTartikli.Clear();
            DTpdvN.Clear();

            rtb.Text = "";
            tekst = "";
            PrometProdajneRobe(p, h, s);
        }
    }
}