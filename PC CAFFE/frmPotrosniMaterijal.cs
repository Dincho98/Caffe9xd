using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPotrosniMaterijal : Form
    {
        public string broj_documenta_edit { get; set; }

        public frmPotrosniMaterijal()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTpodaci = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;
        public frmMenu MainForm { get; set; }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
            {
                this.Dock = DockStyle.None;
            }

            base.WndProc(ref m);
        }

        private DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
        private string poslovnica = "1";

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            string poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            MyDataGrid.MainForm = this;
            numeric();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            fillComboBox();
            txtBroj.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_documenta_edit != null) { fillDocumenat(); }
            btnNoviUnos.Select();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmPotrosniMaterijal MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.C))
                {
                    Clipboard.SetText(MainForm.dgw.CurrentCell.FormattedValue.ToString());
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Insert)
                {
                    MainForm.dgw.Rows.Add(MainForm.dgw.Rows.Count, "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                    MainForm.RedniBroj();
                    return true;
                }
                else if (keyData == Keys.Delete)
                {
                    MainForm.dgw.Rows.RemoveAt(MainForm.dgw.CurrentRow.Index);
                    MainForm.RedniBroj();
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[1];
                d.BeginEdit(true);
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

        private void fillComboBox()
        {
            nmGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            cbNacinPlacanja.Items.Add("Gotovina");
            cbNacinPlacanja.Items.Add("Kartice");
            cbNacinPlacanja.Items.Add("Transakcija");
            cbNacinPlacanja.Items.Add("Ostalo");
            cbNacinPlacanja.Text = "Gotovina";

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DataTable DTptma = classSQL.select("SELECT * FROM potrosni_materijal_artikli ORDER BY naziv", "potrosni_materijal_artikli").Tables[0];
            cbArtikli.DataSource = DTptma;
            cbArtikli.DisplayMember = "naziv";
            cbArtikli.ValueMember = "sifra";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void numeric()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void izracun(int col)
        {
            if (dgw.RowCount > 0)
            {
                try
                {
                    decimal _kolicina = 0, _mpc = 0, _porez = 0, _rabat = 0, _sveukupno = 0, _vpc = 0;

                    decimal.TryParse(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString(), out _porez);

                    if (col == 6)
                    {
                        decimal.TryParse(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString(), out _mpc);
                        dgw.CurrentRow.Cells["vpc"].Value = Math.Round(_mpc / (1 + (_porez / 100)), 4).ToString("#0.000");
                        decimal.TryParse(dgw.CurrentRow.Cells["vpc"].FormattedValue.ToString(), out _vpc);
                    }
                    else
                        decimal.TryParse(dgw.CurrentRow.Cells["vpc"].FormattedValue.ToString(), out _vpc);

                    decimal.TryParse(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString(), out _kolicina);
                    //decimal.TryParse(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString(), out _mpc);

                    decimal.TryParse(dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString(), out _rabat);
                    _mpc = Math.Round((_vpc + (_vpc * _porez / 100)), 4);

                    _sveukupno = (_mpc - (_mpc * _rabat / 100)) * _kolicina;
                    dgw.CurrentRow.Cells["iznos_ukupno"].Value = Math.Round(_sveukupno, 3).ToString("#0.00");
                    dgw.CurrentRow.Cells["mpc"].Value = _mpc.ToString("#0.000");
                    dgw.CurrentRow.Cells["rabat_iznos"].Value = Math.Round(((_mpc * _kolicina) * _rabat / 100)).ToString("#0.00");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                decimal porez = 0, bez_poreza = 0, ukupno = 0, sve_ukupno = 0, pdv = 0;

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    decimal.TryParse(row.Cells["iznos_ukupno"].FormattedValue.ToString(), out ukupno);
                    decimal.TryParse(row.Cells["porez"].FormattedValue.ToString(), out pdv);

                    sve_ukupno += ukupno;
                    bez_poreza += ukupno / (1 + (pdv / 100));
                    porez += (sve_ukupno - bez_poreza);
                }

                textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(sve_ukupno, 2).ToString("#0.00");
                textBox2.Text = "Bez PDV-a: " + Math.Round(bez_poreza, 2).ToString("#0.00");
                textBox3.Text = "PDV: " + Math.Round(Math.Round(porez, 2)).ToString("#0.00");
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            classSQL.delete("DELETE FROM potrosni_materijal WHERE broj='" + txtBroj.Text + "' AND godina='" + nmGodina.Value.ToString() + "'");
            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijelog potrošnog materijala br." + txtBroj.Text + "')");

            try
            {
                if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    string domena = Util.Korisno.domena_za_sinkronizaciju;
                    Sinkronizacija.pomagala_syn Pomagala = new Sinkronizacija.pomagala_syn();

                    string queryWeb = "DELETE FROM potrosni_materijal WHERE " +
                        " broj='" + txtBroj.Text + "'" +
                        " AND godina='" + nmGodina.Value.ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "'" +
                        " AND oib='" + DTpostavke.Rows[0]["oib"].ToString() + "';";

                    string[] odg = Pomagala.MyWebRequest(queryWeb + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "include/primam_post_sql_query.php").Split(';');
                }
            }
            catch { }

            MessageBox.Show("Obrisano.");

            txtBroj.ReadOnly = false;
            nmGodina.ReadOnly = false;
            edit = false;
            EnableDisable(false);
            deleteFields();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            txtBroj.Text = brojFakture();
            edit = false;
            txtBroj.ReadOnly = false;
            nmGodina.ReadOnly = false;

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void EnableDisable(bool x)
        {
            txtSifraOdrediste.Enabled = x;

            txtPartnerNaziv.Enabled = x;

            rtbNapomena.Enabled = x;
            btnPartner.Enabled = x;
            dtpDatum.Enabled = x;
            cbNacinPlacanja.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            rtbNapomena.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            txtBroj.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtSifraOdrediste.Select();
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT  COALESCE(MAX(broj),0) zbroj 1 FROM potrosni_materijal WHERE godina ='" + nmGodina.Value.ToString() + "'", "potrosni_materijal").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return DSbr.Rows[0][0].ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgw.RowCount == 0)
                {
                    MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                    return;
                }

                decimal dec_parse;
                if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
                {
                    txtSifraOdrediste.Text = dec_parse.ToString();
                }
                else
                {
                    MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                    return;
                }

                string uk = u.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    uk = uk.Replace(",", ".");
                }
                else
                {
                    uk = uk.Replace(",", ".");
                }

                provjera_sql(classSQL.delete("DELETE FROM potrosni_materijal WHERE broj='" + txtBroj.Text + "' AND godina='" + nmGodina.Value.ToString() + "'"));

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    if (row.Cells["naziv"].FormattedValue.ToString().Length > 0)
                    {
                        decimal kol, rabat, cijena, porez;

                        decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out kol);
                        decimal.TryParse(row.Cells["rabat"].FormattedValue.ToString(), out rabat);
                        decimal.TryParse(row.Cells["mpc"].FormattedValue.ToString(), out cijena);
                        decimal.TryParse(row.Cells["porez"].FormattedValue.ToString(), out porez);

                        string sifra = row.Cells["sifra"].FormattedValue.ToString() == "" ? "0" : row.Cells["sifra"].FormattedValue.ToString();

                        string sql = "INSERT INTO potrosni_materijal (id_partner,id_zaposlenik,broj,godina,datum,placanje,napomena,sifra,naziv,jmj,kolicina,porez,cijena,rabat)" +
                        " VALUES " +
                        " (" +
                        " '" + txtSifraOdrediste.Text + "'," +
                        " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                        " '" + txtBroj.Text + "'," +
                        " '" + nmGodina.Value.ToString() + "'," +
                        " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        " '" + cbNacinPlacanja.Text + "'," +
                        " '" + rtbNapomena.Text + "'," +
                        " '" + sifra + "'," +
                        " '" + row.Cells["naziv"].FormattedValue.ToString() + "'," +
                        " '" + row.Cells["jmj"].FormattedValue.ToString() + "'," +
                        " '" + Math.Round(kol, 5).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(porez, 2).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(cijena, 3).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(rabat, 5).ToString().Replace(",", ".") + "'" +
                        ")";
                        provjera_sql(classSQL.insert(sql));

                        if (sifra != "0")
                        {
                            classSQL.update("UPDATE potrosni_materijal_artikli SET porez='" + Math.Round(porez, 2).ToString().Replace(",", ".") + "',cijena='" + Math.Round(cijena, 3).ToString().Replace(",", ".") + "'" +
                                " WHERE sifra='" + sifra + "'");
                        }
                    }
                }

                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Novi potrošni materijal br." + txtBroj.Text + "')"));

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                txtBroj.ReadOnly = false;
                nmGodina.ReadOnly = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod spremanja fakture!\n\n" + ex.ToString());
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ((char)('+') == (e.KeyChar))
                    return;
            }
            if (sender.ToString()[0] == '-')
            {
                if ((char)('-') == (e.KeyChar))
                    return;
            }

            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSviDokumentiPotrosnogMaterijala svi_dok = new frmSviDokumentiPotrosnogMaterijala();
            svi_dok.MainForm = this;
            svi_dok.ShowDialog();
            if (broj_documenta_edit != null)
            {
                deleteFields();
                fillDocumenat();
            }
        }

        private void fillDocumenat()
        {
            //fill header

            DTpodaci = classSQL.select("SELECT * FROM potrosni_materijal WHERE godina ='" + nmGodina.Value.ToString() + "' AND broj = '" + broj_documenta_edit + "'", "potrosni_materijal").Tables[0];

            if (DTpodaci.Rows.Count > 0)
            {
                DateTime _DT;
                DateTime.TryParse(DTpodaci.Rows[0]["datum"].ToString(), out _DT);

                string ovl = Util.Korisno.UzmiOvlastTrenutnogZaposlenika();
                int br_days = 366;
                if (ovl == "user")
                    br_days = 10;

                if (Util.Korisno.ZabranaUređivanjaDokumenta(br_days, _DT, ovl))
                {
                    MessageBox.Show("Nemate ovlaštenje uređivati ovaj dokumenat nakon " + br_days + " dana od izrade istog.");
                    return;
                }
            }

            EnableDisable(true);
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);

            if (DTpodaci.Rows.Count > 0)
            {
                txtSifraOdrediste.Text = DTpodaci.Rows[0]["id_partner"].ToString();
                txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTpodaci.Rows[0]["id_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
                rtbNapomena.Text = DTpodaci.Rows[0]["napomena"].ToString();
                dtpDatum.Value = Convert.ToDateTime(DTpodaci.Rows[0]["datum"].ToString());
                cbNacinPlacanja.Text = DTpodaci.Rows[0]["placanje"].ToString();
                nmGodina.Value = Convert.ToInt16(DTpodaci.Rows[0]["godina"].ToString());
                txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTpodaci.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
                txtBroj.Text = DTpodaci.Rows[0]["broj"].ToString();
            }
            else
            {
                return;
            }

            foreach (DataRow r in DTpodaci.Rows)
            {
                decimal mpc, mpc_ukupno, vpc, porez, rabat, kolicina, rabat_iznos;

                decimal.TryParse(r["cijena"].ToString(), out mpc);
                decimal.TryParse(r["kolicina"].ToString(), out kolicina);
                decimal.TryParse(r["porez"].ToString(), out porez);
                decimal.TryParse(r["rabat"].ToString(), out rabat);

                mpc_ukupno = mpc * kolicina;
                rabat_iznos = (mpc_ukupno * rabat / 100);

                vpc = mpc / (1 + (porez / 100));
                int i = 1;
                dgw.Rows.Add(i,
                    r["naziv"].ToString(),
                    r["jmj"].ToString(),
                    Math.Round(kolicina, 4).ToString("#0.000"),
                    Math.Round(porez, 2).ToString("#0.00"),
                    Math.Round(vpc, 4).ToString("#0.000"),
                    Math.Round(mpc, 3).ToString("#0.00"),
                    Math.Round(rabat, 3).ToString("#0.00"),
                    Math.Round(rabat_iznos, 3).ToString("#0.00"),
                    Math.Round(mpc_ukupno - rabat_iznos, 3).ToString("#0.00"),
                    r["id"].ToString());
                izracun(0);
                i++;
            }

            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["rabat_iznos"].DefaultCellStyle = style;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle = style;
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

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM potrosni_materijal WHERE godina='" + nmGodina.Value.ToString() + "' AND broj='" + txtBroj.Text + "'", "ifb").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    //if (brojFakture() == ttxBrojFakture.Text.Trim())
                    //{
                    deleteFields();
                    edit = false;
                    EnableDisable(true);
                    btnSveFakture.Enabled = false;
                    //ttxBrojFakture.Text = brojFakture();
                    btnDeleteAllFaktura.Enabled = false;
                    txtSifraOdrediste.Select();
                    txtBroj.ReadOnly = true;
                    nmGodina.ReadOnly = true;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    //}
                }
                else if (DT.Rows.Count > 0)
                {
                    broj_documenta_edit = txtBroj.Text;
                    fillDocumenat();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    txtBroj.ReadOnly = true;
                    nmGodina.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void ttxBrojFakture_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                dgw.Rows.Add(dgw.Rows.Count, "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                RedniBroj();
            }
            else
                if (e.KeyData == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    if (txt.Name == "rtbNapomena")
                    {
                        if (rtbNapomena.Text == "")
                        {
                            dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                            dgw.Select();
                            int br = dgw.Rows.Count - 1;
                            dgw.CurrentCell = dgw.Rows[br].Cells[1];
                            dgw.BeginEdit(true);

                            RedniBroj();
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                e.SuppressKeyPress = true;

                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    if (txt.Name == "txtSifraOdrediste")
                    {
                        if (txtSifraOdrediste.Text == "")
                        {
                            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                            partnerTrazi.ShowDialog();
                            if (Properties.Settings.Default.id_partner != "")
                            {
                                DataSet partner = new DataSet();
                                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                                if (partner.Tables[0].Rows.Count > 0)
                                {
                                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                                    txtSifraOdrediste.Select();
                                    return;
                                }
                            }
                            else
                            {
                                txtSifraOdrediste.Select();
                                return;
                            }
                        }

                        string Str = txtSifraOdrediste.Text.Trim();
                        double Num;
                        bool isNum = double.TryParse(Str, out Num);
                        if (!isNum)
                        {
                            txtSifraOdrediste.Text = "0";
                        }
                        DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        }
                    }
                }

                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            dgw.Rows.Add(dgw.Rows.Count, "", "", "1", "25", "0", "0", "0", "0", "0", "0");
            RedniBroj();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "" && dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "0")
            {
                string sql = "DELETE FROM potrosni_materijal WHERE id='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'";
                classSQL.delete(sql);
            }

            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            RedniBroj();
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                izracun(e.ColumnIndex);
            }
            catch (Exception) { }
        }

        private void RedniBroj()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                dgw.Rows[i].Cells["br"].Value = i + 1;
            }
        }

        private void frmPotrosniMaterijal_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void btnDodajNaPopis_Click(object sender, EventArgs e)
        {//DTptma.Rows[0]["sifra"].ToString(),
            DataTable DTptma = classSQL.select("SELECT * FROM potrosni_materijal_artikli WHERE sifra='" + cbArtikli.SelectedValue.ToString() + "'", "potrosni_materijal_artikli").Tables[0];

            decimal porez, cijena, vpc;
            decimal.TryParse(DTptma.Rows[0]["porez"].ToString(), out porez);
            decimal.TryParse(DTptma.Rows[0]["cijena"].ToString(), out cijena);
            vpc = cijena / (1 + (porez / 100));

            dgw.Rows.Add(dgw.Rows.Count,
                DTptma.Rows[0]["naziv"].ToString(),
                DTptma.Rows[0]["jmj"].ToString(),
                DTptma.Rows[0]["kolicina"].ToString(),
                DTptma.Rows[0]["porez"].ToString(),
                Math.Round(vpc, 2).ToString("#0.00"),
                Math.Round(cijena, 2).ToString("#0.00"),
                "0",
                "0",
                Math.Round(cijena, 2).ToString("#0.00"),
                "0", DTptma.Rows[0]["sifra"].ToString());

            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
            dgw.BeginEdit(true);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
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