using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmNovaDrzava : Form
    {
        public frmNovaDrzava()
        {
            InitializeComponent();
        }

        private void frmNoviGrad_Load(object sender, EventArgs e)
        {
            SetGrupe();

            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataSet DS_Skladiste;

        private void SetGrupe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["country_code"].ToString(), DT.Rows[i]["zemlja"].ToString(), b, DT.Rows[i]["id_zemlja"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtKod.Text == "" || txtDrzava.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv ili kod države");
                return;
            }

            string sql = "INSERT INTO zemlja (country_code,zemlja,aktivnost) VALUES (" +
                "'" + txtKod.Text + "'," +
                "'" + txtDrzava.Text + "','1'" +
                ")";
            RemoteDB.insert(sql);

            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE zemlja SET country_code ='" + dgv.Rows[e.RowIndex].Cells["kod"].FormattedValue.ToString() + "' WHERE id_zemlja='" + dgv.Rows[e.RowIndex].Cells["id_drzava"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE zemlja SET zemlja='" + dgv.Rows[e.RowIndex].Cells["drzava"].FormattedValue.ToString() + "' WHERE id_zemlja='" + dgv.Rows[e.RowIndex].Cells["id_drzava"].FormattedValue.ToString() + "'";
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
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE zemlja SET aktivnost='" + aa + "' WHERE id_zemlja='" + dgv.Rows[e.RowIndex].Cells["id_drzava"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmNovaDrzava_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}