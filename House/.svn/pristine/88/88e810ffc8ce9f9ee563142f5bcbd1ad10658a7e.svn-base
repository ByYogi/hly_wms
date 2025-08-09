using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.House
{
    public partial class RealTimeStock : BasePage
    {
        public List<CargoProductEntity> CargoProductEntityList
        {
            get
            {
                if (Session["CargoProductEntityList"] == null)
                {
                    Session["CargoProductEntityList"] = new List<CargoProductEntity>();
                }
                return (List<CargoProductEntity>)(Session["CargoProductEntityList"]);
            }
            set
            {
                Session["CargoProductEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoProductEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速度", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("库存数量", typeof(int));
            table.Columns.Add("库存天数", typeof(int));
            table.Columns.Add("进仓价", typeof(string));
            table.Columns.Add("销售价", typeof(string));
            table.Columns.Add("入库时间", typeof(string));
            List<CargoProductEntity> tot = new List<CargoProductEntity>();
            int i = 0;
            foreach (var it in CargoProductEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["所在仓库"] = it.HouseName;
                newRows["产品编码"] = it.ProductCode;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载重"] = it.LoadIndex;
                newRows["速度"] = it.SpeedLevel;
                newRows["批次"] = it.Batch;
                newRows["库存数量"] = it.RealStockNum.ToString();
                newRows["库存天数"] = it.InHouseDay;
                newRows["进仓价"] = it.CostPrice.ToString();
                newRows["销售价"] = it.SalePrice.ToString();
                newRows["入库时间"] = it.InHouseTime.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "实时库存列表" + DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}