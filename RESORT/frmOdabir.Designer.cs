namespace RESORT
{
    partial class frmOdabir
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
            this.btnUnosGosta = new System.Windows.Forms.Button();
            this.lblOdDatuma = new System.Windows.Forms.Label();
            this.lblDodatuma = new System.Windows.Forms.Label();
            this.btnRezervacija = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUnosGosta
            // 
            this.btnUnosGosta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUnosGosta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnosGosta.Location = new System.Drawing.Point(266, 12);
            this.btnUnosGosta.Name = "btnUnosGosta";
            this.btnUnosGosta.Size = new System.Drawing.Size(228, 87);
            this.btnUnosGosta.TabIndex = 1;
            this.btnUnosGosta.Text = "Novi unos gosta";
            this.btnUnosGosta.UseVisualStyleBackColor = true;
            this.btnUnosGosta.Click += new System.EventHandler(this.btnUnosGosta_Click);
            // 
            // lblOdDatuma
            // 
            this.lblOdDatuma.AutoSize = true;
            this.lblOdDatuma.BackColor = System.Drawing.Color.Transparent;
            this.lblOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOdDatuma.Location = new System.Drawing.Point(12, 113);
            this.lblOdDatuma.Name = "lblOdDatuma";
            this.lblOdDatuma.Size = new System.Drawing.Size(78, 20);
            this.lblOdDatuma.TabIndex = 14;
            this.lblOdDatuma.Text = "Odredište";
            // 
            // lblDodatuma
            // 
            this.lblDodatuma.AutoSize = true;
            this.lblDodatuma.BackColor = System.Drawing.Color.Transparent;
            this.lblDodatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDodatuma.Location = new System.Drawing.Point(257, 113);
            this.lblDodatuma.Name = "lblDodatuma";
            this.lblDodatuma.Size = new System.Drawing.Size(78, 20);
            this.lblDodatuma.TabIndex = 14;
            this.lblDodatuma.Text = "Odredište";
            // 
            // btnRezervacija
            // 
            this.btnRezervacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRezervacija.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRezervacija.Location = new System.Drawing.Point(18, 12);
            this.btnRezervacija.Name = "btnRezervacija";
            this.btnRezervacija.Size = new System.Drawing.Size(228, 87);
            this.btnRezervacija.TabIndex = 1;
            this.btnRezervacija.Text = "Izradi rezervaciju";
            this.btnRezervacija.UseVisualStyleBackColor = true;
            this.btnRezervacija.Click += new System.EventHandler(this.btnRezervacija_Click);
            // 
            // frmOdabir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 148);
            this.Controls.Add(this.lblDodatuma);
            this.Controls.Add(this.lblOdDatuma);
            this.Controls.Add(this.btnUnosGosta);
            this.Controls.Add(this.btnRezervacija);
            this.Name = "frmOdabir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Odabir";
            this.Load += new System.EventHandler(this.frmOdabir_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnosGosta;
        private System.Windows.Forms.Label lblOdDatuma;
        private System.Windows.Forms.Label lblDodatuma;
        private System.Windows.Forms.Button btnRezervacija;
    }
}