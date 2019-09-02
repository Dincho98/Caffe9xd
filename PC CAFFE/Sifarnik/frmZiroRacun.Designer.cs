namespace PCPOS.Sifarnik
{
    partial class frmZiroRacun
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
            this.banka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziroracun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_ziroracun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDodajIban = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNazivBanke = new System.Windows.Forms.TextBox();
            this.txtBrojRacuna = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.banka,
            this.ziroracun,
            this.aktivnost,
            this.id_ziroracun});
            this.dgv.Location = new System.Drawing.Point(12, 147);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(470, 365);
            this.dgv.TabIndex = 100;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // banka
            // 
            this.banka.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.banka.HeaderText = "Ime banke";
            this.banka.Name = "banka";
            this.banka.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.banka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ziroracun
            // 
            this.ziroracun.FillWeight = 180F;
            this.ziroracun.HeaderText = "Broj računa";
            this.ziroracun.Name = "ziroracun";
            this.ziroracun.Width = 180;
            // 
            // aktivnost
            // 
            this.aktivnost.FillWeight = 70F;
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            this.aktivnost.Width = 70;
            // 
            // id_ziroracun
            // 
            this.id_ziroracun.HeaderText = "id_ziroracun";
            this.id_ziroracun.Name = "id_ziroracun";
            this.id_ziroracun.ReadOnly = true;
            this.id_ziroracun.Visible = false;
            // 
            // btnDodajIban
            // 
            this.btnDodajIban.BackColor = System.Drawing.Color.White;
            this.btnDodajIban.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajIban.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajIban.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajIban.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDodajIban.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajIban.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajIban.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajIban.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnDodajIban.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDodajIban.Location = new System.Drawing.Point(307, 112);
            this.btnDodajIban.Name = "btnDodajIban";
            this.btnDodajIban.Size = new System.Drawing.Size(175, 29);
            this.btnDodajIban.TabIndex = 99;
            this.btnDodajIban.TabStop = false;
            this.btnDodajIban.Text = "Dodaj novi IBAN";
            this.btnDodajIban.UseVisualStyleBackColor = false;
            this.btnDodajIban.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(11, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 22);
            this.label7.TabIndex = 98;
            this.label7.Text = "IBAN:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Naziv banke:";
            // 
            // txtNazivBanke
            // 
            this.txtNazivBanke.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNazivBanke.Location = new System.Drawing.Point(221, 12);
            this.txtNazivBanke.MaxLength = 13;
            this.txtNazivBanke.Name = "txtNazivBanke";
            this.txtNazivBanke.Size = new System.Drawing.Size(248, 22);
            this.txtNazivBanke.TabIndex = 96;
            // 
            // txtBrojRacuna
            // 
            this.txtBrojRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojRacuna.Location = new System.Drawing.Point(221, 37);
            this.txtBrojRacuna.MaxLength = 13;
            this.txtBrojRacuna.Name = "txtBrojRacuna";
            this.txtBrojRacuna.Size = new System.Drawing.Size(248, 22);
            this.txtBrojRacuna.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(4, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "Broj računa:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtBrojRacuna);
            this.panel1.Controls.Add(this.txtNazivBanke);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 73);
            this.panel1.TabIndex = 101;
            // 
            // frmZiroRacun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(494, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnDodajIban);
            this.Controls.Add(this.label7);
            this.MaximumSize = new System.Drawing.Size(510, 562);
            this.MinimumSize = new System.Drawing.Size(510, 562);
            this.Name = "frmZiroRacun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IBAN";
            this.Load += new System.EventHandler(this.frmDucani_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnDodajIban;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNazivBanke;
        private System.Windows.Forms.TextBox txtBrojRacuna;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn banka;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziroracun;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_ziroracun;
        private System.Windows.Forms.Panel panel1;
    }
}