namespace PCPOS.Kasa
{
    partial class frmPrometKase
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
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbZaposlenik = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.chbDucan = new System.Windows.Forms.CheckBox();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.chbBlagajnik = new System.Windows.Forms.CheckBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDucan
            // 
            this.cbDucan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDucan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDucan.BackColor = System.Drawing.Color.White;
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(55, 136);
            this.cbDucan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(202, 24);
            this.cbDucan.TabIndex = 3;
            this.cbDucan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(52, 118);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 16);
            this.label28.TabIndex = 32;
            this.label28.Text = "Dućan";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.BackColor = System.Drawing.Color.White;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(55, 183);
            this.cbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(202, 24);
            this.cbSkladiste.TabIndex = 4;
            this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(52, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Skladište";
            // 
            // cbZaposlenik
            // 
            this.cbZaposlenik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZaposlenik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZaposlenik.BackColor = System.Drawing.Color.White;
            this.cbZaposlenik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZaposlenik.FormattingEnabled = true;
            this.cbZaposlenik.Location = new System.Drawing.Point(55, 230);
            this.cbZaposlenik.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbZaposlenik.Name = "cbZaposlenik";
            this.cbZaposlenik.Size = new System.Drawing.Size(202, 24);
            this.cbZaposlenik.TabIndex = 5;
            this.cbZaposlenik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(53, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Blagajnik";
            // 
            // lblDO
            // 
            this.lblDO.AutoSize = true;
            this.lblDO.BackColor = System.Drawing.Color.Transparent;
            this.lblDO.Location = new System.Drawing.Point(53, 70);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(88, 16);
            this.lblDO.TabIndex = 41;
            this.lblDO.Text = "Završni datum";
            // 
            // dtpDO
            // 
            this.dtpDO.CustomFormat = "dd/MM/yyyy";
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(57, 89);
            this.dtpDO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(201, 22);
            this.dtpDO.TabIndex = 2;
            this.dtpDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(52, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 39;
            this.label3.Text = "Početni datum";
            // 
            // dtpOD
            // 
            this.dtpOD.CustomFormat = "dd/MM/yyyy";
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(55, 41);
            this.dtpOD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(201, 22);
            this.dtpOD.TabIndex = 1;
            this.dtpOD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbDucan
            // 
            this.chbDucan.AutoSize = true;
            this.chbDucan.BackColor = System.Drawing.Color.Transparent;
            this.chbDucan.Location = new System.Drawing.Point(263, 145);
            this.chbDucan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbDucan.Name = "chbDucan";
            this.chbDucan.Size = new System.Drawing.Size(15, 14);
            this.chbDucan.TabIndex = 8;
            this.chbDucan.UseVisualStyleBackColor = false;
            this.chbDucan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.BackColor = System.Drawing.Color.Transparent;
            this.chbSkladiste.Location = new System.Drawing.Point(263, 191);
            this.chbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 9;
            this.chbSkladiste.UseVisualStyleBackColor = false;
            this.chbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbBlagajnik
            // 
            this.chbBlagajnik.AutoSize = true;
            this.chbBlagajnik.BackColor = System.Drawing.Color.Transparent;
            this.chbBlagajnik.Location = new System.Drawing.Point(264, 238);
            this.chbBlagajnik.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbBlagajnik.Name = "chbBlagajnik";
            this.chbBlagajnik.Size = new System.Drawing.Size(15, 14);
            this.chbBlagajnik.TabIndex = 10;
            this.chbBlagajnik.UseVisualStyleBackColor = false;
            this.chbBlagajnik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
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
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnIspis.Location = new System.Drawing.Point(54, 282);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(224, 39);
            this.btnIspis.TabIndex = 11;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            this.btnIspis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            this.btnIspis.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnIspis.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // frmPrometKase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(327, 349);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.chbBlagajnik);
            this.Controls.Add(this.chbSkladiste);
            this.Controls.Add(this.chbDucan);
            this.Controls.Add(this.lblDO);
            this.Controls.Add(this.dtpDO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpOD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbZaposlenik);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.cbDucan);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPrometKase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promet kase";
            this.Load += new System.EventHandler(this.frmPrometKase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbZaposlenik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.CheckBox chbDucan;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.CheckBox chbBlagajnik;
        private System.Windows.Forms.Button btnIspis;
    }
}