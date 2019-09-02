using RESORT.Forme.izvjestaji.Avansi;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT
{
    public partial class FrmSviAvansi : Form
    {
        public frmAvans MainForm { get; set; }
        public string sifra_avansa;

        public FrmSviAvansi()
        {
            InitializeComponent();
        }

        private DataSet DSavansi = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();

        private void FrmSviAvansi_Load(object sender, EventArgs e)
        {
            kreirajTablice();
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
            fillCB();
            fillDataGrid();

            //DTpostavke = RemoteDB.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "avansi.broj_avansa = '" + txtBroj.Text + "' AND ";
            }
            if (chbVD.Checked)
            {
                VD = "avansi.id_vd = '" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "avansi.dat_knj >= '" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "avansi.dat_knj <= '" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "avansi.id_valuta = '" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "avansi.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = "SELECT DISTINCT " + top + " avansi.broj_avansa AS [Broj avansa], avansi.dat_knj AS [Datum knjiženja], avansi.dat_dok as [Datum], valute.ime_valute as [Valuta]" +
                    ", avansi.godina_avansa as [Godina], avansi.ukupno as [Ukupno] " +
                    " FROM avansi INNER JOIN valute ON avansi.id_valuta = valute.id_valuta " +
                    filter + " ORDER BY avansi.dat_knj DESC" + remote;

            DSavansi = RemoteDB.select(sql, "avansi");
            dgv.DataSource = DSavansi.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void printaj()
        {
            repAvans rav = new repAvans();
            rav.dokumenat = "AVA";
            rav.ImeForme = "Avansi";
            rav.broj_dokumenta = dgv.CurrentRow.Cells["Broj avansa"].FormattedValue.ToString();
            rav.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
            rav.ShowDialog();
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = RemoteDB.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = RemoteDB.select("SELECT * FROM avansi_vd ORDER BY id_vd", "avansi_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = RemoteDB.select("SELECT ime + ' ' + prezime as IME, id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (RemoteDB.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " avansi.broj_avansa AS [Broj avansa]," +
                " avansi.dat_knj AS [Datum knjiženja], " +
                "avansi.dat_dok as [Datum], avansi.godina_avansa as [Godina], valute.ime_valute as [Valuta], avansi.ukupno as [Ukupno] " +
                " FROM avansi INNER JOIN valute ON avansi.id_valuta = valute.id_valuta " +
                " ORDER BY CAST(avansi.broj_avansa AS integer) DESC" +
                "" + remote + "";

            DSavansi = RemoteDB.select(sql, "avansi");
            dgv.DataSource = DSavansi.Tables[0];

            dgv.Columns["Godina"].Visible = false;

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
            //PaintRows(dgv);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                int avans;
                int godina;

                try
                {
                    avans = Convert.ToInt16(dgv.CurrentRow.Cells["Broj avansa"].Value.ToString());
                    godina = Convert.ToInt16(dgv.CurrentRow.Cells["Godina"].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                    return;
                }

                if (MainForm == null)
                {
                    frmAvans childForm = new frmAvans();
                    childForm.broj_avansa_edit = avans;
                    childForm.godina = godina;
                    childForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MainForm.broj_avansa_edit = avans;
                    MainForm.godina = godina;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void kreirajTablice()
        {
            DataTable DTremote = RemoteDB.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            if (DTremote.Rows.Count > 0)
            {
                DataRow[] dataROW = DTremote.Select("table_name = 'avansi'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE avansi" +
                                            "(broj_avansa bigint NOT NULL," +
                                            "dat_dok timestamp without time zone," +
                                            "dat_knj timestamp without time zone," +
                                            "id_zaposlenik integer," +
                                            "id_zaposlenik_izradio integer," +
                                            "model character varying(10)," +
                                            "id_nacin_placanja bigint," +
                                            "id_valuta integer," +
                                            "opis text," +
                                            "id_vd character(5)," +
                                            "godina_avansa character(6)," +
                                            "ukupno numeric," +
                                            "ziro character varying(30)," +
                                            "nult_stp numeric," +
                                            "neoporezivo numeric," +
                                            "osnovica10 numeric," +
                                            "osnovica_var numeric," +
                                            "porez_var numeric," +
                                            "id_pdv integer," +
                                            "CONSTRAINT broj_avansa PRIMARY KEY (broj_avansa )" +
                                            ")";
                    RemoteDB.select(sql, "avansi");
                }

                dataROW = DTremote.Select("table_name = 'avansi_vd'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE avansi_vd" +
                        "id_vd bigint NOT NULL," +
                        "vd character varying(30)," +
                        "grupa character varying(5)," +
                        "CONSTRAINT primary_key_id_vd PRIMARY KEY (id_vd )" +
                        ")";
                    RemoteDB.select(sql, "avansi_vd");
                    RemoteDB.insert("INSERT INTO avansi_vd (id_vd,vd,grupa) VALUES ('Predujam','IP')");
                    RemoteDB.insert("INSERT INTO avansi_vd (id_vd,vd,grupa) VALUES ('Storno primljenog predujma','PRS')");
                }
            }
        }
    }
}