using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    [Serializable]
    public class ClientSessionEntity
    {
        public long ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientShortName { get; set; }
        public string Address { get; set; }
        public string Boss { get; set; }
        public string PinyinName { get; set; }
        public string Cellphone { get; set; }
        public string Telephone { get; set; }
        public string ClientNum { get; set; }
        public string ShopCode { get; set; }
        public string ClientType { get; set; }
        public int HouseID { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 开户名
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string CardNum { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public string HouseName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string BookCheck { get; set; }
        public decimal RebateMoney { get; set; }
        public string CheckOutType { get; set; }
        public decimal LimitMoney { get;set; }
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

    [Serializable]
    public class WXClientSessionEntity
    {
        public string Address { get; set; }
        public string label { get; set; }//Boss客户姓名
        public string Cellphone { get; set; }
        public string HouseName { get; set; }
    }
}
