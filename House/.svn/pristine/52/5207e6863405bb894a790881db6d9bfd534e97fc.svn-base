using House.Entity.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HLYEagle
{
    public partial class Main : BasePage
    {
        public string res = string.Empty;
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string ArgoBarPrintName = string.Empty;
        public string CityCode = string.Empty;
        public string SystemName = string.Empty;
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
                welcome.Text = "当前日期：" + Common.GetSystemDate() + "    欢迎您：" + UserInfor.UserName.Trim() + "，" + GetCurrentTime();
            }
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            ArgoBarPrintName = "";
            QueryItemByLoginName();
        }
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