using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class CargoFactoryOrderBarcodeEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// 船运单号
        /// </summary>
        public string VehicleNo { get; set; }
        /// <summary>
        /// 轮胎条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RDC { get; set; }
        /// <summary>
        /// 货品代码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime OutTime { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 花纹
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
        /// 操作时间
        /// </summary>
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
    }
    public class RetunBarcodeJson
    {
        public string code { get; set; }
        public List<data> data;
    }
    public class data
    {
        /// <summary>
        /// 轮胎条码
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 表示是哪个RDC出库
        /// </summary>
        public string RDC { get; set; }
        /// <summary>
        /// 轮胎7位物料号article，偶尔会有部分缺少物料信息，此时值为null
        /// </summary>
        public string material { get; set; }
        /// <summary>
        /// 轮胎生产批次号，偶尔会有部分缺少物料信息，此时值为null
        /// </summary>
        public string DOT { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 出库扫码时间
        /// </summary>
        public string outtime { get; set; }

    }
}
