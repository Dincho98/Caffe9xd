using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmUskladenjePrometa : Form
    {
        public frmUskladenjePrometa()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        //Globalne varijable
        private DataTable DTunos = new DataTable();

        private DataRow rows;

        private bool edit = false;

        private void frmUskladenjePrometa_Load(object sender, EventArgs e)
        {
            txtObracun.Text = DateTime.Now.ToString("dd.MM.yyyy");
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            Pokretanje();
            popuni();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void popuni()
        {
            DataTable DTp = classSQL.select("SELECT * FROM uskladenje_prometa_trenutno ORDER BY naziv", "uskladenje_prometa_trenutno").Tables[0];
            if (DTp.Rows.Count > 0)
            {
                for (int i = 0; i < DTp.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ZaposleniciSelect(DTp.Rows[i]["zaposlenici"].ToString());
                    }

                    dgv.Rows.Add(
                        //DONOS
                        DTp.Rows[i]["sifra"].ToString(),
                        DTp.Rows[i]["naziv"].ToString(),
                        DTp.Rows[i]["donos_kolicina"].ToString(),
                        DTp.Rows[i]["donos_brojcanik"].ToString(),
                        DTp.Rows[i]["donos_unos_robe"].ToString(),
                        //PRODANO
                        DTp.Rows[i]["prodaja_kucano"].ToString(),
                        DTp.Rows[i]["prodaja_prodano"].ToString(),
                        DTp.Rows[i]["prodaja_stanje"].ToString(),
                        DTp.Rows[i]["prodaja_razlika"].ToString(),
                        DTp.Rows[i]["prodaja_iznos_kn"].ToString(),
                        //PRIJENOS
                        DTp.Rows[i]["prijenos_stanje"].ToString(),
                        DTp.Rows[i]["prijenos_brojcanik"].ToString(),
                        //UKUPNO i CIJENA
                        DTp.Rows[i]["cijena"].ToString(),
                        //BROJČANICI
                        DTp.Rows[i]["brojcanik_za_donos1"].ToString(),
                        DTp.Rows[i]["brojcanik_za_donos2"].ToString(),
                        DTp.Rows[i]["brojcanik_kraj_dana1"].ToString(),
                        DTp.Rows[i]["brojcanik_kraj_dana2"].ToString()
                        );
                }

                txtObracun.Text = DTp.Rows[0]["obracun_dat"].ToString();
                dtpDonosOD.Text = DTp.Rows[0]["donos_datum"].ToString();
                txtPrometBrojano.Text = DTp.Rows[0]["promet_brojano"].ToString();
                txtPrometKase.Text = DTp.Rows[0]["promet_kase"].ToString();
            }
        }

        private void Ukupno()
        {
            decimal ukupnob = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                try
                {
                    ukupnob += Convert.ToDecimal(dgv.Rows[i].Cells["iznoskuna"].FormattedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            txtPrometBrojano.Text = ukupnob.ToString("#0.00");
        }

        private void UkupnoProdanoPremaKucanome()
        {
            decimal ukupnob = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                try
                {
                    ukupnob += Convert.ToDecimal(dgv.Rows[i].Cells["prodaja_kucano"].FormattedValue.ToString()) * Convert.ToDecimal(dgv.Rows[i].Cells["cijena"].FormattedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            txtPrometKase.Text = ukupnob.ToString("#0.00");
        }

        private void Pokretanje()
        {
            DataTable DTz = classSQL.select("SELECT id_zaposlenik,ime + ' ' + prezime AS ime,id_zaposlenik FROM zaposlenici WHERE aktivan='DA'", "zaposlenici").Tables[0];

            for (int i = 0; i < DTz.Rows.Count; i++)
            {
                dgvD.Rows.Add(DTz.Rows[i]["ime"].ToString(), false, DTz.Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void Set()
        {
            DataTable DT = classSQL.select("SELECT * FROM roba_prodaja ORDER BY naziv ASC", "roba").Tables[0];
            dgv.Rows.Clear();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                //dobiva sve prodajne artikle koji se vežu na šifru koja se šalje
                List<string[]> L_prodajno = ProdajniArtikliIzNormativa(DT.Rows[i]["sifra"].ToString());

                decimal predracuni = ProvjeraPredracun(DT.Rows[i]["sifra"].ToString());
                string kkkol = DT.Rows[i]["kolicina"].ToString() == "" ? "0" : DT.Rows[i]["kolicina"].ToString();
                decimal skladiste = decimal.Parse(kkkol);
                string brojcanik = DT.Rows[i]["brojcanik"].ToString() == "" ? "0" : DT.Rows[i]["brojcanik"].ToString();

                List<decimal> l_donos = Donos(DT.Rows[i]["sifra"].ToString());

                decimal donos_stanje = l_donos[0];
                decimal donos_brojcanik = l_donos[1];
                decimal donos_unos_robe = 0;

                decimal prodaja_kucano = predracuni;
                decimal prodaja_fiskalizirano = ProvjeraProdanog(L_prodajno, DT.Rows[i]["sifra"].ToString());

                decimal prodaja_stanje = 0;
                decimal prodaja_razlika = prodaja_kucano - 0;
                decimal prodaja_iznos_kn = 0;

                decimal prijenos_stanje = 0;
                decimal prijenos_brojcanik = 0;

                decimal sq;
                decimal cijena = 0;
                if (!decimal.TryParse(DT.Rows[i]["cijena2"].ToString(), out sq))
                {
                    classSQL.update("UPDATE roba_prodaja SET cijena2='0' WHERE sifra='" + DT.Rows[i]["sifra"].ToString() + "'");
                }
                else
                {
                    cijena = Convert.ToDecimal(DT.Rows[i]["cijena2"].ToString());
                }

                decimal ukupno = cijena * prodaja_kucano;

                dgv.Rows.Add(
                    //DONOS
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    Math.Round(donos_stanje, 3).ToString("#0.00"),
                    Math.Round(donos_brojcanik, 3).ToString("#0.00"),
                    Math.Round(donos_unos_robe, 3).ToString("#0.00"),
                    //PRODANO
                    Math.Round(prodaja_kucano + prodaja_fiskalizirano, 3).ToString("#0.00"),
                    0,
                    Math.Round(prodaja_stanje, 3).ToString("#0.00"),
                    Math.Round(prodaja_razlika, 3).ToString("#0.00"),
                    Math.Round(prodaja_iznos_kn, 3).ToString("#0.00"),
                    //PRIJENOS
                    Math.Round(prijenos_stanje, 3).ToString("#0.00"),
                    Math.Round(prijenos_brojcanik, 3).ToString("#0.00"),
                    //UKUPNO i CIJENA
                    //Math.Round(ukupno, 3).ToString("#0.00"),
                    Math.Round(cijena, 3).ToString("#0.00"),
                    l_donos[2],
                    l_donos[3],
                    0,
                    0
                    );
            }
        }

        private List<decimal> Donos(string sifra_normativ)
        {
            List<decimal> l_donos = new List<decimal>();

            DataTable DT = classSQL.select("SELECT prijenos_stanje,prijenos_brojcanik,brojcanik_kraj_dana1,brojcanik_kraj_dana2 FROM uskladenje_prometa_new " +
                " WHERE sifra='" + sifra_normativ + "' " +
                " AND obracun>='" + dtpDonosOD.Value.ToString("yyyy-MM-dd 00:00:00") + "'" +
                " AND obracun<='" + dtpDonosOD.Value.ToString("yyyy-MM-dd 23:59:59") + "'" +
                "", "uskladenje_prometa_new").Tables[0];

            if (DT.Rows.Count > 0)
            {
                l_donos.Add(Convert.ToDecimal(DT.Rows[0]["prijenos_stanje"].ToString()));
                l_donos.Add(Convert.ToDecimal(DT.Rows[0]["prijenos_brojcanik"].ToString()));
                l_donos.Add(Convert.ToDecimal(DT.Rows[0]["brojcanik_kraj_dana1"].ToString()));
                l_donos.Add(Convert.ToDecimal(DT.Rows[0]["brojcanik_kraj_dana2"].ToString()));
            }
            else
            {
                l_donos.Add(0);
                l_donos.Add(0);
                l_donos.Add(0);
                l_donos.Add(0);
            }
            return l_donos;
        }

        private List<string[]> ProdajniArtikliIzNormativa(string sifra_normativ)
        {
            List<string[]> l = new List<string[]>();
            string sql = "SELECT sifra,kolicina FROM caffe_normativ " +
                " WHERE sifra_normativ='" + sifra_normativ + "'";
            DataTable DT = classSQL.select(sql, "caffe_normativ").Tables[0];

            foreach (DataRow r in DT.Rows)
            {
                string[] arr = new string[2] { r["sifra"].ToString(), r["kolicina"].ToString() };
                l.Add(arr);
            }
            return l;
        }

        public string GetZaposlenici()
        {
            string zaposlenik = "";
            string z = "";
            for (int i = 0; i < dgvD.Rows.Count; i++)
            {
                if (dgvD.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
                {
                    z += "'" + dgvD.Rows[i].Cells["id"].FormattedValue.ToString() + "',";
                }

                if (i == dgvD.Rows.Count - 1)
                {
                    if (z.Length > 1)
                    {
                        z = z.Remove(z.Length - 1);
                        zaposlenik = "(" + z + ")";
                    }
                }
            }
            return zaposlenik;
        }

        private decimal ProvjeraProdanogNeFiskaliziranog(List<string[]> l)
        {
            string kol = "0";
            decimal d_kol = 0;

            string zaposlenik = GetZaposlenici();
            if (zaposlenik.Length > 0)
            {
                zaposlenik = " AND svi_predracuni.id_zaposlenik IN " + zaposlenik;
            }

            DateTime datumDO = Convert.ToDateTime(txtObracun.Text);

            if (zaposlenik.Length > 0)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    string sql = "SELECT kolicina FROM svi_predracuni " +
                        " WHERE sifra='" + l[i][0] + "'" +
                        " AND datum_ispisa>'" + dtpDonosOD.Value.ToString("yyyy-MM-dd 0:00:01") + "'" +
                        " AND datum_ispisa<'" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "'" +
                        " " + zaposlenik + "";
                    DataTable DT = classSQL.select(sql, "roba").Tables[0];
                    kol = DT.Rows.Count == 0 ? "0" : DT.Rows[0][0].ToString();
                    kol = kol == "" ? "0" : kol;
                    d_kol += decimal.Parse(kol) * decimal.Parse(l[i][1]);
                }
                return d_kol;
            }
            return 0;
        }

        //OLD_________________________________NE BRISATI____________________________________________
        /*
        decimal ProvjeraPredracun(List<string[]> l)
        {
                string kol = "0";
                decimal d_kol = 0;
                string zaposlenik = "";

                if (cbZaposlenici.SelectedValue.ToString() != "0")
                {
                    zaposlenik = " AND svi_predracuni.id_zaposlenik='" + cbZaposlenici.SelectedValue.ToString() + "'";
                }

                for (int i = 0; i < l.Count; i++)
                {
                    string sql = "SELECT SUM(CAST(kolicina AS numeric)) FROM svi_predracuni " +
                        " WHERE svi_predracuni.sifra='" + l[i][0] + "' " +
                        "AND svi_predracuni.datum_ispisa>'" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' "+
                        "AND svi_predracuni.datum_ispisa<'" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' "+zaposlenik+"";
                    DataTable DT = classSQL.select(sql, "roba").Tables[0];
                    kol = DT.Rows[0][0].ToString();
                    kol = kol == "" ? "0" : kol;
                    d_kol += decimal.Parse(kol) * decimal.Parse(l[i][1]);
                }
                return d_kol;
        }
        */
        //OLD_________________________________NE BRISATI____________________________________________

        private decimal ProvjeraPredracun(string sifra)
        {
            string kol = "0";
            decimal d_kol = 0;

            string zaposlenik = GetZaposlenici();
            if (zaposlenik.Length > 0)
            {
                zaposlenik = " AND kucani_predracuni.id_zaposlenik IN " + zaposlenik;
            }

            DateTime datumDO = Convert.ToDateTime(txtObracun.Text);

            if (zaposlenik.Length > 0)
            {
                string sql = "SELECT SUM(kolicina) as kolicina FROM kucani_predracuni " +
                    " WHERE kucani_predracuni.sifra='" + sifra + "' " +
                    " AND datum>'" + dtpDonosOD.Value.ToString("yyyy-MM-dd 0:00:01") + "'" +
                    " AND datum<'" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "'" + zaposlenik;

                DataTable DT = classSQL.select(sql, "roba").Tables[0];
                kol = DT.Rows[0][0].ToString();
                kol = kol == "" ? "0" : kol;
                d_kol += decimal.Parse(kol);
                if (sifra == "29")
                {
                    d_kol = d_kol * 142.8571428571429m;
                }
                return d_kol;
            }
            return 0;
        }

        private decimal ProvjeraProdanog(List<string[]> l, string normativ_sifra)
        {
            string kol = "0";
            decimal d_kol = 0;
            DateTime datumDO = Convert.ToDateTime(txtObracun.Text);
            string zaposlenik = GetZaposlenici();
            if (zaposlenik.Length > 0)
            {
                zaposlenik = " AND racuni.id_blagajnik IN " + zaposlenik;
            }

            if (zaposlenik.Length > 0)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    string sql = "SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) FROM racun_stavke " +
                        " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                        " WHERE racun_stavke.sifra_robe='" + l[i][0] + "'" +
                        " AND racuni.datum_racuna>'" + dtpDonosOD.Value.ToString("yyyy-MM-dd 0:00:01") + "'" +
                        " AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "'" +
                        " " + zaposlenik + "";
                    DataTable DT = classSQL.select(sql, "roba").Tables[0];
                    kol = DT.Rows[0][0].ToString();
                    kol = kol == "" ? "0" : kol;

                    if (normativ_sifra == "29")
                    {
                        d_kol += decimal.Parse(kol) * (decimal.Parse(l[i][1]) * 142.8571428571429m);
                    }
                    else
                    {
                        d_kol += decimal.Parse(kol) * decimal.Parse(l[i][1]);
                    }
                }
                return d_kol;
            }
            return 0;
        }

        private decimal KolicinaSkladiste(string sifra)
        {
            DataTable DT = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "'", "roba_prodaja").Tables[0];
            if (DT.Rows.Count > 0)
                return decimal.Parse(DT.Rows[0][0].ToString());
            else
                return 0;
        }

        /// <summary>
        /// Funkcija traži koja sifra ima brojilo
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        private decimal Brojcanik(string sifra)
        {
            DataTable DT = classSQL.select("SELECT brojcanik FROM roba WHERE sifra='" + sifra + "'", "").Tables[0];
            try
            {
                if (DT.Rows.Count > 0)
                    return decimal.Parse(DT.Rows[0][0].ToString());
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Popunjava DataTable
        /// </summary>
        /// <param name="sifra"></param>
        /// <param name="naziv"></param>
        /// <param name="Dkolicina"></param>
        /// <param name="Dbrojcanik"></param>
        /// <param name="Dunos_robe"></param>
        /// <param name="Pkucano"></param>
        /// <param name="Pprodano"></param>
        /// <param name="Pstanje"></param>
        /// <param name="Prazlika"></param>
        /// <param name="Piznos"></param>
        private void FillDatabase(string sifra, string naziv, string kolicina_skladiste, string fisklalizirano, string predracun, string razlika, string ukupno, string unos_robe, string brojcanik, string cijena)
        {
            if (DTunos.Columns["sifra"] == null)
            {
                DTunos.Columns.Add("sifra");
                DTunos.Columns.Add("naziv");
                DTunos.Columns.Add("kolicina_skladiste");
                DTunos.Columns.Add("fisklalizirano");
                DTunos.Columns.Add("predracun");
                DTunos.Columns.Add("razlika");
                DTunos.Columns.Add("ukupno");
                DTunos.Columns.Add("unos_robe");
                DTunos.Columns.Add("brojcanik");
                DTunos.Columns.Add("cijena");
            }
            rows = DTunos.NewRow();
            rows["sifra"] = sifra;
            rows["naziv"] = naziv;
            rows["kolicina_skladiste"] = kolicina_skladiste;
            rows["fisklalizirano"] = fisklalizirano;
            rows["predracun"] = predracun;
            rows["razlika"] = razlika;
            rows["ukupno"] = ukupno;
            rows["unos_robe"] = unos_robe;
            rows["brojcanik"] = brojcanik;
            rows["cijena"] = cijena;
            DTunos.Rows.Add(rows);
        }

        private bool ocitaj = false;

        private void btnUcitajPodatke_Click(object sender, EventArgs e)
        {
            txtPrometBrojano.Text = "0";
            txtPrometKase.Text = "0";
            Set();
            UkupnoProdanoPremaKucanome();
            LoadUkupno();
            ocitaj = true;
        }

        private void LoadUkupno()
        {
            DateTime datumDO = Convert.ToDateTime(txtObracun.Text);
            string zaposlenik = GetZaposlenici();
            if (zaposlenik.Length > 0)
            {
                zaposlenik = " AND svi_predracuni.id_zaposlenik IN " + zaposlenik;
            }

            string sql_liste = "SELECT " +
                " svi_predracuni.kolicina*svi_predracuni.mpc AS cijena6" +
                " FROM svi_predracuni " +
                " WHERE svi_predracuni.datum_ispisa>'" + dtpDonosOD.Value.ToString("yyyy-MM-dd 0:00:01") + "'" +
                " AND svi_predracuni.datum_ispisa<'" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "' " + zaposlenik + "";

            DataTable DT = classSQL.select(sql_liste, "sql").Tables[0];

            decimal dec = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dec += Convert.ToDecimal(DT.Rows[i][0].ToString());
            }

            zaposlenik = GetZaposlenici();
            if (zaposlenik.Length > 0)
            {
                zaposlenik = " AND racuni.id_blagajnik IN " + zaposlenik;
            }

            string sql = "SELECT SUM(mpc) FROM racun_stavke " +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna" +
                " WHERE " +
                " racuni.datum_racuna>'" + dtpDonosOD.Value.ToString("yyyy-MM-dd 0:00:01") + "'" +
                " AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "'" +
                " " + zaposlenik + "";
            DataTable DTfis = classSQL.select(sql, "roba").Tables[0];

            decimal fisk = 0;
            if (DTfis.Rows.Count > 0)
            {
                fisk = Convert.ToDecimal(DTfis.Rows[0][0].ToString());
            }

            txtPrometKase.Text = (dec + fisk).ToString("#0.00");
        }

        private string GetBroj()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM uskladenje_prometa_new", "uskladenje_prometa_new").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnObradiPodatke_Click(object sender, EventArgs e)
        {
            if (!edit)
            {
                string zap = "";
                for (int i = 0; i < dgvD.Rows.Count; i++)
                {
                    if (dgvD.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
                    {
                        zap += dgvD.Rows[i].Cells["id"].FormattedValue.ToString() + "#";
                    }
                }
                if (zap.Length > 1) { zap = zap.Remove(zap.Length - 1); }

                string bbb = GetBroj();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    decimal donos_kol = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina_skladiste"].FormattedValue.ToString());
                    decimal donos_unos = Convert.ToDecimal(dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString());
                    decimal prodano = Convert.ToDecimal(dgv.Rows[i].Cells["prodaja_prodano"].FormattedValue.ToString());

                    dgv.Rows[i].Cells["stanje_prijenos"].Value = (donos_kol + donos_unos) - prodano;

                    string sql = "INSERT INTO uskladenje_prometa_new (sifra,naziv,obracun,donos_od,donos_kolicina,donos_brojcanik,donos_unos_robe," +
                        "prodaja_kucano,prodaja_prodano,prodaja_stanje,prodaja_razlika," +
                        "prodaja_iznos_kn,prijenos_stanje,prijenos_brojcanik,broj,zaposlenici,cijena,brojcanik_za_donos1,brojcanik_za_donos2,brojcanik_kraj_dana1,brojcanik_kraj_dana2) VALUES (" +
                        "'" + dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgv.Rows[i].Cells["naziv"].FormattedValue.ToString() + "'," +
                        "'" + Convert.ToDateTime(txtObracun.Text).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + dtpDonosOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + dgv.Rows[i].Cells["kolicina_skladiste"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["_brojcanik"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["prodaja_kucano"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["prodaja_prodano"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["stanje"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["razlika"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["iznoskuna"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["stanje_prijenos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_prijenos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + bbb + "'," +
                        //"'" + dgv.Rows[i].Cells["ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + zap + "'," +
                        "'" + dgv.Rows[i].Cells["cijena"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["donos_brojcanik1"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["donos_brojcanik2"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_kraj_dana1"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_kraj_dana2"].FormattedValue.ToString().Replace(",", ".") + "'" +
                        ")";

                    classSQL.insert(sql);
                }
            }

            //postavi novo stanje
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                try
                {
                    decimal d = 0;
                    if (!decimal.TryParse(dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString(), out d))
                    {
                        dgv.Rows[i].Cells["unos_robe"].Value = 0;
                    }

                    if (Convert.ToDecimal(dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString()) != 0)
                    {
                        decimal stanje_na_skladistu = 0;
                        decimal unosRobe = Convert.ToDecimal(dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString());
                        string sifra = dgv.Rows[i].Cells["sifra"].FormattedValue.ToString();

                        DataTable DT = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "'", "roba_prodaja").Tables[0];

                        //ovo racuna roba_prodaja skladiste
                        stanje_na_skladistu = DT.Rows.Count > 0 ? decimal.Parse(DT.Rows[0]["kolicina"].ToString()) : 0;
                        stanje_na_skladistu += unosRobe;

                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + stanje_na_skladistu + "' WHERE sifra='" + sifra + "'");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show("Podaci su uspješno obrađeni.");
        }

        private void PoravnajZaposlenikeKucaniPredracuni(decimal kol, string sifra)
        {
            //List<int> id_zap= new List<int>();
            //for (int i = 0; i < dgvD.RowCount; i++)
            //{
            //    if (dgvD.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
            //        id_zap.Add(Convert.ToInt16(dgvD.Rows[i].Cells["id"].FormattedValue.ToString()));
            //}

            //for (int i = 0; i < id_zap.Count; i++)
            //{
            //    DataTable DT = classSQL.select("SELECT kolicina,datum FROM kucani_predracuni WHERE sifra='" + sifra + "' AND id_zaposlenik='" + id_zap[i] + "' AND "+
            //        " datum>'" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //        " AND datum <'" + dtpDonosOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //        "", "kucani_predracuni").Tables[0];

            //        for (int y = 0; y < DT.Rows.Count; y++)
            //        {
            //            decimal kolicina = Convert.ToDecimal(DT.Rows[0]["Kolicina"].ToString());
            //            if (kolicina < kol)
            //            {
            //                classSQL.update("UPDATE kucani_predracuni SET kolicina='0' WHERE sifra='" + sifra + "' AND id_zaposlenik='" + id_zap[i] + "' AND datum='"+DT.Rows[y]["datum"].ToString()+"'");
            //                kol = kol - kolicina;
            //            }
            //            else
            //            {
            //                kol = kolicina - kol;
            //                classSQL.update("UPDATE kucani_predracuni SET kolicina='" + kol.ToString().Replace(",", ".") + "' WHERE sifra='" + sifra + "' AND id_zaposlenik='" + id_zap[i] + "' AND datum='" + DT.Rows[y]["datum"].ToString() + "'");
            //            }
            //        }
            //}
        }

        private void UrediSume()
        {
            decimal promet_kase = 0;
            decimal promet_brojano = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                promet_kase += Convert.ToDecimal(dgv.Rows[i].Cells["prodaja_kucano"].FormattedValue.ToString());
                promet_brojano += Convert.ToDecimal(dgv.Rows[i].Cells["prodaja_prodano"].FormattedValue.ToString());
            }

            //string zaposlenik1 = GetZaposlenici();
            //if (zaposlenik1.Length > 0)
            //{
            //    zaposlenik1 = " AND svi_predracuni.id_zaposlenik IN " + zaposlenik1;
            //}
            //if (zaposlenik1.Length > 0)
            //{
            //    string sql = "SELECT SUM(mpc) FROM svi_predracuni " +
            //    " WHERE " +
            //    " datum_ispisa>'" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //    " AND datum_ispisa<'" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //    " " + zaposlenik1 + "";
            //    DataTable DTpr = classSQL.select(sql, "roba").Tables[0];
            //    if (DTpr.Rows.Count > 0 && DTpr.Rows[0][0].ToString() != "")
            //        txtNefiskaliziranio.Text = Math.Round(Convert.ToDecimal(DTpr.Rows[0][0].ToString()), 3).ToString("#0.00");
            //}
            //else
            //{
            //    txtNefiskaliziranio.Text = "0.00";
            //}
        }

        public string dg(int br, string name)
        {
            return dgv.Rows[br].Cells[name].FormattedValue.ToString();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }

                ocitaj = true;
                frmUskladenje u = new frmUskladenje();
                u.sifra = dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString();
                u.naziv = dgv.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString();

                u.MyForm = this;
                u.row = e.RowIndex;
                u.cell = e.ColumnIndex;
                u.ShowDialog();
                Ukupno();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    FillDatabase(
            //        dg(i,"sifra"),
            //        dg(i,"naziv"),
            //        dg(i,"kolicina_skladiste"),
            //        dg(i,"fiskalizirano"),
            //        dg(i,"predracun"),
            //        dg(i,"razlika"),
            //        dg(i,"ukupno"),
            //        dg(i,"unos_robe"),
            //        dg(i,"_brojcanik"),
            //        dg(i,"cijena")
            //      );
            //}

            //Report.Uskladenje.frmUskladenjeRep r = new Report.Uskladenje.frmUskladenjeRep();
            //r.DTuskladenje = DTunos;
            //r.ShowDialog();
        }

        //OZNAČI ZAPOSLENIKE
        private void dgvD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }

            if (dgvD.Rows[e.RowIndex].Cells["oznaci"].FormattedValue.ToString() == "True")
            {
                dgvD.Rows[e.RowIndex].Cells["oznaci"].Value = false;
            }
            else
            {
                dgvD.Rows[e.RowIndex].Cells["oznaci"].Value = true;
            }
        }

        private void btnBrojcanikKave_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() == "29")
                    {
                        Caffe.Dodaci.frmBrojcanikKave bk = new Dodaci.frmBrojcanikKave();
                        bk.row = i;
                        bk.MyForm = this;
                        bk.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nemate označenog artikla.", "Greška");
            }
        }

        private void txtObracun_Leave(object sender, EventArgs e)
        {
            DateTime d;
            if (!DateTime.TryParse(txtObracun.Text, out d))
            {
                MessageBox.Show("Greška:\r\nUnjeli ste neispravan datum.", "Krivi datum");
                txtObracun.Text = DateTime.Now.ToString("dd.MM.yyyy");
            }
        }

        private void btnMinius_Click(object sender, EventArgs e)
        {
            txtPrometBrojano.Text = "0";
            txtPrometKase.Text = "0";
            DateTime DaT;
            if (!DateTime.TryParse(txtObracun.Text, out DaT))
            {
                DaT = DateTime.Now;
            }
            txtObracun.Text = DaT.AddDays(-1).ToString("dd.MM.yyyy");
            list();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtPrometBrojano.Text = "0";
            txtPrometKase.Text = "0";
            DateTime DaT;
            if (!DateTime.TryParse(txtObracun.Text, out DaT))
            {
                DaT = DateTime.Now;
            }
            txtObracun.Text = DaT.AddDays(1).ToString("dd.MM.yyyy");
            list();
        }

        private void list()
        {
            DateTime datt = Convert.ToDateTime(txtObracun.Text);
            string sql = "SELECT * FROM uskladenje_prometa_new WHERE obracun='" + datt.ToString("yyyy-MM-dd 00:00:00") + "'";
            DataTable D = classSQL.select(sql, "uskladenje_prometa_new").Tables[0];
            dgv.Rows.Clear();

            if (dgv.Rows.Count == 0 && D.Rows.Count == 0)
            {
                return;
            }

            for (int g = 0; g < dgvD.RowCount; g++)
            {
                dgvD.Rows[g].Cells["oznaci"].Value = false;
            }

            for (int i = 0; i < D.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ZaposleniciSelect(D.Rows[i]["zaposlenici"].ToString());
                }

                dgv.Rows.Add(
                    //DONOS
                    D.Rows[i]["sifra"].ToString(),
                    D.Rows[i]["naziv"].ToString(),
                    D.Rows[i]["donos_kolicina"].ToString(),
                    D.Rows[i]["donos_brojcanik"].ToString(),
                    D.Rows[i]["donos_unos_robe"].ToString(),
                    //PRODANO
                    D.Rows[i]["prodaja_kucano"].ToString(),
                    D.Rows[i]["prodaja_prodano"].ToString(),
                    D.Rows[i]["prodaja_stanje"].ToString(),
                    D.Rows[i]["prodaja_razlika"].ToString(),
                    D.Rows[i]["prodaja_iznos_kn"].ToString(),
                    //PRIJENOS
                    D.Rows[i]["prijenos_stanje"].ToString(),
                    D.Rows[i]["prijenos_brojcanik"].ToString(),
                    //UKUPNO i CIJENA
                    //D.Rows[i]["ukupno"].ToString(),
                    D.Rows[i]["cijena"].ToString(),
                    //BROJČANICI
                    D.Rows[i]["brojcanik_za_donos1"].ToString(),
                    D.Rows[i]["brojcanik_za_donos2"].ToString(),
                    D.Rows[i]["brojcanik_kraj_dana1"].ToString(),
                    D.Rows[i]["brojcanik_kraj_dana2"].ToString()
                    );
            }

            Ukupno();
            LoadUkupno();
        }

        private void ZaposleniciSelect(string arr)
        {
            string zap = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '#')
                {
                    for (int g = 0; g < dgvD.RowCount; g++)
                    {
                        if (dgvD.Rows[g].Cells["id"].FormattedValue.ToString() == zap)
                        {
                            dgvD.Rows[g].Cells["oznaci"].Value = true;
                        }
                    }
                    zap = "";
                }
                else
                {
                    zap += arr[i];
                    if (i == arr.Length - 1)
                    {
                        for (int g = 0; g < dgvD.RowCount; g++)
                        {
                            if (dgvD.Rows[g].Cells["id"].FormattedValue.ToString() == zap)
                            {
                                dgvD.Rows[g].Cells["oznaci"].Value = true;
                            }
                        }
                    }
                }
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dgv.Rows.Count > 0)
                {
                    ocitaj = true;
                    int row = dgv.CurrentRow.Index;
                    frmUskladenje u = new frmUskladenje();
                    u.sifra = dgv.Rows[row].Cells["sifra"].FormattedValue.ToString();
                    u.naziv = dgv.Rows[row].Cells["naziv"].FormattedValue.ToString();

                    u.MyForm = this;
                    u.row = row;

                    u.ShowDialog();
                    Ukupno();
                }
            }
            else
            {
                char key = getChar(e);
                string slovo = Convert.ToString(key);

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    string naziv = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
                    if (naziv.Length > 1)
                    {
                        naziv = naziv.Remove(1);
                        if (naziv.ToUpper() == slovo.ToUpper())
                        {
                            dgv.CurrentCell = dgv.Rows[i].Cells[1];
                            break;
                        }
                    }
                }
            }
        }

        private char getChar(KeyEventArgs e)
        {
            int keyValue = e.KeyValue;
            if (!e.Shift && keyValue >= (int)Keys.A && keyValue <= (int)Keys.Z)
                return (char)(keyValue + 32);
            return (char)keyValue;
        }

        private void frmUskladenjePrometa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ocitaj)
            {
                string zap = "";
                for (int i = 0; i < dgvD.Rows.Count; i++)
                {
                    if (dgvD.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
                    {
                        zap += dgvD.Rows[i].Cells["id"].FormattedValue.ToString() + "#";
                    }
                }
                if (zap.Length > 1) { zap = zap.Remove(zap.Length - 1); }

                DataTable DTp = classSQL.select("SELECT * FROM uskladenje_prometa_trenutno", "uskladenje_prometa_trenutno").Tables[0];
                if (DTp.Rows.Count > 0)
                {
                    classSQL.delete("DELETE FROM uskladenje_prometa_trenutno");
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string sql = "INSERT INTO uskladenje_prometa_trenutno (sifra,naziv,obracun,donos_od,donos_kolicina,donos_brojcanik,donos_unos_robe," +
                        "prodaja_kucano,prodaja_prodano,prodaja_stanje,prodaja_razlika," +
                        "prodaja_iznos_kn,prijenos_stanje,prijenos_brojcanik,broj,zaposlenici,cijena,brojcanik_za_donos1," +
                        "brojcanik_za_donos2,brojcanik_kraj_dana1,brojcanik_kraj_dana2,donos_datum,obracun_dat,promet_kase,promet_brojano) VALUES (" +
                        "'" + dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgv.Rows[i].Cells["naziv"].FormattedValue.ToString() + "'," +
                        "'" + Convert.ToDateTime(txtObracun.Text).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + dtpDonosOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + dgv.Rows[i].Cells["kolicina_skladiste"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["_brojcanik"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["unos_robe"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["prodaja_kucano"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["prodaja_prodano"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["stanje"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["razlika"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["iznoskuna"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["stanje_prijenos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_prijenos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'1'," +
                        "'" + zap + "'," +
                        "'" + dgv.Rows[i].Cells["cijena"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["donos_brojcanik1"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["donos_brojcanik2"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_kraj_dana1"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgv.Rows[i].Cells["brojcanik_kraj_dana2"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dtpDonosOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + txtObracun.Text + "'," +
                        "'" + txtPrometKase.Text.Replace(",", ".") + "'," +
                        "'" + txtPrometBrojano.Text.Replace(",", ".") + "'" +
                        ")";

                    classSQL.insert(sql);
                }

                ocitaj = false;
            }
        }

        private void btnBrisanjePrometa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati obračun za " + txtObracun.Text + "?", "Brisanje prometa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    string sifra = dgv.Rows[i].Cells["sifra"].FormattedValue.ToString();
                    DataTable dd = classSQL.select("SELECT donos_unos_robe FROM uskladenje_prometa_new WHERE obracun='" + Convert.ToDateTime(txtObracun.Text).ToString("yyyy-MM-dd 00:00:00") + "' AND sifra='" + sifra + "'", "uskladenje_prometa_new").Tables[0];

                    if (dd.Rows.Count > 0)
                    {
                        decimal stanje_na_skladistu = 0;

                        decimal unosRobe = 0;
                        if (dd.Rows.Count > 0)
                        {
                            unosRobe = Convert.ToDecimal(dd.Rows[0][0].ToString());
                        }

                        DataTable DT = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "'", "roba_prodaja").Tables[0];

                        //ovo racuna roba_prodaja skladiste
                        stanje_na_skladistu = DT.Rows.Count > 0 ? decimal.Parse(DT.Rows[0]["kolicina"].ToString()) : 0;
                        stanje_na_skladistu -= unosRobe;

                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + stanje_na_skladistu + "' WHERE sifra='" + sifra + "'");
                    }
                }

                classSQL.delete("DELETE FROM uskladenje_prometa_new WHERE obracun='" + Convert.ToDateTime(txtObracun.Text).ToString("yyyy-MM-dd 00:00:00") + "'");
                dgv.Rows.Clear();
                MessageBox.Show("Uspješno ste obrisali sve podatke!");
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