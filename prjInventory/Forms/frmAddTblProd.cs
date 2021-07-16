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
    public partial class frmAddTblProd : Form
    {
        public string WbName;
        public string UploadBy;

        SUS_Upload addPdt = new SUS_Upload();
        public frmAddTblProd()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddTblProd_Load(object sender, EventArgs e)
        {

        }

        private void btnSavePdt_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "" || txtItemCode.Text == "" || txtDescription.Text == "" || txtColor.Text == "" || txtSize.Text == "" || txtBrand.Text == "" || txtCategory.Text == "" || txtUOM.Text == "")
            {
                MessageBox.Show("Please don't leave a blank!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult d = MessageBox.Show("Are you sure do your entries are correct?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d != DialogResult.Yes)
            {
                return;
            }

            string desc = txtDescription.Text;

            if (addPdt.checkDesc(desc.Replace("/", "").Replace("-", "").Replace(" ","")))
            {
                MessageBox.Show("Item '" + txtDescription.Text + "' is already exist!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (addPdt.AddProduct(txtBarcode.Text, txtItemCode.Text, txtDescription.Text, txtColor.Text, txtSize.Text, txtBrand.Text, txtCategory.Text, Convert.ToDouble(txtOsrp.Value), Convert.ToDouble(txtSRP.Value), txtUOM.Text, WbName, UploadBy))
            {
                MessageBox.Show("New product successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else 
            {
                MessageBox.Show("Failed to save new product! Please try again later.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                txtItemCode.Focus();
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Focus();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtColor.Focus();
            }
        }

        private void txtColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSize.Focus();
            }
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrand.Focus();
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCategory.Focus();
            }
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOsrp.Focus();
            }
        }

        private void txtOsrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSRP.Focus();
            }
        }

        private void txtSRP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUOM.Focus();
            }
        }

        private void txtUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                 btnSavePdt.Focus();
            }
        }
    }
}
