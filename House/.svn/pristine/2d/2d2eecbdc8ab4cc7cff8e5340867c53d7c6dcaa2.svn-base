using House.Entity.Cargo.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.4.28.	Tbl_Cargo_SuppClientAccount（供应商账单数据表）
    /// </summary>
    [Serializable]
    public class CargoSuppClientAccountEntity
    {
        [Description("ID")]
        public long AccountID { get; set; }

        [Description("账单号")]
        public string AccountNO { get; set; }

        [Description("账单名称")]
        public string AccountTitle { get; set; }

        [Description("创建账单日期")]
        public DateTime CreateDate { get; set; }

        [Description("供应商编号")]
        public int ClientID { get; set; }

        [Description("供应商编码")]
        public int ClientNum { get; set; }

        [Description("仓库编码")]
        public int HouseID { get; set; }

        [Description("仓库名称")]
        public string HouseName { get; set; }

        [Description("本期订单金额/总金额")]
        public decimal ARMoney { get; set; }

        [Description("账单金额")]
        public decimal Total { get; set; }

        [Description("优惠卷金额")]
        public decimal InsuranceFee { get; set; }

        [Description("仓库超期费")]
        public decimal OverDueFee { get; set; }

        [Description("出仓费")]
        public decimal OutStorageFee { get; set; }

        [Description("物流费用")]
        public decimal DeliveryFee { get; set; }

        [Description("退仓费")]
        public decimal StoReleaseFee { get; set; }
        [Description("其他费用")]
        public decimal OtherFee { get; set; }

        [Description("备注说明")]
        public string Memo { get; set; }

        [Description("账单审核状态")]
        public int Status { get; set; }

        [Description("审批人ID")]
        public string NextCheckID { get; set; }

        [Description("审批人姓名")]
        public string NextCheckName { get; set; }

        [Description("审批时间")]
        public DateTime NextCheckDate { get; set; }

        [Description("结算状态")]
        public int CheckStatus { get; set; }

        [Description("操作员")]
        public string OPID { get; set; }

        [Description("操作时间")]
        public DateTime OPDATE { get; set; }

        [Description("账单类型")]
        public int AType { get; set; }

        [Description("电子签名")]
        public int  ElecSign { get; set; }

        [Description("签名时间")]
        public DateTime ElecSignDate { get; set; }

        [Description("签名照片路径")]
        public string ElecSignImg { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 配送费
        /// </summary>
        public decimal TransitFee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CargoSuppClientAccountGoodsEntity> SuppBillGoods { get; set; }
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

    public class SplitRule
    {

        /// <summary>
        /// 手续费承担方
        /// </summary>
        public string feeTakeMchId { get; set; }
        /// <summary>
        /// 分账模式  0-固定金额分
        /// </summary>
        public string type { get; set; }

        public List<SplitRuleList> splitRuleList { get; set; }
        
    }

    public class SplitRuleList {

        /// <summary>
        /// 备注
        /// </summary>
        public string transMessage { get; set; }
        /// <summary>
        /// 分账子单号
        /// </summary>
        public string subOutOrderNo { get; set; }
        /// <summary>
        /// 分账金额  单位分
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 收款方  会员编码
        /// </summary>
        public string bizUserId { get; set; }
    }

    public class CargoSuppClientAccountEntityParams: CargoSuppClientAccountEntity
    {
        public List<CargoSuppClientAccountGoodsEntity> GoodsList { get; set; }
    }

}
