using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 货位与货物类型关联实体1.1.4.Tbl_Cargo_ContainerGoods（货位与产品关联表）
    /// </summary>
    [Serializable]
    public class CargoContainerGoodsEntity
    {
        [Description("表主键")]
        public Int64 ID { get; set; }
        [Description("货位ID")]
        public int ContainerID { get; set; }
        [Description("分类ID")]
        public int TypeID { get; set; }
        [Description("产品ID")]
        public long ProductID { get; set; }
        [Description("件数")]
        public int Piece { get; set; }
        [Description("重量")]
        public decimal Weight { get; set; }
        [Description("优先级")]
        public int Star { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        [Description("入库时间")]
        public DateTime InHouseTime { get; set; }
        /// <summary>
        /// 0：False，未打印
        /// 1：True，已打印
        /// </summary>
        [Description("是否打印入库单")]
        public bool IsPrintInCargo { get; set; }
        /// <summary>
        /// 是否增加件数
        /// True：增加
        /// False：减少
        /// </summary>
        public bool IsAdd { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        [Description("入库单号")]
        public string InCargoID { get; set; }
        /// <summary>
        /// 入库类型
        /// </summary>
        public string InHouseType { get; set; }
        public int OldPiece { get; set; }
        public int NewPiece { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 花纹
        /// </summary>
        public string Figure { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 货位代码
        /// </summary>
        public string ContainerCode { get; set; }
        public decimal SalePrice { get; set; }
        public decimal InHousePrice { get; set; }
        public decimal UnitPrice { get; set; }
        
    }
}
