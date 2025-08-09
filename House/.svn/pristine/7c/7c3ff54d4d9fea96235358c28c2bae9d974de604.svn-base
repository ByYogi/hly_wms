using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Report
{
    public partial class ClientArrearsStatistics : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryClientArrearsStatisticsList
        {
            get
            {
                if (Session["QueryClientArrearsStatisticsList"] == null)
                {
                    Session["QueryClientArrearsStatisticsList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryClientArrearsStatisticsList"]);
            }
            set
            {
                Session["QueryClientArrearsStatisticsList"] = value;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            var List = QueryClientArrearsStatisticsList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            list = list.OrderBy(c => c.GetType().GetProperty("CreateDate").GetValue(c, null)).ToList();
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("联系人", typeof(string));
            table.Columns.Add("电话", typeof(string));
            table.Columns.Add("地址", typeof(string));
            table.Columns.Add("业务员", typeof(string));
            table.Columns.Add("订单总数", typeof(int));
            table.Columns.Add("欠款合计", typeof(decimal));
            table.Columns.Add("最早下单时间", typeof(string));
            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["客户名称"] = it.GetType().GetProperty("AcceptUnit").GetValue(it, null).ToString();
                newRows["联系人"] = it.GetType().GetProperty("AcceptPeople").GetValue(it, null).ToString();
                newRows["电话"] = it.GetType().GetProperty("AcceptCellphone").GetValue(it, null).ToString();
                newRows["地址"] = it.GetType().GetProperty("AcceptAddress").GetValue(it, null).ToString();
                newRows["业务员"] = it.GetType().GetProperty("SaleManName").GetValue(it, null).ToString();
                newRows["订单总数"] = it.GetType().GetProperty("OrderCount").GetValue(it, null);
                newRows["欠款合计"] = it.GetType().GetProperty("TotalCharge").GetValue(it, null);
                newRows["最早下单时间"] = it.GetType().GetProperty("CreateDate").GetValue(it, null).ToString();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "客户欠款统计");
        }
    }
}