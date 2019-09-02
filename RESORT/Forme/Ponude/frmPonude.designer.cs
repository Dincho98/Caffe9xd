namespace RESORT.Ponude.Forme
{
    partial class frmPonude
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPonude));
            this.sa = new RESORT.Ponude.Forme.frmPonude.MyDataGrid();
            this.dgw = new RESORT.Ponude.Forme.frmPonude.MyDataGrid();
            this.ttxBrojFakture = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmGodinaFakture = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblUkupno = new System.Windows.Forms.Label();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.cbNacinPlacanja = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSifraNacinPlacanja = new System.Windows.Forms.TextBox();
            this.cbZiroRacun = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.dtpDanaValuta = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDatumDVO = new System.Windows.Forms.DateTimePicker();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtTecaj = new System.Windows.Forms.TextBox();
            this.txtDana = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opis_usluge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_dolaska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_odlaska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boravisna_pristojba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_nocenja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_usluge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.sa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
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
            this.opis_usluge,
            this.datum_dolaska,
            this.datum_odlaska,
            this.avans,
            this.boravisna_pristojba,
            this.broj_nocenja,
            this.rabat,
            this.iznos_usluge,
            this.cijena_sobe,
            this.tb,
            this.iznos_ukupno,
            this.id_stavka});
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
            this.dgw.Location = new System.Drawing.Point(11, 325);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersVisible = false;
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(983, 244);
            this.dgw.TabIndex = 9;
            this.dgw.TabStop = false;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellContentClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // ttxBrojFakture
            // 
            this.ttxBrojFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ttxBrojFakture.Location = new System.Drawing.Point(108, 16);
            this.ttxBrojFakture.Name = "ttxBrojFakture";
            this.ttxBrojFakture.Size = new System.Drawing.Size(86, 23);
            this.ttxBrojFakture.TabIndex = 0;
            this.ttxBrojFakture.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.ttxBrojFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.ttxBrojFakture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.ttxBrojFakture.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj fakture:";
            // 
            // nmGodinaFakture
            // 
            this.nmGodinaFakture.BackColor = System.Drawing.Color.White;
            this.nmGodinaFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nmGodinaFakture.Location = new System.Drawing.Point(199, 16);
            this.nmGodinaFakture.Name = "nmGodinaFakture";
            this.nmGodinaFakture.Size = new System.Drawing.Size(87, 23);
            this.nmGodinaFakture.TabIndex = 1;
            this.nmGodinaFakture.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.nmGodinaFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.nmGodinaFakture.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ttxBrojFakture);
            this.groupBox1.Controls.Add(this.nmGodinaFakture);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(11, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 45);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lblUkupno);
            this.groupBox2.Controls.Add(this.btnPartner);
            this.groupBox2.Controls.Add(this.rtbNapomena);
            this.groupBox2.Controls.Add(this.cbNacinPlacanja);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtSifraNacinPlacanja);
            this.groupBox2.Controls.Add(this.cbZiroRacun);
            this.groupBox2.Controls.Add(this.cbValuta);
            this.groupBox2.Controls.Add(this.dtpDanaValuta);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dtpDatumDVO);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtIzradio);
            this.groupBox2.Controls.Add(this.txtTecaj);
            this.groupBox2.Controls.Add(this.txtDana);
            this.groupBox2.Controls.Add(this.txtPartnerNaziv);
            this.groupBox2.Controls.Add(this.txtSifraOdrediste);
            this.groupBox2.Controls.Add(this.txtModel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(11, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(980, 173);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // lblUkupno
            // 
            this.lblUkupno.AutoSize = true;
            this.lblUkupno.BackColor = System.Drawing.Color.Transparent;
            this.lblUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblUkupno.Location = new System.Drawing.Point(741, 138);
            this.lblUkupno.Name = "lblUkupno";
            this.lblUkupno.Size = new System.Drawing.Size(76, 20);
            this.lblUkupno.TabIndex = 558;
            this.lblUkupno.Text = "Ukupno:";
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(161, 29);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(30, 24);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 557;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Location = new System.Drawing.Point(745, 51);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(208, 76);
            this.rtbNapomena.TabIndex = 11;
            this.rtbNapomena.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
            this.rtbNapomena.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // cbNacinPlacanja
            // 
            this.cbNacinPlacanja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNacinPlacanja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNacinPlacanja.BackColor = System.Drawing.Color.White;
            this.cbNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNacinPlacanja.FormattingEnabled = true;
            this.cbNacinPlacanja.Location = new System.Drawing.Point(479, 106);
            this.cbNacinPlacanja.Name = "cbNacinPlacanja";
            this.cbNacinPlacanja.Size = new System.Drawing.Size(164, 24);
            this.cbNacinPlacanja.TabIndex = 8;
            this.cbNacinPlacanja.SelectedIndexChanged += new System.EventHandler(this.cbNacinPlacanja_SelectedIndexChanged);
            this.cbNacinPlacanja.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.cbNacinPlacanja.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(345, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 17);
            this.label15.TabIndex = 22;
            this.label15.Text = "Način plać.:";
            // 
            // txtSifraNacinPlacanja
            // 
            this.txtSifraNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSifraNacinPlacanja.Location = new System.Drawing.Point(435, 106);
            this.txtSifraNacinPlacanja.Name = "txtSifraNacinPlacanja";
            this.txtSifraNacinPlacanja.Size = new System.Drawing.Size(44, 24);
            this.txtSifraNacinPlacanja.TabIndex = 7;
            this.txtSifraNacinPlacanja.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtSifraNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.txtSifraNacinPlacanja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraNacinPlacanja.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // cbZiroRacun
            // 
            this.cbZiroRacun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZiroRacun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZiroRacun.BackColor = System.Drawing.Color.White;
            this.cbZiroRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZiroRacun.FormattingEnabled = true;
            this.cbZiroRacun.Location = new System.Drawing.Point(435, 29);
            this.cbZiroRacun.Name = "cbZiroRacun";
            this.cbZiroRacun.Size = new System.Drawing.Size(208, 24);
            this.cbZiroRacun.TabIndex = 5;
            this.cbZiroRacun.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbZiroRacun.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.cbZiroRacun.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.BackColor = System.Drawing.Color.White;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(435, 55);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(208, 24);
            this.cbValuta.TabIndex = 6;
            this.cbValuta.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.cbValuta.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // dtpDanaValuta
            // 
            this.dtpDanaValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDanaValuta.Location = new System.Drawing.Point(108, 130);
            this.dtpDanaValuta.Name = "dtpDanaValuta";
            this.dtpDanaValuta.Size = new System.Drawing.Size(208, 23);
            this.dtpDanaValuta.TabIndex = 4;
            this.dtpDanaValuta.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.dtpDanaValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.dtpDanaValuta.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(345, 130);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 23;
            this.label13.Text = "Izradio:";
            // 
            // dtpDatumDVO
            // 
            this.dtpDatumDVO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDVO.Location = new System.Drawing.Point(108, 80);
            this.dtpDatumDVO.Name = "dtpDatumDVO";
            this.dtpDatumDVO.Size = new System.Drawing.Size(208, 23);
            this.dtpDatumDVO.TabIndex = 2;
            this.dtpDatumDVO.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.dtpDatumDVO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.dtpDatumDVO.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(108, 55);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(208, 23);
            this.dtpDatum.TabIndex = 1;
            this.dtpDatum.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.dtpDatum.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(663, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "Napomena:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(345, 83);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 17);
            this.label20.TabIndex = 20;
            this.label20.Text = "Tečaj:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(9, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Dana valute:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(345, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 17);
            this.label19.TabIndex = 19;
            this.label19.Text = "Valuta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(9, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Dana:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(9, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Datum DVO:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Datum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(345, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Žiro račun";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(10, 34);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 17);
            this.label28.TabIndex = 13;
            this.label28.Text = "Odredište";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(663, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 17);
            this.label14.TabIndex = 25;
            this.label14.Text = "Model:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(435, 132);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(208, 23);
            this.txtIzradio.TabIndex = 24;
            // 
            // txtTecaj
            // 
            this.txtTecaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTecaj.Location = new System.Drawing.Point(435, 81);
            this.txtTecaj.Name = "txtTecaj";
            this.txtTecaj.ReadOnly = true;
            this.txtTecaj.Size = new System.Drawing.Size(208, 23);
            this.txtTecaj.TabIndex = 21;
            // 
            // txtDana
            // 
            this.txtDana.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDana.Location = new System.Drawing.Point(108, 105);
            this.txtDana.Name = "txtDana";
            this.txtDana.Size = new System.Drawing.Size(208, 23);
            this.txtDana.TabIndex = 3;
            this.txtDana.Text = "0";
            this.txtDana.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtDana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDana_KeyDown);
            this.txtDana.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtDana.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(189, 30);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(127, 23);
            this.txtPartnerNaziv.TabIndex = 12;
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(108, 30);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 23);
            this.txtSifraOdrediste.TabIndex = 0;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.White;
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModel.Location = new System.Drawing.Point(745, 26);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(208, 23);
            this.txtModel.TabIndex = 9;
            this.txtModel.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown_1);
            this.txtModel.Leave += new System.EventHandler(this.txtImePrezime_Leave);
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
            this.lblNaDan.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(864, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Izlaz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(564, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši ponudu";
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(426, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Sve ponude";
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 0;
            this.btnNoviUnos.Text = "Novi unos";
            this.btnNoviUnos.UseVisualStyleBackColor = true;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(150, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(288, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(202, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Obriši stavku (DELETE)";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(9, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nova stavka (INSER)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // br
            // 
            this.br.FillWeight = 30F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            this.br.Width = 30;
            // 
            // opis_usluge
            // 
            this.opis_usluge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.opis_usluge.FillWeight = 140F;
            this.opis_usluge.HeaderText = "Opis usluge";
            this.opis_usluge.MinimumWidth = 90;
            this.opis_usluge.Name = "opis_usluge";
            // 
            // datum_dolaska
            // 
            this.datum_dolaska.FillWeight = 110F;
            this.datum_dolaska.HeaderText = "Dat.dolaska";
            this.datum_dolaska.Name = "datum_dolaska";
            this.datum_dolaska.Width = 110;
            // 
            // datum_odlaska
            // 
            this.datum_odlaska.FillWeight = 110F;
            this.datum_odlaska.HeaderText = "Dat.odlaska";
            this.datum_odlaska.Name = "datum_odlaska";
            this.datum_odlaska.Width = 110;
            // 
            // avans
            // 
            this.avans.FillWeight = 60F;
            this.avans.HeaderText = "Avans";
            this.avans.Name = "avans";
            this.avans.Width = 60;
            // 
            // boravisna_pristojba
            // 
            this.boravisna_pristojba.HeaderText = "Boravišna pristojba";
            this.boravisna_pristojba.Name = "boravisna_pristojba";
            this.boravisna_pristojba.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.boravisna_pristojba.Width = 75;
            // 
            // broj_nocenja
            // 
            this.broj_nocenja.FillWeight = 61.10954F;
            this.broj_nocenja.HeaderText = "Br.noćenja";
            this.broj_nocenja.Name = "broj_nocenja";
            this.broj_nocenja.Width = 71;
            // 
            // rabat
            // 
            this.rabat.FillWeight = 61.10954F;
            this.rabat.HeaderText = "Rabat%";
            this.rabat.Name = "rabat";
            this.rabat.Width = 71;
            // 
            // iznos_usluge
            // 
            this.iznos_usluge.HeaderText = "Iznos usluge";
            this.iznos_usluge.Name = "iznos_usluge";
            this.iznos_usluge.Width = 74;
            // 
            // cijena_sobe
            // 
            this.cijena_sobe.HeaderText = "Cijena sobe";
            this.cijena_sobe.Name = "cijena_sobe";
            this.cijena_sobe.Width = 75;
            // 
            // tb
            // 
            this.tb.FillWeight = 30F;
            this.tb.HeaderText = "TB";
            this.tb.Name = "tb";
            this.tb.Width = 30;
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            this.iznos_ukupno.ReadOnly = true;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // frmPonude
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1004, 581);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgw);
            this.Name = "frmPonude";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ponude";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmGodinaFakture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbZiroRacun;
        private System.Windows.Forms.DateTimePicker dtpDanaValuta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDatumDVO;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDana;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ttxBrojFakture;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button button1;
        private frmPonude.MyDataGrid sa;
        private frmPonude.MyDataGrid dgw;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.ComboBox cbNacinPlacanja;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSifraNacinPlacanja;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTecaj;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUkupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn opis_usluge;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_dolaska;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_odlaska;
        private System.Windows.Forms.DataGridViewTextBoxColumn avans;
        private System.Windows.Forms.DataGridViewTextBoxColumn boravisna_pristojba;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_nocenja;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_usluge;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
    }
}