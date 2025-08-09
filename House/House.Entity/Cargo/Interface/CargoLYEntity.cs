using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 联盈系统数据实体
    /// </summary>
    [Serializable]
    public class CargoLYEntity
    {
        public string ak { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string company { get; set; }
    }

    [Serializable]
    public class CargoLYAwbEntity
    {
        public string ak { get; set; }
        public string billNo { get; set; }
        public string shipperTel { get; set; }
        public string company { get; set; }
    }


}
