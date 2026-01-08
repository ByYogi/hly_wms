using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Cargo
{
    /// <summary>
    /// 企业号的公共界面母类
    /// </summary>
    public class QYBasePage : System.Web.UI.Page
    {
        public static void WriteTextLog(string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
        protected override void OnLoad(EventArgs e)
        {
            #if DEBUG
            QiyeBus bus = new QiyeBus();
            QyUserEntity qyuser = bus.QueryUser(new QyUserEntity { UserID = "0006" });
            string cqd = string.Format("cmd={0}&LoginName={1}", "queryUserInfoByUserID", qyuser.UserID);
            string result = wxHttpUtility.SendHttpRequest(Common.GethouseAPI(), cqd);
            userAPIMessage rows = Newtonsoft.Json.JsonConvert.DeserializeObject<userAPIMessage>(result);
            if (rows.Result)
            {
                qyuser.HouseID = rows.userEnt.HouseID;
                qyuser.UserName = rows.userEnt.UserName;
                qyuser.HouseName = string.IsNullOrEmpty(rows.userEnt.HouseName) ? "" : rows.userEnt.HouseName;
                qyuser.CargoPermisID = string.IsNullOrEmpty(rows.userEnt.CargoPermisID) ? "" : rows.userEnt.CargoPermisID;
                qyuser.CargoPermisName = string.IsNullOrEmpty(rows.userEnt.CargoPermisName) ? "" : rows.userEnt.CargoPermisName;
                qyuser.IsHeadHouse = Convert.ToInt32(rows.userEnt.IsHeadHouse);
                qyuser.HeadHouseID = Convert.ToInt32(rows.userEnt.HeadHouseID);
                qyuser.HeadHouseName = rows.userEnt.HeadHouseName;
                qyuser.RoleID = rows.userEnt.RoleID;
                qyuser.RoleName = rows.userEnt.RoleCName;
                qyuser.IsQueryLockStock = rows.userEnt.IsQueryLockStock;
            }
            Session["QyUser"] = qyuser;
            #endif
            if (Session["QyUser"] == null)
            {
                //WriteTextLog(Request.RawUrl, DateTime.Now);
                Session["To_Url"] = ".." + Request.RawUrl;
                string appID = Common.GetQYCorpID();
                string reUrl = Senparc.Weixin.HttpUtility.RequestUtility.UrlEncode(Common.GetRedirect_Url());

                string url = @"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appID + "&redirect_uri=" + reUrl + "&response_type=code&scope=snsapi_base&agentid=1000002&state=STATE#wechat_redirect";
                Response.Redirect(url);
            }

            //QyUserEntity entity = new QyUserEntity();
            //entity.HouseID = 9;
            //entity.CargoPermisID = "9";
            //entity.UserID = "0006";
            //entity.CheckRole = "4";
            //entity.UserName = "吴少武";
            //entity.HouseName = "广州仓库";
            //Session["QyUser"] = entity;

            base.OnLoad(e);
            //WriteTextLog("0", DateTime.Now);
            //CheckSeeion();
        }

        //检测Session是否存在,如果当前登入用户已超时则自动登入
        protected void CheckSeeion()
        {
            //HttpCookie Cookie = CookiesHelper.GetCookie("UserLoginInfo");
            //if (Cookie == null)
            //{
            //    Server.Transfer("/Default.aspx");
            //}
            //UserName = Convert.ToString(Cookie["loginid"]);
            //if (!mc.KeyExists(UserName))
            //{
            //    Server.Transfer("/Default.aspx");
            //    //mc.Set("user", userInfo);
            //}

            if (Session["QyUser"] == null)
            {
                Response.Write("<script>top.location.href=\"/Default.aspx\"</script>");
                //Server.Transfer("/Default.aspx");
            }


            //if (Session["user"] == null || Cookie == null || Cookie["loginid"] == null)
            //{
            //    Server.Transfer("/Default.aspx");
            //}
        }
        //<summary>
        //获取当前登录用户信息
        // </summary>
        protected QyUserEntity QyUserInfo
        {
            get { return (QyUserEntity)Session["QyUser"]; }
            //get { return (SystemUserEntity)mc.Get(UserName); }
        }
    }
}