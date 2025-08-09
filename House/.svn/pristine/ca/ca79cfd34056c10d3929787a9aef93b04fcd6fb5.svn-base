using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 出库单数据实体 1.1.6.Tbl_Cargo_OutContainerGoods（产品出库表）
    /// </summary>
    [Serializable]
    public class CargoContainerOutEntity
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
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        [Description("出库时间")]
        public DateTime OutHouseTime { get; set; }
        /// <summary>
        /// 0：False，未打印
        /// 1：True，已打印
        /// </summary>
        [Description("是否打印出库单")]
        public bool IsPrintOutCargo { get; set; }
        /// <summary>
        /// 是否增加件数
        /// True：增加
        /// False：减少
        /// </summary>
        public bool IsAdd { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        [Description("出库单号")]
        public string OutCargoID { get; set; }

        /// <summary>
        /// 查询条件是否打印出库单
        /// </summary>
        public string IsPrint { get; set; }
        /// <summary>
        /// 出库类型
        /// </summary>
        public string OutHouseType { get; set; }



    }
}
