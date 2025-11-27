using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 采购账单表 Tbl_Cargo_RealPurchaseAccount
    /// 描述：系统订单数据表，维护客户订单数据信息
    /// </summary>
    [Serializable]
    public class CargoRealPurchaseAccountEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public long AccountID { get; set; }

        /// <summary>
        /// 账单号（格式：BP+日期+顺序号）
        /// </summary>
        [Description("账单号")]
        public string AccountNO { get; set; }

        /// <summary>
        /// 账单名称
        /// </summary>
        [Description("账单名称")]
        public string AccountTitle { get; set; }

        /// <summary>
        /// 创建账单日期
        /// </summary>
        [Description("创建账单日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 采购商表主键
        /// </summary>
        [Description("采购商表主键")]
        public int PurchaserID { get; set; }

        /// <summary>
        /// 采购商名称
        /// </summary>
        [Description("采购商名称")]
        public string PurchaserName { get; set; }

        /// <summary>
        /// 本期订总金额
        /// </summary>
        [Description("本期订总金额")]
        public decimal ARMoney { get; set; }

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
        /// 账单金额
        /// </summary>
        [Description("账单金额")]
        public decimal Total { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [Description("备注说明")]
        public string Memo { get; set; }

        /// <summary>
        /// 账单审核状态（0：待审，1：通过，2：拒审，3：结束）
        /// </summary>
        [Description("账单审核状态")]
        public byte Status { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [Description("审批人ID")]
        public string NextCheckID { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        [Description("审批人姓名")]
        public string NextCheckName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [Description("审批时间")]
        public DateTime? NextCheckDate { get; set; }

        /// <summary>
        /// 结算状态（0：未结算，1：已结清，2：未结算）
        /// </summary>
        [Description("结算状态")]
        public byte? CheckStatus { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Description("操作员")]
        public string OPID { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Description("操作时间")]
        public DateTime OPDATE { get; set; }

        /// <summary>
        /// 账单类型（0：采购账单）
        /// </summary>
        [Description("账单类型")]
        public byte AType { get; set; }

        /// <summary>
        /// 电子签名（0：未签名，1：已签名）
        /// </summary>
        [Description("电子签名")]
        public byte? ElecSign { get; set; }

        /// <summary>
        /// 签名时间
        /// </summary>
        [Description("签名时间")]
        public DateTime? ElecSignDate { get; set; }

        /// <summary>
        /// 签名照片路径
        /// </summary>
        [Description("签名照片路径")]
        public string ElecSignImg { get; set; }

        public List<CargoRealPurchaseAccountGoodsEntity> goodsList { get; set; }

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