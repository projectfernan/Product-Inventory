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
    class SKU_Upload : Excel_Conn
    {
        internal delegate void UpdateProgressDelegate(int ProgressPercentage);
        internal event UpdateProgressDelegate UpdateProgress;

        internal delegate void UpdProgMax(int ProgMax, int ProgMin);
        internal event UpdProgMax UpdProgBaxMax;

        int maxCount;
        int intCount;

        WaitCursor wc = new WaitCursor();

        public bool UploadSKU(string xlPath, string sheet ,string wbName, string UploadBy)
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

                        string desc = rs.Fields["Description"].Value.ToString();
                        string descrip = desc.Replace("  "," ");

                        if (checkSmSKU(descrip) == true)
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

                        reDelete: if (delSKUwb(wbName) == false)
                            {
                                goto reDelete;
                            }

                            MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            wc.WaitCurFalse();

                            return false;
                        }

                        string msQr = "INSERT INTO tblskurecord(SKU,UPC,Description,BcodeDesc,DeptCode,SubDeptCode,Class,SubClass,UnitRetail,VendorUPC,WorkBatchName,UploadBy)VALUES('" +
                                      rs.Fields["SM SKU"].Value + "','" +
                                      rs.Fields["SM UPC"].Value + "','" +
                                      descrip + "','" +
                                      rs.Fields["Description"].Value + "','" +
                                      rs.Fields["Dept"].Value + "','" +
                                      rs.Fields["SDept"].Value + "','" +
                                      rs.Fields["Class"].Value + "','" +
                                      rs.Fields["SClass"].Value + "'," +
                                      Convert.ToDouble(rs.Fields["Unit Retail"].Value) + ",'" +
                                      rs.Fields["Vendor UPC"].Value + "','" +
                                      wbName + "','" +
                                      UploadBy + "')";

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

                            reDelete: if (delSKUwb(wbName) == false)
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

                        intCount = intCount + 1;

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

            reDelete: if (delSKUwb(wbName) == false)
                {
                    goto reDelete;
                }

                MessageBox.Show("Upload canceled!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wc.WaitCurFalse();

                return false;
            }
        }

        public bool checkSmSKU(string smSKU)
        {
            try
            {
                DataTable dt = new DataTable();

                string Query = "select SKU from tblskurecord where SKU = '" + smSKU + "'";
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

        bool delSKUwb(string WB)
        {
            try
            {
                string DelQuery = "DELETE FROM tblskurecord WHERE WorkBatchName = '" + WB + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch
            {
                return false;
            }
        }

        public void LoadSKUWbList(ComboBox cbo, string uploadBy)
        {
            try
            {
                if (ConnMySQL() == false)
                {
                    return;
                }

                object rc;
                rs = new Recordset();
                rs = MySql.Execute("SELECT WorkBatchName FROM tblskurecord WHERE UploadBy = '" + uploadBy + "' GROUP BY WorkBatchName", out rc, (int)CommandTypeEnum.adCmdText);

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

        public long LoadSKUrecord(DataGridView dgv,string wbSKUName,string fName, string key)
        {
            try
            {
                string qr = "SELECT SKU,UPC,Description,DeptCode,SubDeptCode,Class,SubClass,FORMAT(UnitRetail,2) AS UnitRetail,VendorUPC FROM tblskurecord WHERE WorkBatchName LIKE '%" + wbSKUName + "%' AND `" + fName + "` LIKE '%" + key + "%'";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}
