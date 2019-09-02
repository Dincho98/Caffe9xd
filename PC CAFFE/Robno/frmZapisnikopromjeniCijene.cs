using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmZapisnikopromjeniCijene : Form
    {
        public frmMenu MainForm { get; set; }

        private DataTable DTRoba = new DataTable();
        private DataTable DTIzradio = new DataTable();
        private DataTable DT_Skladiste = new DataTable();

        public string broj_promjene_edit { get; set; }
        private bool edit = false;

        public frmZapisnikopromjeniCijene()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmZapisnikopromjeniCijene_Load(object sender, EventArgs e)
        {
            EnableDisable(false);
            SetCB();
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtBrojInventure.Text = brojPromjene();
            txtBrojInventure.ReadOnly = false;
            nmGodinaInventure.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaInventure.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaInventure.Value = DateTime.Now.Year;
            if (broj_promjene_edit != null)
            {
                EnableDisable(true);
                DelField();
                ControlDisableEnable(0, 1, 1, 0, 1);
                FillPocetnoStanje(broj_promjene_edit);
            }

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSifra_robe.Text.Length > 0)
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

                string sql = "SELECT roba.*, roba_prodaja.kolicina FROM roba " +
                    "LEFT JOIN roba_prodaja on roba_prodaja.sifra = roba.sifra and roba_prodaja.id_skladiste = '" + cbSkladiste.SelectedValue + "'" +
                    " WHERE roba.sifra='" + txtSifra_robe.Text + "'";

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

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];

            double mpc = Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString());
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["stara_cijena"].Value = mpc;
            dgw.Rows[br].Cells["kolicina"].Value = DTRoba.Rows[0]["kolicina"].ToString();
            dgw.Rows[br].Cells["postotak"].Value = "0";
            dgw.Rows[br].Cells["nova_cijena"].Value = mpc;
            dgw.Rows[br].Cells["pdv"].Value = pdv;
            dgw.Rows[br].Cells["iznos"].Value = mpc;

            dgw.BeginEdit(true);
            PaintRows(dgw);
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select("SELECT ime+' '+prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
            txtIzradio.Text = DTIzradio.Rows[0]["Ime"].ToString();
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void SetRabat()
        {
            int br = dgw.CurrentRow.Index;

            double mpc = Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString());
            double postotak = Convert.ToDouble(dgw.Rows[br].Cells["postotak"].FormattedValue.ToString());
            double kolicina = 1;
            double.TryParse(dgw.Rows[br].Cells["kolicina"].FormattedValue.ToString(), out kolicina);
            dgw.Rows[br].Cells["nova_cijena"].Value = (mpc * postotak / 100) + mpc;
            dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) - Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString()) * kolicina;
        }

        private void SetNovaCijena()
        {
            int br = dgw.CurrentRow.Index;

            double kolicina = 1;
            double.TryParse(dgw.Rows[br].Cells["kolicina"].FormattedValue.ToString(), out kolicina);

            dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) - Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString()) * kolicina;
            dgw.Rows[br].Cells["postotak"].Value = ((Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) / Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString()) - 1) * 100).ToString("#0.00000");
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MouseButtons != 0)
                return;

            if (dgw.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    SetRabat();
                    dgw.CurrentCell = dgw.CurrentRow.Cells[5];
                    dgw.BeginEdit(true);
                }
                catch (Exception) { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    SetRabat();
                    dgw.CurrentCell = dgw.CurrentRow.Cells[6];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                {
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    SetNovaCijena();
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                catch (Exception) { }
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
        }

        private void DelField()
        {
            txtSifra_robe.Text = "";
            dgw.Rows.Clear();
        }

        private void EnableDisable(bool x)
        {
            txtSifra_robe.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            btnObrisi.Enabled = x;

            if (x == true)
            {
                txtBrojInventure.Enabled = false;
                nmGodinaInventure.Enabled = false;
            }
            else if (x == false)
            {
                txtBrojInventure.Enabled = true;
                nmGodinaInventure.Enabled = true;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(false);
            DelField();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private string brojPromjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM promjena_cijene", "promjena_cijene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            broj_promjene_edit = null;
            Robno.frmSvePromjeneCijena sp = new frmSvePromjeneCijena();
            sp.MainForm = this;
            sp.ShowDialog();
            if (broj_promjene_edit != null)
            {
                EnableDisable(true);
                DelField();
                ControlDisableEnable(0, 1, 1, 0, 1);
                FillPocetnoStanje(broj_promjene_edit);
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM promjena_cijene WHERE  broj='" + txtBrojInventure.Text + "'", "pocetno_stanje").Tables[0];
                DelField();
                if (DT.Rows.Count == 0)
                {
                    if (brojPromjene() == txtBrojInventure.Text.Trim())
                    {
                        DelField();
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_promjene_edit = txtBrojInventure.Text;
                    FillPocetnoStanje(broj_promjene_edit);
                    EnableDisable(true);
                    edit = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPocetnoStanje(string broj)
        {
            DataTable DTheader = classSQL.select("SELECT * FROM promjena_cijene WHERE broj='" + broj + "'", "promjena_cijene").Tables[0];

            if (DTheader.Rows.Count > 0)
            {
                txtBrojInventure.Text = broj_promjene_edit;
                cbSkladiste.SelectedValue = DTheader.Rows[0]["id_skladiste"].ToString();
                rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
                dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["date"].ToString());
                txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            }

            string sql = "SELECT promjena_cijene_stavke.sifra," +
                " promjena_cijene_stavke.id_stavka," +
                " promjena_cijene_stavke.stara_cijena," +
                " promjena_cijene_stavke.pdv," +
                " promjena_cijene_stavke.nova_cijena," +
                " promjena_cijene_stavke.postotak," +
                " promjena_cijene_stavke.kolicina," +
                " roba.naziv," +
                " roba.jm " +
                " FROM promjena_cijene_stavke" +
                " LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra WHERE promjena_cijene_stavke.broj='" + broj + "'" +
                "";
            DataTable DT = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["stara_cijena"].Value = DT.Rows[i]["stara_cijena"].ToString();
                dgw.Rows[br].Cells["nova_cijena"].Value = DT.Rows[i]["nova_cijena"].ToString();
                dgw.Rows[br].Cells["postotak"].Value = DT.Rows[i]["postotak"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["pdv"].Value = DT.Rows[i]["pdv"].ToString();
                dgw.Rows[br].Cells["iznos"].Value = (Convert.ToDouble(DT.Rows[i]["nova_cijena"].ToString()) - Convert.ToDouble(DT.Rows[i]["stara_cijena"].ToString())) * Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
            }
            edit = true;
            PaintRows(dgw);
        }

        private void Spremi()
        {
            DataTable DTboll = classSQL.select("SELECT broj FROM promjena_cijene WHERE broj='" + txtBrojInventure.Text + "'", "promjena_cijene").Tables[0];

            if (DTboll.Rows.Count == 0)
            {
                txtBrojInventure.Text = brojPromjene();
                string s = "INSERT INTO promjena_cijene (broj, id_skladiste, date, id_izradio, napomena) VALUES " +
                    "(" +
                    "'" + txtBrojInventure.Text + "'," +
                    "'" + cbSkladiste.SelectedValue + "'," +
                    "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + rtbNapomena.Text + "'" +
                    ")";
                classSQL.insert(s);
            }
            else
            {
                string s = "UPDATE promjena_cijene SET broj='" + txtBrojInventure.Text + "'" +
                ",id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                ",date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                ",napomena='" + rtbNapomena.Text + "'  WHERE broj='" + txtBrojInventure.Text + "'";
                classSQL.update(s);
            }

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double pdv = double.Parse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());
                double stara = double.Parse(dgw.Rows[i].Cells["stara_cijena"].FormattedValue.ToString());
                double nova = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());

                string StrStara = stara.ToString();
                string StrNova = nova.ToString();

                if (classSQL.remoteConnectionString == "")
                {
                    StrStara = StrStara.Replace(",", ".");
                    StrNova = StrNova.Replace(",", ".");
                }
                else
                {
                    StrStara = StrStara.Replace(".", ",");
                    StrNova = StrNova.Replace(".", ",");
                }

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    string s1 = "INSERT INTO promjena_cijene_stavke (stara_cijena,nova_cijena,pdv,sifra,postotak,broj, kolicina) VALUES " +
                        "(" +
                        "'" + StrStara + "'," +
                        "'" + StrNova + "'," +
                        "'" + pdv.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["postotak"].FormattedValue.ToString() + "'," +
                        "'" + txtBrojInventure.Text + "'," +
                        "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString().Replace(',', '.') + "'" +
                        ")";

                    classSQL.insert(s1);

                    string ssss = "UPDATE roba SET " +
                        "mpc='" + nova + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'" +
                        "";

                    classSQL.update(ssss);
                }
                else
                {
                    string s2 = "UPDATE promjena_cijene_stavke SET stara_cijena=" +
                        "'" + StrStara + "'," +
                        " nova_cijena='" + StrNova + "'," +
                        " pdv='" + pdv.ToString() + "'," +
                        " kolicina ='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " postotak='" + dgw.Rows[i].Cells["postotak"].FormattedValue.ToString() + "' WHERE id_stavka = '" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";
                    classSQL.update(s2);

                    string ssss = "UPDATE roba SET " +
                        "mpc='" + nova + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'" +
                        "";
                    classSQL.update(ssss);
                }
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            Spremi();

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double pdv = double.Parse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());
                double vpc = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());
                vpc = vpc / Convert.ToDouble("1," + pdv);

                double mpc = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());

                string StrMpc = mpc.ToString();
                string StrVpc = vpc.ToString();

                if (classSQL.remoteConnectionString == "")
                {
                    StrMpc = StrMpc.Replace(",", ".");
                    StrVpc = StrVpc.Replace(",", ".");
                }
                else
                {
                    StrMpc = StrMpc.Replace(".", ",");
                    StrVpc = StrVpc.Replace(".", ",");
                }

                string sql = "UPDATE roba SET vpc='" + StrVpc.Replace(",", ".") + "',mpc='" + StrMpc + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
            }

            EnableDisable(false);
            edit = false;

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojInventure.Text);
            }

            DelField();
            txtBrojInventure.Text = brojPromjene();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void printaj(string broj)
        {
            Report.Liste.frmListe pr = new Report.Liste.frmListe();
            pr.dokumenat = "promjena_cijene";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Zapisnik o promjeni cijene";
            pr.ShowDialog();
        }

        private void btnOpenRoba_Click_1(object sender, EventArgs e)
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

                string sql = "SELECT roba.*, roba_prodaja.kolicina FROM roba " +
                    "LEFT JOIN roba_prodaja on roba_prodaja.sifra = roba.sifra and roba_prodaja.id_skladiste = '" + cbSkladiste.SelectedValue + "'" +
                    " WHERE roba.sifra='" + propertis_sifra + "'";

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