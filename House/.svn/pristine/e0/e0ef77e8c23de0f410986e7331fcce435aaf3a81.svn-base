using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 采购账单明细表 Tbl_Cargo_RealPurchaseAccountGoods
    /// 描述：系统订单数据表，维护客户订单数据信息
    /// </summary>
    [Serializable]
    public class CargoRealPurchaseAccountGoodsEntity
    {
        /// <summary>
        /// 表主键
        /// </summary>
        [Description("表主键")]
        public long ID { get; set; }

        /// <summary>
        /// 账单号（关联采购账单表）
        /// </summary>
        [Description("账单号")]
        public string AccountNO { get; set; }

        /// <summary>
        /// 采购单ID
        /// </summary>
        [Description("采购单ID")]
        public long PurOrderID { get; set; }

        /// <summary>
        /// 采购单号（格式：P+日期+顺序号）
        /// </summary>
        [Description("采购单号")]
        public string PurOrderNo { get; set; }

        /// <summary>
        /// 采购单费用
        /// </summary>
        [Description("采购单费用")]
        public decimal TransportFee { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        [Description("其他费用")]
        public decimal OtherFee { get; set; }

        /// <summary>
        /// 合计费用
        /// </summary>
        [Description("合计费用")]
        public decimal TotalCharge { get; set; }

        /// <summary>
        /// 账单类型（0：支出，1：收入）
        /// </summary>
        [Description("账单类型")]
        public byte AccountType { get; set; }

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