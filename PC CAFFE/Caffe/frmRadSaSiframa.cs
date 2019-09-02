using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmRadSaSiframa : Form
    {
        public Caffe.frmCaffe MainForm { get; set; }

        public frmRadSaSiframa()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
        private Until.FunkcijeRobno RobnoFunkcije = new Until.FunkcijeRobno();

        private void frmRadSaSiframa_Load(object sender, EventArgs e)
        {
            this.BackColor = MainForm.BackColor;
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            txtKolicina.Text = "1";
            this.txtSifra.Select();
        }

        private DataTable DTroba;
        private string _sql = "";

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            if (txtSifra.Text.Length == 0)
            {
                return;
            }

            _sql = "SELECT sifra, naziv, mpc FROM roba WHERE sifra='" + txtSifra.Text + "' AND aktivnost='1'";
            DTroba = classSQL.select(_sql, "roba").Tables[0];

            if (dataG.Rows.Count > 0)
            {
                dataG.Rows.Clear();
            }

            for (int i = 0; i < DTroba.Rows.Count; i++)
            {
                dataG.Rows.Add(DTroba.Rows[i]["sifra"].ToString(), DTroba.Rows[i]["naziv"].ToString(), Convert.ToDecimal(DTroba.Rows[i]["mpc"].ToString()).ToString("#0.00"));
            }
        }

        private void txtNaziv_TextChanged(object sender, EventArgs e)
        {
            if (txtNaziv.Text.Length == 0)
            {
                return;
            }

            if (dataG.Rows.Count > 0)
            {
                dataG.Rows.Clear();
            }

            _sql = "SELECT sifra, naziv, mpc FROM roba WHERE naziv ~* '" + txtNaziv.Text + "' AND aktivnost='1'";
            DTroba = classSQL.select(_sql, "roba").Tables[0];

            for (int i = 0; i < DTroba.Rows.Count; i++)
            {
                dataG.Rows.Add(DTroba.Rows[i]["sifra"].ToString(), DTroba.Rows[i]["naziv"].ToString(), Convert.ToDecimal(DTroba.Rows[i]["mpc"].ToString()).ToString("#0.00"));
            }
        }

        private void SetRoba(string sifra, double kol)
        {
            DataTable DT = new DataTable();
            string sql = "SELECT " +
                " roba.naziv," +
                " roba.mpc," +
                " roba.nbc," +
                " roba.porez," +
                " roba.sifra," +
                " roba.porez_potrosnja" +
                " FROM roba" +
                " WHERE roba.sifra='" + sifra + "'";

            DT = classSQL.select(sql, "roba").Tables[0];

            if (DT.Rows.Count > 0)
            {
                double kolicina = kol;
                double mpc = Convert.ToDouble(DT.Rows[0]["mpc"].ToString());
                double porez = Convert.ToDouble(DT.Rows[0]["porez"].ToString());
                double pnp = Convert.ToDouble(DT.Rows[0]["porez_potrosnja"].ToString());
                double pdv_stavka;
                double Porez_potrosnja_stavka;

                double PreracunataStopaPDV = Convert.ToDouble((100 * porez) / (100 + porez + pnp));
                pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * pnp) / (100 + porez + pnp));
                Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                decimal nbc;
                decimal.TryParse(DT.Rows[0]["nbc"].ToString(), out nbc);

                MainForm.dgw.Rows.Add(
                     DT.Rows[0]["naziv"].ToString(),
                     kolicina.ToString(),
                     (mpc).ToString("#0.00"),
                     DT.Rows[0]["sifra"].ToString(),
                     MainForm.DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(),
                     DT.Rows[0]["porez"].ToString(),
                     "0",
                     mpc - (pdv_stavka + Porez_potrosnja_stavka),
                     //DT.Rows[0]["nbc"].ToString(),
                     DT.Rows[0]["porez_potrosnja"].ToString(),
                     "",
                     "",
                     nbc.ToString("#0.00000"),
                     "0",
                     ""
                     );

                MainForm.dgw.ClearSelection();
                MainForm.dgw.Rows[MainForm.dgw.Rows.Count - 1].Selected = true;
                MainForm.ProvjeraPromocije(DT.Rows[0]["sifra"].ToString(), MainForm.dgw.Rows.Count - 1);

                MainForm.IzracunUkupno();
                MainForm.PaintRows(MainForm.dgw);

                if (DSpostavke.Tables[0].Rows[0]["obavjeti_ako_nema_repromaterijala"].ToString() == "1")
                {
                    RobnoFunkcije.ObavjestiAkoNaSkladistuImaManjeOdNule(DT.Rows[0]["sifra"].ToString(), DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString());
                }
            }
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ((char)('+') == (e.KeyChar))
                    return;
            }
            if (sender.ToString()[0] == '-')
            {
                if ((char)('-') == (e.KeyChar))
                    return;
            }
            if ((char)(',') == (e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                MainForm.btnPice.Select();
                MainForm.btnPice.PerformClick();
                this.Close();
            }

            if (e.KeyData == Keys.Enter)
            {
                if (dataG.RowCount > 0)
                {
                    e.SuppressKeyPress = true;

                    Control txt = (Control)sender;
                    if (txt.Name == "txtKolicina")
                    {
                        decimal dec_parse;
                        if (!Decimal.TryParse(txtKolicina.Text, out dec_parse)) { MessageBox.Show("Krivo upisana količina.", "Greška"); return; }

                        if (dec_parse == 0)
                        {
                            MessageBox.Show("Količina ne smije biti nula!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int br = dataG.CurrentRow.Index;
                        string id = dataG.Rows[br].Cells["_sifra"].FormattedValue.ToString();
                        SetRoba(id, Convert.ToDouble(txtKolicina.Text));
                        txtSifra.Text = "";
                        txtNaziv.Text = "";
                        txtKolicina.Text = "1";
                        txtSifra.Select();
                    }
                    else
                    {
                        txtKolicina.Select();
                    }
                }
            }

            if (e.KeyData == Keys.Up)
            {
                if (dataG.RowCount > 0)
                {
                    int curent = dataG.CurrentRow.Index;
                    if (curent > 0)
                        dataG.CurrentCell = dataG.Rows[curent - 1].Cells[0];
                }
            }

            if (e.KeyData == Keys.Down)
            {
                if (dataG.RowCount > 0)
                {
                    int curent = dataG.CurrentRow.Index;
                    if (curent < dataG.RowCount - 1)
                        dataG.CurrentCell = dataG.Rows[curent + 1].Cells[0];
                }
            }

            if (e.KeyData == Keys.F5)
            {
                MainForm.btnGotovina.PerformClick();
                if (DSpostavke.Tables[0].Rows[0]["prijava_nakon_racuna"].ToString() == "1")
                {
                    this.Close();
                    MainForm.Close();
                }
            }

            if (e.KeyData == Keys.F6)
            {
                MainForm.btnDodajNaStol.PerformClick();
            }

            if (e.KeyData == Keys.F10)
            {
                Caffe.frmBackOffice bo = new frmBackOffice();
                bo.ShowDialog();
            }

            if (e.KeyData == Keys.F11)
            {
                Caffe.frmOpcije opcije = new Caffe.frmOpcije();
                opcije.FormCaffe = MainForm;
                opcije.ShowDialog();
            }

            if (e.KeyData == Keys.F12)
            {
                MainForm.btnZavrsiSmjenu.PerformClick();
            }

            if (e.KeyData == Keys.Delete)
            {
                MainForm.btnDelete.PerformClick();
            }

            if (e.KeyData == Keys.F2)
            {
                if (MainForm.dgw.RowCount > 0)
                {
                    DodatniPopustPostoci dp = new DodatniPopustPostoci();
                    dp.frm = MainForm;
                    dp.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nemate artikle");
                }
            }

            if (e.KeyData == Keys.Up)
            {
                if (MainForm.dgw.RowCount > 0)
                {
                    int curent = MainForm.dgw.CurrentRow.Index;
                    if (curent > 0)
                        MainForm.dgw.CurrentCell = MainForm.dgw.Rows[curent - 1].Cells[0];
                }
            }

            if (e.KeyData == Keys.Down)
            {
                if (MainForm.dgw.RowCount > 0)
                {
                    int curent = MainForm.dgw.CurrentRow.Index;
                    if (curent < MainForm.dgw.RowCount - 1)
                        MainForm.dgw.CurrentCell = MainForm.dgw.Rows[curent + 1].Cells[0];
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

        private void BtnZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}