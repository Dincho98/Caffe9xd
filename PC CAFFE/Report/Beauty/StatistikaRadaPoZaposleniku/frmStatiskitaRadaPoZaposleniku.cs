using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Beauty.StatistikaRadaPoZaposleniku
{
    public partial class frmStatistikaRadaPoZaposleniku : Form
    {
        public frmStatistikaRadaPoZaposleniku()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmStatistikaRadaPoZaposleniku_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            int trenutnaGodina = Util.Korisno.GodinaKojaSeKoristiUbazi;
            dtpDatumOd.MinDate = new DateTime(trenutnaGodina, 1, 1);
            dtpDatumOd.MaxDate = new DateTime(trenutnaGodina, 12, 31);
            dtpDatumDo.MinDate = dtpDatumOd.MinDate;
            dtpDatumDo.MaxDate = dtpDatumOd.MaxDate;

            string sql = string.Format(@"select 0 as id_zaposlenik, 'Svi' as name, 0 as sort
union all
select id_zaposlenik, concat(ime, ' ', prezime) as name, 1 as sort
from zaposlenici
where aktivan = 'DA';");

            DataSet dsZaposlenici = classSQL.select(sql, "zaposlenici");

            if (dsZaposlenici != null && dsZaposlenici.Tables.Count > 0 && dsZaposlenici.Tables[0] != null && dsZaposlenici.Tables[0].Rows.Count > 0)
            {
                cmbZaposlenici.ValueMember = "id_zaposlenik";
                cmbZaposlenici.DisplayMember = "name";
                cmbZaposlenici.DataSource = dsZaposlenici.Tables[0];
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                statistikaRadaPoZaposleniku.Clear();

                string sql = string.Format(@"select
row_number() OVER (order by r.datum_racuna asc) as rb,
concat(r.broj_racuna, '/', d.ime_ducana, '/', b.ime_blagajne) as racun,
r.datum_racuna,
case when r.beauty_partner != 0 then
	case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
else 'Partner nije upisan' end as osoba,
rs.sifra_robe as sifra,
roba.naziv as naziv_artikla,
replace(rs.kolicina, ',','.')::numeric as kolicina,
rs.mpc::numeric as mpc,
replace(rs.rabat, ',','.')::numeric as rabat,
round((rs.mpc::numeric - (rs.mpc::numeric * replace(rs.rabat, ',','.')::numeric / 100 )) * replace(rs.kolicina, ',','.')::numeric, 2) as ukupno
from racuni r
right join racun_stavke rs on r.broj_racuna = rs.broj_racuna and r.id_ducan = rs.id_ducan and r.id_kasa = rs.id_blagajna
left join ducan d on r.id_ducan = d.id_ducan
left join blagajna b on r.id_ducan = b.id_ducan and r.id_kasa = b.id_blagajna
left join roba on rs.sifra_robe = roba.sifra
left join partners p on r.beauty_partner = p.id_partner
where cast(r.datum_racuna as date) between '{0}' and '{1}'
and case when {2} != 0 then rs.id_izradio = {2} else 1 = 1 end
order by r.datum_racuna asc;",

dtpDatumOd.Value.ToString("yyyy-MM-dd"),
dtpDatumDo.Value.ToString("yyyy-MM-dd"),
cmbZaposlenici.SelectedValue);

                classSQL.NpgAdatpter(sql).Fill(statistikaRadaPoZaposleniku, "dtStatistikaRadaPoZaposleniku");

                string filter = string.Format(@"Filter -> Od datuma {0} do datuma {1}", dtpDatumOd.Value.ToString("dd.MM.yyyy."), dtpDatumDo.Value.ToString("dd.MM.yyyy."));

                if ((int)cmbZaposlenici.SelectedValue != 0)
                {
                    filter += string.Format(" za zaposlenika {0}", cmbZaposlenici.Text);
                }
                else
                {
                    filter += "za sve zaposlenike";
                }

                ReportParameter p1 = new ReportParameter("fiter", filter);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

                this.reportViewer1.RefreshReport();
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