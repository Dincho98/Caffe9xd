namespace PCPOS.Caffe {
    partial class frmDodajRacunKartica {
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
            this.pnlDodajRacun = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIznosRacuna = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnZapisiRacun = new System.Windows.Forms.Button();
            this.txtBrojRacuna = new System.Windows.Forms.TextBox();
            this.pnlDodajRacun.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDodajRacun
            // 
            this.pnlDodajRacun.Controls.Add(this.label3);
            this.pnlDodajRacun.Controls.Add(this.txtIznosRacuna);
            this.pnlDodajRacun.Controls.Add(this.label2);
            this.pnlDodajRacun.Controls.Add(this.btnCancel);
            this.pnlDodajRacun.Controls.Add(this.btnZapisiRacun);
            this.pnlDodajRacun.Controls.Add(this.txtBrojRacuna);
            this.pnlDodajRacun.Location = new System.Drawing.Point(34, 28);
            this.pnlDodajRacun.Name = "pnlDodajRacun";
            this.pnlDodajRacun.Size = new System.Drawing.Size(217, 207);
            this.pnlDodajRacun.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(4, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Iznos računa:";
            // 
            // txtIznosRacuna
            // 
            this.txtIznosRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIznosRacuna.Location = new System.Drawing.Point(3, 74);
            this.txtIznosRacuna.Name = "txtIznosRacuna";
            this.txtIznosRacuna.Size = new System.Drawing.Size(210, 29);
            this.txtIznosRacuna.TabIndex = 4;
            this.txtIznosRacuna.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIznosRacuna_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Broj računa:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(4, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(210, 45);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Odustani";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnZapisiRacun
            // 
            this.btnZapisiRacun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnZapisiRacun.BackColor = System.Drawing.Color.White;
            this.btnZapisiRacun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZapisiRacun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZapisiRacun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnZapisiRacun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnZapisiRacun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnZapisiRacun.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnZapisiRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZapisiRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnZapisiRacun.Location = new System.Drawing.Point(3, 110);
            this.btnZapisiRacun.Name = "btnZapisiRacun";
            this.btnZapisiRacun.Size = new System.Drawing.Size(210, 45);
            this.btnZapisiRacun.TabIndex = 1;
            this.btnZapisiRacun.Text = "Dodaj";
            this.btnZapisiRacun.UseVisualStyleBackColor = false;
            this.btnZapisiRacun.Click += new System.EventHandler(this.btnZapisiRacun_Click);
            // 
            // txtBrojRacuna
            // 
            this.txtBrojRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojRacuna.Location = new System.Drawing.Point(3, 23);
            this.txtBrojRacuna.Name = "txtBrojRacuna";
            this.txtBrojRacuna.Size = new System.Drawing.Size(210, 29);
            this.txtBrojRacuna.TabIndex = 0;
            this.txtBrojRacuna.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBrojRacuna_KeyPress);
            // 
            // frmDodajRacunKartica
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.pnlDodajRacun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "frmDodajRacunKartica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodavanje računa an karticu";
            this.Load += new System.EventHandler(this.frmDodajRacunKartica_Load);
            this.pnlDodajRacun.ResumeLayout(false);
            this.pnlDodajRacun.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDodajRacun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIznosRacuna;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnZapisiRacun;
        private System.Windows.Forms.TextBox txtBrojRacuna;
    }
}