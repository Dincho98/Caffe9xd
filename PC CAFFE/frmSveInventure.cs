using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveInventure : Form
    {
        public frmSveInventure()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmUnosInventura MainForm { get; set; }
        public string broj__inventure;
        private DataSet DS_Zaposlenik;
        private DataSet DSinventura;
        public frmMenu MainFormMenu { get; set; }

        private void frmSveInventure_Load(object sender, EventArgs e)
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
            PaintRows(dgv);
           // this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            DataTable DTzap = classSQL.select("SELECT id_zaposlenik, id_dopustenje FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

            string oibTvrtke = Util.Korisno.oibTvrtke;

            if ((oibTvrtke == "05593216962" || oibTvrtke == "77566209058") && Convert.ToInt32(DTzap.Rows[0]["id_dopustenje"]) > 2)
            {
                btnSveFakture.Visible = false;
            }
            else
            {
                btnSveFakture.Visible = true;
            }
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 300";
            }
            else
            {
                top = " TOP(300) ";
            }

            string sql = "SELECT " + top + " inventura.broj_inventure AS Broj,inventura.datum AS Datum ,inventura.godina AS Godina ,zaposlenici.ime + ' ' + zaposlenici.prezime as Izradio,skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
                " FROM inventura" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = inventura.id_skladiste" +
                " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik ORDER BY CAST(inventura.broj_inventure AS integer)" +
                "" + remote + "";

            DSinventura = classSQL.select(sql, "fakture");
            dgv.DataSource = DSinventura.Tables[0];
            dgv.Columns["id_skladiste"].Visible = false;
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

        private void frmSveInventure_Activated(object sender, EventArgs e)
        {
            PaintRows(dgv);
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
                Broj = "inventura.broj_inventure='" + txtBroj.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "inventura.datum >='" + dtpOD.Value.ToString("MM.dd.yyyy") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "inventura.datum <='" + dtpDO.Value.ToString("MM.dd.yyyy") + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "inventura.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
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
                remote = " LIMIT 300";
            }
            else
            {
                top = " TOP(300) ";
            }

            string sql = "SELECT " + top + " inventura.broj_inventure AS Broj,inventura.datum AS Datum ,inventura.godina AS Godina ,zaposlenici.ime + ' ' + zaposlenici.prezime as Izradio,skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
            " FROM inventura" +
            " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik" +
            " LEFT JOIN skladiste ON skladiste.id_skladiste = inventura.id_skladiste" +
            filter + " ORDER BY CAST(inventura.broj_inventure AS integer)" + remote;

            DSinventura = classSQL.select(sql, "fakture");
            dgv.DataSource = DSinventura.Tables[0];
            dgv.Columns["id_skladiste"].Visible = false;
            PaintRows(dgv);
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

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();
                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmUnosInventura();
                    childForm.broj_inventure_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                    this.Close();
                }
                else
                {
                    MainForm.broj_inventure_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Inventura.frmInventura aa = new Report.Inventura.frmInventura();
            aa.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            aa.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            aa.ShowDialog();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.Inventura.frmInventura aa = new Report.Inventura.frmInventura();
            aa.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            aa.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            aa.ShowDialog();
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