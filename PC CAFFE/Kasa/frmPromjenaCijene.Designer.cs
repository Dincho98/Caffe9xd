namespace PCPOS.Kasa
{
    partial class frmPromjenaCijene
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromjenaCijene));
            this.txtMpc = new System.Windows.Forms.TextBox();
            this.txtVpc = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPromjena = new System.Windows.Forms.Button();
            this.CBskladiste = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMpc
            // 
            this.txtMpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMpc.Location = new System.Drawing.Point(48, 12);
            this.txtMpc.Name = "txtMpc";
            this.txtMpc.Size = new System.Drawing.Size(165, 23);
            this.txtMpc.TabIndex = 18;
            this.txtMpc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMpc_KeyDown);
            this.txtMpc.Leave += new System.EventHandler(this.txtMpc_Leave);
            // 
            // txtVpc
            // 
            this.txtVpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtVpc.Location = new System.Drawing.Point(48, 38);
            this.txtVpc.Name = "txtVpc";
            this.txtVpc.ReadOnly = true;
            this.txtVpc.Size = new System.Drawing.Size(165, 23);
            this.txtVpc.TabIndex = 18;
            this.txtVpc.Text = "0,00";
            this.txtVpc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVpc_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(12, 17);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(30, 13);
            this.label28.TabIndex = 32;
            this.label28.Text = "MPC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "VPC";
            // 
            // btnPromjena
            // 
            this.btnPromjena.BackColor = System.Drawing.Color.Transparent;
            this.btnPromjena.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPromjena.BackgroundImage")));
            this.btnPromjena.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPromjena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPromjena.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPromjena.FlatAppearance.BorderSize = 0;
            this.btnPromjena.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPromjena.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPromjena.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPromjena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromjena.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.btnPromjena.Location = new System.Drawing.Point(73, 97);
            this.btnPromjena.Name = "btnPromjena";
            this.btnPromjena.Size = new System.Drawing.Size(140, 47);
            this.btnPromjena.TabIndex = 33;
            this.btnPromjena.Text = "Promijeni cijenu F1";
            this.btnPromjena.UseVisualStyleBackColor = false;
            this.btnPromjena.Click += new System.EventHandler(this.btnPromjena_Click);
            // 
            // CBskladiste
            // 
            this.CBskladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CBskladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBskladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBskladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.CBskladiste.FormattingEnabled = true;
            this.CBskladiste.Location = new System.Drawing.Point(48, 67);
            this.CBskladiste.Name = "CBskladiste";
            this.CBskladiste.Size = new System.Drawing.Size(165, 24);
            this.CBskladiste.TabIndex = 52;
            this.CBskladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBskladiste_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Skl.";
            // 
            // frmPromjenaCijene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(231, 168);
            this.Controls.Add(this.CBskladiste);
            this.Controls.Add(this.btnPromjena);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtVpc);
            this.Controls.Add(this.txtMpc);
            this.Name = "frmPromjenaCijene";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promjena cijene";
            this.Load += new System.EventHandler(this.frmPromjenaCijene_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMpc;
        private System.Windows.Forms.TextBox txtVpc;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPromjena;
        private System.Windows.Forms.ComboBox CBskladiste;
        private System.Windows.Forms.Label label2;
    }
}