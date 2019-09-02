using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmOtpremnice : Form
    {
        public bool confirmed = false;
        public List<int> idList = new List<int>();

        public frmOtpremnice()
        {
            InitializeComponent();
        }

        private void frmOtpremnicaStavke_Load(object sender, EventArgs e)
        {
            SetStartDate();
            FillGrid();
        }

        private void FillGrid()
        {
            string query = $@"SELECT
	                            otpremnice.broj_otpremnice,
	                            partners.ime_tvrtke,
	                            otpremnice.datum,
	                            otpremnice.ukupno,
                                case when otpremnice.naplaceno_fakturom = 2 then 'Naplačeno'
					                 when otpremnice.naplaceno_fakturom = 1 then 'Nenaplačeno'
                                     when otpremnice.naplaceno_fakturom = 0 then 'Obrisano' end as naplaceno
                            FROM otpremnice
                            JOIN partners ON otpremnice.osoba_partner = partners.id_partner
                            WHERE otpremnice.osoba_partner = {Properties.Settings.Default.id_partner} AND otpremnice.datum >= '{dtpFrom.Value.ToString("dd-MM-yyyy 00:00:00")}' AND otpremnice.datum <= '{dtpTo.Value.ToString("dd-MM-yyyy 23:59:59")}' AND otpremnice.naplaceno_fakturom = 1
                            ORDER BY otpremnice.id_otpremnica ASC;";
            DataTable dt = classSQL.select(query, "otpremnice").Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                dataGridView.Rows.Add();
                int br = dataGridView.Rows.Count - 1;
                string naplaceno = row["naplaceno"].ToString();
                if (naplaceno == "Naplačeno")
                    dataGridView.Rows[br].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (naplaceno == "Nenaplačeno")
                    dataGridView.Rows[br].DefaultCellStyle.BackColor = Color.Salmon;
                else if (naplaceno == "Obrisano")
                    dataGridView.Rows[br].DefaultCellStyle.BackColor = Color.Silver;
                dataGridView.Rows[br].Cells[0].Value = dataGridView.Rows.Count;
                dataGridView.Rows[br].Cells["sifra"].Value = row["broj_otpremnice"].ToString();
                dataGridView.Rows[br].Cells["partner"].Value = row["ime_tvrtke"].ToString();
                dataGridView.Rows[br].Cells["datum"].Value = row["datum"].ToString();
                dataGridView.Rows[br].Cells["ukupno"].Value = row["ukupno"].ToString() + " kn";
                dataGridView.Rows[br].Cells["naplaceno"].Value = row["naplaceno"].ToString();
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            ClearGrid();
            FillGrid();
        }

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[5];
                bool result = (chk.Value == null ? false : (bool)chk.Value);
                if (result)
                    idList.Add(Convert.ToInt32(row.Cells[0].Value.ToString()));
            }
            confirmed = true;
            Close();
        }

        private void SetStartDate()
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
        }

        private void ClearGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
        }
    }
}
