using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo.Interface
{
    public class StockApiEntity
    {
        /// <summary>
        /// 提供key
        /// </summary>
        public string SystemKey { get; set; }
        /// <summary>
        /// 产品类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 货品代码云配系统编码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 轮胎花纹
        /// </summary>
        public string Figure { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 生产地
        /// </summary>
        public string Born { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 品牌代码
        /// </summary>
        public string BrandCode { get; set; }
        /// <summary>
        /// 轮胎类型
        /// </summary>
        public int TyreType { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品单位
        /// </summary>
        public string ProductUnit { get; set; }
        /// <summary>
        /// 开思系统编码
        /// </summary>
        public string CassProductCode { get; set; }
        /// <summary>
        /// 是否有修改
        /// </summary>
        public int IsUpdate { get; set; }
        /// <summary>
        /// 是否推送
        /// </summary>
        public int IsPush { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockNum { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int OID { get; set; }
        public int ContainerID { get; set; }
        public string InCargoID { get; set; }
        public string TypeName { get; set; }
        public int HouseID { get; set; }
    }
    public class ResponseEntity
    {
        public List<StockApiEntity> DataList { get; set; }
    }
    public class StockApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class StockApiResponseEntity
    {
        public Params Params { get; set; }
        public List<Data> data { get; set; }
    }

    public class Params
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        public long tpsBatchId { get; set; }
        /// <summary>
        /// 是否第一页
        /// </summary>
        public bool firstData { get; set; }
        /// <summary>
        /// 是否最后一页
        /// </summary>
        public bool lastData { get; set; }
        /// <summary>
        /// 更新模式.
        /// </summary>
        public string updateMode { get; set; }
        /// <summary>
        /// 数据类型.
        /// </summary>
        public string dataType { get; set; } = "PRODUCT";
        public ThirdPartySystemParams thirdPartySystemParams { get; set; }
    }

    public class ThirdPartySystemParams
    {
        public string name { get; set; } = "DI_LE_TAI";
        public string version { get; set; } = "1.0";
    }

    public class Data
    {
        public string CassProductCode { get; set; }
        public string ProductName { get; set; }
        public string TypeName { get; set; }
        public decimal SalePrice { get; set; }
        public int StockNum { get; set; }
        public string Batch { get; set; }
        public int HouseID { get; set; }
    }















}
