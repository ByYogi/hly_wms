using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.6.17. Tbl_Cargo_UserReportComments（日报评论用户表）
    /// </summary>
    [Serializable]
    public class CargoUserReportCommentsEntity
    {
        public int ID { get; set; }
        [Description("登录名")]
        public string LoginName { get; set; }
        [Description("姓名")]
        public string UserName { get; set; }
        [Description("角色ID")]
        public int? RoleID { get; set; } // 使用int?允许RoleID为null
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