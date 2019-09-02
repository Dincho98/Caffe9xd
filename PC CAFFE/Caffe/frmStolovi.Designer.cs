namespace PCPOS.Caffe
{
    partial class frmStolovi
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
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnazivStola = new System.Windows.Forms.TextBox();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poslovnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_stol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbPoslovnica = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(13, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 22);
            this.label7.TabIndex = 85;
            this.label7.Text = "Stolovi:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(22, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 82;
            this.label4.Text = "Naziv stola:";
            // 
            // txtnazivStola
            // 
            this.txtnazivStola.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivStola.Location = new System.Drawing.Point(101, 15);
            this.txtnazivStola.MaxLength = 13;
            this.txtnazivStola.Name = "txtnazivStola";
            this.txtnazivStola.Size = new System.Drawing.Size(190, 22);
            this.txtnazivStola.TabIndex = 81;
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(327, 50);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(94, 54);
            this.btnNoviUnos.TabIndex = 87;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novi stol";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.Silver;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.naziv,
            this.poslovnica,
            this.aktivnost,
            this.id_stol});
            this.dgv.Location = new System.Drawing.Point(11, 127);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(409, 382);
            this.dgv.TabIndex = 88;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.HeaderText = "Naziv stola";
            this.naziv.Name = "naziv";
            // 
            // poslovnica
            // 
            this.poslovnica.HeaderText = "Poslovnica";
            this.poslovnica.Name = "poslovnica";
            this.poslovnica.ReadOnly = true;
            this.poslovnica.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.poslovnica.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // aktivnost
            // 
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            // 
            // id_stol
            // 
            this.id_stol.HeaderText = "id_stol";
            this.id_stol.Name = "id_stol";
            this.id_stol.ReadOnly = true;
            this.id_stol.Visible = false;
            // 
            // cbPoslovnica
            // 
            this.cbPoslovnica.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbPoslovnica.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPoslovnica.BackColor = System.Drawing.Color.White;
            this.cbPoslovnica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoslovnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbPoslovnica.FormattingEnabled = true;
            this.cbPoslovnica.Location = new System.Drawing.Point(101, 43);
            this.cbPoslovnica.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbPoslovnica.Name = "cbPoslovnica";
            this.cbPoslovnica.Size = new System.Drawing.Size(190, 24);
            this.cbPoslovnica.TabIndex = 89;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 82;
            this.label1.Text = "Polovnica:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtnazivStola);
            this.panel1.Controls.Add(this.cbPoslovnica);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-4, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 85);
            this.panel1.TabIndex = 90;
            // 
            // frmStolovi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(429, 521);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Name = "frmStolovi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stolovi";
            this.Load += new System.EventHandler(this.frmStolovi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnazivStola;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn poslovnica;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stol;
        private System.Windows.Forms.ComboBox cbPoslovnica;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}