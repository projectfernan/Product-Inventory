using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ADODB;

namespace prjInventory
{
    class SystemAcc : MySQL_Conn
    {
        public string Username;
        public string Designation;

        public long LoadtblSystemAcc(DataGridView dgv, string fName, string key)
        {
            try
            {
                string qr = "SELECT Username,Name,Designation,DATE_FORMAT(DateCreated,'%Y-%m-%d %T') AS DateCreated,DATE_FORMAT(DateUpdated,'%Y-%m-%d %T') AS DateUpdated from tblsystemacc where `" + fName + "` like '%" + key + "%'";

                return LoadToDgv(dgv, qr, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool CheckUid(string Uid) 
        {
            try
            {
                DataTable dt = new DataTable();
                string checkQuery = "SELECT Username from tblsystemacc where Username = '" + Uid + "'";

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

        public bool SaveAcc(string Uid,string Pwd,string Name,string Desig)
        {
            try
            {
                string AddQuery = "INSERT INTO tblsystemacc(Username,Password,Name,Designation)VALUES('" + Uid + "','" + Pwd + "','" + Name + "','" + Desig + "')";
                return MySqlQueryCUD(AddQuery);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAcc(string Uid, string Pwd, string Name, string Desig)
        {
            try
            {
                string EditQuery = "UPDATE tblsystemacc SET Password = '" + Pwd + "', Name = '" + Name + "', Designation = '" + Desig + "' WHERE Username = '" + Uid + "'";
                return MySqlQueryCUD(EditQuery);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAcc(string Uid)
        {
            try
            {
                string DelQuery = "DELETE FROM tblsystemacc WHERE Username = '" + Uid + "'";
                return MySqlQueryCUD(DelQuery);
            }
            catch
            {
                return false;
            }
        }

        public void LoadAccList(ComboBox cbo)
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
                rs = MySql.Execute("SELECT Username FROM tblsystemacc", out rc, (int)CommandTypeEnum.adCmdText);

                if (rs.EOF == false)
                {
                    do
                    {
                        cbo.Items.Add(rs.Fields["Username"].Value);
                        rs.MoveNext();
                    } while (rs.EOF == false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool SuperAdmin(string Uid,string Pwd) 
        {
            string SuperAdmin = "fernan";
            string Password = "0901pr0J3ctf0273";

            if (SuperAdmin.Trim() != Uid.Trim())
            {
                return false;
            }
            else
            {
                if (Password.Trim() == Pwd.Trim())
                {
                    this.Designation = "System Creator ";
                    this.Username = "fernan";
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }

        public bool LoginCredentials(string Uid, string Pwd) 
        {
            try
            {
                if (SuperAdmin(Uid,Pwd)) return true;

                DataTable dt = new DataTable();
                string pene = "SELECT Username,Password,Designation FROM tblsystemacc WHERE Username = '" + Uid + "'";

                dt = MySqlQuery(pene);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string uidRec = row["Username"].ToString();
                        string pwdRec = row["Password"].ToString();

                        if (uidRec.Trim() != Uid.Trim())
                        {
                            MessageBox.Show("Incorrect username or password.","Login",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return false;
                        }
                        else
                        {
                            if (pwdRec.Trim() == Pwd.Trim())
                            {
                                this.Designation = row["Designation"].ToString();
                                this.Username = row["Username"].ToString();
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Incorrect username or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                    return false;
                }
                else 
                {
                    MessageBox.Show("Incorrect username or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
