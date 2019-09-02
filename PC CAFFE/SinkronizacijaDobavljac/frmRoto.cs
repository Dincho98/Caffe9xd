using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.SinkronizacijaDobavljac
{
    public partial class frmRoto : Form
    {
        public frmRoto()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string poslovnica = "1";

        private void frmRoto_Load(object sender, EventArgs e)
        {
            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            DataTable DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void loadXML(string path)
        {
            string myXMLfile = path;
            DataSet ds = new DataSet();
            //System.IO.FileStream fsReadXml = new System.IO.FileStream(myXMLfile, System.IO.FileMode.Open);
            try
            {
                //ds.ReadXml(myXMLfile, XmlReadMode.IgnoreSchema);
                string xml = File.ReadAllText(path, System.Text.Encoding.Default);
                //var xmlDoc = XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
                ds.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(xml)), XmlReadMode.InferSchema);

                DataTable DTdoc = ds.Tables["G_DOK"];
                DataTable DTstavke = ds.Tables["G_STAVKE"];
                DataTable DT100 = ds.Tables["FA100RRO"];
                DataTable DTporez = ds.Tables["G_POREZ"];

                dgvArtikli.Rows.Clear();
                dgvMaping.Rows.Clear();
                DT_naseGrupe = new DataTable();
                DT_naseGrupe = new DataTable();

                if (DTdoc.Rows.Count > 0)
                {
                    txtBrojUlaznogDokumenta.Text = DTdoc.Rows[0]["CF_BRDOK"].ToString();
                    datumDokumenta.Text = DTdoc.Rows[0]["FA110_DAT_DOK"].ToString();
                    txtNazivPoslovnice.Text = DTdoc.Rows[0]["GS300_NAZIV"].ToString();
                    txtAdresaPoslovnice.Text = DTdoc.Rows[0]["GS300_ADRESA"].ToString();
                    txtMjestoPoslovnice.Text = DTdoc.Rows[0]["GS300_MJESTO"].ToString();
                }

                foreach (DataRow r in DTstavke.Rows)
                {
                    decimal cijena, rabat, kolicina, porez, iznos, iznosPDV;
                    decimal.TryParse(r["FA210_CJ"].ToString(), out cijena);
                    decimal.TryParse(r["FA210_RABAT"].ToString(), out rabat);
                    decimal.TryParse(r["FA210_KOL"].ToString(), out kolicina);
                    decimal.TryParse(r["FA210_POREZ"].ToString(), out porez);
                    decimal.TryParse(r["FA210_IZNBP"].ToString(), out iznos);
                    decimal.TryParse(r["FA210_IZN"].ToString(), out iznosPDV);

                    if (iznos != 0 && r["FA210_ARTIKL"].ToString().ToUpper() != "X1")
                    {
                        dgvArtikli.Rows.Add(r["FA210_ARTIKL"].ToString(),
                            r["FA210_NAZIV"].ToString(),
                            r["FA210_JMJ"].ToString(),
                            Math.Round(kolicina, 4),
                            Math.Round(cijena, 4),
                            Math.Round(rabat, 4),
                            Math.Round(porez, 2),
                            Math.Round(iznos, 3),
                            Math.Round(iznosPDV, 3)
                            );
                    }
                }

                PostaviGrupeNasegWeba();
                PostaviGrupeDobavljaca(DTstavke);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //fsReadXml.Close();
            }
        }

        private DataTable DTartikli;
        private DataTable DT_naseGrupe = new DataTable();

        private void PostaviGrupeNasegWeba()
        {
            DT_naseGrupe.Clear();
            DT_naseGrupe.Columns.Add("id");
            DT_naseGrupe.Columns.Add("grupa");
            string query = @"SELECT sifra,naziv,povratna_naknada FROM roba_prodaja
                            WHERE aktivnost='1' AND id_podgrupa<>'3'
                            ORDER BY naziv ASC;";
            DTartikli = classSQL.select(query, "t").Tables[0];
            DT_naseGrupe.Rows.Add(0, "");
            foreach (DataRow row in DTartikli.Rows)
            {
                if (row["sifra"].ToString() != "")
                    DT_naseGrupe.Rows.Add(row["sifra"].ToString(), row["naziv"].ToString());
            }

            mNasaGrupa.DataSource = DT_naseGrupe;
            mNasaGrupa.DataPropertyName = "grupa";
            mNasaGrupa.DisplayMember = "grupa";
            mNasaGrupa.HeaderText = "Grupa";
            mNasaGrupa.Name = "mNasaGrupa";
            mNasaGrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            mNasaGrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            mNasaGrupa.ValueMember = "id";
        }

        private void btnUcitajDatoteku_Click(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                loadXML(file);
            }
            IzracunKolona(-1, -1);
        }

        private void PostaviGrupeDobavljaca(DataTable DTa)
        {
            DataTable DT_provjera = new DataTable();
            try
            {
                DataTable DTSK = new DataTable("grupe");
                DTSK.Columns.Add("id");
                DTSK.Columns.Add("grupa");

                foreach (DataRow row in DTa.Rows)
                {
                    string val, valNaziv;
                    val = row["FA210_ARTIKL"].ToString();
                    if (val != "")
                        if (val.ToUpper() != "X1")
                        {
                            if (DTSK.Select("id='" + val + "'").Length == 0)
                            {
                                valNaziv = row["FA210_NAZIV"].ToString();
                                DTSK.Rows.Add(val, valNaziv);
                            }
                        }
                }

                mGrupa.DataSource = DTSK;
                mGrupa.DataPropertyName = "grupa";
                mGrupa.DisplayMember = "grupa";
                mGrupa.HeaderText = "Grupa";
                mGrupa.Name = "mGrupa";
                mGrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                mGrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
                mGrupa.ValueMember = "id";

                DT_provjera = classSQL.select("SELECT * FROM maping WHERE partner='1'", "maping").Tables[0];

                foreach (DataRow row in DTSK.Rows)
                {
                    DataRow[] r = DT_provjera.Select("dobavljac_grupa='" + row["id"].ToString() + "'");

                    string NasaGrupa = "0";
                    string DobavljacGrupa = ReplaceString(row["id"].ToString()).ToUpper();

                    if (r.Length > 0)
                    {
                        if (DT_naseGrupe.Select("id='" + r[0]["nasa_grupa"].ToString() + "'").Length != 0)
                        {
                            NasaGrupa = r[0]["nasa_grupa"].ToString();
                        }
                    }

                    if (r.Length == 0)
                    {
                        classSQL.insert("INSERT INTO maping (dobavljac_grupa,partner) VALUES ('" + DobavljacGrupa + "','1')");
                        dgvMaping.Rows.Add(DobavljacGrupa);
                    }
                    else
                    {
                        dgvMaping.Rows.Add(DobavljacGrupa, NasaGrupa);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string ReplaceString(string ttx)
        {
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
            ttx = ttx.Replace("\r", "");
            ttx = ttx.Replace("\n", "");
            ttx = ttx.Replace("'", "");
            ttx = ttx.Replace("\"", "");

            return ttx;
        }

        private void dgvMaping_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                classSQL.update(@"UPDATE maping
                SET nasa_grupa='" + dgvMaping.Rows[e.RowIndex].Cells["mNasaGrupa"].Value + @"'
                WHERE dobavljac_grupa='" + dgvMaping.Rows[e.RowIndex].Cells["mGrupa"].Value + @"'
                AND partner='1';");
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            //UZIMAM ID PARTNERA OD ROTO
            DataTable DTpartner = classSQL.select("SELECT * FROM partners WHERE ime_tvrtke ~* 'roto' LIMIT 1;", "maping").Tables[0];
            string idPartner = "0";
            if (DTpartner.Rows.Count == 0)
            {
                MessageBox.Show("Greška, nemate partnera sa imenom tvrtke 'ROTO'", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                idPartner = DTpartner.Rows[0]["id_partner"].ToString();

            //PROVIJERAVAM DALI U PRIMKAMA VEĆ NE POSTOJI ISTI BROJ ULAZNOG DOKUMENTA
            DataTable DTprimke = classSQL.select("SELECT * FROM primka " +
                " WHERE br_ulaznog_doc='" + txtBrojUlaznogDokumenta.Text + "';", "maping").Tables[0];

            if (DTprimke.Rows.Count > 0)
            {
                MessageBox.Show("Ovaj unos ne možete spremiti jer već postoji u primkama.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //PROVIJERAM ŠEĆER I ŠLAG JER SE ZAPRIMAJU DRUGAČIJE
            foreach (DataGridViewRow r in dgvArtikli.Rows)
            {
                if (r.Cells["naziv"].FormattedValue.ToString().ToUpper().IndexOf("ŠLAG") != -1
                    || r.Cells["naziv"].FormattedValue.ToString().ToUpper().IndexOf("SLAG") != -1)
                {
                    if (MessageBox.Show("Upozorenje, u stavkama postoji šlag kojeg možda zaprimate u ml. a ne po komadu.\r\nIzmjenite stavku ako je potrebno.\r\nŽelite li ipak spremiti stavke?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                        return;
                }
                else if (r.Cells["naziv"].FormattedValue.ToString().ToUpper().IndexOf("ŠEĆER") != -1
                    || r.Cells["naziv"].FormattedValue.ToString().ToUpper().IndexOf("SECER") != -1)
                {
                    if (MessageBox.Show("Upozorenje, u stavkama postoji šećer kojeg možda zaprimate po komadu. a ne po kutiji.\r\nIzmjenite stavku ako je potrebno.\r\nŽelite li ipak spremiti stavke?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                        return;
                }
            }

            //PROVIJERAM NE GRUPIRANE ARTIKLE
            foreach (DataGridViewRow r in dgvMaping.Rows)
            {
                if (r.Cells[1].Value == null || r.Cells[1].Value.ToString() == "0")
                {
                    if (MessageBox.Show("Upozorenje, imate ne grupirane stavke.\r\nŽelite li te stavke grupirati?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) { tabControl1.SelectedIndex = 1; return; }
                    else
                        break;
                }
            }

            DateTime datum = new DateTime();
            DateTime.TryParse(datumDokumenta.Text, out datum);
            string BrojPrimke = brojPrimke();

            decimal primkaSveUkupnoBezPoreza = 0, primkaSveUkupnoSaPorezom = 0, primkaSveUkupnoBezPorezaSve = 0, primkaSveUkupnoSaPorezomSve = 0;
            foreach (DataGridViewRow r in dgvArtikli.Rows)
            {
                decimal.TryParse(r.Cells["iznos"].FormattedValue.ToString(), out primkaSveUkupnoBezPoreza);
                decimal.TryParse(r.Cells["iznosPDV"].FormattedValue.ToString(), out primkaSveUkupnoSaPorezom);
                primkaSveUkupnoBezPorezaSve += primkaSveUkupnoBezPoreza;
                primkaSveUkupnoSaPorezomSve += primkaSveUkupnoSaPorezom;
            }

            //INSERT HEADER
            string sql = "INSERT INTO primka (" +
                    "broj_primke,id_skladiste,br_ulaznog_doc,id_partner,datum,iznos_bez_poreza,iznos_sa_porezom,carina," +
                    "valuta,id_zaposlenik,napomena,is_kalkulacija,iznos,id_poslovnica,novo,editirano)" +
                    " VALUES " +
                    "(" +
                    "'" + BrojPrimke + "'," +
                    "'" + cbSkladiste.SelectedValue + "'," +
                    "'" + txtBrojUlaznogDokumenta.Text + "'," +
                    "'" + idPartner + "'," +
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + Math.Round(primkaSveUkupnoBezPorezaSve, 2).ToString().Replace(",", ".") + "'," +
                    "'" + Math.Round(primkaSveUkupnoSaPorezomSve, 2).ToString().Replace(",", ".") + "'," +
                    "'0'," +
                    "'1'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "''," +
                    "'0'," +
                    "'" + Math.Round(primkaSveUkupnoBezPorezaSve, 2).ToString().Replace(",", ".") + "'," +
                    "'" + poslovnica + "','1','0'" +
                    ")";
            provjera_sql(classSQL.insert(sql));
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada primke br." + BrojPrimke + "')"));

            //UVIJEL BRIŠEM STAVKE OD ZADANOG BROJA (NEKAD JIH NEMA, NARAVNO :-)  )
            string kol = "";
            provjera_sql(classSQL.delete("DELETE FROM primka_stavke WHERE broj_primke='" + BrojPrimke + "' AND is_kalkulacija='0' AND id_skladiste='" + cbSkladiste.SelectedValue + "'"));

            DataTable DTmaping = classSQL.select("SELECT * FROM maping WHERE partner='1'", "maping").Tables[0];

            //SPREMANJE STAVKA
            foreach (DataGridViewRow r in dgvArtikli.Rows)
            {
                DataRow[] rRobaProdaja = null;
                DataRow[] rMaping = DTmaping.Select("dobavljac_grupa='" + r.Cells["sifra"].FormattedValue.ToString() + "'");

                if (rMaping.Length > 0)
                    rRobaProdaja = DTartikli.Select("sifra='" + rMaping[0]["nasa_grupa"].ToString() + "'");

                if (rRobaProdaja != null && rRobaProdaja.Length > 0)
                {
                    decimal kolicina, cijena, porez, iznos, iznosPDV;
                    decimal.TryParse(r.Cells["kolicina"].FormattedValue.ToString(), out kolicina);
                    decimal.TryParse(r.Cells["cijena"].FormattedValue.ToString(), out cijena);
                    decimal.TryParse(r.Cells["porez"].FormattedValue.ToString(), out porez);
                    decimal.TryParse(r.Cells["iznos"].FormattedValue.ToString(), out iznos);
                    decimal.TryParse(r.Cells["iznosPDV"].FormattedValue.ToString(), out iznosPDV);

                    sql = "INSERT INTO primka_stavke (" +
                        "broj_primke,id_skladiste,sifra,u_pakiranju,broj_paketa,kolicina,cijena_po_komadu,rabat,nabavna_cijena,ulazni_porez,nabavni_iznos,iznos,is_kalkulacija,povratna_naknada)" +
                        " VALUES " +
                        "(" +
                        "'" + BrojPrimke + "'," +
                        "'" + cbSkladiste.SelectedValue.ToString() + "'," +
                        "'" + rRobaProdaja[0]["sifra"].ToString() + "'," +
                        "'" + Math.Round(kolicina, 4).ToString().Replace(",", ".") + "'," +
                        "'1'," +
                        "'" + Math.Round(kolicina, 4).ToString().Replace(",", ".") + "'," +
                        "'" + Math.Round((iznos / kolicina), 4).ToString().Replace(",", ".") + "'," +
                        "'0'," +
                        "'" + Math.Round((iznos / kolicina), 4).ToString().Replace(",", ".") + "'," +
                        "'" + Math.Round(porez, 2).ToString().Replace(",", ".") + "'," +
                        "'" + Math.Round(iznos, 2).ToString().Replace(",", ".") + "'," +
                        "'" + Math.Round(iznosPDV, 2).ToString().Replace(",", ".") + "'," +
                        "'0'," +
                        "'" + rRobaProdaja[0]["povratna_naknada"].ToString().Replace(",", ".") + "'" +
                        ");";

                    provjera_sql(classSQL.insert(sql));
                }
            }
            MessageBox.Show("Primka je uspješno spremljena.", "Spremljeno", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string brojPrimke()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_primke AS INT)) FROM primka WHERE is_kalkulacija='False'", "primka").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void dgvArtikli_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IzracunKolona(e.ColumnIndex, e.RowIndex);
        }

        private void IzracunKolona(int kolona, int row)
        {
            decimal UbezPDV = 0, UsaPDV = 0, bezPDV = 0, saPDV = 0, kolicina = 0, cijena = 0, pdv = 0;

            if (kolona != -1 && row != -1)
            {
                decimal.TryParse(dgvArtikli.Rows[row].Cells["kolicina"].FormattedValue.ToString(), out kolicina);
                decimal.TryParse(dgvArtikli.Rows[row].Cells["cijena"].FormattedValue.ToString(), out cijena);
                decimal.TryParse(dgvArtikli.Rows[row].Cells["iznos"].FormattedValue.ToString(), out bezPDV);
                decimal.TryParse(dgvArtikli.Rows[row].Cells["porez"].FormattedValue.ToString(), out pdv);
            }

            switch (kolona)
            {
                case 3:
                    dgvArtikli.Rows[row].Cells["cijena"].Value = Math.Round((bezPDV / kolicina), 5).ToString("#0.0000");
                    break;

                case 4:
                    dgvArtikli.Rows[row].Cells["iznos"].Value = Math.Round((cijena * kolicina), 3).ToString("#0.00");
                    dgvArtikli.Rows[row].Cells["iznosPDV"].Value = Math.Round((cijena * kolicina) * (1 + (pdv / 100)), 3).ToString("#0.00");
                    break;

                case 7:
                    dgvArtikli.Rows[row].Cells["cijena"].Value = Math.Round((bezPDV / kolicina), 5).ToString("#0.0000");
                    break;

                default:
                    break;
            }

            foreach (DataGridViewRow r in dgvArtikli.Rows)
            {
                decimal.TryParse(r.Cells["iznos"].FormattedValue.ToString(), out bezPDV);
                decimal.TryParse(r.Cells["iznosPDV"].FormattedValue.ToString(), out saPDV);

                UbezPDV += bezPDV;
                UsaPDV += saPDV;
            }

            lblBezPdv.Text = "Ukupno bez poreza: " + Math.Round(UbezPDV, 3).ToString("#0.00");
            lblSaPdv.Text = "Ukupno sa porezom: " + Math.Round(UsaPDV, 3).ToString("#0.00");
            lblPorez.Text = "Porez: " + Math.Round(UsaPDV - UbezPDV, 3).ToString("#0.00");
        }

        private void dgvArtikli_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            IzracunKolona(-1, -1);
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