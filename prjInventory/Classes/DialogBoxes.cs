using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace prjInventory
{
    class DialogBoxes
    {
        public string BrowseExcel() 
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();

                OFD.InitialDirectory = @"C:\\";
                OFD.Title = "Browse Excel File";
                OFD.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";
                OFD.CheckFileExists = true;
                OFD.CheckPathExists = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    return OFD.FileName;
                }
                else 
                {
                    return "";
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error File Dialog Box",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return "";
            }
        }

        public string BrowseImageFile()
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();

                OFD.InitialDirectory = @"C:\\";
                OFD.Title = "Browse Image File";
                OFD.Filter = "Image Files (*.jpeg;*.jpg)|*.jpeg;*.jpg";
                OFD.CheckFileExists = true;
                OFD.CheckPathExists = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    return OFD.FileName;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error File Dialog Box", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public string BrowseImgFolder()
        {
            try
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();

                FBD.Description = "Product Picture Path";
                FBD.SelectedPath = @"C:\\";
                FBD.ShowNewFolderButton = true;
                FBD.ShowDialog();

                return FBD.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Folder Browser Box", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
    }
}
