using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    public class ShippingCarInfoEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ShippingCarInfo data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class ShippingCarInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ShippingCarItemsInfo> items { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal score { get; set; }
    }

    public class ShippingCarItemsInfo
    {
        [Description("表主键")]
        public long ID { get; set; }
        [Description("OpenID")]
        public string OpenID { get; set; }
        [Description("客户编码")]
        public string ClientNum { get; set; }
        [Description("商品Key")]
        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }
        [Description("商品名称")]
        /// <summary>
        /// 男童防晒衣夏装薄外套2019年新款童装儿童中大童透气帅洋气休闲潮
        /// </summary>
        public string name { get; set; }
        [Description("数量")]
        /// <summary>
        /// 
        /// </summary>
        public int number { get; set; }
        [Description("类型ID")]
        public int TypeID { get; set; }
        [Description("规格")]
        public string Specs { get; set; }
        [Description("花纹")]
        public string Figure { get; set; }
        [Description("型号")]
        public string Model { get; set; }
        [Description("货品代码")]
        public string GoodsCode { get; set; }
        [Description("载重指数")]
        public string LoadIndex { get; set; }
        [Description("速度级别")]
        public string SpeedLevel { get; set; }
        [Description("批次年")]
        public int BatchYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
        [Description("价格")]
        /// <summary>
        /// 
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool selected { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stores { get; set; }
        public DateTime OP_DATE { get; set; }

        public int HouseID { get; set; }
        public int ParentAreaID { get; set; }
        public string HouseName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string notes { get; set; }
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
