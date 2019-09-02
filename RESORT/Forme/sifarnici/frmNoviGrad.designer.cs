namespace RESORT.Forme.Sifarnik
{
    partial class frmNoviGrad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoviGrad));
            this.cbDrazava = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPosta = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_zupanija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drzava = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.naselje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZupanija = new System.Windows.Forms.TextBox();
            this.txtnazivGrada = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNaselje = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDrazava
            // 
            this.cbDrazava.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDrazava.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDrazava.BackColor = System.Drawing.Color.White;
            this.cbDrazava.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDrazava.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDrazava.FormattingEnabled = true;
            this.cbDrazava.Location = new System.Drawing.Point(326, 73);
            this.cbDrazava.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDrazava.Name = "cbDrazava";
            this.cbDrazava.Size = new System.Drawing.Size(155, 24);
            this.cbDrazava.TabIndex = 118;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(269, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 116;
            this.label2.Text = "Država:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(269, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 117;
            this.label3.Text = "Pošta:";
            // 
            // txtPosta
            // 
            this.txtPosta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPosta.Location = new System.Drawing.Point(326, 45);
            this.txtPosta.MaxLength = 100;
            this.txtPosta.Name = "txtPosta";
            this.txtPosta.Size = new System.Drawing.Size(155, 22);
            this.txtPosta.TabIndex = 115;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grad,
            this.posta,
            this.id_zupanija,
            this.drzava,
            this.naselje,
            this.id_grad});
            this.dgv.Location = new System.Drawing.Point(11, 138);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(622, 450);
            this.dgv.TabIndex = 114;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // grad
            // 
            this.grad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grad.HeaderText = "Grad";
            this.grad.Name = "grad";
            // 
            // posta
            // 
            this.posta.HeaderText = "Pošta";
            this.posta.Name = "posta";
            // 
            // id_zupanija
            // 
            this.id_zupanija.HeaderText = "Županija";
            this.id_zupanija.Name = "id_zupanija";
            this.id_zupanija.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // drzava
            // 
            this.drzava.HeaderText = "Država";
            this.drzava.Name = "drzava";
            this.drzava.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drzava.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // naselje
            // 
            this.naselje.HeaderText = "Naselje";
            this.naselje.Name = "naselje";
            // 
            // id_grad
            // 
            this.id_grad.HeaderText = "id_grad";
            this.id_grad.Name = "id_grad";
            this.id_grad.ReadOnly = true;
            this.id_grad.Visible = false;
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNoviUnos.BackgroundImage")));
            this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnNoviUnos.FlatAppearance.BorderSize = 0;
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(499, 45);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(134, 54);
            this.btnNoviUnos.TabIndex = 113;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novi grad";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(7, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 22);
            this.label7.TabIndex = 112;
            this.label7.Text = "Gradovi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 110;
            this.label1.Text = "Županija:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 111;
            this.label4.Text = "Naziv grada:";
            // 
            // txtZupanija
            // 
            this.txtZupanija.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtZupanija.Location = new System.Drawing.Point(93, 70);
            this.txtZupanija.MaxLength = 80;
            this.txtZupanija.Name = "txtZupanija";
            this.txtZupanija.Size = new System.Drawing.Size(155, 22);
            this.txtZupanija.TabIndex = 108;
            // 
            // txtnazivGrada
            // 
            this.txtnazivGrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivGrada.Location = new System.Drawing.Point(93, 45);
            this.txtnazivGrada.MaxLength = 200;
            this.txtnazivGrada.Name = "txtnazivGrada";
            this.txtnazivGrada.Size = new System.Drawing.Size(155, 22);
            this.txtnazivGrada.TabIndex = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label5.Location = new System.Drawing.Point(8, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 119;
            this.label5.Text = "Naselje";
            // 
            // txtNaselje
            // 
            this.txtNaselje.Location = new System.Drawing.Point(93, 95);
            this.txtNaselje.Name = "txtNaselje";
            this.txtNaselje.Size = new System.Drawing.Size(155, 20);
            this.txtNaselje.TabIndex = 120;
            // 
            // frmNoviGrad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 594);
            this.Controls.Add(this.txtNaselje);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDrazava);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPosta);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtZupanija);
            this.Controls.Add(this.txtnazivGrada);
            this.Name = "frmNoviGrad";
            this.Text = "Unos novog grada";
            this.Load += new System.EventHandler(this.frmNoviGrad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDrazava;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPosta;
		private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZupanija;
		private System.Windows.Forms.TextBox txtnazivGrada;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtNaselje;
		private System.Windows.Forms.DataGridViewTextBoxColumn grad;
		private System.Windows.Forms.DataGridViewTextBoxColumn posta;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_zupanija;
		private System.Windows.Forms.DataGridViewComboBoxColumn drzava;
		private System.Windows.Forms.DataGridViewTextBoxColumn naselje;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_grad;
    }
}