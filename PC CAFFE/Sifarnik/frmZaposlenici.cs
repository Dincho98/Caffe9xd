using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmZaposlenici : Form
    {
        public frmZaposlenici()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DS = new DataSet();
        private bool BoolNovi = false;

        private void frmZaposlenici_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            SetDGV();
            SetCb();
            btnSpremi.Enabled = false;
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void SetDGV()
        {
            if (dgv.Rows.Count != 0)
            {
                dgv.Rows.Clear();
            }
            DS = new DataSet();

            string sql = "SELECT " +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.ime," +
                " zaposlenici.prezime," +
                " zaposlenici.id_grad," +
                " zaposlenici.adresa," +
                " zaposlenici.id_dopustenje," +
                " zaposlenici.oib," +
                " zaposlenici.tel," +
                " zaposlenici.datum_rodenja," +
                " zaposlenici.email," +
                " zaposlenici.mob," +
                " zaposlenici.aktivan," +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.zaporka," +
                " zaposlenici.kartica," +
                " grad.grad," +
                " dopustenja.naziv" +
                " FROM zaposlenici " +
                " LEFT JOIN dopustenja ON dopustenja.id_dopustenje=zaposlenici.id_dopustenje " +
                " LEFT JOIN grad ON grad.id_grad=zaposlenici.id_grad WHERE zaposlenici.id_zaposlenik NOT IN ('1')";
            DS = classSQL.select(sql, "zaposlenici");

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DS.Tables[0].Rows[i]["id_zaposlenik"].ToString(),
                    DS.Tables[0].Rows[i]["ime"].ToString(),
                    DS.Tables[0].Rows[i]["prezime"].ToString(),
                    DS.Tables[0].Rows[i]["grad"].ToString(),
                    DS.Tables[0].Rows[i]["adresa"].ToString(),
                    DS.Tables[0].Rows[i]["oib"].ToString(),
                    DS.Tables[0].Rows[i]["tel"].ToString(),
                    DS.Tables[0].Rows[i]["mob"].ToString(),
                    DS.Tables[0].Rows[i]["email"].ToString(),
                    DS.Tables[0].Rows[i]["datum_rodenja"].ToString(),
                    DS.Tables[0].Rows[i]["zaporka"].ToString(),
                    DS.Tables[0].Rows[i]["naziv"].ToString(),
                    DS.Tables[0].Rows[i]["aktivan"].ToString(),
                    DS.Tables[0].Rows[i]["id_grad"].ToString(),
                    DS.Tables[0].Rows[i]["id_dopustenje"].ToString(),
                    DS.Tables[0].Rows[i]["id_zaposlenik"].ToString(),
                    DS.Tables[0].Rows[i]["kartica"].ToString());
            }
        }

        private void SetValueInTextBox()
        {
            try
            {
                txtIme.Text = dgv.CurrentRow.Cells["ime"].FormattedValue.ToString();
                txtPrezime.Text = dgv.CurrentRow.Cells["prezime"].FormattedValue.ToString();
                txtAdresa.Text = dgv.CurrentRow.Cells["adresa"].FormattedValue.ToString();
                txtTelefon.Text = dgv.CurrentRow.Cells["tel"].FormattedValue.ToString();
                txtMobitel.Text = dgv.CurrentRow.Cells["mobitel"].FormattedValue.ToString();
                txtEmail.Text = dgv.CurrentRow.Cells["email"].FormattedValue.ToString();
                txtOib.Text = dgv.CurrentRow.Cells["oib"].FormattedValue.ToString();
                dtpRoden.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["roden"].FormattedValue.ToString());
                txtZaporka.Text = dgv.CurrentRow.Cells["zaporka"].FormattedValue.ToString();
                cbDopustenje.SelectedValue = dgv.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
                cbGrad.SelectedValue = dgv.CurrentRow.Cells["id_dopustenje"].FormattedValue.ToString();
                txtKarticaCode.Text = dgv.CurrentRow.Cells["kartica"].FormattedValue.ToString();

                cbGrad.SelectedValue = dgv.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
                cbDopustenje.SelectedValue = dgv.CurrentRow.Cells["id_dopustenje"].FormattedValue.ToString();

                if (dgv.CurrentRow.Cells["aktivan"].FormattedValue.ToString() == "DA")
                {
                    chbAktivnost.Checked = true;
                }
                else
                {
                    chbAktivnost.Checked = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void SetCb()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT DISTINCT (grad),id_grad FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2086";

            //CB dražave
            DataSet DSdop = classSQL.select("SELECT * FROM dopustenja ORDER BY naziv", "dopustenja");
            cbDopustenje.DataSource = DSdop.Tables[0];
            cbDopustenje.DisplayMember = "naziv";
            cbDopustenje.ValueMember = "id_dopustenje";

            SetValueInTextBox();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (BoolNovi == false)
            {
                UpdateZap();
                btnSpremi.Enabled = false;
                btnNovo.Enabled = true;
            }
            else
            {
                Spremaj();
                btnSpremi.Enabled = false;
                btnNovo.Enabled = true;
            }

            classSQL.update("UPDATE zaposlenici SET editirano='1'");
        }

        private void UpdateZap()
        {
            string sql = "UPDATE zaposlenici SET " +
        " ime='" + txtIme.Text + "'," +
        " prezime='" + txtPrezime.Text + "'," +
        " id_grad='" + cbGrad.SelectedValue + "'," +
        " kartica='" + txtKarticaCode.Text + "'," +
        " adresa='" + txtAdresa.Text + "'," +
        " id_dopustenje='" + cbDopustenje.SelectedValue + "'," +
        " oib='" + txtOib.Text + "'," +
        " tel='" + txtTelefon.Text + "'," +
        " datum_rodenja='" + Convert.ToDateTime(dtpRoden.Value).ToString("yyyy-MM-dd H:mm:ss") + "'," +
        " email='" + txtEmail.Text + "'," +
        " mob='" + txtMobitel.Text + "'," +
        " aktivan='" + GetValueCheckBox() + "'," +
        " zaporka='" + txtZaporka.Text + "'" +
        " WHERE id_zaposlenik='" + dgv.CurrentRow.Cells["id_zaposlenik"].FormattedValue.ToString() + "'";

            classSQL.update(sql);
            SetDGV();
            BoolNovi = false;
        }

        private void Spremaj()
        {
            if (MessageBox.Show("Nakon unosa ovog zaposlenika nećete imati više mogućnosti izbrisati istog.", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable DT = classSQL.select("SELECT MAX(id_zaposlenik)zbroj1 AS id FROM zaposlenici", "zaposlenici").Tables[0];
                int id_zaposlenik = 1;

                if (DT.Rows.Count > 0)
                {
                    int.TryParse(DT.Rows[0][0].ToString(), out id_zaposlenik);
                }

                string sql = "INSERT INTO zaposlenici (" +
                " id_zaposlenik,ime,kartica,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka" +
                ") VALUES (" +
                " '" + id_zaposlenik.ToString() + "'," +
                " '" + txtIme.Text + "'," +
                " '" + txtKarticaCode.Text + "'," +
                " '" + txtPrezime.Text + "'," +
                " '" + cbGrad.SelectedValue + "'," +
                " '" + txtAdresa.Text + "'," +
                " '" + cbDopustenje.SelectedValue + "'," +
                " '" + txtOib.Text + "'," +
                " '" + txtTelefon.Text + "'," +
                " '" + dtpRoden.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + txtEmail.Text + "'," +
                " '" + txtMobitel.Text + "'," +
                " '" + GetValueCheckBox() + "'," +
                " '" + txtZaporka.Text + "'" +
                " )";

                classSQL.insert(sql);
                BoolNovi = false;
                SetDGV();
            }
        }

        private string GetValueCheckBox()
        {
            if (chbAktivnost.Checked)
            {
                return "DA";
            }
            else
            {
                return "NE";
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            DelFields();
            BoolNovi = true;
            dgv.Enabled = false;
            btnSpremi.Enabled = true;
            btnNovo.Enabled = false;
        }

        private void DelFields()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtAdresa.Text = "";
            txtOib.Text = "";
            txtTelefon.Text = "";
            dtpRoden.Value = DateTime.Now;
            txtEmail.Text = "";
            txtMobitel.Text = "";
            txtZaporka.Text = "";
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetValueInTextBox();
            btnSpremi.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dgv.Rows.Count != 0)
            {
                dgv.Rows.Clear();
            }
            DS = new DataSet();

            string sql = "SELECT " +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.ime," +
                " zaposlenici.prezime," +
                " zaposlenici.id_grad," +
                " zaposlenici.adresa," +
                " zaposlenici.id_dopustenje," +
                " zaposlenici.oib," +
                " zaposlenici.tel," +
                " zaposlenici.datum_rodenja," +
                " zaposlenici.email," +
                " zaposlenici.mob," +
                " zaposlenici.aktivan," +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.zaporka," +
                " grad.grad," +
                " dopustenja.naziv" +
                " FROM zaposlenici " +
                " LEFT JOIN dopustenja ON dopustenja.id_dopustenje=zaposlenici.id_dopustenje " +
                " LEFT JOIN grad ON grad.id_grad=zaposlenici.id_grad WHERE ime LIKE '%" + textBox1.Text + "%'";
            DS = classSQL.select(sql, "zaposlenici");

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DS.Tables[0].Rows[i]["id_zaposlenik"].ToString(),
                    DS.Tables[0].Rows[i]["ime"].ToString(),
                    DS.Tables[0].Rows[i]["prezime"].ToString(),
                    DS.Tables[0].Rows[i]["grad"].ToString(),
                    DS.Tables[0].Rows[i]["adresa"].ToString(),
                    DS.Tables[0].Rows[i]["oib"].ToString(),
                    DS.Tables[0].Rows[i]["tel"].ToString(),
                    DS.Tables[0].Rows[i]["mob"].ToString(),
                    DS.Tables[0].Rows[i]["email"].ToString(),
                    DS.Tables[0].Rows[i]["datum_rodenja"].ToString(),
                    DS.Tables[0].Rows[i]["zaporka"].ToString(),
                    DS.Tables[0].Rows[i]["naziv"].ToString(),
                    DS.Tables[0].Rows[i]["aktivan"].ToString(),
                    DS.Tables[0].Rows[i]["id_grad"].ToString(),
                    DS.Tables[0].Rows[i]["id_dopustenje"].ToString(),
                    DS.Tables[0].Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            BoolNovi = false;
            dgv.Enabled = true;
            btnNovo.Enabled = true;
            SetDGV();
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