using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmOdaberiSobuZaNaplatiti : Form
    {
        public frmOdaberiSobuZaNaplatiti()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;
        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];
        public string broj_unosa { get; set; }
        public string godina { get; set; }

        private void frmOdaberiSobuZaNaplatiti_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            SetValues();
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
            DataTable DT = RemoteDB.select("SELECT unos_rezervacije.ime_gosta,unos_rezervacije.id,unos_rezervacije.id_soba,sobe.naziv_sobe FROM unos_rezervacije LEFT JOIN sobe ON sobe.id=unos_rezervacije.id_soba WHERE unos_rezervacije.broj='" + broj_unosa + "' AND unos_rezervacije.godina='" + godina + "'", "unos_rezervacije").Tables[0];
            foreach (DataRow row in DT.Rows)
            {
                dgv.Rows.Add(row["ime_gosta"], row["naziv_sobe"], false, row["id"], row["id_soba"]);
            }
        }

        private void btnIzradiRaccun_Click(object sender, EventArgs e)
        {
            List<string> L = new List<string>();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[2].Value.ToString() == "True")
                    L.Add(dgv.Rows[i].Cells["id"].FormattedValue.ToString());
            }

            if (L.Count > 0)
            {
                frmFaktura f = new frmFaktura();
                f.ListaIzRezervacija = L;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nemate odabrane goste.");
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                int cell = 2;

                if (dgv.Rows[row].Cells[cell].Value.ToString() == "False")
                    dgv.Rows[row].Cells[cell].Value = true;
                else
                    dgv.Rows[row].Cells[cell].Value = false;
            }
        }
    }
}