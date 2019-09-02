using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmNormativi : Form
    {
        public frmNormativi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTRoba = new DataTable();
        private DataTable DTRobaNormativi = new DataTable();
        private string sifra_dobivena = "";

        private void frmNormativi_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            dgw.Rows.Add();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Replace(" ", "");
            if (propertis_sifra != "")
            {
                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.Text = DTRoba.Rows[0]["sifra"].ToString();
                    lblOartiklu.Text = DTRoba.Rows[0]["naziv"].ToString() + ", \r\nCijena:" + Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString()).ToString("#0.00") + " kn";

                    string sql_normativ = "SELECT " +
                        " caffe_normativ.sifra_normativ," +
                        " roba.naziv," +
                        " roba.mpc," +
                        " caffe_normativ.kolicina," +
                        " caffe_normativ.id_stavka" +
                        " FROM caffe_normativ" +
                        " LEFT JOIN roba ON roba.sifra=caffe_normativ.sifra_normativ WHERE caffe_normativ.sifra='" + propertis_sifra + "'";
                    DTRobaNormativi = classSQL.select(sql_normativ, "caffe_normativ").Tables[0];
                    dgw.Rows.Clear();

                    for (int i = 0; i < DTRobaNormativi.Rows.Count; i++)
                    {
                        dgw.Rows.Add(DTRobaNormativi.Rows[i]["sifra_normativ"].ToString(), DTRobaNormativi.Rows[i]["naziv"].ToString(), DTRobaNormativi.Rows[i]["kolicina"].ToString(), DTRobaNormativi.Rows[i]["mpc"].ToString(), DTRobaNormativi.Rows[i]["id_stavka"].ToString());
                    }
                    dgw.Rows.Add();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtDobiveni_Leave(object sender, EventArgs e)
        {
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                    }
                    else
                    {
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";
                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    sifra_dobivena = DTRoba.Rows[0]["sifra"].ToString();
                    lblOartiklu.Text = DTRoba.Rows[0]["naziv"].ToString() + "\r\nCijena:" + Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString()).ToString("#0.00") + " kn";

                    string sql_normativ = "SELECT " +
                        " caffe_normativ.sifra_normativ," +
                        " roba.naziv," +
                        " roba.mpc," +
                        " caffe_normativ.kolicina," +
                        " caffe_normativ.id_stavka" +
                        " FROM caffe_normativ" +
                        " LEFT JOIN roba ON roba.sifra=caffe_normativ.sifra_normativ WHERE caffe_normativ.sifra='" + sifra_dobivena + "'";
                    DTRobaNormativi = classSQL.select(sql_normativ, "caffe_normativ").Tables[0];
                    dgw.Rows.Clear();

                    for (int i = 0; i < DTRobaNormativi.Rows.Count; i++)
                    {
                        dgw.Rows.Add(DTRobaNormativi.Rows[i]["sifra_normativ"].ToString(), DTRobaNormativi.Rows[i]["naziv"].ToString(), DTRobaNormativi.Rows[i]["kolicina"].ToString(), DTRobaNormativi.Rows[i]["mpc"].ToString(), DTRobaNormativi.Rows[i]["id_stavka"].ToString());
                    }
                    dgw.Rows.Add();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoj artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool SetNormativ(DataGridViewCellEventArgs e)
        {
            if (dgw.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() == "")
            {
                frmRobaTrazi roba = new frmRobaTrazi();
                roba.ShowDialog();

                if (Properties.Settings.Default.id_roba != "")
                {
                    dgw.Rows[e.RowIndex].Cells[0].Value = Properties.Settings.Default.id_roba;
                    DataTable DTart = classSQL.select("SELECT * FROM roba WHERE sifra='" + Properties.Settings.Default.id_roba + "'", "roba").Tables[0];

                    dgw.Rows[e.RowIndex].Cells["naziv"].Value = DTart.Rows[0]["naziv"].ToString();
                    dgw.Rows[e.RowIndex].Cells["kolicina"].Value = "1";
                    dgw.Rows[e.RowIndex].Cells["mpc"].Value = DTart.Rows[0]["mpc"].ToString();

                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[2];
                    dgw.BeginEdit(true);
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                DataTable DTart = classSQL.select("SELECT * FROM roba WHERE sifra='" + dgw.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + "'", "roba").Tables[0];

                if (DTart.Rows.Count > 0)
                {
                    dgw.Rows[e.RowIndex].Cells["naziv"].Value = DTart.Rows[0]["naziv"].ToString();
                    dgw.Rows[e.RowIndex].Cells["kolicina"].Value = "1";
                    dgw.Rows[e.RowIndex].Cells["mpc"].Value = DTart.Rows[0]["mpc"].ToString();

                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[2];
                    dgw.BeginEdit(true);
                    return true;
                }
                else
                {
                    MessageBox.Show("Kriva šifra.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[0];
                    dgw.CurrentCell.Value = "";
                    dgw.BeginEdit(true);
                    return false;
                }
            }
        }

        public class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmNormativi MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 0)
            {
                if (d.CurrentCell.FormattedValue.ToString() == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        d.CurrentCell.Value = Properties.Settings.Default.id_roba;
                    }
                    else
                    {
                        return;
                    }
                }

                d.EndEdit();
                DataTable DTart = classSQL.select("SELECT * FROM roba WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells[0].FormattedValue.ToString() + "'", "roba").Tables[0];

                if (DTart.Rows.Count > 0)
                {
                    dgw.Rows[dgw.CurrentRow.Index].Cells["naziv"].Value = DTart.Rows[0]["naziv"].ToString();
                    dgw.Rows[dgw.CurrentRow.Index].Cells["kolicina"].Value = "1";
                    dgw.Rows[dgw.CurrentRow.Index].Cells["mpc"].Value = DTart.Rows[0]["mpc"].ToString();

                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[2];
                    dgw.BeginEdit(true);
                }
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                int curent = d.CurrentRow.Index;
                if (curent == d.RowCount - 1 && d.Rows[curent].Cells["naziv"].FormattedValue.ToString() != "")
                {
                    d.Rows.Add();
                    d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                    d.BeginEdit(true);
                }
                else if (curent == d.RowCount - 1)
                {
                    d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                    d.BeginEdit(true);
                }
                else
                {
                    d.CurrentCell = dgw.Rows[curent + 1].Cells[0];
                    d.BeginEdit(true);
                }
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 0)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                int curent = d.CurrentRow.Index;

                if (curent == d.RowCount - 1 && d.Rows[curent].Cells["naziv"].FormattedValue.ToString() != "")
                {
                    d.Rows.Add();
                    d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                    d.BeginEdit(true);
                }
                else if (curent == d.RowCount - 1)
                {
                    d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                    d.BeginEdit(true);
                }
                else
                {
                    d.CurrentCell = dgw.Rows[curent + 1].Cells[0];
                    d.BeginEdit(true);
                }
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.CurrentCell.ColumnIndex == 0)
            {
                d.EndEdit();
                DataTable DTart = classSQL.select("SELECT * FROM roba WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells[0].FormattedValue.ToString() + "'", "roba").Tables[0];

                if (DTart.Rows.Count > 0)
                {
                    dgw.Rows[dgw.CurrentRow.Index].Cells["naziv"].Value = DTart.Rows[0]["naziv"].ToString();
                    dgw.Rows[dgw.CurrentRow.Index].Cells["kolicina"].Value = "1";
                    dgw.Rows[dgw.CurrentRow.Index].Cells["mpc"].Value = DTart.Rows[0]["mpc"].ToString();

                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[2];
                    dgw.BeginEdit(true);
                }

                d.CurrentCell = d.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
        }

        private void UpDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;
            if (d.Rows[curent].Cells["naziv"].FormattedValue.ToString() == "" && d.Rows.Count > 1)
            {
                d.Rows.RemoveAt(d.CurrentRow.Index);
            }
            else if (curent == 0)
            {
            }
            else
            {
                d.CurrentCell = d.Rows[d.CurrentRow.Index - 1].Cells[0];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            int curent = d.CurrentRow.Index;

            if (curent == d.RowCount - 1 && d.Rows[curent].Cells["naziv"].FormattedValue.ToString() != "")
            {
                d.Rows.Add();
                d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                d.BeginEdit(true);
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.RowCount - 1].Cells[0];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[curent + 1].Cells[0];
                d.BeginEdit(true);
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2)
            {
                dgw.CurrentRow.Cells[e.ColumnIndex].Selected = true;
                dgw.BeginEdit(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                MessageBox.Show("Krivi unos šifre.", "Greška");
                return;
            }

            if (txtSifra_robe.Text == "")
            {
                DataTable dtt = classSQL.select("SELECT sifra FROM roba WHERE sifra='" + txtSifra_robe.Text + "'", "roba").Tables[0];

                if (dtt.Rows.Count == 0)
                {
                    MessageBox.Show("Krivi unos šifre.", "Greška");
                    return;
                }
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() != "" && dgw.Rows[i].Cells["naziv"].FormattedValue.ToString() != "")
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value == null)
                    {
                        string sql = "INSERT INTO caffe_normativ (sifra,sifra_normativ,kolicina) VALUES ('" + txtSifra_robe.Text + "','" +
                            "" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "','" +
                            "" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "')";
                        provjera_sql(classSQL.insert(sql));
                    }
                    else
                    {
                        string sql = "UPDATE caffe_normativ SET " +
                            " sifra='" + txtSifra_robe.Text + "'," +
                            " sifra_normativ='" + dg(i, "sifra") + "'," +
                            " kolicina='" + dg(i, "kolicina") + "' WHERE id_stavka='" + dg(i, "id_stavka") + "'";
                        provjera_sql(classSQL.update(sql));
                    }

                    //Ako su stavke obrisane ovaj kod ih briše
                    bool nema_u_dgw;
                    for (int j = 0; j < DTRobaNormativi.Rows.Count; j++)
                    {
                        nema_u_dgw = true;
                        for (int k = 0; k < dgw.Rows.Count; k++)
                        {
                            if (dgw.Rows[k].Cells["id_stavka"].FormattedValue.ToString() == DTRobaNormativi.Rows[j]["id_stavka"].ToString())
                            {
                                nema_u_dgw = false;
                                break;
                            }
                        }

                        if (nema_u_dgw == true)
                        {
                            classSQL.delete("DELETE FROM caffe_normativ WHERE id_stavka='" + DTRobaNormativi.Rows[j]["id_stavka"].ToString() + "'");
                        }
                    }
                }
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void dgw_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
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