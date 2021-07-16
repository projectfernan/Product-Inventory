using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADODB;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace prjInventory
{
    class Excel_Conn : MySQL_Conn
    {
        public Connection ConnExcel = new Connection();
        public Recordset ShRs = new Recordset();

        string excelPath;

        public void setExcelPath(string exlPath) 
        {
            try
            {
                this.excelPath = exlPath;
            }
            catch 
            {
            
            }
        }

        public bool excelConn(string exPath)
        {
            try
            {
                string PathCon = "";

                if (File.Exists(exPath))
                {
                    setExcelPath(exPath);

                    if (exPath.Contains(".xlsx"))
                    {
                        PathCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exPath + ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1\";";
                    }
                    else 
                    {
                        PathCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exPath + ";Extended Properties=\"Excel 8.0; HDR=YES; IMEX=1\";";
                    }


                    if (ConnExcel.State == 1)
                    {
                        ConnExcel.Close();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ConnExcel);
                    }

                    ConnExcel = new Connection();
                    ConnExcel.CursorLocation = CursorLocationEnum.adUseClient;
                    ConnExcel.Open(PathCon);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public Recordset ExcelQuery(string strQuery)
        {
            try
            {
                object rc;
                ShRs = new Recordset();
                ShRs = ConnExcel.Execute(strQuery, out rc, (int)CommandTypeEnum.adCmdText);

                return ShRs;
                //rs.Open(strQuery, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Excel Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ShRs;
            }
        }

        public bool GetSheetName(string exlPath,ComboBox cbo) 
        {
            try
            {
             
                _Application xlsApp = new Microsoft.Office.Interop.Excel.Application();


                Workbook book = xlsApp.Workbooks.Open(exlPath, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                foreach (Worksheet sheet in book.Worksheets)
                {
                    cbo.Items.Add(sheet.Name);
                }
                xlsApp.Workbooks.Close();
                xlsApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Get Sheet Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
