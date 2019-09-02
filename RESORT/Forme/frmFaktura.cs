using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmFaktura : Form
    {
        public string broj_fakture_edit { get; set; }

        public frmFaktura()
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
        public frmFaktura MainForm { get; set; }
        public string G_broj_unosa { get; set; }

        public List<string> ListaIzRezervacija { get; set; }

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
            ControlDisableEnable(1, 0, 1, 0, 0);
            if (ListaIzRezervacija != null) { btnNoviUnos.PerformClick(); SetFromReservations(); }
            if (broj_fakture_edit != null) { btnNoviUnos.PerformClick(); fillFaktute(); }
            if (G_broj_unosa != null) { btnNoviUnos.PerformClick(); txtBrojUnosa.Text = G_broj_unosa; txtBrojUnosa.Select(); VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Enter); }
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmFaktura MainForm { get; set; }

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
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 2)
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
                int curent = d.CurrentRow.Index;
            }
        }

        private void SetFromReservations()
        {
            decimal kom_boravisna = 0;
            for (int i = 0; i < ListaIzRezervacija.Count; i++)
            {
                string sql = "SELECT unos_rezervacije.ime_gosta," +
                    " unos_rezervacije.ime_gosta, " +
                    " unos_rezervacije.datum_dolaska, " +
                    " unos_rezervacije.datum_odlaska, " +
                    " unos_rezervacije.avans, " +
                    " sobe.broj_sobe" +
                    " FROM unos_rezervacije " +
                    " LEFT JOIN sobe ON sobe.id=unos_rezervacije.id_soba WHERE unos_rezervacije.id='" + ListaIzRezervacija[i] + "'";
                DataTable DT = RemoteDB.select(sql, "unos_rezervacije").Tables[0];

                DateTime dtOD = Convert.ToDateTime(DT.Rows[0]["datum_dolaska"].ToString());
                DateTime dtDO = Convert.ToDateTime(DT.Rows[0]["datum_odlaska"].ToString());

                string porez = "0";
                DataTable dtPos = classDBlite.LiteSelect("SELECT pdv_nocenje FROM postavke", "postavke").Tables[0];
                porez = dtPos.Rows.Count == 0 ? "0" : dtPos.Rows[0][0].ToString();

                dgw.Rows.Add(dgw.Rows.Count + 1, DT.Rows[0]["broj_sobe"].ToString(), DT.Rows[0]["ime_gosta"].ToString(), dtOD.ToString(), dtDO.ToString(), DT.Rows[0]["avans"].ToString(), "0", "0", "0", porez, "0", "", "0");
                dgw.Rows[dgw.RowCount - 1].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[dgw.RowCount - 1].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Parse(dgw.Rows[dgw.RowCount - 1].Cells["datum_odlaska"].FormattedValue.ToString()));
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();

                try
                {
                    if (dgw.Rows.Count > 0)
                        kom_boravisna += Convert.ToDecimal(dgw.Rows[dgw.Rows.Count - 1].Cells["broj_nocenja"].FormattedValue.ToString());
                }
                catch (Exception)
                {
                }
            }

            dgw.Rows.Add(dgw.Rows.Count + 1, "-1", "BORAVIŠNA PRISTOJBA", "---------", "---------", "0", kom_boravisna, "0", "7", "0", "0", "", "0");
            RacunajStavku(dgw.Rows.Count - 1);
            RacunajUkupno();
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 3)
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
            //else if (d.CurrentCell.ColumnIndex == 10)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
            //    d.BeginEdit(true);
            //    int curent = d.CurrentRow.Index;
            //}
        }

        private void RightDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 2)
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
                int curent = d.CurrentRow.Index;
            }
            //else if (d.CurrentCell.ColumnIndex == 10)
            //{
            //    int curent = d.CurrentRow.Index;
            //}
        }

        private void UpDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[2];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[2];
                d.BeginEdit(true);
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int sve, int delAll, int spremi)
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
                MessageBox.Show("Žiro račun:\r\n" + ex.ToString());
            }

            ////fill cbPDV
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
                cbNacinPlacanja.SelectedValue = 1;
                txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Način plačanja:\r\n" + ex.ToString());
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
                txtTecaj.Text = "1";
                cbValuta.SelectedValue = "5";

                DataTable DTSK = new DataTable("skladiste");
                DTSK.Columns.Add("id_skladiste", typeof(string));
                DTSK.Columns.Add("skladiste", typeof(string));

                DS_Skladiste = RemoteDB.select("SELECT * FROM skladiste", "skladiste");
                //DTSK.Rows.Add(0,"");
                for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
                {
                    DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valuta, skladište:\r\n" + ex.ToString());
            }

            try
            {
                //fill tko je prijavljen
                txtIzradio.Text = RemoteDB.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + RESORT.Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

                DS_Skladiste = RemoteDB.select("SELECT * FROM skladiste", "skladiste");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zaposlenik:\r\n" + ex.ToString());
            }
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = DateTime.Now.Year;
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                UKUPNO_FAKTURA = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    UKUPNO_FAKTURA = Convert.ToDecimal(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString()) + UKUPNO_FAKTURA;
                }
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            sifarnici.frmPartnerTrazi partnerTrazi = new sifarnici.frmPartnerTrazi();
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

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (dgw.Rows[dgw.CurrentRow.Index].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                    {
                        string kol = RemoteDB.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                        kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                        RemoteDB.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                    }

                    RemoteDB.delete("DELETE FROM faktura_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    RemoteDB.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + RESORT.Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("M/d/yyyy") + "','Brisanje stavke sa fakture br." + ttxBrojFakture.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (edit)
                if (MessageBox.Show("Dali ste sigurni da želite stonirati ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //HEADER
                    string sql = "SELECT * FROM rfaktura_stavke WHERE broj='" + ttxBrojFakture.Text + "'";
                    DataTable DTstavke = RemoteDB.select(sql, "rfaktura_stavke").Tables[0];

                    //STAVKE
                    sql = "SELECT * FROM rfakture WHERE broj='" + ttxBrojFakture.Text + "'";
                    DataTable DTheader = RemoteDB.select(sql, "rfaktura").Tables[0];

                    DateTime datumRacuna = DateTime.Now;

                    if (DTheader.Rows.Count > 0)
                    {
                        if (DTheader.Rows[0]["storno"].ToString() == "DA")
                        {
                            MessageBox.Show("Ovaj račun već je storniran.", "Upozorenje");
                            return;
                        }

                        string brojF = brojFakture();

                        izracun();

                        if (DTpdv.Columns["stopa"] == null)
                        {
                            DTpdv.Columns.Add("stopa");
                            DTpdv.Columns.Add("iznos");
                            DTpdv.Columns.Add("osnovica");
                            DTpdv.Columns.Add("boravisna_pristojba");
                        }
                        else
                        {
                            DTpdv.Clear();
                        }

                        sql = "INSERT INTO rfakture (broj,godina,datumdvo,datum,datum_valute,valuta,id_valuta,id_partner,nacin_placanja,id_izradio,model,napomena,ukupno) VALUES (" +
                            "'" + brojF + "'," +
                            "'" + DTheader.Rows[0]["godina"].ToString() + "'," +
                            "'" + datumRacuna.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'" + datumRacuna.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'" + datumRacuna.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'" + DTheader.Rows[0]["valuta"].ToString().Replace(",", ".") + "'," +
                            "'" + DTheader.Rows[0]["id_valuta"].ToString() + "'," +
                            "'" + DTheader.Rows[0]["id_partner"].ToString() + "'," +
                            "'" + DTheader.Rows[0]["nacin_placanja"].ToString() + "'," +
                            "'" + DTheader.Rows[0]["id_izradio"].ToString() + "'," +
                            "'" + DTheader.Rows[0]["model"].ToString() + "'," +
                            "'" + DTheader.Rows[0]["napomena"].ToString() + "'," +
                            "'" + (Convert.ToDecimal(DTheader.Rows[0]["ukupno"].ToString()) * -1).ToString().Replace(",", ".") + "'" +
                            ")";
                        Funkcije.provjera_sql(RemoteDB.insert(sql));
                        Funkcije.provjera_sql(RemoteDB.update("UPDATE rfakture SET storno='DA' WHERE broj='" + ttxBrojFakture.Text + "'"));

                        //int otpremnica = 0;
                        //if (int.TryParse(DTheader.Rows[0]["otpremnica"].ToString(), out otpremnica) && otpremnica > 0)
                        updateOtpremnicaNaplataFakturom(Convert.ToInt32(ttxBrojFakture.Text));

                        string[] porez_na_potrosnju = new string[3];
                        porez_na_potrosnju[0] = "0";
                        porez_na_potrosnju[1] = "0";
                        porez_na_potrosnju[2] = "0";

                        decimal Porez_potrosnja_sve = 0, Porez_potrosnja_stavka = 0, maxPNP = 0, osnovica_pnp = 0;

                        for (int i = 0; i < DTstavke.Rows.Count; i++)
                        {
                            decimal broj_dana = 0, avans = 0, PP = 0, mpc = 0, pdv = 0, rabat = 0;
                            decimal.TryParse(DTstavke.Rows[i]["dana"].ToString(), out broj_dana);
                            decimal.TryParse(DTstavke.Rows[i]["avans"].ToString(), out avans);
                            decimal.TryParse(DTstavke.Rows[i]["rabat"].ToString(), out rabat);

                            decimal.TryParse(DTstavke.Rows[i]["porez"].ToString(), out pdv);
                            try
                            {
                                decimal.TryParse(DTstavke.Rows[i]["otpremnica_pnp"].ToString(), out PP);
                                PP = 0;
                            }
                            catch { }
                            decimal.TryParse(DTstavke.Rows[i]["cijena_sobe"].ToString(), out mpc);

                            broj_dana = broj_dana * (-1);
                            mpc = mpc * broj_dana;
                            mpc = Math.Round((mpc - (mpc * rabat / 100)), 3);

                            decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * PP) / (100 + pdv + PP));
                            Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                            Porez_potrosnja_stavka = 0;
                            if (PP > 0)
                            {
                                osnovica_pnp += mpc / (1 + (PP + pdv) / 100);
                            }

                            if (maxPNP < PP)
                                maxPNP = PP;

                            Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                            sql = "INSERT INTO rfaktura_stavke (broj,dana,ukupno,rabat,porez,avans,ime_gosta,datumdolaska,datumodlaska,cijena_sobe,broj_sobe,id_unos_gosta, otpremnica_pnp) VALUES " +
                                "(" +
                                "'" + brojF + "'," +
                                "'" + (broj_dana * -1).ToString().Replace(",", ".") + "'," +
                                "'" + (Convert.ToDecimal(DTstavke.Rows[i]["ukupno"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["rabat"].ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["porez"].ToString().Replace(",", ".") + "'," +
                                "'" + avans.ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["ime_gosta"].ToString() + "'," +
                                "'" + DTstavke.Rows[i]["datumdolaska"].ToString() + "'," +
                                "'" + DTstavke.Rows[i]["datumodlaska"].ToString() + "'," +
                                //"'" + (Convert.ToDecimal(DTstavke.Rows[i]["boravisna_pristojba"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                                //"'" + (Convert.ToDecimal(DTstavke.Rows[i]["iznos_usluge"].ToString()) * -1).ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["cijena_sobe"].ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["broj_sobe"].ToString().Replace(",", ".") + "'," +
                                "'" + DTstavke.Rows[i]["id_unos_gosta"].ToString() + "'," +
                                "'" + DTstavke.Rows[i]["otpremnica_pnp"].ToString().Replace(",", ".") + "'" +
                                ")";
                            Funkcije.provjera_sql(RemoteDB.insert(sql));

                            _broj_nocenja = Convert.ToDecimal(DTstavke.Rows[i]["dana"].ToString()) * -1;
                            _rabat = Convert.ToDecimal(DTstavke.Rows[i]["rabat"].ToString());
                            //_usluga = Convert.ToDecimal(DTstavke.Rows[i]["iznos_usluge"].ToString()) * -1;
                            //_bor_pristojba = Convert.ToDecimal(DTstavke.Rows[i]["boravisna_pristojba"].ToString())*-1;
                            _pdv = Convert.ToDecimal(DTstavke.Rows[i]["porez"].ToString());
                            _cijena_sobe = Convert.ToDecimal(DTstavke.Rows[i]["cijena_sobe"].ToString());

                            StopePDVa(_pdv, _cijena_sobe, _broj_nocenja, _bor_pristojba, _rabat, _usluga, PP);
                        }

                        //porez_na_potrosnju[0] = maxPNP.ToString().Replace(".", ",");
                        //porez_na_potrosnju[1] = osnovica_pnp.ToString().Replace(".", ",");
                        //porez_na_potrosnju[2] = Porez_potrosnja_sve.ToString().Replace(".", ",");
                        porez_na_potrosnju[0] = "0";
                        porez_na_potrosnju[1] = "0";
                        porez_na_potrosnju[2] = "0";

                        DataTable DTzaposlenik = RemoteDB.select("SELECT oib FROM zaposlenici", "zaposlenici").Tables[0];
                        DataTable DTpodaciT = classDBlite.LiteSelect("SELECT oib FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];
                        INIFile ini = new INIFile();

                        bool sustav_pdva = false;
                        if (ini.Read("Postavke", "u_sustavu_pdva").ToString() == "1") { sustav_pdva = true; }

                        DataTable DTOstaliPor = new DataTable();
                        DTOstaliPor.Columns.Add("naziv");
                        DTOstaliPor.Columns.Add("stopa");
                        DTOstaliPor.Columns.Add("osnovica");
                        DTOstaliPor.Columns.Add("iznos");
                        DataRow RowOstaliPor;

                        DataTable DTnaknade = new DataTable();
                        DTnaknade.Columns.Add("naziv");
                        DTnaknade.Columns.Add("iznos");
                        DataRow ROWnaknade;

                        string nacin_placanja = "G";
                        if (DTheader.Rows[0]["nacin_placanja"].ToString() == "1")
                        {
                            nacin_placanja = "G";
                        }
                        else if (DTheader.Rows[0]["nacin_placanja"].ToString() == "2")
                        {
                            nacin_placanja = "K";
                        }
                        else if (DTheader.Rows[0]["nacin_placanja"].ToString() == "3")
                        {
                            nacin_placanja = "T";
                        }

                        string[] fiskalizacija = new string[3];
                        fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(DTpodaciT.Rows[0]["oib"].ToString(),
                            DTzaposlenik.Rows[0]["oib"].ToString(),
                            datumRacuna,
                            Convert.ToInt16(brojF),
                            DTpdv,
                            porez_na_potrosnju,
                            DTOstaliPor,
                            "",
                            "",
                            DTnaknade,
                            Convert.ToDecimal(DTheader.Rows[0]["ukupno"].ToString()) * -1,
                            nacin_placanja,
                            false, "F"
                            );

                        Funkcije.DodajPodatkeOfiskalizaciji(brojF, fiskalizacija);

                        RESORT.izvjestaji.Faktura.repFaktura rfak = new RESORT.izvjestaji.Faktura.repFaktura();
                        rfak.dokumenat = "FAK";
                        rfak.ImeForme = "Fakture";
                        rfak.broj_dokumenta = brojF;
                        rfak.ShowDialog();
                    }

                    MessageBox.Show("Faktura je stonirana.");

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        try
                        {
                            DataTable DD = RemoteDB.select("SELECT broj FROM unos_gosta WHERE id='" + dgw.Rows[i].Cells["id_unos_gosta"].FormattedValue.ToString() + "'", "unos_gosta").Tables[0];

                            if (DD.Rows.Count > 0)
                                RemoteDB.update("UPDATE unos_gosta SET odjava='0' WHERE broj='" + DD.Rows[0]["broj"].ToString() + "'");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    }

                    izracun();
                    edit = false;
                    EnableDisable(false);
                    deleteFields();
                    ControlDisableEnable(1, 0, 1, 0, 0);
                }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            dgw.Enabled = true;
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojFakture.Text = brojFakture();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ttxBrojFakture.Enabled = true;
            ControlDisableEnable(1, 0, 1, 0, 0);

            dgw.Columns[3].Visible = true;
            dgw.Columns[4].Visible = true;
            sve_u_jednu_stavku = false;
        }

        private void EnableDisable(bool x)
        {
            btnDelete.Enabled = x;
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
            txtBrojUnosa.Enabled = x;
            txtBrojSobe.Enabled = x;

            btnObrišiSve.Enabled = x;
            btnSveUjednuStavku.Enabled = x;
            btnDodatanLežaj.Enabled = x;
            btnDodajBoravisnuPristojbu.Enabled = x;
            pictureBox1.Enabled = x;
            pictureBox2.Enabled = x;
            txtDana.Enabled = x;
            txtIzradio.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            txtModel.Text = "";
            rtbNapomena.Text = "";
            txtBrojUnosa.Text = "";
            txtBrojUnosa.Text = "";
            txtBrojSobe.Text = "";
            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            dgw.Enabled = true;
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 0, 0, 1);
            ttxBrojFakture.ReadOnly = true;
            nmGodinaFakture.ReadOnly = true;
            txtSifraOdrediste.Select();
            dgw.Columns[3].Visible = true;
            dgw.Columns[4].Visible = true;
            sve_u_jednu_stavku = false;
        }

        private string brojFakture()
        {
            DataTable DSbr = RemoteDB.select("SELECT MAX(broj) FROM rfakture", "rfakture").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
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

        private void updateOtpremnicaNaplataFakturom(int rfaktura_broj = 0, int broj_otpremnice = 0)
        {
            string sql = string.Format(@"update otpremnice set rfaktura_broj = {0}, rfaktura_poslovnica = {1}, rfaktura_naplatni_uredaj = {2} where broj_otpremnice = {3};",
                rfaktura_broj,
                Class.PostavkeFiskalizacije.poslovnicaId,
                Class.PostavkeFiskalizacije.naplatniUredajId,
                broj_otpremnice);
            if (broj_otpremnice == 0 && rfaktura_broj > 0)
            {
                sql = string.Format(@"update otpremnice set rfaktura_broj = 0, rfaktura_poslovnica = 0, rfaktura_naplatni_uredaj = 0 where rfaktura_broj = {0};", rfaktura_broj);
            }
            else if (broj_otpremnice > 0 && rfaktura_broj == 0)
            {
                sql = string.Format(@"update otpremnice set rfaktura_broj = 0, rfaktura_poslovnica = 0, rfaktura_naplatni_uredaj = 0 where broj_otpremnice = {0};", broj_otpremnice);
            }

            RemoteDB.update(sql);
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            izracun();

            if (UKUPNO_FAKTURA == 0)
            {
                MessageBox.Show("Faktura ne može biti spremljena je nemate niti jednu stavku, ili iznos na fakturi je 0.00 kn.");
                return;
            }

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("boravisna_pristojba");
            }
            else
            {
                DTpdv.Clear();
            }

            decimal decP;
            if (!decimal.TryParse(txtSifraOdrediste.Text, out decP))
            {
                txtSifraOdrediste.Text = "0";
            }

            if (!decimal.TryParse(txtSifraNacinPlacanja.Text, out decP))
            {
                MessageBox.Show("Greška kod upisa načina plaćanja.", "Greška");
                return;
            }

            dtpDatum.Value = DateTime.Now;
            DateTime datumRacuna = dtpDatum.Value;

            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "0";
            porez_na_potrosnju[1] = "0";
            porez_na_potrosnju[2] = "0";

            decimal Porez_potrosnja_sve = 0, Porez_potrosnja_stavka = 0, maxPNP = 0, osnovica_pnp = 0;

            if (edit == false)
            {
                string sql = "INSERT INTO Rfakture (" +
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

                string d1 = "", d2 = "";
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    decimal broj_dana = 0, avans = 0, PP = 0, mpc = 0, pdv = 0, rabat = 0;
                    decimal.TryParse(row.Cells["broj_nocenja"].Value.ToString(), out broj_dana);
                    decimal.TryParse(row.Cells["avans"].Value.ToString(), out avans);
                    decimal.TryParse(row.Cells["rabat"].Value.ToString(), out rabat);

                    decimal.TryParse(row.Cells["tb"].Value.ToString(), out pdv);

                    try
                    {
                        decimal.TryParse(row.Cells["otpremnica_pnp"].Value.ToString(), out PP);
                        PP = 0;
                    }
                    catch { }

                    decimal.TryParse(row.Cells["cijena_sobe"].Value.ToString(), out mpc);

                    //broj_dana = broj_dana;
                    mpc = mpc * broj_dana;
                    mpc = Math.Round((mpc - (mpc * rabat / 100)), 3);

                    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * PP) / (100 + pdv + PP));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                    Porez_potrosnja_stavka = 0;
                    if (PP > 0)
                    {
                        osnovica_pnp += mpc / (1 + (PP + pdv) / 100);
                    }

                    if (maxPNP < PP)
                        maxPNP = PP;

                    Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                    DateTime dDolazak;
                    DateTime dOdlazak;

                    if (DateTime.TryParse(row.Cells["datum_dolaska"].FormattedValue.ToString(), out dDolazak) && DateTime.TryParse(row.Cells["datum_odlaska"].FormattedValue.ToString(), out dOdlazak))
                    {
                        d1 = dDolazak.ToString("yyyy-MM-dd H:mm:ss");
                        d2 = dOdlazak.ToString("yyyy-MM-dd H:mm:ss");
                    }
                    else
                    {
                        d1 = DateTime.Now.ToString("1970-01-10 0:00:01");
                        d2 = DateTime.Now.ToString("1970-01-10 0:00:01");
                    }

                    decimal otpremnica_pnp = 0;
                    try
                    {
                        decimal.TryParse(row.Cells["otpremnica_pnp"].FormattedValue.ToString(), out otpremnica_pnp);
                        otpremnica_pnp = 0;
                    }
                    catch { }

                    int otpremnica = 0;
                    int.TryParse(row.Cells["otpremnica"].FormattedValue.ToString(), out otpremnica);

                    sql = "INSERT INTO Rfaktura_stavke " +
                        "(id_unos_gosta,broj,dana,ukupno,rabat,avans,broj_sobe,datumDolaska,datumOdlaska, ime_gosta,porez,cijena_sobe, otpremnica_broj, otpremnica_pnp)" +
                        " VALUES " +
                        "(" +
                        "'" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'," +
                        "'" + ttxBrojFakture.Text + "'," +
                        "'" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["broj_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + d1 + "'," +
                        "'" + d2 + "'," +
                        "'" + row.Cells["ime"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + otpremnica + "'," +
                        "'" + otpremnica_pnp.ToString().Replace(",", ".") + "'" +
                        ")" +
                        "";

                    Funkcije.provjera_sql(RemoteDB.insert(sql));

                    if (otpremnica > 0)
                        updateOtpremnicaNaplataFakturom(Convert.ToInt32(ttxBrojFakture.Text), otpremnica);

                    _broj_nocenja = Convert.ToDecimal(row.Cells["broj_nocenja"].FormattedValue.ToString());
                    _rabat = Convert.ToDecimal(row.Cells["rabat"].FormattedValue.ToString());
                    _pdv = Convert.ToDecimal(row.Cells["tb"].FormattedValue.ToString());
                    _cijena_sobe = Convert.ToDecimal(row.Cells["cijena_sobe"].FormattedValue.ToString());

                    StopePDVa(_pdv, _cijena_sobe, _broj_nocenja, _bor_pristojba, _rabat, _usluga, otpremnica_pnp);

                    if (sve_u_jednu_stavku == true)
                    {
                        SetOdjava();
                    }
                    else if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
                    {
                        for (int f = 0; f < list_id_unos_gosta.Count; f++)
                        {
                            RemoteDB.update("UPDATE unos_gosta SET odjava='1' WHERE id='" + list_id_unos_gosta[f] + "'");
                        }
                    }
                    else
                    {
                        RemoteDB.update("UPDATE unos_gosta SET odjava='1' WHERE id='" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                    }
                }
            }
            else if (edit == true)
            {
                ////////////////////////////////////////////////////////UPDATE//////////////////////////////////////////////////////////////

                string sql = "UPDATE Rfakture SET" +
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

                updateOtpremnicaNaplataFakturom(Convert.ToInt32(ttxBrojFakture.Text));

                int z = 0;
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    decimal broj_dana = 0, avans = 0, PP = 0, mpc = 0, pdv = 0, rabat = 0;
                    decimal.TryParse(row.Cells["broj_nocenja"].Value.ToString(), out broj_dana);
                    decimal.TryParse(row.Cells["avans"].Value.ToString(), out avans);
                    decimal.TryParse(row.Cells["rabat"].Value.ToString(), out rabat);

                    decimal.TryParse(row.Cells["tb"].Value.ToString(), out pdv);
                    try
                    {
                        decimal.TryParse(row.Cells["otpremnica_pnp"].Value.ToString(), out PP);
                        PP = 0;
                    }
                    catch { }
                    decimal.TryParse(row.Cells["cijena_sobe"].Value.ToString(), out mpc);

                    //broj_dana = broj_dana * (-1);
                    mpc = mpc * broj_dana;
                    mpc = Math.Round((mpc - (mpc * rabat / 100)), 3);

                    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * PP) / (100 + pdv + PP));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                    Porez_potrosnja_stavka = 0;
                    if (PP > 0)
                    {
                        osnovica_pnp += mpc / (1 + (PP + pdv) / 100);
                    }

                    if (maxPNP < PP)
                        maxPNP = PP;

                    Porez_potrosnja_sve = (Porez_potrosnja_stavka) + Porez_potrosnja_sve;

                    DateTime dDolazak;
                    DateTime dOdlazak;
                    string d1 = "", d2 = "";
                    if (DateTime.TryParse(row.Cells["datum_dolaska"].FormattedValue.ToString(), out dDolazak) && !DateTime.TryParse(row.Cells["datum_odlaska"].FormattedValue.ToString(), out dOdlazak))
                    {
                        d1 = dDolazak.ToString("yyyy-MM-dd H:mm:ss");
                        d2 = dOdlazak.ToString("yyyy-MM-dd H:mm:ss");
                    }
                    else
                    {
                        d1 = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                        d2 = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                    }

                    int otpremnica = 0;
                    int.TryParse(row.Cells["otpremnica"].FormattedValue.ToString(), out otpremnica);

                    if (dgw.Rows[z].Cells["id_stavka"].Value.ToString() == "")
                    {
                        sql = "INSERT INTO Rfaktura_stavke " +
                        "(id_unos_gosta,broj,dana,ukupno,rabat,avans,broj_sobe,datumDolaska,datumOdlaska, ime_gosta,porez,cijena_sobe, otpremnica_broj)" +
                        " VALUES " +
                        "(" +
                        "'" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'," +
                        "'" + ttxBrojFakture.Text + "'," +
                        "'" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["broj_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + d1 + "'," +
                        "'" + d2 + "'," +
                        "'" + row.Cells["ime"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + otpremnica + "'" +
                        ")" +
                        "";
                        RemoteDB.insert(sql);
                    }
                    else
                    {
                        if (DateTime.TryParse(row.Cells["datum_dolaska"].FormattedValue.ToString(), out dDolazak) && !DateTime.TryParse(row.Cells["datum_odlaska"].FormattedValue.ToString(), out dOdlazak))
                        {
                            d1 = dDolazak.ToString("yyyy-MM-dd H:mm:ss");
                            d2 = dOdlazak.ToString("yyyy-MM-dd H:mm:ss");
                        }
                        else
                        {
                            d1 = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                            d2 = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                        }

                        sql = "UPDATE Rfaktura_stavke SET " +
                        " id_unos_gosta='" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'," +
                        " broj='" + ttxBrojFakture.Text + "'," +
                        " dana='" + row.Cells["broj_nocenja"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " ukupno='" + row.Cells["iznos_ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " rabat='" + row.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " avans='" + row.Cells["avans"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " datumDolaska='" + d1 + "'," +
                        " broj_sobe='" + row.Cells["broj_sobe"].FormattedValue.ToString() + "'," +
                        " ime_gosta='" + row.Cells["ime"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " datumOdlaska='" + d2 + "'," +
                        " porez='" + row.Cells["tb"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " cijena_sobe='" + row.Cells["cijena_sobe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        " otpremnica_broj = '" + otpremnica + "'" +
                        " WHERE id='" + dgw.Rows[z].Cells["id_stavka"].FormattedValue.ToString() + "'";
                        RemoteDB.update(sql);
                    }

                    if (otpremnica > 0)
                        updateOtpremnicaNaplataFakturom(Convert.ToInt32(ttxBrojFakture.Text), otpremnica);

                    _broj_nocenja = Convert.ToDecimal(row.Cells["broj_nocenja"].FormattedValue.ToString());
                    _rabat = Convert.ToDecimal(row.Cells["rabat"].FormattedValue.ToString());
                    _pdv = Convert.ToDecimal(row.Cells["tb"].FormattedValue.ToString());
                    _cijena_sobe = Convert.ToDecimal(row.Cells["cijena_sobe"].FormattedValue.ToString());

                    ttxBrojFakture.Enabled = true;
                    StopePDVa(_pdv, _cijena_sobe, _broj_nocenja, _bor_pristojba, _rabat, _usluga, PP);
                    if (sve_u_jednu_stavku == true)
                    {
                        SetOdjava();
                    }
                    else
                    {
                        RemoteDB.update("UPDATE unos_gosta SET odjava='1' WHERE id='" + row.Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                    }
                    sve_u_jednu_stavku = false;
                    z++;
                }
            }

            //porez_na_potrosnju[0] = maxPNP.ToString().Replace(".", ",");
            //porez_na_potrosnju[1] = osnovica_pnp.ToString().Replace(".", ",");
            //porez_na_potrosnju[2] = Porez_potrosnja_sve.ToString().Replace(".", ",");
            porez_na_potrosnju[0] = "0";
            porez_na_potrosnju[1] = "0";
            porez_na_potrosnju[2] = "0";

            if (MessageBox.Show("Spremljeno.\r\nŽelite li fiskalizirati i ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string nacin_placanja = "G";
                if (cbNacinPlacanja.SelectedValue.ToString() == "1")
                {
                    nacin_placanja = "G";
                }
                else if (cbNacinPlacanja.SelectedValue.ToString() == "2")
                {
                    nacin_placanja = "K";
                }
                else if (cbNacinPlacanja.SelectedValue.ToString() == "3")
                {
                    nacin_placanja = "T";
                }

                DataTable DTzaposlenik = RemoteDB.select("SELECT oib FROM zaposlenici", "zaposlenici").Tables[0];
                DataTable DTpodaciT = classDBlite.LiteSelect("SELECT oib FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];
                INIFile ini = new INIFile();

                bool sustav_pdva = false;
                if (ini.Read("Postavke", "u_sustavu_pdva").ToString() == "1") { sustav_pdva = true; }

                DataTable DTOstaliPor = new DataTable();
                DTOstaliPor.Columns.Add("naziv");
                DTOstaliPor.Columns.Add("stopa");
                DTOstaliPor.Columns.Add("osnovica");
                DTOstaliPor.Columns.Add("iznos");
                DataRow RowOstaliPor;

                DataTable DTnaknade = new DataTable();
                DTnaknade.Columns.Add("naziv");
                DTnaknade.Columns.Add("iznos");
                DataRow ROWnaknade;

                string[] fiskalizacija = new string[3];
                fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(DTpodaciT.Rows[0]["oib"].ToString(),
                    DTzaposlenik.Rows[0]["oib"].ToString(),
                    datumRacuna,
                    Convert.ToInt16(ttxBrojFakture.Text),
                    DTpdv,
                    porez_na_potrosnju,
                    DTOstaliPor,
                    "",
                    "",
                    DTnaknade,
                    UKUPNO_FAKTURA,
                    nacin_placanja,
                    false,
                    "F"
                    );

                Funkcije.DodajPodatkeOfiskalizaciji(ttxBrojFakture.Text, fiskalizacija);

                RESORT.izvjestaji.Faktura.repFaktura rfak = new RESORT.izvjestaji.Faktura.repFaktura();
                rfak.dokumenat = "FAK";
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = ttxBrojFakture.Text;
                rfak.ShowDialog();
            }

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ControlDisableEnable(1, 0, 1, 0, 0);

            dgw.Columns[3].Visible = true;
            dgw.Columns[4].Visible = true;

            RemoteDB.update(@"UPDATE rfakture SET ukupno=
                COALESCE((select SUM(ukupno) from rfaktura_stavke
                WHERE rfaktura_stavke.broj=rfakture.broj GROUP BY rfaktura_stavke.broj),0);");
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

        private DataTable DTpdv = new DataTable();
        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal cij_sobe, decimal brojNoc, decimal borP, decimal rabat, decimal usluga, decimal pnp = 0)
        {
            decimal P_stopa = (100 * pdv) / (100 + pdv + pnp);
            decimal cijena_sobe_sa_rabatom = ((cij_sobe + usluga) - ((cij_sobe * rabat) / 100));

            cijena_sobe_sa_rabatom = cijena_sobe_sa_rabatom * brojNoc;

            decimal pdv_stavka = ((cijena_sobe_sa_rabatom) * P_stopa / 100);
            decimal osnovica = (cijena_sobe_sa_rabatom) - pdv_stavka;

            osnovica = cijena_sobe_sa_rabatom / (1 + (pnp + pdv) / 100);
            DataRow[] dataROW = DTpdv.Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = pdv.ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["osnovica"] = osnovica.ToString();
                RowPdv["boravisna_pristojba"] = (borP * brojNoc).ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                decimal aa = Convert.ToDecimal(dataROW[0]["iznos"].ToString());
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                aa = Convert.ToDecimal(dataROW[0]["iznos"].ToString());

                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
                dataROW[0]["boravisna_pristojba"] = Convert.ToDecimal(dataROW[0]["boravisna_pristojba"].ToString()) + (borP * brojNoc);
            }
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
            frmSveFakture objForm2 = new frmSveFakture();
            objForm2.sifra_fakture = "";

            objForm2.MainForm = this;
            broj_fakture_edit = null;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFaktute();
            }
        }

        private void fillFaktute()
        {
            string sql = "SELECT * FROM rfakture WHERE broj='" + broj_fakture_edit + "' AND godina='" + nmGodinaFakture.Value.ToString() + "'";
            DataTable DT = RemoteDB.select(sql, "rfakture").Tables[0];

            if (DT.Rows.Count > 0)
            {
                ControlDisableEnable(0, 1, 0, 1, 0);
                ttxBrojFakture.Enabled = false;
                EnableDisable(true);
                edit = true;
                EnableDisable(false);

                if (DT.Rows[0]["zik"].ToString() == "")
                {
                    ControlDisableEnable(0, 1, 0, 1, 1);
                }

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
                sql = "SELECT * FROM rfaktura_stavke WHERE broj='" + broj_fakture_edit + "'";
                DataTable DT_stavke = RemoteDB.select(sql, "rfaktura_stavke").Tables[0];

                foreach (DataRow row in DT_stavke.Rows)
                {
                    dgw.Rows.Add();
                    int i = dgw.Rows.Count - 1;

                    string d1 = "", d2 = "";

                    if (row["broj_sobe"].ToString() == "0")
                    {
                        d1 = "---------";
                        d2 = "---------";
                    }
                    else
                    {
                        d1 = row["datumDolaska"].ToString();
                        d2 = row["datumOdlaska"].ToString();
                    }

                    dgw.Rows[i].Cells["br"].Value = i + 1;
                    dgw.Rows[i].Cells["broj_sobe"].Value = row["broj_sobe"].ToString();
                    dgw.Rows[i].Cells["datum_dolaska"].Value = d1;
                    dgw.Rows[i].Cells["datum_odlaska"].Value = d2;
                    dgw.Rows[i].Cells["avans"].Value = row["avans"].ToString();
                    dgw.Rows[i].Cells["broj_nocenja"].Value = row["dana"].ToString();
                    dgw.Rows[i].Cells["rabat"].Value = row["rabat"].ToString();
                    dgw.Rows[i].Cells["cijena_sobe"].Value = row["cijena_sobe"].ToString();
                    dgw.Rows[i].Cells["tb"].Value = row["porez"].ToString();
                    dgw.Rows[i].Cells["iznos_ukupno"].Value = row["ukupno"].ToString();
                    dgw.Rows[i].Cells["id_stavka"].Value = row["id"].ToString();
                    dgw.Rows[i].Cells["id_unos_gosta"].Value = "0";
                    dgw.Rows[i].Cells["ime"].Value = row["ime_gosta"].ToString();
                    dgw.Rows[i].Cells["id_unos_gosta"].Value = row["id_unos_gosta"].ToString();

                    int otpremnica = 0;
                    int.TryParse(row["otpremnica_broj"].ToString(), out otpremnica);
                    if (otpremnica > 0)
                        dgw.Rows[i].Cells["otpremnica"].Value = otpremnica;
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

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DataTable DT = RemoteDB.select("SELECT broj FROM Rfakture WHERE godina='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + ttxBrojFakture.Text + "'", "fakture").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojFakture() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojFakture.ReadOnly = true;
                        nmGodinaFakture.ReadOnly = true;
                        ControlDisableEnable(0, 1, 0, 1, 0);
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
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private List<string> list_broj_sobe = new List<string>();

        private void txtBrojSobe_KeyDown(object sender, KeyEventArgs e)
        {
            list_broj_sobe = new List<string>();
            list_id_unos_gosta = new List<string>();
            if (e.KeyData == Keys.Enter)
            {
                if (__DT.Columns["kol"] == null)
                {
                    __DT.Columns.Add("kol");
                    __DT.Columns.Add("porez");
                    __DT.Columns.Add("naziv");
                    __DT.Columns.Add("broj_sobe");
                    __DT.Columns.Add("id_unos");
                    __DT.Columns.Add("cijena");
                    __DT.Columns.Add("ukupno");
                    __DT.Columns.Add("od");
                    __DT.Columns.Add("do");
                    __DT.Columns.Add("avans");
                    __DT.Columns.Add("pdv");
                    __DT.Columns.Add("ostalo");
                }

                decimal decP;
                if (!decimal.TryParse(txtBrojSobe.Text, out decP))
                {
                    MessageBox.Show("Krivi unos.");
                    return;
                }

                e.SuppressKeyPress = true;

                string sql = "SELECT " +
                " sobe.broj_sobe," +
                " unos_gosta.id," +
                " unos_gosta.broj," +
                " unos_gosta.vrijeme_unosa," +
                " unos_gosta.ime_gosta," +
                " unos_gosta.datum_dolaska," +
                " unos_gosta.datum_odlaska," +
                " unos_gosta.id_agencija," +
                " unos_gosta.id_soba," +
                " unos_gosta.popust," +
                " unos_gosta.porez," +
                " unos_gosta.ukupno," +
                " unos_gosta.id_vrsta_usluge," +
                " unos_gosta.id_boravisna_pristojba," +
                " unos_gosta.id_drzava," +
                " unos_gosta.avans," +
                " unos_gosta.iznos_bor_pristojbe," +
                " unos_gosta.dorucak," +
                " unos_gosta.rucak," +
                " unos_gosta.vecera," +
                " unos_gosta.iznos_vu," +
                " unos_gosta.napomena" +
                " FROM unos_gosta" +
                " LEFT JOIN sobe ON unos_gosta.id_soba=sobe.id " +
                " WHERE sobe.broj_sobe='" + txtBrojSobe.Text + "' AND (odjava <> 1 OR odjava is null)";

                DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

                __DT.Clear();

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    double avans = Convert.ToDouble(DT.Rows[i]["avans"].ToString());
                    double popust = Convert.ToDouble(DT.Rows[i]["popust"].ToString());
                    double ukupno = Convert.ToDouble(DT.Rows[i]["ukupno"].ToString());

                    DateTime DATpocetak = Convert.ToDateTime(DT.Rows[i]["datum_dolaska"].ToString());
                    DateTime DATzavrsetak = Convert.ToDateTime(DT.Rows[i]["datum_odlaska"].ToString());
                    list_id_unos_gosta.Add(DT.Rows[i]["id"].ToString());
                    CijenovnaRazdoblja(DT, DATpocetak, DATzavrsetak, i, "broj", txtBrojSobe.Text);
                }

                for (int y = 0; y < __DT.Rows.Count; y++)
                {
                    dgw.Rows.Add(y + 1,
                        __DT.Rows[y]["broj_sobe"].ToString(),
                        __DT.Rows[y]["naziv"].ToString(),
                        __DT.Rows[y]["od"].ToString(),
                        __DT.Rows[y]["do"].ToString(),
                        Convert.ToDecimal(__DT.Rows[y]["avans"].ToString()),
                        Convert.ToDecimal(__DT.Rows[y]["kol"].ToString()),
                        0,
                        Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                        Convert.ToDecimal(__DT.Rows[y]["porez"].ToString()),
                        Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                        "",
                        "0",
                        __DT.Rows[y]["ostalo"].ToString()
                        );

                    RacunajStavku(dgw.RowCount - 1);
                    RacunajUkupno();
                    izracun();
                }

                if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
                {
                    Sve_U_Jednu_Sobu();
                }
            }
        }

        private List<string> list_id_unos_gosta = new List<string>();

        /// <summary>
        /// Ova funkcija grupira sve goste u jednu sobu
        /// </summary>
        private void Sve_U_Jednu_Sobu()
        {
            __DT.Rows.Clear();
            decimal ukupno = 0;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["ostalo"].FormattedValue.ToString() != "1")
                {
                    DataTable DT = RemoteDB.select("SELECT naziv_sobe FROM sobe WHERE broj_sobe='" + dgw.Rows[i].Cells["broj_sobe"].FormattedValue.ToString() + "'", "sobe").Tables[0];

                    DataRow[] dataROW = __DT.Select("broj_sobe = '" + dgw.Rows[i].Cells["broj_sobe"].FormattedValue.ToString() + "' AND cijena='" + dgw.Rows[i].Cells["cijena_sobe"].FormattedValue.ToString() + "'");
                    if (dataROW.Count() == 0)
                    {
                        row = __DT.NewRow();
                        row["kol"] = dgw.Rows[i].Cells["broj_nocenja"].FormattedValue.ToString();
                        row["porez"] = dgw.Rows[i].Cells["tb"].FormattedValue.ToString();
                        row["naziv"] = DT.Rows[0]["naziv_sobe"].ToString();
                        row["broj_sobe"] = dgw.Rows[i].Cells["broj_sobe"].FormattedValue.ToString();
                        row["id_unos"] = dgw.Rows[i].Cells["id_unos_gosta"].FormattedValue.ToString();
                        row["cijena"] = dgw.Rows[i].Cells["cijena_sobe"].FormattedValue.ToString();
                        row["ukupno"] = dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString();
                        row["od"] = dgw.Rows[i].Cells["datum_dolaska"].FormattedValue.ToString();
                        row["do"] = dgw.Rows[i].Cells["datum_odlaska"].FormattedValue.ToString();
                        row["avans"] = dgw.Rows[i].Cells["avans"].FormattedValue.ToString();
                        row["pdv"] = dgw.Rows[i].Cells["tb"].FormattedValue.ToString();
                        __DT.Rows.Add(row);
                    }
                    else
                    {
                        dataROW[0]["kol"] = (Convert.ToDecimal(dataROW[0]["kol"].ToString()) + Convert.ToDecimal(dgw.Rows[i].Cells["broj_nocenja"].FormattedValue.ToString()));
                        dataROW[0]["ukupno"] = (Convert.ToDecimal(dataROW[0]["ukupno"].ToString()) + Convert.ToDecimal(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString())).ToString("#0.00");
                        dataROW[0]["avans"] = (Convert.ToDecimal(dataROW[0]["avans"].ToString()) + Convert.ToDecimal(dgw.Rows[i].Cells["avans"].FormattedValue.ToString())).ToString("#0.00");
                    }
                }
            }

            int co = dgw.Rows.Count;
            for (int i = 0; i < co; i++)
            {
                for (int y = 0; y < dgw.RowCount; y++)
                {
                    if (dgw.Rows[y].Cells["ostalo"].FormattedValue.ToString() != "1")
                    {
                        dgw.Rows.RemoveAt(y);
                    }
                }
            }

            int iss = dgw.Rows.Count;
            for (int y = 0; y < __DT.Rows.Count; y++)
            {
                dgw.Rows.Add(y + 1,
                    __DT.Rows[y]["broj_sobe"].ToString(),
                    __DT.Rows[y]["naziv"].ToString(),
                    __DT.Rows[y]["od"].ToString(),
                    __DT.Rows[y]["do"].ToString(),
                    Convert.ToDecimal(__DT.Rows[y]["avans"].ToString()),
                    Funkcije.ReturnDaysFromDate(Convert.ToDateTime(__DT.Rows[y]["od"].ToString()), Convert.ToDateTime(__DT.Rows[y]["do"].ToString())),
                    0,
                    Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                    Convert.ToDecimal(__DT.Rows[y]["porez"].ToString()),
                    Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                    "",
                    __DT.Rows[y]["id_unos"].ToString(),
                    __DT.Rows[y]["ostalo"].ToString()
                    );

                RacunajStavku(dgw.RowCount - 1);
                RacunajUkupno();
                izracun();
            }
        }

        private void CijenovnaRazdoblja(DataTable DT, DateTime OD, DateTime DO, int broj_gosta, string id_sobaORbroj_sobe, string broj_sobe)
        {
            decimal tecaj = 0;
            decimal cijena_sobe = 0;
            decimal broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

            DateTime dat = OD;
            ukupno_stavka = 0;
            string d_od = "";

            for (int i = 0; i < broj_nocenja; i++)
            {
                string sql = "";
                if (id_sobaORbroj_sobe == "broj")
                {
                    sql = "SELECT r_cijenasoba.cijena_nocenja,valute.tecaj,r_cijenasoba.od_datuma,r_cijenasoba.do_datuma FROM r_cijenasoba " +
                    " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                    " WHERE id_soba='" + broj_sobe + "'" +
                    " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'>=r_cijenasoba.od_datuma " +
                    " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'<=r_cijenasoba.do_datuma";
                }
                else
                {
                    sql = "SELECT r_cijenasoba.cijena_nocenja,valute.tecaj,r_cijenasoba.od_datuma,r_cijenasoba.do_datuma FROM r_cijenasoba " +
                    " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                    " WHERE id_soba='" + broj_sobe + "'" +
                    " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'>=r_cijenasoba.od_datuma " +
                    " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'<=r_cijenasoba.do_datuma";
                }

                DataTable DTcijene = RemoteDB.select(sql, "r_cijenasoba").Tables[0];
                decimal broj_osoba = DT.Rows.Count;
                if (DTcijene.Rows.Count > 0)
                {
                    tecaj = Convert.ToDecimal(DTcijene.Rows[0]["tecaj"].ToString());
                    cijena_sobe = (Convert.ToDecimal(DTcijene.Rows[0]["cijena_nocenja"].ToString()) * tecaj);
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100));
                }
                else
                {
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100));
                }

                int zadnji = dgw.RowCount;

                //**********************************OVDJE ULAZI AKO PRVI PUT PUNI DATAGRID ILI AKO JE NASTALA NOVA CIJENA***********************************************
                //|| dgw.Rows[zadnji - 1].Cells["id_unos_gosta"].FormattedValue.ToString() != DT.Rows[broj_gosta]["id"].ToString()
                if (zadnji == 0 || dgw.Rows[zadnji - 1].Cells["cijena_sobe"].FormattedValue.ToString() != Math.Round(cijena_sobe, 3).ToString() || dgw.Rows[zadnji - 1].Cells["id_unos_gosta"].FormattedValue.ToString() != DT.Rows[broj_gosta]["id"].ToString())
                {
                    d_od = zadnji == 0 ? OD.ToString() : dat.ToString();
                    ukupno_stavka = 0;
                    ukupno_stavka = ((cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)));

                    dgw.Rows.Add(dgw.Rows.Count,
                            DT.Rows[broj_gosta]["broj_sobe"].ToString(),
                            DT.Rows[broj_gosta]["ime_gosta"].ToString(),
                            d_od,
                            DO.ToString(),
                            Convert.ToDecimal(DT.Rows[broj_gosta]["avans"].ToString()),
                            //DT.Rows[broj_gosta]["iznos_bor_pristojbe"].ToString(),
                            ReturnDaysFromDate(Convert.ToDateTime(d_od), DO),
                            Convert.ToDecimal(DT.Rows[broj_gosta]["popust"].ToString()),
                            //Convert.ToDecimal(DT.Rows[broj_gosta]["iznos_vu"].ToString()),
                            Convert.ToDecimal(Math.Round(cijena_sobe, 3)),
                            Convert.ToDecimal(DT.Rows[broj_gosta]["porez"].ToString()),
                            Convert.ToDecimal(DT.Rows[broj_gosta]["ukupno"].ToString()),
                            "",
                            DT.Rows[broj_gosta]["id"].ToString()
                           );

                    if (zadnji == 0) { RacunajStavku(0); } else { RacunajStavku(zadnji); }
                    RacunajStavku(zadnji - 1);
                    izracun();
                    RacunajUkupno();
                }
                else
                {
                    dgw.Rows[zadnji - 1].Cells["datum_odlaska"].Value = dat.AddDays(1).ToString();
                    dgw.Rows[zadnji - 1].Cells["iznos_ukupno"].Value = ukupno_stavka;
                    dgw.Rows[zadnji - 1].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[zadnji - 1].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Parse(dgw.Rows[zadnji - 1].Cells["datum_odlaska"].FormattedValue.ToString()));
                    RacunajStavku(zadnji - 1);
                    izracun();
                    RacunajUkupno();
                }

                if (Convert.ToDecimal(DT.Rows[broj_gosta]["iznos_bor_pristojbe"].ToString()) > 0)
                {
                    DodatneStavke(
                    1,
                    "0",
                    "BORAVIŠNA PRISTOJBA",
                   "-1",
                    DT.Rows[broj_gosta]["id"].ToString(),
                    Convert.ToDecimal(DT.Rows[broj_gosta]["iznos_bor_pristojbe"].ToString()),
                    "---------",
                    "---------",
                    Convert.ToDecimal(0),
                    0, "1");
                }

                if (Convert.ToDecimal(DT.Rows[broj_gosta]["iznos_vu"].ToString()) > 0)
                {
                    DodatneStavke(
                    1,
                    DT.Rows[broj_gosta]["porez"].ToString(),
                    "USLUGA",
                    DT.Rows[broj_gosta]["broj_sobe"].ToString(),
                    DT.Rows[broj_gosta]["id"].ToString(),
                    Convert.ToDecimal(DT.Rows[broj_gosta]["iznos_vu"].ToString()),
                    "---------",
                    "---------",
                    Convert.ToDecimal(0),
                    0, "1");
                }

                dat = dat.AddDays(1);
                RacunajStavku(zadnji - 1);
                RacunajUkupno();
                izracun();
            }//for
        }

        private int ReturnDaysFromDate(DateTime Date1, DateTime Date2)
        {
            TimeSpan ts = Date2 - Date1;
            int differenceInDays = ts.Days;
            return differenceInDays;
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString() == "---------")
                {
                    return;
                }

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
                    RacunajUkupno();
                }
            }
            else if (e.ColumnIndex == 4)
            {
                if (dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString() == "---------")
                {
                    return;
                }

                try
                {
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_odlaska"].FormattedValue.ToString()));
                    RacunajStavku(dgw.CurrentRow.Index);
                    RacunajUkupno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Krivi unos datuma." + ex.ToString());
                    dgw.Rows[e.RowIndex].Cells[4].Value = DateTime.Now.ToString();
                    dgw.Rows[e.RowIndex].Cells["broj_nocenja"].Value = ReturnDaysFromDate(DateTime.Parse(dgw.Rows[e.RowIndex].Cells["datum_dolaska"].FormattedValue.ToString()), DateTime.Now);
                    RacunajStavku(dgw.CurrentRow.Index);
                    RacunajUkupno();
                }
            }
            else if (e.ColumnIndex == 5)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
            else if (e.ColumnIndex == 6)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
            else if (e.ColumnIndex == 7)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
            else if (e.ColumnIndex == 8)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
            else if (e.ColumnIndex == 9)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
            else if (e.ColumnIndex == 10)
            {
                RacunajStavku(dgw.CurrentRow.Index);
                RacunajUkupno();
            }
        }

        private decimal rabat_stavka = 0;
        private decimal avans_ukupno = 0;
        private decimal ukupno_stavka = 0;
        private decimal cijena_sobe_iznos = 0;
        private decimal broj_nocenja_iznos = 0;

        private void RacunajStavku(int row)
        {
            if (row >= 0)
            {
                decimal dec;
                if (!decimal.TryParse(dgw.Rows[row].Cells["avans"].FormattedValue.ToString().Replace(".", ","), out dec))
                {
                    dgw.Rows[row].Cells["avans"].Value = "0";
                }
                else
                {
                    dgw.Rows[row].Cells["avans"].Value = dgw.Rows[row].Cells["avans"].FormattedValue.ToString().Replace(".", ",");
                }

                if (!decimal.TryParse(dgw.Rows[row].Cells["rabat"].FormattedValue.ToString().Replace(".", ","), out dec))
                {
                    dgw.Rows[row].Cells["rabat"].Value = "0";
                }
                else
                {
                    dgw.Rows[row].Cells["rabat"].Value = dgw.Rows[row].Cells["rabat"].FormattedValue.ToString().Replace(".", ",");
                }

                if (!decimal.TryParse(dgw.Rows[row].Cells["cijena_sobe"].FormattedValue.ToString().Replace(".", ","), out dec))
                {
                    dgw.Rows[row].Cells["cijena_sobe"].Value = "0";
                }
                else
                {
                    dgw.Rows[row].Cells["cijena_sobe"].Value = dgw.Rows[row].Cells["cijena_sobe"].FormattedValue.ToString().Replace(".", ",");
                }

                if (!decimal.TryParse(dgw.Rows[row].Cells["broj_nocenja"].FormattedValue.ToString().Replace(".", ","), out dec))
                {
                    dgw.Rows[row].Cells["broj_nocenja"].Value = "0";
                }
                else
                {
                    dgw.Rows[row].Cells["broj_nocenja"].Value = dgw.Rows[row].Cells["broj_nocenja"].FormattedValue.ToString().Replace(".", ",");
                }

                if (Convert.ToInt32(dgw.Rows[row].Cells["broj_sobe"].Value) == -1)
                {
                    dgw.Rows[row].Cells["broj_sobe"].ReadOnly = true;
                    dgw.Rows[row].Cells["ime"].ReadOnly = true;
                }

                broj_nocenja_iznos = Convert.ToDecimal(dgw.Rows[row].Cells["broj_nocenja"].FormattedValue.ToString().Replace(".", ","));
                rabat_stavka = Convert.ToDecimal(dgw.Rows[row].Cells["rabat"].FormattedValue.ToString().Replace(".", ","));
                avans_ukupno = Convert.ToDecimal(dgw.Rows[row].Cells["avans"].FormattedValue.ToString().Replace(".", ","));
                cijena_sobe_iznos = Convert.ToDecimal(dgw.Rows[row].Cells["cijena_sobe"].FormattedValue.ToString().Replace(".", ",")) * broj_nocenja_iznos;

                ukupno_stavka = (cijena_sobe_iznos - (((cijena_sobe_iznos) * rabat_stavka) / 100));
                dgw.Rows[row].Cells["iznos_ukupno"].Value = ukupno_stavka.ToString("#0.00");
            }
        }

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    Forme.sifarnici.frmPartnerTrazi partnerTrazi = new Forme.sifarnici.frmPartnerTrazi();
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        int otpremnica = 0;
                        int.TryParse(dgw.CurrentRow.Cells["otpremnica"].FormattedValue.ToString(), out otpremnica);

                        if (otpremnica > 0)
                        {
                            if (MessageBox.Show(string.Format("Stavka dodana kroz otpremnicu.{0}Brisanjem ove stavke, brišu se i ostale stavke dodane otpremnicom.{0}Sigurno želite nastaviti?", Environment.NewLine), "Brisanje stavke.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            foreach (DataGridViewRow item in dgw.Rows)
                            {
                                int otpremnica_stavke = 0;
                                int.TryParse(item.Cells["otpremnica"].ToString(), out otpremnica_stavke);
                                if (otpremnica_stavke == otpremnica)
                                {
                                    RemoteDB.delete(string.Format("DELETE FROM rfaktura_stavke WHERE id = '{0}';", item.Cells["id_stavka"].FormattedValue.ToString()));
                                }
                            }

                            updateOtpremnicaNaplataFakturom(0, otpremnica);
                        }
                        else
                        {
                            RemoteDB.delete("DELETE FROM rfaktura_stavke WHERE id='" + dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString() + "'");
                        }
                    }
                }
                RemoteDB.update("UPDATE unos_gosta SET odjava='0' WHERE id='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_unos_gosta"].FormattedValue.ToString() + "'");
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                izracun();
                RacunajUkupno();
                MessageBox.Show("Obrisano");
            }
        }

        private void txtSifraOdrediste_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //if (sender is TextBox)
                //{
                //    TextBox txt = ((TextBox)sender);

                //}
                //else
                //{
                //}

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
                    txtBrojSobe.Select();
                }
            }
        }

        private DataTable __DT = new DataTable();
        private DataRow row;

        private void txtBrojUnosa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                list_broj_sobe = new List<string>();
                list_id_unos_gosta = new List<string>();

                if (__DT.Columns["kol"] == null)
                {
                    __DT.Columns.Add("kol");
                    __DT.Columns.Add("porez");
                    __DT.Columns.Add("naziv");
                    __DT.Columns.Add("broj_sobe");
                    __DT.Columns.Add("id_unos");
                    __DT.Columns.Add("cijena");
                    __DT.Columns.Add("ukupno");
                    __DT.Columns.Add("od");
                    __DT.Columns.Add("do");
                    __DT.Columns.Add("avans");
                    __DT.Columns.Add("pdv");
                    __DT.Columns.Add("ostalo");
                }

                e.SuppressKeyPress = true;

                string sql = "SELECT " +
                " sobe.broj_sobe," +
                " unos_gosta.id," +
                " unos_gosta.broj," +
                " unos_gosta.vrijeme_unosa," +
                " unos_gosta.ime_gosta," +
                " unos_gosta.datum_dolaska," +
                " unos_gosta.datum_odlaska," +
                " unos_gosta.id_agencija," +
                " unos_gosta.id_soba," +
                " unos_gosta.popust," +
                " unos_gosta.porez," +
                " unos_gosta.ukupno," +
                " unos_gosta.id_vrsta_usluge," +
                " unos_gosta.id_boravisna_pristojba," +
                " unos_gosta.id_drzava," +
                " unos_gosta.avans," +
                " unos_gosta.iznos_bor_pristojbe," +
                " unos_gosta.dorucak," +
                " unos_gosta.rucak," +
                " unos_gosta.vecera," +
                " unos_gosta.iznos_vu," +
                " unos_gosta.cijena_sobe," +
                " unos_gosta.napomena" +
                " FROM unos_gosta" +
                " LEFT JOIN sobe ON unos_gosta.id_soba=sobe.id " +
                " WHERE unos_gosta.broj='" + txtBrojUnosa.Text + "' AND (odjava <> 1 OR odjava is null)";

                DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    double avans = Convert.ToDouble(DT.Rows[i]["avans"].ToString());
                    double popust = Convert.ToDouble(DT.Rows[i]["popust"].ToString());
                    double ukupno = Convert.ToDouble(DT.Rows[i]["ukupno"].ToString());

                    DateTime DATpocetak = Convert.ToDateTime(DT.Rows[i]["datum_dolaska"].ToString());
                    DateTime DATzavrsetak = Convert.ToDateTime(DT.Rows[i]["datum_odlaska"].ToString());
                    int brojNocenja = ReturnDaysFromDate(DATpocetak, DATzavrsetak);
                    list_id_unos_gosta.Add(DT.Rows[i]["id"].ToString());
                    CijenovnaRazdoblja(DT, DATpocetak, DATzavrsetak, i, "id", DT.Rows[i]["broj_sobe"].ToString());
                }

                /////////////////////////////////OSTALE USLUGE I DODATNE STAVKE////////////////////////////////////////////////////
                for (int y = 0; y < __DT.Rows.Count; y++)
                {
                    dgw.Rows.Add(y + 1,
                        __DT.Rows[y]["broj_sobe"].ToString(),
                        __DT.Rows[y]["naziv"].ToString(),
                        __DT.Rows[y]["od"].ToString(),
                        __DT.Rows[y]["do"].ToString(),
                        Convert.ToDecimal(__DT.Rows[y]["avans"].ToString()),
                        __DT.Rows[y]["kol"].ToString(),
                        0,
                        Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                        Convert.ToDecimal(__DT.Rows[y]["porez"].ToString()),
                        Convert.ToDecimal(__DT.Rows[y]["cijena"].ToString()),
                        "",
                        "0",
                        __DT.Rows[y]["ostalo"].ToString()
                        );

                    RacunajStavku(dgw.RowCount - 1);
                    RacunajUkupno();
                    izracun();
                }

                if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
                {
                    Sve_U_Jednu_Sobu();
                }
            }
        }

        private void DodatneStavke(decimal kol, string porez, string naziv, string broj_sobe, string id_unos, decimal cijena, string _od, string _do, decimal avans, int pdv, string ostalo)
        {
            DataRow[] dataROW = __DT.Select("naziv = '" + naziv + "' AND cijena='" + cijena + "'");

            if (dataROW.Count() == 0)
            {
                row = __DT.NewRow();
                row["kol"] = kol;
                row["porez"] = porez;
                row["naziv"] = naziv;
                row["broj_sobe"] = broj_sobe;
                row["id_unos"] = id_unos;
                row["cijena"] = cijena;
                row["ukupno"] = cijena;
                row["od"] = _od;
                row["do"] = _do;
                row["avans"] = avans;
                row["pdv"] = pdv;
                row["ostalo"] = ostalo;
                __DT.Rows.Add(row);
            }
            else
            {
                dataROW[0]["kol"] = (Convert.ToDecimal(dataROW[0]["kol"].ToString()) + kol);
                dataROW[0]["ukupno"] = (Convert.ToDecimal(dataROW[0]["ukupno"].ToString()) + cijena).ToString("#0.00");
            }
        }

        public string __broj_sobe { get; set; }
        public string __broj_unosa { get; set; }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Forme.frmPopisSoba ps = new frmPopisSoba();
            ps.__broj_sobe = null;
            ps._FAKTURA = this;

            __broj_sobe = null;
            ps.ShowDialog();

            if (__broj_sobe != null)
            {
                txtBrojSobe.Select();
                txtBrojSobe.Text = __broj_sobe;
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Enter);
            }
        }

        public static class VirtualKeyboard
        {
            [DllImport("user32.dll")]
            private static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            public static void KeyDown(System.Windows.Forms.Keys key)
            {
                keybd_event((byte)key, 0, 0, 0);
            }

            public static void KeyUp(System.Windows.Forms.Keys key)
            {
                keybd_event((byte)key, 0, 0x7F, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Forme.frmPopisUnosa ps = new frmPopisUnosa();
            ps.__broj_unosa = null;
            ps._FAKTURA = this;

            __broj_unosa = null;
            ps.ShowDialog();

            if (__broj_unosa != null)
            {
                txtBrojUnosa.Select();
                txtBrojUnosa.Text = __broj_unosa;
                VirtualKeyboard.KeyDown(System.Windows.Forms.Keys.Enter);
            }
        }

        private void btnObrišiSve_Click(object sender, EventArgs e)
        {
            //int totalCount = dgw.Rows.Cast<DataGridViewRow>().Count(r => r.Cells["id_stavka"].FormattedValue.ToString() != "");

            //if (totalCount > 0) {
            //    updateOtpremnicaNaplataFakturom(Convert.ToInt32(ttxBrojFakture.Text));
            //}

            if (dgw.RowCount > 0)
                dgw.Rows.Clear();

            RacunajUkupno();
        }

        private List<int> l_id_unos = new List<int>();
        private bool sve_u_jednu_stavku = false;

        private void btnSveUjednuStavku_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
            {
                MessageBox.Show("Greška,\r\nnemate nijednu stavku.", "Greška");
                return;
            }

            if (MessageBox.Show("Potvrdom ove poruke spajate sve unose iz tablice u jedno polje.\r\n\r\nDali ste sigurni da želite spojite ove unose?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int _br_nocenja = 0;
                decimal _cijena_sobe = 0;
                decimal _ukupno = 0;
                decimal _pdv = 0;
                decimal _avans = 0;
                decimal _rabat = 0;
                decimal _iznos_usluge = 0;
                decimal _boravisna_pristojba = 0;

                int i = 0;
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    l_id_unos.Add(Convert.ToInt32(row.Cells["id_unos_gosta"].FormattedValue.ToString()));

                    _cijena_sobe = Convert.ToDecimal(row.Cells["cijena_sobe"].FormattedValue.ToString()) + _cijena_sobe;
                    _pdv = Convert.ToInt32(row.Cells["tb"].FormattedValue.ToString());
                    _br_nocenja = Convert.ToInt16(row.Cells["broj_nocenja"].FormattedValue.ToString()) + _br_nocenja;
                    _ukupno = Convert.ToDecimal(row.Cells["iznos_ukupno"].FormattedValue.ToString()) + _ukupno;
                    _avans = Convert.ToDecimal(row.Cells["avans"].FormattedValue.ToString()) + _avans;
                    _rabat = Convert.ToDecimal(row.Cells["rabat"].FormattedValue.ToString()) + _rabat;
                    i++;
                }

                dgw.Rows.Clear();
                dgw.Rows.Add("1", "0", "Sveukupna stavka", DateTime.Now.ToString(), DateTime.Now.ToString(), _avans.ToString(), 1, (_rabat / i).ToString(), _ukupno, _pdv, _ukupno, "", "0");

                sve_u_jednu_stavku = true;
                dgw.Columns[3].Visible = false;
                dgw.Columns[4].Visible = false;
            }
        }

        private void SetOdjava()
        {
            for (int i = 0; i < l_id_unos.Count; i++)
            {
                RemoteDB.update("UPDATE unos_gosta SET odjava='1' WHERE id='" + l_id_unos[i] + "'");
            }
        }

        private void btnDodatanLežaj_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                string d1 = dgw.Rows[0].Cells[3].FormattedValue.ToString();
                string d2 = dgw.Rows[0].Cells[4].FormattedValue.ToString();
                dgw.Rows.Add(dgw.Rows.Count + 1, "0", "Dodatna stavka", d1, d2, "0", "0", "0", "0", "0", "0", "", "0");
            }
            else
            {
                dgw.Rows.Add(dgw.Rows.Count + 1, "0", "Dodatna stavka", "---------", "---------", "0", "0", "0", "0", "0", "0", "", "0");
            }

            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
            dgw.BeginEdit(true);
        }

        private void btnDodajBoravisnuPristojbu_Click(object sender, EventArgs e)
        {
            try
            {
                var t = (from d in dgw.Rows.Cast<DataGridViewRow>() where Convert.ToInt32(d.Cells["broj_sobe"].Value.ToString()) == -1 select d).FirstOrDefault();
                if (t != null)
                {
                    if (MessageBox.Show("Boravišna pristojba već postoji na računu.\r\nŽelite dodati novu?", "Boravišna pristojba", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;
                }

                dgw.Rows.Add(dgw.Rows.Count + 1, "-1", "BORAVIŠNA PRISTOJBA", "---------", "---------", "0", "0", "0", "7", "0", "0", "", "0");
                RacunajStavku(dgw.RowCount - 1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void pbOtpremnice_Click(object sender, EventArgs e)
        {
            try
            {
                frmSveOtpremnice f = new frmSveOtpremnice();
                if (f.ShowDialog() == DialogResult.Yes)
                {
                    foreach (DataGridViewRow dRow in f.dgvOtpremnice.Rows)
                    {
                        if (!Convert.ToBoolean(dRow.Cells["dodaj"].Value))
                            continue;

                        string sql = string.Format(@"select os.*, r.naziv
from otpremnice o
left join otpremnica_stavke os on o.broj_otpremnice = os.broj_otpremnice
left join roba r on os.sifra_robe = r.sifra
where o.broj_otpremnice = {0};", dRow.Cells["broj"].Value);

                        DataSet dsOtpremnicaStavke = RemoteDB.select(sql, "otpremnica_stavke");
                        if (dsOtpremnicaStavke != null && dsOtpremnicaStavke.Tables.Count > 0 && dsOtpremnicaStavke.Tables[0] != null && dsOtpremnicaStavke.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsOtpremnicaStavke.Tables[0].Rows)
                            {
                                decimal kolicina = 0, vpc = 0, porez = 0, rabat = 0, porez_potrosnja = 0;
                                decimal.TryParse(row["kolicina"].ToString(), out kolicina);
                                decimal.TryParse(row["vpc"].ToString(), out vpc);
                                decimal.TryParse(row["porez"].ToString(), out porez);
                                decimal.TryParse(row["rabat"].ToString(), out rabat);
                                decimal.TryParse(row["porez_potrosnja"].ToString(), out porez_potrosnja);

                                decimal mpc = Math.Round((vpc * (1 + ((porez + porez_potrosnja) / 100))), 2, MidpointRounding.AwayFromZero);
                                mpc = Math.Round((mpc - (mpc * rabat / 100)), 2, MidpointRounding.AwayFromZero);

                                dgw.Rows.Add(dgw.Rows.Count + 1, f.txtBrojSobe.Text, row["naziv"].ToString(), "---------", "---------", "0", row["kolicina"], row["rabat"], mpc.ToString(), (int)porez, "0", "", "0", "", row["broj_otpremnice"], row["porez_potrosnja"]);
                                RacunajStavku(dgw.RowCount - 1);
                                RacunajUkupno();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}