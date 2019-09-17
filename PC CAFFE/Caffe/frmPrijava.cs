using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPrijava : Form
    {
        public frmPrijava()
        {
            InitializeComponent();
            Until.design.formBackGround = Color.SlateGray;
            Until.design.panelBackGround = Color.LightSlateGray;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTsettings = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        public frmScren MainForm { get; set; }
        private DataSet DSzaposlenik;
        private DataTable DTpostavke = new DataTable();
        private bool load = false;

        private void frmPrijava_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            cbBlagajnik.Select();
            fillComboBox();
            load = true;
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
            if (DTpostavke == null)
                Application.Exit();

            DTpostavke = classSQL.select_settings("SELECT default_blagajnik,magnetska_kartica FROM postavke", "postavke").Tables[0];
            DSzaposlenik = classSQL.select("SELECT id_zaposlenik, ime + ' ' + prezime AS name FROM zaposlenici WHERE aktivan='DA' AND id_zaposlenik <>'1'", "zaposlenici");
            cbBlagajnik.DataSource = DSzaposlenik.Tables[0];
            cbBlagajnik.DisplayMember = "name";
            cbBlagajnik.ValueMember = "id_zaposlenik";
            try
            {
                cbBlagajnik.SelectedValue = DTpostavke.Rows[0]["default_blagajnik"].ToString();
            }
            catch (Exception)
            {
                classSQL.Setings_Update("UPDATE postavke SET default_blagajnik='" + cbBlagajnik.SelectedValue + "'");
            }

            if (DTpostavke.Rows[0]["magnetska_kartica"].ToString() == "1")
            {
                panel2.Visible = false;
                panel1.Visible = true;
                txtKartica.Select();
            }
            else
            {
                panel2.Visible = true;
                panel1.Visible = false;
                cbBlagajnik.Select();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*this.Hide();
            Caffe.frmGasenjePrograma GP = new frmGasenjePrograma();
            GP.Closed += (s, args) => this.Close();
            MainForm.Close();
            this.Close();
            GP.ShowDialog();*/
            Environment.Exit(0);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            txtSifra.Text += conGrupa.Text;
        }

        private DataTable DTzap = new DataTable();

        private void txtSifra_TextChanged(object sender, EventArgs e)
         {
            //LoginPassword((int)cbBlagajnik.SelectedValue, txtSifra.Text.Trim());

            if (txtSifra.Text == ",00000000000000000000,")
            {
                Properties.Settings.Default.id_zaposlenik = "1";
                Properties.Settings.Default.Save();
                MainForm.Show();
                this.Close();
                return;
            }

            try
            {
                DTzap = classSQL.select("SELECT id_zaposlenik,id_dopustenje FROM zaposlenici WHERE id_zaposlenik='" + cbBlagajnik.SelectedValue.ToString() + "' AND zaporka='" + txtSifra.Text + "'", "zaposlenici").Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

            if (DTzap.Rows.Count > 0)
            {
                Properties.Settings.Default.id_zaposlenik = cbBlagajnik.SelectedValue.ToString();
                Properties.Settings.Default.id_dopustenje = Convert.ToInt32(DTzap.Rows[0]["id_dopustenje"].ToString());
                Properties.Settings.Default.Save();

                if (DTzap.Rows[0]["id_dopustenje"].ToString() == "1")
                {
                    PrijavaAdmin();
                }
                else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "2")
                {
                    PrijavaAdmin();
                }
                else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "3")
                {
                    PrijavaAdmin();
                }
                else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "4")
                {
                    PrijavaAdmin();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrijavaAdmin()
        {
            /*
            this.Hide();
            frmMenu menu = new frmMenu();
            menu.Show();
            this.Close();
            */
            txtSifra.Text = "";
            
                if (this.MainForm == null)
                {
                    this.Hide();
                    frmMenu menu = new frmMenu();
                    //menu.Closed += (s, args) => this.Close();
                    menu.Show();
                }
                else
                {
                    this.Hide();
                    frmScren scren = new frmScren();
                    scren.prvaPrijava = true;
                    scren.MdiParent = this.MdiParent;
                    scren.Dock = DockStyle.Fill;
                    scren.Show();
                }
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrijavaZaposlenik()
        {
            txtSifra.Text = "";
            this.Hide();
            frmScren frmScren = new frmScren();
            frmScren.Dock = DockStyle.Fill;
            frmScren.Show();
            frmScren.btnMaloprodaja.PerformClick();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "DEL" && txtSifra.Text.Length > 0)
            {
                txtSifra.Text = txtSifra.Text.Remove(txtSifra.Text.Length - 1);
            }
        }

        private void cbBlagajnik_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnIzlaz.PerformClick();
            }
        }

        private void cbBlagajnik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnIzlaz.PerformClick();
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra.Select();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtSifra.Text = txtSifra.Text;
        }

        private void frmPrijava_SizeChanged(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnIzlaz.PerformClick();
            }
        }

        private void txtKartica_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyData)
            {
                if (txtKartica.Text == ",00000000000000000000,")
                {
                    Properties.Settings.Default.id_zaposlenik = "2";
                    Properties.Settings.Default.Save();
                    MainForm.Show();
                    this.Close();
                    return;
                }

                try
                {
                    DTzap = classSQL.select("SELECT id_zaposlenik,id_dopustenje FROM zaposlenici WHERE kartica='" + txtKartica.Text + "'", "zaposlenici").Tables[0];
                }
                catch
                {
                }

                if (DTzap.Rows.Count > 0)
                {
                    Properties.Settings.Default.id_zaposlenik = DTzap.Rows[0]["id_zaposlenik"].ToString();
                    Properties.Settings.Default.id_dopustenje = Convert.ToInt32(DTzap.Rows[0]["id_dopustenje"].ToString());
                    Properties.Settings.Default.Save();

                    if (DTzap.Rows[0]["id_dopustenje"].ToString() == "1")
                    {
                        PrijavaAdmin();
                    }
                    else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "2")
                    {
                        PrijavaAdmin();
                    }
                    else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "3")
                    {
                        PrijavaZaposlenik();
                    }
                    else if (DTzap.Rows[0]["id_dopustenje"].ToString() == "4")
                    {
                        PrijavaZaposlenik();
                    }
                }
            }
        }

        private void frmPrijava_Activated(object sender, EventArgs e)
        {
            if (load)
            {
                if (DTpostavke.Rows[0]["magnetska_kartica"].ToString() == "1")
                {
                    txtKartica.Select();
                    txtKartica.Text = "";
                }
            }
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
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