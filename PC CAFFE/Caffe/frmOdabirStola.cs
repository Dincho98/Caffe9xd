using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOdabirStola : Form
    {
        public frmOdabirStola()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public Caffe.frmCaffe CAFFE { get; set; }

        private void frmOdabirStola_Load(object sender, EventArgs e)
        {
            if (File.Exists("belveder"))
            {
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel1.Height = 1200;
                flowLayoutPanel1.Width = 1000;
            }
            else
            {
                flowLayoutPanel1.Visible = false;
                flpStolovi.Controls.Clear();
            }

            SetStolovi();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void SetStolovi()
        {
            string sql = "SELECT " +
                " stolovi.naziv AS stol_naziv," +
                " stolovi.id_stol" +
                " FROM stolovi " +
                " ORDER BY id_stol ASC;" +
                " ";

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Button btn = CreateButtonTable(DT.Rows[i]["stol_naziv"].ToString(), DT.Rows[i]["id_stol"].ToString(), DT.Rows[i]["id_stol"].ToString(), i);

                if (File.Exists("belveder"))
                {
                    flowLayoutPanel1.Controls.Add(btn);
                }
                else
                {
                    flpStolovi.Controls.Add(btn);
                }
            }

            string sql_rac = "SELECT id_stol,SUM(mpc*na_stol.kom) AS ukupno FROM na_stol " +
                " GROUP BY id_stol";

            DataTable DT_rac = classSQL.select(sql_rac, "racuni").Tables[0];

            if (File.Exists("belveder"))
            {
                foreach (Control c in this.flowLayoutPanel1.Controls)
                {
                    DataRow[] DRow = DT_rac.Select("id_stol='" + c.Name + "'");
                    if (DRow.Count() > 0)
                    {
                        c.BackgroundImage = Properties.Resources.dff;
                        c.Text = c.Text.Replace("0,00 kn", "") + Convert.ToDouble(DRow[0]["ukupno"].ToString()).ToString("#0.00") + " kn";
                    }
                }
            }
            else
            {
                foreach (Control c in this.flpStolovi.Controls)
                {
                    DataRow[] DRow = DT_rac.Select("id_stol='" + c.Name + "'");
                    if (DRow.Count() > 0)
                    {
                        c.BackgroundImage = Properties.Resources.dff;
                        c.Text = c.Text.Replace("0,00 kn", "") + Convert.ToDouble(DRow[0]["ukupno"].ToString()).ToString("#0.00") + " kn";
                    }
                }
            }
        }

        private Button CreateButtonTable(string naziv_stola, string ukupno, string id_stol, int i)
        {
            Button button1 = new Button();
            button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            button1.BackColor = System.Drawing.Color.Transparent;

            button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button1.Cursor = System.Windows.Forms.Cursors.Hand;
            button1.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            button1.Location = new System.Drawing.Point(904, 84);
            button1.Name = id_stol;
            button1.Text = naziv_stola + "\r\n" + "0,00" + " kn";
            button1.BackgroundImage = Image.FromFile("Slike/stol.png");
            button1.Size = new System.Drawing.Size(200, 110);
            button1.TabIndex = i;
            //button1.Font = new Font(this.Font, FontStyle.Bold);
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler(this.btnTable_Click);
            button1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            return button1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            CAFFE._OdabraniStol = conGrupa.Name.ToString();
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