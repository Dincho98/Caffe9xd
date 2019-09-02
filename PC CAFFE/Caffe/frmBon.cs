using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmBon : Form
    {
        public frmBon()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmBon_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void btnPonistiPartnera_Click(object sender, EventArgs e)
        {
            try
            {
                txtSifraOdrediste.Text = "";
                txtPartnerNaziv.Text = "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                txtSifraOdrediste.Text = Properties.Settings.Default.id_partner.Trim();
                string sifraPartnera = txtSifraOdrediste.Text.Trim();
                if (sifraPartnera.Length > 0)
                {
                    findPartner(sifraPartnera);
                }
            }
        }

        private void txtSifraOdrediste_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string sifraPartnera = txtSifraOdrediste.Text.Trim();
                if (sifraPartnera.Length > 0 && e.KeyChar == (char)Keys.Enter)
                {
                    findPartner(sifraPartnera);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void findPartner(string sifraPartnera)
        {
            try
            {
                txtPartnerNaziv.Text = "";
                int idPartner = 0;
                if (!int.TryParse(sifraPartnera, out idPartner))
                {
                    MessageBox.Show("Pogrešna šifra partnera.");
                    return;
                }

                string sql = string.Format(@"select case when vrsta_korisnika = 1 then ime_tvrtke else
	case when length(ime) = 0 and length(prezime) = 0 then ime_tvrtke else concat(ime, ' ', prezime) end
	end as partner
from partners
where id_partner = {0};", idPartner);

                DataSet dsPartner = classSQL.select(sql, "partner");
                if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = dsPartner.Tables[0].Rows[0]["partner"].ToString();
                    txtSifraOdrediste.Text = idPartner.ToString();
                }
                else
                {
                    MessageBox.Show("Partner ne postoji.");
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                string controlName = (sender as Button).Tag.ToString().Trim();
                DataGridView dgv = (this.Controls.Find(controlName, true)[0] as DataGridView);
                int step = 5;
                int.TryParse(dgv.Tag.ToString(), out step);
                for (int i = 0; i < step; i++)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex - 1 >= 0)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex - 1;
                    }
                    else
                    {
                        return;
                    }
                }

                controlName = null;
                dgv = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                string controlName = (sender as Button).Tag.ToString().Trim();
                DataGridView dgv = (this.Controls.Find(controlName, true)[0] as DataGridView);
                int step = 5;
                int.TryParse(dgv.Tag.ToString(), out step);
                for (int i = 0; i < step; i++)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex + 1 < dgv.Rows.Count)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex + 1;
                    }
                    else
                    {
                        return;
                    }
                }
                controlName = null;
                dgv = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBon.Columns.Count == 0)
                {
                    dgvBon.Columns.Add("id_partner", "id_partner");
                    dgvBon.Columns.Add("partner", "Partner");
                    dgvBon.Columns.Add("stanje", "Stanje");
                    dgvBon.Columns["stanje"].DefaultCellStyle.Format = "N2";
                }

                string sql = "";
                int idPartner = 0;
                if (txtSifraOdrediste.Text.Trim().Length > 0 && Int32.TryParse(txtSifraOdrediste.Text.Trim(), out idPartner) && idPartner > 0)
                {
                    sql = string.Format(@"select p.id_partner, p.partner, coalesce(x.stanje, 0) as stanje
from (
	select id_partner, sum(case when bon then iznos else iznos * (-1) end) as stanje
	from partners_bon
    where id_partner = {0}
	group by id_partner
) x
right join (
	select id_partner, case when vrsta_korisnika = 1 then ime_tvrtke else
	case when length(ime) = 0 and length(prezime) = 0 then ime_tvrtke else concat(ime, ' ', prezime) end
	end as partner
	from partners
    where id_partner = {0}
) p on x.id_partner = p.id_partner
order by p.partner;", idPartner);
                }
                else
                {
                    sql = @"select p.id_partner, p.partner, coalesce(x.stanje, 0) as stanje
from (
	select id_partner, sum(case when bon then iznos else iznos * (-1) end) as stanje
	from partners_bon
	group by id_partner
) x
left join (
	select id_partner, case when vrsta_korisnika = 1 then ime_tvrtke else
	case when length(ime) = 0 and length(prezime) = 0 then ime_tvrtke else concat(ime, ' ', prezime) end
	end as partner
	from partners
) p on x.id_partner = p.id_partner
order by p.partner;";
                }
                DataSet dsPartners = classSQL.select(sql, "partners");

                dgvBon.Rows.Clear();

                if (dsPartners != null && dsPartners.Tables.Count > 0 && dsPartners.Tables[0] != null && dsPartners.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dsPartners.Tables[0].Rows)
                    {
                        dgvBon.Rows.Add(item["id_partner"], item["partner"], item["stanje"]);
                    }
                }

                dgvBon.Columns["id_partner"].Visible = false;
                // TODO selektirati z baze sve partnere z bonovima ako partner nije odabran u suprotnome samo taj partner
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvBon_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChangedOnPartner();
        }

        private void SelectionChangedOnPartner(bool postaviNaBonGridStanje = false)
        {
            try
            {
                // TODO selektirati detaljno iz baze za odabrani partner

                if (dgvDetaljno.Columns.Count == 0)
                {
                    dgvDetaljno.Columns.Add("id", "id");
                    dgvDetaljno.Columns.Add("datum", "Datum");
                    dgvDetaljno.Columns.Add("iznos", "Iznos");
                    dgvDetaljno.Columns["iznos"].DefaultCellStyle.Format = "N2";
                }

                decimal bon = 0, iskoristeno = 0;
                dgvDetaljno.Rows.Clear();
                if (dgvBon.Rows.Count > 0 && (dgvBon.SelectedCells.Count > 0 && dgvBon.SelectedCells[0].RowIndex >= 0))
                {
                    string sql = string.Format(@"select id, datum, case when bon then iznos else iznos * (-1) end as iznos
from partners_bon
where id_partner = {0}
order by datum desc;", dgvBon.Rows[dgvBon.SelectedCells[0].RowIndex].Cells["id_partner"].Value);

                    DataSet dsDetaljno = classSQL.select(sql, "detaljno");
                    if (dsDetaljno != null && dsDetaljno.Tables.Count > 0 && dsDetaljno.Tables[0] != null && dsDetaljno.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dsDetaljno.Tables[0].Rows)
                        {
                            dgvDetaljno.Rows.Add(item["id"], item["datum"], item["iznos"]);
                            decimal iznos = 0;
                            decimal.TryParse(item["iznos"].ToString(), out iznos);
                            if (iznos > 0)
                            {
                                bon += iznos;
                            }
                            else
                            {
                                iskoristeno += iznos;
                            }
                        }
                    }
                    dgvDetaljno.Columns["id"].Visible = false;
                    if (postaviNaBonGridStanje)
                    {
                        dgvBon.Rows[dgvBon.SelectedRows[0].Index].Cells["stanje"].Value = (bon + iskoristeno);
                    }
                }

                lblUkupnoBon.Text = string.Format("Ukupno bon: {0:N2} kn", bon);
                lblUkupnoPotroseno.Text = string.Format("Ukupno iskoristeno: {0:N2} kn", (iskoristeno * (-1)));
                lblPreostaloZaIskoristiti.Text = string.Format("Ukupno preostalo: {0:N2} kn", (bon + iskoristeno));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnBon_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO tu trebam otvoriti formu za upis bona
                if (dgvBon.Rows.Count == 0)
                {
                    MessageBox.Show("Niste odabrali partnera.");
                    return;
                }

                frmBonUpis f = new frmBonUpis();
                f.idPartnersBon = 0;
                if (dgvDetaljno.Rows.Count > 0 && dgvDetaljno.SelectedRows[0].Index >= 0)
                {
                    if (MessageBox.Show("Želite urediti odabrani", "Bon", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        f.idPartnersBon = (int)dgvDetaljno.Rows[dgvDetaljno.SelectedRows[0].Index].Cells["id"].Value;
                    }
                }

                f.idPartner = Convert.ToInt32(dgvBon.Rows[dgvBon.SelectedRows[0].Index].Cells["id_partner"].Value);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //bool bon = ((int)f.cmbBon.SelectedValue == 1 ? true : false);
                    //decimal iznos = 0, stanje = 0;
                    //decimal.TryParse(f.txtIznos.Text, out iznos);
                    //decimal.TryParse(dgvBon.Rows[dgvBon.SelectedRows[0].Index].Cells["stanje"].Value.ToString(), out stanje);

                    //if (bon)
                    //{
                    //    dgvBon.Rows[dgvBon.SelectedRows[0].Index].Cells["stanje"].Value = (stanje + iznos);
                    //}
                    //else
                    //{
                    //    dgvBon.Rows[dgvBon.SelectedRows[0].Index].Cells["stanje"].Value = (stanje - iznos);
                    //}

                    SelectionChangedOnPartner(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}