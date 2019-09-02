namespace RESORT.Forme.izvjestaji.Statisticki_pregled
{
    partial class frmStatistikaOdabir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatistikaOdabir));
            this.btnIspis = new System.Windows.Forms.Button();
            this.chbZemlja = new System.Windows.Forms.CheckBox();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label28 = new System.Windows.Forms.Label();
            this.cbZemlja = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.BackColor = System.Drawing.Color.Transparent;
            this.btnIspis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIspis.BackgroundImage")));
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnIspis.Location = new System.Drawing.Point(12, 177);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(190, 40);
            this.btnIspis.TabIndex = 84;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // chbZemlja
            // 
            this.chbZemlja.AutoSize = true;
            this.chbZemlja.BackColor = System.Drawing.Color.Transparent;
            this.chbZemlja.Location = new System.Drawing.Point(299, 134);
            this.chbZemlja.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbZemlja.Name = "chbZemlja";
            this.chbZemlja.Size = new System.Drawing.Size(15, 14);
            this.chbZemlja.TabIndex = 81;
            this.chbZemlja.UseVisualStyleBackColor = false;
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
            this.label28.Location = new System.Drawing.Point(9, 113);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 16);
            this.label28.TabIndex = 86;
            this.label28.Text = "Zemlja";
            // 
            // cbZemlja
            // 
            this.cbZemlja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZemlja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZemlja.BackColor = System.Drawing.Color.White;
            this.cbZemlja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemlja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbZemlja.FormattingEnabled = true;
            this.cbZemlja.Location = new System.Drawing.Point(13, 129);
            this.cbZemlja.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbZemlja.Name = "cbZemlja";
            this.cbZemlja.Size = new System.Drawing.Size(279, 24);
            this.cbZemlja.TabIndex = 76;
            // 
            // frmStatistikaOdabir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 239);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.chbZemlja);
            this.Controls.Add(this.lblDO);
            this.Controls.Add(this.dtpDO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpOD);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbZemlja);
            this.Name = "frmStatistikaOdabir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistika";
            this.Load += new System.EventHandler(this.frmIspisProdajnihArtiklaNaMaliPrinter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.CheckBox chbZemlja;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbZemlja;
    }
}