using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 外协承运商档案表
    /// </summary>
    [Serializable]
    public class CarrierEntity
    {
        public string BelongSystem { get; set; }
        /// <summary>
        /// 表主键自增长 
        /// </summary>
        public Int64 CarrierID { get; set; }
        [Description("承运商公司全称")]
        public string CarrierName { get; set; }
        [Description("承运商公司简称")]
        public string CarrierShortName { get; set; }
        [Description("联系人")]
        public string Boss { get; set; }
        [Description("联系电话")]
        public string Telephone { get; set; }
        [Description("公司地址")]
        public string Address { get; set; }
        [Description("邮编")]
        public string ZipCode { get; set; }
        [Description("传真")]
        public string Fax { get; set; }
        [Description("Email")]
        public string Email { get; set; }
        //[Description("公司法人")]
        //public string Corporate { get; set; }
        //[Description("营业执照")]
        //public string License { get; set; }
        //[Description("成立日期")]
        //public DateTime StartDate { get; set; }
        //[Description("有效日期")]
        //public DateTime EndDate { get; set; }
        //[Description("注册资金")]
        //public decimal RegisterFund { get; set; }
        //[Description("年营业额")]
        //public decimal YearMoney { get; set; }
        //[Description("公司性质")]
        //public string CarrierType { get; set; }
        //[Description("公司规模")]
        //public int CarrierScope { get; set; }
        [Description("公司备注")]
        public string Remark { get; set; }
        //[Description("自有车辆")]
        //public int CarNum { get; set; }
        //[Description("仓储面积")]
        //public decimal AreaNum { get; set; }
        [Description("所在城市")]
        public string City { get; set; }
        [Description("经营线路")]
        public string Line { get; set; }
        [Description("是否有信息化")]
        public string IT { get; set; }
        [Description("结算方式")]
        public string CheckOutType { get; set; }
        [Description("合约内容")]
        public string DealInfo { get; set; }
        //[Description("背景资料")]
        //public string BackgroundInfo { get; set; }
        [Description("删除标识")]
        public string DelFlag { get; set; }
        [Description("承运资格")]
        public string HasCarrier { get; set; }
        //[Description("配送资格")]
        //public string HasDelivery { get; set; }
        //[Description("结算资格")]
        //public string HasCheck { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
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
