using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmSobe : Form
    {
        public frmSobe()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmSobe_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

            DataTable DTtip_sobe = RemoteDB.select("SELECT * FROM tip_sobe", "tip_sobe").Tables[0];
            cbTipSoba.DataSource = DTtip_sobe;
            cbTipSoba.DisplayMember = "Tip";
            cbTipSoba.ValueMember = "id";

            //fill tip sobe
            //fill zemlja
            DataTable DT_zemlja = RemoteDB.select("SELECT * FROM tip_sobe WHERE aktivnost='1'", "tip_sobe").Tables[0];

            DataTable DTSK = new DataTable("tip_sobe");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("Tip", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id"], DT_zemlja.Rows[i]["tip"]);
            }

            tip_sobe.DataSource = DTSK;
            tip_sobe.DataPropertyName = "Tip";
            tip_sobe.DisplayMember = "Tip";
            tip_sobe.HeaderText = "Tip";
            tip_sobe.Name = "Tip";
            tip_sobe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            tip_sobe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            tip_sobe.ValueMember = "id";

            SetSobe();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void gw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex >= 0)
                {
                    frmPopis_cijena_po_datumima pc = new frmPopis_cijena_po_datumima();
                    pc._id_soba = dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    pc.ShowDialog();
                }
            }
        }

        private void SetSobe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM sobe ORDER BY broj_sobe", "sobe").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(
                    DT.Rows[i]["naziv_sobe"].ToString(),
                    DT.Rows[i]["broj_sobe"].ToString(),
                    DT.Rows[i]["broj_lezaja"].ToString(),
                    DT.Rows[i]["id_tip_sobe"].ToString(),
                    "Pogled",
                    //DT.Rows[i]["cijena_nocenja"].ToString(),
                    DT.Rows[i]["napomena"].ToString(),
                    b,
                    DT.Rows[i]["id_tip_sobe"].ToString(),
                    DT.Rows[i]["id"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            decimal dec = 0;
            if (String.IsNullOrEmpty(txtNazivSobe.Text))
            {
                MessageBox.Show("Greška, niste upisali naziv sobe.");
                return;
            }

            if (!Decimal.TryParse(txtBrojSobe.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno broj sobe.");
                return;
            }

            if (!Decimal.TryParse(txtBrojLezaja.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno broj ležaja.");
                return;
            }

            if (!Decimal.TryParse(txtCijenaNocenja.Text, out dec))
            {
                txtCijenaNocenja.Text = "0";
                //MessageBox.Show("Greška kod upisa cijene noćenja.", "Greška"); return;
            }

            string sql = "INSERT INTO sobe (broj_sobe,id_tip_sobe,naziv_sobe,broj_lezaja,cijena_nocenja,napomena,aktivnost) VALUES (" +
                "'" + txtBrojSobe.Text + "'," +
                "'" + cbTipSoba.SelectedValue + "'," +
                "'" + txtNazivSobe.Text + "'," +
                "'" + txtBrojLezaja.Text + "'," +
                "'" + txtCijenaNocenja.Text + "'," +
                "'" + txtNapomena.Text + "'," +
                "'1'" +
                ")";
            RemoteDB.insert(sql);

            SetSobe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE sobe SET naziv_sobe='" + dgv.Rows[e.RowIndex].Cells["naziv_sobe"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = "UPDATE sobe SET broj_sobe='" + dgv.Rows[e.RowIndex].Cells["broj_sobe"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE sobe SET broj_lezaja='" + dgv.Rows[e.RowIndex].Cells["broj_lezaja"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    string sql = "UPDATE sobe SET id_tip_sobe='" + dgv.Rows[e.RowIndex].Cells[3].Value + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = "UPDATE sobe SET cijena_nocenja='" + dgv.Rows[e.RowIndex].Cells["cijena_nocenja"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    string sql = "UPDATE sobe SET napomena='" + dgv.Rows[e.RowIndex].Cells["napomena"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE sobe SET aktivnost='" + aa + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmSobe_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}