namespace PCPOS.Caffe {
    partial class frmKarticaSkladista {
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pocetno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.primka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kalkulacija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.izdatnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otpis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.racuni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otpremnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnIspis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.Silver;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.pocetno,
            this.primka,
            this.kalkulacija,
            this.izdatnica,
            this.inventura,
            this.otpis,
            this.racuni,
            this.fakture,
            this.otpremnica,
            this.ms,
            this.stanje});
            this.dgv.GridColor = System.Drawing.Color.Gray;
            this.dgv.Location = new System.Drawing.Point(12, 58);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(966, 471);
            this.dgv.TabIndex = 0;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // pocetno
            // 
            this.pocetno.HeaderText = "Početno";
            this.pocetno.Name = "pocetno";
            this.pocetno.ReadOnly = true;
            // 
            // primka
            // 
            this.primka.HeaderText = "Primka";
            this.primka.Name = "primka";
            this.primka.ReadOnly = true;
            // 
            // kalkulacija
            // 
            this.kalkulacija.HeaderText = "Kalkulacija";
            this.kalkulacija.Name = "kalkulacija";
            this.kalkulacija.ReadOnly = true;
            // 
            // izdatnica
            // 
            this.izdatnica.HeaderText = "Izdatnica";
            this.izdatnica.Name = "izdatnica";
            this.izdatnica.ReadOnly = true;
            // 
            // inventura
            // 
            this.inventura.HeaderText = "Inventurna razlika";
            this.inventura.Name = "inventura";
            this.inventura.ReadOnly = true;
            // 
            // otpis
            // 
            this.otpis.HeaderText = "Otpis";
            this.otpis.Name = "otpis";
            this.otpis.ReadOnly = true;
            // 
            // racuni
            // 
            this.racuni.HeaderText = "Računi";
            this.racuni.Name = "racuni";
            this.racuni.ReadOnly = true;
            // 
            // fakture
            // 
            this.fakture.HeaderText = "Fakture";
            this.fakture.Name = "fakture";
            this.fakture.ReadOnly = true;
            // 
            // otpremnica
            // 
            this.otpremnica.HeaderText = "Otpremnica";
            this.otpremnica.Name = "otpremnica";
            this.otpremnica.ReadOnly = true;
            // 
            // ms
            // 
            this.ms.HeaderText = "Međusklasišnice";
            this.ms.Name = "ms";
            this.ms.ReadOnly = true;
            // 
            // stanje
            // 
            this.stanje.HeaderText = "Trenutno stanje";
            this.stanje.Name = "stanje";
            this.stanje.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Do datuma:";
            // 
            // dtpDoDatuma
            // 
            this.dtpDoDatuma.CustomFormat = "dd.MM.yyyy  HH:mm:ss";
            this.dtpDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDatuma.Location = new System.Drawing.Point(12, 29);
            this.dtpDoDatuma.Name = "dtpDoDatuma";
            this.dtpDoDatuma.Size = new System.Drawing.Size(259, 23);
            this.dtpDoDatuma.TabIndex = 81;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(287, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 82;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspis.Location = new System.Drawing.Point(508, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(97, 40);
            this.btnIspis.TabIndex = 83;
            this.btnIspis.Text = "Ispis   ";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // frmKarticaSkladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(990, 541);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dtpDoDatuma);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv);
            this.Name = "frmKarticaSkladista";
            this.Text = "Kartica skladišta";
            this.Load += new System.EventHandler(this.frmKarticaSkladista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn pocetno;
        private System.Windows.Forms.DataGridViewTextBoxColumn primka;
        private System.Windows.Forms.DataGridViewTextBoxColumn kalkulacija;
        private System.Windows.Forms.DataGridViewTextBoxColumn izdatnica;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventura;
        private System.Windows.Forms.DataGridViewTextBoxColumn otpis;
        private System.Windows.Forms.DataGridViewTextBoxColumn racuni;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakture;
        private System.Windows.Forms.DataGridViewTextBoxColumn otpremnica;
        private System.Windows.Forms.DataGridViewTextBoxColumn ms;
        private System.Windows.Forms.DataGridViewTextBoxColumn stanje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDoDatuma;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnIspis;
    }
}