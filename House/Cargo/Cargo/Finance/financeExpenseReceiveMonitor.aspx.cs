using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Finance
{
    public partial class financeExpenseReceiveMonitor : BasePage
    {
        public List<CargoExpenseEntity> expenseReceiveMonitor
        {
            get
            {
                if (Session["expenseReceiveMonitor"] == null)
                {
                    Session["expenseReceiveMonitor"] = new List<CargoExpenseEntity>();
                }
                return (List<CargoExpenseEntity>)(Session["expenseReceiveMonitor"]);
            }
            set
            {
                Session["expenseReceiveMonitor"] = value;
            }
        }
        public string Un = string.Empty;
        public string Ln = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (expenseReceiveMonitor.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("报销单号", typeof(string));
            table.Columns.Add("报销人", typeof(string));
            table.Columns.Add("报销时间", typeof(string));
            table.Columns.Add("客户名称", typeof(string));//
            table.Columns.Add("受款人", typeof(string));
            table.Columns.Add("受款账号", typeof(string));
            table.Columns.Add("付款方式", typeof(string));
            table.Columns.Add("报销类别", typeof(string));
            table.Columns.Add("报销金额", typeof(decimal));
            table.Columns.Add("审批状态", typeof(string));
            table.Columns.Add("上一审批人", typeof(string));
            table.Columns.Add("上一审批时间", typeof(string));
            table.Columns.Add("应收", typeof(decimal));
            table.Columns.Add("已收", typeof(decimal));
            table.Columns.Add("未收", typeof(decimal));
            table.Columns.Add("操作人", typeof(string));
            table.Columns.Add("收款时间", typeof(string));
            table.Columns.Add("结算状态", typeof(string));
            int i = 0;
            foreach (var it in expenseReceiveMonitor)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["报销单号"] = it.ExID;
                newRows["报销人"] = it.ExName.Trim();
                newRows["报销时间"] = it.ExDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["客户名称"] = it.ClientName.Trim();
                newRows["受款人"] = it.ReceiveName.Trim();
                newRows["受款账号"] = it.ReceiveNumber.Trim();
                newRows["付款方式"] = GetText(it.ChargeType.Trim(), "ChargeType");
                newRows["报销类别"] = it.CostName.Trim();
                newRows["报销金额"] = it.ExCharge;
                newRows["审批状态"] = GetText(it.Status.Trim(), "SPStatus");
                newRows["上一审批人"] = it.UserName.Trim();
                newRows["上一审批时间"] = it.CheckTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.CheckTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.CheckTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["应收"] = it.ExCharge;
                newRows["已收"] = it.ReceivedMoney;
                newRows["未收"] = it.UncollectMoney;
                newRows["操作人"] = it.LastUserName.Trim();
                newRows["收款时间"] = it.OP_DATE.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.OP_DATE.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.OP_DATE.ToString("yyyy-MM-dd HH:mm");
                newRows["结算状态"] = GetText(it.CheckStatus.Trim(), "CheckStatus");
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "按报销单收款监督数据表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("ChargeType"))
            {
                if (value.Trim() == "0")
                    retStr = "现金";
                else if (value.Trim() == "1")
                    retStr = "银行卡";
            }
            else if (id.Contains("SPStatus"))
            {
                if (value.Trim() == "0")
                    retStr = "待审";
                else if (value.Trim() == "1")
                    retStr = "主管已审批";
                else if (value.Trim() == "2")
                    retStr = "拒审";
                else if (value.Trim() == "3")
                    retStr = "结束";
                else if (value.Trim() == "4")
                    retStr = "财务已审批";
                else if (value.Trim() == "5")
                    retStr = "领导已审批";
            }
            else if (id.Contains("CheckStatus"))
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