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
    public partial class frmExportSmTemp : Form
    {
        internal delegate void DeleGotoSKUmenu();
        internal event DeleGotoSKUmenu GotoSKUmenu;

        SUS_Upload exp = new SUS_Upload();
        BrandCode brCode = new BrandCode();

        public string WbName;
        public string UploadBy;

        public frmExportSmTemp()
        {
            InitializeComponent();
            exp.UpdateProgress += UpdateProgress;
            exp.UpdProgBaxMax += UpdProgBarMax;
        }

        void UpdateProgress(int ProgressPercentage)
        {
            if (ProgressPercentage < 0)
            {
                return;
            }
            progBar.Value = ProgressPercentage;
        }

        void UpdProgBarMax(int MaxVal, int MinVal)
        {
            progBar.Maximum = MaxVal;
            progBar.Minimum = MinVal;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpSMTemp_Click(object sender, EventArgs e)
        {
            if (txtVendorCode.Text == "" || txtDeptCode.Text == "" || txtBrandCode.Text == "" || txtFourteenField.Text == "" || txtFifteenField.Text == "" || txtExcelFname.Text == "") 
            {
                MessageBox.Show("Please don't leave a blank those important fields!","Export",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            MessageBox.Show("If you continue you can't add or remove items in this particular Work Batch Name.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            DialogResult dg = MessageBox.Show("Do you still want to continue?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            string dateCreated = dtCreated.Value.ToString("dd MM yyyy");
            string dateSaved = dtSaved.Value.ToString("dd MM yyyy");

            if (exp.expExcelSmTemp(WbName,txtFirstField.Text,txtVendorCode.Text,txtDeptCode.Text,txtSubDeptCode.Text,txtBrandCode.Text,txtStockStyle.Text,txtSourcemarked.Text,txtFourteenField.Text,txtFifteenField.Text,dateCreated,dateSaved,txtExcelFname.Text)) 
            {
retryStat:      if (exp.UpdateFileStat(UploadBy,WbName,1))
                {
                    MessageBox.Show("Work Batch '" + WbName + "' successfully exported!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (GotoSKUmenu != null) 
                    {
                        GotoSKUmenu();
                    }                

                    this.Close();
                }
                else 
                {
                    goto retryStat;    
                }
            }
        }

        private void txtFirstField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtVendorCode.Focus();
            }
        }

        private void txtVendorCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDeptCode.Focus();
            }
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSubDeptCode.Focus();
            }
        }

        private void txtSubDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrandCode.Focus();
            }
        }

        private void txtBrandCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStockStyle.Focus();
            }
        }

        private void txtStockStyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSourcemarked.Focus();
            }
        }

        private void txtSourcemarked_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFourteenField.Focus();
            }
        }

        private void txtFourteenField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFifteenField.Focus();
            }
        }

        private void txtFifteenField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtCreated.Focus();
            }
        }

        private void dtCreated_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtSaved.Focus();
            }
        }

        private void dtSaved_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtExcelFname.Focus();
            }
        }

        private void txtExcelFname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnExpSMTemp.Focus();
            }
        }

        private void frmExportSmTemp_Load(object sender, EventArgs e)
        {
            brCode.LoadBrandCodeCbo(cboBrand);
            cboBrand.Text = "";
            txtBrandCode.Text = "";
        }

        private void cboBrand_SelectedValueChanged(object sender, EventArgs e)
        {
            txtBrandCode.Text = brCode.GetBrandCode(cboBrand.Text);
        }

        private void cboBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                txtStockStyle.Focus();
            }
        }
    }
}
