namespace prjInventory
{
    partial class frmExpSmVPO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpSmVPO));
            this.label10 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtExcelFname = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnExpSMTemp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSCPOA = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.txtBoxes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dtExpectPOut = new System.Windows.Forms.DateTimePicker();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxes)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(25, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 18);
            this.label10.TabIndex = 960;
            this.label10.Text = "Expected Pull Out :";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.lblClose);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(409, 37);
            this.panel9.TabIndex = 956;
            // 
            // lblClose
            // 
            this.lblClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.Black;
            this.lblClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClose.Location = new System.Drawing.Point(390, 8);
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
            this.label11.Text = "     Export SM VPO Template";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExcelFname
            // 
            this.txtExcelFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcelFname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFname.Location = new System.Drawing.Point(164, 260);
            this.txtExcelFname.Name = "txtExcelFname";
            this.txtExcelFname.Size = new System.Drawing.Size(209, 24);
            this.txtExcelFname.TabIndex = 5;
            this.txtExcelFname.Text = "CSGPLW_014334";
            this.txtExcelFname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExcelFname_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(35, 261);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 18);
            this.label13.TabIndex = 959;
            this.label13.Text = "Excel File Name :";
            // 
            // btnExpSMTemp
            // 
            this.btnExpSMTemp.BackColor = System.Drawing.Color.White;
            this.btnExpSMTemp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpSMTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpSMTemp.Image = ((System.Drawing.Image)(resources.GetObject("btnExpSMTemp.Image")));
            this.btnExpSMTemp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpSMTemp.Location = new System.Drawing.Point(277, 300);
            this.btnExpSMTemp.Name = "btnExpSMTemp";
            this.btnExpSMTemp.Size = new System.Drawing.Size(95, 36);
            this.btnExpSMTemp.TabIndex = 6;
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
            this.label1.Location = new System.Drawing.Point(61, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 958;
            this.label1.Text = "SCPOA No. :";
            // 
            // txtSCPOA
            // 
            this.txtSCPOA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSCPOA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSCPOA.Location = new System.Drawing.Point(164, 129);
            this.txtSCPOA.Name = "txtSCPOA";
            this.txtSCPOA.Size = new System.Drawing.Size(209, 24);
            this.txtSCPOA.TabIndex = 1;
            this.txtSCPOA.Text = "0143340030015";
            this.txtSCPOA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSCPOA_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(54, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 18);
            this.label12.TabIndex = 957;
            this.label12.Text = "Vendor Code :";
            // 
            // txtVendor
            // 
            this.txtVendor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendor.Location = new System.Drawing.Point(164, 88);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(209, 24);
            this.txtVendor.TabIndex = 0;
            this.txtVendor.Text = "14334";
            this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
            // 
            // txtBoxes
            // 
            this.txtBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxes.Location = new System.Drawing.Point(164, 217);
            this.txtBoxes.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtBoxes.Name = "txtBoxes";
            this.txtBoxes.Size = new System.Drawing.Size(209, 24);
            this.txtBoxes.TabIndex = 4;
            this.txtBoxes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxes_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(100, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 964;
            this.label3.Text = "Boxes :";
            // 
            // dtExpectPOut
            // 
            this.dtExpectPOut.CustomFormat = "yyyy-MM-dd";
            this.dtExpectPOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtExpectPOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpectPOut.Location = new System.Drawing.Point(164, 172);
            this.dtExpectPOut.Name = "dtExpectPOut";
            this.dtExpectPOut.Size = new System.Drawing.Size(209, 24);
            this.dtExpectPOut.TabIndex = 3;
            // 
            // progBar
            // 
            this.progBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progBar.ForeColor = System.Drawing.Color.Lime;
            this.progBar.Location = new System.Drawing.Point(0, 374);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(409, 28);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progBar.TabIndex = 2538;
            // 
            // frmExpSmVPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(409, 402);
            this.ControlBox = false;
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.dtExpectPOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.txtExcelFname);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnExpSMTemp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSCPOA);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtVendor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExpSmVPO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmExpSmVPO_Load);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtExcelFname;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnExpSMTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSCPOA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtVendor;
        private System.Windows.Forms.NumericUpDown txtBoxes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtExpectPOut;
        private System.Windows.Forms.ProgressBar progBar;
    }
}