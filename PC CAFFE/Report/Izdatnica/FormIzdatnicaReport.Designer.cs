namespace PCPOS.Report.Izdatnica
{
    partial class FormIzdatnicaReport
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
            this.bindingSourceIzdatnica = new System.Windows.Forms.BindingSource(this.components);
            this.IzdatnicaDataSet = new PCPOS.Report.Izdatnica.IzdatnicaDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.bindingSourcePodaciTvrtke = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceIzdatnica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IzdatnicaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSourceIzdatnica
            // 
            this.bindingSourceIzdatnica.DataMember = "DataTableIzdatnica";
            this.bindingSourceIzdatnica.DataSource = this.IzdatnicaDataSet;
            // 
            // IzdatnicaDataSet
            // 
            this.IzdatnicaDataSet.DataSetName = "IzdatnicaDataSet";
            this.IzdatnicaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // FormIzdatnicaReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 450);
            this.Name = "FormIzdatnicaReport";
            this.Text = "FormIzdatnicaReport";
            this.Load += new System.EventHandler(this.FormIzdatnicaReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceIzdatnica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IzdatnicaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourceIzdatnica;
        private IzdatnicaDataSet IzdatnicaDataSet;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource bindingSourcePodaciTvrtke;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}