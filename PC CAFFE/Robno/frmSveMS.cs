using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveMS : Form
    {
        private DataSet DS;

        public frmSveMS()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public Robno.frmMeduskladisnica MainForm { get; set; }
        public frmMenu MainFormMenu { get; set; }
        public string sifra;

        private void frmSviPovrati_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 100;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            fillCB();
            fillDataGrid("", "", "", "", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string zaposlenik = "";
            string broj = "";
            string sifra = "";
            string oddatuma = "";
            string dodatuma = "";

            if (chbBroj.Checked && txtBroj.Text != "") { broj = " AND medu_poslovnice.broj='" + txtBroj.Text + "'"; }
            if (chbIzradio.Checked) { zaposlenik = " AND medu_poslovnice.id_izradio='" + cbIzradio.SelectedValue + "'"; }
            if (chbSifra.Checked && txtSifra.Text != "") { sifra = " AND medu_poslovnice.sifra='" + txtSifra.Text + "'"; }
            if (chbOD.Checked) { oddatuma = " AND medu_poslovnice.datum>='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'"; }
            if (chbDO.Checked) { dodatuma = " AND medu_poslovnice.datum<='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'"; }

            fillDataGrid(zaposlenik, broj, sifra, oddatuma, dodatuma);
        }

        private void fillDataGrid(string zaposlenik, string broj, string sifra, string oddatuma, string dodatuma)
        {
            string sql = @"SELECT iz.naziv_poslovnice as izposlovnice,u.naziv_poslovnice as uposlovnicu, medu_poslovnice.u_poslovnicu,medu_poslovnice.iz_poslovnice,
                        ROUND(COALESCE(SUM(nbc*kolicina),0),2) AS ukupno,medu_poslovnice.datum,zaposlenici.ime,
                        zaposlenici.prezime, medu_poslovnice.broj, medu_poslovnice.godina
                        FROM medu_poslovnice
                        LEFT JOIN poslovnice u ON u.fiskalna_oznaka_poslovnice=medu_poslovnice.u_poslovnicu
                        LEFT JOIN poslovnice iz ON iz.fiskalna_oznaka_poslovnice=medu_poslovnice.iz_poslovnice
                        LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=medu_poslovnice.id_izradio
                        WHERE broj>'0' " + zaposlenik + broj + sifra + oddatuma + dodatuma +
                        @"GROUP BY medu_poslovnice.broj, izposlovnice, uposlovnicu,medu_poslovnice.datum,
                        zaposlenici.ime,zaposlenici.prezime,medu_poslovnice.godina,medu_poslovnice.u_poslovnicu,medu_poslovnice.iz_poslovnice
                        ORDER BY medu_poslovnice.broj DESC";

            DataTable DT = classSQL.select(sql, "medu_poslovnice").Tables[0];
            dgv.Rows.Clear();

            foreach (DataRow r in DT.Rows)
            {
                dgv.Rows.Add(r["broj"].ToString(),
                    r["datum"].ToString(),
                    r["ime"].ToString() + "" + r["prezime"].ToString(),
                    r["izposlovnice"].ToString(),
                    r["uposlovnicu"].ToString(),
                    r["ukupno"].ToString(),
                    r["godina"].ToString(), r["iz_poslovnice"].ToString(), r["u_poslovnicu"].ToString());
            }
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["broj"].Value.ToString();
                string god = dgv.CurrentRow.Cells["godina"].Value.ToString();
                string _iz_poslovnice = dgv.CurrentRow.Cells["iz_poslovnice"].Value.ToString();

                if (Until.classZakljucavanjeDokumenta.isLockMeduskladisnica(Convert.ToInt32(broj), _iz_poslovnice, Convert.ToInt32(god)))
                {
                    MessageBox.Show("Međuskladišnica je zaključana. Uređivanje nije dopušteno.");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    Robno.frmMeduskladisnica childForm = new Robno.frmMeduskladisnica();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_ms_edit = broj;
                    childForm._godina_edit = god;
                    childForm._iz_poslovnice = _iz_poslovnice;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_ms_edit = broj;
                    MainForm._godina_edit = god;
                    MainForm._iz_poslovnice = _iz_poslovnice;
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

        private DataSet DS_Zaposlenik;

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
            rfak.dokumenat = "MEDU_POS";
            rfak.ImeForme = "Ispis međuskladišnice";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            rfak.godina = dgv.CurrentRow.Cells["godina"].FormattedValue.ToString();
            rfak._iz_poslovnice = dgv.CurrentRow.Cells["iz_poslovnice"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokumenat = "MEDU_POS";
            rfak.ImeForme = "Ispis međuskladišnice";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            rfak.godina = dgv.CurrentRow.Cells["godina"].FormattedValue.ToString();
            rfak._iz_poslovnice = dgv.CurrentRow.Cells["iz_poslovnice"].FormattedValue.ToString();
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
    }
}