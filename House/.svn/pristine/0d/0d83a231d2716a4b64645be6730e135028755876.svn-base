using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order.CargoRpl
{
    public class CargoOutOfStockDto
    {
        public int? OOSID { get; set; }
        public int? OOSLogID { get; set; }
        public int? OOSLogRowID { get; set; }
        public long? ProductID { get; set; }
        public long? SID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string GoodsCode { get; set; }
        public int? HouseID { get; set; }
        public string HouseName { get; set; }
        public int? ParentHouse { get; set; }
        public string ParentHouseName { get; set; }
        public int? AreaID { get; set; }
        public string AreaName { get; set; }
        public int? TypeCate { get; set; }
        public string TypeCateName { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
        public int? OldPiece { get; set; }
        public int? Piece { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //额外查询字段
        public int? InTransitStock { get; set; }
        public int? CurStock { get; set; }
        public int? RestockingPiece { get; set; }
        public string Model { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Batch { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }
    }
    public class CargoOutOfStockListDto : ListResponsBase<CargoOutOfStockDto>
    {
    }

    public class CargoOutOfStockParams : PaginationBase
    {
        public int? TypeCate { get; set; }
        public int? TypeID { get; set; }
        public int? HouseID { get; set; }
        public int? AreaID { get; set; }
        public int? ParentHouse { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
    }
}
