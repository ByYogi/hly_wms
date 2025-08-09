using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信商城订单数据实体
    /// </summary>
    [Serializable]
    public class WXOrderManagerEntity
    {
        public Int64 ID { get; set; }
        public string OrderNo { get; set; }
        public Int64 WXID { get; set; }
        public int Piece { get; set; }
        public decimal TotalCharge { get; set; }
        public decimal TransitFee { get; set; }
        public DateTime CreateDate { get; set; }
        public string PayStatus { get; set; }
        public string PayWay { get; set; }
        public string OrderStatus { get; set; }
        public string WXPayOrderNo { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string AcceptAddress { get; set; }
        public string Name { get; set; }
        public string Cellphone { get; set; }
        /// <summary>
        /// 上架商品表主键ID
        /// </summary>
        public Int64 ShelvesID { get; set; }
        /// <summary>
        /// 订单商品数量
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 订单商品价格
        /// </summary>
        public decimal OrderPrice { get; set; }
        public string ProductName { get; set; }
        public Int64 ProductID { get; set; }
        public int TypeID { get; set; }
        public string OrderType { get; set; }
        /// <summary>
        /// 在上架的商品数量
        /// </summary>
        public int OnSaleNum { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 在库表主键ID
        /// </summary>
        public Int64 InContainID { get; set; }
        public int ContainerID { get; set; }
        public string ContainerCode { get; set; }
        /// <summary>
        /// 仓库在库数量
        /// </summary>
        public int InCargoPiece { get; set; }
        public string AreaName { get; set; }
        public int HouseID { get; set; }
        public string wxOpenID { get; set; }
        public string wxName { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int ClientNum { get; set; }
        public int BatchYear { get; set; }
        public string Specs { get; set; }
        public string Batch { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; }
        public string Model { get; set; }
        public string SpeedLevel { get; set; }
        public string SaleType { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 查询所属仓库ID
        /// </summary>
        public string CargoPermisID { get; set; }
        public DateTime OP_DATE { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LogisID { get; set; }

        public string AvatarBig { get; set; }
        public string WXOrderNo { get; set; }
        public string OutHouseName { get; set; }
        public string CheckStatus { get; set; }
        public string SaleManID { get; set; }
        public int Consume { get; set; }
        /// <summary>
        /// 是否APP商城首单 0：否1：是
        /// </summary>
        public string IsAppFirstOrder { get; set; }
        public long CouponID { get; set; }
        public int CouponMoney { get; set; }
        public int CutEntry { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleTitle { get; set; }
        public string GoodEvaluate { get; set; }
        public string LogisEvaluate { get; set; }
        public string EvaluateMemo { get; set; }
    }
}
