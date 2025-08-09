using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.House
{
    public partial class StockBook : BasePage
    {
        public List<CargoProductEntity> CargoStockBookData
        {
            get
            {
                if (Session["CargoStockBookData"] == null)
                {
                    Session["CargoStockBookData"] = new List<CargoProductEntity>();
                }
                return (List<CargoProductEntity>)(Session["CargoStockBookData"]);
            }
            set
            {
                Session["CargoStockBookData"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoStockBookData.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速度", typeof(string));
            table.Columns.Add("库存数", typeof(int));
            //table.Columns.Add("平均成本价", typeof(decimal));
            table.Columns.Add("平均单价", typeof(decimal));
            table.Columns.Add("上一进货价", typeof(decimal));
            table.Columns.Add("供应商", typeof(string));
            table.Columns.Add("进货时间", typeof(string));

            int i = 0;
            foreach (var it in CargoStockBookData)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["仓库"] = it.FirstAreaName;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载重"] = it.LoadIndex;
                newRows["速度"] = it.SpeedLevel;
                newRows["库存数"] = it.InPiece;
                //newRows["平均成本价"] = it.CostPrice;
                newRows["平均单价"] = it.UnitPrice;
                newRows["上一进货价"] = it.SalePrice;
                newRows["供应商"] = it.PurchaserName;
                newRows["进货时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "订货查询数据统计表");

        }
    }
}