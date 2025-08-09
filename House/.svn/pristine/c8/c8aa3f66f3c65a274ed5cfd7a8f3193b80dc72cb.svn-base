using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class NotSignedOrderManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoOrderEntity> notSignedOrderList
        {
            get
            {
                if (Session["notSignedOrderList"] == null) { Session["notSignedOrderList"] = new List<CargoOrderEntity>(); }
                return (List<CargoOrderEntity>)(Session["notSignedOrderList"]);
            }
            set
            {
                Session["notSignedOrderList"] = value;
            }
        }
        protected void btnNotSignedOrder_Click(object sender, EventArgs e)
        {
            var List = notSignedOrderList;
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(Int32));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("物流单号", typeof(string));
            table.Columns.Add("物流公司", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            table.Columns.Add("出发站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("件数", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("联系手机", typeof(string));
            table.Columns.Add("收货地址", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("出库时间", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("出库仓库", typeof(string));

            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["订单号"] = it.GetType().GetProperty("OrderNo").GetValue(it, null).ToString();
                newRows["物流单号"] = it.GetType().GetProperty("LogisAwbNo").GetValue(it, null).ToString(); ;
                newRows["物流公司"] = it.GetType().GetProperty("LogisticName").GetValue(it, null).ToString();
                newRows["订单状态"] = it.GetType().GetProperty("AwbStatus").GetValue(it, null).ToString();
                newRows["出发站"] = it.GetType().GetProperty("Dep").GetValue(it, null).ToString(); ;
                newRows["到达站"] = it.GetType().GetProperty("Dest").GetValue(it, null).ToString();
                newRows["件数"] = it.GetType().GetProperty("Piece").GetValue(it, null).ToString();
                newRows["客户名称"] = it.GetType().GetProperty("AcceptUnit").GetValue(it, null).ToString();
                newRows["收货人"] = it.GetType().GetProperty("AcceptPeople").GetValue(it, null).ToString();
                newRows["联系手机"] = it.GetType().GetProperty("AcceptCellphone").GetValue(it, null).ToString();
                newRows["开单时间"] = it.GetType().GetProperty("CreateDate").GetValue(it, null).ToString();
                newRows["出库时间"] = it.GetType().GetProperty("OutCargoTime").GetValue(it, null).ToString();
                newRows["备注"] = it.GetType().GetProperty("Remark").GetValue(it, null).ToString();
                newRows["出库仓库"] = it.GetType().GetProperty("OutHouseName").GetValue(it, null).ToString();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "未签收订单列表");
        }
    }
}