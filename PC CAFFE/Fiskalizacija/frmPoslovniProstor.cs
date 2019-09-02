using Raverus.FiskalizacijaDEV;
using Raverus.FiskalizacijaDEV.PopratneFunkcije;
using Raverus.FiskalizacijaDEV.Schema;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS.Fiskalizacija
{
    public partial class frmPoslovniProstor : Form
    {
        public frmPoslovniProstor()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPoslovniProstor_Load(object sender, EventArgs e)
        {
            Fillpos_prostor();
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];

        private void Fillpos_prostor()
        {
            DataTable DT = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
            txtOznakaPP.Text = DT.Rows[0]["ime_ducana"].ToString();

            DataTable DTp = classSQL.select("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
            txtOIB.Text = DTp.Rows[0]["oib"].ToString();

            DataTable DTf = classSQL.select_settings("SELECT * FROM podaci_poslovnica_fiskal", "podaci_poslovnica_fiskal").Tables[0];

            if (DTf.Rows.Count > 0)
            {
                txtOIB.Text = DTf.Rows[0]["OIB"].ToString();
                //txtOznakaPP.Text = DTf.Rows[0]["oznakaPP"].ToString();
                txtUlica.Text = DTf.Rows[0]["ulica"].ToString();
                txtKucniBroj.Text = DTf.Rows[0]["broj"].ToString();
                txtKucniDodatak.Text = DTf.Rows[0]["broj_dodatak"].ToString();
                txtBrojPoste.Text = DTf.Rows[0]["posta"].ToString();
                txtNaselje.Text = DTf.Rows[0]["naselje"].ToString();
                txtOpcina.Text = DTf.Rows[0]["opcina"].ToString();
                try
                {
                    dtpDate.Value = Convert.ToDateTime(DTf.Rows[0]["datum"].ToString());
                }
                catch (Exception)
                {
                }
                txtRadnoVrijeme.Text = DTf.Rows[0]["r_vrijeme"].ToString();

                if (DTf.Rows[0]["zatvaranje"].ToString() == "1")
                {
                    chbZatvaranje.Checked = true;
                }
            }
        }

        //void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void btnPosaljiPodatke_Click(object sender, EventArgs e)
        {
            PosaljiPodatke();
        }

        private void PosaljiPodatke()
        {
            if (txtOIB.Text == "") { MessageBox.Show("Krivo upisani oib."); return; }
            if (txtOznakaPP.Text == "") { MessageBox.Show("Krivo upisana oznaka PP."); return; }
            if (txtUlica.Text == "") { MessageBox.Show("Krivo upisana ulica."); return; }
            if (txtKucniBroj.Text == "") { MessageBox.Show("Krivo upisani kučni broj."); return; }
            if (txtBrojPoste.Text == "") { MessageBox.Show("Krivo upisani broj pošte."); return; }
            if (txtNaselje.Text == "") { MessageBox.Show("Krivo upisani oib."); return; }
            if (txtOpcina.Text == "") { MessageBox.Show("Krivo upisana opčina."); return; }
            if (txtRadnoVrijeme.Text == "") { MessageBox.Show("Krivo upisano radno vrijeme."); return; }

            if (txtOznakaPP.Text.Length > 19)
            {
                MessageBox.Show("Previše znamanka imate u poslovnom prostoru");
            }

            Raverus.FiskalizacijaDEV.Schema.ZaglavljeType zaglavlje = new Raverus.FiskalizacijaDEV.Schema.ZaglavljeType()
            {
                DatumVrijeme = Razno.DohvatiFormatiranoTrenutnoDatumVrijeme(),
                IdPoruke = Guid.NewGuid().ToString()
            };

            X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());

            try
            {
                Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();
                if (Class.Postavke.TEST_FISKALIZACIJA)
                {
                    cis.CisUrl = "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest";
                }
                else
                {
                    cis.CisUrl = "https://cis.porezna-uprava.hr:8449/FiskalizacijaService";
                }
                cis.TimeOut = 10000;

                //47165970760

                Raverus.FiskalizacijaDEV.Schema.PoslovniProstorType poslovniProstor = new Raverus.FiskalizacijaDEV.Schema.PoslovniProstorType();
                poslovniProstor.Oib = txtOIB.Text;
                poslovniProstor.OznPoslProstora = txtOznakaPP.Text;

                Raverus.FiskalizacijaDEV.Schema.AdresaType adresa = new Raverus.FiskalizacijaDEV.Schema.AdresaType();
                adresa.Ulica = txtUlica.Text;
                adresa.KucniBroj = txtKucniBroj.Text;
                if (txtKucniDodatak.Text == "")
                {
                    adresa.KucniBrojDodatak = txtKucniDodatak.Text;
                }
                adresa.BrojPoste = txtBrojPoste.Text;
                adresa.Naselje = txtNaselje.Text;
                adresa.Opcina = txtOpcina.Text;
                Raverus.FiskalizacijaDEV.Schema.AdresniPodatakType adresniPodatak = new Raverus.FiskalizacijaDEV.Schema.AdresniPodatakType();
                adresniPodatak.Item = adresa;
                poslovniProstor.AdresniPodatak = adresniPodatak;

                if (chbZatvaranje.Checked)
                {
                    poslovniProstor.OznakaZatvaranjaSpecified = true;
                    poslovniProstor.OznakaZatvaranja = OznakaZatvaranjaType.Z;
                }

                poslovniProstor.RadnoVrijeme = txtRadnoVrijeme.Text;
                poslovniProstor.DatumPocetkaPrimjene = Raverus.FiskalizacijaDEV.PopratneFunkcije.Razno.FormatirajDatum(dtpDate.Value);
                poslovniProstor.SpecNamj = Class.Postavke.OIB_PC1;

                XmlDocument doc = cis.PosaljiPoslovniProstor(poslovniProstor, certifikat);

                //MessageBox.Show(doc.InnerXml.Replace("\'", ""));

                if (cis.OdgovorGreska != null)
                {
                    MessageBox.Show("Greška kod slanja zahtjeva");
                }
                else
                {
                    string zatvaranje = "0";
                    if (chbZatvaranje.Checked)
                    {
                        zatvaranje = "1";
                    }

                    string sql = "UPDATE podaci_poslovnica_fiskal SET " +
                        " OIB='" + txtOIB.Text + "'," +
                        " oznakaPP='" + txtOznakaPP.Text + "'," +
                        " ulica='" + txtUlica.Text + "'," +
                        " broj='" + txtKucniBroj.Text + "'," +
                        " broj_dodatak='" + txtKucniDodatak.Text + "'," +
                        " posta='" + txtBrojPoste.Text + "'," +
                        " naselje='" + txtNaselje.Text + "'," +
                        " opcina='" + txtOpcina.Text + "'," +
                        " datum='" + dtpDate.Value.ToString() + "'," +
                        " r_vrijeme='" + txtRadnoVrijeme.Text + "'," +
                        " zatvaranje='" + zatvaranje + "'" +
                        "";
                    classSQL.Setings_Update(sql);

                    MessageBox.Show("Zahtjev uspješno poslan.");
                }
            }
            catch (Exception ex)
            {
                string sql = "UPDATE podaci_poslovnica_fiskal SET " +
                    " OIB='" + txtOIB.Text + "'," +
                    " oznakaPP='" + txtOznakaPP.Text + "'," +
                    " ulica='" + txtUlica.Text + "'," +
                    " broj='" + txtKucniBroj.Text + "'," +
                    " broj_dodatak='" + txtKucniDodatak.Text + "'," +
                    " posta='" + txtBrojPoste.Text + "'," +
                    " naselje='" + txtNaselje.Text + "'," +
                    " opcina='" + txtOpcina.Text + "'," +
                    " datum='" + dtpDate.Value.ToString() + "'," +
                    " r_vrijeme='" + txtRadnoVrijeme.Text + "'," +
                    " zatvaranje='0'" +
                    "";
                classSQL.Setings_Update(sql);

                MessageBox.Show("Greška kod slanja zahtjeva.\r\n\r\n" + ex.ToString());
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