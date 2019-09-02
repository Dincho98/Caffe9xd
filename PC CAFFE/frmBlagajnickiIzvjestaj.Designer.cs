namespace PCPOS {
    partial class frmBlagajnickiIzvjestaj {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.rb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dokument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uplaceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.izdatak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.lblIznos = new System.Windows.Forms.Label();
            this.odDatuma = new System.Windows.Forms.DateTimePicker();
            this.doDatuma = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUplaceno = new System.Windows.Forms.Label();
            this.lblIzdatak = new System.Windows.Forms.Label();
            this.dtDatumVrijeme = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOznaka = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbOdabir = new System.Windows.Forms.ComboBox();
            this.btnIspisOdabranog = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rb,
            this.datum,
            this.dokument,
            this.oznaka,
            this.Uplaceno,
            this.izdatak,
            this.id,
            this.id_partner});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.Location = new System.Drawing.Point(12, 172);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(935, 354);
            this.dgv.TabIndex = 14;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit_1);
            // 
            // rb
            // 
            this.rb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.rb.HeaderText = "RB";
            this.rb.Name = "rb";
            this.rb.ReadOnly = true;
            this.rb.Width = 60;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            this.datum.Width = 120;
            // 
            // dokument
            // 
            this.dokument.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dokument.HeaderText = "Dokument";
            this.dokument.Name = "dokument";
            this.dokument.ReadOnly = true;
            this.dokument.Width = 180;
            // 
            // oznaka
            // 
            this.oznaka.HeaderText = "Oznaka";
            this.oznaka.Name = "oznaka";
            this.oznaka.ReadOnly = true;
            // 
            // Uplaceno
            // 
            this.Uplaceno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Uplaceno.DefaultCellStyle = dataGridViewCellStyle2;
            this.Uplaceno.HeaderText = "Uplaceno";
            this.Uplaceno.Name = "Uplaceno";
            this.Uplaceno.ReadOnly = true;
            this.Uplaceno.Width = 120;
            // 
            // izdatak
            // 
            this.izdatak.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.izdatak.DefaultCellStyle = dataGridViewCellStyle3;
            this.izdatak.HeaderText = "Izdatak";
            this.izdatak.Name = "izdatak";
            this.izdatak.ReadOnly = true;
            this.izdatak.Width = 120;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // id_partner
            // 
            this.id_partner.HeaderText = "id_partner";
            this.id_partner.Name = "id_partner";
            this.id_partner.ReadOnly = true;
            this.id_partner.Visible = false;
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtIznos.Location = new System.Drawing.Point(13, 77);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(192, 29);
            this.txtIznos.TabIndex = 9;
            // 
            // lblIznos
            // 
            this.lblIznos.AutoSize = true;
            this.lblIznos.BackColor = System.Drawing.Color.Transparent;
            this.lblIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblIznos.Location = new System.Drawing.Point(10, 57);
            this.lblIznos.Name = "lblIznos";
            this.lblIznos.Size = new System.Drawing.Size(55, 17);
            this.lblIznos.TabIndex = 16;
            this.lblIznos.Text = "lblIznos";
            // 
            // odDatuma
            // 
            this.odDatuma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.odDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.odDatuma.Location = new System.Drawing.Point(43, 13);
            this.odDatuma.Name = "odDatuma";
            this.odDatuma.Size = new System.Drawing.Size(120, 20);
            this.odDatuma.TabIndex = 0;
            // 
            // doDatuma
            // 
            this.doDatuma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.doDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.doDatuma.Location = new System.Drawing.Point(43, 42);
            this.doDatuma.Name = "doDatuma";
            this.doDatuma.Size = new System.Drawing.Size(120, 20);
            this.doDatuma.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "OD";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(11, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "DO";
            // 
            // lblUplaceno
            // 
            this.lblUplaceno.AutoSize = true;
            this.lblUplaceno.BackColor = System.Drawing.Color.Transparent;
            this.lblUplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUplaceno.Location = new System.Drawing.Point(22, 16);
            this.lblUplaceno.Name = "lblUplaceno";
            this.lblUplaceno.Size = new System.Drawing.Size(104, 17);
            this.lblUplaceno.TabIndex = 22;
            this.lblUplaceno.Text = "Uplaćeno: 0,00";
            // 
            // lblIzdatak
            // 
            this.lblIzdatak.AutoSize = true;
            this.lblIzdatak.BackColor = System.Drawing.Color.Transparent;
            this.lblIzdatak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblIzdatak.Location = new System.Drawing.Point(191, 16);
            this.lblIzdatak.Name = "lblIzdatak";
            this.lblIzdatak.Size = new System.Drawing.Size(89, 17);
            this.lblIzdatak.TabIndex = 23;
            this.lblIzdatak.Text = "Izdatak: 0,00";
            // 
            // dtDatumVrijeme
            // 
            this.dtDatumVrijeme.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtDatumVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtDatumVrijeme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatumVrijeme.Location = new System.Drawing.Point(13, 128);
            this.dtDatumVrijeme.Name = "dtDatumVrijeme";
            this.dtDatumVrijeme.Size = new System.Drawing.Size(192, 29);
            this.dtDatumVrijeme.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(10, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Datum:";
            // 
            // txtOznaka
            // 
            this.txtOznaka.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtOznaka.Location = new System.Drawing.Point(229, 77);
            this.txtOznaka.Name = "txtOznaka";
            this.txtOznaka.Size = new System.Drawing.Size(300, 29);
            this.txtOznaka.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(226, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Oznaka:";
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(229, 128);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(300, 29);
            this.txtPartnerNaziv.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(226, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Partner:";
            // 
            // cmbOdabir
            // 
            this.cmbOdabir.BackColor = System.Drawing.SystemColors.Control;
            this.cmbOdabir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOdabir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOdabir.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cmbOdabir.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOdabir.FormattingEnabled = true;
            this.cmbOdabir.Location = new System.Drawing.Point(13, 13);
            this.cmbOdabir.Name = "cmbOdabir";
            this.cmbOdabir.Size = new System.Drawing.Size(516, 33);
            this.cmbOdabir.TabIndex = 164;
            this.cmbOdabir.SelectedValueChanged += new System.EventHandler(this.cmbOdabir_SelectedValueChanged);
            // 
            // btnIspisOdabranog
            // 
            this.btnIspisOdabranog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspisOdabranog.BackColor = System.Drawing.Color.White;
            this.btnIspisOdabranog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisOdabranog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisOdabranog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspisOdabranog.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspisOdabranog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisOdabranog.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspisOdabranog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisOdabranog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspisOdabranog.ForeColor = System.Drawing.Color.Black;
            this.btnIspisOdabranog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspisOdabranog.Location = new System.Drawing.Point(615, 532);
            this.btnIspisOdabranog.Name = "btnIspisOdabranog";
            this.btnIspisOdabranog.Size = new System.Drawing.Size(140, 30);
            this.btnIspisOdabranog.TabIndex = 165;
            this.btnIspisOdabranog.Text = "Ispis odabranog";
            this.btnIspisOdabranog.UseVisualStyleBackColor = false;
            this.btnIspisOdabranog.Click += new System.EventHandler(this.btnIspisOdabranog_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(535, 128);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 163;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.ForeColor = System.Drawing.Color.Black;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspis.Location = new System.Drawing.Point(807, 532);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(140, 30);
            this.btnIspis.TabIndex = 15;
            this.btnIspis.Text = "Ispis na A4";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.ForeColor = System.Drawing.Color.Black;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSpremi.Location = new System.Drawing.Point(690, 111);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(72, 49);
            this.btnSpremi.TabIndex = 13;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitaj.BackColor = System.Drawing.Color.White;
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUcitaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitaj.ForeColor = System.Drawing.Color.Black;
            this.btnUcitaj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUcitaj.Location = new System.Drawing.Point(169, 13);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(62, 49);
            this.btnUcitaj.TabIndex = 2;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUplaceno);
            this.panel1.Controls.Add(this.lblIzdatak);
            this.panel1.Location = new System.Drawing.Point(12, 532);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 49);
            this.panel1.TabIndex = 166;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.odDatuma);
            this.panel2.Controls.Add(this.btnUcitaj);
            this.panel2.Controls.Add(this.doDatuma);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(690, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 76);
            this.panel2.TabIndex = 167;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cmbOdabir);
            this.panel3.Controls.Add(this.txtIznos);
            this.panel3.Controls.Add(this.lblIznos);
            this.panel3.Controls.Add(this.dtDatumVrijeme);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtOznaka);
            this.panel3.Controls.Add(this.txtPartnerNaziv);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(627, 167);
            this.panel3.TabIndex = 168;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnEdit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnEdit.Location = new System.Drawing.Point(768, 111);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(72, 49);
            this.btnEdit.TabIndex = 169;
            this.btnEdit.Text = "Uredi";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCancel.Location = new System.Drawing.Point(846, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 49);
            this.btnCancel.TabIndex = 170;
            this.btnCancel.Text = "Odustani";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmBlagajnickiIzvjestaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(959, 574);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnIspisOdabranog);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.dgv);
            this.MinimumSize = new System.Drawing.Size(975, 38);
            this.Name = "frmBlagajnickiIzvjestaj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blagajnički izvještaj";
            this.Load += new System.EventHandler(this.frmBlagajnickiIzvjestaj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

#endregion
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.Label lblIznos;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.DateTimePicker doDatuma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUplaceno;
        private System.Windows.Forms.Label lblIzdatak;
        public System.Windows.Forms.DateTimePicker odDatuma;
        private System.Windows.Forms.DateTimePicker dtDatumVrijeme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.TextBox txtOznaka;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOdabir;
        private System.Windows.Forms.Button btnIspisOdabranog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokument;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaka;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uplaceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn izdatak;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_partner;
    }
}