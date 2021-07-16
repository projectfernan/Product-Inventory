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
    class BrandCode : MySQL_Conn
    {

        public long Loadtblbrand(DataGridView dgv, string fName, string key)
        {
            try
            {
                string qr = "SELECT BrandCode,BrandName,DATE_FORMAT(DateCreated,'%Y-%m-%d %T') AS DateCreated FROM tblbrand WHERE `" + fName + "` LIKE '%" + key + "%'";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool CheckBrand(string brandCode,string brandName)
        {
            try
            {
                DataTable dt = new DataTable();
                string checkQuery = "SELECT BrandCode from tblbrand where BrandCode = '" + brandCode + "' OR BrandName = '" + brandName + "'";

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

        public bool SaveBrand(string brandCode, string brandName)
        {
            try
            {
                string AddQuery = "INSERT INTO tblbrand(BrandCode,BrandName)VALUES('" + brandCode + "','" + brandName + "')";
                return MySqlQueryCUD(AddQuery);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBrand(string Brand)
        {
            try
            {
                string DelQuery = "DELETE FROM tblbrand WHERE BrandCode = '" + Brand + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch
            {
                return false;
            }
        }

        public void LoadBrandCodeCbo(ComboBox cbo)
        {
            try
            {
                string qr = "SELECT BrandName FROM tblbrand";

                DataTable dt = new DataTable();
                dt = MySqlQuery(qr);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cbo.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cbo.DataSource = dt;
                        cbo.ValueMember = "BrandName";
                        cbo.DisplayMember = "BrandName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetBrandCode(string BrandName) 
        {
            try
            {
                DataTable dt = new DataTable();
                string qr = "SELECT BrandCode FROM tblbrand WHERE BrandName = '" + BrandName + "'";

                dt = MySqlQuery(qr);

                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        return row["BrandCode"].ToString();
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
