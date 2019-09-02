using System;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace PCPOS
{
    public partial class frmPosPrinter : Form
    {
        public frmPosPrinter()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private DataTable DTPosPrint;

        private void frmPosPrinter_Load(object sender, EventArgs e)
        {
            FillData();
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void FillData()
        {
            DTPosPrint = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

            //Printer
            txtLogicalPrint.Text = DTPosPrint.Rows[0]["logical_name"].ToString();
            txtDevicePrint.Text = DTPosPrint.Rows[0]["device_category"].ToString();
            txtArtikl.Text = DTPosPrint.Rows[0]["ispred_artikl"].ToString();
            txtKolicina.Text = DTPosPrint.Rows[0]["ispred_kolicine"].ToString();
            txtCijena.Text = DTPosPrint.Rows[0]["ispred_cijene"].ToString();
            txtUkupno.Text = DTPosPrint.Rows[0]["ispred_ukupno"].ToString();
            rtfBottom.Text = DTPosPrint.Rows[0]["bottom_text"].ToString();
            txtPopust.Text = DTPosPrint.Rows[0]["ispred_popust"].ToString();
            txtLinijeBottom.Text = DTPosPrint.Rows[0]["linija_praznih_bottom"].ToString();

            if (DTPosPrint.Columns["prviPrint"] != null)
                textBoxRacun1.Text = DTPosPrint.Rows[0]["prviPrint"].ToString();

            if (DTPosPrint.Columns["drugiPrint"] != null)
                textBoxRacun2.Text = DTPosPrint.Rows[0]["drugiPrint"].ToString();

            if (DTPosPrint.Columns["treciPrint"] != null)
                textBoxRacun3.Text = DTPosPrint.Rows[0]["treciPrint"].ToString();


            numGotovina.Text = DTPosPrint.Rows[0]["ispisGotovina"].ToString();
            //numGotovina.Value = Convert.ToInt32(DTPosPrint.Rows[0]["ispisGotovina"].ToString());

            numKartica.Text = DTPosPrint.Rows[0]["ispisKartica"].ToString();
            // numKartica.Value = Convert.ToInt32(DTPosPrint.Rows[0]["ispisKartica"].ToString());

            numVirman.Text = DTPosPrint.Rows[0]["ispisVirman"].ToString();
            //numVirman.Value = Convert.ToInt32(DTPosPrint.Rows[0]["ispisVirman"].ToString());

            numOstalo.Text = DTPosPrint.Rows[0]["ispisOstalo"].ToString();
            //numOstalo.Value = Convert.ToInt32(DTPosPrint.Rows[0]["ispisOstalo"].ToString());

            numOtpremnica.Text = DTPosPrint.Rows[0]["ispisOtpremnica"].ToString();
            //numOstalo.Value = Convert.ToInt32(DTPosPrint.Rows[0]["ispisOstalo"].ToString());

            //txtPopust.Text = DTPosPrint.Rows[0]["ispred_popust"].ToString();
            //txtPopust.Text = DTPosPrint.Rows[0]["ispred_popust"].ToString();
            //txtPopust.Text = DTPosPrint.Rows[0]["ispred_popust"].ToString();

            //Display
            txtDisplayDevice.Text = DTPosPrint.Rows[0]["lineDisplay_device_category"].ToString();
            txtDisplayLogical.Text = DTPosPrint.Rows[0]["lineDisplay_logicalName"].ToString();

            //Ladica
            txtLogicalLadica.Text = DTPosPrint.Rows[0]["logical_name_drawer"].ToString();
            txtDeviceLadica.Text = DTPosPrint.Rows[0]["device_category_drawer"].ToString();
            txtBrojSlova.Text = DTPosPrint.Rows[0]["broj_slova_na_racunu"].ToString();

            try
            {
                DataTable DTgrupe = classSQL.select("SELECT id_grupa,grupa,printer3 FROM grupa WHERE aktivnost='1' ORDER BY grupa;", "Grupe").Tables[0];
                foreach (DataRow r in DTgrupe.Rows)
                {
                    bool b = false;
                    if (r["printer3"].ToString() == "1")
                        b = true;

                    dgvGrupe.Rows.Add(r["id_grupa"].ToString(), r["grupa"].ToString(), b);
                }

                if (DTPosPrint.Rows[0]["port_display_enable"].ToString() == "1") { chbPrekoPorta.Checked = true; } else { chbPrekoPorta.Checked = false; }
                txtPrekoPorta.Text = DTPosPrint.Rows[0]["port_display"].ToString();
            }
            catch { }

            if (DTPosPrint.Rows[0]["posPrinterBool"].ToString() == "1") { chbPrinter.Checked = true; } else { chbPrinter.Checked = false; }
            if (DTPosPrint.Rows[0]["adresa_narudzbe_racun_kraj"].ToString() == "1") { chbAdresaRacunKraj.Checked = true; } else { chbAdresaRacunKraj.Checked = false; }

            if (DTPosPrint.Rows[0]["drawer_bool"].ToString() == "1") { chbLadica.Checked = true; } else { chbLadica.Checked = false; }
            if (DTPosPrint.Rows[0]["lineDisplay_bool"].ToString() == "1") { chbDisplayAktivnost.Checked = true; } else { chbDisplayAktivnost.Checked = false; }

            if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() == "1") { chbCijelaStavka.Checked = true; } else { chbCijelaStavka.Checked = false; }

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1") { chbStariPrinter.Checked = true; } else { chbStariPrinter.Checked = false; }

            comboBox1.Items.Add("Nije instaliran");
            cbPrinterSank.Items.Add("Nije instaliran");
            cbPrinter2.Items.Add("Nije instaliran");
            cbPrinter3.Items.Add("Nije instaliran");

            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer.ToString());
                cbPrinterSank.Items.Add(printer.ToString());
                cbPrinter2.Items.Add(printer.ToString());
                cbPrinter3.Items.Add(printer.ToString());
            }

            comboBox1.Text = DTPosPrint.Rows[0]["windows_printer_name"].ToString();
            cbPrinterSank.Text = DTPosPrint.Rows[0]["windows_printer_sank"].ToString();
            cbPrinter2.Text = DTPosPrint.Rows[0]["windows_printer_name2"].ToString();
            cbPrinter3.Text = DTPosPrint.Rows[0]["windows_printer_name3"].ToString();

            txtBrojFonta.Text = DTpostavke.Rows[0]["font_print_size"].ToString();

            DataTable DTSK = new DataTable("opcija_ladica");
            DTSK.Columns.Add("id_opcija_ladica", typeof(string));
            DTSK.Columns.Add("opcija_ladica", typeof(string));
            DTSK.Rows.Add(0, "Nije podržano otvaranje ladice.");
            DTSK.Rows.Add(1, "Opcija 1");
            DTSK.Rows.Add(2, "Opcija 2");

            cbLadicaStariPrinter.DataSource = DTSK;
            cbLadicaStariPrinter.DisplayMember = "opcija_ladica";
            cbLadicaStariPrinter.ValueMember = "id_opcija_ladica";
            cbLadicaStariPrinter.SelectedValue = DTpostavke.Rows[0]["ladicaOn"].ToString();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrupe.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string aa = "0";
                    if (dgvGrupe.Rows[e.RowIndex].Cells["aktivno"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = "UPDATE grupa SET printer3='" + aa + "' " +
                        " WHERE id_grupa='" + dgvGrupe.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "';";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            string adresanakrajuracuna = "0";
            string ladicabool = "0";
            string printerBarCodeBool = "0";
            string displayBool_ = "0";
            if (chbAdresaRacunKraj.Checked)
            {
                adresanakrajuracuna = "1";
            }
            if (chbLadica.Checked)
            {
                ladicabool = "1";
            }
            else
            {
                ladicabool = "0";
            }

            if (chbPrinter.Checked)
            {
                printerBarCodeBool = "1";
            }
            else
            {
                printerBarCodeBool = "0";
            }

            if (chbDisplayAktivnost.Checked)
            {
                displayBool_ = "1";
            }
            else
            {
                displayBool_ = "0";
            }

            string stariprinter = "0";
            if (chbStariPrinter.Checked)
            {
                stariprinter = "1";
            }

            classSQL.Setings_Update("UPDATE postavke SET direct_print='" + stariprinter + "'");

            string sql = "UPDATE pos_print SET " +
                " logical_name='" + txtLogicalPrint.Text + "'," +
                " device_category='" + txtDevicePrint.Text + "'," +
                " ispred_kolicine='" + txtKolicina.Text + "'," +
                " ispred_cijene='" + txtCijena.Text + "'," +
                " ispred_popust='" + txtPopust.Text + "'," +
                " ispred_ukupno='" + txtUkupno.Text + "'," +
                " linija_praznih_bottom='" + txtLinijeBottom.Text + "'," +
                " ispred_artikl='" + txtArtikl.Text + "'," +
                " bottom_text='" + rtfBottom.Text + "'," +
                " posPrinterBool='" + printerBarCodeBool + "'," +
                " logical_name_drawer='" + txtLogicalLadica.Text + "'," +
                " device_category_drawer='" + txtDeviceLadica.Text + "'," +
                " drawer_bool='" + ladicabool + "'," +
                " lineDisplay_logicalName='" + txtDisplayLogical.Text + "'," +
                " lineDisplay_device_category='" + txtDisplayDevice.Text + "'," +
                " windows_printer_name='" + comboBox1.Text + "'," +
                " windows_printer_sank='" + cbPrinterSank.Text + "'," +
                " windows_printer_name2='" + cbPrinter2.Text + "'," +
                " windows_printer_name3='" + cbPrinter3.Text + "'," +
                " broj_slova_na_racunu='" + txtBrojSlova.Text + "'," +
                " lineDisplay_bool='" + displayBool_ + "'," +
                " ispisGotovina = '" + numGotovina.Text.ToString() + "'," +
                " ispisKartica = '" + numKartica.Text.ToString() + "'," +
                " ispisVirman = '" + numVirman.Text.ToString() + "'," +
                " prviPrint = '" + textBoxRacun1.Text.ToString() + "'," +
                " drugiPrint = '" + textBoxRacun2.Text.ToString() + "'," +
                " treciPrint = '" + textBoxRacun3.Text.ToString() + "'," +
                " ispisOstalo = '" + numOstalo.Text.ToString() + "'," +
                " ispisOtpremnica = '" + numOtpremnica.Text.ToString() + "'," +
                " adresa_narudzbe_racun_kraj = '" + adresanakrajuracuna + "'" +
                " WHERE id='1'";
            classSQL.Setings_Update(sql);

            decimal dec;
            if (!Decimal.TryParse(txtBrojFonta.Text, out dec))
            {
                txtBrojFonta.Text = "10";
            }

            string cijela_stavka = "0";

            if (chbCijelaStavka.Checked)
            {
                cijela_stavka = "1";
            }

            sql = "UPDATE postavke SET ladicaOn='" + cbLadicaStariPrinter.SelectedValue + "',font_print_size='" + txtBrojFonta.Text + "',ispis_cijele_stavke='" + cijela_stavka + "'";
            classSQL.Setings_Update(sql);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("ime");
            DataRow row;

            row = DTsend.NewRow();
            row["mpc"] = "125";
            row["porez"] = "25";
            row["kolicina"] = "1";
            row["rabat"] = "0";
            row["cijena"] = "100";
            row["ime"] = "Artikl 1";
            row["vpc"] = "50";
            row["porez_potrosnja"] = "0";
            DTsend.Rows.Add(row);

            row = DTsend.NewRow();
            row["mpc"] = "1250";
            row["porez"] = "25";
            row["kolicina"] = "1";
            row["rabat"] = "0";
            row["cijena"] = "1000";
            row["ime"] = "Artikl 2";
            row["vpc"] = "100";
            row["porez_potrosnja"] = "0";
            DTsend.Rows.Add(row);

            DataTable DTpostavkePrinter = classSQL.select("SELECT * FROM pos_print", "pos_print").Tables[0];
            //CheckPosEquipment(PosPrint.classPosPrint.ConnectToPrinter());
            PosPrint.classPosPrintMaloprodaja.PrintReceipt(DTsend, "Probni blagajnik", "1000000", "", "12345678", "100000", "G");
            //PosPrint.classPosPrint.DisconnectFromPrinter();
        }

        private void CheckPosEquipment(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str, "Greška");
            }
        }

        private void btnDisplayPrekoPorta_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                spLineDisplay.PortName = txtPrekoPorta.Text;
                spLineDisplay.Open();
                string a = "000";
                //MessageBox.Show("111111");
                //spLineDisplay.Write(Convert.ToChar(a),0,a.Length);
                //spLineDisplay.WriteLine("11111111");
                /*List<int> array1 = new List<int>();
                array1.Add(1);
                array1.Add(666);
                array1.Add(int.MaxValue);
                
                //spLineDisplay.Write(data, 0, data.Length);
                //spLineDisplay.Write("");
                //spLineDisplay.WriteTimeout = 2000;
                byte[] byteArray = new byte[1];
                byteArray[0] = 0x7a;
                spLineDisplay.Write(byteArray,0,1);
                //spLineDisplay.WriteLine(a);
                spLineDisplay.Close();
                
                spLineDisplay.Open();
                byte[] mBuffer = new byte[1];
                mBuffer[0] = 0x74; //ASCII letter "t".
                spLineDisplay.Write(mBuffer, 0, mBuffer.Length);
                spLineDisplay.Close();
                */
                SerialPort _serialport = new SerialPort("COM2", 19200, Parity.None, 8);
                _serialport.Handshake = Handshake.None;
                _serialport.Open();
                _serialport.Write("4.20");
                _serialport.Close();
            }
            catch (Exception ex)
            {
                if (!(spLineDisplay.IsOpen))
                {
                    MessageBox.Show(ex.ToString());
                }
                else
                {
                    MessageBox.Show("0000");
                }
                MessageBox.Show(ex.ToString());
            }
        }

        private void chbPrekoPorta_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPrekoPorta.Checked)
            {
                string sql = "UPDATE pos_print SET port_display_enable='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE pos_print SET port_display_enable='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void txtPrekoPorta_TextChanged(object sender, EventArgs e)
        {
            string sql = "UPDATE pos_print SET port_display='" + txtPrekoPorta.Text + "'";
            classSQL.Setings_Update(sql);
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