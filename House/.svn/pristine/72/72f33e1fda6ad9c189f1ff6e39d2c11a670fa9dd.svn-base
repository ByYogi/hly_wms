using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 商品上架数据实体1.2.3.Tbl_Cargo_Shelves（商品上架数据表）
    /// </summary>
    [Serializable]
    public class CargoProductShelvesEntity
    {
        public Int64 ID { get; set; }
        public Int64 ProductID { get; set; }
        public Int64 OrderID { get; set; }
        public int TypeID { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 商城价格
        /// </summary>
        public decimal ProductPrice { get; set; }
        public decimal SigningPrice { get; set; }
        public decimal minPurchase { get; set; } = 0;
        public int OnSaleNum { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string FileName { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
        [Description("型号")]
        public string Model { get; set; }
        [Description("货品代码")]
        public string GoodsCode { get; set; }
        [Description("规格")]
        public string Specs { get; set; }
        [Description("花纹")]
        public string Figure { get; set; }
        [Description("胎面宽度")]
        public int TreadWidth { get; set; }
        [Description("轮胎扁平比")]
        public int FlatRatio { get; set; }
        [Description("子午线")]
        public string Meridian { get; set; }
        [Description("轮毂直径")]
        public int HubDiameter { get; set; }
        [Description("载重指数")]
        public string LoadIndex { get; set; }
        [Description("速度级别")]
        public string SpeedLevel { get; set; }
        [Description("最高速度")]
        public int SpeedMax { get; set; }
        [Description("尺寸")]
        public string Size { get; set; }
        [Description("批次")]
        public string Batch { get; set; }

        public int BatchYear { get; set; }
        [Description("销售价")]
        public decimal SalePrice { get; set; }
        [Description("批发价")]
        public decimal TradePrice { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 产品销售价
        /// </summary>
        public decimal originalPrice { get; set; }
        /// <summary>
        /// 修改的价格
        /// </summary>
        public decimal ModifyPrice { get; set; }
        /// <summary>
        /// 销售类型
        /// </summary>
        public string SaleType { get; set; }
        public string ShelveStatus { get; set; }
        public DateTime AdvertStartDate { get; set; }
        public DateTime AdvertEndDate { get; set; }
        public List<CargoProductFileEntity> productFile { get; set; }
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        public string OrderNo { get; set; } 
        public int Consume { get; set; }
        /// <summary>
        /// 是否是直减
        /// </summary>
        public bool IsCut { get; set; }
        /// <summary>
        /// 直减金额
        /// </summary>
        public decimal CutEntry { get; set; }
        /// <summary>
        /// 规则ID
        /// </summary>
        public long RuleID { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        public DateTime? InHouseTime { get; set; }
        public int InCargoStatus { get; set; }
        public int InHouseDay { get; set; }
        /// <summary>
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

    [Serializable]
    public class CargoAddProductShelvesEntity {
        public Int64 ID { get; set; }
        public int ProductID { get; set; }
        public int TypeID { get; set; }
        public string ProductName { get; set; }
        public int OnSaleNum { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal SigningPrice { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string FileName { get; set; }
        public int SaleType { get; set; }
        public int ShelveStatus { get; set; }
        public decimal minPurchase { get; set; }
        public int HouseID { get; set; }
        public int Consume { get; set; }
        public DateTime OP_DATE { get; set; }
        public string ProductCode { get; set; }
        public DateTime? AdvertStartDate { get; set; }
        public DateTime? AdvertEndDate { get; set; }

    }
    [Serializable]
    public class CargoAddProductShelvesEntityDto {
        public Int64 ID { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal SigningPrice { get; set; }
        public decimal minPurchase { get; set; }

    }

    /// <summary>
    /// 上传的商品照片数据实体Tbl_Cargo_ProductFile（产品照片表）
    /// </summary>
    [Serializable]
    public class CargoProductFileEntity
    {
        public Int64 ID { get; set; }
        public Int64 ShelvesID { get; set; }
        public Int64 ProductID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
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
}
