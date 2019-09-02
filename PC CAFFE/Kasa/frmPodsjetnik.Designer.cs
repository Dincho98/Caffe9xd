namespace PCPOS.Kasa
{
    partial class frmPodsjetnik
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPodsjetnik));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOdjava = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 70F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(39, 0, 39, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(821, 324);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nemoj zaboraviti \r\nna izradu računa \r\nu Kiposu.";
            // 
            // btnOdjava
            // 
            this.btnOdjava.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdjava.BackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOdjava.BackgroundImage")));
            this.btnOdjava.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdjava.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdjava.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatAppearance.BorderSize = 0;
            this.btnOdjava.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdjava.Font = new System.Drawing.Font("Arial Narrow", 15F);
            this.btnOdjava.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdjava.Location = new System.Drawing.Point(783, 441);
            this.btnOdjava.Name = "btnOdjava";
            this.btnOdjava.Size = new System.Drawing.Size(205, 96);
            this.btnOdjava.TabIndex = 97;
            this.btnOdjava.Text = "Izlaz   ESC";
            this.btnOdjava.UseVisualStyleBackColor = false;
            this.btnOdjava.Click += new System.EventHandler(this.btnOdjava_Click);
            this.btnOdjava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOdjava_KeyDown);
            // 
            // frmPodsjetnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(78F, 152F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1000, 549);
            this.Controls.Add(this.btnOdjava);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(39, 35, 39, 35);
            this.Name = "frmPodsjetnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Podsjetnik";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPodsjetnik_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOdjava;
    }
}