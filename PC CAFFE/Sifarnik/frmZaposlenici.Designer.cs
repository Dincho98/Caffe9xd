namespace PCPOS.Sifarnik
{
    partial class frmZaposlenici
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
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.cbDopustenje = new System.Windows.Forms.ComboBox();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prezime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oib = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobitel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaporka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dopustenje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_dopustenje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_zaposlenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kartica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOib = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMobitel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtZaporka = new System.Windows.Forms.TextBox();
            this.dtpRoden = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.txtKarticaCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbAktivnost
            // 
            this.chbAktivnost.AutoSize = true;
            this.chbAktivnost.BackColor = System.Drawing.Color.Transparent;
            this.chbAktivnost.Location = new System.Drawing.Point(728, 126);
            this.chbAktivnost.Name = "chbAktivnost";
            this.chbAktivnost.Size = new System.Drawing.Size(15, 14);
            this.chbAktivnost.TabIndex = 17;
            this.chbAktivnost.UseVisualStyleBackColor = false;
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.White;
            this.btnNovo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNovo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNovo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNovo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Location = new System.Drawing.Point(12, 172);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(146, 28);
            this.btnNovo.TabIndex = 15;
            this.btnNovo.Text = "Dodaj novog zaposlenika";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Location = new System.Drawing.Point(164, 172);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(146, 28);
            this.btnSpremi.TabIndex = 16;
            this.btnSpremi.Text = "Spremi promjene";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // cbDopustenje
            // 
            this.cbDopustenje.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDopustenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbDopustenje.FormattingEnabled = true;
            this.cbDopustenje.Location = new System.Drawing.Point(728, 15);
            this.cbDopustenje.Name = "cbDopustenje";
            this.cbDopustenje.Size = new System.Drawing.Size(219, 24);
            this.cbDopustenje.TabIndex = 8;
            // 
            // cbGrad
            // 
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(67, 63);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(250, 24);
            this.cbGrad.TabIndex = 2;
            // 
            // txtIme
            // 
            this.txtIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIme.Location = new System.Drawing.Point(67, 13);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(250, 23);
            this.txtIme.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(650, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Aktivnost:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Grad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Prezime:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ime:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.ime,
            this.prezime,
            this.grad,
            this.adresa,
            this.oib,
            this.tel,
            this.mobitel,
            this.email,
            this.roden,
            this.zaporka,
            this.dopustenje,
            this.aktivan,
            this.id_grad,
            this.id_dopustenje,
            this.id_zaposlenik,
            this.kartica});
            this.dgv.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.Location = new System.Drawing.Point(12, 206);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(960, 217);
            this.dgv.TabIndex = 7;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // ime
            // 
            this.ime.HeaderText = "Ime";
            this.ime.Name = "ime";
            this.ime.ReadOnly = true;
            // 
            // prezime
            // 
            this.prezime.HeaderText = "Prezime";
            this.prezime.Name = "prezime";
            this.prezime.ReadOnly = true;
            // 
            // grad
            // 
            this.grad.HeaderText = "Grad";
            this.grad.Name = "grad";
            this.grad.ReadOnly = true;
            // 
            // adresa
            // 
            this.adresa.HeaderText = "Adresa";
            this.adresa.Name = "adresa";
            this.adresa.ReadOnly = true;
            // 
            // oib
            // 
            this.oib.HeaderText = "OIB";
            this.oib.Name = "oib";
            this.oib.ReadOnly = true;
            // 
            // tel
            // 
            this.tel.HeaderText = "Telefon";
            this.tel.Name = "tel";
            this.tel.ReadOnly = true;
            // 
            // mobitel
            // 
            this.mobitel.HeaderText = "Mobitel";
            this.mobitel.Name = "mobitel";
            this.mobitel.ReadOnly = true;
            // 
            // email
            // 
            this.email.HeaderText = "E-mail";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // roden
            // 
            this.roden.HeaderText = "Rođen";
            this.roden.Name = "roden";
            this.roden.ReadOnly = true;
            // 
            // zaporka
            // 
            this.zaporka.HeaderText = "Zaporka";
            this.zaporka.Name = "zaporka";
            this.zaporka.ReadOnly = true;
            this.zaporka.Visible = false;
            // 
            // dopustenje
            // 
            this.dopustenje.HeaderText = "Dopuštenje";
            this.dopustenje.Name = "dopustenje";
            this.dopustenje.ReadOnly = true;
            // 
            // aktivan
            // 
            this.aktivan.HeaderText = "Aktivan";
            this.aktivan.Name = "aktivan";
            this.aktivan.ReadOnly = true;
            // 
            // id_grad
            // 
            this.id_grad.HeaderText = "id_grad";
            this.id_grad.Name = "id_grad";
            this.id_grad.ReadOnly = true;
            this.id_grad.Visible = false;
            // 
            // id_dopustenje
            // 
            this.id_dopustenje.HeaderText = "id_dopustenje";
            this.id_dopustenje.Name = "id_dopustenje";
            this.id_dopustenje.ReadOnly = true;
            this.id_dopustenje.Visible = false;
            // 
            // id_zaposlenik
            // 
            this.id_zaposlenik.HeaderText = "id_zaposlenik";
            this.id_zaposlenik.Name = "id_zaposlenik";
            this.id_zaposlenik.ReadOnly = true;
            this.id_zaposlenik.Visible = false;
            // 
            // kartica
            // 
            this.kartica.HeaderText = "kartica";
            this.kartica.Name = "kartica";
            this.kartica.ReadOnly = true;
            this.kartica.Visible = false;
            // 
            // txtPrezime
            // 
            this.txtPrezime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPrezime.Location = new System.Drawing.Point(67, 38);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(250, 23);
            this.txtPrezime.TabIndex = 1;
            // 
            // txtAdresa
            // 
            this.txtAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAdresa.Location = new System.Drawing.Point(67, 90);
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(250, 23);
            this.txtAdresa.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(8, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Adresa:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(333, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "OIB:";
            // 
            // txtOib
            // 
            this.txtOib.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOib.Location = new System.Drawing.Point(384, 12);
            this.txtOib.Name = "txtOib";
            this.txtOib.Size = new System.Drawing.Size(250, 23);
            this.txtOib.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(333, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "Mobitel:";
            // 
            // txtMobitel
            // 
            this.txtMobitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMobitel.Location = new System.Drawing.Point(384, 64);
            this.txtMobitel.Name = "txtMobitel";
            this.txtMobitel.Size = new System.Drawing.Size(250, 23);
            this.txtMobitel.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(333, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Telefon:";
            // 
            // txtTelefon
            // 
            this.txtTelefon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTelefon.Location = new System.Drawing.Point(384, 38);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(250, 23);
            this.txtTelefon.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(333, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "E-mail:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEmail.Location = new System.Drawing.Point(384, 89);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 23);
            this.txtEmail.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(648, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "Dopuštenje:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(648, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Rođen:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(648, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Zaporka:";
            // 
            // txtZaporka
            // 
            this.txtZaporka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtZaporka.Location = new System.Drawing.Point(728, 69);
            this.txtZaporka.Name = "txtZaporka";
            this.txtZaporka.PasswordChar = '*';
            this.txtZaporka.Size = new System.Drawing.Size(219, 23);
            this.txtZaporka.TabIndex = 10;
            // 
            // dtpRoden
            // 
            this.dtpRoden.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpRoden.Location = new System.Drawing.Point(728, 43);
            this.dtpRoden.Name = "dtpRoden";
            this.dtpRoden.Size = new System.Drawing.Size(219, 23);
            this.dtpRoden.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(15, 439);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(209, 17);
            this.label13.TabIndex = 10;
            this.label13.Text = "Traži zaposlenika prema imenu:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(19, 455);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 23);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Location = new System.Drawing.Point(316, 172);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(146, 28);
            this.btnOdustani.TabIndex = 16;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // txtKarticaCode
            // 
            this.txtKarticaCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKarticaCode.Location = new System.Drawing.Point(728, 94);
            this.txtKarticaCode.Name = "txtKarticaCode";
            this.txtKarticaCode.PasswordChar = '*';
            this.txtKarticaCode.Size = new System.Drawing.Size(219, 23);
            this.txtKarticaCode.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(649, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 15);
            this.label14.TabIndex = 19;
            this.label14.Text = "Kartica code:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtIme);
            this.panel1.Controls.Add(this.txtKarticaCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtpRoden);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.chbAktivnost);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbDopustenje);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbGrad);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtPrezime);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtZaporka);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtTelefon);
            this.panel1.Controls.Add(this.txtAdresa);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtOib);
            this.panel1.Controls.Add(this.txtMobitel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 154);
            this.panel1.TabIndex = 20;
            // 
            // frmZaposlenici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(984, 492);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgv);
            this.Name = "frmZaposlenici";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zaposlenici";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmZaposlenici_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbAktivnost;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.ComboBox cbDopustenje;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOib;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMobitel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtZaporka;
        private System.Windows.Forms.DateTimePicker dtpRoden;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnOdustani;
		private System.Windows.Forms.TextBox txtKarticaCode;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
		private System.Windows.Forms.DataGridViewTextBoxColumn ime;
		private System.Windows.Forms.DataGridViewTextBoxColumn prezime;
		private System.Windows.Forms.DataGridViewTextBoxColumn grad;
		private System.Windows.Forms.DataGridViewTextBoxColumn adresa;
		private System.Windows.Forms.DataGridViewTextBoxColumn oib;
		private System.Windows.Forms.DataGridViewTextBoxColumn tel;
		private System.Windows.Forms.DataGridViewTextBoxColumn mobitel;
		private System.Windows.Forms.DataGridViewTextBoxColumn email;
		private System.Windows.Forms.DataGridViewTextBoxColumn roden;
		private System.Windows.Forms.DataGridViewTextBoxColumn zaporka;
		private System.Windows.Forms.DataGridViewTextBoxColumn dopustenje;
		private System.Windows.Forms.DataGridViewTextBoxColumn aktivan;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_grad;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_dopustenje;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_zaposlenik;
		private System.Windows.Forms.DataGridViewTextBoxColumn kartica;
        private System.Windows.Forms.Panel panel1;
    }
}