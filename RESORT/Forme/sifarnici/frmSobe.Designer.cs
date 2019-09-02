namespace RESORT.Forme.sifarnici
{
	partial class frmSobe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSobe));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.naziv_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_lezaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tip_sobe = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cijena_nocenja = new System.Windows.Forms.DataGridViewButtonColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_tip_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNazivSobe = new System.Windows.Forms.TextBox();
            this.txtBrojSobe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojLezaja = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTipSoba = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCijenaNocenja = new System.Windows.Forms.TextBox();
            this.txtNapomena = new System.Windows.Forms.RichTextBox();
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
            this.naziv_sobe,
            this.broj_sobe,
            this.broj_lezaja,
            this.tip_sobe,
            this.cijena_nocenja,
            this.napomena,
            this.aktivnost,
            this.id_tip_sobe,
            this.id});
            this.dgv.Location = new System.Drawing.Point(12, 153);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(908, 357);
            this.dgv.TabIndex = 14;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gw_CellClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // naziv_sobe
            // 
            this.naziv_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv_sobe.HeaderText = "Naziv sobe";
            this.naziv_sobe.Name = "naziv_sobe";
            // 
            // broj_sobe
            // 
            this.broj_sobe.HeaderText = "Broj sobe";
            this.broj_sobe.Name = "broj_sobe";
            // 
            // broj_lezaja
            // 
            this.broj_lezaja.HeaderText = "Broj ležaja";
            this.broj_lezaja.Name = "broj_lezaja";
            // 
            // tip_sobe
            // 
            this.tip_sobe.FillWeight = 180F;
            this.tip_sobe.HeaderText = "Tip sobe";
            this.tip_sobe.Name = "tip_sobe";
            this.tip_sobe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tip_sobe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.tip_sobe.Width = 180;
            // 
            // cijena_nocenja
            // 
            this.cijena_nocenja.HeaderText = "Cijena noćenja";
            this.cijena_nocenja.Name = "cijena_nocenja";
            this.cijena_nocenja.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cijena_nocenja.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // id_tip_sobe
            // 
            this.id_tip_sobe.HeaderText = "id_tip_sobe";
            this.id_tip_sobe.Name = "id_tip_sobe";
            this.id_tip_sobe.ReadOnly = true;
            this.id_tip_sobe.Visible = false;
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
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(745, 100);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(175, 29);
            this.btnNoviUnos.TabIndex = 13;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novu sobu";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(9, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sobe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(11, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Naziv sobe:";
            // 
            // txtNazivSobe
            // 
            this.txtNazivSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNazivSobe.Location = new System.Drawing.Point(97, 47);
            this.txtNazivSobe.MaxLength = 50;
            this.txtNazivSobe.Name = "txtNazivSobe";
            this.txtNazivSobe.Size = new System.Drawing.Size(217, 22);
            this.txtNazivSobe.TabIndex = 2;
            // 
            // txtBrojSobe
            // 
            this.txtBrojSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojSobe.Location = new System.Drawing.Point(97, 70);
            this.txtBrojSobe.MaxLength = 13;
            this.txtBrojSobe.Name = "txtBrojSobe";
            this.txtBrojSobe.Size = new System.Drawing.Size(217, 22);
            this.txtBrojSobe.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(11, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Broj sobe:";
            // 
            // txtBrojLezaja
            // 
            this.txtBrojLezaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojLezaja.Location = new System.Drawing.Point(97, 93);
            this.txtBrojLezaja.MaxLength = 13;
            this.txtBrojLezaja.Name = "txtBrojLezaja";
            this.txtBrojLezaja.Size = new System.Drawing.Size(217, 22);
            this.txtBrojLezaja.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(11, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Broj ležaja:";
            // 
            // cbTipSoba
            // 
            this.cbTipSoba.FormattingEnabled = true;
            this.cbTipSoba.Location = new System.Drawing.Point(437, 45);
            this.cbTipSoba.Name = "cbTipSoba";
            this.cbTipSoba.Size = new System.Drawing.Size(217, 21);
            this.cbTipSoba.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label10.Location = new System.Drawing.Point(335, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Tip sobe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(335, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Napomena:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(335, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Cijena noćenja:";
            this.label5.Visible = false;
            // 
            // txtCijenaNocenja
            // 
            this.txtCijenaNocenja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCijenaNocenja.Location = new System.Drawing.Point(437, 107);
            this.txtCijenaNocenja.MaxLength = 13;
            this.txtCijenaNocenja.Name = "txtCijenaNocenja";
            this.txtCijenaNocenja.Size = new System.Drawing.Size(217, 22);
            this.txtCijenaNocenja.TabIndex = 12;
            this.txtCijenaNocenja.Visible = false;
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(437, 67);
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(218, 39);
            this.txtNapomena.TabIndex = 10;
            this.txtNapomena.Text = "";
            // 
            // frmSobe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 526);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCijenaNocenja);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbTipSoba);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBrojLezaja);
            this.Controls.Add(this.txtBrojSobe);
            this.Controls.Add(this.txtNazivSobe);
            this.Name = "frmSobe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSobe_FormClosing);
            this.Load += new System.EventHandler(this.frmSobe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Button btnNoviUnos;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtNazivSobe;
		private System.Windows.Forms.TextBox txtBrojSobe;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBrojLezaja;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbTipSoba;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCijenaNocenja;
        private System.Windows.Forms.RichTextBox txtNapomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_lezaja;
        private System.Windows.Forms.DataGridViewComboBoxColumn tip_sobe;
        private System.Windows.Forms.DataGridViewButtonColumn cijena_nocenja;
        private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tip_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
	}
}