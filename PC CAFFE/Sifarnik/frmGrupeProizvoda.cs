using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmGrupeProizvoda : Form
    {
        public frmGrupeProizvoda()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet DS = new DataSet();

        private void frmGrupeProizvoda_Load(object sender, EventArgs e)
        {
            SetGrupe();

            //fill grupe
            DataTable DT_podgrupa = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            cbpodgrupa.DataSource = DT_podgrupa;
            cbpodgrupa.DisplayMember = "naziv";
            cbpodgrupa.ValueMember = "id_podgrupa";

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //Graphics c = e.Graphics;
        //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataSet DS_Skladiste;

        private void SetGrupe()
        {
            DataTable DTSK = new DataTable("podgrupa");
            DTSK.Columns.Add("id_podgrupa", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM podgrupa", "podgrupa");
            //DTSK.Rows.Add(0,"");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_podgrupa"], DS_Skladiste.Tables[0].Rows[i]["naziv"]);
            }

            id_podgrupa.DataSource = DTSK;
            id_podgrupa.DataPropertyName = "naziv";
            id_podgrupa.DisplayMember = "naziv";
            id_podgrupa.HeaderText = "naziv";
            id_podgrupa.Name = "naziv";
            id_podgrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            id_podgrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            id_podgrupa.ValueMember = "id_podgrupa";

            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM grupa order by grupa asc", "grupa").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                bool d = false;
                if (Convert.ToBoolean(DT.Rows[i]["is_dodatak"].ToString()))
                {
                    d = true;
                }
                bool pol = false;
                if (Convert.ToBoolean(DT.Rows[i]["is_polpol"].ToString()))
                {
                    pol = true;
                }
                dgv.Rows.Add(DT.Rows[i]["grupa"].ToString(), DT.Rows[i]["id_podgrupa"].ToString(), b, d, pol, DT.Rows[i]["id_grupa"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivGrupe.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe.");
                return;
            }

            string s = "SELECT setval('grupa_id_grupa_seq', (SELECT MAX(id_grupa) FROM grupa)+1)";
            classSQL.insert(s);

            string sql = "INSERT INTO grupa (grupa,aktivnost,id_podgrupa, is_dodatak) VALUES (" +
                "'" + txtnazivGrupe.Text + "'," +
                "'1'," +
                "'" + cbpodgrupa.SelectedValue.ToString() + "'," +
                "'" + (chkIsDodatak.Checked ? 1 : 0) + "')";
            classSQL.insert(sql);

            classSQL.update("UPDATE grupa SET editirano='1'");

            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 0)
                    {
                        try
                        {
                            string aa = dgv.Rows[e.RowIndex].Cells["grupa"].Value.ToString();

                            string sql = "UPDATE grupa SET grupa='" + aa + "' WHERE id_grupa='" + dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString() + "'";
                            classSQL.update(sql);

                            classSQL.update("UPDATE grupa SET editirano='1'");
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
                            string aa = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                            string sql = "UPDATE grupa SET id_podgrupa = '" + aa + "' WHERE id_grupa='" + dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString() + "'";
                            classSQL.update(sql);

                            classSQL.update("UPDATE grupa SET editirano='1'");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            string aa = "0";
                            if (Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["aktivnost"].Value))
                            {
                                dgv.Rows[e.RowIndex].Cells["aktivnost"].Value = false;
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["aktivnost"].Value = true;
                                aa = "1";
                            }

                            string sql = "UPDATE grupa SET aktivnost='" + aa + "' WHERE id_grupa='" + dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString() + "'";
                            classSQL.update(sql);

                            classSQL.update("UPDATE grupa SET editirano='1'");
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
                            if (Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["dodatak"].Value))
                            {
                                dgv.Rows[e.RowIndex].Cells["dodatak"].Value = false;
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["dodatak"].Value = true;
                                aa = "1";
                            }

                            string sql = "UPDATE grupa SET is_dodatak = '" + aa + "' WHERE id_grupa='" + dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString() + "'";
                            classSQL.update(sql);

                            classSQL.update("UPDATE grupa SET editirano='1'");
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
                            if (Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["polpol"].Value))
                            {
                                dgv.Rows[e.RowIndex].Cells["polpol"].Value = false;
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["polpol"].Value = true;
                                aa = "1";
                            }

                            string sql = "UPDATE grupa SET is_polpol = '" + aa + "' WHERE id_grupa='" + dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString() + "'";
                            classSQL.update(sql);

                            classSQL.update("UPDATE grupa SET editirano='1'");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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