namespace PCPOS.Caffe
{
    partial class frmBackOffice
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblNaslov = new System.Windows.Forms.Label();
            this.lblUkupno = new System.Windows.Forms.Label();
            this.lblGotovina = new System.Windows.Forms.Label();
            this.lblKartice = new System.Windows.Forms.Label();
            this.flpArtikli = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblFakture = new System.Windows.Forms.Label();
            this.lblOstalo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnRobaNaSkladistu = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnBlagajnickiIzvjestaj = new System.Windows.Forms.Button();
            this.btnKarticaKupca = new System.Windows.Forms.Button();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOtpremnice = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgwUtroseniMaterijal = new System.Windows.Forms.DataGridView();
            this.sifra_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwUtroseniMaterijal)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNaslov
            // 
            this.lblNaslov.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNaslov.AutoSize = true;
            this.lblNaslov.BackColor = System.Drawing.Color.Transparent;
            this.lblNaslov.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblNaslov.Location = new System.Drawing.Point(12, 5);
            this.lblNaslov.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(121, 62);
            this.lblNaslov.TabIndex = 126;
            this.lblNaslov.Text = "Dnevni izvještaj";
            // 
            // lblUkupno
            // 
            this.lblUkupno.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUkupno.AutoSize = true;
            this.lblUkupno.BackColor = System.Drawing.Color.Silver;
            this.lblUkupno.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUkupno.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUkupno.Location = new System.Drawing.Point(744, 57);
            this.lblUkupno.Name = "lblUkupno";
            this.lblUkupno.Size = new System.Drawing.Size(75, 18);
            this.lblUkupno.TabIndex = 127;
            this.lblUkupno.Text = "Ukupno";
            // 
            // lblGotovina
            // 
            this.lblGotovina.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGotovina.AutoSize = true;
            this.lblGotovina.BackColor = System.Drawing.Color.Silver;
            this.lblGotovina.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblGotovina.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGotovina.Location = new System.Drawing.Point(11, 57);
            this.lblGotovina.Name = "lblGotovina";
            this.lblGotovina.Size = new System.Drawing.Size(81, 18);
            this.lblGotovina.TabIndex = 127;
            this.lblGotovina.Text = "Gotovina";
            // 
            // lblKartice
            // 
            this.lblKartice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKartice.AutoSize = true;
            this.lblKartice.BackColor = System.Drawing.Color.Silver;
            this.lblKartice.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblKartice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblKartice.Location = new System.Drawing.Point(11, 77);
            this.lblKartice.Name = "lblKartice";
            this.lblKartice.Size = new System.Drawing.Size(65, 18);
            this.lblKartice.TabIndex = 127;
            this.lblKartice.Text = "Kartice";
            // 
            // flpArtikli
            // 
            this.flpArtikli.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpArtikli.AutoScroll = true;
            this.flpArtikli.BackColor = System.Drawing.Color.Silver;
            this.flpArtikli.Location = new System.Drawing.Point(10, 418);
            this.flpArtikli.Name = "flpArtikli";
            this.flpArtikli.Size = new System.Drawing.Size(477, 187);
            this.flpArtikli.TabIndex = 128;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 31);
            this.label1.TabIndex = 131;
            this.label1.Text = "Zaposlenici";
            // 
            // dtpOD
            // 
            this.dtpOD.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpOD.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(8, 22);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(192, 29);
            this.dtpOD.TabIndex = 132;
            this.dtpOD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // dtpDO
            // 
            this.dtpDO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpDO.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(209, 22);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(187, 29);
            this.dtpDO.TabIndex = 132;
            this.dtpDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 133;
            this.label2.Text = "Od datuma";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(208, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 133;
            this.label3.Text = "Do datuma";
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.AllowUserToOrderColumns = true;
            this.dgw.AllowUserToResizeRows = false;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgw.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.kolicina});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EnableHeadersVisualStyles = false;
            this.dgw.GridColor = System.Drawing.Color.Silver;
            this.dgw.Location = new System.Drawing.Point(10, 130);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersVisible = false;
            this.dgw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(477, 248);
            this.dgw.TabIndex = 135;
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
            this.naziv.Width = 250;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            this.kolicina.ReadOnly = true;
            this.kolicina.Width = 125;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(489, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 136;
            this.label4.Text = "Utrošeni materijal";
            // 
            // Chart1
            // 
            this.Chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Chart1.BackColor = System.Drawing.Color.Silver;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea1.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart1.Legends.Add(legend1);
            this.Chart1.Location = new System.Drawing.Point(493, 418);
            this.Chart1.Name = "Chart1";
            this.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.Chart1.Size = new System.Drawing.Size(465, 187);
            this.Chart1.TabIndex = 127;
            this.Chart1.Text = "chart1";
            // 
            // lblFakture
            // 
            this.lblFakture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFakture.AutoSize = true;
            this.lblFakture.BackColor = System.Drawing.Color.Silver;
            this.lblFakture.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFakture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFakture.Location = new System.Drawing.Point(255, 57);
            this.lblFakture.Name = "lblFakture";
            this.lblFakture.Size = new System.Drawing.Size(68, 18);
            this.lblFakture.TabIndex = 138;
            this.lblFakture.Text = "Fakture";
            // 
            // lblOstalo
            // 
            this.lblOstalo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOstalo.AutoSize = true;
            this.lblOstalo.BackColor = System.Drawing.Color.Silver;
            this.lblOstalo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOstalo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOstalo.Location = new System.Drawing.Point(519, 57);
            this.lblOstalo.Name = "lblOstalo";
            this.lblOstalo.Size = new System.Drawing.Size(62, 18);
            this.lblOstalo.TabIndex = 137;
            this.lblOstalo.Text = "Ostalo";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.btnRobaNaSkladistu);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.btnBlagajnickiIzvjestaj);
            this.flowLayoutPanel1.Controls.Add(this.btnKarticaKupca);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(139, 9);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(746, 56);
            this.flowLayoutPanel1.TabIndex = 140;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 47);
            this.button3.TabIndex = 124;
            this.button3.Text = "Ispis prometa na POS printer";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // btnRobaNaSkladistu
            // 
            this.btnRobaNaSkladistu.BackColor = System.Drawing.Color.White;
            this.btnRobaNaSkladistu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRobaNaSkladistu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRobaNaSkladistu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnRobaNaSkladistu.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRobaNaSkladistu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnRobaNaSkladistu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRobaNaSkladistu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRobaNaSkladistu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRobaNaSkladistu.ForeColor = System.Drawing.Color.Black;
            this.btnRobaNaSkladistu.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRobaNaSkladistu.Location = new System.Drawing.Point(142, 3);
            this.btnRobaNaSkladistu.Name = "btnRobaNaSkladistu";
            this.btnRobaNaSkladistu.Size = new System.Drawing.Size(78, 47);
            this.btnRobaNaSkladistu.TabIndex = 124;
            this.btnRobaNaSkladistu.Text = "Roba na skladištu";
            this.btnRobaNaSkladistu.UseVisualStyleBackColor = false;
            this.btnRobaNaSkladistu.Click += new System.EventHandler(this.btnRobaNaSkladistu_Click);
            this.btnRobaNaSkladistu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(226, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 47);
            this.button2.TabIndex = 124;
            this.button2.Text = "Pregled po računima";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button4.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button4.Location = new System.Drawing.Point(327, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 47);
            this.button4.TabIndex = 124;
            this.button4.Text = "Neuspjele fiskalizacije";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // btnBlagajnickiIzvjestaj
            // 
            this.btnBlagajnickiIzvjestaj.BackColor = System.Drawing.Color.White;
            this.btnBlagajnickiIzvjestaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBlagajnickiIzvjestaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlagajnickiIzvjestaj.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnBlagajnickiIzvjestaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnBlagajnickiIzvjestaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnBlagajnickiIzvjestaj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnBlagajnickiIzvjestaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlagajnickiIzvjestaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBlagajnickiIzvjestaj.ForeColor = System.Drawing.Color.Black;
            this.btnBlagajnickiIzvjestaj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBlagajnickiIzvjestaj.Location = new System.Drawing.Point(426, 3);
            this.btnBlagajnickiIzvjestaj.Name = "btnBlagajnickiIzvjestaj";
            this.btnBlagajnickiIzvjestaj.Size = new System.Drawing.Size(90, 47);
            this.btnBlagajnickiIzvjestaj.TabIndex = 145;
            this.btnBlagajnickiIzvjestaj.Text = "Blagajnički izvještaj";
            this.btnBlagajnickiIzvjestaj.UseVisualStyleBackColor = false;
            this.btnBlagajnickiIzvjestaj.Click += new System.EventHandler(this.btnBlagajnickiIzvjestaj_Click);
            // 
            // btnKarticaKupca
            // 
            this.btnKarticaKupca.BackColor = System.Drawing.Color.White;
            this.btnKarticaKupca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKarticaKupca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKarticaKupca.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnKarticaKupca.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnKarticaKupca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnKarticaKupca.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnKarticaKupca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKarticaKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnKarticaKupca.ForeColor = System.Drawing.Color.Black;
            this.btnKarticaKupca.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnKarticaKupca.Location = new System.Drawing.Point(522, 3);
            this.btnKarticaKupca.Name = "btnKarticaKupca";
            this.btnKarticaKupca.Size = new System.Drawing.Size(113, 47);
            this.btnKarticaKupca.TabIndex = 149;
            this.btnKarticaKupca.Text = "Kartica kupca";
            this.btnKarticaKupca.UseVisualStyleBackColor = false;
            this.btnKarticaKupca.Click += new System.EventHandler(this.btnKarticaKupca_Click);
            // 
            // btnTrazi
            // 
            this.btnTrazi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTrazi.BackColor = System.Drawing.Color.White;
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTrazi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrazi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnTrazi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnTrazi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnTrazi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnTrazi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnTrazi.ForeColor = System.Drawing.Color.Black;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnTrazi.Location = new System.Drawing.Point(402, 22);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(71, 29);
            this.btnTrazi.TabIndex = 134;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = false;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            this.btnTrazi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button6.Location = new System.Drawing.Point(891, 9);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(98, 56);
            this.button6.TabIndex = 130;
            this.button6.Text = "Izlaz ESC";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUnosNormativa_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblOtpremnice);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dgwUtroseniMaterijal);
            this.panel1.Controls.Add(this.lblOstalo);
            this.panel1.Controls.Add(this.btnTrazi);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Chart1);
            this.panel1.Controls.Add(this.lblFakture);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblKartice);
            this.panel1.Controls.Add(this.flpArtikli);
            this.panel1.Controls.Add(this.lblGotovina);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblUkupno);
            this.panel1.Controls.Add(this.dtpDO);
            this.panel1.Controls.Add(this.dgw);
            this.panel1.Controls.Add(this.dtpOD);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 610);
            this.panel1.TabIndex = 141;
            // 
            // lblOtpremnice
            // 
            this.lblOtpremnice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOtpremnice.AutoSize = true;
            this.lblOtpremnice.BackColor = System.Drawing.Color.Silver;
            this.lblOtpremnice.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOtpremnice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOtpremnice.Location = new System.Drawing.Point(255, 77);
            this.lblOtpremnice.Name = "lblOtpremnice";
            this.lblOtpremnice.Size = new System.Drawing.Size(102, 18);
            this.lblOtpremnice.TabIndex = 141;
            this.lblOtpremnice.Text = "Otpremnice";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(7, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 17);
            this.label5.TabIndex = 140;
            this.label5.Text = "Prodani artikli";
            // 
            // dgwUtroseniMaterijal
            // 
            this.dgwUtroseniMaterijal.AllowUserToAddRows = false;
            this.dgwUtroseniMaterijal.AllowUserToDeleteRows = false;
            this.dgwUtroseniMaterijal.AllowUserToOrderColumns = true;
            this.dgwUtroseniMaterijal.AllowUserToResizeRows = false;
            this.dgwUtroseniMaterijal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgwUtroseniMaterijal.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwUtroseniMaterijal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgwUtroseniMaterijal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwUtroseniMaterijal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra_prodaja,
            this.naziv_prodaja,
            this.kolicina_prodaja});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwUtroseniMaterijal.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgwUtroseniMaterijal.EnableHeadersVisualStyles = false;
            this.dgwUtroseniMaterijal.GridColor = System.Drawing.Color.Silver;
            this.dgwUtroseniMaterijal.Location = new System.Drawing.Point(492, 130);
            this.dgwUtroseniMaterijal.MultiSelect = false;
            this.dgwUtroseniMaterijal.Name = "dgwUtroseniMaterijal";
            this.dgwUtroseniMaterijal.ReadOnly = true;
            this.dgwUtroseniMaterijal.RowHeadersVisible = false;
            this.dgwUtroseniMaterijal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgwUtroseniMaterijal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwUtroseniMaterijal.Size = new System.Drawing.Size(471, 248);
            this.dgwUtroseniMaterijal.TabIndex = 139;
            // 
            // sifra_prodaja
            // 
            this.sifra_prodaja.HeaderText = "Šifra";
            this.sifra_prodaja.Name = "sifra_prodaja";
            this.sifra_prodaja.ReadOnly = true;
            // 
            // naziv_prodaja
            // 
            this.naziv_prodaja.HeaderText = "Naziv";
            this.naziv_prodaja.Name = "naziv_prodaja";
            this.naziv_prodaja.ReadOnly = true;
            this.naziv_prodaja.Width = 250;
            // 
            // kolicina_prodaja
            // 
            this.kolicina_prodaja.HeaderText = "Količina";
            this.kolicina_prodaja.Name = "kolicina_prodaja";
            this.kolicina_prodaja.ReadOnly = true;
            this.kolicina_prodaja.Width = 125;
            // 
            // frmBackOffice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.CancelButton = this.button6;
            this.ClientSize = new System.Drawing.Size(1001, 700);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNaslov);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBackOffice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Back Office";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBackOffice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwUtroseniMaterijal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRobaNaSkladistu;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.Label lblUkupno;
        private System.Windows.Forms.Label lblGotovina;
        private System.Windows.Forms.Label lblKartice;
        private System.Windows.Forms.FlowLayoutPanel flpArtikli;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private System.Windows.Forms.Label lblFakture;
        private System.Windows.Forms.Label lblOstalo;
        private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnBlagajnickiIzvjestaj;
        private System.Windows.Forms.Button btnKarticaKupca;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgwUtroseniMaterijal;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra_prodaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_prodaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina_prodaja;
        private System.Windows.Forms.Label lblOtpremnice;
    }
}