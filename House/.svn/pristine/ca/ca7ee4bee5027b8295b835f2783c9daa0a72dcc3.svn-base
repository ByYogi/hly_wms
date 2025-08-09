using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 货位预先存放货物产品的优先级 Tbl_Cargo_ContainerStar（货位星级表）
    /// </summary>
    [Serializable]
    public class CargoContainerStarEntity
    {
        [Description("表主键")]
        public Int64 ID { get; set; }
        [Description("货位ID")]
        public int ContainerID { get; set; }
        [Description("分类ID")]
        public int TypeID { get; set; }
        [Description("产品ID")]
        public int ProductID { get; set; }
        [Description("优先级")]
        public int Star { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
