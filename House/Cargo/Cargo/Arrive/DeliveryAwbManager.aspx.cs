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
    public partial class DeliveryAwbManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<DeliveryEntity> DeliveryList
        {
            get
            {
                if (Session["DeliveryList"] == null)
                {
                    Session["DeliveryList"] = new List<DeliveryEntity>();
                }
                return (List<DeliveryEntity>)(Session["DeliveryList"]);
            }
            set
            {
                Session["DeliveryList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (DeliveryList.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            //table.Columns.Add("序号", typeof(int));
            table.Columns.Add("配送合同号", typeof(string));
            table.Columns.Add("车牌号", typeof(string));
            table.Columns.Add("车长", typeof(decimal));
            table.Columns.Add("车型", typeof(string));
            table.Columns.Add("发车时间", typeof(string));
            table.Columns.Add("创建时间", typeof(string));
            table.Columns.Add("运费", typeof(decimal));
            table.Columns.Add("其它费用", typeof(decimal));
            table.Columns.Add("合计", typeof(decimal));
            table.Columns.Add("司机姓名", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("审核状态", typeof(string));
            table.Columns.Add("对应单号", typeof(string));
            table.Columns.Add("受理日期", typeof(string));
            table.Columns.Add("发货客户", typeof(string));
            table.Columns.Add("收货客户", typeof(string));
            table.Columns.Add("收货地址", typeof(string));
            table.Columns.Add("品名", typeof(string));
            table.Columns.Add("件数", typeof(int));
            table.Columns.Add("分批件数", typeof(int));
            table.Columns.Add("重量", typeof(decimal));
            table.Columns.Add("体积", typeof(decimal));
            table.Columns.Add("送货费", typeof(decimal));
            table.Columns.Add("运单总收入", typeof(decimal));
            table.Columns.Add("代收款", typeof(decimal));
            table.Columns.Add("备注", typeof(string));
            int i = 0;
            foreach (var it in DeliveryList)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                //newRows["序号"] = i;
                newRows["配送合同号"] = it.DeliveryNum.Trim();
                newRows["车牌号"] = it.TruckNum;
                newRows["车长"] = it.Length;
                newRows["车型"] = GetValue(it.Model.Trim(), "Model");
                newRows["发车时间"] = it.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["创建时间"] = it.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["运费"] = it.TransportFee;
                newRows["其它费用"] = it.OtherFee;
                newRows["合计"] = it.TransportFee + it.OtherFee;
                newRows["司机姓名"] = it.Driver.Trim();
                newRows["手机号码"] = it.DriverCellPhone.Trim();
                newRows["审核状态"] = GetValue(it.FinanceFirstCheck.Trim(), "FinanceFirstCheck");
                newRows["对应单号"] = it.AwbNo.Trim();
                newRows["受理日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                newRows["发货客户"] = it.ShipperUnit.Trim();
                newRows["收货客户"] = it.AcceptUnit.Trim();
                newRows["收货地址"] = it.AcceptAddress.Trim();
                newRows["品名"] = it.Goods.Trim();
                newRows["件数"] = it.Piece;
                newRows["分批件数"] = it.AwbPiece;
                newRows["重量"] = it.Weight;
                newRows["体积"] = it.Volume;
                newRows["送货费"] = it.DeliverFee;
                newRows["运单总收入"] = it.TotalCharge;
                newRows["代收款"] = it.CollectMoney;
                newRows["备注"] = it.Remark.Trim();
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "配送运单数据表");
        }

        private string GetValue(string value, string ty)
        {
            string result = string.Empty;
            if (ty.Contains("FinanceFirstCheck"))
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
            if (ty.Contains("Model"))
            {
                switch (value)
                {
                    case "0": result = "厢车"; break;
                    case "1": result = "高栏"; break;
                    case "2": result = "平板"; break;
                    case "3": result = "冷柜"; break;
                    case "4": result = "面包车"; break;
                    case "5": result = "微型车"; break;
                    default: result = ""; break;
                }
            }
            return result;
        }
    }
}