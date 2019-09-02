using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviRadniNalozi : Form
    {
        public frmMenu MainFormMenu { get; set; }
        public frmRadniNalog MainForm { get; set; }

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSrn = new DataSet();

        public string sifra_rn;

        public frmSviRadniNalozi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSviRadniNalozi_Load(object sender, EventArgs e)
        {
            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
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

            string sql = "SELECT " + top + " radni_nalog.broj_naloga AS Broj,radni_nalog.datum_naloga AS [Datum naloga],partners.ime_tvrtke AS [Naručioc],zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio " +
            " FROM radni_nalog " +
            " LEFT JOIN partners ON radni_nalog.id_narucioc = partners.id_partner " +
            " INNER JOIN zaposlenici ON radni_nalog.id_izradio = zaposlenici.id_zaposlenik ORDER BY CAST(radni_nalog.broj_naloga AS integer) DESC" +
            "" + remote + "";

            DSrn = classSQL.select(sql, "radni_nalog");
            dgv.DataSource = DSrn.Tables[0];
            PaintRows(dgv);
        }

        private void fillCB()
        {
            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='rna' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
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
                    var childForm = new frmRadniNalog();
                    childForm.broj_RN_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_RN_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
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
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "radni_nalog.broj_naloga='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "radni_nalog.id_narucioc='" + txtPartner.Text + "' AND ";
            }
            if (chbVD.Checked)
            {
                VD = "radni_nalog.vrasta_dokumenta='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "radni_nalog.datum_naloga >='" + dtOd + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "radni_nalog.datum_naloga <='" + dtDo + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "radni_nalog_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "radni_nalog.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + SifraArtikla + Izradio;

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

            string sql = "SELECT DISTINCT " + top + " radni_nalog.broj_naloga AS Broj,radni_nalog.datum_naloga AS [Datum naloga],partners.ime_tvrtke AS [Naručioc],zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio " +
            " FROM radni_nalog " +
            " LEFT JOIN partners ON radni_nalog.id_narucioc = partners.id_partner " +
            " LEFT JOIN radni_nalog_stavke ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga " +
            " INNER JOIN zaposlenici ON radni_nalog.id_izradio = zaposlenici.id_zaposlenik " + filter + " ORDER BY radni_nalog.datum_naloga DESC" +
            "" + remote + "";

            DSrn = classSQL.select(sql, "radni_nalog");
            dgv.DataSource = DSrn.Tables[0];
            PaintRows(dgv);
        }

        private void frmSviRadniNalozi_Activated(object sender, EventArgs e)
        {
            PaintRows(dgv);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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