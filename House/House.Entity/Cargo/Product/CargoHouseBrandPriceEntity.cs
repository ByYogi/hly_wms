using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓库品牌加价实体 1.9.2.	Tbl_Cargo_HouseBrandPrice（仓库品牌加价数据表）
    /// </summary>
    [Serializable]
    public class CargoHouseBrandPriceEntity
    {
        public long BID { get; set; }
        [Description("仓库ID")]
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        [Description("分类品牌ID")]
        public int TypeID { get; set; }
        public string TypeName { get; set; }

        [Description("加价比率(百分)")]
        public decimal UpRate { get; set; }

        [Description("加价金额")]
        public decimal UpMoney { get; set; }

        /// <summary>
        /// 0:按比率加价1:按固定金额加价
        /// </summary>
        [Description("加价类型")]
        public int UpType { get; set; }


        [Description("操作人")]
        public string OPID { get; set; }

        [Description("操作时间")]
        public DateTime OPDATE { get; set; }

        /// <summary>
        /// 0：非云仓1：云仓
        /// </summary>
        [Description("云仓类型")]
        public int CloudHouseType { get; set; }
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
