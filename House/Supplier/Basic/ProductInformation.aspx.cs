using House.Entity.Cargo.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Basic
{
    public partial class ProductInformation : BasePage
    {
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<CargoSupplierProductPriceEntity> CargoSupplierProductPriceReport
        {
            get
            {
                if (Session["CargoSupplierProductPriceReport"] == null) { Session["CargoSupplierProductPriceReport"] = new List<CargoSupplierProductPriceEntity>(); }
                return (List<CargoSupplierProductPriceEntity>)(Session["CargoSupplierProductPriceReport"]);
            }
            set
            {
                Session["CargoSupplierProductPriceReport"] = value;
            }
        }
        /// <summary>
        /// 导出对账单
        /// </summary>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoSupplierProductPriceReport.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("入仓仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("进仓成本价", typeof(decimal));
            int i = 0;
            foreach (var it in CargoSupplierProductPriceReport)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["入仓仓库"] = it.HouseName;
                newRows["品牌"] = it.TypeName;
                newRows["产品编码"] = it.ProductCode.Trim();
                newRows["规格"] = it.Specs.Trim();
                newRows["花纹"] = it.Figure.Trim();
                newRows["货品代码"] = it.GoodsCode.Trim();
                newRows["载速"] = it.LoadIndex + it.SpeedLevel;
                newRows["进仓成本价"] = it.UnitPrice;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "产品基础数据表");

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }



    }
}