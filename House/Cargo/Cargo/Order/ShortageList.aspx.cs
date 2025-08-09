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
    public partial class ShortageList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Hashtable QueryShortageListInfoList
        {
            get
            {
                if (Session["QueryShortageListInfoList"] == null)
                {
                    Session["QueryShortageListInfoList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryShortageListInfoList"]);
            }
            set
            {
                Session["QueryShortageListInfoList"] = value;
            }
        }
        /// <summary>
        /// 导出订单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShortageList_Click(object sender, EventArgs e)
        {
            var List = QueryShortageListInfoList["rows"];
            IEnumerable<object> list = List as IEnumerable<object>;
            if (list.Count() <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速度", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("缺货时间", typeof(string));

            int i = 0;
            string orderno = string.Empty;
            foreach (var it in list)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["产品名称"] = it.GetType().GetProperty("TypeName").GetValue(it, null).ToString();
                newRows["货品代码"] = it.GetType().GetProperty("GoodsCode").GetValue(it, null).ToString();
                newRows["规格"] = it.GetType().GetProperty("Specs").GetValue(it, null).ToString();
                newRows["花纹"] = it.GetType().GetProperty("Figure").GetValue(it, null).ToString();
                newRows["载重"] = it.GetType().GetProperty("LoadIndex").GetValue(it, null).ToString();
                newRows["速度"] = it.GetType().GetProperty("SpeedLevel").GetValue(it, null).ToString();
                newRows["数量"] = it.GetType().GetProperty("Piece").GetValue(it, null);
                newRows["客户名称"] = it.GetType().GetProperty("ClientName").GetValue(it, null).ToString();
                newRows["缺货时间"] = it.GetType().GetProperty("OP_DATE").GetValue(it, null).ToString() != "0001/1/1 0:00:00" ? it.GetType().GetProperty("OP_DATE").GetValue(it, null).ToString() : "";
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "缺货列表");
        }
    }
}