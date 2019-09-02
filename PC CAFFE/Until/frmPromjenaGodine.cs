using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Until
{
    public partial class frmPromjenaGodine : Form
    {
        public frmPromjenaGodine()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private PCPOS.Until.classFukcijeZaUpravljanjeBazom B = new classFukcijeZaUpravljanjeBazom("CAFFE", "DB");

        private void frmPotvrdaZaNovuGodinu_Load(object sender, EventArgs e)
        {
            List<string> L = B.UzmiSveBazeIzPostgressa();
            DataTable DT = new DataTable();
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("id");
                DT.Columns.Add("name", typeof(int));
            }

            foreach (string db in L)
            {
                string dbprefix = db;
                dbprefix = dbprefix.Remove(dbprefix.Length - Util.Korisno.GodinaKojaSeKoristiUbazi.ToString().Length);
                if (db != "postgres" && (dbprefix == Util.Korisno.prefixBazeKojaSeKoristi()))
                {
                    string baza = db;
                    baza = db.Remove(0, Util.Korisno.prefixBazeKojaSeKoristi().Length);
                    //baza = baza.Replace("DB", "");
                    //baza = baza.Replace("POS", "");
                    //baza = baza.Replace("db", "");
                    //baza = baza.Replace("pos", "");
                    DT.Rows.Add(db, baza);
                }
            }
            DataView dv = new DataView(DT);
            dv.Sort = "name asc";
            DT = dv.ToTable();
            cbGodina.DataSource = DT;
            cbGodina.ValueMember = "id";
            cbGodina.DisplayMember = "name";

            try
            {
                cbGodina.SelectedValue = B.UzmiBazuKojaSeKoristi();
            }
            catch { }
        }

        private void btnPromjenaGodine_Click(object sender, EventArgs e)
        {
            B.PostaviGodinu_U_XML(cbGodina.SelectedValue.ToString());
        }
    }
}