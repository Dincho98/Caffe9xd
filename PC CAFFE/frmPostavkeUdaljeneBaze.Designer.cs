namespace PCPOS
{
    partial class frmPostavkeUdaljeneBaze
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRemoteNameDatabase = new System.Windows.Forms.ComboBox();
            this.chbActive = new System.Windows.Forms.CheckBox();
            this.txtRemoteLozinka = new System.Windows.Forms.TextBox();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.txtRemoteUsername = new System.Windows.Forms.TextBox();
            this.txtRemoteSpremi = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRemoteTest = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemoteImeServera = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.cbRemoteNameDatabase);
            this.groupBox2.Controls.Add(this.chbActive);
            this.groupBox2.Controls.Add(this.txtRemoteLozinka);
            this.groupBox2.Controls.Add(this.txtRemotePort);
            this.groupBox2.Controls.Add(this.txtRemoteUsername);
            this.groupBox2.Controls.Add(this.txtRemoteSpremi);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtRemoteTest);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRemoteImeServera);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 293);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Udaljena baza";
            // 
            // cbRemoteNameDatabase
            // 
            this.cbRemoteNameDatabase.FormattingEnabled = true;
            this.cbRemoteNameDatabase.Location = new System.Drawing.Point(141, 133);
            this.cbRemoteNameDatabase.Name = "cbRemoteNameDatabase";
            this.cbRemoteNameDatabase.Size = new System.Drawing.Size(198, 21);
            this.cbRemoteNameDatabase.TabIndex = 51;
            // 
            // chbActive
            // 
            this.chbActive.AutoSize = true;
            this.chbActive.Location = new System.Drawing.Point(326, 188);
            this.chbActive.Name = "chbActive";
            this.chbActive.Size = new System.Drawing.Size(15, 14);
            this.chbActive.TabIndex = 49;
            this.chbActive.UseVisualStyleBackColor = true;
            // 
            // txtRemoteLozinka
            // 
            this.txtRemoteLozinka.Location = new System.Drawing.Point(141, 160);
            this.txtRemoteLozinka.Name = "txtRemoteLozinka";
            this.txtRemoteLozinka.Size = new System.Drawing.Size(198, 20);
            this.txtRemoteLozinka.TabIndex = 45;
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(141, 107);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(198, 20);
            this.txtRemotePort.TabIndex = 45;
            // 
            // txtRemoteUsername
            // 
            this.txtRemoteUsername.Location = new System.Drawing.Point(141, 81);
            this.txtRemoteUsername.Name = "txtRemoteUsername";
            this.txtRemoteUsername.Size = new System.Drawing.Size(198, 20);
            this.txtRemoteUsername.TabIndex = 45;
            // 
            // txtRemoteSpremi
            // 
            this.txtRemoteSpremi.BackColor = System.Drawing.Color.White;
            this.txtRemoteSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.txtRemoteSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.txtRemoteSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.txtRemoteSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.txtRemoteSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtRemoteSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtRemoteSpremi.Location = new System.Drawing.Point(262, 234);
            this.txtRemoteSpremi.Name = "txtRemoteSpremi";
            this.txtRemoteSpremi.Size = new System.Drawing.Size(77, 37);
            this.txtRemoteSpremi.TabIndex = 48;
            this.txtRemoteSpremi.Text = "Spremi";
            this.txtRemoteSpremi.UseVisualStyleBackColor = false;
            this.txtRemoteSpremi.Click += new System.EventHandler(this.txtRemoteSpremi_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(34, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "Aktivan udaljen server:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(34, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 17);
            this.label11.TabIndex = 47;
            this.label11.Text = "Lozinka:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(34, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 17);
            this.label8.TabIndex = 47;
            this.label8.Text = "Ime baze";
            // 
            // txtRemoteTest
            // 
            this.txtRemoteTest.BackColor = System.Drawing.Color.White;
            this.txtRemoteTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.txtRemoteTest.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.txtRemoteTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.txtRemoteTest.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.txtRemoteTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtRemoteTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtRemoteTest.Location = new System.Drawing.Point(189, 234);
            this.txtRemoteTest.Name = "txtRemoteTest";
            this.txtRemoteTest.Size = new System.Drawing.Size(67, 37);
            this.txtRemoteTest.TabIndex = 48;
            this.txtRemoteTest.Text = "Testiraj";
            this.txtRemoteTest.UseVisualStyleBackColor = false;
            this.txtRemoteTest.Click += new System.EventHandler(this.txtRemoteTest_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(34, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.TabIndex = 47;
            this.label5.Text = "Port";
            // 
            // txtRemoteImeServera
            // 
            this.txtRemoteImeServera.Location = new System.Drawing.Point(141, 55);
            this.txtRemoteImeServera.Name = "txtRemoteImeServera";
            this.txtRemoteImeServera.Size = new System.Drawing.Size(198, 20);
            this.txtRemoteImeServera.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(34, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 17);
            this.label9.TabIndex = 47;
            this.label9.Text = "Korisničko ime:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(34, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 17);
            this.label10.TabIndex = 46;
            this.label10.Text = "Ime servera:";
            // 
            // frmPostavkeUdaljeneBaze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(411, 317);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmPostavkeUdaljeneBaze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Postavke udaljene baze";
            this.Load += new System.EventHandler(this.frmPostavkeUdaljeneBaze_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRemoteNameDatabase;
        private System.Windows.Forms.CheckBox chbActive;
        private System.Windows.Forms.TextBox txtRemoteLozinka;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.TextBox txtRemoteUsername;
        private System.Windows.Forms.Button txtRemoteSpremi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button txtRemoteTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRemoteImeServera;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}