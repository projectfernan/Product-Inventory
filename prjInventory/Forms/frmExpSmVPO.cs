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
    public partial class frmExpSmVPO : Form
    {
        internal delegate void DeleGotoVpo();
        internal event DeleGotoVpo GotoVpo;

        public string UplBy = "";
        public string BbName = "";

        Barcoding bcode = new Barcoding();
        SM_VPO_Temp vpo = new SM_VPO_Temp();
        StoreCode strCode = new StoreCode();
        ProductPullOut po = new ProductPullOut();

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

        public frmExpSmVPO()
        {
            InitializeComponent();

            vpo.UpdateProgress += UpdateProgress;
            vpo.UpdProgBaxMax += UpdProgBarMax;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpSMTemp_Click(object sender, EventArgs e)
        {
            if (txtVendor.Text == "" || txtSCPOA.Text == "" || txtExcelFname.Text == "")
            {
                MessageBox.Show("Please don't leave a blank those important fields!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            if (vpo.expExlSmVpoTemp(BbName, UplBy, txtVendor.Text, txtSCPOA.Text, dtExpectPOut.Value, Convert.ToInt32(txtBoxes.Value), txtExcelFname.Text))
            {
retryStat:  if (po.updPullOutStat(BbName,UplBy,dtExpectPOut.Value, 1))
                {
                    MessageBox.Show("Pull Out Batch '" + BbName + "' successfully exported!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (GotoVpo != null)
                    {
                        GotoVpo();
                    }

                    this.Close();
                }
                else
                {
                    goto retryStat;
                }
            }
        }

        private void txtVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCPOA.Focus();
            }
        }

        private void txtSCPOA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtExpectPOut.Focus();
            }
        }

        private void dtExpectPOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBoxes.Focus();
            }
        }

        private void txtBoxes_KeyDown(object sender, KeyEventArgs e)
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
                btnExpSMTemp. Focus();
            }
        }

        private void frmExpSmVPO_Load(object sender, EventArgs e)
        {
            //strCode.LoadStoreCodeCbo(cboStore);
            //txtStoreCode.Text = "";
            //cboStore.Text = "";
        }

        private void cboStore_SelectedValueChanged(object sender, EventArgs e)
        {
            //txtStoreCode.Text = strCode.GetStoreCode(cboStore.Text);
        }

        private void cboStore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                dtExpectPOut.Focus();
            }
        }
    }
}
