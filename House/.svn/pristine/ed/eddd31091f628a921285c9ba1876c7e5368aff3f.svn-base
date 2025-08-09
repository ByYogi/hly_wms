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
    public partial class reportGross : BasePage
    {
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<SaleManReportEntity> CarProfit
        {
            get
            {
                if (Session["SaleManReport"] == null) { Session["SaleManReport"] = new List<SaleManReportEntity>(); } return (List<SaleManReportEntity>)(Session["SaleManReport"]);
            }
            set
            {
                Session["SaleManReport"] = value;
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
            if (CarProfit.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("开单日期", typeof(string));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("产品类型", typeof(string));
            table.Columns.Add("销售价", typeof(decimal));

            table.Columns.Add("成本价", typeof(decimal));
            table.Columns.Add("毛利", typeof(decimal));

            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("总毛利", typeof(decimal));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("型号", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("载重指数", typeof(int));
            table.Columns.Add("速度级别", typeof(string));
            table.Columns.Add("目的站", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("客户编码", typeof(string));
            table.Columns.Add("客户单位", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("业务员", typeof(string));
            table.Columns.Add("所在货位", typeof(string));
            table.Columns.Add("所在区域", typeof(string));
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("入库时间", typeof(string));
            table.Columns.Add("订单归属月", typeof(string));
            table.Columns.Add("下单方式", typeof(string));
            table.Columns.Add("调货(代发)仓库", typeof(string));
            int i = 0;
            foreach (var it in CarProfit)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["开单日期"] = it.CreateDate.ToString("yyyy-MM-dd") == "0001-01-01" || it.CreateDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : it.CreateDate.ToString("yyyy-MM-dd");
                newRows["订单号"] = it.OrderNo.Trim();
                newRows["订单类型"] = GetText(it.ThrowGood.Trim(), "ThrowGood");
                newRows["产品名称"] = it.ProductName;
                newRows["产品类型"] = it.TypeName;
                newRows["销售价"] = it.ActSalePrice;

                newRows["成本价"] = it.CostPrice;
                newRows["毛利"] = it.Gross;

                newRows["数量"] = it.Piece;
                newRows["总毛利"] = it.TotalGross;
                newRows["规格"] = it.Specs.Trim();
                newRows["花纹"] = it.Figure.Trim();
                newRows["型号"] = it.Model.Trim();
                newRows["批次"] = it.Batch;
                newRows["载重指数"] = it.LoadIndex;
                newRows["速度级别"] = it.SpeedLevel;
                newRows["目的站"] = it.Dest.Trim();
                newRows["货品代码"] = it.GoodsCode.Trim();
                newRows["客户编码"] = it.ClientNum.ToString();
                newRows["客户单位"] = it.AcceptUnit;
                newRows["客户名称"] = it.AcceptPeople;
                newRows["手机号码"] = it.AcceptCellphone;
                newRows["业务员"] = it.SaleManName.Trim();
                newRows["所在货位"] = it.ContainerCode;
                newRows["所在区域"] = it.AreaName;
                newRows["所在仓库"] = it.FirstAreaName;
                newRows["入库时间"] = it.InHouseTime.ToString("yyyy-MM-dd") == "0001-01-01" || it.InHouseTime.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : it.InHouseTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["订单归属月"] = it.BelongMonth;
                newRows["下单方式"] = GetText(it.OrderType, "OrderType");
                newRows["调货(代发)仓库"] = it.TranHouse;
                table.Rows.Add(newRows);
            }
            string dt = string.Empty;
            if (CarProfit[0].StartDate.Equals(CarProfit[0].EndDate))
            {
                dt = CarProfit[0].StartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                dt = CarProfit[0].StartDate.ToString("yyyy-MM-dd") + "--" + CarProfit[0].EndDate.ToString("yyyy-MM-dd");
            }
            ToExcel.DataTableToExcel(table, "", dt + "毛利报表统计");

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
            if (id.Contains("OrderModel"))
            {
                if (value.Trim() == "0")
                    retStr = "发货单";
                else if (value.Trim() == "1")
                    retStr = "退货单";
            }
            else if (id.Contains("ThrowGood"))
            {
                if (value.Trim() == "0")
                    retStr = "客户单";
                else if (value.Trim() == "1")
                    retStr = "抛货单";
                else if (value.Trim() == "2")
                    retStr = "调货单";
                else if (value.Trim() == "3")
                    retStr = "代发单";
            }
            else if (id.Contains("CheckOutType"))
            {
                if (value.Trim() == "0")
                    retStr = "现付";
                else if (value.Trim() == "1")
                    retStr = "回单";
                else if (value.Trim() == "2")
                    retStr = "月结";
                else if (value.Trim() == "3")
                    retStr = "到付";
                else if (value.Trim() == "4")
                    retStr = "代收款";
            }
            else if (id.Contains("OrderType"))
            {
                if (value.Trim() == "0")
                    retStr = "电脑下单";
                else if (value.Trim() == "1")
                    retStr = "企业号下单";
                else if (value.Trim() == "2")
                    retStr = "商城下单";
                else if (value.Trim() == "3")
                    retStr = "APP下单";
                else if (value.Trim() == "4")
                    retStr = "小程序下单";
            }
            return retStr;
        }
    }
}