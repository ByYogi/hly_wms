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
    public partial class RealPurchaseBillManager : BasePage
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
        public List<CargoSuppClientAccountEntity> CargoSuppClientAccountList
        {
            get
            {
                if (Session["CargoSuppClientAccountList"] == null)
                {
                    Session["CargoSuppClientAccountList"] = new List<CargoSuppClientAccountEntity>();
                }
                return (List<CargoSuppClientAccountEntity>)(Session["CargoSuppClientAccountList"]);
            }
            set
            {
                Session["CargoSuppClientAccountList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoSuppClientAccountList.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("账单号", typeof(string));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("账单名称", typeof(string));
            table.Columns.Add("供应商名称", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("金额", typeof(decimal));
            table.Columns.Add("优惠卷金额", typeof(decimal));
            table.Columns.Add("配送费", typeof(decimal));
            table.Columns.Add("物流费用", typeof(decimal));
            table.Columns.Add("仓库超期费", typeof(decimal));
            table.Columns.Add("出仓费", typeof(decimal));
            table.Columns.Add("退仓费", typeof(decimal));
            table.Columns.Add("平台费", typeof(decimal));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("其他费用", typeof(decimal));
            table.Columns.Add("创建日期", typeof(DateTime));
            int i = 0;
            foreach (var it in CargoSuppClientAccountList)
            {
                foreach (var item in it.SuppBillGoods)
                {
                    i++;
                    DataRow newRows = table.NewRow();
                    newRows["序号"] = i;
                    newRows["账单号"] = it.AccountNO;
                    newRows["订单号"] = item.OrderNo;
                    newRows["账单名称"] = it.AccountTitle;
                    newRows["供应商名称"] = it.ClientName;
                    newRows["所属仓库"] = it.HouseName;
                    newRows["金额"] = (item.OrderModel == null ? item.Total : (item.OrderModel == "0" ? item.Total : item.Total * -1));
                    newRows["优惠卷金额"] = item.InsuranceFee;
                    newRows["配送费"] = item.TransitFee;
                    newRows["物流费用"] = item.DeliveryFee;
                    newRows["仓库超期费"] = item.OverDueFee;
                    newRows["出仓费"] = item.OutStorageFee;
                    newRows["退仓费"] = item.StoReleaseFee;
                    newRows["平台费"] = item.OtherFee;
                    newRows["订单类型"] = item.OrderModel == null ? "" : (item.OrderModel == "0" ? "客户单" : "退货单");
                    newRows["其他费用"] = item.OtherExpensesFee;
                    newRows["创建日期"] = it.CreateDate;

                    table.Rows.Add(newRows);
                }
            }
            ToExcel.DataTableToExcel(table, "", "分账账单数据报表");

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