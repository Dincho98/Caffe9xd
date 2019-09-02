using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.izvjestaji.Prometi
{
    public partial class frmPrometiOdabir : Form
    {
        public frmPrometiOdabir()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmIspisProdajnihArtiklaNaMaliPrinter_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetCB();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
            DateTime datumNow = DateTime.Now;
            dtpOD.Value = new DateTime(datumNow.Year, datumNow.Month, 1, 0, 0, 0);
            dtpDO.Value = new DateTime(datumNow.Year, datumNow.Month, DateTime.DaysInMonth(datumNow.Year, datumNow.Month), 23, 59, 59);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataTable DT_zemlja;

        private void SetCB()
        {
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
            RESORT.izvjestaji.Prometi.frmPrometi pr = new RESORT.izvjestaji.Prometi.frmPrometi();
            pr.sifra_partnera = txtSifraPartnera.Text;
            pr.imeGosta = txtImeGosta.Text;
            pr.OD = dtpOD.Value;
            pr.DO = dtpDO.Value;
            pr.dokumenat = txtBrojRacuna.Text;
            pr.ShowDialog();
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            sifarnici.frmPartnerTrazi partnerTrazi = new sifarnici.frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (RESORT.Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = RemoteDB.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + RESORT.Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}