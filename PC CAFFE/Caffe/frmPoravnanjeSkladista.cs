using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPoravnanjeSkladista : Form
    {
        public frmPoravnanjeSkladista()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPoravnanjeSkladista_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            DataTable DT = classSQL.select("SELECT * FROM roba_prodaja ORDER BY naziv ASC", "roba_prodaja").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["sifra"].ToString(), DT.Rows[i]["naziv"].ToString(), DT.Rows[i]["kolicina"].ToString(), DT.Rows[i]["id_roba_prodaja"].ToString());
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE roba_prodaja SET kolicina='" + dgv.Rows[e.RowIndex].Cells["kolicina"].FormattedValue.ToString() + "' WHERE id_roba_prodaja='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
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