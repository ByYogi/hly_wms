using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 城市 数据 实体
    /// </summary>
    [Serializable]
    public class CargoCityEntity
    {
        public int ID { get; set; }
        public string City { get; set; }
        public int ParentID { get; set; }

    }
}
