using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using StudentManage;

namespace StudentManager
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            //��ʾ��ǰ�û�
            this.lblCurrentUser.Text = Program.currentAdmin.AdminName + "]";
            //��ʾ�����屳��
            //this.spContainer.Panel2.BackgroundImage = Image.FromFile("mainbg.png");
            //this.spContainer.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            //��ʾ�汾��
            //this.lblVersion.Text = "��ǰ�汾��V" + ConfigurationManager.AppSettings["pversion"].ToString();
            //Ȩ���趨

        }

        #region Ƕ�봰����ʾ

        //�ر�Ƕ��Ĵ���
        private void ClosePreForm()
        {
            foreach (Control item in this.spContainer.Panel2.Controls)
            {
                if (item is Form)
                {
                    Form objControl = (Form)item;
                    objControl.Close();
                }
            }
        }
        private void OpenForm(Form objForm)
        {
            ClosePreForm();//�ر�ǰ��Ĵ���
            objForm.TopLevel = false;//���Ӵ������óɷǶ����ؼ�      
            objForm.FormBorderStyle = FormBorderStyle.None;//ȥ���Ӵ���ı߿�
            objForm.Parent = this.spContainer.Panel2;//ָ���Ӵ�����ʾ������  
            objForm.Dock = DockStyle.Fill;//����������С�Զ����������С
            objForm.Show();
        }
        //��ʾ������ѧԱ����       
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            FrmAddStudent objForm = new FrmAddStudent();
            OpenForm(objForm);
        }
        private void btnAddStu_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        //��������ѧԱ��Ϣ
        private void tsmi_Import_Click(object sender, EventArgs e)
        {
            FrmImportData objFrmImportData = new FrmImportData();
            OpenForm(objFrmImportData);
        }
        private void btnImportStu_Click(object sender, EventArgs e)
        {
            tsmi_Import_Click(null, null);
        }
        //���ڴ�      
        private void tsmi_Card_Click(object sender, EventArgs e)
        {
            FrmAttendance objForm = new FrmAttendance();
            OpenForm(objForm);
        }
        private void btnCard_Click(object sender, EventArgs e)
        {
            tsmi_Card_Click(null, null);
        }
        //�ɼ����ٲ�ѯ��Ƕ����ʾ��
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            FrmScoreQuery objForm = new FrmScoreQuery();
            OpenForm(objForm);
        }
        private void btnScoreQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }
        //ѧԱ������Ƕ����ʾ��
        private void tsmiManageStudent_Click(object sender, EventArgs e)
        {
            FrmStudentManage objForm = new FrmStudentManage();
            OpenForm(objForm);
        }
        private void btnStuManage_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        //��ʾ�ɼ���ѯ���������    
        private void tsmiQueryAndAnalysis_Click(object sender, EventArgs e)
        {
            FrmScoreManage objFrmScoreMange = new FrmScoreManage();
            OpenForm(objFrmScoreMange);
        }
        private void btnScoreAnalasys_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        //���ڲ�ѯ
        private void tsmi_AQuery_Click(object sender, EventArgs e)
        {
            FrmAttendanceQuery objForm = new FrmAttendanceQuery();
            OpenForm(objForm);
        }
        private void btnAttendanceQuery_Click(object sender, EventArgs e)
        {
            tsmi_AQuery_Click(null, null);
        }

        #endregion

        #region �˳�ϵͳȷ��

        //�˳�ϵͳ
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("ȷ���˳���", "�˳�ѯ��",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region ����

        //�����޸�
        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd objModifyPwd = new FrmModifyPwd();
            objModifyPwd.ShowDialog();
        }
        private void btnModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }
        //�˺��л�
        private void btnChangeAccount_Click(object sender, EventArgs e)
        {
            //������¼����
            FrmUserLogin objUserLogin = new FrmUserLogin();
            objUserLogin.Text = "�л��˺�";
            DialogResult result=objUserLogin.ShowDialog();

            //���ݴ��巵��ֵ�ж��û���¼�Ƿ�ɹ�
            if (result == DialogResult.OK)
            {
                this.lblCurrentUser.Text = Program.currentAdmin.AdminName+"]";
            }
        }
        private void tsbAddStudent_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        private void tsbScoreAnalysis_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        private void tsbModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }

        //���ʹ���
        private void tsmi_linkxkt_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baidu.com");
        }
        private void btnGoXiketang_Click(object sender, EventArgs e)
        {
            tsmi_linkxkt_Click(null, null);
        }
        //ϵͳ����
        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        #endregion



    }
}