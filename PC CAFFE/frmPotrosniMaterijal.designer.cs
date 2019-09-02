namespace PCPOS
{
    partial class frmPotrosniMaterijal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPotrosniMaterijal));
            this.sa = new PCPOS.frmPotrosniMaterijal.MyDataGrid();
            this.dgw = new PCPOS.frmPotrosniMaterijal.MyDataGrid();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmGodina = new System.Windows.Forms.NumericUpDown();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.cbNacinPlacanja = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.cbArtikli = new System.Windows.Forms.ComboBox();
            this.btnDodajNaPopis = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.sa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.naziv,
            this.jmj,
            this.kolicina,
            this.porez,
            this.vpc,
            this.mpc,
            this.rabat,
            this.rabat_iznos,
            this.iznos_ukupno,
            this.id_stavka,
            this.sifra});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 294);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(969, 252);
            this.dgw.TabIndex = 3;
            this.dgw.TabStop = false;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // br
            // 
            this.br.FillWeight = 40F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            this.br.ReadOnly = true;
            this.br.Width = 40;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 61.10954F;
            this.naziv.HeaderText = "Naziv robe ili usluge";
            this.naziv.MinimumWidth = 130;
            this.naziv.Name = "naziv";
            // 
            // jmj
            // 
            this.jmj.FillWeight = 50F;
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.Width = 72;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 50F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            this.kolicina.Width = 72;
            // 
            // porez
            // 
            this.porez.FillWeight = 50F;
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            this.porez.Width = 72;
            // 
            // vpc
            // 
            this.vpc.FillWeight = 70F;
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            this.vpc.Width = 70;
            // 
            // mpc
            // 
            this.mpc.FillWeight = 70F;
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            this.mpc.Width = 70;
            // 
            // rabat
            // 
            this.rabat.FillWeight = 60F;
            this.rabat.HeaderText = "Rabat%";
            this.rabat.Name = "rabat";
            this.rabat.Width = 60;
            // 
            // rabat_iznos
            // 
            this.rabat_iznos.HeaderText = "Rabat iznos";
            this.rabat_iznos.Name = "rabat_iznos";
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            this.iznos_ukupno.ReadOnly = true;
            this.iznos_ukupno.Width = 110;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "sifra";
            this.sifra.Name = "sifra";
            this.sifra.Visible = false;
            // 
            // txtBroj
            // 
            this.txtBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBroj.Location = new System.Drawing.Point(104, 5);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(86, 23);
            this.txtBroj.TabIndex = 0;
            this.txtBroj.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.txtBroj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtBroj.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj fakture:";
            // 
            // nmGodina
            // 
            this.nmGodina.BackColor = System.Drawing.Color.White;
            this.nmGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nmGodina.Location = new System.Drawing.Point(195, 5);
            this.nmGodina.Name = "nmGodina";
            this.nmGodina.Size = new System.Drawing.Size(87, 23);
            this.nmGodina.TabIndex = 1;
            this.nmGodina.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.nmGodina.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(159, 9);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 557;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // cbNacinPlacanja
            // 
            this.cbNacinPlacanja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNacinPlacanja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNacinPlacanja.BackColor = System.Drawing.Color.White;
            this.cbNacinPlacanja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNacinPlacanja.FormattingEnabled = true;
            this.cbNacinPlacanja.Location = new System.Drawing.Point(107, 62);
            this.cbNacinPlacanja.Name = "cbNacinPlacanja";
            this.cbNacinPlacanja.Size = new System.Drawing.Size(277, 24);
            this.cbNacinPlacanja.TabIndex = 10;
            this.cbNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.cbNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(9, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Način plać.:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(9, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Izradio:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(107, 36);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(277, 23);
            this.dtpDatum.TabIndex = 2;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(396, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Napomena:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Datum:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(9, 13);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "Partner";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(107, 92);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(277, 23);
            this.txtIzradio.TabIndex = 13;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(188, 9);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(196, 23);
            this.txtPartnerNaziv.TabIndex = 1;
            this.txtPartnerNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPartnerNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtPartnerNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(107, 9);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 23);
            this.txtSifraOdrediste.TabIndex = 0;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.BackColor = System.Drawing.Color.White;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(483, 10);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(460, 105);
            this.rtbNapomena.TabIndex = 7;
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(687, 552);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(294, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Ukupno sa PDV-om:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(492, 552);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(189, 23);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "Bez PDV-a:";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(331, 552);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(155, 23);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "PDV:";
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
            this.lblNaDan.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
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
            this.button1.TabIndex = 13;
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
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(171, 40);
            this.btnDeleteAllFaktura.TabIndex = 12;
            this.btnDeleteAllFaktura.Text = "Obriši dokumenat";
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
            this.btnSveFakture.TabIndex = 11;
            this.btnSveFakture.Text = "Svi dokumenti";
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
            this.btnNoviUnos.TabIndex = 8;
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
            this.btnOdustani.TabIndex = 9;
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
            this.btnSpremi.TabIndex = 10;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(13, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nova stavka (INSER)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(209, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Obriši stavku (DELETE)";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            this.bgSinkronizacija.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSinkronizacija_RunWorkerCompleted);
            // 
            // cbArtikli
            // 
            this.cbArtikli.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbArtikli.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbArtikli.BackColor = System.Drawing.Color.White;
            this.cbArtikli.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArtikli.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbArtikli.FormattingEnabled = true;
            this.cbArtikli.Location = new System.Drawing.Point(13, 7);
            this.cbArtikli.Name = "cbArtikli";
            this.cbArtikli.Size = new System.Drawing.Size(277, 24);
            this.cbArtikli.TabIndex = 27;
            // 
            // btnDodajNaPopis
            // 
            this.btnDodajNaPopis.BackColor = System.Drawing.Color.White;
            this.btnDodajNaPopis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajNaPopis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNaPopis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNaPopis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajNaPopis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajNaPopis.Location = new System.Drawing.Point(294, 6);
            this.btnDodajNaPopis.Name = "btnDodajNaPopis";
            this.btnDodajNaPopis.Size = new System.Drawing.Size(95, 27);
            this.btnDodajNaPopis.TabIndex = 28;
            this.btnDodajNaPopis.Text = "Dodaj na popis";
            this.btnDodajNaPopis.UseVisualStyleBackColor = false;
            this.btnDodajNaPopis.Click += new System.EventHandler(this.btnDodajNaPopis_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBroj);
            this.panel1.Controls.Add(this.nmGodina);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 34);
            this.panel1.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPartner);
            this.panel2.Controls.Add(this.txtSifraOdrediste);
            this.panel2.Controls.Add(this.cbNacinPlacanja);
            this.panel2.Controls.Add(this.rtbNapomena);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.txtPartnerNaziv);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtIzradio);
            this.panel2.Controls.Add(this.dtpDatum);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(12, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 126);
            this.panel2.TabIndex = 30;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbArtikli);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.btnDodajNaPopis);
            this.panel3.Location = new System.Drawing.Point(12, 230);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(969, 58);
            this.panel3.TabIndex = 31;
            // 
            // frmPotrosniMaterijal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(993, 581);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
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
            this.Name = "frmPotrosniMaterijal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Potrošni materijal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPotrosniMaterijal_FormClosing);
            this.Load += new System.EventHandler(this.frmFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnSveFakture;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbNacinPlacanja;
        private System.Windows.Forms.Label label15;
        private frmPotrosniMaterijal.MyDataGrid sa;
        private frmPotrosniMaterijal.MyDataGrid dgw;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.ComboBox cbArtikli;
        private System.Windows.Forms.Button btnDodajNaPopis;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}