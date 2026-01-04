using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class CreateTMallOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 库存数据导出实体
        /// </summary>
        public List<operateNoticeResultPo> StatisData
        {
            get
            {
                if (Session["operateNoticeResultPo"] == null)
                {
                    Session["operateNoticeResultPo"] = new List<operateNoticeResultPo>();
                }
                return (List<operateNoticeResultPo>)(Session["operateNoticeResultPo"]);
            }
            set
            {
                Session["operateNoticeResultPo"] = value;
            }
        }
        public string IsModifyInNum = string.Empty;
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (StatisData.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("开单日期", typeof(string));
            table.Columns.Add("智能系统订单号", typeof(string));
            table.Columns.Add("销售单号", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格花纹", typeof(string));
            table.Columns.Add("发货数量", typeof(int));
            table.Columns.Add("收件人", typeof(string));
            table.Columns.Add("收件人联系方式", typeof(string));
            table.Columns.Add("收货地址", typeof(string));
            table.Columns.Add("省", typeof(string));
            table.Columns.Add("市", typeof(string));
            table.Columns.Add("区", typeof(string));
            int i = 0;
            foreach (var it in StatisData)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["开单日期"] = it.noticeCreateTime_Actual.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["智能系统订单号"] = it.CargoOrderNo;
                newRows["销售单号"] = it.originBillNo;
                newRows["货品代码"] = it.skuOrderCode;
                newRows["规格花纹"] = it.skuName;
                newRows["发货数量"] = it.needOutboundNum;
                newRows["收件人"] = it.consigneeContacts;
                newRows["收件人联系方式"] = it.consigneePhone;
                newRows["收货地址"] =$@"{it.consigneeProvinceName}{it.consigneeCityName}{it.consigneeCountyName}{it.consigneeDetail}" ;
                newRows["省"] = it.consigneeProvinceName;
                newRows["市"] = it.consigneeCityName;
                newRows["区"] = it.consigneeCountyName;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", $@"天猫订单导出-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}");
            StatisData = new List<operateNoticeResultPo>();
        }
    }

}