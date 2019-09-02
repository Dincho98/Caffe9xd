using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmVrstaUsluge : Form
    {
        public frmVrstaUsluge()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmVrstaUsluge_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetVrstaUsluge();
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

        private void SetVrstaUsluge()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM vrsta_usluge ORDER BY naziv_usluge", "vrsta_usluge").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(
                    DT.Rows[i]["naziv_usluge"].ToString(),
                    Convert.ToDecimal(DT.Rows[i]["iznos"].ToString()).ToString("#0.00"),
                    DT.Rows[i]["napomena"].ToString(),
                    b,
                    DT.Rows[i]["id"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO vrsta_usluge (naziv_usluge,iznos,napomena,aktivnost) VALUES (" +
                "'" + txtNazivUsluge.Text + "'," +
                "'" + txtIznos.Text.Replace(",", ".") + "'," +
                "'" + txtNapomena.Text + "'," +
                "'1'" +
                ")";
            RemoteDB.insert(sql);

            SetVrstaUsluge();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE vrsta_usluge SET naziv_usluge='" + dgv.Rows[e.RowIndex].Cells["ime_usluge"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE vrsta_usluge SET iznos='" + dgv.Rows[e.RowIndex].Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE vrsta_usluge SET napomena='" + dgv.Rows[e.RowIndex].Cells["napomena"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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

                    string sql = "UPDATE vrsta_usluge SET aktivnost='" + aa + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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