using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 产品销售价格维护数据实体Tbl_Cargo_ProductPrice（产品销售价格表暂时富添盛系统使用）
    /// </summary>
    [Serializable]
    public class CargoProductPriceEntity
    {
        public int ID { get; set; }
        /// <summary>
        /// 富添盛编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public string PriceType { get; set; }
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 入手价
        /// </summary>
        public decimal SalePriceClient { get; set; }
        /// <summary>
        /// 对店价
        /// </summary>
        public decimal CostPriceStore { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 商贸入手价
        /// </summary>
        public decimal SMClientPrice { get; set; }
        /// <summary>
        /// 商贸对店价
        /// </summary>
        public decimal SMStorePrice { get; set; }
        /// <summary>
        /// 长和入手价
        /// </summary>
        public decimal CHClientPrice { get; set; }
        /// <summary>
        /// 长和对店价
        /// </summary>
        public decimal CHStorePrice { get; set; }
        /// <summary>
        /// 祺航入手价
        /// </summary>
        public decimal QHClientPrice { get; set; }
        /// <summary>
        /// 祺航对店价
        /// </summary>
        public decimal QHStorePrice { get; set; }
        /// <summary>
        /// 默认对剩余客户其他入手价
        /// </summary>
        public decimal DefaultClientPrice { get; set; }
        /// <summary>
        /// 默认对剩余客户其他对店价
        /// </summary>
        public decimal DefaultStorePrice { get; set; }
        /// <summary>
        /// 出库包装类型
        /// </summary>
        public string OutPackType { get; set; }
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
