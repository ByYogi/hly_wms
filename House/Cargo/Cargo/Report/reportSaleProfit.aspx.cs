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
    public partial class reportSaleProfit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoOrderEntity> QuerySaleProfitList
        {
            get
            {
                if (Session["QuerySaleProfitList"] == null)
                {
                    Session["QuerySaleProfitList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["QuerySaleProfitList"]);
            }
            set
            {
                Session["QuerySaleProfitList"] = value;
            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            if (QuerySaleProfitList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("类型", typeof(string));
            table.Columns.Add("项目", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("金额", typeof(decimal));
            int i = 0;
            foreach (var it in QuerySaleProfitList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["类型"] = it.OrderType;
                newRows["项目"] = it.TypeName;
                newRows["数量"] = it.Piece;
                newRows["金额"] = it.ActSalePrice;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "销售利润表");
        }
    }
}