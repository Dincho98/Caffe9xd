namespace PCPOS {
    partial class frmKoristiKarticu {
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
            this.btnOdustani = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIznosZaOduzeti = new System.Windows.Forms.TextBox();
            this.btnNastavi = new System.Windows.Forms.Button();
            this.lblUkupnoBodovi = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUkupniIznosRacuna = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOdustani
            // 
            this.btnOdustani.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdustani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOdustani.ForeColor = System.Drawing.Color.White;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOdustani.Location = new System.Drawing.Point(368, 154);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(95, 50);
            this.btnOdustani.TabIndex = 124;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 16);
            this.label1.TabIndex = 125;
            this.label1.Text = "Iznos koji se oduzima od ukupnog iznosa računa";
            // 
            // txtIznosZaOduzeti
            // 
            this.txtIznosZaOduzeti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIznosZaOduzeti.Location = new System.Drawing.Point(15, 93);
            this.txtIznosZaOduzeti.Name = "txtIznosZaOduzeti";
            this.txtIznosZaOduzeti.Size = new System.Drawing.Size(236, 22);
            this.txtIznosZaOduzeti.TabIndex = 126;
            this.txtIznosZaOduzeti.Text = "0";
            this.txtIznosZaOduzeti.TextChanged += new System.EventHandler(this.txtIznosZaOduzeti_TextChanged);
            this.txtIznosZaOduzeti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIznosZaOduzeti_KeyPress);
            // 
            // btnNastavi
            // 
            this.btnNastavi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNastavi.BackColor = System.Drawing.Color.White;
            this.btnNastavi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNastavi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNastavi.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNastavi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNastavi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNastavi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNastavi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNastavi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNastavi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNastavi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNastavi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNastavi.Location = new System.Drawing.Point(182, 154);
            this.btnNastavi.Name = "btnNastavi";
            this.btnNastavi.Size = new System.Drawing.Size(95, 50);
            this.btnNastavi.TabIndex = 127;
            this.btnNastavi.Text = "Nastavi";
            this.btnNastavi.UseVisualStyleBackColor = false;
            this.btnNastavi.Click += new System.EventHandler(this.btnNastavi_Click);
            // 
            // lblUkupnoBodovi
            // 
            this.lblUkupnoBodovi.AutoSize = true;
            this.lblUkupnoBodovi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUkupnoBodovi.Location = new System.Drawing.Point(129, 9);
            this.lblUkupnoBodovi.Name = "lblUkupnoBodovi";
            this.lblUkupnoBodovi.Size = new System.Drawing.Size(15, 16);
            this.lblUkupnoBodovi.TabIndex = 128;
            this.lblUkupnoBodovi.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 129;
            this.label2.Text = "Ukupno bodova: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 16);
            this.label3.TabIndex = 130;
            this.label3.Text = "Ukupni iznos računa: ";
            // 
            // lblUkupniIznosRacuna
            // 
            this.lblUkupniIznosRacuna.AutoSize = true;
            this.lblUkupniIznosRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUkupniIznosRacuna.Location = new System.Drawing.Point(153, 25);
            this.lblUkupniIznosRacuna.Name = "lblUkupniIznosRacuna";
            this.lblUkupniIznosRacuna.Size = new System.Drawing.Size(15, 16);
            this.lblUkupniIznosRacuna.TabIndex = 131;
            this.lblUkupniIznosRacuna.Text = "0";
            // 
            // frmKoristiKarticu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.CancelButton = this.btnOdustani;
            this.ClientSize = new System.Drawing.Size(475, 216);
            this.Controls.Add(this.lblUkupniIznosRacuna);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUkupnoBodovi);
            this.Controls.Add(this.btnNastavi);
            this.Controls.Add(this.txtIznosZaOduzeti);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOdustani);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(475, 300);
            this.Name = "frmKoristiKarticu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kartica";
            this.Load += new System.EventHandler(this.frmKoristiKarticu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNastavi;
        private System.Windows.Forms.Label lblUkupnoBodovi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUkupniIznosRacuna;
        public System.Windows.Forms.TextBox txtIznosZaOduzeti;
    }
}