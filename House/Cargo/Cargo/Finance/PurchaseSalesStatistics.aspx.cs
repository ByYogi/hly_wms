using House.Entity.Cargo;
using NPOI.HSSF.Record.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Finance
{
    public partial class PurchaseSalesStatistics : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryPurSelFeeDataList
        {
            get
            {
                if (Session["QueryPurSelFeeDataList"] == null)
                {
                    Session["QueryPurSelFeeDataList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryPurSelFeeDataList"]);
            }
            set
            {
                Session["QueryPurSelFeeDataList"] = value;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            var List = QueryPurSelFeeDataList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            list = list.OrderByDescending(c => (Convert.ToDecimal(c.GetType().GetProperty("SelFee").GetValue(c, null)) - Convert.ToDecimal(c.GetType().GetProperty("SelAffectTotal").GetValue(c, null))) - (Convert.ToDecimal(c.GetType().GetProperty("PurFee").GetValue(c, null)) - Convert.ToDecimal(c.GetType().GetProperty("PurAffectTotal").GetValue(c, null)))).ToList();
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("联系人", typeof(string));
            table.Columns.Add("电话", typeof(string));
            table.Columns.Add("业务员", typeof(string));
            table.Columns.Add("销售合计", typeof(int));
            table.Columns.Add("已收款", typeof(decimal));
            table.Columns.Add("采购合计", typeof(decimal));
            table.Columns.Add("已付款", typeof(decimal));
            table.Columns.Add("抵扣剩余", typeof(decimal));
            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["客户名称"] = it.GetType().GetProperty("ClientName").GetValue(it, null).ToString();
                newRows["联系人"] = it.GetType().GetProperty("Boss").GetValue(it, null).ToString();
                newRows["电话"] = it.GetType().GetProperty("Cellphone").GetValue(it, null).ToString();
                newRows["业务员"] = it.GetType().GetProperty("UserName").GetValue(it, null).ToString();
                newRows["销售合计"] = it.GetType().GetProperty("SelFee").GetValue(it, null);
                newRows["已收款"] = it.GetType().GetProperty("SelAffectTotal").GetValue(it, null);
                newRows["采购合计"] = it.GetType().GetProperty("PurFee").GetValue(it, null);
                newRows["已付款"] = it.GetType().GetProperty("PurAffectTotal").GetValue(it, null);
                newRows["抵扣剩余"] = (Convert.ToDecimal(it.GetType().GetProperty("SelFee").GetValue(it, null)) - Convert.ToDecimal(it.GetType().GetProperty("SelAffectTotal").GetValue(it, null))) - (Convert.ToDecimal(it.GetType().GetProperty("PurFee").GetValue(it, null)) - Convert.ToDecimal(it.GetType().GetProperty("PurAffectTotal").GetValue(it, null)));
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "应收应付统计表");
        }
    }
}