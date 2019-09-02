namespace RESORT.Forme
{
    partial class frmSveOtpremnice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOtpremnice = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaposlenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dodaj = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlSobe = new System.Windows.Forms.Panel();
            this.dgvSobe = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tip_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_lezaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_nocenja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSobeBottom = new System.Windows.Forms.Panel();
            this.btnDodajSobu = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtNazivSobe = new System.Windows.Forms.TextBox();
            this.btnSrchSoba = new System.Windows.Forms.Button();
            this.txtBrojSobe = new System.Windows.Forms.TextBox();
            this.lblNaSobu = new System.Windows.Forms.Label();
            this.btnDodajOdabrano = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtpremnice)).BeginInit();
            this.pnlSobe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSobe)).BeginInit();
            this.pnlSobeBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOtpremnice
            // 
            this.dgvOtpremnice.AllowUserToAddRows = false;
            this.dgvOtpremnice.AllowUserToDeleteRows = false;
            this.dgvOtpremnice.AllowUserToResizeRows = false;
            this.dgvOtpremnice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOtpremnice.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOtpremnice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOtpremnice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOtpremnice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.datum,
            this.zaposlenik,
            this.soba,
            this.ukupno,
            this.napomena,
            this.dodaj});
            this.dgvOtpremnice.Location = new System.Drawing.Point(12, 84);
            this.dgvOtpremnice.Name = "dgvOtpremnice";
            this.dgvOtpremnice.RowHeadersWidth = 20;
            this.dgvOtpremnice.Size = new System.Drawing.Size(980, 485);
            this.dgvOtpremnice.TabIndex = 1;
            // 
            // broj
            // 
            this.broj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.Width = 80;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.Width = 120;
            // 
            // zaposlenik
            // 
            this.zaposlenik.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.zaposlenik.HeaderText = "Zaposlenik";
            this.zaposlenik.Name = "zaposlenik";
            this.zaposlenik.Width = 150;
            // 
            // soba
            // 
            this.soba.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.soba.HeaderText = "Soba";
            this.soba.Name = "soba";
            this.soba.Width = 150;
            // 
            // ukupno
            // 
            this.ukupno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            // 
            // napomena
            // 
            this.napomena.HeaderText = "Napomena";
            this.napomena.Name = "napomena";
            // 
            // dodaj
            // 
            this.dodaj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dodaj.HeaderText = "Dodaj";
            this.dodaj.Name = "dodaj";
            this.dodaj.Width = 60;
            // 
            // pnlSobe
            // 
            this.pnlSobe.Controls.Add(this.dgvSobe);
            this.pnlSobe.Controls.Add(this.pnlSobeBottom);
            this.pnlSobe.Location = new System.Drawing.Point(12, 61);
            this.pnlSobe.Name = "pnlSobe";
            this.pnlSobe.Size = new System.Drawing.Size(980, 508);
            this.pnlSobe.TabIndex = 4;
            // 
            // dgvSobe
            // 
            this.dgvSobe.AllowUserToAddRows = false;
            this.dgvSobe.AllowUserToDeleteRows = false;
            this.dgvSobe.AllowUserToResizeRows = false;
            this.dgvSobe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSobe.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSobe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSobe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.broj_sobe,
            this.tip_sobe,
            this.naziv_sobe,
            this.broj_lezaja,
            this.cijena_nocenja});
            this.dgvSobe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSobe.Location = new System.Drawing.Point(0, 0);
            this.dgvSobe.MultiSelect = false;
            this.dgvSobe.Name = "dgvSobe";
            this.dgvSobe.ReadOnly = true;
            this.dgvSobe.RowHeadersWidth = 20;
            this.dgvSobe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSobe.Size = new System.Drawing.Size(980, 439);
            this.dgvSobe.TabIndex = 1;
            this.dgvSobe.Tag = "8";
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // broj_sobe
            // 
            this.broj_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj_sobe.HeaderText = "Broj sobe";
            this.broj_sobe.Name = "broj_sobe";
            this.broj_sobe.ReadOnly = true;
            // 
            // tip_sobe
            // 
            this.tip_sobe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tip_sobe.HeaderText = "Tip sobe";
            this.tip_sobe.Name = "tip_sobe";
            this.tip_sobe.ReadOnly = true;
            this.tip_sobe.Width = 200;
            // 
            // naziv_sobe
            // 
            this.naziv_sobe.HeaderText = "Naziv sobe";
            this.naziv_sobe.Name = "naziv_sobe";
            this.naziv_sobe.ReadOnly = true;
            // 
            // broj_lezaja
            // 
            this.broj_lezaja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj_lezaja.HeaderText = "Broj ležaja";
            this.broj_lezaja.Name = "broj_lezaja";
            this.broj_lezaja.ReadOnly = true;
            // 
            // cijena_nocenja
            // 
            this.cijena_nocenja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.cijena_nocenja.DefaultCellStyle = dataGridViewCellStyle6;
            this.cijena_nocenja.HeaderText = "Cijena nočenja";
            this.cijena_nocenja.Name = "cijena_nocenja";
            this.cijena_nocenja.ReadOnly = true;
            // 
            // pnlSobeBottom
            // 
            this.pnlSobeBottom.Controls.Add(this.btnDodajSobu);
            this.pnlSobeBottom.Controls.Add(this.btnBack);
            this.pnlSobeBottom.Controls.Add(this.btnDown);
            this.pnlSobeBottom.Controls.Add(this.btnUp);
            this.pnlSobeBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSobeBottom.Location = new System.Drawing.Point(0, 439);
            this.pnlSobeBottom.Name = "pnlSobeBottom";
            this.pnlSobeBottom.Size = new System.Drawing.Size(980, 69);
            this.pnlSobeBottom.TabIndex = 0;
            // 
            // btnDodajSobu
            // 
            this.btnDodajSobu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDodajSobu.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.btnDodajSobu.Location = new System.Drawing.Point(877, 8);
            this.btnDodajSobu.Name = "btnDodajSobu";
            this.btnDodajSobu.Size = new System.Drawing.Size(100, 52);
            this.btnDodajSobu.TabIndex = 6;
            this.btnDodajSobu.Tag = "dgvSobe";
            this.btnDodajSobu.Text = "►";
            this.btnDodajSobu.UseVisualStyleBackColor = true;
            this.btnDodajSobu.Click += new System.EventHandler(this.btnDodajSobu_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.btnBack.Location = new System.Drawing.Point(3, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 52);
            this.btnBack.TabIndex = 5;
            this.btnBack.Tag = "dgvSobe";
            this.btnBack.Text = "◄";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnSrchSoba_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnDown.Location = new System.Drawing.Point(493, 8);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(150, 52);
            this.btnDown.TabIndex = 4;
            this.btnDown.Tag = "dgvSobe";
            this.btnDown.Text = "▼";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnUp.Location = new System.Drawing.Point(337, 8);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(150, 52);
            this.btnUp.TabIndex = 3;
            this.btnUp.Tag = "dgvSobe";
            this.btnUp.Text = "▲";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // txtNazivSobe
            // 
            this.txtNazivSobe.Enabled = false;
            this.txtNazivSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNazivSobe.Location = new System.Drawing.Point(218, 29);
            this.txtNazivSobe.Name = "txtNazivSobe";
            this.txtNazivSobe.Size = new System.Drawing.Size(279, 26);
            this.txtNazivSobe.TabIndex = 8;
            // 
            // btnSrchSoba
            // 
            this.btnSrchSoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchSoba.Location = new System.Drawing.Point(179, 31);
            this.btnSrchSoba.Name = "btnSrchSoba";
            this.btnSrchSoba.Size = new System.Drawing.Size(33, 23);
            this.btnSrchSoba.TabIndex = 7;
            this.btnSrchSoba.Text = "...";
            this.btnSrchSoba.UseVisualStyleBackColor = true;
            this.btnSrchSoba.Click += new System.EventHandler(this.btnSrchSoba_Click);
            // 
            // txtBrojSobe
            // 
            this.txtBrojSobe.Enabled = false;
            this.txtBrojSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtBrojSobe.Location = new System.Drawing.Point(85, 29);
            this.txtBrojSobe.Name = "txtBrojSobe";
            this.txtBrojSobe.Size = new System.Drawing.Size(87, 26);
            this.txtBrojSobe.TabIndex = 6;
            // 
            // lblNaSobu
            // 
            this.lblNaSobu.AutoSize = true;
            this.lblNaSobu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNaSobu.Location = new System.Drawing.Point(13, 34);
            this.lblNaSobu.Name = "lblNaSobu";
            this.lblNaSobu.Size = new System.Drawing.Size(65, 17);
            this.lblNaSobu.TabIndex = 5;
            this.lblNaSobu.Text = "Na sobu:";
            // 
            // btnDodajOdabrano
            // 
            this.btnDodajOdabrano.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajOdabrano.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDodajOdabrano.Location = new System.Drawing.Point(662, 12);
            this.btnDodajOdabrano.Name = "btnDodajOdabrano";
            this.btnDodajOdabrano.Size = new System.Drawing.Size(142, 39);
            this.btnDodajOdabrano.TabIndex = 9;
            this.btnDodajOdabrano.Text = "Dodaj odabrano";
            this.btnDodajOdabrano.UseVisualStyleBackColor = true;
            this.btnDodajOdabrano.Click += new System.EventHandler(this.btnDodajOdabrano_Click);
            // 
            // frmSveOtpremnice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 581);
            this.Controls.Add(this.btnDodajOdabrano);
            this.Controls.Add(this.txtNazivSobe);
            this.Controls.Add(this.btnSrchSoba);
            this.Controls.Add(this.txtBrojSobe);
            this.Controls.Add(this.lblNaSobu);
            this.Controls.Add(this.dgvOtpremnice);
            this.Controls.Add(this.pnlSobe);
            this.MaximumSize = new System.Drawing.Size(1020, 619);
            this.MinimumSize = new System.Drawing.Size(1020, 619);
            this.Name = "frmSveOtpremnice";
            this.Text = "frmSveOtpremnice";
            this.Load += new System.EventHandler(this.frmSveOtpremnice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtpremnice)).EndInit();
            this.pnlSobe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSobe)).EndInit();
            this.pnlSobeBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlSobe;
        private System.Windows.Forms.TextBox txtNazivSobe;
        private System.Windows.Forms.Button btnSrchSoba;
        private System.Windows.Forms.Label lblNaSobu;
        private System.Windows.Forms.Panel pnlSobeBottom;
        private System.Windows.Forms.DataGridView dgvSobe;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn tip_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_lezaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_nocenja;
        private System.Windows.Forms.Button btnDodajSobu;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn zaposlenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dodaj;
        private System.Windows.Forms.Button btnDodajOdabrano;
        public System.Windows.Forms.DataGridView dgvOtpremnice;
        public System.Windows.Forms.TextBox txtBrojSobe;
    }
}