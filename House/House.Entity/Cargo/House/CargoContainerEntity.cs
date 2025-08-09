using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓库货位表管理数据实体Tbl_Cargo_Container
    /// </summary>
    [Serializable]
    public class CargoContainerEntity
    {
        [Description("表主键")]
        public int ContainerID { get; set; }
        [Description("货位代码")]
        public string ContainerCode { get; set; }
        [Description("货位号")]
        public int ContainerNum { get; set; }
        [Description("货位类型")]
        public string ContainerType { get; set; }
        [Description("所在区域ID")]
        public int AreaID { get; set; }
        [Description("可放最大件数")]
        public int MaxPiece { get; set; }
        [Description("可放最大重量")]
        public decimal MaxWeight { get; set; }
        [Description("可放最大体积")]
        public decimal MaxVolume { get; set; }
        [Description("预警件数")]
        public int WarnPiece { get; set; }
        [Description("在货位上件数")]
        public int InPiece { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public int HouseID { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 一级区域ID
        /// </summary>
        public int FirstAreaID { get; set; }
        /// <summary>
        /// 一级区域名称
        /// </summary>
        public string FirstAreaName { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 父区域名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 排放顺序ID
        /// </summary>
        public string EmOrderID { get; set; }
        /// <summary>
        /// 排放顺序名称
        /// </summary>
        public string EmOrder { get; set; }
        public int TypeID { get; set; }
        public int Star { get; set; }
        public string Specs { get; set; }
        /// <summary>
        /// 允许拉上件数
        /// </summary>
        public int allowPiece { get; set; }
        /// <summary>
        /// 产品所在货位上的件数
        /// </summary>
        public int ProductPiece { get; set; } 
        /// <summary>
        /// 是否增加件数
        /// True：增加
        /// False：减少
        /// </summary>
        public bool IsAdd { get; set; }
        /// <summary>
        /// 货位星级排序数据列表
        /// </summary>
        public List<CargoContainerStarEntity> ContainerGoodsList { get; set; }
        public string CargoPermisID { get; set; }
    }
}
