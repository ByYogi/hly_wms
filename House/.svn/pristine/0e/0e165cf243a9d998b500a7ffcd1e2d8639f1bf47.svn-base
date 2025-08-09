using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Arrive
{
    public partial class CarStatusTrack : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<AwbEntity> AwbMonitor
        {
            get
            {
                if (Session["AwbMonitor"] == null)
                {
                    Session["AwbMonitor"] = new List<AwbEntity>();
                }
                return (List<AwbEntity>)(Session["AwbMonitor"]);
            }
            set
            {
                Session["AwbMonitor"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (AwbMonitor.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(decimal));
            table.Columns.Add("运单号", typeof(string));
            table.Columns.Add("开单日期", typeof(DateTime));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("出发站", typeof(string));
            table.Columns.Add("中转站", typeof(string));
            table.Columns.Add("移库站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("品名", typeof(string));
            table.Columns.Add("总件数", typeof(decimal));
            table.Columns.Add("分批件数", typeof(decimal));
            table.Columns.Add("分批重量", typeof(decimal));
            table.Columns.Add("分批体积", typeof(decimal));
            table.Columns.Add("运输方式", typeof(string));
            table.Columns.Add("附加信息", typeof(string));
            int i = 0;
            foreach (var it in AwbMonitor)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["运单号"] = it.AwbNo;
                newRows["开单日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                newRows["客户名称"] = it.ShipperUnit.Trim();
                newRows["出发站"] = it.Dep.Trim();
                newRows["中转站"] = it.Transit.Trim();
                newRows["移库站"] = it.MidDest.Trim();
                newRows["到达站"] = it.Dest.Trim();
                newRows["品名"] = it.Goods.Trim();
                newRows["总件数"] = it.Piece;
                newRows["分批件数"] = it.AwbPiece;
                newRows["分批重量"] = it.AwbWeight;
                newRows["分批体积"] = it.AwbVolume;
                newRows["运输方式"] = GetText(it.TrafficType, "TrafficType");
                newRows["附加信息"] = it.Remark.Trim();
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "配载运单表");
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
            if (id.Contains("TrafficType"))
            {
                if (value.Trim() == "0")
                    retStr = "普汽";
                else if (value.Trim() == "1")
                    retStr = "包车";
                else if (value.Trim() == "2")
                    retStr = "加急";
                else if (value.Trim() == "3")
                    retStr = "铁路";
            }
            return retStr;
        }
    }
}