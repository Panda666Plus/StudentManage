using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAL;
using Models;
using Models.Ext;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private ScoreListService objScoreListService = new ScoreListService();
        public FrmScoreManage()
        {
            InitializeComponent();
            //显示班级
            this.cboClass.SelectedIndexChanged -= new EventHandler(cboClass_SelectedIndexChanged);

            this.cboClass.DataSource = objClassService.GetAllClasses();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;

            this.cboClass.SelectedIndexChanged+= new EventHandler(cboClass_SelectedIndexChanged);
           
        }     
        //根据班级查询      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //显示考试成绩
          
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList1(this.cboClass.Text.Trim());
            new Common.DataGridViewStyle().DgvStyle3(this.dgvScoreList);
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]考试成绩统计";
            Dictionary<string, string> dic = objScoreListService.GetScoreListByClassId(this.cboClass.SelectedValue.ToString());
            lblAttendCount.Text = dic["stuCount"];
            lblCount.Text = dic["absentCount"];
            lblCSharpAvg.Text = dic["avgCSharp"];
            lblDBAvg.Text = dic["avgDB"];
            //缺口人员
            List<string> list = objScoreListService.GetAsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            if (list.Count == 0) this.lblList.Items.Add("没有缺考人员！");
            else
            {
                this.lblList.Items.AddRange(list.ToArray());
            }
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //统计全校考试成绩
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "统计全校考试信息";
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList1("");
          //  new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);
            //显示考试成绩统计
            Dictionary<string, string> dic = objScoreListService.GetScoreInfo();
            lblAttendCount.Text = dic["stuCount"].ToString();
            lblCount.Text = dic["absentCount"].ToString();
            lblCSharpAvg.Text = dic["avgCSharp"].ToString();
            lblDBAvg.Text = dic["avgDB"].ToString();
            //显示缺考人员
            List<string> list = objScoreListService.GetAbsentScoreList();
            lblList.Items.Clear();
            if (list == null) lblList.Items.Add("没有缺考人员");
            else
            {
                this.lblList.Items.AddRange(list.ToArray());
            }
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //添加行号
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //选择框选择改变处理
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvScoreList.IsCurrentCellDirty) //首先判断单元格的值是否有未提交的更改
            {
                this.dgvScoreList.CommitEdit(DataGridViewDataErrorContexts.Commit);//提交修改

                //显示修改后的值（true/false）可以将修改后的状态更新到数据库等...
                string ss = this.dgvScoreList.CurrentCell.EditedFormattedValue.ToString();
                MessageBox.Show(ss);
            }
        }

        private void dgvScoreList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs  e)
        {
            if (e.ColumnIndex == 0 && e.Value is Student)
            {
                e.Value = (e.Value as Student ).StudentId;
            }
            if (e.ColumnIndex == 1 && e.Value is Student)
            {
                e.Value = (e.Value as Student).StudentName;
            }
            if (e.ColumnIndex == 2 && e.Value is Student)
            {
                e.Value = (e.Value as Student).Gender;
            }
            if (e.ColumnIndex == 4 && e.Value is ScoreList)
            {
                e.Value = (e.Value as ScoreList).CSharp;
            }
            if (e.ColumnIndex == 5 && e.Value is ScoreList)
            {
                e.Value = (e.Value as ScoreList).SQLServerDB;
            }
        }
    }
}