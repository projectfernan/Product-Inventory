using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADODB;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace prjInventory
{
    class MySQL_Conn
    {
        public Connection MySql = new Connection();
        public Recordset rs = new Recordset();

        public string Host;
        public string Uid;
        public string Pwd;
        public string Port;
        public string DbName;

        public void SetMySQL(string _Host,string _Uid,string _Pwd,string _Port,string _DbName) 
        {
            try
            {
                this.Host = _Host;
                this.Uid = _Uid;
                this.Pwd = _Pwd;
                this.Port = _Port;
                this.DbName = _DbName;
            }
            catch 
            {
                this.Host = "localhost";
                this.Uid = "root";
                this.Pwd = "pr0J3ctF";
                this.Port = "3306";
                this.DbName = "cmbinvdb";
            }
        }

        public bool ConnMySQL()
        {
            try
            {
                SetMySQL(Properties.Settings.Default.dbHost1, Properties.Settings.Default.dbUid1, Properties.Settings.Default.dbPwd1, Properties.Settings.Default.dbPort1, Properties.Settings.Default.dbName1);

                MakePing p = new MakePing();
                if (p.PingIp(Host) == false)
                {
                    return false;
                }

                if (MySql.State == 1)
                {
                    MySql.Close();
                }

                MySql = new Connection();
                MySql.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                MySql.Open("Driver={MySQL ODBC 8.0 Unicode Driver}; "
                                          + "Server=" + Host + ";"
                                          + "Port=" + Port +";"
                                          + "Option=3;"
                                          + "Database=" + DbName + ";"
                                          + "UID=" + Uid + ";"
                                          + "PWD=" + Pwd + ";character set=utf8;");
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool ConnMySqlOnly()
        {
            try
            {
                SetMySQL(Properties.Settings.Default.dbHost1, Properties.Settings.Default.dbUid1, Properties.Settings.Default.dbPwd1, Properties.Settings.Default.dbPort1, Properties.Settings.Default.dbName1);

                MakePing p = new MakePing();
                if (p.PingIp(Host) == false)
                {
                    return false;
                }

                if (MySql.State == 1)
                {
                    MySql.Close();
                }

                MySql = new Connection();
                MySql.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                MySql.Open("Driver={MySQL ODBC 8.0 Unicode Driver}; "
                                          + "Server=" + Host + ";"
                                          + "Port=" + Port + ";"
                                          + "Option=3;"
                                          + "UID=" + Uid + ";"
                                          + "PWD=" + Pwd + ";");
                return true;

            }
            catch
            {
                return false;
            }
        }

        public void LoadBbList(ComboBox cbo) 
        {
            try
            {
                if (ConnMySqlOnly() == false) 
                {
                    return;
                }

                object rc;
                rs = new Recordset();
                rs = MySql.Execute("SHOW DATABASES", out rc, (int)CommandTypeEnum.adCmdText);

                if (rs.EOF == false) 
                {
                    do
                    {
                        cbo.Items.Add(rs.Fields["Database"].Value);
                        rs.MoveNext();
                    } while (rs.EOF == false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable MySqlQuery(string strQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter();

                if (ConnMySQL() == false)
                {
                    return dt;
                }

                object rc;
                rs = new Recordset();
                rs = MySql.Execute(strQuery, out rc, (int)CommandTypeEnum.adCmdText);

                adapter.Fill(dt, rs);

                return dt;

                //rs.Open(strQuery, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message From Datatable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
        }

        public long LoadToDgv(DataGridView dgv, string myquery, bool hdAutoRez)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = MySqlQuery(myquery);

                if (dt != null)
                {
                    dgv.DataSource = dt;
                }

                int hdCnt = dgv.ColumnCount;
                int hdLup = 0;
                int hdW = (dgv.Width - dgv.RowHeadersWidth) / hdCnt;

                do
                {
                    dgv.Columns[hdLup].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);

                    if (hdAutoRez)
                    {
                        dgv.Columns[hdLup].Width = hdW;
                    }

                    hdLup++;
                }
                while (hdLup != hdCnt);

                dgv.CurrentCell = null;

                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool MySqlQueryCUD(string strQuery)
        {
            try
            {
                if (ConnMySQL() == false)
                {
                    return false;
                }

                object rc;
                rs = new Recordset();
                rs = MySql.Execute(strQuery, out rc, (int)CommandTypeEnum.adCmdText);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
