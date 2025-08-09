using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 小程序促销规则数据实体 Tbl_Cargo_RuleBank
    /// </summary>
    [Serializable]
    public class RuleBankEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RuleBankInfo> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    public class RuleBankInfo
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 优惠规则名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 是否可叠加使用  0：否1：是
        /// </summary>
        public int IsSuperPosition { get; set; }
        /// <summary>
        /// 是否限制条数    0 否  1 是
        /// </summary>
        public int IsFollowQuantity { get; set; }
        /// <summary>
        /// 规则类型0：平台规则  1：供应商规则

        /// </summary>
        public int CouponType { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public int SuppClientNum { get; set; }
        /// <summary>
        /// 发放优惠卷
        /// </summary>
        public int IssueCoupon { get; set; }
        /// <summary>
        /// 优惠卷使用时间
        /// </summary>
        public int ServiceTime { get; set; }
        /// <summary>
        /// 发放赠品
        /// </summary>
        public int IssueGiveaway { get; set; }
        /// <summary>
        /// 规则有效期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 规则有效期
        /// </summary>
        public DateTime EndDate { get; set; }
        [Description("适用规格")]
        public string Specs { get; set; }
        [Description("适用花纹")]
        public string Figure { get; set; }
        [Description("货品代码")]
        public string GoodsCode { get; set; }
        [Description("载重指数")]
        public string LoadIndex { get; set; }
        [Description("速度级别")]
        public string SpeedLevel { get; set; }
        [Description("开始周期")]
        public int StartBatch { get; set; }
        [Description("结束周期")]
        public int EndBatch { get; set; }
        public string ProductCode { get; set; }
        [Description("满数量金额")]
        public int FullEntry { get; set; }
        [Description("减数量金额")]
        public decimal CutEntry { get; set; }
        [Description("折扣")]
        public decimal SaleEntry { get; set; }
        [Description("限额")]
        public int LimitNum { get; set; }
        /// <summary>
        /// 满额享受
        /// </summary>
        [Description("满额享受")]
        public int MeetLimit { get; set; }
        /// <summary>
        /// 常规促销
        /// </summary>
        [Description("常规促销")]
        public int Regular { get; set; }
        public string RuleType { get; set; }
    }

}
