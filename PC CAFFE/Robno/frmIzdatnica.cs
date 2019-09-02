using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmIzdatnica : Form
    {
        public frmIzdatnica()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private string skladiste_pocetno = "";
        private bool edit = false;

        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        private DataTable DTRoba;

        //ova 4 datatablea nam trebaju za konkurentnost
        private DataTable IZDATNICA_IZ_BAZE;

        private DataTable TRENUTNA_IZDATNICA_IZ_BAZE;
        private DataTable STAVKE_IZ_BAZE;
        private DataTable TRENUTNA_STAVKA_IZ_BAZE;

        public int broj_izdatnice { get; set; }
        public int broj_skladista { get; set; }

        private string ID_IZDATNICA;
        private string BROJ_IZDATNICA;
        public frmMenu MainForm { get; set; }

        private void frmIzdatnica_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;

            setDefault();
            setTextBrojIzdatnice();
            ControlDisableEnable(true, false, false, false, false, true);
            EnableDisable(false);

            this.Paint += new PaintEventHandler(Form1_Paint);

            if (broj_izdatnice != 0 && broj_skladista != 0)
            {
                FillIzdatnica(broj_izdatnice, broj_skladista);
            }
            setTextIzradio(Properties.Settings.Default.id_zaposlenik);
        }

        #region Customize DataGridView

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmIzdatnica MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();

                //d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                //d.BeginEdit(true);
            }
            //else if (d.CurrentCell.ColumnIndex == 5)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
            //    d.BeginEdit(true);
            //}
            //else if (d.CurrentCell.ColumnIndex == 6)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
            //    d.BeginEdit(true);
            //}
            //else if (d.CurrentCell.ColumnIndex == 7)
            //{
            //    d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
            //    d.BeginEdit(true);
            //}
            //else if (d.CurrentCell.ColumnIndex == 8)
            //{
            //    int curent = d.CurrentRow.Index;
            //    txtSifra_robe.Text = "";
            //    txtSifra_robe.Focus();
            //}
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        #endregion Customize DataGridView

        #region Buttons

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            setTextBrojIzdatnice();
            ID_IZDATNICA = null;
            BROJ_IZDATNICA = null;
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(false, true, true, false, false, false);
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                txtSifra_robe.Text = txtSifra_robe.Text.Trim();

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.id_skladiste = (int)cbSkladiste.SelectedValue;
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                selectRobaHelper(txtSifra_robe.Text);
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.id_skladiste = (int)cbSkladiste.SelectedValue;
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                selectRobaHelper(propertis_sifra);
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Robno.frmSveIzdatnice objForm2 = new Robno.frmSveIzdatnice();
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_izdatnice != 0 && broj_skladista != 0)
            {
                deleteFields();
                FillIzdatnica(broj_izdatnice, broj_skladista);
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove izadtnice vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu izdatnicu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                provjera_sql(classSQL.delete("DELETE FROM izdatnica WHERE id_izdatnica='" + ID_IZDATNICA + "'"));

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    deleteStavkaHelper(i);
                }
                PregledKolicine();
                dgw.Rows.Clear();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "Brisanje cijele izdatnice dobavljaču br." + txtBroj.Text);
                //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Izdatnica", txtBroj.Text, true);
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(true, false, false, true, false, true);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                string a = dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString();

                //stavka je tek dodana (još ne postoji u bazi), pa samo obrišemo iz datagrida
                if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value == null)
                {
                    PregledKolicine();
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    MessageBox.Show("Obrisano.");
                }
                //stavka je već u bazi pa ju moramo izbrisati iz baze i ažurirati skladište
                else
                {
                    if (MessageBox.Show("Brisanjem ove stavke vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        DataGridViewRow r = dgw.CurrentRow;

                        deleteStavkaHelper(r.Index);

                        PregledKolicine();
                        dgw.Rows.RemoveAt(r.Index);
                        //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Izdatnica", txtBroj.Text, true);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Report.Robno.repIzdatnica rav = new Report.Robno.repIzdatnica();
            //rav.dokumenat = "Pri";
            //rav.ImeForme = "Izdatnica";
            //rav.broj_dokumenta = txtBroj.Text;
            //rav.broj_skladista = cbSkladiste.SelectedValue.ToString();
            //rav.ShowDialog();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            deleteFields();
            setTextBrojIzdatnice();
            edit = false;
            ControlDisableEnable(true, false, false, false, false, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;

            if (!Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse) || txtSifraOdrediste.Text == "")
            {
                MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška");
                return;
            }

            bool spremljeno = SetIzdatnica();
            PregledKolicine();
            if (spremljeno)
            {
                EnableDisable(false);
                deleteFields();
                setTextBrojIzdatnice();
                edit = false;
                ControlDisableEnable(true, false, false, false, false, true);
                bgSinkronizacija.RunWorkerAsync();
                MessageBox.Show("Spremljeno.");
            }
            else
            {
                FillIzdatnica(Convert.ToInt64(txtBroj.Text), Convert.ToInt16(cbSkladiste.SelectedValue));
            }
        }

        private Sinkronizacija.synPokretac PokretacSinkronizacije = new Sinkronizacija.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void PregledKolicine()
        {
            //OVO RADI SPREMLJENA PROCEDURA
            try
            {
                //if (DTpostavke.Rows[0]["skidaj_skladiste_programski"].ToString() == "1")
                //{
                //    string _sql = "";

                //    foreach (DataGridViewRow r in dgw.Rows)
                //    {
                //        _sql += "SELECT postavi_kolicinu_sql_funkcija_prema_sifri('" + r.Cells["sifra"].FormattedValue.ToString() + "') AS odgovor; ";
                //    }

                //    //frmLoad l = new frmLoad();
                //    //l.Text = "Radim provjeru skladišta";
                //    //l.Show();
                //    classSQL.insert(_sql);
                //    //l.Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion Buttons

        #region Insert/Update Izdatnica/Stavke

        /// <summary>
        /// Sprema izdatnicu u bazu. Ako je došlo do greške zbog konkurentnosti ili
        /// nešto drugo vraća false
        /// </summary>
        /// <returns></returns>
        private bool SetIzdatnica()
        {
            string sql = "SELECT broj FROM izdatnica WHERE broj = '" + txtBroj.Text +
                "' AND id_skladiste='" + cbSkladiste.SelectedValue +
                "' AND godina='" + nmGodina.Value.ToString() + "'";
            DataTable DTbool = classSQL.select(sql, "izdatnica").Tables[0];

            if (DTbool.Rows.Count == 0)
            {
                InsertIzdatnica();

                SetIzdatnicaStavke();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    "Nova Izdatnica br." + txtBroj.Text + "/" + nmGodina.Value.ToString() + ", Skladište: " + cbSkladiste.SelectedValue);
                //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Izdatnica", txtBroj.Text, false);
                return true;
            }
            else
            {
                //ovaj prvi if znači sljedeće:
                //prvi korisnik je kliknuo novi zapis,
                //drugi korisnik je u međuvremenu kreirao i spremio u bazu isti zapis (zato je i DTbool.Rows.Count>0);
                //prvi korisnik je kliknuo spremi,
                //program dolazi do ovog našeg ifa. edit je postavljen na false jer je korisnik kliknuo na novi zapis
                if (!edit)
                {
                    MessageBox.Show("Drugi korisnik je već kreirao izdatnicu '" + txtBroj.Text + "/" + nmGodina.Value.ToString() + "'. " +
                        Environment.NewLine + Environment.NewLine +
                        "Pokušaj kreiranja izdatnice '" + (Convert.ToInt64(txtBroj.Text) + 1).ToString() + "/" + nmGodina.Value.ToString() + "'...", "UPOZORENJE!");

                    //kaj sad napraviti? dal da se učita nova primka i izbriše nesejvana primka koju je korisnik napravil?
                    //ili da proba spremiti primku s brojačem + 1, npr.:
                    spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj kreiranja izdatnice br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue);

                    txtBroj.Text = (Convert.ToInt64(txtBroj.Text) + 1).ToString();
                    SetIzdatnica();

                    return true;
                }

                if (!UpdateIzdatnica())
                {
                    MessageBox.Show("Drugi korisnik je već uredio ovu izdatnicu. " +
                        "Izdatnica '" + txtBroj.Text + "/" + nmGodina.Value.ToString() + "' s ažuriranim podacima bit će ponovo učitana!", "UPOZORENJE!");
                    spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj uređivanja Izdatnice br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue);

                    return false;
                }

                //treba riješiti konkurentnost i za stavke
                SetIzdatnicaStavke();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    "Uređivanje Izdatnice br." + txtBroj.Text + "/" + nmGodina.Value.ToString() + ", Skladište " + cbSkladiste.SelectedValue);
                //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Izdatnica", txtBroj.Text, true);

                return true;
            }
        }

        private void InsertIzdatnica()
        {
            string sql = "" +
             "INSERT INTO izdatnica (broj,godina,datum,id_mjesto,originalni_dokument," +
                "id_izradio,napomena,id_skladiste,id_partner) VALUES (" +
                 " '" + txtBroj.Text + "'," +
                 " '" + nmGodina.Value.ToString() + "'," +
                 " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                 " '" + cbMjesto.SelectedValue + "'," +
                 " '" + txtOrginalniDok.Text + "'," +
                 " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                 " '" + rtbNapomena.Text + "'," +
                 " '" + cbSkladiste.SelectedValue + "'," +
                 " '" + txtSifraOdrediste.Text + "')";
            provjera_sql(classSQL.insert(sql));

            //sad treba pokupiti id iz baze od nove izdatnice!!!
            DataTable DTtempIzdatnica = classSQL.select("SELECT id_izdatnica,broj FROM izdatnica WHERE" +
                " broj='" + txtBroj.Text + "' AND " +
                " godina='" + nmGodina.Value.ToString() + "' AND " +
                " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND " +
                " id_mjesto='" + cbMjesto.SelectedValue + "' AND " +
                " originalni_dokument='" + txtOrginalniDok.Text + "' AND " +
                " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "' AND " +
                " napomena='" + rtbNapomena.Text + "' AND " +
                " id_skladiste='" + cbSkladiste.SelectedValue + "' AND " +
                " id_partner='" + txtSifraOdrediste.Text + "';" +
                " ", "izdatnica").Tables[0];
            if (DTtempIzdatnica.Rows.Count > 0)
            {
                ID_IZDATNICA = DTtempIzdatnica.Rows[0]["id_izdatnica"].ToString();
                BROJ_IZDATNICA = DTtempIzdatnica.Rows[0]["broj"].ToString();
            }
        }

        /// <summary>
        /// Vraća true ako je izdatnica uspješno ažurirana, false ako je došlo do greške zbog konkurentnosti
        /// </summary>
        /// <returns></returns>
        private bool UpdateIzdatnica()
        {
            //prvo treba spremiti početne vrijednosti učitane primke (spremljene u globalni PRIMKA_IZ_BAZE)
            //zatim prije apdejtanja provjeriti da li su prethodno učitane
            //vrijednosti iz baze jednake trenutnim vrijednostima iz baze  (spremljene u globalni TRENUTNA_PRIMKA_IZ_BAZE)
            //ako jesu nastavi s apdejtom, ako nisu upozori korisnika da
            //ponovo učita podatke iz baze

            if (UsporediIzdatnice(IZDATNICA_IZ_BAZE))
            {
                string sql = "UPDATE izdatnica SET " +
                    " godina='" + nmGodina.Value.ToString() + "'," +
                    " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    " id_mjesto='" + cbMjesto.SelectedValue + "'," +
                    " originalni_dokument='" + txtOrginalniDok.Text + "'," +
                    " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                    " napomena='" + rtbNapomena.Text + "'," +
                    " editirano = '1'," +
                    " id_partner='" + txtSifraOdrediste.Text + "'" +
                    " WHERE broj='" + txtBroj.Text + "'" +
                    " AND id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                    " AND godina='" + nmGodina.Value.ToString() + "'";
                provjera_sql(classSQL.update(sql));

                return true;
            }
            else
            {
                //vrijednosti učitane izdatnice i izdatnice iz baze se ne poklapaju
                //tj. drugi korisnik je već izmjenio podatke
                return false;
            }
        }

        /// <summary>
        /// Uspoređuje zadanu izdatnicu s izdatnicom iz baze
        /// </summary>
        /// <param name="IZDATNICA_IZ_BAZE"></param>
        /// <returns></returns>
        private bool UsporediIzdatnice(DataTable IZDATNICA_IZ_BAZE)
        {
            string sql = "SELECT * FROM izdatnica WHERE broj = '" + txtBroj.Text +
                 "' AND id_skladiste = '" + cbSkladiste.SelectedValue +
                 "' AND godina= '" + nmGodina.Value.ToString() + "'";
            TRENUTNA_IZDATNICA_IZ_BAZE = classSQL.select(sql, "izdatnica").Tables[0];

            if (TRENUTNA_IZDATNICA_IZ_BAZE.Rows.Count < 1) return false;

            for (int i = 0; i < IZDATNICA_IZ_BAZE.Columns.Count; i++)
            {
                if (IZDATNICA_IZ_BAZE.Rows[0][i].ToString() != TRENUTNA_IZDATNICA_IZ_BAZE.Rows[0][i].ToString())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Ažurira stavke iz datagridviewa
        /// </summary>
        private void SetIzdatnicaStavke()
        {
            for (int i = 0; i < dgw.RowCount; i++)
            {
                DataGridViewRow r = dgw.Rows[i];
                //ako nema id onda je nova stavka pa ju treba samo ubaciti u bazu
                if (r.Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    IzdatnicaStavkeInsert(r);

                    SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "-");
                }
                else
                {
                    if (!IzdatnicaStavkeUpdate(r))
                    {
                        MessageBox.Show("Drugi korisnik je već uredio stavku s robom '" +
                            r.Cells["sifra"].FormattedValue.ToString() + "'. " +
                            "Navedena stavka neće biti spremljena! Nakon što program spremi izdatnicu sa svim " +
                            "ostalim stavkama, ponovo učitajte ovu izdatnicu za dodavanje novih stavaka.", "UPOZORENJE!");

                        spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj uređivanja Izdatnice br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue + ". Stavka: " +
                        r.Cells["sifra"].FormattedValue.ToString() + " (Konkurentnost!)");
                    }
                    else
                    {
                        //stavka je spremljena/apdejtana
                    }
                }
            }
        }

        private void IzdatnicaStavkeInsert(DataGridViewRow r)
        {
            string ssql = "INSERT INTO izdatnica_stavke (sifra,vpc,mpc,nbc,pdv,kolicina,rabat,ukupno,iznos,broj,id_izdatnica) VALUES (" +
                "'" + r.Cells["sifra"].FormattedValue.ToString() + "'," +
                "'" + r.Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["mpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["nbc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["pdv"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["kolicina"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + BROJ_IZDATNICA + "'," +
                "'" + ID_IZDATNICA + "')";
            provjera_sql(classSQL.insert(ssql));
        }

        /// <summary>
        /// Ako je stavka uspješno ažurirana vraća true, false inače
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool IzdatnicaStavkeUpdate(DataGridViewRow r)
        {
            string idStavka = r.Cells["id_stavka"].FormattedValue.ToString();

            if (UsporediIzdatnicaStavke(idStavka))
            {
                //treba povećati za prethodni iznos i tek onda smanjiti
                DataRow[] dr = STAVKE_IZ_BAZE.Select("id_stavka=" + idStavka);
                SetRoba(dr[0]["sifra"].ToString(), dr[0]["kolicina"].ToString().Trim().Replace(".", ","), "+");

                string ssql = "UPDATE izdatnica_stavke SET" +
                    " sifra='" + r.Cells["sifra"].FormattedValue.ToString() + "'," +
                    " vpc='" + r.Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " mpc='" + r.Cells["mpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " nbc='" + r.Cells["nbc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " pdv='" + r.Cells["pdv"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " kolicina='" + r.Cells["kolicina"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " rabat='" + r.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " ukupno='" + r.Cells["ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " iznos='" + r.Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " broj='" + txtBroj.Text + "'" +
                    " WHERE id_stavka='" + r.Cells["id_stavka"].FormattedValue.ToString() + "'";
                provjera_sql(classSQL.update(ssql));

                SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "-");

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UsporediIzdatnicaStavke(string idStavka)
        {
            string sql = "SELECT * FROM izdatnica_stavke WHERE id_stavka = '" + idStavka + "'";
            TRENUTNA_STAVKA_IZ_BAZE = classSQL.select(sql, "izdatnica").Tables[0];
            DataRow[] dr = STAVKE_IZ_BAZE.Select("id_stavka=" + idStavka);

            if (TRENUTNA_STAVKA_IZ_BAZE.Rows.Count < 1) return false;

            return TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["sifra"].ToString() == dr[0]["sifra"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["kolicina"].ToString() == dr[0]["kolicina"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["vpc"].ToString() == dr[0]["vpc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["nbc"].ToString() == dr[0]["nbc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["pdv"].ToString() == dr[0]["pdv"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["id_stavka"].ToString() == dr[0]["id_stavka"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["id_izdatnica"].ToString() == dr[0]["id_izdatnica"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["rabat"].ToString() == dr[0]["rabat"].ToString() &&
                //TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["mpc"].ToString() == dr[0]["mpc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["iznos"].ToString() == dr[0]["iznos"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["ukupno"].ToString() == dr[0]["ukupno"].ToString();
        }

        #endregion Insert/Update Izdatnica/Stavke

        #region Delete Stavka, Update skladište

        /// <summary>
        /// Briše stavke iz datagrida, iz baze i ažurira robu na skladištu
        /// </summary>
        //void DeleteStavkaRoba()
        //{
        //    string a = dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString();

        //    //stavka je tek dodana (još ne postoji u bazi), pa samo obrišemo iz datagrida
        //    if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value == null)
        //    {
        //        PregledKolicine();
        //        dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        //        MessageBox.Show("Obrisano.");
        //    }
        //    //stavka je već u bazi pa ju moramo izbrisati iz baze i ažurirati skladište
        //    else
        //    {
        //        if (MessageBox.Show("Brisanjem ove stavke vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //        {
        //            DataGridViewRow r = dgw.CurrentRow;

        //            deleteStavkaHelper(r.Index);
        //            PregledKolicine();
        //            dgw.Rows.RemoveAt(r.Index);
        //            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Izdatnica", txtBroj.Text, true);
        //        }
        //    }
        //}

        private void deleteStavkaHelper(int redak)
        {
            DataGridViewRow r = dgw.Rows[redak];

            string s = r.Cells["id_stavka"].FormattedValue.ToString();
            if (s.Length != 0)
            {
                SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "+");

                classSQL.delete("DELETE FROM izdatnica_stavke WHERE id_stavka='" + s + "'");
                //dgw.Rows.RemoveAt(redak);
            }
            spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "Brisanje stavke " + s + " sa povratnice robe br." + txtBroj.Text);
            MessageBox.Show("Obrisano.");
        }

        #endregion Delete Stavka, Update skladište

        #region Set roba na skladištu, addrowtodatagrid

        private void SetRoba(string sifra, string kolicina, string plusMinus)
        {
            //(trenutna kolicina=)kol = prijašnja kolicina + dodana kolicina
            string kol = SQL.ClassSkladiste.GetAmount(sifra,
                cbSkladiste.SelectedValue.ToString(),
                kolicina, "1", plusMinus);

            //update trenutna kolicina
            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(),
                kol, sifra);
        }

        private void addRowToDatagridview()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            DataGridViewRow r = dgw.Rows[br];
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];

            double vpc = 0;
            double.TryParse(DTRoba.Rows[0]["vpc"].ToString(), out vpc);
            double mpc = 0;
            double.TryParse(DTRoba.Rows[0]["mpc"].ToString(), out mpc);
            double nbc = 0;
            double.TryParse(DTRoba.Rows[0]["nc"].ToString(), out nbc);
            double pdv = 0;
            double.TryParse(DTRoba.Rows[0]["izlazni_porez"].ToString(), out pdv);

            r.Cells[0].Value = dgw.RowCount;
            r.Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            r.Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            r.Cells["vpc"].Value = vpc.ToString("#0.00");
            r.Cells["nbc"].Value = nbc.ToString("#0.00");
            r.Cells["rabat"].Value = "0";
            r.Cells["mpc"].Value = mpc.ToString("#0.00");
            r.Cells["pdv"].Value = pdv;
            r.Cells["kolicina"].Value = "1";
            r.Cells["ukupno"].Value = "0";
            r.Cells["izdatnicaID"].Value = ID_IZDATNICA;//null ako je nova izdatnica
            r.Cells["id_stavka"].Value = null;
        }

        private void addRoba()
        {
            addRowToDatagridview();

            dgw.BeginEdit(true);
        }

        #endregion Set roba na skladištu, addrowtodatagrid

        #region Fill forma

        private void FillIzdatnica(long broj_izdatnice, int broj_skladista)
        {
            BROJ_IZDATNICA = broj_izdatnice.ToString();
            EnableDisable(true);
            deleteFields();

            string sql = string.Format(@"SELECT *
FROM izdatnica
WHERE broj = '{0}' AND id_skladiste = '{1}';", broj_izdatnice, broj_skladista);

            DataTable DTheader = classSQL.select(sql, "izdatnica").Tables[0];

            IZDATNICA_IZ_BAZE = DTheader;

            if (DTheader.Rows.Count < 1)
            {
                EnableDisable(false);
                deleteFields();
                setTextBrojIzdatnice();
                edit = false;
                ControlDisableEnable(true, false, false, false, false, true);
                MessageBox.Show("U bazi ne postoji izdatnica " + broj_izdatnice + " na skladištu " + broj_skladista);
                return;
            }

            fillHeader(DTheader);

            ////za preslikavanje čitavog datatablea u datagrid
            ////nije dobro jer treba napraviti neke transformacije
            //dgw.DataSource = DT_stavke;

            fillDataGridView(DT_stavke);

            ControlDisableEnable(false, true, true, true, true, false);
            edit = true;
        }

        private void fillDataGridView(DataTable DT_stavke)
        {
            string sql = string.Format(@"SELECT
izdatnica_stavke.id_stavka,
izdatnica_stavke.sifra,
izdatnica_stavke.kolicina,
izdatnica_stavke.vpc,
izdatnica_stavke.nbc,
izdatnica_stavke.broj,
izdatnica_stavke.pdv,
izdatnica_stavke.rabat,
izdatnica_stavke.ukupno,
izdatnica_stavke.iznos,
izdatnica_stavke.id_izdatnica,
roba_prodaja.naziv
FROM izdatnica_stavke
left join izdatnica on izdatnica.id_izdatnica = izdatnica_stavke.id_izdatnica
LEFT JOIN roba_prodaja ON roba_prodaja.sifra = izdatnica_stavke.sifra and izdatnica.id_skladiste = roba_prodaja.id_skladiste
WHERE izdatnica_stavke.id_izdatnica = '" + ID_IZDATNICA + "'");

            DT_stavke = classSQL.select(sql, "izdatnica_stavke").Tables[0];

            STAVKE_IZ_BAZE = DT_stavke;

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                DataGridViewRow r = dgw.Rows[br];
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[4];

                double kolicina = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["kolicina"].ToString().Replace(".", ",")), 3);
                double vpc = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["vpc"].ToString()), 3);
                double pdv = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["pdv"].ToString().Replace(".", ",")), 2);
                double iznos = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["iznos"].ToString()), 2);
                double ukupno = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["ukupno"].ToString()), 2);
                double nbc = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()), 2);

                r.Cells[0].Value = dgw.RowCount;
                r.Cells["sifra"].Value = DT_stavke.Rows[i]["sifra"].ToString();
                r.Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                r.Cells["kolicina"].Value = kolicina.ToString("#0.000");
                r.Cells["vpc"].Value = vpc.ToString("#0.000");
                r.Cells["nbc"].Value = nbc.ToString("#0.00");
                r.Cells["pdv"].Value = pdv.ToString("#0.00");
                r.Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                r.Cells["izdatnicaID"].Value = DT_stavke.Rows[i]["id_izdatnica"].ToString();
                r.Cells["rabat"].Value = DT_stavke.Rows[i]["rabat"].ToString().Replace(".", ",");
                r.Cells["mpc"].Value = Math.Round((vpc + (vpc * pdv / 100)), 2).ToString("#0.00");
                r.Cells["ukupno"].Value = ukupno.ToString("#0.00");
                r.Cells["iznos"].Value = iznos.ToString("#0.00");
            }

            dgw.Columns["ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void fillHeader(DataTable DTheader)
        {
            nmGodina.Value = Convert.ToInt16(DTheader.Rows[0]["godina"].ToString());
            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            cbSkladiste.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_skladiste"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            cbMjesto.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_mjesto"].ToString());
            txtOrginalniDok.Text = DTheader.Rows[0]["originalni_dokument"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            setTextIzradio(DTheader.Rows[0]["id_izradio"].ToString());
            txtSifraOdrediste.Text = DTheader.Rows[0]["id_partner"].ToString();

            //napuni txtSifra
            if (txtSifraOdrediste.Text.Trim() != "")
                txtSifraOdrediste_KeyDown(txtSifraOdrediste, new KeyEventArgs(Keys.Enter));

            ID_IZDATNICA = DTheader.Rows[0]["id_izdatnica"].ToString();
        }

        #endregion Fill forma

        #region Util

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojIzdatnice()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM izdatnica WHERE id_skladiste='" + cbSkladiste.SelectedValue + "'", "izdatnica").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void setTextBrojIzdatnice()
        {
            txtBroj.Text = brojIzdatnice();
        }

        private void setSkladiste()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
            cbSkladiste.SelectedValue = Class.Postavke.id_default_skladiste;
        }

        private void setGodina()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = DateTime.Now.Year;
        }

        private void setMjesto()
        {
            DataTable DT_Mjesto;
            DT_Mjesto = classSQL.select("SELECT * FROM grad order by grad", "grad").Tables[0];
            cbMjesto.DataSource = DT_Mjesto;
            cbMjesto.DisplayMember = "grad";
            cbMjesto.ValueMember = "id_grad";
            cbMjesto.SelectedValue = Class.PodaciTvrtka.gradPoslovnicaId;
        }

        private void setDefault()
        {
            setSkladiste();
            setGodina();
            setMjesto();
        }

        private void selectRobaHelper(string sifra)
        {
            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                {
                    MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string sql = string.Format(@"SELECT *
FROM roba_prodaja
WHERE sifra = '{0}' AND id_skladiste = '{1}';",
sifra, cbSkladiste.SelectedValue);

            DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                txtSifra_robe.BackColor = Color.White;
                addRoba();
            }
            else
            {
                MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setTextIzradio(string id_zaposlenik)
        {
            txtIzradio.Text = getIzradio(id_zaposlenik);
        }

        private string getIzradio(string id_zaposlenik)
        {
            return classSQL.select("SELECT concat(ime, ' ', prezime) as Ime  FROM zaposlenici WHERE id_zaposlenik='" +
                id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void spremiAktivnostZaposlenika(string id, string datum, string radnja)
        {
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" +
            id + "','" + datum + "','" + radnja + "')"));
        }

        private void ControlDisableEnable(bool novi, bool odustani, bool spremi, bool ispis, bool obrisi, bool sve)
        {
            btnNoviUnos.Enabled = novi;

            btnOdustani.Enabled = odustani;

            btnSpremi.Enabled = spremi;

            btnIspis.Enabled = ispis;

            btnDeleteAllFaktura.Enabled = obrisi;

            btnSveFakture.Enabled = sve;
        }

        private void deleteFields()
        {
            dtpDatum.Text = "";
            rtbNapomena.Text = "";
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            txtOrginalniDok.Text = "";
            txtSifra_robe.Text = "";
            dgw.Rows.Clear();
        }

        private void EnableDisable(bool x)
        {
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            cbMjesto.Enabled = x;
            txtOrginalniDok.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnObrisi.Enabled = x;
            pictureBox1.Enabled = x;

            txtBroj.Enabled = !x;
            nmGodina.Enabled = !x;
            cbSkladiste.Enabled = !x;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        #endregion Util

        #region DataGridView helpers

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count > 0)
                dgw.BeginEdit(true);
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        /// <summary>
        /// Sređuje cijene u retku datagrida. Određuje iznos i ukupan iznos prema količini, veleprodajnoj cijeni i porezu
        /// </summary>
        /// <param name="red"></param>
        private void izracun(int red)
        {
            Double dec_parse;
            if (!Double.TryParse(dgw.Rows[red].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["kolicina"].Value = "1";
                MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
            }
            if (!Double.TryParse(dgw.Rows[red].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["rabat"].Value = "0";
                MessageBox.Show("Greška kod upisa rabata.", "Greška"); return;
            }
            if (!Double.TryParse(dgw.Rows[red].Cells["vpc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["vpc"].Value = "0,000";
                MessageBox.Show("Greška kod upisa veleprodajne cijene.", "Greška"); return;
            }
            if (!Double.TryParse(dgw.Rows[red].Cells["mpc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["mpc"].Value = "0,00";
                MessageBox.Show("Greška kod upisa maloprodajne cijene.", "Greška"); return;
            }
            if (!Double.TryParse(dgw.Rows[red].Cells["pdv"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["pdv"].Value = "0,00";
                MessageBox.Show("Greška kod upisa pdv-a.", "Greška"); return;
            }
            if (!Double.TryParse(dgw.Rows[red].Cells["nbc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["nbc"].Value = "0,00";
                MessageBox.Show("Greška kod upisa nabavne cijene.", "Greška"); return;
            }

            try
            {
                dgw.Rows[red].Cells["vpc"].Value = dgw.Rows[red].Cells["vpc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["kolicina"].Value = dgw.Rows[red].Cells["kolicina"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["pdv"].Value = dgw.Rows[red].Cells["pdv"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["mpc"].Value = dgw.Rows[red].Cells["mpc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["nbc"].Value = dgw.Rows[red].Cells["nbc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["rabat"].Value = dgw.Rows[red].Cells["rabat"].Value.ToString().Replace(".", ",");

                //provjera dal su podaci numerički
                //ako nisu dolazi u catch
                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["vpc"].Value), 3);
                double kol = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["kolicina"].Value.ToString().Replace(".", ",")), 3);
                double pdv = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["pdv"].Value.ToString().Replace(".", ",")), 2);
                double mpc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["mpc"].Value.ToString().Replace(".", ",")), 2);
                double nbc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["nbc"].Value.ToString().Replace(".", ",")), 2);
                double rabat = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["rabat"].Value.ToString().Replace(".", ",")), 2);

                double iznosBezPdv, vpcUkupno, mpcUkupno;

                if (dgw.CurrentCell.ColumnIndex == 6)
                {
                    //izračun mpc -> vpc
                    mpcUkupno = mpc * kol;
                    vpcUkupno = mpcUkupno * (1 - rabat / 100);
                    iznosBezPdv = vpcUkupno / (1 + pdv / 100);
                    vpc = mpc / (1 + pdv / 100);
                    //izračun
                }
                else
                {
                    //izračun vpc -> mpc
                    mpc = vpc * (1 + pdv / 100);
                    vpcUkupno = vpc * kol * (1 - rabat / 100);
                    mpcUkupno = mpc * kol * (1 - rabat / 100);
                    //izračun
                }

                dgw.Rows[red].Cells["ukupno"].Value = Math.Round(mpcUkupno, 2).ToString("#0.00");
                dgw.Rows[red].Cells["iznos"].Value = Math.Round(vpcUkupno, 2).ToString("#0.00");
                dgw.Rows[red].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
                dgw.Rows[red].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");

                //moveToCellHelper(dgw.CurrentCell.ColumnIndex, true);
            }
            catch
            {
            }

            dgw.Refresh();
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex >= 4 && dgw.CurrentCell.ColumnIndex <= 7)
            {
            }
            else if (dgw.CurrentCell.ColumnIndex == 8)
            {
                txtSifra_robe.Text = "";
                txtSifra_robe.Select();
                dgw.ClearSelection();
            }

            izracun(dgw.CurrentCell.RowIndex);
        }

        private void moveToCellHelper(int i, bool t)
        {
            try
            {
                dgw.CurrentCell = t ? dgw.CurrentRow.Cells[i + 1] : dgw.CurrentRow.Cells[i];
                dgw.BeginEdit(true);
            }
            catch (Exception)
            {
            }
        }

        #endregion DataGridView helpers

        #region ON_KEY_DOWN

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
                        }
                    }
                    else
                    {
                        txtSifraOdrediste.Select();
                        return;
                    }
                }

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataTable DT = classSQL.select("SELECT broj FROM izdatnica WHERE broj='" + txtBroj.Text +
                    "' AND id_skladiste = '" + cbSkladiste.SelectedValue + "'", "izdatnica").Tables[0];

                deleteFields();

                if (DT.Rows.Count == 0)
                {
                    if ((Convert.ToInt16(brojIzdatnice()) - 1).ToString() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(false, true, true, false, false, false);

                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_izdatnice = Convert.ToInt32(txtBroj.Text);
                    broj_skladista = Convert.ToInt32(cbSkladiste.SelectedValue);
                    FillIzdatnica(broj_izdatnice, broj_skladista);
                    EnableDisable(true);
                    cbSkladiste.Select();
                    ControlDisableEnable(false, true, true, true, false, false);

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraOdrediste.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifraOdredista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is RichTextBox)
            {
                RichTextBox control = ((RichTextBox)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
            else if (sender is RichTextBox)
            {
                RichTextBox control = ((RichTextBox)sender);
                control.BackColor = Color.White;
            }
        }

        private void KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        #endregion ON_KEY_DOWN
    }
}