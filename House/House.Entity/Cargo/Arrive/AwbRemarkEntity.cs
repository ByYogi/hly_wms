using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 运单备注信息表
    /// </summary>
    [Serializable]
    public class AwbRemarkEntity
    {
        public int id { get; set; }
        public string BelongSystem { get; set; }
        public string AwbNo { get; set; }
        public string OP_ID { get; set; }
        public string OP_NAME { get; set; }
        public string Remark { get; set; }
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
