namespace PCPOS
{
    partial class IzlazniRacuni
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
            this.cmbDokument = new System.Windows.Forms.ComboBox();
            this.chbSamoPouzecem = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDoRacuna = new System.Windows.Forms.TextBox();
            this.tbOdRacuna = new System.Windows.Forms.TextBox();
            this.lblKalkulacija = new System.Windows.Forms.Label();
            this.lblDoRacuna = new System.Windows.Forms.Label();
            this.lblOdRacuna = new System.Windows.Forms.Label();
            this.cbBrojevi = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.dtpOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.lblDoDatuma = new System.Windows.Forms.Label();
            this.lblOdDatuma = new System.Windows.Forms.Label();
            this.cbDatum = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.cbSkladiste = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbPArtnerNaziv = new System.Windows.Forms.TextBox();
            this.tbPartner = new System.Windows.Forms.TextBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Odaberite izlaznu listu:";
            // 
            // cmbDokument
            // 
            this.cmbDokument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDokument.FormattingEnabled = true;
            this.cmbDokument.Location = new System.Drawing.Point(171, 9);
            this.cmbDokument.Name = "cmbDokument";
            this.cmbDokument.Size = new System.Drawing.Size(237, 21);
            this.cmbDokument.TabIndex = 1;
            // 
            // chbSamoPouzecem
            // 
            this.chbSamoPouzecem.AutoSize = true;
            this.chbSamoPouzecem.Location = new System.Drawing.Point(448, 13);
            this.chbSamoPouzecem.Name = "chbSamoPouzecem";
            this.chbSamoPouzecem.Size = new System.Drawing.Size(105, 17);
            this.chbSamoPouzecem.TabIndex = 2;
            this.chbSamoPouzecem.Text = "Samo pouzećem";
            this.chbSamoPouzecem.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDoRacuna);
            this.groupBox1.Controls.Add(this.tbOdRacuna);
            this.groupBox1.Controls.Add(this.lblKalkulacija);
            this.groupBox1.Controls.Add(this.lblDoRacuna);
            this.groupBox1.Controls.Add(this.lblOdRacuna);
            this.groupBox1.Controls.Add(this.cbBrojevi);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 91);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "     Brojevi";
            // 
            // tbDoRacuna
            // 
            this.tbDoRacuna.Location = new System.Drawing.Point(109, 49);
            this.tbDoRacuna.Name = "tbDoRacuna";
            this.tbDoRacuna.Size = new System.Drawing.Size(137, 20);
            this.tbDoRacuna.TabIndex = 9;
            // 
            // tbOdRacuna
            // 
            this.tbOdRacuna.Location = new System.Drawing.Point(109, 19);
            this.tbOdRacuna.Name = "tbOdRacuna";
            this.tbOdRacuna.Size = new System.Drawing.Size(137, 20);
            this.tbOdRacuna.TabIndex = 8;
            // 
            // lblKalkulacija
            // 
            this.lblKalkulacija.AutoSize = true;
            this.lblKalkulacija.Location = new System.Drawing.Point(71, 72);
            this.lblKalkulacija.Name = "lblKalkulacija";
            this.lblKalkulacija.Size = new System.Drawing.Size(175, 13);
            this.lblKalkulacija.TabIndex = 7;
            this.lblKalkulacija.Text = "Kod kalkulacija upisujete njihov ID !";
            // 
            // lblDoRacuna
            // 
            this.lblDoRacuna.AutoSize = true;
            this.lblDoRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDoRacuna.Location = new System.Drawing.Point(6, 50);
            this.lblDoRacuna.Name = "lblDoRacuna";
            this.lblDoRacuna.Size = new System.Drawing.Size(73, 16);
            this.lblDoRacuna.TabIndex = 6;
            this.lblDoRacuna.Text = "Do računa:";
            // 
            // lblOdRacuna
            // 
            this.lblOdRacuna.AutoSize = true;
            this.lblOdRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOdRacuna.Location = new System.Drawing.Point(6, 22);
            this.lblOdRacuna.Name = "lblOdRacuna";
            this.lblOdRacuna.Size = new System.Drawing.Size(73, 16);
            this.lblOdRacuna.TabIndex = 5;
            this.lblOdRacuna.Text = "Od računa:";
            // 
            // cbBrojevi
            // 
            this.cbBrojevi.AutoSize = true;
            this.cbBrojevi.Location = new System.Drawing.Point(9, 0);
            this.cbBrojevi.Name = "cbBrojevi";
            this.cbBrojevi.Size = new System.Drawing.Size(15, 14);
            this.cbBrojevi.TabIndex = 4;
            this.cbBrojevi.UseVisualStyleBackColor = true;
            this.cbBrojevi.CheckedChanged += new System.EventHandler(this.cbBrojevi_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpDoDatuma);
            this.groupBox2.Controls.Add(this.dtpOdDatuma);
            this.groupBox2.Controls.Add(this.lblDoDatuma);
            this.groupBox2.Controls.Add(this.lblOdDatuma);
            this.groupBox2.Controls.Add(this.cbDatum);
            this.groupBox2.Location = new System.Drawing.Point(322, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 91);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "     Datum";
            // 
            // dtpDoDatuma
            // 
            this.dtpDoDatuma.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDoDatuma.CustomFormat = "dd/MM/yyyy";
            this.dtpDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDatuma.Location = new System.Drawing.Point(118, 48);
            this.dtpDoDatuma.Name = "dtpDoDatuma";
            this.dtpDoDatuma.Size = new System.Drawing.Size(145, 20);
            this.dtpDoDatuma.TabIndex = 12;
            // 
            // dtpOdDatuma
            // 
            this.dtpOdDatuma.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOdDatuma.CustomFormat = "dd/MM/yyyy";
            this.dtpOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOdDatuma.Location = new System.Drawing.Point(118, 19);
            this.dtpOdDatuma.Name = "dtpOdDatuma";
            this.dtpOdDatuma.Size = new System.Drawing.Size(145, 20);
            this.dtpOdDatuma.TabIndex = 11;
            // 
            // lblDoDatuma
            // 
            this.lblDoDatuma.AutoSize = true;
            this.lblDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDoDatuma.Location = new System.Drawing.Point(6, 50);
            this.lblDoDatuma.Name = "lblDoDatuma";
            this.lblDoDatuma.Size = new System.Drawing.Size(77, 16);
            this.lblDoDatuma.TabIndex = 10;
            this.lblDoDatuma.Text = "Do datuma:";
            // 
            // lblOdDatuma
            // 
            this.lblOdDatuma.AutoSize = true;
            this.lblOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOdDatuma.Location = new System.Drawing.Point(6, 22);
            this.lblOdDatuma.Name = "lblOdDatuma";
            this.lblOdDatuma.Size = new System.Drawing.Size(77, 16);
            this.lblOdDatuma.TabIndex = 10;
            this.lblOdDatuma.Text = "Od datuma:";
            // 
            // cbDatum
            // 
            this.cbDatum.AutoSize = true;
            this.cbDatum.Location = new System.Drawing.Point(9, 0);
            this.cbDatum.Name = "cbDatum";
            this.cbDatum.Size = new System.Drawing.Size(15, 14);
            this.cbDatum.TabIndex = 0;
            this.cbDatum.UseVisualStyleBackColor = true;
            this.cbDatum.CheckedChanged += new System.EventHandler(this.cbDatum_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbSkladiste);
            this.groupBox3.Controls.Add(this.cbSkladiste);
            this.groupBox3.Location = new System.Drawing.Point(12, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(255, 77);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "     Skladište";
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(9, 31);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(237, 21);
            this.cmbSkladiste.TabIndex = 1;
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoSize = true;
            this.cbSkladiste.Location = new System.Drawing.Point(9, 0);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.cbSkladiste.TabIndex = 0;
            this.cbSkladiste.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.tbPArtnerNaziv);
            this.groupBox4.Controls.Add(this.tbPartner);
            this.groupBox4.Location = new System.Drawing.Point(322, 134);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(169, 77);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Partner";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(104, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Traži";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbPArtnerNaziv
            // 
            this.tbPArtnerNaziv.Location = new System.Drawing.Point(9, 45);
            this.tbPArtnerNaziv.Name = "tbPArtnerNaziv";
            this.tbPArtnerNaziv.Size = new System.Drawing.Size(154, 20);
            this.tbPArtnerNaziv.TabIndex = 1;
            // 
            // tbPartner
            // 
            this.tbPartner.Location = new System.Drawing.Point(9, 19);
            this.tbPartner.Name = "tbPartner";
            this.tbPartner.Size = new System.Drawing.Size(89, 20);
            this.tbPartner.TabIndex = 0;
            // 
            // btnIspis
            // 
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspis.Location = new System.Drawing.Point(497, 134);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(94, 77);
            this.btnIspis.TabIndex = 7;
            this.btnIspis.Text = "Ispiši na ekranu";
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // IzlazniRacuni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 223);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chbSamoPouzecem);
            this.Controls.Add(this.cmbDokument);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(619, 262);
            this.MinimumSize = new System.Drawing.Size(619, 262);
            this.Name = "IzlazniRacuni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste izlaznih računa";
            this.Load += new System.EventHandler(this.IzlazniRacuni_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDokument;
        private System.Windows.Forms.CheckBox chbSamoPouzecem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbDoRacuna;
        private System.Windows.Forms.TextBox tbOdRacuna;
        private System.Windows.Forms.Label lblKalkulacija;
        private System.Windows.Forms.Label lblDoRacuna;
        private System.Windows.Forms.Label lblOdRacuna;
        private System.Windows.Forms.CheckBox cbBrojevi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbDatum;
        private System.Windows.Forms.DateTimePicker dtpDoDatuma;
        private System.Windows.Forms.DateTimePicker dtpOdDatuma;
        private System.Windows.Forms.Label lblDoDatuma;
        private System.Windows.Forms.Label lblOdDatuma;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.CheckBox cbSkladiste;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbPArtnerNaziv;
        private System.Windows.Forms.TextBox tbPartner;
        private System.Windows.Forms.Button btnIspis;
    }
}