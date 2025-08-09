using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Report
{
    public partial class reportDayStockStatis : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoDayStockEntity> CargoDayStock
        {
            get
            {
                if (Session["CargoDayStock"] == null)
                {
                    Session["CargoDayStock"] = new List<CargoDayStockEntity>();
                }
                return (List<CargoDayStockEntity>)(Session["CargoDayStock"]);
            }
            set
            {
                Session["CargoDayStock"] = value;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoDayStock.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("日期时间", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("当日库存", typeof(int));
            table.Columns.Add("当日库存价值", typeof(Double));
            table.Columns.Add("马牌库存", typeof(int));
            table.Columns.Add("马牌库存价值", typeof(Double));
            table.Columns.Add("优科库存", typeof(int));
            table.Columns.Add("优科库存价值", typeof(Double));
            table.Columns.Add("韩泰库存", typeof(int));
            table.Columns.Add("韩泰库存价值", typeof(Double));
            table.Columns.Add("固铂库存", typeof(int));
            table.Columns.Add("固铂库存价值", typeof(Double));
            table.Columns.Add("米其林库存", typeof(int));
            table.Columns.Add("米其林库存价值", typeof(Double));
            table.Columns.Add("沃凯途库存", typeof(int));
            table.Columns.Add("沃凯途库存价值", typeof(Double));
            table.Columns.Add("其它库存", typeof(int));
            table.Columns.Add("其它库存价值", typeof(Double));
            int i = 0;
            string housename = string.Empty;
            foreach (var it in CargoDayStock)
            {
                housename = it.Name;
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["日期时间"] = it.StatisDate.ToString("yyyy-MM-dd");
                newRows["所属仓库"] = it.Name;
                newRows["当日库存"] = it.TotalSum;
                newRows["当日库存价值"] = it.TotalValue;
                newRows["马牌库存"] = it.MaPaiSum;
                newRows["马牌库存价值"] = it.MaPaiTotalValue;
                newRows["优科库存"] = it.YKHMSum;
                newRows["优科库存价值"] = it.YKHMTotalValue;
                newRows["韩泰库存"] = it.HanTaiSum;
                newRows["韩泰库存价值"] = it.HanTaiTotalValue;
                newRows["固铂库存"] = it.GuBoSum;
                newRows["固铂库存价值"] = it.GuBoTotalValue;
                newRows["米其林库存"] = it.MQLSum;
                newRows["米其林库存价值"] = it.MQLTotalValue;
                newRows["沃凯途库存"] = it.WKTSum;
                newRows["沃凯途库存价值"] = it.WKTTotalValue;
                newRows["其它库存"] = it.QiTaSum;
                newRows["其它库存价值"] = it.QiTaTotalValue;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", housename + "库存统计表");
        }
    }
}