using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 产品价格修改数据实体1.8.4.Tbl_Cargo_PriceModify（产品价格修改异常记录数据表）
    /// </summary>
    [Serializable]
    public class CargoPriceModifyEntity
    {
        public long MID { get; set; }
        public DateTime OPDate { get; set; }
        public int HouseID { get; set; }
        public string HouseName { get; set; }

        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string ModifySource { get; set; }
        public string ProductCode { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string PriceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductName { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string GoodsCode { get; set; }
        public string Model { get; set; }
        public string ParentName { get; set; }

        public string TypeName { get; set; }

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
