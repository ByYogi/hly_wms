using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 马牌销售单数据实体
    /// </summary>
    [Serializable]
    public class CargoContiSaleOrderRespEntity
    {
        public long CID { get; set; }
        public string orderCode { get; set; }
        public string orderSource { get; set; }
        public string orderType { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string sellerName { get; set; }
        public decimal creditAmount { get; set; }
        public decimal rebateAmount { get; set; }
        public decimal nowTotalAmount { get; set; }
        public decimal freightAmount { get; set; }
        public DateTime submitTime { get; set; }
        public string remark { get; set; }
        public string consigneeAddress { get; set; }
        public string consigneeName { get; set; }
        public string consigneeMobile { get; set; }
        public string distributorSoldTo { get; set; }
        public string warehouseCode { get; set; }
        public string warehouseName { get; set; }
        public DateTime shippingTime { get; set; }
        public DateTime OP_DATE { get; set; }
        public string orderStatus { get; set; } 
        public string orderStatusStr { get; set; }
        public List<CargoContiSaleSKURespEntity> ContiSaleSKUList { get; set; }
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

    /// <summary>
    /// 马牌销售订单明细数据实体
    /// </summary>
    [Serializable]
    public class CargoContiSaleSKURespEntity
    {
        public long SKUID { get; set; }
        public long CID { get; set; }
        public string skuName { get; set; }
        public string skuCode { get; set; }
        public int skuNum { get; set; }
        public decimal nowUnitPrice { get; set; }
        public decimal originalUnitPrice { get; set; }
        public decimal totalAmount { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string GoodsCode { get; set; }
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
