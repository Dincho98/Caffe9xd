namespace PCPOS
{
    partial class frmRadniNalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRadniNalog));
            this.lblNaDan = new System.Windows.Forms.Label();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSveStavke = new System.Windows.Forms.Button();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.btnPartner = new System.Windows.Forms.Button();
            this.cbVD = new System.Windows.Forms.ComboBox();
            this.cbMjestoTroska = new System.Windows.Forms.ComboBox();
            this.cbIzvrsio = new System.Windows.Forms.ComboBox();
            this.dtpDatumZavrsetka = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumPrimitka = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpDatumNaloga = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ttxBrojPonude = new System.Windows.Forms.TextBox();
            this.nmGodinaPonude = new System.Windows.Forms.NumericUpDown();
            this.btnOpenRoba = new System.Windows.Forms.Button();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnSve = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaPonude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaDan.Location = new System.Drawing.Point(16, 625);
            this.lblNaDan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 13);
            this.lblNaDan.TabIndex = 49;
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra_robe.Location = new System.Drawing.Point(7, 7);
            this.txtSifra_robe.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(199, 23);
            this.txtSifra_robe.TabIndex = 35;
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(507, 638);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(464, 23);
            this.textBox1.TabIndex = 40;
            this.textBox1.Text = "Ukupno sa PDV-om:";
            // 
            // btnSveStavke
            // 
            this.btnSveStavke.BackColor = System.Drawing.Color.White;
            this.btnSveStavke.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSveStavke.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSveStavke.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveStavke.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveStavke.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSveStavke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSveStavke.Location = new System.Drawing.Point(532, 176);
            this.btnSveStavke.Margin = new System.Windows.Forms.Padding(4);
            this.btnSveStavke.Name = "btnSveStavke";
            this.btnSveStavke.Size = new System.Drawing.Size(187, 28);
            this.btnSveStavke.TabIndex = 62;
            this.btnSveStavke.Text = "Stavke u radnom nalogu";
            this.btnSveStavke.UseVisualStyleBackColor = false;
            this.btnSveStavke.Click += new System.EventHandler(this.btnSveStavke_Click);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Location = new System.Drawing.Point(532, 42);
            this.rtbNapomena.Margin = new System.Windows.Forms.Padding(4);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(380, 116);
            this.rtbNapomena.TabIndex = 61;
            this.rtbNapomena.Text = "";
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.White;
            this.btnPartner.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPartner.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPartner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPartner.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartner.Location = new System.Drawing.Point(206, 165);
            this.btnPartner.Margin = new System.Windows.Forms.Padding(4);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(37, 28);
            this.btnPartner.TabIndex = 60;
            this.btnPartner.Text = "...";
            this.btnPartner.UseVisualStyleBackColor = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // cbVD
            // 
            this.cbVD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbVD.FormattingEnabled = true;
            this.cbVD.Location = new System.Drawing.Point(136, 10);
            this.cbVD.Margin = new System.Windows.Forms.Padding(4);
            this.cbVD.Name = "cbVD";
            this.cbVD.Size = new System.Drawing.Size(276, 24);
            this.cbVD.TabIndex = 50;
            this.cbVD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbVD_KeyDown);
            // 
            // cbMjestoTroska
            // 
            this.cbMjestoTroska.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMjestoTroska.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMjestoTroska.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbMjestoTroska.FormattingEnabled = true;
            this.cbMjestoTroska.Location = new System.Drawing.Point(136, 135);
            this.cbMjestoTroska.Margin = new System.Windows.Forms.Padding(4);
            this.cbMjestoTroska.Name = "cbMjestoTroska";
            this.cbMjestoTroska.Size = new System.Drawing.Size(276, 24);
            this.cbMjestoTroska.TabIndex = 54;
            this.cbMjestoTroska.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbKomercijalist_KeyDown);
            // 
            // cbIzvrsio
            // 
            this.cbIzvrsio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbIzvrsio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIzvrsio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbIzvrsio.FormattingEnabled = true;
            this.cbIzvrsio.Location = new System.Drawing.Point(532, 7);
            this.cbIzvrsio.Margin = new System.Windows.Forms.Padding(4);
            this.cbIzvrsio.Name = "cbIzvrsio";
            this.cbIzvrsio.Size = new System.Drawing.Size(380, 24);
            this.cbIzvrsio.TabIndex = 55;
            this.cbIzvrsio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbIzvrsio_KeyDown);
            // 
            // dtpDatumZavrsetka
            // 
            this.dtpDatumZavrsetka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumZavrsetka.Location = new System.Drawing.Point(136, 103);
            this.dtpDatumZavrsetka.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDatumZavrsetka.Name = "dtpDatumZavrsetka";
            this.dtpDatumZavrsetka.Size = new System.Drawing.Size(276, 23);
            this.dtpDatumZavrsetka.TabIndex = 45;
            this.dtpDatumZavrsetka.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatumZavrsetka_KeyDown);
            // 
            // dtpDatumPrimitka
            // 
            this.dtpDatumPrimitka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumPrimitka.Location = new System.Drawing.Point(136, 72);
            this.dtpDatumPrimitka.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDatumPrimitka.Name = "dtpDatumPrimitka";
            this.dtpDatumPrimitka.Size = new System.Drawing.Size(276, 23);
            this.dtpDatumPrimitka.TabIndex = 45;
            this.dtpDatumPrimitka.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatumPrimitka_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(26, 199);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 38;
            this.label13.Text = "Izradio:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(26, 168);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 17);
            this.label12.TabIndex = 39;
            this.label12.Text = "Naručioc";
            // 
            // dtpDatumNaloga
            // 
            this.dtpDatumNaloga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumNaloga.Location = new System.Drawing.Point(136, 42);
            this.dtpDatumNaloga.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDatumNaloga.Name = "dtpDatumNaloga";
            this.dtpDatumNaloga.Size = new System.Drawing.Size(276, 23);
            this.dtpDatumNaloga.TabIndex = 46;
            this.dtpDatumNaloga.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatumNaloga_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(26, 138);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 17);
            this.label11.TabIndex = 37;
            this.label11.Text = "Mj.troška:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(446, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 35;
            this.label8.Text = "Napomena:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(26, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "Završna kart:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(26, 76);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 43;
            this.label7.Text = "Datum primitka:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(25, 15);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 17);
            this.label17.TabIndex = 24;
            this.label17.Text = "Vrsta dok.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(26, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Datum naloga:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(446, 15);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 17);
            this.label15.TabIndex = 34;
            this.label15.Text = "Izvršio:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIzradio.Location = new System.Drawing.Point(136, 198);
            this.txtIzradio.Margin = new System.Windows.Forms.Padding(4);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(276, 23);
            this.txtIzradio.TabIndex = 11;
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIzradio_KeyDown);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartnerNaziv.Location = new System.Drawing.Point(244, 167);
            this.txtPartnerNaziv.Margin = new System.Windows.Forms.Padding(4);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(168, 23);
            this.txtPartnerNaziv.TabIndex = 18;
            this.txtPartnerNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerNaziv_KeyDown);
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraOdrediste.Location = new System.Drawing.Point(136, 167);
            this.txtSifraOdrediste.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(69, 24);
            this.txtSifraOdrediste.TabIndex = 20;
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj radnog naloga:";
            // 
            // ttxBrojPonude
            // 
            this.ttxBrojPonude.BackColor = System.Drawing.Color.White;
            this.ttxBrojPonude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ttxBrojPonude.Location = new System.Drawing.Point(183, 7);
            this.ttxBrojPonude.Margin = new System.Windows.Forms.Padding(4);
            this.ttxBrojPonude.Name = "ttxBrojPonude";
            this.ttxBrojPonude.Size = new System.Drawing.Size(83, 23);
            this.ttxBrojPonude.TabIndex = 1;
            this.ttxBrojPonude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown_1);
            // 
            // nmGodinaPonude
            // 
            this.nmGodinaPonude.BackColor = System.Drawing.Color.White;
            this.nmGodinaPonude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nmGodinaPonude.Location = new System.Drawing.Point(269, 7);
            this.nmGodinaPonude.Margin = new System.Windows.Forms.Padding(4);
            this.nmGodinaPonude.Name = "nmGodinaPonude";
            this.nmGodinaPonude.Size = new System.Drawing.Size(72, 23);
            this.nmGodinaPonude.TabIndex = 5;
            this.nmGodinaPonude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown_1);
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.White;
            this.btnOpenRoba.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOpenRoba.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOpenRoba.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOpenRoba.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOpenRoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenRoba.Location = new System.Drawing.Point(207, 5);
            this.btnOpenRoba.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(37, 26);
            this.btnOpenRoba.TabIndex = 37;
            this.btnOpenRoba.Text = "...";
            this.btnOpenRoba.UseVisualStyleBackColor = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
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
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(13, 388);
            this.dgw.Margin = new System.Windows.Forms.Padding(4);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(958, 242);
            this.dgw.TabIndex = 34;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.BackColor = System.Drawing.Color.White;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(820, 3);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(135, 30);
            this.btnObrisi.TabIndex = 65;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(842, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 71;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.Color.White;
            this.btnDeleteAll.Enabled = false;
            this.btnDeleteAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDeleteAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAll.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAll.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAll.TabIndex = 70;
            this.btnDeleteAll.Text = "Obriši radni n";
            this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAllRN_Click);
            // 
            // btnSve
            // 
            this.btnSve.BackColor = System.Drawing.Color.White;
            this.btnSve.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSve.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSve.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSve.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSve.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSve.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSve.Location = new System.Drawing.Point(420, 12);
            this.btnSve.Name = "btnSve";
            this.btnSve.Size = new System.Drawing.Size(130, 40);
            this.btnSve.TabIndex = 69;
            this.btnSve.Text = "Svi radni n.  ";
            this.btnSve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSve.UseVisualStyleBackColor = false;
            this.btnSve.Click += new System.EventHandler(this.btnSviRN_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 68;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(148, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 67;
            this.btnOdustani.Text = "Odustani   ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 66;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ttxBrojPonude);
            this.panel1.Controls.Add(this.nmGodinaPonude);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 39);
            this.panel1.TabIndex = 72;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSveStavke);
            this.panel2.Controls.Add(this.cbVD);
            this.panel2.Controls.Add(this.rtbNapomena);
            this.panel2.Controls.Add(this.txtSifraOdrediste);
            this.panel2.Controls.Add(this.btnPartner);
            this.panel2.Controls.Add(this.txtPartnerNaziv);
            this.panel2.Controls.Add(this.txtIzradio);
            this.panel2.Controls.Add(this.cbMjestoTroska);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.cbIzvrsio);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtpDatumZavrsetka);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.dtpDatumPrimitka);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.dtpDatumNaloga);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(12, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(960, 234);
            this.panel2.TabIndex = 73;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnObrisi);
            this.panel3.Controls.Add(this.btnOpenRoba);
            this.panel3.Controls.Add(this.txtSifra_robe);
            this.panel3.Location = new System.Drawing.Point(12, 343);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(960, 38);
            this.panel3.TabIndex = 74;
            // 
            // frmRadniNalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(984, 674);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnSve);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgw);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRadniNalog";
            this.Text = "Radni nalog";
            this.Load += new System.EventHandler(this.frmRadniNalog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaPonude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.ComboBox cbVD;
        private System.Windows.Forms.ComboBox cbMjestoTroska;
        private System.Windows.Forms.ComboBox cbIzvrsio;
        private System.Windows.Forms.DateTimePicker dtpDatumPrimitka;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpDatumNaloga;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox ttxBrojPonude;
        private System.Windows.Forms.NumericUpDown nmGodinaPonude;
        private System.Windows.Forms.Button btnOpenRoba;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DateTimePicker dtpDatumZavrsetka;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.Button btnSveStavke;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnSve;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}