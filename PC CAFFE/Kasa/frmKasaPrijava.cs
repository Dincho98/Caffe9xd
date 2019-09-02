using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasaPrijava : Form
    {
        public frmKasaPrijava()
        {
            InitializeComponent();
        }

        private bool prijava = false;
        private DataSet DSzaposlenik;
        public frmMenu MainForm { get; set; }

        private void frmKasaPrijava_Load(object sender, EventArgs e)
        {
            fillComboBox();
            DefaultValue();
            cbBlagajnik.Select();
            txtZaporka.PasswordChar = '*';
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            DSzaposlenik = classSQL.select("SELECT id_zaposlenik, ime + ' ' + prezime AS name FROM zaposlenici WHERE aktivan='DA'", "zaposlenici");
            cbBlagajnik.DataSource = DSzaposlenik.Tables[0];
            cbBlagajnik.DisplayMember = "name";
            cbBlagajnik.ValueMember = "id_zaposlenik";
        }

        private void btnUlaz_Click(object sender, EventArgs e)
        {
            Login((int)cbBlagajnik.SelectedValue, txtZaporka.Text.Trim());
        }

        public void Login(int id_zaposlenik, string password) {
            DataTable DTzp = classSQL.select("SELECT id_zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + id_zaposlenik + "' AND zaporka = '" + password + "'", "zaposlenici").Tables[0];
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
        private void DefaultValue()
        {
            DataTable DTpostavke = classSQL.select_settings("SELECT default_ducan,default_blagajna,default_skladiste,default_blagajnik FROM postavke", "postavke").Tables[0];

            //cbBlagajna.SelectedValue = DTpostavke.Rows[0]["default_blagajna"].ToString();
            //cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();
            //cbSkladiste.SelectedValue = DTpostavke.Rows[0]["default_skladiste"].ToString();
            cbBlagajnik.SelectedValue = DTpostavke.Rows[0]["default_blagajnik"].ToString();
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