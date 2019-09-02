namespace PCPOS
{
    partial class frmOtpremnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOtpremnica));
            this.lblNaDan = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbPos_partner = new System.Windows.Forms.GroupBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.cbKomercijalist = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.btnPartner = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtNazivPosPartner = new System.Windows.Forms.TextBox();
            this.txtSifraPosPatner = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbPoslovniPartner = new System.Windows.Forms.RadioButton();
            this.rbOsoba = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojOtpremnice = new System.Windows.Forms.TextBox();
            this.nmGodinaOtpremnice = new System.Windows.Forms.NumericUpDown();
            this.dgw = new PCPOS.frmOtpremnica.MyDataGrid();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label18 = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.rbPos_partner.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaOtpremnice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaDan.Location = new System.Drawing.Point(12, 556);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 13);
            this.lblNaDan.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(307, 551);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(155, 23);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "PDV:";
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSifra_robe.Location = new System.Drawing.Point(12, 324);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(193, 26);
            this.txtSifra_robe.TabIndex = 8;
            this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(468, 551);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(189, 23);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "Bez PDV-a:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(663, 551);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(347, 23);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "Ukupno sa PDV-om:";
            // 
            // rbPos_partner
            // 
            this.rbPos_partner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPos_partner.BackColor = System.Drawing.Color.Transparent;
            this.rbPos_partner.Controls.Add(this.rtbNapomena);
            this.rbPos_partner.Controls.Add(this.cbKomercijalist);
            this.rbPos_partner.Controls.Add(this.label12);
            this.rbPos_partner.Controls.Add(this.button4);
            this.rbPos_partner.Controls.Add(this.btnPartner);
            this.rbPos_partner.Controls.Add(this.label13);
            this.rbPos_partner.Controls.Add(this.dtpDatum);
            this.rbPos_partner.Controls.Add(this.label8);
            this.rbPos_partner.Controls.Add(this.label4);
            this.rbPos_partner.Controls.Add(this.label15);
            this.rbPos_partner.Controls.Add(this.label28);
            this.rbPos_partner.Controls.Add(this.txtIzradio);
            this.rbPos_partner.Controls.Add(this.txtNazivPosPartner);
            this.rbPos_partner.Controls.Add(this.txtSifraPosPatner);
            this.rbPos_partner.Controls.Add(this.txtPartnerNaziv);
            this.rbPos_partner.Controls.Add(this.txtSifraOdrediste);
            this.rbPos_partner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbPos_partner.Location = new System.Drawing.Point(12, 118);
            this.rbPos_partner.Name = "rbPos_partner";
            this.rbPos_partner.Size = new System.Drawing.Size(998, 183);
            this.rbPos_partner.TabIndex = 7;
            this.rbPos_partner.TabStop = false;
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.BackColor = System.Drawing.SystemColors.MenuBar;
            this.rtbNapomena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.rtbNapomena.Location = new System.Drawing.Point(552, 26);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(440, 147);
            this.rtbNapomena.TabIndex = 9;
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown_1);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbKomercijalist
            // 
            this.cbKomercijalist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbKomercijalist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbKomercijalist.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cbKomercijalist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbKomercijalist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbKomercijalist.FormattingEnabled = true;
            this.cbKomercijalist.Location = new System.Drawing.Point(136, 118);
            this.cbKomercijalist.Name = "cbKomercijalist";
            this.cbKomercijalist.Size = new System.Drawing.Size(265, 24);
            this.cbKomercijalist.TabIndex = 7;
            this.cbKomercijalist.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbKomercijalist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbKomercijalist_KeyDown);
            this.cbKomercijalist.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.Location = new System.Drawing.Point(31, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Komercijalista:";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.Location = new System.Drawing.Point(195, 57);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 25);
            this.button4.TabIndex = 4;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.SystemColors.Control;
            this.btnPartner.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.btnPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPartner.Location = new System.Drawing.Point(195, 26);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(30, 25);
            this.btnPartner.TabIndex = 1;
            this.btnPartner.Text = "...";
            this.btnPartner.UseVisualStyleBackColor = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label13.Location = new System.Drawing.Point(76, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 14;
            this.label13.Text = "Izradio:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.dtpDatum.CalendarMonthBackground = System.Drawing.SystemColors.MenuBar;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(136, 88);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(265, 23);
            this.dtpDatum.TabIndex = 6;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(465, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Napomena:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(77, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Datum:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label15.Location = new System.Drawing.Point(40, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 17);
            this.label15.TabIndex = 11;
            this.label15.Text = "Poslovni par.";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label28.Location = new System.Drawing.Point(56, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(74, 17);
            this.label28.TabIndex = 10;
            this.label28.Text = "Odredište:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtIzradio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtIzradio.Location = new System.Drawing.Point(135, 148);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(267, 25);
            this.txtIzradio.TabIndex = 8;
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIzradio_KeyDown);
            // 
            // txtNazivPosPartner
            // 
            this.txtNazivPosPartner.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtNazivPosPartner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNazivPosPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtNazivPosPartner.Location = new System.Drawing.Point(229, 57);
            this.txtNazivPosPartner.Name = "txtNazivPosPartner";
            this.txtNazivPosPartner.ReadOnly = true;
            this.txtNazivPosPartner.Size = new System.Drawing.Size(173, 25);
            this.txtNazivPosPartner.TabIndex = 5;
            this.txtNazivPosPartner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox5_KeyDown);
            // 
            // txtSifraPosPatner
            // 
            this.txtSifraPosPatner.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSifraPosPatner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifraPosPatner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtSifraPosPatner.Location = new System.Drawing.Point(136, 57);
            this.txtSifraPosPatner.Name = "txtSifraPosPatner";
            this.txtSifraPosPatner.Size = new System.Drawing.Size(53, 25);
            this.txtSifraPosPatner.TabIndex = 3;
            this.txtSifraPosPatner.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraPosPatner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyDown);
            this.txtSifraPosPatner.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraPosPatner.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtPartnerNaziv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(229, 26);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(173, 25);
            this.txtPartnerNaziv.TabIndex = 2;
            this.txtPartnerNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerNaziv_KeyDown);
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSifraOdrediste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(136, 26);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 25);
            this.txtSifraOdrediste.TabIndex = 0;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbPoslovniPartner);
            this.groupBox1.Controls.Add(this.rbOsoba);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBrojOtpremnice);
            this.groupBox1.Controls.Add(this.nmGodinaOtpremnice);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(998, 45);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(408, 14);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(208, 24);
            this.cbSkladiste.TabIndex = 2;
            this.cbSkladiste.SelectedIndexChanged += new System.EventHandler(this.cbSkladiste_SelectedIndexChanged);
            this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSkladiste_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(323, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Skladište:";
            // 
            // rbPoslovniPartner
            // 
            this.rbPoslovniPartner.AutoSize = true;
            this.rbPoslovniPartner.Checked = true;
            this.rbPoslovniPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbPoslovniPartner.Location = new System.Drawing.Point(668, 16);
            this.rbPoslovniPartner.Name = "rbPoslovniPartner";
            this.rbPoslovniPartner.Size = new System.Drawing.Size(129, 21);
            this.rbPoslovniPartner.TabIndex = 3;
            this.rbPoslovniPartner.TabStop = true;
            this.rbPoslovniPartner.Text = "Poslovni partner";
            this.rbPoslovniPartner.UseVisualStyleBackColor = true;
            // 
            // rbOsoba
            // 
            this.rbOsoba.AutoSize = true;
            this.rbOsoba.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbOsoba.Location = new System.Drawing.Point(803, 16);
            this.rbOsoba.Name = "rbOsoba";
            this.rbOsoba.Size = new System.Drawing.Size(68, 21);
            this.rbOsoba.TabIndex = 4;
            this.rbOsoba.Text = "Osoba";
            this.rbOsoba.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Broj otpremnice:";
            // 
            // txtBrojOtpremnice
            // 
            this.txtBrojOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBrojOtpremnice.Location = new System.Drawing.Point(136, 15);
            this.txtBrojOtpremnice.Name = "txtBrojOtpremnice";
            this.txtBrojOtpremnice.Size = new System.Drawing.Size(89, 23);
            this.txtBrojOtpremnice.TabIndex = 0;
            this.txtBrojOtpremnice.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBrojOtpremnice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojOtpremnice_KeyDown_1);
            this.txtBrojOtpremnice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtBrojOtpremnice.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodinaOtpremnice
            // 
            this.nmGodinaOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nmGodinaOtpremnice.Location = new System.Drawing.Point(229, 15);
            this.nmGodinaOtpremnice.Name = "nmGodinaOtpremnice";
            this.nmGodinaOtpremnice.Size = new System.Drawing.Size(89, 23);
            this.nmGodinaOtpremnice.TabIndex = 1;
            this.nmGodinaOtpremnice.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodinaOtpremnice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojOtpremnice_KeyDown_1);
            this.nmGodinaOtpremnice.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
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
            this.porez_potrosnja});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 358);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(998, 187);
            this.dgw.TabIndex = 10;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            this.dgw.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellValidated);
            // 
            // br
            // 
            this.br.FillWeight = 50F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            this.br.ReadOnly = true;
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
            this.vpc.ReadOnly = true;
            this.vpc.Visible = false;
            // 
            // nc
            // 
            this.nc.HeaderText = "nc";
            this.nc.Name = "nc";
            this.nc.ReadOnly = true;
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
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new System.Drawing.Point(12, 304);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 17);
            this.label18.TabIndex = 14;
            this.label18.Text = "Šifra artikla/usluge:";
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(875, 322);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(135, 30);
            this.btnObrisi.TabIndex = 9;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = global::PCPOS.Properties.Resources._1059;
            this.btnOpenRoba.Location = new System.Drawing.Point(211, 324);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(26, 26);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 37;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(880, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(564, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(156, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši otpremnicu";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(138, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Sve otpremnice";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 0;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = true;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(148, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani   ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // frmOtpremnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1022, 586);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnOpenRoba);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtSifra_robe);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rbPos_partner);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.label18);
            this.Name = "frmOtpremnica";
            this.Text = "Otpremnica";
            this.Load += new System.EventHandler(this.frmOtpremnica_Load);
            this.rbPos_partner.ResumeLayout(false);
            this.rbPos_partner.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaOtpremnice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox rbPos_partner;
        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtBrojOtpremnice;
        private System.Windows.Forms.NumericUpDown nmGodinaOtpremnice;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbKomercijalist;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNazivPosPartner;
        private System.Windows.Forms.TextBox txtSifraPosPatner;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
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
        private frmOtpremnica.MyDataGrid dgw;
        private System.Windows.Forms.RadioButton rbPoslovniPartner;
        private System.Windows.Forms.RadioButton rbOsoba;
    }
}