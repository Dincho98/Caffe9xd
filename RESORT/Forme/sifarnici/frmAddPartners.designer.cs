namespace RESORT.Forme.sifarnici
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
            this.txtMob = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBodovi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPopust = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chbAktivan = new System.Windows.Forms.CheckBox();
            this.chbPrimanjeLetaka = new System.Windows.Forms.CheckBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.txtZupanija = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDjelatnost = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.rbPoslovni = new System.Windows.Forms.RadioButton();
            this.rbPrivatni = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojKartice = new System.Windows.Forms.TextBox();
            this.txtOib = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTvrtka = new System.Windows.Forms.TextBox();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.cbDrzava = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIme
            // 
            this.txtIme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIme.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIme.Location = new System.Drawing.Point(108, 46);
            this.txtIme.MaxLength = 29;
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(249, 24);
            this.txtIme.TabIndex = 1;
            this.txtIme.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIme.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(30, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ime:";
            // 
            // txtPrezime
            // 
            this.txtPrezime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPrezime.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPrezime.Location = new System.Drawing.Point(108, 72);
            this.txtPrezime.MaxLength = 29;
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(249, 24);
            this.txtPrezime.TabIndex = 2;
            this.txtPrezime.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPrezime.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(30, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prezime:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.Location = new System.Drawing.Point(108, 98);
            this.txtEmail.MaxLength = 29;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(249, 24);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(30, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email:";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAdresa.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAdresa.Location = new System.Drawing.Point(101, 85);
            this.txtAdresa.MaxLength = 29;
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(271, 24);
            this.txtAdresa.TabIndex = 5;
            this.txtAdresa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtAdresa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(21, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Adresa:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(21, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Grad:";
            // 
            // txtTel
            // 
            this.txtTel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTel.Location = new System.Drawing.Point(107, 305);
            this.txtTel.MaxLength = 29;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(250, 24);
            this.txtTel.TabIndex = 7;
            this.txtTel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(29, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tel:";
            // 
            // txtMob
            // 
            this.txtMob.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMob.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMob.Location = new System.Drawing.Point(108, 124);
            this.txtMob.MaxLength = 29;
            this.txtMob.Name = "txtMob";
            this.txtMob.Size = new System.Drawing.Size(249, 24);
            this.txtMob.TabIndex = 9;
            this.txtMob.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtMob.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(30, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Mob:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(27, 297);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Napomena:";
            // 
            // txtBodovi
            // 
            this.txtBodovi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBodovi.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBodovi.Location = new System.Drawing.Point(108, 175);
            this.txtBodovi.Name = "txtBodovi";
            this.txtBodovi.Size = new System.Drawing.Size(249, 24);
            this.txtBodovi.TabIndex = 11;
            this.txtBodovi.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBodovi.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(30, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Bodovi:";
            // 
            // txtPopust
            // 
            this.txtPopust.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPopust.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPopust.Location = new System.Drawing.Point(108, 201);
            this.txtPopust.Name = "txtPopust";
            this.txtPopust.Size = new System.Drawing.Size(249, 24);
            this.txtPopust.TabIndex = 12;
            this.txtPopust.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPopust.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(30, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "Popust:";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(30, 230);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Županija:";
            // 
            // chbAktivan
            // 
            this.chbAktivan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbAktivan.AutoSize = true;
            this.chbAktivan.BackColor = System.Drawing.Color.Transparent;
            this.chbAktivan.Checked = true;
            this.chbAktivan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAktivan.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chbAktivan.Location = new System.Drawing.Point(107, 343);
            this.chbAktivan.Name = "chbAktivan";
            this.chbAktivan.Size = new System.Drawing.Size(72, 21);
            this.chbAktivan.TabIndex = 16;
            this.chbAktivan.Text = "Aktivan";
            this.chbAktivan.UseVisualStyleBackColor = false;
            // 
            // chbPrimanjeLetaka
            // 
            this.chbPrimanjeLetaka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chbPrimanjeLetaka.AutoSize = true;
            this.chbPrimanjeLetaka.BackColor = System.Drawing.Color.Transparent;
            this.chbPrimanjeLetaka.Checked = true;
            this.chbPrimanjeLetaka.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPrimanjeLetaka.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chbPrimanjeLetaka.Location = new System.Drawing.Point(223, 343);
            this.chbPrimanjeLetaka.Name = "chbPrimanjeLetaka";
            this.chbPrimanjeLetaka.Size = new System.Drawing.Size(131, 21);
            this.chbPrimanjeLetaka.TabIndex = 15;
            this.chbPrimanjeLetaka.Text = "Primanje reklama";
            this.chbPrimanjeLetaka.UseVisualStyleBackColor = false;
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(29, 317);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(395, 105);
            this.rtbNapomena.TabIndex = 17;
            // 
            // txtZupanija
            // 
            this.txtZupanija.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtZupanija.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtZupanija.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtZupanija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtZupanija.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtZupanija.FormattingEnabled = true;
            this.txtZupanija.Location = new System.Drawing.Point(108, 227);
            this.txtZupanija.Name = "txtZupanija";
            this.txtZupanija.Size = new System.Drawing.Size(249, 24);
            this.txtZupanija.TabIndex = 13;
            this.txtZupanija.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtZupanija.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(30, 255);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Djelatnost:";
            // 
            // txtDjelatnost
            // 
            this.txtDjelatnost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDjelatnost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDjelatnost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtDjelatnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDjelatnost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDjelatnost.FormattingEnabled = true;
            this.txtDjelatnost.Location = new System.Drawing.Point(108, 253);
            this.txtDjelatnost.Name = "txtDjelatnost";
            this.txtDjelatnost.Size = new System.Drawing.Size(249, 24);
            this.txtDjelatnost.TabIndex = 14;
            this.txtDjelatnost.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtDjelatnost.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(22, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(228, 26);
            this.label14.TabIndex = 100;
            this.label14.Text = "Unos novog partnera";
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSpremi.Location = new System.Drawing.Point(297, 430);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(127, 33);
            this.btnSpremi.TabIndex = 20;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnOdustani.Location = new System.Drawing.Point(169, 430);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(122, 33);
            this.btnOdustani.TabIndex = 19;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSifra.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSifra.Location = new System.Drawing.Point(102, 26);
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
            this.label15.Location = new System.Drawing.Point(22, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 17);
            this.label15.TabIndex = 2;
            this.label15.Text = "ID:";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label22.Location = new System.Drawing.Point(30, 152);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 17);
            this.label22.TabIndex = 2;
            this.label22.Text = "Dat.Rođ:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpDatum.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.dtpDatum.Location = new System.Drawing.Point(108, 150);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(249, 23);
            this.dtpDatum.TabIndex = 10;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rbPoslovni
            // 
            this.rbPoslovni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbPoslovni.AutoSize = true;
            this.rbPoslovni.BackColor = System.Drawing.Color.Transparent;
            this.rbPoslovni.Checked = true;
            this.rbPoslovni.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rbPoslovni.Location = new System.Drawing.Point(203, 27);
            this.rbPoslovni.Name = "rbPoslovni";
            this.rbPoslovni.Size = new System.Drawing.Size(76, 21);
            this.rbPoslovni.TabIndex = 21;
            this.rbPoslovni.TabStop = true;
            this.rbPoslovni.Text = "Poslovni";
            this.rbPoslovni.UseVisualStyleBackColor = false;
            // 
            // rbPrivatni
            // 
            this.rbPrivatni.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbPrivatni.AutoSize = true;
            this.rbPrivatni.BackColor = System.Drawing.Color.Transparent;
            this.rbPrivatni.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rbPrivatni.Location = new System.Drawing.Point(283, 26);
            this.rbPrivatni.Name = "rbPrivatni";
            this.rbPrivatni.Size = new System.Drawing.Size(71, 21);
            this.rbPrivatni.TabIndex = 22;
            this.rbPrivatni.Text = "Privatni";
            this.rbPrivatni.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.Location = new System.Drawing.Point(29, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 17);
            this.label11.TabIndex = 105;
            this.label11.Text = "Br. kartice:";
            // 
            // txtBrojKartice
            // 
            this.txtBrojKartice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBrojKartice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojKartice.Location = new System.Drawing.Point(108, 279);
            this.txtBrojKartice.Name = "txtBrojKartice";
            this.txtBrojKartice.ReadOnly = true;
            this.txtBrojKartice.Size = new System.Drawing.Size(249, 24);
            this.txtBrojKartice.TabIndex = 104;
            // 
            // txtOib
            // 
            this.txtOib.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOib.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOib.Location = new System.Drawing.Point(102, 171);
            this.txtOib.MaxLength = 11;
            this.txtOib.Name = "txtOib";
            this.txtOib.Size = new System.Drawing.Size(271, 24);
            this.txtOib.TabIndex = 8;
            this.txtOib.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtOib.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label16.Location = new System.Drawing.Point(22, 171);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 17);
            this.label16.TabIndex = 2;
            this.label16.Text = "OIB:";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label17.Location = new System.Drawing.Point(21, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 17);
            this.label17.TabIndex = 107;
            this.label17.Text = "Ime tvrtke:";
            // 
            // txtTvrtka
            // 
            this.txtTvrtka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTvrtka.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTvrtka.Location = new System.Drawing.Point(101, 56);
            this.txtTvrtka.MaxLength = 100;
            this.txtTvrtka.Name = "txtTvrtka";
            this.txtTvrtka.Size = new System.Drawing.Size(271, 24);
            this.txtTvrtka.TabIndex = 3;
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
            this.cbGrad.Location = new System.Drawing.Point(102, 141);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(237, 24);
            this.cbGrad.TabIndex = 6;
            this.cbGrad.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbGrad.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(765, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 109;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(762, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 110;
            this.label18.Text = "Traži partnera";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(29, 430);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 33);
            this.button1.TabIndex = 18;
            this.button1.Text = "Novi unos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.cbDrzava);
            this.groupBox1.Controls.Add(this.txtOib);
            this.groupBox1.Controls.Add(this.txtSifra);
            this.groupBox1.Controls.Add(this.txtAdresa);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtTvrtka);
            this.groupBox1.Controls.Add(this.rbPrivatni);
            this.groupBox1.Controls.Add(this.rbPoslovni);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbGrad);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(29, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 207);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Obavezan unos";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Controls.Add(this.chbAktivan);
            this.groupBox2.Controls.Add(this.chbPrimanjeLetaka);
            this.groupBox2.Controls.Add(this.txtIme);
            this.groupBox2.Controls.Add(this.txtTel);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtPrezime);
            this.groupBox2.Controls.Add(this.txtBrojKartice);
            this.groupBox2.Controls.Add(this.txtMob);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.txtBodovi);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPopust);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDjelatnost);
            this.groupBox2.Controls.Add(this.txtZupanija);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(442, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 377);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dodatni neobavezan unos";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(345, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 114;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label19.Location = new System.Drawing.Point(21, 116);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 17);
            this.label19.TabIndex = 112;
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
            this.cbDrzava.Location = new System.Drawing.Point(101, 113);
            this.cbDrzava.Name = "cbDrzava";
            this.cbDrzava.Size = new System.Drawing.Size(238, 24);
            this.cbDrzava.TabIndex = 113;
            this.cbDrzava.SelectedIndexChanged += new System.EventHandler(this.cbDrzava_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(345, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 23);
            this.button2.TabIndex = 115;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmAddPartners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(864, 485);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.rtbNapomena);
            this.Controls.Add(this.label8);
            this.Name = "frmAddPartners";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj novog partnera";
            this.Load += new System.EventHandler(this.frmAddPartners_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox txtMob;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBodovi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPopust;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chbAktivan;
        private System.Windows.Forms.CheckBox chbPrimanjeLetaka;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.ComboBox txtZupanija;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox txtDjelatnost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.RadioButton rbPoslovni;
        private System.Windows.Forms.RadioButton rbPrivatni;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojKartice;
        private System.Windows.Forms.TextBox txtOib;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTvrtka;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbDrzava;
    }
}