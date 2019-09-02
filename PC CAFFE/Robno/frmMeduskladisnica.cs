using PCPOS.Sinkronizacija;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmMeduskladisnica : Form
    {
        public frmMeduskladisnica()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string skladiste_pocetno = "";
        private bool edit = false;
        private DataTable DT_Skladiste;
        public string broj_ms_edit { get; set; }
        public string _godina_edit { get; set; }
        public string _iz_poslovnice { get; set; }

        public frmMenu MainForm { get; set; }
        private string poslovnica = "1";

        private void frmMeduskladisnica_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            txtBroj.Text = UzmiBroj();
            ControlDisableEnable(1, 0, 0, 1, 0);

            if (broj_ms_edit != null) { FillDoc(); }
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DataTable DTposlovnica = classSQL.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            SetSkladiste();
        }

        private void SetSkladiste()
        {
            DataTable DT = classSQL.select("SELECT * FROM poslovnice", "poslovnice").Tables[0];
            if (DT.Rows.Count == 0)
            {
                synPoslovnice Poslovnice = new synPoslovnice(true);
                Poslovnice.Send();
            }

            DataTable DT_poslovnica = classSQL.select("SELECT * FROM poslovnice", "poslovnice").Tables[0];
            DataTable DT_poslovnicaU = classSQL.select("SELECT * FROM poslovnice", "poslovnice").Tables[0];

            cbPoslovnica.DataSource = DT_poslovnica;
            cbPoslovnica.DisplayMember = "naziv_poslovnice";
            cbPoslovnica.ValueMember = "fiskalna_oznaka_poslovnice";
            cbPoslovnica.SelectedValue = poslovnica;

            cbPoslovnicaUposlovnicu.DataSource = DT_poslovnicaU;
            cbPoslovnicaUposlovnicu.DisplayMember = "naziv_poslovnice";
            cbPoslovnicaUposlovnicu.ValueMember = "fiskalna_oznaka_poslovnice";

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkl.DataSource = DT_Skladiste;
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";

            nmGodina.Value = DateTime.Now.Year;
        }

        private void FillDoc()
        {
            if (_iz_poslovnice != poslovnica) { MessageBox.Show("Ovu međuskladišnicu ne možete mijenjati jer nije iz vaše poslovnice."); return; }

            EnableDisable(true);
            DeleteFields();
            ControlDisableEnable(0, 1, 0, 0, 1);

            if (_godina_edit == null)
                _godina_edit = nmGodina.Value.ToString();

            string query = "SELECT medu_poslovnice.*,roba_prodaja.naziv FROM medu_poslovnice" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=medu_poslovnice.sifra " +
                " WHERE broj='" + broj_ms_edit + "' AND godina='" + _godina_edit + "' AND iz_poslovnice='" + cbPoslovnica.SelectedValue + "'";
            DataTable DTms = classSQL.select(query, "povrat_robe").Tables[0];
            if (DTms.Rows.Count == 0)
                return;

            int _godina, _broj;
            DateTime dt;

            int.TryParse(DTms.Rows[0]["godina"].ToString(), out _godina);
            int.TryParse(DTms.Rows[0]["broj"].ToString(), out _broj);

            nmGodina.Value = _godina;
            txtBroj.Text = _broj.ToString();

            DateTime.TryParse(DTms.Rows[0]["datum"].ToString(), out dt);
            dtpDatum.Value = dt;

            cbPoslovnicaUposlovnicu.SelectedValue = DTms.Rows[0]["u_poslovnicu"].ToString();
            cbSkl.SelectedValue = DTms.Rows[0]["id_skladiste"].ToString();

            rtbNapomena.Text = DTms.Rows[0]["napomena"].ToString();

            DataTable Dtz = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTms.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0];

            if (Dtz.Rows.Count > 0)
                txtIzradio.Text = Dtz.Rows[0][0].ToString();

            for (int i = 0; i < DTms.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                decimal kolicina, pdv, nbc;

                decimal.TryParse(DTms.Rows[i]["kolicina"].ToString(), out kolicina);
                decimal.TryParse(DTms.Rows[i]["pdv"].ToString(), out pdv);
                decimal.TryParse(DTms.Rows[i]["nbc"].ToString(), out nbc);

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTms.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTms.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = Math.Round(kolicina, 4).ToString("#0.0000");
                dgw.Rows[br].Cells["pdv"].Value = Math.Round(pdv, 2).ToString("#0.00");
                dgw.Rows[br].Cells["id_stavka"].Value = DTms.Rows[i]["id"].ToString();

                dgw.Rows[br].Cells["nbc"].Value = Math.Round(nbc, 3).ToString("#0.00");
                dgw.Rows[br].Cells["ukupno"].Value = Math.Round((nbc * kolicina), 3).ToString("#0.00");
            }

            ControlDisableEnable(0, 1, 1, 0, 1);
            edit = true;
            IzracunUkupno();
        }

        private string UzmiBroj()
        {
            DataTable DSbr = classSQL.select("SELECT COALESCE(MAX(broj),0)zbroj1 FROM medu_poslovnice WHERE iz_poslovnice='" + cbPoslovnica.SelectedValue + "'", "medu_poslovnice").Tables[0];
            if (DSbr.Rows.Count > 0)
            {
                int br = 1;
                int.TryParse(DSbr.Rows[0][0].ToString(), out br);
                return br.ToString();
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
            txtBroj.Text = UzmiBroj();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
            cbPoslovnica.Select();
            IzracunUkupno();
        }

        private void EnableDisable(bool x)
        {
            cbPoslovnicaUposlovnicu.Enabled = x;
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
            txtBroj.Text = UzmiBroj();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            IzracunUkupno();
        }

        private void DeleteFields()
        {
            dtpDatum.Value = DateTime.Now;
            rtbNapomena.Text = "";
            nmGodina.Value = DateTime.Now.Year;
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
            if (!edit)
            {
                DataTable DTbool = classSQL.select("SELECT * FROM medu_poslovnice WHERE broj = '" + txtBroj.Text + "'" +
                " AND godina='" + nmGodina.Value.ToString() + "' AND iz_poslovnice='" + cbPoslovnica.SelectedValue + "'", "medu_poslovnice").Tables[0];
                if (DTbool.Rows.Count > 0)
                {
                    MessageBox.Show("Ovaj broj netko već koristi. Dodijeljen Vam je sljedeči broj.");
                    txtBroj.Text = UzmiBroj();
                }
            }

            string _br = txtBroj.Text;
            string sql = "BEGIN; ";

            foreach (DataGridViewRow row in dgw.Rows)
            {
                decimal nbc, pdv, kol, pnp, pp;

                decimal.TryParse(row.Cells["nbc"].FormattedValue.ToString(), out nbc);
                decimal.TryParse(row.Cells["pdv"].FormattedValue.ToString(), out pdv);
                decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out kol);
                decimal.TryParse(row.Cells["pnp"].FormattedValue.ToString(), out pnp);
                decimal.TryParse(row.Cells["pp"].FormattedValue.ToString(), out pp);

                if (edit)
                {
                    sql += "DELETE FROM medu_poslovnice WHERE broj='" + _br + "' AND sifra='" + row.Cells["sifra"].FormattedValue.ToString() + "' AND godina='" + nmGodina.Value.ToString() + "' AND iz_poslovnice='" + cbPoslovnica.SelectedValue + "'; ";
                }

                sql += "INSERT INTO medu_poslovnice (sifra,nbc,mpc,pdv,kolicina,pnp,pp,id_skladiste,broj,godina," +
                   "datum,iz_poslovnice,u_poslovnicu,id_izradio,napomena,novo_izskl,novo_uskl) VALUES (" +
                        " '" + row.Cells["sifra"].FormattedValue.ToString() + "'," +
                        " '" + Math.Round(nbc, 3).ToString().Replace(",", ".") + "'," +
                        " ROUND((SELECT UzmiProdajnuCijenuPremaRepromaterijalu('" + row.Cells["sifra"].FormattedValue.ToString() + "','" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "')),2)," +
                        " '" + Math.Round(pdv, 2).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(kol, 4).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(pnp, 2).ToString().Replace(",", ".") + "'," +
                        " '" + Math.Round(pp, 2).ToString().Replace(",", ".") + "'," +
                        " '" + cbSkl.SelectedValue.ToString() + "'," +
                        " '" + _br + "'," +
                        " '" + nmGodina.Value.ToString() + "'," +
                        " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        " '" + cbPoslovnica.SelectedValue.ToString() + "'," +
                        " '" + cbPoslovnicaUposlovnicu.SelectedValue.ToString() + "'," +
                        " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                        " '" + rtbNapomena.Text + "'," +
                        " '1'," +
                        " '1'" +
                        ");";
            }

            sql += " COMMIT;";
            provjera_sql(classSQL.insert(sql));

            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = UzmiBroj();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno");
            IzracunUkupno();
        }

        private void IzracunUkupno()
        {
            decimal u = 0, m = 0, k = 0;
            foreach (DataGridViewRow row in dgw.Rows)
            {
                decimal.TryParse(row.Cells["nbc"].FormattedValue.ToString(), out m);
                decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out k);

                row.Cells["ukupno"].Value = Math.Round((m * k), 3).ToString("#0.00");
                u += (m * k);
            }

            lblUkupno.Text = "UKUPNO: " + Math.Round(u, 3).ToString("N2") + " kn";
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

                DataTable DT = classSQL.select("SELECT broj FROM medu_poslovnice WHERE  broj='" + txtBroj.Text + "' AND godina='" + nmGodina.Value.ToString() + "' AND iz_poslovnice='" + cbPoslovnica.SelectedValue + "'", "povrat_robe").Tables[0];
                DeleteFields();
                if (DT.Rows.Count == 0)
                {
                    _iz_poslovnice = cbPoslovnica.SelectedValue.ToString();
                    if (UzmiBroj() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbPoslovnica.Select();
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count > 0)
                {
                    _iz_poslovnice = cbPoslovnica.SelectedValue.ToString();
                    if (Until.classZakljucavanjeDokumenta.isLockMeduskladisnica(Convert.ToInt32(txtBroj.Text), _iz_poslovnice, DateTime.Now.Year))
                    {
                        MessageBox.Show("Međuskladišnica je zaključana. Uređivanje nije dopušteno.");
                        return;
                    }
                    broj_ms_edit = txtBroj.Text;
                    FillDoc();
                    EnableDisable(true);
                    edit = true;
                    cbPoslovnica.Select();
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
            double nbc = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString());
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["ulazni_porez"].ToString());

            decimal pnp, pp;
            decimal.TryParse(DTRoba.Rows[0]["porez_potrosnja"].ToString(), out pnp);
            decimal.TryParse(DTRoba.Rows[0]["povratna_naknada"].ToString(), out pp);

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["nbc"].Value = nbc.ToString("#0.00");
            dgw.Rows[br].Cells["pdv"].Value = pdv;
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["ukupno"].Value = nbc.ToString("#0.00");

            dgw.Rows[br].Cells["pnp"].Value = pnp.ToString("#0.00");
            dgw.Rows[br].Cells["pp"].Value = pp.ToString("#0.00");

            //SetSkladiste();
            dgw.BeginEdit(true);

            IzracunUkupno();
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

                string sql = "SELECT * FROM roba_prodaja WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Robno.frmSveMS sp = new Robno.frmSveMS();
            sp.sifra = "";
            sp.MainForm = this;
            sp.ShowDialog();

            this.TopMost = true;
            if (broj_ms_edit != null)
            {
                DeleteFields();
                FillDoc();
            }
            IzracunUkupno();
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ovog otpisa vračate i količinu robe na skladišta. Dali ste sigurni da želite obrisai ovaj otpis?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.update("UPDATE medu_poslovnice SET novo_izskl='1',kolicina='0' WHERE broj='" + txtBroj.Text + "' AND godina='" + nmGodina.Value.ToString() + "' AND iz_poslovnice='" + cbPoslovnica.SelectedValue + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele medu_poslovnice br." + txtBroj.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                DeleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
            IzracunUkupno();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }

            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            MessageBox.Show("Obrisano.");
            IzracunUkupno();
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljdeču kolonu.");
                }

                IzracunUkupno();
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
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false);
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