namespace PCPOS {
    partial class frmNaknadnaFiskalizacija {
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
            this.dgw = new System.Windows.Forms.DataGridView();
            this.BR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poslovnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poslovni_prostor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFiskaliziraj = new System.Windows.Forms.Button();
            this.btnFiskalizirajJedan = new System.Windows.Forms.Button();
            this.btnFiskalizirajOdabrano = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BR,
            this.broj,
            this.datum,
            this.poslovnica,
            this.poslovni_prostor,
            this.iznos});
            this.dgw.Location = new System.Drawing.Point(13, 110);
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(678, 444);
            this.dgw.TabIndex = 0;
            // 
            // BR
            // 
            this.BR.HeaderText = "BR";
            this.BR.Name = "BR";
            this.BR.ReadOnly = true;
            // 
            // broj
            // 
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // poslovnica
            // 
            this.poslovnica.HeaderText = "Poslovnica";
            this.poslovnica.Name = "poslovnica";
            this.poslovnica.ReadOnly = true;
            // 
            // poslovni_prostor
            // 
            this.poslovni_prostor.HeaderText = "Poslovni prostor";
            this.poslovni_prostor.Name = "poslovni_prostor";
            this.poslovni_prostor.ReadOnly = true;
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            // 
            // btnFiskaliziraj
            // 
            this.btnFiskaliziraj.BackColor = System.Drawing.Color.White;
            this.btnFiskaliziraj.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnFiskaliziraj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskaliziraj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskaliziraj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnFiskaliziraj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiskaliziraj.Location = new System.Drawing.Point(13, 34);
            this.btnFiskaliziraj.Name = "btnFiskaliziraj";
            this.btnFiskaliziraj.Size = new System.Drawing.Size(178, 33);
            this.btnFiskaliziraj.TabIndex = 1;
            this.btnFiskaliziraj.Text = "Učitaj sve";
            this.btnFiskaliziraj.UseVisualStyleBackColor = false;
            this.btnFiskaliziraj.Click += new System.EventHandler(this.btnFiskaliziraj_Click);
            // 
            // btnFiskalizirajJedan
            // 
            this.btnFiskalizirajJedan.BackColor = System.Drawing.Color.White;
            this.btnFiskalizirajJedan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnFiskalizirajJedan.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskalizirajJedan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskalizirajJedan.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnFiskalizirajJedan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiskalizirajJedan.Location = new System.Drawing.Point(210, 34);
            this.btnFiskalizirajJedan.Name = "btnFiskalizirajJedan";
            this.btnFiskalizirajJedan.Size = new System.Drawing.Size(232, 33);
            this.btnFiskalizirajJedan.TabIndex = 2;
            this.btnFiskalizirajJedan.Text = "Učitaj samo jedan račun";
            this.btnFiskalizirajJedan.UseVisualStyleBackColor = false;
            this.btnFiskalizirajJedan.Click += new System.EventHandler(this.btnFiskalizirajJedan_Click);
            // 
            // btnFiskalizirajOdabrano
            // 
            this.btnFiskalizirajOdabrano.BackColor = System.Drawing.Color.White;
            this.btnFiskalizirajOdabrano.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnFiskalizirajOdabrano.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskalizirajOdabrano.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnFiskalizirajOdabrano.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnFiskalizirajOdabrano.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiskalizirajOdabrano.Location = new System.Drawing.Point(461, 34);
            this.btnFiskalizirajOdabrano.Name = "btnFiskalizirajOdabrano";
            this.btnFiskalizirajOdabrano.Size = new System.Drawing.Size(178, 33);
            this.btnFiskalizirajOdabrano.TabIndex = 3;
            this.btnFiskalizirajOdabrano.Text = "Fiskaliziraj";
            this.btnFiskalizirajOdabrano.UseVisualStyleBackColor = false;
            this.btnFiskalizirajOdabrano.Click += new System.EventHandler(this.btnFiskalizirajOdabrano_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ovu funkciju nemojte sami koristiti.\r\nZa više informacija kontaktirajte Code-iT";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(175, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmNaknadnaFiskalizacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(703, 566);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFiskalizirajOdabrano);
            this.Controls.Add(this.btnFiskalizirajJedan);
            this.Controls.Add(this.btnFiskaliziraj);
            this.Controls.Add(this.dgw);
            this.Name = "frmNaknadnaFiskalizacija";
            this.Text = "Naknadna fiskalizacija";
            this.Load += new System.EventHandler(this.frmNaknadnaFiskalizacija_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DataGridViewTextBoxColumn BR;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn poslovnica;
        private System.Windows.Forms.DataGridViewTextBoxColumn poslovni_prostor;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.Button btnFiskaliziraj;
        private System.Windows.Forms.Button btnFiskalizirajJedan;
        private System.Windows.Forms.Button btnFiskalizirajOdabrano;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}