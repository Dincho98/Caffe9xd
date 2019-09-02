using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Until
{
    public partial class frmUvozArtiklaCSV : Form
    {
        public frmUvozArtiklaCSV()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmUvozArtiklaCSV_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        #region UČITAJ CSV U DATAGRID

        private void loadXLSToDataGrid(string path)
        {
            string[] lines = File.ReadAllLines(path, Encoding.Default);
            string greske = "";
            int broj_row = 0;

            //**********************************************
            //Sifra, Naziv, Mjera, PDV, PNP, Grupa, MPC
            //*************OVO SU KOLONE EXCELA*************

            foreach (string art in lines)
            {
                string[] arrVrijednost = art.Split(';');
                if (arrVrijednost.Length == 7 && broj_row > 0)
                {
                    if (arrVrijednost[0] != "" && arrVrijednost[1] != "" && arrVrijednost[2] != "")
                    {
                        dgv.Rows.Add(arrVrijednost[0].Replace("\"", ""),
                            arrVrijednost[1].Replace("\"", ""),
                            arrVrijednost[2].Replace("\"", ""),
                            arrVrijednost[3].Replace("\"", ""),
                            arrVrijednost[4].Replace("\"", ""),
                            arrVrijednost[5].Replace("\"", ""),
                            arrVrijednost[6].Replace("\"", ""));
                    }
                }
                else if (broj_row > 0)
                {
                    greske += "Greška u kolonama\n";
                }
                broj_row++;
            }

            if (greske.Length > 0)
            {
                MessageBox.Show("Došlo je do pogreške kod učitavanja artikla.\r\n" + greske);
                dgv.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Podaci su uspješno učitani.");
            }
        }

        #endregion UČITAJ CSV U DATAGRID

        #region SPREMI U BAZU IZ DATAGRIDA

        private void SpremiUBazu()
        {
            string greske = "";
            int broj_row = 0;
            string query = "", id_grupa = "1", id_podgrupa = "1";
            decimal mpc = 0;
            int porez = 0, pnp = 0;
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("BEGIN;");
            foreach (DataGridViewRow art in dgv.Rows)
            {
                decimal.TryParse(art.Cells["mpc"].FormattedValue.ToString().Replace(".", ","), out mpc);
                int.TryParse(art.Cells["pdv"].FormattedValue.ToString().Replace(".", ","), out porez);
                int.TryParse(art.Cells["pnp"].FormattedValue.ToString().Replace(".", ","), out pnp);

                //DODJELJUJEM GRUPU PREMA NAZIVU
                DataTable DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + art.Cells["grupa_"].FormattedValue.ToString() + "'", "").Tables[0];
                if (DTgrupa.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO grupa (grupa,id_podgrupa,aktivnost,editirano,novo) VALUES " +
                        "('" + art.Cells["grupa_"].FormattedValue.ToString() + "','1','1','1','1');");
                    DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + art.Cells["grupa_"].FormattedValue.ToString() + "' ORDER BY id_grupa DESC LIMIT 1;", "").Tables[0];
                }
                id_grupa = DTgrupa.Rows[0]["id_grupa"].ToString();
                id_podgrupa = DTgrupa.Rows[0]["id_podgrupa"].ToString();

                //PROVJERA ROBE DA NEBI DOŠLO DO DVIJE ISTE ŠIGRE
                DataTable DTroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + art.Cells["sifra"].FormattedValue.ToString() + "';", "").Tables[0];
                if (DTroba.Rows.Count > 0)
                {
                    query = "UPDATE roba SET " +
                        " naziv='" + art.Cells["naziv"].FormattedValue.ToString() + "'," +
                        " id_grupa='" + id_grupa + "'," +//grupa ???
                        " jm='" + art.Cells["mjera"].FormattedValue.ToString() + "'," +//jm
                        " mpc='" + mpc.ToString().Replace(".", ",") + "'," +
                        " porez='" + porez + "'," +
                        " aktivnost='1'," +
                        " porez_potrosnja='" + pnp + "'" +
                        " WHERE sifra='" + art.Cells["sifra"].FormattedValue.ToString() + "'; ";
                    sbSQL.Append(query);
                }
                else
                {
                    //**********************************************
                    //Sifra, Naziv, Mjera, PDV, PNP, Grupa, MPC
                    //*************OVO SU KOLONE EXCELA*************

                    query = "INSERT INTO roba (naziv,id_grupa,jm,mpc,sifra,ean,porez,porez_potrosnja,aktivnost,id_podgrupa," +
                        " border_color,button_style,brojcanik,nbc,editirano,novo,porezna_grupa) " +
                        " VALUES (" +
                        "'" + art.Cells["naziv"].FormattedValue.ToString() + "'," +
                        "'" + id_grupa + "'," +//grupa ???
                        "'" + art.Cells["mjera"].FormattedValue.ToString() + "'," +//jm
                        "'" + mpc.ToString().Replace(".", ",") + "'," +
                        "'" + art.Cells["sifra"].FormattedValue.ToString() + "'," +//sifra
                        "'0'," +
                        "'" + porez + "'," +
                        "'" + pnp + "'," +
                        "'1'," +
                        "'" + id_podgrupa + "'," +
                        "'0'," +
                        "''," +
                        "'0'," +
                        "'0'," +
                        "'1'," +
                        "'1'," +
                        "'O'" +
                        "); ";
                    sbSQL.Append(query);
                }
            }

            broj_row++;

            sbSQL.Append("COMMIT;");
            classSQL.insert(sbSQL.ToString());

            if (greske.Length > 0)
            {
                MessageBox.Show("Došlo je do pogreške kod učitavanja artikla.\r\n" + greske);
            }
            else
            {
                MessageBox.Show("Podaci su uspješno spremljeni.");
            }
        }

        #endregion SPREMI U BAZU IZ DATAGRIDA

        #region IZVOZ IZ PROGRAMA

        private void btnIzvoz_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV|*.csv";
            saveFileDialog1.Title = "Spremi csv datoteku";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                DataTable DTroba = classSQL.select("SELECT roba.*,grupa.grupa FROM roba LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa;", "roba").Tables[0];
                StringBuilder sbCSV = new StringBuilder();
                decimal nbc;
                sbCSV.AppendLine("Sifra;Naziv;Mjera;PDV;PNP;Grupa;MPC;NBC;IdPodgrupa;Porezna grupa");
                foreach (DataRow r in DTroba.Rows)
                {
                    decimal.TryParse(r["nbc"].ToString(), out nbc);

                    sbCSV.AppendLine(r["sifra"].ToString().Replace(";", "") + ";" +
                        r["naziv"].ToString().Replace(";", "") + ";" +
                        r["jm"].ToString().Replace(";", "") + ";" +
                        r["porez"].ToString().Replace(";", "") + ";" +
                        r["porez_potrosnja"].ToString().Replace(";", "") + ";" +
                        r["grupa"].ToString().Replace(";", "") + ";" +
                        r["mpc"].ToString().Replace(";", "") + ";" +
                        Math.Round(nbc, 4) + ";" +
                        r["id_podgrupa"].ToString().Replace(";", "") + ";" +
                        r["porezna_grupa"].ToString().Replace(";", ""));
                }

                string dd = sbCSV.ToString();
                File.WriteAllText(saveFileDialog1.FileName, sbCSV.ToString(), Encoding.Default);

                if (MessageBox.Show("Uspješno spremljeno, želite li otvoriti CSV datoteku?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(saveFileDialog1.FileName);
                }
            }
        }

        #endregion IZVOZ IZ PROGRAMA

        private void btnUcitajExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();

            saveFileDialog1.Filter = "Excel files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                loadXLSToDataGrid(file);
            }
        }

        private void btnObrisiArtikle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati sve artikle?", "Brisanje artikla", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            DataTable DTrac = classSQL.select("SELECT * FROM racuni LIMIT 1", "racuni").Tables[0];
            if (DTrac.Rows.Count > 0)
            {
                MessageBox.Show("Nije moguće obrisati artikle jer su korišteni.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                classSQL.delete("DELETE FROM roba; DELETE FROM roba_prodaja");
            }
        }

        private void btnPostaviNeaktivneArtikle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite sve artikle postaviti na neaktivne?", "Brisanje artikla", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            classSQL.update("UPDATE roba SET aktivnost='0';");
            MessageBox.Show("Svi artikli uspješno su postavljeni na neaktivnost.");
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            SpremiUBazu();
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