using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmMeduskladisnica : Form
    {
        public frmMeduskladisnica()
        {
            InitializeComponent();
        }

        private DataTable DTRoba = new DataTable();
        private DataTable DTskl = new DataTable();
        private DataTable DTfill = new DataTable();
        private DataTable DTHeaderfill = new DataTable();
        private bool edit = false;
        public string broj_ms_edit { get; set; }
        public string godina_edit { get; set; }
        public string skladiste_edit { get; set; }

        public frmMenu MainForm { get; set; }
        private string broj_meduskladisnica_edit = "";
        private bool load = false;

        private void frmMeduskladisnica_Load(object sender, EventArgs e)
        {
            numeric();
            txtBroj.Select();
            ControlDisableEnable(1, 0, 0, 1, 0);
            SetCB();
            string BrojPonude = brojPonude();
            txtBroj.Text = BrojPonude;

            if (broj_ms_edit != null && godina_edit != null) { fillMeduskladisnice(broj_ms_edit, godina_edit, skladiste_edit); }
            EnableDisable(false);
            PaintRows(dgw);
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            this.Paint += new PaintEventHandler(Form1_Paint);
            load = true;
        }

        private void numeric()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = 2012;
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

        private void SetCB()
        {
            //fill skl
            DTskl = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1')", "skladiste").Tables[0];
            izSkladista.DataSource = DTskl;
            izSkladista.DisplayMember = "skladiste";
            izSkladista.ValueMember = "id_skladiste";

            DataTable DTskl1 = classSQL.select("SELECT * FROM skladiste WHERE  id_skladiste NOT IN ('1')", "skladiste").Tables[0];
            USkladiste.DataSource = DTskl1;
            USkladiste.DisplayMember = "skladiste";
            USkladiste.ValueMember = "id_skladiste";
        }

        private string brojPonude()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj AS bigint)) FROM meduskladisnica WHERE id_skladiste_od='" + izSkladista.SelectedValue + "'", "meduskladisnica").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
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

                string sql = "SELECT roba_prodaja.sifra," +
                    " roba_prodaja.id_skladiste," +
                    " roba_prodaja.kolicina," +
                    " roba_prodaja.nc," +
                    " roba_prodaja.vpc," +
                    " roba_prodaja.ulazni_porez," +
                    " roba_prodaja.sifra," +
                    " roba_prodaja.naziv," +
                    " roba_prodaja.mjera" +
                    " FROM roba_prodaja  " +
                    " WHERE roba_prodaja.sifra='" + propertis_sifra + "' AND roba_prodaja.id_skladiste='" + izSkladista.SelectedValue + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];

                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    txtBroj.Enabled = false;
                    nmGodina.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga u zadanome skladištu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];
            dgw.BeginEdit(true);

            dgw.Rows[br].Cells[0].Value = "1";
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["mjera"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["ulazni_porez"].ToString();
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["nc"]);

            izracun();
            PaintRows(dgw);
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow.Index;

                if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }

                double kol = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
                //double vpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());
                double porez = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString());

                //double porez_ukupno = vpc * porez / 100;
                double mpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["nc"].FormattedValue.ToString());
                double mpc_sa_kolicinom = mpc * kol;

                dgw.Rows[rowBR].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = String.Format("{0:0.00}", ((mpc) * kol));
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT roba_prodaja.sifra," +
                    " roba_prodaja.id_skladiste," +
                    " roba_prodaja.kolicina," +
                    " roba_prodaja.nc," +
                    " roba_prodaja.ulazni_porez," +
                    " roba_prodaja.sifra," +
                    " roba_prodaja.naziv," +
                    " roba_prodaja.mjera" +
                    " FROM roba_prodaja  " +
                    " WHERE roba_prodaja.sifra='" + txtSifra_robe.Text + "' AND roba_prodaja.id_skladiste='" + izSkladista.SelectedValue + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Rows[dgw.Rows.Count - 1].Cells["kolicina"].Selected = true;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga u zadanome skladištu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    if (isNumeric(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.CurrentRow.Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                }
                catch (Exception) { dgw.BeginEdit(true); }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgw.BeginEdit(true);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (izSkladista.SelectedValue.ToString().Trim() == USkladiste.SelectedValue.ToString().Trim())
            {
                MessageBox.Show("Niste pravilno odabrali skladišta.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgw.Rows.Count == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (edit == true)
            {
                UpdateM();
                edit = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
            else
            {
                Spremi();
                edit = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
            MessageBox.Show("Spremljeno");
        }

        private void UpdateM()
        {
            string broj = txtBroj.Text;

            string sql = "UPDATE meduskladisnica SET" +
                " godina='" + nmGodina.Value.ToString() + "'," +
                " id_skladiste_od='" + izSkladista.SelectedValue + "'," +
                " id_skladiste_do='" + USkladiste.SelectedValue + "'," +
                " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                " org_dokumenat='" + orgDokumenat.Text + "'," +
                " napomena='" + rtbNapomena.Text + "' WHERE broj='" + txtBroj.Text + "' AND id_skladiste_od='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'";

            provjera_sql(classSQL.update(sql));

            string kol;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    //skidaj iz skladišta
                    kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), izSkladista.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                    SQL.SQLroba_prodaja.UpdateRows(izSkladista.SelectedValue.ToString(), kol, dg(i, "sifra"));

                    //dodaj na skladište
                    kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), USkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "+");
                    SQL.SQLroba_prodaja.UpdateRows(USkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));

                    sql = "INSERT INTO meduskladisnica_stavke (sifra,mpc,pdv,kolicina,broj,godina,nbc,iz_skladista) VALUES " +
                        "(" +
                        " '" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " '" + Convert.ToDouble(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString()) + "'," +
                        " '" + dgw.Rows[i].Cells["porez"].FormattedValue.ToString() + "'," +
                        " '" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " '" + broj + "'," +
                        " '" + nmGodina.Value.ToString() + "'," +
                        " '" + dgw.Rows[i].Cells["nc"].FormattedValue.ToString() + "'," +
                        " '" + izSkladista.SelectedValue.ToString() + "'" +
                        ")" +
                        "";

                    provjera_sql(classSQL.insert(sql));
                }
                else
                {
                    DataRow[] dataROW = DTfill.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (izSkladista.SelectedValue.ToString() != DTHeaderfill.Rows[0]["id_skladiste_od"].ToString())
                    {
                        //skidaj iz skladišta
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTHeaderfill.Rows[0]["id_skladiste_od"].ToString(), dataROW[0]["kolicina"].ToString(), "+");
                        SQL.SQLroba_prodaja.UpdateRows(DTHeaderfill.Rows[0]["id_skladiste_od"].ToString(), kol, dg(i, "sifra"));

                        //skidaj iz skladišta
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), izSkladista.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                        SQL.SQLroba_prodaja.UpdateRows(izSkladista.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                    else
                    {
                        double kkol = Convert.ToDouble(dg(i, "kolicina")) - Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        //skidaj iz skladišta
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTHeaderfill.Rows[0]["id_skladiste_od"].ToString(), kkol.ToString(), "-");
                        SQL.SQLroba_prodaja.UpdateRows(DTHeaderfill.Rows[0]["id_skladiste_od"].ToString(), kol, dg(i, "sifra"));
                    }

                    if (USkladiste.SelectedValue.ToString() != DTHeaderfill.Rows[0]["id_skladiste_do"].ToString())
                    {
                        //dodaj na skladište
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTHeaderfill.Rows[0]["id_skladiste_do"].ToString(), dataROW[0]["kolicina"].ToString(), "-");
                        SQL.SQLroba_prodaja.UpdateRows(DTHeaderfill.Rows[0]["id_skladiste_do"].ToString(), kol, dg(i, "sifra"));

                        //dodaj na skladište
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), USkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "+");
                        SQL.SQLroba_prodaja.UpdateRows(USkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                    else
                    {
                        double kkol = Convert.ToDouble(dg(i, "kolicina")) - Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        //dodaj na skladište
                        kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), DTHeaderfill.Rows[0]["id_skladiste_do"].ToString(), kkol.ToString(), "+");
                        SQL.SQLroba_prodaja.UpdateRows(DTHeaderfill.Rows[0]["id_skladiste_do"].ToString(), kol, dg(i, "sifra"));
                    }

                    sql = "UPDATE meduskladisnica_stavke SET " +
                        " mpc='" + Convert.ToDouble(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString()) + "'," +
                        " pdv='" + dgw.Rows[i].Cells["porez"].FormattedValue.ToString() + "'," +
                        " kolicina='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " nbc='" + dgw.Rows[i].Cells["nc"].FormattedValue.ToString() + "'," +
                        " iz_skladista='" + izSkladista.SelectedValue.ToString() + "'," +
                        " broj='" + broj + "' WHERE id_stavka='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";
                    //MessageBox.Show(sql);
                    provjera_sql(classSQL.update(sql));
                }

                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void Spremi()
        {
            string broj = brojPonude();

            string sql = "INSERT INTO meduskladisnica" +
                " (broj,godina,id_skladiste_od,id_skladiste_do,datum,id_izradio,org_dokumenat,napomena) VALUES " +
                " (" +
                " '" + broj + "'," +
                " '" + nmGodina.Value.ToString() + "'," +
                " '" + izSkladista.SelectedValue + "'," +
                " '" + USkladiste.SelectedValue + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + orgDokumenat.Text + "'," +
                " '" + rtbNapomena.Text + "'" +
                " )";
            provjera_sql(classSQL.insert(sql));

            string kol;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                //skidaj iz skladišta
                kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), izSkladista.SelectedValue.ToString(), dg(i, "kolicina"), "-");
                SQL.SQLroba_prodaja.UpdateRows(izSkladista.SelectedValue.ToString(), kol, dg(i, "sifra"));

                //dodaj na skladište
                kol = SQL.ClassSkladiste.GetAmountCaffe(dg(i, "sifra"), USkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "+");
                SQL.SQLroba_prodaja.UpdateRows(USkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));

                sql = "INSERT INTO meduskladisnica_stavke (sifra,mpc,pdv,kolicina,broj,godina,nbc,iz_skladista) VALUES " +
                    "(" +
                    " '" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                    " '" + Convert.ToDouble(dgw.Rows[i].Cells["mpc"].FormattedValue.ToString()) + "'," +
                    " '" + dgw.Rows[i].Cells["porez"].FormattedValue.ToString() + "'," +
                    " '" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                    " '" + broj + "'," +
                    " '" + nmGodina.Value.ToString() + "'," +
                    " '" + dgw.Rows[i].Cells["nc"].FormattedValue.ToString() + "'," +
                    " '" + izSkladista.SelectedValue.ToString() + "'" +
                    ")" +
                    "";

                provjera_sql(classSQL.insert(sql));
                edit = true;
            }
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            double kol = 0;
            double kolicina = 0;

            if (dgw.RowCount == 0)
            {
                return;
            }

            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                if (MessageBox.Show("Brisanjem ove međuskladišnice brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovaj dokumenat?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int i = dgw.CurrentRow.Index;
                    //iz skladišta
                    DataRow[] dataROW = DTfill.Select("id_stavka = " + dgw.CurrentRow.Cells["id_stavka"].Value.ToString());
                    kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                    kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                    kolicina = kolicina + kol;
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'");

                    //u skladište
                    kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                    kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                    kolicina = kolicina - kol;
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'");

                    classSQL.delete("DELETE FROM meduskladisnica_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + 2 + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa međuskladišnice br." + txtBroj.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove međuskladišnice brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovaj dokumenat?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double kol = 0;
                double kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        //iz skladišta
                        DataRow[] dataROW = DTfill.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                        kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                        kolicina = kolicina + kol;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'");

                        //u skladište
                        kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                        kolicina = kolicina - kol;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'");
                    }
                }

                classSQL.delete("DELETE FROM meduskladisnica_stavke WHERE broj='" + txtBroj.Text + "'");
                classSQL.delete("DELETE FROM meduskladisnica WHERE broj='" + txtBroj.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele meduskladisnice br." + txtBroj.Text + "')");
                MessageBox.Show("Obrisano.");
                ControlDisableEnable(1, 0, 0, 1, 0);

                edit = false;
                EnableDisable(false);
                deleteFields();
            }
        }

        private void EnableDisable(bool x)
        {
            if (x == true)
            {
                txtBroj.Enabled = false;
                nmGodina.Enabled = false;
                izSkladista.Enabled = false;
            }
            else
            {
                txtBroj.Enabled = true;
                nmGodina.Enabled = true;
                izSkladista.Enabled = true;
            }

            dtpDatum.Enabled = x;
            USkladiste.Enabled = x;
            txtSifra_robe.Enabled = x;
            orgDokumenat.Enabled = x;
            rtbNapomena.Enabled = x;
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

        private void deleteFields()
        {
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            numeric();
            dtpDatum.Text = "";
            txtSifra_robe.Text = "";
            orgDokumenat.Text = "";
            rtbNapomena.Text = "";
            dgw.Rows.Clear();
        }

        private DataTable DT = new DataTable();

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DT = classSQL.select("SELECT * FROM meduskladisnica WHERE godina='" + nmGodina.Value.ToString() + "' AND broj='" + txtBroj.Text + "'", "meduskladisnica").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPonude() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        deleteFields();
                        izSkladista.Select();
                        ControlDisableEnable(0, 1, 1, 0, 0);
                        txtBroj.Text = brojPonude();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_meduskladisnica_edit = txtBroj.Text;
                    fillMeduskladisnice(txtBroj.Text, nmGodina.Value.ToString(), izSkladista.SelectedValue.ToString());
                    EnableDisable(true);
                    edit = true;
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }

                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void nmGodina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DT = classSQL.select("SELECT * FROM meduskladisnica WHERE godina='" + nmGodina.Value.ToString() + "' AND broj='" + txtBroj.Text + "'", "meduskladisnica").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPonude() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        deleteFields();
                        izSkladista.Select();
                        ControlDisableEnable(0, 1, 1, 0, 0);
                        txtBroj.Text = brojPonude();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_meduskladisnica_edit = txtBroj.Text;
                    fillMeduskladisnice(txtBroj.Text, nmGodina.Value.ToString(), izSkladista.SelectedValue.ToString());
                    EnableDisable(true);
                    edit = true;
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }

                e.SuppressKeyPress = true;
                izSkladista.Select();
            }
        }

        private void izSkladista_KeyDown(object sender, KeyEventArgs e)
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
                USkladiste.Select();
            }
        }

        private void USkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                orgDokumenat.Select();
            }
        }

        private void orgDokumenat_KeyDown(object sender, KeyEventArgs e)
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

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            ControlDisableEnable(0, 1, 1, 0, 1);
            EnableDisable(true);
            deleteFields();
            txtBroj.Text = brojPonude();
            edit = false;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            ControlDisableEnable(1, 0, 0, 1, 0);
            EnableDisable(false);
            deleteFields();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveMeduskladisnice sm = new frmSveMeduskladisnice();
            //sm.sifra_fakture = "";
            sm.MainForm = this;
            sm.ShowDialog();
            if (broj_ms_edit != null)
            {
                deleteFields();
                fillMeduskladisnice(broj_ms_edit, DateTime.Now.Year.ToString(), skladiste_edit);
                EnableDisable(true);
                edit = true;
            }
        }

        private void fillMeduskladisnice(string broj, string godina, string skladiste)
        {
            DTHeaderfill = classSQL.select("SELECT * FROM meduskladisnica WHERE broj='" + broj + "' AND godina ='" + godina + "' AND id_skladiste_od='" + skladiste + "'", "meduskladisnica").Tables[0];

            txtBroj.Text = DTHeaderfill.Rows[0]["broj"].ToString();
            nmGodina.Value = Convert.ToInt16(DTHeaderfill.Rows[0]["godina"].ToString());
            izSkladista.SelectedValue = DTHeaderfill.Rows[0]["id_skladiste_od"].ToString();
            dtpDatum.Text = DTHeaderfill.Rows[0]["datum"].ToString();
            USkladiste.SelectedValue = DTHeaderfill.Rows[0]["id_skladiste_do"].ToString();
            orgDokumenat.Text = DTHeaderfill.Rows[0]["org_dokumenat"].ToString();
            rtbNapomena.Text = DTHeaderfill.Rows[0]["napomena"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DTfill = classSQL.select("SELECT meduskladisnica_stavke.sifra," +
                " meduskladisnica_stavke.id_stavka," +
                " meduskladisnica_stavke.mpc," +
                " meduskladisnica_stavke.pdv," +
                " meduskladisnica_stavke.nbc," +
                " meduskladisnica_stavke.vpc," +
                " meduskladisnica_stavke.broj," +
                " meduskladisnica_stavke.godina," +
                " meduskladisnica_stavke.kolicina," +
                " roba_prodaja.naziv," +
                " roba_prodaja.mjera" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=meduskladisnica_stavke.sifra" +
                " WHERE meduskladisnica_stavke.broj='" + broj + "' AND meduskladisnica_stavke.godina='" + godina + "' AND meduskladisnica_stavke.iz_skladista='" + skladiste + "'", "meduskladisnica_stavke").Tables[0];

            if (dgw.Rows.Count > 0) { dgw.Rows.Clear(); }

            for (int i = 0; i < DTfill.Rows.Count; i++)
            {
                dgw.Rows.Add(
                    (i + 1),
                    DTfill.Rows[i]["sifra"].ToString(),
                    DTfill.Rows[i]["naziv"].ToString(),
                    DTfill.Rows[i]["mjera"].ToString(),
                    String.Format("{0:0.00}", DTfill.Rows[i]["kolicina"].ToString()),
                    DTfill.Rows[i]["pdv"].ToString(),
                    String.Format("{0:0.00}", Convert.ToDouble(DTfill.Rows[i]["mpc"].ToString())),
                    String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(DTfill.Rows[i]["mpc"].ToString()) * Convert.ToDouble(DTfill.Rows[i]["kolicina"].ToString()))),
                    DTfill.Rows[i]["vpc"].ToString(),
                    DTfill.Rows[i]["nbc"].ToString(),
                    DTfill.Rows[i]["id_stavka"].ToString()
                );
            }
            edit = true;
            PaintRows(dgw);
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void frmMeduskladisnica_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void izSkladista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                txtBroj.Text = brojPonude();
            }
        }
    }
}