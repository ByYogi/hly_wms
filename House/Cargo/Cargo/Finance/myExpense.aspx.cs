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
    public partial class myExpense : BasePage
    {
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string HouseName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            HouseName = UserInfor.HouseName.Trim();
        }

        /// <summary>
        /// 导出实体
        /// </summary>
        public List<CargoExpenseEntity> Financelist
        {
            get
            {
                if (Session["Financelist"] == null)
                {
                    Session["Financelist"] = new List<CargoExpenseEntity>();
                }
                return (List<CargoExpenseEntity>)(Session["Financelist"]);
            }
            set
            {
                Session["Financelist"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (Financelist.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(decimal));
            table.Columns.Add("报销日期", typeof(string));
            table.Columns.Add("报销单号", typeof(string));
            table.Columns.Add("报销人", typeof(string));
            table.Columns.Add("受款人", typeof(string));
            table.Columns.Add("发生日期", typeof(string));
            table.Columns.Add("一级科目", typeof(string));
            table.Columns.Add("二级科目", typeof(string));
            table.Columns.Add("三级科目", typeof(string));
            table.Columns.Add("说明", typeof(string));
            table.Columns.Add("对应单号", typeof(string));
            table.Columns.Add("费用", typeof(string));
            //table.Columns.Add("报销类别", typeof(string));
            table.Columns.Add("付款方式", typeof(string));
            //table.Columns.Add("报销金额", typeof(decimal));
            table.Columns.Add("下一审批人", typeof(string));
            table.Columns.Add("当前审批人", typeof(string));
            table.Columns.Add("报销状态", typeof(string));
            table.Columns.Add("拒审原因", typeof(string));
            table.Columns.Add("结算状态", typeof(string));
            table.Columns.Add("操作人", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            int i = 0;
            foreach (var it in Financelist)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["报销日期"] = it.ExpenseDate.ToString("yyyy-MM-dd");
                newRows["报销单号"] = it.ExID.ToString();
                newRows["报销人"] = it.ExName.Trim();
                newRows["受款人"] = it.ReceiveName.Trim();
                newRows["发生日期"] = it.HappenDate.ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : it.HappenDate.ToString("yyyy-MM-dd");
                newRows["一级科目"] = GetText(it.ExType.Trim(), "ExType");
                newRows["二级科目"] = it.FName.Trim();
                newRows["三级科目"] = it.SName.Trim();
                newRows["说明"] = it.Memo.Trim();
                newRows["对应单号"] = it.Summary.Trim();
                newRows["费用"] = it.DetailCharge;
                // newRows["报销类别"] = it.CostName.Trim();
                newRows["付款方式"] = GetText(it.ChargeType.Trim(), "ChargeType");
                //newRows["报销金额"] = it.ExCharge;
                newRows["下一审批人"] = string.IsNullOrEmpty(it.NextCheckName.Trim()) ? it.OperaName.Trim() : it.NextCheckName;
                newRows["当前审批人"] = it.UserName;
                newRows["报销状态"] = GetText(it.Status.Trim(), "Status");
                newRows["拒审原因"] = it.DenyReason.Trim();
                newRows["结算状态"] = GetText(it.CheckStatus.Trim(), "Check");
                newRows["操作人"] = it.OperaName.Trim();
                newRows["客户名称"] = it.ClientName.Trim();
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "费用报销表");
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
            if (id.Contains("ChargeType"))
            {
                if (value.Trim() == "0")
                    retStr = "现金";
                else if (value.Trim() == "1")
                    retStr = "银行卡";
                else if (value.Trim() == "2")
                    retStr = "微信";
            }
            else if (id.Contains("Status"))
            {
                if (value.Trim() == "0")
                    retStr = "待批";
                else if (value.Trim() == "1")
                    retStr = "审批中";
                else if (value.Trim() == "2")
                    retStr = "拒审";
                else if (value.Trim() == "3")
                    retStr = "结束";
            }
            else if (id.Contains("Check"))
            {
                if (value.Trim() == "0")
                    retStr = "未结算";
                else if (value.Trim() == "1")
                    retStr = "已结清";
                else if (value.Trim() == "2")
                    retStr = "未结清";
            }
            else if (id.Contains("ExType"))
            {
                if (value.Trim() == "1")
                    retStr = "收入";
                else if (value.Trim() == "2")
                    retStr = "支出";
            }
            return retStr;
        }
    }
}