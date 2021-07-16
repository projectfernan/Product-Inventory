using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjInventory
{
    public partial class frmLogin : Form
    {
        public string Username;
        public string Designation;


        internal delegate void DeleUnlockSystem(string Designate,string Uid);
        internal event DeleUnlockSystem UnlockSystem;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUid.Text == "" || txtPwd.Text == "") 
            {
                MessageBox.Show("Please input your username and password.","Login",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtUid.Focus();
                return;
            }

            SystemAcc login = new SystemAcc();
            if (login.LoginCredentials(txtUid.Text, txtPwd.Text))
            {
                if (UnlockSystem != null)
                {
                    UnlockSystem(login.Designation,login.Username);
                    this.Close();
                }
            }
            else 
            {
                txtUid.Text = "";
                txtPwd.Text = "";
                txtUid.Focus();
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
