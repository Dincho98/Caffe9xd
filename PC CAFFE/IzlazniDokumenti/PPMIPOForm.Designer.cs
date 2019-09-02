namespace PCPOS.IzlazniDokumenti
{
    partial class PPMIPOForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXML = new System.Windows.Forms.Button();
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dsRpodaciTvrtke = new PCPOS.DSRpodaciTvrtke();
            this.dsRlisteTekst = new PCPOS.DSRlisteTekst();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnXML);
            this.panel1.Controls.Add(this.cbDucan);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnTrazi);
            this.panel1.Controls.Add(this.dtpDatumDo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpDatumOd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 139);
            this.panel1.TabIndex = 1;
            // 
            // btnXML
            // 
            this.btnXML.BackColor = System.Drawing.Color.White;
            this.btnXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnXML.Location = new System.Drawing.Point(129, 92);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(104, 36);
            this.btnXML.TabIndex = 7;
            this.btnXML.Text = "Ispiši XML";
            this.btnXML.UseVisualStyleBackColor = false;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // cbDucan
            // 
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(93, 63);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(140, 21);
            this.cbDucan.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ducan :";
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackColor = System.Drawing.Color.White;
            this.btnTrazi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrazi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnTrazi.Location = new System.Drawing.Point(11, 92);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(104, 36);
            this.btnTrazi.TabIndex = 4;
            this.btnTrazi.Text = "Ispiši PDF";
            this.btnTrazi.UseVisualStyleBackColor = false;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumDo.CustomFormat = "dd/MM/yyyy";
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatumDo.Location = new System.Drawing.Point(93, 36);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(140, 20);
            this.dtpDatumDo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Datum do :";
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumOd.CustomFormat = "dd/MM/yyyy";
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatumOd.Location = new System.Drawing.Point(93, 10);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(140, 20);
            this.dtpDatumOd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datum od :";
            // 
            // dsRpodaciTvrtke
            // 
            this.dsRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dsRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsRlisteTekst
            // 
            this.dsRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dsRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "txt files (*.txt)|";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // PPMIPOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(276, 162);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "PPMIPOForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PP-MI-PO";
            this.Load += new System.EventHandler(this.PPMIPOForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRlisteTekst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnXML;
        private DSRpodaciTvrtke dsRpodaciTvrtke;
        private DSRlisteTekst dsRlisteTekst;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}