using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.House
{
    /// <summary>
    /// 单位实体
    /// </summary>
    [Serializable]
    public class SystemUnitEntity
    {
        public int UnitID { get; set; }
        public int FirmID { get; set; }
        public string CName { get; set; }
        public string Address { get; set; }
        public string Boss { get; set; }
        public string phone { get; set; }
        public string Remark { get; set; }
        public string DelFlag { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 联系人手机号码
        /// </summary>
        public string CellPhone { get; set; }

        #region 新路程
        public string BelongSystem { get; set; }
        [Description("所在城市")]
        public string CityCode { get; set; }
        [Description("单位传真")]
        public string Fax { get; set; }
        public decimal Coefficient { get; set; }
        #endregion
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
