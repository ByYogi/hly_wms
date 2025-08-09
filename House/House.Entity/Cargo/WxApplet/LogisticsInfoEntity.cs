using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 订单物流信息数据实体
    /// </summary>
    [Serializable]
    public class LogisticsInfoEntity
    {
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LogisticsEntity> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 物流数据信息
    /// </summary>
    [Serializable]
    public class LogisticsEntity
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPDATE { get; set; }
        public string Status { get; set; }
        public string Memo { get; set; }
        public string SignImage { get; set; }
    }
}
