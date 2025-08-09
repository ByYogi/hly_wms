using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo.House
{
    /// <summary>
    /// 品牌仓库共享数据实体Tbl_Cargo_House
    /// </summary>
    [Serializable]
    public class CargoHouseBrandShareEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("主要仓库ID")]
        public int MainHouseID { get; set; }
        [Description("共享仓库ID")]
        public int ShareHouseID { get; set; }
        [Description("品牌ID")]
        public int BrandID { get; set; }
        [Description("删除标识")]
        public int DelFlag { get; set; }
        [Description("操作员")]
        public string OPID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }

        public string MainHouseName { get; set; }
        public string ShareHouseName { get; set; }
        public string BrandName { get; set; }
        public int UpPriceType { get; set; }
        public decimal FixMoney { get; set; }
        public decimal RatePrice { get; set; }

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
