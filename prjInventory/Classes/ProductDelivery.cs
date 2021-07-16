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
    class ProductDelivery : MySQL_Conn
    {
        public bool insDelivery(string sku, string storeCode, string dr, int qty, DateTime delDate, string DeliBatch, DateTime expDate,string createdBy) 
        {
            try
            {
                string insQr = "INSERT INTO tbldelivered(SKU,StoreCode,DRno,Qty,DeliveryDate,DeliveryBatch,CreatedBy,ExportedDate)VALUES('" +
                    sku + "','" +
                    storeCode + "','" +
                    dr + "'," +
                    qty + ",'" + 
                    delDate.ToString("yyyy-MM-dd") + "','" +
                    DeliBatch + "','" +
                    createdBy + "','" +
                    expDate.ToString("yyyy-MM-dd HH:mm") + "')";

                return MySqlQueryCUD(insQr);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        public string checkPoPendings(string createdBy)
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT PullOutBatch FROM tblpullout WHERE CreatedBy = '" + createdBy + "' AND FileStatus = 0 LIMIT 1;";

                dt = MySqlQuery(qr);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows) 
                    {
                        return row["PullOutBatch"].ToString();
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public bool checkPO() 
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT SKU FROM tblpullout limit 1;";

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

        public long LoadVweDelivered(DataGridView dgv,string createdBy,string batch ,string fName, string key,bool fresh)
        {
            try
            {
                string qr = "";

                if (fresh == false)
                {
                    qr = "SELECT DRno,SKU,Description,StoreCode,Qty,DATE_FORMAT(DeliveryDate,'%Y-%m-%d') AS DeliveryDate,DeliveryBatch,CreatedBy FROM vwe_delivered1 WHERE CreatedBy LIKE '%" + createdBy + "%' AND DeliveryBatch LIKE '%" + batch + "%' AND `" + fName + "` LIKE '%" + key + "%'";
                }
                else 
                {
                    qr = "SELECT DRno,SKU,Description,StoreCode,Qty,DATE_FORMAT(DeliveryDate,'%Y-%m-%d') AS DeliveryDate,DeliveryBatch,CreatedBy FROM vwe_delivered WHERE CreatedBy LIKE '%" + createdBy + "%' AND DeliveryBatch LIKE '%" + batch + "%' AND `" + fName + "` LIKE '%" + key + "%' AND Qty <> 0";
                }

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        

        public void LoadDelBatchCbo(ComboBox cbo,string createdBy)
        {
            try
            {
                string qr = "SELECT DeliveryBatch FROM tbldelivered WHERE CreatedBy = '" + createdBy + "' GROUP BY DeliveryBatch";

                DataTable dt = new DataTable();
                dt = MySqlQuery(qr);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cbo.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cbo.DataSource = dt;
                        cbo.ValueMember = "DeliveryBatch";
                        cbo.DisplayMember = "DeliveryBatch";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
