namespace prjInventory
{
    partial class frmExpSmVDR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpSmVDR));
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtExcelFname = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnExpSMTemp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStoreCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDRno = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtExpectDelivery = new System.Windows.Forms.DateTimePicker();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this.txtDeliBatch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
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
            this.panel9.Size = new System.Drawing.Size(439, 37);
            this.panel9.TabIndex = 945;
            // 
            // lblClose
            // 
            this.lblClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.Black;
            this.lblClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClose.Location = new System.Drawing.Point(420, 8);
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
            this.label11.Size = new System.Drawing.Size(225, 18);
            this.label11.TabIndex = 656;
            this.label11.Text = "     Export SM VDR Template";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExcelFname
            // 
            this.txtExcelFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcelFname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFname.Location = new System.Drawing.Point(194, 256);
            this.txtExcelFname.Name = "txtExcelFname";
            this.txtExcelFname.Size = new System.Drawing.Size(209, 24);
            this.txtExcelFname.TabIndex = 3;
            this.txtExcelFname.Text = "CSGRCW_014334";
            this.txtExcelFname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExcelFname_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(66, 257);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 18);
            this.label13.TabIndex = 948;
            this.label13.Text = "Excel File Name :";
            // 
            // btnExpSMTemp
            // 
            this.btnExpSMTemp.BackColor = System.Drawing.Color.White;
            this.btnExpSMTemp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpSMTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpSMTemp.Image = ((System.Drawing.Image)(resources.GetObject("btnExpSMTemp.Image")));
            this.btnExpSMTemp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpSMTemp.Location = new System.Drawing.Point(308, 299);
            this.btnExpSMTemp.Name = "btnExpSMTemp";
            this.btnExpSMTemp.Size = new System.Drawing.Size(95, 36);
            this.btnExpSMTemp.TabIndex = 4;
            this.btnExpSMTemp.Text = "Export";
            this.btnExpSMTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExpSMTemp.UseVisualStyleBackColor = false;
            this.btnExpSMTemp.Click += new System.EventHandler(this.btnExpSMTemp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(95, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 947;
            this.label1.Text = "Store Code :";
            // 
            // txtStoreCode
            // 
            this.txtStoreCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStoreCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStoreCode.Location = new System.Drawing.Point(194, 129);
            this.txtStoreCode.Name = "txtStoreCode";
            this.txtStoreCode.ReadOnly = true;
            this.txtStoreCode.Size = new System.Drawing.Size(46, 24);
            this.txtStoreCode.TabIndex = 1;
            this.txtStoreCode.TabStop = false;
            this.txtStoreCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStoreCode_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(125, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 18);
            this.label12.TabIndex = 946;
            this.label12.Text = "DR No :";
            // 
            // txtDRno
            // 
            this.txtDRno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDRno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDRno.Location = new System.Drawing.Point(194, 88);
            this.txtDRno.Name = "txtDRno";
            this.txtDRno.Size = new System.Drawing.Size(209, 24);
            this.txtDRno.TabIndex = 0;
            this.txtDRno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDRno_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(55, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 18);
            this.label10.TabIndex = 950;
            this.label10.Text = "Expected Delivery :";
            // 
            // dtExpectDelivery
            // 
            this.dtExpectDelivery.CustomFormat = "yyyy-MM-dd";
            this.dtExpectDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtExpectDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpectDelivery.Location = new System.Drawing.Point(194, 171);
            this.dtExpectDelivery.Name = "dtExpectDelivery";
            this.dtExpectDelivery.Size = new System.Drawing.Size(208, 24);
            this.dtExpectDelivery.TabIndex = 2;
            this.dtExpectDelivery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtExpectDelivery_KeyDown);
            // 
            // cboStore
            // 
            this.cboStore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(249, 129);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(154, 24);
            this.cboStore.TabIndex = 1;
            this.cboStore.SelectedValueChanged += new System.EventHandler(this.cboStore_SelectedValueChanged);
            this.cboStore.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStore_KeyDown);
            // 
            // txtDeliBatch
            // 
            this.txtDeliBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeliBatch.Location = new System.Drawing.Point(194, 213);
            this.txtDeliBatch.Name = "txtDeliBatch";
            this.txtDeliBatch.Size = new System.Drawing.Size(209, 24);
            this.txtDeliBatch.TabIndex = 951;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 18);
            this.label2.TabIndex = 952;
            this.label2.Text = "Delivery Batch Name :";
            // 
            // progBar
            // 
            this.progBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progBar.ForeColor = System.Drawing.Color.Lime;
            this.progBar.Location = new System.Drawing.Point(0, 368);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(439, 28);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progBar.TabIndex = 2537;
            // 
            // frmExpSmVDR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(439, 396);
            this.ControlBox = false;
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.txtDeliBatch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboStore);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtExpectDelivery);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.txtExcelFname);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnExpSMTemp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStoreCode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDRno);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExpSmVDR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmExpSmVDR_Load);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtExcelFname;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnExpSMTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStoreCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDRno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtExpectDelivery;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.TextBox txtDeliBatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progBar;

    }
}