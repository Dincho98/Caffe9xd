using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNormativi : Form
    {
        public string broj_normativa_edit { get; set; }

        public frmNormativi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private bool edit = false;
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataTable DTpostavke = new DataTable();
        public frmMenu MainForm { get; set; }

        private void frmNormativi_Load(object sender, EventArgs e)
        {
            txtBrojNormativa.Text = brojNormativa();
            txtSifraArtikla.Select();
            numeric();
            EnableDisable(false);
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";

            txtBrojNormativa.Select();
            ControlDisableEnable(1, 0, 0, 1, 0);

            if (broj_normativa_edit != null) { fillNormativ(); }
            PaintRows(dgw);
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void numeric()
        {
            nuGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodina.Value = 2012;
        }

        private string brojNormativa()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_normativa) FROM normativi", "normativi").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().TrimEnd())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovom normativu", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();

                    txtBrojNormativa.Enabled = false;
                    nuGodina.Enabled = false;
                    txtSifraArtikla.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[1];
            dgw.BeginEdit(true);

            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["jmj"].Value = String.Format(DTRoba.Rows[0]["jm"].ToString());
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

            PaintRows(dgw);
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DSpar = classSQL.select("SELECT roba.naziv,roba.jm,grupa.grupa FROM roba INNER JOIN grupa ON roba.id_grupa=grupa.id_grupa WHERE sifra='" + txtSifraArtikla.Text + "'", "roba").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtImeArtikla.Text = DSpar.Rows[0]["naziv"].ToString();
                    txtJedinicaMjere.Text = DSpar.Rows[0]["jm"].ToString();
                    txtVrstaRobe.Text = DSpar.Rows[0]["grupa"].ToString();
                    txtKomentar.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnArtikli_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                    txtJedinicaMjere.Text = DTRoba.Rows[0]["jm"].ToString();
                    txtKomentar.Select();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtKomentar.Select();
            }
        }

        private void txtKomentar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifra_robe.Select();
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                    txtBrojNormativa.Enabled = false;
                    nuGodina.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EnableDisable(bool x)
        {
            txtSifra_robe.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            txtSifraArtikla.Enabled = x;
            txtKomentar.Enabled = x;
            btnSpremi.Enabled = x;

            if (x == true)
            {
                nuGodina.Enabled = false;
                txtBrojNormativa.Enabled = false;
            }
            else
            {
                nuGodina.Enabled = true;
                txtBrojNormativa.Enabled = true;
            }
        }

        private void deleteFields()
        {
            //txtBrojNormativa.Text = brojNormativa();
            txtSifraArtikla.Text = "";
            txtSifra_robe.Text = "";
            txtKomentar.Text = "";
            txtImeArtikla.Text = "";
            txtJedinicaMjere.Text = "";
            txtVrstaRobe.Text = "";

            dgw.Rows.Clear();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null)
                {
                    MessageBox.Show("Faktura nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (edit == true)
            {
                UPDATEnormativ();
                EnableDisable(false);
                deleteFields();
                btnSve.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string sql = "INSERT INTO normativi (broj_normativa,sifra_artikla,komentar,godina_normativa,id_zaposlenik) VALUES " +
                " (" +
                 " '" + txtBrojNormativa.Text + "'," +
                " '" + txtSifraArtikla.Text + "'," +
                " '" + txtKomentar.Text + "'," +
                " '" + nuGodina.Value.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sql_stavke = "";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                //ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value, i);

                sql_stavke = "INSERT INTO normativi_stavke " +
                "(sifra_robe,kolicina,id_skladiste,broj_normativa)" +
                "VALUES" +
                "(" +
                "'" + dg(i, "sifra") + "'," +
                "'" + dg(i, "kolicina") + "'," +
                "'" + dgw.Rows[i].Cells[1].Value + "'," +
                "'" + txtBrojNormativa.Text.Trim() + "'" +
                ")";
                provjera_sql(classSQL.insert(sql_stavke));
            }

            MessageBox.Show("Spremljeno");
            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada novog normativa br." + txtBrojNormativa.Text + "')");

            ControlDisableEnable(1, 0, 0, 1, 0);
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSve.Enabled = true;
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSve.Enabled = true;
            EnableDisable(false);
            deleteFields();
            txtBrojNormativa.Text = brojNormativa();
            edit = false;
            btnDeleteAll.Enabled = false;
            txtSifraArtikla.ReadOnly = false;
            txtBrojNormativa.ReadOnly = false;
            nuGodina.ReadOnly = false;
            txtBrojNormativa.Enabled = true;
            nuGodina.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            btnSve.Enabled = false;
            txtBrojNormativa.Text = brojNormativa();
            btnDeleteAll.Enabled = false;
            txtSifraArtikla.ReadOnly = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnSviRN_Click(object sender, EventArgs e)
        {
            frmSviNormativi sf = new frmSviNormativi();
            sf.sifra_normativa = "";
            sf.MainForm = this;
            sf.ShowDialog();
            if (broj_normativa_edit != null)
            {
                dgw.Rows.Clear();
                fillNormativ();
                EnableDisable(true);
                edit = true;
                sf.Enabled = true;
                txtSifraArtikla.ReadOnly = true;
                txtBrojNormativa.ReadOnly = true;
                nuGodina.ReadOnly = true;
            }
        }

        private void UPDATEnormativ()
        {
            string sql = "UPDATE normativi SET " +
                " broj_normativa='" + txtBrojNormativa.Text.Trim() + "'," +
                " sifra_artikla='" + txtSifraArtikla.Text + "'," +
                " komentar='" + txtKomentar.Text + "'," +
                " godina_normativa='" + nuGodina.Value.ToString() + "'" +
                " WHERE broj_normativa='" + txtBrojNormativa.Text.Trim() + "'";
            provjera_sql(classSQL.update(sql));

            string sql_stavke;
            for (int b = 0; b < dgw.Rows.Count; b++)
            {
                if (dgw.Rows[b].Cells["id_stavka"].Value != null)
                {
                    sql = "UPDATE normativi_stavke SET " +
                    " sifra_robe='" + dg(b, "sifra") + "'," +
                    " kolicina='" + dg(b, "kolicina") + "'," +
                    " id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value + "'" +
                    " WHERE id_stavka='" + dg(b, "id_stavka") + "'";
                    provjera_sql(classSQL.update(sql));
                }
                else
                {
                    sql_stavke = "INSERT INTO normativi_stavke " +
                    "(sifra_robe,kolicina,id_skladiste,broj_normativa)" +
                    "VALUES" +
                    "(" +
                    "'" + dg(b, "sifra") + "'," +
                    "'" + dg(b, "kolicina") + "'," +
                    "'" + dgw.Rows[b].Cells[1].Value + "'," +
                    "'" + txtBrojNormativa.Text.Trim() + "'" +
                    ")";
                    provjera_sql(classSQL.insert(sql_stavke));
                }
            }

            MessageBox.Show("Spremljeno");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSve.Enabled = true;

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje ponude br." + txtBrojNormativa.Text + "')");
        }

        private DataTable DSnormativi = new DataTable();

        private void fillNormativ()
        {
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            //fill header

            DSnormativi = classSQL.select("SELECT * FROM normativi WHERE broj_normativa = '" + broj_normativa_edit + "'", "fakture").Tables[0];
            txtBrojNormativa.Text = DSnormativi.Rows[0]["broj_normativa"].ToString();
            nuGodina.Value = Convert.ToInt16(DSnormativi.Rows[0]["godina_normativa"].ToString());
            txtSifraArtikla.Text = DSnormativi.Rows[0]["sifra_artikla"].ToString();

            DataTable DSoArtiklu = new DataTable();
            string ss = "SELECT roba.naziv,roba.jm,grupa.grupa FROM roba INNER JOIN grupa ON roba.id_grupa=grupa.id_grupa WHERE sifra ='" + DSnormativi.Rows[0]["sifra_artikla"].ToString() + "'";
            DSoArtiklu = classSQL.select(ss, "roba").Tables[0];
            txtJedinicaMjere.Text = DSoArtiklu.Rows[0]["jm"].ToString();
            txtImeArtikla.Text = DSoArtiklu.Rows[0]["naziv"].ToString();
            txtVrstaRobe.Text = DSoArtiklu.Rows[0]["grupa"].ToString();
            txtKomentar.Text = DSnormativi.Rows[0]["komentar"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSnormativi.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            //--------fill normativ stavke------------------------------

            DataTable dtR = new DataTable();
            dtR = classSQL.select("SELECT normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste,normativi_stavke.id_stavka," +
                "roba.naziv,roba.jm FROM normativi_stavke " +
                " INNER JOIN roba ON roba.sifra = normativi_stavke.sifra_robe WHERE broj_normativa  = '" + DSnormativi.Rows[0]["broj_normativa"].ToString() + "'", "broj_ponude").Tables[0];

            for (int i = 0; i < dtR.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[i]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = dtR.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = dtR.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[i]["jm"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = dtR.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[1];
                PaintRows(dgw);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue != null)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM normativi_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'"));
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa normativa br." + txtBrojNormativa.Text + "')"));
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllRN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM normativi_stavke WHERE broj_normativa='" + txtBrojNormativa.Text + "'");
                classSQL.delete("DELETE FROM normativi WHERE broj_normativa='" + txtBrojNormativa.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijelog normativa br." + txtBrojNormativa.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAll.Enabled = false;
                btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
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
                btnSve.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSve.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAll.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAll.Enabled = true;
            }
        }

        private void txtBrojNormativa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_normativa FROM normativi WHERE  broj_normativa='" + txtBrojNormativa.Text + "'", "normativi").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojNormativa() == txtBrojNormativa.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        txtBrojNormativa.Text = brojNormativa();
                        txtBrojNormativa.ReadOnly = true;
                        txtBrojNormativa.ReadOnly = true;
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                        return;
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_normativa_edit = txtBrojNormativa.Text;
                    fillNormativ();
                    EnableDisable(true);
                    edit = true;
                    txtBrojNormativa.ReadOnly = true;
                    nuGodina.ReadOnly = true;
                }
                txtSifraArtikla.Select();
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MouseButtons != 0)
                return;

            if (dgw.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[3];
                    dgw.BeginEdit(true);
                }
                catch (Exception) { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    txtSifra_robe.Text = "";
                    txtSifra_robe.BackColor = Color.Silver;
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                catch (Exception) { }
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
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