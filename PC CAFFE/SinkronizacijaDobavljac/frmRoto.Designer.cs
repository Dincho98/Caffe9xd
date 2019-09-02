namespace PCPOS.SinkronizacijaDobavljac {
    partial class frmRoto {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvArtikli = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznosPDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvMaping = new System.Windows.Forms.DataGridView();
            this.mGrupa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.mNasaGrupa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojUlaznogDokumenta = new System.Windows.Forms.TextBox();
            this.datumDokumenta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNazivPoslovnice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdresaPoslovnice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMjestoPoslovnice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUcitajDatoteku = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.lblBezPdv = new System.Windows.Forms.Label();
            this.lblSaPdv = new System.Windows.Forms.Label();
            this.lblPorez = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikli)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaping)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 394);
            this.tabControl1.TabIndex = 77;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvArtikli);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(927, 368);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Artikli";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvArtikli
            // 
            this.dgvArtikli.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvArtikli.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvArtikli.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArtikli.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArtikli.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvArtikli.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvArtikli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArtikli.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.jmj,
            this.kolicina,
            this.cijena,
            this.rabat,
            this.porez,
            this.iznos,
            this.iznosPDV});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvArtikli.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvArtikli.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvArtikli.Location = new System.Drawing.Point(3, 3);
            this.dgvArtikli.Name = "dgvArtikli";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvArtikli.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvArtikli.Size = new System.Drawing.Size(921, 365);
            this.dgvArtikli.TabIndex = 1;
            this.dgvArtikli.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArtikli_CellEndEdit);
            this.dgvArtikli.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvArtikli_UserDeletedRow);
            // 
            // sifra
            // 
            this.sifra.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sifra.FillWeight = 120F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Width = 120;
            // 
            // naziv
            // 
            this.naziv.FillWeight = 50.87719F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // jmj
            // 
            this.jmj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.kolicina.DefaultCellStyle = dataGridViewCellStyle3;
            this.kolicina.FillWeight = 60F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            this.kolicina.Width = 60;
            // 
            // cijena
            // 
            this.cijena.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cijena.DefaultCellStyle = dataGridViewCellStyle4;
            this.cijena.FillWeight = 80F;
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.Width = 80;
            // 
            // rabat
            // 
            this.rabat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.rabat.DefaultCellStyle = dataGridViewCellStyle5;
            this.rabat.FillWeight = 60F;
            this.rabat.HeaderText = "Rabat";
            this.rabat.Name = "rabat";
            this.rabat.ReadOnly = true;
            this.rabat.Width = 60;
            // 
            // porez
            // 
            this.porez.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.porez.DefaultCellStyle = dataGridViewCellStyle6;
            this.porez.FillWeight = 70F;
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            this.porez.Width = 70;
            // 
            // iznos
            // 
            this.iznos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iznos.DefaultCellStyle = dataGridViewCellStyle7;
            this.iznos.FillWeight = 90F;
            this.iznos.HeaderText = "Iznos netto";
            this.iznos.Name = "iznos";
            this.iznos.Width = 90;
            // 
            // iznosPDV
            // 
            this.iznosPDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iznosPDV.DefaultCellStyle = dataGridViewCellStyle8;
            this.iznosPDV.FillWeight = 90F;
            this.iznosPDV.HeaderText = "Iznos PDV";
            this.iznosPDV.Name = "iznosPDV";
            this.iznosPDV.ReadOnly = true;
            this.iznosPDV.Width = 90;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvMaping);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(927, 368);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mapiranje";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvMaping
            // 
            this.dgvMaping.AllowUserToAddRows = false;
            this.dgvMaping.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvMaping.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvMaping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaping.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaping.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaping.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvMaping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mGrupa,
            this.mNasaGrupa});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaping.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvMaping.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMaping.Location = new System.Drawing.Point(3, 3);
            this.dgvMaping.Name = "dgvMaping";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaping.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvMaping.RowHeadersVisible = false;
            this.dgvMaping.Size = new System.Drawing.Size(921, 362);
            this.dgvMaping.TabIndex = 2;
            this.dgvMaping.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaping_CellEndEdit);
            // 
            // mGrupa
            // 
            this.mGrupa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mGrupa.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.mGrupa.HeaderText = "Grupa";
            this.mGrupa.Name = "mGrupa";
            this.mGrupa.ReadOnly = true;
            this.mGrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mGrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // mNasaGrupa
            // 
            this.mNasaGrupa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mNasaGrupa.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.mNasaGrupa.HeaderText = "Naša Grupa";
            this.mNasaGrupa.Name = "mNasaGrupa";
            this.mNasaGrupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mNasaGrupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Broj ulaznog dokumenta: ";
            // 
            // txtBrojUlaznogDokumenta
            // 
            this.txtBrojUlaznogDokumenta.Location = new System.Drawing.Point(135, 6);
            this.txtBrojUlaznogDokumenta.Name = "txtBrojUlaznogDokumenta";
            this.txtBrojUlaznogDokumenta.Size = new System.Drawing.Size(197, 20);
            this.txtBrojUlaznogDokumenta.TabIndex = 80;
            // 
            // datumDokumenta
            // 
            this.datumDokumenta.Location = new System.Drawing.Point(135, 27);
            this.datumDokumenta.Name = "datumDokumenta";
            this.datumDokumenta.Size = new System.Drawing.Size(197, 20);
            this.datumDokumenta.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Datum dokumenta: ";
            // 
            // txtNazivPoslovnice
            // 
            this.txtNazivPoslovnice.Location = new System.Drawing.Point(135, 48);
            this.txtNazivPoslovnice.Name = "txtNazivPoslovnice";
            this.txtNazivPoslovnice.Size = new System.Drawing.Size(197, 20);
            this.txtNazivPoslovnice.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Naziv poslovnice:";
            // 
            // txtAdresaPoslovnice
            // 
            this.txtAdresaPoslovnice.Location = new System.Drawing.Point(475, 5);
            this.txtAdresaPoslovnice.Name = "txtAdresaPoslovnice";
            this.txtAdresaPoslovnice.Size = new System.Drawing.Size(197, 20);
            this.txtAdresaPoslovnice.TabIndex = 86;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(349, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 85;
            this.label4.Text = "Adresa poslovnice:";
            // 
            // txtMjestoPoslovnice
            // 
            this.txtMjestoPoslovnice.Location = new System.Drawing.Point(475, 26);
            this.txtMjestoPoslovnice.Name = "txtMjestoPoslovnice";
            this.txtMjestoPoslovnice.Size = new System.Drawing.Size(197, 20);
            this.txtMjestoPoslovnice.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(349, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 87;
            this.label5.Text = "Mjesto poslovnice:";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Arial", 9F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(474, 48);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(199, 23);
            this.cbSkladiste.TabIndex = 90;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(349, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "Skladište:";
            // 
            // btnUcitajDatoteku
            // 
            this.btnUcitajDatoteku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitajDatoteku.BackColor = System.Drawing.Color.White;
            this.btnUcitajDatoteku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitajDatoteku.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitajDatoteku.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUcitajDatoteku.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitajDatoteku.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitajDatoteku.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUcitajDatoteku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitajDatoteku.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUcitajDatoteku.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUcitajDatoteku.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUcitajDatoteku.Location = new System.Drawing.Point(808, 7);
            this.btnUcitajDatoteku.Name = "btnUcitajDatoteku";
            this.btnUcitajDatoteku.Size = new System.Drawing.Size(138, 43);
            this.btnUcitajDatoteku.TabIndex = 127;
            this.btnUcitajDatoteku.Text = "Učitaj datoteku";
            this.btnUcitajDatoteku.UseVisualStyleBackColor = false;
            this.btnUcitajDatoteku.Click += new System.EventHandler(this.btnUcitajDatoteku_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSpremi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSpremi.Location = new System.Drawing.Point(781, 475);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(167, 44);
            this.btnSpremi.TabIndex = 128;
            this.btnSpremi.Text = "Spremi stavke u primku";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // lblBezPdv
            // 
            this.lblBezPdv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBezPdv.AutoSize = true;
            this.lblBezPdv.BackColor = System.Drawing.Color.Transparent;
            this.lblBezPdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblBezPdv.ForeColor = System.Drawing.Color.Black;
            this.lblBezPdv.Location = new System.Drawing.Point(11, 474);
            this.lblBezPdv.Name = "lblBezPdv";
            this.lblBezPdv.Size = new System.Drawing.Size(139, 15);
            this.lblBezPdv.TabIndex = 129;
            this.lblBezPdv.Text = "Ukupno bez poreza: ";
            // 
            // lblSaPdv
            // 
            this.lblSaPdv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaPdv.AutoSize = true;
            this.lblSaPdv.BackColor = System.Drawing.Color.Transparent;
            this.lblSaPdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSaPdv.ForeColor = System.Drawing.Color.Black;
            this.lblSaPdv.Location = new System.Drawing.Point(11, 505);
            this.lblSaPdv.Name = "lblSaPdv";
            this.lblSaPdv.Size = new System.Drawing.Size(143, 15);
            this.lblSaPdv.TabIndex = 130;
            this.lblSaPdv.Text = "Ukupno sa porezom: ";
            // 
            // lblPorez
            // 
            this.lblPorez.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPorez.AutoSize = true;
            this.lblPorez.BackColor = System.Drawing.Color.Transparent;
            this.lblPorez.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblPorez.ForeColor = System.Drawing.Color.Black;
            this.lblPorez.Location = new System.Drawing.Point(11, 490);
            this.lblPorez.Name = "lblPorez";
            this.lblPorez.Size = new System.Drawing.Size(52, 15);
            this.lblPorez.TabIndex = 131;
            this.lblPorez.Text = "Porez: ";
            // 
            // frmRoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(958, 527);
            this.Controls.Add(this.lblPorez);
            this.Controls.Add(this.lblSaPdv);
            this.Controls.Add(this.lblBezPdv);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnUcitajDatoteku);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.txtMjestoPoslovnice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAdresaPoslovnice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNazivPoslovnice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datumDokumenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBrojUlaznogDokumenta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmRoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roto";
            this.Load += new System.EventHandler(this.frmRoto_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikli)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaping)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.DataGridView dgvArtikli;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.DataGridView dgvMaping;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBrojUlaznogDokumenta;
        private System.Windows.Forms.TextBox datumDokumenta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNazivPoslovnice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdresaPoslovnice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMjestoPoslovnice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewComboBoxColumn mGrupa;
        private System.Windows.Forms.DataGridViewComboBoxColumn mNasaGrupa;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUcitajDatoteku;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label lblBezPdv;
        private System.Windows.Forms.Label lblSaPdv;
        private System.Windows.Forms.Label lblPorez;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznosPDV;

    }
}