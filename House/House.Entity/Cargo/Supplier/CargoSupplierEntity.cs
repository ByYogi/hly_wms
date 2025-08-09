using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 供应商管理实体
    /// </summary>
    [Serializable]
    public class CargoSupplierEntity
    {
        [Description("表主键")]
        public Int64 SuppID { get; set; }
        [Description("供应商公司名称")]
        public string SuppCompany { get; set; }
        [Description("供应商联系人")]
        public string SuppBoss { get; set; }
        [Description("联系人电话")]
        public string SuppCellphone { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("作废标志")]
        public Int64 DelFlag { get; set; }
        [Description("供应商地址")]
        public string Address { get; set; }
        [Description("所在城市")]
        public string City { get; set; }
        [Description("所在区")]
        public string Country { get; set; }
        [Description("所在省份")]
        public string Province { get; set; }
        [Description("供应商级别")]
        public Int64 SuppLevel { get; set; }

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
