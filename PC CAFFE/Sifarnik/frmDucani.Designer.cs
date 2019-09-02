namespace PCPOS.Sifarnik
{
    partial class frmDucani
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
            this.poslovnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_poslovnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNovaPoslovnica = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnazivStola = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.poslovnica,
            this.aktivnost,
            this.id_poslovnica});
            this.dgv.Location = new System.Drawing.Point(12, 119);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(371, 393);
            this.dgv.TabIndex = 100;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // poslovnica
            // 
            this.poslovnica.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.poslovnica.HeaderText = "Poslovnica";
            this.poslovnica.Name = "poslovnica";
            this.poslovnica.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.poslovnica.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // aktivnost
            // 
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            // 
            // id_poslovnica
            // 
            this.id_poslovnica.HeaderText = "id_poslovnica";
            this.id_poslovnica.Name = "id_poslovnica";
            this.id_poslovnica.ReadOnly = true;
            this.id_poslovnica.Visible = false;
            // 
            // btnNovaPoslovnica
            // 
            this.btnNovaPoslovnica.BackColor = System.Drawing.Color.White;
            this.btnNovaPoslovnica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNovaPoslovnica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaPoslovnica.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNovaPoslovnica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNovaPoslovnica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNovaPoslovnica.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNovaPoslovnica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaPoslovnica.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnNovaPoslovnica.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNovaPoslovnica.Location = new System.Drawing.Point(208, 84);
            this.btnNovaPoslovnica.Name = "btnNovaPoslovnica";
            this.btnNovaPoslovnica.Size = new System.Drawing.Size(175, 29);
            this.btnNovaPoslovnica.TabIndex = 99;
            this.btnNovaPoslovnica.TabStop = false;
            this.btnNovaPoslovnica.Text = "Dodaj novu poslovnicu";
            this.btnNovaPoslovnica.UseVisualStyleBackColor = false;
            this.btnNovaPoslovnica.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(13, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 22);
            this.label7.TabIndex = 98;
            this.label7.Text = "Poslovnice:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Naziv poslovnice:";
            // 
            // txtnazivStola
            // 
            this.txtnazivStola.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivStola.Location = new System.Drawing.Point(123, 9);
            this.txtnazivStola.MaxLength = 13;
            this.txtnazivStola.Name = "txtnazivStola";
            this.txtnazivStola.Size = new System.Drawing.Size(248, 22);
            this.txtnazivStola.TabIndex = 96;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtnazivStola);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 41);
            this.panel1.TabIndex = 101;
            // 
            // frmDucani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(395, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNovaPoslovnica);
            this.Controls.Add(this.label7);
            this.MaximumSize = new System.Drawing.Size(411, 562);
            this.MinimumSize = new System.Drawing.Size(411, 562);
            this.Name = "frmDucani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poslovnice";
            this.Load += new System.EventHandler(this.frmDucani_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn poslovnica;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_poslovnica;
        private System.Windows.Forms.Button btnNovaPoslovnica;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnazivStola;
        private System.Windows.Forms.Panel panel1;
    }
}