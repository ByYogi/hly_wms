using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 马牌库存同步数据实体
    /// </summary>
    [Serializable]
    public class CargoContiStockSyncEntity
    {
        public string outNo { get; set; }

        public List<CargoContiStockListEntity> stockList { get; set; }
    }

    public class CargoContiStockListEntity
    {
        public string warehouseName { get; set; }
        public List<CargoContiStockSKUEntity> skuQtyList { get; set; }
    }

    public class CargoContiStockSKUEntity
    {
        public string skuCode { get; set; }

        public List<CargoContiStockDOTEntity> dots { get; set; }
    }

    public class CargoContiStockDOTEntity
    {
        public string dot { get; set; }
        public int qty { get; set; }
    }
}
