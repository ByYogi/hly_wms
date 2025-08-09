using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.6.14. Tbl_Cargo_NoticeUserView（用户查看消息数据表）
    /// </summary>
    [Serializable]
    public class CargoNoticeUserViewEntity
    {
        public int ID { get; set; }
        [Description("公告ID/日报ID")]
        public int MessageID { get; set; }
        [Description("消息标题")]
        public string Title { get; set; }
        [Description("消息类型")]
        public int MessageType { get; set; }
        [Description("阅读状态")]
        public byte ReadStatus { get; set; }
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