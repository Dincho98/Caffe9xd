using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmKasaPrijava : Form
    {
        public frmKasaPrijava()
        {
            InitializeComponent();
        }

        private bool prijava = false;
        private DataTable DTBojeForme;
        private DataSet DSzaposlenik;

        private void frmKasaPrijava_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            fillComboBox();
            cbBlagajnik.Select();
            txtZaporka.PasswordChar = '*';
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

        private void fillComboBox()
        {
            DSzaposlenik = RemoteDB.select("SELECT id_zaposlenik, ime + ' ' + prezime AS name FROM zaposlenici WHERE aktivan='1' OR aktivan='DA'", "zaposlenici");
            cbBlagajnik.DataSource = DSzaposlenik.Tables[0];
            cbBlagajnik.DisplayMember = "name";
            cbBlagajnik.ValueMember = "id_zaposlenik";
        }

        private void btnUlaz_Click(object sender, EventArgs e)
        {
            DataTable DTzp = RemoteDB.select("SELECT id_zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + cbBlagajnik.SelectedValue.ToString() + "' AND zaporka = '" + txtZaporka.Text.Trim() + "'", "zaposlenici").Tables[0];
            if (DTzp.Rows.Count > 0)
            {
                prijava = true;
                this.Close();

                //var mdiForm = this.MdiParent;
                //var childForm = new frmKasa();
                Properties.Settings.Default.id_zaposlenik = cbBlagajnik.SelectedValue.ToString();
                Properties.Settings.Default.Save();
                //childForm.MainForm = MainForm;
                //childForm.Show();
            }
            else
            {
                MessageBox.Show("Greška.\r\nVaša autorizacija nije valjana.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtZaporka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnUlaz.PerformClick();
            }
        }

        private void cbBlagajnik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtZaporka.Select();
            }
        }

        private void frmKasaPrijava_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prijava == false)
            {
                Application.Exit();
                //e.Cancel = true;
            }
        }
    }
}