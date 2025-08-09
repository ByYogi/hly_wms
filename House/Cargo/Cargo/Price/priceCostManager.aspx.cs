using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Price
{
    public partial class priceCostManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoProductEntity> QueryPriceCostDataList
        {
            get
            {
                if (Session["QueryPriceCostDataList"] == null)
                {
                    Session["QueryPriceCostDataList"] = new List<CargoProductEntity>();
                }
                return (List<CargoProductEntity>)(Session["QueryPriceCostDataList"]);
            }
            set
            {
                Session["QueryPriceCostDataList"] = value;
            }
        }
        protected void btnPrice_Click(object sender, EventArgs e)
        {
            var List = QueryPriceCostDataList;
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(Int32));
            table.Columns.Add("产品ID", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            //table.Columns.Add("货位代码", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            //table.Columns.Add("型号", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速度", typeof(string));
            table.Columns.Add("入库数", typeof(string));
            table.Columns.Add("在库数", typeof(string));
            table.Columns.Add("成本价", typeof(string));
            table.Columns.Add("最终成本价", typeof(string));
            table.Columns.Add("单价", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("来源", typeof(string));
            table.Columns.Add("归属部门", typeof(string));
            table.Columns.Add("工厂订单号", typeof(string));
            table.Columns.Add("订单归属月", typeof(string));
            table.Columns.Add("入库时间", typeof(string));

            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["产品ID"] = it.GetType().GetProperty("ProductID").GetValue(it, null).ToString();
                newRows["产品名称"] = it.GetType().GetProperty("ProductName").GetValue(it, null).ToString(); ;
                newRows["品牌"] = it.GetType().GetProperty("TypeName").GetValue(it, null).ToString();
                //newRows["货位代码"] = it.GetType().GetProperty("ContainerCode").GetValue(it, null).ToString();
                newRows["货品代码"] = it.GetType().GetProperty("GoodsCode").GetValue(it, null).ToString();
                //newRows["型号"] = it.GetType().GetProperty("Model").GetValue(it, null).ToString();
                newRows["规格"] = it.GetType().GetProperty("Specs").GetValue(it, null).ToString();
                newRows["花纹"] = it.GetType().GetProperty("Figure").GetValue(it, null).ToString();
                newRows["载重"] = it.GetType().GetProperty("LoadIndex").GetValue(it, null).ToString();
                newRows["速度"] = it.GetType().GetProperty("SpeedLevel").GetValue(it, null).ToString();
                newRows["入库数"] = it.GetType().GetProperty("Numbers").GetValue(it, null).ToString();
                newRows["在库数"] = it.GetType().GetProperty("PackageNum").GetValue(it, null).ToString();
                newRows["成本价"] = it.GetType().GetProperty("CostPrice").GetValue(it, null).ToString();
                newRows["最终成本价"] = it.GetType().GetProperty("FinalCostPrice").GetValue(it, null).ToString();
                newRows["单价"] = it.GetType().GetProperty("UnitPrice").GetValue(it, null).ToString();
                newRows["批次"] = it.GetType().GetProperty("Batch").GetValue(it, null).ToString();
                newRows["来源"] = it.GetType().GetProperty("SourceName").GetValue(it, null).ToString();
                newRows["归属部门"] = GetText(it.GetType().GetProperty("BelongDepart").GetValue(it, null).ToString());
                newRows["工厂订单号"] = it.GetType().GetProperty("SourceOrderNo").GetValue(it, null).ToString();
                newRows["订单归属月"] = it.GetType().GetProperty("BelongMonth").GetValue(it, null).ToString();
                newRows["入库时间"] = it.GetType().GetProperty("InHouseTime").GetValue(it, null).ToString();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "成本价格列表" + DateTime.Now.ToString("MMddHHmm"));
        }
        private string GetText(string value)
        {
            string retStr = string.Empty;
            if (value.Trim() == "0")
            {
                retStr = "RE渠道销售部";
            }
            else if (value.Trim() == "1")
            {
                retStr = "OE渠道销售部";
            }
            else
            {
                retStr = value;
            }
            return retStr;
        }
    }
}