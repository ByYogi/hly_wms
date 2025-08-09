using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 车辆状态跟踪表1.2.3.	Tbl_TruckStatusTrack
    /// </summary>
    [Serializable]
    public class TruckStatusTrackEntity
    {
        public string BelongSystem { get; set; }
        [Description("司机合同号")]
        public string ContractNum { get; set; }
        [Description("车牌号码")]
        public string TruckNum { get; set; }
        [Description("车辆状态")]
        public string TruckFlag { get; set; }
        [Description("当前位置")]
        public string CurrentLocation { get; set; }
        [Description("剩余时间")]
        public decimal LastHour { get; set; }
        [Description("详细信息")]
        public string DetailInfo { get; set; }
        [Description("到达时间")]
        public DateTime ArriveTime { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public Int64 ArriveID { get; set; }
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
