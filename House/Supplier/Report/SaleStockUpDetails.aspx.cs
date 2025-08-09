using House.Entity.Cargo;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Report
{
    public partial class SaleStockUpDetails : BasePage
    {
        /// <summary>
        /// 销售备货明细List
        /// </summary>
        public List<CargoOrderGoodsEntity> OrderSaleStockUpDetailsEntityList
        {
            get
            {
                if (Session["OrderSaleStockUpDetailsEntityList"] == null)
                {
                    Session["OrderSaleStockUpDetailsEntityList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["OrderSaleStockUpDetailsEntityList"]);
            }
            set
            {
                Session["OrderSaleStockUpDetailsEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出销售备货明细主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (OrderSaleStockUpDetailsEntityList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载数", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("产地", typeof(string));
            table.Columns.Add("销售数量", typeof(string));
            table.Columns.Add("平均销售价格", typeof(string));
            table.Columns.Add("销售总额", typeof(string));
            table.Columns.Add("销售价", typeof(string));
            table.Columns.Add("库存", typeof(int));
            table.Columns.Add("库存金额", typeof(string));
            table.Columns.Add("最近成本价", typeof(string));
            table.Columns.Add("最近入库时间", typeof(string));

            List<CargoOrderGoodsEntity> tot = new List<CargoOrderGoodsEntity>();
            int i = 0;
            foreach (var it in OrderSaleStockUpDetailsEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["所在仓库"] = it.HouseName;
                newRows["产品编码"] = it.ProductCode;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载数"] = it.LoadSpeed;
                newRows["批次"] = it.Batch;
                newRows["产地"] = GetBornStr(it.Born);
                newRows["销售数量"] = it.Piece.ToString();
                newRows["平均销售价格"] = it.AvgSalePrice.ToString();
                newRows["销售总额"] = it.ActSalePrice.ToString();
                newRows["销售价"] = it.SalePrice.ToString();
                newRows["库存"] = it.InHousePiece;
                newRows["库存金额"] = it.InHouseTotalPrice.ToString();
                newRows["最近成本价"] = it.CostPrice.ToString();
                newRows["最近入库时间"] = it.InHouseTime.ToString("yyyy-MM-dd HH:mm:ss");

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "销售备货明细数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        public string GetBornStr(string value)
        {
            if (value == "0") { return "国产"; }
            else if (value == "1") { return "进口"; }
            else { return ""; }
        }
    }
}