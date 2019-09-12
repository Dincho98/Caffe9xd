namespace PCPOS.Caffe
{
    partial class frmStoloviZaNaplatuCustom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.runda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chb_naplati = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_podgrupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez_potrosnja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinuto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_zaposlenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polapola = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naplatiButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdresa = new System.Windows.Forms.Button();
            this.btnDodajPopust = new System.Windows.Forms.Button();
            this.btnIspisNarudzbe = new System.Windows.Forms.Button();
            this.btnPredRacun = new System.Windows.Forms.Button();
            this.btnOtpremnica = new System.Windows.Forms.Button();
            this.btnUdsGame = new System.Windows.Forms.Button();
            this.btnKoristiUds = new System.Windows.Forms.Button();
            this.btnIspisiSveNarudzbe = new System.Windows.Forms.Button();
            this.panelStolovi = new System.Windows.Forms.Panel();
            this.btnDellAll = new System.Windows.Forms.Button();
            this.btnPosaljiUKuhinju = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnNaplati = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.lblDostava = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.AllowUserToOrderColumns = true;
            this.dgw.AllowUserToResizeRows = false;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.BackgroundColor = System.Drawing.Color.Silver;
            this.dgw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgw.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.runda,
            this.naziv,
            this.kolicina,
            this.cijena,
            this.chb_naplati,
            this.id_podgrupa,
            this.sifra,
            this.id_skladiste,
            this.porez,
            this.porez_potrosnja,
            this.vpc,
            this.br,
            this.jelo,
            this.skinuto,
            this.id_zaposlenik,
            this.dod,
            this.polapola,
            this.rabat,
            this.naplatiButton});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EnableHeadersVisualStyles = false;
            this.dgw.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(180)))));
            this.dgw.Location = new System.Drawing.Point(695, 71);
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersVisible = false;
            this.dgw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(310, 384);
            this.dgw.TabIndex = 136;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // runda
            // 
            this.runda.FillWeight = 70F;
            this.runda.HeaderText = "Run";
            this.runda.Name = "runda";
            this.runda.ReadOnly = true;
            this.runda.ToolTipText = "Runda";
            this.runda.Width = 40;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Kol";
            this.kolicina.Name = "kolicina";
            this.kolicina.ReadOnly = true;
            this.kolicina.Width = 50;
            // 
            // cijena
            // 
            this.cijena.FillWeight = 90F;
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            this.cijena.Width = 60;
            // 
            // chb_naplati
            // 
            this.chb_naplati.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.NullValue = false;
            this.chb_naplati.DefaultCellStyle = dataGridViewCellStyle1;
            this.chb_naplati.FillWeight = 10F;
            this.chb_naplati.HeaderText = "Naplati";
            this.chb_naplati.Name = "chb_naplati";
            this.chb_naplati.ReadOnly = true;
            this.chb_naplati.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chb_naplati.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chb_naplati.Visible = false;
            this.chb_naplati.Width = 60;
            // 
            // id_podgrupa
            // 
            this.id_podgrupa.HeaderText = "id_podgrupa";
            this.id_podgrupa.Name = "id_podgrupa";
            this.id_podgrupa.ReadOnly = true;
            this.id_podgrupa.Visible = false;
            // 
            // sifra
            // 
            this.sifra.FillWeight = 30F;
            this.sifra.HeaderText = "sifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Visible = false;
            this.sifra.Width = 30;
            // 
            // id_skladiste
            // 
            this.id_skladiste.HeaderText = "id_skladiste";
            this.id_skladiste.Name = "id_skladiste";
            this.id_skladiste.ReadOnly = true;
            this.id_skladiste.Visible = false;
            // 
            // porez
            // 
            this.porez.HeaderText = "porez";
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            this.porez.Visible = false;
            // 
            // porez_potrosnja
            // 
            this.porez_potrosnja.HeaderText = "porez_potrosnja";
            this.porez_potrosnja.Name = "porez_potrosnja";
            this.porez_potrosnja.ReadOnly = true;
            this.porez_potrosnja.Visible = false;
            // 
            // vpc
            // 
            this.vpc.HeaderText = "vpc";
            this.vpc.Name = "vpc";
            this.vpc.ReadOnly = true;
            this.vpc.Visible = false;
            // 
            // br
            // 
            this.br.HeaderText = "br";
            this.br.Name = "br";
            this.br.ReadOnly = true;
            this.br.Visible = false;
            // 
            // jelo
            // 
            this.jelo.HeaderText = "jelo";
            this.jelo.Name = "jelo";
            this.jelo.ReadOnly = true;
            this.jelo.Visible = false;
            // 
            // skinuto
            // 
            this.skinuto.HeaderText = "skinuto";
            this.skinuto.Name = "skinuto";
            this.skinuto.ReadOnly = true;
            this.skinuto.Visible = false;
            // 
            // id_zaposlenik
            // 
            this.id_zaposlenik.HeaderText = "id_zaposlenik";
            this.id_zaposlenik.Name = "id_zaposlenik";
            this.id_zaposlenik.ReadOnly = true;
            this.id_zaposlenik.Visible = false;
            // 
            // dod
            // 
            this.dod.HeaderText = "dod";
            this.dod.Name = "dod";
            this.dod.ReadOnly = true;
            this.dod.Visible = false;
            // 
            // polapola
            // 
            this.polapola.HeaderText = "polapola";
            this.polapola.Name = "polapola";
            this.polapola.ReadOnly = true;
            this.polapola.Visible = false;
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat";
            this.rabat.Name = "rabat";
            this.rabat.ReadOnly = true;
            this.rabat.Visible = false;
            // 
            // naplatiButton
            // 
            this.naplatiButton.FillWeight = 50F;
            this.naplatiButton.HeaderText = "Naplatiti";
            this.naplatiButton.Name = "naplatiButton";
            this.naplatiButton.ReadOnly = true;
            this.naplatiButton.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 37);
            this.label1.TabIndex = 137;
            this.label1.Text = "Stolovi za naplatu";
            // 
            // btnAdresa
            // 
            this.btnAdresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdresa.BackColor = System.Drawing.Color.White;
            this.btnAdresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdresa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnAdresa.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnAdresa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnAdresa.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnAdresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdresa.ForeColor = System.Drawing.Color.Black;
            this.btnAdresa.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAdresa.Location = new System.Drawing.Point(517, 9);
            this.btnAdresa.Name = "btnAdresa";
            this.btnAdresa.Size = new System.Drawing.Size(76, 56);
            this.btnAdresa.TabIndex = 145;
            this.btnAdresa.Text = "Adresa";
            this.toolTip1.SetToolTip(this.btnAdresa, "Dodavanje adrese za narudžbu");
            this.btnAdresa.UseVisualStyleBackColor = false;
            this.btnAdresa.Click += new System.EventHandler(this.btnAdresa_Click);
            // 
            // btnDodajPopust
            // 
            this.btnDodajPopust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodajPopust.BackColor = System.Drawing.Color.White;
            this.btnDodajPopust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajPopust.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajPopust.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajPopust.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajPopust.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajPopust.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajPopust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajPopust.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnDodajPopust.ForeColor = System.Drawing.Color.Black;
            this.btnDodajPopust.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDodajPopust.Location = new System.Drawing.Point(388, 9);
            this.btnDodajPopust.Name = "btnDodajPopust";
            this.btnDodajPopust.Size = new System.Drawing.Size(124, 56);
            this.btnDodajPopust.TabIndex = 143;
            this.btnDodajPopust.Text = "Dodaj popust na artikl";
            this.toolTip1.SetToolTip(this.btnDodajPopust, "Predračun:\r\nIspisuje se sve isto kao i na računu \r\nsamo nije fiskalizirano, običn" +
        "o se izdaje\r\nprema želji gosta prije \r\npravog fiskaliziranog računa.");
            this.btnDodajPopust.UseVisualStyleBackColor = false;
            this.btnDodajPopust.Click += new System.EventHandler(this.btnDodajPopust_Click);
            // 
            // btnIspisNarudzbe
            // 
            this.btnIspisNarudzbe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspisNarudzbe.BackColor = System.Drawing.Color.White;
            this.btnIspisNarudzbe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisNarudzbe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisNarudzbe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspisNarudzbe.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisNarudzbe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisNarudzbe.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspisNarudzbe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisNarudzbe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIspisNarudzbe.ForeColor = System.Drawing.Color.Black;
            this.btnIspisNarudzbe.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspisNarudzbe.Location = new System.Drawing.Point(695, 6);
            this.btnIspisNarudzbe.Name = "btnIspisNarudzbe";
            this.btnIspisNarudzbe.Size = new System.Drawing.Size(111, 59);
            this.btnIspisNarudzbe.TabIndex = 142;
            this.btnIspisNarudzbe.Text = "Ispis odabrane narudžbe F5";
            this.toolTip1.SetToolTip(this.btnIspisNarudzbe, "Ispis naruđbe:\r\nIspisuje se zadnja runda na stolu i \r\nako ima nešto od hrane šalj" +
        "e u \r\nkuhinju na printer.");
            this.btnIspisNarudzbe.UseVisualStyleBackColor = false;
            this.btnIspisNarudzbe.Click += new System.EventHandler(this.btnIspisNarudzbe_Click);
            // 
            // btnPredRacun
            // 
            this.btnPredRacun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPredRacun.BackColor = System.Drawing.Color.White;
            this.btnPredRacun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPredRacun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPredRacun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPredRacun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPredRacun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPredRacun.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPredRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPredRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPredRacun.ForeColor = System.Drawing.Color.Black;
            this.btnPredRacun.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPredRacun.Location = new System.Drawing.Point(691, 461);
            this.btnPredRacun.Name = "btnPredRacun";
            this.btnPredRacun.Size = new System.Drawing.Size(157, 58);
            this.btnPredRacun.TabIndex = 139;
            this.btnPredRacun.Text = "Predračun F2";
            this.toolTip1.SetToolTip(this.btnPredRacun, "Predračun:\r\nIspisuje se sve isto kao i na računu \r\nsamo nije fiskalizirano, običn" +
        "o se izdaje\r\nprema želji gosta prije \r\npravog fiskaliziranog računa.");
            this.btnPredRacun.UseVisualStyleBackColor = false;
            this.btnPredRacun.Click += new System.EventHandler(this.btnPredRacun_Click);
            this.btnPredRacun.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // btnOtpremnica
            // 
            this.btnOtpremnica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOtpremnica.BackColor = System.Drawing.Color.White;
            this.btnOtpremnica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtpremnica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtpremnica.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOtpremnica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOtpremnica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOtpremnica.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOtpremnica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtpremnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnOtpremnica.ForeColor = System.Drawing.Color.Black;
            this.btnOtpremnica.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOtpremnica.Location = new System.Drawing.Point(258, 9);
            this.btnOtpremnica.Name = "btnOtpremnica";
            this.btnOtpremnica.Size = new System.Drawing.Size(124, 56);
            this.btnOtpremnica.TabIndex = 149;
            this.btnOtpremnica.Text = "Otpremnica";
            this.toolTip1.SetToolTip(this.btnOtpremnica, "Predračun:\r\nIspisuje se sve isto kao i na računu \r\nsamo nije fiskalizirano, običn" +
        "o se izdaje\r\nprema želji gosta prije \r\npravog fiskaliziranog računa.");
            this.btnOtpremnica.UseVisualStyleBackColor = false;
            this.btnOtpremnica.Click += new System.EventHandler(this.btnOtpremnica_Click);
            // 
            // btnUdsGame
            // 
            this.btnUdsGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUdsGame.BackColor = System.Drawing.Color.White;
            this.btnUdsGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUdsGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUdsGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUdsGame.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnUdsGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUdsGame.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUdsGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUdsGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUdsGame.ForeColor = System.Drawing.Color.Black;
            this.btnUdsGame.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUdsGame.Location = new System.Drawing.Point(52, 10);
            this.btnUdsGame.Name = "btnUdsGame";
            this.btnUdsGame.Size = new System.Drawing.Size(97, 56);
            this.btnUdsGame.TabIndex = 150;
            this.btnUdsGame.Text = "UDS Game";
            this.toolTip1.SetToolTip(this.btnUdsGame, "Predračun:\r\nIspisuje se sve isto kao i na računu \r\nsamo nije fiskalizirano, običn" +
        "o se izdaje\r\nprema želji gosta prije \r\npravog fiskaliziranog računa.");
            this.btnUdsGame.UseVisualStyleBackColor = false;
            this.btnUdsGame.Visible = false;
            this.btnUdsGame.Click += new System.EventHandler(this.btnUdsGame_Click);
            // 
            // btnKoristiUds
            // 
            this.btnKoristiUds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKoristiUds.BackColor = System.Drawing.Color.White;
            this.btnKoristiUds.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKoristiUds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKoristiUds.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnKoristiUds.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnKoristiUds.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnKoristiUds.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnKoristiUds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKoristiUds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnKoristiUds.ForeColor = System.Drawing.Color.Black;
            this.btnKoristiUds.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnKoristiUds.Location = new System.Drawing.Point(29, 9);
            this.btnKoristiUds.Name = "btnKoristiUds";
            this.btnKoristiUds.Size = new System.Drawing.Size(120, 56);
            this.btnKoristiUds.TabIndex = 151;
            this.btnKoristiUds.Text = "UDS bodovi za";
            this.toolTip1.SetToolTip(this.btnKoristiUds, "Predračun:\r\nIspisuje se sve isto kao i na računu \r\nsamo nije fiskalizirano, običn" +
        "o se izdaje\r\nprema želji gosta prije \r\npravog fiskaliziranog računa.");
            this.btnKoristiUds.UseVisualStyleBackColor = false;
            this.btnKoristiUds.Visible = false;
            this.btnKoristiUds.Click += new System.EventHandler(this.btnKoristiUds_Click);
            // 
            // btnIspisiSveNarudzbe
            // 
            this.btnIspisiSveNarudzbe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspisiSveNarudzbe.BackColor = System.Drawing.Color.White;
            this.btnIspisiSveNarudzbe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisiSveNarudzbe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisiSveNarudzbe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspisiSveNarudzbe.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisiSveNarudzbe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisiSveNarudzbe.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspisiSveNarudzbe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisiSveNarudzbe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIspisiSveNarudzbe.ForeColor = System.Drawing.Color.Black;
            this.btnIspisiSveNarudzbe.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspisiSveNarudzbe.Location = new System.Drawing.Point(598, 9);
            this.btnIspisiSveNarudzbe.Name = "btnIspisiSveNarudzbe";
            this.btnIspisiSveNarudzbe.Size = new System.Drawing.Size(92, 56);
            this.btnIspisiSveNarudzbe.TabIndex = 147;
            this.btnIspisiSveNarudzbe.Text = "Ispis SVIH narudžbi";
            this.btnIspisiSveNarudzbe.UseVisualStyleBackColor = false;
            this.btnIspisiSveNarudzbe.Visible = false;
            this.btnIspisiSveNarudzbe.Click += new System.EventHandler(this.btnSveUKuhinju_Click);
            // 
            // panelStolovi
            // 
            this.panelStolovi.AllowDrop = true;
            this.panelStolovi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStolovi.BackColor = System.Drawing.Color.Silver;
            this.panelStolovi.Location = new System.Drawing.Point(11, 71);
            this.panelStolovi.Name = "panelStolovi";
            this.panelStolovi.Size = new System.Drawing.Size(674, 520);
            this.panelStolovi.TabIndex = 144;
            // 
            // btnDellAll
            // 
            this.btnDellAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDellAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDellAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDellAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDellAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDellAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDellAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDellAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDellAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDellAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnDellAll.ForeColor = System.Drawing.Color.White;
            this.btnDellAll.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDellAll.Location = new System.Drawing.Point(811, 6);
            this.btnDellAll.Name = "btnDellAll";
            this.btnDellAll.Size = new System.Drawing.Size(93, 59);
            this.btnDellAll.TabIndex = 141;
            this.btnDellAll.Text = "Obriši sve(DEL)";
            this.btnDellAll.UseVisualStyleBackColor = false;
            this.btnDellAll.Click += new System.EventHandler(this.btnDellAll_Click);
            // 
            // btnPosaljiUKuhinju
            // 
            this.btnPosaljiUKuhinju.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPosaljiUKuhinju.BackColor = System.Drawing.Color.White;
            this.btnPosaljiUKuhinju.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPosaljiUKuhinju.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPosaljiUKuhinju.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPosaljiUKuhinju.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPosaljiUKuhinju.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPosaljiUKuhinju.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPosaljiUKuhinju.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosaljiUKuhinju.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPosaljiUKuhinju.ForeColor = System.Drawing.Color.Black;
            this.btnPosaljiUKuhinju.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPosaljiUKuhinju.Location = new System.Drawing.Point(850, 462);
            this.btnPosaljiUKuhinju.Name = "btnPosaljiUKuhinju";
            this.btnPosaljiUKuhinju.Size = new System.Drawing.Size(149, 57);
            this.btnPosaljiUKuhinju.TabIndex = 140;
            this.btnPosaljiUKuhinju.Text = "Pošalji narudžbe F4";
            this.btnPosaljiUKuhinju.UseVisualStyleBackColor = false;
            this.btnPosaljiUKuhinju.Click += new System.EventHandler(this.btnPosaljiUKuhinju_Click);
            this.btnPosaljiUKuhinju.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnObrisi.ForeColor = System.Drawing.Color.White;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnObrisi.Location = new System.Drawing.Point(691, 524);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(157, 69);
            this.btnObrisi.TabIndex = 140;
            this.btnObrisi.Text = "Obriši F3";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.button2_Click);
            this.btnObrisi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // btnNaplati
            // 
            this.btnNaplati.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNaplati.BackColor = System.Drawing.Color.White;
            this.btnNaplati.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNaplati.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNaplati.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNaplati.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNaplati.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNaplati.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNaplati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaplati.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnNaplati.ForeColor = System.Drawing.Color.Black;
            this.btnNaplati.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNaplati.Location = new System.Drawing.Point(850, 524);
            this.btnNaplati.Name = "btnNaplati";
            this.btnNaplati.Size = new System.Drawing.Size(150, 69);
            this.btnNaplati.TabIndex = 138;
            this.btnNaplati.Text = "Naplati F1";
            this.btnNaplati.UseVisualStyleBackColor = false;
            this.btnNaplati.Click += new System.EventHandler(this.btnNaplati_Click);
            this.btnNaplati.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // btnESC
            // 
            this.btnESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnESC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnESC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnESC.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnESC.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnESC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnESC.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnESC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnESC.ForeColor = System.Drawing.Color.White;
            this.btnESC.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnESC.Location = new System.Drawing.Point(908, 6);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(91, 59);
            this.btnESC.TabIndex = 124;
            this.btnESC.Text = "Odustani\r\nESC";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.button6_Click);
            this.btnESC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllKeyDown);
            // 
            // lblDostava
            // 
            this.lblDostava.AutoSize = true;
            this.lblDostava.Location = new System.Drawing.Point(8, 55);
            this.lblDostava.Name = "lblDostava";
            this.lblDostava.Size = new System.Drawing.Size(50, 13);
            this.lblDostava.TabIndex = 146;
            this.lblDostava.Text = "Dostava:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(156, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 56);
            this.button1.TabIndex = 152;
            this.button1.Text = "Stolovi - Napredno";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmStoloviZaNaplatuCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1017, 603);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnKoristiUds);
            this.Controls.Add(this.btnUdsGame);
            this.Controls.Add(this.btnOtpremnica);
            this.Controls.Add(this.btnIspisiSveNarudzbe);
            this.Controls.Add(this.lblDostava);
            this.Controls.Add(this.btnAdresa);
            this.Controls.Add(this.panelStolovi);
            this.Controls.Add(this.btnDodajPopust);
            this.Controls.Add(this.btnIspisNarudzbe);
            this.Controls.Add(this.btnDellAll);
            this.Controls.Add(this.btnPosaljiUKuhinju);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnPredRacun);
            this.Controls.Add(this.btnNaplati);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.btnESC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStoloviZaNaplatuCustom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stolovi za naplatu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmStoloviZaNaplatu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNaplati;
        private System.Windows.Forms.Button btnPredRacun;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnPosaljiUKuhinju;
        private System.Windows.Forms.Button btnDellAll;
        private System.Windows.Forms.Button btnIspisNarudzbe;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDodajPopust;
        public System.Windows.Forms.DataGridView dgw;
        public System.Windows.Forms.Panel panelStolovi;
        private System.Windows.Forms.Button btnAdresa;
        private System.Windows.Forms.Label lblDostava;
        private System.Windows.Forms.Button btnIspisiSveNarudzbe;
        private System.Windows.Forms.Button btnOtpremnica;
        private System.Windows.Forms.Button btnUdsGame;
        private System.Windows.Forms.Button btnKoristiUds;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn runda;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chb_naplati;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_podgrupa;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez_potrosnja;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn jelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn skinuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_zaposlenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn dod;
        private System.Windows.Forms.DataGridViewTextBoxColumn polapola;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewButtonColumn naplatiButton;
    }
}