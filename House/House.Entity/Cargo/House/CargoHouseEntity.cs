using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓储管理系统仓库管理数据实体Tbl_Cargo_House
    /// </summary>
    [Serializable]
    public class CargoHouseEntity
    {
        [Description("表主键")]
        public int HouseID { get; set; }
        [Description("仓库名称")]
        public string Name { get; set; }
        [Description("仓库代码")]
        public string HouseCode { get; set; }
        [Description("仓库责任人")]
        public string Person { get; set; }
        [Description("手机号码")]
        public string Cellphone { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("删除标志")]
        public string DelFlag { get; set; }
        [Description("仓库类型")]
        public string BelongHouse { get; set; }
        [Description("仓库地址")]
        public string Address { get; set; }
        [Description("经度")]
        public string Lng { get; set; }
        [Description("纬度")]
        public string Lat { get; set; }
        /// <summary>
        /// 拣货表头
        /// </summary>
        public string PickTitle { get; set; }
        /// <summary>
        /// 发货表头
        /// </summary> 
        public string SendTitle { get; set; }
        /// <summary>
        /// 配送区域范围
        /// </summary>
        public string DeliveryArea { get; set; }
        /// <summary>
        /// 发货城市 
        /// </summary>
        public string DepCity { get; set; }
        public string CargoDepart { get; set; }
        public string BusinessID { get; set; }
        public string OESCargoDepart { get; set; }
        public string StartBusHours { get; set; }
        public string EndBusHours { get; set; }
        /// <summary>
        /// 慧采云仓订单推送标签ID
        /// </summary>
        public int HCYCOrderPushTagID { get; set; }
        /// <summary>
        /// 入驻品牌
        /// </summary>
        public string OperaBrand { get; set; }
        /// <summary>
        /// 一条费用
        /// </summary>
        public decimal LogisFee { get; set; }
        /// <summary>
        /// 两条费用 
        /// </summary>
        public decimal TwoLogisFee { get; set; }
        /// <summary>
        /// 三条费用
        /// </summary>
        public decimal ThreeLogisFee { get; set; }
        /// <summary>
        /// 次日达运费
        /// </summary>
        public decimal NextDayLogisFee { get; set; }
        /// <summary>
        /// 超期单价费用
        /// </summary>
        public decimal OverDueUnitPrice { get; set; }
        /// <summary>
        /// 超期天数
        /// </summary>
        public int OverDayNum { get; set; }
        /// <summary>
        /// 是否允许急送0:允许1:否
        /// </summary>
        public string IsCanRush { get; set; }
        /// <summary>
        /// 是否允许自提0:允许1:否
        /// </summary>
        public string IsCanPickUp { get; set; }
        /// <summary>
        /// 是否允许次日达0:允许1:否
        /// </summary>
        public string IsCanNextDay { get; set; }
        /// <summary>
        /// 库存共享仓库ID
        /// </summary>
        public int StockShareHouseID { get; set; }
        /// <summary>
        /// 分账客户编码（通联）
        /// </summary>
        public string BizUserId { get; set; }
        /// <summary>
        /// 父仓库ID
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 父仓库名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 开思系统自动开单 0：否1：是
        /// </summary>
        public string IsCassAutoOrder { get; set; }
        /// <summary>
        /// 马牌系统自动开单 0：否1：是
        /// </summary>
        public string IsContiAutoOrder { get; set; }
        /// <summary>
        /// 途虎系统自动开单 0：否1：是
        /// </summary>
        public string IsTuhuAutoOrder { get; set; }
        /// <summary>
        /// 三头六臂系统自动开单 0：否1：是
        /// </summary>
        public string IsSanAutoOrder { get; set; }
        /// <summary>
        /// 天猫系统自动开单 0：否1：是
        /// </summary>
        public string IsTMaoAutoOrder { get; set; }

        

        /// 去NULL,替换危险字符
        /// </summary>
        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();

            foreach (PropertyInfo s in pSource)
            {
                if (s.PropertyType.Name.ToUpper().Contains("STRING"))
                {
                    if (s.GetValue(this, null) == null)
                        s.SetValue(this, "", null);
                    else
                        s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
                }
            }
        }
    }

    // 仍然需要这个响应模型来反序列化
    public class TianMaoApiResponse
    {
        public int Code { get; set; }
        public string Info { get; set; }
        public string Msg { get; set; }
    }
    /// <summary>
    /// 订单主信息
    /// </summary>
    public class TMallOrderInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [JsonProperty("uniqueKey")]
        public string uniqueKey { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [JsonProperty("bizType")]
        public int bizType { get; set; }

        /// <summary>
        /// 仓库编码（天猫仓库编码）
        /// </summary>
        [JsonProperty("warehouseCode")]
        public string warehouseCode { get; set; }
        public string supplyId { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        [JsonProperty("itemList")]
        //public List<TMallOrderItem> ItemList { get; set; } = new List<TMallOrderItem>();
        //public string ItemList { get; set; }
        public List<TMallOrderItem> itemList { get; set; }

        /// <summary>
        /// 第三方仓库编码
        /// </summary>
        [JsonProperty("thirdWarehouseCode")]
        public string thirdWarehouseCode { get; set; }

        /// <summary>
        /// 操作时间（毫秒时间戳）
        /// </summary>
        [JsonProperty("operateTime")]
        public long operateTime { get; set; }

        /// <summary>
        /// 转换为DateTime（可选扩展方法）
        /// </summary>
        public DateTime GetOperateDateTime()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(operateTime).LocalDateTime;
        }
    }

    /// <summary>
    /// 订单商品项
    /// </summary>
    public class TMallOrderItem
    {

        /// <summary>
        /// 商品编码
        /// </summary>
        [JsonProperty("skuCode")]
        public string skuCode { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [JsonProperty("skuQuantity")]
        public int skuQuantity { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("skuName")]
        public string skuName { get; set; }
    }
}
