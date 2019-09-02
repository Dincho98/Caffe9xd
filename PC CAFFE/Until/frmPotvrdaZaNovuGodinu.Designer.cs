namespace PCPOS.Until
{
    partial class frmPotvrdaZaNovuGodinu
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
            this.btnPostavi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBNova = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbGodina = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPostavi
            // 
            this.btnPostavi.BackColor = System.Drawing.Color.White;
            this.btnPostavi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPostavi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPostavi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPostavi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostavi.ForeColor = System.Drawing.Color.Black;
            this.btnPostavi.Location = new System.Drawing.Point(169, 102);
            this.btnPostavi.Name = "btnPostavi";
            this.btnPostavi.Size = new System.Drawing.Size(109, 27);
            this.btnPostavi.TabIndex = 0;
            this.btnPostavi.Text = "Kreiraj novu bazu";
            this.btnPostavi.UseVisualStyleBackColor = false;
            this.btnPostavi.Click += new System.EventHandler(this.btnPostavi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lozinka za pristup:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(13, 28);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(286, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baza koju trenutno koristite:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nova baza koju želite kreirati:";
            // 
            // txtDBNova
            // 
            this.txtDBNova.ForeColor = System.Drawing.Color.Black;
            this.txtDBNova.Location = new System.Drawing.Point(16, 76);
            this.txtDBNova.Name = "txtDBNova";
            this.txtDBNova.Size = new System.Drawing.Size(262, 20);
            this.txtDBNova.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbGodina);
            this.groupBox1.Controls.Add(this.txtDBNova);
            this.groupBox1.Controls.Add(this.btnPostavi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(13, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 135);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BAZA PODATAKA";
            // 
            // cbGodina
            // 
            this.cbGodina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGodina.FormattingEnabled = true;
            this.cbGodina.Location = new System.Drawing.Point(16, 39);
            this.cbGodina.Name = "cbGodina";
            this.cbGodina.Size = new System.Drawing.Size(262, 21);
            this.cbGodina.TabIndex = 3;
            // 
            // frmPotvrdaZaNovuGodinu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(309, 218);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Name = "frmPotvrdaZaNovuGodinu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kreiranje nove baze";
            this.Load += new System.EventHandler(this.frmPotvrdaZaNovuGodinu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPostavi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtDBNova;
        private System.Windows.Forms.ComboBox cbGodina;
    }
}