using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    [Serializable]
    public class CargoOrderOutTimeEntity
    {
        [Description("所在仓库ID")]
        public int HouseID { get; set; }
        [Description("所在仓库")]
        public string HouseName { get; set; }

        [Description("订单总数")]
        public string OrderCount1 { get; set; }
        [Description("出库总数")]
        public string PieceCount1 { get; set; }
        [Description("开始扫描总时效")]
        public string StartInterval1 { get; set; }
        [Description("开始扫描平均时效")]
        public string AverageStartInterval1 { get; set; }
        [Description("结束扫描总时效")]
        public string OutInterval1 { get; set; }
        [Description("结束扫描平均时效")]
        public string AverageEndInterval1 { get; set; }
        public string OrderCount2 { get; set; }
        public string PieceCount2 { get; set; }
        public string StartInterval2 { get; set; }
        public string AverageStartInterval2 { get; set; }
        public string OutInterval2 { get; set; }
        public string AverageEndInterval2 { get; set; }
        public string OrderCount3 { get; set; }
        public string PieceCount3 { get; set; }
        public string StartInterval3 { get; set; }
        public string AverageStartInterval3 { get; set; }
        public string OutInterval3 { get; set; }
        public string AverageEndInterval3 { get; set; }
        public string OrderCount4 { get; set; }
        public string PieceCount4 { get; set; }
        public string StartInterval4 { get; set; }
        public string AverageStartInterval4 { get; set; }
        public string OutInterval4 { get; set; }
        public string AverageEndInterval4 { get; set; }
        public string OrderCount5 { get; set; }
        public string PieceCount5 { get; set; }
        public string StartInterval5 { get; set; }
        public string AverageStartInterval5 { get; set; }
        public string OutInterval5 { get; set; }
        public string AverageEndInterval5 { get; set; }
        public string OrderCount6 { get; set; }
        public string PieceCount6 { get; set; }
        public string StartInterval6 { get; set; }
        public string AverageStartInterval6 { get; set; }
        public string OutInterval6 { get; set; }
        public string AverageEndInterval6 { get; set; }



        [Description("订单号")]
        public string OrderNo { get; set; }
        [Description("总件数")]
        public int Piece { get; set; }
        [Description("运单状态")]
        public string AwbStatus { get; set; }
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }
        [Description("开始扫描时间")]
        public DateTime StartOutTime { get; set; }
        [Description("开单至第一次出库间隔时间")]
        public string StartOutIntervalTime { get; set; }
        [Description("最后扫描时间")]
        public DateTime EndOutTime { get; set; }
        [Description("开单至最后出库间隔时间")]
        public string EndOutIntervalTime { get; set; }
        [Description("标签编码")]
        public string TagCode { get; set; }
        [Description("所属货位")]
        public string ContainerCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 订单时效数据统计实体
    /// </summary>
    [Serializable]
    public class CargoReportAgeingEntity
    {
        public int HouseID { get; set; }

        public string CargoPermisID { get; set; }

        public string AbSignStatus { get; set; }
        public string HouseName { get; set; }
        public string OutHouseName { get; set; }
        public string OrderNo { get; set; }
        public int Piece { get; set; }
        public string AwbStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OutCargoTime { get; set; }
        public DateTime SignTime { get; set; }
        public DateTime TakeOrderTime { get; set; }
        public DateTime SendCarTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Signer { get; set; }
        public string SignImage { get; set; }
        /// <summary>
        /// 大区
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        public string HAwbNo { get; set; }

        public string OrderType { get; set; }


        public string LogisAwbNo { get; set; }
        public string AcceptUnit { get; set; }
        public string CreateAwb { get; set; }

        public int LineID { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string LineName { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string PayClientName { get; set; }
        /// <summary>
        /// 店代码
        /// </summary>
        public string ShopCode { get; set; }
        /// <summary>
        /// 开单至最后出库间隔时间 小时保留两位小数
        /// </summary>
        public string OutCargoAgeing { get; set; }
        /// <summary>
        /// 从开单到签收 签收时效  小时，保留两位小数
        /// </summary>
        public string FromCreateToSignAgeing { get; set; }
        /// <summary>
        /// 从出库完成到签收 签收时效 小时，保留两位小数
        /// </summary>
        public string FromOutCargoToSignAgeing { get; set; }
    }
}
