using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace prjInventory 
{
    class ProductPullOut : MySQL_Conn
    {
        public long LoadtoPullOut(DataGridView dgv, string createdBy, string batch)
        {
            try
            {
                string qr = "CALL get_sm_VpoList('" + createdBy + "','" + batch + "');";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool CheckStoreCode(string createdBy,string storeCode,string batch) 
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT StoreCode FROM tblpullout WHERE CreatedBy = '" + createdBy + "' AND StoreCode = '" + storeCode + "' AND PullOutBatch = '" + batch + "' AND FileStatus = 0";

                dt = MySqlQuery(qr);

                if (dt.Rows.Count > 0)
                {
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

        public bool insPullOut(string sku, string storeCode, int qty, DateTime PoDate, string PoBatch,string createdBy)
        {
            try
            {
                string insQr = "INSERT INTO tblpullout(SKU,StoreCode,Qty,PullOutDate,PullOutBatch,CreatedBy)VALUES('" +
                    sku + "','" +
                    storeCode + "'," +
                    qty + ",'" +
                    PoDate.ToString("yyyy-MM-dd") + "','" +
                    PoBatch + "','" +
                    createdBy + "')";

                return MySqlQueryCUD(insQr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool delPullOut(string sku)
        {
            try
            {
                string insQr = "DELETE FROM tblpullout WHERE SKU = '" + sku + "' AND FileStatus = 0";

                return MySqlQueryCUD(insQr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool updPullOutStat(string PoBatch, string createdBy, DateTime PoDate, int stat)
        {
            try
            {
                string insQr = "UPDATE tblpullout SET FileStatus = " + stat + 
                    ", PullOutDate = '" + PoDate.ToString("yyyy-MM-dd") + 
                    "', DateExported = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") +
                    "' WHERE PullOutBatch = '" + PoBatch + "' AND CreatedBy = '" + createdBy + "' AND FileStatus = 0";

                return MySqlQueryCUD(insQr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool CheckPOBatch(string createdBy, string batch)
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT PullOutBatch FROM tblpullout WHERE CreatedBy = '" + createdBy + "' AND PullOutBatch = '" + batch + "' LIMIT 1;";

                dt = MySqlQuery(qr);

                if (dt.Rows.Count > 0)
                {
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
    }
}
