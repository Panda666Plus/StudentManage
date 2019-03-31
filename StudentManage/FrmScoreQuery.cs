﻿using System;
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
    public partial class FrmScoreQuery : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private ScoreListService objScoreListService = new ScoreListService();
        private DataSet ds=null;//保存全部查询结果的数据集
        public FrmScoreQuery()
        {
            InitializeComponent();
            //初始班级列表
            this.cboClass.DataSource = objClassService.GetAllClasses();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
            //显示全部考试成绩
            ds = objScoreListService.GetAllScoreList();
            //禁用
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = ds.Tables[0];
            //new Common.DataGridViewStyle().DgvStyle3(this.dgvScoreList);
        }     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //根据班级名称动态筛选
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ds==null) return;
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName='" + this.cboClass.Text.Trim()+"'";
        }
        //显示全部成绩
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName Like'%%'";
        }
        //根据C#成绩动态筛选
        private void txtScore_TextChanged(object sender, EventArgs e)
        {
            if (this.txtScore.Text.Trim().Length == 0) return;
            if (!Common.DataValidate.IsInteger(this.txtScore.Text.Trim())) return;
            else
                this.ds.Tables[0].DefaultView.RowFilter = "CSharp >'" + this.txtScore.Text.Trim() + "'";
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //添加行数
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

        //打印当前的成绩信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
            new ExcelPrint.DataExport().Export(this.dgvScoreList);
        }
    }
}
