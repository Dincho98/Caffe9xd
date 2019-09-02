using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.predracuni
{
    public partial class frmIspisPredracuna : Form
    {
        public frmIspisPredracuna()
        {
            InitializeComponent();
        }

        private void frmIspisProdajnihArtiklaNaMaliPrinter_Load(object sender, EventArgs e)
        {
            dtpOD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 01);
            dtpDO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            PostaviZaposlenike();
            SetCB();
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void PostaviZaposlenike()
        {
            DataTable DTz = classSQL.select("SELECT id_zaposlenik,ime + ' ' + prezime AS ime,id_zaposlenik FROM zaposlenici WHERE aktivan='DA'", "zaposlenici").Tables[0];

            for (int i = 0; i < DTz.Rows.Count; i++)
            {
                dgvD.Rows.Add(DTz.Rows[i]["ime"].ToString(), false, DTz.Rows[i]["id_zaposlenik"].ToString());
            }
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_stolovi;
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
            /*DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";*/

            //fill stol
            DT_stolovi = classSQL.select("SELECT id_stol,naziv FROM stolovi", "stolovi").Tables[0];
            cbOdabirStola.DataSource = DT_stolovi;
            cbOdabirStola.DisplayMember = "naziv";
            cbOdabirStola.ValueMember = "id_stol";
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
            IspisPredracun(false);
        }

        private void btnIspisZbirno_Click(object sender, EventArgs e)
        {
            IspisPredracun(true);
        }

        private void btnPosIspis_Click(object sender, EventArgs e)
        {
            frmPOSMaliPrinter mp = new frmPOSMaliPrinter();
            mp.datumOD = dtpOD.Value;
            mp.zbirno = false;
            mp.datumDO = dtpDO.Value;
            mp.idstol = "";
            //mp.ducan = cbDucan.SelectedValue.ToString();
            //mp.blagajnik = cbZaposlenik.SelectedValue.ToString();
            if (chbDucan.Checked)
            {
                mp.ducan = cbDucan.SelectedValue.ToString();
            }
            /*
            if (chbBlagajnik.Checked)
            {
                mp.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }*/

            mp.dgv = dgvD;

            if (chbOdabirStola.Checked)
            {
                mp.idstol = cbOdabirStola.SelectedValue.ToString();
            }
            mp.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPOSMaliPrinter mp = new frmPOSMaliPrinter();
            mp.idstol = "";
            mp.datumOD = dtpOD.Value;
            mp.datumDO = dtpDO.Value;
            mp.zbirno = true;
            //mp.ducan = cbDucan.SelectedValue.ToString();
            //mp.blagajnik = cbZaposlenik.SelectedValue.ToString();
            if (chbDucan.Checked)
            {
                mp.ducan = cbDucan.SelectedValue.ToString();
            }
            /*if (chbBlagajnik.Checked)
            {
                mp.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }*/

            mp.dgv = dgvD;

            if (chbOdabirStola.Checked)
            {
                mp.idstol = cbOdabirStola.SelectedValue.ToString();
            }
            mp.ShowDialog();
        }

        private void dgvD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }

            if (dgvD.Rows[e.RowIndex].Cells["oznaci"].FormattedValue.ToString() == "True")
            {
                dgvD.Rows[e.RowIndex].Cells["oznaci"].Value = false;
            }
            else
            {
                dgvD.Rows[e.RowIndex].Cells["oznaci"].Value = true;
            }
        }

        private void IspisPredracun(bool zbirno)
        {
            PCPOS.Report.predracuni.frmIzdanePonude aa = new PCPOS.Report.predracuni.frmIzdanePonude();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ducan_naziv = "";
            aa.blagajnik_naziv = "";
            aa.stol = "";
            aa.zbirno = zbirno;
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
                aa.ducan_naziv = cbDucan.Text;
            }

            /*
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
                aa.blagajnik_naziv = cbZaposlenik.Text;
            }
            */

            aa.dgv = dgvD;

            if (chbOdabirStola.Checked)
            {
                aa.stol = cbOdabirStola.SelectedValue.ToString();
            }

            aa.ShowDialog();
        }
    }
}