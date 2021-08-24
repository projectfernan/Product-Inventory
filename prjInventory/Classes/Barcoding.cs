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
    class Barcoding : Excel_Conn
    {
        internal delegate void UpdateProgressDelegate(int ProgressPercentage);
        internal event UpdateProgressDelegate UpdateProgress;

        internal delegate void UpdProgMax(int ProgMax, int ProgMin);
        internal event UpdProgMax UpdProgBaxMax;

        int maxCount;
        int intCount;

        int deptCodeLen;
        int SdeptCodeLen;

        WaitCursor wc = new WaitCursor();

        public bool UploadInv(string xlPath, string sheet, string wbName, string UploadBy)
        {
            try
            {
                if (excelConn(xlPath) == false)
                {
                    MessageBox.Show("Failed to connect to excel file!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                string qr = "select * from [" + sheet + "$]";
                Recordset rs = new Recordset();

                rs = ExcelQuery(qr);

                if (rs.EOF == false)
                {
                    this.maxCount = rs.RecordCount;
                    this.intCount = 0;

                    UpdProgBaxMax(maxCount, intCount);

                    wc.WaitCurTrue();

                    do
                    {
                    gototest:

                        string desc = rs.Fields["desc"].Value.ToString();
                        string descrip = desc.Replace("-"," ").Replace("/"," ");

                        if (chkInvSKU(descrip) == false)
                        {
                            MessageBox.Show("Description '" + descrip + "' doesn't have an SKU and UPC code yet!","Upload",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            MessageBox.Show("The operation cannot be continued.", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            if (intCount == 0)
                            {
                                return false;
                            }
                            else 
                            {
                                wc.WaitCurFalse();
                            }

                            do
                            {
                                intCount = intCount - 1;
                                UpdateProgress(intCount);
                                Application.DoEvents();
                            } while (intCount != 0);

                            reDelete: if (delInvWb(wbName) == false)
                            {
                                goto reDelete;
                            }

                            MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            wc.WaitCurFalse();

                            return false;
                        }

                        string msQr = "INSERT INTO tblinventory(Description,Qty,Price,Supplier,BarType,NoPcs,ProdId,SalePrice,Cost,NoOrder,Conversion,BarcodeBatch,UploadBy,FileStatus)VALUES('" +
                                      descrip + "','" +
                                      rs.Fields["qty"].Value + "','" +
                                      rs.Fields["price"].Value + "','" +
                                      rs.Fields["supplier"].Value + "','" +
                                      Convert.ToInt32(rs.Fields["bartype"].Value) + "','" +
                                      Convert.ToInt32(rs.Fields["nopcs"].Value) + "','" +
                                      Convert.ToInt32(rs.Fields["prodid"].Value) + "','" +
                                      Convert.ToDouble(rs.Fields["saleprice"].Value) + "'," +
                                      Convert.ToDouble(rs.Fields["cost"].Value) + ",'" +
                                      Convert.ToInt32(rs.Fields["norder"].Value) + "','" +
                                      Convert.ToInt32(rs.Fields["conversion"].Value) + "','" +
                                      wbName + "','" +
                                      UploadBy + "',0)";

                        if (MySqlQueryCUD(msQr) == false)
                        {
                            DialogResult s = MessageBox.Show("Operation failed! Try again?", "Upload", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                            if (s != DialogResult.Retry)
                            {
                                do
                                {
                                    intCount = intCount - 1;
                                    UpdateProgress(intCount);
                                    Application.DoEvents();
                                } while (intCount != 0);

reDelete:                       if (delInvWb(wbName) == false)
                                {
                                    goto reDelete;
                                }

                                MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                wc.WaitCurFalse();

                                return false;
                            }
                            else
                            {
                                goto gototest;
                            }
                        }

                        rs.MoveNext();

                        intCount++;

                        UpdateProgress(intCount);

                        Application.DoEvents();

                    } while (rs.EOF == false);

                    wc.WaitCurFalse();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Upload", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (intCount == 0) return false;

                do
                {
                    intCount = intCount - 1;
                    UpdateProgress(intCount);
                    Application.DoEvents();
                } while (intCount != 0);

                reDelete: if (delInvWb(wbName) == false)
                {
                    goto reDelete;
                }

                MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                wc.WaitCurFalse();

                return false;
            }
        }

        public string checkBBpending(string SysUid)
        {
            try
            {
                DataTable dt = new DataTable();
                string wbName = "";

                string Query = "select BarcodeBatch from tblinventory where UploadBy = '" + SysUid + "' and FileStatus = 0 limit 1";
                dt = MySqlQuery(Query);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        wbName = row["BarcodeBatch"].ToString();
                    }
                    return wbName;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        void GetDeptLen(string WB, string Uid) 
        {
            DataTable dt = new DataTable();
            string qr = "SELECT DeptCode,SubDeptCode FROM tblskurecord WHERE UploadBy = '" + Uid + "'limit 1;";

            string dCode = "";
            string sDcode = "";

            dt = MySqlQuery(qr);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dCode = row["DeptCode"].ToString();
                    sDcode = row["SubDeptCode"].ToString();

                    this.deptCodeLen = dCode.Length;
                    this.SdeptCodeLen = sDcode.Length;
                }
            }
        }

        string makeZero(int len) 
        {
            int no0 = 3 - len;
            int lup = 1;
            string zero = "";

            for (lup = 1; lup <= no0; lup++) 
            {
                zero += "0";
            }
            return zero;
        }

        public long LoadtblInventory(DataGridView dgv, string WB, string Uid)
        {
            try
            {
                GetDeptLen(WB, Uid);

                string deptc0 = makeZero(deptCodeLen);
                string sdeptc0 = makeZero(SdeptCodeLen);

                string qr = "call get_barcode_reg('" + deptc0 + "','" + sdeptc0 + "','" + WB + "','" + Uid + "');";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        bool chkInvSKU(string desc) 
        {
            try
            {
                DataTable dt = new DataTable();

                string qr = "SELECT SKU FROM tblskurecord WHERE Description = '" + desc + "'";

                dt = MySqlQuery(qr);

                if(dt.Rows.Count > 0)
                {
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

        bool delInvWb(string WB)
        {
            try
            {
                string DelQuery = "DELETE FROM tblinventory WHERE BarcodeBatch = '" + WB + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch
            {
                return false;
            }
        }

        public bool expExcelBcodeReg(string bbName, string Uplby, string vendor,string season,string FileName)
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = FileName;
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
                    

                    DataTable dt = new DataTable();

                    GetDeptLen(bbName, Uplby);

                    string deptc0 = makeZero(deptCodeLen);
                    string sdeptc0 = makeZero(SdeptCodeLen);

                    string getQr = "call get_barcode_reg('" + deptc0 + "','" + sdeptc0 + "','" + bbName + "','" + Uplby + "');";

                    dt = MySqlQuery(getQr);

                    if (dt.Rows.Count > 0)
                    {
                        this.maxCount = dt.Rows.Count;
                        this.intCount = 0;

                        UpdProgBaxMax(maxCount, intCount);

                        ws.Cells[1, 1] = "ID";
                        ws.Cells[1, 2] = "VENDOR";
                        ws.Cells[1, 3] = "SEASON";
                        ws.Cells[1, 4] = "DEPT";
                        ws.Cells[1, 5] = "CLASS";
                        ws.Cells[1, 6] = "SUBCLASS";
                        ws.Cells[1, 7] = "SKU";
                        ws.Cells[1, 8] = "DESCRIPTION";
                        ws.Cells[1, 9] = "PRICE";
                        ws.Cells[1, 10] = "UOM";
                        ws.Cells[1, 11] = "QTY";

                        int lup = 2;
                        int fild = 0;
                        int id = 1;
                        do
                        {
                            ws.Cells[lup, 1] = id.ToString(); //dt.Rows[lup][].ToString();
                            ws.Cells[lup, 2] = vendor;
                            ws.Cells[lup, 3] = season;
                            ws.Cells[lup, 4] = dt.Rows[fild]["Dept"].ToString();
                            ws.Cells[lup, 5] = dt.Rows[fild]["Class"].ToString();
                            ws.Cells[lup, 6] = dt.Rows[fild]["SubClass"].ToString();
                            ws.Cells[lup, 7] = dt.Rows[fild]["SKU"].ToString();
                            ws.Cells[lup, 8] = dt.Rows[fild]["Description"].ToString();
                            ws.Cells[lup, 9] = dt.Rows[fild]["Price"].ToString();
                            ws.Cells[lup, 10] = dt.Rows[fild]["UOM"].ToString();
                            ws.Cells[lup, 11] = dt.Rows[fild]["Qty"].ToString(); ;

                            lup++;
                            fild++;
                            id++;

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

        public bool UpdInvFileStat(string uplBy,string bbName, int stat)
        {
            try
            {
                string Query = "UPDATE tblinventory SET FileStatus = " + stat + ",DateUpdated = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE BarcodeBatch = '" + bbName + "' and UploadBy = '" + uplBy + "'";

                return MySqlQueryCUD(Query);
            }
            catch
            {
                return false;
            }
        }
    }
}
