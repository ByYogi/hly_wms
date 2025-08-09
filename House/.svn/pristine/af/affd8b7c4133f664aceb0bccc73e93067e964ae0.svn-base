using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Price
{
    public partial class priceModifyStatis : BasePage
    {

        public List<CargoPriceModifyEntity> CargoPriceModifyExportEntity
        {
            get
            {
                if (Session["CargoPriceModifyExportEntity"] == null) { Session["CargoPriceModifyExportEntity"] = new List<CargoPriceModifyEntity>(); }
                return (List<CargoPriceModifyEntity>)(Session["CargoPriceModifyExportEntity"]);
            }
            set
            {
                Session["CargoPriceModifyExportEntity"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnShopOrder_Click(object sender, EventArgs e)
        {
            if (CargoPriceModifyExportEntity.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("仓库名称", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("修改人姓名", typeof(string));
            table.Columns.Add("修改前价格", typeof(decimal));
            table.Columns.Add("修改后价格", typeof(decimal));
            table.Columns.Add("改价类型", typeof(string));
            table.Columns.Add("修改系统", typeof(string));
            table.Columns.Add("修改时间", typeof(string));
            List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
            int i = 0;
            foreach (var it in CargoPriceModifyExportEntity)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["仓库名称"] = it.HouseName;
                newRows["品牌"] = it.TypeName;
                newRows["产品编码"] = it.ProductCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载速"] = it.LoadIndex+it.SpeedLevel;
                newRows["货品代码"] = it.GoodsCode;
                newRows["修改人姓名"] = it.UserName;
                newRows["修改前价格"] = it.OldPrice;
                newRows["修改后价格"] = it.NewPrice;

                newRows["改价类型"] = GetText(it.PriceType, "PriceType");
                newRows["修改系统"] = GetText(it.ModifySource, "ModifySource");
                newRows["修改时间"] = it.OPDate.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "产品价格修改数据记录表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("PriceType"))
            {
                if (value.Trim() == "0")
                    retStr = "小程序价";
                else if (value.Trim() == "1")
                    retStr = "进仓价";
                else if (value.Trim() == "3")
                    retStr = "门店价";
                else if (value.Trim() == "4")
                    retStr = "成本价";
                else if (value.Trim() == "2")
                    retStr = "单价";
            }
            else if (id.Contains("ModifySource"))
            {
                if (value.Trim() == "0")
                    retStr = "智能仓储系统";
                else if (value.Trim() == "1")
                    retStr = "供应商系统";

            }
            return retStr;
        }
    }
}