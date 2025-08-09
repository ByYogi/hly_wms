using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Arrive
{
    public partial class AwbManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<AwbEntity> PageList
        {
            get
            {
                if (Session["PageList"] == null) { Session["PageList"] = new List<AwbEntity>(); }
                return (List<AwbEntity>)(Session["PageList"]);
            }
            set
            { Session["PageList"] = value; }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (PageList.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(decimal));
            table.Columns.Add("开单日期", typeof(DateTime));
            table.Columns.Add("运单号", typeof(string));
            table.Columns.Add("起点站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("发货单位", typeof(string));//
            table.Columns.Add("发货人", typeof(string));
            table.Columns.Add("发货联系方式", typeof(string));//
            table.Columns.Add("收货单位", typeof(string));//
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("收货联系方式", typeof(string));//
            table.Columns.Add("物品名称", typeof(string));
            table.Columns.Add("包装", typeof(string));
            table.Columns.Add("数量", typeof(decimal));
            table.Columns.Add("数量单价", typeof(decimal));
            table.Columns.Add("重量", typeof(decimal));
            table.Columns.Add("重量单价", typeof(decimal));
            table.Columns.Add("体积", typeof(decimal));
            table.Columns.Add("体积单价", typeof(decimal));
            table.Columns.Add("运输费", typeof(decimal));
            table.Columns.Add("提货费", typeof(decimal));
            table.Columns.Add("送货费", typeof(decimal));
            table.Columns.Add("合计", typeof(decimal));
            table.Columns.Add("代收款", typeof(decimal));
            table.Columns.Add("回扣", typeof(decimal));
            table.Columns.Add("保险费", typeof(decimal));
            table.Columns.Add("结款方式", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("录单员", typeof(string));
            table.Columns.Add("运单状态", typeof(string));

            int i = 0;
            foreach (var it in PageList)
            {
                i++;
                it.EnSafe();
                foreach (var gd in it.AwbGoods)
                {
                    DataRow newRows = table.NewRow();
                    newRows["序号"] = i;
                    newRows["开单日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                    newRows["运单号"] = it.AwbNo;
                    newRows["起点站"] = it.Dep.Trim();
                    newRows["到达站"] = it.Dest.Trim();
                    newRows["发货单位"] = it.ShipperUnit.Trim();
                    newRows["发货人"] = it.ShipperName.Trim();
                    newRows["发货联系方式"] = it.ShipperPhone.Trim();
                    newRows["收货单位"] = it.AcceptUnit.Trim();
                    newRows["收货人"] = it.AcceptPeople.Trim();
                    newRows["收货联系方式"] = it.AcceptPhone.Trim();
                    newRows["物品名称"] = gd.Goods.Trim();
                    newRows["包装"] = gd.Package.Trim();
                    newRows["数量"] = gd.Piece;
                    newRows["数量单价"] = gd.PiecePrice;
                    newRows["重量"] = gd.Weight;
                    newRows["重量单价"] = gd.WeightPrice;
                    newRows["体积"] = gd.Volume;
                    newRows["体积单价"] = gd.VolumePrice;
                    newRows["运输费"] = it.TransportFee;
                    newRows["提货费"] = it.OtherFee;
                    newRows["送货费"] = it.DeliverFee;
                    newRows["合计"] = it.TotalCharge;
                    newRows["代收款"] = it.CollectMoney;
                    newRows["回扣"] = it.Rebate;
                    newRows["保险费"] = it.InsuranceFee;
                    newRows["结款方式"] = GetText(it.CheckOutType, "CheckOutType");
                    newRows["备注"] = it.Remark.Trim();
                    newRows["录单员"] = it.CreateAwb.Trim();
                    newRows["运单状态"] = GetText(it.DelFlag, "DelFlag");
                    table.Rows.Add(newRows);
                }
            }
            ToExcel.DataTableToExcel(table, "", "托运单表");
        }

        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("CheckOutType"))
            {
                if (value.Trim() == "0")
                    retStr = "现付";
                else if (value.Trim() == "1")
                    retStr = "回单";
                else if (value.Trim() == "2")
                    retStr = "月结";
                else if (value.Trim() == "3")
                    retStr = "到付";
                else if (value.Trim() == "4")
                    retStr = "代收款";
            }
            else if (id.Contains("DelFlag"))
            {
                if (value.Trim() == "0")
                    retStr = "正常";
                else if (value.Trim() == "1")
                    retStr = "删除";
                else if (value.Trim() == "2")
                    retStr = "配载";
                else if (value.Trim() == "3")
                    retStr = "结束";
            }
            return retStr;
        }
    }
}