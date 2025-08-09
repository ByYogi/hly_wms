using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{

    /// <summary>
    /// 车辆实体类Tbl_Truck
    /// </summary>
    [Serializable]
    public class TruckEntity
    {
        public string BelongSystem { get; set; }
        [Description("车辆ID")]
        public long Truck_ID { get; set; }
        [Description("车牌号码")]
        public string TruckNum { get; set; }
        [Description("车牌所在城市")]
        public string City { get; set; }
        [Description("车型")]
        public string Model { get; set; }
        [Description("载重")]
        public decimal Weight { get; set; }
        [Description("车长")]
        public decimal Length { get; set; }
        [Description("司机姓名")]
        public string Driver { get; set; }
        [Description("司机手机号码")]
        public string DriverCellPhone { get; set; }
        [Description("司机身份证号码")]
        public string DriverIDNum { get; set; }
        [Description("司机身份证地址")]
        public string DriverIDAddress { get; set; }
        [Description("车辆类型")]
        public string TruckType { get; set; }
        [Description("司机驾照")]
        public string License { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("删除标识")]
        public string DelFlag { get; set; }
        /// <summary>
        /// 车辆状态
        /// </summary>
        public string TruckFlag { get; set; }
        /// <summary>
        /// 车辆所在当前城市
        /// </summary>
        public string CurCity { get; set; }
        /// <summary>
        /// 车辆标识长途车或短途车
        /// </summary>
        [Description("车辆标识")]
        public string TripMark { get; set; }
        public string BlackMemo { get; set; }

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

    [Serializable]
    public class TruckCacheEntity
    {
        public string BelongSystem { get; set; }
        public string TruckNum { get; set; }
        public string Driver { get; set; }
        public string DriverCellPhone { get; set; }
        public string DriverIDNum { get; set; }
        public string DriverIDAddress { get; set; }
        public decimal Length { get; set; }
        public string Model { get; set; }
    }
}
