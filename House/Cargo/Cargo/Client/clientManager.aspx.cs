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
    public partial class clientManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<CargoClientEntity> CargoClientList
        {
            get
            {
                if (Session["CargoClientList"] == null)
                {
                    Session["CargoClientList"] = new List<CargoClientEntity>();
                }
                return (List<CargoClientEntity>)(Session["CargoClientList"]);
            }
            set
            {
                Session["CargoClientList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoClientList.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("客户编码", typeof(string));
            table.Columns.Add("公司名称", typeof(string));
            table.Columns.Add("公司简称", typeof(string));
            table.Columns.Add("客户类型", typeof(string));
            table.Columns.Add("联系人", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("公司电话", typeof(string));
            table.Columns.Add("所在省", typeof(string));
            table.Columns.Add("所在市", typeof(string));
            table.Columns.Add("所在区", typeof(string));
            table.Columns.Add("公司地址", typeof(string));
            table.Columns.Add("透支额度", typeof(decimal));
            table.Columns.Add("目标轮胎数", typeof(int));
            table.Columns.Add("预收款余额", typeof(decimal));
            table.Columns.Add("返利款余额", typeof(decimal));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("所属公司", typeof(string));
            table.Columns.Add("店代码", typeof(string));
            table.Columns.Add("业务员", typeof(string));
            table.Columns.Add("新增时间", typeof(string));
            int i = 0;
            foreach (var it in CargoClientList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["客户编码"] = it.ClientNum.ToString();
                newRows["公司名称"] = it.ClientName;
                newRows["公司简称"] = it.ClientShortName.Trim();
                newRows["客户类型"] = GetText(it.ClientType.Trim(), "ClientType");
                newRows["联系人"] = it.Boss.Trim();
                newRows["手机号码"] = it.Cellphone;
                newRows["公司电话"] = it.Telephone;
                newRows["所在省"] = it.Province;
                newRows["所在市"] = it.City;
                newRows["所在区"] = it.Country;
                newRows["公司地址"] = it.Address;
                newRows["透支额度"] = it.LimitMoney;
                newRows["目标轮胎数"] = it.TargetNum;
                newRows["预收款余额"] = it.PreReceiveMoney;
                newRows["返利款余额"] = it.RebateMoney;
                newRows["所属仓库"] = it.HouseName;
                newRows["所属公司"] = it.UpClientShortName;
                newRows["店代码"] = it.ShopCode;
                newRows["业务员"] = it.UserName;
                newRows["新增时间"] = it.AddData;
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
            if (id.Contains("ClientType"))
            {
                if (value.Trim() == "0")
                    retStr = "普通客户";
                else if (value.Trim() == "1")
                    retStr = "合同客户";
                else if (value.Trim() == "2")
                    retStr = "VIP客户";
            }
            return retStr;
        }
    }
}