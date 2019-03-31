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
            //��ʾ�༶
            this.cboClass.SelectedIndexChanged -= new EventHandler(cboClass_SelectedIndexChanged);

            this.cboClass.DataSource = objClassService.GetAllClasses();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;

            this.cboClass.SelectedIndexChanged+= new EventHandler(cboClass_SelectedIndexChanged);
           
        }     
        //���ݰ༶��ѯ      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��ʾ���Գɼ�
          
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList1(this.cboClass.Text.Trim());
            new Common.DataGridViewStyle().DgvStyle3(this.dgvScoreList);
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]���Գɼ�ͳ��";
            Dictionary<string, string> dic = objScoreListService.GetScoreListByClassId(this.cboClass.SelectedValue.ToString());
            lblAttendCount.Text = dic["stuCount"];
            lblCount.Text = dic["absentCount"];
            lblCSharpAvg.Text = dic["avgCSharp"];
            lblDBAvg.Text = dic["avgDB"];
            //ȱ����Ա
            List<string> list = objScoreListService.GetAsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            if (list.Count == 0) this.lblList.Items.Add("û��ȱ����Ա��");
            else
            {
                this.lblList.Items.AddRange(list.ToArray());
            }
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ͳ��ȫУ���Գɼ�
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "ͳ��ȫУ������Ϣ";
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList1("");
          //  new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);
            //��ʾ���Գɼ�ͳ��
            Dictionary<string, string> dic = objScoreListService.GetScoreInfo();
            lblAttendCount.Text = dic["stuCount"].ToString();
            lblCount.Text = dic["absentCount"].ToString();
            lblCSharpAvg.Text = dic["avgCSharp"].ToString();
            lblDBAvg.Text = dic["avgDB"].ToString();
            //��ʾȱ����Ա
            List<string> list = objScoreListService.GetAbsentScoreList();
            lblList.Items.Clear();
            if (list == null) lblList.Items.Add("û��ȱ����Ա");
            else
            {
                this.lblList.Items.AddRange(list.ToArray());
            }
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //����к�
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //ѡ���ѡ��ı䴦��
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvScoreList.IsCurrentCellDirty) //�����жϵ�Ԫ���ֵ�Ƿ���δ�ύ�ĸ���
            {
                this.dgvScoreList.CommitEdit(DataGridViewDataErrorContexts.Commit);//�ύ�޸�

                //��ʾ�޸ĺ��ֵ��true/false�����Խ��޸ĺ��״̬���µ����ݿ��...
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