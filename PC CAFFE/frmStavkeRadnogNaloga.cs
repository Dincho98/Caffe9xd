using System;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmStavkeRadnogNaloga : Form
    {
        public frmStavkeRadnogNaloga()
        {
            InitializeComponent();
        }

        public frmRadniNalog MainForm { get; set; }
        public string sf_artikla { get; set; }

        private void frmStavkeRadnogNaloga_Load(object sender, EventArgs e)
        {
            string sql = "SELECT normativi_stavke.sifra_robe AS [Šifra artikla],roba.naziv AS Naziv,normativi_stavke.kolicina as Količina" +
                ",skladiste.skladiste as Skladište" +
                " FROM normativi_stavke INNER JOIN normativi ON normativi.broj_normativa=normativi_stavke.broj_normativa " +
                " INNER JOIN skladiste ON skladiste.id_skladiste=normativi_stavke.id_skladiste " +
                " INNER JOIN roba ON normativi_stavke.sifra_robe=roba.sifra " +
                " WHERE normativi.sifra_artikla ='" + sf_artikla + "'";
            dataGridView1.DataSource = classSQL.select(sql, "normativi_stavke").Tables[0];
        }
    }
}