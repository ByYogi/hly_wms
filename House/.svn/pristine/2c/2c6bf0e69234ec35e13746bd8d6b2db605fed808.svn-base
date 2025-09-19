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
        public string ReqBy { get; set; }       // 请求人
        public string AppBy { get; set; }       // 处理人
        public string ReqByName { get; set; }       // 请求人名称
        public string AppByName { get; set; }       // 处理人名称
        public int? ParentRplID { get; set; }       // 父级补货单
        public byte? ScrType { get; set; }      // 触发单类型 (1:销售单;2:移库单)
        public string SrcCode { get; set; }     // 触发单代码
        public int? SrcID { get; set; }     // 触发单ID
        public byte? CreateMethod { get; set; }      // 创建方式 (0:自动;1:手动)
        public int Piece { get; set; }      // 补货数量
        public int? DonePiece { get; set; }      // 补货数量
        public byte? Status { get; set; }        // 状态 (0:待处理;1:已处理;2:补货中;3:已完成;4:已取消)
        public string Reason { get; set; }      // 补货缘由
        public DateTime? ProcessingDate { get; set; }       // 处理时间
        public DateTime? CompletedDate { get; set; }        // 完成时间
        public DateTime? CancelledDate { get; set; }        // 取消时间
        public string Remark { get; set; }      // 备注
        public DateTime CreateDate { get; set; }        // 创建时间
        public DateTime? UpdateDate { get; set; }       // 更新时间

        public List<CargoRplOrderGoodsDto> Rows { get; set; } = new List<CargoRplOrderGoodsDto>();
    }
}
