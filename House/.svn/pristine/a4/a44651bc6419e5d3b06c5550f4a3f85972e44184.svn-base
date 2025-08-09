using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Entity.Cargo;

namespace Cargo.QY
{
    public partial class qyCheckin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<QYCheckinDataEntity> CheckinExport
        {
            get
            {
                if (Session["CheckinExport"] == null)
                {
                    Session["CheckinExport"] = new List<QYCheckinDataEntity>();
                }
                return (List<QYCheckinDataEntity>)(Session["CheckinExport"]);
            }
            set
            {
                Session["CheckinExport"] = value;
            }
        }
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CheckinExport.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("所属部门", typeof(string));
            table.Columns.Add("姓名", typeof(string));
            table.Columns.Add("打卡类型", typeof(string));
            table.Columns.Add("异常类型", typeof(string));
            table.Columns.Add("打卡日期", typeof(string));
            table.Columns.Add("打卡时间", typeof(string));
            table.Columns.Add("规则时间", typeof(string));
            table.Columns.Add("打卡地点", typeof(string));
            table.Columns.Add("WiFi名称", typeof(string));
            table.Columns.Add("备注", typeof(string));
            foreach (var it in CheckinExport)
            {
                DataRow newRows = table.NewRow();
                newRows["所属部门"] = it.DepartmentName.Trim();
                newRows["姓名"] = it.UserName.Trim();
                newRows["打卡类型"] = it.CheckinType.Trim();
                newRows["异常类型"] = string.IsNullOrEmpty(it.ExceptionType) ? "无异常" : it.ExceptionType.Trim();
                newRows["打卡日期"] = it.CheckinTime.ToString("yyyy-MM-dd");
                newRows["打卡时间"] = it.ExceptionType == "未打卡" ? "" : it.CheckinTime.ToString("HH:mm:ss");
                newRows["规则时间"] = it.SchCheckinTime.ToString("HH:mm:ss");
                newRows["打卡地点"] = it.LocationDetail.Trim();
                newRows["WiFi名称"] = it.WifiName.Trim();
                newRows["备注"] = it.Notes.Trim();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "考勤信息");
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<QYCheckinMonthlyReportEntity> CheckinReportForExport
        {
            get
            {
                if (Session["CheckinReportForExport"] == null)
                {
                    Session["CheckinReportForExport"] = new List<QYCheckinMonthlyReportEntity>();
                }
                return (List<QYCheckinMonthlyReportEntity>)(Session["CheckinReportForExport"]);
            }
            set
            {
                Session["CheckinReportForExport"] = value;
            }
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public int CheckinReportExportDay
        {
            get
            {
                if (Session["CheckinReportExportDay"] == null)
                {
                    Session["CheckinReportExportDay"] = new int();
                }
                return (int)(Session["CheckinReportExportDay"]);
            }
            set
            {
                Session["CheckinReportExportDay"] = value;
            }
        }
        protected void btnReportDerived_Click(object sender, EventArgs e)
        {
            if (CheckinReportForExport.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("姓名", typeof(string));
            table.Columns.Add("部门", typeof(string));
            table.Columns.Add("应打卡天数", typeof(string));
            table.Columns.Add("正常天数", typeof(string));
            table.Columns.Add("异常天数", typeof(string));
            table.Columns.Add("标准工作时长", typeof(string));
            table.Columns.Add("实际工作时长", typeof(string));
            table.Columns.Add("工作日加班时长", typeof(string));
            table.Columns.Add("节假日加班时长", typeof(string));
            table.Columns.Add("休息日加班时长", typeof(string));
            table.Columns.Add("迟到次数", typeof(string));
            table.Columns.Add("迟到时长", typeof(string));
            table.Columns.Add("早退次数", typeof(string));
            table.Columns.Add("早退时长", typeof(string));
            table.Columns.Add("缺卡次数", typeof(string));
            table.Columns.Add("旷工次数", typeof(string));
            table.Columns.Add("旷工时长", typeof(string));
            table.Columns.Add("地点异常", typeof(string));
            table.Columns.Add("设备异常", typeof(string));
            table.Columns.Add("补卡次数", typeof(string));
            table.Columns.Add("外勤次数", typeof(string));
            table.Columns.Add("外出", typeof(string));
            table.Columns.Add("出差", typeof(string));
            table.Columns.Add("年假", typeof(string));
            table.Columns.Add("事假", typeof(string));
            table.Columns.Add("病假", typeof(string));
            table.Columns.Add("调休假", typeof(string));
            table.Columns.Add("婚假", typeof(string));
            table.Columns.Add("产假", typeof(string));
            table.Columns.Add("陪产假", typeof(string));
            table.Columns.Add("其他", typeof(string));
            for (int i = 1; i <= CheckinReportExportDay; i++)
            {
                table.Columns.Add(i.ToString(), typeof(string));
            }
            foreach (var it in CheckinReportForExport)
            {
                DataRow newRows = table.NewRow();
                newRows["姓名"] = it.Name.Trim();
                newRows["部门"] = it.DepartmentName.Trim();
                newRows["应打卡天数"] = it.WorkDays == 0 ? "--" : it.WorkDays.ToString();
                newRows["正常天数"] = it.RegularDays == 0 ? "--" : it.RegularDays.ToString();
                newRows["异常天数"] = it.ExceptDays == 0 ? "--" : it.ExceptDays.ToString();
                newRows["标准工作时长"] = it.StandardWorkSec == 0 ? "--" : Math.Round(Convert.ToDouble(it.StandardWorkSec / 3600), 1) + "小时";
                newRows["实际工作时长"] = it.RegularWorkSec == 0 ? "--" : Math.Round(Convert.ToDouble(it.RegularWorkSec / 3600), 1) + "小时";
                newRows["工作日加班时长"] = it.WorkdayOverSec == 0 ? "--" : Math.Round(Convert.ToDouble(it.WorkdayOverSec / 3600), 1) + "小时";
                newRows["节假日加班时长"] = it.HolidaysOverSec == 0 ? "--" : Math.Round(Convert.ToDouble(it.HolidaysOverSec / 3600), 1) + "小时";
                newRows["休息日加班时长"] = it.RestdaysOverSec == 0 ? "--" : Math.Round(Convert.ToDouble(it.RestdaysOverSec / 3600), 1) + "小时";
                newRows["迟到次数"] = it.LateCount == 0 ? "--" : it.LateCount.ToString();
                newRows["迟到时长"] = it.LateDuration == 0 ? "--" : (it.LateDuration / 60) + "分钟";
                newRows["早退次数"] = it.LeaveEarlyCount == 0 ? "--" : it.LeaveEarlyCount.ToString();
                newRows["早退时长"] = it.LeaveEarlyDuration == 0 ? "--" : (it.LeaveEarlyDuration / 60) + "分钟";
                newRows["缺卡次数"] = it.AbsenceCount == 0 ? "--" : it.AbsenceCount.ToString();
                newRows["旷工次数"] = it.AbsenteeismCount == 0 ? "--" : it.AbsenteeismCount.ToString();
                newRows["旷工时长"] = it.AbsenteeismDuration == 0 ? "--" : (it.AbsenteeismDuration / 60) + "分钟";
                newRows["地点异常"] = it.LocationAbnormalCount == 0 ? "--" : it.LocationAbnormalCount.ToString();
                newRows["设备异常"] = it.EquipmentAbnormalCount == 0 ? "--" : it.EquipmentAbnormalCount.ToString();
                newRows["补卡次数"] = it.CardReplacementCount == 0 ? "--" : it.CardReplacementCount.ToString();
                newRows["外勤次数"] = it.FieldCount == 0 ? "--" : it.FieldCount.ToString();
                newRows["外出"] = it.EgressCount == 0 ? "--" : it.EgressCount.ToString();
                newRows["出差"] = it.BusinessCount == 0 ? "--" : it.BusinessCount.ToString();
                newRows["年假"] = it.AnnualLeaveCount == 0 ? "--" : it.AnnualLeaveCount.ToString();
                newRows["事假"] = it.CompassionateLeaveCount == 0 ? "--" : it.CompassionateLeaveCount.ToString();
                newRows["病假"] = it.SickLeaveCount == 0 ? "--" : it.SickLeaveCount.ToString();
                newRows["调休假"] = it.CompensatoryLeaveCount == 0 ? "--" : it.CompensatoryLeaveCount.ToString();
                newRows["婚假"] = it.MarriageHolidayCount == 0 ? "--" : it.MarriageHolidayCount.ToString();
                newRows["产假"] = it.MaternityLeaveCount == 0 ? "--" : it.MaternityLeaveCount.ToString();
                newRows["陪产假"] = it.PaternityLeaveCount == 0 ? "--" : it.PaternityLeaveCount.ToString();
                newRows["其他"] = it.OtherLeaveCount == 0 ? "--" : it.OtherLeaveCount.ToString();
                var infos = it.infos.Split('|');
                for (int i = 1; i <= CheckinReportExportDay; i++)
                {
                    newRows[i.ToString()] = string.IsNullOrEmpty(infos[i - 1]) ? "--" : infos[i - 1];
                }
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", CheckinReportForExport[0].ReportTime + "汇总统计");
        }
    }
}