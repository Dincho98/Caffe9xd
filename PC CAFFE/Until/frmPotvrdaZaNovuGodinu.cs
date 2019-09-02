using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Until
{
    public partial class frmPotvrdaZaNovuGodinu : Form
    {
        public frmPotvrdaZaNovuGodinu()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
            groupBox1.Visible = false;
        }

        public PCPOS.Until.classFukcijeZaUpravljanjeBazom frmNG { get; set; }

        private void frmPotvrdaZaNovuGodinu_Load(object sender, EventArgs e)
        {
            List<string> L = frmNG.UzmiSveBazeIzPostgressa();
            DataTable DT = new DataTable();
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("id");
                DT.Columns.Add("name");
            }

            foreach (string db in L)
            {
                if (db != "postgres")
                {
                    DT.Rows.Add(db, db);
                }
            }

            cbGodina.DataSource = DT;
            cbGodina.ValueMember = "id";
            cbGodina.DisplayMember = "name";

            try
            {
                cbGodina.SelectedValue = frmNG.UzmiBazuKojaSeKoristi();
            }
            catch { }
        }

        private void btnPostavi_Click(object sender, EventArgs e)
        {
            frmNG.PostaviVarijabluZaPostojecuBazu(cbGodina.SelectedValue.ToString());
            frmNG.PostaviVarijabluZaNovuBazu(txtDBNova.Text);
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "q1w2e3r4nova_baza")
            {
                groupBox1.Visible = true;
            }
        }
    }
}