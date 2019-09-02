namespace PCPOS.Robno
{
    partial class frmPostaviStanjePremaZadnjojInventuri
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPostavi = new System.Windows.Forms.Button();
            this.btnZadnjaInv = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolicinaNaSk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDate = new System.Windows.Forms.Label();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.nuGodina = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPostavi
            // 
            this.btnPostavi.BackColor = System.Drawing.Color.White;
            this.btnPostavi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPostavi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostavi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPostavi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPostavi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPostavi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPostavi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostavi.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.btnPostavi.ForeColor = System.Drawing.Color.Red;
            this.btnPostavi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPostavi.Location = new System.Drawing.Point(415, 556);
            this.btnPostavi.Name = "btnPostavi";
            this.btnPostavi.Size = new System.Drawing.Size(342, 33);
            this.btnPostavi.TabIndex = 73;
            this.btnPostavi.TabStop = false;
            this.btnPostavi.Text = "Postavi početno stanje po zadnjoj inventuri";
            this.btnPostavi.UseVisualStyleBackColor = false;
            this.btnPostavi.Click += new System.EventHandler(this.btnPostavi_Click);
            // 
            // btnZadnjaInv
            // 
            this.btnZadnjaInv.BackColor = System.Drawing.Color.White;
            this.btnZadnjaInv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZadnjaInv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZadnjaInv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnZadnjaInv.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnZadnjaInv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnZadnjaInv.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnZadnjaInv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZadnjaInv.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnZadnjaInv.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnZadnjaInv.Location = new System.Drawing.Point(402, 27);
            this.btnZadnjaInv.Name = "btnZadnjaInv";
            this.btnZadnjaInv.Size = new System.Drawing.Size(164, 29);
            this.btnZadnjaInv.TabIndex = 72;
            this.btnZadnjaInv.TabStop = false;
            this.btnZadnjaInv.Text = "Učitaj zadnju inventuru";
            this.btnZadnjaInv.UseVisualStyleBackColor = false;
            this.btnZadnjaInv.Click += new System.EventHandler(this.btnZadnjaInv_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label17.Location = new System.Drawing.Point(12, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 17);
            this.label17.TabIndex = 71;
            this.label17.Text = "Skladište:";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(15, 29);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(207, 24);
            this.cbSkladiste.TabIndex = 70;
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.sifra,
            this.naziv,
            this.jmj,
            this.kolicina,
            this.KolicinaNaSk,
            this.id_stavka,
            this.id_skladiste,
            this.cijena});
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 87);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(745, 463);
            this.dgw.TabIndex = 69;
            // 
            // br
            // 
            this.br.FillWeight = 30F;
            this.br.HeaderText = "Broj";
            this.br.Name = "br";
            // 
            // sifra
            // 
            this.sifra.FillWeight = 95.02538F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.FillWeight = 95.02538F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            // 
            // jmj
            // 
            this.jmj.FillWeight = 95.02538F;
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 95.02538F;
            this.kolicina.HeaderText = "Inv.količina";
            this.kolicina.Name = "kolicina";
            // 
            // KolicinaNaSk
            // 
            this.KolicinaNaSk.FillWeight = 95.02538F;
            this.KolicinaNaSk.HeaderText = "KolicinaNaSk";
            this.KolicinaNaSk.Name = "KolicinaNaSk";
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // id_skladiste
            // 
            this.id_skladiste.HeaderText = "id_skladiste";
            this.id_skladiste.Name = "id_skladiste";
            this.id_skladiste.Visible = false;
            // 
            // cijena
            // 
            this.cijena.HeaderText = "cijena";
            this.cijena.Name = "cijena";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 592);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 13);
            this.lblDate.TabIndex = 74;
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            this.bgSinkronizacija.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSinkronizacija_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(246, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 75;
            this.label1.Text = "Godina Inventure:";
            // 
            // nuGodina
            // 
            this.nuGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nuGodina.Location = new System.Drawing.Point(246, 30);
            this.nuGodina.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.nuGodina.Minimum = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.nuGodina.Name = "nuGodina";
            this.nuGodina.Size = new System.Drawing.Size(84, 23);
            this.nuGodina.TabIndex = 76;
            this.nuGodina.Value = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.nuGodina);
            this.panel1.Controls.Add(this.cbSkladiste);
            this.panel1.Controls.Add(this.btnZadnjaInv);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 69);
            this.panel1.TabIndex = 77;
            // 
            // frmPostaviStanjePremaZadnjojInventuri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(769, 612);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnPostavi);
            this.Controls.Add(this.dgw);
            this.Name = "frmPostaviStanjePremaZadnjojInventuri";
            this.Text = "Postavi stanje prema zadnjoj inventuri";
            this.Load += new System.EventHandler(this.frmPostaviStanjePremaZadnjojInventuri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPostavi;
        private System.Windows.Forms.Button btnZadnjaInv;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DataGridView dgw;
		private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolicinaNaSk;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuGodina;
        private System.Windows.Forms.Panel panel1;
    }
}