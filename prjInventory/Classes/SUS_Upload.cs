using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADODB;
using System.Data;
using System.IO;
using System.Drawing;

namespace prjInventory
{
    class SUS_Upload : Excel_Conn
    {
        internal delegate void UpdateProgressDelegate(int ProgressPercentage);
        internal event UpdateProgressDelegate UpdateProgress;

        internal delegate void UpdProgMax(int ProgMax,int ProgMin);
        internal event UpdProgMax UpdProgBaxMax;

        public int maxCount;
        public int intCount;

        WaitCursor wc = new WaitCursor();

        public bool UploadSUS(string xlPath, string sheetName,string wbName, string UploadBy)
        {
            try
            {
                if (excelConn(xlPath) == false)
                {
                    MessageBox.Show("Failed to connect to excel file!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                string qr = "select * from [" + sheetName + "$]";
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
                        string descrip = desc.Replace("-", " ").Replace("/", " ").Replace("."," ");

                        if (checkDesc(descrip) == true)
                        {
                            MessageBox.Show("It seems the item '" + descrip + "' already exists and cannot be duplicated! The upload operation can't continue!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            if (intCount <= 0) 
                            {
                                wc.WaitCurFalse();
                                return false;
                            }

                            do
                            {
                                intCount = intCount - 1;
                                UpdateProgress(intCount);
                                Application.DoEvents();
                            } while (intCount != 0);

                        reDelete: if (delWB(wbName) == false)
                            {
                                goto reDelete;
                            }

                            MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            wc.WaitCurFalse();

                            return false;
                        }

                        string msQr = "INSERT INTO tblproducts(Barcode,ItemCode,Description,Color,Size,Brand,Category,oSRP,SRP,UOM,`Group`,Sole,`Upper`,Style,Batch,Material,ItemName,fSRP,ItemStatus,MkdnStatus,MovingStatus,WorkBatchName,UploadBy,UpdatedBy,FileStatus,PicName)VALUES('" +
                                      rs.Fields["barcode"].Value + "','" +
                                      rs.Fields["itemcode"].Value + "','" +
                                      descrip + "','" +
                                      rs.Fields["colord"].Value + "','" +
                                      rs.Fields["sized"].Value + "','" +
                                      rs.Fields["brandd"].Value + "','" +
                                      rs.Fields["categoryd"].Value + "'," +
                                      Convert.ToDouble(rs.Fields["deptd"].Value) + "," +
                                      Convert.ToDouble(rs.Fields["sell"].Value) + ",'" +
                                      rs.Fields["unitd"].Value + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "'," +
                                      0 + ",'" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      wbName + "','" +
                                      UploadBy + "','" +
                                      UploadBy + "'," +
                                      0 + ",'" +
                                      rs.Fields["desc"].Value + "')";

                        if (MySqlQueryCUD(msQr) == false)
                        {
                            DialogResult s = MessageBox.Show("Operation failed! Try again?", "Upload", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                            if (s != DialogResult.Retry)
                            {
                                if (intCount <= 0)
                                {
                                    wc.WaitCurFalse();
                                    return false;
                                }

                                do
                                {
                                    intCount = intCount - 1;
                                    UpdateProgress(intCount);
                                    Application.DoEvents();
                                } while (intCount != 0);

                            reDelete: if (delWB(wbName) == false)
                                {
                                    goto reDelete;
                                }

                                MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                if (intCount <= 0) 
                {
                    wc.WaitCurFalse();
                    return false;
                }

                do
                {
                    intCount = intCount - 1;
                    UpdateProgress(intCount);
                    Application.DoEvents();
                } while (intCount != 0);

            reDelete: if (delWB(wbName) == false)
                {
                    goto reDelete;
                }

                MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wc.WaitCurFalse();

                return false;
            }
        }

        public bool AddProduct(string barcode,string itemcode,string descrip,string color,string size,string brand,string category,double oSRP,double SRP,string UOM,string wbName,string UploadBy) 
        {
            try
            {
                string description = descrip.Replace("-", " ").Replace("/", " ").Replace(".", " "); ;

                string msQr = "INSERT INTO tblproducts(Barcode,ItemCode,Description,Color,Size,Brand,`Group`,Category,oSRP,SRP,UOM,Sole,`Upper`,Style,Batch,Material,ItemName,fSRP,ItemStatus,MkdnStatus,MovingStatus,WorkBatchName,UploadBy,UpdatedBy,FileStatus,PicName)VALUES('" +
                                      barcode + "','" +
                                      itemcode + "','" +
                                      description + "','" +
                                      color + "','" +
                                      size + "','" +
                                      brand + "','" +
                                      "" + "','" +
                                      category + "'," +
                                      oSRP + "," +
                                      SRP + ",'" +
                                      UOM + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "'," +
                                      0 + ",'" +
                                      "" + "','" +
                                      "" + "','" +
                                      "" + "','" +
                                      wbName + "','" +
                                      UploadBy + "','" +
                                      UploadBy + "'," +
                                      0 + ",'" +
                                      descrip + "')";

                return MySqlQueryCUD(msQr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Add", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public long LoadtblProducts(DataGridView dgv,string WB,string Uid ,string fName, string key)
        {
            try
            {
                string qr = "SELECT Barcode,ItemCode,Description,Color,Size,Brand,Category,oSRP,SRP,UOM FROM tblproducts WHERE WorkBatchName = '" +WB + "' AND UpdatedBy = '" + Uid + "' AND `" + fName + "` LIKE '%" + key + "%'";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public long LoadMasterPdt(DataGridView dgv, string WB, string Uid, string fName, string key)
        {
            try
            {
                string slect = "SELECT Barcode,ItemCode,Description,Color,Size,Brand,Category,oSRP,SRP,UOM,ItemName,Batch,`Group`,Sole,`Upper`,Style,Material,fSRP,ItemStatus,MkdnStatus,MovingStatus,SKU,UPC,WorkBatchName,UploadBy,UpdatedBy,DateCreated,DateUpdated FROM vwe_master_pdt WHERE UpdatedBy LIKE '%" + Uid + "%' AND WorkBatchName LIKE '%" + WB + "%' AND `" + fName + "` LIKE '%" + key + "%'";

                //string qr = "CALL get_pdt_master('" + Uid + "','" + WB + "','" + fName + "','" + key + "');";

                return LoadToDgv(dgv, slect, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        bool delWB(string WB) 
        {
            try
            {
                string DelQuery = "delete from tblproducts where WorkBatchName = '" + WB + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch 
            {
                return false;
            }
        }

        public bool checkDesc(string descr)
        {
            try
            {
                DataTable dt = new DataTable();

                string Query = "select Description from tblproducts where Description = '" + descr + "'";
                dt = MySqlQuery(Query);

                if (dt.Rows.Count > 0)
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

        public bool checkWbName(string Wb, string SysAcc) 
        {
            try
            {
                DataTable dt = new DataTable();

                string Query = "select WorkBatchName from tblproducts where WorkBatchName = '" + Wb + "' and UpdatedBy = '" + SysAcc + "'";
                dt = MySqlQuery(Query);

                if (dt.Rows.Count > 0)
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

        public string checkWbpending(string SysUid)
        {
            try
            {
                DataTable dt = new DataTable();
                string wbName = "";

                string Query = "select WorkBatchName from tblproducts where UpdatedBy = '" + SysUid + "' and FileStatus = 0 limit 1";
                dt = MySqlQuery(Query);

                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        wbName = row["WorkBatchName"].ToString();
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

        public bool DeleteItem(string itemDesc)
        {
            try
            {
                string Query = "DELETE from tblproducts where Description = '" + itemDesc + "'";
                
                return MySqlQueryCUD(Query);
            }
            catch
            {
                return false;
            }
        }

        public bool expExcelSmTemp(string wbName,string firstField,string vendorCode,string deptCode,string subDeptCode,string brandCode,string stockStyleCode, string socmaBarcode,string fourteenField,string fifteenField, string DateCreated,string DateSaved,string FileName)
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

                    string getQr = "SELECT Brand,Description,Color,Size,oSRP,Category FROM tblproducts WHERE WorkBatchName = '" + wbName + "' and FileStatus = 0";

                    dt = MySqlQuery(getQr);

                    if (dt.Rows.Count > 0)
                    {
                        ws.Cells[1, 1] = "";
                        ws.Cells[1, 2] = "VENDOR";
                        ws.Cells[1, 3] = "DEPT";
                        ws.Cells[1, 4] = "SUB DEPT";
                        ws.Cells[1, 5] = "BRAND";
                        ws.Cells[1, 6] = "#";
                        ws.Cells[1, 7] = "BRAND NAME";
                        ws.Cells[1, 8] = "GENERIC DESC";
                        ws.Cells[1, 9] = "COLOR";
                        ws.Cells[1, 10] = "SIZE";
                        ws.Cells[1, 11] = "STOCK/STYLE CODE";
                        ws.Cells[1, 12] = "SOURCEMARKED BARCODE";
                        ws.Cells[1, 13] = "RETAIL PRICE";
                        ws.Cells[1, 14] = "";
                        ws.Cells[1, 15] = "";
                        ws.Cells[1, 16] = "DATE CREATED";
                        ws.Cells[1, 17] = "DATE SAVED";

                        int lup = 2;
                        int fild = 0;

                        do
                        {
                            ws.Cells[lup, 1] = firstField; //dt.Rows[lup][].ToString();
                            ws.Cells[lup, 2] = vendorCode;
                            ws.Cells[lup, 3] = deptCode;
                            ws.Cells[lup, 4] = subDeptCode;
                            ws.Cells[lup, 5] = brandCode;
                            ws.Cells[lup, 6] = dt.Rows[fild]["Category"].ToString();
                            ws.Cells[lup, 7] = dt.Rows[fild]["Brand"].ToString();
                            ws.Cells[lup, 8] = dt.Rows[fild]["Description"].ToString();
                            ws.Cells[lup, 9] = dt.Rows[fild]["Color"].ToString();
                            ws.Cells[lup, 10] = dt.Rows[fild]["Size"].ToString();
                            ws.Cells[lup, 11] = stockStyleCode;
                            ws.Cells[lup, 12] = socmaBarcode;
                            ws.Cells[lup, 13] = dt.Rows[fild]["oSRP"].ToString();
                            ws.Cells[lup, 14] = fourteenField;
                            ws.Cells[lup, 15] = fifteenField;
                            ws.Cells[lup, 16] = DateCreated;
                            ws.Cells[lup, 17] = DateSaved;

                            lup++;
                            fild++;
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

        public bool UpdateFileStat(string uplBy,string WbName, int stat)
        {
            try
            {
                string Query = "UPDATE tblproducts SET FileStatus = " + stat + " WHERE WorkBatchName = '" + WbName + "' AND UpdatedBy = '" + uplBy + "'";

                return MySqlQueryCUD(Query);
            }
            catch
            {
                return false;
            }
        } 

        public void LoadWbList(ComboBox cbo, string uploadBy)
        {
            try
            {
                if (ConnMySQL() == false)
                {
                    return;
                }

                cbo.Items.Clear();

                object rc;
                rs = new Recordset();
                rs = MySql.Execute("SELECT WorkBatchName FROM tblproducts WHERE UpdatedBy = '" + uploadBy + "' AND FileStatus = 1 GROUP BY WorkBatchName", out rc, (int)CommandTypeEnum.adCmdText);

                if (rs.EOF == false)
                {
                    do
                    {
                        cbo.Items.Add(rs.Fields["WorkBatchName"].Value);
                        rs.MoveNext();
                    } while (rs.EOF == false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPdtWbList(ComboBox cbo, string uploadBy)
        {
            try
            {
                if (ConnMySQL() == false)
                {
                    return;
                }

                cbo.Items.Clear();

                object rc;
                rs = new Recordset();
                rs = MySql.Execute("SELECT WorkBatchName FROM tblproducts WHERE UpdatedBy = '" + uploadBy + "' GROUP BY WorkBatchName", out rc, (int)CommandTypeEnum.adCmdText);

                if (rs.EOF == false)
                {
                    do
                    {
                        cbo.Items.Add(rs.Fields["WorkBatchName"].Value);
                        rs.MoveNext();
                    } while (rs.EOF == false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool UpdatePdt(string desc,string itemName, string group, string batch, string mats, string upper, string sole, string style, string mkdnStat, string moveStat, string itemStat , double SRP, double fSRP,string UpdBy,string itemCode)
        {
            try
            {
                string msQr = "UPDATE tblproducts SET ItemName = '" + itemName + "'," + 
                                                      "`Group` = '" + group + "'," +
                                                      "`Batch` = '" + batch + "'," +
                                                      "`Material` = '" + mats + "'," +
                                                      "`Upper` = '" + upper + "'," +
                                                      "`sole` = '" + sole + "'," +
                                                      "`style` = '" + style + "'," +
                                                      "`MkdnStatus` = '" + mkdnStat + "'," +
                                                      "`MovingStatus` = '" + moveStat + "'," +
                                                      "`ItemStatus` = '" + itemStat + "'," +
                                                      "`SRP` = " + SRP + "," +
                                                      "`fSRP` = " + fSRP + "," +
                                                      "`DateUpdated` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                      "`UpdatedBy` = '" + UpdBy + "' WHERE ItemCode = '" + itemCode + "'";

                return MySqlQueryCUD(msQr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool DeletePdt(string itemDesc)
        {
            try
            {
                string Query = "CALL del_master_pdt('" + itemDesc + "')";

                return MySqlQueryCUD(Query);
            }
            catch
            {
                return false;
            }
        }
    }
}
