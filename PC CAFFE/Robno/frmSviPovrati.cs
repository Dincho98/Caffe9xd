using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSviPovrati : Form
    {
        public frmPovratRobe MainForm { get; set; }
        public frmMenu MainFormMenu { get; set; }
        private DataSet DS;
        private DataSet DS_Zaposlenik;
        public string sifra;

        public frmSviPovrati()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSviPovrati_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            fillCB();
            fillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

            string sql = "SELECT " + top + " povrat_robe.broj AS [Broj], povrat_robe.datum AS [Datum],skladiste.skladiste AS [Skladište]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio]" +
                " FROM povrat_robe " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = povrat_robe.id_izradio " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = povrat_robe.id_skladiste " +
                " ORDER BY CAST(povrat_robe.broj AS integer) DESC" +
                "" + remote + "";

            DS = classSQL.select(sql, "fakture");
            dgv.DataSource = DS.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
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
            row.Height = 22;
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        //dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
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

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();
                if (Until.classZakljucavanjeDokumenta.isLockOtpisRobe(Convert.ToInt32(broj)))
                {
                    MessageBox.Show("Otpis robe je zaključan. Uređivanje nije dopušteno.");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    Robno.frmPovratRobe childForm = new Robno.frmPovratRobe();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_povrata_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_povrata_edit = broj;
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
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "povrat_robe.broj='" + txtBroj.Text + "' AND ";
            }

            if (chbOD.Checked)
            {
                DateStart = "povrat_robe.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "povrat_robe.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }

            if (chbIzradio.Checked)
            {
                Izradio = "povrat_robe.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
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
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            string sql = "SELECT " + top + " povrat_robe.broj AS [Broj], povrat_robe.datum AS [Datum],skladiste.skladiste AS [Skladište]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio]" +
                " FROM povrat_robe " +
                //" LEFT JOIN povrat_robe_stavke ON povrat_robe_stavke.broj = povrat_robe.broj " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = povrat_robe.id_izradio " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = povrat_robe.id_skladiste " + filter +
                "" + remote + "";

            DS = classSQL.select(sql, "povrat_robe");
            dgv.DataSource = DS.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

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
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokumenat = "ODJAVA";
            rfak.ImeForme = "Odjava robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokumenat = "ODJAVA";
            rfak.ImeForme = "Povrat robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Report.OtpisRobe.FormOtpisRobeReport report = new Report.OtpisRobe.FormOtpisRobeReport();
            report.ShowDialog();
        }
    }
}