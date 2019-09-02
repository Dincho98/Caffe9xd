namespace PCPOS.Caffe
{
    partial class frmBonUpis
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblPartner = new System.Windows.Forms.Label();
            this.lblDatum = new System.Windows.Forms.Label();
            this.lblIznos = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.cmbBon = new System.Windows.Forms.ComboBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.pnlBottom.SuspendLayout();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 159);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(376, 101);
            this.pnlBottom.TabIndex = 0;
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.txtIznos);
            this.pnlFill.Controls.Add(this.dtpDatum);
            this.pnlFill.Controls.Add(this.cmbBon);
            this.pnlFill.Controls.Add(this.lblTip);
            this.pnlFill.Controls.Add(this.lblIznos);
            this.pnlFill.Controls.Add(this.lblDatum);
            this.pnlFill.Controls.Add(this.lblPartner);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(376, 159);
            this.pnlFill.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnCancel.Location = new System.Drawing.Point(191, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 52);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Tag = "";
            this.btnCancel.Text = "Odustani";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOK.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnOK.Location = new System.Drawing.Point(61, 25);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(124, 52);
            this.btnOK.TabIndex = 2;
            this.btnOK.Tag = "";
            this.btnOK.Text = "Spremi";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPartner.Location = new System.Drawing.Point(13, 16);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(55, 17);
            this.lblPartner.TabIndex = 0;
            this.lblPartner.Text = "Partner";
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatum.Location = new System.Drawing.Point(13, 75);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(49, 17);
            this.lblDatum.TabIndex = 1;
            this.lblDatum.Text = "Datum";
            // 
            // lblIznos
            // 
            this.lblIznos.AutoSize = true;
            this.lblIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblIznos.Location = new System.Drawing.Point(13, 104);
            this.lblIznos.Name = "lblIznos";
            this.lblIznos.Size = new System.Drawing.Size(41, 17);
            this.lblIznos.TabIndex = 2;
            this.lblIznos.Text = "Iznos";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTip.Location = new System.Drawing.Point(13, 46);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(28, 17);
            this.lblTip.TabIndex = 3;
            this.lblTip.Text = "Tip";
            // 
            // cmbBon
            // 
            this.cmbBon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbBon.FormattingEnabled = true;
            this.cmbBon.Location = new System.Drawing.Point(98, 42);
            this.cmbBon.Name = "cmbBon";
            this.cmbBon.Size = new System.Drawing.Size(217, 24);
            this.cmbBon.TabIndex = 4;
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(98, 72);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(217, 23);
            this.dtpDatum.TabIndex = 5;
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIznos.Location = new System.Drawing.Point(98, 101);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(217, 23);
            this.txtIznos.TabIndex = 6;
            // 
            // frmBonUpis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 260);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlBottom);
            this.MaximumSize = new System.Drawing.Size(392, 298);
            this.MinimumSize = new System.Drawing.Size(392, 298);
            this.Name = "frmBonUpis";
            this.Text = "Bon upis";
            this.Load += new System.EventHandler(this.frmBonUpis_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            this.pnlFill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Label lblIznos;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        public System.Windows.Forms.TextBox txtIznos;
        public System.Windows.Forms.ComboBox cmbBon;
    }
}