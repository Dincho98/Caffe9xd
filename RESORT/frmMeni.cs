using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RESORT
{
    public partial class frmMeni : Form
    {
        public frmMeni()
        {
            InitializeComponent();
        }

        private DataTable DTBojeForme;
        private bool ucitano = false;
        private int sobaWidth = 0;

        private void frmMeni_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Year < 2017)
            {
                ukupnoPoNacinimaPlacanjaToolStripMenuItem.Enabled = false;
                ukupnoPoNacinimaPlacanjaToolStripMenuItem.Visible = false;
            }

            Class.PostavkeFiskalizacije.getPodaci();

            Paint += new PaintEventHandler(Funkcije.Form1_Paint);

            avansToolStripMenuItem.Visible = false;
            panelNatpisi.Hide();
            try
            {
                if (!File.Exists("log.txt"))
                {
                    File.WriteAllText("log.txt", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Properties.Settings.Default.verzija_programa = 1.1m;
            Properties.Settings.Default.Save();
            timerBackup.Start();
            timerBackup.Interval = 1800000;
            ClassProvjeraBaze.ProvjeraTablica();
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];
            DataTable DTremote = RemoteDB.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];
            SetCB();
            SetGosti(DateTime.Now.Month);
            ucitano = true;

            WeatherSet();

            Forme.frmKasaPrijava kasaPrijava = new Forme.frmKasaPrijava();
            kasaPrijava.ShowDialog();

            timerWeather.Start();
        }

        private int selectedCellCount = 0;
        private List<int> selectDGVrow = new List<int>();
        private List<int> selectDGVcell = new List<int>();

        private void selectedCellsButton_Click(object sender, EventArgs e)
        {
            selectDGVcell.Clear();
            selectDGVrow.Clear();
            selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                if (dgv.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        selectDGVrow.Add(dgv.SelectedCells[i].RowIndex);
                        selectDGVcell.Add(dgv.SelectedCells[i].ColumnIndex);

                        if (selectedCellCount > 1)
                        {
                            string od_datuma = selectDGVcell[selectDGVcell.Count - 1].ToString() + "." + cbMjesec.SelectedValue.ToString() + "." + nuGodina.Value.ToString();
                            string do_datuma = (selectDGVcell[0] == 0 ? 1 : selectDGVcell[0]).ToString() + "." + cbMjesec.SelectedValue.ToString() + "." + nuGodina.Value.ToString();
                            if (selectDGVcell[selectDGVcell.Count - 1] > selectDGVcell[0])
                            {
                                od_datuma = (selectDGVcell[0] == 0 ? 1 : selectDGVcell[0]).ToString() + "." + cbMjesec.SelectedValue.ToString() + "." + nuGodina.Value.ToString();
                                do_datuma = selectDGVcell[selectDGVcell.Count - 1].ToString() + "." + cbMjesec.SelectedValue.ToString() + "." + nuGodina.Value.ToString();
                            }

                            string soba = dgv.Rows[dgv.CurrentRow.Index].Cells[0].FormattedValue.ToString();
                            CreateLabelDani(Cursor.Position.X, Cursor.Position.Y);

                            lblSoba.Text = "Soba:" + soba;
                            lblOdDatuma.Text = "Od datuma: " + od_datuma;
                            lblDoDatuma.Text = "Do datuma: " + do_datuma;
                        }
                    }
                }
            }
        }

        private void CreateLabelDani(int x, int y)
        {
            panelNatpisi.Show();
            panelNatpisi.Location = new Point(x, y);
        }

        private void dgv_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectDGVrow.Count > 1)
            {
                string od_datuma = (selectDGVcell[0] == 0 ? 1 : selectDGVcell[0]).ToString() + "/" + cbMjesec.SelectedValue.ToString() + "/" + nuGodina.Value.ToString();
                string do_datuma = selectDGVcell[selectDGVcell.Count - 1].ToString() + "/" + cbMjesec.SelectedValue.ToString() + "/" + nuGodina.Value.ToString();
                if (selectDGVcell[selectDGVcell.Count - 1] < selectDGVcell[0])
                {
                    do_datuma = (selectDGVcell[0] == 0 ? 1 : selectDGVcell[0]).ToString() + "/" + cbMjesec.SelectedValue.ToString() + "/" + nuGodina.Value.ToString();
                    od_datuma = selectDGVcell[selectDGVcell.Count - 1].ToString() + "/" + cbMjesec.SelectedValue.ToString() + "/" + nuGodina.Value.ToString();
                }

                string soba = dgv.Rows[dgv.CurrentRow.Index].Cells[0].FormattedValue.ToString();

                frmOdabir odabir = new frmOdabir();
                odabir.OD_datuma = od_datuma;
                odabir.DO_datuma = do_datuma;
                odabir.soba = soba;
                odabir.__godina = nuGodina.Value.ToString();
                odabir.ShowDialog();
            }
            panelNatpisi.Hide();
        }

        private void PostavaCijene()
        {
            DataTable DTsobe = RemoteDB.select("SELECT * FROM sobe WHERE id_tip_sobe='7' AND id<>'5'", "r_cijenasoba").Tables[0];
            for (int s = 0; s < DTsobe.Rows.Count; s++)
            {
                DataTable DT = RemoteDB.select("SELECT * FROM r_cijenasoba WHERE id_soba='5'", "r_cijenasoba").Tables[0];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string sss = "INSERT INTO R_CijenaSoba (broj_sobe,id_soba,id_valuta,cijena_nocenja,od_datuma,do_datuma) VALUES (" +
                        "'" + DTsobe.Rows[s]["broj_sobe"].ToString() + "'," +
                        "'" + DTsobe.Rows[s]["id"].ToString() + "'," +
                        "'" + DT.Rows[i]["id_valuta"].ToString() + "'," +
                        "'" + DT.Rows[i]["cijena_nocenja"].ToString().Replace(",", ".") + "'," +
                        "'" + DT.Rows[i]["od_datuma"].ToString() + "'," +
                        "'" + DT.Rows[i]["do_datuma"].ToString() + "'" +
                        ")";
                    RemoteDB.insert(sss);
                }
            }
        }

        private void timerWeather_Tick(object sender, EventArgs e)
        {
            WeatherSet();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = ((PictureBox)sender);
            int w = pictureBox.Size.Width;
            int h = pictureBox.Size.Height;
            pictureBox.Size = new Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = ((PictureBox)sender);
            int w = pictureBox.Size.Width;
            int h = pictureBox.Size.Height;
            pictureBox.Size = new Size(w - 7, h - 7);
        }

        public XmlDocument GetDataFromUrl(string url)
        {
            XmlDocument urlData = new XmlDocument();
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url);

            rq.Timeout = 2000;
            HttpWebResponse response;
            try
            {
                response = null;
            }
            catch (Exception)
            {
                response = null;
                return null;
            }
            if (response != null)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    XmlTextReader reader = new XmlTextReader(responseStream);
                    urlData.Load(reader);
                }
            }
            return urlData;
        }

        private void WeatherSet()
        {
            try
            {
                XmlDocument doc = GetDataFromUrl("http://vrijeme.hr/hrvatska_n.xml");

                if (doc != null)
                {
                    XmlDocument doc1 = doc;
                    XmlElement root = doc.DocumentElement;
                    if (root == null)
                        return;

                    XmlNodeList nodes = root.SelectNodes("/Hrvatska/Grad");

                    foreach (XmlNode node in nodes)
                    {
                        string sq = node["GradIme"].InnerText.ToString();
                        cdVrijeme.Items.Add(node["GradIme"].InnerText.ToString());
                    }

                    INIFile ini = new INIFile();

                    ini.Read("Postgre", "hostname");

                    ucitano = true;
                    cdVrijeme.Text = ini.Read("Postavke", "weather_city");
                    ;
                    string[] vrijeme = weather.SetRemoteFields(cdVrijeme.Text, doc);

                    if (vrijeme[5] != "")
                    {
                        label1.Text = vrijeme[5] + ", temperatura: " + vrijeme[0] + ", vlaga: " + vrijeme[1] + ", tlak: " + vrijeme[2] + ", Brzina vjetra: " + vrijeme[4];
                    }
                    else
                    {
                        label1.Text = "Trenutno nema dostupnih informacija.";
                    }
                }
                else
                {
                    label1.Text = "Trenutno nema dostupnih informacija.";
                }
            }
            catch (Exception)
            {
                label1.Text = "Trenutno nema dostupnih informacija.";
            }
        }

        private void cdVrijeme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                INIFile ini = new INIFile();
                XmlDocument doc = new XmlDocument();
                doc.Load("http://vrijeme.hr/hrvatska_n.xml");
                string[] vrijeme = weather.SetRemoteFields(cdVrijeme.Text, doc);
                ini.Write("Postavke", "weather_city", cdVrijeme.Text);
                if (vrijeme[5] != "")
                {
                    label1.Text = vrijeme[5] + ", temperatura: " + vrijeme[0] + ", vlaga: " + vrijeme[1] + ", tlak: " + vrijeme[2] + ", Brzina vjetra: " + vrijeme[4];
                }
                else
                {
                    label1.Text = "Trenutno nema dostupnih informacija.";
                }
            }
        }

        private void SetCB()
        {
            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("br", typeof(int));
            DTSK.Columns.Add("mjesec", typeof(string));

            DTSK.Rows.Add(1, "Siječanj");
            DTSK.Rows.Add(2, "Veljača");
            DTSK.Rows.Add(3, "Ožujak");
            DTSK.Rows.Add(4, "Travanj");
            DTSK.Rows.Add(5, "Svibanj");
            DTSK.Rows.Add(6, "Lipanj");
            DTSK.Rows.Add(7, "Srpanj");
            DTSK.Rows.Add(8, "Kolovoz");
            DTSK.Rows.Add(9, "Rujan");
            DTSK.Rows.Add(10, "Listopad");
            DTSK.Rows.Add(11, "Studeni");
            DTSK.Rows.Add(12, "Prosinac");

            cbMjesec.DataSource = DTSK;
            cbMjesec.DisplayMember = "mjesec";
            cbMjesec.ValueMember = "br";
            cbMjesec.SelectedValue = DateTime.Now.Month;

            nuGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodina.Value = DateTime.Now.Year;
        }

        private void SetGosti(int month, int width = 0)
        {
            var daysCount = DateTime.DaysInMonth(Convert.ToInt16(nuGodina.Value), month);

            //Dodaje prvu kolonu za ime sobe
            dgv.Columns.Add(new DataGridViewColumn() { HeaderText = "Naziv sobe", CellTemplate = new DataGridViewTextBoxCell() });
            dgv.Columns[0].Width = 100;
            if (width != 0)
            {
                dgv.Columns[0].Width = width;
            }

            //dodaje kolone za dane u mjesecu
            for (int i = 1; i <= daysCount; i++)
            {
                dgv.Columns.Add(new DataGridViewColumn() { HeaderText = i.ToString(), CellTemplate = new DataGridViewTextBoxCell() });
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            //dodaje redke za nazive soba
            DataTable DTs = RemoteDB.select("SELECT naziv_sobe,id FROM sobe WHERE aktivnost=1 ORDER BY id_tip_sobe ASC, naziv_sobe", "sobe").Tables[0];
            for (int i = 0; i < DTs.Rows.Count; i++)
            {
                dgv.Rows.Add(DTs.Rows[i]["naziv_sobe"].ToString());

                string sql = "SELECT id,napomena,broj,datum_dolaska,datum_odlaska,id_soba,ime_gosta,avans,dorucak,rucak,vecera FROM unos_rezervacije " +
                   "WHERE (odjava <> 1 OR odjava is null) AND id_soba='" + DTs.Rows[i]["id"].ToString() + "' " +
                   "AND ((date_part('MONTH', datum_dolaska) = '" + cbMjesec.SelectedValue + "' OR date_part('MONTH', datum_odlaska) = '" + cbMjesec.SelectedValue + "'))" +
                   "AND ((date_part('YEAR', datum_dolaska) = '" + nuGodina.Value + "' OR date_part('YEAR', datum_odlaska) = '" + nuGodina.Value + "'))";
                DataTable DTunos = RemoteDB.select(sql, "unos_gosta").Tables[0];

                foreach (DataRow row in DTunos.Rows)
                {
                    DateTime OD = Convert.ToDateTime(row["datum_dolaska"].ToString());
                    DateTime DO = Convert.ToDateTime(row["datum_odlaska"].ToString());

                    if (DO.Month > Convert.ToInt16(cbMjesec.SelectedValue))
                    {
                        DO = Convert.ToDateTime(nuGodina.Value.ToString() + "-" + cbMjesec.SelectedValue + "-" + DateTime.DaysInMonth(DateTime.Now.Year, month) + " 23:59:59");
                    }
                    if (OD.Month < Convert.ToInt16(cbMjesec.SelectedValue))
                    {
                        OD = Convert.ToDateTime(nuGodina.Value.ToString() + "-" + cbMjesec.SelectedValue + "-01" + " 00:00:01");
                    }

                    int broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

                    for (int d = 0; d < broj_nocenja + 1; d++)
                    {
                        if (daysCount < OD.Day + d)
                        {
                            OD = OD.AddDays(-1);
                        }

                        if (dgv.Rows[i].Cells[OD.Day + d].Style.BackColor == Color.SkyBlue || dgv.Rows[i].Cells[OD.Day + d].Style.BackColor == Color.Blue || dgv.Rows[i].Cells[OD.Day + d].Style.BackColor == Color.PowderBlue)
                        {
                            dgv.Rows[i].Cells[OD.Day + d].Style.BackColor = Color.Blue;
                            dgv.Rows[i].Cells[OD.Day + d].Style.ForeColor = Color.Blue;
                            dgv.Rows[i].Cells[OD.Day + d].ErrorText = "";
                        }
                        else
                        {
                            dgv.Rows[i].Cells[OD.Day + d].Style = new DataGridViewCellStyle { ForeColor = Color.SkyBlue, BackColor = Color.SkyBlue };
                        }

                        ///////////////////////////////////////////////////////////
                        string tool_tip = dgv.Rows[i].Cells[OD.Day + d].ToolTipText.ToString();
                        if (tool_tip != "")
                        {
                            string val = "RAZMJENA GOSTI:\r\n\r\n" + tool_tip + "\r\n\r\n____________________________________\r\n\r\n" + SetTitle(DTunos, row["broj"].ToString());
                            dgv.Rows[i].Cells[OD.Day + d].ToolTipText = val;
                        }
                        else
                        {
                            dgv.Rows[i].Cells[OD.Day + d].ToolTipText = SetTitle(DTunos, row["broj"].ToString());
                        }

                        if (dgv.Rows[i].Cells[OD.Day + d].FormattedValue.ToString() == "")
                        {
                            dgv.Rows[i].Cells[OD.Day + d].Value = "R" + row["broj"].ToString();
                        }
                        else
                        {
                            dgv.Rows[i].Cells[OD.Day + d].Value = dgv.Rows[i].Cells[OD.Day + d].FormattedValue.ToString() + "#R" + row["broj"].ToString();
                        }

                        if (broj_nocenja == d)
                        {
                            if (dgv.Rows[i].Cells[OD.Day + d].Style.BackColor != Color.Blue && (OD.Day + d) != daysCount)
                            {
                                dgv.Rows[i].Cells[OD.Day + d].ErrorText = "Ovaj dan soba je slobodna od 10 sati.";
                            }
                        }
                    }
                }

                sql = "SELECT napomena,broj,datum_dolaska,datum_odlaska,id_soba,ime_gosta,avans,dorucak,rucak,vecera FROM unos_gosta WHERE (odjava <> 1 OR odjava is null) AND id_soba='" + DTs.Rows[i]["id"].ToString() + "' AND (date_part('MONTH', datum_dolaska) = '" + cbMjesec.SelectedValue + "' OR date_part('MONTH', datum_odlaska) = '" + cbMjesec.SelectedValue + "')";

                DTunos = RemoteDB.select(sql, "unos_gosta").Tables[0];

                for (int l = 0; l < DTunos.Rows.Count; l++)
                {
                    DateTime datumDolaska = Convert.ToDateTime(DTunos.Rows[l]["datum_dolaska"].ToString());
                    DateTime datumOdlaska = Convert.ToDateTime(DTunos.Rows[l]["datum_odlaska"].ToString());

                    if (datumOdlaska.Month > Convert.ToInt16(cbMjesec.SelectedValue))
                    {
                        datumOdlaska = Convert.ToDateTime(nuGodina.Value.ToString() + "-" + cbMjesec.SelectedValue + "-" + DateTime.DaysInMonth(DateTime.Now.Year, month) + " 23:59:59");
                    }
                    if (datumDolaska.Month < Convert.ToInt16(cbMjesec.SelectedValue))
                    {
                        datumDolaska = Convert.ToDateTime(nuGodina.Value.ToString() + "-" + cbMjesec.SelectedValue + "-01" + " 00:00:01");
                    }

                    int broj_nocenja = Funkcije.ReturnDaysFromDate(datumDolaska, datumOdlaska);

                    for (int d = 0; d < broj_nocenja + 1; d++)
                    {
                        if (daysCount < datumDolaska.Day + d)
                        {
                            datumDolaska = datumDolaska.AddDays(-1);
                        }
                        //if (datumDolaska.Day + d > dgv.ColumnCount - 1)
                        //    continue;

                        bool postojanje = PostojanjeBroja("U" + DTunos.Rows[l]["broj"].ToString(), dgv.Rows[i].Cells[datumDolaska.Day + d].FormattedValue.ToString());

                        if (dgv.Rows[i].Cells[datumDolaska.Day + d].FormattedValue.ToString() != "" && dgv.Rows[i].Cells[datumDolaska.Day + d].FormattedValue.ToString() != "U" + DTunos.Rows[l]["broj"].ToString())
                        {
                            dgv.Rows[i].Cells[datumDolaska.Day + d].Style.BackColor = Color.DarkRed;
                            dgv.Rows[i].Cells[datumDolaska.Day + d].Style.ForeColor = Color.DarkRed;
                        }
                        else
                        {
                            dgv.Rows[i].Cells[datumDolaska.Day + d].Style.BackColor = Color.Red;
                            dgv.Rows[i].Cells[datumDolaska.Day + d].Style.ForeColor = Color.Red;
                        }

                        if (!postojanje)
                        {
                            string tool_tip = dgv.Rows[i].Cells[datumDolaska.Day + d].ToolTipText.ToString();
                            if (tool_tip != "")
                            {
                                string val = "RAZMJENA GOSTI:\r\n\r\n" + tool_tip + "\r\n\r\n____________________________________\r\n\r\n" + SetTitle(DTunos, DTunos.Rows[l]["broj"].ToString());
                                dgv.Rows[i].Cells[datumDolaska.Day + d].ToolTipText = val;
                            }
                            else
                            {
                                dgv.Rows[i].Cells[datumDolaska.Day + d].ToolTipText = SetTitle(DTunos, DTunos.Rows[l]["broj"].ToString());
                            }

                            if (dgv.Rows[i].Cells[datumDolaska.Day + d].FormattedValue.ToString() == "")
                            {
                                dgv.Rows[i].Cells[datumDolaska.Day + d].Value = "U" + DTunos.Rows[l]["broj"].ToString();
                            }
                            else
                            {
                                dgv.Rows[i].Cells[datumDolaska.Day + d].Value = dgv.Rows[i].Cells[datumDolaska.Day + d].FormattedValue.ToString() + "#U" + DTunos.Rows[l]["broj"].ToString();
                            }
                        }

                        if (broj_nocenja == d)
                        {
                            dgv.Rows[i].Cells[datumDolaska.Day + d].ErrorText = "Ovaj dan soba je slobodna od 10 sati.";
                        }
                    }
                }
            }
        }

        private int BrojCharova(char ch, string str)
        {
            int c = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch)
                {
                    c++;
                }
            }
            return c;
        }

        private string SetTitle(DataTable DT, string broj_unosa)
        {
            if (broj_unosa != "")
            {
                DataRow[] dr = DT.Select("broj='" + broj_unosa + "'");

                string vrijednost = "Broj unosa: " + broj_unosa + "\r\n";
                vrijednost += "Gosti: \r\n";

                for (int i = 0; i < dr.Length; i++)
                {
                    vrijednost += dr[i]["ime_gosta"].ToString() + "\r\n";
                }

                vrijednost += "\r\nDolazak: " + dr[0]["datum_dolaska"].ToString() + "\r\n";
                vrijednost += "Odlazak: " + dr[0]["datum_odlaska"].ToString() + "\r\n";
                vrijednost += "\r\nNapomena: " + dr[0]["napomena"].ToString();
                return vrijednost;
            }
            return "";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Color x = Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
                Color y = Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

                Graphics c = e.Graphics;
                Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
                c.FillRectangle(bG, 0, 0, Width, Height);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmUpisGosta upisGosta = new Forme.frmUpisGosta();
                upisGosta.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmSobe sobe = new Forme.sifarnici.frmSobe();
                sobe.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void agencijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmAgencije agencije = new Forme.sifarnici.frmAgencije();
                agencije.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tipSobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmTipSobe tipSobe = new Forme.sifarnici.frmTipSobe();
                tipSobe.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void vrstaUslugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmVrstaUsluge vrstaUsluge = new Forme.sifarnici.frmVrstaUsluge();
                vrstaUsluge.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void boravišnaPristojbaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmBoravisnaPristojba boravisnaPristojba = new Forme.sifarnici.frmBoravisnaPristojba();
                boravisnaPristojba.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void unosRezervacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmUnosRezervacije unosRezervacije = new Forme.frmUnosRezervacije();
                unosRezervacije.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void novaFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmFaktura faktura = new Forme.frmFaktura();
                faktura.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sveFaktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmSveFakture sveFakture = new Forme.frmSveFakture();
                sveFakture.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cbMjesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                if (dgv.Columns.Count > 0)
                {
                    sobaWidth = dgv.Columns[0].Width;
                }
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                SetGosti(Convert.ToInt16(cbMjesec.SelectedValue), sobaWidth);
            }
        }

        private void nuGodina_ValueChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                if (dgv.Columns.Count > 0)
                {
                    sobaWidth = dgv.Columns[0].Width;
                }
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                SetGosti(Convert.ToInt16(cbMjesec.SelectedValue), sobaWidth);
            }
        }

        private void btnNaprijed_Click(object sender, EventArgs e)
        {
            if (cbMjesec.SelectedValue.ToString() == "12")
            {
                return;
            }

            cbMjesec.SelectedValue = Convert.ToInt16(cbMjesec.SelectedValue) + 1;
        }

        private void btnNazad_Click(object sender, EventArgs e)
        {
            if (cbMjesec.SelectedValue.ToString() == "1")
            {
                return;
            }

            cbMjesec.SelectedValue = Convert.ToInt16(cbMjesec.SelectedValue) - 1;
        }

        private void frmMeni_Activated(object sender, EventArgs e)
        {
            if (ucitano)
            {
                Paint += new PaintEventHandler(Funkcije.Form1_Paint);
                if (dgv.Columns.Count > 0)
                {
                    sobaWidth = dgv.Columns[0].Width;
                }
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                SetGosti(Convert.ToInt16(cbMjesec.SelectedValue), sobaWidth);
            }
        }

        private void frmMeni_SizeChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                if (dgv.Columns.Count > 0)
                {
                    sobaWidth = dgv.Columns[0].Width;
                }
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                SetGosti(Convert.ToInt16(cbMjesec.SelectedValue), sobaWidth);
                Paint += new PaintEventHandler(Funkcije.Form1_Paint);
            }
        }

        private void unosDržaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmNovaDrzava novaDrzava = new Forme.sifarnici.frmNovaDrzava();
                novaDrzava.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            List<string> lista = CreateList(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());

            if (lista.Count <= 0)
            {
                return;
            }
            else if (lista.Count == 1)
            {
                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Red || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.DarkRed)
                {
                    Forme.frmIzbornik izbornik = new Forme.frmIzbornik();
                    izbornik.broj_unosa = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString().Remove(0, 1);
                    izbornik.godina = nuGodina.Value.ToString();
                    izbornik.ShowDialog();
                }

                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.SkyBlue || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Blue)
                {
                    Forme.frmIzbornikR i = new Forme.frmIzbornikR();
                    i.broj_unosa = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString().Remove(0, 1);
                    i.godina = nuGodina.Value.ToString();
                    i.ShowDialog();
                }
            }
            else if (lista.Count > 1)
            {
                Forme.frmOdaberiUnos odabirUnosa = new Forme.frmOdaberiUnos();
                odabirUnosa.lista = lista;
                try
                {
                    odabirUnosa.dan = dgv.Columns[dgv.CurrentCell.ColumnIndex].ToString();
                    odabirUnosa.mjesec = cbMjesec.SelectedValue.ToString();
                    odabirUnosa.godina = nuGodina.Value.ToString();
                }
                catch (Exception)
                {
                }
                odabirUnosa.ShowDialog();
            }
        }

        private List<string> CreateList(string str)
        {
            List<string> list_unos = new List<string>();
            string polje = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#')
                {
                    list_unos.Add(polje);
                    polje = "";
                }
                else
                {
                    polje += str[i];
                }

                if (i + 1 == str.Length)
                {
                    list_unos.Add(polje);
                }
            }
            return list_unos;
        }

        private bool PostojanjeBroja(string ch, string str)
        {
            string broj = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#')
                {
                    if (ch == broj)
                    {
                        return true;
                    }
                    broj = "";
                }
                else
                {
                    broj += str[i];
                    if (ch == broj)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void postavkeProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPostavke postavke = new frmPostavke();
                postavke.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void oProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmOnama onama = new Forme.frmOnama();
                onama.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void podaciOTvrtkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmPodaciTvrtke podaciTvrtke = new Forme.frmPodaciTvrtke();
                podaciTvrtke.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void zaposleniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmZaposlenici zaposlenici = new Forme.sifarnici.frmZaposlenici();
                zaposlenici.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void novaPonudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Ponude.Forme.frmPonude ponuda = new Ponude.Forme.frmPonude();
                ponuda.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void svePonudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Ponude.Forme.frmSvePonude svePonude = new Ponude.Forme.frmSvePonude();
                svePonude.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void statističkiPrikazNoćenjaGostijuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.izvjestaji.Statisticki_pregled.frmStatistikaOdabir statistikaOdabir = new Forme.izvjestaji.Statisticki_pregled.frmStatistikaOdabir();
                statistikaOdabir.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void prometIPoreznoIzvješćeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RESORT.Forme.izvjestaji.Prometi.frmPrometiOdabir prometiOdabir = new Forme.izvjestaji.Prometi.frmPrometiOdabir();
                prometiOdabir.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void kontaktirajtePodrškuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string _path = "podrska.exe";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = _path;
                proc.StartInfo.FileName = _path;
                proc.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Greška kod otvaranja programa za podršku!");
            }
        }

        private void postavkeFiskalizacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Fiskalizacija.frmPodaciFiskalizacija podaciFiskalizacija = new Fiskalizacija.frmPodaciFiskalizacija();
                podaciFiskalizacija.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void nefiskaliziraniRačuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Fiskalizacija.frmNeupjeleTransakcije neuspjeleTransakcije = new Fiskalizacija.frmNeupjeleTransakcije();
                neuspjeleTransakcije.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void poslovniProstorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Fiskalizacija.frmPoslovniProstor poslovniProstor = new Fiskalizacija.frmPoslovniProstor();
                poslovniProstor.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void noviAvansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAvans avans = new frmAvans();
                avans.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sviAvansiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSviAvansi sviAvansi = new FrmSviAvansi();
                sviAvansi.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private void timerBackup_Tick(object sender, EventArgs e)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("http://www.pc1.hr/update/resort/verzija.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");
                string VerzijaNaNetu = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");

                if (Properties.Settings.Default.verzija_programa < Convert.ToDecimal(VerzijaNaNetu))
                {
                    timerBackup.Stop();
                    if (MessageBox.Show("Na internetu postoji novija inačica programa.\r\nŽelite li skinuti noviju verziju programa.", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string path = GetApplicationPath();
                        File.WriteAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/RESORT update.txt", path, Encoding.UTF8);

                        Process.Start(path + "\\RESORT Update.exe");
                    }
                    timerBackup.Start();
                }
                else
                {
                    //MessageBox.Show("Trenutno koristite najnoviju inačicu programa.", "Update");
                }
            }
            catch (Exception)
            {
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.frmKasaPrijava kasaPrijava = new Forme.frmKasaPrijava();
                kasaPrijava.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cijeneSobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmCijene_soba cijenaSobe = new Forme.sifarnici.frmCijene_soba();
                cijenaSobe.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void valuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.sifarnici.frmValute valute = new Forme.sifarnici.frmValute();
                valute.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void noviGradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.Sifarnik.frmNoviGrad noviGrad = new Forme.Sifarnik.frmNoviGrad();
                noviGrad.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ukupnoPoNacinimaPlacanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forme.izvjestaji.UkupnoPoNacinimaPlacanja.frmUkupnoPoNacinimaPlacanja f = new Forme.izvjestaji.UkupnoPoNacinimaPlacanja.frmUkupnoPoNacinimaPlacanja();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}