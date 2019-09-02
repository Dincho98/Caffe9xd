namespace PCPOS.Report.Kalkposkl
{
    partial class KalkulacijeForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsRlisteTekst = new PCPOS.DSRlisteTekst();
            ((System.ComponentModel.ISupportInitialize)(this.DTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).BeginInit();
            this.SuspendLayout();
            // 
            // DTRpodaciTvrtkeBindingSource
            // 
            this.DTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.DTRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSPodaciTvrtke";
            reportDataSource1.Value = this.DTRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DSKalkulacijeReport";
            reportDataSource2.Value = null;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Kalkulacije.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1155, 612);
            this.reportViewer.TabIndex = 0;
            // 
            // dSpodaciTvrtkeBindingSource
            // 
            this.dSpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dsRlisteTekst
            // 
            this.dsRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dsRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // KalkulacijeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 612);
            this.Controls.Add(this.reportViewer);
            this.Name = "KalkulacijeForm";
            this.Text = "Kalkulacije";
            this.Load += new System.EventHandler(this.KalkulacijeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource dSpodaciTvrtkeBindingSource;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource DTRpodaciTvrtkeBindingSource;
        private DSRlisteTekst dsRlisteTekst;
    }
}