namespace PCPOS.Sifarnik
{
    partial class frmDodajZemlju
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
            this.drzava = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNovaPoslovnica = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNazivZemlje = new System.Windows.Forms.TextBox();
            this.txtSkraceniNaziv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.drzava,
            this.oznaka,
            this.aktivnost,
            this.id});
            this.dgv.Location = new System.Drawing.Point(12, 158);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(366, 350);
            this.dgv.TabIndex = 100;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // drzava
            // 
            this.drzava.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.drzava.HeaderText = "Država";
            this.drzava.Name = "drzava";
            this.drzava.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drzava.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // oznaka
            // 
            this.oznaka.HeaderText = "Oznaka";
            this.oznaka.Name = "oznaka";
            // 
            // aktivnost
            // 
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
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
            this.btnNovaPoslovnica.Location = new System.Drawing.Point(208, 123);
            this.btnNovaPoslovnica.Name = "btnNovaPoslovnica";
            this.btnNovaPoslovnica.Size = new System.Drawing.Size(175, 29);
            this.btnNovaPoslovnica.TabIndex = 99;
            this.btnNovaPoslovnica.TabStop = false;
            this.btnNovaPoslovnica.Text = "Dodaj novu državu";
            this.btnNovaPoslovnica.UseVisualStyleBackColor = false;
            this.btnNovaPoslovnica.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(11, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 22);
            this.label7.TabIndex = 98;
            this.label7.Text = "Unos države:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(9, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Naziv države:";
            // 
            // txtNazivZemlje
            // 
            this.txtNazivZemlje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNazivZemlje.Location = new System.Drawing.Point(130, 51);
            this.txtNazivZemlje.MaxLength = 13;
            this.txtNazivZemlje.Name = "txtNazivZemlje";
            this.txtNazivZemlje.Size = new System.Drawing.Size(248, 22);
            this.txtNazivZemlje.TabIndex = 96;
            // 
            // txtSkraceniNaziv
            // 
            this.txtSkraceniNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSkraceniNaziv.Location = new System.Drawing.Point(130, 75);
            this.txtSkraceniNaziv.MaxLength = 13;
            this.txtSkraceniNaziv.Name = "txtSkraceniNaziv";
            this.txtSkraceniNaziv.Size = new System.Drawing.Size(248, 22);
            this.txtSkraceniNaziv.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "Skračeni naziv:";
            // 
            // frmDodajZemlju
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(395, 524);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNovaPoslovnica);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSkraceniNaziv);
            this.Controls.Add(this.txtNazivZemlje);
            this.Name = "frmDodajZemlju";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poslovnice";
            this.Load += new System.EventHandler(this.frmDucani_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnNovaPoslovnica;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNazivZemlje;
        private System.Windows.Forms.DataGridViewTextBoxColumn drzava;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaka;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.TextBox txtSkraceniNaziv;
        private System.Windows.Forms.Label label1;

    }
}