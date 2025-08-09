using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Report
{
    public partial class ToDayReturnDetails : BasePage
    {
        /// <summary>
        /// 导出即日达退货明细
        /// </summary>
        public List<CargoOrderGoodsEntity> QueryToDayReturnDetailsList
        {
            get
            {
                if (Session["QueryToDayReturnDetailsList"] == null)
                {
                    Session["QueryToDayReturnDetailsList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["QueryToDayReturnDetailsList"]);
            }
            set
            {
                Session["QueryToDayReturnDetailsList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出即日达退货明细明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (QueryToDayReturnDetailsList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("时间", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
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
            List<CargoOrderGoodsEntity> tot = new List<CargoOrderGoodsEntity>();
            int i = 0;
            foreach (var it in QueryToDayReturnDetailsList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["客户名称"] = it.AcceptUnit;
                newRows["订单号"] = it.OrderNo.ToString();
                newRows["产品编码"] = it.ProductCode;
                newRows["所在仓库"] = it.HouseName.ToString();
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载数"] = it.LoadSpeed;
                newRows["批次"] = it.Batch;
                newRows["产地"] = GetBornStr(it.Born);
                newRows["数量"] = it.Piece;
                newRows["成本单价"] = it.CostPrice.ToString();
                newRows["总成本"] = it.TotalCostPrice.ToString();
                newRows["销售单价"] = it.SalePrice.ToString();
                newRows["销售额"] = it.ActSalePrice.ToString();

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "即日达退货明细明细数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        public string GetBornStr(string value)
        {
            if (value == "0") { return "国产"; }
            else if (value == "1") { return "进口"; }
            else { return ""; }
        }
    }
}