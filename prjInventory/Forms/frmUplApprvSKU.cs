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
    public partial class frmUplApprvSKU : Form
    {
        public string WBname;
        public string UploadBy;

        internal delegate void DeleGotoSKUrec();
        internal event DeleGotoSKUrec GotoSKUrec;

        SKU_Upload sku = new SKU_Upload();
        Excel_Conn xl = new Excel_Conn();

        public frmUplApprvSKU()
        {
            InitializeComponent();

            sku.UpdateProgress += UpdateProgress;
            sku.UpdProgBaxMax += UpdProgBarMax;
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogBoxes db = new DialogBoxes();

            txtExcelPath.Text = db.BrowseExcel();
            txtExcelPath.Focus();

            cboXLsheet.Items.Clear();
            xl.GetSheetName(txtExcelPath.Text, cboXLsheet);
        }

        private void btnUplSUS_Click(object sender, EventArgs e)
        {
            if (txtExcelPath.Text == "")
            {
                return;
            }

            if(cboXLsheet.Text == "")
            {
                MessageBox.Show("Please select excel sheet!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboXLsheet.Focus();
                return;
            }

            DialogResult s = MessageBox.Show("Are you sure do you want to upload this excel file?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (s != DialogResult.Yes)
            {
                return;
            }

            if (sku.UploadSKU(txtExcelPath.Text,cboXLsheet.Text, WBname, UploadBy))
            {
                MessageBox.Show("Successfully uploaded!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SUS_Upload pdt = new SUS_Upload();
                pdt.UpdateFileStat(UploadBy, WBname,2);

                if (GotoSKUrec != null)
                {
                    GotoSKUrec();
                }
                this.Close();
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
