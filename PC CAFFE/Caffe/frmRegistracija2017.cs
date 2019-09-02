using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmRegistracija2017 : Form
    {
        public frmRegistracija2017()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private bool registrirano = false;
        public frmMenu MainForm { get; set; }
        public string productKey { get; set; }
        public int broj { get; set; }

        private void frmRegistracija_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
                txtCode.Text = productKey;
            }
            catch (Exception)
            {
            }
        }

        //2016-08-08
        // dejan bijela pizza capriciosa velka
        // petar SLAVONSKA velka
        // marko DIAVOLLO velka

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtGenerirano.Text != "")
            {
                string s = Class.Registracija.GetMD5(productKey + "5AR").ToUpper();
                string ns = s.Substring(0, 6) + "-" + s.Substring(6, 6) + "-" + s.Substring(12, 6) + "-" + s.Substring(18, 6) + "-" + s.Substring(24, 6);
                string[] ss = ns.Split('-');
                string generirano = ss[1] + "-" + ss[3] + "-" + ss[0] + "-" + ss[2] + "-" + ss[4];

                if (generirano == txtGenerirano.Text)
                {
                    //if (MessageBox.Show(this, "Sinkronizacija podataka\nOdaberite \"Da\" za preuzimanje podataka sa weba\nOdaberite\"Ne\" za slanje podataka na web", "Registracija", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes) {
                    //    MessageBox.Show("Podaci su preuzeti!\nProgram je uspješno registriran.");
                    //} else {
                    using (var ws = new wsSoftKontrol.wsSoftKontrol())
                    {
                        bool rez = ws.Registracija(productKey, generirano, Util.Korisno.oibTvrtke, Class.PodaciTvrtka.nazivPoslovnice, Class.PodaciTvrtka.gradPoslovnicaId, Class.PodaciTvrtka.adresaPoslovnice, Util.Korisno.nazivDucan, Convert.ToInt32(Util.Korisno.nazivBlagajna));
                        if (rez)
                        {
                            string sql = @"INSERT INTO registracija (productKey, activationCode, broj)
VALUES (
'" + productKey + @"',
'" + generirano + @"',
'" + broj + @"'
);";

                            if (Class.Registracija.productKey.Length > 0)
                            {
                                sql = "update registracija set productKey = '" + productKey + @"', activationCode = '" + generirano + @"', broj = '" + broj + "'";
                            }

                            classSQL.Setings_Update(sql);
                            Class.Registracija.getPodaci();
                            MessageBox.Show("Podaci su poslani!\nProgram je uspješno registriran.");

                            //} else {
                            //}
                        }
                    }
                    registrirano = true;
                    //this.Close();
                    //classSQL.Setings_Update("UPDATE postavke SET aktivnost='1'");
                    //File.WriteAllText("code", generirano);
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Krivo upisani podaci.");
                }
            }
        }

        private void frmRegistracija_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (registrirano == false)
            {
                Application.Exit();
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