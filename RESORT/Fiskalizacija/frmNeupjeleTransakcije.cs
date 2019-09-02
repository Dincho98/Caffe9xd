using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace RESORT.Fiskalizacija
{
    public partial class frmNeupjeleTransakcije : Form
    {
        public frmNeupjeleTransakcije()
        {
            InitializeComponent();
        }

        private static DataTable DTfis = classDBlite.LiteSelect("SELECT * FROM podaci_fiskalizacija", "podaci_fiskalizacija").Tables[0];

        private void frmNeupjeleTransakcije_Load(object sender, EventArgs e)
        {
            Set();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
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

        private void Set()
        {
            DataTable DT = RemoteDB.select("SELECT rfakture.broj AS [BROJ]," +
                "'RAČUN' AS [Dokumenat]," +
                " ukupno AS [Ukupno] " +
                " FROM rfakture WHERE (jir='' OR jir is null) AND to_char(datum, 'YYYY')='" + DateTime.Now.Year.ToString() + "' AND nacin_placanja IN ('1','2') ORDER BY broj ASC;", "rfakture").Tables[0];
            dgw.DataSource = DT;

            lblU.Text = "Ukupno ne fiskaliziranih računa " + DT.Rows.Count.ToString();

            PaintRows(dgw);
        }

        private void btnNarudzbe_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM rfakture WHERE (jir='' OR jir is null) AND to_char(datum, 'YYYY')='" + DateTime.Now.Year.ToString() + "' AND nacin_placanja IN ('1','2');";
            DataTable DT = RemoteDB.select(sql, "rfakture").Tables[0];

            if (DT.Rows.Count == 0)
            {
                MessageBox.Show("U ovoj godini sve je fiskalizirano.");
            }

            DataTable DTpodaciT = classDBlite.LiteSelect("SELECT oib FROM podaci_tvrtke", "podaci_tvrtke").Tables[0];
            INIFile ini = new INIFile();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string nacin_placanja = DT.Rows[i]["nacin_placanja"].ToString();
                if (nacin_placanja == "1")
                {
                    nacin_placanja = "G";
                }
                else if (nacin_placanja == "2")
                {
                    nacin_placanja = "K";
                }
                else if (nacin_placanja == "3")
                {
                    nacin_placanja = "T";
                }
                else
                {
                    nacin_placanja = "O";
                }

                bool sustav_pdva = false;
                if (ini.Read("Postavke", "u_sustavu_pdva").ToString() == "1") { sustav_pdva = true; }

                string[] porez_na_potrosnju = new string[3];
                porez_na_potrosnju[0] = "0";
                porez_na_potrosnju[1] = "0";
                porez_na_potrosnju[2] = "0";

                DataTable DTOstaliPor = new DataTable();
                DTOstaliPor.Columns.Add("naziv");
                DTOstaliPor.Columns.Add("stopa");
                DTOstaliPor.Columns.Add("osnovica");
                DTOstaliPor.Columns.Add("iznos");
                DataRow RowOstaliPor;

                DataTable DTnaknade = new DataTable();
                DTnaknade.Columns.Add("naziv");
                DTnaknade.Columns.Add("iznos");
                DataRow ROWnaknade;

                DateTime DatumRac;
                DateTime.TryParse(DT.Rows[i]["datum"].ToString(), out DatumRac);

                int brojF;
                int.TryParse(DT.Rows[i]["broj"].ToString(), out brojF);

                decimal ukupnoF;
                decimal.TryParse(DT.Rows[i]["ukupno"].ToString(), out ukupnoF);

                string queryPorez = @"SELECT
	                            ROUND(rfaktura_stavke.porez,2) AS stopa,

	                            ROUND(SUM(
		                            ((((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))-
		                            ((((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))*rabat)/100))
		                            /
		                            (1 zbroj (rfaktura_stavke.porez/100)))
	                            ),2) AS osnovica,

	                            ROUND(SUM(
		                            (((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))-
		                            ((((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))*rabat)/100))
		                            -
		                            ((((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))-
		                            ((((COALESCE(cijena_sobe,0) zbroj COALESCE(iznos_usluge,0))*COALESCE(dana,0))*rabat)/100))
		                            /
		                            (1 zbroj (rfaktura_stavke.porez/100)))
	                            ),2) AS iznos

                            FROM rfaktura_stavke WHERE rfaktura_stavke.broj=@broj GROUP BY rfaktura_stavke.broj, rfaktura_stavke.porez";

                queryPorez = queryPorez.Replace("@broj", brojF.ToString());

                DataTable DTpdv = RemoteDB.select(queryPorez, "porezi").Tables[0];

                DataTable DTzaposlenik = RemoteDB.select("SELECT oib FROM zaposlenici WHERE id_zaposlenik='" + DT.Rows[i]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0];

                string[] fiskalizacija = new string[3];
                fiskalizacija = classFiskalizacija.Fiskalizacija(DTpodaciT.Rows[0]["oib"].ToString(),
                    DTzaposlenik.Rows[0]["oib"].ToString(),
                    DatumRac,
                    brojF,
                    DTpdv,
                    porez_na_potrosnju,
                    DTOstaliPor,
                    "",
                    "",
                    DTnaknade,
                    Math.Round(ukupnoF, 2),
                    nacin_placanja,
                    true,
                    "F"
                    );

                if (fiskalizacija != null && fiskalizacija[1] != "")
                {
                    DataTable DTra = RemoteDB.select("SELECT * FROM rfakture WHERE broj='" + brojF.ToString() + "' AND (zik<>'' OR zik is not null)", "r").Tables[0];
                    if (DTra.Rows.Count == 0)
                    {
                        RemoteDB.update("UPDATE rfakture SET jir='" + fiskalizacija[0] + "', zik='" + fiskalizacija[1] + "' WHERE broj='" + brojF.ToString() + "'");
                    }
                    else
                    {
                        RemoteDB.update("UPDATE rfakture SET jir='" + fiskalizacija[0] + "' WHERE broj='" + brojF.ToString() + "'");
                    }
                }
            }

            MessageBox.Show("Završeno");
            Set();
        }

        private static void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnIzvozDisc_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                int i = dgw.CurrentRow.Index;
                string xml = dgw.CurrentRow.Cells["XML"].FormattedValue.ToString();

                XmlDocument XML = new XmlDocument();

                XML.LoadXml(xml);

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.FileName = "dd.xml";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XML.Save(saveFileDialog1.OpenFile());
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                provjera_sql(RemoteDB.delete("DELETE FROM neuspjela_fiskalizacija WHERE broj_racuna='" + dgw.CurrentRow.Cells[0].FormattedValue.ToString() + "'"));
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPregledNefiskaliziranog pn = new frmPregledNefiskaliziranog();
            pn.brojRacuna = dgw.CurrentRow.Cells[0].FormattedValue.ToString();
            pn.poslano = dgw.CurrentRow.Cells[1].FormattedValue.ToString();
            pn.greska = dgw.CurrentRow.Cells[2].FormattedValue.ToString();
            pn.ducan = dgw.CurrentRow.Cells[3].FormattedValue.ToString();
            pn.blagajna = dgw.CurrentRow.Cells[4].FormattedValue.ToString();
            pn.datum = dgw.CurrentRow.Cells[5].FormattedValue.ToString();
            pn.ShowDialog();
        }
    }
}