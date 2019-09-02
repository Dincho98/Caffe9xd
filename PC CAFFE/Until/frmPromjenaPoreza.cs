using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Until
{
    public partial class frmPromjenaPoreza : Form
    {
        public string dokument { get; set; }

        public frmPromjenaPoreza()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void frmPromjenaPoreza_Load(object sender, EventArgs e)
        {
            if (dokument == "porez_na_potrosnju")
            {
                this.Text = "Promjena porez na potrošnju";

                lblNapomena.Text = "Prije nego promijenite poreza na potrošnju savjetujte se sa knjigovodstvom.\n\nU lijevom prozorčiću odaberite porez na potrošnju koju želite mijenjati, a u desnom prozorčiću odaberite postotak na koji želite porez na potrošnju promijeniti.\n\nnpr.\nAko želite promijeniti stopu od 3 na 0 u lijevom prozorcicu odaberite 3, a potom u desnom izaberite 0.";

                DataTable DTporezi = classSQL.select("SELECT DISTINCT CAST(REPLACE(porez_potrosnja,',','.') AS numeric) as [porez] FROM roba ORDER BY CAST(REPLACE(porez_potrosnja,',','.') AS numeric) ASC;", "porezi").Tables[0];
                cbPorez.DisplayMember = "porez";
                cbPorez.ValueMember = "porez";
                cbPorez.DataSource = DTporezi;

                if (DTporezi.Select("porez=10").Length != 0)
                    cbPorez.SelectedValue = 10;
            }
            else
            {
                lblNapomena.Text = "Prije nego promijenite stopu poreza savjetujte se sa knjigovodstvom.\n\nU lijevom prozorčiću odaberite stopu poreza koju želite mijenjati, a u desnom prozorčiću odaberite postotak na koji želite stopu promijeniti.\n\nnpr.\nAko želite promijeniti stopu od 10 na 13 u lijevom prozorcicu odaberite 10, a potom u desnom izaberite 13.";

                DataTable DTporezi = classSQL.select("SELECT DISTINCT CAST(REPLACE(porez,',','.') AS numeric) as [porez] FROM roba ORDER BY CAST(REPLACE(porez,',','.') AS numeric) ASC;", "porezi").Tables[0];
                cbPorez.DisplayMember = "porez";
                cbPorez.ValueMember = "porez";
                cbPorez.DataSource = DTporezi;

                if (DTporezi.Select("porez=10").Length != 0)
                    cbPorez.SelectedValue = 10;
            }
        }

        private void cbPorez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dokument == "porez_na_potrosnju")
            {
                numPorez.Value = 0;
            }
            else
            {
                decimal parse;
                decimal.TryParse(cbPorez.SelectedValue.ToString(), out parse);
                if (parse == 10)
                    numPorez.Value = 13;
                else if (parse == 5)
                    numPorez.Value = 10;
                else
                    numPorez.Value = parse;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPromjeni_Click(object sender, EventArgs e)
        {
            int p;
            p = Convert.ToInt16(cbPorez.SelectedValue);

            int pset;
            pset = Convert.ToInt16(numPorez.Text.ToString());
            if (dokument == "porez_na_potrosnju")
            {
                string sql = "UPDATE roba SET porez_potrosnja='" + pset.ToString() + "' WHERE CAST(REPLACE(porez_potrosnja,',','.') AS numeric)='" + p.ToString() + "'";
                classSQL.update(sql);
            }
            else
            {
                string sql = "UPDATE roba SET porez='" + pset.ToString() + "' WHERE CAST(REPLACE(porez,',','.') AS numeric)='" + p.ToString() + "'";
                classSQL.update(sql);
            }

            this.Close();
        }
    }
}