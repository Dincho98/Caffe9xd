using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Fiskalizacija
{
    public partial class frmPodaciFiskalizacija : Form
    {
        public frmPodaciFiskalizacija()
        {
            InitializeComponent();
        }

        private void frmPodaciFiskalizacija_Load(object sender, EventArgs e)
        {
            Fill();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string aktivnost = "0";

            if (chbAktivnost.Checked) { aktivnost = "1"; }

            string sql = string.Format(@"UPDATE podaci_fiskalizacija
SET
    oib = '{0}',
    oznakaPP = '{1}',
    oznaka_prodajnog_mj = '{2}',
    aktivnost = '{3}',
    naziv_certifikata = '{4}',
    oznaka_prodajnog_mj_avans = '{5}',
    oznaka_slijednosti = '{6}',
    sustav_pdv = '{7}',
    test_Yes = '{8}';",
                txtOIB.Text,
                cbPoslovniProstor.SelectedValue,
                cbOznakaBlagajna.SelectedValue,
                aktivnost,
                txtNazivCertifikata.Text,
                chbONPavans.SelectedValue,
                cbOznakaSlijednosti.SelectedValue,
                cbSustavPDV.SelectedValue,
                cbFazaFiskalizacije.SelectedValue);

            classDBlite.LiteSqlCommand(sql);
        }

        private void Fill()
        {
            DataTable DT = classDBlite.LiteSelect("SELECT * FROM podaci_fiskalizacija", "podaci_fiskalizacija").Tables[0];
            if (DT.Rows[0]["aktivnost"].ToString() == "1")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }

            //fill Ducan
            DataTable DTducan = RemoteDB.select("SELECT * FROM ducan", "ducan").Tables[0];
            cbPoslovniProstor.DataSource = DTducan;
            cbPoslovniProstor.DisplayMember = "ime_ducana";
            cbPoslovniProstor.ValueMember = "id_ducan";

            //fill blagajna
            DataTable DTblagajna = RemoteDB.select("SELECT * FROM blagajna", "blagajna").Tables[0];
            cbOznakaBlagajna.DataSource = DTblagajna;
            cbOznakaBlagajna.DisplayMember = "ime_blagajne";
            cbOznakaBlagajna.ValueMember = "id_blagajna";

            //fill avans
            DataTable DTblagajnaA = RemoteDB.select("SELECT * FROM blagajna", "blagajna").Tables[0];
            chbONPavans.DataSource = DTblagajnaA;
            chbONPavans.DisplayMember = "ime_blagajne";
            chbONPavans.ValueMember = "id_blagajna";

            DataTable D = new DataTable("pdv");
            D.Columns.Add("id", typeof(string));
            D.Columns.Add("naziv", typeof(string));
            D.Rows.Add("NE", "Tvrtka nije u sustavu PDV-a");
            D.Rows.Add("DA", "Tvrtka je u sustavu PDV-a");
            cbSustavPDV.DataSource = D;
            cbSustavPDV.DisplayMember = "naziv";
            cbSustavPDV.ValueMember = "id";

            DataTable OS = new DataTable("OznakaFiskal");
            OS.Columns.Add("id", typeof(string));
            OS.Columns.Add("naziv", typeof(string));
            OS.Rows.Add("P", "P - na nivou poslovnog prostora");
            OS.Rows.Add("N", "N - na nivou naplatnog uređaja");
            cbOznakaSlijednosti.DataSource = OS;
            cbOznakaSlijednosti.DisplayMember = "naziv";
            cbOznakaSlijednosti.ValueMember = "id";

            DataTable DTtest = new DataTable("Test");
            DTtest.Columns.Add("id", typeof(string));
            DTtest.Columns.Add("naziv", typeof(string));
            DTtest.Rows.Add("0", "Produkcijsko okruženje");
            DTtest.Rows.Add("1", "Testno okruženje");
            cbFazaFiskalizacije.DataSource = DTtest;
            cbFazaFiskalizacije.DisplayMember = "naziv";
            cbFazaFiskalizacije.ValueMember = "id";

            cbSustavPDV.SelectedValue = DT.Rows[0]["sustav_pdv"].ToString();
            cbFazaFiskalizacije.SelectedValue = DT.Rows[0]["test_Yes"].ToString();
            cbPoslovniProstor.SelectedValue = DT.Rows[0]["oznakaPP"].ToString();
            cbOznakaBlagajna.Text = DT.Rows[0]["oznaka_prodajnog_mj"].ToString();
            txtNazivCertifikata.Text = DT.Rows[0]["naziv_certifikata"].ToString();
            cbOznakaSlijednosti.SelectedValue = DT.Rows[0]["oznaka_slijednosti"].ToString();
            chbONPavans.SelectedValue = DT.Rows[0]["oznaka_prodajnog_mj_avans"].ToString();
            txtOIB.Text = DT.Rows[0]["oib"].ToString();
        }
    }
}