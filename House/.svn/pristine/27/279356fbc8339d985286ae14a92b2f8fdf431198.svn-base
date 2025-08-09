using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class FactoryOrderBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryBarcodeDataList
        {
            get
            {
                if (Session["QueryBarcodeDataList"] == null)
                {
                    Session["QueryBarcodeDataList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryBarcodeDataList"]);
            }
            set
            {
                Session["QueryBarcodeDataList"] = value;
            }
        }
        protected void btnBarcode_Click(object sender, EventArgs e)
        {
            var List = QueryBarcodeDataList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(Int32));
            table.Columns.Add("船运号", typeof(string));
            table.Columns.Add("轮胎码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载重指数", typeof(string));
            table.Columns.Add("速度级别", typeof(string));
            table.Columns.Add("出库时间", typeof(string));

            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["船运号"] = it.GetType().GetProperty("VehicleNo").GetValue(it, null).ToString();
                newRows["轮胎码"] = it.GetType().GetProperty("Barcode").GetValue(it, null).ToString(); ;
                newRows["规格"] = it.GetType().GetProperty("Specs").GetValue(it, null).ToString();
                newRows["花纹"] = it.GetType().GetProperty("Figure").GetValue(it, null).ToString();
                newRows["批次"] = it.GetType().GetProperty("Batch").GetValue(it, null).ToString(); ;
                newRows["货品代码"] = it.GetType().GetProperty("GoodsCode").GetValue(it, null).ToString();
                newRows["载重指数"] = it.GetType().GetProperty("LoadIndex").GetValue(it, null).ToString();
                newRows["速度级别"] = it.GetType().GetProperty("SpeedLevel").GetValue(it, null).ToString();
                newRows["出库时间"] = it.GetType().GetProperty("OutTime").GetValue(it, null).ToString();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "轮胎码列表");
        }
    }
}