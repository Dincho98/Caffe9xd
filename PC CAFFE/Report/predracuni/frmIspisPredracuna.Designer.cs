namespace PCPOS.Report.predracuni
{
    partial class frmIspisPredracuna
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
            this.btnIspis = new System.Windows.Forms.Button();
            this.chbDucan = new System.Windows.Forms.CheckBox();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label28 = new System.Windows.Forms.Label();
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.btnPosIspis = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chbOdabirStola = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOdabirStola = new System.Windows.Forms.ComboBox();
            this.dgvD = new System.Windows.Forms.DataGridView();
            this.djelatnik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaci = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnIspisZbirno = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnIspis.Location = new System.Drawing.Point(14, 367);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(282, 40);
            this.btnIspis.TabIndex = 84;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // chbDucan
            // 
            this.chbDucan.AutoSize = true;
            this.chbDucan.BackColor = System.Drawing.Color.Transparent;
            this.chbDucan.Location = new System.Drawing.Point(298, 131);
            this.chbDucan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbDucan.Name = "chbDucan";
            this.chbDucan.Size = new System.Drawing.Size(15, 14);
            this.chbDucan.TabIndex = 81;
            this.chbDucan.UseVisualStyleBackColor = false;
            // 
            // lblDO
            // 
            this.lblDO.AutoSize = true;
            this.lblDO.BackColor = System.Drawing.Color.Transparent;
            this.lblDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDO.Location = new System.Drawing.Point(10, 66);
            this.lblDO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(92, 16);
            this.lblDO.TabIndex = 90;
            this.lblDO.Text = "Završni datum";
            // 
            // dtpDO
            // 
            this.dtpDO.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(15, 85);
            this.dtpDO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(278, 22);
            this.dtpDO.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(9, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 89;
            this.label3.Text = "Početni datum";
            // 
            // dtpOD
            // 
            this.dtpOD.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(13, 36);
            this.dtpOD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(278, 22);
            this.dtpOD.TabIndex = 74;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label28.Location = new System.Drawing.Point(8, 110);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 16);
            this.label28.TabIndex = 86;
            this.label28.Text = "Dućan";
            // 
            // cbDucan
            // 
            this.cbDucan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDucan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDucan.BackColor = System.Drawing.Color.White;
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(12, 126);
            this.cbDucan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(279, 24);
            this.cbDucan.TabIndex = 76;
            // 
            // btnPosIspis
            // 
            this.btnPosIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPosIspis.BackColor = System.Drawing.Color.White;
            this.btnPosIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPosIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPosIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPosIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPosIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPosIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPosIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPosIspis.Location = new System.Drawing.Point(14, 466);
            this.btnPosIspis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPosIspis.Name = "btnPosIspis";
            this.btnPosIspis.Size = new System.Drawing.Size(280, 40);
            this.btnPosIspis.TabIndex = 84;
            this.btnPosIspis.Text = "POS ispis";
            this.btnPosIspis.UseVisualStyleBackColor = false;
            this.btnPosIspis.Click += new System.EventHandler(this.btnPosIspis_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button1.Location = new System.Drawing.Point(14, 516);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 40);
            this.button1.TabIndex = 84;
            this.button1.Text = "POS ispis zbirno";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbOdabirStola
            // 
            this.chbOdabirStola.AutoSize = true;
            this.chbOdabirStola.BackColor = System.Drawing.Color.Transparent;
            this.chbOdabirStola.Location = new System.Drawing.Point(300, 336);
            this.chbOdabirStola.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbOdabirStola.Name = "chbOdabirStola";
            this.chbOdabirStola.Size = new System.Drawing.Size(15, 14);
            this.chbOdabirStola.TabIndex = 92;
            this.chbOdabirStola.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 315);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 93;
            this.label1.Text = "Odabir stola";
            // 
            // cbOdabirStola
            // 
            this.cbOdabirStola.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbOdabirStola.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbOdabirStola.BackColor = System.Drawing.Color.White;
            this.cbOdabirStola.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOdabirStola.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbOdabirStola.FormattingEnabled = true;
            this.cbOdabirStola.Location = new System.Drawing.Point(11, 331);
            this.cbOdabirStola.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbOdabirStola.Name = "cbOdabirStola";
            this.cbOdabirStola.Size = new System.Drawing.Size(279, 24);
            this.cbOdabirStola.TabIndex = 91;
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
            this.dgvD.Location = new System.Drawing.Point(12, 158);
            this.dgvD.Name = "dgvD";
            this.dgvD.ReadOnly = true;
            this.dgvD.RowHeadersVisible = false;
            this.dgvD.RowTemplate.Height = 18;
            this.dgvD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvD.Size = new System.Drawing.Size(281, 154);
            this.dgvD.TabIndex = 94;
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
            // btnIspisZbirno
            // 
            this.btnIspisZbirno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspisZbirno.BackColor = System.Drawing.Color.White;
            this.btnIspisZbirno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisZbirno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisZbirno.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspisZbirno.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspisZbirno.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspisZbirno.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspisZbirno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisZbirno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnIspisZbirno.Location = new System.Drawing.Point(14, 417);
            this.btnIspisZbirno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIspisZbirno.Name = "btnIspisZbirno";
            this.btnIspisZbirno.Size = new System.Drawing.Size(282, 40);
            this.btnIspisZbirno.TabIndex = 95;
            this.btnIspisZbirno.Text = "Ispis (Zbirno)";
            this.btnIspisZbirno.UseVisualStyleBackColor = false;
            this.btnIspisZbirno.Click += new System.EventHandler(this.btnIspisZbirno_Click);
            // 
            // frmIspisPredracuna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(317, 567);
            this.Controls.Add(this.btnIspisZbirno);
            this.Controls.Add(this.dgvD);
            this.Controls.Add(this.chbOdabirStola);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOdabirStola);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPosIspis);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.chbDucan);
            this.Controls.Add(this.lblDO);
            this.Controls.Add(this.dtpDO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpOD);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbDucan);
            this.Name = "frmIspisPredracuna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ispis predračuna";
            this.Load += new System.EventHandler(this.frmIspisProdajnihArtiklaNaMaliPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.CheckBox chbDucan;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Button btnPosIspis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chbOdabirStola;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOdabirStola;
        private System.Windows.Forms.DataGridView dgvD;
        private System.Windows.Forms.DataGridViewTextBoxColumn djelatnik;
        private System.Windows.Forms.DataGridViewCheckBoxColumn oznaci;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Button btnIspisZbirno;
    }
}