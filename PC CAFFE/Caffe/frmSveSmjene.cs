using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmSveSmjene : Form
    {
        public frmSveSmjene()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmSveSmjene_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            LoadSmjene();
        }

        private void LoadSmjene()
        {
            string sql = "SELECT pocetno_stanje AS [Blag.Minimum],pocetak AS [Početak],zavrsetak AS [Završetak smjene],zavrsno_stanje AS [Završno stanje],napomena AS [Napomena],id FROM smjene " +
                " WHERE smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' " +
                " ORDER BY id DESC";
            DataTable DT = classSQL.select(sql, "smjene").Tables[0];
            //dgv.DataSource = DT;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[0]["Blag.Minimum"].ToString(),
                    DT.Rows[i]["Početak"].ToString(),
                    DT.Rows[i]["Završetak smjene"].ToString(),
                    DT.Rows[i]["Završno stanje"].ToString(),
                    DT.Rows[i]["Napomena"].ToString(),
                    DT.Rows[i]["id"].ToString()
                    );

                dgv.Columns["id"].Visible = false;
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Caffe.frmPregledSmjene ps = new frmPregledSmjene();
            ps.id = dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            ps.datumOD = dgv.Rows[e.RowIndex].Cells["pocetak"].FormattedValue.ToString();
            ps.datumDO = dgv.Rows[e.RowIndex].Cells["zavrsetak"].FormattedValue.ToString();
            ps.ShowDialog();
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