using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmSveNarudzbe : Form
    {
        private string sql = "";
        private DataTable DTpostavke = null;

        public frmSveNarudzbe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPredracuni_Load(object sender, EventArgs e)
        {
            try
            {
                this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

                DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                dtpDatumDo.Value = DateTime.Now;
                dtpDatumOd.Value = dtpDatumDo.Value.AddMonths(-1);

                sql = string.Format(@"SELECT x.* FROM (
    SELECT 0 AS id, 'Svi zaposlenici' AS naziv, 0 AS ord
    UNION
    SELECT id_zaposlenik AS id, CONCAT(ime, ' ', prezime) AS naziv, 1 AS ord
    FROM zaposlenici
    WHERE aktivan = 'DA'
    ORDER BY naziv ASC
) x
ORDER BY x.ord ASC;");
                DataTable dt = classSQL.select(sql, "zaposlenici").Tables[0];
                cmbZaposlenik.DisplayMember = "naziv";
                cmbZaposlenik.ValueMember = "id";
                cmbZaposlenik.DataSource = dt;

                btnPrikaziPodatke.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrikaziPodatke_Click(object sender, EventArgs e)
        {
            try
            {
                sql = string.Format(@"select row_number() over(order by datum) as rb,
cn.broj_narudzbe as broj, cn.datum,
concat(z.ime, ' ', z.prezime) as zaposlenik
from caffe_narudzbe cn
left join zaposlenici z on cn.djelatnik = z.id_zaposlenik
where cast(datum as date) between '{0:yyyy-MM-dd}' and '{1:yyyy-MM-dd}'
and case when {2} = 0 then 1 = 1 else djelatnik = '{2}' end
group by broj, datum, zaposlenik",
                        dtpDatumOd.Value, dtpDatumDo.Value, cmbZaposlenik.SelectedValue);

                DataTable dt = classSQL.select(sql, "caffe_narudzbe").Tables[0];

                dgvPredracuni.DataSource = dt;
                dgvPredracuni.Focus();
                if (dt == null || dt.Rows.Count == 0)
                {
                    dgvPredracunStavke.DataSource = null;
                    dgvPredracunStavke.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPredracuni_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.SplitterDistance = splitContainer1.Height / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvPredracuni_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int broj = 0;
                if (int.TryParse(this.dgvPredracuni.Rows[e.RowIndex].Cells["broj"].Value.ToString(), out broj) && broj != 0)
                {
                    sql = string.Format(@"SELECT ROW_NUMBER() OVER() AS rb, cn.broj_narudzbe as broj, cn.datum as datum_ispisa, cn.sifra_stavke as sifra, roba.naziv, ROUND(replace(cn.kolicina, ',','.')::numeric, 2) AS kolicina
FROM caffe_narudzbe cn
left join roba on cn.sifra_stavke = roba.sifra
WHERE cn.broj_narudzbe = '{0}';",
broj);

                    DataTable dt = classSQL.select(sql, "svi_predracuni").Tables[0];
                    dgvPredracunStavke.DataSource = dt;
                }
                else
                {
                    dgvPredracunStavke.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            try
            {
                Report.Narudzbe.frmNarudzbe f = new Report.Narudzbe.frmNarudzbe();
                f.WindowState = FormWindowState.Maximized;
                f.datumOd = dtpDatumOd.Value;
                f.datumDo = dtpDatumDo.Value;
                f.idDjelatnik = (int)cmbZaposlenik.SelectedValue;
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
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