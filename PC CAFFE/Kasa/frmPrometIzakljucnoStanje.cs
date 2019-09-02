using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPrometIzakljucnoStanje : Form
    {
        public frmPrometIzakljucnoStanje()
        {
            InitializeComponent();
        }

        private void frmPrometIzakljucnoStanje_Load(object sender, EventArgs e)
        {
            SetCB();
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            DT_Skladiste2.Columns.Add("id_skladiste", typeof(string));
            DT_Skladiste2.Columns.Add("skladiste", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_skladiste"].ToString(), DT_Skladiste.Rows[i]["skladiste"].ToString());
            }

            cbSkladiste.DataSource = DT_Skladiste2;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            frmPrometStanje pro = new frmPrometStanje();
            if (chbDucan.Checked) { pro.poslovnica = cbDucan.SelectedValue.ToString(); }
            if (chbSkladiste.Checked) { pro.skladiste = cbSkladiste.SelectedValue.ToString(); }
            pro.datumOD = dtpOD.Value;
            pro.datumDO = dtpDO.Value;
            pro.ShowDialog();
        }
    }
}