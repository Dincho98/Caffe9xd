namespace RESORT.Forme
{
    partial class frmPopis_cijena_po_datumima
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
            this.soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.soba,
            this.broj_sobe,
            this.datum_od,
            this.datum_do,
            this.cijena,
            this.valuta});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(744, 229);
            this.dgv.TabIndex = 0;
            // 
            // soba
            // 
            this.soba.HeaderText = "Soba";
            this.soba.Name = "soba";
            this.soba.ReadOnly = true;
            // 
            // broj_sobe
            // 
            this.broj_sobe.HeaderText = "Broj sobe";
            this.broj_sobe.Name = "broj_sobe";
            this.broj_sobe.ReadOnly = true;
            // 
            // datum_od
            // 
            this.datum_od.HeaderText = "Datum od";
            this.datum_od.Name = "datum_od";
            this.datum_od.ReadOnly = true;
            // 
            // datum_do
            // 
            this.datum_do.HeaderText = "Datum do";
            this.datum_do.Name = "datum_do";
            this.datum_do.ReadOnly = true;
            // 
            // cijena
            // 
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            // 
            // valuta
            // 
            this.valuta.HeaderText = "Valuta";
            this.valuta.Name = "valuta";
            this.valuta.ReadOnly = true;
            // 
            // frmPopis_cijena_po_datumima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 229);
            this.Controls.Add(this.dgv);
            this.Name = "frmPopis_cijena_po_datumima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis cijena po datumima";
            this.Load += new System.EventHandler(this.frmPopis_cijena_po_datumima_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_od;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_do;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn valuta;
    }
}