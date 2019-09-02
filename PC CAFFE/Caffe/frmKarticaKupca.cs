using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmKarticaKupca : Form
    {
        private DataSet DSpostavke;

        public frmKarticaKupca()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
            lblAdresa.Text = "";
            lblKarticaKupca.Text = "";
            lblNaziv.Text = "";
            lblUkupno.Text = "";
        }

        private void frmKarticaKupca_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            dtpOd.Value = dtpDo.Value.AddMonths(-1);
            tmTimer1.Start();
        }

        private void tmTimer1_Tick(object sender, EventArgs e)
        {
            txtKarticaKupca.Focus();
        }

        private void txtKarticaKupca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string prefix = DSpostavke.Tables[0].Rows[0]["reader_prefix"].ToString();
                string sufix = DSpostavke.Tables[0].Rows[0]["reader_sufix"].ToString();
                string kk = "";
                if (e.KeyChar == (char)Keys.Enter && Util.Korisno.regKartica(prefix, sufix, txtKarticaKupca.Text, ref kk))
                {
                    lblKarticaKupca.Text = kk;

                    getData();

                    txtKarticaKupca.Text = "";
                }
                else
                {
                    lblNaziv.Text = "";
                    lblAdresa.Text = "";
                    lblKarticaKupca.Text = "";
                    lblUkupno.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getData()
        {
            if (lblKarticaKupca.Text.Length > 0)
            {
                string sql = "", sql2 = "";
                DataRow drUser;
                sql2 = "SELECT SUM(bodovi) AS ukp FROM karticakupci_racuni WHERE kartica_kupca = '" + lblKarticaKupca.Text + "';";
                if (chkAll.Checked)
                {
                    sql = "SELECT ime_tvrtke, adresa, kartica_kupca, (SELECT sum(bodovi) as ukupnoBodovi from karticakupci_racuni where kartica_kupca = '" + lblKarticaKupca.Text + "') as ukupnoBodovi, kartica_kupca from partners where kartica_kupca = '" + lblKarticaKupca.Text + "';";
                    //sql2 = "SELECT SUM(bodovi) AS ukp WHERE kartica_kupca = '" + lblKarticaKupca.Text + "';";
                }
                else
                {
                    sql = "SELECT ime_tvrtke, adresa, kartica_kupca, (SELECT sum(bodovi) as ukupnoBodovi from karticakupci_racuni where kartica_kupca = '" + lblKarticaKupca.Text + "' AND date(datum_racun) between  '" + dtpOd.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpDo.Value.ToString("yyyy-MM-dd") + "') as ukupnoBodovi, kartica_kupca from partners where kartica_kupca = '" + lblKarticaKupca.Text + "';";
                }
                DataTable dtUkupnoBodovi = classSQL.select(sql2, "karticakupci_racuni").Tables[0];
                lblUkupnoBodova.Text = "Ukupno bodova: ";
                if (dtUkupnoBodovi != null && dtUkupnoBodovi.Rows.Count > 0 && dtUkupnoBodovi.Rows[0][0].ToString().Length > 0)
                {
                    lblUkupnoBodova.Text = "Ukupno bodova: " + dtUkupnoBodovi.Rows[0][0].ToString();
                }

                DataTable dtData = classSQL.select(sql, "partners").Tables[0];

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    drUser = dtData.Rows[0];
                    lblNaziv.Text = drUser["ime_tvrtke"].ToString();
                    lblAdresa.Text = drUser["adresa"].ToString();
                    decimal ukpbod = 0;
                    decimal.TryParse(drUser["ukupnoBodovi"].ToString(), out ukpbod);
                    lblUkupno.Text = "Bodovi: " + ukpbod.ToString("0.00");
                }
                else
                {
                    lblNaziv.Text = "";
                    lblAdresa.Text = "";
                    lblUkupno.Text = "";
                }

                if (chkAll.Checked)
                {
                    sql = "SELECT ROW_NUMBER() OVER(ORDER BY kkr.datum_racun) AS rb, kkr.poslovnica, kkr.naplatni_uredaj, kkr.broj_racuna, kkr.datum_racun, kkr.iznos, kkr.bodovi FROM karticakupci_racuni kkr WHERE kkr.kartica_kupca = '" + lblKarticaKupca.Text + "' ORDER BY kkr.datum_racun;";
                }
                else
                {
                    sql = "SELECT ROW_NUMBER() OVER(ORDER BY kkr.datum_racun) AS rb, kkr.poslovnica, kkr.naplatni_uredaj, kkr.broj_racuna, kkr.datum_racun, kkr.iznos, kkr.bodovi FROM karticakupci_racuni kkr WHERE kkr.kartica_kupca = '" + lblKarticaKupca.Text + "' " +
                        " AND date(kkr.datum_racun) between  '" + dtpOd.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpDo.Value.ToString("yyyy-MM-dd") + "'" +
                        " ORDER BY kkr.datum_racun;";
                }

                dtData = classSQL.select(sql, "karticakupci_racuni").Tables[0];

                dgvData.DataSource = dtData;
            }
            if (!tmTimer1.Enabled)
            {
                tmTimer1.Enabled = true;
                tmTimer1.Start();
            }
        }

        private void dtpOd_ValueChanged(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            getData();
        }

        private void dtpDo_ValueChanged(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            getData();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpOd.Enabled = !chkAll.Checked;
            dtpDo.Enabled = !chkAll.Checked;

            getData();
        }

        private void dtpOd_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void dtpDo_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void chkAll_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void btnDodajRacune_Click(object sender, EventArgs e)
        {
            try
            {
                int boolInteger = 0;
                if (lblKarticaKupca.Text.Length == 10 && int.TryParse(lblKarticaKupca.Text, out boolInteger))
                {
                    frmDodajRacunKartica frm = new frmDodajRacunKartica();
                    frm.karticaKupca = lblKarticaKupca.Text;
                    frm.frmKartica = (frmKarticaKupca)this.FindForm();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kartica nije odabrana.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tmTimer1.Enabled = true;
            tmTimer1.Start();
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