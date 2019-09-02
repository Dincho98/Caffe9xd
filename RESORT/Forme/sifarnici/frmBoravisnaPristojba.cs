using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmBoravisnaPristojba : Form
    {
        public frmBoravisnaPristojba()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmBoravisnaPristojba_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetBoravisna_pristojba();
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

        private void SetBoravisna_pristojba()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM boravisna_pristojba ORDER BY boravisna_pristojba", "boravisna_pristojba").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(
                    DT.Rows[i]["oznaka"].ToString(),
                    DT.Rows[i]["boravisna_pristojba"].ToString(),
                    Convert.ToDecimal(DT.Rows[i]["iznos"].ToString()).ToString("#0.00"),
                    b,
                    DT.Rows[i]["id"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOznake.Text))
            {
                MessageBox.Show("Greška, niste upisali naziv pristojbe.");
                return;
            }

            string sql = "INSERT INTO boravisna_pristojba (oznaka,boravisna_pristojba,iznos,aktivnost) VALUES (" +
                "'" + txtOznake.Text + "'," +
                "'" + txtNazivPristojbe.Text + "'," +
                "'" + txtIznos.Text + "'," +
                "'1'" +
                ")";
            RemoteDB.insert(sql);

            SetBoravisna_pristojba();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE boravisna_pristojba SET oznaka='" + dgv.Rows[e.RowIndex].Cells["oznaka"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE boravisna_pristojba SET boravisna_pristojba='" + dgv.Rows[e.RowIndex].Cells["naziv_pristojbe"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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
                    decimal dec;
                    if (!decimal.TryParse(dgv.Rows[e.RowIndex].Cells["iznos"].FormattedValue.ToString(), out dec))
                    {
                        MessageBox.Show("Greška, niste upisali pravilno avans.");
                        dgv.Rows[e.RowIndex].Cells["iznos"].Value = "0.00";
                        return;
                    }

                    string sql = "UPDATE boravisna_pristojba SET iznos='" + dgv.Rows[e.RowIndex].Cells["iznos"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE boravisna_pristojba SET aktivnost='" + aa + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmAgencije_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}