using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Client
{
    public partial class clientBillManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoClientAccountEntity> CargoClientAccountList
        {
            get
            {
                if (Session["CargoClientAccountList"] == null)
                {
                    Session["CargoClientAccountList"] = new List<CargoClientAccountEntity>();
                }
                return (List<CargoClientAccountEntity>)(Session["CargoClientAccountList"]);
            }
            set
            {
                Session["CargoClientAccountList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoClientAccountList.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("账单号", typeof(string));
            table.Columns.Add("账单名称", typeof(string));
            table.Columns.Add("创建日期", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("账单金额(元)", typeof(decimal));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("审核状态", typeof(string));
            table.Columns.Add("签字状态", typeof(string));
            table.Columns.Add("签字时间", typeof(string));
            table.Columns.Add("结算状态", typeof(string));
            int i = 0;
            foreach (var it in CargoClientAccountList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["所属仓库"] = it.HouseName;
                newRows["账单号"] = it.AccountID;
                newRows["账单名称"] = it.AccountTitle;
                newRows["创建日期"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["客户名称"] = it.ClientName;
                newRows["账单金额(元)"] = it.Total;
                newRows["备注"] = it.Memo;
                newRows["审核状态"] = GetText(it.Status, "Status");
                newRows["签字状态"] = GetText(it.ElecSign, "ElecSign");
                newRows["签字时间"] = it.ElecSign.Equals("1") ? it.ElecSignDate.ToString("yyyy-MM-dd HH:mm:ss") : "";
                newRows["结算状态"] = GetText(it.CheckStatus, "CheckStatus");

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "客户数据报表");

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
            if (id.Equals("Status"))
            {
                if (value.Trim() == "0")
                    retStr = "待审";
                else if (value.Trim() == "1")
                    retStr = "通过";
                else if (value.Trim() == "2")
                    retStr = "拒审";
                else if (value.Trim() == "3")
                    retStr = "结束";
            }
            else if (id.Equals("ElecSign"))
            {
                if (value.Trim() == "0")
                    retStr = "未签字";
                else if (value.Trim() == "1")
                    retStr = "已签字";
            }
            else if (id.Equals("CheckStatus"))
            {
                if (value.Trim() == "0")
                    retStr = "未结算";
                else if (value.Trim() == "1")
                    retStr = "已结清";
                else if (value.Trim() == "2")
                    retStr = "未结清";
            }
            return retStr;
        }
    }
}