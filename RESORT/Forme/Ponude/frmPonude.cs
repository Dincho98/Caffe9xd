using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace RESORT.Ponude.Forme
{
    public partial class frmPonude : Form
    {
        public string broj_fakture_edit { get; set; }

        public frmPonude()
        {
            InitializeComponent();
        }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DSfakture = new DataTable();
        private DataTable DTpromocije1;
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        public frmPonude MainForm { get; set; }

        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;

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

        private void RacunajUkupno()
        {
            UKUPNO_FAKTURA = 0;
            foreach (DataGridViewRow row in dgw.Rows)
            {
                try
                {
                    UKUPNO_FAKTURA = Convert.ToDecimal(row.Cells["iznos_ukupno"].FormattedValue.ToString()) + UKUPNO_FAKTURA;
                    lblUkupno.Text = "Ukupno: " + UKUPNO_FAKTURA.ToString("#0.00") + " kn";
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable DTBojeForme;
        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            PaintRows(dgw);
            numeric();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            //DTpromocije1 = RemoteDB.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            fillComboBox();
            ttxBrojFakture.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_fakture_edit != null) { fillFaktute(); }
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmPonude MainForm { get; set; }

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
                    MainForm.dgw.Rows.Add("", "", DateTime.Now.ToString(), DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "10", "0");
                    MainForm.RedniBroj();
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
                int curent = d.CurrentRow.Index;
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 2)
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
            //else if (d.CurrentCell.ColumnIndex == 6)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
            //    d.BeginEdit(true);
            //}
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
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
                int curent = d.CurrentRow.Index;
            }
        }

        private void RightDGW(DataGridView d)
        {
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            //else if (d.CurrentCell.ColumnIndex == 7)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
            //    d.BeginEdit(true);
            //}
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                int curent = d.CurrentRow.Index;
            }
        }

        private void UpDGW(DataGridView d)
        {
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
                btnNoviUnos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
                btnNoviUnos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
                btnOdustani.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
                btnOdustani.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
                btnSpremi.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
                btnSpremi.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
                btnSveFakture.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
                btnSveFakture.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            }

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
                btnDeleteAllFaktura.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
                btnDeleteAllFaktura.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            try
            {
                //fill ziroracun
                DS_ZiroRacun = RemoteDB.select("SELECT * FROM ziro_racun", "ziro_racun");
                cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
                cbZiroRacun.DisplayMember = "ziroracun";
                cbZiroRacun.ValueMember = "id_ziroracun";
                cbZiroRacun.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //fill cbPDV
            //DataSet DS_porez = RemoteDB.select("SELECT * FROM porezi", "porezi");
            //cbPDV.DataSource = DS_porez.Tables[0];
            //cbPDV.DisplayMember = "naziv";
            //cbPDV.ValueMember = "iznos";

            try
            {
                //fill nacin_placanja
                DSnazivPlacanja = RemoteDB.select("SELECT * FROM nacin_placanja", "nacin_placanja");
                cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
                cbNacinPlacanja.DisplayMember = "naziv_placanja";
                cbNacinPlacanja.ValueMember = "id_placanje";
                cbNacinPlacanja.SelectedValue = 3;
                txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                //DS Valuta
                DSValuta = RemoteDB.select("SELECT * FROM valute", "valute");
                cbValuta.DataSource = DSValuta.Tables[0];
                cbValuta.DisplayMember = "ime_valute";
                cbValuta.ValueMember = "id_valuta";
                cbValuta.SelectedValue = 5;
                txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
                cbValuta.SelectedValue = "1";
                //txtTecaj.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            try
            {
                DS_Skladiste = RemoteDB.select("SELECT * FROM skladiste", "skladiste");
                //DTSK.Rows.Add(0,"");
                for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
                {
                    DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //fill tko je prijavljen
            try
            {
                txtIzradio.Text = RemoteDB.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + RESORT.Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DS_Skladiste = RemoteDB.select("SELECT * FROM skladiste", "skladiste");
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = 2012;
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                UKUPNO_FAKTURA = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    UKUPNO_FAKTURA = Convert.ToDecimal(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
                }
                RacunajUkupno();
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            RESORT.Forme.sifarnici.frmPartnerTrazi partnerTrazi = new RESORT.Forme.sifarnici.frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (RESORT.Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = RemoteDB.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + RESORT.Properties.Settings.Default.id_partner + "'", "partners");
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
            if (edit)
                if (MessageBox.Show("Dali ste sigurni da želite obrisai ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    RemoteDB.delete("DELETE FROM rfaktura_stavke WHERE broj='" + ttxBrojFakture.Text + "'");
                    RemoteDB.delete("DELETE FROM rfakture WHERE broj='" + ttxBrojFakture.Text + "'");
                    RemoteDB.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + RESORT.Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele fakture br." + ttxBrojFakture.Text + "')");
                    MessageBox.Show("Obrisano.");

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        RemoteDB.update("UPDATE unos_gosta SET odjava='0' WHERE id='" + dgw.Rows[i].Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                    }

                    izracun();
                    edit = false;
                    EnableDisable(false);
                    deleteFields();
                    ControlDisableEnable(1, 0, 0, 1, 0);
                }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojFakture.Text = brojFakture();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ttxBrojFakture.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void EnableDisable(bool x)
        {
            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtSifraNacinPlacanja.Enabled = x;
            txtModel.Enabled = x;
            rtbNapomena.Enabled = x;
            btnPartner.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbZiroRacun.Enabled = x;
            cbValuta.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            txtModel.Text = "";
            rtbNapomena.Text = "";
            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            ttxBrojFakture.ReadOnly = true;
            nmGodinaFakture.ReadOnly = true;
            txtSifraOdrediste.Select();
        }

        private string brojFakture()
        {
            try
            {
                DataTable DSbr = RemoteDB.select("SELECT MAX(broj) FROM Rponude", "Rponude").Tables[0];
                if (DSbr.Rows[0][0].ToString() != "")
                {
                    return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception)
            {
                return "1";
            }
        }

        private decimal UKUPNO_FAKTURA = 0;

        private decimal _pdv = 0;
        private decimal _cijena_sobe = 0;
        private decimal _osnovica = 0;
        private decimal _bor_pristojba = 0;
        private decimal _broj_nocenja = 0;
        private decimal _rabat = 0;
        private decimal _usluga = 0;

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            izracun();

            //if (DTpdv.Columns["stopa"] == null)
            //{
            //    DTpdv.Columns.Add("stopa");
            //    DTpdv.Columns.Add("iznos");
            //    DTpdv.Columns.Add("osnovica");
            //    DTpdv.Columns.Add("boravisna_pristojba");
            //}
            //else
            //{
            //    DTpdv.Clear();
            //}

            decimal decP;
            if (!Decimal.TryParse(txtSifraOdrediste.Text, out decP))
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtSifraNacinPlacanja.Text, out decP))
            {
                MessageBox.Show("Greška kod upisa načina plaćanja.", "Greška");
                return;
            }

            DateTime datumRacuna = dtpDatum.Value;

            if (edit == false)
            {
                string sql = "INSERT INTO Rponude (" +
                    " broj," +
                    " godina," +
                    " datumDVO," +
                    " datum," +
                    " datum_valute," +
                    " valuta," +
                    " id_valuta," +
                    " nacin_placanja," +
                    " id_partner," +
                    " id_izradio," +
                    " model," +
                    " napomena," +
                    " ukupno" +
                    ") VALUES (" +
                    "'" + ttxBrojFakture.Text + "'," +
                    "'" + nmGodinaFakture.Value + "'," +
                    "'" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + datumRacuna.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + txtTecaj.Text.Replace(",", ".") + "'," +
                    "'" + cbValuta.SelectedValue + "'," +
                    "'" + cbNacinPlacanja.SelectedValue + "'," +
                    "'" + txtSifraOdrediste.Text + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + txtModel.Text + "'," +
                    "'" + rtbNapomena.Text + "'," +
                    "'" + UKUPNO_FAKTURA.ToString("#0.00").Replace(",", ".") + "'" +
                    ")";
                provjera_sql(RemoteDB.insert(sql));

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    sql = "INSERT INTO Rponude_stavke " +
                        "(broj,dana,ukupno,rabat,avans,datumDolaska,datumOdlaska,boravisna_pristojba,iznos_usluge, opis_usluge,porez,cijena_sobe)" +
                        " VALUES " +
                        "(" +
                        "'" + ttxBrojFakture.Text + "'," +
                        "'" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["datum_dolaska"].FormattedValue.ToString() + "'," +
                        "'" + row.Cells["datum_odlaska"].FormattedValue.ToString() + "'," +
                        "'" + row.Cells["boravisna_pristojba"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["opis_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'" +
                        ")" +
                        "";

                    RemoteDB.insert(sql);

                    //_broj_nocenja = Convert.ToDecimal(row.Cells["broj_nocenja"].FormattedValue.ToString());
                    //_rabat = Convert.ToDecimal(row.Cells["rabat"].FormattedValue.ToString());
                    //_usluga = Convert.ToDecimal(row.Cells["iznos_usluge"].FormattedValue.ToString());
                    //_bor_pristojba = Convert.ToDecimal(row.Cells["boravisna_pristojba"].FormattedValue.ToString());
                    //_pdv = Convert.ToDecimal(row.Cells["tb"].FormattedValue.ToString());
                    //_cijena_sobe = Convert.ToDecimal(row.Cells["cijena_sobe"].FormattedValue.ToString());

                    //StopePDVa(_pdv, _cijena_sobe, _broj_nocenja, _bor_pristojba,_rabat,_usluga);
                }
            }
            else if (edit == true)
            {
                ////////////////////////////////////////////////////////UPDATE//////////////////////////////////////////////////////////////

                string sql = "UPDATE Rponude SET" +
                    " godina='" + nmGodinaFakture.Value + "'," +
                    " datumDVO='" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    " datum='" + datumRacuna.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    " datum_valute='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    " id_partner='" + txtSifraOdrediste.Text + "'," +
                    " valuta='" + txtTecaj.Text + "'," +
                    " id_valuta='" + cbValuta.SelectedValue + "'," +
                    " nacin_placanja='" + cbNacinPlacanja.SelectedValue + "'," +
                    " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                    " model='" + txtModel.Text + "'," +
                    " napomena='" + rtbNapomena.Text + "'," +
                    " ukupno='" + UKUPNO_FAKTURA.ToString("#0.00").Replace(",", ".") + "' WHERE broj='" + ttxBrojFakture.Text + "'" +
                    "";
                provjera_sql(RemoteDB.update(sql));

                int z = 0;
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    if (dgw.Rows[z].Cells["id_stavka"].ToString() == "")
                    {
                        //sql = "INSERT INTO Rponude_stavke " +
                        //    "(id_unos_gosta,broj,dana,ukupno,rabat,avans,broj_sobe,datumDolaska,datumOdlaska,opis_usluge,boravisna_pristojba,iznos_usluge,porez,cijena_sobe)" +
                        //    " VALUES " +
                        //    "(" +
                        //    "'" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'," +
                        //    "'" + ttxBrojFakture.Text + "'," +
                        //    "'" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["broj_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["datum_dolaska"].FormattedValue.ToString() + "'," +
                        //    "'" + row.Cells["datum_odlaska"].FormattedValue.ToString() + "'," +
                        //    "'" + row.Cells["opis_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["boravisna_pristojba"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["iznos_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        //    "'" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'" +
                        //    ")" +
                        //    "";
                        //RemoteDB.insert(sql);

                        sql = "INSERT INTO Rponude_stavke " +
                        "(broj,dana,ukupno,rabat,avans,datumDolaska,datumOdlaska,boravisna_pristojba,iznos_usluge, opis_usluge,porez,cijena_sobe)" +
                        " VALUES " +
                        "(" +
                        "'" + ttxBrojFakture.Text + "'," +
                        "'" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["datum_dolaska"].FormattedValue.ToString() + "'," +
                        "'" + row.Cells["datum_odlaska"].FormattedValue.ToString() + "'," +
                        "'" + row.Cells["boravisna_pristojba"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["opis_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'" +
                        ")" +
                        "";

                        RemoteDB.insert(sql);
                    }
                    else
                    {
                        sql = "UPDATE Rponude_stavke SET " +
                        // " id_unos_gosta='" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'," +
                        " broj='" + ttxBrojFakture.Text + "'," +
                        " dana='" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " ukupno='" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " rabat='" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " avans='" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " datumDolaska='" + row.Cells["datum_dolaska"].FormattedValue.ToString() + "'," +
                        //" broj_sobe='" + row.Cells["broj_sobe"].FormattedValue.ToString() + "'," +
                        " opis_usluge='" + row.Cells["opis_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " datumOdlaska='" + row.Cells["datum_odlaska"].FormattedValue.ToString() + "'," +
                        " porez='" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " boravisna_pristojba='" + row.Cells["boravisna_pristojba"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " iznos_usluge='" + row.Cells["iznos_usluge"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " cijena_sobe='" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'" +
                        " WHERE id='" + row.Cells["id_stavka"].FormattedValue.ToString() + "';";
                        RemoteDB.update(sql);
                    }

                    _broj_nocenja = Convert.ToDecimal(row.Cells["broj_nocenja"].FormattedValue.ToString());
                    _rabat = Convert.ToDecimal(row.Cells["rabat"].FormattedValue.ToString());
                    _usluga = Convert.ToDecimal(row.Cells["iznos_usluge"].FormattedValue.ToString());
                    _bor_pristojba = Convert.ToDecimal(row.Cells["boravisna_pristojba"].FormattedValue.ToString());
                    _pdv = Convert.ToDecimal(row.Cells["tb"].FormattedValue.ToString());
                    _cijena_sobe = Convert.ToDecimal(row.Cells["cijena_sobe"].FormattedValue.ToString());

                    ttxBrojFakture.Enabled = true;
                    StopePDVa(_pdv, _cijena_sobe, _broj_nocenja, _bor_pristojba, _rabat, _usluga);
                    //RemoteDB.update("UPDATE unos_gosta SET odjava='1' WHERE id='" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                    z++;
                }
            }

            if (MessageBox.Show("Spremljeno.\r\nŽelite ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(ttxBrojFakture.Text);

                izvjestaji.Faktura.repFaktura rfak = new izvjestaji.Faktura.repFaktura();
                rfak.dokumenat = "PON";
                rfak.ImeForme = "Ponude";
                rfak.broj_dokumenta = ttxBrojFakture.Text;
                rfak.ShowDialog();
            }

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal cij_sobe, decimal brojNoc, decimal borP, decimal rabat, decimal usluga)
        {
            decimal P_stopa = (100 * pdv) / (100 + pdv);
            decimal cijena_sobe_sa_rabatom = ((cij_sobe + usluga) - ((cij_sobe * rabat) / 100));

            cijena_sobe_sa_rabatom = cijena_sobe_sa_rabatom * brojNoc;

            decimal pdv_stavka = ((cijena_sobe_sa_rabatom) * P_stopa / 100);
            decimal osnovica = (cijena_sobe_sa_rabatom) - pdv_stavka;

            DataRow[] dataROW = null;
            if (DTpdv != null && DTpdv.Rows.Count != 0)
                dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW == null || dataROW.Count() == 0)
            {
                //RowPdv = DTpdv.NewRow();
                //RowPdv["stopa"] = pdv.ToString();
                //RowPdv["iznos"] = pdv_stavka.ToString();
                //RowPdv["osnovica"] = osnovica.ToString();
                //RowPdv["boravisna_pristojba"] = borP.ToString();
                //DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                decimal aa = Convert.ToDecimal(dataROW[0]["iznos"].ToString());
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                aa = Convert.ToDecimal(dataROW[0]["iznos"].ToString());

                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
                dataROW[0]["boravisna_pristojba"] = Convert.ToDecimal(dataROW[0]["boravisna_pristojba"].ToString()) + borP;
            }
        }

        private void printaj(string broj)
        {
            //Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            //pr.dokumenat = "FAK";
            //pr.broj_dokumenta = broj;
            //pr.ImeForme = "Faktura";
            //pr.ShowDialog();
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
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
            frmSvePonude objForm2 = new frmSvePonude();
            objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFaktute();
            }
        }

        private void fillFaktute()
        {
            string sql = "SELECT * FROM rponude WHERE broj='" + broj_fakture_edit + "'";
            DataTable DT = RemoteDB.select(sql, "rponude").Tables[0];

            if (DT.Rows.Count > 0)
            {
                ControlDisableEnable(0, 1, 1, 0, 1);
                ttxBrojFakture.Enabled = false;
                EnableDisable(true);
                edit = true;

                //------------------------------------------------------------------------HEADER--------------------------------------
                ttxBrojFakture.Text = DT.Rows[0]["broj"].ToString();
                nmGodinaFakture.Value = Convert.ToInt16(DT.Rows[0]["godina"].ToString());
                dtpDatumDVO.Value = Convert.ToDateTime(DT.Rows[0]["datumDVO"].ToString());
                dtpDatum.Value = Convert.ToDateTime(DT.Rows[0]["datum"].ToString());
                dtpDanaValuta.Value = Convert.ToDateTime(DT.Rows[0]["datum_valute"].ToString());
                txtTecaj.Text = DT.Rows[0]["valuta"].ToString();
                cbValuta.SelectedValue = DT.Rows[0]["id_valuta"].ToString();
                DataTable DTizradio = RemoteDB.select("SELECT ime+' '+prezime as nazvan FROM zaposlenici WHERE id_zaposlenik='" + DT.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0];
                if (DTizradio.Rows.Count > 0)
                {
                    txtIzradio.Text = DTizradio.Rows[0]["nazvan"].ToString();
                }
                DataTable DTpartner = RemoteDB.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DT.Rows[0]["id_partner"].ToString() + "'", "partners").Tables[0];
                if (DTpartner.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
                }
                txtSifraOdrediste.Text = DT.Rows[0]["id_partner"].ToString();

                txtModel.Text = DT.Rows[0]["model"].ToString();
                rtbNapomena.Text = DT.Rows[0]["napomena"].ToString();
                cbNacinPlacanja.SelectedValue = DT.Rows[0]["nacin_placanja"].ToString();

                //------------------------------------------------------------------------STAVKE--------------------------------------
                sql = "SELECT * FROM Rponude_stavke WHERE broj='" + broj_fakture_edit + "'";
                DataTable DT_stavke = RemoteDB.select(sql, "Rponude_stavke").Tables[0];

                foreach (DataRow row in DT_stavke.Rows)
                {
                    dgw.Rows.Add();
                    int i = dgw.Rows.Count - 1;

                    dgw.Rows[i].Cells["br"].Value = i + 1;
                    dgw.Rows[i].Cells["opis_usluge"].Value = row["opis_usluge"].ToString();
                    dgw.Rows[i].Cells["datum_dolaska"].Value = row["datumDolaska"].ToString();
                    dgw.Rows[i].Cells["datum_odlaska"].Value = row["datumOdlaska"].ToString();
                    dgw.Rows[i].Cells["avans"].Value = row["avans"].ToString();
                    dgw.Rows[i].Cells["boravisna_pristojba"].Value = row["boravisna_pristojba"].ToString();
                    dgw.Rows[i].Cells["broj_nocenja"].Value = row["dana"].ToString();
                    dgw.Rows[i].Cells["rabat"].Value = row["rabat"].ToString();
                    dgw.Rows[i].Cells["iznos_usluge"].Value = row["iznos_usluge"].ToString();
                    dgw.Rows[i].Cells["cijena_sobe"].Value = row["cijena_sobe"].ToString();
                    dgw.Rows[i].Cells["tb"].Value = row["porez"].ToString();
                    dgw.Rows[i].Cells["iznos_ukupno"].Value = row["ukupno"].ToString();
                    dgw.Rows[i].Cells["id_stavka"].Value = Convert.ToInt32(row["id"].ToString());
                    //dgw.Rows[i].Cells["id_unos_gosta"].Value = "0";
                    //dgw.Rows[i].Cells["ime"].Value = row["ime_gosta"].ToString();
                    //dgw.Rows[i].Cells["id_unos_gosta"].Value = row["id_unos_gosta"].ToString();
                }
                RacunajUkupno();
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

        private void txtBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nuGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DataTable DT = RemoteDB.select("SELECT broj FROM Rponude WHERE godina='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + ttxBrojFakture.Text + "'", "Rponude").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojFakture() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojFakture.ReadOnly = true;
                        nmGodinaFakture.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    fillFaktute();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
        }

        private int ReturnDaysFromDate(DateTime Date1, DateTime Date2)
        {
            TimeSpan ts = Date2 - Date1;
            int differenceInDays = ts.Days;
            return differenceInDays;
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                try
                {
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString()));
                    RacunajStavku(dgw.CurrentRow.Index);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Krivi unos datuma." + ex.ToString());
                    dgw.Rows[e.RowIndex].Cells[3].Value = DateTime.Now.ToString();
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Now, DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString()));
                    RacunajStavku(dgw.CurrentRow.Index);
                }
            }
            else if (e.ColumnIndex == 3)
            {
                try
                {
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString()));
                    RacunajStavku(dgw.CurrentRow.Index);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Krivi unos datuma." + ex.ToString());
                    dgw.Rows[e.RowIndex].Cells[3].Value = DateTime.Now.ToString();
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Now);
                    RacunajStavku(dgw.CurrentRow.Index);
                }
            }
            else if (e.ColumnIndex == 5)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }
            else if (e.ColumnIndex == 4)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }
            else if (e.ColumnIndex == 7)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }
            else if (e.ColumnIndex == 8)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }
            else if (e.ColumnIndex == 9)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }
            else if (e.ColumnIndex == 10)
            {
                RacunajStavku(dgw.CurrentRow.Index);
            }

            RacunajUkupno();
        }

        private decimal rabat_stavka = 0;
        private decimal avans_ukupno = 0;
        private decimal ukupno_stavka = 0;
        private decimal vrsta_usluge = 0;
        private decimal bor_pristojba = 0;
        private decimal cijena_sobe_iznos = 0;
        private decimal broj_nocenja_iznos = 0;

        private void RacunajStavku(int row)
        {
            decimal dec;
            if (!decimal.TryParse(dgw.Rows[row].Cells["avans"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["avans"].Value = "0";
            }
            if (!decimal.TryParse(dgw.Rows[row].Cells["rabat"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["rabat"].Value = "0";
            }
            if (!decimal.TryParse(dgw.Rows[row].Cells["boravisna_pristojba"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["boravisna_pristojba"].Value = "0";
            }
            if (!decimal.TryParse(dgw.Rows[row].Cells["iznos_usluge"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["iznos_usluge"].Value = "0";
            }
            if (!decimal.TryParse(dgw.Rows[row].Cells["cijena_sobe"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["cijena_sobe"].Value = "0";
            }
            if (!decimal.TryParse(dgw.Rows[row].Cells["broj_nocenja"].FormattedValue.ToString(), out dec))
            {
                dgw.Rows[row].Cells["broj_nocenja"].Value = "0";
            }

            broj_nocenja_iznos = Convert.ToDecimal(dgw.Rows[row].Cells["broj_nocenja"].FormattedValue.ToString());
            rabat_stavka = Convert.ToDecimal(dgw.Rows[row].Cells["rabat"].FormattedValue.ToString());
            avans_ukupno = Convert.ToDecimal(dgw.Rows[row].Cells["avans"].FormattedValue.ToString());
            vrsta_usluge = Convert.ToDecimal(dgw.Rows[row].Cells["iznos_usluge"].FormattedValue.ToString()) * broj_nocenja_iznos;
            bor_pristojba = Convert.ToDecimal(dgw.Rows[row].Cells["boravisna_pristojba"].FormattedValue.ToString()) * broj_nocenja_iznos;
            cijena_sobe_iznos = Convert.ToDecimal(dgw.Rows[row].Cells["cijena_sobe"].FormattedValue.ToString()) * broj_nocenja_iznos;

            ukupno_stavka = (cijena_sobe_iznos - (((cijena_sobe_iznos) * rabat_stavka) / 100)) - avans_ukupno + vrsta_usluge + bor_pristojba;

            dgw.Rows[row].Cells["iznos_ukupno"].Value = ukupno_stavka.ToString("#0.00");
        }

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    RESORT.Forme.sifarnici.frmPartnerTrazi partnerTrazi = new RESORT.Forme.sifarnici.frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = RemoteDB.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            dtpDatum.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
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
                    //dtpDatum.Select();
                }

                DataTable DSpar = RemoteDB.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    dtpDatum.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                dgw.Rows.Add("", "", DateTime.Now.ToString(), DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "10", "0");
                RedniBroj();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        RemoteDB.delete("DELETE FROM Rponude_stavke WHERE id='" + dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString() + "'");
                    }
                }
                RemoteDB.update("UPDATE unos_gosta SET odjava='0' WHERE id='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                izracun();
                MessageBox.Show("Obrisano");
            }
        }

        private void txtSifraOdrediste_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                }
                else
                {
                }

                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Insert)
            {
                dgw.Rows.Add("", "", DateTime.Now.ToString(), DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "10", "0");
                RedniBroj();
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

        private void txtDana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0";
                    dtpDanaValuta.Select();
                }

                try
                {
                    DateTime dvo = dtpDatumDVO.Value;
                    dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text));
                    ;
                    dtpDanaValuta.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (rtbNapomena.Text == "")
                {
                    dgw.Select();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            dgw.Rows.Add("", "", DateTime.Now.ToString(), DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "10", "0");
            RedniBroj();
            RacunajUkupno();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
            {
                return;
            }

            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
            {
                string sql = "DELETE FROM Rponude_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'";
                RemoteDB.delete(sql);
            }

            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            RedniBroj();
            RacunajUkupno();
        }

        private void RedniBroj()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                dgw.Rows[i].Cells["br"].Value = i + 1;
            }
        }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}