using System;
using System.Data;
using System.Windows.Forms;

namespace RESORT.Forme.Sifarnik
{
    public partial class frmNoviGrad : Form
    {
        private DataTable DTBojeForme;

        public frmNoviGrad()
        {
            InitializeComponent();
        }

        private void frmNoviGrad_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetGrupe();

            //fill zemlja
            DataTable DT_zemlja = RemoteDB.select("SELECT * FROM zemlja WHERE aktivnost='DA'", "zemlja").Tables[0];
            cbDrazava.DataSource = DT_zemlja;
            cbDrazava.DisplayMember = "zemlja";
            cbDrazava.ValueMember = "id_zemlja";
            cbDrazava.SelectedValue = "60";

            DataTable DTSK = new DataTable("zemlja");
            DTSK.Columns.Add("id_zemlja", typeof(string));
            DTSK.Columns.Add("zemlja", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id_zemlja"], DT_zemlja.Rows[i]["zemlja"]);
            }

            drzava.DataSource = DTSK;
            drzava.DataPropertyName = "zemlja";
            drzava.DisplayMember = "zemlja";
            drzava.HeaderText = "Država";
            drzava.Name = "zemlja";
            drzava.Resizable = DataGridViewTriState.True;
            drzava.SortMode = DataGridViewColumnSortMode.Automatic;
            drzava.ValueMember = "id_zemlja";

            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void SetGrupe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM grad", "grad").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["grad"].ToString(), DT.Rows[i]["posta"].ToString(),
                    DT.Rows[i]["zupanija"].ToString(), DT.Rows[i]["id_drzava"].ToString(),
                    DT.Rows[i]["naselje"].ToString(), DT.Rows[i]["id_grad"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivGrada.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe.");
                return;
            }

            string sql = string.Format(@"INSERT INTO grad (grad, posta, id_drzava, zupanija, naselje)
VALUES
    ( '{0}', '{1}', '{2}', '{3}', '{4}' );",
                txtnazivGrada.Text,
                txtPosta.Text,
                cbDrazava.SelectedValue.ToString(),
                txtZupanija.Text,
                txtNaselje.Text);

            RemoteDB.insert(sql);

            odustani();

            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void odustani()
        {
            txtNaselje.Text = "";
            txtnazivGrada.Text = "";
            txtPosta.Text = "";
            txtZupanija.Text = "";
            cbDrazava.SelectedValue = "60";
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format("UPDATE grad SET grad = '{0}' WHERE id_grad = '{1}';",
                        dgv.Rows[e.RowIndex].Cells["grad"].FormattedValue.ToString(),
                        dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = string.Format("UPDATE grad SET posta = '{0}' WHERE id_grad = '{1}';",
                        dgv.Rows[e.RowIndex].Cells["posta"].FormattedValue.ToString(),
                        dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = string.Format("UPDATE grad SET zupanija = '{0}' WHERE id_grad = '{1}';",
                        dgv.Rows[e.RowIndex].Cells["id_zupanija"].FormattedValue.ToString(),
                        dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    string sql = string.Format("UPDATE grad SET id_drzava = '{0}' WHERE id_grad = '{1}';",
                        dgv.Rows[e.RowIndex].Cells[3].Value,
                        dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = string.Format("UPDATE grad SET naselje = '{0}' WHERE id_grad = '{1}';",
                        dgv.Rows[e.RowIndex].Cells[4].Value,
                        dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}