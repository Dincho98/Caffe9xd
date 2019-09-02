namespace PCPOS.Report.Kalkposkl
{
    partial class FaktureForm
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSPodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.dsRlisteTekst = new PCPOS.DSRlisteTekst();
            this.fakturaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRfakturaStavke = new PCPOS.DSRfakturaStavke();
            ((System.ComponentModel.ISupportInitialize)(this.dSPodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fakturaStavkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSFakture";
            reportDataSource1.Value = null;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Fakture.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(1);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1155, 612);
            this.reportViewer.TabIndex = 0;
            // 
            // dSPodaciTvrtkeBindingSource
            // 
            this.dSPodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSPodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsRlisteTekst
            // 
            this.dsRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dsRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fakturaStavkeBindingSource
            // 
            this.fakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            this.fakturaStavkeBindingSource.DataSource = this.dSRfakturaStavke;
            // 
            // dSRfakturaStavke
            // 
            this.dSRfakturaStavke.DataSetName = "DSRfakturaStavke";
            this.dSRfakturaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FaktureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 612);
            this.Controls.Add(this.reportViewer);
            this.Name = "FaktureForm";
            this.Text = "Fakture";
            this.Load += new System.EventHandler(this.FaktureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dSPodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fakturaStavkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource dSPodaciTvrtkeBindingSource;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private DSRlisteTekst dsRlisteTekst;
        private System.Windows.Forms.BindingSource fakturaStavkeBindingSource;
        private DSRfakturaStavke dSRfakturaStavke;
    }
}