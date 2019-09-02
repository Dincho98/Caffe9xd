namespace PCPOS {
    partial class frmMiniOtpremnica {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.btnPartner = new System.Windows.Forms.Button();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNazivNa = new System.Windows.Forms.Label();
            this.txtNazivPartner = new System.Windows.Forms.TextBox();
            this.txtSifraPatner = new System.Windows.Forms.TextBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.rbNaPartnera = new System.Windows.Forms.RadioButton();
            this.rbNaSobu = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSobe = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lezaji = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSobe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.SystemColors.Control;
            this.btnPartner.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.btnPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPartner.Location = new System.Drawing.Point(91, 102);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(30, 25);
            this.btnPartner.TabIndex = 14;
            this.btnPartner.Text = "...";
            this.btnPartner.UseVisualStyleBackColor = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.dtpDatum.CalendarMonthBackground = System.Drawing.SystemColors.MenuBar;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(12, 150);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(227, 23);
            this.dtpDatum.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Datum:";
            // 
            // lblNazivNa
            // 
            this.lblNazivNa.AutoSize = true;
            this.lblNazivNa.BackColor = System.Drawing.Color.Transparent;
            this.lblNazivNa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNazivNa.Location = new System.Drawing.Point(12, 82);
            this.lblNazivNa.Name = "lblNazivNa";
            this.lblNazivNa.Size = new System.Drawing.Size(90, 17);
            this.lblNazivNa.TabIndex = 17;
            this.lblNazivNa.Text = "Poslovni par.";
            // 
            // txtNazivPartner
            // 
            this.txtNazivPartner.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtNazivPartner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNazivPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtNazivPartner.Location = new System.Drawing.Point(127, 102);
            this.txtNazivPartner.Name = "txtNazivPartner";
            this.txtNazivPartner.ReadOnly = true;
            this.txtNazivPartner.Size = new System.Drawing.Size(227, 25);
            this.txtNazivPartner.TabIndex = 15;
            // 
            // txtSifraPatner
            // 
            this.txtSifraPatner.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSifraPatner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSifraPatner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.txtSifraPatner.Location = new System.Drawing.Point(12, 102);
            this.txtSifraPatner.Name = "txtSifraPatner";
            this.txtSifraPatner.Size = new System.Drawing.Size(73, 25);
            this.txtSifraPatner.TabIndex = 13;
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.BackColor = System.Drawing.SystemColors.MenuBar;
            this.rtbNapomena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.rtbNapomena.Location = new System.Drawing.Point(12, 196);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(342, 147);
            this.rtbNapomena.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(12, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Napomena:";
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSpremi.Location = new System.Drawing.Point(12, 349);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(95, 50);
            this.btnSpremi.TabIndex = 128;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOdustani.Location = new System.Drawing.Point(259, 349);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(95, 50);
            this.btnOdustani.TabIndex = 127;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            this.bgSinkronizacija.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSinkronizacija_RunWorkerCompleted);
            // 
            // rbNaPartnera
            // 
            this.rbNaPartnera.AutoSize = true;
            this.rbNaPartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbNaPartnera.Location = new System.Drawing.Point(13, 19);
            this.rbNaPartnera.Name = "rbNaPartnera";
            this.rbNaPartnera.Size = new System.Drawing.Size(111, 24);
            this.rbNaPartnera.TabIndex = 129;
            this.rbNaPartnera.TabStop = true;
            this.rbNaPartnera.Text = "Na partnera";
            this.rbNaPartnera.UseVisualStyleBackColor = true;
            this.rbNaPartnera.CheckedChanged += new System.EventHandler(this.rbNaPartnera_CheckedChanged);
            // 
            // rbNaSobu
            // 
            this.rbNaSobu.AutoSize = true;
            this.rbNaSobu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbNaSobu.Location = new System.Drawing.Point(240, 19);
            this.rbNaSobu.Name = "rbNaSobu";
            this.rbNaSobu.Size = new System.Drawing.Size(86, 24);
            this.rbNaSobu.TabIndex = 130;
            this.rbNaSobu.TabStop = true;
            this.rbNaSobu.Text = "Na sobu";
            this.rbNaSobu.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbNaPartnera);
            this.panel1.Controls.Add(this.rbNaSobu);
            this.panel1.Location = new System.Drawing.Point(15, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 66);
            this.panel1.TabIndex = 131;
            // 
            // dgvSobe
            // 
            this.dgvSobe.AllowUserToAddRows = false;
            this.dgvSobe.AllowUserToDeleteRows = false;
            this.dgvSobe.AllowUserToResizeRows = false;
            this.dgvSobe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSobe.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSobe.BackgroundColor = System.Drawing.Color.White;
            this.dgvSobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSobe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.naziv,
            this.lezaji,
            this.cijena});
            this.dgvSobe.Location = new System.Drawing.Point(12, 134);
            this.dgvSobe.MultiSelect = false;
            this.dgvSobe.Name = "dgvSobe";
            this.dgvSobe.ReadOnly = true;
            this.dgvSobe.RowHeadersWidth = 10;
            this.dgvSobe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSobe.Size = new System.Drawing.Size(342, 209);
            this.dgvSobe.TabIndex = 132;
            this.dgvSobe.Tag = "5";
            this.dgvSobe.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSobe_CellDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // naziv
            // 
            this.naziv.DataPropertyName = "naziv";
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // lezaji
            // 
            this.lezaji.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lezaji.DataPropertyName = "lezaji";
            this.lezaji.HeaderText = "Ležaji";
            this.lezaji.Name = "lezaji";
            this.lezaji.ReadOnly = true;
            this.lezaji.Width = 80;
            // 
            // cijena
            // 
            this.cijena.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cijena.DataPropertyName = "cijena";
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            this.cijena.Width = 80;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.BackColor = System.Drawing.Color.White;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDown.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.btnDown.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDown.Location = new System.Drawing.Point(185, 349);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(54, 50);
            this.btnDown.TabIndex = 148;
            this.btnDown.Tag = "dgvSobe";
            this.btnDown.Text = "▼";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.BackColor = System.Drawing.Color.White;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUp.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.btnUp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUp.Location = new System.Drawing.Point(127, 349);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(54, 50);
            this.btnUp.TabIndex = 147;
            this.btnUp.Tag = "dgvSobe";
            this.btnUp.Text = "▲";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // frmMiniOtpremnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(366, 411);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.rtbNapomena);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNazivNa);
            this.Controls.Add(this.txtNazivPartner);
            this.Controls.Add(this.txtSifraPatner);
            this.Controls.Add(this.dgvSobe);
            this.MaximumSize = new System.Drawing.Size(382, 449);
            this.MinimumSize = new System.Drawing.Size(382, 449);
            this.Name = "frmMiniOtpremnica";
            this.Text = "Otpremnica";
            this.Load += new System.EventHandler(this.frmMiniOtpremnica_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSobe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNazivNa;
        private System.Windows.Forms.TextBox txtNazivPartner;
        private System.Windows.Forms.TextBox txtSifraPatner;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSpremi;
        public System.Windows.Forms.Button btnOdustani;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.RadioButton rbNaPartnera;
        private System.Windows.Forms.RadioButton rbNaSobu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvSobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn lezaji;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
    }
}