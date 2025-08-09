using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 城市仓库线网关系映射数据实体
    /// </summary>
    [Serializable]
    public class CargoLineNetMappingEntity
    {
        public long MID { get; set; }
        public int ClientNum { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 区域大仓
        /// </summary>
        public string HouseName { get; set; }

        public int AreaID { get; set; }
        public int HouseID { get; set; }
        /// <summary>
        /// 优先出库级别
        /// </summary>
        public int OrderLevel { get; set; }
        public string OPID { get; set; }
        public DateTime OPDATE { get; set; }
        public string CargoPermisID { get; set; }
        public string ShopCode { get; set; }
        public string ClientName { get; set; }
        public string ClientShortName { get; set; }

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
