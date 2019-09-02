using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmCreateRemoteDB : Form
    {
        private string AutoNumber = "";

        public frmCreateRemoteDB()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmCreateRemoteDB_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
        }

        private void CreateRemoteDB_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataSet dataSet = new DataSet();
            string sqlString = "Server = localhost; Port = 5432; User Id = postgres; Password = drazen2814mia; Database = postgres;";
            NpgsqlConnection remoteConnection = new NpgsqlConnection(sqlString);
            if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("CREATE DATABASE db" + DateTime.Now.Year.ToString() + " WITH OWNER = postgres ENCODING = 'UTF8' TABLESPACE = pg_default LC_COLLATE = 'Croatian_Croatia.1250' LC_CTYPE = 'Croatian_Croatia.1250' CONNECTION LIMIT = -1", remoteConnection);
            dataSet.Reset();
            da.Fill(dataSet);
            remoteConnection.Close();

            string sql = "";
            string br = "";
            string data_type = "";
            DataTable DT = classSQL.select_settings("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];
            //dataGridView1.DataSource = DT;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataTable DTkey = classSQL.select_settings("SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='" + DT.Rows[i]["TABLE_NAME"].ToString() + "'", "INFORMATION_SCHEMA.KEY_COLUMN_USAGE").Tables[0];

                DataTable DTT = classSQL.select_settings("SELECT AUTOINC_INCREMENT,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION FROM information_schema.columns WHERE TABLE_NAME ='" + DT.Rows[i]["TABLE_NAME"].ToString() + "'", "Table").Tables[0];
                sql += "CREATE TABLE " + DT.Rows[i]["TABLE_NAME"].ToString() + "(\n";

                for (int y = 0; y < DTT.Rows.Count; y++)
                {
                    if (DTT.Rows[y]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && DTT.Rows[y]["DATA_TYPE"].ToString() != "ntext")
                    {
                        br = "(" + DTT.Rows[y]["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
                    }
                    else
                    {
                        br = "";
                    }

                    if (DTT.Rows[y]["DATA_TYPE"].ToString() == "int")
                    {
                        data_type = "integer";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "nvarchar")
                    {
                        data_type = "character varying";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "datetime")
                    {
                        data_type = "timestamp without time zone";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "ntext")
                    {
                        data_type = "text";
                    }
                    else
                    {
                        data_type = DTT.Rows[y]["DATA_TYPE"].ToString();
                    }

                    if (DTT.Rows[y]["AUTOINC_INCREMENT"].ToString() == "1")
                    {
                        if (DTkey.Rows.Count > 0)
                        {
                            AutoNumber = DTkey.Rows[0]["COLUMN_NAME"].ToString();
                            data_type = "serial";
                        }
                    }

                    sql += DTT.Rows[y]["COLUMN_NAME"].ToString() + " " + data_type + br + ",\n";
                }

                if (DTkey.Rows.Count > 0)
                {
                    if (DTkey.Rows[0]["COLUMN_NAME"].ToString() != "")
                    {
                        sql += "CONSTRAINT " + DT.Rows[i]["TABLE_NAME"].ToString() + "_primary_key" + " PRIMARY KEY (" + DTkey.Rows[0]["COLUMN_NAME"].ToString() + "),\n";
                    }
                }

                sql = sql.Remove(sql.Length - 2, 2);
                sql += ")\n\n";

                sqlString = "Server = localhost; Port = 5432; User Id = postgres; Password = drazen2814mia; Database = db" + DateTime.Now.Year.ToString() + ";";
                //sqlString = SQL.claasConnectDatabase.GetRemoteConnectionString();
                remoteConnection = new NpgsqlConnection(sqlString);
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                da = new NpgsqlDataAdapter(sql, remoteConnection);
                //richTextBox1.Text = sql.ToString();
                dataSet.Reset();
                da.Fill(dataSet);
                remoteConnection.Close();
                sql = "";

                DataTable DTcompact = classSQL.select_settings("SELECT * FROM " + DT.Rows[i]["TABLE_NAME"].ToString(), DT.Rows[i]["TABLE_NAME"].ToString()).Tables[0];

                //CREATE DATABASE newdb WITH TEMPLATE originaldb;
                for (int r = 0; r < DTcompact.Rows.Count; r++)
                {
                    string columnName = "";
                    string values = "";
                    sql = "INSERT INTO " + DT.Rows[i]["TABLE_NAME"].ToString() + " ";

                    for (int ii = 0; ii < DTcompact.Columns.Count; ii++)
                    {
                        if (AutoNumber != DTcompact.Columns[ii].ToString())
                        {
                            columnName += DTcompact.Columns[ii].ToString() + ",";
                            values += "'" + DTcompact.Rows[r][DTcompact.Columns[ii]].ToString().Replace("'", "") + "'" + ",";
                            values = values.Replace("\"", "");
                            values = values.Replace("[", "");
                            values = values.Replace("]", "");
                            values = values.Replace("(", "");
                            values = values.Replace(")", "");
                            values = values.Replace("\\", "");
                        }
                        else
                        {
                            AutoNumber = "";
                        }
                    }

                    columnName = columnName.Remove(columnName.Length - 1, 1);
                    values = values.Remove(values.Length - 1, 1);
                    sql += " ( " + columnName + " ) " + " VALUES " + " ( " + values + " ) ";

                    remoteConnection = new NpgsqlConnection(sqlString);
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    da = new NpgsqlDataAdapter(sql, remoteConnection);
                    dataSet.Reset();
                    da.Fill(dataSet);
                    remoteConnection.Close();
                    sql = "";
                }
            }

            MessageBox.Show("Spremljeno");
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