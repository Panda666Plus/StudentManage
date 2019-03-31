using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StudentManage;
using Models;
using DAL;
namespace StudentManager
{
    public partial class FrmModifyPwd : Form
    {
        private AdminService objAdminService = new AdminService();
        public FrmModifyPwd()
        {
            InitializeComponent();
        }
        //修改密码
        private void btnModify_Click(object sender, EventArgs e)
        {
            //验证
            if (this.txtOldPwd.Text.Trim().Length== 0)
            {
                MessageBox.Show("请输入原密码！","提示信息");
                this.txtOldPwd.Focus();
                return;
            }
            if (Program.currentAdmin.LoginPwd != this.txtOldPwd.Text.Trim())
            {
                MessageBox.Show("输入的原密码不正确","提示信息");
                this.txtOldPwd.Focus();
                return;
            }
            if (this.txtNewPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入新密码！","提示信息");
                this.txtNewPwd.Focus();
                return;
            }
            if (this.txtNewPwd.Text.Trim().Length > 6)
            {
                MessageBox.Show("请输入6位数的新密码！","提示信息");
                this.txtNewPwd.Focus();
                return;
            }
            if (this.txtNewPwd.Text.Trim() != this.txtNewPwdConfirm.Text.Trim())
            {
                MessageBox.Show("两次输入的新密码不一致！","提示信息");
                this.txtNewPwdConfirm.Focus();
                return;
            }
            //封装对象
            try
            {
                Admin objAdmin = new Admin()
                {
                    LoginId = Program.currentAdmin.LoginId,
                    LoginPwd = this.txtNewPwd.Text.Trim()
                };
                if (objAdminService.ModifyPwd(objAdmin) == 1)
                {
                    MessageBox.Show("密码修改成功，请妥善保管！", "提示信息");
                    Program.currentAdmin.LoginPwd = objAdmin.LoginPwd;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
