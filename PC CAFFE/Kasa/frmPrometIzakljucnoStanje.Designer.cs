namespace PCPOS.Kasa
{
    partial class frmPrometIzakljucnoStanje
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
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.chbDucan = new System.Windows.Forms.CheckBox();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.BackColor = System.Drawing.Color.Transparent;
            this.chbSkladiste.Location = new System.Drawing.Point(269, 202);
            this.chbSkladiste.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 64;
            this.chbSkladiste.UseVisualStyleBackColor = false;
            // 
            // chbDucan
            // 
            this.chbDucan.AutoSize = true;
            this.chbDucan.BackColor = System.Drawing.Color.Transparent;
            this.chbDucan.Location = new System.Drawing.Point(269, 156);
            this.chbDucan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbDucan.Name = "chbDucan";
            this.chbDucan.Size = new System.Drawing.Size(15, 14);
            this.chbDucan.TabIndex = 63;
            this.chbDucan.UseVisualStyleBackColor = false;
            // 
            // lblDO
            // 
            this.lblDO.AutoSize = true;
            this.lblDO.BackColor = System.Drawing.Color.Transparent;
            this.lblDO.Location = new System.Drawing.Point(46, 82);
            this.lblDO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(74, 13);
            this.lblDO.TabIndex = 71;
            this.lblDO.Text = "Završni datum";
            // 
            // dtpDO
            // 
            this.dtpDO.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(51, 101);
            this.dtpDO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(218, 20);
            this.dtpDO.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Početni datum";
            // 
            // dtpOD
            // 
            this.dtpOD.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(49, 52);
            this.dtpOD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(218, 20);
            this.dtpOD.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(44, 179);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Skladište";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.BackColor = System.Drawing.Color.White;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(48, 197);
            this.cbSkladiste.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(219, 24);
            this.cbSkladiste.TabIndex = 61;
            // 
            // cbDucan
            // 
            this.cbDucan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDucan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDucan.BackColor = System.Drawing.Color.White;
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(48, 151);
            this.cbDucan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(219, 24);
            this.cbDucan.TabIndex = 60;
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.BackColor = System.Drawing.Color.White;
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.btnIspis.Location = new System.Drawing.Point(47, 241);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(220, 40);
            this.btnIspis.TabIndex = 66;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(44, 133);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(39, 13);
            this.label28.TabIndex = 67;
            this.label28.Text = "Dućan";
            // 
            // frmPrometIzakljucnoStanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(331, 310);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.chbSkladiste);
            this.Controls.Add(this.chbDucan);
            this.Controls.Add(this.lblDO);
            this.Controls.Add(this.dtpDO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpOD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.cbDucan);
            this.Name = "frmPrometIzakljucnoStanje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promet i zaključno stanje";
            this.Load += new System.EventHandler(this.frmPrometIzakljucnoStanje_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.CheckBox chbDucan;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Label label28;
    }
}