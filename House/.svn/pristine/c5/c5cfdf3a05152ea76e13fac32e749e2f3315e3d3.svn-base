using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Report
{
    public partial class PurchaseOrderDetails : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoPurchaseOrderGoodsEntity> QueryPurchaseOrderDetailsList
        {
            get
            {
                if (Session["QueryPurchaseOrderDetailsList"] == null)
                {
                    Session["QueryPurchaseOrderDetailsList"] = new List<CargoPurchaseOrderGoodsEntity>();
                }
                return (List<CargoPurchaseOrderGoodsEntity>)(Session["QueryPurchaseOrderDetailsList"]);
            }
            set
            {
                Session["QueryPurchaseOrderDetailsList"] = value;
            }
        }
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (QueryPurchaseOrderDetailsList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("进仓单号", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("批次", typeof(int));
            table.Columns.Add("进仓数量", typeof(string));
            table.Columns.Add("成本单价", typeof(string));
            table.Columns.Add("成本金额", typeof(string));
            table.Columns.Add("销售单价", typeof(string));
            List<CargoPurchaseOrderEntity> tot = new List<CargoPurchaseOrderEntity>();
            int i = 0;
            foreach (var it in QueryPurchaseOrderDetailsList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["进仓单号"] = it.FacOrderNo;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载速"] = it.LoadIndex+it.SpeedLevel;
                newRows["批次"] = it.Batch;
                newRows["进仓数量"] = it.Piece ;
                newRows["成本单价"] = it.CostPrice;
                newRows["成本金额"] = it.Piece* it.CostPrice;
                newRows["销售单价"] = it.SalePrice;

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "进仓单明细" + DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}