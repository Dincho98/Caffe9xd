namespace PCPOS.Until
{
    partial class frmPromjenaGodine
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
            this.btnPromjenaGodine = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbGodina = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPromjenaGodine
            // 
            this.btnPromjenaGodine.BackColor = System.Drawing.Color.White;
            this.btnPromjenaGodine.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPromjenaGodine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromjenaGodine.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPromjenaGodine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromjenaGodine.ForeColor = System.Drawing.Color.Black;
            this.btnPromjenaGodine.Location = new System.Drawing.Point(15, 64);
            this.btnPromjenaGodine.Name = "btnPromjenaGodine";
            this.btnPromjenaGodine.Size = new System.Drawing.Size(142, 27);
            this.btnPromjenaGodine.TabIndex = 0;
            this.btnPromjenaGodine.Text = "Promjena godine";
            this.btnPromjenaGodine.UseVisualStyleBackColor = false;
            this.btnPromjenaGodine.Click += new System.EventHandler(this.btnPromjenaGodine_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Godina:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbGodina);
            this.groupBox1.Controls.Add(this.btnPromjenaGodine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BAZA PODATAKA";
            // 
            // cbGodina
            // 
            this.cbGodina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGodina.FormattingEnabled = true;
            this.cbGodina.Location = new System.Drawing.Point(15, 40);
            this.cbGodina.Name = "cbGodina";
            this.cbGodina.Size = new System.Drawing.Size(142, 21);
            this.cbGodina.TabIndex = 2;
            // 
            // frmPromjenaGodine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(196, 134);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPromjenaGodine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPotvrdaZaNovuGodinu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPromjenaGodine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbGodina;
    }
}