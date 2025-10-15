using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order
{
    public class CargoOOSLogGoodsDto
    {
        public int? ID { get; set; }                  // 主键
        public int? RplID { get; set; }               // 补货单ID
        public long? ProductID { get; set; }          // 产品ID
        public long? SID { get; set; }          // 安全库存ID
        public string ProductName { get; set; }      // 产品名称
        public string ProductCode { get; set; }      // 产品代码
        public string GoodsCode { get; set; }        // 货品代码
        public int? HouseID { get; set; }
        public string HouseName { get; set; }
        public int? AreaID { get; set; }
        public string AreaName { get; set; }
        public int? TypeCate { get; set; } // 品类ID
        public string TypeCateName { get; set; } // 品类ID
        public int? TypeID { get; set; }              // 品牌ID
        public string TypeName { get; set; }         // 品牌名称
        public string Specs { get; set; } //规格
        public string Figure { get; set; } //胎纹
        public string LoadIndex { get; set; } //载重
        public string SpeedLevel { get; set; } //速度级别
        public int? Piece { get; set; }               // 补货数量
        public int? SysPiece { get; set; }               // 建议补货数量
        public int? DonePiece { get; set; }               

        public int? MinStock { get; set; }           // 最小库存数
        public int? MaxStock { get; set; }           // 最大库存数
        public int? SrcPiece { get; set; }           // 缺货仓在库数
        public int? RestockingQty { get; set; }        // 待出库数量
        public int? InTransitQty { get; set; }       // 在途库存
        public int? AvgSalSUM { get; set; }          // 月均销量
        public DateTime? CreateDate { get; set; }         
        public DateTime? UpdateDate { get; set; }

        //工具字段
        public int? ParentHouseID { get; set; }
        public string ParentHouseName { get; set; }

        //计算字段
        public string LISS { get => LoadIndex + SpeedLevel; } //LoadIndex SpeedSymbol 载重指数 + 速度级别
        public int SrcRealPiece { get => SrcPiece.GetValueOrDefault() + InTransitQty.GetValueOrDefault(); }


    }

    public class CargoOOSLogGoodsListDto : ListResponsBase<CargoRplOrderGoodsDto>
    {
    }
}
