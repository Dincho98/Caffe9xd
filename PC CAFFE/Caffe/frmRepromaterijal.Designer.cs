namespace PCPOS.Caffe
{
    partial class frmRepromaterijal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepromaterijal));
            this.dgwR = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPodgrupa = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUlazniPorez = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIzlazniPorez = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPNP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPovratnaNaknada = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPoticajnaNaknada = new System.Windows.Forms.TextBox();
            this.dgwA = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbGrupe = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMjera = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNabavnaCijena = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.chbAktivnost = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.btnUredi = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnObrisiArtikl = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwA)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwR
            // 
            this.dgwR.AllowUserToAddRows = false;
            this.dgwR.AllowUserToDeleteRows = false;
            this.dgwR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgwR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwR.BackgroundColor = System.Drawing.Color.White;
            this.dgwR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwR.Location = new System.Drawing.Point(13, 80);
            this.dgwR.Name = "dgwR";
            this.dgwR.ReadOnly = true;
            this.dgwR.RowHeadersVisible = false;
            this.dgwR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwR.Size = new System.Drawing.Size(696, 344);
            this.dgwR.TabIndex = 20;
            this.dgwR.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwR_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Naziv:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(12, 70);
            this.txtNaziv.MaxLength = 50;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(213, 23);
            this.txtNaziv.TabIndex = 2;
            this.txtNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbGrupa
            // 
            this.cbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Location = new System.Drawing.Point(12, 111);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(213, 24);
            this.cbGrupa.TabIndex = 3;
            this.cbGrupa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbGrupa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Grupa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(12, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Podgrupa:";
            // 
            // cbPodgrupa
            // 
            this.cbPodgrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPodgrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbPodgrupa.FormattingEnabled = true;
            this.cbPodgrupa.Location = new System.Drawing.Point(12, 154);
            this.cbPodgrupa.Name = "cbPodgrupa";
            this.cbPodgrupa.Size = new System.Drawing.Size(213, 24);
            this.cbPodgrupa.TabIndex = 4;
            this.cbPodgrupa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbPodgrupa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(125, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Ulazni porez:";
            // 
            // txtUlazniPorez
            // 
            this.txtUlazniPorez.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUlazniPorez.Location = new System.Drawing.Point(125, 301);
            this.txtUlazniPorez.MaxLength = 50;
            this.txtUlazniPorez.Name = "txtUlazniPorez";
            this.txtUlazniPorez.Size = new System.Drawing.Size(100, 23);
            this.txtUlazniPorez.TabIndex = 9;
            this.txtUlazniPorez.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtUlazniPorez.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtUlazniPorez.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(12, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "Izlazni porez:";
            // 
            // txtIzlazniPorez
            // 
            this.txtIzlazniPorez.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzlazniPorez.Location = new System.Drawing.Point(12, 303);
            this.txtIzlazniPorez.MaxLength = 50;
            this.txtIzlazniPorez.Name = "txtIzlazniPorez";
            this.txtIzlazniPorez.Size = new System.Drawing.Size(107, 23);
            this.txtIzlazniPorez.TabIndex = 8;
            this.txtIzlazniPorez.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzlazniPorez.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtIzlazniPorez.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F);
            this.label6.Location = new System.Drawing.Point(12, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "Porez na potrošnju:";
            // 
            // txtPNP
            // 
            this.txtPNP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPNP.Location = new System.Drawing.Point(12, 345);
            this.txtPNP.MaxLength = 50;
            this.txtPNP.Name = "txtPNP";
            this.txtPNP.Size = new System.Drawing.Size(213, 23);
            this.txtPNP.TabIndex = 10;
            this.txtPNP.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPNP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtPNP.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F);
            this.label7.Location = new System.Drawing.Point(12, 370);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 14);
            this.label7.TabIndex = 1;
            this.label7.Text = "Povratna naknada:";
            // 
            // txtPovratnaNaknada
            // 
            this.txtPovratnaNaknada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPovratnaNaknada.Location = new System.Drawing.Point(12, 387);
            this.txtPovratnaNaknada.MaxLength = 50;
            this.txtPovratnaNaknada.Name = "txtPovratnaNaknada";
            this.txtPovratnaNaknada.Size = new System.Drawing.Size(213, 23);
            this.txtPovratnaNaknada.TabIndex = 11;
            this.txtPovratnaNaknada.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPovratnaNaknada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtPovratnaNaknada.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F);
            this.label8.Location = new System.Drawing.Point(12, 413);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "Poticajna naknada:";
            // 
            // txtPoticajnaNaknada
            // 
            this.txtPoticajnaNaknada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPoticajnaNaknada.Location = new System.Drawing.Point(12, 430);
            this.txtPoticajnaNaknada.MaxLength = 50;
            this.txtPoticajnaNaknada.Name = "txtPoticajnaNaknada";
            this.txtPoticajnaNaknada.Size = new System.Drawing.Size(213, 23);
            this.txtPoticajnaNaknada.TabIndex = 12;
            this.txtPoticajnaNaknada.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPoticajnaNaknada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtPoticajnaNaknada.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dgwA
            // 
            this.dgwA.AllowUserToAddRows = false;
            this.dgwA.AllowUserToDeleteRows = false;
            this.dgwA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgwA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwA.BackgroundColor = System.Drawing.Color.White;
            this.dgwA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.cijena});
            this.dgwA.Location = new System.Drawing.Point(12, 452);
            this.dgwA.Name = "dgwA";
            this.dgwA.ReadOnly = true;
            this.dgwA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwA.Size = new System.Drawing.Size(697, 165);
            this.dgwA.TabIndex = 21;
            // 
            // sifra
            // 
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
            // cijena
            // 
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F);
            this.label9.Location = new System.Drawing.Point(10, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(344, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "Artikli za prodaju koji sadrže odabrani repromaterijal:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F);
            this.label10.Location = new System.Drawing.Point(12, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "Repromaterijal:";
            // 
            // cbGrupe
            // 
            this.cbGrupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrupe.FormattingEnabled = true;
            this.cbGrupe.Location = new System.Drawing.Point(514, 50);
            this.cbGrupe.Name = "cbGrupe";
            this.cbGrupe.Size = new System.Drawing.Size(194, 24);
            this.cbGrupe.TabIndex = 19;
            this.cbGrupe.SelectedIndexChanged += new System.EventHandler(this.cbGrupe_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.Location = new System.Drawing.Point(464, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "Prikaži";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F);
            this.label12.Location = new System.Drawing.Point(125, 243);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 14);
            this.label12.TabIndex = 1;
            this.label12.Text = "Mjera:";
            // 
            // txtMjera
            // 
            this.txtMjera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMjera.Location = new System.Drawing.Point(125, 260);
            this.txtMjera.MaxLength = 50;
            this.txtMjera.Name = "txtMjera";
            this.txtMjera.Size = new System.Drawing.Size(100, 23);
            this.txtMjera.TabIndex = 7;
            this.txtMjera.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtMjera.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F);
            this.label13.Location = new System.Drawing.Point(12, 243);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 14);
            this.label13.TabIndex = 1;
            this.label13.Text = "Nabavna cijena:";
            // 
            // txtNabavnaCijena
            // 
            this.txtNabavnaCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNabavnaCijena.Location = new System.Drawing.Point(12, 260);
            this.txtNabavnaCijena.MaxLength = 50;
            this.txtNabavnaCijena.Name = "txtNabavnaCijena";
            this.txtNabavnaCijena.Size = new System.Drawing.Size(107, 23);
            this.txtNabavnaCijena.TabIndex = 6;
            this.txtNabavnaCijena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtNabavnaCijena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtNabavnaCijena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F);
            this.label14.Location = new System.Drawing.Point(12, 180);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 14);
            this.label14.TabIndex = 1;
            this.label14.Text = "Skladište";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(12, 196);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(213, 24);
            this.cbSkladiste.TabIndex = 5;
            this.cbSkladiste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbSkladiste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // chbAktivnost
            // 
            this.chbAktivnost.AutoSize = true;
            this.chbAktivnost.BackColor = System.Drawing.Color.Transparent;
            this.chbAktivnost.Checked = true;
            this.chbAktivnost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAktivnost.Font = new System.Drawing.Font("Verdana", 9F);
            this.chbAktivnost.Location = new System.Drawing.Point(12, 462);
            this.chbAktivnost.Name = "chbAktivnost";
            this.chbAktivnost.Size = new System.Drawing.Size(84, 18);
            this.chbAktivnost.TabIndex = 13;
            this.chbAktivnost.Text = "Aktivnost";
            this.chbAktivnost.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F);
            this.label15.Location = new System.Drawing.Point(12, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 14);
            this.label15.TabIndex = 1;
            this.label15.Text = "Šifra";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(12, 30);
            this.txtSifra.MaxLength = 50;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(213, 23);
            this.txtSifra.TabIndex = 1;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
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
            this.btnUredi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUredi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUredi.Location = new System.Drawing.Point(172, 12);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(154, 40);
            this.btnUredi.TabIndex = 17;
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
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnIzlaz.ForeColor = System.Drawing.Color.White;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(810, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(154, 40);
            this.btnIzlaz.TabIndex = 18;
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
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(154, 40);
            this.btnNoviUnos.TabIndex = 16;
            this.btnNoviUnos.Text = "Novi unos";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(751, 577);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(107, 40);
            this.btnOdustani.TabIndex = 14;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(864, 577);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(100, 40);
            this.btnSpremi.TabIndex = 15;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
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
            this.btnObrisiArtikl.Location = new System.Drawing.Point(111, 459);
            this.btnObrisiArtikl.Name = "btnObrisiArtikl";
            this.btnObrisiArtikl.Size = new System.Drawing.Size(114, 24);
            this.btnObrisiArtikl.TabIndex = 67;
            this.btnObrisiArtikl.Text = "Obriši normativ";
            this.btnObrisiArtikl.UseVisualStyleBackColor = false;
            this.btnObrisiArtikl.Click += new System.EventHandler(this.btnObrisiArtikl_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtSifra);
            this.panel1.Controls.Add(this.btnObrisiArtikl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chbAktivnost);
            this.panel1.Controls.Add(this.txtNaziv);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtUlazniPorez);
            this.panel1.Controls.Add(this.txtMjera);
            this.panel1.Controls.Add(this.txtNabavnaCijena);
            this.panel1.Controls.Add(this.cbSkladiste);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbPodgrupa);
            this.panel1.Controls.Add(this.txtIzlazniPorez);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbGrupa);
            this.panel1.Controls.Add(this.txtPNP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtPoticajnaNaknada);
            this.panel1.Controls.Add(this.txtPovratnaNaknada);
            this.panel1.Location = new System.Drawing.Point(739, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 491);
            this.panel1.TabIndex = 68;
            // 
            // frmRepromaterijal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(981, 629);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnUredi);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.cbGrupe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dgwA);
            this.Controls.Add(this.dgwR);
            this.Name = "frmRepromaterijal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repromaterijal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            this.Resize += new System.EventHandler(this.frmRepromaterijal_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgwR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwA)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPodgrupa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUlazniPorez;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIzlazniPorez;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPNP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPovratnaNaknada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPoticajnaNaknada;
        private System.Windows.Forms.DataGridView dgwA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbGrupe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMjera;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNabavnaCijena;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.CheckBox chbAktivnost;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.Button btnObrisiArtikl;
        private System.Windows.Forms.Panel panel1;
    }
}