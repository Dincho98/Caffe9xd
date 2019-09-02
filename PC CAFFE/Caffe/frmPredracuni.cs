using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmPredracuni : Form
    {
        private string sql = "";
        private DataTable DTpostavke = null;

        public frmPredracuni()
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
                sql = string.Format(@"DROP TABLE IF EXISTS tempUkp;
CREATE TEMP TABLE tempUkp AS
SELECT p.broj, ROUND(sum(p.kolicina * p.mpc), 2) AS mpcUkp, ROUND(sum(p.kolicina * p.mpc), 2) - ROUND(sum(p.kolicina * p.vpc), 2) as porezUkp, ROUND(sum(p.kolicina * p.vpc), 2) AS vpcUkp, ROUND(sum(p.kolicina * p.porez_potrosnja), 2) AS ppUkp
FROM svi_predracuni AS p
WHERE p.id_ducan = '{0}'
AND p.id_blagajna = '{1}'
AND CAST(p.datum_ispisa AS date) BETWEEN '{2}' AND '{3}'
GROUP BY p.broj;

SELECT DISTINCT ROW_NUMBER() OVER(order by x.datum_ispisa) AS rb, x.* from (SELECT DISTINCT p.broj, p.datum_ispisa, CONCAT(z.ime, ' ', z.prezime) AS zaposlenik, tu.mpcUkp, tu.porezUkp, tu.vpcUkp, tu.ppUkp
FROM svi_predracuni AS p
LEFT JOIN zaposlenici AS z ON p.id_zaposlenik = z.id_zaposlenik
left join tempUkp AS tu on p.broj = tu.broj
WHERE p.id_ducan = '{0}'
AND p.id_blagajna = '{1}'",
                        DTpostavke.Rows[0]["default_ducan"], DTpostavke.Rows[0]["default_blagajna"],
                        dtpDatumOd.Value, dtpDatumDo.Value);

                if (cmbZaposlenik.SelectedValue != null && Convert.ToInt32(cmbZaposlenik.SelectedValue) != 0)
                {
                    sql += string.Format(@"
AND p.id_zaposlenik = '{0}'", cmbZaposlenik.SelectedValue);
                }

                sql += string.Format(@"
AND CAST(p.datum_ispisa AS date) BETWEEN '{0}' AND '{1}' ORDER BY p.broj
) x ORDER BY x.datum_ispisa; ",
    dtpDatumOd.Value, dtpDatumDo.Value);

                DataTable dt = classSQL.select(sql, "svi_predracuni").Tables[0];

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
                    sql = string.Format(@"SELECT ROW_NUMBER() OVER() AS rb, broj, datum_ispisa, sifra, naziv,
round(mpc, 2) AS mpc, ROUND(porez, 2) AS porez, ROUND(vpc, 2) AS vpc, ROUND(porez_potrosnja,2) AS pp, ROUND(kolicina, 2) AS kolicina
FROM svi_predracuni
WHERE id_ducan = '{0}' AND id_blagajna = '{1}' AND broj = '{2}';",
DTpostavke.Rows[0]["default_ducan"], DTpostavke.Rows[0]["default_blagajna"], broj);

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
                Report.predracuni.frmPredracuniSvi f = new Report.predracuni.frmPredracuniSvi();
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