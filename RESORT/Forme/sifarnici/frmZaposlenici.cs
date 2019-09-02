using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmZaposlenici : Form
    {
        public frmZaposlenici()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;

        private void frmSobe_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

            DataTable DTtip_sobe = RemoteDB.select("SELECT * FROM grad", "grad").Tables[0];
            cbGrad.DataSource = DTtip_sobe;
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            DataTable DTdopustenja = RemoteDB.select("SELECT * FROM dopustenja", "dopustenja").Tables[0];
            cbDopustenje.DataSource = DTdopustenja;
            cbDopustenje.DisplayMember = "naziv";
            cbDopustenje.ValueMember = "id_dopustenje";

            DataTable DT_zemlja = RemoteDB.select("SELECT * FROM dopustenja", "dopustenja").Tables[0];
            DataTable DTSK = new DataTable("dopustenje");
            DTSK.Columns.Add("id_dopustenje", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id_dopustenje"], DT_zemlja.Rows[i]["naziv"]);
            }

            dopustenje.DataSource = DTSK;
            dopustenje.DataPropertyName = "naziv";
            dopustenje.DisplayMember = "naziv";
            dopustenje.HeaderText = "naziv";
            dopustenje.Name = "naziv";
            dopustenje.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            dopustenje.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            dopustenje.ValueMember = "id_dopustenje";

            DataTable DT_grad = RemoteDB.select("SELECT * FROM grad", "grad").Tables[0];
            DataTable DTg = new DataTable("grad");
            DTg.Columns.Add("id_grad", typeof(string));
            DTg.Columns.Add("grad", typeof(string));
            for (int i = 0; i < DT_grad.Rows.Count; i++)
            {
                DTg.Rows.Add(DT_grad.Rows[i]["id_grad"], DT_grad.Rows[i]["grad"]);
            }

            grad.DataSource = DTg;
            grad.DataPropertyName = "grad";
            grad.DisplayMember = "grad";
            grad.HeaderText = "grad";
            grad.Name = "grad";
            grad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            grad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            grad.ValueMember = "id_grad";

            Set();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void Set()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM zaposlenici ORDER BY ime", "zaposlenici").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivan"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(
                    DT.Rows[i]["ime"].ToString(),
                    DT.Rows[i]["prezime"].ToString(),
                    DT.Rows[i]["id_grad"].ToString(),
                    DT.Rows[i]["adresa"].ToString(),
                    DT.Rows[i]["id_dopustenje"].ToString(),
                    DT.Rows[i]["oib"].ToString(),
                    DT.Rows[i]["tel"].ToString(),
                    DT.Rows[i]["mob"].ToString(),
                    DT.Rows[i]["email"].ToString(),
                    DT.Rows[i]["zaporka"].ToString(),
                    DT.Rows[i]["datum_rodenja"].ToString(),
                    b,
                    DT.Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unosom ovog zaposlenika nemate više mogučnosti ogrisati istog.\r\nDali ste sigurni da želite unjeti novog djelatnika?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                decimal dec = 0;
                if (String.IsNullOrEmpty(txtIme.Text))
                {
                    MessageBox.Show("Greška, niste upisali pravilno ime.");
                    return;
                }

                if (txtPrezime.Text == "")
                {
                    MessageBox.Show("Greška, niste upisali pravilno prezime.");
                    return;
                }

                if (txtZaporka.Text == "")
                {
                    MessageBox.Show("Greška, niste upisali pravilno zaporku.");
                    return;
                }

                if (!Decimal.TryParse(txtOib.Text, out dec))
                {
                    MessageBox.Show("Greška kod upisa oib-a.", "Greška");
                    return;
                }

                string sql = "INSERT INTO zaposlenici (ime,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka) VALUES (" +
                    "'" + txtIme.Text + "'," +
                    "'" + txtPrezime.Text + "'," +
                    "'" + cbGrad.SelectedValue + "'," +
                    "'" + txtAdresa.Text + "'," +
                    "'" + cbDopustenje.SelectedValue + "'," +
                    "'" + txtOib.Text + "'," +
                    "'" + txtTel.Text + "'," +
                    "'" + dtpRodenje.Value + "'," +
                    "'" + txtEmail.Text + "'," +
                    "'" + txtMob.Text + "'," +
                    "'1'," +
                    "'" + txtZaporka.Text + "'" +
                    ")";
                RemoteDB.insert(sql);

                Set();

                txtAdresa.Text = "";
                txtEmail.Text = "";
                txtIme.Text = "";
                txtPrezime.Text = "";
                txtMob.Text = "";
                txtOib.Text = "";
                txtZaporka.Text = "";
                txtTel.Text = "";

                MessageBox.Show("Spremljno.");
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET ime='" + dgv.Rows[e.RowIndex].Cells["ime"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
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
                    string sql = "UPDATE zaposlenici SET prezime='" + dgv.Rows[e.RowIndex].Cells["prezime"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
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
                    string sql = "UPDATE zaposlenici SET id_grad='" + dgv.Rows[e.RowIndex].Cells[2].Value + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
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
                    string sql = "UPDATE zaposlenici SET adresa='" + dgv.Rows[e.RowIndex].Cells["adresa"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
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
                    string sql = "UPDATE zaposlenici SET id_dopustenje='" + dgv.Rows[e.RowIndex].Cells[4].Value + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET oib='" + dgv.Rows[e.RowIndex].Cells["oib"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET tel='" + dgv.Rows[e.RowIndex].Cells["tel"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET mob='" + dgv.Rows[e.RowIndex].Cells["mob"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 8)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET email='" + dgv.Rows[e.RowIndex].Cells["email"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 9)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET zaporka='" + dgv.Rows[e.RowIndex].Cells["zaporka"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 10)
            {
                try
                {
                    string sql = "UPDATE zaposlenici SET datum_rodenja='" + dgv.Rows[e.RowIndex].Cells["dat_rod"].FormattedValue.ToString() + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 11)
            {
                try
                {
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE zaposlenici SET aktivan='" + aa + "' WHERE id_zaposlenik='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmSobe_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}