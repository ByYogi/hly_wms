using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 新陆程系统与外部系统数据交换实体
    /// </summary>
    [Serializable]
    public class HlyExchangeEntity
    {
        public Int64 ID { get; set; }
        [Description("好来运单号")]
        public string HAwbNo { get; set; }
        [Description("出发站")]
        public string Dep { get; set; }
        [Description("到达站")]
        public string Dest { get; set; }
        [Description("系统件数")]
        public int Piece { get; set; }
        [Description("系统重量")]
        public decimal Weight { get; set; }
        [Description("系统体积")]
        public decimal Volume { get; set; }
        [Description("实到件数")]
        public int ActPiece { get; set; }
        /// <summary>
        /// 未扫描件数
        /// </summary>
        public int NoScanPiece { get; set; }
        [Description("实到重量")]
        public decimal ActWeight { get; set; }
        [Description("实到体积")]
        public decimal ActVolume { get; set; }
        [Description("保费")]
        public decimal InsuranceFee { get; set; }
        [Description("中转费")]
        public decimal TransitFee { get; set; }
        [Description("运输费")]
        public decimal TransportFee { get; set; }
        [Description("配送费")]
        public decimal DeliverFee { get; set; }
        [Description("提货费")]
        public decimal OtherFee { get; set; }
        [Description("总费用")]
        public decimal TotalCharge { get; set; }
        [Description("回扣")]
        public decimal Rebate { get; set; }
        [Description("结算方式")]
        public string CheckOutType { get; set; }
        [Description("代收款")]
        public decimal CollectMoney { get; set; }
        [Description("回单要求")]
        public int ReturnAwb { get; set; }
        [Description("运输方式")]
        public string TrafficType { get; set; }
        [Description("送货方式")]
        public string DeliveryType { get; set; }
        [Description("托运人姓名")]
        public string ShipperName { get; set; }
        [Description("托运人单位")]
        public string ShipperUnit { get; set; }
        [Description("托运人地址")]
        public string ShipperAddress { get; set; }
        [Description("托运人电话")]
        public string ShipperTelephone { get; set; }
        [Description("托运人手机号码")]
        public string ShipperCellphone { get; set; }
        [Description("收货单位")]
        public string AcceptUnit { get; set; }
        [Description("收货地址")]
        public string AcceptAddress { get; set; }
        [Description("收货联系人")]
        public string AcceptPeople { get; set; }
        [Description("收货联系电话")]
        public string AcceptTelephone { get; set; }
        [Description("收货手机号码")]
        public string AcceptCellphone { get; set; }
        [Description("开单受理时间")]
        public DateTime HandleTime { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }
        public string AwbStatus { get; set; }
        [Description("外部系统")]
        public string BelongSystem { get; set; }
        [Description("货物品名")]
        public string Goods { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 保存状态
        /// </summary>
        public string SaveStatus { get; set; }
        /// <summary>
        /// 扫描状态
        /// </summary>
        public string ScanStatus { get; set; }
        /// <summary>
        /// 好来运清单编号
        /// </summary>
        [Description("好来运清单编号")]
        public string HlyAwbNo { get; set; }
        /// <summary>
        /// 好来运五联单号
        /// </summary>
        [Description("好来运五联单号")]
        public string HlyFiveNo { get; set; }
        /// <summary>
        /// 条码标签号子单号
        /// </summary>
        [Description("条码标签号子单号")]
        public string TagCodeNo { get; set; }
        /// <summary>
        /// 包装
        /// </summary>
        public string Package { get; set; }
        /// <summary>
        /// 未扫描子单号
        /// </summary>
        public string NoScanTagCodeNo { get; set; }
        public string ScanTagCodeNo { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OP_DATE { get; set; }
    }
}
