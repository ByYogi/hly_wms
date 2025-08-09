using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{

    /// <summary>
    /// 城市数据实体
    /// </summary>
    [Serializable]
    public class CityEntity
    {
        public int CID { get; set; }
        [Description("城市名称")]
        public string CityName { get; set; }
        [Description("城市代码")]
        public string CityCode { get; set; }
        [Description("作废标识")]
        public string DelFlag { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        [Description("系统代码")]
        public string BelongSystem { get; set; }
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
