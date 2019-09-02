namespace PCPOS.Report.OtpisRobe
{
    partial class FormOtpisRobeReport
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
            this.bindingSourceOtpis = new System.Windows.Forms.BindingSource(this.components);
            this.izdatnicaDataSet = new PCPOS.Report.Izdatnica.IzdatnicaDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.bindingSourcePodaciTvrtke = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOtpis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.izdatnicaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSourceOtpis
            // 
            this.bindingSourceOtpis.DataMember = "DataTableIzdatnica";
            this.bindingSourceOtpis.DataSource = this.izdatnicaDataSet;
            // 
            // izdatnicaDataSet
            // 
            this.izdatnicaDataSet.DataSetName = "IzdatnicaDataSet";
            this.izdatnicaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // bindingSourcePodaciTvrtke
            // 
            this.bindingSourcePodaciTvrtke.DataMember = "DTRpodaciTvrtke";
            this.bindingSourcePodaciTvrtke.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FormOtpisRobeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FormOtpisRobeReport";
            this.Text = "Otpis Robe - Izvjestaj";
            this.Load += new System.EventHandler(this.FormOtpisRobeReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOtpis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.izdatnicaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourceOtpis;
        private Izdatnica.IzdatnicaDataSet izdatnicaDataSet;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource bindingSourcePodaciTvrtke;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}