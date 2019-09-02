using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviNormativi : Form
    {
        public frmMenu MainFormMenu { get; set; }
        public frmNormativi MainForm { get; set; }
        private DataSet DSnormativ = new DataSet();
        public string sifra_normativa;

        public frmSviNormativi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSviNormativi_Load(object sender, EventArgs e)
        {
            PaintRows(dgv);
            filldgv();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

        private void filldgv()
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

            string sql = "SELECT " + top + " normativi.broj_normativa AS [Broj normativa],roba.naziv AS [Ime artikla/usluge]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio, normativi.godina_normativa as Godina  FROM normativi " +
                " INNER JOIN roba ON roba.sifra=normativi.sifra_artikla" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=normativi.id_zaposlenik ORDER BY CAST(normativi.broj_normativa AS integer) " +
                "" + remote + "";

            DSnormativ = classSQL.select(sql, "normativi");
            dgv.DataSource = DSnormativ.Tables[0];
            //fill komercijalist

            DataSet DS_Zaposlenik = new DataSet();
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj normativa"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmNormativi();
                    childForm.broj_normativa_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_normativa_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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
                Broj = "normativi.broj_normativa='" + txtBroj.Text + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "normativi_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }

            if (chbIzradio.Checked)
            {
                Izradio = "normativi.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT DISTINCT " + top + " normativi.broj_normativa AS [Broj normativa],roba.naziv AS [Ime artikla/usluge]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio, normativi.godina_normativa as Godina  FROM normativi " +
                " INNER JOIN roba ON roba.sifra=normativi.sifra_artikla" +
                " INNER JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=normativi.id_zaposlenik" + filter + remote +
                "";

            DSnormativ = classSQL.select(sql, "normativi");
            dgv.DataSource = DSnormativ.Tables[0];
            PaintRows(dgv);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokumenat = "NORMATIV";
            rfak.ImeForme = "Normativi";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj normativa"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokumenat = "NORMATIV";
            rfak.ImeForme = "Normativi";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj normativa"].FormattedValue.ToString();
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