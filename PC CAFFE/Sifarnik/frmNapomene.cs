using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmNapomene : Form
    {
        public frmNapomene()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNapomene_Load(object sender, EventArgs e)
        {
            try
            {
                this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
                FillNapomeneCB();
                getNapomene();
                getNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets data from "napomene" table and populates ComboBox
        /// </summary>
        private void FillNapomeneCB()
        {
            DataTable DTpodgrupe = Global.Database.GetPodgrupe();
            {
                cmbNapomene.ValueMember = "id_podgrupa";
                cmbNapomene.DisplayMember = "naziv";
                cmbNapomene.DataSource = DTpodgrupe;
            }
        }

        private void getNew()
        {
            try
            {
                string sql = @"SELECT COALESCE(MAX(id), 0) zbroj 1 AS id FROM napomene;";
                txtId.Text = classSQL.select(sql, "napomene").Tables[0].Rows[0]["id"].ToString();
                txtNapomena.Text = "";
                chbAktivnost.Checked = false;
                btnSpremi.Tag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getNapomene()
        {
            try
            {
                string sql = @"SELECT id AS [Id]
                                , napomena AS [Napomena]
                                , podgrupa.naziv AS [Podgrupa]
                                , case when aktivno = '1' then 'DA' else 'NE' END AS [Aktivnost]
                                , podgrupa.id_podgrupa
                            FROM napomene 
                            LEFT JOIN podgrupa ON podgrupa.id_podgrupa = napomene.id_podgrupa
                            ORDER BY id ASC;";
                DataSet dsNapomene = new DataSet();
                dsNapomene = classSQL.select(sql, "napomene");
                if (dsNapomene != null && dsNapomene.Tables.Count > 0 && dsNapomene.Tables[0] != null)
                {
                    dgvNapomene.DataSource = dsNapomene.Tables[0];
                    dgvNapomene.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNapomena.Text))
                {
                    MessageBox.Show("Podaci nisu pravilno ispunjeni.");
                    return;
                }

                string sql = $@"UPDATE napomene SET napomena = '{txtNapomena.Text}'
                                , aktivno = '{chbAktivnost.Checked.ToString().ToUpper()}'
                                , editirano = '1'
                                , id_podgrupa = {cmbNapomene.SelectedValue.ToString()}
                            WHERE id = '{txtId.Text}';";

                if (!(bool)btnSpremi.Tag)
                {
                    sql = $@"INSERT INTO napomene (id, napomena, id_podgrupa, aktivno, novo) 
                        VALUES ('{txtId.Text}', '{txtNapomena.Text}' , {cmbNapomene.SelectedValue.ToString()}, '{chbAktivnost.Checked.ToString().ToUpper()}', '1');";
                }

                classSQL.insert(sql);

                getNapomene();
                getNew();

                MessageBox.Show("Spremljeno");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                getNapomene();
                getNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNapomene_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    int id = (int)dgvNapomene.Rows[e.RowIndex].Cells[0].Value;
                    string napomena = dgvNapomene.Rows[e.RowIndex].Cells[1].Value.ToString();
                    bool aktivnost = Convert.ToBoolean(dgvNapomene.Rows[e.RowIndex].Cells[3].Value.ToString().ToUpper() == "DA" ? 1 : 0);

                    txtId.Text = id.ToString();
                    txtNapomena.Text = napomena;
                    cmbNapomene.SelectedValue = dgvNapomene.Rows[e.RowIndex].Cells[4].Value.ToString();
                    chbAktivnost.Checked = aktivnost;
                    btnSpremi.Tag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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