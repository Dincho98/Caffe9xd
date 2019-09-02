namespace PCPOS.Sifarnik
{
    partial class frmAddPartners
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
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.xAdresa = new System.Windows.Forms.Label();
            this.xGrad = new System.Windows.Forms.Label();
            this.rbPoslovni = new System.Windows.Forms.RadioButton();
            this.rbPrivatni = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojKartice = new System.Windows.Forms.TextBox();
            this.txtOib = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.xOIB = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.xTvrtka = new System.Windows.Forms.Label();
            this.txtTvrtka = new System.Windows.Forms.TextBox();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.cbDrzava = new System.Windows.Forms.ComboBox();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKarticaKupca = new System.Windows.Forms.TextBox();
            this.txtMob = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.txtHormonalniNadomjestak = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtKontracepcija = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbSpol = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpDatumRodenja = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTrudnoca = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIme
            // 
            this.txtIme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIme.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIme.Location = new System.Drawing.Point(152, 69);
            this.txtIme.MaxLength = 29;
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(249, 24);
            this.txtIme.TabIndex = 7;
            this.txtIme.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIme.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(108, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ime:";
            // 
            // txtPrezime
            // 
            this.txtPrezime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPrezime.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPrezime.Location = new System.Drawing.Point(152, 98);
            this.txtPrezime.MaxLength = 29;
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(249, 24);
            this.txtPrezime.TabIndex = 9;
            this.txtPrezime.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPrezime.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(84, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Prezime:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.Location = new System.Drawing.Point(152, 337);
            this.txtEmail.MaxLength = 29;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(249, 24);
            this.txtEmail.TabIndex = 31;
            this.txtEmail.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(100, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Email:";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAdresa.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAdresa.Location = new System.Drawing.Point(152, 157);
            this.txtAdresa.MaxLength = 150;
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(249, 24);
            this.txtAdresa.TabIndex = 14;
            this.txtAdresa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtAdresa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(90, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Adresa:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(102, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Grad:";
            // 
            // txtTel
            // 
            this.txtTel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTel.Location = new System.Drawing.Point(152, 277);
            this.txtTel.MaxLength = 29;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(249, 24);
            this.txtTel.TabIndex = 27;
            this.txtTel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(114, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Tel:";
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSpremi.Location = new System.Drawing.Point(240, 581);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(120, 50);
            this.btnSpremi.TabIndex = 1;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnOdustani.Location = new System.Drawing.Point(111, 581);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(120, 50);
            this.btnOdustani.TabIndex = 2;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSifra.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSifra.Location = new System.Drawing.Point(152, 11);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.ReadOnly = true;
            this.txtSifra.Size = new System.Drawing.Size(90, 24);
            this.txtSifra.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label15.Location = new System.Drawing.Point(117, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "ID:";
            // 
            // xAdresa
            // 
            this.xAdresa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xAdresa.AutoSize = true;
            this.xAdresa.BackColor = System.Drawing.Color.Transparent;
            this.xAdresa.Font = new System.Drawing.Font("Tahoma", 15F);
            this.xAdresa.ForeColor = System.Drawing.Color.Red;
            this.xAdresa.Location = new System.Drawing.Point(404, 162);
            this.xAdresa.Margin = new System.Windows.Forms.Padding(0);
            this.xAdresa.Name = "xAdresa";
            this.xAdresa.Size = new System.Drawing.Size(21, 24);
            this.xAdresa.TabIndex = 15;
            this.xAdresa.Text = "*";
            // 
            // xGrad
            // 
            this.xGrad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xGrad.AutoSize = true;
            this.xGrad.BackColor = System.Drawing.Color.Transparent;
            this.xGrad.Font = new System.Drawing.Font("Tahoma", 15F);
            this.xGrad.ForeColor = System.Drawing.Color.Red;
            this.xGrad.Location = new System.Drawing.Point(404, 218);
            this.xGrad.Margin = new System.Windows.Forms.Padding(0);
            this.xGrad.Name = "xGrad";
            this.xGrad.Size = new System.Drawing.Size(21, 24);
            this.xGrad.TabIndex = 22;
            this.xGrad.Text = "*";
            // 
            // rbPoslovni
            // 
            this.rbPoslovni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbPoslovni.AutoSize = true;
            this.rbPoslovni.BackColor = System.Drawing.Color.Transparent;
            this.rbPoslovni.Checked = true;
            this.rbPoslovni.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rbPoslovni.Location = new System.Drawing.Point(253, 12);
            this.rbPoslovni.Name = "rbPoslovni";
            this.rbPoslovni.Size = new System.Drawing.Size(76, 21);
            this.rbPoslovni.TabIndex = 2;
            this.rbPoslovni.TabStop = true;
            this.rbPoslovni.Text = "Poslovni";
            this.rbPoslovni.UseVisualStyleBackColor = false;
            this.rbPoslovni.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbPrivatni
            // 
            this.rbPrivatni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbPrivatni.AutoSize = true;
            this.rbPrivatni.BackColor = System.Drawing.Color.Transparent;
            this.rbPrivatni.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rbPrivatni.Location = new System.Drawing.Point(333, 11);
            this.rbPrivatni.Name = "rbPrivatni";
            this.rbPrivatni.Size = new System.Drawing.Size(71, 21);
            this.rbPrivatni.TabIndex = 3;
            this.rbPrivatni.Text = "Privatni";
            this.rbPrivatni.UseVisualStyleBackColor = false;
            this.rbPrivatni.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.Location = new System.Drawing.Point(70, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 17);
            this.label11.TabIndex = 4;
            this.label11.Text = "Br. kartice:";
            // 
            // txtBrojKartice
            // 
            this.txtBrojKartice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBrojKartice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojKartice.Location = new System.Drawing.Point(152, 40);
            this.txtBrojKartice.Name = "txtBrojKartice";
            this.txtBrojKartice.ReadOnly = true;
            this.txtBrojKartice.Size = new System.Drawing.Size(249, 24);
            this.txtBrojKartice.TabIndex = 5;
            // 
            // txtOib
            // 
            this.txtOib.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOib.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOib.Location = new System.Drawing.Point(152, 247);
            this.txtOib.MaxLength = 16;
            this.txtOib.Name = "txtOib";
            this.txtOib.Size = new System.Drawing.Size(249, 24);
            this.txtOib.TabIndex = 24;
            this.txtOib.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtOib.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label16.Location = new System.Drawing.Point(109, 251);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 17);
            this.label16.TabIndex = 23;
            this.label16.Text = "OIB:";
            // 
            // xOIB
            // 
            this.xOIB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xOIB.AutoSize = true;
            this.xOIB.BackColor = System.Drawing.Color.Transparent;
            this.xOIB.Font = new System.Drawing.Font("Tahoma", 15F);
            this.xOIB.ForeColor = System.Drawing.Color.Red;
            this.xOIB.Location = new System.Drawing.Point(404, 252);
            this.xOIB.Margin = new System.Windows.Forms.Padding(0);
            this.xOIB.Name = "xOIB";
            this.xOIB.Size = new System.Drawing.Size(21, 24);
            this.xOIB.TabIndex = 25;
            this.xOIB.Text = "*";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label17.Location = new System.Drawing.Point(67, 131);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 17);
            this.label17.TabIndex = 10;
            this.label17.Text = "Ime tvrtke:";
            // 
            // xTvrtka
            // 
            this.xTvrtka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xTvrtka.AutoSize = true;
            this.xTvrtka.BackColor = System.Drawing.Color.Transparent;
            this.xTvrtka.Font = new System.Drawing.Font("Tahoma", 15F);
            this.xTvrtka.ForeColor = System.Drawing.Color.Red;
            this.xTvrtka.Location = new System.Drawing.Point(404, 129);
            this.xTvrtka.Margin = new System.Windows.Forms.Padding(0);
            this.xTvrtka.Name = "xTvrtka";
            this.xTvrtka.Size = new System.Drawing.Size(21, 24);
            this.xTvrtka.TabIndex = 12;
            this.xTvrtka.Text = "*";
            // 
            // txtTvrtka
            // 
            this.txtTvrtka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTvrtka.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTvrtka.Location = new System.Drawing.Point(152, 127);
            this.txtTvrtka.MaxLength = 100;
            this.txtTvrtka.Name = "txtTvrtka";
            this.txtTvrtka.Size = new System.Drawing.Size(249, 24);
            this.txtTvrtka.TabIndex = 11;
            this.txtTvrtka.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTvrtka.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbGrad
            // 
            this.cbGrad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(152, 217);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(224, 24);
            this.cbGrad.TabIndex = 20;
            this.cbGrad.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbGrad.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(419, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 109;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label18.Location = new System.Drawing.Point(401, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 17);
            this.label18.TabIndex = 4;
            this.label18.Text = "Traži partnera";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(286, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 55);
            this.button1.TabIndex = 3;
            this.button1.Text = "Novi unos";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(374, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 27);
            this.button3.TabIndex = 18;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(374, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 27);
            this.button2.TabIndex = 21;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label19.Location = new System.Drawing.Point(88, 190);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 17);
            this.label19.TabIndex = 16;
            this.label19.Text = "Država:";
            // 
            // cbDrzava
            // 
            this.cbDrzava.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbDrzava.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDrzava.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDrzava.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrzava.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbDrzava.FormattingEnabled = true;
            this.cbDrzava.Location = new System.Drawing.Point(152, 186);
            this.cbDrzava.Name = "cbDrzava";
            this.cbDrzava.Size = new System.Drawing.Size(224, 24);
            this.cbDrzava.TabIndex = 17;
            this.cbDrzava.SelectedIndexChanged += new System.EventHandler(this.cbDrzava_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabControl1.Location = new System.Drawing.Point(-4, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(508, 512);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtKarticaKupca);
            this.tabPage1.Controls.Add(this.txtMob);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.txtSifra);
            this.tabPage1.Controls.Add(this.txtIme);
            this.tabPage1.Controls.Add(this.txtPrezime);
            this.tabPage1.Controls.Add(this.txtEmail);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.xGrad);
            this.tabPage1.Controls.Add(this.cbGrad);
            this.tabPage1.Controls.Add(this.txtAdresa);
            this.tabPage1.Controls.Add(this.cbDrzava);
            this.tabPage1.Controls.Add(this.txtTel);
            this.tabPage1.Controls.Add(this.txtOib);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.xTvrtka);
            this.tabPage1.Controls.Add(this.xAdresa);
            this.tabPage1.Controls.Add(this.txtTvrtka);
            this.tabPage1.Controls.Add(this.xOIB);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtBrojKartice);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.rbPrivatni);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.rbPoslovni);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Osnovno";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(66, 371);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "Kar. kupca:";
            // 
            // txtKarticaKupca
            // 
            this.txtKarticaKupca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKarticaKupca.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtKarticaKupca.Location = new System.Drawing.Point(152, 367);
            this.txtKarticaKupca.MaxLength = 29;
            this.txtKarticaKupca.Name = "txtKarticaKupca";
            this.txtKarticaKupca.Size = new System.Drawing.Size(249, 24);
            this.txtKarticaKupca.TabIndex = 33;
            // 
            // txtMob
            // 
            this.txtMob.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMob.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMob.Location = new System.Drawing.Point(152, 307);
            this.txtMob.MaxLength = 29;
            this.txtMob.Name = "txtMob";
            this.txtMob.Size = new System.Drawing.Size(249, 24);
            this.txtMob.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(105, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 17);
            this.label9.TabIndex = 28;
            this.label9.Text = "Mob:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.txtHormonalniNadomjestak);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtKontracepcija);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.cmbSpol);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.dtpDatumRodenja);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtTrudnoca);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtNapomena);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(500, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ostalo";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label20.Location = new System.Drawing.Point(34, 376);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(112, 42);
            this.label20.TabIndex = 10;
            this.label20.Text = "Hormonalni:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtHormonalniNadomjestak
            // 
            this.txtHormonalniNadomjestak.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtHormonalniNadomjestak.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtHormonalniNadomjestak.Location = new System.Drawing.Point(152, 373);
            this.txtHormonalniNadomjestak.Multiline = true;
            this.txtHormonalniNadomjestak.Name = "txtHormonalniNadomjestak";
            this.txtHormonalniNadomjestak.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtHormonalniNadomjestak.Size = new System.Drawing.Size(249, 99);
            this.txtHormonalniNadomjestak.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(50, 271);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 17);
            this.label14.TabIndex = 8;
            this.label14.Text = "Kontracepcija:";
            // 
            // txtKontracepcija
            // 
            this.txtKontracepcija.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKontracepcija.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtKontracepcija.Location = new System.Drawing.Point(152, 268);
            this.txtKontracepcija.Multiline = true;
            this.txtKontracepcija.Name = "txtKontracepcija";
            this.txtKontracepcija.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtKontracepcija.Size = new System.Drawing.Size(249, 99);
            this.txtKontracepcija.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(107, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 17);
            this.label13.TabIndex = 4;
            this.label13.Text = "Spol:";
            // 
            // cmbSpol
            // 
            this.cmbSpol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpol.FormattingEnabled = true;
            this.cmbSpol.Items.AddRange(new object[] {
            "Drugo",
            "Muško",
            "Žensko"});
            this.cmbSpol.Location = new System.Drawing.Point(152, 129);
            this.cmbSpol.Name = "cmbSpol";
            this.cmbSpol.Size = new System.Drawing.Size(249, 28);
            this.cmbSpol.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(78, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Dat. rođ.:";
            // 
            // dtpDatumRodenja
            // 
            this.dtpDatumRodenja.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumRodenja.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumRodenja.Location = new System.Drawing.Point(152, 97);
            this.dtpDatumRodenja.Name = "dtpDatumRodenja";
            this.dtpDatumRodenja.Size = new System.Drawing.Size(249, 26);
            this.dtpDatumRodenja.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(74, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "Trudnoća:";
            // 
            // txtTrudnoca
            // 
            this.txtTrudnoca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTrudnoca.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTrudnoca.Location = new System.Drawing.Point(152, 163);
            this.txtTrudnoca.Multiline = true;
            this.txtTrudnoca.Name = "txtTrudnoca";
            this.txtTrudnoca.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtTrudnoca.Size = new System.Drawing.Size(249, 99);
            this.txtTrudnoca.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(67, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Napomena:";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNapomena.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNapomena.Location = new System.Drawing.Point(152, 11);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtNapomena.Size = new System.Drawing.Size(249, 80);
            this.txtNapomena.TabIndex = 1;
            // 
            // frmAddPartners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(500, 643);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAddPartners";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj novog partnera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddPartners_FormClosing);
            this.Load += new System.EventHandler(this.frmAddPartners_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label xAdresa;
        private System.Windows.Forms.Label xGrad;
        private System.Windows.Forms.RadioButton rbPoslovni;
        private System.Windows.Forms.RadioButton rbPrivatni;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojKartice;
        private System.Windows.Forms.TextBox txtOib;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label xOIB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label xTvrtka;
        private System.Windows.Forms.TextBox txtTvrtka;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbDrzava;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKarticaKupca;
        private System.Windows.Forms.TextBox txtMob;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbSpol;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpDatumRodenja;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTrudnoca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtKontracepcija;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtHormonalniNadomjestak;
    }
}