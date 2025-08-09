using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.systempage
{
    public partial class FeedBack : BasePage
    {
        public List<CargoFeedBackEntity> CargoFeedBackExportEntity
        {
            get
            {
                if (Session["CargoFeedBackExportEntity"] == null) { Session["CargoFeedBackExportEntity"] = new List<CargoFeedBackEntity>(); }
                return (List<CargoFeedBackEntity>)(Session["CargoFeedBackExportEntity"]);
            }
            set
            {
                Session["CargoFeedBackExportEntity"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnShopOrder_Click(object sender, EventArgs e)
        {
            if (CargoFeedBackExportEntity.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("门店名称", typeof(string));
            table.Columns.Add("客户姓名", typeof(string));
            table.Columns.Add("回访人", typeof(string));
            table.Columns.Add("回访时间", typeof(string));
            table.Columns.Add("结果类型", typeof(string));
            table.Columns.Add("回访结果", typeof(string));
            table.Columns.Add("操作时间", typeof(string));
            List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
            int i = 0;
            foreach (var it in CargoFeedBackExportEntity)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["所属仓库"] = it.HouseName;
                newRows["门店名称"] = it.CompanyName;
                newRows["客户姓名"] = it.Name;
                newRows["回访人"] = it.FeedBackName;

                newRows["回访时间"] = it.FeedBackTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["结果类型"] = GetText(it.ResultType, "ResultType");
                newRows["回访结果"] = it.FeedBackResult;
                newRows["操作时间"] = it.OPDATE.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "慧采云仓客户回访数据导出表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("ResultType"))
            {
                if (value.Trim() == "2")
                    retStr = "无库存";
                else if (value.Trim() == "1")
                    retStr = "成功下单";
                else if (value.Trim() == "3")
                    retStr = "价格高";
                else if (value.Trim() == "4")
                    retStr = "配送时效";
                else if (value.Trim() == "5")
                    retStr = "周期年份问题";
                else if (value.Trim() == "6")
                    retStr = "跟车主没达成交易";
                else if (value.Trim() == "7")
                    retStr = "了解价格";
                else if (value.Trim() == "8")
                    retStr = "选择其他品牌";
            }
            return retStr;
        }

    }
}