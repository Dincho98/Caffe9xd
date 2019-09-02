using System;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmZid : Form
    {
        public frmZid()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public int idStol { get; set; }
        public frmOdabirStolaCustom frm { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        private bool load = false;

        private void frmZid_Load(object sender, EventArgs e)
        {
            label6.Text = "Visina cijele površine za stolove je:" + (frm.panelStolovi.Height - 10).ToString() + " piksela.";
            label7.Text = "Širina cijele površine za stolove je:" + (frm.panelStolovi.Width - 10).ToString() + " piksela.";

            if (idStol == -1)
            {
                nuTop.Value = 0;
                nuLeft.Value = 0;
                nuHeight.Value = 33;
                nuWidth.Value = 300;
                btnObrisi.Visible = false;
            }
            else
            {
                nuTop.Value = Top;
                nuLeft.Value = Left;
                nuHeight.Value = Height;
                nuWidth.Value = Width;
            }

            load = true;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (idStol == -1)
            {
                classSQL.insert("INSERT INTO zid (x_pozicija,y_pozicija,height,width) VALUES (" +
                    "'" + nuTop.Value.ToString() + "'," +
                    "'" + nuLeft.Value.ToString() + "'," +
                    "'" + nuHeight.Value.ToString() + "'," +
                    "'" + nuWidth.Value.ToString() + "'" +
                    ");");
            }
            else
            {
                classSQL.update(@"UPDATE zid SET
                x_pozicija='" + nuTop.Value.ToString() + @"',
                y_pozicija='" + nuLeft.Value.ToString() + @"',
                height='" + nuHeight.Value.ToString() + @"',
                width='" + nuWidth.Value.ToString() + @"'
                WHERE id='" + idStol + "';");
            }

            frm.panelStolovi.Controls.Clear();
            frm.frmOdabirStola_Load(null, null);
        }

        private void nuTop_ValueChanged(object sender, EventArgs e)
        {
            if (!load)
                return;

            classSQL.update(@"UPDATE zid SET
                x_pozicija='" + nuTop.Value.ToString() + @"',
                y_pozicija='" + nuLeft.Value.ToString() + @"',
                height='" + nuHeight.Value.ToString() + @"',
                width='" + nuWidth.Value.ToString() + @"'
                WHERE id='" + idStol + "';");

            frm.panelStolovi.Controls.Clear();
            frm.frmOdabirStola_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati ovaj zid?", "Brisanje zida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                classSQL.delete(@"DELETE FROM zid WHERE id='" + idStol + "';");
                frm.panelStolovi.Controls.Clear();
                frm.frmOdabirStola_Load(null, null);
            }
        }
    }
}