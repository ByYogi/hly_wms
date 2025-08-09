using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 物流公司数据实体表1.5.2.Tbl_Cargo_Logistic（物流公司表）
    /// </summary>
    [Serializable]
    public class CargoLogisticEntity
    {
        [Description("主键")]
        public int ID { get; set; }
        [Description("物流公司名称")]
        public string LogisticName { get; set; }
        [Description("删除标志")]
        public string DelFlag { get; set; }
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
