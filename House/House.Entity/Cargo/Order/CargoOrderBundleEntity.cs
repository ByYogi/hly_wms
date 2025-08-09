using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo.Order
{
    /// <summary>
    /// 捆包数据实体
    /// </summary>
    [Serializable]
    public class CargoOrderBundleEntity
    {
        [Description("仓库ID")]
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        public int OrderNum { get; set; }
        [Description("捆包单号")]
        public string BundleNo { get; set; }
        [Description("捆包状态")]
        public string BundleStatus { get; set; }
        [Description("捆包类型")]
        public string BundleType { get; set; }
        public int TotalNum { get; set; }
        public long BunID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OP_DATE { get; set; }
        public string Memo { get; set; }
        public string BunGoodsID { get; set; }
        [Description("零件号码")]
        public string GoodsCode { get; set; }
        [Description("零件名称")]
        public string ProductName { get; set; }
        [Description("数量")]
        public int Piece { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        public string LogisticName { get; set; }

        public string CargoPermisID { get; set; }

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
