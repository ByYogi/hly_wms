using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信用户积分数据实体1.3.6.Tbl_WX_Point（积分记录数据表）
    /// </summary>
    [Serializable]
    public class WXUserPointEntity
    {
        public Int64 ID { get; set; }
        public Int64 WXID { get; set; }
        public int Point { get; set; }
        public string PointType { get; set; }
        public string CutPoint { get; set; }
        public DateTime OP_DATE { get; set; }
        public string WXOrderNo { get; set; }
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
