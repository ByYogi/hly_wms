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
    public partial class financeCashierMoneyManager : BasePage
    {

        /// <summary>
        /// 资金明细导出实体
        /// </summary>
        public List<CargoCashierEntity> incomePayList
        {
            get
            {
                if (Session["incomePayList"] == null)
                {
                    Session["incomePayList"] = new List<CargoCashierEntity>();
                }
                return (List<CargoCashierEntity>)(Session["incomePayList"]);
            }
            set
            {
                Session["incomePayList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (incomePayList.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("账户类型", typeof(string));
            table.Columns.Add("账户别名", typeof(string));
            table.Columns.Add("开户行", typeof(string));//
            table.Columns.Add("账号", typeof(string));
            table.Columns.Add("开户名", typeof(string));
            table.Columns.Add("收支", typeof(string));
            table.Columns.Add("收支来源", typeof(string));
            table.Columns.Add("来源单号", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("金额", typeof(string));
            table.Columns.Add("账户余额", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("操作时间", typeof(string));
            table.Columns.Add("操作人", typeof(string));

            int i = 0;
            foreach (var it in incomePayList)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["所属仓库"] = it.HouseName.Trim();
                newRows["账户类型"] = GetText(it.CardType.Trim(), "CardType");
                newRows["账户别名"] = it.Aliases;
                newRows["开户行"] = it.Bank;
                newRows["账号"] = it.CardNum;
                newRows["开户名"] = it.CardName;
                newRows["收支"] = GetText(it.RType.Trim(), "RType");
                newRows["收支来源"] = GetText(it.FromTO.Trim(), "FromTO");
                newRows["来源单号"] = it.AffectAwbNO.Trim();
                newRows["客户名称"] = it.AffectClient.Trim();
                newRows["金额"] = it.AffectCash.ToString();
                newRows["账户余额"] = it.OverMoney.ToString();
                newRows["备注"] = it.Memo.ToString();
                newRows["操作时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["操作人"] = it.OP_ID.Trim();
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "收支明细数据表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("CardType"))
            {
                if (value.Trim() == "0")
                    retStr = "现金";
                else if (value.Trim() == "1")
                    retStr = "银行卡";
                else if (value.Trim() == "2")
                    retStr = "微信";
                else if (value.Trim() == "3")
                    retStr = "支付宝";
            }
            else if (id.Contains("RType"))
            {
                if (value.Trim() == "0")
                    retStr = "收入";
                else if (value.Trim() == "1")
                    retStr = "支出";
            }
            else if (id.Contains("FromTO"))
            {
                if (value.Trim() == "0")
                    retStr = "按订单收款";
                else if (value.Trim() == "1")
                    retStr = "内部转账";
                else if (value.Trim() == "2")
                    retStr = "报销单收款";
                else if (value.Trim() == "3")
                    retStr = "报销单付款";
                else if (value.Trim() == "4")
                    retStr = "物流付款";
                else if (value.Trim() == "5")
                    retStr = "按账单收款";
                else if (value.Trim() == "6")
                    retStr = "进货单付款";
                else if (value.Trim() == "7")
                    retStr = "撤销收款";
                else if (value.Trim() == "8")
                    retStr = "撤销付款";
                else if (value.Trim() == "9")
                    retStr = "投资款";
                else if (value.Trim() == "10")
                    retStr = "借款";
                else if (value.Trim() == "11")
                    retStr = "代收快递";
                else if (value.Trim() == "12")
                    retStr = "代付快递";
                else if (value.Trim() == "13")
                    retStr = "其他业务收入";
                else if (value.Trim() == "14")
                    retStr = "其他业务支出";
                else if (value.Trim() == "15")
                    retStr = "短信";
                else if (value.Trim() == "16")
                    retStr = "还款";
                else if (value.Trim() == "17")
                    retStr = "押金";
                else if (value.Trim() == "18")
                    retStr = "其它收入";
                else if (value.Trim() == "19")
                    retStr = "其它支出";
            }
            return retStr;
        }
    }
}