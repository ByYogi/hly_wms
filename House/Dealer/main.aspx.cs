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

namespace Dealer
{
    public partial class main : BasePage
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
                if (UserInfor.HouseID == 82)
                {
                    CargoClientBus client = new CargoClientBus();
                    DateTime dt = DateTime.Now;
                    //DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
                    //DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末
                    CargoClientEntity clientEnt = client.QueryCargoClientTarget(new CargoClientEntity { ClientNum = Convert.ToInt32(UserInfor.LoginName), StartDate = dt.AddDays(1 - dt.Day), EndDate = dt.AddDays(1 - dt.Day).AddMonths(1).AddDays(-1) });
                    welcome.Text = "月度目标：" + clientEnt.TargetNum + "，完成数：" + clientEnt.SumPiece + "，达成率：" + (clientEnt.SumPiece == 0 || clientEnt.TargetNum == 0 ? "0%" : (Convert.ToDouble(clientEnt.SumPiece) / Convert.ToDouble(clientEnt.TargetNum)).ToString("P")) + "，特价额度：" + decimal.Truncate(clientEnt.LimitMoney) + "，返利额度：" + clientEnt.RebateMoney + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎您：" + UserInfor.UserName.Trim();
                }
                else
                {
                    welcome.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎您：" + UserInfor.UserName.Trim();
                }
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
            if (UserInfor.HouseID == 82)
            {
                res += "{basic:[{'icon': 'icon-house', 'menuname': '销售管理','menus':[{'url': 'OuthouseManager.aspx', 'menuname': '库存查询', 'icon': 'icon-out_cargo','menuid':'库存查询'},{'url': 'addSaleOrder.aspx', 'menuname': '新增订单', 'icon': 'icon-page_lightning','menuid':'新增订单'},{'url': 'OrderManager.aspx', 'menuname': '订单管理', 'icon': 'icon-page_gear','menuid':'订单管理'},{'url': 'clientManager.aspx', 'menuname': '地址管理', 'icon': 'icon-client','menuid':'地址管理'},{'url': 'InvoiceManager.aspx', 'menuname': '开票管理', 'icon': 'icon-printer','menuid':'开票管理'},{'url': 'ShortageList.aspx', 'menuname': '缺货列表', 'icon': 'icon-page_lightning','menuid':'缺货列表'}]}]}";
            }
            else if (UserInfor.HouseID == 10)
            {
                res += "{basic:[{'icon': 'icon-house', 'menuname': '销售管理','menus':[{'url': 'Antyres/OuthouseManager.aspx', 'menuname': '库存查询', 'icon': 'icon-out_cargo','menuid':'库存查询'},{'url': 'Antyres/addSaleOrder.aspx', 'menuname': '新增订单', 'icon': 'icon-page_lightning','menuid':'新增订单'},{'url': 'Antyres/OrderManager.aspx', 'menuname': '订单管理', 'icon': 'icon-page_gear','menuid':'订单管理'},{'url': 'Antyres/clientManager.aspx', 'menuname': '地址管理', 'icon': 'icon-client','menuid':'地址管理'},{'url': 'Antyres/addReturnOrder.aspx', 'menuname': '新增退货', 'icon': 'icon-order_return','menuid':'新增退货'},{'url': 'Antyres/orderReturnManager.aspx', 'menuname': '退货管理', 'icon': 'icon-order_returnManager','menuid':'退货管理'}]}]}";
            }
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
        public List<CargoSafeStockEntity> CargoSafeStockData
        {
            get
            {
                if (Session["CargoSafeStockData"] == null)
                {
                    Session["CargoSafeStockData"] = new List<CargoSafeStockEntity>();
                }
                return (List<CargoSafeStockEntity>)(Session["CargoSafeStockData"]);
            }
            set
            {
                Session["CargoSafeStockData"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoSafeStockData.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("区域大仓", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(int));
            table.Columns.Add("速级", typeof(string));
            table.Columns.Add("告警库存", typeof(int));
            table.Columns.Add("当前库存", typeof(int));
            table.Columns.Add("相差数量", typeof(int));
            int i = 0;
            foreach (var it in CargoSafeStockData)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["区域大仓"] = it.HouseName;
                newRows["所属仓库"] = it.AreaName;
                newRows["品牌"] = it.TypeName;
                newRows["货品代码"] = it.GoodsCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载重"] = it.LoadIndex;
                newRows["速级"] = it.SpeedLevel;
                newRows["告警库存"] = it.StockNum;
                newRows["当前库存"] = it.CurNum;
                newRows["相差数量"] = it.LessNum;
                table.Rows.Add(newRows);
            }
            string tName = CargoSafeStockData[0].AreaName + "库存告警缺货数据表";
            if (CargoSafeStockData[0].HouseName.Equals("广州仓库"))
            {
                tName = "广州仓库库存告警缺货数据表";
            }
            ToExcel.DataTableToExcel(table, "", tName);

        }
    }
}