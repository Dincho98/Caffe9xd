namespace PCPOS
{
    partial class frmKarticaSkladiste
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chbSifraArtikla = new System.Windows.Forms.CheckBox();
            this.txtImeArtikla = new System.Windows.Forms.TextBox();
            this.txtSifraArtikla = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chbDobavljac = new System.Windows.Forms.CheckBox();
            this.chbVD = new System.Windows.Forms.CheckBox();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.Partner = new System.Windows.Forms.Label();
            this.txtSifraDob = new System.Windows.Forms.TextBox();
            this.btnArtikli = new System.Windows.Forms.Button();
            this.cbSkl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtNazivDob = new System.Windows.Forms.TextBox();
            this.cbVD = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbTrgovackaRoba = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbHrana = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbPice = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbSifraArtikla
            // 
            this.chbSifraArtikla.AutoSize = true;
            this.chbSifraArtikla.Location = new System.Drawing.Point(368, 67);
            this.chbSifraArtikla.Name = "chbSifraArtikla";
            this.chbSifraArtikla.Size = new System.Drawing.Size(15, 14);
            this.chbSifraArtikla.TabIndex = 78;
            this.chbSifraArtikla.UseVisualStyleBackColor = true;
            // 
            // txtImeArtikla
            // 
            this.txtImeArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImeArtikla.Location = new System.Drawing.Point(102, 92);
            this.txtImeArtikla.Name = "txtImeArtikla";
            this.txtImeArtikla.ReadOnly = true;
            this.txtImeArtikla.Size = new System.Drawing.Size(260, 24);
            this.txtImeArtikla.TabIndex = 77;
            this.txtImeArtikla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImeArtikla_KeyDown_1);
            // 
            // txtSifraArtikla
            // 
            this.txtSifraArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraArtikla.Location = new System.Drawing.Point(102, 63);
            this.txtSifraArtikla.Name = "txtSifraArtikla";
            this.txtSifraArtikla.Size = new System.Drawing.Size(226, 23);
            this.txtSifraArtikla.TabIndex = 75;
            this.txtSifraArtikla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraArtikla_KeyDown_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(334, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 74;
            this.label2.Text = "Šifra artikla:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(7, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 73;
            this.label3.Text = "Naziv artikla:";
            // 
            // chbDobavljac
            // 
            this.chbDobavljac.AutoSize = true;
            this.chbDobavljac.Location = new System.Drawing.Point(776, 67);
            this.chbDobavljac.Name = "chbDobavljac";
            this.chbDobavljac.Size = new System.Drawing.Size(15, 14);
            this.chbDobavljac.TabIndex = 72;
            this.chbDobavljac.UseVisualStyleBackColor = true;
            // 
            // chbVD
            // 
            this.chbVD.AutoSize = true;
            this.chbVD.Location = new System.Drawing.Point(776, 36);
            this.chbVD.Name = "chbVD";
            this.chbVD.Size = new System.Drawing.Size(15, 14);
            this.chbVD.TabIndex = 72;
            this.chbVD.UseVisualStyleBackColor = true;
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.Location = new System.Drawing.Point(368, 37);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 72;
            this.chbSkladiste.UseVisualStyleBackColor = true;
            // 
            // Partner
            // 
            this.Partner.AutoSize = true;
            this.Partner.BackColor = System.Drawing.Color.Transparent;
            this.Partner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Partner.Location = new System.Drawing.Point(430, 66);
            this.Partner.Name = "Partner";
            this.Partner.Size = new System.Drawing.Size(74, 17);
            this.Partner.TabIndex = 71;
            this.Partner.Text = "Dobavljač:";
            // 
            // txtSifraDob
            // 
            this.txtSifraDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraDob.Location = new System.Drawing.Point(510, 63);
            this.txtSifraDob.Name = "txtSifraDob";
            this.txtSifraDob.Size = new System.Drawing.Size(226, 23);
            this.txtSifraDob.TabIndex = 69;
            this.txtSifraDob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraDob_KeyDown);
            // 
            // btnArtikli
            // 
            this.btnArtikli.BackColor = System.Drawing.Color.White;
            this.btnArtikli.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnArtikli.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnArtikli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnArtikli.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnArtikli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArtikli.Location = new System.Drawing.Point(742, 63);
            this.btnArtikli.Name = "btnArtikli";
            this.btnArtikli.Size = new System.Drawing.Size(28, 23);
            this.btnArtikli.TabIndex = 70;
            this.btnArtikli.Text = "...";
            this.btnArtikli.UseVisualStyleBackColor = false;
            this.btnArtikli.Click += new System.EventHandler(this.btnArtikli_Click);
            // 
            // cbSkl
            // 
            this.cbSkl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSkl.FormattingEnabled = true;
            this.cbSkl.Location = new System.Drawing.Point(102, 32);
            this.cbSkl.Name = "cbSkl";
            this.cbSkl.Size = new System.Drawing.Size(260, 24);
            this.cbSkl.TabIndex = 68;
            this.cbSkl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSkl_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 67;
            this.label1.Text = "Skladiste:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(831, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 66;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Traži");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // txtNazivDob
            // 
            this.txtNazivDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNazivDob.Location = new System.Drawing.Point(510, 91);
            this.txtNazivDob.Name = "txtNazivDob";
            this.txtNazivDob.ReadOnly = true;
            this.txtNazivDob.Size = new System.Drawing.Size(260, 24);
            this.txtNazivDob.TabIndex = 60;
            // 
            // cbVD
            // 
            this.cbVD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbVD.FormattingEnabled = true;
            this.cbVD.Location = new System.Drawing.Point(510, 32);
            this.cbVD.Name = "cbVD";
            this.cbVD.Size = new System.Drawing.Size(260, 24);
            this.cbVD.TabIndex = 65;
            this.cbVD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbVD_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label17.Location = new System.Drawing.Point(415, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 17);
            this.label17.TabIndex = 61;
            this.label17.Text = "Grupa robe.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(425, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 53;
            this.label5.Text = "Naziv dob.:";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.kolicina,
            this.nbc,
            this.mpc,
            this.skladiste});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 184);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(962, 466);
            this.dgw.TabIndex = 59;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // nbc
            // 
            this.nbc.HeaderText = "NBC";
            this.nbc.Name = "nbc";
            // 
            // mpc
            // 
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            this.mpc.Visible = false;
            // 
            // skladiste
            // 
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(844, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 38);
            this.button2.TabIndex = 61;
            this.button2.Text = "Izlaz      ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cbTrgovackaRoba);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cbHrana);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbPice);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpDoDatuma);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chbSifraArtikla);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtImeArtikla);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtSifraArtikla);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cbVD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNazivDob);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.chbDobavljac);
            this.panel1.Controls.Add(this.cbSkl);
            this.panel1.Controls.Add(this.chbVD);
            this.panel1.Controls.Add(this.btnArtikli);
            this.panel1.Controls.Add(this.chbSkladiste);
            this.panel1.Controls.Add(this.txtSifraDob);
            this.panel1.Controls.Add(this.Partner);
            this.panel1.Location = new System.Drawing.Point(12, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 122);
            this.panel1.TabIndex = 62;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(639, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 87;
            this.label9.Text = "Trgovačka roba";
            // 
            // cbTrgovackaRoba
            // 
            this.cbTrgovackaRoba.AutoSize = true;
            this.cbTrgovackaRoba.Location = new System.Drawing.Point(624, 10);
            this.cbTrgovackaRoba.Name = "cbTrgovackaRoba";
            this.cbTrgovackaRoba.Size = new System.Drawing.Size(15, 14);
            this.cbTrgovackaRoba.TabIndex = 86;
            this.cbTrgovackaRoba.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(576, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 17);
            this.label8.TabIndex = 85;
            this.label8.Text = "Hrana";
            // 
            // cbHrana
            // 
            this.cbHrana.AutoSize = true;
            this.cbHrana.Location = new System.Drawing.Point(561, 10);
            this.cbHrana.Name = "cbHrana";
            this.cbHrana.Size = new System.Drawing.Size(15, 14);
            this.cbHrana.TabIndex = 84;
            this.cbHrana.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(525, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 17);
            this.label7.TabIndex = 83;
            this.label7.Text = "Piće";
            // 
            // cbPice
            // 
            this.cbPice.AutoSize = true;
            this.cbPice.Location = new System.Drawing.Point(510, 10);
            this.cbPice.Name = "cbPice";
            this.cbPice.Size = new System.Drawing.Size(15, 14);
            this.cbPice.TabIndex = 82;
            this.cbPice.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(430, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 81;
            this.label6.Text = "Podgrupa:";
            // 
            // dtpDoDatuma
            // 
            this.dtpDoDatuma.CustomFormat = "dd.MM.yyyy  HH:mm:ss";
            this.dtpDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDatuma.Location = new System.Drawing.Point(103, 4);
            this.dtpDoDatuma.Name = "dtpDoDatuma";
            this.dtpDoDatuma.Size = new System.Drawing.Size(259, 23);
            this.dtpDoDatuma.TabIndex = 80;
            this.dtpDoDatuma.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpDoDatuma_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(15, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 79;
            this.label4.Text = "Do datuma:";
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
            this.btnIspis.Location = new System.Drawing.Point(12, 10);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(97, 40);
            this.btnIspis.TabIndex = 63;
            this.btnIspis.Text = "Ispis  ";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // frmKarticaSkladiste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(986, 662);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgw);
            this.Name = "frmKarticaSkladiste";
            this.Text = "Kartica skladiste";
            this.Activated += new System.EventHandler(this.frmKarticaSkladiste_Activated);
            this.Load += new System.EventHandler(this.frmKarticaSkladiste_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbVD;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.ComboBox cbSkl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Partner;
        private System.Windows.Forms.TextBox txtSifraDob;
        private System.Windows.Forms.Button btnArtikli;
        private System.Windows.Forms.TextBox txtNazivDob;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbSifraArtikla;
        private System.Windows.Forms.TextBox txtImeArtikla;
        private System.Windows.Forms.TextBox txtSifraArtikla;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbDobavljac;
        private System.Windows.Forms.CheckBox chbVD;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.DateTimePicker dtpDoDatuma;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbHrana;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbPice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbTrgovackaRoba;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
    }
}