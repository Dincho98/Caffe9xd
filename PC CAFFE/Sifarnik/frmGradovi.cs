using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class Gradovi : Form
    {
        public Gradovi()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataSet Dsgrad = new DataSet();

        private void frmGradovi_Load(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS Županija FROM grad").Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS [Županija] FROM grad").Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];

            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            PaintRows();
        }

        private void PaintRows()
        {
            int br = 0;
            for (int i = 0; i < dgvGrad.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dgvGrad.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dgvGrad.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = this.dgvGrad.RowTemplate;
            row.Height = 30;
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS Županija,naselje as Naselje FROM grad").Update(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS [Županija],naselje as Naselje FROM grad").Update(Dsgrad, "Grad");
            }
            MessageBox.Show("Spremljeno");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (dgvGrad.Rows.Count != 0)
            {
                // dgvGrad.Rows.Clear();
            }
            Dsgrad = new DataSet();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT TOP(200) id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS Županija FROM grad WHERE grad LIKE '%" + textBox2.Text + "%'").Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS [Županija] FROM grad WHERE grad ~* '" + textBox2.Text + "' LIMIT 200").Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];
            PaintRows();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dgvGrad.Rows.Count != 0)
            {
                //dgvGrad.Rows.Clear();
            }
            Dsgrad = new DataSet();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT TOP(200) id_grad AS Šifra,grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS Županija FROM grad WHERE posta LIKE '%" + textBox1.Text + "%'").Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra],grad AS [Ime Grada],posta AS [Broj pošte],zupanija AS [Županija] FROM grad WHERE posta LIKE '%" + textBox1.Text + "%' LIMIT 200").Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];
            PaintRows();
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