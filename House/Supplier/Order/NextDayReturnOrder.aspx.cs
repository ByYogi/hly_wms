using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Order
{
    public partial class NextDayReturnOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 次日达退货订单
        /// </summary>
        public List<CargoOrderEntity> ReturnOrdersInfoList
        {
            get
            {
                if (Session["ReturnOrdersInfoList"] == null)
                {
                    Session["ReturnOrdersInfoList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["ReturnOrdersInfoList"]);
            }
            set
            {
                Session["ReturnOrdersInfoList"] = value;
            }
        }
        /// <summary>
        /// 导出次日达退货订单主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (ReturnOrdersInfoList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("退货时间", typeof(string));
            table.Columns.Add("退货单号", typeof(string));
            table.Columns.Add("件数", typeof(int));
            table.Columns.Add("客户单位", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("联系电话", typeof(string));
            table.Columns.Add("退货地址", typeof(string));
            table.Columns.Add("退货金额", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            List<CargoPurchaseOrderEntity> tot = new List<CargoPurchaseOrderEntity>();
            int i = 0;
            foreach (var it in ReturnOrdersInfoList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["退货时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["退货单号"] = it.OrderNo.ToString();
                newRows["件数"] = it.Piece;
                newRows["客户单位"] = it.AcceptUnit;
                newRows["客户名称"] = it.AcceptPeople;
                newRows["联系电话"] = it.AcceptCellphone;
                newRows["退货地址"] = it.AcceptAddress.ToString();
                newRows["退货金额"] = it.TotalCharge.ToString();
                newRows["订单状态"] = GetStatusStr(it.AwbStatus);
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "次日达退货单数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        /// <summary>
        /// 获取订单状态
        /// </summary>
        public string GetStatusStr(string status)
        {
            if (status.Equals("0")) { return "已下单"; }
            else if (status.Equals("1")) { return "正在备货"; }
            else if (status.Equals("2")) { return "已出库"; }
            else if (status.Equals("3")) { return "已装车"; }
            else if (status.Equals("4")) { return "已到达"; }
            else if (status.Equals("5")) { return "已签收"; }
            else if (status.Equals("6")) { return "已拣货"; }
            else if (status.Equals("7")) { return "配送"; }
            else if (status.Equals("8")) { return "到货确认"; }
            else { return string.Empty; }
        }
    }
}