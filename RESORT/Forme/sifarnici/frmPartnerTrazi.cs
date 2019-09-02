using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmPartnerTrazi : Form
    {
        private DataSet DSpartneri = new DataSet();

        public frmPartnerTrazi()
        {
            InitializeComponent();
        }

        private void frmPartnerTrazi_Load(object sender, EventArgs e)
        {
            PaintRows(dataGridView2);
            PaintRows(dataGridView1);
            Properties.Settings.Default.id_partner = "";
            Properties.Settings.Default.Save();
            fillCB();
            txtIme1.Select();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void GoreDole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView2.RowCount > 0)
                {
                    int br = dataGridView2.CurrentRow.Index;
                    ;
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    Properties.Settings.Default.id_partner = id;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
            }

            if (e.KeyData == Keys.Up)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent > 0)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent - 1].Cells[0];
            }

            if (e.KeyData == Keys.Down)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent < dataGridView2.RowCount - 1)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent + 1].Cells[0];
            }
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string query_top = "";
            string query_djelatnost = "";
            string query_grad = "";
            string query_drazava = "";

            if (CHime.Checked)
            {
                query_top = " WHERE partners.ime_tvrtke LIKE '%" + txtTekst.Text + "%'";
            }
            else if (CHsifra.Checked)
            {
                query_top = " WHERE partners.id_partner ='" + txtTekst.Text + "'";
            }
            else if (CHoib.Checked)
            {
                query_top = " WHERE partners.oib = '" + txtTekst.Text + "'";
            }

            if (chDjelatnost.Checked)
            {
                query_djelatnost = " AND partners.id_djelatnost='" + cbDjelatnost.SelectedValue + "'";
            }

            if (chDrzava.Checked)
            {
                query_drazava = " AND partners.id_zemlja='" + cbDrzava.SelectedValue + "'";
            }

            if (chGrad.Checked)
            {
                query_grad = " AND partners.id_grad='" + cbGrad.SelectedValue + "'";
            }

            string top = "";
            string remote = "";
            //string where = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            DSpartneri = RemoteDB.select("SELECT " + top + " partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka], partners.oib AS [OIB], zemlja.zemlja AS [Država], grad.grad AS [Grad], djelatnosti.ime_djelatnosti AS [Djelatnost] FROM partners " +
            "LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
            "LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
            "LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja " + query_top + query_drazava + query_grad + query_djelatnost + "ORDER BY partners.ime_tvrtke" + remote, "partners");
            dataGridView1.DataSource = DSpartneri.Tables[0];

            PaintRows(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string top = "";
            string remote = "";
            string where = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 30";
                where = "ime_tvrtke ~* '" + txtIme1.Text + "'";
            }
            else
            {
                where = "ime_tvrtke LIKE '%" + txtIme1.Text + "%'";
                top = " TOP(30) ";
            }

            DSpartneri = RemoteDB.select("SELECT " + top + " partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka], partners.oib AS [OIB], zemlja.zemlja AS [Država], grad.grad AS [Grad], djelatnosti.ime_djelatnosti AS [Djelatnost] FROM partners " +
            "LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
            "LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
            "LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja WHERE " + where + " ORDER BY partners.ime_tvrtke " + remote + "", "partners");
            dataGridView2.DataSource = DSpartneri.Tables[0];

            PaintRows(dataGridView2);
        }

        private void fillCB()
        {
            //CB grad
            DataSet DSgrad = RemoteDB.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            //CB djelatnosti
            DataSet DSdjelatnosti = RemoteDB.select("SELECT * FROM djelatnosti ORDER BY ime_djelatnosti", "djelatnosti");
            cbDjelatnost.DataSource = DSdjelatnosti.Tables[0];
            cbDjelatnost.DisplayMember = "ime_djelatnosti";
            cbDjelatnost.ValueMember = "id_djelatnost";

            //CB dražave
            DataSet DSdrazava = RemoteDB.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbDrzava.DataSource = DSdrazava.Tables[0];
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "id_zemlja";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                Properties.Settings.Default.id_partner = dataGridView1.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                Properties.Settings.Default.id_partner = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void txtTekst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTrazi.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddPartners np = new frmAddPartners();
            np.Show();
        }

        private void txtPartnerPremaSifri_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnerPremaSifri.Text == "")
            {
                return;
            }

            string top = "";
            string remote = "";
            string where = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 40";
                where = "CAST(partners.id_partner AS TEXT) LIKE '%" + txtPartnerPremaSifri.Text + "%'";
            }
            else
            {
                where = "partners.id_partner LIKE '%" + txtPartnerPremaSifri.Text + "%'";
                top = " TOP(40) ";
            }

            DSpartneri = RemoteDB.select("SELECT " + top + " partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka], partners.oib AS [OIB], zemlja.zemlja AS [Država], grad.grad AS [Grad], djelatnosti.ime_djelatnosti AS [Djelatnost] FROM partners " +
            "LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
            "LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
            "LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja WHERE " + where + " ORDER BY partners.ime_tvrtke " + remote + "", "partners");
            dataGridView2.DataSource = DSpartneri.Tables[0];

            PaintRows(dataGridView2);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmAddPartners n = new frmAddPartners();
            n.ShowDialog();
        }
    }
}