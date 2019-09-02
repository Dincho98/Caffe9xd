namespace RESORT.Fiskalizacija
{
    partial class frmPodaciFiskalizacija
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
            this.txtOIB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chbAktivnost = new System.Windows.Forms.CheckBox();
            this.cbSustavPDV = new System.Windows.Forms.ComboBox();
            this.cbPoslovniProstor = new System.Windows.Forms.ComboBox();
            this.cbOznakaBlagajna = new System.Windows.Forms.ComboBox();
            this.cbOznakaSlijednosti = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFazaFiskalizacije = new System.Windows.Forms.ComboBox();
            this.txtNazivCertifikata = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.chbONPavans = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtOIB
            // 
            this.txtOIB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOIB.Location = new System.Drawing.Point(203, 59);
            this.txtOIB.MaxLength = 11;
            this.txtOIB.Name = "txtOIB";
            this.txtOIB.Size = new System.Drawing.Size(215, 23);
            this.txtOIB.TabIndex = 66;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F);
            this.label15.Location = new System.Drawing.Point(27, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 14);
            this.label15.TabIndex = 67;
            this.label15.Text = "OIB:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F);
            this.label1.Location = new System.Drawing.Point(27, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 14);
            this.label1.TabIndex = 67;
            this.label1.Text = "Oznaka poslovni prostor:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F);
            this.label2.Location = new System.Drawing.Point(27, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 67;
            this.label2.Text = "Oznaka blagajna:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(28, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 14);
            this.label4.TabIndex = 67;
            this.label4.Text = "Oznaka sljednosti:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(28, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 14);
            this.label5.TabIndex = 67;
            this.label5.Text = "Testna faza fiskalizacije:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F);
            this.label6.Location = new System.Drawing.Point(28, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 14);
            this.label6.TabIndex = 67;
            this.label6.Text = "Aktivnost fiskalizacije:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chbAktivnost
            // 
            this.chbAktivnost.AutoSize = true;
            this.chbAktivnost.Location = new System.Drawing.Point(204, 277);
            this.chbAktivnost.Name = "chbAktivnost";
            this.chbAktivnost.Size = new System.Drawing.Size(15, 14);
            this.chbAktivnost.TabIndex = 68;
            this.chbAktivnost.UseVisualStyleBackColor = true;
            // 
            // cbSustavPDV
            // 
            this.cbSustavPDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSustavPDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSustavPDV.FormattingEnabled = true;
            this.cbSustavPDV.Location = new System.Drawing.Point(204, 208);
            this.cbSustavPDV.Name = "cbSustavPDV";
            this.cbSustavPDV.Size = new System.Drawing.Size(215, 24);
            this.cbSustavPDV.TabIndex = 71;
            // 
            // cbPoslovniProstor
            // 
            this.cbPoslovniProstor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoslovniProstor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbPoslovniProstor.FormattingEnabled = true;
            this.cbPoslovniProstor.Location = new System.Drawing.Point(204, 88);
            this.cbPoslovniProstor.Name = "cbPoslovniProstor";
            this.cbPoslovniProstor.Size = new System.Drawing.Size(214, 24);
            this.cbPoslovniProstor.TabIndex = 70;
            // 
            // cbOznakaBlagajna
            // 
            this.cbOznakaBlagajna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOznakaBlagajna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbOznakaBlagajna.FormattingEnabled = true;
            this.cbOznakaBlagajna.Location = new System.Drawing.Point(203, 118);
            this.cbOznakaBlagajna.Name = "cbOznakaBlagajna";
            this.cbOznakaBlagajna.Size = new System.Drawing.Size(215, 24);
            this.cbOznakaBlagajna.TabIndex = 69;
            // 
            // cbOznakaSlijednosti
            // 
            this.cbOznakaSlijednosti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOznakaSlijednosti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbOznakaSlijednosti.FormattingEnabled = true;
            this.cbOznakaSlijednosti.Location = new System.Drawing.Point(204, 178);
            this.cbOznakaSlijednosti.Name = "cbOznakaSlijednosti";
            this.cbOznakaSlijednosti.Size = new System.Drawing.Size(215, 24);
            this.cbOznakaSlijednosti.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F);
            this.label7.Location = new System.Drawing.Point(28, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 14);
            this.label7.TabIndex = 67;
            this.label7.Text = "Tvrtka je u sustavu PDV-a:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbFazaFiskalizacije
            // 
            this.cbFazaFiskalizacije.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFazaFiskalizacije.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbFazaFiskalizacije.FormattingEnabled = true;
            this.cbFazaFiskalizacije.Location = new System.Drawing.Point(204, 238);
            this.cbFazaFiskalizacije.Name = "cbFazaFiskalizacije";
            this.cbFazaFiskalizacije.Size = new System.Drawing.Size(215, 24);
            this.cbFazaFiskalizacije.TabIndex = 71;
            // 
            // txtNazivCertifikata
            // 
            this.txtNazivCertifikata.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNazivCertifikata.Location = new System.Drawing.Point(203, 32);
            this.txtNazivCertifikata.MaxLength = 11;
            this.txtNazivCertifikata.Name = "txtNazivCertifikata";
            this.txtNazivCertifikata.Size = new System.Drawing.Size(215, 23);
            this.txtNazivCertifikata.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(27, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 14);
            this.label3.TabIndex = 74;
            this.label3.Text = "Naziv certifikata:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSpremi
            // 
            this.btnSpremi.Location = new System.Drawing.Point(31, 320);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(109, 23);
            this.btnSpremi.TabIndex = 75;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F);
            this.label8.Location = new System.Drawing.Point(28, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 14);
            this.label8.TabIndex = 67;
            this.label8.Text = "Oznaka avansa:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chbONPavans
            // 
            this.chbONPavans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chbONPavans.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbONPavans.FormattingEnabled = true;
            this.chbONPavans.Location = new System.Drawing.Point(204, 148);
            this.chbONPavans.Name = "chbONPavans";
            this.chbONPavans.Size = new System.Drawing.Size(215, 24);
            this.chbONPavans.TabIndex = 69;
            // 
            // frmPodaciFiskalizacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 363);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.txtNazivCertifikata);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFazaFiskalizacije);
            this.Controls.Add(this.cbSustavPDV);
            this.Controls.Add(this.cbPoslovniProstor);
            this.Controls.Add(this.chbONPavans);
            this.Controls.Add(this.cbOznakaBlagajna);
            this.Controls.Add(this.cbOznakaSlijednosti);
            this.Controls.Add(this.chbAktivnost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOIB);
            this.Controls.Add(this.label15);
            this.Name = "frmPodaciFiskalizacija";
            this.Text = "Podaci fiskalizacija";
            this.Load += new System.EventHandler(this.frmPodaciFiskalizacija_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOIB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbAktivnost;
        private System.Windows.Forms.ComboBox cbSustavPDV;
        private System.Windows.Forms.ComboBox cbPoslovniProstor;
        private System.Windows.Forms.ComboBox cbOznakaBlagajna;
        private System.Windows.Forms.ComboBox cbOznakaSlijednosti;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbFazaFiskalizacije;
        private System.Windows.Forms.TextBox txtNazivCertifikata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox chbONPavans;
    }
}