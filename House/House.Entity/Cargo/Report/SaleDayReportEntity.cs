using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo.Report
{
    /// <summary>
    /// 销售日收入报表
    /// </summary>
    [Serializable]
    public class SaleDayReportEntity
    {
        #region 查询
        /// <summary>
        /// 所属权限仓库
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public int SuppClientNum { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThrowGood { get; set; }
        public string OrderModel { get; set; }
        public string DeliveryType { get; set; }
        public decimal TransportFee { get; set; }
        public int Piece { get; set; }
        #endregion
        #region 返回
        /// <summary>
        /// 自提产品数
        /// </summary>
        public int SelfPiece { get; set; }
        /// <summary>
        /// 自提销售额
        /// </summary>
        public decimal SelfMoney { get; set; }
        /// <summary>
        /// 配送产品数
        /// </summary>
        public int DeliveryPiece { get; set; }
        /// <summary>
        /// 配送销售额
        /// </summary>
        public decimal DeliveryMoney { get; set; }
        /// <summary>
        /// 运营总收入
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 渠道单收入
        /// </summary>
        public decimal ChannelTotal { get; set; }
        /// <summary>
        /// 即日达收入
        /// </summary>
        public decimal ToDayTotal { get; set; }
        /// <summary>
        /// 次日达收入
        /// </summary>
        public decimal NextDayTotal { get; set; }
        /// <summary>
        /// 即日达退货金额
        /// </summary>
        public decimal ToDayReturnTotal { get; set; }
        /// <summary>
        /// 次日达退货金额
        /// </summary>
        public decimal NextDayReturnTotal { get; set; }

        #endregion
    }
}
