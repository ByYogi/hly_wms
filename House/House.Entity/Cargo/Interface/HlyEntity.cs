using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 和好来运系统数据交换使用Entity实体
    /// </summary>
    [Serializable]
    public class HlyEntity
    {
        /// <summary>
        /// 好来运单号
        /// </summary>
        public string Awbno { get; set; }
        /// <summary>
        /// 新陆程单号
        /// </summary>
        public string Xawbno { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Cuser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Cdatetime { get; set; }
        /// <summary>
        /// 其它备注
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// 好来运运单分隔符
        /// </summary>
        public string SplitCB { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 运单状态
        /// </summary>
        public string State { get; set; }

        public int Pcs { get; set; }
        public decimal Weight { get; set; }
        public string Createuser { get; set; }
        public string Toname { get; set; }
        public string ToPhone { get; set; }
        public string ToAddress { get; set; }
        public string Shipper { get; set; }
        public string Company { get; set; }
        public string fdest { get; set; }
        public string BelongSystem { get; set; }
        public string SignName { get; set; }
        public DateTime SignDate { get; set; }
        public string ExAwbno { get; set; }

        
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
    /// 单独运单号实体
    /// </summary>
    [Serializable]
    public class HlyOnlyAwbEntity
    {
        public string awbno { get; set; }

    }
}
