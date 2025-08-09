using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 特殊客户编码实体
    /// </summary>
    [Serializable]
    public class CargoSpecialVIPNumEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("客户编码")]
        public int ClientNum { get; set; }
        [Description("状态")]
        public string Status { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public string ClientShortName { get; set; }

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
