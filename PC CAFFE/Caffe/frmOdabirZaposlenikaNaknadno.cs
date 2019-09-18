using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmOdabirZaposlenikaNaknadno : Form
    {
        public frmOdabirZaposlenikaNaknadno()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private bool _dodajDjelatnikaNaStavkuRacuna = false;
        public bool dodajDjelatnikaNaStavkuRacuna { get { return _dodajDjelatnikaNaStavkuRacuna; } set { _dodajDjelatnikaNaStavkuRacuna = value; } }
        private string sql = "";
        public frmCaffe caffe { get; set; }

        private void frmOdabirZaposlenikaNaknadno_Load(object sender, EventArgs e)
        {
            try
            {
                string sqlPomocni = "";
                if (dodajDjelatnikaNaStavkuRacuna)
                    sqlPomocni = @"select 0 as id_zaposlenik, 'OBRIŠI' as ime, 0 as sort
union
";

                sql = string.Format(@"SELECT id_zaposlenik, CONCAT(ime,' ',prezime) as ime, 1 as sort
FROM zaposlenici");

                DataTable DTzap = classSQL.select(string.Format(@"{0}{1}
WHERE aktivan='DA' AND (zaporka <> ',00000000000000000000,' and zaposlenici.ime not in ('Održavanje','PC') and zaposlenici.prezime not in ('Održavanje','PC'))
order by sort, ime;", sqlPomocni, sql), "zaposlenici").Tables[0];
                Button btn;
                foreach (DataRow row in DTzap.Rows)
                {
                    btn = new Button();
                    btn.Text = row["ime"].ToString();
                    btn.Name = row["id_zaposlenik"].ToString();
                    btn.BackColor = System.Drawing.Color.Transparent;
                    btn.BackgroundImage = null;
                    btn.ForeColor = SystemColors.ControlText;
                    btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btn.Cursor = System.Windows.Forms.Cursors.Hand;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(31, 53, 79);
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
                    btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
                    btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.Font = new System.Drawing.Font("Arial", 9.5F);
                    btn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                    //btn.Location = new System.Drawing.Point(400, 30);
                    btn.Size = new System.Drawing.Size(245, 35);
                    btn.TabIndex = 2;
                    btn.UseVisualStyleBackColor = false;
                    btn.Click += new System.EventHandler(this.btnzaposlenik_Click);

                    flowLayoutPanelZaposlenici.Controls.Add(btn);
                }

                //this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnzaposlenik_Click(object sender, EventArgs e)
        {
            try
            {
                Button conGrupa = (Button)sender;
                if (dodajDjelatnikaNaStavkuRacuna)
                {
                    int idZaposlenik = 0;
                    string nazivArtikla = "";
                    if (caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Tag != null && Int32.TryParse(caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Tag.ToString(), out idZaposlenik))
                    {
                        DataSet dsZaposlenik = classSQL.select(string.Format(@"{0}
where id_zaposlenik = {1}", sql, idZaposlenik), "zaposlenici");
                        if (dsZaposlenik != null && dsZaposlenik.Tables.Count > 0 && dsZaposlenik.Tables[0] != null && dsZaposlenik.Tables[0].Rows.Count > 0)
                        {
                            nazivArtikla = caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Cells[0].Value.ToString();

                            if (nazivArtikla.StartsWith(dsZaposlenik.Tables[0].Rows[0]["ime"].ToString()))
                            {
                                nazivArtikla = nazivArtikla.Remove(0, dsZaposlenik.Tables[0].Rows[0]["ime"].ToString().Length + 2);
                                caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Cells[0].Value = nazivArtikla;
                                caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Tag = null;
                            }
                        }
                    }

                    idZaposlenik = 0;

                    if (conGrupa.Name != null && Int32.TryParse(conGrupa.Name.ToString(), out idZaposlenik) && idZaposlenik > 0)
                    {
                        caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Tag = conGrupa.Name;
                        nazivArtikla = conGrupa.Text + ", " + caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Cells[0].Value.ToString();
                        caffe.dgw.Rows[caffe.dgw.CurrentCell.RowIndex].Cells[0].Value = nazivArtikla;
                    }
                }
                else
                {
                    string sql = "select id_dopustenje from zaposlenici where id_zaposlenik='" + conGrupa.Name + "'";
                    Properties.Settings.Default.id_dopustenje = int.Parse(classSQL.select(sql, "table").Tables[0].Rows[0][0].ToString());
                    Properties.Settings.Default.id_zaposlenik = conGrupa.Name;
                    Properties.Settings.Default.Save();
                    caffe.lblPrijavljen.Text = "Prijavljen: " + conGrupa.Text;
                }

                this.Close();
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
    }
}