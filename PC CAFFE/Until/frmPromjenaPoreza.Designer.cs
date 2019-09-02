namespace PCPOS.Until
{
    partial class frmPromjenaPoreza
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromjenaPoreza));
            this.cbPorez = new System.Windows.Forms.ComboBox();
            this.numPorez = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPromjeni = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNapomena = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPorez)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPorez
            // 
            this.cbPorez.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPorez.FormattingEnabled = true;
            this.cbPorez.Location = new System.Drawing.Point(11, 233);
            this.cbPorez.Name = "cbPorez";
            this.cbPorez.Size = new System.Drawing.Size(121, 21);
            this.cbPorez.TabIndex = 0;
            this.cbPorez.SelectedIndexChanged += new System.EventHandler(this.cbPorez_SelectedIndexChanged);
            // 
            // numPorez
            // 
            this.numPorez.Location = new System.Drawing.Point(151, 234);
            this.numPorez.Name = "numPorez";
            this.numPorez.Size = new System.Drawing.Size(120, 20);
            this.numPorez.TabIndex = 1;
            this.numPorez.Value = new decimal(new int[] {
            42,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Promijeni sa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Promijeni na:";
            // 
            // btnPromjeni
            // 
            this.btnPromjeni.BackColor = System.Drawing.Color.White;
            this.btnPromjeni.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnPromjeni.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromjeni.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnPromjeni.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPromjeni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromjeni.Location = new System.Drawing.Point(11, 279);
            this.btnPromjeni.Name = "btnPromjeni";
            this.btnPromjeni.Size = new System.Drawing.Size(121, 23);
            this.btnPromjeni.TabIndex = 4;
            this.btnPromjeni.Text = "Promijeni";
            this.btnPromjeni.UseVisualStyleBackColor = false;
            this.btnPromjeni.Click += new System.EventHandler(this.btnPromjeni_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(151, 279);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Odustani";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNapomena
            // 
            this.lblNapomena.AutoEllipsis = true;
            this.lblNapomena.BackColor = System.Drawing.Color.Silver;
            this.lblNapomena.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNapomena.Location = new System.Drawing.Point(0, 0);
            this.lblNapomena.Name = "lblNapomena";
            this.lblNapomena.Size = new System.Drawing.Size(284, 214);
            this.lblNapomena.TabIndex = 53;
            this.lblNapomena.Text = resources.GetString("lblNapomena.Text");
            // 
            // frmPromjenaPoreza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(284, 314);
            this.Controls.Add(this.lblNapomena);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPromjeni);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numPorez);
            this.Controls.Add(this.cbPorez);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmPromjenaPoreza";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promjena poreza";
            this.Load += new System.EventHandler(this.frmPromjenaPoreza_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPorez)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPorez;
        private System.Windows.Forms.NumericUpDown numPorez;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPromjeni;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNapomena;
    }
}