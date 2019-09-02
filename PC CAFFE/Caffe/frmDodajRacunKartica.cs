using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmDodajRacunKartica : Form
    {
        private DataSet DSpostavke;
        public string karticaKupca { get; set; }
        public frmKarticaKupca frmKartica { get; set; }

        public frmDodajRacunKartica()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmDodajRacunKartica_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
        }

        private void btnZapisiRacun_Click(object sender, EventArgs e)
        {
            try
            {
                int boolInteger = 0;
                if (karticaKupca.Length == 10 && int.TryParse(karticaKupca, out boolInteger) && txtBrojRacuna.Text.Length > 0)
                {
                    string poslovnica = classSQL.select("select ime_ducana from ducan where id_ducan = '" + DSpostavke.Tables[0].Rows[0]["default_ducan"] + "'", "ducan").Tables[0].Rows[0]["ime_ducana"].ToString();
                    string naplatni_uredaj = classSQL.select("select ime_blagajne from blagajna where id_ducan = '" + DSpostavke.Tables[0].Rows[0]["default_ducan"] + "' and id_blagajna = '" + DSpostavke.Tables[0].Rows[0]["default_blagajna"] + "'", "blagajna").Tables[0].Rows[0]["ime_blagajne"].ToString();

                    if (txtBrojRacuna.Text.Contains("/") && txtBrojRacuna.Text.Split('/').Length - 1 == 2)
                    {
                        string[] racun = txtBrojRacuna.Text.Split('/');

                        if (poslovnica == racun[1] && naplatni_uredaj == racun[2])
                        {
                            DataRow dr = classSQL.select("select datum_racuna, ukupno::numeric as ukupno from racuni where id_ducan = '" + DSpostavke.Tables[0].Rows[0]["default_ducan"] + "' and id_kasa = '" + DSpostavke.Tables[0].Rows[0]["default_blagajna"] + "' and broj_racuna = '" + racun[0] + "' limit 1", "racuni").Tables[0].Rows[0];
                            DateTime dat = Convert.ToDateTime(dr["datum_racuna"]);
                            decimal izn = Convert.ToDecimal(dr["ukupno"]);

                            zapisiKarticaKupciRacuni(poslovnica, naplatni_uredaj, Convert.ToInt32(racun[0]), dat, izn, karticaKupca);
                        }
                        else
                        {
                            decimal izn = 0;
                            decimal.TryParse(txtIznosRacuna.Text, out izn);
                            zapisiKarticaKupciRacuni(racun[1], racun[2], Convert.ToInt32(racun[0]), null, izn, karticaKupca);
                        }

                        frmKartica.getData();
                    }
                    else
                    {
                        MessageBox.Show("Krivi unos računa.\nUpisani račun treba biti u formatu\n\"BROJ RAČUNA/POSLOVNICA/NAPLATNI UREĐAJ\"");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void zapisiKarticaKupciRacuni(string ducan, string nu, int broj_racuna, DateTime? dat_rac, decimal iznos, string karticaKupca)
        {
            try
            {
                string oib = classSQL.select("select oib from podaci_tvrtka limit 1", "podaci_tvrtka").Tables[0].Rows[0]["oib"].ToString();

                string sql = "select count(*) as br from karticakupci_racuni where oib = '" + oib + "' and poslovnica = '" + ducan + "' and naplatni_uredaj = '" + nu + "' and broj_racuna = '" + broj_racuna + "';";
                if (Convert.ToInt32(classSQL.select(sql, "karticakupci_racuni").Tables[0].Rows[0]["br"]) == 0)
                {
                    decimal kartica_postotak = Convert.ToDecimal(classSQL.select_settings("select kartica_popust from postavke", "postavke").Tables[0].Rows[0]["kartica_popust"]);

                    sql = "INSERT INTO karticakupci_racuni " +
                        "(oib, poslovnica, naplatni_uredaj, kartica_kupca, broj_racuna, datum_racun, iznos, bodovi) " +
                        "VALUES " +
                        "('" + oib + "', " +
                        "'" + ducan + "', " +
                        "'" + nu + "', " +
                        "'" + karticaKupca + "', " +
                        "'" + broj_racuna.ToString() + "', " +
                        "'" + (dat_rac == null ? null : dat_rac) + "', " +
                        "'" + iznos.ToString("#0.00").Replace(',', '.') + "', " +
                        "'" + (iznos * (kartica_postotak / 100)).ToString("0.00").Replace(',', '.') + "')";

                    classSQL.insert(sql);
                }
                else
                {
                    MessageBox.Show("Ovaj račun je več upisani.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBrojRacuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && txtBrojRacuna.Text.Contains('/') && txtBrojRacuna.Text.Split('/').Length - 1 == 2)
                {
                    txtIznosRacuna.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIznosRacuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && txtBrojRacuna.Text.Contains('/') && txtBrojRacuna.Text.Split('/').Length - 1 == 2)
                {
                    btnZapisiRacun_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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