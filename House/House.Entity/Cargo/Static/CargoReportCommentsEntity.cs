using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.6.16. Tbl_Cargo_ReportComments（日报评论表）
    /// </summary>
    [Serializable]
    public class CargoReportCommentsEntity
    {
        public int ID { get; set; }
        [Description("日报ID")]
        public int Report_id { get; set; }
        [Description("评论内容")]
        public string Content { get; set; }
        [Description("登录名")]
        public string LoginName { get; set; }
        [Description("姓名")]
        public string UserName { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }

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