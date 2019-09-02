using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmPodaciTvrtke : Form
    {
        public frmPodaciTvrtke()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmPodaci_tvrtka_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

            DataTable DTSK = new DataTable("nacin");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("R1", "Račun R1");
            DTSK.Rows.Add("R2", "Račun R2");

            cbR1.DataSource = DTSK;
            cbR1.DisplayMember = "naziv";
            cbR1.ValueMember = "id";

            SetValue();

            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void SetValue()
        {
            DataTable DTpodaci = classDBlite.LiteSelect("SELECT * FROM podaci_tvrtke WHERE id='1'", "podaci_tvrtka").Tables[0];
            txtImeTvrtke.Text = DTpodaci.Rows[0]["ime_tvrtke"].ToString();
            txtSkraceno.Text = DTpodaci.Rows[0]["skraceno_ime"].ToString();
            txtOib.Text = DTpodaci.Rows[0]["oib"].ToString();
            txtTelefon.Text = DTpodaci.Rows[0]["tel"].ToString();
            txtFax.Text = DTpodaci.Rows[0]["fax"].ToString();
            txtMobitel.Text = DTpodaci.Rows[0]["mob"].ToString();
            txtAdresa.Text = DTpodaci.Rows[0]["adresa"].ToString();
            txtVlasnik.Text = DTpodaci.Rows[0]["vl"].ToString();
            txtIBAN2.Text = DTpodaci.Rows[0]["iban1"].ToString();
            txtGrad.Text = DTpodaci.Rows[0]["grad"].ToString();
            txtEmail.Text = DTpodaci.Rows[0]["email"].ToString();
            txtPoslovnicaAdresa.Text = DTpodaci.Rows[0]["poslovnica_adresa"].ToString();
            txtGradPoslovnica.Text = DTpodaci.Rows[0]["poslovnica_grad"].ToString();
            txtIBAN.Text = DTpodaci.Rows[0]["iban"].ToString();

            rtbKrajDokumenta.Text = DTpodaci.Rows[0]["opis_na_kraju_fakture"].ToString();
            cbR1.SelectedValue = DTpodaci.Rows[0]["r1"].ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void brnSpremi_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE podaci_tvrtke SET " +
                " ime_tvrtke='" + txtImeTvrtke.Text + "'," +
                " skraceno_ime='" + txtSkraceno.Text + "'," +
                " oib='" + txtOib.Text + "'," +
                " tel='" + txtTelefon.Text + "'," +
                " fax='" + txtFax.Text + "'," +
                " mob='" + txtMobitel.Text + "'," +
                " adresa='" + txtAdresa.Text + "'," +
                " vl='" + txtVlasnik.Text + "'," +
                " iban1='" + txtIBAN2.Text + "'," +
                " grad='" + txtGrad.Text + "'," +
                " email='" + txtEmail.Text + "'," +
                " opis_na_kraju_fakture='" + rtbKrajDokumenta.Text + "'," +
                " poslovnica_adresa='" + txtPoslovnicaAdresa.Text + "'," +
                " poslovnica_grad='" + txtGradPoslovnica.Text + "'," +
                " iban='" + txtIBAN.Text + "'," +
                " r1='" + cbR1.SelectedValue + "'" +
                " WHERE id='1'" +
                "";

            classDBlite.LiteSqlCommand(sql);
            MessageBox.Show("Spremljeno");
        }
    }
}