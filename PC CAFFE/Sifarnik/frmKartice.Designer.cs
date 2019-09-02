namespace PCPOS.Sifarnik {
    partial class frmKartice {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.dgwKartice = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDodajKarticu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwKartice)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwKartice
            // 
            this.dgwKartice.AllowUserToAddRows = false;
            this.dgwKartice.AllowUserToDeleteRows = false;
            this.dgwKartice.AllowUserToResizeRows = false;
            this.dgwKartice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwKartice.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgwKartice.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwKartice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwKartice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.naziv,
            this.aktivnost});
            this.dgwKartice.GridColor = System.Drawing.SystemColors.Window;
            this.dgwKartice.Location = new System.Drawing.Point(13, 119);
            this.dgwKartice.Name = "dgwKartice";
            this.dgwKartice.RowHeadersWidth = 6;
            this.dgwKartice.Size = new System.Drawing.Size(562, 482);
            this.dgwKartice.TabIndex = 0;
            this.dgwKartice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwKartice_CellClick);
            this.dgwKartice.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwKartice_CellEndEdit);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.DataPropertyName = "naziv";
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            // 
            // aktivnost
            // 
            this.aktivnost.DataPropertyName = "aktivnost";
            this.aktivnost.HeaderText = "Aktivna";
            this.aktivnost.Name = "aktivnost";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtNaziv.Location = new System.Drawing.Point(12, 54);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(314, 32);
            this.txtNaziv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Naziv kartice:";
            // 
            // btnDodajKarticu
            // 
            this.btnDodajKarticu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodajKarticu.BackColor = System.Drawing.Color.White;
            this.btnDodajKarticu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajKarticu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajKarticu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajKarticu.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnDodajKarticu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajKarticu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajKarticu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajKarticu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDodajKarticu.Location = new System.Drawing.Point(433, 9);
            this.btnDodajKarticu.Name = "btnDodajKarticu";
            this.btnDodajKarticu.Size = new System.Drawing.Size(142, 77);
            this.btnDodajKarticu.TabIndex = 18;
            this.btnDodajKarticu.Text = "Dodaj karticu";
            this.btnDodajKarticu.UseVisualStyleBackColor = false;
            this.btnDodajKarticu.Click += new System.EventHandler(this.btnDodajKarticu_Click);
            // 
            // frmKartice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(587, 613);
            this.Controls.Add(this.btnDodajKarticu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.dgwKartice);
            this.MaximumSize = new System.Drawing.Size(603, 651);
            this.MinimumSize = new System.Drawing.Size(603, 651);
            this.Name = "frmKartice";
            this.Text = "Kartice";
            this.Load += new System.EventHandler(this.frmKartice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwKartice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwKartice;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDodajKarticu;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktivnost;
    }
}