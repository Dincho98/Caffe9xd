using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasaOpcije : Form
    {
        public frmKasaOpcije()
        {
            InitializeComponent();
        }

        private DataTable DTsetting;
        private DataTable DTsend;

        private void frmKasaOpcije_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private string barcode = "";
        private string sifra = "";

        private void button1_Click(object sender, EventArgs e)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("vpc");
            DataRow row;

            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni  WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            DataTable DSkupac = classSQL.select("SELECT id_kupac FROM racuni WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "' ", "racuni").Tables[0];

            string sql = "SELECT racun_stavke.sifra_robe,roba.naziv,racun_stavke.vpc,racun_stavke.porez_potrosnja,racun_stavke.id_skladiste," +
                " racun_stavke.mpc,racun_stavke.porez,racun_stavke.kolicina,racun_stavke.rabat,roba.oduzmi,roba.mpc AS cijena FROM racun_stavke " +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE racun_stavke.broj_racuna='" + DSbr.Rows[0][0].ToString() + "' " +
                " AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + "' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + "'";
            DataTable DTrac = classSQL.select(sql, "racun_stavke").Tables[0];

            for (int i = 0; i < DTrac.Rows.Count; i++)
            {
                sifra = DTrac.Rows[i]["sifra_robe"].ToString();
                row = DTsend.NewRow();
                row["ime"] = DTrac.Rows[i]["naziv"].ToString();
                row["porez"] = DTrac.Rows[i]["porez"].ToString();
                row["mpc"] = DTrac.Rows[i]["mpc"].ToString();
                row["kolicina"] = DTrac.Rows[i]["kolicina"].ToString();
                row["rabat"] = DTrac.Rows[i]["rabat"].ToString();
                row["cijena"] = DTrac.Rows[i]["mpc"].ToString();
                row["vpc"] = DTrac.Rows[i]["vpc"].ToString();
                row["porez_potrosnja"] = DTrac.Rows[i]["porez_potrosnja"].ToString();
                DTsend.Rows.Add(row);
                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            DTsetting = classSQL.select("SELECT * FROM pos_print", "pos_print").Tables[0];
            string blagajnik = classSQL.select("SELECT ime + ' ' + prezime AS name FROM zaposlenici", "zaposlenici").Tables[0].Rows[0]["name"].ToString();

            barcode = "000" + DSbr.Rows[0][0].ToString();
            PosPrint.classPosPrintMaloprodaja.PrintReceipt(DTsend, blagajnik, DSbr.Rows[0][0].ToString() + "/" + DateTime.Now.Year.ToString(), DSkupac.Rows[0][0].ToString(), barcode, DSbr.Rows[0][0].ToString(), "G");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kasa.frmIspisOdredenogRacuna odd = new Kasa.frmIspisOdredenogRacuna();
            odd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kasa.frmStornoRacuna sr = new Kasa.frmStornoRacuna();
            sr.ShowDialog();
        }

        private void btnStornoZadnjegR_Click(object sender, EventArgs e)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];

            if (DSbr.Rows.Count != 0)
            {
                classSQL.update("UPDATE racuni SET storno='DA' WHERE broj_racuna='" + DSbr.Rows[0][0].ToString() + "' AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'");
            }

            MessageBox.Show("Izvršeno");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kasa.frmDodajPartnera dp = new Kasa.frmDodajPartnera();
            dp.ShowDialog();
        }

        private void btnPromjenaPlacanja_Click(object sender, EventArgs e)
        {
            Kasa.frmPromjenaNacinaPlacanja pnp = new Kasa.frmPromjenaNacinaPlacanja();
            pnp.ShowDialog();
        }
    }
}