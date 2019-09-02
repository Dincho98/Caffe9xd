namespace PCPOS.Caffe {
    partial class frmZid {
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
            this.label1 = new System.Windows.Forms.Label();
            this.nuTop = new System.Windows.Forms.NumericUpDown();
            this.nuHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nuWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nuLeft = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nuTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Od Vrha:";
            // 
            // nuTop
            // 
            this.nuTop.Location = new System.Drawing.Point(99, 74);
            this.nuTop.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nuTop.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nuTop.Name = "nuTop";
            this.nuTop.Size = new System.Drawing.Size(57, 20);
            this.nuTop.TabIndex = 1;
            this.nuTop.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // nuHeight
            // 
            this.nuHeight.Location = new System.Drawing.Point(99, 152);
            this.nuHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nuHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nuHeight.Name = "nuHeight";
            this.nuHeight.Size = new System.Drawing.Size(57, 20);
            this.nuHeight.TabIndex = 3;
            this.nuHeight.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Visina:";
            // 
            // nuWidth
            // 
            this.nuWidth.Location = new System.Drawing.Point(99, 126);
            this.nuWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nuWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nuWidth.Name = "nuWidth";
            this.nuWidth.Size = new System.Drawing.Size(57, 20);
            this.nuWidth.TabIndex = 5;
            this.nuWidth.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Širina:";
            // 
            // nuLeft
            // 
            this.nuLeft.Location = new System.Drawing.Point(99, 100);
            this.nuLeft.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nuLeft.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nuLeft.Name = "nuLeft";
            this.nuLeft.Size = new System.Drawing.Size(57, 20);
            this.nuLeft.TabIndex = 7;
            this.nuLeft.ValueChanged += new System.EventHandler(this.nuTop_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Od lijeve strane:";
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Location = new System.Drawing.Point(15, 195);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(141, 29);
            this.btnSpremi.TabIndex = 8;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Za debljinu zida optimalno je postaviti 33 piksela\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Visina cijele površine za stolove je:\r\n";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Širina cijele površine za stolove je:";
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.Color.White;
            this.btnObrisi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Location = new System.Drawing.Point(162, 195);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(78, 29);
            this.btnObrisi.TabIndex = 12;
            this.btnObrisi.Text = "Obriši zid";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmZid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(252, 245);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.nuLeft);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nuWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nuHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nuTop);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmZid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zid";
            this.Load += new System.EventHandler(this.frmZid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nuTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuTop;
        private System.Windows.Forms.NumericUpDown nuHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nuWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nuLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnObrisi;
    }
}