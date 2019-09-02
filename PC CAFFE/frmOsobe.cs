using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmOsobe : Form
    {
        public Caffe.frmCaffe MainForm { get; set; }
        private string _ids = "";
        public string ids { get { return _ids; } }

        private List<OsobeDugNaplata> _osobe;
        public List<OsobeDugNaplata> osobe { get { return _osobe; } }
        private int _idPartner = 0;
        public int idPartner { get { return _idPartner; } }
        private bool _dodajOsobu = false;
        public bool dodajOsobu { get { return _dodajOsobu; } }
        public bool openInMenu = false;
        private DataSet dsArtikli;
        private DataTable dtDug;

        private int god;

        public frmOsobe()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmOsobe_Load(object sender, EventArgs e)
        {
            try
            {
                this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

                txtNaziv.Text = "";
                txtOsnovno.Text = "";
                txtOstalo.Text = "";

                god = Util.Korisno.GodinaKojaSeKoristiUbazi;
                dtpDugOdabirDatuma.MinDate = new DateTime(god, 1, 1, 0, 0, 0, 0);
                dtpDugOdabirDatuma.MaxDate = new DateTime(god, 12, 31, 23, 59, 59, 999);

                cmbArtikl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbArtikl.AutoCompleteSource = AutoCompleteSource.ListItems;

                btnSvi.PerformClick();
                btnOdustani_Click(new object(), new EventArgs());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmOsobe_Resize(object sender, EventArgs e)
        {
            try
            {
                if (splitContainer.Width > 0)
                    splitContainer.SplitterDistance = splitContainer.Width / 2;
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

        private void btnFilters_Click(object sender, EventArgs e)
        {
            try
            {
                int filterVrsta = 1;
                int.TryParse((sender as Button).Tag.ToString(), out filterVrsta);
                Filtriraj(filterVrsta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void getArtikli()
        {
            try
            {
                string sql = @"SELECT
'' as [Datum], 0 as id_djelatnik, '' as [Izradio], roba.naziv as [Naziv], 0::numeric as [Kol], roba.mpc::numeric as [Cijena], roba.mpc::numeric / (1 zbroj (replace(roba.porez, ',','.')::numeric / 100)) as vpc, roba.nbc, replace(roba.porez, ',','.')::numeric as pdv, roba.sifra, replace(roba.porez_potrosnja, ',','.')::numeric as porez_potrosnja,
0 as dod, id_grupa, 0 as id, 0 as naplaceno_broj_racunom
FROM roba
order by roba.naziv asc;";

                dsArtikli = classSQL.select(sql, "roba");

                if (dsArtikli != null && dsArtikli.Tables.Count > 0 && dsArtikli.Tables[0] != null && dsArtikli.Tables[0].Rows.Count > 0)
                {
                    cmbArtikl.DataSource = dsArtikli.Tables[0];
                    cmbArtikl.ValueMember = "sifra";
                    cmbArtikl.DisplayMember = "Naziv";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void getDjelatnici()
        {
            try
            {
                string sql = string.Format(@"SELECT id_zaposlenik, CONCAT(ime,' ',prezime) as ime, 1 as sort
FROM zaposlenici");

                DataSet dsZaposlenici = classSQL.select(string.Format(@"{0}
WHERE aktivan='DA' AND (zaporka <> ',00000000000000000000,' and zaposlenici.ime not in ('Održavanje','PC') and zaposlenici.prezime not in ('Održavanje','PC'))
order by sort, ime;", sql), "zaposlenici");
                if (dsZaposlenici != null && dsZaposlenici.Tables.Count > 0 && dsZaposlenici.Tables[0] != null && dsZaposlenici.Tables[0].Rows.Count > 0)
                {
                    cmbIzradio.DataSource = dsZaposlenici.Tables[0];
                    cmbIzradio.ValueMember = "id_zaposlenik";
                    cmbIzradio.DisplayMember = "ime";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Filtriraj(int filterVrsta)
        {
            try
            {
                string sql = string.Format(@"select p.id_partner as [ID], case when p.vrsta_korisnika = 1 then p.ime_tvrtke else p.ime || ' ' || p.prezime end as [Ime], g.grad as [Grad], p.adresa as [Adresa],
p.tel as [Tel], p.mob as [Mob], p.email as [Email], p.datum_rodenja as [Datum rođenja], g.posta as [Pošta], p.spol, p.trudnoca, p.kontracepcija, p.hormonalni_nadomjestak
from partners p
left
join grad g on p.id_grad = g.id_grad");

                string filter = string.Format(@"where case when p.vrsta_korisnika = 1 then lower(p.ime_tvrtke) like '%{0}%' else lower(p.ime) like '%{0}%' or lower(p.prezime) like '%{0}%' end LIMIT 100;", txtFilter.Text.ToLower());

                if (openInMenu)
                    filter = string.Format(@"where case when p.vrsta_korisnika = 1 then lower(p.ime_tvrtke) like '%{0}%' else lower(p.ime) like '%{0}%' or lower(p.prezime) like '%{0}%' end;", txtFilter.Text.ToLower());

                if (txtFilter.Text.Contains(" "))
                {
                    string pomocniFilterT = "";
                    string pomocniFilterO = "";

                    string[] filters = txtFilter.Text.Split(' ');
                    foreach (string fil in filters)
                    {
                        if (pomocniFilterT.Length > 0)
                            pomocniFilterT += " or ";

                        pomocniFilterT += string.Format("lower(p.ime_tvrtke) like '%{0}%'", fil);

                        if (pomocniFilterO.Length > 0)
                            pomocniFilterO += " and ";

                        pomocniFilterO += string.Format("(lower(p.ime) like '%{0}%' or lower(p.prezime) like '%{0}%')", fil);
                    }

                    filter = string.Format(@"where case when p.vrsta_korisnika = 1 then {0} else {1} end LIMIT 100;", pomocniFilterT, pomocniFilterO);
                }

                if (filterVrsta == 1)
                {
                    sql = string.Format(@"{0}
{1}", sql, filter);
                }

                if (filterVrsta == 2)
                {
                    DateTime danasnjiDatum = DateTime.Now;
                    int mjesecDanasnjegDatuma = danasnjiDatum.Month;
                    int danDanasnjegDatuma = danasnjiDatum.Day;
                    sql = string.Format(@"{0}
where extract(month from p.datum_rodenja) = {1} and extract(day from p.datum_rodenja) = {2};", sql, mjesecDanasnjegDatuma, danDanasnjegDatuma);
                }

                if (filterVrsta == 3)
                {
                    sql = string.Format(@"{0}
left join beauty_dug bd on p.id_partner = bd.id_partner
where bd.naplaceno_broj_racunom = 0
group by [ID], [Ime], [Grad], [Adresa], [Tel], [Mob], [Email], [Datum rođenja], [Pošta];", sql);
                }

                DataSet dsPartners = classSQL.select(sql, "partners");

                if (dsPartners != null && dsPartners.Tables.Count > 0 && dsPartners.Tables[0] != null && dsPartners.Tables[0].Rows.Count > 0)
                {
                    dgvOsobe.DataSource = dsPartners.Tables[0];
                    dataGridView_Click(new object(), new EventArgs());

                    dgvOsobe.Columns["ID"].Visible = false;
                    dgvOsobe.Columns["Tel"].Visible = false;
                    dgvOsobe.Columns["Mob"].Visible = false;
                    dgvOsobe.Columns["Email"].Visible = false;
                    dgvOsobe.Columns["Datum rođenja"].Visible = false;
                    dgvOsobe.Columns["Pošta"].Visible = false;
                    dgvOsobe.Columns["spol"].Visible = false;
                    dgvOsobe.Columns["trudnoca"].Visible = false;
                    dgvOsobe.Columns["kontracepcija"].Visible = false;
                    dgvOsobe.Columns["hormonalni_nadomjestak"].Visible = false;
                }
                else
                {
                    dgvOsobe.DataSource = null;
                    dgvOsobe.Rows.Clear();
                    if (filterVrsta == 2)
                    {
                        MessageBox.Show("Na današnji dan korisnici nemaju rođendan.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOsobe.Rows.Count == 0)
                {
                    //TODO: Obrisati detaljni prikaz, jer nema nit jednog partnera odabranog i da se nebude prikazivale informacije o prethodno odabranome.
                    return;
                }
                int selectedRowIndex = dgvOsobe.CurrentCell.RowIndex;
                if (selectedRowIndex < 0)
                {
                    return;
                }

                DataGridViewRow dRow = dgvOsobe.Rows[selectedRowIndex];
                int.TryParse(dRow.Cells["ID"].Value.ToString(), out _idPartner);

                string sql = string.Format("select * from selectOsobeBrojDugZadnji({0}, '{1}');", _idPartner, Class.PodaciZaSpajanjeCompaktna.remoteServer);

                DataSet dsBrojDugZadnji = classSQL.select(sql, "dugovanje");
                DataTable dtBrojDugZadnji = null;
                if (dsBrojDugZadnji != null && dsBrojDugZadnji.Tables.Count > 0 && dsBrojDugZadnji.Tables[0] != null && dsBrojDugZadnji.Tables[0].Rows.Count > 0)
                    dtBrojDugZadnji = dsBrojDugZadnji.Tables[0];

                txtNaziv.Text = dRow.Cells["Ime"].Value.ToString();

                string osnovno = "";
                string ostalo = "";
                if (dRow.Cells["Adresa"].Value.ToString().Trim().Length > 0)
                {
                    osnovno += dRow.Cells["Adresa"].Value.ToString();
                }

                if (dRow.Cells["Pošta"].Value.ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;

                    osnovno += dRow.Cells["Pošta"].Value.ToString();
                }

                if (dRow.Cells["Grad"].Value.ToString().Trim().Length > 0)
                {
                    osnovno += " " + dRow.Cells["Grad"].Value.ToString();
                }

                if (dRow.Cells["Tel"].Value.ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "Tel.: " + dRow.Cells["Tel"].Value.ToString();
                }

                if (dRow.Cells["Mob"].Value.ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "Mob.: " + dRow.Cells["Mob"].Value.ToString();
                }

                if (dRow.Cells["Email"].Value.ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "Email: " + dRow.Cells["Email"].Value.ToString();
                }

                if (dRow.Cells["Datum rođenja"].Value.ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "Datum rođenja: " + Convert.ToDateTime(dRow.Cells["Datum rođenja"].Value).ToString("dd.MM.yyyy.");
                }

                if (dtBrojDugZadnji != null && dtBrojDugZadnji.Rows[0]["zadnji_posjet"].ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;

                    osnovno += "Zadnji posjet: " + Convert.ToDateTime(dtBrojDugZadnji.Rows[0]["zadnji_posjet"]).ToString("dd.MM.yyyy.");
                }

                if (dtBrojDugZadnji != null && dtBrojDugZadnji.Rows[0]["br_posjeta"].ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "Br. posjeta: " + dtBrojDugZadnji.Rows[0]["br_posjeta"].ToString();
                }

                if (dtBrojDugZadnji != null && dtBrojDugZadnji.Rows[0]["dug"].ToString().Trim().Length > 0)
                {
                    if (osnovno.Trim().Length > 0)
                        osnovno += Environment.NewLine;
                    osnovno += "DUG: " + dtBrojDugZadnji.Rows[0]["dug"].ToString();
                }

                if (dRow.Cells["spol"].Value.ToString().Trim().Length > 0)
                {
                    if (ostalo.Trim().Length > 0)
                        ostalo += Environment.NewLine;
                    ostalo += "Spol: " + (dRow.Cells["spol"].Value.ToString() == "O" ? "Drugo" : (dRow.Cells["spol"].Value.ToString() == "M" ? "Muški" : "Ženski"));
                }
                if (dRow.Cells["trudnoca"].Value.ToString().Trim().Length > 0)
                {
                    if (ostalo.Trim().Length > 0)
                        ostalo += Environment.NewLine;
                    ostalo += "Trudnoća: " + dRow.Cells["trudnoca"].Value.ToString();
                }
                if (dRow.Cells["kontracepcija"].Value.ToString().Trim().Length > 0)
                {
                    if (ostalo.Trim().Length > 0)
                        ostalo += Environment.NewLine;
                    ostalo += "Kontracepcija: " + dRow.Cells["kontracepcija"].Value.ToString();
                }
                if (dRow.Cells["hormonalni_nadomjestak"].Value.ToString().Trim().Length > 0)
                {
                    if (ostalo.Trim().Length > 0)
                        ostalo += Environment.NewLine;
                    ostalo += "Hormonalni nadomjestak: " + dRow.Cells["hormonalni_nadomjestak"].Value.ToString();
                }

                txtOsnovno.Text = osnovno;
                txtOstalo.Text = ostalo;

                sql = string.Format(@"SELECT datum as [Datum], id_djelatnik, izradio as [Izradio], naziv as [Naziv], kol as [Kol], cijena as [Cijena], vpc, nbc, pdv, sifra, porez_potrosnja, dod, id_grupa, id, naplaceno_broj_racunom FROM
selectosobedugdetaljno({0}, '{1}');", _idPartner, Class.PodaciZaSpajanjeCompaktna.remoteServer);

                DataSet dsDug = classSQL.select(sql, "beauty_dug");

                if (dsDug != null && dsDug.Tables.Count > 0 && dsDug.Tables[0] != null && dsDug.Tables[0].Rows.Count > 0)
                {
                    dtDug = dsDug.Tables[0];
                    setDgvDugovanje();
                }
                else
                {
                    setDgvDugovanje(true);
                }

                getKarticaOsobe();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDugButtons_Click(object sender, EventArgs e)
        {
            try
            {
                int dugVrsta = 1;
                int.TryParse((sender as Button).Tag.ToString(), out dugVrsta);

                if (dugVrsta <= 2 || dugVrsta == 4)
                {
                    btnDugNaplati.Enabled = false;
                    btnDugNovi.Enabled = false;
                    btnDugObrisi.Enabled = false;
                    btnDugUredi.Enabled = false;
                }

                if (openInMenu)
                    btnDugNaplati.Enabled = false;

                if (dugVrsta == 1)
                {
                    btnOdustani.PerformClick();

                    pnlDugBottom1.Visible = true;
                    pnlDugBottom1.BringToFront();
                }
                else if (dugVrsta == 2)
                {
                    if (dgvDugovanje.Rows.Count > 0 && dgvDugovanje.SelectedRows.Count == 1)
                    {
                        if (Convert.ToInt32(dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells["naplaceno_broj_racunom"].Value.ToString()) > 0)
                        {
                            MessageBox.Show("Nije dopušteno uređivanje naplačenog dugovanja.");
                            return;
                        }

                        btnOdustani.PerformClick();
                        btnDodaj.Text = "Spremi";
                        cmbArtikl.Enabled = false;
                        cmbIzradio.SelectedValue = dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells["id_djelatnik"].Value;
                        dtpDugOdabirDatuma.Value = (DateTime)dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells["Datum"].Value;
                        txtDugKolicina.Text = ((decimal)dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells["Kol"].Value).ToString("#0.00");
                        pnlDugBottom1.Visible = true;
                        pnlDugBottom1.BringToFront();
                    }
                    else
                    {
                        btnDugNaplati.Enabled = false;
                        btnDugNovi.Enabled = true;
                        btnDugObrisi.Enabled = true;
                        btnDugUredi.Enabled = true;
                    }
                }
                else if (dugVrsta == 3)
                {
                    if (dgvDugovanje.Rows.Count > 0 && dgvDugovanje.SelectedRows.Count == 1)
                    {
                        if (MessageBox.Show("Želite obrisati odabrani dug?", "Brisanje duga", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                        int rowIndex = dgvDugovanje.CurrentCell.RowIndex;
                        int id_beauty_dug = 0;
                        if (dgvDugovanje.Rows[rowIndex].Cells["id"] != null && int.TryParse(dgvDugovanje.Rows[rowIndex].Cells["id"].Value.ToString(), out id_beauty_dug))
                        {
                            DateTime datumDuga = DateTime.Now;
                            bool uspjesnaPretvorbaDatuma = DateTime.TryParse(dgvDugovanje.Rows[rowIndex].Cells["id"].ToString(), out datumDuga);
                            if (uspjesnaPretvorbaDatuma && datumDuga.Year == Util.Korisno.GodinaKojaSeKoristiUbazi)
                            {
                                string sql = string.Format("delete from beauty_dug where id = {0};", id_beauty_dug);
                                classSQL.update(sql);

                                dgvDugovanje.Rows.RemoveAt(rowIndex);
                                dtDug.Rows.RemoveAt(rowIndex);
                            }
                            else
                            {
                                if (!uspjesnaPretvorbaDatuma)
                                {
                                    MessageBox.Show("Greška kod datuma.");
                                }
                                else
                                {
                                    MessageBox.Show("Nije dozvoljeno brisanje duga iz prethodnih godina.");
                                }
                            }
                        }
                        setDgvDugovanje();
                    }
                    else
                    {
                        btnDugNaplati.Enabled = false;
                        btnDugNovi.Enabled = true;
                        btnDugObrisi.Enabled = true;
                        btnDugUredi.Enabled = true;
                    }
                }
                else if (dugVrsta == 4)
                {
                    btnOdustani.PerformClick();
                    foreach (var item in dgvDugovanje.Rows)
                    {
                        (item as DataGridViewRow).Cells["chbNaplataRow"].Value = false;
                    }

                    dgvDugovanje.Columns["chbNaplataRow"].Visible = true;
                    pnlDugBottom2.Visible = true;
                    pnlDugBottom2.BringToFront();
                }

                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbArtikl.SelectedValue == null || cmbArtikl.SelectedValue.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("Nije odabran artikl.");
                    return;
                }
                string sifra = cmbArtikl.SelectedValue.ToString();

                int idDjelatnik = 0;
                if (!int.TryParse(cmbIzradio.SelectedValue.ToString(), out idDjelatnik))
                {
                    MessageBox.Show("Greška kod odabira djelatnika.");
                    return;
                }

                decimal kolicina = 0;
                if (!decimal.TryParse(txtDugKolicina.Text.Trim(), out kolicina) || kolicina <= 0)
                {
                    MessageBox.Show("Greška kod upisa količine.");
                    return;
                }

                DataView dv = new DataView(dsArtikli.Tables[0]);
                dv.RowFilter = string.Format("sifra = '{0}'", sifra);
                DataTable temp = dv.ToTable();

                if (temp != null && temp.Rows.Count > 0)
                {
                    temp.Rows[0]["id_djelatnik"] = idDjelatnik;
                    temp.Rows[0]["Izradio"] = cmbIzradio.Text;
                    temp.Rows[0]["Kol"] = Math.Round(kolicina, 2, MidpointRounding.AwayFromZero).ToString().Replace(",", ".");
                    temp.Rows[0]["Datum"] = dtpDugOdabirDatuma.Value.ToString("yyyy-MM-dd");
                }

                dv = null;

                if (dtDug == null)
                {
                    dtDug = new DataTable();
                    foreach (var item in dsArtikli.Tables[0].Columns)
                    {
                        dtDug.Columns.Add(item.ToString());
                    }
                }

                int id = 0;
                if ((sender as Button).Text == "Spremi")
                {
                    int.TryParse(dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells["id"].Value.ToString(), out id);
                }

                int idDug = spremiDug(temp.Rows[0], id);
                if (temp != null && temp.Rows.Count > 0)
                    temp.Rows[0]["id"] = idDug;

                if (id == 0)
                {
                    dtDug.Rows.Add(temp.Rows[0].ItemArray);
                }
                else
                {
                    var rowsToUpdate =
    dtDug.AsEnumerable().Where(r => r.Field<int>("id") == id);
                    rowsToUpdate.ElementAt(0).SetField("id_djelatnik", idDjelatnik);
                    rowsToUpdate.ElementAt(0).SetField("Izradio", cmbIzradio.Text);
                    rowsToUpdate.ElementAt(0).SetField("Kol", kolicina.ToString());
                    rowsToUpdate.ElementAt(0).SetField("Datum", dtpDugOdabirDatuma.Value.ToString("yyyy-MM-dd"));
                }

                dv = new DataView(dtDug);
                dv.Sort = "Datum desc";
                dtDug = dv.ToTable();

                setDgvDugovanje();

                btnOdustani.PerformClick();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setDgvDugovanje(bool obrisiDtDug = false)
        {
            try
            {
                dgvDugovanje.DataSource = null;
                dgvDugovanje.Rows.Clear();

                if (obrisiDtDug)
                    dtDug = null;

                if (dtDug == null || dtDug.Rows.Count == 0)
                    return;

                dgvDugovanje.DataSource = dtDug;

                dgvDugovanje.Columns["Cijena"].DefaultCellStyle.Format = "N2";
                dgvDugovanje.Columns["Kol"].DefaultCellStyle.Format = "N2";
                dgvDugovanje.Columns["id_djelatnik"].Visible = false;
                dgvDugovanje.Columns["nbc"].Visible = false;
                dgvDugovanje.Columns["pdv"].Visible = false;
                dgvDugovanje.Columns["sifra"].Visible = false;
                dgvDugovanje.Columns["porez_potrosnja"].Visible = false;
                dgvDugovanje.Columns["dod"].Visible = false;
                dgvDugovanje.Columns["id_grupa"].Visible = false;
                dgvDugovanje.Columns["vpc"].Visible = false;
                dgvDugovanje.Columns["id"].Visible = false;
                dgvDugovanje.Columns["naplaceno_broj_racunom"].Visible = false;

                dgvDugovanje.CurrentCell = dgvDugovanje.Rows[0].Cells[1];
                dgvDugovanje.Rows[0].Selected = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                btnDugNaplati.Enabled = true;
                btnDugNovi.Enabled = true;
                btnDugObrisi.Enabled = true;
                btnDugUredi.Enabled = true;

                if (openInMenu)
                    btnDugNaplati.Enabled = false;

                dgvDugovanje.Columns["chbNaplataRow"].Visible = false;

                pnlDugBottom2.Visible = false;
                pnlDugBottom1.Visible = false;
                pnlDugFill.BringToFront();

                txtDugKolicina.Text = "1";
                getDjelatnici();
                getArtikli();
                btnDodaj.Text = "Dodaj";
                cmbArtikl.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int spremiDug(DataRow dRow, int idDug = 0)
        {
            try
            {
                string sql = "";
                if (idDug == 0)
                {
                    sql = string.Format(@"INSERT INTO beauty_dug(
id_ducan, id_blagajna, godina, id_izradio, id_partner, datum_duga,
sifra, id_grupa, id_skladiste, id_djelatnik, nbc, vpc, pdv, mpc,
rabat, porez_potrosnja, kolicina)
VALUES ({0}, {1}, {2}, {3}, {4}, '{5}',
'{6}', {7}, {8}, {9}, {10}, {11}, {12}, {13},
{14}, {15}, {16})
RETURNING id;", Util.Korisno.idDucan, Util.Korisno.idKasa, god, Properties.Settings.Default.id_zaposlenik, _idPartner, dtpDugOdabirDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"),
dRow["sifra"].ToString(), dRow["id_grupa"].ToString(), Class.Postavke.id_default_skladiste, dRow["id_djelatnik"], dRow["nbc"].ToString().Replace(",", "."), dRow["vpc"].ToString().Replace(",", "."), dRow["pdv"].ToString().Replace(",", "."), dRow["Cijena"].ToString().Replace(",", "."),
0, dRow["porez_potrosnja"].ToString().Replace(",", "."), dRow["Kol"].ToString().Replace(",", "."));
                    DataSet dsDug = classSQL.select(sql, "beauty_dug");

                    if (dsDug != null && dsDug.Tables.Count > 0 && dsDug.Tables[0] != null && dsDug.Tables[0].Rows.Count > 0)
                        int.TryParse(dsDug.Tables[0].Rows[0]["id"].ToString(), out idDug);
                }
                else
                {
                    sql = string.Format(@"UPDATE beauty_dug
   SET id_ducan ={0}, id_blagajna ={1}, godina ={2}, id_izradio ={3}, id_partner ={4},
       datum_duga ='{5}', sifra ='{6}', id_grupa ={7}, id_skladiste ={8}, id_djelatnik ={9},
       nbc ={10}, vpc ={11}, pdv ={12}, mpc ={13}, rabat ={14}, porez_potrosnja ={15}, kolicina = {16}
 WHERE id = {17};", Util.Korisno.idDucan, Util.Korisno.idKasa, god, Properties.Settings.Default.id_zaposlenik, _idPartner, dtpDugOdabirDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"), dRow["sifra"].ToString(), dRow["id_grupa"].ToString(), Class.Postavke.id_default_skladiste, dRow["id_djelatnik"], dRow["nbc"].ToString().Replace(",", "."), dRow["vpc"].ToString().Replace(",", "."), dRow["pdv"].ToString().Replace(",", "."), dRow["Cijena"].ToString().Replace(",", "."), 0, dRow["porez_potrosnja"].ToString().Replace(",", "."), dRow["Kol"].ToString().Replace(",", "."), idDug);
                    classSQL.update(sql);
                }

                return idDug;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void getKarticaOsobe()
        {
            try
            {
                //            string sql = @"SELECT pg_namespace.nspname, pg_proc.proname
                //FROM pg_proc, pg_namespace
                //WHERE pg_proc.pronamespace=pg_namespace.oid
                //   AND pg_proc.proname LIKE '%dblink%';";
                //            DataSet dsDbLink = classSQL.select(sql, "dblink");

                //            if (dsDbLink == null || dsDbLink.Tables.Count == 0 || dsDbLink.Tables[0] == null || dsDbLink.Tables[0].Rows.Count == 0) {
                //                sql = @"CREATE EXTENSION dblink;";
                //                classSQL.update(sql);
                //            }

                //                sql = string.Format(@"select ro.naziv as [Usluga], r.datum_racuna as [Datum], z.ime || ' ' || z.prezime as [Izradio]
                //from racun_stavke rs
                //left
                //join racuni r on rs.broj_racuna = r.broj_racuna and rs.id_ducan = r.id_kasa and rs.id_ducan = r.id_ducan
                //left join roba ro on rs.sifra_robe = ro.sifra
                //left join zaposlenici z on rs.id_izradio = z.id_zaposlenik
                //where r.beauty_partner = {0}
                //order by r.datum_racuna asc;", _idPartner);

                string sql = string.Format(@"SELECT * FROM
selectosobekartica({0}, '{1}');", _idPartner, Class.PodaciZaSpajanjeCompaktna.remoteServer);

                DataSet dsKartica = classSQL.select(sql, "kartica");
                if (dsKartica != null && dsKartica.Tables.Count > 0 && dsKartica.Tables[0] != null && dsKartica.Tables[0].Rows.Count > 0)
                {
                    dgvKartica.DataSource = dsKartica.Tables[0];
                }
                else
                {
                    dgvKartica.DataSource = null;
                    dgvKartica.Rows.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnNaplataDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                var checkedRows = from DataGridViewRow r in dgvDugovanje.Rows
                                  where Convert.ToBoolean(r.Cells[0].Value) == true
                                  select r;

                if (checkedRows.Count() == 0)
                {
                    MessageBox.Show("Niste odabrali artikle.");
                    return;
                }

                foreach (var item in checkedRows)
                {
                    if (_ids.Length > 0)
                        _ids += ", ";

                    _ids += item.Cells["id"].Value.ToString().Trim();
                    DateTime datumDuga = DateTime.Now;
                    DateTime.TryParse(item.Cells["datum"].Value.ToString().Trim(), out datumDuga);

                    OsobeDugNaplata osoba = new OsobeDugNaplata()
                    {
                        godina = datumDuga.Year,
                        id = Convert.ToInt32(item.Cells["id"].Value.ToString().Trim())
                    };

                    if (_osobe == null)
                    {
                        _osobe = new List<OsobeDugNaplata>();
                    }
                    _osobe.Add(osoba);

                    double porez = Convert.ToDouble(item.Cells["pdv"].Value.ToString().Trim());
                    double pnp = Convert.ToDouble(item.Cells["porez_potrosnja"].Value.ToString().Trim());
                    double mpc = Convert.ToDouble(item.Cells["Cijena"].Value.ToString().Trim());
                    double pdv_stavka = 0, Porez_potrosnja_stavka = 0;

                    double PreracunataStopaPDV = Convert.ToDouble((100 * porez) / (100 + porez + pnp));
                    pdv_stavka = (mpc * PreracunataStopaPDV) / 100;

                    double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * pnp) / (100 + porez + pnp));
                    Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

                    string nazivArtikla = item.Cells["Izradio"].Value.ToString().Trim() + ", " + item.Cells["Naziv"].Value.ToString().Trim();

                    MainForm.dgw.Rows.Add(
                        nazivArtikla,// DT.Rows[0]["naziv"].ToString(),
                        Convert.ToDecimal(item.Cells["Kol"].Value).ToString("#0.00").Trim(),//(polindex != -1 ? 0.5 : kolicina),
                        mpc.ToString("#0.00"), //mpc.ToString("#0.00"),
                        item.Cells["sifra"].Value.ToString().Trim(), //DT.Rows[0]["sifra"].ToString(),
                        Class.Postavke.id_default_skladiste, //DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString(),
                        porez.ToString("#0.00"), //DT.Rows[0]["porez"].ToString(),
                        "0",
                        mpc - (pdv_stavka + Porez_potrosnja_stavka),
                        pnp.ToString("#0.00"), //DT.Rows[0]["porez_potrosnja"].ToString(),
                        "",
                        "",
                        item.Cells["nbc"].Value.ToString().Trim(), //nbc.ToString("#0.00000"),
                        item.Cells["dod"].Value.ToString().Trim(), //(btnDodatak.BackgroundImage == Properties.Resources.dff ? DT.Rows[0]["dod"].ToString() : "0"),
                        "" //(polindex != -1 ? polpol.ToString() : "")
                    );
                    MainForm.dgw.Rows[MainForm.dgw.Rows.Count - 1].Tag = item.Cells["id_djelatnik"].Value;
                    MainForm.IzracunUkupno();
                }
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnNaplataOdustani_Click(object sender, EventArgs e)
        {
            btnOdustani_Click(new object(), new EventArgs());
        }

        private void dgvDugovanje_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDugovanje.Rows.Count == 0 || dgvDugovanje.CurrentCell.RowIndex < 0)
                {
                    return;
                }

                if (dgvDugovanje.CurrentCell.ColumnIndex == 0)
                {
                    dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgvDugovanje.Rows[dgvDugovanje.CurrentCell.RowIndex].Cells[0].Value);

                    var checkedRows = from DataGridViewRow r in dgvDugovanje.Rows
                                      where Convert.ToBoolean(r.Cells[0].Value) == true
                                      select r;
                    decimal iznos = 0;
                    foreach (var item in checkedRows)
                    {
                        iznos += (Convert.ToDecimal(item.Cells["kol"].Value) * Convert.ToDecimal(item.Cells["Cijena"].Value));
                    }

                    lblIznosOdabranogDuga.Text = string.Format("Iznos: {0} kn", Math.Round(iznos, 2).ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnSvi.PerformClick();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvOsobe_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvOsobe.Rows.Count == 0 || openInMenu)
                {
                    return;
                }
                int selectedRowIndex = dgvOsobe.CurrentCell.RowIndex;
                if (selectedRowIndex < 0)
                {
                    return;
                }

                _dodajOsobu = false;

                if (MessageBox.Show("Želite dodati osobu na račun?", "Osoba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _dodajOsobu = true;
                    Close();
                }
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

    public class OsobeDugNaplata
    {
        public int godina { get; set; }
        public int id { get; set; }
    }
}