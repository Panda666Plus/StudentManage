using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using StudentManager;

namespace StudentManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmUserLogin frmUserLogin = new FrmUserLogin();
           DialogResult result= frmUserLogin.ShowDialog();
            if (result == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }
            
        }
        public static Admin currentAdmin = null; 
    }
}
