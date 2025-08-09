using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Entity.Cargo;

namespace Cargo.Report
{
    public partial class reportOutTime : BasePage
    {
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<CargoOrderOutTimeEntity> OrderOutTime
        {
            get
            {
                if (Session["OrderOutTime"] == null)
                {
                    Session["OrderOutTime"] = new List<CargoOrderOutTimeEntity>();
                }
                return (List<CargoOrderOutTimeEntity>)(Session["OrderOutTime"]);
            }
            set
            {
                Session["OrderOutTime"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (OrderOutTime.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("所属仓库", typeof(string));
            DateTime sDT = OrderOutTime[0].StartDate;
            DateTime eDT = OrderOutTime[0].EndDate;
            int Month = (eDT.Year - sDT.Year) * 12 + (eDT.Month - sDT.Month);
            for (int j = 1; j <= Month + 1; j++)
            {
                if (sDT.Month == eDT.Month)
                {
                    table.Columns.Add(eDT.ToString("yyyyMM") + "订单总数", typeof(string));
                    table.Columns.Add(eDT.ToString("yyyyMM") + "出库总数", typeof(string));
                    table.Columns.Add(eDT.ToString("yyyyMM") + "平均扫描时效", typeof(string));
                    table.Columns.Add(eDT.ToString("yyyyMM") + "平均出库时效", typeof(string));
                }
                else
                {
                    table.Columns.Add(sDT.ToString("yyyyMM") + "订单总数", typeof(string));
                    table.Columns.Add(sDT.ToString("yyyyMM") + "出库总数", typeof(string));
                    table.Columns.Add(sDT.ToString("yyyyMM") + "平均扫描时效", typeof(string));
                    table.Columns.Add(sDT.ToString("yyyyMM") + "平均出库时效", typeof(string));
                    sDT = sDT.AddMonths(1);
                }
            }
            int i = 0;
            foreach (var it in OrderOutTime)
            {
                sDT = OrderOutTime[0].StartDate;
                i++;
                DataRow newRows = table.NewRow();
                newRows["所属仓库"] = it.HouseName;
                for (int t = 1; t <= Month + 1; t++)
                {
                    switch (t)
                    {
                        case 1:
                            newRows[sDT.ToString("yyyyMM") + "订单总数"] = string.IsNullOrEmpty(it.OrderCount1) ? "0" : it.OrderCount1 + " 单";
                            newRows[sDT.ToString("yyyyMM") + "出库总数"] = string.IsNullOrEmpty(it.PieceCount1) ? "0" : it.PieceCount1 + " 件";
                            newRows[sDT.ToString("yyyyMM") + "平均扫描时效"] = string.IsNullOrEmpty(it.AverageStartInterval1) ? "0" : it.AverageStartInterval1 + " 分钟";
                            newRows[sDT.ToString("yyyyMM") + "平均出库时效"] = string.IsNullOrEmpty(it.AverageEndInterval1) ? "0" : it.AverageEndInterval1 + " 分钟";
                            sDT = sDT.AddMonths(1);
                            break;
                        case 2:
                            newRows[sDT.ToString("yyyyMM") + "订单总数"] = string.IsNullOrEmpty(it.OrderCount2) ? "0" : it.OrderCount2 + " 单";
                            newRows[sDT.ToString("yyyyMM") + "出库总数"] = string.IsNullOrEmpty(it.PieceCount2) ? "0" : it.PieceCount2 + " 件";
                            newRows[sDT.ToString("yyyyMM") + "平均扫描时效"] = string.IsNullOrEmpty(it.AverageStartInterval2) ? "0" : it.AverageStartInterval2 + " 分钟";
                            newRows[sDT.ToString("yyyyMM") + "平均出库时效"] = string.IsNullOrEmpty(it.AverageEndInterval2) ? "0" : it.AverageEndInterval2 + " 分钟";
                            sDT = sDT.AddMonths(1);
                            break;
                        case 3:
                            newRows[sDT.ToString("yyyyMM") + "订单总数"] = string.IsNullOrEmpty(it.OrderCount3) ? "0" : it.OrderCount3 + " 单";
                            newRows[sDT.ToString("yyyyMM") + "出库总数"] = string.IsNullOrEmpty(it.PieceCount3) ? "0" : it.PieceCount3 + " 件";
                            newRows[sDT.ToString("yyyyMM") + "平均扫描时效"] = string.IsNullOrEmpty(it.AverageStartInterval3) ? "0" : it.AverageStartInterval3 + " 分钟";
                            newRows[sDT.ToString("yyyyMM") + "平均出库时效"] = string.IsNullOrEmpty(it.AverageEndInterval3) ? "0" : it.AverageEndInterval3 + " 分钟";
                            sDT = sDT.AddMonths(1);
                            break;
                        case 4:
                            newRows[sDT.ToString("yyyyMM") + "订单总数"] = string.IsNullOrEmpty(it.OrderCount4) ? "0" : it.OrderCount4 + " 单";
                            newRows[sDT.ToString("yyyyMM") + "出库总数"] = string.IsNullOrEmpty(it.PieceCount4) ? "0" : it.PieceCount4 + " 件";
                            newRows[sDT.ToString("yyyyMM") + "平均扫描时效"] = string.IsNullOrEmpty(it.AverageStartInterval4) ? "0" : it.AverageStartInterval4 + " 分钟";
                            newRows[sDT.ToString("yyyyMM") + "平均出库时效"] = string.IsNullOrEmpty(it.AverageEndInterval4) ? "0" : it.AverageEndInterval4 + " 分钟";
                            sDT = sDT.AddMonths(1);
                            break;
                        default:
                            break;
                    }
                }
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", OrderOutTime[0].StartDate.ToString("yyyy-MM") + "至" + OrderOutTime[0].EndDate.ToString("yyyy-MM") + "订单时效统计");

        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("AwbStatus"))
            {
                if (value.Trim() == "0")
                    retStr = "已下单";
                else if (value.Trim() == "1")
                    retStr = "正在备货";
                else if (value.Trim() == "2")
                    retStr = "已出库";
                else if (value.Trim() == "3")
                    retStr = "已装车";
                else if (value.Trim() == "4")
                    retStr = "已到达";
                else if (value.Trim() == "5")
                    retStr = "已签收";
                else if (value.Trim() == "6")
                    retStr = "已拣货";
                else if (value.Trim() == "7")
                    retStr = "配送";
            }
            return retStr;
        }
    }
}