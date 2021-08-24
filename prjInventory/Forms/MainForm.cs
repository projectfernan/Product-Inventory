using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjInventory
{
    public partial class MainForm : Form
    {
        MySQL_Conn mySqlTest = new MySQL_Conn();

        SUS_Upload upl = new SUS_Upload();
        SystemAcc sa = new SystemAcc();
        SKU_Upload sku = new SKU_Upload();
        Barcoding bcode = new Barcoding();
        SM_VDR_Temp smVdr = new SM_VDR_Temp();
        SM_VPO_Temp smVpo = new SM_VPO_Temp();
        BrandCode brCode = new BrandCode();
        StoreCode strCode = new StoreCode();
        ProductDelivery delv = new ProductDelivery();
        ProductPullOut po = new ProductPullOut();

        bool POfresh;

        public MainForm()
        {
            InitializeComponent();
            upl.UpdateProgress += UpdateProgress;
            upl.UpdProgBaxMax += UpdProgBarMax;

            bcode.UpdateProgress += UpdateProgBarcode;
            bcode.UpdProgBaxMax += UpdProgBarMaxBcode;
        }

        void UpdateProgress(int ProgressPercentage)
        {
            if (ProgressPercentage < 0)
            {
                return;
            }
            progBar.Value = ProgressPercentage;
        }

        void UpdateProgBarcode(int ProgressPercentage)
        {
            if (ProgressPercentage < 0)
            {
                return;
            }
            progBarUpLXLInv.Value = ProgressPercentage;
        }

        void UpdProgBarMax(int MaxVal, int MinVal)
        {
            progBar.Maximum = MaxVal;
            progBar.Minimum = MinVal;
        }

        void UpdProgBarMaxBcode(int MaxVal, int MinVal)
        {
            progBarUpLXLInv.Maximum = MaxVal;
            progBarUpLXLInv.Minimum = MinVal;
        }

        void SKUmenu()
        {
            tabControl.SelectedIndex = 1;
        }

        void BarcodeMenu() 
        {
            tabControl.SelectedIndex = 9;
        }

        void CreateVdr() 
        {
            cboVdrBcodeBatch.Items.Clear();
            tabControl.SelectedIndex = 13;
        }

        void CreateVpo()
        {
            //cboVpoBcodeBatch.Items.Clear();
            //tabControl.SelectedIndex = 15;
            txtPullOutBatch.Text = "";
            tabControl.SelectedIndex = 20;
        }

        void SystemLock()
        {
            btnPdt.Enabled = false;
            btnSKUAppli.Enabled = false;
            btnBarcoding.Enabled = false;
            btnDelivery.Enabled = false;
            btnPullOut.Enabled = false;
            btnSettings.Enabled = false;
            btnTerminate.Enabled = false;
            btnAbout.Enabled = false;

            tabControl.SelectedIndex = 0;
            tabControl.Enabled = false;

            lblDesig.Text = "System :";
            lblSysAcc.Text = "Lock";
        }

        void SystemUnlock(string Desig,string Uid)
        {
            btnPdt.Enabled = true;
            btnSKUAppli.Enabled = true;
            btnBarcoding.Enabled = true;
            btnDelivery.Enabled = true;
            btnPullOut.Enabled = true;

            if (Desig == "Operator")
            {
                btnSettings.Enabled = false;
            }
            else 
            {
                btnSettings.Enabled = true;
            }
            btnAbout.Enabled = true;
            btnTerminate.Enabled = true;

            tabControl.Enabled = true;

            lblDesig.Text = Desig + " :";
            lblSysAcc.Text = Uid;

            sa.LoadAccList(cboPdtUpdBy);
            upl.LoadPdtWbList(cboPdtWb, lblSysAcc.Text);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            sa.LoadAccList(cboPdtUpdBy);

            tabControl.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (mySqlTest.ConnMySQL())
            {
                lblDbStatus.Text = "Connected";
                lblDbStatus.ForeColor = Color.Blue;
            }
            else 
            {
                lblDbStatus.Text = "Disconnected";
                lblDbStatus.ForeColor = Color.Red;
            }

            lblMasterPdtCnt.Text = upl.LoadMasterPdt(dgvPdtML, cboPdtWb.Text, cboPdtUpdBy.Text, cboPdtCateg.Text, "-").ToString();
            loadImgPathSettings();

            sa.LoadAccList(cboPdtUpdBy);

            SystemLock();
        }

        private void btnSKUAppli_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void btnCreateSKU_Click(object sender, EventArgs e)
        {
            string wbPending = upl.checkWbpending(lblSysAcc.Text);

            if (wbPending != "") 
            {
                lblWBName.Text = wbPending;
                lblSusProdCnt.Text = upl.LoadtblProducts(dgvTblProd, lblWBName.Text, lblSysAcc.Text, "Barcode", "").ToString();
                btnUPCS.Enabled = false;

                tabControl.SelectedIndex = 3;
                return;
            }

            tabControl.SelectedIndex = 2;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogBoxes db = new DialogBoxes();

            txtExcelPath.Text = db.BrowseExcel();
            txtExcelPath.Focus();

            cboSUSshList.Items.Clear();
            upl.GetSheetName(txtExcelPath.Text, cboSUSshList);
        }

       

        private void btnUplSUS_Click(object sender, EventArgs e)
        {
            if (txtExcelPath.Text == "") 
            {
                return;
            }

            DialogResult s = MessageBox.Show("Are you sure do you want to upload this excel file?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (s != DialogResult.Yes)
            {
                return;
            }

            if (cboSUSshList.Text == "") 
            {
                MessageBox.Show("Please select excel sheet!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSUSshList.Focus();
            }

            if (txtWBname.Text == "") 
            {
                MessageBox.Show("Please fill up the Work Batch Name!","Upload",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtWBname.Focus();
                return;
            }

            if (upl.checkWbName(txtWBname.Text,lblSysAcc.Text)) 
            {
                MessageBox.Show("Work Batch Name already exist!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtWBname.Focus();
                return;
            }

            if(upl.UploadSUS(txtExcelPath.Text, cboSUSshList.Text,txtWBname.Text,lblSysAcc.Text))
            {
                MessageBox.Show("Successfully uploaded!","Upload",MessageBoxButtons.OK,MessageBoxIcon.Information);

                lblSusProdCnt.Text =  upl.LoadtblProducts(dgvTblProd,txtWBname.Text, lblSysAcc.Text,"Barcode","").ToString();
                lblWBName.Text = txtWBname.Text;
                btnUPCS.Enabled = false;
                btnAddItem.Enabled = true;
                btnRemove.Enabled = true;
                btnExpSMTemp.Enabled = true;
                lblSKUlabelwb.Text = "     Create SKU Application";

                txtExcelPath.Text = "";
                txtWBname.Text = "";
                cboSUSshList.Text = "";
                progBar.Value = 0;

                tabControl.SelectedIndex = 3;
            }
        }

        private void rbtExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtExcel.Checked == true) 
            {
                pnManual.Visible = false;
            }
        }

        private void rbtManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtManual.Checked == true)
            {
                pnManual.Visible = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtWBmanual.Text == "")
            {
                MessageBox.Show("Please fill up the Work Batch Name!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtWBmanual.Focus();
                return;
            }

            lblWBName.Text = txtWBmanual.Text;

            lblSusProdCnt.Text = upl.LoadtblProducts(dgvTblProd, txtWBmanual.Text, lblSysAcc.Text, "Barcode", "").ToString();
            btnUPCS.Enabled = false;
            btnAddItem.Enabled = true;
            btnRemove.Enabled = true;
            btnExpSMTemp.Enabled = true;
            txtWBmanual.Text = "";

            lblSKUlabelwb.Text = "     Create SKU Application";

            tabControl.SelectedIndex = 3;
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure do you want terminate the software?", "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveDb();
            MessageBox.Show("Database settings successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboDbName_Click(object sender, EventArgs e)
        {
            saveDb();

            cboDbName.Items.Clear();
            mySqlTest.LoadBbList(cboDbName);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            saveDb();

            if (mySqlTest.ConnMySQL())
            {
                MessageBox.Show("Connection successful!", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDbStatus.Text = "Connected";
                lblDbStatus.ForeColor = Color.Blue;
            }
            else 
            {
                MessageBox.Show("Connection failed!", "Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblDbStatus.Text = "Disconnected";
                lblDbStatus.ForeColor = Color.Red;
            }
        }

        private void btnSettings_Click_1(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        void saveDb() 
        {
            Properties.Settings.Default.dbHost1 = txtDbHost.Text;
            Properties.Settings.Default.dbUid1 = txtDbUid.Text;
            Properties.Settings.Default.dbPwd1 = txtDbPwd.Text;
            Properties.Settings.Default.dbPort1 = txtDbPort.Value.ToString();
            Properties.Settings.Default.dbName1 = cboDbName.Text;

            Properties.Settings.Default.Save();
        }

        void ViewDb() 
        {
            txtDbHost.Text = Properties.Settings.Default.dbHost1;
            txtDbUid.Text = Properties.Settings.Default.dbUid1;
            txtDbPwd.Text = Properties.Settings.Default.dbPwd1;
            txtDbPort.Value = Convert.ToInt32(Properties.Settings.Default.dbPort1);
            cboDbName.Text = Properties.Settings.Default.dbName1;
        }

        private void btnDbSettings_Click(object sender, EventArgs e)
        {
            ViewDb();

            saveDb();
            cboDbName.Items.Clear();
            mySqlTest.LoadBbList(cboDbName);

            tabControl.SelectedIndex = 5;
        }

        private void cboDbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void btnSysAccSettings_Click(object sender, EventArgs e)
        {
            btnAccSearch_Click(sender, e);
            AddClearAccInfo();
            txtAccUid.Focus();
            gpAccInfo.Enabled = false;

            tabControl.SelectedIndex = 6;
        }

        private void btnBack6_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            switch (btnAddAcc.Text)
            {
                case "Add":
                    if (btnEditAcc.Text == "Update") return;

                    AddClearAccInfo();
                    txtAccUid.Enabled = true;
                    gpAccInfo.Enabled = true;
                    txtAccUid.Focus();

                    btnAddAcc.Text = "Save";
                break;

                case "Save":

                if (txtAccUid.Text == "" || txtAccPwd.Text == "" || txtAccConfirm.Text == "" || cboAccDesig.Text == "" || txtAccFName.Text == "")
                {
                    MessageBox.Show("Please don't leave a blank!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtAccPwd.Text != txtAccConfirm.Text)
                {
                    MessageBox.Show("Password mismatch! Please type again.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccPwd.Focus();

                    return;
                }

                DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (d != DialogResult.Yes)
                {
                    return;
                }

                if (sa.CheckUid(txtAccUid.Text)) 
                {
                    MessageBox.Show("Username already exist!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccUid.Focus();
                    return;
                }

                if (sa.SaveAcc(txtAccUid.Text, txtAccPwd.Text, txtAccFName.Text, cboAccDesig.Text))
                {
                    MessageBox.Show("System Account successfully created!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnAccSearch_Click(sender, e);

                    AddClearAccInfo();
                    gpAccInfo.Enabled = false;

                    btnAddAcc.Text = "Add";
                }
                else 
                {
                    MessageBox.Show("Failed to create system account! Please try again later.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                break;
            }
        }

        void AddClearAccInfo() 
        {
            txtAccUid.Text = "";
            txtAccPwd.Text = "";
            txtAccConfirm.Text = "";
            txtAccFName.Text = "";
            cboAccDesig.Text = "";

            btnAddAcc.Text = "Add";
            btnEditAcc.Text = "Edit";
        }

        private void btnAccSearch_Click(object sender, EventArgs e)
        {
            lblAccRecount.Text = sa.LoadtblSystemAcc(dgvSysAcc, cboAccCateg.Text, txtAccKeyword.Text).ToString();
        }

        private void txtAccKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnAccSearch_Click(sender, e);
            }
        }

        private void dgvSysAcc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvSysAcc.SelectedRows)
                {
                    txtAccUid.Text = row.Cells[0].Value.ToString();
                    txtAccFName.Text = row.Cells[1].Value.ToString();
                    cboAccDesig.Text = row.Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditAcc_Click(object sender, EventArgs e)
        {
            switch (btnEditAcc.Text)
            { 
                case "Edit":
                    if (btnAddAcc.Text == "Save") return;

                    gpAccInfo.Enabled = true;
                    txtAccUid.Enabled = false;
                    txtAccPwd.Focus();

                    btnEditAcc.Text = "Update";

                    break;
                case "Update":
                    if (txtAccUid.Text == "" || txtAccPwd.Text == "" || txtAccConfirm.Text == "" || cboAccDesig.Text == "" || txtAccFName.Text == "")
                    {
                        MessageBox.Show("Please don't leave a blank!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (txtAccPwd.Text != txtAccConfirm.Text)
                    {
                        MessageBox.Show("Password mismatch! Please type again.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAccPwd.Focus();

                        return;
                    }

                    DialogResult d = MessageBox.Show("Are you sure do you want to save changes?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (d != DialogResult.Yes)
                    {
                        return;
                    }

                    if (sa.UpdateAcc(txtAccUid.Text, txtAccPwd.Text, txtAccFName.Text, cboAccDesig.Text))
                    {
                        MessageBox.Show("System Account successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnAccSearch_Click(sender, e);

                        AddClearAccInfo();
                        gpAccInfo.Enabled = false;

                        btnEditAcc.Text = "Edit";
                    }
                    else 
                    {
                        MessageBox.Show("Failed to update system account! Please try again later.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    break;
            }
        }

        private void btnDelAcc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvSysAcc.SelectedRows)
                {
                     DialogResult d = MessageBox.Show("Are you sure do you want to delete '" + row.Cells[0].Value.ToString() + "' account?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (d != DialogResult.Yes)
                    {
                        return;
                    }

                    if (sa.DeleteAcc(row.Cells[0].Value.ToString()))
                    {
                        MessageBox.Show("System Account successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnAccSearch_Click(sender, e);
                    }
                    else 
                    {
                        MessageBox.Show("Failed to delete system account! Please try again later.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboAccCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddTblProd f = new frmAddTblProd();
            f.WbName = lblWBName.Text;
            f.UploadBy = lblSysAcc.Text;

            f.ShowDialog();
        }

        private void btnSUSrefresh_Click(object sender, EventArgs e)
        {
            lblSusProdCnt.Text = upl.LoadtblProducts(dgvTblProd, lblWBName.Text,lblSysAcc.Text, "Barcode", "").ToString();
        }

        private void btnSUSfind_Click(object sender, EventArgs e)
        {
            lblSusProdCnt.Text = upl.LoadtblProducts(dgvTblProd, lblWBName.Text, lblSysAcc.Text,cboSusCateg.Text,txtSUSkeyword.Text).ToString();
        }

        private void txtSUSkeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSUSfind_Click(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvTblProd.SelectedRows)
                {
                    DialogResult d = MessageBox.Show("Are you sure do you want to remove item '" + row.Cells[2].Value.ToString() + "'?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (d != DialogResult.Yes)
                    {
                        return;
                    }

                    if (upl.DeleteItem(row.Cells[2].Value.ToString()))
                    {
                        MessageBox.Show("Item successfully removed!", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnSUSrefresh_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the item! Please try again later.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSusCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void GotoSKUrec() 
        {
            cboSKUwb.Items.Clear();
            sku.LoadSKUWbList(cboSKUwb, lblSysAcc.Text);
            lblSkuRecCount.Text = sku.LoadSKUrecord(dgvSKUrec,cboSKUwb.Text, cboSKUcateg.Text, txtSKUkeyword.Text).ToString();

            tabControl.SelectedIndex = 8;
        }

        private void btnUPCS_Click(object sender, EventArgs e)
        {
            frmUplApprvSKU frm = new frmUplApprvSKU();
            frm.WBname = lblWBName.Text;
            frm.UploadBy = lblSysAcc.Text;
            frm.GotoSKUrec += new frmUplApprvSKU.DeleGotoSKUrec(GotoSKUrec);
            frm.ShowDialog();
        }

        private void btnExpSMTemp_Click(object sender, EventArgs e)
        {
            frmExportSmTemp frm = new frmExportSmTemp();
            frm.GotoSKUmenu += new frmExportSmTemp.DeleGotoSKUmenu(SKUmenu);
            frm.WbName = lblWBName.Text;
            frm.UploadBy = lblSysAcc.Text;
            frm.ShowDialog();
        }

        private void btnUplSKU_Click(object sender, EventArgs e)
        {
            cboSKUWorkBatch.Items.Clear();
            cboSKUWorkBatch.Text = "";
            upl.LoadWbList(cboSKUWorkBatch, lblSysAcc.Text);

            tabControl.SelectedIndex = 7;
        }

        private void cboSKUWorkBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSKUuplNext_Click(object sender, EventArgs e)
        {
            if (cboSKUWorkBatch.Text == "") return;

            lblWBName.Text = cboSKUWorkBatch.Text;
            lblSusProdCnt.Text = upl.LoadtblProducts(dgvTblProd, lblWBName.Text, lblSysAcc.Text, "Barcode", "").ToString();
            btnUPCS.Enabled = true;
            btnAddItem.Enabled = false;
            btnRemove.Enabled = false;
            btnExpSMTemp.Enabled = false;
            lblSKUlabelwb.Text = "     Upload Approved SKU Application";

            tabControl.SelectedIndex = 3;
        }

        private void btnSKUplBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void btnSKUrec_Click(object sender, EventArgs e)
        {
            GotoSKUrec();
        }

        private void cboSKUcateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSKUrefresh_Click(object sender, EventArgs e)
        {
            lblSkuRecCount.Text = sku.LoadSKUrecord(dgvSKUrec, "", cboSKUcateg.Text, "").ToString();
        }

        private void btnSKUsearch_Click(object sender, EventArgs e)
        {
            lblSkuRecCount.Text = sku.LoadSKUrecord(dgvSKUrec, cboSKUwb.Text, cboSKUcateg.Text, txtSKUkeyword.Text).ToString();
        }

        private void txtSKUkeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnSKUsearch_Click(sender, e);
            }
        }

        private void cboSUSshList_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnBarcoding_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 9;
        }

        private void btnBarcodeReg_Click(object sender, EventArgs e)
        {
            string bb = bcode.checkBBpending(lblSysAcc.Text);

            if (bb != "") 
            {
                lblRegBcodeCnt.Text = bcode.LoadtblInventory(dgvBarcodeReg, bb, lblSysAcc.Text).ToString();
                lblBcodeBatchReg.Text = bb;

                tabControl.SelectedIndex = 11;

                return;
            }

            tabControl.SelectedIndex = 10;
        }

        private void btnBrowseInvXL_Click(object sender, EventArgs e)
        {
            DialogBoxes db = new DialogBoxes();

            txtInvXLfile.Text = db.BrowseExcel();
            txtInvXLfile.Focus();

            cboInvXLsheet.Items.Clear();
            upl.GetSheetName(txtInvXLfile.Text, cboInvXLsheet);
        }

        private void btnUpLInvRegBcode_Click(object sender, EventArgs e)
        {
            if (txtInvXLfile.Text == "") 
            {
                return;
            }

            DialogResult s = MessageBox.Show("Are you sure do you want to upload this excel file?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (s != DialogResult.Yes)
            {
                return;
            }

            if (cboInvXLsheet.Text == "") 
            {
                MessageBox.Show("Please select excel sheet!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboInvXLsheet.Focus();
                return;
            }

            if (txtBarcodeBatch.Text == "") 
            {
                MessageBox.Show("Please don't leave a blank the Barcode Batch Name!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcodeBatch.Focus();
                return;
            }

            if (bcode.UploadInv(txtInvXLfile.Text, cboInvXLsheet.Text, txtBarcodeBatch.Text, lblSysAcc.Text)) 
            {
                MessageBox.Show("Successfully uploaded!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblRegBcodeCnt.Text = bcode.LoadtblInventory(dgvBarcodeReg, txtBarcodeBatch.Text, lblSysAcc.Text).ToString();
                lblBcodeBatchReg.Text = txtBarcodeBatch.Text;

                txtInvXLfile.Text = "";
                cboInvXLsheet.Text = "";
                txtBarcodeBatch.Text = "";
                progBarUpLXLInv.Value = 0;

                tabControl.SelectedIndex = 11;
            }
        }

        private void btnRegBcodeBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 9;
        }

        private void btnExpRegBcode_Click(object sender, EventArgs e)
        {
            frmExpBcodeReg exp = new frmExpBcodeReg();
            exp.GotoBecodemenu += new frmExpBcodeReg.DeleGotoBcodemenu(BarcodeMenu);
            exp.bCodeBatch = lblBcodeBatchReg.Text;
            exp.UploadBy = lblSysAcc.Text;
            exp.ShowDialog();
        }

        private void btnOpenDiag_Click(object sender, EventArgs e)
        {
            DialogBoxes ofd = new DialogBoxes();
            txtImgPath.Text = ofd.BrowseImgFolder();
        }

        private void btnImagePath_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 12;
        }

        private void btnImgPath_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PdtImgPath = txtImgPath.Text;
            Properties.Settings.Default.PdtImgField = cboPdtField.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("Product Image Path successfully saved!","Save",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        void loadImgPathSettings() 
        {
            txtImgPath.Text = Properties.Settings.Default.PdtImgPath;
            cboPdtField.Text = Properties.Settings.Default.PdtImgField;
        }

        private void btnImgPathBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        bool checkOtUpd() 
        {
            foreach (DataGridViewRow row in dgvPdtML.SelectedRows)
            {
                if (row.Cells[1].Value.ToString() == "" ||
                    row.Cells[10].Value.ToString() == "" ||
                    row.Cells[11].Value.ToString() == "" ||
                    row.Cells[16].Value.ToString() == "" ||
                    row.Cells[14].Value.ToString() == "" ||
                    row.Cells[13].Value.ToString() == "" ||
                    row.Cells[15].Value.ToString() == "")
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            return false;
        }

        private void btnUpdOneTime_Click(object sender, EventArgs e)
        {
            frmUpdMasterPdt frm = new frmUpdMasterPdt();

            if (checkOtUpd() == false) 
            {
                MessageBox.Show("You can't update this record anymore.","One Time Update",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvPdtML.SelectedRows)
            {
                DialogResult d = MessageBox.Show("Are you sure do you want to one time update the item code'" + row.Cells[1].Value.ToString() + "'?", "One Time Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (d != DialogResult.Yes)
                {
                    return;
                }

                MessageBox.Show("This update is one time only, you can't do it again so please double check if your entries are correct!","One Time Update",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                DialogResult cont = MessageBox.Show("Continue one time update?", "One Time Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (cont != DialogResult.Yes)
                {
                    return;
                }

                frm.bcode = row.Cells[0].Value.ToString();
                frm.desc = row.Cells[2].Value.ToString();

                frm.brand = row.Cells[5].Value.ToString();
                frm.group = row.Cells[11].Value.ToString();
                frm.category = row.Cells[6].Value.ToString();
                frm.oSRP = row.Cells[7].Value.ToString();
                frm.UOM = row.Cells[9].Value.ToString();

                frm.itemCode = row.Cells[1].Value.ToString();
                frm.itemName = row.Cells[10].Value.ToString();
                frm.batch = row.Cells[11].Value.ToString();
                frm.material = row.Cells[16].Value.ToString();
                frm.upper = row.Cells[14].Value.ToString();
                frm.sole = row.Cells[13].Value.ToString(); ;
                frm.style = row.Cells[15].Value.ToString();

                frm.mkdnStat = row.Cells[19].Value.ToString();
                frm.movingStat = row.Cells[20].Value.ToString();
                frm.itemStat = row.Cells[18].Value.ToString();
                frm.fSRP = Convert.ToDecimal(row.Cells[17].Value.ToString());
                frm.SRP = Convert.ToDecimal(row.Cells[8].Value.ToString());

                frm.oneTimeUpd = true;
                frm.UpdateBy = lblSysAcc.Text;

                frm.ShowDialog();
            }
        }

        private void btnFindPdt_Click(object sender, EventArgs e)
        {
            lblMasterPdtCnt.Text = upl.LoadMasterPdt(dgvPdtML, cboPdtWb.Text, cboPdtUpdBy.Text, cboPdtCateg.Text, txtPdtKeyword.Text).ToString();
        }

        private void txtPdtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnFindPdt_Click(sender, e);
            }
        }

        private void cboPdtCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnUpdPdtStat_Click(object sender, EventArgs e)
        {
            frmUpdMasterPdt frm = new frmUpdMasterPdt();

            foreach (DataGridViewRow row in dgvPdtML.SelectedRows)
            {
                DialogResult d = MessageBox.Show("Are you sure do you want to update product status of item '" + row.Cells[2].Value.ToString() + "'?", "Update Pdt Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (d != DialogResult.Yes)
                {
                    return;
                }

                frm.bcode = row.Cells[0].Value.ToString();
                frm.desc = row.Cells[2].Value.ToString();

                frm.brand = row.Cells[5].Value.ToString();
                frm.group = row.Cells[11].Value.ToString();
                frm.category = row.Cells[6].Value.ToString();
                frm.oSRP = row.Cells[7].Value.ToString();
                frm.UOM = row.Cells[9].Value.ToString();

                frm.itemCode = row.Cells[1].Value.ToString();
                frm.itemName = row.Cells[10].Value.ToString();
                frm.batch = row.Cells[11].Value.ToString();
                frm.material = row.Cells[16].Value.ToString();
                frm.upper = row.Cells[14].Value.ToString();
                frm.sole = row.Cells[13].Value.ToString(); ;
                frm.style = row.Cells[15].Value.ToString();

                frm.mkdnStat = row.Cells[19].Value.ToString();
                frm.movingStat = row.Cells[20].Value.ToString();
                frm.itemStat = row.Cells[18].Value.ToString();
                frm.fSRP = Convert.ToDecimal(row.Cells[17].Value.ToString());
                frm.SRP = Convert.ToDecimal(row.Cells[8].Value.ToString());

                frm.oneTimeUpd = false;
                frm.UpdateBy = lblSysAcc.Text;

                frm.ShowDialog();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmUpdMasterPdt frm = new frmUpdMasterPdt();

            foreach (DataGridViewRow row in dgvPdtML.SelectedRows)
            {
                frm.bcode = row.Cells[0].Value.ToString();
                frm.desc = row.Cells[2].Value.ToString();

                frm.brand = row.Cells[5].Value.ToString();
                frm.group = row.Cells[11].Value.ToString();
                frm.category = row.Cells[6].Value.ToString();
                frm.oSRP = row.Cells[7].Value.ToString();
                frm.UOM = row.Cells[9].Value.ToString();

                frm.itemCode = row.Cells[1].Value.ToString();
                frm.itemName = row.Cells[10].Value.ToString();
                frm.batch = row.Cells[11].Value.ToString();
                frm.material = row.Cells[16].Value.ToString();
                frm.upper = row.Cells[14].Value.ToString();
                frm.sole = row.Cells[13].Value.ToString(); ;
                frm.style = row.Cells[15].Value.ToString();

                frm.mkdnStat = row.Cells[19].Value.ToString();
                frm.movingStat = row.Cells[20].Value.ToString();
                frm.itemStat = row.Cells[18].Value.ToString();
                frm.fSRP = Convert.ToDecimal(row.Cells[17].Value.ToString());
                frm.SRP = Convert.ToDecimal(row.Cells[8].Value.ToString());

                frm.oneTimeUpd = false;
                frm.pdtStatDisable();

                frm.ShowDialog();
            }
        }

        private void btnDelPdt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPdtML.SelectedRows)
            {
                DialogResult d = MessageBox.Show("Are you sure do you want to delete product item '" + row.Cells[2].Value.ToString() + "'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (d != DialogResult.Yes)
                {
                    return;
                }

                MessageBox.Show("Warning! All data associated to this item will also deleted.","Delete",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                DialogResult dd = MessageBox.Show("Continue to delete this product item?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dd != DialogResult.Yes)
                {
                    return;
                }

                if (upl.DeletePdt(row.Cells[2].Value.ToString()))
                {
                    MessageBox.Show("Product item '" + upl.DeletePdt(row.Cells[2].Value.ToString()) + "' is successfully deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnFindPdt_Click(sender, e);
                }
                else 
                {
                    MessageBox.Show("Failed to delete. Please try again.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnTabBcodeRegBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 9;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            if (smVdr.VdrBcodeBatchList(cboVdrBcodeBatch, lblSysAcc.Text))
            {
                
            }
            else 
            {
                //MessageBox.Show("Failed to load page Create VDR. Please try again.","Product Delivery",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            tabControl.SelectedIndex = 13;
        }

        private void cboVdrBcodeBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnVdrNext_Click(object sender, EventArgs e)
        {
            if (cboVdrBcodeBatch.Text == "") return;
   
            lblVdrBcodeBatch.Text = cboVdrBcodeBatch.Text;
            lblVdrExpRecnt.Text = smVdr.LoadVdrExpList(dgvVdrExpList, lblSysAcc.Text, lblVdrBcodeBatch.Text).ToString();

            cboVdrBcodeBatch.Text = "";

            tabControl.SelectedIndex = 14;
        }

        private void btnVdrExpBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 13;
        }

        private void btnVdrExpTemp_Click(object sender, EventArgs e)
        {
            frmExpSmVDR Vdr = new frmExpSmVDR();
            Vdr.GotoVdr += new frmExpSmVDR.DeleGotoVdr(CreateVdr);
            Vdr.UplBy = lblSysAcc.Text;
            Vdr.BbName = lblVdrBcodeBatch.Text;

            Vdr.ShowDialog();
        }

        private void btnPullOut_Click(object sender, EventArgs e)
        {
            //smVpo.VpoBcodeBatchList(cboVpoBcodeBatch, lblSysAcc.Text);
            //tabControl.SelectedIndex = 15;

            string poBatch = delv.checkPoPendings(lblSysAcc.Text);

            if (poBatch != "")
            {
                POfresh = true;

                lblDeliveredCnt.Text = delv.LoadVweDelivered(dgvDeliveredRec, cboDelvCreatedBy.Text, cboDelvBatch.Text, cboDelvCateg.Text,"f", true).ToString();
                lblToPoCount.Text = po.LoadtoPullOut(dgvToPO, lblSysAcc.Text, poBatch).ToString();

                lblPullOutBatch.Text = poBatch;
                sa.LoadAccList(cboDelvCreatedBy);

                tabControl.SelectedIndex = 21;
            }
            else 
            {
                txtPullOutBatch.Text = "";
                tabControl.SelectedIndex = 20;
            }
        }

        private void btnVpoNext_Click(object sender, EventArgs e)
        {
            if (cboVpoBcodeBatch.Text == "") return;

            lblVpoBcodeBatch.Text = cboVpoBcodeBatch.Text;
            lblVpoRecnt.Text = smVpo.LoadVpoExpList(dgvVpoExpList, lblSysAcc.Text, lblVpoBcodeBatch.Text).ToString();

            cboVpoBcodeBatch.Text = "";

            tabControl.SelectedIndex = 16;
        }

        private void cboVpoBcodeBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnVpoBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 15;
        }

        private void btnVpoExp_Click(object sender, EventArgs e)
        {
            frmExpSmVPO Vpo = new frmExpSmVPO();
            Vpo.GotoVpo += new frmExpSmVPO.DeleGotoVpo(CreateVpo);
            Vpo.UplBy = lblSysAcc.Text;
            Vpo.BbName = lblVpoBcodeBatch.Text;

            Vpo.ShowDialog();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Enter) 
            {
                SystemLock();

                frmLogin frm = new frmLogin();
                frm.UnlockSystem += new frmLogin.DeleUnlockSystem(SystemUnlock);
                frm.ShowDialog();
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (lblSysAcc.Text == "Lock") return;

                DialogResult d = MessageBox.Show("Are you sure do you want to logout?","Logout",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

                if (d == DialogResult.Yes) 
                {
                    SystemLock();
                }
            }
        }

        private void cboPdtUpdBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //upl.LoadPdtWbList(cboPdtWb, lblSysAcc.Text);
        }

        private void cboPdtUpdBy_SelectedValueChanged(object sender, EventArgs e)
        {
            upl.LoadPdtWbList(cboPdtWb, cboPdtUpdBy.Text);
            cboPdtWb.Text = "";
        }

        private void btnSearchRm_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.teamviewer.com/en/teamviewer-automatic-download/"); 
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 17;
        }

        private void label53_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.projectfsoftwaresolutions.net"); 
        }

        private void txtAccUid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAccPwd.Focus();
            }

            if (e.KeyCode == Keys.Tab)
            {
                txtAccPwd.Focus();
            }
        }

        private void txtAccPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAccConfirm.Focus();
            }
        }

        private void txtAccConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAccFName.Focus();
            }
        }

        private void txtAccFName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboAccDesig.Focus();
            }
        }

        private void cboAccDesig_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnBrandSettings_Click(object sender, EventArgs e)
        {
            txtBrandKeyword.Text = "";
            btnBrandSearch_Click(sender, e);

            tabControl.SelectedIndex = 18;
        }

        private void btnStoreSettings_Click(object sender, EventArgs e)
        {
            txtStoreKeyword.Text = "";
            btnStoreSearch_Click(sender, e);

            tabControl.SelectedIndex = 19;
        }

        private void btnBreandSearch_Click(object sender, EventArgs e)
        {
            lblBrandRecnt.Text = brCode.Loadtblbrand(dgvBrandRec, cboBrandCateg.Text, txtBrandKeyword.Text).ToString();
        }

        private void cboBrandCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        bool CheckFilds() 
        {
            if (txtBrandCode.Text == "" || txtBrandName.Text == "")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        void ResetInfo() 
        {
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            txtBrandKeyword.Text = "";
            gbBrandInfo.Enabled = false;
        }

        private void btnBrandAdd_Click(object sender, EventArgs e)
        {
            switch(btnBrandAdd.Text)
            {
                case "Add":
                    ResetInfo();
                    gbBrandInfo.Enabled = true;
                    txtBrandCode.Focus();

                    btnBrandAdd.Text = "Save";
                    break;
                case "Save":
                    if (CheckFilds() == true)
                    {
                        MessageBox.Show("Please fill up the blanks!", "Save",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("Are you sure your entries are correct?","Save",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes) return;

                    if(brCode.CheckBrand(txtBrandCode.Text,txtBrandName.Text))
                    {
                        MessageBox.Show("BrandCode or BrandName is already exist!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (brCode.SaveBrand(txtBrandCode.Text, txtBrandName.Text))
                    {
                        MessageBox.Show("Brand successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetInfo();
                        btnBrandSearch_Click(sender, e);
                    }
                    else 
                    {
                        MessageBox.Show("Failed to saved! Please try again.","Save",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }

                    btnBrandAdd.Text = "Add";
                    break;
            }
        }

        private void txtBrandCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) 
            {
                txtBrandName.Focus();
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtBrandName.Focus();
            }
        }

        private void btnBrandDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvBrandRec.SelectedRows)
                {
                    DialogResult d = MessageBox.Show("Are you sure do you want to delete '" + row.Cells[0].Value.ToString() + "' brand?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (d != DialogResult.Yes)
                    {
                        return;
                    }

                    if (brCode.DeleteBrand(row.Cells[0].Value.ToString()))
                    {
                        MessageBox.Show("Brand code successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnBrandSearch_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete brand code! Please try again later.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStoreBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void btnStoreSearch_Click(object sender, EventArgs e)
        {
            lblStoreRCount.Text = strCode.Loadtblstore(dgvStoreRec,cboStoreCateg.Text,txtStoreKeyword.Text).ToString();
        }

        private void cboStoreCateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void ResetStoreInfo() 
        {
            txtStoreCode.Text = "";
            txtStoreName.Text = "";
            txtStoreKeyword.Text = "";
            gbStoreInfo.Enabled = false;
        }

        bool CheckStoreFilds() 
        {
            if (txtStoreCode.Text == "" || txtStoreName.Text == "")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void btnStoreAdd_Click(object sender, EventArgs e)
        {
            switch (btnStoreAdd.Text)
            {
                case "Add":
                    ResetStoreInfo();
                    gbStoreInfo.Enabled = true;
                    txtStoreCode.Focus();

                    btnStoreAdd.Text = "Save";
                    break;
                case "Save":
                    if (CheckStoreFilds() == true)
                    {
                        MessageBox.Show("Please fill up the blanks!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("Are you sure your entries are correct?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes) return;

                    if (strCode.CheckStore(txtStoreCode.Text, txtStoreName.Text))
                    {
                        MessageBox.Show("StoreCode or StoreName is already exist!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (strCode.SaveStore(txtStoreCode.Text, txtStoreName.Text))
                    {
                        MessageBox.Show("Store successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetStoreInfo();
                        btnStoreSearch_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to saved! Please try again.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnStoreAdd.Text = "Add";
                    break;
            }
        }

        private void btnStoreDel_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvStoreRec.SelectedRows)
                {
                    DialogResult d = MessageBox.Show("Are you sure do you want to delete '" + row.Cells[0].Value.ToString() + "' store?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (d != DialogResult.Yes)
                    {
                        return;
                    }

                    if (strCode.DeleteStore(row.Cells[0].Value.ToString()))
                    {
                        MessageBox.Show("StoreCode successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnStoreSearch_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete StoreCode! Please try again later.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtStoreKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnStoreSearch_Click(sender, e);
            }
        }

        private void txtBrandKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnBrandSearch_Click(sender, e);
            }
        }

        private void btnBrandSearch_Click(object sender, EventArgs e)
        {
            lblBrandRecnt.Text = brCode.Loadtblbrand(dgvBrandRec, cboBrandCateg.Text, txtBrandKeyword.Text).ToString();
        }

        private void txtStoreCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtStoreName.Focus();
            }
        }

        private void btnStoreBackSettings_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void tabExpPullOut21_Click(object sender, EventArgs e)
        {

        }

        private void btnPOnext_Click(object sender, EventArgs e)
        {
            if (txtPullOutBatch.Text == "") return;

            if (po.CheckPOBatch(lblSysAcc.Text, txtPullOutBatch.Text))
            {
                MessageBox.Show("Pull Out batch already exist!", "Next", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                POfresh = delv.checkPO();

                lblPullOutBatch.Text = txtPullOutBatch.Text;
                lblDeliveredCnt.Text = delv.LoadVweDelivered(dgvDeliveredRec, cboDelvCreatedBy.Text, cboDelvBatch.Text, cboDelvCateg.Text, "f", POfresh).ToString();
                lblToPoCount.Text = po.LoadtoPullOut(dgvToPO, lblSysAcc.Text, lblPullOutBatch.Text).ToString();
                sa.LoadAccList(cboDelvCreatedBy);

                txtPullOutBatch.Text = "";

                tabControl.SelectedIndex = 21;
            }
        }

        private void txtDelvKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnDelvSearch_Click(sender,e);
            }
        }

        private void btnToPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQtyPO.Value <= 0) 
                {
                    MessageBox.Show("Please input the quantity of the product you want to be pulled out!","Pull Out",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in dgvDeliveredRec.SelectedRows)
                {
                    int stockQty = Convert.ToInt32(row.Cells[4].Value.ToString());

                    if (stockQty < txtQtyPO.Value) 
                    {
                        MessageBox.Show("You can't pull out the quantity that exceeded the total quantity remaining.", "Pull Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (po.CheckStoreCode(lblSysAcc.Text, row.Cells[3].Value.ToString(), lblPullOutBatch.Text) == false) 
                    {
                        if (dgvToPO.Rows.Count == 0) goto pullit;

                        MessageBox.Show("You can't pull out the product with the different StoreCode.", "Pull Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

pullit:             if (po.insPullOut(row.Cells[1].Value.ToString(), row.Cells[3].Value.ToString(),Convert.ToInt32(txtQtyPO.Value), DateTime.Now, lblPullOutBatch.Text, lblSysAcc.Text))
                    {
                        lblDeliveredCnt.Text = delv.LoadVweDelivered(dgvDeliveredRec, cboDelvCreatedBy.Text, cboDelvBatch.Text, cboDelvCateg.Text, txtDelvKeyword.Text, true).ToString();
                        lblToPoCount.Text = po.LoadtoPullOut(dgvToPO, lblSysAcc.Text, lblPullOutBatch.Text).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Failed to pull out! Please try again.", "Pull Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cboDelvCreatedBy_SelectedValueChanged(object sender, EventArgs e)
        {
            delv.LoadDelBatchCbo(cboDelvBatch, cboDelvCreatedBy.Text);
        }

        private void btnDelvSearch_Click(object sender, EventArgs e)
        {
            lblDeliveredCnt.Text = delv.LoadVweDelivered(dgvDeliveredRec, cboDelvCreatedBy.Text, cboDelvBatch.Text, cboDelvCateg.Text, txtDelvKeyword.Text, POfresh).ToString();
        }

        private void btnCancelPO_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvToPO.SelectedRows)
                {
                    if (po.delPullOut(row.Cells[1].Value.ToString()))
                    {
                        lblDeliveredCnt.Text = delv.LoadVweDelivered(dgvDeliveredRec, cboDelvCreatedBy.Text, cboDelvBatch.Text, cboDelvCateg.Text, txtDelvKeyword.Text, true).ToString();
                        lblToPoCount.Text = po.LoadtoPullOut(dgvToPO, lblSysAcc.Text, lblPullOutBatch.Text).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Failed to redo! Please try again later.", "Redo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExpPOList_Click(object sender, EventArgs e)
        {
            frmExpSmVPO Vpo = new frmExpSmVPO();
            Vpo.GotoVpo += new frmExpSmVPO.DeleGotoVpo(CreateVpo);
            Vpo.UplBy = lblSysAcc.Text;
            Vpo.BbName = lblPullOutBatch.Text;

            Vpo.ShowDialog();
        }

        private void tabExpSmSkuApp3_Click(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabDbSettings5_Click(object sender, EventArgs e)
        {

        }
    }
}
