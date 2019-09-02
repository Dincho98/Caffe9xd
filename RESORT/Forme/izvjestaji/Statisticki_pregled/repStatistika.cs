using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RESORT.izvjestaji.Statisticki_pregled
{
    public partial class repStatistika : Form
    {
        public repStatistika()
        {
            InitializeComponent();
        }

        private static DataTable DTstatistika = new DataTable();
        private static DataRow Drow;

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public DateTime OD { get; set; }
        public DateTime DO { get; set; }
        public string zemlja { get; set; }

        private void repFaktura_Load(object sender, EventArgs e)
        {
            DTstatistika = dSstatistike.DTstatistika;
            string zemljaFilter = "";
            if (zemlja != null && zemlja != "")
            {
                zemljaFilter = " AND unos_gosta.id_drzava='" + zemlja + "' ";
            }

            string sql = "SELECT zemlja.zemlja,unos_gosta.datum_dolaska,unos_gosta.datum_odlaska,unos_gosta.iznos_bor_pristojbe FROM unos_gosta" +
                " LEFT JOIN zemlja ON zemlja.country_code=unos_gosta.id_drzava WHERE " +
                " unos_gosta.vrijeme_unosa>='" + OD.ToString("yyyy-MM-dd H:mm:ss") + "' AND  unos_gosta.vrijeme_unosa<='" + DO.ToString("yyyy-MM-dd H:mm:ss") + "'" + zemljaFilter +
                "";

            DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

            string nas = "STATISTIČKI PRIKAZ DOLAZAKA I NOĆENJA GOSTIJU\r\nza period od " + OD.ToString("yyyy-MM-dd H:mm:ss") + " do " + DO.ToString("yyyy-MM-dd H:mm:ss") + "";

            foreach (DataRow row in DT.Rows)
            {
                int broj_dana = Funkcije.ReturnDaysFromDate(Convert.ToDateTime(row["datum_dolaska"].ToString()), Convert.ToDateTime(row["datum_odlaska"].ToString()));
                SetTable(row["zemlja"].ToString(), 1, 1 * broj_dana, (Convert.ToDouble(row["iznos_bor_pristojbe"].ToString()) * broj_dana), nas);
            }

            this.reportViewer1.RefreshReport();
        }

        private static void SetTable(string drzava, double broj_gosti, double broj_nocenja, double taksa, string naslov)
        {
            DataRow[] dataROW = DTstatistika.Select("drzava = '" + drzava + "'");

            if (dataROW.Count() == 0)
            {
                Drow = DTstatistika.NewRow();
                Drow["drzava"] = drzava;
                Drow["broj_gostiju"] = broj_gosti;
                Drow["broj_nocenja"] = broj_nocenja;
                Drow["taksa"] = taksa;
                Drow["naslov"] = naslov;
                DTstatistika.Rows.Add(Drow);
            }
            else
            {
                dataROW[0]["broj_gostiju"] = (Convert.ToDouble(dataROW[0]["broj_gostiju"].ToString()) + broj_gosti).ToString();
                dataROW[0]["broj_nocenja"] = (Convert.ToDouble(dataROW[0]["broj_nocenja"].ToString()) + broj_nocenja).ToString();
                dataROW[0]["taksa"] = (Convert.ToDouble(dataROW[0]["taksa"].ToString()) + taksa).ToString();
            }
        }

        private string BrojiRedove(string str, int br)
        {
            string vrati = "";
            for (int i = 0; i < br; i++)
            {
                if (vrati.Length == (br - str.Length))
                {
                    vrati += str;
                    return vrati;
                }
                else if ((br - str.Length) < br)
                {
                    vrati += " ";
                }
            }

            return vrati;
        }

        private void repStatistika_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}