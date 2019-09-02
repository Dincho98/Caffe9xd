using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RESORT.Forme
{
    public partial class frmUpisGosta : Form
    {
        public frmUpisGosta()
        {
            InitializeComponent();
            nuGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
        }

        private decimal broj_nocenja = 0;
        private DataTable DTBojeForme;
        private string broj_upisa = "";
        private bool LoadCompleted = false;

        public string __OD_datuma { get; set; }
        public string __DO_datuma { get; set; }
        public string __soba { get; set; }
        public string __godina { get; set; }

        //ako ide iz rezervacije
        public string __ime_gosta { get; set; }

        public string __vrsta_usluge { get; set; }
        public string __drzava { get; set; }
        public string __broj_osobne { get; set; }
        public string __broj_putovnice { get; set; }

        private DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];

        public string broj_unosa { get; set; }

        private void frmUpisGosta_Load(object sender, EventArgs e)
        {
            DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

            txtBrojDokumenta.Select();
            SetComboBox();
            broj_upisa = BrojDokumenta();
            txtBrojDokumenta.Text = broj_upisa;
            txtAvans.Text = "0";
            LoadCompleted = true;
            PrvoPokretanje();
            if (broj_unosa != null) { txtBrojDokumenta.Text = broj_unosa; Fill(); }
            if (__soba != null) { SetNew(); }
            Paint += new PaintEventHandler(Funkcije.Form1_Paint);
        }

        private void SetNew()
        {
            dtpDatDolaska.Value = Convert.ToDateTime(__OD_datuma);
            dtpDatOdlaska.Value = Convert.ToDateTime(__DO_datuma);
            try
            {
                nuGodina.Value = Convert.ToInt16(__godina);
                broj_upisa = BrojDokumenta();
                txtBrojDokumenta.Text = broj_upisa;
            }
            catch
            {
            }

            DataTable DT = RemoteDB.select("SELECT id FROM sobe WHERE naziv_sobe='" + __soba + "'", "sobe").Tables[0];

            try
            {
                cbSoba.SelectedValue = DT.Rows[0][0].ToString();
            }
            catch { }

            if (__ime_gosta != null)
            {
                txtImePrezime.Text = __ime_gosta;
                cbVrstaUsluge.SelectedValue = __vrsta_usluge;
                cbDrzava.SelectedValue = __drzava;
                txtBrojPutovnice.Text = __broj_putovnice;
                txtBrojOsobne.Text = __broj_osobne;
            }
        }

        private void PrvoPokretanje()
        {
            DataTable DTt = RemoteDB.select("SELECT * FROM boravisna_pristojba WHERE id='" + cbBorProstojba.SelectedValue + "'", "boravisna_pristojba").Tables[0];
            if (DTt.Rows.Count > 0)
            {
                txtIznosBorPristojbe.Text = DTt.Rows[0]["iznos"].ToString();
            }
            else
            {
                txtIznosBorPristojbe.Text = "0";
            }

            string sql = "SELECT r_cijenasoba.cijena_nocenja,valute.tecaj FROM r_cijenasoba " +
                 " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                 " WHERE id_soba='" + cbSoba.SelectedValue.ToString() + "'" +
                 " AND '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'>=r_cijenasoba.od_datuma " +
                 " AND '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'<=r_cijenasoba.do_datuma;";

            DataTable DTss = RemoteDB.select(sql, "sobe").Tables[0];
            if (DTss.Rows.Count > 0)
            {
                //txtCijena.Text = Math.Round((Convert.ToDecimal(DTss.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTss.Rows[0]["tecaj"].ToString())), 3).ToString("#0.000");
                cijena_sobe = Math.Round((Convert.ToDecimal(DTss.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTss.Rows[0]["tecaj"].ToString())), 3);
            }
            else
            {
                if (__soba == null)
                    MessageBox.Show("Za današnji dan nemate zadanu cijenu za navedenu sobu.");

                cijena_sobe = 0;
            }

            DataTable DTu = RemoteDB.select("SELECT * FROM vrsta_usluge WHERE id='" + cbVrstaUsluge.SelectedValue + "'", "vrsta_usluge").Tables[0];
            if (DTu.Rows.Count > 0)
            {
                txtCijenaUsluge.Text = DTu.Rows[0]["iznos"].ToString();
            }

            RacunajStavku();
            RacunajUkupno();
        }

        private string BrojDokumenta()
        {
            DataTable DSbr = RemoteDB.select("SELECT MAX(broj) FROM unos_gosta WHERE godina='" + nuGodina.Value.ToString() + "'", "unos_gosta").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()))))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()))))), ((int)(((byte)(Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()))))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetComboBox()
        {
            DataTable DTvrsta_gosta = classDBlite.LiteSelect("SELECT * FROM vrsta_gosta", "vrsta_gosta").Tables[0];
            cbVrsteGosta.DataSource = DTvrsta_gosta;
            cbVrsteGosta.DisplayMember = "vrsta_gosta";
            cbVrsteGosta.ValueMember = "id";

            DataTable DTgencija = RemoteDB.select("SELECT * FROM agencija", "agencija").Tables[0];
            cbAgencija.DataSource = DTgencija;
            cbAgencija.DisplayMember = "ime_agencije";
            cbAgencija.ValueMember = "id";

            DataTable DTtip_sobe = RemoteDB.select("SELECT * FROM tip_sobe", "tip_sobe").Tables[0];
            cbTipSoba.DataSource = DTtip_sobe;
            cbTipSoba.DisplayMember = "tip";
            cbTipSoba.ValueMember = "id";
            cbTipSoba.SelectedValue = "1";

            DataTable DTdrzava = RemoteDB.select("SELECT * FROM zemlja", "zemlja").Tables[0];
            cbDrzava.DataSource = DTdrzava;
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "country_code";
            cbDrzava.SelectedValue = "HR";

            DataSet DSs = RemoteDB.select("SELECT id,broj_sobe, naziv_sobe FROM sobe WHERE aktivnost='1'", "sobe");
            DataTable DTsoba = DSs.Tables[0];
            cbSoba.DataSource = DTsoba;
            cbSoba.DisplayMember = "naziv_sobe";
            cbSoba.ValueMember = "id";
            txtBrojSobe.DataBindings.Add("Text", DSs.Tables[0], "broj_sobe");

            DataTable DTvrsta_usluge = RemoteDB.select("SELECT * FROM vrsta_usluge WHERE aktivnost='1'", "vrsta_usluge").Tables[0];
            cbVrstaUsluge.DataSource = DTvrsta_usluge;
            cbVrstaUsluge.DisplayMember = "naziv_usluge";
            cbVrstaUsluge.ValueMember = "id";

            DataTable DTboravisna_pristojba = RemoteDB.select("SELECT * FROM boravisna_pristojba", "boravisna_pristojba").Tables[0];
            cbBorProstojba.DataSource = DTboravisna_pristojba;
            cbBorProstojba.DisplayMember = "boravisna_pristojba";
            cbBorProstojba.ValueMember = "id";

            nuGodina.Value = DateTime.Now.Year;
        }

        private decimal ukupno_unos = 0;
        private decimal sve_ukupno = 0;

        private void btnDodajNaPopis_Click(object sender, EventArgs e)
        {
            int dorucak = 0;
            int rucak = 0;
            int vecera = 0;
            if (chbDorucak.Checked) { dorucak = 1; }
            if (chbRucak.Checked) { rucak = 1; }
            if (chbVecera.Checked) { vecera = 1; }

            if (broj_nocenja == 0) { MessageBox.Show("Niste pravilno odabrali datume.", "Upozorenje"); return; }

            if (String.IsNullOrEmpty(txtImePrezime.Text))
            {
                if (MessageBox.Show("Dali ste sigurni da ne želite upisati ime ili prezime gosta?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            if (String.IsNullOrEmpty(txtBrojPutovnice.Text) && String.IsNullOrEmpty(txtBrojOsobne.Text))
            {
                //MessageBox.Show("Za spremanje novog gosta obavezno upišite broj osobne ili broj putovnice!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                txtBrojPutovnice.Text = "0";
            }

            decimal dec;
            if (!decimal.TryParse(txtAvans.Text, out dec))
            {
                MessageBox.Show("Greška, niste upisali pravilno avans.");
                return;
            }

            dgv.Rows.Add(
                txtImePrezime.Text,
                txtAdresa.Text,
                dtpDatDolaska.Value,
                dtpDatOdlaska.Value,
                cbSoba.Text,
                txtAvans.Text,
                txtIznosBorPristojbe.Text,
                txtCijenaUsluge.Text,
                DTpostavke.Rows[0]["pdv_nocenje"].ToString(),
                (ukupno_stavka).ToString("#0.00"),
                txtBrojOsobne.Text,
                txtBrojPutovnice.Text,
                cbAgencija.SelectedValue,
                cbSoba.SelectedValue,
                cbTipSoba.SelectedValue,
                cbVrstaUsluge.SelectedValue,
                dorucak,
                rucak,
                vecera,
                txtNapomena.Text,
                txtPopust.Text,
                cbBorProstojba.SelectedValue,
                cbVrsteGosta.SelectedValue,
                cbDrzava.SelectedValue,
                dtpRodenje.Value,
                cijena_sobe_prosjek / broj_nocenja,
                ""
                );

            RacunajUkupno();
            NaplatiZaCijeluSobu();
        }

        private void NaplatiZaCijeluSobu()
        {
            if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
            {
                //////////////////////////////////////////////////IZRAČUNAVA CIJENU SOBE//////////////////////////////////////
                int br_unosa = 0;
                for (int z = 0; z < dgv.RowCount; z++)
                {
                    if (dgv.Rows[z].Cells["id_soba"].FormattedValue.ToString() == cbSoba.SelectedValue.ToString())
                    {
                        br_unosa++;
                    }
                }

                decimal cc_sobe = 0;
                decimal rrabat = 0;
                decimal iiznosBP = 0;
                decimal iznosVS = 0;
                for (int z = 0; z < dgv.RowCount; z++)
                {
                    if (dgv.Rows[z].Cells["id_soba"].FormattedValue.ToString() == cbSoba.SelectedValue.ToString())
                    {
                        decimal dec;
                        if (!decimal.TryParse(dgv.Rows[z].Cells["iznosBP"].FormattedValue.ToString(), out dec))
                        {
                            dgv.Rows[z].Cells["iznosBP"].Value = "0";
                        }
                        if (!decimal.TryParse(dgv.Rows[z].Cells["avans"].FormattedValue.ToString(), out dec))
                        {
                            dgv.Rows[z].Cells["avans"].Value = "0";
                        }
                        if (!decimal.TryParse(dgv.Rows[z].Cells["IznosVS"].FormattedValue.ToString(), out dec))
                        {
                            dgv.Rows[z].Cells["IznosVS"].Value = "0";
                        }
                        if (!decimal.TryParse(dgv.Rows[z].Cells["cijena_sobe_dgv"].FormattedValue.ToString(), out dec))
                        {
                            dgv.Rows[z].Cells["cijena_sobe_dgv"].Value = "0";
                        }

                        ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;

                        cc_sobe = Convert.ToDecimal(dgv.Rows[z].Cells["cijena_sobe_dgv"].FormattedValue.ToString());
                        iiznosBP = Convert.ToDecimal(dgv.Rows[z].Cells["iznosBP"].FormattedValue.ToString());
                        iznosVS = Convert.ToDecimal(dgv.Rows[z].Cells["IznosVS"].FormattedValue.ToString());

                        cc_sobe = cc_sobe / br_unosa;

                        dgv.Rows[z].Cells["ukupno"].Value = (cc_sobe - (((cc_sobe) * rrabat) / 100)) + iznosVS + iiznosBP;
                    }
                }

                RacunajUkupno();
            }

            DateTime OD = dtpDatDolaska.Value;
            DateTime DO = dtpDatOdlaska.Value;
            broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

            ukupno_stavka = 0;
            cijena_sobe_prosjek = 0;

            rabat_stavka = Convert.ToDecimal(txtPopust.Text);
            avans_ukupno = Convert.ToDecimal(txtAvans.Text);
            vrsta_usluge = Convert.ToDecimal(txtCijenaUsluge.Text);
            bor_pristojba = Convert.ToDecimal(txtIznosBorPristojbe.Text);

            if (cbSoba.Items.Count == 0) { MessageBox.Show("Greška.\r\nKrivo odabrana soba ili krivo odabrani tip sobe."); return; }

            DateTime dat = OD;
            for (int i = 0; i < broj_nocenja; i++)
            {
                string sql = "SELECT r_cijenasoba.cijena_nocenja,valute.tecaj,r_cijenasoba.od_datuma,r_cijenasoba.do_datuma FROM r_cijenasoba " +
                " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                " WHERE id_soba='" + cbSoba.SelectedValue.ToString() + "'" +
                " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'>=r_cijenasoba.od_datuma " +
                " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'<=r_cijenasoba.do_datuma";

                DataTable DTcijene = RemoteDB.select(sql, "r_cijenasoba").Tables[0];

                if (DTcijene.Rows.Count > 0)
                {
                    cijena_sobe = Convert.ToDecimal(DTcijene.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTcijene.Rows[0]["tecaj"].ToString());
                    try
                    {
                        //cijena_sobe = Convert.ToDecimal(txtCijena.Text);
                    }
                    catch { cijena_sobe = 0; }
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
                    cijena_sobe_prosjek += cijena_sobe;
                    dat = dat.AddDays(1);
                }
                else
                {
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
                    cijena_sobe_prosjek += cijena_sobe;
                    dat = dat.AddDays(1);
                }
            }

            //---------------------------------------------------------------------------------------------------------
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate nijednu stavku za spremiti.", "Upozorenje");
            }
            txtBrojDokumenta.Enabled = true;

            foreach (DataGridViewRow row in this.dgv.Rows)
            {
                if (row.Cells["id_unos"].FormattedValue.ToString() == "")
                {
                    string sql = "INSERT INTO unos_gosta (porez,cijena_sobe,iznos_vu,iznos_bor_pristojbe,popust,ukupno,broj,godina,ime_gosta,adresa,broj_osobne,broj_putovnice,datum_dolaska,datum_odlaska,datum_rodenja,id_agencija,id_soba," +
                        "id_vrsta_gosta,id_tip_sobe,id_vrsta_usluge,id_boravisna_pristojba,id_drzava,avans,dorucak,vrijeme_unosa,rucak,vecera,napomena) VALUES (" +
                        "@porez,@cijena_sobe,@iznos_vu,@iznos_bor_pristojbe,@popust,@ukupno,@broj,@godina,@ime_gosta,@adresa,@broj_osobne,@broj_putovnice,@datum_dolaska,@datum_odlaska,@datum_rodenja,@id_agencija,@id_soba," +
                        "@id_vrsta_gosta,@id_tip_sobe,@id_vrsta_usluge,@id_boravisna_pristojba,@id_drzava,@avans,@dorucak,@vrijeme_unosa,@rucak,@vecera,@napomena)";

                    if (RemoteDB.remoteConnection.State.ToString() == "Closed") { RemoteDB.remoteConnection.Open(); }
                    NpgsqlCommand command = RemoteDB.remoteConnection.CreateCommand();

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@cijena_sobe", row.Cells["cijena_sobe_dgv"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@iznos_vu", row.Cells["iznosVS"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@iznos_bor_pristojbe", row.Cells["IznosBP"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@ukupno", row.Cells["ukupno"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@popust", row.Cells["popust"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_tip_sobe", row.Cells["id_tip_sobe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@napomena", row.Cells["napomena"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj", txtBrojDokumenta.Text);
                    command.Parameters.AddWithValue("@godina", nuGodina.Value);
                    command.Parameters.AddWithValue("@id_vrsta_gosta", row.Cells["id_vrsta_gosta"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@ime_gosta", row.Cells["imeprezime"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@adresa", row.Cells["adresa"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_osobne", row.Cells["broj_osobne"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_putovnice", row.Cells["broj_putovnice"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_dolaska", row.Cells["datum_dolaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_odlaska", row.Cells["datum_odlaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_rodenja", row.Cells["datum_rodenja"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_agencija", row.Cells["id_agencija"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_soba", row.Cells["id_soba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_tip_sobe", row.Cells["id_tip_sobe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vrijeme_unosa", DateTime.Now);
                    command.Parameters.AddWithValue("@id_vrsta_usluge", row.Cells["id_vrsta_usluge"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_boravisna_pristojba", row.Cells["id_boravisna_pristojba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_drzava", row.Cells["id_drzava"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@avans", row.Cells["avans"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@dorucak", row.Cells["dorucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@rucak", row.Cells["rucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vecera", row.Cells["vecera"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@porez", row.Cells["tb"].FormattedValue.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    string sql = "UPDATE unos_gosta SET cijena_sobe=@cijena_sobe,iznos_vu=@iznos_vu,iznos_bor_pristojbe=@iznos_bor_pristojbe, " +
                        " popust=@popust,ukupno=@ukupno,broj=@broj,godina=@godina,ime_gosta=@ime_gosta,adresa=@adresa,broj_osobne=@broj_osobne," +
                        " broj_putovnice=@broj_putovnice,porez=@porez,datum_dolaska=@datum_dolaska,datum_odlaska=@datum_odlaska,datum_rodenja=@datum_rodenja," +
                        " id_vrsta_gosta=@id_vrsta_gosta,id_agencija=@id_agencija,id_soba=@id_soba,id_vrsta_usluge=@id_vrsta_usluge,id_boravisna_pristojba=@id_boravisna_pristojba," +
                        " id_drzava=@id_drzava,id_tip_sobe=@id_tip_sobe,avans=@avans,dorucak=@dorucak,vrijeme_unosa=@vrijeme_unosa,rucak=@rucak,vecera=@vecera,napomena=@napomena " +
                        " WHERE id=@id";

                    if (RemoteDB.remoteConnection.State.ToString() == "Closed") { RemoteDB.remoteConnection.Open(); }
                    NpgsqlCommand command = RemoteDB.remoteConnection.CreateCommand();

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@cijena_sobe", row.Cells["cijena_sobe_dgv"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@iznos_vu", row.Cells["iznosVS"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@iznos_bor_pristojbe", row.Cells["IznosBP"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@ukupno", row.Cells["ukupno"].FormattedValue.ToString().Replace(",", "."));
                    command.Parameters.AddWithValue("@popust", row.Cells["popust"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj", txtBrojDokumenta.Text);
                    command.Parameters.AddWithValue("@id_tip_sobe", row.Cells["id_tip_sobe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@napomena", row.Cells["napomena"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@godina", nuGodina.Value);
                    command.Parameters.AddWithValue("@id_vrsta_gosta", row.Cells["id_vrsta_gosta"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@ime_gosta", row.Cells["imeprezime"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@adresa", row.Cells["adresa"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_osobne", row.Cells["broj_osobne"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@broj_putovnice", row.Cells["broj_putovnice"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_dolaska", row.Cells["datum_dolaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_odlaska", row.Cells["datum_odlaska"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@datum_rodenja", row.Cells["datum_rodenja"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@porez", row.Cells["tb"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_agencija", row.Cells["id_agencija"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_soba", row.Cells["id_soba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_tip_sobe", row.Cells["id_tip_sobe"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vrijeme_unosa", DateTime.Now);
                    command.Parameters.AddWithValue("@id_vrsta_usluge", row.Cells["id_vrsta_usluge"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_boravisna_pristojba", row.Cells["id_boravisna_pristojba"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id_drzava", row.Cells["id_drzava"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@avans", row.Cells["avans"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@dorucak", row.Cells["dorucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@rucak", row.Cells["rucak"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@vecera", row.Cells["vecera"].FormattedValue.ToString());
                    command.Parameters.AddWithValue("@id", row.Cells["id_unos"].FormattedValue.ToString());
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Spremljeno", "Spremljeno");
            if (dgv.Rows.Count > 0)
                dgv.Rows.Clear();
        }

        private void txtImePrezime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    if (txt.Name == "txtBrojSobe")
                    {
                        DataSet DSs = RemoteDB.select("SELECT id FROM sobe WHERE broj_sobe='" + txtBrojSobe.Text + "'", "sobe");
                        if (DSs.Tables[0].Rows.Count > 0)
                        {
                            cbSoba.SelectedValue = txt.Text;
                        }
                        else
                        {
                            MessageBox.Show("Upisali ste krivi broj sobe.", "Upozorenje");
                        }
                    }

                    if (txt.Name == "txtCijena")
                    {
                        try
                        {
                            //cijena_sobe = Math.Round(Convert.ToDecimal(txtCijena.Text), 3);
                            RacunajStavku();
                        }
                        catch (Exception) { cijena_sobe = 0; }
                    }
                }

                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void txtImePrezime_Enter(object sender, EventArgs e)
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
        }

        private void txtImePrezime_Leave(object sender, EventArgs e)
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
                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
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
        }

        private void btnObrisiOznacenog_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                if (MessageBox.Show("Dali ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgv.CurrentRow.Cells["id_unos"].FormattedValue.ToString() != "")
                    {
                        RemoteDB.delete("DELETE FROM unos_gosta WHERE id='" + dgv.CurrentRow.Cells["id_unos"].FormattedValue.ToString() + "'");
                    }

                    dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                    RacunajUkupno();
                    MessageBox.Show("Obrisano");
                }
            }
        }

        private void cbTipSoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadCompleted)
            {
                if (cbTipSoba.SelectedValue.ToString() == "1")
                {
                    DataSet DSs1 = RemoteDB.select("SELECT id,broj_sobe, naziv_sobe FROM sobe WHERE aktivnost='1'", "sobe");
                    DataTable DTsoba1 = DSs1.Tables[0];
                    cbSoba.DataSource = DTsoba1;
                    cbSoba.DisplayMember = "naziv_sobe";
                    cbSoba.ValueMember = "id";
                    txtBrojSobe.DataBindings.Clear();
                    txtBrojSobe.DataBindings.Add("Text", DSs1.Tables[0], "broj_sobe");
                    return;
                }

                DataSet DSs = RemoteDB.select("SELECT id,broj_sobe, naziv_sobe FROM sobe WHERE id_tip_sobe='" + cbTipSoba.SelectedValue.ToString() + "' AND aktivnost='1'", "sobe");
                DataTable DTsoba = DSs.Tables[0];
                cbSoba.DataSource = DTsoba;
                cbSoba.DisplayMember = "naziv_sobe";
                cbSoba.ValueMember = "id";
                txtBrojSobe.DataBindings.Clear();
                txtBrojSobe.DataBindings.Add("Text", DSs.Tables[0], "broj_sobe");
            }
        }

        private void cbSoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadCompleted)
            {
                string sql = string.Format(@"SELECT r_cijenasoba.cijena_nocenja, valute.tecaj
FROM r_cijenasoba
LEFT JOIN valute ON valute.id_valuta = r_cijenasoba.id_valuta
WHERE id_soba = '{0}' AND '{1}' >= r_cijenasoba.od_datuma AND '{2}' <= r_cijenasoba.do_datuma;",
    cbSoba.SelectedValue.ToString(),
    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));

                DataTable DTt = RemoteDB.select(sql, "sobe").Tables[0];
                if (DTt.Rows.Count > 0)
                {
                    //txtCijena.Text = Math.Round((Convert.ToDecimal(DTt.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTt.Rows[0]["tecaj"].ToString())), 3).ToString("#0.000");
                    cijena_sobe = Math.Round((Convert.ToDecimal(DTt.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTt.Rows[0]["tecaj"].ToString())), 3);
                }
                else
                {
                    MessageBox.Show("Za današnji dan nemate zadanu cijenu za navedenu sobu.");
                    cijena_sobe = 0;
                    //txtCijena.Text = "0";
                }
                RacunajStavku();
                RacunajUkupno();
            }
        }

        private decimal cijena_sobe = 0;
        private decimal cijena_sobe_prosjek = 0;
        private decimal rabat_stavka = 0;
        private decimal avans_ukupno = 0;
        private decimal ukupno_stavka = 0;
        private decimal vrsta_usluge = 0;
        private decimal bor_pristojba = 0;

        private void RacunajStavku()
        {
            decimal dec;
            if (!decimal.TryParse(txtAvans.Text, out dec))
            {
                txtAvans.Text = "0";
            }
            if (!decimal.TryParse(txtPopust.Text, out dec))
            {
                txtPopust.Text = "0";
            }
            if (!decimal.TryParse(txtCijenaUsluge.Text, out dec))
            {
                txtCijenaUsluge.Text = "0";
            }
            if (!decimal.TryParse(txtIznosBorPristojbe.Text, out dec))
            {
                txtIznosBorPristojbe.Text = "0";
            }

            DateTime OD = dtpDatDolaska.Value;
            DateTime DO = dtpDatOdlaska.Value;
            broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

            ukupno_stavka = 0;
            cijena_sobe_prosjek = 0;

            rabat_stavka = Convert.ToDecimal(txtPopust.Text);
            avans_ukupno = Convert.ToDecimal(txtAvans.Text);
            vrsta_usluge = Convert.ToDecimal(txtCijenaUsluge.Text);
            bor_pristojba = Convert.ToDecimal(txtIznosBorPristojbe.Text);

            if (cbSoba.Items.Count == 0) { MessageBox.Show("Greška.\r\nKrivo odabrana soba ili krivo odabrani tip sobe."); return; }

            DateTime dat = OD;
            for (int i = 0; i < broj_nocenja; i++)
            {
                string sql = "SELECT r_cijenasoba.cijena_nocenja,valute.tecaj,r_cijenasoba.od_datuma,r_cijenasoba.do_datuma FROM r_cijenasoba " +
                " LEFT JOIN valute ON valute.id_valuta=r_cijenasoba.id_valuta" +
                " WHERE id_soba='" + cbSoba.SelectedValue.ToString() + "'" +
                " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'>=r_cijenasoba.od_datuma " +
                " AND '" + dat.ToString("yyyy-MM-dd H:mm:ss") + "'<=r_cijenasoba.do_datuma";

                DataTable DTcijene = RemoteDB.select(sql, "r_cijenasoba").Tables[0];

                if (DTcijene.Rows.Count > 0)
                {
                    cijena_sobe = Convert.ToDecimal(DTcijene.Rows[0]["cijena_nocenja"].ToString()) * Convert.ToDecimal(DTcijene.Rows[0]["tecaj"].ToString());
                    try
                    {
                        //cijena_sobe = Convert.ToDecimal(txtCijena.Text);
                    }
                    catch { cijena_sobe = 0; }
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
                    cijena_sobe_prosjek += cijena_sobe;
                    dat = dat.AddDays(1);
                }
                else
                {
                    ukupno_stavka += (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
                    cijena_sobe_prosjek += cijena_sobe;
                    dat = dat.AddDays(1);
                }
            }

            //ukupno_stavka = (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100))+vrsta_usluge+bor_pristojba;
            //ukupno_stavka = ukupno_stavka * broj_nocenja;

            if (cijena_sobe_prosjek > 0)
                lblCijenaSobe.Text = (cijena_sobe_prosjek / broj_nocenja).ToString("#0.00");
            else
                lblCijenaSobe.Text = (cijena_sobe).ToString("#0.00");

            lblAvans.Text = avans_ukupno.ToString("#0.00");
            lblPopust.Text = ((((cijena_sobe) * rabat_stavka) / 100) * broj_nocenja).ToString("#0.00") + " kn";
            lblUkupnoPoUnosu.Text = ukupno_stavka.ToString("#0.00") + " kn";
            //lblPopust.Text = (((cijena_sobe) * rabat_stavka) / 100).ToString("#0.00")+" kn";
        }

        private void RacunajUkupno()
        {
            sve_ukupno = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                decimal kol = Funkcije.ReturnDaysFromDate(Convert.ToDateTime(dgv.Rows[i].Cells["datum_dolaska"].FormattedValue.ToString()), Convert.ToDateTime(dgv.Rows[i].Cells["datum_odlaska"].FormattedValue.ToString()));
                sve_ukupno = (Convert.ToDecimal(dgv.Rows[i].Cells["ukupno"].FormattedValue.ToString()) * kol) + sve_ukupno;
            }

            lblUkupno.Text = sve_ukupno.ToString("#0.00");
        }

        private void cbBorProstojba_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadCompleted)
            {
                DataTable DTt = RemoteDB.select("SELECT * FROM boravisna_pristojba WHERE id='" + cbBorProstojba.SelectedValue + "'", "boravisna_pristojba").Tables[0];
                if (DTt.Rows.Count > 0)
                {
                    txtIznosBorPristojbe.Text = DTt.Rows[0]["iznos"].ToString();
                }
                RacunajStavku();
                RacunajUkupno();
            }
        }

        private void cbVrstaUsluge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadCompleted)
            {
                DataTable DTu = RemoteDB.select("SELECT * FROM vrsta_usluge WHERE id='" + cbVrstaUsluge.SelectedValue + "'", "vrsta_usluge").Tables[0];
                if (DTu.Rows.Count > 0)
                {
                    txtCijenaUsluge.Text = DTu.Rows[0]["iznos"].ToString();
                }
                RacunajStavku();
                RacunajUkupno();
            }
        }

        private void txtAvans_Leave(object sender, EventArgs e)
        {
            RacunajStavku();
            RacunajUkupno();
            txtAvans.BackColor = Color.White;
            txtPopust.Select();
        }

        private void txtPopust_Leave(object sender, EventArgs e)
        {
            RacunajStavku();
            RacunajUkupno();
            txtPopust.BackColor = Color.White;
            cbAgencija.Select();
        }

        private void DeleteFields(bool dgv_bool)
        {
            RacunajStavku();

            txtImePrezime.Text = "";
            txtAdresa.Text = "";
            txtAvans.Text = "0";
            txtBrojOsobne.Text = "";
            txtBrojPutovnice.Text = "";
            txtNapomena.Text = "";
            txtPopust.Text = "0";

            if (dgv_bool)
            {
                dgv.Rows.Clear();
            }

            RacunajUkupno();
        }

        private bool editDgv = false;
        private int broj_rowa_dgv_edit = new int();

        private void btnUrediOznacenog_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Nemate nitijednu stavku.");
                return;
            }

            if (editDgv)
            {
                int b = broj_rowa_dgv_edit;
                DateTime OD = dtpDatDolaska.Value;
                DateTime DO = dtpDatOdlaska.Value;
                broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

                dgv.Rows[b].Cells["imeprezime"].Value = txtImePrezime.Text;
                dgv.Rows[b].Cells["adresa"].Value = txtAdresa.Text;
                dgv.Rows[b].Cells["datum_dolaska"].Value = dtpDatDolaska.Value;
                dgv.Rows[b].Cells["datum_odlaska"].Value = dtpDatOdlaska.Value;
                dgv.Rows[b].Cells["soba"].Value = cbSoba.Text;
                dgv.Rows[b].Cells["avans"].Value = txtAvans.Text;
                dgv.Rows[b].Cells["iznosBP"].Value = txtIznosBorPristojbe.Text;
                dgv.Rows[b].Cells["iznosVS"].Value = txtCijenaUsluge.Text;
                dgv.Rows[b].Cells["cijena_sobe_dgv"].Value = Funkcije.Mat(cijena_sobe_prosjek / broj_nocenja, 3);
                dgv.Rows[b].Cells["broj_osobne"].Value = txtBrojOsobne.Text;
                dgv.Rows[b].Cells["broj_putovnice"].Value = txtBrojPutovnice.Text;
                dgv.Rows[b].Cells["id_agencija"].Value = cbAgencija.SelectedValue;
                dgv.Rows[b].Cells["id_soba"].Value = cbSoba.SelectedValue;
                dgv.Rows[b].Cells["id_tip_sobe"].Value = cbTipSoba.SelectedValue;
                dgv.Rows[b].Cells["id_vrsta_usluge"].Value = cbVrstaUsluge.SelectedValue;
                ukupno_stavka = (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;

                if (DTpostavke.Rows[0]["naplata_po_sobi"].ToString() == "1")
                {
                    ukupno_stavka = ukupno_stavka * broj_nocenja;
                    dgv.Rows[b].Cells["ukupno"].Value = Funkcije.Mat(ukupno_stavka, 3);
                }

                if (chbDorucak.Checked)
                {
                    dgv.Rows[b].Cells["dorucak"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["dorucak"].Value = "0";
                }
                if (chbRucak.Checked)
                {
                    dgv.Rows[b].Cells["rucak"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["rucak"].Value = "0";
                }
                if (chbVecera.Checked)
                {
                    dgv.Rows[b].Cells["vecera"].Value = "1";
                }
                else
                {
                    dgv.Rows[b].Cells["vecera"].Value = "0";
                }

                dgv.Rows[b].Cells["napomena"].Value = txtNapomena.Text;
                dgv.Rows[b].Cells["popust"].Value = txtPopust.Text;
                dgv.Rows[b].Cells["id_boravisna_pristojba"].Value = cbBorProstojba.SelectedValue;
                dgv.Rows[b].Cells["id_vrsta_gosta"].Value = cbVrsteGosta.SelectedValue;
                dgv.Rows[b].Cells["id_drzava"].Value = cbDrzava.SelectedValue;
                dgv.Rows[b].Cells["datum_rodenja"].Value = dtpRodenje.Value;

                DeleteFields(false);
                btnUrediOznacenog.Text = "Uredi označenog unosa";
                btnUrediOznacenog.Font = new Font(btnUrediOznacenog.Font.Name, 8.25F, FontStyle.Regular);
                broj_rowa_dgv_edit = new int();
                editDgv = false;
                NaplatiZaCijeluSobu();
                return;
            }

            int br = dgv.CurrentRow.Index;
            broj_rowa_dgv_edit = br;

            txtImePrezime.Text = dgv.Rows[br].Cells["imeprezime"].FormattedValue.ToString();
            txtAdresa.Text = dgv.Rows[br].Cells["adresa"].FormattedValue.ToString();
            dtpDatDolaska.Value = Convert.ToDateTime(dgv.Rows[br].Cells["datum_dolaska"].FormattedValue.ToString());
            dtpDatOdlaska.Value = Convert.ToDateTime(dgv.Rows[br].Cells["datum_odlaska"].FormattedValue.ToString());
            //cbSoba.SelectedValue = dgv.Rows[br].Cells["soba"].FormattedValue.ToString();
            txtAvans.Text = dgv.Rows[br].Cells["avans"].FormattedValue.ToString();
            txtIznosBorPristojbe.Text = dgv.Rows[br].Cells["iznosBP"].FormattedValue.ToString();
            txtCijenaUsluge.Text = dgv.Rows[br].Cells["iznosVS"].FormattedValue.ToString();

            txtBrojOsobne.Text = dgv.Rows[br].Cells["broj_osobne"].FormattedValue.ToString();
            txtBrojPutovnice.Text = dgv.Rows[br].Cells["broj_putovnice"].FormattedValue.ToString();
            cbAgencija.SelectedValue = dgv.Rows[br].Cells["id_agencija"].FormattedValue.ToString();
            cbSoba.SelectedValue = dgv.Rows[br].Cells["id_soba"].FormattedValue.ToString();
            cbTipSoba.SelectedValue = dgv.Rows[br].Cells["id_tip_sobe"].FormattedValue.ToString();
            cbVrstaUsluge.SelectedValue = dgv.Rows[br].Cells["id_vrsta_usluge"].FormattedValue.ToString();

            if (dgv.Rows[br].Cells["dorucak"].FormattedValue.ToString() == "1")
            {
                chbDorucak.Checked = true;
            }
            else
            {
                chbDorucak.Checked = false;
            }
            if (dgv.Rows[br].Cells["rucak"].FormattedValue.ToString() == "1")
            {
                chbRucak.Checked = true;
            }
            else
            {
                chbRucak.Checked = false;
            }
            if (dgv.Rows[br].Cells["vecera"].FormattedValue.ToString() == "1")
            {
                chbVecera.Checked = true;
            }
            else
            {
                chbVecera.Checked = false;
            }

            txtNapomena.Text = dgv.Rows[br].Cells["napomena"].FormattedValue.ToString();
            txtPopust.Text = dgv.Rows[br].Cells["popust"].FormattedValue.ToString();
            cbBorProstojba.SelectedValue = dgv.Rows[br].Cells["id_boravisna_pristojba"].FormattedValue.ToString();
            cbVrsteGosta.SelectedValue = dgv.Rows[br].Cells["id_vrsta_gosta"].FormattedValue.ToString();
            cbDrzava.SelectedValue = dgv.Rows[br].Cells["id_drzava"].FormattedValue.ToString();
            dtpRodenje.Value = Convert.ToDateTime(dgv.Rows[br].Cells["datum_rodenja"].FormattedValue.ToString());

            DateTime OD1 = dtpDatDolaska.Value;
            DateTime DO1 = dtpDatOdlaska.Value;
            decimal broj_nocenja1 = Funkcije.ReturnDaysFromDate(OD1, DO1);

            ukupno_stavka = (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
            ukupno_stavka = ukupno_stavka * broj_nocenja1;

            btnUrediOznacenog.Text = "Završi uređivanje";
            editDgv = true;
            btnUrediOznacenog.Font = new Font(btnUrediOznacenog.Font.Name, 9, FontStyle.Bold);
            //NaplatiZaCijeluSobu();
        }

        private bool edit = false;
        public string broj_edit { get; set; }

        private void txtBrojDokumenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = RemoteDB.select("SELECT broj FROM unos_gosta WHERE broj='" + txtBrojDokumenta.Text + "'  AND godina='" + nuGodina.Value.ToString() + "'", "unos_gosta").Tables[0];

                if (DT.Rows.Count == 0)
                {
                    if (BrojDokumenta() == txtBrojDokumenta.Text)
                    {
                        DeleteFields(true);
                        edit = false;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count > 0)
                {
                    if (dgv.Rows.Count > 0)
                    {
                        dgv.Rows.Clear();
                    }

                    broj_edit = txtBrojDokumenta.Text;
                    Fill();
                    edit = true;
                }
            }
        }

        private void Fill()
        {
            txtBrojDokumenta.Enabled = false;

            try
            {
                nuGodina.Value = Convert.ToInt16(__godina);
            }
            catch
            {
            }

            string sql = "SELECT * FROM unos_gosta WHERE broj='" + txtBrojDokumenta.Text + "' AND godina='" + __godina + "'";

            DataTable DT = RemoteDB.select(sql, "unos_gosta").Tables[0];

            foreach (DataRow dr in DT.Rows)
            {
                try
                {
                    dgv.Rows.Add();
                    int br = dgv.Rows.Count - 1;

                    dgv.Rows[br].Cells["cijena_sobe_dgv"].Value = dr["cijena_sobe"].ToString();
                    dgv.Rows[br].Cells["iznosVS"].Value = dr["iznos_vu"].ToString();
                    dgv.Rows[br].Cells["iznosBP"].Value = dr["iznos_bor_pristojbe"].ToString();
                    dgv.Rows[br].Cells["popust"].Value = dr["popust"].ToString();
                    dgv.Rows[br].Cells["napomena"].Value = dr["napomena"].ToString();
                    txtBrojDokumenta.Text = dr["broj"].ToString();
                    nuGodina.Value = Convert.ToInt16(dr["godina"].ToString());
                    dgv.Rows[br].Cells["imeprezime"].Value = dr["ime_gosta"].ToString();
                    dgv.Rows[br].Cells["adresa"].Value = dr["adresa"].ToString();
                    dgv.Rows[br].Cells["cijena_sobe_dgv"].Value = Funkcije.Mat(Convert.ToDecimal(dr["cijena_sobe"].ToString()), 3);
                    dgv.Rows[br].Cells["broj_osobne"].Value = dr["broj_osobne"].ToString();
                    dgv.Rows[br].Cells["broj_putovnice"].Value = dr["broj_putovnice"].ToString();
                    dgv.Rows[br].Cells["datum_dolaska"].Value = dr["datum_dolaska"].ToString();
                    dgv.Rows[br].Cells["tb"].Value = dr["porez"].ToString();
                    dgv.Rows[br].Cells["datum_odlaska"].Value = dr["datum_odlaska"].ToString();
                    dgv.Rows[br].Cells["datum_rodenja"].Value = dr["datum_rodenja"].ToString();
                    dgv.Rows[br].Cells["id_agencija"].Value = dr["id_agencija"].ToString();
                    dgv.Rows[br].Cells["id_soba"].Value = dr["id_soba"].ToString();
                    dgv.Rows[br].Cells["id_tip_sobe"].Value = dr["id_tip_sobe"].ToString();
                    dgv.Rows[br].Cells["id_vrsta_gosta"].Value = dr["id_vrsta_gosta"].ToString();
                    dgv.Rows[br].Cells["ukupno"].Value = Funkcije.Mat(Convert.ToDecimal(dr["ukupno"].ToString()), 3);
                    DataTable DDD = RemoteDB.select("SELECT naziv_sobe FROM sobe WHERE id='" + dr["id_soba"].ToString() + "'", "sobe").Tables[0];
                    if (DDD.Rows.Count > 0)
                    {
                        dgv.Rows[br].Cells["soba"].Value = DDD.Rows[0]["naziv_sobe"].ToString();
                    }
                    dgv.Rows[br].Cells["id_vrsta_usluge"].Value = dr["id_vrsta_usluge"].ToString();
                    dgv.Rows[br].Cells["id_boravisna_pristojba"].Value = dr["id_boravisna_pristojba"].ToString();
                    dgv.Rows[br].Cells["id_drzava"].Value = dr["id_drzava"].ToString();
                    dgv.Rows[br].Cells["avans"].Value = dr["avans"].ToString();
                    dgv.Rows[br].Cells["dorucak"].Value = dr["dorucak"].ToString();
                    dgv.Rows[br].Cells["rucak"].Value = dr["rucak"].ToString();
                    dgv.Rows[br].Cells["vecera"].Value = dr["vecera"].ToString();
                    dgv.Rows[br].Cells["id_unos"].Value = dr["id"].ToString();

                    decimal dec;
                    if (!decimal.TryParse(dr["avans"].ToString(), out dec))
                    {
                        txtAvans.Text = "0";
                    }
                    if (!decimal.TryParse(dr["popust"].ToString(), out dec))
                    {
                        txtPopust.Text = "0";
                    }
                    if (!decimal.TryParse(dr["iznos_vu"].ToString(), out dec))
                    {
                        txtCijenaUsluge.Text = "0";
                    }
                    if (!decimal.TryParse(dr["iznos_bor_pristojbe"].ToString(), out dec))
                    {
                        txtIznosBorPristojbe.Text = "0";
                    }

                    rabat_stavka = Convert.ToDecimal(dr["popust"].ToString());
                    avans_ukupno = Convert.ToDecimal(dr["avans"].ToString());
                    vrsta_usluge = Convert.ToDecimal(dr["iznos_vu"].ToString());
                    bor_pristojba = Convert.ToDecimal(dr["iznos_bor_pristojbe"].ToString());
                    cijena_sobe = Convert.ToDecimal(dr["cijena_sobe"].ToString());

                    DateTime OD = dtpDatDolaska.Value;
                    DateTime DO = dtpDatOdlaska.Value;
                    decimal broj_nocenja = Funkcije.ReturnDaysFromDate(OD, DO);

                    ukupno_stavka = (cijena_sobe - (((cijena_sobe) * rabat_stavka) / 100)) + vrsta_usluge + bor_pristojba;
                    ukupno_stavka = ukupno_stavka * broj_nocenja;
                    RacunajUkupno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška:\r\n\r\n" + ex.ToString(), "Greška");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(colorDlg.Color.ToString());
            }
        }

        private void btnAvans_Click(object sender, EventArgs e)
        {
        }

        private void btnDrzava_Click(object sender, EventArgs e)
        {
            Forme.sifarnici.frmNovaDrzava nd = new sifarnici.frmNovaDrzava();
            nd.ShowDialog();

            DataTable DTdrzava = RemoteDB.select("SELECT * FROM zemlja", "zemlja").Tables[0];
            cbDrzava.DataSource = DTdrzava;
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "country_code";
            cbDrzava.SelectedValue = "HR";
        }

        private void txtIznosBorPristojbe_Validated(object sender, EventArgs e)
        {
            RacunajStavku();
        }

        private void dtpDatDolaska_Validated(object sender, EventArgs e)
        {
            RacunajStavku();
        }

        private void nuGodina_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void nuGodina_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int izlaz;
                if (int.TryParse(__godina, out izlaz))
                {
                    nuGodina.Value = izlaz;
                }

                broj_upisa = BrojDokumenta();
                txtBrojDokumenta.Text = broj_upisa;
            }
            catch
            {
            }
        }
    }
}