using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 企业微信群聊数据实体1.9.8.Tbl_QY_GroupChat（企业微信群聊会话数据表）
    /// </summary>
    [Serializable]
    public class QyGroupChatEntity
    {
        public int ID { get; set; }
        public string ChatID { get; set; }
        public int HouseID { get; set; }
        public string ChatType { get; set; }
        public string ChatName { get; set; }
        public string Owner { get; set; }
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
