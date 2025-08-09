using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 企业用户打卡数据实体
    /// </summary>
    [Serializable]
    public class QYCheckinDataEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("用户ID")]
        public string UserID { get; set; }
        [Description("用户名称")]
        public string UserName { get; set; }
        [Description("部门ID")]
        public string DepartmentID { get; set; }
        [Description("部门名称")]
        public string DepartmentName { get; set; }
        [Description("规则名称")]
        public string GroupName { get; set; }
        [Description("打卡类型")]
        public string CheckinType { get; set; }
        [Description("异常类型")]
        public string ExceptionType { get; set; }
        [Description("打卡时间")]
        public DateTime CheckinTime { get; set; }
        [Description("打卡地点title")]
        public string LocationTitle { get; set; }
        [Description("打卡地点")]
        public string LocationDetail { get; set; }
        [Description("WiFi名称")]
        public string WifiName { get; set; }
        [Description("打卡备注")]
        public string Notes { get; set; }
        [Description("Mac缔造者")]
        public string WifiMac { get; set; }
        [Description("附件ID")]
        public string MediaIDs { get; set; }
        [Description("附件地址")]
        public string MediaPath { get; set; }
        [Description("经度")]
        public int Lat { get; set; }
        [Description("纬度")]
        public int Lng { get; set; }
        [Description("设备ID")]
        public string DeviceID { get; set; }
        [Description("打卡标准时间")]
        public DateTime SchCheckinTime { get; set; }
        [Description("规则ID")]
        public int GroupID { get; set; }
        [Description("时段ID")]
        public int TimelineID { get; set; }
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
    public class CheckintListReturnJson
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public List<checkindata> checkindata;
    }
    public class checkindata 
    {
        [Description("用户ID")]
        public string userid { get; set; }
        [Description("规则名称")]
        public string groupname { get; set; }
        [Description("打卡类型")]
        public string checkin_type { get; set; }
        [Description("异常类型")]
        public string exception_type { get; set; }
        [Description("打卡时间")]
        public int checkin_time { get; set; }
        [Description("打卡地点title")]
        public string location_title { get; set; }
        [Description("打卡地点")]
        public string location_detail { get; set; }
        [Description("WiFi名称")]
        public string wifiname { get; set; }
        [Description("打卡备注")]
        public string notes { get; set; }
        [Description("Mac地址")]
        public string wifimac { get; set; }
        [Description("附件ID")]
        public List<string> mediaids { get; set; }
        [Description("经度")]
        public int lat { get; set; }
        [Description("纬度")]
        public int lng { get; set; }
        [Description("设备ID")]
        public string deviceid { get; set; }
        [Description("打卡标准时间")]
        public int sch_checkin_time { get; set; }
        [Description("规则ID")]
        public int groupid { get; set; }
        [Description("时段ID")]
        public int timeline_id { get; set; }
    }
}
