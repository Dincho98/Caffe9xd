using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmSviRacuni : Form
    {
        public frmSviRacuni()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();

        public bool storno { get; set; }
        public string BrojRacuna { get; set; }

        private void frmSviRacuni_Load(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None) { this.WindowState = FormWindowState.Maximized; }
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            //int heigt = SystemInformation.VirtualScreen.Height;
            //this.Height = heigt - 60;
            //this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        //void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

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

        private void PaintRows(DataGridView dg)
        {
            //int br = 0;
            //int BrojIspisa;

            //for (int i = 0; i < dg.Rows.Count; i++)
            //{
            //    int.TryParse(dg.Rows[i].Cells["Broj ispisa"].FormattedValue.ToString(), out BrojIspisa);

            //  if (dg.Rows[i].Cells["Storno"].FormattedValue.ToString() == "DA")
            //  {
            //       dg.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //  }
            //   else if (BrojIspisa > 1)
            // {
            //    dg.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
            //  }
            //}

            // DataGridViewRow row = dg.RowTemplate;
            // row.Height = 22;
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 2000";
            }
            else
            {
                top = " TOP(2000) ";
            }

            string sql = "SELECT " + top + "  racuni.broj_racuna AS [Broj računa], racuni.datum_racuna AS [Datum]" +
                ",partners.ime_tvrtke AS [Partner],zaposlenici.ime +' '+ zaposlenici.prezime AS [Blagajnik] ,racuni.ukupno::numeric as [Ukupno]" +
                ", racuni.storno AS [Storno],racuni.broj_ispisa AS [Broj ispisa],racuni.godina,racuni.id_kasa,racuni.id_ducan " +
                " FROM racuni " +
                " LEFT JOIN partners ON racuni.id_kupac = partners.id_partner " +
                " LEFT JOIN zaposlenici ON racuni.id_blagajnik=zaposlenici.id_zaposlenik ORDER BY racuni.datum_racuna DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "racuni");
            dgv.DataSource = DSfakture.Tables[0];

            dgv.Columns["id_kasa"].Visible = dgv.Columns["id_ducan"].Visible = false;
            dgv.Columns["godina"].Visible = false;

            //if (File.Exists("dontshownumbersofprint"))
            {
                dgv.Columns["Broj ispisa"].Visible = false;
            }

            dgv.Columns[6].Width = 70;
            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string sifra = dgv.CurrentRow.Cells["Broj fakture"].Value.ToString();
                //sifra_fakture = sifra;
                //MainForm.broj_fakture_edit = sifra_fakture;
                //MainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "racuni.broj_racuna='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "racuni.id_kupac='" + txtPartner.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "racuni.datum_racuna >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "racuni.datum_racuna <='" + dtpDO.Value.ToString("yyyy-MM-dd") + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "racun_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }

            if (chbIzradio.Checked)
            {
                Izradio = "racuni.id_blagajnik='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 3000";
            }
            else
            {
                top = " TOP(3000) ";
            }

            string sql = "SELECT DISTINCT " + top + "  racuni.broj_racuna AS [Broj računa], racuni.datum_racuna AS [Datum]" +
                ",partners.ime_tvrtke AS [Partner],zaposlenici.ime +' '+ zaposlenici.prezime AS [Blagajnik] ,racuni.ukupno::numeric as [Ukupno]" +
                ", racuni.storno AS [Storno],racuni.broj_ispisa AS [Broj ispisa],racuni.godina,racuni.id_kasa,racuni.id_ducan" +
                " FROM racuni " +
                " LEFT JOIN partners ON racuni.id_kupac = partners.id_partner " +
                " LEFT JOIN racun_stavke ON racuni.broj_racuna = racun_stavke.broj_racuna  AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna " +
                " LEFT JOIN zaposlenici ON racuni.id_blagajnik=zaposlenici.id_zaposlenik " + filter + " ORDER BY racuni.datum_racuna DESC" +

                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns[6].Width = 70;

            dgv.Columns["id_kasa"].Visible = dgv.Columns["id_ducan"].Visible = false;
            dgv.Columns["godina"].Visible = false;
            // if (File.Exists("dontshownumbersofprint"))
            {
                dgv.Columns["Broj ispisa"].Visible = false;
            }
            PaintRows(dgv);
        }

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (storno)
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                BrojRacuna = row.Cells[0].Value.ToString();
                Close();
            }
            else
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();

                rfak.godina = dgv.CurrentRow.Cells["godina"].FormattedValue.ToString();
                rfak.id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.id_poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();

                rfak.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();

            rfak.godina = dgv.CurrentRow.Cells["godina"].FormattedValue.ToString();
            rfak.id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
            rfak.id_poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();

            rfak.ShowDialog();
        }

        private void btnNaknadnoDodavanjePartnera_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    return;

                int rowIndex = dgv.CurrentCell.RowIndex;
                int broj_racuna = Convert.ToInt32(dgv.Rows[rowIndex].Cells["Broj računa"].Value);
                int id_ducan = Convert.ToInt32(dgv.Rows[rowIndex].Cells["id_ducan"].Value);
                int id_kasa = Convert.ToInt32(dgv.Rows[rowIndex].Cells["id_kasa"].Value);
                string partner = dgv.Rows[rowIndex].Cells["Partner"].Value.ToString();

                if (partner.Length != 0)
                {
                    MessageBox.Show("Dodavanje partnera na R1 račun nije dozvoljeno.");
                    return;
                }

                if (MessageBox.Show("Želite naknadno dodati partnera na odabrani račun?", "R1 račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    frmPartnerTrazi f = new frmPartnerTrazi();
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        partner = classSQL.select("select ime_tvrtke from partners where id_partner = '" + Properties.Settings.Default.id_partner + "';", "partners").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        if (MessageBox.Show("Sigurno želite dodati partnera " + partner + " na račun broj " + dgv.Rows[rowIndex].Cells["Broj računa"].Value, "R1 račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string sql = "update racuni set id_kupac = '" + Properties.Settings.Default.id_partner + "' where broj_racuna = '" + broj_racuna + "' and id_ducan = '" + id_ducan + "' and id_kasa = '" + id_kasa + "';";
                            classSQL.update(sql);
                            dgv.Rows[rowIndex].Cells["Partner"].Value = partner;
                            MessageBox.Show("Izvršeno");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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