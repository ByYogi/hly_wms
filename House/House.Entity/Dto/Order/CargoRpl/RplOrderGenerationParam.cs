using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order.CargoRpl
{
    public class UpdateOOSParam
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public byte SrcType { get; set; } // 触发单类型 (1:销售单;2:移库单;3:补货单)
        public string ReasonTag { get; set; }
        public string Reason { get; set; }
        public int? SrcID { get; set; }
        public string SrcCode { get; set; }
        public List<UpdateOOSGoodsParam> Rows { get; set; }
    }
    public class UpdateOOSGoodsParam
    {
        public long ProductID { get; set; }
        public int AreaID { get; set; }
    }
}
