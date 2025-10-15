using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order.CargoRpl
{
    public class CargoRplOrderDtlDto
    {
        public int? RplID { get; set; }      // 主键
        public string RplNo { get; set; }      // 编号
        public int? HouseID { get; set; }        // 缺货仓库
        public int? FromHouse { get; set; }      // 补货仓库
        public string HouseName { get; set; }       // 缺货仓库名称
        public string FromHouseName { get; set; }       // 补货仓库名称
        public string UserID { get; set; }       // 请求人
        public string UserName { get; set; }       // 请求人名称
        public int Piece { get; set; }      // 补货数量
        public int? DonePiece { get; set; }      // 补货数量
        public byte? Status { get; set; }        // 状态 (0:待处理;1:已处理;2:补货中;3:已完成;4:已取消)
        public DateTime? ProcessingDate { get; set; }       // 处理时间
        public DateTime? CompletedDate { get; set; }        // 完成时间
        public DateTime? CancelledDate { get; set; }        // 取消时间
        public string Remark { get; set; }      // 备注
        public DateTime CreateDate { get; set; }        // 创建时间
        public DateTime? UpdateDate { get; set; }       // 更新时间

        public List<CargoRplOrderGoodsDto> Rows { get; set; } = new List<CargoRplOrderGoodsDto>();
    }

    public class CargoRplOrderExcelModel
    {
        public int? RplID { get; set; }      // 主键
        public string RplNo { get; set; }      // 编号
        public int? HouseID { get; set; }        // 缺货仓库
        public int? FromHouse { get; set; }      // 补货仓库
        public string HouseName { get; set; }       // 缺货仓库名称
        public string FromHouseName { get; set; }       // 补货仓库名称
        public string UserID { get; set; }       // 请求人
        public string UserName { get; set; }       // 请求人名称
        public int TotalPiece { get; set; }      // 补货数量
        public int? TotalDonePiece { get; set; }      // 补货数量
        public byte? Status { get; set; }        // 状态 (0:待处理;1:已处理;2:补货中;3:已完成;4:已取消)
        public DateTime? ProcessingDate { get; set; }       // 处理时间
        public DateTime? CompletedDate { get; set; }        // 完成时间
        public DateTime? CancelledDate { get; set; }        // 取消时间
        public string Remark { get; set; }      // 备注
        public DateTime CreateDate { get; set; }        // 创建时间
        public DateTime? UpdateDate { get; set; }       // 更新时间



        public int? ID { get; set; }                  // 主键
        public long? ProductID { get; set; }          // 产品ID
        public string ProductName { get; set; }      // 产品名称
        public string ProductCode { get; set; }      // 产品代码
        public string GoodsCode { get; set; }        // 货品代码
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
        public int Piece { get; set; }      // 补货数量
        public int? SysPiece { get; set; }               // 建议补货数量
        public int? DonePiece { get; set; }


        //计算字段
        public string LISS { get => LoadIndex + SpeedLevel; } //LoadIndex SpeedSymbol 载速（载重指数 + 速度级别 ）
    }
}
