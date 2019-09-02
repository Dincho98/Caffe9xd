namespace PCPOS.Report.Faktura
{
    partial class repFaktura
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTRfakturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSFaktura = new PCPOS.DSFaktura();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.dtFakturaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRfakturaStavke = new PCPOS.DSRfakturaStavke();
            this.dTstopeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSstope = new PCPOS.DSstope();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.bindingSourceSlika = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetPDF417Code = new PCPOS.Report.DataSetPDF417Code();
            ((System.ComponentModel.ISupportInitialize)(this.dTRfakturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFakturaStavkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTstopeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSlika)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPDF417Code)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRfakturaBindingSource
            // 
            this.dTRfakturaBindingSource.DataMember = "DTRfaktura";
            this.dTRfakturaBindingSource.DataSource = this.dSFaktura;
            // 
            // dSFaktura
            // 
            this.dSFaktura.DataSetName = "DSFaktura";
            this.dSFaktura.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTRpodaciTvrtkeBindingSource
            // 
            this.dTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dTRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtFakturaStavkeBindingSource
            // 
            this.dtFakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            this.dtFakturaStavkeBindingSource.DataSource = this.dSRfakturaStavke;
            // 
            // dSRfakturaStavke
            // 
            this.dSRfakturaStavke.DataSetName = "DSRfakturaStavke";
            this.dSRfakturaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTstopeBindingSource
            // 
            this.dTstopeBindingSource.DataMember = "DTstope";
            this.dTstopeBindingSource.DataSource = this.dSstope;
            // 
            // dSstope
            // 
            this.dSstope.DataSetName = "DSstope";
            this.dSstope.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSKaktura";
            reportDataSource1.Value = this.dTRfakturaBindingSource;
            reportDataSource2.Name = "DSPodaciTvrtke";
            reportDataSource2.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource3.Name = "DSfakturaStavke";
            reportDataSource3.Value = this.dtFakturaStavkeBindingSource;
            reportDataSource4.Name = "DSstope";
            reportDataSource4.Value = this.dTstopeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Faktura.Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(816, 755);
            this.reportViewer1.TabIndex = 0;
            // 
            // bindingSourceSlika
            // 
            this.bindingSourceSlika.DataMember = "PDF417";
            this.bindingSourceSlika.DataSource = this.dataSetPDF417Code;
            // 
            // dataSetPDF417Code
            // 
            this.dataSetPDF417Code.DataSetName = "DataSetPDF417Code";
            this.dataSetPDF417Code.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // repFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 755);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "repFaktura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Faktura ";
            this.Load += new System.EventHandler(this.repFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRfakturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFakturaStavkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTstopeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSlika)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPDF417Code)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSFaktura dSFaktura;
        private System.Windows.Forms.BindingSource dTRfakturaBindingSource;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private DSstope dSstope;
        private System.Windows.Forms.BindingSource dTstopeBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dtFakturaStavkeBindingSource;
        private DSRfakturaStavke dSRfakturaStavke;
        private System.Windows.Forms.BindingSource bindingSourceSlika;
        private DataSetPDF417Code dataSetPDF417Code;
    }
}