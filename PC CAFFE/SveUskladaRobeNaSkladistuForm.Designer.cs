namespace PCPOS
{
    partial class SveUskladaRobeNaSkladistuForm
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
            this.sveUskladeView = new System.Windows.Forms.DataGridView();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.UrediUskladuButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sveUskladeView)).BeginInit();
            this.SuspendLayout();
            // 
            // sveUskladeView
            // 
            this.sveUskladeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sveUskladeView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sveUskladeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sveUskladeView.Location = new System.Drawing.Point(13, 79);
            this.sveUskladeView.Name = "sveUskladeView";
            this.sveUskladeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sveUskladeView.Size = new System.Drawing.Size(798, 457);
            this.sveUskladeView.TabIndex = 0;
            this.sveUskladeView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sveUskladeView_CellContentClick);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSveFakture.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSveFakture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(194, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(175, 40);
            this.btnSveFakture.TabIndex = 23;
            this.btnSveFakture.Text = "Ispis usklade robe";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = false;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(681, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 78;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UrediUskladuButton
            // 
            this.UrediUskladuButton.BackColor = System.Drawing.Color.Gainsboro;
            this.UrediUskladuButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.UrediUskladuButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.UrediUskladuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.UrediUskladuButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.UrediUskladuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UrediUskladuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UrediUskladuButton.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.UrediUskladuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UrediUskladuButton.Location = new System.Drawing.Point(13, 12);
            this.UrediUskladuButton.Name = "UrediUskladuButton";
            this.UrediUskladuButton.Size = new System.Drawing.Size(175, 40);
            this.UrediUskladuButton.TabIndex = 79;
            this.UrediUskladuButton.Text = "Uredi uskladu robe";
            this.UrediUskladuButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UrediUskladuButton.UseVisualStyleBackColor = false;
            this.UrediUskladuButton.Click += new System.EventHandler(this.UrediUskladuButton_Click);
            // 
            // SveUskladaRobeNaSkladistuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(828, 549);
            this.Controls.Add(this.UrediUskladuButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.sveUskladeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SveUskladaRobeNaSkladistuForm";
            ((System.ComponentModel.ISupportInitialize)(this.sveUskladeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sveUskladeView;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button UrediUskladuButton;
    }
}