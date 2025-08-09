using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户收货地址管理
    /// </summary>
    [Serializable]
    public class ClientAcceptAddressEntity
    {
        public string BelongSystem { get; set; }
        public Int64 AID { get; set; }
        [Description("客户编码")]
        public int ClientID { get; set; }
        [Description("收货地址所在城市")]
        public string AcceptCity { get; set; }
        [Description("收货地址")]
        public string AcceptAddress { get; set; }
        [Description("联系人")]
        public string AcceptPeople { get; set; }
        [Description("电话号码")]
        public string AcceptTelephone { get; set; }
        [Description("手机号码")]
        public string AcceptCellphone { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        [Description("收货公司名称")]
        public string AcceptCompany { get; set; }
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