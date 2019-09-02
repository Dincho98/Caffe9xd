namespace RESORT.Forme.sifarnici
{
    partial class frmPartnerTrazi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartnerTrazi));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnKolicina = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPartnerPremaSifri = new System.Windows.Forms.TextBox();
            this.txtIme1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.CHoib = new System.Windows.Forms.RadioButton();
            this.CHime = new System.Windows.Forms.RadioButton();
            this.CHsifra = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chDrzava = new System.Windows.Forms.CheckBox();
            this.chGrad = new System.Windows.Forms.CheckBox();
            this.chDjelatnost = new System.Windows.Forms.CheckBox();
            this.cbDjelatnost = new System.Windows.Forms.ComboBox();
            this.cbDrzava = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.lm = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTekst = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 18);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(972, 643);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SlateGray;
            this.tabPage1.Controls.Add(this.btnKolicina);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txtPartnerPremaSifri);
            this.tabPage1.Controls.Add(this.txtIme1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(964, 605);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "          Brzo pretraživanje          ";
            // 
            // btnKolicina
            // 
            this.btnKolicina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKolicina.BackColor = System.Drawing.Color.Transparent;
            this.btnKolicina.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKolicina.BackgroundImage")));
            this.btnKolicina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKolicina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKolicina.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.BorderSize = 0;
            this.btnKolicina.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKolicina.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.btnKolicina.Location = new System.Drawing.Point(792, 20);
            this.btnKolicina.Name = "btnKolicina";
            this.btnKolicina.Size = new System.Drawing.Size(148, 52);
            this.btnKolicina.TabIndex = 33;
            this.btnKolicina.Text = "Dodaj novog partnera";
            this.btnKolicina.UseVisualStyleBackColor = false;
            this.btnKolicina.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(20, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "Partneri";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView2.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView2.Location = new System.Drawing.Point(23, 92);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(917, 492);
            this.dataGridView2.TabIndex = 30;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(350, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "Traži prema šifra";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(184, 17);
            this.label10.TabIndex = 29;
            this.label10.Text = "Traži prema imenu partnera";
            // 
            // txtPartnerPremaSifri
            // 
            this.txtPartnerPremaSifri.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartnerPremaSifri.Location = new System.Drawing.Point(353, 35);
            this.txtPartnerPremaSifri.Name = "txtPartnerPremaSifri";
            this.txtPartnerPremaSifri.Size = new System.Drawing.Size(304, 26);
            this.txtPartnerPremaSifri.TabIndex = 0;
            this.txtPartnerPremaSifri.TextChanged += new System.EventHandler(this.txtPartnerPremaSifri_TextChanged);
            this.txtPartnerPremaSifri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoreDole_KeyDown);
            // 
            // txtIme1
            // 
            this.txtIme1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIme1.Location = new System.Drawing.Point(23, 36);
            this.txtIme1.Name = "txtIme1";
            this.txtIme1.Size = new System.Drawing.Size(304, 26);
            this.txtIme1.TabIndex = 0;
            this.txtIme1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtIme1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoreDole_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SlateGray;
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.CHoib);
            this.tabPage2.Controls.Add(this.CHime);
            this.tabPage2.Controls.Add(this.CHsifra);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.txtTekst);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnTrazi);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(964, 605);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "          Napredno pretraživanje          ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.Location = new System.Drawing.Point(29, 232);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(906, 350);
            this.dataGridView1.TabIndex = 33;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(26, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 32;
            this.label3.Text = "Partneri";
            // 
            // CHoib
            // 
            this.CHoib.AutoSize = true;
            this.CHoib.Location = new System.Drawing.Point(416, 65);
            this.CHoib.Name = "CHoib";
            this.CHoib.Size = new System.Drawing.Size(159, 21);
            this.CHoib.TabIndex = 28;
            this.CHoib.Text = "Pretraži prema OIB-u";
            this.CHoib.UseVisualStyleBackColor = true;
            // 
            // CHime
            // 
            this.CHime.AutoSize = true;
            this.CHime.Location = new System.Drawing.Point(187, 65);
            this.CHime.Name = "CHime";
            this.CHime.Size = new System.Drawing.Size(219, 21);
            this.CHime.TabIndex = 28;
            this.CHime.Text = "Pretraži prema imenu partnera";
            this.CHime.UseVisualStyleBackColor = true;
            // 
            // CHsifra
            // 
            this.CHsifra.AutoSize = true;
            this.CHsifra.Checked = true;
            this.CHsifra.Location = new System.Drawing.Point(29, 65);
            this.CHsifra.Name = "CHsifra";
            this.CHsifra.Size = new System.Drawing.Size(145, 21);
            this.CHsifra.TabIndex = 29;
            this.CHsifra.TabStop = true;
            this.CHsifra.Text = "Pretraži prema šifri";
            this.CHsifra.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chDrzava);
            this.groupBox1.Controls.Add(this.chGrad);
            this.groupBox1.Controls.Add(this.chDjelatnost);
            this.groupBox1.Controls.Add(this.cbDjelatnost);
            this.groupBox1.Controls.Add(this.cbDrzava);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbGrad);
            this.groupBox1.Controls.Add(this.lm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(909, 97);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodatne opcije";
            // 
            // chDrzava
            // 
            this.chDrzava.AutoSize = true;
            this.chDrzava.Location = new System.Drawing.Point(563, 62);
            this.chDrzava.Name = "chDrzava";
            this.chDrzava.Size = new System.Drawing.Size(15, 14);
            this.chDrzava.TabIndex = 13;
            this.chDrzava.UseVisualStyleBackColor = true;
            // 
            // chGrad
            // 
            this.chGrad.AutoSize = true;
            this.chGrad.Location = new System.Drawing.Point(372, 62);
            this.chGrad.Name = "chGrad";
            this.chGrad.Size = new System.Drawing.Size(15, 14);
            this.chGrad.TabIndex = 13;
            this.chGrad.UseVisualStyleBackColor = true;
            // 
            // chDjelatnost
            // 
            this.chDjelatnost.AutoSize = true;
            this.chDjelatnost.Location = new System.Drawing.Point(183, 62);
            this.chDjelatnost.Name = "chDjelatnost";
            this.chDjelatnost.Size = new System.Drawing.Size(15, 14);
            this.chDjelatnost.TabIndex = 13;
            this.chDjelatnost.UseVisualStyleBackColor = true;
            // 
            // cbDjelatnost
            // 
            this.cbDjelatnost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDjelatnost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDjelatnost.FormattingEnabled = true;
            this.cbDjelatnost.Items.AddRange(new object[] {
            "     "});
            this.cbDjelatnost.Location = new System.Drawing.Point(27, 59);
            this.cbDjelatnost.Name = "cbDjelatnost";
            this.cbDjelatnost.Size = new System.Drawing.Size(154, 24);
            this.cbDjelatnost.TabIndex = 12;
            // 
            // cbDrzava
            // 
            this.cbDrzava.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDrzava.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDrzava.FormattingEnabled = true;
            this.cbDrzava.Location = new System.Drawing.Point(407, 59);
            this.cbDrzava.Name = "cbDrzava";
            this.cbDrzava.Size = new System.Drawing.Size(154, 24);
            this.cbDrzava.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Traži prema Dražavi";
            // 
            // cbGrad
            // 
            this.cbGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(216, 59);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(154, 24);
            this.cbGrad.TabIndex = 12;
            // 
            // lm
            // 
            this.lm.AutoSize = true;
            this.lm.Location = new System.Drawing.Point(213, 41);
            this.lm.Name = "lm";
            this.lm.Size = new System.Drawing.Size(128, 17);
            this.lm.TabIndex = 6;
            this.lm.Text = "Traži prema Gradu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Traži po djelatnosti";
            // 
            // txtTekst
            // 
            this.txtTekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTekst.Location = new System.Drawing.Point(26, 32);
            this.txtTekst.Name = "txtTekst";
            this.txtTekst.Size = new System.Drawing.Size(473, 26);
            this.txtTekst.TabIndex = 26;
            this.txtTekst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTekst_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Traži";
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(528, 32);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(114, 26);
            this.btnTrazi.TabIndex = 24;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // frmPartnerTrazi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmPartnerTrazi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPartnerTrazi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoreDole_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtIme1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton CHime;
        private System.Windows.Forms.RadioButton CHsifra;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chGrad;
        private System.Windows.Forms.CheckBox chDjelatnost;
        private System.Windows.Forms.ComboBox cbDjelatnost;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.Label lm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTekst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton CHoib;
        private System.Windows.Forms.CheckBox chDrzava;
        private System.Windows.Forms.ComboBox cbDrzava;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPartnerPremaSifri;
        private System.Windows.Forms.Button btnKolicina;

    }
}