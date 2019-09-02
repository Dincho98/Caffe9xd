using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                Import();
                Application.DoEvents();
            }
        }

        private void Import()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "mjestaRh");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string sql = "INSERT INTO grad (posta,grad,naselje,zupanija) VALUES (" +
                    " '" + dataSet.Tables[0].Rows[i][1].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][2].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][3].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][4].ToString() + "'" +
                    ")";
                classSQL.insert(sql);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                ImportDrzave();
                Application.DoEvents();
            }
        }

        private void ImportDrzave()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "Country");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string sql = "INSERT INTO zemlja (zemlja,country_code,aktivnost) VALUES (" +
                    " '" + dataSet.Tables[0].Rows[i][1].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][2].ToString() + "','DA'" +
                    ")";
                classSQL.insert(sql);
            }
            MessageBox.Show("Done");
            dgv.DataSource = classSQL.select_settings("SELECT * FROM zemlja", "zemlja").Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // dgv.DataSource = classSQL.select_settings("SELECT * FROM partners", "partners").Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                ImportPartners();
                Application.DoEvents();
            }
        }

        private void ImportPartners()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "Lista poslovnih partnera");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            string grad = "";

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                DataTable DT = classSQL.select("SELECT id_grad FROM grad WHERE posta LIKE'%" + dataSet.Tables[0].Rows[i][9].ToString() + "%'", "grad").Tables[0];
                if (DT.Rows.Count != 0)
                {
                    grad = DT.Rows[0][0].ToString();
                }
                else
                {
                    grad = "1";
                }

                string sql = "INSERT INTO partners (ime_tvrtke,id_grad,adresa,oib,id_partner) VALUES (" +
                    " '" + dataSet.Tables[0].Rows[i][2].ToString() + "'," +
                    " '" + grad + "'," +
                    " '" + dataSet.Tables[0].Rows[i][7].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i]["OIB"].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][1].ToString() + "'" +
                    ")";
                classSQL.insert(sql);
            }
            MessageBox.Show("Done");
            dgv.DataSource = classSQL.select_settings("SELECT * FROM partners", "partners").Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                FillRoba();
                Application.DoEvents();
            }
        }

        private void FillRoba()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "Artikli");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            MessageBox.Show(dataSet.Tables[0].Rows.Count.ToString());

            string oduzmi = "DA";
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                //if (dataSet.Tables[0].Rows[i][4].ToString() == "USLUGA") {
                //    oduzmi = "NE";
                //} else {
                //    oduzmi = "DA";
                //}

                string naziv = dataSet.Tables[0].Rows[i][0].ToString().Trim();
                string sf = dataSet.Tables[0].Rows[i][1].ToString();
                naziv = naziv.Replace("\"", "");
                naziv = naziv.Replace("'", "");
                naziv = naziv.Replace("[", "");
                naziv = naziv.Replace("]", "");

                sf = sf.Replace("\"", "");
                sf = sf.Replace("'", "");
                sf = sf.Replace("[", "");
                sf = sf.Replace("]", "");
                sf = sf.Replace(" ", "");
                sf = sf.Trim();

                //if (sf.Length > 20) {
                //    sf = sf.Remove(20);
                //}

                string sql = "INSERT INTO roba (sifra, naziv, jm, porez, nbc, mpc, id_grupa, id_podgrupa) VALUES (" +
                    " '" + sf + "'," +
                    " '" + naziv + "'," +
                    " '" + dataSet.Tables[0].Rows[i][3].ToString() + "'," +
                    " '" + dataSet.Tables[0].Rows[i][6].ToString() + "'," +
                    " '0'," +
                    " '" + Convert.ToDouble(dataSet.Tables[0].Rows[i][7].ToString()) + "'," +
                    " '1'," +
                    " '1'" +
                    ")";
                provjera_sql(classSQL.insert(sql));
            }
            MessageBox.Show("Done");
            dgv.DataSource = classSQL.select_settings("SELECT * FROM roba", "partners").Tables[0];
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                FillEAN();
                Application.DoEvents();
            }
        }

        private void FillEAN()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "PREGLED ROBA");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            MessageBox.Show(dataSet.Tables[0].Rows.Count.ToString());

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string sf = dataSet.Tables[0].Rows[i][0].ToString();
                sf = sf.Replace("\"", "");
                sf = sf.Replace("'", "");
                sf = sf.Replace("[", "");
                sf = sf.Replace("]", "");
                sf = sf.Replace(" ", "");
                sf = sf.Trim();

                if (sf.Length > 20)
                {
                    sf = sf.Remove(20);
                }

                if (dataSet.Tables[0].Rows[i][14].ToString() != "")
                {
                    string sql = "UPDATE roba SET ean='" + dataSet.Tables[0].Rows[i][14].ToString() + "' WHERE sifra='" + sf + "'";
                    provjera_sql(classSQL.update(sql));
                }
            }
            MessageBox.Show("Done");
        }

        private void btnRobaProdaja_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                FillRobaProdaja();
                Application.DoEvents();
            }
        }

        private void FillRobaProdaja()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "ISPIS ZALIHE");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            MessageBox.Show(dataSet.Tables[0].Rows.Count.ToString());

            int uu = 0;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (classSQL.remoteConnectionString == "")
                {
                }
                string sf = dataSet.Tables[0].Rows[i][1].ToString();
                sf = sf.Replace("\"", "");
                sf = sf.Replace("'", "");
                sf = sf.Replace("[", "");
                sf = sf.Replace("]", "");
                sf = sf.Replace(" ", "");
                sf = sf.Trim();

                string mpc = dataSet.Tables[0].Rows[i][12].ToString();
                string vpc = (Convert.ToDouble(dataSet.Tables[0].Rows[i][12].ToString()) / Convert.ToDouble("1,25")).ToString();

                if (classSQL.remoteConnectionString == "")
                {
                    mpc = mpc.Replace(",", ".");
                    vpc = vpc.Replace(",", ".");
                }
                else
                {
                    mpc = mpc.Replace(".", ",");
                    vpc = vpc.Replace(",", ".");
                }

                if (sf.Length > 20)
                {
                    sf = sf.Remove(20);
                }

                DataTable DT = classSQL.select("SELECT sifra FROM roba WHERE sifra='" + sf + "'", "roba").Tables[0];
                if (DT.Rows.Count == 0)
                {
                    string s = "INSERT INTO roba (sifra,naziv,jm,porez,nc,vpc,mpc,oduzmi,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,id_grupa) VALUES (" +
                        "'" + sf + "'," +
                        "'" + dataSet.Tables[0].Rows[i][2].ToString() + "'," +
                        "'" + dataSet.Tables[0].Rows[i][4].ToString() + "'," +
                        "'25'," +
                        "'0'," +
                        "'" + vpc + "'," +
                        "'" + mpc + "'," +
                        "'DA'," +
                        "'247'," +
                        "'247'," +
                        "'830'," +
                        "'4'," +
                        "'1'" +
                        ")";
                    provjera_sql(classSQL.insert(s));
                    uu++;
                }

                string sql = "INSERT INTO roba_prodaja (sifra,id_skladiste,kolicina,vpc,porez,nc) VALUES (" +
                    "'" + sf + "'," +
                    "'2'," +
                    "'" + dataSet.Tables[0].Rows[i]["UKKOL"].ToString() + "'," +
                    "'" + vpc + "'," +
                    "'25'," +
                    "'0'" +
                    ")";
                provjera_sql(classSQL.update(sql));
            }
            MessageBox.Show("Done");
            MessageBox.Show(uu.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable DTroba = classSQL.select("select sifra from roba group by sifra having count(*) > 1", "roba").Tables[0];
            int br = 0;
            for (int i = 0; i < DTroba.Rows.Count; i++)
            {
                DataTable DTR = classSQL.select("SELECT sifra,id_roba FROM roba WHERE sifra='" + DTroba.Rows[i]["sifra"].ToString() + "'", "roba").Tables[0];
                if (DTR.Rows.Count > 1)
                {
                    classSQL.delete("DELETE FROM roba WHERE id_roba='" + DTR.Rows[0]["id_roba"].ToString() + "'");
                    br++;
                }
            }
            MessageBox.Show(br.ToString());
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
            }
        }

        private void btnNBCuProdaju_Click(object sender, EventArgs e)
        {
            DataTable dtDT = classSQL.select("SELECT * FROM roba_prodaja", "roba_prodaja").Tables[0];

            for (int i = 0; i < dtDT.Rows.Count; i++)
            {
                DataTable d = classSQL.select("SELECT nc FROM roba WHERE sifra='" + dtDT.Rows[i]["sifra"].ToString() + "'", "").Tables[0];

                string sql = "UPDATE roba_prodaja SET nc='" + d.Rows[0]["nc"].ToString() + "' WHERE id_roba_prodaja='" + dtDT.Rows[i]["id_roba_prodaja"].ToString() + "' AND nc='0'";
                classSQL.update(sql);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                FillRobaCaffe();
                Application.DoEvents();
            }
        }

        private void FillRobaCaffe()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            MessageBox.Show(dataSet.Tables[0].Rows.Count.ToString());

            string oduzmi = "";
            for (int i = 1; i < dataSet.Tables[0].Rows.Count + 1; i++)
            {
                string naziv = dataSet.Tables[0].Rows[i][3].ToString().Trim();
                string sf = dataSet.Tables[0].Rows[i][2].ToString();
                naziv = naziv.Replace("\"", "");
                naziv = naziv.Replace("'", "");
                naziv = naziv.Replace("[", "");
                naziv = naziv.Replace("]", "");

                sf = sf.Replace("\"", "");
                sf = sf.Replace("'", "");
                sf = sf.Replace("[", "");
                sf = sf.Replace("]", "");
                sf = sf.Replace(" ", "");
                sf = sf.Trim();

                if (sf.Length > 20)
                {
                    sf = sf.Remove(20);
                }

                string id_porez = "";
                string porez = "";

                if (dataSet.Tables[0].Rows[i][6].ToString().Trim() == "TBR25")
                {
                    id_porez = "1";
                    porez = "25";
                }
                else
                {
                    id_porez = "2";
                    porez = "28";
                }

                string sql = "INSERT INTO roba (sifra,naziv,jm,porez,nc,vpc,mpc,oduzmi,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,id_grupa) VALUES (" +
                    " '" + sf + "'," +
                    " '" + naziv + "'," +
                    " '" + dataSet.Tables[0].Rows[i][5].ToString() + "'," +
                    " '" + id_porez + "'," +
                    " '0'," +
                    " '" + Convert.ToDouble(dataSet.Tables[0].Rows[i][7].ToString()) / Convert.ToDouble("1," + porez) + "'," +
                    " '" + Convert.ToDouble(dataSet.Tables[0].Rows[i][7].ToString()) + "'," +
                    " 'DA'," +
                    " '247'," +
                    " '247'," +
                    " '830'," +
                    " '4'," +
                    " '" + dataSet.Tables[0].Rows[i][4].ToString() + "'" +
                    ")";
                provjera_sql(classSQL.insert(sql));
            }
            MessageBox.Show("Done");
            dgv.DataSource = classSQL.select_settings("SELECT * FROM roba", "partners").Tables[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                FillRobaProdajaCaffe();
                Application.DoEvents();
            }
        }

        private void FillRobaProdajaCaffe()
        {
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtFileName.Text);
            string query = String.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            MessageBox.Show(dataSet.Tables[0].Rows.Count.ToString());

            int uu = 0;
            for (int i = 1; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string sql = "INSERT INTO roba (naziv,id_grupa,jm,mpc,sifra,ean,porez,porez_potrosnja,aktivnost,id_podgrupa) VALUES (" +
                    "'" + dataSet.Tables[0].Rows[i][1].ToString() + "'," +
                    "'" + dataSet.Tables[0].Rows[i][3].ToString() + "'," +
                    "'" + dataSet.Tables[0].Rows[i][7].ToString() + "'," +
                    "'" + dataSet.Tables[0].Rows[i][2].ToString() + "'," +
                    "'" + dataSet.Tables[0].Rows[i][0].ToString() + "'," +
                    "'0'," +
                    "'" + dataSet.Tables[0].Rows[i][6].ToString() + "'," +
                    "'" + dataSet.Tables[0].Rows[i][9].ToString() + "'," +
                    "'1'," +
                    "'1'" +
                    ")";
                provjera_sql(classSQL.update(sql));
            }
            MessageBox.Show("Done");
            MessageBox.Show(uu.ToString());
        }
    }
}