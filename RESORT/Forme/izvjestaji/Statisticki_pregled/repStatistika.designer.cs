using RESORT;
namespace RESORT.izvjestaji.Statisticki_pregled
{
    partial class repStatistika
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
            this.dTstatistikaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSstatistike = new RESORT.DSstatistike();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dTstatistikaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstatistike)).BeginInit();
            this.SuspendLayout();
            // 
            // dTstatistikaBindingSource
            // 
            this.dTstatistikaBindingSource.DataMember = "DTstatistika";
            this.dTstatistikaBindingSource.DataSource = this.dSstatistike;
            // 
            // dSstatistike
            // 
            this.dSstatistike.DataSetName = "DSstatistike";
            this.dSstatistike.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSstatistika";
            reportDataSource1.Value = this.dTstatistikaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "RESORT.Forme.izvjestaji.Statisticki_pregled.Statistika.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(816, 674);
            this.reportViewer1.TabIndex = 0;
            // 
            // repStatistika
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 674);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "repStatistika";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistika";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.repStatistika_FormClosing);
            this.Load += new System.EventHandler(this.repFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTstatistikaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstatistike)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSstatistike dSstatistike;
        private System.Windows.Forms.BindingSource dTstatistikaBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;



    }
}