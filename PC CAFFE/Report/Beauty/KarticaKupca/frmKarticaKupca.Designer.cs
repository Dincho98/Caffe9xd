namespace PCPOS.Report.Beauty.KarticaKupca
{
    partial class frmKarticaKupca
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKarticaKupca));
            this.statistikaRadaPoZaposlenikuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statistikaRadaPoZaposleniku = new PCPOS.StatistikaRadaPoZaposleniku();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblDatumOd = new System.Windows.Forms.Label();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.lblDatumDo = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.chbZbirno = new System.Windows.Forms.CheckBox();
            this.btnPonistiPartnera = new System.Windows.Forms.Button();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.lblSifraPartnera = new System.Windows.Forms.Label();
            this.txtPartnerSifra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposlenikuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposleniku)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            this.SuspendLayout();
            // 
            // statistikaRadaPoZaposlenikuBindingSource
            // 
            this.statistikaRadaPoZaposlenikuBindingSource.DataMember = "dtStatistikaRadaPoZaposleniku";
            this.statistikaRadaPoZaposlenikuBindingSource.DataSource = this.statistikaRadaPoZaposleniku;
            // 
            // statistikaRadaPoZaposleniku
            // 
            this.statistikaRadaPoZaposleniku.DataSetName = "StatistikaRadaPoZaposleniku";
            this.statistikaRadaPoZaposleniku.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource3.Name = "dsStatistikaRadaPoZaposleniku";
            reportDataSource3.Value = this.statistikaRadaPoZaposlenikuBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Beauty.StatistikaRadaPoZaposleniku.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 71);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(766, 729);
            this.reportViewer1.TabIndex = 577;
            // 
            // lblDatumOd
            // 
            this.lblDatumOd.AutoSize = true;
            this.lblDatumOd.BackColor = System.Drawing.Color.Transparent;
            this.lblDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumOd.Location = new System.Drawing.Point(12, 45);
            this.lblDatumOd.Name = "lblDatumOd";
            this.lblDatumOd.Size = new System.Drawing.Size(73, 17);
            this.lblDatumOd.TabIndex = 580;
            this.lblDatumOd.Text = "Datum od:";
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(117, 42);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(167, 23);
            this.dtpDatumOd.TabIndex = 581;
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(406, 42);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(167, 23);
            this.dtpDatumDo.TabIndex = 583;
            // 
            // lblDatumDo
            // 
            this.lblDatumDo.AutoSize = true;
            this.lblDatumDo.BackColor = System.Drawing.Color.Transparent;
            this.lblDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumDo.Location = new System.Drawing.Point(327, 45);
            this.lblDatumDo.Name = "lblDatumDo";
            this.lblDatumDo.Size = new System.Drawing.Size(73, 17);
            this.lblDatumDo.TabIndex = 582;
            this.lblDatumDo.Text = "Datum do:";
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.White;
            this.btnShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnShow.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnShow.Location = new System.Drawing.Point(658, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(120, 53);
            this.btnShow.TabIndex = 584;
            this.btnShow.Text = "Prikaži";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // chbZbirno
            // 
            this.chbZbirno.AutoSize = true;
            this.chbZbirno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chbZbirno.Location = new System.Drawing.Point(579, 14);
            this.chbZbirno.Name = "chbZbirno";
            this.chbZbirno.Size = new System.Drawing.Size(68, 21);
            this.chbZbirno.TabIndex = 590;
            this.chbZbirno.Text = "Zbirno";
            this.chbZbirno.UseVisualStyleBackColor = true;
            this.chbZbirno.CheckedChanged += new System.EventHandler(this.chbZbirno_CheckedChanged);
            // 
            // btnPonistiPartnera
            // 
            this.btnPonistiPartnera.Location = new System.Drawing.Point(548, 11);
            this.btnPonistiPartnera.Name = "btnPonistiPartnera";
            this.btnPonistiPartnera.Size = new System.Drawing.Size(25, 25);
            this.btnPonistiPartnera.TabIndex = 589;
            this.btnPonistiPartnera.Text = "X";
            this.btnPonistiPartnera.UseVisualStyleBackColor = true;
            this.btnPonistiPartnera.Click += new System.EventHandler(this.btnPonistiPartnera_Click);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(220, 8);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(30, 30);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 588;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(256, 12);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(286, 23);
            this.txtPartnerNaziv.TabIndex = 587;
            // 
            // lblSifraPartnera
            // 
            this.lblSifraPartnera.AutoSize = true;
            this.lblSifraPartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSifraPartnera.Location = new System.Drawing.Point(12, 15);
            this.lblSifraPartnera.Name = "lblSifraPartnera";
            this.lblSifraPartnera.Size = new System.Drawing.Size(99, 17);
            this.lblSifraPartnera.TabIndex = 586;
            this.lblSifraPartnera.Text = "Šifra partnera:";
            // 
            // txtPartnerSifra
            // 
            this.txtPartnerSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerSifra.Location = new System.Drawing.Point(117, 12);
            this.txtPartnerSifra.Name = "txtPartnerSifra";
            this.txtPartnerSifra.Size = new System.Drawing.Size(97, 23);
            this.txtPartnerSifra.TabIndex = 585;
            this.txtPartnerSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerSifra_KeyDown);
            // 
            // frmKarticaKupca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(790, 812);
            this.Controls.Add(this.chbZbirno);
            this.Controls.Add(this.btnPonistiPartnera);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.txtPartnerNaziv);
            this.Controls.Add(this.lblSifraPartnera);
            this.Controls.Add(this.txtPartnerSifra);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.lblDatumDo);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.lblDatumOd);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmKarticaKupca";
            this.Text = "Statistika rada po zaposleniku";
            this.Load += new System.EventHandler(this.frmKarticaKupca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposlenikuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statistikaRadaPoZaposleniku)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource statistikaRadaPoZaposlenikuBindingSource;
        private PCPOS.StatistikaRadaPoZaposleniku statistikaRadaPoZaposleniku;
        private System.Windows.Forms.Label lblDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label lblDatumDo;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.CheckBox chbZbirno;
        private System.Windows.Forms.Button btnPonistiPartnera;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.Label lblSifraPartnera;
        private System.Windows.Forms.TextBox txtPartnerSifra;
    }
}