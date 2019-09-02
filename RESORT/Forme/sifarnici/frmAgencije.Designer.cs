namespace RESORT.Forme.sifarnici
{
	partial class frmAgencije
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgencije));
			this.dgv = new System.Windows.Forms.DataGridView();
			this.ime_agencije = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnNoviUnos = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtNazivAgencije = new System.Windows.Forms.TextBox();
			this.txtNapomena = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.BackgroundColor = System.Drawing.Color.White;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ime_agencije,
            this.napomena,
            this.aktivnost,
            this.id});
			this.dgv.Location = new System.Drawing.Point(12, 133);
			this.dgv.Name = "dgv";
			this.dgv.RowHeadersVisible = false;
			this.dgv.Size = new System.Drawing.Size(528, 378);
			this.dgv.TabIndex = 120;
			this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
			this.dgv.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
			// 
			// ime_agencije
			// 
			this.ime_agencije.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ime_agencije.HeaderText = "Ime agencije";
			this.ime_agencije.Name = "ime_agencije";
			// 
			// napomena
			// 
			this.napomena.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.napomena.HeaderText = "Napomena";
			this.napomena.Name = "napomena";
			// 
			// aktivnost
			// 
			this.aktivnost.HeaderText = "Aktivnost";
			this.aktivnost.Name = "aktivnost";
			this.aktivnost.Width = 70;
			// 
			// id
			// 
			this.id.HeaderText = "id";
			this.id.Name = "id";
			this.id.Visible = false;
			// 
			// btnNoviUnos
			// 
			this.btnNoviUnos.BackColor = System.Drawing.Color.Transparent;
			this.btnNoviUnos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNoviUnos.BackgroundImage")));
			this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
			this.btnNoviUnos.FlatAppearance.BorderSize = 0;
			this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNoviUnos.Font = new System.Drawing.Font("Arial Narrow", 12F);
			this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.btnNoviUnos.Location = new System.Drawing.Point(369, 92);
			this.btnNoviUnos.Name = "btnNoviUnos";
			this.btnNoviUnos.Size = new System.Drawing.Size(175, 29);
			this.btnNoviUnos.TabIndex = 119;
			this.btnNoviUnos.TabStop = false;
			this.btnNoviUnos.Text = "Dodaj novu agenciju";
			this.btnNoviUnos.UseVisualStyleBackColor = false;
			this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(9, 11);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(97, 22);
			this.label7.TabIndex = 118;
			this.label7.Text = "Agencije:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label4.Location = new System.Drawing.Point(11, 49);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 117;
			this.label4.Text = "Naziv agencije:";
			// 
			// txtNazivAgencije
			// 
			this.txtNazivAgencije.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtNazivAgencije.Location = new System.Drawing.Point(113, 46);
			this.txtNazivAgencije.MaxLength = 100;
			this.txtNazivAgencije.Name = "txtNazivAgencije";
			this.txtNazivAgencije.Size = new System.Drawing.Size(217, 22);
			this.txtNazivAgencije.TabIndex = 114;
			// 
			// txtNapomena
			// 
			this.txtNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtNapomena.Location = new System.Drawing.Point(113, 69);
			this.txtNapomena.MaxLength = 1000;
			this.txtNapomena.Multiline = true;
			this.txtNapomena.Name = "txtNapomena";
			this.txtNapomena.Size = new System.Drawing.Size(217, 52);
			this.txtNapomena.TabIndex = 123;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label3.Location = new System.Drawing.Point(11, 72);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 16);
			this.label3.TabIndex = 124;
			this.label3.Text = "Napomena:";
			// 
			// frmAgencije
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(553, 525);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtNapomena);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.btnNoviUnos);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtNazivAgencije);
			this.Name = "frmAgencije";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Agencije";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAgencije_FormClosing);
			this.Load += new System.EventHandler(this.frmAgencije_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Button btnNoviUnos;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtNazivAgencije;
		private System.Windows.Forms.TextBox txtNapomena;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridViewTextBoxColumn ime_agencije;
		private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
		private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
	}
}