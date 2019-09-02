namespace PCPOS.IzlazniDokumenti
{
    partial class ObracunGrupeProizvodaForm
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
            this.cbGrupe = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpOd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDo = new System.Windows.Forms.DateTimePicker();
            this.btnIspisi = new System.Windows.Forms.Button();
            this.checkGrupa = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbGrupe
            // 
            this.cbGrupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupe.FormattingEnabled = true;
            this.cbGrupe.Location = new System.Drawing.Point(12, 27);
            this.cbGrupe.Name = "cbGrupe";
            this.cbGrupe.Size = new System.Drawing.Size(245, 21);
            this.cbGrupe.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Grupa proizvoda :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Datum od :";
            // 
            // dtpOd
            // 
            this.dtpOd.CustomFormat = "dd/MM/yyyy";
            this.dtpOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOd.Location = new System.Drawing.Point(12, 75);
            this.dtpOd.Name = "dtpOd";
            this.dtpOd.Size = new System.Drawing.Size(266, 20);
            this.dtpOd.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Datum od :";
            // 
            // dtpDo
            // 
            this.dtpDo.CustomFormat = "dd/MM/yyyy";
            this.dtpDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDo.Location = new System.Drawing.Point(12, 126);
            this.dtpDo.Name = "dtpDo";
            this.dtpDo.Size = new System.Drawing.Size(266, 20);
            this.dtpDo.TabIndex = 5;
            // 
            // btnIspisi
            // 
            this.btnIspisi.BackColor = System.Drawing.Color.White;
            this.btnIspisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspisi.Location = new System.Drawing.Point(12, 159);
            this.btnIspisi.Name = "btnIspisi";
            this.btnIspisi.Size = new System.Drawing.Size(266, 45);
            this.btnIspisi.TabIndex = 6;
            this.btnIspisi.Text = "Ispiši";
            this.btnIspisi.UseVisualStyleBackColor = false;
            this.btnIspisi.Click += new System.EventHandler(this.btnIspisi_Click);
            // 
            // checkGrupa
            // 
            this.checkGrupa.AutoSize = true;
            this.checkGrupa.Location = new System.Drawing.Point(263, 31);
            this.checkGrupa.Name = "checkGrupa";
            this.checkGrupa.Size = new System.Drawing.Size(15, 14);
            this.checkGrupa.TabIndex = 7;
            this.checkGrupa.UseVisualStyleBackColor = true;
            // 
            // ObracunGrupeProizvodaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(290, 216);
            this.Controls.Add(this.checkGrupa);
            this.Controls.Add(this.btnIspisi);
            this.Controls.Add(this.dtpDo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpOd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbGrupe);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ObracunGrupeProizvodaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obračun grupe proizvoda";
            this.Load += new System.EventHandler(this.ObracunGrupeProizvodaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGrupe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpOd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDo;
        private System.Windows.Forms.Button btnIspisi;
        private System.Windows.Forms.CheckBox checkGrupa;
    }
}