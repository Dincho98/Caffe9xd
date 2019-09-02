namespace PCPOS.Caffe {
    partial class frmDodajRacunNaKarticu {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDodajRacun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(12, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(177, 26);
            this.textBox1.TabIndex = 0;
            // 
            // btnDodajRacun
            // 
            this.btnDodajRacun.BackColor = System.Drawing.Color.White;
            this.btnDodajRacun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajRacun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajRacun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajRacun.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodajRacun.Location = new System.Drawing.Point(194, 35);
            this.btnDodajRacun.Name = "btnDodajRacun";
            this.btnDodajRacun.Size = new System.Drawing.Size(177, 26);
            this.btnDodajRacun.TabIndex = 1;
            this.btnDodajRacun.Text = "Dodaj račun";
            this.btnDodajRacun.UseVisualStyleBackColor = false;
            // 
            // frmDodajRacunNaKarticu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(383, 279);
            this.Controls.Add(this.btnDodajRacun);
            this.Controls.Add(this.textBox1);
            this.Name = "frmDodajRacunNaKarticu";
            this.Text = "Dodaj Racun Na Karticu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDodajRacun;
    }
}