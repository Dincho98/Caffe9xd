namespace PCPOS.Report.NaruzdbeNaStolu
{
    partial class FrmNarudzbeNaStolu
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.bindingSourceNarudzbe = new System.Windows.Forms.BindingSource(this.components);
            this.narudzbeNaStoluDataSet = new PCPOS.Report.NaruzdbeNaStolu.NarudzbeNaStoluDataSet();
            this.bindingSourcePodaciTvrtke = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNarudzbe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.narudzbeNaStoluDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // bindingSourceNarudzbe
            // 
            this.bindingSourceNarudzbe.DataMember = "DTNarudzbe";
            this.bindingSourceNarudzbe.DataSource = this.narudzbeNaStoluDataSet;
            // 
            // narudzbeNaStoluDataSet
            // 
            this.narudzbeNaStoluDataSet.DataSetName = "NarudzbeNaStoluDataSet";
            this.narudzbeNaStoluDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // FrmNarudzbeNaStolu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 414);
            this.Name = "FrmNarudzbeNaStolu";
            this.Text = "Izvještaj - Narudžbe na stolu";
            this.Load += new System.EventHandler(this.FrmNarudzbeNaStolu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNarudzbe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.narudzbeNaStoluDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource bindingSourceNarudzbe;
        private NarudzbeNaStoluDataSet narudzbeNaStoluDataSet;
        private System.Windows.Forms.BindingSource bindingSourcePodaciTvrtke;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}