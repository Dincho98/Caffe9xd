using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobaUsluge : Form
    {
        private DataSet DSgrupa = new DataSet();
        private DataSet DSdrazava = new DataSet();
        private DataSet DSdrazava_uvoz = new DataSet();
        private DataSet DSmanufacturers = new DataSet();
        private DataSet DSPartneri = new DataSet();
        private DataSet DSporezi = new DataSet();
        private DataSet DSpp = new DataSet();
        private Boolean NoviUnos = false;
        private string id_roba;

        public frmRobaUsluge()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        public frmMenu MainFormMenu { get; set; }

        private void frmRobaUsluge_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            enable(true);
            //txtSifra.Enabled = true;
            string count = classSQL.select("SELECT count(*) FROM roba", "roba").Tables[0].Rows[0][0].ToString();

            while (classSQL.select("SELECT count(*) FROM roba WHERE sifra='" + count + "'", "roba").Tables[0].Rows[0][0].ToString() != "0")
            {
                count = Convert.ToString(Convert.ToInt16(count) + 1);
            }

            txtSifra.Text = count;
            NoviUnos = true;
            txtNaziv.Select();
        }

        private void enable(bool x)
        {
            txtSifraDob.Enabled = x;
            txtMPC.Enabled = x;
            txtnabavna.Enabled = x;
            txtNaziv.Enabled = x;
            txtSifra.Enabled = x;
            txtVeleprodajna.Enabled = x;
            cbGrupa.Enabled = x;
            txtJedMj.Enabled = x;
            cbProizvodac.Enabled = x;
            cbPorezNaPotrosnju.Enabled = x;
            cbZemljaPodrijetla.Enabled = x;
            cbZemljaUvoza.Enabled = x;
            txtEan.Enabled = x;
            bcUzmiSaSkladista.Enabled = x;
            txtPDV.Enabled = x;

            if (x == true)
            {
                txtEan.Text = "";
                txtMPC.Text = "0,00";
                txtnabavna.Text = "0,00";
                txtNaziv.Text = "";
                txtSifra.Text = "";
                txtNazivDob.Text = "";
                txtSifraDob.Text = "";
                txtVeleprodajna.Text = "0,00";
                popuniCB();
            }

            if (x == false)
            {
                txtNazivDob.Text = "";
                txtSifraDob.Text = "";
                txtEan.Text = "";
                txtMPC.Text = "";
                txtnabavna.Text = "";
                txtNaziv.Text = "";
                txtSifra.Text = "";
                txtVeleprodajna.Text = "";
            }
        }

        public class MyValue
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        private void popuniCB()
        {
            //CB grupe
            DSgrupa = classSQL.select("SELECT * FROM grupa ORDER BY grupa", "grupa");
            cbGrupa.DataSource = DSgrupa.Tables[0];
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //CB dražave
            DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemljaPodrijetla.DataSource = DSdrazava.Tables[0];
            cbZemljaPodrijetla.DisplayMember = "zemlja";
            cbZemljaPodrijetla.ValueMember = "id_zemlja";
            cbZemljaPodrijetla.SelectedValue = "60";

            //CB dražave_uvoz
            DSdrazava_uvoz = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemljaUvoza.DataSource = DSdrazava_uvoz.Tables[0];
            cbZemljaUvoza.DisplayMember = "zemlja";
            cbZemljaUvoza.ValueMember = "id_zemlja";
            cbZemljaUvoza.SelectedValue = "60";

            //CB proizvođač
            DSmanufacturers = classSQL.select("SELECT * FROM manufacturers ORDER BY manufacturers", "manufacturers");
            cbProizvodac.DataSource = DSmanufacturers.Tables[0];
            cbProizvodac.DisplayMember = "manufacturers";
            cbProizvodac.ValueMember = "id_manufacturers";

            //DS porez
            DSporezi = classSQL.select("SELECT * FROM porezi ORDER BY id_porez ASC", "porezi");
            txtPDV.DataSource = DSporezi.Tables[0];
            txtPDV.DisplayMember = "naziv";
            txtPDV.ValueMember = "iznos";

            //DS porez_na_potrosnju
            DSpp = classSQL.select("SELECT * FROM porez_na_potrosnju ORDER BY id_porez_potrosnja ASC", "porezi");
            cbPorezNaPotrosnju.DataSource = DSpp.Tables[0];
            cbPorezNaPotrosnju.DisplayMember = "naziv";
            cbPorezNaPotrosnju.ValueMember = "iznos";
            cbPorezNaPotrosnju.SelectedValue = "0";

            IList<MyValue> values = new List<MyValue> { new MyValue { id = "DA", name = "Kod prodaje skidaj sa skladišta" }, new MyValue { id = "NE", name = "Ne skidaj sa skladišta" } };
            bcUzmiSaSkladista.DataSource = values;
            bcUzmiSaSkladista.DisplayMember = "name";
            bcUzmiSaSkladista.ValueMember = "id";
        }

        private void btnDobavljac_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi djelatnost_trazi = new frmPartnerTrazi();
            djelatnost_trazi.ShowDialog();

            //dobiva odabranog partnera
            if (Properties.Settings.Default.id_partner != "")
            {
                Fill(Properties.Settings.Default.id_partner);
                //NoviUnos = false;
            }
        }

        private void Fill(string id)
        {
            try
            {
                if (id != "")
                {
                    txtSifraDob.Text = id;
                    DataTable DT = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + id + "'", "partners").Tables[0];

                    if (DT.Rows.Count > 0)
                    {
                        txtNazivDob.Text = DT.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Greška.\r\n" + x);
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba = new frmRobaTrazi();
            roba.ShowDialog();

            if (txtNaziv.Enabled == false)
            {
                enable(true);
                //txtSifra.Enabled = false;
            }

            if (Properties.Settings.Default.id_roba != "")
            {
                Fill_Roba(Properties.Settings.Default.id_roba);
            }
        }

        private void Fill_Roba(string id)
        {
            if (id != "")
            {
                DataTable tbRoba = classSQL.select("SELECT * FROM roba WHERE sifra='" + id + "'", "roba").Tables[0];

                if (tbRoba.Rows.Count > 0)
                {
                    enable(true);
                    txtSifra.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Kriva šifra.");
                    return;
                }

                id_roba = tbRoba.Rows[0]["id_roba"].ToString();
                DataTable DT = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + tbRoba.Rows[0]["id_partner"].ToString() + "'", "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtNazivDob.Text = DT.Rows[0][0].ToString();
                    txtSifraDob.Text = tbRoba.Rows[0]["id_partner"].ToString();
                }
                txtMPC.Text = Convert.ToDouble(tbRoba.Rows[0]["mpc"].ToString()).ToString("#0.00");
                txtEan.Text = tbRoba.Rows[0]["ean"].ToString();
                txtnabavna.Text = Convert.ToDouble(tbRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
                txtNaziv.Text = tbRoba.Rows[0]["naziv"].ToString();
                txtSifra.Text = tbRoba.Rows[0]["sifra"].ToString().Trim();
                txtVeleprodajna.Text = Convert.ToDouble(tbRoba.Rows[0]["vpc"].ToString()).ToString("#0.00");
                cbGrupa.SelectedValue = tbRoba.Rows[0]["id_grupa"].ToString();
                txtJedMj.Text = tbRoba.Rows[0]["jm"].ToString();
                cbProizvodac.SelectedValue = tbRoba.Rows[0]["id_manufacturers"].ToString();
                cbZemljaPodrijetla.SelectedValue = tbRoba.Rows[0]["id_zemlja_porijekla"].ToString();
                cbZemljaUvoza.SelectedValue = tbRoba.Rows[0]["id_zemlja_uvoza"].ToString();
                txtPDV.SelectedValue = tbRoba.Rows[0]["porez"].ToString();
                cbPorezNaPotrosnju.SelectedValue = tbRoba.Rows[0]["porez_potrosnja"].ToString();
                bcUzmiSaSkladista.SelectedValue = tbRoba.Rows[0]["oduzmi"].ToString();

                NoviUnos = false;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (NoviUnos == true)
            {
                spremi();
            }
            else
            {
                update();
            }
            txtSifra.Enabled = true;
        }

        private void update()
        {
            if (txtSifra.Text == "") { MessageBox.Show("Niste pravilno upisali šifru."); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Niste pravilno upisali ime artikla/usluge."); return; }
            if (txtJedMj.Text == "") { MessageBox.Show("Niste pravilno upisali jedinicu mjere."); return; }
            if (txtVeleprodajna.Text == "") { MessageBox.Show("Niste pravilno upisali veleprodajnu cijenu."); return; }
            if (txtMPC.Text == "") { MessageBox.Show("Niste pravilno upisali maloprodajnu cijenu."); return; }
            if (txtSifraDob.Text == "") { MessageBox.Show("Niste pravilno upisali šifru dobavljača."); return; }

            string vpc = txtVeleprodajna.Text;
            string mpc = txtMPC.Text;
            string nc = txtnabavna.Text;

            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(",", ".");
                nc = nc.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(".", ",");
                nc = nc.Replace(".", ",");
            }

            string sql = "UPDATE roba SET " +
                " naziv='" + txtNaziv.Text + "'," +
                " id_grupa='" + cbGrupa.SelectedValue + "'," +
                " jm='" + txtJedMj.Text + "'," +
                " nc='" + nc + "'," +
                " vpc='" + vpc + "'," +
                " ean='" + txtEan.Text + "'," +
                " mpc='" + mpc + "'," +
                " porez='" + txtPDV.SelectedValue + "'," +
                " id_zemlja_porijekla='" + cbZemljaPodrijetla.SelectedValue + "'," +
                " id_zemlja_uvoza='" + cbZemljaUvoza.SelectedValue + "'," +
                " id_partner='" + txtSifraDob.Text + "'," +
                " id_manufacturers='" + cbProizvodac.SelectedValue + "'," +
                " oduzmi='" + bcUzmiSaSkladista.SelectedValue + "'," +
                " porez_potrosnja='" + cbPorezNaPotrosnju.SelectedValue.ToString() + "'," +
                " sifra='" + txtSifra.Text.Trim() + "' WHERE id_roba='" + id_roba + "'";

            provjera_sql(classSQL.update(sql));

            txtSifra.Enabled = false;
            enable(false);
        }

        private void spremi()
        {
            if (txtEan.Text == "")
            {
                txtEan.Text = "-1";
            }

            if (txtNaziv.Enabled == false)
            {
                return;
            }

            if (txtSifra.Text.Length > 2)
            {
                if (txtSifra.Text.Substring(0, 3) == "000")
                {
                    MessageBox.Show("Početak šifra ne smije sadržavati više od dvije nule.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifra.Text = "";
                    return;
                }
            }

            string number = classSQL.select("SELECT count(*) FROM roba WHERE sifra ='" + txtSifra.Text + "'", "roba").Tables[0].Rows[0][0].ToString();

            if (number != "0") { MessageBox.Show("Greška.\r\nUpisana šifra već postoji."); return; }
            if (txtSifra.Text == "") { MessageBox.Show("Niste pravilno upisali šifru."); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Niste pravilno upisali ime artikla/usluge."); return; }
            if (txtJedMj.Text == "") { MessageBox.Show("Niste pravilno upisali jedinicu mjere."); return; }
            if (txtVeleprodajna.Text == "") { MessageBox.Show("Niste pravilno upisali veleprodajnu cijenu."); return; }
            if (txtMPC.Text == "") { MessageBox.Show("Niste pravilno upisali maloprodajnu cijenu."); return; }
            if (txtSifraDob.Text == "") { MessageBox.Show("Niste pravilno upisali šifru dobavljača."); return; }

            string vpc = txtVeleprodajna.Text;
            string mpc = txtMPC.Text;
            string nc = txtnabavna.Text;

            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(",", ".");
                nc = nc.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(".", ",");
                nc = nc.Replace(".", ",");
            }

            string sql = "";
            sql = "INSERT INTO roba (naziv,sifra,ean,id_grupa,nc,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,porez,oduzmi,porez_potrosnja,jm) " +
                "VALUES ('" + txtNaziv.Text + "'," +
            "'" + txtSifra.Text.Trim() + "'," +
            "'" + txtEan.Text + "'," +
            "'" + cbGrupa.SelectedValue + "'," +
            "'" + nc + "'," +
            "'" + vpc + "'," +
            "'" + mpc + "'," +
            "'" + cbZemljaPodrijetla.SelectedValue + "'," +
            "'" + cbZemljaUvoza.SelectedValue + "'," +
            "'" + txtSifraDob.Text + "'," +
            "'" + cbProizvodac.SelectedValue + "'," +
            "'" + txtPDV.SelectedValue + "'," +
            "'" + bcUzmiSaSkladista.SelectedValue + "'," +
            "'" + cbPorezNaPotrosnju.SelectedValue + "'," +
            "'" + txtJedMj.Text + "')";
            provjera_sql(classSQL.insert(sql));

            DataTable DT_skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            for (int i = 0; i < DT_skladiste.Rows.Count; i++)
            {
                sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) " +
                    "VALUES ('" + DT_skladiste.Rows[i]["id_skladiste"].ToString() + "'," +
                 "'0'," +
                 "'" + nc + "'," +
                 "'" + vpc + "'," +
                 "'" + txtPDV.SelectedValue + "'," +
                 "'" + txtSifra.Text + "'," +
                 "'" + cbPorezNaPotrosnju.SelectedValue + "')";
                provjera_sql(classSQL.insert(sql));
            }

            NoviUnos = false;
            enable(false);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            if (txtSifra.Text != "")
            {
                if (txtSifra.Text.Length > 2)
                {
                    if (txtSifra.Text.Substring(0, 3) == "000")
                    {
                        MessageBox.Show("Početak šifra ne smije sadržavati više od dvije nule.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSifra.Text = "";
                        return;
                    }
                }
                string count = classSQL.select("SELECT count(*) FROM roba WHERE sifra='" + txtSifra.Text + "'", "roba").Tables[0].Rows[0][0].ToString();

                if (count == "0")
                {
                    txtSifra.BackColor = Color.Azure;
                }
                else
                {
                    txtSifra.BackColor = Color.MistyRose;
                }
            }
            else
            {
                txtSifra.BackColor = Color.MistyRose;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enable(false);
            txtSifra.Enabled = true;
        }

        private void txtNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbGrupa.Select();
            }
        }

        private void cbGrupa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtJedMj.Select();
            }
        }

        private void txtJedMj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtEan.Select();
            }
        }

        private void txtEan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraDob.Select();
            }
        }

        private void cbDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraDob.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            bcUzmiSaSkladista.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraDob.Select();
                        }
                    }
                    else
                    {
                        txtSifraDob.Select();
                        return;
                    }
                }

                DataTable DT = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraDob.Text + "'", "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtNazivDob.Text = DT.Rows[0][0].ToString();
                    bcUzmiSaSkladista.Select();
                }
                else
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                    txtSifraDob.Text = "";
                }
            }
        }

        private void bcUzmiSaSkladista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtnabavna.Select();
            }
        }

        private void txtnabavna_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtVeleprodajna.Select();
            }
        }

        private void txtVeleprodajna_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPDV.Select();
            }
        }

        private void txtPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtMPC.Text = Convert.ToDouble((Convert.ToDouble(txtVeleprodajna.Text) * porez / 100) + Convert.ToDouble(txtVeleprodajna.Text)).ToString("#0.00");
                cbPorezNaPotrosnju.Select();
            }
        }

        private void cbPorezNaPotrosnju_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtMPC.Text = Convert.ToDouble((Convert.ToDouble(txtVeleprodajna.Text) * porez / 100) + Convert.ToDouble(txtVeleprodajna.Text)).ToString("#0.00");
                txtMPC.Select();
            }
        }

        private void txtMPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbProizvodac.Select();
            }
        }

        private void cbProizvodac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbZemljaPodrijetla.Select();
            }
        }

        private void cbZemljaPodrijetla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbZemljaUvoza.Select();
            }
        }

        private void txtVeleprodajna_Leave(object sender, EventArgs e)
        {
        }

        private void txtMPC_Leave(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtSifra.BackColor == Color.Azure)
                {
                    if (MessageBox.Show("Upisana šifra nije u sustavu. \r\nŽelite li dodatu novu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string sif = txtSifra.Text;
                        enable(true);
                        txtNaziv.Select();
                        txtSifra.Text = sif;
                        NoviUnos = true;
                        return;
                    }
                }

                Fill_Roba(txtSifra.Text);
                txtNaziv.Select();
            }
        }

        private void txtnabavna_Leave(object sender, EventArgs e)
        {
            try
            {
                txtnabavna.Text = Convert.ToDouble(txtnabavna.Text).ToString("#0.00");
            }
            catch (Exception)
            {
                txtnabavna.Text = "0.00";
            }
        }

        private void txtMPC_Leave_1(object sender, EventArgs e)
        {
            try
            {
                txtMPC.Text = Convert.ToDouble(txtMPC.Text).ToString("#0.00");
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtVeleprodajna.Text = Convert.ToDouble(Convert.ToDouble(txtMPC.Text) / Convert.ToDouble("1," + porez)).ToString("#0.0000");
            }
            catch (Exception)
            {
                txtMPC.Text = "0.00";
            }
        }

        private void txtVeleprodajna_Leave_1(object sender, EventArgs e)
        {
            try
            {
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtVeleprodajna.Text = Convert.ToDouble(txtVeleprodajna.Text).ToString("#0.00");
                txtMPC.Text = Convert.ToDouble((Convert.ToDouble(txtVeleprodajna.Text) * porez / 100) + Convert.ToDouble(txtVeleprodajna.Text)).ToString("#0.00");
            }
            catch (Exception)
            {
                txtVeleprodajna.Text = "0.00";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRobaUsluge_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(MainFormMenu.)
            //{
            //    MainFormMenu.panel1.Show();
            //}
        }

        private void txtPDV_Leave(object sender, EventArgs e)
        {
            try
            {
                txtMPC.Text = Convert.ToDouble(txtMPC.Text).ToString("#0.00");
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtVeleprodajna.Text = Convert.ToDouble(Convert.ToDouble(txtMPC.Text) / Convert.ToDouble("1," + porez)).ToString("#0.00");
            }
            catch (Exception)
            {
                txtMPC.Text = "0.00";
            }
        }

        private void cbPorezNaPotrosnju_Leave(object sender, EventArgs e)
        {
            try
            {
                txtMPC.Text = Convert.ToDouble(txtMPC.Text).ToString("#0.00");
                double porez = Convert.ToDouble(txtPDV.SelectedValue) + Convert.ToDouble(cbPorezNaPotrosnju.SelectedValue.ToString());
                txtVeleprodajna.Text = Convert.ToDouble(Convert.ToDouble(txtMPC.Text) / Convert.ToDouble("1," + porez)).ToString("#0.00");
            }
            catch (Exception)
            {
                txtMPC.Text = "0.00";
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
    }
}