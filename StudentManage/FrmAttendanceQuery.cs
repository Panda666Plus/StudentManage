using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;
using DAL;
namespace StudentManager
{
    
    public partial class FrmAttendanceQuery : Form
    {
        private AttendanceService objAttendanceService = new AttendanceService();
        
        public FrmAttendanceQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
          
            DateTime dt1 = Convert.ToDateTime(this.dtpTime.Text.Trim());
            DateTime dt2 = dt1.AddDays(1.0);
            this.dgvStudentList.AutoGenerateColumns = false;
            this.dgvStudentList.DataSource = objAttendanceService.GetStudentByDate(dt1, dt2, this.txtName.Text.Trim());
            new Common.DataGridViewStyle().DgvStyle3(this.dgvStudentList);

            lblCount.Text = objAttendanceService.GetAllStudents();
            lblReal.Text = objAttendanceService.GetAttendStudents(Convert.ToDateTime(this.dtpTime.Text.Trim()), false);
            lblAbsenceCount.Text =( Convert.ToInt32(lblCount.Text.Trim()) - Convert.ToInt32(lblReal.Text.Trim())).ToString();
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
