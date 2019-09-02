namespace PCPOS.Caffe
{
    partial class frmUskladenje
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtZalihaKolicina = new System.Windows.Forms.TextBox();
            this.txtZalihaBrojcanik = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtZalihaUlaz = new System.Windows.Forms.TextBox();
            this.lblBrojcanik = new System.Windows.Forms.Label();
            this.txtOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZalihaUkupno = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProdanoKolicina = new System.Windows.Forms.TextBox();
            this.txtProdanoBrojcanik = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProdanoStanje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPrijenosKolicina = new System.Windows.Forms.TextBox();
            this.txtPrijenosBrojcanik = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSifra = new System.Windows.Forms.Label();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.lblMjera = new System.Windows.Forms.Label();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.txtCijena = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(15, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kolicina:";
            // 
            // txtZalihaKolicina
            // 
            this.txtZalihaKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtZalihaKolicina.Location = new System.Drawing.Point(132, 19);
            this.txtZalihaKolicina.Name = "txtZalihaKolicina";
            this.txtZalihaKolicina.Size = new System.Drawing.Size(129, 23);
            this.txtZalihaKolicina.TabIndex = 2;
            this.txtZalihaKolicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtZalihaKolicina.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // txtZalihaBrojcanik
            // 
            this.txtZalihaBrojcanik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtZalihaBrojcanik.Location = new System.Drawing.Point(132, 44);
            this.txtZalihaBrojcanik.Name = "txtZalihaBrojcanik";
            this.txtZalihaBrojcanik.Size = new System.Drawing.Size(129, 23);
            this.txtZalihaBrojcanik.TabIndex = 3;
            this.txtZalihaBrojcanik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtZalihaBrojcanik.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(15, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Brojčanik:";
            // 
            // txtZalihaUlaz
            // 
            this.txtZalihaUlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtZalihaUlaz.Location = new System.Drawing.Point(132, 69);
            this.txtZalihaUlaz.Name = "txtZalihaUlaz";
            this.txtZalihaUlaz.Size = new System.Drawing.Size(129, 23);
            this.txtZalihaUlaz.TabIndex = 1;
            this.txtZalihaUlaz.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtZalihaUlaz.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // lblBrojcanik
            // 
            this.lblBrojcanik.AutoSize = true;
            this.lblBrojcanik.BackColor = System.Drawing.Color.Transparent;
            this.lblBrojcanik.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblBrojcanik.Location = new System.Drawing.Point(15, 72);
            this.lblBrojcanik.Name = "lblBrojcanik";
            this.lblBrojcanik.Size = new System.Drawing.Size(35, 15);
            this.lblBrojcanik.TabIndex = 6;
            this.lblBrojcanik.Text = "Ulaz:";
            // 
            // txtOK
            // 
            this.txtOK.BackColor = System.Drawing.Color.White;
            this.txtOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.txtOK.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.txtOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.txtOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.txtOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtOK.Location = new System.Drawing.Point(203, 446);
            this.txtOK.Name = "txtOK";
            this.txtOK.Size = new System.Drawing.Size(87, 29);
            this.txtOK.TabIndex = 5;
            this.txtOK.Text = "OK";
            this.txtOK.UseVisualStyleBackColor = false;
            this.txtOK.Click += new System.EventHandler(this.txtOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtZalihaKolicina);
            this.groupBox1.Controls.Add(this.txtZalihaBrojcanik);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblBrojcanik);
            this.groupBox1.Controls.Add(this.txtZalihaUkupno);
            this.groupBox1.Controls.Add(this.txtZalihaUlaz);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZALIHA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(15, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ukupno:";
            // 
            // txtZalihaUkupno
            // 
            this.txtZalihaUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtZalihaUkupno.Location = new System.Drawing.Point(132, 94);
            this.txtZalihaUkupno.Name = "txtZalihaUkupno";
            this.txtZalihaUkupno.Size = new System.Drawing.Size(129, 23);
            this.txtZalihaUkupno.TabIndex = 4;
            this.txtZalihaUkupno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtZalihaUkupno.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txtProdanoKolicina);
            this.groupBox2.Controls.Add(this.txtProdanoBrojcanik);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtProdanoStanje);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PRODANO";
            // 
            // txtProdanoKolicina
            // 
            this.txtProdanoKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtProdanoKolicina.Location = new System.Drawing.Point(132, 19);
            this.txtProdanoKolicina.Name = "txtProdanoKolicina";
            this.txtProdanoKolicina.Size = new System.Drawing.Size(129, 23);
            this.txtProdanoKolicina.TabIndex = 1;
            this.txtProdanoKolicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtProdanoKolicina.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // txtProdanoBrojcanik
            // 
            this.txtProdanoBrojcanik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtProdanoBrojcanik.Location = new System.Drawing.Point(132, 43);
            this.txtProdanoBrojcanik.Name = "txtProdanoBrojcanik";
            this.txtProdanoBrojcanik.Size = new System.Drawing.Size(129, 23);
            this.txtProdanoBrojcanik.TabIndex = 2;
            this.txtProdanoBrojcanik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtProdanoBrojcanik.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(15, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Stanje:";
            // 
            // txtProdanoStanje
            // 
            this.txtProdanoStanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtProdanoStanje.Location = new System.Drawing.Point(132, 68);
            this.txtProdanoStanje.Name = "txtProdanoStanje";
            this.txtProdanoStanje.Size = new System.Drawing.Size(129, 23);
            this.txtProdanoStanje.TabIndex = 3;
            this.txtProdanoStanje.Tag = "1";
            this.txtProdanoStanje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtProdanoStanje.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(15, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Brojčanik:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(15, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Količina:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txtPrijenosKolicina);
            this.groupBox3.Controls.Add(this.txtPrijenosBrojcanik);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(12, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 71);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PRIJENOS";
            // 
            // txtPrijenosKolicina
            // 
            this.txtPrijenosKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrijenosKolicina.Location = new System.Drawing.Point(132, 19);
            this.txtPrijenosKolicina.Name = "txtPrijenosKolicina";
            this.txtPrijenosKolicina.Size = new System.Drawing.Size(129, 23);
            this.txtPrijenosKolicina.TabIndex = 0;
            this.txtPrijenosKolicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtPrijenosKolicina.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // txtPrijenosBrojcanik
            // 
            this.txtPrijenosBrojcanik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrijenosBrojcanik.Location = new System.Drawing.Point(132, 42);
            this.txtPrijenosBrojcanik.Name = "txtPrijenosBrojcanik";
            this.txtPrijenosBrojcanik.Size = new System.Drawing.Size(129, 23);
            this.txtPrijenosBrojcanik.TabIndex = 1;
            this.txtPrijenosBrojcanik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            this.txtPrijenosBrojcanik.Leave += new System.EventHandler(this.txtZalihaKolicina_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.Location = new System.Drawing.Point(15, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "Brojčanik:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.Location = new System.Drawing.Point(15, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Količina:";
            // 
            // lblSifra
            // 
            this.lblSifra.AutoSize = true;
            this.lblSifra.BackColor = System.Drawing.Color.Transparent;
            this.lblSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSifra.Location = new System.Drawing.Point(13, 10);
            this.lblSifra.Name = "lblSifra";
            this.lblSifra.Size = new System.Drawing.Size(61, 17);
            this.lblSifra.TabIndex = 0;
            this.lblSifra.Text = "Kolicina:";
            // 
            // lblNaziv
            // 
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.BackColor = System.Drawing.Color.Transparent;
            this.lblNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNaziv.Location = new System.Drawing.Point(13, 29);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(61, 17);
            this.lblNaziv.TabIndex = 11;
            this.lblNaziv.Text = "Kolicina:";
            // 
            // lblMjera
            // 
            this.lblMjera.AutoSize = true;
            this.lblMjera.BackColor = System.Drawing.Color.Transparent;
            this.lblMjera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMjera.Location = new System.Drawing.Point(13, 48);
            this.lblMjera.Name = "lblMjera";
            this.lblMjera.Size = new System.Drawing.Size(61, 17);
            this.lblMjera.TabIndex = 10;
            this.lblMjera.Text = "Kolicina:";
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Location = new System.Drawing.Point(110, 446);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(87, 29);
            this.btnOdustani.TabIndex = 6;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // txtCijena
            // 
            this.txtCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCijena.Location = new System.Drawing.Point(144, 403);
            this.txtCijena.Name = "txtCijena";
            this.txtCijena.Size = new System.Drawing.Size(129, 23);
            this.txtCijena.TabIndex = 3;
            this.txtCijena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(27, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cijena";
            // 
            // frmUskladenje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(301, 487);
            this.Controls.Add(this.txtCijena);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.lblMjera);
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.lblSifra);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtOK);
            this.Name = "frmUskladenje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usklađenje";
            this.Load += new System.EventHandler(this.frmUskladenje_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZalihaKolicina;
        private System.Windows.Forms.TextBox txtZalihaBrojcanik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtZalihaUlaz;
        private System.Windows.Forms.Label lblBrojcanik;
        private System.Windows.Forms.Button txtOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZalihaUkupno;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProdanoKolicina;
        private System.Windows.Forms.TextBox txtProdanoBrojcanik;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProdanoStanje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPrijenosKolicina;
        private System.Windows.Forms.TextBox txtPrijenosBrojcanik;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSifra;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.Label lblMjera;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.TextBox txtCijena;
        private System.Windows.Forms.Label label3;
    }
}