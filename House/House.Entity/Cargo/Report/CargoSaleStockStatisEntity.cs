using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 销售库存 表数据 实体
    /// </summary>
    [Serializable]
    public class CargoSaleStockStatisEntity
    {
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; } 
        /// <summary>
        /// 出库总件数
        /// </summary>
        public int OutTotalNum { get; set; }
        /// <summary>
        /// 入库总件数
        /// </summary>
        public int InTotalNum { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }

        /// <summary>
        /// 入库一
        /// </summary>
        public int In1 { get; set; }
        public int In2 { get; set; }
        public int In3 { get; set; }
        public int In4 { get; set; }
        public int In5 { get; set; }
        public int In6 { get; set; }
        public int In7 { get; set; }
        public int In8 { get; set; }
        /// <summary>
        /// 出库一
        /// </summary>
        public int Out1 { get; set; }
        public int Out2 { get; set; }
        public int Out3 { get; set; }
        public int Out4 { get; set; }
        public int Out5 { get; set; }
        public int Out6 { get; set; }
        public int Out7 { get; set; }
        public int Out8 { get; set; }
         
        public int TypeID { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 出库入库百分比
        /// </summary>
        public string OutInRate { get; set; }
        /// <summary>
        /// 当前库存数
        /// </summary>
        public int CurrentNum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
