using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class probe : Form
    {
        public probe()
        {
            InitializeComponent();
        }

        private void probe_Load(object sender, EventArgs e)
        {
            //classSQL.insert("INSERT INTO money (iznos) values ('3,55')");

            //dataGridView1.DataSource = classSQL.select("SELECT * FROM money", "money").Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql5 = "SELECT * FROM roba_prodaja";
            DataTable DT5 = classSQL.select(sql5, "roba_prodaja").Tables[0];

            for (int i = 0; i < DT5.Rows.Count; i++)
            {
                //string mpc = DT5.Rows[i]["mpc"].ToString();
                //double postotak = Convert.ToDouble("1," + (Convert.ToDouble(DT5.Rows[i]["porez"].ToString()) + Convert.ToDouble(DT5.Rows[i]["porez_potrosnja"].ToString())));
                //string vpc = Convert.ToString(Convert.ToDouble(DT5.Rows[i]["mpc"].ToString()) / postotak);
                string sql6 = "UPDATE roba SET porez_potrosnja='" + DT5.Rows[i]["porez_potrosnja"].ToString() + "' WHERE sifra='" + DT5.Rows[i]["sifra"].ToString() + "'";
                classSQL.update(sql6);
            }

            string sql = "SELECT * FROM roba";
            DataTable DT = classSQL.select(sql, "roba").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string mpc = DT.Rows[i]["mpc"].ToString();
                double postotak = Convert.ToDouble("1," + (Convert.ToDouble(DT.Rows[i]["porez"].ToString()) + Convert.ToDouble(DT.Rows[i]["porez_potrosnja"].ToString())));
                string vpc = Convert.ToString(Convert.ToDouble(DT.Rows[i]["mpc"].ToString()) / postotak);
                string sql1 = "UPDATE roba SET vpc='" + vpc.Replace(",", ".") + "' WHERE id_roba='" + DT.Rows[i]["id_roba"].ToString() + "'";
                classSQL.update(sql1);
            }

            string sql7 = "SELECT * FROM roba";
            DataTable DT7 = classSQL.select(sql7, "roba").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string vpc = DT.Rows[i]["vpc"].ToString();
                string sql1 = "UPDATE roba_prodaja SET vpc='" + vpc.Replace(",", ".") + "' WHERE sifra='" + DT.Rows[i]["sifra"].ToString() + "'";
                classSQL.update(sql1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 h = new Form1();
            h.ShowDialog();
        }
    }
}