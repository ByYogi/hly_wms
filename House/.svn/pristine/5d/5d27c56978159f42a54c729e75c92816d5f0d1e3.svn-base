using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.4.6.Tbl_Cargo_PostAddress（发票邮寄地址数据表）
    /// </summary>
    [Serializable]
    public class CargoClientPostAddressEntity
    {
        public long ID { get; set; }
        public int ClientNum { get; set; }
        public string AcceptProvince { get; set; }
        public string AcceptCity { get; set; }
        public string AcceptCountry { get; set; }
        public string AcceptAddress { get; set; }
        public string AcceptPeople { get; set; }
        public string AcceptCellphone { get; set; }
        public string ZipCode { get; set; }
        public string OP_ID { get; set; }
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
