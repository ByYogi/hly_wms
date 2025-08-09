using House.Entity.Cargo.Order;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;

namespace Supplier.Report
{
    public partial class OrderSaleDetails : BasePage
    {
        /// <summary>
        /// 订单销售明细List
        /// </summary>
        public List<CargoOrderGoodsEntity> OrderSaleDetailsEntityList
        {
            get
            {
                if (Session["OrderSaleDetailsEntityList"] == null)
                {
                    Session["OrderSaleDetailsEntityList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["OrderSaleDetailsEntityList"]);
            }
            set
            {
                Session["OrderSaleDetailsEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出订单销售明细主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (OrderSaleDetailsEntityList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("出库仓库", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载数", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("产地", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("成本单价", typeof(string));
            table.Columns.Add("总成本", typeof(string));
            table.Columns.Add("销售单价", typeof(string));
            table.Columns.Add("销售额", typeof(string));
            int i = 0;
            int TotalPiece = 0; decimal TotalCost = 0.00M; decimal TotalSale = 0.00M;
            foreach (var it in OrderSaleDetailsEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["出库仓库"] = it.HouseName.ToString();
                newRows["开单时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                //newRows["订单类型"] = GetOrderModel(it.OrderModel);
                newRows["订单类型"] = GetOrderTypeStr(it.ThrowGood);
                newRows["订单号"] = it.OrderNo.ToString();
                newRows["客户名称"] = it.AcceptUnit;
                newRows["品牌"] = it.TypeName;
                newRows["产品编码"] = it.ProductCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载数"] = it.LoadSpeed;
                newRows["批次"] = it.Batch;
                newRows["产地"] = GetBornStr(it.Born);

                if (it.OrderModel.Equals("1"))
                {
                    newRows["数量"] = "-" + it.Piece;
                    newRows["成本单价"] = it.CostPrice.ToString();
                    newRows["总成本"] = "-" + it.TotalCostPrice.ToString();
                    newRows["销售单价"] = it.ActSalePrice.ToString();
                    newRows["销售额"] = "-" + it.SalePrice.ToString();
                    TotalPiece -= it.Piece;
                    TotalCost -= it.TotalCostPrice;
                    TotalSale -= it.SalePrice;
                }
                else
                {
                    newRows["数量"] = it.Piece;
                    newRows["成本单价"] = it.CostPrice.ToString();
                    newRows["总成本"] = it.TotalCostPrice.ToString();
                    newRows["销售单价"] = it.ActSalePrice.ToString();
                    newRows["销售额"] = it.SalePrice.ToString();

                    TotalPiece += it.Piece;
                    TotalCost += it.TotalCostPrice;
                    TotalSale += it.SalePrice;
                }
                table.Rows.Add(newRows);
            }
            DataRow footRows = table.NewRow();
            footRows["出库仓库"] = "汇总：";
            footRows["数量"] = TotalPiece;
            footRows["总成本"] = TotalCost;
            footRows["销售额"] = TotalSale;
            table.Rows.Add(footRows);

            ToExcel.DataTableToExcel(table, "", "订单销售明细数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        public string GetBornStr(string value)
        {
            if (value == "0") { return "国产"; }
            else if (value == "1") { return "进口"; }
            else { return ""; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string GetOrderTypeStr(string status)
        {
            if (status.Equals("22")) { return "急送单"; }
            else if (status.Equals("23")) { return "次日达单"; }
            else if (status.Equals("24")) { return "渠道单"; }
            else if (status.Equals("25")) { return "退仓单"; }
            else if (status.Equals("5")) { return "退货单"; }
            else if (status.Equals("0")) { return "普送单"; }
            else { return string.Empty; }
        }

        public string GetOrderModel(string value)
        {
            if (value == "0") { return "销售单"; }
            else if (value == "1") { return "退货单"; }
            else { return ""; }
        }
    }
}