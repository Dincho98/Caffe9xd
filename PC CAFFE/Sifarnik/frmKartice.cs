using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmKartice : Form
    {
        public frmKartice()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmKartice_Load(object sender, EventArgs e)
        {
            try
            {
                getKarticeInGrid();
                this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDodajKarticu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO kartice (naziv, aktivnost) " +
                    "VALUES('" + txtNaziv.Text + "', " +
                    "'1');";
                classSQL.insert(sql);

                getKarticeInGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getKarticeInGrid()
        {
            try
            {
                string sql = "SELECT id, naziv, aktivnost FROM kartice ORDER BY id asc";
                DataSet dsKartice = classSQL.select(sql, "kartice");
                if (dsKartice != null && dsKartice.Tables.Count > 0 && dsKartice.Tables[0] != null && dsKartice.Tables[0].Rows.Count > 0)
                {
                    dgwKartice.DataSource = dsKartice.Tables[0];
                }
                txtNaziv.Text = "";
                txtNaziv.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgwKartice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    string sql = "UPDATE kartice SET naziv = '" + dgwKartice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "', editirano = '1'  WHERE id = '" + dgwKartice.Rows[e.RowIndex].Cells[0].Value + "';";
                    classSQL.update(sql);
                }
                else if (e.ColumnIndex == 2)
                {
                    string sql = "UPDATE kartice SET aktivnost = '" + dgwKartice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "', editirano = '1'  WHERE id = '" + dgwKartice.Rows[e.RowIndex].Cells[0].Value + "';";
                    classSQL.update(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgwKartice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > -1)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgwKartice.Rows[e.RowIndex].Cells[e.ColumnIndex];
                chk.Value = !(bool)chk.Value;
                dgwKartice.EndEdit();

                string sql = "UPDATE kartice SET aktivnost = '" + dgwKartice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "', editirano = '1'  WHERE id = '" + dgwKartice.Rows[e.RowIndex].Cells[0].Value + "';";
                classSQL.update(sql);
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