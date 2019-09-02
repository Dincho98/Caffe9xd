using System;
using System.Data;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmPopis_cijena_po_datumima : Form
    {
        public frmPopis_cijena_po_datumima()
        {
            InitializeComponent();
        }

        public string _id_soba { get; set; }

        private void frmPopis_cijena_po_datumima_Load(object sender, EventArgs e)
        {
            string sql = "SELECT sobe.broj_sobe,valute.ime_valute,sobe.naziv_sobe,r_cijenasoba.od_datuma,r_cijenasoba.do_datuma,r_cijenasoba.cijena_nocenja " +
                " FROM r_cijenasoba" +
                " LEFT JOIN sobe ON sobe.id=r_cijenasoba.id_soba" +
                " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                " WHERE sobe.id='" + _id_soba + "' ";

            DataTable DT = RemoteDB.select(sql, "r_cijenasoba").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                dgv.Rows.Add(row["broj_sobe"].ToString(),
                    row["naziv_sobe"].ToString(),
                    row["od_datuma"].ToString(),
                    row["do_datuma"].ToString(),
                    row["cijena_nocenja"].ToString(),
                    row["ime_valute"].ToString()
                    );
            }
        }
    }
}