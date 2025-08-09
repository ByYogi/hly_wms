using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 账单实体
    /// </summary>
    [Serializable]
    public class CargoAccountEntity
    {
        [Description("主键")]
        public int ID { get; set; }
        [Description("账单号")]
        public string AccountNo { get; set; }
        [Description("账单名称")]
        public string Title { get; set; }
        [Description("账单日期")]
        public DateTime CreateDate { get; set; }
        [Description("客户名称")]
        public string Name { get; set; }
        [Description("账单金额")]
        public double Total { get; set; }
        [Description("税费")]
        public double TaxFee { get; set; }
        [Description("其它费用")]
        public double OtherFee { get; set; }
        [Description("代收款")]
        public double CollectMoney { get; set; }
        [Description("代垫款")]
        public double PayMoney { get; set; }
        [Description("备注")]
        public string Memo { get; set; }
        [Description("账单审核状态")]
        public string ApplyStatus { get; set; }
        [Description("审核人")]
        public string CheckName { get; set; }
        [Description("审核时间")]
        public DateTime CheckDate { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("账单类型")]
        public int AType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Description("订单号")]
        public string OrderNo { get; set; }
        public decimal ReceivedMoney { get; set; }

        public List<CargoOrderEntity> orderList { get; set; }
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
