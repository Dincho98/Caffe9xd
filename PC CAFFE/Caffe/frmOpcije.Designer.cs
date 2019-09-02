namespace PCPOS.Caffe
{
    partial class frmOpcije
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpcije));
            this.nuVisina = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnButtonColor = new System.Windows.Forms.Button();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.chkUnderline = new System.Windows.Forms.CheckBox();
            this.chkItalic = new System.Windows.Forms.CheckBox();
            this.chkBold = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nuSirina = new System.Windows.Forms.NumericUpDown();
            this.btnStornoZadnjegR = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chbRadSaNarudzbama = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chbRadSSiframa = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnNaknadniPartnerNaRacun = new System.Windows.Forms.Button();
            this.btnIspisOtpremnice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nuVisina)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuSirina)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nuVisina
            // 
            this.nuVisina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nuVisina.Location = new System.Drawing.Point(58, 26);
            this.nuVisina.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nuVisina.Name = "nuVisina";
            this.nuVisina.Size = new System.Drawing.Size(161, 26);
            this.nuVisina.TabIndex = 0;
            this.nuVisina.ValueChanged += new System.EventHandler(this.nuVisina_ValueChanged);
            this.nuVisina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnButtonColor);
            this.groupBox1.Controls.Add(this.btnFontColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbSize);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nuSirina);
            this.groupBox1.Controls.Add(this.nuVisina);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(15, 210);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opcije ikona";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(233, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Button color";
            // 
            // btnButtonColor
            // 
            this.btnButtonColor.Location = new System.Drawing.Point(323, 56);
            this.btnButtonColor.Name = "btnButtonColor";
            this.btnButtonColor.Size = new System.Drawing.Size(75, 26);
            this.btnButtonColor.TabIndex = 9;
            this.btnButtonColor.UseVisualStyleBackColor = true;
            this.btnButtonColor.Click += new System.EventHandler(this.btnButtonColor_Click);
            // 
            // btnFontColor
            // 
            this.btnFontColor.Location = new System.Drawing.Point(323, 83);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(75, 26);
            this.btnFontColor.TabIndex = 8;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(246, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Font color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(282, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size";
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(323, 24);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(127, 28);
            this.cmbSize.TabIndex = 5;
            this.cmbSize.SelectionChangeCommitted += new System.EventHandler(this.cmbSize_SelectionChangeCommitted);
            // 
            // chkUnderline
            // 
            this.chkUnderline.AutoSize = true;
            this.chkUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chkUnderline.ForeColor = System.Drawing.Color.Black;
            this.chkUnderline.Location = new System.Drawing.Point(131, 88);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new System.Drawing.Size(88, 21);
            this.chkUnderline.TabIndex = 4;
            this.chkUnderline.Text = "Underline";
            this.chkUnderline.UseVisualStyleBackColor = true;
            this.chkUnderline.CheckedChanged += new System.EventHandler(this.chkUnderline_CheckedChanged);
            // 
            // chkItalic
            // 
            this.chkItalic.AutoSize = true;
            this.chkItalic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chkItalic.ForeColor = System.Drawing.Color.Black;
            this.chkItalic.Location = new System.Drawing.Point(70, 88);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new System.Drawing.Size(55, 21);
            this.chkItalic.TabIndex = 3;
            this.chkItalic.Text = "Italic";
            this.chkItalic.UseVisualStyleBackColor = true;
            this.chkItalic.CheckedChanged += new System.EventHandler(this.chkItalic_CheckedChanged);
            // 
            // chkBold
            // 
            this.chkBold.AutoSize = true;
            this.chkBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chkBold.ForeColor = System.Drawing.Color.Black;
            this.chkBold.Location = new System.Drawing.Point(9, 88);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new System.Drawing.Size(55, 21);
            this.chkBold.TabIndex = 2;
            this.chkBold.Text = "Bold";
            this.chkBold.UseVisualStyleBackColor = true;
            this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Širina";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Visina";
            // 
            // nuSirina
            // 
            this.nuSirina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nuSirina.Location = new System.Drawing.Point(58, 57);
            this.nuSirina.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nuSirina.Name = "nuSirina";
            this.nuSirina.Size = new System.Drawing.Size(161, 26);
            this.nuSirina.TabIndex = 0;
            this.nuSirina.ValueChanged += new System.EventHandler(this.nuSirina_ValueChanged);
            this.nuSirina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // btnStornoZadnjegR
            // 
            this.btnStornoZadnjegR.BackColor = System.Drawing.Color.White;
            this.btnStornoZadnjegR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStornoZadnjegR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStornoZadnjegR.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnStornoZadnjegR.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnStornoZadnjegR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnStornoZadnjegR.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnStornoZadnjegR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStornoZadnjegR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStornoZadnjegR.Location = new System.Drawing.Point(338, 12);
            this.btnStornoZadnjegR.Name = "btnStornoZadnjegR";
            this.btnStornoZadnjegR.Size = new System.Drawing.Size(144, 89);
            this.btnStornoZadnjegR.TabIndex = 6;
            this.btnStornoZadnjegR.Text = "Storno zadnjeg računa";
            this.btnStornoZadnjegR.UseVisualStyleBackColor = false;
            this.btnStornoZadnjegR.Click += new System.EventHandler(this.btnStornoZadnjegR_Click);
            this.btnStornoZadnjegR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
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
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(501, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 89);
            this.button3.TabIndex = 7;
            this.button3.Text = "Storno određenog računa";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
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
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(175, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 89);
            this.button2.TabIndex = 8;
            this.button2.Text = "Ispis određenog računa";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 89);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ispis zadnjeg\r\nračuna";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // chbRadSaNarudzbama
            // 
            this.chbRadSaNarudzbama.AutoSize = true;
            this.chbRadSaNarudzbama.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chbRadSaNarudzbama.ForeColor = System.Drawing.Color.Black;
            this.chbRadSaNarudzbama.Location = new System.Drawing.Point(18, 90);
            this.chbRadSaNarudzbama.Name = "chbRadSaNarudzbama";
            this.chbRadSaNarudzbama.Size = new System.Drawing.Size(97, 24);
            this.chbRadSaNarudzbama.TabIndex = 9;
            this.chbRadSaNarudzbama.Text = "Aktivirano";
            this.chbRadSaNarudzbama.UseVisualStyleBackColor = true;
            this.chbRadSaNarudzbama.CheckedChanged += new System.EventHandler(this.chbRadSaNarudzbama_CheckedChanged);
            this.chbRadSaNarudzbama.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chbRadSaNarudzbama);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(503, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 123);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rad sa narudžbama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(16, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(275, 52);
            this.label3.TabIndex = 10;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // chbRadSSiframa
            // 
            this.chbRadSSiframa.AutoSize = true;
            this.chbRadSSiframa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chbRadSSiframa.ForeColor = System.Drawing.Color.Black;
            this.chbRadSSiframa.Location = new System.Drawing.Point(15, 342);
            this.chbRadSSiframa.Name = "chbRadSSiframa";
            this.chbRadSSiframa.Size = new System.Drawing.Size(195, 24);
            this.chbRadSSiframa.TabIndex = 9;
            this.chbRadSSiframa.Text = "Preferiraj rad sa šiframa";
            this.chbRadSSiframa.UseVisualStyleBackColor = true;
            this.chbRadSSiframa.CheckedChanged += new System.EventHandler(this.chbRadSSiframa_CheckedChanged);
            this.chbRadSSiframa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
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
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button4.Location = new System.Drawing.Point(664, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 89);
            this.button4.TabIndex = 7;
            this.button4.Text = "Otvori ladicu";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            // 
            // btnNaknadniPartnerNaRacun
            // 
            this.btnNaknadniPartnerNaRacun.BackColor = System.Drawing.Color.White;
            this.btnNaknadniPartnerNaRacun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNaknadniPartnerNaRacun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNaknadniPartnerNaRacun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNaknadniPartnerNaRacun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNaknadniPartnerNaRacun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNaknadniPartnerNaRacun.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNaknadniPartnerNaRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaknadniPartnerNaRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNaknadniPartnerNaRacun.Location = new System.Drawing.Point(12, 107);
            this.btnNaknadniPartnerNaRacun.Name = "btnNaknadniPartnerNaRacun";
            this.btnNaknadniPartnerNaRacun.Size = new System.Drawing.Size(144, 89);
            this.btnNaknadniPartnerNaRacun.TabIndex = 10;
            this.btnNaknadniPartnerNaRacun.Text = "Naknadno dodaj partnera na račun";
            this.btnNaknadniPartnerNaRacun.UseVisualStyleBackColor = false;
            this.btnNaknadniPartnerNaRacun.Click += new System.EventHandler(this.btnNaknadniPartnerNaRacun_Click);
            // 
            // btnIspisOtpremnice
            // 
            this.btnIspisOtpremnice.BackColor = System.Drawing.Color.White;
            this.btnIspisOtpremnice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisOtpremnice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisOtpremnice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspisOtpremnice.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspisOtpremnice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisOtpremnice.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspisOtpremnice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspisOtpremnice.Location = new System.Drawing.Point(175, 107);
            this.btnIspisOtpremnice.Name = "btnIspisOtpremnice";
            this.btnIspisOtpremnice.Size = new System.Drawing.Size(144, 89);
            this.btnIspisOtpremnice.TabIndex = 11;
            this.btnIspisOtpremnice.Text = "Ispis otpremnice";
            this.btnIspisOtpremnice.UseVisualStyleBackColor = false;
            this.btnIspisOtpremnice.Click += new System.EventHandler(this.btnIspisOtpremnice_Click);
            // 
            // frmOpcije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(822, 371);
            this.Controls.Add(this.btnIspisOtpremnice);
            this.Controls.Add(this.btnNaknadniPartnerNaRacun);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chbRadSSiframa);
            this.Controls.Add(this.btnStornoZadnjegR);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(838, 410);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(838, 410);
            this.Name = "frmOpcije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opcije";
            this.Load += new System.EventHandler(this.frmOpcije_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcije_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nuVisina)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuSirina)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nuVisina;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuSirina;
        private System.Windows.Forms.Button btnStornoZadnjegR;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chbRadSaNarudzbama;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbRadSSiframa;
		private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chkItalic;
        private System.Windows.Forms.CheckBox chkBold;
        private System.Windows.Forms.CheckBox chkUnderline;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Button btnButtonColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNaknadniPartnerNaRacun;
        private System.Windows.Forms.Button btnIspisOtpremnice;
    }
}