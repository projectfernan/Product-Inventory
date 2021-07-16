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
    public partial class frmExpSmVDR : Form
    {
        internal delegate void DeleGotoVdr();
        internal event DeleGotoVdr GotoVdr;

        public string UplBy = "";
        public string BbName = "";

        Barcoding bcode = new Barcoding();
        SM_VDR_Temp vdr = new SM_VDR_Temp();
        StoreCode strCode = new StoreCode();

        public frmExpSmVDR()
        {
            InitializeComponent();
        }

        private void btnExpSMTemp_Click(object sender, EventArgs e)
        {
            if (txtDRno.Text == "" || txtStoreCode.Text == "" || txtExcelFname.Text == "")
            {
                MessageBox.Show("Please don't leave a blank those important fields!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            if (vdr.expExlSmVDRTemp(BbName, UplBy, txtDRno.Text, txtStoreCode.Text,dtExpectDelivery.Value,txtDeliBatch.Text,txtExcelFname.Text))
            {
            retryStat: if (bcode.UpdInvFileStat(UplBy, BbName, 2))
                {
                    MessageBox.Show("Barcode Batch '" + BbName + "' successfully exported!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (GotoVdr != null)
                    {
                        GotoVdr();
                    }

                    this.Close();
                }
                else
                {
                    goto retryStat;
                }
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDRno_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtStoreCode.Focus();
            }
        }

        private void txtStoreCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtExpectDelivery.Focus();
            }
        }

        private void dtExpectDelivery_KeyDown(object sender, KeyEventArgs e)
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

        private void frmExpSmVDR_Load(object sender, EventArgs e)
        {
            strCode.LoadStoreCodeCbo(cboStore);
            txtStoreCode.Text = "";
            cboStore.Text = "";
        }

        private void cboStore_SelectedValueChanged(object sender, EventArgs e)
        {
            txtStoreCode.Text = strCode.GetStoreCode(cboStore.Text);
        }

        private void cboStore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                dtExpectDelivery.Focus();
            }
        }
    }
}
