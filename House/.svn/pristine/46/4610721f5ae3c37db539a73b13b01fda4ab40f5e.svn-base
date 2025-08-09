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
    public partial class TransitAwbManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<TransitEntity> TransitList
        {
            get
            {
                if (Session["TransitList"] == null)
                {
                    Session["TransitList"] = new List<TransitEntity>();
                }
                return (List<TransitEntity>)(Session["TransitList"]);
            }
            set
            {
                Session["TransitList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (TransitList.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            //table.Columns.Add("序号", typeof(int));
            table.Columns.Add("中转单号", typeof(string));
            table.Columns.Add("外协单号", typeof(string));
            table.Columns.Add("承运公司", typeof(string));
            table.Columns.Add("中转时间", typeof(string));
            table.Columns.Add("付款方式", typeof(string));
            table.Columns.Add("联系人", typeof(string));
            table.Columns.Add("联系电话", typeof(string));
            //table.Columns.Add("中转件数", typeof(int));
            //table.Columns.Add("件数单价", typeof(decimal));
            //table.Columns.Add("中转重量", typeof(decimal));
            //table.Columns.Add("重量单价", typeof(decimal));
            //table.Columns.Add("中转体积", typeof(decimal));
            //table.Columns.Add("体积单价", typeof(decimal));
            table.Columns.Add("运输费用", typeof(decimal));
            table.Columns.Add("其它费用", typeof(decimal));
            table.Columns.Add("提货费", typeof(decimal));
            table.Columns.Add("送货费", typeof(decimal));
            table.Columns.Add("代收款", typeof(decimal));
            table.Columns.Add("装卸费", typeof(decimal));
            table.Columns.Add("合计", typeof(decimal));
            table.Columns.Add("审核状态", typeof(string));
            table.Columns.Add("对应单号", typeof(string));
            table.Columns.Add("出发站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("中转站", typeof(string));
            table.Columns.Add("受理日期", typeof(string));
            table.Columns.Add("发货客户", typeof(string));
            table.Columns.Add("收货客户", typeof(string));
            table.Columns.Add("运单件数", typeof(int));
            table.Columns.Add("分批件数", typeof(int));
            table.Columns.Add("运单重量", typeof(decimal));
            table.Columns.Add("运单体积", typeof(decimal));
            //table.Columns.Add("到付运费", typeof(decimal));
            table.Columns.Add("运单代收款", typeof(decimal));
            table.Columns.Add("总收入", typeof(decimal));
            table.Columns.Add("备注", typeof(string));
            int i = 0;
            foreach (var it in TransitList)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                //newRows["序号"] = i;
                newRows["中转单号"] = it.TransitID.ToString();
                newRows["外协单号"] = it.AssistNum.Trim();
                newRows["承运公司"] = it.CarrierShortName;
                newRows["中转时间"] = it.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["付款方式"] = GetValue(it.CheckOutType.Trim(), "CheckOutType");
                newRows["联系人"] = it.Boss.Trim();
                newRows["联系电话"] = it.Telephone.Trim();
                //newRows["中转件数"] = it.Piece;
                //newRows["件数单价"] = it.PiecePrice;
                //newRows["中转重量"] = it.Weight;
                //newRows["重量单价"] = it.WeightPrice;
                //newRows["中转体积"] = it.Volume;
                //newRows["体积单价"] = it.VolumePrice;
                newRows["运输费用"] = it.TransportFee;
                newRows["其它费用"] = it.OtherFee;
                newRows["提货费"] = it.DeliveryFee;
                newRows["送货费"] = it.SendFee;
                newRows["代收款"] = it.CollectMoney;
                newRows["装卸费"] = it.HandFee;
                newRows["合计"] = it.TransportFee + it.OtherFee + it.DeliveryFee + it.SendFee + it.HandFee;
                newRows["审核状态"] = GetValue(it.FinanceFirstCheck.Trim(), "FinanceFirstCheck");
                newRows["对应单号"] = it.AwbNo.Trim();
                newRows["出发站"] = it.Dep.Trim();
                newRows["到达站"] = it.Dest.Trim();
                newRows["中转站"] = it.Transit.Trim();
                newRows["受理日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                newRows["发货客户"] = it.ShipperUnit.Trim();
                newRows["收货客户"] = it.AcceptUnit.Trim();
                newRows["运单件数"] = it.AwbPiece;
                newRows["分批件数"] = it.FenPiPiece;
                newRows["运单重量"] = it.AwbWeight;
                newRows["运单体积"] = it.AwbVolume;
                // newRows["到付运费"] = it.TotalCharge;
                newRows["运单代收款"] = it.PrepayFee;
                newRows["总收入"] = it.TotalCharge;
                newRows["备注"] = it.Remark.Trim();
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "中转运单数据表");
        }

        private string GetValue(string value, string id)
        {
            string result = string.Empty;
            if (id.Contains("FinanceFirstCheck"))
            {
                switch (value)
                {
                    case "0":
                        result = "待审";
                        break;
                    case "1":
                        result = "已审";
                        break;
                    default:
                        result = "待审";
                        break;
                }
            }
            else if (id.Contains("CheckOutType"))
            {
                if (value.Trim() == "0")
                    result = "现付";
                else if (value.Trim() == "1")
                    result = "回单";
                else if (value.Trim() == "2")
                    result = "月结";
                else if (value.Trim() == "3")
                    result = "到付";
                else if (value.Trim() == "4")
                    result = "代收款";
            }
            return result;
        }
    }
}