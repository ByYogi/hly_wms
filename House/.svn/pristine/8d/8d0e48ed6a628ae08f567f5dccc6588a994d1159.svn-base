using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity.House;
using NPOI.HSSF.Record.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier
{
    public partial class Main : BasePage
    {
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string ArgoBarPrintName = string.Empty;
        public string CityCode = string.Empty;
        public string SystemName = string.Empty;
        public string res = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (UserInfor == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (UserInfor.UserName == null || UserInfor.LoginName == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (!IsPostBack)
            {
                welcome.Text = "所属仓库：<a href='javascript:changeHouse();'>" + UserInfor.SettleHouseName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎您：" + UserInfor.UserName.Trim();
            }
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            ArgoBarPrintName = "";
            QueryItemByLoginName();
        }

        /// <summary>
        /// 按登陆名查询所有导航链接并格式化
        /// </summary>
        public void QueryItemByLoginName()
        {
           res += "{'basic':[{'icon':'icon-order_Manager','menuname':'订单管理','menus':[{'url':'Order/ToDayOrders.aspx','menuname':'即日达订单','icon':'icon-page_lightning','menuid':'即日达订单'},{'url':'Order/NextDayOrders.aspx','menuname':'次日达订单','icon':'icon-page_red','menuid':'次日达订单'},{'url':'Order/ChannelOrders.aspx','menuname':'渠道订单','icon':'icon-page_gear','menuid':'渠道订单'}/*,{'url':'clientManager.aspx','menuname':'即日达退货单','icon':'icon-client','menuid':'即日达退货单'}*//*,{'url':'Order/NextDayReturnOrder.aspx','menuname':'次日达退货单','icon':'icon-printer','menuid':'次日达退货单'}*//*,{'url':'ShortageList.aspx','menuname':'支付记录','icon':'icon-page_lightning','menuid':'支付记录'}*/]},{'icon':'icon-house','menuname':'仓库管理','menus':[{'url':'House/PurchaseOrder.aspx','menuname':'开进仓单','icon':'icon-out_cargo','menuid':'开进仓单'},{'url':'House/PurchaseOrderManager.aspx','menuname':'进仓单管理','icon':'icon-out_cargo','menuid':'进仓单管理'},{'url':'House/RealTimeStock.aspx','menuname':'实时库存','icon':'icon-page_lightning','menuid':'实时库存'},{'url':'House/NextDayStock.aspx','menuname':'次日达库存','icon':'icon-page_gear','menuid':'次日达库存'},{'url':'House/HouseReturnOrder.aspx','menuname':'仓库退货单','icon':'icon-table_row_delete','menuid':'仓库退货单'}/*,{'url':'InvoiceManager.aspx','menuname':'进仓拒收订单','icon':'icon-printer','menuid':'进仓拒收订单'}*/]},{'icon':'icon-wrench_orange','menuname':'基础管理','menus':[{'url':'Basic/ProductInformation.aspx','menuname':'进仓价格管理','icon':'icon-order_returnManager','menuid':'进仓价格管理'},{'url':'Basic/ChannelCustomers.aspx','menuname':'渠道客户管理','icon':'icon-client','menuid':'渠道客户管理'},{'url':'Basic/priceRuleBank.aspx','menuname':'促销规则管理','icon':'icon-page_gear','menuid':'促销规则管理'}]},{'icon':'icon-chart_bar','menuname':'报表统计','menus':[{'url':'Report/OrderSaleDetails.aspx','menuname':'订单销售明细','icon':'icon-money','menuid':'订单销售明细'},{'url':'Report/OrderSale.aspx','menuname':'订单销售','icon':'icon-money','menuid':'订单销售'},{'url':'Report/PurchaseOrderDetails.aspx','menuname':'进仓单明细','icon':'icon-page_gear','menuid':'进仓单明细'},{'url':'Report/HouseReturnDetails.aspx','menuname':'仓库退货明细','icon':'icon-client','menuid':'仓库退货明细'}/*,{'url':'Report/ToDayReturnDetails.aspx','menuname':'即日达退货明细','icon':'icon-printer','menuid':'即日达退货明细'}*//*,{'url':'Report/NextDayReturnDetails.aspx','menuname':'次日达退货明细','icon':'icon-page_lightning','menuid':'次日达退货明细'}*/,{'url':'Report/AccessingStockDetails.aspx','menuname':'访问备货明细','icon':'icon-page_lightning','menuid':'访问备货明细'},{'url':'Report/SaleStockUpDetails.aspx','menuname':'销售备货明细','icon':'icon-page_lightning','menuid':'销售备货明细'},{'url':'Report/SaleDayReport.aspx','menuname':'销售统计分析','icon':'icon-color_swatch','menuid':'销售统计分析'}/*,{'url':'ShortageList.aspx','menuname':'库存决策表','icon':'icon-page_lightning','menuid':'库存决策表'}*/]}]}";
        }
        /// <summary>
        /// 返回时间欢迎语
        /// </summary>
        /// <returns></returns>
        private string GetCurrentTime()
        {
            int currentHour = Convert.ToInt32(DateTime.Now.ToString("HH"));//取24小时制的当前小时数
            if (currentHour < 6 && currentHour >= 0)
            {
                return "早上好！";
            }
            if (currentHour < 11 && 6 <= currentHour)
            {
                return "上午好！";
            }
            if (11 <= currentHour && currentHour < 13)
            {
                return "中午好！";
            }
            if (13 <= currentHour && currentHour < 17)
            {
                return "下午好！";
            }
            if (17 <= currentHour && currentHour < 23)
            {
                return "晚上好！";
            }
            return "上午好！";
        }
    }
}