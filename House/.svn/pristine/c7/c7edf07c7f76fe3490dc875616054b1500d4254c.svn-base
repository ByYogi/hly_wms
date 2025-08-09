using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户信息实体
    /// </summary>
    [Serializable]
    public class ClientEntity
    {
        public string BelongSystem { get; set; }
        public int ClientID { get; set; }
        [Description("客户全称")]
        public string ClientName { get; set; }
        [Description("客户简称")]
        public string ClientShortName { get; set; }
        [Description("公司地址")]
        public string Address { get; set; }
        [Description("邮编号码")]
        public string ZipCode { get; set; }
        [Description("公司电话")]
        public string Telephone { get; set; }
        [Description("公司传真")]
        public string Fax { get; set; }
        [Description("公司负责人")]
        public string Boss { get; set; }
        [Description("公司职务")]
        public string Position { get; set; }
        [Description("手机号码")]
        public string Cellphone { get; set; }
        [Description("电子邮件")]
        public string Email { get; set; }
        [Description("发票抬头")]
        public string Invoice { get; set; }
        [Description("税号")]
        public string TaxNum { get; set; }
        [Description("开户银行")]
        public string Bank { get; set; }
        [Description("银行账号")]
        public string BankAccount { get; set; }
        [Description("所属行业")]
        public string Business { get; set; }
        [Description("经营产品")]
        public string Product { get; set; }
        [Description("企业性质")]
        public string CompanyType { get; set; }
        [Description("企业规模")]
        public int CompanyScope { get; set; }
        [Description("企业备注")]
        public string CompanyRemark { get; set; }
        [Description("开始运作时间")]
        public DateTime StartDate { get; set; }
        [Description("最后运作时间")]
        public DateTime EndDate { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string DelFlag { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public List<ClientAcceptAddressEntity> AcceptAddress { get; set; }
        [Description("客户性质")]
        public string ClientType { get; set; }
        [Description("客户编号")]
        public string ClientNum { get; set; }
        [Description("归属网点")]
        public string BelongDot { get; set; }
        [Description("结款网点")]
        public string ReceiveDot { get; set; }
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

    //[Serializable]
    //public class ClientSessionEntity
    //{
    //    public string BelongSystem { get; set; }
    //    public int ClientID { get; set; }
    //    public string ClientName { get; set; }
    //    public string ClientShortName { get; set; }
    //    public string Address { get; set; }
    //    public string Boss { get; set; }
    //    public string Cellphone { get; set; }
    //    public string Telephone { get; set; }
    //    public string ClientNum { get; set; }
    //    /// <summary>
    //    /// 去NULL,替换危险字符
    //    /// </summary>
    //    public void EnSafe()
    //    {
    //        PropertyInfo[] pSource = this.GetType().GetProperties();

    //        foreach (PropertyInfo s in pSource)
    //        {
    //            if (s.PropertyType.Name.ToUpper().Contains("STRING"))
    //            {
    //                if (s.GetValue(this, null) == null)
    //                    s.SetValue(this, "", null);
    //                else
    //                    s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
    //            }
    //        }
    //    }
    //}
}
