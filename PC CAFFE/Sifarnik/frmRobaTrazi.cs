using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobaTrazi : Form
    {
        private DataSet DSgrupa = new DataSet();
        private DataSet DSdobavljac = new DataSet();
        private DataSet DSmanufacturers = new DataSet();
        private bool _roba = false;

        public bool roba
        {
            get { return _roba; }
            set { _roba = value; }
        }

        public int id_skladiste { get; internal set; }

        public frmRobaTrazi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private bool remoteDBboll = false;

        private void frmRobaTrazi_Load(object sender, EventArgs e)


        {
            PaintRows(dataGridView2);
            PaintRows(dataGridView1);
            txtIme.Select();
            Properties.Settings.Default.id_roba = "";
            Properties.Settings.Default.Save();
            CBset();

            if (classSQL.remoteConnectionString != "")
            {
                remoteDBboll = true;
            }

            pretraziArtikle();

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void pretraziArtikle(bool traziPoNazivu = true)
        {
            string rns = "", limit = "", limitRemote = "", where = "", skladiste = "";
            if (id_skladiste != 0)
            {
                skladiste = string.Format(" and roba_prodaja.id_skladiste = '{0}'", id_skladiste);
            }

            if (chbRNS.Checked)
            {
                rns = " AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
                if (roba)
                    rns = " AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            }
            else
            {
                rns = "";
            }

            if (traziPoNazivu)
            {
                if (remoteDBboll != true)
                {
                    //limit = " TOP(40) ";
                    where = "lower(roba_prodaja.naziv) LIKE lower('%" + txtIme.Text + "%')  AND roba_prodaja.aktivnost='1'";
                    if (roba)
                        where = "lower(roba.naziv) LIKE lower('%" + txtIme.Text + "%')  AND roba.aktivnost='1'";
                }
                else
                {
                    //limitRemote = " LIMIT 40 ";
                    where = "lower(roba_prodaja.naziv) ~* lower('" + txtIme.Text + "')  AND roba_prodaja.aktivnost='1'";
                    if (roba)
                        where = "lower(roba.naziv) LIKE lower('%" + txtIme.Text + "%')  AND roba.aktivnost='1'";
                }
            }
            else
            {
                if (remoteDBboll != true)
                {
                    //limit = " TOP(40) ";
                    where = "roba_prodaja.sifra LIKE '%" + txtTraziPremaSifri.Text + "%' AND roba_prodaja.aktivnost='1'";
                    if (roba)
                        where = "roba.sifra LIKE '%" + txtTraziPremaSifri.Text + "%'  AND roba.aktivnost='1'";
                }
                else
                {
                    //limitRemote = " LIMIT 40 ";
                    where = "roba_prodaja.sifra ~* '" + txtTraziPremaSifri.Text + "' AND roba_prodaja.aktivnost='1'";
                    if (roba)
                        where = "roba.sifra LIKE '%" + txtTraziPremaSifri.Text + "%'  AND roba.aktivnost='1'";
                }
            }

            string sql = @"SELECT " + limit + @" roba_prodaja.sifra as [Šifra], roba_prodaja.naziv AS [Naziv], grupa.grupa as [Grupa],
                        roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as Nabavna, '0' as Veleprodajna, roba_prodaja.mpc as Maloprodajna
                        FROM roba_prodaja
                        LEFT JOIN grupa ON roba_prodaja.id_grupa = grupa.id_grupa
                        LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste
                        WHERE " + where + rns + skladiste + @" AND roba_prodaja.sifra !~* '!serial'
                        ORDER BY CAST(id_roba_prodaja as numeric) " + limitRemote;
            if (roba)
            {
                sql = @"SELECT " + limit + @" roba.sifra as [Šifra], roba.naziv AS [Naziv], roba.mpc as [Maloprodajna]
                        FROM roba
                        WHERE " + where + rns + @"
                        ORDER BY CAST(id_roba as numeric) " + limitRemote;
            }

            dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];

            PaintRows(dataGridView2);
            dataGridView2.Columns[1].Width = 300;

            if (roba)
            {
                //dataGridView2.Columns["Količina"].Visible = false;
            }
            else
            {
                SetDecimalInDgv(dataGridView2, "Nabavna", "", "");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pretraziArtikle();
        }

        private void SetSkladisteKojeNePostoji()
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells["Skladište"].FormattedValue.ToString() == "")
                {
                    dataGridView2.Rows[i].Cells["Skladište"].Value = "------------";
                    dataGridView2.Rows[i].Cells["Količina"].Value = "0";
                }
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["Skladište"].FormattedValue.ToString() == "")
                {
                    dataGridView1.Rows[i].Cells["Skladište"].Value = "------------";
                    dataGridView1.Rows[i].Cells["Količina"].Value = "0";
                }
            }
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        if (column != "")
                            dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        if (column1 != "")
                            dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        if (column2 != "")
                            dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void CBset()
        {
            //CB grupe

            DSgrupa = classSQL.select("SELECT * FROM grupa ORDER BY grupa", "grupa");
            cbGrupa.DataSource = DSgrupa.Tables[0];
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //CB dobavljač
            DSdobavljac = classSQL.select("SELECT * FROM partners ORDER BY ime_tvrtke", "partners");
            cbPartner.DataSource = DSdobavljac.Tables[0];
            cbPartner.DisplayMember = "ime_tvrtke";
            cbPartner.ValueMember = "id_partner";
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string query = "", query1 = "", query_grupa = "", query_dobavljac = "", query_proizvodac = "", limit = "", limitRemote = "";

            if (sifra.Checked)
            {
                query = " roba_prodaja.sifra LIKE '%" + txtSifra.Text + "%' ";
                if (roba)
                    query = " roba.sifra LIKE '%" + txtSifra.Text + "%' ";
            }
            else if (ime.Checked)
            {
                query = " roba_prodaja.naziv LIKE '%" + txtSifra.Text + "%'";
                if (roba)
                    query = " roba.naziv LIKE '%" + txtSifra.Text + "%'";
            }

            if (chGrupa.Checked)
            {
                query_grupa = " AND roba_prodaja.id_grupa=" + cbGrupa.SelectedValue;
            }

            if (chDobavljac.Checked)
            {
                query_dobavljac = " AND roba_prodaja.id_partner=" + cbPartner.SelectedValue;
            }

            if (remoteDBboll == true)
            {
                //limitRemote = " LIMIT 200 ";
            }
            else
            {
                //limit = " TOP(200) ";
            }

            string sql = @"SELECT " + limit + @"roba_prodaja.sifra as [Šifra], roba_prodaja.naziv AS [Naziv], partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa],
                        roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as Nabavna, roba_prodaja.mpc as Maloprodajna
                        FROM roba_prodaja
                        LEFT JOIN grupa ON roba_prodaja.id_grupa = grupa.id_grupa
                        LEFT JOIN partners ON roba_prodaja.id_partner = partners.id_partner
                        LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste
                        WHERE" + query + query1 + query_grupa + query_proizvodac + query_dobavljac + limitRemote;

            if (roba)
            {
                sql = @"SELECT " + limit + @" roba.sifra as [Šifra], roba.naziv AS [Naziv], grupa.grupa as [Grupa],
                        roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba.nbc as Nabavna, '0' as Veleprodajna, roba.mpc as Maloprodajna
                        FROM roba
                        LEFT JOIN roba_prodaja ON roba_prodaja.sifra = roba.sifra
                        LEFT JOIN grupa ON roba_prodaja.id_grupa = grupa.id_grupa
                        LEFT JOIN partners ON roba_prodaja.id_partner = partners.id_partner
                        LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste
                        WHERE" + query + query1 + query_grupa + query_proizvodac + query_dobavljac + limitRemote;
            }

            dataGridView1.DataSource = classSQL.select(sql, "roba").Tables[0];

            PaintRows(dataGridView1);
            if (roba)
            {
                dataGridView1.Columns["Količina"].Visible = false;
            }

            SetDecimalInDgv(dataGridView1, "Nabavna", "", "Maloprodajna");
            SetSkladisteKojeNePostoji();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int br = e.RowIndex;
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    Properties.Settings.Default.id_roba = id;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                string id = dataGridView1.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.id_roba = id;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnTrazi.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRobaUsluge ru = new frmRobaUsluge();
            ru.Show();
        }

        private void txtTraziPremaSifri_TextChanged_1(object sender, EventArgs e)
        {
            pretraziArtikle(false);

            //string rns = "", limit = "", limitRemote = "", where = "";

            //if (chbRNS.Checked) {
            //    rns = "AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            //    if (roba)
            //        rns = "AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            //} else {
            //    rns = "";
            //}

            //if (remoteDBboll != true) {
            //    //limit = " TOP(40) ";
            //    where = "roba_prodaja.sifra LIKE '%" + txtTraziPremaSifri.Text + "%' AND roba_prodaja.aktivnost='1'";
            //    if (roba)
            //        where = "roba.sifra LIKE '%" + txtTraziPremaSifri.Text + "%'  AND roba.aktivnost='1'";
            //} else {
            //    //limitRemote = " LIMIT 40 ";
            //    where = "roba_prodaja.sifra ~* '" + txtTraziPremaSifri.Text + "' AND roba_prodaja.aktivnost='1'";
            //    if (roba)
            //        where = "roba.sifra LIKE '%" + txtTraziPremaSifri.Text + "%'  AND roba.aktivnost='1'";
            //}

            //string sql = @"SELECT " + limit + @" roba_prodaja.sifra as [Šifra],roba_prodaja.naziv AS [Naziv],partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa],
            //roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište] ,roba_prodaja.nc as Nabavna, roba_prodaja.mpc as Maloprodajna
            //FROM roba_prodaja
            //LEFT JOIN grupa ON roba_prodaja.id_grupa = grupa.id_grupa
            //LEFT JOIN partners ON roba_prodaja.id_partner = partners.id_partner
            //LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste
            //WHERE " + where + rns + limitRemote;

            //sql = @"SELECT " + limit + @" roba_prodaja.sifra as [Šifra], roba_prodaja.naziv AS [Naziv], grupa.grupa as [Grupa],
            //            roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as Nabavna, '0' as Veleprodajna, roba_prodaja.mpc as Maloprodajna
            //            FROM roba_prodaja
            //            LEFT JOIN grupa ON roba_prodaja.id_grupa = grupa.id_grupa
            //            LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste
            //            WHERE " + where + rns + @" AND roba_prodaja.sifra !~* '!serial'
            //            ORDER BY CAST(id_roba_prodaja as numeric) " + limitRemote;

            //if (roba) {
            //    sql = @"SELECT " + limit + @" roba.sifra as [Šifra], roba.naziv AS [Naziv], roba.mpc as [Maloprodajna]
            //            FROM roba
            //            WHERE " + where + rns + @"
            //            ORDER BY CAST(id_roba as numeric) " + limitRemote;

            //}

            //dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];
            //PaintRows(dataGridView2);

            //dataGridView2.Columns[1].Width = 300;
            //if (!roba) {
            //    SetDecimalInDgv(dataGridView2, "Nabavna", "Veleprodajna", "Maloprodajna");
            //    SetSkladisteKojeNePostoji();
            //}
        }

        private void txtIme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView2.RowCount > 0)
                {
                    int br = dataGridView2.CurrentRow.Index;
                    ;
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    Properties.Settings.Default.id_roba = id;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
            }

            if (e.KeyData == Keys.Up)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent > 0)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent - 1].Cells[0];
            }

            if (e.KeyData == Keys.Down)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent < dataGridView2.RowCount - 1)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent + 1].Cells[0];
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