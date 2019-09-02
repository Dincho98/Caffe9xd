using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmBonUpis : Form
    {
        public int idPartnersBon { get; set; }
        public int idPartner { get; set; }

        public frmBonUpis()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmBonUpis_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            string sql = @"select x.bon, x.naziv
from (
    select 0 as bon, 'Koristi bon' as naziv, 1 as sort
    union
    select 1 as bon, 'Kupi bon' as naziv, 0 as sort
) x
order by sort asc;";
            DataSet dsBon = classSQL.select(sql, "bon");

            cmbBon.DisplayMember = "naziv";
            cmbBon.ValueMember = "bon";
            cmbBon.DataSource = dsBon.Tables[0];
            cmbBon.SelectedValue = 1;

            sql = string.Format(@"select case when partners.vrsta_korisnika = 1 then partners.ime_tvrtke else
	case when length(partners.ime) = 0 and length(partners.prezime) = 0 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end
	end as partner
from partners
where id_partner = {0};", idPartner);

            DataSet dsPartner = classSQL.select(sql, "partner");
            if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
            {
                lblPartner.Text += ": " + dsPartner.Tables[0].Rows[0]["partner"];
            }

            decimal iznos = 0;
            DateTime datum = DateTime.Now;
            dtpDatum.Value = datum;
            txtIznos.Text = iznos.ToString("#,##");
            if (idPartnersBon > 0)
            {
                sql = string.Format(@"select partners_bon.id, partners_bon.id_partner, partners_bon.datum, partners_bon.bon, partners_bon.iznos
from partners_bon
where partners_bon.id = {0};", idPartnersBon);
                DataSet dsPartnersBon = classSQL.select(sql, "partners_bon");
                if (dsPartnersBon != null && dsPartnersBon.Tables.Count > 0 && dsPartnersBon.Tables[0] != null && dsPartnersBon.Tables[0].Rows.Count > 0)
                {
                    bool bon = true;
                    DateTime.TryParse(dsPartnersBon.Tables[0].Rows[0]["datum"].ToString(), out datum);
                    decimal.TryParse(dsPartnersBon.Tables[0].Rows[0]["iznos"].ToString(), out iznos);
                    bool.TryParse(dsPartnersBon.Tables[0].Rows[0]["bon"].ToString(), out bon);

                    dtpDatum.Value = datum;
                    txtIznos.Text = iznos.ToString("#,##");
                    cmbBon.SelectedValue = (bon ? 1 : 0);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            decimal iznos = 0;
            if (txtIznos.Text.Trim().Length == 0 || !decimal.TryParse(txtIznos.Text.Trim(), out iznos))
            {
                MessageBox.Show("Pogrešan unos iznosa.");
                return;
            }

            if (idPartnersBon > 0)
            {
                string sql = string.Format(@"update partners_bon
set
    datum = '{0:yyyy-MM-dd HH:mm:ss}',
    bon = {1},
    iznos = {2}
where id = {3};", dtpDatum.Value, ((int)cmbBon.SelectedValue == 1 ? true : false), iznos.ToString().Replace(",", "."), idPartnersBon);
                classSQL.update(sql);
            }
            else
            {
                string sql = string.Format(@"insert into partners_bon (id_partner, datum, bon, iznos)
values (
{0}, '{1:yyyy-MM-dd HH:mm:ss}', {2}, {3}
);", idPartner, dtpDatum.Value, ((int)cmbBon.SelectedValue == 1 ? true : false), iznos.ToString().Replace(",", "."));
                classSQL.insert(sql);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}