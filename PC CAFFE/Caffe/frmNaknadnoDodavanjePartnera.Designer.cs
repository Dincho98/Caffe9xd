namespace PCPOS.Caffe {
    partial class frmNaknadnoDodavanjePartnera {
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
            this.lblBrojRacuna = new System.Windows.Forms.Label();
            this.txtBrojRacuna = new System.Windows.Forms.TextBox();
            this.btnNaknadnoDodavanjePartnera = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBrojRacuna
            // 
            this.lblBrojRacuna.AutoSize = true;
            this.lblBrojRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblBrojRacuna.Location = new System.Drawing.Point(12, 9);
            this.lblBrojRacuna.Name = "lblBrojRacuna";
            this.lblBrojRacuna.Size = new System.Drawing.Size(85, 17);
            this.lblBrojRacuna.TabIndex = 0;
            this.lblBrojRacuna.Text = "Broj računa:";
            // 
            // txtBrojRacuna
            // 
            this.txtBrojRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtBrojRacuna.Location = new System.Drawing.Point(12, 29);
            this.txtBrojRacuna.Name = "txtBrojRacuna";
            this.txtBrojRacuna.Size = new System.Drawing.Size(161, 26);
            this.txtBrojRacuna.TabIndex = 1;
            // 
            // btnNaknadnoDodavanjePartnera
            // 
            this.btnNaknadnoDodavanjePartnera.BackColor = System.Drawing.Color.White;
            this.btnNaknadnoDodavanjePartnera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNaknadnoDodavanjePartnera.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNaknadnoDodavanjePartnera.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNaknadnoDodavanjePartnera.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNaknadnoDodavanjePartnera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaknadnoDodavanjePartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNaknadnoDodavanjePartnera.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNaknadnoDodavanjePartnera.Location = new System.Drawing.Point(265, 15);
            this.btnNaknadnoDodavanjePartnera.Name = "btnNaknadnoDodavanjePartnera";
            this.btnNaknadnoDodavanjePartnera.Size = new System.Drawing.Size(156, 40);
            this.btnNaknadnoDodavanjePartnera.TabIndex = 28;
            this.btnNaknadnoDodavanjePartnera.Text = "Dodaj partnera";
            this.btnNaknadnoDodavanjePartnera.UseVisualStyleBackColor = false;
            this.btnNaknadnoDodavanjePartnera.Click += new System.EventHandler(this.btnNaknadnoDodavanjePartnera_Click);
            // 
            // frmNaknadnoDodavanjePartnera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(481, 247);
            this.Controls.Add(this.btnNaknadnoDodavanjePartnera);
            this.Controls.Add(this.txtBrojRacuna);
            this.Controls.Add(this.lblBrojRacuna);
            this.Name = "frmNaknadnoDodavanjePartnera";
            this.Text = "Naknadno dodavanje partnera";
            this.Load += new System.EventHandler(this.frmNaknadnoDodavanjePartnera_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBrojRacuna;
        private System.Windows.Forms.TextBox txtBrojRacuna;
        private System.Windows.Forms.Button btnNaknadnoDodavanjePartnera;
    }
}