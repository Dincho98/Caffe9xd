namespace RESORT.Forme.sifarnici
{
    partial class frmCijene_soba
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
            this.txtBrojSobe = new System.Windows.Forms.TextBox();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSoba = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnObrisiOznacenog = new System.Windows.Forms.Button();
            this.btnDodajNaPopis = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.broj_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuta = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cijena_nocenja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.od_datuma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.do_datuma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCijenaSobe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbValute = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPromjeniTecaj = new System.Windows.Forms.Button();
            this.txtTecaj = new System.Windows.Forms.TextBox();
            this.txtPreracunKune = new System.Windows.Forms.TextBox();
            this.btnSviUnosi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBrojSobe
            // 
            this.txtBrojSobe.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojSobe.Location = new System.Drawing.Point(67, 77);
            this.txtBrojSobe.Name = "txtBrojSobe";
            this.txtBrojSobe.Size = new System.Drawing.Size(49, 24);
            this.txtBrojSobe.TabIndex = 33;
            // 
            // dtpOD
            // 
            this.dtpOD.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpOD.CustomFormat = "dd.MM.yyyy";
            this.dtpOD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(212, 129);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(176, 24);
            this.dtpOD.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(14, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Cijena vrijedi od datuma:";
            // 
            // cbSoba
            // 
            this.cbSoba.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSoba.FormattingEnabled = true;
            this.cbSoba.Location = new System.Drawing.Point(119, 77);
            this.cbSoba.Name = "cbSoba";
            this.cbSoba.Size = new System.Drawing.Size(269, 24);
            this.cbSoba.TabIndex = 34;
            this.cbSoba.SelectedIndexChanged += new System.EventHandler(this.cbSoba_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.Location = new System.Drawing.Point(13, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 36;
            this.label11.Text = "Soba:";
            // 
            // btnObrisiOznacenog
            // 
            this.btnObrisiOznacenog.Location = new System.Drawing.Point(180, 229);
            this.btnObrisiOznacenog.Name = "btnObrisiOznacenog";
            this.btnObrisiOznacenog.Size = new System.Drawing.Size(157, 26);
            this.btnObrisiOznacenog.TabIndex = 38;
            this.btnObrisiOznacenog.Text = "Obriši označenog";
            this.btnObrisiOznacenog.UseVisualStyleBackColor = true;
            this.btnObrisiOznacenog.Click += new System.EventHandler(this.btnObrisiOznacenog_Click);
            // 
            // btnDodajNaPopis
            // 
            this.btnDodajNaPopis.Location = new System.Drawing.Point(18, 229);
            this.btnDodajNaPopis.Name = "btnDodajNaPopis";
            this.btnDodajNaPopis.Size = new System.Drawing.Size(157, 26);
            this.btnDodajNaPopis.TabIndex = 37;
            this.btnDodajNaPopis.Text = "Dodaj na popis";
            this.btnDodajNaPopis.UseVisualStyleBackColor = true;
            this.btnDodajNaPopis.Click += new System.EventHandler(this.btnDodajNaPopis_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj_sobe,
            this.naziv_sobe,
            this.valuta,
            this.cijena_nocenja,
            this.od_datuma,
            this.do_datuma,
            this.id_soba,
            this.id});
            this.dgv.Location = new System.Drawing.Point(19, 263);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(858, 297);
            this.dgv.TabIndex = 106;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // broj_sobe
            // 
            this.broj_sobe.FillWeight = 80F;
            this.broj_sobe.HeaderText = "Broj sobe";
            this.broj_sobe.Name = "broj_sobe";
            this.broj_sobe.ReadOnly = true;
            this.broj_sobe.Width = 80;
            // 
            // naziv_sobe
            // 
            this.naziv_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv_sobe.HeaderText = "Naziv sobe";
            this.naziv_sobe.Name = "naziv_sobe";
            this.naziv_sobe.ReadOnly = true;
            // 
            // valuta
            // 
            this.valuta.FillWeight = 180F;
            this.valuta.HeaderText = "Valuta";
            this.valuta.Name = "valuta";
            this.valuta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.valuta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.valuta.Width = 180;
            // 
            // cijena_nocenja
            // 
            this.cijena_nocenja.HeaderText = "Cijena noćenja";
            this.cijena_nocenja.Name = "cijena_nocenja";
            // 
            // od_datuma
            // 
            this.od_datuma.FillWeight = 140F;
            this.od_datuma.HeaderText = "Od datuma";
            this.od_datuma.Name = "od_datuma";
            this.od_datuma.Width = 140;
            // 
            // do_datuma
            // 
            this.do_datuma.FillWeight = 140F;
            this.do_datuma.HeaderText = "Do datuma";
            this.do_datuma.Name = "do_datuma";
            this.do_datuma.Width = 140;
            // 
            // id_soba
            // 
            this.id_soba.HeaderText = "id_soba";
            this.id_soba.Name = "id_soba";
            this.id_soba.Visible = false;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // txtCijenaSobe
            // 
            this.txtCijenaSobe.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCijenaSobe.Location = new System.Drawing.Point(119, 103);
            this.txtCijenaSobe.Name = "txtCijenaSobe";
            this.txtCijenaSobe.Size = new System.Drawing.Size(90, 24);
            this.txtCijenaSobe.TabIndex = 107;
            this.txtCijenaSobe.TextChanged += new System.EventHandler(this.txtCijenaSobe_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(14, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 108;
            this.label3.Text = "Cijena sobe:";
            // 
            // cbValute
            // 
            this.cbValute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbValute.FormattingEnabled = true;
            this.cbValute.Location = new System.Drawing.Point(212, 103);
            this.cbValute.Name = "cbValute";
            this.cbValute.Size = new System.Drawing.Size(176, 24);
            this.cbValute.TabIndex = 109;
            this.cbValute.SelectedIndexChanged += new System.EventHandler(this.cbValute_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(14, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "Cijena vrijedi do datuma:";
            // 
            // dtpDO
            // 
            this.dtpDO.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDO.CustomFormat = "dd.MM.yyyy";
            this.dtpDO.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(212, 155);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(176, 24);
            this.dtpDO.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 26);
            this.label1.TabIndex = 110;
            this.label1.Text = "Postava cijene po sobama:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(457, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 111;
            this.label5.Text = "Tečaj:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(457, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 111;
            this.label6.Text = "Ukupno u kunama:";
            // 
            // btnPromjeniTecaj
            // 
            this.btnPromjeniTecaj.Location = new System.Drawing.Point(639, 75);
            this.btnPromjeniTecaj.Name = "btnPromjeniTecaj";
            this.btnPromjeniTecaj.Size = new System.Drawing.Size(87, 26);
            this.btnPromjeniTecaj.TabIndex = 112;
            this.btnPromjeniTecaj.Text = "Promjeni tečaj";
            this.btnPromjeniTecaj.UseVisualStyleBackColor = true;
            this.btnPromjeniTecaj.Click += new System.EventHandler(this.btnPromjeniTecaj_Click);
            // 
            // txtTecaj
            // 
            this.txtTecaj.Location = new System.Drawing.Point(509, 77);
            this.txtTecaj.Name = "txtTecaj";
            this.txtTecaj.ReadOnly = true;
            this.txtTecaj.Size = new System.Drawing.Size(124, 20);
            this.txtTecaj.TabIndex = 113;
            // 
            // txtPreracunKune
            // 
            this.txtPreracunKune.Location = new System.Drawing.Point(589, 103);
            this.txtPreracunKune.Name = "txtPreracunKune";
            this.txtPreracunKune.ReadOnly = true;
            this.txtPreracunKune.Size = new System.Drawing.Size(137, 20);
            this.txtPreracunKune.TabIndex = 113;
            // 
            // btnSviUnosi
            // 
            this.btnSviUnosi.ForeColor = System.Drawing.Color.Red;
            this.btnSviUnosi.Location = new System.Drawing.Point(671, 229);
            this.btnSviUnosi.Name = "btnSviUnosi";
            this.btnSviUnosi.Size = new System.Drawing.Size(206, 26);
            this.btnSviUnosi.TabIndex = 38;
            this.btnSviUnosi.Text = "Postavi navedenu cijenu za sve sobe";
            this.btnSviUnosi.UseVisualStyleBackColor = true;
            this.btnSviUnosi.Click += new System.EventHandler(this.btnSviUnosi_Click);
            // 
            // frmCijene_soba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 567);
            this.Controls.Add(this.txtPreracunKune);
            this.Controls.Add(this.txtTecaj);
            this.Controls.Add(this.btnPromjeniTecaj);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbValute);
            this.Controls.Add(this.txtCijenaSobe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnSviUnosi);
            this.Controls.Add(this.btnObrisiOznacenog);
            this.Controls.Add(this.btnDodajNaPopis);
            this.Controls.Add(this.txtBrojSobe);
            this.Controls.Add(this.dtpDO);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpOD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSoba);
            this.Controls.Add(this.label11);
            this.Name = "frmCijene_soba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cijene soba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCijene_soba_FormClosing);
            this.Load += new System.EventHandler(this.frmCijene_soba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBrojSobe;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSoba;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnObrisiOznacenog;
        private System.Windows.Forms.Button btnDodajNaPopis;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtCijenaSobe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbValute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPromjeniTecaj;
        private System.Windows.Forms.TextBox txtTecaj;
        private System.Windows.Forms.TextBox txtPreracunKune;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_sobe;
        private System.Windows.Forms.DataGridViewComboBoxColumn valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_nocenja;
        private System.Windows.Forms.DataGridViewTextBoxColumn od_datuma;
        private System.Windows.Forms.DataGridViewTextBoxColumn do_datuma;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Button btnSviUnosi;
    }
}