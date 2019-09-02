using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmIzbornikR : Form
    {
        public frmIzbornikR()
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
            DataTable DT = RemoteDB.select("SELECT sobe.naziv_sobe FROM unos_rezervacije LEFT JOIN sobe ON sobe.id=unos_rezervacije.id_soba WHERE unos_rezervacije.broj='" + broj_unosa + "' AND unos_rezervacije.godina='" + godina + "' GROUP BY sobe.naziv_sobe", "unos_rezervacije").Tables[0];
            string s = "";
            int i = 0;
            foreach (DataRow row in DT.Rows)
            {
                if (i == 3 || i == 6 || i == 9 || i == 12)
                {
                    s += "\r\n";
                }
                s += row["naziv_sobe"].ToString() + ", ";
                i++;
            }

            lblNaslov.Text = "Broj unosa: " + broj_unosa + "\r\nSobe: " + s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forme.frmUnosRezervacije upg = new Forme.frmUnosRezervacije();
            upg.broj_unosa = broj_unosa;
            upg.__godina = godina;
            upg.ShowDialog();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati kompletan unos rezervacije pod brojem " + broj_unosa + ".", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RemoteDB.delete("DELETE FROM unos_rezervacije WHERE broj='" + broj_unosa + "' AND godina='" + godina + "'");
            }

            this.Close();
        }

        private void btnIzradiRacun_Click(object sender, EventArgs e)
        {
            DataTable DT = RemoteDB.select("SELECT sobe.naziv_sobe," +
                "unos_rezervacije.id_vrsta_usluge," +
                "unos_rezervacije.ime_gosta," +
                "unos_rezervacije.id_vrsta_usluge," +
                "unos_rezervacije.broj_putovnice," +
                "unos_rezervacije.broj_osobne," +
                "unos_rezervacije.datum_dolaska," +
                "unos_rezervacije.id_drzava," +
                "unos_rezervacije.datum_odlaska FROM unos_rezervacije " +
                "LEFT JOIN sobe ON sobe.id=unos_rezervacije.id_soba WHERE unos_rezervacije.broj='" + broj_unosa + "'  AND godina='" + godina + "'", "unos_rezervacije").Tables[0];

            Forme.frmUpisGosta ug = new frmUpisGosta();
            ug.__soba = DT.Rows[0]["naziv_sobe"].ToString();
            ug.__OD_datuma = DT.Rows[0]["datum_dolaska"].ToString();
            ug.__DO_datuma = DT.Rows[0]["datum_odlaska"].ToString();
            ug.__drzava = DT.Rows[0]["id_drzava"].ToString();
            ug.__ime_gosta = DT.Rows[0]["ime_gosta"].ToString();
            ug.__drzava = DT.Rows[0]["id_drzava"].ToString();
            ug.__vrsta_usluge = DT.Rows[0]["id_vrsta_usluge"].ToString();
            ug.__broj_osobne = DT.Rows[0]["broj_osobne"].ToString();
            ug.__broj_putovnice = DT.Rows[0]["broj_putovnice"].ToString();
            ug.__godina = godina;
            ug.ShowDialog();

            //if(MessageBox.Show("Iz rezervacije ste unijeli gosta u unos gosta (Check in). Želite li izbrisati rezervaciju?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                // RemoteDB.delete("DELETE FROM unos_rezervacije WHERE broj='"+broj_unosa+"'");
            }
        }

        private void btnIzradiRaccun_Click(object sender, EventArgs e)
        {
            Forme.frmOdaberiSobuZaNaplatiti sn = new frmOdaberiSobuZaNaplatiti();
            sn.broj_unosa = broj_unosa;
            sn.godina = godina;
            sn.ShowDialog();
        }
    }
}