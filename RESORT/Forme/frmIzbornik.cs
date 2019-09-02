using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmIzbornik : Form
    {
        public frmIzbornik()
        {
            InitializeComponent();
        }

        public string broj_unosa { get; set; }
        public string godina { get; set; }

        private DataTable DTBojeForme;
        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmIzbornik_Load(object sender, EventArgs e)
        {
            SetValues();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetValues()
        {
            DataTable DT = RemoteDB.select("SELECT sobe.naziv_sobe FROM unos_gosta LEFT JOIN sobe ON sobe.id=unos_gosta.id_soba WHERE unos_gosta.broj='" + broj_unosa + "' AND godina='" + godina + "' GROUP BY sobe.naziv_sobe", "unos_gosta").Tables[0];
            string s = "";

            int i = 0;
            foreach (DataRow row in DT.Rows)
            {
                if (i == 3 || i == 6 || i == 9 || i == 12)
                {
                    s += "\r\n";
                    i++;
                }
                s += row["naziv_sobe"].ToString() + ", ";
            }

            lblNaslov.Text = "Broj unosa: " + broj_unosa + "\r\nSobe: " + s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forme.frmUpisGosta upg = new Forme.frmUpisGosta();
            upg.broj_unosa = broj_unosa;
            upg.__godina = godina;
            upg.ShowDialog();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati kompletan unos pod brojem " + broj_unosa + ".", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RemoteDB.delete("DELETE FROM unos_gosta WHERE broj='" + broj_unosa + "' AND godina='" + godina + "'");
            }

            this.Close();
        }

        private void btnIzradiRacun_Click(object sender, EventArgs e)
        {
            Forme.frmFaktura f = new frmFaktura();
            f.G_broj_unosa = null;
            f.G_broj_unosa = broj_unosa;

            f.ShowDialog();
        }
    }
}