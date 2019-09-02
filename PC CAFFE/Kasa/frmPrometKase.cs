using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPrometKase : Form
    {
        bool SlanjeDokumenata;
        DateTime PocetniDatum;
        DateTime ZavrsniDatum;

        public frmPrometKase(bool slanjeDokumenata=false,DateTime? pocetniDatum=null,DateTime? zavrsniDatum=null)
        {
            InitializeComponent();

            if (slanjeDokumenata)
            {
                SlanjeDokumenata = slanjeDokumenata;
                PocetniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(pocetniDatum), 0, 0, 1);
                ZavrsniDatum = Global.GlobalFunctions.GenerirajDatumSVremenom(Convert.ToDateTime(zavrsniDatum), 23, 59, 59);
            }
        }

        private void frmPrometKase_Load(object sender, EventArgs e)
        {
            SetCB();
            //this.Paint += new PaintEventHandler(Form1_Paint);

            if (SlanjeDokumenata)
            {
                Report.Liste2.frmListe formListe = new Report.Liste2.frmListe(true);
                formListe.datumOD = PocetniDatum;
                formListe.datumDO = ZavrsniDatum;
                formListe.ImeForme = "Promet kase";
                formListe.dokumenat = "PROMET";
                formListe.ShowDialog();
                this.Close();
            }
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        //DataTable DT_Ducan;
        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_skladiste", typeof(string));
            DT_Skladiste2.Columns.Add("skladiste", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste WHERE id_skladiste NOT IN(1)", "skladiste").Tables[0];

            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_skladiste"].ToString(), DT_Skladiste.Rows[i]["skladiste"].ToString());
            }

            cbSkladiste.DataSource = DT_Skladiste2;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dtpOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnIspis.PerformClick();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.Liste2.frmListe aa = new Report.Liste2.frmListe();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet kase";
            aa.dokumenat = "PROMET";
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                aa.skladiste = cbSkladiste.SelectedValue.ToString();
            }
            aa.ShowDialog();
        }
    }
}