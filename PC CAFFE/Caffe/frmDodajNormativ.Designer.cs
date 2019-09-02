namespace PCPOS.Caffe
{
    partial class frmDodajNormativ
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
            this.components = new System.ComponentModel.Container();
            this.dgwR = new System.Windows.Forms.DataGridView();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnDodajNoviArtikl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwR
            // 
            this.dgwR.AllowUserToAddRows = false;
            this.dgwR.AllowUserToDeleteRows = false;
            this.dgwR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwR.BackgroundColor = System.Drawing.Color.White;
            this.dgwR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgwR.Location = new System.Drawing.Point(12, 99);
            this.dgwR.Name = "dgwR";
            this.dgwR.ReadOnly = true;
            this.dgwR.RowHeadersVisible = false;
            this.dgwR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwR.Size = new System.Drawing.Size(482, 457);
            this.dgwR.TabIndex = 18;
            this.toolTip1.SetToolTip(this.dgwR, "Odaberi normativ.");
            this.dgwR.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwR_CellClick);
            this.dgwR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(68, 36);
            this.txtNaziv.MaxLength = 50;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(151, 23);
            this.txtNaziv.TabIndex = 62;
            this.txtNaziv.TextChanged += new System.EventHandler(this.txtNaziv_TextChanged);
            this.txtNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(13, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 14);
            this.label4.TabIndex = 65;
            this.label4.Text = "Naziv:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbGrupa
            // 
            this.cbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Location = new System.Drawing.Point(240, 33);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(163, 24);
            this.cbGrupa.TabIndex = 63;
            this.cbGrupa.SelectedIndexChanged += new System.EventHandler(this.cbGrupa_SelectedIndexChanged);
            this.cbGrupa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(237, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 64;
            this.label2.Text = "Grupa:";
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(68, 12);
            this.txtSifra.MaxLength = 50;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(151, 23);
            this.txtSifra.TabIndex = 66;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 14);
            this.label1.TabIndex = 67;
            this.label1.Text = "Šifra:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(13, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 14);
            this.label3.TabIndex = 68;
            this.label3.Text = "Odaberi normativ:";
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIzlaz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIzlaz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzlaz.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnIzlaz.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnIzlaz.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIzlaz.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIzlaz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzlaz.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.btnIzlaz.ForeColor = System.Drawing.Color.White;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(422, 7);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(73, 52);
            this.btnIzlaz.TabIndex = 69;
            this.btnIzlaz.Text = "Odustani\r\nESC";
            this.btnIzlaz.UseVisualStyleBackColor = false;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnDodajNoviArtikl
            // 
            this.btnDodajNoviArtikl.BackColor = System.Drawing.Color.White;
            this.btnDodajNoviArtikl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajNoviArtikl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajNoviArtikl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajNoviArtikl.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNoviArtikl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNoviArtikl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajNoviArtikl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajNoviArtikl.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnDodajNoviArtikl.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDodajNoviArtikl.Location = new System.Drawing.Point(357, 561);
            this.btnDodajNoviArtikl.Name = "btnDodajNoviArtikl";
            this.btnDodajNoviArtikl.Size = new System.Drawing.Size(140, 27);
            this.btnDodajNoviArtikl.TabIndex = 70;
            this.btnDodajNoviArtikl.Text = "Dodaj novi normativ";
            this.btnDodajNoviArtikl.UseVisualStyleBackColor = false;
            this.btnDodajNoviArtikl.Click += new System.EventHandler(this.btnDodajNoviArtikl_Click);
            // 
            // frmDodajNormativ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(506, 592);
            this.Controls.Add(this.btnDodajNoviArtikl);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbGrupa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgwR);
            this.Name = "frmDodajNormativ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj normativ";
            this.Load += new System.EventHandler(this.frmDodajNormativ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwR;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnDodajNoviArtikl;
    }
}