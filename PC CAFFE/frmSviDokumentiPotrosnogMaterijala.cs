using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviDokumentiPotrosnogMaterijala : Form
    {
        public frmPotrosniMaterijal MainForm { get; set; }
        public string sifra_ponude;

        public frmSviDokumentiPotrosnogMaterijala()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        public frmMenu MainFormMenu { get; set; }

        private void frmSvePonude_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 488, 5);

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            fillCB();
            fillDataGrid();
        }

        private void fillCB()
        {
            //fill komercijalist
            /*DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";*/

            DateTime d1, d2;
            DateTime.TryParse(DateTime.Now.Year.ToString() + "-01-01 00:00:01", out d1);
            DateTime.TryParse(DateTime.Now.Year.ToString() + "-12-31 23:59:59", out d2);
            dtpOD.Value = d1;
            dtpDO.Value = d2;
        }

        private DataTable DTpodaci = new DataTable();

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

            string filter = "";
            filter = " WHERE potrosni_materijal.datum>='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'";
            filter += " AND potrosni_materijal.datum<='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'";

            if (txtSifra.Text.Length > 0) { filter += " AND potrosni_materijal.sifra='" + txtSifra.Text + "'"; }
            if (txtBroj.Text.Length > 0) { filter += " AND potrosni_materijal.broj='" + txtBroj.Text + "'"; }
            if (txtPartner.Text.Length > 0) { filter += " AND potrosni_materijal.id_partner='" + txtPartner.Text + "'"; }

            string sql = @"SELECT
                            potrosni_materijal.broj,
                            potrosni_materijal.godina,
                            partners.ime_tvrtke,
                            CONCAT(zaposlenici.ime,' ',zaposlenici.prezime) as djelatnici,
                            ROUND(SUM((cijena-(cijena*rabat/100))*kolicina),2) AS ukupno
                            FROM potrosni_materijal
                            LEFT JOIN partners ON potrosni_materijal.id_partner = partners.id_partner
                            LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = potrosni_materijal.id_zaposlenik
                            " + filter + @"
                            GROUP BY potrosni_materijal.broj,potrosni_materijal.godina,partners.ime_tvrtke,djelatnici
                            ORDER BY potrosni_materijal.broj  DESC " + remote + ";";

            DTpodaci = classSQL.select(sql, "fakture").Tables[0];

            dgv.Rows.Clear();
            foreach (DataRow r in DTpodaci.Rows)
            {
                dgv.Rows.Add(r["broj"].ToString(), r["godina"].ToString(), r["ime_tvrtke"].ToString(), r["djelatnici"].ToString(), r["ukupno"].ToString());
            }
            dgv_CellClick(null, null);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["broj"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmPotrosniMaterijal();
                    childForm.broj_documenta_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_documenta_edit = broj;
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
            fillDataGrid();
        }

        private void frmSvePonude_Activated(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string sql = @"SELECT * FROM potrosni_materijal
                            WHERE broj='" + dgv.CurrentRow.Cells["broj"].FormattedValue.ToString() + "' AND godina='" + dgv.CurrentRow.Cells["godina"].FormattedValue.ToString() + @"'
                            ORDER BY potrosni_materijal.id  DESC;";

                DTpodaci = classSQL.select(sql, "potrosni_materijal").Tables[0];

                dataGridView1.Rows.Clear();
                foreach (DataRow r in DTpodaci.Rows)
                {
                    decimal _kolicina = 0, _mpc = 0, _porez = 0, _rabat = 0, _sveukupno = 0;

                    decimal.TryParse(r["kolicina"].ToString(), out _kolicina);
                    decimal.TryParse(r["cijena"].ToString(), out _mpc);
                    decimal.TryParse(r["porez"].ToString(), out _porez);
                    decimal.TryParse(r["rabat"].ToString(), out _rabat);

                    _sveukupno = (_mpc - (_mpc * _rabat / 100)) * _kolicina;

                    dataGridView1.Rows.Add(r["sifra"].ToString(), r["naziv"].ToString(), r["kolicina"].ToString(), r["porez"].ToString(), Math.Round(_mpc, 3).ToString("#0.00"), Math.Round(_rabat, 3).ToString("#0.00"), Math.Round(_sveukupno, 3).ToString("#0.00"));
                }
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