using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo.Order
{
    public class CargoRplOrderGoods
    {
        public int ID { get; set; }                  // 主键
        public int RplID { get; set; }               // 补货单ID
        public long ProductID { get; set; }          // 产品ID
        public string ProductName { get; set; }      // 产品名称
        public string ProductCode { get; set; }      // 产品代码
        public string GoodsCode { get; set; }        // 货品代码
        public int TypeCate { get; set; } // 品类ID
        public int TypeID { get; set; }              // 品牌ID
        public string TypeName { get; set; }         // 品牌名称
        public int Piece { get; set; }               // 补货数量
        public int SysPiece { get; set; }            // 建议补货数
        public int? MinStock { get; set; }           // 最小库存数
        public int? MaxStock { get; set; }           // 最大库存数
        public int? SrcPiece { get; set; }           // 缺货仓在库数
        public int? PenddimgQty { get; set; }        // 待出库数量
        public int? InTransitQty { get; set; }       // 在途库存
        public int? AvgSalSUM { get; set; }          // 月均销量
        public DateTime CreateDate { get; set; }     // 创建时间
        public DateTime? UpdateDate { get; set; }    // 更新时间
    }
}
