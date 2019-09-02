namespace PCPOS
{
    partial class frmFaktura
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaktura));
            this.sa = new PCPOS.frmFaktura.MyDataGrid();
            this.dgw = new PCPOS.frmFaktura.MyDataGrid();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_roba_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez_potrosnja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.povratna_naknada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttxBrojFakture = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmGodinaFakture = new System.Windows.Forms.NumericUpDown();
            this.chbOdizmiIzSkladista = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.nuGodinaPonude = new System.Windows.Forms.NumericUpDown();
            this.txtBrojPonude = new System.Windows.Forms.TextBox();
            this.btnPartner1 = new System.Windows.Forms.PictureBox();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.cbNacinPlacanja = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSifraNacinPlacanja = new System.Windows.Forms.TextBox();
            this.cbOtprema = new System.Windows.Forms.ComboBox();
            this.nuGodinaPredujma = new System.Windows.Forms.NumericUpDown();
            this.btnPredujam = new System.Windows.Forms.Button();
            this.btnNarKupca = new System.Windows.Forms.Button();
            this.btnRadniNalog = new System.Windows.Forms.Button();
            this.cbVD = new System.Windows.Forms.ComboBox();
            this.cbZiroRacun = new System.Windows.Forms.ComboBox();
            this.cbNarKupca = new System.Windows.Forms.ComboBox();
            this.cbRadniBalog = new System.Windows.Forms.ComboBox();
            this.cbPredujam = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.cbKomercijalist = new System.Windows.Forms.ComboBox();
            this.cbIzjava = new System.Windows.Forms.ComboBox();
            this.dtpDanaValuta = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDatumDVO = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtTecaj = new System.Windows.Forms.TextBox();
            this.txtDana = new System.Windows.Forms.TextBox();
            this.txtIznosPredujma = new System.Windows.Forms.TextBox();
            this.txtNarKupca1 = new System.Windows.Forms.TextBox();
            this.txtSifraNarKupca = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv1 = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.txtSifraFakturirati = new System.Windows.Forms.TextBox();
            this.txtSifraRadniNalog = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOdaberiOtpremnice = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.sa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodinaPonude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodinaPredujma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // sa
            // 
            this.sa.Location = new System.Drawing.Point(0, 0);
            this.sa.Name = "sa";
            this.sa.Size = new System.Drawing.Size(240, 150);
            this.sa.TabIndex = 0;
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.sifra,
            this.naziv,
            this.skladiste,
            this.jmj,
            this.kolicina,
            this.porez,
            this.mpc,
            this.rabat,
            this.rabat_iznos,
            this.cijena_bez_pdva,
            this.iznos_bez_pdva,
            this.iznos_ukupno,
            this.vpc,
            this.nc,
            this.id_stavka,
            this.id_roba_prodaja,
            this.oduzmi,
            this.porez_potrosnja,
            this.povratna_naknada});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 445);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(969, 103);
            this.dgw.TabIndex = 0;
            this.dgw.TabStop = false;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // br
            // 
            this.br.FillWeight = 50F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            // 
            // sifra
            // 
            this.sifra.FillWeight = 61.10954F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 61.10954F;
            this.naziv.HeaderText = "Naziv robe ili usluge";
            this.naziv.MinimumWidth = 130;
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // skladiste
            // 
            this.skladiste.DataPropertyName = "sifra";
            this.skladiste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.Visible = false;
            // 
            // jmj
            // 
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 61.10954F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // porez
            // 
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            // 
            // mpc
            // 
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            // 
            // rabat
            // 
            this.rabat.FillWeight = 61.10954F;
            this.rabat.HeaderText = "Rabat%";
            this.rabat.Name = "rabat";
            // 
            // rabat_iznos
            // 
            this.rabat_iznos.HeaderText = "Rabat iznos";
            this.rabat_iznos.Name = "rabat_iznos";
            this.rabat_iznos.ReadOnly = true;
            // 
            // cijena_bez_pdva
            // 
            this.cijena_bez_pdva.FillWeight = 120F;
            this.cijena_bez_pdva.HeaderText = "Cijena bez pdv-a";
            this.cijena_bez_pdva.Name = "cijena_bez_pdva";
            this.cijena_bez_pdva.ReadOnly = true;
            // 
            // iznos_bez_pdva
            // 
            this.iznos_bez_pdva.FillWeight = 120F;
            this.iznos_bez_pdva.HeaderText = "Iznos bez pdv-a";
            this.iznos_bez_pdva.Name = "iznos_bez_pdva";
            this.iznos_bez_pdva.ReadOnly = true;
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            this.iznos_ukupno.ReadOnly = true;
            // 
            // vpc
            // 
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            this.vpc.Visible = false;
            // 
            // nc
            // 
            this.nc.HeaderText = "nc";
            this.nc.Name = "nc";
            this.nc.Visible = false;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // id_roba_prodaja
            // 
            this.id_roba_prodaja.HeaderText = "id_roba_prodaja";
            this.id_roba_prodaja.Name = "id_roba_prodaja";
            this.id_roba_prodaja.Visible = false;
            // 
            // oduzmi
            // 
            this.oduzmi.HeaderText = "oduzmi";
            this.oduzmi.Name = "oduzmi";
            this.oduzmi.Visible = false;
            // 
            // porez_potrosnja
            // 
            this.porez_potrosnja.HeaderText = "porez_potrosnja";
            this.porez_potrosnja.Name = "porez_potrosnja";
            this.porez_potrosnja.Visible = false;
            // 
            // povratna_naknada
            // 
            this.povratna_naknada.HeaderText = "povratna_naknada";
            this.povratna_naknada.Name = "povratna_naknada";
            this.povratna_naknada.Visible = false;
            // 
            // ttxBrojFakture
            // 
            this.ttxBrojFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ttxBrojFakture.Location = new System.Drawing.Point(105, 6);
            this.ttxBrojFakture.Name = "ttxBrojFakture";
            this.ttxBrojFakture.Size = new System.Drawing.Size(86, 23);
            this.ttxBrojFakture.TabIndex = 1;
            this.ttxBrojFakture.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.ttxBrojFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.ttxBrojFakture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.ttxBrojFakture.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj fakture:";
            // 
            // nmGodinaFakture
            // 
            this.nmGodinaFakture.BackColor = System.Drawing.Color.White;
            this.nmGodinaFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nmGodinaFakture.Location = new System.Drawing.Point(196, 6);
            this.nmGodinaFakture.Name = "nmGodinaFakture";
            this.nmGodinaFakture.Size = new System.Drawing.Size(87, 23);
            this.nmGodinaFakture.TabIndex = 2;
            this.nmGodinaFakture.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodinaFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.nmGodinaFakture.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // chbOdizmiIzSkladista
            // 
            this.chbOdizmiIzSkladista.AutoSize = true;
            this.chbOdizmiIzSkladista.Location = new System.Drawing.Point(779, 9);
            this.chbOdizmiIzSkladista.Name = "chbOdizmiIzSkladista";
            this.chbOdizmiIzSkladista.Size = new System.Drawing.Size(139, 17);
            this.chbOdizmiIzSkladista.TabIndex = 65;
            this.chbOdizmiIzSkladista.Text = "Oduzmi robu iz skladišta";
            this.chbOdizmiIzSkladista.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(673, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 24);
            this.button2.TabIndex = 64;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Maroon;
            this.label9.Location = new System.Drawing.Point(335, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 17);
            this.label9.TabIndex = 62;
            this.label9.Text = "Preuzmi stavke iz ponude:";
            // 
            // nuGodinaPonude
            // 
            this.nuGodinaPonude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nuGodinaPonude.Location = new System.Drawing.Point(624, 6);
            this.nuGodinaPonude.Name = "nuGodinaPonude";
            this.nuGodinaPonude.Size = new System.Drawing.Size(49, 23);
            this.nuGodinaPonude.TabIndex = 4;
            this.nuGodinaPonude.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nuGodinaPonude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojPonude_KeyDown_1);
            this.nuGodinaPonude.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtBrojPonude
            // 
            this.txtBrojPonude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBrojPonude.Location = new System.Drawing.Point(540, 6);
            this.txtBrojPonude.Name = "txtBrojPonude";
            this.txtBrojPonude.Size = new System.Drawing.Size(83, 23);
            this.txtBrojPonude.TabIndex = 3;
            this.txtBrojPonude.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBrojPonude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojPonude_KeyDown_1);
            this.txtBrojPonude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtBrojPonude.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // btnPartner1
            // 
            this.btnPartner1.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner1.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner1.Image")));
            this.btnPartner1.Location = new System.Drawing.Point(158, 35);
            this.btnPartner1.Name = "btnPartner1";
            this.btnPartner1.Size = new System.Drawing.Size(31, 28);
            this.btnPartner1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner1.TabIndex = 557;
            this.btnPartner1.TabStop = false;
            this.btnPartner1.Click += new System.EventHandler(this.btnPartner1_Click);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(158, 11);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 557;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Location = new System.Drawing.Point(432, 171);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(529, 76);
            this.rtbNapomena.TabIndex = 556;
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
            // 
            // cbNacinPlacanja
            // 
            this.cbNacinPlacanja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNacinPlacanja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNacinPlacanja.BackColor = System.Drawing.Color.White;
            this.cbNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNacinPlacanja.FormattingEnabled = true;
            this.cbNacinPlacanja.Location = new System.Drawing.Point(476, 138);
            this.cbNacinPlacanja.Name = "cbNacinPlacanja";
            this.cbNacinPlacanja.Size = new System.Drawing.Size(163, 24);
            this.cbNacinPlacanja.TabIndex = 18;
            this.cbNacinPlacanja.SelectedIndexChanged += new System.EventHandler(this.cbNacinPlacanja_SelectedIndexChanged);
            this.cbNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbNacinPlacanja_KeyDown_1);
            this.cbNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(342, 141);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 64;
            this.label15.Text = "Način plać.:";
            // 
            // txtSifraNacinPlacanja
            // 
            this.txtSifraNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSifraNacinPlacanja.Location = new System.Drawing.Point(432, 138);
            this.txtSifraNacinPlacanja.Name = "txtSifraNacinPlacanja";
            this.txtSifraNacinPlacanja.Size = new System.Drawing.Size(44, 24);
            this.txtSifraNacinPlacanja.TabIndex = 17;
            this.txtSifraNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraNacinPlacanja_KeyDown);
            this.txtSifraNacinPlacanja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbOtprema
            // 
            this.cbOtprema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbOtprema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbOtprema.BackColor = System.Drawing.Color.White;
            this.cbOtprema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbOtprema.FormattingEnabled = true;
            this.cbOtprema.Location = new System.Drawing.Point(754, 83);
            this.cbOtprema.Name = "cbOtprema";
            this.cbOtprema.Size = new System.Drawing.Size(207, 24);
            this.cbOtprema.TabIndex = 22;
            this.cbOtprema.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbOtprema.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbOtprema_KeyDown);
            this.cbOtprema.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nuGodinaPredujma
            // 
            this.nuGodinaPredujma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nuGodinaPredujma.Location = new System.Drawing.Point(754, 110);
            this.nuGodinaPredujma.Name = "nuGodinaPredujma";
            this.nuGodinaPredujma.Size = new System.Drawing.Size(48, 23);
            this.nuGodinaPredujma.TabIndex = 23;
            this.nuGodinaPredujma.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nuGodinaPredujma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nuGodinaPredujma_KeyDown);
            this.nuGodinaPredujma.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // btnPredujam
            // 
            this.btnPredujam.BackColor = System.Drawing.Color.White;
            this.btnPredujam.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPredujam.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPredujam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPredujam.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPredujam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPredujam.Location = new System.Drawing.Point(934, 110);
            this.btnPredujam.Name = "btnPredujam";
            this.btnPredujam.Size = new System.Drawing.Size(28, 24);
            this.btnPredujam.TabIndex = 60;
            this.btnPredujam.Text = "...";
            this.btnPredujam.UseVisualStyleBackColor = false;
            // 
            // btnNarKupca
            // 
            this.btnNarKupca.BackColor = System.Drawing.Color.White;
            this.btnNarKupca.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNarKupca.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNarKupca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNarKupca.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNarKupca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNarKupca.Location = new System.Drawing.Point(934, 30);
            this.btnNarKupca.Name = "btnNarKupca";
            this.btnNarKupca.Size = new System.Drawing.Size(28, 25);
            this.btnNarKupca.TabIndex = 60;
            this.btnNarKupca.Text = "...";
            this.btnNarKupca.UseVisualStyleBackColor = false;
            // 
            // btnRadniNalog
            // 
            this.btnRadniNalog.BackColor = System.Drawing.Color.White;
            this.btnRadniNalog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnRadniNalog.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnRadniNalog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnRadniNalog.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRadniNalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRadniNalog.Location = new System.Drawing.Point(934, 2);
            this.btnRadniNalog.Name = "btnRadniNalog";
            this.btnRadniNalog.Size = new System.Drawing.Size(28, 26);
            this.btnRadniNalog.TabIndex = 60;
            this.btnRadniNalog.Text = "...";
            this.btnRadniNalog.UseVisualStyleBackColor = false;
            // 
            // cbVD
            // 
            this.cbVD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbVD.BackColor = System.Drawing.Color.White;
            this.cbVD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbVD.FormattingEnabled = true;
            this.cbVD.Location = new System.Drawing.Point(106, 63);
            this.cbVD.Name = "cbVD";
            this.cbVD.Size = new System.Drawing.Size(208, 24);
            this.cbVD.TabIndex = 7;
            this.cbVD.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbVD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbVD_KeyDown);
            this.cbVD.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbZiroRacun
            // 
            this.cbZiroRacun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZiroRacun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZiroRacun.BackColor = System.Drawing.Color.White;
            this.cbZiroRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZiroRacun.FormattingEnabled = true;
            this.cbZiroRacun.Location = new System.Drawing.Point(432, 55);
            this.cbZiroRacun.Name = "cbZiroRacun";
            this.cbZiroRacun.Size = new System.Drawing.Size(208, 24);
            this.cbZiroRacun.TabIndex = 15;
            this.cbZiroRacun.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbZiroRacun.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbZiroRacun_KeyDown);
            this.cbZiroRacun.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbNarKupca
            // 
            this.cbNarKupca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNarKupca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNarKupca.BackColor = System.Drawing.Color.White;
            this.cbNarKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNarKupca.FormattingEnabled = true;
            this.cbNarKupca.Location = new System.Drawing.Point(808, 31);
            this.cbNarKupca.Name = "cbNarKupca";
            this.cbNarKupca.Size = new System.Drawing.Size(126, 24);
            this.cbNarKupca.TabIndex = 48;
            this.cbNarKupca.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbNarKupca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbNarKupca_KeyDown);
            this.cbNarKupca.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbRadniBalog
            // 
            this.cbRadniBalog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbRadniBalog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbRadniBalog.BackColor = System.Drawing.Color.White;
            this.cbRadniBalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbRadniBalog.FormattingEnabled = true;
            this.cbRadniBalog.Location = new System.Drawing.Point(808, 4);
            this.cbRadniBalog.Name = "cbRadniBalog";
            this.cbRadniBalog.Size = new System.Drawing.Size(126, 24);
            this.cbRadniBalog.TabIndex = 48;
            this.cbRadniBalog.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbRadniBalog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbRadniBalog_KeyDown);
            this.cbRadniBalog.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbPredujam
            // 
            this.cbPredujam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbPredujam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPredujam.BackColor = System.Drawing.Color.White;
            this.cbPredujam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbPredujam.FormattingEnabled = true;
            this.cbPredujam.Location = new System.Drawing.Point(803, 110);
            this.cbPredujam.Name = "cbPredujam";
            this.cbPredujam.Size = new System.Drawing.Size(132, 24);
            this.cbPredujam.TabIndex = 24;
            this.cbPredujam.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbPredujam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPredujam_KeyDown);
            this.cbPredujam.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.BackColor = System.Drawing.Color.White;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(432, 83);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(208, 24);
            this.cbValuta.TabIndex = 16;
            this.cbValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbValuta_KeyDown);
            this.cbValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbKomercijalist
            // 
            this.cbKomercijalist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbKomercijalist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbKomercijalist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKomercijalist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbKomercijalist.FormattingEnabled = true;
            this.cbKomercijalist.Location = new System.Drawing.Point(106, 223);
            this.cbKomercijalist.Name = "cbKomercijalist";
            this.cbKomercijalist.Size = new System.Drawing.Size(208, 24);
            this.cbKomercijalist.TabIndex = 13;
            this.cbKomercijalist.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbKomercijalist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbKomercijalist_KeyDown);
            this.cbKomercijalist.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbIzjava
            // 
            this.cbIzjava.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbIzjava.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIzjava.BackColor = System.Drawing.Color.White;
            this.cbIzjava.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzjava.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbIzjava.FormattingEnabled = true;
            this.cbIzjava.Location = new System.Drawing.Point(106, 195);
            this.cbIzjava.Name = "cbIzjava";
            this.cbIzjava.Size = new System.Drawing.Size(208, 24);
            this.cbIzjava.TabIndex = 12;
            this.cbIzjava.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbIzjava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbIzjava_KeyDown);
            this.cbIzjava.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDanaValuta
            // 
            this.dtpDanaValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDanaValuta.Location = new System.Drawing.Point(106, 169);
            this.dtpDanaValuta.Name = "dtpDanaValuta";
            this.dtpDanaValuta.Size = new System.Drawing.Size(208, 23);
            this.dtpDanaValuta.TabIndex = 11;
            this.dtpDanaValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDanaValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDanaValuta_KeyDown);
            this.dtpDanaValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(342, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Izradio:";
            // 
            // dtpDatumDVO
            // 
            this.dtpDatumDVO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDVO.Location = new System.Drawing.Point(106, 117);
            this.dtpDatumDVO.Name = "dtpDatumDVO";
            this.dtpDatumDVO.Size = new System.Drawing.Size(208, 23);
            this.dtpDatumDVO.TabIndex = 9;
            this.dtpDatumDVO.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatumDVO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatumDVO_KeyDown);
            this.dtpDatumDVO.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(7, 223);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Komercijalista:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(106, 91);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(208, 23);
            this.dtpDatum.TabIndex = 8;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(7, 197);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "Izjava:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(341, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Napomena:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(342, 115);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 13);
            this.label20.TabIndex = 36;
            this.label20.Text = "Tečaj:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(7, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Dana valute:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Location = new System.Drawing.Point(667, 140);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 13);
            this.label26.TabIndex = 44;
            this.label26.Text = "Iznos predujma:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(342, 89);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 42;
            this.label19.Text = "Valuta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(7, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Dana:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Location = new System.Drawing.Point(667, 113);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 13);
            this.label25.TabIndex = 41;
            this.label25.Text = "Predujam:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Datum DVO:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Location = new System.Drawing.Point(667, 86);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(50, 13);
            this.label24.TabIndex = 27;
            this.label24.Text = "Otprema:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(7, 67);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "Vrsta dok.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(7, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Datum:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Location = new System.Drawing.Point(666, 59);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 13);
            this.label23.TabIndex = 22;
            this.label23.Text = "Nar.Kupca";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(342, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Žiro račun";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Location = new System.Drawing.Point(666, 32);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 33;
            this.label22.Text = "Nar.Kupca";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(7, 15);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(52, 13);
            this.label28.TabIndex = 31;
            this.label28.Text = "Odredište";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Fakturirati:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(665, 5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 13);
            this.label21.TabIndex = 29;
            this.label21.Text = "Radni nalog:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(342, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Model:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(432, 3);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(208, 23);
            this.txtIzradio.TabIndex = 11;
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIzradio_KeyDown);
            // 
            // txtTecaj
            // 
            this.txtTecaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTecaj.Location = new System.Drawing.Point(432, 111);
            this.txtTecaj.Name = "txtTecaj";
            this.txtTecaj.ReadOnly = true;
            this.txtTecaj.Size = new System.Drawing.Size(208, 23);
            this.txtTecaj.TabIndex = 555;
            this.txtTecaj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTecaj_KeyDown);
            // 
            // txtDana
            // 
            this.txtDana.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDana.Location = new System.Drawing.Point(106, 143);
            this.txtDana.Name = "txtDana";
            this.txtDana.Size = new System.Drawing.Size(208, 23);
            this.txtDana.TabIndex = 10;
            this.txtDana.Text = "0";
            this.txtDana.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtDana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDana_KeyDown);
            this.txtDana.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtDana.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtIznosPredujma
            // 
            this.txtIznosPredujma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIznosPredujma.Location = new System.Drawing.Point(803, 137);
            this.txtIznosPredujma.Name = "txtIznosPredujma";
            this.txtIznosPredujma.ReadOnly = true;
            this.txtIznosPredujma.Size = new System.Drawing.Size(158, 23);
            this.txtIznosPredujma.TabIndex = 13;
            this.txtIznosPredujma.Text = "0,00";
            // 
            // txtNarKupca1
            // 
            this.txtNarKupca1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNarKupca1.Location = new System.Drawing.Point(754, 58);
            this.txtNarKupca1.Name = "txtNarKupca1";
            this.txtNarKupca1.Size = new System.Drawing.Size(207, 23);
            this.txtNarKupca1.TabIndex = 23;
            this.txtNarKupca1.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtNarKupca1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarKupca1_KeyDown);
            this.txtNarKupca1.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraNarKupca
            // 
            this.txtSifraNarKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSifraNarKupca.Location = new System.Drawing.Point(754, 31);
            this.txtSifraNarKupca.Name = "txtSifraNarKupca";
            this.txtSifraNarKupca.Size = new System.Drawing.Size(53, 24);
            this.txtSifraNarKupca.TabIndex = 20;
            this.txtSifraNarKupca.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraNarKupca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraNarKupca_KeyDown);
            this.txtSifraNarKupca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraNarKupca.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(187, 11);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(127, 23);
            this.txtPartnerNaziv.TabIndex = 18;
            // 
            // txtPartnerNaziv1
            // 
            this.txtPartnerNaziv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv1.Location = new System.Drawing.Point(187, 37);
            this.txtPartnerNaziv1.Name = "txtPartnerNaziv1";
            this.txtPartnerNaziv1.ReadOnly = true;
            this.txtPartnerNaziv1.Size = new System.Drawing.Size(127, 23);
            this.txtPartnerNaziv1.TabIndex = 19;
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(106, 11);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 23);
            this.txtSifraOdrediste.TabIndex = 5;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraFakturirati
            // 
            this.txtSifraFakturirati.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraFakturirati.Location = new System.Drawing.Point(106, 36);
            this.txtSifraFakturirati.Name = "txtSifraFakturirati";
            this.txtSifraFakturirati.Size = new System.Drawing.Size(53, 23);
            this.txtSifraFakturirati.TabIndex = 6;
            this.txtSifraFakturirati.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraFakturirati.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraFakturirati_KeyDown);
            this.txtSifraFakturirati.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraFakturirati.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraRadniNalog
            // 
            this.txtSifraRadniNalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSifraRadniNalog.Location = new System.Drawing.Point(754, 4);
            this.txtSifraRadniNalog.Name = "txtSifraRadniNalog";
            this.txtSifraRadniNalog.Size = new System.Drawing.Size(53, 24);
            this.txtSifraRadniNalog.TabIndex = 19;
            this.txtSifraRadniNalog.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraRadniNalog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraRadniNalog_KeyDown);
            this.txtSifraRadniNalog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraRadniNalog.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.White;
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModel.Location = new System.Drawing.Point(432, 29);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(208, 23);
            this.txtModel.TabIndex = 14;
            this.txtModel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
            this.txtModel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(687, 552);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(294, 23);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "Ukupno sa PDV-om:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(12, 552);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(189, 23);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "Bez PDV-a:";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(207, 552);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(155, 23);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "PDV:";
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.BackColor = System.Drawing.Color.White;
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra_robe.Location = new System.Drawing.Point(137, 6);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(150, 23);
            this.txtSifra_robe.TabIndex = 26;
            this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraArtikla_KeyDown);
            this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNaDan.Location = new System.Drawing.Point(12, 555);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 17);
            this.lblNaDan.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(11, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 17);
            this.label10.TabIndex = 37;
            this.label10.Text = "Šifra robe/usluge:";
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRoba.Image")));
            this.btnOpenRoba.Location = new System.Drawing.Point(293, 2);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 38;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(851, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 18;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.BackColor = System.Drawing.Color.White;
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDeleteAllFaktura.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAllFaktura.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAllFaktura.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDeleteAllFaktura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllFaktura.TabIndex = 17;
            this.btnDeleteAllFaktura.Text = "Obriši fakturu";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = false;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.BackColor = System.Drawing.Color.White;
            this.btnSveFakture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSveFakture.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSveFakture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 16;
            this.btnSveFakture.Text = "Sve fakture  ";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = false;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 15;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(148, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 14;
            this.btnOdustani.Text = "Odustani   ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 13;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click_1);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.BackColor = System.Drawing.Color.White;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(836, 3);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(127, 29);
            this.btnObrisi.TabIndex = 8;
            this.btnObrisi.Text = "     Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox4.Location = new System.Drawing.Point(526, 552);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(155, 23);
            this.textBox4.TabIndex = 39;
            this.textBox4.Text = "Rabat:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label27.ForeColor = System.Drawing.Color.Maroon;
            this.label27.Location = new System.Drawing.Point(10, 9);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(166, 17);
            this.label27.TabIndex = 72;
            this.label27.Text = "Otpremnice u fakturu:";
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox5.Location = new System.Drawing.Point(368, 552);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(152, 23);
            this.textBox5.TabIndex = 41;
            this.textBox5.Text = "PNP:";
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            this.bgSinkronizacija.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSinkronizacija_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chbOdizmiIzSkladista);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.nmGodinaFakture);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.ttxBrojFakture);
            this.panel1.Controls.Add(this.nuGodinaPonude);
            this.panel1.Controls.Add(this.txtBrojPonude);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 36);
            this.panel1.TabIndex = 558;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnOdaberiOtpremnice);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Location = new System.Drawing.Point(12, 361);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 38);
            this.panel2.TabIndex = 559;
            // 
            // btnOdaberiOtpremnice
            // 
            this.btnOdaberiOtpremnice.BackColor = System.Drawing.Color.White;
            this.btnOdaberiOtpremnice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdaberiOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOdaberiOtpremnice.Location = new System.Drawing.Point(179, 4);
            this.btnOdaberiOtpremnice.Name = "btnOdaberiOtpremnice";
            this.btnOdaberiOtpremnice.Size = new System.Drawing.Size(178, 28);
            this.btnOdaberiOtpremnice.TabIndex = 562;
            this.btnOdaberiOtpremnice.Text = "Dodaj otpremnice";
            this.btnOdaberiOtpremnice.UseVisualStyleBackColor = false;
            this.btnOdaberiOtpremnice.Click += new System.EventHandler(this.btnOdaberiOtpremnice_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnPartner1);
            this.panel3.Controls.Add(this.txtSifraOdrediste);
            this.panel3.Controls.Add(this.btnPartner);
            this.panel3.Controls.Add(this.txtModel);
            this.panel3.Controls.Add(this.rtbNapomena);
            this.panel3.Controls.Add(this.txtSifraRadniNalog);
            this.panel3.Controls.Add(this.cbNacinPlacanja);
            this.panel3.Controls.Add(this.txtSifraFakturirati);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.txtPartnerNaziv1);
            this.panel3.Controls.Add(this.txtSifraNacinPlacanja);
            this.panel3.Controls.Add(this.txtPartnerNaziv);
            this.panel3.Controls.Add(this.cbOtprema);
            this.panel3.Controls.Add(this.txtSifraNarKupca);
            this.panel3.Controls.Add(this.nuGodinaPredujma);
            this.panel3.Controls.Add(this.txtNarKupca1);
            this.panel3.Controls.Add(this.btnPredujam);
            this.panel3.Controls.Add(this.txtIznosPredujma);
            this.panel3.Controls.Add(this.btnNarKupca);
            this.panel3.Controls.Add(this.txtDana);
            this.panel3.Controls.Add(this.btnRadniNalog);
            this.panel3.Controls.Add(this.txtTecaj);
            this.panel3.Controls.Add(this.cbVD);
            this.panel3.Controls.Add(this.txtIzradio);
            this.panel3.Controls.Add(this.cbZiroRacun);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.cbNarKupca);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.cbRadniBalog);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cbPredujam);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.cbValuta);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.cbKomercijalist);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cbIzjava);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.dtpDanaValuta);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.dtpDatumDVO);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dtpDatum);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(12, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(969, 255);
            this.panel3.TabIndex = 560;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnObrisi);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.txtSifra_robe);
            this.panel4.Controls.Add(this.btnOpenRoba);
            this.panel4.Location = new System.Drawing.Point(12, 405);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(969, 36);
            this.panel4.TabIndex = 561;
            // 
            // frmFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(993, 581);
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFaktura";
            this.Text = "Faktura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFaktura_FormClosing);
            this.Load += new System.EventHandler(this.frmFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodinaPonude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodinaPredujma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmGodinaFakture;
        private System.Windows.Forms.ComboBox cbZiroRacun;
        private System.Windows.Forms.ComboBox cbPredujam;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.ComboBox cbKomercijalist;
        private System.Windows.Forms.ComboBox cbIzjava;
        private System.Windows.Forms.DateTimePicker dtpDanaValuta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDatumDVO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTecaj;
        private System.Windows.Forms.TextBox txtDana;
        private System.Windows.Forms.TextBox txtNarKupca1;
        private System.Windows.Forms.TextBox txtSifraNarKupca;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtPartnerNaziv1;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.TextBox txtSifraFakturirati;
        private System.Windows.Forms.TextBox txtSifraRadniNalog;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.NumericUpDown nuGodinaPredujma;
        private System.Windows.Forms.Button btnPredujam;
        private System.Windows.Forms.Button btnNarKupca;
        private System.Windows.Forms.Button btnRadniNalog;
        private System.Windows.Forms.ComboBox cbNarKupca;
        private System.Windows.Forms.ComboBox cbRadniBalog;
        private System.Windows.Forms.TextBox txtIznosPredujma;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.ComboBox cbVD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBrojPonude;
        private System.Windows.Forms.NumericUpDown nuGodinaPonude;
        public System.Windows.Forms.TextBox ttxBrojFakture;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.ComboBox cbOtprema;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbNacinPlacanja;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSifraNacinPlacanja;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private frmFaktura.MyDataGrid sa;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.CheckBox chbOdizmiIzSkladista;
        private frmFaktura.MyDataGrid dgw;
        private System.Windows.Forms.PictureBox btnPartner1;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewComboBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn nc;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_roba_prodaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn oduzmi;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez_potrosnja;
        private System.Windows.Forms.DataGridViewTextBoxColumn povratna_naknada;
        private System.Windows.Forms.TextBox textBox5;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnOdaberiOtpremnice;
    }
}