using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmMenu : Form
    {
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        public Until.classFukcijeZaUpravljanjeBazom baza = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
        private Util.Korisno kor;
        private bool dohvatiKontrolu = true;
        private bool naknadnaFiskalizacijaAlert = false;
        private bool skipPrijava = false;

        /*public frmMenu()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
            CheckConnection();
            if (!skipPrijava)
                baza.ProvjeriDaliTrebaKreiratiNovuGodinu();

            kor = new Util.Korisno();
            Properties.Settings.Default.verzija_programa = 6.870m;
            Properties.Settings.Default.Save();

            #region PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE

            try
            {
                string gggodina = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();

                if (!File.Exists("ProvjeraTablicaBaze" + gggodina))
                {
                    File.WriteAllText("ProvjeraTablicaBaze" + gggodina, "0");
                }

                string verzijaZadnjeProvjereBaze = File.ReadAllText("ProvjeraTablicaBaze" + gggodina);
                decimal verzijaZadnjeProvjereBazeFile = 0, verzijaZadnjeProvjereBazeCurent = 0;
                decimal.TryParse(verzijaZadnjeProvjereBaze, out verzijaZadnjeProvjereBazeFile);
                decimal.TryParse(Properties.Settings.Default.verzija_programa.ToString(), out verzijaZadnjeProvjereBazeCurent);
                Util.Korisno.verzijaPrograma = verzijaZadnjeProvjereBazeCurent;

                if (verzijaZadnjeProvjereBazeFile < verzijaZadnjeProvjereBazeCurent)
                {
                    ProvjeraBaze.IncomingWithUpdate();
                    File.WriteAllText("ProvjeraTablicaBaze" + gggodina, Properties.Settings.Default.verzija_programa.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            #endregion PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE
        }*/

        public frmMenu(bool prijava = false)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
            CheckConnection();
            if (prijava)
                baza.ProvjeriDaliTrebaKreiratiNovuGodinu();

            kor = new Util.Korisno();
            Properties.Settings.Default.verzija_programa = 6.870m;
            Properties.Settings.Default.Save();

            #region PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE

            try
            {
                string gggodina = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();

                if (!File.Exists("ProvjeraTablicaBaze" + gggodina))
                {
                    File.WriteAllText("ProvjeraTablicaBaze" + gggodina, "0");
                }

                string verzijaZadnjeProvjereBaze = File.ReadAllText("ProvjeraTablicaBaze" + gggodina);
                decimal verzijaZadnjeProvjereBazeFile = 0, verzijaZadnjeProvjereBazeCurent = 0;
                decimal.TryParse(verzijaZadnjeProvjereBaze, out verzijaZadnjeProvjereBazeFile);
                decimal.TryParse(Properties.Settings.Default.verzija_programa.ToString(), out verzijaZadnjeProvjereBazeCurent);
                Util.Korisno.verzijaPrograma = verzijaZadnjeProvjereBazeCurent;

                if (verzijaZadnjeProvjereBazeFile < verzijaZadnjeProvjereBazeCurent)
                {
                    ProvjeraBaze.IncomingWithUpdate();
                    File.WriteAllText("ProvjeraTablicaBaze" + gggodina, Properties.Settings.Default.verzija_programa.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            #endregion PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //OVO MORA BITI NA VRHU
            Util.Korisno.VratiDucanIBlagajnu();
            Util.Korisno.getKarticaKupca();
            Util.Korisno.getDomenaZaSinkronizaciju();
            Util.Korisno.getOib();
            Class.Registracija.getPodaci();
            Class.PodaciTvrtka.getPodaci();
            Class.Postavke.getPodaci();
            Class.PosPrint.getPodaci();

            if (!Class.Postavke.is_beauty)
            {
                statistikaRadaPoZaposlenikuToolStripMenuItem.Enabled = false;
                statistikaRadaPoZaposlenikuToolStripMenuItem.Visible = false;
            }

            izdatnicaToolStripMenuItem.Enabled = false;
            izdatnicaToolStripMenuItem.Visible = false;

            if (Class.Postavke.is_caffe)
            {
                izdatnicaToolStripMenuItem.Enabled = true;
                izdatnicaToolStripMenuItem.Visible = true;
            }

            if (Class.PodaciTvrtka.oibTvrtke == "5AR")
            {
                MessageBox.Show("Pozdrav Petar!!! :)");
            }

            if (Environment.MachineName != "POWER-RAC")
            {
                importToolStripMenuItem.Enabled = false;
                importToolStripMenuItem.Visible = false;
            }

            //////////////////////////////////////////SINKRONIZACIJA ARTIKALA SA WEBOM/////////////////////////////////
            try
            {
                if (System.Environment.MachineName == "POWER-RAC" && !Util.Korisno.domena_za_sinkronizaciju.Contains("localhost"))
                {
                    string newDomena = Util.Korisno.domena_za_sinkronizaciju.Replace("//", "//localhost/");
                    Util.Korisno.domena_za_sinkronizaciju = newDomena;
                    classSQL.Setings_Update("update postavke set domena_za_sinkronizaciju = '" + Util.Korisno.domena_za_sinkronizaciju + "';");
                }

                this.Text = DTtvrtka.Rows[0]["ime_tvrtke"].ToString();
                kor.ProvjeriVrijemeUpozoriKorisnika(5);

                kor.ProvjeriNadogradnjuPremaOibu(DTtvrtka.Rows[0]["oib"].ToString());

                if (DateTime.Now.Year > 2014 && DTpostavke.Rows[0]["skidaj_kolicinu_po_dokumentima"].ToString() != "1")
                    classSQL.Setings_Update("UPDATE postavke SET skidaj_kolicinu_po_dokumentima='1'");

                if (DTpostavke.Rows[0]["upozori_iskljucenu_fiskalizaciju"].ToString() == "1" && DTfis.Rows[0]["aktivna"].ToString() == "0")
                {
                    MessageBox.Show("Molimo obratite pozornost na aktivnost fiskalizacije.\r\nU postavkama fiskalizacije fiskalizacija je isključena.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            timerBackupSvakih2Sata.Start();

            CultureInfo before = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hr-HR");
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            }
            finally
            {
            }

            if (baza.UzmiGodinuKojaSeKoristi() != DateTime.Now.Year)
            {
                MessageBox.Show("UPOZORENJE!!!\r\nDatum i vrijeme na računalu nije jednako godini u bazi podataka!\r\n" +
                    "Za više informacija nazovite Code-iT!\r\nMogući problem je da na Vašem računalu nije dobro podešeno vrijeme i datum.", "VAŽNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1)
            {
                timerGodina.Interval = 300000;
            }
            else
            {
                timerGodina.Interval = 900000;
            }

            timerGodina.Start();

            backgroundWorkerDownload.RunWorkerAsync();

            početnoStanjeToolStripMenuItem.Visible = false;

            SetDatabaseOnLoad();

            //TAJMER NEUSPJELE FISKALIZACIJE
            timerNF.Interval = 200000;
            timerNF.Start();

            timerNaknadnaFiskalizacija.Start();

            this.Hide();

            if (File.Exists("code"))
                File.Delete("code");

           /* if (Environment.MachineName != "POWER-RAC" && Environment.MachineName != "DEJANVIBOVIĆ")
            {
                if (DTpostavke.Rows[0]["aktivnost"].ToString() == "0" || !File.Exists("code"))
                {
                    List<string> keys = new List<string>();
                    string uniqId = Class.Registracija.getUniqueID("C");
                    string s = Class.Registracija.GetMD5(uniqId + "5AR" + (Class.Registracija.broj == 0 ? "" : Class.Registracija.broj.ToString())).ToUpper();
                    string ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);

                    if (Util.Korisno.CheckForInternetConnection() && Util.Korisno.CheckForInternetConnection(Properties.Settings.Default.PC_POS_wsSoftKontrol_wsSoftKontrol.ToString()))
                    {
                        if (Class.Registracija.productKey.Length == 0 || Class.Registracija.activationCode.Length == 0 || (Class.Registracija.productKey.Length > 0 && Class.Registracija.productKey != ns))
                        {
                            int newBroj = 0;
                            using (var ws = new wsSoftKontrol.wsSoftKontrol())
                            {
                                if (Class.Registracija.productKey.Length > 0 && Class.Registracija.productKey != ns)
                                {
                                    s = Class.Registracija.GetMD5(uniqId + "5AR").ToUpper();
                                    ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);
                                }

                                keys.Add(ns);
                                if (ws.checkIfProductKeyExists(ns))
                                {
                                    for (int i = 1; i < 100; i++)
                                    {
                                        s = Class.Registracija.GetMD5(uniqId + "5AR" + i.ToString()).ToUpper();
                                        ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);
                                        keys.Add(ns);
                                        if (!ws.checkIfProductKeyExists(ns))
                                        {
                                            newBroj = i;
                                            break;
                                        }
                                    }
                                }
                            }

                            //Caffe.frmRegistracija2017 CR = new Caffe.frmRegistracija2017();
                            //CR.broj = newBroj;
                            //CR.productKey = ns;
                            //CR.MainForm = this;
                            //CR.ShowDialog();
                        }
                    }
                }
            }*/

            DateTime datum_vece = Convert.ToDateTime(DateTime.Now.Year + "-12-20");
            DateTime datum_manje = Convert.ToDateTime(DateTime.Now.Year + "-1-20");

            VisibleControls();

            if (!skipPrijava)
            {
                Caffe.frmPrijava p = new Caffe.frmPrijava();
                p.MainForm = this;
                p.ShowDialog();
            }

            try
            {
                frmScren sc = new frmScren();
                if (this.IsMdiContainer)
                {
                    sc.MdiParent = this;
                }

                sc.MainForm = this;
                sc.Dock = DockStyle.Fill;
                sc.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            /*if (Util.Korisno.GetDopustenjeZaposlenika() <= 2)
            {
                pregledToolStripMenuItem.Visible = true;
            }
            else
            {
                pregledToolStripMenuItem.Visible = false;
            }*/

            timerProvjeraNadogradnje.Start();
            timerSinkronizacijaPoPeriodu.Start();

            backgroundWorker1.RunWorkerAsync();
        }

        private void ImportPartners(string FileName)
        {
            string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", FileName);
            string query = string.Format("select * from [{0}$]", "Firme");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            string grad = "1";

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                grad = "1";
                DataTable DT = classSQL.select("SELECT id_grad FROM grad WHERE lower(grad) LIKE'%" + dataSet.Tables[0].Rows[i][2].ToString().ToLower() + "%'", "grad").Tables[0];
                if (DT.Rows.Count != 0)
                {
                    grad = DT.Rows[0][0].ToString();
                }

                string[] sss = new string[5];

                string sql = "INSERT INTO partners (id_grad, ime_tvrtke, adresa, oib, id_zemlja, vrsta_korisnika, id_partner) VALUES (" +
                    " '" + grad + "'," +
                    " '" + dataSet.Tables[0].Rows[i][0].ToString().Trim() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][1].ToString().Trim() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][3].ToString().Trim() + "'," +
                    " '60'," +
                    " '1'," +
                    " '" + (i + 1) + "'" +
                    ")";
                classSQL.insert(sql);
            }
            MessageBox.Show("Done");
        }

        private void timerGodina_Tick(object sender, EventArgs e)
        {
            baza.ProvjeriDaliTrebaKreiratiNovuGodinu();
        }

        private void CheckConnection()
        {
            try
            {
                if (classSQL.remoteConnectionString != "")
                {
                    if (SQL.claasConnectDatabase.TestRemoteConnection() != true)
                    {
                        frmPostavkeUdaljeneBaze pu = new frmPostavkeUdaljeneBaze();
                        pu.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
                frmPostavkeUdaljeneBaze pu = new frmPostavkeUdaljeneBaze();
                pu.ShowDialog();
                Application.Exit();
            }
        }

        public void GetMotherBoardID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    MessageBox.Show(queryObj["Architecture"].ToString() + "\r\n" + queryObj["ProcessorId"].ToString() + "\r\n" + queryObj["Family"].ToString() + "\r\n" + queryObj["Caption"].ToString());
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }
        }

        private void timerNF_Tick(object sender, EventArgs e)
        {
            try
            {
                timerNF.Interval = 1800000;
                string sql_neuspjele = "SELECT count(*) FROM neuspjela_fiskalizacija";

                int broj = int.Parse(classSQL.select(sql_neuspjele, "neuspjela_fiskalizacija").Tables[0].Rows[0][0].ToString());

                if (broj > 0)
                {
                    if (MessageBox.Show("Postoje neuspjele fiskalizacije!\r\nŽelite li sada fiskalizirati?", "Upozorenje!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Form fc = Application.OpenForms["frmCaffe"];
                        if (fc != null)
                        {
                            Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
                            aa.ShowDialog(fc);
                        }
                        else
                        {
                            Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
                            aa.ShowDialog();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void unosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmAddPartners new_partner = new Sifarnik.frmAddPartners();
            new_partner.MainFormMenu = this;
            new_partner.ShowDialog();
        }

        private void robauslugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajniArtikli robaUsluge = new Caffe.frmProdajniArtikli();
            robaUsluge.MdiParent = this;
            robaUsluge.Dock = DockStyle.Fill;
            robaUsluge.MainForm = this;
            robaUsluge.Show();
        }

        private void kalkulacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNovaKalkulacija nova_kalkulacija = new frmNovaKalkulacija();
            nova_kalkulacija.MdiParent = this;
            nova_kalkulacija.Dock = DockStyle.Fill;
            nova_kalkulacija.MainForm = this;
            nova_kalkulacija.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            probe pr = new probe();
            pr.ShowDialog();
        }

        private void sveKalkulacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPopisKalkulacija fr = new frmPopisKalkulacija();
            fr.MdiParent = this;
            fr.MainFormMenu = this;
            fr.Show();
        }

        private void novaFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFaktura f = new frmFaktura();
                f.MdiParent = this;
                f.Dock = DockStyle.Fill;
                f.MainForm = this;
                f.Show();
            }
            catch
            {
                frmFaktura f = new frmFaktura();
                f.ShowDialog();
            }
        }

        private void sveFaktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSveFakture f = new frmSveFakture();
                f.MainFormMenu = this;
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void novaPonudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPonude ponu = new frmPonude();
            ponu.MdiParent = this;
            ponu.Dock = DockStyle.Fill;
            ponu.MainForm = this;
            ponu.Show();
        }

        private void svePonudeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSavPotrosniMaterijal sp = new frmSavPotrosniMaterijal();
                sp.MainFormMenu = this;
                sp.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void noviRadniNalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRadniNalog rn = new frmRadniNalog();
            rn.MdiParent = this;
            rn.Dock = DockStyle.Fill;
            rn.MainFormMenu = this;
            rn.Show();
        }

        private void sviRadniNaloziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSviRadniNalozi srn = new frmSviRadniNalozi();
            srn.MdiParent = this;
            srn.MainFormMenu = this;
            srn.Show();
        }

        private void unosNormativaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNormativi rn = new frmNormativi();
            rn.MdiParent = this;
            rn.Dock = DockStyle.Fill;
            rn.MainForm = this;
            rn.Show();
        }

        private void sviNormativiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSviNormativi sf = new frmSviNormativi();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void karticaSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void karticaSkladištaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        public void otpremnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOtpremnica ot = new frmOtpremnica();
            ot.MdiParent = this;
            ot.Dock = DockStyle.Fill;
            ot.MainForm = this;
            ot.Show();
        }

        private void sveOtpremniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSveOtpremnice so = new frmSveOtpremnice();
            so.MdiParent = this;
            so.MainFormMenu = this;
            so.Show();
        }

        private void aktivnostiZaposlenikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAktivnostiZaposlenika AZ = new frmAktivnostiZaposlenika();
            AZ.ShowDialog();
        }

        private void kasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(Caffe.frmCaffe))
                {
                    OpenForm.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            Caffe.frmCaffe ks = new Caffe.frmCaffe();
            ks.MainForm = this;
            ks.Show();
        }

        private void promocijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPromocijePostavke pp = new frmPromocijePostavke();
            pp.ShowDialog();
        }

        private void unosInventureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUnosInventura inv = new frmUnosInventura();
            inv.MdiParent = this;
            inv.Dock = DockStyle.Fill;
            inv.MainFormMenu = this;
            inv.Show();
        }

        private void podaciOTvrtkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            podloga frmpo = new podloga();
            frmpo.ShowDialog();
        }

        private void posPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPosPrinter posprint = new frmPosPrinter();
            posprint.ShowDialog();
        }

        private void postavkeProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPostavke settings_form = new frmPostavke();
            settings_form.ShowDialog();
        }

        private void gradoviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.Gradovi grad = new Sifarnik.Gradovi();
            grad.ShowDialog();
        }

        private void skladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmSkladista sk = new Sifarnik.frmSkladista();
            sk.ShowDialog();
        }

        private void zemljeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmZemlje zm = new Sifarnik.frmZemlje();
            zm.ShowDialog();
        }

        private void žiroRačuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmZiroRacun zr = new Sifarnik.frmZiroRacun();
            zr.ShowDialog();
        }

        private void zaposleniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmZaposlenici zap = new Sifarnik.frmZaposlenici();
            zap.ShowDialog();
        }

        private void blagajneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmBlagajne bl = new Sifarnik.frmBlagajne();
            bl.ShowDialog();
        }

        private void dučaniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmDucani duc = new Sifarnik.frmDucani();
            duc.ShowDialog();
        }

        private void proizviđaćiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmProizvodaci pro = new Sifarnik.frmProizvodaci();
            pro.ShowDialog();
        }

        private void grupeProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmGrupeProizvoda GP = new Sifarnik.frmGrupeProizvoda();
            GP.ShowDialog();
        }

        private void međuskladišnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMeduskladisnica med = new frmMeduskladisnica();
            med.MdiParent = this;
            med.Dock = DockStyle.Fill;
            med.MainForm = this;
            med.Show();
        }

        private void sveOtpremniceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSveMeduskladisnice sm = new frmSveMeduskladisnice();
            sm.MdiParent = this;
            sm.MainFormMenu = this;
            sm.Show();
        }

        private void sviRačuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmSviRacuni sr = new Kasa.frmSviRacuni();
            sr.ShowDialog();
        }

        private void sveInventureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSveInventure sinv = new frmSveInventure();
            sinv.MdiParent = this;
            sinv.MainFormMenu = this;
            sinv.Show();
        }

        private void početnoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPocetnoStanje pc = new frmPocetnoStanje();
            pc.MdiParent = this;
            pc.Dock = DockStyle.Fill;
            pc.Show();
        }

        private void odjavaKomisioneRobeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Robno.frmOdjavaKomisione zp = new Robno.frmOdjavaKomisione();
            zp.MdiParent = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sveOdjavaKomisioneRobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmSveOdjave_komisione sf = new Robno.frmSveOdjave_komisione();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void zapisnikOPromjeniCijeneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Robno.frmZapisnikopromjeniCijene zp = new Robno.frmZapisnikopromjeniCijene();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sviZapisniciOPromjeniCijeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmSvePromjeneCijena sf = new Robno.frmSvePromjeneCijena();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void povratRobeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Robno.frmPovratRobe zp = new Robno.frmPovratRobe();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sviPovratiRobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmSviPovrati sf = new Robno.frmSviPovrati();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void prometKasePoRačunimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.Kasa.frmPrometKase pr = new Report.Kasa.frmPrometKase();
            pr.ShowDialog();
        }

        private void prometKaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometKase sf = new Kasa.frmPrometKase();
            sf.MdiParent = this;
            sf.Show();
        }

        private void novaPrimkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmPrimka zp = new Robno.frmPrimka();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void picKalk_Click(object sender, EventArgs e)
        {
        }

        private void prometPoRobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometPoRobi prp = new Kasa.frmPrometPoRobi();
            prp.ShowDialog();
        }

        private void neposlanaFiskalizacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
            aa.ShowDialog(this);
        }

        private void prometIZaključnoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometIzakljucnoStanje pz = new Kasa.frmPrometIzakljucnoStanje();
            pz.ShowDialog();
        }

        private void VisibleControls()
        {
            radniNalogToolStripMenuItem1.Visible = false;
            prometKasePoRačunimaToolStripMenuItem.Visible = false;
            karticaRobeToolStripMenuItem.Visible = false;
            odjavaKomisioneRobeToolStripMenuItem.Visible = false;

            promocijaToolStripMenuItem.Visible = false;

            string zaposlenik = "0";

            if (zaposlenik == "1")
            {
            }

            if (zaposlenik == "2")
            {
            }

            if (zaposlenik == "3")
            {
                foreach (Form OpenForm in Application.OpenForms)
                {
                    if (OpenForm.GetType() == typeof(Caffe.frmCaffe))
                    {
                        OpenForm.WindowState = FormWindowState.Maximized;
                        return;
                    }
                }

                Caffe.frmCaffe ks = new Caffe.frmCaffe();
                ks.MainForm = this;
                ks.Show();
            }
        }

        private void svePrimkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmSvePrimke sp = new Robno.frmSvePrimke();
            sp.MainFormMenu = this;
            sp.MdiParent = this;
            sp.Show();
        }

        private void repromaterijalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmRepromaterijal rp = new Caffe.frmRepromaterijal();
            rp.MainFormMenu = this;
            rp.ShowDialog();
        }

        private void prometPoProdajnojRobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajnaRoba prp = new Caffe.frmProdajnaRoba();
            prp.ShowDialog();
        }

        private void stoloviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmStolovi s = new Caffe.frmStolovi();
            s.ShowDialog();
        }

        private void dodakNoviGradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik.frmNoviGrad NG = new Sifarnik.frmNoviGrad();
            NG.ShowDialog();
        }

        private void postaviStanjePremaZadnjojInventuriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmPostaviStanjePremaZadnjojInventuri PS = new Robno.frmPostaviStanjePremaZadnjojInventuri();
            PS.ShowDialog();
        }


        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private void poslovniProstorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmPoslovniProstor ps = new Fiskalizacija.frmPoslovniProstor();
            ps.ShowDialog();
        }

        private void neuspjeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
            aa.ShowDialog(this);
        }

        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Podrška POWER COMPUTERS.exe"))
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        //PCPOS.Until.classDownloadFiles down = new Until.classDownloadFiles();
                        //down.SkiniDatoteku("http://pc1.hr/podrska/help.doc", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Podrška POWER COMPUTERS.exe");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene " +
                " WHERE smjene.id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa='" + Util.Korisno.idKasa + "' ", "smjene").Tables[0];
            if (DSbr.Rows.Count > 0)
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "null";
            }
        }

        private void sveSmjeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmSveSmjene sm = new Caffe.frmSveSmjene();
            sm.ShowDialog();
        }

        private void fakturaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void ispisSvihRačunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmSviRacuni sr = new Caffe.frmSviRacuni();
            sr.ShowDialog();
        }

        private void popisNormativaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.normativi.frmNormativi n = new Report.normativi.frmNormativi();
            n.ShowDialog();
        }

        private void pregledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.predracuni.frmIspisPredracuna ip = new Report.predracuni.frmIspisPredracuna();
            ip.ShowDialog();
        }

        private void SetDatabaseOnLoad()
        {
            DataTable DT = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            DataRow[] dataROW = DT.Select("table_name = 'uskladenje_prometa_new'");
            if (dataROW.Length == 0)
            {
                string sqlTab = "CREATE TABLE uskladenje_prometa_new" +
                " (id serial NOT NULL,sifra character varying(30), " +
                "  naziv character varying(50)," +
                "  obracun timestamp without time zone," +
                "  donos_od timestamp without time zone," +
                "  donos_kolicina numeric," +
                "  donos_brojcanik numeric," +
                "  donos_unos_robe numeric," +
                "  prodaja_kucano numeric," +
                "  prodaja_prodano numeric," +
                "  prodaja_stanje numeric," +
                "  prodaja_razlika numeric," +
                "  prodaja_iznos_kn numeric," +
                "  prijenos_stanje numeric," +
                "  prijenos_brojcanik numeric," +
                "  broj int," +
                "  ukupno numeric," +
                "  cijena numeric," +
                "  prodajna_cijena numeric," +
                "  zaposlenici character varying(50)," +
                "  brojcanik_za_donos1 numeric," +
                "  brojcanik_za_donos2 numeric," +
                "  brojcanik_kraj_dana1 numeric," +
                "  brojcanik_kraj_dana2 numeric," +
                "  CONSTRAINT id_uskladenje_prometa_new_primary_key PRIMARY KEY (id )" +
                " )";
                classSQL.select(sqlTab, "uskladenje_prometa");
            }

            dataROW = DT.Select("table_name = 'uskladenje_prometa_trenutno'");
            if (dataROW.Length == 0)
            {
                string sqlTab = "CREATE TABLE uskladenje_prometa_trenutno" +
                " (id serial NOT NULL,sifra character varying(30), " +
                "  naziv character varying(50)," +
                "  obracun timestamp without time zone," +
                "  donos_od timestamp without time zone," +
                "  donos_kolicina numeric," +
                "  donos_brojcanik numeric," +
                "  donos_unos_robe numeric," +
                "  prodaja_kucano numeric," +
                "  prodaja_prodano numeric," +
                "  prodaja_stanje numeric," +
                "  prodaja_razlika numeric," +
                "  prodaja_iznos_kn numeric," +
                "  prijenos_stanje numeric," +
                "  prijenos_brojcanik numeric," +
                "  broj int," +
                "  ukupno numeric," +
                "  cijena numeric," +
                "  prodajna_cijena numeric," +
                "  zaposlenici character varying(50)," +
                "  brojcanik_za_donos1 numeric," +
                "  brojcanik_za_donos2 numeric," +
                "  brojcanik_kraj_dana1 numeric," +
                "  brojcanik_kraj_dana2 numeric," +
                "  donos_datum character varying(50)," +
                "  obracun_dat character varying(50)," +
                "  promet_kase numeric," +
                "  promet_brojano numeric," +
                "  CONSTRAINT id_uskladenje_prometa_2_primary_key PRIMARY KEY (id )" +
                " )";
                classSQL.select(sqlTab, "uskladenje_prometa");
            }

            dataROW = DT.Select("table_name = 'uskladenje_prometa'");
            if (dataROW.Length == 0)
            {
                string sqlTab = "CREATE TABLE uskladenje_prometa" +
                " (id serial NOT NULL,sifra character varying(30), " +
                "  naziv character varying(50)," +
                "  od_datuma timestamp without time zone," +
                "  do_datuma timestamp without time zone," +
                "  kolicina_skladiste numeric," +
                "  fiskalizirano numeric," +
                "  predracun numeric," +
                "  razlika numeric," +
                "  ukupno numeric," +
                "  unos_robe numeric," +
                "  brojcanik numeric," +
                "  broj int," +
                "  cijena numeric," +
                "  CONSTRAINT id_uskladenje_prometa_primary_key PRIMARY KEY (id )" +
                " )";
                classSQL.select(sqlTab, "uskladenje_prometa");
            }

            dataROW = DT.Select("table_name = 'kucani_predracuni'");
            if (dataROW.Length == 0)
            {
                string sqlTab = "CREATE TABLE kucani_predracuni" +
                " (id serial NOT NULL," +
                "  sifra character varying(30), " +
                "  datum timestamp without time zone," +
                "  kolicina numeric," +
                "  id_zaposlenik int," +
                "  id_skladiste int," +
                "  CONSTRAINT kucani_predracuni_primary_key PRIMARY KEY (id )" +
                "  )";
                classSQL.select(sqlTab, "uskladenje_prometa");
            }

            DataTable DTcn = classSQL.select("SELECT * FROM caffe_narudzbe LIMIT 1", "roba").Tables[0];

            if (DTcn.Columns["datum"] == null)
            {
                string sss = "ALTER TABLE caffe_narudzbe ADD COLUMN datum timestamp without time zone";
                classSQL.insert(sss);
                classSQL.update("UPDATE caffe_narudzbe SET datum='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'");
            }

            DataTable DTrp = classSQL.select("SELECT * FROM roba_prodaja LIMIT 1", "roba").Tables[0];

            if (DTrp.Columns["kolicina_predracun"] == null)
            {
                string sss = "ALTER TABLE roba_prodaja ADD COLUMN kolicina_predracun decimal";
                classSQL.insert(sss);
                classSQL.update("UPDATE roba_prodaja SET kolicina_predracun='0'");
            }

            if (DTrp.Columns["cijena2"] == null)
            {
                string sss = "ALTER TABLE roba_prodaja ADD COLUMN cijena2 decimal";
                classSQL.insert(sss);
                classSQL.update("UPDATE roba_prodaja SET cijena2='0'");
            }

            if (DTrp.Columns["u_pakiranju"] == null)
            {
                string sss = "ALTER TABLE roba_prodaja ADD COLUMN u_pakiranju decimal";
                classSQL.insert(sss);
                classSQL.update("UPDATE roba_prodaja SET u_pakiranju='1'");
            }

            if (DTrp.Columns["brojcanik"] == null)
            {
                string sss = "ALTER TABLE roba_prodaja ADD COLUMN brojcanik decimal";
                classSQL.insert(sss);
                classSQL.select_settings(sss, "roba");
                classSQL.update("UPDATE roba SET brojcanik='0'");
            }

            DataTable DTporez = classSQL.select("SELECT * FROM porezi WHERE iznos='5'", "porezi").Tables[0];
            if (DTporez.Rows.Count == 0)
            {
                classSQL.insert("INSERT  INTO porezi (naziv,iznos) VALUES ('PDV 5%','5')");
            }
        }

        private void usklađenjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmUskladenjePrometa u = new Caffe.frmUskladenjePrometa();
            u.ShowDialog();
        }

        private void usklađenjeSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmKarticaSkladista f = new Caffe.frmKarticaSkladista();
            f.Text = (sender as ToolStripMenuItem).Text;
            f.ShowDialog();
        }

        private void pregledRačunaPoDanimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima pd = new Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima();
            pd.ShowDialog();
        }

        private void porezNaPotrošnjuPoKategorijamaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<string> L = baza.UzmiSveBazeIzPostgressa();
                string sve_baze = "";
                foreach (string DBe in L)
                {
                    sve_baze += DBe + ";";
                }

                string dd = Environment.SystemDirectory + "\\msvcr100.dll";

                Until.classDownloadFiles d = new Until.classDownloadFiles();

                try
                {
                    if (!File.Exists("msvcr100.dll"))
                    {
                        d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/msvcr100.dll", "msvcr100.dll");
                    }
                }
                catch
                {
                }

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                try
                {
                    if (!File.Exists(Environment.SystemDirectory + "\\msvcr100.dll"))
                    {
                        if (File.Exists("msvcr100.dll"))
                        {
                            d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/DlluSystem32.doc", "DlluSystem32.exe");
                            GC.Collect();
                            if (File.Exists("DlluSystem32.exe"))
                            {
                                Process proc = Process.Start(path + "\\DlluSystem32.exe");
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    if (Directory.Exists("C:\\windows\\sysWOW64"))
                    {
                        if (!File.Exists("C:\\windows\\sysWOW64\\msvcr100.dll"))
                        {
                            if (File.Exists("msvcr100.dll"))
                            {
                                d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/DlluSystem32.doc", "DlluSystem32.exe");
                                GC.Collect();
                                if (File.Exists("DlluSystem32.exe"))
                                {
                                    Process proc = Process.Start(path + "\\DlluSystem32.exe");
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            catch
            {
            }
        }

        private void novaKalkulacijaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Robno.frmKalkulacija zp = new Robno.frmKalkulacija();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sveKalkulacijeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Robno.frmSveKalkulacije sk = new Robno.frmSveKalkulacije();
            sk.MainFormMenu = this;
            sk.MdiParent = this;
            sk.Show();
        }
        /*
        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();
        private string iskljucenZbogDugovanja = "";

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            if (dohvatiKontrolu)
            {
                getKontrola();

                dohvatiKontrolu = false;
            }

            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        }
        */
        private void getKontrola()
        {
            try
            {
                using (var ws = new wsSoftKontrol.wsSoftKontrol())
                {
                    var kontrola = ws.Kontrola(Class.PodaciTvrtka.oibTvrtke);

                    if (kontrola != null && kontrola.Split('¤')[0] != null && kontrola.Split('¤')[1] != null && kontrola.Split('¤')[0] != "0")
                    {
                        int iKon = 0;
                        if (Int32.TryParse(kontrola.Split('¤')[0], out iKon))
                        {
                            if (iKon > 0)
                            {
                                if (iKon == 1)
                                { //obavijest
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        MessageBox.Show(this, kontrola.Split('¤')[1], "Obavijest");
                                    });
                                }
                                else
                                { //iskljucenje
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (MessageBox.Show(this, kontrola.Split('¤')[1], "Obavijest") == System.Windows.Forms.DialogResult.OK)
                                        {
                                            this.Close();
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private DataSet DS_aktivacija;
        private newSql SqlPostgres = new newSql();

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sinkronizacija.frmPoruka msg = new Sinkronizacija.frmPoruka();
            if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1")
            {
                msg.Show();
            }

            Until.FunkcijeRobno robno = new Until.FunkcijeRobno();
            robno.PostaviStanjeSkladista();

            Util.Korisno kor = new Util.Korisno();
            kor.ProvjeriRacuneOdZadnjeProvjere(DTpostavke);

            if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1")
            {
                msg.Close();
            }
        }

        private void timerBackupSvakih2Sata_Tick(object sender, EventArgs e)
        {
            try
            {
                string remoteServer = "";
                string DBname = "";

                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
                XDocument xmlFile = XDocument.Load(path);
                var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
                foreach (XElement book in query)
                {
                    remoteServer = book.Attribute("server").Value;
                    DBname = book.Attribute("database").Value;
                }

                string lokacija_za_spremanje = "";
                if (Directory.Exists(DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString()))
                {
                    lokacija_za_spremanje = DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString();
                }
                else
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString()))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString());
                    }

                    lokacija_za_spremanje = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString();
                }

                File.WriteAllText("DBbackup/DBbackup.bat", "pg_dump.exe --host " + remoteServer + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + lokacija_za_spremanje + "\\db" + DateTime.Now.ToString("yyyy-MM-dd H") + ".backup\" \"" + DBname + "\"");
                string _path = System.Environment.CurrentDirectory;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.WorkingDirectory = _path + "\\DBbackup";
                proc.StartInfo.FileName = _path + "\\DBbackup\\DBbackup.bat";
                proc.Start();
            }
            catch { }
        }

        private void bgSinkronizacija_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Util.Korisno.RadimSinkronizaciju = false;
        }

        private void novaMeđuskladišnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmMeduskladisnica ms = new Robno.frmMeduskladisnica();
            ms.MdiParent = this;
            ms.Dock = DockStyle.Fill;
            ms.MainForm = this;
            ms.Show();
        }

        private void sveMeđuskladišniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmSveMS ms = new Robno.frmSveMS();
            ms.MdiParent = this;
            ms.MainFormMenu = this;
            ms.Show();
        }

        private void noviPotrošniMaterijalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPotrosniMaterijal ms = new frmPotrosniMaterijal();
            ms.MdiParent = this;
            ms.MainForm = this;
            ms.Dock = DockStyle.Fill;
            ms.Show();
        }

        private void sviDokumentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSviDokumentiPotrosnogMaterijala ms = new frmSviDokumentiPotrosnogMaterijala();
            ms.MdiParent = this;
            ms.MainFormMenu = this;
            ms.Show();
        }

        private void fiskalizirajRačuneSaGreškomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmNaknadnaFiskalizacija().ShowDialog();
        }

        private void uvozArtiklaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Until.frmUvozArtiklaCSV().ShowDialog();
        }

        private void sviPredračuniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void napomenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Sifarnik.frmNapomene f = new Sifarnik.frmNapomene();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void karticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Sifarnik.frmKartice f = new Sifarnik.frmKartice();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sveOtpremniceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmSveOtpremnice f = new frmSveOtpremnice();
                f.MainFormMenu = this;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerSinkronizacijaPoPeriodu_Tick(object sender, EventArgs e)
        {
            try
            {
                timerSinkronizacijaPoPeriodu.Stop();
                timerSinkronizacijaPoPeriodu.Interval = 300000;
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    bgSinkronizacija.RunWorkerAsync();
                }
                timerSinkronizacijaPoPeriodu.Start();
            }
            catch
            {
                timerSinkronizacijaPoPeriodu.Start();
                throw;
            }
        }

        private void timerRestartNovaGodina_Tick(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void partnesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                ImportPartners(fdlg.FileName);
                Application.DoEvents();
            }
        }

        private void statistikaRadaPoZaposlenikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Report.Beauty.StatistikaRadaPoZaposleniku.frmStatistikaRadaPoZaposleniku f = new Report.Beauty.StatistikaRadaPoZaposleniku.frmStatistikaRadaPoZaposleniku();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void izdatnicaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Robno.frmIzdatnica f = new Robno.frmIzdatnica();
                f.MdiParent = this;
                f.MainForm = this;
                f.Dock = DockStyle.Fill;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sveIzdatniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Robno.frmSveIzdatnice f = new Robno.frmSveIzdatnice();
                f.MdiParent = this;
                f.MainFormMenu = this;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void osobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOsobe f = new frmOsobe();
            f.openInMenu = true;
            f.ShowDialog();
        }

        private void blagajničkiIzvještajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBlagajnickiIzvjestaj f = new frmBlagajnickiIzvjestaj();
            f.ShowDialog();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void narudžbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Caffe.frmSveNarudzbe f = new Caffe.frmSveNarudzbe();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
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

        private void izlazneListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzlazniRacuni izlazniRacuni = new IzlazniRacuni();
            izlazniRacuni.ShowDialog();
        }

        private void PPMIPOtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzlazniDokumenti.PPMIPOForm form = new IzlazniDokumenti.PPMIPOForm();
            form.ShowDialog();
        }

        private void ObavijestNaknadnaFiskalizacija()
        {
            string query = @"SELECT
                            racuni.broj_racuna,
                            racuni.datum_racuna,
                            ducan.ime_ducana,
                            racuni.id_ducan,
                            blagajna.ime_blagajne,
                            racuni.nacin_placanja,
                            racuni.id_blagajnik,
                            racuni.id_kasa,
                            racuni.godina,
                            racuni.jir,
                            racuni.zik,
                            SUM(
                            CAST(mpc AS NUMERIC)*
                            CAST(REPLACE(kolicina,',','.') AS NUMERIC)
                            ) AS ukupno
                            FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.godina=racun_stavke.godina AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_blagajna
                            LEFT JOIN ducan ON ducan.id_ducan=racuni.id_ducan
                            LEFT JOIN blagajna ON blagajna.id_blagajna=racuni.id_kasa
                            WHERE ((jir='' OR zik='') OR (jir IS NULL OR zik IS NULL)) AND racun_stavke.id_ducan='" + Util.Korisno.idDucan + @"' AND racun_stavke.id_blagajna='" + Util.Korisno.idKasa + @"'
                            GROUP BY racuni.broj_racuna,racuni.godina,racuni.datum_racuna,ducan.ime_ducana,blagajna.ime_blagajne,
                            racuni.id_blagajnik,racuni.nacin_placanja,racuni.id_ducan,racuni.id_kasa,racuni.godina,racuni.jir,racuni.zik
                            ORDER BY racuni.datum_racuna ASC;";

            DataTable DTracuni = classSQL.select(query, "racuni").Tables[0];
            if (DTracuni.Rows.Count > 0)
            {
                bool naknadnaFiskalizacija = false;
                bool neuspjelaFiskalizacija = false;
                foreach (DataRow row in DTracuni.Rows)
                {
                    if ((row["jir"].ToString() == "" || row["jir"].ToString() == "''")
                    && (row["zik"].ToString() == "" || row["zik"].ToString() == "''"))
                        naknadnaFiskalizacija = true;

                    if ((row["jir"].ToString() == "" || row["jir"].ToString() == "''") && row["zik"].ToString() != "")
                        neuspjelaFiskalizacija = true;
                }

                if (naknadnaFiskalizacija || neuspjelaFiskalizacija)
                {
                    timerNaknadnaFiskalizacija.Stop();
                    MessageBox.Show("Postoje računi s greškom i potrebna je naknadna fiskalizacija. Pritisnite tipku OK za nastavak.", "Računi s greškom", MessageBoxButtons.OK);
                    if (Application.OpenForms["frmNaknadnaFiskalizacija"] == null && naknadnaFiskalizacija)
                    {
                        frmNaknadnaFiskalizacija form = new frmNaknadnaFiskalizacija();
                        form.ShowDialog();
                    }

                    if (Application.OpenForms["frmNeupjeleTransakcije"] == null && neuspjelaFiskalizacija)
                    {
                        Fiskalizacija.frmNeupjeleTransakcije form = new Fiskalizacija.frmNeupjeleTransakcije();
                        form.ShowDialog();
                    }
                    timerNaknadnaFiskalizacija.Start();
                }
            }
        }

        private void timerNaknadnaFiskalizacija_Tick(object sender, EventArgs e)
        {
            ObavijestNaknadnaFiskalizacija();
        }

        private void karticaSkladistaDetaljnoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prometPrProdajnojRobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmProdajnaRoba prp = new Caffe.frmProdajnaRoba();
            prp.ShowDialog();
        }

        private void prometPoRobiSaSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometPoRobi prp = new Kasa.frmPrometPoRobi();
            prp.ShowDialog();
        }

        private void prometIZaključnoStanjeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometIzakljucnoStanje pz = new Kasa.frmPrometIzakljucnoStanje();
            pz.ShowDialog();
        }

        private void pregledRačunaPoDanimaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima pd = new Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima();
            pd.ShowDialog();
        }

        private void obracunGrupeProizvodauIzradiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzlazniDokumenti.ObracunGrupeProizvodaForm form = new IzlazniDokumenti.ObracunGrupeProizvodaForm();
            form.ShowDialog();
        }

        private void obracunPorezaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzlazniDokumenti.ObracunForm form = new IzlazniDokumenti.ObracunForm();
            form.ShowDialog();
        }

        private void prometKaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometKase sf = new Kasa.frmPrometKase();
            sf.MdiParent = this;
            sf.Show();
        }

        private void ispisSvihRačunaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Caffe.frmSviRacuni sr = new Caffe.frmSviRacuni();
            sr.ShowDialog();
        }

        private void aktivnostZaposlenikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAktivnostiZaposlenika form = new frmAktivnostiZaposlenika();
            form.ShowDialog();
        }

        private void neuspjeleTransakcijeFiskalizacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmNeupjeleTransakcije form = new Fiskalizacija.frmNeupjeleTransakcije();
            form.ShowDialog(this);
        }

        private void fiskalizirajRačuneSaGreškomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmNaknadnaFiskalizacija().ShowDialog();
        }

        private void poslovniProstorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Fiskalizacija.frmPoslovniProstor form = new Fiskalizacija.frmPoslovniProstor();
            form.ShowDialog();
        }

        private void pregledIzdanihPredracunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.predracuni.frmIspisPredracuna ip = new Report.predracuni.frmIspisPredracuna();
            ip.ShowDialog();
        }

        private void robaNaSkladištuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmRobaNaSkladistu rs = new Caffe.frmRobaNaSkladistu();
            rs.ShowDialog();
        }

        private void karticaRobeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmKarticaRobe kr = new frmKarticaRobe();
            kr.MdiParent = this;
            kr.Dock = DockStyle.Fill;
            kr.MainFormMenu = this;
            kr.Show();
        }

        private void karticaSkladištaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmKarticaSkladiste ks = new frmKarticaSkladiste();
            ks.MdiParent = this;
            ks.Dock = DockStyle.Fill;
            ks.MainFormMenu = this;
            ks.Show();
        }

        private void karticaSkladištaDetaljnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caffe.frmKarticaSkladista form = new Caffe.frmKarticaSkladista();
            form.ShowDialog();
        }

        private void porezNaPotrosnjuPoGrupamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPorezNaPotrosnju frm = new Kasa.frmPorezNaPotrosnju();
            frm.ShowDialog();
        }

        private void predračuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Caffe.frmPredracuni f = new Caffe.frmPredracuni();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prijava"></param>
        public void SetSkipPrijava(bool prijava)
        {
            skipPrijava = prijava;
        }

        private void uskladaRobeNaSkladištuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UskladaSkladistaForm uskladaSkladista = new UskladaSkladistaForm();
            uskladaSkladista.MdiParent = this;
            uskladaSkladista.Dock = DockStyle.Fill;
            uskladaSkladista.Show();
        }

        private void postavkeZaSlanjeDokumentacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPostavkeZaSlanjeDokumentacije frmPostavkeZaSlanjeDokumentacije = new FrmPostavkeZaSlanjeDokumentacije();
            frmPostavkeZaSlanjeDokumentacije.ShowDialog();
        }
    }
}