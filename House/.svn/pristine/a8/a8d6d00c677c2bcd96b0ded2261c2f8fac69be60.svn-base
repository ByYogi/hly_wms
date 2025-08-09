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
    public partial class BatchImportOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryBatchImportOrderDataList
        {
            get
            {
                if (Session["QueryBatchImportOrderDataList"] == null)
                {
                    Session["QueryBatchImportOrderDataList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryBatchImportOrderDataList"]);
            }
            set
            {
                Session["QueryBatchImportOrderDataList"] = value;
            }
        }
        /// <summary>
        /// 导出订单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrderInfo_Click(object sender, EventArgs e)
        {
            var List = QueryBatchImportOrderDataList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("ID号", typeof(int));
            table.Columns.Add("区域", typeof(string));
            table.Columns.Add("省份", typeof(string));
            table.Columns.Add("城市", typeof(string));
            table.Columns.Add("店代码", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("发票号码", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("数量", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            table.Columns.Add("出库单号", typeof(string));
            table.Columns.Add("出库状态", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("级别", typeof(string)); 
            table.Columns.Add("业务名称", typeof(string));
            table.Columns.Add("操作人员", typeof(string));
            table.Columns.Add("导单时间", typeof(string));
            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["ID号"] = it.GetType().GetProperty("TID").GetValue(it, null);
                newRows["区域"] = it.GetType().GetProperty("Region").GetValue(it, null).ToString();
                newRows["省份"] = it.GetType().GetProperty("Province").GetValue(it, null).ToString();
                newRows["城市"] = it.GetType().GetProperty("City").GetValue(it, null).ToString();
                newRows["店代码"] = it.GetType().GetProperty("SourceCode").GetValue(it, null).ToString();
                newRows["客户名称"] = it.GetType().GetProperty("SourceName").GetValue(it, null).ToString();
                newRows["发票号码"] = it.GetType().GetProperty("GtmcNo").GetValue(it, null).ToString();
                newRows["货品代码"] = it.GetType().GetProperty("GoodsCode").GetValue(it, null).ToString();
                newRows["数量"] = it.GetType().GetProperty("Piece").GetValue(it, null).ToString();
                newRows["订单状态"] = GetText(it.GetType().GetProperty("OrderStatus").GetValue(it, null).ToString(), "OrderStatus");
                newRows["出库单号"] = it.GetType().GetProperty("OrderNo").GetValue(it, null).ToString();
                newRows["出库状态"] = GetText(it.GetType().GetProperty("AwbStatus").GetValue(it, null).ToString(), "AwbStatus");
                newRows["开单时间"] = it.GetType().GetProperty("CreateDate").GetValue(it, null).ToString();
                newRows["品牌"] = it.GetType().GetProperty("TypeName").GetValue(it, null).ToString();
                newRows["规格"] = it.GetType().GetProperty("Specs").GetValue(it, null).ToString();
                newRows["花纹"] = it.GetType().GetProperty("Figure").GetValue(it, null).ToString();
                newRows["载速"] = it.GetType().GetProperty("LoadIndex").GetValue(it, null).ToString();
                newRows["级别"] = it.GetType().GetProperty("SpeedLevel").GetValue(it, null).ToString();
                newRows["业务名称"] = it.GetType().GetProperty("BusinessName").GetValue(it, null).ToString();
                newRows["操作人员"] = it.GetType().GetProperty("OPName").GetValue(it, null).ToString();
                newRows["导单时间"] = it.GetType().GetProperty("OPDATE").GetValue(it, null).ToString();
                table.Rows.Add(newRows);
                
            }
            ToExcel.DataTableToExcel(table, "", "导入订单列表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            switch (id)
            {
                case "AwbStatus":
                    if (value.Trim() == "0")
                        retStr = "未出库";
                    else if (value.Trim() == "1")
                        retStr = "出库中";
                    else if (value.Trim() == "2")
                        retStr = "已出库";
                    break;
                case "OrderStatus":
                    if (value.Trim() == "0")
                        retStr = "未生成";
                    else if (value.Trim() == "1")
                        retStr = "已生成";
                    break;
            }
            return retStr;
        }
    }
}