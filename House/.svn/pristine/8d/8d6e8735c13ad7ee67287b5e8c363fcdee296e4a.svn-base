using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 企业微信打卡规则实体
    /// </summary>
    [Serializable]
    public class CheckinRuleEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("规则名称")]
        public string RuleName { get; set; }
        [Description("规则类型")]
        public int RuleType { get; set; }
        [Description("打卡地点")]
        public string CheckinLocation { get; set; }
        [Description("打卡坐标")]
        public string CheckinCoordinate { get; set; }
        [Description("打卡范围")]
        public int CheckinScope { get; set; }
        [Description("范围外打卡")]
        public int ScopeOuterType { get; set; }
        [Description("是否允许补卡")]
        public int AdditionalType { get; set; }
        [Description("补卡时限")]
        public int AdditionalTime { get; set; }
        [Description("补卡次数")]
        public int AdditionalCount { get; set; }
        [Description("删除标识")]
        public int DelFlag { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }


        [Description("规则时间表主键")]
        public int RuleTimeID { get; set; }
        [Description("规则ID")]
        public int RuleID { get; set; }
        [Description("上班星期")]
        public string WorkWeek { get; set; }
        [Description("上班时间")]
        public string ToWorkTime { get; set; }
        [Description("下班时间")]
        public string OffWorkTime { get; set; }
        [Description("上班打卡开始时间")]
        public string ToWorkStartTime { get; set; }
        [Description("上班打卡结束时间")]
        public string ToWorkEndTime { get; set; }
        [Description("下班打卡开始时间")]
        public string OffWorkStartTime { get; set; }
        [Description("下班打卡结束时间")]
        public string OffWorkEndTime { get; set; }
        [Description("休息时间状态")]
        public int BreakTimeType { get; set; }
        [Description("休息开始时间")]
        public string BreakStartTime { get; set; }
        [Description("休息结束时间")]
        public string BreakFinishTime { get; set; }
        [Description("弹性上下班状态")]
        public int FlexibleWorkType { get; set; }
        [Description("允许迟到时间")]
        public int AllowLateTime { get; set; }
        [Description("允许早退时间")]
        public int AllowLeaveTime { get; set; }
        [Description("最多早到早走")]
        public int EarlyArrivalDeparture { get; set; }
        [Description("最多晚到晚走")]
        public int LateArrivalDeparture { get; set; }
        [Description("下班晚走次日晚到状态")]
        public int LateOffToWorkType { get; set; }
        [Description("晚走时长")]
        public int LeaveLateTime { get; set; }
        [Description("晚到时长")]
        public int ArriveLateTime { get; set; }
        [Description("下班是否打卡")]
        public int OffWorkCheckinType { get; set; }


        [Description("规则人员表主键")]
        public int RulePersonnelID { get; set; }
        [Description("打卡部门")]
        public string CheckinDepart { get; set; }
        [Description("打卡人员")]
        public string CheckinUser { get; set; }
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
    /// 企业微信打卡规则时间实体
    /// </summary>
    public class CheckinRuleTimeEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("规则ID")]
        public int RuleID { get; set; }
        [Description("上班星期")]
        public string WorkWeek { get; set; }
        [Description("上班时间")]
        public string ToWorkTime { get; set; }
        [Description("下班时间")]
        public string OffWorkTime { get; set; }
        [Description("上班打卡开始时间")]
        public string ToWorkStartTime { get; set; }
        [Description("上班打卡结束时间")]
        public string ToWorkEndTime { get; set; }
        [Description("下班打卡开始时间")]
        public string OffWorkStartTime { get; set; }
        [Description("下班打卡结束时间")]
        public string OffWorkEndTime { get; set; }
        [Description("休息时间状态")]
        public int BreakTimeType { get; set; }
        [Description("休息开始时间")]
        public string BreakStartTime { get; set; }
        [Description("休息结束时间")]
        public string BreakFinishTime { get; set; }
        [Description("弹性上下班状态")]
        public int FlexibleWorkType { get; set; }
        [Description("允许迟到时间")]
        public int AllowLateTime { get; set; }
        [Description("允许早退时间")]
        public int AllowLeaveTime { get; set; }
        [Description("最多早到早走")]
        public int EarlyArrivalDeparture { get; set; }
        [Description("最多晚到晚走")]
        public int LateArrivalDeparture { get; set; }
        [Description("下班晚走次日晚到状态")]
        public int LateOffToWorkType { get; set; }
        [Description("晚走时长")]
        public int LeaveLateTime { get; set; }
        [Description("晚到时长")]
        public int ArriveLateTime { get; set; }
        [Description("下班是否打卡")]
        public int OffWorkCheckinType { get; set; }
    }
    /// <summary>
    /// 企业微信打卡规则人员实体
    /// </summary>
    public class CheckinRulePersonnelEntity
    {
        [Description("表主键")]
        public int ID { get; set; }
        [Description("规则ID")]
        public int RuleID { get; set; }
        [Description("打卡部门")]
        public string CheckinDepart { get; set; }
        [Description("打卡人员")]
        public string CheckinUser { get; set; }
    }
}
