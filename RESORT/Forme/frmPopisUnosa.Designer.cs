namespace RESORT.Forme
{
    partial class frmPopisUnosa
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
            this.dgw = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ime_prezime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dolazak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odlazak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.ime_prezime,
            this.avans,
            this.Dolazak,
            this.Odlazak,
            this.ukupno,
            this.id});
            this.dgw.Location = new System.Drawing.Point(12, 12);
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersVisible = false;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(748, 578);
            this.dgw.TabIndex = 0;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // broj
            // 
            this.broj.FillWeight = 70F;
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            this.broj.Width = 70;
            // 
            // ime_prezime
            // 
            this.ime_prezime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ime_prezime.HeaderText = "Ime prezime";
            this.ime_prezime.Name = "ime_prezime";
            this.ime_prezime.ReadOnly = true;
            // 
            // avans
            // 
            this.avans.HeaderText = "Avans";
            this.avans.Name = "avans";
            this.avans.ReadOnly = true;
            // 
            // Dolazak
            // 
            this.Dolazak.HeaderText = "Dolazak";
            this.Dolazak.Name = "Dolazak";
            this.Dolazak.ReadOnly = true;
            // 
            // Odlazak
            // 
            this.Odlazak.HeaderText = "Odlazak";
            this.Odlazak.Name = "Odlazak";
            this.Odlazak.ReadOnly = true;
            // 
            // ukupno
            // 
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // frmPopisUnosa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 602);
            this.Controls.Add(this.dgw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPopisUnosa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis unosa";
            this.Load += new System.EventHandler(this.frmPopisSoba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime_prezime;
        private System.Windows.Forms.DataGridViewTextBoxColumn avans;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dolazak;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odlazak;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}