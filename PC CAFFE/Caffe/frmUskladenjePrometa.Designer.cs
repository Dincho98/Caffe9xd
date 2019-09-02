namespace PCPOS.Caffe
{
    partial class frmUskladenjePrometa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina_skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._brojcanik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unos_robe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodaja_kucano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodaja_prodano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razlika = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznoskuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanje_prijenos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brojcanik_prijenos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donos_brojcanik1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donos_brojcanik2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brojcanik_kraj_dana1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brojcanik_kraj_dana2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDonosOD = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUcitajPodatke = new System.Windows.Forms.Button();
            this.btnObradiPodatke = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrometKase = new System.Windows.Forms.TextBox();
            this.txtPrometBrojano = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvD = new System.Windows.Forms.DataGridView();
            this.djelatnik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaci = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBrojcanikKave = new System.Windows.Forms.Button();
            this.btnMinius = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.txtObracun = new System.Windows.Forms.TextBox();
            this.btnBrisanjePrometa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle57.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle57;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle58.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            dataGridViewCellStyle58.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle58;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.kolicina_skladiste,
            this._brojcanik,
            this.unos_robe,
            this.prodaja_kucano,
            this.prodaja_prodano,
            this.stanje,
            this.razlika,
            this.iznoskuna,
            this.stanje_prijenos,
            this.brojcanik_prijenos,
            this.cijena,
            this.donos_brojcanik1,
            this.donos_brojcanik2,
            this.brojcanik_kraj_dana1,
            this.brojcanik_kraj_dana2});
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle70.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle70.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle70.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle70.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle70.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle70.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle70;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(12, 98);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(979, 429);
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            // 
            // sifra
            // 
            this.sifra.FillWeight = 60F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.Width = 60;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            // 
            // kolicina_skladiste
            // 
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle59.BackColor = System.Drawing.Color.PapayaWhip;
            this.kolicina_skladiste.DefaultCellStyle = dataGridViewCellStyle59;
            this.kolicina_skladiste.FillWeight = 70F;
            this.kolicina_skladiste.HeaderText = "DONOS: Kol. na skladištu";
            this.kolicina_skladiste.Name = "kolicina_skladiste";
            this.kolicina_skladiste.Width = 70;
            // 
            // _brojcanik
            // 
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle60.BackColor = System.Drawing.Color.PapayaWhip;
            this._brojcanik.DefaultCellStyle = dataGridViewCellStyle60;
            this._brojcanik.FillWeight = 70F;
            this._brojcanik.HeaderText = "DONOS: Brojčanik           ";
            this._brojcanik.Name = "_brojcanik";
            this._brojcanik.Width = 70;
            // 
            // unos_robe
            // 
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle61.BackColor = System.Drawing.Color.PapayaWhip;
            this.unos_robe.DefaultCellStyle = dataGridViewCellStyle61;
            this.unos_robe.FillWeight = 70F;
            this.unos_robe.HeaderText = "DONOS: Unos robe";
            this.unos_robe.Name = "unos_robe";
            this.unos_robe.Width = 70;
            // 
            // prodaja_kucano
            // 
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle62.BackColor = System.Drawing.Color.LightGreen;
            this.prodaja_kucano.DefaultCellStyle = dataGridViewCellStyle62;
            this.prodaja_kucano.FillWeight = 70F;
            this.prodaja_kucano.HeaderText = "PRODAJA: Kucano";
            this.prodaja_kucano.Name = "prodaja_kucano";
            this.prodaja_kucano.Width = 70;
            // 
            // prodaja_prodano
            // 
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle63.BackColor = System.Drawing.Color.LightGreen;
            this.prodaja_prodano.DefaultCellStyle = dataGridViewCellStyle63;
            this.prodaja_prodano.FillWeight = 75F;
            this.prodaja_prodano.HeaderText = "PRODAJA: Prodano";
            this.prodaja_prodano.Name = "prodaja_prodano";
            this.prodaja_prodano.Width = 75;
            // 
            // stanje
            // 
            dataGridViewCellStyle64.BackColor = System.Drawing.Color.LightGreen;
            this.stanje.DefaultCellStyle = dataGridViewCellStyle64;
            this.stanje.FillWeight = 70F;
            this.stanje.HeaderText = "PRODAJA: Stanje               ";
            this.stanje.Name = "stanje";
            this.stanje.Width = 70;
            // 
            // razlika
            // 
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle65.BackColor = System.Drawing.Color.LightGreen;
            this.razlika.DefaultCellStyle = dataGridViewCellStyle65;
            this.razlika.FillWeight = 70F;
            this.razlika.HeaderText = "PRODAJA: Razlika         ";
            this.razlika.Name = "razlika";
            this.razlika.Width = 70;
            // 
            // iznoskuna
            // 
            dataGridViewCellStyle66.BackColor = System.Drawing.Color.LightGreen;
            this.iznoskuna.DefaultCellStyle = dataGridViewCellStyle66;
            this.iznoskuna.FillWeight = 75F;
            this.iznoskuna.HeaderText = "PRODAJA: Iznos kuna     ";
            this.iznoskuna.Name = "iznoskuna";
            this.iznoskuna.Width = 75;
            // 
            // stanje_prijenos
            // 
            dataGridViewCellStyle67.BackColor = System.Drawing.Color.LavenderBlush;
            this.stanje_prijenos.DefaultCellStyle = dataGridViewCellStyle67;
            this.stanje_prijenos.FillWeight = 75F;
            this.stanje_prijenos.HeaderText = "PRIJENOS: Stanje                ";
            this.stanje_prijenos.Name = "stanje_prijenos";
            this.stanje_prijenos.Width = 75;
            // 
            // brojcanik_prijenos
            // 
            dataGridViewCellStyle68.BackColor = System.Drawing.Color.LavenderBlush;
            this.brojcanik_prijenos.DefaultCellStyle = dataGridViewCellStyle68;
            this.brojcanik_prijenos.FillWeight = 75F;
            this.brojcanik_prijenos.HeaderText = "PRIJENOS: Brojčanik                     ";
            this.brojcanik_prijenos.Name = "brojcanik_prijenos";
            this.brojcanik_prijenos.Width = 75;
            // 
            // cijena
            // 
            dataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cijena.DefaultCellStyle = dataGridViewCellStyle69;
            this.cijena.FillWeight = 75F;
            this.cijena.HeaderText = "CIJENA";
            this.cijena.Name = "cijena";
            this.cijena.Width = 75;
            // 
            // donos_brojcanik1
            // 
            this.donos_brojcanik1.HeaderText = "donos_brojcanik1";
            this.donos_brojcanik1.Name = "donos_brojcanik1";
            this.donos_brojcanik1.Visible = false;
            // 
            // donos_brojcanik2
            // 
            this.donos_brojcanik2.HeaderText = "donos_brojcanik2";
            this.donos_brojcanik2.Name = "donos_brojcanik2";
            this.donos_brojcanik2.Visible = false;
            // 
            // brojcanik_kraj_dana1
            // 
            this.brojcanik_kraj_dana1.HeaderText = "brojcanik_kraj_dana1";
            this.brojcanik_kraj_dana1.Name = "brojcanik_kraj_dana1";
            this.brojcanik_kraj_dana1.Visible = false;
            // 
            // brojcanik_kraj_dana2
            // 
            this.brojcanik_kraj_dana2.HeaderText = "brojcanik_kraj_dana2";
            this.brojcanik_kraj_dana2.Name = "brojcanik_kraj_dana2";
            this.brojcanik_kraj_dana2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(33, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "OBRAČUN:";
            // 
            // dtpDonosOD
            // 
            this.dtpDonosOD.CustomFormat = "dd.MM.yyyy";
            this.dtpDonosOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDonosOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDonosOD.Location = new System.Drawing.Point(125, 45);
            this.dtpDonosOD.Name = "dtpDonosOD";
            this.dtpDonosOD.Size = new System.Drawing.Size(155, 23);
            this.dtpDonosOD.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(33, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "DONOS OD:";
            // 
            // btnUcitajPodatke
            // 
            this.btnUcitajPodatke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitajPodatke.BackColor = System.Drawing.Color.White;
            this.btnUcitajPodatke.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitajPodatke.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnUcitajPodatke.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitajPodatke.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnUcitajPodatke.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnUcitajPodatke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitajPodatke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitajPodatke.Location = new System.Drawing.Point(843, 15);
            this.btnUcitajPodatke.Name = "btnUcitajPodatke";
            this.btnUcitajPodatke.Size = new System.Drawing.Size(148, 46);
            this.btnUcitajPodatke.TabIndex = 7;
            this.btnUcitajPodatke.Text = "Učitaj podatke";
            this.btnUcitajPodatke.UseVisualStyleBackColor = false;
            this.btnUcitajPodatke.Click += new System.EventHandler(this.btnUcitajPodatke_Click);
            // 
            // btnObradiPodatke
            // 
            this.btnObradiPodatke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObradiPodatke.BackColor = System.Drawing.Color.White;
            this.btnObradiPodatke.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObradiPodatke.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnObradiPodatke.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnObradiPodatke.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnObradiPodatke.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnObradiPodatke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObradiPodatke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObradiPodatke.Location = new System.Drawing.Point(843, 537);
            this.btnObradiPodatke.Name = "btnObradiPodatke";
            this.btnObradiPodatke.Size = new System.Drawing.Size(148, 43);
            this.btnObradiPodatke.TabIndex = 14;
            this.btnObradiPodatke.Text = "Obradi podatke";
            this.btnObradiPodatke.UseVisualStyleBackColor = false;
            this.btnObradiPodatke.Click += new System.EventHandler(this.btnObradiPodatke_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(19, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Promet kase:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(19, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Promet brojano:";
            // 
            // txtPrometKase
            // 
            this.txtPrometKase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPrometKase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrometKase.Location = new System.Drawing.Point(134, 0);
            this.txtPrometKase.Name = "txtPrometKase";
            this.txtPrometKase.ReadOnly = true;
            this.txtPrometKase.Size = new System.Drawing.Size(99, 23);
            this.txtPrometKase.TabIndex = 20;
            // 
            // txtPrometBrojano
            // 
            this.txtPrometBrojano.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPrometBrojano.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrometBrojano.Location = new System.Drawing.Point(134, 26);
            this.txtPrometBrojano.Name = "txtPrometBrojano";
            this.txtPrometBrojano.ReadOnly = true;
            this.txtPrometBrojano.Size = new System.Drawing.Size(99, 23);
            this.txtPrometBrojano.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(239, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "kn:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(239, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "kn:";
            // 
            // dgvD
            // 
            this.dgvD.AllowUserToAddRows = false;
            this.dgvD.AllowUserToDeleteRows = false;
            this.dgvD.BackgroundColor = System.Drawing.Color.White;
            this.dgvD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.djelatnik,
            this.oznaci,
            this.id});
            this.dgvD.Location = new System.Drawing.Point(438, 5);
            this.dgvD.Name = "dgvD";
            this.dgvD.ReadOnly = true;
            this.dgvD.RowHeadersVisible = false;
            this.dgvD.RowTemplate.Height = 18;
            this.dgvD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvD.Size = new System.Drawing.Size(212, 92);
            this.dgvD.TabIndex = 22;
            this.dgvD.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvD_CellClick);
            // 
            // djelatnik
            // 
            this.djelatnik.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.djelatnik.HeaderText = "Djelatnik";
            this.djelatnik.Name = "djelatnik";
            this.djelatnik.ReadOnly = true;
            // 
            // oznaci
            // 
            this.oznaci.FillWeight = 47F;
            this.oznaci.HeaderText = "Označi";
            this.oznaci.Name = "oznaci";
            this.oznaci.ReadOnly = true;
            this.oznaci.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oznaci.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.oznaci.Width = 47;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // btnBrojcanikKave
            // 
            this.btnBrojcanikKave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrojcanikKave.BackColor = System.Drawing.Color.White;
            this.btnBrojcanikKave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrojcanikKave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnBrojcanikKave.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnBrojcanikKave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnBrojcanikKave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnBrojcanikKave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrojcanikKave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBrojcanikKave.Location = new System.Drawing.Point(11, 536);
            this.btnBrojcanikKave.Name = "btnBrojcanikKave";
            this.btnBrojcanikKave.Size = new System.Drawing.Size(130, 43);
            this.btnBrojcanikKave.TabIndex = 23;
            this.btnBrojcanikKave.Text = "Brojčanik";
            this.btnBrojcanikKave.UseVisualStyleBackColor = false;
            this.btnBrojcanikKave.Click += new System.EventHandler(this.btnBrojcanikKave_Click);
            // 
            // btnMinius
            // 
            this.btnMinius.BackColor = System.Drawing.Color.White;
            this.btnMinius.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnMinius.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnMinius.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnMinius.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnMinius.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinius.Location = new System.Drawing.Point(282, 22);
            this.btnMinius.Name = "btnMinius";
            this.btnMinius.Size = new System.Drawing.Size(19, 22);
            this.btnMinius.TabIndex = 24;
            this.btnMinius.Text = "-";
            this.btnMinius.UseVisualStyleBackColor = false;
            this.btnMinius.Click += new System.EventHandler(this.btnMinius_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.White;
            this.btnPlus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPlus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPlus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPlus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Location = new System.Drawing.Point(303, 22);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(19, 22);
            this.btnPlus.TabIndex = 25;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // txtObracun
            // 
            this.txtObracun.Location = new System.Drawing.Point(125, 23);
            this.txtObracun.Name = "txtObracun";
            this.txtObracun.Size = new System.Drawing.Size(155, 20);
            this.txtObracun.TabIndex = 26;
            this.txtObracun.Leave += new System.EventHandler(this.txtObracun_Leave);
            // 
            // btnBrisanjePrometa
            // 
            this.btnBrisanjePrometa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrisanjePrometa.BackColor = System.Drawing.Color.White;
            this.btnBrisanjePrometa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrisanjePrometa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnBrisanjePrometa.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnBrisanjePrometa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnBrisanjePrometa.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnBrisanjePrometa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrisanjePrometa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBrisanjePrometa.ForeColor = System.Drawing.Color.Red;
            this.btnBrisanjePrometa.Location = new System.Drawing.Point(147, 536);
            this.btnBrisanjePrometa.Name = "btnBrisanjePrometa";
            this.btnBrisanjePrometa.Size = new System.Drawing.Size(130, 43);
            this.btnBrisanjePrometa.TabIndex = 27;
            this.btnBrisanjePrometa.Text = "Brisanje prometa";
            this.btnBrisanjePrometa.UseVisualStyleBackColor = false;
            this.btnBrisanjePrometa.Click += new System.EventHandler(this.btnBrisanjePrometa_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtObracun);
            this.panel1.Controls.Add(this.dtpDonosOD);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPlus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnMinius);
            this.panel1.Location = new System.Drawing.Point(-3, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 85);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtPrometKase);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtPrometBrojano);
            this.panel2.Location = new System.Drawing.Point(360, 531);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 74);
            this.panel2.TabIndex = 29;
            // 
            // frmUskladenjePrometa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1003, 586);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBrisanjePrometa);
            this.Controls.Add(this.btnBrojcanikKave);
            this.Controls.Add(this.dgvD);
            this.Controls.Add(this.btnObradiPodatke);
            this.Controls.Add(this.btnUcitajPodatke);
            this.Controls.Add(this.dgv);
            this.Name = "frmUskladenjePrometa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usklađenje prometa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUskladenjePrometa_FormClosing);
            this.Load += new System.EventHandler(this.frmUskladenjePrometa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDonosOD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUcitajPodatke;
        private System.Windows.Forms.Button btnObradiPodatke;
        public System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrometKase;
        private System.Windows.Forms.TextBox txtPrometBrojano;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvD;
        private System.Windows.Forms.DataGridViewTextBoxColumn djelatnik;
        private System.Windows.Forms.DataGridViewCheckBoxColumn oznaci;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Button btnBrojcanikKave;
        private System.Windows.Forms.Button btnMinius;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.TextBox txtObracun;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina_skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn _brojcanik;
        private System.Windows.Forms.DataGridViewTextBoxColumn unos_robe;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodaja_kucano;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodaja_prodano;
        private System.Windows.Forms.DataGridViewTextBoxColumn stanje;
        private System.Windows.Forms.DataGridViewTextBoxColumn razlika;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznoskuna;
        private System.Windows.Forms.DataGridViewTextBoxColumn stanje_prijenos;
        private System.Windows.Forms.DataGridViewTextBoxColumn brojcanik_prijenos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn donos_brojcanik1;
        private System.Windows.Forms.DataGridViewTextBoxColumn donos_brojcanik2;
        private System.Windows.Forms.DataGridViewTextBoxColumn brojcanik_kraj_dana1;
        private System.Windows.Forms.DataGridViewTextBoxColumn brojcanik_kraj_dana2;
        private System.Windows.Forms.Button btnBrisanjePrometa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}