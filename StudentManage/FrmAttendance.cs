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
using Models.Ext;
using Common;

namespace StudentManager
{
    public partial class FrmAttendance : Form
    {
        private AttendanceService objAttendanceService = new AttendanceService();
        private StudentService objStudentService = new StudentService();
        private List<StudentExt> SignedStudent = new List<StudentExt>();//保存已打卡的学员对象
        public FrmAttendance()
        {
            InitializeComponent();
            //获取考勤的学员总数
            lblCount.Text = objAttendanceService.GetAllStudents();

            this.dgvStudentList.AutoGenerateColumns = false;
            timer1_Tick(null, null);
            ShowStat();
        }
        private void ShowStat()
        {
            //显示实际的出勤人数
            lblReal.Text = objAttendanceService.GetAttendStudents(DateTime.Now,true);
            //显示缺勤人数
            lblAbsenceCount.Text = (Convert.ToInt32(lblCount.Text.Trim()) - Convert.ToInt32(lblReal.Text.Trim())).ToString();
        }
        //显示当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblYear.Text = DateTime.Now.Year.ToString();
            this.lblMonth.Text = DateTime.Now.Month.ToString();
            this.lblDay.Text = DateTime.Now.Day.ToString();
            this.lblTime.Text = DateTime.Now.ToLongTimeString();

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.lblWeek.Text = "一";
                    break;
                case DayOfWeek.Tuesday:
                    this.lblWeek.Text = "二";
                    break;
                case DayOfWeek.Wednesday:
                    this.lblWeek.Text = "三";
                    break;
                case DayOfWeek.Thursday:
                    this.lblWeek.Text = "四";
                    break;
                case DayOfWeek.Friday:
                    this.lblWeek.Text = "五";
                    break;
                case DayOfWeek.Saturday:
                    this.lblWeek.Text = "六";
                    break;
                case DayOfWeek.Sunday:
                    this.lblWeek.Text = "日";
                    break;
            }
        }
        //学员打卡        
        private void txtStuCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtStuCardNo.Text.Trim().Length!=0 && e.KeyValue==13)
            {
                //1.显示学员信息
                StudentExt objStu = objStudentService.GetStudentByCardNo(this.txtStuCardNo.Text.Trim());
                if (objStu==null)
                {
                    MessageBox.Show("输入的考勤卡号不正确！","提示信息");
                    this.lblInfo.Text = "打卡失败！";
                    lblStuName.Text = "";
                    lblStuClass.Text = "";
                    lblStuId.Text = "";
                    this.pbStu.Image = null;
                    return;
                }
                else
                {
                    lblStuName.Text = objStu.StudentName;
                    lblStuId.Text = objStu.StudentId.ToString();
                    lblStuClass.Text = objStu.ClassName.ToString();
                    if (this.pbStu.Image != null && objStu.StuImage.Length != 0)
                        this.pbStu.Image=(Image)new SerializeObjectToString().DeserializeObject(objStu.StuImage);
                    else
                        this.pbStu.Image = Image.FromFile("default.png");

                }
                //2.添加打卡记录
                string result=objAttendanceService.AddRecord(this.txtStuCardNo.Text.Trim());
                if (result !="success") 
                {
                    this.lblInfo.Text = "打卡失败！";
                    MessageBox.Show(result,"错误提示！");
                }
                else
                {
                    this.lblInfo.Text="打卡成功！";
                    ShowStat();
                    objStu.DTime = DateTime.Now;
                    //将学员信息同步显示在列表中
                    SignedStudent.Add(objStu);
                    this.dgvStudentList.DataSource = null;
                    this.dgvStudentList.DataSource = this.SignedStudent;

                    this.txtStuCardNo.Text = " ";
                    this.txtStuCardNo.Focus();
                }
            }
           
        }
        //结束打卡
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAttendance_Load(object sender, EventArgs e)
        {
            this.txtStuCardNo.Focus();
        }

        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
    }
}
