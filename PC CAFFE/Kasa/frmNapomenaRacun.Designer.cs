namespace PCPOS.Kasa
{
    partial class frmNapomenaRacun
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
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ispis = new System.Windows.Forms.Button();
            this.zanemari = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Location = new System.Drawing.Point(24, 86);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(330, 167);
            this.rtbNapomena.TabIndex = 0;
            this.rtbNapomena.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ovaj tekst će se ispisati na kraju računa.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "(Ako ne želite ispis na kraju računa, idite na postavke";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(305, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "te isključite opciju \'Ispis napomene na maloprodajnim računima\')";
            // 
            // ispis
            // 
            this.ispis.BackColor = System.Drawing.Color.White;
            this.ispis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ispis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ispis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.ispis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.ispis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.ispis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ispis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ispis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ispis.Location = new System.Drawing.Point(34, 270);
            this.ispis.Name = "ispis";
            this.ispis.Size = new System.Drawing.Size(116, 33);
            this.ispis.TabIndex = 6;
            this.ispis.Text = "Ispis";
            this.ispis.UseVisualStyleBackColor = false;
            this.ispis.Click += new System.EventHandler(this.ispis_Click);
            // 
            // zanemari
            // 
            this.zanemari.BackColor = System.Drawing.Color.White;
            this.zanemari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zanemari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zanemari.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.zanemari.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.zanemari.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.zanemari.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.zanemari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zanemari.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zanemari.Location = new System.Drawing.Point(222, 270);
            this.zanemari.Name = "zanemari";
            this.zanemari.Size = new System.Drawing.Size(116, 33);
            this.zanemari.TabIndex = 7;
            this.zanemari.Text = "Zanemari";
            this.zanemari.UseVisualStyleBackColor = false;
            this.zanemari.Click += new System.EventHandler(this.zanemari_Click);
            // 
            // frmNapomenaRacun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 333);
            this.Controls.Add(this.zanemari);
            this.Controls.Add(this.ispis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbNapomena);
            this.Name = "frmNapomenaRacun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Napomena na računu";
            this.Load += new System.EventHandler(this.frmNapomenaRacun_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ispis;
        private System.Windows.Forms.Button zanemari;
    }
}