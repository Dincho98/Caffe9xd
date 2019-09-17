namespace PCPOS.Caffe
{
    partial class frmCaffe
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaffe));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flpGrupe = new System.Windows.Forms.FlowLayoutPanel();
            this.flpArtikli = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBrojRac = new System.Windows.Forms.Label();
            this.lblnativGrupe = new System.Windows.Forms.Label();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.CBstolovi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUnos = new System.Windows.Forms.TextBox();
            this.lblUkupno = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblKupac = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.spLineDisplay = new System.IO.Ports.SerialPort(this.components);
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.txtKarticaKupca = new System.Windows.Forms.TextBox();
            this.timeKarticaKupca = new System.Windows.Forms.Timer(this.components);
            this.btnNapomena = new System.Windows.Forms.Button();
            this.btnPolaPola = new System.Windows.Forms.Button();
            this.btnKoristiKarticu = new System.Windows.Forms.Button();
            this.btnDodatak = new System.Windows.Forms.Button();
            this.btnOdustaniKartica = new System.Windows.Forms.Button();
            this.btnPopust = new System.Windows.Forms.Button();
            this.btnDesert = new System.Windows.Forms.Button();
            this.btnGlavnoJelo = new System.Windows.Forms.Button();
            this.btnPredjela = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btnZavrsiSmjenu = new System.Windows.Forms.Button();
            this.btnNarudzbe = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnOpcije = new System.Windows.Forms.Button();
            this.btnStolovi = new System.Windows.Forms.Button();
            this.btnNajProdavanije = new System.Windows.Forms.Button();
            this.btnHrana = new System.Windows.Forms.Button();
            this.btnOdjava = new System.Windows.Forms.Button();
            this.btnPice = new System.Windows.Forms.Button();
            this.btnRacunR1 = new System.Windows.Forms.Button();
            this.btnDodajNaStol = new System.Windows.Forms.Button();
            this.btnGotovina = new System.Windows.Forms.Button();
            this.btnOtpremnica = new System.Windows.Forms.Button();
            this.lblBeautyOsoba = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSat = new System.Windows.Forms.Label();
            this.lblZadnjiRac = new System.Windows.Forms.Label();
            this.lblKarticaUkupno = new System.Windows.Forms.Label();
            this.lblKarticaKorisnik = new System.Windows.Forms.Label();
            this.lblPrijavljen = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPostojeNeuspjeleFiskalizacije = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez_potrosnja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zakljucaj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polapola = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_podgrupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpGrupe
            // 
            resources.ApplyResources(this.flpGrupe, "flpGrupe");
            this.flpGrupe.BackColor = System.Drawing.Color.Silver;
            this.flpGrupe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpGrupe.Name = "flpGrupe";
            // 
            // flpArtikli
            // 
            resources.ApplyResources(this.flpArtikli, "flpArtikli");
            this.flpArtikli.BackColor = System.Drawing.Color.Silver;
            this.flpArtikli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpArtikli.Name = "flpArtikli";
            // 
            // lblBrojRac
            // 
            resources.ApplyResources(this.lblBrojRac, "lblBrojRac");
            this.lblBrojRac.BackColor = System.Drawing.Color.Transparent;
            this.lblBrojRac.Name = "lblBrojRac";
            // 
            // lblnativGrupe
            // 
            this.lblnativGrupe.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblnativGrupe, "lblnativGrupe");
            this.lblnativGrupe.Name = "lblnativGrupe";
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.AllowUserToResizeColumns = false;
            this.dgw.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dgw, "dgw");
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.naziv,
            this.kolicina,
            this.mpc,
            this.sifra,
            this.skladiste,
            this.porez,
            this.rabat,
            this.vpc,
            this.porez_potrosnja,
            this.zakljucaj,
            this.jelo,
            this.nbc,
            this.dod,
            this.polapola,
            this.id_podgrupa});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEnter);
            this.dgw.SelectionChanged += new System.EventHandler(this.dgw_SelectionChanged);
            this.dgw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // CBstolovi
            // 
            resources.ApplyResources(this.CBstolovi, "CBstolovi");
            this.CBstolovi.FormattingEnabled = true;
            this.CBstolovi.Name = "CBstolovi";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtUnos
            // 
            resources.ApplyResources(this.txtUnos, "txtUnos");
            this.txtUnos.Name = "txtUnos";
            this.txtUnos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // lblUkupno
            // 
            resources.ApplyResources(this.lblUkupno, "lblUkupno");
            this.lblUkupno.Name = "lblUkupno";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CBstolovi);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtUnos);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblKupac
            // 
            resources.ApplyResources(this.lblKupac, "lblKupac");
            this.lblKupac.BackColor = System.Drawing.Color.Transparent;
            this.lblKupac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblKupac.Name = "lblKupac";
            this.lblKupac.Click += new System.EventHandler(this.lblKupac_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtKarticaKupca
            // 
            resources.ApplyResources(this.txtKarticaKupca, "txtKarticaKupca");
            this.txtKarticaKupca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtKarticaKupca.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKarticaKupca.Name = "txtKarticaKupca";
            this.txtKarticaKupca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            this.txtKarticaKupca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKarticaKupca_KeyPress);
            // 
            // timeKarticaKupca
            // 
            this.timeKarticaKupca.Interval = 500;
            this.timeKarticaKupca.Tick += new System.EventHandler(this.timeKarticaKupca_Tick);
            // 
            // btnNapomena
            // 
            resources.ApplyResources(this.btnNapomena, "btnNapomena");
            this.btnNapomena.BackColor = System.Drawing.Color.White;
            this.btnNapomena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNapomena.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNapomena.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNapomena.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNapomena.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNapomena.Name = "btnNapomena";
            this.btnNapomena.Tag = "0";
            this.btnNapomena.UseVisualStyleBackColor = false;
            this.btnNapomena.Click += new System.EventHandler(this.btnNapomena_Click);
            // 
            // btnPolaPola
            // 
            resources.ApplyResources(this.btnPolaPola, "btnPolaPola");
            this.btnPolaPola.BackColor = System.Drawing.Color.White;
            this.btnPolaPola.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPolaPola.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPolaPola.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPolaPola.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPolaPola.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPolaPola.Name = "btnPolaPola";
            this.btnPolaPola.UseVisualStyleBackColor = false;
            this.btnPolaPola.Click += new System.EventHandler(this.btnPolaPola_Click);
            this.btnPolaPola.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnKoristiKarticu
            // 
            resources.ApplyResources(this.btnKoristiKarticu, "btnKoristiKarticu");
            this.btnKoristiKarticu.BackColor = System.Drawing.Color.White;
            this.btnKoristiKarticu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKoristiKarticu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnKoristiKarticu.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnKoristiKarticu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnKoristiKarticu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnKoristiKarticu.Name = "btnKoristiKarticu";
            this.btnKoristiKarticu.UseVisualStyleBackColor = false;
            this.btnKoristiKarticu.Click += new System.EventHandler(this.btnKoristiKarticu_Click);
            this.btnKoristiKarticu.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnDodatak
            // 
            resources.ApplyResources(this.btnDodatak, "btnDodatak");
            this.btnDodatak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodatak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodatak.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDodatak.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDodatak.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodatak.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodatak.ForeColor = System.Drawing.Color.White;
            this.btnDodatak.Name = "btnDodatak";
            this.btnDodatak.UseVisualStyleBackColor = false;
            this.btnDodatak.Click += new System.EventHandler(this.btnDodatak_Click);
            this.btnDodatak.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnOdustaniKartica
            // 
            resources.ApplyResources(this.btnOdustaniKartica, "btnOdustaniKartica");
            this.btnOdustaniKartica.BackColor = System.Drawing.Color.White;
            this.btnOdustaniKartica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustaniKartica.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustaniKartica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdustaniKartica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustaniKartica.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustaniKartica.Name = "btnOdustaniKartica";
            this.btnOdustaniKartica.UseVisualStyleBackColor = false;
            this.btnOdustaniKartica.Click += new System.EventHandler(this.btnOdustaniKartica_Click);
            this.btnOdustaniKartica.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnPopust
            // 
            resources.ApplyResources(this.btnPopust, "btnPopust");
            this.btnPopust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPopust.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPopust.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPopust.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPopust.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPopust.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPopust.ForeColor = System.Drawing.Color.White;
            this.btnPopust.Name = "btnPopust";
            this.btnPopust.UseVisualStyleBackColor = false;
            this.btnPopust.Click += new System.EventHandler(this.button1_Click_2);
            this.btnPopust.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnDesert
            // 
            resources.ApplyResources(this.btnDesert, "btnDesert");
            this.btnDesert.BackColor = System.Drawing.Color.White;
            this.btnDesert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDesert.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDesert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDesert.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDesert.Name = "btnDesert";
            this.btnDesert.UseVisualStyleBackColor = false;
            this.btnDesert.Click += new System.EventHandler(this.btnDesert_Click);
            this.btnDesert.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnGlavnoJelo
            // 
            resources.ApplyResources(this.btnGlavnoJelo, "btnGlavnoJelo");
            this.btnGlavnoJelo.BackColor = System.Drawing.Color.White;
            this.btnGlavnoJelo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGlavnoJelo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnGlavnoJelo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnGlavnoJelo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnGlavnoJelo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnGlavnoJelo.Name = "btnGlavnoJelo";
            this.btnGlavnoJelo.UseVisualStyleBackColor = false;
            this.btnGlavnoJelo.Click += new System.EventHandler(this.btnGlavnoJelo_Click);
            this.btnGlavnoJelo.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnPredjela
            // 
            resources.ApplyResources(this.btnPredjela, "btnPredjela");
            this.btnPredjela.BackColor = System.Drawing.Color.White;
            this.btnPredjela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPredjela.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPredjela.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPredjela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPredjela.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPredjela.Name = "btnPredjela";
            this.btnPredjela.UseVisualStyleBackColor = false;
            this.btnPredjela.Click += new System.EventHandler(this.btnPredjela_Click);
            this.btnPredjela.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.button1_Click_1);
            this.btnDelete.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // button10
            // 
            resources.ApplyResources(this.button10, "button10");
            this.button10.BackColor = System.Drawing.Color.White;
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button10.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button10.Name = "button10";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.Enter += new System.EventHandler(this.btnPice_Enter);
            this.button10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnZavrsiSmjenu
            // 
            resources.ApplyResources(this.btnZavrsiSmjenu, "btnZavrsiSmjenu");
            this.btnZavrsiSmjenu.BackColor = System.Drawing.Color.White;
            this.btnZavrsiSmjenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZavrsiSmjenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnZavrsiSmjenu.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnZavrsiSmjenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnZavrsiSmjenu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnZavrsiSmjenu.Name = "btnZavrsiSmjenu";
            this.btnZavrsiSmjenu.UseVisualStyleBackColor = false;
            this.btnZavrsiSmjenu.Click += new System.EventHandler(this.btnZavrsiSmjenu_Click);
            this.btnZavrsiSmjenu.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnZavrsiSmjenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnNarudzbe
            // 
            resources.ApplyResources(this.btnNarudzbe, "btnNarudzbe");
            this.btnNarudzbe.BackColor = System.Drawing.Color.White;
            this.btnNarudzbe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNarudzbe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNarudzbe.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNarudzbe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNarudzbe.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNarudzbe.Name = "btnNarudzbe";
            this.btnNarudzbe.UseVisualStyleBackColor = false;
            this.btnNarudzbe.Click += new System.EventHandler(this.btnNarudzbe_Click);
            this.btnNarudzbe.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnNarudzbe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnOdustani
            // 
            resources.ApplyResources(this.btnOdustani, "btnOdustani");
            this.btnOdustani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.ForeColor = System.Drawing.Color.White;
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            this.btnOdustani.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnOdustani.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnOpcije
            // 
            resources.ApplyResources(this.btnOpcije, "btnOpcije");
            this.btnOpcije.BackColor = System.Drawing.Color.White;
            this.btnOpcije.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpcije.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOpcije.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOpcije.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOpcije.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOpcije.Name = "btnOpcije";
            this.btnOpcije.UseVisualStyleBackColor = false;
            this.btnOpcije.Click += new System.EventHandler(this.button9_Click);
            this.btnOpcije.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnOpcije.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnStolovi
            // 
            resources.ApplyResources(this.btnStolovi, "btnStolovi");
            this.btnStolovi.BackColor = System.Drawing.Color.White;
            this.btnStolovi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStolovi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnStolovi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnStolovi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnStolovi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnStolovi.Name = "btnStolovi";
            this.btnStolovi.UseVisualStyleBackColor = false;
            this.btnStolovi.Click += new System.EventHandler(this.button8_Click);
            this.btnStolovi.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnStolovi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnNajProdavanije
            // 
            this.btnNajProdavanije.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnNajProdavanije, "btnNajProdavanije");
            this.btnNajProdavanije.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNajProdavanije.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNajProdavanije.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNajProdavanije.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNajProdavanije.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNajProdavanije.Name = "btnNajProdavanije";
            this.btnNajProdavanije.Tag = "-1";
            this.btnNajProdavanije.UseVisualStyleBackColor = false;
            this.btnNajProdavanije.Click += new System.EventHandler(this.btnPice_Click);
            this.btnNajProdavanije.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnNajProdavanije.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnHrana
            // 
            this.btnHrana.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnHrana, "btnHrana");
            this.btnHrana.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHrana.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnHrana.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnHrana.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnHrana.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnHrana.Name = "btnHrana";
            this.btnHrana.Tag = "2";
            this.btnHrana.UseVisualStyleBackColor = false;
            this.btnHrana.Click += new System.EventHandler(this.btnPice_Click);
            this.btnHrana.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnHrana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnOdjava
            // 
            resources.ApplyResources(this.btnOdjava, "btnOdjava");
            this.btnOdjava.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdjava.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdjava.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOdjava.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdjava.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdjava.ForeColor = System.Drawing.Color.White;
            this.btnOdjava.Name = "btnOdjava";
            this.btnOdjava.UseVisualStyleBackColor = false;
            this.btnOdjava.Click += new System.EventHandler(this.btnOdjava_Click);
            this.btnOdjava.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnOdjava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnPice
            // 
            this.btnPice.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPice, "btnPice");
            this.btnPice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPice.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPice.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPice.Name = "btnPice";
            this.btnPice.Tag = "1";
            this.btnPice.UseVisualStyleBackColor = false;
            this.btnPice.Click += new System.EventHandler(this.btnPice_Click);
            this.btnPice.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnPice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnRacunR1
            // 
            resources.ApplyResources(this.btnRacunR1, "btnRacunR1");
            this.btnRacunR1.BackColor = System.Drawing.Color.White;
            this.btnRacunR1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRacunR1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnRacunR1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRacunR1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnRacunR1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRacunR1.Name = "btnRacunR1";
            this.btnRacunR1.UseVisualStyleBackColor = false;
            this.btnRacunR1.Click += new System.EventHandler(this.button2_Click);
            this.btnRacunR1.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnRacunR1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnDodajNaStol
            // 
            resources.ApplyResources(this.btnDodajNaStol, "btnDodajNaStol");
            this.btnDodajNaStol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDodajNaStol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajNaStol.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDodajNaStol.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDodajNaStol.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDodajNaStol.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDodajNaStol.ForeColor = System.Drawing.Color.White;
            this.btnDodajNaStol.Name = "btnDodajNaStol";
            this.btnDodajNaStol.UseVisualStyleBackColor = false;
            this.btnDodajNaStol.Click += new System.EventHandler(this.btnDodajNaStol_Click);
            this.btnDodajNaStol.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnDodajNaStol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnGotovina
            // 
            resources.ApplyResources(this.btnGotovina, "btnGotovina");
            this.btnGotovina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnGotovina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGotovina.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGotovina.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnGotovina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnGotovina.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnGotovina.ForeColor = System.Drawing.Color.White;
            this.btnGotovina.Name = "btnGotovina";
            this.btnGotovina.UseVisualStyleBackColor = false;
            this.btnGotovina.Click += new System.EventHandler(this.btnGotovina_Click);
            this.btnGotovina.Enter += new System.EventHandler(this.btnPice_Enter);
            this.btnGotovina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            // 
            // btnOtpremnica
            // 
            resources.ApplyResources(this.btnOtpremnica, "btnOtpremnica");
            this.btnOtpremnica.BackColor = System.Drawing.Color.White;
            this.btnOtpremnica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtpremnica.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOtpremnica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOtpremnica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOtpremnica.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOtpremnica.Name = "btnOtpremnica";
            this.btnOtpremnica.Tag = "0";
            this.btnOtpremnica.UseVisualStyleBackColor = false;
            this.btnOtpremnica.Click += new System.EventHandler(this.btnOtpremnica_Click);
            // 
            // lblBeautyOsoba
            // 
            resources.ApplyResources(this.lblBeautyOsoba, "lblBeautyOsoba");
            this.lblBeautyOsoba.BackColor = System.Drawing.Color.Transparent;
            this.lblBeautyOsoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBeautyOsoba.Name = "lblBeautyOsoba";
            this.lblBeautyOsoba.Click += new System.EventHandler(this.lblBeautyOsoba_Click);
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.BackColor = System.Drawing.Color.White;
            this.btnUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUp.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.BackColor = System.Drawing.Color.White;
            this.btnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDown.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblUkupno);
            this.panel2.Name = "panel2";
            // 
            // lblSat
            // 
            resources.ApplyResources(this.lblSat, "lblSat");
            this.lblSat.BackColor = System.Drawing.Color.Transparent;
            this.lblSat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSat.ForeColor = System.Drawing.Color.Black;
            this.lblSat.Name = "lblSat";
            this.lblSat.Click += new System.EventHandler(this.button1_Click);
            this.lblSat.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // lblZadnjiRac
            // 
            resources.ApplyResources(this.lblZadnjiRac, "lblZadnjiRac");
            this.lblZadnjiRac.BackColor = System.Drawing.Color.Transparent;
            this.lblZadnjiRac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblZadnjiRac.ForeColor = System.Drawing.Color.Black;
            this.lblZadnjiRac.Name = "lblZadnjiRac";
            // 
            // lblKarticaUkupno
            // 
            resources.ApplyResources(this.lblKarticaUkupno, "lblKarticaUkupno");
            this.lblKarticaUkupno.BackColor = System.Drawing.Color.Transparent;
            this.lblKarticaUkupno.ForeColor = System.Drawing.Color.White;
            this.lblKarticaUkupno.Name = "lblKarticaUkupno";
            // 
            // lblKarticaKorisnik
            // 
            resources.ApplyResources(this.lblKarticaKorisnik, "lblKarticaKorisnik");
            this.lblKarticaKorisnik.BackColor = System.Drawing.Color.Transparent;
            this.lblKarticaKorisnik.ForeColor = System.Drawing.Color.White;
            this.lblKarticaKorisnik.Name = "lblKarticaKorisnik";
            // 
            // lblPrijavljen
            // 
            resources.ApplyResources(this.lblPrijavljen, "lblPrijavljen");
            this.lblPrijavljen.BackColor = System.Drawing.Color.Transparent;
            this.lblPrijavljen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrijavljen.ForeColor = System.Drawing.Color.Black;
            this.lblPrijavljen.Name = "lblPrijavljen";
            this.lblPrijavljen.Click += new System.EventHandler(this.lblPrijavljen_Click);
            this.lblPrijavljen.Enter += new System.EventHandler(this.btnPice_Enter);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelPostojeNeuspjeleFiskalizacije);
            this.panel3.Controls.Add(this.lblSat);
            this.panel3.Controls.Add(this.lblPrijavljen);
            this.panel3.Controls.Add(this.lblZadnjiRac);
            this.panel3.Controls.Add(this.lblKarticaUkupno);
            this.panel3.Controls.Add(this.lblKarticaKorisnik);
            this.panel3.Name = "panel3";
            // 
            // labelPostojeNeuspjeleFiskalizacije
            // 
            resources.ApplyResources(this.labelPostojeNeuspjeleFiskalizacije, "labelPostojeNeuspjeleFiskalizacije");
            this.labelPostojeNeuspjeleFiskalizacije.ForeColor = System.Drawing.Color.Red;
            this.labelPostojeNeuspjeleFiskalizacije.Name = "labelPostojeNeuspjeleFiskalizacije";
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 600000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.naziv, "naziv");
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 60F;
            resources.ApplyResources(this.kolicina, "kolicina");
            this.kolicina.Name = "kolicina";
            this.kolicina.ReadOnly = true;
            // 
            // mpc
            // 
            this.mpc.FillWeight = 70F;
            resources.ApplyResources(this.mpc, "mpc");
            this.mpc.Name = "mpc";
            this.mpc.ReadOnly = true;
            // 
            // sifra
            // 
            resources.ApplyResources(this.sifra, "sifra");
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // skladiste
            // 
            resources.ApplyResources(this.skladiste, "skladiste");
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            // 
            // porez
            // 
            resources.ApplyResources(this.porez, "porez");
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            // 
            // rabat
            // 
            resources.ApplyResources(this.rabat, "rabat");
            this.rabat.Name = "rabat";
            this.rabat.ReadOnly = true;
            // 
            // vpc
            // 
            resources.ApplyResources(this.vpc, "vpc");
            this.vpc.Name = "vpc";
            this.vpc.ReadOnly = true;
            // 
            // porez_potrosnja
            // 
            resources.ApplyResources(this.porez_potrosnja, "porez_potrosnja");
            this.porez_potrosnja.Name = "porez_potrosnja";
            this.porez_potrosnja.ReadOnly = true;
            // 
            // zakljucaj
            // 
            resources.ApplyResources(this.zakljucaj, "zakljucaj");
            this.zakljucaj.Name = "zakljucaj";
            this.zakljucaj.ReadOnly = true;
            // 
            // jelo
            // 
            resources.ApplyResources(this.jelo, "jelo");
            this.jelo.Name = "jelo";
            this.jelo.ReadOnly = true;
            // 
            // nbc
            // 
            this.nbc.FillWeight = 70F;
            resources.ApplyResources(this.nbc, "nbc");
            this.nbc.Name = "nbc";
            this.nbc.ReadOnly = true;
            // 
            // dod
            // 
            resources.ApplyResources(this.dod, "dod");
            this.dod.Name = "dod";
            this.dod.ReadOnly = true;
            // 
            // polapola
            // 
            resources.ApplyResources(this.polapola, "polapola");
            this.polapola.Name = "polapola";
            this.polapola.ReadOnly = true;
            // 
            // id_podgrupa
            // 
            resources.ApplyResources(this.id_podgrupa, "id_podgrupa");
            this.id_podgrupa.Name = "id_podgrupa";
            this.id_podgrupa.ReadOnly = true;
            // 
            // frmCaffe
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.btnKoristiKarticu);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.btnDesert);
            this.Controls.Add(this.btnGlavnoJelo);
            this.Controls.Add(this.btnPredjela);
            this.Controls.Add(this.flpArtikli);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.lblBeautyOsoba);
            this.Controls.Add(this.btnOtpremnica);
            this.Controls.Add(this.btnPice);
            this.Controls.Add(this.btnHrana);
            this.Controls.Add(this.btnNapomena);
            this.Controls.Add(this.btnPolaPola);
            this.Controls.Add(this.btnDodatak);
            this.Controls.Add(this.txtKarticaKupca);
            this.Controls.Add(this.btnOdustaniKartica);
            this.Controls.Add(this.btnPopust);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblKupac);
            this.Controls.Add(this.lblBrojRac);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.btnZavrsiSmjenu);
            this.Controls.Add(this.btnNarudzbe);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnOpcije);
            this.Controls.Add(this.btnStolovi);
            this.Controls.Add(this.btnNajProdavanije);
            this.Controls.Add(this.btnOdjava);
            this.Controls.Add(this.lblnativGrupe);
            this.Controls.Add(this.btnRacunR1);
            this.Controls.Add(this.btnDodajNaStol);
            this.Controls.Add(this.btnGotovina);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpGrupe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCaffe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmCaffe_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCaffe_FormClosing);
            this.Load += new System.EventHandler(this.frmCaffe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPice_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpGrupe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBstolovi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblnativGrupe;
        private System.Windows.Forms.TextBox txtUnos;
        internal System.Windows.Forms.Label lblUkupno;
        private System.Windows.Forms.Label lblBrojRac;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHrana;
        private System.Windows.Forms.Button btnOdjava;
        private System.Windows.Forms.Button btnStolovi;
        private System.Windows.Forms.Button btnOpcije;
        private System.Windows.Forms.Button button10;
        public System.Windows.Forms.FlowLayoutPanel flpArtikli;
        private System.Windows.Forms.Label lblKupac;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnNajProdavanije;
		private System.Windows.Forms.Button btnNarudzbe;
        public System.Windows.Forms.DataGridView dgw;
        public System.Windows.Forms.Button btnGotovina;
        public System.Windows.Forms.Button btnDodajNaStol;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnRacunR1;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnPice;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button btnZavrsiSmjenu;
        private System.Windows.Forms.Button btnPredjela;
        private System.Windows.Forms.Button btnGlavnoJelo;
        private System.Windows.Forms.Button btnDesert;
        private System.IO.Ports.SerialPort spLineDisplay;
        private System.Windows.Forms.Button btnPopust;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.Timer timeKarticaKupca;
        public System.Windows.Forms.TextBox txtKarticaKupca;
        private System.Windows.Forms.Button btnOdustaniKartica;
        private System.Windows.Forms.Button btnDodatak;
        private System.Windows.Forms.Button btnKoristiKarticu;
        private System.Windows.Forms.Button btnPolaPola;
        private System.Windows.Forms.Button btnNapomena;
        private System.Windows.Forms.Button btnOtpremnica;
        private System.Windows.Forms.Label lblBeautyOsoba;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSat;
        private System.Windows.Forms.Label lblZadnjiRac;
        private System.Windows.Forms.Label lblKarticaUkupno;
        private System.Windows.Forms.Label lblKarticaKorisnik;
        public System.Windows.Forms.Label lblPrijavljen;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelPostojeNeuspjeleFiskalizacije;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez_potrosnja;
        private System.Windows.Forms.DataGridViewTextBoxColumn zakljucaj;
        private System.Windows.Forms.DataGridViewTextBoxColumn jelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dod;
        private System.Windows.Forms.DataGridViewTextBoxColumn polapola;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_podgrupa;
    }
}