using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户回访数据实体1.4.28.Tbl_Cargo_FeedBack（客户回访数据表）
    /// </summary>
    [Serializable]
    public class CargoFeedBackEntity
    {
        public long FID { get; set; }
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string ResultType { get; set; }
        public DateTime FeedBackTime { get; set; }
        public string FeedBackName { get; set; }

        public string FeedBackResult { get; set; }
        public DateTime OPDATE { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
