using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmPovratRobe : Form
    {
        public frmPovratRobe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string skladiste_pocetno = "";
        private bool edit = false;
        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        public string broj_povrata_edit { get; set; }
        public frmMenu MainForm { get; set; }

        private void frmPovratRobe_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            SetSkladiste();
            txtBroj.Text = brojPovrata();
            nmGodina.Value = DateTime.Now.Year;
            nmGodina.Maximum = DateTime.Now.Year + 30;
            nmGodina.Minimum = DateTime.Now.Year - 30;
            ControlDisableEnable(1, 0, 0, 1, 0);

            if (broj_povrata_edit != null) { FillPovrat(); }
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void SetSkladiste()
        {
            classSQL.update("UPDATE povrat_robe SET godina='2014' WHERE godina='0' OR godina is null OR godina =''");

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void FillPovrat()
        {
            string sssql = "SELECT * FROM povrat_robe WHERE broj='" + broj_povrata_edit + "'";
            DataTable DTheader = classSQL.select(sssql, "povrat_robe").Tables[0];

            DateTime _DT;
            DateTime.TryParse(DTheader.Rows[0]["datum"].ToString(), out _DT);
            string ovl = Util.Korisno.UzmiOvlastTrenutnogZaposlenika();
            int br_days = 366;
            if (ovl == "user")
                br_days = 10;

            if (Util.Korisno.ZabranaUređivanjaDokumenta(br_days, _DT, ovl))
            {
                MessageBox.Show("Nemate ovlaštenje uređivati ovaj dokumenat nakon " + br_days + " dana od izrade istog.");
                return;
            }

            EnableDisable(true);
            DeleteFields();
            ControlDisableEnable(0, 1, 0, 0, 1);

            nmGodina.Value = Convert.ToInt16(DTheader.Rows[0]["godina"].ToString());
            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            //cbSkladiste.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_skladiste"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            string sql = "SELECT " +
                " povrat_robe_stavke.id_stavka," +
                " povrat_robe_stavke.sifra," +
                " povrat_robe_stavke.kolicina," +
                " povrat_robe_stavke.vpc," +
                " povrat_robe_stavke.nbc," +
                " povrat_robe_stavke.pdv," +
                " povrat_robe_stavke.rabat," +
                " roba_prodaja.naziv" +
                " FROM povrat_robe_stavke " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=povrat_robe_stavke.sifra" +
                " WHERE povrat_robe_stavke.broj='" + broj_povrata_edit + "'";

            DT_stavke = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                double kolicina = Convert.ToDouble(DT_stavke.Rows[i]["kolicina"].ToString());
                double pdv = Convert.ToDouble(DT_stavke.Rows[i]["pdv"].ToString());

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT_stavke.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT_stavke.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["pdv"].Value = DT_stavke.Rows[i]["pdv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["mpc"].Value = Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()).ToString("#0.00");
                dgw.Rows[br].Cells["ukupno"].Value = Convert.ToDouble(Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()) * kolicina).ToString("#0.00");
            }

            ControlDisableEnable(0, 1, 1, 0, 1);
            edit = true;
            PaintRows(dgw);
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private string brojPovrata()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM povrat_robe", "povrat_robe").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBroj.Text = brojPovrata();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
            cbSkladiste.Select();
        }

        private void EnableDisable(bool x)
        {
            dtpDatum.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;

            if (x == true)
            {
                txtBroj.Enabled = false;
                nmGodina.Enabled = false;
            }
            else if (x == false)
            {
                txtBroj.Enabled = true;
                nmGodina.Enabled = true;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojPovrata();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void DeleteFields()
        {
            dtpDatum.Text = "";
            rtbNapomena.Text = "";
            dgw.Rows.Clear();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            DataTable DTbool = classSQL.select("SELECT broj FROM povrat_robe WHERE broj = '" + txtBroj.Text + "'", "povrat_robe").Tables[0];

            string sql = "";
            if (DTbool.Rows.Count == 0)
            {
                sql = "INSERT INTO povrat_robe (broj,godina,datum,id_izradio,napomena,id_skladiste,novo) VALUES (" +
                     " '" + txtBroj.Text + "'," +
                     " '" + nmGodina.Value.ToString() + "'," +
                      " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                          " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                           " '" + rtbNapomena.Text + "'," +
                           " '" + cbSkladiste.SelectedValue + "','1'" +
                                ")";
                provjera_sql(classSQL.insert(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Povrat robe br." + txtBroj.Text + "')"));
            }
            else
            {
                sql = "UPDATE povrat_robe SET " +
                     " godina='" + nmGodina.Value.ToString() + "'," +
                     " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                     " editirano='1'," +
                     " napomena='" + rtbNapomena.Text + "" +
                     "' WHERE broj='" + txtBroj.Text + "'";
                provjera_sql(classSQL.update(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje povrata robe br." + txtBroj.Text + "')"));
            }

            string ssql = "";
            string kol = "";

            DataTable DTartikli = classSQL.select("SELECT * FROM roba", "roba").Tables[0];

            for (int i = 0; i < dgw.RowCount; i++)
            {
                string sifra = dgw.Rows[i].Cells["sifra"].FormattedValue.ToString();

                DataRow[] row = DTartikli.Select("sifra='" + sifra + "'");
                decimal mpc_cijena = 0;
                if (row.Length > 0)
                    decimal.TryParse(row[0]["mpc"].ToString(), out mpc_cijena);

                decimal mpc, nbc, kolicina, pdv, rabat;
                decimal.TryParse(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString(), out mpc);
                decimal.TryParse(dgw.Rows[i].Cells["nbc"].FormattedValue.ToString(), out nbc);
                decimal.TryParse(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), out kolicina);
                decimal.TryParse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString(), out pdv);
                decimal.TryParse("0", out rabat);

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    ssql = "INSERT INTO povrat_robe_stavke (sifra,nbc,vpc,rabat,mpc,pdv,kolicina,prodajna_cijena,broj) VALUES (" +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + Math.Round(nbc, 3).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round((mpc / (1 + (pdv / 100))), 3).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round(rabat, 5).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round(mpc, 3).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round(pdv, 2).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round(kolicina, 5).ToString().Replace(".", ",") + "'," +
                        "'" + Math.Round(mpc, 3).ToString().Replace(",", ".") + "'," +
                        "'" + txtBroj.Text + "'" +
                        ")";

                    provjera_sql(classSQL.insert(ssql));

                    /*
                      sifra character varying(25),
                      vpc money,
                      pdv character varying(8),
                      rabat character varying(15),
                      broj bigint,
                      kolicina character varying(8),
                      id_stavka serial NOT NULL,
                      nbc money,
                      mpc money,
                      prodajna_cijena numeric DEFAULT (0)::numeric,
                    */

                    kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                    SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                }
                else
                {
                    ssql = "UPDATE povrat_robe_stavke SET " +
                        "sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "prodajna_cijena='" + mpc_cijena.ToString().Replace(",", ".") + "'," +
                        "pdv='" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "kolicina='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'" +
                        " WHERE id_stavka='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'" +
                        "";

                    provjera_sql(classSQL.update(ssql));

                    DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (cbSkladiste.SelectedValue.ToString() == skladiste_pocetno)
                    {
                        kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "+");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                    else
                    {
                        //vrača na staro skladiste
                        kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                        SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                        //oduzima sa novog skladiste
                        kol = SQL.ClassSkladiste.GetAmountCaffeDirect(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                }
            }

            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojPovrata();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno");
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataTable DT = classSQL.select("SELECT broj FROM povrat_robe WHERE  broj='" + txtBroj.Text + "'", "povrat_robe").Tables[0];
                DeleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPovrata() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    if (Until.classZakljucavanjeDokumenta.isLockOtpisRobe(Convert.ToInt32(txtBroj.Text)))
                    {
                        MessageBox.Show("Otpis robe je zaključan. Uređivanje nije dopušteno.");
                        return;
                    }

                    broj_povrata_edit = txtBroj.Text;
                    FillPovrat();
                    EnableDisable(true);
                    edit = true;
                    cbSkladiste.Select();
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtIzradio.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtOrginalniDok_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTRoba;

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba_prodaja" +
                    " WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];

            //double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
            double mpc = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString());
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["ulazni_porez"].ToString());

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["mpc"].Value = mpc.ToString("#0.00");
            dgw.Rows[br].Cells["pdv"].Value = pdv;
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["ukupno"].Value = mpc.ToString("#0.00");
            dgw.Rows[br].Cells["nbc"].Value = mpc.ToString("#0.00");

            SetSkladiste();
            dgw.BeginEdit(true);
            PaintRows(dgw);
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            this.TopMost = false;

            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba_prodaja WHERE sifra = '" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            Robno.frmSviPovrati sp = new Robno.frmSviPovrati();
            sp.sifra = "";
            sp.MainForm = this;
            sp.ShowDialog();

            this.TopMost = true;
            if (broj_povrata_edit != null)
            {
                DeleteFields();
                FillPovrat();
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ovog otpisa vračate i količinu robe na skladišta. Dali ste sigurni da želite obrisai ovaj otpis?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                        skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_pocetno + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        skl = skl + fa_kolicina;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_pocetno + "'");
                    }
                }

                classSQL.delete("DELETE FROM povrat_robe_stavke WHERE broj='" + txtBroj.Text + "'");
                //classSQL.delete("DELETE FROM povrat_robe WHERE broj='" + txtBroj.Text + "'");
                classSQL.update("UPDATE povrat_robe SET editirano='1' WHERE broj='" + txtBroj.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje cijele povratnice dobavljaču br." + txtBroj.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                DeleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke vračate i količinu robe na skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste_pocetno + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste_pocetno + "'");

                    classSQL.update("UPDATE povrat_robe SET editirano='1' WHERE broj='" + txtBroj.Text + "'");
                    classSQL.delete("DELETE FROM povrat_robe_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje stavke sa povratnice robe br." + txtBroj.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();

                    decimal kol, mpc;
                    decimal.TryParse(dgw.Rows[e.RowIndex].Cells["mpc"].FormattedValue.ToString(), out mpc);
                    decimal.TryParse(dgw.Rows[e.RowIndex].Cells["kolicina"].FormattedValue.ToString(), out kol);

                    dgw.Rows[e.RowIndex].Cells["ukupno"].Value = Math.Round((kol * mpc), 3).ToString("#0.00");
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }
            }
        }

        private void frmPovratRobe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            robno.PostaviStanjeSkladista();
            try
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
        }

        private void frmPovratRobe_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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