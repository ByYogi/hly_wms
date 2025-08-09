using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class RedirectURL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.根据回传的URL获取 Code
            string code = Request["code"];
            try
            {
                string secret = ConfigurationSettings.AppSettings["MyWorkSecret"];
                //2.获取Token
                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), secret);
                //3.根据Token和Code获取用户UserID
                GetUserInfoResult user = OAuth2Api.GetUserId(token.access_token, code);
                QiyeBus bus = new QiyeBus();

                QyUserEntity qyuser = bus.QueryUser(new QyUserEntity { UserID = user.UserId });
                if (qyuser != null && !string.IsNullOrEmpty(qyuser.UserID))
                {
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
                        qyuser.IsQueryLockStock = rows.userEnt.IsQueryLockStock;
                        qyuser.HeadHouseName = rows.userEnt.HeadHouseName;
                        qyuser.RoleID = rows.userEnt.RoleID;
                        qyuser.RoleName = rows.userEnt.RoleCName;
                    }
                    Session["QyUser"] = qyuser;
                    Response.Redirect(Session["To_Url"].ToString());
                }
                else
                {
                    Response.Redirect("../QY/Error.aspx");
                }
            }
            catch (Exception ex)
            {
                //WriteTextLog(ex.Message, DateTime.Now);
            }
        }
    }
}