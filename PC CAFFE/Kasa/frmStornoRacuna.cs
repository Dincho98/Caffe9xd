using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmStornoRacuna : Form
    {
        public frmStornoRacuna()
        {
            InitializeComponent();
        }

        private DataTable DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DTsettings = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private Until.FunkcijeRobno RobnoFunkcije = new Until.FunkcijeRobno();

        private void frmStornoRacuna_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            //SetCB();
            this.Paint += new PaintEventHandler(Form1_Paint);
            SubscribeClickEvent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE racuni.id_ducan='" + Util.Korisno.idDucan + "' AND racuni.id_kasa='" + Util.Korisno.idKasa + "'", "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// Used to subscribe event on buttons when the form loads
        /// </summary>
        private void SubscribeClickEvent()
        {
            btnOne.Click += ButtonClick;
            btnTwo.Click += ButtonClick;
            btnThree.Click += ButtonClick;
            btnFour.Click += ButtonClick;
            btnFive.Click += ButtonClick;
            btnSix.Click += ButtonClick;
            btnSeven.Click += ButtonClick;
            btnEight.Click += ButtonClick;
            btnNine.Click += ButtonClick;
            btnZero.Click += ButtonClick;
            btnComma.Click += ButtonClick;
            btnDel.Click += ButtonClick;
        }

        /// <summary>
        /// This method is called when the button is clicked
        /// </summary>
        /// <param name="sender">Reference to button</param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Text != "DEL")
                    textBox1.AppendText(button.Text);
                else if (textBox1.Text.Length > 0)
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void SetCB()
        {
            //DataTable DT_Skladiste = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
            //cbDucan.DataSource = DT_Skladiste;
            //cbDucan.DisplayMember = "ime_ducana";
            //cbDucan.ValueMember = "id_ducan";

            //DataTable DT_blagajna = classSQL.select("SELECT * FROM blagajna", "blagajna").Tables[0];
            //cbDucan.DataSource = DT_blagajna;
            //cbDucan.DisplayMember = "ime_blagajne";
            //cbDucan.ValueMember = "id_blagajna";
        }

        private DataTable DTsend;

        private void button1_Click(object sender, EventArgs e)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("godina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("stol");
            DTsend.Columns.Add("jelo");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("storno");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_blagajna");
            DTsend.Columns.Add("dod");
            DTsend.Columns.Add("pol");

            DataRow row;

            string brRac = brojRacuna();
            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            DataTable DTr = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + textBox1.Text + "'  " +
                " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                " AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                " AND godina='" + DateTime.Now.Year.ToString() + "'", "racuni").Tables[0];

            if (DTr.Rows.Count != 0)
            {
                if (DTr.Rows[0]["storno"].ToString() == "DA")
                {
                    MessageBox.Show("Ovaj je račun je već stonirani.");
                    return;
                }

                decimal dec;
                decimal Dukupno_virman = 0;
                if (!decimal.TryParse(DTr.Rows[0]["ukupno_virman"].ToString(), out dec))
                {
                    Dukupno_virman = 0;
                }
                else
                {
                    Dukupno_virman = Convert.ToDecimal(DTr.Rows[0]["ukupno_virman"].ToString());
                }

                string sqlR = "INSERT INTO racuni (" +
                        " broj_racuna," +
                        " datum_racuna," +
                        " id_ducan," +
                        " id_kasa," +
                        " id_kupac," +
                        " id_blagajnik," +
                        " ukupno_gotovina," +
                        " ukupno_kartice," +
                        " storno," +
                        " ukupno," +
                        " dobiveno_gotovina," +
                        " id_stol," +
                        " nacin_placanja," +
                        " ukupno_virman," +
                        " godina" +
                        ") VALUES (" +
                        "'" + brRac + "'," +
                         "'" + dt + "'," +
                        "'" + DTr.Rows[0]["id_ducan"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_kasa"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_kupac"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_blagajnik"].ToString() + "'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno_gotovina"].ToString()) * (-1)).ToString() + "'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno_kartice"].ToString()) * (-1)).ToString() + "'," +
                        "'NE'," +
                        "'" + (Convert.ToDouble(DTr.Rows[0]["ukupno"].ToString()) * (-1)).ToString() + "'," +
                        "'" + DTr.Rows[0]["dobiveno_gotovina"].ToString() + "'," +
                        "'" + DTr.Rows[0]["id_stol"].ToString() + "'," +
                        "'" + DTr.Rows[0]["nacin_placanja"].ToString() + "'," +
                        "'" + (Dukupno_virman * (-1)).ToString() + "'," +
                        "'" + DTr.Rows[0]["godina"].ToString() + "'" +
                        ")";

                provjera_sql(classSQL.insert(sqlR));

                string sqlA = "SELECT * FROM racun_stavke WHERE broj_racuna='" + textBox1.Text + "'" +
                    " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                    " AND id_blagajna='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "'";
                DataTable DTA = classSQL.select(sqlA, "racun_stavke").Tables[0];

                string sifra = "";
                for (int i = 0; i < DTA.Rows.Count; i++)
                {
                    sifra = DTA.Rows[i]["sifra_robe"].ToString();
                    row = DTsend.NewRow();
                    row["broj_racuna"] = brRac;
                    row["sifra_robe"] = sifra;
                    row["mpc"] = DTA.Rows[i]["mpc"].ToString();
                    row["godina"] = DTA.Rows[i]["godina"].ToString();
                    row["id_skladiste"] = DTA.Rows[i]["id_skladiste"].ToString();
                    row["porez"] = DTA.Rows[i]["porez"].ToString();
                    row["kolicina"] = (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString();
                    row["rabat"] = DTA.Rows[i]["rabat"].ToString();
                    row["vpc"] = DTA.Rows[i]["vpc"].ToString();
                    row["cijena"] = DTA.Rows[i]["mpc"].ToString();
                    row["porez_potrosnja"] = DTA.Rows[i]["porez_potrosnja"].ToString();
                    row["ime"] = classSQL.select("SELECT naziv FROM roba WHERE sifra='" + sifra + "'", "roba").Tables[0].Rows[0][0].ToString();
                    row["stol"] = DTr.Rows[0]["id_stol"].ToString();
                    row["nbc"] = DTA.Rows[0]["nbc"].ToString();
                    row["dod"] = 0;
                    row["pol"] = 0;

                    DTsend.Rows.Add(row);

                    sqlA = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,odjava,nbc,porez_potrosnja,id_ducan,godina,id_blagajna) VALUES (" +
                        "'" + brRac + "'," +
                        "'" + DTA.Rows[i]["sifra_robe"].ToString() + "'," +
                        "'" + DTA.Rows[i]["id_skladiste"].ToString() + "'," +
                        "'" + DTA.Rows[i]["mpc"].ToString() + "'," +
                        "'" + DTA.Rows[i]["porez"].ToString() + "'," +
                        "'" + (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString() + "'," +
                        "'" + DTA.Rows[i]["rabat"].ToString() + "'," +
                        "'" + DTA.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                        "'" + DTA.Rows[i]["odjava"].ToString() + "'," +
                        "'" + DTA.Rows[i]["nbc"].ToString().Replace(",", ".") + "'," +
                        "'" + DTA.Rows[i]["porez_potrosnja"].ToString() + "'," +
                        "'" + DTA.Rows[i]["id_ducan"].ToString() + "'," +
                        "'" + DTA.Rows[i]["godina"].ToString() + "'," +
                        "'" + DTA.Rows[i]["id_blagajna"].ToString() + "'" +
                        ")";
                    provjera_sql(classSQL.insert(sqlA));

                    SQL.ClassSkladiste.GetAmountCaffe(DTA.Rows[i]["sifra_robe"].ToString(), DTA.Rows[i]["id_skladiste"].ToString(), (Convert.ToDouble(DTA.Rows[i]["kolicina"].ToString()) * (-1)).ToString(), "-");
                }

                if (DTr.Rows[0]["nacin_placanja"].ToString() == "G")
                    Util.Korisno.dodajIznosUBlagajnickiIzvjestaj(Convert.ToInt32(brRac), dRac, (Convert.ToDecimal(DTr.Rows[0]["ukupno"].ToString()) * (-1)));

                //OVA FUNKCIJA POSTAVLJA SKLADIŠTE TAKO DA UZME U OBZIR SVE DOKUMENTE I ZAKLJUČI STANJE NA SKLADIŠTU

                if (DTsettings.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() == "1")
                {
                    RobnoFunkcije.PostaviStanjeSkladista();
                }

                if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
                {
                    try
                    {
                        try
                        {
                            PosPrint.classPosPrintCaffe.PrintReceipt(DTsend, DTr.Rows[0]["id_blagajnik"].ToString(), brRac + "/" + DTr.Rows[0]["id_ducan"].ToString() + "/" + DTr.Rows[0]["id_kasa"].ToString(), DTr.Rows[0]["id_kupac"].ToString(), "", brRac, DTr.Rows[0]["nacin_placanja"].ToString(), "DA", 0);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\nŽelite li ispisati ovaj dokumenat na A4 format?\r\nOvo je orginalna greška:\r\n" + ex, "Printer", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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

                classSQL.update("UPDATE racuni SET storno='DA' WHERE broj_racuna='" + textBox1.Text + "'" +
                    " AND id_ducan='" + DTsettings.Rows[0]["default_ducan"].ToString() + "'" +
                    " AND id_kasa='" + DTsettings.Rows[0]["default_blagajna"].ToString() + "'" +
                    " AND godina='" + DateTime.Now.Year.ToString() + "'");

                MessageBox.Show("Izvršeno");
                this.Close();
            }
            else
            {
                MessageBox.Show("Krivi broj računa.", "Greška");
                return;
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBoxPartner_Click(object sender, EventArgs e)
        {
            frmSviRacuni form = new frmSviRacuni
            {
                storno = true
            };
            form.ShowDialog();
            if (form.BrojRacuna != null)
                textBox1.Text = form.BrojRacuna;
        }
    }
}