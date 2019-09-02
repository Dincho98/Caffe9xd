namespace PCPOS.Caffe
{
	partial class frmSveSmjene
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.b_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pocetak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zavrsetak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zavrsno_stanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.b_min,
            this.pocetak,
            this.zavrsetak,
            this.zavrsno_stanje,
            this.napomena,
            this.id});
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgv.Location = new System.Drawing.Point(13, 12);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(819, 569);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // b_min
            // 
            this.b_min.HeaderText = "Blag.Minimum";
            this.b_min.Name = "b_min";
            this.b_min.ReadOnly = true;
            // 
            // pocetak
            // 
            this.pocetak.HeaderText = "Početak smjene";
            this.pocetak.Name = "pocetak";
            this.pocetak.ReadOnly = true;
            // 
            // zavrsetak
            // 
            this.zavrsetak.HeaderText = "Završetak smjene";
            this.zavrsetak.Name = "zavrsetak";
            this.zavrsetak.ReadOnly = true;
            // 
            // zavrsno_stanje
            // 
            this.zavrsno_stanje.HeaderText = "Završno stanje";
            this.zavrsno_stanje.Name = "zavrsno_stanje";
            this.zavrsno_stanje.ReadOnly = true;
            // 
            // napomena
            // 
            this.napomena.HeaderText = "Napomena";
            this.napomena.Name = "napomena";
            this.napomena.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 587);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Završno stanje je stanje bez pologa";
            // 
            // frmSveSmjene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(844, 614);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv);
            this.Name = "frmSveSmjene";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sve smjene";
            this.Load += new System.EventHandler(this.frmSveSmjene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn b_min;
		private System.Windows.Forms.DataGridViewTextBoxColumn pocetak;
		private System.Windows.Forms.DataGridViewTextBoxColumn zavrsetak;
		private System.Windows.Forms.DataGridViewTextBoxColumn zavrsno_stanje;
		private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
	}
}