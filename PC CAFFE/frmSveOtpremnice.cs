using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveOtpremnice : Form
    {
        private DataSet DSfakture = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        public frmOtpremnica MainForm { get; set; }
        public string sifra_otpremnice;
        public frmMenu MainFormMenu { get; set; }

        public frmSveOtpremnice()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSveOtpremnice_Load(object sender, EventArgs e)
        {
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
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            PaintRows(dgv);
        }

        private void PaintRows(DataGridView dg)
        {
            //int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                string naplaceno = dg.Rows[i].Cells[5].FormattedValue.ToString();
                if (naplaceno == "Naplačeno")
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    //br++;
                }
                else if (naplaceno == "Nenaplačeno")
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
                    //br = 0;
                }
                else if (naplaceno == "Obrisano")
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                    //br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 22;
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            string sql = string.Format(@"SELECT {0} otpremnice.broj_otpremnice AS [Broj otpremnice], otpremnice.datum AS [Datum],
                                        case when otpremnice.na_sobu = true then sobe.naziv_sobe else
                                        partners.ime_tvrtke end AS [Partner],
                                        otpremnice.ukupno::numeric as [Iznos],
                                        otpremnice.id_skladiste as [Skladište],
                                        case when otpremnice.naplaceno_fakturom = 2 then 'Naplačeno'
					                     when otpremnice.naplaceno_fakturom = 1 then 'Nenaplačeno'
                                         when otpremnice.naplaceno_fakturom = 0 then 'Obrisano' end AS [Status]
                                        FROM otpremnice
                                        LEFT JOIN partners ON otpremnice.osoba_partner = partners.id_partner
                                        LEFT JOIN sobe ON otpremnice.osoba_partner = sobe.id
                                        ORDER BY otpremnice.datum DESC {1};",
                top,
                remote);

            DSfakture = classSQL.select(sql, "otpremnice");
            dgv.DataSource = DSfakture.Tables[0];
            if (DSfakture.Tables[0].Rows.Count > 0)
                PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount != 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj otpremnice"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmOtpremnica();
                    childForm.broj_otpremnice_edit = broj;
                    childForm.skladiste_edit = dgv.CurrentRow.Cells["skladiste"].Value.ToString();
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_otpremnice_edit = broj;
                    MainForm.skladiste_edit = dgv.CurrentRow.Cells["Skladište"].Value.ToString();
                    MainForm.Show();
                    this.Close();
                }
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
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked && !string.IsNullOrWhiteSpace(txtBroj.Text))
            {
                Broj = "otpremnice.broj_otpremnice='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked && !string.IsNullOrWhiteSpace(txtPartner.Text))
            {
                Partner = " case when otpremnice.na_sobu = false then otpremnice.osoba_partner='" + txtPartner.Text + "' else 1=1 end AND ";
            }

            if (chbOD.Checked)
            {
                DateStart = "otpremnice.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "otpremnice.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbArtikl.Checked && !string.IsNullOrWhiteSpace(cbArtikl.Text))
            {
                SifraArtikla = "otpremnica_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "otpremnice.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";

            string sql = string.Format(@"SELECT DISTINCT {0} otpremnice.broj_otpremnice AS [Broj otpremnice], otpremnice.datum AS [Datum],
                case when otpremnice.na_sobu = true then sobe.naziv_sobe else
                partners.ime_tvrtke end AS [Partner], otpremnice.id_skladiste as [Skladište], otpremnice.ukupno::numeric as [Iznos], 
                case when otpremnice.naplaceno_fakturom = 2 then 'Naplačeno'
					when otpremnice.naplaceno_fakturom = 1 then 'Nenaplačeno'
                    when otpremnice.naplaceno_fakturom = 0 then 'Obrisano' end AS [Status] 
                FROM otpremnice
                LEFT JOIN partners ON otpremnice.osoba_partner = partners.id_partner
                LEFT JOIN otpremnica_stavke ON otpremnica_stavke.broj_otpremnice = otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste=otpremnice.id_skladiste
                left join sobe on otpremnice.osoba_partner = sobe.id
                {1}
                ORDER BY otpremnice.datum DESC {2};",
                top, filter, remote);

            DSfakture = classSQL.select(sql, "otpremnice");
            dgv.DataSource = DSfakture.Tables[0];
            if (DSfakture.Tables[0].Rows.Count > 0)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "OTP";
            rfak.ImeForme = "Otpremnica";
            //rfak.from_skladiste = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "OTP";
            rfak.from_skladiste = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();
            rfak.ImeForme = "Otpremnica";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnIspisNaPos_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    int rowindex = dgv.CurrentCell.RowIndex;
                    if (rowindex > -1)
                    {
                        string broj_otpremnice = "0";

                        try
                        {
                            broj_otpremnice = dgv.Rows[rowindex].Cells["Broj otpremnice"].Value.ToString();

                            if (broj_otpremnice != null && broj_otpremnice != "0")
                            {
                                Class.Otpremnica _otpremnica = new Class.Otpremnica();
                                _otpremnica.otpremnicaPripremaZaPrint(broj_otpremnice);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSrchPartner_Click(object sender, EventArgs e)
        {
            try
            {
                frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                partnerTrazi.ShowDialog();
                if (Properties.Settings.Default.id_partner != "")
                {
                    txtPartner.Text = Properties.Settings.Default.id_partner;
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnObrisiOtpremnicu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    var confirm = MessageBox.Show("Jeste li sigurni da želite obrisati odabranu otpremnicu?", "Upozorenje", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        int rowindex = dgv.CurrentCell.RowIndex;
                        if (rowindex > -1)
                        {
                            int broj_otpremnice = Convert.ToInt32(dgv.Rows[rowindex].Cells["Broj otpremnice"].Value);
                            string queryOtpremnice = $@"UPDATE otpremnice SET naplaceno_fakturom = 0, ukupno = 0 WHERE broj_otpremnice = {broj_otpremnice}";
                            classSQL.update(queryOtpremnice);
                            MessageBox.Show("Otpremnica obrisana!");
                            RefreshGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            fillDataGrid();
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