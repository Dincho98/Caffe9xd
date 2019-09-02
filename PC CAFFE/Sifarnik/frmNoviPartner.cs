using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNoviPartner : Form
    {
        private DataSet DSdjelatnosti = new DataSet();
        private DataSet DSdrazava = new DataSet();
        private DataSet DSgrad = new DataSet();
        private bool edit = false;

        public frmNoviPartner()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmMenu MainFormMenu { get; set; }

        private void frmNoviPartner_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            button1.Enabled = false;
            txtNaziv.Select();
            ControlDisableEnable(1, 0, 0, 1);
            txtSifra.Enabled = true;
            txtSifra.Select();
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            enable(true);
            cbFill();
            button1.Enabled = true;
            txtSifra.Text = brojPartner();
            txtNaziv.Select();
            edit = false;
            cbDrzava.SelectedValue = "60";
            cbGrad.SelectedValue = "1";
            //cbPPT.SelectedValue = "";
            ControlDisableEnable(0, 1, 1, 0);
        }

        private string brojPartner()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void enable(bool x)
        {
            if (x == true)
            {
                txtSifra.Enabled = false;
            }
            else
            {
                txtSifra.Enabled = true;
            }

            txtNaziv.Enabled = x;
            txtAdresa.Enabled = x;
            cbDjelatnost.Enabled = x;
            txtZR.Enabled = x;
            cbDrzava.Enabled = x;
            txtOib.Enabled = x;

            txtZR.Enabled = x;
            cbDjelatnost.Enabled = x;
            cbGrad.Enabled = x;
            cbPPT.Enabled = x;
            rtbNapomena.Enabled = x;

            dgwKontakti.Rows.Clear();
            //txtSifra.Text = brojPartner();
            txtNaziv.Text = "";
            txtAdresa.Text = "";
            ;
            txtZR.Text = "";
            txtOib.Text = "";
            txtZR.Text = "";
            rtbNapomena.Text = "";
        }

        private void cbFill()
        {
            //CB grad
            DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            //CB pošta
            cbPPT.DataSource = DSgrad.Tables[0];
            cbPPT.DisplayMember = "posta";
            cbPPT.ValueMember = "id_grad";

            //CB djelatnosti
            DSdjelatnosti = classSQL.select("SELECT * FROM djelatnosti ORDER BY ime_djelatnosti", "djelatnosti");
            cbDjelatnost.DataSource = DSdjelatnosti.Tables[0];
            cbDjelatnost.DisplayMember = "ime_djelatnosti";
            cbDjelatnost.ValueMember = "id_djelatnost";

            //CB dražave
            DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbDrzava.DataSource = DSdrazava.Tables[0];
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "id_zemlja";
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partner = new frmPartnerTrazi();
            partner.ShowDialog();

            if (Properties.Settings.Default.id_partner != "")
            {
                enable(true);
                cbFill();
                FillPartner(Properties.Settings.Default.id_partner);
                button1.Enabled = true;
                edit = true;
            }
        }

        private void FillPartner(string id)
        {
            DataTable tbPartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + id + "'", "partners").Tables[0];

            txtSifra.Text = tbPartner.Rows[0]["id_partner"].ToString();
            txtNaziv.Text = tbPartner.Rows[0]["ime_tvrtke"].ToString();
            cbDrzava.SelectedValue = tbPartner.Rows[0]["id_zemlja"].ToString();
            cbGrad.SelectedValue = tbPartner.Rows[0]["id_grad"].ToString();
            txtAdresa.Text = tbPartner.Rows[0]["adresa"].ToString();
            txtOib.Text = tbPartner.Rows[0]["oib"].ToString();
            cbDjelatnost.SelectedValue = tbPartner.Rows[0]["id_djelatnost"].ToString();
            txtZR.Text = tbPartner.Rows[0]["zr"].ToString();
            rtbNapomena.Text = tbPartner.Rows[0]["napomena"].ToString();

            DataSet DSkontakti = classSQL.select("SELECT * FROM kontakti_partner WHERE id_partner='" + id + "'", "kontakti_partner");
            dgwKontakti.Rows.Clear();
            int br = DSkontakti.Tables[0].Rows.Count;
            for (int i = 0; i < br; i++)
            {
                dgwKontakti.Rows.Add(DSkontakti.Tables[0].Rows[i]["id_kontakt"], DSkontakti.Tables[0].Rows[i]["vrsta"], DSkontakti.Tables[0].Rows[i]["kontakt"], DSkontakti.Tables[0].Rows[i]["osoba"], "Obriši");
            }
            ControlDisableEnable(0, 1, 1, 0);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (edit == false)
            {
                spremi();
                button1.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1);
                edit = false;
            }
            else
            {
                promjeni();
                button1.Enabled = false;
                enable(false);
                edit = false;
                ControlDisableEnable(1, 0, 0, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (edit == true)
            {
                int b = dgwKontakti.Rows.Count;
                for (int i = 0; i < b; i++)
                {
                    classSQL.update("UPDATE kontakti_partner SET vrsta='" + dgwKontakti.Rows[i].Cells[1].FormattedValue.ToString() + "',kontakt='" + dgwKontakti.Rows[i].Cells[2].FormattedValue.ToString() + "',osoba='" + dgwKontakti.Rows[i].Cells[3].FormattedValue.ToString() + "' WHERE id_kontakt='" + dgwKontakti.Rows[i].Cells[0].FormattedValue.ToString() + "'");
                }

                classSQL.insert("INSERT INTO kontakti_partner (id_partner,vrsta) VALUES ('" + txtSifra.Text + "','Telefon')");
                DataSet DSkontakti = classSQL.select("SELECT * FROM kontakti_partner WHERE id_partner='" + txtSifra.Text + "'", "kontakti_partner");
                dgwKontakti.Rows.Clear();
                int br = DSkontakti.Tables[0].Rows.Count;
                for (int i = 0; i < br; i++)
                {
                    dgwKontakti.Rows.Add(DSkontakti.Tables[0].Rows[i]["id_kontakt"], DSkontakti.Tables[0].Rows[i]["vrsta"], DSkontakti.Tables[0].Rows[i]["kontakt"], DSkontakti.Tables[0].Rows[i]["osoba"], "Obriši");
                }
            }
            else
            {
                dgwKontakti.Rows.Add("", "Telefon", "", "", "Obriši");
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            enable(false);
            button1.Enabled = false;
            ControlDisableEnable(1, 0, 0, 1);
        }

        private void spremi()
        {
            DataTable DT = classSQL.select("SELECT id_partner FROM partners WHERE oib='" + txtOib.Text + "' AND oib NOT IN ('')", "partners").Tables[0];

            if (DT.Rows.Count > 0)
            {
                MessageBox.Show("Partner već postoji u bazi pod šifrom broj " + DT.Rows[0][0].ToString() + ".", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Sprema novog partnera
            string sql = "INSERT INTO partners (id_partner,ime_tvrtke,id_grad,adresa,oib,napomena,id_djelatnost,zr,id_zemlja)" +
                " VALUES (" +
                "'" + txtSifra.Text + "'," +
                "'" + txtNaziv.Text + "'," +
                "'" + cbGrad.SelectedValue + "'," +
                "'" + txtAdresa.Text + "'," +
                "'" + txtOib.Text + "'," +
                "'" + rtbNapomena.Text + "'," +
                "'" + cbDjelatnost.SelectedValue + "'," +
                "'" + txtZR.Text + "'," +
                "'" + cbDrzava.SelectedValue + "'" +
                ")";

            classSQL.insert(sql);
            string ssql = "";

            if (classSQL.remoteConnectionString == "")
            {
                ssql = "SELECT TOP(1) id_partner FROM partners ORDER BY CAST(id_partner AS integer) DESC ";
            }
            else
            {
                ssql = "SELECT id_partner FROM partners ORDER BY CAST(id_partner AS integer) DESC LIMIT 1";
            }

            string id_save = classSQL.select(ssql, "partners").Tables[0].Rows[0][0].ToString();

            int br = dgwKontakti.Rows.Count;
            string sql_kontakt = "";
            for (int i = 0; i < br; i++)
            {
                sql_kontakt = "INSERT INTO kontakti_partner (id_partner,vrsta,kontakt,osoba) VALUES ('" + id_save + "','" + dgwKontakti.Rows[i].Cells[0].FormattedValue.ToString() + "','" + dgwKontakti.Rows[i].Cells[1].FormattedValue.ToString() + "','" + dgwKontakti.Rows[i].Cells[2].FormattedValue.ToString() + "')";
                classSQL.insert(sql_kontakt);
            }

            MessageBox.Show("Spremljeno.");
            enable(false);
        }

        private void promjeni()
        {
            //Update novog partnera
            string sql = "UPDATE partners SET " +
                "ime_tvrtke='" + txtNaziv.Text + "'," +
                "id_grad='" + cbGrad.SelectedValue + "'," +
                "adresa='" + txtAdresa.Text + "'," +
                "oib='" + txtOib.Text + "'," +
                "napomena='" + txtOib.Text + "'," +
                "id_djelatnost='" + cbDjelatnost.SelectedValue + "'," +
                "zr='" + txtZR.Text + "'," +
                "id_zemlja='" + cbDrzava.SelectedValue + "'" +
                " WHERE id_partner='" + txtSifra.Text + "'";
            classSQL.update(sql);

            int br = dgwKontakti.Rows.Count;

            for (int i = 0; i < br; i++)
            {
                string sql_kontakt = "UPDATE kontakti_partner SET " +
                    "vrsta='" + dgwKontakti.Rows[i].Cells[1].FormattedValue.ToString() + "'," +
                    "kontakt='" + dgwKontakti.Rows[i].Cells[2].FormattedValue.ToString() + "'," +
                    "osoba='" + dgwKontakti.Rows[i].Cells[3].FormattedValue.ToString() + "' WHERE id_kontakt='" + dgwKontakti.Rows[i].Cells[0].FormattedValue.ToString() + "'";
                classSQL.update(sql_kontakt);
            }

            MessageBox.Show("Spremljeno.");
        }

        private void dgwKontakti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovaj kontakt.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int br = dgwKontakti.CurrentRow.Index;
                    string val = dgwKontakti.Rows[br].Cells[0].FormattedValue.ToString();
                    string sql = "DELETE FROM kontakti_partner WHERE id_kontakt='" + val + "'";
                    classSQL.delete(sql);
                    dgwKontakti.Rows.Remove(dgwKontakti.CurrentRow);
                }
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNaziv.Select();
            }
        }

        private void txtNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbDrzava.Select();
            }
        }

        private void cbDrzava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbGrad.Select();
            }
        }

        private void cbGrad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbPPT.Select();
            }
        }

        private void cbPPT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtAdresa.Select();
            }
        }

        private void txtAdresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtOib.Select();
            }
        }

        //private void txtR1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtOib.Select();
        //    }
        //}

        private void txtOib_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbDjelatnost.Select();
            }
        }

        private void cbDjelatnost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtZR.Select();
            }
        }

        private void txtZR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNoviPartner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainFormMenu != null)
            {
            }
        }

        private void txtSifra_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DataTable DT = classSQL.select("SELECT id_partner FROM partners WHERE id_partner='" + txtSifra.Text + "'", "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    enable(true);
                    cbFill();
                    FillPartner(txtSifra.Text);
                    button1.Enabled = true;
                    edit = true;
                }
                else
                {
                    if (MessageBox.Show("Kriva šifra.\r\nŽelite li dodati novog partnera?", "Greška", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        btnNew_Click(btnNoviUnos, e);
                    }
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