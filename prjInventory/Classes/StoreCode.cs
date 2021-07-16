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
    class StoreCode : MySQL_Conn
    {
        public long Loadtblstore(DataGridView dgv, string fName, string key)
        {
            try
            {
                string qr = "SELECT StoreCode,StoreName,DATE_FORMAT(DateCreated,'%Y-%m-%d %T') AS DateCreated FROM tblstore WHERE `" + fName + "` LIKE '%" + key + "%'";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool CheckStore(string storeCode, string storeName)
        {
            try
            {
                DataTable dt = new DataTable();
                string checkQuery = "SELECT StoreCode from tblstore where StoreCode = '" + storeCode + "' OR StoreName = '" + storeName + "'";

                dt = MySqlQuery(checkQuery);

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

        public bool SaveStore(string storeCode, string storeName)
        {
            try
            {
                string AddQuery = "INSERT INTO tblstore(StoreCode,StoreName)VALUES('" + storeCode + "','" + storeName + "')";
                return MySqlQueryCUD(AddQuery);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteStore(string storeCode)
        {
            try
            {
                string DelQuery = "DELETE FROM tblstore WHERE StoreCode = '" + storeCode + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch
            {
                return false;
            }
        }

        public void LoadStoreCodeCbo(ComboBox cbo)
        {
            try
            {
                string qr = "SELECT StoreName FROM tblstore";

                DataTable dt = new DataTable();
                dt = MySqlQuery(qr);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cbo.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cbo.DataSource = dt;
                        cbo.ValueMember = "StoreName";
                        cbo.DisplayMember = "StoreName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetStoreCode(string storeName)
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT StoreCode FROM tblstore WHERE StoreName = '" + storeName + "'";

                dt = MySqlQuery(qr);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return row["StoreCode"].ToString();
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
    }
}
