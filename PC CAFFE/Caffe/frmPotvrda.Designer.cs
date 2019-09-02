namespace PCPOS.Caffe
{
	partial class frmPotvrda
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
            this.btnZavrsi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnZavrsi
            // 
            this.btnZavrsi.BackColor = System.Drawing.Color.White;
            this.btnZavrsi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZavrsi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZavrsi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnZavrsi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnZavrsi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnZavrsi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnZavrsi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZavrsi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnZavrsi.ForeColor = System.Drawing.Color.Black;
            this.btnZavrsi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnZavrsi.Location = new System.Drawing.Point(252, 89);
            this.btnZavrsi.Name = "btnZavrsi";
            this.btnZavrsi.Size = new System.Drawing.Size(195, 69);
            this.btnZavrsi.TabIndex = 124;
            this.btnZavrsi.Text = "Završi račun\r\nENTER";
            this.btnZavrsi.UseVisualStyleBackColor = false;
            this.btnZavrsi.Click += new System.EventHandler(this.btnZavrsi_Click);
            this.btnZavrsi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOdustani_KeyDown);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnOdustani.ForeColor = System.Drawing.Color.Black;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOdustani.Location = new System.Drawing.Point(18, 89);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(179, 69);
            this.btnOdustani.TabIndex = 125;
            this.btnOdustani.Text = "Odustani\r\nESC";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            this.btnOdustani.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOdustani_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(407, 26);
            this.label1.TabIndex = 126;
            this.label1.Text = "Dali ste sigurni da želite završiti račun?";
            // 
            // frmPotvrda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(459, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnZavrsi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPotvrda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPotvrda";
            this.Load += new System.EventHandler(this.frmPotvrda_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOdustani_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Button btnZavrsi;
		public System.Windows.Forms.Button btnOdustani;
		private System.Windows.Forms.Label label1;
	}
}