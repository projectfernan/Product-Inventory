namespace prjInventory
{
    partial class frmExportSmTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportSmTemp));
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFirstField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVendorCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeptCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrandCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStockStyle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSourcemarked = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFourteenField = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFifteenField = new System.Windows.Forms.TextBox();
            this.dtCreated = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtSaved = new System.Windows.Forms.DateTimePicker();
            this.btnExpSMTemp = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtExcelFname = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSubDeptCode = new System.Windows.Forms.TextBox();
            this.cboBrand = new System.Windows.Forms.ComboBox();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.lblClose);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(471, 37);
            this.panel9.TabIndex = 902;
            // 
            // lblClose
            // 
            this.lblClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.Black;
            this.lblClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClose.Location = new System.Drawing.Point(452, 8);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(16, 18);
            this.lblClose.TabIndex = 657;
            this.lblClose.Text = "x";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(4, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(186, 18);
            this.label11.TabIndex = 656;
            this.label11.Text = "     Export SM Template";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(129, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 916;
            this.label12.Text = "First Field :";
            // 
            // txtFirstField
            // 
            this.txtFirstField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstField.Location = new System.Drawing.Point(215, 87);
            this.txtFirstField.Name = "txtFirstField";
            this.txtFirstField.Size = new System.Drawing.Size(209, 24);
            this.txtFirstField.TabIndex = 915;
            this.txtFirstField.Text = "0";
            this.txtFirstField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFirstField_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(106, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 918;
            this.label1.Text = "Vendor Code :";
            // 
            // txtVendorCode
            // 
            this.txtVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVendorCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendorCode.Location = new System.Drawing.Point(215, 128);
            this.txtVendorCode.Name = "txtVendorCode";
            this.txtVendorCode.Size = new System.Drawing.Size(209, 24);
            this.txtVendorCode.TabIndex = 1;
            this.txtVendorCode.Text = "14334";
            this.txtVendorCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorCode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(76, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 18);
            this.label2.TabIndex = 920;
            this.label2.Text = "Department Code :";
            // 
            // txtDeptCode
            // 
            this.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeptCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeptCode.Location = new System.Drawing.Point(215, 169);
            this.txtDeptCode.Name = "txtDeptCode";
            this.txtDeptCode.Size = new System.Drawing.Size(209, 24);
            this.txtDeptCode.TabIndex = 2;
            this.txtDeptCode.Text = "2";
            this.txtDeptCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeptCode_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(114, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 18);
            this.label3.TabIndex = 922;
            this.label3.Text = "Brand Code :";
            // 
            // txtBrandCode
            // 
            this.txtBrandCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrandCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBrandCode.Location = new System.Drawing.Point(215, 261);
            this.txtBrandCode.Name = "txtBrandCode";
            this.txtBrandCode.ReadOnly = true;
            this.txtBrandCode.Size = new System.Drawing.Size(46, 24);
            this.txtBrandCode.TabIndex = 2535;
            this.txtBrandCode.TabStop = false;
            this.txtBrandCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrandCode_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(78, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 18);
            this.label5.TabIndex = 926;
            this.label5.Text = "Stock/Style Code :";
            // 
            // txtStockStyle
            // 
            this.txtStockStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStockStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockStyle.Location = new System.Drawing.Point(215, 307);
            this.txtStockStyle.Name = "txtStockStyle";
            this.txtStockStyle.Size = new System.Drawing.Size(209, 24);
            this.txtStockStyle.TabIndex = 6;
            this.txtStockStyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockStyle_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(35, 354);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 18);
            this.label6.TabIndex = 928;
            this.label6.Text = "Sourcemarked Barcode :";
            // 
            // txtSourcemarked
            // 
            this.txtSourcemarked.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourcemarked.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourcemarked.Location = new System.Drawing.Point(215, 354);
            this.txtSourcemarked.Name = "txtSourcemarked";
            this.txtSourcemarked.Size = new System.Drawing.Size(209, 24);
            this.txtSourcemarked.TabIndex = 7;
            this.txtSourcemarked.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSourcemarked_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(99, 403);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 18);
            this.label7.TabIndex = 930;
            this.label7.Text = "Fourteen Field :";
            // 
            // txtFourteenField
            // 
            this.txtFourteenField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFourteenField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFourteenField.Location = new System.Drawing.Point(215, 401);
            this.txtFourteenField.Name = "txtFourteenField";
            this.txtFourteenField.Size = new System.Drawing.Size(209, 24);
            this.txtFourteenField.TabIndex = 8;
            this.txtFourteenField.Text = "27";
            this.txtFourteenField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFourteenField_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(114, 445);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 18);
            this.label8.TabIndex = 932;
            this.label8.Text = "Fifteen Field :";
            // 
            // txtFifteenField
            // 
            this.txtFifteenField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFifteenField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFifteenField.Location = new System.Drawing.Point(215, 443);
            this.txtFifteenField.Name = "txtFifteenField";
            this.txtFifteenField.Size = new System.Drawing.Size(209, 24);
            this.txtFifteenField.TabIndex = 9;
            this.txtFifteenField.Text = "B";
            this.txtFifteenField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFifteenField_KeyDown);
            // 
            // dtCreated
            // 
            this.dtCreated.CustomFormat = "dd MM yyyy";
            this.dtCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCreated.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreated.Location = new System.Drawing.Point(215, 487);
            this.dtCreated.Name = "dtCreated";
            this.dtCreated.Size = new System.Drawing.Size(209, 24);
            this.dtCreated.TabIndex = 10;
            this.dtCreated.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtCreated_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(106, 488);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 18);
            this.label9.TabIndex = 934;
            this.label9.Text = "Date Created :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(117, 531);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 18);
            this.label10.TabIndex = 936;
            this.label10.Text = "Date Saved :";
            // 
            // dtSaved
            // 
            this.dtSaved.CustomFormat = "dd MM yyyy";
            this.dtSaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtSaved.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSaved.Location = new System.Drawing.Point(215, 530);
            this.dtSaved.Name = "dtSaved";
            this.dtSaved.Size = new System.Drawing.Size(209, 24);
            this.dtSaved.TabIndex = 11;
            this.dtSaved.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtSaved_KeyDown);
            // 
            // btnExpSMTemp
            // 
            this.btnExpSMTemp.BackColor = System.Drawing.Color.White;
            this.btnExpSMTemp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpSMTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpSMTemp.Image = ((System.Drawing.Image)(resources.GetObject("btnExpSMTemp.Image")));
            this.btnExpSMTemp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpSMTemp.Location = new System.Drawing.Point(329, 619);
            this.btnExpSMTemp.Name = "btnExpSMTemp";
            this.btnExpSMTemp.Size = new System.Drawing.Size(95, 36);
            this.btnExpSMTemp.TabIndex = 12;
            this.btnExpSMTemp.Text = "Export";
            this.btnExpSMTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExpSMTemp.UseVisualStyleBackColor = false;
            this.btnExpSMTemp.Click += new System.EventHandler(this.btnExpSMTemp_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(86, 576);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 18);
            this.label13.TabIndex = 938;
            this.label13.Text = "Excel File Name :";
            // 
            // txtExcelFname
            // 
            this.txtExcelFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcelFname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFname.Location = new System.Drawing.Point(215, 575);
            this.txtExcelFname.Name = "txtExcelFname";
            this.txtExcelFname.Size = new System.Drawing.Size(209, 24);
            this.txtExcelFname.TabIndex = 12;
            this.txtExcelFname.Text = "CONS_014334";
            this.txtExcelFname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExcelFname_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(86, 217);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 18);
            this.label14.TabIndex = 940;
            this.label14.Text = "Sub Dept. Code :";
            // 
            // txtSubDeptCode
            // 
            this.txtSubDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubDeptCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubDeptCode.Location = new System.Drawing.Point(215, 215);
            this.txtSubDeptCode.Name = "txtSubDeptCode";
            this.txtSubDeptCode.Size = new System.Drawing.Size(209, 24);
            this.txtSubDeptCode.TabIndex = 3;
            this.txtSubDeptCode.Text = "65";
            this.txtSubDeptCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubDeptCode_KeyDown);
            // 
            // cboBrand
            // 
            this.cboBrand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBrand.FormattingEnabled = true;
            this.cboBrand.Location = new System.Drawing.Point(270, 261);
            this.cboBrand.Name = "cboBrand";
            this.cboBrand.Size = new System.Drawing.Size(154, 24);
            this.cboBrand.TabIndex = 4;
            this.cboBrand.SelectedValueChanged += new System.EventHandler(this.cboBrand_SelectedValueChanged);
            this.cboBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboBrand_KeyDown);
            // 
            // frmExportSmTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(471, 705);
            this.ControlBox = false;
            this.Controls.Add(this.cboBrand);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtSubDeptCode);
            this.Controls.Add(this.txtExcelFname);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnExpSMTemp);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtSaved);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtCreated);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFifteenField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFourteenField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSourcemarked);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStockStyle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBrandCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeptCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVendorCode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFirstField);
            this.Controls.Add(this.panel9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExportSmTemp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmExportSmTemp_Load);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFirstField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVendorCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeptCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrandCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStockStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSourcemarked;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFourteenField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFifteenField;
        private System.Windows.Forms.DateTimePicker dtCreated;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtSaved;
        private System.Windows.Forms.Button btnExpSMTemp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtExcelFname;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSubDeptCode;
        private System.Windows.Forms.ComboBox cboBrand;
    }
}