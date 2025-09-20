using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order.CargoRpl
{
    public class RplOrderAutoGeneratParam
    {
        public string ReqBy { get; set; }
        public string ReqByName { get; set; }
        public int SrcType { get; set; }
        public int SrcID { get; set; }
        public string SrcCode { get; set; }
        public List<RplOrderAutoGeneratParam_Goods> GoodsList { get; set; }
    }
    public class RplOrderAutoGeneratParam_Goods
    {
        public long ProductID { get; set; }
        public int AreaID { get; set; }
    }
}
