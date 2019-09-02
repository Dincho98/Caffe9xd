namespace RESORT.Forme.sifarnici
{
    partial class frmValute

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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.dgvSk = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Skraceni_naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puni_naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tecaj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paritet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtParitet = new System.Windows.Forms.TextBox();
            this.txtSkraceniNaziv = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPuniNaziv = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSk)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Naziv:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNaziv.Location = new System.Drawing.Point(107, 64);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(211, 23);
            this.txtNaziv.TabIndex = 2;
            // 
            // btnNovo
            // 
            this.btnNovo.Location = new System.Drawing.Point(438, 86);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(146, 28);
            this.btnNovo.TabIndex = 5;
            this.btnNovo.Text = "Dodaj novu valutu";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(373, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tečaj:";
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIznos.Location = new System.Drawing.Point(419, 8);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(165, 23);
            this.txtIznos.TabIndex = 2;
            // 
            // dgvSk
            // 
            this.dgvSk.AllowUserToAddRows = false;
            this.dgvSk.AllowUserToDeleteRows = false;
            this.dgvSk.BackgroundColor = System.Drawing.Color.White;
            this.dgvSk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.Skraceni_naziv,
            this.naziv,
            this.puni_naziv,
            this.tecaj,
            this.paritet,
            this.id_valuta});
            this.dgvSk.Location = new System.Drawing.Point(15, 135);
            this.dgvSk.Name = "dgvSk";
            this.dgvSk.Size = new System.Drawing.Size(648, 275);
            this.dgvSk.TabIndex = 6;
            this.dgvSk.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSk_CellEndEdit);
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // Skraceni_naziv
            // 
            this.Skraceni_naziv.HeaderText = "Skraćeni naziv";
            this.Skraceni_naziv.Name = "Skraceni_naziv";
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // puni_naziv
            // 
            this.puni_naziv.HeaderText = "Puni naziv";
            this.puni_naziv.Name = "puni_naziv";
            // 
            // tecaj
            // 
            this.tecaj.HeaderText = "Tečaj";
            this.tecaj.Name = "tecaj";
            // 
            // paritet
            // 
            this.paritet.HeaderText = "Paritet";
            this.paritet.Name = "paritet";
            // 
            // id_valuta
            // 
            this.id_valuta.HeaderText = "id_valuta";
            this.id_valuta.Name = "id_valuta";
            this.id_valuta.Visible = false;
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra.Location = new System.Drawing.Point(107, 8);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(211, 23);
            this.txtSifra.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Šifra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Skraćeni naziv:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(373, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Paritet:";
            // 
            // txtParitet
            // 
            this.txtParitet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtParitet.Location = new System.Drawing.Point(419, 36);
            this.txtParitet.Name = "txtParitet";
            this.txtParitet.Size = new System.Drawing.Size(165, 23);
            this.txtParitet.TabIndex = 11;
            // 
            // txtSkraceniNaziv
            // 
            this.txtSkraceniNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSkraceniNaziv.Location = new System.Drawing.Point(107, 36);
            this.txtSkraceniNaziv.Name = "txtSkraceniNaziv";
            this.txtSkraceniNaziv.Size = new System.Drawing.Size(211, 23);
            this.txtSkraceniNaziv.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Puni naziv:";
            // 
            // txtPuniNaziv
            // 
            this.txtPuniNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPuniNaziv.Location = new System.Drawing.Point(107, 91);
            this.txtPuniNaziv.Name = "txtPuniNaziv";
            this.txtPuniNaziv.Size = new System.Drawing.Size(211, 23);
            this.txtPuniNaziv.TabIndex = 14;
            // 
            // frmValute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 422);
            this.Controls.Add(this.txtPuniNaziv);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSkraceniNaziv);
            this.Controls.Add(this.txtParitet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.dgvSk);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.txtIznos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label1);
            this.Name = "frmValute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Valute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStopePoreza_FormClosing);
            this.Load += new System.EventHandler(this.frmValute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIznos;
		private System.Windows.Forms.DataGridView dgvSk;
		private System.Windows.Forms.TextBox txtSifra;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtParitet;
		private System.Windows.Forms.TextBox txtSkraceniNaziv;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtPuniNaziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
		private System.Windows.Forms.DataGridViewTextBoxColumn Skraceni_naziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn puni_naziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn tecaj;
		private System.Windows.Forms.DataGridViewTextBoxColumn paritet;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_valuta;
    }
}