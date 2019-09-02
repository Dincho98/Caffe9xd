using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmPostaviStanjePremaZadnjojInventuri : Form
    {
        public frmPostaviStanjePremaZadnjojInventuri()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DateTime datumInventure;
        private string skladiste;
        private string inventura;
        private bool inventuraIzProsleGodine = false;
        private PCPOS.Until.classFukcijeZaUpravljanjeBazom tmp = new PCPOS.Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");

        private void frmPostaviStanjePremaZadnjojInventuri_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            DataTable DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            nuGodina.Value = DateTime.Now.Year;
        }

        private string brojInventure()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST (broj_inventure AS INT)) FROM inventura", "inventura").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "0";
            }
        }

        private void fillInventura(string broj_inventure_edit, string skl)
        {
            dgw.Rows.Clear();

            string sql1 = "SELECT " +
                " inventura_stavke.sifra_robe," +
                " inventura.broj_inventure," +
                " inventura.datum," +
                " inventura_stavke.jmj," +
                " inventura_stavke.cijena," +
                " inventura_stavke.naziv," +
                " inventura_stavke.kolicina," +
                " inventura_stavke.id_stavke," +
                " roba_prodaja.kolicina AS [k] " +
                " FROM inventura_stavke " +
                " LEFT JOIN inventura ON inventura_stavke.broj_inventure=inventura.broj_inventure" +
                " LEFT JOIN roba_prodaja ON inventura_stavke.sifra_robe=roba_prodaja.sifra" +
                " WHERE inventura_stavke.broj_inventure='" + broj_inventure_edit + "' AND inventura.id_skladiste='" + skl + "'";
            DataTable DTinventura_stavke = classSQL.select(sql1, "inventura_stavke").Tables[0];

            if (DTinventura_stavke.Rows.Count == 0)
            {
                return;
            }

            lblDate.Text = "Datum učitane inventure je: " + DTinventura_stavke.Rows[0]["datum"].ToString();
            datumInventure = Convert.ToDateTime(DTinventura_stavke.Rows[0]["datum"].ToString());

            for (int br = 0; br < DTinventura_stavke.Rows.Count; br++)
            {
                dgw.Rows.Add();

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTinventura_stavke.Rows[br]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTinventura_stavke.Rows[br]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = DTinventura_stavke.Rows[br]["jmj"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DTinventura_stavke.Rows[br]["kolicina"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DTinventura_stavke.Rows[br]["id_stavke"].ToString();
                dgw.Rows[br].Cells["KolicinaNaSk"].Value = DTinventura_stavke.Rows[br]["k"].ToString();
                dgw.Rows[br].Cells["id_skladiste"].Value = cbSkladiste.SelectedValue.ToString();
                dgw.Rows[br].Cells["cijena"].Value = DTinventura_stavke.Rows[br]["cijena"].ToString();
            }
            PaintRows(dgw);
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void btnZadnjaInv_Click(object sender, EventArgs e)
        {
            inventura = brojInventure(cbSkladiste.SelectedValue.ToString(), (int)nuGodina.Value);
            skladiste = cbSkladiste.SelectedValue.ToString();
            fillInventura(inventura, skladiste);
            if (inventura == "0" && tmp.PostojiProslaGodina())
            {
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(tmp.UzmiGodinuKojaSeKoristi().ToString(), (tmp.UzmiGodinuKojaSeKoristi() - 1).ToString()));
                inventuraIzProsleGodine = true;
                inventura = brojInventure(cbSkladiste.SelectedValue.ToString(), (int)nuGodina.Value);
                skladiste = cbSkladiste.SelectedValue.ToString();
                fillInventura(inventura, skladiste);
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);
            }
        }

        private string brojInventure(string skl, int godina)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_inventure as numeric)) FROM " +
                "inventura WHERE id_skladiste='" + skl + "'" +
                " AND godina='" + godina.ToString() + "'", "inventura").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "0";
            }
        }

        private void btnPostavi_Click(object sender, EventArgs e)
        {
            PostaviInventuru(true);
            if (inventuraIzProsleGodine)
            {
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(tmp.UzmiGodinuKojaSeKoristi().ToString(), (tmp.UzmiGodinuKojaSeKoristi() - 1).ToString()));
                classSQL.update("UPDATE inventura SET is_pocetno_stanje='1' WHERE broj_inventure='" + inventura + "' AND id_skladiste='" + skladiste + "'");
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);
            }
            else
            {
                classSQL.update("UPDATE inventura SET is_pocetno_stanje='1' WHERE broj_inventure='" + inventura + "' AND id_skladiste='" + skladiste + "'");
            }

            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            robno.PostaviStanjeSkladista();
            try
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false);
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
        }

        private List<DataTable> UzmiKolicineIzOveIprosleGodine()
        {
            DataSet DS = new DataSet();

            string sql = @"SELECT sifra,naziv,
                    /*PRIMKA*/
                    ROUND((coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke
                    LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.is_kalkulacija='False'
                    WHERE roba_prodaja.sifra=primka_stavke.sifra AND primka.is_kalkulacija='False' AND datum<'@datum'),0)
                    /*KALKULACIJA*/
                    +coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke
                    LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.is_kalkulacija='True'
                    WHERE roba_prodaja.sifra=primka_stavke.sifra AND primka.is_kalkulacija='True' AND datum<'@datum'),0)
                    /*OTPIS*/
                    -coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke
                    LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj
                    WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra AND datum<'@datum'),0)
                    /*RAČUNI*/
                    -coalesce((
                    SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric))))
                    FROM racun_stavke
                    LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                    LEFT JOIN caffe_normativ ON caffe_normativ.sifra=racun_stavke.sifra_robe
                    WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra AND racuni.datum_racuna<'@datum'
                    ),0)
                    /*MS IZLAZ*/
                    -coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice
                    WHERE medu_poslovnice.iz_poslovnice='DU' AND medu_poslovnice.sifra=roba_prodaja.sifra AND datum<'@datum'),0)
                    /*MS ULAZ*/
                    +coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice
                    WHERE medu_poslovnice.u_poslovnicu='DU' AND medu_poslovnice.sifra=roba_prodaja.sifra AND datum<'@datum'),0)),4)  as kol
                    FROM roba_prodaja ORDER BY naziv;";

            sql = sql.Replace("@datum", datumInventure.ToString("yyyy-MM-dd H:mm:ss")).Replace("+", "zbroj");
            DataTable DTovaGodina = classSQL.select(sql, "Artikli").Tables[0];

            sql = @"SELECT sifra,naziv,
                    /*PRIMKA*/
                    ROUND((coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke
                    LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.is_kalkulacija='False'
                    WHERE roba_prodaja.sifra=primka_stavke.sifra AND primka.is_kalkulacija='False' AND datum>'@datum'),0)
                    /*KALKULACIJA*/
                    +coalesce((SELECT SUM(kolicina) AS kol FROM primka_stavke
                    LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke AND primka.is_kalkulacija='True'
                    WHERE roba_prodaja.sifra=primka_stavke.sifra AND primka.is_kalkulacija='True' AND datum>'@datum'),0)
                    /*OTPIS*/
                    -coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') AS numeric)) AS kol FROM povrat_robe_stavke
                    LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj
                    WHERE roba_prodaja.sifra=povrat_robe_stavke.sifra AND datum>'@datum'),0)
                    /*RAČUNI*/
                    -coalesce((
                    SELECT SUM(CAST(REPLACE(caffe_normativ.kolicina,',','.') AS numeric)*coalesce((CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric))))
                    FROM racun_stavke
                    LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                    LEFT JOIN caffe_normativ ON caffe_normativ.sifra=racun_stavke.sifra_robe
                    WHERE caffe_normativ.sifra_normativ=roba_prodaja.sifra AND racuni.datum_racuna>'@datum'
                    ),0)

                    /*MS IZLAZ*/
                    -coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice
                    WHERE medu_poslovnice.iz_poslovnice='DU' AND medu_poslovnice.sifra=roba_prodaja.sifra AND datum>'@datum'),0)

                    /*MS ULAZ*/
                    +coalesce((SELECT SUM(medu_poslovnice.kolicina) as kolicina FROM medu_poslovnice
                    WHERE medu_poslovnice.u_poslovnicu='DU' AND medu_poslovnice.sifra=roba_prodaja.sifra AND datum>'@datum'),0)),4)  as kol
                    FROM roba_prodaja ORDER BY naziv;";

            sql = sql.Replace("@datum", datumInventure.ToString("yyyy-MM-dd H:mm:ss")).Replace("+", "zbroj");

            #region OVAJ DIO RADI KONEKCIJU NA BAZU IZ PRETHODNE GODINE

            Until.classFukcijeZaUpravljanjeBazom B = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
            string _trenutna_baza = B.UzmiBazuKojaSeKoristi();
            string _prethodna_godina = "";

            if (_trenutna_baza.Length == 6)
            {
                int godina;
                if (int.TryParse(_trenutna_baza.Remove(0, 2), out godina))
                {
                    _prethodna_godina = _trenutna_baza.Remove(2) + (godina - 1).ToString();
                }
            }

            //U ovaj dio ulazi samo ako postoji baza koja je u varijabli "_prethodna_godina"
            DataTable DTprethodna_godina = new DataTable();
            DataSet DStemp = new DataSet();
            if (B.GodinaPostoji(_prethodna_godina))
            {
                if (classSQL.remoteConnection.State.ToString() != "Closed") { classSQL.remoteConnection.Close(); }
                NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(_trenutna_baza, _prethodna_godina));
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                DStemp.Reset();
                da.Fill(DStemp);
                remoteConnection.Close();
                DTprethodna_godina = DStemp.Tables[0];
            }

            #endregion OVAJ DIO RADI KONEKCIJU NA BAZU IZ PRETHODNE GODINE

            List<DataTable> Ldt = new List<DataTable>();
            Ldt.Add(DTovaGodina);
            Ldt.Add(DTprethodna_godina);
            return Ldt;
        }

        private struct DokumentiRobno
        {
            public decimal kalulacija;
            public decimal racun;
            public decimal izdatnice;
            public decimal primke;
            public decimal fakture;
            public decimal meduskladisnice_u_skl;
            public decimal meduskladisnice_iz_skl;
            public decimal otpremnice;
            public decimal otpis;
            public decimal povrat_robe;
            public decimal radni_nalog;
            public decimal radni_nalog2;
        }

        private void PostaviInventuru(bool pocetno)
        {
            ControlEnableDisable(false);

            if (dgw.Rows.Count > 0)
            {
                List<DataTable> Ldt = UzmiKolicineIzOveIprosleGodine();
                DataTable DTsadasnja_godina = Ldt[0];
                DataTable DTprosla_godina = Ldt[1];
                DateTime datumspremanja;

                int g = DateTime.Now.Year;
                if (Util.Korisno.GodinaKojaSeKoristiUbazi != 0)
                    g = Util.Korisno.GodinaKojaSeKoristiUbazi;

                DateTime.TryParse(g + "-01-01 00:00:59", out datumspremanja);

                if (pocetno)
                    classSQL.update("DELETE FROM pocetno WHERE id_skladiste='" + skladiste + "'");

                DataTable DTartikli = classSQL.select("SELECT * FROM roba", "roba").Tables[0];
                DataTable DTRepromaterijal = classSQL.select("SELECT * FROM roba_prodaja", "roba_prodaja").Tables[0];

                foreach (DataGridViewRow r in dgw.Rows)
                {
                    decimal _kolicinaSadasnjaGodina = 0, _kolicinaPrijasnjaGodina = 0, ukupna_kolicina_za_spremiti = 0, inventurna_kolicina = 0, kolicina_pocetnog = 0;

                    ///
                    ///OVAJ DIO UZIMA IZ ROW-a I STAVLJA U struct "SadasnjaGodina_struct"
                    ///
                    if (DTsadasnja_godina.Rows.Count > 0)
                    {
                        DataRow[] rowSG = DTsadasnja_godina.Select("sifra='" + r.Cells["Sifra"].FormattedValue.ToString() + "'");
                        if (rowSG.Length > 0)
                        {
                            decimal.TryParse(rowSG[0]["kol"].ToString(), out _kolicinaSadasnjaGodina);
                        }
                    }

                    /*string sifra = r.Cells["sifra"].FormattedValue.ToString();
                    if (sifra == "46")
                    {
                        sifra = "46";
                    }*/

                    ///
                    ///OVAJ DIO UZIMA IZ ROW-a I STAVLJA U struct "StaraGod_struct"
                    ///

                    if (DTprosla_godina.Rows.Count > 0)
                    {
                        DataRow[] rowPG = DTprosla_godina.Select("sifra='" + r.Cells["Sifra"].FormattedValue.ToString() + "'");
                        if (rowPG.Length > 0)
                        {
                            decimal.TryParse(rowPG[0]["kol"].ToString(), out _kolicinaPrijasnjaGodina);
                        }
                    }

                    ///
                    ///OVAJ DIO je da uzme kolicinu sa popisane inventure
                    ///
                    decimal.TryParse(r.Cells["kolicina"].FormattedValue.ToString(), out inventurna_kolicina);
                    kolicina_pocetnog = inventurna_kolicina + _kolicinaPrijasnjaGodina;
                    ukupna_kolicina_za_spremiti = inventurna_kolicina + _kolicinaPrijasnjaGodina - _kolicinaSadasnjaGodina;

                    string sql = "";

                    if (pocetno)
                    {
                        string sifra = r.Cells["sifra"].FormattedValue.ToString();

                        DataRow[] row = DTartikli.Select("sifra='" + sifra + "'");
                        DataRow[] rowReproMat = DTRepromaterijal.Select("sifra='" + sifra + "'");

                        decimal mpc_cijena = 0, povratna_naknada = 0, nbc = 0;

                        if (row.Length > 0)
                            decimal.TryParse(row[0]["mpc"].ToString(), out mpc_cijena);

                        if (rowReproMat.Length > 0)
                            decimal.TryParse(rowReproMat[0]["povratna_naknada"].ToString(), out povratna_naknada);

                        decimal.TryParse(r.Cells["cijena"].FormattedValue.ToString(), out nbc);

                        sql = "INSERT INTO pocetno (sifra,id_skladiste,kolicina,nc,datum,novo,povratna_naknada,prodajna_cijena) VALUES (" +
                            "'" + sifra + "'," +
                            "'" + skladiste + "'," +
                            "'" + ukupna_kolicina_za_spremiti.ToString().Replace(",", ".") + "'," +
                            "'" + nbc.ToString().Replace(",", ".") + "'," +
                            "'" + datumspremanja.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'1'," +
                            "'" + povratna_naknada.ToString().Replace(",", ".") + "'," +
                            "'" + mpc_cijena.ToString().Replace(",", ".") + "');";
                        classSQL.update(sql);
                    }
                }

                //Postavljam stanje skladišta prema dokumentima
                Until.FunkcijeRobno fr = new Until.FunkcijeRobno();
                fr.PostaviStanjeSkladista();

                MessageBox.Show("Stanje je uspješno postavljeno!", "Stanje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nije odabrana inventura.");
            }

            ControlEnableDisable(true);
        }

        private void ControlEnableDisable(bool t)
        {
            cbSkladiste.Enabled = t;
            btnZadnjaInv.Enabled = t;
            btnPostavi.Enabled = t;
        }

        /*
        decimal kol = Convert.ToDecimal(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());

        string sql = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() + "'" +
            " WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'" +
            " AND id_skladiste='" + dgw.Rows[i].Cells["id_skladiste"].FormattedValue.ToString() + "'" +
            "";
        classSQL.update(sql);
        KolicinaPotroseno(dgw.Rows[i].Cells["sifra"].FormattedValue.ToString());
        */

        /*
		decimal kol_normativ = 0;
		decimal kol_racun = 0;
		decimal kol_robe = 0;
		decimal kol_zaSpremiti = 0;
		DataTable DT_roba_prodaja;

		private void KolicinaPotroseno(string sifra_repromaterijala)
		{
			string sql_nor = "SELECT " +
				" sifra,sifra_normativ,kolicina" +
				" FROM caffe_normativ " +
				" WHERE sifra_normativ='" + sifra_repromaterijala + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'";

			DataTable DTnor = classSQL.select(sql_nor, "caffe_normativ").Tables[0];

			for (int nor = 0; nor < DTnor.Rows.Count; nor++)
			{
				string sqlr = "SELECT " +
					" SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
					" racun_stavke.sifra_robe" +
					" FROM racun_stavke " +
					" LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna " +
					" WHERE racuni.datum_racuna>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' AND racun_stavke.sifra_robe='" + DTnor.Rows[nor]["sifra"].ToString() + "'" +
					" GROUP BY racun_stavke.sifra_robe";

				DataTable DTr = classSQL.select(sqlr, "racuni").Tables[0];

				for (int i = 0; i < DTr.Rows.Count; i++)
				{
						string sql_repro = "SELECT kolicina FROM roba_prodaja WHERE sifra='" + DTnor.Rows[i]["sifra_normativ"].ToString() + "'";
						DT_roba_prodaja = classSQL.select(sql_repro, "roba_prodaja").Tables[0];

						kol_robe = Convert.ToDecimal(DT_roba_prodaja.Rows[0]["kolicina"].ToString());
						kol_normativ = Convert.ToDecimal(DTnor.Rows[nor]["kolicina"].ToString());
						kol_racun = Convert.ToDecimal(DTr.Rows[i]["kolicina"].ToString());

						kol_zaSpremiti = kol_robe - (kol_normativ * kol_racun);
						classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol_zaSpremiti.ToString() + "' WHERE sifra='" + DTnor.Rows[i]["sifra_normativ"].ToString() + "'");
				}
			}

			//////////////////////////////POVRAT ROBE////////////////////////////////////////////////////////////
			string sqlo = "SELECT " +
			" SUM(CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
			" povrat_robe_stavke.sifra" +
			" FROM povrat_robe_stavke " +
			" LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj " +
			" WHERE povrat_robe.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' AND povrat_robe_stavke.sifra='" + sifra_repromaterijala + "'" +
			" GROUP BY povrat_robe_stavke.sifra";

			DataTable DTo = classSQL.select(sqlo, "racuni").Tables[0];

			for (int i = 0; i < DTo.Rows.Count; i++)
			{
				string sql_repro = "SELECT kolicina FROM roba_prodaja WHERE sifra='" + DTo.Rows[i]["sifra"].ToString() + "'";
				DT_roba_prodaja = classSQL.select(sql_repro, "roba_prodaja").Tables[0];

				kol_robe = Convert.ToDecimal(DT_roba_prodaja.Rows[0]["kolicina"].ToString());
				kol_racun = Convert.ToDecimal(DTo.Rows[i]["kolicina"].ToString());

				kol_zaSpremiti = kol_robe - kol_racun;

				classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol_zaSpremiti.ToString() + "' WHERE sifra='" + DTo.Rows[i]["sifra"].ToString() + "'");
			}

			//////////////////////////////PRIMKE////////////////////////////////////////////////////////////
			string sqlp = "SELECT " +
				" SUM(primka_stavke.kolicina) AS kolicina," +
				" primka_stavke.sifra," +
				" primka_stavke.id_skladiste" +
				" FROM primka_stavke " +
				" LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke " +
				" WHERE primka.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' AND primka_stavke.sifra='" + sifra_repromaterijala + "'" +
				" GROUP BY primka_stavke.sifra,primka_stavke.id_skladiste";

			DataTable DTp = classSQL.select(sqlp, "primka").Tables[0];

			for (int i = 0; i < DTp.Rows.Count; i++)
			{
				string sql_repro = "SELECT kolicina FROM roba_prodaja WHERE sifra='" + DTp.Rows[i]["sifra"].ToString() + "'";
				DT_roba_prodaja = classSQL.select(sql_repro, "roba_prodaja").Tables[0];

				kol_robe = Convert.ToDecimal(DT_roba_prodaja.Rows[0]["kolicina"].ToString());
				kol_racun = Convert.ToDecimal(DTp.Rows[i]["kolicina"].ToString());

				kol_zaSpremiti = kol_robe + kol_racun;
				classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol_zaSpremiti.ToString() + "' WHERE sifra='" + DTp.Rows[i]["sifra"].ToString() + "'");
			}
		}

        */

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