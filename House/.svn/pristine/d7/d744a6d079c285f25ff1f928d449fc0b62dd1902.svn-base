using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo.Product
{
    /// <summary>
    /// 供应商产品价格数据表
    /// </summary>
    [Serializable]
    public class CargoSupplierProductPriceEntity
    {
        [Description("表主键")]
        public Int64 ID { get; set; }
        /// <summary>
        /// 产品规格表ID
        /// </summary>
        [Description("产品规格表ID")]
        public int SID { get; set; }
        [Description("产品编码")]
        public string ProductCode { get; set; }
        [Description("仓库ID")]
        public int HouseID { get; set; }
        /// <summary>
        /// 供应商ID(客户ID)
        /// </summary>
        [Description("供应商ID")]
        public int SupplierID { get; set; }
        /// <summary>
        /// 供应商编码(客户编码)
        /// </summary>
        [Description("供应商编码")]
        public int SupplierNum { get; set; }
        [Description("单价")]
        public double UnitPrice { get; set; }

        [Description("销售价")]
        public double SalePrice { get; set; }

        
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 删除标识（0:启用1:停用）
        /// </summary>
        [Description("删除标识")]
        public int DelFlag { get; set; }

        #region 用于列表显示,与原实体表结构无关
        /// <summary>
        /// 品牌
        /// </summary>
        [Description("品牌")]
        public string TypeName { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        [Description("品牌ID")]
        public int TypeID { get; set; }
        [Description("规格")]
        public string Specs { get; set; }
        /// <summary>
        /// 轮胎花纹
        /// </summary>
        [Description("花纹")]
        public string Figure { get; set; }
        /// <summary>
        /// 货品代码
        /// </summary>
        [Description("货品代码")]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        [Description("载重指数")]
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        [Description("速度级别")]
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 生产地
        /// </summary>
        [Description("生产地")]
        public int Born { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string HouseName { get; set; }
        #endregion


    }
}
