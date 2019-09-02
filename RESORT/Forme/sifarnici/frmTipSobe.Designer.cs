namespace RESORT.Forme.sifarnici
{
	partial class frmTipSobe
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipSobe));
			this.dgv = new System.Windows.Forms.DataGridView();
			this.tip_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnNoviUnos = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtTipSobe = new System.Windows.Forms.TextBox();
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
            this.tip_sobe,
            this.aktivnost,
            this.id});
			this.dgv.Location = new System.Drawing.Point(16, 86);
			this.dgv.Name = "dgv";
			this.dgv.RowHeadersVisible = false;
			this.dgv.Size = new System.Drawing.Size(528, 425);
			this.dgv.TabIndex = 129;
			this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
			// 
			// tip_sobe
			// 
			this.tip_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.tip_sobe.HeaderText = "Tip sobe";
			this.tip_sobe.Name = "tip_sobe";
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
			this.btnNoviUnos.Location = new System.Drawing.Point(369, 39);
			this.btnNoviUnos.Name = "btnNoviUnos";
			this.btnNoviUnos.Size = new System.Drawing.Size(175, 29);
			this.btnNoviUnos.TabIndex = 128;
			this.btnNoviUnos.TabStop = false;
			this.btnNoviUnos.Text = "Dodaj novi tip sobe";
			this.btnNoviUnos.UseVisualStyleBackColor = false;
			this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(13, 11);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 22);
			this.label7.TabIndex = 127;
			this.label7.Text = "Tip sobe:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label4.Location = new System.Drawing.Point(15, 49);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 16);
			this.label4.TabIndex = 126;
			this.label4.Text = "Tip sobe:";
			// 
			// txtTipSobe
			// 
			this.txtTipSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtTipSobe.Location = new System.Drawing.Point(117, 46);
			this.txtTipSobe.MaxLength = 40;
			this.txtTipSobe.Name = "txtTipSobe";
			this.txtTipSobe.Size = new System.Drawing.Size(217, 22);
			this.txtTipSobe.TabIndex = 125;
			// 
			// frmTipSobe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 527);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.btnNoviUnos);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtTipSobe);
			this.Name = "frmTipSobe";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tip sobe";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTipSobe_FormClosing);
			this.Load += new System.EventHandler(this.frmTipSobe_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Button btnNoviUnos;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtTipSobe;
		private System.Windows.Forms.DataGridViewTextBoxColumn tip_sobe;
		private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
	}
}