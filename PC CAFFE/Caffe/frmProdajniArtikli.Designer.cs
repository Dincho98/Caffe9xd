namespace PCPOS.Caffe
{
    partial class frmProdajniArtikli
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
            this.chbAktivnost = new System.Windows.Forms.CheckBox();
            this.cbGrupe = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPodgrupa = new System.Windows.Forms.ComboBox();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPNP = new System.Windows.Forms.TextBox();
            this.txtIzlazniPorez = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMjera = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgwA = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwR = new System.Windows.Forms.DataGridView();
            this.txtCijena = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDodajNoviArtikl = new System.Windows.Forms.Button();
            this.btnUredi = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnObrisiNormativ = new System.Windows.Forms.Button();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnOdaberi = new System.Windows.Forms.Button();
            this.Green = new System.Windows.Forms.RadioButton();
            this.bezBoje = new System.Windows.Forms.RadioButton();
            this.Aqua = new System.Windows.Forms.RadioButton();
            this.MediumPurple = new System.Windows.Forms.RadioButton();
            this.Mangenta = new System.Windows.Forms.RadioButton();
            this.Violet = new System.Windows.Forms.RadioButton();
            this.Gray = new System.Windows.Forms.RadioButton();
            this.SkyBlue = new System.Windows.Forms.RadioButton();
            this.ForestGreen = new System.Windows.Forms.RadioButton();
            this.Yellow = new System.Windows.Forms.RadioButton();
            this.Blue = new System.Windows.Forms.RadioButton();
            this.White = new System.Windows.Forms.RadioButton();
            this.Black = new System.Windows.Forms.RadioButton();
            this.btnGrupa = new System.Windows.Forms.Button();
            this.btnObrisiArtikl = new System.Windows.Forms.Button();
            this.cbPoreznaGrupa = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgwA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbAktivnost
            // 
            this.chbAktivnost.AutoSize = true;
            this.chbAktivnost.BackColor = System.Drawing.Color.Transparent;
            this.chbAktivnost.Checked = true;
            this.chbAktivnost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAktivnost.Font = new System.Drawing.Font("Verdana", 9F);
            this.chbAktivnost.Location = new System.Drawing.Point(512, 327);
            this.chbAktivnost.Name = "chbAktivnost";
            this.chbAktivnost.Size = new System.Drawing.Size(84, 18);
            this.chbAktivnost.TabIndex = 14;
            this.chbAktivnost.Text = "Aktivnost";
            this.chbAktivnost.UseVisualStyleBackColor = false;
            // 
            // cbGrupe
            // 
            this.cbGrupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrupe.FormattingEnabled = true;
            this.cbGrupe.Location = new System.Drawing.Point(300, 78);
            this.cbGrupe.Name = "cbGrupe";
            this.cbGrupe.Size = new System.Drawing.Size(194, 24);
            this.cbGrupe.TabIndex = 3;
            this.cbGrupe.SelectedIndexChanged += new System.EventHandler(this.cbGrupe_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.Location = new System.Drawing.Point(250, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 15);
            this.label11.TabIndex = 42;
            this.label11.Text = "Prikaži:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F);
            this.label10.Location = new System.Drawing.Point(512, 357);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 14);
            this.label10.TabIndex = 40;
            this.label10.Text = "Normativ prodajnog artikla";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F);
            this.label9.Location = new System.Drawing.Point(12, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 14);
            this.label9.TabIndex = 38;
            this.label9.Text = "Prodajni artikli:";
            // 
            // cbPodgrupa
            // 
            this.cbPodgrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPodgrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbPodgrupa.FormattingEnabled = true;
            this.cbPodgrupa.Location = new System.Drawing.Point(616, 175);
            this.cbPodgrupa.Name = "cbPodgrupa";
            this.cbPodgrupa.Size = new System.Drawing.Size(247, 24);
            this.cbPodgrupa.TabIndex = 8;
            this.cbPodgrupa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbPodgrupa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbGrupa
            // 
            this.cbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Location = new System.Drawing.Point(616, 150);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(247, 24);
            this.cbGrupa.TabIndex = 7;
            this.cbGrupa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbGrupa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(511, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 26;
            this.label3.Text = "Podgrupa:";
            // 
            // txtPNP
            // 
            this.txtPNP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPNP.Location = new System.Drawing.Point(616, 297);
            this.txtPNP.MaxLength = 50;
            this.txtPNP.Name = "txtPNP";
            this.txtPNP.Size = new System.Drawing.Size(247, 23);
            this.txtPNP.TabIndex = 13;
            this.txtPNP.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPNP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtPNP.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtIzlazniPorez
            // 
            this.txtIzlazniPorez.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzlazniPorez.Location = new System.Drawing.Point(616, 225);
            this.txtIzlazniPorez.MaxLength = 50;
            this.txtIzlazniPorez.Name = "txtIzlazniPorez";
            this.txtIzlazniPorez.Size = new System.Drawing.Size(247, 23);
            this.txtIzlazniPorez.TabIndex = 10;
            this.txtIzlazniPorez.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzlazniPorez.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtIzlazniPorez.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F);
            this.label6.Location = new System.Drawing.Point(512, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 14);
            this.label6.TabIndex = 30;
            this.label6.Text = "Porez na pot.:";
            // 
            // txtMjera
            // 
            this.txtMjera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMjera.Location = new System.Drawing.Point(616, 249);
            this.txtMjera.MaxLength = 50;
            this.txtMjera.Name = "txtMjera";
            this.txtMjera.Size = new System.Drawing.Size(247, 23);
            this.txtMjera.TabIndex = 11;
            this.txtMjera.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtMjera.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(511, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 29;
            this.label5.Text = "Porez PDV:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F);
            this.label12.Location = new System.Drawing.Point(511, 253);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 14);
            this.label12.TabIndex = 27;
            this.label12.Text = "Mjera:";
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(616, 78);
            this.txtSifra.MaxLength = 50;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(247, 23);
            this.txtSifra.TabIndex = 4;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(616, 102);
            this.txtNaziv.MaxLength = 50;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(247, 23);
            this.txtNaziv.TabIndex = 5;
            this.txtNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(509, 85);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 14);
            this.label15.TabIndex = 22;
            this.label15.Text = "Šifra:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(511, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 32;
            this.label2.Text = "Grupa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(509, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 34;
            this.label1.Text = "Naziv:";
            // 
            // dgwA
            // 
            this.dgwA.AllowUserToAddRows = false;
            this.dgwA.AllowUserToDeleteRows = false;
            this.dgwA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgwA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwA.BackgroundColor = System.Drawing.Color.White;
            this.dgwA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.kolicina,
            this.skladiste});
            this.dgwA.Location = new System.Drawing.Point(513, 373);
            this.dgwA.Name = "dgwA";
            this.dgwA.RowHeadersVisible = false;
            this.dgwA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwA.Size = new System.Drawing.Size(348, 166);
            this.dgwA.TabIndex = 16;
            this.dgwA.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwA_CellEndEdit);
            // 
            // sifra
            // 
            this.sifra.FillWeight = 30F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 30F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // skladiste
            // 
            this.skladiste.FillWeight = 50F;
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            // 
            // dgwR
            // 
            this.dgwR.AllowUserToAddRows = false;
            this.dgwR.AllowUserToDeleteRows = false;
            this.dgwR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgwR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwR.BackgroundColor = System.Drawing.Color.Silver;
            this.dgwR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgwR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwR.Location = new System.Drawing.Point(15, 105);
            this.dgwR.Name = "dgwR";
            this.dgwR.ReadOnly = true;
            this.dgwR.RowHeadersVisible = false;
            this.dgwR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwR.Size = new System.Drawing.Size(479, 480);
            this.dgwR.TabIndex = 17;
            this.dgwR.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwR_CellClick);
            // 
            // txtCijena
            // 
            this.txtCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCijena.Location = new System.Drawing.Point(616, 126);
            this.txtCijena.MaxLength = 50;
            this.txtCijena.Name = "txtCijena";
            this.txtCijena.Size = new System.Drawing.Size(247, 23);
            this.txtCijena.TabIndex = 6;
            this.txtCijena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtCijena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtCijena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(509, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 61;
            this.label4.Text = "Cijena:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtEan
            // 
            this.txtEan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEan.Location = new System.Drawing.Point(616, 273);
            this.txtEan.MaxLength = 50;
            this.txtEan.Name = "txtEan";
            this.txtEan.Size = new System.Drawing.Size(247, 23);
            this.txtEan.TabIndex = 12;
            this.txtEan.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtEan.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F);
            this.label7.Location = new System.Drawing.Point(511, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 63;
            this.label7.Text = "Bar code:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 10F);
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.Location = new System.Drawing.Point(758, 545);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 40);
            this.button3.TabIndex = 19;
            this.button3.Text = "Spremi";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 10F);
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(639, 545);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 40);
            this.button2.TabIndex = 18;
            this.button2.Text = "Odustani";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnDodajNoviArtikl
            // 
            this.btnDodajNoviArtikl.BackColor = System.Drawing.Color.White;
            this.btnDodajNoviArtikl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajNoviArtikl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajNoviArtikl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajNoviArtikl.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDodajNoviArtikl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNoviArtikl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajNoviArtikl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajNoviArtikl.Font = new System.Drawing.Font("Arial", 10F);
            this.btnDodajNoviArtikl.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDodajNoviArtikl.Location = new System.Drawing.Point(749, 346);
            this.btnDodajNoviArtikl.Name = "btnDodajNoviArtikl";
            this.btnDodajNoviArtikl.Size = new System.Drawing.Size(115, 25);
            this.btnDodajNoviArtikl.TabIndex = 15;
            this.btnDodajNoviArtikl.Text = "Dodaj normativ";
            this.btnDodajNoviArtikl.UseVisualStyleBackColor = false;
            this.btnDodajNoviArtikl.Click += new System.EventHandler(this.btnDodajNoviArtikl_Click);
            // 
            // btnUredi
            // 
            this.btnUredi.BackColor = System.Drawing.Color.White;
            this.btnUredi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUredi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUredi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUredi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUredi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUredi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUredi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUredi.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUredi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUredi.Location = new System.Drawing.Point(168, 12);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(154, 40);
            this.btnUredi.TabIndex = 1;
            this.btnUredi.Text = "Uredi stavku";
            this.btnUredi.UseVisualStyleBackColor = false;
            this.btnUredi.Click += new System.EventHandler(this.btnUredi_Click);
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIzlaz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIzlaz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzlaz.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnIzlaz.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIzlaz.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIzlaz.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIzlaz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzlaz.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnIzlaz.ForeColor = System.Drawing.Color.White;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(825, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(154, 40);
            this.btnIzlaz.TabIndex = 2;
            this.btnIzlaz.Text = "Izlaz";
            this.btnIzlaz.UseVisualStyleBackColor = false;
            this.btnIzlaz.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(8, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(154, 40);
            this.btnNoviUnos.TabIndex = 0;
            this.btnNoviUnos.Text = "Novi unos";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnObrisiNormativ
            // 
            this.btnObrisiNormativ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnObrisiNormativ.BackColor = System.Drawing.Color.White;
            this.btnObrisiNormativ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisiNormativ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiNormativ.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisiNormativ.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnObrisiNormativ.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisiNormativ.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisiNormativ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiNormativ.Font = new System.Drawing.Font("Arial", 10F);
            this.btnObrisiNormativ.ForeColor = System.Drawing.Color.DarkRed;
            this.btnObrisiNormativ.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnObrisiNormativ.Location = new System.Drawing.Point(511, 545);
            this.btnObrisiNormativ.Name = "btnObrisiNormativ";
            this.btnObrisiNormativ.Size = new System.Drawing.Size(119, 40);
            this.btnObrisiNormativ.TabIndex = 64;
            this.btnObrisiNormativ.Text = "Obriši normativ";
            this.btnObrisiNormativ.UseVisualStyleBackColor = false;
            this.btnObrisiNormativ.Click += new System.EventHandler(this.btnObrisiNormativ_Click);
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Arial", 10F);
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspis.Location = new System.Drawing.Point(328, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(166, 40);
            this.btnIspis.TabIndex = 1;
            this.btnIspis.Text = "Ispis prodajnih artikla";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // btnOdaberi
            // 
            this.btnOdaberi.BackColor = System.Drawing.Color.White;
            this.btnOdaberi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdaberi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdaberi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdaberi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdaberi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdaberi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdaberi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdaberi.Font = new System.Drawing.Font("Arial", 10F);
            this.btnOdaberi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOdaberi.Location = new System.Drawing.Point(3, 9);
            this.btnOdaberi.Name = "btnOdaberi";
            this.btnOdaberi.Size = new System.Drawing.Size(102, 60);
            this.btnOdaberi.TabIndex = 20;
            this.btnOdaberi.Text = "Odaberi izgled gumba";
            this.btnOdaberi.UseVisualStyleBackColor = false;
            this.btnOdaberi.Click += new System.EventHandler(this.btnOdaberi_Click);
            // 
            // Green
            // 
            this.Green.AutoSize = true;
            this.Green.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Green.ForeColor = System.Drawing.Color.Green;
            this.Green.Location = new System.Drawing.Point(3, 241);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(64, 17);
            this.Green.TabIndex = 1;
            this.Green.TabStop = true;
            this.Green.Text = "Zeleno";
            this.Green.UseVisualStyleBackColor = true;
            this.Green.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // bezBoje
            // 
            this.bezBoje.AutoSize = true;
            this.bezBoje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bezBoje.Location = new System.Drawing.Point(3, 172);
            this.bezBoje.Name = "bezBoje";
            this.bezBoje.Size = new System.Drawing.Size(74, 17);
            this.bezBoje.TabIndex = 1;
            this.bezBoje.TabStop = true;
            this.bezBoje.Text = "Bez boje";
            this.bezBoje.UseVisualStyleBackColor = true;
            this.bezBoje.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Aqua
            // 
            this.Aqua.AutoSize = true;
            this.Aqua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Aqua.ForeColor = System.Drawing.Color.Aqua;
            this.Aqua.Location = new System.Drawing.Point(3, 448);
            this.Aqua.Name = "Aqua";
            this.Aqua.Size = new System.Drawing.Size(54, 17);
            this.Aqua.TabIndex = 1;
            this.Aqua.TabStop = true;
            this.Aqua.Text = "Aqua";
            this.Aqua.UseVisualStyleBackColor = true;
            this.Aqua.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // MediumPurple
            // 
            this.MediumPurple.AutoSize = true;
            this.MediumPurple.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MediumPurple.ForeColor = System.Drawing.Color.MediumPurple;
            this.MediumPurple.Location = new System.Drawing.Point(3, 425);
            this.MediumPurple.Name = "MediumPurple";
            this.MediumPurple.Size = new System.Drawing.Size(107, 17);
            this.MediumPurple.TabIndex = 1;
            this.MediumPurple.TabStop = true;
            this.MediumPurple.Text = "Medium purple";
            this.MediumPurple.UseVisualStyleBackColor = true;
            this.MediumPurple.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Mangenta
            // 
            this.Mangenta.AutoSize = true;
            this.Mangenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Mangenta.ForeColor = System.Drawing.Color.DarkMagenta;
            this.Mangenta.Location = new System.Drawing.Point(3, 402);
            this.Mangenta.Name = "Mangenta";
            this.Mangenta.Size = new System.Drawing.Size(74, 17);
            this.Mangenta.TabIndex = 1;
            this.Mangenta.TabStop = true;
            this.Mangenta.Text = "Mangeta";
            this.Mangenta.UseVisualStyleBackColor = true;
            this.Mangenta.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Violet
            // 
            this.Violet.AutoSize = true;
            this.Violet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Violet.ForeColor = System.Drawing.Color.Violet;
            this.Violet.Location = new System.Drawing.Point(3, 379);
            this.Violet.Name = "Violet";
            this.Violet.Size = new System.Drawing.Size(57, 17);
            this.Violet.TabIndex = 1;
            this.Violet.TabStop = true;
            this.Violet.Text = "Violet";
            this.Violet.UseVisualStyleBackColor = true;
            this.Violet.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Gray
            // 
            this.Gray.AutoSize = true;
            this.Gray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Gray.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.Gray.Location = new System.Drawing.Point(3, 356);
            this.Gray.Name = "Gray";
            this.Gray.Size = new System.Drawing.Size(50, 17);
            this.Gray.TabIndex = 1;
            this.Gray.TabStop = true;
            this.Gray.Text = "Sivo";
            this.Gray.UseVisualStyleBackColor = true;
            this.Gray.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // SkyBlue
            // 
            this.SkyBlue.AutoSize = true;
            this.SkyBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SkyBlue.ForeColor = System.Drawing.Color.SkyBlue;
            this.SkyBlue.Location = new System.Drawing.Point(3, 333);
            this.SkyBlue.Name = "SkyBlue";
            this.SkyBlue.Size = new System.Drawing.Size(71, 17);
            this.SkyBlue.TabIndex = 1;
            this.SkyBlue.TabStop = true;
            this.SkyBlue.Text = "SkyBlue";
            this.SkyBlue.UseVisualStyleBackColor = true;
            this.SkyBlue.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // ForestGreen
            // 
            this.ForestGreen.AutoSize = true;
            this.ForestGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForestGreen.ForeColor = System.Drawing.Color.ForestGreen;
            this.ForestGreen.Location = new System.Drawing.Point(3, 310);
            this.ForestGreen.Name = "ForestGreen";
            this.ForestGreen.Size = new System.Drawing.Size(98, 17);
            this.ForestGreen.TabIndex = 1;
            this.ForestGreen.TabStop = true;
            this.ForestGreen.Text = "Forest Green";
            this.ForestGreen.UseVisualStyleBackColor = true;
            this.ForestGreen.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Yellow
            // 
            this.Yellow.AutoSize = true;
            this.Yellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Yellow.ForeColor = System.Drawing.Color.Yellow;
            this.Yellow.Location = new System.Drawing.Point(3, 287);
            this.Yellow.Name = "Yellow";
            this.Yellow.Size = new System.Drawing.Size(51, 17);
            this.Yellow.TabIndex = 1;
            this.Yellow.TabStop = true;
            this.Yellow.Text = "Žuto";
            this.Yellow.UseVisualStyleBackColor = true;
            this.Yellow.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Blue
            // 
            this.Blue.AutoSize = true;
            this.Blue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Blue.ForeColor = System.Drawing.Color.Blue;
            this.Blue.Location = new System.Drawing.Point(3, 264);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(57, 17);
            this.Blue.TabIndex = 1;
            this.Blue.TabStop = true;
            this.Blue.Text = "Plavo";
            this.Blue.UseVisualStyleBackColor = true;
            this.Blue.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // White
            // 
            this.White.AutoSize = true;
            this.White.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.White.ForeColor = System.Drawing.Color.White;
            this.White.Location = new System.Drawing.Point(3, 218);
            this.White.Name = "White";
            this.White.Size = new System.Drawing.Size(56, 17);
            this.White.TabIndex = 1;
            this.White.TabStop = true;
            this.White.Text = "Bijelo";
            this.White.UseVisualStyleBackColor = true;
            this.White.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // Black
            // 
            this.Black.AutoSize = true;
            this.Black.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Black.Location = new System.Drawing.Point(3, 195);
            this.Black.Name = "Black";
            this.Black.Size = new System.Drawing.Size(51, 17);
            this.Black.TabIndex = 1;
            this.Black.TabStop = true;
            this.Black.Text = "Crno";
            this.Black.UseVisualStyleBackColor = true;
            this.Black.CheckedChanged += new System.EventHandler(this.Green_CheckedChanged);
            // 
            // btnGrupa
            // 
            this.btnGrupa.Location = new System.Drawing.Point(7, 84);
            this.btnGrupa.Name = "btnGrupa";
            this.btnGrupa.Size = new System.Drawing.Size(94, 73);
            this.btnGrupa.TabIndex = 0;
            this.btnGrupa.Text = "button1";
            this.btnGrupa.UseVisualStyleBackColor = true;
            // 
            // btnObrisiArtikl
            // 
            this.btnObrisiArtikl.BackColor = System.Drawing.Color.White;
            this.btnObrisiArtikl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisiArtikl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiArtikl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisiArtikl.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnObrisiArtikl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisiArtikl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisiArtikl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiArtikl.Font = new System.Drawing.Font("Arial", 10F);
            this.btnObrisiArtikl.ForeColor = System.Drawing.Color.DarkRed;
            this.btnObrisiArtikl.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnObrisiArtikl.Location = new System.Drawing.Point(749, 321);
            this.btnObrisiArtikl.Name = "btnObrisiArtikl";
            this.btnObrisiArtikl.Size = new System.Drawing.Size(115, 25);
            this.btnObrisiArtikl.TabIndex = 66;
            this.btnObrisiArtikl.Text = "Obriši artikl";
            this.btnObrisiArtikl.UseVisualStyleBackColor = false;
            this.btnObrisiArtikl.Click += new System.EventHandler(this.btnObrisiArtikl_Click);
            // 
            // cbPoreznaGrupa
            // 
            this.cbPoreznaGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoreznaGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbPoreznaGrupa.FormattingEnabled = true;
            this.cbPoreznaGrupa.Location = new System.Drawing.Point(616, 200);
            this.cbPoreznaGrupa.Name = "cbPoreznaGrupa";
            this.cbPoreznaGrupa.Size = new System.Drawing.Size(247, 24);
            this.cbPoreznaGrupa.TabIndex = 68;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Verdana", 9F);
            this.label16.Location = new System.Drawing.Point(512, 206);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 14);
            this.label16.TabIndex = 67;
            this.label16.Text = "Porez na potr.:";
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            this.bgSinkronizacija.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgSinkronizacija_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnOdaberi);
            this.panel1.Controls.Add(this.Green);
            this.panel1.Controls.Add(this.btnGrupa);
            this.panel1.Controls.Add(this.bezBoje);
            this.panel1.Controls.Add(this.Black);
            this.panel1.Controls.Add(this.Aqua);
            this.panel1.Controls.Add(this.White);
            this.panel1.Controls.Add(this.MediumPurple);
            this.panel1.Controls.Add(this.Blue);
            this.panel1.Controls.Add(this.Mangenta);
            this.panel1.Controls.Add(this.Yellow);
            this.panel1.Controls.Add(this.Violet);
            this.panel1.Controls.Add(this.ForestGreen);
            this.panel1.Controls.Add(this.Gray);
            this.panel1.Controls.Add(this.SkyBlue);
            this.panel1.Location = new System.Drawing.Point(869, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 476);
            this.panel1.TabIndex = 69;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(869, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 31);
            this.label8.TabIndex = 70;
            this.label8.Text = "Obrub prodajnog gumba";
            // 
            // frmProdajniArtikli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(991, 589);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbPoreznaGrupa);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnObrisiArtikl);
            this.Controls.Add(this.btnObrisiNormativ);
            this.Controls.Add(this.txtEan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCijena);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDodajNoviArtikl);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnUredi);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.chbAktivnost);
            this.Controls.Add(this.cbGrupe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbPodgrupa);
            this.Controls.Add(this.cbGrupa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPNP);
            this.Controls.Add(this.txtIzlazniPorez);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMjera);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgwA);
            this.Controls.Add(this.dgwR);
            this.Name = "frmProdajniArtikli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prodajni artikli";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProdajniArtikli_FormClosing);
            this.Load += new System.EventHandler(this.frmProdajniArtikli_Load);
            this.Resize += new System.EventHandler(this.frmProdajniArtikli_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgwA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.CheckBox chbAktivnost;
        private System.Windows.Forms.ComboBox cbGrupe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPodgrupa;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPNP;
        private System.Windows.Forms.TextBox txtIzlazniPorez;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMjera;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgwA;
        private System.Windows.Forms.DataGridView dgwR;
        private System.Windows.Forms.Button btnDodajNoviArtikl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtCijena;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnObrisiNormativ;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button btnGrupa;
        private System.Windows.Forms.RadioButton Green;
        private System.Windows.Forms.RadioButton bezBoje;
        private System.Windows.Forms.RadioButton Aqua;
        private System.Windows.Forms.RadioButton MediumPurple;
        private System.Windows.Forms.RadioButton Mangenta;
        private System.Windows.Forms.RadioButton Violet;
        private System.Windows.Forms.RadioButton Gray;
        private System.Windows.Forms.RadioButton SkyBlue;
        private System.Windows.Forms.RadioButton ForestGreen;
        private System.Windows.Forms.RadioButton Yellow;
        private System.Windows.Forms.RadioButton Blue;
        private System.Windows.Forms.RadioButton White;
        private System.Windows.Forms.RadioButton Black;
        private System.Windows.Forms.Button btnOdaberi;
        private System.Windows.Forms.Button btnObrisiArtikl;
        private System.Windows.Forms.ComboBox cbPoreznaGrupa;
        private System.Windows.Forms.Label label16;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
    }
}