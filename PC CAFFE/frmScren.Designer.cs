namespace PCPOS
{
    partial class frmScren
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.lblSat = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojDana = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMaloprodaja = new System.Windows.Forms.CheckBox();
            this.chbFakture = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTrenutnaGodina = new System.Windows.Forms.Label();
            this.btnPromjenaG = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMaloprodaja = new System.Windows.Forms.Button();
            this.timerUpozoranaNaKrivuGodinu = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.kalkulacijeButton = new System.Windows.Forms.Button();
            this.btnFakture = new System.Windows.Forms.Button();
            this.btnOdjava = new System.Windows.Forms.Button();
            this.btnPodrska = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDatum = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPostojeNeuspjeleFiskalizacije = new System.Windows.Forms.Label();
            this.labelHitnoNazoviteCodeIt = new System.Windows.Forms.Label();
            this.labelPreostaloVrijeme = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monthCalendar1.Location = new System.Drawing.Point(59, 9);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.monthCalendar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.monthCalendar1_KeyDown);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(9, 218);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(175, 18);
            this.label11.TabIndex = 3;
            this.label11.Text = "Kartice  ukupno: 0.00 kn";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(195, 18);
            this.label10.TabIndex = 2;
            this.label10.Text = "Blagajna gotovina : 0.00 kn";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(9, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(178, 18);
            this.label9.TabIndex = 4;
            this.label9.Text = "Fakture  ukupno: 0.00 kn";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(9, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "Maloprodaja ukupno: 0.00 kn";
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
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button4.Location = new System.Drawing.Point(59, 267);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(199, 37);
            this.button4.TabIndex = 5;
            this.button4.Text = "Osvježi";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button1_Click);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 100000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(23, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 17);
            this.label12.TabIndex = 6;
            this.label12.Text = "Odjava";
            // 
            // lblSat
            // 
            this.lblSat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSat.BackColor = System.Drawing.Color.Transparent;
            this.lblSat.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.lblSat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.lblSat.Location = new System.Drawing.Point(908, 8);
            this.lblSat.Name = "lblSat";
            this.lblSat.Size = new System.Drawing.Size(209, 36);
            this.lblSat.TabIndex = 13;
            this.lblSat.Text = "08:52:48";
            this.lblSat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Chart1
            // 
            this.Chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea2.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea2);
            this.Chart1.Location = new System.Drawing.Point(0, 49);
            this.Chart1.Name = "Chart1";
            this.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 2;
            this.Chart1.Series.Add(series2);
            this.Chart1.Size = new System.Drawing.Size(607, 268);
            this.Chart1.TabIndex = 5;
            this.Chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(67, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Prodaja zadnjih";
            // 
            // txtBrojDana
            // 
            this.txtBrojDana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBrojDana.Location = new System.Drawing.Point(188, 22);
            this.txtBrojDana.Name = "txtBrojDana";
            this.txtBrojDana.Size = new System.Drawing.Size(48, 20);
            this.txtBrojDana.TabIndex = 1;
            this.txtBrojDana.TextChanged += new System.EventHandler(this.txtBrojDana_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(242, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "dana";
            // 
            // cbMaloprodaja
            // 
            this.cbMaloprodaja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMaloprodaja.AutoSize = true;
            this.cbMaloprodaja.Checked = true;
            this.cbMaloprodaja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMaloprodaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbMaloprodaja.Location = new System.Drawing.Point(394, 22);
            this.cbMaloprodaja.Name = "cbMaloprodaja";
            this.cbMaloprodaja.Size = new System.Drawing.Size(105, 21);
            this.cbMaloprodaja.TabIndex = 3;
            this.cbMaloprodaja.Text = "Maloprodaja";
            this.cbMaloprodaja.UseVisualStyleBackColor = true;
            this.cbMaloprodaja.CheckedChanged += new System.EventHandler(this.cbMaloprodaja_CheckedChanged);
            // 
            // chbFakture
            // 
            this.chbFakture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbFakture.AutoSize = true;
            this.chbFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbFakture.Location = new System.Drawing.Point(503, 22);
            this.chbFakture.Name = "chbFakture";
            this.chbFakture.Size = new System.Drawing.Size(75, 21);
            this.chbFakture.TabIndex = 4;
            this.chbFakture.Text = "Fakture";
            this.chbFakture.UseVisualStyleBackColor = true;
            this.chbFakture.CheckedChanged += new System.EventHandler(this.chbFakture_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(179, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Podrška";
            // 
            // lblTrenutnaGodina
            // 
            this.lblTrenutnaGodina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrenutnaGodina.AutoSize = true;
            this.lblTrenutnaGodina.BackColor = System.Drawing.Color.Transparent;
            this.lblTrenutnaGodina.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTrenutnaGodina.ForeColor = System.Drawing.Color.Black;
            this.lblTrenutnaGodina.Location = new System.Drawing.Point(30, 11);
            this.lblTrenutnaGodina.Name = "lblTrenutnaGodina";
            this.lblTrenutnaGodina.Size = new System.Drawing.Size(203, 19);
            this.lblTrenutnaGodina.TabIndex = 0;
            this.lblTrenutnaGodina.Text = "Trenutno koristite 2012 g:";
            // 
            // btnPromjenaG
            // 
            this.btnPromjenaG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPromjenaG.BackColor = System.Drawing.Color.White;
            this.btnPromjenaG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPromjenaG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPromjenaG.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPromjenaG.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPromjenaG.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromjenaG.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPromjenaG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromjenaG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPromjenaG.ForeColor = System.Drawing.Color.Black;
            this.btnPromjenaG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPromjenaG.Location = new System.Drawing.Point(15, 33);
            this.btnPromjenaG.Name = "btnPromjenaG";
            this.btnPromjenaG.Size = new System.Drawing.Size(232, 37);
            this.btnPromjenaG.TabIndex = 1;
            this.btnPromjenaG.Text = "Promjeni godinu";
            this.btnPromjenaG.UseVisualStyleBackColor = false;
            this.btnPromjenaG.Click += new System.EventHandler(this.btnPromjenaG_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button6.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button6.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button6.Location = new System.Drawing.Point(786, 32);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 83);
            this.button6.TabIndex = 5;
            this.button6.Text = "Partneri";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.picPartner_Click);
            this.button6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button5.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button5.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button5.Location = new System.Drawing.Point(661, 32);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 83);
            this.button5.TabIndex = 4;
            this.button5.Text = "Repromaterijal";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.picRoba_Click);
            this.button5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.Location = new System.Drawing.Point(536, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 83);
            this.button3.TabIndex = 3;
            this.button3.Text = "Artikli";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.picPonude_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(411, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 83);
            this.button2.TabIndex = 2;
            this.button2.Text = "Računi";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.picFak_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(161, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 83);
            this.button1.TabIndex = 1;
            this.button1.Text = "Primka";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.picKalk_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // btnMaloprodaja
            // 
            this.btnMaloprodaja.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMaloprodaja.BackColor = System.Drawing.Color.White;
            this.btnMaloprodaja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaloprodaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaloprodaja.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnMaloprodaja.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMaloprodaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnMaloprodaja.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnMaloprodaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaloprodaja.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnMaloprodaja.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnMaloprodaja.Location = new System.Drawing.Point(36, 32);
            this.btnMaloprodaja.Name = "btnMaloprodaja";
            this.btnMaloprodaja.Size = new System.Drawing.Size(122, 83);
            this.btnMaloprodaja.TabIndex = 0;
            this.btnMaloprodaja.Text = "Prodaja\r\nENTER";
            this.btnMaloprodaja.UseVisualStyleBackColor = false;
            this.btnMaloprodaja.Click += new System.EventHandler(this.picMaloprodaj_Click);
            this.btnMaloprodaja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMaloprodaja_KeyDown);
            // 
            // timerUpozoranaNaKrivuGodinu
            // 
            this.timerUpozoranaNaKrivuGodinu.Tick += new System.EventHandler(this.timerUpozoranaNaKrivuGodinu_Tick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(521, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(381, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Verzija programa: 1.3";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.kalkulacijeButton);
            this.panel1.Controls.Add(this.btnFakture);
            this.panel1.Controls.Add(this.btnMaloprodaja);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Location = new System.Drawing.Point(30, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 150);
            this.panel1.TabIndex = 0;
            // 
            // kalkulacijeButton
            // 
            this.kalkulacijeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kalkulacijeButton.BackColor = System.Drawing.Color.White;
            this.kalkulacijeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.kalkulacijeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kalkulacijeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.kalkulacijeButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.kalkulacijeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.kalkulacijeButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.kalkulacijeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kalkulacijeButton.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.kalkulacijeButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.kalkulacijeButton.Location = new System.Drawing.Point(286, 32);
            this.kalkulacijeButton.Name = "kalkulacijeButton";
            this.kalkulacijeButton.Size = new System.Drawing.Size(122, 83);
            this.kalkulacijeButton.TabIndex = 7;
            this.kalkulacijeButton.Text = "Kalkulacije";
            this.kalkulacijeButton.UseVisualStyleBackColor = false;
            this.kalkulacijeButton.Click += new System.EventHandler(this.kalkulacijeButton_Click);
            // 
            // btnFakture
            // 
            this.btnFakture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFakture.BackColor = System.Drawing.Color.White;
            this.btnFakture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFakture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFakture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnFakture.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnFakture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnFakture.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnFakture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFakture.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnFakture.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFakture.Location = new System.Drawing.Point(911, 32);
            this.btnFakture.Name = "btnFakture";
            this.btnFakture.Size = new System.Drawing.Size(122, 83);
            this.btnFakture.TabIndex = 6;
            this.btnFakture.Text = "Fakture";
            this.btnFakture.UseVisualStyleBackColor = false;
            this.btnFakture.Click += new System.EventHandler(this.btnFakture_Click);
            // 
            // btnOdjava
            // 
            this.btnOdjava.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdjava.BackgroundImage = global::PCPOS.Properties.Resources.ikona_logout;
            this.btnOdjava.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOdjava.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdjava.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOdjava.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdjava.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdjava.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdjava.Location = new System.Drawing.Point(12, 8);
            this.btnOdjava.Name = "btnOdjava";
            this.btnOdjava.Size = new System.Drawing.Size(74, 63);
            this.btnOdjava.TabIndex = 4;
            this.btnOdjava.UseVisualStyleBackColor = false;
            this.btnOdjava.Click += new System.EventHandler(this.btnOdjava_Click);
            // 
            // btnPodrska
            // 
            this.btnPodrska.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPodrska.BackgroundImage = global::PCPOS.Properties.Resources.ikona_podrska;
            this.btnPodrska.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPodrska.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPodrska.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPodrska.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPodrska.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPodrska.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPodrska.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPodrska.Location = new System.Drawing.Point(172, 8);
            this.btnPodrska.Name = "btnPodrska";
            this.btnPodrska.Size = new System.Drawing.Size(74, 63);
            this.btnPodrska.TabIndex = 8;
            this.btnPodrska.UseVisualStyleBackColor = false;
            this.btnPodrska.Click += new System.EventHandler(this.btnPodrska_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnInfo.BackgroundImage = global::PCPOS.Properties.Resources.ikona_info;
            this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnInfo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Location = new System.Drawing.Point(92, 8);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(74, 63);
            this.btnInfo.TabIndex = 5;
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTrenutnaGodina);
            this.panel2.Controls.Add(this.btnPromjenaG);
            this.panel2.Location = new System.Drawing.Point(252, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 76);
            this.panel2.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(114, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Info";
            // 
            // lblDatum
            // 
            this.lblDatum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatum.BackColor = System.Drawing.Color.Transparent;
            this.lblDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.lblDatum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.lblDatum.Location = new System.Drawing.Point(908, 46);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(209, 30);
            this.lblDatum.TabIndex = 14;
            this.lblDatum.Text = "06.04.2016.";
            this.lblDatum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtBrojDana);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cbMaloprodaja);
            this.panel3.Controls.Add(this.chbFakture);
            this.panel3.Controls.Add(this.Chart1);
            this.panel3.Location = new System.Drawing.Point(505, 263);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(612, 330);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.monthCalendar1);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Location = new System.Drawing.Point(13, 263);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(318, 330);
            this.panel4.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.label7.Location = new System.Drawing.Point(520, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 19);
            this.label7.TabIndex = 11;
            this.label7.Text = "by Code - iT";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.label5.Location = new System.Drawing.Point(519, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 36);
            this.label5.TabIndex = 10;
            this.label5.Text = "PC POS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPostojeNeuspjeleFiskalizacije
            // 
            this.labelPostojeNeuspjeleFiskalizacije.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPostojeNeuspjeleFiskalizacije.BackColor = System.Drawing.Color.Transparent;
            this.labelPostojeNeuspjeleFiskalizacije.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPostojeNeuspjeleFiskalizacije.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPostojeNeuspjeleFiskalizacije.Location = new System.Drawing.Point(673, 9);
            this.labelPostojeNeuspjeleFiskalizacije.Name = "labelPostojeNeuspjeleFiskalizacije";
            this.labelPostojeNeuspjeleFiskalizacije.Size = new System.Drawing.Size(254, 23);
            this.labelPostojeNeuspjeleFiskalizacije.TabIndex = 15;
            this.labelPostojeNeuspjeleFiskalizacije.Text = "Postoje neuspjele fiskalizacije!";
            this.labelPostojeNeuspjeleFiskalizacije.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelHitnoNazoviteCodeIt
            // 
            this.labelHitnoNazoviteCodeIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHitnoNazoviteCodeIt.BackColor = System.Drawing.Color.Transparent;
            this.labelHitnoNazoviteCodeIt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHitnoNazoviteCodeIt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelHitnoNazoviteCodeIt.Location = new System.Drawing.Point(709, 61);
            this.labelHitnoNazoviteCodeIt.Name = "labelHitnoNazoviteCodeIt";
            this.labelHitnoNazoviteCodeIt.Size = new System.Drawing.Size(209, 23);
            this.labelHitnoNazoviteCodeIt.TabIndex = 16;
            this.labelHitnoNazoviteCodeIt.Text = "HITNO nazovite Code-IT";
            this.labelHitnoNazoviteCodeIt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPreostaloVrijeme
            // 
            this.labelPreostaloVrijeme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreostaloVrijeme.BackColor = System.Drawing.Color.Transparent;
            this.labelPreostaloVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreostaloVrijeme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPreostaloVrijeme.Location = new System.Drawing.Point(567, 35);
            this.labelPreostaloVrijeme.Name = "labelPreostaloVrijeme";
            this.labelPreostaloVrijeme.Size = new System.Drawing.Size(360, 23);
            this.labelPreostaloVrijeme.TabIndex = 17;
            this.labelPreostaloVrijeme.Text = "Preostalo vrijeme: 48.43h";
            this.labelPreostaloVrijeme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer3
            // 
            this.timer3.Interval = 10000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick_1);
            // 
            // timer4
            // 
            this.timer4.Interval = 30000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // frmScren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1129, 592);
            this.ControlBox = false;
            this.Controls.Add(this.labelHitnoNazoviteCodeIt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelPreostaloVrijeme);
            this.Controls.Add(this.labelPostojeNeuspjeleFiskalizacije);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnPodrska);
            this.Controls.Add(this.btnOdjava);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSat);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScren";
            this.Text = "frmScren";
            this.Load += new System.EventHandler(this.frmScren_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblSat;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBrojDana;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbMaloprodaja;
        private System.Windows.Forms.CheckBox chbFakture;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTrenutnaGodina;
        private System.Windows.Forms.Button btnPromjenaG;
        private System.Windows.Forms.Timer timerUpozoranaNaKrivuGodinu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPodrska;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFakture;
        private System.Windows.Forms.Button kalkulacijeButton;
        private System.Windows.Forms.Label labelPostojeNeuspjeleFiskalizacije;
        private System.Windows.Forms.Label labelHitnoNazoviteCodeIt;
        private System.Windows.Forms.Label labelPreostaloVrijeme;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        public System.Windows.Forms.Button btnOdjava;
        public System.Windows.Forms.Button btnMaloprodaja;
    }
}