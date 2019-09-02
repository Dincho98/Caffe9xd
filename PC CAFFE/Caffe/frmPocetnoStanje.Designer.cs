namespace PCPOS.Caffe
{
	partial class frmPocetnoStanje
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
            this.lblZaposlenik = new System.Windows.Forms.Label();
            this.txtPocetno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnZapocniSmjenu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblProdanoGotovina = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblZaposlenik
            // 
            this.lblZaposlenik.AutoSize = true;
            this.lblZaposlenik.BackColor = System.Drawing.Color.Transparent;
            this.lblZaposlenik.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblZaposlenik.Location = new System.Drawing.Point(12, 202);
            this.lblZaposlenik.Name = "lblZaposlenik";
            this.lblZaposlenik.Size = new System.Drawing.Size(146, 14);
            this.lblZaposlenik.TabIndex = 69;
            this.lblZaposlenik.Text = "Prijavljen: Drazen Vuk";
            this.lblZaposlenik.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPocetno
            // 
            this.txtPocetno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPocetno.Location = new System.Drawing.Point(154, 87);
            this.txtPocetno.MaxLength = 50;
            this.txtPocetno.Name = "txtPocetno";
            this.txtPocetno.Size = new System.Drawing.Size(197, 23);
            this.txtPocetno.TabIndex = 62;
            this.txtPocetno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPocetno_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(101, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 14);
            this.label1.TabIndex = 68;
            this.label1.Text = "Polog:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(354, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 14);
            this.label3.TabIndex = 68;
            this.label3.Text = "kn";
            // 
            // btnZapocniSmjenu
            // 
            this.btnZapocniSmjenu.BackColor = System.Drawing.Color.White;
            this.btnZapocniSmjenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZapocniSmjenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZapocniSmjenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnZapocniSmjenu.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnZapocniSmjenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnZapocniSmjenu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnZapocniSmjenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZapocniSmjenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnZapocniSmjenu.ForeColor = System.Drawing.Color.Black;
            this.btnZapocniSmjenu.Location = new System.Drawing.Point(206, 142);
            this.btnZapocniSmjenu.Name = "btnZapocniSmjenu";
            this.btnZapocniSmjenu.Size = new System.Drawing.Size(191, 56);
            this.btnZapocniSmjenu.TabIndex = 70;
            this.btnZapocniSmjenu.Text = "Započni smjenu\r\nENTER";
            this.btnZapocniSmjenu.UseVisualStyleBackColor = false;
            this.btnZapocniSmjenu.Click += new System.EventHandler(this.btnZapocniSmjenu_Click);
            this.btnZapocniSmjenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPocetno_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(12, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 56);
            this.button1.TabIndex = 70;
            this.button1.Text = "Ne želim započeti smjenu\r\nESC\r\n";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPocetno_KeyDown);
            // 
            // lblProdanoGotovina
            // 
            this.lblProdanoGotovina.AutoSize = true;
            this.lblProdanoGotovina.BackColor = System.Drawing.Color.Transparent;
            this.lblProdanoGotovina.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lblProdanoGotovina.Location = new System.Drawing.Point(12, 23);
            this.lblProdanoGotovina.Name = "lblProdanoGotovina";
            this.lblProdanoGotovina.Size = new System.Drawing.Size(194, 17);
            this.lblProdanoGotovina.TabIndex = 132;
            this.lblProdanoGotovina.Text = "Početak smjene i polog:";
            // 
            // frmPocetnoStanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(414, 228);
            this.Controls.Add(this.lblProdanoGotovina);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnZapocniSmjenu);
            this.Controls.Add(this.lblZaposlenik);
            this.Controls.Add(this.txtPocetno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPocetnoStanje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Početak smijene";
            this.Load += new System.EventHandler(this.frmPocetnoStanje_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblZaposlenik;
		private System.Windows.Forms.TextBox txtPocetno;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnZapocniSmjenu;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label lblProdanoGotovina;
	}
}