using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.House
{
    public class ModifySafeStockDto
    {
        public int? HouseID { get; set; }
        public int? AreaID { get; set; }
        public int? TypeID { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public int? StockNum { get; set; }
        public string OPID { get; set; }
        public DateTime? OP_DATE { get; set; }
        public int? MaxStock { get; set; }
        public int? MinStock { get; set; }
        public string ProductCode { get; set; }
        public int? MinStockDay { get; set; }
        public int? MaxStockDay { get; set; }
        public int? HCYCStock { get; set; }
        public int? OEStock { get; set; }
        public int? REStock { get; set; }
        public int? IsolatedStock { get; set; }
        public int? ControlStock { get; set; }

        //------------ 工具字段 ------------
        public int? AvgSaleNum { get; set; }
        
    }
}
