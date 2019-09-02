using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmCijene_soba : Form
    {
        public frmCijene_soba()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;
        private bool load = false;

        private void frmCijene_soba_Load(object sender, EventArgs e)
        {
            SetValues();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
            load = true;
            promjena_tecaja();
            SetSobe();
        }

        private void SetSobe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            string ssql = "SELECT " +
                " sobe.naziv_sobe," +
                " R_CijenaSoba.id," +
                " R_CijenaSoba.broj_sobe," +
                " R_CijenaSoba.id_soba," +
                " R_CijenaSoba.id_valuta," +
                " R_CijenaSoba.cijena_nocenja," +
                " R_CijenaSoba.od_datuma," +
                " R_CijenaSoba.do_datuma" +
                " FROM R_CijenaSoba LEFT JOIN sobe ON sobe.id=R_CijenaSoba.id_soba ORDER BY CAST(R_CijenaSoba.broj_sobe AS int) ASC , R_CijenaSoba.od_datuma ";
            DataTable DT = RemoteDB.select(ssql, "R_CijenaSoba").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(Convert.ToInt16(DT.Rows[i]["broj_sobe"].ToString()),
                    DT.Rows[i]["naziv_sobe"].ToString(),
                    DT.Rows[i]["id_valuta"].ToString(),
                    DT.Rows[i]["cijena_nocenja"].ToString(),
                    DT.Rows[i]["od_datuma"].ToString(),
                    DT.Rows[i]["do_datuma"].ToString(),
                    DT.Rows[i]["id_soba"].ToString(),
                    DT.Rows[i]["id"].ToString());
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetValues()
        {
            DataTable DTvaluta = RemoteDB.select("SELECT * FROM valute", "valute").Tables[0];
            DataTable DTSK = new DataTable("tip_sobe");
            DTSK.Columns.Add("id_valuta", typeof(string));
            DTSK.Columns.Add("ime_valute", typeof(string));
            for (int i = 0; i < DTvaluta.Rows.Count; i++)
            {
                DTSK.Rows.Add(DTvaluta.Rows[i]["id_valuta"], DTvaluta.Rows[i]["ime_valute"]);
            }

            valuta.DataSource = DTSK;
            valuta.DataPropertyName = "ime_valute";
            valuta.DisplayMember = "ime_valute";
            valuta.HeaderText = "ime_valute";
            valuta.Name = "ime_valute";
            valuta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            valuta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            valuta.ValueMember = "id_valuta";

            cbValute.DataSource = DTvaluta;
            cbValute.DisplayMember = "ime_valute";
            cbValute.ValueMember = "id_valuta";
            cbValute.SelectedValue = "1";

            DataSet DSs = RemoteDB.select("SELECT id,broj_sobe, naziv_sobe FROM sobe WHERE aktivnost='1'", "sobe");
            DataTable DTsoba = DSs.Tables[0];
            cbSoba.DataSource = DTsoba;
            cbSoba.DisplayMember = "naziv_sobe";
            cbSoba.ValueMember = "id";
            txtBrojSobe.DataBindings.Add("Text", DSs.Tables[0], "broj_sobe");
        }

        private void cbValute_SelectedIndexChanged(object sender, EventArgs e)
        {
            promjena_tecaja();
        }

        private void promjena_tecaja()
        {
            if (load)
            {
                try
                {
                    DataTable DT = RemoteDB.select("SELECT * FROM valute WHERE id_valuta='" + cbValute.SelectedValue + "'", "valute").Tables[0];
                    txtTecaj.Text = DT.Rows.Count > 0 ? DT.Rows[0]["tecaj"].ToString() : "1";

                    decimal dec;
                    if (!decimal.TryParse(txtCijenaSobe.Text, out dec))
                    {
                        txtCijenaSobe.Text = "0";
                    }
                    if (!decimal.TryParse(txtTecaj.Text, out dec))
                    {
                        txtTecaj.Text = "0";
                    }

                    txtPreracunKune.Text = Funkcije.Mat(Convert.ToDecimal(txtCijenaSobe.Text) * Convert.ToDecimal(txtTecaj.Text), 3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtCijenaSobe_TextChanged(object sender, EventArgs e)
        {
            promjena_tecaja();
        }

        private void cbSoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            promjena_tecaja();
        }

        private void btnDodajNaPopis_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtBrojSobe.Text, out dec_parse))
            {
                txtBrojSobe.Text = "0";
                MessageBox.Show("Greška kod upisa broja sobe.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtCijenaSobe.Text, out dec_parse))
            {
                txtCijenaSobe.Text = "0";
                MessageBox.Show("Greška kod upisa cijene za sobu.", "Greška");
                return;
            }

            DateTime datum = Convert.ToDateTime(dtpDO.Value.ToString("yyyy-MM-dd") + " 23:59:59");

            string sql = "INSERT INTO R_CijenaSoba (broj_sobe,id_soba,id_valuta,cijena_nocenja,od_datuma,do_datuma) VALUES (" +
                "'" + txtBrojSobe.Text + "'," +
                "'" + cbSoba.SelectedValue + "'," +
                "'" + cbValute.SelectedValue + "'," +
                "'" + txtCijenaSobe.Text.Replace(",", ".") + "'," +
                "'" + dtpOD.Value.ToString("yyyy-MM-dd") + "'," +
                "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                ")";
            RemoteDB.insert(sql);

            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            string ssql = "SELECT " +
                " sobe.naziv_sobe," +
                " R_CijenaSoba.id," +
                " R_CijenaSoba.broj_sobe," +
                " R_CijenaSoba.id_soba," +
                " R_CijenaSoba.id_valuta," +
                " R_CijenaSoba.cijena_nocenja," +
                " R_CijenaSoba.od_datuma," +
                " R_CijenaSoba.do_datuma" +
                " FROM R_CijenaSoba LEFT JOIN sobe ON sobe.id=R_CijenaSoba.id_soba";
            DataTable DT = RemoteDB.select(ssql, "R_CijenaSoba").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["broj_sobe"].ToString(),
                    DT.Rows[i]["naziv_sobe"].ToString(),
                    DT.Rows[i]["id_valuta"].ToString(),
                    DT.Rows[i]["cijena_nocenja"].ToString(),
                    DT.Rows[i]["od_datuma"].ToString(),
                    DT.Rows[i]["do_datuma"].ToString(),
                    DT.Rows[i]["id_soba"].ToString(),
                    DT.Rows[i]["id"].ToString());
            }
        }

        private void btnObrisiOznacenog_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu cijenu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgv.CurrentRow.Cells["id"].FormattedValue.ToString() != "")
                    {
                        RemoteDB.delete("DELETE FROM R_CijenaSoba WHERE id='" + dgv.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
                    }

                    dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                    MessageBox.Show("Obrisano");
                }
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal dec_parse;

            if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    if (!Decimal.TryParse(dgv.Rows[e.RowIndex].Cells["cijena_nocenja"].FormattedValue.ToString(), out dec_parse))
                    {
                        dgv.Rows[e.RowIndex].Cells["cijena_nocenja"].Value = "0";
                        MessageBox.Show("Greška kod upisa cijene za sobu.", "Greška");
                        return;
                    }

                    string sql = "UPDATE R_CijenaSoba SET cijena_nocenja='" + dgv.Rows[e.RowIndex].Cells["cijena_nocenja"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE R_CijenaSoba SET id_valuta='" + dgv.Rows[e.RowIndex].Cells[2].Value + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = "UPDATE R_CijenaSoba SET od_datuma='" + dgv.Rows[e.RowIndex].Cells["od_datuma"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    string sql = "UPDATE R_CijenaSoba SET do_datuma='" + dgv.Rows[e.RowIndex].Cells["do_datuma"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmCijene_soba_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPromjeniTecaj.Select();
        }

        private void btnPromjeniTecaj_Click(object sender, EventArgs e)
        {
            Forme.sifarnici.frmValute v = new Forme.sifarnici.frmValute();
            v.ShowDialog();
        }

        private void btnSviUnosi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Potvrdom ovog unosa spremate cijene po svim sobama u iznosu od " + txtCijenaSobe.Text + " kn.\r\nDali ste sigurni da želite spremiti ove promjene?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable DT = RemoteDB.select("SELECT * FROM sobe", "sobe").Tables[0];
                RemoteDB.delete("DELETE FROM r_cijenasoba");
                foreach (DataRow row in DT.Rows)
                {
                    string sql = "INSERT INTO r_cijenasoba (cijena_nocenja,od_datuma,do_datuma,id_valuta,broj_sobe,id_soba)" +
                        " VALUES" +
                        " ('" + txtCijenaSobe.Text.Replace(",", ".") + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + DateTime.Now.AddYears(20).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + cbValute.SelectedValue + "'," +
                        "'" + row["broj_sobe"] + "'," +
                        "'" + row["id"] + "'" +
                        ")";
                    RemoteDB.insert(sql);
                }
            }
        }
    }
}