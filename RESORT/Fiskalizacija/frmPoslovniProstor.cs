using Raverus.FiskalizacijaDEV;
using Raverus.FiskalizacijaDEV.PopratneFunkcije;
using Raverus.FiskalizacijaDEV.Schema;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace RESORT.Fiskalizacija
{
    public partial class frmPoslovniProstor : Form
    {
        public frmPoslovniProstor()
        {
            InitializeComponent();
        }

        private void frmPoslovniProstor_Load(object sender, EventArgs e)
        {
            Fillpos_prostor();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private static DataTable DTf = classDBlite.LiteSelect("SELECT * FROM podaci_fiskalizacija", "podaci_fiskalizacija").Tables[0];

        private void Fillpos_prostor()
        {
            DataTable DT = RemoteDB.select("SELECT * FROM ducan", "ducan").Tables[0];
            txtOznakaPP.Text = DT.Rows[0]["ime_ducana"].ToString();

            DataTable DTp = classDBlite.LiteSelect("SELECT * FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];
            txtOIB.Text = DTp.Rows[0]["oib"].ToString();

            if (DTf.Rows.Count > 0)
            {
                txtOIB.Text = DTf.Rows[0]["oib"].ToString();
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

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

            ZaglavljeType zaglavlje = new ZaglavljeType()
            {
                DatumVrijeme = Razno.DohvatiFormatiranoTrenutnoDatumVrijeme(),
                IdPoruke = Guid.NewGuid().ToString()
            };

            X509Certificate2 certifikat = Potpisivanje.DohvatiCertifikat(DTf.Rows[0]["naziv_certifikata"].ToString());

            try
            {
                CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

                if (DTf.Rows[0]["test_Yes"].ToString() == "1")
                {
                    cis.CisUrl = "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest";
                }
                else
                {
                    cis.CisUrl = "https://cis.porezna-uprava.hr:8449/FiskalizacijaService";
                }
                cis.TimeOut = 10000;

                PoslovniProstorType poslovniProstor = new PoslovniProstorType();
                poslovniProstor.Oib = txtOIB.Text;
                poslovniProstor.OznPoslProstora = txtOznakaPP.Text;

                AdresaType adresa = new AdresaType();
                adresa.Ulica = txtUlica.Text;
                adresa.KucniBroj = txtKucniBroj.Text;
                if (txtKucniDodatak.Text != "")
                {
                    adresa.KucniBrojDodatak = txtKucniDodatak.Text;
                }
                adresa.BrojPoste = txtBrojPoste.Text;
                adresa.Naselje = txtNaselje.Text;
                adresa.Opcina = txtOpcina.Text;
                AdresniPodatakType adresniPodatak = new AdresniPodatakType();
                adresniPodatak.Item = adresa;
                poslovniProstor.AdresniPodatak = adresniPodatak;

                if (chbZatvaranje.Checked)
                {
                    poslovniProstor.OznakaZatvaranjaSpecified = true;
                    poslovniProstor.OznakaZatvaranja = OznakaZatvaranjaType.Z;
                }

                poslovniProstor.RadnoVrijeme = txtRadnoVrijeme.Text;
                poslovniProstor.DatumPocetkaPrimjene = Razno.FormatirajDatum(dtpDate.Value);
                poslovniProstor.SpecNamj = "47165970760";

                XmlDocument doc = cis.PosaljiPoslovniProstor(poslovniProstor, certifikat);

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

                    string sql = string.Format(@"UPDATE podaci_fiskalizacija
SET
    oib = '{0}',
    ulica = '{1}',
    broj = '{2}',
    broj_dodatak = '{3}',
    posta = '{4}',
    naselje = '{5}',
    opcina = '{6}',
    datum = '{7}',
    r_vrijeme = '{8}',
    zatvaranje = '{9}';",
                    txtOIB.Text,
                    txtUlica.Text,
                    txtKucniBroj.Text,
                    txtKucniDodatak.Text,
                    txtBrojPoste.Text,
                    txtNaselje.Text,
                    txtOpcina.Text,
                    dtpDate.Value.ToString("yyyy-MM-dd H:mm:ss"),
                    txtRadnoVrijeme.Text,
                    zatvaranje);

                    classDBlite.LiteSqlCommand(sql);

                    MessageBox.Show("Zahtjev uspješno poslan.");
                }
            }
            catch (Exception ex)
            {
                string sql = string.Format(@"UPDATE podaci_fiskalizacija
SET
    OIB = '{0}',
    oznakaPP = '{1}',
    ulica = '{2}',
    broj = '{3}',
    broj_dodatak = '{4}',
    posta = '{5}',
    naselje = '{6}',
    opcina = '{7}',
    datum = '{8}',
    r_vrijeme = '{9}',
    zatvaranje = '0';",
                    txtOIB.Text,
                    txtOznakaPP.Text,
                    txtUlica.Text,
                    txtKucniBroj.Text,
                    txtKucniDodatak.Text,
                    txtBrojPoste.Text,
                    txtNaselje.Text,
                    txtOpcina.Text,
                    dtpDate.Value.ToString("yyyy-MM-dd H:mm:ss"),
                    txtRadnoVrijeme.Text);

                classDBlite.LiteSqlCommand(sql);
                MessageBox.Show("Greška kod slanja zahtjeva.\r\n\r\n" + ex.ToString());
            }
        }
    }
}