using House.Entity.Cargo;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Order
{
    public class CargoRplOrderDto
    {
        //展示相关字段
        public int? RplID { get; set; }               // 主键
        public string RplNo { get; set; }           // 编号
        public int? HouseID { get; set; }             // 补货仓库
        public string HouseName { get; set; }             // 补货仓库
        public int? FromHouse { get; set; }           // 来源仓库
        public string FromHouseName { get; set; }           // 来源仓库
        public string ReqBy { get; set; }            // 触发人
        public string ReqByName { get; set; }        // 触发人名称
        public int? Piece { get; set; }               // 补货数量
        public int? DonePiece { get; set; }               // 补货数量
        public byte? Status { get; set; }             // 状态 (0:待处理;1:已处理;2:补货中;3:已完成;4:已取消)
        public string TypeNames { get; set; }             // 品牌名称组
        public string Reason { get; set; }           // 补货缘由
        public string Remark { get; set; }           // 备注
        public int? SpendDays { get {
                if (!CreateDate.HasValue || !CompletedDate.HasValue)
                    return null;
                return (int)Math.Ceiling((CompletedDate.Value - CreateDate.Value).TotalDays); 
            } 
        }  //花费天数
        public DateTime? CreateDate { get; set; }
        public DateTime? CompletedDate { get; set; }        // 完成时间


        //筛选相关字段
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Specs { get; set; }
        public int? TypeID { get; set; }
        public int? TypeCate { get; set; }

    }
    
    public class CargoRplOrderListDto : ListResponsBase<CargoRplOrderDto>
    {
    }

    public class CargoRplOrderParams: PaginationBase
    {
        //筛选相关字段
        public int? RplID { get; set; }               // 主键
        public List<int> RplIDs { get; set; }           // 主键列表
        public string RplNo { get; set; }           // 编号
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Specs { get; set; }
        public int? TypeID { get; set; }
        public int? TypeCate { get; set; }
        public byte? Status { get; set; }             // 状态 (0:待处理;1:已处理;2:补货中;3:已完成;4:已取消)
        public int? HouseID { get; set; }             // 补货仓库
        public int? FromHouse { get; set; }           // 来源仓库
        public string ReqBy { get; set; }            // 触发人
        public string ReqByName { get; set; }        // 触发人名称
    }
}
