namespace PCPOS.Caffe
{
    partial class frmOdabirPodgrupe
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
            this.btnPice = new System.Windows.Forms.Button();
            this.btnHrana = new System.Windows.Forms.Button();
            this.btnTrgovackaRoba = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPice
            // 
            this.btnPice.BackColor = System.Drawing.Color.White;
            this.btnPice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPice.Location = new System.Drawing.Point(12, 12);
            this.btnPice.Name = "btnPice";
            this.btnPice.Size = new System.Drawing.Size(284, 68);
            this.btnPice.TabIndex = 0;
            this.btnPice.Text = "Piće";
            this.btnPice.UseVisualStyleBackColor = false;
            this.btnPice.Click += new System.EventHandler(this.BtnPice_Click);
            // 
            // btnHrana
            // 
            this.btnHrana.BackColor = System.Drawing.Color.White;
            this.btnHrana.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHrana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHrana.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnHrana.Location = new System.Drawing.Point(12, 86);
            this.btnHrana.Name = "btnHrana";
            this.btnHrana.Size = new System.Drawing.Size(284, 68);
            this.btnHrana.TabIndex = 1;
            this.btnHrana.Text = "Hrana";
            this.btnHrana.UseVisualStyleBackColor = false;
            this.btnHrana.Click += new System.EventHandler(this.BtnHrana_Click);
            // 
            // btnTrgovackaRoba
            // 
            this.btnTrgovackaRoba.BackColor = System.Drawing.Color.White;
            this.btnTrgovackaRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrgovackaRoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrgovackaRoba.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnTrgovackaRoba.Location = new System.Drawing.Point(12, 160);
            this.btnTrgovackaRoba.Name = "btnTrgovackaRoba";
            this.btnTrgovackaRoba.Size = new System.Drawing.Size(284, 68);
            this.btnTrgovackaRoba.TabIndex = 2;
            this.btnTrgovackaRoba.Text = "Trgovačka roba";
            this.btnTrgovackaRoba.UseVisualStyleBackColor = false;
            this.btnTrgovackaRoba.Click += new System.EventHandler(this.BtnTrgovackaRoba_Click);
            // 
            // frmOdabirPodgrupe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(308, 241);
            this.Controls.Add(this.btnTrgovackaRoba);
            this.Controls.Add(this.btnHrana);
            this.Controls.Add(this.btnPice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmOdabirPodgrupe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Odabir podgrupe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPice;
        private System.Windows.Forms.Button btnHrana;
        private System.Windows.Forms.Button btnTrgovackaRoba;
    }
}