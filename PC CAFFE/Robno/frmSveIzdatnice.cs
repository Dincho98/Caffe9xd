using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveIzdatnice : Form
    {
        public frmIzdatnica MainForm { get; set; }
        private bool slanjeDokumentacije;
        private DateTime pocetniDatum;
        private DateTime zavrsniDatum;

        public frmSveIzdatnice(bool slanjeDokumentacije=false,DateTime? pocetniDatum=null,DateTime? zavrsniDatum=null)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;

            if (slanjeDokumentacije)
            {
                this.slanjeDokumentacije = slanjeDokumentacije;
                this.pocetniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(pocetniDatum), 0, 0, 1);
                this.zavrsniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(zavrsniDatum), 23, 59, 59);
            }
        }

        private DataSet DSizdatnice = new DataSet();
        private DataSet DSpartners = new DataSet();
        private DataSet DSzaposlenik = new DataSet();

        public frmMenu MainFormMenu { get; set; }

        private void frmSveIzdatnice_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            if (MainFormMenu == null)
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 60;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }
            else
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 140;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }

            fillCB();
            fillDataGrid();
            try
            {
                if (dgv.Rows.Count > 1)
                {
                    string br = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }

        private void fillDataGrid_stavke(string broj)
        {
            try
            {
                if (broj == null || broj == "")
                {
                    return;
                }

                dataGridView1.Visible = true;
                string sql = string.Format(@"SELECT izdatnica_stavke.broj AS [Broj Izdatnice],izdatnica_stavke.sifra as [Sifra], izdatnica_stavke.nbc as [NBC], izdatnica_stavke.kolicina as [Količina], round((round(replace(izdatnica_stavke.kolicina, ',' ,'.')::numeric, 3) * round(izdatnica_stavke.nbc, 2)), 2)  as [Ukupno]
FROM izdatnica_stavke
LEFT JOIN izdatnica ON izdatnica.id_izdatnica = izdatnica_stavke.id_izdatnica
WHERE izdatnica_stavke.id_izdatnica = '{0}'
ORDER BY izdatnica_stavke.sifra DESC", broj);

                DSizdatnice = classSQL.select(sql, "izdatnica");
                dataGridView1.DataSource = DSizdatnice.Tables[0];

                //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //DataGridViewCellStyle style = new DataGridViewCellStyle();
                //style.Format = "N2";
                //dgv.Columns["MPC"].DefaultCellStyle = style;
                //dgv.Columns["VPC"].DefaultCellStyle = style;
                //dgv.Columns["Rabat"].DefaultCellStyle = style;
                //dgv.Columns["Količina"].DefaultCellStyle = style;
            }
            catch { }
        }

        #region buttons

        private void button1_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                int broj_izdatnice;
                int broj_skladista;

                try
                {
                    broj_izdatnice = Convert.ToInt16(dgv.CurrentRow.Cells["Broj izdatnice"].Value.ToString());
                    broj_skladista = Convert.ToInt16(dgv.CurrentRow.Cells["Broj skladista"].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    frmIzdatnica childForm = new frmIzdatnica();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_izdatnice = broj_izdatnice;
                    childForm.broj_skladista = broj_skladista;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_izdatnice = broj_izdatnice;
                    MainForm.broj_skladista = broj_skladista;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string Izradio = "";

            if (chbBroj.Checked)
            {
                Broj = "izdatnica.broj='" + txtBroj.Text + "' AND ";
            }

            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "izdatnica.id_partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "izdatnica.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "izdatnica.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = "";
                        }
                    }
                }
            }
            if (chbOD.Checked)
            {
                DateStart = "izdatnica.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "izdatnica.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "izdatnica.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + DateStart + DateEnd + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = " ";
            string remote = ";";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500;";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = string.Format(@"SELECT DISTINCT{0}izdatnica.broj AS [Broj izdatnice], izdatnica.id_skladiste AS [Broj skladista], izdatnica.datum AS [Datum], partners.ime_tvrtke AS [Partner], sum(round((round(replace(izdatnica_stavke.kolicina, ',', '.')::numeric, 3) * round(izdatnica_stavke.nbc, 2)), 2)) as [Ukupno], izdatnica.id_izdatnica as [ID]
FROM izdatnica
LEFT JOIN partners ON izdatnica.id_partner = partners.id_partner
LEFT JOIN izdatnica_stavke ON izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica
{1}
GROUP BY izdatnica.broj, izdatnica.id_skladiste, izdatnica.datum, partners.ime_tvrtke, izdatnica.id_izdatnica
ORDER BY izdatnica.datum DESC{2}",
                top, filter, remote);

            DSizdatnice = classSQL.select(sql, "izdatnica");
            dgv.DataSource = DSizdatnice.Tables[0];

            setNulaZaUkupno();

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
            dgv.Columns["ID"].Visible = false;
        }

        #endregion buttons

        #region Util

        private void fillDataGrid()
        {
            string top = " ";
            string remote = ";";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500;";
            }
            else
            {
                top = " TOP(500) ";
            }

            //string sql = "SELECT " + top + " izdatnica.broj AS [Broj izdatnice], izdatnica.id_skladiste AS [Broj skladista]," +
            //    " izdatnica.datum AS [Datum], " +
            //    " partners.ime_tvrtke AS [Partner], sum(izdatnica_stavke.ukupno) as [Ukupno], izdatnica.id_izdatnica as [ID] " +
            //    " FROM izdatnica" +
            //    " LEFT JOIN partners ON izdatnica.id_partner = partners.id_partner " +
            //    " LEFT JOIN izdatnica_stavke ON izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica" +
            //    " GROUP BY izdatnica.broj, izdatnica.id_skladiste, izdatnica.datum, partners.ime_tvrtke, izdatnica.id_izdatnica" +
            //    " ORDER BY izdatnica.broj DESC" + remote;

            string sql = string.Format(@"SELECT DISTINCT{0}izdatnica.broj AS [Broj izdatnice], izdatnica.id_skladiste AS [Broj skladista], izdatnica.datum AS [Datum], partners.ime_tvrtke AS [Partner], sum(round((round(replace(izdatnica_stavke.kolicina, ',', '.')::numeric, 3) * round(izdatnica_stavke.nbc, 2)), 2)) as [Ukupno], izdatnica.id_izdatnica as [ID]
FROM izdatnica
LEFT JOIN partners ON izdatnica.id_partner = partners.id_partner
LEFT JOIN izdatnica_stavke ON izdatnica_stavke.id_izdatnica = izdatnica.id_izdatnica
GROUP BY izdatnica.broj, izdatnica.id_skladiste, izdatnica.datum, partners.ime_tvrtke, izdatnica.id_izdatnica
ORDER BY izdatnica.datum DESC{1}",
                top, remote);

            DSizdatnice = classSQL.select(sql, "izdatnica");
            dgv.DataSource = DSizdatnice.Tables[0];
            dgv.Columns["ID"].Visible = false;

            setNulaZaUkupno();

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void setNulaZaUkupno()
        {
            if (DSizdatnice.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("U bazi nema stavaka!");
            }
            else
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["ukupno"].FormattedValue.ToString().Trim() == "")
                    {
                        try
                        {
                            dgv.Rows[i].Cells["ukupno"].Value = "0,00";
                        }
                        catch
                        {
                            dgv.Rows[i].Cells["ukupno"].Value = "0.00";
                        }
                    }
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
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void fillCB()
        {
            //fill valuta
            DSpartners = classSQL.select("SELECT ime_tvrtke, id_partner FROM partners order by ime_tvrtke", "valute");
            cbPartner.DataSource = DSpartners.Tables[0];
            cbPartner.DisplayMember = "ime_tvrtke";
            cbPartner.ValueMember = "id_partner";
            cbPartner.SelectedValue = 5;

            //fill komercijalist
            DSzaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME, id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DSzaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void printaj()
        {
            Report.Robno.repIzdatnica rav = new Report.Robno.repIzdatnica();
            rav.dokumenat = "AVA";
            rav.ImeForme = "Izdatnice";
            try
            {
                rav.broj_dokumenta = dgv.CurrentRow.Cells["Broj izdatnice"].FormattedValue.ToString();
                rav.broj_skladista = dgv.CurrentRow.Cells["Broj skladista"].FormattedValue.ToString();
                rav.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Izdatnica se ne može isprintati!");
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }

        #endregion Util

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    //txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtPartner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtPartner.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        txtPartner.Select();
                        txtPartner.SelectAll();
                        return;
                    }
                }

                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);

                if (isNum)
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        txtPartner.Select();
                        txtPartner.SelectAll();
                    }
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            txtPartner.Text = DSpar.Rows[0][0].ToString();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                        }
                    }
                }
            }
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
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

        private void button4_Click(object sender, EventArgs e)
        {
            PCPOS.Report.Izdatnica.FormIzdatnicaReport IzdatnicaReport = new PCPOS.Report.Izdatnica.FormIzdatnicaReport();
            IzdatnicaReport.ShowDialog();
        }
    }
}