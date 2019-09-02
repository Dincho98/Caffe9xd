namespace RESORT.Forme
{
    partial class frmPopisSoba
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
            this.broj_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_lezaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_nocenja = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.broj_sobe,
            this.naziv_sobe,
            this.broj_lezaja,
            this.cijena_nocenja,
            this.id});
            this.dgw.Location = new System.Drawing.Point(12, 12);
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersVisible = false;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(439, 493);
            this.dgw.TabIndex = 0;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // broj_sobe
            // 
            this.broj_sobe.FillWeight = 70F;
            this.broj_sobe.HeaderText = "Br.sobe";
            this.broj_sobe.Name = "broj_sobe";
            this.broj_sobe.ReadOnly = true;
            this.broj_sobe.Width = 70;
            // 
            // naziv_sobe
            // 
            this.naziv_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv_sobe.HeaderText = "Naziv sobe";
            this.naziv_sobe.Name = "naziv_sobe";
            this.naziv_sobe.ReadOnly = true;
            // 
            // broj_lezaja
            // 
            this.broj_lezaja.HeaderText = "Broj ležaja";
            this.broj_lezaja.Name = "broj_lezaja";
            this.broj_lezaja.ReadOnly = true;
            // 
            // cijena_nocenja
            // 
            this.cijena_nocenja.HeaderText = "Cijena nočenja";
            this.cijena_nocenja.Name = "cijena_nocenja";
            this.cijena_nocenja.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // frmPopisSoba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 517);
            this.Controls.Add(this.dgw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPopisSoba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis soba";
            this.Load += new System.EventHandler(this.frmPopisSoba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_lezaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_nocenja;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}