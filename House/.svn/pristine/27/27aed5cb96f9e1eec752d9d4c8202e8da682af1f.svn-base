using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer.Antyres
{
    public partial class OrderManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryOrderInfoList
        {
            get
            {
                if (Session["QueryOrderInfoList"] == null)
                {
                    Session["QueryOrderInfoList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryOrderInfoList"]);
            }
            set
            {
                Session["QueryOrderInfoList"] = value;
            }
        }
        /// <summary>
        /// 导出订单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrderInfo_Click(object sender, EventArgs e)
        {
            var List = QueryOrderInfoList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("合计", typeof(decimal));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("联系手机", typeof(string));
            table.Columns.Add("收货地址", typeof(string));
            //table.Columns.Add("订单状态", typeof(string));
            //table.Columns.Add("开单时间", typeof(string));
            //table.Columns.Add("出库时间", typeof(string));
            table.Columns.Add("结算状态", typeof(string));

            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["订单号"] = it.GetType().GetProperty("OrderNo").GetValue(it, null).ToString();
                newRows["数量"] = it.GetType().GetProperty("Piece").GetValue(it, null); 
                newRows["合计"] = it.GetType().GetProperty("TotalCharge").GetValue(it, null);
                newRows["客户名称"] = it.GetType().GetProperty("AcceptUnit").GetValue(it, null).ToString();
                newRows["收货人"] = it.GetType().GetProperty("AcceptPeople").GetValue(it, null).ToString();
                newRows["联系手机"] = it.GetType().GetProperty("AcceptTelephone").GetValue(it, null).ToString();
                newRows["收货地址"] = it.GetType().GetProperty("AcceptAddress").GetValue(it, null).ToString();
                //newRows["订单状态"] = GetText(it.GetType().GetProperty("AwbStatus").GetValue(it, null).ToString(), "AwbStatus");
                //newRows["开单时间"] = it.GetType().GetProperty("CreateDate").GetValue(it, null).ToString();
                // newRows["出库时间"] = it.GetType().GetProperty("OutCargoTime").GetValue(it, null).ToString() != "0001/1/1 0:00:00" ? it.GetType().GetProperty("OutCargoTime").GetValue(it, null).ToString() : "";
                newRows["结算状态"] = GetText(it.GetType().GetProperty("CheckStatus").GetValue(it, null).ToString(), "CheckStatus");
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "订单列表");
        }

        public List<CargoContainerShowEntity> QueryOrderGoodsList
        {
            get
            {
                if (Session["QueryOrderGoodsList"] == null)
                {
                    Session["QueryOrderGoodsList"] = new List<CargoContainerShowEntity>();
                }
                return (List<CargoContainerShowEntity>)(Session["QueryOrderGoodsList"]);
            }
            set
            {
                Session["QueryOrderGoodsList"] = value;
            }
        }
        protected void btnOrderGoods_Click(object sender, EventArgs e)
        {
            if (QueryOrderGoodsList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("销售价", typeof(decimal));
            table.Columns.Add("金额", typeof(decimal));
            int i = 0; int tSum = 0; decimal tCharge = 0.00M;
            string OrderNo = string.Empty;
            foreach (var it in QueryOrderGoodsList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["订单号"] = it.OrderNo;
                OrderNo = it.OrderNo;
                newRows["品牌"] = it.TypeName;
                newRows["货品代码"] = it.GoodsCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载速"] = it.LoadIndex + it.SpeedLevel;
                newRows["数量"] = it.Piece;
                newRows["销售价"] = it.ActSalePrice;
                newRows["金额"] = it.Piece * it.ActSalePrice;
                table.Rows.Add(newRows);
                tSum += it.Piece;
                tCharge += it.ActSalePrice * it.Piece;
            }
            i++;
            DataRow tRows = table.NewRow();
            tRows["序号"] = i++;
            tRows["订单号"] = "汇总：";
            tRows["数量"] = tSum;
            tRows["金额"] = tCharge;
            table.Rows.Add(tRows);

            ToExcel.DataTableToExcel(table, "", "订单号：" + OrderNo + "明细列表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            switch (id)
            {
                case "AwbStatus":
                    if (value.Trim() == "0")
                        retStr = "已下单";
                    else if (value.Trim() == "1")
                        retStr = "出库中";
                    else if (value.Trim() == "2")
                        retStr = "已出库";
                    else if (value.Trim() == "3")
                        retStr = "运输在途";
                    else if (value.Trim() == "4")
                        retStr = "已到达";
                    else if (value.Trim() == "5")
                        retStr = "已签收";
                    else if (value.Trim() == "6")
                        retStr = "已拣货";
                    else if (value.Trim() == "7")
                        retStr = "正在配送";
                    break;
                case "CheckStatus":
                    if (value.Trim() == "0")
                        retStr = "未结算";
                    else if (value.Trim() == "1")
                        retStr = "已结算";
                    else if (value.Trim() == "2")
                        retStr = "未结清";
                    break;
            }
            return retStr;
        }
    }
}