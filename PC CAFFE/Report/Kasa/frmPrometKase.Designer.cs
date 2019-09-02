namespace PCPOS.Report.Kasa
{
    partial class frmPrometKase
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
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.dTkasaPrometBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSKasaPromet = new PCPOS.DSKasaPromet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSkasaVrstaRobe = new PCPOS.DSkasaVrstaRobe();
            this.dTvrstaRobeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSkasaNacinPlacanja = new PCPOS.DSkasaNacinPlacanja();
            this.kasaNacinPlacanjaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTkasaPrometBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKasaPromet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkasaVrstaRobe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTvrstaRobeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkasaNacinPlacanja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kasaNacinPlacanjaBindingSource)).BeginInit();
            this.SuspendLayout();
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
            // dTkasaPrometBindingSource
            // 
            this.dTkasaPrometBindingSource.DataMember = "DTkasaPromet";
            this.dTkasaPrometBindingSource.DataSource = this.dSKasaPromet;
            // 
            // dSKasaPromet
            // 
            this.dSKasaPromet.DataSetName = "DSKasaPromet";
            this.dSKasaPromet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSpodaciTvrtke";
            reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DSkasaPromet";
            reportDataSource2.Value = this.dTkasaPrometBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kasa.KasaPromet.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(950, 678);
            this.reportViewer1.TabIndex = 3;
            // 
            // dSkasaVrstaRobe
            // 
            this.dSkasaVrstaRobe.DataSetName = "DSkasaVrstaRobe";
            this.dSkasaVrstaRobe.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTvrstaRobeBindingSource
            // 
            this.dTvrstaRobeBindingSource.DataMember = "DTvrstaRobe";
            this.dTvrstaRobeBindingSource.DataSource = this.dSkasaVrstaRobe;
            // 
            // dSkasaNacinPlacanja
            // 
            this.dSkasaNacinPlacanja.DataSetName = "DSkasaNacinPlacanja";
            this.dSkasaNacinPlacanja.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // kasaNacinPlacanjaBindingSource
            // 
            this.kasaNacinPlacanjaBindingSource.DataMember = "KasaNacinPlacanja";
            this.kasaNacinPlacanjaBindingSource.DataSource = this.dSkasaNacinPlacanja;
            // 
            // frmPrometKase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 678);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmPrometKase";
            this.Text = "Promet kase po računima";
            this.Load += new System.EventHandler(this.test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTkasaPrometBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKasaPromet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkasaVrstaRobe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTvrstaRobeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkasaNacinPlacanja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kasaNacinPlacanjaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DSKasaPromet dSKasaPromet;
        private System.Windows.Forms.BindingSource dTkasaPrometBindingSource;
        private DSkasaVrstaRobe dSkasaVrstaRobe;
        private System.Windows.Forms.BindingSource dTvrstaRobeBindingSource;
        private DSkasaNacinPlacanja dSkasaNacinPlacanja;
        private System.Windows.Forms.BindingSource kasaNacinPlacanjaBindingSource;

    }
}