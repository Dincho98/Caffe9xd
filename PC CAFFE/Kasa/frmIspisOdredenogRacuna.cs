using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmIspisOdredenogRacuna : Form
    {
        public frmIspisOdredenogRacuna()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmIspisOdredenogRacuna_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private DataTable DTsettings = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTsend;
        private string sifra = "";
        private string barcode = "";

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
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DataRow row;

            string sql = "SELECT racun_stavke.vpc,racun_stavke.sifra_robe,racun_stavke.porez_potrosnja,roba.naziv,racun_stavke.id_skladiste," +
                "racun_stavke.mpc,racun_stavke.porez,racun_stavke.kolicina,racun_stavke.rabat FROM racun_stavke " +
                    " INNER JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                    " WHERE racun_stavke.broj_racuna='" + textBox1.Text + "'" +
                    " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "' " +
                    " AND id_blagajna='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    " AND godina='2019'";

            DataTable DT = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + textBox1.Text + "'  AND racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];

            if (DT.Rows.Count == 0)
            {
                MessageBox.Show("Krivi unos.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable DTitems = classSQL.select(sql, "racun_stavke").Tables[0];
            DataTable DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            string imeBlagajnika = classSQL.select("SELECT ime + ' ' + prezime AS name FROM zaposlenici WHERE id_zaposlenik='" + DT.Rows[0]["id_blagajnik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            for (int i = 0; i < DTitems.Rows.Count; i++)
            {
                sifra = DTitems.Rows[i]["sifra_robe"].ToString();
                row = DTsend.NewRow();
                row["broj_racuna"] = textBox1.Text;
                row["sifra_robe"] = DTitems.Rows[i]["sifra_robe"].ToString();
                row["id_skladiste"] = DTitems.Rows[i]["id_skladiste"].ToString();
                row["mpc"] = DTitems.Rows[i]["mpc"].ToString();
                row["porez"] = DTitems.Rows[i]["porez"].ToString();
                row["kolicina"] = DTitems.Rows[i]["kolicina"].ToString();
                row["rabat"] = DTitems.Rows[i]["rabat"].ToString();
                row["cijena"] = DTitems.Rows[i]["mpc"].ToString();
                row["ime"] = DTitems.Rows[i]["naziv"].ToString();
                row["vpc"] = DTitems.Rows[i]["vpc"].ToString();
                row["porez_potrosnja"] = DTitems.Rows[i]["porez_potrosnja"].ToString();
                DTsend.Rows.Add(row);
            }

            classSQL.update("UPDATE racuni SET broj_ispisa=broj_ispisa+1 WHERE broj_racuna='" + textBox1.Text + "'" +
                    " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "' " +
                    " AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "'");

            barcode = "000" + textBox1.Text;
            if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() != "0")
            {
                PosPrint.classPosPrintCaffeNaknadno.PrintReceipt(DTsend, imeBlagajnika, textBox1.Text + "/" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString(), DT.Rows[0]["id_kupac"].ToString(), barcode, textBox1.Text, "G", "");

                this.Close();
            }
            else
            {
                if (DTsend.Rows.Count > 0)
                {
                    Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                    rfak.dokumenat = "RAC";
                    rfak.ImeForme = "Račun";
                    rfak.broj_dokumenta = textBox1.Text;
                    rfak.godina = DateTime.Now.Year.ToString();
                    rfak.id_kasa = DTsettings.Rows[0]["default_ducan"].ToString();
                    rfak.id_poslovnica = DTsettings.Rows[0]["default_blagajna"].ToString();
                    this.Hide();
                    rfak.ShowDialog();
                    this.Close();
                }
            }
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