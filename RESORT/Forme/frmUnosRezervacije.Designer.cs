namespace RESORT.Forme
{
	partial class frmUnosRezervacije
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.imeprezime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_dolaska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_odlaska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_vrsta_gosta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_drzava = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_osobne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_putovnice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_agencija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tip_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_vrsta_usluge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dorucak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rucak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vecera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odrasli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.djeca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bebe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_unos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.txtBebe = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtDjeca = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtOdrasli = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBrojOsobne = new System.Windows.Forms.TextBox();
            this.txtBrojPutovnice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nuGodina = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtBrojDokumenta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtImePrezime = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbDrzava = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAvans = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbVecera = new System.Windows.Forms.CheckBox();
            this.chbRucak = new System.Windows.Forms.CheckBox();
            this.chbDorucak = new System.Windows.Forms.CheckBox();
            this.cbAgencija = new System.Windows.Forms.ComboBox();
            this.txtBrojSobe = new System.Windows.Forms.TextBox();
            this.cbVrsteGosta = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbVrstaUsluge = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbTipSoba = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbSoba = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpDatOdlaska = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpDatDolaska = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAvans = new System.Windows.Forms.TextBox();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnObrisiOznacenog = new System.Windows.Forms.Button();
            this.btnDodajNaPopis = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnUredi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            this.imeprezime,
            this.datum_dolaska,
            this.datum_odlaska,
            this.soba,
            this.avans,
            this.id_vrsta_gosta,
            this.id_drzava,
            this.broj_osobne,
            this.broj_putovnice,
            this.id_agencija,
            this.id_soba,
            this.id_tip_sobe,
            this.id_vrsta_usluge,
            this.dorucak,
            this.rucak,
            this.vecera,
            this.napomena,
            this.odrasli,
            this.djeca,
            this.bebe,
            this.id_unos});
            this.dgv.Location = new System.Drawing.Point(12, 319);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(980, 302);
            this.dgv.TabIndex = 5;
            // 
            // imeprezime
            // 
            this.imeprezime.HeaderText = "Ime i prezime";
            this.imeprezime.Name = "imeprezime";
            this.imeprezime.ReadOnly = true;
            // 
            // datum_dolaska
            // 
            this.datum_dolaska.HeaderText = "Datum dolaska";
            this.datum_dolaska.Name = "datum_dolaska";
            this.datum_dolaska.ReadOnly = true;
            // 
            // datum_odlaska
            // 
            this.datum_odlaska.HeaderText = "Datum odlaska";
            this.datum_odlaska.Name = "datum_odlaska";
            this.datum_odlaska.ReadOnly = true;
            // 
            // soba
            // 
            this.soba.HeaderText = "Soba";
            this.soba.Name = "soba";
            this.soba.ReadOnly = true;
            // 
            // avans
            // 
            this.avans.HeaderText = "Avans";
            this.avans.Name = "avans";
            this.avans.ReadOnly = true;
            // 
            // id_vrsta_gosta
            // 
            this.id_vrsta_gosta.HeaderText = "id_vrsta_gosta";
            this.id_vrsta_gosta.Name = "id_vrsta_gosta";
            this.id_vrsta_gosta.ReadOnly = true;
            this.id_vrsta_gosta.Visible = false;
            // 
            // id_drzava
            // 
            this.id_drzava.HeaderText = "id_drzava";
            this.id_drzava.Name = "id_drzava";
            this.id_drzava.ReadOnly = true;
            this.id_drzava.Visible = false;
            // 
            // broj_osobne
            // 
            this.broj_osobne.HeaderText = "broj_osobne";
            this.broj_osobne.Name = "broj_osobne";
            this.broj_osobne.ReadOnly = true;
            this.broj_osobne.Visible = false;
            // 
            // broj_putovnice
            // 
            this.broj_putovnice.HeaderText = "broj_putovnice";
            this.broj_putovnice.Name = "broj_putovnice";
            this.broj_putovnice.ReadOnly = true;
            this.broj_putovnice.Visible = false;
            // 
            // id_agencija
            // 
            this.id_agencija.HeaderText = "id_agencija";
            this.id_agencija.Name = "id_agencija";
            this.id_agencija.ReadOnly = true;
            this.id_agencija.Visible = false;
            // 
            // id_soba
            // 
            this.id_soba.HeaderText = "id_soba";
            this.id_soba.Name = "id_soba";
            this.id_soba.ReadOnly = true;
            this.id_soba.Visible = false;
            // 
            // id_tip_sobe
            // 
            this.id_tip_sobe.HeaderText = "id_tip_sobe";
            this.id_tip_sobe.Name = "id_tip_sobe";
            this.id_tip_sobe.ReadOnly = true;
            this.id_tip_sobe.Visible = false;
            // 
            // id_vrsta_usluge
            // 
            this.id_vrsta_usluge.HeaderText = "id_vrsta_usluge";
            this.id_vrsta_usluge.Name = "id_vrsta_usluge";
            this.id_vrsta_usluge.ReadOnly = true;
            this.id_vrsta_usluge.Visible = false;
            // 
            // dorucak
            // 
            this.dorucak.HeaderText = "dorucak";
            this.dorucak.Name = "dorucak";
            this.dorucak.ReadOnly = true;
            this.dorucak.Visible = false;
            // 
            // rucak
            // 
            this.rucak.HeaderText = "rucak";
            this.rucak.Name = "rucak";
            this.rucak.ReadOnly = true;
            this.rucak.Visible = false;
            // 
            // vecera
            // 
            this.vecera.HeaderText = "vecera";
            this.vecera.Name = "vecera";
            this.vecera.ReadOnly = true;
            this.vecera.Visible = false;
            // 
            // napomena
            // 
            this.napomena.HeaderText = "napomena";
            this.napomena.Name = "napomena";
            this.napomena.ReadOnly = true;
            this.napomena.Visible = false;
            // 
            // odrasli
            // 
            this.odrasli.HeaderText = "odrasli";
            this.odrasli.Name = "odrasli";
            this.odrasli.ReadOnly = true;
            this.odrasli.Visible = false;
            // 
            // djeca
            // 
            this.djeca.HeaderText = "djeca";
            this.djeca.Name = "djeca";
            this.djeca.ReadOnly = true;
            this.djeca.Visible = false;
            // 
            // bebe
            // 
            this.bebe.HeaderText = "bebe";
            this.bebe.Name = "bebe";
            this.bebe.ReadOnly = true;
            this.bebe.Visible = false;
            // 
            // id_unos
            // 
            this.id_unos.HeaderText = "id_unos";
            this.id_unos.Name = "id_unos";
            this.id_unos.ReadOnly = true;
            this.id_unos.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(9, 301);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "Gosti:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtNapomena);
            this.groupBox1.Controls.Add(this.txtBebe);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtDjeca);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtOdrasli);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBrojOsobne);
            this.groupBox1.Controls.Add(this.txtBrojPutovnice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtImePrezime);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbDrzava);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnAvans);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.cbAgencija);
            this.groupBox1.Controls.Add(this.txtBrojSobe);
            this.groupBox1.Controls.Add(this.cbVrsteGosta);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbVrstaUsluge);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbTipSoba);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbSoba);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dtpDatOdlaska);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtpDatDolaska);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAvans);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unos rezervacije za gosta";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNapomena.Location = new System.Drawing.Point(100, 204);
            this.txtNapomena.MaxLength = 250;
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(870, 38);
            this.txtNapomena.TabIndex = 8;
            this.txtNapomena.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtNapomena.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // txtBebe
            // 
            this.txtBebe.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBebe.Location = new System.Drawing.Point(903, 81);
            this.txtBebe.Name = "txtBebe";
            this.txtBebe.Size = new System.Drawing.Size(66, 24);
            this.txtBebe.TabIndex = 16;
            this.txtBebe.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtBebe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtBebe.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label21.Location = new System.Drawing.Point(900, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(43, 17);
            this.label21.TabIndex = 38;
            this.label21.Text = "Bebe:";
            // 
            // txtDjeca
            // 
            this.txtDjeca.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDjeca.Location = new System.Drawing.Point(828, 81);
            this.txtDjeca.Name = "txtDjeca";
            this.txtDjeca.Size = new System.Drawing.Size(67, 24);
            this.txtDjeca.TabIndex = 15;
            this.txtDjeca.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtDjeca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtDjeca.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label20.Location = new System.Drawing.Point(825, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 17);
            this.label20.TabIndex = 37;
            this.label20.Text = "Djeca:";
            // 
            // txtOdrasli
            // 
            this.txtOdrasli.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOdrasli.Location = new System.Drawing.Point(752, 81);
            this.txtOdrasli.Name = "txtOdrasli";
            this.txtOdrasli.Size = new System.Drawing.Size(68, 24);
            this.txtOdrasli.TabIndex = 14;
            this.txtOdrasli.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtOdrasli.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtOdrasli.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(749, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "Odrasli:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(335, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "Broj putovnice:";
            // 
            // txtBrojOsobne
            // 
            this.txtBrojOsobne.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojOsobne.Location = new System.Drawing.Point(444, 153);
            this.txtBrojOsobne.MaxLength = 50;
            this.txtBrojOsobne.Name = "txtBrojOsobne";
            this.txtBrojOsobne.Size = new System.Drawing.Size(179, 24);
            this.txtBrojOsobne.TabIndex = 12;
            this.txtBrojOsobne.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtBrojOsobne.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtBrojOsobne.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // txtBrojPutovnice
            // 
            this.txtBrojPutovnice.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojPutovnice.Location = new System.Drawing.Point(444, 178);
            this.txtBrojPutovnice.MaxLength = 50;
            this.txtBrojPutovnice.Name = "txtBrojPutovnice";
            this.txtBrojPutovnice.Size = new System.Drawing.Size(179, 24);
            this.txtBrojPutovnice.TabIndex = 13;
            this.txtBrojPutovnice.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtBrojPutovnice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtBrojPutovnice.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(335, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Broj osobne:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nuGodina);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtBrojDokumenta);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // nuGodina
            // 
            this.nuGodina.Location = new System.Drawing.Point(229, 18);
            this.nuGodina.Name = "nuGodina";
            this.nuGodina.Size = new System.Drawing.Size(77, 24);
            this.nuGodina.TabIndex = 2;
            this.nuGodina.ValueChanged += new System.EventHandler(this.nuGodina_ValueChanged);
            this.nuGodina.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.nuGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.nuGodina.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(3, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(89, 17);
            this.label18.TabIndex = 3;
            this.label18.Text = "Broj unosa:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(168, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Godina:";
            // 
            // txtBrojDokumenta
            // 
            this.txtBrojDokumenta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojDokumenta.Location = new System.Drawing.Point(92, 19);
            this.txtBrojDokumenta.Name = "txtBrojDokumenta";
            this.txtBrojDokumenta.Size = new System.Drawing.Size(66, 24);
            this.txtBrojDokumenta.TabIndex = 0;
            this.txtBrojDokumenta.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtBrojDokumenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtBrojDokumenta.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 26);
            this.button1.TabIndex = 19;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtImePrezime
            // 
            this.txtImePrezime.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtImePrezime.Location = new System.Drawing.Point(100, 79);
            this.txtImePrezime.MaxLength = 50;
            this.txtImePrezime.Name = "txtImePrezime";
            this.txtImePrezime.Size = new System.Drawing.Size(211, 24);
            this.txtImePrezime.TabIndex = 2;
            this.txtImePrezime.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtImePrezime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtImePrezime.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(5, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 17);
            this.label14.TabIndex = 21;
            this.label14.Text = "Ime i prezime:";
            // 
            // cbDrzava
            // 
            this.cbDrzava.FormattingEnabled = true;
            this.cbDrzava.Location = new System.Drawing.Point(444, 128);
            this.cbDrzava.Name = "cbDrzava";
            this.cbDrzava.Size = new System.Drawing.Size(149, 24);
            this.cbDrzava.TabIndex = 11;
            this.cbDrzava.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbDrzava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbDrzava.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(336, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "Dražava:";
            // 
            // btnAvans
            // 
            this.btnAvans.Location = new System.Drawing.Point(931, 130);
            this.btnAvans.Name = "btnAvans";
            this.btnAvans.Size = new System.Drawing.Size(39, 26);
            this.btnAvans.TabIndex = 20;
            this.btnAvans.Text = "...";
            this.btnAvans.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbVecera);
            this.groupBox3.Controls.Add(this.chbRucak);
            this.groupBox3.Controls.Add(this.chbDorucak);
            this.groupBox3.Location = new System.Drawing.Point(737, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 39);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // chbVecera
            // 
            this.chbVecera.AutoSize = true;
            this.chbVecera.Location = new System.Drawing.Point(161, 14);
            this.chbVecera.Name = "chbVecera";
            this.chbVecera.Size = new System.Drawing.Size(68, 21);
            this.chbVecera.TabIndex = 2;
            this.chbVecera.Text = "Večera";
            this.chbVecera.UseVisualStyleBackColor = true;
            // 
            // chbRucak
            // 
            this.chbRucak.AutoSize = true;
            this.chbRucak.Location = new System.Drawing.Point(89, 14);
            this.chbRucak.Name = "chbRucak";
            this.chbRucak.Size = new System.Drawing.Size(65, 21);
            this.chbRucak.TabIndex = 1;
            this.chbRucak.Text = "Ručak";
            this.chbRucak.UseVisualStyleBackColor = true;
            // 
            // chbDorucak
            // 
            this.chbDorucak.AutoSize = true;
            this.chbDorucak.Location = new System.Drawing.Point(6, 14);
            this.chbDorucak.Name = "chbDorucak";
            this.chbDorucak.Size = new System.Drawing.Size(79, 21);
            this.chbDorucak.TabIndex = 0;
            this.chbDorucak.Text = "Doručak";
            this.chbDorucak.UseVisualStyleBackColor = true;
            // 
            // cbAgencija
            // 
            this.cbAgencija.FormattingEnabled = true;
            this.cbAgencija.Location = new System.Drawing.Point(100, 129);
            this.cbAgencija.Name = "cbAgencija";
            this.cbAgencija.Size = new System.Drawing.Size(211, 24);
            this.cbAgencija.TabIndex = 4;
            this.cbAgencija.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbAgencija.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbAgencija.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // txtBrojSobe
            // 
            this.txtBrojSobe.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBrojSobe.Location = new System.Drawing.Point(100, 179);
            this.txtBrojSobe.Name = "txtBrojSobe";
            this.txtBrojSobe.Size = new System.Drawing.Size(53, 24);
            this.txtBrojSobe.TabIndex = 6;
            this.txtBrojSobe.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtBrojSobe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtBrojSobe.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // cbVrsteGosta
            // 
            this.cbVrsteGosta.FormattingEnabled = true;
            this.cbVrsteGosta.Location = new System.Drawing.Point(100, 104);
            this.cbVrsteGosta.MaxLength = 150;
            this.cbVrsteGosta.Name = "cbVrsteGosta";
            this.cbVrsteGosta.Size = new System.Drawing.Size(211, 24);
            this.cbVrsteGosta.TabIndex = 3;
            this.cbVrsteGosta.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbVrsteGosta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbVrsteGosta.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label17.Location = new System.Drawing.Point(649, 172);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 17);
            this.label17.TabIndex = 35;
            this.label17.Text = "Obroci:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label16.Location = new System.Drawing.Point(649, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "Avans:";
            // 
            // cbVrstaUsluge
            // 
            this.cbVrstaUsluge.FormattingEnabled = true;
            this.cbVrstaUsluge.Location = new System.Drawing.Point(752, 106);
            this.cbVrstaUsluge.Name = "cbVrstaUsluge";
            this.cbVrstaUsluge.Size = new System.Drawing.Size(217, 24);
            this.cbVrstaUsluge.TabIndex = 17;
            this.cbVrstaUsluge.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbVrstaUsluge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbVrstaUsluge.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label22.Location = new System.Drawing.Point(649, 61);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 17);
            this.label22.TabIndex = 32;
            this.label22.Text = "Vrsta gostiju:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(649, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 17);
            this.label13.TabIndex = 33;
            this.label13.Text = "Vrsta usluge:";
            // 
            // cbTipSoba
            // 
            this.cbTipSoba.FormattingEnabled = true;
            this.cbTipSoba.Location = new System.Drawing.Point(100, 154);
            this.cbTipSoba.Name = "cbTipSoba";
            this.cbTipSoba.Size = new System.Drawing.Size(211, 24);
            this.cbTipSoba.TabIndex = 5;
            this.cbTipSoba.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbTipSoba.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbTipSoba.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(5, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Tip sobe:";
            // 
            // cbSoba
            // 
            this.cbSoba.FormattingEnabled = true;
            this.cbSoba.Location = new System.Drawing.Point(154, 179);
            this.cbSoba.Name = "cbSoba";
            this.cbSoba.Size = new System.Drawing.Size(157, 24);
            this.cbSoba.TabIndex = 7;
            this.cbSoba.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.cbSoba.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.cbSoba.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(5, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Napomena:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.Location = new System.Drawing.Point(5, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "Soba:";
            // 
            // dtpDatOdlaska
            // 
            this.dtpDatOdlaska.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatOdlaska.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpDatOdlaska.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDatOdlaska.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatOdlaska.Location = new System.Drawing.Point(444, 103);
            this.dtpDatOdlaska.Name = "dtpDatOdlaska";
            this.dtpDatOdlaska.Size = new System.Drawing.Size(179, 24);
            this.dtpDatOdlaska.TabIndex = 10;
            this.dtpDatOdlaska.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.dtpDatOdlaska.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.dtpDatOdlaska.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(336, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Datum odlaska:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(5, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Agencija:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(5, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Vrsta gosta:";
            // 
            // dtpDatDolaska
            // 
            this.dtpDatDolaska.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatDolaska.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpDatDolaska.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDatDolaska.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatDolaska.Location = new System.Drawing.Point(444, 78);
            this.dtpDatDolaska.Name = "dtpDatDolaska";
            this.dtpDatDolaska.Size = new System.Drawing.Size(179, 24);
            this.dtpDatDolaska.TabIndex = 9;
            this.dtpDatDolaska.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.dtpDatDolaska.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.dtpDatDolaska.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(336, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Datum dolaska:";
            // 
            // txtAvans
            // 
            this.txtAvans.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAvans.Location = new System.Drawing.Point(752, 131);
            this.txtAvans.Name = "txtAvans";
            this.txtAvans.Size = new System.Drawing.Size(179, 24);
            this.txtAvans.TabIndex = 18;
            this.txtAvans.Enter += new System.EventHandler(this.txtImePrezime_Enter);
            this.txtAvans.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            this.txtAvans.Leave += new System.EventHandler(this.txtImePrezime_Leave);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremi.Location = new System.Drawing.Point(834, 269);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(157, 26);
            this.btnSpremi.TabIndex = 3;
            this.btnSpremi.Text = "Spremi unose";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnObrisiOznacenog
            // 
            this.btnObrisiOznacenog.Location = new System.Drawing.Point(175, 269);
            this.btnObrisiOznacenog.Name = "btnObrisiOznacenog";
            this.btnObrisiOznacenog.Size = new System.Drawing.Size(157, 26);
            this.btnObrisiOznacenog.TabIndex = 2;
            this.btnObrisiOznacenog.Text = "Obriši označenog";
            this.btnObrisiOznacenog.UseVisualStyleBackColor = true;
            this.btnObrisiOznacenog.Click += new System.EventHandler(this.btnObrisiOznacenog_Click);
            // 
            // btnDodajNaPopis
            // 
            this.btnDodajNaPopis.Location = new System.Drawing.Point(13, 269);
            this.btnDodajNaPopis.Name = "btnDodajNaPopis";
            this.btnDodajNaPopis.Size = new System.Drawing.Size(157, 26);
            this.btnDodajNaPopis.TabIndex = 1;
            this.btnDodajNaPopis.Text = "Dodaj na popis";
            this.btnDodajNaPopis.UseVisualStyleBackColor = true;
            this.btnDodajNaPopis.Click += new System.EventHandler(this.btnDodajNaPopis_Click);
            // 
            // btnUredi
            // 
            this.btnUredi.Location = new System.Drawing.Point(338, 269);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(157, 26);
            this.btnUredi.TabIndex = 2;
            this.btnUredi.Text = "Uredi označenog";
            this.btnUredi.UseVisualStyleBackColor = true;
            this.btnUredi.Click += new System.EventHandler(this.btnUredi_Click);
            // 
            // frmUnosRezervacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1006, 633);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnUredi);
            this.Controls.Add(this.btnObrisiOznacenog);
            this.Controls.Add(this.btnDodajNaPopis);
            this.Name = "frmUnosRezervacije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unos rezervacije";
            this.Load += new System.EventHandler(this.frmUnosRezervacije_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBebe;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtDjeca;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtOdrasli;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nuGodina;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBrojDokumenta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtImePrezime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbDrzava;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAvans;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chbVecera;
        private System.Windows.Forms.CheckBox chbRucak;
        private System.Windows.Forms.CheckBox chbDorucak;
        private System.Windows.Forms.ComboBox cbAgencija;
        private System.Windows.Forms.TextBox txtBrojSobe;
        private System.Windows.Forms.ComboBox cbVrsteGosta;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbVrstaUsluge;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbTipSoba;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbSoba;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpDatOdlaska;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBrojPutovnice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDatDolaska;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAvans;
        private System.Windows.Forms.TextBox txtBrojOsobne;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnObrisiOznacenog;
        private System.Windows.Forms.Button btnDodajNaPopis;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.DataGridViewTextBoxColumn imeprezime;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_dolaska;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_odlaska;
        private System.Windows.Forms.DataGridViewTextBoxColumn soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn avans;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_vrsta_gosta;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_drzava;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_osobne;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_putovnice;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_agencija;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tip_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_vrsta_usluge;
        private System.Windows.Forms.DataGridViewTextBoxColumn dorucak;
        private System.Windows.Forms.DataGridViewTextBoxColumn rucak;
        private System.Windows.Forms.DataGridViewTextBoxColumn vecera;
        private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn odrasli;
        private System.Windows.Forms.DataGridViewTextBoxColumn djeca;
        private System.Windows.Forms.DataGridViewTextBoxColumn bebe;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_unos;
	}
}