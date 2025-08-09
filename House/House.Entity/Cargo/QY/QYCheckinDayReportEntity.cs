using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 企业用户打卡日报统计实体
    /// </summary>
    [Serializable]
    public class QYCheckinDayReportEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("日报日期")]
        public DateTime Date { get; set; }
        [Description("记录类型")]
        public int RecordType { get; set; }
        [Description("打卡人员姓名")]
        public string Name { get; set; }
        [Description("打卡人员别名")]
        public string NameEx { get; set; }
        [Description("打卡人员所在部门")]
        public string DepartsName { get; set; }
        [Description("打卡人员账号")]
        public string UserID { get; set; }
        [Description("所属规则ID")]
        public int Groupid { get; set; }
        [Description("所属规则名")]
        public string GroupName { get; set; }
        [Description("当日所属班次ID")]
        public int ScheduleId { get; set; }
        [Description("当日所属班次名称")]
        public string ScheduleName { get; set; }
        [Description("上班时间")]
        public int WorkSec { get; set; }
        [Description("下班时间")]
        public int OffWorkSec { get; set; }
        [Description("日报类型")]
        public int DayType { get; set; }
        [Description("当日打卡次数")]
        public int CheckinCount { get; set; }
        [Description("当日实际工作时长")]
        public int RegularWorkSec { get; set; }
        [Description("当日标准工作时长")]
        public int StandardWorkSec { get; set; }
        [Description("当日最早打卡时间")]
        public int EarliestTime { get; set; }
        [Description("当日最晚打卡时间")]
        public int LastestTime { get; set; }
        [Description("假勤申请ID")]
        public string SpNumber { get; set; }
        [Description("假勤标题文本")]
        public string SpTitleText { get; set; }
        [Description("假勤标题语言类型")]
        public string SpTitleLang { get; set; }
        [Description("假勤描述文本")]
        public string SpDescriptionText { get; set; }
        [Description("假勤描述语言类型")]
        public string SpDescriptionLang { get; set; }
        [Description("迟到次数")]
        public int LateCount { get; set; }
        [Description("迟到时长")]
        public int LateDuration { get; set; }
        [Description("早退次数")]
        public int LeaveEarlyCount { get; set; }
        [Description("早退时长")]
        public int LeaveEarlyDuration { get; set; }
        [Description("旷工次数")]
        public int AbsenteeismCount { get; set; }
        [Description("旷工时长")]
        public int AbsenteeismDuration { get; set; }
        [Description("缺卡次数")]
        public int AbsenceCount { get; set; }
        [Description("地点异常次数")]
        public int LocationAbnormalCount { get; set; }
        [Description("设备异常次数")]
        public int EquipmentAbnormalCount { get; set; }
        [Description("加班状态")]
        public int OtStatus { get; set; }
        [Description("加班时长")]
        public int OtDuration { get; set; }
        [Description("加班不足时长")]
        public int ExceptionDuration { get; set; }
        [Description("年假次数")]
        public int AnnualLeaveCount { get; set; }
        [Description("年假时长")]
        public int AnnualLeaveDuration { get; set; }
        [Description("年假时长单位")]
        public int AnnualLeaveTimeType { get; set; }
        [Description("事假次数")]
        public int CompassionateLeaveCount { get; set; }
        [Description("事假时长")]
        public int CompassionateLeaveDuration { get; set; }
        [Description("事假时长单位")]
        public int CompassionateLeaveTimeType { get; set; }
        [Description("病假次数")]
        public int SickLeaveCount { get; set; }
        [Description("病假时长")]
        public int SickLeaveDuration { get; set; }
        [Description("病假时长单位")]
        public int SickLeaveTimeType { get; set; }
        [Description("调休假次数")]
        public int CompensatoryLeaveCount { get; set; }
        [Description("调休假时长")]
        public int CompensatoryLeaveDuration { get; set; }
        [Description("调休假时长单位")]
        public int CompensatoryLeaveTimeType { get; set; }
        [Description("婚假次数")]
        public int MarriageHolidayCount { get; set; }
        [Description("婚假时长")]
        public int MarriageHolidayDuration { get; set; }
        [Description("婚假时长单位")]
        public int MarriageHolidayTimeType { get; set; }
        [Description("产假次数")]
        public int MaternityLeaveCount { get; set; }
        [Description("产假时长")]
        public int MaternityLeaveDuration { get; set; }
        [Description("产假时长单位")]
        public int MaternityLeaveTimeType { get; set; }
        [Description("陪产假次数")]
        public int PaternityLeaveCount { get; set; }
        [Description("陪产假时长")]
        public int PaternityLeaveDuration { get; set; }
        [Description("陪产假时长单位")]
        public int PaternityLeaveTimeType { get; set; }
        [Description("其他假次数")]
        public int OtherLeaveCount { get; set; }
        [Description("其他假时长")]
        public int OtherLeaveDuration { get; set; }
        [Description("其他假时长单位")]
        public int OtherLeaveTimeType { get; set; }
        [Description("补卡次数")]
        public int CardReplacementCount { get; set; }
        [Description("出差次数")]
        public int BusinessCount { get; set; }
        [Description("出差时长")]
        public int BusinessDuration { get; set; }
        [Description("出差时长单位")]
        public int BusinessTimeType { get; set; }
        [Description("外出次数")]
        public int EgressCount { get; set; }
        [Description("外出时长")]
        public int EgressDuration { get; set; }
        [Description("外出时长单位")]
        public int EgressTimeType { get; set; }
        [Description("外勤次数")]
        public int FieldCount { get; set; }
        [Description("外勤时长")]
        public int FieldDuration { get; set; }
        [Description("外勤时长单位")]
        public int FieldTimeType { get; set; }
        public DateTime OP_DATE { get; set; }

        public QYCheckinDayReportSpInfo spInfo { get; set; }
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
    /// 企业用户打卡日报统计假勤信息实体
    /// </summary>
    [Serializable]
    public class QYCheckinDayReportSpInfo
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("日报数据对应ID")]
        public int DayReportID { get; set; }
        [Description("假勤申请ID")]
        public string SpNumber { get; set; }
        [Description("标题文本语言类型")]
        public string TitleLang { get; set; }
        [Description("标题文本摘要")]
        public string TitleText { get; set; }
        [Description("描述文本语言类型")]
        public string DescriptionLang { get; set; }
        [Description("描述文本摘要")]
        public string DescriptionText { get; set; }

        public DateTime OP_DATE { get; set; }

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

    public class CheckintDayReportReturnJson
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public List<day_datas> datas;
    }

    public class day_datas
    {
        public day_base_info base_info { get; set; }
        public day_summary_info summary_info { get; set; }
        public List<day_holiday_infos> holiday_infos;
        public List<day_exception_infos> exception_infos;
        public day_ot_info ot_info { get; set; }
        public List<day_sp_items> sp_items;
    }
    public class day_base_info
    {
        [Description("日报日期")]
        public int date { get; set; }
        [Description("记录类型")]
        public int record_type { get; set; }
        [Description("姓名")]
        public string name { get; set; }
        [Description("别名")]
        public string name_ex { get; set; }
        [Description("所在部门")]
        public string departs_name { get; set; }
        [Description("账号")]
        public string acctid { get; set; }
        [Description("打卡规则信息")]
        public rule_info rule_info { get; set; }
        [Description("日报类型")]
        public int day_type { get; set; }
    }
    public class rule_info
    {
        [Description("规则ID")]
        public int groupid { get; set; }
        [Description("规则名称")]
        public string groupname { get; set; }
        [Description("班次ID")]
        public int scheduleid { get; set; }
        [Description("班次名称")]
        public string schedulename { get; set; }
        [Description("账号")]
        public List<checkintime> checkintime;
    }
    public class checkintime
    {
        [Description("上班时间")]
        public int work_sec { get; set; }
        [Description("下班时间")]
        public int off_work_sec { get; set; }
    }
    public class day_summary_info
    {
        [Description("当日打卡次数")]
        public int checkin_count { get; set; }
        [Description("当日实际工作时长")]
        public int regular_work_sec { get; set; }
        [Description("当日标准工作时长")]
        public int standard_work_sec { get; set; }
        [Description("当日最早打卡时间")]
        public int earliest_time { get; set; }
        [Description("当日最晚打卡时间")]
        public int lastest_time { get; set; }
    }
    public class day_holiday_infos
    {
        [Description("描述信息")]
        public sp_description sp_description { get; set; }
        [Description("假勤申请ID")]
        public string sp_number { get; set; }
        [Description("标题信息")]
        public sp_title sp_title { get; set; }
    }
    public class sp_description
    {
        public List<sp_description_data> data;
    }
    public class sp_description_data
    {
        [Description("描述文本")]
        public string text { get; set; }
        [Description("语言类型")]
        public string lang { get; set; }
    }
    public class sp_title
    {
        public List<sp_title_data> data;
    }
    public class sp_title_data
    {
        [Description("标题文本")]
        public string text { get; set; }
        [Description("语言类型")]
        public string lang { get; set; }
    }
    public class day_exception_infos
    {
        [Description("异常类型")]
        public int exception { get; set; }
        [Description("异常次数")]
        public int count { get; set; }
        [Description("异常时长")]
        public int duration { get; set; }
    }
    public class day_ot_info
    {
        [Description("状态")]
        public int ot_status { get; set; }
        [Description("加班时长")]
        public int ot_duration { get; set; }
        //[Description("加班不足的时长")]
        //public string exception_duration { get; set; }
    }
    public class day_sp_items
    {
        [Description("假勤类型")]
        public int type { get; set; }
        [Description("请假类型")]
        public int vacation_id { get; set; }
        [Description("假勤次数")]
        public int count { get; set; }
        [Description("假勤时长")]
        public int duration { get; set; }
        [Description("时长单位")]
        public int time_type { get; set; }
        [Description("统计项名称")]
        public string name { get; set; }
    }
}
