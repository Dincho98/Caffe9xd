namespace PCPOS.Report.Robno
{
    partial class repIzdatnica
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
            this.dTRIzdatnicaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dSIzdatnica = new PCPOS.dsizd();
            this.dTIzdatnicaStavkeBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dSIzdatnicaStavke = new PCPOS.DSIzdatnicaStavke();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dTRIzdatnicaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSIzdatnica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTIzdatnicaStavkeBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSIzdatnicaStavke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRIzdatnicaBindingSource1
            // 
            this.dTRIzdatnicaBindingSource1.DataMember = "DTRIzdatnica";
            this.dTRIzdatnicaBindingSource1.DataSource = this.dSIzdatnica;
            // 
            // dSIzdatnica
            // 
            this.dSIzdatnica.DataSetName = "dsizd";
            this.dSIzdatnica.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTIzdatnicaStavkeBindingSource1
            // 
            this.dTIzdatnicaStavkeBindingSource1.DataMember = "DTIzdatnicaStavke";
            this.dTIzdatnicaStavkeBindingSource1.DataSource = this.dSIzdatnicaStavke;
            // 
            // dSIzdatnicaStavke
            // 
            this.dSIzdatnicaStavke.DataSetName = "DSIzdatnicaStavke";
            this.dSIzdatnicaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dSIzdatnica";
            reportDataSource1.Value = this.dTRIzdatnicaBindingSource1;
            reportDataSource2.Name = "dSIzdatnicaStavke";
            reportDataSource2.Value = this.dTIzdatnicaStavkeBindingSource1;
            reportDataSource3.Name = "dSPodaciTvrtke";
            reportDataSource3.Value = this.dTRpodaciTvrtkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Robno.izdatnica.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-1, -1);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(937, 743);
            this.reportViewer1.TabIndex = 5;
            // 
            // repIzdatnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 740);
            this.Controls.Add(this.reportViewer1);
            this.Name = "repIzdatnica";
            this.Text = "Izdatnica";
            this.Load += new System.EventHandler(this.repIzdatnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRIzdatnicaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSIzdatnica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTIzdatnicaStavkeBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSIzdatnicaStavke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private dsizd dSIzdatnica;
        private DSIzdatnicaStavke dSIzdatnicaStavke;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private System.Windows.Forms.BindingSource dTIzdatnicaStavkeBindingSource1;
        private System.Windows.Forms.BindingSource dTRIzdatnicaBindingSource1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

    }
}