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
    public partial class frmExpBcodeReg : Form
    {
        internal delegate void DeleGotoBcodemenu();
        internal event DeleGotoBcodemenu GotoBecodemenu;

        public string bCodeBatch;
        public string UploadBy;

        Barcoding bcode = new Barcoding();

        public frmExpBcodeReg()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpSMTemp_Click(object sender, EventArgs e)
        {
            if (txtVendor.Text == "" || txtSeason.Text == "" || txtExcelFname.Text == "") 
            {
                MessageBox.Show("Please don't leave a blank those important fields!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            if(bcode.expExcelBcodeReg(bCodeBatch,UploadBy,txtVendor.Text,txtSeason.Text,txtExcelFname.Text))
            {
retryStat:      if (bcode.UpdInvFileStat(UploadBy, bCodeBatch, 1))
                {
                    MessageBox.Show("Barcode Batch '" + bCodeBatch + "' successfully exported!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (GotoBecodemenu != null)
                    {
                        GotoBecodemenu();
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
                txtSeason.Focus();
            }
        }

        private void txtSeason_KeyDown(object sender, KeyEventArgs e)
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
    }
}
