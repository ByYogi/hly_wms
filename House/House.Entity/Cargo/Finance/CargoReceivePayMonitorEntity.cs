using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 应收应付监督数据实体
    /// </summary>
    [Serializable]
    public class CargoReceivePayMonitorEntity
    {
        public long OrderID { get; set; }
        public string OrderNo { get; set; }
        public int HouseID { get; set; }
        public string Dep { get; set; }
        public string Dest { get; set; }
        public int Piece { get; set; }
        public decimal TransportFee { get; set; }
        public decimal TotalCharge { get; set; }
        public decimal TransitFee { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptAddress { get; set; }
        public string AcceptTelephone { get; set; }
        public string AcceptPeople { get; set; }
        public string AcceptCellphone { get; set; }
        public string CreateAwb { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateAwbID { get; set; }
        public string CheckStatus { get; set; }

        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public string SaleManID { get; set; }
        public string SaleManName { get; set; }
        public string HouseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 财务二审状态
        /// </summary>
        public string FinanceSecondCheck { get; set; }
        /// <summary>
        /// 财务二审人
        /// </summary>
        public string FinanceSecondCheckName { get; set; }
        /// <summary>
        /// 财务二审时间
        /// </summary>
        public DateTime FinanceSecondCheckDate { get; set; }
        /// <summary>
        /// 已收
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        /// <summary>
        /// 未收
        /// </summary>
        public decimal UncollectMoney { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
        public string CargoPermisID { get; set; }
        public string RType { get; set; }
        public string FromTO { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderModel { get; set; }
        /// <summary>
        /// 预收款余额
        /// </summary>
        public decimal PreReceiveMoney { get; set; }
        /// <summary>
        /// 返利余额
        /// </summary>
        public decimal RebateMoney { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }
        public int RowNumber { get; set; }
        public string OrderType { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int ClientNum { get; set; }
        /// <summary>
        /// 付款人编码
        /// </summary> 
        public int PayClientNum { get; set; }
        /// <summary>
        /// 付款人姓名
        /// </summary>
        public string PayClientName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string BelongHouse { get; set; }
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
