using House.Entity.Cargo;
using House.Entity.Dto.Order.CargoRpl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Purchase
{
    public class RealFactoryPODetailDto
    {
        public string PurOrderNo { get; set; }
        public string PurDepart { get; set; }
        public byte? OwnerShip { get; set; }
        public byte? WhetherTax { get; set; }
        public string PurchaserName { get; set; }
        public byte? PurchaseType { get; set; }
        public byte? PurchaseInStoreType { get; set; }
        public string CreateAwb { get; set; }
        public int? PaymentMethod { get; set; }
        public string TypeName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public int? Piece { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class RealFactoryPODetailListDto : ListResponsBase<RealFactoryPODetailDto>
    {
    }

    public class RealFactoryPODetailParams : PaginationBase
    {
        public string TypeName { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; }
        public string PurOrderNo { get; set; }
        public string ProductCode { get; set; }
        public string PurchaserName { get; set; }
        public byte? PurchaseType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
