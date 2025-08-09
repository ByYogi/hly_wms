using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 车辆适配轮胎规格数据实体1.2.7.Tbl_Cargo_CarTyreMatch（车型和轮胎匹配表）
    /// </summary>
    [Serializable]
    public class CargoProductCarTyreEntity
    {
        public int ID { get; set; }
        public string CarBrade { get; set; }
        public string Car { get; set; }
        public string Spec { get; set; }
        public string Brade { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
