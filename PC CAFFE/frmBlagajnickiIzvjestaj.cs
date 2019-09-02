using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmBlagajnickiIzvjestaj : Form
    {
        public frmBlagajnickiIzvjestaj()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string[] strArr = new string[] { "POČETNO STANJE", "POZAJMNICA", "POCETNI POLOG BLAGAJNE", "PROMET BLAGAJNE - R", "PROMET BLAGAJNE" };
        private string Dokumenat = "";
        private DataTable DTblagajnicki_izvjestaj = new DataTable();

        private void frmBlagajnickiIzvjestaj_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            if (Util.Korisno.GetDopustenjeZaposlenika() <= 2)
            {
                btnEdit.Enabled = true;
                btnEdit.Visible = true;
                btnCancel.Enabled = true;
                btnCancel.Visible = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnEdit.Visible = false;
                btnCancel.Enabled = false;
                btnCancel.Visible = false;
            }

            DateTime DT;
            DateTime.TryParse(DateTime.Now.AddMonths(-2).ToString("yyyy-MM-01 0:00:01"), out DT);
            odDatuma.Value = DT;

            string sqlPrometBlagajne = "";
            if (Util.Korisno.GetDopustenjeZaposlenika() == 1)
            {
                sqlPrometBlagajne = @"select 'PROMET BLAGAJNE' as doc, 'Promet blagajne' as naziv, 0 as sort
    union";
            }
            string sql = string.Format(@"select * from (
    {0}
    select 'UPLATA UTRŠKA' as doc, 'Novi unos uplate utrška' as naziv, 1 as sort
    union
    select 'POLOG ZAJMA' as doc, 'Novi unos polog zajma' as naziv, 2 as sort
    union
    select 'ISPLATA PO GOT RN' as doc, 'Novi unos isplate po GOT. RN.' as naziv, 3 as sort
    union
    select 'POČETNO STANJE' as doc, 'Novi unos početnog stanja' as naziv, 4 as sort
    union
    select 'POZAJMNICA' as doc, 'Novi unos pozajmnice' as naziv, 5 as sort
    union
    select 'POVRAT POZAJMNICE' as doc, 'Povrat pozajmnice' as naziv, 6 as sort
    union
    select 'PROMET BLAGAJNE - R' as doc, 'Promet blagajne - ručno' as naziv, 7 as sort
) x
order by x.sort asc;", sqlPrometBlagajne);

            DataSet dsBlag = classSQL.select(sql, "blag");

            if (dsBlag != null && dsBlag.Tables.Count > 0 && dsBlag.Tables[0] != null)
            {
                cmbOdabir.ValueMember = "doc";
                cmbOdabir.DisplayMember = "naziv";
                cmbOdabir.DataSource = dsBlag.Tables[0];
            }

            EnableDisable(false);
            PopuniDataGrid();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (txtIznos.Text.Length == 0)
                return;

            decimal u;
            decimal.TryParse(txtIznos.Text, out u);

            string sql = "";
            if (strArr.Contains(Dokumenat))
            {
                int rb = Util.Korisno.dohvatiZadnjiRedniBrojUBlagajnickomeIzvjestaju(true);

                sql =
                "INSERT INTO blagajnicki_izvjestaj (" +
                    "datum, dokumenat, oznaka_dokumenta, uplaceno, izdatak, novo, editirano, rb";
                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += ", id_partner"; //id_partner
                }

                sql += string.Format(@")
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '0', '1', '0', {4}",
                        dtDatumVrijeme.Value.ToString("yyyy-MM-dd H:mm:ss"),
                        Dokumenat,
                        txtOznaka.Text,
                        u.ToString().Replace(",", "."),
                        rb); //editirano

                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += string.Format(", '{0}'", Properties.Settings.Default.id_partner); //id_partner
                }

                sql += ")";

                if (btnSpremi.Tag != null && btnSpremi.Tag.ToString().Length > 0)
                {
                    int ID = 0;
                    if (int.TryParse(btnSpremi.Tag.ToString(), out ID) && ID > 0)
                    {
                        rb = 0;
                        int.TryParse(dgv.CurrentRow.Cells["rb"].Value.ToString(), out rb);
                        sql = string.Format(@"update blagajnicki_izvjestaj
set
    datum = '{0:yyyy-MM-dd HH:mm:ss}',
    dokumenat = '{1}',
    oznaka_dokumenta = '{2}',
    uplaceno = {3},
    izdatak = 0,
    novo = '0',
    editirano = '1',
    rb = {4},
    id_partner = {5}
where id = {6};", dtDatumVrijeme.Value, Dokumenat, txtOznaka.Text, u.ToString().Replace(",", "."), rb, (Properties.Settings.Default.id_partner.Length == 0 ? "null" : Properties.Settings.Default.id_partner), ID);
                    }
                }

                classSQL.insert(sql);
            }
            else
            {
                int rb = Util.Korisno.dohvatiZadnjiRedniBrojUBlagajnickomeIzvjestaju(false);

                sql = "INSERT INTO blagajnicki_izvjestaj (" +
                    "datum,dokumenat,oznaka_dokumenta,uplaceno,izdatak,novo,editirano, rb";
                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += ", id_partner"; //id_partner
                }

                sql += string.Format(@")
VALUES
( '{0}', '{1}', '{2}', '0', '{3}', '1', '0', {4}",
                    dtDatumVrijeme.Value.ToString("yyyy-MM-dd H:mm:ss"),
                    Dokumenat,
                    txtOznaka.Text,
                    u.ToString().Replace(",", "."),
                    rb);

                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += string.Format(", '{0}'", Properties.Settings.Default.id_partner); //id_partner
                }
                sql += ")";

                if (btnSpremi.Tag != null && btnSpremi.Tag.ToString().Length > 0)
                {
                    int ID = 0;
                    if (int.TryParse(btnSpremi.Tag.ToString(), out ID) && ID > 0)
                    {
                        rb = 0;
                        int.TryParse(dgv.CurrentRow.Cells["rb"].Value.ToString(), out rb);
                        sql = string.Format(@"update blagajnicki_izvjestaj
set
    datum = '{0:yyyy-MM-dd HH:mm:ss}',
    dokumenat = '{1}',
    oznaka_dokumenta = '{2}',
    uplaceno = 0,
    izdatak = {3},
    novo = '0',
    editirano = '1',
    rb = {4},
    id_partner = {5}
where id = {6};", dtDatumVrijeme.Value, Dokumenat, txtOznaka.Text, u.ToString().Replace(",", "."), rb, (Properties.Settings.Default.id_partner.Length == 0 ? "null" : Properties.Settings.Default.id_partner), ID);
                    }
                }

                classSQL.insert(sql);
            }

            EnableDisable(false);

            MessageBox.Show("Spremljeno", "Spremljeno");
            PopuniDataGrid();
        }

        private void EnableDisable(bool b)
        {
            if (btnEdit.Visible)
            {
                btnEdit.Enabled = true;
                cmbOdabir.Enabled = true;
                btnSpremi.Tag = null;
            }

            //cmbOdabir.SelectedValue = (cmbOdabir.Items.Contains("PROMET BLAGAJNE") ? "PROMET BLAGAJNE" : "UPLATA UTRŠKA");
            txtIznos.Text = "";
            txtOznaka.Text = "";
            txtPartnerNaziv.Text = "";
            Properties.Settings.Default.id_partner = null;

            if (b)
                txtIznos.Focus();
        }

        public void PopuniDataGrid()
        {
            string filter = string.Format(@"WHERE cast(datum as date) >= '{0}' AND cast(datum as date) <= '{1}'",
                odDatuma.Value.ToString("yyyy-MM-dd"),
                doDatuma.Value.ToString("yyyy-MM-dd"));

            string sql = string.Format(@"SELECT * FROM
(
    SELECT id, datum, dokumenat,
    CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
    THEN partners.ime_tvrtke
    ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
    END AS oznaka_dokumenta,
    CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE', 'PROMET BLAGAJNE - R')
    THEN uplaceno
    ELSE '0' END as uplaceno, izdatak
    FROM blagajnicki_izvjestaj
    LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
    WHERE dokumenat <> 'PROMET BLAGAJNE'

    UNION ALL

    SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
    concat(MIN(CAST(racuni.broj_racuna AS INT)), '-', MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

    SUM((CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC) * CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno, '0' as izdatak

    FROM racuni
    LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
    WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null)
    GROUP BY date_trunc('day', datum_racuna)
)
bm
{0}
ORDER BY datum ASC;",
filter);

            sql = string.Format(@"select bi.id, bi.datum, bi.dokumenat, case when bi.id_partner is null then bi.oznaka_dokumenta else
	concat(bi.oznaka_dokumenta, ' - ', case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end)
	end as oznaka_dokumenta, bi.uplaceno, bi.izdatak, bi.rb, bi.id_partner
from blagajnicki_izvjestaj bi
left join partners p on bi.id_partner = p.id_partner
{0}
order by datum asc;",
                filter);

            DTblagajnicki_izvjestaj = classSQL.select(sql, "blagajnicki_izvjestaj").Tables[0];

            DateTime dat;
            decimal u, i, uuk = 0, iuk = 0;

            if (dgv.Rows.Count > 0)
                dgv.Rows.Clear();

            foreach (DataRow r in DTblagajnicki_izvjestaj.Rows)
            {
                decimal.TryParse(r["uplaceno"].ToString(), out u);
                decimal.TryParse(r["izdatak"].ToString(), out i);
                DateTime.TryParse(r["datum"].ToString(), out dat);

                uuk = u + uuk;
                iuk = i + iuk;

                dgv.Rows.Add(r["rb"].ToString(),
                    dat,
                    r["dokumenat"].ToString(),
                    r["oznaka_dokumenta"].ToString(),
                    Math.Round(u, 3).ToString("#0.00"),
                    Math.Round(i, 3).ToString("#0.00"),
                    r["id"].ToString(),
                    r["id_partner"].ToString());
            }

            lblIzdatak.Text = "IZDATAK: " + iuk.ToString("N2") + " kn";
            lblUplaceno.Text = "UKUPNO: " + uuk.ToString("N2") + " kn";
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            PopuniDataGrid();
        }

        private void dgv_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            return;

            //            if (dgv.CurrentRow.Cells["id"].FormattedValue.ToString() == "")
            //                return;
            //            if (dgv.CurrentRow.Cells["dokument"].FormattedValue.ToString() == "PROMET BLAGAJNE")
            //            {
            //                MessageBox.Show("Zabranjeno polje!!!");
            //                dgv.CurrentRow.Cells["dokument"].Value = "PROMET BLAGAJNE";
            //                return;
            //            }

            //            if (dgv.CurrentCell.ColumnIndex == 4)
            //            {
            //                try
            //                {
            //                    string sql = string.Format(@"UPDATE blagajnicki_izvjestaj
            //SET
            //    izdatak = '{0}',
            //    editirano = '1'
            //WHERE id = '{1}';",
            //dgv.Rows[e.RowIndex].Cells["izdatak"].FormattedValue.ToString().Replace(",", "."),
            //dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
            //                    classSQL.update(sql);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show(ex.ToString());
            //                }
            //            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.BlagajnickiIzvjestaj.frmBlagajnickiIzvjestajReport blai = new Report.BlagajnickiIzvjestaj.frmBlagajnickiIzvjestajReport();
            blai.tdOdDatuma.Value = odDatuma.Value;
            blai.tdDoDatuma.Value = doDatuma.Value;
            blai.btnTrazi_Click(null, null);
            blai.ShowDialog();
        }

        private void SetPartner(int idPartner)
        {
            if (idPartner != 0)
            {
                string sql = string.Format("select * from partners where id_partner = '{0}';", idPartner);
                txtPartnerNaziv.Text = classSQL.select(sql, "partners").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                Properties.Settings.Default.id_partner = idPartner.ToString();
            }
            else
            {
                txtPartnerNaziv.Text = "";
                Properties.Settings.Default.id_partner = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                frmPartnerTrazi partner = new frmPartnerTrazi();
                partner.ShowDialog();

                if (Properties.Settings.Default.id_partner != null && Properties.Settings.Default.id_partner.ToString().Length > 0)
                {
                    SetPartner(Convert.ToInt32(Properties.Settings.Default.id_partner));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbOdabir_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOdabir.SelectedValue == null)
                    return;

                Dokumenat = cmbOdabir.SelectedValue.ToString();
                lblIznos.Text = Dokumenat;
                this.Text = "Blagajnički izvještaj - " + Dokumenat;
                EnableDisable(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnIspisOdabranog_Click(object sender, EventArgs e)
        {
            try
            {
                decimal iznos = 0;
                string partner = "", broj_dokumenta = "", grad = "";
                DateTime datum_dokumenta = new DateTime();

                DateTime.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["datum"].Value.ToString(), out datum_dokumenta);

                int id = 0;
                int.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["id"].Value.ToString(), out id);

                string sql = string.Format(@"select case when partners.vrsta_korisnika = 1 then partners.ime_tvrtke else concat(partners.ime, ' ', partners.prezime) end as partner,
blagajnicki_izvjestaj.oznaka_dokumenta
from blagajnicki_izvjestaj
left join partners on blagajnicki_izvjestaj.id_partner = partners.id_partner
where blagajnicki_izvjestaj.id = {0};",
id);
                DataSet dsPartner = classSQL.select(sql, "partners");

                if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
                {
                    partner = dsPartner.Tables[0].Rows[0]["partner"].ToString();
                }

                if (id >= 1)
                {
                    broj_dokumenta = dsPartner.Tables[0].Rows[0]["oznaka_dokumenta"].ToString();
                }
                else
                {
                    broj_dokumenta = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["oznaka"].Value.ToString();
                }

                sql = string.Format("select grad from grad where id_grad = {0}", Class.PodaciTvrtka.gradPoslovnicaId);
                DataSet dsGrad = classSQL.select(sql, "grad");
                if (dsGrad != null && dsGrad.Tables.Count > 0 && dsGrad.Tables[0] != null && dsGrad.Tables[0].Rows.Count > 0)
                {
                    grad = dsGrad.Tables[0].Rows[0]["grad"].ToString();
                }

                Report.BlagajnickiIzvjestaj.frmBlagajnickaIsplatnica f = new Report.BlagajnickiIzvjestaj.frmBlagajnickaIsplatnica();

                f.dokument = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["dokument"].Value.ToString();

                if (f.dokuments.Contains(f.dokument))
                    decimal.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Uplaceno"].Value.ToString(), out iznos);
                else
                    decimal.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["izdatak"].Value.ToString(), out iznos);

                f.broj = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["rb"].Value.ToString();
                f.iznos = iznos;
                f.partner = partner;
                f.broj_dokumenta = broj_dokumenta;
                f.mjesto_i_datum = grad + (grad.Length > 0 ? ", " : "") + datum_dokumenta.ToString("dd.MM.yyyy.");
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0 || dgv.CurrentRow.Index < 0)
                return;

            if (dgv.CurrentRow.Cells["dokument"].FormattedValue.ToString() == "PROMET BLAGAJNE" && Util.Korisno.GetDopustenjeZaposlenika() != 1)
            {
                MessageBox.Show("Odabrano polje nije dopušteno uređivati.");
                return;
            }

            if (MessageBox.Show("Želite urediti odabrani unos?", "Blagajnički izvještaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbOdabir.SelectedValue = dgv.CurrentRow.Cells["dokument"].Value;
                int idPartner = 0;
                int.TryParse(dgv.CurrentRow.Cells["id_partner"].Value.ToString(), out idPartner);
                SetPartner(idPartner);

                btnSpremi.Tag = dgv.CurrentRow.Cells["id"].Value;
                decimal iznos = 0;
                if (strArr.Contains(cmbOdabir.SelectedValue.ToString()))
                {
                    //uplaceno
                    decimal.TryParse(dgv.CurrentRow.Cells["Uplaceno"].Value.ToString(), out iznos);
                }
                else
                {
                    //isplaceno
                    decimal.TryParse(dgv.CurrentRow.Cells["izdatak"].Value.ToString(), out iznos);
                }

                txtIznos.Text = iznos.ToString("#0.00");

                string oznaka = dgv.CurrentRow.Cells["oznaka"].Value.ToString();
                if (idPartner != 0)
                {
                    oznaka = oznaka.Remove(oznaka.Length - txtPartnerNaziv.Text.Length - 3);
                }
                txtOznaka.Text = oznaka;
                DateTime dt = new DateTime();
                DateTime.TryParse(dgv.CurrentRow.Cells["datum"].Value.ToString(), out dt);
                dtDatumVrijeme.Value = dt;

                cmbOdabir.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                EnableDisable(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}