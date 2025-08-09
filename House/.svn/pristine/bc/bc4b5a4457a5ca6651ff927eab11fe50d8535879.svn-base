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
    public partial class reportMoveContainer : BasePage
    {
        public List<CargoReportMoveContainerEntity> MoveContainerReport
        {
            get
            {
                if (Session["MoveContainerReport"] == null)
                {
                    Session["MoveContainerReport"] = new List<CargoReportMoveContainerEntity>();
                }
                return (List<CargoReportMoveContainerEntity>)(Session["MoveContainerReport"]);
            }
            set
            {
                Session["MoveContainerReport"] = value;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (MoveContainerReport.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("产品ID", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("型号", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("成本价", typeof(decimal));
            table.Columns.Add("原货位代码", typeof(string));
            table.Columns.Add("原货位区域", typeof(string));
            table.Columns.Add("原所在仓库", typeof(string));
            table.Columns.Add("移库数量", typeof(int));
            table.Columns.Add("新货位代码", typeof(string));
            table.Columns.Add("新货位区域", typeof(string));
            table.Columns.Add("新仓库", typeof(string));
            table.Columns.Add("移库状态", typeof(string));
            table.Columns.Add("操作人", typeof(string));
            table.Columns.Add("移库时间", typeof(string));
            int i = 0;
            foreach (var it in MoveContainerReport)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["产品编码"] = it.ProductCode.Trim();
                newRows["产品名称"] = it.ProductName.Trim();
                newRows["产品ID"] = it.ProductID;
                newRows["规格"] = it.Specs.Trim();
                newRows["花纹"] = it.Figure.Trim();
                newRows["型号"] = it.Model.Trim();
                newRows["批次"] = it.Batch;
                newRows["成本价"] = it.CostPrice;
                newRows["原货位代码"] = it.OldCCode.Trim();
                newRows["原货位区域"] = it.OldAreaName.Trim();
                newRows["原所在仓库"] = it.OldHouseName.Trim();
                newRows["移库数量"] = it.MoveNum;
                newRows["新货位代码"] = it.NewCCode.Trim();
                newRows["新货位区域"] = it.NewAreaName.Trim();
                newRows["新仓库"] = it.NewHouseName.Trim();
                newRows["移库状态"] = GetText(it.MoveStatus.Trim(), "MoveStatus");
                newRows["操作人"] = it.UserName.Trim();
                newRows["移库时间"] = it.OP_DATE.ToString("yyyy-MM-dd") == "0001-01-01" || it.OP_DATE.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : it.OP_DATE.ToString("yyyy-MM-dd");
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "每日移库报表");

        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("MoveStatus"))
            {
                if (value.Trim() == "1")
                    retStr = "全部移";
                else if (value.Trim() == "2")
                    retStr = "部分移";
            }
            return retStr;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}