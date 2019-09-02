using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPromocijePostavke : Form
    {
        private int id_promocije1 = 0;
        private int id_promocije2 = 0;
        private int id_promocije3 = 0;
        private int id_promocije4 = 0;
        private int id_promocije5 = 0;
        private int id_promocije6 = 0;

        private bool edit_promocija1 = false;
        private bool edit_promocija2 = false;
        private bool edit_promocija3 = false;
        private bool edit_promocija4 = false;
        private bool edit_promocija5 = false;
        private bool edit_promocija6 = false;

        public frmPromocijePostavke()
        {
            InitializeComponent();
        }

        private void frmPromocijePostavke_Load(object sender, EventArgs e)
        {
            SetDgv("1&2=3");
            SetDgv("1");
            SetDgv("1&2");
            SetDgv("1&2&3");
            SetDgv("a&a=a");
            SetDgv("a=a");

            setPopustKodSljedeceKupovine();

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        //........................................Promocija 1...............................................
        private void SetDgv(string nacin)
        {
            DataTable DTpromo;
            string sql = "";

            if (nacin == "1&2=3")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='1&2=3'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv1.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv1.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["artikl1"].ToString(),
                        DTpromo.Rows[i]["artikl2"].ToString(),
                        DTpromo.Rows[i]["artikl3"].ToString(),
                        DTpromo.Rows[i]["popust3"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
            else if (nacin == "1")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='1'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv2.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv2.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["artikl1"].ToString(),
                        DTpromo.Rows[i]["popust1"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
            else if (nacin == "1&2")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='1&2'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv3.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv3.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["artikl1"].ToString(),
                        DTpromo.Rows[i]["artikl2"].ToString(),
                        DTpromo.Rows[i]["popust1"].ToString(),
                        DTpromo.Rows[i]["popust2"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
            else if (nacin == "1&2&3")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='1&2&3'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv4.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv4.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["artikl1"].ToString(),
                        DTpromo.Rows[i]["artikl2"].ToString(),
                        DTpromo.Rows[i]["artikl3"].ToString(),
                        DTpromo.Rows[i]["popust1"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
            else if (nacin == "a+a=a")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='a+a=a'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv5.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv5.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["popust3"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
            else if (nacin == "a=a")
            {
                sql = "SELECT * FROM promocije WHERE nacin1='a=a'";
                DTpromo = classSQL.select(sql, "promocije").Tables[0];
                dgv6.Rows.Clear();
                for (int i = 0; i < DTpromo.Rows.Count; i++)
                {
                    dgv6.Rows.Add(
                        DTpromo.Rows[i]["naziv"].ToString(),
                        DTpromo.Rows[i]["od_datuma"].ToString(),
                        DTpromo.Rows[i]["do_datuma"].ToString(),
                        DTpromo.Rows[i]["popust3"].ToString(),
                        "Uredi",
                        "Obriši",
                        DTpromo.Rows[i]["id_promocija"].ToString()
                        );
                }
            }
        }

        private void btnSpremi1_Click(object sender, EventArgs e)
        {
            if (txtImePromocije1.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (txtArtikl_A1.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (txtArtikl_B1.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (txtArtikl_C1.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (edit_promocija1 == true)
            {
                UpdatePromocija1();
                return;
            }

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,artikl1,artikl2,artikl3,popust3,nacin1) VALUES (" +
                "'" + txtImePromocije1.Text + "'," +
                "'" + dtpOD1.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpDO1.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + txtArtikl_A1.Text + "'," +
                "'" + txtArtikl_B1.Text + "'," +
                "'" + txtArtikl_C1.Text + "'," +
                "'" + nuPopust1.Value.ToString() + "'," +
                "'1&2=3'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija1 = false;
            DelField();
            SetDgv("1&2=3");
            MessageBox.Show("Spremljeno");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                UrediPromociju1();
            }

            if (e.ColumnIndex == 8)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv1.CurrentRow.Cells["id"].FormattedValue.ToString() + "'"));
                    dgv1.Rows.Remove(dgv1.CurrentRow);
                }
            }
        }

        private void UrediPromociju1()
        {
            edit_promocija1 = true;
            txtImePromocije1.Text = dgv1.CurrentRow.Cells["ime"].FormattedValue.ToString();
            dtpOD1.Value = Convert.ToDateTime(dgv1.CurrentRow.Cells["trajeod"].FormattedValue.ToString());
            dtpDO1.Value = Convert.ToDateTime(dgv1.CurrentRow.Cells["traje_do"].FormattedValue.ToString());
            txtArtikl_A1.Text = dgv1.CurrentRow.Cells["artikla"].FormattedValue.ToString();
            txtArtikl_B1.Text = dgv1.CurrentRow.Cells["artiklb"].FormattedValue.ToString();
            txtArtikl_C1.Text = dgv1.CurrentRow.Cells["artiklc"].FormattedValue.ToString();
            nuPopust1.Value = Convert.ToInt16(dgv1.CurrentRow.Cells["popust"].FormattedValue.ToString());
            id_promocije1 = Convert.ToInt16(dgv1.CurrentRow.Cells["id"].FormattedValue.ToString());
        }

        private void UpdatePromocija1()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtImePromocije1.Text + "'," +
            " od_datuma='" + dtpOD1.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpDO1.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " artikl1='" + txtArtikl_A1.Text + "'," +
            " artikl2='" + txtArtikl_B1.Text + "'," +
            " artikl3='" + txtArtikl_C1.Text + "'," +
            " popust3='" + nuPopust1.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije1 + "'";
            classSQL.update(sql);
            edit_promocija1 = false;
            DelField();
            SetDgv("1&2=3");
            MessageBox.Show("Spremljeno");
        }

        private void DelField()
        {
            txtImePromocije1.Text = "";
            txtArtikl_A1.Text = "";
            txtArtikl_B1.Text = "";
            txtArtikl_C1.Text = "";
        }

        //........................................Promocija 2...............................................

        private void btnSpremi2_Click(object sender, EventArgs e)
        {
            if (txtIme2.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (txtArtikl2.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (edit_promocija2 == true)
            {
                UpdatePromocija2();
                return;
            }

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,artikl1,popust1,nacin1) VALUES (" +
                "'" + txtIme2.Text + "'," +
                "'" + dtpTrajeOd.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpTrajeDo.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + txtArtikl2.Text + "'," +
                "'" + nuPopust2.Value.ToString() + "'," +
                "'1'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija2 = false;
            DelField2();
            SetDgv("1");
            MessageBox.Show("Spremljeno");
        }

        private void UpdatePromocija2()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtIme2.Text + "'," +
            " od_datuma='" + dtpTrajeOd.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpTrajeDo.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " artikl1='" + txtArtikl2.Text + "'," +
            " popust3='" + nuPopust2.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije2 + "'";
            classSQL.update(sql);
            edit_promocija2 = false;
            DelField2();
            SetDgv("1");
            MessageBox.Show("Spremljeno");
        }

        private void DelField2()
        {
            txtIme2.Text = "";
            txtArtikl2.Text = "";
        }

        private void dgv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                UrediPromociju2();
            }

            if (e.ColumnIndex == 6)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv2.CurrentRow.Cells[7].FormattedValue.ToString() + "'"));
                    dgv2.Rows.Remove(dgv2.CurrentRow);
                }
            }
        }

        private void UrediPromociju2()
        {
            edit_promocija2 = true;
            txtIme2.Text = dgv2.CurrentRow.Cells[0].FormattedValue.ToString();
            dtpTrajeOd.Value = Convert.ToDateTime(dgv2.CurrentRow.Cells[1].FormattedValue.ToString());
            dtpTrajeDo.Value = Convert.ToDateTime(dgv2.CurrentRow.Cells[2].FormattedValue.ToString());
            txtArtikl2.Text = dgv2.CurrentRow.Cells[3].FormattedValue.ToString();
            nuPopust2.Value = Convert.ToInt16(dgv2.CurrentRow.Cells[4].FormattedValue.ToString());
            id_promocije2 = Convert.ToInt16(dgv2.CurrentRow.Cells[7].FormattedValue.ToString());
        }

        //.........................................Promocije 3 .............................................

        private void btnSpremi3_Click(object sender, EventArgs e)
        {
            if (txtImePromocije3.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (txtAriklA3.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (txtArtiklB3.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (edit_promocija3 == true)
            {
                UpdatePromocija3();
                SetDgv("1&2");
                return;
            }

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,artikl1,artikl2,popust1,popust2,nacin1) VALUES (" +
                "'" + txtImePromocije3.Text + "'," +
                "'" + dtpOD3.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpDO3.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + txtAriklA3.Text + "'," +
                "'" + txtArtiklB3.Text + "'," +
                "'" + nuPopustA3.Value.ToString() + "'," +
                "'" + nuPopustB3.Value.ToString() + "'," +
                "'1&2'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija3 = false;
            DelField3();
            SetDgv("1&2");
            MessageBox.Show("Spremljeno");
        }

        private void DelField3()
        {
            txtImePromocije3.Text = "";
            txtAriklA3.Text = "";
            txtArtiklB3.Text = "";
        }

        private void UpdatePromocija3()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtImePromocije3.Text + "'," +
            " od_datuma='" + dtpOD3.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpDO3.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " artikl1='" + txtAriklA3.Text + "'," +
            " artikl2='" + txtArtiklB3.Text + "'," +
            " popust1='" + nuPopustA3.Value.ToString() + "'," +
            " popust2='" + nuPopustB3.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije3 + "'";
            provjera_sql(classSQL.update(sql));
            edit_promocija3 = false;
            DelField3();
            MessageBox.Show("Spremljeno");
        }

        private void dgv3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                UrediPromociju3();
            }

            if (e.ColumnIndex == 8)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv3.CurrentRow.Cells[9].FormattedValue.ToString() + "'"));
                    dgv3.Rows.Remove(dgv3.CurrentRow);
                }
            }
        }

        private void UrediPromociju3()
        {
            edit_promocija3 = true;
            txtImePromocije3.Text = dgv3.CurrentRow.Cells[0].FormattedValue.ToString();
            dtpOD3.Value = Convert.ToDateTime(dgv3.CurrentRow.Cells[1].FormattedValue.ToString());
            dtpDO3.Value = Convert.ToDateTime(dgv3.CurrentRow.Cells[2].FormattedValue.ToString());
            txtAriklA3.Text = dgv3.CurrentRow.Cells[3].FormattedValue.ToString();
            txtArtiklB3.Text = dgv3.CurrentRow.Cells[4].FormattedValue.ToString();
            nuPopustA3.Value = Convert.ToInt16(dgv3.CurrentRow.Cells[5].FormattedValue.ToString());
            nuPopustB3.Value = Convert.ToInt16(dgv3.CurrentRow.Cells[6].FormattedValue.ToString());
            id_promocije3 = Convert.ToInt16(dgv3.CurrentRow.Cells[9].FormattedValue.ToString());
        }

        //........................................PROMOCIJA 4............................................

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (txtNaslov4.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (txtArtiklA4.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (txtArtiklB4.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (txtArtiklC4.Text == "")
            {
                MessageBox.Show("Niste popunili polja za artikle.", "Greška");
                return;
            }

            if (edit_promocija4 == true)
            {
                UpdatePromocija4();
                SetDgv("1&2&3");
                return;
            }

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,artikl1,artikl2,artikl3,popust1,popust2,popust3,nacin1) VALUES (" +
                "'" + txtNaslov4.Text + "'," +
                "'" + dtpOD4.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpDO4.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + txtArtiklA4.Text + "'," +
                "'" + txtArtiklB4.Text + "'," +
                "'" + txtArtiklC4.Text + "'," +
                "'" + nuPopust4.Value.ToString() + "'," +
                "'" + nuPopust4.Value.ToString() + "'," +
                "'" + nuPopust4.Value.ToString() + "'," +
                "'1&2&3'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija4 = false;
            DelField4();
            SetDgv("1&2&3");
            MessageBox.Show("Spremljeno");
        }

        private void DelField4()
        {
            txtNaslov4.Text = "";
            txtArtiklA4.Text = "";
            txtArtiklA4.Text = "";
            txtArtiklA4.Text = "";
        }

        private void UpdatePromocija4()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtNaslov4.Text + "'," +
            " od_datuma='" + dtpOD4.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpDO4.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " artikl1='" + txtArtiklA4.Text + "'," +
            " artikl2='" + txtArtiklB4.Text + "'," +
            " artikl3='" + txtArtiklC4.Text + "'," +
            " popust1='" + nuPopust4.Value.ToString() + "'," +
            " popust2='" + nuPopust4.Value.ToString() + "'," +
            " popust3='" + nuPopust4.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije4 + "'";
            provjera_sql(classSQL.update(sql));
            edit_promocija4 = false;
            DelField4();
            MessageBox.Show("Spremljeno");
        }

        private void dgv4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                UrediPromociju4();
            }

            if (e.ColumnIndex == 8)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv4.CurrentRow.Cells[9].FormattedValue.ToString() + "'"));
                    dgv4.Rows.Remove(dgv4.CurrentRow);
                }
            }
        }

        private void UrediPromociju4()
        {
            edit_promocija4 = true;
            txtNaslov4.Text = dgv4.CurrentRow.Cells[0].FormattedValue.ToString();
            dtpOD3.Value = Convert.ToDateTime(dgv4.CurrentRow.Cells[1].FormattedValue.ToString());
            dtpDO3.Value = Convert.ToDateTime(dgv4.CurrentRow.Cells[2].FormattedValue.ToString());
            txtArtiklA4.Text = dgv4.CurrentRow.Cells[3].FormattedValue.ToString();
            txtArtiklB4.Text = dgv4.CurrentRow.Cells[4].FormattedValue.ToString();
            txtArtiklC4.Text = dgv4.CurrentRow.Cells[5].FormattedValue.ToString();
            nuPopust4.Value = Convert.ToInt16(dgv4.CurrentRow.Cells[6].FormattedValue.ToString());
            id_promocije4 = Convert.ToInt16(dgv4.CurrentRow.Cells[9].FormattedValue.ToString());
        }

        //........................................PROMOCIJA 5............................................

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNaslov5.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (edit_promocija5 == true)
            {
                UpdatePromocija5();
                SetDgv("a+a=a");
                return;
            }

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,popust3,nacin1) VALUES (" +
                "'" + txtNaslov5.Text + "'," +
                "'" + dtpOD5.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpDO5.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + nuPopust5.Value.ToString() + "'," +
                "'a+a=a'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija5 = false;
            DelField5();
            SetDgv("a+a=a");
            MessageBox.Show("Spremljeno");
        }

        private void DelField5()
        {
            txtNaslov5.Text = "";
        }

        private void UpdatePromocija5()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtNaslov5.Text + "'," +
            " od_datuma='" + dtpOD5.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpDO5.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " popust3='" + nuPopust5.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije5 + "'";
            provjera_sql(classSQL.update(sql));
            edit_promocija5 = false;
            DelField5();
            MessageBox.Show("Spremljeno");
        }

        private void dgv5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                UrediPromociju5();
            }

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv5.CurrentRow.Cells[6].FormattedValue.ToString() + "'"));
                    dgv5.Rows.Remove(dgv5.CurrentRow);
                }
            }
        }

        private void UrediPromociju5()
        {
            edit_promocija5 = true;
            txtNaslov5.Text = dgv5.CurrentRow.Cells[0].FormattedValue.ToString();
            dtpOD5.Value = Convert.ToDateTime(dgv5.CurrentRow.Cells[1].FormattedValue.ToString());
            dtpDO5.Value = Convert.ToDateTime(dgv5.CurrentRow.Cells[2].FormattedValue.ToString());
            nuPopust5.Value = Convert.ToInt16(dgv5.CurrentRow.Cells[3].FormattedValue.ToString());
            id_promocije5 = Convert.ToInt16(dgv5.CurrentRow.Cells[6].FormattedValue.ToString());
        }

        //..................................PROMOCIJA 6................................................

        private void btnSpremi6_Click(object sender, EventArgs e)
        {
            if (txtNaslov6.Text == "")
            {
                MessageBox.Show("Niste upisali naslov.", "Greška");
                return;
            }

            if (edit_promocija6 == true)
            {
                UpdatePromocija6();
                SetDgv("a=a");
                return;
            }

            provjera5_6(dtpOD6.Value.ToString("MM.dd.yyyy H:mm:ss"), dtpDO6.Value.ToString("MM.dd.yyyy H:mm:ss"), "a=a");

            string sql = "INSERT INTO promocije (naziv,od_datuma,do_datuma,popust3,nacin1) VALUES (" +
                "'" + txtNaslov6.Text + "'," +
                "'" + dtpOD6.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + dtpDO6.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
                "'" + nuPopust6.Value.ToString() + "'," +
                "'a=a'" +
                ")";
            provjera_sql(classSQL.insert(sql));
            edit_promocija6 = false;
            DelField6();
            SetDgv("a=a");
            MessageBox.Show("Spremljeno");
        }

        private void DelField6()
        {
            txtNaslov6.Text = "";
        }

        private void UpdatePromocija6()
        {
            string sql = "UPDATE promocije SET " +
            " naziv='" + txtNaslov6.Text + "'," +
            " od_datuma='" + dtpOD6.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " do_datuma='" + dtpDO6.Value.ToString("MM.dd.yyyy H:mm:ss") + "'," +
            " popust3='" + nuPopust6.Value.ToString() + "'" +
            " WHERE id_promocija='" + id_promocije6 + "'";
            provjera_sql(classSQL.update(sql));
            edit_promocija6 = false;
            DelField6();
            MessageBox.Show("Spremljeno");
        }

        private void dgv6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                UrediPromociju6();
            }

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu promociju.\r\nNakon brisanja nema više povratka istog.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM promocije WHERE id_promocija='" + dgv6.CurrentRow.Cells[6].FormattedValue.ToString() + "'"));
                    dgv6.Rows.Remove(dgv6.CurrentRow);
                }
            }
        }

        private void UrediPromociju6()
        {
            edit_promocija6 = true;
            txtNaslov6.Text = dgv6.CurrentRow.Cells[0].FormattedValue.ToString();
            dtpOD6.Value = Convert.ToDateTime(dgv6.CurrentRow.Cells[1].FormattedValue.ToString());
            dtpDO6.Value = Convert.ToDateTime(dgv6.CurrentRow.Cells[2].FormattedValue.ToString());
            nuPopust6.Value = Convert.ToInt16(dgv6.CurrentRow.Cells[3].FormattedValue.ToString());
            id_promocije6 = Convert.ToInt16(dgv6.CurrentRow.Cells[6].FormattedValue.ToString());
        }

        private string provjera5_6(string _od, string _do, string nacin)
        {
            //DataTable DTprovjera = classSQL.select("SELECT * FROM promocije WHERE od_datuma=>'" + _od + "' AND do_datuma=<'" + _do + "' AND nacin1='" + nacin + "'", "promocije").Tables[0];
            return "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string active = "";

            if (chbAktivnost.Checked)
            {
                active = "DA";
            }
            else
            {
                active = "NE";
            }

            string sql = "UPDATE promocija1 SET " +
                " ime='" + txtImeNext.Text + "'," +
                " popust='" + nuPopustNext.Value.ToString() + "'," +
                " aktivnost='" + active + "'," +
                " traje_do='" + nuBrojDanaNext.Value.ToString() + "'" +
                "";

            classSQL.update(sql);
        }

        private void setPopustKodSljedeceKupovine()
        {
            DataTable DT = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];

            if (DT.Rows.Count > 0)
            {
                txtImeNext.Text = DT.Rows[0]["ime"].ToString();
                nuPopustNext.Value = Convert.ToInt16(DT.Rows[0]["popust"].ToString());

                if (DT.Rows[0]["aktivnost"].ToString() == "DA")
                {
                    chbAktivnost.Checked = true;
                }
                else
                {
                    chbAktivnost.Checked = false;
                }

                nuBrojDanaNext.Value = Convert.ToInt16(DT.Rows[0]["traje_do"].ToString());
            }
        }
    }
}