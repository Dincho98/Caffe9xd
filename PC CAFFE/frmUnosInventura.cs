using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmUnosInventura : Form
    {
        public frmUnosInventura()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string broj_inventure_edit { get; set; }
        private DataTable DTRoba;
        private DataSet DS_Skladiste;
        private DataTable DTIzradio;
        private bool edit = false;
        public frmMenu MainFormMenu { get; set; }

        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        private void frmInventura_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            PaintRows(dgw);
            SetCB();
            numeric();
            txtBrojInventure.Text = brojInventure();
            txtSifra_robe.Enabled = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_inventure_edit != null) { fillInventura(); }

            if (DTtvrtka.Rows[0]["oib"].ToString() == "05593216962" || DTtvrtka.Rows[0]["oib"].ToString() == "77566209058")
            {
                txtSifra_robe.ReadOnly = true;
                pictureBox1.Enabled = false;
                dgw.Columns[5].ReadOnly = true;
            }

            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmUnosInventura MainForm { get; set; }

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
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
                int curent = d.CurrentRow.Index;
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 4)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[4];
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

        private void numeric()
        {
            nmGodinaInventure.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaInventure.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaInventure.Value = DateTime.Now.Year;
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select("SELECT ime+' '+prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
            txtIzradio.Text = DTIzradio.Rows[0]["Ime"].ToString();
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            cbSkladiste.DataSource = DS_Skladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private string brojInventure()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_inventure AS INT)) FROM inventura", "inventura").Tables[0];
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
                e.SuppressKeyPress = true;

                if (DTtvrtka.Rows[0]["oib"].ToString() == "05593216962" || DTtvrtka.Rows[0]["oib"].ToString() == "77566209058")
                {
                    return;
                }

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT roba_prodaja.mjera,roba_prodaja.naziv,roba_prodaja.id_skladiste,roba_prodaja.kolicina,roba_prodaja.nc," +
                    " roba_prodaja.ulazni_porez,roba_prodaja.sifra,roba.mpc,roba_prodaja.povratna_naknada " +
                    " FROM roba_prodaja " +
                    " LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra" +
                    " WHERE roba_prodaja.sifra='" + txtSifra_robe.Text + "' AND roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "'";

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                    dgw.BeginEdit(true);
                    txtBrojInventure.ReadOnly = true;
                    nmGodinaInventure.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["mjera"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["cijena"].Value = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
            dgw.Rows[br].Cells["KolicinaNaSk"].Value = DTRoba.Rows[0]["kolicina"].ToString();
            dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["ulazni_porez"].ToString();

            dgw.Rows[br].Cells["mpc"].Value = DTRoba.Rows[0]["mpc"].ToString();
            dgw.Rows[br].Cells["povratna_naknada"].Value = DTRoba.Rows[0]["povratna_naknada"].ToString();

            PaintRows(dgw);
        }

        private void dgw_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var txtEdit = (TextBox)e.Control;
            txtEdit.KeyDown += EditKeyDown;
        }

        private void EditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgw.CurrentRow.Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString()) * Convert.ToDouble(dgw.CurrentRow.Cells["cijena"].FormattedValue.ToString())));
                txtSifra_robe.Select();
                base.OnKeyDown(e);
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal kol, cijena;
            if (dgw.CurrentCell.ColumnIndex == 4 && dgw.CurrentCell.FormattedValue.ToString() != "")
            {
                decimal.TryParse(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString(), out kol);
                dgw.CurrentRow.Cells["kolicina"].Value = Math.Round(kol, 5).ToString("#0.0000");

                if (decimal.TryParse(dgw.CurrentRow.Cells["cijena"].FormattedValue.ToString(), out cijena))
                {
                    dgw.CurrentRow.Cells["iznos"].Value = Math.Round((kol * cijena), 5).ToString("#0.0000");
                }
                else
                {
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    dgw.CurrentCell.Value = 0;
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                decimal nab;
                decimal.TryParse(dgw.CurrentRow.Cells["cijena"].FormattedValue.ToString(), out nab);
                dgw.CurrentRow.Cells["cijena"].Value = Math.Round(nab, 5).ToString("#0.0000");
                txtSifra_robe.Text = "";
                txtSifra_robe.Select();
                dgw.ClearSelection();
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            this.TopMost = true;
            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");

            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                {
                    MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string sql = "SELECT roba_prodaja.mjera,roba_prodaja.naziv,roba_prodaja.id_skladiste,roba_prodaja.kolicina,roba_prodaja.nc," +
                " roba_prodaja.ulazni_porez,roba_prodaja.sifra,roba_prodaja.povratna_naknada,roba.mpc" +
                " FROM roba_prodaja " +
                " LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra" +
                " WHERE roba_prodaja.sifra='" + propertis_sifra + "' AND roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "'";

            DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                txtSifra_robe.BackColor = Color.White;
                SetRoba();
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                dgw.BeginEdit(true);
                txtBrojInventure.ReadOnly = true;
                nmGodinaInventure.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.CurrentRow.Cells["id_stavka"].Value != null && dgw.CurrentRow.Cells["id_stavka"].Value.ToString() != "")
            {
                classSQL.update("UPDATE inventura SET editirano='1' WHERE broj_inventure='" + txtBrojInventure.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'");
                classSQL.delete("DELETE FROM inventura_stavke WHERE id_stavke='" + dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString() + "'");
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke.Inventura br." + txtBrojInventure.Text + "')"));
            }

            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            MessageBox.Show("Obrisano.");
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 4 || dgw.CurrentCell.ColumnIndex == 5)
            {
                dgw.BeginEdit(true);
            }
        }

        private void EnableDisable(bool x)
        {
            if (x == true)
            {
                txtBrojInventure.Enabled = false;
                nmGodinaInventure.Enabled = false;
            }
            else
            {
                txtBrojInventure.Enabled = true;
                nmGodinaInventure.Enabled = true;
            }

            btnUcitajSveStavke.Enabled = x;
            btnUcitajSvuHranu.Enabled = x;
            btnSamoRepromaterijal.Enabled = x;
            btnSamoTrgRobu.Enabled = x;
            txtSifra_robe.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
        }

        private void deleteFields()
        {
            dgw.Rows.Clear();
            //txtBrojInventure.Text = brojInventure();
            nmGodinaInventure.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
            rtbNapomena.Text = "";
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

            if (edit == true)
            {
                UpdateInventura();
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            txtBrojInventure.Text = brojInventure();

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_inventure");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("jmj");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("KolicinaNaSk");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            string sql = "INSERT INTO inventura (broj_inventure,id_skladiste,datum,id_zaposlenik,napomena,godina,novo,is_pocetno_stanje) VALUES (" +
                "'" + txtBrojInventure.Text + "'," +
                "'" + cbSkladiste.SelectedValue + "'," +
                "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + DTIzradio.Rows[0]["id_zaposlenik"].ToString() + "'," +
                "'" + rtbNapomena.Text + "'," +
                "'" + nmGodinaInventure.Value.ToString() + "','1','0'" +
                ")";
            classSQL.insert(sql);

            decimal kol;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                decimal.TryParse(dg(i, "kolicina"), out kol);

                row = DTsend.NewRow();
                row["broj_inventure"] = txtBrojInventure.Text;
                row["sifra_robe"] = dg(i, "sifra");
                row["jmj"] = dg(i, "jmj");
                row["kolicina"] = kol.ToString().Replace(".", ",");
                row["KolicinaNaSk"] = dg(i, "KolicinaNaSk");
                row["cijena"] = dg(i, "cijena");
                row["naziv"] = dg(i, "naziv");
                row["porez"] = dg(i, "porez");
                row["mpc"] = dg(i, "mpc");
                row["povratna_naknada"] = dg(i, "povratna_naknada");
                DTsend.Rows.Add(row);
                try
                {
                    //classSQL.update("UPDATE roba_prodaja SET nc='" + dg(i, "cijena").Replace(",", ".") + "'" +
                    //" WHERE sifra='" + dg(i, "sifra") + "' AND"+
                    //" id_skladiste='"+cbSkladiste.SelectedValue.ToString()+"'");
                }
                catch { }
            }
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova inventura br." + txtBrojInventure.Text + "')"));
            SQL.SQLinventura.InsertStavke(DTsend);
            deleteFields();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            deleteFields();
            txtBrojInventure.Text = brojInventure();
            EnableDisable(true);
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            edit = false;
            deleteFields();
            EnableDisable(false);
            txtBrojInventure.ReadOnly = false;
            nmGodinaInventure.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void SveInventure_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmSveInventure objForm2 = new frmSveInventure();
            objForm2.broj__inventure = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            this.TopMost = true;
            if (broj_inventure_edit != null)
            {
                fillInventura();
                EnableDisable(true);
                edit = true;
            }
        }

        private void fillInventura()
        {
            string sql = "SELECT * FROM inventura WHERE broj_inventure='" + broj_inventure_edit + "'";
            DataTable DTinventura = classSQL.select(sql, "inventura").Tables[0];
            if (DTinventura.Rows.Count == 0)
                return;

            DateTime _DT;
            DateTime.TryParse(DTinventura.Rows[0]["datum"].ToString(), out _DT);

            string ovl = Util.Korisno.UzmiOvlastTrenutnogZaposlenika();
            int br_days = 366;
            if (ovl == "user")
                br_days = 30;

            if (Util.Korisno.ZabranaUređivanjaDokumenta(br_days, _DT, ovl))
            {
                MessageBox.Show("Nemate ovlaštenje uređivati ovaj dokumenat nakon " + br_days + " dana od izrade istog.");
                return;
            }

            if (DTinventura.Rows[0]["zakljucano"].ToString() == "1")
            {
                MessageBox.Show("Ova inventura je proknjižena, ne možete pristupati istoj.");
                return;
            }

            edit = true;
            dgw.Rows.Clear();

            if (DTinventura.Rows.Count == 0)
            {
                return;
            }

            txtBrojInventure.Text = DTinventura.Rows[0]["broj_inventure"].ToString();
            nmGodinaInventure.Value = Convert.ToInt16(DTinventura.Rows[0]["godina"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTinventura.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTinventura.Rows[0]["napomena"].ToString();
            cbSkladiste.SelectedValue = DTinventura.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik='" + DTinventura.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            string sql1 = "SELECT * FROM inventura_stavke WHERE broj_inventure='" + broj_inventure_edit + "'";

            DataTable DTinventura_stavke = classSQL.select(sql1, "inventura_stavke").Tables[0];

            for (int br = 0; br < DTinventura_stavke.Rows.Count; br++)
            {
                dgw.Rows.Add();

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTinventura_stavke.Rows[br]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DTinventura_stavke.Rows[br]["porez"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTinventura_stavke.Rows[br]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = DTinventura_stavke.Rows[br]["jmj"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DTinventura_stavke.Rows[br]["kolicina"].ToString();
                dgw.Rows[br].Cells["KolicinaNaSk"].Value = DTinventura_stavke.Rows[br]["kolicina_koja_je_bila_na_skl"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DTinventura_stavke.Rows[br]["id_stavke"].ToString();
                dgw.Rows[br].Cells["cijena"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString()));
                dgw.Rows[br].Cells["iznos"].Value = String.Format("{0:0.00}", Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString()) * Convert.ToDouble(DTinventura_stavke.Rows[br]["kolicina"].ToString()));
                dgw.Rows[br].Cells["mpc"].Value = DTinventura_stavke.Rows[br]["mpc"].ToString();
                dgw.Rows[br].Cells["povratna_naknada"].Value = DTinventura_stavke.Rows[br]["povratna_naknada"].ToString();
            }
            PaintRows(dgw);
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisai ovu inventuru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM inventura_stavke WHERE broj_inventure='" + txtBrojInventure.Text + "'");
                classSQL.delete("UPDATE inventura SET editirano='1',is_pocetno_stanje='0', obrisano = '1' WHERE broj_inventure='" + txtBrojInventure.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele inventure br." + txtBrojInventure.Text + "')");
                MessageBox.Show("Obrisano.");
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele inventure br." + txtBrojInventure.Text + "')"));

                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void UpdateInventura()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_inventure");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("jmj");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("KolicinaNaSk");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            DataTable DTsend1 = new DataTable();
            DTsend1.Columns.Add("broj_inventure");
            DTsend1.Columns.Add("sifra_robe");
            DTsend1.Columns.Add("jmj");
            DTsend1.Columns.Add("kolicina");
            DTsend1.Columns.Add("KolicinaNaSk");
            DTsend1.Columns.Add("cijena");
            DTsend1.Columns.Add("naziv");
            DTsend1.Columns.Add("porez");
            DTsend1.Columns.Add("mpc");
            DTsend1.Columns.Add("povratna_naknada");
            DataRow row1;

            string sql = "UPDATE inventura SET " +
             " id_skladiste='" + cbSkladiste.SelectedValue + "'," +
             " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
             " napomena='" + rtbNapomena.Text + "'," +
             " editirano='1', " +
             " godina='" + nmGodinaInventure.Value.ToString() + "'" +
             " WHERE broj_inventure='" + txtBrojInventure.Text + "'";
            classSQL.update(sql);

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    row = DTsend.NewRow();
                    row["broj_inventure"] = txtBrojInventure.Text;
                    row["sifra_robe"] = dg(i, "sifra");
                    row["jmj"] = dg(i, "jmj");
                    row["kolicina"] = dg(i, "kolicina");
                    row["KolicinaNaSk"] = dg(i, "KolicinaNaSk");
                    row["cijena"] = dg(i, "cijena");
                    row["naziv"] = dg(i, "naziv");
                    row["porez"] = dg(i, "porez");
                    row["mpc"] = dg(i, "mpc");
                    row["povratna_naknada"] = dg(i, "povratna_naknada");
                    DTsend.Rows.Add(row);
                }
                else
                {
                    row1 = DTsend1.NewRow();
                    row1["broj_inventure"] = txtBrojInventure.Text;
                    row1["sifra_robe"] = dg(i, "sifra");
                    row1["jmj"] = dg(i, "jmj");
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["KolicinaNaSk"] = dg(i, "KolicinaNaSk");
                    row1["cijena"] = dg(i, "cijena");
                    row1["naziv"] = dg(i, "naziv");
                    row1["porez"] = dg(i, "porez");
                    row1["mpc"] = dg(i, "mpc");
                    row1["povratna_naknada"] = dg(i, "povratna_naknada");
                    DTsend1.Rows.Add(row1);
                }

                try
                {
                    /*classSQL.update("UPDATE roba_prodaja SET nc='" + dg(i, "cijena").Replace(",", ".") + "'" +
                        " WHERE sifra='" + dg(i, "sifra") + "' AND" +
                        " id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'");*/
                }
                catch { }
            }
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izmjena inventure br." + txtBrojInventure.Text + "')"));
            provjera_sql(SQL.SQLinventura.InsertStavke(DTsend1));
            provjera_sql(SQL.SQLinventura.UpdateStavke(DTsend));
            deleteFields();
            EnableDisable(false);
            edit = false;
            MessageBox.Show("Spremljeno");
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_inventure FROM inventura WHERE broj_inventure='" + txtBrojInventure.Text + "'", "inventura").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojInventure() == txtBrojInventure.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_inventure_edit = txtBrojInventure.Text;
                    fillInventura();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtBrojInventure.ReadOnly = true;
                    nmGodinaInventure.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);

                e.SuppressKeyPress = true;
                cbSkladiste.Select();
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void frmUnosInventura_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false);
        }

        private void btnUcitajSveStavke_Click(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;

            string filter = "";
            if (b1.Name.ToString() == "btnSamoRepromaterijal")
            {
                filter = " AND roba_prodaja.id_podgrupa='1'";
            }
            else if (b1.Name.ToString() == "btnSamoTrgRobu")
            {
                filter = " AND roba_prodaja.id_podgrupa='3'";
            }
            else if (b1.Name.ToString() == "btnUcitajSvuHranu")
            {
                filter = " AND roba_prodaja.id_podgrupa='2'";
            }

            if (dgw.Rows.Count > 0)
            {
                MessageBox.Show("Greška!\r\nVeć postoje stavke u ovoj inventuri.");
                return;
            }

            string sql = "";
            DataTable DT = new DataTable();

            dgw.Rows.Clear();

            if (DT.Rows.Count == 0)
            {
                sql = @"DROP TABLE IF EXISTS koristeno;
                        CREATE TEMP TABLE koristeno AS
                        SELECT * FROM
                        (
                            SELECT sifra as sifra FROM pocetno GROUP BY sifra
	                        UNION ALL
	                        SELECT sifra_robe as sifra FROM inventura_stavke WHERE broj_inventure='1' GROUP BY sifra_robe
	                        UNION ALL
	                        SELECT sifra FROM primka_stavke GROUP BY sifra
	                        UNION ALL
	                        SELECT sifra FROM medu_poslovnice GROUP BY sifra
	                        UNION ALL
	                        SELECT sifra FROM povrat_robe_stavke GROUP BY sifra
	                        UNION ALL
	                        SELECT caffe_normativ.sifra_normativ FROM racun_stavke
	                        JOIN caffe_normativ ON caffe_normativ.sifra=racun_stavke.sifra_robe
	                        GROUP BY caffe_normativ.sifra_normativ
                        ) koristeno
                        GROUP BY sifra;

                    SELECT roba_prodaja.mjera,roba_prodaja.naziv,roba_prodaja.id_skladiste,roba_prodaja.kolicina,roba_prodaja.nc,
                    roba_prodaja.ulazni_porez,roba_prodaja.sifra,roba_prodaja.povratna_naknada,roba.mpc
                    FROM roba_prodaja
                    LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra
                    WHERE roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "' " + filter + @" AND case when (select count(sifra) from koristeno) > 0 then roba_prodaja.sifra IN (SELECT sifra FROM koristeno) else 1=1 end
                ORDER BY roba_prodaja.naziv ASC";
                DT = classSQL.select(sql, "roba_prodaja").Tables[0];
            }

            int i = 1;

            foreach (DataRow r in DT.Rows)
            {
                decimal kolicina, ukupno, nc;
                decimal.TryParse(r["nc"].ToString(), out nc);
                decimal.TryParse(r["kolicina"].ToString(), out kolicina);

                if (DTtvrtka.Rows[0]["oib"].ToString() == "05593216962" || DTtvrtka.Rows[0]["oib"].ToString() == "77566209058")
                {
                    if (!File.Exists("uzmikolicinu"))
                        kolicina = 0;
                }

                ukupno = kolicina * nc;

                dgw.Rows.Add(i, r["sifra"].ToString(),
                    r["naziv"].ToString(),
                    r["mjera"].ToString(),
                    kolicina,
                    Math.Round(nc, 3).ToString("#0.00"),
                    Math.Round(ukupno, 3).ToString("#0.00"),
                    r["kolicina"].ToString(),
                    "",
                    r["ulazni_porez"].ToString(),
                    r["mpc"].ToString(),
                    r["povratna_naknada"].ToString());
                i++;
            }
        }

        private void frmUnosInventura_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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