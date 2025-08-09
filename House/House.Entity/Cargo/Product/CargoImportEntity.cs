using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// Excel导入结果实体
    /// </summary>
    [Serializable]
    public class CargoImportEntity
    {
        /// <summary>
        /// 导入状态
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 已存在数量
        /// </summary>
        public int ExistCount { get; set; }
        /// <summary>
        /// 冗余数据
        /// </summary>
        public string TempData { get; set; }
    }
}
