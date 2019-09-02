using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmZiroRacun : Form
    {
        public frmZiroRacun()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DSducani = new DataSet();

        private void frmDucani_Load(object sender, EventArgs e)
        {
            SetDucani();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void SetDucani()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT id_ziroracun,ziroracun,banka,aktivnost FROM ziro_racun", "ziro_racun").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString().ToUpper() == "DA")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["banka"].ToString(), DT.Rows[i]["ziroracun"].ToString(), b, DT.Rows[i]["id_ziroracun"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtNazivBanke.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv poslovnice.");
                return;
            }

            string sql = "INSERT INTO ziro_racun (banka,ziroracun,aktivnost) VALUES (" +
                "'" + txtNazivBanke.Text + "'," +
                "'" + txtBrojRacuna.Text + "'," +
                "'DA'" +
                ")";
            classSQL.insert(sql);

            SetDucani();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE ziro_racun SET banka='" + dgv.Rows[e.RowIndex].Cells["banka"].FormattedValue.ToString() + "' WHERE id_ziroracun='" + dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
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
                    string sql = "UPDATE ziro_racun SET ziroracun='" + dgv.Rows[e.RowIndex].Cells["ziroracun"].FormattedValue.ToString() + "' WHERE id_ziroracun='" + dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
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
                    string aa = "NE";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "DA";
                    }

                    string sql = "UPDATE ziro_racun SET aktivnost='" + aa + "' WHERE id_ziroracun='" + dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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