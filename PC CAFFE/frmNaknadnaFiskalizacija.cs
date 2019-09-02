using PCPOS.Fiskalizacija;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNaknadnaFiskalizacija : Form
    {
        public frmNaknadnaFiskalizacija()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTrac = new DataTable();

        private void frmNaknadnaFiskalizacija_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            UzmiRacune(null);
        }

        private void UzmiRacune(string broj)
        {
            string broj_rac = "";
            if (broj != null)
                broj_rac = " AND racuni.broj_racuna='" + broj + "'";

            string query = @"SELECT
                            racuni.broj_racuna,
                            racuni.datum_racuna,
                            ducan.ime_ducana,
                            racuni.id_ducan,
                            blagajna.ime_blagajne,
                            racuni.nacin_placanja,
                            racuni.id_blagajnik,
                            racuni.id_kasa,
                            racuni.godina,
                            SUM(
                            CAST(mpc AS NUMERIC)*
                            CAST(REPLACE(kolicina,',','.') AS NUMERIC)
                            ) AS ukupno
                            FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                            LEFT JOIN ducan ON ducan.id_ducan=racuni.id_ducan
                            LEFT JOIN blagajna ON blagajna.id_blagajna=racuni.id_kasa
                            WHERE ((jir='' AND zik='') OR (jir IS NULL AND zik IS NULL)) " + broj_rac + @" AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + @"' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + @"'
                            GROUP BY racuni.broj_racuna,racuni.godina,racuni.datum_racuna,ducan.ime_ducana,blagajna.ime_blagajne,
                            racuni.id_blagajnik,racuni.nacin_placanja,racuni.id_ducan,racuni.id_kasa,racuni.godina
                            ORDER BY racuni.datum_racuna ASC;";

            DTrac = classSQL.select(query, "racuni").Tables[0];

            dgw.Rows.Clear();
            int i = 1;
            foreach (DataRow r in DTrac.Rows)
            {
                dgw.Rows.Add(i, r["broj_racuna"].ToString(),
                    r["datum_racuna"].ToString(),
                    r["ime_ducana"].ToString(),
                    r["ime_blagajne"].ToString(),
                    r["ukupno"].ToString());
            }
        }

        private void btnFiskaliziraj_Click(object sender, EventArgs e)
        {
            UzmiRacune(null);
        }

        private void btnFiskalizirajJedan_Click(object sender, EventArgs e)
        {
            UzmiRacune(dgw.Rows[dgw.CurrentCell.RowIndex].Cells["broj"].FormattedValue.ToString());
        }

        private void btnFiskalizirajOdabrano_Click(object sender, EventArgs e)
        {
            DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            DataTable DTzaposlenici = classSQL.select("SELECT * FROM zaposlenici", "zaposlenici").Tables[0];

            if (DTfis.Rows[0]["aktivna"].ToString() == "0")
                return;

            foreach (DataRow r in DTrac.Rows)
            {
                try
                {
                    string sql_pnp = @"SELECT
                                    CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC) as porez,

                                    ROUND(SUM(((CAST(mpc AS NUMERIC)*CAST(REPLACE(kolicina,',','.') AS NUMERIC))*
                                    ((100*CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/(100+(CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC)+CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)))))
                                    /100),2) AS pnp,

                                    ROUND(SUM((CAST(mpc AS NUMERIC)*CAST(REPLACE(kolicina,',','.') AS NUMERIC))/
                                    (1+((CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC)+CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC))/100))
                                    ),2) as osnovica,

                                    ROUND(SUM(
                                    CAST(mpc AS NUMERIC)*
                                    CAST(REPLACE(kolicina,',','.') AS NUMERIC)
                                    ),2) AS ukupno

                                    FROM racun_stavke
                                    LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna

                                    WHERE racuni.broj_racuna='@broj' AND CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC)>0
                                    GROUP BY CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC);";

                    sql_pnp = sql_pnp.Replace("+", "zbroj").Replace("@broj", r["broj_racuna"].ToString());
                    DataTable DTpnp = classSQL.select(sql_pnp, "tbl").Tables[0];

                    string sql_pdv = @"SELECT
                                    CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC) as stopa,

                                    ROUND(SUM(((CAST(mpc AS NUMERIC)*CAST(REPLACE(kolicina,',','.') AS NUMERIC))*
                                    ((100*CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC))/(100+(CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC)+CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)))))
                                    /100),2) AS iznos,

                                    ROUND(SUM((CAST(mpc AS NUMERIC)*CAST(REPLACE(kolicina,',','.') AS NUMERIC))/
                                    (1+((CAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC)+CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC))/100))
                                    ),2) as osnovica,

                                    ROUND(SUM(
                                    CAST(mpc AS NUMERIC)*
                                    CAST(REPLACE(kolicina,',','.') AS NUMERIC)
                                    ),2) AS ukupno

                                    FROM racun_stavke
                                    LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna

                                    WHERE racuni.broj_racuna='@broj'
                                    GROUP BY CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)";

                    sql_pdv = sql_pdv.Replace("+", "zbroj").Replace("@broj", r["broj_racuna"].ToString());
                    DataTable DTpdv = classSQL.select(sql_pdv, "tbl").Tables[0];

                    string[] arrPNP = new string[3];
                    if (DTpnp.Rows.Count > 0)
                    {
                        arrPNP[0] = DTpnp.Rows[0]["porez"].ToString();
                        arrPNP[1] = DTpnp.Rows[0]["osnovica"].ToString();
                        arrPNP[2] = DTpnp.Rows[0]["pnp"].ToString();
                    }
                    else
                    {
                        arrPNP[0] = "0";
                        arrPNP[1] = "0";
                        arrPNP[2] = "0";
                    }

                    bool sustav_pdv = DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0" ? false : true;
                    int broj;
                    int.TryParse(r["broj_racuna"].ToString(), out broj);
                    int broj_kase;
                    int.TryParse(r["ime_blagajne"].ToString(), out broj_kase);
                    decimal ukupno;
                    decimal.TryParse(r["ukupno"].ToString(), out ukupno);

                    string oib_zaposlenika = "";
                    DataRow[] rowOperater = DTzaposlenici.Select("id_zaposlenik='" + r["id_blagajnik"].ToString() + "'");
                    if (rowOperater.Length > 0) { oib_zaposlenika = rowOperater[0]["oib"].ToString(); }

                    DateTime datum_racuna;
                    DateTime.TryParse(r["datum_racuna"].ToString(), out datum_racuna);

                    string nacin_placanja = "G";
                    if (r["nacin_placanja"].ToString() != "")
                        nacin_placanja = r["nacin_placanja"].ToString();

                    string[] fiskalizacija = new string[3];
                    fiskalizacija = classFiskalizacija.Fiskalizacija(DTpodaci.Rows[0]["oib"].ToString(),
                        oib_zaposlenika,
                        new DataTable(),
                        datum_racuna,
                        sustav_pdv,
                        broj,
                        r["id_ducan"].ToString(),
                        broj_kase,
                        DTpdv,
                        arrPNP,
                        new DataTable(),
                        "0",
                        "0",
                        new DataTable(),
                        ukupno,
                        nacin_placanja,
                        true,
                        0);

                    classSQL.update("UPDATE racuni SET jir='" + fiskalizacija[0] + "',zik='" + fiskalizacija[1] + "' WHERE broj_racuna='" + broj.ToString() + "'" +
                        " AND godina='" + r["godina"].ToString() + "' AND id_kasa='" + r["id_kasa"].ToString() + "' AND id_ducan='" + r["id_ducan"].ToString() + "';");

                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    ",'Naknadno fiskalizirani račun bez jira i zkia. Broj: " + broj.ToString() + ".')");

                    frmScren.ProvjeriPostojiLiNeuspjelaFiskalizacija();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            MessageBox.Show("Riješeno");
            UzmiRacune(null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*if (textBox1.Text == "q1w2e3r4")
            {
                btnFiskaliziraj.Enabled = true;
                btnFiskalizirajJedan.Enabled = true;
                btnFiskalizirajOdabrano.Enabled = true;
            }*/
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