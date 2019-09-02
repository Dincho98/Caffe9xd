using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmDodajNormativ : Form
    {
        public frmDodajNormativ()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public string sifra_prodajnog_artikla { get; set; }
        private bool SveOcitano = false;

        private void frmDodajNormativ_Load(object sender, EventArgs e)
        {
            SetCB();
            RepromaterijalSet(null, null, null);
            SveOcitano = true;
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void SetCB()
        {
            //fill grupa
            DataTable DTgrupa = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            cbGrupa.DataSource = DTgrupa;
            cbGrupa.DisplayMember = "naziv";
            cbGrupa.ValueMember = "id_podgrupa";
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            RepromaterijalSet(null, null, txtSifra.Text);
        }

        private void txtNaziv_TextChanged(object sender, EventArgs e)
        {
            RepromaterijalSet(null, txtNaziv.Text, null);
        }

        private void cbGrupa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SveOcitano == true)
                RepromaterijalSet(cbGrupa.SelectedValue.ToString(), null, null);
        }

        private void RepromaterijalSet(string grupa, string naziv, string sifra)
        {
            string query = "";
            if (grupa != "0" && grupa != null)
            {
                query = "AND roba_prodaja.id_podgrupa='" + grupa + "'";
            }

            if (naziv != "" && naziv != null)
            {
                if (classSQL.remoteConnectionString == "")
                {
                    query = "AND roba_prodaja.naziv LIKE '%" + naziv + "%'";
                }
                else
                {
                    query = "AND roba_prodaja.naziv ~* '" + naziv + "'";
                }
            }

            if (sifra != "" && sifra != null)
            {
                query = "AND roba_prodaja.sifra LIKE '%" + sifra + "%'";
            }

            string sql = "SELECT " +
            "roba_prodaja.sifra," +
            "roba_prodaja.naziv," +
            "grupa.grupa," +
            "roba_prodaja.id_podgrupa," +
            "roba_prodaja.id_grupa," +
            "roba_prodaja.ulazni_porez," +
            "roba_prodaja.izlazni_porez," +
            "roba_prodaja.id_skladiste," +
            "skladiste.skladiste," +
            "roba_prodaja.nc," +
            "roba_prodaja.porez_potrosnja," +
            "roba_prodaja.povratna_naknada," +
            "roba_prodaja.poticajna_naknada," +
            "roba_prodaja.aktivnost," +
            "roba_prodaja.mjera" +
            " FROM roba_prodaja " +
            " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste" +
            " LEFT JOIN grupa ON grupa.id_grupa=roba_prodaja.id_grupa" +
            " WHERE roba_prodaja.aktivnost is not null " + query + "ORDER BY roba_prodaja.naziv";
            DataTable DT = classSQL.select(sql, "repromaterijal").Tables[0];
            dgwR.DataSource = DT;

            dgwR.Columns["id_podgrupa"].Visible = false;
            dgwR.Columns["id_grupa"].Visible = false;
            dgwR.Columns["ulazni_porez"].Visible = false;
            dgwR.Columns["izlazni_porez"].Visible = false;
            dgwR.Columns["porez_potrosnja"].Visible = false;
            dgwR.Columns["povratna_naknada"].Visible = false;
            dgwR.Columns["poticajna_naknada"].Visible = false;
            dgwR.Columns["aktivnost"].Visible = false;
            dgwR.Columns["mjera"].Visible = false;
            dgwR.Columns["nc"].Visible = false;
            dgwR.Columns["id_skladiste"].Visible = false;

            dgwR.Columns["sifra"].HeaderText = "Šifra";
            dgwR.Columns["naziv"].HeaderText = "Naziv";
            dgwR.Columns["skladiste"].HeaderText = "Skladište";
            dgwR.Columns["grupa"].HeaderText = "Grupa";

            dgwR.Columns["sifra"].Width = 200;

            PaintRows(dgwR);
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
            row.Height = 20;
        }

        private void dgwR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite dodati ovaj normativ odabranoj stavki?", "Upit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string sql = "INSERT INTO caffe_normativ " +
                "(sifra,sifra_normativ,kolicina,id_skladiste,novo) " +
                " VALUES " +
                " (" +
                "'" + sifra_prodajnog_artikla + "'," +
                "'" + dgwR.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'," +
                "'0'," +
                "'" + dgwR.Rows[e.RowIndex].Cells["id_skladiste"].FormattedValue.ToString() + "','1'" +
                ") " +
                "";

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                " ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Dodavanje normativa po prodajnoj šifri: " + sifra_prodajnog_artikla + " i po normativu: " + dgwR.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "')"));

            provjera_sql(classSQL.insert(sql));
            this.Close();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnDodajNoviArtikl_Click(object sender, EventArgs e)
        {
            Caffe.frmRepromaterijal r = new frmRepromaterijal();
            r.ShowDialog();
            RepromaterijalSet(null, null, null);
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