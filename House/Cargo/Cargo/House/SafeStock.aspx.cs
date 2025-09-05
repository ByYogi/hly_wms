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
    public partial class SafeSnap : BasePage
    {
        public List<CargoSafeStockEntity> CargoSafeStockData
        {
            get
            {
                if (Session["CargoSafeStockData"] == null)
                {
                    Session["CargoSafeStockData"] = new List<CargoSafeStockEntity>();
                }
                return (List<CargoSafeStockEntity>)(Session["CargoSafeStockData"]);
            }
            set
            {
                Session["CargoSafeStockData"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoSafeStockData.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("区域大仓", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载重指数", typeof(string));
            table.Columns.Add("速度级别", typeof(string));

            table.Columns.Add("最小库存", typeof(int));
            table.Columns.Add("最大库存", typeof(int));
            table.Columns.Add("补货数量", typeof(int));

            table.Columns.Add("安全库存", typeof(int));
            table.Columns.Add("在库库存", typeof(int));
            table.Columns.Add("在途库存", typeof(int));
            table.Columns.Add("全国在库库存", typeof(int));
            table.Columns.Add("云仓销售数量", typeof(int));
            table.Columns.Add("全渠道销售数量", typeof(int));
            table.Columns.Add("全国销售数量", typeof(int));
            table.Columns.Add("月均销量", typeof(int));
            table.Columns.Add("库存度天数", typeof(int));
            int i = 0;
            int TStockNum = 0, TCurNum = 0, TLessNum = 0, TSaleNum=0, 
                TMinStock = 0, TMaxStock = 0,TMoveNum = 0, TTotalNum = 0, 
                TWXSaleNum = 0, TTotalSaleNum = 0, TAvgSaleNum = 0, TDoi = 0;
            foreach (var it in CargoSafeStockData)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["区域大仓"] = it.HouseName;
                newRows["所属仓库"] = it.AreaName;
                newRows["产品编码"] = it.ProductCode;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs.Trim();
                newRows["花纹"] = it.Figure.Trim();
                newRows["货品代码"] = it.GoodsCode.Trim();
                newRows["载重指数"] = it.LoadIndex;
                newRows["速度级别"] = it.SpeedLevel;

                newRows["最小库存"] = it.MinStock;
                newRows["最大库存"] = it.MaxStock;
                newRows["补货数量"] = it.LessNum;

                newRows["安全库存"] = it.StockNum;
                newRows["在库库存"] = it.CurNum;
                newRows["在途库存"] = it.MoveNum;
                newRows["全国在库库存"] = it.TotalNum;
                newRows["云仓销售数量"] = it.WXSaleNum;
                newRows["全渠道销售数量"] = it.SaleNum;
                newRows["全国销售数量"] = it.TotalSaleNum;
                newRows["月均销量"] = it.AvgSaleNum;
                newRows["库存度天数"] = it.DOI;

                TStockNum += it.StockNum; TCurNum += it.CurNum;
                TLessNum += it.LessNum; TSaleNum += it.SaleNum;
                TMinStock += it.MinStock.GetValueOrDefault(); TMaxStock += it.MaxStock.GetValueOrDefault();
                TMoveNum += it.MoveNum.GetValueOrDefault(); TTotalNum += it.TotalNum.GetValueOrDefault();
                TWXSaleNum += it.WXSaleNum.GetValueOrDefault(); TTotalSaleNum += it.TotalSaleNum.GetValueOrDefault();
                TAvgSaleNum += it.AvgSaleNum.GetValueOrDefault(); TDoi += it.DOI.GetValueOrDefault();
                table.Rows.Add(newRows);
            }
            DataRow footRow = table.NewRow();
            footRow["区域大仓"] = "汇总：";
            footRow["最小库存"] = TMinStock;
            footRow["最大库存"] = TMaxStock;
            footRow["补货数量"] = TLessNum;
            footRow["安全库存"] = TStockNum;
            footRow["在库库存"] = TCurNum;
            footRow["在途库存"] = TMoveNum;
            footRow["全国在库库存"] = TTotalNum;
            footRow["云仓销售数量"] = TWXSaleNum;
            footRow["全渠道销售数量"] = TSaleNum;
            footRow["全国销售数量"] = TTotalSaleNum;
            footRow["月均销量"] = TAvgSaleNum;
            footRow["库存度天数"] = TDoi;
            table.Rows.Add(footRow);

            ToExcel.DataTableToExcel(table, "", "安全库存数据表");

        }
    }
}