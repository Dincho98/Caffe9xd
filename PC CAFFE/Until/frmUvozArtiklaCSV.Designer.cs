namespace PCPOS.Until {
    partial class frmUvozArtiklaCSV {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnUcitajExcel = new System.Windows.Forms.Button();
            this.txtPathExcel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mjera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupa_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPostaviNeaktivneArtikle = new System.Windows.Forms.Button();
            this.btnObrisiArtikle = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnIzvoz = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUcitajExcel
            // 
            this.btnUcitajExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitajExcel.BackColor = System.Drawing.Color.White;
            this.btnUcitajExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitajExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitajExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUcitajExcel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUcitajExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitajExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUcitajExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitajExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnUcitajExcel.ForeColor = System.Drawing.Color.Black;
            this.btnUcitajExcel.Location = new System.Drawing.Point(175, 53);
            this.btnUcitajExcel.Name = "btnUcitajExcel";
            this.btnUcitajExcel.Size = new System.Drawing.Size(135, 35);
            this.btnUcitajExcel.TabIndex = 77;
            this.btnUcitajExcel.Text = "Učitaj excel";
            this.btnUcitajExcel.UseVisualStyleBackColor = false;
            this.btnUcitajExcel.Click += new System.EventHandler(this.btnUcitajExcel_Click);
            // 
            // txtPathExcel
            // 
            this.txtPathExcel.Location = new System.Drawing.Point(11, 27);
            this.txtPathExcel.Name = "txtPathExcel";
            this.txtPathExcel.Size = new System.Drawing.Size(294, 23);
            this.txtPathExcel.TabIndex = 75;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtPathExcel);
            this.groupBox1.Controls.Add(this.btnUcitajExcel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 92);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Putanja do CSV datoteke";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.mjera,
            this.pdv,
            this.pnp,
            this.grupa_,
            this.MPC});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Location = new System.Drawing.Point(18, 127);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(956, 391);
            this.dgv.TabIndex = 80;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.naziv.FillWeight = 250F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.Width = 250;
            // 
            // mjera
            // 
            this.mjera.HeaderText = "Mjera";
            this.mjera.Name = "mjera";
            // 
            // pdv
            // 
            this.pdv.HeaderText = "PDV";
            this.pdv.Name = "pdv";
            // 
            // pnp
            // 
            this.pnp.HeaderText = "PNP";
            this.pnp.Name = "pnp";
            // 
            // grupa_
            // 
            this.grupa_.HeaderText = "Grupa";
            this.grupa_.Name = "grupa_";
            // 
            // MPC
            // 
            this.MPC.HeaderText = "MPC";
            this.MPC.Name = "MPC";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnPostaviNeaktivneArtikle);
            this.groupBox2.Controls.Add(this.btnObrisiArtikle);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(347, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 92);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Upravljanje artiklima";
            // 
            // btnPostaviNeaktivneArtikle
            // 
            this.btnPostaviNeaktivneArtikle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPostaviNeaktivneArtikle.BackColor = System.Drawing.Color.White;
            this.btnPostaviNeaktivneArtikle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPostaviNeaktivneArtikle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostaviNeaktivneArtikle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPostaviNeaktivneArtikle.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPostaviNeaktivneArtikle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPostaviNeaktivneArtikle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPostaviNeaktivneArtikle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostaviNeaktivneArtikle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnPostaviNeaktivneArtikle.ForeColor = System.Drawing.Color.Black;
            this.btnPostaviNeaktivneArtikle.Location = new System.Drawing.Point(5, 55);
            this.btnPostaviNeaktivneArtikle.Name = "btnPostaviNeaktivneArtikle";
            this.btnPostaviNeaktivneArtikle.Size = new System.Drawing.Size(250, 31);
            this.btnPostaviNeaktivneArtikle.TabIndex = 78;
            this.btnPostaviNeaktivneArtikle.Text = "Postavi sve artikle da budu neaktivni";
            this.btnPostaviNeaktivneArtikle.UseVisualStyleBackColor = false;
            this.btnPostaviNeaktivneArtikle.Click += new System.EventHandler(this.btnPostaviNeaktivneArtikle_Click);
            // 
            // btnObrisiArtikle
            // 
            this.btnObrisiArtikle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisiArtikle.BackColor = System.Drawing.Color.White;
            this.btnObrisiArtikle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisiArtikle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiArtikle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisiArtikle.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnObrisiArtikle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisiArtikle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisiArtikle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiArtikle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnObrisiArtikle.ForeColor = System.Drawing.Color.Black;
            this.btnObrisiArtikle.Location = new System.Drawing.Point(5, 21);
            this.btnObrisiArtikle.Name = "btnObrisiArtikle";
            this.btnObrisiArtikle.Size = new System.Drawing.Size(250, 31);
            this.btnObrisiArtikle.TabIndex = 77;
            this.btnObrisiArtikle.Text = "Obriši sve artikle";
            this.btnObrisiArtikle.UseVisualStyleBackColor = false;
            this.btnObrisiArtikle.Click += new System.EventHandler(this.btnObrisiArtikle_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSpremi.ForeColor = System.Drawing.Color.Black;
            this.btnSpremi.Location = new System.Drawing.Point(724, 524);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(250, 33);
            this.btnSpremi.TabIndex = 82;
            this.btnSpremi.Text = "Spremi u bazu";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnIzvoz);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(626, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 92);
            this.groupBox3.TabIndex = 83;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Izvoz artikla u CSV datoteku";
            // 
            // btnIzvoz
            // 
            this.btnIzvoz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzvoz.BackColor = System.Drawing.Color.White;
            this.btnIzvoz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIzvoz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzvoz.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIzvoz.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIzvoz.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIzvoz.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIzvoz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvoz.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnIzvoz.ForeColor = System.Drawing.Color.Black;
            this.btnIzvoz.Location = new System.Drawing.Point(5, 18);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(250, 69);
            this.btnIzvoz.TabIndex = 77;
            this.btnIzvoz.Text = "Izvoz artikla u excel (CSV)";
            this.btnIzvoz.UseVisualStyleBackColor = false;
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(18, 524);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(6);
            this.label1.Size = new System.Drawing.Size(614, 40);
            this.label1.TabIndex = 84;
            this.label1.Text = "Kolone u CSV datoteci moraju biti posložene kao i u tablici u ovoj formi.\r\nKod sp" +
    "remanja CSV datoteke odaberite \"CSV (MS-DOS)\". Prilikom učitavanja datoteke u pr" +
    "ogram ugasite sve prozore Excela.";
            // 
            // frmUvozArtiklaCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(986, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmUvozArtiklaCSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uvoz artikla CSV";
            this.Load += new System.EventHandler(this.frmUvozArtiklaCSV_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUcitajExcel;
        private System.Windows.Forms.TextBox txtPathExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn mjera;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn pnp;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupa_;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPostaviNeaktivneArtikle;
        private System.Windows.Forms.Button btnObrisiArtikle;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.Label label1;
    }
}