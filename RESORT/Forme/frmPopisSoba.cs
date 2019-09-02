using System;
using System.Data;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmPopisSoba : Form
    {
        public frmPopisSoba()
        {
            InitializeComponent();
        }

        public frmFaktura _FAKTURA { get; set; }
        public string __broj_sobe { get; set; }

        private void frmPopisSoba_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM sobe ORDER BY CAST(broj_sobe AS int) ASC";
            DataTable DT = RemoteDB.select(sql, "sobe").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                dgw.Rows.Add(row["broj_sobe"].ToString(), row["naziv_sobe"].ToString(), row["broj_lezaja"].ToString(), row["cijena_nocenja"].ToString(), row["id"].ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _FAKTURA.__broj_sobe = dgw.Rows[e.RowIndex].Cells["broj_sobe"].FormattedValue.ToString();
                this.Close();
            }
        }
    }
}