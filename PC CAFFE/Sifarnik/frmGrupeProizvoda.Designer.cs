namespace PCPOS.Sifarnik
{
    partial class frmGrupeProizvoda
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
            this.cbpodgrupa = new System.Windows.Forms.ComboBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.grupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_podgrupa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dodatak = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.polpol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_grupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnazivGrupe = new System.Windows.Forms.TextBox();
            this.chkIsDodatak = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbpodgrupa
            // 
            this.cbpodgrupa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbpodgrupa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbpodgrupa.BackColor = System.Drawing.Color.White;
            this.cbpodgrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbpodgrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbpodgrupa.FormattingEnabled = true;
            this.cbpodgrupa.Location = new System.Drawing.Point(109, 39);
            this.cbpodgrupa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbpodgrupa.Name = "cbpodgrupa";
            this.cbpodgrupa.Size = new System.Drawing.Size(170, 24);
            this.cbpodgrupa.TabIndex = 4;
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
            this.grupa,
            this.id_podgrupa,
            this.aktivnost,
            this.dodatak,
            this.polpol,
            this.id_grupa});
            this.dgv.Location = new System.Drawing.Point(12, 143);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(563, 458);
            this.dgv.TabIndex = 0;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            // 
            // grupa
            // 
            this.grupa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grupa.HeaderText = "Grupa";
            this.grupa.Name = "grupa";
            // 
            // id_podgrupa
            // 
            this.id_podgrupa.HeaderText = "Podgrupa";
            this.id_podgrupa.Name = "id_podgrupa";
            this.id_podgrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.id_podgrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // aktivnost
            // 
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            // 
            // dodatak
            // 
            this.dodatak.HeaderText = "Dodatak";
            this.dodatak.Name = "dodatak";
            // 
            // polpol
            // 
            this.polpol.HeaderText = "50:50";
            this.polpol.Name = "polpol";
            // 
            // id_grupa
            // 
            this.id_grupa.HeaderText = "id_grupa";
            this.id_grupa.Name = "id_grupa";
            this.id_grupa.ReadOnly = true;
            this.id_grupa.Visible = false;
            // 
            // btnNoviUnos
            // 
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
            this.btnNoviUnos.Location = new System.Drawing.Point(296, 49);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(134, 54);
            this.btnNoviUnos.TabIndex = 7;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novu grupu";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(10, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Grupe proizvoda:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(30, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pogdrupa:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(19, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Naziv grupe:";
            // 
            // txtnazivGrupe
            // 
            this.txtnazivGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivGrupe.Location = new System.Drawing.Point(109, 11);
            this.txtnazivGrupe.MaxLength = 50;
            this.txtnazivGrupe.Name = "txtnazivGrupe";
            this.txtnazivGrupe.Size = new System.Drawing.Size(170, 22);
            this.txtnazivGrupe.TabIndex = 2;
            // 
            // chkIsDodatak
            // 
            this.chkIsDodatak.AutoSize = true;
            this.chkIsDodatak.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsDodatak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkIsDodatak.Location = new System.Drawing.Point(21, 67);
            this.chkIsDodatak.Name = "chkIsDodatak";
            this.chkIsDodatak.Size = new System.Drawing.Size(104, 20);
            this.chkIsDodatak.TabIndex = 6;
            this.chkIsDodatak.Text = "Je dodatak:  ";
            this.chkIsDodatak.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtnazivGrupe);
            this.panel1.Controls.Add(this.chkIsDodatak);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbpodgrupa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 100);
            this.panel1.TabIndex = 8;
            // 
            // frmGrupeProizvoda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(587, 613);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Name = "frmGrupeProizvoda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupe proizvoda";
            this.Load += new System.EventHandler(this.frmGrupeProizvoda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbpodgrupa;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnazivGrupe;
        private System.Windows.Forms.CheckBox chkIsDodatak;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupa;
        private System.Windows.Forms.DataGridViewComboBoxColumn id_podgrupa;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dodatak;
        private System.Windows.Forms.DataGridViewCheckBoxColumn polpol;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grupa;
        private System.Windows.Forms.Panel panel1;
    }
}