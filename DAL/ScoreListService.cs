using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Models.Ext;
using Models;
using DAL;

namespace DAL
{
    public class ScoreListService
    {
        /// <summary>
        /// 查询所以学员成绩列表
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public DataSet GetAllScoreList()
        {
            string sql = "select Students.StudentId,StudentName,Gender,ClassName,PhoneNumber,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId";
            
            return SQLHelper.GetDataSet(sql); 
           
        }

        /// <summary>
        /// 查询学员成绩
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public List<StudentExt> GetScoreList(string ClassName)
        {
            string sql = "select Students.StudentId,StudentName,Gender,ClassName,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId =Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId";
            if(ClassName!=null&&ClassName.Length!=0)
            {
                sql += " where ClassName='"+ClassName+"'";
            }
            
            sql = string.Format(sql, ClassName);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<StudentExt> list = new List<StudentExt>();
            while (objReader.Read())
            {
                list.Add(new StudentExt()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender=objReader["Gender"].ToString(),
                    ClassName=objReader["ClassName"].ToString(),
                    CSharp=objReader["CSharp"].ToString(),
                    SQLServerDB=objReader["SQLServerDB"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 查询(考试成绩统计）
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Dictionary<string ,string> GetScoreListByClassId(string classId)
        {
            string sql = "select stuCount=Count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from ScoreList";
            sql += " inner join Students on ScoreList.StudentId=Students.StudentId where ClassId={0};";
            sql += " select absentCount=Count(*) from Students where StudentId not in";
            sql += " (select StudentId from ScoreList) and ClassId={1}";
            sql = string.Format(sql, classId,classId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (objReader.Read())
            {
                dic.Add("stuCount", objReader["stuCount"].ToString());
                dic.Add("avgCSharp", objReader["avgCSharp"].ToString());
                dic.Add("avgDB", objReader["avgDB"].ToString());
            }
            if (objReader.NextResult())
            {
                if (objReader.Read())
                {
                    dic.Add("absentCount", objReader["absentCount"].ToString());
                }
            }
            objReader.Close();
            return dic;
        }

        /// <summary>
        /// 查询缺考学员名单
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<string> GetAsentListByClassId(string classId)
        {
            string sql = "select StudentName from Students where StudentId not in";
            sql += " (select StudentId from ScoreList) and ClassId={0}";
            sql = string.Format(sql, classId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 查询成绩(统计全校考试成绩)
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<ExtStudent> GetScoreList1(string className)
        {
            string sql = "select Students.StudentId,StudentName,ClassName,Gender,PhoneNumber,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId";
            if (className != null && className.Length != 0)
            {
                sql += " where ClassName='" + className + "'";
            }
            
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ExtStudent> list = new List<ExtStudent>();
            while (objReader.Read())
            {
                list.Add(new ExtStudent()
                {

                    ObjStudent = new Student()
                    {
                        StudentId = Convert.ToInt32(objReader["StudentId"]),
                        StudentName = objReader["StudentName"].ToString(),

                        Gender = objReader["Gender"].ToString(),
                        PhoneNumber = objReader["PhoneNumber"].ToString(),
                    },
                    ObjScore = new ScoreList()
                    {
                        CSharp = Convert.ToInt32(objReader["CSharp"]),
                        SQLServerDB = Convert.ToInt32(objReader["SQLServerDB"]),
                    },
                    ClassName = objReader["ClassName"].ToString(),
                    cc = false
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 查询(考试成绩统计)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetScoreInfo()
        {
            string sql = "select stuCount=Count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from Students";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId;";
            sql += "select absentCount=Count(*) from Students where StudentId not in ";
            sql += "(select StudentId from ScoreList)";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            while (objReader.Read())
            {
                dic.Add("stuCount", objReader["stuCount"].ToString());
                dic.Add("avgCSharp", objReader["avgCSharp"].ToString());
                dic.Add("avgDB", objReader["avgDB"].ToString());
            }
            while (objReader.NextResult())
            {
                if (objReader.Read())
                {
                    dic.Add("absentCount", objReader["absentCount"].ToString());
                }
            }
            objReader.Close();
            return dic;
        }
        /// <summary>
        /// 缺考人员(考试成绩统计)
        /// </summary>
        /// <returns></returns>
        public List<string> GetAbsentScoreList()
        {
            string sql = "select StudentName from Students where StudentId not in";
            sql += "(select StudentId from ScoreList)";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<String> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }
    }
}
