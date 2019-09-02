namespace RESORT.Fiskalizacija
{
	partial class frmPregledNefiskaliziranog
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
			this.txtBrojRacuna = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPoslano = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGreška = new System.Windows.Forms.RichTextBox();
			this.txtDucan = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtBlagajna = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDatum = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtBrojRacuna
			// 
			this.txtBrojRacuna.Location = new System.Drawing.Point(13, 39);
			this.txtBrojRacuna.Name = "txtBrojRacuna";
			this.txtBrojRacuna.Size = new System.Drawing.Size(204, 20);
			this.txtBrojRacuna.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Broj računa";
			// 
			// txtPoslano
			// 
			this.txtPoslano.Location = new System.Drawing.Point(12, 205);
			this.txtPoslano.Name = "txtPoslano";
			this.txtPoslano.Size = new System.Drawing.Size(719, 123);
			this.txtPoslano.TabIndex = 2;
			this.txtPoslano.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 190);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Poslano";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 341);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Dobivena greška";
			// 
			// txtGreška
			// 
			this.txtGreška.Location = new System.Drawing.Point(12, 357);
			this.txtGreška.Name = "txtGreška";
			this.txtGreška.Size = new System.Drawing.Size(719, 123);
			this.txtGreška.TabIndex = 2;
			this.txtGreška.Text = "";
			// 
			// txtDucan
			// 
			this.txtDucan.Location = new System.Drawing.Point(13, 79);
			this.txtDucan.Name = "txtDucan";
			this.txtDucan.Size = new System.Drawing.Size(204, 20);
			this.txtDucan.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Dučan";
			// 
			// txtBlagajna
			// 
			this.txtBlagajna.Location = new System.Drawing.Point(13, 120);
			this.txtBlagajna.Name = "txtBlagajna";
			this.txtBlagajna.Size = new System.Drawing.Size(204, 20);
			this.txtBlagajna.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Blagajna";
			// 
			// txtDatum
			// 
			this.txtDatum.Location = new System.Drawing.Point(12, 160);
			this.txtDatum.Name = "txtDatum";
			this.txtDatum.Size = new System.Drawing.Size(204, 20);
			this.txtDatum.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Datum";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 493);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(110, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Spremi";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// frmPregledNefiskaliziranog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 530);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtGreška);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtPoslano);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDatum);
			this.Controls.Add(this.txtBlagajna);
			this.Controls.Add(this.txtDucan);
			this.Controls.Add(this.txtBrojRacuna);
			this.Name = "frmPregledNefiskaliziranog";
			this.Text = "Pregled ne fiskaliziranog";
			this.Load += new System.EventHandler(this.frmPregledNefiskaliziranog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtBrojRacuna;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox txtPoslano;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox txtGreška;
		private System.Windows.Forms.TextBox txtDucan;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtBlagajna;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtDatum;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
	}
}