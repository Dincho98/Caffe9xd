namespace PCPOS.Report.Beauty.StatistikaRadaPoZaposleniku
{
    partial class frmStatistikaRadaPoZaposleniku
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.statistikaRadaPoZaposlenikuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statistikaRadaPoZaposleniku = new PCPOS.StatistikaRadaPoZaposleniku();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblZaposlenik = new System.Windows.Forms.Label();
            this.cmbZaposlenici = new System.Windows.Forms.ComboBox();
            this.lblDatumOd = new System.Windows.Forms.Label();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.lblDatumDo = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposlenikuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposleniku)).BeginInit();
            this.SuspendLayout();
            // 
            // statistikaRadaPoZaposlenikuBindingSource
            // 
            this.statistikaRadaPoZaposlenikuBindingSource.DataMember = "dtStatistikaRadaPoZaposleniku";
            this.statistikaRadaPoZaposlenikuBindingSource.DataSource = this.statistikaRadaPoZaposleniku;
            // 
            // statistikaRadaPoZaposleniku
            // 
            this.statistikaRadaPoZaposleniku.DataSetName = "StatistikaRadaPoZaposleniku";
            this.statistikaRadaPoZaposleniku.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dsStatistikaRadaPoZaposleniku";
            reportDataSource1.Value = this.statistikaRadaPoZaposlenikuBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Beauty.StatistikaRadaPoZaposleniku.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 100);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(766, 700);
            this.reportViewer1.TabIndex = 577;
            // 
            // lblZaposlenik
            // 
            this.lblZaposlenik.AutoSize = true;
            this.lblZaposlenik.BackColor = System.Drawing.Color.Transparent;
            this.lblZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblZaposlenik.Location = new System.Drawing.Point(13, 16);
            this.lblZaposlenik.Name = "lblZaposlenik";
            this.lblZaposlenik.Size = new System.Drawing.Size(81, 17);
            this.lblZaposlenik.TabIndex = 578;
            this.lblZaposlenik.Text = "Zaposlenik:";
            // 
            // cmbZaposlenici
            // 
            this.cmbZaposlenici.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZaposlenici.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbZaposlenici.FormattingEnabled = true;
            this.cmbZaposlenici.Location = new System.Drawing.Point(101, 12);
            this.cmbZaposlenici.Name = "cmbZaposlenici";
            this.cmbZaposlenici.Size = new System.Drawing.Size(204, 24);
            this.cmbZaposlenici.TabIndex = 579;
            // 
            // lblDatumOd
            // 
            this.lblDatumOd.AutoSize = true;
            this.lblDatumOd.BackColor = System.Drawing.Color.Transparent;
            this.lblDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumOd.Location = new System.Drawing.Point(13, 45);
            this.lblDatumOd.Name = "lblDatumOd";
            this.lblDatumOd.Size = new System.Drawing.Size(73, 17);
            this.lblDatumOd.TabIndex = 580;
            this.lblDatumOd.Text = "Datum od:";
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(101, 42);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(204, 23);
            this.dtpDatumOd.TabIndex = 581;
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(101, 71);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(204, 23);
            this.dtpDatumDo.TabIndex = 583;
            // 
            // lblDatumDo
            // 
            this.lblDatumDo.AutoSize = true;
            this.lblDatumDo.BackColor = System.Drawing.Color.Transparent;
            this.lblDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumDo.Location = new System.Drawing.Point(13, 74);
            this.lblDatumDo.Name = "lblDatumDo";
            this.lblDatumDo.Size = new System.Drawing.Size(73, 17);
            this.lblDatumDo.TabIndex = 582;
            this.lblDatumDo.Text = "Datum do:";
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.White;
            this.btnShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnShow.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnShow.Location = new System.Drawing.Point(374, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(120, 53);
            this.btnShow.TabIndex = 584;
            this.btnShow.Text = "Prikaži";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // frmStatistikaRadaPoZaposleniku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(790, 812);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.lblDatumDo);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.lblDatumOd);
            this.Controls.Add(this.cmbZaposlenici);
            this.Controls.Add(this.lblZaposlenik);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmStatistikaRadaPoZaposleniku";
            this.Text = "Statistika rada po zaposleniku";
            this.Load += new System.EventHandler(this.frmStatistikaRadaPoZaposleniku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposlenikuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposleniku)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource statistikaRadaPoZaposlenikuBindingSource;
        private PCPOS.StatistikaRadaPoZaposleniku statistikaRadaPoZaposleniku;
        private System.Windows.Forms.Label lblZaposlenik;
        private System.Windows.Forms.ComboBox cmbZaposlenici;
        private System.Windows.Forms.Label lblDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label lblDatumDo;
        private System.Windows.Forms.Button btnShow;
    }
}