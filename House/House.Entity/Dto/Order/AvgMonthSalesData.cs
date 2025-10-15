using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order
{
    public class AvgMonthSalesData
    {
        public string ProductCode { get; set; }
        public int TypeID { get; set; }
        public int HouseID { get; set; }
        public int AreaID { get; set; }
        public int TotalPiece { get; set; }
        public int AvgMonthSale { get; set; }
    }

    public class AvgMonthSalesData_qryParam
    {
        public string ProductCode { get; set; }
        public int HouseID { get; set; }
        public int AreaID { get; set; }
        public int TypeID { get; set; }
    }
}
