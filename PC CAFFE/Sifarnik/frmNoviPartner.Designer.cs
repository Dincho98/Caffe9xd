namespace PCPOS
{
    partial class frmNoviPartner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoviPartner));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.cbPPT = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOib = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgwKontakti = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vrsta_kontakta = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.kontakt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.osoba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Obrisi = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.cbDjelatnost = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtZR = new System.Windows.Forms.TextBox();
            this.cbDrzava = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.txtSifra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgwKontakti)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(17, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Šifra partnera:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Enabled = false;
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(120, 123);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(238, 23);
            this.txtNaziv.TabIndex = 2;
            this.txtNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNaziv_KeyDown);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.BackColor = System.Drawing.Color.White;
            this.rtbNapomena.Enabled = false;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(495, 149);
            this.rtbNapomena.MaxLength = 150;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(238, 74);
            this.rtbNapomena.TabIndex = 10;
            this.rtbNapomena.Text = "";
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(17, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Naziv tvrtke:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(17, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Adresa:";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Enabled = false;
            this.txtAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAdresa.Location = new System.Drawing.Point(120, 200);
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(238, 23);
            this.txtAdresa.TabIndex = 6;
            this.txtAdresa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdresa_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(17, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Država:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(17, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Grad:";
            // 
            // cbGrad
            // 
            this.cbGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Enabled = false;
            this.cbGrad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(120, 174);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(135, 24);
            this.cbGrad.TabIndex = 4;
            this.cbGrad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbGrad_KeyDown);
            // 
            // cbPPT
            // 
            this.cbPPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPPT.Enabled = false;
            this.cbPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbPPT.FormattingEnabled = true;
            this.cbPPT.Location = new System.Drawing.Point(287, 173);
            this.cbPPT.Name = "cbPPT";
            this.cbPPT.Size = new System.Drawing.Size(71, 24);
            this.cbPPT.TabIndex = 5;
            this.cbPPT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPPT_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(258, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "PPT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(17, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "OIB:";
            // 
            // txtOib
            // 
            this.txtOib.Enabled = false;
            this.txtOib.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOib.Location = new System.Drawing.Point(120, 226);
            this.txtOib.Name = "txtOib";
            this.txtOib.Size = new System.Drawing.Size(238, 23);
            this.txtOib.TabIndex = 7;
            this.txtOib.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOib_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(408, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Napomena:";
            // 
            // dgwKontakti
            // 
            this.dgwKontakti.AllowUserToAddRows = false;
            this.dgwKontakti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwKontakti.BackgroundColor = System.Drawing.Color.White;
            this.dgwKontakti.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwKontakti.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgwKontakti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwKontakti.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.vrsta_kontakta,
            this.kontakt,
            this.osoba,
            this.Obrisi});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwKontakti.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgwKontakti.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgwKontakti.Location = new System.Drawing.Point(16, 324);
            this.dgwKontakti.Name = "dgwKontakti";
            this.dgwKontakti.RowTemplate.Height = 20;
            this.dgwKontakti.Size = new System.Drawing.Size(718, 255);
            this.dgwKontakti.TabIndex = 12;
            this.dgwKontakti.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwKontakti_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // vrsta_kontakta
            // 
            this.vrsta_kontakta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vrsta_kontakta.HeaderText = "Vrsta_kontakta";
            this.vrsta_kontakta.Items.AddRange(new object[] {
            "Telefon",
            "Fax",
            "Mobitel",
            "Email",
            "Web"});
            this.vrsta_kontakta.Name = "vrsta_kontakta";
            this.vrsta_kontakta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.vrsta_kontakta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // kontakt
            // 
            this.kontakt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kontakt.HeaderText = "Kontakt";
            this.kontakt.Name = "kontakt";
            // 
            // osoba
            // 
            this.osoba.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.osoba.HeaderText = "Odgovorna osoba";
            this.osoba.Name = "osoba";
            // 
            // Obrisi
            // 
            this.Obrisi.HeaderText = "Obriši";
            this.Obrisi.Name = "Obrisi";
            this.Obrisi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Obrisi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Obrisi.Width = 70;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(408, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Djelatnost:";
            // 
            // cbDjelatnost
            // 
            this.cbDjelatnost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDjelatnost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDjelatnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDjelatnost.Enabled = false;
            this.cbDjelatnost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDjelatnost.FormattingEnabled = true;
            this.cbDjelatnost.Location = new System.Drawing.Point(496, 97);
            this.cbDjelatnost.Name = "cbDjelatnost";
            this.cbDjelatnost.Size = new System.Drawing.Size(238, 24);
            this.cbDjelatnost.TabIndex = 8;
            this.cbDjelatnost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbDjelatnost_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(407, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Žiro račun:";
            // 
            // txtZR
            // 
            this.txtZR.Enabled = false;
            this.txtZR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtZR.Location = new System.Drawing.Point(496, 123);
            this.txtZR.Name = "txtZR";
            this.txtZR.Size = new System.Drawing.Size(238, 23);
            this.txtZR.TabIndex = 9;
            this.txtZR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZR_KeyDown);
            // 
            // cbDrzava
            // 
            this.cbDrzava.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDrzava.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDrzava.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrzava.Enabled = false;
            this.cbDrzava.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDrzava.FormattingEnabled = true;
            this.cbDrzava.Location = new System.Drawing.Point(120, 148);
            this.cbDrzava.Name = "cbDrzava";
            this.cbDrzava.Size = new System.Drawing.Size(238, 24);
            this.cbDrzava.TabIndex = 3;
            this.cbDrzava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbDrzava_KeyDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(15, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 32);
            this.button1.TabIndex = 11;
            this.button1.Text = "Dodaj kontakt";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(608, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 40);
            this.button2.TabIndex = 17;
            this.button2.Text = "Izlaz      ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.btnNoviUnos.Location = new System.Drawing.Point(11, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 13;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNew_Click);
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
            this.btnOdustani.Location = new System.Drawing.Point(149, 12);
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
            this.btnSpremi.Location = new System.Drawing.Point(287, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 15;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
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
            this.btnSveFakture.Location = new System.Drawing.Point(426, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 16;
            this.btnSveFakture.Text = "Traži        ";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = false;
            this.btnSveFakture.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.Enabled = false;
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(120, 97);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(238, 23);
            this.txtSifra.TabIndex = 1;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown_1);
            // 
            // frmNoviPartner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 597);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgwKontakti);
            this.Controls.Add(this.cbDrzava);
            this.Controls.Add(this.cbDjelatnost);
            this.Controls.Add(this.cbPPT);
            this.Controls.Add(this.cbGrad);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rtbNapomena);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtZR);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOib);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAdresa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmNoviPartner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partneri";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNoviPartner_FormClosing);
            this.Load += new System.EventHandler(this.frmNoviPartner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwKontakti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.ComboBox cbPPT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOib;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgwKontakti;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbDjelatnost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtZR;
        private System.Windows.Forms.ComboBox cbDrzava;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewComboBoxColumn vrsta_kontakta;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontakt;
        private System.Windows.Forms.DataGridViewTextBoxColumn osoba;
        private System.Windows.Forms.DataGridViewLinkColumn Obrisi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.TextBox txtSifra;
    }
}