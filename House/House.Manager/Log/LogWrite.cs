using House.DataAccess;
using House.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Manager
{
    /// <summary>
    /// 增加日志类
    /// </summary>
    public class LogWrite<EntityType>
    {
        /// <summary>
        /// 日志添加方法
        /// </summary>
        public void WriteLog(EntityType entity, LogEntity logEn)
        {
            logEn.EnSafe();
            SqlHelper sql = new SqlHelper();
            //返回日志详细内容
            string Content = logEn.Memo + "！" + Environment.NewLine + BuildLogContent(entity);
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into Tbl_SysLog(UserID,IPAddress,Operate,NvgPage,Status,Moudle,Memo)");
                sb.Append(" Values ");
                sb.Append("(@UserID,@IPAddress,@Operate,@NvgPage,@Status,@Moudle,@Memo)");

                DbCommand command = sql.GetSqlStringCommond(sb.ToString());
                sql.AddInParameter(command, "@UserID", DbType.String, logEn.UserID.Trim());
                sql.AddInParameter(command, "@IPAddress", DbType.String, logEn.IPAddress.Trim());
                sql.AddInParameter(command, "@Status", DbType.String, logEn.Status);
                sql.AddInParameter(command, "@Operate", DbType.String, logEn.Operate);
                sql.AddInParameter(command, "@NvgPage", DbType.String, logEn.NvgPage);
                sql.AddInParameter(command, "@Moudle", DbType.String, logEn.Moudle);
                sql.AddInParameter(command, "@Memo", DbType.String, Content);

                sql.ExecuteNonQuery(command);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 日志添加方法
        /// </summary>
        public void WriteLog(LogEntity logEn)
        {
            logEn.EnSafe();
            SqlHelper sql = new SqlHelper();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into Tbl_SysLog(UserID,IPAddress,Operate,NvgPage,Status,Moudle,Memo)");
                sb.Append(" Values ");
                sb.Append("(@UserID,@IPAddress,@Operate,@NvgPage,@Status,@Moudle,@Memo)");

                DbCommand command = sql.GetSqlStringCommond(sb.ToString());
                sql.AddInParameter(command, "@UserID", DbType.String, logEn.UserID.Trim());
                sql.AddInParameter(command, "@IPAddress", DbType.String, logEn.IPAddress.Trim());
                sql.AddInParameter(command, "@Status", DbType.String, logEn.Status);
                sql.AddInParameter(command, "@Operate", DbType.String, logEn.Operate);
                sql.AddInParameter(command, "@NvgPage", DbType.String, logEn.NvgPage);
                sql.AddInParameter(command, "@Moudle", DbType.String, logEn.Moudle);
                sql.AddInParameter(command, "@Memo", DbType.String, logEn.Memo);

                sql.ExecuteNonQuery(command);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void NewayWriteLog(EntityType entity, LogEntity logEn)
        {
            SqlHelper sql = new SqlHelper("Neway");
            //返回日志详细内容
            string Content = logEn.Memo + "！" + Environment.NewLine + BuildLogContent(entity);
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into Tbl_SysLog(UserID,IPAddress,Operate,NvgPage,Status,Moudle,content,BelongSystem)");
                sb.Append(" Values ");
                sb.Append("(@UserID,@IPAddress,@Operate,@NvgPage,@Status,@Moudle,@content,@BelongSystem)");

                DbCommand command = sql.GetSqlStringCommond(sb.ToString());
                sql.AddInParameter(command, "@UserID", DbType.String, logEn.UserID.Trim());
                sql.AddInParameter(command, "@IPAddress", DbType.String, logEn.IPAddress.Trim());
                sql.AddInParameter(command, "@Status", DbType.String, logEn.Status);
                sql.AddInParameter(command, "@Operate", DbType.String, logEn.Operate);
                sql.AddInParameter(command, "@NvgPage", DbType.String, logEn.NvgPage);
                sql.AddInParameter(command, "@Moudle", DbType.String, logEn.Moudle);
                sql.AddInParameter(command, "@BelongSystem", DbType.String, "NW");
                sql.AddInParameter(command, "@content", DbType.String, Content);

                sql.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 日志添加方法
        /// </summary>
        public void NewayWriteLog(LogEntity logEn)
        {
            SqlHelper sql = new SqlHelper("Neway");
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into Tbl_SysLog(UserID,IPAddress,Operate,NvgPage,Status,Moudle,content,BelongSystem)");
                sb.Append(" Values ");
                sb.Append("(@UserID,@IPAddress,@Operate,@NvgPage,@Status,@Moudle,@content,@BelongSystem)");

                DbCommand command = sql.GetSqlStringCommond(sb.ToString());
                sql.AddInParameter(command, "@UserID", DbType.String, logEn.UserID.Trim());
                sql.AddInParameter(command, "@IPAddress", DbType.String, logEn.IPAddress.Trim());
                sql.AddInParameter(command, "@Status", DbType.String, logEn.Status);
                sql.AddInParameter(command, "@Operate", DbType.String, logEn.Operate);
                sql.AddInParameter(command, "@NvgPage", DbType.String, logEn.NvgPage);
                sql.AddInParameter(command, "@Moudle", DbType.String, logEn.Moudle);
                sql.AddInParameter(command, "@content", DbType.String, logEn.Memo);
                sql.AddInParameter(command, "@BelongSystem", DbType.String, "NW");

                sql.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得一个给定的实体的所有属性信息的数组
        /// </summary>
        /// <param name="myEntity">要操作的实体对象</param>
        /// <returns>返回这个给定的实体的属性信息数组</returns>
        /// <remarks>
        /// 修改原因：将private的方法变为public的方法，提供给其他类使用
        /// </remarks>
        private PropertyInfo[] GetPropertyInfo(EntityType myEntity)
        {
            //获得要操作的实体的类型
            Type typeOfInstance = myEntity.GetType();
            //获得所有的实体类中的属性
            PropertyInfo[] info = typeOfInstance.GetProperties();
            return info;
        }
        /// <summary>
        /// 构建实体内容组成日志详细内容
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        private string BuildLogContent(EntityType ent)
        {
            string res = string.Empty;
            Hashtable result = new Hashtable();

            PropertyInfo[] info = GetPropertyInfo(ent);
            foreach (var it in info)
            {
                if (it == null)
                {
                    continue;
                }

                DescriptionAttribute a = (DescriptionAttribute)Attribute.GetCustomAttribute(it, typeof(DescriptionAttribute));

                if (a == null || it.GetValue(ent, null) == null)
                {
                    continue;
                }

                if (it.PropertyType.Equals(typeof(DateTime)))
                {
                    if (Convert.ToDateTime(it.GetValue(ent, null)).ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        if (!result.Contains(it.Name))
                        {
                            result.Add(a.Description, it.GetValue(ent, null));
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(it.GetValue(ent, null).ToString()) || !it.GetValue(ent, null).ToString().Equals("0"))
                    {
                        if (!result.Contains(it.Name))
                        {
                            result.Add(a.Description, it.GetValue(ent, null));
                        }
                    }
                }
            }

            foreach (DictionaryEntry ie in result)
            {
                res += ie.Key + "：" + ie.Value + ";";
            }
            return res;
        }
    }
}
