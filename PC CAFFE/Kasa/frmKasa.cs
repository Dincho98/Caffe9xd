using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasa : Form
    {
        public frmKasa()
        {
            InitializeComponent();
        }

        public string IznosKartica { get; set; }
        public string IznosGotovina { get; set; }
        public string DobivenoGotovina { get; set; }
        public string placanje { get; set; }

        public string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifra_skladiste { get; set; }
        private DataTable DTpostavkePrinter;
        private DataSet DS_Skladiste;
        private DataSet DSpostavke;
        private DataTable DTpromocije;
        private DataTable DTpromocije1;
        private DataTable DTsend = new DataTable();
        private double ukupno = 0;
        private string brRac;
        private string blagajnik_ime;
        private string sifraPartnera = "0";
        public frmMenu MainForm { get; set; }

        private void frmKasa_Load(object sender, EventArgs e)
        {
            //string sql = "SELECT sifra,mpc FROM roba";
            //DataTable DT = classSQL.select(sql, "roba").Tables[0];

            //for (int i = 0; i < DT.Rows.Count; i++)
            //{
            //    double mpc = Convert.ToDouble(DT.Rows[i]["mpc"].ToString());
            //    double vpc = mpc / 1.25;
            //    double sifra = (vpc);

            //    string s = "UPDATE roba SET vpc='" + vpc.ToString().Replace(",", ".") + "' WHERE sifra='" + DT.Rows[i]["sifra"].ToString() + "'";
            //    provjera_sql(classSQL.update(s));
            //}

            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();
                sifra_skladiste = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                //blagajnik_ime = DSpostavke.Tables[0].Rows[][].ToString();
            }

            SetSkladiste();
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            blagajnik_ime = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik = '" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            SetSize();
            DTpromocije = classSQL.select("SELECT * FROM promocije WHERE do_datuma >='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AND od_datuma <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'", "promocije").Tables[0];

            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            //backgroundWorkerConnectPos.RunWorkerAsync();
            PaintRows(dgv);
            //this.Paint += new PaintEventHandler(Form1_Paint);
            panTastatura.Visible = true;
            this.CBskladiste.SelectedIndexChanged += new System.EventHandler(this.CBskladiste_SelectedIndexChanged);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void SetSize()
        {
            //if(this.Width>1300)
            //{
            //    panTastatura.Visible = true;
            //}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetSkladiste()
        {
            //DS skladiste
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste ORDER BY skladiste", "skladiste");
            CBskladiste.DataSource = DS_Skladiste.Tables[0];
            CBskladiste.DisplayMember = "skladiste";
            CBskladiste.ValueMember = "id_skladiste";

            CBskladiste.SelectedValue = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
            //IFcomboBoxTrue = "load";
            //DataTable DTSK = new DataTable("skladiste");
            //DTSK.Columns.Add("id_skladiste", typeof(string));
            //DTSK.Columns.Add("skladiste", typeof(string));

            //DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            //DTSK.Rows.Add(0, "");
            //for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            //{
            //    DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            //}

            //skladiste.DataSource = DTSK;
            //skladiste.DataPropertyName = "skladiste";
            //skladiste.DisplayMember = "skladiste";
            //skladiste.HeaderText = "SKLADIŠTE";
            //skladiste.Name = "skladiste";
            //skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            //skladiste.ValueMember = "id_skladiste";
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int col = dgv.CurrentCell.ColumnIndex;
                int row = dgv.CurrentCell.RowIndex;

                if (col < dgv.ColumnCount - 1)
                {
                    col++;
                }
                else
                {
                    col = 0;
                    row++;
                }

                if (row == dgv.RowCount)
                    dgv.Rows.Add();

                //SendKeys.Send("{TAB}");

                //datagridview.CurrentCell = datagridview[col, row];
                dgv.BeginEdit(true);
                e.Handled = true;
            }

            if (e.KeyData == Keys.Right)
            {
                e.Handled = true;
                DataGridViewCell cell =
                dgv.Rows[0].Cells[dgv.CurrentCell.ColumnIndex];
                dgv.CurrentCell = cell;
                dgv.BeginEdit(true);
                //dgv.CurrentCell=dgv.CurrentRow.Cells[3];
            }
        }

        //private DataGridViewCell _celWasEndEdit;
        //private void datagridview_SelectionChanged(object sender, EventArgs e)
        //{
        //    //if (dgv.CurrentRow.Cells["kolicina"].ReadOnly == true) { MessageBox.Show(""); }

        //    if (MouseButtons != 0) return;
        //    dgv.BeginEdit(true);
        //    if (_celWasEndEdit != null && dgv.CurrentCell != null)
        //    {
        //        // if we are currently in the next line of last edit cell
        //        if (dgv.CurrentCell.RowIndex == _celWasEndEdit.RowIndex + 1 && dgv.CurrentCell.ColumnIndex == _celWasEndEdit.ColumnIndex)
        //        {
        //            int iColNew;
        //            int iRowNew = 0;
        //            if (_celWasEndEdit.ColumnIndex >= dgv.ColumnCount - 1)
        //            {
        //                iColNew = 0;
        //                iRowNew = dgv.CurrentCell.RowIndex;
        //                if (dgv.CurrentCell.ColumnIndex == 0)
        //                {
        //                    if (krivaSifra == false)
        //                    {
        //                        dgv.CurrentCell = dgv[3, iRowNew];
        //                    }
        //                    else
        //                    {
        //                        dgv.CurrentCell=dgv[0,iRowNew];
        //                    }
        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 3)
        //                {
        //                    dgv.CurrentCell = dgv[6, iRowNew];
        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 5)
        //                {
        //                    dgv.CurrentCell = dgv[5, iRowNew];
        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 6)
        //                {
        //                    dgv.CurrentCell = dgv[0, iRowNew+1];
        //                }
        //            }
        //            else
        //            {
        //                iColNew = _celWasEndEdit.ColumnIndex + 1;
        //                iRowNew = _celWasEndEdit.RowIndex;
        //                if (dgv.CurrentCell.ColumnIndex == 0)
        //                {
        //                    if (krivaSifra == false)
        //                    {
        //                        dgv.CurrentCell = dgv[3, iRowNew];
        //                    }
        //                    else
        //                    {
        //                        dgv.CurrentCell = dgv[0, iRowNew];
        //                    }
        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 3)
        //                {
        //                    int aa = dgv.CurrentRow.Index - 1;
        //                    if (dgv.Rows[aa].Cells["kolicina"].ReadOnly == true)
        //                    {
        //                        dgv.CurrentCell = dgv[6, iRowNew];
        //                    }
        //                    else
        //                    {
        //                        dgv.CurrentCell = dgv[5, iRowNew];
        //                    }

        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 5)
        //                {
        //                    dgv.CurrentCell = dgv[6, iRowNew];
        //                }
        //                else if (dgv.CurrentCell.ColumnIndex == 6)
        //                {
        //                    dgv.CurrentCell = dgv[0, iRowNew + 1];
        //                }
        //            }
        //        }
        //        _celWasEndEdit = null;
        //    }
        //}

        //private void datagridview_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgv.CurrentCell.ColumnIndex == 0 && dgv.CurrentCell.Value!=null)
        //    {
        //        if (dgv.CurrentRow.Cells["kolicina"].Value == null || dgv.CurrentRow.Cells["popust"].Value == null)
        //        {
        //            SetRoba(dgv.CurrentCell.FormattedValue.ToString());
        //        }
        //    }
        //    else if (dgv.CurrentCell.ColumnIndex == 3 && dgv.CurrentRow.Cells[0].Value != null && dgv.CurrentRow.Cells[3].Value != null)
        //    {
        //        SetSkladiste(dgv.CurrentCell.Value.ToString());
        //    }
        //    else if (dgv.CurrentCell.ColumnIndex == 5 && dgv.CurrentRow.Cells[0].Value != null && dgv.CurrentRow.Cells[5].Value != null)
        //    {
        //        SetKolicina(Convert.ToDouble(dgv.CurrentCell.FormattedValue.ToString()));
        //    }
        //    else if (dgv.CurrentCell.ColumnIndex == 6 && dgv.CurrentRow.Cells[0].Value != null && dgv.CurrentRow.Cells[6].Value != null)
        //    {
        //        SetRabat(Convert.ToDouble(dgv.CurrentCell.FormattedValue.ToString()));
        //    }
        //    _celWasEndEdit = dgv[e.ColumnIndex, e.RowIndex];
        //}

        private void SetSkladiste(string skladiste)
        {
            if (dgv.RowCount > 0)
            {
                if (dgv.CurrentRow.Cells[8].FormattedValue.ToString() == "DA")
                {
                    DataTable DSprodaja = classSQL.select("SELECT vpc,porez,kolicina FROM roba_prodaja WHERE sifra='" + dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja").Tables[0];
                    if (DSprodaja.Rows.Count > 0)
                    {
                        dgv.CurrentRow.Cells[4].Value = (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) + Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString());
                        dgv.CurrentRow.Cells[7].Value = (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) + Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString());

                        double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
                        double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
                        double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
                        double ukupno;
                        ukupno = cijena * kolicina;
                        rabat = ukupno * rabat / 100;
                        dgv.CurrentRow.Cells[7].Value = String.Format("{0:0.00}", ukupno - rabat);
                        Ukupno();
                    }
                }
            }
        }

        private void SetRabat(double rabat)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
            double ukupno;
            ukupno = cijena * kolicina;
            rabat = ukupno * rabat / 100;
            dgv.CurrentRow.Cells[7].Value = String.Format("{0:0.00}", ukupno - rabat);
            Ukupno();
        }

        private void SetKolicina(double kolicina)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
            double ukupno;
            ukupno = cijena * kolicina;
            rabat = ukupno * rabat / 100;
            dgv.CurrentRow.Cells[7].Value = String.Format("{0:0.00}", ukupno - rabat);
            Ukupno();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "C")
            {
                txtUnos.Text = txtUnos.Text.Remove(txtUnos.Text.Length - 1);
            }
            else
            {
                txtUnos.Text += btn.Text;
            }
        }

        private void SetRoba(string sifra)
        {
            if (txtUnos.Text.Length > 2)
            {
                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtUnos.Text.Substring(0, 3) == "000")
                {
                    double uk;
                    double popust;
                    DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtUnos.Text.Substring(3, txtUnos.Text.Length - 3) + "' AND dokumenat='RN'", "racun_popust_kod_sljedece_kupnje").Tables[0];

                    if (DTrp.Rows.Count == 0)
                    {
                        MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
                    {
                        MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                    if (dateFromPopust < DateTime.Now)
                    {
                        MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                    popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                    uk = uk * popust / 100;

                    if ((ukupno - uk) < 0)
                    {
                        MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dgv.Rows.Add(
                                txtUnos.Text,
                                "Popust sa prethodnog računa",
                                "kn",
                                1,
                                String.Format("{0:0.00}", (uk * (-1))),
                                "1",
                                0,
                                String.Format("{0:0.00}", (uk * (-1))),
                                "",
                                DSpostavke.Tables[0].Rows[0]["pdv"].ToString(),
                                (uk * (-1)) / Convert.ToDouble("1," + DSpostavke.Tables[0].Rows[0]["pdv"].ToString())
                            );

                    Ukupno();
                    dgv.Select();
                    dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[3];
                    PaintRows(dgv);
                    return;
                }
            }

            string ItemDisplay = "";
            string PriceItem = "";
            DataTable DTroba = classSQL.select("SELECT nc,sifra,oduzmi,naziv,jm,vpc,mpc,porez FROM roba WHERE sifra='" + sifra + "'", "roba").Tables[0];
            if (DTroba.Rows.Count != 0)
            {
                PriceItem = String.Format("{0:0.00}", DTroba.Rows[0]["mpc"]);
                ItemDisplay = DTroba.Rows[0]["naziv"].ToString();

                dgv.Rows.Add(
                DTroba.Rows[0]["sifra"].ToString(),
                ItemDisplay,
                DTroba.Rows[0]["jm"].ToString(),
                CBskladiste.SelectedValue,
                String.Format("{0:0.00}", DTroba.Rows[0]["mpc"]),
                "1",
                "0",
                PriceItem,
                DTroba.Rows[0]["oduzmi"].ToString(),
                DTroba.Rows[0]["porez"].ToString(),
                DTroba.Rows[0]["vpc"].ToString(),
                DTroba.Rows[0]["nc"].ToString());
                dgv.Select();
                dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[3];
                txtUnos.Select();

                if (DTroba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    dgv.CurrentRow.Cells[3].Value = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                    //CBskladiste.SelectedValue = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                    DataTable DSprodaja = classSQL.select("SELECT nc,vpc,porez,kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString() + "'", "roba_prodaja").Tables[0];
                    if (DSprodaja.Rows.Count > 0)
                    {
                        dgv.CurrentRow.Cells["vpc"].Value = DSprodaja.Rows[0]["vpc"].ToString();
                        dgv.CurrentRow.Cells["nbc"].Value = DSprodaja.Rows[0]["nc"].ToString();
                        string p = DSprodaja.Rows[0]["porez"].ToString();
                        dgv.CurrentRow.Cells[4].Value = String.Format("{0:0.00}", (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) + Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()));
                        dgv.CurrentRow.Cells[7].Value = String.Format("{0:0.00}", (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) + Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()));
                        GetKolicinaSkaldiste(sifra, CBskladiste.SelectedValue.ToString(), dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                        //Ukupno();
                    }
                    else
                    {
                        GetKolicinaSkaldiste(sifra, CBskladiste.SelectedValue.ToString(), dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                    }
                }
                else
                {
                    lblSkladiste.Text = "";
                }

                ProvjeraPromocije(sifra);
                //krivaSifra = false;
                Ukupno();
                PaintRows(dgv);
                txtUnos.Text = "";
            }
            else
            {
                //krivaSifra = true;
                MessageBox.Show("Kriva šifra", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void GetKolicinaSkaldiste(string sifra, string skladiste, string jmj)
        {
            DataTable DTkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "'", "roba_prodaja").Tables[0];

            if (DTkol.Rows.Count == 0)
            {
                if (MessageBox.Show("Na odabranom skladišnu nemate traženi artikl/uslugu.\r\nŽelite li nastaviti?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu ima 0 " + jmj;
                    lblSkladiste.ForeColor = Color.Red;
                }
            }
            else if (Convert.ToDecimal(DTkol.Rows[0][0].ToString()) < 0)
            {
                if (MessageBox.Show("Na odabranom skladišnu nemate traženi artikl/uslugu.\r\nŽelite li nastaviti?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu ima " + DTkol.Rows[0][0].ToString() + " " + jmj;
                    lblSkladiste.ForeColor = Color.Red;
                }
            }
            else
            {
                lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu ima " + DTkol.Rows[0][0].ToString() + " " + jmj;
                lblSkladiste.ForeColor = Color.Lime;
            }
        }

        private void Ukupno()
        {
            ukupno = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["iznos"].Value != null)
                {
                    ukupno += Convert.ToDouble(dgv.Rows[i].Cells["iznos"].FormattedValue);
                    lblUkupno.Text = String.Format("{0:0.00}", ukupno) + " Kn";
                }
            }

            if (DTpostavkePrinter.Rows[0]["lineDisplay_bool"].ToString() == "1")
            {
                backgroundWorkerSendToDisplay.RunWorkerAsync();
            }
        }

        private string artikl_start = "";
        private string cijena_start = "";
        private string artikl_display = "";
        private string cijena_display = "";

        private void backgroundWorkerSendToDisplay_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = dgv.Rows.Count - 1;
            if (dgv.Rows[i].Cells["naziv"].FormattedValue.ToString() == "") { return; }
            artikl_start = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
            cijena_start = dgv.Rows[i].Cells["iznos"].FormattedValue.ToString();
            if (artikl_start.Length > 12) { artikl_start = artikl_start.Substring(0, 12); }

            if (cijena_start != cijena_display || artikl_start != artikl_display)
            {
                cijena_display = cijena_start;
                artikl_display = artikl_start;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da želite odustati?", "Odustani", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                brRac = brojRacuna();
                lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
                SetOnNull();
            }
            txtUnos.Select();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtImePartnera.Text = "";
            txtUnos.Select();
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    sifraPartnera = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    sifraPartnera = "0";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            txtUnos.Select();
        }

        private void SetOnNull()
        {
            dgv.Rows.Clear();
            txtImePartnera.Text = "";
            txtImePartnera.Text = "";
            lblUkupno.Text = "0,00 Kn";
            sifraPartnera = "0";
        }

        private void NoviUnos()
        {
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            SetOnNull();
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni  WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnGotovina_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            frmUzvratiti objForm2 = new frmUzvratiti();
            IznosGotovina = null;
            IznosKartica = null;
            objForm2.getUkupnoKasa = ukupno.ToString();
            objForm2.getNacin = "GO";
            objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null)
            {
                SpremiRacun(IznosKartica, IznosGotovina);
                NoviUnos();
                txtUnos.Select();
            }
        }

        private void btnKartica_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            frmUzvratiti objForm2 = new frmUzvratiti();
            objForm2.getUkupnoKasa = ukupno.ToString();
            objForm2.getNacin = "KA";
            objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null)
            {
                SpremiRacun(IznosKartica, IznosGotovina);
                NoviUnos();
                txtUnos.Select();
            }
        }

        private string barcode = "";

        private void SpremiRacun(string kartica, string gotovina)
        {
            string kol;
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DataRow row;

            string g;
            string k;
            if (IznosGotovina != null)
            {
                g = "1";
            }
            else
            {
                g = "0";
            }

            if (IznosKartica != null)
            {
                k = "1";
            }
            else
            {
                k = "0";
            }

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            string uk1 = ukupno.ToString();
            string dobiveno_gotovina;
            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
                IznosGotovina = IznosGotovina.Replace(",", ".");
                IznosKartica = IznosKartica.Replace(",", ".");
                dobiveno_gotovina = DobivenoGotovina.Replace(",", ".");
            }
            else
            {
                IznosGotovina = IznosGotovina.Replace(".", ",");
                IznosKartica = IznosKartica.Replace(".", ",");
                uk1 = uk1.Replace(".", ",");
                dobiveno_gotovina = DobivenoGotovina.Replace(".", ",");
            }

            brRac = brojRacuna();
            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina,kartice,ukupno_gotovina,ukupno_kartice,broj_kartice_cashback,broj_kartice_bodovi,br_sa_prethodnog_racuna,ukupno,storno,dobiveno_gotovina) " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + g + "'," +
                "'" + k + "'," +
                "'" + IznosGotovina + "'," +
                "'" + IznosKartica + "'," +
                "'" + txtBrojKarticeCB.Text + "'," +
                "'" + txtBrojKarticeSB.Text + "'," +
                "'" + txtBrojKarticePO.Text + "'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'" + dobiveno_gotovina + "'" +
                ")";

            provjera_sql(classSQL.insert(sql));
            string sifra = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dg(i, "oduzmi") == "DA")
                {
                    kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgv.Rows[i].Cells["skladiste"].Value.ToString(), dg(i, "kolicina"), "1", "-");
                    SQL.SQLroba_prodaja.UpdateRows(dgv.Rows[i].Cells["skladiste"].Value.ToString(), kol, dg(i, "sifra"));
                }

                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["broj_racuna"] = brRac;
                row["sifra_robe"] = ReturnSifra(sifra);
                row["id_skladiste"] = dgv.Rows[i].Cells["skladiste"].Value;
                row["mpc"] = dg(i, "cijena");
                row["porez"] = dg(i, "porez");
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "popust");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nbc");
                row["cijena"] = dg(i, "cijena");
                row["ime"] = dg(i, "naziv");
                row["porez_potrosnja"] = "0";
                DTsend.Rows.Add(row);

                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "' AND dokumenat='RN'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            sifra_skladiste = "";
            provjera_sql(SQL.SQLracun.InsertStavke(DTsend));

            barcode = "000" + brRac;
            if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
            {
                string uk = ukupno.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    uk = uk.Replace(",", ".");
                }
                else
                {
                    uk = uk.Replace(".", ",");
                }

                provjera_sql(classSQL.insert("INSERT INTO racun_popust_kod_sljedece_kupnje (broj_racuna,datum,ukupno,popust,koristeno,dokumenat) VALUES (" +
                     "'" + brRac + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     "'" + uk + "'," +
                     "'" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                     "'NE'," +
                     "'RN'" +
                     ")"));
            }

            if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
            {
                try
                {
                    if (Convert.ToDecimal(IznosGotovina) == 0 && Convert.ToDecimal(IznosKartica) > 0)
                    {
                        placanje = "K";
                    }
                    else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) == 0)
                    {
                        placanje = "G";
                    }
                    else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) > 0)
                    {
                        placanje = "O";
                    }

                    PosPrint.classPosPrintMaloprodaja.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" + DateTime.Now.Year.ToString(), sifraPartnera, barcode, brRac, placanje);
                    //PosPrint.classPosPrint.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" + DateTime.Now.Year.ToString(), DTpostavkePrinter, sifraPartnera, barcode,brRac);
                }
                catch (Exception)
                {
                    if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\nŽelite li ispisati ovaj dokumenat na A4 format?", "Printer") == DialogResult.Yes)
                    {
                        Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                        rfak.dokumenat = "RAC";
                        rfak.ImeForme = "Račun";
                        rfak.broj_dokumenta = brRac;
                        rfak.ShowDialog();
                    }
                }
            }
            else
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.broj_dokumenta = brRac;
                rfak.ShowDialog();
            }

            Kasa.frmPodsjetnik p = new Kasa.frmPodsjetnik();
            p.ShowDialog();
        }

        private string ReturnSifra(string sifra)
        {
            try
            {
                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                {
                    return "00000";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return sifra;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string dg(int row, string cell)
        {
            return dgv.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnOdustaniCB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeCB.Text = "";
            txtPodaciOvlasnikuCB.Text = "";
        }

        private void btnOdustaniSB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeSB.Text = "";
            txtPodaciOvlasnikuSB.Text = "";
        }

        private void btnOdustaniPO_Click(object sender, EventArgs e)
        {
            txtBrojKarticePO.Text = "";
            txtPodaciOvlasnikuPO.Text = "";
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if(dgv.CurrentCell.ColumnIndex==3)
            //{
            //    var txtEdit = (ComboBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
            //else
            //{
            //    var txtEdit = (TextBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
        }

        private void EditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnAlati_Click(btnAlati, e);
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnPartner_Click(btnPartner, e);
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnObrisiStavku_Click(btnObrisiStavku, e);
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnGotovina_Click(btnGotovina, e);
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnKartica_Click(btnKartica, e);
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                //btnOdustani_Click(btnOdustani, e);
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private int brojac = 0;
        private int brojac_po3 = 1;
        private double brojac_iznos = 1000000;
        private int RoW = 0;

        private int brojac1 = 0;
        private int brojac_po2 = 1;
        private double brojac_iznos1 = 1000000;
        private int RoW1 = 0;

        private void ProvjeraPromocije(string sifra)
        {
            if (DTpromocije.Rows.Count > 0)
            {
                bool a_true = false;
                bool a1_true = false;
                bool a2_true = false;
                //bool a3_true = false;
                for (int i = 0; i < DTpromocije.Rows.Count; i++)
                {
                    int row1 = 0;
                    int row2 = 0;
                    int row3 = 0;
                    int row_za_popust = 0;

                    if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2=3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();

                        for (int a = 0; a < dgv.Rows.Count - 1; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art2.Trim())
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art3.Trim())
                                {
                                    a2_true = true;
                                    row_za_popust = a;
                                }

                                if (a_true == true && a1_true == true && a2_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                                {
                                    dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                                    dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;
                                    dgv.Rows[row_za_popust].Cells["kolicina"].ReadOnly = true;

                                    dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row_za_popust].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row_za_popust].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                                    dgv.Rows[row_za_popust].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row_za_popust].Cells["popust"].Value) / 100)));
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    dgv.Rows[a].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[a].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                                    dgv.Rows[a].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row_za_popust].Cells["popust"].Value) / 100)));
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                            }
                        }
                        if (a_true == true && a1_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;

                            dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgv.Rows[row1].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row1].Cells["popust"].Value) / 100)));
                            dgv.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgv.Rows[row2].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row2].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2&3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                            {
                                a_true = true;
                                row1 = a;
                            }
                            else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                            {
                                a1_true = true;
                                row2 = a;
                            }
                            else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art3)
                            {
                                a2_true = true;
                                row3 = a;
                            }
                        }
                        if (a_true == true && a1_true == true && a2_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row3].Cells["kolicina"].ReadOnly = true;

                            dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row3].Cells["zakljucaj"].Value = 1;

                            dgv.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgv.Rows[row1].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row1].Cells["popust"].Value) / 100)));
                            dgv.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgv.Rows[row2].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row2].Cells["popust"].Value) / 100)));
                            dgv.Rows[row3].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[row3].Cells["iznos"].Value = String.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row3].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row3].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row3].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a+a=a")
                    {
                        brojac++;
                        if (brojac == 3)
                        {
                            for (int a = (brojac_po3 - 3); a < brojac_po3; a++)
                            {
                                if (Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos)
                                {
                                    brojac_iznos = Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgv.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[RoW].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[RoW].Cells["iznos"].Value = Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[RoW].Cells["popust"].Value) / 100);
                            Ukupno();
                            brojac = 0;
                        }
                        brojac_po3++;
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a=a")
                    {
                        brojac1++;
                        if (brojac1 == 2)
                        {
                            for (int a = (brojac_po2 - 2); a < brojac_po2; a++)
                            {
                                if (Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos1)
                                {
                                    brojac_iznos1 = Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW1 = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgv.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[RoW1].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[RoW1].Cells["iznos"].Value = Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[RoW].Cells["popust"].Value) / 100);
                            Ukupno();
                            brojac1 = 0;
                        }
                        brojac_po2++;
                    }
                }
            }
        }

        private void CheckPosEquipment(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str, "Greška");
            }
        }

        private void frmKasa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
            {
                //PosPrint.classPosPrint.DisconnectFromPrinter();
            }
            if (DTpostavkePrinter.Rows[0]["lineDisplay_bool"].ToString() == "1")
            {
                // PosPrint.classLineDisplay.WriteOnDisplay("");
                // PosPrint.classLineDisplay.CloseDisplay();
            }
            if (DTpostavkePrinter.Rows[0]["drawer_bool"].ToString() == "1")
            {
                //PosPrint.classCashDrawer.ClosePortCashDrawer();
            }
        }

        private void btnAlati_Click(object sender, EventArgs e)
        {
            frmKasaOpcije ko = new frmKasaOpcije();
            ko.ShowDialog();
            txtUnos.Select();
        }

        private void btnObrisiStavku_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            int current = dgv.CurrentRow.Index;
            //int ukupnoRows= dgv.Rows.Count;
            //dgv.Rows[ukupnoRows-1].Cells[0].Selected = true;
            Ukupno();
            dgv.Rows.RemoveAt(current);
            txtUnos.Select();
        }

        //private void txtUnos_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode==Keys.Enter)
        //    {
        //        DataGridViewRow row = this.dgv.RowTemplate;
        //        row.DefaultCellStyle.BackColor = Color.Bisque;
        //        row.Height = 40;

        //        if (txtUnos.Text.Length == 2)
        //        {
        //            SetKolicina(Convert.ToDouble(txtUnos.Text));
        //        }

        //        SetRoba(txtUnos.Text);
        //        txtUnos.Text = "";
        //        return;
        //    }

        //    if (dgv.RowCount > 0)
        //    {
        //        if (e.KeyCode == Keys.Up)
        //        {
        //            int cr = dgv.SelectedRows.Count-1;
        //            if(cr>=0)
        //            {
        //                dgv.Rows[cr].Selected = true;
        //            }
        //            txtUnos.Select();
        //        }

        //        if (e.KeyCode == Keys.Down)
        //        {
        //            int cr = dgv.SelectedRows.Count+1;
        //            if (cr <= dgv.Rows.Count)
        //            {
        //                dgv.Rows[cr].Selected = true;
        //            }
        //            txtUnos.Select();
        //        }
        //    }
        //}

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void btnKolicina_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            //if (isNumeric(txtUnos.Text, System.Globalization.NumberStyles.AllowDecimalPoint) == false) { MessageBox.Show("Greška kod upisa količine.", "Greška"); return; }

            decimal dec_parse;
            if (Decimal.TryParse(txtUnos.Text, out dec_parse))
            {
                txtUnos.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa količine.", "Greška");
                return;
            }

            dgv.CurrentRow.Cells["kolicina"].Value = txtUnos.Text;
            SetKolicina(Convert.ToDouble(txtUnos.Text));
            txtUnos.Text = "";
            txtUnos.Select();
        }

        private void btnRabat_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            if (isNumeric(txtUnos.Text, System.Globalization.NumberStyles.AllowDecimalPoint) == false) { MessageBox.Show("Greška kod upisa rabata.", "Greška"); return; }

            dgv.CurrentRow.Cells["popust"].Value = txtUnos.Text;
            SetRabat(Convert.ToDouble(txtUnos.Text));
            txtUnos.Text = "";
            txtUnos.Select();
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CBskladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                dgv.CurrentRow.Cells["skladiste"].Value = CBskladiste.SelectedValue;
                SetSkladiste(CBskladiste.SelectedValue.ToString());
            }
        }

        private void CBskladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnos.Select();
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            //if (dgv.RowCount > 0)
            //{
            //    if (e.KeyCode == Keys.Up)
            //    {
            //        int cr = dgv.CurrentRow.Index-1;
            //        dgv.Rows[cr].Selected = true;
            //    }

            //    if (e.KeyCode == Keys.Down)
            //    {
            //        int cr = dgv.CurrentRow.Index+1;
            //        dgv.Rows[cr].Selected = true;
            //    }
            //}
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CBskladiste.SelectedValue = dgv.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
        }

        private DataTable DTean = new DataTable();

        private void btnOdjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataGridViewRow row = this.dgv.RowTemplate;
                row.DefaultCellStyle.BackColor = Color.Bisque;
                row.Height = 40;

                if (txtUnos.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtUnos.Text = Properties.Settings.Default.id_roba;
                        txtUnos.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                string sifra = "";

                DTean = classSQL.select("SELECT sifra FROM roba WHERE ean='" + txtUnos.Text + "'", "roba").Tables[0];
                if (DTean.Rows.Count == 0)
                {
                    sifra = txtUnos.Text;
                }
                else
                {
                    sifra = DTean.Rows[0][0].ToString();
                }

                SetRoba(sifra);
                //txtUnos.Text = "";
                return;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnKolicina_Click(btnKolicina, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnRabat_Click(btnRabat, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnTrazi_Click(btnTrazi, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnPartner_Click(btnPartner, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnGotovina_Click(btnGotovina, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnKartica_Click(btnKartica, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                button15_Click(btnOdustaniSve, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnObrisiStavku_Click(btnObrisiStavku, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnAlati_Click(btnAlati, e);
            }
            else if (e.KeyCode == Keys.F11)
            {
            }
            else if (e.KeyCode == Keys.F12)
            {
            }
            else if (e.KeyCode == Keys.Up)
            {
            }
            else if (e.KeyCode == Keys.Down)
            {
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            txtUnos.Select();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba = new frmRobaTrazi();
            roba.ShowDialog();

            if (Properties.Settings.Default.id_roba != "")
            {
                txtUnos.Text = Properties.Settings.Default.id_roba;
                txtUnos.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                Kasa.frmPromjenaCijene pr = new Kasa.frmPromjenaCijene();
                pr.idSkladiste = CBskladiste.SelectedValue.ToString();
                pr.sifra = dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString();
                pr.porez = dgv.CurrentRow.Cells["porez"].FormattedValue.ToString();
                pr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nemate niti jednu stavku.");
            }

            //decimal dec_parse;
            //if (Decimal.TryParse(txtUnos.Text, out dec_parse))
            //{
            //    txtUnos.Text = dec_parse.ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            //}

            //// ovo je izračun ako odredite MPC automatski računa popust
            //double VPC = Convert.ToDouble(dgv.CurrentRow.Cells["vpc"].FormattedValue.ToString());
            //double StariMPC = (VPC * Convert.ToDouble(dgv.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100) + VPC;

            //dgv.CurrentRow.Cells["popust"].Value = Convert.ToString(((Convert.ToDouble(txtUnos.Text) / StariMPC - 1) * 100) * (-1));
            //if (dgv.CurrentRow.Cells["popust"].FormattedValue.ToString().Length > 10)
            //{
            //    dgv.CurrentRow.Cells["popust"].Value = dgv.CurrentRow.Cells["popust"].FormattedValue.ToString().Remove(10);
            //}

            //SetRabat(Convert.ToDouble(dgv.CurrentRow.Cells["popust"].FormattedValue.ToString()));
            //txtUnos.Text = "";
            //txtUnos.Select();
        }

        private void CBskladiste_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                SetSkladiste(CBskladiste.SelectedValue.ToString());
                GetKolicinaSkaldiste(dg(dgv.CurrentRow.Index, "sifra"), CBskladiste.SelectedValue.ToString(), dg(dgv.CurrentRow.Index, "jmj"));
            }
        }
    }
}