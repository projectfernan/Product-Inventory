using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace prjInventory
{
    class FileCopy
    {
        public bool CopyImage(string source,string fileName) 
        {
            try
            {
                if (source == "" || source == null) return true;

                string desti = Properties.Settings.Default.PdtImgPath + @"\" + fileName + ".jpeg";  //@"\" + Properties.Settings.Default.PdtImgField + ".jpeg";

                if (File.Exists(desti)) 
                {
                    File.Delete(desti);
                }

                File.Copy(source, desti);

                return true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error Image",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }


    }
}
