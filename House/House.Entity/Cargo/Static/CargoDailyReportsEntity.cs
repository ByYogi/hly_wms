using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.6.14. Tbl_Cargo_DailyReports（用户提交日报数据表）
    /// </summary>
    [Serializable]
    public class CargoDailyReportsEntity
    {
        public int ID { get; set; }
        [Description("日报标题")]
        public string Title { get; set; }
        [Description("日报内容")]
        public string Content { get; set; }
        [Description("登录名")]
        public string LoginName { get; set; }
        [Description("姓名")]
        public string UserName { get; set; }
        [Description("日报可评论用户登录名")]
        public string LoginReportName { get; set; }
        [Description("日报可评论用户姓名")]
        public string UserReportName { get; set; }

        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int IsReply { get; set; }
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