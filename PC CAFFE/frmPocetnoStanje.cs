using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPocetnoStanje : Form
    {
        public string broj_pocetno_edit { get; set; }

        private DataTable DTRoba = new DataTable();
        private DataTable DTIzradio = new DataTable();
        private DataTable DT_Skladiste = new DataTable();
        private bool edit = false;

        public frmPocetnoStanje()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPocetnoStanje_Load(object sender, EventArgs e)
        {
            EnableDisable(false);
            SetCB();
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtBrojInventure.Text = brojPocetno_stanje();
            txtBrojInventure.ReadOnly = false;
            nmGodinaInventure.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaInventure.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaInventure.Value = 2012;

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

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
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["mjera"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["nbc"].Value = DTRoba.Rows[0]["nc"].ToString();
            dgw.Rows[br].Cells["vpc"].Value = DTRoba.Rows[0]["vpc"].ToString();
            dgw.Rows[br].Cells["pdv"].Value = DTRoba.Rows[0]["ulazni_porez"].ToString();
            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", ((Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DTRoba.Rows[0]["ulazni_porez"].ToString())) / 100) + Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()));

            dgw.BeginEdit(true);
            PaintRows(dgw);
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select("SELECT ime+' '+prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
            txtIzradio.Text = DTIzradio.Rows[0]["Ime"].ToString();
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN ('1')", "skladiste").Tables[0];
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

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MouseButtons != 0)
                return;

            if (dgw.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[5];
                    dgw.BeginEdit(true);
                }
                catch (Exception) { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[9];
                    dgw.BeginEdit(true);
                }
                catch (Exception) { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 9)
            {
                try
                {
                    // ovo je izračun ako odredite MPC automatski računa popust
                    double MPC = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString());
                    dgw.CurrentRow.Cells["mpc"].Value = MPC.ToString("#0.00");
                    dgw.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(MPC / Convert.ToDouble("1," + dgw.CurrentRow.Cells["pdv"].FormattedValue.ToString())).ToString("#0.00");

                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                catch (Exception)
                {
                }
            }

            //izracun();
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
            txtBrojInventure.Text = brojPocetno_stanje();
            EnableDisable(false);
            DelField();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPocetno_stanje();

            if (edit == false)
            {
                Spremi();
                EnableDisable(false);
                edit = false;
            }
            else
            {
                Update();
                EnableDisable(false);
                edit = false;
            }

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void Spremi()
        {
            string sql = "INSERT INTO pocetno_stanje (broj,id_skladiste,date,id_izradio,napomena) VALUES" +
                "(" +
                "'" + txtBrojInventure.Text + "'," +
                "'" + cbSkladiste.SelectedValue + "'," +
                "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + rtbNapomena.Text + "'" +
                ")";

            provjera_sql(classSQL.insert(sql));

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string mpc = dgw.Rows[i].Cells["mpc"].FormattedValue.ToString();
                string nbc = dgw.Rows[i].Cells["nbc"].FormattedValue.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    mpc = mpc.Replace(",", ".");
                    nbc = nbc.Replace(",", ".");
                }
                else
                {
                    mpc = mpc.Replace(".", ",");
                    nbc = nbc.Replace(".", ",");
                }

                string sql1 = "INSERT INTO pocetno_stanje_stavke (sifra,ean,kolicina,pdv,mpc,nbc,broj) VALUES" +
                    "(" +
                    "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                    "'" + dgw.Rows[i].Cells["ean"].FormattedValue.ToString() + "'," +
                    "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                    "'" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                    "'" + mpc + "'," +
                    "'" + nbc + "'," +
                    "'" + txtBrojInventure.Text + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql1));

                GetAmount(dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(), cbSkladiste.SelectedValue.ToString(), dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), dgw.Rows[i].Cells["vpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["mpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["ean"].FormattedValue.ToString(), dgw.Rows[i].Cells["nbc"].FormattedValue.ToString());
            }
        }

        private void Update()
        {
            string sql = "UPDATE pocetno_stanje SET " +
                " broj='" + txtBrojInventure.Text + "'," +
                " id_skladiste='" + cbSkladiste.SelectedValue + "'," +
                " date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                " napomena='" + rtbNapomena.Text + "' WHERE broj='" + txtBrojInventure.Text + "'";
            provjera_sql(classSQL.update(sql));

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string mpc = dgw.Rows[i].Cells["mpc"].FormattedValue.ToString();
                string nbc = dgw.Rows[i].Cells["nbc"].FormattedValue.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    mpc = mpc.Replace(",", ".");
                    nbc = nbc.Replace(",", ".");
                }
                else
                {
                    mpc = mpc.Replace(".", ",");
                    nbc = nbc.Replace(".", ",");
                }

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    string sql1 = "INSERT INTO pocetno_stanje_stavke (sifra,ean,kolicina,pdv,mpc,nbc,broj) VALUES" +
                        "(" +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["ean"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "'" + mpc + "'," +
                        "'" + nbc + "'," +
                        "'" + txtBrojInventure.Text + "'" +
                        ")";
                    provjera_sql(classSQL.insert(sql1));
                    GetAmount(dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(), cbSkladiste.SelectedValue.ToString(), dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), dgw.Rows[i].Cells["vpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["mpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["ean"].FormattedValue.ToString(), dgw.Rows[i].Cells["nbc"].FormattedValue.ToString());
                }
                else
                {
                    string sql2 = "UPDATE pocetno_stanje_stavke SET " +
                        " sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " ean='" + dgw.Rows[i].Cells["ean"].FormattedValue.ToString() + "'," +
                        " kolicina='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " pdv='" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        " mpc='" + mpc + "'," +
                        " nbc='" + nbc + "'," +
                        " WHERE broj='" + txtBrojInventure.Text + "'";
                    provjera_sql(classSQL.update(sql2));
                    GetAmount(dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(), cbSkladiste.SelectedValue.ToString(), dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), dgw.Rows[i].Cells["vpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["mpc"].FormattedValue.ToString(), dgw.Rows[i].Cells["ean"].FormattedValue.ToString(), dgw.Rows[i].Cells["nbc"].FormattedValue.ToString());
                }
            }
        }

        public void GetAmount(string sifra, string skladiste, string kolicina, string vpc, string mpc, string ean, string nc)
        {
            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(",", ".");
                nc = nc.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(".", ",");
                mpc = mpc.Replace(".", ",");
                nc = nc.Replace(".", ",");
            }

            DataSet DSkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                provjera_sql(classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,sifra) VALUES ('" + skladiste + "','" + kolicina + "','" + nc + "','" + vpc + "','" + sifra + "')"));

                string sql2 = "UPDATE roba SET " +
                     "vpc='" + vpc + "'," +
                     "mpc='" + mpc + "'," +
                     "nc='" + nc + "'," +
                     "ean='" + ean + "' WHERE sifra='" + sifra + "'" +
                     "";
                provjera_sql(classSQL.update(sql2));
            }
            else
            {
                string sql = "UPDATE roba_prodaja SET kolicina='" + kolicina + "'," +
                     "nc='" + nc + "'," +
                     "vpc='" + vpc + "'," +
                     "porez='" + pdv + "' WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "'";
                provjera_sql(classSQL.update(sql));

                string sql1 = "UPDATE roba SET " +
                     "vpc='" + vpc + "'," +
                     "mpc='" + mpc + "'," +
                     "nc='" + nc + "'," +
                     "ean='" + ean + "' WHERE sifra='" + sifra + "'" +
                     "";
                provjera_sql(classSQL.update(sql1));
            }
        }

        private string brojPocetno_stanje()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM pocetno_stanje", "pocetno_stanje").Tables[0];
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
            //edit = true;
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPocetno_stanje();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
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
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM pocetno_stanje WHERE  broj='" + txtBrojInventure.Text + "'", "pocetno_stanje").Tables[0];
                DelField();
                if (DT.Rows.Count == 0)
                {
                    if (brojPocetno_stanje() == txtBrojInventure.Text.Trim())
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
                    broj_pocetno_edit = txtBrojInventure.Text;
                    FillPocetnoStanje(broj_pocetno_edit);
                    EnableDisable(true);
                    edit = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPocetnoStanje(string broj)
        {
            string sql = "SELECT pocetno_stanje_stavke.sifra," +
                " pocetno_stanje_stavke.ean," +
                " pocetno_stanje_stavke.id_stavka," +
                " pocetno_stanje_stavke.kolicina," +
                " pocetno_stanje_stavke.pdv," +
                " pocetno_stanje_stavke.mpc," +
                " pocetno_stanje_stavke.nbc," +
                " roba.naziv," +
                " roba.jm " +
                " FROM pocetno_stanje_stavke" +
                " LEFT JOIN roba ON roba.sifra=pocetno_stanje_stavke.sifra WHERE pocetno_stanje_stavke.broj='" + broj + "'" +
                "";
            DataTable DT = classSQL.select(sql, "pocetno_stanje_stavke").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["EAN"].Value = DT.Rows[0]["ean"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = DT.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT.Rows[0]["kolicina"].ToString();
                dgw.Rows[br].Cells["nbc"].Value = DT.Rows[0]["nbc"].ToString();
                dgw.Rows[br].Cells["mpc"].Value = Convert.ToDouble(DT.Rows[0]["mpc"].ToString()).ToString("#0.000");
                dgw.Rows[br].Cells["vpc"].Value = Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["mpc"].ToString()) / Convert.ToDouble("1," + DT.Rows[0]["pdv"].ToString())).ToString("#0.000");
                dgw.Rows[br].Cells["pdv"].Value = DT.Rows[0]["pdv"].ToString();
            }
            edit = true;
            PaintRows(dgw);
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
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