using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOdabirStolaCustom : Form
    {
        public frmOdabirStolaCustom()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public Caffe.frmCaffe CAFFE { get; set; }

        public void frmOdabirStola_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            if (CAFFE == null)
            {
                uredi = true;
                btnDodajNoviStol.Visible = true;
                btnDodajNoviZid.Visible = true;
                btnUrediRasporedStolova.Visible = false;
            }
            else
            {
                btnDodajNoviStol.Visible = false;
                btnDodajNoviZid.Visible = false;
            }

            SetStolovi();

            this.panelStolovi.AllowDrop = true;

            this.panelStolovi.DragOver += new DragEventHandler(panel1_DragOver);
            this.panelStolovi.DragDrop += new DragEventHandler(panel1_DragDrop);
            IscrtajZidove();

            GC.Collect();
        }

        private void IscrtajZidove()
        {
            DataTable DTzid = classSQL.select("SELECT * FROM zid;", "zid").Tables[0];

            foreach (DataRow r in DTzid.Rows)
            {
                int height, width, x, y;
                int.TryParse(r["height"].ToString(), out height);
                int.TryParse(r["width"].ToString(), out width);
                int.TryParse(r["x_pozicija"].ToString(), out x);
                int.TryParse(r["y_pozicija"].ToString(), out y);
                Panel p = new Panel();
                p.Height = height;
                p.Width = width;
                p.Name = r["id"].ToString();
                p.BackgroundImage = Properties.Resources.starizid;
                p.BackgroundImageLayout = ImageLayout.Tile;
                p.Location = new Point(x, y);

                p.MouseDown += new MouseEventHandler(this.c_MouseDown);
                p.DoubleClick += new EventHandler(this.panelZidChange_Click);

                panelStolovi.Controls.Add(p);
            }
        }

        private void panelZidChange_Click(object sender, EventArgs e)
        {
            if (!uredi)
                return;

            Panel p = (Panel)sender;
            frmZid z = new frmZid();
            int id;
            int.TryParse(p.Name.ToString(), out id);
            z.idStol = id;

            z.Top = p.Location.X;
            z.Left = p.Location.Y;
            z.Width = p.Width;
            z.Height = p.Height;
            z.frm = this;
            z.ShowDialog();

            panelStolovi.Controls.Clear();
            frmOdabirStola_Load(null, null);
        }

        private void btnDodajNoviZid_Click(object sender, EventArgs e)
        {
            frmZid z = new frmZid();
            z.idStol = -1;
            z.frm = this;
            z.ShowDialog();

            panelStolovi.Controls.Clear();
            frmOdabirStola_Load(null, null);
        }

        private void c_MouseDown(object sender, MouseEventArgs e)
        {
            if (uredi)
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    Control c = sender as Control;
                    c.DoDragDrop(c, DragDropEffects.Move);
                }
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            if (c.GetType().ToString() == "System.Windows.Forms.Button")
            {
                Button bTemp = (Button)c;
                if (c != null)
                {
                    c.Location = this.panelStolovi.PointToClient(new Point(e.X, e.Y));
                    this.panelStolovi.Controls.Add(c);
                }
                classSQL.update("UPDATE stolovi SET " +
                                " x_pozicija='" + (e.X - 17).ToString() + "'," +
                                " y_pozicija='" + (e.Y - 83).ToString() + "'" +
                                " WHERE id_stol='" + bTemp.Name + "';");
            }
            else if (c.GetType().ToString() == "System.Windows.Forms.Panel")
            {
                Panel pTemp = (Panel)c;
                classSQL.update("UPDATE zid SET " +
                                    " x_pozicija='" + (e.X - 17).ToString() + "'," +
                                    " y_pozicija='" + (e.Y - 83).ToString() + "'" +
                                    " WHERE id='" + pTemp.Name + "';");
            }
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            if (c != null)
            {
                c.Location = this.panelStolovi.PointToClient(new Point(e.X, e.Y));
                this.panelStolovi.Controls.Add(c);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetStolovi()
        {
            string sql = "SELECT " +
                " stolovi.naziv AS stol_naziv," +
                " stolovi.id_stol," +
                " x_pozicija," +
                " y_pozicija" +
                " FROM stolovi " +
                " ORDER BY id_stol ASC;" +
                " ";

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                int x, y;
                int.TryParse(DT.Rows[i]["x_pozicija"].ToString(), out x);
                int.TryParse(DT.Rows[i]["y_pozicija"].ToString(), out y);
                Button btn = CreateButtonTable(DT.Rows[i]["stol_naziv"].ToString(), DT.Rows[i]["id_stol"].ToString(), DT.Rows[i]["id_stol"].ToString(), i, x, y);
                panelStolovi.Controls.Add(btn);
            }

            string sql_rac = "SELECT id_stol,SUM(mpc*na_stol.kom) AS ukupno FROM na_stol " +
                " GROUP BY id_stol";

            DataTable DT_rac = classSQL.select(sql_rac, "racuni").Tables[0];

            foreach (Control c in this.panelStolovi.Controls)
            {
                DataRow[] DRow = DT_rac.Select("id_stol='" + c.Name + "'");
                if (DRow.Count() > 0)
                {
                    c.ForeColor = Color.Red;
                    c.Text = c.Text.Replace("0,00 kn", "") + Convert.ToDouble(DRow[0]["ukupno"].ToString()).ToString("#0.00") + " kn";
                }
            }
        }

        private Button CreateButtonTable(string naziv_stola, string ukupno, string id_stol, int i, int x, int y)
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
            button1.Location = new System.Drawing.Point(x, y);
            button1.Name = id_stol;
            button1.Text = naziv_stola + "\r\n" + "0,00" + " kn";
            button1.BackgroundImage = Image.FromFile("Slike/stol.png");
            button1.Size = new System.Drawing.Size(150, 80);
            button1.MouseDown += new MouseEventHandler(c_MouseDown);
            button1.TabIndex = i;
            //button1.Font = new Font(this.Font, FontStyle.Bold);
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler(this.btnTable_Click);
            button1.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            return button1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            CAFFE._OdabraniStol = conGrupa.Name.ToString();
            this.Close();
        }

        private void btnDodajNoviStol_Click(object sender, EventArgs e)
        {
            Caffe.frmStolovi s = new frmStolovi();
            s.ShowDialog();
            panelStolovi.Controls.Clear();
            frmOdabirStola_Load(null, null);
        }

        private bool uredi = false;

        private void btnUrediRasporedStolova_Click(object sender, EventArgs e)
        {
            if (uredi)
            {
                uredi = false;
                btnDodajNoviStol.Visible = false;
                btnDodajNoviZid.Visible = false;
            }
            else
            {
                uredi = true;
                btnDodajNoviStol.Visible = true;
                btnDodajNoviZid.Visible = true;
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