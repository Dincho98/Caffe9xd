namespace RESORT.Forme
{
    partial class frmOdaberiSobuZaNaplatiti
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
            this.gost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odaberi = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNaslov = new System.Windows.Forms.Label();
            this.btnIzradiRaccun = new System.Windows.Forms.Button();
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
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gost,
            this.soba,
            this.odaberi,
            this.id,
            this.id_soba});
            this.dgv.Location = new System.Drawing.Point(12, 81);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(465, 228);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // gost
            // 
            this.gost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gost.HeaderText = "Gost";
            this.gost.Name = "gost";
            this.gost.ReadOnly = true;
            // 
            // soba
            // 
            this.soba.FillWeight = 150F;
            this.soba.HeaderText = "Soba";
            this.soba.Name = "soba";
            this.soba.ReadOnly = true;
            this.soba.Width = 150;
            // 
            // odaberi
            // 
            this.odaberi.FillWeight = 70F;
            this.odaberi.HeaderText = "Odaberi";
            this.odaberi.Name = "odaberi";
            this.odaberi.ReadOnly = true;
            this.odaberi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.odaberi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.odaberi.Width = 70;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // id_soba
            // 
            this.id_soba.HeaderText = "id_soba";
            this.id_soba.Name = "id_soba";
            this.id_soba.ReadOnly = true;
            this.id_soba.Visible = false;
            // 
            // lblNaslov
            // 
            this.lblNaslov.AutoSize = true;
            this.lblNaslov.BackColor = System.Drawing.Color.Transparent;
            this.lblNaslov.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblNaslov.ForeColor = System.Drawing.Color.Black;
            this.lblNaslov.Location = new System.Drawing.Point(12, 16);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(167, 40);
            this.lblNaslov.TabIndex = 136;
            this.lblNaslov.Text = "Odaberite goste koje \r\nželite prebaciti u račun";
            // 
            // btnIzradiRaccun
            // 
            this.btnIzradiRaccun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzradiRaccun.BackColor = System.Drawing.Color.Transparent;
            this.btnIzradiRaccun.BackgroundImage = global::RESORT.Properties.Resources.backGroung;
            this.btnIzradiRaccun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIzradiRaccun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzradiRaccun.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIzradiRaccun.FlatAppearance.BorderSize = 0;
            this.btnIzradiRaccun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIzradiRaccun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIzradiRaccun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIzradiRaccun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzradiRaccun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnIzradiRaccun.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIzradiRaccun.Location = new System.Drawing.Point(325, 315);
            this.btnIzradiRaccun.Name = "btnIzradiRaccun";
            this.btnIzradiRaccun.Size = new System.Drawing.Size(152, 37);
            this.btnIzradiRaccun.TabIndex = 137;
            this.btnIzradiRaccun.Text = "Izradi račun";
            this.btnIzradiRaccun.UseVisualStyleBackColor = false;
            this.btnIzradiRaccun.Click += new System.EventHandler(this.btnIzradiRaccun_Click);
            // 
            // frmOdaberiSobuZaNaplatiti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 364);
            this.Controls.Add(this.btnIzradiRaccun);
            this.Controls.Add(this.lblNaslov);
            this.Controls.Add(this.dgv);
            this.Name = "frmOdaberiSobuZaNaplatiti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Odaberi sobu za naplatiti";
            this.Load += new System.EventHandler(this.frmOdaberiSobuZaNaplatiti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.Button btnIzradiRaccun;
        private System.Windows.Forms.DataGridViewTextBoxColumn gost;
        private System.Windows.Forms.DataGridViewTextBoxColumn soba;
        private System.Windows.Forms.DataGridViewCheckBoxColumn odaberi;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_soba;
    }
}