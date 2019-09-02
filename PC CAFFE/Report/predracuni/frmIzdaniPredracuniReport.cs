using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.predracuni
{
    public partial class frmIzdanePonude : Form
    {
        public frmIzdanePonude()
        {
            InitializeComponent();
        }

        public string blagajnik_naziv { get; set; }
        public string ducan_naziv { get; set; }
        public string blagajnik { get; set; }
        public string stol { get; set; }
        public string ducan { get; set; }
        public DataGridView dgv { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public bool zbirno { get; set; }

        private void frmIzdanePonude_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            if(zbirno)
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.predracuni.PredracuniZbirno.rdlc";
            else
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.predracuni.Predracuni.rdlc";

            Predracun();
            this.reportViewer1.RefreshReport();
        }

        private void Predracun()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string uvjet = "";
            string blag = GetZaposlenici();
            if (blag.Length > 0)
            {
                uvjet += "AND svi_predracuni.id_zaposlenik IN " + blag + "";
            }

            if (ducan != "" && ducan != null)
            {
                uvjet += "AND svi_predracuni.id_ducan='" + ducan + "'";
            }

            if (stol != "")
            {
                uvjet += "AND svi_predracuni.id_stol='" + stol + "'";
            }

            string sql_liste = "";
            if(zbirno)
            {
                sql_liste = $@"SELECT svi_predracuni.sifra, 
	                            svi_predracuni.naziv,
	                            SUM(svi_predracuni.mpc) AS cijena3, 
	                            SUM(svi_predracuni.kolicina*svi_predracuni.mpc) AS cijena6, 
	                            svi_predracuni.porez AS cijena5, 
	                            SUM(svi_predracuni.kolicina) AS cijena2
                            FROM svi_predracuni  
                            LEFT JOIN stolovi ON stolovi.id_stol=CAST(svi_predracuni.id_stol AS INT)  
                            LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=CAST(svi_predracuni.id_zaposlenik AS INT)  
                            LEFT JOIN blagajna ON blagajna.id_blagajna=CAST(svi_predracuni.id_blagajna AS INT)  
                            LEFT JOIN ducan ON ducan.id_ducan=CAST(svi_predracuni.id_ducan AS INT) 
                            WHERE svi_predracuni.datum_ispisa>'{datumOD.ToString("yyyy-MM-dd 00:00:00")}' AND svi_predracuni.datum_ispisa<'{datumDO.ToString("yyyy-MM-dd 00:00:00")} {uvjet}' 
                            GROUP BY svi_predracuni.sifra, svi_predracuni.naziv, svi_predracuni.porez
                            ORDER BY CAST(svi_predracuni.sifra AS INT) ASC";
            }
            else
            {
                sql_liste = "SELECT " +
                " svi_predracuni.sifra," +
                " svi_predracuni.mpc AS cijena3," +
                " svi_predracuni.kolicina*svi_predracuni.mpc AS cijena6," +
                " svi_predracuni.porez AS cijena5," +
                " svi_predracuni.id_stol," +
                " svi_predracuni.id_zaposlenik," +
                " svi_predracuni.datum_ispisa AS string2," +
                " svi_predracuni.kolicina AS cijena2," +
                " svi_predracuni.vpc," +
                " svi_predracuni.porez_potrosnja," +
                " svi_predracuni.naziv," +
                " svi_predracuni.broj AS cijena1," +
                " svi_predracuni.id_blagajna," +
                " ducan.ime_ducana," +
                " stolovi.naziv AS string1," +
                " blagajna.ime_blagajne" +
                " FROM svi_predracuni " +
                " LEFT JOIN stolovi ON stolovi.id_stol=CAST(svi_predracuni.id_stol AS INT) " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=CAST(svi_predracuni.id_zaposlenik AS INT) " +
                " LEFT JOIN blagajna ON blagajna.id_blagajna=CAST(svi_predracuni.id_blagajna AS INT) " +
                " LEFT JOIN ducan ON ducan.id_ducan=CAST(svi_predracuni.id_ducan AS INT) " +
                " WHERE svi_predracuni.datum_ispisa>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND svi_predracuni.datum_ispisa<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "' " + uvjet + " ORDER BY CAST(svi_predracuni.id AS INT) ASC";
            }

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_liste_string = "";

            if (blagajnik_naziv != "" && ducan_naziv != "")
                sql_liste_string = "SELECT 'POSLOVNICA: " + ducan_naziv + "' AS string1,'BLAGAJNIK: " + blagajnik_naziv + "' AS string2,'OD:" + datumOD + "  DO:" + datumDO + "' AS string3";
            else if (blagajnik_naziv != "")
                sql_liste_string = "SELECT 'BLAGAJNIK: " + blagajnik_naziv + "' AS string2,'OD:" + datumOD + "  DO:" + datumDO + "' AS string3";
            else if (ducan_naziv != "")
                sql_liste_string = "SELECT 'POSLOVNICA: " + ducan_naziv + "' AS string1,'OD:" + datumOD + "  DO:" + datumDO + "' AS string3";
            else
                sql_liste_string = "SELECT '' AS string1,'' AS string2,'OD:" + datumOD + "  DO:" + datumDO + "' AS string3";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        public string GetZaposlenici()
        {
            string zaposlenik = "";
            string z = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["oznaci"].FormattedValue.ToString() == "True")
                {
                    z += "'" + dgv.Rows[i].Cells["id"].FormattedValue.ToString() + "',";
                }

                if (i == dgv.Rows.Count - 1)
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
    }
}