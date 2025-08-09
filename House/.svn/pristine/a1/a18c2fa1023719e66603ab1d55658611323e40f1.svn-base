using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 财务收支记录表数据实体1.6.2.Tbl_Cargo_FinanceRecord（财务收支记录表）
    /// </summary>
    [Serializable]
    public class CargoCashierEntity
    {
        [Description("记录ID号")]
        public Int64 RecordID { get; set; }
        [Description("银行卡ID")]
        public int BasicID { get; set; }
        /// <summary>
        /// 现金ID
        /// </summary>
        public int CashID { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>
        public int WxID { get; set; }
        /// <summary>
        /// 支付宝ID
        /// </summary>
        public int AliID { get; set; }
        [Description("收支类型")]
        public string RType { get; set; }
        [Description("受影响前余额")]
        public decimal BeforeMoney { get; set; }
        [Description("实时余额")]
        public decimal OverMoney { get; set; }
        [Description("涉及微信")]
        public decimal AffectWX { get; set; }
        [Description("涉及现金")]
        public decimal AffectCash { get; set; }
        [Description("涉及银行卡")]
        public decimal AffectCard { get; set; }
        [Description("涉及支付宝")]
        public decimal AffectAlipay { get; set; }
        [Description("收支来源")]
        public string FromTO { get; set; }
        [Description("涉及单号")]
        public string AffectAwbNO { get; set; }
        [Description("涉及客户")]
        public string AffectClient { get; set; }
        [Description("付款人")]
        public string PayName { get; set; }
        [Description("付款人电话")]
        public string PayPhone { get; set; }
        [Description("付款日期")]
        public DateTime PayDate { get; set; }
        [Description("备注")]
        public string Memo { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public string CardType { get; set; }
        public string Aliases { get; set; }
        public string Bank { get; set; }
        public string CardName { get; set; }
        public string CardNum { get; set; }
        public List<string> OrderNo { get; set; }
        /// <summary>
        /// 客户预收款
        /// </summary>
        public List<CargoClientPreRecordEntity> clientPreRecord { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CheckStatus { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 查询所属仓库订单
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 所属仓库名称
        /// </summary>
        public string HouseName { get; set; }
        public string HouseID { get; set; }
        /// <summary>
        /// 交易类型0:账号交易1:预收款交易
        /// </summary>
        public string TradeType { get; set; }
        public int ClientNum { get; set; }
        public string OldFromTO { get; set; }
        public int RevokeType { get; set; }
        public string WxOrderNo { get; set; }
        

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
