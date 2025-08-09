using House.Entity.Cargo;
using House.Entity.House;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo
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

                welcome.Text = "所属仓库：<a href='javascript:changeHouse();'>" + UserInfor.HouseName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;当前日期：" + Common.GetSystemDate() + "    欢迎您：" + UserInfor.UserName.Trim() + "，" + GetCurrentTime();
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
            List<SystemItemEntity> result = (List<SystemItemEntity>)Session["List"];
            res = "{basic:[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += "{'icon': '" + it.ItemIcon.Trim() + "', 'menuname': '" + it.CName.Trim() + "','menus':[";
                    List<SystemItemEntity> secList = result.FindAll(c => c.ParentID.Equals(it.ItemID));
                    foreach (var e in secList)
                    {
                        res += "{'url': '" + e.ItemSrc.ToString() + "', 'menuname': '" + e.CName.Trim() + "', 'icon': '" + e.ItemIcon.Trim() + "','menuid':'" + e.CName.Trim() + "'},";
                    }
                    res = res.Substring(0, res.Length - 1);
                    res += "]},";
                }
            }
            res = res.Substring(0, res.Length - 1);
            res += "]}";
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
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(string));
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
                newRows["产品编码"] = it.ProductCode;
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