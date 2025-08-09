using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓库物流订阅数据实体1.6.9.Tbl_Cargo_HouseLogisPoll（仓库物流订阅表）
    /// </summary>
    [Serializable]
    public class CargoHouseLogisPollEntity
    {
        [Description("主键")]
        public int ID { get; set; }
        [Description("物流公司ID")]
        public int LogisID { get; set; }
        [Description("物流公司名称")]
        public string LogisticName { get; set; }
        [Description("启用状态")]
        public string DelFlag { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("操作人")]
        public string OP_ID { get; set; }

        [Description("仓库ID")]
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        [Description("物流代码")]
        public string ComCode { get; set; }
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
