using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 付款申请书里收款人信息数据实体1.6.14.Tbl_Cargo_ReceiveInfo（收款人信息数据表）
    /// </summary>
    [Serializable]
    public class CargoReceiveInfoEntity
    {
        public long ID { get; set; }
        public int HouseID { get; set; }
        public string ReceiveUnit { get; set; }
        public string CardName { get; set; }
        public string CardBank { get; set; }
        public string CardNum { get; set; }
        public string CardType { get; set; }
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
