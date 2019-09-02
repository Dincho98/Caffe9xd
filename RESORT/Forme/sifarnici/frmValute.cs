using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme.sifarnici
{
    public partial class frmValute : Form
    {
        public frmValute()
        {
            InitializeComponent();
        }

        private DataSet DSsk = new DataSet();
        private DataTable DTBojeForme;

        private void frmValute_Load(object sender, EventArgs e)
        {
            AlterValute();
            SetDgv();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
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

        private void SetDgv()
        {
            if (dgvSk.Rows.Count != 0)
            {
                dgvSk.Rows.Clear();
            }

            DataTable DT = RemoteDB.select("SELECT * FROM valute", "valute").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgvSk.Rows.Add(
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DT.Rows[i]["ime_valute"].ToString(),
                    DT.Rows[i]["puni_naziv"].ToString(),
                    DT.Rows[i]["tecaj"].ToString(),
                    DT.Rows[i]["paritet"].ToString(),
                    DT.Rows[i]["id_valuta"].ToString());
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (txtNaziv.Text.Trim() == "")
            {
                MessageBox.Show("Niste pravilno upisali naziv valute.");
                return;
            }

            decimal dec_parse;
            if (!Decimal.TryParse(txtIznos.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa iznosa tečaja.", "Greška");
                return;
            }

            if (!Decimal.TryParse(txtParitet.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa iznosa pariteta.", "Greška");
                return;
            }

            provjera_sql(RemoteDB.insert("INSERT INTO valute (sifra,naziv,ime_valute,puni_naziv,tecaj,paritet) VALUES" +
                "('" + txtSifra.Text + "','" + txtSkraceniNaziv.Text + "','" + txtNaziv.Text + "','" + txtPuniNaziv.Text + "', " +
                "'" + txtIznos.Text + "','" + txtParitet.Text + "')"));
            SetDgv();
        }

        private void dgvSk_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSk.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE valute SET ime_valute='" + dgvSk.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString() + "' WHERE id_valuta='" + dgvSk.Rows[e.RowIndex].Cells["id_valuta"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgvSk.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = "UPDATE valute SET naziv='" + dgvSk.Rows[e.RowIndex].Cells["Skraceni_naziv"].FormattedValue.ToString() + "' WHERE id_valuta='" + dgvSk.Rows[e.RowIndex].Cells["id_valuta"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgvSk.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    decimal dec = 0;
                    if (String.IsNullOrEmpty(dgvSk.Rows[e.RowIndex].Cells["tecaj"].FormattedValue.ToString()))
                    {
                        dgvSk.Rows[e.RowIndex].Cells["tecaj"].Value = "0";
                        MessageBox.Show("Greška, kod unosa tečaja.");
                        return;
                    }

                    string sql = "UPDATE valute SET tecaj='" + dgvSk.Rows[e.RowIndex].Cells["tecaj"].FormattedValue.ToString() + "' WHERE id_valuta='" + dgvSk.Rows[e.RowIndex].Cells["id_valuta"].FormattedValue.ToString() + "'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmStopePoreza_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNovo.Select();
        }

        private static void AlterValute()
        {
            DataTable dt = RemoteDB.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            string provjeri_col_type = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                " table_name='valute' AND column_name ='sifra'";
            DataTable DTcoltype = RemoteDB.select(provjeri_col_type, "valute").Tables[0];

            if (DTcoltype.Rows.Count < 1)
            {
                try
                {
                    string sql = "ALTER TABLE valute ADD COLUMN sifra character(3); " +
                        "ALTER TABLE valute ADD COLUMN naziv character(3); " +
                        "ALTER TABLE valute ADD COLUMN puni_naziv character varying(100); " +
                        "ALTER TABLE valute ADD COLUMN paritet numeric; ";
                    RemoteDB.update(sql);

                    sql = "UPDATE valute SET sifra='978',naziv='EUR',puni_naziv='Euro',paritet='1'" +
                        " WHERE ime_valute='978 EUR'";
                    RemoteDB.update(sql);
                    sql = "UPDATE valute SET sifra='840',naziv='USD',puni_naziv='Američki Dolar',paritet='1'" +
                        " WHERE ime_valute='840 USD'";
                    RemoteDB.update(sql);
                    sql = "UPDATE valute SET sifra='348',naziv='HUF',puni_naziv='Mađarska forinta',paritet='100'" +
                        " WHERE ime_valute='348 HUF'";
                    RemoteDB.update(sql);
                    sql = "UPDATE valute SET sifra='756',naziv='CHF',puni_naziv='Švicarski franak',paritet='1'" +
                        " WHERE ime_valute='756 CHF'";
                    RemoteDB.update(sql);

                    sql = "UPDATE valute SET sifra='1',tecaj='1',naziv='HR',puni_naziv='Kune',paritet='1'" +
                    " WHERE id_valuta='1'";
                    RemoteDB.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}