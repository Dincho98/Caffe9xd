using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmNoviGrad : Form
    {
        public frmNoviGrad()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNoviGrad_Load(object sender, EventArgs e)
        {
            SetGrupe();

            //fill zemlja
            DataTable DT_zemlja = classSQL.select("SELECT * FROM zemlja WHERE aktivnost='DA'", "zemlja").Tables[0];
            cbDrazava.DataSource = DT_zemlja;
            cbDrazava.DisplayMember = "zemlja";
            cbDrazava.ValueMember = "id_zemlja";

            DataTable DTSK = new DataTable("zemlja");
            DTSK.Columns.Add("id_zemlja", typeof(string));
            DTSK.Columns.Add("zemlja", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id_zemlja"], DT_zemlja.Rows[i]["zemlja"]);
            }

            id_drazava.DataSource = DTSK;
            id_drazava.DataPropertyName = "zemlja";
            id_drazava.DisplayMember = "zemlja";
            id_drazava.HeaderText = "zemlja";
            id_drazava.Name = "zemlja";
            id_drazava.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            id_drazava.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            id_drazava.ValueMember = "id_zemlja";

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataSet DS_Skladiste;

        private void SetGrupe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM grad", "grad").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["grad"].ToString(), DT.Rows[i]["posta"].ToString(), DT.Rows[i]["zupanija"].ToString(), DT.Rows[i]["id_drzava"].ToString(), b, DT.Rows[i]["id_grad"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivGrada.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe.");
                return;
            }

            string sql = "INSERT INTO grad (grad,posta,id_drzava,zupanija,aktivnost) VALUES (" +
                "'" + txtnazivGrada.Text + "'," +
                "'" + txtPosta.Text + "'," +
                "'" + cbDrazava.SelectedValue.ToString() + "'," +
                "'" + txtZupanija.Text + "','1'" +
                ")";
            classSQL.insert(sql);

            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE grad SET grad='" + dgv.Rows[e.RowIndex].Cells["grad"].FormattedValue.ToString() + "' WHERE id_grad='" + dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE grad SET posta='" + dgv.Rows[e.RowIndex].Cells["posta"].FormattedValue.ToString() + "' WHERE id_grad='" + dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString() + "'";
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
                    string sql = "UPDATE grad SET zupanija='" + dgv.Rows[e.RowIndex].Cells["id_zupanija"].FormattedValue.ToString() + "' WHERE id_grad='" + dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
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
                    string sql = "UPDATE grad SET id_drzava='" + dgv.Rows[e.RowIndex].Cells[3].Value + "' WHERE id_grad='" + dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
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
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE grad SET aktivnost='" + aa + "' WHERE id_grad='" + dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString() + "'";
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