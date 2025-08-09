using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 运单状态跟踪
    /// </summary>
    [Serializable]
    public class AwbStatus
    {
        public string BelongSystem { get; set; }
        public Int64 TID { get; set; }
        public Int64 AwbID { get; set; }
        public string AwbNo { get; set; }
        public string TruckFlag { get; set; }
        public string CurrentLocation { get; set; }
        public decimal LastHour { get; set; }
        public DateTime? ArriveTime { get; set; }
        public string DetailInfo { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public string UserName { get; set; }
        public string CheckStatus { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public string Signer { get; set; }
        public DateTime? SignTime { get; set; }
        public string FilePath { get; set; }
        public string TbFilePath { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<AwbFilesEntity> Files { get; set; }
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
