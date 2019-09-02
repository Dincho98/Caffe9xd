namespace PCPOS.Fiskalizacija
{
    partial class frmNeupjeleTransakcije
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
            this.dgw = new System.Windows.Forms.DataGridView();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.lblU = new System.Windows.Forms.Label();
            this.btnIzvozDisc = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
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
            this.dgw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Location = new System.Drawing.Point(12, 12);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(960, 508);
            this.dgw.TabIndex = 0;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(836, 526);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(136, 33);
            this.btnNoviUnos.TabIndex = 128;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Fiskaliziraj";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNarudzbe_Click);
            // 
            // lblU
            // 
            this.lblU.AutoSize = true;
            this.lblU.BackColor = System.Drawing.Color.Transparent;
            this.lblU.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblU.Location = new System.Drawing.Point(13, 532);
            this.lblU.Name = "lblU";
            this.lblU.Size = new System.Drawing.Size(365, 26);
            this.lblU.TabIndex = 129;
            this.lblU.Text = "Ukupno ne fiskaliziranih računa 0";
            // 
            // btnIzvozDisc
            // 
            this.btnIzvozDisc.BackColor = System.Drawing.Color.White;
            this.btnIzvozDisc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIzvozDisc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzvozDisc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIzvozDisc.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIzvozDisc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIzvozDisc.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIzvozDisc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvozDisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnIzvozDisc.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIzvozDisc.Location = new System.Drawing.Point(694, 526);
            this.btnIzvozDisc.Name = "btnIzvozDisc";
            this.btnIzvozDisc.Size = new System.Drawing.Size(139, 33);
            this.btnIzvozDisc.TabIndex = 128;
            this.btnIzvozDisc.TabStop = false;
            this.btnIzvozDisc.Text = "Izvoz na disc";
            this.btnIzvozDisc.UseVisualStyleBackColor = false;
            this.btnIzvozDisc.Click += new System.EventHandler(this.btnIzvozDisc_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.Color.White;
            this.btnObrisi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnObrisi.ForeColor = System.Drawing.Color.Red;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnObrisi.Location = new System.Drawing.Point(549, 525);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(139, 33);
            this.btnObrisi.TabIndex = 128;
            this.btnObrisi.TabStop = false;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Visible = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // frmNeupjeleTransakcije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.Controls.Add(this.lblU);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnIzvozDisc);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.dgw);
            this.Name = "frmNeupjeleTransakcije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neuspjele transakcije prema porezni upravi";
            this.Load += new System.EventHandler(this.frmNeupjeleTransakcije_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Label lblU;
		private System.Windows.Forms.Button btnIzvozDisc;
		private System.Windows.Forms.Button btnObrisi;
    }
}