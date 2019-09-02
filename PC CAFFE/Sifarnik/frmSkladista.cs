using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmSkladista : Form
    {
        public frmSkladista()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSsk = new DataSet();

        private void frmSkladista_Load(object sender, EventArgs e)
        {
            SetDgv();
            SetCb();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private bool boolNovi = false;

        private void SetCb()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT DISTINCT (grad),id_grad FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            //CB dražave
            DataSet DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemlja.DataSource = DSdrazava.Tables[0];
            cbZemlja.DisplayMember = "zemlja";
            cbZemlja.ValueMember = "id_zemlja";

            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();
            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();

            cbKalkulacija.Items.Add("DA");
            cbKalkulacija.Items.Add("NE");
            cbKalkulacija.SelectedIndex = cbKalkulacija.FindStringExact(dgvSk.CurrentRow.Cells["kalkulacija"].FormattedValue.ToString());

            if (dgvSk.CurrentRow.Cells["aktivnost"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }
        }

        private void SetDgv()
        {
            if (dgvSk.Rows.Count != 0)
            {
                dgvSk.Rows.Clear();
            }
            DSsk = new DataSet();
            if (classSQL.remoteConnectionString == "")
            {
                string sql = "SELECT skladiste.skladiste AS Skladište,grad.grad AS Grad,zemlja.zemlja AS Država, skladiste.kalkulacija, skladiste.aktivnost, skladiste.id_grad,skladiste.id_zemlja,skladiste.id_skladiste FROM skladiste LEFT JOIN grad ON skladiste.id_grad=grad.id_grad LEFT JOIN zemlja " +
                    " ON zemlja.id_zemlja=skladiste.id_zemlja";
                classSQL.CeAdatpter(sql).Fill(DSsk, "skladiste");
            }
            else
            {
                string sql = "SELECT skladiste.skladiste AS [Skladište],grad.grad AS Grad,zemlja.zemlja AS [Država], skladiste.kalkulacija, skladiste.aktivnost, skladiste.id_grad,skladiste.id_zemlja,skladiste.id_skladiste FROM skladiste LEFT JOIN grad ON skladiste.id_grad=grad.id_grad LEFT JOIN zemlja " +
                " ON zemlja.id_zemlja=skladiste.id_zemlja";
                classSQL.NpgAdatpter(sql).Fill(DSsk, "skladiste");
            }

            for (int i = 0; i < DSsk.Tables[0].Rows.Count; i++)
            {
                dgvSk.Rows.Add(DSsk.Tables[0].Rows[i]["Skladište"].ToString(), DSsk.Tables[0].Rows[i]["Grad"].ToString(), DSsk.Tables[0].Rows[i]["Država"].ToString(), DSsk.Tables[0].Rows[i]["kalkulacija"].ToString(), DSsk.Tables[0].Rows[i]["aktivnost"].ToString(), DSsk.Tables[0].Rows[i]["id_grad"].ToString(), DSsk.Tables[0].Rows[i]["id_zemlja"].ToString(), DSsk.Tables[0].Rows[i]["id_skladiste"].ToString());
            }

            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();

            if (dgvSk.CurrentRow.Cells["aktivnost"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (boolNovi == false)
            {
                boolNovi = false;
                string sql = "UPDATE skladiste SET skladiste='" + txtSkladiste.Text + "',id_grad='" + cbGrad.SelectedValue + "',id_zemlja='" + cbZemlja.SelectedValue + "',aktivnost='" + GetValueCheckBox() + "', kalkulacija ='" + cbKalkulacija.Text + "' WHERE id_skladiste='" + dgvSk.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString() + "'";
                classSQL.update(sql);
                SetDgv();
            }
            else
            {
                if (MessageBox.Show("Nakon unosa ovog skladišta nećete imati više mogućnosti izbrisati isto.", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    boolNovi = false;
                    string sql = "INSERT INTO skladiste (skladiste,id_grad,id_zemlja,kalkulacija,aktivnost) VALUES ('" + txtSkladiste.Text + "','" + cbGrad.SelectedValue + "','" + cbZemlja.SelectedValue + "','" + cbKalkulacija.Text + "','" + GetValueCheckBox() + "')";
                    provjera_sql(classSQL.insert(sql));
                    SetDgv();
                }
            }
            dgvSk.Enabled = true;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void dgvSk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();
            cbKalkulacija.SelectedIndex = cbKalkulacija.FindStringExact(dgvSk.CurrentRow.Cells["kalkulacija"].FormattedValue.ToString());

            if (dgvSk.CurrentRow.Cells["aktivnost"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            boolNovi = true;
            SetOnNull();
            dgvSk.Enabled = false;
        }

        private void SetOnNull()
        {
            txtSkladiste.Text = "";
            chbAktivnost.Checked = true;
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

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            boolNovi = false;
            dgvSk.Enabled = true;
            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();
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