using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信用户管理 地址管理
    /// </summary>
    [Serializable]
    public class WXUserAddressEntity
    {
        public Int64 ID { get; set; }
        [Description("用户ID")]
        public Int64 WXID { get; set; }
        [Description("省")]
        public string Province { get; set; }
        [Description("市")]
        public string City { get; set; }
        [Description("区")]
        public string Country { get; set; }
        [Description("详细地址")]
        public string Address { get; set; }
        [Description("收货人姓名")]
        public string Name { get; set; }
        [Description("联系电话")]
        public string Cellphone { get; set; }
        public string AddressType { get; set; }
        public string IsDefault { get; set; }
        public DateTime OP_DATE { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string wxOpenID { get; set; }
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
