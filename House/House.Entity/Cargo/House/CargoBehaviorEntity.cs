using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 小程序客户行为数据实体1.4.27.Tbl_Cargo_Behavior（小程序用户行为数据表）
    /// </summary>
    [Serializable]
    public class CargoBehaviorEntity
    {
        public long BID { get; set; }
        public int HouseID { get; set; }
        public int ClientNum { get; set; }
        public string wxOpenID { get; set; }
        public string BehaType { get; set; }
        public string KeyWord { get; set; }
        public string TypeName { get; set; }
        public string SearchResult { get; set; }
        public DateTime OPDATE { get; set; }
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
