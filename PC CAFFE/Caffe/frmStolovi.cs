using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmStolovi : Form
    {
        public frmStolovi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmStolovi_Load(object sender, EventArgs e)
        {
            SetStol();

            //fill grupe
            DataTable DT_cbGrupa = classSQL.select("SELECT id_ducan,ime_ducana FROM ducan WHERE aktivnost='DA'", "ducan").Tables[0];
            cbPoslovnica.DataSource = DT_cbGrupa;
            cbPoslovnica.DisplayMember = "ime_ducana";
            cbPoslovnica.ValueMember = "id_ducan";

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    //Graphics c = e.Graphics;
        //    //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    //c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void SetStol()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT id_stol,naziv,id_poslovnica,aktivnost  FROM stolovi WHERE id_poslovnica='" + DTpostavke.Rows[0]["default_ducan"].ToString() + "'", "stolovi").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["naziv"].ToString(), DT.Rows[i]["id_poslovnica"].ToString(), b, DT.Rows[i]["id_stol"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivStola.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv stola.");
                return;
            }

            string sql = "INSERT INTO stolovi (naziv,aktivnost,id_poslovnica) VALUES (" +
                "'" + txtnazivStola.Text + "'," +
                "'1'," +
                "'" + cbPoslovnica.SelectedValue.ToString() + "'" +
                ")";
            classSQL.insert(sql);

            SetStol();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE stolovi SET naziv='" + dgv.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString() + "' WHERE id_stol='" + dgv.Rows[e.RowIndex].Cells["id_stol"].FormattedValue.ToString() + "'";
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
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE stolovi SET aktivnost='" + aa + "' WHERE id_stol='" + dgv.Rows[e.RowIndex].Cells["id_stol"].FormattedValue.ToString() + "'";
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