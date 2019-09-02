using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmUnosRezervacije : Form
    {
        public frmUnosRezervacije()
        {
            InitializeComponent();
            nuGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
        }

        private string broj_upisa = "";
        private DataTable DTBojeForme;

        public string __OD_datuma { get; set; }
        public string __DO_datuma { get; set; }
        public string __soba { get; set; }

        public string broj_unosa { get; set; }
        public string __godina { get; set; }

        private void frmUnosRezervacije_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetComboBox();
            txtBrojDokumenta.Select();
            broj_upisa = BrojDokumenta();
            txtBrojDokumenta.Text = broj_upisa;
            if (broj_unosa != null) { txtBrojDokumenta.Text = broj_unosa; Fill(broj_unosa); }
            if (__soba != null) { SetNew(); }
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void SetNew()
        {
            dtpDatDolaska.Value = Convert.ToDateTime(__OD_datuma);
            dtpDatOdlaska.Value = Convert.ToDateTime(__DO_datuma);

            try
            {
                nuGodina.Value = Convert.ToInt16(__godina);
                broj_upisa = BrojDokumenta();
                txtBrojDokumenta.Text = broj_upisa;
            }
            catch
            {
            }

            string sql = string.Format(@"select * from sobe where naziv_sobe = '{0}';", __soba);

            DataTable DT = RemoteDB.select(sql, "sobe").Tables[0];

            try
            {
                cbSoba.SelectedValue = DT.Rows[0][0].ToString();
            }
            catch { }
        }

        private void Fill(string broj)
        {
            try
            {
                nuGodina.Value = Convert.ToInt16(__godina);
            }
            catch
            {
            }
            txtBrojDokumenta.Enabled = false;
            string sql = "SELECT * FROM unos_rezervacije WHERE broj='" + broj + "' AND godina='" + __godina + "'";

            DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

            foreach (DataRow dr in DT.Rows)
            {
                try
                {
                    dgv.Rows.Add();
                    int br = dgv.Rows.Count - 1;

                    dgv.Rows[br].Cells["imeprezime"].Value = dr["ime_gosta"].ToString();
                    dgv.Rows[br].Cells["datum_dolaska"].Value = dr["datum_dolaska"].ToString();
                    dgv.Rows[br].Cells["datum_odlaska"].Value = dr["datum_odlaska"].ToString();

                    DataTable DDD = RemoteDB.select("SELECT id,naziv_sobe FROM sobe WHERE id='" + dr["id_soba"].ToString() + "'", "sobe").Tables[0];
                    if (DDD.Rows.Count > 0)
                    {
                        dgv.Rows[br].Cells["soba"].Value = DDD.Rows[0]["naziv_sobe"].ToString();
                    }
                    dgv.Rows[br].Cells["id_unos"].Value = dr["id"].ToString();
                    dgv.Rows[br].Cells["avans"].Value = dr["avans"].ToString();
                    txtBrojDokumenta.Text = dr["broj"].ToString();
                    nuGodina.Value = Convert.ToInt16(dr["godina"].ToString());
                    dgv.Rows[br].Cells["id_drzava"].Value = dr["id_drzava"].ToString();
                    dgv.Rows[br].Cells["broj_osobne"].Value = dr["broj_osobne"].ToString();
                    dgv.Rows[br].Cells["broj_putovnice"].Value = dr["broj_putovnice"].ToString();
                    dgv.Rows[br].Cells["id_agencija"].Value = dr["datum_odlaska"].ToString();
                    //dgv.Rows[br].Cells["id_tip_sobe"].Value = dr["id_tip_sobe"].ToString();
                    dgv.Rows[br].Cells["id_vrsta_usluge"].Value = dr["id_vrsta_usluge"].ToString();
                    dgv.Rows[br].Cells["dorucak"].Value = dr["dorucak"].ToString();
                    dgv.Rows[br].Cells["rucak"].Value = dr["rucak"].ToString();
                    dgv.Rows[br].Cells["vecera"].Value = dr["vecera"].ToString();
                    dgv.Rows[br].Cells["napomena"].Value = dr["napomena"].ToString();
                    dgv.Rows[br].Cells["odrasli"].Value = dr["odrasli"].ToString();
                    dgv.Rows[br].Cells["djeca"].Value = dr["djeca"].ToString();
                    dgv.Rows[br].Cells["bebe"].Value = dr["bebe"].ToString();
                    dgv.Rows[br].Cells["id_agencija"].Value = dr["id_agencija"].ToString();
                    dgv.Rows[br].Cells["id_vrsta_gosta"].Value = dr["id_vrsta_gosta"].ToString();
                    dgv.Rows[br].Cells["id_soba"].Value = dr["id_soba"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška:\r\n\r\n" + ex.ToString(), "Greška");
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string BrojDokumenta()
        {
            DataTable DSbr = RemoteDB.select("SELECT MAX(broj) FROM unos_rezervacije WHERE godina='" + nuGodina.Value.ToString() + "'", "unos_rezervacije").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void SetComboBox()
        {
            txtDjeca.Text = "0";
            txtOdrasli.Text = "0";
            txtBebe.Text = "0";
            txtAvans.Text = "0";

            DataTable DTvrsta_gosta = classDBlite.LiteSelect("SELECT * FROM vrsta_gosta", "vrsta_gosta").Tables[0];
            cbVrsteGosta.DataSource = DTvrsta_gosta;
            cbVrsteGosta.DisplayMember = "vrsta_gosta";
            cbVrsteGosta.ValueMember = "id";

            DataTable DTgencija = RemoteDB.select("SELECT * FROM agencija", "agencija").Tables[0];
            cbAgencija.DataSource = DTgencija;
            cbAgencija.DisplayMember = "ime_agencije";
            cbAgencija.ValueMember = "id";

            DataTable DTtip_sobe = RemoteDB.select("SELECT * FROM tip_sobe", "tip_sobe").Tables[0];
            cbTipSoba.DataSource = DTtip_sobe;
            cbTipSoba.DisplayMember = "tip";
            cbTipSoba.ValueMember = "id";

            DataTable DTdrzava = RemoteDB.select("SELECT * FROM zemlja", "zemlja").Tables[0];
            cbDrzava.DataSource = DTdrzava;
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "country_code";
            cbDrzava.SelectedValue = "HR";

            DataSet DSs = RemoteDB.select("SELECT id,broj_sobe, naziv_sobe FROM sobe", "sobe");
            DataTable DTsoba = DSs.Tables[0];
            cbSoba.DataSource = DTsoba;
            cbSoba.DisplayMember = "naziv_sobe";
            cbSoba.ValueMember = "id";
            txtBrojSobe.DataBindings.Add("Text", DSs.Tables[0], "broj_sobe");

            DataTable DTvrsta_usluge = RemoteDB.select("SELECT * FROM vrsta_usluge", "vrsta_usluge").Tables[0];
            cbVrstaUsluge.DataSource = DTvrsta_usluge;
            cbVrstaUsluge.DisplayMember = "naziv_usluge";
            cbVrstaUsluge.ValueMember = "id";

            nuGodina.Value = DateTime.Now.Year;
        }

        //bool provjera_rezervacije()
        //{
        //    string sql = "SELECT FROM unos_rezervacije WHERE ";

        //    DataTable DT = RemoteDB.select(sql, "unos_rezervacije").Tables[0];

        //    return true;
        //}

        private void btnDodajNaPopis_Click(object sender, EventArgs e)
        {
            int dorucak = 0;
            int rucak = 0;
            int vecera = 0;
            if (chbDorucak.Checked) { dorucak = 1; }
            if (chbRucak.Checked) { rucak = 1; }
            if (chbVecera.Checked) { vecera = 1; }

            //if (provjera_rezervacije()) { MessageBox.Show("Na ovome datumu već postoji rezervacija."); return; }

            int dec = 0;
            if (!int.TryParse(txtDjeca.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno broj djece.");
                return;
            }

            if (!int.TryParse(txtOdrasli.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno broj odraslih gostiju.");
                return;
            }

            if (!int.TryParse(txtBebe.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno broj beba.");
                return;
            }

            if (String.IsNullOrEmpty(txtImePrezime.Text))
            {
                if (MessageBox.Show("Dali ste sigurni da ne želite upisati ime ili prezime gosta?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            if (String.IsNullOrEmpty(txtBrojPutovnice.Text) && String.IsNullOrEmpty(txtBrojOsobne.Text))
            {
                //MessageBox.Show("Za spremanje novog gosta obavezno upišite broj osobne ili broj putovnice!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            if (!ProvjeraDatuma(cbSoba.Text)) { return; }

            dgv.Rows.Add(
                txtImePrezime.Text,
                dtpDatDolaska.Value,
                dtpDatOdlaska.Value,
                cbSoba.Text,
                txtAvans.Text,
                cbVrsteGosta.SelectedValue,
                cbDrzava.SelectedValue,
                txtBrojOsobne.Text,
                txtBrojPutovnice.Text,
                cbAgencija.SelectedValue,
                cbSoba.SelectedValue,
                cbTipSoba.SelectedValue,
                cbVrstaUsluge.SelectedValue,
                dorucak,
                rucak,
                vecera,
                txtNapomena.Text,
                txtOdrasli.Text,
                txtDjeca.Text,
                txtBebe.Text
                );
        }

        private bool ProvjeraDatuma(string soba)
        {
            int dana = Funkcije.ReturnDaysFromDate(dtpDatDolaska.Value, dtpDatOdlaska.Value);
            string poruka = "";
            DateTime dat = dtpDatDolaska.Value;
            for (int i = 0; i < dana + 1; i++)
            {
                string sql = "SELECT * FROM unos_rezervacije " +
                    " WHERE id_soba='" + cbSoba.SelectedValue + "'" +
                    " AND '" + dat.ToString("yyyy-MM-dd") + " 23:59:59" + "'>=unos_rezervacije.datum_dolaska " +
                    " AND '" + dat.ToString("yyyy-MM-dd") + " 00:00:01" + "'<=unos_rezervacije.datum_odlaska";
                DataTable DT = RemoteDB.select(sql, "r_cijenasoba").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    poruka += dat.ToString("yyyy-MM-dd") + "\r\n";
                }
                dat = dat.AddDays(1);
            }

            if (poruka != "")
            {
                if (MessageBox.Show("Upozorenje.\r\nSoba pod nazivom " + soba + " već ima rezervacije po datumima:\r\n" + poruka + "\r\n\r\nŽelite li uistinu dodati na popis ovaj unos?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate nijednu stavku za spremiti.", "Upozorenje");
            }

            foreach (DataGridViewRow row in this.dgv.Rows)
            {
                if (row.Cells["id_unos"].FormattedValue.ToString() == "")
                {
                    string sql = "INSERT INTO unos_rezervacije (id_vrsta_gosta,godina,broj,ime_gosta,broj_osobne,broj_putovnice,datum_dolaska,datum_odlaska,id_agencija,id_soba," +
                        "id_vrsta_usluge,id_drzava,avans,dorucak,rucak,vecera,napomena,vrijeme_unosa,odrasli,djeca,bebe) VALUES (" +
                        "@id_vrsta_gosta,@godina,@broj,@ime_gosta,@broj_osobne,@broj_putovnice,@datum_dolaska,@datum_odlaska,@id_agencija,@id_soba," +
                        "@id_vrsta_usluge,@id_drzava,@avans,@dorucak,@rucak,@vecera,@napomena,@vrijeme_unosa,@odrasli,@djeca,@bebe)";

                    if (RemoteDB.remoteConnection.State.ToString() == "Closed") { RemoteDB.remoteConnection.Open(); }
                    NpgsqlCommand command = RemoteDB.remoteConnection.CreateCommand();

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@godina", nuGodina.Value);
                    command.Parameters.AddWithValue("@broj", txtBrojDokumenta.Text);
                    command.Parameters.AddWithValue("@ime_gosta", row.Cells["imeprezime"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_osobne", row.Cells["broj_osobne"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_putovnice", row.Cells["broj_putovnice"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_dolaska", row.Cells["datum_dolaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_odlaska", row.Cells["datum_odlaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_agencija", row.Cells["id_agencija"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_soba", row.Cells["id_soba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_vrsta_usluge", row.Cells["id_vrsta_usluge"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_drzava", row.Cells["id_drzava"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vrijeme_unosa", DateTime.Now);
                    command.Parameters.AddWithValue("@avans", row.Cells["avans"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@dorucak", row.Cells["dorucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@rucak", row.Cells["rucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vecera", row.Cells["vecera"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@napomena", row.Cells["napomena"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@odrasli", row.Cells["odrasli"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@djeca", row.Cells["djeca"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@bebe", row.Cells["bebe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_vrsta_gosta", row.Cells["id_vrsta_gosta"].FormattedValue.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    string sql = "UPDATE unos_rezervacije SET id_vrsta_gosta=@id_vrsta_gosta,godina=@godina,broj=@broj,ime_gosta=@ime_gosta," +
                        " broj_osobne=@broj_osobne,broj_putovnice=@broj_putovnice,datum_dolaska=@datum_dolaska,datum_odlaska=@datum_odlaska,id_agencija=@id_agencija,id_soba=@id_soba," +
                        " id_vrsta_usluge=@id_vrsta_usluge,id_drzava=@id_drzava,avans=@avans,dorucak=@dorucak,rucak=@rucak,vecera=@vecera," +
                        "napomena=@napomena,vrijeme_unosa=@vrijeme_unosa,odrasli=@odrasli,djeca=@djeca,bebe=@bebe WHERE id=@id";

                    if (RemoteDB.remoteConnection.State.ToString() == "Closed") { RemoteDB.remoteConnection.Open(); }
                    NpgsqlCommand command = RemoteDB.remoteConnection.CreateCommand();

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@godina", nuGodina.Value);
                    command.Parameters.AddWithValue("@broj", txtBrojDokumenta.Text);
                    command.Parameters.AddWithValue("@ime_gosta", row.Cells["imeprezime"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_osobne", row.Cells["broj_osobne"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_putovnice", row.Cells["broj_putovnice"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_dolaska", row.Cells["datum_dolaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_odlaska", row.Cells["datum_odlaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_agencija", row.Cells["id_agencija"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_soba", row.Cells["id_soba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_vrsta_usluge", row.Cells["id_vrsta_usluge"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_drzava", row.Cells["id_drzava"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vrijeme_unosa", DateTime.Now);
                    command.Parameters.AddWithValue("@avans", row.Cells["avans"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@dorucak", row.Cells["dorucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@rucak", row.Cells["rucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vecera", row.Cells["vecera"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@napomena", row.Cells["napomena"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@odrasli", row.Cells["odrasli"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@djeca", row.Cells["djeca"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@bebe", row.Cells["bebe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_vrsta_gosta", row.Cells["id_vrsta_gosta"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id", row.Cells["id_unos"].FormattedValue.ToString());
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Spremljeno");
            if (dgv.Rows.Count > 0)
                dgv.Rows.Clear();
        }

        private void txtImePrezime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void txtImePrezime_Enter(object sender, EventArgs e)
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

        private void txtImePrezime_Leave(object sender, EventArgs e)
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

        private void btnObrisiOznacenog_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgv.CurrentRow.Cells["id_unos"].FormattedValue.ToString() != "")
                    {
                        RemoteDB.delete("DELETE FROM unos_rezervacije WHERE id='" + dgv.CurrentRow.Cells["id_unos"].FormattedValue.ToString() + "'");
                    }
                    dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forme.sifarnici.frmNovaDrzava nd = new sifarnici.frmNovaDrzava();
            nd.ShowDialog();

            DataTable DTdrzava = RemoteDB.select("SELECT * FROM zemlja", "zemlja").Tables[0];
            cbDrzava.DataSource = DTdrzava;
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "country_code";
            cbDrzava.SelectedValue = "HR";
        }

        private bool editDgv = false;
        private int broj_rowa_dgv_edit = new int();

        private void btnUredi_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Nemate nitijednu stavku.");
                return;
            }

            if (editDgv)
            {
                int b = broj_rowa_dgv_edit;

                dgv.Rows[b].Cells["imeprezime"].Value = txtImePrezime.Text;
                dgv.Rows[b].Cells["datum_dolaska"].Value = dtpDatDolaska.Value;
                dgv.Rows[b].Cells["datum_odlaska"].Value = dtpDatOdlaska.Value;
                dgv.Rows[b].Cells["soba"].Value = cbSoba.Text;
                dgv.Rows[b].Cells["avans"].Value = txtAvans.Text;
                dgv.Rows[b].Cells["broj_osobne"].Value = txtBrojOsobne.Text;
                dgv.Rows[b].Cells["broj_putovnice"].Value = txtBrojPutovnice.Text;
                dgv.Rows[b].Cells["id_agencija"].Value = cbAgencija.SelectedValue;
                dgv.Rows[b].Cells["id_soba"].Value = cbSoba.SelectedValue;
                dgv.Rows[b].Cells["id_tip_sobe"].Value = cbTipSoba.SelectedValue;
                dgv.Rows[b].Cells["id_vrsta_usluge"].Value = cbVrstaUsluge.SelectedValue;

                if (chbDorucak.Checked)
                {
                    dgv.Rows[b].Cells["dorucak"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["dorucak"].Value = "0";
                }
                if (chbRucak.Checked)
                {
                    dgv.Rows[b].Cells["rucak"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["rucak"].Value = "0";
                }
                if (chbVecera.Checked)
                {
                    dgv.Rows[b].Cells["vecera"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["vecera"].Value = "0";
                }

                dgv.Rows[b].Cells["napomena"].Value = txtNapomena.Text;
                dgv.Rows[b].Cells["id_vrsta_gosta"].Value = cbVrsteGosta.SelectedValue;
                dgv.Rows[b].Cells["id_drzava"].Value = cbDrzava.SelectedValue;

                DeleteFields(false);

                btnUredi.Text = "Uredi označenog";
                btnUredi.Font = new Font(btnUredi.Font.Name, 8.25F, FontStyle.Regular);
                broj_rowa_dgv_edit = new int();
                editDgv = false;
                return;
            }

            int br = dgv.CurrentRow.Index;
            broj_rowa_dgv_edit = br;

            txtImePrezime.Text = dgv.Rows[br].Cells["imeprezime"].FormattedValue.ToString();
            dtpDatDolaska.Value = Convert.ToDateTime(dgv.Rows[br].Cells["datum_dolaska"].FormattedValue.ToString());
            dtpDatOdlaska.Value = Convert.ToDateTime(dgv.Rows[br].Cells["datum_odlaska"].FormattedValue.ToString());
            cbSoba.Text = dgv.Rows[br].Cells["soba"].FormattedValue.ToString();
            txtAvans.Text = dgv.Rows[br].Cells["avans"].FormattedValue.ToString();
            txtBrojOsobne.Text = dgv.Rows[br].Cells["broj_osobne"].FormattedValue.ToString();
            txtBrojPutovnice.Text = dgv.Rows[br].Cells["broj_putovnice"].FormattedValue.ToString();
            cbAgencija.SelectedValue = dgv.Rows[br].Cells["id_agencija"].FormattedValue.ToString();
            cbSoba.SelectedValue = dgv.Rows[br].Cells["id_soba"].FormattedValue.ToString();
            //cbTipSoba.SelectedValue = dgv.Rows[br].Cells["id_tip_sobe"].FormattedValue.ToString();
            cbVrstaUsluge.SelectedValue = dgv.Rows[br].Cells["id_vrsta_usluge"].FormattedValue.ToString();

            if (dgv.Rows[br].Cells["dorucak"].FormattedValue.ToString() == "1")
            {
                chbDorucak.Checked = true;
            }
            else
            {
                chbDorucak.Checked = false;
            }
            if (dgv.Rows[br].Cells["rucak"].FormattedValue.ToString() == "1")
            {
                chbRucak.Checked = true;
            }
            else
            {
                chbRucak.Checked = false;
            }
            if (dgv.Rows[br].Cells["vecera"].FormattedValue.ToString() == "1")
            {
                chbVecera.Checked = true;
            }
            else
            {
                chbVecera.Checked = false;
            }

            txtNapomena.Text = dgv.Rows[br].Cells["napomena"].FormattedValue.ToString();
            cbVrsteGosta.SelectedValue = dgv.Rows[br].Cells["id_vrsta_gosta"].FormattedValue.ToString();
            cbDrzava.SelectedValue = dgv.Rows[br].Cells["id_drzava"].FormattedValue.ToString();
            btnUredi.Font = new Font(btnUredi.Font.Name, 9, FontStyle.Bold);
            btnUredi.Text = "Završi uređivanje";
            editDgv = true;
        }

        private void DeleteFields(bool DeleteFrom)
        {
            txtImePrezime.Text = "";
            cbSoba.Text = "";
            txtAvans.Text = "";
            txtBrojOsobne.Text = "";
            txtBrojPutovnice.Text = "";
            txtNapomena.Text = "";

            if (DeleteFrom)
            {
                dgv.Rows.Clear();
            }
        }

        private void nuGodina_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int izlaz;
                if (int.TryParse(__godina, out izlaz))
                {
                    nuGodina.Value = izlaz;
                }
                broj_upisa = BrojDokumenta();
                txtBrojDokumenta.Text = broj_upisa;
            }
            catch
            {
            }
        }
    }
}