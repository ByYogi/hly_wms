using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.9.2.Tbl_App_Version（商城APP版本更新表）
    /// </summary>
    [Serializable]
    public class APPVersionEntity
    {
        public int update { get; set; }
        public int ID { get; set; }
        public string AppVersion { get; set; }
        public string DowloadUrl { get; set; }
        public string Memo { get; set; }
        public string LimitStatus { get; set; }
        public DateTime OP_DATE { get; set; }
        public string AppType { get; set; }
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

    [Serializable]
    public class AppleCheckEntity
    {
        public int ID { get; set; } 
        public string AppVersion { get; set; }
        public string isOpenAppleLogin { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
