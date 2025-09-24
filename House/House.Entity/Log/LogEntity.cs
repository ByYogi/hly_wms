using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    [Serializable]
    public class LogEntity
    {
        public Int64 BID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("用户名")]
        public string UserID { get; set; }
        /// <summary>
        /// 用户所在电脑IP地址
        /// </summary>
        [Description("IP地址")]
        public string IPAddress { get; set; }
        /// <summary>
        /// 功能描述操作说明
        ///Q：查询
        ///A：新增
        ///D：删除
        ///U：修改
        ///L：登陆   
        /// </summary>
        [Description("功能描述")]
        public string Operate { get; set; }
        /// <summary>
        /// 用户模块名称
        /// </summary>
        [Description("模块名称")]
        public string Moudle { get; set; }
        /// <summary>
        /// 导航页
        /// </summary>
        [Description("导航页")]
        public string NvgPage { get; set; }
        /// <summary>
        /// 用户操作日志详细说明
        /// </summary>
        [Description("操作日志详细说明")]
        public string Memo { get; set; }
        /// <summary>
        /// 日志状态0：成功1：失败
        /// </summary>
        [Description("日志状态")]
        public string Status { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 去NULL,替换危险字符
        /// </summary>
        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();

            foreach (PropertyInfo s in pSource)
            {
                if (s.PropertyType.Name.ToUpper().Contains("STRING"))
                {
                    if (s.GetValue(this, null) == null)
                        s.SetValue(this, "", null);
                    else
                        s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
                }
            }
        }
    }
}
