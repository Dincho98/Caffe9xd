using System;
using System.Data;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmPopisUnosa : Form
    {
        public frmPopisUnosa()
        {
            InitializeComponent();
        }

        public frmFaktura _FAKTURA { get; set; }
        public string __broj_unosa { get; set; }

        private void frmPopisSoba_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM unos_gosta WHERE odjava is null OR odjava <> '1' ORDER BY CAST(broj AS INT) DESC";
            DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                dgw.Rows.Add(row["broj"].ToString(), row["ime_gosta"].ToString(), row["avans"].ToString(), row["datum_dolaska"].ToString(), row["datum_odlaska"].ToString(), row["ukupno"].ToString(), row["id"].ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _FAKTURA.__broj_unosa = dgw.Rows[e.RowIndex].Cells["broj"].FormattedValue.ToString();
            this.Close();
        }
    }
}