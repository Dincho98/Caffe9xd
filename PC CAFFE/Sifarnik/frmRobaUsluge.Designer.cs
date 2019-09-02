namespace PCPOS
{
    partial class frmRobaUsluge
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobaUsluge));
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtnabavna = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVeleprodajna = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMPC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDobavljac = new System.Windows.Forms.Button();
            this.cbProizvodac = new System.Windows.Forms.ComboBox();
            this.cbZemljaPodrijetla = new System.Windows.Forms.ComboBox();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbZemljaUvoza = new System.Windows.Forms.ComboBox();
            this.txtJedMj = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEan = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtPDV = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bcUzmiSaSkladista = new System.Windows.Forms.ComboBox();
            this.txtSifraDob = new System.Windows.Forms.TextBox();
            this.txtNazivDob = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.cbPorezNaPotrosnju = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.SystemColors.Window;
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(152, 134);
            this.txtSifra.MaxLength = 25;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(277, 23);
            this.txtSifra.TabIndex = 1;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(14, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Jedinica mjere:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(13, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Grupa:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Enabled = false;
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(152, 161);
            this.txtNaziv.MaxLength = 99;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(277, 23);
            this.txtNaziv.TabIndex = 2;
            this.txtNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNaziv_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(13, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Naziv robe/usluge:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(13, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Šifra robe/usluge:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(468, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Nabavna cijena:";
            // 
            // txtnabavna
            // 
            this.txtnabavna.Enabled = false;
            this.txtnabavna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtnabavna.Location = new System.Drawing.Point(610, 134);
            this.txtnabavna.Name = "txtnabavna";
            this.txtnabavna.Size = new System.Drawing.Size(238, 23);
            this.txtnabavna.TabIndex = 8;
            this.txtnabavna.Text = "0,00";
            this.txtnabavna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnabavna_KeyDown);
            this.txtnabavna.Leave += new System.EventHandler(this.txtnabavna_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(468, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Veleprodajna cijena:";
            // 
            // txtVeleprodajna
            // 
            this.txtVeleprodajna.Enabled = false;
            this.txtVeleprodajna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtVeleprodajna.Location = new System.Drawing.Point(610, 161);
            this.txtVeleprodajna.Name = "txtVeleprodajna";
            this.txtVeleprodajna.Size = new System.Drawing.Size(238, 23);
            this.txtVeleprodajna.TabIndex = 9;
            this.txtVeleprodajna.Text = "0,00";
            this.txtVeleprodajna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVeleprodajna_KeyDown);
            this.txtVeleprodajna.Leave += new System.EventHandler(this.txtVeleprodajna_Leave_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(468, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Maloprodajna cijena:";
            // 
            // txtMPC
            // 
            this.txtMPC.Enabled = false;
            this.txtMPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMPC.Location = new System.Drawing.Point(610, 244);
            this.txtMPC.Name = "txtMPC";
            this.txtMPC.Size = new System.Drawing.Size(238, 23);
            this.txtMPC.TabIndex = 11;
            this.txtMPC.Text = "0,00";
            this.txtMPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMPC_KeyDown);
            this.txtMPC.Leave += new System.EventHandler(this.txtMPC_Leave_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(468, 276);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Proizvođač:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(468, 303);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Zemlja porijetla:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(468, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "Porez:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(13, 281);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "Dobavljač:";
            // 
            // btnDobavljac
            // 
            this.btnDobavljac.BackColor = System.Drawing.Color.White;
            this.btnDobavljac.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnDobavljac.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnDobavljac.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnDobavljac.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDobavljac.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDobavljac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDobavljac.Location = new System.Drawing.Point(405, 271);
            this.btnDobavljac.Name = "btnDobavljac";
            this.btnDobavljac.Size = new System.Drawing.Size(24, 25);
            this.btnDobavljac.TabIndex = 33;
            this.btnDobavljac.Text = "...";
            this.toolTip1.SetToolTip(this.btnDobavljac, "Odaberi");
            this.btnDobavljac.UseVisualStyleBackColor = false;
            this.btnDobavljac.Click += new System.EventHandler(this.btnDobavljac_Click);
            // 
            // cbProizvodac
            // 
            this.cbProizvodac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbProizvodac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProizvodac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProizvodac.Enabled = false;
            this.cbProizvodac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbProizvodac.FormattingEnabled = true;
            this.cbProizvodac.Location = new System.Drawing.Point(610, 271);
            this.cbProizvodac.Name = "cbProizvodac";
            this.cbProizvodac.Size = new System.Drawing.Size(238, 24);
            this.cbProizvodac.TabIndex = 12;
            this.cbProizvodac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProizvodac_KeyDown);
            // 
            // cbZemljaPodrijetla
            // 
            this.cbZemljaPodrijetla.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZemljaPodrijetla.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZemljaPodrijetla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemljaPodrijetla.Enabled = false;
            this.cbZemljaPodrijetla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZemljaPodrijetla.FormattingEnabled = true;
            this.cbZemljaPodrijetla.Location = new System.Drawing.Point(610, 299);
            this.cbZemljaPodrijetla.Name = "cbZemljaPodrijetla";
            this.cbZemljaPodrijetla.Size = new System.Drawing.Size(238, 24);
            this.cbZemljaPodrijetla.TabIndex = 13;
            this.cbZemljaPodrijetla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbZemljaPodrijetla_KeyDown);
            // 
            // cbGrupa
            // 
            this.cbGrupa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrupa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupa.Enabled = false;
            this.cbGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Location = new System.Drawing.Point(152, 188);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(277, 24);
            this.cbGrupa.TabIndex = 3;
            this.cbGrupa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbGrupa_KeyDown);
            // 
            // cbZemljaUvoza
            // 
            this.cbZemljaUvoza.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZemljaUvoza.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZemljaUvoza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemljaUvoza.Enabled = false;
            this.cbZemljaUvoza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZemljaUvoza.FormattingEnabled = true;
            this.cbZemljaUvoza.Location = new System.Drawing.Point(610, 327);
            this.cbZemljaUvoza.Name = "cbZemljaUvoza";
            this.cbZemljaUvoza.Size = new System.Drawing.Size(238, 24);
            this.cbZemljaUvoza.TabIndex = 14;
            // 
            // txtJedMj
            // 
            this.txtJedMj.Enabled = false;
            this.txtJedMj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtJedMj.Location = new System.Drawing.Point(152, 217);
            this.txtJedMj.MaxLength = 20;
            this.txtJedMj.Name = "txtJedMj";
            this.txtJedMj.Size = new System.Drawing.Size(277, 23);
            this.txtJedMj.TabIndex = 4;
            this.txtJedMj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJedMj_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.Location = new System.Drawing.Point(14, 253);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 8;
            this.label12.Text = "Bar code:";
            // 
            // txtEan
            // 
            this.txtEan.BackColor = System.Drawing.SystemColors.Window;
            this.txtEan.Enabled = false;
            this.txtEan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEan.Location = new System.Drawing.Point(152, 244);
            this.txtEan.MaxLength = 24;
            this.txtEan.Name = "txtEan";
            this.txtEan.Size = new System.Drawing.Size(277, 23);
            this.txtEan.TabIndex = 5;
            this.txtEan.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            this.txtEan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEan_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(922, 22);
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtPDV
            // 
            this.txtPDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPDV.Enabled = false;
            this.txtPDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPDV.FormattingEnabled = true;
            this.txtPDV.Location = new System.Drawing.Point(610, 188);
            this.txtPDV.Name = "txtPDV";
            this.txtPDV.Size = new System.Drawing.Size(238, 24);
            this.txtPDV.TabIndex = 10;
            this.txtPDV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPDV_KeyDown);
            this.txtPDV.Leave += new System.EventHandler(this.txtPDV_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label13.Location = new System.Drawing.Point(468, 330);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "Zemlja uvoza:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label14.Location = new System.Drawing.Point(14, 308);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 17);
            this.label14.TabIndex = 12;
            this.label14.Text = "Uzimaj sa skladišta:";
            // 
            // bcUzmiSaSkladista
            // 
            this.bcUzmiSaSkladista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bcUzmiSaSkladista.Enabled = false;
            this.bcUzmiSaSkladista.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bcUzmiSaSkladista.FormattingEnabled = true;
            this.bcUzmiSaSkladista.Location = new System.Drawing.Point(152, 300);
            this.bcUzmiSaSkladista.Name = "bcUzmiSaSkladista";
            this.bcUzmiSaSkladista.Size = new System.Drawing.Size(277, 24);
            this.bcUzmiSaSkladista.TabIndex = 7;
            this.bcUzmiSaSkladista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bcUzmiSaSkladista_KeyDown);
            // 
            // txtSifraDob
            // 
            this.txtSifraDob.BackColor = System.Drawing.SystemColors.Window;
            this.txtSifraDob.Enabled = false;
            this.txtSifraDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraDob.Location = new System.Drawing.Point(152, 272);
            this.txtSifraDob.MaxLength = 24;
            this.txtSifraDob.Name = "txtSifraDob";
            this.txtSifraDob.Size = new System.Drawing.Size(68, 23);
            this.txtSifraDob.TabIndex = 6;
            this.txtSifraDob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbDobavljac_KeyDown);
            // 
            // txtNazivDob
            // 
            this.txtNazivDob.BackColor = System.Drawing.SystemColors.Window;
            this.txtNazivDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNazivDob.Location = new System.Drawing.Point(221, 272);
            this.txtNazivDob.MaxLength = 24;
            this.txtNazivDob.Name = "txtNazivDob";
            this.txtNazivDob.ReadOnly = true;
            this.txtNazivDob.Size = new System.Drawing.Size(184, 23);
            this.txtNazivDob.TabIndex = 39;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(780, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 83;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.BackColor = System.Drawing.Color.White;
            this.btnSveFakture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSveFakture.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSveFakture.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSveFakture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(422, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(154, 40);
            this.btnSveFakture.TabIndex = 81;
            this.btnSveFakture.Text = "Svi artikli i usluge";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = false;
            this.btnSveFakture.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.White;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(8, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 80;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(146, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 79;
            this.btnOdustani.Text = "Odustani   ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 78;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label15.Location = new System.Drawing.Point(468, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 17);
            this.label15.TabIndex = 12;
            this.label15.Text = "Porez na potrošnju";
            // 
            // cbPorezNaPotrosnju
            // 
            this.cbPorezNaPotrosnju.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPorezNaPotrosnju.Enabled = false;
            this.cbPorezNaPotrosnju.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbPorezNaPotrosnju.FormattingEnabled = true;
            this.cbPorezNaPotrosnju.Location = new System.Drawing.Point(610, 216);
            this.cbPorezNaPotrosnju.Name = "cbPorezNaPotrosnju";
            this.cbPorezNaPotrosnju.Size = new System.Drawing.Size(238, 24);
            this.cbPorezNaPotrosnju.TabIndex = 10;
            this.cbPorezNaPotrosnju.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPorezNaPotrosnju_keyDown);
            this.cbPorezNaPotrosnju.Leave += new System.EventHandler(this.cbPorezNaPotrosnju_Leave);
            // 
            // frmRobaUsluge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(922, 442);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.txtNazivDob);
            this.Controls.Add(this.txtSifraDob);
            this.Controls.Add(this.bcUzmiSaSkladista);
            this.Controls.Add(this.cbPorezNaPotrosnju);
            this.Controls.Add(this.txtPDV);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbZemljaUvoza);
            this.Controls.Add(this.cbGrupa);
            this.Controls.Add(this.cbZemljaPodrijetla);
            this.Controls.Add(this.cbProizvodac);
            this.Controls.Add(this.btnDobavljac);
            this.Controls.Add(this.txtEan);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMPC);
            this.Controls.Add(this.txtVeleprodajna);
            this.Controls.Add(this.txtJedMj);
            this.Controls.Add(this.txtnabavna);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmRobaUsluge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roba i usluge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRobaUsluge_FormClosing);
            this.Load += new System.EventHandler(this.frmRobaUsluge_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtnabavna;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVeleprodajna;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMPC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDobavljac;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbProizvodac;
        private System.Windows.Forms.ComboBox cbZemljaPodrijetla;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.ComboBox cbZemljaUvoza;
        private System.Windows.Forms.TextBox txtJedMj;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox txtPDV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox bcUzmiSaSkladista;
        private System.Windows.Forms.TextBox txtNazivDob;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbPorezNaPotrosnju;
        private System.Windows.Forms.TextBox txtSifraDob;
    }
}