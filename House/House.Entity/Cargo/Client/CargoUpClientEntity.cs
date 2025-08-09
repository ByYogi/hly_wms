using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 上游客户数据实体
    /// </summary>
    [Serializable]
    public class CargoUpClientEntity
    {
        [Description("上游客户ID")]
        public long UpClientID { get; set; }
        [Description("客户全称")]
        public string UpClientName { get; set; }
        [Description("客户简称")]
        public string UpClientShortName { get; set; }
        [Description("客户类型")]
        public int ClientType { get; set; }
        [Description("公司地址")]
        public string Address { get; set; }
        [Description("邮编")]
        public string ZipCode { get; set; }
        [Description("公司电话")]
        public string Telephone { get; set; }
        [Description("负责人")]
        public string Boss { get; set; }
        [Description("手机号码")]
        public string Cellphone { get; set; }
        [Description("删除标识")]
        public int DelFlag { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("所在省份")]
        public string Province { get; set; }
        [Description("所在城市")]
        public string City { get; set; }
        [Description("所在区")]
        public string Country { get; set; }
        [Description("所在仓库")]
        public string HouseID { get; set; }
        [Description("仓库名称")]
        public string HouseName { get; set; }
        public DateTime OP_DATE { get; set; }
        public string HouseIDs { get; set; }


        [Description("上游客户部门ID")]
        public int ID { get; set; }
        [Description("部门名称")]
        public string DepName { get; set; }
        [Description("小胎库存结算标准")]
        public double SmallStockCheckFee { get; set; }
        [Description("大胎库存结算标准")]
        public double BigStockCheckFee { get; set; }
        [Description("操作人ID")]
        public string OP_ID { get; set; }
        [Description("操作人Name")]
        public string OP_Name { get; set; }
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
