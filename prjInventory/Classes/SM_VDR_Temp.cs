using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADODB;
using System.Data;
using System.IO;

namespace prjInventory
{
    class SM_VDR_Temp : Excel_Conn
    {
        internal delegate void UpdateProgressDelegate(int ProgressPercentage);
        internal event UpdateProgressDelegate UpdateProgress;

        internal delegate void UpdProgMax(int ProgMax, int ProgMin);
        internal event UpdProgMax UpdProgBaxMax;

        int maxCount;
        int intCount;

        WaitCursor wc = new WaitCursor();

        public bool VdrBcodeBatchList(ComboBox cbo, string uplBy)
        {
            try
            {
                DataTable dt = new DataTable();
                cbo.Items.Clear();

                string Query = "SELECT BarcodeBatch FROM tblinventory WHERE UploadBy = '" + uplBy + "' AND FileStatus = 1 GROUP BY BarcodeBatch";
                dt = MySqlQuery(Query);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cbo.Items.Add(row["BarcodeBatch"].ToString());
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public long LoadVdrExpList(DataGridView dgv, string UplBy, string BbName)
        {
            try
            {
                string qr = "call get_sm_vdr('" + UplBy + "','" + BbName + "');";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool expExlSmVDRTemp(string bbName, string Uplby, string DRno, string storeCode, DateTime expDelivery, string deliBatch,string fileName)
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = fileName;
                sfd.Filter = "Excel files (*.xlsx,*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    WaitCursor wc = new WaitCursor();
                    wc.WaitCurTrue();

                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook wb = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range cells = wb.Worksheets[1].Cells;
                    cells.NumberFormat = "@";

                    ws = wb.Sheets["Sheet1"];
                    ws = wb.ActiveSheet;

                    ProductDelivery delv = new ProductDelivery();
                    DataTable dt = new DataTable();


                    string getQr = "call get_sm_vdr('" + Uplby + "','" + bbName + "');";

                    dt = MySqlQuery(getQr);

                    if (dt.Rows.Count > 0)
                    {
                        this.maxCount = dt.Rows.Count;
                        this.intCount = 0;

                        UpdProgBaxMax(maxCount, intCount);

                        ws.Cells[1, 1] = "DR No.";
                        ws.Cells[1, 2] = "Store Code";
                        ws.Cells[1, 3] = "Expected Delivery Date";
                        ws.Cells[1, 4] = "Dept. Code";
                        ws.Cells[1, 5] = "Sub-Dept Code";
                        ws.Cells[1, 6] = "Class Code";
                        ws.Cells[1, 7] = "SKU";
                        ws.Cells[1, 8] = "QTY";

                        int lup = 2;
                        int fild = 0;
                        do
                        {
                            ws.Cells[lup, 1] = DRno; //dt.Rows[lup][].ToString();
                            ws.Cells[lup, 2] = storeCode;
                            ws.Cells[lup, 3] = expDelivery.ToString("Mddyy");
                            ws.Cells[lup, 4] = dt.Rows[fild]["DeptCode"].ToString();
                            ws.Cells[lup, 5] = dt.Rows[fild]["SubDeptCode"].ToString();
                            ws.Cells[lup, 6] = dt.Rows[fild]["ClassCode"].ToString();
                            ws.Cells[lup, 7] = dt.Rows[fild]["SKU"].ToString();
                            ws.Cells[lup, 8] = dt.Rows[fild]["Qty"].ToString();

reins:                      if (delv.insDelivery(dt.Rows[fild]["SKU"].ToString(), storeCode, DRno, Convert.ToInt32(dt.Rows[fild]["Qty"].ToString()), expDelivery, deliBatch, DateTime.Now,Uplby) == false) 
                            {
                                goto reins;
                            }

                            lup++;
                            fild++;

                            intCount++;
                            UpdateProgress(intCount);
                            Application.DoEvents();

                        } while (fild != dt.Rows.Count);

                        wb.SaveAs(sfd.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                           Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                           Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        app.Quit();

                        wc.WaitCurFalse();

                        MessageBox.Show("Exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wc.WaitCurFalse();
                return false;
            }
        }
    }
}
