namespace PCPOS.Robno
{
    partial class frmIzdatnica
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgw = new PCPOS.Robno.frmIzdatnica.MyDataGrid();
            this.rb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.izdatnicaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbMjesto = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.txtOrginalniDok = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.nmGodina = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.bgSinkronizacija = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rb,
            this.sifra,
            this.naziv,
            this.nbc,
            this.kolicina,
            this.vpc,
            this.mpc,
            this.pdv,
            this.rabat,
            this.iznos,
            this.ukupno,
            this.id_stavka,
            this.izdatnicaID});
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 292);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(963, 298);
            this.dgw.TabIndex = 11;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            this.dgw.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dgw.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rb
            // 
            this.rb.FillWeight = 30F;
            this.rb.HeaderText = "RB";
            this.rb.Name = "rb";
            this.rb.ReadOnly = true;
            // 
            // sifra
            // 
            this.sifra.FillWeight = 80F;
            this.sifra.HeaderText = "Šifra robe";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 105.5838F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // nbc
            // 
            this.nbc.FillWeight = 70F;
            this.nbc.HeaderText = "Nabavna";
            this.nbc.Name = "nbc";
            this.nbc.Visible = false;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 70F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // vpc
            // 
            this.vpc.HeaderText = "Veleprodajna";
            this.vpc.Name = "vpc";
            this.vpc.Visible = false;
            // 
            // mpc
            // 
            this.mpc.HeaderText = "Maloprodajna";
            this.mpc.Name = "mpc";
            this.mpc.Visible = false;
            // 
            // pdv
            // 
            this.pdv.HeaderText = "PDV %";
            this.pdv.Name = "pdv";
            this.pdv.Visible = false;
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat %";
            this.rabat.Name = "rabat";
            this.rabat.Visible = false;
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos bez pdv-a";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            this.iznos.Visible = false;
            // 
            // ukupno
            // 
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.ReadOnly = true;
            this.ukupno.Visible = false;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // izdatnicaID
            // 
            this.izdatnicaID.HeaderText = "izdatnica";
            this.izdatnicaID.Name = "izdatnicaID";
            this.izdatnicaID.Visible = false;
            // 
            // cbMjesto
            // 
            this.cbMjesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMjesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMjesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMjesto.Font = new System.Drawing.Font("Arial", 10F);
            this.cbMjesto.FormattingEnabled = true;
            this.cbMjesto.Location = new System.Drawing.Point(544, 9);
            this.cbMjesto.Name = "cbMjesto";
            this.cbMjesto.Size = new System.Drawing.Size(247, 24);
            this.cbMjesto.TabIndex = 8;
            this.cbMjesto.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbMjesto.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.Location = new System.Drawing.Point(442, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Mjesto troška:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(458, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Napomena:";
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Enabled = false;
            this.rtbNapomena.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(544, 68);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(247, 52);
            this.rtbNapomena.TabIndex = 12;
            this.rtbNapomena.Text = "";
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtOrginalniDok
            // 
            this.txtOrginalniDok.BackColor = System.Drawing.Color.White;
            this.txtOrginalniDok.Enabled = false;
            this.txtOrginalniDok.Font = new System.Drawing.Font("Arial", 10F);
            this.txtOrginalniDok.Location = new System.Drawing.Point(544, 39);
            this.txtOrginalniDok.Name = "txtOrginalniDok";
            this.txtOrginalniDok.Size = new System.Drawing.Size(247, 23);
            this.txtOrginalniDok.TabIndex = 10;
            this.txtOrginalniDok.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtOrginalniDok.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.Location = new System.Drawing.Point(439, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Originalni dok.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(77, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Izradio:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(56, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Odredište:";
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.BackColor = System.Drawing.Color.White;
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Arial", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(51, 97);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(305, 23);
            this.txtPartnerNaziv.TabIndex = 6;
            this.txtPartnerNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPartnerNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(77, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Datum:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.Enabled = false;
            this.dtpDatum.Font = new System.Drawing.Font("Arial", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(136, 39);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(220, 23);
            this.dtpDatum.TabIndex = 3;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtIzradio
            // 
            this.txtIzradio.BackColor = System.Drawing.Color.White;
            this.txtIzradio.Font = new System.Drawing.Font("Arial", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(136, 10);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(220, 23);
            this.txtIzradio.TabIndex = 1;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Enabled = false;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Arial", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(136, 68);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(175, 23);
            this.txtSifraOdrediste.TabIndex = 5;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._10591;
            this.pictureBox1.Location = new System.Drawing.Point(317, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 215;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtBroj
            // 
            this.txtBroj.BackColor = System.Drawing.Color.White;
            this.txtBroj.Font = new System.Drawing.Font("Arial", 10F);
            this.txtBroj.Location = new System.Drawing.Point(543, 8);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(145, 23);
            this.txtBroj.TabIndex = 3;
            this.txtBroj.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
            this.txtBroj.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Arial", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(135, 7);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(221, 24);
            this.cbSkladiste.TabIndex = 1;
            this.cbSkladiste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbSkladiste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodina
            // 
            this.nmGodina.BackColor = System.Drawing.Color.White;
            this.nmGodina.Font = new System.Drawing.Font("Arial", 10F);
            this.nmGodina.Location = new System.Drawing.Point(694, 8);
            this.nmGodina.Name = "nmGodina";
            this.nmGodina.Size = new System.Drawing.Size(97, 23);
            this.nmGodina.TabIndex = 4;
            this.nmGodina.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodina.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.ForeColor = System.Drawing.Color.Maroon;
            this.label17.Location = new System.Drawing.Point(47, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 18);
            this.label17.TabIndex = 0;
            this.label17.Text = "Skladište:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(442, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj unosa:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(47, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Šifra artikla:";
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.Font = new System.Drawing.Font("Arial", 10F);
            this.txtSifra_robe.Location = new System.Drawing.Point(136, 11);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(221, 23);
            this.txtSifra_robe.TabIndex = 10;
            this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspis.Location = new System.Drawing.Point(707, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(97, 40);
            this.btnIspis.TabIndex = 5;
            this.btnIspis.Text = "Ispis   ";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Visible = false;
            this.btnIspis.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.BackColor = System.Drawing.Color.White;
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDeleteAllFaktura.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAllFaktura.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAllFaktura.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDeleteAllFaktura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(145, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši izdatnicu";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = false;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
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
            this.btnObrisi.Location = new System.Drawing.Point(828, 7);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(130, 30);
            this.btnObrisi.TabIndex = 12;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = global::PCPOS.Properties.Resources._10591;
            this.btnOpenRoba.Location = new System.Drawing.Point(363, 7);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 238;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(868, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Izlaz    ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.BackColor = System.Drawing.Color.White;
            this.btnSveFakture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSveFakture.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSveFakture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Sve izdatnice";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = false;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
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
            this.btnNoviUnos.TabIndex = 0;
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
            this.btnOdustani.TabIndex = 1;
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
            this.btnSpremi.Image = global::PCPOS.Properties.Resources.filesave;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // bgSinkronizacija
            // 
            this.bgSinkronizacija.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSinkronizacija_DoWork);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtBroj);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.cbSkladiste);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nmGodina);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 40);
            this.panel1.TabIndex = 239;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbMjesto);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtSifraOdrediste);
            this.panel2.Controls.Add(this.rtbNapomena);
            this.panel2.Controls.Add(this.txtIzradio);
            this.panel2.Controls.Add(this.txtOrginalniDok);
            this.panel2.Controls.Add(this.dtpDatum);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtPartnerNaziv);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(12, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(963, 130);
            this.panel2.TabIndex = 240;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtSifra_robe);
            this.panel3.Controls.Add(this.btnOpenRoba);
            this.panel3.Controls.Add(this.btnObrisi);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(12, 240);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(963, 46);
            this.panel3.TabIndex = 241;
            // 
            // frmIzdatnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(987, 602);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Name = "frmIzdatnica";
            this.Text = "Izdatnica utrošenog repromaterijala";
            this.Load += new System.EventHandler(this.frmIzdatnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbMjesto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        public System.Windows.Forms.TextBox txtOrginalniDok;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        public System.Windows.Forms.TextBox txtIzradio;
        public System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSifra_robe;
        //private System.Windows.Forms.DataGridView dgw;
        private frmIzdatnica.MyDataGrid dgw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn izdatnicaID;
        private System.ComponentModel.BackgroundWorker bgSinkronizacija;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}