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
    public partial class reportSaleStock : BasePage
    {

        public List<CargoSaleStockStatisEntity> CarSaleStock
        {
            get
            {
                if (Session["CarSaleStock"] == null)
                {
                    Session["CarSaleStock"] = new List<CargoSaleStockStatisEntity>();
                }
                return (List<CargoSaleStockStatisEntity>)(Session["CarSaleStock"]);
            }
            set
            {
                Session["CarSaleStock"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CarSaleStock.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("总入库", typeof(int));
            table.Columns.Add("总出库", typeof(int));
            table.Columns.Add("出库入库比", typeof(string));
            table.Columns.Add("当前库存数", typeof(string));
            DateTime sDT = CarSaleStock[0].StartDate;
            DateTime eDT = CarSaleStock[0].EndDate;
            int Month = (eDT.Year - sDT.Year) * 12 + (eDT.Month - sDT.Month);
            for (int j = 1; j <= Month + 1; j++)
            {
                if (sDT.Month == eDT.Month)
                {
                    table.Columns.Add(eDT.ToString("yyyyMM") + "入库", typeof(int));
                    table.Columns.Add(eDT.ToString("yyyyMM") + "出库", typeof(int));
                }
                else
                {
                    table.Columns.Add(sDT.ToString("yyyyMM") + "入库", typeof(int));
                    table.Columns.Add(sDT.ToString("yyyyMM") + "出库", typeof(int));
                    sDT = sDT.AddMonths(1);
                }
            }

            int i = 0;
            foreach (var it in CarSaleStock)
            {
                sDT = CarSaleStock[0].StartDate;
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["总入库"] = it.InTotalNum;
                newRows["总出库"] = it.OutTotalNum;
                newRows["出库入库比"] = it.OutInRate;
                newRows["当前库存数"] = it.CurrentNum;
                for (int t = 1; t <= Month + 1; t++)
                {
                    switch (t)
                    {
                        case 1:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In1;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out1;
                            sDT = sDT.AddMonths(1);
                            break;
                        case 2:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In2;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out2;
                            sDT = sDT.AddMonths(1);
                            break;
                        case 3:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In3;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out3;
                            sDT = sDT.AddMonths(1);
                            break;
                        case 4:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In4;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out4;
                            sDT = sDT.AddMonths(1);
                            break;
                        case 5:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In5;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out5;
                            sDT = sDT.AddMonths(1);
                            break;
                        case 6:
                            newRows[sDT.ToString("yyyyMM") + "入库"] = it.In6;
                            newRows[sDT.ToString("yyyyMM") + "出库"] = it.Out6;
                            sDT = sDT.AddMonths(1);
                            break;
                        default:
                            break;
                    }
                }
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", CarSaleStock[0].StartDate.ToString("yyyy-MM") + "至" + CarSaleStock[0].EndDate.ToString("yyyy-MM") + "销售库存统计报表");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}