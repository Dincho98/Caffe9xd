namespace RESORT.Forme.izvjestaji.UkupnoPoNacinimaPlacanja
{
    partial class frmUkupnoPoNacinimaPlacanja
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.lblDoDatuma = new System.Windows.Forms.Label();
            this.dtpOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.lblOdDatuma = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSRpodaciTvrtke = new RESORT.DSRpodaciTvrtke();
            this.dSRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRfakturaStavke = new RESORT.DSRfakturaStavke();
            this.dSRfakturaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavkeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.btnPrikazi);
            this.pnlTop.Controls.Add(this.dtpDoDatuma);
            this.pnlTop.Controls.Add(this.lblDoDatuma);
            this.pnlTop.Controls.Add(this.dtpOdDatuma);
            this.pnlTop.Controls.Add(this.lblOdDatuma);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(816, 70);
            this.pnlTop.TabIndex = 0;
            // 
            // dtpDoDatuma
            // 
            this.dtpDoDatuma.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDoDatuma.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDatuma.Location = new System.Drawing.Point(96, 41);
            this.dtpDoDatuma.Name = "dtpDoDatuma";
            this.dtpDoDatuma.Size = new System.Drawing.Size(186, 23);
            this.dtpDoDatuma.TabIndex = 3;
            // 
            // lblDoDatuma
            // 
            this.lblDoDatuma.AutoSize = true;
            this.lblDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDoDatuma.Location = new System.Drawing.Point(7, 44);
            this.lblDoDatuma.Name = "lblDoDatuma";
            this.lblDoDatuma.Size = new System.Drawing.Size(81, 17);
            this.lblDoDatuma.TabIndex = 2;
            this.lblDoDatuma.Text = "Do datuma:";
            // 
            // dtpOdDatuma
            // 
            this.dtpOdDatuma.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpOdDatuma.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOdDatuma.Location = new System.Drawing.Point(96, 12);
            this.dtpOdDatuma.Name = "dtpOdDatuma";
            this.dtpOdDatuma.Size = new System.Drawing.Size(186, 23);
            this.dtpOdDatuma.TabIndex = 1;
            // 
            // lblOdDatuma
            // 
            this.lblOdDatuma.AutoSize = true;
            this.lblOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblOdDatuma.Location = new System.Drawing.Point(7, 15);
            this.lblOdDatuma.Name = "lblOdDatuma";
            this.lblOdDatuma.Size = new System.Drawing.Size(82, 17);
            this.lblOdDatuma.TabIndex = 0;
            this.lblOdDatuma.Text = "Od datuma:";
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.Color.Transparent;
            this.pnlFill.Controls.Add(this.reportViewer1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 70);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(816, 604);
            this.pnlFill.TabIndex = 1;
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPrikazi.Location = new System.Drawing.Point(724, 12);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(80, 52);
            this.btnPrikazi.TabIndex = 4;
            this.btnPrikazi.Text = "Prikaži";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.btnPrikazi_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsPodaciTvrtka";
            reportDataSource1.Value = this.dSRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "dsFakturaStavke";
            reportDataSource2.Value = this.dSRfakturaStavkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "RESORT.Forme.izvjestaji.UkupnoPoNacinimaPlacanja.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(816, 604);
            this.reportViewer1.TabIndex = 1;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRpodaciTvrtkeBindingSource
            // 
            this.dSRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRfakturaStavke
            // 
            this.dSRfakturaStavke.DataSetName = "DSRfakturaStavke";
            this.dSRfakturaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRfakturaStavkeBindingSource
            // 
            this.dSRfakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            this.dSRfakturaStavkeBindingSource.DataSource = this.dSRfakturaStavke;
            // 
            // frmUkupnoPoNacinimaPlacanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 674);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmUkupnoPoNacinimaPlacanja";
            this.Text = "Ukupno po načinima plaćanja";
            this.Load += new System.EventHandler(this.frmUkupnoPoNacinimaPlacanja_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUkupnoPoNacinimaPlacanja_Paint);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavkeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Label lblOdDatuma;
        private System.Windows.Forms.DateTimePicker dtpOdDatuma;
        private System.Windows.Forms.DateTimePicker dtpDoDatuma;
        private System.Windows.Forms.Label lblDoDatuma;
        private System.Windows.Forms.Button btnPrikazi;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dSRpodaciTvrtkeBindingSource;
        private DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dSRfakturaStavkeBindingSource;
        private DSRfakturaStavke dSRfakturaStavke;
    }
}