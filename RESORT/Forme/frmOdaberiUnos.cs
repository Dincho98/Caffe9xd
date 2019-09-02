using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmOdaberiUnos : Form
    {
        public frmOdaberiUnos()
        {
            InitializeComponent();
        }

        public List<string> lista { get; set; }
        public string dan { get; set; }
        public string mjesec { get; set; }
        public string godina { get; set; }

        private DataTable DTBojeForme;
        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmOdaberiUnos_Load(object sender, EventArgs e)
        {
            SetValues();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
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

        private void SetValues()
        {
            for (int i = 0; i < lista.Count; i++)
            {
                string unos = lista[i].Remove(0, 1);
                string forma = lista[i].Remove(1);
                string naziv = "";
                string gosti = "";
                DataTable DT;

                if (forma == "U")
                {
                    DT = RemoteDB.select("SELECT * FROM unos_gosta WHERE broj='" + unos + "'", "unos_gosta").Tables[0];
                    foreach (DataRow row in DT.Rows)
                    {
                        gosti += row["ime_gosta"].ToString() + "\r\n";
                    }

                    naziv = "UNESENI GOSTI:\r\n" +
                        gosti +
                        "OD: " + DT.Rows[0]["datum_dolaska"].ToString() + "\r\nDO: " + DT.Rows[0]["datum_odlaska"].ToString();
                }
                else
                {
                    DT = RemoteDB.select("SELECT * FROM unos_rezervacije WHERE broj='" + unos + "'", "unos_rezervacije").Tables[0];
                    naziv = "UNESENA REZERVACIJA:\r\n" +
                    gosti +
                    "OD: " + DT.Rows[0]["datum_dolaska"].ToString() + "\r\nDO: " + DT.Rows[0]["datum_odlaska"].ToString();
                }

                string ime_gumba = naziv;
                string name_gumba = lista[i];

                Button btnGrupa = new Button();
                btnGrupa.Text = ime_gumba;
                btnGrupa.Name = name_gumba;
                btnGrupa.BackColor = System.Drawing.Color.Transparent;
                btnGrupa.BackgroundImage = Image.FromFile("slike/backGroung.jpg");
                ;
                btnGrupa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btnGrupa.Cursor = System.Windows.Forms.Cursors.Hand;
                btnGrupa.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
                btnGrupa.FlatAppearance.BorderSize = 0;
                btnGrupa.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
                btnGrupa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                btnGrupa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                btnGrupa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnGrupa.Font = new System.Drawing.Font("Arial", 10F);
                btnGrupa.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                //btnGrupa.Location = new System.Drawing.Point(301, 33);
                btnGrupa.Size = new System.Drawing.Size(250, 200);
                btnGrupa.TabIndex = i;
                btnGrupa.UseVisualStyleBackColor = false;
                btnGrupa.Click += new System.EventHandler(this.btnGrupa_Click);
                flowLayoutPanel1.Controls.Add(btnGrupa);
            }
        }

        private void btnGrupa_Click(object sender, EventArgs e)
        {
            Control conArtikl = (Control)sender;

            string id = conArtikl.Name.ToString();

            if (id.Remove(1) == "U")
            {
                Forme.frmIzbornik i = new Forme.frmIzbornik();
                i.broj_unosa = id.Remove(0, 1);
                i.godina = godina;
                i.ShowDialog();
                //Forme.frmUpisGosta upg = new Forme.frmUpisGosta();
                //upg.broj_unosa = id.Remove(0, 1);
                //upg.ShowDialog();
            }
            else if (id.Remove(1) == "R")
            {
                Forme.frmIzbornikR i = new Forme.frmIzbornikR();
                i.broj_unosa = id.Remove(0, 1);
                i.godina = godina;
                i.ShowDialog();
                //Forme.frmUnosRezervacije upg = new Forme.frmUnosRezervacije();
                //upg.broj_unosa = id.Remove(0, 1);
                //upg.ShowDialog();
            }
        }
    }
}