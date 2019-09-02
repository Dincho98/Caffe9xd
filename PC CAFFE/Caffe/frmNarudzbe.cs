using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmNarudzbe : Form
    {
        public frmNarudzbe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmNarudzbe_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            timer1.Start();
            fill();
        }

        private void fill()
        {
            bool k = false;
            bool s = false;
            bool postojanje_kuhinje = false;

            string sql_group = "SELECT datum FROM caffe_narudzbe GROUP BY datum ORDER BY datum DESC";
            DataTable DT_group = classSQL.select(sql_group, "caffe_narudzbe").Tables[0];

            for (int i = 0; i < DT_group.Rows.Count; i++)
            {
                string sql = string.Format(@"SELECT caffe_narudzbe.kolicina, grupa.id_podgrupa, caffe_narudzbe.datum, caffe_narudzbe.sifra_stavke, roba.naziv,
caffe_narudzbe.sank_spreman, caffe_narudzbe.kuhinja_spremna, concat(zaposlenici.ime, ' ', zaposlenici.prezime) AS zaposlenik
FROM caffe_narudzbe
LEFT JOIN roba ON roba.sifra=caffe_narudzbe.sifra_stavke
LEFT JOIN grupa ON roba.id_grupa=grupa.id_grupa
LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=caffe_narudzbe.djelatnik
WHERE datum = '{0}';",
DT_group.Rows[i]["datum"].ToString());

                string tekst_richtextbox = "";
                DataTable DT = classSQL.select(sql, "caffe_narudzbe").Tables[0];

                for (int y = 0; y < DT.Rows.Count; y++)
                {
                    if (DT.Rows[y]["id_podgrupa"].ToString() == "2")
                    {
                        postojanje_kuhinje = true;
                    }
                }

                if (postojanje_kuhinje)
                {
                    tekst_richtextbox = "Datum: " + DT.Rows[0]["datum"].ToString() + "\r\n" + "Djelatnik: " + DT.Rows[0]["zaposlenik"].ToString() + "\r\n\r\n";

                    if (DT.Rows[0]["sank_spreman"].ToString() == "1")
                    {
                        s = true;
                    }

                    if (DT.Rows[0]["kuhinja_spremna"].ToString() == "1")
                    {
                        k = true;
                    }

                    for (int y = 0; y < DT.Rows.Count; y++)
                    {
                        tekst_richtextbox += y + 1 + ". " + DT.Rows[y]["naziv"].ToString() + ", Kol:" + DT.Rows[y]["kolicina"].ToString() + "\r\n";
                    }

                    CreateLabel(DT_group.Rows[i]["datum"].ToString(), tekst_richtextbox, k, s, postojanje_kuhinje);
                }
                k = false;
                s = false;
                postojanje_kuhinje = false;
            }
        }

        private void CreateLabel(string broj_racuna, string tekst, bool k, bool s, bool postojanje_kuhinje)
        {
            Panel panel1 = new System.Windows.Forms.Panel();
            Button button1 = new System.Windows.Forms.Button();
            //Button button2 = new System.Windows.Forms.Button();
            RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
            //
            // panel1
            //
            panel1.Controls.Add(richTextBox1);

            if (postojanje_kuhinje)
            {
                panel1.Controls.Add(button1);
            }

            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(330, 330);
            panel1.TabIndex = 0;
            //
            // button1
            //

            button1.Location = new System.Drawing.Point(2, 287);
            button1.Name = broj_racuna;
            button1.Size = new System.Drawing.Size(196, 40);
            button1.TabIndex = 0;
            button1.Font = new System.Drawing.Font("Arial", 9.5F);

            button1.Text = "Jelo se priprema";
            button1.UseVisualStyleBackColor = true;
            button1.BackColor = System.Drawing.Color.Transparent;
            button1.BackgroundImage = Properties.Resources.dff;
            button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button1.Cursor = System.Windows.Forms.Cursors.Hand;
            button1.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.ForeColor = Color.Red;
            button1.Click += new System.EventHandler(this.KuhinjaSpremna_Click);
            if (k) { button1.ForeColor = Color.Black; button1.Text = "Jelo je pripremljeno"; }

            //
            // button
            //
            if (postojanje_kuhinje)
            {
                //button2.Location = new System.Drawing.Point(2, 237);
                //button2.Size = new System.Drawing.Size(196, 40);
            }
            else
            {
                //button2.Location = new System.Drawing.Point(2, 237);
                //button2.Size = new System.Drawing.Size(196, 40);
            }

            /*button2.Name = broj_racuna;
            button2.Font = new System.Drawing.Font("Arial", 9.5F);
            button2.TabIndex = 0;
            button2.Text = "Piće se priprema";
            button2.UseVisualStyleBackColor = true;
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.BackgroundImage = Properties.Resources.btn11;
            button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button2.Cursor = System.Windows.Forms.Cursors.Hand;
            button2.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Click += new System.EventHandler(this.SankSpreman_Click);
            button2.ForeColor = Color.Red;
            if (s) { button2.ForeColor = Color.Black; button2.Text = "Piće je spremno"; }*/
            //
            // richTextBox1
            //
            richTextBox1.Location = new System.Drawing.Point(4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Font = new System.Drawing.Font("Arial", 14F);
            richTextBox1.Size = new System.Drawing.Size(329, 280);
            richTextBox1.TabIndex = 1;
            if (k)
                richTextBox1.BackColor = Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(206)))), ((int)(((byte)(217)))));
            else
                richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(175)))), ((int)(((byte)(175)))));

            richTextBox1.Text = tekst;

            flowLayoutPanel1.Controls.Add(panel1);
        }

        private void KuhinjaSpremna_Click(object sender, EventArgs e)
        {
            Button btnKuhinja = ((Button)sender);
            string sql = string.Format("UPDATE caffe_narudzbe SET kuhinja_spremna=1 WHERE datum = '{0}';", btnKuhinja.Name.ToString());
            classSQL.update(sql);
            flowLayoutPanel1.Controls.Clear();
            fill();
        }

        private void SankSpreman_Click(object sender, EventArgs e)
        {
            Button btnSank = ((Button)sender);
            string sql = string.Format("UPDATE caffe_narudzbe SET sank_spreman=1 WHERE datum = '{0}';", btnSank.Name.ToString());
            classSQL.update(sql);
            flowLayoutPanel1.Controls.Clear();
            fill();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            fill();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
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