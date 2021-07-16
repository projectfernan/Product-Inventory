using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace prjInventory
{
    public partial class frmUpdMasterPdt : Form
    {
        public string bcode;
        public string desc;
        public string UpdateBy;

        public string brand;
        public string group;
        public string category;
        public string oSRP;
        public string UOM;

        public string itemCode;
        public string itemName;
        public string batch;
        public string material;
        public string upper;
        public string sole;
        public string style;

        public string mkdnStat;
        public string movingStat;
        public string itemStat;
        public decimal fSRP;
        public decimal SRP;

        public bool oneTimeUpd;
        public string ImgPath = "";

        public frmUpdMasterPdt()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void loadData() 
        {
            txtBrand.Text = brand;
            txtGroup.Text = group;
            txtCategory.Text = category;
            txtoSRP.Text = oSRP;
            txtUOM.Text = UOM;

            txtItemCode.Text = itemCode;
            txtItemName.Text = itemName;
            txtBatch.Text = batch;
            txtMaterial.Text = material;
            txtUpper.Text = upper;
            txtSole.Text = sole;
            txtStyle.Text = style;

            txtMkdnStat.Text = mkdnStat;
            txtMoveStat.Text = movingStat;
            txtItemStat.Text = itemStat;
            txtfSRP.Value = fSRP;
            txtSRP.Value = SRP;

            string path = imgFile();

            pbPDT.ImageLocation = path;
        }

        void oneTimeUpdEnable() 
        {
            txtItemName.ReadOnly = true;
            txtGroup.ReadOnly = true;
            txtBatch.ReadOnly = true;
            txtMaterial.ReadOnly = true;
            txtUpper.ReadOnly = true;
            txtSole.ReadOnly = true;
            txtStyle.ReadOnly = true;
        }

        void oneTimeUpdDisable()
        {
            txtItemName.ReadOnly = false;
            txtGroup.ReadOnly = false;
            txtBatch.ReadOnly = false;
            txtMaterial.ReadOnly = false;
            txtUpper.ReadOnly = false;
            txtSole.ReadOnly = false;
            txtStyle.ReadOnly = false;
        }

        public void pdtStatDisable() 
        {
            txtMkdnStat.ReadOnly = true;
            txtMoveStat.ReadOnly = true;
            txtItemStat.ReadOnly = true;
            txtfSRP.Enabled = false;
            txtSRP.Enabled = false;
            btnSavePdt.Visible = false;
            btnBrowse.Visible = false;
            btnBrowse.Focus();
        }

        public void pdtStatEnable()
        {
            txtMkdnStat.ReadOnly = false;
            txtMoveStat.ReadOnly = false;
            txtItemStat.ReadOnly = false;
            txtfSRP.Enabled = true;
            txtSRP.Enabled = true;
            btnSavePdt.Visible = true;
            btnBrowse.Visible = true;
        }

        bool checkBlankOT() 
        {
            if (txtItemCode.Text == "" ||
              txtItemName.Text == "" ||
              txtBatch.Text == "" ||
              txtMaterial.Text == "" ||
              txtUpper.Text == "" ||
               txtSole.Text == "" ||
              txtStyle.Text == "")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        bool checkPdtStat() 
        {
            if (txtMkdnStat.Text == "" ||
                txtMoveStat.Text == "" ||
                txtItemStat.Text == "")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void frmUpdMasterPdt_Load(object sender, EventArgs e)
        {
            loadData();

            if (oneTimeUpd) 
            {
                oneTimeUpdDisable();
            }
            else
            {
                oneTimeUpdEnable();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogBoxes imgF = new DialogBoxes();
            ImgPath = imgF.BrowseImageFile();
            pbPDT.ImageLocation = ImgPath;
        }

        string FileName() 
        {
            switch (Properties.Settings.Default.PdtImgField) 
            { 
                case "ItemCode":
                    return itemCode;
                case  "Barcode":
                    return bcode;
                case "Description":
                    return desc;
                default:
                    return "";
            }
        }

        string imgFile() 
        {
            try
            {
                string imgFilePath = Properties.Settings.Default.PdtImgPath + @"\" + FileName() + ".JPEG";

                if (File.Exists(imgFilePath))
                {
                    return imgFilePath;
                }
                else
                {
                    return Application.StartupPath + @"\" + "NoPhoto.jpg";
                }
                
            }
            catch 
            {
                return Application.StartupPath + @"\" + "NoPhoto.jpg";
            }
        }

        private void btnSavePdt_Click(object sender, EventArgs e)
        {
            if (oneTimeUpd)
            {
                if (checkBlankOT())
                {
                    MessageBox.Show("Please don't leave a blank!!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else 
            {
                if (checkPdtStat()) 
                {
                    MessageBox.Show("Please don't leave a blank!!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult d = MessageBox.Show("Are you sure your entries are correct?","Update",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (d != DialogResult.Yes) return;

            SUS_Upload upd = new SUS_Upload();
            if (upd.UpdatePdt(desc,txtItemName.Text,txtGroup.Text,txtBatch.Text,txtMaterial.Text,txtUpper.Text,txtSole.Text,txtStyle.Text,txtMkdnStat.Text,txtMoveStat.Text,txtItemStat.Text,Convert.ToDouble( txtSRP.Value),Convert.ToDouble( txtfSRP.Value),UpdateBy,itemCode) == false) 
            {
                MessageBox.Show("Failed to update. Please try again.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FileCopy cp = new FileCopy();
            if (cp.CopyImage(ImgPath, FileName()) == false) 
            {
                MessageBox.Show("Failed to copy product image file!","Update",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Product successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
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
                txtoSRP.Focus();
            }
        }

        private void txtoSRP_KeyDown_1(object sender, KeyEventArgs e)
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
                txtItemName.Focus();
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGroup.Focus();
            }
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBatch.Focus();
            }
        }

        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaterial.Focus();
            }
        }

        private void txtMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUpper.Focus();
            }
        }

        private void txtUpper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSole.Focus();
            }
        }

        private void txtSole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStyle.Focus();
            }
        }

        private void txtStyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMkdnStat.Focus();
            }
        }

        private void txtMkdnStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMoveStat.Focus();
            }
        }

        private void txtMoveStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemStat.Focus();
            }
        }

        private void txtItemStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfSRP.Focus();
            }
        }

        private void txtfSRP_KeyDown(object sender, KeyEventArgs e)
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
                btnSavePdt.Focus();
            }
        }

       
    }
}
