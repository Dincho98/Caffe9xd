namespace PCPOS.Until
{
    partial class frmPorezUpozorenje
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnPromijena = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNeSmetaj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dolaskom 2014. godine došlo je do promjena stopi PDV-a.\r\nŽelite il sada promijeni" +
    "ti poreze?";
            // 
            // btnPromijena
            // 
            this.btnPromijena.BackColor = System.Drawing.Color.White;
            this.btnPromijena.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPromijena.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromijena.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromijena.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPromijena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromijena.Location = new System.Drawing.Point(12, 48);
            this.btnPromijena.Name = "btnPromijena";
            this.btnPromijena.Size = new System.Drawing.Size(100, 23);
            this.btnPromijena.TabIndex = 1;
            this.btnPromijena.Text = "Promijeni";
            this.btnPromijena.UseVisualStyleBackColor = false;
            this.btnPromijena.Click += new System.EventHandler(this.btnPromijena_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(148, 48);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Kasnije";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNeSmetaj
            // 
            this.btnNeSmetaj.BackColor = System.Drawing.Color.White;
            this.btnNeSmetaj.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNeSmetaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNeSmetaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNeSmetaj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNeSmetaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNeSmetaj.Location = new System.Drawing.Point(280, 48);
            this.btnNeSmetaj.Name = "btnNeSmetaj";
            this.btnNeSmetaj.Size = new System.Drawing.Size(100, 23);
            this.btnNeSmetaj.TabIndex = 3;
            this.btnNeSmetaj.Text = "Ne upozoravaj";
            this.btnNeSmetaj.UseVisualStyleBackColor = false;
            this.btnNeSmetaj.Click += new System.EventHandler(this.btnNeSmetaj_Click);
            // 
            // frmPorezUpozorenje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(392, 83);
            this.Controls.Add(this.btnNeSmetaj);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPromijena);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmPorezUpozorenje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upozorenje!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPromijena;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNeSmetaj;
    }
}