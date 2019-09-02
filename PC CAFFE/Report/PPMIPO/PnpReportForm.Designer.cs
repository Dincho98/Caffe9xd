namespace PCPOS.Report.PPMIPO
{
    partial class PnpReportForm
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
            this.dsRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsRlisteTekst = new PCPOS.DSRlisteTekst();
            ((System.ComponentModel.ISupportInitialize)(this.dsRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).BeginInit();
            this.SuspendLayout();
            // 
            // dsRpodaciTvrtke
            // 
            this.dsRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dsRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(18, 16);
            this.reportViewer.Name = "ReportViewer";
            this.reportViewer.Size = new System.Drawing.Size(396, 246);
            this.reportViewer.TabIndex = 0;
            // 
            // dsRlisteTekst
            // 
            this.dsRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dsRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PnpReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 612);
            this.Name = "PnpReportForm";
            this.Text = "Porez na potrošnju";
            this.Load += new System.EventHandler(this.PnpReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DSRpodaciTvrtke dsRpodaciTvrtke;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private DSRlisteTekst dsRlisteTekst;
    }
}