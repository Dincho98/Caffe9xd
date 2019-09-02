namespace PCPOS
{
    partial class frmKasaPrijava
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbBlagajnik = new System.Windows.Forms.ComboBox();
            this.txtZaporka = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUlaz = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(23, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Blagajnik:";
            // 
            // cbBlagajnik
            // 
            this.cbBlagajnik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbBlagajnik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbBlagajnik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlagajnik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbBlagajnik.FormattingEnabled = true;
            this.cbBlagajnik.Location = new System.Drawing.Point(100, 28);
            this.cbBlagajnik.Name = "cbBlagajnik";
            this.cbBlagajnik.Size = new System.Drawing.Size(225, 28);
            this.cbBlagajnik.TabIndex = 0;
            this.cbBlagajnik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbBlagajnik_KeyDown);
            // 
            // txtZaporka
            // 
            this.txtZaporka.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtZaporka.Location = new System.Drawing.Point(100, 59);
            this.txtZaporka.Name = "txtZaporka";
            this.txtZaporka.Size = new System.Drawing.Size(225, 26);
            this.txtZaporka.TabIndex = 1;
            this.txtZaporka.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZaporka_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(24, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Zaporka:";
            // 
            // btnUlaz
            // 
            this.btnUlaz.FlatAppearance.BorderSize = 0;
            this.btnUlaz.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUlaz.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUlaz.Image = global::PCPOS.Properties.Resources.Log_Out_icon;
            this.btnUlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUlaz.Location = new System.Drawing.Point(331, 28);
            this.btnUlaz.Name = "btnUlaz";
            this.btnUlaz.Size = new System.Drawing.Size(99, 57);
            this.btnUlaz.TabIndex = 63;
            this.btnUlaz.Text = "     Ulaz";
            this.btnUlaz.UseVisualStyleBackColor = true;
            this.btnUlaz.Click += new System.EventHandler(this.btnUlaz_Click);
            // 
            // frmKasaPrijava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 119);
            this.Controls.Add(this.btnUlaz);
            this.Controls.Add(this.txtZaporka);
            this.Controls.Add(this.cbBlagajnik);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKasaPrijava";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prijava";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKasaPrijava_FormClosing);
            this.Load += new System.EventHandler(this.frmKasaPrijava_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBlagajnik;
        private System.Windows.Forms.TextBox txtZaporka;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUlaz;
    }
}