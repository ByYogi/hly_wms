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
    public partial class FactoryPurchaseOrderManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoFactoryPurchaseOrderGoodsEntity> QueryFactoryPurchaseOrderGoodsList
        {
            get
            {
                if (Session["QueryFactoryPurchaseOrderGoodsList"] == null)
                {
                    Session["QueryFactoryPurchaseOrderGoodsList"] = new List<CargoFactoryPurchaseOrderGoodsEntity>();
                }
                return (List<CargoFactoryPurchaseOrderGoodsEntity>)(Session["QueryFactoryPurchaseOrderGoodsList"]);
            }
            set
            {
                Session["QueryFactoryPurchaseOrderGoodsList"] = value;
            }
        }
        protected void btnOrderGoods_Click(object sender, EventArgs e)
        {
            if (QueryFactoryPurchaseOrderGoodsList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速度", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("进货价", typeof(decimal));
            int i = 0; int tSum = 0; double tCharge = 0;
            string OrderNo = string.Empty;
            foreach (var it in QueryFactoryPurchaseOrderGoodsList)
            {
                DataRow newRows = table.NewRow();
                OrderNo = it.OrderNo;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载重"] = it.LoadIndex;
                newRows["速度"] = it.SpeedLevel;
                newRows["数量"] = it.Piece;
                newRows["进货价"] = it.UnitPrice;
                table.Rows.Add(newRows);
                tSum += it.Piece;
                tCharge += it.UnitPrice * it.Piece;
            }
            ToExcel.DataTableToExcel(table, "", "工厂进货单" + OrderNo + "明细列表");
        }
    }
}