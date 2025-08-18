using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto
{
    public class ContainerGoodsInsertDto
    {
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
        //[Description("操作时间")]
        //public DateTime? OP_DATE { get; set; }
        [Description("入库单号")]
        public string InCargoID { get; set; }

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

    }
}
