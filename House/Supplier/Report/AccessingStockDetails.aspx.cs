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
    public partial class AccessingStockDetails : BasePage
    {
        /// <summary>
        /// 访问备货明细List
        /// </summary>
        public List<CargoAccessingStockDetailsEntity> CargoStockDetailsEntityList
        {
            get
            {
                if (Session["CargoStockDetailsEntityList"] == null)
                {
                    Session["CargoStockDetailsEntityList"] = new List<CargoAccessingStockDetailsEntity>();
                }
                return (List<CargoAccessingStockDetailsEntity>)(Session["CargoStockDetailsEntityList"]);
            }
            set
            {
                Session["CargoStockDetailsEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出访问备货明细主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoStockDetailsEntityList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载数", typeof(string));
            table.Columns.Add("访问数量", typeof(int));
            table.Columns.Add("销售数量", typeof(int));
            table.Columns.Add("库存数量", typeof(int));
            table.Columns.Add("最近入库时间", typeof(string));
            table.Columns.Add("库存状况", typeof(string));

            List<CargoOrderGoodsEntity> tot = new List<CargoOrderGoodsEntity>();
            int i = 0;
            foreach (var it in CargoStockDetailsEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["所在仓库"] = it.HouseName;
                newRows["产品编码"] = it.ProductCode;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载数"] = it.LoadSpeed;
                newRows["访问数量"] = it.AccessCount;
                newRows["销售数量"] = it.SaleOrderNum;
                newRows["库存数量"] = it.Piece;
                newRows["最近入库时间"] = it.InHouseTimeStr;//.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["库存状况"] = it.Piece < 4 ? "低库存" : "高库存";

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "访问备货明细数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        public string GetBornStr(string value)
        {
            if (value == "0") { return "国产"; }
            else if (value == "1") { return "进口"; }
            else { return ""; }
        }
    }
}