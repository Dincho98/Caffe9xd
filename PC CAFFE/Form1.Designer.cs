namespace PCPOS
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRobaProdaja = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prezime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNBCuProdaju = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Grad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(25, 171);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(610, 63);
            this.dgv.TabIndex = 1;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(436, 22);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(199, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "država";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(223, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "partneri";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(223, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(192, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "roba";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(223, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(192, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Dodaj ean";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRobaProdaja
            // 
            this.btnRobaProdaja.Location = new System.Drawing.Point(25, 80);
            this.btnRobaProdaja.Name = "btnRobaProdaja";
            this.btnRobaProdaja.Size = new System.Drawing.Size(192, 23);
            this.btnRobaProdaja.TabIndex = 4;
            this.btnRobaProdaja.Text = "Roba Prodaja";
            this.btnRobaProdaja.UseVisualStyleBackColor = true;
            this.btnRobaProdaja.Click += new System.EventHandler(this.btnRobaProdaja_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(25, 109);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(192, 23);
            this.button6.TabIndex = 4;
            this.button6.Text = "Briše duplikate u robi";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ime,
            this.prezime,
            this.tel});
            this.dataGridView1.Location = new System.Drawing.Point(25, 259);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(610, 121);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ime
            // 
            this.ime.HeaderText = "ime";
            this.ime.Name = "ime";
            // 
            // prezime
            // 
            this.prezime.HeaderText = "prezime";
            this.prezime.Name = "prezime";
            // 
            // tel
            // 
            this.tel.HeaderText = "tel";
            this.tel.Name = "tel";
            // 
            // btnNBCuProdaju
            // 
            this.btnNBCuProdaju.Location = new System.Drawing.Point(223, 109);
            this.btnNBCuProdaju.Name = "btnNBCuProdaju";
            this.btnNBCuProdaju.Size = new System.Drawing.Size(192, 23);
            this.btnNBCuProdaju.TabIndex = 4;
            this.btnNBCuProdaju.Text = "Dodaj NBC u prodaju";
            this.btnNBCuProdaju.UseVisualStyleBackColor = true;
            this.btnNBCuProdaju.Click += new System.EventHandler(this.btnNBCuProdaju_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(25, 142);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(192, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "roba CAFFE";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(223, 142);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(192, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "Roba ProdajaCaffe";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 413);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnRobaProdaja);
            this.Controls.Add(this.btnNBCuProdaju);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnRobaProdaja;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime;
        private System.Windows.Forms.DataGridViewTextBoxColumn prezime;
        private System.Windows.Forms.DataGridViewTextBoxColumn tel;
        private System.Windows.Forms.Button btnNBCuProdaju;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}