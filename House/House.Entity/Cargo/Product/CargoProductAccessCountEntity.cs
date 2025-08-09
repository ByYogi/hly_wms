using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo.Product
{
    /// <summary>
    /// 访问计数表
    /// </summary>
    public class CargoProductAccessCountEntity
    {
        /// <summary>
        /// 表主键
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// 所属仓库ID
        /// </summary>
        public Int32 HouseID { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public Int32 SuppClientNum { get; set; }
        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime AccessTime { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public Int32 AccessCount { get; set; }
    }
}
